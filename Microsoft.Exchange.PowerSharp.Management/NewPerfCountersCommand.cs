using System;
using System.Management.Automation;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002F7 RID: 759
	public class NewPerfCountersCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x0600331F RID: 13087 RVA: 0x0005A342 File Offset: 0x00058542
		private NewPerfCountersCommand() : base("New-PerfCounters")
		{
		}

		// Token: 0x06003320 RID: 13088 RVA: 0x0005A34F File Offset: 0x0005854F
		public NewPerfCountersCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003321 RID: 13089 RVA: 0x0005A35E File Offset: 0x0005855E
		public virtual NewPerfCountersCommand SetParameters(NewPerfCountersCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002F8 RID: 760
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170018EA RID: 6378
			// (set) Token: 0x06003322 RID: 13090 RVA: 0x0005A368 File Offset: 0x00058568
			public virtual int FileMappingSize
			{
				set
				{
					base.PowerSharpParameters["FileMappingSize"] = value;
				}
			}

			// Token: 0x170018EB RID: 6379
			// (set) Token: 0x06003323 RID: 13091 RVA: 0x0005A380 File Offset: 0x00058580
			public virtual string DefinitionFileName
			{
				set
				{
					base.PowerSharpParameters["DefinitionFileName"] = value;
				}
			}

			// Token: 0x170018EC RID: 6380
			// (set) Token: 0x06003324 RID: 13092 RVA: 0x0005A393 File Offset: 0x00058593
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170018ED RID: 6381
			// (set) Token: 0x06003325 RID: 13093 RVA: 0x0005A3AB File Offset: 0x000585AB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170018EE RID: 6382
			// (set) Token: 0x06003326 RID: 13094 RVA: 0x0005A3C3 File Offset: 0x000585C3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170018EF RID: 6383
			// (set) Token: 0x06003327 RID: 13095 RVA: 0x0005A3DB File Offset: 0x000585DB
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
