using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C40 RID: 3136
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class EmbeddedParticipantProperty : SmartPropertyDefinition
	{
		// Token: 0x06006EFF RID: 28415 RVA: 0x001DDA4C File Offset: 0x001DBC4C
		protected EmbeddedParticipantProperty(string displayName, ParticipantEntryIdConsumer entryIdConsumer, NativeStorePropertyDefinition displayNamePropertyDefinition, NativeStorePropertyDefinition emailAddressPropertyDefinition, NativeStorePropertyDefinition routingTypePropertyDefinition, NativeStorePropertyDefinition entryIdPropertyDefinition, NativeStorePropertyDefinition smtpAddressPropertyDefinition, NativeStorePropertyDefinition sipUriPropertyDefinition, NativeStorePropertyDefinition sidPropertyDefinition, NativeStorePropertyDefinition guidPropertyDefinition, params PropertyDependency[] additionalDependencies) : base(displayName, typeof(Participant), PropertyFlags.None, Array<PropertyDefinitionConstraint>.Empty, EmbeddedParticipantProperty.GetDependencies(additionalDependencies, new NativeStorePropertyDefinition[]
		{
			displayNamePropertyDefinition,
			emailAddressPropertyDefinition,
			routingTypePropertyDefinition,
			entryIdPropertyDefinition,
			smtpAddressPropertyDefinition,
			sipUriPropertyDefinition,
			sidPropertyDefinition,
			guidPropertyDefinition
		}))
		{
			this.entryIdConsumer = entryIdConsumer;
			this.displayNamePropDef = displayNamePropertyDefinition;
			this.emailAddressPropDef = emailAddressPropertyDefinition;
			this.routingTypePropDef = routingTypePropertyDefinition;
			this.entryIdPropDef = entryIdPropertyDefinition;
			this.smtpAddressPropDef = smtpAddressPropertyDefinition;
			this.sipUriPropDef = sipUriPropertyDefinition;
			this.sidPropDef = sidPropertyDefinition;
			this.guidPropDef = guidPropertyDefinition;
		}

		// Token: 0x06006F00 RID: 28416 RVA: 0x001DDAEC File Offset: 0x001DBCEC
		internal EmbeddedParticipantProperty(string displayName, ParticipantEntryIdConsumer entryIdConsumer, NativeStorePropertyDefinition displayNamePropertyDefinition, NativeStorePropertyDefinition emailAddressPropertyDefinition, NativeStorePropertyDefinition routingTypePropertyDefinition, NativeStorePropertyDefinition entryIdPropertyDefinition, NativeStorePropertyDefinition smtpAddressPropertyDefinition, NativeStorePropertyDefinition sipUriPropertyDefinition, NativeStorePropertyDefinition sidPropertyDefinition, NativeStorePropertyDefinition guidPropertyDefinition) : this(displayName, entryIdConsumer, displayNamePropertyDefinition, emailAddressPropertyDefinition, routingTypePropertyDefinition, entryIdPropertyDefinition, smtpAddressPropertyDefinition, sipUriPropertyDefinition, sidPropertyDefinition, guidPropertyDefinition, Array<PropertyDependency>.Empty)
		{
		}

		// Token: 0x06006F01 RID: 28417 RVA: 0x001DDB18 File Offset: 0x001DBD18
		protected static PropertyDependency[] GetDependencies(PropertyDependency[] additionalDependencies, params NativeStorePropertyDefinition[] propDefsForReadAndWrite)
		{
			List<PropertyDependency> list = new List<PropertyDependency>(additionalDependencies.Length + 5);
			list.AddRange(additionalDependencies);
			foreach (NativeStorePropertyDefinition nativeStorePropertyDefinition in propDefsForReadAndWrite)
			{
				if (nativeStorePropertyDefinition != null)
				{
					list.Add(new PropertyDependency(nativeStorePropertyDefinition, PropertyDependencyType.NeedForRead));
				}
			}
			return list.ToArray();
		}

		// Token: 0x06006F02 RID: 28418 RVA: 0x001DDB64 File Offset: 0x001DBD64
		protected override void InternalDeleteValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			foreach (NativeStorePropertyDefinition nativeStorePropertyDefinition in this.AllPropertyDefinitions)
			{
				if (nativeStorePropertyDefinition != null)
				{
					propertyBag.Delete(nativeStorePropertyDefinition);
				}
			}
		}

		// Token: 0x06006F03 RID: 28419 RVA: 0x001DDBB8 File Offset: 0x001DBDB8
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			Participant participant = (Participant)value;
			ParticipantEntryId participantEntryId = ParticipantEntryId.FromParticipant(participant, this.entryIdConsumer);
			propertyBag.Update(this.displayNamePropDef, participant.DisplayName);
			propertyBag.Update(this.emailAddressPropDef, participant.EmailAddress);
			propertyBag.Update(this.routingTypePropDef, participant.RoutingType);
			propertyBag.Update(this.entryIdPropDef, (participantEntryId != null) ? participantEntryId.ToByteArray() : null);
			if (this.smtpAddressPropDef != null)
			{
				propertyBag.Update(this.smtpAddressPropDef, participant.TryGetProperty(ParticipantSchema.SmtpAddress));
			}
			if (this.sipUriPropDef != null)
			{
				propertyBag.Update(this.sipUriPropDef, participant.TryGetProperty(ParticipantSchema.SipUri));
			}
			if (this.sidPropDef != null)
			{
				propertyBag.Update(this.sidPropDef, participant.TryGetProperty(ParticipantSchema.ParticipantSID));
			}
			if (this.guidPropDef != null)
			{
				propertyBag.Update(this.guidPropDef, participant.TryGetProperty(ParticipantSchema.ParticipantGuid));
			}
		}

		// Token: 0x06006F04 RID: 28420 RVA: 0x001DDCAC File Offset: 0x001DBEAC
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			Participant.Builder builder = new Participant.Builder();
			if (!this.Get(propertyBag, builder))
			{
				return new PropertyError(this, PropertyErrorCode.NotFound);
			}
			return builder.ToParticipant();
		}

		// Token: 0x17001DF8 RID: 7672
		// (get) Token: 0x06006F05 RID: 28421 RVA: 0x001DDCD7 File Offset: 0x001DBED7
		internal NativeStorePropertyDefinition DisplayNamePropertyDefinition
		{
			get
			{
				return this.displayNamePropDef;
			}
		}

		// Token: 0x17001DF9 RID: 7673
		// (get) Token: 0x06006F06 RID: 28422 RVA: 0x001DDCDF File Offset: 0x001DBEDF
		internal NativeStorePropertyDefinition EmailAddressPropertyDefinition
		{
			get
			{
				return this.emailAddressPropDef;
			}
		}

		// Token: 0x17001DFA RID: 7674
		// (get) Token: 0x06006F07 RID: 28423 RVA: 0x001DDCE7 File Offset: 0x001DBEE7
		internal NativeStorePropertyDefinition RoutingTypePropertyDefinition
		{
			get
			{
				return this.routingTypePropDef;
			}
		}

		// Token: 0x17001DFB RID: 7675
		// (get) Token: 0x06006F08 RID: 28424 RVA: 0x001DDCEF File Offset: 0x001DBEEF
		internal NativeStorePropertyDefinition EntryIdPropertyDefinition
		{
			get
			{
				return this.entryIdPropDef;
			}
		}

		// Token: 0x17001DFC RID: 7676
		// (get) Token: 0x06006F09 RID: 28425 RVA: 0x001DDCF7 File Offset: 0x001DBEF7
		internal NativeStorePropertyDefinition SmtpAddressPropertyDefinition
		{
			get
			{
				return this.smtpAddressPropDef;
			}
		}

		// Token: 0x17001DFD RID: 7677
		// (get) Token: 0x06006F0A RID: 28426 RVA: 0x001DDCFF File Offset: 0x001DBEFF
		internal NativeStorePropertyDefinition SipUriPropertyDefinition
		{
			get
			{
				return this.sipUriPropDef;
			}
		}

		// Token: 0x17001DFE RID: 7678
		// (get) Token: 0x06006F0B RID: 28427 RVA: 0x001DDD07 File Offset: 0x001DBF07
		internal NativeStorePropertyDefinition SidPropertyDefinition
		{
			get
			{
				return this.sidPropDef;
			}
		}

		// Token: 0x17001DFF RID: 7679
		// (get) Token: 0x06006F0C RID: 28428 RVA: 0x001DDD0F File Offset: 0x001DBF0F
		internal NativeStorePropertyDefinition GuidPropertyDefinition
		{
			get
			{
				return this.guidPropDef;
			}
		}

		// Token: 0x17001E00 RID: 7680
		// (get) Token: 0x06006F0D RID: 28429 RVA: 0x001DDD18 File Offset: 0x001DBF18
		protected virtual IList<NativeStorePropertyDefinition> AllPropertyDefinitions
		{
			get
			{
				List<NativeStorePropertyDefinition> list = new List<NativeStorePropertyDefinition>();
				list.Add(this.displayNamePropDef);
				list.Add(this.emailAddressPropDef);
				list.Add(this.routingTypePropDef);
				list.Add(this.entryIdPropDef);
				if (this.smtpAddressPropDef != null)
				{
					list.Add(this.smtpAddressPropDef);
				}
				if (this.sipUriPropDef != null)
				{
					list.Add(this.sipUriPropDef);
				}
				if (this.sidPropDef != null)
				{
					list.Add(this.sidPropDef);
				}
				if (this.guidPropDef != null)
				{
					list.Add(this.guidPropDef);
				}
				return list;
			}
		}

		// Token: 0x06006F0E RID: 28430 RVA: 0x001DDDAC File Offset: 0x001DBFAC
		protected bool Get(PropertyBag.BasicPropertyStore propertyBag, Participant.Builder participantBuilder)
		{
			byte[] valueOrDefault = propertyBag.GetValueOrDefault<byte[]>(this.entryIdPropDef);
			if (valueOrDefault != null)
			{
				participantBuilder.SetPropertiesFrom(ParticipantEntryId.TryFromEntryId(valueOrDefault));
			}
			participantBuilder.DisplayName = propertyBag.GetValueOrDefault<string>(this.displayNamePropDef, participantBuilder.DisplayName);
			participantBuilder.EmailAddress = propertyBag.GetValueOrDefault<string>(this.emailAddressPropDef, participantBuilder.EmailAddress);
			participantBuilder.RoutingType = propertyBag.GetValueOrDefault<string>(this.routingTypePropDef, participantBuilder.RoutingType);
			if (this.smtpAddressPropDef != null)
			{
				string valueOrDefault2 = propertyBag.GetValueOrDefault<string>(this.smtpAddressPropDef);
				if (!string.IsNullOrEmpty(valueOrDefault2))
				{
					participantBuilder[ParticipantSchema.SmtpAddress] = valueOrDefault2;
				}
			}
			if (this.sipUriPropDef != null)
			{
				string valueOrDefault3 = propertyBag.GetValueOrDefault<string>(this.sipUriPropDef);
				if (!string.IsNullOrEmpty(valueOrDefault3))
				{
					participantBuilder[ParticipantSchema.SipUri] = valueOrDefault3;
				}
			}
			if (this.sidPropDef != null)
			{
				byte[] valueOrDefault4 = propertyBag.GetValueOrDefault<byte[]>(this.sidPropDef);
				if (valueOrDefault4 != null)
				{
					participantBuilder[ParticipantSchema.ParticipantSID] = valueOrDefault4;
				}
			}
			if (this.guidPropDef != null)
			{
				byte[] valueOrDefault5 = propertyBag.GetValueOrDefault<byte[]>(this.guidPropDef);
				if (valueOrDefault5 != null)
				{
					participantBuilder[ParticipantSchema.ParticipantGuid] = valueOrDefault5;
				}
			}
			return !string.IsNullOrEmpty(participantBuilder.DisplayName) || !string.IsNullOrEmpty(participantBuilder.EmailAddress);
		}

		// Token: 0x04004230 RID: 16944
		private readonly ParticipantEntryIdConsumer entryIdConsumer;

		// Token: 0x04004231 RID: 16945
		private readonly NativeStorePropertyDefinition displayNamePropDef;

		// Token: 0x04004232 RID: 16946
		private readonly NativeStorePropertyDefinition emailAddressPropDef;

		// Token: 0x04004233 RID: 16947
		private readonly NativeStorePropertyDefinition entryIdPropDef;

		// Token: 0x04004234 RID: 16948
		private readonly NativeStorePropertyDefinition routingTypePropDef;

		// Token: 0x04004235 RID: 16949
		private readonly NativeStorePropertyDefinition smtpAddressPropDef;

		// Token: 0x04004236 RID: 16950
		private readonly NativeStorePropertyDefinition sipUriPropDef;

		// Token: 0x04004237 RID: 16951
		private readonly NativeStorePropertyDefinition sidPropDef;

		// Token: 0x04004238 RID: 16952
		private readonly NativeStorePropertyDefinition guidPropDef;
	}
}
