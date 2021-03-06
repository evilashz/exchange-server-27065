using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005BC RID: 1468
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class OutboundAddressCache : ConversionAddressCache
	{
		// Token: 0x06003C32 RID: 15410 RVA: 0x000F77D8 File Offset: 0x000F59D8
		internal OutboundAddressCache(OutboundConversionOptions options, ConversionLimitsTracker limitsTracker) : base(limitsTracker, options.UseSimpleDisplayName, options.EwsOutboundMimeConversion)
		{
			this.outboundOptions = options;
		}

		// Token: 0x06003C33 RID: 15411 RVA: 0x000F77F4 File Offset: 0x000F59F4
		internal void CopyDataFromItem(Item item)
		{
			base.ReplyTo.Clear();
			base.ReplyTo.Resync(true);
			foreach (StorePropertyDefinition propertyDefinition in base.CacheProperties)
			{
				object obj = item.TryGetProperty(propertyDefinition);
				if (!(obj is PropertyError))
				{
					this.propertyBag[propertyDefinition] = obj;
				}
			}
			base.ReplyTo.Resync(true);
			if (item is MessageItem || item is Task)
			{
				this.CopyRecipientData(item);
				return;
			}
			this.CopyAttendeeData(item);
		}

		// Token: 0x06003C34 RID: 15412 RVA: 0x000F78A0 File Offset: 0x000F5AA0
		private void CopyAttendeeData(Item item)
		{
			CalendarItemBase calendarItemBase = item as CalendarItemBase;
			if (calendarItemBase != null)
			{
				foreach (Attendee attendee in calendarItemBase.AttendeeCollection)
				{
					ConversionRecipientEntry conversionRecipientEntry = new ConversionRecipientEntry(attendee.Participant, (RecipientItemType)attendee.AttendeeType);
					foreach (PropertyDefinition propertyDefinition in attendee.CoreRecipient.PropertyBag.AllFoundProperties)
					{
						StorePropertyDefinition property = (StorePropertyDefinition)propertyDefinition;
						object obj = attendee.TryGetProperty(property);
						if (!(obj is PropertyError))
						{
							conversionRecipientEntry.SetProperty(property, obj, false);
						}
					}
					base.AddRecipient(conversionRecipientEntry);
				}
			}
		}

		// Token: 0x06003C35 RID: 15413 RVA: 0x000F7980 File Offset: 0x000F5B80
		private void CopyRecipientData(Item item)
		{
			RecipientCollection recipientCollection = null;
			CoreRecipientCollection recipientCollection2 = item.CoreItem.GetRecipientCollection(true);
			if (recipientCollection2 != null)
			{
				recipientCollection = new RecipientCollection(recipientCollection2);
			}
			foreach (Recipient recipient in recipientCollection)
			{
				ConversionRecipientEntry conversionRecipientEntry = new ConversionRecipientEntry(recipient.Participant, recipient.RecipientItemType);
				foreach (PropertyDefinition propertyDefinition in recipient.CoreRecipient.PropertyBag.AllFoundProperties)
				{
					StorePropertyDefinition property = (StorePropertyDefinition)propertyDefinition;
					object obj = recipient.TryGetProperty(property);
					if (!(obj is PropertyError))
					{
						conversionRecipientEntry.SetProperty(property, obj, false);
					}
				}
				base.AddRecipient(conversionRecipientEntry);
			}
		}

		// Token: 0x1700125C RID: 4700
		// (get) Token: 0x06003C36 RID: 15414 RVA: 0x000F7A6C File Offset: 0x000F5C6C
		internal MemoryPropertyBag Properties
		{
			get
			{
				return this.propertyBag;
			}
		}

		// Token: 0x1700125D RID: 4701
		// (get) Token: 0x06003C37 RID: 15415 RVA: 0x000F7A74 File Offset: 0x000F5C74
		internal ConversionRecipientList Recipients
		{
			get
			{
				return this.recipients;
			}
		}

		// Token: 0x06003C38 RID: 15416 RVA: 0x000F7A7C File Offset: 0x000F5C7C
		internal ADRawEntry GetCachedRecipient(Participant participant)
		{
			if (this.outboundOptions == null || this.outboundOptions.RecipientCache == null)
			{
				return null;
			}
			if (participant == null)
			{
				return null;
			}
			ProxyAddress proxyAddressForParticipant = OutboundAddressCache.GetProxyAddressForParticipant(participant);
			if (proxyAddressForParticipant == null)
			{
				return null;
			}
			Result<ADRawEntry> result;
			if (this.outboundOptions.RecipientCache.TryGetValue(proxyAddressForParticipant, out result) && result.Error == null)
			{
				return result.Data;
			}
			return null;
		}

		// Token: 0x06003C39 RID: 15417 RVA: 0x000F7AE4 File Offset: 0x000F5CE4
		internal bool? IsDelegateOfPrincipal(Participant principal, Participant delegateParticipant)
		{
			if (this.outboundOptions == null || this.outboundOptions.RecipientCache == null)
			{
				return null;
			}
			ProxyAddress proxyAddressForParticipant = OutboundAddressCache.GetProxyAddressForParticipant(principal);
			if (proxyAddressForParticipant == null)
			{
				return null;
			}
			Result<ADRawEntry> result;
			if (this.outboundOptions.RecipientCache.TryGetValue(proxyAddressForParticipant, out result))
			{
				ADRawEntry data = result.Data;
				if (data == null)
				{
					return null;
				}
				MultiValuedProperty<ADObjectId> multiValuedProperty = data[ADRecipientSchema.GrantSendOnBehalfTo] as MultiValuedProperty<ADObjectId>;
				if (multiValuedProperty == null || multiValuedProperty.Count == 0)
				{
					return new bool?(false);
				}
				ProxyAddress proxyAddressForParticipant2 = OutboundAddressCache.GetProxyAddressForParticipant(delegateParticipant);
				if (proxyAddressForParticipant2 == null)
				{
					return null;
				}
				Result<ADRawEntry> result2;
				if (this.outboundOptions.RecipientCache.TryGetValue(proxyAddressForParticipant2, out result2))
				{
					ADRawEntry data2 = result2.Data;
					if (data2 == null)
					{
						return null;
					}
					ADObjectId adobjectId = data2[ADObjectSchema.Id] as ADObjectId;
					if (adobjectId == null)
					{
						goto IL_13A;
					}
					using (MultiValuedProperty<ADObjectId>.Enumerator enumerator = multiValuedProperty.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ADObjectId x = enumerator.Current;
							if (ADObjectId.Equals(x, adobjectId))
							{
								return new bool?(true);
							}
						}
						goto IL_13A;
					}
				}
				return null;
			}
			IL_13A:
			return new bool?(false);
		}

		// Token: 0x06003C3A RID: 15418 RVA: 0x000F7C44 File Offset: 0x000F5E44
		protected override IADRecipientCache GetRecipientCache(int count)
		{
			return this.outboundOptions.InternalGetRecipientCache(count);
		}

		// Token: 0x06003C3B RID: 15419 RVA: 0x000F7C52 File Offset: 0x000F5E52
		protected override bool CanResolveParticipant(Participant participant)
		{
			return this.outboundOptions.IsSenderTrusted || (participant != null && participant.RoutingType == "EX");
		}

		// Token: 0x1700125E RID: 4702
		// (get) Token: 0x06003C3C RID: 15420 RVA: 0x000F7C7E File Offset: 0x000F5E7E
		protected override string TargetResolutionType
		{
			get
			{
				return "SMTP";
			}
		}

		// Token: 0x06003C3D RID: 15421 RVA: 0x000F7C88 File Offset: 0x000F5E88
		private static ProxyAddress GetProxyAddressForParticipant(Participant participant)
		{
			if (string.IsNullOrEmpty(participant.RoutingType) || string.IsNullOrEmpty(participant.EmailAddress))
			{
				return null;
			}
			if (participant.RoutingType.Equals("SMTP", StringComparison.OrdinalIgnoreCase))
			{
				if (SmtpProxyAddress.IsValidProxyAddress(participant.EmailAddress))
				{
					return new SmtpProxyAddress(participant.EmailAddress, true);
				}
				return null;
			}
			else
			{
				if (!participant.RoutingType.Equals("EX", StringComparison.OrdinalIgnoreCase))
				{
					return null;
				}
				return ProxyAddress.Parse(ProxyAddressPrefix.LegacyDN.PrimaryPrefix, participant.EmailAddress);
			}
		}

		// Token: 0x04002005 RID: 8197
		private OutboundConversionOptions outboundOptions;
	}
}
