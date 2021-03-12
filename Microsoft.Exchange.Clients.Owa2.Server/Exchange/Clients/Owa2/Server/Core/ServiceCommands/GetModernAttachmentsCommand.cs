using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.ServiceCommands
{
	// Token: 0x02000319 RID: 793
	internal class GetModernAttachmentsCommand : ServiceCommand<GetModernAttachmentsResponse>
	{
		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001A56 RID: 6742 RVA: 0x00060EA2 File Offset: 0x0005F0A2
		// (set) Token: 0x06001A57 RID: 6743 RVA: 0x00060EAA File Offset: 0x0005F0AA
		private ExDateTime itemCreationTimeStart { get; set; }

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001A58 RID: 6744 RVA: 0x00060EB3 File Offset: 0x0005F0B3
		// (set) Token: 0x06001A59 RID: 6745 RVA: 0x00060EBB File Offset: 0x0005F0BB
		private ExDateTime itemCreationTimeEnd { get; set; }

		// Token: 0x06001A5A RID: 6746 RVA: 0x00060EC4 File Offset: 0x0005F0C4
		public GetModernAttachmentsCommand(CallContext callContext, GetModernAttachmentsRequest request) : this(callContext)
		{
			this.request = request;
			if (this.request.AttachmentsReturnedMax <= 0 || this.request.AttachmentsReturnedMax > 100)
			{
				this.request.AttachmentsReturnedMax = 100;
			}
			if (this.request.ItemsOffset < 0)
			{
				this.request.ItemsOffset = 0;
			}
			if (this.request.ItemsToProcessMax <= 0 || this.request.ItemsToProcessMax > 400)
			{
				this.request.ItemsToProcessMax = 400;
			}
			if (this.request.SortByColumns == null)
			{
				this.request.SortByColumns = GetModernAttachmentsCommand.defaultSortOrder;
			}
			this.itemCreationTimeStart = GetModernAttachmentsCommand.Utilities.TryParseDateTimeString(this.request.ItemCreationTimeStart, ExDateTime.MinValue);
			this.itemCreationTimeEnd = GetModernAttachmentsCommand.Utilities.TryParseDateTimeString(this.request.ItemCreationTimeEnd, ExDateTime.MaxValue);
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x00060FA6 File Offset: 0x0005F1A6
		private GetModernAttachmentsCommand(CallContext callContext) : base(callContext)
		{
			this.perfItemsFoundCount = 0;
			this.perfItemsBoundCount = 0;
			this.perfAttachmentsEnumeratedCount = 0;
			this.perfAttachmentCount = 0;
			this.perfFindItemsDuration = new TimeSpan(0L);
			this.perfBindItemsDuration = new TimeSpan(0L);
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x00060FE8 File Offset: 0x0005F1E8
		static GetModernAttachmentsCommand()
		{
			GetModernAttachmentsCommand.invariantCulture = CultureInfo.InvariantCulture;
			GetModernAttachmentsCommand.findFilterHasAttachment = new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.HasAttachment, true);
			GetModernAttachmentsCommand.findItemsQueryPropDefs = new PropertyDefinition[]
			{
				ItemSchema.Id,
				ItemSchema.ConversationId
			};
			GetModernAttachmentsCommand.bindItemPropDefs = new PropertyDefinition[]
			{
				ItemSchema.Id,
				ItemSchema.ConversationId,
				ItemSchema.AttachmentContent,
				StoreObjectSchema.ItemClass,
				ItemSchema.Subject,
				StoreObjectSchema.CreationTime,
				StoreObjectSchema.LastModifiedTime,
				ItemSchema.Size
			};
			GetModernAttachmentsCommand.sortByMapping = new Dictionary<GetModernAttachmentsRequest.AttachmentsSortBy, StorePropertyDefinition>();
			GetModernAttachmentsCommand.sortByMapping.Add(GetModernAttachmentsRequest.AttachmentsSortBy.ItemClass, StoreObjectSchema.ItemClass);
			GetModernAttachmentsCommand.sortByMapping.Add(GetModernAttachmentsRequest.AttachmentsSortBy.ItemConversationTopic, ItemSchema.ConversationTopic);
			GetModernAttachmentsCommand.sortByMapping.Add(GetModernAttachmentsRequest.AttachmentsSortBy.ItemCreationTime, StoreObjectSchema.CreationTime);
			GetModernAttachmentsCommand.sortByMapping.Add(GetModernAttachmentsRequest.AttachmentsSortBy.ItemLastModifiedTime, StoreObjectSchema.LastModifiedTime);
			GetModernAttachmentsCommand.sortByMapping.Add(GetModernAttachmentsRequest.AttachmentsSortBy.ItemReceivedTime, ItemSchema.ReceivedTime);
			GetModernAttachmentsCommand.sortByMapping.Add(GetModernAttachmentsRequest.AttachmentsSortBy.ItemSentRepresentingDisplayName, ItemSchema.SentRepresentingDisplayName);
			GetModernAttachmentsCommand.sortByMapping.Add(GetModernAttachmentsRequest.AttachmentsSortBy.ItemSize, ItemSchema.Size);
			GetModernAttachmentsCommand.sortByMapping.Add(GetModernAttachmentsRequest.AttachmentsSortBy.ItemSubject, ItemSchema.Subject);
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x00061120 File Offset: 0x0005F320
		protected override GetModernAttachmentsResponse InternalExecute()
		{
			if (this.request != null)
			{
				this.response = new GetModernAttachmentsResponse();
				if (this.request.FolderId != null && this.request.FolderId.Length > 0)
				{
					foreach (BaseFolderId baseFolderId in this.request.FolderId)
					{
						if (this.IsAttachmentCountExceeded())
						{
							return this.response;
						}
						ModernAttachmentGroup modernAttachmentGroup = new ModernAttachmentGroup();
						this.GetAttachmentsFromEwsFolder(baseFolderId, modernAttachmentGroup);
						this.response.AddGroup(modernAttachmentGroup);
					}
				}
				if (this.request.ItemId != null)
				{
					for (int j = 0; j < this.request.ItemId.Length; j++)
					{
						if (this.IsAttachmentCountExceeded())
						{
							return this.response;
						}
						ModernAttachmentGroup modernAttachmentGroup2 = new ModernAttachmentGroup();
						this.GetAttachmentsFromEwsItem(this.request.ItemId[j], modernAttachmentGroup2);
						this.response.AddGroup(modernAttachmentGroup2);
					}
				}
			}
			return this.response;
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x00061218 File Offset: 0x0005F418
		private QueryFilter BuildQueryFilter()
		{
			QueryFilter queryFilter = GetModernAttachmentsCommand.findFilterHasAttachment;
			if (this.itemCreationTimeStart != ExDateTime.MinValue)
			{
				ComparisonFilter comparisonFilter = new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ItemSchema.ReceivedTime, this.itemCreationTimeStart);
				queryFilter = new AndFilter(new QueryFilter[]
				{
					queryFilter,
					comparisonFilter
				});
			}
			if (this.itemCreationTimeEnd != ExDateTime.MaxValue)
			{
				ComparisonFilter comparisonFilter2 = new ComparisonFilter(ComparisonOperator.LessThanOrEqual, ItemSchema.ReceivedTime, this.itemCreationTimeEnd);
				queryFilter = new AndFilter(new QueryFilter[]
				{
					queryFilter,
					comparisonFilter2
				});
			}
			if (!string.IsNullOrEmpty(this.request.ItemFromStart))
			{
				ComparisonFilter comparisonFilter3 = new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ItemSchema.SentRepresentingDisplayName, this.request.ItemFromStart);
				queryFilter = new AndFilter(new QueryFilter[]
				{
					queryFilter,
					comparisonFilter3
				});
			}
			if (!string.IsNullOrEmpty(this.request.ItemFromEnd))
			{
				ComparisonFilter comparisonFilter4 = new ComparisonFilter(ComparisonOperator.LessThanOrEqual, ItemSchema.SentRepresentingDisplayName, this.request.ItemFromEnd);
				queryFilter = new AndFilter(new QueryFilter[]
				{
					queryFilter,
					comparisonFilter4
				});
			}
			return queryFilter;
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x0006133C File Offset: 0x0005F53C
		private SortBy[] BuildSortBy()
		{
			GetModernAttachmentsRequest.AttachmentsSortBy[] sortByColumns = this.request.SortByColumns;
			List<SortBy> list = null;
			if (sortByColumns != null && sortByColumns.Length > 0)
			{
				list = new List<SortBy>(sortByColumns.Length);
				for (int i = 0; i < sortByColumns.Length; i++)
				{
					GetModernAttachmentsRequest.AttachmentsSortBy attachmentsSortBy = sortByColumns[i] & ~GetModernAttachmentsRequest.AttachmentsSortBy.DescendingOrder;
					SortOrder sortOrder = (sortByColumns[i] == attachmentsSortBy) ? SortOrder.Ascending : SortOrder.Descending;
					if (GetModernAttachmentsCommand.sortByMapping.ContainsKey(attachmentsSortBy))
					{
						list.Add(new SortBy(GetModernAttachmentsCommand.sortByMapping[attachmentsSortBy], sortOrder));
					}
				}
			}
			if (list == null)
			{
				return null;
			}
			return list.ToArray();
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x000613C0 File Offset: 0x0005F5C0
		private void GetAttachmentsFromEwsFolder(BaseFolderId baseFolderId, ModernAttachmentGroup outGroup)
		{
			if (baseFolderId == null || outGroup == null)
			{
				return;
			}
			string folderIdDisplayName = GetModernAttachmentsCommand.Utilities.GetFolderIdDisplayName(baseFolderId);
			try
			{
				IdAndSession idAndSession = base.IdConverter.ConvertFolderIdToIdAndSessionReadOnly(baseFolderId);
				ExDateTime now = ExDateTime.Now;
				using (Folder folder = Folder.Bind(idAndSession.Session, idAndSession.Id))
				{
					List<ModernAttachment> list = new List<ModernAttachment>(0);
					SortBy[] sortColumns = this.BuildSortBy();
					QueryFilter queryFilter = this.BuildQueryFilter();
					using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, queryFilter, sortColumns, GetModernAttachmentsCommand.findItemsQueryPropDefs))
					{
						int itemsOffset = this.request.ItemsOffset;
						int num = this.request.ItemsToProcessMax;
						int num2 = itemsOffset + num + 1;
						object[][] rows = queryResult.GetRows(num2 - 1);
						this.perfFindItemsDuration = ExDateTime.Now - now;
						now = ExDateTime.Now;
						outGroup.ItemsTotal = rows.Length;
						if (itemsOffset >= rows.Length)
						{
							return;
						}
						if (num2 > rows.Length)
						{
							num2 = rows.Length;
						}
						outGroup.ItemsOffsetNext = num2;
						outGroup.RetrievedLastItem = (num2 == rows.Length);
						if (rows != null && rows.Length > 0)
						{
							this.perfItemsFoundCount += rows.Length;
							for (int i = itemsOffset; i < num2; i++)
							{
								object[] array = rows[i];
								StoreId storeId = (StoreId)array[0];
								this.GetAttachmentsFromItemByStoreId(idAndSession.Session, storeId, list);
								outGroup.ItemsProcessed++;
							}
						}
						outGroup.AttachmentsReturned = this.perfAttachmentCount;
						this.perfBindItemsDuration = ExDateTime.Now - now;
						outGroup.AttachmentGroup = list.ToArray();
					}
					outGroup.Path = new string[]
					{
						GetModernAttachmentsCommand.Utilities.FormatParam("folderId.DisplayName", folderIdDisplayName),
						GetModernAttachmentsCommand.Utilities.FormatParam("folder.ClassName", folder.ClassName),
						GetModernAttachmentsCommand.Utilities.FormatParam("folder.DisplayName", folder.DisplayName),
						GetModernAttachmentsCommand.Utilities.FormatParam("perfItemsFoundCount", this.perfItemsFoundCount),
						GetModernAttachmentsCommand.Utilities.FormatParam("perfItemsBoundCount", this.perfItemsBoundCount),
						GetModernAttachmentsCommand.Utilities.FormatParam("perfAttachmentsEnumeratedCount", this.perfAttachmentsEnumeratedCount),
						GetModernAttachmentsCommand.Utilities.FormatParam("perfAttachmentCount", this.perfAttachmentCount),
						GetModernAttachmentsCommand.Utilities.FormatParam("perfFindItemsDuration", this.perfFindItemsDuration),
						GetModernAttachmentsCommand.Utilities.FormatParam("perfBindItemsDuration", this.perfBindItemsDuration)
					};
				}
			}
			catch (Exception exception)
			{
				StructuredErrors structuredErrors = this.ConvertToNestedErrors(exception);
				structuredErrors.Path = new string[]
				{
					"GetAttachmentsFromEwsFolder",
					GetModernAttachmentsCommand.Utilities.FormatParam("folderId.DisplayName", folderIdDisplayName),
					GetModernAttachmentsCommand.Utilities.FormatParam("folderId.DisplayName", folderIdDisplayName),
					GetModernAttachmentsCommand.Utilities.FormatParam("perfItemsFoundCount", this.perfItemsFoundCount),
					GetModernAttachmentsCommand.Utilities.FormatParam("perfItemsBoundCount", this.perfItemsBoundCount),
					GetModernAttachmentsCommand.Utilities.FormatParam("perfAttachmentsEnumeratedCount", this.perfAttachmentsEnumeratedCount),
					GetModernAttachmentsCommand.Utilities.FormatParam("perfAttachmentCount", this.perfAttachmentCount),
					GetModernAttachmentsCommand.Utilities.FormatParam("perfFindItemsDuration", this.perfFindItemsDuration),
					GetModernAttachmentsCommand.Utilities.FormatParam("perfBindItemsDuration", this.perfBindItemsDuration)
				};
				this.response.AddError(structuredErrors);
			}
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x00061768 File Offset: 0x0005F968
		private void GetAttachmentsFromEwsItem(BaseItemId baseItemId, ModernAttachmentGroup outGroup)
		{
			if (baseItemId == null || outGroup == null)
			{
				return;
			}
			string itemIdDisplayName = GetModernAttachmentsCommand.Utilities.GetItemIdDisplayName(baseItemId);
			try
			{
				IdAndSession idAndSession = base.IdConverter.ConvertItemIdToIdAndSessionReadOnly(baseItemId);
				using (Item item = Item.Bind(idAndSession.Session, idAndSession.Id))
				{
					List<ModernAttachment> list = new List<ModernAttachment>(0);
					this.GetAttachmentsFromItemByStoreId(item.Session, item.Id, list);
					outGroup.AttachmentGroup = list.ToArray();
					outGroup.Path = new string[]
					{
						GetModernAttachmentsCommand.Utilities.FormatParam("itemId.DisplayName", itemIdDisplayName),
						GetModernAttachmentsCommand.Utilities.FormatParam("itemId.Id", baseItemId.GetId()),
						GetModernAttachmentsCommand.Utilities.FormatParam("itemId.ChangeKey", baseItemId.GetChangeKey())
					};
				}
			}
			catch (Exception exception)
			{
				StructuredErrors structuredErrors = this.ConvertToNestedErrors(exception);
				structuredErrors.Path = new string[]
				{
					"GetAttachmentsFromEwsItem",
					GetModernAttachmentsCommand.Utilities.FormatParam("itemId.DisplayName", itemIdDisplayName),
					GetModernAttachmentsCommand.Utilities.FormatParam("itemId.Id", baseItemId.GetId()),
					GetModernAttachmentsCommand.Utilities.FormatParam("itemId.ChangeKey", baseItemId.GetChangeKey())
				};
				this.response.AddError(structuredErrors);
			}
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x000618A8 File Offset: 0x0005FAA8
		private void GetAttachmentsFromItemByStoreId(StoreSession session, StoreId storeId, List<ModernAttachment> outModernAttachments)
		{
			if (session == null || storeId == null || outModernAttachments == null || this.IsAttachmentCountExceeded())
			{
				return;
			}
			try
			{
				using (Item item = Item.Bind(session, storeId, ItemBindOption.LoadRequiredPropertiesOnly, GetModernAttachmentsCommand.bindItemPropDefs))
				{
					this.perfItemsBoundCount++;
					if (item.AttachmentCollection != null)
					{
						IdAndSession idAndSession = new IdAndSession(item.Id, item.Session);
						IList<AttachmentHandle> handles = item.AttachmentCollection.GetHandles();
						for (int i = 0; i < handles.Count; i++)
						{
							AttachmentHandle attachmentHandle = handles[i];
							using (Attachment attachment = item.AttachmentCollection.Open(attachmentHandle))
							{
								if (GetModernAttachmentsCommand.Utilities.ShowAttachment(attachment, 100000))
								{
									ModernAttachment modernAttachment = new ModernAttachment();
									GetModernAttachmentsCommand.PopulateAttachmentProperties(idAndSession, item, attachment, modernAttachment);
									GetModernAttachmentsCommand.PopulateItemProperties(idAndSession, item, modernAttachment);
									modernAttachment.Info = new ModernAttachment.AttachmentInfo();
									modernAttachment.Info.Index = i;
									modernAttachment.Info.Path = new string[]
									{
										GetModernAttachmentsCommand.Utilities.FormatParam("ATT.N", attachmentHandle.AttachNumber)
									};
									outModernAttachments.Add(modernAttachment);
									this.perfAttachmentCount++;
								}
								this.perfAttachmentsEnumeratedCount++;
							}
						}
					}
				}
			}
			catch (Exception exception)
			{
				StructuredErrors structuredErrors = this.ConvertToNestedErrors(exception);
				structuredErrors.Path = new string[]
				{
					"GetAttachmentsFromItemByStoreId",
					GetModernAttachmentsCommand.Utilities.FormatParam("storeId", storeId.ToBase64String())
				};
				this.response.AddError(structuredErrors);
			}
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x00061A84 File Offset: 0x0005FC84
		private StructuredErrors ConvertToNestedErrors(Exception exception)
		{
			StructuredErrors structuredErrors = null;
			while (exception != null)
			{
				StructuredError structuredError = new StructuredError();
				LocalizedException ex = exception as LocalizedException;
				if (ex != null)
				{
					structuredError.Code = ex.ErrorCode;
					structuredError.Info = ex.LocalizedString;
				}
				else
				{
					structuredError.Code = 0;
					structuredError.Info = exception.ToString();
				}
				Type type = exception.GetType();
				structuredError.Name = type.FullName;
				structuredError.Guid = type.GUID.ToString("n");
				structuredError.Message = exception.Message;
				structuredError.Source = exception.Source;
				structuredError.TargetSite = exception.TargetSite.Name;
				if (structuredErrors == null)
				{
					structuredErrors = new StructuredErrors();
				}
				structuredErrors.AddError(structuredError);
				exception = exception.InnerException;
			}
			return structuredErrors;
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x00061B50 File Offset: 0x0005FD50
		private static void PopulateAttachmentProperties(IdAndSession idAndSession, Item item, Attachment attachment, ModernAttachment outAttachment)
		{
			if (outAttachment == null || attachment == null || item == null)
			{
				return;
			}
			outAttachment.Data = new ModernAttachment.AttachmentData();
			outAttachment.Data.AttachmentEx = new AttachmentTypeEx();
			switch (attachment.AttachmentType)
			{
			case AttachmentType.NoAttachment:
				break;
			case AttachmentType.Stream:
			{
				StreamAttachment streamAttachment = attachment as StreamAttachment;
				if (streamAttachment != null)
				{
					FileAttachmentType fileAttachmentType = new FileAttachmentType();
					outAttachment.Data.Attachment = fileAttachmentType;
					fileAttachmentType.IsContactPhoto = streamAttachment.IsContactPhoto;
				}
				break;
			}
			case AttachmentType.EmbeddedMessage:
			{
				ItemAttachment itemAttachment = attachment as ItemAttachment;
				if (itemAttachment != null)
				{
					outAttachment.Data.Attachment = new ItemAttachmentType();
				}
				break;
			}
			case AttachmentType.Ole:
			{
				OleAttachment oleAttachment = attachment as OleAttachment;
				if (oleAttachment != null)
				{
					FileAttachmentType fileAttachmentType2 = new FileAttachmentType();
					outAttachment.Data.Attachment = fileAttachmentType2;
					fileAttachmentType2.IsContactPhoto = oleAttachment.IsContactPhoto;
				}
				break;
			}
			case AttachmentType.Reference:
			{
				ReferenceAttachment referenceAttachment = attachment as ReferenceAttachment;
				if (referenceAttachment != null)
				{
					UserContext userContext = UserContextManager.GetUserContext(HttpContext.Current);
					if (userContext != null && userContext.FeaturesManager != null && userContext.FeaturesManager.ClientServerSettings.AttachmentsFilePicker.Enabled)
					{
						ReferenceAttachmentType referenceAttachmentType = new ReferenceAttachmentType();
						outAttachment.Data.Attachment = referenceAttachmentType;
						referenceAttachmentType.AttachLongPathName = referenceAttachment.AttachLongPathName;
						referenceAttachmentType.ProviderEndpointUrl = referenceAttachment.ProviderEndpointUrl;
						referenceAttachmentType.ProviderType = referenceAttachment.ProviderType;
					}
					else
					{
						outAttachment.Data.Attachment = new FileAttachmentType();
					}
				}
				break;
			}
			default:
				return;
			}
			if (outAttachment.Data.Attachment == null)
			{
				outAttachment.Data.Attachment = new AttachmentType();
			}
			IdAndSession idAndSession2;
			if (idAndSession == null)
			{
				idAndSession2 = new IdAndSession(item.Id, item.Session);
			}
			else
			{
				idAndSession2 = idAndSession.Clone();
			}
			idAndSession2.AttachmentIds.Add(attachment.Id);
			outAttachment.Data.Attachment.AttachmentId = new AttachmentIdType(idAndSession2.GetConcatenatedId().Id);
			outAttachment.Data.Attachment.ContentId = (string.IsNullOrEmpty(attachment.ContentId) ? null : attachment.ContentId);
			outAttachment.Data.Attachment.ContentLocation = ((attachment.ContentLocation == null) ? null : attachment.ContentLocation.PathAndQuery);
			outAttachment.Data.Attachment.ContentType = attachment.ContentType;
			outAttachment.Data.Attachment.IsInline = attachment.IsInline;
			outAttachment.Data.Attachment.LastModifiedTime = GetModernAttachmentsCommand.Utilities.FormatExDateTime(attachment.LastModifiedTime);
			outAttachment.Data.Attachment.Name = attachment.DisplayName;
			outAttachment.Data.Attachment.Size = (int)attachment.Size;
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x00061DFC File Offset: 0x0005FFFC
		private static void PopulateItemProperties(IdAndSession idAndSession, Item item, ModernAttachment outAttachment)
		{
			ItemType itemType = ItemType.CreateFromStoreObjectType(item.StoreObjectId.ObjectType);
			itemType.ItemClass = item.ClassName;
			itemType.Subject = (item[ItemSchema.Subject] as string);
			itemType.ItemId = GetModernAttachmentsCommand.Utilities.GetItemId(idAndSession, item.Id);
			itemType.DateTimeCreated = GetModernAttachmentsCommand.Utilities.FormatExDateTime(item.CreationTime);
			itemType.LastModifiedTime = GetModernAttachmentsCommand.Utilities.FormatExDateTime(item.LastModifiedTime);
			itemType.Size = new int?((int)item.Size());
			ConversationId conversationId = PropertyCommand.GetPropertyValueFromStoreObject(item, ItemSchema.ConversationId) as ConversationId;
			string id = IdConverter.ConversationIdToEwsId(idAndSession.Session.MailboxGuid, conversationId);
			itemType.ConversationId = new ItemId(id, null);
			outAttachment.Data.Item = itemType;
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x00061EBE File Offset: 0x000600BE
		private bool IsAttachmentCountExceeded()
		{
			return this.perfAttachmentCount > this.request.AttachmentsReturnedMax;
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x00061ED3 File Offset: 0x000600D3
		private void WriteTrace(string message)
		{
		}

		// Token: 0x04000E95 RID: 3733
		private const int itemsToProcessMax = 400;

		// Token: 0x04000E96 RID: 3734
		private const int attachmentsReturnedMax = 100;

		// Token: 0x04000E97 RID: 3735
		private const int inpAttachmentImageSizeMax = 100000;

		// Token: 0x04000E98 RID: 3736
		private static CultureInfo invariantCulture;

		// Token: 0x04000E99 RID: 3737
		private static QueryFilter findFilterHasAttachment;

		// Token: 0x04000E9A RID: 3738
		private static PropertyDefinition[] findItemsQueryPropDefs;

		// Token: 0x04000E9B RID: 3739
		private static PropertyDefinition[] bindItemPropDefs;

		// Token: 0x04000E9C RID: 3740
		private static Dictionary<GetModernAttachmentsRequest.AttachmentsSortBy, StorePropertyDefinition> sortByMapping;

		// Token: 0x04000E9D RID: 3741
		private static GetModernAttachmentsRequest.AttachmentsSortBy[] defaultSortOrder = new GetModernAttachmentsRequest.AttachmentsSortBy[]
		{
			GetModernAttachmentsRequest.AttachmentsSortBy.ItemReceivedTime | GetModernAttachmentsRequest.AttachmentsSortBy.DescendingOrder
		};

		// Token: 0x04000E9E RID: 3742
		private int perfItemsFoundCount;

		// Token: 0x04000E9F RID: 3743
		private int perfItemsBoundCount;

		// Token: 0x04000EA0 RID: 3744
		private int perfAttachmentsEnumeratedCount;

		// Token: 0x04000EA1 RID: 3745
		private int perfAttachmentCount;

		// Token: 0x04000EA2 RID: 3746
		private TimeSpan perfFindItemsDuration;

		// Token: 0x04000EA3 RID: 3747
		private TimeSpan perfBindItemsDuration;

		// Token: 0x04000EA4 RID: 3748
		private GetModernAttachmentsRequest request;

		// Token: 0x04000EA5 RID: 3749
		private GetModernAttachmentsResponse response;

		// Token: 0x0200031A RID: 794
		private static class Utilities
		{
			// Token: 0x06001A69 RID: 6761 RVA: 0x00061EE4 File Offset: 0x000600E4
			public static string GetHexString(byte[] bytes)
			{
				if (bytes == null || bytes.Length == 0)
				{
					return null;
				}
				StringBuilder stringBuilder = new StringBuilder();
				foreach (byte b in bytes)
				{
					stringBuilder.AppendFormat("{0:x2}", b);
				}
				return stringBuilder.ToString();
			}

			// Token: 0x06001A6A RID: 6762 RVA: 0x00061F30 File Offset: 0x00060130
			public static string FormatParam(string name, object value)
			{
				return string.Format(GetModernAttachmentsCommand.invariantCulture, name + "={0}", new object[]
				{
					value
				});
			}

			// Token: 0x06001A6B RID: 6763 RVA: 0x00061F5E File Offset: 0x0006015E
			public static string FormatExDateTime(ExDateTime dateTime)
			{
				return dateTime.ToString("s", GetModernAttachmentsCommand.invariantCulture);
			}

			// Token: 0x06001A6C RID: 6764 RVA: 0x00061F74 File Offset: 0x00060174
			public static string GetFolderIdDisplayName(BaseFolderId baseFolderId)
			{
				FolderId folderId = baseFolderId as FolderId;
				string result;
				if (folderId != null)
				{
					result = folderId.Id;
				}
				else
				{
					DistinguishedFolderId distinguishedFolderId = baseFolderId as DistinguishedFolderId;
					if (distinguishedFolderId != null)
					{
						result = distinguishedFolderId.Id.ToString();
					}
					else
					{
						result = null;
					}
				}
				return result;
			}

			// Token: 0x06001A6D RID: 6765 RVA: 0x00061FB4 File Offset: 0x000601B4
			public static string GetItemIdDisplayName(BaseItemId baseItemId)
			{
				ItemId itemId = baseItemId as ItemId;
				string text = baseItemId.GetType().Name + ":";
				if (itemId != null)
				{
					text += itemId.Id;
				}
				else
				{
					RecurringMasterItemId recurringMasterItemId = baseItemId as RecurringMasterItemId;
					if (recurringMasterItemId != null)
					{
						text += recurringMasterItemId.OccurrenceId;
					}
					else
					{
						OccurrenceItemId occurrenceItemId = baseItemId as OccurrenceItemId;
						if (occurrenceItemId != null)
						{
							object obj = text;
							text = string.Concat(new object[]
							{
								obj,
								occurrenceItemId.RecurringMasterId,
								"[",
								occurrenceItemId.InstanceIndex,
								"]"
							});
						}
						else
						{
							RecurringMasterItemIdRanges recurringMasterItemIdRanges = baseItemId as RecurringMasterItemIdRanges;
							if (recurringMasterItemIdRanges != null)
							{
								object obj2 = text;
								text = string.Concat(new object[]
								{
									obj2,
									recurringMasterItemIdRanges.Id,
									"Ranges[",
									recurringMasterItemIdRanges.Ranges.Length,
									"]"
								});
							}
						}
					}
				}
				return text;
			}

			// Token: 0x06001A6E RID: 6766 RVA: 0x000620B4 File Offset: 0x000602B4
			public static ItemId GetItemId(IdAndSession idAndSession, StoreId storeId)
			{
				ConcatenatedIdAndChangeKey concatenatedIdAndChangeKey;
				if (idAndSession.Session is PublicFolderSession)
				{
					concatenatedIdAndChangeKey = IdConverter.GetConcatenatedIdForPublicFolderItem(storeId, StoreId.GetStoreObjectId(idAndSession.ParentFolderId), null);
				}
				else
				{
					concatenatedIdAndChangeKey = IdConverter.GetConcatenatedId(storeId, idAndSession, null);
				}
				return new ItemId(concatenatedIdAndChangeKey.Id, concatenatedIdAndChangeKey.ChangeKey);
			}

			// Token: 0x06001A6F RID: 6767 RVA: 0x000620FF File Offset: 0x000602FF
			public static bool IsImageExt(string attachmentExt)
			{
				return GetModernAttachmentsCommand.Utilities.imageExt.Contains(attachmentExt.ToUpperInvariant());
			}

			// Token: 0x06001A70 RID: 6768 RVA: 0x00062111 File Offset: 0x00060311
			public static bool ShowAttachment(Attachment attachment, int minSize)
			{
				return !attachment.IsInline && attachment.AttachmentType != AttachmentType.EmbeddedMessage && !GetModernAttachmentsCommand.Utilities.IsImageExt(attachment.FileExtension);
			}

			// Token: 0x06001A71 RID: 6769 RVA: 0x00062134 File Offset: 0x00060334
			public static ExDateTime TryParseDateTimeString(string dateTime, ExDateTime defaultValue)
			{
				if (string.IsNullOrEmpty(dateTime))
				{
					return defaultValue;
				}
				return ExDateTime.Parse(ExTimeZone.UtcTimeZone, dateTime, GetModernAttachmentsCommand.invariantCulture);
			}

			// Token: 0x04000EA8 RID: 3752
			private static readonly string imageExt = ".JPG\0.JPEG\0.JPE\0.JFIF\0.GIF\0.PNG\0.BMP\0.ICO";
		}
	}
}
