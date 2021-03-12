using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay.MountPoint
{
	// Token: 0x02000237 RID: 567
	internal class DatabaseVolumeInfo
	{
		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001573 RID: 5491 RVA: 0x00055753 File Offset: 0x00053953
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.VolumeManagerTracer;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x0005575A File Offset: 0x0005395A
		// (set) Token: 0x06001575 RID: 5493 RVA: 0x00055762 File Offset: 0x00053962
		public MountedFolderPath ExchangeVolumeMountPoint { get; private set; }

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x0005576B File Offset: 0x0005396B
		// (set) Token: 0x06001577 RID: 5495 RVA: 0x00055773 File Offset: 0x00053973
		public MountedFolderPath DatabaseVolumeMountPoint { get; private set; }

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x0005577C File Offset: 0x0005397C
		// (set) Token: 0x06001579 RID: 5497 RVA: 0x00055784 File Offset: 0x00053984
		public MountedFolderPath DatabaseVolumeName { get; private set; }

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x0005578D File Offset: 0x0005398D
		// (set) Token: 0x0600157B RID: 5499 RVA: 0x00055795 File Offset: 0x00053995
		public bool IsDatabasePathOnMountedFolder { get; private set; }

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x0600157C RID: 5500 RVA: 0x0005579E File Offset: 0x0005399E
		// (set) Token: 0x0600157D RID: 5501 RVA: 0x000557A6 File Offset: 0x000539A6
		public MountedFolderPath LogVolumeMountPoint { get; private set; }

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x000557AF File Offset: 0x000539AF
		// (set) Token: 0x0600157F RID: 5503 RVA: 0x000557B7 File Offset: 0x000539B7
		public MountedFolderPath LogVolumeName { get; private set; }

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x000557C0 File Offset: 0x000539C0
		// (set) Token: 0x06001581 RID: 5505 RVA: 0x000557C8 File Offset: 0x000539C8
		public bool IsLogPathOnMountedFolder { get; private set; }

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001582 RID: 5506 RVA: 0x000557D1 File Offset: 0x000539D1
		// (set) Token: 0x06001583 RID: 5507 RVA: 0x000557D9 File Offset: 0x000539D9
		public bool IsValid { get; private set; }

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001584 RID: 5508 RVA: 0x000557E2 File Offset: 0x000539E2
		// (set) Token: 0x06001585 RID: 5509 RVA: 0x000557EA File Offset: 0x000539EA
		public bool IsExchangeVolumeMountPointValid { get; private set; }

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x000557F3 File Offset: 0x000539F3
		// (set) Token: 0x06001587 RID: 5511 RVA: 0x000557FB File Offset: 0x000539FB
		public DatabaseVolumeInfoException LastException { get; private set; }

		// Token: 0x06001588 RID: 5512 RVA: 0x00055804 File Offset: 0x00053A04
		private DatabaseVolumeInfo()
		{
			this.ExchangeVolumeMountPoint = MountedFolderPath.Empty;
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x00055818 File Offset: 0x00053A18
		public static DatabaseVolumeInfo GetInstance(string edbPath, string logPath, string databaseName, string autoDagVolumesRootFolderPath, string autoDagDatabasesRootFolderPath, int autoDagDatabaseCopiesPerVolume)
		{
			Exception ex = null;
			DatabaseVolumeInfo databaseVolumeInfo = new DatabaseVolumeInfo();
			MountedFolderPath volumePathName = MountPointUtil.GetVolumePathName(edbPath, out ex);
			if (ex != null)
			{
				DatabaseVolumeInfo.Tracer.TraceError<string, string, Exception>(0L, "DatabaseVolumeInfo.GetInstance( {0} ): GetVolumePathName() for EDB path '{1}' failed with error: {2}", databaseName, edbPath, ex);
				databaseVolumeInfo.LastException = new DatabaseVolumeInfoInitException(databaseName, ex.Message, ex);
				return databaseVolumeInfo;
			}
			databaseVolumeInfo.DatabaseVolumeMountPoint = volumePathName;
			MountedFolderPath volumeNameForVolumeMountPoint = MountPointUtil.GetVolumeNameForVolumeMountPoint(volumePathName, out ex);
			if (ex != null)
			{
				DatabaseVolumeInfo.Tracer.TraceError<string, MountedFolderPath, Exception>(0L, "DatabaseVolumeInfo.GetInstance( {0} ): GetVolumeNameForVolumeMountPoint() for EDB mount point '{1}' failed with error: {2}", databaseName, volumePathName, ex);
				databaseVolumeInfo.LastException = new DatabaseVolumeInfoInitException(databaseName, ex.Message, ex);
				return databaseVolumeInfo;
			}
			databaseVolumeInfo.DatabaseVolumeName = volumeNameForVolumeMountPoint;
			bool isDatabasePathOnMountedFolder = MountPointUtil.IsDirectoryMountPoint(volumePathName.Path, out ex);
			if (ex != null)
			{
				DatabaseVolumeInfo.Tracer.TraceError<string, MountedFolderPath, Exception>(0L, "DatabaseVolumeInfo.GetInstance( {0} ): IsDirectoryMountPoint() for EDB mount point '{1}' failed with error: {2}", databaseName, volumePathName, ex);
				databaseVolumeInfo.LastException = new DatabaseVolumeInfoInitException(databaseName, ex.Message, ex);
				return databaseVolumeInfo;
			}
			databaseVolumeInfo.IsDatabasePathOnMountedFolder = isDatabasePathOnMountedFolder;
			MountedFolderPath volumePathName2 = MountPointUtil.GetVolumePathName(logPath, out ex);
			if (ex != null)
			{
				DatabaseVolumeInfo.Tracer.TraceError<string, string, Exception>(0L, "DatabaseVolumeInfo.GetInstance( {0} ): GetVolumePathName() for LOG path '{1}' failed with error: {2}", databaseName, logPath, ex);
				databaseVolumeInfo.LastException = new DatabaseVolumeInfoInitException(databaseName, ex.Message, ex);
				return databaseVolumeInfo;
			}
			databaseVolumeInfo.LogVolumeMountPoint = volumePathName2;
			MountedFolderPath volumeNameForVolumeMountPoint2 = MountPointUtil.GetVolumeNameForVolumeMountPoint(volumePathName2, out ex);
			if (ex != null)
			{
				DatabaseVolumeInfo.Tracer.TraceError<string, MountedFolderPath, Exception>(0L, "DatabaseVolumeInfo.GetInstance( {0} ): GetVolumeNameForVolumeMountPoint() for LOG mount point '{1}' failed with error: {2}", databaseName, volumePathName2, ex);
				databaseVolumeInfo.LastException = new DatabaseVolumeInfoInitException(databaseName, ex.Message, ex);
				return databaseVolumeInfo;
			}
			databaseVolumeInfo.LogVolumeName = volumeNameForVolumeMountPoint2;
			bool isLogPathOnMountedFolder = MountPointUtil.IsDirectoryMountPoint(volumePathName2.Path, out ex);
			if (ex != null)
			{
				DatabaseVolumeInfo.Tracer.TraceError<string, MountedFolderPath, Exception>(0L, "DatabaseVolumeInfo.GetInstance( {0} ): IsDirectoryMountPoint() for LOG mount point '{1}' failed with error: {2}", databaseName, volumePathName2, ex);
				databaseVolumeInfo.LastException = new DatabaseVolumeInfoInitException(databaseName, ex.Message, ex);
				return databaseVolumeInfo;
			}
			databaseVolumeInfo.IsLogPathOnMountedFolder = isLogPathOnMountedFolder;
			if (!string.IsNullOrEmpty(autoDagVolumesRootFolderPath) && !string.IsNullOrEmpty(autoDagDatabasesRootFolderPath))
			{
				ExchangeVolume instance = ExchangeVolume.GetInstance(volumeNameForVolumeMountPoint, autoDagVolumesRootFolderPath, autoDagDatabasesRootFolderPath, autoDagDatabaseCopiesPerVolume);
				if (instance.IsValid)
				{
					databaseVolumeInfo.ExchangeVolumeMountPoint = (instance.ExchangeVolumeMountPoint ?? MountedFolderPath.Empty);
					databaseVolumeInfo.IsExchangeVolumeMountPointValid = !MountedFolderPath.IsNullOrEmpty(databaseVolumeInfo.ExchangeVolumeMountPoint);
				}
			}
			databaseVolumeInfo.IsValid = true;
			return databaseVolumeInfo;
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x00055A04 File Offset: 0x00053C04
		public static DatabaseVolumeInfo GetInstance(IReplayConfiguration config)
		{
			return DatabaseVolumeInfo.GetInstance(config.DestinationEdbPath, config.DestinationLogPath, config.DisplayName, config.AutoDagVolumesRootFolderPath, config.AutoDagDatabasesRootFolderPath, config.AutoDagDatabaseCopiesPerVolume);
		}
	}
}
