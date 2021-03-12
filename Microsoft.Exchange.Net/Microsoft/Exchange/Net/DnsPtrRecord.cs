using System;
using System.Net;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BFF RID: 3071
	internal class DnsPtrRecord : DnsRecord
	{
		// Token: 0x06004342 RID: 17218 RVA: 0x000B4484 File Offset: 0x000B2684
		internal DnsPtrRecord(Win32DnsRecordHeader header, IntPtr dataPointer) : base(header)
		{
			this.host = ((DnsPtrRecord.Win32DnsPtrRecord)Marshal.PtrToStructure(dataPointer, typeof(DnsPtrRecord.Win32DnsPtrRecord))).nameHost;
		}

		// Token: 0x06004343 RID: 17219 RVA: 0x000B44BB File Offset: 0x000B26BB
		internal DnsPtrRecord(IPAddress address, string host) : base(PtrRequest.ConstructPTRQuery(address))
		{
			this.host = host;
			base.RecordType = DnsRecordType.PTR;
		}

		// Token: 0x06004344 RID: 17220 RVA: 0x000B44D8 File Offset: 0x000B26D8
		internal DnsPtrRecord(string name, string host) : base(name)
		{
			this.host = host;
			base.RecordType = DnsRecordType.PTR;
		}

		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x06004345 RID: 17221 RVA: 0x000B44F0 File Offset: 0x000B26F0
		public string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x06004346 RID: 17222 RVA: 0x000B44F8 File Offset: 0x000B26F8
		public override string ToString()
		{
			return base.ToString() + " " + this.host;
		}

		// Token: 0x04003941 RID: 14657
		private string host;

		// Token: 0x02000C00 RID: 3072
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct Win32DnsPtrRecord
		{
			// Token: 0x04003942 RID: 14658
			public string nameHost;
		}
	}
}
