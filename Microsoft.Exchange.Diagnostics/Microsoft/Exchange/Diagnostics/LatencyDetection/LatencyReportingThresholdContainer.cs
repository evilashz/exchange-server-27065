using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Exchange.Diagnostics.LatencyDetection
{
	// Token: 0x02000176 RID: 374
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class LatencyReportingThresholdContainer
	{
		// Token: 0x06000A99 RID: 2713 RVA: 0x0002764C File Offset: 0x0002584C
		private LatencyReportingThresholdContainer()
		{
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x0002765F File Offset: 0x0002585F
		public static LatencyReportingThresholdContainer Instance
		{
			get
			{
				return LatencyReportingThresholdContainer.singletonInstance;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x00027666 File Offset: 0x00025866
		internal IDictionary<string, LatencyDetectionLocation> Locations
		{
			get
			{
				return this.locationsByName;
			}
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00027670 File Offset: 0x00025870
		public void Clear()
		{
			foreach (LatencyDetectionLocation latencyDetectionLocation in this.Locations.Values)
			{
				latencyDetectionLocation.ClearThresholds();
			}
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x000276C4 File Offset: 0x000258C4
		public void SetThreshold(string identity, LoggingType type, TimeSpan threshold)
		{
			if (string.IsNullOrEmpty(identity))
			{
				throw new ArgumentNullException("identity");
			}
			LatencyDetectionLocation latencyDetectionLocation;
			if (this.locationsByName.TryGetValue(identity, out latencyDetectionLocation))
			{
				latencyDetectionLocation.SetThreshold(type, threshold);
				return;
			}
			throw new ArgumentException("Not the id of an existing location.", "identity");
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00027710 File Offset: 0x00025910
		public TimeSpan GetThreshold(string identity, LoggingType type)
		{
			if (string.IsNullOrEmpty(identity))
			{
				throw new ArgumentException("Should not be null or empty.", "identity");
			}
			LatencyDetectionLocation latencyDetectionLocation = null;
			if (this.locationsByName.TryGetValue(identity, out latencyDetectionLocation))
			{
				return latencyDetectionLocation.GetThreshold(type).Threshold;
			}
			throw new ArgumentException("Not the id of an existing location.", "identity");
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00027764 File Offset: 0x00025964
		internal void ValidateLocation(LatencyDetectionLocation location)
		{
			if (location == null)
			{
				throw new ArgumentNullException("location");
			}
			string identity = location.Identity;
			TimeSpan minimumThreshold = location.MinimumThreshold;
			if (location.MinimumThreshold < LatencyReportingThreshold.MinimumThresholdValue)
			{
				string message = string.Format(CultureInfo.InvariantCulture, "Minimum threshold for location with identity \"{0}\", {1}, is below the allowed minimum of {2}.", new object[]
				{
					identity,
					minimumThreshold,
					LatencyReportingThreshold.MinimumThresholdValue
				});
				throw new ArgumentException(message, "location");
			}
			LatencyDetectionLocation latencyDetectionLocation;
			if (this.locationsByName.TryGetValue(identity, out latencyDetectionLocation))
			{
				if (location != latencyDetectionLocation)
				{
					string message2 = string.Format(CultureInfo.InvariantCulture, "More than one {0} found with Identity = \"{1}\"", new object[]
					{
						typeof(LatencyDetectionLocation).FullName,
						identity
					});
					throw new ArgumentException(message2, "location");
				}
			}
			else
			{
				this.locationsByName[identity] = location;
			}
		}

		// Token: 0x04000742 RID: 1858
		private static LatencyReportingThresholdContainer singletonInstance = new LatencyReportingThresholdContainer();

		// Token: 0x04000743 RID: 1859
		private readonly IDictionary<string, LatencyDetectionLocation> locationsByName = new Dictionary<string, LatencyDetectionLocation>();
	}
}
