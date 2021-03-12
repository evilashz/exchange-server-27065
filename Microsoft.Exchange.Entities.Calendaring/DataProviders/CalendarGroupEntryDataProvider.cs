using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.Calendaring.TypeConversion.Translators;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataProviders;
using Microsoft.Exchange.Entities.EntitySets;
using Microsoft.Exchange.Entities.TypeConversion.Translators;

namespace Microsoft.Exchange.Entities.Calendaring.DataProviders
{
	// Token: 0x0200001D RID: 29
	internal class CalendarGroupEntryDataProvider : StorageItemDataProvider<IMailboxSession, Calendar, ICalendarGroupEntry>
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x000035B8 File Offset: 0x000017B8
		public CalendarGroupEntryDataProvider(IStorageEntitySetScope<IMailboxSession> parentScope) : base(parentScope, null, ExTraceGlobals.CalendarDataProviderTracer)
		{
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000035C7 File Offset: 0x000017C7
		protected override IStorageTranslator<ICalendarGroupEntry, Calendar> Translator
		{
			get
			{
				return CalendarGroupEntryTranslator.Instance;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000035D0 File Offset: 0x000017D0
		public virtual Calendar Create(Calendar entity, ICalendarGroup calendarGroup)
		{
			this.Validate(entity, true);
			Calendar result;
			using (ICalendarGroupEntry calendarGroupEntry = base.XsoFactory.CreateCalendarGroupEntry(base.Session, StoreId.GetStoreObjectId(entity.CalendarFolderStoreId), calendarGroup))
			{
				result = this.Update(entity, calendarGroupEntry, SaveMode.NoConflictResolution);
			}
			return result;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000362C File Offset: 0x0000182C
		public override void Validate(Calendar entity, bool isNew)
		{
			if (entity.IsPropertySet(entity.Schema.NameProperty))
			{
				entity.Name = ((entity.Name != null) ? entity.Name.Trim() : null);
				if (string.IsNullOrWhiteSpace(entity.Name))
				{
					throw new CalendarNameCannotBeEmptyException();
				}
			}
			else if (isNew)
			{
				throw new CalendarNameCannotBeEmptyException();
			}
			base.Validate(entity, isNew);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000368C File Offset: 0x0000188C
		protected internal override ICalendarGroupEntry BindToStoreObject(StoreId id)
		{
			return base.XsoFactory.BindToCalendarGroupEntry(base.Session, id);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000036A0 File Offset: 0x000018A0
		protected internal override void ValidateStoreObjectIdForCorrectType(StoreObjectId storeObjectId)
		{
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000036A2 File Offset: 0x000018A2
		protected override ICalendarGroupEntry CreateNewStoreObject()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000036AC File Offset: 0x000018AC
		protected override void SaveAndCheckForConflicts(ICalendarGroupEntry storeObject, SaveMode saveMode)
		{
			try
			{
				base.SaveAndCheckForConflicts(storeObject, saveMode);
			}
			catch (IrresolvableConflictException innerException)
			{
				throw new CalendarGroupEntryUpdateFailedException(innerException);
			}
		}
	}
}
