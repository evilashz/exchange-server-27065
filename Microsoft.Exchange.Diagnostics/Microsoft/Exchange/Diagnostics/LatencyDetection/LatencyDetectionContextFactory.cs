using System;

namespace Microsoft.Exchange.Diagnostics.LatencyDetection
{
	// Token: 0x0200016F RID: 367
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class LatencyDetectionContextFactory
	{
		// Token: 0x06000A6F RID: 2671 RVA: 0x0002706C File Offset: 0x0002526C
		private LatencyDetectionContextFactory(LatencyDetectionLocation factoryLocation)
		{
			this.location = factoryLocation;
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x0002707B File Offset: 0x0002527B
		public string LocationIdentity
		{
			get
			{
				return this.location.Identity;
			}
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00027088 File Offset: 0x00025288
		public static LatencyDetectionContextFactory CreateFactory(string identity, TimeSpan minimumThreshold, TimeSpan defaultThreshold)
		{
			LatencyDetectionLocation factoryLocation = new LatencyDetectionLocation(LatencyDetectionContextFactory.thresholdInitializer, identity, minimumThreshold, defaultThreshold);
			return new LatencyDetectionContextFactory(factoryLocation);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x000270A9 File Offset: 0x000252A9
		public static LatencyDetectionContextFactory CreateFactory(string identity)
		{
			return LatencyDetectionContextFactory.CreateFactory(identity, LatencyReportingThreshold.MinimumThresholdValue, TimeSpan.MaxValue);
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x000270BB File Offset: 0x000252BB
		public LatencyDetectionContext CreateContext(string version)
		{
			return this.CreateContext(version, this.location.Identity, new IPerformanceDataProvider[0]);
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x000270D5 File Offset: 0x000252D5
		public LatencyDetectionContext CreateContext(string version, object hash, params IPerformanceDataProvider[] providers)
		{
			return this.CreateContext(ContextOptions.Default, version, hash, providers);
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x000270E1 File Offset: 0x000252E1
		public LatencyReportingThreshold GetThreshold(LoggingType type)
		{
			return this.location.GetThreshold(type);
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x000270EF File Offset: 0x000252EF
		public LatencyDetectionContext CreateContext(ContextOptions contextOptions, string version, object hash, params IPerformanceDataProvider[] providers)
		{
			LatencyDetectionContext.ValidateBinningParameters(this.location, version, hash);
			return new LatencyDetectionContext(this.location, contextOptions, version, hash, providers);
		}

		// Token: 0x04000729 RID: 1833
		private static readonly IThresholdInitializer thresholdInitializer = PerformanceReportingOptions.Instance;

		// Token: 0x0400072A RID: 1834
		private readonly LatencyDetectionLocation location;
	}
}
