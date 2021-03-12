using System;
using System.Management.Automation;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200007D RID: 125
	public class CompileMofFileCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x06001868 RID: 6248 RVA: 0x00037540 File Offset: 0x00035740
		private CompileMofFileCommand() : base("Compile-MofFile")
		{
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x0003754D File Offset: 0x0003574D
		public CompileMofFileCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x0003755C File Offset: 0x0003575C
		public virtual CompileMofFileCommand SetParameters(CompileMofFileCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200007E RID: 126
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000327 RID: 807
			// (set) Token: 0x0600186B RID: 6251 RVA: 0x00037566 File Offset: 0x00035766
			public virtual string MofFilePath
			{
				set
				{
					base.PowerSharpParameters["MofFilePath"] = value;
				}
			}

			// Token: 0x17000328 RID: 808
			// (set) Token: 0x0600186C RID: 6252 RVA: 0x00037579 File Offset: 0x00035779
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000329 RID: 809
			// (set) Token: 0x0600186D RID: 6253 RVA: 0x00037591 File Offset: 0x00035791
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700032A RID: 810
			// (set) Token: 0x0600186E RID: 6254 RVA: 0x000375A9 File Offset: 0x000357A9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700032B RID: 811
			// (set) Token: 0x0600186F RID: 6255 RVA: 0x000375C1 File Offset: 0x000357C1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
