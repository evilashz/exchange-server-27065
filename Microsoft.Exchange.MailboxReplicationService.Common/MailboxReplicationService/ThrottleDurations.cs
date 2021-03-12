using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001AA RID: 426
	[Serializable]
	public class ThrottleDurations : XMLSerializableBase
	{
		// Token: 0x06001009 RID: 4105 RVA: 0x00025EA5 File Offset: 0x000240A5
		public ThrottleDurations()
		{
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x00025EAD File Offset: 0x000240AD
		public ThrottleDurations(TimeSpan mdb, TimeSpan cpu, TimeSpan mdbRepl, TimeSpan contentIndexing, TimeSpan unk)
		{
			this.MdbThrottle = mdb;
			this.CpuThrottle = cpu;
			this.MdbReplicationThrottle = mdbRepl;
			this.ContentIndexingThrottle = contentIndexing;
			this.UnknownThrottle = unk;
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x00025EDA File Offset: 0x000240DA
		// (set) Token: 0x0600100C RID: 4108 RVA: 0x00025EE2 File Offset: 0x000240E2
		[XmlIgnore]
		public TimeSpan MdbThrottle { get; private set; }

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x00025EEB File Offset: 0x000240EB
		// (set) Token: 0x0600100E RID: 4110 RVA: 0x00025EF3 File Offset: 0x000240F3
		[XmlIgnore]
		public TimeSpan CpuThrottle { get; private set; }

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x00025EFC File Offset: 0x000240FC
		// (set) Token: 0x06001010 RID: 4112 RVA: 0x00025F04 File Offset: 0x00024104
		[XmlIgnore]
		public TimeSpan MdbReplicationThrottle { get; private set; }

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001011 RID: 4113 RVA: 0x00025F0D File Offset: 0x0002410D
		// (set) Token: 0x06001012 RID: 4114 RVA: 0x00025F15 File Offset: 0x00024115
		[XmlIgnore]
		public TimeSpan ContentIndexingThrottle { get; private set; }

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001013 RID: 4115 RVA: 0x00025F1E File Offset: 0x0002411E
		// (set) Token: 0x06001014 RID: 4116 RVA: 0x00025F26 File Offset: 0x00024126
		[XmlIgnore]
		public TimeSpan UnknownThrottle { get; private set; }

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x00025F30 File Offset: 0x00024130
		// (set) Token: 0x06001016 RID: 4118 RVA: 0x00025F4B File Offset: 0x0002414B
		[XmlAttribute("MdbThrottle")]
		public long MdbThrottleTicks
		{
			get
			{
				return this.MdbThrottle.Ticks;
			}
			set
			{
				this.MdbThrottle = new TimeSpan(value);
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001017 RID: 4119 RVA: 0x00025F5C File Offset: 0x0002415C
		// (set) Token: 0x06001018 RID: 4120 RVA: 0x00025F77 File Offset: 0x00024177
		[XmlAttribute("CpuThrottle")]
		public long CpuThrottleTicks
		{
			get
			{
				return this.CpuThrottle.Ticks;
			}
			set
			{
				this.CpuThrottle = new TimeSpan(value);
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x00025F88 File Offset: 0x00024188
		// (set) Token: 0x0600101A RID: 4122 RVA: 0x00025FA3 File Offset: 0x000241A3
		[XmlAttribute("MdbReplicationThrottle")]
		public long MdbReplicationThrottleTicks
		{
			get
			{
				return this.MdbReplicationThrottle.Ticks;
			}
			set
			{
				this.MdbReplicationThrottle = new TimeSpan(value);
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x00025FB4 File Offset: 0x000241B4
		// (set) Token: 0x0600101C RID: 4124 RVA: 0x00025FCF File Offset: 0x000241CF
		[XmlAttribute("ContentIndexingThrottle")]
		public long ContentIndexingThrottleTicks
		{
			get
			{
				return this.ContentIndexingThrottle.Ticks;
			}
			set
			{
				this.ContentIndexingThrottle = new TimeSpan(value);
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x0600101D RID: 4125 RVA: 0x00025FE0 File Offset: 0x000241E0
		// (set) Token: 0x0600101E RID: 4126 RVA: 0x00025FFB File Offset: 0x000241FB
		[XmlAttribute("UnknownThrottle")]
		public long UnknownThrottleTicks
		{
			get
			{
				return this.UnknownThrottle.Ticks;
			}
			set
			{
				this.UnknownThrottle = new TimeSpan(value);
			}
		}
	}
}
