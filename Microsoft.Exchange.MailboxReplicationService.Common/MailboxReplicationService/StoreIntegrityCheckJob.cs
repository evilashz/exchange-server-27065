using System;
using System.Runtime.Serialization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200004B RID: 75
	[DataContract]
	internal class StoreIntegrityCheckJob
	{
		// Token: 0x060003C6 RID: 966 RVA: 0x00006D1C File Offset: 0x00004F1C
		public StoreIntegrityCheckJob()
		{
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x00006D24 File Offset: 0x00004F24
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x00006D2C File Offset: 0x00004F2C
		[DataMember(Name = "RequestGuid")]
		public Guid RequestGuid { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x00006D35 File Offset: 0x00004F35
		// (set) Token: 0x060003CA RID: 970 RVA: 0x00006D3D File Offset: 0x00004F3D
		[DataMember(Name = "MailboxGuid")]
		public Guid MailboxGuid { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060003CB RID: 971 RVA: 0x00006D46 File Offset: 0x00004F46
		// (set) Token: 0x060003CC RID: 972 RVA: 0x00006D4E File Offset: 0x00004F4E
		[DataMember(Name = "DatabaseGuid")]
		public Guid DatabaseGuid { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00006D57 File Offset: 0x00004F57
		// (set) Token: 0x060003CE RID: 974 RVA: 0x00006D5F File Offset: 0x00004F5F
		[DataMember(Name = "JobGuid")]
		public Guid JobGuid { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060003CF RID: 975 RVA: 0x00006D68 File Offset: 0x00004F68
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x00006D70 File Offset: 0x00004F70
		[DataMember(Name = "JobState")]
		public int JobState { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00006D79 File Offset: 0x00004F79
		// (set) Token: 0x060003D2 RID: 978 RVA: 0x00006D81 File Offset: 0x00004F81
		[DataMember(Name = "Progress")]
		public short Progress { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x00006D8A File Offset: 0x00004F8A
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x00006D92 File Offset: 0x00004F92
		[DataMember(Name = "Priority")]
		public int Priority { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x00006D9B File Offset: 0x00004F9B
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x00006DA3 File Offset: 0x00004FA3
		[DataMember(Name = "CorruptionsDetected")]
		public int CorruptionsDetected { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00006DAC File Offset: 0x00004FAC
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x00006DB4 File Offset: 0x00004FB4
		[DataMember(Name = "CorruptionsFixed")]
		public int CorruptionsFixed { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00006DBD File Offset: 0x00004FBD
		// (set) Token: 0x060003DA RID: 986 RVA: 0x00006DC5 File Offset: 0x00004FC5
		[DataMember(Name = "CreationTime")]
		public DateTime? CreationTime { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060003DB RID: 987 RVA: 0x00006DCE File Offset: 0x00004FCE
		// (set) Token: 0x060003DC RID: 988 RVA: 0x00006DD6 File Offset: 0x00004FD6
		[DataMember(Name = "FinishTime")]
		public DateTime? FinishTime { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00006DDF File Offset: 0x00004FDF
		// (set) Token: 0x060003DE RID: 990 RVA: 0x00006DE7 File Offset: 0x00004FE7
		[DataMember(Name = "LastExecutionTime")]
		public DateTime? LastExecutionTime { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00006DF0 File Offset: 0x00004FF0
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x00006DF8 File Offset: 0x00004FF8
		[DataMember(Name = "TimeInServer")]
		public TimeSpan? TimeInServer { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00006E01 File Offset: 0x00005001
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x00006E09 File Offset: 0x00005009
		[DataMember(Name = "ErrorCode")]
		public int? ErrorCode { get; set; }

		// Token: 0x060003E3 RID: 995 RVA: 0x00006E14 File Offset: 0x00005014
		internal StoreIntegrityCheckJob(PropValue[] propValues)
		{
			foreach (PropValue propValue in propValues)
			{
				if (!propValue.IsError() && !propValue.IsNull())
				{
					PropTag propTag = propValue.PropTag;
					if (propTag <= PropTag.IsIntegJobLastExecutionTime)
					{
						if (propTag <= PropTag.IsIntegJobTask)
						{
							if (propTag <= PropTag.IsIntegJobGuid)
							{
								if (propTag != PropTag.IsIntegJobMailboxGuid)
								{
									if (propTag == PropTag.IsIntegJobGuid)
									{
										this.JobGuid = propValue.GetGuid();
									}
								}
								else
								{
									this.MailboxGuid = propValue.GetGuid();
								}
							}
							else if (propTag != PropTag.IsIntegJobFlags && propTag != PropTag.IsIntegJobTask)
							{
							}
						}
						else if (propTag <= PropTag.IsIntegJobCreationTime)
						{
							if (propTag != PropTag.IsIntegJobState)
							{
								if (propTag == PropTag.IsIntegJobCreationTime)
								{
									this.CreationTime = new DateTime?(propValue.GetDateTime().ToLocalTime());
								}
							}
							else
							{
								this.JobState = (int)propValue.GetShort();
							}
						}
						else if (propTag != PropTag.IsIntegJobFinishTime)
						{
							if (propTag == PropTag.IsIntegJobLastExecutionTime)
							{
								this.LastExecutionTime = new DateTime?(propValue.GetDateTime().ToLocalTime());
							}
						}
						else
						{
							this.FinishTime = new DateTime?(propValue.GetDateTime().ToLocalTime());
						}
					}
					else if (propTag <= PropTag.IsIntegJobProgress)
					{
						if (propTag <= PropTag.IsIntegJobCorruptionsFixed)
						{
							if (propTag != PropTag.IsIntegJobCorruptionsDetected)
							{
								if (propTag == PropTag.IsIntegJobCorruptionsFixed)
								{
									this.CorruptionsFixed = propValue.GetInt();
								}
							}
							else
							{
								this.CorruptionsDetected = propValue.GetInt();
							}
						}
						else if (propTag != PropTag.IsIntegJobRequestGuid)
						{
							if (propTag == PropTag.IsIntegJobProgress)
							{
								this.Progress = propValue.GetShort();
							}
						}
						else
						{
							this.RequestGuid = propValue.GetGuid();
						}
					}
					else if (propTag <= PropTag.IsIntegJobSource)
					{
						if (propTag != PropTag.IsIntegJobCorruptions && propTag != PropTag.IsIntegJobSource)
						{
						}
					}
					else if (propTag != PropTag.IsIntegJobPriority)
					{
						if (propTag != PropTag.IsIntegJobTimeInServer)
						{
							if (propTag == PropTag.RtfSyncTrailingCount)
							{
								this.ErrorCode = new int?(propValue.GetInt());
							}
						}
						else
						{
							this.TimeInServer = new TimeSpan?(TimeSpan.FromMilliseconds(propValue.GetDouble()));
						}
					}
					else
					{
						this.Priority = (int)propValue.GetShort();
					}
				}
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00007094 File Offset: 0x00005294
		public override string ToString()
		{
			return string.Format("RequestGuid: {0}, MailboxGuid: {1}, DatabaseGuid: {2}, JobGuid: {3}, JobState: {4}, Progress: {5}, Priority: {6}, CorruptionsDetected: {7}, CorruptionsFixed: {8}, CreationTime: {9}, FinishTime: {10}, LastExecutionTime: {11}, TimeInServer: {12}, ErrorCode: {13}", new object[]
			{
				this.RequestGuid,
				this.MailboxGuid,
				this.DatabaseGuid,
				this.JobGuid,
				this.JobState,
				this.Progress,
				this.Priority,
				this.CorruptionsDetected,
				this.CorruptionsFixed,
				(this.CreationTime != null) ? this.CreationTime.ToString() : "<null>",
				(this.FinishTime != null) ? this.FinishTime.ToString() : "<null>",
				(this.LastExecutionTime != null) ? this.LastExecutionTime.ToString() : "<null>",
				(this.TimeInServer != null) ? this.TimeInServer.ToString() : "<null>",
				(this.ErrorCode != null) ? this.ErrorCode.ToString() : "<null>"
			});
		}
	}
}
