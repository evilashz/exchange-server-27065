using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C42 RID: 3138
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ContactFaxSlotParticipantProperty : ContactEmailSlotParticipantProperty
	{
		// Token: 0x06006F16 RID: 28438 RVA: 0x001DE1CC File Offset: 0x001DC3CC
		internal ContactFaxSlotParticipantProperty(EmailAddressIndex emailAddressIndex, NativeStorePropertyDefinition displayNamePropertyDefinition, NativeStorePropertyDefinition emailAddressPropertyDefinition, NativeStorePropertyDefinition routingTypePropertyDefinition, NativeStorePropertyDefinition entryIdPropertyDefinition, NativeStorePropertyDefinition faxPropDef) : base(emailAddressIndex, displayNamePropertyDefinition, emailAddressPropertyDefinition, routingTypePropertyDefinition, entryIdPropertyDefinition, null, new PropertyDependency[]
		{
			new PropertyDependency(faxPropDef, PropertyDependencyType.AllRead)
		})
		{
			this.faxPropDef = faxPropDef;
		}

		// Token: 0x06006F17 RID: 28439 RVA: 0x001DE204 File Offset: 0x001DC404
		protected override ReadOnlyCollection<NativeStorePropertyDefinition> GetEmailSlotProperties()
		{
			ICollection<NativeStorePropertyDefinition> emailSlotProperties = base.GetEmailSlotProperties();
			NativeStorePropertyDefinition[] array = new NativeStorePropertyDefinition[emailSlotProperties.Count + 1];
			emailSlotProperties.CopyTo(array, 0);
			array[emailSlotProperties.Count] = this.faxPropDef;
			return new ReadOnlyCollection<NativeStorePropertyDefinition>(array);
		}

		// Token: 0x06006F18 RID: 28440 RVA: 0x001DE244 File Offset: 0x001DC444
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			base.InternalSetValue(propertyBag, value);
			if (value == null)
			{
				propertyBag.Delete(this.faxPropDef);
				return;
			}
			Participant participant = (Participant)value;
			propertyBag.SetValueWithFixup(this.faxPropDef, participant.EmailAddress ?? string.Empty);
		}

		// Token: 0x0400423D RID: 16957
		private readonly NativeStorePropertyDefinition faxPropDef;
	}
}
