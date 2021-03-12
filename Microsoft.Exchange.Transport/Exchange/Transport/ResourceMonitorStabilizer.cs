using System;
using System.Xml.Linq;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200004D RID: 77
	internal class ResourceMonitorStabilizer : ResourceMonitor
	{
		// Token: 0x060001EC RID: 492 RVA: 0x00009376 File Offset: 0x00007576
		public ResourceMonitorStabilizer(ResourceMonitor resourceMonitor, ResourceManagerConfiguration.StabilizedResourceMonitorConfiguration config) : base("ResourceMonitorStabilizer", config)
		{
			this.resourceMonitor = resourceMonitor;
			this.historyState = new ResourceMonitorStabilizer.HistoryState(config.HistoryDepth);
			base.DisplayName = this.resourceMonitor.DisplayName;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001ED RID: 493 RVA: 0x000093AD File Offset: 0x000075AD
		public override ResourceUses ResourceUses
		{
			get
			{
				return this.historyState.ResourceUses;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001EE RID: 494 RVA: 0x000093BA File Offset: 0x000075BA
		public override int CurrentPressureRaw
		{
			get
			{
				return this.historyState.CurrentPressureRaw;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001EF RID: 495 RVA: 0x000093C7 File Offset: 0x000075C7
		public override ResourceUses CurrentResourceUsesRaw
		{
			get
			{
				return this.historyState.CurrentResourceUsesRaw;
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000093D4 File Offset: 0x000075D4
		protected override bool GetCurrentReading(out int currentReading)
		{
			this.resourceMonitor.UpdateReading();
			this.historyState.AddReading(this.resourceMonitor.ResourceUses, this.resourceMonitor.CurrentPressure);
			currentReading = this.historyState.CurrentPressure;
			return true;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00009410 File Offset: 0x00007610
		public override void DoCleanup()
		{
			this.resourceMonitor.DoCleanup();
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00009420 File Offset: 0x00007620
		public override XElement GetDiagnosticInfo(bool verbose)
		{
			XElement diagnosticInfo = this.resourceMonitor.GetDiagnosticInfo(verbose);
			diagnosticInfo.Add(this.historyState.GetDiagnosticInfo(verbose));
			return diagnosticInfo;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000944D File Offset: 0x0000764D
		public override string ToString()
		{
			return this.resourceMonitor.ToString(this.ResourceUses, base.CurrentPressure);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00009468 File Offset: 0x00007668
		public override void UpdateConfig()
		{
			int historyDepth = this.historyState.HistoryDepth;
			this.resourceMonitor.UpdateConfig();
			base.HighPressureLimit = this.resourceMonitor.HighPressureLimit;
			base.MediumPressureLimit = this.resourceMonitor.MediumPressureLimit;
			base.LowPressureLimit = this.resourceMonitor.LowPressureLimit;
			this.historyState = new ResourceMonitorStabilizer.HistoryState(historyDepth);
		}

		// Token: 0x04000128 RID: 296
		private ResourceMonitor resourceMonitor;

		// Token: 0x04000129 RID: 297
		private ResourceMonitorStabilizer.HistoryState historyState;

		// Token: 0x0200004E RID: 78
		private sealed class HistoryState
		{
			// Token: 0x060001F5 RID: 501 RVA: 0x000094CC File Offset: 0x000076CC
			public HistoryState(int historyDepth)
			{
				if (historyDepth < 1)
				{
					throw new ArgumentException("historyDepth can't be smaller than 1.");
				}
				this.measurementHistory = new ResourceMonitorStabilizer.HistoryState.Measurement[historyDepth];
				for (int i = 0; i < historyDepth; i++)
				{
					this.measurementHistory[i] = new ResourceMonitorStabilizer.HistoryState.Measurement();
				}
				this.measurement = this.measurementHistory[0];
				this.currentResourceUsesRaw = ResourceUses.Normal;
			}

			// Token: 0x17000085 RID: 133
			// (get) Token: 0x060001F6 RID: 502 RVA: 0x00009528 File Offset: 0x00007728
			public int HistoryDepth
			{
				get
				{
					return this.measurementHistory.Length;
				}
			}

			// Token: 0x17000086 RID: 134
			// (get) Token: 0x060001F7 RID: 503 RVA: 0x00009532 File Offset: 0x00007732
			public ResourceUses ResourceUses
			{
				get
				{
					return this.measurement.ResourceUses;
				}
			}

			// Token: 0x17000087 RID: 135
			// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000953F File Offset: 0x0000773F
			public ResourceUses CurrentResourceUsesRaw
			{
				get
				{
					return this.currentResourceUsesRaw;
				}
			}

			// Token: 0x17000088 RID: 136
			// (get) Token: 0x060001F9 RID: 505 RVA: 0x00009547 File Offset: 0x00007747
			public int CurrentPressure
			{
				get
				{
					return this.measurement.Pressure;
				}
			}

			// Token: 0x17000089 RID: 137
			// (get) Token: 0x060001FA RID: 506 RVA: 0x00009554 File Offset: 0x00007754
			public int CurrentPressureRaw
			{
				get
				{
					return this.currentPressureRaw;
				}
			}

			// Token: 0x060001FB RID: 507 RVA: 0x0000955C File Offset: 0x0000775C
			public void AddReading(ResourceUses resourceUses, int pressure)
			{
				this.currentPressureRaw = pressure;
				this.currentResourceUsesRaw = resourceUses;
				this.measurementHistory[this.currentIndex] = new ResourceMonitorStabilizer.HistoryState.Measurement(resourceUses, pressure);
				this.currentIndex = (this.currentIndex + 1) % this.HistoryDepth;
				this.CalculateStabilizedState();
			}

			// Token: 0x060001FC RID: 508 RVA: 0x0000959C File Offset: 0x0000779C
			public XElement GetDiagnosticInfo(bool verbose)
			{
				XElement xelement = new XElement("HistoricalMeasurementsCollection", new XElement("count", this.HistoryDepth));
				if (verbose)
				{
					for (int i = 0; i < this.HistoryDepth; i++)
					{
						ResourceMonitorStabilizer.HistoryState.Measurement measurement = this.measurementHistory[this.MapLogicalToPhysicalIndex(i)];
						xelement.Add(measurement.GetDiagnosticInfo());
					}
				}
				else
				{
					xelement.Add(new XElement("help", "Use 'verbose' to get the measurements."));
				}
				return xelement;
			}

			// Token: 0x060001FD RID: 509 RVA: 0x00009620 File Offset: 0x00007820
			private int MapLogicalToPhysicalIndex(int index)
			{
				int historyDepth = this.HistoryDepth;
				if (index < 0 || index >= historyDepth)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return (index + this.currentIndex) % historyDepth;
			}

			// Token: 0x060001FE RID: 510 RVA: 0x00009654 File Offset: 0x00007854
			private void CalculateStabilizedState()
			{
				ResourceMonitorStabilizer.HistoryState.Measurement measurement = new ResourceMonitorStabilizer.HistoryState.Measurement(ResourceUses.High, int.MaxValue);
				for (int i = 0; i < this.HistoryDepth; i++)
				{
					ResourceMonitorStabilizer.HistoryState.Measurement measurement2 = this.measurementHistory[this.MapLogicalToPhysicalIndex(i)];
					if (measurement2.ResourceUses < measurement.ResourceUses || measurement2.Pressure < measurement.Pressure)
					{
						measurement = measurement2;
					}
				}
				this.measurement = measurement;
			}

			// Token: 0x0400012A RID: 298
			private ResourceMonitorStabilizer.HistoryState.Measurement[] measurementHistory;

			// Token: 0x0400012B RID: 299
			private int currentIndex;

			// Token: 0x0400012C RID: 300
			private ResourceMonitorStabilizer.HistoryState.Measurement measurement;

			// Token: 0x0400012D RID: 301
			private int currentPressureRaw;

			// Token: 0x0400012E RID: 302
			private ResourceUses currentResourceUsesRaw;

			// Token: 0x0200004F RID: 79
			private sealed class Measurement
			{
				// Token: 0x060001FF RID: 511 RVA: 0x000096B2 File Offset: 0x000078B2
				public Measurement() : this(ResourceUses.Normal, 0)
				{
				}

				// Token: 0x06000200 RID: 512 RVA: 0x000096BC File Offset: 0x000078BC
				public Measurement(ResourceUses resourceUses, int pressure)
				{
					this.resourceUses = resourceUses;
					this.pressure = pressure;
				}

				// Token: 0x1700008A RID: 138
				// (get) Token: 0x06000201 RID: 513 RVA: 0x000096D2 File Offset: 0x000078D2
				public ResourceUses ResourceUses
				{
					get
					{
						return this.resourceUses;
					}
				}

				// Token: 0x1700008B RID: 139
				// (get) Token: 0x06000202 RID: 514 RVA: 0x000096DA File Offset: 0x000078DA
				public int Pressure
				{
					get
					{
						return this.pressure;
					}
				}

				// Token: 0x06000203 RID: 515 RVA: 0x000096E4 File Offset: 0x000078E4
				public XElement GetDiagnosticInfo()
				{
					return new XElement("Measurement", new object[]
					{
						new XElement("pressureRaw", this.pressure),
						new XElement("resourceUsesRaw", this.resourceUses)
					});
				}

				// Token: 0x0400012F RID: 303
				private readonly ResourceUses resourceUses;

				// Token: 0x04000130 RID: 304
				private readonly int pressure;
			}
		}
	}
}
