using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200060B RID: 1547
	[DataContract]
	public class MailboxFolder : NodeInfo, IComparable
	{
		// Token: 0x170026A4 RID: 9892
		// (get) Token: 0x06004505 RID: 17669 RVA: 0x000D0C41 File Offset: 0x000CEE41
		// (set) Token: 0x06004506 RID: 17670 RVA: 0x000D0C49 File Offset: 0x000CEE49
		[DataMember]
		public List<MailboxFolder> Children
		{
			get
			{
				return this.children;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06004507 RID: 17671 RVA: 0x000D0C50 File Offset: 0x000CEE50
		public MailboxFolder(MailboxFolder folder)
		{
			this.Folder = folder;
			base.ID = folder.FolderStoreObjectId;
			base.Name = folder.Name;
			this.IsInboxFolder = (this.Folder.DefaultFolderType == DefaultFolderType.Inbox);
			base.CanNewSubNode = (this.Folder.DefaultFolderType != DefaultFolderType.ElcRoot && this.Folder.ExtendedFolderFlags != ExtendedFolderFlags.ExclusivelyBound && this.Folder.ExtendedFolderFlags != ExtendedFolderFlags.RemoteHierarchy);
		}

		// Token: 0x170026A5 RID: 9893
		// (get) Token: 0x06004508 RID: 17672 RVA: 0x000D0D32 File Offset: 0x000CEF32
		// (set) Token: 0x06004509 RID: 17673 RVA: 0x000D0D3A File Offset: 0x000CEF3A
		[DataMember]
		public bool IsInboxFolder { get; internal set; }

		// Token: 0x170026A6 RID: 9894
		// (get) Token: 0x0600450A RID: 17674 RVA: 0x000D0D43 File Offset: 0x000CEF43
		// (set) Token: 0x0600450B RID: 17675 RVA: 0x000D0D4B File Offset: 0x000CEF4B
		internal MailboxFolder Folder { get; private set; }

		// Token: 0x0600450C RID: 17676 RVA: 0x000D0D54 File Offset: 0x000CEF54
		public int CompareTo(object obj)
		{
			MailboxFolder mailboxFolder = obj as MailboxFolder;
			int num = 5;
			int num2 = 5;
			if (MailboxFolder.FoldersToShowFirst.ContainsKey(this.Folder.DefaultFolderType))
			{
				MailboxFolder.FoldersToShowFirst.TryGetValue(this.Folder.DefaultFolderType, out num);
			}
			if (MailboxFolder.FoldersToShowFirst.ContainsKey(mailboxFolder.Folder.DefaultFolderType))
			{
				MailboxFolder.FoldersToShowFirst.TryGetValue(mailboxFolder.Folder.DefaultFolderType, out num2);
			}
			if (num == num2)
			{
				return this.Folder.Name.CompareTo(mailboxFolder.Folder.Name);
			}
			if (num < num2)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x04002E50 RID: 11856
		private List<MailboxFolder> children = new List<MailboxFolder>();

		// Token: 0x04002E51 RID: 11857
		private static readonly Dictionary<DefaultFolderType?, int> FoldersToShowFirst = new Dictionary<DefaultFolderType?, int>
		{
			{
				new DefaultFolderType?(DefaultFolderType.Inbox),
				1
			},
			{
				new DefaultFolderType?(DefaultFolderType.Drafts),
				2
			},
			{
				new DefaultFolderType?(DefaultFolderType.SentItems),
				3
			},
			{
				new DefaultFolderType?(DefaultFolderType.DeletedItems),
				4
			}
		};
	}
}
