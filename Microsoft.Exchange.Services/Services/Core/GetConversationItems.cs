using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Conversations.Repositories;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Core.Types.Conversations;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000301 RID: 769
	internal sealed class GetConversationItems : MultiStepServiceCommand<GetConversationItemsRequest, Microsoft.Exchange.Services.Core.Types.Conversations.ConversationResponseType>
	{
		// Token: 0x060015B1 RID: 5553 RVA: 0x00070B54 File Offset: 0x0006ED54
		public GetConversationItems(CallContext callContext, GetConversationItemsRequest request) : base(callContext, request)
		{
			this.foldersToIgnore = base.Request.FoldersToIgnore;
			this.conversations = base.Request.Conversations;
			this.sortOrder = base.Request.SortOrder;
			this.maxItemsToReturn = base.Request.MaxItemsToReturn;
			this.mailboxScope = base.Request.MailboxScope;
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060015B2 RID: 5554 RVA: 0x00070BC0 File Offset: 0x0006EDC0
		private static PropertyDefinition[] PropertiesToLoad
		{
			get
			{
				if (GetConversationItems.propertiesToLoad == null)
				{
					List<PropertyDefinition> list = new List<PropertyDefinition>(ConversationLoaderHelper.MandatoryConversationPropertiesToLoad);
					list.AddRange(ConversationLoaderHelper.NonMandatoryPropertiesToLoad);
					GetConversationItems.propertiesToLoad = list.ToArray();
				}
				return GetConversationItems.propertiesToLoad;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060015B3 RID: 5555 RVA: 0x00070BFA File Offset: 0x0006EDFA
		private static PropertyDefinition[] InferenceEnabledPropertiesToLoad
		{
			get
			{
				if (GetConversationItems.inferenceEnabledPropertiesToLoad == null)
				{
					GetConversationItems.inferenceEnabledPropertiesToLoad = ConversationLoaderHelper.CalculateInferenceEnabledPropertiesToLoad(GetConversationItems.PropertiesToLoad);
				}
				return GetConversationItems.inferenceEnabledPropertiesToLoad;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060015B4 RID: 5556 RVA: 0x00070C17 File Offset: 0x0006EE17
		internal override int StepCount
		{
			get
			{
				return this.conversations.Length;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060015B5 RID: 5557 RVA: 0x00070C21 File Offset: 0x0006EE21
		internal Microsoft.Exchange.Services.Core.Types.ConversationRequestType CurrentConversationRequest
		{
			get
			{
				return this.conversations[base.CurrentStep];
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060015B6 RID: 5558 RVA: 0x00070C30 File Offset: 0x0006EE30
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

		// Token: 0x060015B7 RID: 5559 RVA: 0x00070C4C File Offset: 0x0006EE4C
		internal override void PreExecuteCommand()
		{
			base.PreExecuteCommand();
			if (this.maxItemsToReturn < 0 || this.maxItemsToReturn > 100)
			{
				throw new ServiceInvalidOperationException(CoreResources.IDs.InvalidMaxItemsToReturn);
			}
			this.conversationStatisticsLogger = new ConversationStatisticsLogger(base.CallContext.ProtocolLog);
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x00070C98 File Offset: 0x0006EE98
		internal override IExchangeWebMethodResponse GetResponse()
		{
			GetConversationItemsResponse getConversationItemsResponse = new GetConversationItemsResponse();
			getConversationItemsResponse.BuildForResults<Microsoft.Exchange.Services.Core.Types.Conversations.ConversationResponseType>(base.Results);
			return getConversationItemsResponse;
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x00070E80 File Offset: 0x0006F080
		internal override ServiceResult<Microsoft.Exchange.Services.Core.Types.Conversations.ConversationResponseType> Execute()
		{
			ServiceResult<Microsoft.Exchange.Services.Core.Types.Conversations.ConversationResponseType> serviceResult = null;
			GrayException.MapAndReportGrayExceptions(delegate()
			{
				Microsoft.Exchange.Services.Core.Types.ConversationRequestType currentConversationRequest = this.CurrentConversationRequest;
				this.conversationStatisticsLogger.Log(currentConversationRequest);
				IdAndSession sessionFromConversationId = this.GetSessionFromConversationId(currentConversationRequest.ConversationId);
				ConversationId conversationId = sessionFromConversationId.Id as ConversationId;
				MailboxSession mailboxSession = (MailboxSession)sessionFromConversationId.Session;
				ServiceResult<Microsoft.Exchange.Services.Core.Types.Conversations.ConversationResponseType> serviceResult;
				if (!this.Request.MailboxScopeSpecified || this.Request.MailboxScope != MailboxSearchLocation.All)
				{
					serviceResult = this.LoadAndBuildConversation(conversationId, currentConversationRequest.SyncState, mailboxSession);
					return;
				}
				if (mailboxSession.MailboxOwner.GetArchiveMailbox() == null)
				{
					throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorArchiveMailboxNotEnabled);
				}
				GetConversationItems.ExecuteArchiveGetConversationItemsDelegate executeArchiveGetConversationItemsDelegate = new GetConversationItems.ExecuteArchiveGetConversationItemsDelegate(this.LoadAndBuildArchiveConversation);
				IAsyncResult result = executeArchiveGetConversationItemsDelegate.BeginInvoke(conversationId, currentConversationRequest.SyncState, mailboxSession.MailboxOwner, null, null);
				serviceResult = this.LoadAndBuildConversation(conversationId, currentConversationRequest.SyncState, mailboxSession);
				ServiceResult<Microsoft.Exchange.Services.Core.Types.Conversations.ConversationResponseType> serviceResult2 = executeArchiveGetConversationItemsDelegate.EndInvoke(result);
				if (serviceResult != null && serviceResult.Value != null && serviceResult2 != null && serviceResult2.Value != null)
				{
					ConversationNode[] conversationNodes = serviceResult.Value.ConversationNodes;
					ConversationNode[] conversationNodes2 = serviceResult2.Value.ConversationNodes;
					serviceResult = new ServiceResult<Microsoft.Exchange.Services.Core.Types.Conversations.ConversationResponseType>(new Microsoft.Exchange.Services.Core.Types.Conversations.ConversationResponseType
					{
						ConversationId = serviceResult.Value.ConversationId,
						ConversationNodes = ConversationNodeFactory.MergeConversationNodes(conversationNodes, conversationNodes2, this.sortOrder, this.maxItemsToReturn),
						SyncState = serviceResult.Value.SyncState
					});
					return;
				}
				if (serviceResult != null && serviceResult.Value != null)
				{
					serviceResult = serviceResult;
					return;
				}
				serviceResult = serviceResult2;
			}, new GrayException.IsGrayExceptionDelegate(GrayException.IsSystemGrayException));
			return serviceResult;
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x00070EC4 File Offset: 0x0006F0C4
		internal static bool CanOptimizeAsSingleNodeTree(ICoreConversation conversation, byte[] currentSyncState, ItemResponseShape itemResponseShape, bool returnSubmittedItems)
		{
			return (itemResponseShape.UseSafeHtml || !string.IsNullOrEmpty(itemResponseShape.CssScopeClassName)) && (!conversation.ConversationTree.RootMessageNode.HasBeenSubmitted || returnSubmittedItems) && conversation.ConversationTree.Count == 1 && (currentSyncState == null || currentSyncState.Length == 0);
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x00070F1A File Offset: 0x0006F11A
		private static PropertyDefinition[] GetPropertiesForConversationLoad(ItemResponseShape itemResponseShape)
		{
			if (!itemResponseShape.InferenceEnabled)
			{
				return GetConversationItems.PropertiesToLoad;
			}
			return GetConversationItems.InferenceEnabledPropertiesToLoad;
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x00070F30 File Offset: 0x0006F130
		private static ICollection<IConversationTreeNode> CalculateChangedTreeNodes(IConversationTree conversationTree, List<StoreObjectId> changedItems)
		{
			HashSet<IConversationTreeNode> hashSet = new HashSet<IConversationTreeNode>();
			foreach (StoreObjectId storeObjectId in changedItems)
			{
				IConversationTreeNode item;
				if (conversationTree.TryGetConversationTreeNode(storeObjectId, out item))
				{
					hashSet.Add(item);
				}
			}
			return hashSet;
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x00070F94 File Offset: 0x0006F194
		private static List<StoreObjectId> CalculateItemChanges(ICoreConversation conversation, byte[] syncState)
		{
			KeyValuePair<List<StoreObjectId>, List<StoreObjectId>> keyValuePair = conversation.CalculateChanges(syncState);
			List<StoreObjectId> list = new List<StoreObjectId>();
			list.AddRange(keyValuePair.Key);
			list.AddRange(keyValuePair.Value);
			return list;
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x00070FCA File Offset: 0x0006F1CA
		private ItemResponseShape GetResponseShape()
		{
			return Global.ResponseShapeResolver.GetResponseShape<ItemResponseShape>(base.Request.ShapeName, base.Request.ItemShape, base.CallContext.FeaturesManager);
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x00070FF7 File Offset: 0x0006F1F7
		protected override IParticipantResolver ConstructParticipantResolver()
		{
			return Microsoft.Exchange.Services.Core.Types.ParticipantResolver.Create(base.CallContext, this.ItemResponseShape.MaximumRecipientsToReturn);
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x00071010 File Offset: 0x0006F210
		private ServiceResult<Microsoft.Exchange.Services.Core.Types.Conversations.ConversationResponseType> LoadAndBuildConversation(ConversationId conversationId, byte[] currentSyncState, MailboxSession mailboxSession)
		{
			ServiceResult<Microsoft.Exchange.Services.Core.Types.Conversations.ConversationResponseType> result;
			try
			{
				this.conversationLoader = new XsoConversationRepository<Conversation>(this.ItemResponseShape, GetConversationItems.GetPropertiesForConversationLoad(this.ItemResponseShape), base.IdConverter, new ConversationFactory(mailboxSession), base.CallContext, base.ParticipantResolver);
				Conversation conversation = this.conversationLoader.Load(conversationId, mailboxSession, this.foldersToIgnore, true, true, new PropertyDefinition[0]);
				if (conversation == null)
				{
					throw new ObjectNotFoundException(ServerStrings.ExItemNotFound);
				}
				conversation.TrimToNewest(this.maxItemsToReturn);
				EWSSettings.CurrentConversation = conversation;
				ConversationNode[] conversationNodes;
				if (GetConversationItems.CanOptimizeAsSingleNodeTree(conversation, currentSyncState, this.ItemResponseShape, base.Request.ReturnSubmittedItems))
				{
					ConversationNodeFactory conversationNodeFactory = this.CreateConversationNodeFactory(mailboxSession, conversation);
					ConversationNode conversationNode = conversationNodeFactory.BuildConversationNodeFromSingleNodeTree();
					conversationNodes = new ConversationNode[]
					{
						conversationNode
					};
				}
				else
				{
					conversationNodes = this.BuildConversationNodes(conversation, currentSyncState, mailboxSession);
				}
				this.conversationStatisticsLogger.Log(conversation.ConversationStatistics);
				result = new ServiceResult<Microsoft.Exchange.Services.Core.Types.Conversations.ConversationResponseType>(new Microsoft.Exchange.Services.Core.Types.Conversations.ConversationResponseType
				{
					ConversationId = this.CurrentConversationRequest.ConversationId,
					ConversationNodes = conversationNodes,
					SyncState = conversation.SerializedTreeState,
					TotalConversationNodesCount = conversation.ConversationTree.Count
				});
			}
			finally
			{
				EWSSettings.CurrentConversation = null;
			}
			return result;
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x00071170 File Offset: 0x0006F370
		private ConversationNode[] BuildConversationNodes(Conversation conversation, byte[] syncState, MailboxSession mailboxSession)
		{
			IConversationTree conversationTree = conversation.ConversationTree;
			if (conversationTree == null || conversationTree.Count == 0)
			{
				throw new ObjectNotFoundException(ServerStrings.ExItemNotFound);
			}
			conversationTree.Sort((ConversationTreeSortOrder)this.sortOrder);
			Func<StoreObjectId, bool> returnOnlyMandatoryProperties;
			if (syncState != null && syncState.Length > 0)
			{
				List<StoreObjectId> itemsToRender = GetConversationItems.CalculateItemChanges(conversation, syncState);
				if (itemsToRender.Count > 0)
				{
					this.PrefetchItems(mailboxSession, itemsToRender);
				}
				ICollection<IConversationTreeNode> collection = GetConversationItems.CalculateChangedTreeNodes(conversationTree, itemsToRender);
				this.conversationLoader.LoadItemParts(conversation, collection, false);
				base.ParticipantResolver.LoadAdDataIfNeeded(conversation.AllParticipants(collection));
				returnOnlyMandatoryProperties = ((StoreObjectId objectId) => !itemsToRender.Contains(objectId));
			}
			else
			{
				this.PrefetchItems(mailboxSession, conversation.GetMessageIdsForPreread());
				this.conversationLoader.LoadItemParts(conversation, conversationTree, false);
				base.ParticipantResolver.LoadAdDataIfNeeded(conversation.AllParticipants(null));
				returnOnlyMandatoryProperties = ((StoreObjectId objectId) => false);
			}
			List<ConversationNode> list = this.BuildConversationNodes(mailboxSession, conversation, returnOnlyMandatoryProperties);
			if (list != null)
			{
				return list.ToArray();
			}
			return null;
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x00071284 File Offset: 0x0006F484
		private List<ConversationNode> BuildConversationNodes(MailboxSession mailboxSession, ICoreConversation conversation, Func<StoreObjectId, bool> returnOnlyMandatoryProperties)
		{
			List<ConversationNode> list = new List<ConversationNode>();
			ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(4043713853U, conversation.Topic);
			ConversationNodeFactory conversationNodeFactory = this.CreateConversationNodeFactory(mailboxSession, conversation);
			foreach (IConversationTreeNode treeNode in conversation.ConversationTree)
			{
				if (list.Count >= this.maxItemsToReturn)
				{
					break;
				}
				ConversationNode conversationNode = conversationNodeFactory.CreateInstance(treeNode, returnOnlyMandatoryProperties);
				if (conversationNode.ItemCount != 0)
				{
					list.Add(conversationNode);
				}
			}
			return list;
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x0007131C File Offset: 0x0006F51C
		private ConversationNodeFactory CreateConversationNodeFactory(MailboxSession mailboxSession, ICoreConversation conversation)
		{
			return new ConversationNodeFactory(mailboxSession, conversation, base.ParticipantResolver, this.ItemResponseShape, base.Request.ReturnSubmittedItems, ConversationLoaderHelper.MandatoryConversationPropertiesToLoad, GetConversationItems.GetPropertiesForConversationLoad(this.ItemResponseShape), this.conversationLoader.PropertiesLoaded, this.conversationLoader.PropertiesLoadedPerItem, !string.IsNullOrEmpty(base.Request.ShapeName));
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x00071380 File Offset: 0x0006F580
		private void PrefetchItems(MailboxSession mailboxSession, List<StoreObjectId> itemIds)
		{
			mailboxSession.PrereadMessages(itemIds.ToArray());
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x00071390 File Offset: 0x0006F590
		private IdAndSession GetSessionFromConversationId(BaseItemId conversationId)
		{
			IdAndSession idAndSession = base.IdConverter.ConvertConversationIdToIdAndSession(conversationId, this.mailboxScope == MailboxSearchLocation.ArchiveOnly);
			if (!(idAndSession.Session is MailboxSession))
			{
				throw new ServiceInvalidOperationException(CoreResources.IDs.ConversationSupportedOnlyForMailboxSession);
			}
			return idAndSession;
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x000713D4 File Offset: 0x0006F5D4
		private ServiceResult<Microsoft.Exchange.Services.Core.Types.Conversations.ConversationResponseType> LoadAndBuildArchiveConversation(ConversationId conversationId, byte[] currentSyncState, IExchangePrincipal primaryPrincipal)
		{
			ExchangeServiceBinding archiveServiceBinding = EwsClientHelper.GetArchiveServiceBinding(base.CallContext.EffectiveCaller, primaryPrincipal);
			if (archiveServiceBinding != null)
			{
				return this.LoadAndBuildRemoteConversation(archiveServiceBinding);
			}
			return new ServiceResult<Microsoft.Exchange.Services.Core.Types.Conversations.ConversationResponseType>(new ServiceError((CoreResources.IDs)3156121664U, Microsoft.Exchange.Services.Core.Types.ResponseCodeType.ErrorArchiveMailboxServiceDiscoveryFailed, 0, ExchangeVersion.Exchange2012));
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x00071440 File Offset: 0x0006F640
		private ServiceResult<Microsoft.Exchange.Services.Core.Types.Conversations.ConversationResponseType> LoadAndBuildRemoteConversation(ExchangeServiceBinding serviceBinding)
		{
			GetConversationItemsRequest source = new GetConversationItemsRequest
			{
				Conversations = new Microsoft.Exchange.Services.Core.Types.ConversationRequestType[]
				{
					this.CurrentConversationRequest
				},
				FoldersToIgnore = base.Request.FoldersToIgnore,
				ItemShape = this.GetResponseShape(),
				MaxItemsToReturn = base.Request.MaxItemsToReturn,
				SortOrder = base.Request.SortOrder,
				MailboxScope = MailboxSearchLocation.ArchiveOnly
			};
			GetConversationItemsType getConversationItemsType = EwsClientHelper.Convert<GetConversationItemsRequest, GetConversationItemsType>(source);
			Exception ex = null;
			GetConversationItemsResponseType getConversationItemsResponseType = null;
			bool flag = EwsClientHelper.ExecuteEwsCall(delegate
			{
				getConversationItemsResponseType = serviceBinding.GetConversationItems(getConversationItemsType);
			}, out ex);
			if (!flag || getConversationItemsResponseType.ResponseMessages == null || getConversationItemsResponseType.ResponseMessages.Items == null || getConversationItemsResponseType.ResponseMessages.Items.Length <= 0)
			{
				return new ServiceResult<Microsoft.Exchange.Services.Core.Types.Conversations.ConversationResponseType>(new ServiceError((CoreResources.IDs)3668888236U, Microsoft.Exchange.Services.Core.Types.ResponseCodeType.ErrorInternalServerError, 0, ExchangeVersion.Exchange2012));
			}
			GetConversationItemsResponseMessageType source2 = (GetConversationItemsResponseMessageType)getConversationItemsResponseType.ResponseMessages.Items[0];
			GetConversationItemsResponseMessage getConversationItemsResponseMessage = EwsClientHelper.Convert<GetConversationItemsResponseMessageType, GetConversationItemsResponseMessage>(source2);
			if (getConversationItemsResponseMessage.ResponseClass == ResponseClass.Success)
			{
				return new ServiceResult<Microsoft.Exchange.Services.Core.Types.Conversations.ConversationResponseType>(new Microsoft.Exchange.Services.Core.Types.Conversations.ConversationResponseType
				{
					ConversationId = getConversationItemsResponseMessage.Conversation.ConversationId,
					ConversationNodes = getConversationItemsResponseMessage.Conversation.ConversationNodes,
					SyncState = getConversationItemsResponseMessage.Conversation.SyncState
				});
			}
			return new ServiceResult<Microsoft.Exchange.Services.Core.Types.Conversations.ConversationResponseType>(new ServiceError((CoreResources.IDs)3668888236U, getConversationItemsResponseMessage.ResponseCode, getConversationItemsResponseMessage.DescriptiveLinkKey, ExchangeVersion.Exchange2012));
		}

		// Token: 0x04000EB6 RID: 3766
		private const int MaxItemsToReturnLimit = 100;

		// Token: 0x04000EB7 RID: 3767
		private static PropertyDefinition[] propertiesToLoad;

		// Token: 0x04000EB8 RID: 3768
		private static PropertyDefinition[] inferenceEnabledPropertiesToLoad;

		// Token: 0x04000EB9 RID: 3769
		private readonly int maxItemsToReturn;

		// Token: 0x04000EBA RID: 3770
		private ItemResponseShape itemResponseShape;

		// Token: 0x04000EBB RID: 3771
		private Microsoft.Exchange.Services.Core.Types.ConversationRequestType[] conversations;

		// Token: 0x04000EBC RID: 3772
		private BaseFolderId[] foldersToIgnore;

		// Token: 0x04000EBD RID: 3773
		private XsoConversationRepository<Conversation> conversationLoader;

		// Token: 0x04000EBE RID: 3774
		private Microsoft.Exchange.Services.Core.Types.ConversationNodeSortOrder sortOrder;

		// Token: 0x04000EBF RID: 3775
		private MailboxSearchLocation mailboxScope;

		// Token: 0x04000EC0 RID: 3776
		private ConversationStatisticsLogger conversationStatisticsLogger;

		// Token: 0x02000302 RID: 770
		// (Invoke) Token: 0x060015CA RID: 5578
		private delegate ServiceResult<Microsoft.Exchange.Services.Core.Types.Conversations.ConversationResponseType> ExecuteArchiveGetConversationItemsDelegate(ConversationId conversationId, byte[] currentSyncState, IExchangePrincipal primaryPrincipal);
	}
}
