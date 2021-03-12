using System;
using System.IO;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008AA RID: 2218
	internal class HaTaskOutputHelper : ITaskOutputHelper, ILogTraceHelper
	{
		// Token: 0x1700176C RID: 5996
		// (get) Token: 0x06004E40 RID: 20032 RVA: 0x001447C1 File Offset: 0x001429C1
		// (set) Token: 0x06004E41 RID: 20033 RVA: 0x001447C9 File Offset: 0x001429C9
		internal Exception LastException
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

		// Token: 0x06004E42 RID: 20034 RVA: 0x001447D2 File Offset: 0x001429D2
		internal HaTaskOutputHelper(string taskName, Task.TaskErrorLoggingDelegate writeError, Task.TaskWarningLoggingDelegate writeWarning, Task.TaskVerboseLoggingDelegate writeVerbose, Task.TaskProgressLoggingDelegate writeProgress, int hashCode)
		{
			this.m_taskName = taskName;
			this.m_writeError = writeError;
			this.m_writeWarning = writeWarning;
			this.m_writeVerbose = writeVerbose;
			this.m_writeProgress = writeProgress;
			this.m_hashCode = hashCode;
		}

		// Token: 0x06004E43 RID: 20035 RVA: 0x00144808 File Offset: 0x00142A08
		internal void CreateTempLogFile()
		{
			string path = string.Format("dagtask_{0}_{1}.log", DateTime.UtcNow.ToString("yyyy-MM-dd_HH-mm-ss.fff"), this.m_taskName);
			string text = Path.Combine(ConfigurationContext.Setup.SetupLoggingPath, "DagTasks");
			bool flag = true;
			try
			{
				this.m_logFileName = Path.Combine(text, path);
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				ExTraceGlobals.ClusterTracer.TraceDebug<string>((long)this.m_hashCode, "Opening the log file {0}.", this.m_logFileName);
				this.m_writer = new StreamWriter(new FileStream(this.m_logFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.Read));
			}
			catch (UnauthorizedAccessException arg)
			{
				ExTraceGlobals.ClusterTracer.TraceDebug<string, string, UnauthorizedAccessException>((long)this.m_hashCode, "Could not create the directory {0} OR open the log file {1}, error {2}. Falling back to %temp%", text, this.m_logFileName, arg);
				flag = false;
			}
			catch (SecurityException arg2)
			{
				ExTraceGlobals.ClusterTracer.TraceDebug<string, string, SecurityException>((long)this.m_hashCode, "Could not create the directory {0} OR open the log file {1}, error {2}. Falling back to %temp%", text, this.m_logFileName, arg2);
				flag = false;
			}
			catch (IOException arg3)
			{
				ExTraceGlobals.ClusterTracer.TraceDebug<string, string, IOException>((long)this.m_hashCode, "Could not create the directory {0} OR open the log file {1}, error {2}. Falling back to %temp%", text, this.m_logFileName, arg3);
				flag = false;
			}
			if (!flag)
			{
				this.m_logFileName = Path.Combine(Path.GetTempPath(), path);
				ExTraceGlobals.ClusterTracer.TraceDebug<string>((long)this.m_hashCode, "Opening the log file {0}.", this.m_logFileName);
				this.m_writer = new StreamWriter(this.m_logFileName);
			}
			this.m_writer.AutoFlush = true;
			this.m_writer.WriteLine("{0} started on machine {1}.", this.m_taskName, Environment.MachineName);
		}

		// Token: 0x06004E44 RID: 20036 RVA: 0x0014499C File Offset: 0x00142B9C
		internal void CloseTempLogFile()
		{
			if (this.m_writer != null)
			{
				this.m_writer.WriteLine("{0} explicitly called CloseTempLogFile().", this.m_taskName);
				this.m_writer.Close();
				this.m_writer = null;
			}
		}

		// Token: 0x06004E45 RID: 20037 RVA: 0x001449D0 File Offset: 0x00142BD0
		public void AppendLogMessage(LocalizedString locMessage)
		{
			if (this.m_writer != null)
			{
				string arg = DateTime.UtcNow.ToString("s");
				this.m_writer.WriteLine("[{0}] {1}", arg, locMessage.ToString());
			}
		}

		// Token: 0x06004E46 RID: 20038 RVA: 0x00144A18 File Offset: 0x00142C18
		public void AppendLogMessage(string englishMessage, params object[] args)
		{
			if (this.m_writer != null)
			{
				string arg = DateTime.UtcNow.ToString("s");
				this.m_writer.Write("[{0}] ", arg);
				this.m_writer.WriteLine(englishMessage, args);
			}
		}

		// Token: 0x06004E47 RID: 20039 RVA: 0x00144A5E File Offset: 0x00142C5E
		public void WriteErrorSimple(Exception error)
		{
			this.WriteErrorEx(error, ErrorCategory.InvalidArgument, null);
		}

		// Token: 0x06004E48 RID: 20040 RVA: 0x00144A69 File Offset: 0x00142C69
		internal void WriteError(Exception error, ErrorCategory errorCategory, object target)
		{
			this.WriteErrorEx(error, errorCategory, target);
		}

		// Token: 0x06004E49 RID: 20041 RVA: 0x00144A74 File Offset: 0x00142C74
		internal void WriteErrorEx(Exception error, ErrorCategory errorCategory, object target)
		{
			if (this.m_writer != null)
			{
				if (!string.IsNullOrEmpty(this.m_logFileName))
				{
					this.WriteWarning(Strings.DagTaskErrorEncounteredMoreDetailsInLog(this.m_logFileName, Environment.MachineName));
				}
				this.AppendLogMessage("WriteError! Exception = {0}", new object[]
				{
					error.ToString()
				});
			}
			this.m_writeError(error, errorCategory, target);
		}

		// Token: 0x06004E4A RID: 20042 RVA: 0x00144AD6 File Offset: 0x00142CD6
		public void WriteVerbose(LocalizedString locString)
		{
			this.WriteVerboseEx(locString);
		}

		// Token: 0x06004E4B RID: 20043 RVA: 0x00144ADF File Offset: 0x00142CDF
		internal void WriteVerboseEx(LocalizedString locString)
		{
			this.AppendLogMessage(locString.ToString(), new object[0]);
			this.m_writeVerbose(locString);
		}

		// Token: 0x06004E4C RID: 20044 RVA: 0x00144B06 File Offset: 0x00142D06
		public void WriteWarning(LocalizedString locString)
		{
			this.WriteWarningEx(locString);
		}

		// Token: 0x06004E4D RID: 20045 RVA: 0x00144B0F File Offset: 0x00142D0F
		internal void WriteWarningEx(LocalizedString locString)
		{
			this.AppendLogMessage(locString.ToString(), new object[0]);
			this.m_writeWarning(locString);
		}

		// Token: 0x06004E4E RID: 20046 RVA: 0x00144B36 File Offset: 0x00142D36
		public void WriteProgressSimple(LocalizedString locString)
		{
			this.WriteProgressIncrementalSimple(locString, 2);
		}

		// Token: 0x06004E4F RID: 20047 RVA: 0x00144B40 File Offset: 0x00142D40
		internal void WriteProgressIncrementalSimple(LocalizedString locString, int incrementalPercent)
		{
			this.m_percentComplete = Math.Min(this.m_percentComplete + incrementalPercent, 100);
			this.WriteProgressEx(Strings.ProgressStatusInProgress, locString, this.m_percentComplete);
		}

		// Token: 0x06004E50 RID: 20048 RVA: 0x00144B69 File Offset: 0x00142D69
		internal void WriteProgress(LocalizedString activity, LocalizedString statusDescription, int percentComplete)
		{
			this.WriteProgressEx(activity, statusDescription, percentComplete);
		}

		// Token: 0x06004E51 RID: 20049 RVA: 0x00144B74 File Offset: 0x00142D74
		internal void WriteProgressEx(LocalizedString activity, LocalizedString statusDescription, int percentComplete)
		{
			this.AppendLogMessage("Updated Progress '{0}' {1}%.", new object[]
			{
				statusDescription.ToString(),
				percentComplete.ToString()
			});
			this.AppendLogMessage(activity);
			this.m_percentComplete = percentComplete;
			this.m_writeProgress(activity, statusDescription, percentComplete);
		}

		// Token: 0x04002E91 RID: 11921
		private Task.TaskErrorLoggingDelegate m_writeError;

		// Token: 0x04002E92 RID: 11922
		private Task.TaskWarningLoggingDelegate m_writeWarning;

		// Token: 0x04002E93 RID: 11923
		private Task.TaskVerboseLoggingDelegate m_writeVerbose;

		// Token: 0x04002E94 RID: 11924
		private Task.TaskProgressLoggingDelegate m_writeProgress;

		// Token: 0x04002E95 RID: 11925
		private int m_percentComplete;

		// Token: 0x04002E96 RID: 11926
		private readonly string m_taskName;

		// Token: 0x04002E97 RID: 11927
		private readonly int m_hashCode;

		// Token: 0x04002E98 RID: 11928
		private string m_logFileName;

		// Token: 0x04002E99 RID: 11929
		private StreamWriter m_writer;

		// Token: 0x04002E9A RID: 11930
		private Exception m_lastException;
	}
}
