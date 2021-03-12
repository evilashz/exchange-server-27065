using System;
using System.Xml.Serialization;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EDC RID: 3804
	[Serializable]
	public class WorkHoursTimeZone
	{
		// Token: 0x170022DA RID: 8922
		// (get) Token: 0x06008351 RID: 33617 RVA: 0x0023B378 File Offset: 0x00239578
		// (set) Token: 0x06008352 RID: 33618 RVA: 0x0023B385 File Offset: 0x00239585
		[XmlElement]
		public int Bias
		{
			get
			{
				return this.timeZoneInfo.Bias;
			}
			set
			{
				this.timeZoneInfo.Bias = value;
			}
		}

		// Token: 0x170022DB RID: 8923
		// (get) Token: 0x06008353 RID: 33619 RVA: 0x0023B393 File Offset: 0x00239593
		// (set) Token: 0x06008354 RID: 33620 RVA: 0x0023B3B0 File Offset: 0x002395B0
		[XmlElement]
		public ZoneTransition Standard
		{
			get
			{
				return new ZoneTransition(this.timeZoneInfo.StandardBias, this.timeZoneInfo.StandardDate);
			}
			set
			{
				this.timeZoneInfo.StandardBias = value.Bias;
				this.timeZoneInfo.StandardDate = value.ChangeDate.SystemTime;
			}
		}

		// Token: 0x170022DC RID: 8924
		// (get) Token: 0x06008355 RID: 33621 RVA: 0x0023B3D9 File Offset: 0x002395D9
		// (set) Token: 0x06008356 RID: 33622 RVA: 0x0023B3F6 File Offset: 0x002395F6
		[XmlElement]
		public ZoneTransition DaylightSavings
		{
			get
			{
				return new ZoneTransition(this.timeZoneInfo.DaylightBias, this.timeZoneInfo.DaylightDate);
			}
			set
			{
				this.timeZoneInfo.DaylightBias = value.Bias;
				this.timeZoneInfo.DaylightDate = value.ChangeDate.SystemTime;
			}
		}

		// Token: 0x170022DD RID: 8925
		// (get) Token: 0x06008357 RID: 33623 RVA: 0x0023B41F File Offset: 0x0023961F
		// (set) Token: 0x06008358 RID: 33624 RVA: 0x0023B427 File Offset: 0x00239627
		[XmlElement]
		public string Name { get; set; }

		// Token: 0x06008359 RID: 33625 RVA: 0x0023B430 File Offset: 0x00239630
		public WorkHoursTimeZone()
		{
		}

		// Token: 0x170022DE RID: 8926
		// (get) Token: 0x0600835A RID: 33626 RVA: 0x0023B438 File Offset: 0x00239638
		internal REG_TIMEZONE_INFO TimeZoneInfo
		{
			get
			{
				return this.timeZoneInfo;
			}
		}

		// Token: 0x0600835B RID: 33627 RVA: 0x0023B440 File Offset: 0x00239640
		internal WorkHoursTimeZone(ExTimeZone timeZone)
		{
			this.timeZoneInfo = TimeZoneHelper.RegTimeZoneInfoFromExTimeZone(timeZone);
			this.Name = timeZone.Id;
		}

		// Token: 0x0600835C RID: 33628 RVA: 0x0023B460 File Offset: 0x00239660
		internal bool IsSameTimeZoneInfo(REG_TIMEZONE_INFO other)
		{
			if (other == this.timeZoneInfo)
			{
				return true;
			}
			REG_TIMEZONE_INFO v = this.timeZoneInfo;
			v.StandardDate.Milliseconds = 0;
			v.DaylightDate.Milliseconds = 0;
			REG_TIMEZONE_INFO v2 = other;
			v2.StandardDate.Milliseconds = 0;
			v2.DaylightDate.Milliseconds = 0;
			return v == v2;
		}

		// Token: 0x040057F1 RID: 22513
		private REG_TIMEZONE_INFO timeZoneInfo;
	}
}
