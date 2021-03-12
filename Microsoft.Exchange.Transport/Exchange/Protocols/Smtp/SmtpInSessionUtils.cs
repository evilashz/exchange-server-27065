using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.ProcessManager;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Logging;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000413 RID: 1043
	internal static class SmtpInSessionUtils
	{
		// Token: 0x06002FD8 RID: 12248 RVA: 0x000BFA46 File Offset: 0x000BDC46
		public static bool IsAnonymous(SecurityIdentifier identity)
		{
			ArgumentValidator.ThrowIfNull("identity", identity);
			return identity.IsWellKnown(WellKnownSidType.AnonymousSid);
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x000BFA5B File Offset: 0x000BDC5B
		public static bool IsAuthenticated(SecurityIdentifier identity)
		{
			ArgumentValidator.ThrowIfNull("identity", identity);
			return !SmtpInSessionUtils.IsAnonymous(identity);
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x000BFA71 File Offset: 0x000BDC71
		public static bool IsExternalAuthoritative(SecurityIdentifier identity)
		{
			ArgumentValidator.ThrowIfNull("identity", identity);
			return identity == WellKnownSids.ExternallySecuredServers;
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x000BFA89 File Offset: 0x000BDC89
		public static bool IsPartner(SecurityIdentifier identity)
		{
			ArgumentValidator.ThrowIfNull("identity", identity);
			return identity == WellKnownSids.PartnerServers;
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x000BFAA1 File Offset: 0x000BDCA1
		public static bool IsShadowedBySender(string senderShadowContext)
		{
			return !string.IsNullOrEmpty(senderShadowContext);
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x000BFAAC File Offset: 0x000BDCAC
		public static bool IsPeerShadowSession(string peerSessionPrimaryServer)
		{
			return !string.IsNullOrEmpty(peerSessionPrimaryServer);
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x000BFAB7 File Offset: 0x000BDCB7
		public static bool IsAnonymousClientProxiedSession(ISmtpInSession session)
		{
			ArgumentValidator.ThrowIfNull("session", session);
			return session.ProxiedClientAddress != null && session.InboundClientProxyState == InboundClientProxyStates.None;
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x000BFAD7 File Offset: 0x000BDCD7
		public static bool HasSMTPAcceptXMessageContextADRecipientCachePermission(Permission permissions)
		{
			return (permissions & Permission.SMTPAcceptXMessageContextADRecipientCache) != Permission.None;
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x000BFAE6 File Offset: 0x000BDCE6
		public static bool HasSMTPAcceptXMessageContextExtendedPropertiesPermission(Permission permissions)
		{
			return (permissions & Permission.SMTPAcceptXMessageContextExtendedProperties) != Permission.None;
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x000BFAF5 File Offset: 0x000BDCF5
		public static bool HasSMTPAcceptXMessageContextFastIndexPermission(Permission permissions)
		{
			return (permissions & Permission.SMTPAcceptXMessageContextFastIndex) != Permission.None;
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x000BFB04 File Offset: 0x000BDD04
		public static bool HasSMTPAcceptAnySenderPermission(Permission permissions)
		{
			return (permissions & Permission.SMTPAcceptAnySender) != Permission.None;
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x000BFB10 File Offset: 0x000BDD10
		public static bool HasSMTPAntiSpamBypassPermission(Permission permissions)
		{
			return (permissions & Permission.BypassAntiSpam) != Permission.None;
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x000BFB1C File Offset: 0x000BDD1C
		public static bool HasSMTPBypassMessageSizeLimitPermission(Permission permissions)
		{
			return (permissions & Permission.BypassMessageSizeLimit) != Permission.None;
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x000BFB2B File Offset: 0x000BDD2B
		public static bool HasSMTPAcceptAuthoritativeDomainSenderPermission(Permission permissions)
		{
			return (permissions & Permission.SMTPAcceptAuthoritativeDomainSender) != Permission.None;
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x000BFB37 File Offset: 0x000BDD37
		public static bool HasSMTPSubmitPermission(Permission permissions)
		{
			return (permissions & Permission.SMTPSubmit) != Permission.None;
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x000BFB42 File Offset: 0x000BDD42
		public static bool HasSMTPAcceptEXCH50Permission(Permission permissions)
		{
			return (permissions & Permission.SMTPAcceptEXCH50) != Permission.None;
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x000BFB51 File Offset: 0x000BDD51
		public static bool HasSMTPAcceptXShadowPermission(Permission permissions)
		{
			return (permissions & Permission.SMTPAcceptXShadow) != Permission.None;
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x000BFB60 File Offset: 0x000BDD60
		public static bool HasSMTPAcceptXProxyFromPermission(Permission permissions)
		{
			return (permissions & Permission.SMTPAcceptXProxyFrom) != Permission.None;
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x000BFB6F File Offset: 0x000BDD6F
		public static bool HasSMTPAcceptXSessionParamsPermission(Permission permissions)
		{
			return (permissions & Permission.SMTPAcceptXSessionParams) != Permission.None;
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x000BFB7E File Offset: 0x000BDD7E
		public static bool HasSMTPAcceptAnyRecipientPermission(Permission permissions)
		{
			return (permissions & Permission.SMTPAcceptAnyRecipient) != Permission.None;
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x000BFB89 File Offset: 0x000BDD89
		public static bool HasSMTPAcceptAuthenticationFlag(Permission permissions)
		{
			return (permissions & Permission.SMTPAcceptAuthenticationFlag) != Permission.None;
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x000BFB94 File Offset: 0x000BDD94
		public static bool HasSMTPAcceptOrgHeadersPermission(Permission permissions)
		{
			return (permissions & Permission.AcceptOrganizationHeaders) != Permission.None;
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x000BFBA3 File Offset: 0x000BDDA3
		public static bool HasSMTPAcceptForestHeadersPermission(Permission permissions)
		{
			return (permissions & Permission.AcceptForestHeaders) != Permission.None;
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x000BFBB2 File Offset: 0x000BDDB2
		public static bool HasSendForestHeadersPermission(Permission permissions)
		{
			return (permissions & Permission.SendForestHeaders) != Permission.None;
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x000BFBC1 File Offset: 0x000BDDC1
		public static bool HasSMTPAcceptRoutingHeadersPermission(Permission permissions)
		{
			return (permissions & Permission.AcceptRoutingHeaders) != Permission.None;
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x000BFBD0 File Offset: 0x000BDDD0
		public static bool HasSMTPAcceptOrarPermission(Permission permissions)
		{
			return SmtpInSessionUtils.HasSMTPAcceptOrgHeadersPermission(permissions);
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x000BFBD8 File Offset: 0x000BDDD8
		public static bool HasSMTPAcceptRDstPermission(Permission permissions)
		{
			return SmtpInSessionUtils.HasSMTPAcceptOrgHeadersPermission(permissions);
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x000BFBE0 File Offset: 0x000BDDE0
		public static bool HasSMTPAcceptXAttrPermission(Permission permissions)
		{
			return (permissions & Permission.SMTPAcceptXAttr) != Permission.None;
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x000BFBEF File Offset: 0x000BDDEF
		public static bool HasSMTPAcceptXSysProbePermission(Permission permissions)
		{
			return (permissions & Permission.SMTPAcceptXSysProbe) != Permission.None;
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x000BFBFE File Offset: 0x000BDDFE
		public static bool HasSMTPAcceptXOrigFromPermission(Permission permissions)
		{
			return SmtpInSessionUtils.HasSMTPAcceptOrgHeadersPermission(permissions);
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x000BFC06 File Offset: 0x000BDE06
		public static bool HasSMTPAcceptXProxyPermission(SecurityIdentifier remoteIdentity, MultilevelAuthMechanism authMechanism)
		{
			ArgumentValidator.ThrowIfNull("remoteIdentity", remoteIdentity);
			return SmtpInSessionUtils.IsAuthenticated(remoteIdentity) && authMechanism == MultilevelAuthMechanism.MUTUALGSSAPI;
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x000BFC22 File Offset: 0x000BDE22
		public static bool HasSMTPAcceptXProxyToPermission(SecurityIdentifier remoteIdentity, MultilevelAuthMechanism authMechanism)
		{
			ArgumentValidator.ThrowIfNull("remoteIdentity", remoteIdentity);
			return SmtpInSessionUtils.IsAuthenticated(remoteIdentity) && authMechanism == MultilevelAuthMechanism.MUTUALGSSAPI;
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x000BFC3E File Offset: 0x000BDE3E
		public static bool HasAcceptOorgProtocolCapability(SmtpReceiveCapabilities capabilities)
		{
			return (capabilities & SmtpReceiveCapabilities.AcceptOorgProtocol) != SmtpReceiveCapabilities.None;
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x000BFC49 File Offset: 0x000BDE49
		public static bool HasAcceptOorgHeaderCapability(SmtpReceiveCapabilities capabilities)
		{
			return (capabilities & SmtpReceiveCapabilities.AcceptOorgHeader) != SmtpReceiveCapabilities.None;
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x000BFC54 File Offset: 0x000BDE54
		public static bool HasAcceptOrgHeadersCapability(SmtpReceiveCapabilities capabilities)
		{
			return (capabilities & SmtpReceiveCapabilities.AcceptOrgHeaders) != SmtpReceiveCapabilities.None;
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x000BFC60 File Offset: 0x000BDE60
		public static bool HasAcceptCloudServicesMailCapability(SmtpReceiveCapabilities capabilities)
		{
			return (capabilities & SmtpReceiveCapabilities.AcceptCloudServicesMail) != SmtpReceiveCapabilities.None;
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x000BFC6F File Offset: 0x000BDE6F
		public static bool HasAcceptCrossForestMailCapability(SmtpReceiveCapabilities capabilities)
		{
			return (capabilities & SmtpReceiveCapabilities.AcceptCrossForestMail) != SmtpReceiveCapabilities.None;
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x000BFC7E File Offset: 0x000BDE7E
		public static bool HasAllowSubmitCapability(SmtpReceiveCapabilities capabilities)
		{
			return (capabilities & SmtpReceiveCapabilities.AllowSubmit) != SmtpReceiveCapabilities.None;
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x000BFC8D File Offset: 0x000BDE8D
		public static bool ShouldAcceptOorgProtocol(SmtpReceiveCapabilities capabilities)
		{
			return SmtpInSessionUtils.HasAcceptOorgProtocolCapability(capabilities) || SmtpInSessionUtils.HasAcceptCloudServicesMailCapability(capabilities);
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x000BFC9F File Offset: 0x000BDE9F
		public static bool ShouldAcceptProxyProtocol(ProcessTransportRole transportRole, SmtpReceiveCapabilities capabilities)
		{
			return transportRole == ProcessTransportRole.Hub && (capabilities & SmtpReceiveCapabilities.AcceptProxyProtocol) != SmtpReceiveCapabilities.None;
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x000BFCAF File Offset: 0x000BDEAF
		public static bool ShouldAcceptProxyFromProtocol(ProcessTransportRole transportRole, SmtpReceiveCapabilities capabilities)
		{
			return (transportRole == ProcessTransportRole.Hub || transportRole == ProcessTransportRole.FrontEnd) && (capabilities & SmtpReceiveCapabilities.AcceptProxyFromProtocol) != SmtpReceiveCapabilities.None;
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x000BFCC3 File Offset: 0x000BDEC3
		public static bool ShouldAcceptProxyToProtocol(ProcessTransportRole transportRole, SmtpReceiveCapabilities capabilities)
		{
			return transportRole == ProcessTransportRole.FrontEnd && (capabilities & SmtpReceiveCapabilities.AcceptProxyToProtocol) != SmtpReceiveCapabilities.None;
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x000BFCD5 File Offset: 0x000BDED5
		public static bool ShouldAcceptXAttrProtocol(SmtpReceiveCapabilities capabilities)
		{
			return (capabilities & SmtpReceiveCapabilities.AcceptXAttrProtocol) != SmtpReceiveCapabilities.None;
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x000BFCE1 File Offset: 0x000BDEE1
		public static bool ShouldAcceptXSysProbeProtocol(SmtpReceiveCapabilities capabilities)
		{
			return (capabilities & SmtpReceiveCapabilities.AcceptXSysProbeProtocol) != SmtpReceiveCapabilities.None;
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x000BFCF0 File Offset: 0x000BDEF0
		public static bool ShouldAcceptXOrigFromProtocol(SmtpReceiveCapabilities capabilities)
		{
			return (capabilities & SmtpReceiveCapabilities.AcceptXOriginalFromProtocol) != SmtpReceiveCapabilities.None;
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x000BFCFF File Offset: 0x000BDEFF
		public static bool ShouldAllowConsumerMail(SmtpReceiveCapabilities capabilities)
		{
			return (capabilities & SmtpReceiveCapabilities.AllowConsumerMail) != SmtpReceiveCapabilities.None;
		}

		// Token: 0x06003006 RID: 12294 RVA: 0x000BFD0E File Offset: 0x000BDF0E
		public static bool ShouldAuthLoginBeAdvertised(AuthMechanisms authMechanism, SecureState secureState)
		{
			return (authMechanism & AuthMechanisms.BasicAuth) != AuthMechanisms.None && ((authMechanism & AuthMechanisms.BasicAuthRequireTLS) == AuthMechanisms.None || secureState == SecureState.StartTls || secureState == SecureState.AnonymousTls);
		}

		// Token: 0x06003007 RID: 12295 RVA: 0x000BFD28 File Offset: 0x000BDF28
		public static bool ShouldAuthGssApiBeAdvertised(bool integratedAuthenticationSupported, bool authGssApiEnabled)
		{
			return integratedAuthenticationSupported && authGssApiEnabled;
		}

		// Token: 0x06003008 RID: 12296 RVA: 0x000BFD30 File Offset: 0x000BDF30
		public static bool ShouldAuthNtlmBeAdvertised(bool integratedAuthenticationSupported)
		{
			return integratedAuthenticationSupported;
		}

		// Token: 0x06003009 RID: 12297 RVA: 0x000BFD33 File Offset: 0x000BDF33
		public static bool ShouldExpsGssApiBeAdvertised(AuthMechanisms authMechanism, ProcessTransportRole role)
		{
			return (authMechanism & AuthMechanisms.ExchangeServer) != AuthMechanisms.None && SmtpInSessionUtils.DoesRoleSupportInboundXExpsGssapi(role);
		}

		// Token: 0x0600300A RID: 12298 RVA: 0x000BFD44 File Offset: 0x000BDF44
		public static bool DoesRoleSupportInboundXExpsGssapi(ProcessTransportRole role)
		{
			switch (role)
			{
			case ProcessTransportRole.Hub:
			case ProcessTransportRole.FrontEnd:
			case ProcessTransportRole.MailboxDelivery:
				return true;
			}
			return false;
		}

		// Token: 0x0600300B RID: 12299 RVA: 0x000BFD72 File Offset: 0x000BDF72
		public static bool ShouldExpsExchangeAuthBeAdvertised(AuthMechanisms authMechanism, SecureState secureState, ProcessTransportRole role)
		{
			return (authMechanism & AuthMechanisms.ExchangeServer) != AuthMechanisms.None && secureState == SecureState.AnonymousTls && SmtpInSessionUtils.DoesRoleSupportInboundXExpsExchangeAuth(role);
		}

		// Token: 0x0600300C RID: 12300 RVA: 0x000BFD88 File Offset: 0x000BDF88
		public static bool DoesRoleSupportInboundXExpsExchangeAuth(ProcessTransportRole role)
		{
			switch (role)
			{
			case ProcessTransportRole.Hub:
			case ProcessTransportRole.FrontEnd:
			case ProcessTransportRole.MailboxDelivery:
				return true;
			}
			return false;
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x000BFDB6 File Offset: 0x000BDFB6
		public static bool ShouldExpsNtlmBeAdvertised(AuthMechanisms authMechanism)
		{
			return (authMechanism & AuthMechanisms.ExchangeServer) != AuthMechanisms.None;
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x000BFDC2 File Offset: 0x000BDFC2
		public static bool ShouldStartTlsBeAdvertised(AuthMechanisms authMechanism, SecureState secureState, bool startTlsSupported)
		{
			return secureState == SecureState.None && (authMechanism & AuthMechanisms.Tls) != AuthMechanisms.None && startTlsSupported;
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x000BFDCF File Offset: 0x000BDFCF
		public static bool ShouldAnonymousTlsBeAdvertised(AuthMechanisms authMechanism, SecureState secureState, bool anonymousTlsSupported)
		{
			return secureState == SecureState.None && (authMechanism & AuthMechanisms.ExchangeServer) != AuthMechanisms.None && anonymousTlsSupported;
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x000BFDDD File Offset: 0x000BDFDD
		public static bool ShouldXoorgBeAdvertised(SmtpReceiveCapabilities capabilities)
		{
			return SmtpInSessionUtils.ShouldAcceptOorgProtocol(capabilities);
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x000BFDE5 File Offset: 0x000BDFE5
		public static bool ShouldXproxyBeAdvertised(ProcessTransportRole role, SmtpReceiveCapabilities capabilities, SecureState secureState)
		{
			return SmtpInSessionUtils.ShouldAcceptProxyProtocol(role, capabilities) || secureState == SecureState.AnonymousTls;
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x000BFDF6 File Offset: 0x000BDFF6
		public static bool ShouldXproxyFromBeAdvertised(ProcessTransportRole role, SmtpReceiveCapabilities capabilities, SecureState secureState)
		{
			return SmtpInSessionUtils.ShouldAcceptProxyFromProtocol(role, capabilities) || ((role == ProcessTransportRole.Hub || role == ProcessTransportRole.FrontEnd) && secureState == SecureState.AnonymousTls);
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x000BFE10 File Offset: 0x000BE010
		public static bool ShouldXproxyToBeAdvertised(ProcessTransportRole role, SmtpReceiveCapabilities capabilities, SecureState secureState)
		{
			return SmtpInSessionUtils.ShouldAcceptProxyToProtocol(role, capabilities) || (role == ProcessTransportRole.FrontEnd && secureState == SecureState.AnonymousTls);
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x000BFE27 File Offset: 0x000BE027
		public static bool ShouldXrsetProxyToBeAdvertised(ProcessTransportRole role, SmtpReceiveCapabilities capabilities, SecureState secureState)
		{
			return SmtpInSessionUtils.ShouldXproxyToBeAdvertised(role, capabilities, secureState);
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x000BFE31 File Offset: 0x000BE031
		public static bool ShouldXSessionMdbGuidBeAdvertised(ProcessTransportRole role, SecureState secureState)
		{
			return role == ProcessTransportRole.MailboxDelivery && secureState == SecureState.AnonymousTls;
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x000BFE3D File Offset: 0x000BE03D
		public static bool ShouldXAttrBeAdvertised(SmtpReceiveCapabilities capabilities, SecureState secureState)
		{
			return SmtpInSessionUtils.ShouldAcceptXAttrProtocol(capabilities) || (secureState == SecureState.AnonymousTls && MultiTenantTransport.MultiTenancyEnabled);
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x000BFE54 File Offset: 0x000BE054
		public static bool ShouldXSysProbeBeAdvertised(SmtpReceiveCapabilities capabilities, SecureState secureState)
		{
			return SmtpInSessionUtils.ShouldAcceptXSysProbeProtocol(capabilities) || secureState == SecureState.AnonymousTls;
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x000BFE64 File Offset: 0x000BE064
		public static bool ShouldExtendedPropertiesBeAdvertised(ProcessTransportRole role, SecureState secureState, bool extendedPropertiesEnabled)
		{
			return secureState == SecureState.AnonymousTls && (role == ProcessTransportRole.MailboxDelivery || role == ProcessTransportRole.Hub) && extendedPropertiesEnabled;
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x000BFE74 File Offset: 0x000BE074
		public static bool ShouldADRecipientCacheBeAdvertised(ProcessTransportRole role, SecureState secureState, bool adRecipientCacheEnabled)
		{
			return secureState == SecureState.AnonymousTls && (role == ProcessTransportRole.MailboxDelivery || role == ProcessTransportRole.Hub) && adRecipientCacheEnabled;
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x000BFE84 File Offset: 0x000BE084
		public static bool ShouldFastIndexBeAdvertised(ProcessTransportRole role, SecureState secureState, bool fastIndexEnabled)
		{
			return secureState == SecureState.AnonymousTls && (role == ProcessTransportRole.MailboxDelivery || role == ProcessTransportRole.Hub) && fastIndexEnabled;
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x000BFE94 File Offset: 0x000BE094
		public static bool ShouldXSessionTypeBeAdvertised(ProcessTransportRole role, SecureState secureState)
		{
			return role == ProcessTransportRole.MailboxDelivery && secureState == SecureState.AnonymousTls;
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x000BFEA0 File Offset: 0x000BE0A0
		public static Permission GetPermissions(IAuthzAuthorization authzAuthorization, IntPtr userToken, RawSecurityDescriptor securityDescriptor)
		{
			Permission result = Permission.None;
			try
			{
				if (securityDescriptor != null)
				{
					result = authzAuthorization.CheckPermissions(userToken, securityDescriptor, null);
				}
			}
			catch (Win32Exception ex)
			{
				ExTraceGlobals.SmtpReceiveTracer.TraceError<int>(0L, "AuthzAuthorization.CheckPermissions failed with {0}.", ex.NativeErrorCode);
			}
			return result;
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x000BFEEC File Offset: 0x000BE0EC
		public static Permission GetPermissions(IAuthzAuthorization authzAuthorization, SecurityIdentifier client, RawSecurityDescriptor securityDescriptor)
		{
			Permission result = Permission.None;
			try
			{
				if (securityDescriptor != null)
				{
					result = authzAuthorization.CheckPermissions(client, securityDescriptor, null);
				}
			}
			catch (Win32Exception ex)
			{
				ExTraceGlobals.SmtpReceiveTracer.TraceError<int>(0L, "AuthzAuthorization.CheckPermissions failed with {0}.", ex.NativeErrorCode);
			}
			return result;
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x000BFF38 File Offset: 0x000BE138
		public static RestrictedHeaderSet RestrictedHeaderSetFromPermissions(Permission permissions)
		{
			RestrictedHeaderSet restrictedHeaderSet = RestrictedHeaderSet.None;
			if (!SmtpInSessionUtils.HasSMTPAcceptRoutingHeadersPermission(permissions))
			{
				restrictedHeaderSet |= RestrictedHeaderSet.MTA;
			}
			if (!SmtpInSessionUtils.HasSMTPAcceptForestHeadersPermission(permissions))
			{
				restrictedHeaderSet |= RestrictedHeaderSet.Forest;
			}
			if (!SmtpInSessionUtils.HasSMTPAcceptOrgHeadersPermission(permissions))
			{
				restrictedHeaderSet |= RestrictedHeaderSet.Organization;
			}
			return restrictedHeaderSet;
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x000BFF6C File Offset: 0x000BE16C
		public static string FormatTimeSpan(TimeSpan span)
		{
			string text;
			if (span.Milliseconds > 0)
			{
				text = string.Concat(new string[]
				{
					span.Hours.ToString("00"),
					":",
					span.Minutes.ToString("00"),
					":",
					span.Seconds.ToString("00"),
					".",
					span.Milliseconds.ToString("000")
				});
			}
			else
			{
				text = string.Concat(new string[]
				{
					span.Hours.ToString("00"),
					":",
					span.Minutes.ToString("00"),
					":",
					span.Seconds.ToString("00")
				});
			}
			if (span.TotalDays > 0.0)
			{
				text = ((int)span.TotalDays).ToString(CultureInfo.InvariantCulture) + "." + text;
			}
			return text;
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x000C00B0 File Offset: 0x000BE2B0
		public static bool IsRemoteConnectionError(object error)
		{
			if (error is SocketError)
			{
				SocketError socketError = (SocketError)error;
				return socketError != SocketError.Shutdown && socketError != SocketError.TimedOut;
			}
			return false;
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x000C00E4 File Offset: 0x000BE2E4
		public static bool IsMuaSubmission(Permission permissions, SecurityIdentifier remoteIdentity)
		{
			ArgumentValidator.ThrowIfNull("remoteIdentity", remoteIdentity);
			bool flag = SmtpInSessionUtils.HasSMTPAcceptAnySenderPermission(permissions) || SmtpInSessionUtils.HasSMTPAcceptAuthoritativeDomainSenderPermission(permissions);
			return SmtpInSessionUtils.IsAuthenticated(remoteIdentity) && !flag && !SmtpInSessionUtils.IsPartner(remoteIdentity);
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x000C0124 File Offset: 0x000BE324
		public static bool IsTarpitAuthenticationLevelHigh(Permission permissions, SecurityIdentifier remoteIdentity, bool tarpitMuaSubmission)
		{
			ArgumentValidator.ThrowIfNull("remoteIdentity", remoteIdentity);
			bool flag = SmtpInSessionUtils.IsAuthenticated(remoteIdentity);
			if (tarpitMuaSubmission)
			{
				return flag && !SmtpInSessionUtils.IsMuaSubmission(permissions, remoteIdentity);
			}
			return flag;
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x000C0174 File Offset: 0x000BE374
		public static ADOperationResult TryCreateOrUpdateADRecipientCache(TransportMailItem transportMailItem, OrganizationId mailCommandInternalOrganizationId, IProtocolLogSession logSession)
		{
			ArgumentValidator.ThrowIfNull("transportMailItem", transportMailItem);
			ArgumentValidator.ThrowIfNull("logSession", logSession);
			ADOperationResult adoperationResult;
			if (mailCommandInternalOrganizationId != null)
			{
				adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					MultiTenantTransport.UpdateADRecipientCacheAndOrganizationScope(transportMailItem, mailCommandInternalOrganizationId);
				}, 0);
			}
			else
			{
				adoperationResult = MultiTenantTransport.TryCreateADRecipientCache(transportMailItem);
			}
			if (!adoperationResult.Succeeded)
			{
				logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Encountered AD exception during attribution. Details:{0}", new object[]
				{
					adoperationResult.Exception
				});
				MultiTenantTransport.TraceAttributionError(string.Format("Error {0} creating recipient cache for message {1}. We will try again in EOH for Permanent error. For transient error mail is rejected with transient response code.", adoperationResult.Exception, MultiTenantTransport.ToString(transportMailItem)), new object[0]);
			}
			return adoperationResult;
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x000C0234 File Offset: 0x000BE434
		public static bool IsMaxProtocolErrorsExceeded(int numProtocolErrors, ReceiveConnector receiveConnector)
		{
			ArgumentValidator.ThrowIfNull("receiveConnector", receiveConnector);
			return !receiveConnector.MaxProtocolErrors.IsUnlimited && numProtocolErrors > receiveConnector.MaxProtocolErrors.Value;
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x000C0270 File Offset: 0x000BE470
		public static string GetBreadcrumbsAsString(Breadcrumbs<SmtpInSessionBreadcrumbs> breadcrumbs)
		{
			SmtpInSessionBreadcrumbs[] array = new SmtpInSessionBreadcrumbs[64];
			int num = 0;
			foreach (SmtpInSessionBreadcrumbs smtpInSessionBreadcrumbs in ((IEnumerable<SmtpInSessionBreadcrumbs>)breadcrumbs))
			{
				array[num] = smtpInSessionBreadcrumbs;
				if (++num == 64)
				{
					break;
				}
			}
			return string.Join<SmtpInSessionBreadcrumbs>(Environment.NewLine, array);
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x000C02D4 File Offset: 0x000BE4D4
		public static void ApplyRoleBasedEhloOptionsOverrides(IEhloOptions ehloOptions, bool isFrontEndTransportProcess)
		{
			ArgumentValidator.ThrowIfNull("ehloOptions", ehloOptions);
			if (isFrontEndTransportProcess)
			{
				ehloOptions.Xexch50 = false;
				ehloOptions.XShadow = false;
				ehloOptions.XShadowRequest = false;
				ehloOptions.XAdrc = false;
				ehloOptions.XExprops = false;
				ehloOptions.XFastIndex = false;
			}
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x000C0310 File Offset: 0x000BE510
		public static bool ShouldSupportIntegratedAuthentication(bool supportIntegratedAuth, bool isFrontEndTransportProcess)
		{
			return (!isFrontEndTransportProcess || !VariantConfiguration.InvariantNoFlightingSnapshot.Global.WindowsLiveID.Enabled) && supportIntegratedAuth;
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x000C033C File Offset: 0x000BE53C
		public static bool IsMailFromNotAuthorized(bool oorgPresent, bool xAttrPresent, bool xSysProbePresent, SmtpReceiveCapabilities capabilities, Permission permissions)
		{
			return (oorgPresent && !SmtpInSessionUtils.ShouldAcceptOorgProtocol(capabilities)) || (xAttrPresent && !SmtpInSessionUtils.ShouldAcceptXAttrProtocol(capabilities) && !SmtpInSessionUtils.HasSMTPAcceptXAttrPermission(permissions)) || (xSysProbePresent && !SmtpInSessionUtils.ShouldAcceptXSysProbeProtocol(capabilities) && !SmtpInSessionUtils.HasSMTPAcceptXSysProbePermission(permissions));
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x000C0376 File Offset: 0x000BE576
		public static bool IsMessageTooLarge(long messageSize, Permission permissions, long maxMessageSize)
		{
			return !SmtpInSessionUtils.HasSMTPBypassMessageSizeLimitPermission(permissions) && !SmtpInSessionUtils.HasSMTPAcceptOrgHeadersPermission(permissions) && messageSize > maxMessageSize;
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x000C0390 File Offset: 0x000BE590
		public static string GetFormattedTemporaryMessageId(ulong sessionId, DateTime sessionStartTime, int numberOfMessagesReceived)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0:X16};{1:yyyy-MM-ddTHH\\:mm\\:ss.fffZ};{2}", new object[]
			{
				sessionId,
				sessionStartTime,
				numberOfMessagesReceived
			});
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x000C03CF File Offset: 0x000BE5CF
		public static bool ShouldThrottleIncomingTLSConnections(IPConnectionTable tlsIPConnectionTable, bool receiveTlsThrottlingEnabled)
		{
			ArgumentValidator.ThrowIfNull("tlsIPConnectionTable", tlsIPConnectionTable);
			return receiveTlsThrottlingEnabled && !tlsIPConnectionTable.CanAcceptConnection(IPAddress.None);
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x000C03F0 File Offset: 0x000BE5F0
		public static ulong GetSignificantBytesOfIPAddress(IPAddress ipAddress)
		{
			ArgumentValidator.ThrowIfNull("ipAddress", ipAddress);
			IPvxAddress pvxAddress = new IPvxAddress(ipAddress);
			if (pvxAddress.AddressFamily == AddressFamily.InterNetwork)
			{
				return pvxAddress.LowBytes;
			}
			return pvxAddress.HighBytes;
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x000C0429 File Offset: 0x000BE629
		public static SmtpInCommand IdentifySmtpCommand(byte[] command, out int cmdEndOffset)
		{
			return SmtpInSessionUtils.IdentifySmtpCommand(command, 0, (command == null) ? 0 : command.Length, out cmdEndOffset);
		}

		// Token: 0x0600302E RID: 12334 RVA: 0x000C043C File Offset: 0x000BE63C
		public static SmtpInCommand IdentifySmtpCommand(byte[] command, int validDataOffset, int validDataLength, out int cmdEndOffset)
		{
			cmdEndOffset = 0;
			if (command == null || command.Length == 0 || validDataLength == 0)
			{
				return SmtpInCommand.UNKNOWN;
			}
			int num;
			SmtpInSessionUtils.GetCommandOffsets(command, validDataOffset, validDataLength, out num, out cmdEndOffset);
			char c = (char)Util.LowerC[(int)command[validDataOffset]];
			int num2 = cmdEndOffset - num;
			char c2 = c;
			switch (c2)
			{
			case 'a':
				if (SmtpCommand.CompareArg(SmtpCommand.AUTH, command, num, num2))
				{
					return SmtpInCommand.AUTH;
				}
				break;
			case 'b':
				if (SmtpCommand.CompareArg(SmtpCommand.BDAT, command, num, num2))
				{
					return SmtpInCommand.BDAT;
				}
				break;
			case 'c':
			case 'f':
			case 'g':
			case 'i':
			case 'j':
			case 'k':
			case 'l':
			case 'o':
			case 'p':
				break;
			case 'd':
				if (SmtpCommand.CompareArg(SmtpCommand.DATA, command, num, num2))
				{
					return SmtpInCommand.DATA;
				}
				break;
			case 'e':
				if (SmtpCommand.CompareArg(SmtpCommand.EHLO, command, num, num2))
				{
					return SmtpInCommand.EHLO;
				}
				if (SmtpCommand.CompareArg(SmtpCommand.EXPN, command, num, num2))
				{
					return SmtpInCommand.EXPN;
				}
				break;
			case 'h':
				if (SmtpCommand.CompareArg(SmtpCommand.HELO, command, num, num2))
				{
					return SmtpInCommand.HELO;
				}
				if (SmtpCommand.CompareArg(SmtpCommand.HELP, command, num, num2))
				{
					return SmtpInCommand.HELP;
				}
				break;
			case 'm':
				if (SmtpCommand.CompareArg(SmtpCommand.MAIL, command, num, num2))
				{
					return SmtpInCommand.MAIL;
				}
				break;
			case 'n':
				if (SmtpCommand.CompareArg(SmtpCommand.NOOP, command, num, num2))
				{
					return SmtpInCommand.NOOP;
				}
				break;
			case 'q':
				if (SmtpCommand.CompareArg(SmtpCommand.QUIT, command, num, num2))
				{
					return SmtpInCommand.QUIT;
				}
				break;
			case 'r':
				if (SmtpCommand.CompareArg(SmtpCommand.RCPT, command, num, num2))
				{
					return SmtpInCommand.RCPT;
				}
				if (SmtpCommand.CompareArg(SmtpCommand.RSET, command, num, num2))
				{
					return SmtpInCommand.RSET;
				}
				if (SmtpCommand.CompareArg(SmtpCommand.RCPT2, command, num, num2))
				{
					return SmtpInCommand.RCPT2;
				}
				break;
			case 's':
				if (num2 != SmtpCommand.STARTTLS.Length)
				{
					return SmtpInCommand.UNKNOWN;
				}
				if (SmtpCommand.CompareArg(SmtpCommand.STARTTLS, command, num, num2))
				{
					return SmtpInCommand.STARTTLS;
				}
				break;
			default:
				switch (c2)
				{
				case 'v':
					if (SmtpCommand.CompareArg(SmtpCommand.VRFY, command, num, num2))
					{
						return SmtpInCommand.VRFY;
					}
					break;
				case 'x':
					if (SmtpCommand.CompareArg(SmtpCommand.XEXCH50, command, num, num2))
					{
						return SmtpInCommand.XEXCH50;
					}
					if (SmtpCommand.CompareArg(SmtpCommand.ANONYMOUSTLS, command, num, num2))
					{
						return SmtpInCommand.XANONYMOUSTLS;
					}
					if (SmtpCommand.CompareArg(SmtpCommand.EXPS, command, num, num2))
					{
						return SmtpInCommand.XEXPS;
					}
					if (SmtpCommand.CompareArg(SmtpCommand.XSHADOW, command, num, num2))
					{
						return SmtpInCommand.XSHADOW;
					}
					if (SmtpCommand.CompareArg(SmtpCommand.XQDISCARD, command, num, num2))
					{
						return SmtpInCommand.XQDISCARD;
					}
					if (SmtpCommand.CompareArg(SmtpCommand.XPROXY, command, num, num2))
					{
						return SmtpInCommand.XPROXY;
					}
					if (SmtpCommand.CompareArg(SmtpCommand.XPROXYFROM, command, num, num2))
					{
						return SmtpInCommand.XPROXYFROM;
					}
					if (SmtpCommand.CompareArg(SmtpCommand.XPROXYTO, command, num, num2))
					{
						return SmtpInCommand.XPROXYTO;
					}
					if (SmtpCommand.CompareArg(SmtpCommand.XSHADOWREQUEST, command, num, num2))
					{
						return SmtpInCommand.XSHADOWREQUEST;
					}
					if (SmtpCommand.CompareArg(SmtpCommand.XSESSIONPARAMS, command, num, num2))
					{
						return SmtpInCommand.XSESSIONPARAMS;
					}
					break;
				}
				break;
			}
			return SmtpInCommand.UNKNOWN;
		}

		// Token: 0x0600302F RID: 12335 RVA: 0x000C06CC File Offset: 0x000BE8CC
		private static void GetCommandOffsets(byte[] command, int validDataOffset, int validDataLength, out int beginOffset, out int endOffset)
		{
			beginOffset = 0;
			endOffset = 0;
			if (command == null)
			{
				return;
			}
			beginOffset = BufferParser.GetNextToken(command, validDataOffset, validDataLength, out endOffset);
		}
	}
}
