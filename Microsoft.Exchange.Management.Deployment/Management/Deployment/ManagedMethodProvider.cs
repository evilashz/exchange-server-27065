using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Management.Analysis.Features;
using Microsoft.Exchange.Setup.Common;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000020 RID: 32
	internal class ManagedMethodProvider : IManagedMethodProvider
	{
		// Token: 0x06000052 RID: 82 RVA: 0x000024D0 File Offset: 0x000006D0
		public Dictionary<string, object[]> CheckDNS(string ipAddress, string svrFQDN)
		{
			Dictionary<string, object[]> dictionary = new Dictionary<string, object[]>();
			string arg = string.Empty;
			ipAddress = ipAddress.TrimEnd(ManagedMethodProvider.aipArrayDelimiter);
			string[] array = ipAddress.Split(ManagedMethodProvider.aipArrayDelimiter);
			ManagedMethodProvider.IP4_ARRAY[] array2 = new ManagedMethodProvider.IP4_ARRAY[array.Length];
			int num = 0;
			foreach (string ipAddress2 in array)
			{
				array2[num].AddrCount = 1U;
				array2[num].AddrArray = this.ConvertIPToUINT(ipAddress2);
				num++;
			}
			DateTime d = DateTime.UtcNow.ToLocalTime();
			string str = Strings.ServerFQDNDisplayName;
			for (int j = 0; j < num; j++)
			{
				d = DateTime.UtcNow.ToLocalTime();
				arg = this.ProcessDnsQuery(svrFQDN, array[j], array2[j]);
				dictionary.Add("A", new object[]
				{
					string.Format("TimeSpan = {0}", (DateTime.UtcNow.ToLocalTime() - d).TotalMilliseconds.ToString()),
					string.Format("DNS Server = {0}", array[j]),
					str + " = " + svrFQDN,
					string.Format("DNS Query Result = {0}", arg)
				});
			}
			return dictionary;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002630 File Offset: 0x00000830
		public Dictionary<string, object[]> PortAvailable(string svrName, Dictionary<string, List<string>> commands)
		{
			Dictionary<string, object[]> dictionary = new Dictionary<string, object[]>();
			DateTime d = DateTime.UtcNow.ToLocalTime();
			foreach (KeyValuePair<string, List<string>> keyValuePair in commands)
			{
				string value = keyValuePair.Value[0];
				string text = keyValuePair.Value[1];
				string text2 = keyValuePair.Value[2];
				int timeout = 0;
				try
				{
					if (text2.Length > 0)
					{
						timeout = Convert.ToInt32(text2);
					}
					if (text.Length > 0 && text != "TCP")
					{
						throw new ArgumentException();
					}
					int portNum = Convert.ToInt32(value);
					d = DateTime.UtcNow.ToLocalTime();
					object[] value2 = new object[]
					{
						(DateTime.UtcNow.ToLocalTime() - d).TotalMilliseconds.ToString(),
						this.VerifyPort(svrName, portNum, text, timeout).ToString()
					};
					dictionary.Add(keyValuePair.Key, value2);
				}
				catch (Exception e)
				{
					SetupLogger.LogError(e);
					throw;
				}
			}
			return dictionary;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002784 File Offset: 0x00000984
		public string GetUserNameEx(ValidationConstant.ExtendedNameFormat extendedNameType)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			try
			{
				int capacity = stringBuilder.Capacity;
				if (ManagedMethodProvider.GetUserNameEx(extendedNameType, stringBuilder, ref capacity) == 0)
				{
					int num = Marshal.GetLastWin32Error();
					if (num == 234)
					{
						stringBuilder.Capacity = capacity;
						num = 0;
						if (ManagedMethodProvider.GetUserNameEx(extendedNameType, stringBuilder, ref capacity) == 0)
						{
							num = Marshal.GetLastWin32Error();
						}
					}
					if (num != 0)
					{
						throw new Win32Exception(num);
					}
				}
			}
			catch (Exception e)
			{
				SetupLogger.LogError(e);
				throw;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002804 File Offset: 0x00000A04
		public string GetComputerNameEx(ValidationConstant.ComputerNameFormat computerNameFormat)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			try
			{
				int capacity = stringBuilder.Capacity;
				if (ManagedMethodProvider.GetComputerNameEx(computerNameFormat, stringBuilder, ref capacity) == 0)
				{
					int num = Marshal.GetLastWin32Error();
					if (num == 234)
					{
						stringBuilder.Capacity = capacity;
						num = 0;
						if (ManagedMethodProvider.GetComputerNameEx(computerNameFormat, stringBuilder, ref capacity) == 0)
						{
							num = Marshal.GetLastWin32Error();
						}
					}
					if (num != 0)
					{
						throw new Win32Exception(num);
					}
				}
			}
			catch (Exception e)
			{
				SetupLogger.LogError(e);
				throw;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000056 RID: 86
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int GetComputerNameEx(ValidationConstant.ComputerNameFormat nameFormat, StringBuilder nameBuffer, ref int nSize);

		// Token: 0x06000057 RID: 87
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern byte GetUserNameEx(ValidationConstant.ExtendedNameFormat nameFormat, StringBuilder nameBuffer, ref int nSize);

		// Token: 0x06000058 RID: 88 RVA: 0x00002884 File Offset: 0x00000A84
		private static Socket ConnectSocket(string svrName, string protocol, int portNum, int timeOut, int connectTimeout)
		{
			Socket result = null;
			SocketType socketType;
			ProtocolType protocolType;
			SocketOptionLevel optionLevel;
			if (protocol != null)
			{
				if (protocol == "UDP")
				{
					socketType = SocketType.Dgram;
					protocolType = ProtocolType.Udp;
					optionLevel = SocketOptionLevel.Udp;
					goto IL_61;
				}
				if (protocol == "ICMP")
				{
					socketType = SocketType.Raw;
					protocolType = ProtocolType.Icmp;
					optionLevel = SocketOptionLevel.Socket;
					goto IL_61;
				}
				if (!(protocol == "TCP"))
				{
				}
			}
			socketType = SocketType.Stream;
			protocolType = ProtocolType.Tcp;
			optionLevel = SocketOptionLevel.Socket;
			IL_61:
			Socket socket = null;
			try
			{
				IPHostEntry hostEntry = Dns.GetHostEntry(svrName);
				foreach (IPAddress address in hostEntry.AddressList)
				{
					IPEndPoint ipendPoint = new IPEndPoint(address, portNum);
					if (socket != null)
					{
						socket.Close();
						socket = null;
					}
					socket = new Socket(ipendPoint.AddressFamily, socketType, protocolType);
					bool flag;
					if (connectTimeout == 0)
					{
						socket.Connect(ipendPoint);
						flag = socket.Connected;
					}
					else
					{
						IAsyncResult asyncResult = socket.BeginConnect(ipendPoint, null, null);
						flag = asyncResult.AsyncWaitHandle.WaitOne(connectTimeout, true);
					}
					if (flag)
					{
						if (protocolType != ProtocolType.Udp)
						{
							socket.SetSocketOption(optionLevel, SocketOptionName.ReceiveTimeout, timeOut);
							socket.SetSocketOption(optionLevel, SocketOptionName.SendTimeout, timeOut);
						}
						result = socket;
						socket = null;
						break;
					}
				}
			}
			catch (Exception e)
			{
				SetupLogger.LogError(e);
			}
			finally
			{
				if (socket != null)
				{
					socket.Close();
					socket = null;
				}
			}
			return result;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000029F0 File Offset: 0x00000BF0
		private static void DisconnectSocket(Socket socket)
		{
			if (socket != null && socket.Connected)
			{
				socket.Shutdown(SocketShutdown.Both);
				socket.Close();
			}
			socket = null;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002A10 File Offset: 0x00000C10
		private bool VerifyPort(string svrName, int portNum, string protocol, int timeout)
		{
			Socket socket = ManagedMethodProvider.ConnectSocket(svrName, protocol, portNum, 60000, timeout);
			bool result = socket != null && socket.Connected;
			ManagedMethodProvider.DisconnectSocket(socket);
			return result;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002A48 File Offset: 0x00000C48
		private string ProcessDnsQuery(string pName, string ipDNSName, ManagedMethodProvider.IP4_ARRAY ipDNS)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr zero = IntPtr.Zero;
			StringBuilder stringBuilder = new StringBuilder();
			DnsApi.DnsRecordType dnsRecordType = DnsApi.DnsRecordType.A;
			DnsApi.DnsQueryOptions options = DnsApi.DnsQueryOptions.ByPassCache | DnsApi.DnsQueryOptions.TreatAsFqdn;
			try
			{
				intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(ipDNS));
				Marshal.StructureToPtr(ipDNS, intPtr, false);
				int num = DnsApi.DnsQuery(pName, dnsRecordType, options, intPtr, ref zero, IntPtr.Zero);
				if (num == 0)
				{
					IntPtr intPtr2 = zero;
					do
					{
						DnsApi.DNS_RECORD_BASE dns_RECORD_BASE = (DnsApi.DNS_RECORD_BASE)Marshal.PtrToStructure(intPtr2, typeof(DnsApi.DNS_RECORD_BASE));
						if (dns_RECORD_BASE.WType == (ushort)dnsRecordType)
						{
							DnsApi.DnsRecordType wtype = (DnsApi.DnsRecordType)dns_RECORD_BASE.WType;
							if (wtype == DnsApi.DnsRecordType.A)
							{
								stringBuilder.Append(this.ConvertUINTToIP(((DnsApi.DNS_RECORD_A)Marshal.PtrToStructure(intPtr2, typeof(DnsApi.DNS_RECORD_A))).A) + ";");
							}
						}
						intPtr2 = dns_RECORD_BASE.PNext;
					}
					while (intPtr2 != IntPtr.Zero);
					if (stringBuilder.Length > 0)
					{
						stringBuilder = (stringBuilder = new StringBuilder(stringBuilder.ToString().TrimEnd(ManagedMethodProvider.aip6AdrDelimiter)));
					}
				}
				else if (num != 9501)
				{
					Win32Exception ex = new Win32Exception(num);
					throw ex;
				}
			}
			catch (Exception ex2)
			{
				throw new Exception(Strings.ErrorDNSQueryA(ipDNSName, pName, ex2.Message), ex2);
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					DnsApi.DnsRecordListFree(zero, DnsApi.DnsFreeType.DnsFreeRecordList);
				}
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002BE4 File Offset: 0x00000DE4
		private string ConvertUINTToIP(uint ipNumber)
		{
			string text = string.Empty;
			uint num = 255U;
			for (int i = 0; i <= 24; i += 8)
			{
				text = text + ((byte)((ipNumber & num) >> i)).ToString() + ".";
				num <<= 8;
			}
			return text.TrimEnd(ManagedMethodProvider.aipAdrDelimiter);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002C38 File Offset: 0x00000E38
		private uint ConvertIPToUINT(string ipAddress)
		{
			string[] array = ipAddress.Split(ManagedMethodProvider.aipAdrDelimiter);
			uint num = 0U;
			if (array.Length == 4)
			{
				for (int i = 3; i >= 0; i--)
				{
					num = num * 256U + Convert.ToUInt32(array[i]);
				}
			}
			return num;
		}

		// Token: 0x04000065 RID: 101
		private const string IpAdrDelimiter = ".";

		// Token: 0x04000066 RID: 102
		private const string Ip6AdrDelimiter = ":";

		// Token: 0x04000067 RID: 103
		private const string IpItemDelimiter = ",";

		// Token: 0x04000068 RID: 104
		private const string IpArrayDelimiter = ";";

		// Token: 0x04000069 RID: 105
		private const string SzARPAAddressBase = "in-addr.arpa.";

		// Token: 0x0400006A RID: 106
		private const int DEFAULTTIMEOUT = 60000;

		// Token: 0x0400006B RID: 107
		private const int MAXTIMEOUT = 120000;

		// Token: 0x0400006C RID: 108
		private const int ErrorMoreData = 234;

		// Token: 0x0400006D RID: 109
		private static char[] aipAdrDelimiter = new char[]
		{
			'.'
		};

		// Token: 0x0400006E RID: 110
		private static char[] aip6AdrDelimiter = new char[]
		{
			':'
		};

		// Token: 0x0400006F RID: 111
		private static char[] aipArrayDelimiter = new char[]
		{
			';'
		};

		// Token: 0x04000070 RID: 112
		private static char[] aipItemDelimiter = new char[]
		{
			','
		};

		// Token: 0x02000021 RID: 33
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct IP4_ARRAY
		{
			// Token: 0x04000071 RID: 113
			public uint AddrCount;

			// Token: 0x04000072 RID: 114
			public uint AddrArray;
		}
	}
}
