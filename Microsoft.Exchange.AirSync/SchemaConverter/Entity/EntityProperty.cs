using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Entity
{
	// Token: 0x02000198 RID: 408
	internal abstract class EntityProperty : PropertyBase
	{
		// Token: 0x060011A7 RID: 4519 RVA: 0x00060A6B File Offset: 0x0005EC6B
		public EntityProperty(PropertyType type = PropertyType.ReadWrite, bool syncForOccurenceItem = false)
		{
			this.Type = type;
			this.SyncForOccurenceItem = syncForOccurenceItem;
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x00060A81 File Offset: 0x0005EC81
		public EntityProperty(PropertyDefinition edmProperty, PropertyType type = PropertyType.ReadWrite, bool syncForOccurenceItem = false) : this(new EntityPropertyDefinition(edmProperty), type, false)
		{
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00060A91 File Offset: 0x0005EC91
		public EntityProperty(EntityPropertyDefinition propertyDefinition, PropertyType type = PropertyType.ReadWrite, bool syncForOccurenceItem = false)
		{
			this.EntityPropertyDefinition = propertyDefinition;
			this.Type = type;
			this.SyncForOccurenceItem = syncForOccurenceItem;
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x060011AA RID: 4522 RVA: 0x00060AAE File Offset: 0x0005ECAE
		// (set) Token: 0x060011AB RID: 4523 RVA: 0x00060AB6 File Offset: 0x0005ECB6
		private protected IItem Item { protected get; private set; }

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x060011AC RID: 4524 RVA: 0x00060ABF File Offset: 0x0005ECBF
		// (set) Token: 0x060011AD RID: 4525 RVA: 0x00060AC7 File Offset: 0x0005ECC7
		public PropertyType Type { get; private set; }

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x00060AD0 File Offset: 0x0005ECD0
		// (set) Token: 0x060011AF RID: 4527 RVA: 0x00060AD8 File Offset: 0x0005ECD8
		public bool SyncForOccurenceItem { get; set; }

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x00060AE1 File Offset: 0x0005ECE1
		// (set) Token: 0x060011B1 RID: 4529 RVA: 0x00060AE9 File Offset: 0x0005ECE9
		internal EntityPropertyDefinition EntityPropertyDefinition { get; private set; }

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x00060AF2 File Offset: 0x0005ECF2
		protected Event CalendaringEvent
		{
			get
			{
				if (this.calendaringEvent == null)
				{
					this.calendaringEvent = (this.Item as Event);
					if (this.calendaringEvent == null)
					{
						throw new UnexpectedTypeException("Event", this.Item);
					}
				}
				return this.calendaringEvent;
			}
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x00060B2C File Offset: 0x0005ED2C
		public virtual void Bind(IItem item)
		{
			this.Item = item;
			if (!this.SyncForOccurenceItem && this.CalendaringEvent != null && this.CalendaringEvent.Type == EventType.Occurrence)
			{
				base.State = PropertyState.SetToDefault;
				return;
			}
			if (this.EntityPropertyDefinition == null)
			{
				base.State = PropertyState.Modified;
				return;
			}
			if (this.Item.IsPropertySet(this.EntityPropertyDefinition.EdmDefinition))
			{
				base.State = PropertyState.Modified;
				return;
			}
			base.State = PropertyState.SetToDefault;
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x00060BA0 File Offset: 0x0005EDA0
		public override void Unbind()
		{
			try
			{
				this.calendaringEvent = null;
				this.Item = null;
				base.State = PropertyState.Uninitialized;
			}
			finally
			{
				base.Unbind();
			}
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x00060BDC File Offset: 0x0005EDDC
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"(Base: ",
				base.ToString(),
				", propertyDefinition: ",
				this.EntityPropertyDefinition,
				", type: ",
				this.Type,
				", item: ",
				this.Item,
				", state: ",
				base.State,
				")"
			});
		}

		// Token: 0x04000B3D RID: 2877
		private Event calendaringEvent;
	}
}
