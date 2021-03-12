using System;
using System.Diagnostics;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Authentication.FederatedAuthService;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000060 RID: 96
	internal class LiveIdBasicAuthentication : ILiveIdBasicAuthentication
	{
		// Token: 0x060002E8 RID: 744 RVA: 0x00017C48 File Offset: 0x00015E48
		private static AuthServiceClient GetClient()
		{
			AuthServiceClient authServiceClient;
			lock (LiveIdBasicAuthentication.clientLock)
			{
				if (LiveIdBasicAuthentication.sharedClient == null)
				{
					LiveIdBasicAuthentication.sharedClient = LiveIdBasicAuthentication.NewClientDelegate();
					LiveIdBasicAuthentication.sharedClient.AddRef();
				}
				authServiceClient = LiveIdBasicAuthentication.sharedClient;
				authServiceClient.AddRef();
			}
			AuthServiceClient authServiceClient2 = authServiceClient;
			lock (authServiceClient2)
			{
				if (authServiceClient2.State == CommunicationState.Closing || authServiceClient2.State == CommunicationState.Closed || authServiceClient2.State == CommunicationState.Faulted)
				{
					authServiceClient2.Release();
					authServiceClient = LiveIdBasicAuthentication.NewClientDelegate();
					authServiceClient.AddRef();
					LiveIdBasicAuthentication.SetClient(authServiceClient);
				}
			}
			return authServiceClient;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00017D10 File Offset: 0x00015F10
		private static void SetClient(AuthServiceClient value)
		{
			lock (LiveIdBasicAuthentication.clientLock)
			{
				if (value != null)
				{
					value.AddRef();
				}
				if (LiveIdBasicAuthentication.sharedClient != null)
				{
					LiveIdBasicAuthentication.sharedClient.Release();
				}
				LiveIdBasicAuthentication.sharedClient = value;
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00017D6C File Offset: 0x00015F6C
		private static void InvalidateClient(AuthServiceClient value)
		{
			lock (LiveIdBasicAuthentication.clientLock)
			{
				if (value == LiveIdBasicAuthentication.sharedClient)
				{
					LiveIdBasicAuthentication.sharedClient.Release();
					LiveIdBasicAuthentication.sharedClient = null;
				}
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00017DC0 File Offset: 0x00015FC0
		~LiveIdBasicAuthentication()
		{
			if (this.instanceClient != null)
			{
				this.instanceClient.Release();
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00017DFC File Offset: 0x00015FFC
		// (set) Token: 0x060002ED RID: 749 RVA: 0x00017E04 File Offset: 0x00016004
		public string ApplicationName
		{
			get
			{
				return this.application;
			}
			set
			{
				this.application = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00017E0D File Offset: 0x0001600D
		// (set) Token: 0x060002EF RID: 751 RVA: 0x00017E15 File Offset: 0x00016015
		public string UserIpAddress
		{
			get
			{
				return this.userAddress;
			}
			set
			{
				this.userAddress = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x00017E1E File Offset: 0x0001601E
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x00017E26 File Offset: 0x00016026
		public string UserAgent
		{
			get
			{
				return this.userAgent;
			}
			set
			{
				this.userAgent = value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00017E2F File Offset: 0x0001602F
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x00017E37 File Offset: 0x00016037
		public bool SyncAD
		{
			get
			{
				return this.syncAD;
			}
			set
			{
				this.syncAD = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00017E40 File Offset: 0x00016040
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x00017E48 File Offset: 0x00016048
		public bool SyncADBackEndOnly
		{
			get
			{
				return this.syncADBackEndOnly;
			}
			set
			{
				this.syncADBackEndOnly = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00017E51 File Offset: 0x00016051
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x00017E59 File Offset: 0x00016059
		public bool SyncUPN { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00017E62 File Offset: 0x00016062
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x00017E6A File Offset: 0x0001606A
		public bool BypassPositiveLogonCache { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00017E73 File Offset: 0x00016073
		// (set) Token: 0x060002FB RID: 763 RVA: 0x00017E7B File Offset: 0x0001607B
		public bool AllowLiveIDOnlyAuth
		{
			get
			{
				return this.allowLiveIDOnlyAuth;
			}
			set
			{
				this.allowLiveIDOnlyAuth = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002FC RID: 764 RVA: 0x00017E84 File Offset: 0x00016084
		// (set) Token: 0x060002FD RID: 765 RVA: 0x00017E8C File Offset: 0x0001608C
		public bool AllowOfflineOrgIdAsPrimeAuth
		{
			get
			{
				return this.allowOfflineOrgIdAsPrimeAuth;
			}
			set
			{
				this.allowOfflineOrgIdAsPrimeAuth = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00017E95 File Offset: 0x00016095
		public string LastRequestErrorMessage
		{
			get
			{
				return this.lastRequestErrorMessage;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00017E9D File Offset: 0x0001609D
		public LiveIdAuthResult LastAuthResult
		{
			get
			{
				return this.lastResult;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00017EA5 File Offset: 0x000160A5
		// (set) Token: 0x06000301 RID: 769 RVA: 0x00017EAD File Offset: 0x000160AD
		public bool RecoverableLogonFailure { get; private set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00017EB6 File Offset: 0x000160B6
		// (set) Token: 0x06000303 RID: 771 RVA: 0x00017EBE File Offset: 0x000160BE
		public bool Tarpit { get; private set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00017EC7 File Offset: 0x000160C7
		// (set) Token: 0x06000305 RID: 773 RVA: 0x00017ECF File Offset: 0x000160CF
		public bool AuthenticatedByOfflineAuth { get; private set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00017ED8 File Offset: 0x000160D8
		// (set) Token: 0x06000307 RID: 775 RVA: 0x00017EE0 File Offset: 0x000160E0
		public LiveIdAuthResult? OfflineOrgIdFailureResult { get; set; }

		// Token: 0x06000308 RID: 776 RVA: 0x00017EE9 File Offset: 0x000160E9
		public SecurityStatus GetWindowsIdentity(byte[] userBytes, byte[] passBytes, out WindowsIdentity identity, out IAccountValidationContext accountValidationContext)
		{
			return this.GetWindowsIdentity(userBytes, passBytes, Guid.Empty, out identity, out accountValidationContext);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00017EFC File Offset: 0x000160FC
		public SecurityStatus GetWindowsIdentity(byte[] userBytes, byte[] passBytes, Guid requestId, out WindowsIdentity identity, out IAccountValidationContext accountValidationContext)
		{
			accountValidationContext = null;
			bool flag;
			identity = this.GetWindowsIdentity(userBytes, passBytes, string.Empty, requestId, out flag);
			if (identity != null)
			{
				if (ConfigBase<AdDriverConfigSchema>.GetConfig<bool>("AccountValidationEnabled"))
				{
					accountValidationContext = new AccountValidationContextBySID(identity.Owner, ExDateTime.UtcNow, this.ApplicationName);
				}
				return SecurityStatus.OK;
			}
			return SecurityStatus.LogonDenied;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00017F53 File Offset: 0x00016153
		public WindowsIdentity GetWindowsIdentity(byte[] userBytes, byte[] passBytes, string remoteOrganizationContext, out bool userNotFoundInAD)
		{
			return this.GetWindowsIdentity(userBytes, passBytes, remoteOrganizationContext, Guid.Empty, out userNotFoundInAD);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00017F68 File Offset: 0x00016168
		public WindowsIdentity GetWindowsIdentity(byte[] userBytes, byte[] passBytes, string remoteOrganizationContext, Guid requestId, out bool userNotFoundInAD)
		{
			IAsyncResult asyncResult = this.BeginGetWindowsIdentity(userBytes, passBytes, remoteOrganizationContext, requestId, null, null);
			LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
			if (lazyAsyncResult != null)
			{
				lazyAsyncResult.InternalWaitForCompletion();
			}
			else
			{
				asyncResult.AsyncWaitHandle.WaitOne();
				asyncResult.AsyncWaitHandle.Close();
			}
			WindowsIdentity result;
			LiveIdAuthResult liveIdAuthResult = this.EndGetWindowsIdentity(asyncResult, out result);
			userNotFoundInAD = (liveIdAuthResult == LiveIdAuthResult.UserNotFoundInAD);
			return result;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00017FBF File Offset: 0x000161BF
		public IAsyncResult BeginGetWindowsIdentity(byte[] userBytes, byte[] passBytes, AsyncCallback callback, object state, Guid requestId = default(Guid))
		{
			return this.BeginGetWindowsIdentity(userBytes, passBytes, string.Empty, requestId, callback, state);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00017FD3 File Offset: 0x000161D3
		public IAsyncResult BeginGetWindowsIdentity(byte[] userBytes, byte[] passBytes, string remoteOrganizationContext, AsyncCallback callback, object state)
		{
			return this.BeginGetWindowsIdentity(userBytes, passBytes, remoteOrganizationContext, Guid.Empty, callback, state);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00018088 File Offset: 0x00016288
		public IAsyncResult BeginGetWindowsIdentity(byte[] userBytes, byte[] passBytes, string remoteOrganizationContext, Guid requestId, AsyncCallback callback, object state)
		{
			return this.BeginGetAuthToken(userBytes, passBytes, callback, state, delegate
			{
				IAsyncResult result;
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					result = this.instanceClient.BeginLogonUserFederationCredsAsync((uint)currentProcess.Id, userBytes, passBytes, remoteOrganizationContext, this.syncAD, this.application, this.userAgent, this.userAddress, requestId, callback, state);
				}
				return result;
			});
		}

		// Token: 0x0600030F RID: 783 RVA: 0x000181B4 File Offset: 0x000163B4
		public LiveIdAuthResult EndGetWindowsIdentity(IAsyncResult ar, out WindowsIdentity identity)
		{
			WindowsIdentity localIdentity = null;
			LiveIdAuthResult result = this.EndGetAuthToken(ar, delegate
			{
				AuthStatus result2 = AuthStatus.LogonFailed;
				SafeUserTokenHandle safeUserTokenHandle = new SafeUserTokenHandle(this.instanceClient.EndLogonUserFederationCredsAsync(out this.lastRequestErrorMessage, ar));
				using (safeUserTokenHandle)
				{
					int num = safeUserTokenHandle.DangerousGetHandle().ToInt32();
					if (num >= -29 && num <= 0)
					{
						result2 = (AuthStatus)num;
					}
					else if (!safeUserTokenHandle.IsInvalid)
					{
						localIdentity = new WindowsIdentity(safeUserTokenHandle.DangerousGetHandle(), "Kerberos", WindowsAccountType.Normal, true);
						result2 = AuthStatus.LogonSuccess;
					}
					if (num == -1 && this.allowLiveIDOnlyAuth)
					{
						localIdentity = WindowsIdentity.GetCurrent();
					}
				}
				return result2;
			});
			identity = localIdentity;
			return result;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00018200 File Offset: 0x00016400
		public SecurityStatus GetCommonAccessToken(byte[] userBytes, byte[] passBytes, Guid requestId, out string commonAccessToken, out IAccountValidationContext accountValidationContext)
		{
			accountValidationContext = null;
			bool flag;
			commonAccessToken = this.GetCommonAccessToken(userBytes, passBytes, string.Empty, out flag, requestId);
			if (!string.IsNullOrEmpty(commonAccessToken))
			{
				if (ConfigBase<AdDriverConfigSchema>.GetConfig<bool>("AccountValidationEnabled"))
				{
					CommonAccessToken commonAccessToken2 = CommonAccessToken.Deserialize(commonAccessToken);
					ExDateTime utcNow;
					if (!commonAccessToken2.ExtensionData.ContainsKey("CreateTime") || !ExDateTime.TryParse(commonAccessToken2.ExtensionData["CreateTime"], out utcNow))
					{
						utcNow = ExDateTime.UtcNow;
					}
					string puid = commonAccessToken2.ExtensionData["Puid"];
					accountValidationContext = new AccountValidationContextByPUID(puid, utcNow, this.ApplicationName);
				}
				return SecurityStatus.OK;
			}
			return SecurityStatus.LogonDenied;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0001829C File Offset: 0x0001649C
		public string GetCommonAccessToken(byte[] userBytes, byte[] passBytes, string remoteOrganizationContext, out bool userNotFoundInAD, Guid requestId = default(Guid))
		{
			IAsyncResult asyncResult = this.BeginGetCommonAccessToken(userBytes, passBytes, remoteOrganizationContext, requestId, null, null);
			LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
			if (lazyAsyncResult != null)
			{
				lazyAsyncResult.InternalWaitForCompletion();
			}
			else
			{
				asyncResult.AsyncWaitHandle.WaitOne();
				asyncResult.AsyncWaitHandle.Close();
			}
			string result;
			LiveIdAuthResult liveIdAuthResult = this.EndGetCommonAccessToken(asyncResult, out result);
			userNotFoundInAD = (liveIdAuthResult == LiveIdAuthResult.UserNotFoundInAD);
			return result;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000182F3 File Offset: 0x000164F3
		public IAsyncResult BeginGetCommonAccessToken(byte[] userBytes, byte[] passBytes, AsyncCallback callback, object state)
		{
			return this.BeginGetCommonAccessToken(userBytes, passBytes, Guid.Empty, callback, state);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00018305 File Offset: 0x00016505
		public IAsyncResult BeginGetCommonAccessToken(byte[] userBytes, byte[] passBytes, Guid requestId, AsyncCallback callback, object state)
		{
			return this.BeginGetCommonAccessToken(userBytes, passBytes, string.Empty, requestId, callback, state);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000183B8 File Offset: 0x000165B8
		public IAsyncResult BeginGetCommonAccessToken(byte[] userBytes, byte[] passBytes, string remoteOrganizationContext, Guid requestId, AsyncCallback callback, object state)
		{
			AuthOptions flags = AuthOptions.None;
			if (this.SyncAD)
			{
				flags |= AuthOptions.SyncAD;
			}
			if (this.SyncADBackEndOnly)
			{
				flags |= AuthOptions.SyncADBackEndOnly;
			}
			if (this.BypassPositiveLogonCache)
			{
				flags |= AuthOptions.BypassPositiveCache;
			}
			if (this.SyncUPN)
			{
				flags |= AuthOptions.SyncUPN;
			}
			return this.BeginGetAuthToken(userBytes, passBytes, callback, state, delegate
			{
				IAsyncResult result;
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					result = this.instanceClient.BeginLogonCommonAccessTokenFederationCredsAsync((uint)currentProcess.Id, userBytes, passBytes, flags, remoteOrganizationContext, this.application, this.userAgent, this.userAddress, requestId, callback, state);
				}
				return result;
			});
		}

		// Token: 0x06000315 RID: 789 RVA: 0x000184C0 File Offset: 0x000166C0
		public LiveIdAuthResult EndGetCommonAccessToken(IAsyncResult ar, out string commonAccessToken)
		{
			string localCat = null;
			LiveIdAuthResult result = this.EndGetAuthToken(ar, () => this.instanceClient.EndLogonCommonAccessTokenFederationCredsAsync(out localCat, out this.lastRequestErrorMessage, ar));
			commonAccessToken = localCat;
			return result;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0001850C File Offset: 0x0001670C
		private IAsyncResult BeginGetAuthToken(byte[] userBytes, byte[] passBytes, AsyncCallback callback, object state, Func<IAsyncResult> beginAuthToken)
		{
			this.lastRequestErrorMessage = null;
			this.RecoverableLogonFailure = false;
			this.Tarpit = false;
			string @string;
			if (userBytes != null && userBytes.Length > 0 && userBytes[userBytes.Length - 1] == 0)
			{
				@string = Encoding.Default.GetString(userBytes, 0, userBytes.Length - 1);
			}
			else
			{
				@string = Encoding.Default.GetString(userBytes);
			}
			if (!SmtpAddress.IsValidSmtpAddress(@string))
			{
				this.beginResult = LiveIdAuthResult.InvalidUsername;
				this.lastResult = this.beginResult;
				this.lastRequestErrorMessage = "member name is not a valid SMTP address";
				IAsyncResult asyncResult = new LazyAsyncResult(this, state, callback);
				((LazyAsyncResult)asyncResult).InvokeCallback();
				return asyncResult;
			}
			return this.BeginOperation(callback, state, beginAuthToken);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x000185AC File Offset: 0x000167AC
		private LiveIdAuthResult EndGetAuthToken(IAsyncResult ar, Func<AuthStatus> endAuthToken)
		{
			LiveIdAuthResult result = LiveIdAuthResult.AuthFailure;
			try
			{
				LazyAsyncResult lazyAsyncResult = ar as LazyAsyncResult;
				if (lazyAsyncResult != null)
				{
					result = this.beginResult;
				}
				else
				{
					AuthStatus authStatus = endAuthToken();
					if (!string.IsNullOrEmpty(this.lastRequestErrorMessage) && this.lastRequestErrorMessage.IndexOf("AuthenticatedBy:OfflineOrgId.") >= 0)
					{
						this.AuthenticatedByOfflineAuth = true;
						Match match = LiveIdBasicAuthentication.offlineOrgIdResultRegex.Match(this.lastRequestErrorMessage);
						AuthStatus authStatus2;
						if (match.Success && Enum.TryParse<AuthStatus>(match.Groups[1].Value, out authStatus2))
						{
							AuthStatus authStatus3 = authStatus2;
							if (authStatus3 != AuthStatus.OfflineHrdFailed)
							{
								switch (authStatus3)
								{
								case AuthStatus.AmbigiousMailboxFound:
									this.OfflineOrgIdFailureResult = new LiveIdAuthResult?(LiveIdAuthResult.AmbigiousMailboxFoundFailure);
									break;
								case AuthStatus.OffineOrgIdAuthFailed:
									this.OfflineOrgIdFailureResult = new LiveIdAuthResult?(LiveIdAuthResult.OfflineOrgIdAuthFailure);
									break;
								default:
									if (authStatus3 == AuthStatus.LowConfidence)
									{
										this.OfflineOrgIdFailureResult = new LiveIdAuthResult?(LiveIdAuthResult.LowPasswordConfidence);
									}
									break;
								}
							}
							else
							{
								this.OfflineOrgIdFailureResult = new LiveIdAuthResult?(LiveIdAuthResult.OfflineHrdFailed);
							}
						}
					}
					switch (authStatus)
					{
					case AuthStatus.UnfamiliarLocation:
						result = LiveIdAuthResult.UnfamiliarLocation;
						break;
					case AuthStatus.Forbidden:
						result = LiveIdAuthResult.Forbidden;
						break;
					case AuthStatus.InternalServerError:
						result = LiveIdAuthResult.InternalServerError;
						break;
					case AuthStatus.AccountNotProvisioned:
						result = LiveIdAuthResult.AccountNotProvisioned;
						break;
					case AuthStatus.RepeatedADFSRulesDenied:
					case AuthStatus.ADFSRulesDenied:
						result = LiveIdAuthResult.FederatedStsADFSRulesDenied;
						break;
					case AuthStatus.FederatedStsUrlNotEncrypted:
						result = LiveIdAuthResult.FederatedStsUrlNotEncrypted;
						break;
					case AuthStatus.AppPasswordRequired:
						result = LiveIdAuthResult.AppPasswordRequired;
						this.RecoverableLogonFailure = true;
						break;
					case AuthStatus.OfflineHrdFailed:
					case AuthStatus.HRDFailed:
						result = LiveIdAuthResult.HRDFailure;
						break;
					case AuthStatus.PuidNotFound:
					case AuthStatus.Redirect:
						result = LiveIdAuthResult.UserNotFoundInAD;
						break;
					case AuthStatus.PuidMismatch:
						result = LiveIdAuthResult.PuidMismatchFailure;
						break;
					case AuthStatus.UnableToOpenTicket:
						result = LiveIdAuthResult.UnableToOpenTicketFailure;
						break;
					case AuthStatus.AmbigiousMailboxFound:
						result = LiveIdAuthResult.AmbigiousMailboxFoundFailure;
						break;
					case AuthStatus.OffineOrgIdAuthFailed:
						result = LiveIdAuthResult.OfflineOrgIdAuthFailure;
						break;
					case AuthStatus.S4ULogonFailed:
						result = LiveIdAuthResult.S4ULogonFailure;
						break;
					case AuthStatus.RepeatedBadPassword:
					case AuthStatus.BadPassword:
						result = LiveIdAuthResult.InvalidCreds;
						break;
					case AuthStatus.LowConfidence:
						result = LiveIdAuthResult.LowPasswordConfidence;
						break;
					case AuthStatus.RepeatedExpiredCredentials:
					case AuthStatus.ExpiredCredentials:
						result = LiveIdAuthResult.ExpiredCreds;
						this.RecoverableLogonFailure = true;
						break;
					case AuthStatus.RepeatedRecoverableFailure:
					case AuthStatus.RecoverableLogonFailed:
						result = LiveIdAuthResult.RecoverableAuthFailure;
						this.RecoverableLogonFailure = true;
						break;
					case AuthStatus.RepeatedFederatedStsFailure:
					case AuthStatus.FederatedStsFailed:
						result = LiveIdAuthResult.FederatedStsUnreachable;
						break;
					case AuthStatus.RepeatedLiveIDFailure:
					case AuthStatus.LiveIDFailed:
						result = LiveIdAuthResult.LiveServerUnreachable;
						break;
					case AuthStatus.RepeatedLogonFailure:
					case AuthStatus.LogonFailed:
						result = LiveIdAuthResult.AuthFailure;
						break;
					case AuthStatus.LogonSuccess:
						result = LiveIdAuthResult.Success;
						break;
					default:
						result = LiveIdAuthResult.AuthFailure;
						break;
					}
					switch (authStatus)
					{
					case AuthStatus.RepeatedBadPassword:
					case AuthStatus.RepeatedExpiredCredentials:
					case AuthStatus.RepeatedRecoverableFailure:
					case AuthStatus.RepeatedFederatedStsFailure:
					case AuthStatus.RepeatedLogonFailure:
						this.Tarpit = true;
						break;
					}
				}
			}
			catch (InvalidOperationException ex)
			{
				this.lastRequestErrorMessage = string.Format("{0}", ex.Message);
				ExTraceGlobals.AuthenticationTracer.TraceWarning<string>((long)this.GetHashCode(), "Invalid Operation Exception while retrieving result from LiveIdBasic service: {0}", this.lastRequestErrorMessage);
				result = LiveIdAuthResult.CommunicationFailure;
				LiveIdBasicAuthentication.eventLogger.LogEvent(SecurityEventLogConstants.Tuple_GeneralClientException, "InvalidOperationException", new object[]
				{
					(uint)Process.GetCurrentProcess().Id,
					ex + "\n" + ((ex.InnerException != null) ? ex.InnerException.ToString() : string.Empty)
				});
			}
			catch (TimeoutException ex2)
			{
				this.lastRequestErrorMessage = string.Format("{0}", ex2.Message);
				ExTraceGlobals.AuthenticationTracer.TraceWarning<string>((long)this.GetHashCode(), "Timed out while trying to query the LiveIdBasic service: {0}", this.lastRequestErrorMessage);
				result = LiveIdAuthResult.OperationTimedOut;
			}
			catch (FaultException ex3)
			{
				this.lastRequestErrorMessage = string.Format("{0}", (ex3.InnerException != null) ? ex3.InnerException.ToString() : ex3.ToString());
				ExTraceGlobals.AuthenticationTracer.TraceWarning<string>((long)this.GetHashCode(), "Fault Exception while retrieving result from LiveIdBasic service: {0}", this.lastRequestErrorMessage);
				result = LiveIdAuthResult.FaultException;
			}
			catch (CommunicationException ex4)
			{
				this.lastRequestErrorMessage = string.Format("{0}", (ex4.InnerException != null) ? ex4.InnerException.ToString() : ex4.ToString());
				ExTraceGlobals.AuthenticationTracer.TraceWarning<string>((long)this.GetHashCode(), "Communication Exception while retrieving result from LiveIdBasic service: {0}", this.lastRequestErrorMessage);
				result = LiveIdAuthResult.CommunicationFailure;
				LiveIdBasicAuthentication.eventLogger.LogEvent(SecurityEventLogConstants.Tuple_GeneralClientException, "CommunicationException", new object[]
				{
					(uint)Process.GetCurrentProcess().Id,
					ex4 + "\n" + ((ex4.InnerException != null) ? ex4.InnerException.ToString() : string.Empty)
				});
			}
			finally
			{
				this.lastResult = result;
				if (this.instanceClient != null)
				{
					this.instanceClient.Release();
					this.instanceClient = null;
				}
			}
			return result;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00018A54 File Offset: 0x00016C54
		public LiveIdAuthResult SyncADPassword(string puid, byte[] userBytes, byte[] passBytes, string remoteOrganizationContext, bool syncHrd)
		{
			IAsyncResult asyncResult = this.BeginSyncADPassword(puid, userBytes, passBytes, remoteOrganizationContext, null, null, Guid.Empty, syncHrd);
			LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
			if (lazyAsyncResult != null)
			{
				lazyAsyncResult.InternalWaitForCompletion();
			}
			else
			{
				asyncResult.AsyncWaitHandle.WaitOne();
				asyncResult.AsyncWaitHandle.Close();
			}
			return this.EndSyncADPassword(asyncResult);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00018B48 File Offset: 0x00016D48
		public IAsyncResult BeginSyncADPassword(string puid, byte[] userBytes, byte[] passBytes, string remoteOrganizationContext, AsyncCallback callback, object state, Guid requestId = default(Guid), bool syncHrd = false)
		{
			if (string.IsNullOrEmpty(puid))
			{
				throw new ArgumentException("puid is null or empty");
			}
			byte[] puidPlusUserBytes;
			if (syncHrd)
			{
				puidPlusUserBytes = new byte[userBytes.Length + puid.Length + 2 + "true".Length];
			}
			else
			{
				puidPlusUserBytes = new byte[userBytes.Length + puid.Length + 1];
			}
			Array.Copy(Encoding.ASCII.GetBytes(puid), puidPlusUserBytes, puid.Length);
			puidPlusUserBytes[puid.Length] = 58;
			Array.Copy(userBytes, 0, puidPlusUserBytes, puid.Length + 1, userBytes.Length);
			if (syncHrd)
			{
				puidPlusUserBytes[userBytes.Length + puid.Length + 1] = 58;
				Array.Copy(Encoding.ASCII.GetBytes("true"), 0, puidPlusUserBytes, userBytes.Length + puid.Length + 2, "true".Length);
			}
			AuthOptions options = AuthOptions.SyncAD | AuthOptions.PasswordAndHRDSync;
			if (this.SyncUPN)
			{
				options |= AuthOptions.SyncUPN;
			}
			return this.BeginGetAuthToken(userBytes, passBytes, callback, state, delegate
			{
				IAsyncResult result;
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					result = this.instanceClient.BeginLogonCommonAccessTokenFederationCredsAsync((uint)currentProcess.Id, puidPlusUserBytes, passBytes, options, remoteOrganizationContext, this.application, this.userAgent, this.userAddress, requestId, callback, state);
				}
				return result;
			});
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00018CE8 File Offset: 0x00016EE8
		public LiveIdAuthResult EndSyncADPassword(IAsyncResult ar)
		{
			return this.EndGetAuthToken(ar, delegate
			{
				string text;
				return this.instanceClient.EndLogonCommonAccessTokenFederationCredsAsync(out text, out this.lastRequestErrorMessage, ar);
			});
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00018D24 File Offset: 0x00016F24
		public bool IsNego2AuthEnabledForDomain(string domain, out bool nego2Enabled)
		{
			AuthServiceClient authServiceClient = null;
			bool flag = false;
			bool flag2 = true;
			nego2Enabled = false;
			do
			{
				try
				{
					authServiceClient = LiveIdBasicAuthentication.GetClient();
					nego2Enabled = authServiceClient.IsNego2AuthEnabledForDomain(domain);
					flag2 = false;
					flag = false;
				}
				catch (Exception ex)
				{
					if (ex is CommunicationException)
					{
						ExTraceGlobals.AuthenticationTracer.TraceWarning<Exception>((long)this.GetHashCode(), "CommunicationException {0}", ex);
					}
					else if (ex is InvalidOperationException)
					{
						ExTraceGlobals.AuthenticationTracer.TraceWarning<Exception>((long)this.GetHashCode(), "InvalidOperationException {0}", ex);
					}
					else
					{
						if (!(ex is TimeoutException))
						{
							throw;
						}
						ExTraceGlobals.AuthenticationTracer.TraceWarning<Exception>((long)this.GetHashCode(), "TimeoutException {0}", ex);
					}
					if (flag)
					{
						LiveIdBasicAuthentication.eventLogger.LogEvent(SecurityEventLogConstants.Tuple_CannotConnectToAuthService, "CannotConnectToAuthService", new object[]
						{
							(uint)Process.GetCurrentProcess().Id,
							ex.Message,
							(ex.InnerException != null) ? ex.InnerException.ToString() : string.Empty
						});
						return false;
					}
					flag = true;
				}
				finally
				{
					if (authServiceClient != null)
					{
						if (flag2)
						{
							LiveIdBasicAuthentication.InvalidateClient(authServiceClient);
						}
						authServiceClient.Release();
						authServiceClient = null;
					}
				}
			}
			while (flag);
			return !flag2;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00018E60 File Offset: 0x00017060
		private IAsyncResult BeginOperation(AsyncCallback callback, object state, Func<IAsyncResult> beginOperation)
		{
			this.lastRequestErrorMessage = null;
			this.beginResult = LiveIdAuthResult.Success;
			IAsyncResult asyncResult = null;
			bool flag = false;
			if (this.instanceClient != null)
			{
				throw new InvalidOperationException("You cannot call Begin twice without first calling End on a single instance of LiveIdBasicAuthentication");
			}
			do
			{
				bool flag2 = false;
				try
				{
					this.instanceClient = LiveIdBasicAuthentication.GetClient();
					flag2 = true;
					asyncResult = beginOperation();
					flag2 = false;
					flag = false;
				}
				catch (Exception ex)
				{
					if (ex is CommunicationException)
					{
						ExTraceGlobals.AuthenticationTracer.TraceWarning<Exception>((long)this.GetHashCode(), "CommunicationException {0}", ex);
					}
					else if (ex is InvalidOperationException)
					{
						ExTraceGlobals.AuthenticationTracer.TraceWarning<Exception>((long)this.GetHashCode(), "InvalidOperationException {0}", ex);
					}
					else
					{
						if (!(ex is TimeoutException))
						{
							throw;
						}
						ExTraceGlobals.AuthenticationTracer.TraceWarning<Exception>((long)this.GetHashCode(), "TimeoutException {0}", ex);
					}
					if (flag)
					{
						LiveIdBasicAuthentication.eventLogger.LogEvent(SecurityEventLogConstants.Tuple_CannotConnectToAuthService, "CannotConnectToAuthService", new object[]
						{
							(uint)Process.GetCurrentProcess().Id,
							ex.GetHashCode() + ex.Message,
							(ex.InnerException != null) ? ex.InnerException.ToString() : string.Empty
						});
						this.beginResult = LiveIdAuthResult.CommunicationFailure;
						this.lastRequestErrorMessage = string.Format("{0}", (ex.InnerException != null) ? ex.InnerException.ToString() : ex.ToString());
						asyncResult = new LazyAsyncResult(this, state, callback);
						((LazyAsyncResult)asyncResult).InvokeCallback(ex);
						return asyncResult;
					}
					flag = true;
				}
				finally
				{
					this.lastResult = this.beginResult;
					if (flag2)
					{
						flag2 = false;
						if (this.instanceClient != null)
						{
							LiveIdBasicAuthentication.InvalidateClient(this.instanceClient);
							this.instanceClient.Release();
							this.instanceClient = null;
						}
					}
				}
			}
			while (flag);
			return asyncResult;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00019050 File Offset: 0x00017250
		private static AuthServiceClient CreateAuthServiceClient()
		{
			AuthServiceClient authServiceClient;
			try
			{
				authServiceClient = new AuthServiceClient();
			}
			catch (Exception ex)
			{
				LiveIdBasicAuthentication.eventLogger.LogEvent(SecurityEventLogConstants.Tuple_CannotConnectToAuthService, "CannotFindEndPointConfig", new object[]
				{
					(uint)Process.GetCurrentProcess().Id,
					ex.GetHashCode() + ex.Message,
					(ex.InnerException != null) ? ex.InnerException.ToString() : string.Empty
				});
				int num = 1009;
				string uri = string.Format("net.tcp://{0}:{1}/Microsoft.Exchange.Security.Authentication.FederatedAuthService", "localhost", num);
				NetTcpBinding binding = new NetTcpBinding(SecurityMode.Transport);
				EndpointAddress remoteAddress = new EndpointAddress(uri);
				authServiceClient = new AuthServiceClient(binding, remoteAddress);
			}
			authServiceClient.Open();
			return authServiceClient;
		}

		// Token: 0x040002B9 RID: 697
		public static readonly string LiveIdComponent = "MSExchange LiveIdBasicAuthentication";

		// Token: 0x040002BA RID: 698
		public static LiveIdBasicAuthentication.NewAuthServiceClient NewClientDelegate = () => LiveIdBasicAuthentication.CreateAuthServiceClient();

		// Token: 0x040002BB RID: 699
		private static readonly Regex offlineOrgIdResultRegex = new Regex("<OfflineOrgIdAuthResult=(.*?)>", RegexOptions.Compiled);

		// Token: 0x040002BC RID: 700
		private bool syncAD;

		// Token: 0x040002BD RID: 701
		private bool syncADBackEndOnly;

		// Token: 0x040002BE RID: 702
		private bool allowOfflineOrgIdAsPrimeAuth;

		// Token: 0x040002BF RID: 703
		private bool allowLiveIDOnlyAuth;

		// Token: 0x040002C0 RID: 704
		private string userAgent;

		// Token: 0x040002C1 RID: 705
		private string userAddress;

		// Token: 0x040002C2 RID: 706
		private string application;

		// Token: 0x040002C3 RID: 707
		private string lastRequestErrorMessage;

		// Token: 0x040002C4 RID: 708
		private LiveIdAuthResult beginResult;

		// Token: 0x040002C5 RID: 709
		private LiveIdAuthResult lastResult;

		// Token: 0x040002C6 RID: 710
		private static AuthServiceClient sharedClient;

		// Token: 0x040002C7 RID: 711
		private static readonly object clientLock = new object();

		// Token: 0x040002C8 RID: 712
		private AuthServiceClient instanceClient;

		// Token: 0x040002C9 RID: 713
		private static readonly ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.AuthenticationTracer.Category, LiveIdBasicAuthentication.LiveIdComponent);

		// Token: 0x02000061 RID: 97
		// (Invoke) Token: 0x06000322 RID: 802
		public delegate AuthServiceClient NewAuthServiceClient();
	}
}
