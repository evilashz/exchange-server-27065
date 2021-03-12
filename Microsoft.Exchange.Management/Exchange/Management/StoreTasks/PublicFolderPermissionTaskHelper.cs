using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x02000CCA RID: 3274
	internal static class PublicFolderPermissionTaskHelper
	{
		// Token: 0x06007E32 RID: 32306 RVA: 0x00203DC4 File Offset: 0x00201FC4
		internal static MailboxFolderIdParameter GetMailboxFolderIdParameterForPublicFolder(IConfigurationSession configurationSession, PublicFolderIdParameter publicFolderIdParameter, Guid publicFolderMailboxGuid, ADUser publicFolderMailboxUser, OrganizationId organizationId, Task.ErrorLoggerDelegate errorHandler)
		{
			if (publicFolderMailboxUser == null)
			{
				publicFolderMailboxUser = PublicFolderPermissionTaskHelper.GetPublicFolderHierarchyMailboxUser(configurationSession);
				if (publicFolderMailboxUser == null)
				{
					errorHandler(new LocalizedException(PublicFolderSession.GetNoPublicFoldersProvisionedError(configurationSession.SessionSettings.CurrentOrganizationId)), ExchangeErrorCategory.Client, publicFolderIdParameter);
				}
			}
			if (publicFolderIdParameter.PublicFolderId.StoreObjectId == null)
			{
				using (PublicFolderDataProvider publicFolderDataProvider = new PublicFolderDataProvider(configurationSession, "*-PublicFolderClientPermission", publicFolderMailboxGuid))
				{
					StoreObjectId storeObjectId = publicFolderDataProvider.ResolveStoreObjectIdFromFolderPath(publicFolderIdParameter.PublicFolderId.MapiFolderPath);
					if (storeObjectId == null)
					{
						errorHandler(new LocalizedException(Strings.ErrorPublicFolderNotFound(publicFolderIdParameter.ToString())), ExchangeErrorCategory.Client, publicFolderIdParameter);
					}
					publicFolderIdParameter.PublicFolderId.StoreObjectId = storeObjectId;
				}
			}
			if (publicFolderIdParameter.Organization != null)
			{
				publicFolderIdParameter.PublicFolderId.OrganizationId = organizationId;
			}
			return new MailboxFolderIdParameter(publicFolderIdParameter, publicFolderMailboxUser);
		}

		// Token: 0x06007E33 RID: 32307 RVA: 0x00203E94 File Offset: 0x00202094
		internal static void SyncPublicFolder(IConfigurationSession configurationSession, StoreObjectId folderId)
		{
			using (PublicFolderDataProvider publicFolderDataProvider = new PublicFolderDataProvider(configurationSession, "*-PublicFolderClientPermission", Guid.Empty))
			{
				PublicFolderSession publicFolderSession = publicFolderDataProvider.PublicFolderSession;
				using (CoreFolder coreFolder = CoreFolder.Bind(publicFolderSession, folderId, new PropertyDefinition[]
				{
					CoreFolderSchema.AclTableAndSecurityDescriptor
				}))
				{
					PublicFolderContentMailboxInfo contentMailboxInfo = coreFolder.GetContentMailboxInfo();
					Guid guid = contentMailboxInfo.IsValid ? contentMailboxInfo.MailboxGuid : Guid.Empty;
					ExchangePrincipal contentMailboxPrincipal;
					if (guid != Guid.Empty && guid != publicFolderSession.MailboxGuid && PublicFolderSession.TryGetPublicFolderMailboxPrincipal(publicFolderSession.OrganizationId, guid, true, out contentMailboxPrincipal))
					{
						PublicFolderSyncJobRpc.SyncFolder(contentMailboxPrincipal, folderId.ProviderLevelItemId);
					}
				}
			}
		}

		// Token: 0x06007E34 RID: 32308 RVA: 0x00203F64 File Offset: 0x00202164
		private static ADUser GetPublicFolderHierarchyMailboxUser(IConfigurationSession configurationSession)
		{
			PublicFolderInformation defaultPublicFolderMailbox = configurationSession.GetOrgContainer().DefaultPublicFolderMailbox;
			Guid hierarchyMailboxGuid = defaultPublicFolderMailbox.HierarchyMailboxGuid;
			if (defaultPublicFolderMailbox.Type != PublicFolderInformation.HierarchyType.MailboxGuid || hierarchyMailboxGuid == Guid.Empty)
			{
				return null;
			}
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.FullyConsistent, configurationSession.SessionSettings, 136, "GetPublicFolderHierarchyMailboxUser", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\MapiTasks\\PublicFolderPermission\\PublicFolderPermissionTaskHelper.cs");
			ADUser[] array = tenantOrRootOrgRecipientSession.FindADUser(null, QueryScope.SubTree, new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADMailboxRecipientSchema.ExchangeGuid, hierarchyMailboxGuid),
				Filters.GetRecipientTypeDetailsFilterOptimization(RecipientTypeDetails.PublicFolderMailbox)
			}), null, 1);
			if (array.Length <= 0)
			{
				return null;
			}
			return array[0];
		}
	}
}
