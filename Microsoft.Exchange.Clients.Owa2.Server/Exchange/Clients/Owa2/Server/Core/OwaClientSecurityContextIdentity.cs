using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000A1 RID: 161
	public class OwaClientSecurityContextIdentity : OwaIdentity
	{
		// Token: 0x0600065B RID: 1627 RVA: 0x00012DC4 File Offset: 0x00010FC4
		private OwaClientSecurityContextIdentity(ClientSecurityContext clientSecurityContext, string logonName, string authenticationType, OrganizationId userOrganizationId)
		{
			if (clientSecurityContext == null)
			{
				throw new ArgumentNullException("clientSecurityContext");
			}
			if (string.IsNullOrEmpty(logonName))
			{
				throw new ArgumentNullException("logonName", "logonName cannot be null or empty.");
			}
			if (userOrganizationId == null && !OwaIdentity.IsLogonNameFullyQualified(logonName))
			{
				throw new ArgumentException("logonName", string.Format(CultureInfo.InvariantCulture, "'{0}' is not a valid logon name.", new object[]
				{
					logonName
				}));
			}
			if (string.IsNullOrEmpty(authenticationType))
			{
				throw new ArgumentNullException("authenticationType", "authenticationType cannot be null or empty.");
			}
			this.logonName = logonName;
			this.authenticationType = authenticationType;
			base.UserOrganizationId = userOrganizationId;
			if (!SyncUtilities.IsDatacenterMode())
			{
				this.clientSecurityContext = clientSecurityContext;
				OWAMiniRecipient owaminiRecipient = base.GetOWAMiniRecipient();
				if (owaminiRecipient != null && owaminiRecipient.MasterAccountSid != null)
				{
					try
					{
						this.clientSecurityContext = OwaClientSecurityContextIdentity.TokenMunger.MungeToken(clientSecurityContext, OrganizationId.ForestWideOrgId);
						return;
					}
					catch (TokenMungingException ex)
					{
						ExTraceGlobals.CoreCallTracer.TraceError(0L, "OwaClientSecurityContextIdentity.TokenMunger.MungeToken for LogonName='{0}', AuthenticationType='{1}', UserOrgId='{2}' failed with exception: {3}", new object[]
						{
							this.logonName,
							this.authenticationType,
							base.UserOrganizationId,
							ex.Message
						});
					}
				}
			}
			this.clientSecurityContext = clientSecurityContext.Clone();
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x00012F00 File Offset: 0x00011100
		public override bool IsPartial
		{
			get
			{
				return this.clientSecurityContext == null;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x00012F0B File Offset: 0x0001110B
		public override WindowsIdentity WindowsIdentity
		{
			get
			{
				throw new OwaNotSupportedException("WindowsIdentity");
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x00012F17 File Offset: 0x00011117
		public override SecurityIdentifier UserSid
		{
			get
			{
				return this.clientSecurityContext.UserSid;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x00012F24 File Offset: 0x00011124
		public override string UniqueId
		{
			get
			{
				return this.UserSid.ToString();
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x00012F31 File Offset: 0x00011131
		public override string AuthenticationType
		{
			get
			{
				return this.authenticationType;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x00012F39 File Offset: 0x00011139
		internal override ClientSecurityContext ClientSecurityContext
		{
			get
			{
				return this.clientSecurityContext;
			}
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00012F41 File Offset: 0x00011141
		public override string GetLogonName()
		{
			return this.logonName;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00012F49 File Offset: 0x00011149
		public override string SafeGetRenderableName()
		{
			return this.logonName;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00012F51 File Offset: 0x00011151
		internal static OwaClientSecurityContextIdentity CreateFromClientSecurityContext(ClientSecurityContext clientSecurityContext, string logonName, string authenticationType)
		{
			return new OwaClientSecurityContextIdentity(clientSecurityContext, logonName, authenticationType, null);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00012F5C File Offset: 0x0001115C
		internal static OwaClientSecurityContextIdentity CreateFromClientSecurityContextIdentity(ClientSecurityContextIdentity cscIdentity)
		{
			if (cscIdentity == null)
			{
				throw new ArgumentNullException("cscIdentity");
			}
			return OwaClientSecurityContextIdentity.InternalCreateFromClientSecurityContextIdentity(cscIdentity, cscIdentity.Name, null);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00012F7C File Offset: 0x0001117C
		internal static OwaClientSecurityContextIdentity CreateFromOAuthIdentity(OAuthIdentity oauthIdentity)
		{
			if (oauthIdentity == null)
			{
				throw new ArgumentNullException("oauthIdentity");
			}
			ExAssert.RetailAssert(!oauthIdentity.IsAppOnly, "IsApplyOnly cannot be null in OAuthIdentity.");
			ExAssert.RetailAssert(oauthIdentity.ActAsUser != null, "ActAsUser cannot be null in OAuthIdentity.");
			string partitionId = string.Empty;
			if (!(oauthIdentity.OrganizationId == null) && !(oauthIdentity.OrganizationId.PartitionId == null))
			{
				partitionId = oauthIdentity.OrganizationId.PartitionId.ToString();
			}
			SidBasedIdentity cscIdentity = new SidBasedIdentity(oauthIdentity.ActAsUser.UserPrincipalName, oauthIdentity.ActAsUser.Sid.Value, oauthIdentity.ActAsUser.UserPrincipalName, oauthIdentity.AuthenticationType, partitionId)
			{
				UserOrganizationId = oauthIdentity.OrganizationId
			};
			return OwaClientSecurityContextIdentity.InternalCreateFromClientSecurityContextIdentity(cscIdentity, oauthIdentity.ActAsUser.UserPrincipalName, null);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00013050 File Offset: 0x00011250
		internal static OwaClientSecurityContextIdentity CreateFromLiveIDIdentity(LiveIDIdentity liveIDIdentity)
		{
			if (liveIDIdentity == null)
			{
				throw new ArgumentNullException("liveIDIdentity");
			}
			return OwaClientSecurityContextIdentity.InternalCreateFromClientSecurityContextIdentity(liveIDIdentity, liveIDIdentity.MemberName, liveIDIdentity.UserOrganizationId);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00013080 File Offset: 0x00011280
		internal static OwaClientSecurityContextIdentity CreateFromAdfsIdentity(AdfsIdentity adfsIdentity)
		{
			if (adfsIdentity == null)
			{
				throw new ArgumentNullException("adfsIdentity");
			}
			return OwaClientSecurityContextIdentity.InternalCreateFromClientSecurityContextIdentity(adfsIdentity, adfsIdentity.MemberName, adfsIdentity.UserOrganizationId);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x000130B0 File Offset: 0x000112B0
		internal static OwaClientSecurityContextIdentity CreateFromsidBasedIdentity(SidBasedIdentity sidBasedIdentity)
		{
			if (sidBasedIdentity == null)
			{
				throw new ArgumentNullException("sidBasedIdentity");
			}
			return OwaClientSecurityContextIdentity.InternalCreateFromClientSecurityContextIdentity(sidBasedIdentity, sidBasedIdentity.MemberName, sidBasedIdentity.UserOrganizationId);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x000130E0 File Offset: 0x000112E0
		private static OwaClientSecurityContextIdentity InternalCreateFromClientSecurityContextIdentity(ClientSecurityContextIdentity cscIdentity, string logonName, OrganizationId userOrganizationId = null)
		{
			SidBasedIdentity sidBasedIdentity = cscIdentity as SidBasedIdentity;
			if (sidBasedIdentity != null)
			{
				OwaClientSecurityContextIdentity.PrePopulateUserGroupSids(sidBasedIdentity);
			}
			OwaClientSecurityContextIdentity result;
			try
			{
				using (ClientSecurityContext clientSecurityContext = cscIdentity.CreateClientSecurityContext())
				{
					result = new OwaClientSecurityContextIdentity(clientSecurityContext, logonName, cscIdentity.AuthenticationType, userOrganizationId);
				}
			}
			catch (AuthzException ex)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string, string, AuthzException>(0L, "OwaClientSecurityContextIdentity.CreateFromClientSecurityContextIdentity for ClientSecurityContextIdentity.Name={0} ClientSecurityContextIdentity.AuthenticationType={1} failed with exception: {2}", cscIdentity.Name, cscIdentity.AuthenticationType, ex);
				if (ex.InnerException is Win32Exception)
				{
					throw new OwaIdentityException("There was a problem creating the Client Security Context.", ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00013178 File Offset: 0x00011378
		internal override ExchangePrincipal InternalCreateExchangePrincipal()
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug<string>(0L, "OwaClientSecurityContextIdentity.CreateExchangePrincipal Creating scoped AD session for {0}", (base.UserOrganizationId == null) ? this.DomainName : base.UserOrganizationId.ToString());
			bool flag = HttpContext.Current != null && UserAgentUtilities.IsMonitoringRequest(HttpContext.Current.Request.UserAgent);
			ExchangePrincipal result;
			try
			{
				ADSessionSettings sessionSettings = (base.UserOrganizationId == null) ? UserContextUtilities.CreateScopedSessionSettings(this.DomainName, null) : UserContextUtilities.CreateScopedSessionSettings(null, base.UserOrganizationId);
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, true, ConsistencyMode.IgnoreInvalid, null, sessionSettings, 417, "InternalCreateExchangePrincipal", "f:\\15.00.1497\\sources\\dev\\clients\\src\\Owa2\\Server\\Core\\common\\OwaClientSecurityContextIdentity.cs");
				if (flag)
				{
					TimeSpan value = TimeSpan.FromSeconds((double)OwaClientSecurityContextIdentity.ADTimeoutForExchangePrincipalInstantiation.Value);
					ExTraceGlobals.CoreCallTracer.TraceDebug<double>(0L, "OwaClientSecurityContextIdentity.CreateExchangePrincipal Reduced AD timeout to {0} seconds", value.TotalSeconds);
					tenantOrRootOrgRecipientSession.ClientSideSearchTimeout = new TimeSpan?(value);
				}
				ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "OwaClientSecurityContextIdentity.CreateExchangePrincipal Calling ExchangePrincipal.FromUserSid");
				try
				{
					result = ExchangePrincipal.FromUserSid(tenantOrRootOrgRecipientSession, this.UserSid);
				}
				catch (UserHasNoMailboxException ex)
				{
					ADUser aduser = tenantOrRootOrgRecipientSession.FindBySid(this.UserSid) as ADUser;
					ex.Data.Add("PrimarySmtpAddress", this.logonName);
					if (aduser == null)
					{
						throw;
					}
					if (aduser.RecipientType == RecipientType.MailUser && aduser.SKUAssigned != true)
					{
						throw new OwaUserHasNoMailboxAndNoLicenseAssignedException(ex.Message, ex.InnerException);
					}
					throw;
				}
			}
			catch (Exception ex2)
			{
				OwaDiagnostics.Logger.LogEvent(ClientsEventLogConstants.Tuple_OwaFailedToCreateExchangePrincipal, string.Empty, new object[]
				{
					this.UserSid,
					flag,
					ex2
				});
				throw;
			}
			return result;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00013360 File Offset: 0x00011560
		internal override MailboxSession CreateMailboxSession(ExchangePrincipal exchangePrincipal, CultureInfo cultureInfo)
		{
			return this.CreateMailboxSession(exchangePrincipal, cultureInfo, "Client=OWA;Action=ViaProxy");
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001336F File Offset: 0x0001156F
		internal override MailboxSession CreateInstantSearchMailboxSession(ExchangePrincipal exchangePrincipal, CultureInfo cultureInfo)
		{
			if (exchangePrincipal.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox)
			{
				return this.CreateDelegateMailboxSession(exchangePrincipal, cultureInfo, "Client=OWA;Action=InstantSearch");
			}
			return this.CreateMailboxSession(exchangePrincipal, cultureInfo, "Client=OWA;Action=InstantSearch");
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x000133A0 File Offset: 0x000115A0
		internal MailboxSession CreateMailboxSession(ExchangePrincipal exchangePrincipal, CultureInfo cultureInfo, string userContextString)
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "OwaClientSecurityContextIdentity.CreateMailboxSession");
			MailboxSession result;
			try
			{
				MailboxSession mailboxSession = MailboxSession.Open(exchangePrincipal, this.clientSecurityContext, cultureInfo, userContextString);
				result = mailboxSession;
			}
			catch (AccessDeniedException innerException)
			{
				throw new OwaExplicitLogonException("User has no access rights to the mailbox", "ErrorExplicitLogonAccessDenied", innerException);
			}
			return result;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x000133F4 File Offset: 0x000115F4
		internal override MailboxSession CreateDelegateMailboxSession(ExchangePrincipal exchangePrincipal, CultureInfo cultureInfo)
		{
			return this.CreateDelegateMailboxSession(exchangePrincipal, cultureInfo, "Client=OWA;Action=ViaProxy");
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00013404 File Offset: 0x00011604
		internal MailboxSession CreateDelegateMailboxSession(ExchangePrincipal exchangePrincipal, CultureInfo cultureInfo, string userContextString)
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "OwaClientSecurityContextIdentity.CreateMailboxSession");
			MailboxSession result;
			try
			{
				IADOrgPerson iadorgPerson = base.CreateADRecipientBySid() as IADOrgPerson;
				if (iadorgPerson == null)
				{
					throw new OwaExplicitLogonException("User do not have representation in current forest", null);
				}
				MailboxSession mailboxSession = MailboxSession.OpenWithBestAccess(exchangePrincipal, iadorgPerson, this.clientSecurityContext, cultureInfo, userContextString);
				result = mailboxSession;
			}
			catch (AccessDeniedException innerException)
			{
				throw new OwaExplicitLogonException("User has no access rights to the mailbox", Strings.GetLocalizedString(882888134), innerException);
			}
			return result;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001347C File Offset: 0x0001167C
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing && this.clientSecurityContext != null)
			{
				this.clientSecurityContext.Dispose();
				this.clientSecurityContext = null;
			}
			base.InternalDispose(isDisposing);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x000134A2 File Offset: 0x000116A2
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OwaClientSecurityContextIdentity>(this);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x000134AC File Offset: 0x000116AC
		private static void PrePopulateUserGroupSids(SidBasedIdentity sidBasedIdentity)
		{
			if (sidBasedIdentity.PrepopulatedGroupSidIds == null || sidBasedIdentity.PrepopulatedGroupSidIds.Count<string>() <= 0)
			{
				ExTraceGlobals.CoreCallTracer.TraceDebug<string>(0L, "Attempting to prepopulate group SIDS for user '{0}'.", sidBasedIdentity.Sid.Value);
				if (sidBasedIdentity.UserOrganizationId != null)
				{
					List<string> list = null;
					IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, true, ConsistencyMode.IgnoreInvalid, null, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(sidBasedIdentity.UserOrganizationId), 647, "PrePopulateUserGroupSids", "f:\\15.00.1497\\sources\\dev\\clients\\src\\Owa2\\Server\\Core\\common\\OwaClientSecurityContextIdentity.cs");
					if (tenantOrRootOrgRecipientSession != null)
					{
						ExTraceGlobals.CoreCallTracer.TraceDebug<string>(0L, "OwaClientSecurityContextIdentity.GetUserGroupSidIds created IRecipientSession instance for user '{0}'.", sidBasedIdentity.Sid.Value);
						ADRecipient adrecipient = tenantOrRootOrgRecipientSession.FindBySid(sidBasedIdentity.Sid);
						if (adrecipient != null)
						{
							ExTraceGlobals.CoreCallTracer.TraceDebug<string, SmtpAddress, string>(0L, "OwaClientSecurityContextIdentity.GetUserGroupSidIds fetched ADRecipient instance with DisplaName '{0}' and PrimarySmtpAddress '{1}' for user '{2}'.", adrecipient.DisplayName, adrecipient.PrimarySmtpAddress, sidBasedIdentity.Sid.Value);
							list = tenantOrRootOrgRecipientSession.GetTokenSids(adrecipient, AssignmentMethod.S4U);
						}
						else
						{
							ExTraceGlobals.CoreCallTracer.TraceError<string>(0L, "OwaClientSecurityContextIdentity.GetUserGroupSidIds was unable to get ADRecipient instance for user '{0}'.", sidBasedIdentity.Sid.Value);
						}
					}
					else
					{
						ExTraceGlobals.CoreCallTracer.TraceError<string>(0L, "OwaClientSecurityContextIdentity.GetUserGroupSidIds was unable to get IRecipientSession instance for user '{0}'.", sidBasedIdentity.Sid.Value);
					}
					if (list == null)
					{
						ExTraceGlobals.CoreCallTracer.TraceError<string>(0L, "OwaClientSecurityContextIdentity.GetUserGroupSidIds was unable to find any group SIDs for user '{0}'.", sidBasedIdentity.Sid.Value);
						return;
					}
					ExTraceGlobals.CoreCallTracer.TraceDebug<string, string>(0L, "Prepopulating User group SIds '{0}', for user '{1}'.", string.Join(", ", list), sidBasedIdentity.Sid.Value);
					sidBasedIdentity.PrepopulateGroupSidIds(list);
				}
			}
		}

		// Token: 0x04000384 RID: 900
		public const string ErrorMessageKeyForUserSmtpAddress = "PrimarySmtpAddress";

		// Token: 0x04000385 RID: 901
		private static readonly IntAppSettingsEntry ADTimeoutForExchangePrincipalInstantiation = new IntAppSettingsEntry("ADTimeoutForExchangePrincipalInstantiation", 20, null);

		// Token: 0x04000386 RID: 902
		private static readonly ITokenMunger TokenMunger = new SlaveAccountTokenMunger();

		// Token: 0x04000387 RID: 903
		private readonly string logonName;

		// Token: 0x04000388 RID: 904
		private readonly string authenticationType;

		// Token: 0x04000389 RID: 905
		private ClientSecurityContext clientSecurityContext;
	}
}
