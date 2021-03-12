using System;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200073A RID: 1850
	public class AddIPBlockListEntryCommand : SyntheticCommandWithPipelineInput<IPBlockListEntry, IPBlockListEntry>
	{
		// Token: 0x06005F03 RID: 24323 RVA: 0x00092E65 File Offset: 0x00091065
		private AddIPBlockListEntryCommand() : base("Add-IPBlockListEntry")
		{
		}

		// Token: 0x06005F04 RID: 24324 RVA: 0x00092E72 File Offset: 0x00091072
		public AddIPBlockListEntryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005F05 RID: 24325 RVA: 0x00092E81 File Offset: 0x00091081
		public virtual AddIPBlockListEntryCommand SetParameters(AddIPBlockListEntryCommand.IPRangeParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005F06 RID: 24326 RVA: 0x00092E8B File Offset: 0x0009108B
		public virtual AddIPBlockListEntryCommand SetParameters(AddIPBlockListEntryCommand.IPAddressParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005F07 RID: 24327 RVA: 0x00092E95 File Offset: 0x00091095
		public virtual AddIPBlockListEntryCommand SetParameters(AddIPBlockListEntryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200073B RID: 1851
		public class IPRangeParameters : ParametersBase
		{
			// Token: 0x17003C48 RID: 15432
			// (set) Token: 0x06005F08 RID: 24328 RVA: 0x00092E9F File Offset: 0x0009109F
			public virtual IPRange IPRange
			{
				set
				{
					base.PowerSharpParameters["IPRange"] = value;
				}
			}

			// Token: 0x17003C49 RID: 15433
			// (set) Token: 0x06005F09 RID: 24329 RVA: 0x00092EB2 File Offset: 0x000910B2
			public virtual DateTime ExpirationTime
			{
				set
				{
					base.PowerSharpParameters["ExpirationTime"] = value;
				}
			}

			// Token: 0x17003C4A RID: 15434
			// (set) Token: 0x06005F0A RID: 24330 RVA: 0x00092ECA File Offset: 0x000910CA
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x17003C4B RID: 15435
			// (set) Token: 0x06005F0B RID: 24331 RVA: 0x00092EDD File Offset: 0x000910DD
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17003C4C RID: 15436
			// (set) Token: 0x06005F0C RID: 24332 RVA: 0x00092EF0 File Offset: 0x000910F0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003C4D RID: 15437
			// (set) Token: 0x06005F0D RID: 24333 RVA: 0x00092F08 File Offset: 0x00091108
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003C4E RID: 15438
			// (set) Token: 0x06005F0E RID: 24334 RVA: 0x00092F20 File Offset: 0x00091120
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003C4F RID: 15439
			// (set) Token: 0x06005F0F RID: 24335 RVA: 0x00092F38 File Offset: 0x00091138
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003C50 RID: 15440
			// (set) Token: 0x06005F10 RID: 24336 RVA: 0x00092F50 File Offset: 0x00091150
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200073C RID: 1852
		public class IPAddressParameters : ParametersBase
		{
			// Token: 0x17003C51 RID: 15441
			// (set) Token: 0x06005F12 RID: 24338 RVA: 0x00092F70 File Offset: 0x00091170
			public virtual IPAddress IPAddress
			{
				set
				{
					base.PowerSharpParameters["IPAddress"] = value;
				}
			}

			// Token: 0x17003C52 RID: 15442
			// (set) Token: 0x06005F13 RID: 24339 RVA: 0x00092F83 File Offset: 0x00091183
			public virtual DateTime ExpirationTime
			{
				set
				{
					base.PowerSharpParameters["ExpirationTime"] = value;
				}
			}

			// Token: 0x17003C53 RID: 15443
			// (set) Token: 0x06005F14 RID: 24340 RVA: 0x00092F9B File Offset: 0x0009119B
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x17003C54 RID: 15444
			// (set) Token: 0x06005F15 RID: 24341 RVA: 0x00092FAE File Offset: 0x000911AE
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17003C55 RID: 15445
			// (set) Token: 0x06005F16 RID: 24342 RVA: 0x00092FC1 File Offset: 0x000911C1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003C56 RID: 15446
			// (set) Token: 0x06005F17 RID: 24343 RVA: 0x00092FD9 File Offset: 0x000911D9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003C57 RID: 15447
			// (set) Token: 0x06005F18 RID: 24344 RVA: 0x00092FF1 File Offset: 0x000911F1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003C58 RID: 15448
			// (set) Token: 0x06005F19 RID: 24345 RVA: 0x00093009 File Offset: 0x00091209
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003C59 RID: 15449
			// (set) Token: 0x06005F1A RID: 24346 RVA: 0x00093021 File Offset: 0x00091221
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200073D RID: 1853
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003C5A RID: 15450
			// (set) Token: 0x06005F1C RID: 24348 RVA: 0x00093041 File Offset: 0x00091241
			public virtual DateTime ExpirationTime
			{
				set
				{
					base.PowerSharpParameters["ExpirationTime"] = value;
				}
			}

			// Token: 0x17003C5B RID: 15451
			// (set) Token: 0x06005F1D RID: 24349 RVA: 0x00093059 File Offset: 0x00091259
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x17003C5C RID: 15452
			// (set) Token: 0x06005F1E RID: 24350 RVA: 0x0009306C File Offset: 0x0009126C
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17003C5D RID: 15453
			// (set) Token: 0x06005F1F RID: 24351 RVA: 0x0009307F File Offset: 0x0009127F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003C5E RID: 15454
			// (set) Token: 0x06005F20 RID: 24352 RVA: 0x00093097 File Offset: 0x00091297
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003C5F RID: 15455
			// (set) Token: 0x06005F21 RID: 24353 RVA: 0x000930AF File Offset: 0x000912AF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003C60 RID: 15456
			// (set) Token: 0x06005F22 RID: 24354 RVA: 0x000930C7 File Offset: 0x000912C7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003C61 RID: 15457
			// (set) Token: 0x06005F23 RID: 24355 RVA: 0x000930DF File Offset: 0x000912DF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
