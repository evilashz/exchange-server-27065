using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BFC RID: 3068
	internal class DnsNsRecord : DnsRecord
	{
		// Token: 0x0600433A RID: 17210 RVA: 0x000B43C5 File Offset: 0x000B25C5
		internal DnsNsRecord(string name) : base(name)
		{
		}

		// Token: 0x0600433B RID: 17211 RVA: 0x000B43D0 File Offset: 0x000B25D0
		internal DnsNsRecord(Win32DnsRecordHeader header, IntPtr dataPointer) : base(header)
		{
			this.host = ((DnsNsRecord.Win32DnsNsRecord)Marshal.PtrToStructure(dataPointer, typeof(DnsNsRecord.Win32DnsNsRecord))).nameHost;
		}

		// Token: 0x0600433C RID: 17212 RVA: 0x000B4407 File Offset: 0x000B2607
		internal DnsNsRecord(string name, string ns) : base(name)
		{
			this.host = ns;
			base.RecordType = DnsRecordType.NS;
		}

		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x0600433D RID: 17213 RVA: 0x000B441E File Offset: 0x000B261E
		public static IComparer<DnsNsRecord> Comparer
		{
			get
			{
				if (DnsNsRecord.comparer == null)
				{
					DnsNsRecord.comparer = new DnsNsRecord.NsComparer();
				}
				return DnsNsRecord.comparer;
			}
		}

		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x0600433E RID: 17214 RVA: 0x000B4436 File Offset: 0x000B2636
		public string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x0600433F RID: 17215 RVA: 0x000B443E File Offset: 0x000B263E
		public override string ToString()
		{
			return base.ToString() + " " + this.host;
		}

		// Token: 0x0400393E RID: 14654
		private static IComparer<DnsNsRecord> comparer;

		// Token: 0x0400393F RID: 14655
		private readonly string host;

		// Token: 0x02000BFD RID: 3069
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct Win32DnsNsRecord
		{
			// Token: 0x04003940 RID: 14656
			public string nameHost;
		}

		// Token: 0x02000BFE RID: 3070
		private class NsComparer : IComparer<DnsNsRecord>
		{
			// Token: 0x06004340 RID: 17216 RVA: 0x000B4456 File Offset: 0x000B2656
			public int Compare(DnsNsRecord x, DnsNsRecord y)
			{
				if (x == null)
				{
					if (y != null)
					{
						return -1;
					}
					return 0;
				}
				else
				{
					if (y == null)
					{
						return 1;
					}
					return string.Compare(x.Host, y.Host, StringComparison.OrdinalIgnoreCase);
				}
			}
		}
	}
}
