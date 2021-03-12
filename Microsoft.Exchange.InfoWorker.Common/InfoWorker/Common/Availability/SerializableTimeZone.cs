using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000108 RID: 264
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SerializableTimeZone
	{
		// Token: 0x0600070E RID: 1806 RVA: 0x0001F329 File Offset: 0x0001D529
		public SerializableTimeZone()
		{
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0001F331 File Offset: 0x0001D531
		internal SerializableTimeZone(ExTimeZone timezone)
		{
			this.TimeZone = timezone;
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x0001F340 File Offset: 0x0001D540
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x0001F348 File Offset: 0x0001D548
		[XmlElement]
		[DataMember]
		public int Bias
		{
			get
			{
				return this.bias;
			}
			set
			{
				if (value < -1440 || value > 1440)
				{
					throw new InvalidParameterException(Strings.descInvalidTimeZoneBias);
				}
				this.bias = value;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x0001F36C File Offset: 0x0001D56C
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x0001F374 File Offset: 0x0001D574
		[DataMember]
		[XmlElement]
		public SerializableTimeZoneTime StandardTime
		{
			get
			{
				return this.standardTime;
			}
			set
			{
				this.standardTime = value;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x0001F37D File Offset: 0x0001D57D
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x0001F385 File Offset: 0x0001D585
		[XmlElement]
		[DataMember]
		public SerializableTimeZoneTime DaylightTime
		{
			get
			{
				return this.daylightTime;
			}
			set
			{
				this.daylightTime = value;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x0001F390 File Offset: 0x0001D590
		// (set) Token: 0x06000717 RID: 1815 RVA: 0x0001F41C File Offset: 0x0001D61C
		[XmlIgnore]
		internal ExTimeZone TimeZone
		{
			get
			{
				REG_TIMEZONE_INFO regInfo = default(REG_TIMEZONE_INFO);
				regInfo.Bias = this.Bias;
				if (this.StandardTime != null)
				{
					regInfo.StandardBias = this.StandardTime.Bias;
					regInfo.StandardDate = this.StandardTime.SystemTime;
				}
				if (this.DaylightTime != null)
				{
					regInfo.DaylightBias = this.DaylightTime.Bias;
					regInfo.DaylightDate = this.DaylightTime.SystemTime;
				}
				return TimeZoneHelper.CreateCustomExTimeZoneFromRegTimeZoneInfo(regInfo, "tzone://Microsoft/Custom", "Customized Time Zone");
			}
			set
			{
				REG_TIMEZONE_INFO reg_TIMEZONE_INFO = TimeZoneHelper.RegTimeZoneInfoFromExTimeZone(value);
				this.Bias = reg_TIMEZONE_INFO.Bias;
				this.standardTime = new SerializableTimeZoneTime(reg_TIMEZONE_INFO.StandardBias, reg_TIMEZONE_INFO.StandardDate);
				this.daylightTime = new SerializableTimeZoneTime(reg_TIMEZONE_INFO.DaylightBias, reg_TIMEZONE_INFO.DaylightDate);
			}
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0001F46F File Offset: 0x0001D66F
		internal bool IsDynamicTimeZone()
		{
			return this.daylightTime.TransitionYear > 0 || this.standardTime.TransitionYear > 0;
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001F48F File Offset: 0x0001D68F
		public override string ToString()
		{
			return string.Format("Bias = {0}, [StandardTime = {1}], [DaylightTime = {2}]", this.bias, this.standardTime, this.daylightTime);
		}

		// Token: 0x04000441 RID: 1089
		public const int MaximumNegativeBias = -1440;

		// Token: 0x04000442 RID: 1090
		public const int MaximumPositiveBias = 1440;

		// Token: 0x04000443 RID: 1091
		private int bias;

		// Token: 0x04000444 RID: 1092
		private SerializableTimeZoneTime daylightTime;

		// Token: 0x04000445 RID: 1093
		private SerializableTimeZoneTime standardTime;
	}
}
