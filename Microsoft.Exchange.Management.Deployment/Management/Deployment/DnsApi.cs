using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000009 RID: 9
	internal class DnsApi
	{
		// Token: 0x0600002F RID: 47
		[DllImport("dnsapi.dll", CharSet = CharSet.Unicode, EntryPoint = "DnsQuery_W")]
		public static extern int DnsQuery(string name, DnsApi.DnsRecordType type, DnsApi.DnsQueryOptions options, IntPtr dnsServers, ref IntPtr QueryResultSet, IntPtr reserved);

		// Token: 0x06000030 RID: 48
		[DllImport("dnsapi.dll", CharSet = CharSet.Unicode)]
		public static extern void DnsRecordListFree(IntPtr ptrRecords, DnsApi.DnsFreeType freeType);

		// Token: 0x0200000A RID: 10
		public enum DnsFreeType
		{
			// Token: 0x04000012 RID: 18
			DnsFreeFlat,
			// Token: 0x04000013 RID: 19
			DnsFreeRecordList,
			// Token: 0x04000014 RID: 20
			DnsFreeParsedMessageFields
		}

		// Token: 0x0200000B RID: 11
		public enum DnsStatus
		{
			// Token: 0x04000016 RID: 22
			Success
		}

		// Token: 0x0200000C RID: 12
		public enum DnsRecordType : ushort
		{
			// Token: 0x04000018 RID: 24
			A = 1,
			// Token: 0x04000019 RID: 25
			NS,
			// Token: 0x0400001A RID: 26
			CNAME = 5,
			// Token: 0x0400001B RID: 27
			SOA,
			// Token: 0x0400001C RID: 28
			PTR = 12,
			// Token: 0x0400001D RID: 29
			MX = 15,
			// Token: 0x0400001E RID: 30
			TXT,
			// Token: 0x0400001F RID: 31
			AAAA = 28,
			// Token: 0x04000020 RID: 32
			SRV = 33
		}

		// Token: 0x0200000D RID: 13
		[Flags]
		public enum DnsQueryOptions
		{
			// Token: 0x04000022 RID: 34
			Standard = 0,
			// Token: 0x04000023 RID: 35
			AcceptTruncatedResponse = 1,
			// Token: 0x04000024 RID: 36
			UseTcpOnly = 2,
			// Token: 0x04000025 RID: 37
			NoRecursion = 4,
			// Token: 0x04000026 RID: 38
			ByPassCache = 8,
			// Token: 0x04000027 RID: 39
			TreatAsFqdn = 4096
		}

		// Token: 0x0200000E RID: 14
		public enum DNSErrorCodes
		{
			// Token: 0x04000029 RID: 41
			DNS_ERROR_RESPONSE_CODES_BASE = 9000,
			// Token: 0x0400002A RID: 42
			DNS_ERROR_RCODE_FORMAT_ERROR,
			// Token: 0x0400002B RID: 43
			DNS_ERROR_RCODE_SERVER_FAILURE,
			// Token: 0x0400002C RID: 44
			DNS_ERROR_RCODE_NAME_ERROR
		}

		// Token: 0x0200000F RID: 15
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct DNS_MX_DATA
		{
			// Token: 0x0400002D RID: 45
			public string PNameExchange;

			// Token: 0x0400002E RID: 46
			public ushort WPreference;

			// Token: 0x0400002F RID: 47
			public ushort Pad;
		}

		// Token: 0x02000010 RID: 16
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct DNS_SRV_DATA
		{
			// Token: 0x04000030 RID: 48
			public string PNameTarget;

			// Token: 0x04000031 RID: 49
			public ushort WPriority;

			// Token: 0x04000032 RID: 50
			public ushort WWeight;

			// Token: 0x04000033 RID: 51
			public ushort WPort;

			// Token: 0x04000034 RID: 52
			public ushort Pad;
		}

		// Token: 0x02000011 RID: 17
		public struct IP6_ADDRESS
		{
			// Token: 0x04000035 RID: 53
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public byte[] Address;
		}

		// Token: 0x02000012 RID: 18
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct DNS_RECORD_BASE
		{
			// Token: 0x04000036 RID: 54
			public readonly IntPtr PNext;

			// Token: 0x04000037 RID: 55
			public string PName;

			// Token: 0x04000038 RID: 56
			public ushort WType;

			// Token: 0x04000039 RID: 57
			public ushort WDataLength;

			// Token: 0x0400003A RID: 58
			public uint Flags;

			// Token: 0x0400003B RID: 59
			public uint DWTtl;

			// Token: 0x0400003C RID: 60
			public uint DWReserved;
		}

		// Token: 0x02000013 RID: 19
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct DNS_RECORD_A
		{
			// Token: 0x0400003D RID: 61
			public readonly IntPtr PNext;

			// Token: 0x0400003E RID: 62
			public string PName;

			// Token: 0x0400003F RID: 63
			public ushort WType;

			// Token: 0x04000040 RID: 64
			public ushort WDataLength;

			// Token: 0x04000041 RID: 65
			public uint Flags;

			// Token: 0x04000042 RID: 66
			public uint DWTtl;

			// Token: 0x04000043 RID: 67
			public uint DWReserved;

			// Token: 0x04000044 RID: 68
			public uint A;
		}

		// Token: 0x02000014 RID: 20
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct DNS_RECORD_AAAA
		{
			// Token: 0x04000045 RID: 69
			public readonly IntPtr PNext;

			// Token: 0x04000046 RID: 70
			public string PName;

			// Token: 0x04000047 RID: 71
			public ushort WType;

			// Token: 0x04000048 RID: 72
			public ushort WDataLength;

			// Token: 0x04000049 RID: 73
			public uint Flags;

			// Token: 0x0400004A RID: 74
			public uint DWTtl;

			// Token: 0x0400004B RID: 75
			public uint DWReserved;

			// Token: 0x0400004C RID: 76
			public DnsApi.IP6_ADDRESS A;
		}

		// Token: 0x02000015 RID: 21
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct DNS_RECORD_PTR
		{
			// Token: 0x0400004D RID: 77
			public readonly IntPtr PNext;

			// Token: 0x0400004E RID: 78
			public string PName;

			// Token: 0x0400004F RID: 79
			public ushort WType;

			// Token: 0x04000050 RID: 80
			public ushort WDataLength;

			// Token: 0x04000051 RID: 81
			public uint Flags;

			// Token: 0x04000052 RID: 82
			public uint DWTtl;

			// Token: 0x04000053 RID: 83
			public uint DWReserved;

			// Token: 0x04000054 RID: 84
			public string PTR;
		}

		// Token: 0x02000016 RID: 22
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct DNS_RECORD_MX
		{
			// Token: 0x04000055 RID: 85
			public readonly IntPtr PNext;

			// Token: 0x04000056 RID: 86
			public string PName;

			// Token: 0x04000057 RID: 87
			public ushort WType;

			// Token: 0x04000058 RID: 88
			public ushort WDataLength;

			// Token: 0x04000059 RID: 89
			public uint Flags;

			// Token: 0x0400005A RID: 90
			public uint DWTtl;

			// Token: 0x0400005B RID: 91
			public uint DWReserved;

			// Token: 0x0400005C RID: 92
			public DnsApi.DNS_MX_DATA MX;
		}

		// Token: 0x02000017 RID: 23
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct DNS_RECORD_SRV
		{
			// Token: 0x0400005D RID: 93
			public readonly IntPtr PNext;

			// Token: 0x0400005E RID: 94
			public string PName;

			// Token: 0x0400005F RID: 95
			public ushort WType;

			// Token: 0x04000060 RID: 96
			public ushort WDataLength;

			// Token: 0x04000061 RID: 97
			public uint Flags;

			// Token: 0x04000062 RID: 98
			public uint DWTtl;

			// Token: 0x04000063 RID: 99
			public uint DWReserved;

			// Token: 0x04000064 RID: 100
			public DnsApi.DNS_SRV_DATA SRV;
		}
	}
}
