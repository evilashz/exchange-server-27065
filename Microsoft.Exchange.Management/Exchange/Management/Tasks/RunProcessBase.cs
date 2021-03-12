using System;
using System.ComponentModel;
using System.Management.Automation;
using System.Threading;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000ED RID: 237
	public abstract class RunProcessBase : Task
	{
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x0001CE64 File Offset: 0x0001B064
		// (set) Token: 0x060006FE RID: 1790 RVA: 0x0001CE7B File Offset: 0x0001B07B
		[Parameter(Mandatory = false)]
		public string Args
		{
			get
			{
				return (string)base.Fields["Args"];
			}
			set
			{
				base.Fields["Args"] = value;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x0001CE8E File Offset: 0x0001B08E
		// (set) Token: 0x06000700 RID: 1792 RVA: 0x0001CEB9 File Offset: 0x0001B0B9
		[Parameter(Mandatory = false)]
		public int Timeout
		{
			get
			{
				if (base.Fields["Timeout"] != null)
				{
					return (int)base.Fields["Timeout"];
				}
				return -1;
			}
			set
			{
				base.Fields["Timeout"] = value;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x0001CED1 File Offset: 0x0001B0D1
		// (set) Token: 0x06000702 RID: 1794 RVA: 0x0001CF01 File Offset: 0x0001B101
		[Parameter(Mandatory = false)]
		public int[] IgnoreExitCode
		{
			get
			{
				if (base.Fields["IgnoreExitCode"] != null)
				{
					return (int[])base.Fields["IgnoreExitCode"];
				}
				return new int[0];
			}
			set
			{
				base.Fields["IgnoreExitCode"] = value;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x0001CF14 File Offset: 0x0001B114
		// (set) Token: 0x06000704 RID: 1796 RVA: 0x0001CF3F File Offset: 0x0001B13F
		[Parameter(Mandatory = false)]
		public uint RetryCount
		{
			get
			{
				if (base.Fields["RetryCount"] != null)
				{
					return (uint)base.Fields["RetryCount"];
				}
				return 0U;
			}
			set
			{
				base.Fields["RetryCount"] = value;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x0001CF57 File Offset: 0x0001B157
		// (set) Token: 0x06000706 RID: 1798 RVA: 0x0001CF86 File Offset: 0x0001B186
		[Parameter(Mandatory = false)]
		public uint RetryDelay
		{
			get
			{
				if (base.Fields["RetryDelay"] != null)
				{
					return (uint)base.Fields["RetryDelay"];
				}
				return 200U;
			}
			set
			{
				base.Fields["RetryDelay"] = value;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x0001CF9E File Offset: 0x0001B19E
		// (set) Token: 0x06000708 RID: 1800 RVA: 0x0001CFA6 File Offset: 0x0001B1A6
		protected string ExeName
		{
			get
			{
				return this.exeName;
			}
			set
			{
				this.exeName = value;
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001CFB0 File Offset: 0x0001B1B0
		protected bool IsExitCodeIgnorable(int ExitCode)
		{
			foreach (int num in this.IgnoreExitCode)
			{
				if (ExitCode == num)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001CFE1 File Offset: 0x0001B1E1
		protected virtual void HandleProcessOutput(string outputString, string errorString)
		{
			base.WriteVerbose(Strings.ProcessStandardOutput(outputString));
			base.WriteVerbose(Strings.ProcessStandardError(errorString));
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001CFFC File Offset: 0x0001B1FC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (string.IsNullOrEmpty(this.ExeName))
			{
				base.WriteError(new ArgumentException(Strings.ErrorFileNameCannotBeEmptyOrNull, "ExeName"), ErrorCategory.InvalidArgument, "ExeName");
			}
			base.WriteVerbose(Strings.ProcessStart(this.ExeName, this.Args));
			int num = 0;
			uint num2 = 1U + this.RetryCount;
			while (num2-- > 0U)
			{
				string outputString;
				string errorString;
				try
				{
					num = ProcessRunner.Run(this.ExeName, this.Args, this.Timeout, null, out outputString, out errorString);
				}
				catch (Win32Exception exception)
				{
					base.WriteError(exception, ErrorCategory.InvalidOperation, null);
					TaskLogger.LogExit();
					return;
				}
				catch (TimeoutException exception2)
				{
					base.WriteError(exception2, ErrorCategory.OperationTimeout, null);
					TaskLogger.LogExit();
					return;
				}
				this.HandleProcessOutput(outputString, errorString);
				if (num == 0)
				{
					continue;
				}
				if (this.IsExitCodeIgnorable(num))
				{
					base.WriteVerbose(Strings.ExceptionRunProcessExitIgnored(num));
					break;
				}
				if (num2 > 0U)
				{
					base.WriteVerbose(Strings.ExceptionRunProcessFailedRetry(num, num2));
					Thread.Sleep((int)this.RetryDelay);
					continue;
				}
				base.WriteError(new TaskException(Strings.ExceptionRunProcessFailed(num)), ErrorCategory.InvalidOperation, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0400034F RID: 847
		private const string ArgsParameter = "Args";

		// Token: 0x04000350 RID: 848
		private const string TimeoutParameter = "Timeout";

		// Token: 0x04000351 RID: 849
		private const string IgnoreExitCodeParameter = "IgnoreExitCode";

		// Token: 0x04000352 RID: 850
		private const string RetryCountParameter = "RetryCount";

		// Token: 0x04000353 RID: 851
		private const string RetryDelayParameter = "RetryDelay";

		// Token: 0x04000354 RID: 852
		private const int DefaultRetryDelayMs = 200;

		// Token: 0x04000355 RID: 853
		private string exeName;
	}
}
