using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009CE RID: 2510
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ElcArchiveStoreDataProvider : EwsStoreDataProvider
	{
		// Token: 0x06005CAC RID: 23724 RVA: 0x001821D4 File Offset: 0x001803D4
		public ElcArchiveStoreDataProvider(IExchangePrincipal primaryExchangePrincipal) : base(new LazilyInitialized<IExchangePrincipal>(() => primaryExchangePrincipal))
		{
			IMailboxInfo archiveMailbox = primaryExchangePrincipal.GetArchiveMailbox();
			if (archiveMailbox != null && archiveMailbox.IsRemote)
			{
				throw new NotImplementedException("Cross premise remote archive is not supported yet");
			}
			base.LogonType = new SpecialLogonType?(SpecialLogonType.SystemService);
			base.BudgetType = OpenAsAdminOrSystemServiceBudgetTypeType.RunAsBackgroundLoad;
		}

		// Token: 0x06005CAD RID: 23725 RVA: 0x00182290 File Offset: 0x00180490
		public bool CreateOrUpdateUserConfiguration(byte[] xmlData, FolderId folderId, string configName)
		{
			UserConfiguration userConfig = base.InvokeServiceCall<UserConfiguration>(() => UserConfiguration.Bind(this.Service, configName, folderId, 4));
			if (userConfig != null)
			{
				userConfig.XmlData = xmlData;
				base.InvokeServiceCall(delegate()
				{
					userConfig.Update();
				});
			}
			else
			{
				userConfig = new UserConfiguration(base.Service);
				userConfig.XmlData = xmlData;
				base.InvokeServiceCall(delegate()
				{
					userConfig.Save(configName, folderId);
				});
			}
			return true;
		}

		// Token: 0x06005CAE RID: 23726 RVA: 0x0018236C File Offset: 0x0018056C
		public void DeleteUserConfiguration(FolderId folderId, string configName)
		{
			UserConfiguration userConfig = base.InvokeServiceCall<UserConfiguration>(() => UserConfiguration.Bind(this.Service, configName, folderId, 4));
			if (userConfig != null)
			{
				base.InvokeServiceCall(delegate()
				{
					userConfig.Delete();
				});
			}
		}

		// Token: 0x06005CAF RID: 23727 RVA: 0x001823D0 File Offset: 0x001805D0
		public bool SaveUserConfiguration(byte[] xmlData, string configName, out Exception ex)
		{
			ex = null;
			bool result = false;
			try
			{
				Folder orCreateFolder = base.GetOrCreateFolder(ClientStrings.Inbox, new FolderId(20));
				result = this.CreateOrUpdateUserConfiguration(xmlData, orCreateFolder.Id, configName);
			}
			catch (DataSourceOperationException ex2)
			{
				result = false;
				ex = ex2;
			}
			return result;
		}

		// Token: 0x06005CB0 RID: 23728 RVA: 0x00182444 File Offset: 0x00180644
		public Folder GetDefaultFolder(WellKnownFolderName folderName, out Exception ex)
		{
			Folder result = null;
			ex = null;
			try
			{
				result = base.InvokeServiceCall<Folder>(() => Folder.Bind(this.Service, folderName));
			}
			catch (DataSourceOperationException ex2)
			{
				ex = ex2;
			}
			return result;
		}

		// Token: 0x06005CB1 RID: 23729 RVA: 0x001824C8 File Offset: 0x001806C8
		public Folder GetFolder(WellKnownFolderName folderName, List<PropertyDefinitionBase> properties)
		{
			PropertySet retentionPropertySet = new PropertySet(0, properties);
			return base.InvokeServiceCall<Folder>(() => Folder.Bind(this.Service, folderName, retentionPropertySet));
		}

		// Token: 0x06005CB2 RID: 23730 RVA: 0x00182508 File Offset: 0x00180708
		public Folder GetCreateFolder(string displayName, FolderId parentFolder)
		{
			return base.GetOrCreateFolder(displayName, parentFolder);
		}

		// Token: 0x06005CB3 RID: 23731 RVA: 0x00182530 File Offset: 0x00180730
		public void SaveFolder(Folder folder, FolderId parentId)
		{
			base.InvokeServiceCall(delegate()
			{
				folder.Save(parentId);
			});
		}

		// Token: 0x06005CB4 RID: 23732 RVA: 0x00182578 File Offset: 0x00180778
		public void UpdateFolder(Folder folder)
		{
			base.InvokeServiceCall(delegate()
			{
				folder.Update();
			});
		}

		// Token: 0x06005CB5 RID: 23733 RVA: 0x001825CC File Offset: 0x001807CC
		public bool MoveItems(List<ItemId> itemIds, FolderId destinationFolderIdInArchive, out Exception ex)
		{
			ex = null;
			bool result;
			try
			{
				base.InvokeServiceCall(delegate()
				{
					this.Service.MoveItems(itemIds, destinationFolderIdInArchive);
				});
				result = true;
			}
			catch (DataSourceOperationException ex2)
			{
				ex = ex2;
				result = false;
			}
			return result;
		}

		// Token: 0x06005CB6 RID: 23734 RVA: 0x00182908 File Offset: 0x00180B08
		public IEnumerable<Folder> GetFolderHierarchy(int pageSize, List<PropertyDefinitionBase> properties, WellKnownFolderName rootFolder)
		{
			FolderView folderView = new FolderView(pageSize);
			folderView.Traversal = 1;
			folderView.PropertySet = new PropertySet(0, properties.ToArray());
			for (;;)
			{
				FindFoldersResults findFolderResults = base.InvokeServiceCall<FindFoldersResults>(() => this.Service.FindFolders(rootFolder, folderView));
				IEnumerable<Folder> folders = findFolderResults.Folders;
				foreach (Folder folder in folders)
				{
					yield return folder;
				}
				if (!findFolderResults.MoreAvailable)
				{
					break;
				}
				folderView.Offset = findFolderResults.NextPageOffset.Value;
			}
			yield break;
		}

		// Token: 0x06005CB7 RID: 23735 RVA: 0x0018293C File Offset: 0x00180B3C
		public void DeleteUserConfiguration(string configName, out Exception ex)
		{
			ex = null;
			try
			{
				Folder orCreateFolder = base.GetOrCreateFolder(ClientStrings.Inbox, new FolderId(20));
				this.DeleteUserConfiguration(orCreateFolder.Id, configName);
			}
			catch (DataSourceOperationException ex2)
			{
				ex = ex2;
			}
		}
	}
}
