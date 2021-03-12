using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x02000F67 RID: 3943
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversationAggregatorFactory
	{
		// Token: 0x060086EA RID: 34538 RVA: 0x0024FDE4 File Offset: 0x0024DFE4
		public ConversationAggregatorFactory(IMailboxSession session, IMailboxOwner mailboxOwner, IXSOFactory xsoFactory, ConversationIndexTrackingEx indexTrackingEx)
		{
			this.mailboxOwner = mailboxOwner;
			this.session = session;
			this.xsoFactory = xsoFactory;
			this.indexTrackingEx = indexTrackingEx;
		}

		// Token: 0x060086EB RID: 34539 RVA: 0x0024FE0C File Offset: 0x0024E00C
		public static bool TryInstantiateAggregatorForSave(IMailboxSession session, CoreItemOperation saveOperation, ICoreItem item, ConversationIndexTrackingEx indexTrackingEx, out IConversationAggregator aggregator)
		{
			MailboxOwnerFactory mailboxOwnerFactory = new MailboxOwnerFactory(session);
			ConversationAggregatorFactory conversationAggregatorFactory = new ConversationAggregatorFactory(session, mailboxOwnerFactory.Create(), XSOFactory.Default, indexTrackingEx);
			return conversationAggregatorFactory.TryInstantiateAggregatorForSave(saveOperation, item, out aggregator);
		}

		// Token: 0x060086EC RID: 34540 RVA: 0x0024FE40 File Offset: 0x0024E040
		public static bool TryInstantiateAggregatorForDelivery(IMailboxSession session, MiniRecipient miniRecipient, ConversationIndexTrackingEx indexTrackingEx, out IConversationAggregator aggregator)
		{
			MailboxOwnerFactory mailboxOwnerFactory = new MailboxOwnerFactory(session);
			return ConversationAggregatorFactory.TryInstantiateAggregatorForDelivery(session, mailboxOwnerFactory.Create(miniRecipient), indexTrackingEx, out aggregator);
		}

		// Token: 0x060086ED RID: 34541 RVA: 0x0024FE64 File Offset: 0x0024E064
		public static bool TryInstantiateAggregatorForDelivery(IMailboxSession session, IMailboxOwner mailboxOwner, ConversationIndexTrackingEx indexTrackingEx, out IConversationAggregator aggregator)
		{
			ConversationAggregatorFactory conversationAggregatorFactory = new ConversationAggregatorFactory(session, mailboxOwner, XSOFactory.Default, indexTrackingEx);
			return conversationAggregatorFactory.TryInstantiateAggregatorForDelivery(out aggregator);
		}

		// Token: 0x060086EE RID: 34542 RVA: 0x0024FE88 File Offset: 0x0024E088
		public bool TryInstantiateAggregatorForSave(CoreItemOperation saveOperation, ICoreItem item, out IConversationAggregator aggregator)
		{
			ConversationAggregatorFactory.AggregationApproach approach = this.IdentifyAggregationApproachForSave(saveOperation, item);
			return this.TryInstantiateAggregator(approach, false, out aggregator);
		}

		// Token: 0x060086EF RID: 34543 RVA: 0x0024FEA8 File Offset: 0x0024E0A8
		public bool TryInstantiateAggregatorForDelivery(out IConversationAggregator aggregator)
		{
			ConversationAggregatorFactory.AggregationApproach approach = this.IdentifyAggregationApproachForDelivery();
			return this.TryInstantiateAggregator(approach, true, out aggregator);
		}

		// Token: 0x060086F0 RID: 34544 RVA: 0x0024FEC8 File Offset: 0x0024E0C8
		private bool TryInstantiateAggregator(ConversationAggregatorFactory.AggregationApproach approach, bool scenarioSupportsSearchForDuplicatedMessages, out IConversationAggregator aggregator)
		{
			aggregator = null;
			if (approach == ConversationAggregatorFactory.AggregationApproach.NoOp)
			{
				return false;
			}
			IAggregationByItemClassReferencesSubjectProcessor aggregationByItemClassReferencesSubjectProcessor = AggregationByItemClassReferencesSubjectProcessor.CreateInstance(this.xsoFactory, this.session, this.mailboxOwner.RequestExtraPropertiesWhenSearching, this.indexTrackingEx);
			if (approach == ConversationAggregatorFactory.AggregationApproach.SameConversation)
			{
				aggregator = new SameConversationAggregator(aggregationByItemClassReferencesSubjectProcessor);
				return true;
			}
			ConversationAggregationDiagnosticsFrame diagnosticsFrame = new ConversationAggregationDiagnosticsFrame(this.session);
			AggregationByParticipantProcessor participantProcessor = AggregationByParticipantProcessor.CreateInstance(this.session, this.xsoFactory);
			switch (approach)
			{
			case ConversationAggregatorFactory.AggregationApproach.SideConversation:
				aggregator = new SideConversationAggregator(this.mailboxOwner, scenarioSupportsSearchForDuplicatedMessages, aggregationByItemClassReferencesSubjectProcessor, participantProcessor, diagnosticsFrame);
				return true;
			case ConversationAggregatorFactory.AggregationApproach.ThreadedConversation:
				aggregator = new ThreadedConversationAggregator(this.mailboxOwner, scenarioSupportsSearchForDuplicatedMessages, aggregationByItemClassReferencesSubjectProcessor, participantProcessor, diagnosticsFrame);
				return true;
			default:
				return false;
			}
		}

		// Token: 0x060086F1 RID: 34545 RVA: 0x0024FF68 File Offset: 0x0024E168
		private ConversationAggregatorFactory.AggregationApproach IdentifyAggregationApproachForSave(CoreItemOperation saveOperation, ICoreItem item)
		{
			if (this.session.LogonType == LogonType.Transport)
			{
				return ConversationAggregatorFactory.AggregationApproach.NoOp;
			}
			ICorePropertyBag propertyBag = item.PropertyBag;
			string valueOrDefault = propertyBag.GetValueOrDefault<string>(InternalSchema.ItemClass, string.Empty);
			if (string.IsNullOrEmpty(valueOrDefault) || ConversationIdFromIndexProperty.CheckExclusionList(valueOrDefault) || propertyBag.GetValueOrDefault<bool>(InternalSchema.IsAssociated))
			{
				return ConversationAggregatorFactory.AggregationApproach.NoOp;
			}
			if (this.mailboxOwner.ThreadedConversationProcessingEnabled || this.mailboxOwner.SideConversationProcessingEnabled)
			{
				if (saveOperation == CoreItemOperation.Send || (item.Origin == Origin.New && !ConversationIndex.WasMessageEverProcessed(propertyBag)))
				{
					if (!this.mailboxOwner.ThreadedConversationProcessingEnabled)
					{
						return ConversationAggregatorFactory.AggregationApproach.SideConversation;
					}
					return ConversationAggregatorFactory.AggregationApproach.ThreadedConversation;
				}
				else
				{
					if (this.mailboxOwner.IsGroupMailbox && !propertyBag.GetValueOrDefault<bool>(InternalSchema.IsDraft))
					{
						return ConversationAggregatorFactory.AggregationApproach.SideConversation;
					}
					return ConversationAggregatorFactory.AggregationApproach.NoOp;
				}
			}
			else
			{
				if (item.Origin == Origin.New && !ConversationIndex.WasMessageEverProcessed(propertyBag))
				{
					return ConversationAggregatorFactory.AggregationApproach.SameConversation;
				}
				return ConversationAggregatorFactory.AggregationApproach.NoOp;
			}
		}

		// Token: 0x060086F2 RID: 34546 RVA: 0x00250030 File Offset: 0x0024E230
		private ConversationAggregatorFactory.AggregationApproach IdentifyAggregationApproachForDelivery()
		{
			if (this.mailboxOwner.ThreadedConversationProcessingEnabled)
			{
				return ConversationAggregatorFactory.AggregationApproach.ThreadedConversation;
			}
			if (this.mailboxOwner.SideConversationProcessingEnabled)
			{
				return ConversationAggregatorFactory.AggregationApproach.SideConversation;
			}
			return ConversationAggregatorFactory.AggregationApproach.SameConversation;
		}

		// Token: 0x04005A30 RID: 23088
		private readonly IMailboxOwner mailboxOwner;

		// Token: 0x04005A31 RID: 23089
		private readonly IMailboxSession session;

		// Token: 0x04005A32 RID: 23090
		private readonly IXSOFactory xsoFactory;

		// Token: 0x04005A33 RID: 23091
		private readonly ConversationIndexTrackingEx indexTrackingEx;

		// Token: 0x02000F68 RID: 3944
		private enum AggregationApproach
		{
			// Token: 0x04005A35 RID: 23093
			NoOp,
			// Token: 0x04005A36 RID: 23094
			SameConversation,
			// Token: 0x04005A37 RID: 23095
			SideConversation,
			// Token: 0x04005A38 RID: 23096
			ThreadedConversation
		}
	}
}
