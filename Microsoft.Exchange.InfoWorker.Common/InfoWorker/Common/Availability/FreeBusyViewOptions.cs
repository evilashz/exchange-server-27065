using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000FF RID: 255
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FreeBusyViewOptions
	{
		// Token: 0x060006D3 RID: 1747 RVA: 0x0001E2F2 File Offset: 0x0001C4F2
		public FreeBusyViewOptions()
		{
			this.Init();
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001E300 File Offset: 0x0001C500
		public FreeBusyViewOptions(Duration timeWindow, int mergedFreeBusyIntervalInMinutes, FreeBusyViewType requestedView)
		{
			this.Init();
			this.timeWindow = timeWindow;
			this.mergedFreeBusyIntervalInMinutes = mergedFreeBusyIntervalInMinutes;
			this.requestedView = requestedView;
			this.Validate();
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x0001E329 File Offset: 0x0001C529
		// (set) Token: 0x060006D6 RID: 1750 RVA: 0x0001E331 File Offset: 0x0001C531
		[DataMember]
		public Duration TimeWindow
		{
			get
			{
				return this.timeWindow;
			}
			set
			{
				this.timeWindow = value;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0001E33A File Offset: 0x0001C53A
		// (set) Token: 0x060006D8 RID: 1752 RVA: 0x0001E342 File Offset: 0x0001C542
		[DataMember]
		public int MergedFreeBusyIntervalInMinutes
		{
			get
			{
				return this.mergedFreeBusyIntervalInMinutes;
			}
			set
			{
				this.mergedFreeBusyIntervalInMinutes = value;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0001E34B File Offset: 0x0001C54B
		// (set) Token: 0x060006DA RID: 1754 RVA: 0x0001E353 File Offset: 0x0001C553
		[IgnoreDataMember]
		public FreeBusyViewType RequestedView
		{
			get
			{
				return this.requestedView;
			}
			set
			{
				this.requestedView = value;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0001E35C File Offset: 0x0001C55C
		// (set) Token: 0x060006DC RID: 1756 RVA: 0x0001E369 File Offset: 0x0001C569
		[XmlIgnore]
		[DataMember(Name = "RequestedView")]
		public string RequestedViewString
		{
			get
			{
				return EnumUtil.ToString<FreeBusyViewType>(this.RequestedView);
			}
			set
			{
				this.RequestedView = EnumUtil.Parse<FreeBusyViewType>(value);
			}
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0001E377 File Offset: 0x0001C577
		internal static bool IsMerged(FreeBusyViewType viewType)
		{
			return (viewType & FreeBusyViewType.MergedOnly) > FreeBusyViewType.None;
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001E380 File Offset: 0x0001C580
		internal static FreeBusyViewOptions CreateDefaultForMeetingSuggestions(Duration timeWindow)
		{
			return new FreeBusyViewOptions(timeWindow, 30, FreeBusyViewType.MergedOnly);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0001E398 File Offset: 0x0001C598
		internal void Validate()
		{
			if (this.requestedView == FreeBusyViewType.None)
			{
				throw new InvalidFreeBusyViewTypeException();
			}
			FreeBusyViewOptions.IsMerged(this.requestedView);
			if (this.requestedView == FreeBusyViewType.None)
			{
				throw new ArgumentException("RequestedView can not be None", "RequestedView");
			}
			if (this.mergedFreeBusyIntervalInMinutes < 5 || this.mergedFreeBusyIntervalInMinutes > 1440)
			{
				throw new InvalidMergedFreeBusyIntervalException(5, 1440);
			}
			if (this.timeWindow == null)
			{
				throw new MissingArgumentException(Strings.descMissingArgument("FreeBusyViewOptions.TimeWindow"));
			}
			this.timeWindow.Validate("FreeBusyViewOptions.TimeWindow");
			TimeSpan timeSpan = this.timeWindow.EndTime.Subtract(this.timeWindow.StartTime);
			if (timeSpan.Days > Configuration.MaximumQueryIntervalDays)
			{
				throw new TimeIntervalTooBigException("FreeBusyViewOptions.TimeWindow", Configuration.MaximumQueryIntervalDays, timeSpan.Days);
			}
			if (timeSpan.TotalMinutes < (double)this.mergedFreeBusyIntervalInMinutes)
			{
				throw new InvalidMergedFreeBusyIntervalException(5, 1440);
			}
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001E481 File Offset: 0x0001C681
		[OnDeserializing]
		private void Init(StreamingContext context)
		{
			this.Init();
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0001E489 File Offset: 0x0001C689
		private void Init()
		{
			this.mergedFreeBusyIntervalInMinutes = 30;
			this.requestedView = FreeBusyViewType.FreeBusy;
		}

		// Token: 0x04000411 RID: 1041
		internal const int MergedFreeBusyFlag = 1;

		// Token: 0x04000412 RID: 1042
		private Duration timeWindow;

		// Token: 0x04000413 RID: 1043
		private int mergedFreeBusyIntervalInMinutes;

		// Token: 0x04000414 RID: 1044
		private FreeBusyViewType requestedView;
	}
}
