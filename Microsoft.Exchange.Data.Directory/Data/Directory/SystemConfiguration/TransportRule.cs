using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005DC RID: 1500
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class TransportRule : ADConfigurationObject
	{
		// Token: 0x06004565 RID: 17765 RVA: 0x001022B0 File Offset: 0x001004B0
		public TransportRule()
		{
			this.WhenChangedUTCCopy = null;
		}

		// Token: 0x06004566 RID: 17766 RVA: 0x001022D4 File Offset: 0x001004D4
		public TransportRule(string ruleName, ADObjectId ruleCollectionId)
		{
			this.WhenChangedUTCCopy = null;
			base.SetId(ruleCollectionId.GetChildId(ruleName));
		}

		// Token: 0x170016EA RID: 5866
		// (get) Token: 0x06004567 RID: 17767 RVA: 0x00102304 File Offset: 0x00100504
		// (set) Token: 0x06004568 RID: 17768 RVA: 0x00102334 File Offset: 0x00100534
		public DateTime? WhenChangedUTCCopy
		{
			get
			{
				DateTime? whenChangedUTC = base.WhenChangedUTC;
				if (whenChangedUTC == null)
				{
					return this.whenChangedUTCCopy;
				}
				return new DateTime?(whenChangedUTC.GetValueOrDefault());
			}
			set
			{
				this.whenChangedUTCCopy = value;
			}
		}

		// Token: 0x170016EB RID: 5867
		// (get) Token: 0x06004569 RID: 17769 RVA: 0x0010233D File Offset: 0x0010053D
		internal override ADObjectSchema Schema
		{
			get
			{
				return TransportRule.schema;
			}
		}

		// Token: 0x170016EC RID: 5868
		// (get) Token: 0x0600456A RID: 17770 RVA: 0x00102344 File Offset: 0x00100544
		internal override string MostDerivedObjectClass
		{
			get
			{
				return TransportRule.mostDerivedClass;
			}
		}

		// Token: 0x170016ED RID: 5869
		// (get) Token: 0x0600456B RID: 17771 RVA: 0x0010234B File Offset: 0x0010054B
		// (set) Token: 0x0600456C RID: 17772 RVA: 0x0010235D File Offset: 0x0010055D
		public string Xml
		{
			get
			{
				return (string)this[TransportRuleSchema.Xml];
			}
			internal set
			{
				this[TransportRuleSchema.Xml] = value;
			}
		}

		// Token: 0x170016EE RID: 5870
		// (get) Token: 0x0600456D RID: 17773 RVA: 0x0010236B File Offset: 0x0010056B
		// (set) Token: 0x0600456E RID: 17774 RVA: 0x0010237D File Offset: 0x0010057D
		public int Priority
		{
			get
			{
				return (int)this[TransportRuleSchema.Priority];
			}
			set
			{
				this[TransportRuleSchema.Priority] = value;
			}
		}

		// Token: 0x170016EF RID: 5871
		// (get) Token: 0x0600456F RID: 17775 RVA: 0x00102390 File Offset: 0x00100590
		// (set) Token: 0x06004570 RID: 17776 RVA: 0x001023D7 File Offset: 0x001005D7
		public Guid ImmutableId
		{
			get
			{
				Guid guid = (Guid)this[TransportRuleSchema.ImmutableId];
				if (guid == Guid.Empty)
				{
					guid = ((base.Id == null) ? Guid.Empty : base.Id.ObjectGuid);
				}
				return guid;
			}
			internal set
			{
				this[TransportRuleSchema.ImmutableId] = value;
			}
		}

		// Token: 0x04002FA7 RID: 12199
		private static TransportRuleSchema schema = ObjectSchema.GetInstance<TransportRuleSchema>();

		// Token: 0x04002FA8 RID: 12200
		private static string mostDerivedClass = "msExchTransportRule";

		// Token: 0x04002FA9 RID: 12201
		private DateTime? whenChangedUTCCopy;
	}
}
