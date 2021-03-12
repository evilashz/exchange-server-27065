using System;
using System.IO;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Isam.Esent.Interop;
using Microsoft.Isam.Esent.Interop.Unpublished;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002DA RID: 730
	internal class LogContinuityChecker
	{
		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06001D02 RID: 7426 RVA: 0x0008279F File Offset: 0x0008099F
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.LogInspectorTracer;
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06001D03 RID: 7427 RVA: 0x000827A6 File Offset: 0x000809A6
		// (set) Token: 0x06001D04 RID: 7428 RVA: 0x000827AE File Offset: 0x000809AE
		private bool Initialized { get; set; }

		// Token: 0x06001D05 RID: 7429 RVA: 0x000827B8 File Offset: 0x000809B8
		public bool Examine(JET_LOGINFOMISC logInfo, string logFileName, out LocalizedString error)
		{
			error = LocalizedString.Empty;
			if (this.Initialized)
			{
				if (logInfo.ulGeneration != this.m_lastCheckedInfo.ulGeneration + 1)
				{
					error = ReplayStrings.FileCheckLogfileGeneration(logFileName, (long)logInfo.ulGeneration, (long)(this.m_lastCheckedInfo.ulGeneration + 1));
					return false;
				}
				if (logInfo.logtimePreviousGeneration != this.m_lastCheckedInfo.logtimeCreate)
				{
					error = ReplayStrings.FileCheckLogfileCreationTime(logFileName, logInfo.logtimePreviousGeneration.ToDateTime() ?? DateTime.MinValue, this.m_lastCheckedInfo.logtimeCreate.ToDateTime() ?? DateTime.MinValue);
					return false;
				}
			}
			else
			{
				this.Initialized = true;
			}
			this.m_lastCheckedInfo = logInfo;
			return true;
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x0008289C File Offset: 0x00080A9C
		public void Initialize(long logGeneration, string logPath, string prefix, string suffix)
		{
			string text = Path.Combine(logPath, EseHelper.MakeLogfileName(prefix, suffix, logGeneration));
			this.Initialized = false;
			try
			{
				JET_LOGINFOMISC logInfo;
				UnpublishedApi.JetGetLogFileInfo(text, out logInfo, JET_LogInfo.Misc2);
				LocalizedString localizedString;
				this.Examine(logInfo, text, out localizedString);
			}
			catch (EsentErrorException ex)
			{
				LogContinuityChecker.Tracer.TraceError<string, EsentErrorException>((long)this.GetHashCode(), "LogContinuityChecker failed to init with '{0}': {1}", text, ex);
				throw new LogInspectorFailedGeneralException(text, ex.Message, ex);
			}
		}

		// Token: 0x04000C12 RID: 3090
		private JET_LOGINFOMISC m_lastCheckedInfo;
	}
}
