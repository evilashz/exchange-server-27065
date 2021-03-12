using System;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000D2 RID: 210
	internal class HaTaskStringBuilderOutputHelper : ITaskOutputHelper, ILogTraceHelper, IClusterSetupProgress
	{
		// Token: 0x0600086C RID: 2156 RVA: 0x00028860 File Offset: 0x00026A60
		internal HaTaskStringBuilderOutputHelper(string taskName)
		{
			this.m_taskName = taskName;
			this.m_writer = new StringBuilder(2048);
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x0002888A File Offset: 0x00026A8A
		// (set) Token: 0x0600086E RID: 2158 RVA: 0x00028892 File Offset: 0x00026A92
		public Exception LastException
		{
			get
			{
				return this.m_lastException;
			}
			set
			{
				this.m_lastException = value;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x0002889B File Offset: 0x00026A9B
		// (set) Token: 0x06000870 RID: 2160 RVA: 0x000288A3 File Offset: 0x00026AA3
		public int MaxPercentageDuringCallback
		{
			get
			{
				return this.m_maxPercentageDuringCallback;
			}
			set
			{
				this.m_maxPercentageDuringCallback = value;
			}
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x000288AC File Offset: 0x00026AAC
		public void AppendLogMessage(LocalizedString locMessage)
		{
			string arg = DateTime.UtcNow.ToString("s");
			if (this.m_writer != null)
			{
				this.m_writer.AppendFormat("[{0}] {1}", arg, locMessage.ToString());
				this.m_writer.AppendLine();
			}
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00028900 File Offset: 0x00026B00
		public void AppendLogMessage(string englishMessage, params object[] args)
		{
			string arg = DateTime.UtcNow.ToString("s");
			if (this.m_writer != null)
			{
				this.m_writer.AppendFormat("[{0}] ", arg);
				this.m_writer.AppendFormat(englishMessage, args);
				this.m_writer.AppendLine();
			}
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00028954 File Offset: 0x00026B54
		public void WriteErrorSimple(Exception error)
		{
			this.WriteErrorEx(error, ErrorCategory.InvalidArgument, null);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0002895F File Offset: 0x00026B5F
		public void WriteVerbose(LocalizedString locString)
		{
			this.WriteVerboseEx(locString);
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00028968 File Offset: 0x00026B68
		public void WriteWarning(LocalizedString locString)
		{
			this.WriteWarningEx(locString);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00028971 File Offset: 0x00026B71
		public void WriteProgressSimple(LocalizedString locString)
		{
			this.WriteProgressIncrementalSimple(locString, 2);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0002897C File Offset: 0x00026B7C
		public int ClusterSetupProgressCallback(IntPtr pvCallbackArg, ClusapiMethods.CLUSTER_SETUP_PHASE eSetupPhase, ClusapiMethods.CLUSTER_SETUP_PHASE_TYPE ePhaseType, ClusapiMethods.CLUSTER_SETUP_PHASE_SEVERITY ePhaseSeverity, uint dwPercentComplete, string lpszObjectName, uint dwStatus)
		{
			this.AppendLogMessage("ClusterSetupProgressCallback( eSetupPhase = {0}, ePhaseType = {1}, ePhaseSeverity = {2}, dwPercentComplete = {3}, szObjectName = {4}, dwStatus = 0x{5:x} )", new object[]
			{
				eSetupPhase,
				ePhaseType,
				ePhaseSeverity,
				dwPercentComplete,
				lpszObjectName,
				dwStatus
			});
			this.m_maxPercentageDuringCallback = Math.Max(this.m_maxPercentageDuringCallback, (int)dwPercentComplete);
			Exception ex = HaTaskCallbackHelper.LookUpStatus(eSetupPhase, ePhaseType, ePhaseSeverity, dwPercentComplete, lpszObjectName, dwStatus);
			if (ex != null)
			{
				this.m_lastException = ex;
				this.AppendLogMessage("Found a matching exception: {0}", new object[]
				{
					ex
				});
			}
			return 1;
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00028A16 File Offset: 0x00026C16
		public override string ToString()
		{
			if (this.m_writer != null)
			{
				this.m_logContents = this.m_writer.ToString();
			}
			return this.m_logContents;
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00028A37 File Offset: 0x00026C37
		internal void CreateTempLogFile()
		{
			this.m_writer.AppendFormat("{0} started on machine {1}.", this.m_taskName, Environment.MachineName);
			this.m_writer.AppendLine();
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00028A64 File Offset: 0x00026C64
		internal void CloseTempLogFile()
		{
			if (this.m_writer != null)
			{
				this.m_writer.AppendFormat("{0} explicitly called CloseTempLogFile().", this.m_taskName);
				this.m_writer.AppendLine();
				this.m_logContents = this.m_writer.ToString();
				this.m_writer = null;
			}
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00028AB4 File Offset: 0x00026CB4
		internal void WriteError(Exception error, ErrorCategory errorCategory, object target)
		{
			this.WriteErrorEx(error, errorCategory, target);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00028AC0 File Offset: 0x00026CC0
		internal void WriteErrorEx(Exception error, ErrorCategory errorCategory, object target)
		{
			this.AppendLogMessage("WriteError! Exception = {0}", new object[]
			{
				error.ToString()
			});
			throw error;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00028AEA File Offset: 0x00026CEA
		internal void WriteVerboseEx(LocalizedString locString)
		{
			this.AppendLogMessage(locString.ToString(), new object[0]);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00028B08 File Offset: 0x00026D08
		internal void WriteWarningEx(LocalizedString locString)
		{
			this.AppendLogMessage("Warning: {0}", new object[]
			{
				locString.ToString()
			});
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00028B38 File Offset: 0x00026D38
		internal void WriteProgressIncrementalSimple(LocalizedString locString, int incrementalPercent)
		{
			this.m_percentComplete = Math.Min(this.m_percentComplete + incrementalPercent, 100);
			this.WriteProgressEx(ReplayStrings.ProgressStatusInProgress, locString, this.m_percentComplete);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00028B61 File Offset: 0x00026D61
		internal void WriteProgress(LocalizedString activity, LocalizedString statusDescription, int percentComplete)
		{
			this.WriteProgressEx(activity, statusDescription, percentComplete);
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00028B6C File Offset: 0x00026D6C
		internal void WriteProgressEx(LocalizedString activity, LocalizedString statusDescription, int percentComplete)
		{
			this.AppendLogMessage("Updated Progress '{0}' {1}%.", new object[]
			{
				statusDescription.ToString(),
				percentComplete.ToString()
			});
			this.AppendLogMessage(activity);
			this.m_percentComplete = percentComplete;
		}

		// Token: 0x040003A3 RID: 931
		private int m_percentComplete;

		// Token: 0x040003A4 RID: 932
		private string m_taskName;

		// Token: 0x040003A5 RID: 933
		private StringBuilder m_writer;

		// Token: 0x040003A6 RID: 934
		private string m_logContents = string.Empty;

		// Token: 0x040003A7 RID: 935
		private Exception m_lastException;

		// Token: 0x040003A8 RID: 936
		private int m_maxPercentageDuringCallback;
	}
}
