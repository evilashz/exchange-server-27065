using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020006A7 RID: 1703
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class PolicyTipMessageConfig : ADConfigurationObject
	{
		// Token: 0x170019C9 RID: 6601
		// (get) Token: 0x06004EB3 RID: 20147 RVA: 0x00121D74 File Offset: 0x0011FF74
		internal override ADObjectSchema Schema
		{
			get
			{
				return PolicyTipMessageConfig.schema;
			}
		}

		// Token: 0x170019CA RID: 6602
		// (get) Token: 0x06004EB4 RID: 20148 RVA: 0x00121D7B File Offset: 0x0011FF7B
		internal override string MostDerivedObjectClass
		{
			get
			{
				return PolicyTipMessageConfig.mostDerivedClass;
			}
		}

		// Token: 0x170019CB RID: 6603
		// (get) Token: 0x06004EB5 RID: 20149 RVA: 0x00121D82 File Offset: 0x0011FF82
		// (set) Token: 0x06004EB6 RID: 20150 RVA: 0x00121D94 File Offset: 0x0011FF94
		public string Locale
		{
			get
			{
				return (string)this[PolicyTipMessageConfigSchema.Locale];
			}
			set
			{
				this[PolicyTipMessageConfigSchema.Locale] = value;
			}
		}

		// Token: 0x170019CC RID: 6604
		// (get) Token: 0x06004EB7 RID: 20151 RVA: 0x00121DA2 File Offset: 0x0011FFA2
		// (set) Token: 0x06004EB8 RID: 20152 RVA: 0x00121DB4 File Offset: 0x0011FFB4
		public PolicyTipMessageConfigAction Action
		{
			get
			{
				return (PolicyTipMessageConfigAction)this[PolicyTipMessageConfigSchema.Action];
			}
			set
			{
				this[PolicyTipMessageConfigSchema.Action] = value;
			}
		}

		// Token: 0x170019CD RID: 6605
		// (get) Token: 0x06004EB9 RID: 20153 RVA: 0x00121DC7 File Offset: 0x0011FFC7
		// (set) Token: 0x06004EBA RID: 20154 RVA: 0x00121DD9 File Offset: 0x0011FFD9
		[Parameter]
		public string Value
		{
			get
			{
				return (string)this[PolicyTipMessageConfigSchema.Value];
			}
			set
			{
				this[PolicyTipMessageConfigSchema.Value] = value;
			}
		}

		// Token: 0x170019CE RID: 6606
		// (get) Token: 0x06004EBB RID: 20155 RVA: 0x00121DE7 File Offset: 0x0011FFE7
		internal override ADObjectId ParentPath
		{
			get
			{
				return PolicyTipMessageConfig.PolicyTipMessageConfigContainer;
			}
		}

		// Token: 0x170019CF RID: 6607
		// (get) Token: 0x06004EBC RID: 20156 RVA: 0x00121DEE File Offset: 0x0011FFEE
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x040035B5 RID: 13749
		internal static readonly ADObjectId PolicyTipMessageConfigContainer = new ADObjectId("CN=PolicyTipMessageConfigs,CN=Rules,CN=Transport Settings");

		// Token: 0x040035B6 RID: 13750
		private static PolicyTipMessageConfigSchema schema = ObjectSchema.GetInstance<PolicyTipMessageConfigSchema>();

		// Token: 0x040035B7 RID: 13751
		private static string mostDerivedClass = "msExchPolicyTipMessageConfig";
	}
}
