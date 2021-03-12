using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.LinkedFolder;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Net.JobQueues;
using Microsoft.Exchange.UnifiedGroups;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000E2 RID: 226
	[Cmdlet("New", "SiteMailbox", DefaultParameterSetName = "TeamMailboxIW", SupportsShouldProcess = true)]
	public sealed class NewSiteMailbox : NewMailboxOrSyncMailbox
	{
		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x0003E226 File Offset: 0x0003C426
		// (set) Token: 0x0600112C RID: 4396 RVA: 0x0003E22E File Offset: 0x0003C42E
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x0600112D RID: 4397 RVA: 0x0003E237 File Offset: 0x0003C437
		// (set) Token: 0x0600112E RID: 4398 RVA: 0x0003E23F File Offset: 0x0003C43F
		[Parameter(Mandatory = false, Position = 0)]
		[ValidateNotNullOrEmpty]
		public new string DisplayName
		{
			get
			{
				return base.DisplayName;
			}
			set
			{
				base.DisplayName = value;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x0600112F RID: 4399 RVA: 0x0003E248 File Offset: 0x0003C448
		// (set) Token: 0x06001130 RID: 4400 RVA: 0x0003E250 File Offset: 0x0003C450
		[Parameter(Mandatory = false, ParameterSetName = "TeamMailboxITPro")]
		[ValidateNotNullOrEmpty]
		public new OrganizationIdParameter Organization
		{
			get
			{
				return base.Organization;
			}
			set
			{
				base.Organization = value;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001131 RID: 4401 RVA: 0x0003E259 File Offset: 0x0003C459
		// (set) Token: 0x06001132 RID: 4402 RVA: 0x0003E261 File Offset: 0x0003C461
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "TeamMailboxITPro")]
		public new OrganizationalUnitIdParameter OrganizationalUnit
		{
			get
			{
				return base.OrganizationalUnit;
			}
			set
			{
				base.OrganizationalUnit = value;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x0003E26A File Offset: 0x0003C46A
		// (set) Token: 0x06001134 RID: 4404 RVA: 0x0003E272 File Offset: 0x0003C472
		[Parameter(Mandatory = false, ParameterSetName = "TeamMailboxITPro")]
		[ValidateNotNullOrEmpty]
		public new DatabaseIdParameter Database
		{
			get
			{
				return base.Database;
			}
			set
			{
				base.Database = value;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x0003E27B File Offset: 0x0003C47B
		// (set) Token: 0x06001136 RID: 4406 RVA: 0x0003E283 File Offset: 0x0003C483
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "TeamMailboxITPro")]
		public new string Alias
		{
			get
			{
				return base.Alias;
			}
			set
			{
				base.Alias = value;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001137 RID: 4407 RVA: 0x0003E28C File Offset: 0x0003C48C
		// (set) Token: 0x06001138 RID: 4408 RVA: 0x0003E2A4 File Offset: 0x0003C4A4
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public Uri SharePointUrl
		{
			get
			{
				return (Uri)base.Fields["SharePointUrl"];
			}
			set
			{
				if (value != null && (!value.IsAbsoluteUri || (!(value.Scheme == Uri.UriSchemeHttps) && !(value.Scheme == Uri.UriSchemeHttp))))
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorTeamMailboxSharePointUrl), ExchangeErrorCategory.Client, null);
				}
				base.Fields["SharePointUrl"] = value;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x0003E30D File Offset: 0x0003C50D
		// (set) Token: 0x0600113A RID: 4410 RVA: 0x0003E315 File Offset: 0x0003C515
		private new SwitchParameter AccountDisabled { get; set; }

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x0003E31E File Offset: 0x0003C51E
		// (set) Token: 0x0600113C RID: 4412 RVA: 0x0003E326 File Offset: 0x0003C526
		private new MailboxPolicyIdParameter ActiveSyncMailboxPolicy { get; set; }

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x0003E32F File Offset: 0x0003C52F
		// (set) Token: 0x0600113E RID: 4414 RVA: 0x0003E337 File Offset: 0x0003C537
		private new AddressBookMailboxPolicyIdParameter AddressBookPolicy { get; set; }

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x0003E340 File Offset: 0x0003C540
		// (set) Token: 0x06001140 RID: 4416 RVA: 0x0003E348 File Offset: 0x0003C548
		private new SwitchParameter Arbitration { get; set; }

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x0003E351 File Offset: 0x0003C551
		// (set) Token: 0x06001142 RID: 4418 RVA: 0x0003E359 File Offset: 0x0003C559
		private new MailboxIdParameter ArbitrationMailbox { get; set; }

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x0003E362 File Offset: 0x0003C562
		// (set) Token: 0x06001144 RID: 4420 RVA: 0x0003E36A File Offset: 0x0003C56A
		private new SwitchParameter Archive { get; set; }

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x0003E373 File Offset: 0x0003C573
		// (set) Token: 0x06001146 RID: 4422 RVA: 0x0003E37B File Offset: 0x0003C57B
		private new DatabaseIdParameter ArchiveDatabase { get; set; }

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001147 RID: 4423 RVA: 0x0003E384 File Offset: 0x0003C584
		// (set) Token: 0x06001148 RID: 4424 RVA: 0x0003E38C File Offset: 0x0003C58C
		private new SmtpDomain ArchiveDomain { get; set; }

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x0003E395 File Offset: 0x0003C595
		// (set) Token: 0x0600114A RID: 4426 RVA: 0x0003E39D File Offset: 0x0003C59D
		private new SwitchParameter AuditLog { get; set; }

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x0003E3A6 File Offset: 0x0003C5A6
		// (set) Token: 0x0600114C RID: 4428 RVA: 0x0003E3AE File Offset: 0x0003C5AE
		private new SwitchParameter AuxMailbox { get; set; }

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x0600114D RID: 4429 RVA: 0x0003E3B7 File Offset: 0x0003C5B7
		// (set) Token: 0x0600114E RID: 4430 RVA: 0x0003E3BF File Offset: 0x0003C5BF
		private new SwitchParameter BypassLiveId { get; set; }

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x0003E3C8 File Offset: 0x0003C5C8
		// (set) Token: 0x06001150 RID: 4432 RVA: 0x0003E3D0 File Offset: 0x0003C5D0
		private new SwitchParameter Discovery { get; set; }

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x0003E3D9 File Offset: 0x0003C5D9
		// (set) Token: 0x06001152 RID: 4434 RVA: 0x0003E3E1 File Offset: 0x0003C5E1
		private new SwitchParameter Equipment { get; set; }

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x0003E3EA File Offset: 0x0003C5EA
		// (set) Token: 0x06001154 RID: 4436 RVA: 0x0003E3F2 File Offset: 0x0003C5F2
		private new SwitchParameter EvictLiveId { get; set; }

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x0003E3FB File Offset: 0x0003C5FB
		// (set) Token: 0x06001156 RID: 4438 RVA: 0x0003E403 File Offset: 0x0003C603
		private new string ExternalDirectoryObjectId { get; set; }

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x0003E40C File Offset: 0x0003C60C
		// (set) Token: 0x06001158 RID: 4440 RVA: 0x0003E414 File Offset: 0x0003C614
		private new string FederatedIdentity { get; set; }

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x0003E41D File Offset: 0x0003C61D
		// (set) Token: 0x0600115A RID: 4442 RVA: 0x0003E425 File Offset: 0x0003C625
		private new string FirstName { get; set; }

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x0003E42E File Offset: 0x0003C62E
		// (set) Token: 0x0600115C RID: 4444 RVA: 0x0003E436 File Offset: 0x0003C636
		private new SwitchParameter ForestWideDomainControllerAffinityByExecutingUser { get; set; }

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x0003E43F File Offset: 0x0003C63F
		// (set) Token: 0x0600115E RID: 4446 RVA: 0x0003E447 File Offset: 0x0003C647
		private new string ImmutableId { get; set; }

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x0003E450 File Offset: 0x0003C650
		// (set) Token: 0x06001160 RID: 4448 RVA: 0x0003E458 File Offset: 0x0003C658
		private new SwitchParameter ImportLiveId { get; set; }

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x0003E461 File Offset: 0x0003C661
		// (set) Token: 0x06001162 RID: 4450 RVA: 0x0003E469 File Offset: 0x0003C669
		private new string Initials { get; set; }

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x0003E472 File Offset: 0x0003C672
		// (set) Token: 0x06001164 RID: 4452 RVA: 0x0003E47A File Offset: 0x0003C67A
		private new MultiValuedProperty<CultureInfo> Languages { get; set; }

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x0003E483 File Offset: 0x0003C683
		// (set) Token: 0x06001166 RID: 4454 RVA: 0x0003E48B File Offset: 0x0003C68B
		private new string LastName { get; set; }

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x0003E494 File Offset: 0x0003C694
		// (set) Token: 0x06001168 RID: 4456 RVA: 0x0003E49C File Offset: 0x0003C69C
		private new PSCredential LinkedCredential { get; set; }

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x0003E4A5 File Offset: 0x0003C6A5
		// (set) Token: 0x0600116A RID: 4458 RVA: 0x0003E4AD File Offset: 0x0003C6AD
		private new string LinkedDomainController { get; set; }

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x0003E4B6 File Offset: 0x0003C6B6
		// (set) Token: 0x0600116C RID: 4460 RVA: 0x0003E4BE File Offset: 0x0003C6BE
		private new UserIdParameter LinkedMasterAccount { get; set; }

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x0003E4C7 File Offset: 0x0003C6C7
		// (set) Token: 0x0600116E RID: 4462 RVA: 0x0003E4CF File Offset: 0x0003C6CF
		private new SwitchParameter LinkedRoom { get; set; }

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x0600116F RID: 4463 RVA: 0x0003E4D8 File Offset: 0x0003C6D8
		// (set) Token: 0x06001170 RID: 4464 RVA: 0x0003E4E0 File Offset: 0x0003C6E0
		private new MailboxPlanIdParameter MailboxPlan { get; set; }

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001171 RID: 4465 RVA: 0x0003E4E9 File Offset: 0x0003C6E9
		// (set) Token: 0x06001172 RID: 4466 RVA: 0x0003E4F1 File Offset: 0x0003C6F1
		private new Guid MailboxContainerGuid { get; set; }

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x0003E4FA File Offset: 0x0003C6FA
		// (set) Token: 0x06001174 RID: 4468 RVA: 0x0003E502 File Offset: 0x0003C702
		private new WindowsLiveId MicrosoftOnlineServicesID { get; set; }

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001175 RID: 4469 RVA: 0x0003E50B File Offset: 0x0003C70B
		// (set) Token: 0x06001176 RID: 4470 RVA: 0x0003E513 File Offset: 0x0003C713
		private new MultiValuedProperty<ModeratorIDParameter> ModeratedBy { get; set; }

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x0003E51C File Offset: 0x0003C71C
		// (set) Token: 0x06001178 RID: 4472 RVA: 0x0003E524 File Offset: 0x0003C724
		private new bool ModerationEnabled { get; set; }

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x0003E52D File Offset: 0x0003C72D
		// (set) Token: 0x0600117A RID: 4474 RVA: 0x0003E535 File Offset: 0x0003C735
		private new NetID NetID { get; set; }

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x0003E53E File Offset: 0x0003C73E
		// (set) Token: 0x0600117C RID: 4476 RVA: 0x0003E546 File Offset: 0x0003C746
		private new SecureString Password { get; set; }

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x0003E54F File Offset: 0x0003C74F
		// (set) Token: 0x0600117E RID: 4478 RVA: 0x0003E557 File Offset: 0x0003C757
		private new SmtpAddress PrimarySmtpAddress { get; set; }

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x0003E560 File Offset: 0x0003C760
		// (set) Token: 0x06001180 RID: 4480 RVA: 0x0003E568 File Offset: 0x0003C768
		private new SwitchParameter PublicFolder { get; set; }

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001181 RID: 4481 RVA: 0x0003E571 File Offset: 0x0003C771
		// (set) Token: 0x06001182 RID: 4482 RVA: 0x0003E579 File Offset: 0x0003C779
		private new SwitchParameter HoldForMigration { get; set; }

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x0003E582 File Offset: 0x0003C782
		// (set) Token: 0x06001184 RID: 4484 RVA: 0x0003E58A File Offset: 0x0003C78A
		private new bool QueryBaseDNRestrictionEnabled { get; set; }

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06001185 RID: 4485 RVA: 0x0003E593 File Offset: 0x0003C793
		// (set) Token: 0x06001186 RID: 4486 RVA: 0x0003E59B File Offset: 0x0003C79B
		private new RemoteAccountPolicyIdParameter RemoteAccountPolicy { get; set; }

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x0003E5A4 File Offset: 0x0003C7A4
		// (set) Token: 0x06001188 RID: 4488 RVA: 0x0003E5AC File Offset: 0x0003C7AC
		private new SwitchParameter RemoteArchive { get; set; }

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x0003E5B5 File Offset: 0x0003C7B5
		// (set) Token: 0x0600118A RID: 4490 RVA: 0x0003E5BD File Offset: 0x0003C7BD
		private new bool RemotePowerShellEnabled { get; set; }

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x0003E5C6 File Offset: 0x0003C7C6
		// (set) Token: 0x0600118C RID: 4492 RVA: 0x0003E5CE File Offset: 0x0003C7CE
		private new RemovedMailboxIdParameter RemovedMailbox { get; set; }

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x0003E5D7 File Offset: 0x0003C7D7
		// (set) Token: 0x0600118E RID: 4494 RVA: 0x0003E5DF File Offset: 0x0003C7DF
		private new bool ResetPasswordOnNextLogon { get; set; }

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x0003E5E8 File Offset: 0x0003C7E8
		// (set) Token: 0x06001190 RID: 4496 RVA: 0x0003E5F0 File Offset: 0x0003C7F0
		private new MailboxPolicyIdParameter RetentionPolicy { get; set; }

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001191 RID: 4497 RVA: 0x0003E5F9 File Offset: 0x0003C7F9
		// (set) Token: 0x06001192 RID: 4498 RVA: 0x0003E601 File Offset: 0x0003C801
		private new MailboxPolicyIdParameter RoleAssignmentPolicy { get; set; }

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001193 RID: 4499 RVA: 0x0003E60A File Offset: 0x0003C80A
		// (set) Token: 0x06001194 RID: 4500 RVA: 0x0003E612 File Offset: 0x0003C812
		private new SwitchParameter Room { get; set; }

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x0003E61B File Offset: 0x0003C81B
		// (set) Token: 0x06001196 RID: 4502 RVA: 0x0003E623 File Offset: 0x0003C823
		private new string SamAccountName { get; set; }

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x0003E62C File Offset: 0x0003C82C
		// (set) Token: 0x06001198 RID: 4504 RVA: 0x0003E634 File Offset: 0x0003C834
		private new TransportModerationNotificationFlags SendModerationNotifications { get; set; }

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x0003E63D File Offset: 0x0003C83D
		// (set) Token: 0x0600119A RID: 4506 RVA: 0x0003E645 File Offset: 0x0003C845
		private new SwitchParameter Shared { get; set; }

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x0003E64E File Offset: 0x0003C84E
		// (set) Token: 0x0600119C RID: 4508 RVA: 0x0003E656 File Offset: 0x0003C856
		private new SharingPolicyIdParameter SharingPolicy { get; set; }

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x0003E65F File Offset: 0x0003C85F
		// (set) Token: 0x0600119E RID: 4510 RVA: 0x0003E667 File Offset: 0x0003C867
		private new bool SKUAssigned { get; set; }

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x0003E670 File Offset: 0x0003C870
		// (set) Token: 0x060011A0 RID: 4512 RVA: 0x0003E678 File Offset: 0x0003C878
		private new MultiValuedProperty<Capability> AddOnSKUCapability { get; set; }

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x060011A1 RID: 4513 RVA: 0x0003E681 File Offset: 0x0003C881
		// (set) Token: 0x060011A2 RID: 4514 RVA: 0x0003E689 File Offset: 0x0003C889
		private new Capability SKUCapability { get; set; }

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x060011A3 RID: 4515 RVA: 0x0003E692 File Offset: 0x0003C892
		// (set) Token: 0x060011A4 RID: 4516 RVA: 0x0003E69A File Offset: 0x0003C89A
		private new SwitchParameter TargetAllMDBs { get; set; }

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x060011A5 RID: 4517 RVA: 0x0003E6A3 File Offset: 0x0003C8A3
		// (set) Token: 0x060011A6 RID: 4518 RVA: 0x0003E6AB File Offset: 0x0003C8AB
		private new ThrottlingPolicyIdParameter ThrottlingPolicy { get; set; }

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x060011A7 RID: 4519 RVA: 0x0003E6B4 File Offset: 0x0003C8B4
		// (set) Token: 0x060011A8 RID: 4520 RVA: 0x0003E6BC File Offset: 0x0003C8BC
		private new CountryInfo UsageLocation { get; set; }

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x0003E6C5 File Offset: 0x0003C8C5
		// (set) Token: 0x060011AA RID: 4522 RVA: 0x0003E6CD File Offset: 0x0003C8CD
		private new SwitchParameter UseExistingLiveId { get; set; }

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x0003E6D6 File Offset: 0x0003C8D6
		// (set) Token: 0x060011AC RID: 4524 RVA: 0x0003E6DE File Offset: 0x0003C8DE
		private new string UserPrincipalName { get; set; }

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x0003E6E7 File Offset: 0x0003C8E7
		// (set) Token: 0x060011AE RID: 4526 RVA: 0x0003E6EF File Offset: 0x0003C8EF
		private new WindowsLiveId WindowsLiveID { get; set; }

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x0003E6F8 File Offset: 0x0003C8F8
		// (set) Token: 0x060011B0 RID: 4528 RVA: 0x0003E700 File Offset: 0x0003C900
		private new SecureString RoomMailboxPassword { get; set; }

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x0003E709 File Offset: 0x0003C909
		// (set) Token: 0x060011B2 RID: 4530 RVA: 0x0003E711 File Offset: 0x0003C911
		private new bool EnableRoomMailboxAccount { get; set; }

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x0003E71A File Offset: 0x0003C91A
		// (set) Token: 0x060011B4 RID: 4532 RVA: 0x0003E722 File Offset: 0x0003C922
		private new bool IsExcludedFromServingHierarchy { get; set; }

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x0003E72B File Offset: 0x0003C92B
		// (set) Token: 0x060011B6 RID: 4534 RVA: 0x0003E733 File Offset: 0x0003C933
		private new MailboxProvisioningConstraint MailboxProvisioningConstraint { get; set; }

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x0003E73C File Offset: 0x0003C93C
		// (set) Token: 0x060011B8 RID: 4536 RVA: 0x0003E744 File Offset: 0x0003C944
		private new MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences { get; set; }

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x0003E74D File Offset: 0x0003C94D
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewTeamMailbox(this.Name.ToString(), base.UserPrincipalName.ToString(), base.RecipientContainerId.ToString());
			}
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0003E778 File Offset: 0x0003C978
		protected override void InternalBeginProcessing()
		{
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).CmdletInfra.SiteMailboxProvisioningInExecutingUserOUEnabled.Enabled && this.OrganizationalUnit == null && base.CurrentTaskContext.UserInfo != null)
			{
				string identity = ADRecipient.OrganizationUnitFromADObjectId(base.CurrentTaskContext.UserInfo.ExecutingUserId);
				this.OrganizationalUnit = new OrganizationalUnitIdParameter(identity);
			}
			base.InternalBeginProcessing();
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0003E7E4 File Offset: 0x0003C9E4
		protected override void InternalStateReset()
		{
			if (string.IsNullOrEmpty(this.Name) && string.IsNullOrEmpty(this.DisplayName))
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorNewTeamMailboxParameter), ExchangeErrorCategory.Client, null);
			}
			IList<TeamMailboxProvisioningPolicy> teamMailboxPolicies = this.GetTeamMailboxPolicies();
			if (teamMailboxPolicies == null || teamMailboxPolicies.Count == 0)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorTeamMailboxCanNotLoadPolicy), ExchangeErrorCategory.Client, null);
			}
			this.provisioningPolicy = teamMailboxPolicies[0];
			if (string.IsNullOrEmpty(this.Name))
			{
				string prefix = null;
				if (!string.IsNullOrEmpty(this.provisioningPolicy.AliasPrefix))
				{
					prefix = this.provisioningPolicy.AliasPrefix;
				}
				else if (this.provisioningPolicy.DefaultAliasPrefixEnabled)
				{
					prefix = (Datacenter.IsMicrosoftHostedOnly(true) ? "SMO-" : "SM-");
				}
				this.Name = TeamMailboxHelper.GenerateUniqueAliasForSiteMailbox(base.TenantGlobalCatalogSession, base.CurrentOrganizationId, this.DisplayName, prefix, Datacenter.IsMicrosoftHostedOnly(true), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
			}
			if (Datacenter.IsMicrosoftHostedOnly(true))
			{
				SiteMailboxAddressesTemplate siteMailboxAddressesTemplate = null;
				try
				{
					siteMailboxAddressesTemplate = SiteMailboxAddressesTemplate.GetSiteMailboxAddressesTemplate(this.ConfigurationSession, base.ProvisioningCache);
				}
				catch (ErrorSiteMailboxCannotLoadAddressTemplateException exception)
				{
					base.WriteError(exception, ExchangeErrorCategory.Client, null);
				}
				base.UserPrincipalName = RecipientTaskHelper.GenerateUniqueUserPrincipalName(base.TenantGlobalCatalogSession, this.Name, siteMailboxAddressesTemplate.UserPrincipalNameDomain, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				base.UserSpecifiedParameters["SpecificAddressTemplates"] = siteMailboxAddressesTemplate.AddressTemplates;
			}
			base.InternalStateReset();
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0003E96C File Offset: 0x0003CB6C
		protected override void PrepareUserObject(ADUser user)
		{
			TaskLogger.LogEnter();
			base.PrepareUserObject(user);
			user.SetExchangeVersion(ADUser.GetMaximumSupportedExchangeObjectVersion(RecipientTypeDetails.TeamMailbox, false));
			TaskLogger.LogExit();
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0003E994 File Offset: 0x0003CB94
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (!base.TryGetExecutingUserId(out this.executingUserId))
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorTeamMailboxCannotIdentifyTheUser), ExchangeErrorCategory.Client, null);
			}
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).CmdletInfra.SiteMailboxCheckSharePointUrlAgainstTrustedHosts.Enabled && base.Fields.IsModified("SharePointUrl"))
			{
				if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
				{
					this.CheckSharePointUrlSafetyInDatacenter(this.SharePointUrl);
					return;
				}
				if (!base.Fields.IsModified("Force"))
				{
					this.CheckSharePointUrlSafetyOnPremises(this.SharePointUrl);
				}
			}
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0003EA44 File Offset: 0x0003CC44
		protected override void InternalProcessRecord()
		{
			ADUser dataObject = this.DataObject;
			TeamMailbox teamMailbox = TeamMailbox.FromDataObject(dataObject);
			this.teamMailboxHelper = new TeamMailboxHelper(teamMailbox, base.ExchangeRunspaceConfig.ExecutingUser, base.ExchangeRunspaceConfig.ExecutingUserOrganizationId, (IRecipientSession)base.DataSession, new TeamMailboxGetDataObject<ADUser>(base.GetDataObject<ADUser>));
			TeamMailboxMembershipHelper teamMailboxMembershipHelper = new TeamMailboxMembershipHelper(teamMailbox, (IRecipientSession)base.DataSession);
			Exception ex = null;
			ADUser aduser = TeamMailboxADUserResolver.Resolve((IRecipientSession)base.DataSession, this.executingUserId, out ex);
			if (aduser == null)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorExecutingUserIsNull), ExchangeErrorCategory.Client, null);
			}
			if (base.Fields.IsModified("DisplayName"))
			{
				teamMailbox.DisplayName = this.DisplayName;
			}
			teamMailbox.SetPolicy(this.provisioningPolicy);
			base.WriteVerbose(Strings.SiteMailboxPolicySet(this.provisioningPolicy.ToString()));
			teamMailbox.SetMyRole(this.executingUserId);
			Uri sharePointUrl = this.SharePointUrl;
			SharePointMemberShip sharePointMemberShip = SharePointMemberShip.Others;
			Uri webCollectionUrl = null;
			Guid empty = Guid.Empty;
			if (base.Fields.IsModified("SharePointUrl") && !base.Fields.IsModified("Force"))
			{
				try
				{
					TeamMailboxHelper.CheckSharePointSite(SmtpAddress.Empty, ref sharePointUrl, base.ExchangeRunspaceConfig.ExecutingUser, base.ExchangeRunspaceConfig.ExecutingUserOrganizationId, aduser, out sharePointMemberShip, out webCollectionUrl, out empty);
				}
				catch (RecipientTaskException exception)
				{
					base.WriteError(exception, ExchangeErrorCategory.Client, null);
				}
			}
			teamMailbox.SharePointUrl = sharePointUrl;
			teamMailbox.SetSharePointSiteInfo(webCollectionUrl, empty);
			teamMailbox.SharePointLinkedBy = this.executingUserId;
			List<ADObjectId> list = new List<ADObjectId>();
			List<ADObjectId> list2 = new List<ADObjectId>();
			IList<ADObjectId> list3 = null;
			IList<ADObjectId> usersToRemove = null;
			if (TeamMailboxMembershipHelper.IsUserQualifiedType(aduser))
			{
				if (sharePointMemberShip == SharePointMemberShip.Owner)
				{
					list.Add(this.executingUserId);
					teamMailboxMembershipHelper.UpdateTeamMailboxUserList(teamMailbox.Owners, list, out list3, out usersToRemove);
					teamMailboxMembershipHelper.UpdateTeamMailboxUserList(teamMailbox.OwnersAndMembers, list, out list3, out usersToRemove);
				}
				else if (sharePointMemberShip == SharePointMemberShip.Member)
				{
					list2.Add(this.executingUserId);
					teamMailboxMembershipHelper.UpdateTeamMailboxUserList(teamMailbox.OwnersAndMembers, list2, out list3, out usersToRemove);
				}
			}
			Exception ex2 = null;
			try
			{
				teamMailboxMembershipHelper.SetTeamMailboxUserPermissions(list3, usersToRemove, new SecurityIdentifier[]
				{
					WellKnownSids.SiteMailboxGrantedAccessMembers
				}, false);
				if (list3 != null)
				{
					base.WriteVerbose(Strings.SiteMailboxCreatorSet(list3[0].ToString()));
				}
			}
			catch (OverflowException ex3)
			{
				ex2 = ex3;
			}
			catch (COMException ex4)
			{
				ex2 = ex4;
			}
			catch (UnauthorizedAccessException ex5)
			{
				ex2 = ex5;
			}
			catch (TransientException ex6)
			{
				ex2 = ex6;
			}
			catch (DataSourceOperationException ex7)
			{
				ex2 = ex7;
			}
			if (ex2 != null)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorSetTeamMailboxUserPermissions(teamMailbox.DisplayName, ex2.Message)), ExchangeErrorCategory.Client, null);
			}
			base.InternalProcessRecord();
			if (base.Fields.IsModified("SharePointUrl") && !base.Fields.IsModified("Force"))
			{
				try
				{
					this.teamMailboxHelper.LinkSharePointSite(sharePointUrl, true, false);
					base.WriteVerbose(Strings.SiteMailboxLinkedToSharePointSite(sharePointUrl.AbsoluteUri));
				}
				catch (RecipientTaskException exception2)
				{
					base.DataSession.Delete(this.DataObject);
					base.WriteError(exception2, ExchangeErrorCategory.Client, null);
				}
			}
			IList<Exception> list4;
			teamMailboxMembershipHelper.SetShowInMyClient(list3, usersToRemove, out list4);
			foreach (Exception ex8 in list4)
			{
				this.WriteWarning(Strings.ErrorTeamMailboxResolveUser(ex8.Message));
			}
			EnqueueResult enqueueResult = EnqueueResult.Success;
			if (this.databaseLocationInfo != null)
			{
				int num = 0;
				for (;;)
				{
					base.WriteVerbose(new LocalizedString(string.Format("Trying to send membership sync request to server {0} for MailboxGuid {1} using domain controller {2}", this.databaseLocationInfo.ServerFqdn, dataObject.ExchangeGuid, this.lastUsedDc)));
					enqueueResult = RpcClientWrapper.EnqueueTeamMailboxSyncRequest(this.databaseLocationInfo.ServerFqdn, dataObject.ExchangeGuid, QueueType.TeamMailboxMembershipSync, base.CurrentOrganizationId, "NewTMCMD_" + base.ExecutingUserIdentityName, this.lastUsedDc, SyncOption.Default);
					base.WriteVerbose(new LocalizedString(string.Format("The membership sync result is {0}", enqueueResult.Result)));
					if (enqueueResult.Result == EnqueueResultType.Successful)
					{
						goto IL_409;
					}
					if (num > 12)
					{
						break;
					}
					Thread.Sleep(5000);
					num++;
				}
				this.WriteWarning(Strings.ErrorTeamMailboxEnqueueMembershipSyncEvent(enqueueResult.ResultDetail));
				goto IL_414;
				IL_409:
				base.WriteVerbose(Strings.SiteMailboxMembershipSyncEventEnqueued);
				IL_414:
				enqueueResult = RpcClientWrapper.EnqueueTeamMailboxSyncRequest(this.databaseLocationInfo.ServerFqdn, dataObject.ExchangeGuid, QueueType.TeamMailboxDocumentSync, base.CurrentOrganizationId, "NewTMCMD_" + base.ExecutingUserIdentityName, this.lastUsedDc, SyncOption.Default);
				base.WriteVerbose(new LocalizedString(string.Format("Document sync request to server {0} for MailboxGuid {1} using domain controller {2}. The result is: {3}", new object[]
				{
					this.databaseLocationInfo.ServerFqdn,
					dataObject.ExchangeGuid,
					this.lastUsedDc,
					enqueueResult.ResultDetail
				})));
				return;
			}
			this.WriteWarning(Strings.ErrorTeamMailboxEnqueueMembershipSyncEvent("No database location information available"));
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0003EF64 File Offset: 0x0003D164
		protected override void WriteResult(ADObject result)
		{
			ADUser dataObject = (ADUser)result;
			TeamMailbox teamMailbox = TeamMailbox.FromDataObject(dataObject);
			teamMailbox.SetMyRole(this.executingUserId);
			teamMailbox.ShowInMyClient = true;
			base.WriteResult(teamMailbox);
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0003EF9C File Offset: 0x0003D19C
		private IList<TeamMailboxProvisioningPolicy> GetTeamMailboxPolicies()
		{
			OrganizationId currentOrganizationId = base.CurrentOrganizationId;
			IConfigurationSession session = this.ConfigurationSession;
			if (SharedConfiguration.IsDehydratedConfiguration(currentOrganizationId) || (SharedConfiguration.GetSharedConfigurationState(currentOrganizationId) & SharedTenantConfigurationState.Static) != SharedTenantConfigurationState.UnSupported)
			{
				ADSessionSettings adsessionSettings = null;
				SharedConfiguration sharedConfiguration = SharedConfiguration.GetSharedConfiguration(currentOrganizationId);
				if (sharedConfiguration != null)
				{
					adsessionSettings = sharedConfiguration.GetSharedConfigurationSessionSettings();
				}
				if (adsessionSettings == null)
				{
					adsessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, currentOrganizationId, base.ExecutingUserOrganizationId, false);
					adsessionSettings.IsSharedConfigChecked = true;
				}
				session = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, base.NetCredential, adsessionSettings, 732, "GetTeamMailboxPolicies", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\TeamMailbox\\NewSiteMailbox.cs");
			}
			return DefaultTeamMailboxProvisioningPolicyUtility.GetDefaultPolicies(session);
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x0003F034 File Offset: 0x0003D234
		private void CheckSharePointUrlSafetyInDatacenter(Uri proposedUrl)
		{
			bool flag = false;
			LocalizedString message = Strings.ErrorSharePointUrlNotWhitelisted;
			string[] source = new string[]
			{
				"not used in E15 CUs because they are on-premises only"
			};
			string text = this.ExtractSecondLevelDomainLowercase(proposedUrl);
			if (string.IsNullOrEmpty(text))
			{
				message = Strings.ErrorCannotParseUsefulHostnameFrom(proposedUrl.ToString());
			}
			else
			{
				flag = source.Contains(text);
				if (!flag)
				{
					Uri rootSiteUrl = Microsoft.Exchange.UnifiedGroups.SharePointUrl.GetRootSiteUrl(base.CurrentOrganizationId);
					string b = this.ExtractSecondLevelDomainLowercase(rootSiteUrl);
					flag = string.Equals(text, b, StringComparison.OrdinalIgnoreCase);
				}
			}
			if (!flag)
			{
				base.WriteError(new UrlInValidException(message), ExchangeErrorCategory.Authorization, null);
			}
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x0003F0BC File Offset: 0x0003D2BC
		private void CheckSharePointUrlSafetyOnPremises(Uri proposedUrl)
		{
			bool flag = false;
			if (proposedUrl != null)
			{
				string host = proposedUrl.Host;
				PartnerApplication[] rootOrgPartnerApplications = OAuthConfigHelper.GetRootOrgPartnerApplications();
				foreach (PartnerApplication partnerApplication in rootOrgPartnerApplications)
				{
					string a = null;
					try
					{
						base.WriteVerbose(Strings.VerboseCheckingAgainstPartnerApplicationMetadataUrl(partnerApplication.AuthMetadataUrl));
						Uri uri = new Uri(partnerApplication.AuthMetadataUrl);
						a = uri.Host;
					}
					catch
					{
					}
					if (string.Equals(a, host, StringComparison.OrdinalIgnoreCase))
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				base.WriteError(new UrlInValidException(Strings.ErrorSharePointUrlDoesNotMatchPartnerApplication), ExchangeErrorCategory.Authorization, null);
			}
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x0003F164 File Offset: 0x0003D364
		private string ExtractSecondLevelDomainLowercase(Uri uri)
		{
			string result = null;
			if (uri != null)
			{
				string[] array = uri.Host.Split(new char[]
				{
					'.'
				});
				if (array.Length >= 2)
				{
					result = string.Format("{0}.{1}", array[array.Length - 2], array[array.Length - 1]).ToLowerInvariant();
				}
			}
			return result;
		}

		// Token: 0x0400030F RID: 783
		private ADObjectId executingUserId;

		// Token: 0x04000310 RID: 784
		private TeamMailboxHelper teamMailboxHelper;

		// Token: 0x04000311 RID: 785
		private TeamMailboxProvisioningPolicy provisioningPolicy;
	}
}
