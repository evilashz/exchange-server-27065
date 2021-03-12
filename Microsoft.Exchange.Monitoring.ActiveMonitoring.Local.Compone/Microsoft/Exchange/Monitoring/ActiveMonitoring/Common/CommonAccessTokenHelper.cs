using System;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000122 RID: 290
	internal static class CommonAccessTokenHelper
	{
		// Token: 0x060008A3 RID: 2211 RVA: 0x00032A5C File Offset: 0x00030C5C
		public static CommonAccessToken CreateLiveId(string emailAddress)
		{
			if (string.IsNullOrEmpty(emailAddress))
			{
				throw new ArgumentNullException("emailAddress");
			}
			IRecipientSession recipientSession = null;
			ADUser adRawEntry = CommonAccessTokenHelper.ResolveTenantUser(emailAddress, out recipientSession);
			LiveIdFbaTokenAccessor liveIdFbaTokenAccessor = LiveIdFbaTokenAccessor.Create(adRawEntry);
			return liveIdFbaTokenAccessor.GetToken();
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00032A94 File Offset: 0x00030C94
		public static CommonAccessToken CreateLiveIdBasic(string emailAddress)
		{
			if (string.IsNullOrEmpty(emailAddress))
			{
				throw new ArgumentNullException("emailAddress");
			}
			IRecipientSession recipientSession = null;
			ADUser adRawEntry = CommonAccessTokenHelper.ResolveTenantUser(emailAddress, out recipientSession);
			LiveIdBasicTokenAccessor liveIdBasicTokenAccessor = LiveIdBasicTokenAccessor.Create(adRawEntry);
			return liveIdBasicTokenAccessor.GetToken();
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00032ACC File Offset: 0x00030CCC
		public static CommonAccessToken CreateCertificateSid(string emailAddress)
		{
			if (string.IsNullOrEmpty(emailAddress))
			{
				throw new ArgumentNullException("emailAddress");
			}
			IRecipientSession recipientSession = null;
			ADUser adRawEntry;
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled)
			{
				adRawEntry = CommonAccessTokenHelper.ResolveTenantUser(emailAddress, out recipientSession);
			}
			else
			{
				adRawEntry = CommonAccessTokenHelper.ResolveRootOrgUser(emailAddress, out recipientSession);
			}
			CertificateSidTokenAccessor certificateSidTokenAccessor = CertificateSidTokenAccessor.Create(adRawEntry);
			return certificateSidTokenAccessor.GetToken();
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00032B34 File Offset: 0x00030D34
		public static CommonAccessToken CreateWindows(string userPrincipalName)
		{
			if (string.IsNullOrEmpty(userPrincipalName))
			{
				throw new ArgumentNullException("userPrincipalName");
			}
			CommonAccessToken result;
			using (WindowsIdentity windowsIdentity = new WindowsIdentity(userPrincipalName))
			{
				result = CommonAccessTokenHelper.CreateWindows(windowsIdentity);
			}
			return result;
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00032B80 File Offset: 0x00030D80
		public static CommonAccessToken CreateWindows(WindowsIdentity windowsIdentity)
		{
			if (windowsIdentity == null)
			{
				throw new ArgumentNullException("windowsIdentity");
			}
			WindowsTokenAccessor windowsTokenAccessor = WindowsTokenAccessor.Create(windowsIdentity);
			return windowsTokenAccessor.GetToken();
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00032BA8 File Offset: 0x00030DA8
		public static CommonAccessToken CreateWindows(string username, string password)
		{
			CommonAccessToken result;
			using (AuthenticationContext authenticationContext = new AuthenticationContext())
			{
				SecurityStatus securityStatus = authenticationContext.LogonUser(username, password.ConvertToSecureString());
				if (securityStatus != SecurityStatus.OK)
				{
					throw new ApplicationException(string.Format("monitoring mailbox {0} logon failed, SecurityStatus={1}", username, securityStatus));
				}
				WindowsIdentity identity = authenticationContext.Identity;
				result = CommonAccessTokenHelper.CreateWindows(identity);
			}
			return result;
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00032C10 File Offset: 0x00030E10
		public static ADUser ResolveTenantUser(string emailAddress, out IRecipientSession recipientSession)
		{
			if (!SmtpAddress.IsValidSmtpAddress(emailAddress))
			{
				throw new ArgumentException(string.Format("'{0}' is not a valid SMTP address", emailAddress), "emailAddress");
			}
			string domain = SmtpAddress.Parse(emailAddress).Domain;
			ADSessionSettings sessionSettings;
			try
			{
				sessionSettings = ADSessionSettings.FromTenantAcceptedDomain(domain);
			}
			catch (CannotResolveTenantNameException ex)
			{
				throw new ArgumentException(string.Format("'{0}' cannot resolve as a tenant domain/r/nOriginal Exception: {1}", domain, ex.Message), "emailAddress");
			}
			recipientSession = DirectorySessionFactory.Default.CreateTenantRecipientSession(false, ConsistencyMode.IgnoreInvalid, sessionSettings, 186, "ResolveTenantUser", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Utils\\CommonAccessTokenHelper.cs");
			ADUser aduser = recipientSession.FindByProxyAddress<ADUser>(new SmtpProxyAddress(emailAddress, true));
			if (aduser == null)
			{
				throw new ApplicationException(string.Format("monitoring mailbox {0} not found!", emailAddress));
			}
			return aduser;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00032CC8 File Offset: 0x00030EC8
		public static ADUser ResolveRootOrgUser(string emailAddress, out IRecipientSession recipientSession)
		{
			if (!SmtpAddress.IsValidSmtpAddress(emailAddress))
			{
				throw new ArgumentException(string.Format("'{0}' is not a valid SMTP address", emailAddress), "emailAddress");
			}
			string domain = SmtpAddress.Parse(emailAddress).Domain;
			recipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 220, "ResolveRootOrgUser", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Utils\\CommonAccessTokenHelper.cs");
			ADUser aduser = recipientSession.FindByProxyAddress<ADUser>(new SmtpProxyAddress(emailAddress, true));
			if (aduser == null)
			{
				throw new ApplicationException(string.Format("monitoring mailbox {0} not found!", emailAddress));
			}
			return aduser;
		}
	}
}
