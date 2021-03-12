using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;
using Microsoft.Exchange.EDiscovery.Export;
using Microsoft.Exchange.EDiscovery.Export.EwsProxy;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch
{
	// Token: 0x02000007 RID: 7
	internal class MailboxItemIdList : IItemIdList
	{
		// Token: 0x06000033 RID: 51 RVA: 0x000025E8 File Offset: 0x000007E8
		public MailboxItemIdList(string targetPrimarySmtpAddress, string sourcePrimarySmtpAddress, string workingLocation, IEwsClient ewsClient, bool isUnsearchable)
		{
			Util.ThrowIfNullOrEmpty(targetPrimarySmtpAddress, "targetPrimarySmtpAddress");
			Util.ThrowIfNullOrEmpty(workingLocation, "workingLocation");
			Util.ThrowIfNull(ewsClient, "ewsClient");
			this.targetPrimarySmtpAddress = targetPrimarySmtpAddress;
			this.sourcePrimarySmtpAddress = sourcePrimarySmtpAddress;
			this.workingLocation = workingLocation;
			this.ewsClient = ewsClient;
			this.memoryCache = new List<ItemId>(ConstantProvider.ItemIdListCacheSize);
			this.IsUnsearchable = isUnsearchable;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000265D File Offset: 0x0000085D
		public string SourceId
		{
			get
			{
				return this.sourcePrimarySmtpAddress;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002665 File Offset: 0x00000865
		public IList<ItemId> MemoryCache
		{
			get
			{
				return this.memoryCache;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000266D File Offset: 0x0000086D
		public bool Exists
		{
			get
			{
				this.PopulateWorkingFolders();
				return this.workingFolder != null && this.workingSubFolder != null;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000268B File Offset: 0x0000088B
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002693 File Offset: 0x00000893
		public bool IsUnsearchable { get; private set; }

		// Token: 0x06000039 RID: 57 RVA: 0x0000269C File Offset: 0x0000089C
		public void WriteItemId(ItemId itemId)
		{
			lock (this.syncObject)
			{
				this.memoryCache.Add(itemId);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000026E4 File Offset: 0x000008E4
		public void Flush()
		{
			lock (this.syncObject)
			{
				if (this.memoryCache.Count > 0)
				{
					this.CreateWorkingFolderIfNotExist();
					int num = 0;
					FolderIdType folderId = this.workingSubFolder.FolderId;
					List<ItemType> list = new List<ItemType>(Math.Min(this.memoryCache.Count, Constants.ReadWriteBatchSize));
					foreach (ItemId itemId in this.memoryCache)
					{
						list.Add(new ItemType
						{
							ExtendedProperty = this.CreateItemExtendedPropertyValuesFromItemId(itemId)
						});
						num++;
						if (list.Count == Constants.ReadWriteBatchSize || num == this.memoryCache.Count)
						{
							List<ItemType> list2 = this.ewsClient.CreateItems(this.targetPrimarySmtpAddress, folderId, list.ToArray());
							if (list2 == null || list2.Count != list.Count)
							{
								throw new ExportException(ExportErrorType.NumberOfItemsMismatched, string.Format("Expected number of items ({0}) supposed to write to the list is not matched with actual ({1})", list.Count, list2.Count));
							}
							list.Clear();
						}
					}
				}
				this.memoryCache.Clear();
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002ABC File Offset: 0x00000CBC
		public IEnumerable<ItemId> ReadItemIds()
		{
			if (this.Exists)
			{
				int offset = 0;
				FolderIdType folderId = this.workingSubFolder.FolderId;
				for (;;)
				{
					List<ItemType> items = this.ewsClient.RetrieveItems(this.targetPrimarySmtpAddress, folderId, MailboxItemIdList.AdditionalProperties, null, false, new int?(Constants.ReadWriteBatchSize), offset);
					int totalItemsReturned = (items != null) ? items.Count : 0;
					if (totalItemsReturned == 0)
					{
						break;
					}
					offset += totalItemsReturned;
					foreach (ItemType item in items)
					{
						ItemId retItemId = this.PopulateItemIdFromItemExtendedPropertyValues(item.ExtendedProperty);
						if (retItemId != null)
						{
							yield return retItemId;
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002ADC File Offset: 0x00000CDC
		public void Delete()
		{
			this.PopulateWorkingFolders();
			if (!string.IsNullOrEmpty(this.sourcePrimarySmtpAddress))
			{
				if (this.workingSubFolder != null)
				{
					this.ewsClient.DeleteFolder(this.targetPrimarySmtpAddress, new BaseFolderIdType[]
					{
						this.workingSubFolder.FolderId
					});
					this.workingSubFolder = null;
					return;
				}
			}
			else if (this.workingFolder != null)
			{
				this.ewsClient.DeleteFolder(this.targetPrimarySmtpAddress, new BaseFolderIdType[]
				{
					this.workingFolder.FolderId
				});
				this.workingFolder = null;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002B6C File Offset: 0x00000D6C
		private static string EncodeDataForEws(string data)
		{
			if (string.IsNullOrEmpty(data))
			{
				return data;
			}
			string text = Convert.ToBase64String(Encoding.UTF7.GetBytes(data));
			if (text.Length > 255)
			{
				text = text.Substring(0, 252);
			}
			return text;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002BB0 File Offset: 0x00000DB0
		private static string DecodeDataFromEws(string data)
		{
			if (string.IsNullOrEmpty(data))
			{
				return data;
			}
			string result = string.Empty;
			try
			{
				result = Encoding.UTF7.GetString(Convert.FromBase64String(data));
			}
			catch (Exception arg)
			{
				ExTraceGlobals.SearchTracer.TraceError<Exception>(0L, "DecodeDataFromEws: Exception: {0}", arg);
			}
			return result;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002C08 File Offset: 0x00000E08
		private void PopulateWorkingFolders()
		{
			if (this.workingFolder == null)
			{
				this.workingFolder = this.GetWorkingFolder();
			}
			if (this.workingSubFolder == null && !string.IsNullOrEmpty(this.sourcePrimarySmtpAddress))
			{
				this.workingSubFolder = this.ewsClient.GetFolderByName(this.targetPrimarySmtpAddress, this.workingFolder.FolderId, this.sourcePrimarySmtpAddress);
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002C68 File Offset: 0x00000E68
		private BaseFolderType GetWorkingFolder()
		{
			BaseFolderIdType parentFolderId = new DistinguishedFolderIdType
			{
				Id = DistinguishedFolderIdNameType.msgfolderroot,
				Mailbox = new EmailAddressType
				{
					EmailAddress = this.targetPrimarySmtpAddress
				}
			};
			string text = this.IsUnsearchable ? (this.workingLocation + ".unsearchable") : this.workingLocation;
			BaseFolderType baseFolderType = this.ewsClient.GetFolderByName(this.targetPrimarySmtpAddress, parentFolderId, text);
			if (baseFolderType == null)
			{
				FolderType folderType = new FolderType
				{
					DisplayName = text
				};
				folderType.ExtendedProperty = new ExtendedPropertyType[]
				{
					MailboxItemIdList.IsHiddenExtendedProperty
				};
				List<BaseFolderType> list = this.ewsClient.CreateFolder(this.targetPrimarySmtpAddress, parentFolderId, new BaseFolderType[]
				{
					folderType
				});
				baseFolderType = list[0];
			}
			return baseFolderType;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002D38 File Offset: 0x00000F38
		private void CreateWorkingFolderIfNotExist()
		{
			if (!this.Exists)
			{
				BaseFolderType[] folders = new BaseFolderType[]
				{
					new FolderType
					{
						DisplayName = this.sourcePrimarySmtpAddress
					}
				};
				this.ewsClient.CreateFolder(this.targetPrimarySmtpAddress, this.workingFolder.FolderId, folders);
				this.PopulateWorkingFolders();
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002D90 File Offset: 0x00000F90
		private ExtendedPropertyType[] CreateItemExtendedPropertyValuesFromItemId(ItemId itemId)
		{
			List<ExtendedPropertyType> list = new List<ExtendedPropertyType>(10);
			if (!string.IsNullOrEmpty(itemId.Id))
			{
				list.Add(new ExtendedPropertyType
				{
					ExtendedFieldURI = MailboxItemIdList.ItemIdExtendedProperty,
					Item = itemId.Id
				});
			}
			if (!string.IsNullOrEmpty(itemId.ParentFolder))
			{
				list.Add(new ExtendedPropertyType
				{
					ExtendedFieldURI = MailboxItemIdList.ParentFolderIdExtendedProperty,
					Item = itemId.ParentFolder
				});
			}
			if (!string.IsNullOrEmpty(itemId.PrimaryItemId))
			{
				list.Add(new ExtendedPropertyType
				{
					ExtendedFieldURI = MailboxItemIdList.PrimaryItemIdExtendedProperty,
					Item = itemId.PrimaryItemId
				});
			}
			if (itemId.Size != 0U)
			{
				list.Add(new ExtendedPropertyType
				{
					ExtendedFieldURI = MailboxItemIdList.ItemSizeExtendedProperty,
					Item = itemId.Size.ToString()
				});
			}
			if (!string.IsNullOrEmpty(itemId.Subject))
			{
				list.Add(new ExtendedPropertyType
				{
					ExtendedFieldURI = MailboxItemIdList.SubjectExtendedProperty,
					Item = MailboxItemIdList.EncodeDataForEws(itemId.Subject)
				});
			}
			if (itemId.DocumentId != 0)
			{
				list.Add(new ExtendedPropertyType
				{
					ExtendedFieldURI = MailboxItemIdList.DocumentIdExtendedProperty,
					Item = itemId.DocumentId.ToString()
				});
			}
			if (!string.IsNullOrEmpty(itemId.Sender))
			{
				list.Add(new ExtendedPropertyType
				{
					ExtendedFieldURI = MailboxItemIdList.SenderExtendedProperty,
					Item = MailboxItemIdList.EncodeDataForEws(itemId.Sender)
				});
			}
			if (!string.IsNullOrEmpty(itemId.SenderSmtpAddress))
			{
				list.Add(new ExtendedPropertyType
				{
					ExtendedFieldURI = MailboxItemIdList.SenderSmtpAddressExtendedProperty,
					Item = MailboxItemIdList.EncodeDataForEws(itemId.SenderSmtpAddress)
				});
			}
			if (itemId.ReceivedTime != DateTime.MinValue)
			{
				list.Add(new ExtendedPropertyType
				{
					ExtendedFieldURI = MailboxItemIdList.ReceivedTimeExtendedProperty,
					Item = itemId.ReceivedTime.ToString()
				});
			}
			if (itemId.SentTime != DateTime.MinValue)
			{
				list.Add(new ExtendedPropertyType
				{
					ExtendedFieldURI = MailboxItemIdList.SentTimeExtendedProperty,
					Item = itemId.SentTime.ToString()
				});
			}
			if (!string.IsNullOrEmpty(itemId.Importance))
			{
				list.Add(new ExtendedPropertyType
				{
					ExtendedFieldURI = MailboxItemIdList.ImportanceExtendedProperty,
					Item = itemId.Importance
				});
			}
			list.Add(new ExtendedPropertyType
			{
				ExtendedFieldURI = MailboxItemIdList.IsReadExtendedProperty,
				Item = itemId.IsRead.ToString()
			});
			if (!string.IsNullOrEmpty(itemId.UniqueHash))
			{
				UniqueItemHash uniqueItemHash = UniqueItemHash.Parse(itemId.UniqueHash);
				if (!string.IsNullOrEmpty(uniqueItemHash.InternetMessageId))
				{
					list.Add(new ExtendedPropertyType
					{
						ExtendedFieldURI = MailboxItemIdList.InternetMessageIdExtendedProperty,
						Item = uniqueItemHash.InternetMessageId
					});
				}
				if (!string.IsNullOrEmpty(uniqueItemHash.ConversationTopic))
				{
					list.Add(new ExtendedPropertyType
					{
						ExtendedFieldURI = MailboxItemIdList.ConversationTopicExtendedProperty,
						Item = MailboxItemIdList.EncodeDataForEws(uniqueItemHash.ConversationTopic)
					});
				}
				list.Add(new ExtendedPropertyType
				{
					ExtendedFieldURI = MailboxItemIdList.IsSentItemsExtendedProperty,
					Item = uniqueItemHash.IsSentItems.ToString()
				});
				if (uniqueItemHash.BodyTagInfo != null)
				{
					byte[] inArray = uniqueItemHash.BodyTagInfo.ToByteArray();
					string item = Convert.ToBase64String(inArray);
					list.Add(new ExtendedPropertyType
					{
						ExtendedFieldURI = MailboxItemIdList.BodyTagInfoExtendedProperty,
						Item = item
					});
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003164 File Offset: 0x00001364
		private ItemId PopulateItemIdFromItemExtendedPropertyValues(ExtendedPropertyType[] extendedProperties)
		{
			string id = null;
			string parentFolder = null;
			string primaryItemId = null;
			uint size = 0U;
			string subject = null;
			int documentId = 0;
			string text = null;
			string sender = null;
			string senderSmtpAddress = null;
			DateTime minValue = DateTime.MinValue;
			DateTime minValue2 = DateTime.MinValue;
			string importance = null;
			bool isRead = false;
			string text2 = null;
			BodyTagInfo btInfo = null;
			bool sentItems = false;
			if (extendedProperties != null)
			{
				foreach (ExtendedPropertyType extendedPropertyType in extendedProperties)
				{
					if (string.Compare(extendedPropertyType.ExtendedFieldURI.PropertySetId, Constants.MailboxItemIdPropertyIdSet, StringComparison.OrdinalIgnoreCase) == 0)
					{
						if (string.Compare(extendedPropertyType.ExtendedFieldURI.PropertyName, MailboxItemIdList.ItemIdExtendedProperty.PropertyName, StringComparison.OrdinalIgnoreCase) == 0 && extendedPropertyType.ExtendedFieldURI.PropertyType == MailboxItemIdList.ItemIdExtendedProperty.PropertyType)
						{
							id = (string)extendedPropertyType.Item;
						}
						else if (string.Compare(extendedPropertyType.ExtendedFieldURI.PropertyName, MailboxItemIdList.ParentFolderIdExtendedProperty.PropertyName, StringComparison.OrdinalIgnoreCase) == 0 && extendedPropertyType.ExtendedFieldURI.PropertyType == MailboxItemIdList.ParentFolderIdExtendedProperty.PropertyType)
						{
							parentFolder = (string)extendedPropertyType.Item;
						}
						else if (string.Compare(extendedPropertyType.ExtendedFieldURI.PropertyName, MailboxItemIdList.PrimaryItemIdExtendedProperty.PropertyName, StringComparison.OrdinalIgnoreCase) == 0 && extendedPropertyType.ExtendedFieldURI.PropertyType == MailboxItemIdList.PrimaryItemIdExtendedProperty.PropertyType)
						{
							primaryItemId = (string)extendedPropertyType.Item;
						}
						else if (string.Compare(extendedPropertyType.ExtendedFieldURI.PropertyName, MailboxItemIdList.ItemSizeExtendedProperty.PropertyName, StringComparison.OrdinalIgnoreCase) == 0 && extendedPropertyType.ExtendedFieldURI.PropertyType == MailboxItemIdList.ItemSizeExtendedProperty.PropertyType)
						{
							uint.TryParse(extendedPropertyType.Item as string, out size);
						}
						else if (string.Compare(extendedPropertyType.ExtendedFieldURI.PropertyName, MailboxItemIdList.DocumentIdExtendedProperty.PropertyName, StringComparison.OrdinalIgnoreCase) == 0 && extendedPropertyType.ExtendedFieldURI.PropertyType == MailboxItemIdList.DocumentIdExtendedProperty.PropertyType)
						{
							int.TryParse(extendedPropertyType.Item as string, out documentId);
						}
						else if (string.Compare(extendedPropertyType.ExtendedFieldURI.PropertyName, MailboxItemIdList.SubjectExtendedProperty.PropertyName, StringComparison.OrdinalIgnoreCase) == 0 && extendedPropertyType.ExtendedFieldURI.PropertyType == MailboxItemIdList.SubjectExtendedProperty.PropertyType)
						{
							subject = MailboxItemIdList.DecodeDataFromEws((string)extendedPropertyType.Item);
						}
						else if (string.Compare(extendedPropertyType.ExtendedFieldURI.PropertyName, MailboxItemIdList.SenderExtendedProperty.PropertyName, StringComparison.OrdinalIgnoreCase) == 0 && extendedPropertyType.ExtendedFieldURI.PropertyType == MailboxItemIdList.SenderExtendedProperty.PropertyType)
						{
							sender = MailboxItemIdList.DecodeDataFromEws((string)extendedPropertyType.Item);
						}
						else if (string.Compare(extendedPropertyType.ExtendedFieldURI.PropertyName, MailboxItemIdList.SenderSmtpAddressExtendedProperty.PropertyName, StringComparison.OrdinalIgnoreCase) == 0 && extendedPropertyType.ExtendedFieldURI.PropertyType == MailboxItemIdList.SenderExtendedProperty.PropertyType)
						{
							senderSmtpAddress = MailboxItemIdList.DecodeDataFromEws((string)extendedPropertyType.Item);
						}
						else if (string.Compare(extendedPropertyType.ExtendedFieldURI.PropertyName, MailboxItemIdList.ReceivedTimeExtendedProperty.PropertyName, StringComparison.OrdinalIgnoreCase) == 0 && extendedPropertyType.ExtendedFieldURI.PropertyType == MailboxItemIdList.ReceivedTimeExtendedProperty.PropertyType)
						{
							DateTime.TryParse(extendedPropertyType.Item as string, out minValue);
						}
						else if (string.Compare(extendedPropertyType.ExtendedFieldURI.PropertyName, MailboxItemIdList.SentTimeExtendedProperty.PropertyName, StringComparison.OrdinalIgnoreCase) == 0 && extendedPropertyType.ExtendedFieldURI.PropertyType == MailboxItemIdList.SentTimeExtendedProperty.PropertyType)
						{
							DateTime.TryParse(extendedPropertyType.Item as string, out minValue2);
						}
						else if (string.Compare(extendedPropertyType.ExtendedFieldURI.PropertyName, MailboxItemIdList.ImportanceExtendedProperty.PropertyName, StringComparison.OrdinalIgnoreCase) == 0 && extendedPropertyType.ExtendedFieldURI.PropertyType == MailboxItemIdList.ImportanceExtendedProperty.PropertyType)
						{
							importance = (string)extendedPropertyType.Item;
						}
						else if (string.Compare(extendedPropertyType.ExtendedFieldURI.PropertyName, MailboxItemIdList.IsReadExtendedProperty.PropertyName, StringComparison.OrdinalIgnoreCase) == 0 && extendedPropertyType.ExtendedFieldURI.PropertyType == MailboxItemIdList.IsReadExtendedProperty.PropertyType)
						{
							bool.TryParse(extendedPropertyType.Item as string, out isRead);
						}
						else if (string.Compare(extendedPropertyType.ExtendedFieldURI.PropertyName, MailboxItemIdList.InternetMessageIdExtendedProperty.PropertyName, StringComparison.OrdinalIgnoreCase) == 0 && extendedPropertyType.ExtendedFieldURI.PropertyType == MailboxItemIdList.InternetMessageIdExtendedProperty.PropertyType)
						{
							text = (string)extendedPropertyType.Item;
						}
						else if (string.Compare(extendedPropertyType.ExtendedFieldURI.PropertyName, MailboxItemIdList.ConversationTopicExtendedProperty.PropertyName, StringComparison.OrdinalIgnoreCase) == 0 && extendedPropertyType.ExtendedFieldURI.PropertyType == MailboxItemIdList.ConversationTopicExtendedProperty.PropertyType)
						{
							text2 = MailboxItemIdList.DecodeDataFromEws((string)extendedPropertyType.Item);
						}
						else if (string.Compare(extendedPropertyType.ExtendedFieldURI.PropertyName, MailboxItemIdList.IsSentItemsExtendedProperty.PropertyName, StringComparison.OrdinalIgnoreCase) == 0 && extendedPropertyType.ExtendedFieldURI.PropertyType == MailboxItemIdList.IsSentItemsExtendedProperty.PropertyType)
						{
							sentItems = bool.Parse((string)extendedPropertyType.Item);
						}
						else if (string.Compare(extendedPropertyType.ExtendedFieldURI.PropertyName, MailboxItemIdList.BodyTagInfoExtendedProperty.PropertyName, StringComparison.OrdinalIgnoreCase) == 0 && extendedPropertyType.ExtendedFieldURI.PropertyType == MailboxItemIdList.BodyTagInfoExtendedProperty.PropertyType)
						{
							string s = (string)extendedPropertyType.Item;
							byte[] byteArray = Convert.FromBase64String(s);
							btInfo = BodyTagInfo.FromByteArray(byteArray);
						}
					}
				}
				string uniqueHash = null;
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
				{
					uniqueHash = new UniqueItemHash(text, text2, btInfo, sentItems).ToString();
				}
				return new ItemId
				{
					Id = id,
					ParentFolder = parentFolder,
					PrimaryItemId = primaryItemId,
					Size = size,
					Subject = subject,
					DocumentId = documentId,
					InternetMessageId = text,
					Sender = sender,
					SenderSmtpAddress = senderSmtpAddress,
					ReceivedTime = minValue,
					SentTime = minValue2,
					Importance = importance,
					IsRead = isRead,
					UniqueHash = uniqueHash,
					SourceId = this.sourcePrimarySmtpAddress
				};
			}
			return null;
		}

		// Token: 0x04000023 RID: 35
		private const string UnsearchableSuffix = ".unsearchable";

		// Token: 0x04000024 RID: 36
		internal static readonly ExtendedPropertyType IsHiddenExtendedProperty = new ExtendedPropertyType
		{
			ExtendedFieldURI = new PathToExtendedFieldType
			{
				PropertyTag = "0x10F4",
				PropertyType = MapiPropertyTypeType.Boolean
			},
			Item = "true"
		};

		// Token: 0x04000025 RID: 37
		private static readonly PathToExtendedFieldType ItemIdExtendedProperty = new PathToExtendedFieldType
		{
			PropertySetId = Constants.MailboxItemIdPropertyIdSet,
			PropertyName = "ItemId",
			PropertyType = MapiPropertyTypeType.String
		};

		// Token: 0x04000026 RID: 38
		private static readonly PathToExtendedFieldType ParentFolderIdExtendedProperty = new PathToExtendedFieldType
		{
			PropertySetId = Constants.MailboxItemIdPropertyIdSet,
			PropertyName = "ParentFolderId",
			PropertyType = MapiPropertyTypeType.String
		};

		// Token: 0x04000027 RID: 39
		private static readonly PathToExtendedFieldType PrimaryItemIdExtendedProperty = new PathToExtendedFieldType
		{
			PropertySetId = Constants.MailboxItemIdPropertyIdSet,
			PropertyName = "PrimaryItemId",
			PropertyType = MapiPropertyTypeType.String
		};

		// Token: 0x04000028 RID: 40
		private static readonly PathToExtendedFieldType ItemSizeExtendedProperty = new PathToExtendedFieldType
		{
			PropertySetId = Constants.MailboxItemIdPropertyIdSet,
			PropertyName = "ItemSize",
			PropertyType = MapiPropertyTypeType.Long
		};

		// Token: 0x04000029 RID: 41
		private static readonly PathToExtendedFieldType SubjectExtendedProperty = new PathToExtendedFieldType
		{
			PropertySetId = Constants.MailboxItemIdPropertyIdSet,
			PropertyName = "Subject",
			PropertyType = MapiPropertyTypeType.String
		};

		// Token: 0x0400002A RID: 42
		private static readonly PathToExtendedFieldType DocumentIdExtendedProperty = new PathToExtendedFieldType
		{
			PropertySetId = Constants.MailboxItemIdPropertyIdSet,
			PropertyName = "DocumentId",
			PropertyType = MapiPropertyTypeType.Integer
		};

		// Token: 0x0400002B RID: 43
		private static readonly PathToExtendedFieldType SenderExtendedProperty = new PathToExtendedFieldType
		{
			PropertySetId = Constants.MailboxItemIdPropertyIdSet,
			PropertyName = "Sender",
			PropertyType = MapiPropertyTypeType.String
		};

		// Token: 0x0400002C RID: 44
		private static readonly PathToExtendedFieldType SenderSmtpAddressExtendedProperty = new PathToExtendedFieldType
		{
			PropertySetId = Constants.MailboxItemIdPropertyIdSet,
			PropertyName = "SenderSmtpAddress",
			PropertyType = MapiPropertyTypeType.String
		};

		// Token: 0x0400002D RID: 45
		private static readonly PathToExtendedFieldType ReceivedTimeExtendedProperty = new PathToExtendedFieldType
		{
			PropertySetId = Constants.MailboxItemIdPropertyIdSet,
			PropertyName = "ReceivedTime",
			PropertyType = MapiPropertyTypeType.String
		};

		// Token: 0x0400002E RID: 46
		private static readonly PathToExtendedFieldType SentTimeExtendedProperty = new PathToExtendedFieldType
		{
			PropertySetId = Constants.MailboxItemIdPropertyIdSet,
			PropertyName = "SentTime",
			PropertyType = MapiPropertyTypeType.String
		};

		// Token: 0x0400002F RID: 47
		private static readonly PathToExtendedFieldType ImportanceExtendedProperty = new PathToExtendedFieldType
		{
			PropertySetId = Constants.MailboxItemIdPropertyIdSet,
			PropertyName = "Importance",
			PropertyType = MapiPropertyTypeType.String
		};

		// Token: 0x04000030 RID: 48
		private static readonly PathToExtendedFieldType IsReadExtendedProperty = new PathToExtendedFieldType
		{
			PropertySetId = Constants.MailboxItemIdPropertyIdSet,
			PropertyName = "IsRead",
			PropertyType = MapiPropertyTypeType.String
		};

		// Token: 0x04000031 RID: 49
		private static readonly PathToExtendedFieldType InternetMessageIdExtendedProperty = new PathToExtendedFieldType
		{
			PropertySetId = Constants.MailboxItemIdPropertyIdSet,
			PropertyName = "InternetMessageId",
			PropertyType = MapiPropertyTypeType.String
		};

		// Token: 0x04000032 RID: 50
		private static readonly PathToExtendedFieldType ConversationTopicExtendedProperty = new PathToExtendedFieldType
		{
			PropertySetId = Constants.MailboxItemIdPropertyIdSet,
			PropertyName = "ConversationTopic",
			PropertyType = MapiPropertyTypeType.String
		};

		// Token: 0x04000033 RID: 51
		private static readonly PathToExtendedFieldType IsSentItemsExtendedProperty = new PathToExtendedFieldType
		{
			PropertySetId = Constants.MailboxItemIdPropertyIdSet,
			PropertyName = "IsSentItems",
			PropertyType = MapiPropertyTypeType.String
		};

		// Token: 0x04000034 RID: 52
		private static readonly PathToExtendedFieldType BodyTagInfoExtendedProperty = new PathToExtendedFieldType
		{
			PropertySetId = Constants.MailboxItemIdPropertyIdSet,
			PropertyName = "BodyTagInfo",
			PropertyType = MapiPropertyTypeType.String
		};

		// Token: 0x04000035 RID: 53
		private static readonly BasePathToElementType[] AdditionalProperties = new BasePathToElementType[]
		{
			MailboxItemIdList.ItemIdExtendedProperty,
			MailboxItemIdList.ParentFolderIdExtendedProperty,
			MailboxItemIdList.PrimaryItemIdExtendedProperty,
			MailboxItemIdList.ItemSizeExtendedProperty,
			MailboxItemIdList.SubjectExtendedProperty,
			MailboxItemIdList.DocumentIdExtendedProperty,
			MailboxItemIdList.SenderExtendedProperty,
			MailboxItemIdList.SenderSmtpAddressExtendedProperty,
			MailboxItemIdList.ReceivedTimeExtendedProperty,
			MailboxItemIdList.SentTimeExtendedProperty,
			MailboxItemIdList.ImportanceExtendedProperty,
			MailboxItemIdList.IsReadExtendedProperty,
			MailboxItemIdList.InternetMessageIdExtendedProperty,
			MailboxItemIdList.ConversationTopicExtendedProperty,
			MailboxItemIdList.IsSentItemsExtendedProperty,
			MailboxItemIdList.BodyTagInfoExtendedProperty
		};

		// Token: 0x04000036 RID: 54
		private readonly string sourcePrimarySmtpAddress;

		// Token: 0x04000037 RID: 55
		private readonly string targetPrimarySmtpAddress;

		// Token: 0x04000038 RID: 56
		private readonly string workingLocation;

		// Token: 0x04000039 RID: 57
		private readonly object syncObject = new object();

		// Token: 0x0400003A RID: 58
		private BaseFolderType workingFolder;

		// Token: 0x0400003B RID: 59
		private BaseFolderType workingSubFolder;

		// Token: 0x0400003C RID: 60
		private IEwsClient ewsClient;

		// Token: 0x0400003D RID: 61
		private List<ItemId> memoryCache;
	}
}
