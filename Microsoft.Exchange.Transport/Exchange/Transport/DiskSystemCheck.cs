using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000018 RID: 24
	internal sealed class DiskSystemCheck : ISystemCheck
	{
		// Token: 0x06000084 RID: 132 RVA: 0x000032AF File Offset: 0x000014AF
		public DiskSystemCheck(SystemCheckConfig systemCheckConfig, TransportAppConfig transportAppConfig, ITransportConfiguration transportConfiguration) : this(new DiskSystemCheckUtilsWrapper(), systemCheckConfig, transportAppConfig, transportConfiguration)
		{
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000032C0 File Offset: 0x000014C0
		public DiskSystemCheck(IDiskSystemCheckUtilsWrapper diskSystemCheckUtil, SystemCheckConfig systemCheckConfig, TransportAppConfig transportAppConfig, ITransportConfiguration transportConfiguration)
		{
			ArgumentValidator.ThrowIfNull("systemCheckUtil", diskSystemCheckUtil);
			ArgumentValidator.ThrowIfNull("systemCheckConfig", systemCheckConfig);
			ArgumentValidator.ThrowIfNull("transportAppConfig", transportAppConfig);
			ArgumentValidator.ThrowIfNull("transportConfiguration", transportConfiguration);
			this.diskSystemCheckUtil = diskSystemCheckUtil;
			this.systemCheckConfig = systemCheckConfig;
			this.transportAppConfig = transportAppConfig;
			this.transportConfiguration = transportConfiguration;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000086 RID: 134 RVA: 0x0000331D File Offset: 0x0000151D
		public bool Enabled
		{
			get
			{
				return this.systemCheckConfig.IsDiskSystemCheckEnabled;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000087 RID: 135 RVA: 0x0000332A File Offset: 0x0000152A
		public IEnumerable<string> CheckedVolumes
		{
			get
			{
				return this.checkedVolumes;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003332 File Offset: 0x00001532
		public int LastLockedVolumeCheckRetryCount
		{
			get
			{
				return this.lastLockedVolumeCheckRetryCount;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000333A File Offset: 0x0000153A
		public string LocalizedLockedVolumeError
		{
			get
			{
				return this.localizedLockedVolumeError;
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003344 File Offset: 0x00001544
		public void Check()
		{
			if (this.Enabled)
			{
				List<string> list = new List<string>
				{
					this.transportAppConfig.WorkerProcess.TemporaryStoragePath,
					this.transportAppConfig.QueueDatabase.DatabasePath,
					this.transportAppConfig.QueueDatabase.LogFilePath
				};
				if (this.transportConfiguration != null && this.transportConfiguration.LocalServer != null && this.transportConfiguration.LocalServer.TransportServer != null)
				{
					Server transportServer = this.transportConfiguration.LocalServer.TransportServer;
					list.Add((transportServer.MessageTrackingLogPath != null) ? transportServer.MessageTrackingLogPath.PathName : null);
					list.Add((transportServer.ReceiveProtocolLogPath != null) ? transportServer.ReceiveProtocolLogPath.PathName : null);
					list.Add((transportServer.SendProtocolLogPath != null) ? transportServer.SendProtocolLogPath.PathName : null);
				}
				this.CheckPathsNotOnLockedVolumes(list);
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003454 File Offset: 0x00001654
		public void CheckPathsNotOnLockedVolumes(IList<string> transportDataPaths)
		{
			ArgumentValidator.ThrowIfNull("transportDataPaths", transportDataPaths);
			this.checkedVolumes = new List<string>();
			this.lastLockedVolumeCheckRetryCount = 0;
			foreach (string path in transportDataPaths)
			{
				if (Path.IsPathRooted(path) && !this.checkedVolumes.Contains(Path.GetPathRoot(path)))
				{
					this.checkedVolumes.Add(Path.GetPathRoot(path));
					this.lastLockedVolumeCheckRetryCount = 1;
					while (this.IsPathOnLockedVolume(path))
					{
						if (++this.lastLockedVolumeCheckRetryCount > this.systemCheckConfig.LockedVolumeCheckRetryCount)
						{
							throw new TransportComponentLoadFailedException(this.localizedLockedVolumeError, new TransientException(new LocalizedString(this.localizedLockedVolumeError)));
						}
						Thread.Sleep(this.systemCheckConfig.LockedVolumeCheckRetryInterval);
					}
				}
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003544 File Offset: 0x00001744
		private bool IsPathOnLockedVolume(string path)
		{
			Exception ex = null;
			this.localizedLockedVolumeError = string.Empty;
			bool flag = this.diskSystemCheckUtil.IsFilePathOnLockedVolume(path, out ex);
			if (flag || ex != null)
			{
				if (ex != null)
				{
					this.localizedLockedVolumeError = Strings.BitlockerQueryFailed(path, ex.ToString(), this.systemCheckConfig.LockedVolumeCheckRetryInterval.Seconds, this.lastLockedVolumeCheckRetryCount, this.systemCheckConfig.LockedVolumeCheckRetryCount);
					ExTraceGlobals.ConfigurationTracer.TraceError(0L, this.localizedLockedVolumeError);
					Components.EventLogger.LogEvent(TransportEventLogConstants.Tuple_BitlockerQueryFailed, null, new object[]
					{
						path,
						ex.ToString(),
						this.systemCheckConfig.LockedVolumeCheckRetryInterval.Seconds,
						this.lastLockedVolumeCheckRetryCount,
						this.systemCheckConfig.LockedVolumeCheckRetryCount
					});
				}
				else
				{
					this.localizedLockedVolumeError = Strings.FilePathOnLockedVolume(path, this.systemCheckConfig.LockedVolumeCheckRetryInterval.Seconds, this.lastLockedVolumeCheckRetryCount, this.systemCheckConfig.LockedVolumeCheckRetryCount);
					ExTraceGlobals.ConfigurationTracer.TraceError(0L, this.localizedLockedVolumeError);
					Components.EventLogger.LogEvent(TransportEventLogConstants.Tuple_FilePathOnLockedVolume, null, new object[]
					{
						path,
						this.systemCheckConfig.LockedVolumeCheckRetryInterval.Seconds,
						this.lastLockedVolumeCheckRetryCount,
						this.systemCheckConfig.LockedVolumeCheckRetryCount
					});
				}
			}
			return flag;
		}

		// Token: 0x04000039 RID: 57
		private IDiskSystemCheckUtilsWrapper diskSystemCheckUtil;

		// Token: 0x0400003A RID: 58
		private SystemCheckConfig systemCheckConfig;

		// Token: 0x0400003B RID: 59
		private TransportAppConfig transportAppConfig;

		// Token: 0x0400003C RID: 60
		private ITransportConfiguration transportConfiguration;

		// Token: 0x0400003D RID: 61
		private string localizedLockedVolumeError;

		// Token: 0x0400003E RID: 62
		private List<string> checkedVolumes;

		// Token: 0x0400003F RID: 63
		private int lastLockedVolumeCheckRetryCount;
	}
}
