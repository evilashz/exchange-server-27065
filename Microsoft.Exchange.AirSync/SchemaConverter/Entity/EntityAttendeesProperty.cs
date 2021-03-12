using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Entity
{
	// Token: 0x0200019A RID: 410
	[Serializable]
	internal class EntityAttendeesProperty : EntityProperty, IExtendedAttendeesProperty, IMultivaluedProperty<ExtendedAttendeeData>, IProperty, IEnumerable<ExtendedAttendeeData>, IEnumerable
	{
		// Token: 0x060011BE RID: 4542 RVA: 0x00061103 File Offset: 0x0005F303
		public EntityAttendeesProperty() : base(SchematizedObject<EventSchema>.SchemaInstance.AttendeesProperty, PropertyType.ReadWrite, false)
		{
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x060011BF RID: 4543 RVA: 0x00061117 File Offset: 0x0005F317
		public int Count
		{
			get
			{
				if (this.values == null)
				{
					return 0;
				}
				return this.values.Count;
			}
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0006112E File Offset: 0x0005F32E
		public override void Bind(IItem item)
		{
			base.Bind(item);
			this.values = base.CalendaringEvent.Attendees;
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00061148 File Offset: 0x0005F348
		public override void Unbind()
		{
			try
			{
				this.values = null;
			}
			finally
			{
				base.Unbind();
			}
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x0006134C File Offset: 0x0005F54C
		public IEnumerator<ExtendedAttendeeData> GetEnumerator()
		{
			if (this.values != null)
			{
				foreach (Attendee attendee in this.values)
				{
					bool sendExtendedData = base.CalendaringEvent.IsOrganizer;
					ResponseStatus status = attendee.Status;
					ResponseType response = (status == null) ? ResponseType.None : status.Response;
					if (response != ResponseType.Organizer)
					{
						yield return new ExtendedAttendeeData(attendee.EmailAddress, attendee.Name, (int)response, (int)attendee.Type, sendExtendedData);
					}
				}
			}
			yield break;
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00061368 File Offset: 0x0005F568
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00061370 File Offset: 0x0005F570
		public override void CopyFrom(IProperty srcProperty)
		{
			IExtendedAttendeesProperty extendedAttendeesProperty = srcProperty as IExtendedAttendeesProperty;
			if (extendedAttendeesProperty != null)
			{
				List<Attendee> list = new List<Attendee>(extendedAttendeesProperty.Count);
				foreach (ExtendedAttendeeData extendedAttendeeData in extendedAttendeesProperty)
				{
					list.Add(new Attendee
					{
						EmailAddress = extendedAttendeeData.EmailAddress,
						Name = extendedAttendeeData.DisplayName,
						Type = (AttendeeType)extendedAttendeeData.Type
					});
				}
				base.CalendaringEvent.Attendees = list;
			}
		}

		// Token: 0x04000B43 RID: 2883
		private IList<Attendee> values;
	}
}
