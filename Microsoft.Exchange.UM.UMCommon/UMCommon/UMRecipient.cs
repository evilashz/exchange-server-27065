using System;
using System.Diagnostics;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200017B RID: 379
	internal class UMRecipient : DisposableBase
	{
		// Token: 0x06000BFB RID: 3067 RVA: 0x0002BE9D File Offset: 0x0002A09D
		public UMRecipient(ADRecipient adrecipient)
		{
			this.Initialize(adrecipient, true);
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0002BEB9 File Offset: 0x0002A0B9
		protected UMRecipient()
		{
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x0002BECC File Offset: 0x0002A0CC
		public ADRecipient ADRecipient
		{
			get
			{
				return this.recipient;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x0002BED4 File Offset: 0x0002A0D4
		public string DisplayName
		{
			get
			{
				return this.ADRecipient.DisplayName ?? this.ADRecipient.Alias;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x0002BEF0 File Offset: 0x0002A0F0
		public bool RequiresLegacyRedirectForCallAnswering
		{
			get
			{
				return this.IsUMEnabledMailbox && this.InternalIsIncompatibleMailboxUser;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x0002BF02 File Offset: 0x0002A102
		public bool RequiresLegacyRedirectForSubscriberAccess
		{
			get
			{
				return this.IsUMEnabledMailbox && this.InternalUMMailboxPolicy != null && this.InternalUMMailboxPolicy.AllowSubscriberAccess && this.InternalIsIncompatibleMailboxUser;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x0002BF2C File Offset: 0x0002A12C
		public ADObjectId MailboxServerSite
		{
			get
			{
				if (this.InternalExchangePrincipal != null)
				{
					ServiceTopology currentServiceTopology = ServiceTopology.GetCurrentServiceTopology("f:\\15.00.1497\\sources\\dev\\um\\src\\umcommon\\UMRecipient.cs", "MailboxServerSite", 162);
					Site site = currentServiceTopology.GetSite(this.InternalExchangePrincipal.MailboxInfo.Location.ServerFqdn, "f:\\15.00.1497\\sources\\dev\\um\\src\\umcommon\\UMRecipient.cs", "MailboxServerSite", 163);
					return site.Id;
				}
				return null;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x0002BF89 File Offset: 0x0002A189
		public OrganizationId OrganizationId
		{
			get
			{
				if (this.recipient == null)
				{
					return null;
				}
				return this.recipient.OrganizationId;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x0002BFA0 File Offset: 0x0002A1A0
		public Guid TenantGuid
		{
			get
			{
				if (this.tenantGuid == null)
				{
					ExAssert.RetailAssert(this.recipient != null, "recipient is null");
					IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromADRecipient(this.recipient);
					this.tenantGuid = new Guid?(iadsystemConfigurationLookup.GetExternalDirectoryOrganizationId());
				}
				return this.tenantGuid.Value;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x0002BFF8 File Offset: 0x0002A1F8
		public VersionEnum VersionCompatibility
		{
			get
			{
				VersionEnum result = VersionEnum.Compatible;
				if (this.InternalExchangePrincipal != null)
				{
					if (this.InternalExchangePrincipal.MailboxInfo.Location.ServerVersion >= Server.E2007MinVersion && this.InternalExchangePrincipal.MailboxInfo.Location.ServerVersion < Server.E14MinVersion)
					{
						result = VersionEnum.E12Legacy;
					}
					else if (this.InternalExchangePrincipal.MailboxInfo.Location.ServerVersion >= Server.E14MinVersion && this.InternalExchangePrincipal.MailboxInfo.Location.ServerVersion < Server.E15MinVersion)
					{
						result = VersionEnum.E14Legacy;
					}
				}
				return result;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x0002C088 File Offset: 0x0002A288
		public virtual string MailAddress
		{
			get
			{
				SmtpAddress primarySmtpAddress = this.ADRecipient.PrimarySmtpAddress;
				return this.ADRecipient.PrimarySmtpAddress.ToString();
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x0002C0BA File Offset: 0x0002A2BA
		public virtual DRMProtectionOptions DRMPolicyForCA
		{
			get
			{
				if (this.InternalUMMailboxPolicy != null)
				{
					return this.InternalUMMailboxPolicy.ProtectUnauthenticatedVoiceMail;
				}
				return DRMProtectionOptions.None;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x0002C0D1 File Offset: 0x0002A2D1
		public virtual DRMProtectionOptions DRMPolicyForInterpersonal
		{
			get
			{
				if (this.InternalUMMailboxPolicy != null)
				{
					return this.InternalUMMailboxPolicy.ProtectAuthenticatedVoiceMail;
				}
				return DRMProtectionOptions.None;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0002C0E8 File Offset: 0x0002A2E8
		protected static UMRecipient.FieldCheckDelegate FieldMissingCheck
		{
			get
			{
				return UMRecipient.fieldMissingCheck;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x0002C0EF File Offset: 0x0002A2EF
		protected UMMailboxPolicy InternalUMMailboxPolicy
		{
			get
			{
				if (this.policy == null)
				{
					return null;
				}
				return this.policy.Value;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x0002C106 File Offset: 0x0002A306
		protected UMMailbox InternalADUMMailboxSettings
		{
			get
			{
				return this.adumMailboxSettings;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x0002C10E File Offset: 0x0002A30E
		public bool IsUMEnabledMailbox
		{
			get
			{
				return this.InternalADUMMailboxSettings != null && this.InternalADUMMailboxSettings.UMEnabled;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x0002C125 File Offset: 0x0002A325
		protected ExchangePrincipal InternalExchangePrincipal
		{
			get
			{
				if (this.exchangePrincipal == null)
				{
					return null;
				}
				return this.exchangePrincipal.Value;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x0002C13C File Offset: 0x0002A33C
		protected bool InternalIsIncompatibleMailboxUser
		{
			get
			{
				bool result = false;
				if (this.InternalExchangePrincipal != null)
				{
					result = !CommonUtil.IsServerCompatible(this.InternalExchangePrincipal.MailboxInfo.Location.ServerVersion);
				}
				return result;
			}
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0002C172 File Offset: 0x0002A372
		public static bool TryCreate(ADRecipient adrecipient, out UMRecipient umrecipient)
		{
			umrecipient = new UMRecipient();
			if (umrecipient.Initialize(adrecipient, false))
			{
				return true;
			}
			umrecipient.Dispose();
			umrecipient = null;
			return false;
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0002C193 File Offset: 0x0002A393
		public bool RequiresRedirectForCallAnswering()
		{
			return this.RequiresLegacyRedirectForCallAnswering;
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0002C19B File Offset: 0x0002A39B
		public bool RequiresRedirectForSubscriberAccess()
		{
			return this.RequiresLegacyRedirectForSubscriberAccess;
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0002C1A3 File Offset: 0x0002A3A3
		public override string ToString()
		{
			return this.ADRecipient.ToString();
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0002C1BC File Offset: 0x0002A3BC
		protected virtual bool Initialize(ADRecipient recipient, bool throwOnFailure)
		{
			bool flag = false;
			try
			{
				this.recipient = recipient;
				if (!this.CheckField(this.recipient, "recipient", UMRecipient.FieldMissingCheck, throwOnFailure))
				{
					return flag;
				}
				if (CommonConstants.UseDataCenterCallRouting)
				{
					if (!this.CheckField(OrganizationId.ForestWideOrgId.Equals(this.recipient.OrganizationId), "ScopeOfTheUser", (object x) => !(bool)x, throwOnFailure))
					{
						return flag;
					}
				}
				ADUser aduser = recipient as ADUser;
				this.adumMailboxSettings = ((aduser != null) ? new UMMailbox(aduser) : null);
				this.policy = new Lazy<UMMailboxPolicy>(new Func<UMMailboxPolicy>(this.InitializeMailboxPolicy));
				this.exchangePrincipal = new Lazy<ExchangePrincipal>(new Func<ExchangePrincipal>(this.InitializeExchangePrincipal));
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.Dispose();
				}
			}
			return flag;
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0002C2AC File Offset: 0x0002A4AC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<UMRecipient>(this);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0002C2B4 File Offset: 0x0002A4B4
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0002C2B8 File Offset: 0x0002A4B8
		protected bool CheckField(object fieldValue, string fieldName, UMRecipient.FieldCheckDelegate checker, bool throwOnFailure)
		{
			if (checker(fieldValue))
			{
				return true;
			}
			PIIMessage data = PIIMessage.Create(PIIType._User, this.ADRecipient);
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, data, "UMRecipient.CheckField failed!  recipient ='_User', field='{0}', throw = '{1}'", new object[]
			{
				fieldName,
				throwOnFailure
			});
			if (throwOnFailure)
			{
				throw new UMRecipientValidationException(this.ToString(), fieldName);
			}
			return false;
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0002C358 File Offset: 0x0002A558
		private ExchangePrincipal InitializeExchangePrincipal()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "InitializeExchangePrincipal: lazy initialization", new object[0]);
			ExchangePrincipal result = null;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			try
			{
				ADUser aduser = this.ADRecipient as ADUser;
				if (aduser != null && !string.IsNullOrEmpty(aduser.LegacyExchangeDN) && aduser.RecipientType == RecipientType.UserMailbox)
				{
					ADSessionSettings settings = this.InvokeWithStopwatch<ADSessionSettings>("DirectoryExtensions.ToADSessionSettings", () => aduser.OrganizationId.ToADSessionSettings());
					result = this.InvokeWithStopwatch<ExchangePrincipal>("ExchangePrincipal.FromLegacyDN", () => ExchangePrincipal.FromLegacyDN(settings, aduser.LegacyExchangeDN, RemotingOptions.AllowCrossSite));
					stopwatch.Stop();
					TimeSpan elapsed = stopwatch.Elapsed;
					CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "InitializeExchangePrincipal: object found ({0}ms)", new object[]
					{
						(int)elapsed.TotalMilliseconds
					});
					if (elapsed > UMRecipient.ExchangePrincipalWarningThreshold)
					{
						string message = string.Format("InitializeExchangePrincipal: Exchange principal {0} took {1} seconds to initialize. This is more than the warning threshold of {2} seconds", this.recipient.LegacyExchangeDN, elapsed.TotalSeconds, UMRecipient.ExchangePrincipalWarningThreshold.TotalSeconds);
						ExceptionHandling.SendWatsonWithoutDumpWithExtraData(new TimeoutException(message));
						CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "InitializeExchangePrincipal: finished generating watson", new object[0]);
					}
				}
			}
			catch (ObjectNotFoundException ex)
			{
				stopwatch.Stop();
				PIIMessage data = PIIMessage.Create(PIIType._User, this.ADRecipient);
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, data, "UMRecipient.InitializeExchangePrincipal threw an exception: {0}", new object[]
				{
					ex
				});
				this.exchangePrincipal = null;
			}
			return result;
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0002C524 File Offset: 0x0002A724
		private UMMailboxPolicy InitializeMailboxPolicy()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "InitializeMailboxPolicy: lazy initialization", new object[0]);
			IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromADRecipient(this.ADRecipient);
			return iadsystemConfigurationLookup.GetPolicyFromRecipient(this.ADRecipient);
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0002C55F File Offset: 0x0002A75F
		protected T InvokeWithStopwatch<T>(string operationName, Func<T> func)
		{
			return this.latencyStopwatch.Invoke<T>(operationName, func);
		}

		// Token: 0x04000687 RID: 1671
		private static readonly TimeSpan ExchangePrincipalWarningThreshold = TimeSpan.FromSeconds(10.0);

		// Token: 0x04000688 RID: 1672
		private static UMRecipient.FieldCheckDelegate fieldMissingCheck = (object fieldValue) => null != fieldValue;

		// Token: 0x04000689 RID: 1673
		private LatencyStopwatch latencyStopwatch = new LatencyStopwatch();

		// Token: 0x0400068A RID: 1674
		private Lazy<ExchangePrincipal> exchangePrincipal;

		// Token: 0x0400068B RID: 1675
		private UMMailbox adumMailboxSettings;

		// Token: 0x0400068C RID: 1676
		private Lazy<UMMailboxPolicy> policy;

		// Token: 0x0400068D RID: 1677
		private ADRecipient recipient;

		// Token: 0x0400068E RID: 1678
		private Guid? tenantGuid;

		// Token: 0x0200017C RID: 380
		// (Invoke) Token: 0x06000C1D RID: 3101
		protected delegate bool FieldCheckDelegate(object fieldValue);

		// Token: 0x0200017D RID: 381
		internal class Factory
		{
			// Token: 0x06000C20 RID: 3104 RVA: 0x0002C5B0 File Offset: 0x0002A7B0
			public static T FromADRecipient<T>(ADRecipient adrecipient) where T : UMRecipient
			{
				T t = default(T);
				UMRecipient umrecipient = null;
				if (UMSubscriber.TryCreate(adrecipient, out umrecipient))
				{
					t = (umrecipient as T);
				}
				else if (UMMailboxRecipient.TryCreate(adrecipient, out umrecipient))
				{
					t = (umrecipient as T);
				}
				else if (UMRecipient.TryCreate(adrecipient, out umrecipient))
				{
					t = (umrecipient as T);
				}
				if (t == null && umrecipient != null)
				{
					umrecipient.Dispose();
					umrecipient = null;
				}
				return t;
			}

			// Token: 0x06000C21 RID: 3105 RVA: 0x0002C624 File Offset: 0x0002A824
			public static T FromExtension<T>(string phone, UMDialPlan dialPlan, UMRecipient scopingUser) where T : UMRecipient
			{
				IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateUmProxyAddressLookup(dialPlan);
				ADRecipient adrecipient = iadrecipientLookup.LookupByExtensionAndDialPlan(phone, dialPlan);
				if (adrecipient == null)
				{
					return default(T);
				}
				return UMRecipient.Factory.FromADRecipient<T>(adrecipient);
			}

			// Token: 0x06000C22 RID: 3106 RVA: 0x0002C654 File Offset: 0x0002A854
			public static T FromPrincipal<T>(IExchangePrincipal principal) where T : UMRecipient
			{
				if (principal == null)
				{
					throw new InvalidPrincipalException();
				}
				IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromOrganizationId(principal.MailboxInfo.OrganizationId, null);
				ADRecipient adrecipient = iadrecipientLookup.LookupByExchangePrincipal(principal);
				return UMRecipient.Factory.FromADRecipient<T>(adrecipient);
			}
		}
	}
}
