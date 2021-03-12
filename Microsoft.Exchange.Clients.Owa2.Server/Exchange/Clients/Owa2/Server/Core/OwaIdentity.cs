using System;
using System.Globalization;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ClientAccessRules;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000A0 RID: 160
	public abstract class OwaIdentity : DisposeTrackableBase
	{
		// Token: 0x06000638 RID: 1592 RVA: 0x000125DC File Offset: 0x000107DC
		internal static OwaIdentity ResolveLogonIdentity(HttpContext httpContext, AuthZClientInfo effectiveCaller)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			OwaIdentity owaIdentity;
			if (effectiveCaller != null && effectiveCaller.ClientSecurityContext != null)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug<string>(0L, "[OwaIdentity::ResolveLogonIdentity] - Taking identity from overrideClientSecurityContext. User: {0}.", effectiveCaller.PrimarySmtpAddress);
				owaIdentity = OwaCompositeIdentity.CreateFromAuthZClientInfo(effectiveCaller);
			}
			else
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "[OwaIdentity::ResolveLogonIdentity] - Looking for identity on httpContext.");
				IIdentity userIdentity = CompositeIdentityBuilder.GetUserIdentity(httpContext);
				if (userIdentity == null)
				{
					ExTraceGlobals.CoreCallTracer.TraceError(0L, "[OwaIdentity::ResolveLogonIdentity] - httpContext was passed without an identity");
					throw new OwaIdentityException("The httpContext must have an identity associated with it.");
				}
				owaIdentity = OwaIdentity.GetOwaIdentity(userIdentity);
			}
			if (owaIdentity != null)
			{
				string logonName = owaIdentity.GetLogonName();
				ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "[OwaIdentity::ResolveLogonIdentity] Successfully resolved logon identity. Type={0}, AuthType={1}, Name={2}, IsPartial={3}", new object[]
				{
					owaIdentity.GetType(),
					owaIdentity.AuthenticationType ?? string.Empty,
					logonName ?? string.Empty,
					owaIdentity.IsPartial
				});
				return owaIdentity;
			}
			ExTraceGlobals.CoreCallTracer.TraceError(0L, "[OwaIdentity::ResolveLogonIdentity] - was unable to create the security context.");
			throw new OwaIdentityException("Cannot create security context for the specified identity.");
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x000126DC File Offset: 0x000108DC
		protected static OwaIdentity GetOwaIdentity(IIdentity identity)
		{
			CompositeIdentity compositeIdentity = identity as CompositeIdentity;
			if (compositeIdentity != null)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "[OwaIdentity::ResolveLogonIdentity] - Trying to resolve CompositeIdentity.");
				return OwaCompositeIdentity.CreateFromCompositeIdentity(compositeIdentity);
			}
			WindowsIdentity windowsIdentity = identity as WindowsIdentity;
			if (windowsIdentity != null)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "[OwaIdentity::ResolveLogonIdentity] - Trying to resolve WindowsIdentity.");
				if (windowsIdentity.IsAnonymous)
				{
					ExTraceGlobals.CoreCallTracer.TraceError(0L, "[OwaIdentity::ResolveLogonIdentity] - Windows identity cannot be anonymous.");
					throw new OwaIdentityException("Cannot create security context for anonymous windows identity.");
				}
				return OwaWindowsIdentity.CreateFromWindowsIdentity(windowsIdentity);
			}
			else
			{
				LiveIDIdentity liveIDIdentity = identity as LiveIDIdentity;
				if (liveIDIdentity != null)
				{
					ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "[OwaIdentity::ResolveLogonIdentity] - Trying to resolve LiveIDIdentity.");
					return OwaClientSecurityContextIdentity.CreateFromLiveIDIdentity(liveIDIdentity);
				}
				WindowsTokenIdentity windowsTokenIdentity = identity as WindowsTokenIdentity;
				if (windowsTokenIdentity != null)
				{
					ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "[OwaIdentity::ResolveLogonIdentity] - Trying to resolve WindowsTokenIdentity.");
					return OwaClientSecurityContextIdentity.CreateFromClientSecurityContextIdentity(windowsTokenIdentity);
				}
				OAuthIdentity oauthIdentity = identity as OAuthIdentity;
				if (oauthIdentity != null)
				{
					ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "[OwaIdentity::ResolveLogonIdentity] - Trying to resolve OAuthIdentity.");
					return OwaClientSecurityContextIdentity.CreateFromOAuthIdentity(oauthIdentity);
				}
				AdfsIdentity adfsIdentity = identity as AdfsIdentity;
				if (adfsIdentity != null)
				{
					ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "[OwaIdentity::ResolveLogonIdentity] - Trying to resolve AdfsIdentity.");
					return OwaClientSecurityContextIdentity.CreateFromAdfsIdentity(identity as AdfsIdentity);
				}
				SidBasedIdentity sidBasedIdentity = identity as SidBasedIdentity;
				if (sidBasedIdentity != null)
				{
					ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "[OwaIdentity::ResolveLogonIdentity] - Trying to resolve SidBasedIdentity.");
					return OwaClientSecurityContextIdentity.CreateFromsidBasedIdentity(sidBasedIdentity);
				}
				ExTraceGlobals.CoreCallTracer.TraceError<Type>(0L, "[OwaIdentity::ResolveLogonIdentity] - Cannot resolve unsupported identity type: {0}.", identity.GetType());
				throw new NotSupportedException(string.Format("Unexpected identity type. {0}", identity.GetType()));
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600063A RID: 1594
		public abstract WindowsIdentity WindowsIdentity { get; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x00012834 File Offset: 0x00010A34
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x0001283C File Offset: 0x00010A3C
		public OWAMiniRecipient OwaMiniRecipient { get; set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x00012848 File Offset: 0x00010A48
		public virtual string DomainName
		{
			get
			{
				if (string.IsNullOrEmpty(this.domainName))
				{
					string logonName = this.GetLogonName();
					string text;
					if (!OwaIdentity.TryParseDomainFromLogonName(logonName, out text))
					{
						ExTraceGlobals.CoreCallTracer.TraceError<string>(0L, "Unable to parse domain name from logon name '{0}'.", logonName);
						throw new OwaIdentityException(string.Format(CultureInfo.InvariantCulture, "Could not get a valid domain name from the identity '{0}'.", new object[]
						{
							logonName ?? "<NULL>"
						}));
					}
					this.domainName = text;
				}
				return this.domainName;
			}
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x000128BC File Offset: 0x00010ABC
		protected static bool IsLogonNameFullyQualified(string logonName)
		{
			string text;
			return OwaIdentity.TryParseDomainFromLogonName(logonName, out text);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x000128D4 File Offset: 0x00010AD4
		internal static bool TryParseDomainFromLogonName(string logonName, out string domainName)
		{
			domainName = null;
			if (!string.IsNullOrEmpty(logonName))
			{
				int num = logonName.IndexOf('\\');
				if (num > 0)
				{
					domainName = logonName.Substring(0, num);
				}
				else
				{
					SmtpAddress smtpAddress = new SmtpAddress(logonName);
					if (smtpAddress.IsValidAddress)
					{
						domainName = smtpAddress.Domain;
					}
				}
			}
			return !string.IsNullOrEmpty(domainName);
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00012929 File Offset: 0x00010B29
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x00012931 File Offset: 0x00010B31
		public OrganizationId UserOrganizationId { get; set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000642 RID: 1602
		public abstract SecurityIdentifier UserSid { get; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000643 RID: 1603
		public abstract string AuthenticationType { get; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000644 RID: 1604
		public abstract string UniqueId { get; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000645 RID: 1605
		public abstract bool IsPartial { get; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x0001293C File Offset: 0x00010B3C
		public virtual SmtpAddress PrimarySmtpAddress
		{
			get
			{
				if (this.OwaMiniRecipient == null)
				{
					return default(SmtpAddress);
				}
				return this.OwaMiniRecipient.PrimarySmtpAddress;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x00012968 File Offset: 0x00010B68
		public OrganizationProperties UserOrganizationProperties
		{
			get
			{
				if (this.userOrganizationProperties == null)
				{
					OWAMiniRecipient owaminiRecipient = this.GetOWAMiniRecipient();
					if (!OrganizationPropertyCache.TryGetOrganizationProperties(owaminiRecipient.OrganizationId, out this.userOrganizationProperties))
					{
						throw new OwaADObjectNotFoundException("The organization does not exist in AD. OrgId:" + owaminiRecipient.OrganizationId);
					}
				}
				return this.userOrganizationProperties;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000648 RID: 1608
		internal abstract ClientSecurityContext ClientSecurityContext { get; }

		// Token: 0x06000649 RID: 1609
		public abstract string GetLogonName();

		// Token: 0x0600064A RID: 1610
		public abstract string SafeGetRenderableName();

		// Token: 0x0600064B RID: 1611 RVA: 0x000129B3 File Offset: 0x00010BB3
		public virtual void Refresh(OwaIdentity identity)
		{
			if (identity.GetType() != base.GetType())
			{
				throw new OwaInvalidOperationException(string.Format("Type of passed in identity does not match current identity.  Expected {0} but got {1}.", base.GetType(), identity.GetType()));
			}
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x000129E4 File Offset: 0x00010BE4
		public virtual bool IsEqualsTo(OwaIdentity otherIdentity)
		{
			return otherIdentity != null && otherIdentity.UserSid.Equals(this.UserSid);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x000129FC File Offset: 0x00010BFC
		public ADRecipient CreateADRecipientBySid()
		{
			IRecipientSession recipientSession = (this.UserOrganizationId == null) ? UserContextUtilities.CreateScopedRecipientSession(true, ConsistencyMode.FullyConsistent, this.DomainName, null) : UserContextUtilities.CreateScopedRecipientSession(true, ConsistencyMode.FullyConsistent, null, this.UserOrganizationId);
			ADRecipient adrecipient = recipientSession.FindBySid(this.UserSid);
			if (adrecipient == null)
			{
				throw new OwaADUserNotFoundException(this.SafeGetRenderableName());
			}
			return adrecipient;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00012A53 File Offset: 0x00010C53
		public OWAMiniRecipient GetOWAMiniRecipient()
		{
			if (this.OwaMiniRecipient == null)
			{
				this.OwaMiniRecipient = this.CreateOWAMiniRecipientBySid();
			}
			return this.OwaMiniRecipient;
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00012A70 File Offset: 0x00010C70
		public OWAMiniRecipient CreateOWAMiniRecipientBySid()
		{
			IRecipientSession recipientSession = (this.UserOrganizationId == null) ? UserContextUtilities.CreateScopedRecipientSession(true, ConsistencyMode.FullyConsistent, this.DomainName, null) : UserContextUtilities.CreateScopedRecipientSession(true, ConsistencyMode.FullyConsistent, null, this.UserOrganizationId);
			bool flag = false;
			bool enabled = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).OwaServer.OwaClientAccessRulesEnabled.Enabled;
			if (enabled)
			{
				ClientAccessRuleCollection collection = ClientAccessRulesCache.Instance.GetCollection(this.UserOrganizationId ?? OrganizationId.ForestWideOrgId);
				flag = (collection.Count > 0);
			}
			OWAMiniRecipient owaminiRecipient = recipientSession.FindMiniRecipientBySid<OWAMiniRecipient>(this.UserSid, flag ? OWAMiniRecipientSchema.AdditionalPropertiesWithClientAccessRules : OWAMiniRecipientSchema.AdditionalProperties);
			if (owaminiRecipient == null)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<SecurityIdentifier>(0L, "OwaIdentity.CreateOWAMiniRecipientBySid: got null OWAMiniRecipient for Sid: {0}", this.UserSid);
				throw new OwaADUserNotFoundException(this.SafeGetRenderableName());
			}
			return owaminiRecipient;
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00012B3C File Offset: 0x00010D3C
		internal static OwaIdentity CreateOwaIdentityFromExplicitLogonAddress(string smtpAddress)
		{
			OwaIdentity result = OwaMiniRecipientIdentity.CreateFromProxyAddress(smtpAddress);
			ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "The request is under explicit logon: {0}", smtpAddress);
			return result;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00012B64 File Offset: 0x00010D64
		internal ExchangePrincipal CreateExchangePrincipal()
		{
			ExchangePrincipal exchangePrincipal = null;
			try
			{
				if (ExTraceGlobals.CoreTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					string text = null;
					using (WindowsIdentity current = WindowsIdentity.GetCurrent())
					{
						text = current.Name;
					}
					if (string.IsNullOrEmpty(text))
					{
						text = "<n/a>";
					}
					string arg = this.SafeGetRenderableName();
					ExTraceGlobals.CoreTracer.TraceDebug<string, string>(0L, "Using accout {0} to bind to ExchangePrincipal object for user {1}", text, arg);
				}
				exchangePrincipal = this.InternalCreateExchangePrincipal();
			}
			catch (AdUserNotFoundException innerException)
			{
				throw new OwaADUserNotFoundException(this.SafeGetRenderableName(), null, innerException);
			}
			catch (ObjectNotFoundException ex)
			{
				bool flag = false;
				DataValidationException ex2 = ex.InnerException as DataValidationException;
				if (ex2 != null)
				{
					PropertyValidationError propertyValidationError = ex2.Error as PropertyValidationError;
					if (propertyValidationError != null && propertyValidationError.PropertyDefinition == MiniRecipientSchema.Languages)
					{
						OWAMiniRecipient owaminiRecipient = this.FixCorruptOWAMiniRecipientCultureEntry();
						if (owaminiRecipient != null)
						{
							try
							{
								exchangePrincipal = ExchangePrincipal.FromMiniRecipient(owaminiRecipient);
								ExTraceGlobals.CoreTracer.TraceDebug<SecurityIdentifier>(0L, "OwaIdentity.CreateExchangePrincipal: Got ExchangePrincipal from MiniRecipient for Sid: {0}", this.UserSid);
								flag = true;
							}
							catch (ObjectNotFoundException)
							{
							}
						}
					}
				}
				if (!flag)
				{
					ExTraceGlobals.CoreTracer.TraceDebug<SecurityIdentifier, ObjectNotFoundException>(0L, "OwaIdentity.CreateExchangePrincipal: Fail to create ExchangePrincipal for Sid: {0}. Cannot recover from exception: {1}", this.UserSid, ex);
					throw ex;
				}
			}
			if (exchangePrincipal == null)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<SecurityIdentifier>(0L, "OwaIdentity.CreateExchangePrincipal: Got a null ExchangePrincipal for Sid: {0}", this.UserSid);
			}
			return exchangePrincipal;
		}

		// Token: 0x06000652 RID: 1618
		internal abstract ExchangePrincipal InternalCreateExchangePrincipal();

		// Token: 0x06000653 RID: 1619
		internal abstract MailboxSession CreateMailboxSession(ExchangePrincipal exchangePrincipal, CultureInfo cultureInfo);

		// Token: 0x06000654 RID: 1620
		internal abstract MailboxSession CreateInstantSearchMailboxSession(ExchangePrincipal exchangePrincipal, CultureInfo cultureInfo);

		// Token: 0x06000655 RID: 1621
		internal abstract MailboxSession CreateDelegateMailboxSession(ExchangePrincipal exchangePrincipal, CultureInfo cultureInfo);

		// Token: 0x06000656 RID: 1622 RVA: 0x00012CBC File Offset: 0x00010EBC
		internal OWAMiniRecipient FixCorruptOWAMiniRecipientCultureEntry()
		{
			if (ExTraceGlobals.CoreTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "User {0} has corrupt culture, setting client culture to empty", this.SafeGetRenderableName());
			}
			IRecipientSession recipientSession = (this.UserOrganizationId == null) ? UserContextUtilities.CreateScopedRecipientSession(false, ConsistencyMode.PartiallyConsistent, this.DomainName, null) : UserContextUtilities.CreateScopedRecipientSession(false, ConsistencyMode.PartiallyConsistent, null, this.UserOrganizationId);
			ADUser aduser = recipientSession.FindBySid(this.UserSid) as ADUser;
			if (aduser != null)
			{
				aduser.Languages = new MultiValuedProperty<CultureInfo>();
				if (ExTraceGlobals.CoreTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "Saving culture for User {0}, setting client culture to empty", this.SafeGetRenderableName());
				}
				recipientSession.Save(aduser);
				return recipientSession.FindMiniRecipientBySid<OWAMiniRecipient>(this.UserSid, OWAMiniRecipientSchema.AdditionalProperties);
			}
			ExTraceGlobals.CoreTracer.TraceDebug<SecurityIdentifier>(0L, "OwaIdentity.FixCorruptOWAMiniRecipientCultureEntry: got null adUser for Sid: {0}", this.UserSid);
			return null;
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00012D93 File Offset: 0x00010F93
		internal bool IsCrossForest(SecurityIdentifier masterAccountSid)
		{
			return this.UserSid != null && this.UserSid.Equals(masterAccountSid);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00012DB1 File Offset: 0x00010FB1
		protected override void InternalDispose(bool isDisposing)
		{
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00012DB3 File Offset: 0x00010FB3
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OwaIdentity>(this);
		}

		// Token: 0x04000380 RID: 896
		private string domainName;

		// Token: 0x04000381 RID: 897
		private OrganizationProperties userOrganizationProperties;
	}
}
