using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.ActiveManager;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000046 RID: 70
	[DataContract]
	internal sealed class ServerHealthStatus
	{
		// Token: 0x06000346 RID: 838 RVA: 0x00005D38 File Offset: 0x00003F38
		public ServerHealthStatus()
		{
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00005D40 File Offset: 0x00003F40
		// (set) Token: 0x06000348 RID: 840 RVA: 0x00005D48 File Offset: 0x00003F48
		[DataMember(Name = "HealthState")]
		public ServerHealthState HealthState { get; set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00005D51 File Offset: 0x00003F51
		// (set) Token: 0x0600034A RID: 842 RVA: 0x00005D59 File Offset: 0x00003F59
		[DataMember(Name = "FailureReason", EmitDefaultValue = false)]
		public byte[] FailureReasonData { get; set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00005D62 File Offset: 0x00003F62
		// (set) Token: 0x0600034C RID: 844 RVA: 0x00005D6A File Offset: 0x00003F6A
		[DataMember(Name = "Agent")]
		public int AgentInt { get; set; }

		// Token: 0x0600034D RID: 845 RVA: 0x00005D73 File Offset: 0x00003F73
		public ServerHealthStatus(ServerHealthState healthState)
		{
			this.HealthState = healthState;
			this.Agent = ConstraintCheckAgent.None;
			this.FailureReason = LocalizedString.Empty;
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00005D94 File Offset: 0x00003F94
		// (set) Token: 0x0600034F RID: 847 RVA: 0x00005DA1 File Offset: 0x00003FA1
		public LocalizedString FailureReason
		{
			get
			{
				return CommonUtils.ByteDeserialize(this.FailureReasonData);
			}
			set
			{
				this.FailureReasonData = CommonUtils.ByteSerialize(value);
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00005DAF File Offset: 0x00003FAF
		// (set) Token: 0x06000351 RID: 849 RVA: 0x00005DB7 File Offset: 0x00003FB7
		public ConstraintCheckAgent Agent
		{
			get
			{
				return (ConstraintCheckAgent)this.AgentInt;
			}
			set
			{
				this.AgentInt = (int)value;
			}
		}

		// Token: 0x04000282 RID: 642
		public static ServerHealthStatus Healthy = new ServerHealthStatus(ServerHealthState.Healthy);
	}
}
