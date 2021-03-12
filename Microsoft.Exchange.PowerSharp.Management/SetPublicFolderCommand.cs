using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000250 RID: 592
	public class SetPublicFolderCommand : SyntheticCommandWithPipelineInputNoOutput<PublicFolder>
	{
		// Token: 0x06002C3C RID: 11324 RVA: 0x00051289 File Offset: 0x0004F489
		private SetPublicFolderCommand() : base("Set-PublicFolder")
		{
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x00051296 File Offset: 0x0004F496
		public SetPublicFolderCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x000512A5 File Offset: 0x0004F4A5
		public virtual SetPublicFolderCommand SetParameters(SetPublicFolderCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x000512AF File Offset: 0x0004F4AF
		public virtual SetPublicFolderCommand SetParameters(SetPublicFolderCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000251 RID: 593
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001355 RID: 4949
			// (set) Token: 0x06002C40 RID: 11328 RVA: 0x000512B9 File Offset: 0x0004F4B9
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17001356 RID: 4950
			// (set) Token: 0x06002C41 RID: 11329 RVA: 0x000512D7 File Offset: 0x0004F4D7
			public virtual string Path
			{
				set
				{
					base.PowerSharpParameters["Path"] = ((value != null) ? new PublicFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17001357 RID: 4951
			// (set) Token: 0x06002C42 RID: 11330 RVA: 0x000512F5 File Offset: 0x0004F4F5
			public virtual string OverrideContentMailbox
			{
				set
				{
					base.PowerSharpParameters["OverrideContentMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001358 RID: 4952
			// (set) Token: 0x06002C43 RID: 11331 RVA: 0x00051313 File Offset: 0x0004F513
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001359 RID: 4953
			// (set) Token: 0x06002C44 RID: 11332 RVA: 0x0005132B File Offset: 0x0004F52B
			public virtual bool? MailEnabled
			{
				set
				{
					base.PowerSharpParameters["MailEnabled"] = value;
				}
			}

			// Token: 0x1700135A RID: 4954
			// (set) Token: 0x06002C45 RID: 11333 RVA: 0x00051343 File Offset: 0x0004F543
			public virtual Guid? MailRecipientGuid
			{
				set
				{
					base.PowerSharpParameters["MailRecipientGuid"] = value;
				}
			}

			// Token: 0x1700135B RID: 4955
			// (set) Token: 0x06002C46 RID: 11334 RVA: 0x0005135B File Offset: 0x0004F55B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700135C RID: 4956
			// (set) Token: 0x06002C47 RID: 11335 RVA: 0x0005136E File Offset: 0x0004F56E
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700135D RID: 4957
			// (set) Token: 0x06002C48 RID: 11336 RVA: 0x00051381 File Offset: 0x0004F581
			public virtual CultureInfo EformsLocaleId
			{
				set
				{
					base.PowerSharpParameters["EformsLocaleId"] = value;
				}
			}

			// Token: 0x1700135E RID: 4958
			// (set) Token: 0x06002C49 RID: 11337 RVA: 0x00051394 File Offset: 0x0004F594
			public virtual bool PerUserReadStateEnabled
			{
				set
				{
					base.PowerSharpParameters["PerUserReadStateEnabled"] = value;
				}
			}

			// Token: 0x1700135F RID: 4959
			// (set) Token: 0x06002C4A RID: 11338 RVA: 0x000513AC File Offset: 0x0004F5AC
			public virtual EnhancedTimeSpan? AgeLimit
			{
				set
				{
					base.PowerSharpParameters["AgeLimit"] = value;
				}
			}

			// Token: 0x17001360 RID: 4960
			// (set) Token: 0x06002C4B RID: 11339 RVA: 0x000513C4 File Offset: 0x0004F5C4
			public virtual EnhancedTimeSpan? RetainDeletedItemsFor
			{
				set
				{
					base.PowerSharpParameters["RetainDeletedItemsFor"] = value;
				}
			}

			// Token: 0x17001361 RID: 4961
			// (set) Token: 0x06002C4C RID: 11340 RVA: 0x000513DC File Offset: 0x0004F5DC
			public virtual Unlimited<ByteQuantifiedSize>? ProhibitPostQuota
			{
				set
				{
					base.PowerSharpParameters["ProhibitPostQuota"] = value;
				}
			}

			// Token: 0x17001362 RID: 4962
			// (set) Token: 0x06002C4D RID: 11341 RVA: 0x000513F4 File Offset: 0x0004F5F4
			public virtual Unlimited<ByteQuantifiedSize>? IssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["IssueWarningQuota"] = value;
				}
			}

			// Token: 0x17001363 RID: 4963
			// (set) Token: 0x06002C4E RID: 11342 RVA: 0x0005140C File Offset: 0x0004F60C
			public virtual Unlimited<ByteQuantifiedSize>? MaxItemSize
			{
				set
				{
					base.PowerSharpParameters["MaxItemSize"] = value;
				}
			}

			// Token: 0x17001364 RID: 4964
			// (set) Token: 0x06002C4F RID: 11343 RVA: 0x00051424 File Offset: 0x0004F624
			public virtual ExDateTime? LastMovedTime
			{
				set
				{
					base.PowerSharpParameters["LastMovedTime"] = value;
				}
			}

			// Token: 0x17001365 RID: 4965
			// (set) Token: 0x06002C50 RID: 11344 RVA: 0x0005143C File Offset: 0x0004F63C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001366 RID: 4966
			// (set) Token: 0x06002C51 RID: 11345 RVA: 0x00051454 File Offset: 0x0004F654
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001367 RID: 4967
			// (set) Token: 0x06002C52 RID: 11346 RVA: 0x0005146C File Offset: 0x0004F66C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001368 RID: 4968
			// (set) Token: 0x06002C53 RID: 11347 RVA: 0x00051484 File Offset: 0x0004F684
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001369 RID: 4969
			// (set) Token: 0x06002C54 RID: 11348 RVA: 0x0005149C File Offset: 0x0004F69C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000252 RID: 594
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700136A RID: 4970
			// (set) Token: 0x06002C56 RID: 11350 RVA: 0x000514BC File Offset: 0x0004F6BC
			public virtual string Path
			{
				set
				{
					base.PowerSharpParameters["Path"] = ((value != null) ? new PublicFolderIdParameter(value) : null);
				}
			}

			// Token: 0x1700136B RID: 4971
			// (set) Token: 0x06002C57 RID: 11351 RVA: 0x000514DA File Offset: 0x0004F6DA
			public virtual string OverrideContentMailbox
			{
				set
				{
					base.PowerSharpParameters["OverrideContentMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700136C RID: 4972
			// (set) Token: 0x06002C58 RID: 11352 RVA: 0x000514F8 File Offset: 0x0004F6F8
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700136D RID: 4973
			// (set) Token: 0x06002C59 RID: 11353 RVA: 0x00051510 File Offset: 0x0004F710
			public virtual bool? MailEnabled
			{
				set
				{
					base.PowerSharpParameters["MailEnabled"] = value;
				}
			}

			// Token: 0x1700136E RID: 4974
			// (set) Token: 0x06002C5A RID: 11354 RVA: 0x00051528 File Offset: 0x0004F728
			public virtual Guid? MailRecipientGuid
			{
				set
				{
					base.PowerSharpParameters["MailRecipientGuid"] = value;
				}
			}

			// Token: 0x1700136F RID: 4975
			// (set) Token: 0x06002C5B RID: 11355 RVA: 0x00051540 File Offset: 0x0004F740
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001370 RID: 4976
			// (set) Token: 0x06002C5C RID: 11356 RVA: 0x00051553 File Offset: 0x0004F753
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001371 RID: 4977
			// (set) Token: 0x06002C5D RID: 11357 RVA: 0x00051566 File Offset: 0x0004F766
			public virtual CultureInfo EformsLocaleId
			{
				set
				{
					base.PowerSharpParameters["EformsLocaleId"] = value;
				}
			}

			// Token: 0x17001372 RID: 4978
			// (set) Token: 0x06002C5E RID: 11358 RVA: 0x00051579 File Offset: 0x0004F779
			public virtual bool PerUserReadStateEnabled
			{
				set
				{
					base.PowerSharpParameters["PerUserReadStateEnabled"] = value;
				}
			}

			// Token: 0x17001373 RID: 4979
			// (set) Token: 0x06002C5F RID: 11359 RVA: 0x00051591 File Offset: 0x0004F791
			public virtual EnhancedTimeSpan? AgeLimit
			{
				set
				{
					base.PowerSharpParameters["AgeLimit"] = value;
				}
			}

			// Token: 0x17001374 RID: 4980
			// (set) Token: 0x06002C60 RID: 11360 RVA: 0x000515A9 File Offset: 0x0004F7A9
			public virtual EnhancedTimeSpan? RetainDeletedItemsFor
			{
				set
				{
					base.PowerSharpParameters["RetainDeletedItemsFor"] = value;
				}
			}

			// Token: 0x17001375 RID: 4981
			// (set) Token: 0x06002C61 RID: 11361 RVA: 0x000515C1 File Offset: 0x0004F7C1
			public virtual Unlimited<ByteQuantifiedSize>? ProhibitPostQuota
			{
				set
				{
					base.PowerSharpParameters["ProhibitPostQuota"] = value;
				}
			}

			// Token: 0x17001376 RID: 4982
			// (set) Token: 0x06002C62 RID: 11362 RVA: 0x000515D9 File Offset: 0x0004F7D9
			public virtual Unlimited<ByteQuantifiedSize>? IssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["IssueWarningQuota"] = value;
				}
			}

			// Token: 0x17001377 RID: 4983
			// (set) Token: 0x06002C63 RID: 11363 RVA: 0x000515F1 File Offset: 0x0004F7F1
			public virtual Unlimited<ByteQuantifiedSize>? MaxItemSize
			{
				set
				{
					base.PowerSharpParameters["MaxItemSize"] = value;
				}
			}

			// Token: 0x17001378 RID: 4984
			// (set) Token: 0x06002C64 RID: 11364 RVA: 0x00051609 File Offset: 0x0004F809
			public virtual ExDateTime? LastMovedTime
			{
				set
				{
					base.PowerSharpParameters["LastMovedTime"] = value;
				}
			}

			// Token: 0x17001379 RID: 4985
			// (set) Token: 0x06002C65 RID: 11365 RVA: 0x00051621 File Offset: 0x0004F821
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700137A RID: 4986
			// (set) Token: 0x06002C66 RID: 11366 RVA: 0x00051639 File Offset: 0x0004F839
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700137B RID: 4987
			// (set) Token: 0x06002C67 RID: 11367 RVA: 0x00051651 File Offset: 0x0004F851
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700137C RID: 4988
			// (set) Token: 0x06002C68 RID: 11368 RVA: 0x00051669 File Offset: 0x0004F869
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700137D RID: 4989
			// (set) Token: 0x06002C69 RID: 11369 RVA: 0x00051681 File Offset: 0x0004F881
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
