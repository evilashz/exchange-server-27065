using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200048D RID: 1165
	[Serializable]
	public class InterceptorRule : ADConfigurationObject
	{
		// Token: 0x060034C5 RID: 13509 RVA: 0x000D1E2C File Offset: 0x000D002C
		public InterceptorRule()
		{
		}

		// Token: 0x060034C6 RID: 13510 RVA: 0x000D1E34 File Offset: 0x000D0034
		public InterceptorRule(string ruleName)
		{
			base.SetId(InterceptorRule.InterceptorRulesContainer.GetChildId(ruleName));
		}

		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x060034C7 RID: 13511 RVA: 0x000D1E4D File Offset: 0x000D004D
		internal override ADObjectSchema Schema
		{
			get
			{
				return InterceptorRule.schema;
			}
		}

		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x060034C8 RID: 13512 RVA: 0x000D1E54 File Offset: 0x000D0054
		internal override string MostDerivedObjectClass
		{
			get
			{
				return InterceptorRule.mostDerivedClass;
			}
		}

		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x060034C9 RID: 13513 RVA: 0x000D1E5B File Offset: 0x000D005B
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x060034CA RID: 13514 RVA: 0x000D1E62 File Offset: 0x000D0062
		internal override ADObjectId ParentPath
		{
			get
			{
				return InterceptorRule.InterceptorRulesContainer;
			}
		}

		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x060034CB RID: 13515 RVA: 0x000D1E69 File Offset: 0x000D0069
		// (set) Token: 0x060034CC RID: 13516 RVA: 0x000D1E7B File Offset: 0x000D007B
		public string Xml
		{
			get
			{
				return (string)this[InterceptorRuleSchema.Xml];
			}
			internal set
			{
				this[InterceptorRuleSchema.Xml] = value;
			}
		}

		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x060034CD RID: 13517 RVA: 0x000D1E89 File Offset: 0x000D0089
		// (set) Token: 0x060034CE RID: 13518 RVA: 0x000D1E9B File Offset: 0x000D009B
		public string Version
		{
			get
			{
				return (string)this[InterceptorRuleSchema.Version];
			}
			internal set
			{
				this[InterceptorRuleSchema.Version] = value;
			}
		}

		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x060034CF RID: 13519 RVA: 0x000D1EA9 File Offset: 0x000D00A9
		// (set) Token: 0x060034D0 RID: 13520 RVA: 0x000D1EBB File Offset: 0x000D00BB
		public MultiValuedProperty<ADObjectId> Target
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[InterceptorRuleSchema.Target];
			}
			internal set
			{
				this[InterceptorRuleSchema.Target] = value;
			}
		}

		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x060034D1 RID: 13521 RVA: 0x000D1ECC File Offset: 0x000D00CC
		public DateTime ExpireTime
		{
			get
			{
				return this.ExpireTimeUtc.ToLocalTime();
			}
		}

		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x060034D2 RID: 13522 RVA: 0x000D1EE7 File Offset: 0x000D00E7
		// (set) Token: 0x060034D3 RID: 13523 RVA: 0x000D1EF9 File Offset: 0x000D00F9
		public DateTime ExpireTimeUtc
		{
			get
			{
				return (DateTime)this[InterceptorRuleSchema.ExpireTimeUtc];
			}
			internal set
			{
				this[InterceptorRuleSchema.ExpireTimeUtc] = value;
			}
		}

		// Token: 0x040023FC RID: 9212
		internal static ADObjectId InterceptorRulesContainer = new ADObjectId("CN=Interceptor Rules,CN=Transport Settings");

		// Token: 0x040023FD RID: 9213
		private static InterceptorRuleSchema schema = ObjectSchema.GetInstance<InterceptorRuleSchema>();

		// Token: 0x040023FE RID: 9214
		private static string mostDerivedClass = "msExchTransportRule";
	}
}
