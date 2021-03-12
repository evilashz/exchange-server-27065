using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000554 RID: 1364
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Serializable]
	public class ReplicationCheckOutputObject : ConfigurableObject
	{
		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x06003092 RID: 12434 RVA: 0x000C48FC File Offset: 0x000C2AFC
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ReplicationCheckOutputObject.schema;
			}
		}

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x06003093 RID: 12435 RVA: 0x000C4903 File Offset: 0x000C2B03
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x06003094 RID: 12436 RVA: 0x000C490A File Offset: 0x000C2B0A
		// (set) Token: 0x06003095 RID: 12437 RVA: 0x000C491C File Offset: 0x000C2B1C
		public string Server
		{
			get
			{
				return (string)this[ReplicationCheckOutputObjectSchema.Server];
			}
			private set
			{
				this[ReplicationCheckOutputObjectSchema.Server] = value;
			}
		}

		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x06003096 RID: 12438 RVA: 0x000C492A File Offset: 0x000C2B2A
		// (set) Token: 0x06003097 RID: 12439 RVA: 0x000C493C File Offset: 0x000C2B3C
		public CheckId CheckId
		{
			get
			{
				return (CheckId)this[ReplicationCheckOutputObjectSchema.CheckIdProperty];
			}
			private set
			{
				this[ReplicationCheckOutputObjectSchema.CheckIdProperty] = value;
			}
		}

		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x06003098 RID: 12440 RVA: 0x000C494F File Offset: 0x000C2B4F
		// (set) Token: 0x06003099 RID: 12441 RVA: 0x000C4961 File Offset: 0x000C2B61
		internal string Check
		{
			get
			{
				return (string)this[ReplicationCheckOutputObjectSchema.Check];
			}
			private set
			{
				this[ReplicationCheckOutputObjectSchema.Check] = value;
			}
		}

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x0600309A RID: 12442 RVA: 0x000C496F File Offset: 0x000C2B6F
		// (set) Token: 0x0600309B RID: 12443 RVA: 0x000C4981 File Offset: 0x000C2B81
		public new string Identity
		{
			get
			{
				return (string)this[ReplicationCheckOutputObjectSchema.IdentityProperty];
			}
			private set
			{
				this[ReplicationCheckOutputObjectSchema.IdentityProperty] = value;
			}
		}

		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x0600309C RID: 12444 RVA: 0x000C498F File Offset: 0x000C2B8F
		// (set) Token: 0x0600309D RID: 12445 RVA: 0x000C49A1 File Offset: 0x000C2BA1
		public uint? DbFailureEventId
		{
			get
			{
				return (uint?)this[ReplicationCheckOutputObjectSchema.DbFailureEventId];
			}
			private set
			{
				this[ReplicationCheckOutputObjectSchema.DbFailureEventId] = value;
			}
		}

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x0600309E RID: 12446 RVA: 0x000C49B4 File Offset: 0x000C2BB4
		// (set) Token: 0x0600309F RID: 12447 RVA: 0x000C49C6 File Offset: 0x000C2BC6
		public ReplicationCheckResult Result
		{
			get
			{
				return (ReplicationCheckResult)this[ReplicationCheckOutputObjectSchema.Result];
			}
			private set
			{
				this[ReplicationCheckOutputObjectSchema.Result] = value;
			}
		}

		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x060030A0 RID: 12448 RVA: 0x000C49D4 File Offset: 0x000C2BD4
		// (set) Token: 0x060030A1 RID: 12449 RVA: 0x000C49E6 File Offset: 0x000C2BE6
		public string Error
		{
			get
			{
				return (string)this[ReplicationCheckOutputObjectSchema.Error];
			}
			private set
			{
				this[ReplicationCheckOutputObjectSchema.Error] = value;
			}
		}

		// Token: 0x060030A2 RID: 12450 RVA: 0x000C49F4 File Offset: 0x000C2BF4
		internal ReplicationCheckOutputObject(ReplicationCheck check) : base(new SimpleProviderPropertyBag())
		{
			this.Server = check.ServerName;
			this.Check = check.Title;
			this.CheckId = check.CheckId;
			this.Result = new ReplicationCheckResult(ReplicationCheckResultEnum.Undefined);
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x000C4A34 File Offset: 0x000C2C34
		internal void Update(string instanceIdentity, ReplicationCheckResult newResult, string newErrorMessage)
		{
			this.Update(instanceIdentity, newResult, newErrorMessage, null);
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x000C4A53 File Offset: 0x000C2C53
		internal void Update(string instanceIdentity, ReplicationCheckResult newResult, string newErrorMessage, uint? dbFailureEventId)
		{
			this.Identity = instanceIdentity;
			this.Result = newResult;
			this.Error = newErrorMessage;
			this.DbFailureEventId = dbFailureEventId;
		}

		// Token: 0x04002287 RID: 8839
		private static ReplicationCheckOutputObjectSchema schema = ObjectSchema.GetInstance<ReplicationCheckOutputObjectSchema>();
	}
}
