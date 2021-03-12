using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E52 RID: 3666
	internal class User : Entity
	{
		// Token: 0x1700155A RID: 5466
		// (get) Token: 0x06005E31 RID: 24113 RVA: 0x00125D10 File Offset: 0x00123F10
		// (set) Token: 0x06005E32 RID: 24114 RVA: 0x00125D22 File Offset: 0x00123F22
		public string DisplayName
		{
			get
			{
				return (string)base[UserSchema.DisplayName];
			}
			set
			{
				base[UserSchema.DisplayName] = value;
			}
		}

		// Token: 0x1700155B RID: 5467
		// (get) Token: 0x06005E33 RID: 24115 RVA: 0x00125D30 File Offset: 0x00123F30
		// (set) Token: 0x06005E34 RID: 24116 RVA: 0x00125D42 File Offset: 0x00123F42
		public string Alias
		{
			get
			{
				return (string)base[UserSchema.Alias];
			}
			set
			{
				base[UserSchema.Alias] = value;
			}
		}

		// Token: 0x1700155C RID: 5468
		// (get) Token: 0x06005E35 RID: 24117 RVA: 0x00125D50 File Offset: 0x00123F50
		// (set) Token: 0x06005E36 RID: 24118 RVA: 0x00125D62 File Offset: 0x00123F62
		public Guid MailboxGuid
		{
			get
			{
				return (Guid)base[UserSchema.MailboxGuid];
			}
			set
			{
				base[UserSchema.MailboxGuid] = value;
			}
		}

		// Token: 0x1700155D RID: 5469
		// (get) Token: 0x06005E37 RID: 24119 RVA: 0x00125D75 File Offset: 0x00123F75
		// (set) Token: 0x06005E38 RID: 24120 RVA: 0x00125D87 File Offset: 0x00123F87
		public IEnumerable<Folder> Folders
		{
			get
			{
				return (IEnumerable<Folder>)base[UserSchema.Folders];
			}
			set
			{
				base[UserSchema.Folders] = value;
			}
		}

		// Token: 0x1700155E RID: 5470
		// (get) Token: 0x06005E39 RID: 24121 RVA: 0x00125D95 File Offset: 0x00123F95
		// (set) Token: 0x06005E3A RID: 24122 RVA: 0x00125DA7 File Offset: 0x00123FA7
		public IEnumerable<Message> Messages
		{
			get
			{
				return (IEnumerable<Message>)base[UserSchema.Messages];
			}
			set
			{
				base[UserSchema.Messages] = value;
			}
		}

		// Token: 0x1700155F RID: 5471
		// (get) Token: 0x06005E3B RID: 24123 RVA: 0x00125DB5 File Offset: 0x00123FB5
		// (set) Token: 0x06005E3C RID: 24124 RVA: 0x00125DC7 File Offset: 0x00123FC7
		public IEnumerable<Event> Events
		{
			get
			{
				return (IEnumerable<Event>)base[UserSchema.Events];
			}
			set
			{
				base[UserSchema.Events] = value;
			}
		}

		// Token: 0x17001560 RID: 5472
		// (get) Token: 0x06005E3D RID: 24125 RVA: 0x00125DD5 File Offset: 0x00123FD5
		// (set) Token: 0x06005E3E RID: 24126 RVA: 0x00125DE7 File Offset: 0x00123FE7
		public IEnumerable<Calendar> Calendars
		{
			get
			{
				return (IEnumerable<Calendar>)base[UserSchema.Calendars];
			}
			set
			{
				base[UserSchema.Calendars] = value;
			}
		}

		// Token: 0x17001561 RID: 5473
		// (get) Token: 0x06005E3F RID: 24127 RVA: 0x00125DF5 File Offset: 0x00123FF5
		// (set) Token: 0x06005E40 RID: 24128 RVA: 0x00125E07 File Offset: 0x00124007
		public Calendar Calendar
		{
			get
			{
				return (Calendar)base[UserSchema.Calendar];
			}
			set
			{
				base[UserSchema.Calendar] = value;
			}
		}

		// Token: 0x17001562 RID: 5474
		// (get) Token: 0x06005E41 RID: 24129 RVA: 0x00125E15 File Offset: 0x00124015
		// (set) Token: 0x06005E42 RID: 24130 RVA: 0x00125E27 File Offset: 0x00124027
		public IEnumerable<CalendarGroup> CalendarGroups
		{
			get
			{
				return (IEnumerable<CalendarGroup>)base[UserSchema.CalendarGroups];
			}
			set
			{
				base[UserSchema.CalendarGroups] = value;
			}
		}

		// Token: 0x17001563 RID: 5475
		// (get) Token: 0x06005E43 RID: 24131 RVA: 0x00125E35 File Offset: 0x00124035
		// (set) Token: 0x06005E44 RID: 24132 RVA: 0x00125E47 File Offset: 0x00124047
		public Folder RootFolder
		{
			get
			{
				return (Folder)base[UserSchema.RootFolder];
			}
			set
			{
				base[UserSchema.RootFolder] = value;
			}
		}

		// Token: 0x17001564 RID: 5476
		// (get) Token: 0x06005E45 RID: 24133 RVA: 0x00125E55 File Offset: 0x00124055
		// (set) Token: 0x06005E46 RID: 24134 RVA: 0x00125E67 File Offset: 0x00124067
		public Folder Inbox
		{
			get
			{
				return (Folder)base[UserSchema.Inbox];
			}
			set
			{
				base[UserSchema.Inbox] = value;
			}
		}

		// Token: 0x17001565 RID: 5477
		// (get) Token: 0x06005E47 RID: 24135 RVA: 0x00125E75 File Offset: 0x00124075
		// (set) Token: 0x06005E48 RID: 24136 RVA: 0x00125E87 File Offset: 0x00124087
		public Folder Drafts
		{
			get
			{
				return (Folder)base[UserSchema.Drafts];
			}
			set
			{
				base[UserSchema.Drafts] = value;
			}
		}

		// Token: 0x17001566 RID: 5478
		// (get) Token: 0x06005E49 RID: 24137 RVA: 0x00125E95 File Offset: 0x00124095
		// (set) Token: 0x06005E4A RID: 24138 RVA: 0x00125EA7 File Offset: 0x001240A7
		public Folder SentItems
		{
			get
			{
				return (Folder)base[UserSchema.SentItems];
			}
			set
			{
				base[UserSchema.SentItems] = value;
			}
		}

		// Token: 0x17001567 RID: 5479
		// (get) Token: 0x06005E4B RID: 24139 RVA: 0x00125EB5 File Offset: 0x001240B5
		// (set) Token: 0x06005E4C RID: 24140 RVA: 0x00125EC7 File Offset: 0x001240C7
		public Folder DeletedItems
		{
			get
			{
				return (Folder)base[UserSchema.DeletedItems];
			}
			set
			{
				base[UserSchema.DeletedItems] = value;
			}
		}

		// Token: 0x17001568 RID: 5480
		// (get) Token: 0x06005E4D RID: 24141 RVA: 0x00125ED5 File Offset: 0x001240D5
		// (set) Token: 0x06005E4E RID: 24142 RVA: 0x00125EE7 File Offset: 0x001240E7
		public IEnumerable<Contact> Contacts
		{
			get
			{
				return (IEnumerable<Contact>)base[UserSchema.Contacts];
			}
			set
			{
				base[UserSchema.Contacts] = value;
			}
		}

		// Token: 0x17001569 RID: 5481
		// (get) Token: 0x06005E4F RID: 24143 RVA: 0x00125EF5 File Offset: 0x001240F5
		// (set) Token: 0x06005E50 RID: 24144 RVA: 0x00125F07 File Offset: 0x00124107
		public IEnumerable<ContactFolder> ContactFolders
		{
			get
			{
				return (IEnumerable<ContactFolder>)base[UserSchema.ContactFolders];
			}
			set
			{
				base[UserSchema.ContactFolders] = value;
			}
		}

		// Token: 0x1700156A RID: 5482
		// (get) Token: 0x06005E51 RID: 24145 RVA: 0x00125F15 File Offset: 0x00124115
		internal override EntitySchema Schema
		{
			get
			{
				return UserSchema.SchemaInstance;
			}
		}

		// Token: 0x06005E52 RID: 24146 RVA: 0x00125F1C File Offset: 0x0012411C
		internal override Uri GetWebUri(ODataContext odataContext)
		{
			ArgumentValidator.ThrowIfNull("odataContext", odataContext);
			string uriString = string.Format("{0}Users('{1}')", odataContext.HttpContext.GetServiceRootUri(), base.Id);
			return new Uri(uriString);
		}

		// Token: 0x04003301 RID: 13057
		internal new static readonly EdmEntityType EdmEntityType = new EdmEntityType(typeof(User).Namespace, typeof(User).Name, Entity.EdmEntityType);
	}
}
