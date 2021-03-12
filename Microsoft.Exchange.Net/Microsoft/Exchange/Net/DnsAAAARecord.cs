using System;
using System.Net;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BEB RID: 3051
	internal class DnsAAAARecord : DnsAddressRecord
	{
		// Token: 0x060042D9 RID: 17113 RVA: 0x000B2120 File Offset: 0x000B0320
		internal DnsAAAARecord(Win32DnsRecordHeader header, IntPtr dataPointer) : base(header)
		{
			this.address = new IPAddress(((DnsAAAARecord.AryIp6Address)Marshal.PtrToStructure(dataPointer, typeof(DnsAAAARecord.AryIp6Address))).bytes);
		}

		// Token: 0x060042DA RID: 17114 RVA: 0x000B215C File Offset: 0x000B035C
		internal DnsAAAARecord(string name, IPAddress address) : base(name, address)
		{
			this.address = address;
			base.RecordType = DnsRecordType.AAAA;
		}

		// Token: 0x02000BEC RID: 3052
		private struct AryIp6Address
		{
			// Token: 0x04003901 RID: 14593
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public byte[] bytes;
		}
	}
}
