using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000B RID: 11
	public class MRSProxyAuthorizationManager : AuthorizationManagerBase
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00003725 File Offset: 0x00001925
		private TestIntegration TestIntegration
		{
			get
			{
				if (this.testIntegration == null)
				{
					this.testIntegration = new TestIntegration(false);
				}
				return this.testIntegration;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003744 File Offset: 0x00001944
		internal override AdminRoleDefinition[] ComputeAdminRoles(IRootOrganizationRecipientSession recipientSession, ITopologyConfigurationSession configSession)
		{
			string containerDN = configSession.ConfigurationNamingContext.ToDNString();
			ADGroup adgroup = recipientSession.ResolveWellKnownGuid<ADGroup>(WellKnownGuid.EmaWkGuid, containerDN);
			List<AdminRoleDefinition> list = new List<AdminRoleDefinition>(4);
			list.Add(new AdminRoleDefinition(adgroup.Sid, "RecipientAdmins"));
			list.Add(new AdminRoleDefinition(recipientSession.GetExchangeServersUsgSid(), "ExchangeServers"));
			list.Add(new AdminRoleDefinition(new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null), "LocalSystem"));
			list.Add(new AdminRoleDefinition(new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null), "BuiltinAdmins"));
			SecurityIdentifier[] additionalSids = this.GetAdditionalSids();
			for (int i = 0; i < additionalSids.Length; i++)
			{
				string roleName = string.Format("AdditionalAdminRoleFromConfiguration {0}", i);
				list.Add(new AdminRoleDefinition(additionalSids[i], roleName));
			}
			return list.ToArray();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003810 File Offset: 0x00001A10
		internal override IAuthenticationInfo Authenticate(OperationContext operationContext)
		{
			if (operationContext.ServiceSecurityContext.IsAnonymous)
			{
				return null;
			}
			if (this.IsCertificateAuthentication(operationContext))
			{
				return new AuthenticationInfo(true);
			}
			Guid fromHeaders = this.GetFromHeaders<Guid>(operationContext, "MailboxGuid");
			if (fromHeaders == Guid.Empty)
			{
				MrsTracer.ProxyService.Debug("MRSProxyAuthorizationManager.Authenticate: did not find MailboxGuid header on the message. It is valid for remote PST.", new object[0]);
			}
			MrsTracer.ActivityID = fromHeaders.GetHashCode();
			return this.GetPrincipal(operationContext);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003884 File Offset: 0x00001A84
		internal override bool PostAuthnCheck(OperationContext operationContext, IAuthenticationInfo authenticationInfo)
		{
			if (operationContext.ServiceSecurityContext.IsAnonymous)
			{
				return false;
			}
			MRSProxyAuthorizationManager.AuthenticationData authenticationData = this.GetAuthenticationData(operationContext);
			if (authenticationData.IsAuthorized)
			{
				return true;
			}
			if (authenticationInfo.IsCertificateAuthentication)
			{
				authenticationData.IsAuthorized = true;
			}
			else
			{
				authenticationData.IsAuthorized = base.PostAuthnCheck(operationContext, authenticationInfo);
			}
			return authenticationData.IsAuthorized;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000038D8 File Offset: 0x00001AD8
		private IAuthenticationInfo GetPrincipal(OperationContext operationContext)
		{
			MRSProxyAuthorizationManager.AuthenticationData authenticationData = this.GetAuthenticationData(operationContext);
			if (authenticationData.AuthenticationInfo != null)
			{
				return authenticationData.AuthenticationInfo;
			}
			IAuthenticationInfo authenticationInfo = base.Authenticate(operationContext);
			if (authenticationInfo == null)
			{
				return null;
			}
			if (operationContext.Channel.LocalAddress.Uri.Scheme == "net.tcp" || this.TestIntegration.UseHttpsForLocalMoves)
			{
				return authenticationInfo;
			}
			WindowsPrincipal windowsPrincipal = authenticationInfo.WindowsPrincipal;
			WindowsIdentity windowsIdentity = windowsPrincipal.Identity as WindowsIdentity;
			using (ClientSecurityContext clientSecurityContext = new ClientSecurityContext(windowsIdentity))
			{
				if (!LocalServer.AllowsTokenSerializationBy(clientSecurityContext))
				{
					MrsTracer.ProxyService.Debug("MRSProxyAuthorizationManager: User {0} does not have the permission to serialize security token.", new object[]
					{
						authenticationInfo.PrincipalName
					});
					return null;
				}
			}
			object obj;
			if (!OperationContext.Current.IncomingMessageProperties.TryGetValue(HttpRequestMessageProperty.Name, out obj))
			{
				return null;
			}
			HttpRequestMessageProperty httpRequestMessageProperty = obj as HttpRequestMessageProperty;
			if (httpRequestMessageProperty == null)
			{
				return null;
			}
			string[] values = httpRequestMessageProperty.Headers.GetValues("X-CommonAccessToken");
			if (values == null || values.Length != 1)
			{
				return null;
			}
			string text = values[0];
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			using (ClientSecurityContext clientSecurityContext2 = new ClientSecurityContext(windowsIdentity))
			{
				if (!LocalServer.AllowsTokenSerializationBy(clientSecurityContext2))
				{
					MrsTracer.ProxyService.Debug("MRSProxyAuthorizationManager: User {0} does not have the permission to serialize security token.", new object[]
					{
						windowsIdentity
					});
					return null;
				}
			}
			CommonAccessToken commonAccessToken = CommonAccessToken.Deserialize(text);
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(commonAccessToken.WindowsAccessToken.UserSid);
			IRootOrganizationRecipientSession rootOrganizationRecipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 300, "GetPrincipal", "f:\\15.00.1497\\sources\\dev\\mrs\\src\\ProxyService\\MRSProxyAuthorizationManager.cs");
			ADRawEntry adrawEntry = rootOrganizationRecipientSession.FindADRawEntryBySid(securityIdentifier, MRSProxyAuthorizationManager.userPrincipalName);
			if (adrawEntry == null)
			{
				authenticationData.AuthenticationInfo = new AuthenticationInfo(securityIdentifier);
			}
			else
			{
				string sUserPrincipalName = (string)adrawEntry[ADUserSchema.UserPrincipalName];
				windowsIdentity = new WindowsIdentity(sUserPrincipalName);
				authenticationData.AuthenticationInfo = new AuthenticationInfo(windowsIdentity, windowsIdentity.Name);
			}
			return authenticationData.AuthenticationInfo;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003AE8 File Offset: 0x00001CE8
		private T GetFromHeaders<T>(OperationContext operationContext, string headerName)
		{
			int num = operationContext.IncomingMessageHeaders.FindHeader(headerName, "http://schemas.microsoft.com/exchange/services/2006/types");
			if (num != -1)
			{
				return operationContext.IncomingMessageHeaders.GetHeader<T>(num);
			}
			return default(T);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003B24 File Offset: 0x00001D24
		private MRSProxyAuthorizationManager.AuthenticationData GetAuthenticationData(OperationContext operationContext)
		{
			MRSProxyAuthorizationManager.AuthenticationData authenticationData = null;
			if (!MRSProxyAuthorizationManager.Sessions.TryGetValue(operationContext.SessionId, out authenticationData))
			{
				authenticationData = new MRSProxyAuthorizationManager.AuthenticationData();
				MRSProxyAuthorizationManager.Sessions.AddAbsolute(operationContext.SessionId, authenticationData, MRSProxyAuthorizationManager.authenticationDataExpiration, null);
			}
			return authenticationData;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003B68 File Offset: 0x00001D68
		private SecurityIdentifier[] GetAdditionalSids()
		{
			string config = ConfigBase<MRSConfigSchema>.GetConfig<string>("AdditionalSidsForMrsProxyAuthorization");
			if (string.IsNullOrWhiteSpace(config))
			{
				return Array<SecurityIdentifier>.Empty;
			}
			string[] array = config.Split(new char[]
			{
				','
			});
			List<SecurityIdentifier> list = new List<SecurityIdentifier>(array.Length);
			foreach (string text in array)
			{
				if (!string.IsNullOrWhiteSpace(text))
				{
					try
					{
						list.Add(new SecurityIdentifier(text));
					}
					catch (ArgumentException ex)
					{
						CommonUtils.LogEvent(MRSEventLogConstants.Tuple_ServiceConfigCorrupt, new object[]
						{
							CommonUtils.FullExceptionMessage(ex)
						});
						MrsTracer.ProxyService.Debug("MRSProxyAuthorizationManager.GetAdditionalSids: Cannot parse SID '{0}'. Skipping.", new object[]
						{
							text
						});
					}
				}
			}
			if (list.Count == 0)
			{
				return Array<SecurityIdentifier>.Empty;
			}
			return list.ToArray();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003C4C File Offset: 0x00001E4C
		private bool IsCertificateAuthentication(OperationContext operationContext)
		{
			if (!ConfigBase<MRSConfigSchema>.GetConfig<bool>("ProxyServiceCertificateEndpointEnabled"))
			{
				return false;
			}
			ServiceSecurityContext serviceSecurityContext = operationContext.ServiceSecurityContext;
			if (serviceSecurityContext == null || serviceSecurityContext.AuthorizationContext == null || serviceSecurityContext.AuthorizationContext.ClaimSets == null || serviceSecurityContext.AuthorizationContext.ClaimSets.Count != 1)
			{
				return false;
			}
			X509CertificateClaimSet x509CertificateClaimSet = serviceSecurityContext.AuthorizationContext.ClaimSets[0] as X509CertificateClaimSet;
			return x509CertificateClaimSet != null && !(x509CertificateClaimSet.X509Certificate.Subject != ConfigBase<MRSConfigSchema>.GetConfig<string>("ProxyClientCertificateSubject"));
		}

		// Token: 0x0400002F RID: 47
		internal const string MailboxGuidKey = "MRSProxy.MailboxGuid";

		// Token: 0x04000030 RID: 48
		private static readonly TimeoutCache<string, MRSProxyAuthorizationManager.AuthenticationData> Sessions = new TimeoutCache<string, MRSProxyAuthorizationManager.AuthenticationData>(16, 1024, false);

		// Token: 0x04000031 RID: 49
		private static TimeSpan authenticationDataExpiration = TimeSpan.FromHours(1.0);

		// Token: 0x04000032 RID: 50
		private static PropertyDefinition[] userPrincipalName = new PropertyDefinition[]
		{
			ADUserSchema.UserPrincipalName
		};

		// Token: 0x04000033 RID: 51
		private TestIntegration testIntegration;

		// Token: 0x0200000C RID: 12
		private class AuthenticationData
		{
			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000041 RID: 65 RVA: 0x00003D27 File Offset: 0x00001F27
			// (set) Token: 0x06000042 RID: 66 RVA: 0x00003D2F File Offset: 0x00001F2F
			public bool IsAuthorized { get; set; }

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x06000043 RID: 67 RVA: 0x00003D38 File Offset: 0x00001F38
			// (set) Token: 0x06000044 RID: 68 RVA: 0x00003D40 File Offset: 0x00001F40
			public IAuthenticationInfo AuthenticationInfo { get; set; }
		}
	}
}
