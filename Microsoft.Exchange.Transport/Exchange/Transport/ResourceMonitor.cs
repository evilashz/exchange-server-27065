using System;
using System.Xml.Linq;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000013 RID: 19
	internal abstract class ResourceMonitor
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00002DD4 File Offset: 0x00000FD4
		protected ResourceMonitor(string displayName, ResourceManagerConfiguration.ResourceMonitorConfiguration configuration)
		{
			if (displayName == null)
			{
				throw new ArgumentNullException("displayName");
			}
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			this.displayName = displayName;
			this.Configuration = configuration;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002E29 File Offset: 0x00001029
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002E31 File Offset: 0x00001031
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			protected set
			{
				this.displayName = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002E3A File Offset: 0x0000103A
		public int CurrentPressure
		{
			get
			{
				return this.currentPressure;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002E42 File Offset: 0x00001042
		public virtual int CurrentPressureRaw
		{
			get
			{
				return this.currentPressure;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002E4A File Offset: 0x0000104A
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00002E52 File Offset: 0x00001052
		public int HighPressureLimit
		{
			get
			{
				return this.highPercentageResourceUsedLimit;
			}
			protected set
			{
				this.highPercentageResourceUsedLimit = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002E5B File Offset: 0x0000105B
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00002E63 File Offset: 0x00001063
		public int MediumPressureLimit
		{
			get
			{
				return this.mediumPercentageResourceUsedLimit;
			}
			protected set
			{
				this.mediumPercentageResourceUsedLimit = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002E6C File Offset: 0x0000106C
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002E74 File Offset: 0x00001074
		public int LowPressureLimit
		{
			get
			{
				return this.lowPercentageResourceUsedLimit;
			}
			protected set
			{
				this.lowPercentageResourceUsedLimit = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002E80 File Offset: 0x00001080
		public virtual ResourceUses ResourceUses
		{
			get
			{
				ResourceUses result = ResourceUses.Normal;
				if (this.currentPressure >= this.HighPressureLimit || (this.currentPressure > this.MediumPressureLimit && this.previousResourceUses == ResourceUses.High))
				{
					result = ResourceUses.High;
				}
				else if (this.currentPressure >= this.MediumPressureLimit || (this.currentPressure > this.LowPressureLimit && this.previousResourceUses > ResourceUses.Normal))
				{
					result = ResourceUses.Medium;
				}
				return result;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002EE0 File Offset: 0x000010E0
		public virtual ResourceUses CurrentResourceUsesRaw
		{
			get
			{
				return this.ResourceUses;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002EE8 File Offset: 0x000010E8
		public ResourceUses PreviousResourceUses
		{
			get
			{
				return this.previousResourceUses;
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002EF0 File Offset: 0x000010F0
		public virtual void UpdateReading()
		{
			ResourceUses resourceUses = this.ResourceUses;
			int num;
			if (this.GetCurrentReading(out num))
			{
				this.previousResourceUses = resourceUses;
				this.currentPressure = num;
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002F1C File Offset: 0x0000111C
		public virtual void UpdateConfig()
		{
			this.HighPressureLimit = this.Configuration.HighThreshold;
			this.MediumPressureLimit = this.Configuration.MediumThreshold;
			this.LowPressureLimit = this.Configuration.NormalThreshold;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002F51 File Offset: 0x00001151
		public virtual void DoCleanup()
		{
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002F54 File Offset: 0x00001154
		public virtual XElement GetDiagnosticInfo(bool verbose)
		{
			XElement xelement = new XElement("Configuration");
			this.Configuration.AddDiagnosticInfo(xelement);
			return new XElement("ResourceMonitor", new object[]
			{
				new XElement("type", base.GetType().Name),
				new XElement("displayName", this.displayName),
				new XElement("currentPressure", this.currentPressure),
				new XElement("resourceUses", this.ResourceUses),
				new XElement("lowPressureLimit", this.LowPressureLimit),
				new XElement("mediumPressureLimit", this.MediumPressureLimit),
				new XElement("highPressureLimit", this.HighPressureLimit),
				xelement
			});
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000305E File Offset: 0x0000125E
		public virtual string ToString(ResourceUses resourceUses, int currentPressure)
		{
			return Strings.ResourceUses(this.displayName, currentPressure, ResourceManager.MapToLocalizedString(resourceUses), this.LowPressureLimit, this.MediumPressureLimit, this.HighPressureLimit);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003089 File Offset: 0x00001289
		public override string ToString()
		{
			return this.ToString(this.ResourceUses, this.currentPressure);
		}

		// Token: 0x06000079 RID: 121
		protected abstract bool GetCurrentReading(out int currentReading);

		// Token: 0x0400002E RID: 46
		protected readonly ResourceManagerConfiguration.ResourceMonitorConfiguration Configuration;

		// Token: 0x0400002F RID: 47
		private int highPercentageResourceUsedLimit = 90;

		// Token: 0x04000030 RID: 48
		private int mediumPercentageResourceUsedLimit = 80;

		// Token: 0x04000031 RID: 49
		private int lowPercentageResourceUsedLimit = 70;

		// Token: 0x04000032 RID: 50
		private int currentPressure;

		// Token: 0x04000033 RID: 51
		private ResourceUses previousResourceUses;

		// Token: 0x04000034 RID: 52
		private string displayName;
	}
}
