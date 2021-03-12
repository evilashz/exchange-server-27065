using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000CF3 RID: 3315
	public class SetMailContactCommand : SyntheticCommandWithPipelineInputNoOutput<MailContact>
	{
		// Token: 0x0600AE1B RID: 44571 RVA: 0x000FB8ED File Offset: 0x000F9AED
		private SetMailContactCommand() : base("Set-MailContact")
		{
		}

		// Token: 0x0600AE1C RID: 44572 RVA: 0x000FB8FA File Offset: 0x000F9AFA
		public SetMailContactCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600AE1D RID: 44573 RVA: 0x000FB909 File Offset: 0x000F9B09
		public virtual SetMailContactCommand SetParameters(SetMailContactCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600AE1E RID: 44574 RVA: 0x000FB913 File Offset: 0x000F9B13
		public virtual SetMailContactCommand SetParameters(SetMailContactCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000CF4 RID: 3316
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17007FEE RID: 32750
			// (set) Token: 0x0600AE1F RID: 44575 RVA: 0x000FB91D File Offset: 0x000F9B1D
			public virtual SwitchParameter ForceUpgrade
			{
				set
				{
					base.PowerSharpParameters["ForceUpgrade"] = value;
				}
			}

			// Token: 0x17007FEF RID: 32751
			// (set) Token: 0x0600AE20 RID: 44576 RVA: 0x000FB935 File Offset: 0x000F9B35
			public virtual SwitchParameter GenerateExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["GenerateExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17007FF0 RID: 32752
			// (set) Token: 0x0600AE21 RID: 44577 RVA: 0x000FB94D File Offset: 0x000F9B4D
			public virtual string SecondaryAddress
			{
				set
				{
					base.PowerSharpParameters["SecondaryAddress"] = value;
				}
			}

			// Token: 0x17007FF1 RID: 32753
			// (set) Token: 0x0600AE22 RID: 44578 RVA: 0x000FB960 File Offset: 0x000F9B60
			public virtual string SecondaryDialPlan
			{
				set
				{
					base.PowerSharpParameters["SecondaryDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17007FF2 RID: 32754
			// (set) Token: 0x0600AE23 RID: 44579 RVA: 0x000FB97E File Offset: 0x000F9B7E
			public virtual SwitchParameter RemovePicture
			{
				set
				{
					base.PowerSharpParameters["RemovePicture"] = value;
				}
			}

			// Token: 0x17007FF3 RID: 32755
			// (set) Token: 0x0600AE24 RID: 44580 RVA: 0x000FB996 File Offset: 0x000F9B96
			public virtual SwitchParameter RemoveSpokenName
			{
				set
				{
					base.PowerSharpParameters["RemoveSpokenName"] = value;
				}
			}

			// Token: 0x17007FF4 RID: 32756
			// (set) Token: 0x0600AE25 RID: 44581 RVA: 0x000FB9AE File Offset: 0x000F9BAE
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x17007FF5 RID: 32757
			// (set) Token: 0x0600AE26 RID: 44582 RVA: 0x000FB9C1 File Offset: 0x000F9BC1
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x17007FF6 RID: 32758
			// (set) Token: 0x0600AE27 RID: 44583 RVA: 0x000FB9D4 File Offset: 0x000F9BD4
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17007FF7 RID: 32759
			// (set) Token: 0x0600AE28 RID: 44584 RVA: 0x000FB9E7 File Offset: 0x000F9BE7
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007FF8 RID: 32760
			// (set) Token: 0x0600AE29 RID: 44585 RVA: 0x000FBA05 File Offset: 0x000F9C05
			public virtual MultiValuedProperty<RecipientIdParameter> GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17007FF9 RID: 32761
			// (set) Token: 0x0600AE2A RID: 44586 RVA: 0x000FBA18 File Offset: 0x000F9C18
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17007FFA RID: 32762
			// (set) Token: 0x0600AE2B RID: 44587 RVA: 0x000FBA2B File Offset: 0x000F9C2B
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17007FFB RID: 32763
			// (set) Token: 0x0600AE2C RID: 44588 RVA: 0x000FBA3E File Offset: 0x000F9C3E
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17007FFC RID: 32764
			// (set) Token: 0x0600AE2D RID: 44589 RVA: 0x000FBA51 File Offset: 0x000F9C51
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17007FFD RID: 32765
			// (set) Token: 0x0600AE2E RID: 44590 RVA: 0x000FBA64 File Offset: 0x000F9C64
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17007FFE RID: 32766
			// (set) Token: 0x0600AE2F RID: 44591 RVA: 0x000FBA77 File Offset: 0x000F9C77
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x17007FFF RID: 32767
			// (set) Token: 0x0600AE30 RID: 44592 RVA: 0x000FBA8F File Offset: 0x000F9C8F
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17008000 RID: 32768
			// (set) Token: 0x0600AE31 RID: 44593 RVA: 0x000FBAA7 File Offset: 0x000F9CA7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008001 RID: 32769
			// (set) Token: 0x0600AE32 RID: 44594 RVA: 0x000FBABA File Offset: 0x000F9CBA
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x17008002 RID: 32770
			// (set) Token: 0x0600AE33 RID: 44595 RVA: 0x000FBACD File Offset: 0x000F9CCD
			public virtual Unlimited<int> MaxRecipientPerMessage
			{
				set
				{
					base.PowerSharpParameters["MaxRecipientPerMessage"] = value;
				}
			}

			// Token: 0x17008003 RID: 32771
			// (set) Token: 0x0600AE34 RID: 44596 RVA: 0x000FBAE5 File Offset: 0x000F9CE5
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x17008004 RID: 32772
			// (set) Token: 0x0600AE35 RID: 44597 RVA: 0x000FBAFD File Offset: 0x000F9CFD
			public virtual bool UsePreferMessageFormat
			{
				set
				{
					base.PowerSharpParameters["UsePreferMessageFormat"] = value;
				}
			}

			// Token: 0x17008005 RID: 32773
			// (set) Token: 0x0600AE36 RID: 44598 RVA: 0x000FBB15 File Offset: 0x000F9D15
			public virtual MessageFormat MessageFormat
			{
				set
				{
					base.PowerSharpParameters["MessageFormat"] = value;
				}
			}

			// Token: 0x17008006 RID: 32774
			// (set) Token: 0x0600AE37 RID: 44599 RVA: 0x000FBB2D File Offset: 0x000F9D2D
			public virtual MessageBodyFormat MessageBodyFormat
			{
				set
				{
					base.PowerSharpParameters["MessageBodyFormat"] = value;
				}
			}

			// Token: 0x17008007 RID: 32775
			// (set) Token: 0x0600AE38 RID: 44600 RVA: 0x000FBB45 File Offset: 0x000F9D45
			public virtual MacAttachmentFormat MacAttachmentFormat
			{
				set
				{
					base.PowerSharpParameters["MacAttachmentFormat"] = value;
				}
			}

			// Token: 0x17008008 RID: 32776
			// (set) Token: 0x0600AE39 RID: 44601 RVA: 0x000FBB5D File Offset: 0x000F9D5D
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17008009 RID: 32777
			// (set) Token: 0x0600AE3A RID: 44602 RVA: 0x000FBB70 File Offset: 0x000F9D70
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x1700800A RID: 32778
			// (set) Token: 0x0600AE3B RID: 44603 RVA: 0x000FBB83 File Offset: 0x000F9D83
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700800B RID: 32779
			// (set) Token: 0x0600AE3C RID: 44604 RVA: 0x000FBB96 File Offset: 0x000F9D96
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x1700800C RID: 32780
			// (set) Token: 0x0600AE3D RID: 44605 RVA: 0x000FBBA9 File Offset: 0x000F9DA9
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x1700800D RID: 32781
			// (set) Token: 0x0600AE3E RID: 44606 RVA: 0x000FBBBC File Offset: 0x000F9DBC
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x1700800E RID: 32782
			// (set) Token: 0x0600AE3F RID: 44607 RVA: 0x000FBBCF File Offset: 0x000F9DCF
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x1700800F RID: 32783
			// (set) Token: 0x0600AE40 RID: 44608 RVA: 0x000FBBE2 File Offset: 0x000F9DE2
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17008010 RID: 32784
			// (set) Token: 0x0600AE41 RID: 44609 RVA: 0x000FBBF5 File Offset: 0x000F9DF5
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17008011 RID: 32785
			// (set) Token: 0x0600AE42 RID: 44610 RVA: 0x000FBC08 File Offset: 0x000F9E08
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17008012 RID: 32786
			// (set) Token: 0x0600AE43 RID: 44611 RVA: 0x000FBC1B File Offset: 0x000F9E1B
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x17008013 RID: 32787
			// (set) Token: 0x0600AE44 RID: 44612 RVA: 0x000FBC2E File Offset: 0x000F9E2E
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x17008014 RID: 32788
			// (set) Token: 0x0600AE45 RID: 44613 RVA: 0x000FBC41 File Offset: 0x000F9E41
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x17008015 RID: 32789
			// (set) Token: 0x0600AE46 RID: 44614 RVA: 0x000FBC54 File Offset: 0x000F9E54
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x17008016 RID: 32790
			// (set) Token: 0x0600AE47 RID: 44615 RVA: 0x000FBC67 File Offset: 0x000F9E67
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x17008017 RID: 32791
			// (set) Token: 0x0600AE48 RID: 44616 RVA: 0x000FBC7A File Offset: 0x000F9E7A
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x17008018 RID: 32792
			// (set) Token: 0x0600AE49 RID: 44617 RVA: 0x000FBC8D File Offset: 0x000F9E8D
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17008019 RID: 32793
			// (set) Token: 0x0600AE4A RID: 44618 RVA: 0x000FBCA0 File Offset: 0x000F9EA0
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x1700801A RID: 32794
			// (set) Token: 0x0600AE4B RID: 44619 RVA: 0x000FBCB3 File Offset: 0x000F9EB3
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x1700801B RID: 32795
			// (set) Token: 0x0600AE4C RID: 44620 RVA: 0x000FBCC6 File Offset: 0x000F9EC6
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x1700801C RID: 32796
			// (set) Token: 0x0600AE4D RID: 44621 RVA: 0x000FBCD9 File Offset: 0x000F9ED9
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x1700801D RID: 32797
			// (set) Token: 0x0600AE4E RID: 44622 RVA: 0x000FBCEC File Offset: 0x000F9EEC
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x1700801E RID: 32798
			// (set) Token: 0x0600AE4F RID: 44623 RVA: 0x000FBCFF File Offset: 0x000F9EFF
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x1700801F RID: 32799
			// (set) Token: 0x0600AE50 RID: 44624 RVA: 0x000FBD12 File Offset: 0x000F9F12
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17008020 RID: 32800
			// (set) Token: 0x0600AE51 RID: 44625 RVA: 0x000FBD25 File Offset: 0x000F9F25
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17008021 RID: 32801
			// (set) Token: 0x0600AE52 RID: 44626 RVA: 0x000FBD38 File Offset: 0x000F9F38
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17008022 RID: 32802
			// (set) Token: 0x0600AE53 RID: 44627 RVA: 0x000FBD50 File Offset: 0x000F9F50
			public virtual Unlimited<ByteQuantifiedSize> MaxSendSize
			{
				set
				{
					base.PowerSharpParameters["MaxSendSize"] = value;
				}
			}

			// Token: 0x17008023 RID: 32803
			// (set) Token: 0x0600AE54 RID: 44628 RVA: 0x000FBD68 File Offset: 0x000F9F68
			public virtual Unlimited<ByteQuantifiedSize> MaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveSize"] = value;
				}
			}

			// Token: 0x17008024 RID: 32804
			// (set) Token: 0x0600AE55 RID: 44629 RVA: 0x000FBD80 File Offset: 0x000F9F80
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17008025 RID: 32805
			// (set) Token: 0x0600AE56 RID: 44630 RVA: 0x000FBD98 File Offset: 0x000F9F98
			public virtual bool EmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["EmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x17008026 RID: 32806
			// (set) Token: 0x0600AE57 RID: 44631 RVA: 0x000FBDB0 File Offset: 0x000F9FB0
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17008027 RID: 32807
			// (set) Token: 0x0600AE58 RID: 44632 RVA: 0x000FBDC8 File Offset: 0x000F9FC8
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x17008028 RID: 32808
			// (set) Token: 0x0600AE59 RID: 44633 RVA: 0x000FBDE0 File Offset: 0x000F9FE0
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x17008029 RID: 32809
			// (set) Token: 0x0600AE5A RID: 44634 RVA: 0x000FBDF3 File Offset: 0x000F9FF3
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x1700802A RID: 32810
			// (set) Token: 0x0600AE5B RID: 44635 RVA: 0x000FBE0B File Offset: 0x000FA00B
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x1700802B RID: 32811
			// (set) Token: 0x0600AE5C RID: 44636 RVA: 0x000FBE1E File Offset: 0x000FA01E
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x1700802C RID: 32812
			// (set) Token: 0x0600AE5D RID: 44637 RVA: 0x000FBE36 File Offset: 0x000FA036
			public virtual string MailTip
			{
				set
				{
					base.PowerSharpParameters["MailTip"] = value;
				}
			}

			// Token: 0x1700802D RID: 32813
			// (set) Token: 0x0600AE5E RID: 44638 RVA: 0x000FBE49 File Offset: 0x000FA049
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x1700802E RID: 32814
			// (set) Token: 0x0600AE5F RID: 44639 RVA: 0x000FBE5C File Offset: 0x000FA05C
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700802F RID: 32815
			// (set) Token: 0x0600AE60 RID: 44640 RVA: 0x000FBE6F File Offset: 0x000FA06F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008030 RID: 32816
			// (set) Token: 0x0600AE61 RID: 44641 RVA: 0x000FBE87 File Offset: 0x000FA087
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008031 RID: 32817
			// (set) Token: 0x0600AE62 RID: 44642 RVA: 0x000FBE9F File Offset: 0x000FA09F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008032 RID: 32818
			// (set) Token: 0x0600AE63 RID: 44643 RVA: 0x000FBEB7 File Offset: 0x000FA0B7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008033 RID: 32819
			// (set) Token: 0x0600AE64 RID: 44644 RVA: 0x000FBECF File Offset: 0x000FA0CF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CF5 RID: 3317
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17008034 RID: 32820
			// (set) Token: 0x0600AE66 RID: 44646 RVA: 0x000FBEEF File Offset: 0x000FA0EF
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailContactIdParameter(value) : null);
				}
			}

			// Token: 0x17008035 RID: 32821
			// (set) Token: 0x0600AE67 RID: 44647 RVA: 0x000FBF0D File Offset: 0x000FA10D
			public virtual SwitchParameter ForceUpgrade
			{
				set
				{
					base.PowerSharpParameters["ForceUpgrade"] = value;
				}
			}

			// Token: 0x17008036 RID: 32822
			// (set) Token: 0x0600AE68 RID: 44648 RVA: 0x000FBF25 File Offset: 0x000FA125
			public virtual SwitchParameter GenerateExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["GenerateExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17008037 RID: 32823
			// (set) Token: 0x0600AE69 RID: 44649 RVA: 0x000FBF3D File Offset: 0x000FA13D
			public virtual string SecondaryAddress
			{
				set
				{
					base.PowerSharpParameters["SecondaryAddress"] = value;
				}
			}

			// Token: 0x17008038 RID: 32824
			// (set) Token: 0x0600AE6A RID: 44650 RVA: 0x000FBF50 File Offset: 0x000FA150
			public virtual string SecondaryDialPlan
			{
				set
				{
					base.PowerSharpParameters["SecondaryDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17008039 RID: 32825
			// (set) Token: 0x0600AE6B RID: 44651 RVA: 0x000FBF6E File Offset: 0x000FA16E
			public virtual SwitchParameter RemovePicture
			{
				set
				{
					base.PowerSharpParameters["RemovePicture"] = value;
				}
			}

			// Token: 0x1700803A RID: 32826
			// (set) Token: 0x0600AE6C RID: 44652 RVA: 0x000FBF86 File Offset: 0x000FA186
			public virtual SwitchParameter RemoveSpokenName
			{
				set
				{
					base.PowerSharpParameters["RemoveSpokenName"] = value;
				}
			}

			// Token: 0x1700803B RID: 32827
			// (set) Token: 0x0600AE6D RID: 44653 RVA: 0x000FBF9E File Offset: 0x000FA19E
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFrom
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFrom"] = value;
				}
			}

			// Token: 0x1700803C RID: 32828
			// (set) Token: 0x0600AE6E RID: 44654 RVA: 0x000FBFB1 File Offset: 0x000FA1B1
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromDLMembers"] = value;
				}
			}

			// Token: 0x1700803D RID: 32829
			// (set) Token: 0x0600AE6F RID: 44655 RVA: 0x000FBFC4 File Offset: 0x000FA1C4
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> AcceptMessagesOnlyFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["AcceptMessagesOnlyFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x1700803E RID: 32830
			// (set) Token: 0x0600AE70 RID: 44656 RVA: 0x000FBFD7 File Offset: 0x000FA1D7
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700803F RID: 32831
			// (set) Token: 0x0600AE71 RID: 44657 RVA: 0x000FBFF5 File Offset: 0x000FA1F5
			public virtual MultiValuedProperty<RecipientIdParameter> GrantSendOnBehalfTo
			{
				set
				{
					base.PowerSharpParameters["GrantSendOnBehalfTo"] = value;
				}
			}

			// Token: 0x17008040 RID: 32832
			// (set) Token: 0x0600AE72 RID: 44658 RVA: 0x000FC008 File Offset: 0x000FA208
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17008041 RID: 32833
			// (set) Token: 0x0600AE73 RID: 44659 RVA: 0x000FC01B File Offset: 0x000FA21B
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFrom
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFrom"] = value;
				}
			}

			// Token: 0x17008042 RID: 32834
			// (set) Token: 0x0600AE74 RID: 44660 RVA: 0x000FC02E File Offset: 0x000FA22E
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromDLMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromDLMembers"] = value;
				}
			}

			// Token: 0x17008043 RID: 32835
			// (set) Token: 0x0600AE75 RID: 44661 RVA: 0x000FC041 File Offset: 0x000FA241
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> RejectMessagesFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["RejectMessagesFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17008044 RID: 32836
			// (set) Token: 0x0600AE76 RID: 44662 RVA: 0x000FC054 File Offset: 0x000FA254
			public virtual MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromSendersOrMembers
			{
				set
				{
					base.PowerSharpParameters["BypassModerationFromSendersOrMembers"] = value;
				}
			}

			// Token: 0x17008045 RID: 32837
			// (set) Token: 0x0600AE77 RID: 44663 RVA: 0x000FC067 File Offset: 0x000FA267
			public virtual bool CreateDTMFMap
			{
				set
				{
					base.PowerSharpParameters["CreateDTMFMap"] = value;
				}
			}

			// Token: 0x17008046 RID: 32838
			// (set) Token: 0x0600AE78 RID: 44664 RVA: 0x000FC07F File Offset: 0x000FA27F
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17008047 RID: 32839
			// (set) Token: 0x0600AE79 RID: 44665 RVA: 0x000FC097 File Offset: 0x000FA297
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008048 RID: 32840
			// (set) Token: 0x0600AE7A RID: 44666 RVA: 0x000FC0AA File Offset: 0x000FA2AA
			public virtual ProxyAddress ExternalEmailAddress
			{
				set
				{
					base.PowerSharpParameters["ExternalEmailAddress"] = value;
				}
			}

			// Token: 0x17008049 RID: 32841
			// (set) Token: 0x0600AE7B RID: 44667 RVA: 0x000FC0BD File Offset: 0x000FA2BD
			public virtual Unlimited<int> MaxRecipientPerMessage
			{
				set
				{
					base.PowerSharpParameters["MaxRecipientPerMessage"] = value;
				}
			}

			// Token: 0x1700804A RID: 32842
			// (set) Token: 0x0600AE7C RID: 44668 RVA: 0x000FC0D5 File Offset: 0x000FA2D5
			public virtual UseMapiRichTextFormat UseMapiRichTextFormat
			{
				set
				{
					base.PowerSharpParameters["UseMapiRichTextFormat"] = value;
				}
			}

			// Token: 0x1700804B RID: 32843
			// (set) Token: 0x0600AE7D RID: 44669 RVA: 0x000FC0ED File Offset: 0x000FA2ED
			public virtual bool UsePreferMessageFormat
			{
				set
				{
					base.PowerSharpParameters["UsePreferMessageFormat"] = value;
				}
			}

			// Token: 0x1700804C RID: 32844
			// (set) Token: 0x0600AE7E RID: 44670 RVA: 0x000FC105 File Offset: 0x000FA305
			public virtual MessageFormat MessageFormat
			{
				set
				{
					base.PowerSharpParameters["MessageFormat"] = value;
				}
			}

			// Token: 0x1700804D RID: 32845
			// (set) Token: 0x0600AE7F RID: 44671 RVA: 0x000FC11D File Offset: 0x000FA31D
			public virtual MessageBodyFormat MessageBodyFormat
			{
				set
				{
					base.PowerSharpParameters["MessageBodyFormat"] = value;
				}
			}

			// Token: 0x1700804E RID: 32846
			// (set) Token: 0x0600AE80 RID: 44672 RVA: 0x000FC135 File Offset: 0x000FA335
			public virtual MacAttachmentFormat MacAttachmentFormat
			{
				set
				{
					base.PowerSharpParameters["MacAttachmentFormat"] = value;
				}
			}

			// Token: 0x1700804F RID: 32847
			// (set) Token: 0x0600AE81 RID: 44673 RVA: 0x000FC14D File Offset: 0x000FA34D
			public virtual MultiValuedProperty<byte[]> UserCertificate
			{
				set
				{
					base.PowerSharpParameters["UserCertificate"] = value;
				}
			}

			// Token: 0x17008050 RID: 32848
			// (set) Token: 0x0600AE82 RID: 44674 RVA: 0x000FC160 File Offset: 0x000FA360
			public virtual MultiValuedProperty<byte[]> UserSMimeCertificate
			{
				set
				{
					base.PowerSharpParameters["UserSMimeCertificate"] = value;
				}
			}

			// Token: 0x17008051 RID: 32849
			// (set) Token: 0x0600AE83 RID: 44675 RVA: 0x000FC173 File Offset: 0x000FA373
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17008052 RID: 32850
			// (set) Token: 0x0600AE84 RID: 44676 RVA: 0x000FC186 File Offset: 0x000FA386
			public virtual string CustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute1"] = value;
				}
			}

			// Token: 0x17008053 RID: 32851
			// (set) Token: 0x0600AE85 RID: 44677 RVA: 0x000FC199 File Offset: 0x000FA399
			public virtual string CustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute10"] = value;
				}
			}

			// Token: 0x17008054 RID: 32852
			// (set) Token: 0x0600AE86 RID: 44678 RVA: 0x000FC1AC File Offset: 0x000FA3AC
			public virtual string CustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute11"] = value;
				}
			}

			// Token: 0x17008055 RID: 32853
			// (set) Token: 0x0600AE87 RID: 44679 RVA: 0x000FC1BF File Offset: 0x000FA3BF
			public virtual string CustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute12"] = value;
				}
			}

			// Token: 0x17008056 RID: 32854
			// (set) Token: 0x0600AE88 RID: 44680 RVA: 0x000FC1D2 File Offset: 0x000FA3D2
			public virtual string CustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute13"] = value;
				}
			}

			// Token: 0x17008057 RID: 32855
			// (set) Token: 0x0600AE89 RID: 44681 RVA: 0x000FC1E5 File Offset: 0x000FA3E5
			public virtual string CustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute14"] = value;
				}
			}

			// Token: 0x17008058 RID: 32856
			// (set) Token: 0x0600AE8A RID: 44682 RVA: 0x000FC1F8 File Offset: 0x000FA3F8
			public virtual string CustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute15"] = value;
				}
			}

			// Token: 0x17008059 RID: 32857
			// (set) Token: 0x0600AE8B RID: 44683 RVA: 0x000FC20B File Offset: 0x000FA40B
			public virtual string CustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute2"] = value;
				}
			}

			// Token: 0x1700805A RID: 32858
			// (set) Token: 0x0600AE8C RID: 44684 RVA: 0x000FC21E File Offset: 0x000FA41E
			public virtual string CustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute3"] = value;
				}
			}

			// Token: 0x1700805B RID: 32859
			// (set) Token: 0x0600AE8D RID: 44685 RVA: 0x000FC231 File Offset: 0x000FA431
			public virtual string CustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute4"] = value;
				}
			}

			// Token: 0x1700805C RID: 32860
			// (set) Token: 0x0600AE8E RID: 44686 RVA: 0x000FC244 File Offset: 0x000FA444
			public virtual string CustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute5"] = value;
				}
			}

			// Token: 0x1700805D RID: 32861
			// (set) Token: 0x0600AE8F RID: 44687 RVA: 0x000FC257 File Offset: 0x000FA457
			public virtual string CustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute6"] = value;
				}
			}

			// Token: 0x1700805E RID: 32862
			// (set) Token: 0x0600AE90 RID: 44688 RVA: 0x000FC26A File Offset: 0x000FA46A
			public virtual string CustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute7"] = value;
				}
			}

			// Token: 0x1700805F RID: 32863
			// (set) Token: 0x0600AE91 RID: 44689 RVA: 0x000FC27D File Offset: 0x000FA47D
			public virtual string CustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute8"] = value;
				}
			}

			// Token: 0x17008060 RID: 32864
			// (set) Token: 0x0600AE92 RID: 44690 RVA: 0x000FC290 File Offset: 0x000FA490
			public virtual string CustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["CustomAttribute9"] = value;
				}
			}

			// Token: 0x17008061 RID: 32865
			// (set) Token: 0x0600AE93 RID: 44691 RVA: 0x000FC2A3 File Offset: 0x000FA4A3
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute1"] = value;
				}
			}

			// Token: 0x17008062 RID: 32866
			// (set) Token: 0x0600AE94 RID: 44692 RVA: 0x000FC2B6 File Offset: 0x000FA4B6
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute2"] = value;
				}
			}

			// Token: 0x17008063 RID: 32867
			// (set) Token: 0x0600AE95 RID: 44693 RVA: 0x000FC2C9 File Offset: 0x000FA4C9
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute3"] = value;
				}
			}

			// Token: 0x17008064 RID: 32868
			// (set) Token: 0x0600AE96 RID: 44694 RVA: 0x000FC2DC File Offset: 0x000FA4DC
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute4"] = value;
				}
			}

			// Token: 0x17008065 RID: 32869
			// (set) Token: 0x0600AE97 RID: 44695 RVA: 0x000FC2EF File Offset: 0x000FA4EF
			public virtual MultiValuedProperty<string> ExtensionCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ExtensionCustomAttribute5"] = value;
				}
			}

			// Token: 0x17008066 RID: 32870
			// (set) Token: 0x0600AE98 RID: 44696 RVA: 0x000FC302 File Offset: 0x000FA502
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17008067 RID: 32871
			// (set) Token: 0x0600AE99 RID: 44697 RVA: 0x000FC315 File Offset: 0x000FA515
			public virtual ProxyAddressCollection EmailAddresses
			{
				set
				{
					base.PowerSharpParameters["EmailAddresses"] = value;
				}
			}

			// Token: 0x17008068 RID: 32872
			// (set) Token: 0x0600AE9A RID: 44698 RVA: 0x000FC328 File Offset: 0x000FA528
			public virtual bool HiddenFromAddressListsEnabled
			{
				set
				{
					base.PowerSharpParameters["HiddenFromAddressListsEnabled"] = value;
				}
			}

			// Token: 0x17008069 RID: 32873
			// (set) Token: 0x0600AE9B RID: 44699 RVA: 0x000FC340 File Offset: 0x000FA540
			public virtual Unlimited<ByteQuantifiedSize> MaxSendSize
			{
				set
				{
					base.PowerSharpParameters["MaxSendSize"] = value;
				}
			}

			// Token: 0x1700806A RID: 32874
			// (set) Token: 0x0600AE9C RID: 44700 RVA: 0x000FC358 File Offset: 0x000FA558
			public virtual Unlimited<ByteQuantifiedSize> MaxReceiveSize
			{
				set
				{
					base.PowerSharpParameters["MaxReceiveSize"] = value;
				}
			}

			// Token: 0x1700806B RID: 32875
			// (set) Token: 0x0600AE9D RID: 44701 RVA: 0x000FC370 File Offset: 0x000FA570
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x1700806C RID: 32876
			// (set) Token: 0x0600AE9E RID: 44702 RVA: 0x000FC388 File Offset: 0x000FA588
			public virtual bool EmailAddressPolicyEnabled
			{
				set
				{
					base.PowerSharpParameters["EmailAddressPolicyEnabled"] = value;
				}
			}

			// Token: 0x1700806D RID: 32877
			// (set) Token: 0x0600AE9F RID: 44703 RVA: 0x000FC3A0 File Offset: 0x000FA5A0
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700806E RID: 32878
			// (set) Token: 0x0600AEA0 RID: 44704 RVA: 0x000FC3B8 File Offset: 0x000FA5B8
			public virtual bool RequireSenderAuthenticationEnabled
			{
				set
				{
					base.PowerSharpParameters["RequireSenderAuthenticationEnabled"] = value;
				}
			}

			// Token: 0x1700806F RID: 32879
			// (set) Token: 0x0600AEA1 RID: 44705 RVA: 0x000FC3D0 File Offset: 0x000FA5D0
			public virtual string SimpleDisplayName
			{
				set
				{
					base.PowerSharpParameters["SimpleDisplayName"] = value;
				}
			}

			// Token: 0x17008070 RID: 32880
			// (set) Token: 0x0600AEA2 RID: 44706 RVA: 0x000FC3E3 File Offset: 0x000FA5E3
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17008071 RID: 32881
			// (set) Token: 0x0600AEA3 RID: 44707 RVA: 0x000FC3FB File Offset: 0x000FA5FB
			public virtual MultiValuedProperty<string> UMDtmfMap
			{
				set
				{
					base.PowerSharpParameters["UMDtmfMap"] = value;
				}
			}

			// Token: 0x17008072 RID: 32882
			// (set) Token: 0x0600AEA4 RID: 44708 RVA: 0x000FC40E File Offset: 0x000FA60E
			public virtual SmtpAddress WindowsEmailAddress
			{
				set
				{
					base.PowerSharpParameters["WindowsEmailAddress"] = value;
				}
			}

			// Token: 0x17008073 RID: 32883
			// (set) Token: 0x0600AEA5 RID: 44709 RVA: 0x000FC426 File Offset: 0x000FA626
			public virtual string MailTip
			{
				set
				{
					base.PowerSharpParameters["MailTip"] = value;
				}
			}

			// Token: 0x17008074 RID: 32884
			// (set) Token: 0x0600AEA6 RID: 44710 RVA: 0x000FC439 File Offset: 0x000FA639
			public virtual MultiValuedProperty<string> MailTipTranslations
			{
				set
				{
					base.PowerSharpParameters["MailTipTranslations"] = value;
				}
			}

			// Token: 0x17008075 RID: 32885
			// (set) Token: 0x0600AEA7 RID: 44711 RVA: 0x000FC44C File Offset: 0x000FA64C
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17008076 RID: 32886
			// (set) Token: 0x0600AEA8 RID: 44712 RVA: 0x000FC45F File Offset: 0x000FA65F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008077 RID: 32887
			// (set) Token: 0x0600AEA9 RID: 44713 RVA: 0x000FC477 File Offset: 0x000FA677
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008078 RID: 32888
			// (set) Token: 0x0600AEAA RID: 44714 RVA: 0x000FC48F File Offset: 0x000FA68F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008079 RID: 32889
			// (set) Token: 0x0600AEAB RID: 44715 RVA: 0x000FC4A7 File Offset: 0x000FA6A7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700807A RID: 32890
			// (set) Token: 0x0600AEAC RID: 44716 RVA: 0x000FC4BF File Offset: 0x000FA6BF
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
