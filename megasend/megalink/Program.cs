using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.IO.Ports;

namespace megalink
{
    class Program
    {
        static SerialPort port;

        static void Main(string[] args)
        {
            port = new SerialPort("/dev/tty.usbmodem00000000001A1");
            port.Open();
            port.ReadExisting();
            port.ReadTimeout = 2000;
            port.WriteTimeout = 2000;

            byte[] data = File.ReadAllBytes("hello.txt");
            memWR(0x1810000, data, 0, data.Length);
        }

        static void tx32(int arg)
        {
            byte[] buff = new byte[4];
            buff[0] = (byte)(arg >> 24);
            buff[1] = (byte)(arg >> 16);
            buff[2] = (byte)(arg >> 8);
            buff[3] = (byte)(arg);

            txData(buff, 0, buff.Length);
        }

        static void tx8(int arg)
        {
            byte[] buff = new byte[1];
            buff[0] = (byte)(arg);
            txData(buff, 0, buff.Length);
        }

        static void txData(byte[] buff)
        {
            txData(buff, 0, buff.Length);
        }

        static void txData(byte[] buff, int offset, int len)
        {
            while (len > 0)
            {
                int block = 8192;
                if (block > len) block = len;

                port.Write(buff, offset, block);

                for (int i = offset; i < block; i++)
                {
                    Console.Write("{0:x2} ", buff[i]);
                }

                len -= block;
                offset += block;

            }
        }

        //************************************************************************************************ 

        static void txCMD(byte cmd_code)
        {
            byte[] cmd = new byte[4];
            cmd[0] = (byte)('+');
            cmd[1] = (byte)('+' ^ 0xff);
            cmd[2] = cmd_code;
            cmd[3] = (byte)(cmd_code ^ 0xff);
            txData(cmd);
        }

        public static void memWR(int addr, byte[] buff, int offset, int len)
        {
            const byte CMD_MEM_WR = 0x1A;

            if (len == 0) return;
            txCMD(CMD_MEM_WR);
            tx32(addr);
            tx32(len);
            tx8(0);//exec
            txData(buff, offset, len);
        }
    }
}
