using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;
using Microsoft.Exchange.EDiscovery.Export;
using Microsoft.Exchange.EDiscovery.Export.EwsProxy;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000090 RID: 144
	internal sealed class ElcEwsClient : ElcBaseServiceClient<ExchangeServiceBinding, IElcEwsClient>, IElcEwsClient
	{
		// Token: 0x06000575 RID: 1397 RVA: 0x000297F0 File Offset: 0x000279F0
		public ElcEwsClient(IExchangePrincipal exchangePrincipal, Uri serviceEndpoint, IServiceCallingContext<ExchangeServiceBinding> serviceCallingContext, TimeSpan totalRetryTimeWindow, TimeSpan[] retrySchedule) : base(serviceEndpoint, serviceCallingContext, CancellationToken.None)
		{
			this.exchangePrincipal = exchangePrincipal;
			base.TotalRetryTimeWindow = totalRetryTimeWindow;
			this.RetrySchedule = retrySchedule;
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x00029816 File Offset: 0x00027A16
		public override IElcEwsClient FunctionalInterface
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00029938 File Offset: 0x00027B38
		public byte[] GetMrmConfiguration(BaseFolderIdType folderId)
		{
			byte[] userConfiguration = null;
			GetUserConfigurationType getUserConfiguration = new GetUserConfigurationType
			{
				UserConfigurationName = new UserConfigurationNameType
				{
					Name = "MRM",
					Item = folderId
				},
				UserConfigurationProperties = (UserConfigurationPropertyType.Dictionary | UserConfigurationPropertyType.XmlData | UserConfigurationPropertyType.BinaryData)
			};
			this.CallService(() => this.ServiceBinding.GetUserConfiguration(getUserConfiguration), delegate(ResponseMessageType responseMessage, int messageIndex)
			{
				if (responseMessage.ResponseClass == ResponseClassType.Error)
				{
					if (responseMessage.ResponseCode != ResponseCodeType.ErrorItemNotFound)
					{
						throw new RetryException(new ElcEwsException(ElcEwsErrorType.FailedToGetUserConfiguration, responseMessage.ResponseCode.ToString() + " : " + responseMessage.MessageText), false);
					}
					ElcEwsClient.Tracer.TraceDebug<string>(0L, "MRM user configuration was not found in the mailbox of {0}.", this.exchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString());
				}
				else
				{
					GetUserConfigurationResponseMessageType getUserConfigurationResponseMessageType = responseMessage as GetUserConfigurationResponseMessageType;
					if (getUserConfigurationResponseMessageType != null && getUserConfigurationResponseMessageType.UserConfiguration != null && getUserConfigurationResponseMessageType.UserConfiguration.XmlData != null)
					{
						userConfiguration = getUserConfigurationResponseMessageType.UserConfiguration.XmlData;
					}
					else
					{
						ElcEwsClient.Tracer.TraceDebug<string>(0L, "MRM user configuration was not in the GetUserConfiguration response of {0}", this.exchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString());
					}
				}
				return false;
			}, (Exception exception) => ElcEwsClient.WrapElcEwsException(ElcEwsErrorType.FailedToGetUserConfiguration, exception));
			return userConfiguration;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00029A2C File Offset: 0x00027C2C
		public void CreateMrmConfiguration(BaseFolderIdType folderId, byte[] userConfiguration)
		{
			CreateUserConfigurationType createUserConfiguration = new CreateUserConfigurationType
			{
				UserConfiguration = new UserConfigurationType
				{
					UserConfigurationName = new UserConfigurationNameType
					{
						Name = "MRM",
						Item = folderId
					},
					XmlData = userConfiguration
				}
			};
			this.CallService(() => this.ServiceBinding.CreateUserConfiguration(createUserConfiguration), delegate(ResponseMessageType responseMessage, int messageIndex)
			{
				if (responseMessage.ResponseClass == ResponseClassType.Error)
				{
					throw new ElcEwsException(ElcEwsErrorType.FailedToCreateUserConfiguration, responseMessage.ResponseCode.ToString() + " : " + responseMessage.MessageText);
				}
				return false;
			}, (Exception exception) => ElcEwsClient.WrapElcEwsException(ElcEwsErrorType.FailedToCreateUserConfiguration, exception));
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00029B30 File Offset: 0x00027D30
		public void UpdateMrmConfiguration(BaseFolderIdType folderId, byte[] userConfiguration)
		{
			UpdateUserConfigurationType updateUserConfiguration = new UpdateUserConfigurationType
			{
				UserConfiguration = new UserConfigurationType
				{
					UserConfigurationName = new UserConfigurationNameType
					{
						Name = "MRM",
						Item = folderId
					},
					XmlData = userConfiguration
				}
			};
			this.CallService(() => this.ServiceBinding.UpdateUserConfiguration(updateUserConfiguration), delegate(ResponseMessageType responseMessage, int messageIndex)
			{
				if (responseMessage.ResponseClass == ResponseClassType.Error)
				{
					throw new ElcEwsException(ElcEwsErrorType.FailedToUpdateUserConfiguration, responseMessage.ResponseCode.ToString() + " : " + responseMessage.MessageText);
				}
				return false;
			}, (Exception exception) => ElcEwsClient.WrapElcEwsException(ElcEwsErrorType.FailedToUpdateUserConfiguration, exception));
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00029C40 File Offset: 0x00027E40
		public void DeleteMrmConfiguration(BaseFolderIdType folderId)
		{
			DeleteUserConfigurationType deleteUserConfiguration = new DeleteUserConfigurationType
			{
				UserConfigurationName = new UserConfigurationNameType
				{
					Name = "MRM",
					Item = folderId
				}
			};
			this.CallService(() => this.ServiceBinding.DeleteUserConfiguration(deleteUserConfiguration), delegate(ResponseMessageType responseMessage, int messageIndex)
			{
				if (responseMessage.ResponseClass == ResponseClassType.Error && responseMessage.ResponseCode != ResponseCodeType.ErrorItemNotFound)
				{
					throw new ElcEwsException(ElcEwsErrorType.FailedToDeleteUserConfiguration, responseMessage.ResponseCode.ToString() + " : " + responseMessage.MessageText);
				}
				return false;
			}, (Exception exception) => ElcEwsClient.WrapElcEwsException(ElcEwsErrorType.FailedToDeleteUserConfiguration, exception));
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00029D74 File Offset: 0x00027F74
		public BaseFolderType GetFolderById(BaseFolderIdType folderId, BasePathToElementType[] additionalProperties)
		{
			GetFolderType getFolder = new GetFolderType();
			FolderResponseShapeType folderResponseShapeType = new FolderResponseShapeType();
			folderResponseShapeType.BaseShape = DefaultShapeNamesType.IdOnly;
			folderResponseShapeType.AdditionalProperties = additionalProperties;
			getFolder.FolderShape = folderResponseShapeType;
			getFolder.FolderIds = new BaseFolderIdType[]
			{
				folderId
			};
			List<BaseFolderType> folders = new List<BaseFolderType>(1);
			this.CallService(() => this.ServiceBinding.GetFolder(getFolder), delegate(ResponseMessageType responseMessage, int messageIndex)
			{
				if (responseMessage.ResponseClass == ResponseClassType.Error)
				{
					if (responseMessage.ResponseCode != ResponseCodeType.ErrorItemNotFound)
					{
						folders.Clear();
						throw new RetryException(new ElcEwsException(ElcEwsErrorType.FailedToGetFolderById, responseMessage.ResponseCode.ToString() + " : " + responseMessage.MessageText), false);
					}
				}
				else
				{
					FolderInfoResponseMessageType folderInfoResponseMessageType = (FolderInfoResponseMessageType)responseMessage;
					folders.Add(folderInfoResponseMessageType.Folders[0]);
				}
				return true;
			}, (Exception exception) => ElcEwsClient.WrapElcEwsException(ElcEwsErrorType.FailedToGetFolderById, exception));
			if (folders.Count <= 0)
			{
				return null;
			}
			return folders[0];
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00029EF4 File Offset: 0x000280F4
		public IEnumerable<BaseFolderType> GetFolderHierarchy(BaseFolderIdType parentFolderId, bool isDeepTraversal, BasePathToElementType[] additionalProperties)
		{
			List<BaseFolderType> folderList = new List<BaseFolderType>();
			FindFolderType findFolder = new FindFolderType();
			FolderResponseShapeType folderResponseShapeType = new FolderResponseShapeType();
			folderResponseShapeType.BaseShape = DefaultShapeNamesType.IdOnly;
			folderResponseShapeType.AdditionalProperties = additionalProperties;
			findFolder.FolderShape = folderResponseShapeType;
			findFolder.ParentFolderIds = new BaseFolderIdType[]
			{
				parentFolderId
			};
			findFolder.Traversal = (isDeepTraversal ? FolderQueryTraversalType.Deep : FolderQueryTraversalType.Shallow);
			bool morePage = true;
			int offset = 0;
			while (morePage)
			{
				findFolder.Item = new IndexedPageViewType
				{
					BasePoint = IndexBasePointType.Beginning,
					Offset = offset
				};
				this.CallService(() => this.ServiceBinding.FindFolder(findFolder), delegate(ResponseMessageType responseMessage, int messageIndex)
				{
					FindFolderResponseMessageType findFolderResponseMessageType = (FindFolderResponseMessageType)responseMessage;
					if (responseMessage.ResponseClass == ResponseClassType.Error)
					{
						throw new RetryException(new ElcEwsException(ElcEwsErrorType.FailedToGetFolderHierarchy, responseMessage.ResponseCode.ToString() + " : " + responseMessage.MessageText), false);
					}
					folderList.AddRange(findFolderResponseMessageType.RootFolder.Folders);
					if (findFolderResponseMessageType.RootFolder.IncludesLastItemInRange || !findFolderResponseMessageType.RootFolder.IncludesLastItemInRangeSpecified)
					{
						morePage = false;
					}
					else
					{
						offset = findFolderResponseMessageType.RootFolder.IndexedPagingOffset;
					}
					return false;
				}, (Exception exception) => ElcEwsClient.WrapElcEwsException(ElcEwsErrorType.FailedToGetFolderHierarchy, exception));
			}
			return folderList;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0002A0C0 File Offset: 0x000282C0
		public BaseFolderType CreateFolder(BaseFolderIdType parentFolderId, BaseFolderType newFolder)
		{
			CreateFolderType createFolder = new CreateFolderType();
			createFolder.Folders = new BaseFolderType[]
			{
				newFolder
			};
			createFolder.ParentFolderId = new TargetFolderIdType
			{
				Item = parentFolderId
			};
			List<BaseFolderType> newFolders = new List<BaseFolderType>(1);
			this.CallService(() => this.ServiceBinding.CreateFolder(createFolder), delegate(ResponseMessageType responseMessage, int messageIndex)
			{
				if (responseMessage.ResponseClass != ResponseClassType.Error)
				{
					FolderInfoResponseMessageType folderInfoResponseMessageType = (FolderInfoResponseMessageType)responseMessage;
					newFolders.AddRange(folderInfoResponseMessageType.Folders);
					return true;
				}
				if (responseMessage.ResponseCode == ResponseCodeType.ErrorFolderExists)
				{
					throw new ElcEwsException(ElcEwsErrorType.FailedToCreateExistingFolder, responseMessage.ResponseCode.ToString() + " : " + responseMessage.MessageText);
				}
				throw new ElcEwsException(ElcEwsErrorType.FailedToCreateFolder, responseMessage.ResponseCode.ToString() + " : " + responseMessage.MessageText);
			}, (Exception exception) => ElcEwsClient.WrapElcEwsException(ElcEwsErrorType.FailedToCreateFolder, exception));
			if (newFolders.Count <= 0)
			{
				return null;
			}
			return newFolders[0];
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0002A21C File Offset: 0x0002841C
		public BaseFolderType GetFolderByName(BaseFolderIdType parentFolderId, string folderDisplayName, BasePathToElementType[] additionalProperties)
		{
			FindFolderType findFolder = new FindFolderType();
			FolderResponseShapeType folderResponseShapeType = new FolderResponseShapeType();
			folderResponseShapeType.BaseShape = DefaultShapeNamesType.IdOnly;
			folderResponseShapeType.AdditionalProperties = additionalProperties;
			findFolder.FolderShape = folderResponseShapeType;
			findFolder.Restriction = new RestrictionType
			{
				Item = new IsEqualToType
				{
					Item = new PathToUnindexedFieldType
					{
						FieldURI = UnindexedFieldURIType.folderDisplayName
					},
					FieldURIOrConstant = new FieldURIOrConstantType
					{
						Item = new ConstantValueType
						{
							Value = folderDisplayName
						}
					}
				}
			};
			findFolder.ParentFolderIds = new BaseFolderIdType[]
			{
				parentFolderId
			};
			List<BaseFolderType> folders = new List<BaseFolderType>(1);
			this.CallService(() => this.ServiceBinding.FindFolder(findFolder), delegate(ResponseMessageType responseMessage, int messageIndex)
			{
				if (responseMessage.ResponseClass == ResponseClassType.Error)
				{
					if (responseMessage.ResponseCode != ResponseCodeType.ErrorItemNotFound)
					{
						folders.Clear();
						throw new RetryException(new ElcEwsException(ElcEwsErrorType.FailedToGetFolderByName, responseMessage.ResponseCode.ToString() + " : " + responseMessage.MessageText), false);
					}
				}
				else
				{
					FindFolderResponseMessageType findFolderResponseMessageType = (FindFolderResponseMessageType)responseMessage;
					folders.AddRange(findFolderResponseMessageType.RootFolder.Folders);
				}
				return true;
			}, (Exception exception) => ElcEwsClient.WrapElcEwsException(ElcEwsErrorType.FailedToGetFolderByName, exception));
			if (folders.Count <= 0)
			{
				return null;
			}
			return folders[0];
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0002A3C8 File Offset: 0x000285C8
		public BaseFolderType UpdateFolder(FolderChangeType folderChange)
		{
			UpdateFolderType updateFolder = new UpdateFolderType();
			updateFolder.FolderChanges = new FolderChangeType[]
			{
				folderChange
			};
			List<BaseFolderType> updatedFolders = new List<BaseFolderType>(1);
			this.CallService(() => this.ServiceBinding.UpdateFolder(updateFolder), delegate(ResponseMessageType responseMessage, int messageIndex)
			{
				if (responseMessage.ResponseClass == ResponseClassType.Error)
				{
					throw new ElcEwsException(ElcEwsErrorType.FailedToUpdateFolder, responseMessage.ResponseCode.ToString() + " : " + responseMessage.MessageText);
				}
				FolderInfoResponseMessageType folderInfoResponseMessageType = (FolderInfoResponseMessageType)responseMessage;
				updatedFolders.AddRange(folderInfoResponseMessageType.Folders);
				return true;
			}, (Exception exception) => ElcEwsClient.WrapElcEwsException(ElcEwsErrorType.FailedToUpdateFolder, exception));
			if (updatedFolders.Count <= 0)
			{
				return null;
			}
			return updatedFolders[0];
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0002A6E0 File Offset: 0x000288E0
		public List<ElcEwsItem> ExportItems(IList<ElcEwsItem> items)
		{
			ExportItemsType exportItemsType = new ExportItemsType();
			exportItemsType.ItemIds = (from elcEwsItem in items
			select new ItemIdType
			{
				Id = elcEwsItem.Id
			}).ToArray<ItemIdType>();
			List<ElcEwsItem> exportedItems = new List<ElcEwsItem>(exportItemsType.ItemIds.Length);
			int startIndex = 0;
			this.CallService(() => this.ServiceBinding.ExportItems(exportItemsType), delegate(ResponseMessageType responseMessage, int messageIndex)
			{
				ExportItemsResponseMessageType exportItemsResponseMessageType = (ExportItemsResponseMessageType)responseMessage;
				ElcEwsItem elcEwsItem = items[startIndex + messageIndex];
				if (exportItemsResponseMessageType.ResponseClass == ResponseClassType.Error)
				{
					ElcEwsClient.Tracer.TraceError<ResponseCodeType>(0L, "ElcEwsClient.ExportItems: ExportItems with error response message. ResponseCode : {0}", exportItemsResponseMessageType.ResponseCode);
					if (exportItemsResponseMessageType.ResponseCode == ResponseCodeType.ErrorItemNotFound)
					{
						exportedItems.Add(new ElcEwsItem
						{
							Id = elcEwsItem.Id,
							Data = null,
							Error = new ElcEwsException(ElcEwsErrorType.NotFound),
							StorageItemData = elcEwsItem.StorageItemData
						});
					}
					else
					{
						exportedItems.Add(new ElcEwsItem
						{
							Id = elcEwsItem.Id,
							Data = null,
							Error = new ElcEwsException(ElcEwsErrorType.FailedToExportItem, exportItemsResponseMessageType.ResponseCode.ToString()),
							StorageItemData = elcEwsItem.StorageItemData
						});
					}
				}
				else if (exportItemsResponseMessageType.ResponseClass == ResponseClassType.Warning)
				{
					ElcEwsClient.Tracer.TraceWarning<ResponseCodeType, string>(0L, "ElcEwsClient.ExportItems: Message response warning. ResponseCode:{0}; MessageText:'{1}'", exportItemsResponseMessageType.ResponseCode, exportItemsResponseMessageType.MessageText);
					if (exportItemsResponseMessageType.ResponseCode == ResponseCodeType.ErrorBatchProcessingStopped)
					{
						ElcEwsClient.Tracer.TraceWarning<int, int>(0L, "ElcEwsClient.ExportItems: Hitting ErrorBatchProcessingStopped, startIndex:{0}; messageIndex={1}", startIndex, messageIndex);
						if (messageIndex == 0)
						{
							throw new RetryException(new ElcEwsException(ElcEwsErrorType.FailedToExportItem, "Internal Error: Hitting ErrorBatchProcessingStopped as the first response."), false);
						}
						startIndex += messageIndex;
						exportItemsType.ItemIds = exportItemsType.ItemIds.Skip(messageIndex).ToArray<ItemIdType>();
						throw new RetryException(new ElcEwsException(ElcEwsErrorType.FailedToExportItem, "Hitting ErrorBatchProcessingStopped"), true);
					}
					else
					{
						exportedItems.Add(new ElcEwsItem
						{
							Id = elcEwsItem.Id,
							Data = null,
							Error = new ElcEwsException(ElcEwsErrorType.FailedToExportItem, exportItemsResponseMessageType.ResponseCode.ToString()),
							StorageItemData = elcEwsItem.StorageItemData
						});
					}
				}
				else
				{
					exportedItems.Add(new ElcEwsItem
					{
						Id = elcEwsItem.Id,
						Data = exportItemsResponseMessageType.Data,
						Error = null,
						StorageItemData = elcEwsItem.StorageItemData
					});
				}
				return true;
			}, (Exception exception) => ElcEwsClient.WrapElcEwsException(ElcEwsErrorType.FailedToExportItem, exception));
			return exportedItems;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0002AA2C File Offset: 0x00028C2C
		public List<ElcEwsItem> UploadItems(FolderIdType parentFolderId, IList<ElcEwsItem> items, bool alwaysCreateNew)
		{
			UploadItemsType uploadItems = new UploadItemsType();
			uploadItems.Items = (from item in items
			select new UploadItemType
			{
				CreateAction = (alwaysCreateNew ? CreateActionType.CreateNew : CreateActionType.UpdateOrCreate),
				ItemId = new ItemIdType
				{
					Id = item.Id
				},
				Data = item.Data,
				ParentFolderId = parentFolderId
			}).ToArray<UploadItemType>();
			List<ElcEwsItem> returnedItems = new List<ElcEwsItem>(uploadItems.Items.Length);
			int startIndex = 0;
			this.CallService(() => this.ServiceBinding.UploadItems(uploadItems), delegate(ResponseMessageType responseMessage, int messageIndex)
			{
				UploadItemsResponseMessageType uploadItemsResponseMessageType = (UploadItemsResponseMessageType)responseMessage;
				ElcEwsItem elcEwsItem = items[startIndex + messageIndex];
				if (uploadItemsResponseMessageType.ResponseClass == ResponseClassType.Error)
				{
					ElcEwsClient.Tracer.TraceError<ResponseCodeType>(0L, "ElcEwsClient.UploadItems: UploadItems with error response message. ResponseCode : {0}", uploadItemsResponseMessageType.ResponseCode);
					if (uploadItemsResponseMessageType.ResponseCode == ResponseCodeType.ErrorQuotaExceeded)
					{
						throw new ElcEwsException(ElcEwsErrorType.TargetOutOfSpace, uploadItemsResponseMessageType.ResponseCode.ToString());
					}
					returnedItems.Add(new ElcEwsItem
					{
						Id = elcEwsItem.Id,
						Data = elcEwsItem.Data,
						Error = new ElcEwsException(ElcEwsErrorType.FailedToUploadItem, uploadItemsResponseMessageType.ResponseCode.ToString()),
						StorageItemData = elcEwsItem.StorageItemData
					});
				}
				else if (uploadItemsResponseMessageType.ResponseClass == ResponseClassType.Warning)
				{
					ElcEwsClient.Tracer.TraceWarning<ResponseCodeType, string>(0L, "ElcEwsClient.UploadItems: Message response warning. ResponseCode:{0}; MessageText:'{1}'", uploadItemsResponseMessageType.ResponseCode, uploadItemsResponseMessageType.MessageText);
					if (uploadItemsResponseMessageType.ResponseCode == ResponseCodeType.ErrorBatchProcessingStopped)
					{
						ElcEwsClient.Tracer.TraceWarning<int, int>(0L, "ElcEwsClient.UploadItems: Hitting ErrorBatchProcessingStopped, startIndex:{0}; messageIndex={1}", startIndex, messageIndex);
						if (messageIndex == 0)
						{
							throw new RetryException(new ElcEwsException(ElcEwsErrorType.FailedToUploadItem, "Internal Error: Hitting ErrorBatchProcessingStopped as the first response."), false);
						}
						startIndex += messageIndex;
						uploadItems.Items = uploadItems.Items.Skip(messageIndex).ToArray<UploadItemType>();
						throw new RetryException(new ElcEwsException(ElcEwsErrorType.FailedToUploadItem, "Hitting ErrorBatchProcessingStopped"), true);
					}
					else
					{
						returnedItems.Add(new ElcEwsItem
						{
							Id = elcEwsItem.Id,
							Data = null,
							Error = new ElcEwsException(ElcEwsErrorType.FailedToUploadItem, uploadItemsResponseMessageType.ResponseCode.ToString()),
							StorageItemData = elcEwsItem.StorageItemData
						});
					}
				}
				else
				{
					returnedItems.Add(new ElcEwsItem
					{
						Id = uploadItemsResponseMessageType.ItemId.Id,
						Data = elcEwsItem.Data,
						Error = null,
						StorageItemData = elcEwsItem.StorageItemData
					});
				}
				return true;
			}, (Exception exception) => ElcEwsClient.WrapElcEwsException(ElcEwsErrorType.FailedToUploadItem, exception));
			return returnedItems;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0002ACDC File Offset: 0x00028EDC
		public List<ElcEwsItem> GetItems(IList<ElcEwsItem> items)
		{
			GetItemType getItem = new GetItemType();
			ItemResponseShapeType itemResponseShapeType = new ItemResponseShapeType();
			itemResponseShapeType.BaseShape = DefaultShapeNamesType.Default;
			getItem.ItemShape = itemResponseShapeType;
			getItem.ItemIds = (from elcEwsItem in items
			select new ItemIdType
			{
				Id = elcEwsItem.Id
			}).ToArray<ItemIdType>();
			List<ElcEwsItem> getItems = new List<ElcEwsItem>(getItem.ItemIds.Length);
			this.CallService(() => this.ServiceBinding.GetItem(getItem), delegate(ResponseMessageType responseMessage, int messageIndex)
			{
				ItemInfoResponseMessageType itemInfoResponseMessageType = (ItemInfoResponseMessageType)responseMessage;
				ElcEwsItem elcEwsItem = items[messageIndex];
				if (itemInfoResponseMessageType.ResponseClass != ResponseClassType.Success)
				{
					ElcEwsClient.Tracer.TraceError<ResponseCodeType>(0L, "ElcEwsClient.GetItems: GetItem with error response message. ResponseCode : {0}", itemInfoResponseMessageType.ResponseCode);
					if (itemInfoResponseMessageType.ResponseCode == ResponseCodeType.ErrorItemNotFound)
					{
						getItems.Add(new ElcEwsItem
						{
							Id = elcEwsItem.Id,
							Data = null,
							Error = new ElcEwsException(ElcEwsErrorType.NotFound),
							StorageItemData = elcEwsItem.StorageItemData
						});
					}
					else
					{
						getItems.Add(new ElcEwsItem
						{
							Id = elcEwsItem.Id,
							Data = null,
							Error = new ElcEwsException(ElcEwsErrorType.FailedToGetItem, itemInfoResponseMessageType.ResponseCode.ToString()),
							StorageItemData = elcEwsItem.StorageItemData
						});
					}
				}
				else if (itemInfoResponseMessageType.Items != null && itemInfoResponseMessageType.Items.Items != null && itemInfoResponseMessageType.Items.Items.Length > 0)
				{
					getItems.Add(new ElcEwsItem
					{
						Id = itemInfoResponseMessageType.Items.Items[0].ItemId.Id,
						Data = null,
						Error = null,
						StorageItemData = elcEwsItem.StorageItemData
					});
				}
				else
				{
					getItems.Add(new ElcEwsItem
					{
						Id = elcEwsItem.Id,
						Data = null,
						Error = new ElcEwsException(ElcEwsErrorType.NoItemReturned),
						StorageItemData = elcEwsItem.StorageItemData
					});
				}
				return true;
			}, (Exception exception) => ElcEwsClient.WrapElcEwsException(ElcEwsErrorType.FailedToGetItem, exception));
			return getItems;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0002ADB4 File Offset: 0x00028FB4
		private void CallService(Func<BaseResponseMessageType> delegateEwsCall, Func<ResponseMessageType, int, bool> responseMessageProcessor, Func<Exception, Exception> exceptionHandler)
		{
			try
			{
				this.CallEws(delegateEwsCall, responseMessageProcessor, exceptionHandler, ExchangeVersionType.Exchange2013_SP1, this.exchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString());
			}
			catch (ExportException ex)
			{
				throw new ElcEwsException(ElcEwsErrorType.ExchangeWebServiceCallFailed, ex.InnerException ?? ex);
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0002AFD8 File Offset: 0x000291D8
		private void CallEws(Func<BaseResponseMessageType> delegateEwsCall, Func<ResponseMessageType, int, bool> responseMessageProcessor, Func<Exception, Exception> exceptionHandler, ExchangeVersionType requestServerVersion, string mailboxEmailAddress)
		{
			if (this.Connect())
			{
				base.ServiceBinding.RequestServerVersionValue = new RequestServerVersion
				{
					Version = requestServerVersion
				};
				base.ServiceCallingContext.SetServiceApiContext(base.ServiceBinding, mailboxEmailAddress);
				try
				{
					base.InternalCallService<BaseResponseMessageType>(delegateEwsCall, delegate(BaseResponseMessageType response)
					{
						List<ResponseMessageType> list = new List<ResponseMessageType>(response.ResponseMessages.Items);
						foreach (ResponseMessageType responseMessageType in from t in response.ResponseMessages.Items
						orderby (int)t.ResponseClass
						select t)
						{
							int arg = list.IndexOf(responseMessageType);
							if (responseMessageType.ResponseClass == ResponseClassType.Error)
							{
								ElcEwsClient.Tracer.TraceError<ResponseCodeType, string>(0L, "ElcEwsClient.CallEws: Message response error. ResponseCode:{0}; MessageText:'{1}'", responseMessageType.ResponseCode, responseMessageType.MessageText);
								if (responseMessageType.ResponseCode == ResponseCodeType.ErrorInternalServerTransientError || responseMessageType.ResponseCode == ResponseCodeType.ErrorMailboxStoreUnavailable || responseMessageType.ResponseCode == ResponseCodeType.ErrorServerBusy || responseMessageType.ResponseCode == ResponseCodeType.ErrorTimeoutExpired)
								{
									ElcEwsClient.Tracer.TraceError<ResponseCodeType>(0L, "ElcEwsClient.CallEws: Transient exception causing retry. ResponseCode: {0}.", responseMessageType.ResponseCode);
									throw new RetryException(new ExportException(ExportErrorType.ExchangeWebServiceCallFailed, responseMessageType.MessageText), false);
								}
								if (responseMessageProcessor == null)
								{
									ElcEwsClient.Tracer.TraceError(0L, "ElcEwsClient.CallEws: Error response message received and responseMessageProcessor is null.");
									throw new ExportException(ExportErrorType.ExchangeWebServiceCallFailed, responseMessageType.MessageText);
								}
							}
							try
							{
								if (responseMessageProcessor != null && !responseMessageProcessor(responseMessageType, arg))
								{
									break;
								}
							}
							catch (RetryException ex)
							{
								if (ElcBaseServiceClient<ExchangeServiceBinding, IElcEwsClient>.IsTransientError(responseMessageType.ResponseCode))
								{
									throw;
								}
								throw ex.InnerException;
							}
						}
					}, exceptionHandler, () => base.ServiceCallingContext.AuthorizeServiceBinding(base.ServiceBinding), delegate(Uri redirectedUrl)
					{
						base.ServiceCallingContext.SetServiceUrlAffinity(base.ServiceBinding, redirectedUrl);
						base.ServiceCallingContext.SetServiceUrl(base.ServiceBinding, redirectedUrl);
					});
					return;
				}
				catch (ExportException)
				{
					ElcEwsClient.Tracer.TraceError<ServiceHttpContext>(0L, "ElcEwsClient.CallEws: {0}", base.ServiceBinding.HttpContext);
					throw;
				}
			}
			throw new ExportException(ExportErrorType.ExchangeWebServiceCallFailed, "Unable to connect to Exchange web service at: " + base.ServiceEndpoint.ToString());
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0002B0C4 File Offset: 0x000292C4
		private static ElcEwsException WrapElcEwsException(ElcEwsErrorType errorType, Exception exception)
		{
			ElcEwsException ex = exception as ElcEwsException;
			if (ex == null || ex.ErrorType != errorType)
			{
				return new ElcEwsException(errorType, exception);
			}
			return ex;
		}

		// Token: 0x0400040B RID: 1035
		private const UserConfigurationPropertyType ElcConfigurationTypes = UserConfigurationPropertyType.Dictionary | UserConfigurationPropertyType.XmlData | UserConfigurationPropertyType.BinaryData;

		// Token: 0x0400040C RID: 1036
		private static readonly Trace Tracer = ExTraceGlobals.ELCTracer;

		// Token: 0x0400040D RID: 1037
		private IExchangePrincipal exchangePrincipal;
	}
}
