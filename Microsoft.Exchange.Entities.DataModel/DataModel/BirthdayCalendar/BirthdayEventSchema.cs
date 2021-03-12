using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.DataModel.BirthdayCalendar
{
	// Token: 0x02000025 RID: 37
	public sealed class BirthdayEventSchema : StorageEntitySchema
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00003104 File Offset: 0x00001304
		public BirthdayEventSchema()
		{
			base.RegisterPropertyDefinition(BirthdayEventSchema.StaticAttributionProperty);
			base.RegisterPropertyDefinition(BirthdayEventSchema.StaticSubjectProperty);
			base.RegisterPropertyDefinition(BirthdayEventSchema.StaticBirthdayProperty);
			base.RegisterPropertyDefinition(BirthdayEventSchema.StaticPersonIdProperty);
			base.RegisterPropertyDefinition(BirthdayEventSchema.StaticContactIdProperty);
			base.RegisterPropertyDefinition(BirthdayEventSchema.StaticIsWritableProperty);
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003159 File Offset: 0x00001359
		public TypedPropertyDefinition<string> AttributionProperty
		{
			get
			{
				return BirthdayEventSchema.StaticAttributionProperty;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003160 File Offset: 0x00001360
		public TypedPropertyDefinition<string> SubjectProperty
		{
			get
			{
				return BirthdayEventSchema.StaticSubjectProperty;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003167 File Offset: 0x00001367
		public TypedPropertyDefinition<ExDateTime> BirthdayProperty
		{
			get
			{
				return BirthdayEventSchema.StaticBirthdayProperty;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000316E File Offset: 0x0000136E
		public TypedPropertyDefinition<bool> IsWritableProperty
		{
			get
			{
				return BirthdayEventSchema.StaticIsWritableProperty;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00003175 File Offset: 0x00001375
		internal TypedPropertyDefinition<PersonId> PersonIdProperty
		{
			get
			{
				return BirthdayEventSchema.StaticPersonIdProperty;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000317C File Offset: 0x0000137C
		internal TypedPropertyDefinition<StoreObjectId> ContactIdProperty
		{
			get
			{
				return BirthdayEventSchema.StaticContactIdProperty;
			}
		}

		// Token: 0x0400004B RID: 75
		private static readonly TypedPropertyDefinition<string> StaticAttributionProperty = new TypedPropertyDefinition<string>("BirthdayEvent.Attribution", null, true);

		// Token: 0x0400004C RID: 76
		private static readonly TypedPropertyDefinition<string> StaticSubjectProperty = new TypedPropertyDefinition<string>("BirthdayEvent.Subject", null, true);

		// Token: 0x0400004D RID: 77
		private static readonly TypedPropertyDefinition<ExDateTime> StaticBirthdayProperty = new TypedPropertyDefinition<ExDateTime>("BirthdayEvent.Birthday", default(ExDateTime), true);

		// Token: 0x0400004E RID: 78
		private static readonly TypedPropertyDefinition<PersonId> StaticPersonIdProperty = new TypedPropertyDefinition<PersonId>("BirthdayEvent.PersonId", null, false);

		// Token: 0x0400004F RID: 79
		private static readonly TypedPropertyDefinition<StoreObjectId> StaticContactIdProperty = new TypedPropertyDefinition<StoreObjectId>("BirthdayEvent.ContactId", null, false);

		// Token: 0x04000050 RID: 80
		private static readonly TypedPropertyDefinition<bool> StaticIsWritableProperty = new TypedPropertyDefinition<bool>("BirthdayEvent.IsWritable", false, true);
	}
}
