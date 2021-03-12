using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C09 RID: 3081
	internal class DnsSrvRecord : DnsRecord
	{
		// Token: 0x0600437D RID: 17277 RVA: 0x000B5545 File Offset: 0x000B3745
		internal DnsSrvRecord(string name) : base(name)
		{
		}

		// Token: 0x0600437E RID: 17278 RVA: 0x000B5550 File Offset: 0x000B3750
		internal DnsSrvRecord(Win32DnsRecordHeader header, IntPtr dataPointer) : base(header)
		{
			DnsSrvRecord.Win32DnsSrvRecord win32DnsSrvRecord = (DnsSrvRecord.Win32DnsSrvRecord)Marshal.PtrToStructure(dataPointer, typeof(DnsSrvRecord.Win32DnsSrvRecord));
			this.target = win32DnsSrvRecord.nameTarget;
			this.priority = (int)win32DnsSrvRecord.priority;
			this.weight = (int)win32DnsSrvRecord.weight;
			this.port = (int)win32DnsSrvRecord.port;
		}

		// Token: 0x0600437F RID: 17279 RVA: 0x000B55AE File Offset: 0x000B37AE
		internal DnsSrvRecord(string name, string target) : base(name)
		{
			this.target = target;
			base.RecordType = DnsRecordType.SRV;
		}

		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x06004380 RID: 17280 RVA: 0x000B55C6 File Offset: 0x000B37C6
		public static IComparer<DnsSrvRecord> Comparer
		{
			get
			{
				if (DnsSrvRecord.comparer == null)
				{
					DnsSrvRecord.comparer = new DnsSrvRecord.SrvComparer();
				}
				return DnsSrvRecord.comparer;
			}
		}

		// Token: 0x170010E8 RID: 4328
		// (get) Token: 0x06004381 RID: 17281 RVA: 0x000B55DE File Offset: 0x000B37DE
		public string NameTarget
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x06004382 RID: 17282 RVA: 0x000B55E6 File Offset: 0x000B37E6
		public int Priority
		{
			get
			{
				return this.priority;
			}
		}

		// Token: 0x170010EA RID: 4330
		// (get) Token: 0x06004383 RID: 17283 RVA: 0x000B55EE File Offset: 0x000B37EE
		public int Weight
		{
			get
			{
				return this.weight;
			}
		}

		// Token: 0x170010EB RID: 4331
		// (get) Token: 0x06004384 RID: 17284 RVA: 0x000B55F6 File Offset: 0x000B37F6
		public int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x06004385 RID: 17285 RVA: 0x000B55FE File Offset: 0x000B37FE
		public override string ToString()
		{
			return base.ToString() + " " + this.target;
		}

		// Token: 0x04003971 RID: 14705
		private static IComparer<DnsSrvRecord> comparer;

		// Token: 0x04003972 RID: 14706
		private string target;

		// Token: 0x04003973 RID: 14707
		private int priority;

		// Token: 0x04003974 RID: 14708
		private int weight;

		// Token: 0x04003975 RID: 14709
		private int port;

		// Token: 0x02000C0A RID: 3082
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct Win32DnsSrvRecord
		{
			// Token: 0x04003976 RID: 14710
			public string nameTarget;

			// Token: 0x04003977 RID: 14711
			public ushort priority;

			// Token: 0x04003978 RID: 14712
			public ushort weight;

			// Token: 0x04003979 RID: 14713
			public ushort port;

			// Token: 0x0400397A RID: 14714
			public ushort pad;
		}

		// Token: 0x02000C0B RID: 3083
		private class SrvComparer : IComparer<DnsSrvRecord>
		{
			// Token: 0x06004386 RID: 17286 RVA: 0x000B5616 File Offset: 0x000B3816
			public int Compare(DnsSrvRecord a, DnsSrvRecord b)
			{
				if (a.priority == b.priority)
				{
					return a.weight.CompareTo(b.weight);
				}
				return a.priority.CompareTo(b.priority);
			}
		}
	}
}
