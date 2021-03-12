using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.InfoWorker.Availability
{
	// Token: 0x0200000B RID: 11
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class FreeBusyView
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000032AE File Offset: 0x000014AE
		// (set) Token: 0x0600005A RID: 90 RVA: 0x000032B6 File Offset: 0x000014B6
		[XmlElement]
		[IgnoreDataMember]
		public FreeBusyViewType FreeBusyViewType
		{
			get
			{
				return this.view;
			}
			set
			{
				this.view = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000032BF File Offset: 0x000014BF
		// (set) Token: 0x0600005C RID: 92 RVA: 0x000032CC File Offset: 0x000014CC
		[DataMember(Name = "FreeBusyViewType")]
		[XmlIgnore]
		public string FreeBusyViewTypeString
		{
			get
			{
				return EnumUtilities.ToString<FreeBusyViewType>(this.FreeBusyViewType);
			}
			set
			{
				this.FreeBusyViewType = EnumUtilities.Parse<FreeBusyViewType>(value);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000032DA File Offset: 0x000014DA
		// (set) Token: 0x0600005E RID: 94 RVA: 0x000032E2 File Offset: 0x000014E2
		[DataMember]
		[XmlElement(IsNullable = false)]
		public string MergedFreeBusy
		{
			get
			{
				return this.mergedFreeBusy;
			}
			set
			{
				if (value != null)
				{
					this.mergedFreeBusy = value;
				}
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000032EE File Offset: 0x000014EE
		// (set) Token: 0x06000060 RID: 96 RVA: 0x000032F6 File Offset: 0x000014F6
		[XmlArray(IsNullable = false)]
		[DataMember]
		[XmlArrayItem(Type = typeof(CalendarEvent), IsNullable = false)]
		public CalendarEvent[] CalendarEventArray
		{
			get
			{
				return this.calendarEventArray;
			}
			set
			{
				if (value != null)
				{
					this.calendarEventArray = value;
				}
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003302 File Offset: 0x00001502
		// (set) Token: 0x06000062 RID: 98 RVA: 0x0000330A File Offset: 0x0000150A
		[DataMember]
		[XmlElement(IsNullable = false)]
		public WorkingHours WorkingHours
		{
			get
			{
				return this.workingHours;
			}
			set
			{
				if (value != null)
				{
					this.workingHours = value;
				}
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003318 File Offset: 0x00001518
		internal static FreeBusyView CreateFrom(FreeBusyQueryResult result)
		{
			if (result == null)
			{
				return null;
			}
			return new FreeBusyView
			{
				MergedFreeBusy = result.MergedFreeBusy,
				FreeBusyViewType = result.View,
				calendarEventArray = result.CalendarEventArray,
				workingHours = result.WorkingHours
			};
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003361 File Offset: 0x00001561
		private FreeBusyView()
		{
		}

		// Token: 0x0400001B RID: 27
		private FreeBusyViewType view;

		// Token: 0x0400001C RID: 28
		private string mergedFreeBusy;

		// Token: 0x0400001D RID: 29
		private CalendarEvent[] calendarEventArray;

		// Token: 0x0400001E RID: 30
		private WorkingHours workingHours;
	}
}
