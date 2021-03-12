using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C07 RID: 3079
	internal class DnsSoaRecord : DnsRecord
	{
		// Token: 0x06004373 RID: 17267 RVA: 0x000B5464 File Offset: 0x000B3664
		internal DnsSoaRecord(string name) : base(name)
		{
		}

		// Token: 0x06004374 RID: 17268 RVA: 0x000B5470 File Offset: 0x000B3670
		internal DnsSoaRecord(Win32DnsRecordHeader header, IntPtr dataPointer) : base(header)
		{
			DnsSoaRecord.Win32DnsSoaRecord win32DnsSoaRecord = (DnsSoaRecord.Win32DnsSoaRecord)Marshal.PtrToStructure(dataPointer, typeof(DnsSoaRecord.Win32DnsSoaRecord));
			this.primaryServer = win32DnsSoaRecord.namePrimaryServer;
			this.administrator = win32DnsSoaRecord.nameAdministrator;
			this.serialNumber = win32DnsSoaRecord.serialNumber;
			this.refresh = win32DnsSoaRecord.refresh;
			this.retry = win32DnsSoaRecord.retry;
			this.expire = win32DnsSoaRecord.expire;
			this.defaultTimeToLive = win32DnsSoaRecord.defaultTTL;
		}

		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x06004375 RID: 17269 RVA: 0x000B54F5 File Offset: 0x000B36F5
		public string PrimaryServer
		{
			get
			{
				return this.primaryServer;
			}
		}

		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x06004376 RID: 17270 RVA: 0x000B54FD File Offset: 0x000B36FD
		public string Administrator
		{
			get
			{
				return this.administrator;
			}
		}

		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x06004377 RID: 17271 RVA: 0x000B5505 File Offset: 0x000B3705
		public int SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
		}

		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x06004378 RID: 17272 RVA: 0x000B550D File Offset: 0x000B370D
		public int Refresh
		{
			get
			{
				return this.refresh;
			}
		}

		// Token: 0x170010E4 RID: 4324
		// (get) Token: 0x06004379 RID: 17273 RVA: 0x000B5515 File Offset: 0x000B3715
		public int Retry
		{
			get
			{
				return this.retry;
			}
		}

		// Token: 0x170010E5 RID: 4325
		// (get) Token: 0x0600437A RID: 17274 RVA: 0x000B551D File Offset: 0x000B371D
		public int Expire
		{
			get
			{
				return this.expire;
			}
		}

		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x0600437B RID: 17275 RVA: 0x000B5525 File Offset: 0x000B3725
		public int DefaultTimeToLive
		{
			get
			{
				return this.defaultTimeToLive;
			}
		}

		// Token: 0x0600437C RID: 17276 RVA: 0x000B552D File Offset: 0x000B372D
		public override string ToString()
		{
			return base.ToString() + " " + base.Name;
		}

		// Token: 0x04003963 RID: 14691
		private readonly string primaryServer;

		// Token: 0x04003964 RID: 14692
		private readonly string administrator;

		// Token: 0x04003965 RID: 14693
		private readonly int serialNumber;

		// Token: 0x04003966 RID: 14694
		private readonly int refresh;

		// Token: 0x04003967 RID: 14695
		private readonly int retry;

		// Token: 0x04003968 RID: 14696
		private readonly int expire;

		// Token: 0x04003969 RID: 14697
		private readonly int defaultTimeToLive;

		// Token: 0x02000C08 RID: 3080
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct Win32DnsSoaRecord
		{
			// Token: 0x0400396A RID: 14698
			public readonly string namePrimaryServer;

			// Token: 0x0400396B RID: 14699
			public readonly string nameAdministrator;

			// Token: 0x0400396C RID: 14700
			public readonly int serialNumber;

			// Token: 0x0400396D RID: 14701
			public readonly int refresh;

			// Token: 0x0400396E RID: 14702
			public readonly int retry;

			// Token: 0x0400396F RID: 14703
			public readonly int expire;

			// Token: 0x04003970 RID: 14704
			public readonly int defaultTTL;
		}
	}
}
