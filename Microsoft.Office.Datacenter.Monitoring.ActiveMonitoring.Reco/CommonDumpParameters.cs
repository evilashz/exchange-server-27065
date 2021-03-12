using System;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000003 RID: 3
	public class CommonDumpParameters : ICloneable
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000024C8 File Offset: 0x000006C8
		public CommonDumpParameters()
		{
			this.IgnoreRegistryOverride = false;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000024D7 File Offset: 0x000006D7
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000024DF File Offset: 0x000006DF
		public DumpMode Mode { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000024E8 File Offset: 0x000006E8
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000024F0 File Offset: 0x000006F0
		public string Path { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000024F9 File Offset: 0x000006F9
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002501 File Offset: 0x00000701
		public double MinimumFreeSpace { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000250A File Offset: 0x0000070A
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002512 File Offset: 0x00000712
		public int MaximumDurationInSeconds { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000251B File Offset: 0x0000071B
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002523 File Offset: 0x00000723
		public bool IgnoreRegistryOverride { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000252C File Offset: 0x0000072C
		public bool IsDumpRequested
		{
			get
			{
				return this.Mode != DumpMode.None;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000253A File Offset: 0x0000073A
		public CommonDumpParameters Clone()
		{
			return (CommonDumpParameters)base.MemberwiseClone();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002547 File Offset: 0x00000747
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
