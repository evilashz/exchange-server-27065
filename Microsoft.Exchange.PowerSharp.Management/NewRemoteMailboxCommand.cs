using System;
using System.Globalization;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D5B RID: 3419
	public class NewRemoteMailboxCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x0600B4BA RID: 46266 RVA: 0x00104418 File Offset: 0x00102618
		private NewRemoteMailboxCommand() : base("New-RemoteMailbox")
		{
		}

		// Token: 0x0600B4BB RID: 46267 RVA: 0x00104425 File Offset: 0x00102625
		public NewRemoteMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B4BC RID: 46268 RVA: 0x00104434 File Offset: 0x00102634
		public virtual NewRemoteMailboxCommand SetParameters(NewRemoteMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B4BD RID: 46269 RVA: 0x0010443E File Offset: 0x0010263E
		public virtual NewRemoteMailboxCommand SetParameters(NewRemoteMailboxCommand.RoomParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B4BE RID: 46270 RVA: 0x00104448 File Offset: 0x00102648
		public virtual NewRemoteMailboxCommand SetParameters(NewRemoteMailboxCommand.EquipmentParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B4BF RID: 46271 RVA: 0x00104452 File Offset: 0x00102652
		public virtual NewRemoteMailboxCommand SetParameters(NewRemoteMailboxCommand.SharedParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B4C0 RID: 46272 RVA: 0x0010445C File Offset: 0x0010265C
		public virtual NewRemoteMailboxCommand SetParameters(NewRemoteMailboxCommand.DisabledUserParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B4C1 RID: 46273 RVA: 0x00104466 File Offset: 0x00102666
		public virtual NewRemoteMailboxCommand SetParameters(NewRemoteMailboxCommand.EnabledUserParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D5C RID: 3420
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170085BD RID: 34237
			// (set) Token: 0x0600B4C2 RID: 46274 RVA: 0x00104470 File Offset: 0x00102670
			public virtual ProxyAddress RemoteRoutingAddress
			{
				set
				{
					base.PowerSharpParameters["RemoteRoutingAddress"] = value;
				}
			}

			// Token: 0x170085BE RID: 34238
			// (set) Token: 0x0600B4C3 RID: 46275 RVA: 0x00104483 File Offset: 0x00102683
			public virtual string OnPremisesOrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OnPremisesOrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170085BF RID: 34239
			// (set) Token: 0x0600B4C4 RID: 46276 RVA: 0x001044A1 File Offset: 0x001026A1
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x170085C0 RID: 34240
			// (set) Token: 0x0600B4C5 RID: 46277 RVA: 0x001044B9 File Offset: 0x001026B9
			public virtual SwitchParameter ACLableSyncedObjectEnabled
			{
				set
				{
					base.PowerSharpParameters["ACLableSyncedObjectEnabled"] = value;
				}
			}

			// Token: 0x170085C1 RID: 34241
			// (set) Token: 0x0600B4C6 RID: 46278 RVA: 0x001044D1 File Offset: 0x001026D1
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x170085C2 RID: 34242
			// (set) Token: 0x0600B4C7 RID: 46279 RVA: 0x001044E9 File Offset: 0x001026E9
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x170085C3 RID: 34243
			// (set) Token: 0x0600B4C8 RID: 46280 RVA: 0x001044FC File Offset: 0x001026FC
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x170085C4 RID: 34244
			// (set) Token: 0x0600B4C9 RID: 46281 RVA: 0x0010450F File Offset: 0x0010270F
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x170085C5 RID: 34245
			// (set) Token: 0x0600B4CA RID: 46282 RVA: 0x00104522 File Offset: 0x00102722
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x170085C6 RID: 34246
			// (set) Token: 0x0600B4CB RID: 46283 RVA: 0x00104535 File Offset: 0x00102735
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x170085C7 RID: 34247
			// (set) Token: 0x0600B4CC RID: 46284 RVA: 0x00104548 File Offset: 0x00102748
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x170085C8 RID: 34248
			// (set) Token: 0x0600B4CD RID: 46285 RVA: 0x0010455B File Offset: 0x0010275B
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x170085C9 RID: 34249
			// (set) Token: 0x0600B4CE RID: 46286 RVA: 0x00104573 File Offset: 0x00102773
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x170085CA RID: 34250
			// (set) Token: 0x0600B4CF RID: 46287 RVA: 0x00104586 File Offset: 0x00102786
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x170085CB RID: 34251
			// (set) Token: 0x0600B4D0 RID: 46288 RVA: 0x00104599 File Offset: 0x00102799
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170085CC RID: 34252
			// (set) Token: 0x0600B4D1 RID: 46289 RVA: 0x001045B7 File Offset: 0x001027B7
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x170085CD RID: 34253
			// (set) Token: 0x0600B4D2 RID: 46290 RVA: 0x001045CA File Offset: 0x001027CA
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x170085CE RID: 34254
			// (set) Token: 0x0600B4D3 RID: 46291 RVA: 0x001045E2 File Offset: 0x001027E2
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x170085CF RID: 34255
			// (set) Token: 0x0600B4D4 RID: 46292 RVA: 0x001045FA File Offset: 0x001027FA
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x170085D0 RID: 34256
			// (set) Token: 0x0600B4D5 RID: 46293 RVA: 0x00104612 File Offset: 0x00102812
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x170085D1 RID: 34257
			// (set) Token: 0x0600B4D6 RID: 46294 RVA: 0x00104625 File Offset: 0x00102825
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x170085D2 RID: 34258
			// (set) Token: 0x0600B4D7 RID: 46295 RVA: 0x0010463D File Offset: 0x0010283D
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170085D3 RID: 34259
			// (set) Token: 0x0600B4D8 RID: 46296 RVA: 0x00104650 File Offset: 0x00102850
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170085D4 RID: 34260
			// (set) Token: 0x0600B4D9 RID: 46297 RVA: 0x00104663 File Offset: 0x00102863
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170085D5 RID: 34261
			// (set) Token: 0x0600B4DA RID: 46298 RVA: 0x00104676 File Offset: 0x00102876
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170085D6 RID: 34262
			// (set) Token: 0x0600B4DB RID: 46299 RVA: 0x0010468E File Offset: 0x0010288E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170085D7 RID: 34263
			// (set) Token: 0x0600B4DC RID: 46300 RVA: 0x001046A6 File Offset: 0x001028A6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170085D8 RID: 34264
			// (set) Token: 0x0600B4DD RID: 46301 RVA: 0x001046BE File Offset: 0x001028BE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170085D9 RID: 34265
			// (set) Token: 0x0600B4DE RID: 46302 RVA: 0x001046D6 File Offset: 0x001028D6
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D5D RID: 3421
		public class RoomParameters : ParametersBase
		{
			// Token: 0x170085DA RID: 34266
			// (set) Token: 0x0600B4E0 RID: 46304 RVA: 0x001046F6 File Offset: 0x001028F6
			public virtual SwitchParameter Room
			{
				set
				{
					base.PowerSharpParameters["Room"] = value;
				}
			}

			// Token: 0x170085DB RID: 34267
			// (set) Token: 0x0600B4E1 RID: 46305 RVA: 0x0010470E File Offset: 0x0010290E
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x170085DC RID: 34268
			// (set) Token: 0x0600B4E2 RID: 46306 RVA: 0x00104721 File Offset: 0x00102921
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x170085DD RID: 34269
			// (set) Token: 0x0600B4E3 RID: 46307 RVA: 0x00104734 File Offset: 0x00102934
			public virtual ProxyAddress RemoteRoutingAddress
			{
				set
				{
					base.PowerSharpParameters["RemoteRoutingAddress"] = value;
				}
			}

			// Token: 0x170085DE RID: 34270
			// (set) Token: 0x0600B4E4 RID: 46308 RVA: 0x00104747 File Offset: 0x00102947
			public virtual string OnPremisesOrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OnPremisesOrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170085DF RID: 34271
			// (set) Token: 0x0600B4E5 RID: 46309 RVA: 0x00104765 File Offset: 0x00102965
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x170085E0 RID: 34272
			// (set) Token: 0x0600B4E6 RID: 46310 RVA: 0x0010477D File Offset: 0x0010297D
			public virtual SwitchParameter ACLableSyncedObjectEnabled
			{
				set
				{
					base.PowerSharpParameters["ACLableSyncedObjectEnabled"] = value;
				}
			}

			// Token: 0x170085E1 RID: 34273
			// (set) Token: 0x0600B4E7 RID: 46311 RVA: 0x00104795 File Offset: 0x00102995
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x170085E2 RID: 34274
			// (set) Token: 0x0600B4E8 RID: 46312 RVA: 0x001047AD File Offset: 0x001029AD
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x170085E3 RID: 34275
			// (set) Token: 0x0600B4E9 RID: 46313 RVA: 0x001047C0 File Offset: 0x001029C0
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x170085E4 RID: 34276
			// (set) Token: 0x0600B4EA RID: 46314 RVA: 0x001047D3 File Offset: 0x001029D3
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x170085E5 RID: 34277
			// (set) Token: 0x0600B4EB RID: 46315 RVA: 0x001047E6 File Offset: 0x001029E6
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x170085E6 RID: 34278
			// (set) Token: 0x0600B4EC RID: 46316 RVA: 0x001047F9 File Offset: 0x001029F9
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x170085E7 RID: 34279
			// (set) Token: 0x0600B4ED RID: 46317 RVA: 0x0010480C File Offset: 0x00102A0C
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x170085E8 RID: 34280
			// (set) Token: 0x0600B4EE RID: 46318 RVA: 0x0010481F File Offset: 0x00102A1F
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x170085E9 RID: 34281
			// (set) Token: 0x0600B4EF RID: 46319 RVA: 0x00104837 File Offset: 0x00102A37
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x170085EA RID: 34282
			// (set) Token: 0x0600B4F0 RID: 46320 RVA: 0x0010484A File Offset: 0x00102A4A
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x170085EB RID: 34283
			// (set) Token: 0x0600B4F1 RID: 46321 RVA: 0x0010485D File Offset: 0x00102A5D
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170085EC RID: 34284
			// (set) Token: 0x0600B4F2 RID: 46322 RVA: 0x0010487B File Offset: 0x00102A7B
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x170085ED RID: 34285
			// (set) Token: 0x0600B4F3 RID: 46323 RVA: 0x0010488E File Offset: 0x00102A8E
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x170085EE RID: 34286
			// (set) Token: 0x0600B4F4 RID: 46324 RVA: 0x001048A6 File Offset: 0x00102AA6
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x170085EF RID: 34287
			// (set) Token: 0x0600B4F5 RID: 46325 RVA: 0x001048BE File Offset: 0x00102ABE
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x170085F0 RID: 34288
			// (set) Token: 0x0600B4F6 RID: 46326 RVA: 0x001048D6 File Offset: 0x00102AD6
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x170085F1 RID: 34289
			// (set) Token: 0x0600B4F7 RID: 46327 RVA: 0x001048E9 File Offset: 0x00102AE9
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x170085F2 RID: 34290
			// (set) Token: 0x0600B4F8 RID: 46328 RVA: 0x00104901 File Offset: 0x00102B01
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170085F3 RID: 34291
			// (set) Token: 0x0600B4F9 RID: 46329 RVA: 0x00104914 File Offset: 0x00102B14
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170085F4 RID: 34292
			// (set) Token: 0x0600B4FA RID: 46330 RVA: 0x00104927 File Offset: 0x00102B27
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170085F5 RID: 34293
			// (set) Token: 0x0600B4FB RID: 46331 RVA: 0x0010493A File Offset: 0x00102B3A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170085F6 RID: 34294
			// (set) Token: 0x0600B4FC RID: 46332 RVA: 0x00104952 File Offset: 0x00102B52
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170085F7 RID: 34295
			// (set) Token: 0x0600B4FD RID: 46333 RVA: 0x0010496A File Offset: 0x00102B6A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170085F8 RID: 34296
			// (set) Token: 0x0600B4FE RID: 46334 RVA: 0x00104982 File Offset: 0x00102B82
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170085F9 RID: 34297
			// (set) Token: 0x0600B4FF RID: 46335 RVA: 0x0010499A File Offset: 0x00102B9A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D5E RID: 3422
		public class EquipmentParameters : ParametersBase
		{
			// Token: 0x170085FA RID: 34298
			// (set) Token: 0x0600B501 RID: 46337 RVA: 0x001049BA File Offset: 0x00102BBA
			public virtual SwitchParameter Equipment
			{
				set
				{
					base.PowerSharpParameters["Equipment"] = value;
				}
			}

			// Token: 0x170085FB RID: 34299
			// (set) Token: 0x0600B502 RID: 46338 RVA: 0x001049D2 File Offset: 0x00102BD2
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x170085FC RID: 34300
			// (set) Token: 0x0600B503 RID: 46339 RVA: 0x001049E5 File Offset: 0x00102BE5
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x170085FD RID: 34301
			// (set) Token: 0x0600B504 RID: 46340 RVA: 0x001049F8 File Offset: 0x00102BF8
			public virtual ProxyAddress RemoteRoutingAddress
			{
				set
				{
					base.PowerSharpParameters["RemoteRoutingAddress"] = value;
				}
			}

			// Token: 0x170085FE RID: 34302
			// (set) Token: 0x0600B505 RID: 46341 RVA: 0x00104A0B File Offset: 0x00102C0B
			public virtual string OnPremisesOrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OnPremisesOrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170085FF RID: 34303
			// (set) Token: 0x0600B506 RID: 46342 RVA: 0x00104A29 File Offset: 0x00102C29
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17008600 RID: 34304
			// (set) Token: 0x0600B507 RID: 46343 RVA: 0x00104A41 File Offset: 0x00102C41
			public virtual SwitchParameter ACLableSyncedObjectEnabled
			{
				set
				{
					base.PowerSharpParameters["ACLableSyncedObjectEnabled"] = value;
				}
			}

			// Token: 0x17008601 RID: 34305
			// (set) Token: 0x0600B508 RID: 46344 RVA: 0x00104A59 File Offset: 0x00102C59
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17008602 RID: 34306
			// (set) Token: 0x0600B509 RID: 46345 RVA: 0x00104A71 File Offset: 0x00102C71
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17008603 RID: 34307
			// (set) Token: 0x0600B50A RID: 46346 RVA: 0x00104A84 File Offset: 0x00102C84
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17008604 RID: 34308
			// (set) Token: 0x0600B50B RID: 46347 RVA: 0x00104A97 File Offset: 0x00102C97
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17008605 RID: 34309
			// (set) Token: 0x0600B50C RID: 46348 RVA: 0x00104AAA File Offset: 0x00102CAA
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17008606 RID: 34310
			// (set) Token: 0x0600B50D RID: 46349 RVA: 0x00104ABD File Offset: 0x00102CBD
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17008607 RID: 34311
			// (set) Token: 0x0600B50E RID: 46350 RVA: 0x00104AD0 File Offset: 0x00102CD0
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17008608 RID: 34312
			// (set) Token: 0x0600B50F RID: 46351 RVA: 0x00104AE3 File Offset: 0x00102CE3
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17008609 RID: 34313
			// (set) Token: 0x0600B510 RID: 46352 RVA: 0x00104AFB File Offset: 0x00102CFB
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x1700860A RID: 34314
			// (set) Token: 0x0600B511 RID: 46353 RVA: 0x00104B0E File Offset: 0x00102D0E
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x1700860B RID: 34315
			// (set) Token: 0x0600B512 RID: 46354 RVA: 0x00104B21 File Offset: 0x00102D21
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700860C RID: 34316
			// (set) Token: 0x0600B513 RID: 46355 RVA: 0x00104B3F File Offset: 0x00102D3F
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x1700860D RID: 34317
			// (set) Token: 0x0600B514 RID: 46356 RVA: 0x00104B52 File Offset: 0x00102D52
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x1700860E RID: 34318
			// (set) Token: 0x0600B515 RID: 46357 RVA: 0x00104B6A File Offset: 0x00102D6A
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x1700860F RID: 34319
			// (set) Token: 0x0600B516 RID: 46358 RVA: 0x00104B82 File Offset: 0x00102D82
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17008610 RID: 34320
			// (set) Token: 0x0600B517 RID: 46359 RVA: 0x00104B9A File Offset: 0x00102D9A
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17008611 RID: 34321
			// (set) Token: 0x0600B518 RID: 46360 RVA: 0x00104BAD File Offset: 0x00102DAD
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008612 RID: 34322
			// (set) Token: 0x0600B519 RID: 46361 RVA: 0x00104BC5 File Offset: 0x00102DC5
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17008613 RID: 34323
			// (set) Token: 0x0600B51A RID: 46362 RVA: 0x00104BD8 File Offset: 0x00102DD8
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17008614 RID: 34324
			// (set) Token: 0x0600B51B RID: 46363 RVA: 0x00104BEB File Offset: 0x00102DEB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008615 RID: 34325
			// (set) Token: 0x0600B51C RID: 46364 RVA: 0x00104BFE File Offset: 0x00102DFE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008616 RID: 34326
			// (set) Token: 0x0600B51D RID: 46365 RVA: 0x00104C16 File Offset: 0x00102E16
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008617 RID: 34327
			// (set) Token: 0x0600B51E RID: 46366 RVA: 0x00104C2E File Offset: 0x00102E2E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008618 RID: 34328
			// (set) Token: 0x0600B51F RID: 46367 RVA: 0x00104C46 File Offset: 0x00102E46
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008619 RID: 34329
			// (set) Token: 0x0600B520 RID: 46368 RVA: 0x00104C5E File Offset: 0x00102E5E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D5F RID: 3423
		public class SharedParameters : ParametersBase
		{
			// Token: 0x1700861A RID: 34330
			// (set) Token: 0x0600B522 RID: 46370 RVA: 0x00104C7E File Offset: 0x00102E7E
			public virtual SwitchParameter Shared
			{
				set
				{
					base.PowerSharpParameters["Shared"] = value;
				}
			}

			// Token: 0x1700861B RID: 34331
			// (set) Token: 0x0600B523 RID: 46371 RVA: 0x00104C96 File Offset: 0x00102E96
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x1700861C RID: 34332
			// (set) Token: 0x0600B524 RID: 46372 RVA: 0x00104CA9 File Offset: 0x00102EA9
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x1700861D RID: 34333
			// (set) Token: 0x0600B525 RID: 46373 RVA: 0x00104CBC File Offset: 0x00102EBC
			public virtual ProxyAddress RemoteRoutingAddress
			{
				set
				{
					base.PowerSharpParameters["RemoteRoutingAddress"] = value;
				}
			}

			// Token: 0x1700861E RID: 34334
			// (set) Token: 0x0600B526 RID: 46374 RVA: 0x00104CCF File Offset: 0x00102ECF
			public virtual string OnPremisesOrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OnPremisesOrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700861F RID: 34335
			// (set) Token: 0x0600B527 RID: 46375 RVA: 0x00104CED File Offset: 0x00102EED
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17008620 RID: 34336
			// (set) Token: 0x0600B528 RID: 46376 RVA: 0x00104D05 File Offset: 0x00102F05
			public virtual SwitchParameter ACLableSyncedObjectEnabled
			{
				set
				{
					base.PowerSharpParameters["ACLableSyncedObjectEnabled"] = value;
				}
			}

			// Token: 0x17008621 RID: 34337
			// (set) Token: 0x0600B529 RID: 46377 RVA: 0x00104D1D File Offset: 0x00102F1D
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17008622 RID: 34338
			// (set) Token: 0x0600B52A RID: 46378 RVA: 0x00104D35 File Offset: 0x00102F35
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17008623 RID: 34339
			// (set) Token: 0x0600B52B RID: 46379 RVA: 0x00104D48 File Offset: 0x00102F48
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17008624 RID: 34340
			// (set) Token: 0x0600B52C RID: 46380 RVA: 0x00104D5B File Offset: 0x00102F5B
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17008625 RID: 34341
			// (set) Token: 0x0600B52D RID: 46381 RVA: 0x00104D6E File Offset: 0x00102F6E
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17008626 RID: 34342
			// (set) Token: 0x0600B52E RID: 46382 RVA: 0x00104D81 File Offset: 0x00102F81
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17008627 RID: 34343
			// (set) Token: 0x0600B52F RID: 46383 RVA: 0x00104D94 File Offset: 0x00102F94
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17008628 RID: 34344
			// (set) Token: 0x0600B530 RID: 46384 RVA: 0x00104DA7 File Offset: 0x00102FA7
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17008629 RID: 34345
			// (set) Token: 0x0600B531 RID: 46385 RVA: 0x00104DBF File Offset: 0x00102FBF
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x1700862A RID: 34346
			// (set) Token: 0x0600B532 RID: 46386 RVA: 0x00104DD2 File Offset: 0x00102FD2
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x1700862B RID: 34347
			// (set) Token: 0x0600B533 RID: 46387 RVA: 0x00104DE5 File Offset: 0x00102FE5
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700862C RID: 34348
			// (set) Token: 0x0600B534 RID: 46388 RVA: 0x00104E03 File Offset: 0x00103003
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x1700862D RID: 34349
			// (set) Token: 0x0600B535 RID: 46389 RVA: 0x00104E16 File Offset: 0x00103016
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x1700862E RID: 34350
			// (set) Token: 0x0600B536 RID: 46390 RVA: 0x00104E2E File Offset: 0x0010302E
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x1700862F RID: 34351
			// (set) Token: 0x0600B537 RID: 46391 RVA: 0x00104E46 File Offset: 0x00103046
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17008630 RID: 34352
			// (set) Token: 0x0600B538 RID: 46392 RVA: 0x00104E5E File Offset: 0x0010305E
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17008631 RID: 34353
			// (set) Token: 0x0600B539 RID: 46393 RVA: 0x00104E71 File Offset: 0x00103071
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008632 RID: 34354
			// (set) Token: 0x0600B53A RID: 46394 RVA: 0x00104E89 File Offset: 0x00103089
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17008633 RID: 34355
			// (set) Token: 0x0600B53B RID: 46395 RVA: 0x00104E9C File Offset: 0x0010309C
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17008634 RID: 34356
			// (set) Token: 0x0600B53C RID: 46396 RVA: 0x00104EAF File Offset: 0x001030AF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008635 RID: 34357
			// (set) Token: 0x0600B53D RID: 46397 RVA: 0x00104EC2 File Offset: 0x001030C2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008636 RID: 34358
			// (set) Token: 0x0600B53E RID: 46398 RVA: 0x00104EDA File Offset: 0x001030DA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008637 RID: 34359
			// (set) Token: 0x0600B53F RID: 46399 RVA: 0x00104EF2 File Offset: 0x001030F2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008638 RID: 34360
			// (set) Token: 0x0600B540 RID: 46400 RVA: 0x00104F0A File Offset: 0x0010310A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008639 RID: 34361
			// (set) Token: 0x0600B541 RID: 46401 RVA: 0x00104F22 File Offset: 0x00103122
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D60 RID: 3424
		public class DisabledUserParameters : ParametersBase
		{
			// Token: 0x1700863A RID: 34362
			// (set) Token: 0x0600B543 RID: 46403 RVA: 0x00104F42 File Offset: 0x00103142
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x1700863B RID: 34363
			// (set) Token: 0x0600B544 RID: 46404 RVA: 0x00104F5A File Offset: 0x0010315A
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x1700863C RID: 34364
			// (set) Token: 0x0600B545 RID: 46405 RVA: 0x00104F6D File Offset: 0x0010316D
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x1700863D RID: 34365
			// (set) Token: 0x0600B546 RID: 46406 RVA: 0x00104F80 File Offset: 0x00103180
			public virtual ProxyAddress RemoteRoutingAddress
			{
				set
				{
					base.PowerSharpParameters["RemoteRoutingAddress"] = value;
				}
			}

			// Token: 0x1700863E RID: 34366
			// (set) Token: 0x0600B547 RID: 46407 RVA: 0x00104F93 File Offset: 0x00103193
			public virtual string OnPremisesOrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OnPremisesOrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700863F RID: 34367
			// (set) Token: 0x0600B548 RID: 46408 RVA: 0x00104FB1 File Offset: 0x001031B1
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17008640 RID: 34368
			// (set) Token: 0x0600B549 RID: 46409 RVA: 0x00104FC9 File Offset: 0x001031C9
			public virtual SwitchParameter ACLableSyncedObjectEnabled
			{
				set
				{
					base.PowerSharpParameters["ACLableSyncedObjectEnabled"] = value;
				}
			}

			// Token: 0x17008641 RID: 34369
			// (set) Token: 0x0600B54A RID: 46410 RVA: 0x00104FE1 File Offset: 0x001031E1
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17008642 RID: 34370
			// (set) Token: 0x0600B54B RID: 46411 RVA: 0x00104FF9 File Offset: 0x001031F9
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17008643 RID: 34371
			// (set) Token: 0x0600B54C RID: 46412 RVA: 0x0010500C File Offset: 0x0010320C
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17008644 RID: 34372
			// (set) Token: 0x0600B54D RID: 46413 RVA: 0x0010501F File Offset: 0x0010321F
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17008645 RID: 34373
			// (set) Token: 0x0600B54E RID: 46414 RVA: 0x00105032 File Offset: 0x00103232
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17008646 RID: 34374
			// (set) Token: 0x0600B54F RID: 46415 RVA: 0x00105045 File Offset: 0x00103245
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17008647 RID: 34375
			// (set) Token: 0x0600B550 RID: 46416 RVA: 0x00105058 File Offset: 0x00103258
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17008648 RID: 34376
			// (set) Token: 0x0600B551 RID: 46417 RVA: 0x0010506B File Offset: 0x0010326B
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17008649 RID: 34377
			// (set) Token: 0x0600B552 RID: 46418 RVA: 0x00105083 File Offset: 0x00103283
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x1700864A RID: 34378
			// (set) Token: 0x0600B553 RID: 46419 RVA: 0x00105096 File Offset: 0x00103296
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x1700864B RID: 34379
			// (set) Token: 0x0600B554 RID: 46420 RVA: 0x001050A9 File Offset: 0x001032A9
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700864C RID: 34380
			// (set) Token: 0x0600B555 RID: 46421 RVA: 0x001050C7 File Offset: 0x001032C7
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x1700864D RID: 34381
			// (set) Token: 0x0600B556 RID: 46422 RVA: 0x001050DA File Offset: 0x001032DA
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x1700864E RID: 34382
			// (set) Token: 0x0600B557 RID: 46423 RVA: 0x001050F2 File Offset: 0x001032F2
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x1700864F RID: 34383
			// (set) Token: 0x0600B558 RID: 46424 RVA: 0x0010510A File Offset: 0x0010330A
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17008650 RID: 34384
			// (set) Token: 0x0600B559 RID: 46425 RVA: 0x00105122 File Offset: 0x00103322
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17008651 RID: 34385
			// (set) Token: 0x0600B55A RID: 46426 RVA: 0x00105135 File Offset: 0x00103335
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008652 RID: 34386
			// (set) Token: 0x0600B55B RID: 46427 RVA: 0x0010514D File Offset: 0x0010334D
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17008653 RID: 34387
			// (set) Token: 0x0600B55C RID: 46428 RVA: 0x00105160 File Offset: 0x00103360
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17008654 RID: 34388
			// (set) Token: 0x0600B55D RID: 46429 RVA: 0x00105173 File Offset: 0x00103373
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008655 RID: 34389
			// (set) Token: 0x0600B55E RID: 46430 RVA: 0x00105186 File Offset: 0x00103386
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008656 RID: 34390
			// (set) Token: 0x0600B55F RID: 46431 RVA: 0x0010519E File Offset: 0x0010339E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008657 RID: 34391
			// (set) Token: 0x0600B560 RID: 46432 RVA: 0x001051B6 File Offset: 0x001033B6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008658 RID: 34392
			// (set) Token: 0x0600B561 RID: 46433 RVA: 0x001051CE File Offset: 0x001033CE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008659 RID: 34393
			// (set) Token: 0x0600B562 RID: 46434 RVA: 0x001051E6 File Offset: 0x001033E6
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D61 RID: 3425
		public class EnabledUserParameters : ParametersBase
		{
			// Token: 0x1700865A RID: 34394
			// (set) Token: 0x0600B564 RID: 46436 RVA: 0x00105206 File Offset: 0x00103406
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x1700865B RID: 34395
			// (set) Token: 0x0600B565 RID: 46437 RVA: 0x00105219 File Offset: 0x00103419
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x1700865C RID: 34396
			// (set) Token: 0x0600B566 RID: 46438 RVA: 0x0010522C File Offset: 0x0010342C
			public virtual ProxyAddress RemoteRoutingAddress
			{
				set
				{
					base.PowerSharpParameters["RemoteRoutingAddress"] = value;
				}
			}

			// Token: 0x1700865D RID: 34397
			// (set) Token: 0x0600B567 RID: 46439 RVA: 0x0010523F File Offset: 0x0010343F
			public virtual string OnPremisesOrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OnPremisesOrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700865E RID: 34398
			// (set) Token: 0x0600B568 RID: 46440 RVA: 0x0010525D File Offset: 0x0010345D
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x1700865F RID: 34399
			// (set) Token: 0x0600B569 RID: 46441 RVA: 0x00105275 File Offset: 0x00103475
			public virtual SwitchParameter ACLableSyncedObjectEnabled
			{
				set
				{
					base.PowerSharpParameters["ACLableSyncedObjectEnabled"] = value;
				}
			}

			// Token: 0x17008660 RID: 34400
			// (set) Token: 0x0600B56A RID: 46442 RVA: 0x0010528D File Offset: 0x0010348D
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17008661 RID: 34401
			// (set) Token: 0x0600B56B RID: 46443 RVA: 0x001052A5 File Offset: 0x001034A5
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17008662 RID: 34402
			// (set) Token: 0x0600B56C RID: 46444 RVA: 0x001052B8 File Offset: 0x001034B8
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17008663 RID: 34403
			// (set) Token: 0x0600B56D RID: 46445 RVA: 0x001052CB File Offset: 0x001034CB
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17008664 RID: 34404
			// (set) Token: 0x0600B56E RID: 46446 RVA: 0x001052DE File Offset: 0x001034DE
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17008665 RID: 34405
			// (set) Token: 0x0600B56F RID: 46447 RVA: 0x001052F1 File Offset: 0x001034F1
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17008666 RID: 34406
			// (set) Token: 0x0600B570 RID: 46448 RVA: 0x00105304 File Offset: 0x00103504
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17008667 RID: 34407
			// (set) Token: 0x0600B571 RID: 46449 RVA: 0x00105317 File Offset: 0x00103517
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17008668 RID: 34408
			// (set) Token: 0x0600B572 RID: 46450 RVA: 0x0010532F File Offset: 0x0010352F
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17008669 RID: 34409
			// (set) Token: 0x0600B573 RID: 46451 RVA: 0x00105342 File Offset: 0x00103542
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x1700866A RID: 34410
			// (set) Token: 0x0600B574 RID: 46452 RVA: 0x00105355 File Offset: 0x00103555
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700866B RID: 34411
			// (set) Token: 0x0600B575 RID: 46453 RVA: 0x00105373 File Offset: 0x00103573
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x1700866C RID: 34412
			// (set) Token: 0x0600B576 RID: 46454 RVA: 0x00105386 File Offset: 0x00103586
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x1700866D RID: 34413
			// (set) Token: 0x0600B577 RID: 46455 RVA: 0x0010539E File Offset: 0x0010359E
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x1700866E RID: 34414
			// (set) Token: 0x0600B578 RID: 46456 RVA: 0x001053B6 File Offset: 0x001035B6
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x1700866F RID: 34415
			// (set) Token: 0x0600B579 RID: 46457 RVA: 0x001053CE File Offset: 0x001035CE
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17008670 RID: 34416
			// (set) Token: 0x0600B57A RID: 46458 RVA: 0x001053E1 File Offset: 0x001035E1
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008671 RID: 34417
			// (set) Token: 0x0600B57B RID: 46459 RVA: 0x001053F9 File Offset: 0x001035F9
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17008672 RID: 34418
			// (set) Token: 0x0600B57C RID: 46460 RVA: 0x0010540C File Offset: 0x0010360C
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17008673 RID: 34419
			// (set) Token: 0x0600B57D RID: 46461 RVA: 0x0010541F File Offset: 0x0010361F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008674 RID: 34420
			// (set) Token: 0x0600B57E RID: 46462 RVA: 0x00105432 File Offset: 0x00103632
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008675 RID: 34421
			// (set) Token: 0x0600B57F RID: 46463 RVA: 0x0010544A File Offset: 0x0010364A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008676 RID: 34422
			// (set) Token: 0x0600B580 RID: 46464 RVA: 0x00105462 File Offset: 0x00103662
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008677 RID: 34423
			// (set) Token: 0x0600B581 RID: 46465 RVA: 0x0010547A File Offset: 0x0010367A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008678 RID: 34424
			// (set) Token: 0x0600B582 RID: 46466 RVA: 0x00105492 File Offset: 0x00103692
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
