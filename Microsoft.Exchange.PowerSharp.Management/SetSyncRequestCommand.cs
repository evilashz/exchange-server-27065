using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000AC0 RID: 2752
	public class SetSyncRequestCommand : SyntheticCommandWithPipelineInputNoOutput<SyncRequestIdParameter>
	{
		// Token: 0x06008850 RID: 34896 RVA: 0x000C8C06 File Offset: 0x000C6E06
		private SetSyncRequestCommand() : base("Set-SyncRequest")
		{
		}

		// Token: 0x06008851 RID: 34897 RVA: 0x000C8C13 File Offset: 0x000C6E13
		public SetSyncRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008852 RID: 34898 RVA: 0x000C8C22 File Offset: 0x000C6E22
		public virtual SetSyncRequestCommand SetParameters(SetSyncRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008853 RID: 34899 RVA: 0x000C8C2C File Offset: 0x000C6E2C
		public virtual SetSyncRequestCommand SetParameters(SetSyncRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008854 RID: 34900 RVA: 0x000C8C36 File Offset: 0x000C6E36
		public virtual SetSyncRequestCommand SetParameters(SetSyncRequestCommand.RehomeParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000AC1 RID: 2753
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005E89 RID: 24201
			// (set) Token: 0x06008855 RID: 34901 RVA: 0x000C8C40 File Offset: 0x000C6E40
			public virtual string RemoteServerName
			{
				set
				{
					base.PowerSharpParameters["RemoteServerName"] = value;
				}
			}

			// Token: 0x17005E8A RID: 24202
			// (set) Token: 0x06008856 RID: 34902 RVA: 0x000C8C53 File Offset: 0x000C6E53
			public virtual int RemoteServerPort
			{
				set
				{
					base.PowerSharpParameters["RemoteServerPort"] = value;
				}
			}

			// Token: 0x17005E8B RID: 24203
			// (set) Token: 0x06008857 RID: 34903 RVA: 0x000C8C6B File Offset: 0x000C6E6B
			public virtual string SmtpServerName
			{
				set
				{
					base.PowerSharpParameters["SmtpServerName"] = value;
				}
			}

			// Token: 0x17005E8C RID: 24204
			// (set) Token: 0x06008858 RID: 34904 RVA: 0x000C8C7E File Offset: 0x000C6E7E
			public virtual int SmtpServerPort
			{
				set
				{
					base.PowerSharpParameters["SmtpServerPort"] = value;
				}
			}

			// Token: 0x17005E8D RID: 24205
			// (set) Token: 0x06008859 RID: 34905 RVA: 0x000C8C96 File Offset: 0x000C6E96
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17005E8E RID: 24206
			// (set) Token: 0x0600885A RID: 34906 RVA: 0x000C8CA9 File Offset: 0x000C6EA9
			public virtual AuthenticationMethod Authentication
			{
				set
				{
					base.PowerSharpParameters["Authentication"] = value;
				}
			}

			// Token: 0x17005E8F RID: 24207
			// (set) Token: 0x0600885B RID: 34907 RVA: 0x000C8CC1 File Offset: 0x000C6EC1
			public virtual IMAPSecurityMechanism Security
			{
				set
				{
					base.PowerSharpParameters["Security"] = value;
				}
			}

			// Token: 0x17005E90 RID: 24208
			// (set) Token: 0x0600885C RID: 34908 RVA: 0x000C8CD9 File Offset: 0x000C6ED9
			public virtual TimeSpan IncrementalSyncInterval
			{
				set
				{
					base.PowerSharpParameters["IncrementalSyncInterval"] = value;
				}
			}

			// Token: 0x17005E91 RID: 24209
			// (set) Token: 0x0600885D RID: 34909 RVA: 0x000C8CF1 File Offset: 0x000C6EF1
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005E92 RID: 24210
			// (set) Token: 0x0600885E RID: 34910 RVA: 0x000C8D09 File Offset: 0x000C6F09
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005E93 RID: 24211
			// (set) Token: 0x0600885F RID: 34911 RVA: 0x000C8D21 File Offset: 0x000C6F21
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005E94 RID: 24212
			// (set) Token: 0x06008860 RID: 34912 RVA: 0x000C8D39 File Offset: 0x000C6F39
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005E95 RID: 24213
			// (set) Token: 0x06008861 RID: 34913 RVA: 0x000C8D4C File Offset: 0x000C6F4C
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005E96 RID: 24214
			// (set) Token: 0x06008862 RID: 34914 RVA: 0x000C8D64 File Offset: 0x000C6F64
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005E97 RID: 24215
			// (set) Token: 0x06008863 RID: 34915 RVA: 0x000C8D7C File Offset: 0x000C6F7C
			public virtual SkippableMergeComponent SkipMerging
			{
				set
				{
					base.PowerSharpParameters["SkipMerging"] = value;
				}
			}

			// Token: 0x17005E98 RID: 24216
			// (set) Token: 0x06008864 RID: 34916 RVA: 0x000C8D94 File Offset: 0x000C6F94
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x17005E99 RID: 24217
			// (set) Token: 0x06008865 RID: 34917 RVA: 0x000C8DAC File Offset: 0x000C6FAC
			public virtual DateTime? StartAfter
			{
				set
				{
					base.PowerSharpParameters["StartAfter"] = value;
				}
			}

			// Token: 0x17005E9A RID: 24218
			// (set) Token: 0x06008866 RID: 34918 RVA: 0x000C8DC4 File Offset: 0x000C6FC4
			public virtual DateTime? CompleteAfter
			{
				set
				{
					base.PowerSharpParameters["CompleteAfter"] = value;
				}
			}

			// Token: 0x17005E9B RID: 24219
			// (set) Token: 0x06008867 RID: 34919 RVA: 0x000C8DDC File Offset: 0x000C6FDC
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new SyncRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005E9C RID: 24220
			// (set) Token: 0x06008868 RID: 34920 RVA: 0x000C8DFA File Offset: 0x000C6FFA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005E9D RID: 24221
			// (set) Token: 0x06008869 RID: 34921 RVA: 0x000C8E0D File Offset: 0x000C700D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005E9E RID: 24222
			// (set) Token: 0x0600886A RID: 34922 RVA: 0x000C8E25 File Offset: 0x000C7025
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005E9F RID: 24223
			// (set) Token: 0x0600886B RID: 34923 RVA: 0x000C8E3D File Offset: 0x000C703D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005EA0 RID: 24224
			// (set) Token: 0x0600886C RID: 34924 RVA: 0x000C8E55 File Offset: 0x000C7055
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005EA1 RID: 24225
			// (set) Token: 0x0600886D RID: 34925 RVA: 0x000C8E6D File Offset: 0x000C706D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000AC2 RID: 2754
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005EA2 RID: 24226
			// (set) Token: 0x0600886F RID: 34927 RVA: 0x000C8E8D File Offset: 0x000C708D
			public virtual DateTime? StartAfter
			{
				set
				{
					base.PowerSharpParameters["StartAfter"] = value;
				}
			}

			// Token: 0x17005EA3 RID: 24227
			// (set) Token: 0x06008870 RID: 34928 RVA: 0x000C8EA5 File Offset: 0x000C70A5
			public virtual DateTime? CompleteAfter
			{
				set
				{
					base.PowerSharpParameters["CompleteAfter"] = value;
				}
			}

			// Token: 0x17005EA4 RID: 24228
			// (set) Token: 0x06008871 RID: 34929 RVA: 0x000C8EBD File Offset: 0x000C70BD
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new SyncRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005EA5 RID: 24229
			// (set) Token: 0x06008872 RID: 34930 RVA: 0x000C8EDB File Offset: 0x000C70DB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005EA6 RID: 24230
			// (set) Token: 0x06008873 RID: 34931 RVA: 0x000C8EEE File Offset: 0x000C70EE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005EA7 RID: 24231
			// (set) Token: 0x06008874 RID: 34932 RVA: 0x000C8F06 File Offset: 0x000C7106
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005EA8 RID: 24232
			// (set) Token: 0x06008875 RID: 34933 RVA: 0x000C8F1E File Offset: 0x000C711E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005EA9 RID: 24233
			// (set) Token: 0x06008876 RID: 34934 RVA: 0x000C8F36 File Offset: 0x000C7136
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005EAA RID: 24234
			// (set) Token: 0x06008877 RID: 34935 RVA: 0x000C8F4E File Offset: 0x000C714E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000AC3 RID: 2755
		public class RehomeParameters : ParametersBase
		{
			// Token: 0x17005EAB RID: 24235
			// (set) Token: 0x06008879 RID: 34937 RVA: 0x000C8F6E File Offset: 0x000C716E
			public virtual SwitchParameter RehomeRequest
			{
				set
				{
					base.PowerSharpParameters["RehomeRequest"] = value;
				}
			}

			// Token: 0x17005EAC RID: 24236
			// (set) Token: 0x0600887A RID: 34938 RVA: 0x000C8F86 File Offset: 0x000C7186
			public virtual DateTime? StartAfter
			{
				set
				{
					base.PowerSharpParameters["StartAfter"] = value;
				}
			}

			// Token: 0x17005EAD RID: 24237
			// (set) Token: 0x0600887B RID: 34939 RVA: 0x000C8F9E File Offset: 0x000C719E
			public virtual DateTime? CompleteAfter
			{
				set
				{
					base.PowerSharpParameters["CompleteAfter"] = value;
				}
			}

			// Token: 0x17005EAE RID: 24238
			// (set) Token: 0x0600887C RID: 34940 RVA: 0x000C8FB6 File Offset: 0x000C71B6
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new SyncRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005EAF RID: 24239
			// (set) Token: 0x0600887D RID: 34941 RVA: 0x000C8FD4 File Offset: 0x000C71D4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005EB0 RID: 24240
			// (set) Token: 0x0600887E RID: 34942 RVA: 0x000C8FE7 File Offset: 0x000C71E7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005EB1 RID: 24241
			// (set) Token: 0x0600887F RID: 34943 RVA: 0x000C8FFF File Offset: 0x000C71FF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005EB2 RID: 24242
			// (set) Token: 0x06008880 RID: 34944 RVA: 0x000C9017 File Offset: 0x000C7217
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005EB3 RID: 24243
			// (set) Token: 0x06008881 RID: 34945 RVA: 0x000C902F File Offset: 0x000C722F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005EB4 RID: 24244
			// (set) Token: 0x06008882 RID: 34946 RVA: 0x000C9047 File Offset: 0x000C7247
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
