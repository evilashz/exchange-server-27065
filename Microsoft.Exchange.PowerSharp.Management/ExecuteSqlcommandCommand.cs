using System;
using System.Collections;
using System.Management.Automation;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000077 RID: 119
	public class ExecuteSqlcommandCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x0600183C RID: 6204 RVA: 0x000371F2 File Offset: 0x000353F2
		private ExecuteSqlcommandCommand() : base("Execute-Sqlcommand")
		{
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x000371FF File Offset: 0x000353FF
		public ExecuteSqlcommandCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x0003720E File Offset: 0x0003540E
		public virtual ExecuteSqlcommandCommand SetParameters(ExecuteSqlcommandCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000078 RID: 120
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000307 RID: 775
			// (set) Token: 0x0600183F RID: 6207 RVA: 0x00037218 File Offset: 0x00035418
			public virtual string Command
			{
				set
				{
					base.PowerSharpParameters["Command"] = value;
				}
			}

			// Token: 0x17000308 RID: 776
			// (set) Token: 0x06001840 RID: 6208 RVA: 0x0003722B File Offset: 0x0003542B
			public virtual SwitchParameter ExecuteScalar
			{
				set
				{
					base.PowerSharpParameters["ExecuteScalar"] = value;
				}
			}

			// Token: 0x17000309 RID: 777
			// (set) Token: 0x06001841 RID: 6209 RVA: 0x00037243 File Offset: 0x00035443
			public virtual SwitchParameter ExecuteScript
			{
				set
				{
					base.PowerSharpParameters["ExecuteScript"] = value;
				}
			}

			// Token: 0x1700030A RID: 778
			// (set) Token: 0x06001842 RID: 6210 RVA: 0x0003725B File Offset: 0x0003545B
			public virtual Hashtable Arguments
			{
				set
				{
					base.PowerSharpParameters["Arguments"] = value;
				}
			}

			// Token: 0x1700030B RID: 779
			// (set) Token: 0x06001843 RID: 6211 RVA: 0x0003726E File Offset: 0x0003546E
			public virtual int Timeout
			{
				set
				{
					base.PowerSharpParameters["Timeout"] = value;
				}
			}

			// Token: 0x1700030C RID: 780
			// (set) Token: 0x06001844 RID: 6212 RVA: 0x00037286 File Offset: 0x00035486
			public virtual string DatabaseName
			{
				set
				{
					base.PowerSharpParameters["DatabaseName"] = value;
				}
			}

			// Token: 0x1700030D RID: 781
			// (set) Token: 0x06001845 RID: 6213 RVA: 0x00037299 File Offset: 0x00035499
			public virtual string ServerName
			{
				set
				{
					base.PowerSharpParameters["ServerName"] = value;
				}
			}

			// Token: 0x1700030E RID: 782
			// (set) Token: 0x06001846 RID: 6214 RVA: 0x000372AC File Offset: 0x000354AC
			public virtual string MirrorServerName
			{
				set
				{
					base.PowerSharpParameters["MirrorServerName"] = value;
				}
			}

			// Token: 0x1700030F RID: 783
			// (set) Token: 0x06001847 RID: 6215 RVA: 0x000372BF File Offset: 0x000354BF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000310 RID: 784
			// (set) Token: 0x06001848 RID: 6216 RVA: 0x000372D7 File Offset: 0x000354D7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000311 RID: 785
			// (set) Token: 0x06001849 RID: 6217 RVA: 0x000372EF File Offset: 0x000354EF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000312 RID: 786
			// (set) Token: 0x0600184A RID: 6218 RVA: 0x00037307 File Offset: 0x00035507
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
