using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C41 RID: 3137
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ContactEmailSlotParticipantProperty : EmbeddedParticipantProperty
	{
		// Token: 0x06006F0F RID: 28431 RVA: 0x001DDEE0 File Offset: 0x001DC0E0
		internal ContactEmailSlotParticipantProperty(EmailAddressIndex emailAddressIndex, NativeStorePropertyDefinition displayNamePropertyDefinition, NativeStorePropertyDefinition emailAddressPropertyDefinition, NativeStorePropertyDefinition routingTypePropertyDefinition, NativeStorePropertyDefinition entryIdPropertyDefinition, NativeStorePropertyDefinition emailAddressForDisplayPropertyDefinition, params PropertyDependency[] additionalDependencies) : base("Contact" + emailAddressIndex.ToString(), ParticipantEntryIdConsumer.ContactEmailSlot, displayNamePropertyDefinition, emailAddressPropertyDefinition, routingTypePropertyDefinition, entryIdPropertyDefinition, null, null, null, null, EmbeddedParticipantProperty.GetDependencies(additionalDependencies, new NativeStorePropertyDefinition[]
		{
			InternalSchema.EntryId,
			emailAddressForDisplayPropertyDefinition
		}))
		{
			this.emailAddressForDisplayPropDef = emailAddressForDisplayPropertyDefinition;
			this.emailAddressIndex = emailAddressIndex;
		}

		// Token: 0x17001E01 RID: 7681
		// (get) Token: 0x06006F10 RID: 28432 RVA: 0x001DDF40 File Offset: 0x001DC140
		internal static Dictionary<EmailAddressIndex, ContactEmailSlotParticipantProperty> AllInstances
		{
			get
			{
				if (ContactEmailSlotParticipantProperty.allInstances == null)
				{
					Dictionary<EmailAddressIndex, ContactEmailSlotParticipantProperty> dictionary = new Dictionary<EmailAddressIndex, ContactEmailSlotParticipantProperty>();
					foreach (ContactEmailSlotParticipantProperty contactEmailSlotParticipantProperty in new ContactEmailSlotParticipantProperty[]
					{
						InternalSchema.ContactEmail1,
						InternalSchema.ContactEmail2,
						InternalSchema.ContactEmail3,
						InternalSchema.ContactBusinessFax,
						InternalSchema.ContactHomeFax,
						InternalSchema.ContactOtherFax
					})
					{
						dictionary.Add(contactEmailSlotParticipantProperty.emailAddressIndex, contactEmailSlotParticipantProperty);
					}
					ContactEmailSlotParticipantProperty.allInstances = dictionary;
				}
				return ContactEmailSlotParticipantProperty.allInstances;
			}
		}

		// Token: 0x17001E02 RID: 7682
		// (get) Token: 0x06006F11 RID: 28433 RVA: 0x001DDFC2 File Offset: 0x001DC1C2
		internal ReadOnlyCollection<NativeStorePropertyDefinition> EmailSlotProperties
		{
			get
			{
				if (this.emailSlotProperties == null)
				{
					this.emailSlotProperties = this.GetEmailSlotProperties();
				}
				return this.emailSlotProperties;
			}
		}

		// Token: 0x06006F12 RID: 28434 RVA: 0x001DDFE0 File Offset: 0x001DC1E0
		protected virtual ReadOnlyCollection<NativeStorePropertyDefinition> GetEmailSlotProperties()
		{
			IList<NativeStorePropertyDefinition> allPropertyDefinitions = base.AllPropertyDefinitions;
			if (this.emailAddressForDisplayPropDef != null)
			{
				NativeStorePropertyDefinition[] array = new NativeStorePropertyDefinition[allPropertyDefinitions.Count + 1];
				allPropertyDefinitions.CopyTo(array, 0);
				array[allPropertyDefinitions.Count] = this.emailAddressForDisplayPropDef;
				return new ReadOnlyCollection<NativeStorePropertyDefinition>(array);
			}
			return new ReadOnlyCollection<NativeStorePropertyDefinition>(base.AllPropertyDefinitions);
		}

		// Token: 0x17001E03 RID: 7683
		// (get) Token: 0x06006F13 RID: 28435 RVA: 0x001DE032 File Offset: 0x001DC232
		protected override IList<NativeStorePropertyDefinition> AllPropertyDefinitions
		{
			get
			{
				return this.EmailSlotProperties;
			}
		}

		// Token: 0x06006F14 RID: 28436 RVA: 0x001DE03C File Offset: 0x001DC23C
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			Participant.Builder builder = new Participant.Builder();
			bool flag = base.Get(propertyBag, builder);
			if (this.emailAddressForDisplayPropDef != null)
			{
				string valueOrDefault = propertyBag.GetValueOrDefault<string>(this.emailAddressForDisplayPropDef);
				if (!string.IsNullOrEmpty(valueOrDefault))
				{
					builder[ParticipantSchema.EmailAddressForDisplay] = valueOrDefault;
					if (Participant.RoutingTypeEquals(builder.RoutingType, "EX") && PropertyError.IsPropertyNotFound(builder.TryGetProperty(ParticipantSchema.SmtpAddress)) && SmtpAddress.IsValidSmtpAddress(valueOrDefault))
					{
						builder[ParticipantSchema.SmtpAddress] = valueOrDefault;
					}
				}
			}
			if (!flag && PropertyError.IsPropertyNotFound(builder.TryGetProperty(ParticipantSchema.EmailAddressForDisplay)))
			{
				return new PropertyError(this, PropertyErrorCode.NotFound);
			}
			byte[] valueOrDefault2 = propertyBag.GetValueOrDefault<byte[]>(InternalSchema.EntryId);
			if (valueOrDefault2 != null)
			{
				builder.Origin = new StoreParticipantOrigin(StoreObjectId.FromProviderSpecificId(valueOrDefault2), this.emailAddressIndex);
			}
			return builder.ToParticipant();
		}

		// Token: 0x06006F15 RID: 28437 RVA: 0x001DE104 File Offset: 0x001DC304
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			Participant participant = (Participant)value;
			ParticipantEntryId participantEntryId = ParticipantEntryId.FromParticipant(participant, ParticipantEntryIdConsumer.ContactEmailSlot);
			propertyBag.SetValueWithFixup(base.DisplayNamePropertyDefinition, participant.DisplayName ?? string.Empty);
			propertyBag.SetValueWithFixup(base.EmailAddressPropertyDefinition, participant.EmailAddress ?? string.Empty);
			propertyBag.SetValueWithFixup(base.RoutingTypePropertyDefinition, participant.RoutingType ?? string.Empty);
			if (this.emailAddressForDisplayPropDef != null)
			{
				AtomicStorePropertyDefinition propertyDefinition = this.emailAddressForDisplayPropDef;
				string propertyValue;
				if ((propertyValue = participant.GetValueOrDefault<string>(ParticipantSchema.EmailAddressForDisplay)) == null)
				{
					propertyValue = (participant.EmailAddress ?? string.Empty);
				}
				propertyBag.SetValueWithFixup(propertyDefinition, propertyValue);
			}
			if (participantEntryId != null)
			{
				propertyBag.SetValueWithFixup(base.EntryIdPropertyDefinition, participantEntryId.ToByteArray());
				return;
			}
			propertyBag.Delete(base.EntryIdPropertyDefinition);
		}

		// Token: 0x04004239 RID: 16953
		private readonly NativeStorePropertyDefinition emailAddressForDisplayPropDef;

		// Token: 0x0400423A RID: 16954
		private readonly EmailAddressIndex emailAddressIndex;

		// Token: 0x0400423B RID: 16955
		private static Dictionary<EmailAddressIndex, ContactEmailSlotParticipantProperty> allInstances;

		// Token: 0x0400423C RID: 16956
		private ReadOnlyCollection<NativeStorePropertyDefinition> emailSlotProperties;
	}
}
