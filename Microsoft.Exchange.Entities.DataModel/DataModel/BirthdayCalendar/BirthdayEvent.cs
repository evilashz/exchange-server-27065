using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.DataModel.BirthdayCalendar
{
	// Token: 0x02000023 RID: 35
	public class BirthdayEvent : StorageEntity<BirthdayEventSchema>, IBirthdayEventInternal, IBirthdayEvent, IStorageEntity, IEntity, IPropertyChangeTracker<PropertyDefinition>, IVersioned
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00002D8F File Offset: 0x00000F8F
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00002DA2 File Offset: 0x00000FA2
		public ExDateTime Birthday
		{
			get
			{
				return base.GetPropertyValueOrDefault<ExDateTime>(base.Schema.BirthdayProperty);
			}
			set
			{
				base.SetPropertyValue<ExDateTime>(base.Schema.BirthdayProperty, value);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00002DB6 File Offset: 0x00000FB6
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00002DC9 File Offset: 0x00000FC9
		public string Attribution
		{
			get
			{
				return base.GetPropertyValueOrDefault<string>(base.Schema.AttributionProperty);
			}
			set
			{
				base.SetPropertyValue<string>(base.Schema.AttributionProperty, value);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00002DE0 File Offset: 0x00000FE0
		public bool IsBirthYearKnown
		{
			get
			{
				return this.Birthday.Year != 1604;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00002E05 File Offset: 0x00001005
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00002E18 File Offset: 0x00001018
		public bool IsWritable
		{
			get
			{
				return base.GetPropertyValueOrDefault<bool>(base.Schema.IsWritableProperty);
			}
			set
			{
				base.SetPropertyValue<bool>(base.Schema.IsWritableProperty, value);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00002E2C File Offset: 0x0000102C
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00002E3F File Offset: 0x0000103F
		public string Subject
		{
			get
			{
				return base.GetPropertyValueOrDefault<string>(base.Schema.SubjectProperty);
			}
			set
			{
				base.SetPropertyValue<string>(base.Schema.SubjectProperty, value);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00002E53 File Offset: 0x00001053
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00002E66 File Offset: 0x00001066
		PersonId IBirthdayEventInternal.PersonId
		{
			get
			{
				return base.GetPropertyValueOrDefault<PersonId>(base.Schema.PersonIdProperty);
			}
			set
			{
				base.SetPropertyValue<PersonId>(base.Schema.PersonIdProperty, value);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00002E7A File Offset: 0x0000107A
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00002E8D File Offset: 0x0000108D
		StoreObjectId IBirthdayEventInternal.ContactId
		{
			get
			{
				return base.GetPropertyValueOrDefault<StoreObjectId>(base.Schema.ContactIdProperty);
			}
			set
			{
				base.SetPropertyValue<StoreObjectId>(base.Schema.ContactIdProperty, value);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00002EA1 File Offset: 0x000010A1
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00002EA9 File Offset: 0x000010A9
		StoreId IBirthdayEventInternal.StoreId
		{
			get
			{
				return base.StoreId;
			}
			set
			{
				base.StoreId = value;
			}
		}

		// Token: 0x02000024 RID: 36
		public new static class Accessors
		{
			// Token: 0x04000039 RID: 57
			public static readonly EntityPropertyAccessor<IBirthdayEvent, string> Subject = new EntityPropertyAccessor<IBirthdayEvent, string>(SchematizedObject<BirthdayEventSchema>.SchemaInstance.SubjectProperty, (IBirthdayEvent birthdayEvent) => birthdayEvent.Subject, delegate(IBirthdayEvent birthdayEvent, string s)
			{
				birthdayEvent.Subject = s;
			});

			// Token: 0x0400003A RID: 58
			public static readonly EntityPropertyAccessor<IBirthdayEvent, string> Attribution = new EntityPropertyAccessor<IBirthdayEvent, string>(SchematizedObject<BirthdayEventSchema>.SchemaInstance.AttributionProperty, (IBirthdayEvent birthdayEvent) => birthdayEvent.Attribution, delegate(IBirthdayEvent birthdayEvent, string attribution)
			{
				birthdayEvent.Attribution = attribution;
			});

			// Token: 0x0400003B RID: 59
			public static readonly EntityPropertyAccessor<IBirthdayEvent, ExDateTime> Birthday = new EntityPropertyAccessor<IBirthdayEvent, ExDateTime>(SchematizedObject<BirthdayEventSchema>.SchemaInstance.BirthdayProperty, (IBirthdayEvent birthdayEvent) => birthdayEvent.Birthday, delegate(IBirthdayEvent birthdayEvent, ExDateTime birthday)
			{
				birthdayEvent.Birthday = birthday;
			});

			// Token: 0x0400003C RID: 60
			public static readonly EntityPropertyAccessor<IBirthdayEvent, bool> IsWritable = new EntityPropertyAccessor<IBirthdayEvent, bool>(SchematizedObject<BirthdayEventSchema>.SchemaInstance.IsWritableProperty, (IBirthdayEvent birthdayEvent) => birthdayEvent.IsWritable, delegate(IBirthdayEvent birthdayEvent, bool isWritable)
			{
				birthdayEvent.IsWritable = isWritable;
			});

			// Token: 0x0400003D RID: 61
			internal static readonly EntityPropertyAccessor<IBirthdayEventInternal, PersonId> PersonId = new EntityPropertyAccessor<IBirthdayEventInternal, PersonId>(SchematizedObject<BirthdayEventSchema>.SchemaInstance.PersonIdProperty, (IBirthdayEventInternal birthdayEvent) => birthdayEvent.PersonId, delegate(IBirthdayEventInternal birthdayEvent, PersonId personId)
			{
				birthdayEvent.PersonId = personId;
			});

			// Token: 0x0400003E RID: 62
			internal static readonly EntityPropertyAccessor<IBirthdayEventInternal, StoreObjectId> ContactId = new EntityPropertyAccessor<IBirthdayEventInternal, StoreObjectId>(SchematizedObject<BirthdayEventSchema>.SchemaInstance.ContactIdProperty, (IBirthdayEventInternal birthdayEvent) => birthdayEvent.ContactId, delegate(IBirthdayEventInternal birthdayEvent, StoreObjectId contactId)
			{
				birthdayEvent.ContactId = contactId;
			});
		}
	}
}
