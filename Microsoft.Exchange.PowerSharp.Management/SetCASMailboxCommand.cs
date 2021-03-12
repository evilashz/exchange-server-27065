using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000BD6 RID: 3030
	public class SetCASMailboxCommand : SyntheticCommandWithPipelineInputNoOutput<CASMailbox>
	{
		// Token: 0x0600922C RID: 37420 RVA: 0x000D56DC File Offset: 0x000D38DC
		private SetCASMailboxCommand() : base("Set-CASMailbox")
		{
		}

		// Token: 0x0600922D RID: 37421 RVA: 0x000D56E9 File Offset: 0x000D38E9
		public SetCASMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600922E RID: 37422 RVA: 0x000D56F8 File Offset: 0x000D38F8
		public virtual SetCASMailboxCommand SetParameters(SetCASMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600922F RID: 37423 RVA: 0x000D5702 File Offset: 0x000D3902
		public virtual SetCASMailboxCommand SetParameters(SetCASMailboxCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000BD7 RID: 3031
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006639 RID: 26169
			// (set) Token: 0x06009230 RID: 37424 RVA: 0x000D570C File Offset: 0x000D390C
			public virtual SwitchParameter ResetAutoBlockedDevices
			{
				set
				{
					base.PowerSharpParameters["ResetAutoBlockedDevices"] = value;
				}
			}

			// Token: 0x1700663A RID: 26170
			// (set) Token: 0x06009231 RID: 37425 RVA: 0x000D5724 File Offset: 0x000D3924
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700663B RID: 26171
			// (set) Token: 0x06009232 RID: 37426 RVA: 0x000D5742 File Offset: 0x000D3942
			public virtual string OwaMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["OwaMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700663C RID: 26172
			// (set) Token: 0x06009233 RID: 37427 RVA: 0x000D5760 File Offset: 0x000D3960
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700663D RID: 26173
			// (set) Token: 0x06009234 RID: 37428 RVA: 0x000D5778 File Offset: 0x000D3978
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700663E RID: 26174
			// (set) Token: 0x06009235 RID: 37429 RVA: 0x000D578B File Offset: 0x000D398B
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x1700663F RID: 26175
			// (set) Token: 0x06009236 RID: 37430 RVA: 0x000D579E File Offset: 0x000D399E
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17006640 RID: 26176
			// (set) Token: 0x06009237 RID: 37431 RVA: 0x000D57B6 File Offset: 0x000D39B6
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17006641 RID: 26177
			// (set) Token: 0x06009238 RID: 37432 RVA: 0x000D57C9 File Offset: 0x000D39C9
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006642 RID: 26178
			// (set) Token: 0x06009239 RID: 37433 RVA: 0x000D57DC File Offset: 0x000D39DC
			public virtual MultiValuedProperty<string> ActiveSyncAllowedDeviceIDs
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncAllowedDeviceIDs"] = value;
				}
			}

			// Token: 0x17006643 RID: 26179
			// (set) Token: 0x0600923A RID: 37434 RVA: 0x000D57EF File Offset: 0x000D39EF
			public virtual MultiValuedProperty<string> ActiveSyncBlockedDeviceIDs
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncBlockedDeviceIDs"] = value;
				}
			}

			// Token: 0x17006644 RID: 26180
			// (set) Token: 0x0600923B RID: 37435 RVA: 0x000D5802 File Offset: 0x000D3A02
			public virtual bool ActiveSyncDebugLogging
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncDebugLogging"] = value;
				}
			}

			// Token: 0x17006645 RID: 26181
			// (set) Token: 0x0600923C RID: 37436 RVA: 0x000D581A File Offset: 0x000D3A1A
			public virtual bool ActiveSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncEnabled"] = value;
				}
			}

			// Token: 0x17006646 RID: 26182
			// (set) Token: 0x0600923D RID: 37437 RVA: 0x000D5832 File Offset: 0x000D3A32
			public virtual bool OWAEnabled
			{
				set
				{
					base.PowerSharpParameters["OWAEnabled"] = value;
				}
			}

			// Token: 0x17006647 RID: 26183
			// (set) Token: 0x0600923E RID: 37438 RVA: 0x000D584A File Offset: 0x000D3A4A
			public virtual bool OWAforDevicesEnabled
			{
				set
				{
					base.PowerSharpParameters["OWAforDevicesEnabled"] = value;
				}
			}

			// Token: 0x17006648 RID: 26184
			// (set) Token: 0x0600923F RID: 37439 RVA: 0x000D5862 File Offset: 0x000D3A62
			public virtual bool ECPEnabled
			{
				set
				{
					base.PowerSharpParameters["ECPEnabled"] = value;
				}
			}

			// Token: 0x17006649 RID: 26185
			// (set) Token: 0x06009240 RID: 37440 RVA: 0x000D587A File Offset: 0x000D3A7A
			public virtual bool PopEnabled
			{
				set
				{
					base.PowerSharpParameters["PopEnabled"] = value;
				}
			}

			// Token: 0x1700664A RID: 26186
			// (set) Token: 0x06009241 RID: 37441 RVA: 0x000D5892 File Offset: 0x000D3A92
			public virtual bool PopUseProtocolDefaults
			{
				set
				{
					base.PowerSharpParameters["PopUseProtocolDefaults"] = value;
				}
			}

			// Token: 0x1700664B RID: 26187
			// (set) Token: 0x06009242 RID: 37442 RVA: 0x000D58AA File Offset: 0x000D3AAA
			public virtual MimeTextFormat PopMessagesRetrievalMimeFormat
			{
				set
				{
					base.PowerSharpParameters["PopMessagesRetrievalMimeFormat"] = value;
				}
			}

			// Token: 0x1700664C RID: 26188
			// (set) Token: 0x06009243 RID: 37443 RVA: 0x000D58C2 File Offset: 0x000D3AC2
			public virtual bool PopEnableExactRFC822Size
			{
				set
				{
					base.PowerSharpParameters["PopEnableExactRFC822Size"] = value;
				}
			}

			// Token: 0x1700664D RID: 26189
			// (set) Token: 0x06009244 RID: 37444 RVA: 0x000D58DA File Offset: 0x000D3ADA
			public virtual bool PopSuppressReadReceipt
			{
				set
				{
					base.PowerSharpParameters["PopSuppressReadReceipt"] = value;
				}
			}

			// Token: 0x1700664E RID: 26190
			// (set) Token: 0x06009245 RID: 37445 RVA: 0x000D58F2 File Offset: 0x000D3AF2
			public virtual bool PopForceICalForCalendarRetrievalOption
			{
				set
				{
					base.PowerSharpParameters["PopForceICalForCalendarRetrievalOption"] = value;
				}
			}

			// Token: 0x1700664F RID: 26191
			// (set) Token: 0x06009246 RID: 37446 RVA: 0x000D590A File Offset: 0x000D3B0A
			public virtual bool ImapEnabled
			{
				set
				{
					base.PowerSharpParameters["ImapEnabled"] = value;
				}
			}

			// Token: 0x17006650 RID: 26192
			// (set) Token: 0x06009247 RID: 37447 RVA: 0x000D5922 File Offset: 0x000D3B22
			public virtual bool ImapUseProtocolDefaults
			{
				set
				{
					base.PowerSharpParameters["ImapUseProtocolDefaults"] = value;
				}
			}

			// Token: 0x17006651 RID: 26193
			// (set) Token: 0x06009248 RID: 37448 RVA: 0x000D593A File Offset: 0x000D3B3A
			public virtual MimeTextFormat ImapMessagesRetrievalMimeFormat
			{
				set
				{
					base.PowerSharpParameters["ImapMessagesRetrievalMimeFormat"] = value;
				}
			}

			// Token: 0x17006652 RID: 26194
			// (set) Token: 0x06009249 RID: 37449 RVA: 0x000D5952 File Offset: 0x000D3B52
			public virtual bool ImapEnableExactRFC822Size
			{
				set
				{
					base.PowerSharpParameters["ImapEnableExactRFC822Size"] = value;
				}
			}

			// Token: 0x17006653 RID: 26195
			// (set) Token: 0x0600924A RID: 37450 RVA: 0x000D596A File Offset: 0x000D3B6A
			public virtual bool ImapSuppressReadReceipt
			{
				set
				{
					base.PowerSharpParameters["ImapSuppressReadReceipt"] = value;
				}
			}

			// Token: 0x17006654 RID: 26196
			// (set) Token: 0x0600924B RID: 37451 RVA: 0x000D5982 File Offset: 0x000D3B82
			public virtual bool ImapForceICalForCalendarRetrievalOption
			{
				set
				{
					base.PowerSharpParameters["ImapForceICalForCalendarRetrievalOption"] = value;
				}
			}

			// Token: 0x17006655 RID: 26197
			// (set) Token: 0x0600924C RID: 37452 RVA: 0x000D599A File Offset: 0x000D3B9A
			public virtual bool MAPIEnabled
			{
				set
				{
					base.PowerSharpParameters["MAPIEnabled"] = value;
				}
			}

			// Token: 0x17006656 RID: 26198
			// (set) Token: 0x0600924D RID: 37453 RVA: 0x000D59B2 File Offset: 0x000D3BB2
			public virtual bool? MapiHttpEnabled
			{
				set
				{
					base.PowerSharpParameters["MapiHttpEnabled"] = value;
				}
			}

			// Token: 0x17006657 RID: 26199
			// (set) Token: 0x0600924E RID: 37454 RVA: 0x000D59CA File Offset: 0x000D3BCA
			public virtual bool MAPIBlockOutlookNonCachedMode
			{
				set
				{
					base.PowerSharpParameters["MAPIBlockOutlookNonCachedMode"] = value;
				}
			}

			// Token: 0x17006658 RID: 26200
			// (set) Token: 0x0600924F RID: 37455 RVA: 0x000D59E2 File Offset: 0x000D3BE2
			public virtual string MAPIBlockOutlookVersions
			{
				set
				{
					base.PowerSharpParameters["MAPIBlockOutlookVersions"] = value;
				}
			}

			// Token: 0x17006659 RID: 26201
			// (set) Token: 0x06009250 RID: 37456 RVA: 0x000D59F5 File Offset: 0x000D3BF5
			public virtual bool MAPIBlockOutlookRpcHttp
			{
				set
				{
					base.PowerSharpParameters["MAPIBlockOutlookRpcHttp"] = value;
				}
			}

			// Token: 0x1700665A RID: 26202
			// (set) Token: 0x06009251 RID: 37457 RVA: 0x000D5A0D File Offset: 0x000D3C0D
			public virtual bool MAPIBlockOutlookExternalConnectivity
			{
				set
				{
					base.PowerSharpParameters["MAPIBlockOutlookExternalConnectivity"] = value;
				}
			}

			// Token: 0x1700665B RID: 26203
			// (set) Token: 0x06009252 RID: 37458 RVA: 0x000D5A25 File Offset: 0x000D3C25
			public virtual bool? EwsEnabled
			{
				set
				{
					base.PowerSharpParameters["EwsEnabled"] = value;
				}
			}

			// Token: 0x1700665C RID: 26204
			// (set) Token: 0x06009253 RID: 37459 RVA: 0x000D5A3D File Offset: 0x000D3C3D
			public virtual bool? EwsAllowOutlook
			{
				set
				{
					base.PowerSharpParameters["EwsAllowOutlook"] = value;
				}
			}

			// Token: 0x1700665D RID: 26205
			// (set) Token: 0x06009254 RID: 37460 RVA: 0x000D5A55 File Offset: 0x000D3C55
			public virtual bool? EwsAllowMacOutlook
			{
				set
				{
					base.PowerSharpParameters["EwsAllowMacOutlook"] = value;
				}
			}

			// Token: 0x1700665E RID: 26206
			// (set) Token: 0x06009255 RID: 37461 RVA: 0x000D5A6D File Offset: 0x000D3C6D
			public virtual bool? EwsAllowEntourage
			{
				set
				{
					base.PowerSharpParameters["EwsAllowEntourage"] = value;
				}
			}

			// Token: 0x1700665F RID: 26207
			// (set) Token: 0x06009256 RID: 37462 RVA: 0x000D5A85 File Offset: 0x000D3C85
			public virtual EwsApplicationAccessPolicy? EwsApplicationAccessPolicy
			{
				set
				{
					base.PowerSharpParameters["EwsApplicationAccessPolicy"] = value;
				}
			}

			// Token: 0x17006660 RID: 26208
			// (set) Token: 0x06009257 RID: 37463 RVA: 0x000D5A9D File Offset: 0x000D3C9D
			public virtual MultiValuedProperty<string> EwsAllowList
			{
				set
				{
					base.PowerSharpParameters["EwsAllowList"] = value;
				}
			}

			// Token: 0x17006661 RID: 26209
			// (set) Token: 0x06009258 RID: 37464 RVA: 0x000D5AB0 File Offset: 0x000D3CB0
			public virtual MultiValuedProperty<string> EwsBlockList
			{
				set
				{
					base.PowerSharpParameters["EwsBlockList"] = value;
				}
			}

			// Token: 0x17006662 RID: 26210
			// (set) Token: 0x06009259 RID: 37465 RVA: 0x000D5AC3 File Offset: 0x000D3CC3
			public virtual bool ShowGalAsDefaultView
			{
				set
				{
					base.PowerSharpParameters["ShowGalAsDefaultView"] = value;
				}
			}

			// Token: 0x17006663 RID: 26211
			// (set) Token: 0x0600925A RID: 37466 RVA: 0x000D5ADB File Offset: 0x000D3CDB
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006664 RID: 26212
			// (set) Token: 0x0600925B RID: 37467 RVA: 0x000D5AEE File Offset: 0x000D3CEE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006665 RID: 26213
			// (set) Token: 0x0600925C RID: 37468 RVA: 0x000D5B06 File Offset: 0x000D3D06
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006666 RID: 26214
			// (set) Token: 0x0600925D RID: 37469 RVA: 0x000D5B1E File Offset: 0x000D3D1E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006667 RID: 26215
			// (set) Token: 0x0600925E RID: 37470 RVA: 0x000D5B36 File Offset: 0x000D3D36
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006668 RID: 26216
			// (set) Token: 0x0600925F RID: 37471 RVA: 0x000D5B4E File Offset: 0x000D3D4E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000BD8 RID: 3032
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006669 RID: 26217
			// (set) Token: 0x06009261 RID: 37473 RVA: 0x000D5B6E File Offset: 0x000D3D6E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700666A RID: 26218
			// (set) Token: 0x06009262 RID: 37474 RVA: 0x000D5B8C File Offset: 0x000D3D8C
			public virtual SwitchParameter ResetAutoBlockedDevices
			{
				set
				{
					base.PowerSharpParameters["ResetAutoBlockedDevices"] = value;
				}
			}

			// Token: 0x1700666B RID: 26219
			// (set) Token: 0x06009263 RID: 37475 RVA: 0x000D5BA4 File Offset: 0x000D3DA4
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700666C RID: 26220
			// (set) Token: 0x06009264 RID: 37476 RVA: 0x000D5BC2 File Offset: 0x000D3DC2
			public virtual string OwaMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["OwaMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700666D RID: 26221
			// (set) Token: 0x06009265 RID: 37477 RVA: 0x000D5BE0 File Offset: 0x000D3DE0
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700666E RID: 26222
			// (set) Token: 0x06009266 RID: 37478 RVA: 0x000D5BF8 File Offset: 0x000D3DF8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700666F RID: 26223
			// (set) Token: 0x06009267 RID: 37479 RVA: 0x000D5C0B File Offset: 0x000D3E0B
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17006670 RID: 26224
			// (set) Token: 0x06009268 RID: 37480 RVA: 0x000D5C1E File Offset: 0x000D3E1E
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17006671 RID: 26225
			// (set) Token: 0x06009269 RID: 37481 RVA: 0x000D5C36 File Offset: 0x000D3E36
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17006672 RID: 26226
			// (set) Token: 0x0600926A RID: 37482 RVA: 0x000D5C49 File Offset: 0x000D3E49
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17006673 RID: 26227
			// (set) Token: 0x0600926B RID: 37483 RVA: 0x000D5C5C File Offset: 0x000D3E5C
			public virtual MultiValuedProperty<string> ActiveSyncAllowedDeviceIDs
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncAllowedDeviceIDs"] = value;
				}
			}

			// Token: 0x17006674 RID: 26228
			// (set) Token: 0x0600926C RID: 37484 RVA: 0x000D5C6F File Offset: 0x000D3E6F
			public virtual MultiValuedProperty<string> ActiveSyncBlockedDeviceIDs
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncBlockedDeviceIDs"] = value;
				}
			}

			// Token: 0x17006675 RID: 26229
			// (set) Token: 0x0600926D RID: 37485 RVA: 0x000D5C82 File Offset: 0x000D3E82
			public virtual bool ActiveSyncDebugLogging
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncDebugLogging"] = value;
				}
			}

			// Token: 0x17006676 RID: 26230
			// (set) Token: 0x0600926E RID: 37486 RVA: 0x000D5C9A File Offset: 0x000D3E9A
			public virtual bool ActiveSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncEnabled"] = value;
				}
			}

			// Token: 0x17006677 RID: 26231
			// (set) Token: 0x0600926F RID: 37487 RVA: 0x000D5CB2 File Offset: 0x000D3EB2
			public virtual bool OWAEnabled
			{
				set
				{
					base.PowerSharpParameters["OWAEnabled"] = value;
				}
			}

			// Token: 0x17006678 RID: 26232
			// (set) Token: 0x06009270 RID: 37488 RVA: 0x000D5CCA File Offset: 0x000D3ECA
			public virtual bool OWAforDevicesEnabled
			{
				set
				{
					base.PowerSharpParameters["OWAforDevicesEnabled"] = value;
				}
			}

			// Token: 0x17006679 RID: 26233
			// (set) Token: 0x06009271 RID: 37489 RVA: 0x000D5CE2 File Offset: 0x000D3EE2
			public virtual bool ECPEnabled
			{
				set
				{
					base.PowerSharpParameters["ECPEnabled"] = value;
				}
			}

			// Token: 0x1700667A RID: 26234
			// (set) Token: 0x06009272 RID: 37490 RVA: 0x000D5CFA File Offset: 0x000D3EFA
			public virtual bool PopEnabled
			{
				set
				{
					base.PowerSharpParameters["PopEnabled"] = value;
				}
			}

			// Token: 0x1700667B RID: 26235
			// (set) Token: 0x06009273 RID: 37491 RVA: 0x000D5D12 File Offset: 0x000D3F12
			public virtual bool PopUseProtocolDefaults
			{
				set
				{
					base.PowerSharpParameters["PopUseProtocolDefaults"] = value;
				}
			}

			// Token: 0x1700667C RID: 26236
			// (set) Token: 0x06009274 RID: 37492 RVA: 0x000D5D2A File Offset: 0x000D3F2A
			public virtual MimeTextFormat PopMessagesRetrievalMimeFormat
			{
				set
				{
					base.PowerSharpParameters["PopMessagesRetrievalMimeFormat"] = value;
				}
			}

			// Token: 0x1700667D RID: 26237
			// (set) Token: 0x06009275 RID: 37493 RVA: 0x000D5D42 File Offset: 0x000D3F42
			public virtual bool PopEnableExactRFC822Size
			{
				set
				{
					base.PowerSharpParameters["PopEnableExactRFC822Size"] = value;
				}
			}

			// Token: 0x1700667E RID: 26238
			// (set) Token: 0x06009276 RID: 37494 RVA: 0x000D5D5A File Offset: 0x000D3F5A
			public virtual bool PopSuppressReadReceipt
			{
				set
				{
					base.PowerSharpParameters["PopSuppressReadReceipt"] = value;
				}
			}

			// Token: 0x1700667F RID: 26239
			// (set) Token: 0x06009277 RID: 37495 RVA: 0x000D5D72 File Offset: 0x000D3F72
			public virtual bool PopForceICalForCalendarRetrievalOption
			{
				set
				{
					base.PowerSharpParameters["PopForceICalForCalendarRetrievalOption"] = value;
				}
			}

			// Token: 0x17006680 RID: 26240
			// (set) Token: 0x06009278 RID: 37496 RVA: 0x000D5D8A File Offset: 0x000D3F8A
			public virtual bool ImapEnabled
			{
				set
				{
					base.PowerSharpParameters["ImapEnabled"] = value;
				}
			}

			// Token: 0x17006681 RID: 26241
			// (set) Token: 0x06009279 RID: 37497 RVA: 0x000D5DA2 File Offset: 0x000D3FA2
			public virtual bool ImapUseProtocolDefaults
			{
				set
				{
					base.PowerSharpParameters["ImapUseProtocolDefaults"] = value;
				}
			}

			// Token: 0x17006682 RID: 26242
			// (set) Token: 0x0600927A RID: 37498 RVA: 0x000D5DBA File Offset: 0x000D3FBA
			public virtual MimeTextFormat ImapMessagesRetrievalMimeFormat
			{
				set
				{
					base.PowerSharpParameters["ImapMessagesRetrievalMimeFormat"] = value;
				}
			}

			// Token: 0x17006683 RID: 26243
			// (set) Token: 0x0600927B RID: 37499 RVA: 0x000D5DD2 File Offset: 0x000D3FD2
			public virtual bool ImapEnableExactRFC822Size
			{
				set
				{
					base.PowerSharpParameters["ImapEnableExactRFC822Size"] = value;
				}
			}

			// Token: 0x17006684 RID: 26244
			// (set) Token: 0x0600927C RID: 37500 RVA: 0x000D5DEA File Offset: 0x000D3FEA
			public virtual bool ImapSuppressReadReceipt
			{
				set
				{
					base.PowerSharpParameters["ImapSuppressReadReceipt"] = value;
				}
			}

			// Token: 0x17006685 RID: 26245
			// (set) Token: 0x0600927D RID: 37501 RVA: 0x000D5E02 File Offset: 0x000D4002
			public virtual bool ImapForceICalForCalendarRetrievalOption
			{
				set
				{
					base.PowerSharpParameters["ImapForceICalForCalendarRetrievalOption"] = value;
				}
			}

			// Token: 0x17006686 RID: 26246
			// (set) Token: 0x0600927E RID: 37502 RVA: 0x000D5E1A File Offset: 0x000D401A
			public virtual bool MAPIEnabled
			{
				set
				{
					base.PowerSharpParameters["MAPIEnabled"] = value;
				}
			}

			// Token: 0x17006687 RID: 26247
			// (set) Token: 0x0600927F RID: 37503 RVA: 0x000D5E32 File Offset: 0x000D4032
			public virtual bool? MapiHttpEnabled
			{
				set
				{
					base.PowerSharpParameters["MapiHttpEnabled"] = value;
				}
			}

			// Token: 0x17006688 RID: 26248
			// (set) Token: 0x06009280 RID: 37504 RVA: 0x000D5E4A File Offset: 0x000D404A
			public virtual bool MAPIBlockOutlookNonCachedMode
			{
				set
				{
					base.PowerSharpParameters["MAPIBlockOutlookNonCachedMode"] = value;
				}
			}

			// Token: 0x17006689 RID: 26249
			// (set) Token: 0x06009281 RID: 37505 RVA: 0x000D5E62 File Offset: 0x000D4062
			public virtual string MAPIBlockOutlookVersions
			{
				set
				{
					base.PowerSharpParameters["MAPIBlockOutlookVersions"] = value;
				}
			}

			// Token: 0x1700668A RID: 26250
			// (set) Token: 0x06009282 RID: 37506 RVA: 0x000D5E75 File Offset: 0x000D4075
			public virtual bool MAPIBlockOutlookRpcHttp
			{
				set
				{
					base.PowerSharpParameters["MAPIBlockOutlookRpcHttp"] = value;
				}
			}

			// Token: 0x1700668B RID: 26251
			// (set) Token: 0x06009283 RID: 37507 RVA: 0x000D5E8D File Offset: 0x000D408D
			public virtual bool MAPIBlockOutlookExternalConnectivity
			{
				set
				{
					base.PowerSharpParameters["MAPIBlockOutlookExternalConnectivity"] = value;
				}
			}

			// Token: 0x1700668C RID: 26252
			// (set) Token: 0x06009284 RID: 37508 RVA: 0x000D5EA5 File Offset: 0x000D40A5
			public virtual bool? EwsEnabled
			{
				set
				{
					base.PowerSharpParameters["EwsEnabled"] = value;
				}
			}

			// Token: 0x1700668D RID: 26253
			// (set) Token: 0x06009285 RID: 37509 RVA: 0x000D5EBD File Offset: 0x000D40BD
			public virtual bool? EwsAllowOutlook
			{
				set
				{
					base.PowerSharpParameters["EwsAllowOutlook"] = value;
				}
			}

			// Token: 0x1700668E RID: 26254
			// (set) Token: 0x06009286 RID: 37510 RVA: 0x000D5ED5 File Offset: 0x000D40D5
			public virtual bool? EwsAllowMacOutlook
			{
				set
				{
					base.PowerSharpParameters["EwsAllowMacOutlook"] = value;
				}
			}

			// Token: 0x1700668F RID: 26255
			// (set) Token: 0x06009287 RID: 37511 RVA: 0x000D5EED File Offset: 0x000D40ED
			public virtual bool? EwsAllowEntourage
			{
				set
				{
					base.PowerSharpParameters["EwsAllowEntourage"] = value;
				}
			}

			// Token: 0x17006690 RID: 26256
			// (set) Token: 0x06009288 RID: 37512 RVA: 0x000D5F05 File Offset: 0x000D4105
			public virtual EwsApplicationAccessPolicy? EwsApplicationAccessPolicy
			{
				set
				{
					base.PowerSharpParameters["EwsApplicationAccessPolicy"] = value;
				}
			}

			// Token: 0x17006691 RID: 26257
			// (set) Token: 0x06009289 RID: 37513 RVA: 0x000D5F1D File Offset: 0x000D411D
			public virtual MultiValuedProperty<string> EwsAllowList
			{
				set
				{
					base.PowerSharpParameters["EwsAllowList"] = value;
				}
			}

			// Token: 0x17006692 RID: 26258
			// (set) Token: 0x0600928A RID: 37514 RVA: 0x000D5F30 File Offset: 0x000D4130
			public virtual MultiValuedProperty<string> EwsBlockList
			{
				set
				{
					base.PowerSharpParameters["EwsBlockList"] = value;
				}
			}

			// Token: 0x17006693 RID: 26259
			// (set) Token: 0x0600928B RID: 37515 RVA: 0x000D5F43 File Offset: 0x000D4143
			public virtual bool ShowGalAsDefaultView
			{
				set
				{
					base.PowerSharpParameters["ShowGalAsDefaultView"] = value;
				}
			}

			// Token: 0x17006694 RID: 26260
			// (set) Token: 0x0600928C RID: 37516 RVA: 0x000D5F5B File Offset: 0x000D415B
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006695 RID: 26261
			// (set) Token: 0x0600928D RID: 37517 RVA: 0x000D5F6E File Offset: 0x000D416E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006696 RID: 26262
			// (set) Token: 0x0600928E RID: 37518 RVA: 0x000D5F86 File Offset: 0x000D4186
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006697 RID: 26263
			// (set) Token: 0x0600928F RID: 37519 RVA: 0x000D5F9E File Offset: 0x000D419E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006698 RID: 26264
			// (set) Token: 0x06009290 RID: 37520 RVA: 0x000D5FB6 File Offset: 0x000D41B6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006699 RID: 26265
			// (set) Token: 0x06009291 RID: 37521 RVA: 0x000D5FCE File Offset: 0x000D41CE
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
