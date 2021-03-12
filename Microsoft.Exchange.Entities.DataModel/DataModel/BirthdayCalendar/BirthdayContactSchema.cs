using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.DataModel.BirthdayCalendar
{
	// Token: 0x02000020 RID: 32
	public sealed class BirthdayContactSchema : StorageEntitySchema
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00002C94 File Offset: 0x00000E94
		public BirthdayContactSchema()
		{
			base.RegisterPropertyDefinition(BirthdayContactSchema.StaticAttributionProperty);
			base.RegisterPropertyDefinition(BirthdayContactSchema.StaticDisplayNameProperty);
			base.RegisterPropertyDefinition(BirthdayContactSchema.StaticBirthdayProperty);
			base.RegisterPropertyDefinition(BirthdayContactSchema.StaticPersonIdProperty);
			base.RegisterPropertyDefinition(BirthdayContactSchema.StaticShouldHideBirthdayProperty);
			base.RegisterPropertyDefinition(BirthdayContactSchema.StaticIsWritableProperty);
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002CE9 File Offset: 0x00000EE9
		public TypedPropertyDefinition<string> AttributionProperty
		{
			get
			{
				return BirthdayContactSchema.StaticAttributionProperty;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002CF0 File Offset: 0x00000EF0
		public TypedPropertyDefinition<ExDateTime?> BirthdayProperty
		{
			get
			{
				return BirthdayContactSchema.StaticBirthdayProperty;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00002CF7 File Offset: 0x00000EF7
		public TypedPropertyDefinition<string> DisplayNameProperty
		{
			get
			{
				return BirthdayContactSchema.StaticDisplayNameProperty;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00002CFE File Offset: 0x00000EFE
		public TypedPropertyDefinition<bool> ShouldHideBirthdayProperty
		{
			get
			{
				return BirthdayContactSchema.StaticShouldHideBirthdayProperty;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00002D05 File Offset: 0x00000F05
		public TypedPropertyDefinition<bool> IsWritableProperty
		{
			get
			{
				return BirthdayContactSchema.StaticIsWritableProperty;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002D0C File Offset: 0x00000F0C
		internal TypedPropertyDefinition<PersonId> PersonIdProperty
		{
			get
			{
				return BirthdayContactSchema.StaticPersonIdProperty;
			}
		}

		// Token: 0x04000033 RID: 51
		private static readonly TypedPropertyDefinition<string> StaticDisplayNameProperty = new TypedPropertyDefinition<string>("BirthdayContact.DisplayName", null, false);

		// Token: 0x04000034 RID: 52
		private static readonly TypedPropertyDefinition<string> StaticAttributionProperty = new TypedPropertyDefinition<string>("BirthdayContact.Attribution", null, true);

		// Token: 0x04000035 RID: 53
		private static readonly TypedPropertyDefinition<ExDateTime?> StaticBirthdayProperty = new TypedPropertyDefinition<ExDateTime?>("BirthdayContact.Birthday", null, true);

		// Token: 0x04000036 RID: 54
		private static readonly TypedPropertyDefinition<PersonId> StaticPersonIdProperty = new TypedPropertyDefinition<PersonId>("BirthdayContact.PersonId", null, false);

		// Token: 0x04000037 RID: 55
		private static readonly TypedPropertyDefinition<bool> StaticShouldHideBirthdayProperty = new TypedPropertyDefinition<bool>("BirthdayContact.ShouldHideBirthday", false, true);

		// Token: 0x04000038 RID: 56
		private static readonly TypedPropertyDefinition<bool> StaticIsWritableProperty = new TypedPropertyDefinition<bool>("BirthdayContact.IsWritable", false, true);
	}
}
