using System;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x02000021 RID: 33
	public class ListItemWrapper : ClientObjectWrapper<ListItem>, IListItem, IClientObject<ListItem>
	{
		// Token: 0x1700003E RID: 62
		public object this[string fieldName]
		{
			get
			{
				return this.backingItem[fieldName];
			}
			set
			{
				this.backingItem[fieldName] = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003053 File Offset: 0x00001253
		public int Id
		{
			get
			{
				return this.backingItem.Id;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003060 File Offset: 0x00001260
		public string IdAsString
		{
			get
			{
				return this.backingItem.Id.ToString();
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003080 File Offset: 0x00001280
		public IFile File
		{
			get
			{
				IFile result;
				if ((result = this.file) == null)
				{
					result = (this.file = new FileWrapper(this.backingItem.File));
				}
				return result;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000030B0 File Offset: 0x000012B0
		public IFolder Folder
		{
			get
			{
				IFolder result;
				if ((result = this.folder) == null)
				{
					result = (this.folder = new FolderWrapper(this.backingItem.Folder));
				}
				return result;
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000030E0 File Offset: 0x000012E0
		public void BreakRoleInheritance(bool copyRoleAssignments, bool clearSubscopes)
		{
			this.backingItem.BreakRoleInheritance(copyRoleAssignments, clearSubscopes);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000030EF File Offset: 0x000012EF
		public ListItemWrapper(ListItem item) : base(item)
		{
			this.backingItem = item;
		}

		// Token: 0x0400003B RID: 59
		private ListItem backingItem;

		// Token: 0x0400003C RID: 60
		private IFile file;

		// Token: 0x0400003D RID: 61
		private IFolder folder;
	}
}
