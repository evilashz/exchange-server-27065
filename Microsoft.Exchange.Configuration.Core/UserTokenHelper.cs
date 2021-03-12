﻿using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x02000010 RID: 16
	internal static class UserTokenHelper
	{
		// Token: 0x06000060 RID: 96 RVA: 0x000039FC File Offset: 0x00001BFC
		internal static void UniformCommonAccessToken(this UserToken userToken)
		{
			CommonAccessToken commonAccessToken = userToken.CommonAccessToken;
			if (commonAccessToken == null)
			{
				return;
			}
			CommonAccessToken commonAccessToken2 = CommonAccessToken.Deserialize(commonAccessToken.Serialize());
			commonAccessToken2.Version = 0;
			commonAccessToken2.ExtensionData.Clear();
			foreach (string key in UserTokenHelper.WinRMCATExtensionDataKeys)
			{
				if (commonAccessToken.ExtensionData.ContainsKey(key))
				{
					commonAccessToken2.ExtensionData[key] = commonAccessToken.ExtensionData[key];
				}
			}
			if (!commonAccessToken2.ExtensionData.ContainsKey("UserSid") && userToken.UserSid != null)
			{
				commonAccessToken2.ExtensionData["UserSid"] = userToken.UserSid.ToString();
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003AB0 File Offset: 0x00001CB0
		internal static string GetReadableCommonAccessToken(this UserToken userToken)
		{
			CommonAccessToken commonAccessToken = userToken.CommonAccessToken;
			if (commonAccessToken == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Type:{0} Version:{1} WinAT:{2}", commonAccessToken.TokenType, commonAccessToken.Version, (commonAccessToken.WindowsAccessToken != null) ? commonAccessToken.WindowsAccessToken.UserSid : "null");
			foreach (KeyValuePair<string, string> keyValuePair in commonAccessToken.ExtensionData)
			{
				stringBuilder.AppendFormat(" {0}:{1}", keyValuePair.Key, keyValuePair.Value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003B68 File Offset: 0x00001D68
		internal static UserToken CreateDefaultUserTokenInERC(IIdentity identity, DelegatedPrincipal delegatedPrincipal, bool impersonated)
		{
			if (!impersonated && delegatedPrincipal != null)
			{
				CommonAccessToken commonAccessToken = new CommonAccessToken(AccessTokenType.RemotePowerShellDelegated);
				commonAccessToken.ExtensionData["DelegatedData"] = delegatedPrincipal.Identity.Name;
				return new UserToken(AuthenticationType.RemotePowerShellDelegated, delegatedPrincipal, null, delegatedPrincipal.Identity.Name, null, null, null, delegatedPrincipal.DelegatedOrganization, false, commonAccessToken);
			}
			SidOAuthIdentity sidOAuthIdentity = identity as SidOAuthIdentity;
			if (sidOAuthIdentity != null)
			{
				PartitionId partitionId;
				PartitionId.TryParse(sidOAuthIdentity.PartitionId, out partitionId);
				return new UserToken(AuthenticationType.OAuth, null, null, sidOAuthIdentity.Name, sidOAuthIdentity.Sid, partitionId, sidOAuthIdentity.OAuthIdentity.OrganizationId, sidOAuthIdentity.ManagedTenantName, false, sidOAuthIdentity.OAuthIdentity.ToCommonAccessTokenVersion1());
			}
			WindowsIdentity windowsIdentity = identity as WindowsIdentity;
			if (windowsIdentity != null)
			{
				return new UserToken(AuthenticationType.Kerberos, null, null, windowsIdentity.Name, windowsIdentity.User, null, null, null, false, new CommonAccessToken(windowsIdentity));
			}
			WindowsTokenIdentity windowsTokenIdentity = identity as WindowsTokenIdentity;
			if (windowsTokenIdentity != null && windowsTokenIdentity.AccessToken != null && windowsTokenIdentity.AccessToken.CommonAccessToken != null)
			{
				PartitionId partitionId2;
				PartitionId.TryParse(windowsTokenIdentity.PartitionId, out partitionId2);
				return new UserToken(AuthenticationType.Kerberos, null, null, windowsTokenIdentity.Name, windowsTokenIdentity.Sid, partitionId2, null, null, false, windowsTokenIdentity.AccessToken.CommonAccessToken);
			}
			CommonAccessToken commonAccessToken2 = new CommonAccessToken(AccessTokenType.CertificateSid);
			SecurityIdentifier securityIdentifier = identity.GetSecurityIdentifier();
			commonAccessToken2.ExtensionData["UserSid"] = securityIdentifier.ToString();
			GenericSidIdentity genericSidIdentity = identity as GenericSidIdentity;
			if (genericSidIdentity != null)
			{
				commonAccessToken2.ExtensionData["Partition"] = genericSidIdentity.PartitionId;
			}
			return new UserToken(AuthenticationType.Certificate, null, null, identity.Name, securityIdentifier, null, null, null, false, commonAccessToken2);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003CEA File Offset: 0x00001EEA
		internal static CommonAccessToken CommonAccessTokenForCmdletProxy(this UserToken userToken)
		{
			return userToken.CommonAccessTokenForCmdletProxy(Server.E15MinVersion);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003CF8 File Offset: 0x00001EF8
		internal static CommonAccessToken CommonAccessTokenForCmdletProxy(this UserToken userToken, int targetVersion)
		{
			CommonAccessToken commonAccessToken = userToken.CommonAccessToken;
			if (commonAccessToken != null)
			{
				if (userToken.AuthenticationType == AuthenticationType.OAuth && targetVersion < Server.E15MinVersion)
				{
					OAuthIdentity oauthIdentity = OAuthIdentitySerializer.ConvertFromCommonAccessToken(commonAccessToken);
					SidOAuthIdentity sidIdentity = SidOAuthIdentity.Create(oauthIdentity);
					CertificateSidTokenAccessor certificateSidTokenAccessor = CertificateSidTokenAccessor.Create(sidIdentity);
					commonAccessToken = certificateSidTokenAccessor.GetToken();
				}
			}
			else
			{
				commonAccessToken = new CommonAccessToken(AccessTokenType.CertificateSid);
				ExAssert.RetailAssert(userToken.UserSid != null, "UserToken.UserSid is expected to NOT NULL when CommonAccessToken doesn't exist. UserToken = " + userToken);
				commonAccessToken.ExtensionData["UserSid"] = userToken.UserSid.ToString();
				if (userToken.PartitionId != null)
				{
					commonAccessToken.ExtensionData["Partition"] = userToken.PartitionId.ToString();
				}
			}
			commonAccessToken.IsCompressed = true;
			return commonAccessToken;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003DB3 File Offset: 0x00001FB3
		internal static void UpdateUserSidForTest(this UserToken userToken, string updatedUserSid)
		{
			if (userToken == null)
			{
				return;
			}
			userToken.UserSid = new SecurityIdentifier(updatedUserSid);
			if (userToken.CommonAccessToken != null)
			{
				userToken.CommonAccessToken.ExtensionData["UserSid"] = updatedUserSid;
			}
		}

		// Token: 0x04000041 RID: 65
		private static readonly string[] WinRMCATExtensionDataKeys = new string[]
		{
			"UserSid",
			"Partition",
			"AppPasswordUsed",
			"UserPrincipalName",
			"MemberName",
			"OrganizationIdBase64",
			"Puid",
			"MemberName",
			"AppType",
			"AppOnly",
			"DelegatedData"
		};
	}
}
