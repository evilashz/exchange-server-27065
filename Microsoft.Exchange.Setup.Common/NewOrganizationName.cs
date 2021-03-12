using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000047 RID: 71
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class NewOrganizationName : OrganizationName
	{
		// Token: 0x06000337 RID: 823 RVA: 0x0000B288 File Offset: 0x00009488
		public NewOrganizationName(string name) : base(name)
		{
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000B291 File Offset: 0x00009491
		public NewOrganizationName(AdName adName) : base(adName)
		{
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000B29A File Offset: 0x0000949A
		protected override ADPropertyDefinition ADPropertyDefinition
		{
			get
			{
				return NewOrganizationName.NewOrgName;
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000B2A4 File Offset: 0x000094A4
		public static PropertyConstraintViolationError ValidateIsValidNewOrgName(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag, PropertyDefinitionConstraint owner)
		{
			string text = value.ToString();
			if (!LegacyDN.IsValidLegacyCommonName(text))
			{
				return new PropertyConstraintViolationError(Strings.InvalidOrganizationName(text), propertyDefinition, value, owner);
			}
			return null;
		}

		// Token: 0x040000C5 RID: 197
		private static readonly ADPropertyDefinition NewOrgName = new ADPropertyDefinition("New Organization Name", ExchangeObjectVersion.Exchange2003, typeof(string), "New Organization Name", ADPropertyDefinitionFlags.DoNotProvisionalClone, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new NoLeadingOrTrailingWhitespaceConstraint(),
			new ADObjectNameStringLengthConstraint(1, 64),
			new ContainingNonWhitespaceConstraint(),
			new ADObjectNameCharacterConstraint(OrganizationName.ConstrainedCharacters),
			new DelegateConstraint(new ValidationDelegate(NewOrganizationName.ValidateIsValidNewOrgName))
		}, null, null);
	}
}
