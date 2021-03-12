using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.Calendaring.TypeConversion.Translators;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataProviders;
using Microsoft.Exchange.Entities.EntitySets;
using Microsoft.Exchange.Entities.TypeConversion.Translators;

namespace Microsoft.Exchange.Entities.Calendaring.DataProviders
{
	// Token: 0x0200001C RID: 28
	internal class CalendarGroupDataProvider : StorageItemDataProvider<IMailboxSession, CalendarGroup, ICalendarGroup>
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00003400 File Offset: 0x00001600
		public CalendarGroupDataProvider(IStorageEntitySetScope<IMailboxSession> parentScope) : base(parentScope, null, ExTraceGlobals.CalendarDataProviderTracer)
		{
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x0000340F File Offset: 0x0000160F
		protected override IStorageTranslator<ICalendarGroup, CalendarGroup> Translator
		{
			get
			{
				return CalendarGroupTranslator.Instance;
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003418 File Offset: 0x00001618
		public override void Delete(StoreId id, DeleteItemFlags flags)
		{
			using (ICalendarGroup calendarGroup = this.Bind(id))
			{
				CalendarGroupType groupType = calendarGroup.GroupType;
				if (groupType == CalendarGroupType.MyCalendars || groupType == CalendarGroupType.OtherCalendars)
				{
					throw new CannotDeleteSpecialCalendarGroupException(calendarGroup.Id, calendarGroup.GroupClassId, calendarGroup.GroupName);
				}
				ReadOnlyCollection<CalendarGroupEntryInfo> childCalendars = calendarGroup.GetChildCalendars();
				List<StoreId> list = new List<StoreId>();
				foreach (CalendarGroupEntryInfo calendarGroupEntryInfo in childCalendars)
				{
					if (calendarGroupEntryInfo is LocalCalendarGroupEntryInfo)
					{
						throw new CalendarGroupIsNotEmptyException(calendarGroup.Id, calendarGroup.GroupClassId, calendarGroup.GroupName, childCalendars.Count);
					}
					list.Add(calendarGroupEntryInfo.Id);
				}
				foreach (StoreId id2 in list)
				{
					base.Delete(id2, flags);
				}
			}
			base.Delete(id, flags);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003530 File Offset: 0x00001730
		public override void Validate(CalendarGroup entity, bool isNew)
		{
			if (entity.IsPropertySet(entity.Schema.NameProperty))
			{
				entity.Name = ((entity.Name != null) ? entity.Name.Trim() : null);
				if (string.IsNullOrWhiteSpace(entity.Name))
				{
					throw new InvalidCalendarGroupNameException();
				}
			}
			else if (isNew)
			{
				throw new InvalidCalendarGroupNameException();
			}
			base.Validate(entity, isNew);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003590 File Offset: 0x00001790
		protected internal override ICalendarGroup BindToStoreObject(StoreId id)
		{
			return base.XsoFactory.BindToCalendarGroup(base.Session, id, null);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000035A5 File Offset: 0x000017A5
		protected override ICalendarGroup CreateNewStoreObject()
		{
			return base.XsoFactory.CreateCalendarGroup(base.Session);
		}
	}
}
