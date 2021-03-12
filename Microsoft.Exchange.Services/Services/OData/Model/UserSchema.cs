using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E53 RID: 3667
	internal class UserSchema : EntitySchema
	{
		// Token: 0x1700156B RID: 5483
		// (get) Token: 0x06005E55 RID: 24149 RVA: 0x00125F8D File Offset: 0x0012418D
		public new static UserSchema SchemaInstance
		{
			get
			{
				return UserSchema.UserSchemaInstance.Member;
			}
		}

		// Token: 0x1700156C RID: 5484
		// (get) Token: 0x06005E56 RID: 24150 RVA: 0x00125F99 File Offset: 0x00124199
		public override EdmEntityType EdmEntityType
		{
			get
			{
				return User.EdmEntityType;
			}
		}

		// Token: 0x1700156D RID: 5485
		// (get) Token: 0x06005E57 RID: 24151 RVA: 0x00125FA0 File Offset: 0x001241A0
		public override ReadOnlyCollection<PropertyDefinition> DeclaredProperties
		{
			get
			{
				return UserSchema.DeclaredUserProperties;
			}
		}

		// Token: 0x1700156E RID: 5486
		// (get) Token: 0x06005E58 RID: 24152 RVA: 0x00125FA7 File Offset: 0x001241A7
		public override ReadOnlyCollection<PropertyDefinition> AllProperties
		{
			get
			{
				return UserSchema.AllUserProperties;
			}
		}

		// Token: 0x1700156F RID: 5487
		// (get) Token: 0x06005E59 RID: 24153 RVA: 0x00125FAE File Offset: 0x001241AE
		public override ReadOnlyCollection<PropertyDefinition> DefaultProperties
		{
			get
			{
				return UserSchema.DefaultUserProperties;
			}
		}

		// Token: 0x04003302 RID: 13058
		public static readonly PropertyDefinition DisplayName = new PropertyDefinition("DisplayName", typeof(string))
		{
			EdmType = EdmCoreModel.Instance.GetString(true),
			Flags = PropertyDefinitionFlags.CanFilter,
			ADDriverPropertyProvider = new SimpleDirectoryPropertyProvider(ADRecipientSchema.DisplayName)
		};

		// Token: 0x04003303 RID: 13059
		public static readonly PropertyDefinition Alias = new PropertyDefinition("Alias", typeof(string))
		{
			EdmType = EdmCoreModel.Instance.GetString(true),
			Flags = PropertyDefinitionFlags.CanFilter,
			ADDriverPropertyProvider = new SimpleDirectoryPropertyProvider(ADRecipientSchema.Alias)
		};

		// Token: 0x04003304 RID: 13060
		public static readonly PropertyDefinition MailboxGuid = new PropertyDefinition("MailboxGuid", typeof(Guid))
		{
			EdmType = EdmCoreModel.Instance.GetGuid(true),
			Flags = PropertyDefinitionFlags.CanFilter,
			ADDriverPropertyProvider = new SimpleDirectoryPropertyProvider(ADMailboxRecipientSchema.ExchangeGuid)
		};

		// Token: 0x04003305 RID: 13061
		public static readonly PropertyDefinition Folders = new PropertyDefinition("Folders", typeof(IEnumerable<Folder>))
		{
			Flags = PropertyDefinitionFlags.Navigation,
			NavigationTargetEntity = Folder.EdmEntityType
		};

		// Token: 0x04003306 RID: 13062
		public static readonly PropertyDefinition Messages = new PropertyDefinition("Messages", typeof(IEnumerable<Message>))
		{
			Flags = PropertyDefinitionFlags.Navigation,
			NavigationTargetEntity = Message.EdmEntityType
		};

		// Token: 0x04003307 RID: 13063
		public static readonly PropertyDefinition Calendars = new PropertyDefinition("Calendars", typeof(IEnumerable<Calendar>))
		{
			Flags = PropertyDefinitionFlags.Navigation,
			NavigationTargetEntity = Microsoft.Exchange.Services.OData.Model.Calendar.EdmEntityType
		};

		// Token: 0x04003308 RID: 13064
		public static readonly PropertyDefinition Calendar = new PropertyDefinition("Calendar", typeof(Calendar))
		{
			Flags = PropertyDefinitionFlags.Navigation,
			NavigationTargetEntity = Microsoft.Exchange.Services.OData.Model.Calendar.EdmEntityType
		};

		// Token: 0x04003309 RID: 13065
		public static readonly PropertyDefinition CalendarGroups = new PropertyDefinition("CalendarGroups", typeof(IEnumerable<CalendarGroup>))
		{
			Flags = PropertyDefinitionFlags.Navigation,
			NavigationTargetEntity = CalendarGroup.EdmEntityType
		};

		// Token: 0x0400330A RID: 13066
		public static readonly PropertyDefinition Events = new PropertyDefinition("Events", typeof(IEnumerable<Event>))
		{
			Flags = PropertyDefinitionFlags.Navigation,
			NavigationTargetEntity = Event.EdmEntityType
		};

		// Token: 0x0400330B RID: 13067
		public static readonly PropertyDefinition RootFolder = new PropertyDefinition("RootFolder", typeof(Folder))
		{
			Flags = PropertyDefinitionFlags.Navigation,
			NavigationTargetEntity = Folder.EdmEntityType
		};

		// Token: 0x0400330C RID: 13068
		public static readonly PropertyDefinition Inbox = new PropertyDefinition("Inbox", typeof(Folder))
		{
			Flags = PropertyDefinitionFlags.Navigation,
			NavigationTargetEntity = Folder.EdmEntityType
		};

		// Token: 0x0400330D RID: 13069
		public static readonly PropertyDefinition Drafts = new PropertyDefinition("Drafts", typeof(Folder))
		{
			Flags = PropertyDefinitionFlags.Navigation,
			NavigationTargetEntity = Folder.EdmEntityType
		};

		// Token: 0x0400330E RID: 13070
		public static readonly PropertyDefinition SentItems = new PropertyDefinition("SentItems", typeof(Folder))
		{
			Flags = PropertyDefinitionFlags.Navigation,
			NavigationTargetEntity = Folder.EdmEntityType
		};

		// Token: 0x0400330F RID: 13071
		public static readonly PropertyDefinition DeletedItems = new PropertyDefinition("DeletedItems", typeof(Folder))
		{
			Flags = PropertyDefinitionFlags.Navigation,
			NavigationTargetEntity = Folder.EdmEntityType
		};

		// Token: 0x04003310 RID: 13072
		public static readonly PropertyDefinition Contacts = new PropertyDefinition("Contacts", typeof(IEnumerable<Contact>))
		{
			Flags = PropertyDefinitionFlags.Navigation,
			NavigationTargetEntity = Contact.EdmEntityType
		};

		// Token: 0x04003311 RID: 13073
		public static readonly PropertyDefinition ContactFolders = new PropertyDefinition("ContactFolders", typeof(IEnumerable<ContactFolder>))
		{
			Flags = PropertyDefinitionFlags.Navigation,
			NavigationTargetEntity = ContactFolder.EdmEntityType
		};

		// Token: 0x04003312 RID: 13074
		public static readonly ReadOnlyCollection<PropertyDefinition> DeclaredUserProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>
		{
			UserSchema.DisplayName,
			UserSchema.Alias,
			UserSchema.MailboxGuid,
			UserSchema.Folders,
			UserSchema.Messages,
			UserSchema.RootFolder,
			UserSchema.Inbox,
			UserSchema.Drafts,
			UserSchema.SentItems,
			UserSchema.DeletedItems,
			UserSchema.Calendars,
			UserSchema.Calendar,
			UserSchema.CalendarGroups,
			UserSchema.Events,
			UserSchema.Contacts,
			UserSchema.ContactFolders
		});

		// Token: 0x04003313 RID: 13075
		public static readonly ReadOnlyCollection<PropertyDefinition> AllUserProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>(EntitySchema.AllEntityProperties.Union(UserSchema.DeclaredUserProperties)));

		// Token: 0x04003314 RID: 13076
		public static readonly ReadOnlyCollection<PropertyDefinition> DefaultUserProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>(EntitySchema.DefaultEntityProperties)
		{
			UserSchema.DisplayName,
			UserSchema.Alias,
			UserSchema.MailboxGuid
		});

		// Token: 0x04003315 RID: 13077
		private static readonly LazyMember<UserSchema> UserSchemaInstance = new LazyMember<UserSchema>(() => new UserSchema());
	}
}
