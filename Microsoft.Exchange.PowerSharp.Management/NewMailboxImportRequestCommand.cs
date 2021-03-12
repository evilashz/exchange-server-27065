using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A22 RID: 2594
	public class NewMailboxImportRequestCommand : SyntheticCommandWithPipelineInput<MailboxImportRequest, MailboxImportRequest>
	{
		// Token: 0x06008198 RID: 33176 RVA: 0x000C0049 File Offset: 0x000BE249
		private NewMailboxImportRequestCommand() : base("New-MailboxImportRequest")
		{
		}

		// Token: 0x06008199 RID: 33177 RVA: 0x000C0056 File Offset: 0x000BE256
		public NewMailboxImportRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600819A RID: 33178 RVA: 0x000C0065 File Offset: 0x000BE265
		public virtual NewMailboxImportRequestCommand SetParameters(NewMailboxImportRequestCommand.MailboxImportRequestParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600819B RID: 33179 RVA: 0x000C006F File Offset: 0x000BE26F
		public virtual NewMailboxImportRequestCommand SetParameters(NewMailboxImportRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A23 RID: 2595
		public class MailboxImportRequestParameters : ParametersBase
		{
			// Token: 0x1700590D RID: 22797
			// (set) Token: 0x0600819C RID: 33180 RVA: 0x000C0079 File Offset: 0x000BE279
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x1700590E RID: 22798
			// (set) Token: 0x0600819D RID: 33181 RVA: 0x000C0097 File Offset: 0x000BE297
			public virtual LongPath FilePath
			{
				set
				{
					base.PowerSharpParameters["FilePath"] = value;
				}
			}

			// Token: 0x1700590F RID: 22799
			// (set) Token: 0x0600819E RID: 33182 RVA: 0x000C00AA File Offset: 0x000BE2AA
			public virtual int? ContentCodePage
			{
				set
				{
					base.PowerSharpParameters["ContentCodePage"] = value;
				}
			}

			// Token: 0x17005910 RID: 22800
			// (set) Token: 0x0600819F RID: 33183 RVA: 0x000C00C2 File Offset: 0x000BE2C2
			public virtual string SourceRootFolder
			{
				set
				{
					base.PowerSharpParameters["SourceRootFolder"] = value;
				}
			}

			// Token: 0x17005911 RID: 22801
			// (set) Token: 0x060081A0 RID: 33184 RVA: 0x000C00D5 File Offset: 0x000BE2D5
			public virtual string TargetRootFolder
			{
				set
				{
					base.PowerSharpParameters["TargetRootFolder"] = value;
				}
			}

			// Token: 0x17005912 RID: 22802
			// (set) Token: 0x060081A1 RID: 33185 RVA: 0x000C00E8 File Offset: 0x000BE2E8
			public virtual SwitchParameter IsArchive
			{
				set
				{
					base.PowerSharpParameters["IsArchive"] = value;
				}
			}

			// Token: 0x17005913 RID: 22803
			// (set) Token: 0x060081A2 RID: 33186 RVA: 0x000C0100 File Offset: 0x000BE300
			public virtual PSCredential RemoteCredential
			{
				set
				{
					base.PowerSharpParameters["RemoteCredential"] = value;
				}
			}

			// Token: 0x17005914 RID: 22804
			// (set) Token: 0x060081A3 RID: 33187 RVA: 0x000C0113 File Offset: 0x000BE313
			public virtual Fqdn RemoteHostName
			{
				set
				{
					base.PowerSharpParameters["RemoteHostName"] = value;
				}
			}

			// Token: 0x17005915 RID: 22805
			// (set) Token: 0x060081A4 RID: 33188 RVA: 0x000C0126 File Offset: 0x000BE326
			public virtual string IncludeFolders
			{
				set
				{
					base.PowerSharpParameters["IncludeFolders"] = value;
				}
			}

			// Token: 0x17005916 RID: 22806
			// (set) Token: 0x060081A5 RID: 33189 RVA: 0x000C0139 File Offset: 0x000BE339
			public virtual string ExcludeFolders
			{
				set
				{
					base.PowerSharpParameters["ExcludeFolders"] = value;
				}
			}

			// Token: 0x17005917 RID: 22807
			// (set) Token: 0x060081A6 RID: 33190 RVA: 0x000C014C File Offset: 0x000BE34C
			public virtual SwitchParameter ExcludeDumpster
			{
				set
				{
					base.PowerSharpParameters["ExcludeDumpster"] = value;
				}
			}

			// Token: 0x17005918 RID: 22808
			// (set) Token: 0x060081A7 RID: 33191 RVA: 0x000C0164 File Offset: 0x000BE364
			public virtual ConflictResolutionOption ConflictResolutionOption
			{
				set
				{
					base.PowerSharpParameters["ConflictResolutionOption"] = value;
				}
			}

			// Token: 0x17005919 RID: 22809
			// (set) Token: 0x060081A8 RID: 33192 RVA: 0x000C017C File Offset: 0x000BE37C
			public virtual FAICopyOption AssociatedMessagesCopyOption
			{
				set
				{
					base.PowerSharpParameters["AssociatedMessagesCopyOption"] = value;
				}
			}

			// Token: 0x1700591A RID: 22810
			// (set) Token: 0x060081A9 RID: 33193 RVA: 0x000C0194 File Offset: 0x000BE394
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x1700591B RID: 22811
			// (set) Token: 0x060081AA RID: 33194 RVA: 0x000C01AC File Offset: 0x000BE3AC
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x1700591C RID: 22812
			// (set) Token: 0x060081AB RID: 33195 RVA: 0x000C01C4 File Offset: 0x000BE3C4
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x1700591D RID: 22813
			// (set) Token: 0x060081AC RID: 33196 RVA: 0x000C01DC File Offset: 0x000BE3DC
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x1700591E RID: 22814
			// (set) Token: 0x060081AD RID: 33197 RVA: 0x000C01EF File Offset: 0x000BE3EF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700591F RID: 22815
			// (set) Token: 0x060081AE RID: 33198 RVA: 0x000C0202 File Offset: 0x000BE402
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005920 RID: 22816
			// (set) Token: 0x060081AF RID: 33199 RVA: 0x000C021A File Offset: 0x000BE41A
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005921 RID: 22817
			// (set) Token: 0x060081B0 RID: 33200 RVA: 0x000C022D File Offset: 0x000BE42D
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17005922 RID: 22818
			// (set) Token: 0x060081B1 RID: 33201 RVA: 0x000C0240 File Offset: 0x000BE440
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005923 RID: 22819
			// (set) Token: 0x060081B2 RID: 33202 RVA: 0x000C0258 File Offset: 0x000BE458
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x17005924 RID: 22820
			// (set) Token: 0x060081B3 RID: 33203 RVA: 0x000C0270 File Offset: 0x000BE470
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005925 RID: 22821
			// (set) Token: 0x060081B4 RID: 33204 RVA: 0x000C0288 File Offset: 0x000BE488
			public virtual SkippableMergeComponent SkipMerging
			{
				set
				{
					base.PowerSharpParameters["SkipMerging"] = value;
				}
			}

			// Token: 0x17005926 RID: 22822
			// (set) Token: 0x060081B5 RID: 33205 RVA: 0x000C02A0 File Offset: 0x000BE4A0
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x17005927 RID: 22823
			// (set) Token: 0x060081B6 RID: 33206 RVA: 0x000C02B8 File Offset: 0x000BE4B8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005928 RID: 22824
			// (set) Token: 0x060081B7 RID: 33207 RVA: 0x000C02D0 File Offset: 0x000BE4D0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005929 RID: 22825
			// (set) Token: 0x060081B8 RID: 33208 RVA: 0x000C02E8 File Offset: 0x000BE4E8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700592A RID: 22826
			// (set) Token: 0x060081B9 RID: 33209 RVA: 0x000C0300 File Offset: 0x000BE500
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700592B RID: 22827
			// (set) Token: 0x060081BA RID: 33210 RVA: 0x000C0318 File Offset: 0x000BE518
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000A24 RID: 2596
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700592C RID: 22828
			// (set) Token: 0x060081BC RID: 33212 RVA: 0x000C0338 File Offset: 0x000BE538
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x1700592D RID: 22829
			// (set) Token: 0x060081BD RID: 33213 RVA: 0x000C0350 File Offset: 0x000BE550
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x1700592E RID: 22830
			// (set) Token: 0x060081BE RID: 33214 RVA: 0x000C0368 File Offset: 0x000BE568
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x1700592F RID: 22831
			// (set) Token: 0x060081BF RID: 33215 RVA: 0x000C0380 File Offset: 0x000BE580
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005930 RID: 22832
			// (set) Token: 0x060081C0 RID: 33216 RVA: 0x000C0393 File Offset: 0x000BE593
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005931 RID: 22833
			// (set) Token: 0x060081C1 RID: 33217 RVA: 0x000C03A6 File Offset: 0x000BE5A6
			public virtual SwitchParameter Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005932 RID: 22834
			// (set) Token: 0x060081C2 RID: 33218 RVA: 0x000C03BE File Offset: 0x000BE5BE
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005933 RID: 22835
			// (set) Token: 0x060081C3 RID: 33219 RVA: 0x000C03D1 File Offset: 0x000BE5D1
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17005934 RID: 22836
			// (set) Token: 0x060081C4 RID: 33220 RVA: 0x000C03E4 File Offset: 0x000BE5E4
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005935 RID: 22837
			// (set) Token: 0x060081C5 RID: 33221 RVA: 0x000C03FC File Offset: 0x000BE5FC
			public virtual RequestWorkloadType WorkloadType
			{
				set
				{
					base.PowerSharpParameters["WorkloadType"] = value;
				}
			}

			// Token: 0x17005936 RID: 22838
			// (set) Token: 0x060081C6 RID: 33222 RVA: 0x000C0414 File Offset: 0x000BE614
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005937 RID: 22839
			// (set) Token: 0x060081C7 RID: 33223 RVA: 0x000C042C File Offset: 0x000BE62C
			public virtual SkippableMergeComponent SkipMerging
			{
				set
				{
					base.PowerSharpParameters["SkipMerging"] = value;
				}
			}

			// Token: 0x17005938 RID: 22840
			// (set) Token: 0x060081C8 RID: 33224 RVA: 0x000C0444 File Offset: 0x000BE644
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x17005939 RID: 22841
			// (set) Token: 0x060081C9 RID: 33225 RVA: 0x000C045C File Offset: 0x000BE65C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700593A RID: 22842
			// (set) Token: 0x060081CA RID: 33226 RVA: 0x000C0474 File Offset: 0x000BE674
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700593B RID: 22843
			// (set) Token: 0x060081CB RID: 33227 RVA: 0x000C048C File Offset: 0x000BE68C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700593C RID: 22844
			// (set) Token: 0x060081CC RID: 33228 RVA: 0x000C04A4 File Offset: 0x000BE6A4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700593D RID: 22845
			// (set) Token: 0x060081CD RID: 33229 RVA: 0x000C04BC File Offset: 0x000BE6BC
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
