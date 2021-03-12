using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BF3 RID: 3059
	internal class DnsCNameRecord : DnsRecord
	{
		// Token: 0x06004323 RID: 17187 RVA: 0x000B3FAE File Offset: 0x000B21AE
		internal DnsCNameRecord(string name) : base(name)
		{
		}

		// Token: 0x06004324 RID: 17188 RVA: 0x000B3FB8 File Offset: 0x000B21B8
		internal DnsCNameRecord(Win32DnsRecordHeader header, IntPtr dataPointer) : base(header)
		{
			this.host = ((DnsCNameRecord.Win32DnsCNameRecord)Marshal.PtrToStructure(dataPointer, typeof(DnsCNameRecord.Win32DnsCNameRecord))).nameHost;
		}

		// Token: 0x06004325 RID: 17189 RVA: 0x000B3FEF File Offset: 0x000B21EF
		internal DnsCNameRecord(string name, string cname) : base(name)
		{
			this.host = cname;
			base.RecordType = DnsRecordType.CNAME;
		}

		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x06004326 RID: 17190 RVA: 0x000B4006 File Offset: 0x000B2206
		public static IComparer<DnsCNameRecord> Comparer
		{
			get
			{
				if (DnsCNameRecord.comparer == null)
				{
					DnsCNameRecord.comparer = new DnsCNameRecord.CNameComparer();
				}
				return DnsCNameRecord.comparer;
			}
		}

		// Token: 0x170010CB RID: 4299
		// (get) Token: 0x06004327 RID: 17191 RVA: 0x000B401E File Offset: 0x000B221E
		public string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x06004328 RID: 17192 RVA: 0x000B4026 File Offset: 0x000B2226
		public override string ToString()
		{
			return base.ToString() + " " + this.host;
		}

		// Token: 0x04003929 RID: 14633
		private static IComparer<DnsCNameRecord> comparer;

		// Token: 0x0400392A RID: 14634
		private string host;

		// Token: 0x02000BF4 RID: 3060
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct Win32DnsCNameRecord
		{
			// Token: 0x0400392B RID: 14635
			public string nameHost;
		}

		// Token: 0x02000BF5 RID: 3061
		private class CNameComparer : IComparer<DnsCNameRecord>
		{
			// Token: 0x06004329 RID: 17193 RVA: 0x000B4040 File Offset: 0x000B2240
			public int Compare(DnsCNameRecord x, DnsCNameRecord y)
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
					return string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase);
				}
			}
		}
	}
}
