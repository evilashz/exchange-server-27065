using System;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch
{
	// Token: 0x02000D17 RID: 3351
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxDiscoverySearchRequestSchema : EwsStoreObjectSchema
	{
		// Token: 0x060073E3 RID: 29667 RVA: 0x00202354 File Offset: 0x00200554
		public static GuidNamePropertyDefinition CreateStorePropertyDefinition(EwsStoreObjectPropertyDefinition ewsStorePropertyDefinition)
		{
			ExtendedPropertyDefinition extendedPropertyDefinition = (ExtendedPropertyDefinition)ewsStorePropertyDefinition.StorePropertyDefinition;
			Type propertyType = ((ewsStorePropertyDefinition.PropertyDefinitionFlags & PropertyDefinitionFlags.MultiValued) == PropertyDefinitionFlags.MultiValued) ? ewsStorePropertyDefinition.Type.MakeArrayType() : ewsStorePropertyDefinition.Type;
			return GuidNamePropertyDefinition.InternalCreate(ewsStorePropertyDefinition.Name, propertyType, MailboxDiscoverySearchRequestSchema.GetMapiPropType(extendedPropertyDefinition.MapiType), extendedPropertyDefinition.PropertySetId.Value, extendedPropertyDefinition.Name, PropertyFlags.None, NativeStorePropertyDefinition.TypeCheckingFlag.DisableTypeCheck, false, PropertyDefinitionConstraint.None);
		}

		// Token: 0x060073E4 RID: 29668 RVA: 0x002023C0 File Offset: 0x002005C0
		private static PropType GetMapiPropType(MapiPropertyType ewsMapiPropertyType)
		{
			PropType result;
			if (ewsMapiPropertyType != 14)
			{
				switch (ewsMapiPropertyType)
				{
				case 25:
					result = PropType.String;
					break;
				case 26:
					result = PropType.StringArray;
					break;
				default:
					throw new NotImplementedException();
				}
			}
			else
			{
				result = PropType.Int;
			}
			return result;
		}

		// Token: 0x040050EA RID: 20714
		public static readonly EwsStoreObjectPropertyDefinition AsynchronousActionRequest = new EwsStoreObjectPropertyDefinition("AsynchronousActionRequest", ExchangeObjectVersion.Exchange2012, typeof(ActionRequestType), PropertyDefinitionFlags.None, ActionRequestType.None, ActionRequestType.None, MailboxDiscoverySearchRequestExtendedStoreSchema.AsynchronousActionRequest);

		// Token: 0x040050EB RID: 20715
		public static readonly EwsStoreObjectPropertyDefinition AsynchronousActionRbacContext = new EwsStoreObjectPropertyDefinition("AsynchronousActionRbacContext", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, null, null, MailboxDiscoverySearchRequestExtendedStoreSchema.AsynchronousActionRbacContext);
	}
}
