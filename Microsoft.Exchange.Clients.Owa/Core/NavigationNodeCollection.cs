using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200016B RID: 363
	internal sealed class NavigationNodeCollection : NavigationNodeList<NavigationNodeGroup>
	{
		// Token: 0x06000C87 RID: 3207 RVA: 0x00055618 File Offset: 0x00053818
		private static Dictionary<NavigationNodeGroupSection, Strings.IDs[]> MapLocalizedGroupNames()
		{
			Dictionary<NavigationNodeGroupSection, Strings.IDs[]> dictionary = new Dictionary<NavigationNodeGroupSection, Strings.IDs[]>(7);
			dictionary[NavigationNodeGroupSection.Mail] = new Strings.IDs[]
			{
				1773975404,
				1710133786,
				-801423628
			};
			dictionary[NavigationNodeGroupSection.First] = new Strings.IDs[]
			{
				1773975404,
				1710133786,
				-801423628
			};
			dictionary[NavigationNodeGroupSection.Calendar] = new Strings.IDs[]
			{
				606093953,
				2028589045,
				-242508443
			};
			dictionary[NavigationNodeGroupSection.Contacts] = new Strings.IDs[]
			{
				-353956381,
				159507183,
				-913201897
			};
			dictionary[NavigationNodeGroupSection.Tasks] = new Strings.IDs[]
			{
				1165698602,
				77196276,
				43988026
			};
			dictionary[NavigationNodeGroupSection.Notes] = new Strings.IDs[]
			{
				1125265199,
				-1086241029,
				1972312651
			};
			dictionary[NavigationNodeGroupSection.Journal] = new Strings.IDs[]
			{
				41137364,
				1257607026,
				303762284
			};
			return dictionary;
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x00055752 File Offset: 0x00053952
		private NavigationNodeCollection(UserContext userContext, MailboxSession session, NavigationNodeGroupSection navigationNodeGroupSection, object[][] data)
		{
			this.navigationNodeGroupSection = navigationNodeGroupSection;
			if (!this.IsFavorites)
			{
				this.ThrowIfNonMailGroupSectionNotSupported();
			}
			this.Load(userContext, session, data);
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x0005577C File Offset: 0x0005397C
		private static List<NavigationNodeCollection> LoadNavigationNodeCollection(UserContext userContext, MailboxSession session, params NavigationNodeGroupSection[] groupSections)
		{
			object[][] data = NavigationNodeCollection.LoadCommonViewAssociatedMessages(session);
			List<NavigationNodeCollection> list = new List<NavigationNodeCollection>(groupSections.Length);
			foreach (NavigationNodeGroupSection navigationNodeGroupSection in groupSections)
			{
				list.Add(new NavigationNodeCollection(userContext, session, navigationNodeGroupSection, data));
			}
			return list;
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x000557C4 File Offset: 0x000539C4
		public static NavigationNodeCollection[] TryCreateNavigationNodeCollections(UserContext userContext, MailboxSession session, params NavigationNodeGroupSection[] groupSections)
		{
			bool flag = false;
			List<NavigationNodeCollection> list = null;
			list = NavigationNodeCollection.LoadNavigationNodeCollection(userContext, session, groupSections);
			foreach (NavigationNodeCollection navigationNodeCollection in list)
			{
				if (navigationNodeCollection.CheckIfDataCorrected())
				{
					flag = true;
					navigationNodeCollection.Save(session);
				}
			}
			if (flag)
			{
				list = NavigationNodeCollection.LoadNavigationNodeCollection(userContext, session, groupSections);
				foreach (NavigationNodeCollection navigationNodeCollection2 in list)
				{
					if (navigationNodeCollection2.CheckIfDataCorrected())
					{
						ExTraceGlobals.CoreCallTracer.TraceDebug<NavigationNodeGroupSection>(0L, "Retry twice but read invalid format data for group {0} from mailbox session.", navigationNodeCollection2.GroupSection);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x00055894 File Offset: 0x00053A94
		public static NavigationNodeCollection TryCreateNavigationNodeCollection(UserContext userContext, MailboxSession session, NavigationNodeGroupSection navigationNodeGroupSection)
		{
			return NavigationNodeCollection.TryCreateNavigationNodeCollections(userContext, session, new NavigationNodeGroupSection[]
			{
				navigationNodeGroupSection
			})[0];
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x000558B8 File Offset: 0x00053AB8
		private static Guid GetDefaultGroupClassId(NavigationNodeGroupType navigationNodeGroupType)
		{
			switch (navigationNodeGroupType)
			{
			case NavigationNodeGroupType.MyFoldersGroup:
				return NavigationNodeCollection.MyFoldersClassId;
			case NavigationNodeGroupType.SharedFoldersGroup:
				return NavigationNodeCollection.PeoplesFoldersClassId;
			default:
				return NavigationNodeCollection.OtherFoldersClassId;
			}
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x000558E8 File Offset: 0x00053AE8
		private static Dictionary<PropertyDefinition, int> MapProperties()
		{
			NavigationNodeCollection.nativeStorePropertyDefinitions = StorePropertyDefinition.GetNativePropertyDefinitions<PropertyDefinition>(PropertyDependencyType.AllRead, NavigationNodeCollection.AllProperties);
			Dictionary<PropertyDefinition, int> dictionary = new Dictionary<PropertyDefinition, int>(NavigationNodeCollection.nativeStorePropertyDefinitions.Count);
			int num = 0;
			foreach (NativeStorePropertyDefinition key in NavigationNodeCollection.nativeStorePropertyDefinitions)
			{
				dictionary[key] = num++;
			}
			return dictionary;
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x0005595C File Offset: 0x00053B5C
		private static bool CheckNodeTypes(object[] row, params NavigationNodeType[] types)
		{
			object obj = row[NavigationNodeCollection.PropertyMap[NavigationNodeSchema.Type]];
			if (!(obj is int))
			{
				return false;
			}
			NavigationNodeType navigationNodeType = (NavigationNodeType)obj;
			foreach (NavigationNodeType navigationNodeType2 in types)
			{
				if (navigationNodeType == navigationNodeType2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x000559B4 File Offset: 0x00053BB4
		private static bool CheckItemClassAndSection(NavigationNodeGroupSection navigationNodeGroupSection, object[] row)
		{
			object obj = row[NavigationNodeCollection.PropertyMap[NavigationNodeSchema.GroupSection]];
			if (!(obj is int))
			{
				return false;
			}
			string text = row[NavigationNodeCollection.PropertyMap[StoreObjectSchema.ItemClass]] as string;
			return text != null && text.Equals("IPM.Microsoft.WunderBar.Link", StringComparison.OrdinalIgnoreCase) && navigationNodeGroupSection == (NavigationNodeGroupSection)obj;
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00055A10 File Offset: 0x00053C10
		internal static string GetDefaultGroupSubject(NavigationNodeGroupType groupType, NavigationNodeGroupSection groupSection)
		{
			if (groupSection == NavigationNodeGroupSection.First)
			{
				return LocalizedStrings.GetNonEncoded(364750115);
			}
			return LocalizedStrings.GetNonEncoded(NavigationNodeCollection.localizedGroupNames[groupSection][(int)groupType]);
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x00055A33 File Offset: 0x00053C33
		private static NavigationNodeGroup CreateDefaultGroup(NavigationNodeGroupType groupType, NavigationNodeGroupSection groupSection)
		{
			return new NavigationNodeGroup(NavigationNodeCollection.GetDefaultGroupSubject(groupType, groupSection), groupSection, NavigationNodeCollection.GetDefaultGroupClassId(groupType));
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00055A48 File Offset: 0x00053C48
		public static NavigationNodeGroup CreateCustomizedGroup(NavigationNodeGroupSection groupSection, string groupSubject)
		{
			if (groupSubject == null)
			{
				throw new ArgumentNullException("groupSubject");
			}
			return NavigationNodeCollection.CreateCustomizedGroup(groupSection, groupSubject, Guid.NewGuid());
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00055A64 File Offset: 0x00053C64
		private static NavigationNodeGroup CreateCustomizedGroup(NavigationNodeGroupSection groupSection, string groupSubject, Guid groupClassId)
		{
			if (groupClassId.Equals(Guid.Empty))
			{
				throw new ArgumentException("groupClassId should not be empty");
			}
			return new NavigationNodeGroup(groupSubject, groupSection, groupClassId);
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x00055A87 File Offset: 0x00053C87
		private static NavigationNodeGroup CreateFavoritesGroup()
		{
			return new NavigationNodeGroup(NavigationNodeCollection.GetDefaultGroupSubject(NavigationNodeGroupType.MyFoldersGroup, NavigationNodeGroupSection.First), NavigationNodeGroupSection.First, Guid.NewGuid());
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x00055A9C File Offset: 0x00053C9C
		private static object[][] LoadCommonViewAssociatedMessages(MailboxSession session)
		{
			object[][] result;
			using (Folder folder = Folder.Bind(session, DefaultFolderType.CommonViews))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.Associated, null, NavigationNodeCollection.SortByWhenQuerying, NavigationNodeCollection.nativeStorePropertyDefinitions.ToArray<NativeStorePropertyDefinition>()))
				{
					result = Utilities.FetchRowsFromQueryResult(queryResult, 10000);
				}
			}
			return result;
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00055B0C File Offset: 0x00053D0C
		private static bool IsDefaultGroupType(NavigationNodeGroupType groupType)
		{
			return groupType == NavigationNodeGroupType.MyFoldersGroup || groupType == NavigationNodeGroupType.OtherFoldersGroup || groupType == NavigationNodeGroupType.SharedFoldersGroup;
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00055B1C File Offset: 0x00053D1C
		private static void ThrowIfGroupSectionNotMatchFolderClass(NavigationNodeGroupSection groupSection, string folderClass)
		{
			switch (groupSection)
			{
			case NavigationNodeGroupSection.First:
				if (string.IsNullOrEmpty(folderClass) || ObjectClass.IsOfClass(folderClass, "IPF.Note"))
				{
					return;
				}
				break;
			case NavigationNodeGroupSection.Calendar:
				if (ObjectClass.IsOfClass(folderClass, "IPF.Appointment"))
				{
					return;
				}
				break;
			case NavigationNodeGroupSection.Contacts:
				if (ObjectClass.IsOfClass(folderClass, "IPF.Contact"))
				{
					return;
				}
				break;
			case NavigationNodeGroupSection.Tasks:
				if (ObjectClass.IsOfClass(folderClass, "IPF.Task"))
				{
					return;
				}
				break;
			}
			throw new InvalidOperationException(string.Format("The folder container class \"{0}\" doesn't match the group section \"{1}\".", folderClass, groupSection.ToString()));
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x00055BA4 File Offset: 0x00053DA4
		private void Load(UserContext userContext, MailboxSession session, object[][] rows)
		{
			int num = 0;
			while (num < rows.Length && !NavigationNodeCollection.CheckItemClassAndSection(this.navigationNodeGroupSection, rows[num]))
			{
				num++;
			}
			if (this.IsFavorites)
			{
				base.Clear();
				base.Add(NavigationNodeCollection.CreateFavoritesGroup());
			}
			else
			{
				int num2 = num;
				while (num2 < rows.Length && NavigationNodeCollection.CheckItemClassAndSection(this.navigationNodeGroupSection, rows[num2]))
				{
					if (NavigationNodeCollection.CheckNodeTypes(rows[num2], new NavigationNodeType[]
					{
						NavigationNodeType.Header
					}))
					{
						this.UniquelyAddGroup(new NavigationNodeGroup(rows[num2], NavigationNodeCollection.PropertyMap));
					}
					num2++;
				}
			}
			Dictionary<Guid, int> dictionary = new Dictionary<Guid, int>(base.Count);
			for (int i = 0; i < base.Count; i++)
			{
				dictionary.Add(base[i].NavigationNodeGroupClassId, i);
			}
			while (num < rows.Length && NavigationNodeCollection.CheckItemClassAndSection(this.navigationNodeGroupSection, rows[num]))
			{
				if (NavigationNodeCollection.CheckNodeTypes(rows[num], new NavigationNodeType[]
				{
					NavigationNodeType.NormalFolder,
					NavigationNodeType.SharedFolder,
					NavigationNodeType.SmartFolder,
					NavigationNodeType.GSCalendar
				}))
				{
					bool includeArchive = this.GroupSection == NavigationNodeGroupSection.First;
					NavigationNodeFolder navigationNodeFolder = new NavigationNodeFolder(userContext, session, includeArchive, rows[num], NavigationNodeCollection.PropertyMap);
					if (navigationNodeFolder.IsValid)
					{
						int num3 = -1;
						if (this.IsFavorites)
						{
							num3 = 0;
						}
						else
						{
							Guid guid = navigationNodeFolder.NavigationNodeParentGroupClassId;
							if (!guid.Equals(Guid.Empty) && dictionary.ContainsKey(guid))
							{
								num3 = dictionary[guid];
							}
							else
							{
								NavigationNodeGroup node = null;
								if (!guid.Equals(Guid.Empty) && !string.IsNullOrEmpty(navigationNodeFolder.NavigationNodeGroupName))
								{
									node = NavigationNodeCollection.CreateCustomizedGroup(this.navigationNodeGroupSection, navigationNodeFolder.NavigationNodeGroupName, guid);
								}
								else
								{
									NavigationNodeGroupType navigationNodeGroupType = NavigationNodeGroupType.OtherFoldersGroup;
									switch (navigationNodeFolder.NavigationNodeType)
									{
									case NavigationNodeType.NormalFolder:
										navigationNodeGroupType = NavigationNodeGroupType.MyFoldersGroup;
										break;
									case NavigationNodeType.SharedFolder:
										navigationNodeGroupType = NavigationNodeGroupType.SharedFoldersGroup;
										break;
									}
									guid = NavigationNodeCollection.GetDefaultGroupClassId(navigationNodeGroupType);
									if (dictionary.ContainsKey(guid))
									{
										num3 = dictionary[guid];
									}
									else
									{
										node = NavigationNodeCollection.CreateDefaultGroup(navigationNodeGroupType, this.navigationNodeGroupSection);
									}
								}
								if (num3 < 0)
								{
									base.Add(node);
									num3 = base.Count - 1;
									dictionary[guid] = num3;
								}
							}
						}
						this.UniquelyAddFolder(navigationNodeFolder, num3);
					}
				}
				num++;
			}
			this.serverCollection = new NavigationNodeList<NavigationNodeGroup>();
			base.CopyToList(this.serverCollection);
			foreach (NavigationNodeGroup navigationNodeGroup in this)
			{
				foreach (NavigationNodeFolder navigationNodeFolder2 in navigationNodeGroup.Children)
				{
					if (navigationNodeFolder2.FolderId != null && !navigationNodeFolder2.IsFlagSet(NavigationNodeFlags.PublicFolderFavorite))
					{
						navigationNodeFolder2.FixLegacyDNRelatedFlag(session);
					}
				}
			}
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x00055E84 File Offset: 0x00054084
		public int FindGroupById(Guid groupId)
		{
			this.ThrowIfSaved();
			for (int i = 0; i < base.Count; i++)
			{
				if (groupId.Equals(base[i].NavigationNodeGroupClassId))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x00055EC0 File Offset: 0x000540C0
		private int GetDefaultGroup(NavigationNodeGroupType groupType)
		{
			int num = this.FindGroupById(NavigationNodeCollection.GetDefaultGroupClassId(groupType));
			if (num < 0)
			{
				NavigationNodeGroup node = NavigationNodeCollection.CreateDefaultGroup(groupType, this.navigationNodeGroupSection);
				num = 0;
				if (groupType == NavigationNodeGroupType.SharedFoldersGroup)
				{
					num = this.FindGroupById(NavigationNodeCollection.GetDefaultGroupClassId(NavigationNodeGroupType.MyFoldersGroup)) + 1;
				}
				else if (groupType == NavigationNodeGroupType.OtherFoldersGroup)
				{
					int val = this.FindGroupById(NavigationNodeCollection.GetDefaultGroupClassId(NavigationNodeGroupType.MyFoldersGroup));
					int val2 = this.FindGroupById(NavigationNodeCollection.GetDefaultGroupClassId(NavigationNodeGroupType.SharedFoldersGroup));
					num = Math.Max(val, val2) + 1;
				}
				base.Insert(num, node);
			}
			return num;
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00055F34 File Offset: 0x00054134
		private List<NavigationNode> BuildNodeList(IList<NavigationNodeGroup> collections, bool doSaveHeader)
		{
			List<NavigationNode> list = new List<NavigationNode>();
			foreach (NavigationNodeGroup navigationNodeGroup in collections)
			{
				if (doSaveHeader)
				{
					list.Add(navigationNodeGroup);
				}
				foreach (NavigationNodeFolder item in navigationNodeGroup.Children)
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00055FC8 File Offset: 0x000541C8
		public void Save(MailboxSession session)
		{
			this.ThrowIfSaved();
			bool doSaveHeader = !this.IsFavorites;
			List<NavigationNode> list = this.BuildNodeList(this.serverCollection, doSaveHeader);
			List<NavigationNode> list2 = this.BuildNodeList(this, doSaveHeader);
			List<NavigationNode> list3 = new List<NavigationNode>();
			Dictionary<VersionedId, NavigationNode> dictionary = new Dictionary<VersionedId, NavigationNode>();
			Dictionary<VersionedId, NavigationNode> dictionary2 = new Dictionary<VersionedId, NavigationNode>();
			Dictionary<VersionedId, NavigationNode> dictionary3 = new Dictionary<VersionedId, NavigationNode>();
			foreach (NavigationNode navigationNode in list2)
			{
				if (navigationNode.IsNew)
				{
					list3.Add(navigationNode);
				}
				else if (navigationNode.IsDirty)
				{
					dictionary.Add(navigationNode.NavigationNodeId, navigationNode);
				}
				else
				{
					dictionary2.Add(navigationNode.NavigationNodeId, navigationNode);
				}
			}
			foreach (NavigationNode navigationNode2 in list)
			{
				if (!navigationNode2.IsNew && !dictionary.ContainsKey(navigationNode2.NavigationNodeId) && !dictionary2.ContainsKey(navigationNode2.NavigationNodeId))
				{
					dictionary3.Add(navigationNode2.NavigationNodeId, navigationNode2);
				}
			}
			if (list3.Count > 0)
			{
				this.SaveNewNodes(list3, session);
			}
			if (dictionary.Count > 0)
			{
				this.SaveModifiedNodes(dictionary, session);
			}
			if (dictionary3.Count > 0 || (this.duplicateNodes != null && this.duplicateNodes.Count > 0))
			{
				this.DeleteNodes(dictionary3, session);
			}
			this.serverCollection = null;
			base.Clear();
			this.isSaved = true;
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x00056164 File Offset: 0x00054364
		public NavigationNode RemoveFolderOrGroupByNodeId(StoreObjectId nodeId)
		{
			this.ThrowIfSaved();
			NavigationNode navigationNode = base.RemoveChildByNodeId(nodeId);
			if (navigationNode != null)
			{
				return navigationNode;
			}
			foreach (NavigationNodeGroup navigationNodeGroup in this)
			{
				navigationNode = navigationNodeGroup.Children.RemoveChildByNodeId(nodeId);
				if (navigationNode != null)
				{
					return navigationNode;
				}
			}
			return null;
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x000561D0 File Offset: 0x000543D0
		public int RemoveFolderByLegacyDNandId(string mailboxLegacyDN, StoreObjectId folderId)
		{
			this.ThrowIfSaved();
			int num = 0;
			foreach (NavigationNodeGroup navigationNodeGroup in this)
			{
				num += navigationNodeGroup.RemoveFolderByLegacyDNandId(mailboxLegacyDN, folderId);
			}
			return num;
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00056228 File Offset: 0x00054428
		public NavigationNodeFolder[] FindFoldersById(StoreObjectId folderId)
		{
			this.ThrowIfSaved();
			List<NavigationNodeFolder> list = new List<NavigationNodeFolder>();
			foreach (NavigationNodeGroup navigationNodeGroup in this)
			{
				foreach (NavigationNodeFolder item in navigationNodeGroup.FindFoldersById(folderId))
				{
					list.Add(item);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x000562A4 File Offset: 0x000544A4
		public NavigationNodeFolder[] FindGSCalendarsByLegacyDN(string mailboxLegacyDN)
		{
			this.ThrowIfSaved();
			List<NavigationNodeFolder> list = new List<NavigationNodeFolder>();
			foreach (NavigationNodeGroup navigationNodeGroup in this)
			{
				NavigationNodeFolder[] array = navigationNodeGroup.FindGSCalendarsByLegacyDN(mailboxLegacyDN);
				if (array != null)
				{
					list.AddRange(array);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0005630C File Offset: 0x0005450C
		public NavigationNode FindNavigationNodeByNodeId(StoreObjectId nodeId)
		{
			this.ThrowIfSaved();
			int num = base.FindChildByNodeId(nodeId);
			if (num >= 0)
			{
				return base[num];
			}
			foreach (NavigationNodeGroup navigationNodeGroup in this)
			{
				int num2 = navigationNodeGroup.Children.FindChildByNodeId(nodeId);
				if (num2 >= 0)
				{
					return navigationNodeGroup.Children[num2];
				}
			}
			return null;
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00056390 File Offset: 0x00054590
		public bool TryFindGroupAndNodeIndexByNodeId(StoreObjectId nodeId, out int groupIndex, out int nodeIndex)
		{
			this.ThrowIfSaved();
			nodeIndex = -1;
			groupIndex = base.FindChildByNodeId(nodeId);
			if (groupIndex >= 0)
			{
				return true;
			}
			for (int i = 0; i < base.Count; i++)
			{
				int num = base[i].Children.FindChildByNodeId(nodeId);
				if (num >= 0)
				{
					groupIndex = i;
					nodeIndex = num;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x000563E8 File Offset: 0x000545E8
		private void SaveModifiedNodes(Dictionary<VersionedId, NavigationNode> nodes, MailboxSession session)
		{
			foreach (KeyValuePair<VersionedId, NavigationNode> keyValuePair in nodes)
			{
				keyValuePair.Value.Save(session);
			}
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0005643C File Offset: 0x0005463C
		private void SaveNewNodes(List<NavigationNode> nodes, MailboxSession session)
		{
			for (int i = 0; i < nodes.Count; i++)
			{
				nodes[i].Save(session);
			}
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00056468 File Offset: 0x00054668
		private void DeleteNodes(Dictionary<VersionedId, NavigationNode> nodes, MailboxSession session)
		{
			List<StoreId> list = new List<StoreId>(nodes.Count);
			List<StoreId> list2 = new List<StoreId>(nodes.Count);
			foreach (KeyValuePair<VersionedId, NavigationNode> keyValuePair in nodes)
			{
				if (keyValuePair.Value is NavigationNodeFolder)
				{
					list.Add(keyValuePair.Key.ObjectId);
				}
				else if (keyValuePair.Value is NavigationNodeGroup)
				{
					list2.Add(keyValuePair.Key.ObjectId);
				}
			}
			if (this.duplicateNodes != null && this.duplicateNodes.Count > 0)
			{
				foreach (KeyValuePair<VersionedId, NavigationNode> keyValuePair2 in this.duplicateNodes)
				{
					if (keyValuePair2.Value is NavigationNodeFolder)
					{
						list.Add(keyValuePair2.Key.ObjectId);
					}
					else if (keyValuePair2.Value is NavigationNodeGroup)
					{
						list2.Add(keyValuePair2.Key.ObjectId);
					}
				}
			}
			list.AddRange(list2);
			session.Delete(DeleteItemFlags.HardDelete, list.ToArray());
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x000565B8 File Offset: 0x000547B8
		private bool CheckLatestNodes(NavigationNode node1, NavigationNode node2)
		{
			return node1.LastModifiedTime.CompareTo(node2.LastModifiedTime) > 0;
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x000565DC File Offset: 0x000547DC
		private void AddDuplicateNode(NavigationNode duplicateNode)
		{
			if (this.duplicateNodes == null)
			{
				this.duplicateNodes = new Dictionary<VersionedId, NavigationNode>();
			}
			this.duplicateNodes.Add(duplicateNode.NavigationNodeId, duplicateNode);
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00056604 File Offset: 0x00054804
		private void UniquelyAddGroup(NavigationNodeGroup nodeToAdd)
		{
			int num = this.FindGroupById(nodeToAdd.NavigationNodeGroupClassId);
			bool flag = true;
			if (num >= 0)
			{
				flag = this.CheckLatestNodes(nodeToAdd, base[num]);
				this.AddDuplicateNode(flag ? base[num] : nodeToAdd);
				if (flag)
				{
					base.RemoveAt(num);
				}
			}
			if (flag)
			{
				base.Add(nodeToAdd);
			}
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0005665C File Offset: 0x0005485C
		private void UniquelyAddFolder(NavigationNodeFolder nodeToAdd, int currentGroupIndex)
		{
			bool flag = true;
			if (nodeToAdd.IsValid)
			{
				int i = 0;
				while (i < base.Count)
				{
					int num = base[i].FindEquivalentNode(nodeToAdd);
					if (num >= 0)
					{
						NavigationNode navigationNode = base[i].Children[num];
						flag = this.CheckLatestNodes(nodeToAdd, navigationNode);
						this.AddDuplicateNode(flag ? navigationNode : nodeToAdd);
						if (flag)
						{
							base[i].Children.RemoveAt(num);
							break;
						}
						break;
					}
					else
					{
						i++;
					}
				}
			}
			if (flag)
			{
				base[currentGroupIndex].Children.Add(nodeToAdd);
			}
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x000566EB File Offset: 0x000548EB
		private void ThrowIfNonMailGroupSectionNotSupported()
		{
			if (this.navigationNodeGroupSection != NavigationNodeGroupSection.Calendar && this.navigationNodeGroupSection != NavigationNodeGroupSection.Contacts && this.navigationNodeGroupSection != NavigationNodeGroupSection.Tasks)
			{
				throw new NotSupportedException("Only calendar, contacts and tasks are supported.");
			}
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x00056713 File Offset: 0x00054913
		private void ThrowIfSaved()
		{
			if (this.isSaved)
			{
				throw new InvalidOperationException("The navigation node collection cannot be used after a save. Use a new navigation node collection.");
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x00056728 File Offset: 0x00054928
		private bool IsFavorites
		{
			get
			{
				return this.navigationNodeGroupSection == NavigationNodeGroupSection.First;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x00056733 File Offset: 0x00054933
		internal NavigationNodeGroupSection GroupSection
		{
			get
			{
				return this.navigationNodeGroupSection;
			}
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0005673C File Offset: 0x0005493C
		private bool CheckIfDataCorrected()
		{
			foreach (NavigationNodeGroup navigationNodeGroup in this)
			{
				if (!this.IsFavorites && (navigationNodeGroup.IsDirty || navigationNodeGroup.IsNew))
				{
					return true;
				}
				foreach (NavigationNodeFolder navigationNodeFolder in navigationNodeGroup.Children)
				{
					if (navigationNodeFolder.IsDirty || navigationNodeFolder.IsNew)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x000567EC File Offset: 0x000549EC
		public NavigationNodeFolder AddFolderToDefaultGroup(UserContext userContext, NavigationNodeGroupType groupType, Folder folder, bool isSharedFolder)
		{
			this.ThrowIfSaved();
			if (!NavigationNodeCollection.IsDefaultGroupType(groupType))
			{
				throw new ArgumentOutOfRangeException("Invalid default group type:" + groupType.ToString());
			}
			this.ThrowIfNonMailGroupSectionNotSupported();
			NavigationNodeCollection.ThrowIfGroupSectionNotMatchFolderClass(this.navigationNodeGroupSection, folder.ClassName);
			MailboxSession mailboxSession = folder.Session as MailboxSession;
			if (mailboxSession == null)
			{
				throw new ArgumentException("Should not add a folder that doesn't belong to a mailbox session.");
			}
			string text;
			if (isSharedFolder)
			{
				text = Utilities.GetMailboxOwnerDisplayName(mailboxSession);
				if (!Utilities.IsSpecialFolderForSession(mailboxSession, folder.Id.ObjectId))
				{
					text = string.Format(LocalizedStrings.GetNonEncoded(-83764036), text, folder.DisplayName);
				}
			}
			else
			{
				text = folder.DisplayName;
			}
			return this.AddFolderToGroup(this.GetDefaultGroup(groupType), folder, userContext, text, isSharedFolder ? NavigationNodeType.SharedFolder : NavigationNodeType.NormalFolder);
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x000568AC File Offset: 0x00054AAC
		internal NavigationNodeFolder InsertToDoFolderToGroup(UserContext userContext)
		{
			this.ThrowIfSaved();
			if (this.GroupSection != NavigationNodeGroupSection.Tasks)
			{
				throw new InvalidOperationException("Only task module can accept todo folder.");
			}
			NavigationNodeFolder result;
			using (Folder folder = Utilities.GetFolder<Folder>(userContext, OwaStoreObjectId.CreateFromMailboxFolderId(userContext.FlaggedItemsAndTasksFolderId), new PropertyDefinition[]
			{
				StoreObjectSchema.RecordKey
			}))
			{
				result = this.AddFolderToGroup(this.GetDefaultGroup(NavigationNodeGroupType.MyFoldersGroup), 0, folder, userContext, folder.DisplayName, NavigationNodeType.NormalFolder);
			}
			return result;
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0005692C File Offset: 0x00054B2C
		internal NavigationNodeFolder AddMyFolderToGroup(UserContext userContext, MailboxSession mailboxSession, object[] folderPropertyValues, Dictionary<PropertyDefinition, int> folderPropertyMap)
		{
			this.ThrowIfSaved();
			this.ThrowIfNonMailGroupSectionNotSupported();
			Utilities.CheckAndThrowForRequiredProperty(folderPropertyMap, new PropertyDefinition[]
			{
				FolderSchema.Id,
				FolderSchema.DisplayName,
				StoreObjectSchema.ContainerClass,
				FolderSchema.ExtendedFolderFlags
			});
			NavigationNodeCollection.ThrowIfGroupSectionNotMatchFolderClass(this.navigationNodeGroupSection, folderPropertyValues[folderPropertyMap[StoreObjectSchema.ContainerClass]] as string);
			bool flag = Utilities.IsOneOfTheFolderFlagsSet(folderPropertyValues[folderPropertyMap[FolderSchema.ExtendedFolderFlags]], new ExtendedFolderFlags[]
			{
				ExtendedFolderFlags.SharedIn,
				ExtendedFolderFlags.ExchangeCrossOrgShareFolder
			});
			bool flag2 = Utilities.IsOneOfTheFolderFlagsSet(folderPropertyValues[folderPropertyMap[FolderSchema.ExtendedFolderFlags]], new ExtendedFolderFlags[]
			{
				ExtendedFolderFlags.WebCalFolder
			});
			NavigationNodeGroupType groupType = NavigationNodeGroupType.MyFoldersGroup;
			if (flag2)
			{
				groupType = NavigationNodeGroupType.OtherFoldersGroup;
			}
			else if (flag)
			{
				groupType = NavigationNodeGroupType.SharedFoldersGroup;
			}
			int defaultGroup = this.GetDefaultGroup(groupType);
			NavigationNodeFolder navigationNodeFolder = new NavigationNodeFolder(mailboxSession, userContext.IsMyMailbox(mailboxSession), folderPropertyValues, folderPropertyMap, (folderPropertyValues[folderPropertyMap[FolderSchema.DisplayName]] as string) ?? string.Empty, base[defaultGroup].NavigationNodeGroupClassId, this.navigationNodeGroupSection, base[defaultGroup].Subject);
			base[defaultGroup].Children.Add(navigationNodeFolder);
			return navigationNodeFolder;
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00056A68 File Offset: 0x00054C68
		public NavigationNodeFolder InsertMyFolderToFavorites(Folder folder, int index)
		{
			this.ThrowIfSaved();
			if (!this.IsFavorites)
			{
				throw new InvalidOperationException("This collection doesn't represent favorites.");
			}
			NavigationNodeCollection.ThrowIfGroupSectionNotMatchFolderClass(this.navigationNodeGroupSection, folder.ClassName);
			if (base.Count == 0)
			{
				base.Add(NavigationNodeCollection.CreateFavoritesGroup());
			}
			StoreObjectType objectType = folder.Id.ObjectId.ObjectType;
			NavigationNodeType nodeType = (objectType == StoreObjectType.OutlookSearchFolder || objectType == StoreObjectType.SearchFolder) ? NavigationNodeType.SmartFolder : NavigationNodeType.NormalFolder;
			return this.AddFolderToGroup(0, index, folder, !Utilities.IsInArchiveMailbox(folder), folder.DisplayName, nodeType);
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00056AE9 File Offset: 0x00054CE9
		public NavigationNodeFolder AppendFolderToFavorites(Folder folder)
		{
			return this.InsertMyFolderToFavorites(folder, (base.Count == 1) ? base[0].Children.Count : 0);
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00056B0F File Offset: 0x00054D0F
		public NavigationNodeFolder AddFolderToGroup(int groupIndex, Folder folder, UserContext userContext, string subject, NavigationNodeType nodeType)
		{
			return this.AddFolderToGroup(groupIndex, base[groupIndex].Children.Count, folder, userContext.IsMyMailbox(folder.Session), subject, nodeType);
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00056B3A File Offset: 0x00054D3A
		private NavigationNodeFolder AddFolderToGroup(int groupIndex, int nodeIndex, Folder folder, UserContext userContext, string subject, NavigationNodeType nodeType)
		{
			return this.AddFolderToGroup(groupIndex, nodeIndex, folder, userContext.IsMyMailbox(folder.Session), subject, nodeType);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00056B58 File Offset: 0x00054D58
		private NavigationNodeFolder AddFolderToGroup(int groupIndex, int nodeIndex, Folder folder, bool isMyFolder, string subject, NavigationNodeType nodeType)
		{
			NavigationNodeFolder navigationNodeFolder = new NavigationNodeFolder(folder, isMyFolder, subject, nodeType, base[groupIndex].NavigationNodeGroupClassId, this.navigationNodeGroupSection, base[groupIndex].Subject);
			NavigationNodeGroup navigationNodeGroup = base[groupIndex];
			navigationNodeGroup.Children.Insert(nodeIndex, navigationNodeFolder);
			return navigationNodeFolder;
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00056BA8 File Offset: 0x00054DA8
		internal static void AddNonMailFolderToSharedFoldersGroup(UserContext userContext, Folder folder, NavigationNodeGroupSection groupSection)
		{
			if (groupSection == NavigationNodeGroupSection.Mail)
			{
				throw new ArgumentOutOfRangeException("Cannot add mail folder to shared folder group");
			}
			NavigationNodeCollection navigationNodeCollection = NavigationNodeCollection.TryCreateNavigationNodeCollection(userContext, userContext.MailboxSession, groupSection);
			if (navigationNodeCollection.FindFoldersById(folder.Id.ObjectId).Length == 0)
			{
				navigationNodeCollection.AddFolderToDefaultGroup(userContext, NavigationNodeGroupType.SharedFoldersGroup, folder, true);
				navigationNodeCollection.Save(userContext.MailboxSession);
			}
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x00056C00 File Offset: 0x00054E00
		internal static void AddGSCalendarToSharedFoldersGroup(UserContext userContext, ExchangePrincipal exchangePrincipal)
		{
			NavigationNodeCollection navigationNodeCollection = NavigationNodeCollection.TryCreateNavigationNodeCollection(userContext, userContext.MailboxSession, NavigationNodeGroupSection.Calendar);
			if (navigationNodeCollection.FindGSCalendarsByLegacyDN(exchangePrincipal.LegacyDn).Length == 0)
			{
				int defaultGroup = navigationNodeCollection.GetDefaultGroup(NavigationNodeGroupType.SharedFoldersGroup);
				NavigationNodeGroup navigationNodeGroup = navigationNodeCollection[defaultGroup];
				NavigationNodeFolder node = NavigationNodeFolder.CreateGSNode(exchangePrincipal, navigationNodeGroup.NavigationNodeGroupClassId, navigationNodeGroup.Subject, exchangePrincipal.MailboxInfo.DisplayName, navigationNodeGroup.NavigationNodeGroupSection);
				navigationNodeGroup.Children.Insert(navigationNodeGroup.Children.Count, node);
				navigationNodeCollection.Save(userContext.MailboxSession);
			}
		}

		// Token: 0x040008F2 RID: 2290
		internal static readonly Guid MyFoldersClassId = new Guid("{0006F0B7-0000-0000-C000-000000000046}");

		// Token: 0x040008F3 RID: 2291
		internal static readonly Guid OtherFoldersClassId = new Guid("{0006F0B8-0000-0000-C000-000000000046}");

		// Token: 0x040008F4 RID: 2292
		internal static readonly Guid PeoplesFoldersClassId = new Guid("{0006F0B9-0000-0000-C000-000000000046}");

		// Token: 0x040008F5 RID: 2293
		private static readonly SortBy[] SortByWhenQuerying = new SortBy[]
		{
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
			new SortBy(NavigationNodeSchema.GroupSection, SortOrder.Ascending),
			new SortBy(NavigationNodeSchema.Ordinal, SortOrder.Ascending)
		};

		// Token: 0x040008F6 RID: 2294
		private static readonly PropertyDefinition[] AllProperties = new PropertyDefinition[]
		{
			NavigationNodeSchema.Type,
			NavigationNodeSchema.GroupClassId,
			NavigationNodeSchema.ParentGroupClassId,
			StoreObjectSchema.ItemClass,
			ItemSchema.Subject,
			StoreObjectSchema.EntryId,
			NavigationNodeSchema.OutlookTagId,
			NavigationNodeSchema.Flags,
			NavigationNodeSchema.Ordinal,
			NavigationNodeSchema.NodeEntryId,
			NavigationNodeSchema.NodeRecordKey,
			NavigationNodeSchema.StoreEntryId,
			NavigationNodeSchema.ClassId,
			NavigationNodeSchema.GroupName,
			NavigationNodeSchema.GroupSection,
			NavigationNodeSchema.AddressBookEntryId,
			NavigationNodeSchema.AddressBookStoreEntryId,
			NavigationNodeSchema.CalendarTypeFromOlderExchange,
			FolderSchema.AssociatedSearchFolderId,
			ItemSchema.Categories,
			ViewStateProperties.TreeNodeCollapseStatus,
			ViewStateProperties.FilteredViewFrom,
			ViewStateProperties.FilteredViewTo,
			ViewStateProperties.FilteredViewFlags,
			ViewStateProperties.FilterSourceFolder,
			ItemSchema.Id,
			StoreObjectSchema.LastModifiedTime,
			NavigationNodeSchema.CalendarColor
		};

		// Token: 0x040008F7 RID: 2295
		private static readonly Dictionary<PropertyDefinition, int> PropertyMap = NavigationNodeCollection.MapProperties();

		// Token: 0x040008F8 RID: 2296
		private readonly NavigationNodeGroupSection navigationNodeGroupSection;

		// Token: 0x040008F9 RID: 2297
		private static ICollection<NativeStorePropertyDefinition> nativeStorePropertyDefinitions;

		// Token: 0x040008FA RID: 2298
		private static Dictionary<NavigationNodeGroupSection, Strings.IDs[]> localizedGroupNames = NavigationNodeCollection.MapLocalizedGroupNames();

		// Token: 0x040008FB RID: 2299
		private NavigationNodeList<NavigationNodeGroup> serverCollection;

		// Token: 0x040008FC RID: 2300
		private bool isSaved;

		// Token: 0x040008FD RID: 2301
		private Dictionary<VersionedId, NavigationNode> duplicateNodes;
	}
}
