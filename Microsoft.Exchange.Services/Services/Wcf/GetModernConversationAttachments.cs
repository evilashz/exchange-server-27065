using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Conversations.Repositories;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200091F RID: 2335
	internal sealed class GetModernConversationAttachments : MultiStepServiceCommand<GetModernConversationAttachmentsRequest, ModernConversationAttachmentsResponseType>
	{
		// Token: 0x060043A0 RID: 17312 RVA: 0x000E54D3 File Offset: 0x000E36D3
		public GetModernConversationAttachments(CallContext callContext, GetModernConversationAttachmentsRequest request) : base(callContext, request)
		{
		}

		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x060043A1 RID: 17313 RVA: 0x000E54DD File Offset: 0x000E36DD
		internal override int StepCount
		{
			get
			{
				return base.Request.Conversations.Length;
			}
		}

		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x060043A2 RID: 17314 RVA: 0x000E54EC File Offset: 0x000E36EC
		private static PropertyDefinition[] MandatoryPropertiesToLoad
		{
			get
			{
				if (GetModernConversationAttachments.mandatoryPropertiesToLoad == null)
				{
					GetModernConversationAttachments.mandatoryPropertiesToLoad = new List<PropertyDefinition>(ConversationLoaderHelper.MandatoryConversationPropertiesToLoad)
					{
						MessageItemSchema.ReplyToBlobExists,
						MessageItemSchema.ReplyToNamesExists
					}.ToArray();
				}
				return GetModernConversationAttachments.mandatoryPropertiesToLoad;
			}
		}

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x060043A3 RID: 17315 RVA: 0x000E5534 File Offset: 0x000E3734
		private static PropertyDefinition[] PropertiesToLoad
		{
			get
			{
				if (GetModernConversationAttachments.propertiesToLoad == null)
				{
					List<PropertyDefinition> list = new List<PropertyDefinition>(GetModernConversationAttachments.MandatoryPropertiesToLoad);
					list.AddRange(ConversationLoaderHelper.NonMandatoryPropertiesToLoad);
					list.AddRange(ConversationLoaderHelper.InReplyToPropertiesToLoad);
					GetModernConversationAttachments.propertiesToLoad = list.ToArray();
				}
				return GetModernConversationAttachments.propertiesToLoad;
			}
		}

		// Token: 0x060043A4 RID: 17316 RVA: 0x000E557C File Offset: 0x000E377C
		private ItemResponseShape GetResponseShape()
		{
			return new ItemResponseShape
			{
				AdditionalProperties = new List<PropertyPath>
				{
					new PropertyUri(PropertyUriEnum.Attachments),
					new PropertyUri(PropertyUriEnum.From)
				}.ToArray(),
				ClientSupportsIrm = base.Request.ClientSupportsIrm
			};
		}

		// Token: 0x060043A5 RID: 17317 RVA: 0x000E55CD File Offset: 0x000E37CD
		internal override void PreExecuteCommand()
		{
			base.PreExecuteCommand();
			this.itemResponseShape = this.GetResponseShape();
		}

		// Token: 0x060043A6 RID: 17318 RVA: 0x000E55E4 File Offset: 0x000E37E4
		internal override IExchangeWebMethodResponse GetResponse()
		{
			GetModernConversationAttachmentsResponse getModernConversationAttachmentsResponse = new GetModernConversationAttachmentsResponse();
			getModernConversationAttachmentsResponse.BuildForResults<ModernConversationAttachmentsResponseType>(base.Results);
			return getModernConversationAttachmentsResponse;
		}

		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x060043A7 RID: 17319 RVA: 0x000E5604 File Offset: 0x000E3804
		internal ConversationRequestType CurrentConversationRequest
		{
			get
			{
				return base.Request.Conversations[base.CurrentStep];
			}
		}

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x060043A8 RID: 17320 RVA: 0x000E5618 File Offset: 0x000E3818
		private ItemResponseShape ItemResponseShape
		{
			get
			{
				if (this.itemResponseShape == null)
				{
					this.itemResponseShape = this.GetResponseShape();
				}
				return this.itemResponseShape;
			}
		}

		// Token: 0x060043A9 RID: 17321 RVA: 0x000E5634 File Offset: 0x000E3834
		protected override IParticipantResolver ConstructParticipantResolver()
		{
			return Microsoft.Exchange.Services.Core.Types.ParticipantResolver.Create(base.CallContext, this.ItemResponseShape.MaximumRecipientsToReturn);
		}

		// Token: 0x060043AA RID: 17322 RVA: 0x000E56B8 File Offset: 0x000E38B8
		internal override ServiceResult<ModernConversationAttachmentsResponseType> Execute()
		{
			ServiceResult<ModernConversationAttachmentsResponseType> serviceResult = null;
			GrayException.MapAndReportGrayExceptions(delegate()
			{
				ConversationRequestType currentConversationRequest = this.CurrentConversationRequest;
				IdAndSession sessionFromConversationId = XsoConversationRepositoryExtensions.GetSessionFromConversationId(this.IdConverter, currentConversationRequest.ConversationId, MailboxSearchLocation.PrimaryOnly);
				ConversationId conversationId = sessionFromConversationId.Id as ConversationId;
				MailboxSession mailboxSession = (MailboxSession)sessionFromConversationId.Session;
				serviceResult = this.ExtractAttachmentsFromConversation(conversationId, currentConversationRequest.SyncState, mailboxSession);
			}, new GrayException.IsGrayExceptionDelegate(GrayException.IsSystemGrayException));
			return serviceResult;
		}

		// Token: 0x060043AB RID: 17323 RVA: 0x000E56FC File Offset: 0x000E38FC
		private ServiceResult<ModernConversationAttachmentsResponseType> ExtractAttachmentsFromConversation(ConversationId conversationId, byte[] currentSyncState, MailboxSession mailboxSession)
		{
			ServiceResult<ModernConversationAttachmentsResponseType> result;
			try
			{
				XsoConversationRepository<Conversation> xsoConversationRepository = new XsoConversationRepository<Conversation>(this.ItemResponseShape, GetModernConversationAttachments.PropertiesToLoad, base.IdConverter, new ConversationFactory(mailboxSession), base.ParticipantResolver);
				ICoreConversation coreConversation = xsoConversationRepository.Load(conversationId, mailboxSession, base.Request.FoldersToIgnore, true, false, new PropertyDefinition[0]);
				if (coreConversation == null || coreConversation.ConversationTree == null || coreConversation.ConversationTree.Count == 0)
				{
					throw new ObjectNotFoundException(ServerStrings.ExItemNotFound);
				}
				EWSSettings.CurrentConversation = coreConversation;
				List<IConversationTreeNode> onlyNodesWithAttachments = this.GetOnlyNodesWithAttachments(XsoConversationRepositoryExtensions.GetTreeNodes(coreConversation, currentSyncState));
				if (onlyNodesWithAttachments.Count > 0)
				{
					coreConversation.OnBeforeItemLoad += this.ConversationOnBeforeItemLoad;
					this.LoadItemParts(coreConversation, onlyNodesWithAttachments, mailboxSession, xsoConversationRepository);
				}
				List<IConversationTreeNode> onlyNodesWithAttachments2 = this.GetOnlyNodesWithAttachments(coreConversation.ConversationTree.ToList<IConversationTreeNode>());
				List<ItemType> list;
				if (onlyNodesWithAttachments2.Count > 0)
				{
					list = this.BuildAttachmentItems(coreConversation, mailboxSession, onlyNodesWithAttachments, onlyNodesWithAttachments2);
				}
				else
				{
					list = new List<ItemType>(0);
				}
				ModernConversationAttachmentsResponseType value = new ModernConversationAttachmentsResponseType
				{
					ConversationId = this.CurrentConversationRequest.ConversationId,
					SyncState = coreConversation.SerializedTreeState,
					ItemsWithAttachments = list.ToArray()
				};
				result = new ServiceResult<ModernConversationAttachmentsResponseType>(value);
			}
			finally
			{
				EWSSettings.CurrentConversation = null;
			}
			return result;
		}

		// Token: 0x060043AC RID: 17324 RVA: 0x000E5840 File Offset: 0x000E3A40
		private void LoadItemParts(ICoreConversation conversation, List<IConversationTreeNode> relevantNodes, MailboxSession mailboxSession, XsoConversationRepository<Conversation> conversationLoader)
		{
			List<StoreObjectId> listStoreObjectIds = XsoConversationRepositoryExtensions.GetListStoreObjectIds(relevantNodes);
			this.PrefetchItems(mailboxSession, listStoreObjectIds);
			conversationLoader.LoadItemParts(conversation, relevantNodes, true);
		}

		// Token: 0x060043AD RID: 17325 RVA: 0x000E5866 File Offset: 0x000E3A66
		private void PrefetchItems(MailboxSession mailboxSession, List<StoreObjectId> itemIds)
		{
			mailboxSession.PrereadMessages(itemIds.ToArray());
		}

		// Token: 0x060043AE RID: 17326 RVA: 0x000E5874 File Offset: 0x000E3A74
		private List<IConversationTreeNode> GetOnlyNodesWithAttachments(IEnumerable<IConversationTreeNode> relevantNodes)
		{
			List<IConversationTreeNode> list = new List<IConversationTreeNode>();
			foreach (IConversationTreeNode conversationTreeNode in relevantNodes)
			{
				if (conversationTreeNode.HasAttachments)
				{
					list.Add(conversationTreeNode);
				}
			}
			return list;
		}

		// Token: 0x060043AF RID: 17327 RVA: 0x000E58CC File Offset: 0x000E3ACC
		private List<ItemType> BuildAttachmentItems(ICoreConversation conversation, MailboxSession mailboxSession, List<IConversationTreeNode> nodesToLoad, List<IConversationTreeNode> nodesWithAttachments)
		{
			List<ItemType> list = new List<ItemType>();
			HashSet<IConversationTreeNode> hashSet = new HashSet<IConversationTreeNode>(nodesToLoad);
			base.ParticipantResolver.LoadAdDataIfNeeded(conversation.AllParticipants(nodesToLoad));
			ConversationTreeNodeBase.SortByDate(nodesWithAttachments);
			foreach (IConversationTreeNode conversationTreeNode in nodesWithAttachments)
			{
				list.AddRange(this.CreateItemsFromTreeNode(conversationTreeNode, conversation, mailboxSession, hashSet.Contains(conversationTreeNode)));
			}
			return list;
		}

		// Token: 0x060043B0 RID: 17328 RVA: 0x000E5954 File Offset: 0x000E3B54
		public ItemType[] CreateItemsFromTreeNode(IConversationTreeNode treeNode, ICoreConversation conversation, MailboxSession mailboxSession, bool getItemPart)
		{
			List<ItemType> list = new List<ItemType>(treeNode.StorePropertyBags.Count);
			foreach (IStorePropertyBag storePropertyBag in treeNode.StorePropertyBags)
			{
				VersionedId versionedId = (VersionedId)storePropertyBag.TryGetProperty(ItemSchema.Id);
				StoreObjectId objectId = versionedId.ObjectId;
				IdAndSession itemIdAndSession = new IdAndSession(objectId, mailboxSession);
				ItemType itemType = ItemType.CreateFromStoreObjectType(objectId.ObjectType);
				itemType.ItemClass = storePropertyBag.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, string.Empty);
				itemType.ItemId = IdConverter.GetItemIdFromStoreId(objectId, new MailboxId(mailboxSession));
				if (getItemPart)
				{
					ItemPart itemPart = conversation.GetItemPart(objectId);
					itemType.PropertyBag[ItemSchema.DateTimeSent] = ConversationDataConverter.GetDatetimeProperty(itemPart, ItemSchema.SentTime);
					itemType.PropertyBag[MessageSchema.From] = base.ParticipantResolver.ResolveToSingleRecipientType(itemPart.StorePropertyBag.GetValueOrDefault<IParticipant>(ItemSchema.From, null));
					itemType.HasAttachments = new bool?(itemPart.StorePropertyBag.GetValueOrDefault<bool>(ItemSchema.HasAttachment, false));
					itemType.Attachments = ConversationDataConverter.GetAttachments(itemPart, itemIdAndSession);
				}
				else
				{
					itemType.HasAttachments = new bool?(true);
				}
				if (itemType is MessageType)
				{
					list.Add(itemType);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060043B1 RID: 17329 RVA: 0x000E5ACC File Offset: 0x000E3CCC
		private void ConversationOnBeforeItemLoad(object sender, LoadItemEventArgs eventArgs)
		{
			eventArgs.HtmlStreamOptionCallback = null;
			eventArgs.MessagePropertyDefinitions = new List<PropertyDefinition>
			{
				MessageItemSchema.ReplyToBlob,
				MessageItemSchema.ReplyToNames
			}.ToArray<PropertyDefinition>();
			eventArgs.OpportunisticLoadPropertyDefinitions = null;
		}

		// Token: 0x0400277E RID: 10110
		private static PropertyDefinition[] propertiesToLoad;

		// Token: 0x0400277F RID: 10111
		private static PropertyDefinition[] mandatoryPropertiesToLoad;

		// Token: 0x04002780 RID: 10112
		private ItemResponseShape itemResponseShape;
	}
}
