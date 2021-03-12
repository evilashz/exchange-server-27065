using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensions;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008D3 RID: 2259
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ConversationMembersQueryBase : IConversationMembersQuery
	{
		// Token: 0x06005414 RID: 21524 RVA: 0x0015D26B File Offset: 0x0015B46B
		public ConversationMembersQueryBase(IXSOFactory xsoFactory, IMailboxSession session)
		{
			this.xsoFactory = xsoFactory;
			this.session = session;
		}

		// Token: 0x17001777 RID: 6007
		// (get) Token: 0x06005415 RID: 21525 RVA: 0x0015D281 File Offset: 0x0015B481
		protected IXSOFactory XsoFactory
		{
			get
			{
				return this.xsoFactory;
			}
		}

		// Token: 0x17001778 RID: 6008
		// (get) Token: 0x06005416 RID: 21526 RVA: 0x0015D289 File Offset: 0x0015B489
		protected IMailboxSession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x06005417 RID: 21527 RVA: 0x0015D294 File Offset: 0x0015B494
		public List<IStorePropertyBag> Query(ConversationId conversationId, ICollection<PropertyDefinition> headerPropertyDefinition, IList<StoreObjectId> folderIds, bool useFolderIdsAsExclusionList)
		{
			Util.ThrowOnNullArgument(conversationId, "conversationId");
			this.ValidateRequestedProperties(headerPropertyDefinition);
			List<IStorePropertyBag> list = this.InternalQuery(conversationId, headerPropertyDefinition);
			if (list == null)
			{
				return null;
			}
			this.FilterResults(folderIds, useFolderIdsAsExclusionList, list);
			return list;
		}

		// Token: 0x06005418 RID: 21528 RVA: 0x0015D2CC File Offset: 0x0015B4CC
		public Dictionary<object, List<IStorePropertyBag>> AggregateMembersPerField(PropertyDefinition field, object defaultValue, List<IStorePropertyBag> members)
		{
			Dictionary<object, List<IStorePropertyBag>> dictionary = new Dictionary<object, List<IStorePropertyBag>>();
			foreach (IStorePropertyBag storePropertyBag in members)
			{
				object valueOrDefault = storePropertyBag.GetValueOrDefault<object>(field, defaultValue);
				List<IStorePropertyBag> list;
				if (!dictionary.TryGetValue(valueOrDefault, out list))
				{
					list = new List<IStorePropertyBag>(1000);
					dictionary.Add(valueOrDefault, list);
				}
				list.Add(storePropertyBag);
			}
			return dictionary;
		}

		// Token: 0x06005419 RID: 21529
		protected abstract ComparisonFilter CreateConversationFilter(ConversationId conversationId);

		// Token: 0x0600541A RID: 21530
		protected abstract IFolder GetSearchFolder(ICollection<PropertyDefinition> headerPropertyDefinition);

		// Token: 0x0600541B RID: 21531
		protected abstract IQueryResult GetQueryResult(IFolder rootFolder, ComparisonFilter conversationFilter, ICollection<PropertyDefinition> headerPropertyDefinition);

		// Token: 0x0600541C RID: 21532 RVA: 0x0015D34C File Offset: 0x0015B54C
		protected List<IStorePropertyBag> RetrieveItems(ComparisonFilter conversationFilter, ICollection<PropertyDefinition> headerPropertyDefinition, out Dictionary<StoreObjectId, List<StoreObjectId>> itemsRetrievalFailed)
		{
			List<IStorePropertyBag> list = new List<IStorePropertyBag>(1000);
			ICollection<NativeStorePropertyDefinition> nativePropertyDefinitions = StorePropertyDefinition.GetNativePropertyDefinitions<PropertyDefinition>(PropertyDependencyType.NeedForRead, headerPropertyDefinition);
			itemsRetrievalFailed = new Dictionary<StoreObjectId, List<StoreObjectId>>();
			using (IFolder searchFolder = this.GetSearchFolder(headerPropertyDefinition))
			{
				try
				{
					using (IQueryResult queryResult = this.GetQueryResult(searchFolder, conversationFilter, headerPropertyDefinition))
					{
						IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(1000);
						while (propertyBags.Length != 0)
						{
							IStorePropertyBag[] array = propertyBags;
							int i = 0;
							while (i < array.Length)
							{
								IStorePropertyBag storePropertyBag = array[i];
								Dictionary<StoreObjectId, List<StoreObjectId>> itemsFailedToRetrieve = this.GetItemsFailedToRetrieve(storePropertyBag, nativePropertyDefinitions);
								if (itemsFailedToRetrieve != null)
								{
									using (Dictionary<StoreObjectId, List<StoreObjectId>>.Enumerator enumerator = itemsFailedToRetrieve.GetEnumerator())
									{
										while (enumerator.MoveNext())
										{
											KeyValuePair<StoreObjectId, List<StoreObjectId>> keyValuePair = enumerator.Current;
											itemsRetrievalFailed.Add(keyValuePair.Key, keyValuePair.Value);
										}
										goto IL_A7;
									}
									goto IL_9F;
								}
								goto IL_9F;
								IL_A7:
								i++;
								continue;
								IL_9F:
								list.Add(storePropertyBag);
								goto IL_A7;
							}
							propertyBags = queryResult.GetPropertyBags(1000);
						}
					}
				}
				catch (StoragePermanentException ex)
				{
					if (!(ex.InnerException is MapiExceptionConversationNotFound))
					{
						throw;
					}
					return null;
				}
			}
			return list;
		}

		// Token: 0x0600541D RID: 21533 RVA: 0x0015D490 File Offset: 0x0015B690
		private Dictionary<StoreObjectId, List<StoreObjectId>> GetItemsFailedToRetrieve(IStorePropertyBag propertyBag, ICollection<NativeStorePropertyDefinition> nativeHeaderPropertyDefinition)
		{
			Dictionary<StoreObjectId, List<StoreObjectId>> dictionary = null;
			bool flag = !this.CheckAllPropertiesWereRetrieved(propertyBag, nativeHeaderPropertyDefinition);
			if (flag)
			{
				dictionary = new Dictionary<StoreObjectId, List<StoreObjectId>>();
				StoreObjectId objectId = ((VersionedId)propertyBag.TryGetProperty(ItemSchema.Id)).ObjectId;
				StoreObjectId key = (StoreObjectId)propertyBag.TryGetProperty(StoreObjectSchema.ParentItemId);
				List<StoreObjectId> list = null;
				if (!dictionary.TryGetValue(key, out list))
				{
					list = new List<StoreObjectId>();
					dictionary.Add(key, list);
				}
				list.Add(objectId);
			}
			return dictionary;
		}

		// Token: 0x0600541E RID: 21534 RVA: 0x0015D504 File Offset: 0x0015B704
		private List<IStorePropertyBag> InternalQuery(ConversationId conversationId, ICollection<PropertyDefinition> headerPropertyDefinition)
		{
			this.ValidateRequestedProperties(headerPropertyDefinition);
			Dictionary<StoreObjectId, List<StoreObjectId>> folderToPromotePropertyIn;
			List<IStorePropertyBag> list = this.RetrieveItems(this.CreateConversationFilter(conversationId), headerPropertyDefinition, out folderToPromotePropertyIn);
			if (list == null)
			{
				return null;
			}
			list.AddRange(this.RetrieveItemsFromFoldersWithNonPromotedProperties(folderToPromotePropertyIn, headerPropertyDefinition));
			return list;
		}

		// Token: 0x0600541F RID: 21535 RVA: 0x0015D540 File Offset: 0x0015B740
		private void FilterResults(IList<StoreObjectId> folderIds, bool useFolderIdsAsExclusionList, List<IStorePropertyBag> messages)
		{
			if (folderIds != null && folderIds.Count > 0)
			{
				HashSet<StoreObjectId> hashSet = new HashSet<StoreObjectId>(folderIds);
				for (int i = messages.Count - 1; i >= 0; i--)
				{
					StoreObjectId storeObjectId = messages[i].TryGetProperty(StoreObjectSchema.ParentItemId) as StoreObjectId;
					if (storeObjectId != null && ((!useFolderIdsAsExclusionList && !hashSet.Contains(storeObjectId)) || (useFolderIdsAsExclusionList && hashSet.Contains(storeObjectId))))
					{
						messages.RemoveAt(i);
					}
				}
			}
		}

		// Token: 0x06005420 RID: 21536 RVA: 0x0015D5AC File Offset: 0x0015B7AC
		private bool CheckAllPropertiesWereRetrieved(IStorePropertyBag propertyBag, ICollection<NativeStorePropertyDefinition> nativeRequiredPropertiesDefinition)
		{
			bool result = true;
			foreach (PropertyDefinition propertyDefinition in nativeRequiredPropertiesDefinition)
			{
				PropertyError propertyError = propertyBag.TryGetProperty(propertyDefinition) as PropertyError;
				if (propertyError != null && propertyError.PropertyErrorCode == PropertyErrorCode.PropertyNotPromoted)
				{
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x06005421 RID: 21537 RVA: 0x0015D610 File Offset: 0x0015B810
		private List<IStorePropertyBag> RetrieveItemsFromFoldersWithNonPromotedProperties(Dictionary<StoreObjectId, List<StoreObjectId>> folderToPromotePropertyIn, ICollection<PropertyDefinition> propertiesToRetrieve)
		{
			List<IStorePropertyBag> list = new List<IStorePropertyBag>();
			foreach (KeyValuePair<StoreObjectId, List<StoreObjectId>> keyValuePair in folderToPromotePropertyIn)
			{
				try
				{
					using (IFolder folder = this.xsoFactory.BindToFolder(this.Session, keyValuePair.Key))
					{
						using (IQueryResult queryResult = folder.IItemQuery(ItemQueryType.None, null, null, propertiesToRetrieve))
						{
							foreach (StoreObjectId propertyValue in keyValuePair.Value)
							{
								ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.Id, propertyValue);
								if (queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter))
								{
									list.AddRange(queryResult.GetPropertyBags(1));
								}
							}
						}
					}
				}
				catch (StoragePermanentException ex)
				{
					if (!(ex.InnerException is MapiExceptionConversationNotFound))
					{
						throw;
					}
				}
			}
			return list;
		}

		// Token: 0x06005422 RID: 21538 RVA: 0x0015D740 File Offset: 0x0015B940
		protected virtual void ValidateRequestedProperties(ICollection<PropertyDefinition> properties)
		{
			if (properties == null || properties.Count == 0)
			{
				return;
			}
			foreach (PropertyDefinition propertyDefinition in properties)
			{
				if (!ConversationMembersQueryBase.IsPropertyInAllowedList(propertyDefinition))
				{
					throw new UnsupportedPropertyInConversationLoadException(new LocalizedString(propertyDefinition.Name + " property cannot be requested in Conversation.Load"), propertyDefinition);
				}
			}
		}

		// Token: 0x06005423 RID: 21539 RVA: 0x0015D7B4 File Offset: 0x0015B9B4
		private static bool IsPropertyInAllowedList(PropertyDefinition property)
		{
			SmartPropertyDefinition smartPropertyDefinition = property as SmartPropertyDefinition;
			if (smartPropertyDefinition != null)
			{
				foreach (PropertyDependency propertyDependency in smartPropertyDefinition.Dependencies)
				{
					if ((propertyDependency.Type & PropertyDependencyType.NeedForRead) == PropertyDependencyType.NeedForRead && !ConversationMembersQueryBase.AllowedPropertiesForLoad.Contains(propertyDependency.Property))
					{
						return false;
					}
				}
				return true;
			}
			return ConversationMembersQueryBase.AllowedPropertiesForLoad.Contains(property);
		}

		// Token: 0x06005424 RID: 21540 RVA: 0x0015D81C File Offset: 0x0015BA1C
		private static HashSet<PropertyDefinition> GetAllowedNativePropertiesToLoad()
		{
			PropertyDefinition[] array = new PropertyDefinition[]
			{
				ItemSchema.Id,
				StoreObjectSchema.ChangeKey,
				StoreObjectSchema.ItemClass,
				ItemSchema.ConversationId,
				ItemSchema.ConversationFamilyId,
				ItemSchema.BodyTag,
				InternalSchema.DisplayBccInternal,
				InternalSchema.DisplayCcInternal,
				InternalSchema.DisplayToInternal,
				ItemSchema.DisplayBcc,
				ItemSchema.DisplayCc,
				ItemSchema.DisplayTo,
				ItemSchema.DocumentId,
				ItemSchema.HasAttachment,
				StoreObjectSchema.ParentItemId,
				ItemSchema.ParentDisplayName,
				ItemSchema.ConversationTopic,
				ItemSchema.Subject,
				ItemSchema.ConversationIndex,
				ItemSchema.IconIndex,
				MessageItemSchema.MessageInConflict,
				MessageItemSchema.IsRead,
				ItemSchema.From,
				ItemSchema.Sender,
				ItemSchema.ReceivedTime,
				MessageItemSchema.IsDraft,
				MessageItemSchema.HasBeenSubmitted,
				ItemSchema.Categories,
				ItemSchema.ItemColor,
				StoreObjectSchema.LastModifiedTime,
				ItemSchema.IsToDoItem,
				ItemSchema.FlagStatus,
				ItemSchema.FlagRequest,
				TaskSchema.StartDate,
				TaskSchema.DueDate,
				ItemSchema.CompleteDate,
				MessageItemSchema.LastVerbExecuted,
				MessageItemSchema.LastVerbExecutionTime,
				ItemSchema.Size,
				ItemSchema.Importance,
				ItemSchema.Sensitivity,
				ItemSchema.IsClassified,
				MessageItemSchema.IsReadReceiptPending,
				ItemSchema.BlockStatus,
				ItemSchema.EdgePcl,
				ItemSchema.LinkEnabled,
				ItemSchema.IsResponseRequested,
				ItemSchema.InternetMessageId,
				MessageItemSchema.VoiceMessageAttachmentOrder,
				MessageItemSchema.VotingBlob,
				MessageItemSchema.VotingResponse,
				StoreObjectSchema.ContentClass,
				StoreObjectSchema.PolicyTag,
				ItemSchema.RetentionDate,
				StoreObjectSchema.IsRestricted,
				ItemSchema.IsComplete,
				ItemSchema.SentRepresentingDisplayName,
				MessageItemSchema.ReceivedRepresentingEmailAddress,
				StoreObjectSchema.EntryId,
				ItemSchema.SentRepresentingEmailAddress,
				MessageItemSchema.DRMRights,
				ItemSchema.UtcStartDate,
				ItemSchema.UtcDueDate,
				MessageItemSchema.RequireProtectedPlayOnPhone,
				ItemSchema.IsClutter,
				MessageItemSchema.MessageBccMe,
				MessageItemSchema.ReplyToNames,
				MessageItemSchema.ReplyToBlob,
				ItemSchema.PropertyExistenceTracker,
				ItemSchema.NativeBodyInfo,
				StoreObjectSchema.ArchivePeriod,
				StoreObjectSchema.RetentionPeriod,
				StoreObjectSchema.RetentionFlags,
				ItemSchema.InstanceKey,
				ItemSchema.Fid,
				MessageItemSchema.MID,
				StoreObjectSchema.CreationTime,
				ItemSchema.SentTime,
				ItemSchema.RetentionDate,
				MessageItemSchema.SharingInstanceGuid,
				MessageItemSchema.TextMessageDeliveryStatus,
				StoreObjectSchema.ArchiveTag,
				MessageItemSchema.IsReadReceiptRequested,
				MessageItemSchema.IsDeliveryReceiptRequested,
				MessageItemSchema.HasBeenSubmitted,
				ItemSchema.Preview,
				ItemSchema.ExchangeApplicationFlags,
				ItemSchema.ConversationCreatorSID,
				ItemSchema.RichContent,
				MessageItemSchema.LikeCount,
				ItemSchema.ReceivedOrRenewTime,
				MessageItemSchema.MapiLikersBlob,
				InternalSchema.EffectiveRights,
				MessageItemSchema.IsGroupEscalationMessage,
				ItemSchema.InReplyTo,
				InternalSchema.ObjectType
			};
			HashSet<PropertyDefinition> hashSet = new HashSet<PropertyDefinition>();
			foreach (PropertyDefinition propertyDefinition in array)
			{
				hashSet.AddRange(propertyDefinition.RequiredPropertyDefinitionsWhenReading);
			}
			return hashSet;
		}

		// Token: 0x04002D7C RID: 11644
		public const int ConversationQueryFetchSize = 1000;

		// Token: 0x04002D7D RID: 11645
		private static readonly HashSet<PropertyDefinition> AllowedPropertiesForLoad = ConversationMembersQueryBase.GetAllowedNativePropertiesToLoad();

		// Token: 0x04002D7E RID: 11646
		private readonly IXSOFactory xsoFactory;

		// Token: 0x04002D7F RID: 11647
		private readonly IMailboxSession session;
	}
}
