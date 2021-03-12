using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Exchange.InfoWorker.EventLog;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200004F RID: 79
	internal class FolderProcessor
	{
		// Token: 0x060002C7 RID: 711 RVA: 0x00010D80 File Offset: 0x0000EF80
		internal FolderProcessor(MailboxSession mailboxSession, List<AdFolderData> userPolicies)
		{
			this.mailboxSession = mailboxSession;
			this.userPolicies = userPolicies;
			this.BuildListOfProvisionedFolders();
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00010DB2 File Offset: 0x0000EFB2
		internal MailboxSession MailboxSession
		{
			get
			{
				return this.mailboxSession;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00010DBA File Offset: 0x0000EFBA
		internal List<ProvisionedFolder> ProvisionedFolderList
		{
			get
			{
				return this.provisionedFolderList;
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00010DC2 File Offset: 0x0000EFC2
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "Folder Processor for " + this.mailboxSession.MailboxOwner;
			}
			return this.toString;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00010DED File Offset: 0x0000EFED
		internal static string NormalizeFolderPath(string folderPath)
		{
			return folderPath.Replace(new string('', 1), "\\/").Replace('', '\\');
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00010E14 File Offset: 0x0000F014
		internal static IEnumerator<List<object[]>> GetFolderHierarchy(DefaultFolderType folderType, MailboxSession mailboxSession, PropertyDefinition[] dataColumns)
		{
			Folder folder = Folder.Bind(mailboxSession, folderType, dataColumns);
			QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.DeepTraversal, null, null, dataColumns);
			return new FolderProcessor.FolderEnumerator(queryResult, folder, folder.GetProperties(dataColumns));
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00010E44 File Offset: 0x0000F044
		internal string GetFolderPathFromId(StoreObjectId folderId)
		{
			string result = null;
			foreach (object[] array in this.entireFolderList)
			{
				if (ElcMailboxHelper.Exists(array[0]))
				{
					StoreObjectId objectId = ((VersionedId)array[0]).ObjectId;
					if (objectId.Equals(folderId))
					{
						result = (ElcMailboxHelper.Exists(array[7]) ? ((string)array[7]) : null);
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00010ED0 File Offset: 0x0000F0D0
		internal ProvisionedFolder GetFolderFromId(Guid elcFolderGuid)
		{
			foreach (ProvisionedFolder provisionedFolder in this.provisionedFolderList)
			{
				if (provisionedFolder.IsProvisionedFolder && provisionedFolder.ElcFolderGuid == elcFolderGuid)
				{
					return provisionedFolder;
				}
			}
			return null;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00010F3C File Offset: 0x0000F13C
		internal ProvisionedFolder GetFolderFromId(StoreObjectId folderId)
		{
			foreach (ProvisionedFolder provisionedFolder in this.provisionedFolderList)
			{
				if (provisionedFolder.FolderId.Equals(folderId))
				{
					return provisionedFolder;
				}
			}
			return null;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00010FA0 File Offset: 0x0000F1A0
		internal StoreObjectId GetSubfolderUnderTarget(StoreObjectId targetRootId, Folder sourceFolder, string elcPolicyName)
		{
			FolderProcessor.Tracer.TraceDebug<FolderProcessor, string>((long)this.GetHashCode(), "{0}: ExpirationEnforcer is creating folder hierarchy under target folder. Source folder name: {1}.", this, sourceFolder.DisplayName);
			string targetRootPath;
			string sourcePath;
			this.GetFullPathOfTargetAndSource(targetRootId, sourceFolder, elcPolicyName, out targetRootPath, out sourcePath);
			StoreObjectId storeObjectId = null;
			string startFolderPath = null;
			string[] array;
			this.GetSubfoldersToCreate(targetRootPath, sourcePath, targetRootId, out array, out storeObjectId, out startFolderPath);
			if (array != null)
			{
				storeObjectId = this.CreateFolderHierarchy(array, storeObjectId, sourcePath, targetRootPath, elcPolicyName, startFolderPath);
			}
			return storeObjectId;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00011000 File Offset: 0x0000F200
		internal bool IsPolicyValid(ProvisionedFolder provisionedFolder, ContentSetting policy, string itemClass, MailboxDataForFolders mailboxData)
		{
			if (!ElcPolicySettings.ArePolicyPropertiesValid(policy, TraceContext.Get(), this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()))
			{
				return false;
			}
			FolderProcessor.CircularPolicyType circularPolicyType = this.LookForCircularPolicies(provisionedFolder, policy, itemClass, mailboxData);
			if (circularPolicyType == FolderProcessor.CircularPolicyType.BadCycle)
			{
				throw new SkipException(Strings.descCycleInPolicies(this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(), policy.Name));
			}
			return true;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00011084 File Offset: 0x0000F284
		private void GetFullPathOfTargetAndSource(StoreObjectId targetRootId, Folder sourceFolder, string elcPolicyName, out string targetRootPath, out string sourcePath)
		{
			targetRootPath = null;
			sourcePath = null;
			targetRootPath = this.GetFolderPathFromId(targetRootId);
			if (!string.IsNullOrEmpty(targetRootPath))
			{
				FolderProcessor.Tracer.TraceDebug<FolderProcessor, string, string>((long)this.GetHashCode(), "{0}: ExpirationEnforcer is creating folder hierarchy under target folder. Source folder name: {1}. Full path of target folder is {2}.", this, sourceFolder.DisplayName, targetRootPath);
			}
			sourcePath = this.GetFolderPathFromId(sourceFolder.Id.ObjectId);
			if (!string.IsNullOrEmpty(sourcePath))
			{
				FolderProcessor.Tracer.TraceDebug<FolderProcessor, string, string>((long)this.GetHashCode(), "{0}: ExpirationEnforcer is creating folder hierarchy under target folder. Source folder name: {1}. Full path of source folder is {2}.", this, sourceFolder.DisplayName, sourcePath);
			}
			if (string.IsNullOrEmpty(targetRootPath) || string.IsNullOrEmpty(sourcePath))
			{
				FolderProcessor.Tracer.TraceError((long)this.GetHashCode(), "{0}: ExpirationEnforcer: Failed to process folder '{1}', policy '{2}'. Folder hierarchy could not be created under target folder because the folderUrl was not found for either the source or target folders. Source folderUrl: '{3}'. Target folderUrl: '{4}'.", new object[]
				{
					this,
					sourceFolder.DisplayName,
					elcPolicyName,
					sourcePath,
					targetRootPath
				});
				Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_FailedToCreateFolderHierarchy, null, new object[]
				{
					sourceFolder.DisplayName,
					this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(),
					elcPolicyName
				});
				throw new InvalidExpiryDestinationException(Strings.descMissingFolderUrl(sourceFolder.DisplayName, this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()));
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x000111E0 File Offset: 0x0000F3E0
		private void GetSubfoldersToCreate(string targetRootPath, string sourcePath, StoreObjectId targetRootId, out string[] subfoldersToCreate, out StoreObjectId startFolderId, out string startFolderPath)
		{
			subfoldersToCreate = null;
			startFolderPath = null;
			startFolderId = null;
			string empty = string.Empty;
			int num = 0;
			string text = string.Concat(new object[]
			{
				targetRootPath.TrimEnd(new char[]
				{
					'/'
				}),
				'/',
				sourcePath.TrimStart(new char[]
				{
					'/'
				}),
				'/'
			});
			foreach (object[] array in this.entireFolderList)
			{
				string text2 = ElcMailboxHelper.Exists(array[7]) ? ((string)array[7]) : null;
				if (string.IsNullOrEmpty(text2))
				{
					FolderProcessor.Tracer.TraceError<FolderProcessor, string>((long)this.GetHashCode(), "{0}: ExpirationEnforcer is creating folder hierarchy under target folder. Skipping current folder during second pass through folder list, since it is missing UrlName property. Source folder: {1}.", this, sourcePath);
				}
				else
				{
					text2 += '/';
					if (text.StartsWith(text2, StringComparison.OrdinalIgnoreCase) && text2.Length > num)
					{
						if (ElcMailboxHelper.Exists(array[0]))
						{
							num = text2.Length;
							startFolderId = ((VersionedId)array[0]).ObjectId;
							startFolderPath = text2;
						}
						else
						{
							FolderProcessor.Tracer.TraceDebug<FolderProcessor, string>((long)this.GetHashCode(), "{0}: ExpirationEnforcer is creating folder hierarchy under target. Skipping current folder during second pass through folder list, since it is missing versionedId. Source folder: {1}.", this, sourcePath);
						}
					}
				}
			}
			if (num == text.Length)
			{
				return;
			}
			if (startFolderId == null)
			{
				FolderProcessor.Tracer.TraceDebug<FolderProcessor, string>((long)this.GetHashCode(), "{0}: ExpirationEnforcer is creating folder hierarchy under target. No matching folder found under the target root, so the hierarchy will be created, starting from the target root folder.Source folder: {1}.", this, sourcePath);
				startFolderId = targetRootId;
				num = targetRootPath.Length;
			}
			string text3 = text.Substring(num);
			text3 = HttpUtility.UrlDecode(text3);
			subfoldersToCreate = text3.Split(new char[]
			{
				'/'
			}, StringSplitOptions.RemoveEmptyEntries);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00011398 File Offset: 0x0000F598
		private StoreObjectId CreateFolderHierarchy(string[] subfoldersToCreate, StoreObjectId startFolderId, string sourcePath, string targetRootPath, string elcPolicyName, string startFolderPath)
		{
			string text = null;
			string text2 = string.Empty;
			foreach (string text2 in subfoldersToCreate)
			{
				text2 = text2.Replace('', '/');
				text2 = text2.Trim();
				FolderProcessor.Tracer.TraceDebug<FolderProcessor, string, string>((long)this.GetHashCode(), "{0}: ExpirationEnforcer is creating folder hierarchy under target folder. Source folder: {1}. Going to create subfolder {2} under target.", this, sourcePath, text2);
				try
				{
					using (Folder folder = Folder.Create(this.mailboxSession, startFolderId, StoreObjectType.Folder, text2, CreateMode.OpenIfExists))
					{
						object obj = folder.TryGetProperty(FolderSchema.ExtendedFolderFlags);
						if (!(obj is ExtendedFolderFlags))
						{
							folder[FolderSchema.ExtendedFolderFlags] = ExtendedFolderFlags.ShowTotal;
						}
						FolderSaveResult folderSaveResult = folder.Save();
						if (folderSaveResult.OperationResult != OperationResult.Succeeded)
						{
							text = folderSaveResult.ToString();
							break;
						}
						folder.Load();
						startFolderId = folder.Id.ObjectId;
						FolderProcessor.Tracer.TraceDebug<FolderProcessor, string, string>((long)this.GetHashCode(), "{0}: ExpirationEnforcer is creating folder hierarchy under target folder. Source folder name: {1}. Just created subfolder {2} under target.", this, sourcePath, folder.DisplayName);
					}
				}
				catch (PropertyErrorException ex)
				{
					text = ex.ToString();
					break;
				}
				catch (CorruptDataException ex2)
				{
					text = ex2.ToString();
					break;
				}
				catch (ObjectNotFoundException ex3)
				{
					text = ex3.ToString();
					break;
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				FolderProcessor.Tracer.TraceError((long)this.GetHashCode(), "{0}: ExpirationEnforcer: Failed to create folder hierarchy. Policy '{1}'. Source folder path '{2}'.  Target folder path '{3}'. Creating the subfolder '{4}' under '{5}' failed. Error: ", new object[]
				{
					this,
					elcPolicyName,
					sourcePath,
					targetRootPath,
					text2,
					startFolderPath,
					text
				});
				Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_FailedToCreateFolderHierarchy, null, new object[]
				{
					sourcePath,
					this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(),
					elcPolicyName,
					targetRootPath,
					text2,
					startFolderPath,
					text
				});
				throw new InvalidExpiryDestinationException(Strings.descFailedToCreateFolderHierarchy(sourcePath, this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()));
			}
			return startFolderId;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000115C4 File Offset: 0x0000F7C4
		private void BuildListOfProvisionedFolders()
		{
			VersionedId rootFolderId = null;
			string text = null;
			using (Folder folder = Folder.Bind(this.MailboxSession, DefaultFolderType.Root, FolderProcessor.DataColumns))
			{
				rootFolderId = folder.Id;
				try
				{
					if (ElcMailboxHelper.Exists(folder[FolderSchema.ELCPolicyIds]))
					{
						text = (string)folder[FolderSchema.ELCPolicyIds];
						List<object> list = new List<object>(folder.GetProperties(FolderProcessor.DataColumns));
						list.Add(list[3]);
						this.entireFolderList.Add(list.ToArray());
						FolderProcessor.Tracer.TraceDebug<FolderProcessor, string>((long)this.GetHashCode(), "{0}: Root Policy '{1}' is set for this mailbox.", this, text);
					}
				}
				catch (PropertyErrorException ex)
				{
					if (ex.PropertyErrors[0].PropertyErrorCode != PropertyErrorCode.NotFound)
					{
						throw;
					}
					FolderProcessor.Tracer.TraceDebug<FolderProcessor>((long)this.GetHashCode(), "{0}: Root Policy does not exist for this mailbox.", this);
				}
				using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.DeepTraversal, null, null, FolderProcessor.DataColumns))
				{
					for (;;)
					{
						object[][] rows = queryResult.GetRows(100);
						if (rows.Length <= 0)
						{
							break;
						}
						for (int i = 0; i < rows.Length; i++)
						{
							this.entireFolderList.Add(rows[i]);
						}
					}
				}
			}
			ElcMailboxHelper.PopulateFolderPathProperty(this.entireFolderList, new FolderPathIndices(3, 6, 0, 1, 7));
			for (int j = 0; j < this.entireFolderList.Count; j++)
			{
				ProvisionedFolder provisionedFolder = this.GetProvisionedFolder(j, rootFolderId, text);
				if (provisionedFolder != null)
				{
					this.ProvisionedFolderList.Add(provisionedFolder);
				}
			}
			FolderProcessor.Tracer.TraceDebug<FolderProcessor, int>((long)this.GetHashCode(), "{0}: Number of folders found with policies for this mailbox is: {1}.", this, this.ProvisionedFolderList.Count);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00011780 File Offset: 0x0000F980
		private ProvisionedFolder GetProvisionedFolder(int currFolderIdx, VersionedId rootFolderId, string rootFolderPolicyIds)
		{
			bool flag = false;
			if (!ElcMailboxHelper.Exists(this.entireFolderList[currFolderIdx][2]))
			{
				flag = true;
				FolderProcessor.Tracer.TraceDebug<FolderProcessor, object>((long)this.GetHashCode(), "{0}: Folder '{1}' does not have any policy associated. Checking parent and root.", this, this.entireFolderList[currFolderIdx][3]);
				object obj = this.entireFolderList[currFolderIdx][1];
				int num = currFolderIdx;
				while (ElcMailboxHelper.Exists(obj) && !((StoreObjectId)obj).Equals(rootFolderId.ObjectId))
				{
					int num2 = -1;
					int num3 = (num == 0) ? (this.entireFolderList.Count - 1) : (num - 1);
					while (num3 != num)
					{
						VersionedId versionedId = (VersionedId)this.entireFolderList[num3][0];
						if (versionedId.ObjectId.Equals((StoreObjectId)obj))
						{
							num2 = num3;
							break;
						}
						if (num3 == 0)
						{
							num3 = this.entireFolderList.Count - 1;
						}
						else
						{
							num3--;
						}
					}
					if (num2 == -1)
					{
						throw new SkipException(Strings.descMissingParentFolder(this.entireFolderList[num][3], obj));
					}
					if (ElcMailboxHelper.Exists(this.entireFolderList[num2][2]))
					{
						FolderProcessor.Tracer.TraceDebug<FolderProcessor, object, object>((long)this.GetHashCode(), "{0}: Parent folder '{1}' of folder '{2}' has policy. Assigning to child.", this, this.entireFolderList[num2][3], this.entireFolderList[currFolderIdx][3]);
						this.entireFolderList[currFolderIdx][2] = this.entireFolderList[num2][2];
						break;
					}
					obj = this.entireFolderList[num2][1];
					num = num2;
				}
				if (!ElcMailboxHelper.Exists(this.entireFolderList[currFolderIdx][2]) && !string.IsNullOrEmpty(rootFolderPolicyIds))
				{
					FolderProcessor.Tracer.TraceDebug<FolderProcessor, object>((long)this.GetHashCode(), "{0}: Folder '{1}' takes on root policy.", this, this.entireFolderList[currFolderIdx][3]);
					this.entireFolderList[currFolderIdx][2] = rootFolderPolicyIds;
				}
			}
			if (!ElcMailboxHelper.Exists(this.entireFolderList[currFolderIdx][2]))
			{
				FolderProcessor.Tracer.TraceDebug<FolderProcessor, object>((long)this.GetHashCode(), "{0}: Folder '{1}' being discarded because it does not have an associated folder object.", this, this.entireFolderList[currFolderIdx][3]);
				return null;
			}
			string displayName = (string)this.entireFolderList[currFolderIdx][3];
			AdFolderData folderDataFromList = this.GetFolderDataFromList((string)this.entireFolderList[currFolderIdx][2]);
			if (folderDataFromList.Folder.BaseFolderOnly && flag)
			{
				if (string.IsNullOrEmpty(rootFolderPolicyIds) || !((string)this.entireFolderList[currFolderIdx][2] != rootFolderPolicyIds))
				{
					FolderProcessor.Tracer.TraceDebug<FolderProcessor, object>((long)this.GetHashCode(), "{0}: Folder '{1}' being discarded because it does not inherit policy and not root policy is defined..", this, this.entireFolderList[currFolderIdx][3]);
					return null;
				}
				FolderProcessor.Tracer.TraceDebug<FolderProcessor, object>((long)this.GetHashCode(), "{0}: Folder '{1}' takes on root policy.", this, this.entireFolderList[currFolderIdx][3]);
				this.entireFolderList[currFolderIdx][2] = rootFolderPolicyIds;
				folderDataFromList = this.GetFolderDataFromList((string)this.entireFolderList[currFolderIdx][2]);
			}
			VersionedId versionedId2 = (VersionedId)this.entireFolderList[currFolderIdx][0];
			bool isProvisionedFolder = ElcMailboxHelper.Exists(this.entireFolderList[currFolderIdx][4]) && ((int)this.entireFolderList[currFolderIdx][4] & 1) != 0;
			string containerClass = null;
			if (ElcMailboxHelper.Exists(this.entireFolderList[currFolderIdx][5]))
			{
				containerClass = (string)this.entireFolderList[currFolderIdx][5];
			}
			string fullFolderPath = string.Empty;
			if (ElcMailboxHelper.Exists(this.entireFolderList[currFolderIdx][7]))
			{
				fullFolderPath = HttpUtility.UrlDecode((string)this.entireFolderList[currFolderIdx][7]);
			}
			if (folderDataFromList == null || folderDataFromList.FolderSettings == null)
			{
				FolderProcessor.Tracer.TraceDebug<FolderProcessor, object>((long)this.GetHashCode(), "{0}: Folder '{1}' has an associated ElcFolder object, but no policy applies to it.", this, this.entireFolderList[currFolderIdx][3]);
			}
			return new ProvisionedFolder(versionedId2.ObjectId, displayName, fullFolderPath, containerClass, isProvisionedFolder, folderDataFromList.FolderSettings, new Guid((string)this.entireFolderList[currFolderIdx][2]), flag);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00011BC4 File Offset: 0x0000FDC4
		private AdFolderData GetFolderDataFromList(string elcFolderGuid)
		{
			int num = this.userPolicies.FindIndex((AdFolderData adFolder) => string.Compare(elcFolderGuid, adFolder.Folder.Guid.ToString(), true) == 0);
			if (num > -1)
			{
				return this.userPolicies[num];
			}
			FolderProcessor.Tracer.TraceDebug<FolderProcessor, string>((long)this.GetHashCode(), "{0}: No object in AD found corresponding to ElcFolder guid string '{0}'. This folder will not be processed.", this, elcFolderGuid);
			return null;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00011C28 File Offset: 0x0000FE28
		private FolderProcessor.CircularPolicyType LookForCircularPolicies(ProvisionedFolder provisionedFolder, ContentSetting policy, string itemClass, MailboxDataForFolders mailboxData)
		{
			if (provisionedFolder.ValidatedPolicies.Contains(itemClass))
			{
				return FolderProcessor.CircularPolicyType.NoCycle;
			}
			List<FolderProcessor.FolderPolicy> list = new List<FolderProcessor.FolderPolicy>();
			list.Add(new FolderProcessor.FolderPolicy(provisionedFolder, policy));
			while (policy.RetentionAction == RetentionActionType.MoveToFolder || policy.RetentionAction == RetentionActionType.MoveToDeletedItems)
			{
				ProvisionedFolder destinationFolder = this.GetDestinationFolder(policy, mailboxData);
				if (destinationFolder == null)
				{
					FolderProcessor.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: The destination folder of Policy '{1}' is not in the list of provisioned folders. Quit.", TraceContext.Get(), policy.Name);
				}
				else
				{
					FolderProcessor.CircularPolicyType circularPolicyType = this.CheckDestinationFolder(list, destinationFolder, policy);
					if (circularPolicyType != FolderProcessor.CircularPolicyType.NoCycle)
					{
						return circularPolicyType;
					}
					ContentSetting destinationPolicy = this.GetDestinationPolicy(destinationFolder, itemClass);
					if (destinationPolicy != null)
					{
						list.Add(new FolderProcessor.FolderPolicy(destinationFolder, destinationPolicy));
						policy = destinationPolicy;
						continue;
					}
					FolderProcessor.Tracer.TraceDebug<object, string, string>((long)this.GetHashCode(), "{0}: The destination folder '{1}' of Policy '{2}' does not have a valid policy on it. Quit.", TraceContext.Get(), destinationFolder.FullFolderPath, policy.Name);
				}
				IL_E8:
				provisionedFolder.ValidatedPolicies.Add(itemClass);
				return FolderProcessor.CircularPolicyType.NoCycle;
			}
			FolderProcessor.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: The Policy '{1}' is not a MoveTo policy. Quit", TraceContext.Get(), policy.Name);
			goto IL_E8;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00011D2C File Offset: 0x0000FF2C
		private ProvisionedFolder GetDestinationFolder(ContentSetting policy, MailboxDataForFolders mailboxData)
		{
			if (policy.RetentionAction != RetentionActionType.MoveToFolder || policy.MoveToDestinationFolder == null)
			{
				if (policy.RetentionAction == RetentionActionType.MoveToDeletedItems)
				{
					StoreObjectId defaultFolderId = this.mailboxSession.GetDefaultFolderId(DefaultFolderType.DeletedItems);
					if (defaultFolderId != null)
					{
						return this.GetFolderFromId(defaultFolderId);
					}
				}
				return null;
			}
			Guid folderGuidFromObjectGuid = mailboxData.GetFolderGuidFromObjectGuid(policy.MoveToDestinationFolder);
			if (folderGuidFromObjectGuid == Guid.Empty)
			{
				return null;
			}
			return this.GetFolderFromId(folderGuidFromObjectGuid);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00011DC0 File Offset: 0x0000FFC0
		private FolderProcessor.CircularPolicyType CheckDestinationFolder(List<FolderProcessor.FolderPolicy> visitedNodes, ProvisionedFolder destProvisionedFolder, ContentSetting policy)
		{
			int num = visitedNodes.FindIndex((FolderProcessor.FolderPolicy folderPolicy) => folderPolicy.ProvisionedFolder.FolderId.Equals(destProvisionedFolder.FolderId));
			if (num < 0)
			{
				FolderProcessor.Tracer.TraceDebug<object, ProvisionedFolder>((long)this.GetHashCode(), "{0}: Folder {1} has not been visited before. No cycle yet.", TraceContext.Get(), destProvisionedFolder);
				return FolderProcessor.CircularPolicyType.NoCycle;
			}
			if (num == visitedNodes.Count - 1)
			{
				FolderProcessor.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: Policy '{1}' has source = destination. Quit without error.", TraceContext.Get(), policy.Name);
				return FolderProcessor.CircularPolicyType.GoodCycle;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = num; i < visitedNodes.Count; i++)
			{
				stringBuilder.Append(visitedNodes[i].ProvisionedFolder.DisplayName + ":" + visitedNodes[i].Policy.Name + ", ");
			}
			string text = stringBuilder.ToString().TrimEnd(new char[]
			{
				',',
				' '
			});
			FolderProcessor.Tracer.TraceError<object, string>((long)this.GetHashCode(), "{0}: There is a cycle among the policies in this mailbox. Policies involved (Folder Name:Policy Name) are {1}", TraceContext.Get(), text);
			Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_CycleInPolicies, null, new object[]
			{
				this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(),
				text
			});
			return FolderProcessor.CircularPolicyType.BadCycle;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00011F20 File Offset: 0x00010120
		private ContentSetting GetDestinationPolicy(ProvisionedFolder destProvisionedFolder, string itemClass)
		{
			ContentSetting applyingPolicy = ElcPolicySettings.GetApplyingPolicy(destProvisionedFolder.ElcPolicies, itemClass, destProvisionedFolder.ItemClassToPolicyMapping);
			if (applyingPolicy == null || !ElcPolicySettings.ArePolicyPropertiesValid(applyingPolicy, TraceContext.Get(), this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()))
			{
				return null;
			}
			return applyingPolicy;
		}

		// Token: 0x04000250 RID: 592
		private const int FolderIdIndex = 0;

		// Token: 0x04000251 RID: 593
		private const int ParentIdIndex = 1;

		// Token: 0x04000252 RID: 594
		private const int ElcFolderIdIndex = 2;

		// Token: 0x04000253 RID: 595
		private const int DisplayNameIndex = 3;

		// Token: 0x04000254 RID: 596
		private const int AdminFolderFlagsIndex = 4;

		// Token: 0x04000255 RID: 597
		private const int ContainerClassIndex = 5;

		// Token: 0x04000256 RID: 598
		private const int FolderDepthIndex = 6;

		// Token: 0x04000257 RID: 599
		private const int FolderPathIndex = 7;

		// Token: 0x04000258 RID: 600
		private const char ForwardSlashCode = '';

		// Token: 0x04000259 RID: 601
		private const char BackSlashCode = '';

		// Token: 0x0400025A RID: 602
		private static readonly PropertyDefinition[] DataColumns = new PropertyDefinition[]
		{
			FolderSchema.Id,
			StoreObjectSchema.ParentItemId,
			FolderSchema.ELCPolicyIds,
			StoreObjectSchema.DisplayName,
			FolderSchema.AdminFolderFlags,
			StoreObjectSchema.ContainerClass,
			FolderSchema.FolderHierarchyDepth
		};

		// Token: 0x0400025B RID: 603
		private static readonly Trace Tracer = ExTraceGlobals.CommonEnforcerOperationsTracer;

		// Token: 0x0400025C RID: 604
		private MailboxSession mailboxSession;

		// Token: 0x0400025D RID: 605
		private List<AdFolderData> userPolicies;

		// Token: 0x0400025E RID: 606
		private List<ProvisionedFolder> provisionedFolderList = new List<ProvisionedFolder>();

		// Token: 0x0400025F RID: 607
		private List<object[]> entireFolderList = new List<object[]>();

		// Token: 0x04000260 RID: 608
		private string toString;

		// Token: 0x02000050 RID: 80
		private enum CircularPolicyType
		{
			// Token: 0x04000262 RID: 610
			NoCycle,
			// Token: 0x04000263 RID: 611
			GoodCycle,
			// Token: 0x04000264 RID: 612
			BadCycle
		}

		// Token: 0x02000051 RID: 81
		private class FolderPolicy
		{
			// Token: 0x060002DD RID: 733 RVA: 0x00011FD4 File Offset: 0x000101D4
			internal FolderPolicy(ProvisionedFolder provisionedFolder, ContentSetting policy)
			{
				this.provisionedFolder = provisionedFolder;
				this.policy = policy;
			}

			// Token: 0x170000AE RID: 174
			// (get) Token: 0x060002DE RID: 734 RVA: 0x00011FEA File Offset: 0x000101EA
			internal ProvisionedFolder ProvisionedFolder
			{
				get
				{
					return this.provisionedFolder;
				}
			}

			// Token: 0x170000AF RID: 175
			// (get) Token: 0x060002DF RID: 735 RVA: 0x00011FF2 File Offset: 0x000101F2
			internal ContentSetting Policy
			{
				get
				{
					return this.policy;
				}
			}

			// Token: 0x04000265 RID: 613
			private ProvisionedFolder provisionedFolder;

			// Token: 0x04000266 RID: 614
			private ContentSetting policy;
		}

		// Token: 0x02000052 RID: 82
		private class FolderEnumerator : QueryResultsEnumerator
		{
			// Token: 0x060002E0 RID: 736 RVA: 0x00011FFA File Offset: 0x000101FA
			internal FolderEnumerator(QueryResult queryResult, Folder rootFolder, object[] rootFolderProperties) : base(queryResult)
			{
				this.rootFolder = rootFolder;
				this.rootFolderProperties = rootFolderProperties;
				this.isAtBegining = true;
			}

			// Token: 0x060002E1 RID: 737 RVA: 0x00012018 File Offset: 0x00010218
			public override bool MoveNext()
			{
				bool result = base.MoveNext();
				if (this.isAtBegining)
				{
					if (base.Current != null)
					{
						base.Current.Insert(0, this.rootFolderProperties);
					}
					this.isAtBegining = false;
				}
				return result;
			}

			// Token: 0x060002E2 RID: 738 RVA: 0x00012056 File Offset: 0x00010256
			public override void Dispose()
			{
				base.Dispose();
				if (this.rootFolder != null)
				{
					this.rootFolder.Dispose();
				}
			}

			// Token: 0x060002E3 RID: 739 RVA: 0x00012071 File Offset: 0x00010271
			public override void Reset()
			{
				base.Reset();
				this.isAtBegining = true;
			}

			// Token: 0x060002E4 RID: 740 RVA: 0x00012080 File Offset: 0x00010280
			protected override bool ProcessResults(object[][] partialResults)
			{
				return true;
			}

			// Token: 0x060002E5 RID: 741 RVA: 0x00012083 File Offset: 0x00010283
			protected override void HandleException(Exception exception)
			{
				FolderProcessor.Tracer.TraceDebug<string, Exception>((long)this.rootFolder.GetHashCode(), "{0}: Failed to get folder hierarchy because the folder was not found or was inaccessible. Exception: '{1}'", this.rootFolder.DisplayName, exception);
			}

			// Token: 0x04000267 RID: 615
			private readonly Folder rootFolder;

			// Token: 0x04000268 RID: 616
			private readonly object[] rootFolderProperties;

			// Token: 0x04000269 RID: 617
			private bool isAtBegining;
		}
	}
}
