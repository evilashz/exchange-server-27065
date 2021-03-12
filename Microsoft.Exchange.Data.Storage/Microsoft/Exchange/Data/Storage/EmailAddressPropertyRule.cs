using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AA5 RID: 2725
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class EmailAddressPropertyRule
	{
		// Token: 0x06006376 RID: 25462 RVA: 0x001A3468 File Offset: 0x001A1668
		public static bool UpdateEmailProperties(ICorePropertyBag propertyBag)
		{
			bool flag = false;
			flag |= EmailAddressPropertyRule.UpdateEmailAddressProperty(propertyBag, EmailAddressProperties.Email1);
			flag |= EmailAddressPropertyRule.UpdateEmailAddressProperty(propertyBag, EmailAddressProperties.Email2);
			return flag | EmailAddressPropertyRule.UpdateEmailAddressProperty(propertyBag, EmailAddressProperties.Email3);
		}

		// Token: 0x06006377 RID: 25463 RVA: 0x001A34A4 File Offset: 0x001A16A4
		internal static bool UpdateEmailAddressProperty(ICorePropertyBag propertyBag, EmailAddressProperties emailAddressProperty)
		{
			if (!propertyBag.IsPropertyDirty(emailAddressProperty.OriginalDisplayName) || propertyBag.IsPropertyDirty(emailAddressProperty.Address))
			{
				return false;
			}
			string valueOrDefault = propertyBag.GetValueOrDefault<string>(emailAddressProperty.RoutingType, null);
			if (!"SMTP".Equals(valueOrDefault, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			string valueOrDefault2 = propertyBag.GetValueOrDefault<string>(emailAddressProperty.OriginalDisplayName, null);
			if (string.IsNullOrEmpty(valueOrDefault2))
			{
				return false;
			}
			string valueOrDefault3 = propertyBag.GetValueOrDefault<string>(emailAddressProperty.Address, null);
			if (string.IsNullOrEmpty(valueOrDefault3))
			{
				propertyBag[emailAddressProperty.Address] = valueOrDefault2;
				return true;
			}
			return false;
		}

		// Token: 0x04003835 RID: 14389
		public static readonly PropertyReference[] UpdateProperties = new PropertyReference[]
		{
			new PropertyReference(EmailAddressProperties.Email1.RoutingType, PropertyAccess.Read),
			new PropertyReference(EmailAddressProperties.Email2.RoutingType, PropertyAccess.Read),
			new PropertyReference(EmailAddressProperties.Email3.RoutingType, PropertyAccess.Read),
			new PropertyReference(EmailAddressProperties.Email1.Address, PropertyAccess.ReadWrite),
			new PropertyReference(EmailAddressProperties.Email2.Address, PropertyAccess.ReadWrite),
			new PropertyReference(EmailAddressProperties.Email3.Address, PropertyAccess.ReadWrite),
			new PropertyReference(EmailAddressProperties.Email1.OriginalDisplayName, PropertyAccess.Read),
			new PropertyReference(EmailAddressProperties.Email2.OriginalDisplayName, PropertyAccess.Read),
			new PropertyReference(EmailAddressProperties.Email3.OriginalDisplayName, PropertyAccess.Read)
		};
	}
}
