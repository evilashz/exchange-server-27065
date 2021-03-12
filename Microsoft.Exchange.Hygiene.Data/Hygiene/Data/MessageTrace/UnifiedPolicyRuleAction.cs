using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x020001A5 RID: 421
	internal class UnifiedPolicyRuleAction : ConfigurablePropertyBag
	{
		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x00036926 File Offset: 0x00034B26
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(string.Format("{0}\\{1}\\{2}", this.ObjectId, this.RuleId, this.Action));
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x00036953 File Offset: 0x00034B53
		public Guid OrganizationalUnitRoot
		{
			get
			{
				return (Guid)this[UnifiedPolicyRuleActionSchema.OrganizationalUnitRootProperty];
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x00036965 File Offset: 0x00034B65
		public Guid ObjectId
		{
			get
			{
				return (Guid)this[UnifiedPolicyRuleActionSchema.ObjectIdProperty];
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x00036977 File Offset: 0x00034B77
		public string DataSource
		{
			get
			{
				return this[UnifiedPolicyRuleActionSchema.DataSourceProperty] as string;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x00036989 File Offset: 0x00034B89
		public Guid RuleId
		{
			get
			{
				return (Guid)this[UnifiedPolicyRuleActionSchema.RuleIdProperty];
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x060011BA RID: 4538 RVA: 0x0003699B File Offset: 0x00034B9B
		// (set) Token: 0x060011BB RID: 4539 RVA: 0x000369AD File Offset: 0x00034BAD
		public string Action
		{
			get
			{
				return this[UnifiedPolicyRuleActionSchema.ActionProperty] as string;
			}
			set
			{
				this[UnifiedPolicyRuleActionSchema.ActionProperty] = value;
			}
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x000369BB File Offset: 0x00034BBB
		public override Type GetSchemaType()
		{
			return typeof(UnifiedPolicyRuleActionSchema);
		}

		// Token: 0x04000879 RID: 2169
		internal static readonly HygienePropertyDefinition[] Properties = new HygienePropertyDefinition[]
		{
			UnifiedPolicyRuleActionSchema.OrganizationalUnitRootProperty,
			UnifiedPolicyRuleActionSchema.ObjectIdProperty,
			UnifiedPolicyRuleActionSchema.DataSourceProperty,
			UnifiedPolicyRuleActionSchema.RuleIdProperty,
			UnifiedPolicyRuleActionSchema.ActionProperty,
			UnifiedPolicyCommonSchema.HashBucketProperty,
			UnifiedPolicyCommonSchema.IntValue1Prop,
			UnifiedPolicyCommonSchema.IntValue2Prop,
			UnifiedPolicyCommonSchema.IntValue3Prop,
			UnifiedPolicyCommonSchema.LongValue1Prop,
			UnifiedPolicyCommonSchema.LongValue2Prop,
			UnifiedPolicyCommonSchema.GuidValue1Prop,
			UnifiedPolicyCommonSchema.GuidValue2Prop,
			UnifiedPolicyCommonSchema.StringValue1Prop,
			UnifiedPolicyCommonSchema.StringValue2Prop,
			UnifiedPolicyCommonSchema.StringValue3Prop,
			UnifiedPolicyCommonSchema.StringValue4Prop,
			UnifiedPolicyCommonSchema.StringValue5Prop,
			UnifiedPolicyCommonSchema.ByteValue1Prop,
			UnifiedPolicyCommonSchema.ByteValue2Prop
		};
	}
}
