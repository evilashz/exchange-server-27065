using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Conversations.LoadingListBuilders;
using Microsoft.Exchange.Services.Core.Conversations.Repositories;
using Microsoft.Exchange.Services.Core.Conversations.ResponseBuilders;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;

namespace Microsoft.Exchange.Services.Core.Conversations
{
	// Token: 0x02000303 RID: 771
	internal abstract class GetConversationItemsBase<TConversationType, TRequestType, TSingleItemType> : MultiStepServiceCommand<TRequestType, TSingleItemType> where TConversationType : ICoreConversation where TRequestType : GetConversationItemsRequest where TSingleItemType : new()
	{
		// Token: 0x060015CD RID: 5581 RVA: 0x000715F7 File Offset: 0x0006F7F7
		protected GetConversationItemsBase(CallContext callContext, TRequestType request) : base(callContext, request)
		{
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060015CE RID: 5582 RVA: 0x00071604 File Offset: 0x0006F804
		private static PropertyDefinition[] MandatoryPropertiesToLoad
		{
			get
			{
				if (GetConversationItemsBase<TConversationType, TRequestType, TSingleItemType>.mandatoryPropertiesToLoad == null)
				{
					List<PropertyDefinition> list = new List<PropertyDefinition>(ConversationLoaderHelper.MandatoryConversationPropertiesToLoad);
					list.AddRange(ConversationLoaderHelper.ModernConversationMandatoryPropertiesToLoad);
					GetConversationItemsBase<TConversationType, TRequestType, TSingleItemType>.mandatoryPropertiesToLoad = list.ToArray();
				}
				return GetConversationItemsBase<TConversationType, TRequestType, TSingleItemType>.mandatoryPropertiesToLoad;
			}
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x0007163E File Offset: 0x0006F83E
		protected override IParticipantResolver ConstructParticipantResolver()
		{
			return Microsoft.Exchange.Services.Core.Types.ParticipantResolver.Create(base.CallContext, this.ItemResponseShape.MaximumRecipientsToReturn);
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060015D0 RID: 5584 RVA: 0x00071658 File Offset: 0x0006F858
		internal override int StepCount
		{
			get
			{
				TRequestType request = base.Request;
				return request.Conversations.Length;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x0007167B File Offset: 0x0006F87B
		protected virtual ServiceResult<TSingleItemType>[] InternalResults
		{
			get
			{
				return base.Results;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060015D2 RID: 5586 RVA: 0x00071683 File Offset: 0x0006F883
		protected virtual PropertyDefinition[] AdditionalRequestedProperties
		{
			get
			{
				return new PropertyDefinition[0];
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060015D3 RID: 5587
		protected abstract int MaxItemsToReturn { get; }

		// Token: 0x060015D4 RID: 5588 RVA: 0x0007168C File Offset: 0x0006F88C
		private ItemResponseShape GetResponseShape()
		{
			IResponseShapeResolver responseShapeResolver = Global.ResponseShapeResolver;
			TRequestType request = base.Request;
			string shapeName = request.ShapeName;
			TRequestType request2 = base.Request;
			return responseShapeResolver.GetResponseShape<ItemResponseShape>(shapeName, request2.ItemShape, base.CallContext.FeaturesManager);
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x000716D6 File Offset: 0x0006F8D6
		internal override void PreExecuteCommand()
		{
			base.PreExecuteCommand();
			this.conversationStatisticsLogger = new ConversationStatisticsLogger(base.CallContext.ProtocolLog);
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x000716F4 File Offset: 0x0006F8F4
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return this.GetResponseInternal();
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x000716FC File Offset: 0x0006F8FC
		internal ConversationRequestType CurrentConversationRequest
		{
			get
			{
				TRequestType request = base.Request;
				return request.Conversations[base.CurrentStep];
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x00071724 File Offset: 0x0006F924
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

		// Token: 0x060015D9 RID: 5593 RVA: 0x00071740 File Offset: 0x0006F940
		private static PropertyDefinition[] GetPropertiesForConversationLoad(ItemResponseShape itemResponseShape)
		{
			PropertyDefinition[] propertiesToLoad = GetConversationItemsBase<TConversationType, TRequestType, TSingleItemType>.GetPropertiesToLoad(itemResponseShape);
			if (!itemResponseShape.InferenceEnabled)
			{
				return propertiesToLoad;
			}
			return ConversationLoaderHelper.CalculateInferenceEnabledPropertiesToLoad(propertiesToLoad);
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x00071764 File Offset: 0x0006F964
		private static PropertyDefinition[] GetPropertiesToLoad(ItemResponseShape itemResponseShape)
		{
			List<PropertyDefinition> list = new List<PropertyDefinition>(GetConversationItemsBase<TConversationType, TRequestType, TSingleItemType>.MandatoryPropertiesToLoad);
			list.AddRange(ConversationLoaderHelper.NonMandatoryPropertiesToLoad);
			list.AddRange(ConversationLoaderHelper.InReplyToPropertiesToLoad);
			if (itemResponseShape.AdditionalProperties != null && itemResponseShape.AdditionalProperties.Any<PropertyPath>())
			{
				foreach (PropertyInformation propertyInformation in ConversationLoaderHelper.ModernConversationOptionalPropertiesToLoad)
				{
					if (itemResponseShape.AdditionalProperties.Contains(propertyInformation.PropertyPath))
					{
						list.AddRange(propertyInformation.GetPropertyDefinitions(null));
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x000717E8 File Offset: 0x0006F9E8
		internal override ServiceResult<TSingleItemType> Execute()
		{
			int maxItemsToReturn = this.MaxItemsToReturn;
			TRequestType request = base.Request;
			bool returnSubmittedItems = request.ReturnSubmittedItems;
			TRequestType request2 = base.Request;
			ConversationRequestArguments conversationRequestArguments = new ConversationRequestArguments(maxItemsToReturn, returnSubmittedItems, request2.SortOrder);
			ExTraceGlobals.GetConversationItemsTracer.TraceDebug((long)this.GetHashCode(), "GetConversationItemsBase.Execute: Requesting conversation. ConversationId: {0}, MaxItemsToReturn: {1}, ReturnSubmittedItems: {2}, SortOrder: {3}", new object[]
			{
				this.CurrentConversationRequest.ConversationId,
				conversationRequestArguments.MaxItemsToReturn,
				conversationRequestArguments.ReturnSubmittedItems,
				conversationRequestArguments.SortOrder
			});
			ConversationRequestType currentConversationRequest = this.CurrentConversationRequest;
			byte[] syncState = currentConversationRequest.SyncState;
			IdAndSession sessionFromConversationId = XsoConversationRepositoryExtensions.GetSessionFromConversationId(base.IdConverter, currentConversationRequest.ConversationId, MailboxSearchLocation.PrimaryOnly);
			ConversationId conversationId = sessionFromConversationId.Id as ConversationId;
			MailboxSession mailboxSession = (MailboxSession)sessionFromConversationId.Session;
			XsoConversationRepository<TConversationType> xsoConversationRepository = new XsoConversationRepository<TConversationType>(this.ItemResponseShape, GetConversationItemsBase<TConversationType, TRequestType, TSingleItemType>.GetPropertiesForConversationLoad(this.ItemResponseShape), base.IdConverter, this.CreateConversationFactory(mailboxSession), base.CallContext, base.ParticipantResolver);
			bool useFolderIdsAsExclusionList = !mailboxSession.IsGroupMailbox();
			BaseFolderId[] array;
			if (!mailboxSession.IsGroupMailbox())
			{
				TRequestType request3 = base.Request;
				array = request3.FoldersToIgnore;
			}
			else
			{
				array = this.GetGroupFoldersToLoadFrom(mailboxSession);
			}
			BaseFolderId[] folderIds = array;
			TConversationType tconversationType = xsoConversationRepository.Load(conversationId, mailboxSession, folderIds, useFolderIdsAsExclusionList, true, this.AdditionalRequestedProperties);
			if (tconversationType == null || tconversationType.ConversationTree == null || tconversationType.ConversationTree.Count == 0)
			{
				throw new ObjectNotFoundException(ServerStrings.ExItemNotFound);
			}
			ConversationNodeLoadingList conversationNodeLoadingList = this.CalculateNodesToLoadFromXso(tconversationType, syncState, conversationRequestArguments);
			ExTraceGlobals.GetConversationItemsTracer.TraceDebug<int, int, int>((long)this.GetHashCode(), "GetConversationItemsBase.Execute: LoadingList calculated. Items to be loaded: {0}, Items to be ignored: {1}, Items to be skipped: {2}", conversationNodeLoadingList.ToBeLoaded.Count<IConversationTreeNode>(), conversationNodeLoadingList.ToBeIgnored.Count<IConversationTreeNode>(), conversationNodeLoadingList.NotToBeLoaded.Count<IConversationTreeNode>());
			this.LoadConversationFromXso(syncState, mailboxSession, conversationNodeLoadingList, tconversationType, xsoConversationRepository);
			TSingleItemType value = this.ConvertXsoConversationToEwsConversation(mailboxSession, xsoConversationRepository, tconversationType, conversationNodeLoadingList, conversationRequestArguments);
			this.conversationStatisticsLogger.Log(currentConversationRequest);
			this.conversationStatisticsLogger.Log(tconversationType.ConversationStatistics);
			return new ServiceResult<TSingleItemType>(value);
		}

		// Token: 0x060015DC RID: 5596
		protected abstract IExchangeWebMethodResponse GetResponseInternal();

		// Token: 0x060015DD RID: 5597
		protected abstract ICoreConversationFactory<TConversationType> CreateConversationFactory(IMailboxSession mailboxSession);

		// Token: 0x060015DE RID: 5598
		protected abstract ConversationNodeLoadingListBuilderBase CreateConversationNodeLoadingListBuilder(TConversationType conversation, ConversationRequestArguments requestArguments, List<IConversationTreeNode> nonSyncedNodes);

		// Token: 0x060015DF RID: 5599
		protected abstract ConversationResponseBuilderBase<TSingleItemType> CreateBuilder(IMailboxSession mailboxSession, TConversationType conversation, ConversationNodeLoadingList loadingList, ConversationRequestArguments requestArguments, ModernConversationNodeFactory conversationNodeFactory);

		// Token: 0x060015E0 RID: 5600 RVA: 0x00071A10 File Offset: 0x0006FC10
		private ConversationNodeLoadingList CalculateNodesToLoadFromXso(TConversationType conversation, byte[] currentSyncState, ConversationRequestArguments requestArguments)
		{
			List<IConversationTreeNode> treeNodes = XsoConversationRepositoryExtensions.GetTreeNodes(conversation, currentSyncState);
			ConversationNodeLoadingListBuilderBase conversationNodeLoadingListBuilderBase = this.CreateConversationNodeLoadingListBuilder(conversation, requestArguments, treeNodes);
			return conversationNodeLoadingListBuilderBase.Build();
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x00071A3C File Offset: 0x0006FC3C
		private void LoadConversationFromXso(byte[] currentSyncState, MailboxSession mailboxSession, ConversationNodeLoadingList loadingList, ICoreConversation conversation, IConversationRepository<TConversationType> conversationRepository)
		{
			bool flag = XsoConversationRepositoryExtensions.IsValidSyncState(currentSyncState);
			HashSet<IConversationTreeNode> hashSet = new HashSet<IConversationTreeNode>(loadingList.ToBeLoaded);
			ExTraceGlobals.GetConversationItemsTracer.TraceDebug<int, bool>((long)this.GetHashCode(), "GetConversationItemsBase.LoadConversationFromXso: Loading nodes from XSO. Nodes to load: {0}, Has sync state: {1}", hashSet.Count, flag);
			conversationRepository.PrefetchAndLoadItemParts(mailboxSession, conversation, hashSet, flag);
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x00071A88 File Offset: 0x0006FC88
		private TSingleItemType ConvertXsoConversationToEwsConversation(MailboxSession mailboxSession, XsoConversationRepository<TConversationType> conversationRepository, TConversationType conversation, ConversationNodeLoadingList loadingList, ConversationRequestArguments requestArguments)
		{
			ModernConversationNodeFactory conversationNodeFactory = this.CreateConversationNodeFactory(mailboxSession, conversationRepository, conversation, new HashSet<IConversationTreeNode>(loadingList.ToBeLoaded, ConversationTreeNodeBase.EqualityComparer));
			ConversationResponseBuilderBase<TSingleItemType> conversationResponseBuilderBase = this.CreateBuilder(mailboxSession, conversation, loadingList, requestArguments, conversationNodeFactory);
			return conversationResponseBuilderBase.Build();
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x00071ACC File Offset: 0x0006FCCC
		private BaseFolderId[] GetGroupFoldersToLoadFrom(MailboxSession mailboxSession)
		{
			DistinguishedFolderId distinguishedFolderId = new DistinguishedFolderId
			{
				Id = DistinguishedFolderIdName.inbox,
				Mailbox = new EmailAddressWrapper
				{
					EmailAddress = mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(),
					RoutingType = "SMTP"
				}
			};
			return new BaseFolderId[]
			{
				distinguishedFolderId
			};
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x00071B34 File Offset: 0x0006FD34
		private ModernConversationNodeFactory CreateConversationNodeFactory(MailboxSession mailboxSession, XsoConversationRepository<TConversationType> conversationRepository, ICoreConversation conversation, HashSet<IConversationTreeNode> itemsToBeFullyLoaded)
		{
			IParticipantResolver participantResolver = base.ParticipantResolver;
			ItemResponseShape itemResponse = this.ItemResponseShape;
			ICollection<PropertyDefinition> collection = GetConversationItemsBase<TConversationType, TRequestType, TSingleItemType>.MandatoryPropertiesToLoad;
			ICollection<PropertyDefinition> propertiesForConversationLoad = GetConversationItemsBase<TConversationType, TRequestType, TSingleItemType>.GetPropertiesForConversationLoad(this.ItemResponseShape);
			HashSet<PropertyDefinition> propertiesLoaded = conversationRepository.PropertiesLoaded;
			Dictionary<StoreObjectId, HashSet<PropertyDefinition>> propertiesLoadedPerItem = conversationRepository.PropertiesLoadedPerItem;
			TRequestType request = base.Request;
			return new ModernConversationNodeFactory(mailboxSession, conversation, participantResolver, itemResponse, collection, propertiesForConversationLoad, propertiesLoaded, propertiesLoadedPerItem, itemsToBeFullyLoaded, !string.IsNullOrEmpty(request.ShapeName));
		}

		// Token: 0x04000EC2 RID: 3778
		private static PropertyDefinition[] mandatoryPropertiesToLoad;

		// Token: 0x04000EC3 RID: 3779
		private ItemResponseShape itemResponseShape;

		// Token: 0x04000EC4 RID: 3780
		private ConversationStatisticsLogger conversationStatisticsLogger;
	}
}
