using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E62 RID: 3682
	internal class ContactFolder : Entity
	{
		// Token: 0x170015F1 RID: 5617
		// (get) Token: 0x06005F92 RID: 24466 RVA: 0x0012A6F7 File Offset: 0x001288F7
		// (set) Token: 0x06005F93 RID: 24467 RVA: 0x0012A709 File Offset: 0x00128909
		public string ParentFolderId
		{
			get
			{
				return (string)base[ContactFolderSchema.ParentFolderId];
			}
			set
			{
				base[ContactFolderSchema.ParentFolderId] = value;
			}
		}

		// Token: 0x170015F2 RID: 5618
		// (get) Token: 0x06005F94 RID: 24468 RVA: 0x0012A717 File Offset: 0x00128917
		// (set) Token: 0x06005F95 RID: 24469 RVA: 0x0012A729 File Offset: 0x00128929
		public string DisplayName
		{
			get
			{
				return (string)base[ContactFolderSchema.DisplayName];
			}
			set
			{
				base[ContactFolderSchema.DisplayName] = value;
			}
		}

		// Token: 0x170015F3 RID: 5619
		// (get) Token: 0x06005F96 RID: 24470 RVA: 0x0012A737 File Offset: 0x00128937
		// (set) Token: 0x06005F97 RID: 24471 RVA: 0x0012A749 File Offset: 0x00128949
		public IEnumerable<ContactFolder> ChildFolders
		{
			get
			{
				return (IEnumerable<ContactFolder>)base[ContactFolderSchema.ChildFolders];
			}
			set
			{
				base[ContactFolderSchema.ChildFolders] = value;
			}
		}

		// Token: 0x170015F4 RID: 5620
		// (get) Token: 0x06005F98 RID: 24472 RVA: 0x0012A757 File Offset: 0x00128957
		// (set) Token: 0x06005F99 RID: 24473 RVA: 0x0012A769 File Offset: 0x00128969
		public IEnumerable<Contact> Contacts
		{
			get
			{
				return (IEnumerable<Contact>)base[ContactFolderSchema.ChildFolders];
			}
			set
			{
				base[ContactFolderSchema.ChildFolders] = value;
			}
		}

		// Token: 0x170015F5 RID: 5621
		// (get) Token: 0x06005F9A RID: 24474 RVA: 0x0012A777 File Offset: 0x00128977
		internal override EntitySchema Schema
		{
			get
			{
				return ContactFolderSchema.SchemaInstance;
			}
		}

		// Token: 0x040033D5 RID: 13269
		internal new static readonly EdmEntityType EdmEntityType = new EdmEntityType(typeof(ContactFolder).Namespace, typeof(ContactFolder).Name, Entity.EdmEntityType);
	}
}
