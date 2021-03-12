using System;
using System.Management.Automation;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020000B6 RID: 182
	public class PrecompileManagedBinaryCommand : SyntheticCommand<object>
	{
		// Token: 0x06001AA2 RID: 6818 RVA: 0x0003A23C File Offset: 0x0003843C
		private PrecompileManagedBinaryCommand() : base("Precompile-ManagedBinary")
		{
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x0003A249 File Offset: 0x00038449
		public PrecompileManagedBinaryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x0003A258 File Offset: 0x00038458
		public virtual PrecompileManagedBinaryCommand SetParameters(PrecompileManagedBinaryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000B7 RID: 183
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170004EF RID: 1263
			// (set) Token: 0x06001AA5 RID: 6821 RVA: 0x0003A262 File Offset: 0x00038462
			public virtual string BinaryName
			{
				set
				{
					base.PowerSharpParameters["BinaryName"] = value;
				}
			}

			// Token: 0x170004F0 RID: 1264
			// (set) Token: 0x06001AA6 RID: 6822 RVA: 0x0003A275 File Offset: 0x00038475
			public virtual string AppBase
			{
				set
				{
					base.PowerSharpParameters["AppBase"] = value;
				}
			}

			// Token: 0x170004F1 RID: 1265
			// (set) Token: 0x06001AA7 RID: 6823 RVA: 0x0003A288 File Offset: 0x00038488
			public virtual string Action
			{
				set
				{
					base.PowerSharpParameters["Action"] = value;
				}
			}

			// Token: 0x170004F2 RID: 1266
			// (set) Token: 0x06001AA8 RID: 6824 RVA: 0x0003A29B File Offset: 0x0003849B
			public virtual string Args
			{
				set
				{
					base.PowerSharpParameters["Args"] = value;
				}
			}

			// Token: 0x170004F3 RID: 1267
			// (set) Token: 0x06001AA9 RID: 6825 RVA: 0x0003A2AE File Offset: 0x000384AE
			public virtual int Timeout
			{
				set
				{
					base.PowerSharpParameters["Timeout"] = value;
				}
			}

			// Token: 0x170004F4 RID: 1268
			// (set) Token: 0x06001AAA RID: 6826 RVA: 0x0003A2C6 File Offset: 0x000384C6
			public virtual int IgnoreExitCode
			{
				set
				{
					base.PowerSharpParameters["IgnoreExitCode"] = value;
				}
			}

			// Token: 0x170004F5 RID: 1269
			// (set) Token: 0x06001AAB RID: 6827 RVA: 0x0003A2DE File Offset: 0x000384DE
			public virtual uint RetryCount
			{
				set
				{
					base.PowerSharpParameters["RetryCount"] = value;
				}
			}

			// Token: 0x170004F6 RID: 1270
			// (set) Token: 0x06001AAC RID: 6828 RVA: 0x0003A2F6 File Offset: 0x000384F6
			public virtual uint RetryDelay
			{
				set
				{
					base.PowerSharpParameters["RetryDelay"] = value;
				}
			}

			// Token: 0x170004F7 RID: 1271
			// (set) Token: 0x06001AAD RID: 6829 RVA: 0x0003A30E File Offset: 0x0003850E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170004F8 RID: 1272
			// (set) Token: 0x06001AAE RID: 6830 RVA: 0x0003A326 File Offset: 0x00038526
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170004F9 RID: 1273
			// (set) Token: 0x06001AAF RID: 6831 RVA: 0x0003A33E File Offset: 0x0003853E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170004FA RID: 1274
			// (set) Token: 0x06001AB0 RID: 6832 RVA: 0x0003A356 File Offset: 0x00038556
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
