using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000492 RID: 1170
	internal sealed class JournalingReconciliationAccountSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04002407 RID: 9223
		private static readonly PropertyDefinitionConstraint[] ReconciliationUrlConstraints = new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute),
			new StringLengthConstraint(8, 2048)
		};

		// Token: 0x04002408 RID: 9224
		public static readonly ADPropertyDefinition ArchiveUri = new ADPropertyDefinition("ArchiveUri", ExchangeObjectVersion.Exchange2010, typeof(Uri), "msExchJournalingReconciliationUrl", ADPropertyDefinitionFlags.None, null, JournalingReconciliationAccountSchema.ReconciliationUrlConstraints, JournalingReconciliationAccountSchema.ReconciliationUrlConstraints, null, null);

		// Token: 0x04002409 RID: 9225
		public static readonly ADPropertyDefinition UserName = new ADPropertyDefinition("UserName", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchJournalingReconciliationUsername", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400240A RID: 9226
		public static readonly ADPropertyDefinition Password = new ADPropertyDefinition("Password", ExchangeObjectVersion.Exchange2010, typeof(string), "msExchJournalingReconciliationPassword", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400240B RID: 9227
		public static readonly ADPropertyDefinition Mailboxes = new ADPropertyDefinition("Mailboxes", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchJournalingReconciliationMailboxes", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400240C RID: 9228
		public static readonly ADPropertyDefinition AuthenticationCredential = new ADPropertyDefinition("AuthenticationCredential", ExchangeObjectVersion.Exchange2010, typeof(PSCredential), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			JournalingReconciliationAccountSchema.UserName,
			JournalingReconciliationAccountSchema.Password
		}, null, (IPropertyBag propertyBag) => PSCredentialHelper.GetCredentialFromUserPass((string)propertyBag[JournalingReconciliationAccountSchema.UserName], (string)propertyBag[JournalingReconciliationAccountSchema.Password], false), delegate(object value, IPropertyBag propertyBag)
		{
			string value2 = null;
			string value3 = null;
			PSCredentialHelper.GetUserPassFromCredential((PSCredential)value, out value2, out value3, false);
			propertyBag[JournalingReconciliationAccountSchema.UserName] = value2;
			propertyBag[JournalingReconciliationAccountSchema.Password] = value3;
		}, null, null);
	}
}
