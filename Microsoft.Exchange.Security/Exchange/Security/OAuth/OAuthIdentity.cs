using System;
using System.Linq;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Security.PartnerToken;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000DA RID: 218
	internal class OAuthIdentity : GenericIdentity, IOrganizationScopedIdentity, IIdentity
	{
		// Token: 0x06000772 RID: 1906 RVA: 0x00034B02 File Offset: 0x00032D02
		private OAuthIdentity(OrganizationId organizationId, OAuthApplication application, OAuthActAsUser actAsUser) : base(application.Id, Constants.BearerAuthenticationType)
		{
			this.OrganizationId = organizationId;
			this.OAuthApplication = application;
			this.ActAsUser = actAsUser;
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x00034B2C File Offset: 0x00032D2C
		public override string Name
		{
			get
			{
				if (this.IsAppOnly)
				{
					return this.OAuthApplication.Id;
				}
				if (!this.ActAsUser.IsUserVerified)
				{
					return string.Format("<unverified>{0}", this.ActAsUser);
				}
				if (this.ActAsUser.Sid == null)
				{
					return string.Format("<user w/o sid>{0}", this.ActAsUser);
				}
				return this.ActAsUser.Sid.Value;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x00034B9F File Offset: 0x00032D9F
		// (set) Token: 0x06000775 RID: 1909 RVA: 0x00034BA7 File Offset: 0x00032DA7
		public OrganizationId OrganizationId { get; private set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x00034BB0 File Offset: 0x00032DB0
		// (set) Token: 0x06000777 RID: 1911 RVA: 0x00034BB8 File Offset: 0x00032DB8
		public OAuthApplication OAuthApplication { get; private set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x00034BC1 File Offset: 0x00032DC1
		// (set) Token: 0x06000779 RID: 1913 RVA: 0x00034BC9 File Offset: 0x00032DC9
		public OAuthActAsUser ActAsUser { get; internal set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x00034BD2 File Offset: 0x00032DD2
		// (set) Token: 0x0600077B RID: 1915 RVA: 0x00034BDA File Offset: 0x00032DDA
		public bool IsAuthenticatedAtBackend { get; internal set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x00034BE3 File Offset: 0x00032DE3
		public bool IsOfficeExtension
		{
			get
			{
				return this.OAuthApplication.IsOfficeExtension;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x00034BF0 File Offset: 0x00032DF0
		public OfficeExtensionInfo OfficeExtension
		{
			get
			{
				return this.OAuthApplication.OfficeExtension;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x00034C00 File Offset: 0x00032E00
		public bool IsKnownFromSameOrgExchange
		{
			get
			{
				return this.OAuthApplication.IsFromSameOrgExchange != null && this.OAuthApplication.IsFromSameOrgExchange.Value;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x00034C37 File Offset: 0x00032E37
		// (set) Token: 0x06000780 RID: 1920 RVA: 0x00034C3F File Offset: 0x00032E3F
		public string ExtraLoggingInfo { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x00034C48 File Offset: 0x00032E48
		public bool IsAppOnly
		{
			get
			{
				return this.ActAsUser == null;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x00034C54 File Offset: 0x00032E54
		public ADRecipient ADRecipient
		{
			get
			{
				if (this.adRecipient == null)
				{
					ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.OrganizationId);
					IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 175, "ADRecipient", "f:\\15.00.1497\\sources\\dev\\Security\\src\\Authentication\\OAuth\\OAuthIdentity.cs");
					this.adRecipient = tenantOrRootOrgRecipientSession.FindBySid(this.ActAsUser.Sid);
				}
				return this.adRecipient;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x00034CAF File Offset: 0x00032EAF
		OrganizationId IOrganizationScopedIdentity.OrganizationId
		{
			get
			{
				return this.OrganizationId;
			}
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00034CB8 File Offset: 0x00032EB8
		IStandardBudget IOrganizationScopedIdentity.AcquireBudget()
		{
			if (!this.IsAppOnly && !(this.ActAsUser.Sid == null))
			{
				return StandardBudget.Acquire(this.ActAsUser.Sid, BudgetType.Ews, true, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.OrganizationId));
			}
			if (this.IsKnownFromSameOrgExchange)
			{
				return StandardBudget.Acquire(new UnthrottledBudgetKey("cross-premise freebusy", BudgetType.Ews));
			}
			return StandardBudget.Acquire(new TenantBudgetKey(this.OrganizationId, BudgetType.Ews));
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00034D28 File Offset: 0x00032F28
		public static OAuthIdentity Create(OrganizationId organizationId, OAuthApplication application, OAuthActAsUser actAsUser)
		{
			OAuthCommon.VerifyNonNullArgument("organizationId", organizationId);
			OAuthCommon.VerifyNonNullArgument("application", application);
			return new OAuthIdentity(organizationId, application, actAsUser);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00034D4C File Offset: 0x00032F4C
		public static OAuthIdentity Create(OrganizationId organizationId, OAuthApplication application, MiniRecipient recipient)
		{
			OAuthCommon.VerifyNonNullArgument("organizationId", organizationId);
			OAuthCommon.VerifyNonNullArgument("application", application);
			return new OAuthIdentity(organizationId, application, OAuthActAsUser.CreateFromMiniRecipient(organizationId, recipient));
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00034D84 File Offset: 0x00032F84
		public CommonAccessToken ToCommonAccessToken(int targetServerVersion)
		{
			if (FaultInjection.TraceTest<bool>((FaultInjection.LIDs)3011915069U))
			{
				throw new InvalidOAuthTokenException(OAuthErrors.TestOnlyExceptionDuringOAuthCATGeneration, null, null);
			}
			if (OAuthIdentity.EnforceV1Token.Value || targetServerVersion < OAuthTokenAccessor.MinVersion || FaultInjection.TraceTest<bool>((FaultInjection.LIDs)3481677117U))
			{
				return this.ToCommonAccessTokenVersion1();
			}
			return this.ToCommonAccessTokenVersion2();
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00034DD7 File Offset: 0x00032FD7
		public CommonAccessToken ToCommonAccessTokenVersion1()
		{
			return OAuthIdentitySerializer.ConvertToCommonAccessToken(this);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00034DDF File Offset: 0x00032FDF
		public CommonAccessToken ToCommonAccessTokenVersion2()
		{
			return OAuthTokenAccessor.Create(this).GetToken();
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00034DEC File Offset: 0x00032FEC
		public IIdentity ConvertIdentityIfNeed()
		{
			bool flag = this.OAuthApplication.ApplicationType == OAuthApplicationType.V1App && this.OAuthApplication.V1ProfileApp != null && this.OAuthApplication == Constants.IdTokenApplication;
			bool flag2 = this.OAuthApplication.ApplicationType == OAuthApplicationType.V1App && OAuthGrant.ExtractKnownGrants(this.OAuthApplication.V1ProfileApp.Scope).Contains("user_impersonation");
			bool flag3 = this.OAuthApplication.ApplicationType == OAuthApplicationType.CallbackApp && OAuthGrant.ExtractKnownGrants(this.OAuthApplication.OfficeExtension.Scope).Contains("user_impersonation");
			if (flag2 || flag || flag3)
			{
				return new SidBasedIdentity(this.ActAsUser.UserPrincipalName, this.ActAsUser.GetMasterAccountSidIfAvailable().Value, (this.ActAsUser.WindowsLiveID != SmtpAddress.Empty) ? this.ActAsUser.WindowsLiveID.ToString() : this.ActAsUser.UserPrincipalName, Constants.BearerAuthenticationType, this.OrganizationId.PartitionId.ToString())
				{
					UserOrganizationId = this.OrganizationId
				};
			}
			if (OAuthIdentity.RehydrateSidOAuthIdentity.Value)
			{
				ExTraceGlobals.BackendRehydrationTracer.TraceDebug(0L, "[OAuthAuthenticator::InternalRehydrate] Convert OAuthIdentity to SidOAuthIdentity.");
				return SidOAuthIdentity.Create(this);
			}
			return this;
		}

		// Token: 0x04000715 RID: 1813
		private static readonly BoolAppSettingsEntry EnforceV1Token = new BoolAppSettingsEntry("OAuthHttpModule.EnforceV1Token", false, ExTraceGlobals.OAuthTracer);

		// Token: 0x04000716 RID: 1814
		private static BoolAppSettingsEntry RehydrateSidOAuthIdentity = new BoolAppSettingsEntry("OAuthAuthenticator.RehydrateSidOAuthIdentity", false, ExTraceGlobals.OAuthTracer);

		// Token: 0x04000717 RID: 1815
		private ADRecipient adRecipient;
	}
}
