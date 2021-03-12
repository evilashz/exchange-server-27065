using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000F7 RID: 247
	[Cmdlet("Compile", "MofFile")]
	public class CompileMofFile : Task
	{
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x0001F670 File Offset: 0x0001D870
		// (set) Token: 0x06000751 RID: 1873 RVA: 0x0001F65D File Offset: 0x0001D85D
		[Parameter(Mandatory = true)]
		public string MofFilePath
		{
			get
			{
				return (string)base.Fields["MofFilePath"];
			}
			set
			{
				base.Fields["MofFilePath"] = value;
			}
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0001F688 File Offset: 0x0001D888
		protected override void InternalProcessRecord()
		{
			MofCompiler mofCompiler = new MofCompiler();
			WbemCompileStatusInfo wbemCompileStatusInfo = default(WbemCompileStatusInfo);
			wbemCompileStatusInfo.InitializeStatusInfo();
			int num = mofCompiler.CompileFile(this.MofFilePath, null, null, null, null, 0, 0, 0, ref wbemCompileStatusInfo);
			if (num == 0)
			{
				TaskLogger.Log(Strings.ExchangeTracingProviderInstalledSuccess);
				return;
			}
			if (num == 262145)
			{
				TaskLogger.Log(Strings.ExchangeTracingProviderAlreadyExists);
				return;
			}
			base.WriteError(new ExchangeTracingProviderInstallException(wbemCompileStatusInfo.HResult), ErrorCategory.NotSpecified, wbemCompileStatusInfo);
		}
	}
}
