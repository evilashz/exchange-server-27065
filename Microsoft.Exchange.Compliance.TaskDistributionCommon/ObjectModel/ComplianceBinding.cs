using System;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel
{
	// Token: 0x02000024 RID: 36
	internal class ComplianceBinding
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00005234 File Offset: 0x00003434
		public ComplianceBinding()
		{
			this.JobStartTime = ComplianceJobConstants.MinComplianceTime;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00005247 File Offset: 0x00003447
		// (set) Token: 0x0600008E RID: 142 RVA: 0x0000524F File Offset: 0x0000344F
		public Guid TenantId { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00005258 File Offset: 0x00003458
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00005260 File Offset: 0x00003460
		public Guid JobRunId { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00005269 File Offset: 0x00003469
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00005271 File Offset: 0x00003471
		public string Bindings { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000527A File Offset: 0x0000347A
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00005282 File Offset: 0x00003482
		public ushort BindingOptions { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000095 RID: 149 RVA: 0x0000528B File Offset: 0x0000348B
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00005293 File Offset: 0x00003493
		internal ComplianceBindingType BindingType { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000097 RID: 151 RVA: 0x0000529C File Offset: 0x0000349C
		// (set) Token: 0x06000098 RID: 152 RVA: 0x000052A4 File Offset: 0x000034A4
		internal DateTime JobStartTime { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000052AD File Offset: 0x000034AD
		// (set) Token: 0x0600009A RID: 154 RVA: 0x000052B5 File Offset: 0x000034B5
		internal string JobMaster { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000052BE File Offset: 0x000034BE
		// (set) Token: 0x0600009C RID: 156 RVA: 0x000052C6 File Offset: 0x000034C6
		internal int NumBindings { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000052CF File Offset: 0x000034CF
		// (set) Token: 0x0600009E RID: 158 RVA: 0x000052D7 File Offset: 0x000034D7
		internal int NumBindingsFailed { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000052E0 File Offset: 0x000034E0
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x000052E8 File Offset: 0x000034E8
		internal ComplianceJobStatus JobStatus { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000052F1 File Offset: 0x000034F1
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x000052F9 File Offset: 0x000034F9
		internal byte[] JobResults { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00005302 File Offset: 0x00003502
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x0000530F File Offset: 0x0000350F
		internal bool AllBindings
		{
			get
			{
				return (this.BindingOptions & 1) == 1;
			}
			set
			{
				if (value)
				{
					this.BindingOptions |= 1;
					return;
				}
				this.BindingOptions &= 65534;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00005337 File Offset: 0x00003537
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x0000534E File Offset: 0x0000354E
		internal string[] BindingsArray
		{
			get
			{
				if (this.Bindings == null)
				{
					return null;
				}
				return Utils.JsonStringToStringArray(this.Bindings);
			}
			set
			{
				if (value == null)
				{
					this.Bindings = null;
					return;
				}
				this.Bindings = Utils.StringArrayToJsonString(value);
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00005368 File Offset: 0x00003568
		internal void CopyFromRow(TempDatabase.ComplianceJobBindingTable row)
		{
			this.TenantId = row.TenantId;
			this.JobRunId = row.JobRunId;
			this.BindingOptions = row.BindingOptions;
			this.Bindings = row.Bindings;
			this.BindingType = row.BindingType;
			this.JobStartTime = row.JobStartTime;
			this.JobResults = row.JobResults;
			this.JobStatus = row.JobStatus;
			this.NumBindings = row.NumberBindings;
			this.NumBindingsFailed = row.NumberBindingsFailed;
			this.JobMaster = row.JobMaster;
		}

		// Token: 0x04000065 RID: 101
		internal const ushort BindingOptionAllBindings = 1;
	}
}
