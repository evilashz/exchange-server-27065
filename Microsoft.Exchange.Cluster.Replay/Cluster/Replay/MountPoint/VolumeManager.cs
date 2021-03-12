using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.Replay.IO;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Cluster.Replay.MountPoint
{
	// Token: 0x02000240 RID: 576
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class VolumeManager : IVolumeManager
	{
		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x060015E7 RID: 5607 RVA: 0x00057B06 File Offset: 0x00055D06
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.VolumeManagerTracer;
			}
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x00057B0D File Offset: 0x00055D0D
		public static void GetDatabaseLogEdbFolderNames(string databaseName, out string logFolderName, out string edbFolderName)
		{
			logFolderName = string.Format("{0}.log", databaseName);
			edbFolderName = string.Format("{0}.db", databaseName);
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x00057B29 File Offset: 0x00055D29
		public static MountedFolderPath GetDatabaseMountedFolderPath(NonRootLocalLongFullPath databaseRootFolderPath, string databaseName)
		{
			if (databaseRootFolderPath != null && !string.IsNullOrEmpty(databaseName))
			{
				return new MountedFolderPath(Path.Combine(databaseRootFolderPath.PathName, databaseName));
			}
			return null;
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x00057B50 File Offset: 0x00055D50
		public static VolumeManager DefaultInstance
		{
			get
			{
				if (VolumeManager.m_defaultInstance == null)
				{
					lock (VolumeManager.lockObjectForSingleton)
					{
						if (VolumeManager.m_defaultInstance == null)
						{
							VolumeManager.m_defaultInstance = new VolumeManager();
						}
					}
				}
				return VolumeManager.m_defaultInstance;
			}
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00057BA8 File Offset: 0x00055DA8
		public VolumeManager()
		{
			this.m_assignedVolumes = new Dictionary<string, VolumeManager.VolumeAssignmentInfo>();
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x060015EC RID: 5612 RVA: 0x00057BDC File Offset: 0x00055DDC
		public IEnumerable<ExchangeVolume> Volumes
		{
			get
			{
				return this.m_volumes;
			}
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x00057BE4 File Offset: 0x00055DE4
		public ExchangeVolume LookupVolume(string mountPoint)
		{
			return this.LookupVolume(new MountedFolderPath(mountPoint));
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00057BF4 File Offset: 0x00055DF4
		public ExchangeVolume LookupVolume(MountedFolderPath mountPoint)
		{
			ExchangeVolume result;
			lock (this.m_lockObjectForReadWrite)
			{
				if (this.m_volumeTable.ContainsKey(mountPoint))
				{
					ExchangeVolume exchangeVolume = this.m_volumeTable[mountPoint];
					result = exchangeVolume;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x00057C50 File Offset: 0x00055E50
		public void RefreshIfTooStale(TimeSpan staleNessThreshold)
		{
			if (DateTime.UtcNow - this.refreshTimeInUtc > staleNessThreshold)
			{
				lock (this.m_lockObjectForRefreshIfTooStale)
				{
					if (DateTime.UtcNow - this.refreshTimeInUtc > staleNessThreshold)
					{
						this.Refresh(Dependencies.MonitoringADConfigProvider.GetConfigIgnoringStaleness(true));
					}
				}
			}
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x00057CE8 File Offset: 0x00055EE8
		public void Refresh(IMonitoringADConfig adConfig)
		{
			VolumeManager.Tracer.TraceDebug((long)this.GetHashCode(), "VolumeManager: Entering Refresh()...");
			using (VolumeEnumerator volumeEnumerator = new VolumeEnumerator())
			{
				IEnumerable<ExchangeVolume> exchangeVolumes = volumeEnumerator.GetExchangeVolumes(adConfig.Dag.AutoDagVolumesRootFolderPath.PathName, adConfig.Dag.AutoDagDatabasesRootFolderPath.PathName, adConfig.Dag.AutoDagDatabaseCopiesPerVolume);
				lock (this.m_lockObjectForReadWrite)
				{
					this.m_volumes = exchangeVolumes.OrderBy((ExchangeVolume vol) => vol, ExchangeVolumeDbMountPointsComparer.Instance).ToArray<ExchangeVolume>();
					this.m_volumeTable = (from volume in this.m_volumes
					where !MountedFolderPath.IsNullOrEmpty(volume.GetMostAppropriateMountPoint())
					select volume).ToDictionary((ExchangeVolume volume) => volume.GetMostAppropriateMountPoint());
					this.refreshTimeInUtc = DateTime.UtcNow;
				}
			}
			VolumeManager.Tracer.TraceDebug((long)this.GetHashCode(), "VolumeManager: Refresh() done.");
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x00057E54 File Offset: 0x00056054
		public IEnumerable<CopyStatusClientCachedEntry> LookUpCopyStatusesForVolumeLabel(string volumeLabel, IEnumerable<CopyStatusClientCachedEntry> serverCopyStatus, out Exception exception)
		{
			exception = null;
			List<CopyStatusClientCachedEntry> list = new List<CopyStatusClientCachedEntry>();
			if (volumeLabel == null || serverCopyStatus == null)
			{
				VolumeManager.Tracer.TraceDebug((long)this.GetHashCode(), "VolumeManager: LookUpCopyStatusesForVolumeLabel() serverCopyStatus or volume was null.");
				return null;
			}
			VolumeManager.Tracer.TraceDebug<string>((long)this.GetHashCode(), "VolumeManager: LookUpCopyStatusesForVolumeLabel() is attempting to get copies on volume with label: {0}.", volumeLabel);
			IEnumerable<ExchangeVolume> enumerable = from volume in this.m_volumes
			where StringUtil.IsEqualIgnoreCase(volume.VolumeLabel, volumeLabel)
			select volume;
			int num = (enumerable == null) ? 0 : enumerable.Count<ExchangeVolume>();
			if (num == 0)
			{
				VolumeManager.Tracer.TraceDebug<string>((long)this.GetHashCode(), "VolumeManager: LookUpGetCopyStatusesForVolumeLabel() no volumes were found with volume label '{0}'.", volumeLabel);
				return null;
			}
			if (num > 1)
			{
				IEnumerable<string> values = from v in enumerable
				select v.VolumeName.Path;
				VolumeManager.Tracer.TraceDebug<int, string, string>((long)this.GetHashCode(), "VolumeManager: LookUpGetCopyStatusesForVolumeLabel() found {0} many volumes '{1}' with volume label '{2}'.", enumerable.Count<ExchangeVolume>(), string.Join(",", values), volumeLabel);
				exception = new FoundTooManyVolumesWithSameVolumeLabelException(string.Join(",", values), volumeLabel);
				return null;
			}
			foreach (CopyStatusClientCachedEntry copyStatusClientCachedEntry in serverCopyStatus)
			{
				if (StringUtil.IsEqualIgnoreCase(copyStatusClientCachedEntry.CopyStatus.DatabaseVolumeName, enumerable.FirstOrDefault<ExchangeVolume>().VolumeName.Path))
				{
					VolumeManager.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "VolumeManager: LookUpCopyStatusesForVolumeLabel() found a match for database guid: {0}.", copyStatusClientCachedEntry.DbGuid);
					list.Add(copyStatusClientCachedEntry);
				}
			}
			VolumeManager.Tracer.TraceDebug<int>((long)this.GetHashCode(), "VolumeManager: LookUpCopyStatusesForVolumeLabel() found {0} copies", list.Count);
			if (list.Count <= 0)
			{
				return null;
			}
			return list;
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x00058020 File Offset: 0x00056220
		public ExchangeVolume AssignSpare(DatabaseSpareInfo[] dbInfos)
		{
			string databaseNames = VolumeManager.GetDatabaseNames(dbInfos);
			VolumeManager.Tracer.TraceDebug<string>((long)this.GetHashCode(), "VolumeManager: Entering AssignSpare() for following Databases: {0}", databaseNames);
			ExchangeVolume result;
			lock (this.m_lockObjectForReadWrite)
			{
				bool flag2;
				ExchangeVolume exchangeVolume = this.ChooseSpareVolume(dbInfos, out flag2);
				if (exchangeVolume == null)
				{
					VolumeManager.Tracer.TraceError((long)this.GetHashCode(), "VolumeManager: AssignSpare() could not find any spare volumes to use.");
					throw new CouldNotFindSpareVolumeException(databaseNames);
				}
				foreach (DatabaseSpareInfo dbInfo in dbInfos)
				{
					this.AssignSpareToDatabase(exchangeVolume, dbInfo);
				}
				if (!flag2)
				{
					exchangeVolume.Refresh();
					this.RefreshVolumes();
				}
				result = exchangeVolume;
			}
			return result;
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x00058118 File Offset: 0x00056318
		public ExchangeVolume FixupMountPointForDatabase(DatabaseSpareInfo dbInfo, MountedFolderPath volumeToAssign)
		{
			ExchangeVolume exchangeVolume = this.m_volumes.FirstOrDefault((ExchangeVolume vol) => vol.IsValid && vol.IsExchangeVolume && vol.VolumeName.Equals(volumeToAssign));
			if (exchangeVolume == null)
			{
				throw new CouldNotFindVolumeException(volumeToAssign.Path);
			}
			Exception ex;
			if (!exchangeVolume.IsVolumeMissingDatabaseMountPoints(out ex))
			{
				throw new DbFixupFailedVolumeHasMaxDbMountPointsException(dbInfo.DbName, volumeToAssign.Path);
			}
			if (ex != null)
			{
				throw new DbFixupFailedException(dbInfo.DbName, volumeToAssign.Path, ex.Message, ex);
			}
			this.AssignSpareToDatabase(exchangeVolume, dbInfo);
			return exchangeVolume;
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x000581AC File Offset: 0x000563AC
		public bool TryReclaimVolume(ExchangeVolume volume, ExchangeVolumeDbStatusInfo volStatusInfo, out bool formatSucceeded, out Exception exception)
		{
			exception = null;
			formatSucceeded = false;
			if (!this.IsVolumeSafeToFormat(volume, volStatusInfo, out exception))
			{
				VolumeManager.Tracer.TraceDebug<MountedFolderPath, Exception>((long)this.GetHashCode(), "VolumeManager: TryReclaimVolume() returning false because volume '{0}' is not safe to format. Exception: {1}", volume.VolumeName, exception);
				return false;
			}
			VolumeManager.Tracer.TraceDebug<MountedFolderPath>((long)this.GetHashCode(), "VolumeManager: TryReclaimVolume() passed volume safety checks for volume '{0}'.", volume.VolumeName);
			exception = this.FormatVolume(volume);
			if (exception != null)
			{
				return false;
			}
			formatSucceeded = true;
			string path = volume.VolumeName.Path;
			string path2 = volume.ExchangeVolumeMountPoint.Path;
			volume.Refresh();
			if (!this.IsVolumePossibleSpare(volume, null))
			{
				VolumeManager.Tracer.TraceError<string>((long)this.GetHashCode(), "VolumeManager: TryReclaimVolume(): Volume '{0}' was found to be still unusable as a spare after formatting it!", path);
				exception = new VolumeCouldNotBeReclaimedException(path, path2);
				return false;
			}
			return true;
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x00058284 File Offset: 0x00056484
		public Exception ConfigureDatabaseMountPoints(ExchangeVolume volume, IEnumerable<IADDatabase> databases, int numExpectedDbsPerVolume)
		{
			Exception ex = null;
			IEnumerable<string> databaseNamesFromVolume = this.GetDatabaseNamesFromVolume(volume, out ex);
			if (ex != null || databaseNamesFromVolume == null || !databaseNamesFromVolume.Any<string>())
			{
				VolumeManager.Tracer.TraceError<MountedFolderPath, string>((long)this.GetHashCode(), "DiskReclaimer: Could not find database files in volume: '{0}'. Error: {1}", volume.VolumeName, AmExceptionHelper.GetExceptionMessageOrNoneString(ex));
				return ex;
			}
			int num = databaseNamesFromVolume.Count<string>();
			if (num != numExpectedDbsPerVolume)
			{
				VolumeManager.Tracer.TraceError((long)this.GetHashCode(), "DiskReclaimer: ConfigureDatabaseMountPoints() found unexpected number of DB directories on volume '{0} ({1})'. Expected: {2}, Actual: {3}", new object[]
				{
					volume.ExchangeVolumeMountPoint,
					volume.VolumeName,
					numExpectedDbsPerVolume,
					num
				});
				ex = new DbVolumeInvalidDirectoriesException(volume.VolumeName.Path, volume.ExchangeVolumeMountPoint.Path, numExpectedDbsPerVolume, num);
				return ex;
			}
			using (IEnumerator<string> enumerator = databaseNamesFromVolume.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string dbName = enumerator.Current;
					IEnumerable<IADDatabase> source = from db in databases
					where StringUtil.IsEqualIgnoreCase(db.Name, dbName)
					select db;
					if (source.Count<IADDatabase>() == 0)
					{
						VolumeManager.Tracer.TraceError<string>((long)this.GetHashCode(), "DiskReclaimer: Database files: '{0}' found on the server does not exist in AD. Verify DAG configuration", dbName);
						ex = new DatabasesMissingInADException(dbName, volume.VolumeName.Path);
						break;
					}
					MountedFolderPath mountedFolderPath = new MountedFolderPath(Path.Combine(volume.DatabasesRootPath, dbName));
					if (MountPointUtil.IsDirectoryAccessibleMountPoint(mountedFolderPath.Path, out ex))
					{
						VolumeManager.Tracer.TraceDebug((long)this.GetHashCode(), "DiskReclaimer: Directory is already a valid accessible mountpoint. Skip this mountpoint.");
					}
					else
					{
						ex = null;
						try
						{
							if (Directory.Exists(mountedFolderPath.Path))
							{
								ex = MountPointUtil.DeleteVolumeMountPoint(mountedFolderPath);
							}
							else
							{
								Directory.CreateDirectory(mountedFolderPath.Path);
							}
						}
						catch (ArgumentException ex2)
						{
							ex = ex2;
						}
						catch (IOException ex3)
						{
							ex = ex3;
						}
						catch (UnauthorizedAccessException ex4)
						{
							ex = ex4;
						}
						catch (SecurityException ex5)
						{
							ex = ex5;
						}
						if (ex != null)
						{
							VolumeManager.Tracer.TraceError<string, MountedFolderPath, Exception>((long)this.GetHashCode(), "DiskReclaimer: ConfigureVolumesPostReInstall() could not process mountpoint folder: '{0}' for volume: '{1}'. Error: {2}", mountedFolderPath.Path, volume.VolumeName, ex);
							break;
						}
						ex = MountPointUtil.SetVolumeMountPoint(mountedFolderPath, volume.VolumeName);
						if (ex != null)
						{
							VolumeManager.Tracer.TraceError<string, MountedFolderPath, Exception>((long)this.GetHashCode(), "DiskReclaimer: ConfigureVolumesPostReInstall() failed to assign mountpoint: '{0}' to volume: '{1}'. Error: {2}", mountedFolderPath.Path, volume.VolumeName, ex);
						}
					}
				}
			}
			return ex;
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x00058544 File Offset: 0x00056744
		internal IEnumerable<string> GetDatabaseNamesFromVolume(ExchangeVolume volume, out Exception exception)
		{
			return this.GetDatabaseNamesFromDirectory(volume.ExchangeVolumeMountPoint.Path, out exception);
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x00058574 File Offset: 0x00056774
		internal IEnumerable<string> GetDatabaseNamesFromDirectory(string directory, out Exception exception)
		{
			exception = null;
			DirectoryInfo di = null;
			HashSet<string> result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			exception = MountPointUtil.HandleIOExceptions(delegate
			{
				di = new DirectoryInfo(directory);
			});
			if (exception != null)
			{
				VolumeManager.Tracer.TraceError<string, Exception>(0L, "GetDatabaseNamesFromDirectory(): Failed to construct DirectoryInfo for directory '{0}'. Exception: {1}", directory, exception);
				return result;
			}
			if (!di.Exists)
			{
				VolumeManager.Tracer.TraceError<string>(0L, "GetDatabaseNamesFromDirectory(): Directory '{0}' is not present.", directory);
				return result;
			}
			this.GetDatabaseNamesFromDirectory(di, "{0}.db", ref result, out exception);
			if (exception != null)
			{
				return result;
			}
			this.GetDatabaseNamesFromDirectory(di, "{0}.log", ref result, out exception);
			if (exception != null)
			{
				return result;
			}
			return result;
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x00058630 File Offset: 0x00056830
		internal List<ExchangeVolume>[] DetermineVolumeSpareStatuses()
		{
			List<ExchangeVolume>[] array = new List<ExchangeVolume>[7];
			foreach (ExchangeVolume exchangeVolume in this.m_volumes)
			{
				Exception ex;
				VolumeSpareStatus spareStatus = exchangeVolume.GetSpareStatus(out ex);
				int num = (int)spareStatus;
				List<ExchangeVolume> list = array[num];
				if (list == null)
				{
					list = new List<ExchangeVolume>();
					array[num] = list;
				}
				list.Add(exchangeVolume);
				if (spareStatus == VolumeSpareStatus.Quarantined && ex != null)
				{
					ReplayCrimsonEvents.DiskReclaimerCorruptedVolumeInformation.LogPeriodic<MountedFolderPath, MountedFolderPath, string>(exchangeVolume.VolumeName.Path, DiagCore.DefaultEventSuppressionInterval, exchangeVolume.VolumeName, exchangeVolume.ExchangeVolumeMountPoint, ex.Message);
				}
				else if (spareStatus == VolumeSpareStatus.Error)
				{
					ReplayCrimsonEvents.DiskReclaimerComputeStatusFailed.LogPeriodic<MountedFolderPath, MountedFolderPath, string>(exchangeVolume.VolumeName.Path, DiagCore.DefaultEventSuppressionInterval, exchangeVolume.VolumeName, exchangeVolume.ExchangeVolumeMountPoint, ex.Message);
				}
			}
			ReplayCrimsonEvents.DiskReclaimerLogVolumes.Log<int, int, int, int, int, int, int, string, string, string, string, string, string, string>(this.GetGroupCount(array, VolumeSpareStatus.Unknown), this.GetGroupCount(array, VolumeSpareStatus.UnEncryptedEmptySpare), this.GetGroupCount(array, VolumeSpareStatus.Quarantined), this.GetGroupCount(array, VolumeSpareStatus.NotUsableAsSpare), this.GetGroupCount(array, VolumeSpareStatus.Error), this.GetGroupCount(array, VolumeSpareStatus.EncryptingEmptySpare), this.GetGroupCount(array, VolumeSpareStatus.EncryptedEmptySpare), this.GetGroupedVolumesString(array, VolumeSpareStatus.UnEncryptedEmptySpare), this.GetGroupedVolumesString(array, VolumeSpareStatus.Quarantined), this.GetGroupedVolumesString(array, VolumeSpareStatus.NotUsableAsSpare), this.GetGroupedVolumesString(array, VolumeSpareStatus.Error), this.GetGroupedVolumesString(array, VolumeSpareStatus.Unknown), this.GetGroupedVolumesString(array, VolumeSpareStatus.EncryptingEmptySpare), this.GetGroupedVolumesString(array, VolumeSpareStatus.EncryptedEmptySpare));
			return array;
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x00058778 File Offset: 0x00056978
		internal void DetermineMisconfiguredVolumes(IMonitoringADConfig adConfig)
		{
			lock (this.m_lockObjectForReadWrite)
			{
				foreach (ExchangeVolume exchangeVolume in this.m_volumes)
				{
					if (exchangeVolume.IsExchangeVolume && exchangeVolume.DatabaseMountPoints.Length != 0)
					{
						if (exchangeVolume.DatabaseMountPoints.Length != adConfig.Dag.AutoDagDatabaseCopiesPerVolume)
						{
							ReplayCrimsonEvents.DiskReclaimerDbVolumeInvalidMountPoints.LogPeriodic<MountedFolderPath, MountedFolderPath>(exchangeVolume.VolumeName.Path, TimeSpan.FromDays(1.0), exchangeVolume.VolumeName, exchangeVolume.ExchangeVolumeMountPoint);
						}
						Exception ex;
						IEnumerable<string> databaseNamesFromVolume = this.GetDatabaseNamesFromVolume(exchangeVolume, out ex);
						if (databaseNamesFromVolume.Count<string>() > adConfig.Dag.AutoDagDatabaseCopiesPerVolume)
						{
							ReplayCrimsonEvents.DiskReclaimerDbVolumeInvalidDirectories.LogPeriodic<MountedFolderPath, MountedFolderPath>(exchangeVolume.VolumeName.Path, TimeSpan.FromDays(1.0), exchangeVolume.VolumeName, exchangeVolume.ExchangeVolumeMountPoint);
						}
					}
				}
			}
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x00058888 File Offset: 0x00056A88
		private int GetGroupCount(List<ExchangeVolume>[] groupings, VolumeSpareStatus status)
		{
			List<ExchangeVolume> list = groupings[(int)status];
			if (list != null)
			{
				return list.Count;
			}
			return 0;
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x000588A4 File Offset: 0x00056AA4
		private string GetGroupedVolumesString(List<ExchangeVolume>[] groupings, VolumeSpareStatus status)
		{
			string result = string.Empty;
			List<ExchangeVolume> list = groupings[(int)status];
			if (list != null)
			{
				list.Sort(ExchangeVolumeDbMountPointsComparer.Instance);
				StringBuilder stringBuilder = new StringBuilder(1000);
				foreach (ExchangeVolume exchangeVolume in list)
				{
					stringBuilder.AppendFormat("{0}  ( {1} )\n", exchangeVolume.GetMostAppropriateMountPoint(), exchangeVolume.VolumeName);
					foreach (MountedFolderPath mountedFolderPath in exchangeVolume.DatabaseMountPoints)
					{
						if (!MountedFolderPath.IsNullOrEmpty(mountedFolderPath))
						{
							stringBuilder.AppendFormat("\t{0}\n", mountedFolderPath.Path);
						}
					}
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x00058970 File Offset: 0x00056B70
		private void RefreshVolumes()
		{
			foreach (ExchangeVolume exchangeVolume in this.m_volumes)
			{
				exchangeVolume.Refresh();
			}
			this.DetermineVolumeSpareStatuses();
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x000589C4 File Offset: 0x00056BC4
		private void GetDatabaseNamesFromDirectory(DirectoryInfo di, string folderNameFormatStr, ref HashSet<string> dbNameSet, out Exception exception)
		{
			DirectoryInfo[] directoryArray = null;
			exception = null;
			if (dbNameSet == null)
			{
				dbNameSet = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
			}
			string folderSearchPattern = string.Format(folderNameFormatStr, '*');
			exception = MountPointUtil.HandleIOExceptions(delegate
			{
				directoryArray = di.GetDirectories(folderSearchPattern);
			});
			if (exception != null)
			{
				VolumeManager.Tracer.TraceError<string, string, Exception>((long)this.GetHashCode(), "GetDatabaseNamesFromDirectory(): GetDirectories() for directory '{0}' and search pattern '{1}' failed. Exception: {2}", di.FullName, folderSearchPattern, exception);
				return;
			}
			if (!directoryArray.Any<DirectoryInfo>())
			{
				VolumeManager.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "GetDatabaseNamesFromDirectory(): Directory '{0}' has no database folders with search pattern '{1}'.", di.FullName, folderSearchPattern);
				return;
			}
			foreach (DirectoryInfo directoryInfo in directoryArray)
			{
				string text = string.Format(folderNameFormatStr, "");
				string item = directoryInfo.Name.TrimEnd(text.ToCharArray());
				if (!dbNameSet.Contains(item))
				{
					dbNameSet.Add(item);
				}
			}
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x00058AE0 File Offset: 0x00056CE0
		private static string GetDatabaseNames(DatabaseSpareInfo[] dbInfos)
		{
			IEnumerable<string> values = (from info in dbInfos
			select info.DbName).SortDatabaseNames();
			return string.Join(", ", values);
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x00058B24 File Offset: 0x00056D24
		public static Exception CreateDatabaseFolders(string databaseName, ExchangeVolume volume)
		{
			string directory;
			string directory2;
			VolumeManager.GetDatabaseLogEdbFolderPaths(databaseName, volume, out directory, out directory2);
			Exception ex = DirectoryOperations.TryCreateDirectory(directory);
			if (ex != null)
			{
				return ex;
			}
			return DirectoryOperations.TryCreateDirectory(directory2);
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x00058B54 File Offset: 0x00056D54
		private static bool AreDatabaseFoldersNonExistentOrEmpty(string databaseName, ExchangeVolume volume)
		{
			string directory;
			string directory2;
			VolumeManager.GetDatabaseLogEdbFolderPaths(databaseName, volume, out directory, out directory2);
			Exception ex;
			return MountPointUtil.IsDirectoryNonExistentOrEmpty(directory, out ex) && MountPointUtil.IsDirectoryNonExistentOrEmpty(directory2, out ex);
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x00058B88 File Offset: 0x00056D88
		private static void GetDatabaseLogEdbFolderPaths(string databaseName, ExchangeVolume volume, out string logFolderPath, out string edbFolderPath)
		{
			string path;
			string path2;
			VolumeManager.GetDatabaseLogEdbFolderNames(databaseName, out path, out path2);
			logFolderPath = Path.Combine(volume.ExchangeVolumeMountPoint.Path, path);
			edbFolderPath = Path.Combine(volume.ExchangeVolumeMountPoint.Path, path2);
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x00058BC8 File Offset: 0x00056DC8
		private static bool TestWriteFile(string directoryPath, out Exception exception)
		{
			string filePath = Path.Combine(directoryPath, "VolumeManagerTestFile.txt");
			exception = null;
			try
			{
				using (SafeFileHandle safeFileHandle = VolumeManager.OpenTestFileHandle(filePath, out exception))
				{
					if (exception != null)
					{
						return false;
					}
					using (FileStream fileStream = new FileStream(safeFileHandle, FileAccess.ReadWrite))
					{
						using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.ASCII))
						{
							streamWriter.AutoFlush = true;
							streamWriter.Write(DateTimeHelper.ToPersistedString(DateTime.UtcNow));
							return true;
						}
					}
				}
			}
			catch (IOException ex)
			{
				exception = ex;
			}
			catch (SecurityException ex2)
			{
				exception = ex2;
			}
			catch (UnauthorizedAccessException ex3)
			{
				exception = ex3;
			}
			return exception == null;
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x00058CB0 File Offset: 0x00056EB0
		private static SafeFileHandle OpenTestFileHandle(string filePath, out Exception exception)
		{
			exception = null;
			SafeFileHandle safeFileHandle = NativeMethods.CreateFile(filePath, FileAccess.ReadWrite, FileShare.Read, IntPtr.Zero, FileMode.Create, FileFlags.FILE_FLAG_DELETE_ON_CLOSE, IntPtr.Zero);
			if (safeFileHandle == null || safeFileHandle.IsInvalid)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				exception = new Win32Exception(lastWin32Error);
				return null;
			}
			return safeFileHandle;
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x00058CF8 File Offset: 0x00056EF8
		private void AssignSpareToDatabase(ExchangeVolume spareVol, DatabaseSpareInfo dbInfo)
		{
			Exception ex = VolumeManager.CreateDatabaseFolders(dbInfo.DbName, spareVol);
			if (ex != null)
			{
				VolumeManager.Tracer.TraceError<string, MountedFolderPath, Exception>((long)this.GetHashCode(), "VolumeManager: AssignSpareToDatabase() failed to create LOG/EDB directories for database '{0}' on volume '{1}'. Exception: {2}", dbInfo.DbName, spareVol.VolumeName, ex);
				throw new CouldNotCreateDbDirectoriesException(dbInfo.DbName, spareVol.VolumeName.Path, ex.Message, ex);
			}
			VolumeManager.Tracer.TraceDebug<string, MountedFolderPath>((long)this.GetHashCode(), "VolumeManager: AssignSpareToDatabase() created LOG/EDB directories for database '{0}' on volume '{1}'.", dbInfo.DbName, spareVol.VolumeName);
			if (spareVol.DatabaseMountPoints.Contains(dbInfo.DatabaseMountPoint))
			{
				VolumeManager.Tracer.TraceDebug<string, MountedFolderPath>((long)this.GetHashCode(), "VolumeManager: AssignSpareToDatabase() skipping creating database mount point for database '{0}' at '{1}', because it already exists.", dbInfo.DbName, dbInfo.DatabaseMountPoint);
				return;
			}
			ex = DirectoryOperations.TryCreateDirectory(dbInfo.DatabaseMountPoint.Path);
			if (ex != null)
			{
				VolumeManager.Tracer.TraceError<MountedFolderPath, Exception>((long)this.GetHashCode(), "VolumeManager: AssignSpareToDatabase() failed to create database mountpoint directory '{0}'. Exception: {1}", dbInfo.DatabaseMountPoint, ex);
				throw new CouldNotCreateDbMountPointFolderException(dbInfo.DbName, dbInfo.DatabaseMountPoint.Path, ex.Message, ex);
			}
			ex = MountPointUtil.DeleteVolumeMountPoint(dbInfo.DatabaseMountPoint);
			if (ex != null)
			{
				VolumeManager.Tracer.TraceError<string, MountedFolderPath, Exception>((long)this.GetHashCode(), "VolumeManager: AssignSpareToDatabase() failed to delete database mount point for database '{0}' at '{1}'. Exception: {2}", dbInfo.DbName, dbInfo.DatabaseMountPoint, ex);
				throw new CouldNotDeleteDbMountPointException(dbInfo.DbName, dbInfo.DatabaseMountPoint.Path, ex.Message, ex);
			}
			VolumeManager.Tracer.TraceDebug<string, MountedFolderPath>((long)this.GetHashCode(), "VolumeManager: AssignSpareToDatabase() deleted database mount point for database '{0}' at '{1}'.", dbInfo.DbName, dbInfo.DatabaseMountPoint);
			ex = MountPointUtil.SetVolumeMountPoint(dbInfo.DatabaseMountPoint, spareVol.VolumeName);
			if (ex != null)
			{
				VolumeManager.Tracer.TraceError((long)this.GetHashCode(), "VolumeManager: AssignSpareToDatabase() failed to create database mount point for database '{0}' at '{1}' on volume '{2}'. Exception: {3}", new object[]
				{
					dbInfo.DbName,
					dbInfo.DatabaseMountPoint,
					spareVol.VolumeName,
					ex
				});
				throw new CouldNotCreateDbMountPointException(dbInfo.DbName, dbInfo.DatabaseMountPoint.Path, spareVol.VolumeName.Path, ex.Message, ex);
			}
			VolumeManager.Tracer.TraceDebug<string, MountedFolderPath, MountedFolderPath>((long)this.GetHashCode(), "VolumeManager: AssignSpareToDatabase() successfully created database mount point for database '{0}' at '{1}' on volume '{2}'.", dbInfo.DbName, dbInfo.DatabaseMountPoint, spareVol.VolumeName);
			ex = MountPointUtil.SetVolumeLabel(spareVol.ExchangeVolumeMountPoint, dbInfo.DbName);
			if (ex != null)
			{
				VolumeManager.Tracer.TraceError<MountedFolderPath, Exception>((long)this.GetHashCode(), "VolumeManager: AssignSpareToDatabase() failed to set label for volume '{0}'. Exception: {1}", spareVol.VolumeName, ex);
			}
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x00058F60 File Offset: 0x00057160
		private ExchangeVolume ChooseSpareVolume(DatabaseSpareInfo[] dbInfos, out bool isCachedAssignment)
		{
			ExchangeVolume exchangeVolume = null;
			string databaseNames = VolumeManager.GetDatabaseNames(dbInfos);
			isCachedAssignment = false;
			VolumeManager.VolumeAssignmentInfo volumeAssignmentInfo;
			if (this.m_assignedVolumes.TryGetValue(databaseNames, out volumeAssignmentInfo))
			{
				VolumeManager.Tracer.TraceDebug<MountedFolderPath, string, DateTime>((long)this.GetHashCode(), "VolumeManager: ChooseSpareVolume() found cached volume '{0}' for databases '{1}'. VolumeAssignmentTimeUtc = {2}.", volumeAssignmentInfo.AssignedVolume.VolumeName, databaseNames, volumeAssignmentInfo.AssignedTimeUtc);
				TimeSpan t = DateTime.UtcNow.Subtract(volumeAssignmentInfo.AssignedTimeUtc);
				if (t <= TimeSpan.FromSeconds((double)RegistryParameters.AutoReseedVolumeAssignmentCacheTTLInSecs))
				{
					isCachedAssignment = true;
					return volumeAssignmentInfo.AssignedVolume;
				}
			}
			foreach (ExchangeVolume exchangeVolume2 in this.m_volumes)
			{
				if (this.IsVolumePossibleSpare(exchangeVolume2, dbInfos))
				{
					VolumeManager.Tracer.TraceDebug<MountedFolderPath>((long)this.GetHashCode(), "VolumeManager: ChooseSpareVolume() is picking volume '{0}' as a spare.", exchangeVolume2.VolumeName);
					exchangeVolume = exchangeVolume2;
					break;
				}
			}
			if (exchangeVolume != null)
			{
				this.m_assignedVolumes[databaseNames] = new VolumeManager.VolumeAssignmentInfo(exchangeVolume);
			}
			return exchangeVolume;
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x0005908C File Offset: 0x0005728C
		private bool IsVolumePossibleSpare(ExchangeVolume volume, DatabaseSpareInfo[] dbInfos)
		{
			if (!volume.IsValid || !volume.IsExchangeVolume || !volume.IsAvailableAsSpare)
			{
				VolumeManager.Tracer.TraceError((long)this.GetHashCode(), "VolumeManager: IsVolumePossibleSpare() is skipping volume '{0}' because it is not usable as a spare. [IsValid={1}, IsExchangeVolume={2}, IsAvailableAsSpare={3}]", new object[]
				{
					volume.VolumeName,
					volume.IsValid,
					volume.IsExchangeVolume,
					volume.IsAvailableAsSpare
				});
				return false;
			}
			Exception arg;
			if (!VolumeManager.TestWriteFile(volume.ExchangeVolumeMountPoint.Path, out arg))
			{
				VolumeManager.Tracer.TraceError<MountedFolderPath, Exception>((long)this.GetHashCode(), "VolumeManager: IsVolumePossibleSpare() is skipping volume '{0}' because the disk write-file test failed. Exception: {1}", volume.VolumeName, arg);
				return false;
			}
			if (dbInfos != null)
			{
				if (!dbInfos.All((DatabaseSpareInfo dbInfo) => VolumeManager.AreDatabaseFoldersNonExistentOrEmpty(dbInfo.DbName, volume)))
				{
					VolumeManager.Tracer.TraceError<MountedFolderPath>((long)this.GetHashCode(), "VolumeManager: IsVolumePossibleSpare() is skipping volume '{0}' because there are some files/folders on it already.", volume.VolumeName);
					return false;
				}
				IEnumerable<string> databaseNamesFromVolume = this.GetDatabaseNamesFromVolume(volume, out arg);
				ICollection<string> dbNamesFromInfos = from dbInfo in dbInfos
				select dbInfo.DbName;
				IEnumerable<string> enumerable = from dbOnVol in databaseNamesFromVolume
				where !dbNamesFromInfos.Contains(dbOnVol, StringComparer.OrdinalIgnoreCase)
				select dbOnVol;
				if (enumerable.Count<string>() > 0)
				{
					VolumeManager.Tracer.TraceError<MountedFolderPath, string>((long)this.GetHashCode(), "VolumeManager: IsVolumePossibleSpare() is skipping volume '{0}' because there are other database files still present on it. The DiskReclaimer needs to format this volume before it can be used as a spare. Other databases: {1}", volume.VolumeName, string.Join(", ", enumerable));
					return false;
				}
			}
			VolumeManager.Tracer.TraceDebug<MountedFolderPath>((long)this.GetHashCode(), "VolumeManager: IsVolumePossibleSpare() is picking volume '{0}' as a possible spare.", volume.VolumeName);
			return true;
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x00059284 File Offset: 0x00057484
		public Exception FormatVolume(ExchangeVolume volume)
		{
			string path = volume.VolumeName.Path;
			VolumeManager.Tracer.TraceDebug<MountedFolderPath, string>((long)this.GetHashCode(), "VolumeManager: FormatVolume() is attempting to format volume '{0}' at mounted folder '{1}'.", volume.VolumeName, volume.ExchangeVolumeMountPoint.Path);
			if (!volume.IsValid || !volume.IsExchangeVolume || volume.DatabaseMountPoints.Length != 0)
			{
				VolumeManager.Tracer.TraceError<MountedFolderPath, string>((long)this.GetHashCode(), "VolumeManager: FormatVolume() is skipping formatting volume '{0}' at mounted folder '{1}' because it is invalid, not an Exchange volume, or has Database mount points.", volume.VolumeName, volume.ExchangeVolumeMountPoint.Path);
				return new VolumeNotSafeForFormatException(volume.VolumeName.Path, volume.ExchangeVolumeMountPoint.Path);
			}
			string queryString = string.Format("SELECT * FROM Win32_Volume WHERE DeviceId=\"{0}\"", path.Replace("\\", "\\\\"));
			Exception ex = null;
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(queryString))
				{
					using (ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get())
					{
						if (managementObjectCollection.Count != 1)
						{
							throw new CouldNotFindVolumeForFormatException();
						}
						foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
						{
							ManagementObject managementObject = (ManagementObject)managementBaseObject;
							managementObject.InvokeMethod("Format", new object[]
							{
								"NTFS",
								true,
								65536
							});
						}
					}
				}
			}
			catch (COMException ex2)
			{
				ex = new VolumeFormatFailedException(volume.VolumeName.Path, volume.ExchangeVolumeMountPoint.Path, ex2.Message, ex2);
			}
			catch (ManagementException ex3)
			{
				ex = new VolumeFormatFailedException(volume.VolumeName.Path, volume.ExchangeVolumeMountPoint.Path, ex3.Message, ex3);
			}
			catch (DatabaseVolumeInfoException ex4)
			{
				ex = new VolumeFormatFailedException(volume.VolumeName.Path, volume.ExchangeVolumeMountPoint.Path, ex4.Message, ex4);
			}
			if (ex != null)
			{
				VolumeManager.Tracer.TraceError<MountedFolderPath, string, Exception>((long)this.GetHashCode(), "VolumeManager: FormatVolume() failed to format volume '{0}' at mounted folder '{1}'. Error: {2}", volume.VolumeName, volume.ExchangeVolumeMountPoint.Path, ex);
			}
			else
			{
				VolumeManager.Tracer.TraceDebug<MountedFolderPath, string>((long)this.GetHashCode(), "VolumeManager: FormatVolume() successfully formatted volume '{0}' at mounted folder '{1}'.", volume.VolumeName, volume.ExchangeVolumeMountPoint.Path);
			}
			return ex;
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x00059500 File Offset: 0x00057700
		internal bool IsVolumeSafeToFormat(ExchangeVolume volume, ExchangeVolumeDbStatusInfo volStatusInfo, out Exception exception)
		{
			exception = null;
			string text;
			DateTime lastWriteTimeUtcInDirectory = MountPointUtil.GetLastWriteTimeUtcInDirectory(volume.ExchangeVolumeMountPoint.Path, true, out text, out exception);
			if (exception != null)
			{
				if (exception is IOException)
				{
					VolumeManager.Tracer.TraceDebug<MountedFolderPath, Exception>((long)this.GetHashCode(), "VolumeManager: IsVolumeSafeToFormat(): hit an IOException while enumerating directory contents. Returning true for volume '{0}' since format could fix up this volume. Error: {1}", volume.VolumeName, exception);
					return true;
				}
				VolumeManager.Tracer.TraceError<MountedFolderPath, string, Exception>((long)this.GetHashCode(), "VolumeManager: IsVolumeSafeToFormat(): Returning false for volume '{0}' with ExchangeVolumeMountPoint of '{1}' due to exception from MountPointUtil.GetLastWriteTimeUtcInDirectory(): {2}", volume.VolumeName, volume.ExchangeVolumeMountPoint.Path, exception);
				return false;
			}
			else
			{
				if (lastWriteTimeUtcInDirectory == DateTime.MaxValue)
				{
					VolumeManager.Tracer.TraceDebug<MountedFolderPath>((long)this.GetHashCode(), "VolumeManager: IsVolumeSafeToFormat(): Volume '{0}' is safe to format/reclaim since it is empty", volume.VolumeName);
					return true;
				}
				TimeSpan dataProtectionTimeWindow = this.GetDataProtectionTimeWindow(volStatusInfo);
				VolumeManager.Tracer.TraceDebug<TimeSpan, MountedFolderPath>((long)this.GetHashCode(), "VolumeManager: IsVolumeSafeToFormat(): Selecting a data protection delay of '{0}' for volume '{1}' based on its file contents.", dataProtectionTimeWindow, volume.VolumeName);
				TimeSpan timeSpan;
				if (!this.IsTimeStampSafeForFormat(lastWriteTimeUtcInDirectory, dataProtectionTimeWindow, out timeSpan))
				{
					VolumeManager.Tracer.TraceDebug((long)this.GetHashCode(), "VolumeManager: IsVolumeSafeToFormat(): Skipping volume '{0}' since the contents were updated recently within threshold of {2}. LastUpdateTimeUtc='{1}', LastUpdatePath='{3}'.", new object[]
					{
						volume.VolumeName,
						lastWriteTimeUtcInDirectory,
						dataProtectionTimeWindow,
						text
					});
					exception = new VolumeRecentlyModifiedException(volume.VolumeName.Path, dataProtectionTimeWindow, lastWriteTimeUtcInDirectory.ToString(), volume.ExchangeVolumeMountPoint.Path, text);
					return false;
				}
				VolumeManager.Tracer.TraceDebug((long)this.GetHashCode(), "VolumeManager: IsVolumeSafeToFormat(): Volume '{0}' is safe to format/reclaim. LastUpdateTimeUtc={1}, lastUpdateElapsed={2}, LastUpdatePath='{3}''", new object[]
				{
					volume.VolumeName,
					lastWriteTimeUtcInDirectory,
					timeSpan,
					text
				});
				return true;
			}
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x00059744 File Offset: 0x00057944
		internal List<string> GetDatabaseNamesFromEdbFilesOnVolume(ExchangeVolume volume, out Exception exception, bool ignoreSelectEdbs = true)
		{
			exception = null;
			List<string> dbNames = new List<string>();
			int count = 0;
			exception = MountPointUtil.HandleIOExceptions(delegate
			{
				DirectoryInfo path = new DirectoryInfo(volume.ExchangeVolumeMountPoint.Path);
				using (DirectoryEnumerator directoryEnumerator = new DirectoryEnumerator(path, true, false))
				{
					directoryEnumerator.ReturnBaseNames = true;
					IEnumerable<string> source = directoryEnumerator.EnumerateFiles("*.edb", DirectoryEnumerator.ExcludeHiddenAndSystemFilter);
					dbNames = (from file in source
					select Path.GetFileNameWithoutExtension(file)).ToList<string>();
					count = dbNames.Count;
				}
			});
			if (exception != null)
			{
				VolumeManager.Tracer.TraceError<MountedFolderPath, string>((long)this.GetHashCode(), "VolumeManager: GetDatabaseNameFromEdbFileOnVolume(): Volume '{0}' hit exception during enumeration. Error - {1}", volume.VolumeName, exception.Message);
				return dbNames;
			}
			if (count == 0)
			{
				VolumeManager.Tracer.TraceDebug<MountedFolderPath>((long)this.GetHashCode(), "VolumeManager: GetDatabaseNameFromEdbFileOnVolume(): Volume '{0}' does not contain edb files", volume.VolumeName);
				return dbNames;
			}
			VolumeManager.Tracer.TraceDebug<MountedFolderPath, string>((long)this.GetHashCode(), "VolumeManager: GetDatabaseNameFromEdbFileOnVolume(): Volume '{0}' is contains edb files of the following databases '{1}.", volume.VolumeName, dbNames.ToString());
			if (ignoreSelectEdbs)
			{
				dbNames.RemoveAll(new Predicate<string>(VolumeManager.MatchesDatabaseNamesToIgnore));
			}
			return dbNames;
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x00059836 File Offset: 0x00057A36
		internal static bool MatchesDatabaseNamesToIgnore(string name)
		{
			return VolumeManager.DatabaseNamesToIgnore.Contains(name, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x000599A8 File Offset: 0x00057BA8
		internal void GetUnknownFilesInformationFromVolume(ExchangeVolume volume, ref ExchangeVolumeDbStatusInfo volStatusInfo, out Exception exception)
		{
			exception = null;
			DirectoryInfo di = null;
			exception = MountPointUtil.HandleIOExceptions(delegate
			{
				di = new DirectoryInfo(volume.ExchangeVolumeMountPoint.Path);
			});
			volStatusInfo.UnknownFilesException = exception;
			if (exception != null)
			{
				VolumeManager.Tracer.TraceError<MountedFolderPath, Exception>((long)this.GetHashCode(), "VolumeManager: GetUnknownFilesInformationFromVolume(): Unable to enumerate files under path: '{0}'. Error: {1}", volume.ExchangeVolumeMountPoint, exception);
				return;
			}
			using (DirectoryEnumerator dirEnum = new DirectoryEnumerator(di, false, false))
			{
				ExchangeVolumeDbStatusInfo tempStatus = volStatusInfo;
				exception = MountPointUtil.HandleIOExceptions(delegate
				{
					IEnumerable<string> enumerable = dirEnum.EnumerateDirectories("*", DirectoryEnumerator.ExcludeHiddenAndSystemFilter);
					foreach (string text in enumerable)
					{
						if (!text.EndsWith(".db", StringComparison.InvariantCultureIgnoreCase) && !text.EndsWith(".log", StringComparison.InvariantCultureIgnoreCase))
						{
							Exception ex = null;
							if (MountPointUtil.DoesContainAnyFiles(text, true, out ex))
							{
								VolumeManager.Tracer.TraceDebug<string>((long)this.GetHashCode(), "VolumeManager: GetUnknownFilesInformationFromVolume(): Found unknown files in directory '{0}'.", text);
								tempStatus.UnknownFilesFound = true;
								break;
							}
						}
					}
					if (!tempStatus.UnknownFilesFound)
					{
						IEnumerable<string> source = dirEnum.EnumerateFiles("*", DirectoryEnumerator.ExcludeHiddenAndSystemFilter);
						if (source.Any<string>())
						{
							VolumeManager.Tracer.TraceDebug<MountedFolderPath>((long)this.GetHashCode(), "VolumeManager: GetUnknownFilesInformationFromVolume(): Found unknown files in directory '{0}'.", volume.ExchangeVolumeMountPoint);
							tempStatus.UnknownFilesFound = true;
						}
					}
				});
				volStatusInfo = tempStatus;
				volStatusInfo.UnknownFilesException = exception;
			}
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x00059AE8 File Offset: 0x00057CE8
		internal ExchangeVolumeDbStatusInfo GetDatabasesInformationFromVolume(ExchangeVolume volume, IEnumerable<IADDatabase> databases, IEnumerable<CopyStatusClientCachedEntry> serverCopyStatus, out Exception exception)
		{
			ExchangeVolumeDbStatusInfo result = default(ExchangeVolumeDbStatusInfo);
			exception = null;
			List<string> databaseNamesFromEdbFilesOnVolume = this.GetDatabaseNamesFromEdbFilesOnVolume(volume, out exception, true);
			result.DbFilesException = exception;
			if (exception != null)
			{
				VolumeManager.Tracer.TraceError<string, string>((long)this.GetHashCode(), "VolumeManager: AreDatabaseCopiesConfiguredAndHealthy() Unable to retrieve databases that exist on this volume: '{0}'. Error: {1}", volume.VolumeName.Path, exception.Message);
				return result;
			}
			if (!databaseNamesFromEdbFilesOnVolume.Any<string>())
			{
				VolumeManager.Tracer.TraceDebug<string>((long)this.GetHashCode(), "VolumeManager: AreDatabaseCopiesConfiguredAndHealthy() No databases that exist on this volume: '{0}'.", volume.VolumeName.Path);
				return result;
			}
			result.DbFilesFound = true;
			using (List<string>.Enumerator enumerator = databaseNamesFromEdbFilesOnVolume.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					VolumeManager.<>c__DisplayClass39 CS$<>8__locals1 = new VolumeManager.<>c__DisplayClass39();
					CS$<>8__locals1.dbName = enumerator.Current;
					IADDatabase dbTemp = null;
					if (databases != null)
					{
						dbTemp = (from db in databases
						where StringUtil.IsEqualIgnoreCase(db.Name, CS$<>8__locals1.dbName)
						select db).FirstOrDefault<IADDatabase>();
					}
					if (dbTemp == null)
					{
						VolumeManager.Tracer.TraceError<string>((long)this.GetHashCode(), "VolumeManager: AreDatabaseCopiesConfiguredAndHealthy() Database '{0}' found on the server does not exist in AD. Verify DAG configuration", CS$<>8__locals1.dbName);
						exception = new DatabasesMissingInADException(CS$<>8__locals1.dbName, volume.VolumeName.Path);
						result.DbMissingInAD = true;
						break;
					}
					CopyStatusClientCachedEntry copyStatusClientCachedEntry = null;
					if (serverCopyStatus != null)
					{
						copyStatusClientCachedEntry = (from cs in serverCopyStatus
						where cs.DbGuid.Equals(dbTemp.Guid)
						select cs).FirstOrDefault<CopyStatusClientCachedEntry>();
					}
					if (copyStatusClientCachedEntry == null || copyStatusClientCachedEntry.Result != CopyStatusRpcResult.Success)
					{
						VolumeManager.Tracer.TraceError<string>((long)this.GetHashCode(), "VolumeManager: AreDatabaseCopiesConfiguredAndHealthy() Unable to retrieve copy status table for the following databases in AD: '{0}'.", CS$<>8__locals1.dbName);
						exception = new DatabasesMissingInCopyStatusLookUpTable(CS$<>8__locals1.dbName);
						result.DbCopyStatusMissingOrFailedToRetrieve = true;
						break;
					}
					if (copyStatusClientCachedEntry.CopyStatus.CopyStatus != CopyStatusEnum.Healthy && copyStatusClientCachedEntry.CopyStatus.CopyStatus != CopyStatusEnum.Mounted)
					{
						exception = new DatabaseNotHealthyOnVolume(CS$<>8__locals1.dbName, volume.VolumeName.Path);
						result.DbCopyStatusNotHealthy = true;
						break;
					}
				}
			}
			result.DbFilesException = exception;
			return result;
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x00059D18 File Offset: 0x00057F18
		internal ExchangeVolumeDbStatusInfo GetFilesInformationFromVolume(ExchangeVolume volume, IEnumerable<IADDatabase> databases, IEnumerable<CopyStatusClientCachedEntry> serverCopyStatus)
		{
			Exception ex = null;
			ExchangeVolumeDbStatusInfo databasesInformationFromVolume = this.GetDatabasesInformationFromVolume(volume, databases, serverCopyStatus, out ex);
			this.GetUnknownFilesInformationFromVolume(volume, ref databasesInformationFromVolume, out ex);
			return databasesInformationFromVolume;
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x00059D40 File Offset: 0x00057F40
		public void UpdateVolumeInfoCopyState(Guid dbGuid, IReplicaInstanceManager rim = null)
		{
			if (rim == null)
			{
				VolumeManager.Tracer.TraceError<Guid>((long)this.GetHashCode(), "UpdateVolumeInfoCopyState: ReplicaInstanceManager was null. Skipping UpdateVolumeInfoCopyState() for database guid {0}.", dbGuid);
				return;
			}
			Exception ex = null;
			try
			{
				ISetVolumeInfo volumeInfoCallback = rim.GetVolumeInfoCallback(dbGuid, true);
				if (volumeInfoCallback == null)
				{
					VolumeManager.Tracer.TraceError<Guid>((long)this.GetHashCode(), "UpdateVolumeInfoCopyState: 'setVolume' callback was null. Skipping UpdateVolumeInfoCopyState() for database {0}.", dbGuid);
					return;
				}
				volumeInfoCallback.UpdateVolumeInfo();
			}
			catch (TaskServerTransientException ex2)
			{
				ex = ex2;
			}
			catch (TaskServerException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				VolumeManager.Tracer.TraceError<Guid, Exception>((long)this.GetHashCode(), "UpdateVolumeInfoCopyState: UpdateVolumeInfoCopyState() for database {0} failed. Exception: {1}", dbGuid, ex);
			}
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x00059DDC File Offset: 0x00057FDC
		public DatabaseVolumeInfo GetFirstMountedVolumeInfoInDatabaseGroup(string databaseGroupName, IEnumerable<IADDatabase> databases, IMonitoringADConfig adConfig)
		{
			if (StringUtil.IsStringNullOrEmptyOrWhiteSpace(databaseGroupName))
			{
				VolumeManager.Tracer.TraceDebug((long)this.GetHashCode(), "GetFirstMountedVolumeInfoInDatabaseGroup: No databasegroupname was passed in. Returning null.");
				return null;
			}
			if (databases == null)
			{
				VolumeManager.Tracer.TraceDebug((long)this.GetHashCode(), "GetFirstMountedVolumeInfoInDatabaseGroup: No databases were passed in. Returning null.");
				return null;
			}
			IEnumerable<IADDatabase> databaseGroupMembers = this.GetDatabaseGroupMembers(databases, databaseGroupName);
			if (databaseGroupMembers == null)
			{
				VolumeManager.Tracer.TraceDebug<string>((long)this.GetHashCode(), "GetFirstMountedVolumeInfoInDatabaseGroup: No databases were found for databasegroup: '{0}'.", databaseGroupName);
				return null;
			}
			DatabaseVolumeInfo result = null;
			Exception ex = null;
			foreach (IADDatabase iaddatabase in databaseGroupMembers)
			{
				DatabaseVolumeInfo instance = DatabaseVolumeInfo.GetInstance(iaddatabase.EdbFilePath.PathName, iaddatabase.LogFolderPath.PathName, iaddatabase.Name, adConfig.Dag.AutoDagVolumesRootFolderPath.PathName, adConfig.Dag.AutoDagDatabasesRootFolderPath.PathName, adConfig.Dag.AutoDagDatabaseCopiesPerVolume);
				if (instance.IsValid && MountPointUtil.IsDirectoryAccessibleMountPoint(instance.DatabaseVolumeMountPoint.Path, out ex))
				{
					result = instance;
					break;
				}
			}
			return result;
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x00059F18 File Offset: 0x00058118
		public IEnumerable<IADDatabase> GetDatabaseGroupMembers(IEnumerable<IADDatabase> databases, string databaseGroupName)
		{
			if (databases == null)
			{
				VolumeManager.Tracer.TraceDebug((long)this.GetHashCode(), "GetDatabaseGroupMembers: No databases were passed in. Returning null.");
				return null;
			}
			if (StringUtil.IsStringNullOrEmptyOrWhiteSpace(databaseGroupName))
			{
				VolumeManager.Tracer.TraceDebug((long)this.GetHashCode(), "GetDatabaseGroupMembers: No databasegroupname was passed in. Returning null.");
				return null;
			}
			return from db in databases
			where StringUtil.IsEqualIgnoreCase(databaseGroupName, db.DatabaseGroup)
			select db;
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x00059F88 File Offset: 0x00058188
		internal bool CreateMountPointDirectory(MountedFolderPath dbMountPath, out Exception exception)
		{
			exception = null;
			bool result = false;
			if (dbMountPath == null)
			{
				exception = new VolumeMountPathDoesNotExistException();
				return result;
			}
			if (MountPointUtil.IsDirectoryAccessibleMountPoint(dbMountPath.Path, out exception))
			{
				VolumeManager.Tracer.TraceDebug((long)this.GetHashCode(), "DiskReclaimer: Directory is already a valid accessible mountpoint. Skip this mountpoint.");
				if (exception == null)
				{
					exception = new PathIsAlreadyAValidMountPoint(dbMountPath.Path);
				}
				return true;
			}
			exception = null;
			try
			{
				if (Directory.Exists(dbMountPath.Path))
				{
					exception = MountPointUtil.DeleteVolumeMountPoint(dbMountPath);
				}
				else
				{
					Directory.CreateDirectory(dbMountPath.Path);
				}
				result = true;
			}
			catch (ArgumentException ex)
			{
				exception = ex;
			}
			catch (IOException ex2)
			{
				exception = ex2;
			}
			catch (UnauthorizedAccessException ex3)
			{
				exception = ex3;
			}
			return result;
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x0005A0A0 File Offset: 0x000582A0
		public bool FixActiveDatabaseMountPoint(IADDatabase database, IEnumerable<IADDatabase> databases, IMonitoringADConfig adConfig, out Exception exception, bool checkDatabaseGroupExists = true)
		{
			bool flag = false;
			exception = null;
			DatabaseVolumeInfo databaseVolumeInfo = null;
			if (database == null)
			{
				VolumeManager.Tracer.TraceDebug((long)this.GetHashCode(), "FixActiveDatabaseMountPoint: No database was passed in. Return false.");
				return false;
			}
			if (checkDatabaseGroupExists && StringUtil.IsStringNullOrEmptyOrWhiteSpace(database.DatabaseGroup))
			{
				VolumeManager.Tracer.TraceDebug<string>((long)this.GetHashCode(), "FixActiveDatabaseMountPoint: DatabaseGroup hasn't been set for database '{0}' yet. Return false.", database.Name);
				exception = new DatabaseGroupNotSetException(database.Name);
				return false;
			}
			MountedFolderPath mountedFolderPath = new MountedFolderPath(Path.Combine(adConfig.Dag.AutoDagDatabasesRootFolderPath.PathName, database.Name));
			flag = this.CreateMountPointDirectory(mountedFolderPath, out exception);
			if (flag && exception != null && exception is PathIsAlreadyAValidMountPoint)
			{
				VolumeManager.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "FixActiveDatabaseMountPoint: MountPath exists and is a valid mountpoint: '{0}'. Error: {1}", mountedFolderPath.Path, exception.Message);
				return true;
			}
			if (!flag)
			{
				VolumeManager.Tracer.TraceError<string, string>((long)this.GetHashCode(), "FixActiveDatabaseMountPoint: Unable to create mount point directory: '{0}'. Error: {1}", mountedFolderPath.Path, exception.Message);
				return flag;
			}
			exception = MountPointUtil.HandleIOExceptions(delegate
			{
				if (Directory.Exists(database.LogFolderPath.ToString()) && !File.Exists(database.EdbFilePath.ToString()))
				{
					Directory.Delete(database.LogFolderPath.ToString(), true);
				}
			});
			if (exception != null)
			{
				VolumeManager.Tracer.TraceError<string, string>((long)this.GetHashCode(), "FixActiveDatabaseMountPoint: Failed cleaning database log folder: '{0}'. Error: {1}", database.LogFolderPath.ToString(), exception.Message);
			}
			flag = false;
			if (databases != null)
			{
				databaseVolumeInfo = this.GetFirstMountedVolumeInfoInDatabaseGroup(database.DatabaseGroup, databases, adConfig);
			}
			if (databaseVolumeInfo != null)
			{
				exception = MountPointUtil.SetVolumeMountPoint(mountedFolderPath, databaseVolumeInfo.DatabaseVolumeName);
				if (exception != null)
				{
					VolumeManager.Tracer.TraceError<string, MountedFolderPath, string>((long)this.GetHashCode(), "FixActiveDatabaseMountPoint: Unable to set mountpoint: '{0}' to volume: '{1}. Error: {2}", mountedFolderPath.Path, databaseVolumeInfo.DatabaseVolumeName, exception.Message);
					return flag;
				}
				exception = MountPointUtil.SetVolumeLabel(mountedFolderPath, database.Name);
				if (exception != null)
				{
					VolumeManager.Tracer.TraceError<string, MountedFolderPath, string>((long)this.GetHashCode(), "FixActiveDatabaseMountPoint: Unable to set label: '{0}' to volume: '{1}. Error: {2}", database.Name, databaseVolumeInfo.DatabaseVolumeName, exception.Message);
					return flag;
				}
				ExchangeVolume instance = ExchangeVolume.GetInstance(databaseVolumeInfo.DatabaseVolumeName, adConfig.Dag.AutoDagVolumesRootFolderPath.PathName, adConfig.Dag.AutoDagDatabasesRootFolderPath.PathName, adConfig.Dag.AutoDagDatabaseCopiesPerVolume);
				exception = VolumeManager.CreateDatabaseFolders(database.Name, instance);
				if (exception != null)
				{
					VolumeManager.Tracer.TraceError<string, string>((long)this.GetHashCode(), "FixActiveDatabaseMountPoint: Unable to create db and log folders for database: '{0}'. Error: {1}", database.Name, exception.Message);
					return flag;
				}
				flag = true;
				VolumeManager.Tracer.TraceDebug<string, MountedFolderPath>((long)this.GetHashCode(), "FixActiveDatabaseMountPoint: Created mountpoint: '{0}' for volume: '{1}' and internal db and log folders.", mountedFolderPath.Path, databaseVolumeInfo.DatabaseVolumeName);
			}
			else
			{
				VolumeManager.Tracer.TraceDebug((long)this.GetHashCode(), "FixActiveDatabaseMountPoint: No database in group has mountpoints. Attempting to assign a spare.");
				DatabaseSpareInfo[] dbInfos = new DatabaseSpareInfo[]
				{
					new DatabaseSpareInfo(database.Name, mountedFolderPath)
				};
				try
				{
					this.AssignSpare(dbInfos);
				}
				catch (DatabaseVolumeInfoException ex)
				{
					exception = ex;
				}
				if (exception != null)
				{
					VolumeManager.Tracer.TraceError<string, string>((long)this.GetHashCode(), "FixActiveDatabaseMountPoint: Unable to allocate spare volume for database: '{0}'. Error: {1}", database.Name, exception.Message);
					return flag;
				}
				flag = true;
				VolumeManager.Tracer.TraceDebug<string>((long)this.GetHashCode(), "FixActiveDatabaseMountPoint: Allocated new spare volume for database: '{0}'.", database.Name);
			}
			return flag;
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x0005A460 File Offset: 0x00058660
		internal List<IADDatabase> GetNeverMountedActives(IEnumerable<IADDatabase> databases, IMonitoringADConfig adConfig, IEnumerable<CopyStatusClientCachedEntry> serverStatusResults)
		{
			if (databases == null || serverStatusResults == null)
			{
				VolumeManager.Tracer.TraceError((long)this.GetHashCode(), "GetNeverMountedActivesWithoutVolumeMountPoint: Either databases or serverStatusResults was null. Return null");
				return null;
			}
			List<IADDatabase> list = new List<IADDatabase>();
			IEnumerable<CopyStatusClientCachedEntry> enumerable = from copy in serverStatusResults
			where copy.IsActive
			select copy;
			using (IEnumerator<CopyStatusClientCachedEntry> enumerator = enumerable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					CopyStatusClientCachedEntry active = enumerator.Current;
					IADDatabase iaddatabase = (from db in databases
					where active.DbGuid.Equals(db.Guid) && !db.DatabaseCreated
					select db).FirstOrDefault<IADDatabase>();
					if (iaddatabase == null)
					{
						VolumeManager.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "GetNeverMountedActivesWithoutVolumeMountPoint: DatabaseGuid: '{0}' was mounted before and edb was created. Skip this database", active.DbGuid);
					}
					else
					{
						list.Add(iaddatabase);
						VolumeManager.Tracer.TraceDebug<string>((long)this.GetHashCode(), "GetNeverMountedActivesWithoutVolumeMountPoint: Found never mounted active copy of database: '{0}'.", iaddatabase.Name);
					}
				}
			}
			return list;
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x0005A56C File Offset: 0x0005876C
		private TimeSpan GetDataProtectionTimeWindow(ExchangeVolumeDbStatusInfo volStatusInfo)
		{
			TimeSpan result = DiskReclaimerManager.DiskReclaimerSpareDelayShort;
			if (volStatusInfo.UnknownFilesFound)
			{
				result = DiskReclaimerManager.DiskReclaimerSpareDelayLong;
			}
			else if (volStatusInfo.DbFilesFound)
			{
				if (volStatusInfo.DbMissingInAD)
				{
					result = DiskReclaimerManager.DiskReclaimerSpareDelayLong;
				}
				else if (volStatusInfo.DbCopyStatusMissingOrFailedToRetrieve || volStatusInfo.DbCopyStatusNotHealthy)
				{
					result = DiskReclaimerManager.DiskReclaimerSpareDelayMedium;
				}
			}
			return result;
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x0005A5C4 File Offset: 0x000587C4
		private bool IsTimeStampSafeForFormat(DateTime lastUpdateUtc, TimeSpan protectionDelay, out TimeSpan lastUpdateElapsed)
		{
			lastUpdateElapsed = DateTime.UtcNow.Subtract(lastUpdateUtc);
			return lastUpdateElapsed > protectionDelay;
		}

		// Token: 0x0400089F RID: 2207
		public const int DiskClusterSize = 65536;

		// Token: 0x040008A0 RID: 2208
		public const string LogFolderNameSuffix = ".log";

		// Token: 0x040008A1 RID: 2209
		public const string EdbFolderNameSuffix = ".db";

		// Token: 0x040008A2 RID: 2210
		public const string LogFolderNameFormatStr = "{0}.log";

		// Token: 0x040008A3 RID: 2211
		public const string EdbFolderNameFormatStr = "{0}.db";

		// Token: 0x040008A4 RID: 2212
		public const string EdbFileNameExtensionStr = ".edb";

		// Token: 0x040008A5 RID: 2213
		public const string EdbFileNameSearchFilter = "*.edb";

		// Token: 0x040008A6 RID: 2214
		public static List<string> DatabaseNamesToIgnore = new List<string>
		{
			"tmp"
		};

		// Token: 0x040008A7 RID: 2215
		private static VolumeManager m_defaultInstance = null;

		// Token: 0x040008A8 RID: 2216
		private static object lockObjectForSingleton = new object();

		// Token: 0x040008A9 RID: 2217
		private ExchangeVolume[] m_volumes;

		// Token: 0x040008AA RID: 2218
		private Dictionary<MountedFolderPath, ExchangeVolume> m_volumeTable;

		// Token: 0x040008AB RID: 2219
		private Dictionary<string, VolumeManager.VolumeAssignmentInfo> m_assignedVolumes;

		// Token: 0x040008AC RID: 2220
		private DateTime refreshTimeInUtc = DateTime.MinValue;

		// Token: 0x040008AD RID: 2221
		private object m_lockObjectForRefreshIfTooStale = new object();

		// Token: 0x040008AE RID: 2222
		private object m_lockObjectForReadWrite = new object();

		// Token: 0x02000241 RID: 577
		private class VolumeAssignmentInfo
		{
			// Token: 0x17000628 RID: 1576
			// (get) Token: 0x0600161E RID: 5662 RVA: 0x0005A62C File Offset: 0x0005882C
			// (set) Token: 0x0600161F RID: 5663 RVA: 0x0005A634 File Offset: 0x00058834
			public ExchangeVolume AssignedVolume { get; private set; }

			// Token: 0x17000629 RID: 1577
			// (get) Token: 0x06001620 RID: 5664 RVA: 0x0005A63D File Offset: 0x0005883D
			// (set) Token: 0x06001621 RID: 5665 RVA: 0x0005A645 File Offset: 0x00058845
			public DateTime AssignedTimeUtc { get; private set; }

			// Token: 0x06001622 RID: 5666 RVA: 0x0005A64E File Offset: 0x0005884E
			public VolumeAssignmentInfo(ExchangeVolume assignedVolume)
			{
				this.AssignedVolume = assignedVolume;
				this.AssignedTimeUtc = DateTime.UtcNow;
			}
		}
	}
}
