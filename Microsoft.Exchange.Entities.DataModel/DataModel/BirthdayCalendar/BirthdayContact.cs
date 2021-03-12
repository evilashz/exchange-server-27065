using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.BirthdayCalendar;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.DataModel.BirthdayCalendar
{
	// Token: 0x0200001E RID: 30
	public class BirthdayContact : StorageEntity<BirthdayContactSchema>, IBirthdayContactInternal, IBirthdayContact, IStorageEntity, IEntity, IPropertyChangeTracker<PropertyDefinition>, IVersioned
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000065 RID: 101 RVA: 0x0000293C File Offset: 0x00000B3C
		// (set) Token: 0x06000066 RID: 102 RVA: 0x0000294F File Offset: 0x00000B4F
		public string DisplayName
		{
			get
			{
				return base.GetPropertyValueOrDefault<string>(base.Schema.DisplayNameProperty);
			}
			set
			{
				base.SetPropertyValue<string>(base.Schema.DisplayNameProperty, value);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002963 File Offset: 0x00000B63
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002976 File Offset: 0x00000B76
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

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000298A File Offset: 0x00000B8A
		// (set) Token: 0x0600006A RID: 106 RVA: 0x0000299D File Offset: 0x00000B9D
		public ExDateTime? Birthday
		{
			get
			{
				return base.GetPropertyValueOrDefault<ExDateTime?>(base.Schema.BirthdayProperty);
			}
			set
			{
				base.SetPropertyValue<ExDateTime?>(base.Schema.BirthdayProperty, value);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000029B1 File Offset: 0x00000BB1
		// (set) Token: 0x0600006C RID: 108 RVA: 0x000029C4 File Offset: 0x00000BC4
		PersonId IBirthdayContactInternal.PersonId
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

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000029D8 File Offset: 0x00000BD8
		// (set) Token: 0x0600006E RID: 110 RVA: 0x000029E0 File Offset: 0x00000BE0
		StoreId IBirthdayContactInternal.StoreId
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

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000029E9 File Offset: 0x00000BE9
		// (set) Token: 0x06000070 RID: 112 RVA: 0x000029FC File Offset: 0x00000BFC
		public bool ShouldHideBirthday
		{
			get
			{
				return base.GetPropertyValueOrDefault<bool>(base.Schema.ShouldHideBirthdayProperty);
			}
			set
			{
				base.SetPropertyValue<bool>(base.Schema.ShouldHideBirthdayProperty, value);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002A10 File Offset: 0x00000C10
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00002A23 File Offset: 0x00000C23
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

		// Token: 0x06000073 RID: 115 RVA: 0x00002A37 File Offset: 0x00000C37
		public override string ToString()
		{
			return this.DisplayName;
		}

		// Token: 0x0200001F RID: 31
		public new static class Accessors
		{
			// Token: 0x04000021 RID: 33
			public static readonly EntityPropertyAccessor<IBirthdayContact, ExDateTime?> Birthday = new EntityPropertyAccessor<IBirthdayContact, ExDateTime?>(SchematizedObject<BirthdayContactSchema>.SchemaInstance.BirthdayProperty, (IBirthdayContact theBirthdayContact) => theBirthdayContact.Birthday, delegate(IBirthdayContact theBirthdayContact, ExDateTime? birthday)
			{
				theBirthdayContact.Birthday = birthday;
			});

			// Token: 0x04000022 RID: 34
			public static readonly EntityPropertyAccessor<IBirthdayContact, string> DisplayName = new EntityPropertyAccessor<IBirthdayContact, string>(SchematizedObject<BirthdayContactSchema>.SchemaInstance.DisplayNameProperty, (IBirthdayContact theBirthdayContact) => theBirthdayContact.DisplayName, delegate(IBirthdayContact theBirthdayContact, string displayName)
			{
				theBirthdayContact.DisplayName = displayName;
			});

			// Token: 0x04000023 RID: 35
			public static readonly EntityPropertyAccessor<IBirthdayContact, bool> ShouldHideBirthday = new EntityPropertyAccessor<IBirthdayContact, bool>(SchematizedObject<BirthdayContactSchema>.SchemaInstance.ShouldHideBirthdayProperty, (IBirthdayContact theBirthdayContact) => theBirthdayContact.ShouldHideBirthday, delegate(IBirthdayContact theBirthdayContact, bool shouldHideBirthday)
			{
				theBirthdayContact.ShouldHideBirthday = shouldHideBirthday;
			});

			// Token: 0x04000024 RID: 36
			public static readonly EntityPropertyAccessor<IBirthdayContact, bool> IsWritable = new EntityPropertyAccessor<IBirthdayContact, bool>(SchematizedObject<BirthdayContactSchema>.SchemaInstance.IsWritableProperty, (IBirthdayContact theBirthdayContact) => theBirthdayContact.IsWritable, delegate(IBirthdayContact theBirthdayContact, bool isWritable)
			{
				theBirthdayContact.IsWritable = isWritable;
			});

			// Token: 0x04000025 RID: 37
			public static readonly EntityPropertyAccessor<IBirthdayContact, string> Attribution = new EntityPropertyAccessor<IBirthdayContact, string>(SchematizedObject<BirthdayContactSchema>.SchemaInstance.AttributionProperty, (IBirthdayContact theBirthdayContact) => theBirthdayContact.Attribution, delegate(IBirthdayContact theBirthdayContact, string attribution)
			{
				theBirthdayContact.Attribution = attribution;
			});

			// Token: 0x04000026 RID: 38
			internal static readonly EntityPropertyAccessor<IBirthdayContactInternal, PersonId> PersonId = new EntityPropertyAccessor<IBirthdayContactInternal, PersonId>(SchematizedObject<BirthdayContactSchema>.SchemaInstance.PersonIdProperty, (IBirthdayContactInternal theBirthdayContact) => theBirthdayContact.PersonId, delegate(IBirthdayContactInternal theBirthdayContact, PersonId personId)
			{
				theBirthdayContact.PersonId = personId;
			});
		}
	}
}
