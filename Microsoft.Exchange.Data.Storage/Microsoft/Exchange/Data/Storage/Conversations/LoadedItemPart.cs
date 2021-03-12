﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x02000F66 RID: 3942
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LoadedItemPart : ItemPart
	{
		// Token: 0x060086DE RID: 34526 RVA: 0x0024FA00 File Offset: 0x0024DC00
		private void InitializeLoadedItemPart(IItem item, IStorePropertyBag propertyBag, BodyFragmentInfo bodyFragmentInfo, bool didLoadSucceed, long bytesLoaded, AttachmentCollection attachmentCollection)
		{
			this.bodyFragmentInfo = bodyFragmentInfo;
			if (this.bodyFragmentInfo == null)
			{
				base.UniqueFragmentInfo = (base.DisclaimerFragmentInfo = FragmentInfo.Empty);
			}
			this.didLoadSucceed = didLoadSucceed;
			if (this.didLoadSucceed)
			{
				this.bytesLoaded = bytesLoaded;
			}
			base.ItemId = item.Id.ObjectId;
			base.Subject = (item.TryGetProperty(ItemSchema.Subject) as string);
			if (attachmentCollection != null)
			{
				foreach (AttachmentHandle handle in attachmentCollection)
				{
					using (Attachment attachment = attachmentCollection.Open(handle, null))
					{
						base.RawAttachments.Add(new AttachmentInfo(item.Id.ObjectId, attachment));
					}
				}
			}
			IMessageItem messageItem = item as IMessageItem;
			if (messageItem != null)
			{
				if (messageItem.Sender != null)
				{
					this.displayNameToParticipant[messageItem.Sender.DisplayName] = messageItem.Sender;
				}
				if (messageItem.From != null)
				{
					this.displayNameToParticipant[messageItem.From.DisplayName] = messageItem.From;
				}
				foreach (Recipient recipient in messageItem.Recipients)
				{
					recipient.Participant.Submitted = recipient.Submitted;
					this.displayNameToParticipant[recipient.Participant.DisplayName] = recipient.Participant;
					base.Recipients.Add(recipient.RecipientItemType, new IParticipant[]
					{
						recipient.Participant
					});
				}
				foreach (Participant participant in messageItem.ReplyTo)
				{
					this.displayNameToParticipant[participant.DisplayName] = participant;
				}
			}
			if (propertyBag != null)
			{
				base.StorePropertyBag = propertyBag;
			}
		}

		// Token: 0x170023B8 RID: 9144
		// (get) Token: 0x060086DF RID: 34527 RVA: 0x0024FC34 File Offset: 0x0024DE34
		public override bool DidLoadSucceed
		{
			get
			{
				return this.didLoadSucceed;
			}
		}

		// Token: 0x170023B9 RID: 9145
		// (get) Token: 0x060086E0 RID: 34528 RVA: 0x0024FC3C File Offset: 0x0024DE3C
		public override bool IsExtractedPart
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170023BA RID: 9146
		// (get) Token: 0x060086E1 RID: 34529 RVA: 0x0024FC40 File Offset: 0x0024DE40
		public override IList<IParticipant> ReplyToParticipants
		{
			get
			{
				if (this.replyToParticipants == null)
				{
					ReplyTo replyTo = ReplyTo.CreateInstance(base.StorePropertyBag);
					if (replyTo == null)
					{
						this.replyToParticipants = Array<IParticipant>.Empty;
					}
					else
					{
						this.replyToParticipants = replyTo.Cast<IParticipant>().ToList<IParticipant>();
					}
				}
				return this.replyToParticipants;
			}
		}

		// Token: 0x170023BB RID: 9147
		// (get) Token: 0x060086E2 RID: 34530 RVA: 0x0024FC88 File Offset: 0x0024DE88
		public ParticipantTable ReplyAllParticipants
		{
			get
			{
				if (this.replyAllParticipants == null)
				{
					this.replyAllParticipants = this.CalculateReplyAllParticipants();
				}
				return this.replyAllParticipants;
			}
		}

		// Token: 0x170023BC RID: 9148
		// (get) Token: 0x060086E3 RID: 34531 RVA: 0x0024FCA4 File Offset: 0x0024DEA4
		internal BodyFragmentInfo BodyFragmentInfo
		{
			get
			{
				return this.bodyFragmentInfo;
			}
		}

		// Token: 0x170023BD RID: 9149
		// (get) Token: 0x060086E4 RID: 34532 RVA: 0x0024FCAC File Offset: 0x0024DEAC
		internal long BytesLoaded
		{
			get
			{
				return this.bytesLoaded;
			}
		}

		// Token: 0x170023BE RID: 9150
		// (get) Token: 0x060086E5 RID: 34533 RVA: 0x0024FCB4 File Offset: 0x0024DEB4
		internal Dictionary<string, Participant> DisplayNameToParticipant
		{
			get
			{
				return this.displayNameToParticipant;
			}
		}

		// Token: 0x060086E6 RID: 34534 RVA: 0x0024FCBC File Offset: 0x0024DEBC
		internal void AddBodySummary(object bodySummary)
		{
			QueryResultPropertyBag queryResultPropertyBag = new QueryResultPropertyBag(base.StorePropertyBag, new NativeStorePropertyDefinition[]
			{
				InternalSchema.TextBody,
				InternalSchema.Preview
			}, new object[]
			{
				bodySummary,
				bodySummary
			});
			base.StorePropertyBag = queryResultPropertyBag.AsIStorePropertyBag();
		}

		// Token: 0x060086E7 RID: 34535 RVA: 0x0024FD0C File Offset: 0x0024DF0C
		private ParticipantTable CalculateReplyAllParticipants()
		{
			IParticipant valueOrDefault = base.StorePropertyBag.GetValueOrDefault<IParticipant>(ItemSchema.From, null);
			IParticipant valueOrDefault2 = base.StorePropertyBag.GetValueOrDefault<IParticipant>(ItemSchema.Sender, null);
			IDictionary<RecipientItemType, HashSet<IParticipant>> dictionary = ReplyAllParticipantsRepresentationProperty<IParticipant>.BuildReplyAllRecipients<IParticipant>(valueOrDefault2, valueOrDefault, this.ReplyToParticipants, base.Recipients.ToDictionary(), ParticipantComparer.EmailAddress);
			ParticipantTable participantTable = new ParticipantTable();
			foreach (KeyValuePair<RecipientItemType, HashSet<IParticipant>> keyValuePair in dictionary)
			{
				participantTable.Add(keyValuePair.Key, keyValuePair.Value);
			}
			return participantTable;
		}

		// Token: 0x060086E8 RID: 34536 RVA: 0x0024FDB0 File Offset: 0x0024DFB0
		internal LoadedItemPart(IItem item, IStorePropertyBag propertyBag, BodyFragmentInfo bodyFragmentInfo, PropertyDefinition[] loadedProperties, ItemPartIrmInfo irmInfo, bool didLoadSucceed, long bytesLoaded, AttachmentCollection attachmentCollection) : base(loadedProperties)
		{
			this.irmInfo = irmInfo;
			this.InitializeLoadedItemPart(item, propertyBag, bodyFragmentInfo, didLoadSucceed, bytesLoaded, attachmentCollection);
		}

		// Token: 0x170023BF RID: 9151
		// (get) Token: 0x060086E9 RID: 34537 RVA: 0x0024FDDC File Offset: 0x0024DFDC
		public override ItemPartIrmInfo IrmInfo
		{
			get
			{
				return this.irmInfo;
			}
		}

		// Token: 0x04005A29 RID: 23081
		private Dictionary<string, Participant> displayNameToParticipant = new Dictionary<string, Participant>();

		// Token: 0x04005A2A RID: 23082
		private BodyFragmentInfo bodyFragmentInfo;

		// Token: 0x04005A2B RID: 23083
		private bool didLoadSucceed;

		// Token: 0x04005A2C RID: 23084
		private ParticipantTable replyAllParticipants;

		// Token: 0x04005A2D RID: 23085
		private long bytesLoaded;

		// Token: 0x04005A2E RID: 23086
		private IList<IParticipant> replyToParticipants;

		// Token: 0x04005A2F RID: 23087
		private ItemPartIrmInfo irmInfo;
	}
}
