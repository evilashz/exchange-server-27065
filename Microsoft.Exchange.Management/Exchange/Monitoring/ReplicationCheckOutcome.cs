using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000552 RID: 1362
	[Serializable]
	public sealed class ReplicationCheckOutcome : ConfigurableObject
	{
		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x06003080 RID: 12416 RVA: 0x000C465F File Offset: 0x000C285F
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ReplicationCheckOutcome.schema;
			}
		}

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x06003081 RID: 12417 RVA: 0x000C4666 File Offset: 0x000C2866
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x06003082 RID: 12418 RVA: 0x000C466D File Offset: 0x000C286D
		// (set) Token: 0x06003083 RID: 12419 RVA: 0x000C467F File Offset: 0x000C287F
		public string Server
		{
			get
			{
				return (string)this[ReplicationCheckOutcomeSchema.Server];
			}
			private set
			{
				this[ReplicationCheckOutcomeSchema.Server] = value;
			}
		}

		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x06003084 RID: 12420 RVA: 0x000C468D File Offset: 0x000C288D
		// (set) Token: 0x06003085 RID: 12421 RVA: 0x000C469F File Offset: 0x000C289F
		public string Check
		{
			get
			{
				return (string)this[ReplicationCheckOutcomeSchema.Check];
			}
			private set
			{
				this[ReplicationCheckOutcomeSchema.Check] = value;
			}
		}

		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x06003086 RID: 12422 RVA: 0x000C46AD File Offset: 0x000C28AD
		// (set) Token: 0x06003087 RID: 12423 RVA: 0x000C46BF File Offset: 0x000C28BF
		public string CheckDescription
		{
			get
			{
				return (string)this[ReplicationCheckOutcomeSchema.CheckDescription];
			}
			private set
			{
				this[ReplicationCheckOutcomeSchema.CheckDescription] = value;
			}
		}

		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x06003088 RID: 12424 RVA: 0x000C46CD File Offset: 0x000C28CD
		// (set) Token: 0x06003089 RID: 12425 RVA: 0x000C46DF File Offset: 0x000C28DF
		public ReplicationCheckResult Result
		{
			get
			{
				return (ReplicationCheckResult)this[ReplicationCheckOutcomeSchema.Result];
			}
			private set
			{
				this[ReplicationCheckOutcomeSchema.Result] = value;
			}
		}

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x0600308A RID: 12426 RVA: 0x000C46ED File Offset: 0x000C28ED
		// (set) Token: 0x0600308B RID: 12427 RVA: 0x000C46FF File Offset: 0x000C28FF
		public string Error
		{
			get
			{
				return (string)this[ReplicationCheckOutcomeSchema.Error];
			}
			private set
			{
				this[ReplicationCheckOutcomeSchema.Error] = value;
			}
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x000C470D File Offset: 0x000C290D
		internal ReplicationCheckOutcome(string serverName, string checktitle, string checkdescription, ReplicationCheckResult result1, string errorMsg) : base(new SimpleProviderPropertyBag())
		{
			this.Server = serverName;
			this.Check = checktitle;
			this.CheckDescription = checkdescription;
			this.Result = result1;
			this.Error = errorMsg;
		}

		// Token: 0x0600308D RID: 12429 RVA: 0x000C4740 File Offset: 0x000C2940
		internal ReplicationCheckOutcome(ReplicationCheck check) : base(new SimpleProviderPropertyBag())
		{
			this.Server = check.ServerName;
			this.Check = check.Title;
			this.CheckDescription = check.Description;
			this.Result = new ReplicationCheckResult(ReplicationCheckResultEnum.Undefined);
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x000C478D File Offset: 0x000C298D
		internal void Update(ReplicationCheckResult newResult, string newErrorMessage)
		{
			this.Result = newResult;
			this.Error = newErrorMessage;
		}

		// Token: 0x0400227F RID: 8831
		private static ReplicationCheckOutcomeSchema schema = ObjectSchema.GetInstance<ReplicationCheckOutcomeSchema>();
	}
}
