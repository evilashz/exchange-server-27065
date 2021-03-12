using System;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Common.Bitlocker.Utilities;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay.MountPoint
{
	// Token: 0x0200023A RID: 570
	internal class ExchangeVolume
	{
		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x00055B44 File Offset: 0x00053D44
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.VolumeManagerTracer;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x00055B4B File Offset: 0x00053D4B
		public MountedFolderPath VolumeName
		{
			get
			{
				return this.m_volumeName;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x00055B53 File Offset: 0x00053D53
		// (set) Token: 0x06001592 RID: 5522 RVA: 0x00055B5B File Offset: 0x00053D5B
		public DriveType DriveType { get; private set; }

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001593 RID: 5523 RVA: 0x00055B64 File Offset: 0x00053D64
		// (set) Token: 0x06001594 RID: 5524 RVA: 0x00055B6C File Offset: 0x00053D6C
		public bool IsExchangeVolume { get; private set; }

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001595 RID: 5525 RVA: 0x00055B75 File Offset: 0x00053D75
		// (set) Token: 0x06001596 RID: 5526 RVA: 0x00055B7D File Offset: 0x00053D7D
		public bool IsAvailableAsSpare { get; private set; }

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001597 RID: 5527 RVA: 0x00055B86 File Offset: 0x00053D86
		// (set) Token: 0x06001598 RID: 5528 RVA: 0x00055B8E File Offset: 0x00053D8E
		public MountedFolderPath ExchangeVolumeMountPoint { get; private set; }

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001599 RID: 5529 RVA: 0x00055B97 File Offset: 0x00053D97
		public string DatabasesRootPath
		{
			get
			{
				return this.m_databasesRootPath;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x0600159A RID: 5530 RVA: 0x00055B9F File Offset: 0x00053D9F
		// (set) Token: 0x0600159B RID: 5531 RVA: 0x00055BA7 File Offset: 0x00053DA7
		public string VolumeLabel { get; private set; }

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x0600159C RID: 5532 RVA: 0x00055BB0 File Offset: 0x00053DB0
		// (set) Token: 0x0600159D RID: 5533 RVA: 0x00055BB8 File Offset: 0x00053DB8
		public MountedFolderPath[] DatabaseMountPoints { get; private set; }

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x00055BC1 File Offset: 0x00053DC1
		// (set) Token: 0x0600159F RID: 5535 RVA: 0x00055BC9 File Offset: 0x00053DC9
		public MountedFolderPath[] MountPoints { get; private set; }

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x00055BD2 File Offset: 0x00053DD2
		// (set) Token: 0x060015A1 RID: 5537 RVA: 0x00055BDA File Offset: 0x00053DDA
		public bool IsValid { get; private set; }

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x00055BE3 File Offset: 0x00053DE3
		// (set) Token: 0x060015A3 RID: 5539 RVA: 0x00055BEB File Offset: 0x00053DEB
		public DatabaseVolumeInfoException LastException { get; private set; }

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x060015A4 RID: 5540 RVA: 0x00055BF4 File Offset: 0x00053DF4
		private string ExchangeVolumesRootPath
		{
			get
			{
				return this.m_exchangeVolumesRootPath;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x060015A5 RID: 5541 RVA: 0x00055BFC File Offset: 0x00053DFC
		private int NumDbsPerVolume
		{
			get
			{
				return this.m_numDbsPerVolume;
			}
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x00055C04 File Offset: 0x00053E04
		private ExchangeVolume(MountedFolderPath volumeName, string exchangeVolumesRootPath, string databasesRootPath, int numDbsPerVolume)
		{
			this.m_volumeName = volumeName;
			this.m_exchangeVolumesRootPath = exchangeVolumesRootPath;
			this.m_databasesRootPath = databasesRootPath;
			this.m_numDbsPerVolume = numDbsPerVolume;
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x00055C2C File Offset: 0x00053E2C
		public static ExchangeVolume GetInstance(MountedFolderPath volumeName, string exchangeVolumesRootPath, string databasesRootPath, int numDbsPerVolume)
		{
			ExchangeVolume exchangeVolume = new ExchangeVolume(volumeName, exchangeVolumesRootPath, databasesRootPath, numDbsPerVolume);
			exchangeVolume.Refresh();
			return exchangeVolume;
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x00055CBC File Offset: 0x00053EBC
		public void Refresh()
		{
			Exception ex = null;
			this.Init();
			DriveType driveType = NativeMethods.GetDriveType(this.VolumeName.Path);
			this.DriveType = driveType;
			if (this.DriveType != DriveType.Fixed)
			{
				ExchangeVolume.Tracer.TraceError<MountedFolderPath, DriveType>((long)this.GetHashCode(), "ExchangeVolume.GetInstance( {0} ): Volume is not a Fixed DriveType. Actual: {1}", this.VolumeName, this.DriveType);
				this.IsValid = true;
				return;
			}
			string volumeLabel = MountPointUtil.GetVolumeLabel(this.VolumeName, out ex);
			if (ex != null)
			{
				ExchangeVolume.Tracer.TraceError<MountedFolderPath, Exception>((long)this.GetHashCode(), "ExchangeVolume.GetInstance( {0} ): Could not retrieve volume label. Error - ", this.VolumeName, ex);
				ex = null;
			}
			else
			{
				ExchangeVolume.Tracer.TraceDebug<MountedFolderPath, string>((long)this.GetHashCode(), "ExchangeVolume.GetInstance( {0} ): Volume has a label '{1}'.", this.VolumeName, volumeLabel);
				this.VolumeLabel = volumeLabel;
			}
			MountedFolderPath[] volumePathNamesForVolumeName = MountPointUtil.GetVolumePathNamesForVolumeName(this.VolumeName, out ex);
			if (ex != null)
			{
				ExchangeVolume.Tracer.TraceError<MountedFolderPath, Exception>((long)this.GetHashCode(), "ExchangeVolume.GetInstance( {0} ): GetVolumePathNamesForVolumeName() failed with error: {1}", this.VolumeName, ex);
				this.LastException = new ExchangeVolumeInfoInitException(this.VolumeName.Path, ex.Message, ex);
				return;
			}
			this.MountPoints = volumePathNamesForVolumeName;
			MountedFolderPath[] array = (from mp in volumePathNamesForVolumeName
			where MountPointUtil.IsPathDirectlyUnderParentPath(mp.Path, this.ExchangeVolumesRootPath, out ex) && ex == null
			select mp).ToArray<MountedFolderPath>();
			if (ex != null)
			{
				ExchangeVolume.Tracer.TraceError<MountedFolderPath, Exception>((long)this.GetHashCode(), "ExchangeVolume.GetInstance( {0} ): IsPathDirectlyUnderParentPath() for ExchangeVolumeMountPoints failed with error: {1}", this.VolumeName, ex);
				this.LastException = new ExchangeVolumeInfoInitException(this.VolumeName.Path, ex.Message, ex);
				return;
			}
			int num = array.Length;
			if (num > 0)
			{
				this.IsExchangeVolume = true;
				this.ExchangeVolumeMountPoint = array[0];
				if (num > 1)
				{
					string text = string.Join(", ", from mp in array
					select mp.Path);
					ExchangeVolume.Tracer.TraceError<MountedFolderPath, string, string>((long)this.GetHashCode(), "ExchangeVolume.GetInstance( {0} ): Multiple mount points found under '{1}': {2}", this.VolumeName, this.ExchangeVolumesRootPath, text);
					this.LastException = new ExchangeVolumeInfoMultipleExMountPointsException(this.VolumeName.Path, this.ExchangeVolumesRootPath, text);
					return;
				}
			}
			MountedFolderPath[] array2 = (from mp in volumePathNamesForVolumeName
			where MountPointUtil.IsPathDirectlyUnderParentPath(mp.Path, this.DatabasesRootPath, out ex) && ex == null
			orderby mp
			select mp).ToArray<MountedFolderPath>();
			if (ex != null)
			{
				ExchangeVolume.Tracer.TraceError<MountedFolderPath, Exception>((long)this.GetHashCode(), "ExchangeVolume.GetInstance( {0} ): IsPathDirectlyUnderParentPath() for DatabaseMountPoints failed with error: {1}", this.VolumeName, ex);
				this.LastException = new ExchangeVolumeInfoInitException(this.VolumeName.Path, ex.Message, ex);
				return;
			}
			int num2 = array2.Length;
			if (num2 < this.NumDbsPerVolume)
			{
				if (this.IsExchangeVolume)
				{
					Exception ex2;
					VolumeSpareStatus spareStatus = this.GetSpareStatus(out ex2);
					if (spareStatus == VolumeSpareStatus.EncryptingEmptySpare && ex2 == null)
					{
						ExchangeVolume.Tracer.TraceDebug<MountedFolderPath, int, int>((long)this.GetHashCode(), "ExchangeVolume.GetInstance( {0} ): Volume has {1} Database mount points. It should have {2}. But volume is getting Encrypted. Not setting as spare.", this.VolumeName, num2, this.NumDbsPerVolume);
						this.IsAvailableAsSpare = false;
					}
					else
					{
						ExchangeVolume.Tracer.TraceDebug<MountedFolderPath, int, int>((long)this.GetHashCode(), "ExchangeVolume.GetInstance( {0} ): Volume has {1} Database mount points. It should have {2}. Setting as spare.", this.VolumeName, num2, this.NumDbsPerVolume);
						this.IsAvailableAsSpare = true;
					}
				}
			}
			else
			{
				if (num2 > this.NumDbsPerVolume)
				{
					string text2 = string.Join(", ", from mp in array2
					select mp.Path);
					ExchangeVolume.Tracer.TraceError((long)this.GetHashCode(), "ExchangeVolume.GetInstance( {0} ): Volume has {1} Database mount points, but should only have MAX of {2}: {3}", new object[]
					{
						this.VolumeName,
						num2,
						this.NumDbsPerVolume,
						text2
					});
					this.LastException = new ExchangeVolumeInfoMultipleDbMountPointsException(this.VolumeName.Path, this.DatabasesRootPath, text2, this.NumDbsPerVolume);
					return;
				}
				ExchangeVolume.Tracer.TraceDebug<MountedFolderPath, int>((long)this.GetHashCode(), "ExchangeVolume.GetInstance( {0} ): Volume has expected {1} Database mount points.", this.VolumeName, num2);
			}
			this.DatabaseMountPoints = array2;
			this.IsValid = true;
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x0005610C File Offset: 0x0005430C
		public VolumeSpareStatus GetSpareStatus(out Exception exception)
		{
			exception = null;
			ExchangeVolume.Tracer.TraceDebug<MountedFolderPath>((long)this.GetHashCode(), "ExchangeVolume.GetSpareStatus(): Computing status of volume '{0}'.", this.VolumeName);
			if (!this.IsValid)
			{
				ExchangeVolume.Tracer.TraceError<DatabaseVolumeInfoException>((long)this.GetHashCode(), "ExchangeVolume.GetSpareStatus(): Returning Error because the ExchangeVolume instance is invalid. Error: {0}", this.LastException);
				exception = this.LastException;
				return VolumeSpareStatus.Error;
			}
			if (!this.IsExchangeVolume || !this.IsAvailableAsSpare || this.DatabaseMountPoints.Length != 0)
			{
				ExchangeVolume.Tracer.TraceDebug<MountedFolderPath>((long)this.GetHashCode(), "ExchangeVolume.GetSpareStatus(): Returning Volume '{0}' is NotUsableAsSpare.", this.VolumeName);
				return VolumeSpareStatus.NotUsableAsSpare;
			}
			ExchangeVolume.Tracer.TraceDebug((long)this.GetHashCode(), "ExchangeVolume.GetSpareStatus(): Found a potential spare volume...");
			MountedFolderPath exchangeVolumeMountPoint = this.ExchangeVolumeMountPoint;
			if (MountPointUtil.IsDirectoryNonExistentOrEmpty(exchangeVolumeMountPoint.Path, out exception))
			{
				ExchangeVolume.Tracer.TraceDebug<MountedFolderPath>((long)this.GetHashCode(), "ExchangeVolume.GetSpareStatus(): Returning Volume '{0}' is EmptySpare.", this.VolumeName);
				bool flag = BitlockerUtil.IsVolumeMountedOnVirtualDisk(this.VolumeName.Path, out exception);
				if (exception != null)
				{
					ExchangeVolume.Tracer.TraceDebug<MountedFolderPath, Exception>((long)this.GetHashCode(), "ExchangeVolume.GetSpareStatus(): Exception finding whether Volume '{0}' is mounted on a virtual disk or not. Reason {1}", this.VolumeName, exception);
				}
				if (!flag)
				{
					bool flag2 = false;
					bool flag3 = false;
					exception = BitlockerUtil.IsVolumeEncryptedOrEncrypting(this.VolumeName.Path, out flag2, out flag3);
					if (exception != null)
					{
						ExchangeVolume.Tracer.TraceDebug<MountedFolderPath, Exception>((long)this.GetHashCode(), "ExchangeVolume.GetSpareStatus(): Exception finding whether Volume '{0}' is encrypting or not. Reason {1}", this.VolumeName, exception);
					}
					else
					{
						if (flag2)
						{
							return VolumeSpareStatus.EncryptingEmptySpare;
						}
						if (flag3)
						{
							return VolumeSpareStatus.EncryptedEmptySpare;
						}
						string arg;
						string arg2;
						bool flag4 = BitlockerUtil.IsEncryptionPausedDueToBadBlocks(this.VolumeName.Path, out exception, out arg, out arg2);
						if (exception != null)
						{
							ExchangeVolume.Tracer.TraceDebug<MountedFolderPath, Exception>((long)this.GetHashCode(), "ExchangeVolume.GetSpareStatus(): Exception finding whether Volume '{0}' has encryption paused due to bad blocks. Reason {1}", this.VolumeName, exception);
							return VolumeSpareStatus.Error;
						}
						if (flag4)
						{
							ExchangeVolume.Tracer.TraceDebug<MountedFolderPath, string, string>((long)this.GetHashCode(), "ExchangeVolume.GetSpareStatus(): Returning Volume '{0}' is Qurantined due to encryption paused due to bad blocks. Mount point {1}. Event Xml {2}", this.VolumeName, arg, arg2);
							return VolumeSpareStatus.Quarantined;
						}
					}
				}
				else
				{
					ExchangeVolume.Tracer.TraceDebug<MountedFolderPath>((long)this.GetHashCode(), "ExchangeVolume.GetSpareStatus(): Volume mounted on virtual disk. Not attempting to find bitlocker spare status", this.VolumeName);
				}
				return VolumeSpareStatus.UnEncryptedEmptySpare;
			}
			int num = 0;
			if (exception == null || FileOperations.IsCorruptedIOException((IOException)exception, out num))
			{
				ExchangeVolume.Tracer.TraceDebug<MountedFolderPath, string>((long)this.GetHashCode(), "ExchangeVolume.GetSpareStatus(): Returning Volume '{0}' is Quarantined because it has some files/directories under mountPath '{1}'.", this.VolumeName, exchangeVolumeMountPoint.Path);
				return VolumeSpareStatus.Quarantined;
			}
			ExchangeVolume.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "ExchangeVolume.GetSpareStatus(): Returning Error because IsDirectoryNonExistentOrEmpty() returned exception for mountPath '{0}'. Exception: {1}", exchangeVolumeMountPoint.Path, exception);
			return VolumeSpareStatus.Error;
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x00056350 File Offset: 0x00054550
		public bool IsVolumeMissingDatabaseMountPoints(out Exception exception)
		{
			bool flag = true;
			int num = -1;
			exception = null;
			if (!this.IsValid)
			{
				if (this.LastException == null)
				{
					exception = new InvalidVolumeMissingException(this.VolumeName.Path);
				}
				else
				{
					exception = this.LastException;
				}
				ExchangeVolume.Tracer.TraceError<MountedFolderPath, Exception>((long)this.GetHashCode(), "ExchangeVolume.IsVolumeMissingDatabaseMountPoints(): Volume '{0}' is either not valid. Error: {1}", this.VolumeName, exception);
				return flag;
			}
			if (this.DatabaseMountPoints != null)
			{
				num = this.DatabaseMountPoints.Length;
				if (num >= this.m_numDbsPerVolume)
				{
					flag = false;
				}
			}
			ExchangeVolume.Tracer.TraceDebug((long)this.GetHashCode(), "ExchangeVolume.IsVolumeMissingDatabaseMountPoints(): Volume '{0}' is supposed to have {1} database mountpoints but only found {2}. Result: {3}.", new object[]
			{
				this.VolumeName,
				this.m_numDbsPerVolume,
				num,
				flag
			});
			return flag;
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x00056414 File Offset: 0x00054614
		public MountedFolderPath GetMostAppropriateMountPoint()
		{
			if (this.IsExchangeVolume && !MountedFolderPath.IsNullOrEmpty(this.ExchangeVolumeMountPoint))
			{
				return this.ExchangeVolumeMountPoint;
			}
			if (this.MountPoints == null || this.MountPoints.Length == 0)
			{
				return MountedFolderPath.Empty;
			}
			return this.MountPoints[0];
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x00056452 File Offset: 0x00054652
		public bool IsDatabaseMountPointsNullOrEmpty()
		{
			return this.DatabaseMountPoints == null || this.DatabaseMountPoints.Length == 0;
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x0005646C File Offset: 0x0005466C
		private void Init()
		{
			this.IsValid = false;
			this.LastException = null;
			this.DriveType = DriveType.Unknown;
			this.IsExchangeVolume = false;
			this.IsAvailableAsSpare = false;
			this.ExchangeVolumeMountPoint = MountedFolderPath.Empty;
			this.DatabaseMountPoints = new MountedFolderPath[0];
			this.MountPoints = new MountedFolderPath[0];
		}

		// Token: 0x04000883 RID: 2179
		private readonly MountedFolderPath m_volumeName;

		// Token: 0x04000884 RID: 2180
		private readonly string m_exchangeVolumesRootPath;

		// Token: 0x04000885 RID: 2181
		private readonly string m_databasesRootPath;

		// Token: 0x04000886 RID: 2182
		private readonly int m_numDbsPerVolume;
	}
}
