using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Principal;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000C4 RID: 196
	internal class OAuthActAsUser
	{
		// Token: 0x060006B1 RID: 1713 RVA: 0x00030EE1 File Offset: 0x0002F0E1
		private OAuthActAsUser(OrganizationId organizationId, OAuthActAsUser.VerifiedUserInfo verifiedUser)
		{
			this.organizationId = organizationId;
			this.verifiedUser = verifiedUser;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00030F1C File Offset: 0x0002F11C
		private OAuthActAsUser(OrganizationId organizationId, Dictionary<string, string> userAttributes)
		{
			this.organizationId = organizationId;
			this.rawAttributes = userAttributes;
			this.rawAttributeString = string.Join("|", from p in this.rawAttributes
			orderby p.Key
			select p.Key + ":" + p.Value);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00030F97 File Offset: 0x0002F197
		protected OAuthActAsUser(OrganizationId organizationId, string attributeType, string attributeValue)
		{
			this.organizationId = organizationId;
			this.rawAttributes = new Dictionary<string, string>();
			this.rawAttributes.Add(attributeType, attributeValue);
			this.rawAttributeString = attributeType + ":" + attributeValue;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00030FD0 File Offset: 0x0002F1D0
		public void VerifyUser()
		{
			this.verifiedUser = OAuthActAsUser.VerifiedUserInfoResultCache.Instance.Get(this);
			if (this.verifiedUser.Exception != null)
			{
				throw this.verifiedUser.Exception;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x00030FFC File Offset: 0x0002F1FC
		public bool IsUserVerified
		{
			get
			{
				return this.verifiedUser != null;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x0003100A File Offset: 0x0002F20A
		public SecurityIdentifier Sid
		{
			get
			{
				this.EnsureUserIsVerified();
				return this.verifiedUser.Sid;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0003101D File Offset: 0x0002F21D
		public SecurityIdentifier MasterAccountSid
		{
			get
			{
				this.EnsureUserIsVerified();
				return this.verifiedUser.MasterAccountSid;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00031030 File Offset: 0x0002F230
		public string UserPrincipalName
		{
			get
			{
				this.EnsureUserIsVerified();
				return this.verifiedUser.UserPrincipalName;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x00031043 File Offset: 0x0002F243
		public SmtpAddress WindowsLiveID
		{
			get
			{
				this.EnsureUserIsVerified();
				return this.verifiedUser.WindowsLiveID;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x00031056 File Offset: 0x0002F256
		public string DistinguishedName
		{
			get
			{
				this.EnsureUserIsVerified();
				return this.verifiedUser.DistinguishedName;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x00031069 File Offset: 0x0002F269
		public ADRawEntry ADRawEntry
		{
			get
			{
				this.EnsureUserIsVerified();
				return this.verifiedUser.ADRawEntry;
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0003107C File Offset: 0x0002F27C
		public void EnsureUserIsVerified()
		{
			if (!this.IsUserVerified)
			{
				this.VerifyUser();
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x000310C8 File Offset: 0x0002F2C8
		protected virtual OAuthActAsUser.VerifiedUserInfo InternalVerifyUser()
		{
			IDirectorySession directorySession = AuthCommon.IsWindowsLiveIDEnabled ? this.GetTenantRecipientSession() : this.GetRecipientSession();
			QueryFilter filter = this.GetQueryFilter();
			ADRawEntry[] users = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				users = directorySession.Find(null, QueryScope.SubTree, filter, null, 3, OAuthActAsUser.ADRawEntryPropertySet);
			});
			if (adoperationResult != ADOperationResult.Success)
			{
				return OAuthActAsUser.VerifiedUserInfo.FromExceptionWithADOperationResult(adoperationResult);
			}
			if (users == null || users.Length == 0)
			{
				return OAuthActAsUser.VerifiedUserInfo.FromExceptionWithNoUserFound(filter.ToString());
			}
			string text;
			this.rawAttributes.TryGetValue("sid", out text);
			ADRawEntry adrawEntry = null;
			if (users.Length == 1)
			{
				adrawEntry = users[0];
			}
			else
			{
				bool flag;
				if (users.Length == 2 && !string.IsNullOrEmpty(text))
				{
					adrawEntry = this.ChooseADRawEntryBasedOnSID(users[0], users[1], text);
					flag = (adrawEntry == null);
				}
				else if (AuthCommon.IsWindowsLiveIDEnabled)
				{
					adrawEntry = (directorySession as ITenantRecipientSession).ChooseBetweenAmbiguousUsers(users);
					flag = (adrawEntry == null);
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					string userDNs = string.Join(",", from r in users
					select (string)r[ADObjectSchema.DistinguishedName]);
					return OAuthActAsUser.VerifiedUserInfo.FromExceptionWithAmbiguousUsersFound(filter, userDNs);
				}
			}
			string text2 = (string)adrawEntry[ADObjectSchema.DistinguishedName];
			ExTraceGlobals.OAuthTracer.TraceDebug<QueryFilter, string>((long)this.GetHashCode(), "[InternalVerifyUser] Found exactly 1 matched user with {0}: {1}", filter, text2);
			if (!string.IsNullOrEmpty(text))
			{
				SecurityIdentifier securityIdentifier = (SecurityIdentifier)adrawEntry[ADRecipientSchema.MasterAccountSid];
				SecurityIdentifier securityIdentifier2 = (SecurityIdentifier)adrawEntry[ADMailboxRecipientSchema.Sid];
				if (!this.IsValidADRawEntryBasedOnSID(securityIdentifier, securityIdentifier2, text))
				{
					ExTraceGlobals.OAuthTracer.TraceWarning((long)this.GetHashCode(), "[InternalVerifyUser] SID claim is {0}, the recipient found is {1}, with SID {2}, MasterAccountSid {3}", new object[]
					{
						text,
						text2,
						securityIdentifier2,
						securityIdentifier
					});
					return OAuthActAsUser.VerifiedUserInfo.FromException(new InvalidOAuthTokenException(OAuthErrors.NameIdNotMatchMasterAccountSid, null, null));
				}
			}
			string text3;
			this.rawAttributes.TryGetValue("netid", out text3);
			if (!string.IsNullOrEmpty(text3))
			{
				SmtpAddress smtpAddress = (SmtpAddress)adrawEntry[ADRecipientSchema.WindowsLiveID];
				NetID netID = (NetID)adrawEntry[ADUserSchema.NetID];
				if (!this.IsValidADRawEntryBasedOnNetID(smtpAddress, netID, text3))
				{
					ExTraceGlobals.OAuthTracer.TraceWarning((long)this.GetHashCode(), "[InternalVerifyUser] NetID claim is {0}, the recipient found is {1}, with NetID {2}, WindowsLiveID  {3}", new object[]
					{
						text3,
						text2,
						netID,
						smtpAddress
					});
					return OAuthActAsUser.VerifiedUserInfo.FromException(new InvalidOAuthTokenException(OAuthErrors.NameIdNotMatchLiveIDInstanceType, null, null));
				}
			}
			return OAuthActAsUser.VerifiedUserInfo.FromADObject(adrawEntry);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00031388 File Offset: 0x0002F588
		private ADRawEntry ChooseADRawEntryBasedOnSID(ADRawEntry recipient1, ADRawEntry recipient2, string sidQueryValue)
		{
			SecurityIdentifier securityIdentifier = (SecurityIdentifier)recipient1[ADRecipientSchema.MasterAccountSid];
			SecurityIdentifier securityIdentifier2 = (SecurityIdentifier)recipient2[ADRecipientSchema.MasterAccountSid];
			SecurityIdentifier securityIdentifier3 = (SecurityIdentifier)recipient1[ADMailboxRecipientSchema.Sid];
			SecurityIdentifier securityIdentifier4 = (SecurityIdentifier)recipient2[ADMailboxRecipientSchema.Sid];
			bool flag = this.IsValidADRawEntryBasedOnSID(securityIdentifier, securityIdentifier3, sidQueryValue);
			bool flag2 = this.IsValidADRawEntryBasedOnSID(securityIdentifier2, securityIdentifier4, sidQueryValue);
			if (flag && flag2)
			{
				ExTraceGlobals.OAuthTracer.TraceWarning((long)this.GetHashCode(), "[ChooseADRawEntryBasedOnSID] both recipients have valid SID in place. {0}, {1}, {2} and {3} {4} {5}", new object[]
				{
					recipient1[ADObjectSchema.DistinguishedName],
					securityIdentifier3,
					securityIdentifier,
					recipient2[ADObjectSchema.DistinguishedName],
					securityIdentifier4,
					securityIdentifier2
				});
				if (securityIdentifier != null && string.Equals(securityIdentifier.ToString(), sidQueryValue, StringComparison.OrdinalIgnoreCase))
				{
					return recipient1;
				}
				if (securityIdentifier2 != null && string.Equals(securityIdentifier2.ToString(), sidQueryValue, StringComparison.OrdinalIgnoreCase))
				{
					return recipient2;
				}
			}
			ExTraceGlobals.OAuthTracer.TraceWarning<object, object, string>((long)this.GetHashCode(), "[ChooseADRawEntryBasedOnSID] at least 1 recipient from '{0}' and '{1}' has no valid SID in place, the sid claim is '{2}'", recipient1[ADObjectSchema.DistinguishedName], recipient2[ADObjectSchema.DistinguishedName], sidQueryValue);
			return null;
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x000314B1 File Offset: 0x0002F6B1
		private bool IsValidADRawEntryBasedOnSID(SecurityIdentifier masterSid, SecurityIdentifier sid, string sidQueryValue)
		{
			if (masterSid != null)
			{
				return string.Equals(masterSid.ToString(), sidQueryValue, StringComparison.OrdinalIgnoreCase);
			}
			return sid != null && string.Equals(sid.ToString(), sidQueryValue, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x000314E4 File Offset: 0x0002F6E4
		private bool IsValidADRawEntryBasedOnNetID(SmtpAddress windowsLiveId, NetID netId, string netIdQueryValue)
		{
			if (windowsLiveId == SmtpAddress.Empty)
			{
				return false;
			}
			if (netId == null)
			{
				return false;
			}
			LiveIdInstanceType? liveIdInstanceType = DomainPropertyCache.Singleton.Get(new SmtpDomainWithSubdomains(windowsLiveId.Domain)).LiveIdInstanceType;
			return liveIdInstanceType != null && liveIdInstanceType.Value == LiveIdInstanceType.Business && netId == new NetID(netIdQueryValue);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00031548 File Offset: 0x0002F748
		private QueryFilter GetQueryFilter()
		{
			string text;
			this.rawAttributes.TryGetValue("sid", out text);
			string text2;
			this.rawAttributes.TryGetValue("netid", out text2);
			string text3;
			this.rawAttributes.TryGetValue("upn", out text3);
			string text4;
			this.rawAttributes.TryGetValue("smtp", out text4);
			string text5;
			this.rawAttributes.TryGetValue("sip", out text5);
			string text6;
			this.rawAttributes.TryGetValue("liveid", out text6);
			List<QueryFilter> list = new List<QueryFilter>(6);
			if (!string.IsNullOrEmpty(text))
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADMailboxRecipientSchema.Sid, text));
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.MasterAccountSid, text));
			}
			if (!string.IsNullOrEmpty(text2))
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.NetID, new NetID(text2)));
			}
			if (!string.IsNullOrEmpty(text3))
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADUserSchema.UserPrincipalName, text3));
			}
			if (!string.IsNullOrEmpty(text4))
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.PrimarySmtpAddress, text4));
			}
			if (!string.IsNullOrEmpty(text5))
			{
				ProxyAddress proxyAddress = ProxyAddress.Parse("sip", text5);
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.EmailAddresses, proxyAddress.ProxyAddressString));
			}
			if (!string.IsNullOrEmpty(text6))
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.WindowsLiveID, text6));
			}
			QueryFilter queryFilter = (list.Count == 1) ? list.First<QueryFilter>() : new OrFilter(list.ToArray());
			ExTraceGlobals.OAuthTracer.TraceDebug<QueryFilter>((long)this.GetHashCode(), "[GetQueryFilter] The query filter used to find the user: {0}", queryFilter);
			return queryFilter;
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x000316DC File Offset: 0x0002F8DC
		public void AddExtensionDataToCommonAccessToken(CommonAccessToken token)
		{
			if (this.rawAttributes != null)
			{
				token.ExtensionData["RawUserInfo"] = this.rawAttributes.SerializeToJson();
			}
			if (this.IsUserVerified)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>(4);
				dictionary.Add("UserDn", this.DistinguishedName);
				if (!string.IsNullOrEmpty(this.UserPrincipalName))
				{
					dictionary.Add("UserPrincipalName", this.UserPrincipalName);
				}
				if (this.Sid != null)
				{
					ExTraceGlobals.OAuthTracer.TraceDebug<string>(0L, "AddExtensionDataToCommonAccessToken Sid value: {0}", this.Sid.Value);
					dictionary.Add("UserSid", this.Sid.Value);
				}
				if (this.MasterAccountSid != null)
				{
					ExTraceGlobals.OAuthTracer.TraceDebug<string>(0L, "AddExtensionDataToCommonAccessToken MasterAccountSid value: {0}", this.MasterAccountSid.Value);
					dictionary.Add("MasterSid", this.MasterAccountSid.Value);
				}
				if (this.WindowsLiveID != SmtpAddress.Empty)
				{
					dictionary.Add("MemberName", this.WindowsLiveID.ToString());
				}
				token.ExtensionData["VerifiedUserInfo"] = dictionary.SerializeToJson();
			}
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00031814 File Offset: 0x0002FA14
		public static OAuthActAsUser CreateFromPrimarySid(OrganizationId organizationId, SecurityIdentifier securityIdentifier)
		{
			return new OAuthActAsUser.SidOnlyActAsUser(organizationId, securityIdentifier);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0003181D File Offset: 0x0002FA1D
		public static OAuthActAsUser CreateFromExternalDirectoryObjectId(OrganizationId organizationId, string externalDirectoryObjectId)
		{
			return new OAuthActAsUser.OidOnlyActAsUser(organizationId, externalDirectoryObjectId);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00031826 File Offset: 0x0002FA26
		public static OAuthActAsUser CreateFromPuidOnly(OrganizationId organizationId, NetID puid)
		{
			return new OAuthActAsUser.PuidOnlyActAsUser(organizationId, puid);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0003182F File Offset: 0x0002FA2F
		public static OAuthActAsUser CreateFromSmtpOnly(OrganizationId organizationId, string smtpAddress)
		{
			return new OAuthActAsUser.SmtpOnlyActAsUser(organizationId, smtpAddress);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00031838 File Offset: 0x0002FA38
		public static OAuthActAsUser CreateFromPuid(OAuthTokenHandler handler, OrganizationId organizationId, string puid, string smtpOrLiveId)
		{
			ITenantRecipientSession tenantRecipientSession;
			ADRawEntry adrawEntry = DirectoryHelper.GetADRawEntry(puid, organizationId.OrganizationalUnit.Name, smtpOrLiveId, new PropertyDefinition[]
			{
				ADMailboxRecipientSchema.Sid
			}, true, out tenantRecipientSession);
			if (adrawEntry == null)
			{
				handler.Throw(OAuthErrors.NoUserFoundWithGivenClaims, null, null, null);
			}
			return new OAuthActAsUser.SidOnlyActAsUser(organizationId, (SecurityIdentifier)adrawEntry[ADMailboxRecipientSchema.Sid]);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00031894 File Offset: 0x0002FA94
		public static OAuthActAsUser CreateFromOuterToken(OAuthTokenHandler handler, OrganizationId organizationId, JwtSecurityToken jwt, bool acceptSecurityIdentifierInformation)
		{
			string upn = null;
			string smtp = null;
			string sip = null;
			string nameid = null;
			string nii = null;
			OAuthCommon.TryGetClaimValue(jwt, Constants.ClaimTypes.NameIdentifier, out nameid);
			OAuthCommon.TryGetClaimValue(jwt, Constants.ClaimTypes.Nii, out nii);
			OAuthCommon.TryGetClaimValue(jwt, Constants.ClaimTypes.Upn, out upn);
			OAuthCommon.TryGetClaimValue(jwt, Constants.ClaimTypes.Smtp, out smtp);
			OAuthCommon.TryGetClaimValue(jwt, Constants.ClaimTypes.Sip, out sip);
			Dictionary<string, string> dictionary = OAuthActAsUser.AdjustClaimToUserAttribute(handler, nii, nameid, upn, smtp, sip, acceptSecurityIdentifierInformation);
			return OAuthActAsUser.InternalCreateFromAttributes(organizationId, true, dictionary, null);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0003190C File Offset: 0x0002FB0C
		public static OAuthActAsUser CreateFromAppContext(OAuthTokenHandler handler, OrganizationId organizationId, Dictionary<string, string> claimsFromAppContext, bool acceptSecurityIdentifierInformation)
		{
			string upn = null;
			string smtp = null;
			string sip = null;
			string nameid = null;
			string nii = null;
			claimsFromAppContext.TryGetValue(Constants.ClaimTypes.NameIdentifier, out nameid);
			claimsFromAppContext.TryGetValue(Constants.ClaimTypes.Nii, out nii);
			claimsFromAppContext.TryGetValue(Constants.ClaimTypes.Upn, out upn);
			claimsFromAppContext.TryGetValue(Constants.ClaimTypes.Smtp, out smtp);
			claimsFromAppContext.TryGetValue(Constants.ClaimTypes.Sip, out sip);
			Dictionary<string, string> dictionary = OAuthActAsUser.AdjustClaimToUserAttribute(handler, nii, nameid, upn, smtp, sip, acceptSecurityIdentifierInformation);
			return OAuthActAsUser.InternalCreateFromAttributes(organizationId, true, dictionary, null);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00031984 File Offset: 0x0002FB84
		internal static Dictionary<string, string> AdjustClaimToUserAttribute(OAuthTokenHandler handler, string nii, string nameid, string upn, string smtp, string sip, bool acceptSecurityIdentifierInformation)
		{
			if (string.IsNullOrEmpty(nii))
			{
				return OAuthActAsUser.CollectUserAttributes(handler, null, null, nameid, smtp, sip, null);
			}
			if (!acceptSecurityIdentifierInformation)
			{
				if (AuthCommon.IsWindowsLiveIDEnabled)
				{
					return OAuthActAsUser.CollectUserAttributes(handler, null, null, null, smtp, sip, upn);
				}
				return OAuthActAsUser.CollectUserAttributes(handler, null, null, upn, smtp, sip, null);
			}
			else if (string.Equals(nii, Constants.NiiClaimValues.ActiveDirectory, StringComparison.OrdinalIgnoreCase))
			{
				if (AuthCommon.IsWindowsLiveIDEnabled)
				{
					return OAuthActAsUser.CollectUserAttributes(handler, null, null, null, smtp, sip, upn);
				}
				return OAuthActAsUser.CollectUserAttributes(handler, nameid, null, upn, smtp, sip, null);
			}
			else
			{
				if (!string.Equals(nii, Constants.NiiClaimValues.BusinessLiveId, StringComparison.OrdinalIgnoreCase) && !string.Equals(nii, Constants.NiiClaimValues.LegacyBusinessLiveId, StringComparison.OrdinalIgnoreCase))
				{
					return OAuthActAsUser.CollectUserAttributes(handler, null, null, null, smtp, sip, null);
				}
				if (!AuthCommon.IsWindowsLiveIDEnabled)
				{
					return OAuthActAsUser.CollectUserAttributes(handler, null, null, upn, smtp, sip, null);
				}
				return OAuthActAsUser.CollectUserAttributes(handler, null, nameid, null, smtp, sip, upn);
			}
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00031A54 File Offset: 0x0002FC54
		private static Dictionary<string, string> CollectUserAttributes(OAuthTokenHandler handler, string sid, string netId, string upn, string smtp, string sip, string liveId)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			OAuthActAsUser.CollectIfNotNull(dictionary, "sid", sid);
			NetID netID;
			if (NetID.TryParse(netId, out netID))
			{
				OAuthActAsUser.CollectIfNotNull(dictionary, "netid", netId);
			}
			else
			{
				ExTraceGlobals.OAuthTracer.TraceWarning<string>(0L, "[CollectUserAttributes] Invalid NetID claim value {0}", netId);
			}
			OAuthActAsUser.CollectIfNotNull(dictionary, "upn", upn);
			OAuthActAsUser.CollectIfNotNull(dictionary, "smtp", smtp);
			OAuthActAsUser.CollectIfNotNull(dictionary, "sip", sip);
			OAuthActAsUser.CollectIfNotNull(dictionary, "liveid", liveId);
			if (dictionary.Count == 0)
			{
				ExTraceGlobals.OAuthTracer.TraceWarning(0L, "[CollectUserAttributes] No valid user context claims found");
				handler.Throw(OAuthErrors.NoUserClaimsFound, null, null, null);
			}
			return dictionary;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00031AF8 File Offset: 0x0002FCF8
		private static void CollectIfNotNull(Dictionary<string, string> attributes, string attributeType, string attributeValue)
		{
			if (!string.IsNullOrEmpty(attributeValue))
			{
				attributes.Add(attributeType, attributeValue);
			}
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00031B0A File Offset: 0x0002FD0A
		public static OAuthActAsUser CreateFromMiniRecipient(OrganizationId organizationId, MiniRecipient recipient)
		{
			if (recipient == null)
			{
				return null;
			}
			return new OAuthActAsUser(organizationId, OAuthActAsUser.VerifiedUserInfo.FromADObject(recipient));
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00031B20 File Offset: 0x0002FD20
		internal static OAuthActAsUser InternalCreateFromAttributes(OrganizationId organizationId, bool calledAtFrontEnd, Dictionary<string, string> rawAttributes, Dictionary<string, string> verifiedAttributes = null)
		{
			OAuthActAsUser oauthActAsUser;
			if (verifiedAttributes != null)
			{
				if (rawAttributes == null)
				{
					return new OAuthActAsUser(organizationId, OAuthActAsUser.VerifiedUserInfo.FromVerifiedAttributes(verifiedAttributes));
				}
				oauthActAsUser = new OAuthActAsUser.FrontEndVerifiedOAuthActAsUser(organizationId, rawAttributes, verifiedAttributes);
			}
			else if (rawAttributes.Count == 1)
			{
				string text = rawAttributes.Keys.First<string>();
				string text2 = rawAttributes[text];
				string a;
				if ((a = text) != null)
				{
					if (a == "sid")
					{
						oauthActAsUser = new OAuthActAsUser.SidOnlyActAsUser(organizationId, new SecurityIdentifier(text2));
						goto IL_89;
					}
					if (a == "oid")
					{
						oauthActAsUser = new OAuthActAsUser.OidOnlyActAsUser(organizationId, text2);
						goto IL_89;
					}
				}
				oauthActAsUser = new OAuthActAsUser(organizationId, rawAttributes);
			}
			else
			{
				oauthActAsUser = new OAuthActAsUser(organizationId, rawAttributes);
			}
			IL_89:
			if (!calledAtFrontEnd)
			{
				oauthActAsUser.VerifyUser();
			}
			return oauthActAsUser;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00031BC0 File Offset: 0x0002FDC0
		protected IRecipientSession GetRecipientSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.organizationId), 907, "GetRecipientSession", "f:\\15.00.1497\\sources\\dev\\Security\\src\\Authentication\\OAuth\\OAuthActAsUser.cs");
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00031BE8 File Offset: 0x0002FDE8
		protected ITenantRecipientSession GetTenantRecipientSession()
		{
			return DirectorySessionFactory.Default.CreateTenantRecipientSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.organizationId), 919, "GetTenantRecipientSession", "f:\\15.00.1497\\sources\\dev\\Security\\src\\Authentication\\OAuth\\OAuthActAsUser.cs");
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00031C10 File Offset: 0x0002FE10
		public override int GetHashCode()
		{
			return this.rawAttributeString.GetHashCode() ^ this.organizationId.GetHashCode();
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00031C2C File Offset: 0x0002FE2C
		public override bool Equals(object obj)
		{
			OAuthActAsUser oauthActAsUser = obj as OAuthActAsUser;
			return oauthActAsUser != null && this.rawAttributeString.Equals(oauthActAsUser.rawAttributeString) && this.organizationId.Equals(oauthActAsUser.organizationId);
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00031C6B File Offset: 0x0002FE6B
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.rawAttributeString))
			{
				return "actas1(" + this.rawAttributeString + ")";
			}
			return this.verifiedUser.ToString();
		}

		// Token: 0x0400065C RID: 1628
		protected static readonly IEnumerable<PropertyDefinition> ADRawEntryPropertySet = new PropertyDefinition[]
		{
			ADObjectSchema.ExchangeVersion,
			ADObjectSchema.OrganizationId,
			ADMailboxRecipientSchema.Database,
			ADMailboxRecipientSchema.Sid,
			ADRecipientSchema.PrimarySmtpAddress,
			ADRecipientSchema.ExternalEmailAddress,
			ADUserSchema.UserPrincipalName,
			ADRecipientSchema.WindowsLiveID,
			ADUserSchema.NetID,
			ADRecipientSchema.MasterAccountSid,
			ADUserSchema.UserAccountControl,
			ADObjectSchema.DistinguishedName
		};

		// Token: 0x0400065D RID: 1629
		private readonly OrganizationId organizationId;

		// Token: 0x0400065E RID: 1630
		private readonly Dictionary<string, string> rawAttributes;

		// Token: 0x0400065F RID: 1631
		private readonly string rawAttributeString;

		// Token: 0x04000660 RID: 1632
		private OAuthActAsUser.VerifiedUserInfo verifiedUser;

		// Token: 0x020000C5 RID: 197
		private static class UserAttributeType
		{
			// Token: 0x04000664 RID: 1636
			public const string Sid = "sid";

			// Token: 0x04000665 RID: 1637
			public const string ExternalDirectoryObjectId = "oid";

			// Token: 0x04000666 RID: 1638
			public const string SmtpAddress = "smtp";

			// Token: 0x04000667 RID: 1639
			public const string SipAddress = "sip";

			// Token: 0x04000668 RID: 1640
			public const string NetId = "netid";

			// Token: 0x04000669 RID: 1641
			public const string UserPrincipalName = "upn";

			// Token: 0x0400066A RID: 1642
			public const string LiveId = "liveid";
		}

		// Token: 0x020000C6 RID: 198
		protected class VerifiedUserInfo
		{
			// Token: 0x060006D8 RID: 1752 RVA: 0x00031D1A File Offset: 0x0002FF1A
			private VerifiedUserInfo()
			{
			}

			// Token: 0x17000170 RID: 368
			// (get) Token: 0x060006D9 RID: 1753 RVA: 0x00031D22 File Offset: 0x0002FF22
			// (set) Token: 0x060006DA RID: 1754 RVA: 0x00031D2A File Offset: 0x0002FF2A
			public SecurityIdentifier Sid { get; private set; }

			// Token: 0x17000171 RID: 369
			// (get) Token: 0x060006DB RID: 1755 RVA: 0x00031D33 File Offset: 0x0002FF33
			// (set) Token: 0x060006DC RID: 1756 RVA: 0x00031D3B File Offset: 0x0002FF3B
			public SecurityIdentifier MasterAccountSid { get; private set; }

			// Token: 0x17000172 RID: 370
			// (get) Token: 0x060006DD RID: 1757 RVA: 0x00031D44 File Offset: 0x0002FF44
			// (set) Token: 0x060006DE RID: 1758 RVA: 0x00031D4C File Offset: 0x0002FF4C
			public string UserPrincipalName { get; private set; }

			// Token: 0x17000173 RID: 371
			// (get) Token: 0x060006DF RID: 1759 RVA: 0x00031D55 File Offset: 0x0002FF55
			// (set) Token: 0x060006E0 RID: 1760 RVA: 0x00031D5D File Offset: 0x0002FF5D
			public SmtpAddress WindowsLiveID { get; private set; }

			// Token: 0x17000174 RID: 372
			// (get) Token: 0x060006E1 RID: 1761 RVA: 0x00031D66 File Offset: 0x0002FF66
			// (set) Token: 0x060006E2 RID: 1762 RVA: 0x00031D6E File Offset: 0x0002FF6E
			public string DistinguishedName { get; private set; }

			// Token: 0x17000175 RID: 373
			// (get) Token: 0x060006E3 RID: 1763 RVA: 0x00031D77 File Offset: 0x0002FF77
			// (set) Token: 0x060006E4 RID: 1764 RVA: 0x00031D7F File Offset: 0x0002FF7F
			public ADRawEntry ADRawEntry { get; private set; }

			// Token: 0x17000176 RID: 374
			// (get) Token: 0x060006E5 RID: 1765 RVA: 0x00031D88 File Offset: 0x0002FF88
			// (set) Token: 0x060006E6 RID: 1766 RVA: 0x00031D90 File Offset: 0x0002FF90
			public Exception Exception { get; private set; }

			// Token: 0x060006E7 RID: 1767 RVA: 0x00031D9C File Offset: 0x0002FF9C
			public static OAuthActAsUser.VerifiedUserInfo FromVerifiedAttributes(Dictionary<string, string> verifiedAttributes)
			{
				OAuthActAsUser.VerifiedUserInfo verifiedUserInfo = new OAuthActAsUser.VerifiedUserInfo();
				string userPrincipalName;
				if (verifiedAttributes.TryGetValue("UserPrincipalName", out userPrincipalName))
				{
					verifiedUserInfo.UserPrincipalName = userPrincipalName;
				}
				string address;
				if (verifiedAttributes.TryGetValue("MemberName", out address) && SmtpAddress.IsValidSmtpAddress(address))
				{
					verifiedUserInfo.WindowsLiveID = SmtpAddress.Parse(address);
				}
				string text;
				if (verifiedAttributes.TryGetValue("UserSid", out text))
				{
					ExTraceGlobals.OAuthTracer.TraceDebug<string>(0L, "FromVerifiedAttributes Sid value : {0}", text);
					verifiedUserInfo.Sid = new SecurityIdentifier(text);
				}
				string text2;
				if (verifiedAttributes.TryGetValue("MasterSid", out text2))
				{
					ExTraceGlobals.OAuthTracer.TraceDebug<string>(0L, "FromVerifiedAttributes MasterSid value : {0}", text2);
					verifiedUserInfo.MasterAccountSid = new SecurityIdentifier(text2);
				}
				string distinguishedName;
				if (verifiedAttributes.TryGetValue("UserDn", out distinguishedName))
				{
					verifiedUserInfo.DistinguishedName = distinguishedName;
				}
				return verifiedUserInfo;
			}

			// Token: 0x060006E8 RID: 1768 RVA: 0x00031E5C File Offset: 0x0003005C
			public static OAuthActAsUser.VerifiedUserInfo FromADObject(ADRawEntry entry)
			{
				OAuthActAsUser.VerifiedUserInfo verifiedUserInfo = new OAuthActAsUser.VerifiedUserInfo();
				verifiedUserInfo.ADRawEntry = entry;
				verifiedUserInfo.Sid = (entry[ADMailboxRecipientSchema.Sid] as SecurityIdentifier);
				UserAccountControlFlags userAccountControlFlags = (UserAccountControlFlags)entry[ADUserSchema.UserAccountControl];
				if ((userAccountControlFlags & UserAccountControlFlags.AccountDisabled) == UserAccountControlFlags.AccountDisabled)
				{
					verifiedUserInfo.MasterAccountSid = (entry[ADRecipientSchema.MasterAccountSid] as SecurityIdentifier);
				}
				verifiedUserInfo.UserPrincipalName = (entry[ADUserSchema.UserPrincipalName] as string);
				verifiedUserInfo.WindowsLiveID = (SmtpAddress)entry[ADRecipientSchema.WindowsLiveID];
				verifiedUserInfo.DistinguishedName = (entry[ADObjectSchema.DistinguishedName] as string);
				return verifiedUserInfo;
			}

			// Token: 0x060006E9 RID: 1769 RVA: 0x00031EFC File Offset: 0x000300FC
			public static OAuthActAsUser.VerifiedUserInfo FromExceptionWithADOperationResult(ADOperationResult result)
			{
				ExTraceGlobals.OAuthTracer.TraceWarning<ADOperationErrorCode, Exception>(0L, "[VerifiedUserInfo] Failed to query AD with error code {0}, exception {1}", result.ErrorCode, result.Exception);
				InvalidOAuthTokenException exception = new InvalidOAuthTokenException(OAuthErrors.ADOperationFailed, null, result.Exception);
				return OAuthActAsUser.VerifiedUserInfo.FromException(exception);
			}

			// Token: 0x060006EA RID: 1770 RVA: 0x00031F40 File Offset: 0x00030140
			public static OAuthActAsUser.VerifiedUserInfo FromExceptionWithNoUserFound(string queryFilter)
			{
				ExTraceGlobals.OAuthTracer.TraceWarning<string>(0L, "[VerifiedUserInfo] Did not find matched user with query {0}", queryFilter);
				InvalidOAuthTokenException exception = new InvalidOAuthTokenException(OAuthErrors.NoUserFoundWithGivenClaims, null, null)
				{
					ExtraData = queryFilter
				};
				return OAuthActAsUser.VerifiedUserInfo.FromException(exception);
			}

			// Token: 0x060006EB RID: 1771 RVA: 0x00031F7C File Offset: 0x0003017C
			public static OAuthActAsUser.VerifiedUserInfo FromExceptionWithAmbiguousUsersFound(QueryFilter queryFilter, string userDNs)
			{
				ExTraceGlobals.OAuthTracer.TraceWarning<string, QueryFilter>(0L, "[VerifiedUserInfo] Found more than 1 matched user: {0}. Queryfilter: {1}", userDNs, queryFilter);
				InvalidOAuthTokenException exception = new InvalidOAuthTokenException(OAuthErrors.MoreThan1UserFoundWithGivenClaims, null, null)
				{
					ExtraData = queryFilter + "+" + userDNs
				};
				return OAuthActAsUser.VerifiedUserInfo.FromException(exception);
			}

			// Token: 0x060006EC RID: 1772 RVA: 0x00031FC4 File Offset: 0x000301C4
			public static OAuthActAsUser.VerifiedUserInfo FromException(Exception exception)
			{
				return new OAuthActAsUser.VerifiedUserInfo
				{
					Exception = exception
				};
			}

			// Token: 0x060006ED RID: 1773 RVA: 0x00031FDF File Offset: 0x000301DF
			public override string ToString()
			{
				return string.Format("actas2(dn:{0})", this.DistinguishedName);
			}
		}

		// Token: 0x020000C7 RID: 199
		private sealed class VerifiedUserInfoResultCache : LazyLookupTimeoutCache<OAuthActAsUser, OAuthActAsUser.VerifiedUserInfo>
		{
			// Token: 0x060006EE RID: 1774 RVA: 0x00031FF1 File Offset: 0x000301F1
			private VerifiedUserInfoResultCache() : base(2, OAuthActAsUser.VerifiedUserInfoResultCache.cacheSize.Value, false, OAuthActAsUser.VerifiedUserInfoResultCache.cacheTimeToLive.Value)
			{
			}

			// Token: 0x17000177 RID: 375
			// (get) Token: 0x060006EF RID: 1775 RVA: 0x0003200F File Offset: 0x0003020F
			public static OAuthActAsUser.VerifiedUserInfoResultCache Instance
			{
				get
				{
					return OAuthActAsUser.VerifiedUserInfoResultCache.instance;
				}
			}

			// Token: 0x060006F0 RID: 1776 RVA: 0x00032016 File Offset: 0x00030216
			protected override OAuthActAsUser.VerifiedUserInfo CreateOnCacheMiss(OAuthActAsUser key, ref bool shouldAdd)
			{
				shouldAdd = true;
				return key.InternalVerifyUser();
			}

			// Token: 0x04000672 RID: 1650
			private static readonly TimeSpanAppSettingsEntry cacheTimeToLive = new TimeSpanAppSettingsEntry("OAuthUserCacheTimeToLive", TimeSpanUnit.Seconds, TimeSpan.FromMinutes(15.0), ExTraceGlobals.OAuthTracer);

			// Token: 0x04000673 RID: 1651
			private static readonly IntAppSettingsEntry cacheSize = new IntAppSettingsEntry("OAuthUserCacheMaxItems", 4000, ExTraceGlobals.OAuthTracer);

			// Token: 0x04000674 RID: 1652
			private static OAuthActAsUser.VerifiedUserInfoResultCache instance = new OAuthActAsUser.VerifiedUserInfoResultCache();
		}

		// Token: 0x020000C8 RID: 200
		private class SidOnlyActAsUser : OAuthActAsUser
		{
			// Token: 0x060006F2 RID: 1778 RVA: 0x00032077 File Offset: 0x00030277
			public SidOnlyActAsUser(OrganizationId organizationId, SecurityIdentifier sid) : base(organizationId, "sid", sid.Value)
			{
				this.SecurityIdentifier = sid;
			}

			// Token: 0x17000178 RID: 376
			// (get) Token: 0x060006F3 RID: 1779 RVA: 0x00032092 File Offset: 0x00030292
			// (set) Token: 0x060006F4 RID: 1780 RVA: 0x0003209A File Offset: 0x0003029A
			public SecurityIdentifier SecurityIdentifier { get; private set; }

			// Token: 0x060006F5 RID: 1781 RVA: 0x000320D0 File Offset: 0x000302D0
			protected override OAuthActAsUser.VerifiedUserInfo InternalVerifyUser()
			{
				IRecipientSession recipientSession = base.GetRecipientSession();
				ADRawEntry recipient = null;
				ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					recipient = recipientSession.FindADRawEntryBySid(this.SecurityIdentifier, OAuthActAsUser.ADRawEntryPropertySet);
				});
				if (adoperationResult != ADOperationResult.Success)
				{
					return OAuthActAsUser.VerifiedUserInfo.FromExceptionWithADOperationResult(adoperationResult);
				}
				if (recipient == null)
				{
					return OAuthActAsUser.VerifiedUserInfo.FromExceptionWithNoUserFound(this.SecurityIdentifier.Value);
				}
				return OAuthActAsUser.VerifiedUserInfo.FromADObject(recipient);
			}
		}

		// Token: 0x020000C9 RID: 201
		private class OidOnlyActAsUser : OAuthActAsUser
		{
			// Token: 0x060006F6 RID: 1782 RVA: 0x00032142 File Offset: 0x00030342
			public OidOnlyActAsUser(OrganizationId organizationId, string externalDirectoryObjectId) : base(organizationId, "oid", externalDirectoryObjectId)
			{
				this.ExternalDirectoryObjectId = externalDirectoryObjectId;
			}

			// Token: 0x17000179 RID: 377
			// (get) Token: 0x060006F7 RID: 1783 RVA: 0x00032158 File Offset: 0x00030358
			// (set) Token: 0x060006F8 RID: 1784 RVA: 0x00032160 File Offset: 0x00030360
			public string ExternalDirectoryObjectId { get; private set; }

			// Token: 0x060006F9 RID: 1785 RVA: 0x00032198 File Offset: 0x00030398
			protected override OAuthActAsUser.VerifiedUserInfo InternalVerifyUser()
			{
				ITenantRecipientSession recipientSession = base.GetTenantRecipientSession();
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.ExternalDirectoryObjectId, this.ExternalDirectoryObjectId);
				ADUser[] users = null;
				ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					users = recipientSession.Find<ADUser>(null, QueryScope.SubTree, filter, null, 4);
				});
				if (adoperationResult != ADOperationResult.Success)
				{
					return OAuthActAsUser.VerifiedUserInfo.FromExceptionWithADOperationResult(adoperationResult);
				}
				if (users == null || users.Length == 0)
				{
					return OAuthActAsUser.VerifiedUserInfo.FromExceptionWithNoUserFound(this.ExternalDirectoryObjectId);
				}
				ADRawEntry adrawEntry = recipientSession.ChooseBetweenAmbiguousUsers(users);
				if (adrawEntry == null)
				{
					return OAuthActAsUser.VerifiedUserInfo.FromExceptionWithAmbiguousUsersFound(filter, string.Join(",", from r in users
					select r.DistinguishedName));
				}
				return OAuthActAsUser.VerifiedUserInfo.FromADObject(adrawEntry);
			}
		}

		// Token: 0x020000CA RID: 202
		private class PuidOnlyActAsUser : OAuthActAsUser
		{
			// Token: 0x060006FB RID: 1787 RVA: 0x0003226D File Offset: 0x0003046D
			public PuidOnlyActAsUser(OrganizationId organizationId, NetID puid) : base(organizationId, "netid", puid.ToString())
			{
				this.Puid = puid;
			}

			// Token: 0x1700017A RID: 378
			// (get) Token: 0x060006FC RID: 1788 RVA: 0x00032288 File Offset: 0x00030488
			// (set) Token: 0x060006FD RID: 1789 RVA: 0x00032290 File Offset: 0x00030490
			public NetID Puid { get; private set; }

			// Token: 0x060006FE RID: 1790 RVA: 0x000322F4 File Offset: 0x000304F4
			protected override OAuthActAsUser.VerifiedUserInfo InternalVerifyUser()
			{
				ITenantRecipientSession recipientSession = base.GetTenantRecipientSession();
				ADRawEntry recipient = null;
				ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					recipient = recipientSession.FindUniqueEntryByNetID(this.Puid.ToString(), this.organizationId.OrganizationalUnit.Name, OAuthActAsUser.ADRawEntryPropertySet.ToArray<PropertyDefinition>());
				});
				if (adoperationResult != ADOperationResult.Success)
				{
					return OAuthActAsUser.VerifiedUserInfo.FromExceptionWithADOperationResult(adoperationResult);
				}
				if (recipient == null)
				{
					return OAuthActAsUser.VerifiedUserInfo.FromExceptionWithNoUserFound(this.Puid.ToString());
				}
				return OAuthActAsUser.VerifiedUserInfo.FromADObject(recipient);
			}
		}

		// Token: 0x020000CB RID: 203
		private class SmtpOnlyActAsUser : OAuthActAsUser
		{
			// Token: 0x060006FF RID: 1791 RVA: 0x00032366 File Offset: 0x00030566
			public SmtpOnlyActAsUser(OrganizationId organizationId, string smtp) : base(organizationId, "smtp", smtp)
			{
				this.Smtp = smtp;
			}

			// Token: 0x1700017B RID: 379
			// (get) Token: 0x06000700 RID: 1792 RVA: 0x0003237C File Offset: 0x0003057C
			// (set) Token: 0x06000701 RID: 1793 RVA: 0x00032384 File Offset: 0x00030584
			public string Smtp { get; private set; }

			// Token: 0x06000702 RID: 1794 RVA: 0x000323B4 File Offset: 0x000305B4
			protected override OAuthActAsUser.VerifiedUserInfo InternalVerifyUser()
			{
				IRecipientSession recipientSession = base.GetTenantRecipientSession();
				ADRawEntry recipient = null;
				SmtpProxyAddress proxyaddress = new SmtpProxyAddress(this.Smtp, true);
				ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					recipient = recipientSession.FindByProxyAddress(proxyaddress, OAuthActAsUser.ADRawEntryPropertySet);
				});
				if (adoperationResult != ADOperationResult.Success)
				{
					return OAuthActAsUser.VerifiedUserInfo.FromExceptionWithADOperationResult(adoperationResult);
				}
				if (recipient == null)
				{
					return OAuthActAsUser.VerifiedUserInfo.FromExceptionWithNoUserFound(this.Smtp);
				}
				return OAuthActAsUser.VerifiedUserInfo.FromADObject(recipient);
			}
		}

		// Token: 0x020000CC RID: 204
		private class FrontEndVerifiedOAuthActAsUser : OAuthActAsUser
		{
			// Token: 0x06000703 RID: 1795 RVA: 0x0003242C File Offset: 0x0003062C
			public FrontEndVerifiedOAuthActAsUser(OrganizationId organizationId, Dictionary<string, string> rawAttributes, Dictionary<string, string> verifiedAttributes) : base(organizationId, rawAttributes)
			{
				this.verified = verifiedAttributes;
			}

			// Token: 0x06000704 RID: 1796 RVA: 0x0003243D File Offset: 0x0003063D
			protected override OAuthActAsUser.VerifiedUserInfo InternalVerifyUser()
			{
				return OAuthActAsUser.VerifiedUserInfo.FromVerifiedAttributes(this.verified);
			}

			// Token: 0x0400067A RID: 1658
			private Dictionary<string, string> verified;
		}
	}
}
