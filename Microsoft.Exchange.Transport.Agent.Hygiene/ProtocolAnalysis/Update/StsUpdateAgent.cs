using System;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StsUpdate;
using Microsoft.Exchange.Transport.Agent.AntiSpam.Common;
using Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.Configuration;
using Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.Update
{
	// Token: 0x02000063 RID: 99
	internal class StsUpdateAgent
	{
		// Token: 0x060002B6 RID: 694 RVA: 0x000123C4 File Offset: 0x000105C4
		public static StsUpdateAgent.VersionComparison CompareAndSetCurrentVersion(string major, string minor)
		{
			string a = major + minor;
			if (string.IsNullOrEmpty(StsUpdateAgent.version) || a != StsUpdateAgent.version)
			{
				StsUpdateAgent.version = a;
				return StsUpdateAgent.VersionComparison.NotEq;
			}
			return StsUpdateAgent.VersionComparison.Eq;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x000123FB File Offset: 0x000105FB
		public static bool EndProcessing
		{
			get
			{
				return StsUpdateAgent.shuttingDown;
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00012402 File Offset: 0x00010602
		public void Shutdown()
		{
			this.OnShutdownHandler();
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0001240A File Offset: 0x0001060A
		public void Startup()
		{
			this.OnStartupHandler();
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00012414 File Offset: 0x00010614
		public StsUpdateAgent()
		{
			StsUpdateAgent.updateAgentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			StsUpdateAgent.updateAgentDatFullFileName = Path.Combine(StsUpdateAgent.updateAgentPath, "Microsoft.SmartScreen.DomainRep.dat");
			StsUpdateAgent.updateAgentSparamFullFileName = Path.Combine(StsUpdateAgent.updateAgentPath, "Microsoft.SmartScreen.Sparam.dat");
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00012470 File Offset: 0x00010670
		private void FileWatcherStart()
		{
			this.dataFileWatcher = new FileSystemWatcher(StsUpdateAgent.updateAgentPath, "Microsoft.SmartScreen.DomainRep.dat");
			this.dataFileWatcher.NotifyFilter = (NotifyFilters.FileName | NotifyFilters.LastWrite);
			this.dataFileWatcher.Changed += this.OnDataFileChanged;
			this.dataFileWatcher.Created += this.OnDataFileChanged;
			this.dataFileWatcher.Renamed += new RenamedEventHandler(this.OnDataFileChanged);
			this.dataFileWatcher.EnableRaisingEvents = true;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x000124F0 File Offset: 0x000106F0
		private void OnStartupHandler()
		{
			this.FileWatcherStart();
			ExTraceGlobals.AgentTracer.TraceDebug((long)this.GetHashCode(), "OnStartupHandler");
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0001250E File Offset: 0x0001070E
		private void OnShutdownHandler()
		{
			StsUpdateAgent.shuttingDown = true;
			if (this.dataFileWatcher != null)
			{
				this.dataFileWatcher = null;
			}
			Database.Detach();
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0001252C File Offset: 0x0001072C
		private void ProcessNewData()
		{
			FileStream fileStream = null;
			byte[] data = null;
			BinaryReader binaryReader = null;
			try
			{
				if (!File.Exists(StsUpdateAgent.updateAgentDatFullFileName))
				{
					ExTraceGlobals.AgentTracer.TraceError((long)this.GetHashCode(), "ProcessNewData: data file does not exist.");
					return;
				}
				fileStream = new FileStream(StsUpdateAgent.updateAgentDatFullFileName, FileMode.Open, FileAccess.Read, FileShare.Read | FileShare.Delete);
				FileInfo fileInfo = new FileInfo(StsUpdateAgent.updateAgentDatFullFileName);
				binaryReader = new BinaryReader(fileStream);
				if (fileInfo.Length > 2147483647L)
				{
					ExTraceGlobals.AgentTracer.TraceError((long)this.GetHashCode(), "ProcessNewData: failed to read the file. File size is larger than 2 GB");
					StsUpdateAgent.EventLogger.LogEvent(AgentsEventLogConstants.Tuple_UpdateAgentFileNotLoaded, null, null);
					return;
				}
				data = binaryReader.ReadBytes((int)fileInfo.Length);
			}
			catch (IOException ex)
			{
				ExTraceGlobals.AgentTracer.TraceError<string>((long)this.GetHashCode(), "ProcessNewData: failed to read the file. {0}", ex.Message);
				StsUpdateAgent.EventLogger.LogEvent(AgentsEventLogConstants.Tuple_UpdateAgentFileNotLoaded, null, null);
				return;
			}
			catch (UnauthorizedAccessException ex2)
			{
				ExTraceGlobals.AgentTracer.TraceError<string>((long)this.GetHashCode(), "ProcessNewData: failed to read the file. {0}", ex2.Message);
				StsUpdateAgent.EventLogger.LogEvent(AgentsEventLogConstants.Tuple_UpdateAgentFileNotLoaded, null, null);
				return;
			}
			finally
			{
				if (binaryReader != null)
				{
					binaryReader.Close();
				}
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
			VerifyContent verifyContent = new VerifyContent();
			if (verifyContent.Parse(data))
			{
				if (StsUpdateAgent.shuttingDown)
				{
					return;
				}
				ProtocolAnalysisSrlSettings protocolAnalysisSrlSettings = new ProtocolAnalysisSrlSettings();
				FileStream fileStream2 = null;
				try
				{
					if (!File.Exists(StsUpdateAgent.updateAgentSparamFullFileName))
					{
						ExTraceGlobals.AgentTracer.TraceError((long)this.GetHashCode(), "ProcessNewData: SPARAM file does not exist.");
						return;
					}
					fileStream2 = new FileStream(StsUpdateAgent.updateAgentSparamFullFileName, FileMode.Open, FileAccess.Read, FileShare.Read | FileShare.Delete);
					protocolAnalysisSrlSettings.StoreReputationServiceParams(fileStream2, ExTraceGlobals.OnDownloadTracer);
					ConfigurationAccess.NotifySrlConfigChanged(protocolAnalysisSrlSettings.Fields);
					StsUpdateAgent.PerformanceCounters.TotalSrlUpdate();
				}
				catch (IOException ex3)
				{
					ExTraceGlobals.AgentTracer.TraceError<string>((long)this.GetHashCode(), "ProcessNewData: failed to read the SPARAM file. {0}", ex3.Message);
					return;
				}
				catch (UnauthorizedAccessException ex4)
				{
					ExTraceGlobals.AgentTracer.TraceError<string>((long)this.GetHashCode(), "ProcessNewData: failed to read the SPARAM file. {0}", ex4.Message);
					return;
				}
				finally
				{
					if (fileStream2 != null)
					{
						fileStream2.Dispose();
					}
				}
				return;
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00012768 File Offset: 0x00010968
		private void OnDataFileChanged(object source, FileSystemEventArgs ev)
		{
			lock (this.syncObject)
			{
				if (!StsUpdateAgent.shuttingDown)
				{
					this.ProcessNewData();
				}
			}
		}

		// Token: 0x0400023B RID: 571
		private const string UpdateFileName = "Microsoft.SmartScreen.DomainRep.dat";

		// Token: 0x0400023C RID: 572
		private const string UpdateSparamFileName = "Microsoft.SmartScreen.Sparam.dat";

		// Token: 0x0400023D RID: 573
		internal static ExEventLog EventLogger = new ExEventLog(ExTraceGlobals.FactoryTracer.Category, "MSExchange Antispam");

		// Token: 0x0400023E RID: 574
		private static string updateAgentPath;

		// Token: 0x0400023F RID: 575
		private static string updateAgentDatFullFileName;

		// Token: 0x04000240 RID: 576
		private static string updateAgentSparamFullFileName;

		// Token: 0x04000241 RID: 577
		private static bool shuttingDown;

		// Token: 0x04000242 RID: 578
		private static string version;

		// Token: 0x04000243 RID: 579
		private object syncObject = new object();

		// Token: 0x04000244 RID: 580
		private FileSystemWatcher dataFileWatcher;

		// Token: 0x02000064 RID: 100
		internal sealed class PerformanceCounters
		{
			// Token: 0x060002C1 RID: 705 RVA: 0x000127CF File Offset: 0x000109CF
			public static void TotalUpdate()
			{
				StsUpdatePerfCounters.TotalUpdate.Increment();
			}

			// Token: 0x060002C2 RID: 706 RVA: 0x000127DC File Offset: 0x000109DC
			public static void TotalSrlUpdate()
			{
				StsUpdatePerfCounters.TotalSrlUpdate.Increment();
			}

			// Token: 0x060002C3 RID: 707 RVA: 0x000127E9 File Offset: 0x000109E9
			public static void RemoveCounters()
			{
				StsUpdatePerfCounters.TotalSrlUpdate.RawValue = 0L;
				StsUpdatePerfCounters.TotalUpdate.RawValue = 0L;
			}
		}

		// Token: 0x02000065 RID: 101
		internal enum VersionComparison
		{
			// Token: 0x04000246 RID: 582
			Eq,
			// Token: 0x04000247 RID: 583
			NotEq
		}
	}
}
