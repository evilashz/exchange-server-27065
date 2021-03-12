using System;
using System.Collections;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200060F RID: 1551
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class MailboxFolders : DataSourceService, IMailboxFolders, IGetListService<MailboxFolderFilter, MailboxFolder>, INewObjectService<MailboxFolder, NewMailboxFolder>
	{
		// Token: 0x0600451B RID: 17691 RVA: 0x000D0F14 File Offset: 0x000CF114
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxFolder?Recurse&MailFolderOnly&ResultSize@R:Self")]
		public PowerShellResults<MailboxFolder> GetList(MailboxFolderFilter filter, SortOptions sort)
		{
			PowerShellResults<MailboxFolder> list = base.GetList<MailboxFolder, MailboxFolderFilter>("Get-MailboxFolder", filter, sort, "Name");
			if (list.Succeeded)
			{
				list.Output = this.LinkFolders(list.Output, filter.FolderPickerType);
			}
			return list;
		}

		// Token: 0x0600451C RID: 17692 RVA: 0x000D0F55 File Offset: 0x000CF155
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxFolder?Identity@R:Self")]
		public PowerShellResults<MailboxFolder> GetObject(Identity identity)
		{
			return base.GetObject<MailboxFolder>("Get-MailboxFolder", (Identity)identity.ToMailboxFolderIdParameter());
		}

		// Token: 0x0600451D RID: 17693 RVA: 0x000D0F6D File Offset: 0x000CF16D
		[PrincipalPermission(SecurityAction.Demand, Role = "New-MailboxFolder?Name&Parent@W:Self")]
		public PowerShellResults<MailboxFolder> NewObject(NewMailboxFolder properties)
		{
			return base.NewObject<MailboxFolder, NewMailboxFolder>("New-MailboxFolder", properties);
		}

		// Token: 0x0600451E RID: 17694 RVA: 0x000D0F7C File Offset: 0x000CF17C
		private MailboxFolder[] LinkFolders(MailboxFolder[] rows, FolderPickerType folderPickerType)
		{
			Hashtable hashtable = new Hashtable();
			for (int i = 0; i < rows.Length; i++)
			{
				hashtable[rows[i].Folder.FolderStoreObjectId] = i;
			}
			for (int j = 0; j < rows.Length; j++)
			{
				MailboxFolder folder = rows[j].Folder;
				if (!(folder.DefaultFolderType == DefaultFolderType.CommunicatorHistory))
				{
					if (folderPickerType == FolderPickerType.VoiceMailFolderPicker)
					{
						if (folder.DefaultFolderType != DefaultFolderType.None && folder.DefaultFolderType != DefaultFolderType.Inbox && folder.DefaultFolderType != DefaultFolderType.Root)
						{
							goto IL_138;
						}
					}
					else
					{
						if (folderPickerType != FolderPickerType.RulesFolderPicker)
						{
							throw new NotSupportedException();
						}
						if (folder.DefaultFolderType == DefaultFolderType.Outbox)
						{
							goto IL_138;
						}
					}
					MailboxFolderId parentFolder = folder.ParentFolder;
					if (parentFolder != null && hashtable.Contains(parentFolder.StoreObjectId))
					{
						int num = (int)hashtable[parentFolder.StoreObjectId];
						rows[num].Children.Add(rows[j]);
					}
				}
				IL_138:;
			}
			for (int k = 0; k < rows.Length; k++)
			{
				if (rows[k].Folder.DefaultFolderType == DefaultFolderType.Root)
				{
					rows[k].Name = RbacPrincipal.Current.Name;
					rows[k].Children.Sort();
					return new MailboxFolder[]
					{
						rows[k]
					};
				}
			}
			return null;
		}

		// Token: 0x04002E58 RID: 11864
		private const string Noun = "MailboxFolder";

		// Token: 0x04002E59 RID: 11865
		internal const string GetCmdlet = "Get-MailboxFolder";

		// Token: 0x04002E5A RID: 11866
		internal const string NewCmdlet = "New-MailboxFolder";

		// Token: 0x04002E5B RID: 11867
		internal const string ReadScope = "@R:Self";

		// Token: 0x04002E5C RID: 11868
		internal const string WriteScope = "@W:Self";

		// Token: 0x04002E5D RID: 11869
		internal const string GetListRole = "Get-MailboxFolder?Recurse&MailFolderOnly&ResultSize@R:Self";

		// Token: 0x04002E5E RID: 11870
		private const string GetObjectRole = "Get-MailboxFolder?Identity@R:Self";

		// Token: 0x04002E5F RID: 11871
		private const string NewObjectRole = "New-MailboxFolder?Name&Parent@W:Self";

		// Token: 0x04002E60 RID: 11872
		internal static readonly MailboxFolders Instance = new MailboxFolders();
	}
}
