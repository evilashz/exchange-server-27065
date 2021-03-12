using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x020001AA RID: 426
	internal class UnifiedPolicyTrace : ConfigurablePropertyBag, IComparable<UnifiedPolicyTrace>
	{
		// Token: 0x060011D4 RID: 4564 RVA: 0x00036E39 File Offset: 0x00035039
		public UnifiedPolicyTrace(Guid organizationalUnitRoot, Guid objectId)
		{
			this.OrganizationalUnitRoot = organizationalUnitRoot;
			this.ObjectId = objectId;
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x00036E4F File Offset: 0x0003504F
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(string.Format("{0}\\{1}", this.OrganizationalUnitRoot, this.ObjectId));
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x060011D6 RID: 4566 RVA: 0x00036E76 File Offset: 0x00035076
		// (set) Token: 0x060011D7 RID: 4567 RVA: 0x00036E88 File Offset: 0x00035088
		public Guid OrganizationalUnitRoot
		{
			get
			{
				return (Guid)this[UnifiedPolicyTraceSchema.OrganizationalUnitRootProperty];
			}
			set
			{
				this[UnifiedPolicyTraceSchema.OrganizationalUnitRootProperty] = value;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x00036E9B File Offset: 0x0003509B
		// (set) Token: 0x060011D9 RID: 4569 RVA: 0x00036EAD File Offset: 0x000350AD
		public Guid ObjectId
		{
			get
			{
				return (Guid)this[UnifiedPolicyTraceSchema.ObjectIdProperty];
			}
			set
			{
				this[UnifiedPolicyTraceSchema.ObjectIdProperty] = value;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x060011DA RID: 4570 RVA: 0x00036EC0 File Offset: 0x000350C0
		// (set) Token: 0x060011DB RID: 4571 RVA: 0x00036ED2 File Offset: 0x000350D2
		public string DataSource
		{
			get
			{
				return this[UnifiedPolicyTraceSchema.DataSourceProperty] as string;
			}
			set
			{
				this[UnifiedPolicyTraceSchema.DataSourceProperty] = value;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x00036EE0 File Offset: 0x000350E0
		// (set) Token: 0x060011DD RID: 4573 RVA: 0x00036EF2 File Offset: 0x000350F2
		public Guid FileId
		{
			get
			{
				return (Guid)this[UnifiedPolicyTraceSchema.FileIdProperty];
			}
			set
			{
				this[UnifiedPolicyTraceSchema.FileIdProperty] = value;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x00036F05 File Offset: 0x00035105
		// (set) Token: 0x060011DF RID: 4575 RVA: 0x00036F17 File Offset: 0x00035117
		public string FileName
		{
			get
			{
				return this[UnifiedPolicyTraceSchema.FileNameProperty] as string;
			}
			set
			{
				this[UnifiedPolicyTraceSchema.FileNameProperty] = value;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x00036F25 File Offset: 0x00035125
		// (set) Token: 0x060011E1 RID: 4577 RVA: 0x00036F37 File Offset: 0x00035137
		public long Size
		{
			get
			{
				return (long)this[UnifiedPolicyTraceSchema.SizeProperty];
			}
			set
			{
				this[UnifiedPolicyTraceSchema.SizeProperty] = value;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x00036F4A File Offset: 0x0003514A
		// (set) Token: 0x060011E3 RID: 4579 RVA: 0x00036F5C File Offset: 0x0003515C
		public Guid SiteId
		{
			get
			{
				return (Guid)this[UnifiedPolicyTraceSchema.SiteIdProperty];
			}
			set
			{
				this[UnifiedPolicyTraceSchema.SiteIdProperty] = value;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x00036F6F File Offset: 0x0003516F
		// (set) Token: 0x060011E5 RID: 4581 RVA: 0x00036F81 File Offset: 0x00035181
		public string FileUrl
		{
			get
			{
				return this[UnifiedPolicyTraceSchema.FileUrlProperty] as string;
			}
			set
			{
				this[UnifiedPolicyTraceSchema.FileUrlProperty] = value;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x00036F8F File Offset: 0x0003518F
		// (set) Token: 0x060011E7 RID: 4583 RVA: 0x00036FA1 File Offset: 0x000351A1
		public string Owner
		{
			get
			{
				return this[UnifiedPolicyTraceSchema.OwnerProperty] as string;
			}
			set
			{
				this[UnifiedPolicyTraceSchema.OwnerProperty] = value;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x00036FAF File Offset: 0x000351AF
		// (set) Token: 0x060011E9 RID: 4585 RVA: 0x00036FC1 File Offset: 0x000351C1
		public bool IsViewableByExternalUsers
		{
			get
			{
				return (bool)this[UnifiedPolicyTraceSchema.IsViewableByExternalUsersProperty];
			}
			set
			{
				this[UnifiedPolicyTraceSchema.IsViewableByExternalUsersProperty] = value;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x00036FD4 File Offset: 0x000351D4
		// (set) Token: 0x060011EB RID: 4587 RVA: 0x00036FE6 File Offset: 0x000351E6
		public string LastModifiedBy
		{
			get
			{
				return this[UnifiedPolicyTraceSchema.LastModifiedByProperty] as string;
			}
			set
			{
				this[UnifiedPolicyTraceSchema.LastModifiedByProperty] = value;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060011EC RID: 4588 RVA: 0x00036FF4 File Offset: 0x000351F4
		// (set) Token: 0x060011ED RID: 4589 RVA: 0x00037006 File Offset: 0x00035206
		public DateTime CreateTime
		{
			get
			{
				return (DateTime)this[UnifiedPolicyTraceSchema.CreateTimeProperty];
			}
			set
			{
				this[UnifiedPolicyTraceSchema.CreateTimeProperty] = value;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x00037019 File Offset: 0x00035219
		// (set) Token: 0x060011EF RID: 4591 RVA: 0x0003702B File Offset: 0x0003522B
		public DateTime LastModifiedTime
		{
			get
			{
				return (DateTime)this[UnifiedPolicyTraceSchema.LastModifiedTimeProperty];
			}
			set
			{
				this[UnifiedPolicyTraceSchema.LastModifiedTimeProperty] = value;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x0003703E File Offset: 0x0003523E
		// (set) Token: 0x060011F1 RID: 4593 RVA: 0x00037050 File Offset: 0x00035250
		public DateTime PolicyMatchTime
		{
			get
			{
				return (DateTime)this[UnifiedPolicyTraceSchema.PolicyMatchTimeProperty];
			}
			set
			{
				this[UnifiedPolicyTraceSchema.PolicyMatchTimeProperty] = value;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x00037064 File Offset: 0x00035264
		public List<UnifiedPolicyRule> Rules
		{
			get
			{
				List<UnifiedPolicyRule> result;
				if ((result = this.rules) == null)
				{
					result = (this.rules = new List<UnifiedPolicyRule>());
				}
				return result;
			}
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x0003708C File Offset: 0x0003528C
		public void Add(UnifiedPolicyRule rule)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			rule[UnifiedPolicyRuleSchema.OrganizationalUnitRootProperty] = this.OrganizationalUnitRoot;
			rule[UnifiedPolicyRuleSchema.ObjectIdProperty] = this.ObjectId;
			rule[UnifiedPolicyRuleSchema.DataSourceProperty] = this.DataSource;
			this.Rules.Add(rule);
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x000370F0 File Offset: 0x000352F0
		public override Type GetSchemaType()
		{
			return typeof(UnifiedPolicyTraceSchema);
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x000370FC File Offset: 0x000352FC
		public int CompareTo(UnifiedPolicyTrace other)
		{
			if (other == null)
			{
				return 1;
			}
			int num = 0;
			byte[] array = this.ObjectId.ToByteArray();
			byte[] array2 = other.ObjectId.ToByteArray();
			int num2 = 10;
			while (num == 0 && num2 < 16)
			{
				num = array[num2].CompareTo(array2[num2]);
				num2++;
			}
			return num;
		}

		// Token: 0x04000890 RID: 2192
		internal static readonly HygienePropertyDefinition[] Properties = new HygienePropertyDefinition[]
		{
			UnifiedPolicyTraceSchema.OrganizationalUnitRootProperty,
			UnifiedPolicyTraceSchema.ObjectIdProperty,
			UnifiedPolicyTraceSchema.DataSourceProperty,
			UnifiedPolicyTraceSchema.FileIdProperty,
			UnifiedPolicyTraceSchema.FileNameProperty,
			UnifiedPolicyTraceSchema.SizeProperty,
			UnifiedPolicyTraceSchema.SiteIdProperty,
			UnifiedPolicyTraceSchema.FileUrlProperty,
			UnifiedPolicyTraceSchema.OwnerProperty,
			UnifiedPolicyTraceSchema.IsViewableByExternalUsersProperty,
			UnifiedPolicyTraceSchema.LastModifiedByProperty,
			UnifiedPolicyTraceSchema.CreateTimeProperty,
			UnifiedPolicyTraceSchema.LastModifiedTimeProperty,
			UnifiedPolicyTraceSchema.PolicyMatchTimeProperty,
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

		// Token: 0x04000891 RID: 2193
		private List<UnifiedPolicyRule> rules;
	}
}
