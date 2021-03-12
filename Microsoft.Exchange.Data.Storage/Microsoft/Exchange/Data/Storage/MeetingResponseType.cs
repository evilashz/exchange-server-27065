using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C34 RID: 3124
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class MeetingResponseType : SmartPropertyDefinition
	{
		// Token: 0x06006ED4 RID: 28372 RVA: 0x001DCEF4 File Offset: 0x001DB0F4
		internal MeetingResponseType() : base("MeetingResponseType", typeof(ResponseType), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.ItemClass, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.MapiResponseType, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x06006ED5 RID: 28373 RVA: 0x001DCF40 File Offset: 0x001DB140
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			string itemClass = propertyBag.GetValue(InternalSchema.ItemClass) as string;
			ResponseType responseType;
			if (ObjectClass.IsMeetingPositiveResponse(itemClass))
			{
				responseType = ResponseType.Accept;
			}
			else if (ObjectClass.IsMeetingTentativeResponse(itemClass))
			{
				responseType = ResponseType.Tentative;
			}
			else
			{
				if (!ObjectClass.IsMeetingNegativeResponse(itemClass))
				{
					return propertyBag.GetValue(InternalSchema.MapiResponseType);
				}
				responseType = ResponseType.Decline;
			}
			return responseType;
		}

		// Token: 0x06006ED6 RID: 28374 RVA: 0x001DCF96 File Offset: 0x001DB196
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			propertyBag.SetValueWithFixup(InternalSchema.MapiResponseType, value);
		}
	}
}
