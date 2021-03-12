using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000326 RID: 806
	public abstract class ComInteropTlbTaskBase : RunProcessBase
	{
		// Token: 0x06001B81 RID: 7041 RVA: 0x0007A6B2 File Offset: 0x000788B2
		public ComInteropTlbTaskBase(bool register)
		{
			this.register = register;
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x0007A6C4 File Offset: 0x000788C4
		protected override void InternalProcessRecord()
		{
			base.ExeName = Path.Combine(RuntimeEnvironment.GetRuntimeDirectory(), "regasm.exe");
			base.Args = string.Concat(new string[]
			{
				this.register ? string.Empty : "/unregister ",
				"/tlb:\"",
				Path.Combine(ConfigurationContext.Setup.BinPath, "ComInterop.tlb"),
				"\" \"",
				Path.Combine(ConfigurationContext.Setup.BinPath, "Microsoft.Exchange.Transport.Agent.ContentFilter.ComInterop.dll"),
				"\""
			});
			base.Timeout = 60000;
			base.InternalProcessRecord();
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06001B83 RID: 7043 RVA: 0x0007A75D File Offset: 0x0007895D
		[Precondition(ExpectedResult = true, FailureDescriptionId = Strings.IDs.ComInteropDllNotFound)]
		internal FileExistsCondition ComInteropDllExistCheck
		{
			get
			{
				return new FileExistsCondition(Path.Combine(ConfigurationContext.Setup.BinPath, "Microsoft.Exchange.Transport.Agent.ContentFilter.ComInterop.dll"));
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06001B84 RID: 7044 RVA: 0x0007A773 File Offset: 0x00078973
		[Precondition(ExpectedResult = true, FailureDescriptionId = Strings.IDs.RegasmNotFound)]
		internal FileExistsCondition RegAsmExistCheck
		{
			get
			{
				return new FileExistsCondition(Path.Combine(RuntimeEnvironment.GetRuntimeDirectory(), "regasm.exe"));
			}
		}

		// Token: 0x04000BD7 RID: 3031
		protected const string ComInteropDllName = "Microsoft.Exchange.Transport.Agent.ContentFilter.ComInterop.dll";

		// Token: 0x04000BD8 RID: 3032
		protected const string ComInteropTlbName = "ComInterop.tlb";

		// Token: 0x04000BD9 RID: 3033
		protected const string RegAsmFilename = "regasm.exe";

		// Token: 0x04000BDA RID: 3034
		private readonly bool register;
	}
}
