using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;

namespace SimpleSekiroSavegameHelper
{
    class PatternScan
    {
        private long dwStart = 0;
        private byte[] bData;

        internal PatternScan(IntPtr hProcess, long baseAddress, int moduleSize)
        {
            if (IntPtr.Size == 4)
                dwStart = (uint)baseAddress;
            else if (IntPtr.Size == 8)
                dwStart = baseAddress;
            bData = new byte[moduleSize];

            if (!ReadProcessMemory(hProcess, dwStart, bData, moduleSize, out IntPtr lpNumberOfBytesRead))
            {
                MessageBox.Show("Could not read memory in PatternScan()!", "Simple Sekiro Savegame Helper", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (lpNumberOfBytesRead.ToInt64() != moduleSize || bData == null || bData.Length == 0)
            {
                MessageBox.Show("ReadProcessMemory error in PatternScan()!", "Simple Sekiro Savegame Helper", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
        }

        ~PatternScan()
        {
            bData = null;
            GC.Collect();
        }

        /// <summary>
        /// Finds a pattern or signature inside initialized process.
        /// </summary>
        /// <param name="szPattern">A character-delimited string representing the pattern to be found, '??' act as a wildcard.</param>
        /// <param name="cDelimiter">Determines how the string will be split. Defaults is ' '.</param>
        /// <returns>The address of the beginning of the pattern if found, 0 if not found.</returns>
        internal Int64 FindPattern(string szPattern, char cDelimiter = ' ')
        {
            string[] saPattern = szPattern.Split(cDelimiter);
            string szMask = "";
            for (int i = 0; i < saPattern.Length; i++)
            {
                if (saPattern[i] == "??")
                {
                    szMask += "?";
                    saPattern[i] = "0";
                }
                else szMask += "x";
            }
            byte[] bPattern = new byte[saPattern.Length];
            for (int i = 0; i < saPattern.Length; i++)
                bPattern[i] = Convert.ToByte(saPattern[i], 0x10);

            if (bPattern == null || bPattern.Length == 0)
                throw new ArgumentException("Pattern's length is zero!");
            if (bPattern.Length != szMask.Length)
                throw new ArgumentException("Pattern's bytes and szMask must be of the same size!");

            long ix;
            int iy;

            List<byte> not0PatternBytesList = new List<byte>();
            List<int> not0PatternBytesIndexList = new List<int>();

            int dataLength = bData.Length - bPattern.Length;

            for (iy = bPattern.Length - 1; iy > -1; iy--)
            {
                if (szMask[iy] == 'x')
                {
                    not0PatternBytesList.Add(bPattern[iy]);
                    not0PatternBytesIndexList.Add(iy);
                }
            }

            byte[] not0PatternBytesArray = not0PatternBytesList.ToArray();
            int not0PatternBytesL = not0PatternBytesArray.Length;
            int[] not0PatternBytesIndexArray = not0PatternBytesIndexList.ToArray();

            for (ix = 0; ix < dataLength; ix++)
            {
                if (not0PatternBytesArray[not0PatternBytesL - 1] != bData[ix]) continue;
                bool check = true;

                for (iy = not0PatternBytesArray.Length - 1; iy > -1; iy--)
                {
                    if (not0PatternBytesArray[iy] == bData[ix + not0PatternBytesIndexArray[iy]])
                        continue;
                    check = false;
                    break;
                }

                if (check)
                {
                    return dwStart+ix;
                }
            }

            return -1;
        }


        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory(
           IntPtr hProcess,
           Int64 lpBaseAddress,
           [Out] Byte[] lpBuffer,
           Int64 dwSize,
           out IntPtr lpNumberOfBytesRead);
    }
}
