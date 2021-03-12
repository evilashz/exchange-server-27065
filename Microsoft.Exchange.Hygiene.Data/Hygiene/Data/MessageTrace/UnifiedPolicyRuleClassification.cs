using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x020001A7 RID: 423
	internal class UnifiedPolicyRuleClassification : ConfigurablePropertyBag
	{
		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x00036AEE File Offset: 0x00034CEE
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(string.Format("{0}\\{1}\\{2}", this.ObjectId, this.RuleId, this.ClassificationId));
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x00036B20 File Offset: 0x00034D20
		public Guid OrganizationalUnitRoot
		{
			get
			{
				return (Guid)this[UnifiedPolicyRuleClassificationSchema.OrganizationalUnitRootProperty];
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x00036B32 File Offset: 0x00034D32
		public Guid ObjectId
		{
			get
			{
				return (Guid)this[UnifiedPolicyRuleClassificationSchema.ObjectIdProperty];
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x00036B44 File Offset: 0x00034D44
		public string DataSource
		{
			get
			{
				return this[UnifiedPolicyRuleClassificationSchema.DataSourceProperty] as string;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x00036B56 File Offset: 0x00034D56
		// (set) Token: 0x060011C6 RID: 4550 RVA: 0x00036B68 File Offset: 0x00034D68
		public Guid RuleId
		{
			get
			{
				return (Guid)this[UnifiedPolicyRuleClassificationSchema.RuleIdProperty];
			}
			set
			{
				this[UnifiedPolicyRuleClassificationSchema.RuleIdProperty] = value;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x00036B7B File Offset: 0x00034D7B
		// (set) Token: 0x060011C8 RID: 4552 RVA: 0x00036B8D File Offset: 0x00034D8D
		public Guid ClassificationId
		{
			get
			{
				return (Guid)this[UnifiedPolicyRuleClassificationSchema.ClassificationIdProperty];
			}
			set
			{
				this[UnifiedPolicyRuleClassificationSchema.ClassificationIdProperty] = value;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x00036BA0 File Offset: 0x00034DA0
		// (set) Token: 0x060011CA RID: 4554 RVA: 0x00036BB2 File Offset: 0x00034DB2
		public int Count
		{
			get
			{
				return (int)this[UnifiedPolicyRuleClassificationSchema.CountProperty];
			}
			set
			{
				this[UnifiedPolicyRuleClassificationSchema.CountProperty] = value;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x00036BC5 File Offset: 0x00034DC5
		// (set) Token: 0x060011CC RID: 4556 RVA: 0x00036BD7 File Offset: 0x00034DD7
		public int Confidence
		{
			get
			{
				return (int)this[UnifiedPolicyRuleClassificationSchema.ConfidenceProperty];
			}
			set
			{
				this[UnifiedPolicyRuleClassificationSchema.ConfidenceProperty] = value;
			}
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00036BEA File Offset: 0x00034DEA
		public override Type GetSchemaType()
		{
			return typeof(UnifiedPolicyRuleClassificationSchema);
		}

		// Token: 0x0400087F RID: 2175
		internal static readonly HygienePropertyDefinition[] Properties = new HygienePropertyDefinition[]
		{
			UnifiedPolicyRuleClassificationSchema.OrganizationalUnitRootProperty,
			UnifiedPolicyRuleClassificationSchema.ObjectIdProperty,
			UnifiedPolicyRuleClassificationSchema.DataSourceProperty,
			UnifiedPolicyRuleClassificationSchema.RuleIdProperty,
			UnifiedPolicyRuleClassificationSchema.ClassificationIdProperty,
			UnifiedPolicyRuleClassificationSchema.CountProperty,
			UnifiedPolicyRuleClassificationSchema.ConfidenceProperty,
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
