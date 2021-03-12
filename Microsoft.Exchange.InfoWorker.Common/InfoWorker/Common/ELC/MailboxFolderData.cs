using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.InfoWorker.Common.ELC
{
	// Token: 0x020001AC RID: 428
	internal class MailboxFolderData
	{
		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x00030C6D File Offset: 0x0002EE6D
		// (set) Token: 0x06000B41 RID: 2881 RVA: 0x00030C75 File Offset: 0x0002EE75
		internal VersionedId Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x00030C7E File Offset: 0x0002EE7E
		// (set) Token: 0x06000B43 RID: 2883 RVA: 0x00030C86 File Offset: 0x0002EE86
		internal StoreObjectId ParentId
		{
			get
			{
				return this.parentId;
			}
			set
			{
				this.parentId = value;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x00030C8F File Offset: 0x0002EE8F
		// (set) Token: 0x06000B45 RID: 2885 RVA: 0x00030C97 File Offset: 0x0002EE97
		internal string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x00030CA0 File Offset: 0x0002EEA0
		// (set) Token: 0x06000B47 RID: 2887 RVA: 0x00030CA8 File Offset: 0x0002EEA8
		internal Guid ElcFolderGuid
		{
			get
			{
				return this.adGuid;
			}
			set
			{
				this.adGuid = value;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x00030CB1 File Offset: 0x0002EEB1
		// (set) Token: 0x06000B49 RID: 2889 RVA: 0x00030CB9 File Offset: 0x0002EEB9
		internal string Comment
		{
			get
			{
				return this.comment;
			}
			set
			{
				this.comment = value;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x00030CC2 File Offset: 0x0002EEC2
		// (set) Token: 0x06000B4B RID: 2891 RVA: 0x00030CCA File Offset: 0x0002EECA
		internal ELCFolderFlags Flags
		{
			get
			{
				return this.elcFlags;
			}
			set
			{
				this.elcFlags = value;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x00030CD3 File Offset: 0x0002EED3
		// (set) Token: 0x06000B4D RID: 2893 RVA: 0x00030CDB File Offset: 0x0002EEDB
		internal int FolderQuota
		{
			get
			{
				return this.folderQuota;
			}
			set
			{
				this.folderQuota = value;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x00030CE4 File Offset: 0x0002EEE4
		// (set) Token: 0x06000B4F RID: 2895 RVA: 0x00030CEC File Offset: 0x0002EEEC
		internal string Url
		{
			get
			{
				return this.url;
			}
			set
			{
				this.url = value;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x00030CF5 File Offset: 0x0002EEF5
		// (set) Token: 0x06000B51 RID: 2897 RVA: 0x00030CFD File Offset: 0x0002EEFD
		internal string LocalizedName
		{
			get
			{
				return this.localizedName;
			}
			set
			{
				this.localizedName = value;
			}
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00030D06 File Offset: 0x0002EF06
		internal bool IsOrganizationalFolder()
		{
			return (this.elcFlags & ELCFolderFlags.Provisioned) == ELCFolderFlags.Provisioned;
		}

		// Token: 0x0400086E RID: 2158
		private VersionedId id;

		// Token: 0x0400086F RID: 2159
		private StoreObjectId parentId;

		// Token: 0x04000870 RID: 2160
		private string name;

		// Token: 0x04000871 RID: 2161
		private Guid adGuid;

		// Token: 0x04000872 RID: 2162
		private string comment;

		// Token: 0x04000873 RID: 2163
		private ELCFolderFlags elcFlags;

		// Token: 0x04000874 RID: 2164
		private int folderQuota;

		// Token: 0x04000875 RID: 2165
		private string url;

		// Token: 0x04000876 RID: 2166
		private string localizedName;
	}
}
