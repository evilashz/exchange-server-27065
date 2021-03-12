using System;

namespace Microsoft.Exchange.Data.Directory.Provisioning
{
	// Token: 0x02000797 RID: 1943
	internal class RecipientEnforcementProvisioningPolicySchema : EnforcementProvisioningPolicySchema
	{
		// Token: 0x040040E4 RID: 16612
		public static readonly ADPropertyDefinition ObjectCountQuota = new ADPropertyDefinition("ObjectCountQuota", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchObjectCountQuota", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x040040E5 RID: 16613
		public static readonly ADPropertyDefinition DistributionListCountQuota = new ADPropertyDefinition("DistributionListCountQuota", ExchangeObjectVersion.Exchange2010, typeof(Unlimited<int>), null, ADPropertyDefinitionFlags.Calculated, Unlimited<int>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			RecipientEnforcementProvisioningPolicySchema.ObjectCountQuota
		}, null, new GetterDelegate(RecipientEnforcementProvisioningPolicy.DistributionListCountQuotaGetter), new SetterDelegate(RecipientEnforcementProvisioningPolicy.DistributionListCountQuotaSetter), null, null);

		// Token: 0x040040E6 RID: 16614
		public static readonly ADPropertyDefinition DistributionListCount = new ADPropertyDefinition("DistributionListCount", ExchangeObjectVersion.Exchange2010, typeof(int?), null, ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040040E7 RID: 16615
		public static readonly ADPropertyDefinition MailboxCountQuota = new ADPropertyDefinition("MailboxCountQuota", ExchangeObjectVersion.Exchange2010, typeof(Unlimited<int>), null, ADPropertyDefinitionFlags.Calculated, Unlimited<int>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			RecipientEnforcementProvisioningPolicySchema.ObjectCountQuota
		}, null, new GetterDelegate(RecipientEnforcementProvisioningPolicy.MailboxCountQuotaGetter), new SetterDelegate(RecipientEnforcementProvisioningPolicy.MailboxCountQuotaSetter), null, null);

		// Token: 0x040040E8 RID: 16616
		public static readonly ADPropertyDefinition MailboxCount = new ADPropertyDefinition("MailboxCount", ExchangeObjectVersion.Exchange2010, typeof(int?), null, ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040040E9 RID: 16617
		public static readonly ADPropertyDefinition MailUserCountQuota = new ADPropertyDefinition("MailUserCountQuota", ExchangeObjectVersion.Exchange2010, typeof(Unlimited<int>), null, ADPropertyDefinitionFlags.Calculated, Unlimited<int>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			RecipientEnforcementProvisioningPolicySchema.ObjectCountQuota
		}, null, new GetterDelegate(RecipientEnforcementProvisioningPolicy.MailUserCountQuotaGetter), new SetterDelegate(RecipientEnforcementProvisioningPolicy.MailUserCountQuotaSetter), null, null);

		// Token: 0x040040EA RID: 16618
		public static readonly ADPropertyDefinition MailUserCount = new ADPropertyDefinition("MailUserCount", ExchangeObjectVersion.Exchange2010, typeof(int?), null, ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040040EB RID: 16619
		public static readonly ADPropertyDefinition ContactCountQuota = new ADPropertyDefinition("ContactCountQuota", ExchangeObjectVersion.Exchange2010, typeof(Unlimited<int>), null, ADPropertyDefinitionFlags.Calculated, Unlimited<int>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			RecipientEnforcementProvisioningPolicySchema.ObjectCountQuota
		}, null, new GetterDelegate(RecipientEnforcementProvisioningPolicy.ContactCountQuotaGetter), new SetterDelegate(RecipientEnforcementProvisioningPolicy.ContactCountQuotaSetter), null, null);

		// Token: 0x040040EC RID: 16620
		public static readonly ADPropertyDefinition ContactCount = new ADPropertyDefinition("ContactCount", ExchangeObjectVersion.Exchange2010, typeof(int?), null, ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040040ED RID: 16621
		public static readonly ADPropertyDefinition TeamMailboxCountQuota = new ADPropertyDefinition("TeamMailboxCountQuota", ExchangeObjectVersion.Exchange2010, typeof(Unlimited<int>), null, ADPropertyDefinitionFlags.Calculated, Unlimited<int>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			RecipientEnforcementProvisioningPolicySchema.ObjectCountQuota
		}, null, new GetterDelegate(RecipientEnforcementProvisioningPolicy.TeamMailboxCountQuotaGetter), new SetterDelegate(RecipientEnforcementProvisioningPolicy.TeamMailboxCountQuotaSetter), null, null);

		// Token: 0x040040EE RID: 16622
		public static readonly ADPropertyDefinition TeamMailboxCount = new ADPropertyDefinition("TeamMailboxCount", ExchangeObjectVersion.Exchange2010, typeof(int?), null, ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040040EF RID: 16623
		public static readonly ADPropertyDefinition PublicFolderMailboxCountQuota = new ADPropertyDefinition("PublicFolderMailboxCountQuota", ExchangeObjectVersion.Exchange2010, typeof(Unlimited<int>), null, ADPropertyDefinitionFlags.Calculated, Unlimited<int>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			RecipientEnforcementProvisioningPolicySchema.ObjectCountQuota
		}, null, new GetterDelegate(RecipientEnforcementProvisioningPolicy.PublicFolderMailboxCountQuotaGetter), new SetterDelegate(RecipientEnforcementProvisioningPolicy.PublicFolderMailboxCountQuotaSetter), null, null);

		// Token: 0x040040F0 RID: 16624
		public static readonly ADPropertyDefinition PublicFolderMailboxCount = new ADPropertyDefinition("PublicFolderMailboxCount", ExchangeObjectVersion.Exchange2010, typeof(int?), null, ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040040F1 RID: 16625
		public static readonly ADPropertyDefinition MailPublicFolderCountQuota = new ADPropertyDefinition("MailPublicFolderCountQuota", ExchangeObjectVersion.Exchange2010, typeof(Unlimited<int>), null, ADPropertyDefinitionFlags.Calculated, Unlimited<int>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			RecipientEnforcementProvisioningPolicySchema.ObjectCountQuota
		}, null, new GetterDelegate(RecipientEnforcementProvisioningPolicy.MailPublicFolderCountQuotaGetter), new SetterDelegate(RecipientEnforcementProvisioningPolicy.MailPublicFolderCountQuotaSetter), null, null);

		// Token: 0x040040F2 RID: 16626
		public static readonly ADPropertyDefinition MailPublicFolderCount = new ADPropertyDefinition("MailPublicFolderCount", ExchangeObjectVersion.Exchange2010, typeof(int?), null, ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
