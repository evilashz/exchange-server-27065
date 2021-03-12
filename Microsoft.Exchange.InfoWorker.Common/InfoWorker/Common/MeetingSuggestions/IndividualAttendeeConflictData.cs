using System;
using System.Xml.Serialization;
using Microsoft.Exchange.InfoWorker.Common.Availability;

namespace Microsoft.Exchange.InfoWorker.Common.MeetingSuggestions
{
	// Token: 0x02000044 RID: 68
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class IndividualAttendeeConflictData : AttendeeConflictData
	{
		// Token: 0x06000160 RID: 352 RVA: 0x00008764 File Offset: 0x00006964
		internal static IndividualAttendeeConflictData Create(AttendeeData inputAttendee, BusyType busyType)
		{
			return new IndividualAttendeeConflictData
			{
				attendee = inputAttendee,
				busyType = busyType
			};
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00008786 File Offset: 0x00006986
		// (set) Token: 0x06000162 RID: 354 RVA: 0x0000878E File Offset: 0x0000698E
		[XmlElement]
		public BusyType BusyType
		{
			get
			{
				return this.busyType;
			}
			set
			{
				this.busyType = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00008797 File Offset: 0x00006997
		[XmlIgnore]
		public AttendeeData Attendee
		{
			get
			{
				return this.attendee;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000087A0 File Offset: 0x000069A0
		[XmlIgnore]
		public bool AttendeeHasConflict
		{
			get
			{
				switch (this.busyType)
				{
				case BusyType.Tentative:
				case BusyType.Busy:
				case BusyType.OOF:
				case BusyType.NoData:
					return true;
				}
				return false;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000165 RID: 357 RVA: 0x000087D8 File Offset: 0x000069D8
		[XmlIgnore]
		public bool IsMissingFreeBusyData
		{
			get
			{
				BusyType busyType = this.busyType;
				return busyType == BusyType.NoData;
			}
		}

		// Token: 0x040000ED RID: 237
		private AttendeeData attendee;

		// Token: 0x040000EE RID: 238
		private BusyType busyType;
	}
}
