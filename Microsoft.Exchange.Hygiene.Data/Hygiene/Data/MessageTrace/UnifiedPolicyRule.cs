using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x020001A4 RID: 420
	internal class UnifiedPolicyRule : ConfigurablePropertyBag
	{
		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x0600119E RID: 4510 RVA: 0x000365B1 File Offset: 0x000347B1
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(string.Format("{0}\\{1}", this.ObjectId, this.RuleId));
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x000365D8 File Offset: 0x000347D8
		public Guid OrganizationalUnitRoot
		{
			get
			{
				return (Guid)this[UnifiedPolicyRuleSchema.OrganizationalUnitRootProperty];
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x000365EA File Offset: 0x000347EA
		public Guid ObjectId
		{
			get
			{
				return (Guid)this[UnifiedPolicyRuleSchema.ObjectIdProperty];
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x060011A1 RID: 4513 RVA: 0x000365FC File Offset: 0x000347FC
		public string DataSource
		{
			get
			{
				return this[UnifiedPolicyRuleSchema.DataSourceProperty] as string;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x0003660E File Offset: 0x0003480E
		// (set) Token: 0x060011A3 RID: 4515 RVA: 0x00036620 File Offset: 0x00034820
		public Guid RuleId
		{
			get
			{
				return (Guid)this[UnifiedPolicyRuleSchema.RuleIdProperty];
			}
			set
			{
				this[UnifiedPolicyRuleSchema.RuleIdProperty] = value;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x060011A4 RID: 4516 RVA: 0x00036633 File Offset: 0x00034833
		// (set) Token: 0x060011A5 RID: 4517 RVA: 0x0003664A File Offset: 0x0003484A
		public Guid? DLPId
		{
			get
			{
				return new Guid?((Guid)this[UnifiedPolicyRuleSchema.DLPIdProperty]);
			}
			set
			{
				this[UnifiedPolicyRuleSchema.DLPIdProperty] = value;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x060011A6 RID: 4518 RVA: 0x0003665D File Offset: 0x0003485D
		// (set) Token: 0x060011A7 RID: 4519 RVA: 0x0003666F File Offset: 0x0003486F
		public string Mode
		{
			get
			{
				return this[UnifiedPolicyRuleSchema.ModeProperty] as string;
			}
			set
			{
				this[UnifiedPolicyRuleSchema.ModeProperty] = value;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x060011A8 RID: 4520 RVA: 0x0003667D File Offset: 0x0003487D
		// (set) Token: 0x060011A9 RID: 4521 RVA: 0x0003668F File Offset: 0x0003488F
		public string Severity
		{
			get
			{
				return this[UnifiedPolicyRuleSchema.SeverityProperty] as string;
			}
			set
			{
				this[UnifiedPolicyRuleSchema.SeverityProperty] = value;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x060011AA RID: 4522 RVA: 0x0003669D File Offset: 0x0003489D
		// (set) Token: 0x060011AB RID: 4523 RVA: 0x000366AF File Offset: 0x000348AF
		public string OverrideType
		{
			get
			{
				return this[UnifiedPolicyRuleSchema.OverrideTypeProperty] as string;
			}
			set
			{
				this[UnifiedPolicyRuleSchema.OverrideTypeProperty] = value;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x060011AC RID: 4524 RVA: 0x000366BD File Offset: 0x000348BD
		// (set) Token: 0x060011AD RID: 4525 RVA: 0x000366CF File Offset: 0x000348CF
		public string OverrideJustification
		{
			get
			{
				return this[UnifiedPolicyRuleSchema.OverrideJustificationProperty] as string;
			}
			set
			{
				this[UnifiedPolicyRuleSchema.OverrideJustificationProperty] = value;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x000366E0 File Offset: 0x000348E0
		public List<UnifiedPolicyRuleAction> Actions
		{
			get
			{
				List<UnifiedPolicyRuleAction> result;
				if ((result = this.actions) == null)
				{
					result = (this.actions = new List<UnifiedPolicyRuleAction>());
				}
				return result;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x00036708 File Offset: 0x00034908
		public List<UnifiedPolicyRuleClassification> Classifications
		{
			get
			{
				List<UnifiedPolicyRuleClassification> result;
				if ((result = this.classifications) == null)
				{
					result = (this.classifications = new List<UnifiedPolicyRuleClassification>());
				}
				return result;
			}
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x00036730 File Offset: 0x00034930
		public void Add(UnifiedPolicyRuleAction action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			action[UnifiedPolicyRuleSchema.OrganizationalUnitRootProperty] = this.OrganizationalUnitRoot;
			action[UnifiedPolicyRuleSchema.ObjectIdProperty] = this.ObjectId;
			action[UnifiedPolicyRuleSchema.DataSourceProperty] = this.DataSource;
			action[UnifiedPolicyRuleSchema.RuleIdProperty] = this.RuleId;
			this.Actions.Add(action);
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x000367AC File Offset: 0x000349AC
		public void Add(UnifiedPolicyRuleClassification classification)
		{
			if (classification == null)
			{
				throw new ArgumentNullException("classification");
			}
			classification[UnifiedPolicyRuleClassificationSchema.OrganizationalUnitRootProperty] = this.OrganizationalUnitRoot;
			classification[UnifiedPolicyRuleClassificationSchema.ObjectIdProperty] = this.ObjectId;
			classification[UnifiedPolicyRuleClassificationSchema.DataSourceProperty] = this.DataSource;
			classification[UnifiedPolicyRuleClassificationSchema.RuleIdProperty] = this.RuleId;
			this.Classifications.Add(classification);
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00036826 File Offset: 0x00034A26
		public override Type GetSchemaType()
		{
			return typeof(UnifiedPolicyRuleSchema);
		}

		// Token: 0x04000876 RID: 2166
		internal static readonly HygienePropertyDefinition[] Properties = new HygienePropertyDefinition[]
		{
			UnifiedPolicyRuleSchema.OrganizationalUnitRootProperty,
			UnifiedPolicyRuleSchema.ObjectIdProperty,
			UnifiedPolicyRuleSchema.DataSourceProperty,
			UnifiedPolicyRuleSchema.RuleIdProperty,
			UnifiedPolicyRuleSchema.DLPIdProperty,
			UnifiedPolicyRuleSchema.ModeProperty,
			UnifiedPolicyRuleSchema.SeverityProperty,
			UnifiedPolicyRuleSchema.OverrideTypeProperty,
			UnifiedPolicyRuleSchema.OverrideJustificationProperty,
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

		// Token: 0x04000877 RID: 2167
		private List<UnifiedPolicyRuleAction> actions;

		// Token: 0x04000878 RID: 2168
		private List<UnifiedPolicyRuleClassification> classifications;
	}
}
