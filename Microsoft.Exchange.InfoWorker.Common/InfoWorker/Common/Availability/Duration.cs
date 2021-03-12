using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000F4 RID: 244
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class Duration
	{
		// Token: 0x0600067C RID: 1660 RVA: 0x0001CF8B File Offset: 0x0001B18B
		public Duration()
		{
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001CF93 File Offset: 0x0001B193
		public Duration(DateTime startTime, DateTime endTime)
		{
			this.startTime = startTime;
			this.endTime = endTime;
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x0001CFA9 File Offset: 0x0001B1A9
		// (set) Token: 0x0600067F RID: 1663 RVA: 0x0001CFB1 File Offset: 0x0001B1B1
		[IgnoreDataMember]
		[XmlElement]
		public DateTime StartTime
		{
			get
			{
				return this.startTime;
			}
			set
			{
				this.startTime = value;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x0001CFBA File Offset: 0x0001B1BA
		// (set) Token: 0x06000681 RID: 1665 RVA: 0x0001CFC2 File Offset: 0x0001B1C2
		[XmlElement]
		[IgnoreDataMember]
		public DateTime EndTime
		{
			get
			{
				return this.endTime;
			}
			set
			{
				this.endTime = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x0001CFCB File Offset: 0x0001B1CB
		// (set) Token: 0x06000683 RID: 1667 RVA: 0x0001CFD8 File Offset: 0x0001B1D8
		[DataMember(Name = "StartTime", IsRequired = true)]
		[XmlIgnore]
		public string StartTimeString
		{
			get
			{
				return this.StartTime.ToIso8061();
			}
			set
			{
				this.StartTime = DateTime.Parse(value);
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x0001CFE6 File Offset: 0x0001B1E6
		// (set) Token: 0x06000685 RID: 1669 RVA: 0x0001CFF3 File Offset: 0x0001B1F3
		[DataMember(Name = "EndTime", IsRequired = true)]
		[XmlIgnore]
		public string EndTimeString
		{
			get
			{
				return this.EndTime.ToIso8061();
			}
			set
			{
				this.EndTime = DateTime.Parse(value);
			}
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001D004 File Offset: 0x0001B204
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Start={0}, End={1}", new object[]
			{
				this.startTime,
				this.endTime
			});
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001D044 File Offset: 0x0001B244
		internal void Validate(string propertyName)
		{
			if (this.startTime >= this.EndTime)
			{
				throw new InvalidTimeIntervalException(propertyName);
			}
		}

		// Token: 0x040003E7 RID: 999
		private DateTime startTime;

		// Token: 0x040003E8 RID: 1000
		private DateTime endTime;
	}
}
