using System;
using System.Collections.Generic;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Entity
{
	// Token: 0x020001A8 RID: 424
	[Serializable]
	internal class EntityReminderOffsetProperty : EntityProperty, IReminder160Property, IIntegerProperty, IProperty
	{
		// Token: 0x0600121B RID: 4635 RVA: 0x00062643 File Offset: 0x00060843
		public EntityReminderOffsetProperty() : base(SchematizedObject<EventSchema>.SchemaInstance.PopupReminderSettingsProperty, PropertyType.ReadWrite, false)
		{
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x00062657 File Offset: 0x00060857
		public bool ReminderIsSet
		{
			get
			{
				return base.CalendaringEvent.PopupReminderSettings.Count > 0 && base.CalendaringEvent.PopupReminderSettings[0].IsReminderSet;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x00062684 File Offset: 0x00060884
		public virtual int IntegerData
		{
			get
			{
				if (this.ReminderIsSet)
				{
					return base.CalendaringEvent.PopupReminderSettings[0].ReminderMinutesBeforeStart;
				}
				return -1;
			}
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x000626A8 File Offset: 0x000608A8
		public override void CopyFrom(IProperty srcProperty)
		{
			IReminder160Property reminder160Property = srcProperty as IReminder160Property;
			if (reminder160Property != null)
			{
				if (base.CalendaringEvent.PopupReminderSettings == null)
				{
					base.CalendaringEvent.PopupReminderSettings = new List<EventPopupReminderSetting>(1);
				}
				if (base.CalendaringEvent.PopupReminderSettings.Count == 0)
				{
					base.CalendaringEvent.PopupReminderSettings.Add(new EventPopupReminderSetting());
				}
				base.CalendaringEvent.PopupReminderSettings[0].IsReminderSet = reminder160Property.ReminderIsSet;
				if (reminder160Property.ReminderIsSet)
				{
					base.CalendaringEvent.PopupReminderSettings[0].ReminderMinutesBeforeStart = reminder160Property.IntegerData;
				}
			}
		}
	}
}
