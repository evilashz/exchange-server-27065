using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005BB RID: 1467
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class InboundAddressCache : ConversionAddressCache
	{
		// Token: 0x06003C25 RID: 15397 RVA: 0x000F7243 File Offset: 0x000F5443
		internal InboundAddressCache(InboundConversionOptions options, ConversionLimitsTracker limitsTracker, MimeMessageLevel messageLevel) : base(limitsTracker, false, false)
		{
			this.inboundOptions = options;
			this.mimeMessageLevel = messageLevel;
			this.tnefRecipientList = null;
		}

		// Token: 0x06003C26 RID: 15398 RVA: 0x000F7264 File Offset: 0x000F5464
		internal void AddDependentAddressCache(InboundAddressCache tnefAddressCache)
		{
			this.tnefRecipientList = tnefAddressCache.recipients;
			base.AddParticipantList(tnefAddressCache.recipients);
			base.ReplyTo.Resync(true);
			tnefAddressCache.ReplyTo.Resync(true);
			if (this.ReplaceMatchingEntries(base.ReplyTo, tnefAddressCache.ReplyTo))
			{
				base.ReplyTo.Resync(true);
			}
			if (base.ReplyTo.Count == 0 && tnefAddressCache.ReplyTo.Count != 0)
			{
				this.propertyBag[InternalSchema.MapiReplyToBlob] = tnefAddressCache.ReplyTo.Blob;
				this.propertyBag[InternalSchema.MapiReplyToNames] = tnefAddressCache.ReplyTo.Names;
				this.replyTo = null;
			}
			foreach (ConversionItemParticipants.ParticipantDefinitionEntry participantDefinitionEntry in ConversionItemParticipants.ParticipantEntries)
			{
				Participant valueOrDefault = this.propertyBag.GetValueOrDefault<Participant>(participantDefinitionEntry.ParticipantProperty);
				if (valueOrDefault == null)
				{
					valueOrDefault = tnefAddressCache.propertyBag.GetValueOrDefault<Participant>(participantDefinitionEntry.ParticipantProperty);
					if (valueOrDefault != null && (participantDefinitionEntry.IsAlwaysResolvable || this.CanResolveParticipant(valueOrDefault) || valueOrDefault.RoutingType != "EX"))
					{
						this.propertyBag.SetOrDeleteProperty(participantDefinitionEntry.ParticipantProperty, valueOrDefault);
					}
				}
			}
		}

		// Token: 0x06003C27 RID: 15399 RVA: 0x000F739B File Offset: 0x000F559B
		internal void ClearRecipients()
		{
			this.limitsTracker.RollbackRecipients(this.recipients.Count);
			this.recipients.Clear();
		}

		// Token: 0x06003C28 RID: 15400 RVA: 0x000F73BE File Offset: 0x000F55BE
		internal void CopyDataToItem(ICoreItem coreItem)
		{
			this.CopyDataToItem(coreItem, false);
		}

		// Token: 0x06003C29 RID: 15401 RVA: 0x000F73C8 File Offset: 0x000F55C8
		internal void CopyDataToItem(ICoreItem coreItem, bool importResourceFromTnef)
		{
			base.ReplyTo.Resync(true);
			foreach (ConversionItemParticipants.ParticipantDefinitionEntry participantDefinitionEntry in ConversionItemParticipants.ParticipantEntries)
			{
				CoreObject.GetPersistablePropertyBag(coreItem).SetOrDeleteProperty(participantDefinitionEntry.ParticipantProperty, this.propertyBag.TryGetProperty(participantDefinitionEntry.ParticipantProperty));
			}
			CoreObject.GetPersistablePropertyBag(coreItem).SetOrDeleteProperty(InternalSchema.MapiReplyToBlob, this.propertyBag.TryGetProperty(InternalSchema.MapiReplyToBlob));
			CoreObject.GetPersistablePropertyBag(coreItem).SetOrDeleteProperty(InternalSchema.MapiReplyToNames, this.propertyBag.TryGetProperty(InternalSchema.MapiReplyToNames));
			if (this.tnefRecipientList != null)
			{
				Dictionary<ConversionRecipientEntry, ConversionRecipientEntry> dictionary = new Dictionary<ConversionRecipientEntry, ConversionRecipientEntry>();
				foreach (ConversionRecipientEntry conversionRecipientEntry in this.tnefRecipientList)
				{
					dictionary[conversionRecipientEntry] = conversionRecipientEntry;
				}
				foreach (ConversionRecipientEntry conversionRecipientEntry2 in this.recipients)
				{
					ConversionRecipientEntry conversionRecipientEntry3 = null;
					if (dictionary.TryGetValue(conversionRecipientEntry2, out conversionRecipientEntry3) && conversionRecipientEntry3 != null)
					{
						conversionRecipientEntry2.CopyDependentProperties(conversionRecipientEntry3);
					}
				}
			}
			foreach (ConversionRecipientEntry conversionRecipientEntry4 in this.recipients)
			{
				if (RecipientItemType.Bcc == conversionRecipientEntry4.RecipientItemType)
				{
					importResourceFromTnef = false;
				}
				this.CopyRecipientToMessage(coreItem, conversionRecipientEntry4);
			}
			if (importResourceFromTnef)
			{
				foreach (ConversionRecipientEntry conversionRecipientEntry5 in this.tnefRecipientList)
				{
					if (RecipientItemType.Bcc == conversionRecipientEntry5.RecipientItemType && (conversionRecipientEntry5.Participant.GetValueOrDefault<bool>(ParticipantSchema.IsRoom, false) || conversionRecipientEntry5.Participant.GetValueOrDefault<bool>(ParticipantSchema.IsResource, false)))
					{
						this.CopyRecipientToMessage(coreItem, conversionRecipientEntry5);
					}
				}
			}
		}

		// Token: 0x06003C2A RID: 15402 RVA: 0x000F75E0 File Offset: 0x000F57E0
		private void CopyRecipientToMessage(ICoreItem coreItem, ConversionRecipientEntry entry)
		{
			CoreRecipient coreRecipient = coreItem.Recipients.CreateCoreRecipient(new CoreRecipient.SetDefaultPropertiesDelegate(Recipient.SetDefaultRecipientProperties), entry.Participant);
			coreRecipient.RecipientItemType = entry.RecipientItemType;
			foreach (NativeStorePropertyDefinition nativeStorePropertyDefinition in entry.AllNativeProperties)
			{
				object obj = entry.TryGetProperty(nativeStorePropertyDefinition);
				coreRecipient.PropertyBag.TryGetProperty(nativeStorePropertyDefinition);
				if (!(obj is PropertyError) && !RecipientBase.ImmutableProperties.Contains(nativeStorePropertyDefinition))
				{
					coreRecipient.PropertyBag.SetProperty(nativeStorePropertyDefinition, obj);
				}
			}
			coreRecipient.InternalUpdateParticipant(entry.Participant);
		}

		// Token: 0x06003C2B RID: 15403 RVA: 0x000F7694 File Offset: 0x000F5894
		protected override bool CanResolveParticipant(Participant participant)
		{
			return this.IsResolvingAllParticipants;
		}

		// Token: 0x17001258 RID: 4696
		// (get) Token: 0x06003C2C RID: 15404 RVA: 0x000F769C File Offset: 0x000F589C
		public bool IsResolvingAllParticipants
		{
			get
			{
				if (this.mimeMessageLevel != MimeMessageLevel.AttachedMessage)
				{
					return this.inboundOptions.IsSenderTrusted;
				}
				return !this.inboundOptions.ApplyTrustToAttachedMessages || this.inboundOptions.IsSenderTrusted;
			}
		}

		// Token: 0x17001259 RID: 4697
		// (get) Token: 0x06003C2D RID: 15405 RVA: 0x000F76CD File Offset: 0x000F58CD
		protected override string TargetResolutionType
		{
			get
			{
				return "EX";
			}
		}

		// Token: 0x1700125A RID: 4698
		// (get) Token: 0x06003C2E RID: 15406 RVA: 0x000F76D4 File Offset: 0x000F58D4
		internal InboundConversionOptions Options
		{
			get
			{
				return this.inboundOptions;
			}
		}

		// Token: 0x1700125B RID: 4699
		// (get) Token: 0x06003C2F RID: 15407 RVA: 0x000F76DC File Offset: 0x000F58DC
		internal IList<ConversionRecipientEntry> Recipients
		{
			get
			{
				return new ReadOnlyCollection<ConversionRecipientEntry>(this.recipients);
			}
		}

		// Token: 0x06003C30 RID: 15408 RVA: 0x000F76EC File Offset: 0x000F58EC
		private bool ReplaceMatchingEntries(IConversionParticipantList primary, IConversionParticipantList secondary)
		{
			if (primary.Count != secondary.Count)
			{
				return false;
			}
			bool result = false;
			for (int i = 0; i < primary.Count; i++)
			{
				if ((this.CanResolveParticipant(primary[i]) || primary.IsConversionParticipantAlwaysResolvable(i)) && primary[i].AreAddressesEqual(secondary[i]))
				{
					primary[i] = secondary[i];
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06003C31 RID: 15409 RVA: 0x000F775C File Offset: 0x000F595C
		protected override IADRecipientCache GetRecipientCache(int count)
		{
			IADRecipientCache iadrecipientCache = this.inboundOptions.RecipientCache;
			if (iadrecipientCache == null)
			{
				IRecipientSession recipientSession = this.inboundOptions.UserADSession;
				if (recipientSession == null)
				{
					try
					{
						recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 1431, "GetRecipientCache", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ContentConversion\\ConversionAddressCache.cs");
					}
					catch (ExAssertException)
					{
						throw new ArgumentException(InboundConversionOptions.NoScopedTenantInfoNotice);
					}
				}
				iadrecipientCache = new ADRecipientCache<ADRawEntry>(recipientSession, Util.CollectionToArray<ADPropertyDefinition>(ParticipantSchema.SupportedADProperties), count);
			}
			return iadrecipientCache;
		}

		// Token: 0x04002002 RID: 8194
		private InboundConversionOptions inboundOptions;

		// Token: 0x04002003 RID: 8195
		private MimeMessageLevel mimeMessageLevel;

		// Token: 0x04002004 RID: 8196
		private ConversionRecipientList tnefRecipientList;
	}
}
