using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Management.Automation;
using System.Threading;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200021A RID: 538
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("precompile", "ManagedBinary")]
	public sealed class PrecompileManagedBinary : RunProcessBase
	{
		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x00050FA6 File Offset: 0x0004F1A6
		// (set) Token: 0x0600126A RID: 4714 RVA: 0x00050FBD File Offset: 0x0004F1BD
		[Parameter(Mandatory = false)]
		public string BinaryName
		{
			get
			{
				return (string)base.Fields["BinaryName"];
			}
			set
			{
				base.Fields["BinaryName"] = value;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x00050FD0 File Offset: 0x0004F1D0
		// (set) Token: 0x0600126C RID: 4716 RVA: 0x00050FE7 File Offset: 0x0004F1E7
		[Parameter(Mandatory = false)]
		public string AppBase
		{
			get
			{
				return (string)base.Fields["AppBase"];
			}
			set
			{
				base.Fields["AppBase"] = value;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x00050FFA File Offset: 0x0004F1FA
		// (set) Token: 0x0600126E RID: 4718 RVA: 0x00051011 File Offset: 0x0004F211
		[Parameter(Mandatory = false)]
		public string Action
		{
			get
			{
				return (string)base.Fields["Action"];
			}
			set
			{
				base.Fields["Action"] = value;
			}
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00051024 File Offset: 0x0004F224
		private string GetDotNetInstallPath()
		{
			string result = string.Empty;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(Path.Combine(PrecompileManagedBinary.dotNetSetupRegistry, PrecompileManagedBinary.dotNetSetupVersion)))
			{
				if (registryKey != null)
				{
					result = (string)registryKey.GetValue(PrecompileManagedBinary.installPathRegistryKey, null);
				}
			}
			return result;
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x00051084 File Offset: 0x0004F284
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			string dotNetInstallPath = this.GetDotNetInstallPath();
			if (string.IsNullOrEmpty(dotNetInstallPath))
			{
				base.WriteError(new ArgumentException(Strings.ErrorDotNetInstallPathNotFound, "DotNetInstallPath"), ErrorCategory.InvalidArgument, "DotNetInstallPath");
			}
			this.fullExePath = Path.Combine(dotNetInstallPath, PrecompileManagedBinary.ngenExeName);
			string str = PrecompileManagedBinary.SupportedActions.install.ToString();
			int timeout = -1;
			if (!string.IsNullOrEmpty(this.Action))
			{
				PrecompileManagedBinary.SupportedActions supportedActions;
				if (Enum.TryParse<PrecompileManagedBinary.SupportedActions>(this.Action, true, out supportedActions))
				{
					if (supportedActions == PrecompileManagedBinary.SupportedActions.queue)
					{
						str += " /queue";
					}
					else
					{
						str = supportedActions.ToString();
					}
				}
				else
				{
					base.WriteError(new ArgumentException(string.Format("Ngen Action '{0}' isn't supported.", this.Action)), ErrorCategory.InvalidArgument, "Ngen Action");
				}
				if (supportedActions == PrecompileManagedBinary.SupportedActions.executeQueuedItems)
				{
					timeout = 0;
				}
			}
			this.commandLineArguments = str;
			if (!base.Fields.IsModified("AppBase"))
			{
				this.AppBase = string.Empty;
			}
			if (!string.IsNullOrEmpty(this.BinaryName))
			{
				this.commandLineArguments = this.commandLineArguments + " \"" + this.BinaryName + "\" /nologo /verbose";
			}
			if (!string.IsNullOrEmpty(this.AppBase))
			{
				this.commandLineArguments = this.commandLineArguments + " /AppBase:\"" + this.AppBase + "\"";
			}
			base.Args = this.commandLineArguments;
			base.Timeout = timeout;
			base.ExeName = this.fullExePath;
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x000511F4 File Offset: 0x0004F3F4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (string.IsNullOrEmpty(base.ExeName))
			{
				base.WriteError(new ArgumentException(Strings.ErrorFileNameCannotBeEmptyOrNull, "ExeName"), ErrorCategory.InvalidArgument, "ExeName");
			}
			base.WriteVerbose(Strings.ProcessStart(base.ExeName, base.Args));
			int num = 6;
			int num2 = 0;
			Exception ex;
			for (;;)
			{
				num2++;
				ex = null;
				try
				{
					string outputString;
					string errorString;
					int num3;
					if (num2 == 1)
					{
						num3 = ProcessRunner.Run(base.ExeName, base.Args, new Dictionary<string, string>
						{
							{
								"COMPLUS_NGENWORKERCOUNT",
								"3"
							}
						}, base.Timeout, null, out outputString, out errorString);
					}
					else
					{
						base.WriteVerbose(new LocalizedString(string.Format("Attempt {0} to execute '{1}'", num2, base.ExeName)));
						num3 = ProcessRunner.Run(base.ExeName, base.Args, new Dictionary<string, string>
						{
							{
								"COMPLUS_EnableMultiproc",
								"0"
							}
						}, base.Timeout, null, out outputString, out errorString);
						base.WriteVerbose(new LocalizedString(string.Format("'{0}' returned exitCode {1}", base.ExeName, num3)));
					}
					this.HandleProcessOutput(outputString, errorString);
					if (num3 != 0)
					{
						ex = new TaskException(Strings.ExceptionRunProcessFailed(num3));
					}
				}
				catch (Win32Exception ex2)
				{
					ex = ex2;
				}
				catch (TimeoutException ex3)
				{
					ex = ex3;
				}
				catch (Exception ex4)
				{
					base.WriteVerbose(Strings.ProcessStandardError(string.Format("Attempt {0} failed and will not be retried. Exception: {1}", num2, ex4.ToString())));
					throw;
				}
				if (ex == null)
				{
					goto IL_1C1;
				}
				if (num2 > num)
				{
					break;
				}
				base.WriteVerbose(Strings.ProcessStandardError(string.Format("Retry({0}/{1}): exception: {2}", num2, num, ex.ToString())));
				Thread.Sleep(num2 * 1000);
			}
			base.WriteVerbose(Strings.ProcessStandardError(string.Format("Reached maximum times of retries({0}): exception: {1}. Ignore and continue.", num, ex.ToString())));
			IL_1C1:
			TaskLogger.LogExit();
		}

		// Token: 0x040007DB RID: 2011
		private static string ngenExeName = "ngen.exe";

		// Token: 0x040007DC RID: 2012
		private static string dotNetSetupVersion = "v4\\Client";

		// Token: 0x040007DD RID: 2013
		private static string dotNetSetupRegistry = "SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\";

		// Token: 0x040007DE RID: 2014
		private static string installPathRegistryKey = "InstallPath";

		// Token: 0x040007DF RID: 2015
		private string fullExePath;

		// Token: 0x040007E0 RID: 2016
		private string commandLineArguments;

		// Token: 0x0200021B RID: 539
		private enum SupportedActions
		{
			// Token: 0x040007E2 RID: 2018
			install,
			// Token: 0x040007E3 RID: 2019
			uninstall,
			// Token: 0x040007E4 RID: 2020
			queue,
			// Token: 0x040007E5 RID: 2021
			executeQueuedItems
		}
	}
}
