using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.Security.PartnerToken;
using Microsoft.Exchange.Services.EventLogs;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006DC RID: 1756
	internal class AuthZClientInfo : IDisposable
	{
		// Token: 0x060035C0 RID: 13760 RVA: 0x000C0888 File Offset: 0x000BEA88
		protected AuthZClientInfo()
		{
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x000C08B0 File Offset: 0x000BEAB0
		protected AuthZClientInfo(ClientSecurityContext clientSecurityContext, UserIdentity userIdentity, OriginatedFrom originatedFrom)
		{
			this.ClientSecurityContext = clientSecurityContext;
			this.UserIdentity = userIdentity;
			this.OriginatedFrom = originatedFrom;
			if (userIdentity == null)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug((long)this.GetHashCode(), "[AuthZClientInfo::ctor] The account does not have a corresponding recipient object.  Account Sid: " + this.ObjectSid);
			}
			RequestDetailsLogger.Current.ActivityScope.UserEmail = this.PrimarySmtpAddress;
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x000C092F File Offset: 0x000BEB2F
		protected AuthZClientInfo(ClientSecurityContext clientSecurityContext, OriginatedFrom originatedFrom) : this(clientSecurityContext, null, originatedFrom)
		{
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x060035C3 RID: 13763 RVA: 0x000C093A File Offset: 0x000BEB3A
		public IReadOnlyList<AuthZClientInfo> SecondaryClientInfoItems
		{
			get
			{
				return this.secondaryClientInfoCollection;
			}
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x000C0944 File Offset: 0x000BEB44
		public static AuthZClientInfo FromExternalIdentityToken(ExternalIdentityToken externalIdentityToken)
		{
			ClientSecurityContext clientSecurityContext = new ClientSecurityContext(externalIdentityToken, AuthzFlags.AuthzSkipTokenGroups);
			return new AuthZClientInfo(clientSecurityContext, OriginatedFrom.ExternalIdentity);
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x000C0960 File Offset: 0x000BEB60
		private static AuthZClientInfo FromWindowsIdentity(WindowsIdentity identity)
		{
			AuthZClientInfo result;
			try
			{
				result = AuthZClientInfo.InternalFromClientSecurityContext(new ClientSecurityContext(identity), null, OriginatedFrom.Unknown);
			}
			catch (InvalidSerializedAccessTokenException ex)
			{
				if (ExTraceGlobals.ServerToServerAuthZTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					string arg = ServiceDiagnostics.BuildFullExceptionTrace(ex);
					ExTraceGlobals.ServerToServerAuthZTracer.TraceError<SecurityIdentifier, string>(0L, "[AuthZClientInfo::CreateFromRequest] Exception encountered obtaining client security context for caller.  Verify that the caller is a DOMAIN account and also that the local system account is a member of the Windows Authorization Access group in the domain holding the calling user account.  Caller: {0}, Exception: {1}", identity.User, arg);
				}
				throw new InvalidSerializedAccessTokenException(CoreResources.IDs.ErrorCallerIsInvalidADAccount, ex);
			}
			return result;
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x000C09CC File Offset: 0x000BEBCC
		public static AuthZClientInfo FromSecurityAccessToken(SerializedSecurityAccessToken securityAccessToken)
		{
			ClientSecurityContext clientSecurityContext;
			try
			{
				clientSecurityContext = new ClientSecurityContext(securityAccessToken, AuthzFlags.AuthzSkipTokenGroups);
			}
			catch (SecurityException ex)
			{
				ExTraceGlobals.ServerToServerAuthZTracer.TraceError<string>(0L, "[AuthZClientInfo::FromSerializedAccessToken] SecurityException encountered.  Exception message: {0}", ex.Message);
				throw new InvalidSerializedAccessTokenException(ex);
			}
			catch (MissingPrimaryGroupSidException innerException)
			{
				ExTraceGlobals.ServerToServerAuthZTracer.TraceError(0L, "[CallContext::ClientContextFromSerializedAccessToken] Security Access Token was missing primary group sid (empty group sids)");
				throw new InvalidSerializedAccessTokenException(innerException);
			}
			return AuthZClientInfo.InternalFromClientSecurityContext(clientSecurityContext, securityAccessToken.SmtpAddress, OriginatedFrom.AccessToken);
		}

		// Token: 0x060035C7 RID: 13767 RVA: 0x000C0A44 File Offset: 0x000BEC44
		public static AuthZClientInfo FromUserIdentity(UserIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			ClientSecurityContext clientSecurityContext = AuthZClientInfo.CreateClientSecurityContext(identity);
			return new AuthZClientInfo(clientSecurityContext, identity, OriginatedFrom.DirectoryRecipient);
		}

		// Token: 0x060035C8 RID: 13768 RVA: 0x000C0A70 File Offset: 0x000BEC70
		public void AddRef()
		{
			long arg = (long)Interlocked.Increment(ref this.referenceCount);
			ExTraceGlobals.ServerToServerAuthZTracer.TraceDebug<string, long>((long)this.GetHashCode(), "[AuthZClientInfo::AddRef] Reference count for '{0}' being incremented.  New count: {1}", (this.ClientSecurityContext == null) ? "Application" : this.ClientSecurityContext.UserSid.ToString(), arg);
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x060035C9 RID: 13769 RVA: 0x000C0AC0 File Offset: 0x000BECC0
		// (set) Token: 0x060035CA RID: 13770 RVA: 0x000C0AC8 File Offset: 0x000BECC8
		public UserIdentity UserIdentity { get; private set; }

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x060035CB RID: 13771 RVA: 0x000C0AD1 File Offset: 0x000BECD1
		// (set) Token: 0x060035CC RID: 13772 RVA: 0x000C0AE8 File Offset: 0x000BECE8
		public ClientSecurityContext ClientSecurityContext
		{
			get
			{
				if (this.mungedSecurityContext == null)
				{
					return this.clientSecurityContext;
				}
				return this.mungedSecurityContext;
			}
			private set
			{
				this.clientSecurityContext = value;
			}
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x060035CD RID: 13773 RVA: 0x000C0AF1 File Offset: 0x000BECF1
		// (set) Token: 0x060035CE RID: 13774 RVA: 0x000C0AF9 File Offset: 0x000BECF9
		public OriginatedFrom OriginatedFrom { get; private set; }

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x060035CF RID: 13775 RVA: 0x000C0B02 File Offset: 0x000BED02
		public string PrimarySmtpAddress
		{
			get
			{
				if (this.UserIdentity != null)
				{
					return this.UserIdentity.SmtpAddress;
				}
				return null;
			}
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x060035D0 RID: 13776 RVA: 0x000C0B19 File Offset: 0x000BED19
		public SecurityIdentifier ObjectSid
		{
			get
			{
				if (this.ClientSecurityContext != null)
				{
					return this.ClientSecurityContext.UserSid;
				}
				if (this.UserIdentity != null)
				{
					return this.UserIdentity.ObjectSid;
				}
				return null;
			}
		}

		// Token: 0x060035D1 RID: 13777 RVA: 0x000C0B44 File Offset: 0x000BED44
		public virtual string ToCallerString()
		{
			if (this.UserRoleTypes == null)
			{
				return this.ObjectSid.ToString();
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.ObjectSid.ToString());
			foreach (RoleType roleType in this.UserRoleTypes)
			{
				stringBuilder.AppendFormat("_{0}", roleType);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060035D2 RID: 13778 RVA: 0x000C0BB0 File Offset: 0x000BEDB0
		private static AuthZClientInfo InternalFromClientSecurityContext(ClientSecurityContext clientSecurityContext, string userSmtpAddress, OriginatedFrom originatedFrom)
		{
			AuthZClientInfo authZClientInfo = null;
			try
			{
				UserIdentity userIdentity = null;
				bool flag = EWSSettings.IsMultiTenancyEnabled && !string.IsNullOrEmpty(userSmtpAddress) && SmtpAddress.IsValidSmtpAddress(userSmtpAddress);
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string, string>(0L, "[AuthZClientInfo::InternalFromClientSecurityContext] Using ADRecipientSession::{0}() method, where SmtpAddress is {1}", flag ? "CreateFromSmtpAddress" : "CreateForRootOrganization", (!string.IsNullOrEmpty(userSmtpAddress)) ? userSmtpAddress : "<null/empty>");
				ADRecipientSessionContext adRecipientSessionContext;
				if (flag)
				{
					adRecipientSessionContext = ADRecipientSessionContext.CreateFromSmtpAddress(userSmtpAddress);
				}
				else
				{
					adRecipientSessionContext = ADRecipientSessionContext.CreateForRootOrganization();
				}
				if (!ADIdentityInformationCache.Singleton.TryGetUserIdentity(clientSecurityContext.UserSid, adRecipientSessionContext, out userIdentity))
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>(0L, "[AuthZClientInfo::InternalFromClientSecurityContext] The calling account was not found in the directory.  However, we were able to get a valid client security context for it.  It is likely an account from another forest with a trust relationship set up. Caller Sid: {0})", clientSecurityContext.UserSid.ToString());
					authZClientInfo = new AuthZClientInfo(clientSecurityContext, (originatedFrom == OriginatedFrom.Unknown) ? OriginatedFrom.AccessToken : originatedFrom);
				}
				else
				{
					authZClientInfo = new AuthZClientInfo(clientSecurityContext, userIdentity, (originatedFrom == OriginatedFrom.Unknown) ? OriginatedFrom.DirectoryRecipient : originatedFrom);
				}
			}
			catch (SecurityException ex)
			{
				ExTraceGlobals.ServerToServerAuthZTracer.TraceError<string>(0L, "[AuthZClientInfo::InternalFromSerializedAccessToken] SecurityException encountered.  Exception message: {0}", ex.Message);
				throw new InvalidSerializedAccessTokenException(ex);
			}
			catch (MissingPrimaryGroupSidException innerException)
			{
				ExTraceGlobals.ServerToServerAuthZTracer.TraceError(0L, "[AuthZClientInfo::InternalFromSerializedAccessToken] Security Access Token was missing primary group sid (empty group sids)");
				throw new InvalidSerializedAccessTokenException(innerException);
			}
			finally
			{
				if (authZClientInfo == null && clientSecurityContext != null)
				{
					clientSecurityContext.Dispose();
				}
			}
			return authZClientInfo;
		}

		// Token: 0x060035D3 RID: 13779 RVA: 0x000C0CE8 File Offset: 0x000BEEE8
		internal virtual ADRecipientSessionContext GetADRecipientSessionContext()
		{
			if (this.UserIdentity != null)
			{
				return ADRecipientSessionContext.CreateFromADIdentityInformation(this.UserIdentity);
			}
			if (this.ClientSecurityContext.UserSid != null)
			{
				return ADRecipientSessionContext.CreateFromSidInRootOrg(this.ClientSecurityContext.UserSid);
			}
			return ADRecipientSessionContext.CreateForRootOrganization();
		}

		// Token: 0x060035D4 RID: 13780 RVA: 0x000C0D28 File Offset: 0x000BEF28
		public void Dispose()
		{
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<int>((long)this.GetHashCode(), "AuthZClientInfo.Dispose. Hashcode: {0}", this.GetHashCode());
			if (Interlocked.Decrement(ref this.referenceCount) <= 0)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug((long)this.GetHashCode(), "AuthZClientInfo.Dispose. Ref count zero.  Disposed!");
				lock (this.lockObject)
				{
					if (this.ClientSecurityContext != null)
					{
						this.ClientSecurityContext.Dispose();
						this.ClientSecurityContext = null;
					}
					if (this.mungedSecurityContext != null)
					{
						this.mungedSecurityContext.Dispose();
						this.mungedSecurityContext = null;
					}
					return;
				}
			}
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<int>((long)this.GetHashCode(), "AuthZClientInfo.Dispose. Ref count = {0}, Not disposed.", this.referenceCount);
		}

		// Token: 0x060035D5 RID: 13781 RVA: 0x000C0DF4 File Offset: 0x000BEFF4
		private static ClientSecurityContext CreateClientSecurityContext(UserIdentity userIdentity)
		{
			ClientSecurityContext result;
			if (userIdentity.IsClientSecurityContextCreatedFromAccountGroupInformation || !AuthZClientInfo.TryCreateClientSecurityContextFromS4ULogon(userIdentity, out result))
			{
				result = AuthZClientInfo.CreateClientSecurityContextFromAccountGroupInformation(userIdentity);
			}
			AuthZClientInfo.LogSidInformation(result);
			return result;
		}

		// Token: 0x060035D6 RID: 13782 RVA: 0x000C0E24 File Offset: 0x000BF024
		private static bool TryCreateClientSecurityContextFromS4ULogon(UserIdentity userIdentity, out ClientSecurityContext clientSecurityContext)
		{
			bool flag = false;
			clientSecurityContext = null;
			try
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<ADRecipient, SecurityIdentifier>(0L, "AuthZClientInfo.TryCreateClientSecurityContextFromS4ULogon. Constructing client security context with AuthzRequireS4ULogon.  Recipient: {0}, Sid: {1}", userIdentity.Recipient, userIdentity.Sid);
				SerializedSecurityAccessToken serializedSecurityAccessToken = new SerializedSecurityAccessToken();
				serializedSecurityAccessToken.UserSid = userIdentity.Sid.ToString();
				AuthzFlags authzFlags;
				if (Global.GetAppSettingAsBool("PopulateGroupSidsWithLDAP", true))
				{
					SidStringAndAttributes[] userGroupSidsForS4U = AuthZClientInfo.GetUserGroupSidsForS4U(userIdentity.ADUser.Id, userIdentity.OrganizationId);
					if (userGroupSidsForS4U == null)
					{
						RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendAuthenticationError(RequestDetailsLogger.Current, "GroupSidsNotFound", userIdentity.ADUser.Id.DistinguishedName);
						ExTraceGlobals.CommonAlgorithmTracer.TraceError<ADUser>(0L, "TryCreateClientSecurityContextFromS4ULogon failed, groupSids was null for user sid: {0}", userIdentity.ADUser);
						return false;
					}
					serializedSecurityAccessToken.GroupSids = userGroupSidsForS4U;
					authzFlags = AuthzFlags.AuthzSkipTokenGroups;
				}
				else
				{
					authzFlags = AuthzFlags.AuthzRequireS4ULogon;
				}
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(RequestDetailsLogger.Current, "AuthzFlags", authzFlags);
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<ADUser, AuthzFlags>(0L, "Creating ClientSecurityContext for {0} with AuthzFlags: {1}", userIdentity.ADUser, authzFlags);
				clientSecurityContext = new ClientSecurityContext(serializedSecurityAccessToken, authzFlags);
				flag = true;
			}
			catch (AuthzException ex)
			{
				Win32Exception ex2 = ex.InnerException as Win32Exception;
				string arg = (ex2 != null) ? ex2.NativeErrorCode.ToString() : "None";
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<ADRecipient, AuthzException, string>(0L, "AuthZClientInfo.TryCreateClientSecurityContextFromS4ULogon. SecurityException encountered.  Recipient: {0}, Exception: {1}, System Error Code {2}", userIdentity.Recipient, ex, arg);
			}
			finally
			{
				if (!flag && clientSecurityContext != null)
				{
					clientSecurityContext.Dispose();
					clientSecurityContext = null;
				}
			}
			return flag;
		}

		// Token: 0x060035D7 RID: 13783 RVA: 0x000C0FB0 File Offset: 0x000BF1B0
		private static ClientSecurityContext CreateClientSecurityContextFromAccountGroupInformation(UserIdentity userIdentity)
		{
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<ADRecipient, SecurityIdentifier>(0L, "AuthZClientInfo.CreateClientSecurityContextFromAccountGroupInformation. Constructing client security context from account groups.  Recipient: {0}, Sid: {1}", userIdentity.Recipient, userIdentity.ObjectSid);
			SerializedSecurityAccessToken serializedSecurityAccessToken = new SerializedSecurityAccessToken();
			serializedSecurityAccessToken.UserSid = userIdentity.ObjectSid.ToString();
			AuthzFlags flags;
			if (userIdentity.MasterAccountSid != null)
			{
				AuthZClientInfo.AddSidsToToken(serializedSecurityAccessToken, userIdentity.MasterAccountSid);
				flags = AuthzFlags.AuthzSkipTokenGroups;
			}
			else
			{
				flags = AuthzFlags.Default;
			}
			userIdentity.IsClientSecurityContextCreatedFromAccountGroupInformation = true;
			return new ClientSecurityContext(serializedSecurityAccessToken, flags);
		}

		// Token: 0x060035D8 RID: 13784 RVA: 0x000C1028 File Offset: 0x000BF228
		private static SidStringAndAttributes[] GetUserGroupSidsForS4U(ADObjectId userAdObjectId, OrganizationId userOganizationId)
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, true, ConsistencyMode.IgnoreInvalid, null, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(userOganizationId), 751, "GetUserGroupSidsForS4U", "f:\\15.00.1497\\sources\\dev\\services\\src\\Core\\Types\\AuthZClientInfo.cs");
			List<string> tokenSids = tenantOrRootOrgRecipientSession.GetTokenSids(userAdObjectId, AssignmentMethod.S4U);
			if (tokenSids != null)
			{
				return (from s in tokenSids
				select new SidStringAndAttributes(s, 4U)).ToArray<SidStringAndAttributes>();
			}
			return null;
		}

		// Token: 0x060035D9 RID: 13785 RVA: 0x000C1094 File Offset: 0x000BF294
		private static void AddSidsToToken(SerializedSecurityAccessToken serializedSecurityAccessToken, SecurityIdentifier masterAccountSid)
		{
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<SecurityIdentifier>(0L, "AuthZClientInfo.AddSidsToToken. Adding sids to token for masterAccountSid: {0}", masterAccountSid);
			using (ClientSecurityContext clientSecurityContext = new ClientSecurityContext(serializedSecurityAccessToken, AuthzFlags.Default))
			{
				clientSecurityContext.SetSecurityAccessToken(serializedSecurityAccessToken);
			}
			serializedSecurityAccessToken.UserSid = masterAccountSid.ToString();
		}

		// Token: 0x060035DA RID: 13786 RVA: 0x000C10EC File Offset: 0x000BF2EC
		private static void LogSidInformation(ClientSecurityContext clientSecurityContext)
		{
			if (!ServiceDiagnostics.Logger.IsEventCategoryEnabled(ServicesEventLogConstants.Tuple_ImpersonationSecurityContext.CategoryId, ServicesEventLogConstants.Tuple_ImpersonationSecurityContext.Level))
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "AuthZClientInfo.LogSidInformation. Logging is disabled for ImpersonationSecurityContext.");
				return;
			}
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<SecurityIdentifier>(0L, "AuthZClientInfo.LogSidInformation. Logging sid information for UserSid: {0}", clientSecurityContext.UserSid);
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			StringBuilder stringBuilder3 = new StringBuilder();
			SerializedSecurityAccessToken serializedSecurityAccessToken = new SerializedSecurityAccessToken();
			clientSecurityContext.SetSecurityAccessToken(serializedSecurityAccessToken);
			AuthZClientInfo.AddSidInformation(stringBuilder, serializedSecurityAccessToken.UserSid);
			AuthZClientInfo.AddGroupSidInformation(stringBuilder2, serializedSecurityAccessToken.GroupSids);
			AuthZClientInfo.AddGroupSidInformation(stringBuilder3, serializedSecurityAccessToken.RestrictedGroupSids);
			ServiceDiagnostics.Logger.LogEvent(ServicesEventLogConstants.Tuple_ImpersonationSecurityContext, serializedSecurityAccessToken.UserSid, new object[]
			{
				stringBuilder,
				stringBuilder2,
				stringBuilder3,
				Environment.NewLine + Environment.NewLine
			});
		}

		// Token: 0x060035DB RID: 13787 RVA: 0x000C11D0 File Offset: 0x000BF3D0
		private static void AddGroupSidInformation(StringBuilder message, SidStringAndAttributes[] groupInformationList)
		{
			if (groupInformationList != null)
			{
				foreach (SidStringAndAttributes sidStringAndAttributes in groupInformationList)
				{
					AuthZClientInfo.AddSidInformation(message, sidStringAndAttributes.SecurityIdentifier);
				}
			}
			if (message.Length > 32000)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<int, int>(0L, "AuthZClientInfo.AddGroupSidInformation. Group information length, {0}, exceeds maximum event log entry length, {1}. Truncating group information.", message.Length, 32000);
				message.Length = 32000;
				message.Append("...");
			}
		}

		// Token: 0x060035DC RID: 13788 RVA: 0x000C1240 File Offset: 0x000BF440
		private static void AddSidInformation(StringBuilder message, string sid)
		{
			message.Append(Environment.NewLine + sid);
			string ntaccount = AuthZClientInfo.GetNTAccount(sid);
			if (ntaccount != null)
			{
				message.Append("    " + ntaccount);
			}
		}

		// Token: 0x060035DD RID: 13789 RVA: 0x000C127C File Offset: 0x000BF47C
		private static string GetNTAccount(string sid)
		{
			string result = null;
			SecurityIdentifier securityIdentifier = null;
			IdentityReference identityReference = null;
			Exception ex = null;
			try
			{
				securityIdentifier = new SecurityIdentifier(sid);
			}
			catch (ArgumentException ex2)
			{
				ex = ex2;
			}
			catch (OutOfMemoryException ex3)
			{
				ex = ex3;
			}
			catch (SystemException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<Exception>(0L, "AuthZClientInfo.AddSidInformation. SecurityIdentifier construction failed: {0}", ex);
				return result;
			}
			try
			{
				identityReference = securityIdentifier.Translate(AuthZClientInfo.NTAccountType);
			}
			catch (IdentityNotMappedException arg)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<IdentityNotMappedException>(0L, "AuthZClientInfo.AddSidInformation. SecurityIdentifier.Translate failed: {0}", arg);
				return result;
			}
			if (identityReference != null)
			{
				result = identityReference.Value;
			}
			return result;
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x060035DE RID: 13790 RVA: 0x000C1338 File Offset: 0x000BF538
		public virtual RoleType[] ApplicationRoleTypes
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x060035DF RID: 13791 RVA: 0x000C133F File Offset: 0x000BF53F
		public RoleType[] UserRoleTypes
		{
			get
			{
				if (this.managementRoleType == null)
				{
					return null;
				}
				return this.managementRoleType.UserRoleTypes;
			}
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x060035E0 RID: 13792 RVA: 0x000C1356 File Offset: 0x000BF556
		public Guid ObjectGuid
		{
			get
			{
				if (this.UserIdentity != null)
				{
					return this.UserIdentity.ObjectGuid;
				}
				return Guid.Empty;
			}
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x000C1371 File Offset: 0x000BF571
		public virtual AuthZBehavior GetAuthZBehavior()
		{
			if (this.UserRoleTypes == null)
			{
				return AuthZBehavior.DefaultBehavior;
			}
			return new AuthZBehavior.AdminRoleUserAuthZBehavior(this);
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x000C1388 File Offset: 0x000BF588
		public static AuthZClientInfo ResolveIdentity(IIdentity identity)
		{
			CompositeIdentity compositeIdentity = identity as CompositeIdentity;
			if (compositeIdentity != null)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<CompositeIdentity>(0L, "[AuthZClientInfo.FromIdentity] The calling identity is a CompositeIdentity ({0}).", compositeIdentity);
				AuthZClientInfo authZClientInfo = AuthZClientInfo.ResolveIdentity(compositeIdentity.PrimaryIdentity);
				authZClientInfo.secondaryClientInfoCollection = new AuthZClientInfo[compositeIdentity.SecondaryIdentitiesCount];
				if (authZClientInfo.secondaryClientInfoCollection.Length > 0)
				{
					int num = 0;
					foreach (GenericIdentity identity2 in compositeIdentity.SecondaryIdentities)
					{
						authZClientInfo.secondaryClientInfoCollection[num++] = AuthZClientInfo.ResolveIdentity(identity2);
					}
				}
				return authZClientInfo;
			}
			PartnerIdentity partnerIdentity = identity as PartnerIdentity;
			if (partnerIdentity != null)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<PartnerIdentity>(0L, "[AuthZClientInfo.FromIdentity] The calling identity is a PartnerIdentity ({0}).", partnerIdentity);
				return PartnerAuthZClientInfo.FromPartnerIdentity(partnerIdentity);
			}
			OAuthIdentity oauthIdentity = identity as OAuthIdentity;
			if (oauthIdentity != null)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<OAuthIdentity>(0L, "[AuthZClientInfo.FromIdentity] The calling identity is a OAuthIdentity ({0}).", oauthIdentity);
				return AuthZClientInfo.FromOAuthIdentity(oauthIdentity);
			}
			WindowsTokenIdentity windowsTokenIdentity = identity as WindowsTokenIdentity;
			if (windowsTokenIdentity != null)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<WindowsTokenIdentity>(0L, "[AuthZClientInfo.FromIdentity] The calling identity is a WindowsTokenIdentity ({0}).", windowsTokenIdentity);
				return AuthZClientInfo.FromWindowsTokenIdentity(windowsTokenIdentity);
			}
			SidBasedIdentity sidBasedIdentity = identity as SidBasedIdentity;
			if (sidBasedIdentity != null)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<SidBasedIdentity>(0L, "[AuthZClientInfo.FromIdentity] The calling identity is a SidBasedIdentity ({0}).", sidBasedIdentity);
				return AuthZClientInfo.FromSidBasedIdentity(sidBasedIdentity);
			}
			WindowsIdentity windowsIdentity = identity as WindowsIdentity;
			if (windowsIdentity != null)
			{
				ExTraceGlobals.AuthenticationTracer.TraceError<WindowsIdentity>(0L, "[AuthZClientInfo.FromIdentity] The calling identity is a WindowsIdentity ({0}).", windowsIdentity);
				return AuthZClientInfo.FromWindowsIdentity(windowsIdentity);
			}
			ExTraceGlobals.AuthenticationTracer.TraceError<IIdentity>(0L, "[AuthZClientInfo.FromIdentity] Unexpected calling identity {0}", identity);
			throw new InvalidSerializedAccessTokenException();
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x000C1508 File Offset: 0x000BF708
		private static AuthZClientInfo FromOAuthIdentity(OAuthIdentity identity)
		{
			if (identity.IsAppOnly)
			{
				return new AuthZClientInfo.ApplicationAttachedAuthZClientInfo(identity);
			}
			SecurityIdentifier sid = identity.ActAsUser.Sid;
			if ((sid == null || FaultInjection.TraceTest<bool>((FaultInjection.LIDs)2475044157U)) && identity.IsKnownFromSameOrgExchange && HttpContext.Current != null && HttpContext.Current.Request.UserAgent.StartsWith("ASProxy/CrossForest", StringComparison.InvariantCultureIgnoreCase))
			{
				return new AuthZClientInfo.ApplicationAttachedAuthZClientInfo(identity, ClientSecurityContext.FreeBusyPermissionDefaultClientSecurityContext.Clone(), null);
			}
			if (sid == null)
			{
				throw new ServiceAccessDeniedException();
			}
			UserIdentity userIdentity = ADIdentityInformationCache.Singleton.GetUserIdentity(sid, ADRecipientSessionContext.CreateForOrganization(identity.OrganizationId));
			ClientSecurityContext clientSecurityContext = AuthZClientInfo.CreateClientSecurityContext(userIdentity);
			return new AuthZClientInfo.ApplicationAttachedAuthZClientInfo(identity, clientSecurityContext, userIdentity);
		}

		// Token: 0x060035E4 RID: 13796 RVA: 0x000C15B8 File Offset: 0x000BF7B8
		private static AuthZClientInfo FromWindowsTokenIdentity(WindowsTokenIdentity identity)
		{
			ClientSecurityContext clientSecurityContext = identity.CreateClientSecurityContext();
			return AuthZClientInfo.InternalFromClientSecurityContext(clientSecurityContext, null, OriginatedFrom.Unknown);
		}

		// Token: 0x060035E5 RID: 13797 RVA: 0x000C15D4 File Offset: 0x000BF7D4
		private static AuthZClientInfo FromSidBasedIdentity(SidBasedIdentity identity)
		{
			UserIdentity userIdentity = ADIdentityInformationCache.Singleton.GetUserIdentity(identity.Sid, ADRecipientSessionContext.CreateForOrganization(identity.UserOrganizationId));
			if (identity.PrepopulatedGroupSidIds != null)
			{
				ClientSecurityContext clientSecurityContext = identity.CreateClientSecurityContext();
				return new AuthZClientInfo(clientSecurityContext, userIdentity, OriginatedFrom.DirectoryRecipient);
			}
			return AuthZClientInfo.FromUserIdentity(userIdentity);
		}

		// Token: 0x060035E6 RID: 13798 RVA: 0x000C161C File Offset: 0x000BF81C
		public virtual void ApplyManagementRole(ManagementRoleType managementRoleType, WebMethodEntry methodEntry)
		{
			if (managementRoleType != null)
			{
				if (managementRoleType.HasApplicationRoles || !managementRoleType.HasUserRoles)
				{
					throw new InvalidManagementRoleHeaderException((CoreResources.IDs)3264410200U);
				}
			}
			else if (methodEntry.RequireUserAdminRole && !string.IsNullOrWhiteSpace(methodEntry.DefaultUserRoleType))
			{
				managementRoleType = new ManagementRoleType
				{
					UserRoles = new string[]
					{
						methodEntry.DefaultUserRoleType
					}
				};
			}
			this.managementRoleType = managementRoleType;
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x000C1688 File Offset: 0x000BF888
		public void MungeTokenForLinkedAccounts(CallContext callContext)
		{
			if (EWSSettings.IsLinkedAccountTokenMungingEnabled && this.UserIdentity != null && this.UserIdentity.MasterAccountSid != null && !this.wasTokenMungingAttempted)
			{
				lock (this.lockObject)
				{
					if (!this.wasTokenMungingAttempted)
					{
						try
						{
							this.wasTokenMungingAttempted = true;
							this.mungedSecurityContext = AuthZClientInfo.TokenMunger.MungeToken(this.ClientSecurityContext, OrganizationId.ForestWideOrgId);
						}
						catch (TokenMungingException ex)
						{
							RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericError(callContext.ProtocolLog, "TokenMungingException", ex.ToString());
						}
					}
				}
			}
		}

		// Token: 0x04001E17 RID: 7703
		private static readonly Type NTAccountType = typeof(NTAccount);

		// Token: 0x04001E18 RID: 7704
		private int referenceCount = 1;

		// Token: 0x04001E19 RID: 7705
		private object lockObject = new object();

		// Token: 0x04001E1A RID: 7706
		private ClientSecurityContext clientSecurityContext;

		// Token: 0x04001E1B RID: 7707
		private ClientSecurityContext mungedSecurityContext;

		// Token: 0x04001E1C RID: 7708
		private AuthZClientInfo[] secondaryClientInfoCollection = new AuthZClientInfo[0];

		// Token: 0x04001E1D RID: 7709
		protected ManagementRoleType managementRoleType;

		// Token: 0x04001E1E RID: 7710
		private bool wasTokenMungingAttempted;

		// Token: 0x04001E1F RID: 7711
		private static readonly ITokenMunger TokenMunger = new SlaveAccountTokenMunger();

		// Token: 0x020006DD RID: 1757
		internal sealed class ApplicationAttachedAuthZClientInfo : AuthZClientInfo
		{
			// Token: 0x060035EA RID: 13802 RVA: 0x000C175B File Offset: 0x000BF95B
			public ApplicationAttachedAuthZClientInfo(OAuthIdentity identity, ClientSecurityContext clientSecurityContext, UserIdentity userIdentity) : base(clientSecurityContext, userIdentity, OriginatedFrom.DirectoryRecipient)
			{
				this.identity = identity;
			}

			// Token: 0x060035EB RID: 13803 RVA: 0x000C176D File Offset: 0x000BF96D
			public ApplicationAttachedAuthZClientInfo(OAuthIdentity identity)
			{
				this.identity = identity;
			}

			// Token: 0x17000C6E RID: 3182
			// (get) Token: 0x060035EC RID: 13804 RVA: 0x000C177C File Offset: 0x000BF97C
			public OAuthIdentity OAuthIdentity
			{
				get
				{
					return this.identity;
				}
			}

			// Token: 0x060035ED RID: 13805 RVA: 0x000C1784 File Offset: 0x000BF984
			public override void ApplyManagementRole(ManagementRoleType managementRoleType, WebMethodEntry methodEntry)
			{
				if (managementRoleType == null)
				{
					return;
				}
				if (this.identity.IsOfficeExtension)
				{
					throw new InvalidManagementRoleHeaderException((CoreResources.IDs)2329012714U);
				}
				if (this.identity.IsAppOnly && managementRoleType.HasUserRoles)
				{
					throw new InvalidManagementRoleHeaderException((CoreResources.IDs)3910111167U);
				}
				if (managementRoleType.HasUserRoles)
				{
					if (!managementRoleType.HasApplicationRoles)
					{
						throw new InvalidManagementRoleHeaderException(CoreResources.IDs.MessageApplicationRoleShouldPresentWhenUserRolePresent);
					}
					foreach (RoleType value in managementRoleType.UserRoleTypes)
					{
						if (!managementRoleType.ApplicationRoleTypes.Contains(value))
						{
							throw new InvalidManagementRoleHeaderException(CoreResources.IDs.MessageApplicationRoleShouldPresentWhenUserRolePresent);
						}
					}
				}
				if (managementRoleType.HasApplicationRoles && managementRoleType.ApplicationRoleTypes.Length > 1)
				{
					throw new InvalidManagementRoleHeaderException((CoreResources.IDs)2335200077U);
				}
				this.managementRoleType = managementRoleType;
			}

			// Token: 0x17000C6F RID: 3183
			// (get) Token: 0x060035EE RID: 13806 RVA: 0x000C1858 File Offset: 0x000BFA58
			public override RoleType[] ApplicationRoleTypes
			{
				get
				{
					if (this.managementRoleType != null && this.managementRoleType.HasApplicationRoles)
					{
						return this.managementRoleType.ApplicationRoleTypes;
					}
					if (this.identity.IsOfficeExtension)
					{
						return AuthZClientInfo.ApplicationAttachedAuthZClientInfo.OfficeExtensionRoleTypes;
					}
					return AuthZClientInfo.ApplicationAttachedAuthZClientInfo.DefaultApplicationRoleTypes;
				}
			}

			// Token: 0x060035EF RID: 13807 RVA: 0x000C1894 File Offset: 0x000BFA94
			public override AuthZBehavior GetAuthZBehavior()
			{
				AuthZBehavior authZBehavior = null;
				if (!this.identity.IsAppOnly)
				{
					authZBehavior = base.GetAuthZBehavior();
				}
				RoleType roleType = this.ApplicationRoleTypes[0];
				switch (roleType)
				{
				case RoleType.MailboxSearch:
					if (authZBehavior.GetType() != typeof(AuthZBehavior.AdminRoleUserAuthZBehavior))
					{
						throw new InvalidManagementRoleHeaderException((CoreResources.IDs)3313362701U);
					}
					return new AuthZBehavior.MailboxSearchRoleAuthZBehavior(this.identity, authZBehavior);
				case RoleType.LegalHold:
					if (authZBehavior.GetType() != typeof(AuthZBehavior.AdminRoleUserAuthZBehavior))
					{
						throw new InvalidManagementRoleHeaderException(CoreResources.IDs.MessageMissingUserRolesForLegalHoldRoleTypeApp);
					}
					return new AuthZBehavior.LegalHoldRoleAuthZBehavior(this.identity, authZBehavior);
				default:
					if (roleType != RoleType.OrganizationConfiguration)
					{
						switch (roleType)
						{
						case RoleType.UserApplication:
							return new AuthZBehavior.UserApplicationRoleAuthZBehavior(this.identity, authZBehavior);
						case RoleType.ArchiveApplication:
							return new AuthZBehavior.ArchiveApplicationRoleAuthZBehavior(this.identity, authZBehavior);
						case RoleType.LegalHoldApplication:
							return new AuthZBehavior.LegalHoldApplicationRoleAuthZBehavior(this.identity, authZBehavior);
						case RoleType.OfficeExtensionApplication:
							if (!this.identity.OfficeExtension.IsScopedToken)
							{
								return new AuthZBehavior.OfficeExtensionRoleAuthZBehavior(this.identity);
							}
							return new AuthZBehavior.ScopedOfficeExtensionRoleAuthZbehavior(this.identity);
						case RoleType.TeamMailboxLifecycleApplication:
							return new AuthZBehavior.TeamMailboxLifecycleApplicationRoleAuthZBehavior(this.identity, authZBehavior);
						case RoleType.MailboxSearchApplication:
							return new AuthZBehavior.MailboxSearchApplicationRoleAuthZBehavior(this.identity, authZBehavior);
						case RoleType.ExchangeCrossServiceIntegration:
							return new AuthZBehavior.ExchangeCrossServiceIntegrationRoleAuthZBehavior(this.identity, authZBehavior);
						}
						throw new InvalidManagementRoleHeaderException((CoreResources.IDs)3773912990U);
					}
					if (authZBehavior.GetType() != typeof(AuthZBehavior.AdminRoleUserAuthZBehavior))
					{
						throw new InvalidManagementRoleHeaderException(CoreResources.IDs.MessageMissingUserRolesForOrganizationConfigurationRoleTypeApp);
					}
					return new AuthZBehavior.OrganizationConfigurationRoleAuthZBehavior(this.identity, authZBehavior);
				}
			}

			// Token: 0x060035F0 RID: 13808 RVA: 0x000C1A3E File Offset: 0x000BFC3E
			internal override ADRecipientSessionContext GetADRecipientSessionContext()
			{
				return ADRecipientSessionContext.CreateForOrganization(this.identity.OrganizationId);
			}

			// Token: 0x060035F1 RID: 13809 RVA: 0x000C1A50 File Offset: 0x000BFC50
			public override string ToCallerString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.identity.OAuthApplication.Id);
				if (this.managementRoleType != null && this.managementRoleType.HasApplicationRoles)
				{
					foreach (RoleType roleType in this.ApplicationRoleTypes)
					{
						stringBuilder.AppendFormat("_{0}", roleType);
					}
				}
				if (!this.identity.IsAppOnly)
				{
					stringBuilder.Append("_");
					stringBuilder.Append(base.ToCallerString());
				}
				return stringBuilder.ToString();
			}

			// Token: 0x04001E23 RID: 7715
			private static readonly RoleType[] DefaultApplicationRoleTypes = new RoleType[]
			{
				RoleType.UserApplication
			};

			// Token: 0x04001E24 RID: 7716
			private static readonly RoleType[] OfficeExtensionRoleTypes = new RoleType[]
			{
				RoleType.OfficeExtensionApplication
			};

			// Token: 0x04001E25 RID: 7717
			private readonly OAuthIdentity identity;
		}
	}
}
