using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A2C RID: 2604
	public class SetMailboxImportRequestCommand : SyntheticCommandWithPipelineInputNoOutput<MailboxImportRequestIdParameter>
	{
		// Token: 0x06008202 RID: 33282 RVA: 0x000C08D4 File Offset: 0x000BEAD4
		private SetMailboxImportRequestCommand() : base("Set-MailboxImportRequest")
		{
		}

		// Token: 0x06008203 RID: 33283 RVA: 0x000C08E1 File Offset: 0x000BEAE1
		public SetMailboxImportRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008204 RID: 33284 RVA: 0x000C08F0 File Offset: 0x000BEAF0
		public virtual SetMailboxImportRequestCommand SetParameters(SetMailboxImportRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008205 RID: 33285 RVA: 0x000C08FA File Offset: 0x000BEAFA
		public virtual SetMailboxImportRequestCommand SetParameters(SetMailboxImportRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008206 RID: 33286 RVA: 0x000C0904 File Offset: 0x000BEB04
		public virtual SetMailboxImportRequestCommand SetParameters(SetMailboxImportRequestCommand.RehomeParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A2D RID: 2605
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005963 RID: 22883
			// (set) Token: 0x06008207 RID: 33287 RVA: 0x000C090E File Offset: 0x000BEB0E
			public virtual PSCredential RemoteCredential
			{
				set
				{
					base.PowerSharpParameters["RemoteCredential"] = value;
				}
			}

			// Token: 0x17005964 RID: 22884
			// (set) Token: 0x06008208 RID: 33288 RVA: 0x000C0921 File Offset: 0x000BEB21
			public virtual Fqdn RemoteHostName
			{
				set
				{
					base.PowerSharpParameters["RemoteHostName"] = value;
				}
			}

			// Token: 0x17005965 RID: 22885
			// (set) Token: 0x06008209 RID: 33289 RVA: 0x000C0934 File Offset: 0x000BEB34
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005966 RID: 22886
			// (set) Token: 0x0600820A RID: 33290 RVA: 0x000C094C File Offset: 0x000BEB4C
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005967 RID: 22887
			// (set) Token: 0x0600820B RID: 33291 RVA: 0x000C0964 File Offset: 0x000BEB64
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005968 RID: 22888
			// (set) Token: 0x0600820C RID: 33292 RVA: 0x000C097C File Offset: 0x000BEB7C
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005969 RID: 22889
			// (set) Token: 0x0600820D RID: 33293 RVA: 0x000C098F File Offset: 0x000BEB8F
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x1700596A RID: 22890
			// (set) Token: 0x0600820E RID: 33294 RVA: 0x000C09A7 File Offset: 0x000BEBA7
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x1700596B RID: 22891
			// (set) Token: 0x0600820F RID: 33295 RVA: 0x000C09BF File Offset: 0x000BEBBF
			public virtual SkippableMergeComponent SkipMerging
			{
				set
				{
					base.PowerSharpParameters["SkipMerging"] = value;
				}
			}

			// Token: 0x1700596C RID: 22892
			// (set) Token: 0x06008210 RID: 33296 RVA: 0x000C09D7 File Offset: 0x000BEBD7
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x1700596D RID: 22893
			// (set) Token: 0x06008211 RID: 33297 RVA: 0x000C09EF File Offset: 0x000BEBEF
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxImportRequestIdParameter(value) : null);
				}
			}

			// Token: 0x1700596E RID: 22894
			// (set) Token: 0x06008212 RID: 33298 RVA: 0x000C0A0D File Offset: 0x000BEC0D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700596F RID: 22895
			// (set) Token: 0x06008213 RID: 33299 RVA: 0x000C0A20 File Offset: 0x000BEC20
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005970 RID: 22896
			// (set) Token: 0x06008214 RID: 33300 RVA: 0x000C0A38 File Offset: 0x000BEC38
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005971 RID: 22897
			// (set) Token: 0x06008215 RID: 33301 RVA: 0x000C0A50 File Offset: 0x000BEC50
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005972 RID: 22898
			// (set) Token: 0x06008216 RID: 33302 RVA: 0x000C0A68 File Offset: 0x000BEC68
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005973 RID: 22899
			// (set) Token: 0x06008217 RID: 33303 RVA: 0x000C0A80 File Offset: 0x000BEC80
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000A2E RID: 2606
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005974 RID: 22900
			// (set) Token: 0x06008219 RID: 33305 RVA: 0x000C0AA0 File Offset: 0x000BECA0
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxImportRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005975 RID: 22901
			// (set) Token: 0x0600821A RID: 33306 RVA: 0x000C0ABE File Offset: 0x000BECBE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005976 RID: 22902
			// (set) Token: 0x0600821B RID: 33307 RVA: 0x000C0AD1 File Offset: 0x000BECD1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005977 RID: 22903
			// (set) Token: 0x0600821C RID: 33308 RVA: 0x000C0AE9 File Offset: 0x000BECE9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005978 RID: 22904
			// (set) Token: 0x0600821D RID: 33309 RVA: 0x000C0B01 File Offset: 0x000BED01
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005979 RID: 22905
			// (set) Token: 0x0600821E RID: 33310 RVA: 0x000C0B19 File Offset: 0x000BED19
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700597A RID: 22906
			// (set) Token: 0x0600821F RID: 33311 RVA: 0x000C0B31 File Offset: 0x000BED31
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000A2F RID: 2607
		public class RehomeParameters : ParametersBase
		{
			// Token: 0x1700597B RID: 22907
			// (set) Token: 0x06008221 RID: 33313 RVA: 0x000C0B51 File Offset: 0x000BED51
			public virtual SwitchParameter RehomeRequest
			{
				set
				{
					base.PowerSharpParameters["RehomeRequest"] = value;
				}
			}

			// Token: 0x1700597C RID: 22908
			// (set) Token: 0x06008222 RID: 33314 RVA: 0x000C0B69 File Offset: 0x000BED69
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxImportRequestIdParameter(value) : null);
				}
			}

			// Token: 0x1700597D RID: 22909
			// (set) Token: 0x06008223 RID: 33315 RVA: 0x000C0B87 File Offset: 0x000BED87
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700597E RID: 22910
			// (set) Token: 0x06008224 RID: 33316 RVA: 0x000C0B9A File Offset: 0x000BED9A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700597F RID: 22911
			// (set) Token: 0x06008225 RID: 33317 RVA: 0x000C0BB2 File Offset: 0x000BEDB2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005980 RID: 22912
			// (set) Token: 0x06008226 RID: 33318 RVA: 0x000C0BCA File Offset: 0x000BEDCA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005981 RID: 22913
			// (set) Token: 0x06008227 RID: 33319 RVA: 0x000C0BE2 File Offset: 0x000BEDE2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005982 RID: 22914
			// (set) Token: 0x06008228 RID: 33320 RVA: 0x000C0BFA File Offset: 0x000BEDFA
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
