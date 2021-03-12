using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.Calendar
{
	// Token: 0x0200009F RID: 159
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "Calendar", TypeName = "Recurrence")]
	public class Recurrence
	{
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000A323 File Offset: 0x00008523
		// (set) Token: 0x060003BB RID: 955 RVA: 0x0000A32B File Offset: 0x0000852B
		[XmlElement(ElementName = "Type")]
		public byte Type { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0000A334 File Offset: 0x00008534
		// (set) Token: 0x060003BD RID: 957 RVA: 0x0000A33C File Offset: 0x0000853C
		[XmlElement(ElementName = "Interval")]
		public ushort? Interval { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000A345 File Offset: 0x00008545
		// (set) Token: 0x060003BF RID: 959 RVA: 0x0000A34D File Offset: 0x0000854D
		[XmlElement(ElementName = "DayOfWeek")]
		public ushort? DayOfWeek { get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000A356 File Offset: 0x00008556
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x0000A35E File Offset: 0x0000855E
		[XmlElement(ElementName = "WeekOfMonth")]
		public byte? WeekOfMonth { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000A367 File Offset: 0x00008567
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x0000A36F File Offset: 0x0000856F
		[XmlElement(ElementName = "DayOfMonth")]
		public byte? DayOfMonth { get; set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000A378 File Offset: 0x00008578
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x0000A380 File Offset: 0x00008580
		[XmlElement(ElementName = "MonthOfYear")]
		public byte? MonthOfYear { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0000A389 File Offset: 0x00008589
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x0000A391 File Offset: 0x00008591
		[XmlElement(ElementName = "Occurrences")]
		public ushort? Occurrences { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000A39A File Offset: 0x0000859A
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x0000A3A2 File Offset: 0x000085A2
		[XmlElement(ElementName = "Until", Namespace = "Calendar")]
		public string Until { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0000A3AC File Offset: 0x000085AC
		[XmlIgnore]
		public bool IntervalSpecified
		{
			get
			{
				return this.Interval != null;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000A3C8 File Offset: 0x000085C8
		[XmlIgnore]
		public bool DayOfWeekSpecified
		{
			get
			{
				return this.DayOfWeek != null;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0000A3E4 File Offset: 0x000085E4
		[XmlIgnore]
		public bool WeekOfMonthSpecified
		{
			get
			{
				return this.WeekOfMonth != null;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000A400 File Offset: 0x00008600
		[XmlIgnore]
		public bool DayOfMonthSpecified
		{
			get
			{
				return this.DayOfMonth != null;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0000A41C File Offset: 0x0000861C
		[XmlIgnore]
		public bool MonthOfYearSpecified
		{
			get
			{
				return this.MonthOfYear != null;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0000A438 File Offset: 0x00008638
		[XmlIgnore]
		public bool OccurrencesSpecified
		{
			get
			{
				return this.Occurrences != null;
			}
		}
	}
}
