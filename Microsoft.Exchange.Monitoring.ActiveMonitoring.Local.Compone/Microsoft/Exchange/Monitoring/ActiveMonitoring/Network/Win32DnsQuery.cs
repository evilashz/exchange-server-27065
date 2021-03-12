using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Network
{
	// Token: 0x0200021F RID: 543
	internal static class Win32DnsQuery
	{
		// Token: 0x06000F3F RID: 3903 RVA: 0x0006588F File Offset: 0x00063A8F
		internal static Win32DnsQueryResult<IPAddress> ResolveRecordsA(string domainName, IPAddress server)
		{
			return Win32DnsQuery.ResolveRecords<IPAddress>(domainName, server, Win32DnsQuery.RecordType.DNS_TYPE_A, (Win32DnsQuery.DnsRecord dnsRecord) => new IPAddress((long)((ulong)((uint)Marshal.PtrToStructure(dnsRecord.Data, typeof(uint))))));
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x000658E3 File Offset: 0x00063AE3
		internal static Win32DnsQueryResult<IPAddress> ResolveRecordsAaaa(string domainName, IPAddress server)
		{
			return Win32DnsQuery.ResolveRecords<IPAddress>(domainName, server, Win32DnsQuery.RecordType.DNS_TYPE_AAAA, delegate(Win32DnsQuery.DnsRecord dnsRecord)
			{
				byte[] array = new byte[16];
				Marshal.Copy(dnsRecord.Data, array, 0, 16);
				return new IPAddress(array);
			});
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0006592A File Offset: 0x00063B2A
		internal static Win32DnsQueryResult<string> ResolveRecordsAny(string domainName, IPAddress server)
		{
			return Win32DnsQuery.ResolveRecords<string>(domainName, server, Win32DnsQuery.RecordType.DNS_TYPE_ANY, (Win32DnsQuery.DnsRecord dnsRecord) => string.Format("{0}  {1}", dnsRecord.Name, (Win32DnsQuery.RecordType)dnsRecord.Type));
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x00065968 File Offset: 0x00063B68
		internal static Win32DnsQueryResult<string> ResolveRecordsCname(string domainName, IPAddress server)
		{
			return Win32DnsQuery.ResolveRecords<string>(domainName, server, Win32DnsQuery.RecordType.DNS_TYPE_CNAME, (Win32DnsQuery.DnsRecord dnsRecord) => Marshal.PtrToStringUni(Marshal.ReadIntPtr(dnsRecord.Data)));
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x00065A1B File Offset: 0x00063C1B
		internal static Win32DnsQueryResult<string> ResolveRecordsTxt(string domainName, IPAddress server)
		{
			return Win32DnsQuery.ResolveRecords<string>(domainName, server, Win32DnsQuery.RecordType.DNS_TYPE_TEXT, delegate(Win32DnsQuery.DnsRecord dnsRecord)
			{
				Win32DnsQuery.DNS_TXT_DATA dns_TXT_DATA = (Win32DnsQuery.DNS_TXT_DATA)Marshal.PtrToStructure(dnsRecord.Data, typeof(Win32DnsQuery.DNS_TXT_DATA));
				if (dns_TXT_DATA.StringCount == 1U)
				{
					return Marshal.PtrToStringUni(dns_TXT_DATA.StringArray);
				}
				StringBuilder stringBuilder = new StringBuilder();
				IntPtr intPtr = dns_TXT_DATA.StringArray;
				int num = 0;
				while ((long)num < (long)((ulong)dns_TXT_DATA.StringCount))
				{
					string text = Marshal.PtrToStringUni(intPtr);
					stringBuilder.Append(text);
					intPtr += 2 * (text.Length + 1);
					num++;
				}
				return stringBuilder.ToString();
			});
		}

		// Token: 0x06000F44 RID: 3908
		[DllImport("dnsapi", CharSet = CharSet.Unicode, EntryPoint = "DnsQuery_W", ExactSpelling = true)]
		private static extern int DnsQuery([MarshalAs(UnmanagedType.LPWStr)] string lpstrName, Win32DnsQuery.RecordType wType, Win32DnsQuery.QueryOptions fOptions, ref Win32DnsQuery.IP4_ARRAY pServerIpArray, out IntPtr ppQueryResultsSet, uint pReserved);

		// Token: 0x06000F45 RID: 3909
		[DllImport("dnsapi")]
		private static extern void DnsRecordListFree(IntPtr pRecordList, int fFreeType);

		// Token: 0x06000F46 RID: 3910 RVA: 0x00065A44 File Offset: 0x00063C44
		private static Win32DnsQuery.IP4_ARRAY GetServerIp4Array(IPAddress server)
		{
			Win32DnsQuery.IP4_ARRAY result = default(Win32DnsQuery.IP4_ARRAY);
			if (server == null || server == IPAddress.None)
			{
				result.AddrCount = 0U;
				result.AddrArray = null;
			}
			else
			{
				if (server.AddressFamily != AddressFamily.InterNetwork)
				{
					throw new ArgumentException(string.Format("The IP address must be an IPv4 address ({0}).", server), "server");
				}
				result.AddrCount = 1U;
				result.AddrArray = new uint[]
				{
					BitConverter.ToUInt32(server.GetAddressBytes(), 0)
				};
			}
			return result;
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x00065ABC File Offset: 0x00063CBC
		private static Win32DnsQueryResult<T> ResolveRecords<T>(string domainName, IPAddress server, Win32DnsQuery.RecordType type, Func<Win32DnsQuery.DnsRecord, T> interpreter)
		{
			Win32DnsQuery.ValidateArgumentNotNull(domainName, "domainName");
			Win32DnsQuery.ValidateArgumentNotNull(interpreter, "interpreter");
			Win32DnsQuery.QueryOptions fOptions;
			if (server == null)
			{
				fOptions = Win32DnsQuery.QueryOptions.DNS_QUERY_STANDARD;
			}
			else
			{
				fOptions = Win32DnsQuery.QueryOptions.DNS_QUERY_BYPASS_CACHE;
			}
			Win32DnsQuery.IP4_ARRAY serverIp4Array = Win32DnsQuery.GetServerIp4Array(server);
			IntPtr zero = IntPtr.Zero;
			Win32DnsQueryResult<T> result;
			try
			{
				Stopwatch stopwatch = Stopwatch.StartNew();
				long num = (long)Win32DnsQuery.DnsQuery(domainName, type, fOptions, ref serverIp4Array, out zero, 0U);
				stopwatch.Stop();
				if (num != 0L)
				{
					result = new Win32DnsQueryResult<T>(stopwatch.Elapsed, num, null);
				}
				else
				{
					List<T> list = new List<T>();
					IntPtr intPtr = zero;
					while (intPtr != IntPtr.Zero)
					{
						Win32DnsQuery.DnsRecord arg = (Win32DnsQuery.DnsRecord)Marshal.PtrToStructure(intPtr, typeof(Win32DnsQuery.DnsRecord));
						if (arg.Type == (ushort)type || type == Win32DnsQuery.RecordType.DNS_TYPE_ANY)
						{
							arg.Data = intPtr + Marshal.OffsetOf(typeof(Win32DnsQuery.DnsRecord), "Data").ToInt32();
							T item = interpreter(arg);
							list.Add(item);
						}
						intPtr = arg.Next;
					}
					result = new Win32DnsQueryResult<T>(stopwatch.Elapsed, num, list.ToArray());
				}
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					Win32DnsQuery.DnsRecordListFree(zero, 1);
				}
			}
			return result;
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x00065BF8 File Offset: 0x00063DF8
		private static void ValidateArgumentNotNull(object argument, string name)
		{
			if (argument == null)
			{
				throw new ArgumentNullException(name);
			}
		}

		// Token: 0x02000220 RID: 544
		[Flags]
		private enum QueryOptions : uint
		{
			// Token: 0x04000B75 RID: 2933
			DNS_QUERY_STANDARD = 0U,
			// Token: 0x04000B76 RID: 2934
			DNS_QUERY_ACCEPT_TRUNCATED_RESPONSE = 1U,
			// Token: 0x04000B77 RID: 2935
			DNS_QUERY_USE_TCP_ONLY = 2U,
			// Token: 0x04000B78 RID: 2936
			DNS_QUERY_NO_RECURSION = 4U,
			// Token: 0x04000B79 RID: 2937
			DNS_QUERY_BYPASS_CACHE = 8U,
			// Token: 0x04000B7A RID: 2938
			DNS_QUERY_NO_WIRE_QUERY = 16U,
			// Token: 0x04000B7B RID: 2939
			DNS_QUERY_NO_LOCAL_NAME = 32U,
			// Token: 0x04000B7C RID: 2940
			DNS_QUERY_NO_HOSTS_FILE = 64U,
			// Token: 0x04000B7D RID: 2941
			DNS_QUERY_NO_NETBT = 128U,
			// Token: 0x04000B7E RID: 2942
			DNS_QUERY_WIRE_ONLY = 256U,
			// Token: 0x04000B7F RID: 2943
			DNS_QUERY_RETURN_MESSAGE = 512U,
			// Token: 0x04000B80 RID: 2944
			DNS_QUERY_MULTICAST_ONLY = 1024U,
			// Token: 0x04000B81 RID: 2945
			DNS_QUERY_NO_MULTICAST = 2048U,
			// Token: 0x04000B82 RID: 2946
			DNS_QUERY_TREAT_AS_FQDN = 4096U,
			// Token: 0x04000B83 RID: 2947
			DNS_QUERY_ADDRCONFIG = 8192U,
			// Token: 0x04000B84 RID: 2948
			DNS_QUERY_DUAL_ADDR = 16384U,
			// Token: 0x04000B85 RID: 2949
			DNS_QUERY_MULTICAST_WAIT = 131072U,
			// Token: 0x04000B86 RID: 2950
			DNS_QUERY_MULTICAST_VERIFY = 262144U,
			// Token: 0x04000B87 RID: 2951
			DNS_QUERY_DONT_RESET_TTL_VALUES = 1048576U,
			// Token: 0x04000B88 RID: 2952
			DNS_QUERY_DISABLE_IDN_ENCODING = 2097152U,
			// Token: 0x04000B89 RID: 2953
			DNS_QUERY_APPEND_MULTILABEL = 8388608U,
			// Token: 0x04000B8A RID: 2954
			DNS_QUERY_RESERVED = 4026531840U
		}

		// Token: 0x02000221 RID: 545
		private enum RecordType : ushort
		{
			// Token: 0x04000B8C RID: 2956
			DNS_TYPE_A = 1,
			// Token: 0x04000B8D RID: 2957
			DNS_TYPE_CNAME = 5,
			// Token: 0x04000B8E RID: 2958
			DNS_TYPE_TEXT = 16,
			// Token: 0x04000B8F RID: 2959
			DNS_TYPE_AAAA = 28,
			// Token: 0x04000B90 RID: 2960
			DNS_TYPE_ANY = 255
		}

		// Token: 0x02000222 RID: 546
		private struct DnsRecord
		{
			// Token: 0x04000B91 RID: 2961
			public IntPtr Next;

			// Token: 0x04000B92 RID: 2962
			[MarshalAs(UnmanagedType.LPWStr)]
			public string Name;

			// Token: 0x04000B93 RID: 2963
			public ushort Type;

			// Token: 0x04000B94 RID: 2964
			public ushort DataLength;

			// Token: 0x04000B95 RID: 2965
			public uint Flags;

			// Token: 0x04000B96 RID: 2966
			public uint Ttl;

			// Token: 0x04000B97 RID: 2967
			public uint Reserved;

			// Token: 0x04000B98 RID: 2968
			public IntPtr Data;
		}

		// Token: 0x02000223 RID: 547
		private struct DNS_TXT_DATA
		{
			// Token: 0x04000B99 RID: 2969
			public uint StringCount;

			// Token: 0x04000B9A RID: 2970
			public IntPtr StringArray;
		}

		// Token: 0x02000224 RID: 548
		private struct IP4_ARRAY
		{
			// Token: 0x04000B9B RID: 2971
			public uint AddrCount;

			// Token: 0x04000B9C RID: 2972
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.U4)]
			public uint[] AddrArray;
		}
	}
}
