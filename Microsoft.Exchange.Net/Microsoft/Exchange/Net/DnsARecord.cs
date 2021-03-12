using System;
using System.Net;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BED RID: 3053
	internal class DnsARecord : DnsAddressRecord
	{
		// Token: 0x060042DB RID: 17115 RVA: 0x000B2178 File Offset: 0x000B0378
		internal DnsARecord(Win32DnsRecordHeader header, IntPtr dataPointer) : base(header)
		{
			this.address = new IPAddress((long)((ulong)((DnsARecord.Win32DnsARecord)Marshal.PtrToStructure(dataPointer, typeof(DnsARecord.Win32DnsARecord))).address));
		}

		// Token: 0x060042DC RID: 17116 RVA: 0x000B21B5 File Offset: 0x000B03B5
		internal DnsARecord(string name, IPAddress address) : base(name)
		{
			this.address = address;
			base.RecordType = DnsRecordType.A;
		}

		// Token: 0x02000BEE RID: 3054
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct Win32DnsARecord
		{
			// Token: 0x04003902 RID: 14594
			public uint address;
		}
	}
}
