using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.FreeBusy;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000795 RID: 1941
	[Cmdlet("Install", "FreeBusyFolder")]
	public sealed class InstallFreeBusyFolder : Task
	{
		// Token: 0x06004463 RID: 17507 RVA: 0x001187C0 File Offset: 0x001169C0
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			try
			{
				Server server = LocalServer.GetServer();
				PublicFolderDatabase[] e14SP1PublicFolderDatabases = this.GetE14SP1PublicFolderDatabases();
				if (e14SP1PublicFolderDatabases == null)
				{
					base.WriteVerbose(Strings.InstallFreeBusyFolderNoPublicFolderDatabase);
				}
				else if (!this.HasPublicFolderDatabase(server, e14SP1PublicFolderDatabases))
				{
					base.WriteVerbose(Strings.InstallFreeBusyFolderNoPublicFolderDatabase);
				}
				else
				{
					this.EnsureExternalFreeBusyFolder(server, e14SP1PublicFolderDatabases);
				}
			}
			catch (LocalizedException exception)
			{
				base.WriteVerbose(Strings.InstallFreeBusyFolderGeneralFailure(this.GetExceptionString(exception)));
			}
		}

		// Token: 0x06004464 RID: 17508 RVA: 0x00118838 File Offset: 0x00116A38
		private string GetExceptionString(Exception exception)
		{
			StringBuilder stringBuilder = new StringBuilder(1000);
			while (exception != null)
			{
				stringBuilder.AppendLine(exception.ToString());
				stringBuilder.AppendLine();
				exception = exception.InnerException;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004465 RID: 17509 RVA: 0x00118878 File Offset: 0x00116A78
		private bool HasPublicFolderDatabase(Server server, PublicFolderDatabase[] publicFolderDatabases)
		{
			foreach (PublicFolderDatabase publicFolderDatabase in publicFolderDatabases)
			{
				if (publicFolderDatabase.Server.Equals(server.Id))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004466 RID: 17510 RVA: 0x001188BC File Offset: 0x00116ABC
		private void EnsureExternalFreeBusyFolder(Server localServer, PublicFolderDatabase[] publicFolderDatabases)
		{
			Organization orgContainer = this.configurationSession.GetOrgContainer();
			if (orgContainer == null)
			{
				base.WriteVerbose(Strings.InstallFreeBusyFolderCannotGetOrganizationContainer);
				return;
			}
			string legacyDN = orgContainer.LegacyExchangeDN + "/ou=External (FYDIBOHF25SPDLT)";
			if (this.IsExternalFreeBusyFolderCreated(legacyDN, publicFolderDatabases))
			{
				base.WriteVerbose(Strings.InstallFreeBusyFolderAlreadyExists);
				return;
			}
			using (PublicFolderSession publicFolderSession = this.GetPublicFolderSession(localServer))
			{
				StoreObjectId freeBusyFolderId = FreeBusyFolder.GetFreeBusyFolderId(publicFolderSession, legacyDN, FreeBusyFolderDisposition.CreateIfNeeded);
				if (freeBusyFolderId == null)
				{
					base.WriteVerbose(Strings.InstallFreeBusyFolderUnableToCreateFolder);
				}
				else
				{
					base.WriteVerbose(Strings.InstallFreeBusyFolderCreatedFolder(freeBusyFolderId.ToString()));
					using (Folder folder = Folder.Bind(publicFolderSession, freeBusyFolderId, new PropertyDefinition[]
					{
						FolderSchema.ReplicaList
					}))
					{
						string[] array = Array.ConvertAll<PublicFolderDatabase, string>(publicFolderDatabases, (PublicFolderDatabase database) => database.ExchangeLegacyDN);
						string[] secondArray = (string[])folder[FolderSchema.ReplicaList];
						if (!this.IsEqualsArrayOfLegacyDN(array, secondArray))
						{
							folder[FolderSchema.ReplicaList] = array;
							folder.Save();
							folder.Load();
						}
					}
				}
			}
		}

		// Token: 0x06004467 RID: 17511 RVA: 0x001189F4 File Offset: 0x00116BF4
		private bool IsEqualsArrayOfLegacyDN(string[] firstArray, string[] secondArray)
		{
			HashSet<string> hashSet = new HashSet<string>(firstArray, StringComparer.InvariantCultureIgnoreCase);
			HashSet<string> equals = new HashSet<string>(secondArray, StringComparer.InvariantCultureIgnoreCase);
			return hashSet.SetEquals(equals);
		}

		// Token: 0x06004468 RID: 17512 RVA: 0x00118A20 File Offset: 0x00116C20
		private bool IsExternalFreeBusyFolderCreated(string legacyDN, PublicFolderDatabase[] publicFolderDatabases)
		{
			foreach (PublicFolderDatabase publicFolderDatabase in publicFolderDatabases)
			{
				if (this.IsExternalFreeBusyFolderCreated(legacyDN, publicFolderDatabase))
				{
					base.WriteVerbose(Strings.InstallFreeBusyFolderAlreadyExistsInDatabase(publicFolderDatabase.Id.ToString()));
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004469 RID: 17513 RVA: 0x00118A68 File Offset: 0x00116C68
		private bool IsExternalFreeBusyFolderCreated(string legacyDN, PublicFolderDatabase publicFolderDatabase)
		{
			Server server = publicFolderDatabase.GetServer();
			if (server == null)
			{
				return false;
			}
			bool result;
			try
			{
				using (PublicFolderSession publicFolderSession = this.GetPublicFolderSession(server))
				{
					StoreObjectId freeBusyFolderId = FreeBusyFolder.GetFreeBusyFolderId(publicFolderSession, legacyDN, FreeBusyFolderDisposition.None);
					result = (freeBusyFolderId != null);
				}
			}
			catch (LocalizedException)
			{
				base.WriteVerbose(Strings.InstallFreeBusyFolderUnableToCheckDatabase(publicFolderDatabase.Id.ToString()));
				result = false;
			}
			return result;
		}

		// Token: 0x0600446A RID: 17514 RVA: 0x00118AFE File Offset: 0x00116CFE
		private PublicFolderSession GetPublicFolderSession(Server server)
		{
			return FreeBusyFolder.RetryOnStorageTransientException<PublicFolderSession>(() => PublicFolderSession.OpenAsAdmin(OrganizationId.ForestWideOrgId, null, Guid.Empty, null, CultureInfo.CurrentCulture, "Client=management;Action=Install-FreeBusyFolder", null));
		}

		// Token: 0x0600446B RID: 17515 RVA: 0x00118B24 File Offset: 0x00116D24
		private PublicFolderDatabase[] GetE14SP1PublicFolderDatabases()
		{
			PublicFolderDatabase[] array = this.configurationSession.Find<PublicFolderDatabase>(null, QueryScope.SubTree, null, null, 1000);
			if (array != null)
			{
				List<PublicFolderDatabase> list = new List<PublicFolderDatabase>(array.Length);
				foreach (PublicFolderDatabase publicFolderDatabase in array)
				{
					Server server = publicFolderDatabase.GetServer();
					if (server != null && server.IsE14Sp1OrLater)
					{
						list.Add(publicFolderDatabase);
					}
				}
				if (list.Count > 0)
				{
					return list.ToArray();
				}
			}
			return null;
		}

		// Token: 0x04002A68 RID: 10856
		private IConfigurationSession configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 30, "configurationSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\StoreTasks\\InstallFreeBusyFolder.cs");
	}
}
