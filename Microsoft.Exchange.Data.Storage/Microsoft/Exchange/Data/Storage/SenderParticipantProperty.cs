using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CC5 RID: 3269
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SenderParticipantProperty : EmbeddedParticipantProperty
	{
		// Token: 0x06007188 RID: 29064 RVA: 0x001F76B4 File Offset: 0x001F58B4
		internal SenderParticipantProperty(string displayName, NativeStorePropertyDefinition displayNamePropertyDefinition, NativeStorePropertyDefinition emailAddressPropertyDefinition, NativeStorePropertyDefinition routingTypePropertyDefinition, NativeStorePropertyDefinition entryIdPropertyDefinition, NativeStorePropertyDefinition smtpAddressPropertyDefinition, NativeStorePropertyDefinition sipUriPropertyDefinition, NativeStorePropertyDefinition sidPropertyDefinition, NativeStorePropertyDefinition guidPropertyDefinition) : base(displayName, ParticipantEntryIdConsumer.SupportsADParticipantEntryId, displayNamePropertyDefinition, emailAddressPropertyDefinition, routingTypePropertyDefinition, entryIdPropertyDefinition, smtpAddressPropertyDefinition, sipUriPropertyDefinition, sidPropertyDefinition, guidPropertyDefinition, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.SendRichInfo, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x06007189 RID: 29065 RVA: 0x001F76F0 File Offset: 0x001F58F0
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			base.InternalSetValue(propertyBag, value);
			Participant participant = (Participant)value;
			bool? valueAsNullable = participant.GetValueAsNullable<bool>(ParticipantSchema.SendRichInfo);
			if (valueAsNullable != null)
			{
				propertyBag.Update(InternalSchema.SendRichInfo, valueAsNullable.Value);
			}
		}

		// Token: 0x0600718A RID: 29066 RVA: 0x001F773C File Offset: 0x001F593C
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			Participant.Builder builder = new Participant.Builder();
			if (base.Get(propertyBag, builder))
			{
				bool? valueAsNullable = propertyBag.GetValueAsNullable<bool>(InternalSchema.SendRichInfo);
				if (valueAsNullable != null)
				{
					builder[ParticipantSchema.SendRichInfo] = valueAsNullable.Value;
				}
				return builder.ToParticipant();
			}
			return new PropertyError(this, PropertyErrorCode.NotFound);
		}
	}
}
