using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200048C RID: 1164
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class AttributionDisplayNameProperty : SmartPropertyDefinition
	{
		// Token: 0x060033B4 RID: 13236 RVA: 0x000D203B File Offset: 0x000D023B
		internal AttributionDisplayNameProperty() : base("AttributionDisplayName", typeof(string), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, AttributionDisplayNameProperty.PropertyDependencies)
		{
		}

		// Token: 0x060033B5 RID: 13237 RVA: 0x000D2060 File Offset: 0x000D0260
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			string text = propertyBag.GetValueOrDefault<string>(InternalSchema.PartnerNetworkId, null);
			if (string.IsNullOrEmpty(text))
			{
				text = WellKnownNetworkNames.Outlook;
			}
			else if (text == WellKnownNetworkNames.QuickContacts)
			{
				string valueOrDefault = propertyBag.GetValueOrDefault<string>(InternalSchema.ParentDisplayName, null);
				if ("{06967759-274D-40B2-A3EB-D7F9E73727D7}" != valueOrDefault)
				{
					text = valueOrDefault;
				}
				else
				{
					text = ServerStrings.PeopleQuickContactsAttributionDisplayName;
				}
			}
			else if (!WellKnownNetworkNames.IsWellKnownExternalNetworkName(text))
			{
				text = WellKnownNetworkNames.Outlook;
			}
			return text;
		}

		// Token: 0x04001BD9 RID: 7129
		private static readonly PropertyDependency[] PropertyDependencies = new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.ParentDisplayName, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.PartnerNetworkId, PropertyDependencyType.NeedForRead)
		};
	}
}
