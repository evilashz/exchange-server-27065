using System;
using System.Collections;
using System.Globalization;
using System.Net;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x02000188 RID: 392
	[Serializable]
	internal class FlagData : INestedData
	{
		// Token: 0x060010ED RID: 4333 RVA: 0x0005D9FF File Offset: 0x0005BBFF
		public FlagData()
		{
			this.subProperties = new Hashtable();
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x0005DA12 File Offset: 0x0005BC12
		// (set) Token: 0x060010EF RID: 4335 RVA: 0x0005DA20 File Offset: 0x0005BC20
		public ExDateTime? CompleteTime
		{
			get
			{
				return this.GetDateTimeProperty("CompleteTime");
			}
			set
			{
				this.subProperties["CompleteTime"] = ((value != null) ? value.Value.ToString("yyyy-MM-dd\\THH:mm:ss.fff\\Z", DateTimeFormatInfo.InvariantInfo) : null);
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x0005DA62 File Offset: 0x0005BC62
		// (set) Token: 0x060010F1 RID: 4337 RVA: 0x0005DA70 File Offset: 0x0005BC70
		public ExDateTime? DateCompleted
		{
			get
			{
				return this.GetDateTimeProperty("DateCompleted");
			}
			set
			{
				this.subProperties["DateCompleted"] = ((value != null) ? value.Value.ToString("yyyy-MM-dd\\THH:mm:ss.fff\\Z", DateTimeFormatInfo.InvariantInfo) : null);
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x0005DAB2 File Offset: 0x0005BCB2
		// (set) Token: 0x060010F3 RID: 4339 RVA: 0x0005DAC0 File Offset: 0x0005BCC0
		public ExDateTime? DueDate
		{
			get
			{
				return this.GetDateTimeProperty("DueDate");
			}
			set
			{
				this.subProperties["DueDate"] = ((value != null) ? value.Value.ToString("yyyy-MM-dd\\THH:mm:ss.fff\\Z", DateTimeFormatInfo.InvariantInfo) : null);
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x060010F4 RID: 4340 RVA: 0x0005DB02 File Offset: 0x0005BD02
		// (set) Token: 0x060010F5 RID: 4341 RVA: 0x0005DB10 File Offset: 0x0005BD10
		public ExDateTime? OrdinalDate
		{
			get
			{
				return this.GetDateTimeProperty("OrdinalDate");
			}
			set
			{
				this.subProperties["OrdinalDate"] = ((value != null) ? value.Value.ToString("yyyy-MM-dd\\THH:mm:ss.fff\\Z", DateTimeFormatInfo.InvariantInfo) : null);
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x060010F6 RID: 4342 RVA: 0x0005DB54 File Offset: 0x0005BD54
		// (set) Token: 0x060010F7 RID: 4343 RVA: 0x0005DB94 File Offset: 0x0005BD94
		public bool? ReminderSet
		{
			get
			{
				string text = this.subProperties["ReminderSet"] as string;
				if (text == null)
				{
					return null;
				}
				return new bool?(text == "1");
			}
			set
			{
				this.subProperties["ReminderSet"] = ((value != null) ? (value.Value ? "1" : "0") : null);
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060010F8 RID: 4344 RVA: 0x0005DBC7 File Offset: 0x0005BDC7
		// (set) Token: 0x060010F9 RID: 4345 RVA: 0x0005DBD4 File Offset: 0x0005BDD4
		public ExDateTime? ReminderTime
		{
			get
			{
				return this.GetDateTimeProperty("ReminderTime");
			}
			set
			{
				this.subProperties["ReminderTime"] = ((value != null) ? value.Value.ToString("yyyy-MM-dd\\THH:mm:ss.fff\\Z", DateTimeFormatInfo.InvariantInfo) : null);
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x0005DC16 File Offset: 0x0005BE16
		// (set) Token: 0x060010FB RID: 4347 RVA: 0x0005DC24 File Offset: 0x0005BE24
		public ExDateTime? StartDate
		{
			get
			{
				return this.GetDateTimeProperty("StartDate");
			}
			set
			{
				this.subProperties["StartDate"] = ((value != null) ? value.Value.ToString("yyyy-MM-dd\\THH:mm:ss.fff\\Z", DateTimeFormatInfo.InvariantInfo) : null);
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x0005DC68 File Offset: 0x0005BE68
		// (set) Token: 0x060010FD RID: 4349 RVA: 0x0005DCA8 File Offset: 0x0005BEA8
		public int? Status
		{
			get
			{
				string text = this.subProperties["Status"] as string;
				if (text == null)
				{
					return null;
				}
				return new int?(int.Parse(text, CultureInfo.InvariantCulture));
			}
			set
			{
				this.subProperties["Status"] = ((value != null) ? value.Value.ToString(CultureInfo.InvariantCulture) : null);
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x0005DCE5 File Offset: 0x0005BEE5
		// (set) Token: 0x060010FF RID: 4351 RVA: 0x0005DCFC File Offset: 0x0005BEFC
		public string Subject
		{
			get
			{
				return this.subProperties["Subject"] as string;
			}
			set
			{
				this.subProperties["Subject"] = value;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001100 RID: 4352 RVA: 0x0005DD0F File Offset: 0x0005BF0F
		// (set) Token: 0x06001101 RID: 4353 RVA: 0x0005DD26 File Offset: 0x0005BF26
		public string SubOrdinalDate
		{
			get
			{
				return this.subProperties["SubOrdinalDate"] as string;
			}
			set
			{
				this.subProperties["SubOrdinalDate"] = value;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001102 RID: 4354 RVA: 0x0005DD39 File Offset: 0x0005BF39
		public IDictionary SubProperties
		{
			get
			{
				return this.subProperties;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x0005DD41 File Offset: 0x0005BF41
		// (set) Token: 0x06001104 RID: 4356 RVA: 0x0005DD58 File Offset: 0x0005BF58
		public string Type
		{
			get
			{
				return this.subProperties["FlagType"] as string;
			}
			set
			{
				this.subProperties["FlagType"] = value;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001105 RID: 4357 RVA: 0x0005DD6B File Offset: 0x0005BF6B
		// (set) Token: 0x06001106 RID: 4358 RVA: 0x0005DD78 File Offset: 0x0005BF78
		public ExDateTime? UtcDueDate
		{
			get
			{
				return this.GetDateTimeProperty("UtcDueDate");
			}
			set
			{
				this.subProperties["UtcDueDate"] = ((value != null) ? value.Value.ToString("yyyy-MM-dd\\THH:mm:ss.fff\\Z", DateTimeFormatInfo.InvariantInfo) : null);
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001107 RID: 4359 RVA: 0x0005DDBA File Offset: 0x0005BFBA
		// (set) Token: 0x06001108 RID: 4360 RVA: 0x0005DDC8 File Offset: 0x0005BFC8
		public ExDateTime? UtcStartDate
		{
			get
			{
				return this.GetDateTimeProperty("UtcStartDate");
			}
			set
			{
				this.subProperties["UtcStartDate"] = ((value != null) ? value.Value.ToString("yyyy-MM-dd\\THH:mm:ss.fff\\Z", DateTimeFormatInfo.InvariantInfo) : null);
			}
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x0005DE0C File Offset: 0x0005C00C
		public static bool IsTaskProperty(string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new ArgumentNullException("propertyName");
			}
			return propertyName == "StartDate" || propertyName == "UtcStartDate" || propertyName == "DueDate" || propertyName == "UtcDueDate" || propertyName == "DateCompleted" || propertyName == "ReminderSet" || propertyName == "ReminderTime" || propertyName == "Subject" || propertyName == "OrdinalDate" || propertyName == "SubOrdinalDate";
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x0005DEAE File Offset: 0x0005C0AE
		public void Clear()
		{
			this.subProperties.Clear();
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x0005DEBB File Offset: 0x0005C0BB
		public bool ContainsValidData()
		{
			return this.subProperties.Count > 0;
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x0005DECC File Offset: 0x0005C0CC
		private ExDateTime? GetDateTimeProperty(string propertyName)
		{
			string text = this.subProperties[propertyName] as string;
			if (text == null)
			{
				return null;
			}
			ExDateTime value;
			if (!ExDateTime.TryParseExact(text, "yyyy-MM-dd\\THH:mm:ss.fff\\Z", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out value))
			{
				throw new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.InvalidDateTime, null, false)
				{
					ErrorStringForProtocolLogger = "InvalidDateTimeInFlagData"
				};
			}
			return new ExDateTime?(value);
		}

		// Token: 0x04000AEF RID: 2799
		private IDictionary subProperties;
	}
}
