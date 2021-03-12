using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000103 RID: 259
	public class GetEventloglevelCommand : SyntheticCommandWithPipelineInput<EventCategoryObject, EventCategoryObject>
	{
		// Token: 0x06001ED6 RID: 7894 RVA: 0x0003FBF4 File Offset: 0x0003DDF4
		private GetEventloglevelCommand() : base("Get-Eventloglevel")
		{
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x0003FC01 File Offset: 0x0003DE01
		public GetEventloglevelCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x0003FC10 File Offset: 0x0003DE10
		public virtual GetEventloglevelCommand SetParameters(GetEventloglevelCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x0003FC1A File Offset: 0x0003DE1A
		public virtual GetEventloglevelCommand SetParameters(GetEventloglevelCommand.ServerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x0003FC24 File Offset: 0x0003DE24
		public virtual GetEventloglevelCommand SetParameters(GetEventloglevelCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000104 RID: 260
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000889 RID: 2185
			// (set) Token: 0x06001EDB RID: 7899 RVA: 0x0003FC2E File Offset: 0x0003DE2E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700088A RID: 2186
			// (set) Token: 0x06001EDC RID: 7900 RVA: 0x0003FC41 File Offset: 0x0003DE41
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700088B RID: 2187
			// (set) Token: 0x06001EDD RID: 7901 RVA: 0x0003FC59 File Offset: 0x0003DE59
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700088C RID: 2188
			// (set) Token: 0x06001EDE RID: 7902 RVA: 0x0003FC71 File Offset: 0x0003DE71
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700088D RID: 2189
			// (set) Token: 0x06001EDF RID: 7903 RVA: 0x0003FC89 File Offset: 0x0003DE89
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000105 RID: 261
		public class ServerParameters : ParametersBase
		{
			// Token: 0x1700088E RID: 2190
			// (set) Token: 0x06001EE1 RID: 7905 RVA: 0x0003FCA9 File Offset: 0x0003DEA9
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x1700088F RID: 2191
			// (set) Token: 0x06001EE2 RID: 7906 RVA: 0x0003FCBC File Offset: 0x0003DEBC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000890 RID: 2192
			// (set) Token: 0x06001EE3 RID: 7907 RVA: 0x0003FCCF File Offset: 0x0003DECF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000891 RID: 2193
			// (set) Token: 0x06001EE4 RID: 7908 RVA: 0x0003FCE7 File Offset: 0x0003DEE7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000892 RID: 2194
			// (set) Token: 0x06001EE5 RID: 7909 RVA: 0x0003FCFF File Offset: 0x0003DEFF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000893 RID: 2195
			// (set) Token: 0x06001EE6 RID: 7910 RVA: 0x0003FD17 File Offset: 0x0003DF17
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000106 RID: 262
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000894 RID: 2196
			// (set) Token: 0x06001EE8 RID: 7912 RVA: 0x0003FD37 File Offset: 0x0003DF37
			public virtual ECIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17000895 RID: 2197
			// (set) Token: 0x06001EE9 RID: 7913 RVA: 0x0003FD4A File Offset: 0x0003DF4A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000896 RID: 2198
			// (set) Token: 0x06001EEA RID: 7914 RVA: 0x0003FD5D File Offset: 0x0003DF5D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000897 RID: 2199
			// (set) Token: 0x06001EEB RID: 7915 RVA: 0x0003FD75 File Offset: 0x0003DF75
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000898 RID: 2200
			// (set) Token: 0x06001EEC RID: 7916 RVA: 0x0003FD8D File Offset: 0x0003DF8D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000899 RID: 2201
			// (set) Token: 0x06001EED RID: 7917 RVA: 0x0003FDA5 File Offset: 0x0003DFA5
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
