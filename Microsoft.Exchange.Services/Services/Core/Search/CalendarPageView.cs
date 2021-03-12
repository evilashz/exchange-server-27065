using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x02000260 RID: 608
	[XmlType(TypeName = "CalendarView", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class CalendarPageView : BasePagingType
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000FF0 RID: 4080 RVA: 0x0004D3F2 File Offset: 0x0004B5F2
		// (set) Token: 0x06000FF1 RID: 4081 RVA: 0x0004D3FA File Offset: 0x0004B5FA
		[XmlAttribute]
		[IgnoreDataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x0004D404 File Offset: 0x0004B604
		// (set) Token: 0x06000FF3 RID: 4083 RVA: 0x0004D424 File Offset: 0x0004B624
		[DataMember(Name = "StartDate", IsRequired = true)]
		[XmlIgnore]
		public string StartDateString
		{
			get
			{
				return this.StartDate.ToString(CultureInfo.InvariantCulture);
			}
			set
			{
				this.StartDate = DateTime.Parse(value);
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x0004D432 File Offset: 0x0004B632
		// (set) Token: 0x06000FF5 RID: 4085 RVA: 0x0004D43A File Offset: 0x0004B63A
		[IgnoreDataMember]
		[XmlAttribute]
		public DateTime EndDate { get; set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x0004D444 File Offset: 0x0004B644
		// (set) Token: 0x06000FF7 RID: 4087 RVA: 0x0004D464 File Offset: 0x0004B664
		[XmlIgnore]
		[DataMember(Name = "EndDate", IsRequired = true)]
		public string EndDateString
		{
			get
			{
				return this.EndDate.ToString(CultureInfo.InvariantCulture);
			}
			set
			{
				this.EndDate = DateTime.Parse(value);
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x0004D474 File Offset: 0x0004B674
		[XmlIgnore]
		[IgnoreDataMember]
		public ExDateTime StartDateEx
		{
			get
			{
				if (!this.startExDateTimeInitialized)
				{
					try
					{
						if (this.StartDate.Kind == DateTimeKind.Local)
						{
							this.startExDateTime = new ExDateTime(EWSSettings.RequestTimeZone, this.StartDate.ToUniversalTime());
						}
						else
						{
							this.startExDateTime = new ExDateTime(EWSSettings.RequestTimeZone, this.StartDate);
						}
						this.startExDateTimeInitialized = true;
					}
					catch (ArgumentOutOfRangeException innerException)
					{
						throw new ValueOutOfRangeException(innerException, CalendarPageView.messageXmlValuesNames, CalendarPageView.messageXmlValuesStartDate);
					}
				}
				return this.startExDateTime;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x0004D504 File Offset: 0x0004B704
		[XmlIgnore]
		[IgnoreDataMember]
		public ExDateTime EndDateEx
		{
			get
			{
				if (!this.endExDateTimeInitialized)
				{
					try
					{
						if (this.EndDate.Kind == DateTimeKind.Local)
						{
							this.endExDateTime = new ExDateTime(EWSSettings.RequestTimeZone, this.EndDate.ToUniversalTime());
						}
						else
						{
							this.endExDateTime = new ExDateTime(EWSSettings.RequestTimeZone, this.EndDate);
						}
						this.endExDateTimeInitialized = true;
					}
					catch (ArgumentOutOfRangeException innerException)
					{
						throw new ValueOutOfRangeException(innerException, CalendarPageView.messageXmlValuesNames, CalendarPageView.messageXmlValuesEndDate);
					}
				}
				return this.endExDateTime;
			}
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0004D594 File Offset: 0x0004B794
		internal BasePageResult ApplyPostQueryPaging(object[][] view)
		{
			return new BasePageResult(new NormalQueryView(view, base.MaxRows));
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0004D5A8 File Offset: 0x0004B7A8
		internal void Validate(CalendarFolder calendarFolder)
		{
			if (calendarFolder == null)
			{
				throw new CalendarExceptionFolderIsInvalidForCalendarView();
			}
			ExDateTime t = this.StartDateEx;
			ExDateTime exDateTime = this.EndDateEx;
			if (t > exDateTime)
			{
				throw new CalendarExceptionEndDateIsEarlierThanStartDate();
			}
			ExDateTime other = ExDateTime.MaxValue.AddYears(-2);
			if (t.CompareTo(other) < 0)
			{
				t = t.AddYears(2);
			}
			else
			{
				exDateTime = exDateTime.AddYears(-2);
			}
			if (t.CompareTo(exDateTime) < 0)
			{
				throw new CalendarExceptionViewRangeTooBig();
			}
		}

		// Token: 0x04000BE5 RID: 3045
		private const int MaxCalendarViewRangeInYears = 2;

		// Token: 0x04000BE6 RID: 3046
		private static string[] messageXmlValuesNames = new string[]
		{
			"Element",
			"Attribute"
		};

		// Token: 0x04000BE7 RID: 3047
		private static string[] messageXmlValuesStartDate = new string[]
		{
			"CalendarView",
			"StartDate"
		};

		// Token: 0x04000BE8 RID: 3048
		private static string[] messageXmlValuesEndDate = new string[]
		{
			"CalendarView",
			"EndDate"
		};

		// Token: 0x04000BE9 RID: 3049
		private ExDateTime startExDateTime;

		// Token: 0x04000BEA RID: 3050
		private ExDateTime endExDateTime;

		// Token: 0x04000BEB RID: 3051
		private bool startExDateTimeInitialized;

		// Token: 0x04000BEC RID: 3052
		private bool endExDateTimeInitialized;
	}
}
