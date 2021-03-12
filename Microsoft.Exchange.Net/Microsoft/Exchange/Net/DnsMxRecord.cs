using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BF9 RID: 3065
	internal class DnsMxRecord : DnsRecord
	{
		// Token: 0x06004333 RID: 17203 RVA: 0x000B42E4 File Offset: 0x000B24E4
		internal DnsMxRecord(Win32DnsRecordHeader header, IntPtr dataPointer) : base(header)
		{
			DnsMxRecord.Win32DnsMxRecord win32DnsMxRecord = (DnsMxRecord.Win32DnsMxRecord)Marshal.PtrToStructure(dataPointer, typeof(DnsMxRecord.Win32DnsMxRecord));
			this.preference = (int)win32DnsMxRecord.preference;
			this.nameExchange = win32DnsMxRecord.nameExchange;
		}

		// Token: 0x170010CC RID: 4300
		// (get) Token: 0x06004334 RID: 17204 RVA: 0x000B4328 File Offset: 0x000B2528
		public static IComparer<DnsMxRecord> Comparer
		{
			get
			{
				if (DnsMxRecord.comparer == null)
				{
					DnsMxRecord.comparer = new DnsMxRecord.MxComparer();
				}
				return DnsMxRecord.comparer;
			}
		}

		// Token: 0x170010CD RID: 4301
		// (get) Token: 0x06004335 RID: 17205 RVA: 0x000B4340 File Offset: 0x000B2540
		public string NameExchange
		{
			get
			{
				return this.nameExchange;
			}
		}

		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x06004336 RID: 17206 RVA: 0x000B4348 File Offset: 0x000B2548
		public int Preference
		{
			get
			{
				return this.preference;
			}
		}

		// Token: 0x06004337 RID: 17207 RVA: 0x000B4350 File Offset: 0x000B2550
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				base.ToString(),
				" ",
				this.preference,
				" ",
				this.nameExchange
			});
		}

		// Token: 0x04003938 RID: 14648
		private static IComparer<DnsMxRecord> comparer;

		// Token: 0x04003939 RID: 14649
		private int preference;

		// Token: 0x0400393A RID: 14650
		private string nameExchange;

		// Token: 0x02000BFA RID: 3066
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct Win32DnsMxRecord
		{
			// Token: 0x0400393B RID: 14651
			public string nameExchange;

			// Token: 0x0400393C RID: 14652
			public ushort preference;

			// Token: 0x0400393D RID: 14653
			public ushort pad;
		}

		// Token: 0x02000BFB RID: 3067
		private class MxComparer : IComparer<DnsMxRecord>
		{
			// Token: 0x06004338 RID: 17208 RVA: 0x000B439C File Offset: 0x000B259C
			public int Compare(DnsMxRecord a, DnsMxRecord b)
			{
				return a.Preference.CompareTo(b.Preference);
			}
		}
	}
}
