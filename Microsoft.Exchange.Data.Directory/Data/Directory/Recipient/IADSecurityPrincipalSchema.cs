using System;
using System.Security.Principal;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000270 RID: 624
	internal abstract class IADSecurityPrincipalSchema
	{
		// Token: 0x04001036 RID: 4150
		public static readonly ADPropertyDefinition SamAccountName = new ADPropertyDefinition("SamAccountName", ExchangeObjectVersion.Exchange2003, typeof(string), "SamAccountName", ADPropertyDefinitionFlags.DoNotProvisionalClone | ADPropertyDefinitionFlags.ForestSpecific, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new MandatoryStringLengthConstraint(1, 256),
			new DelegateConstraint(new ValidationDelegate(ConstraintDelegates.ValidateUserSamAccountNameIncludeNoAt)),
			new DelegateConstraint(new ValidationDelegate(ConstraintDelegates.ValidateUserSamAccountNameLength)),
			new CharacterConstraint("\"\\/[]:|<>+=;?,*\u0001\u0002\u0003\u0004\u0005\u0006\a\b\t\n\v\f\r\u000e\u000f\u0010\u0011\u0012\u0013\u0014\u0015\u0016\u0017\u0018\u0019\u001a\u001b\u001c\u001d\u001e\u001f".ToCharArray(), false),
			new ContainingNonWhitespaceConstraint(),
			new NoTrailingSpecificCharacterConstraint('.')
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04001037 RID: 4151
		public static readonly ADPropertyDefinition Sid = new ADPropertyDefinition("Sid", ExchangeObjectVersion.Exchange2003, typeof(SecurityIdentifier), "objectSid", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Binary | ADPropertyDefinitionFlags.ForestSpecific, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, MServRecipientSchema.Sid, null);

		// Token: 0x04001038 RID: 4152
		public static readonly ADPropertyDefinition SidHistory = new ADPropertyDefinition("SidHistory", ExchangeObjectVersion.Exchange2003, typeof(SecurityIdentifier), "sIDHistory", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04001039 RID: 4153
		internal static readonly ADPropertyDefinition GroupType = new ADPropertyDefinition("GroupType", ExchangeObjectVersion.Exchange2003, typeof(GroupTypeFlags), "groupType", ADPropertyDefinitionFlags.None, GroupTypeFlags.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x0400103A RID: 4154
		internal static readonly ADPropertyDefinition AltSecurityIdentities = new ADPropertyDefinition("AltSecurityIdentities", ExchangeObjectVersion.Exchange2003, typeof(string), "AltSecurityIdentities", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x0400103B RID: 4155
		public static readonly ADPropertyDefinition IsSecurityPrincipal = new ADPropertyDefinition("IsSecurityPrincipal", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.ObjectClass,
			IADSecurityPrincipalSchema.GroupType
		}, new CustomFilterBuilderDelegate(ADGroup.IsSecurityPrincipalFilterBuilder), new GetterDelegate(ADGroup.IsSecurityPrincipalGetter), null, null, null);

		// Token: 0x0400103C RID: 4156
		public static readonly ADPropertyDefinition NetID = new ADPropertyDefinition("NetID", ExchangeObjectVersion.Exchange2003, typeof(NetID), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			IADSecurityPrincipalSchema.AltSecurityIdentities
		}, new CustomFilterBuilderDelegate(ADUser.NetIdFilterBuilder), new GetterDelegate(ADUser.NetIdGetter), new SetterDelegate(ADUser.NetIdSetter), MServRecipientSchema.NetID, null);

		// Token: 0x0400103D RID: 4157
		public static readonly ADPropertyDefinition NetIDSuffix = new ADPropertyDefinition("NetIDSuffix", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			IADSecurityPrincipalSchema.AltSecurityIdentities
		}, null, new GetterDelegate(ADUser.NetIdSuffixGetter), new SetterDelegate(ADUser.NetIdSuffixSetter), null, null);

		// Token: 0x0400103E RID: 4158
		public static readonly ADPropertyDefinition ConsumerNetID = new ADPropertyDefinition("ConsumerNetID", ExchangeObjectVersion.Exchange2003, typeof(NetID), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			IADSecurityPrincipalSchema.AltSecurityIdentities
		}, new CustomFilterBuilderDelegate(ADUser.ConsumerNetIdFilterBuilder), new GetterDelegate(ADUser.ConsumerNetIdGetter), new SetterDelegate(ADUser.ConsumerNetIdSetter), null, null);

		// Token: 0x0400103F RID: 4159
		public static readonly ADPropertyDefinition OriginalNetID = new ADPropertyDefinition("OriginalNetID", ExchangeObjectVersion.Exchange2003, typeof(NetID), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			IADSecurityPrincipalSchema.AltSecurityIdentities
		}, new CustomFilterBuilderDelegate(ADUser.OriginalNetIdFilterBuilder), new GetterDelegate(ADUser.OriginalNetIdGetter), new SetterDelegate(ADUser.OriginalNetIdSetter), null, null);

		// Token: 0x04001040 RID: 4160
		public static readonly ADPropertyDefinition CertificateSubject = new ADPropertyDefinition("CertificateSubject", ExchangeObjectVersion.Exchange2003, typeof(X509Identifier), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			IADSecurityPrincipalSchema.AltSecurityIdentities
		}, new CustomFilterBuilderDelegate(ADUser.CertificateSubjectFilterBuilder), new GetterDelegate(ADUser.CertificateSubjectGetter), new SetterDelegate(ADUser.CertificateSubjectSetter), null, null);
	}
}
