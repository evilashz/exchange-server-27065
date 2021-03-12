using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E54 RID: 3668
	internal class Folder : Entity
	{
		// Token: 0x17001570 RID: 5488
		// (get) Token: 0x06005E5D RID: 24157 RVA: 0x00126467 File Offset: 0x00124667
		// (set) Token: 0x06005E5E RID: 24158 RVA: 0x00126479 File Offset: 0x00124679
		public string ParentFolderId
		{
			get
			{
				return (string)base[FolderSchema.ParentFolderId];
			}
			set
			{
				base[FolderSchema.ParentFolderId] = value;
			}
		}

		// Token: 0x17001571 RID: 5489
		// (get) Token: 0x06005E5F RID: 24159 RVA: 0x00126487 File Offset: 0x00124687
		// (set) Token: 0x06005E60 RID: 24160 RVA: 0x00126499 File Offset: 0x00124699
		public string DisplayName
		{
			get
			{
				return (string)base[FolderSchema.DisplayName];
			}
			set
			{
				base[FolderSchema.DisplayName] = value;
			}
		}

		// Token: 0x17001572 RID: 5490
		// (get) Token: 0x06005E61 RID: 24161 RVA: 0x001264A7 File Offset: 0x001246A7
		// (set) Token: 0x06005E62 RID: 24162 RVA: 0x001264B9 File Offset: 0x001246B9
		public string ClassName
		{
			get
			{
				return (string)base[FolderSchema.ClassName];
			}
			set
			{
				base[FolderSchema.ClassName] = value;
			}
		}

		// Token: 0x17001573 RID: 5491
		// (get) Token: 0x06005E63 RID: 24163 RVA: 0x001264C7 File Offset: 0x001246C7
		// (set) Token: 0x06005E64 RID: 24164 RVA: 0x001264D9 File Offset: 0x001246D9
		public int TotalCount
		{
			get
			{
				return (int)base[FolderSchema.TotalCount];
			}
			set
			{
				base[FolderSchema.TotalCount] = value;
			}
		}

		// Token: 0x17001574 RID: 5492
		// (get) Token: 0x06005E65 RID: 24165 RVA: 0x001264EC File Offset: 0x001246EC
		// (set) Token: 0x06005E66 RID: 24166 RVA: 0x001264FE File Offset: 0x001246FE
		public int ChildFolderCount
		{
			get
			{
				return (int)base[FolderSchema.ChildFolderCount];
			}
			set
			{
				base[FolderSchema.ChildFolderCount] = value;
			}
		}

		// Token: 0x17001575 RID: 5493
		// (get) Token: 0x06005E67 RID: 24167 RVA: 0x00126511 File Offset: 0x00124711
		// (set) Token: 0x06005E68 RID: 24168 RVA: 0x00126523 File Offset: 0x00124723
		public int UnreadItemCount
		{
			get
			{
				return (int)base[FolderSchema.UnreadItemCount];
			}
			set
			{
				base[FolderSchema.UnreadItemCount] = value;
			}
		}

		// Token: 0x17001576 RID: 5494
		// (get) Token: 0x06005E69 RID: 24169 RVA: 0x00126536 File Offset: 0x00124736
		// (set) Token: 0x06005E6A RID: 24170 RVA: 0x00126548 File Offset: 0x00124748
		public IEnumerable<Folder> ChildFolders
		{
			get
			{
				return (IEnumerable<Folder>)base[FolderSchema.ChildFolders];
			}
			set
			{
				base[FolderSchema.ChildFolders] = value;
			}
		}

		// Token: 0x17001577 RID: 5495
		// (get) Token: 0x06005E6B RID: 24171 RVA: 0x00126556 File Offset: 0x00124756
		// (set) Token: 0x06005E6C RID: 24172 RVA: 0x00126568 File Offset: 0x00124768
		public IEnumerable<Message> Messages
		{
			get
			{
				return (IEnumerable<Message>)base[FolderSchema.Messages];
			}
			set
			{
				base[FolderSchema.Messages] = value;
			}
		}

		// Token: 0x17001578 RID: 5496
		// (get) Token: 0x06005E6D RID: 24173 RVA: 0x00126576 File Offset: 0x00124776
		internal override EntitySchema Schema
		{
			get
			{
				return FolderSchema.SchemaInstance;
			}
		}

		// Token: 0x04003317 RID: 13079
		internal new static readonly EdmEntityType EdmEntityType = new EdmEntityType(typeof(Folder).Namespace, typeof(Folder).Name, Entity.EdmEntityType);
	}
}
