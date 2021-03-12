using System;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000A04 RID: 2564
	internal class ResourceMetricPolicy
	{
		// Token: 0x060076A4 RID: 30372 RVA: 0x00186C6F File Offset: 0x00184E6F
		public ResourceMetricPolicy(ResourceMetricType type, WorkloadClassification classification) : this(type, classification, VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null))
		{
		}

		// Token: 0x060076A5 RID: 30373 RVA: 0x00186C88 File Offset: 0x00184E88
		public ResourceMetricPolicy(ResourceMetricType type, WorkloadClassification classification, VariantConfigurationSnapshot config) : this(type, classification, config.WorkloadManagement.GetObject<IResourceSettings>(type, new object[0]))
		{
		}

		// Token: 0x060076A6 RID: 30374 RVA: 0x00186CB8 File Offset: 0x00184EB8
		public ResourceMetricPolicy(ResourceMetricType metricType, WorkloadClassification classification, IResourceSettings settings)
		{
			this.MetricType = metricType;
			this.Classification = classification;
			switch (classification)
			{
			case WorkloadClassification.Discretionary:
				this.UnderloadedThreshold = settings.DiscretionaryUnderloaded;
				this.OverloadedThreshold = settings.DiscretionaryOverloaded;
				this.CriticalThreshold = settings.DiscretionaryCritical;
				break;
			case WorkloadClassification.InternalMaintenance:
				this.UnderloadedThreshold = settings.InternalMaintenanceUnderloaded;
				this.OverloadedThreshold = settings.InternalMaintenanceOverloaded;
				this.CriticalThreshold = settings.InternalMaintenanceCritical;
				break;
			case WorkloadClassification.CustomerExpectation:
				this.UnderloadedThreshold = settings.CustomerExpectationUnderloaded;
				this.OverloadedThreshold = settings.CustomerExpectationOverloaded;
				this.CriticalThreshold = settings.CustomerExpectationCritical;
				break;
			case WorkloadClassification.Urgent:
				this.UnderloadedThreshold = settings.UrgentUnderloaded;
				this.OverloadedThreshold = settings.UrgentOverloaded;
				this.CriticalThreshold = settings.UrgentCritical;
				break;
			}
			this.Validate();
		}

		// Token: 0x17002A73 RID: 10867
		// (get) Token: 0x060076A7 RID: 30375 RVA: 0x00186D94 File Offset: 0x00184F94
		// (set) Token: 0x060076A8 RID: 30376 RVA: 0x00186D9C File Offset: 0x00184F9C
		public ResourceMetricType MetricType { get; private set; }

		// Token: 0x17002A74 RID: 10868
		// (get) Token: 0x060076A9 RID: 30377 RVA: 0x00186DA5 File Offset: 0x00184FA5
		// (set) Token: 0x060076AA RID: 30378 RVA: 0x00186DAD File Offset: 0x00184FAD
		public WorkloadClassification Classification { get; private set; }

		// Token: 0x17002A75 RID: 10869
		// (get) Token: 0x060076AB RID: 30379 RVA: 0x00186DB6 File Offset: 0x00184FB6
		// (set) Token: 0x060076AC RID: 30380 RVA: 0x00186DBE File Offset: 0x00184FBE
		public int UnderloadedThreshold { get; private set; }

		// Token: 0x17002A76 RID: 10870
		// (get) Token: 0x060076AD RID: 30381 RVA: 0x00186DC7 File Offset: 0x00184FC7
		// (set) Token: 0x060076AE RID: 30382 RVA: 0x00186DCF File Offset: 0x00184FCF
		public int OverloadedThreshold { get; private set; }

		// Token: 0x17002A77 RID: 10871
		// (get) Token: 0x060076AF RID: 30383 RVA: 0x00186DD8 File Offset: 0x00184FD8
		// (set) Token: 0x060076B0 RID: 30384 RVA: 0x00186DE0 File Offset: 0x00184FE0
		public int CriticalThreshold { get; private set; }

		// Token: 0x060076B1 RID: 30385 RVA: 0x00186DEC File Offset: 0x00184FEC
		public ResourceLoad InterpretMetricValue(int value)
		{
			ResourceLoad unknown;
			if (value < 0)
			{
				unknown = ResourceLoad.Unknown;
			}
			else if (value > this.CriticalThreshold)
			{
				unknown = new ResourceLoad(ResourceLoad.Critical.LoadRatio, new int?(value), null);
			}
			else if (value > this.OverloadedThreshold)
			{
				unknown = new ResourceLoad((double)value / (double)this.UnderloadedThreshold, new int?(value), null);
			}
			else if (value >= this.UnderloadedThreshold)
			{
				unknown = new ResourceLoad(ResourceLoad.Full.LoadRatio, new int?(value), null);
			}
			else
			{
				unknown = new ResourceLoad((double)value / (double)this.UnderloadedThreshold, new int?(value), null);
			}
			return unknown;
		}

		// Token: 0x17002A78 RID: 10872
		// (get) Token: 0x060076B2 RID: 30386 RVA: 0x00186E90 File Offset: 0x00185090
		public ResourceLoad MaxOverloaded
		{
			get
			{
				return new ResourceLoad(Math.Max((double)this.CriticalThreshold / (double)this.UnderloadedThreshold, 5.0), null, null);
			}
		}

		// Token: 0x060076B3 RID: 30387 RVA: 0x00186ECC File Offset: 0x001850CC
		public override string ToString()
		{
			return string.Format("{0}:{1}:{2}:{3}:{4}", new object[]
			{
				this.MetricType,
				this.Classification,
				this.UnderloadedThreshold,
				this.OverloadedThreshold,
				this.CriticalThreshold
			});
		}

		// Token: 0x060076B4 RID: 30388 RVA: 0x00186F31 File Offset: 0x00185131
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ResourceMetricPolicy);
		}

		// Token: 0x060076B5 RID: 30389 RVA: 0x00186F40 File Offset: 0x00185140
		public bool Equals(ResourceMetricPolicy policy)
		{
			return policy != null && this.MetricType == policy.MetricType && this.Classification == policy.Classification && this.UnderloadedThreshold == policy.UnderloadedThreshold && this.OverloadedThreshold == policy.OverloadedThreshold && this.CriticalThreshold == policy.CriticalThreshold;
		}

		// Token: 0x060076B6 RID: 30390 RVA: 0x00186F98 File Offset: 0x00185198
		public override int GetHashCode()
		{
			return (int)(this.MetricType ^ (ResourceMetricType)this.Classification ^ (ResourceMetricType)this.UnderloadedThreshold ^ (ResourceMetricType)this.OverloadedThreshold ^ (ResourceMetricType)this.CriticalThreshold);
		}

		// Token: 0x060076B7 RID: 30391 RVA: 0x00186FBC File Offset: 0x001851BC
		public XElement GetDiagnosticInfo()
		{
			XElement xelement = new XElement("ResourceMetricPolicy");
			xelement.Add(new XElement("MetricType", this.MetricType));
			xelement.Add(new XElement("Classification", this.Classification));
			xelement.Add(new XElement("UnderloadedThreshold", this.UnderloadedThreshold));
			xelement.Add(new XElement("OverloadedThreshold", this.OverloadedThreshold));
			xelement.Add(new XElement("CriticalThreshold", this.CriticalThreshold));
			return xelement;
		}

		// Token: 0x060076B8 RID: 30392 RVA: 0x0018707C File Offset: 0x0018527C
		private void Validate()
		{
			if (this.UnderloadedThreshold <= 0)
			{
				throw new InvalidResourceThresholdException(DirectoryStrings.InvalidNonPositiveResourceThreshold(this.Classification.ToString(), "Underloaded", this.UnderloadedThreshold));
			}
			if (this.OverloadedThreshold <= 0)
			{
				throw new InvalidResourceThresholdException(DirectoryStrings.InvalidNonPositiveResourceThreshold(this.Classification.ToString(), "Overloaded", this.OverloadedThreshold));
			}
			if (this.CriticalThreshold <= 0)
			{
				throw new InvalidResourceThresholdException(DirectoryStrings.InvalidNonPositiveResourceThreshold(this.Classification.ToString(), "Critical", this.CriticalThreshold));
			}
			if (this.UnderloadedThreshold > this.OverloadedThreshold)
			{
				throw new InvalidResourceThresholdException(DirectoryStrings.InvalidBiggerResourceThreshold(this.Classification.ToString(), "Underloaded", "Overloaded", this.UnderloadedThreshold, this.OverloadedThreshold));
			}
			if (this.OverloadedThreshold > this.CriticalThreshold)
			{
				throw new InvalidResourceThresholdException(DirectoryStrings.InvalidBiggerResourceThreshold(this.Classification.ToString(), "Overloaded", "Critical", this.OverloadedThreshold, this.CriticalThreshold));
			}
		}

		// Token: 0x04004BF5 RID: 19445
		private const string ProcessAccessManagerComponentName = "ResourceMetricPolicy";
	}
}
