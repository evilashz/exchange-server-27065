using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200041D RID: 1053
	internal class TargetFolderMRU
	{
		// Token: 0x060025C1 RID: 9665 RVA: 0x000DA8EB File Offset: 0x000D8AEB
		private TargetFolderMRU()
		{
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x000DA8F4 File Offset: 0x000D8AF4
		public static void GetFolders(UserContext userContext, out OwaStoreObjectId[] folderIds, out string[] folderNames, out string[] folderClassNames, out int folderCount)
		{
			folderIds = new OwaStoreObjectId[10];
			folderNames = new string[10];
			folderClassNames = new string[10];
			folderCount = 0;
			SimpleConfiguration<TargetFolderMRUEntry> simpleConfiguration = new SimpleConfiguration<TargetFolderMRUEntry>(userContext);
			simpleConfiguration.Load();
			bool flag = false;
			int i = 0;
			while (i < simpleConfiguration.Entries.Count)
			{
				if (i >= 10)
				{
					break;
				}
				OwaStoreObjectId owaStoreObjectId = null;
				try
				{
					owaStoreObjectId = OwaStoreObjectId.CreateFromString(simpleConfiguration.Entries[i].FolderId);
				}
				catch (OwaInvalidIdFormatException)
				{
					simpleConfiguration.Entries.RemoveAt(i);
					flag = true;
					continue;
				}
				if (!userContext.IsPublicFoldersAvailable() && owaStoreObjectId.IsPublic)
				{
					i++;
				}
				else
				{
					Folder folder = null;
					string text = null;
					string text2 = null;
					try
					{
						folder = Utilities.GetFolder<Folder>(userContext, owaStoreObjectId, new PropertyDefinition[0]);
						if (Utilities.IsFolderSegmentedOut(folder.ClassName, userContext))
						{
							i++;
							continue;
						}
						text2 = folder.ClassName;
						text = Utilities.GetDisplayNameByFolder(folder, userContext);
					}
					catch (ObjectNotFoundException)
					{
						simpleConfiguration.Entries.RemoveAt(i);
						flag = true;
						continue;
					}
					catch (StorageTransientException)
					{
						i++;
						continue;
					}
					finally
					{
						if (folder != null)
						{
							folder.Dispose();
							folder = null;
						}
					}
					folderIds[folderCount] = owaStoreObjectId;
					folderNames[folderCount] = text;
					folderClassNames[folderCount] = text2;
					folderCount++;
					i++;
				}
			}
			while (simpleConfiguration.Entries.Count > 10)
			{
				simpleConfiguration.Entries.RemoveAt(10);
				flag = true;
			}
			if (flag)
			{
				simpleConfiguration.Save();
			}
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x000DAA80 File Offset: 0x000D8C80
		public static IList<TargetFolderMRUEntry> AddAndGetFolders(OwaStoreObjectId folderId, UserContext userContext)
		{
			SimpleConfiguration<TargetFolderMRUEntry> simpleConfiguration = new SimpleConfiguration<TargetFolderMRUEntry>(userContext);
			simpleConfiguration.Load();
			bool flag = false;
			ReadOnlyCollection<TargetFolderMRUEntry> result = new ReadOnlyCollection<TargetFolderMRUEntry>(simpleConfiguration.Entries);
			while (simpleConfiguration.Entries.Count > 10)
			{
				simpleConfiguration.Entries.RemoveAt(10);
				flag = true;
			}
			int i = 0;
			while (i < simpleConfiguration.Entries.Count)
			{
				OwaStoreObjectId owaStoreObjectId = OwaStoreObjectId.CreateFromString(simpleConfiguration.Entries[i].FolderId);
				if (owaStoreObjectId.Equals(folderId))
				{
					if (i == 0)
					{
						if (flag)
						{
							simpleConfiguration.Save();
						}
						return result;
					}
					simpleConfiguration.Entries.RemoveAt(i);
					break;
				}
				else
				{
					i++;
				}
			}
			if (simpleConfiguration.Entries.Count == 10)
			{
				simpleConfiguration.Entries.RemoveAt(9);
			}
			simpleConfiguration.Entries.Insert(0, new TargetFolderMRUEntry(folderId));
			simpleConfiguration.Save();
			return result;
		}

		// Token: 0x04001A11 RID: 6673
		private const int EntryCountMax = 10;
	}
}
