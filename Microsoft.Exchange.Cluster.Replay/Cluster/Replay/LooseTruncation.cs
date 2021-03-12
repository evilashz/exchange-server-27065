using System;
using System.IO;
using System.Security;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002E9 RID: 745
	internal class LooseTruncation
	{
		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06001DF9 RID: 7673 RVA: 0x0008968A File Offset: 0x0008788A
		// (set) Token: 0x06001DFA RID: 7674 RVA: 0x00089692 File Offset: 0x00087892
		public bool Enabled { get; set; }

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06001DFB RID: 7675 RVA: 0x0008969B File Offset: 0x0008789B
		// (set) Token: 0x06001DFC RID: 7676 RVA: 0x000896A3 File Offset: 0x000878A3
		public int MinCopiesProtect { get; set; }

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06001DFD RID: 7677 RVA: 0x000896AC File Offset: 0x000878AC
		// (set) Token: 0x06001DFE RID: 7678 RVA: 0x000896B4 File Offset: 0x000878B4
		public int MinLogsToPreserve { get; set; }

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06001DFF RID: 7679 RVA: 0x000896BD File Offset: 0x000878BD
		// (set) Token: 0x06001E00 RID: 7680 RVA: 0x000896C5 File Offset: 0x000878C5
		public long LowSpaceThresholdInMB { get; set; }

		// Token: 0x06001E01 RID: 7681 RVA: 0x000896CE File Offset: 0x000878CE
		public LooseTruncation()
		{
			this.InitFromRegistry();
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x000896DC File Offset: 0x000878DC
		private void InitFromRegistry()
		{
			Exception ex = null;
			this.Enabled = false;
			this.MinCopiesProtect = 0;
			this.MinLogsToPreserve = 100000;
			this.LowSpaceThresholdInMB = 200000L;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\BackupInformation\\"))
				{
					if (registryKey != null)
					{
						object value = registryKey.GetValue("LooseTruncation_MinCopiesToProtect");
						if (value != null && value is int)
						{
							int num = (int)value;
							if (num <= 0)
							{
								return;
							}
							this.MinCopiesProtect = num;
						}
						value = registryKey.GetValue("LooseTruncation_MinLogsToProtect");
						if (value != null && value is int)
						{
							int num = (int)value;
							if (num >= 0)
							{
								this.MinLogsToPreserve = num;
							}
						}
						value = registryKey.GetValue("LooseTruncation_MinDiskFreeSpaceThresholdInMB");
						if (value != null && value is int)
						{
							int num = (int)value;
							if (num <= 0)
							{
								return;
							}
							this.LowSpaceThresholdInMB = (long)num;
						}
						this.Enabled = true;
					}
				}
			}
			catch (SecurityException ex2)
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
			if (ex != null)
			{
				LooseTruncation.Tracer.TraceError<Exception>(0L, "LooseTruncation failed to read registry: {0}", ex);
			}
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x00089820 File Offset: 0x00087A20
		public bool SpaceIsLow(IReplayConfiguration config)
		{
			string destinationLogPath = config.DestinationLogPath;
			ulong num;
			ulong num2;
			Exception freeSpace = DiskHelper.GetFreeSpace(destinationLogPath, out num, out num2);
			if (freeSpace != null)
			{
				LooseTruncation.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "SpaceIsLow: GetFreeSpace() failed against directory '{0}'. Exception: {1}", destinationLogPath, freeSpace);
				ReplayCrimsonEvents.TruncationFailedToGetDiskSpace.LogPeriodic<string, string, string>(config.Identity, DiagCore.DefaultEventSuppressionInterval, config.Identity, freeSpace.Message, destinationLogPath);
				return false;
			}
			bool flag = false;
			long num3 = (long)(num2 / 1048576UL);
			if (num3 < this.LowSpaceThresholdInMB)
			{
				flag = true;
			}
			LooseTruncation.Tracer.TraceDebug<bool, long, long>((long)this.GetHashCode(), "SpaceIsLow={0}. FreeMB={1} LowSpaceThresholdInMB={2}", flag, num3, this.LowSpaceThresholdInMB);
			return flag;
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x000898BC File Offset: 0x00087ABC
		public long MinRequiredGen(IFileChecker fileChecker, IReplayConfiguration config)
		{
			long num = fileChecker.FileState.LowestGenerationRequired;
			if (!config.CircularLoggingEnabled && fileChecker.FileState.LastGenerationBackedUp != 0L)
			{
				num = Math.Min(num, fileChecker.FileState.LastGenerationBackedUp);
			}
			long copyNotificationGenerationNumber = config.ReplayState.CopyNotificationGenerationNumber;
			if (copyNotificationGenerationNumber <= (long)this.MinLogsToPreserve)
			{
				return 0L;
			}
			long num2 = (long)(this.MinLogsToPreserve / 10);
			if (num <= num2)
			{
				return 0L;
			}
			return Math.Min(copyNotificationGenerationNumber - (long)this.MinLogsToPreserve, num - num2);
		}

		// Token: 0x04000C9C RID: 3228
		private const string RegValName_LooseTruncation_MinDiskFreeSpaceInMB = "LooseTruncation_MinDiskFreeSpaceThresholdInMB";

		// Token: 0x04000C9D RID: 3229
		private const string RegValName_LooseTruncation_MinCopiesToProtect = "LooseTruncation_MinCopiesToProtect";

		// Token: 0x04000C9E RID: 3230
		private const string RegValName_LooseTruncation_MinLogsToProtect = "LooseTruncation_MinLogsToProtect";

		// Token: 0x04000C9F RID: 3231
		private const string RegistryRootName = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\BackupInformation\\";

		// Token: 0x04000CA0 RID: 3232
		private static readonly Trace Tracer = ExTraceGlobals.LogTruncaterTracer;
	}
}
