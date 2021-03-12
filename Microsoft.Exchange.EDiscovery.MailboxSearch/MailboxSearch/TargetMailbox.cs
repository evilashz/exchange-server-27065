using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.EDiscovery.Export;
using Microsoft.Exchange.EDiscovery.Export.EwsProxy;
using Microsoft.Exchange.InfoWorker.Common;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch
{
	// Token: 0x02000016 RID: 22
	internal class TargetMailbox : ITargetMailbox, ITarget, IDisposable
	{
		// Token: 0x0600012C RID: 300 RVA: 0x00008BBE File Offset: 0x00006DBE
		public TargetMailbox(OrganizationId orgId, string primarySmtpAddress, string legacyDN, Uri serviceEndpoint, IExportContext exportContext) : this(orgId, primarySmtpAddress, legacyDN, new EwsClient(serviceEndpoint, new ServerToServerEwsCallingContext(null)), exportContext)
		{
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00008BD8 File Offset: 0x00006DD8
		public TargetMailbox(OrganizationId orgId, string primarySmtpAddress, string legacyDN, IEwsClient ewsClient, IExportContext exportContext)
		{
			Util.ThrowIfNullOrEmpty(primarySmtpAddress, "primarySmtpAddress");
			Util.ThrowIfNullOrEmpty(legacyDN, "legacyDN");
			Util.ThrowIfNull(ewsClient, "ewsClient");
			Util.ThrowIfNull(exportContext, "exportContext");
			this.organizationId = orgId;
			this.ewsClient = ewsClient;
			this.targetLocation = exportContext.TargetLocation;
			this.Initialize(primarySmtpAddress, legacyDN, exportContext);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00008C3F File Offset: 0x00006E3F
		public TargetMailbox(OrganizationId orgId, string primarySmtpAddress, string legacyDN, Uri serviceEndpoint, ITargetLocation targetLocation) : this(orgId, primarySmtpAddress, legacyDN, new EwsClient(serviceEndpoint, new ServerToServerEwsCallingContext(null)), targetLocation)
		{
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00008C5C File Offset: 0x00006E5C
		public TargetMailbox(OrganizationId orgId, string primarySmtpAddress, string legacyDN, IEwsClient ewsClient, ITargetLocation targetLocation)
		{
			Util.ThrowIfNullOrEmpty(primarySmtpAddress, "primarySmtpAddress");
			Util.ThrowIfNullOrEmpty(legacyDN, "legacyDN");
			Util.ThrowIfNull(ewsClient, "ewsClient");
			Util.ThrowIfNull(targetLocation, "targetLocation");
			this.organizationId = orgId;
			this.ewsClient = ewsClient;
			this.targetLocation = targetLocation;
			this.Initialize(primarySmtpAddress, legacyDN, null);
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00008CC0 File Offset: 0x00006EC0
		public static Regex InvalidFileCharExpression
		{
			get
			{
				if (TargetMailbox.invalidFileCharExpression == null)
				{
					string str = new string(Path.GetInvalidFileNameChars());
					TargetMailbox.invalidFileCharExpression = new Regex("[" + Regex.Escape(str) + "#]", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
				}
				return TargetMailbox.invalidFileCharExpression;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00008D08 File Offset: 0x00006F08
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00008D10 File Offset: 0x00006F10
		public string PrimarySmtpAddress { get; private set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00008D19 File Offset: 0x00006F19
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00008D21 File Offset: 0x00006F21
		public string LegacyDistinguishedName { get; private set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00008D2A File Offset: 0x00006F2A
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00008D32 File Offset: 0x00006F32
		public IExportContext ExportContext { get; private set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00008D3B File Offset: 0x00006F3B
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00008D43 File Offset: 0x00006F43
		public ExportSettings ExportSettings { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00008D4C File Offset: 0x00006F4C
		public IEwsClient EwsClientInstance
		{
			get
			{
				return this.ewsClient;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00008D54 File Offset: 0x00006F54
		public bool ExportLocationExist
		{
			get
			{
				return this.GetWorkingOrResultFolder(this.targetLocation.ExportLocation) != null;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00008D6D File Offset: 0x00006F6D
		public bool WorkingLocationExist
		{
			get
			{
				return this.GetWorkingOrResultFolder(this.targetLocation.WorkingLocation) != null;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00008D86 File Offset: 0x00006F86
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00008D8E File Offset: 0x00006F8E
		public string[] StatusMailRecipients { get; set; }

		// Token: 0x0600013E RID: 318 RVA: 0x00008D98 File Offset: 0x00006F98
		public void Dispose()
		{
			if (!this.disposed)
			{
				if (this.exportRecordAttachmentLog != null)
				{
					this.exportRecordAttachmentLog.Dispose();
					this.exportRecordAttachmentLog = null;
				}
				if (this.statusLog != null)
				{
					this.statusLog.Dispose();
					this.statusLog = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00008DE8 File Offset: 0x00006FE8
		public void CheckInitialStatus(SourceInformationCollection allSourceInformation, OperationStatus status)
		{
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00008DEA File Offset: 0x00006FEA
		public IItemIdList CreateItemIdList(string mailboxId, bool isUnsearchable)
		{
			return new MailboxItemIdList(this.PrimarySmtpAddress, mailboxId, this.targetLocation.WorkingLocation, this.ewsClient, isUnsearchable);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00008E0A File Offset: 0x0000700A
		public void RemoveItemIdList(string mailboxId, bool isUnsearchable)
		{
			new MailboxItemIdList(this.PrimarySmtpAddress, mailboxId, this.targetLocation.WorkingLocation, this.ewsClient, isUnsearchable).Delete();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00008E2F File Offset: 0x0000702F
		public IContextualBatchDataWriter<List<ItemInformation>> CreateDataWriter(IProgressController progressController)
		{
			return new MailboxWriter(this.ExportContext, this, progressController);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00008E3E File Offset: 0x0000703E
		public void Rollback(SourceInformationCollection allSourceInformation)
		{
			this.PreRemoveSearchResults(false);
			this.RemoveSearchResults();
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00008E4D File Offset: 0x0000704D
		public IStatusLog GetStatusLog()
		{
			if (this.statusLog == null)
			{
				this.statusLog = new MailboxStatusLog(this.organizationId, this.ExportContext.ExportMetadata.ExportName);
			}
			return this.statusLog;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00008E80 File Offset: 0x00007080
		public string CreateResultFolder(string resultFolderName)
		{
			BaseFolderType baseFolderType = this.CreateRootResultFolderIfNotExist();
			BaseFolderType baseFolderType2 = this.ewsClient.GetFolderByName(this.PrimarySmtpAddress, baseFolderType.FolderId, resultFolderName);
			if (baseFolderType2 == null)
			{
				baseFolderType2 = this.CreateFolder(baseFolderType.FolderId, resultFolderName, false);
			}
			return baseFolderType2.FolderId.Id;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00008ECC File Offset: 0x000070CC
		public void PreRemoveSearchResults(bool removeLogs)
		{
			BaseFolderType orCreateRecycleFolder = this.GetOrCreateRecycleFolder(true);
			BaseFolderType workingOrResultFolder = this.GetWorkingOrResultFolder(this.targetLocation.ExportLocation);
			if (workingOrResultFolder != null)
			{
				if (removeLogs)
				{
					this.ewsClient.MoveFolder(this.PrimarySmtpAddress, orCreateRecycleFolder.FolderId, new BaseFolderIdType[]
					{
						workingOrResultFolder.FolderId
					});
					return;
				}
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				this.ewsClient.GetAllFolders(this.PrimarySmtpAddress, workingOrResultFolder.FolderId.Id, false, false, dictionary);
				List<BaseFolderIdType> list = new List<BaseFolderIdType>(dictionary.Count);
				foreach (KeyValuePair<string, string> keyValuePair in dictionary)
				{
					list.Add(new FolderIdType
					{
						Id = keyValuePair.Key
					});
				}
				if (list.Count > 0)
				{
					this.ewsClient.MoveFolder(this.PrimarySmtpAddress, orCreateRecycleFolder.FolderId, list.ToArray());
				}
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00008FD8 File Offset: 0x000071D8
		public void RemoveSearchResults()
		{
			this.DeleteWorkingFolders();
			BaseFolderType orCreateRecycleFolder = this.GetOrCreateRecycleFolder(false);
			if (orCreateRecycleFolder == null)
			{
				return;
			}
			this.ewsClient.DeleteFolder(this.PrimarySmtpAddress, new BaseFolderIdType[]
			{
				orCreateRecycleFolder.FolderId
			});
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000901C File Offset: 0x0000721C
		public BaseFolderType GetFolder(string folderId)
		{
			FolderIdType folderId2 = new FolderIdType
			{
				Id = folderId
			};
			return this.ewsClient.GetFolderById(this.PrimarySmtpAddress, folderId2);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000904A File Offset: 0x0000724A
		public BaseFolderType GetFolderByName(BaseFolderIdType parentFolderId, string folderName)
		{
			return this.ewsClient.GetFolderByName(this.PrimarySmtpAddress, parentFolderId, folderName);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00009060 File Offset: 0x00007260
		public BaseFolderType CreateFolder(BaseFolderIdType parentFolderId, string newFolderName, bool isHidden)
		{
			FolderType folderType = new FolderType
			{
				DisplayName = newFolderName
			};
			if (isHidden)
			{
				folderType.ExtendedProperty = new ExtendedPropertyType[]
				{
					MailboxItemIdList.IsHiddenExtendedProperty
				};
			}
			List<BaseFolderType> list = this.ewsClient.CreateFolder(this.PrimarySmtpAddress, parentFolderId, new BaseFolderType[]
			{
				folderType
			});
			if (list == null || list.Count <= 0)
			{
				return null;
			}
			return list[0];
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000090CC File Offset: 0x000072CC
		public List<ItemInformation> CopyItems(string parentFolderId, IList<ItemInformation> items)
		{
			FolderIdType parentFolderId2 = new FolderIdType
			{
				Id = parentFolderId
			};
			return this.ewsClient.UploadItems(this.PrimarySmtpAddress, parentFolderId2, items, true);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000090FC File Offset: 0x000072FC
		public void CreateOrUpdateSearchLogEmail(MailboxDiscoverySearch searchObject, List<string> successfulMailboxes, List<string> unsuccessfulMailboxes)
		{
			Util.ThrowIfNull(searchObject, "searchObject");
			this.exportRecordLogFileName = Path.ChangeExtension(this.ToSafeFileNameString(searchObject.Name), ".csv");
			this.InternalCreateOrUpdateSearchLogEmail(searchObject, successfulMailboxes, unsuccessfulMailboxes, true);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000912F File Offset: 0x0000732F
		public void WriteExportRecordLog(MailboxDiscoverySearch searchObject, IEnumerable<ExportRecord> records)
		{
			Util.ThrowIfNull(searchObject, "searchObject");
			this.InternalCreateOrUpdateSearchLogEmail(searchObject, null, null, false);
			this.InternalWriteExportRecordLog(records);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000914D File Offset: 0x0000734D
		public void AttachDiscoveryLogFiles()
		{
			this.InternalAttachDiscoveryLogFiles();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00009158 File Offset: 0x00007358
		private void InternalCreateOrUpdateSearchLogEmail(MailboxDiscoverySearch searchObject, List<string> successfulMailboxes, List<string> unsuccessfulMailboxes, bool updateIfExists)
		{
			BaseFolderType baseFolderType = this.CreateRootResultFolderIfNotExist();
			if (string.IsNullOrEmpty(this.logItemId))
			{
				this.logItemId = this.CreateSearchLogEmail(baseFolderType.FolderId, searchObject, successfulMailboxes, unsuccessfulMailboxes);
				return;
			}
			ItemType item = this.ewsClient.GetItem(this.PrimarySmtpAddress, this.logItemId);
			if (item == null)
			{
				this.logItemId = this.CreateSearchLogEmail(baseFolderType.FolderId, searchObject, successfulMailboxes, unsuccessfulMailboxes);
				return;
			}
			this.logItemId = item.ItemId.Id;
			if (updateIfExists)
			{
				ItemChangeType itemChangeType = new ItemChangeType
				{
					Item = item.ItemId,
					Updates = new ItemChangeDescriptionType[]
					{
						new SetItemFieldType
						{
							Item = new PathToUnindexedFieldType
							{
								FieldURI = UnindexedFieldURIType.itemBody
							},
							Item1 = new MessageType
							{
								Body = new BodyType
								{
									BodyType1 = BodyTypeType.HTML,
									Value = Util.CreateLogMailBody(searchObject, this.StatusMailRecipients, successfulMailboxes, unsuccessfulMailboxes, this.ExportContext.Sources)
								}
							}
						}
					}
				};
				this.ewsClient.UpdateItems(this.PrimarySmtpAddress, baseFolderType.FolderId, new ItemChangeType[]
				{
					itemChangeType
				});
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00009294 File Offset: 0x00007494
		private string CreateSearchLogEmail(FolderIdType folderId, MailboxDiscoverySearch searchObject, List<string> successfulMailboxes, List<string> unsuccessfulMailboxes)
		{
			ItemType itemType = new ItemType
			{
				Subject = string.Format("{0}-{1}", searchObject.Name, searchObject.LastStartTime.ToString()),
				Body = new BodyType
				{
					BodyType1 = BodyTypeType.HTML,
					Value = Util.CreateLogMailBody(searchObject, this.StatusMailRecipients, successfulMailboxes, unsuccessfulMailboxes, this.ExportContext.Sources)
				}
			};
			List<ItemType> list = this.ewsClient.CreateItems(this.PrimarySmtpAddress, folderId, new ItemType[]
			{
				itemType
			});
			return list[0].ItemId.Id;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000933B File Offset: 0x0000753B
		private void InternalWriteExportRecordLog(IEnumerable<ExportRecord> records)
		{
			if (records == null)
			{
				return;
			}
			if (this.exportRecordAttachmentLog == null)
			{
				this.exportRecordAttachmentLog = new AttachmentLog(this.exportRecordLogFileName, Strings.SearchLogHeader);
			}
			this.exportRecordAttachmentLog.WriteLogs(this.CreateLogEntriesFromExportRecords(records));
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00009378 File Offset: 0x00007578
		private void InternalAttachDiscoveryLogFiles()
		{
			if (!string.IsNullOrEmpty(this.exportRecordAttachmentId))
			{
				this.ewsClient.DeleteAttachments(this.PrimarySmtpAddress, new string[]
				{
					this.exportRecordAttachmentId
				});
			}
			if (this.exportRecordAttachmentLog == null)
			{
				this.exportRecordAttachmentLog = new AttachmentLog(this.exportRecordLogFileName, Strings.SearchLogHeader);
			}
			FileAttachmentType fileAttachmentType = new FileAttachmentType
			{
				Name = Path.ChangeExtension(this.exportRecordLogFileName, ".zip"),
				Content = this.exportRecordAttachmentLog.GetCompressedLogData()
			};
			List<AttachmentType> list = this.ewsClient.CreateAttachments(this.PrimarySmtpAddress, this.logItemId, new AttachmentType[]
			{
				fileAttachmentType
			});
			if (list != null && list.Count > 0)
			{
				this.exportRecordAttachmentId = list[0].AttachmentId.Id;
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00009750 File Offset: 0x00007950
		private IEnumerable<string> CreateLogEntriesFromExportRecords(IEnumerable<ExportRecord> records)
		{
			foreach (ExportRecord er in records)
			{
				yield return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24}", new object[]
				{
					er.SourceId,
					Util.QuoteValueIfRequired(er.OriginalPath.Replace(er.SourceId, string.Empty)),
					Util.QuoteValueIfRequired(er.Title),
					er.IsRead,
					er.SentTime,
					er.ReceivedTime,
					Util.QuoteValueIfRequired(er.Sender),
					Util.QuoteValueIfRequired(er.SenderSmtpAddress),
					er.Importance,
					string.Empty,
					er.Id,
					string.Empty,
					string.Empty,
					string.Empty,
					string.Empty,
					string.Empty,
					string.Empty,
					string.Empty,
					string.Empty,
					string.Empty,
					string.Empty,
					string.Empty,
					string.Empty,
					string.Empty,
					string.Empty
				});
			}
			yield break;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00009774 File Offset: 0x00007974
		private void Initialize(string primarySmtpAddress, string legacyDN, IExportContext exportContext)
		{
			this.PrimarySmtpAddress = primarySmtpAddress;
			this.LegacyDistinguishedName = legacyDN;
			this.ExportContext = exportContext;
			if (this.ExportContext != null)
			{
				if (!this.ExportContext.IsResume)
				{
					this.DeleteWorkingFolders();
				}
				this.CreateWorkingFolderIfNotExist();
			}
			this.StatusMailRecipients = new string[0];
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000097C4 File Offset: 0x000079C4
		private BaseFolderType GetWorkingOrResultFolder(string folderName)
		{
			DistinguishedFolderIdType parentFolderId = new DistinguishedFolderIdType
			{
				Id = DistinguishedFolderIdNameType.msgfolderroot,
				Mailbox = new EmailAddressType
				{
					EmailAddress = this.PrimarySmtpAddress
				}
			};
			return this.ewsClient.GetFolderByName(this.PrimarySmtpAddress, parentFolderId, folderName);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00009810 File Offset: 0x00007A10
		private BaseFolderType GetOrCreateRecycleFolder(bool createFolderIfNotExist)
		{
			DistinguishedFolderIdType folderId = new DistinguishedFolderIdType
			{
				Id = DistinguishedFolderIdNameType.root,
				Mailbox = new EmailAddressType
				{
					EmailAddress = this.PrimarySmtpAddress
				}
			};
			BaseFolderType folderById = this.ewsClient.GetFolderById(this.PrimarySmtpAddress, folderId);
			BaseFolderType baseFolderType = this.ewsClient.GetFolderByName(this.PrimarySmtpAddress, folderById.FolderId, Constants.MailboxSearchRecycleFolderName);
			if (baseFolderType == null && createFolderIfNotExist)
			{
				baseFolderType = this.CreateFolder(folderById.FolderId, Constants.MailboxSearchRecycleFolderName, true);
			}
			return baseFolderType;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00009894 File Offset: 0x00007A94
		private void CreateWorkingFolderIfNotExist()
		{
			BaseFolderIdType parentFolderId = new DistinguishedFolderIdType
			{
				Id = DistinguishedFolderIdNameType.msgfolderroot,
				Mailbox = new EmailAddressType
				{
					EmailAddress = this.PrimarySmtpAddress
				}
			};
			this.workingFolder = this.GetWorkingOrResultFolder(this.targetLocation.WorkingLocation);
			if (this.workingFolder == null)
			{
				FolderType folderType = new FolderType
				{
					DisplayName = this.targetLocation.WorkingLocation
				};
				folderType.ExtendedProperty = new ExtendedPropertyType[]
				{
					MailboxItemIdList.IsHiddenExtendedProperty
				};
				List<BaseFolderType> list = this.ewsClient.CreateFolder(this.PrimarySmtpAddress, parentFolderId, new BaseFolderType[]
				{
					folderType
				});
				this.workingFolder = list[0];
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00009951 File Offset: 0x00007B51
		private void DeleteWorkingFolders()
		{
			this.RemoveItemIdList(null, false);
			this.RemoveItemIdList(null, true);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00009964 File Offset: 0x00007B64
		private BaseFolderType CreateRootResultFolderIfNotExist()
		{
			BaseFolderType baseFolderType = this.GetWorkingOrResultFolder(this.targetLocation.ExportLocation);
			if (baseFolderType == null)
			{
				DistinguishedFolderIdType parentFolderId = new DistinguishedFolderIdType
				{
					Id = DistinguishedFolderIdNameType.msgfolderroot,
					Mailbox = new EmailAddressType
					{
						EmailAddress = this.PrimarySmtpAddress
					}
				};
				baseFolderType = this.CreateFolder(parentFolderId, this.targetLocation.ExportLocation, false);
			}
			return baseFolderType;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000099C4 File Offset: 0x00007BC4
		private string ToSafeFileNameString(string fileName)
		{
			return TargetMailbox.InvalidFileCharExpression.Replace(fileName, "_");
		}

		// Token: 0x04000092 RID: 146
		private static Regex invalidFileCharExpression;

		// Token: 0x04000093 RID: 147
		private readonly IEwsClient ewsClient;

		// Token: 0x04000094 RID: 148
		private readonly ITargetLocation targetLocation;

		// Token: 0x04000095 RID: 149
		private BaseFolderType workingFolder;

		// Token: 0x04000096 RID: 150
		private OrganizationId organizationId;

		// Token: 0x04000097 RID: 151
		private bool disposed;

		// Token: 0x04000098 RID: 152
		private string logItemId;

		// Token: 0x04000099 RID: 153
		private string exportRecordAttachmentId;

		// Token: 0x0400009A RID: 154
		private AttachmentLog exportRecordAttachmentLog;

		// Token: 0x0400009B RID: 155
		private string exportRecordLogFileName;

		// Token: 0x0400009C RID: 156
		private IStatusLog statusLog;
	}
}
