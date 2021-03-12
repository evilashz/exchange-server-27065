using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005BA RID: 1466
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class ConversionAddressCache : ConversionAddressCollection
	{
		// Token: 0x17001254 RID: 4692
		// (get) Token: 0x06003C17 RID: 15383 RVA: 0x000F6F5D File Offset: 0x000F515D
		internal HashSet<NativeStorePropertyDefinition> CacheProperties
		{
			get
			{
				return this.cacheProperties;
			}
		}

		// Token: 0x17001255 RID: 4693
		// (get) Token: 0x06003C18 RID: 15384 RVA: 0x000F6F65 File Offset: 0x000F5165
		internal static HashSet<NativeStorePropertyDefinition> AllCacheProperties
		{
			get
			{
				return ConversionAddressCache.mergeCacheProperties;
			}
		}

		// Token: 0x06003C19 RID: 15385 RVA: 0x000F6F6C File Offset: 0x000F516C
		internal bool IsCacheProperty(NativeStorePropertyDefinition property)
		{
			return this.itemParticipants.CacheProperties.Contains(property);
		}

		// Token: 0x06003C1A RID: 15386 RVA: 0x000F6F7F File Offset: 0x000F517F
		internal static bool IsAnyCacheProperty(NativeStorePropertyDefinition property)
		{
			return ConversionAddressCache.AllCacheProperties.Contains(property);
		}

		// Token: 0x06003C1B RID: 15387 RVA: 0x000F6F8C File Offset: 0x000F518C
		internal ConversionAddressCache(ConversionLimitsTracker limitsTracker, bool useSimpleDisplayName, bool ewsOutboundMimeConversion) : base(useSimpleDisplayName, ewsOutboundMimeConversion)
		{
			this.limitsTracker = limitsTracker;
			if (!this.limitsTracker.EnforceLimits)
			{
				this.disableLengthValidation = true;
			}
			this.propertyBag = new MemoryPropertyBag();
			this.propertyBag.SetAllPropertiesLoaded();
			this.recipients = new ConversionRecipientList();
			this.replyTo = new ReplyTo(this.propertyBag, false);
			this.itemParticipants = new ConversionItemParticipants(this.propertyBag, this.ewsOutboundMimeConversion);
			base.AddParticipantList(this.recipients);
			base.AddParticipantList(this.ReplyTo);
			base.AddParticipantList(this.itemParticipants);
			this.cacheProperties = new HashSet<NativeStorePropertyDefinition>(Util.MergeArrays<NativeStorePropertyDefinition>(new ICollection<NativeStorePropertyDefinition>[]
			{
				this.itemParticipants.CacheProperties,
				new NativeStorePropertyDefinition[]
				{
					InternalSchema.MapiReplyToBlob,
					InternalSchema.MapiReplyToNames
				}
			}));
		}

		// Token: 0x06003C1C RID: 15388 RVA: 0x000F706B File Offset: 0x000F526B
		internal void SetProperty(NativeStorePropertyDefinition property, object value)
		{
			this.propertyBag[property] = value;
		}

		// Token: 0x06003C1D RID: 15389 RVA: 0x000F707C File Offset: 0x000F527C
		internal void AddRecipients(List<Participant> participants, RecipientItemType recipientType)
		{
			foreach (Participant participant in participants)
			{
				ConversionRecipientEntry recipientEntry = new ConversionRecipientEntry(participant, recipientType);
				this.AddRecipient(recipientEntry);
			}
		}

		// Token: 0x06003C1E RID: 15390 RVA: 0x000F70D4 File Offset: 0x000F52D4
		internal void AddRecipient(ConversionRecipientEntry recipientEntry)
		{
			this.limitsTracker.CountRecipient();
			this.recipients.Add(recipientEntry);
		}

		// Token: 0x06003C1F RID: 15391 RVA: 0x000F70F0 File Offset: 0x000F52F0
		internal void AddReplyTo(List<Participant> participants)
		{
			foreach (Participant participant in participants)
			{
				Participant item = participant;
				if (participant.RoutingType == null)
				{
					item = new Participant(participant.DisplayName, participant.DisplayName, "SMTP");
				}
				this.ReplyTo.Add(item);
			}
		}

		// Token: 0x17001256 RID: 4694
		// (get) Token: 0x06003C20 RID: 15392 RVA: 0x000F716C File Offset: 0x000F536C
		internal ConversionItemParticipants Participants
		{
			get
			{
				return this.itemParticipants;
			}
		}

		// Token: 0x17001257 RID: 4695
		// (get) Token: 0x06003C21 RID: 15393 RVA: 0x000F7174 File Offset: 0x000F5374
		internal ReplyTo ReplyTo
		{
			get
			{
				if (this.replyTo == null)
				{
					this.replyTo = new ReplyTo(this.propertyBag);
				}
				return this.replyTo;
			}
		}

		// Token: 0x06003C22 RID: 15394 RVA: 0x000F7198 File Offset: 0x000F5398
		internal void Resolve()
		{
			this.ReplyTo.Resync(true);
			ConversionAddressCollection.ParticipantResolutionList participantResolutionList = base.CreateResolutionList();
			base.ResolveParticipants(participantResolutionList);
			base.SetResolvedParticipants(participantResolutionList);
			this.ReplyTo.Resync(true);
		}

		// Token: 0x06003C23 RID: 15395 RVA: 0x000F71D2 File Offset: 0x000F53D2
		internal void Cleanup()
		{
			this.recipients.Clear();
			this.propertyBag.Clear();
			this.propertyBag.SetAllPropertiesLoaded();
			this.replyTo = null;
		}

		// Token: 0x04001FFB RID: 8187
		protected ConversionRecipientList recipients;

		// Token: 0x04001FFC RID: 8188
		protected ReplyTo replyTo;

		// Token: 0x04001FFD RID: 8189
		protected ConversionItemParticipants itemParticipants;

		// Token: 0x04001FFE RID: 8190
		protected MemoryPropertyBag propertyBag;

		// Token: 0x04001FFF RID: 8191
		protected ConversionLimitsTracker limitsTracker;

		// Token: 0x04002000 RID: 8192
		protected HashSet<NativeStorePropertyDefinition> cacheProperties;

		// Token: 0x04002001 RID: 8193
		private static HashSet<NativeStorePropertyDefinition> mergeCacheProperties = new HashSet<NativeStorePropertyDefinition>(Util.MergeArrays<NativeStorePropertyDefinition>(new ICollection<NativeStorePropertyDefinition>[]
		{
			ConversionItemParticipants.AllCacheProperties,
			new NativeStorePropertyDefinition[]
			{
				InternalSchema.MapiReplyToBlob,
				InternalSchema.MapiReplyToNames
			}
		}));
	}
}
