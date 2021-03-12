using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000109 RID: 265
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SerializableTimeZoneTime
	{
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x0001F4B2 File Offset: 0x0001D6B2
		// (set) Token: 0x0600071B RID: 1819 RVA: 0x0001F4BA File Offset: 0x0001D6BA
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
				if (value < -720 || value > 720)
				{
					throw new InvalidParameterException(Strings.descInvalidTransitionBias(-720, 720));
				}
				this.bias = value;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x0001F4E8 File Offset: 0x0001D6E8
		// (set) Token: 0x0600071D RID: 1821 RVA: 0x0001F524 File Offset: 0x0001D724
		[XmlElement]
		[DataMember]
		public string Time
		{
			get
			{
				return string.Format("{0:00}:{1:00}:{2:00}", this.time.Hours, this.time.Minutes, this.time.Seconds);
			}
			set
			{
				if (!TimeSpan.TryParse(value, out this.time))
				{
					throw new InvalidParameterException(Strings.descInvalidTransitionTime);
				}
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x0001F53F File Offset: 0x0001D73F
		// (set) Token: 0x0600071F RID: 1823 RVA: 0x0001F547 File Offset: 0x0001D747
		[DataMember]
		[XmlElement]
		public short DayOrder
		{
			get
			{
				return this.dayOrder;
			}
			set
			{
				if (value < 0 || value > 31)
				{
					throw new InvalidParameterException(Strings.descInvalidDayOrder(0, 31));
				}
				this.dayOrder = value;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x0001F567 File Offset: 0x0001D767
		// (set) Token: 0x06000721 RID: 1825 RVA: 0x0001F56F File Offset: 0x0001D76F
		[XmlElement]
		[DataMember]
		public short Month
		{
			get
			{
				return this.month;
			}
			set
			{
				if (value < 0 || value > 12)
				{
					throw new InvalidParameterException(Strings.descInvalidMonth(0, 12));
				}
				this.month = value;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0001F58F File Offset: 0x0001D78F
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x0001F597 File Offset: 0x0001D797
		[DataMember]
		[XmlElement]
		public DayOfWeek DayOfWeek
		{
			get
			{
				return this.dayOfWeek;
			}
			set
			{
				this.dayOfWeek = value;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x0001F5A0 File Offset: 0x0001D7A0
		// (set) Token: 0x06000725 RID: 1829 RVA: 0x0001F5B8 File Offset: 0x0001D7B8
		[XmlElement(IsNullable = false)]
		[DataMember]
		public string Year
		{
			get
			{
				if (this.year != 0)
				{
					return Convert.ToString(this.year);
				}
				return null;
			}
			set
			{
				short num;
				if (!short.TryParse(value, out num))
				{
					throw new InvalidParameterException(Strings.descInvalidYear(1601, 4500));
				}
				if (num < 1601 || num > 4500)
				{
					throw new InvalidParameterException(Strings.descInvalidYear(1601, 4500));
				}
				this.year = num;
			}
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001F610 File Offset: 0x0001D810
		public override string ToString()
		{
			return string.Format("Bias = {0}, Month = {1}, DayOrder = {2}, DayOfWeek = {3}, Time = {4}", new object[]
			{
				this.bias,
				this.month,
				this.dayOrder,
				this.dayOfWeek,
				this.time
			});
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001F675 File Offset: 0x0001D875
		public SerializableTimeZoneTime()
		{
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001F680 File Offset: 0x0001D880
		internal SerializableTimeZoneTime(int bias, NativeMethods.SystemTime systemTime)
		{
			this.bias = bias;
			this.month = (short)systemTime.Month;
			this.dayOrder = (short)systemTime.Day;
			this.dayOfWeek = (DayOfWeek)systemTime.DayOfWeek;
			this.year = (short)systemTime.Year;
			this.time = new TimeSpan((int)systemTime.Hour, (int)systemTime.Minute, (int)systemTime.Second);
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x0001F6F4 File Offset: 0x0001D8F4
		internal NativeMethods.SystemTime SystemTime
		{
			get
			{
				return new NativeMethods.SystemTime
				{
					Year = (ushort)this.year,
					Month = (ushort)this.month,
					Day = (ushort)this.dayOrder,
					DayOfWeek = (ushort)this.DayOfWeek,
					Hour = (ushort)this.time.Hours,
					Minute = (ushort)this.time.Minutes,
					Second = (ushort)this.time.Seconds
				};
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x0001F77B File Offset: 0x0001D97B
		[XmlIgnore]
		internal short TransitionYear
		{
			get
			{
				return this.year;
			}
		}

		// Token: 0x04000446 RID: 1094
		public const int MaximumNegativeTransitionBias = -720;

		// Token: 0x04000447 RID: 1095
		public const int MaximumPositiveTransitionBias = 720;

		// Token: 0x04000448 RID: 1096
		public const int MinimumDayOrder = 0;

		// Token: 0x04000449 RID: 1097
		public const int MaximumDayOrder = 31;

		// Token: 0x0400044A RID: 1098
		public const int MinimumMonth = 0;

		// Token: 0x0400044B RID: 1099
		public const int MaximumMonth = 12;

		// Token: 0x0400044C RID: 1100
		public const int MinimumYear = 1601;

		// Token: 0x0400044D RID: 1101
		public const int MaximumYear = 4500;

		// Token: 0x0400044E RID: 1102
		private int bias;

		// Token: 0x0400044F RID: 1103
		private short year;

		// Token: 0x04000450 RID: 1104
		private short month;

		// Token: 0x04000451 RID: 1105
		private short dayOrder;

		// Token: 0x04000452 RID: 1106
		private DayOfWeek dayOfWeek;

		// Token: 0x04000453 RID: 1107
		private TimeSpan time;
	}
}
