using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Cluster.Replay.MountPoint
{
	// Token: 0x02000239 RID: 569
	internal class ExchangeVolumeDbMountPointsComparer : Comparer<ExchangeVolume>
	{
		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x0600158B RID: 5515 RVA: 0x00055A2F File Offset: 0x00053C2F
		public static ExchangeVolumeDbMountPointsComparer Instance
		{
			get
			{
				return ExchangeVolumeDbMountPointsComparer.s_instance;
			}
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x00055A36 File Offset: 0x00053C36
		private ExchangeVolumeDbMountPointsComparer()
		{
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x00055A40 File Offset: 0x00053C40
		public override int Compare(ExchangeVolume x, ExchangeVolume y)
		{
			if (!x.IsExchangeVolume && !y.IsExchangeVolume)
			{
				MountedFolderPath mostAppropriateMountPoint = x.GetMostAppropriateMountPoint();
				MountedFolderPath mostAppropriateMountPoint2 = y.GetMostAppropriateMountPoint();
				if (MountedFolderPath.IsNullOrEmpty(mostAppropriateMountPoint) && !MountedFolderPath.IsNullOrEmpty(mostAppropriateMountPoint2))
				{
					return -1;
				}
				if (!MountedFolderPath.IsNullOrEmpty(mostAppropriateMountPoint) && MountedFolderPath.IsNullOrEmpty(mostAppropriateMountPoint2))
				{
					return 1;
				}
				if (MountedFolderPath.IsNullOrEmpty(mostAppropriateMountPoint) && MountedFolderPath.IsNullOrEmpty(mostAppropriateMountPoint2))
				{
					return x.VolumeName.CompareTo(y.VolumeName);
				}
				return mostAppropriateMountPoint.CompareTo(mostAppropriateMountPoint2);
			}
			else if (x.IsExchangeVolume && y.IsExchangeVolume)
			{
				if (x.IsDatabaseMountPointsNullOrEmpty() && !y.IsDatabaseMountPointsNullOrEmpty())
				{
					return -1;
				}
				if (!x.IsDatabaseMountPointsNullOrEmpty() && y.IsDatabaseMountPointsNullOrEmpty())
				{
					return 1;
				}
				if (x.IsDatabaseMountPointsNullOrEmpty() && y.IsDatabaseMountPointsNullOrEmpty())
				{
					return x.ExchangeVolumeMountPoint.CompareTo(y.ExchangeVolumeMountPoint);
				}
				return x.DatabaseMountPoints[0].CompareTo(y.DatabaseMountPoints[0]);
			}
			else
			{
				if (!x.IsExchangeVolume && y.IsExchangeVolume)
				{
					return -1;
				}
				return 1;
			}
		}

		// Token: 0x04000882 RID: 2178
		private static readonly ExchangeVolumeDbMountPointsComparer s_instance = new ExchangeVolumeDbMountPointsComparer();
	}
}
