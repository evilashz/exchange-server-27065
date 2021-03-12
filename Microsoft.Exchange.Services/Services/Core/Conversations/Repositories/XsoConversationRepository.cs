using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Services.Core.Conversations.Repositories
{
	// Token: 0x020003A7 RID: 935
	internal class XsoConversationRepository<T> : IConversationRepository<T> where T : ICoreConversation
	{
		// Token: 0x06001A40 RID: 6720 RVA: 0x00096A64 File Offset: 0x00094C64
		public XsoConversationRepository(ItemResponseShape itemResponseShape, PropertyDefinition[] propertiesToLoad, IIdConverter idConverter, ICoreConversationFactory<T> conversationFactory, IParticipantResolver participantResolver) : this(itemResponseShape, propertiesToLoad, idConverter, conversationFactory, null, participantResolver)
		{
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x00096A74 File Offset: 0x00094C74
		public XsoConversationRepository(ItemResponseShape itemResponseShape, PropertyDefinition[] propertiesToLoad, IIdConverter idConverter, ICoreConversationFactory<T> conversationFactory, CallContext callContext, IParticipantResolver participantResolver)
		{
			this.itemResponseShape = itemResponseShape;
			this.propertiesToLoad = propertiesToLoad;
			this.idConverter = idConverter;
			this.conversationFactory = conversationFactory;
			this.PrepareSmimeProperties(callContext);
			this.participantResolver = participantResolver;
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06001A42 RID: 6722 RVA: 0x00096AA9 File Offset: 0x00094CA9
		public Dictionary<StoreObjectId, HashSet<PropertyDefinition>> PropertiesLoadedPerItem
		{
			get
			{
				return this.propertiesLoadedPerItem;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06001A43 RID: 6723 RVA: 0x00096AB1 File Offset: 0x00094CB1
		public HashSet<PropertyDefinition> PropertiesLoaded
		{
			get
			{
				return this.propertiesLoaded;
			}
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x00096ABC File Offset: 0x00094CBC
		private void PrepareSmimeProperties(CallContext callContext)
		{
			bool flag = this.VerifySmimeConversationFlightEnabled(callContext);
			bool flag2 = flag && callContext != null && callContext.DefaultDomain != null && callContext.IsOwa;
			this.isSmimeSupported = (flag2 && !callContext.IsSmimeInstalled);
			this.domainName = (this.isSmimeSupported ? callContext.DefaultDomain.ToString() : null);
			this.getInlineImageWhenClientSmimeInstalled = (flag2 && callContext.IsSmimeInstalled);
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x00096B2C File Offset: 0x00094D2C
		private bool VerifySmimeConversationFlightEnabled(CallContext callContext)
		{
			if (callContext == null || callContext.AccessingADUser == null)
			{
				return false;
			}
			VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(callContext.AccessingADUser.GetContext(null), null, null);
			return snapshot != null && snapshot.OwaClientServer.SmimeConversation.Enabled;
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x00096B74 File Offset: 0x00094D74
		private ICollection<PropertyDefinition> GetAdditionalOpportunisticPropertiesToLoad(IStorePropertyBag storePropertyBag)
		{
			List<PropertyDefinition> list = new List<PropertyDefinition>();
			list.AddRange(ConversationLoaderHelper.EntityExtractionPropeties);
			if (IsClutterProperty.GetFlagValueOrDefaultFromStorePropertyBag(storePropertyBag, this.itemResponseShape))
			{
				list.AddRange(ConversationLoaderHelper.InferenceReasonsProperties);
			}
			return list;
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x00096BAC File Offset: 0x00094DAC
		private HashSet<PropertyDefinition> GetAdditionalPropertiesToLoad(IStorePropertyBag storePropertyBag)
		{
			HashSet<PropertyDefinition> hashSet = new HashSet<PropertyDefinition>();
			if (storePropertyBag.GetValueOrDefault<bool>(MessageItemSchema.ReplyToNamesExists, false) || storePropertyBag.GetValueOrDefault<bool>(MessageItemSchema.ReplyToBlobExists, false))
			{
				hashSet.Add(MessageItemSchema.ReplyToNames);
				hashSet.Add(MessageItemSchema.ReplyToBlob);
			}
			if (storePropertyBag.GetValueOrDefault<bool>(ItemSchema.IsClassified, false))
			{
				hashSet.UnionWith(ConversationLoaderHelper.ComplianceProperties);
			}
			string valueOrDefault = storePropertyBag.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, string.Empty);
			if (ObjectClass.IsVoiceMessage(valueOrDefault))
			{
				hashSet.UnionWith(ConversationLoaderHelper.VoiceMailProperties);
			}
			if (ObjectClass.IsOfClass(valueOrDefault, "IPM.Note.Microsoft.Approval.Request"))
			{
				hashSet.UnionWith(ConversationLoaderHelper.ApprovalRequestProperties);
			}
			if (ObjectClass.IsMeetingMessage(valueOrDefault))
			{
				hashSet.UnionWith(ConversationLoaderHelper.MeetingMessageProperties);
				hashSet.UnionWith(ConversationLoaderHelper.SingleRecipientProperties);
			}
			if (ObjectClass.IsMeetingRequest(valueOrDefault))
			{
				hashSet.UnionWith(ConversationLoaderHelper.ChangeHighlightingProperties);
			}
			if (ObjectClass.IsEventReminderMessage(valueOrDefault))
			{
				hashSet.UnionWith(ConversationLoaderHelper.ReminderMessageProperties);
			}
			return hashSet;
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x00096C90 File Offset: 0x00094E90
		private List<StoreObjectId> CalculateFoldersStoreObjectId(BaseFolderId[] folderIds)
		{
			List<StoreObjectId> list;
			if (folderIds != null)
			{
				list = new List<StoreObjectId>(folderIds.Length);
				foreach (BaseFolderId folderId in folderIds)
				{
					IdAndSession idAndSession = this.idConverter.ConvertFolderIdToIdAndSessionReadOnly(folderId);
					if (!(idAndSession.Session is MailboxSession))
					{
						throw new ServiceInvalidOperationException(CoreResources.IDs.ConversationSupportedOnlyForMailboxSession);
					}
					StoreObjectId asStoreObjectId = idAndSession.GetAsStoreObjectId();
					list.Add(asStoreObjectId);
				}
			}
			else
			{
				list = new List<StoreObjectId>(0);
			}
			return list;
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x00096D0C File Offset: 0x00094F0C
		private void ConversationOnBeforeItemLoad(LoadItemEventArgs eventArgs, params PropertyDefinition[] requestedProperties)
		{
			eventArgs.HtmlStreamOptionCallback = new HtmlStreamOptionCallback(this.GetSafeHtmlCallbacks);
			IStorePropertyBag storePropertyBag = eventArgs.StorePropertyBag;
			HashSet<PropertyDefinition> additionalPropertiesToLoad = this.GetAdditionalPropertiesToLoad(storePropertyBag);
			additionalPropertiesToLoad.UnionWith(requestedProperties);
			ICollection<PropertyDefinition> additionalOpportunisticPropertiesToLoad = this.GetAdditionalOpportunisticPropertiesToLoad(storePropertyBag);
			this.PropertiesLoadedPerItem[eventArgs.StorePropertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id, null).ObjectId] = new HashSet<PropertyDefinition>(additionalPropertiesToLoad);
			eventArgs.MessagePropertyDefinitions = additionalPropertiesToLoad.ToArray<PropertyDefinition>();
			eventArgs.OpportunisticLoadPropertyDefinitions = additionalOpportunisticPropertiesToLoad.ToArray<PropertyDefinition>();
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x00096DB4 File Offset: 0x00094FB4
		private KeyValuePair<HtmlStreamingFlags, HtmlCallbackBase> GetSafeHtmlCallbacks(Item item)
		{
			bool itemBlockStatus = Util.GetItemBlockStatus(this.participantResolver, item, this.itemResponseShape.BlockExternalImages, this.itemResponseShape.BlockExternalImagesIfSenderUntrusted);
			HtmlBodyCallback htmlBodyCallback = new HtmlBodyCallback(item, null, this.getInlineImageWhenClientSmimeInstalled);
			htmlBodyCallback.AddBlankTargetToLinks = this.itemResponseShape.AddBlankTargetToLinks;
			htmlBodyCallback.InlineImageUrlTemplate = this.itemResponseShape.InlineImageUrlTemplate;
			htmlBodyCallback.InlineImageUrlOnLoadTemplate = this.itemResponseShape.InlineImageUrlOnLoadTemplate;
			htmlBodyCallback.InlineImageCustomDataTemplate = this.itemResponseShape.InlineImageCustomDataTemplate;
			htmlBodyCallback.IsBodyFragment = true;
			htmlBodyCallback.BlockExternalImages = itemBlockStatus;
			htmlBodyCallback.CalculateAttachmentInlineProps = this.itemResponseShape.CalculateAttachmentInlineProps;
			htmlBodyCallback.HasBlockedImagesAction = delegate(bool value)
			{
				EWSSettings.ItemHasBlockedImages = new bool?(value);
			};
			htmlBodyCallback.InlineAttachmentIdAction = delegate(string value)
			{
				IDictionary<string, bool> inlineImagesInUniqueBody = EWSSettings.InlineImagesInUniqueBody;
				inlineImagesInUniqueBody[value] = true;
			};
			HtmlBodyCallback value2 = htmlBodyCallback;
			return new KeyValuePair<HtmlStreamingFlags, HtmlCallbackBase>(HtmlStreamingFlags.FilterHtml, value2);
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x00096EA4 File Offset: 0x000950A4
		public T Load(ConversationId conversationId, IMailboxSession mailboxSession, BaseFolderId[] folderIds, bool useFolderIdsAsExclusionList = true, bool additionalPropertiesOnLoadItemParts = true, params PropertyDefinition[] requestedProperties)
		{
			return this.InternalLoad(conversationId, mailboxSession, folderIds, useFolderIdsAsExclusionList, additionalPropertiesOnLoadItemParts, requestedProperties);
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x00096EB8 File Offset: 0x000950B8
		public void PrefetchAndLoadItemParts(IMailboxSession mailboxSession, ICoreConversation conversation, HashSet<IConversationTreeNode> nodesToLoadItemPart, bool isSyncScenario)
		{
			List<StoreObjectId> list;
			if (isSyncScenario)
			{
				list = XsoConversationRepositoryExtensions.GetListStoreObjectIds(nodesToLoadItemPart);
			}
			else
			{
				list = conversation.GetMessageIdsForPreread();
			}
			if (list.Count > 0)
			{
				this.PrefetchItems(mailboxSession, list);
			}
			this.InternalLoadItemParts(conversation, nodesToLoadItemPart, false);
			this.participantResolver.LoadAdDataIfNeeded(conversation.AllParticipants(null));
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x00096F05 File Offset: 0x00095105
		public void LoadItemParts(ICoreConversation conversation, ICollection<IConversationTreeNode> changedTreeNodes, bool skipBodySummaries = false)
		{
			this.InternalLoadItemParts(conversation, changedTreeNodes, skipBodySummaries);
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x00096F10 File Offset: 0x00095110
		private void InternalLoadItemParts(ICoreConversation conversation, ICollection<IConversationTreeNode> changedTreeNodes, bool skipBodySummaries)
		{
			if (!changedTreeNodes.Any<IConversationTreeNode>())
			{
				return;
			}
			if (!skipBodySummaries)
			{
				conversation.LoadBodySummaries();
			}
			try
			{
				conversation.LoadItemParts(changedTreeNodes);
			}
			catch (TextConvertersException innerException)
			{
				throw new PropertyRequestFailedException(CoreResources.IDs.ErrorItemPropertyRequestFailed, ItemSchema.Body.PropertyPath, innerException);
			}
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x00096F64 File Offset: 0x00095164
		protected virtual bool CalculateIsIrmEnabled(IMailboxSession mailboxSession)
		{
			return IrmUtils.IsIrmEnabled(this.itemResponseShape.ClientSupportsIrm, (MailboxSession)mailboxSession);
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x00096F7C File Offset: 0x0009517C
		protected virtual void PrefetchItems(IMailboxSession mailboxSession, List<StoreObjectId> itemIds)
		{
			((MailboxSession)mailboxSession).PrereadMessages(itemIds.ToArray());
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x00096F8F File Offset: 0x0009518F
		protected virtual bool IsPermissionError(Exception exception)
		{
			return exception is MapiExceptionNoAccess;
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x00096FB8 File Offset: 0x000951B8
		private T InternalLoad(ConversationId conversationId, IMailboxSession mailboxSession, BaseFolderId[] folderIds, bool useFolderIdsAsExclusionList = true, bool additionalPropertiesOnLoadItemParts = true, params PropertyDefinition[] requestedProperties)
		{
			T result;
			try
			{
				bool isIrmEnabled = this.CalculateIsIrmEnabled(mailboxSession);
				this.propertiesLoadedPerItem = new Dictionary<StoreObjectId, HashSet<PropertyDefinition>>();
				this.propertiesLoaded = new HashSet<PropertyDefinition>();
				T t = this.conversationFactory.CreateConversation(conversationId, this.CalculateFoldersStoreObjectId(folderIds), useFolderIdsAsExclusionList, isIrmEnabled, this.isSmimeSupported, this.domainName, this.propertiesToLoad);
				this.propertiesLoaded = new HashSet<PropertyDefinition>(this.propertiesToLoad);
				if (additionalPropertiesOnLoadItemParts)
				{
					t.OnBeforeItemLoad += delegate(object sender, LoadItemEventArgs args)
					{
						this.ConversationOnBeforeItemLoad(args, requestedProperties);
					};
				}
				result = t;
			}
			catch (StoragePermanentException ex)
			{
				if (!this.IsPermissionError(ex.InnerException))
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceDebug((long)conversationId.GetHashCode(), "[GetConversationItems::LoadConversation] encountered exception - Class: {0}, Message: {1}.Inner exception was not MapiExceptionConversationNotFound but rather Class: {2}, Message {3}", new object[]
					{
						ex.GetType().FullName,
						ex.Message,
						(ex.InnerException == null) ? "<NULL>" : ex.InnerException.GetType().FullName,
						(ex.InnerException == null) ? "<NULL>" : ex.InnerException.Message
					});
					throw;
				}
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug((long)conversationId.GetHashCode(), "[GetConversationItems::LoadConversation] encountered exception - Class: {0}, Message: {1}.Inner exception was MapiExceptionNoAccess - Class: {2}, Message {3}", new object[]
				{
					ex.GetType().FullName,
					ex.Message,
					(ex.InnerException == null) ? "<NULL>" : ex.InnerException.GetType().FullName,
					(ex.InnerException == null) ? "<NULL>" : ex.InnerException.Message
				});
				result = default(T);
			}
			return result;
		}

		// Token: 0x04001161 RID: 4449
		private readonly PropertyDefinition[] propertiesToLoad;

		// Token: 0x04001162 RID: 4450
		private readonly ItemResponseShape itemResponseShape;

		// Token: 0x04001163 RID: 4451
		private readonly IIdConverter idConverter;

		// Token: 0x04001164 RID: 4452
		private readonly ICoreConversationFactory<T> conversationFactory;

		// Token: 0x04001165 RID: 4453
		private bool isSmimeSupported;

		// Token: 0x04001166 RID: 4454
		private string domainName;

		// Token: 0x04001167 RID: 4455
		private bool getInlineImageWhenClientSmimeInstalled;

		// Token: 0x04001168 RID: 4456
		private readonly IParticipantResolver participantResolver;

		// Token: 0x04001169 RID: 4457
		private HashSet<PropertyDefinition> propertiesLoaded;

		// Token: 0x0400116A RID: 4458
		private Dictionary<StoreObjectId, HashSet<PropertyDefinition>> propertiesLoadedPerItem;
	}
}
