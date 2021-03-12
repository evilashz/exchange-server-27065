using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200043E RID: 1086
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class EmailAddressPolicyContainer : ADConfigurationObject
	{
		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x0600312D RID: 12589 RVA: 0x000C5EBB File Offset: 0x000C40BB
		internal override ADObjectSchema Schema
		{
			get
			{
				return EmailAddressPolicyContainer.schema;
			}
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x0600312E RID: 12590 RVA: 0x000C5EC2 File Offset: 0x000C40C2
		internal override string MostDerivedObjectClass
		{
			get
			{
				return EmailAddressPolicyContainer.mostDerivedClass;
			}
		}

		// Token: 0x06003130 RID: 12592 RVA: 0x000C5ED1 File Offset: 0x000C40D1
		internal override void StampPersistableDefaultValues()
		{
			if (!base.IsModified(ADConfigurationObjectSchema.SystemFlags))
			{
				this[ADConfigurationObjectSchema.SystemFlags] = SystemFlagsEnum.None;
			}
			base.StampPersistableDefaultValues();
		}

		// Token: 0x04002107 RID: 8455
		public const string DefaultName = "Recipient Policies";

		// Token: 0x04002108 RID: 8456
		private static EmailAddressPolicyContainerSchema schema = ObjectSchema.GetInstance<EmailAddressPolicyContainerSchema>();

		// Token: 0x04002109 RID: 8457
		private static string mostDerivedClass = "msExchRecipientPolicyContainer";
	}
}
