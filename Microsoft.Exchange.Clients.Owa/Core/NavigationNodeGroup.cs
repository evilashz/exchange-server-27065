using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200016E RID: 366
	internal sealed class NavigationNodeGroup : NavigationNode, ICloneable
	{
		// Token: 0x06000CF2 RID: 3314 RVA: 0x000580F7 File Offset: 0x000562F7
		public NavigationNodeGroup(string subject, NavigationNodeGroupSection navigationNodeGroupSection, Guid navigationNodeGroupClassId) : base(NavigationNodeType.Header, subject, navigationNodeGroupSection)
		{
			this.NavigationNodeGroupClassId = navigationNodeGroupClassId;
			this.children = new NavigationNodeGroup.NavigationNodeFolderList(this);
			base.ClearDirty();
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0005811B File Offset: 0x0005631B
		internal NavigationNodeGroup(object[] values, Dictionary<PropertyDefinition, int> propertyMap) : base(NavigationNodeGroup.groupProperties, values, propertyMap)
		{
			this.children = new NavigationNodeGroup.NavigationNodeFolderList(this);
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x00058136 File Offset: 0x00056336
		private NavigationNodeGroup(MemoryPropertyBag propertyBag) : base(propertyBag)
		{
			this.children = new NavigationNodeGroup.NavigationNodeFolderList(this);
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x0005814B File Offset: 0x0005634B
		// (set) Token: 0x06000CF6 RID: 3318 RVA: 0x00058158 File Offset: 0x00056358
		internal Guid NavigationNodeGroupClassId
		{
			get
			{
				return base.GuidGetter(NavigationNodeSchema.GroupClassId);
			}
			private set
			{
				base.GuidSetter(NavigationNodeSchema.GroupClassId, value);
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x00058166 File Offset: 0x00056366
		internal NavigationNodeList<NavigationNodeFolder> Children
		{
			get
			{
				return this.children;
			}
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00058170 File Offset: 0x00056370
		public object Clone()
		{
			NavigationNodeGroup navigationNodeGroup = new NavigationNodeGroup(this.propertyBag);
			this.children.CopyToList(navigationNodeGroup.children);
			if (base.IsNew)
			{
				navigationNodeGroup.IsNew = true;
			}
			return navigationNodeGroup;
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x000581AC File Offset: 0x000563AC
		public override int GetHashCode()
		{
			return this.NavigationNodeGroupClassId.GetHashCode();
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x000581D0 File Offset: 0x000563D0
		public override bool Equals(object obj)
		{
			NavigationNodeGroup navigationNodeGroup = obj as NavigationNodeGroup;
			return navigationNodeGroup != null && this.NavigationNodeGroupClassId.Equals(navigationNodeGroup.NavigationNodeGroupClassId);
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00058200 File Offset: 0x00056400
		public int RemoveFolderByLegacyDNandId(string mailboxLegacyDN, StoreObjectId folderId)
		{
			int num = 0;
			for (int i = this.children.Count - 1; i >= 0; i--)
			{
				if (StringComparer.OrdinalIgnoreCase.Equals(mailboxLegacyDN, this.children[i].MailboxLegacyDN) && folderId.CompareTo(this.children[i].FolderId) == 0)
				{
					this.children.RemoveAt(i);
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00058270 File Offset: 0x00056470
		public int FindEquivalentNode(NavigationNodeFolder nodeFolder)
		{
			for (int i = 0; i < this.children.Count; i++)
			{
				if (nodeFolder.Equals(this.children[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x000582AC File Offset: 0x000564AC
		public NavigationNodeFolder[] FindFoldersById(StoreObjectId folderId)
		{
			List<NavigationNodeFolder> list = new List<NavigationNodeFolder>();
			foreach (NavigationNodeFolder navigationNodeFolder in this.children)
			{
				if (navigationNodeFolder.FolderId != null && folderId.CompareTo(navigationNodeFolder.FolderId) == 0)
				{
					list.Add(navigationNodeFolder);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0005831C File Offset: 0x0005651C
		public NavigationNodeFolder[] FindGSCalendarsByLegacyDN(string mailboxLegacyDN)
		{
			List<NavigationNodeFolder> list = new List<NavigationNodeFolder>();
			foreach (NavigationNodeFolder navigationNodeFolder in this.children)
			{
				if (string.Equals(mailboxLegacyDN, navigationNodeFolder.MailboxLegacyDN, StringComparison.OrdinalIgnoreCase) && navigationNodeFolder.IsGSCalendar)
				{
					list.Add(navigationNodeFolder);
				}
			}
			return list.ToArray();
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x0005838C File Offset: 0x0005658C
		// (set) Token: 0x06000D00 RID: 3328 RVA: 0x000583A7 File Offset: 0x000565A7
		public bool IsExpanded
		{
			get
			{
				return !Utilities.IsFlagSet(this.propertyBag.GetValueOrDefault<int>(ViewStateProperties.TreeNodeCollapseStatus), 1);
			}
			set
			{
				this.propertyBag.SetProperty(ViewStateProperties.TreeNodeCollapseStatus, value ? StatusPersistTreeNodeType.None : StatusPersistTreeNodeType.CurrentNode);
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x000583C8 File Offset: 0x000565C8
		// (set) Token: 0x06000D02 RID: 3330 RVA: 0x0005844B File Offset: 0x0005664B
		public override string Subject
		{
			get
			{
				if (base.IsFlagSet(NavigationNodeFlags.OneOffName))
				{
					return base.Subject;
				}
				NavigationNodeGroupType navigationNodeGroupType = NavigationNodeGroupType.UserCreatedGroup;
				if (NavigationNodeCollection.MyFoldersClassId.Equals(this.NavigationNodeGroupClassId))
				{
					navigationNodeGroupType = NavigationNodeGroupType.MyFoldersGroup;
				}
				else if (NavigationNodeCollection.OtherFoldersClassId.Equals(this.NavigationNodeGroupClassId))
				{
					navigationNodeGroupType = NavigationNodeGroupType.OtherFoldersGroup;
				}
				else if (NavigationNodeCollection.PeoplesFoldersClassId.Equals(this.NavigationNodeGroupClassId))
				{
					navigationNodeGroupType = NavigationNodeGroupType.SharedFoldersGroup;
				}
				if (navigationNodeGroupType == NavigationNodeGroupType.UserCreatedGroup)
				{
					return base.Subject;
				}
				return NavigationNodeCollection.GetDefaultGroupSubject(navigationNodeGroupType, base.NavigationNodeGroupSection);
			}
			set
			{
				base.NavigationNodeFlags |= NavigationNodeFlags.OneOffName;
				base.Subject = value;
			}
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00058468 File Offset: 0x00056668
		protected override void UpdateMessage(MessageItem message)
		{
			base.UpdateMessage(message);
			message[NavigationNodeSchema.GroupClassId] = this.NavigationNodeGroupClassId.ToByteArray();
			message[ViewStateProperties.TreeNodeCollapseStatus] = (this.IsExpanded ? 0 : 1);
		}

		// Token: 0x0400090C RID: 2316
		private readonly NavigationNodeGroup.NavigationNodeFolderList children;

		// Token: 0x0400090D RID: 2317
		private static PropertyDefinition[] groupProperties = new PropertyDefinition[]
		{
			ViewStateProperties.TreeNodeCollapseStatus,
			NavigationNodeSchema.GroupClassId
		};

		// Token: 0x0200016F RID: 367
		private class NavigationNodeFolderList : NavigationNodeList<NavigationNodeFolder>
		{
			// Token: 0x06000D05 RID: 3333 RVA: 0x000584DE File Offset: 0x000566DE
			public NavigationNodeFolderList(NavigationNodeGroup parentGroup)
			{
				this.parentGroup = parentGroup;
			}

			// Token: 0x06000D06 RID: 3334 RVA: 0x000584ED File Offset: 0x000566ED
			protected override void OnBeforeNodeAdd(NavigationNodeFolder node)
			{
				this.MakeNavigationNodeFolderAsChild(node);
				base.OnBeforeNodeAdd(node);
			}

			// Token: 0x06000D07 RID: 3335 RVA: 0x00058500 File Offset: 0x00056700
			private void MakeNavigationNodeFolderAsChild(NavigationNodeFolder node)
			{
				if (this.parentGroup.NavigationNodeGroupSection != node.NavigationNodeGroupSection)
				{
					throw new ArgumentException(string.Format("Current node list is for group section {0}, but the new node is in group section {1}, which does not matched.", this.parentGroup.NavigationNodeGroupSection.ToString(), node.NavigationNodeGroupSection.ToString()));
				}
				if (node.NavigationNodeGroupSection != NavigationNodeGroupSection.First && !node.NavigationNodeParentGroupClassId.Equals(this.parentGroup.NavigationNodeGroupClassId))
				{
					node.NavigationNodeParentGroupClassId = this.parentGroup.NavigationNodeGroupClassId;
					node.NavigationNodeGroupName = this.parentGroup.Subject;
				}
			}

			// Token: 0x0400090E RID: 2318
			private readonly NavigationNodeGroup parentGroup;
		}
	}
}
