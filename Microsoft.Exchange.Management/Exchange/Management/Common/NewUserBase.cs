using System;
using System.Diagnostics;
using System.Globalization;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x02000127 RID: 295
	public abstract class NewUserBase : NewMailEnabledRecipientObjectTask<ADUser>
	{
		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x0002FF7D File Offset: 0x0002E17D
		// (set) Token: 0x06000A56 RID: 2646 RVA: 0x0002FF85 File Offset: 0x0002E185
		protected ExPerformanceCounter NumberofCalls
		{
			get
			{
				return this.numberofCalls;
			}
			set
			{
				this.numberofCalls = value;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x0002FF8E File Offset: 0x0002E18E
		// (set) Token: 0x06000A58 RID: 2648 RVA: 0x0002FF96 File Offset: 0x0002E196
		protected ExPerformanceCounter TotalResponseTime
		{
			get
			{
				return this.totalResponseTime;
			}
			set
			{
				this.totalResponseTime = value;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x0002FF9F File Offset: 0x0002E19F
		// (set) Token: 0x06000A5A RID: 2650 RVA: 0x0002FFA7 File Offset: 0x0002E1A7
		protected ExPerformanceCounter NumberofSuccessfulCalls
		{
			get
			{
				return this.numberofSuccessfulCalls;
			}
			set
			{
				this.numberofSuccessfulCalls = value;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0002FFB0 File Offset: 0x0002E1B0
		// (set) Token: 0x06000A5C RID: 2652 RVA: 0x0002FFB8 File Offset: 0x0002E1B8
		protected ExPerformanceCounter AverageTimeTaken
		{
			get
			{
				return this.averageTimeTaken;
			}
			set
			{
				this.averageTimeTaken = value;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x0002FFC1 File Offset: 0x0002E1C1
		// (set) Token: 0x06000A5E RID: 2654 RVA: 0x0002FFC9 File Offset: 0x0002E1C9
		protected ExPerformanceCounter AverageBaseTimeTaken
		{
			get
			{
				return this.averageBaseTimeTaken;
			}
			set
			{
				this.averageBaseTimeTaken = value;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x0002FFD2 File Offset: 0x0002E1D2
		// (set) Token: 0x06000A60 RID: 2656 RVA: 0x0002FFDA File Offset: 0x0002E1DA
		protected ExPerformanceCounter AverageTimeTakenWithCache
		{
			get
			{
				return this.averageTimeTakenWithCache;
			}
			set
			{
				this.averageTimeTakenWithCache = value;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x0002FFE3 File Offset: 0x0002E1E3
		// (set) Token: 0x06000A62 RID: 2658 RVA: 0x0002FFEB File Offset: 0x0002E1EB
		protected ExPerformanceCounter AverageBaseTimeTakenWithCache
		{
			get
			{
				return this.averageBaseTimeTakenWithCache;
			}
			set
			{
				this.averageBaseTimeTakenWithCache = value;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x0002FFF4 File Offset: 0x0002E1F4
		// (set) Token: 0x06000A64 RID: 2660 RVA: 0x0002FFFC File Offset: 0x0002E1FC
		protected ExPerformanceCounter AverageTimeTakenWithoutCache
		{
			get
			{
				return this.averageTimeTakenWithoutCache;
			}
			set
			{
				this.averageTimeTakenWithoutCache = value;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x00030005 File Offset: 0x0002E205
		// (set) Token: 0x06000A66 RID: 2662 RVA: 0x0003000D File Offset: 0x0002E20D
		protected ExPerformanceCounter AverageBaseTimeTakenWithoutCache
		{
			get
			{
				return this.averageBaseTimeTakenWithoutCache;
			}
			set
			{
				this.averageBaseTimeTakenWithoutCache = value;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x00030016 File Offset: 0x0002E216
		// (set) Token: 0x06000A68 RID: 2664 RVA: 0x0003001E File Offset: 0x0002E21E
		protected ExPerformanceCounter CacheActivePercentage
		{
			get
			{
				return this.cacheActivePercentage;
			}
			set
			{
				this.cacheActivePercentage = value;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x00030027 File Offset: 0x0002E227
		// (set) Token: 0x06000A6A RID: 2666 RVA: 0x0003002F File Offset: 0x0002E22F
		protected ExPerformanceCounter CacheActiveBasePercentage
		{
			get
			{
				return this.cacheActiveBasePercentage;
			}
			set
			{
				this.cacheActiveBasePercentage = value;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x00030038 File Offset: 0x0002E238
		// (set) Token: 0x06000A6C RID: 2668 RVA: 0x00030045 File Offset: 0x0002E245
		[Parameter]
		public string FirstName
		{
			get
			{
				return this.DataObject.FirstName;
			}
			set
			{
				this.DataObject.FirstName = value;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x00030053 File Offset: 0x0002E253
		// (set) Token: 0x06000A6E RID: 2670 RVA: 0x00030060 File Offset: 0x0002E260
		[Parameter]
		public string Initials
		{
			get
			{
				return this.DataObject.Initials;
			}
			set
			{
				this.DataObject.Initials = value;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x0003006E File Offset: 0x0002E26E
		// (set) Token: 0x06000A70 RID: 2672 RVA: 0x0003007B File Offset: 0x0002E27B
		[Parameter]
		public string LastName
		{
			get
			{
				return this.DataObject.LastName;
			}
			set
			{
				this.DataObject.LastName = value;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x00030089 File Offset: 0x0002E289
		// (set) Token: 0x06000A72 RID: 2674 RVA: 0x00030091 File Offset: 0x0002E291
		[Parameter(Mandatory = true)]
		public virtual SecureString Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x0003009A File Offset: 0x0002E29A
		// (set) Token: 0x06000A74 RID: 2676 RVA: 0x000300A7 File Offset: 0x0002E2A7
		[Parameter]
		[ValidateNotNullOrEmpty]
		public string SamAccountName
		{
			get
			{
				return this.DataObject.SamAccountName;
			}
			set
			{
				this.DataObject.SamAccountName = value;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x000300B5 File Offset: 0x0002E2B5
		// (set) Token: 0x06000A76 RID: 2678 RVA: 0x000300C2 File Offset: 0x0002E2C2
		[Parameter(Mandatory = true)]
		public virtual string UserPrincipalName
		{
			get
			{
				return this.DataObject.UserPrincipalName;
			}
			set
			{
				this.DataObject.UserPrincipalName = value;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x000300D0 File Offset: 0x0002E2D0
		// (set) Token: 0x06000A78 RID: 2680 RVA: 0x000300F1 File Offset: 0x0002E2F1
		[Parameter]
		public bool ResetPasswordOnNextLogon
		{
			get
			{
				return (bool)(base.Fields[ADUserSchema.PasswordLastSetRaw] ?? false);
			}
			set
			{
				base.Fields[ADUserSchema.PasswordLastSetRaw] = value;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x00030109 File Offset: 0x0002E309
		// (set) Token: 0x06000A7A RID: 2682 RVA: 0x00030120 File Offset: 0x0002E320
		[Parameter(Mandatory = true, ParameterSetName = "WindowsLiveID")]
		[Parameter(Mandatory = true, ParameterSetName = "ImportLiveId")]
		[Parameter(Mandatory = true, ParameterSetName = "FederatedUser")]
		[Parameter(Mandatory = true, ParameterSetName = "WindowsLiveCustomDomains")]
		public WindowsLiveId WindowsLiveID
		{
			get
			{
				return (WindowsLiveId)base.Fields["WindowsLiveID"];
			}
			set
			{
				base.Fields["WindowsLiveID"] = value;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x00030133 File Offset: 0x0002E333
		// (set) Token: 0x06000A7C RID: 2684 RVA: 0x0003013B File Offset: 0x0002E33B
		[Parameter(Mandatory = false, ParameterSetName = "EnableRoomMailboxAccount")]
		[Parameter(Mandatory = true, ParameterSetName = "MicrosoftOnlineServicesID")]
		[Parameter(Mandatory = true, ParameterSetName = "MicrosoftOnlineServicesFederatedUser")]
		public WindowsLiveId MicrosoftOnlineServicesID
		{
			get
			{
				return this.WindowsLiveID;
			}
			set
			{
				this.WindowsLiveID = value;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x00030144 File Offset: 0x0002E344
		// (set) Token: 0x06000A7E RID: 2686 RVA: 0x0003016A File Offset: 0x0002E36A
		[Parameter(Mandatory = true, ParameterSetName = "WindowsLiveCustomDomains")]
		public SwitchParameter UseExistingLiveId
		{
			get
			{
				return (SwitchParameter)(base.Fields["UseExistingLiveId"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["UseExistingLiveId"] = value;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x00030182 File Offset: 0x0002E382
		// (set) Token: 0x06000A80 RID: 2688 RVA: 0x00030199 File Offset: 0x0002E399
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveCustomDomains")]
		[Parameter(Mandatory = false, ParameterSetName = "FederatedUser")]
		[Parameter(Mandatory = false, ParameterSetName = "MicrosoftOnlineServicesFederatedUser")]
		public NetID NetID
		{
			get
			{
				return (NetID)base.Fields["NetID"];
			}
			set
			{
				base.Fields["NetID"] = value;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x000301AC File Offset: 0x0002E3AC
		// (set) Token: 0x06000A82 RID: 2690 RVA: 0x000301D2 File Offset: 0x0002E3D2
		[Parameter(Mandatory = true, ParameterSetName = "ImportLiveId")]
		public SwitchParameter ImportLiveId
		{
			get
			{
				return (SwitchParameter)(base.Fields["ImportLiveId"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ImportLiveId"] = value;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x000301EA File Offset: 0x0002E3EA
		// (set) Token: 0x06000A84 RID: 2692 RVA: 0x00030210 File Offset: 0x0002E410
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveCustomDomains")]
		public SwitchParameter BypassLiveId
		{
			get
			{
				return (SwitchParameter)(base.Fields["BypassLiveId"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["BypassLiveId"] = value;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x00030228 File Offset: 0x0002E428
		// (set) Token: 0x06000A86 RID: 2694 RVA: 0x0003024E File Offset: 0x0002E44E
		[Parameter(Mandatory = false, ParameterSetName = "FederatedUser")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveID")]
		public SwitchParameter EvictLiveId
		{
			get
			{
				return (SwitchParameter)(base.Fields["EvictLiveId"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["EvictLiveId"] = value;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x00030266 File Offset: 0x0002E466
		// (set) Token: 0x06000A88 RID: 2696 RVA: 0x0003027D File Offset: 0x0002E47D
		[Parameter(Mandatory = true, ParameterSetName = "FederatedUser")]
		[Parameter(Mandatory = true, ParameterSetName = "MicrosoftOnlineServicesFederatedUser")]
		public string FederatedIdentity
		{
			get
			{
				return (string)base.Fields["FederatedIdentity"];
			}
			set
			{
				base.Fields["FederatedIdentity"] = value;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x00030290 File Offset: 0x0002E490
		// (set) Token: 0x06000A8A RID: 2698 RVA: 0x000302A7 File Offset: 0x0002E4A7
		[Parameter(Mandatory = false)]
		public string ImmutableId
		{
			get
			{
				return (string)base.Fields[ADRecipientSchema.ImmutableId];
			}
			set
			{
				base.Fields[ADRecipientSchema.ImmutableId] = value;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x000302BA File Offset: 0x0002E4BA
		// (set) Token: 0x06000A8C RID: 2700 RVA: 0x000302DB File Offset: 0x0002E4DB
		[Parameter(Mandatory = false)]
		public virtual Capability SKUCapability
		{
			get
			{
				return (Capability)(base.Fields["SKUCapability"] ?? Capability.None);
			}
			set
			{
				base.VerifyValues<Capability>(CapabilityHelper.AllowedSKUCapabilities, value);
				base.Fields["SKUCapability"] = value;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x000302FF File Offset: 0x0002E4FF
		// (set) Token: 0x06000A8E RID: 2702 RVA: 0x0003031F File Offset: 0x0002E51F
		[Parameter(Mandatory = false)]
		public virtual MultiValuedProperty<Capability> AddOnSKUCapability
		{
			get
			{
				return (MultiValuedProperty<Capability>)(base.Fields["AddOnSKUCapability"] ?? new MultiValuedProperty<Capability>());
			}
			set
			{
				if (value != null)
				{
					base.VerifyValues<Capability>(CapabilityHelper.AllowedSKUCapabilities, value.ToArray());
				}
				base.Fields["AddOnSKUCapability"] = value;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x00030346 File Offset: 0x0002E546
		// (set) Token: 0x06000A90 RID: 2704 RVA: 0x00030367 File Offset: 0x0002E567
		[Parameter(Mandatory = false)]
		public virtual bool SKUAssigned
		{
			get
			{
				return (bool)(base.Fields[ADRecipientSchema.SKUAssigned] ?? false);
			}
			set
			{
				base.Fields[ADRecipientSchema.SKUAssigned] = value;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x0003037F File Offset: 0x0002E57F
		// (set) Token: 0x06000A92 RID: 2706 RVA: 0x0003038C File Offset: 0x0002E58C
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<CultureInfo> Languages
		{
			get
			{
				return this.DataObject.Languages;
			}
			set
			{
				this.DataObject.Languages = value;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x0003039A File Offset: 0x0002E59A
		protected virtual bool AllowBypassLiveIdWithoutWlid
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x000303A0 File Offset: 0x0002E5A0
		protected override void InternalBeginProcessing()
		{
			using (new CmdletMonitoredScope(base.CurrentTaskContext.UniqueId, "BizLogic", "NewUserBase.InternalBeginProcessing", LoggerHelper.CmdletPerfMonitors))
			{
				if (this.NumberofCalls != null && NewUserBase.counterCategoryExist)
				{
					this.NumberofCalls.Increment();
					if (this.NumberofCalls.RawValue >= 100000L)
					{
						this.NumberofCalls.RawValue = 1L;
						this.NumberofSuccessfulCalls.RawValue = 0L;
						this.TotalResponseTime.RawValue = 0L;
					}
				}
				if (this.WindowsLiveID != null && this.WindowsLiveID.NetId != null && this.WindowsLiveID.SmtpAddress != SmtpAddress.Empty)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorCannotProvideNetIDAndSmtpAddress), ExchangeErrorCategory.Client, null);
				}
				if (this.BypassLiveId && !this.AllowBypassLiveIdWithoutWlid)
				{
					if (this.NetID == null)
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorMissingNetIDWhenBypassWLID), ExchangeErrorCategory.Client, null);
					}
					else if (this.WindowsLiveID.SmtpAddress.Equals(SmtpAddress.Empty))
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorEmptyProxyAddressInWLID), ExchangeErrorCategory.Client, null);
					}
				}
				base.InternalBeginProcessing();
				if (this.NetID != null && this.WindowsLiveID.NetId != null && !this.NetID.Equals(this.WindowsLiveID.NetId))
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorNetIDValuesDoNotMatch(this.NetID.ToString(), this.WindowsLiveID.NetId.ToString())), ExchangeErrorCategory.Client, null);
				}
				if (this.NetID != null && this.WindowsLiveID.NetId == null)
				{
					this.WindowsLiveID.NetId = this.NetID;
				}
				if (this.WindowsLiveID != null && !VariantConfiguration.InvariantNoFlightingSnapshot.Global.WindowsLiveID.Enabled)
				{
					base.ThrowTerminatingError(new RecipientTaskException(Strings.ErrorEnableWindowsLiveIdForEnterpriseMailbox), ExchangeErrorCategory.Client, null);
				}
			}
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x000305D8 File Offset: 0x0002E7D8
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (this.WindowsLiveID != null)
			{
				using (new CmdletMonitoredScope(base.CurrentTaskContext.UniqueId, "BizLogic", "NewUserBase.InternalValidate", LoggerHelper.CmdletPerfMonitors))
				{
					if (this.WindowsLiveID.NetId != null && !this.BypassLiveId)
					{
						MailboxTaskHelper.IsLiveIdExists((IRecipientSession)base.DataSession, this.WindowsLiveID.SmtpAddress, this.WindowsLiveID.NetId, new Task.ErrorLoggerDelegate(base.WriteError));
					}
					MailboxTaskHelper.CheckNameAvailability(base.TenantGlobalCatalogSession, base.Name, base.RecipientContainerId, new Task.ErrorLoggerDelegate(base.WriteError));
				}
			}
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x000306B4 File Offset: 0x0002E8B4
		protected virtual void StampChangesBeforeSettingPassword()
		{
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x000306B6 File Offset: 0x0002E8B6
		protected virtual void StampChangesAfterSettingPassword()
		{
			this.DataObject.UserAccountControl = UserAccountControlFlags.NormalAccount;
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x000306C8 File Offset: 0x0002E8C8
		// (set) Token: 0x06000A99 RID: 2713 RVA: 0x000306D0 File Offset: 0x0002E8D0
		protected bool IsSetRandomPassword
		{
			get
			{
				return this.isSetRandomPassword;
			}
			set
			{
				this.isSetRandomPassword = value;
			}
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x000306DC File Offset: 0x0002E8DC
		protected virtual void PrepareUserObject(ADUser user)
		{
			TaskLogger.LogEnter();
			if (this.WindowsLiveID != null && this.WindowsLiveID.SmtpAddress != SmtpAddress.Empty)
			{
				if (base.IsDebugOn)
				{
					base.WriteDebug(Strings.DebugStartInAcceptedDomainCheck);
				}
				if (this.ShouldCheckAcceptedDomains())
				{
					RecipientTaskHelper.ValidateInAcceptedDomain(this.ConfigurationSession, base.CurrentOrganizationId, this.WindowsLiveID.SmtpAddress.Domain, new Task.ErrorLoggerDelegate(base.WriteError), base.ProvisioningCache);
				}
				if (base.IsDebugOn)
				{
					base.WriteDebug(Strings.DebugEndInAcceptedDomainCheck);
				}
				this.IsSetRandomPassword = true;
				user.WindowsLiveID = this.WindowsLiveID.SmtpAddress;
				user.UserPrincipalName = this.WindowsLiveID.SmtpAddress.ToString();
			}
			else if (VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.ValidateExternalEmailAddressInAcceptedDomain.Enabled && this.ShouldCheckAcceptedDomains())
			{
				RecipientTaskHelper.ValidateInAcceptedDomain(this.ConfigurationSession, user.OrganizationId, RecipientTaskHelper.GetDomainPartOfUserPrincalName(user.UserPrincipalName), new Task.ErrorLoggerDelegate(base.WriteError), base.ProvisioningCache);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00030804 File Offset: 0x0002EA04
		protected override void PrepareRecipientObject(ADUser user)
		{
			TaskLogger.LogEnter();
			string userPrincipalName = user.UserPrincipalName;
			base.PrepareRecipientObject(user);
			bool flag = base.Fields.Contains("SoftDeletedObject");
			if (flag && userPrincipalName != user.UserPrincipalName)
			{
				user.UserPrincipalName = userPrincipalName;
			}
			using (new CmdletMonitoredScope(base.CurrentTaskContext.UniqueId, "BizLogic", "NewUserBase.PrepareUserObject", LoggerHelper.CmdletPerfMonitors))
			{
				this.PrepareUserObject(user);
			}
			if (!string.IsNullOrEmpty(this.ImmutableId))
			{
				this.DataObject.ImmutableId = this.ImmutableId;
			}
			if (base.IsDebugOn)
			{
				base.WriteDebug(Strings.DebugStartUpnUniquenessCheck);
			}
			using (new CmdletMonitoredScope(base.CurrentTaskContext.UniqueId, "BizLogic", "RecipientTaskHelper.IsUserPrincipalNameUnique", LoggerHelper.CmdletPerfMonitors))
			{
				RecipientTaskHelper.IsUserPrincipalNameUnique(base.TenantGlobalCatalogSession, user, user.UserPrincipalName, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), ExchangeErrorCategory.ServerOperation, !flag);
			}
			if (base.IsDebugOn)
			{
				base.WriteDebug(Strings.DebugEndUpnUniquenessCheck);
			}
			if (!string.IsNullOrEmpty(user.SamAccountName))
			{
				using (new CmdletMonitoredScope(base.CurrentTaskContext.UniqueId, "BizLogic", "RecipientTaskHelper.IsSamAccountNameUnique", LoggerHelper.CmdletPerfMonitors))
				{
					RecipientTaskHelper.IsSamAccountNameUnique(base.TenantGlobalCatalogSession, user, user.SamAccountName, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), ExchangeErrorCategory.Client, !flag);
					goto IL_2C1;
				}
			}
			bool useRandomSuffix = this.WindowsLiveID != null && this.WindowsLiveID.SmtpAddress != SmtpAddress.Empty;
			if (base.IsDebugOn)
			{
				base.WriteDebug(Strings.DebugStartGeneratingUniqueSamAccountName);
			}
			using (new CmdletMonitoredScope(base.CurrentTaskContext.UniqueId, "BizLogic", "RecipientTaskHelper.PrepareRecipientObject/VariantConfiguration", LoggerHelper.CmdletPerfMonitors))
			{
				IRecipientSession[] recipientSessions = new IRecipientSession[]
				{
					base.RootOrgGlobalCatalogSession
				};
				if (VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.ServiceAccountForest.Enabled && base.CurrentOrganizationId != OrganizationId.ForestWideOrgId)
				{
					recipientSessions = new IRecipientSession[]
					{
						base.RootOrgGlobalCatalogSession,
						base.PartitionOrRootOrgGlobalCatalogSession
					};
				}
				using (new CmdletMonitoredScope(base.CurrentTaskContext.UniqueId, "BizLogic", "RecipientTaskHelper.GenerateUniqueSamAccountName", LoggerHelper.CmdletPerfMonitors))
				{
					user.SamAccountName = RecipientTaskHelper.GenerateUniqueSamAccountName(recipientSessions, user.Id.DomainId, RecipientTaskHelper.GetLocalPartOfUserPrincalName(user.UserPrincipalName), false, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), useRandomSuffix);
				}
			}
			if (base.IsDebugOn)
			{
				base.WriteDebug(Strings.DebugEndGeneratingUniqueSamAccountName);
			}
			IL_2C1:
			if (string.IsNullOrEmpty(user.Alias))
			{
				using (new CmdletMonitoredScope(base.CurrentTaskContext.UniqueId, "BizLogic", "RecipientTaskHelper.GenerateUniqueAlias", LoggerHelper.CmdletPerfMonitors))
				{
					user.Alias = RecipientTaskHelper.GenerateUniqueAlias(base.TenantGlobalCatalogSession, user.OrganizationId, string.IsNullOrEmpty(user.UserPrincipalName) ? user.SamAccountName : RecipientTaskHelper.GetLocalPartOfUserPrincalName(user.UserPrincipalName), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				}
			}
			if (base.Fields.IsModified("SKUCapability"))
			{
				user.SKUCapability = new Capability?(this.SKUCapability);
			}
			if (base.Fields.IsModified("AddOnSKUCapability"))
			{
				CapabilityHelper.SetAddOnSKUCapabilities(this.AddOnSKUCapability, user.PersistedCapabilities);
				RecipientTaskHelper.UpgradeArchiveQuotaOnArchiveAddOnSKU(user, user.PersistedCapabilities);
			}
			if (base.Fields.IsModified(ADRecipientSchema.SKUAssigned))
			{
				user.SKUAssigned = new bool?(this.SKUAssigned);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00030C18 File Offset: 0x0002EE18
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			bool flag = base.Fields.Contains("SoftDeletedObject");
			if (base.IsVerboseOn)
			{
				base.WriteVerbose(TaskVerboseStringHelper.GetSaveObjectVerboseString(this.DataObject, base.DataSession, typeof(ADUser)));
			}
			if (this.WindowsLiveID != null && this.DataObject.NetID == null)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorMissingWindowsLiveIdHandler), ExchangeErrorCategory.ServerOperation, null);
			}
			try
			{
				if (base.IsDebugOn)
				{
					base.WriteDebug(Strings.DebugStartSaveDataObject);
				}
				base.DataSession.Save(this.DataObject);
				if (base.IsDebugOn)
				{
					base.WriteDebug(Strings.DebugEndSaveDataObject);
				}
			}
			finally
			{
				base.WriteVerbose(TaskVerboseStringHelper.GetSourceVerboseString(base.DataSession));
			}
			bool flag2 = false;
			try
			{
				if (!flag)
				{
					this.StampChangesBeforeSettingPassword();
					if (this.IsSetRandomPassword)
					{
						base.WriteVerbose(Strings.VerboseSettingPassword(this.DataObject.Id.ToString()));
						MailboxTaskHelper.SetMailboxPassword((IRecipientSession)base.DataSession, this.DataObject, null, new Task.ErrorLoggerDelegate(base.WriteError));
					}
					else if (this.Password != null)
					{
						base.WriteVerbose(Strings.VerboseSettingPassword(this.DataObject.Id.ToString()));
						((IRecipientSession)base.DataSession).SetPassword(this.DataObject, this.Password);
					}
					bool flag3 = base.Fields.IsModified(ADUserSchema.PasswordLastSetRaw) ? this.ResetPasswordOnNextLogon : this.DataObject.ResetPasswordOnNextLogon;
					bool bypassModerationCheck = this.DataObject.BypassModerationCheck;
					ADObjectId id = this.DataObject.Id;
					using (new CmdletMonitoredScope(base.CurrentTaskContext.UniqueId, "BizLogic", "DataSession.Read<ADUser>", LoggerHelper.CmdletPerfMonitors))
					{
						this.DataObject = (ADUser)base.DataSession.Read<ADUser>(this.DataObject.Identity);
					}
					if (this.DataObject == null || this.DataObject.Id == null)
					{
						string id2 = (id == null) ? string.Empty : id.ToString();
						base.WriteError(new RecipientTaskException(Strings.ErrorCreatedUserNotExist(id2)), ExchangeErrorCategory.ServerOperation, null);
					}
					this.DataObject.BypassModerationCheck = bypassModerationCheck;
					this.DataObject[ADUserSchema.PasswordLastSetRaw] = new long?(flag3 ? 0L : -1L);
					this.StampChangesAfterSettingPassword();
				}
				base.InternalProcessRecord();
				flag2 = !base.HasErrors;
			}
			finally
			{
				if (!flag2 && this.DataObject != null && this.DataObject.Id != null && !flag)
				{
					try
					{
						base.WriteVerbose(TaskVerboseStringHelper.GetDeleteObjectVerboseString(this.DataObject.Id, base.DataSession, typeof(ADUser)));
						base.DataSession.Delete(this.DataObject);
					}
					catch (DataSourceTransientException innerException)
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorRemoveUserFailed(this.DataObject.Id.ToString()), innerException), ExchangeErrorCategory.ServerTransient, null);
					}
					finally
					{
						base.WriteVerbose(TaskVerboseStringHelper.GetSourceVerboseString(base.DataSession));
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00030FAC File Offset: 0x0002F1AC
		protected override void InternalEndProcessing()
		{
			TaskLogger.LogEnter();
			using (new CmdletMonitoredScope(base.CurrentTaskContext.UniqueId, "BizLogic", "NewUserBase.InternalEndProcessing", LoggerHelper.CmdletPerfMonitors))
			{
				base.InternalEndProcessing();
				this.timer.Stop();
				if (!base.HasErrors && this.TotalResponseTime != null && this.AverageTimeTaken != null && this.AverageBaseTimeTaken != null && this.NumberofSuccessfulCalls != null && this.AverageTimeTakenWithCache != null && this.AverageBaseTimeTakenWithCache != null && this.AverageTimeTakenWithoutCache != null && this.AverageBaseTimeTakenWithoutCache != null && this.CacheActivePercentage != null && this.CacheActiveBasePercentage != null && NewUserBase.counterCategoryExist)
				{
					this.AverageTimeTaken.IncrementBy(this.timer.ElapsedTicks);
					this.TotalResponseTime.IncrementBy(this.timer.ElapsedMilliseconds);
					this.AverageBaseTimeTaken.Increment();
					this.NumberofSuccessfulCalls.Increment();
					this.CacheActiveBasePercentage.Increment();
					if (base.ProvisioningCache.Enabled)
					{
						this.AverageTimeTakenWithCache.IncrementBy(this.timer.ElapsedTicks);
						this.AverageBaseTimeTakenWithCache.Increment();
						this.CacheActivePercentage.Increment();
					}
					else
					{
						this.AverageTimeTakenWithoutCache.IncrementBy(this.timer.ElapsedTicks);
						this.AverageBaseTimeTakenWithoutCache.Increment();
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04000561 RID: 1377
		private static readonly Guid EtsGroupSIDCacheKey = new Guid("A0AAE3BE-749D-401e-8711-0CE7DAC9B4D1");

		// Token: 0x04000562 RID: 1378
		private SecureString password;

		// Token: 0x04000563 RID: 1379
		private bool isSetRandomPassword;

		// Token: 0x04000564 RID: 1380
		private static bool counterCategoryExist = PerformanceCounterCategory.Exists("MSExchange Provisioning");

		// Token: 0x04000565 RID: 1381
		private Stopwatch timer = new Stopwatch();

		// Token: 0x04000566 RID: 1382
		private ExPerformanceCounter numberofCalls;

		// Token: 0x04000567 RID: 1383
		private ExPerformanceCounter numberofSuccessfulCalls;

		// Token: 0x04000568 RID: 1384
		private ExPerformanceCounter averageTimeTaken;

		// Token: 0x04000569 RID: 1385
		private ExPerformanceCounter averageBaseTimeTaken;

		// Token: 0x0400056A RID: 1386
		private ExPerformanceCounter averageTimeTakenWithCache;

		// Token: 0x0400056B RID: 1387
		private ExPerformanceCounter averageBaseTimeTakenWithCache;

		// Token: 0x0400056C RID: 1388
		private ExPerformanceCounter averageTimeTakenWithoutCache;

		// Token: 0x0400056D RID: 1389
		private ExPerformanceCounter averageBaseTimeTakenWithoutCache;

		// Token: 0x0400056E RID: 1390
		private ExPerformanceCounter totalResponseTime;

		// Token: 0x0400056F RID: 1391
		private ExPerformanceCounter cacheActivePercentage;

		// Token: 0x04000570 RID: 1392
		private ExPerformanceCounter cacheActiveBasePercentage;
	}
}
