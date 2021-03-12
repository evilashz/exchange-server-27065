using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200021F RID: 543
	[DataContract]
	[Serializable]
	public sealed class TransferProgressTracker : XMLSerializableBase
	{
		// Token: 0x06001D58 RID: 7512 RVA: 0x0003CA1C File Offset: 0x0003AC1C
		public TransferProgressTracker()
		{
			this.lastMinuteBytes = new FixedTimeSumProgress(1000U, 60);
			this.perMinuteBytes = new FixedTimeSumProgress(60000U, 120);
			this.perMinuteItems = new FixedTimeSumProgress(60000U, 120);
			this.perHourBytes = new FixedTimeSumProgress(3600000U, 120);
			this.perHourItems = new FixedTimeSumProgress(3600000U, 120);
			this.perDayBytes = new FixedTimeSumProgress(86400000U, 90);
			this.perDayItems = new FixedTimeSumProgress(86400000U, 90);
			this.perMonthBytes = new FixedTimeSumProgress(2592000000U, 24);
			this.perMonthItems = new FixedTimeSumProgress(2592000000U, 24);
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x0003CAD1 File Offset: 0x0003ACD1
		private TransferProgressTracker(SerializationInfo info, StreamingContext context) : this()
		{
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06001D5A RID: 7514 RVA: 0x0003CAD9 File Offset: 0x0003ACD9
		// (set) Token: 0x06001D5B RID: 7515 RVA: 0x0003CAE6 File Offset: 0x0003ACE6
		[XmlArrayItem("M")]
		[XmlArray("LastMinuteBytes")]
		[DataMember(Name = "LastMinuteBytes")]
		public FixedTimeSumSlot[] LastMinuteBytes
		{
			get
			{
				return this.lastMinuteBytes.Serialize();
			}
			set
			{
				this.lastMinuteBytes = new FixedTimeSumProgress(1000U, 60, value);
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06001D5C RID: 7516 RVA: 0x0003CAFB File Offset: 0x0003ACFB
		// (set) Token: 0x06001D5D RID: 7517 RVA: 0x0003CB08 File Offset: 0x0003AD08
		[XmlArray("PerMinuteBytes")]
		[XmlArrayItem("M")]
		[DataMember(Name = "PerMinuteBytes")]
		public FixedTimeSumSlot[] PerMinuteBytes
		{
			get
			{
				return this.perMinuteBytes.Serialize();
			}
			set
			{
				this.perMinuteBytes = new FixedTimeSumProgress(60000U, 120, value);
			}
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06001D5E RID: 7518 RVA: 0x0003CB1D File Offset: 0x0003AD1D
		// (set) Token: 0x06001D5F RID: 7519 RVA: 0x0003CB2A File Offset: 0x0003AD2A
		[XmlArray("PerMinuteItems")]
		[XmlArrayItem("M")]
		[DataMember(Name = "PerMinuteItems")]
		public FixedTimeSumSlot[] PerMinuteItems
		{
			get
			{
				return this.perMinuteItems.Serialize();
			}
			set
			{
				this.perMinuteItems = new FixedTimeSumProgress(60000U, 120, value);
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06001D60 RID: 7520 RVA: 0x0003CB3F File Offset: 0x0003AD3F
		// (set) Token: 0x06001D61 RID: 7521 RVA: 0x0003CB4C File Offset: 0x0003AD4C
		[XmlArray("PerHourBytes")]
		[XmlArrayItem("H")]
		[DataMember(Name = "PerHourBytes")]
		public FixedTimeSumSlot[] PerHourBytes
		{
			get
			{
				return this.perHourBytes.Serialize();
			}
			set
			{
				this.perHourBytes = new FixedTimeSumProgress(3600000U, 120, value);
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06001D62 RID: 7522 RVA: 0x0003CB61 File Offset: 0x0003AD61
		// (set) Token: 0x06001D63 RID: 7523 RVA: 0x0003CB6E File Offset: 0x0003AD6E
		[XmlArray("PerHourItems")]
		[DataMember(Name = "PerHourItems")]
		[XmlArrayItem("H")]
		public FixedTimeSumSlot[] PerHourItems
		{
			get
			{
				return this.perHourItems.Serialize();
			}
			set
			{
				this.perHourItems = new FixedTimeSumProgress(3600000U, 120, value);
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06001D64 RID: 7524 RVA: 0x0003CB83 File Offset: 0x0003AD83
		// (set) Token: 0x06001D65 RID: 7525 RVA: 0x0003CB90 File Offset: 0x0003AD90
		[DataMember(Name = "PerDayBytes")]
		[XmlArray("PerDayBytes")]
		[XmlArrayItem("D")]
		public FixedTimeSumSlot[] PerDayBytes
		{
			get
			{
				return this.perDayBytes.Serialize();
			}
			set
			{
				this.perDayBytes = new FixedTimeSumProgress(86400000U, 90, value);
			}
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06001D66 RID: 7526 RVA: 0x0003CBA5 File Offset: 0x0003ADA5
		// (set) Token: 0x06001D67 RID: 7527 RVA: 0x0003CBB2 File Offset: 0x0003ADB2
		[XmlArray("PerDayItems")]
		[DataMember(Name = "PerDayItems")]
		[XmlArrayItem("D")]
		public FixedTimeSumSlot[] PerDayItems
		{
			get
			{
				return this.perDayItems.Serialize();
			}
			set
			{
				this.perDayItems = new FixedTimeSumProgress(86400000U, 90, value);
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x06001D68 RID: 7528 RVA: 0x0003CBC7 File Offset: 0x0003ADC7
		// (set) Token: 0x06001D69 RID: 7529 RVA: 0x0003CBD4 File Offset: 0x0003ADD4
		[XmlArray("PerMonthBytes")]
		[DataMember(Name = "PerMonthBytes")]
		[XmlArrayItem("Mo")]
		public FixedTimeSumSlot[] PerMonthBytes
		{
			get
			{
				return this.perMonthBytes.Serialize();
			}
			set
			{
				this.perMonthBytes = new FixedTimeSumProgress(2592000000U, 24, value);
			}
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x06001D6A RID: 7530 RVA: 0x0003CBE9 File Offset: 0x0003ADE9
		// (set) Token: 0x06001D6B RID: 7531 RVA: 0x0003CBF6 File Offset: 0x0003ADF6
		[DataMember(Name = "PerMonthItems")]
		[XmlArray("PerMonthItems")]
		[XmlArrayItem("Mo")]
		public FixedTimeSumSlot[] PerMonthItems
		{
			get
			{
				return this.perMonthItems.Serialize();
			}
			set
			{
				this.perMonthItems = new FixedTimeSumProgress(2592000000U, 24, value);
			}
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06001D6C RID: 7532 RVA: 0x0003CC0B File Offset: 0x0003AE0B
		[XmlIgnore]
		public ulong BytesPerMinute
		{
			get
			{
				if (this.lastMinuteBytes != null)
				{
					return (ulong)this.lastMinuteBytes.GetValue();
				}
				return 0UL;
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06001D6D RID: 7533 RVA: 0x0003CC24 File Offset: 0x0003AE24
		// (set) Token: 0x06001D6E RID: 7534 RVA: 0x0003CC2C File Offset: 0x0003AE2C
		[DataMember(Name = "BytesTransferred")]
		[XmlElement(ElementName = "BytesTransferred")]
		public ulong BytesTransferred { get; set; }

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06001D6F RID: 7535 RVA: 0x0003CC35 File Offset: 0x0003AE35
		// (set) Token: 0x06001D70 RID: 7536 RVA: 0x0003CC3D File Offset: 0x0003AE3D
		[XmlElement(ElementName = "ItemsTransferred")]
		[DataMember(Name = "ItemsTransferred")]
		public ulong ItemsTransferred { get; set; }

		// Token: 0x06001D71 RID: 7537 RVA: 0x0003CC46 File Offset: 0x0003AE46
		public static TransferProgressTracker operator +(TransferProgressTracker oldTracker, TransferProgressTracker newTracker)
		{
			if (newTracker == null || newTracker.BytesTransferred == 0UL)
			{
				return oldTracker;
			}
			if (oldTracker != null)
			{
				newTracker.BytesTransferred += oldTracker.BytesTransferred;
				newTracker.ItemsTransferred += oldTracker.ItemsTransferred;
			}
			return newTracker;
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x0003CC84 File Offset: 0x0003AE84
		public void AddBytes(uint blockSize)
		{
			this.lastMinuteBytes.Add(blockSize);
			this.perMinuteBytes.Add(blockSize);
			this.perHourBytes.Add(blockSize);
			this.perDayBytes.Add(blockSize);
			this.perMonthBytes.Add(blockSize);
			this.BytesTransferred += (ulong)blockSize;
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x0003CCDC File Offset: 0x0003AEDC
		public void AddItems(uint itemCount)
		{
			this.perMinuteItems.Add(itemCount);
			this.perHourItems.Add(itemCount);
			this.perDayItems.Add(itemCount);
			this.perMonthItems.Add(itemCount);
			this.ItemsTransferred += (ulong)itemCount;
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x0003CD28 File Offset: 0x0003AF28
		internal TransferProgressTrackerXML GetDiagnosticInfo(RequestStatisticsDiagnosticArgument arguments)
		{
			bool showTimeSlots = arguments.HasArgument("showtimeslots");
			return new TransferProgressTrackerXML(this, showTimeSlots);
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x0003CD48 File Offset: 0x0003AF48
		internal TransferProgressTrackerXML GetDiagnosticInfo(MRSDiagnosticArgument arguments)
		{
			bool showTimeSlots = arguments.HasArgument("showtimeslots");
			return new TransferProgressTrackerXML(this, showTimeSlots);
		}

		// Token: 0x04000C2A RID: 3114
		private const uint MillisecondsPerMonth = 2592000000U;

		// Token: 0x04000C2B RID: 3115
		private const uint MillisecondsPerDay = 86400000U;

		// Token: 0x04000C2C RID: 3116
		private const uint MillisecondsPerHour = 3600000U;

		// Token: 0x04000C2D RID: 3117
		private const uint MillisecondsPerMinute = 60000U;

		// Token: 0x04000C2E RID: 3118
		private const uint MillisecondsPerSecond = 1000U;

		// Token: 0x04000C2F RID: 3119
		private const ushort NumberOfMinutes = 120;

		// Token: 0x04000C30 RID: 3120
		private const ushort NumberOfHours = 120;

		// Token: 0x04000C31 RID: 3121
		private const ushort NumberOfDays = 90;

		// Token: 0x04000C32 RID: 3122
		private const ushort NumberOfMonths = 24;

		// Token: 0x04000C33 RID: 3123
		[NonSerialized]
		private FixedTimeSumProgress lastMinuteBytes;

		// Token: 0x04000C34 RID: 3124
		[NonSerialized]
		private FixedTimeSumProgress perMinuteBytes;

		// Token: 0x04000C35 RID: 3125
		[NonSerialized]
		private FixedTimeSumProgress perMinuteItems;

		// Token: 0x04000C36 RID: 3126
		[NonSerialized]
		private FixedTimeSumProgress perHourBytes;

		// Token: 0x04000C37 RID: 3127
		[NonSerialized]
		private FixedTimeSumProgress perHourItems;

		// Token: 0x04000C38 RID: 3128
		[NonSerialized]
		private FixedTimeSumProgress perDayBytes;

		// Token: 0x04000C39 RID: 3129
		[NonSerialized]
		private FixedTimeSumProgress perDayItems;

		// Token: 0x04000C3A RID: 3130
		[NonSerialized]
		private FixedTimeSumProgress perMonthBytes;

		// Token: 0x04000C3B RID: 3131
		[NonSerialized]
		private FixedTimeSumProgress perMonthItems;
	}
}
