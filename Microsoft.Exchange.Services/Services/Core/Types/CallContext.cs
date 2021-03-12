using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using System.Web;
using Microsoft.Exchange.AirSync;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ClientAccessRules;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Services.DispatchPipe.Ews;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000707 RID: 1799
	internal class CallContext : IDisposeTrackable, IDisposable
	{
		// Token: 0x06003651 RID: 13905 RVA: 0x000C2350 File Offset: 0x000C0550
		internal static void SetCurrent(CallContext callContext)
		{
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, (callContext == null) ? "<Null>" : callContext.ToString());
			CallContext callContext2 = CallContext.Current;
			CallContext.current = callContext;
			if (callContext != null && !callContext.isDisposed)
			{
				CallContext.AssertContextVariables(callContext.HttpContext, callContext.ProtocolLog);
				if (callContext.ProtocolLog != null && callContext.ProtocolLog.ActivityScope != null)
				{
					ActivityContext.SetThreadScope(callContext.ProtocolLog.ActivityScope);
				}
				if (callContext.OwaCulture != null)
				{
					Thread.CurrentThread.CurrentCulture = callContext.OwaCulture;
					Thread.CurrentThread.CurrentUICulture = callContext.OwaCulture;
					return;
				}
			}
			else
			{
				ActivityContext.ClearThreadScope();
				if (callContext2 != null)
				{
					if (callContext2.previousThreadCulture != null)
					{
						Thread.CurrentThread.CurrentCulture = callContext2.previousThreadCulture;
						callContext2.previousThreadCulture = null;
					}
					if (callContext2.previousThreadUICulture != null)
					{
						Thread.CurrentThread.CurrentUICulture = callContext2.previousThreadUICulture;
						callContext2.previousThreadUICulture = null;
					}
				}
			}
		}

		// Token: 0x06003652 RID: 13906 RVA: 0x000C2438 File Offset: 0x000C0638
		internal static void ClearCallContextForCurrentThread()
		{
			if (CallContext.current != null)
			{
				string arg = CallContext.current.MethodName;
				bool arg2 = CallContext.current.isDisposed;
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<string, bool>((long)CallContext.current.GetHashCode(), "[CallContext::ResetCallContextForThread] CallContext.current is non-null. Was created for method name: {0}. IsDisposed {1}. ", arg, arg2);
				CallContext.current = null;
			}
		}

		// Token: 0x06003653 RID: 13907 RVA: 0x000C2484 File Offset: 0x000C0684
		private static void DisposeObject(IDisposable disposable)
		{
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}

		// Token: 0x06003654 RID: 13908 RVA: 0x000C248F File Offset: 0x000C068F
		private static bool CanUserAgentUseBackingCache(string userAgent)
		{
			return string.IsNullOrEmpty(userAgent) || !userAgent.Contains("MSEXCHMON");
		}

		// Token: 0x06003655 RID: 13909 RVA: 0x000C24A9 File Offset: 0x000C06A9
		private bool MatchesUserAgent(string pattern)
		{
			return UserAgentPattern.IsMatch(pattern, this.userAgent);
		}

		// Token: 0x06003656 RID: 13910 RVA: 0x000C24B7 File Offset: 0x000C06B7
		internal int GetAccessingPrincipalServerVersion()
		{
			if (this.AccessingPrincipal == null)
			{
				return BaseServerIdInfo.InvalidServerVersion;
			}
			if (this.AccessingPrincipal is RemoteUserMailboxPrincipal)
			{
				return Server.E14SP1MinVersion;
			}
			return this.AccessingPrincipal.MailboxInfo.Location.ServerVersion;
		}

		// Token: 0x06003657 RID: 13911 RVA: 0x000C24F0 File Offset: 0x000C06F0
		private CallContext(AppWideStoreSessionCache mailboxSessionBackingCache, AcceptedDomainCache acceptedDomainCache, UserWorkloadManager workloadManager, ProxyCASStatus proxyCASStatus, AuthZClientInfo effectiveCallerClientInfo, ExchangePrincipal effectiveCallerExchangePrincipal, ADRecipientSessionContext adRecipientSessionContext, MailboxAccessType mailboxAccessType, ProxyRequestType? availabilityProxyRequestType, CultureInfo serverCulture, CultureInfo clientCulture, string userAgent, bool isWSSecurityUser, CallContext.UserKind userKind, OriginalCallerContext originalCallerContext, RequestedLogonType requestedLogonType, WebMethodEntry webMethodEntry, AuthZBehavior authZBehavior) : this()
		{
			this.workloadManager = workloadManager;
			this.acceptedDomainCache = acceptedDomainCache;
			this.proxyCASStatus = proxyCASStatus;
			this.mailboxAccessType = mailboxAccessType;
			this.availabilityProxyRequestType = availabilityProxyRequestType;
			this.effectiveCallerAuthZClientInfo = effectiveCallerClientInfo;
			this.effectiveCallerExchangePrincipal = effectiveCallerExchangePrincipal;
			this.adRecipientSessionContext = adRecipientSessionContext;
			this.serverCulture = serverCulture;
			this.clientCulture = clientCulture;
			this.userAgent = userAgent;
			this.isWSSecurityUser = isWSSecurityUser;
			this.userKind = userKind;
			this.sessionCache = new SessionCache(mailboxSessionBackingCache, this);
			this.originalCallerContext = originalCallerContext;
			this.requestedLogonType = requestedLogonType;
			this.webMethodEntry = webMethodEntry;
			this.authZBehavior = authZBehavior;
			if (Global.EnableMailboxLogger && this.WorkloadType == WorkloadType.Ews && SyncStateStorage.GetMailboxLoggingEnabled(this.SessionCache.GetMailboxIdentityMailboxSession(), null))
			{
				this.mailboxLogger = new MailboxLoggerHandler(this.SessionCache.GetMailboxIdentityMailboxSession(), "EWS", "All", false);
				this.mailboxLogger.SetData(MailboxLogDataName.RequestHeader, "Test Header");
				this.mailboxLogger.SaveLogToMailbox();
			}
		}

		// Token: 0x06003658 RID: 13912 RVA: 0x000C25F7 File Offset: 0x000C07F7
		protected CallContext(HttpContext httpContext, EwsOperationContextBase operationContext, RequestDetailsLogger requestDetailsLogger) : this(httpContext, operationContext, requestDetailsLogger, false)
		{
		}

		// Token: 0x06003659 RID: 13913 RVA: 0x000C2604 File Offset: 0x000C0804
		protected CallContext(HttpContext httpContext, EwsOperationContextBase operationContext, RequestDetailsLogger requestDetailsLogger, bool isMock)
		{
			this.AssertContexts(httpContext, operationContext, requestDetailsLogger);
			this.httpContext = httpContext;
			this.operationContext = operationContext;
			this.protocolLog = requestDetailsLogger;
			if (!isMock)
			{
				this.isWebSocketRequest = ((httpContext.Items != null && httpContext.Items["IsWebSocketRequest"] != null) || httpContext.IsWebSocketRequest);
			}
			this.InitializeOwaFields();
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x0600365A RID: 13914 RVA: 0x000C2699 File Offset: 0x000C0899
		protected CallContext() : this(HttpContext.Current, EwsOperationContextBase.Current, RequestDetailsLogger.Current)
		{
		}

		// Token: 0x0600365B RID: 13915 RVA: 0x000C26B0 File Offset: 0x000C08B0
		protected void CheckDisposed()
		{
			if (this.isDisposed)
			{
				string objectName = string.Format("Instance of {0} for method {1} was already disposed on thread {2}, accessed on thread {3}", new object[]
				{
					base.GetType().Name,
					this.MethodName,
					this.disposerThreadId,
					Thread.CurrentThread.ManagedThreadId
				});
				throw new ObjectDisposedException(objectName);
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x0600365C RID: 13916 RVA: 0x000C2716 File Offset: 0x000C0916
		public ProxyCASStatus ProxyCASStatus
		{
			get
			{
				return this.proxyCASStatus;
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x0600365D RID: 13917 RVA: 0x000C271E File Offset: 0x000C091E
		internal ProxyRequestType? AvailabilityProxyRequestType
		{
			get
			{
				return this.availabilityProxyRequestType;
			}
		}

		// Token: 0x0600365E RID: 13918 RVA: 0x000C2726 File Offset: 0x000C0926
		public virtual string GetEffectiveAccessingSmtpAddress()
		{
			return this.EffectiveCaller.PrimarySmtpAddress;
		}

		// Token: 0x0600365F RID: 13919 RVA: 0x000C2733 File Offset: 0x000C0933
		public CultureInfo GetSessionCulture(StoreSession session)
		{
			if (!session.MailboxOwner.MailboxInfo.IsArchive)
			{
				return session.Culture;
			}
			return this.clientCulture;
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06003660 RID: 13920 RVA: 0x000C2754 File Offset: 0x000C0954
		// (set) Token: 0x06003661 RID: 13921 RVA: 0x000C275C File Offset: 0x000C095C
		public string OwaUserContextKey
		{
			get
			{
				return this.owaUserContextKey;
			}
			set
			{
				this.owaUserContextKey = value;
			}
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06003662 RID: 13922 RVA: 0x000C2765 File Offset: 0x000C0965
		public string SoapAction
		{
			get
			{
				return this.soapAction;
			}
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06003663 RID: 13923 RVA: 0x000C276D File Offset: 0x000C096D
		public MailboxAccessType MailboxAccessType
		{
			get
			{
				return this.mailboxAccessType;
			}
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06003664 RID: 13924 RVA: 0x000C2775 File Offset: 0x000C0975
		public ADUser AccessingADUser
		{
			get
			{
				if (this.EffectiveCaller.UserIdentity != null)
				{
					return this.EffectiveCaller.UserIdentity.ADUser;
				}
				return null;
			}
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06003665 RID: 13925 RVA: 0x000C2796 File Offset: 0x000C0996
		public ExchangePrincipal AccessingPrincipal
		{
			get
			{
				return this.effectiveCallerExchangePrincipal;
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06003666 RID: 13926 RVA: 0x000C27A0 File Offset: 0x000C09A0
		public string OrganizationalUnitName
		{
			get
			{
				if (this.AccessingPrincipal == null)
				{
					return string.Empty;
				}
				if (this.AccessingPrincipal.MailboxInfo != null && this.AccessingPrincipal.MailboxInfo.OrganizationId != null && this.AccessingPrincipal.MailboxInfo.OrganizationId.OrganizationalUnit != null)
				{
					return this.AccessingPrincipal.MailboxInfo.OrganizationId.OrganizationalUnit.Name;
				}
				if (this.AccessingPrincipal.ObjectId != null && this.AccessingPrincipal.ObjectId.DomainId != null)
				{
					return this.AccessingPrincipal.ObjectId.DomainId.ToCanonicalName();
				}
				return string.Empty;
			}
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06003667 RID: 13927 RVA: 0x000C284C File Offset: 0x000C0A4C
		public ExchangePrincipal MailboxIdentityPrincipal
		{
			get
			{
				if (!string.IsNullOrEmpty(this.OwaExplicitLogonUser))
				{
					return ExchangePrincipalCache.GetFromCache(this.OwaExplicitLogonUser, this.ADRecipientSessionContext);
				}
				return this.AccessingPrincipal;
			}
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06003668 RID: 13928 RVA: 0x000C2873 File Offset: 0x000C0A73
		public SecurityIdentifier EffectiveCallerSid
		{
			get
			{
				if (this.EffectiveCaller.ClientSecurityContext == null)
				{
					throw new ServiceAccessDeniedException(CoreResources.IDs.MessageOperationRequiresUserContext);
				}
				return this.EffectiveCaller.ClientSecurityContext.UserSid;
			}
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06003669 RID: 13929 RVA: 0x000C28A2 File Offset: 0x000C0AA2
		public SessionCache SessionCache
		{
			get
			{
				return this.sessionCache;
			}
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x0600366A RID: 13930 RVA: 0x000C28AA File Offset: 0x000C0AAA
		public UserWorkloadManager WorkloadManager
		{
			get
			{
				return this.workloadManager;
			}
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x0600366B RID: 13931 RVA: 0x000C28B2 File Offset: 0x000C0AB2
		public IEwsBudget Budget
		{
			get
			{
				this.CheckDisposed();
				return this.callerBudget;
			}
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x0600366C RID: 13932 RVA: 0x000C28C0 File Offset: 0x000C0AC0
		public CultureInfo ClientCulture
		{
			get
			{
				return this.clientCulture;
			}
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x0600366D RID: 13933 RVA: 0x000C28C8 File Offset: 0x000C0AC8
		public CultureInfo ServerCulture
		{
			get
			{
				return this.serverCulture;
			}
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x0600366E RID: 13934 RVA: 0x000C28D0 File Offset: 0x000C0AD0
		// (set) Token: 0x0600366F RID: 13935 RVA: 0x000C28D8 File Offset: 0x000C0AD8
		public CultureInfo OwaCulture
		{
			get
			{
				return this.owaCulture;
			}
			set
			{
				this.owaCulture = value;
				this.clientCulture = value;
				EWSSettings.ClientCulture = value;
				this.previousThreadCulture = Thread.CurrentThread.CurrentCulture;
				this.previousThreadUICulture = Thread.CurrentThread.CurrentUICulture;
				Thread.CurrentThread.CurrentCulture = value;
				Thread.CurrentThread.CurrentUICulture = value;
			}
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x06003670 RID: 13936 RVA: 0x000C292F File Offset: 0x000C0B2F
		public AcceptedDomain DefaultDomain
		{
			get
			{
				return this.acceptedDomainCache.DefaultDomain;
			}
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x06003671 RID: 13937 RVA: 0x000C293C File Offset: 0x000C0B3C
		public string Description
		{
			get
			{
				if (this.description == null)
				{
					this.description = string.Format("RC:{0};Action:{1};Caller:{2}", EWSSettings.RequestCorrelation, this.SoapAction, (this.Budget == null) ? "<NULL>" : this.Budget.Owner.ToString());
				}
				return this.description;
			}
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x06003672 RID: 13938 RVA: 0x000C2996 File Offset: 0x000C0B96
		public WebMethodEntry WebMethodEntry
		{
			get
			{
				return this.webMethodEntry;
			}
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06003673 RID: 13939 RVA: 0x000C299E File Offset: 0x000C0B9E
		public AuthZBehavior AuthZBehavior
		{
			get
			{
				return this.authZBehavior;
			}
		}

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06003674 RID: 13940 RVA: 0x000C29A6 File Offset: 0x000C0BA6
		public OriginalCallerContext OriginalCallerContext
		{
			get
			{
				return this.originalCallerContext;
			}
		}

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x06003675 RID: 13941 RVA: 0x000C29AE File Offset: 0x000C0BAE
		// (set) Token: 0x06003676 RID: 13942 RVA: 0x000C29B6 File Offset: 0x000C0BB6
		public IOwaCallback OwaCallback
		{
			get
			{
				return this.owaCallback;
			}
			set
			{
				this.owaCallback = value;
			}
		}

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x06003677 RID: 13943 RVA: 0x000C29BF File Offset: 0x000C0BBF
		// (set) Token: 0x06003678 RID: 13944 RVA: 0x000C29C7 File Offset: 0x000C0BC7
		public WorkloadType WorkloadType
		{
			get
			{
				return this.workloadType;
			}
			set
			{
				this.workloadType = value;
			}
		}

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06003679 RID: 13945 RVA: 0x000C29D0 File Offset: 0x000C0BD0
		// (set) Token: 0x0600367A RID: 13946 RVA: 0x000C29D8 File Offset: 0x000C0BD8
		public bool BackgroundLoad
		{
			get
			{
				return this.backgroundLoad;
			}
			set
			{
				this.backgroundLoad = value;
			}
		}

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x0600367B RID: 13947 RVA: 0x000C29E1 File Offset: 0x000C0BE1
		// (set) Token: 0x0600367C RID: 13948 RVA: 0x000C29E9 File Offset: 0x000C0BE9
		public bool IsRequestTracingEnabled
		{
			get
			{
				return this.isRequestTracingEnabled;
			}
			set
			{
				this.isRequestTracingEnabled = value;
			}
		}

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x0600367D RID: 13949 RVA: 0x000C29F2 File Offset: 0x000C0BF2
		// (set) Token: 0x0600367E RID: 13950 RVA: 0x000C29FA File Offset: 0x000C0BFA
		public bool IsSmimeInstalled { get; private set; }

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x0600367F RID: 13951 RVA: 0x000C2A03 File Offset: 0x000C0C03
		// (set) Token: 0x06003680 RID: 13952 RVA: 0x000C2A0B File Offset: 0x000C0C0B
		public bool IsOwa { get; set; }

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x06003681 RID: 13953 RVA: 0x000C2A14 File Offset: 0x000C0C14
		// (set) Token: 0x06003682 RID: 13954 RVA: 0x000C2A1C File Offset: 0x000C0C1C
		public bool IsTransientErrorResponse { get; set; }

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x06003683 RID: 13955 RVA: 0x000C2A25 File Offset: 0x000C0C25
		// (set) Token: 0x06003684 RID: 13956 RVA: 0x000C2A2D File Offset: 0x000C0C2D
		public bool IsServiceUnavailableOnTransientError { get; set; }

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x06003685 RID: 13957 RVA: 0x000C2A36 File Offset: 0x000C0C36
		// (set) Token: 0x06003686 RID: 13958 RVA: 0x000C2A3E File Offset: 0x000C0C3E
		public IFeaturesManager FeaturesManager { get; set; }

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x06003687 RID: 13959 RVA: 0x000C2A47 File Offset: 0x000C0C47
		// (set) Token: 0x06003688 RID: 13960 RVA: 0x000C2A4F File Offset: 0x000C0C4F
		public string CallerApplicationId { get; set; }

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x06003689 RID: 13961 RVA: 0x000C2A58 File Offset: 0x000C0C58
		public bool IsWebSocketRequest
		{
			get
			{
				return this.isWebSocketRequest;
			}
		}

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x0600368A RID: 13962 RVA: 0x000C2A60 File Offset: 0x000C0C60
		internal CallContext.UserKind UserKindSource
		{
			get
			{
				return this.userKind;
			}
		}

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x0600368B RID: 13963 RVA: 0x000C2A68 File Offset: 0x000C0C68
		// (set) Token: 0x0600368C RID: 13964 RVA: 0x000C2A70 File Offset: 0x000C0C70
		internal bool AllowUnthrottledBudget
		{
			get
			{
				return this.allowUnthrottledBudget;
			}
			set
			{
				this.allowUnthrottledBudget = value;
			}
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x0600368D RID: 13965 RVA: 0x000C2A79 File Offset: 0x000C0C79
		// (set) Token: 0x0600368E RID: 13966 RVA: 0x000C2A81 File Offset: 0x000C0C81
		internal string OwaExplicitLogonUser { get; set; }

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x0600368F RID: 13967 RVA: 0x000C2A8A File Offset: 0x000C0C8A
		internal bool IsExternalUser
		{
			get
			{
				return this.userKind == CallContext.UserKind.External;
			}
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06003690 RID: 13968 RVA: 0x000C2A95 File Offset: 0x000C0C95
		internal LogonType LogonType
		{
			get
			{
				return this.requestedLogonType.LogonType;
			}
		}

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06003691 RID: 13969 RVA: 0x000C2AA2 File Offset: 0x000C0CA2
		internal LogonTypeSource LogonTypeSource
		{
			get
			{
				return this.requestedLogonType.Source;
			}
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06003692 RID: 13970 RVA: 0x000C2AAF File Offset: 0x000C0CAF
		internal bool RequirePrivilegedLogon
		{
			get
			{
				return this.LogonType == LogonType.Admin || this.LogonType == LogonType.SystemService;
			}
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06003693 RID: 13971 RVA: 0x000C2AC5 File Offset: 0x000C0CC5
		internal bool IsWSSecurityUser
		{
			get
			{
				return this.isWSSecurityUser;
			}
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06003694 RID: 13972 RVA: 0x000C2ACD File Offset: 0x000C0CCD
		internal bool IsPartnerUser
		{
			get
			{
				return this.userKind == CallContext.UserKind.Partner;
			}
		}

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06003695 RID: 13973 RVA: 0x000C2AD8 File Offset: 0x000C0CD8
		internal bool IsOAuthUser
		{
			get
			{
				return this.userKind == CallContext.UserKind.OAuth;
			}
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06003696 RID: 13974 RVA: 0x000C2AE3 File Offset: 0x000C0CE3
		internal bool IsMSAUser
		{
			get
			{
				return this.userKind == CallContext.UserKind.MSA;
			}
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06003697 RID: 13975 RVA: 0x000C2AEE File Offset: 0x000C0CEE
		internal string UserAgent
		{
			get
			{
				return this.userAgent;
			}
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06003698 RID: 13976 RVA: 0x000C2AF6 File Offset: 0x000C0CF6
		internal AuthZClientInfo EffectiveCaller
		{
			get
			{
				return this.effectiveCallerAuthZClientInfo;
			}
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06003699 RID: 13977 RVA: 0x000C2B00 File Offset: 0x000C0D00
		internal ADRecipientSessionContext ADRecipientSessionContext
		{
			get
			{
				if (this.adRecipientSessionContext == null)
				{
					string message = string.Format("ADRecipientSessionContext is null. {0}", this.Description);
					throw new InvalidOperationException(message);
				}
				return this.adRecipientSessionContext;
			}
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x0600369A RID: 13978 RVA: 0x000C2B33 File Offset: 0x000C0D33
		// (set) Token: 0x0600369B RID: 13979 RVA: 0x000C2B3B File Offset: 0x000C0D3B
		internal HttpContext HttpContext
		{
			get
			{
				return this.httpContext;
			}
			set
			{
				this.httpContext = value;
			}
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x0600369C RID: 13980 RVA: 0x000C2B44 File Offset: 0x000C0D44
		internal EwsOperationContextBase OperationContext
		{
			get
			{
				return this.operationContext;
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x0600369D RID: 13981 RVA: 0x000C2B4C File Offset: 0x000C0D4C
		internal RequestDetailsLogger ProtocolLog
		{
			get
			{
				return this.protocolLog;
			}
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x0600369E RID: 13982 RVA: 0x000C2B54 File Offset: 0x000C0D54
		// (set) Token: 0x0600369F RID: 13983 RVA: 0x000C2B5C File Offset: 0x000C0D5C
		internal List<ClientStatistics> ClientRequestStatistics { get; set; }

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x060036A0 RID: 13984 RVA: 0x000C2B65 File Offset: 0x000C0D65
		// (set) Token: 0x060036A1 RID: 13985 RVA: 0x000C2B6D File Offset: 0x000C0D6D
		internal bool EncodeStringProperties { get; set; }

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x060036A2 RID: 13986 RVA: 0x000C2B76 File Offset: 0x000C0D76
		// (set) Token: 0x060036A3 RID: 13987 RVA: 0x000C2B7E File Offset: 0x000C0D7E
		internal bool UsingWcfDispatcher
		{
			get
			{
				return this.usingWcfDispatcher;
			}
			set
			{
				this.usingWcfDispatcher = value;
			}
		}

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x060036A4 RID: 13988 RVA: 0x000C2B87 File Offset: 0x000C0D87
		// (set) Token: 0x060036A5 RID: 13989 RVA: 0x000C2B8F File Offset: 0x000C0D8F
		internal bool IsLongRunningScenario { get; set; }

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x060036A6 RID: 13990 RVA: 0x000C2B98 File Offset: 0x000C0D98
		// (set) Token: 0x060036A7 RID: 13991 RVA: 0x000C2BA0 File Offset: 0x000C0DA0
		internal bool IsDetachedFromRequest { get; set; }

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x060036A8 RID: 13992 RVA: 0x000C2BA9 File Offset: 0x000C0DA9
		// (set) Token: 0x060036A9 RID: 13993 RVA: 0x000C2BB1 File Offset: 0x000C0DB1
		internal bool IsRequestProxiedFromDifferentResourceForest { get; private set; }

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x060036AA RID: 13994 RVA: 0x000C2BBA File Offset: 0x000C0DBA
		// (set) Token: 0x060036AB RID: 13995 RVA: 0x000C2BC2 File Offset: 0x000C0DC2
		internal bool IsHybridPublicFolderAccessRequest { get; set; }

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060036AC RID: 13996 RVA: 0x000C2BCC File Offset: 0x000C0DCC
		// (remove) Token: 0x060036AD RID: 13997 RVA: 0x000C2C04 File Offset: 0x000C0E04
		internal event EventHandler OnDisposed;

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x060036AE RID: 13998 RVA: 0x000C2C39 File Offset: 0x000C0E39
		internal List<IDisposable> ObjectsToDisposeList
		{
			get
			{
				if (this.objectToDisposeList == null)
				{
					this.objectToDisposeList = new List<IDisposable>();
				}
				return this.objectToDisposeList;
			}
		}

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x060036AF RID: 13999 RVA: 0x000C2C54 File Offset: 0x000C0E54
		internal MailboxLoggerHandler MailboxLogger
		{
			get
			{
				return this.mailboxLogger;
			}
		}

		// Token: 0x060036B0 RID: 14000 RVA: 0x000C2C5C File Offset: 0x000C0E5C
		internal MailboxIdServerInfo GetServerInfoForEffectiveCaller()
		{
			if (string.IsNullOrEmpty(this.EffectiveCaller.PrimarySmtpAddress))
			{
				return null;
			}
			return MailboxIdServerInfo.Create(this.EffectiveCaller.PrimarySmtpAddress);
		}

		// Token: 0x060036B1 RID: 14001 RVA: 0x000C2C82 File Offset: 0x000C0E82
		internal void DisposeForExchangeService()
		{
			this.callerBudget = null;
			this.Dispose();
		}

		// Token: 0x060036B2 RID: 14002 RVA: 0x000C2C94 File Offset: 0x000C0E94
		internal IDictionary<string, string> GetAccessingInformation()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("AccessingSmtpAddress", this.GetEffectiveAccessingSmtpAddress());
			if (this.AccessingPrincipal != null && this.AccessingPrincipal.MailboxInfo != null)
			{
				dictionary.Add("AccessingMailboxGuid", this.AccessingPrincipal.MailboxInfo.MailboxGuid.ToString());
				IUserPrincipal userPrincipal = this.AccessingPrincipal as IUserPrincipal;
				if (userPrincipal != null && userPrincipal.NetId != null)
				{
					dictionary.Add("AccessingNetId", userPrincipal.NetId.ToString());
				}
			}
			return dictionary;
		}

		// Token: 0x060036B3 RID: 14003 RVA: 0x000C2D2C File Offset: 0x000C0F2C
		protected virtual void Dispose(bool isDisposing)
		{
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<int>((long)this.GetHashCode(), "CallContext.Dispose. Hashcode: {0}", this.GetHashCode());
			if (!this.isDisposed)
			{
				lock (this.instanceLock)
				{
					if (!this.isDisposed)
					{
						this.isDisposed = true;
						this.disposerThreadId = Thread.CurrentThread.ManagedThreadId;
						if (this.OnDisposed != null)
						{
							this.OnDisposed(this, EventArgs.Empty);
							this.OnDisposed = null;
						}
						if (this.objectToDisposeList != null)
						{
							foreach (IDisposable disposable in this.objectToDisposeList)
							{
								CallContext.DisposeObject(disposable);
							}
							this.objectToDisposeList = null;
						}
						if (this.effectiveCallerAuthZClientInfo != null)
						{
							this.effectiveCallerAuthZClientInfo.Dispose();
							this.effectiveCallerAuthZClientInfo = null;
						}
						this.DisposeOwaFields();
						if (this.sessionCache != null)
						{
							this.sessionCache.Dispose();
							this.sessionCache = null;
						}
						if (this.callerBudget != null)
						{
							try
							{
								FaultInjection.GenerateFault((FaultInjection.LIDs)3559271741U);
								if (this.ProtocolLog != null && this.ProtocolLog.ActivityScope != null)
								{
									ActivityContext.SetThreadScope(this.protocolLog.ActivityScope);
								}
								if (!this.IsDetachedFromRequest)
								{
									this.callerBudget.LogEndStateToIIS();
								}
							}
							catch (ADTransientException arg)
							{
								ExTraceGlobals.CommonAlgorithmTracer.TraceError<ADTransientException>(0L, "[CallContext::Dispose] Got ADTransientException {0} while disposing budget.", arg);
							}
							finally
							{
								ExTraceGlobals.UtilAlgorithmTracer.TraceDebug((long)this.GetHashCode(), "[CallContext::Dispose] Disposing of CallContext budget.");
								this.callerBudget.Dispose();
							}
							this.callerBudget = null;
						}
						if (isDisposing)
						{
							GC.SuppressFinalize(this);
						}
					}
				}
			}
		}

		// Token: 0x060036B4 RID: 14004 RVA: 0x000C2F3C File Offset: 0x000C113C
		public void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			this.Dispose(true);
		}

		// Token: 0x060036B5 RID: 14005 RVA: 0x000C2F58 File Offset: 0x000C1158
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<CallContext>(this);
		}

		// Token: 0x060036B6 RID: 14006 RVA: 0x000C2F60 File Offset: 0x000C1160
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x060036B7 RID: 14007 RVA: 0x000C2F78 File Offset: 0x000C1178
		public static CallContext Current
		{
			get
			{
				CallContext result = null;
				if (CallContext.current != null)
				{
					result = CallContext.current;
				}
				else if (HttpContext.Current != null)
				{
					result = (HttpContext.Current.Items["CallContext"] as CallContext);
				}
				else
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "CallContext.Current is null");
				}
				return result;
			}
		}

		// Token: 0x060036B8 RID: 14008 RVA: 0x000C2FCB File Offset: 0x000C11CB
		protected virtual void AssertContexts(HttpContext httpContext, EwsOperationContextBase operationContext, RequestDetailsLogger requestDetailsLogger)
		{
			CallContext.AssertContextVariables(httpContext, requestDetailsLogger);
		}

		// Token: 0x060036B9 RID: 14009 RVA: 0x000C2FD4 File Offset: 0x000C11D4
		protected static void AssertContextVariables(HttpContext httpContext, RequestDetailsLogger requestDetailsLogger)
		{
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x060036BA RID: 14010 RVA: 0x000C2FD6 File Offset: 0x000C11D6
		// (set) Token: 0x060036BB RID: 14011 RVA: 0x000C2FDE File Offset: 0x000C11DE
		public bool DuplicatedActionDetectionEnabled { get; private set; }

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x060036BC RID: 14012 RVA: 0x000C2FE7 File Offset: 0x000C11E7
		// (set) Token: 0x060036BD RID: 14013 RVA: 0x000C2FEF File Offset: 0x000C11EF
		public bool? IsDuplicatedAction { get; private set; }

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x060036BE RID: 14014 RVA: 0x000C2FF8 File Offset: 0x000C11F8
		public string MobileDevicePolicyId
		{
			get
			{
				return this.mobileDevicePolicyId;
			}
		}

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x060036BF RID: 14015 RVA: 0x000C3000 File Offset: 0x000C1200
		public string OwaDeviceId
		{
			get
			{
				return this.owaDeviceId;
			}
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x060036C0 RID: 14016 RVA: 0x000C3008 File Offset: 0x000C1208
		public bool IsMowa
		{
			get
			{
				return this.owaProtocol == "MOWA";
			}
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x060036C1 RID: 14017 RVA: 0x000C301A File Offset: 0x000C121A
		// (set) Token: 0x060036C2 RID: 14018 RVA: 0x000C3022 File Offset: 0x000C1222
		public bool IsOutlookService { get; private set; }

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x060036C3 RID: 14019 RVA: 0x000C302C File Offset: 0x000C122C
		public bool IsRemoteWipeRequested
		{
			get
			{
				if (this.IsMowa && this.HasDeviceHeaders)
				{
					GlobalInfo globalInfo = this.GetMowaSyncState();
					if (globalInfo != null)
					{
						return globalInfo.RemoteWipeRequestedTime != null;
					}
				}
				return false;
			}
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x060036C4 RID: 14020 RVA: 0x000C3063 File Offset: 0x000C1263
		public bool IsExplicitLogon
		{
			get
			{
				return this.isExplicitLogon;
			}
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x060036C5 RID: 14021 RVA: 0x000C306B File Offset: 0x000C126B
		public string OwaDeviceType
		{
			get
			{
				return this.owaDeviceType;
			}
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x060036C6 RID: 14022 RVA: 0x000C3073 File Offset: 0x000C1273
		public string OwaProtocol
		{
			get
			{
				return this.owaProtocol;
			}
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x060036C7 RID: 14023 RVA: 0x000C307B File Offset: 0x000C127B
		// (set) Token: 0x060036C8 RID: 14024 RVA: 0x000C3083 File Offset: 0x000C1283
		public bool ReturningSavedResult { get; private set; }

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x060036C9 RID: 14025 RVA: 0x000C308C File Offset: 0x000C128C
		// (set) Token: 0x060036CA RID: 14026 RVA: 0x000C3094 File Offset: 0x000C1294
		public bool ResultSaved { get; private set; }

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x060036CB RID: 14027 RVA: 0x000C309D File Offset: 0x000C129D
		public bool HasDeviceHeaders
		{
			get
			{
				return !string.IsNullOrWhiteSpace(this.owaDeviceId) && !string.IsNullOrWhiteSpace(this.owaDeviceType) && !string.IsNullOrWhiteSpace(this.owaProtocol);
			}
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x060036CC RID: 14028 RVA: 0x000C30C9 File Offset: 0x000C12C9
		private bool DetectDuplicatedActions
		{
			get
			{
				return this.DuplicatedActionDetectionEnabled && this.HasDeviceHeaders && CallContext.IsQueuedActionId(this.owaActionId);
			}
		}

		// Token: 0x060036CD RID: 14029 RVA: 0x000C30E8 File Offset: 0x000C12E8
		public void MarkRemoteWipeAsSent()
		{
			GlobalInfo globalInfo = this.GetMowaSyncState();
			if (globalInfo != null)
			{
				globalInfo.RemoteWipeSentTime = new ExDateTime?(ExDateTime.UtcNow);
				this.SaveMowaSyncState();
			}
		}

		// Token: 0x060036CE RID: 14030 RVA: 0x000C3118 File Offset: 0x000C1318
		public void MarkRemoteWipeAsAcknowledged()
		{
			GlobalInfo globalInfo = this.GetMowaSyncState();
			if (globalInfo != null)
			{
				globalInfo.RemoteWipeAckTime = new ExDateTime?(ExDateTime.UtcNow);
				ProvisionCommand.SendRemoteWipeConfirmationMessage(globalInfo.RemoteWipeConfirmationAddresses, ExDateTime.Now, this.SessionCache.GetMailboxIdentityMailboxSession(), new DeviceIdentity(this.OwaDeviceId, this.OwaDeviceType, this.owaProtocol), this);
				this.SaveMowaSyncState();
			}
		}

		// Token: 0x060036CF RID: 14031 RVA: 0x000C3178 File Offset: 0x000C1378
		public void UpdateLastPolicyTime()
		{
			GlobalInfo globalInfo = this.GetMowaSyncState();
			if (globalInfo != null)
			{
				double value = 24.0;
				if (globalInfo.LastPolicyTime == null || globalInfo.DevicePolicyApplicationStatus == DevicePolicyApplicationStatus.NotApplied || ExDateTime.Compare(globalInfo.LastPolicyTime.Value, ExDateTime.UtcNow, TimeSpan.FromHours(value)) != 0)
				{
					globalInfo.LastPolicyTime = new ExDateTime?(ExDateTime.UtcNow);
					this.SaveMowaSyncState();
				}
			}
		}

		// Token: 0x060036D0 RID: 14032 RVA: 0x000C31E8 File Offset: 0x000C13E8
		public void UpdatePolicyApplied(ADObjectId policy)
		{
			GlobalInfo globalInfo = this.GetMowaSyncState();
			if (globalInfo != null && ((globalInfo.DevicePolicyApplied == null && policy != null) || (globalInfo.DevicePolicyApplied != null && policy == null) || globalInfo.DevicePolicyApplied.ObjectGuid != policy.ObjectGuid))
			{
				globalInfo.DevicePolicyApplied = policy;
				this.SaveMowaSyncState();
			}
		}

		// Token: 0x060036D1 RID: 14033 RVA: 0x000C323C File Offset: 0x000C143C
		public void UpdateLastSyncAttemptTime()
		{
			if (this.IsMowa)
			{
				GlobalInfo globalInfo = this.GetMowaSyncState();
				if (globalInfo != null)
				{
					double value = 30.0;
					if (globalInfo.LastSyncAttemptTime == null || ExDateTime.Compare(globalInfo.LastSyncAttemptTime.Value, ExDateTime.UtcNow, TimeSpan.FromMinutes(value)) != 0)
					{
						globalInfo.LastSyncAttemptTime = new ExDateTime?(ExDateTime.UtcNow);
						this.SaveMowaSyncState();
					}
				}
			}
		}

		// Token: 0x060036D2 RID: 14034 RVA: 0x000C32AC File Offset: 0x000C14AC
		public void UpdateLastSyncSuccessTime()
		{
			if (this.IsMowa)
			{
				GlobalInfo globalInfo = this.GetMowaSyncState();
				if (globalInfo != null)
				{
					double value = 30.0;
					if (globalInfo.LastSyncSuccessTime == null || ExDateTime.Compare(globalInfo.LastSyncSuccessTime.Value, ExDateTime.UtcNow, TimeSpan.FromMinutes(value)) != 0)
					{
						globalInfo.LastSyncSuccessTime = new ExDateTime?(ExDateTime.UtcNow);
						this.SaveMowaSyncState();
					}
				}
			}
		}

		// Token: 0x060036D3 RID: 14035 RVA: 0x000C331C File Offset: 0x000C151C
		public void MarkDeviceAsBlockedByPolicy()
		{
			GlobalInfo globalInfo = this.GetMowaSyncState();
			if (globalInfo != null && globalInfo.DevicePolicyApplicationStatus != DevicePolicyApplicationStatus.NotApplied)
			{
				globalInfo.DevicePolicyApplicationStatus = DevicePolicyApplicationStatus.NotApplied;
				globalInfo.DeviceAccessState = DeviceAccessState.Blocked;
				globalInfo.DeviceAccessStateReason = DeviceAccessStateReason.Policy;
				this.SaveMowaSyncState();
			}
		}

		// Token: 0x060036D4 RID: 14036 RVA: 0x000C3358 File Offset: 0x000C1558
		public void MarkDeviceAsAllowed()
		{
			GlobalInfo globalInfo = this.GetMowaSyncState();
			if (globalInfo != null && globalInfo.DevicePolicyApplicationStatus != DevicePolicyApplicationStatus.AppliedInFull)
			{
				globalInfo.DevicePolicyApplicationStatus = DevicePolicyApplicationStatus.AppliedInFull;
				globalInfo.DeviceAccessState = DeviceAccessState.Allowed;
				globalInfo.DeviceAccessStateReason = DeviceAccessStateReason.Global;
				this.SaveMowaSyncState();
			}
		}

		// Token: 0x060036D5 RID: 14037 RVA: 0x000C3393 File Offset: 0x000C1593
		public static bool IsQueuedActionId(string actionId)
		{
			return !string.IsNullOrWhiteSpace(actionId) && actionId[0] != '-';
		}

		// Token: 0x060036D6 RID: 14038 RVA: 0x000C33AD File Offset: 0x000C15AD
		public bool IsDeviceIdProvisioned()
		{
			return string.IsNullOrWhiteSpace(this.owaDeviceId) || this.TryLoadOwaSyncStateStorage();
		}

		// Token: 0x060036D7 RID: 14039 RVA: 0x000C33CC File Offset: 0x000C15CC
		public bool TryGetResponse<T>(out T results)
		{
			if (this.DetectDuplicatedActions)
			{
				CallContext.OwaActionQueueState<T> owaActionQueueState = this.GetOwaActionQueueState<T>();
				try
				{
					this.IsDuplicatedAction = new bool?(owaActionQueueState.LastActionId == this.owaActionId);
					this.protocolLog.Set(ServiceCommonMetadata.IsDuplicatedAction, this.IsDuplicatedAction.Value ? "Y" : "N");
					if (this.IsDuplicatedAction.Value)
					{
						this.ReturningSavedResult = false;
						try
						{
							JsonFaultDetail lastActionError = owaActionQueueState.LastActionError;
							if (lastActionError != null)
							{
								this.ReturningSavedResult = true;
								throw new FaultException<JsonFaultDetail>(lastActionError, new FaultReason(owaActionQueueState.LastActionError.Message));
							}
							results = owaActionQueueState.LastActionResults;
							this.ReturningSavedResult = true;
							return true;
						}
						finally
						{
							if (!this.IsOutlookService)
							{
								this.httpContext.Response.Headers["X-OWA-ReturnedSavedResult"] = this.ReturningSavedResult.ToString();
							}
						}
					}
				}
				catch (CorruptSyncStateException exception)
				{
					RequestDetailsLogger.LogException(exception, "DuplicatedActionDetection is ignoring CorruptSyncStateException.", "TryGetResponse");
					this.IsDuplicatedAction = new bool?(false);
				}
				catch (CustomSerializationException exception2)
				{
					RequestDetailsLogger.LogException(exception2, "DuplicatedActionDetection is ignoring CustomSerializationException.", "TryGetResponse");
					this.IsDuplicatedAction = new bool?(false);
				}
			}
			results = default(T);
			return false;
		}

		// Token: 0x060036D8 RID: 14040 RVA: 0x000C3538 File Offset: 0x000C1738
		public void SetResponse<T>(T result, Exception exception)
		{
			if (exception != null)
			{
				this.IsTransientErrorResponse = false;
			}
			if (this.DetectDuplicatedActions)
			{
				this.ResultSaved = false;
				try
				{
					if (!FaultExceptionUtilities.GetIsTransient(exception) && !this.IsTransientErrorResponse)
					{
						CallContext.OwaActionQueueState<T> owaActionQueueState = this.GetOwaActionQueueState<T>();
						owaActionQueueState.LastActionId = this.owaActionId;
						owaActionQueueState.LastActionResults = result;
						owaActionQueueState.LastActionError = CallContext.CreateJsonFaultDetail(exception);
						this.owaActionQueueSyncState.Commit();
						this.ResultSaved = true;
					}
				}
				finally
				{
					if (!this.IsOutlookService)
					{
						this.httpContext.Response.Headers["X-OWA-ResultSaved"] = this.ResultSaved.ToString();
					}
				}
			}
		}

		// Token: 0x060036D9 RID: 14041 RVA: 0x000C35F4 File Offset: 0x000C17F4
		internal void SetCallContextFromActionInfo(string deviceId, string protocol, string deviceType, string actionId, bool IsOutlookService)
		{
			this.owaDeviceId = deviceId;
			this.owaProtocol = protocol;
			this.owaDeviceType = deviceType;
			this.owaActionId = actionId;
			this.IsOutlookService = IsOutlookService;
			this.DuplicatedActionDetectionEnabled = true;
			CallContext.SetCurrent(this);
		}

		// Token: 0x060036DA RID: 14042 RVA: 0x000C3628 File Offset: 0x000C1828
		internal void DisableDupDetection()
		{
			this.DuplicatedActionDetectionEnabled = false;
			CallContext.SetCurrent(null);
		}

		// Token: 0x060036DB RID: 14043 RVA: 0x000C3638 File Offset: 0x000C1838
		internal static void SetServiceUnavailableForTransientErrorResponse(CallContext callContext)
		{
			if (callContext != null && callContext.IsTransientErrorResponse && callContext.DetectDuplicatedActions)
			{
				IOutgoingWebResponseContext outgoingWebResponseContext = callContext.CreateWebResponseContext();
				if (outgoingWebResponseContext != null && outgoingWebResponseContext.StatusCode == HttpStatusCode.OK)
				{
					outgoingWebResponseContext.StatusCode = HttpStatusCode.ServiceUnavailable;
					outgoingWebResponseContext.SuppressContent = true;
					outgoingWebResponseContext.Headers["X-OWA-TransientErrorResponse"] = true.ToString();
				}
			}
		}

		// Token: 0x060036DC RID: 14044 RVA: 0x000C369C File Offset: 0x000C189C
		private static JsonFaultDetail CreateJsonFaultDetail(Exception exception)
		{
			JsonFaultDetail jsonFaultDetail = null;
			if (exception != null)
			{
				jsonFaultDetail = new JsonFaultDetail();
				jsonFaultDetail.Message = exception.Message;
				jsonFaultDetail.StackTrace = exception.StackTrace;
				jsonFaultDetail.ExceptionType = exception.GetType().FullName;
				jsonFaultDetail.ExceptionDetail = new ExceptionDetail(exception);
			}
			return jsonFaultDetail;
		}

		// Token: 0x060036DD RID: 14045 RVA: 0x000C36EC File Offset: 0x000C18EC
		private void InitializeOwaFields()
		{
			this.mobileDevicePolicyId = this.httpContext.Request.Headers["X-OWA-MobileDevicePolicyId"];
			this.owaProtocol = this.httpContext.Request.Headers["X-OWA-Protocol"];
			this.owaDeviceId = this.httpContext.Request.Headers["X-OWA-DeviceId"];
			this.owaDeviceType = this.httpContext.Request.Headers["X-OWA-DeviceType"];
			this.owaActionId = this.httpContext.Request.Headers["X-OWA-ActionId"];
			this.IsSmimeInstalled = (this.httpContext.Request.Headers["X-OWA-SmimeInstalled"] == "1");
			this.isExplicitLogon = !string.IsNullOrEmpty(this.httpContext.Request.Headers["X-OWA-ExplicitLogonUser"]);
			this.IsOutlookService = false;
		}

		// Token: 0x060036DE RID: 14046 RVA: 0x000C37F4 File Offset: 0x000C19F4
		private void DisposeOwaFields()
		{
			if (this.DetectDuplicatedActions && !this.IsOutlookService)
			{
				try
				{
					this.httpContext.Response.Headers["X-OWA-DuplicatedAction"] = ((this.IsDuplicatedAction != null) ? this.IsDuplicatedAction.Value.ToString() : "null");
				}
				catch (HttpException)
				{
				}
			}
			if (this.owaActionQueueSyncState != null)
			{
				this.owaActionQueueSyncState.Dispose();
				this.owaActionQueueSyncState = null;
			}
			if (this.mowaSyncState != null)
			{
				this.mowaSyncState.Dispose();
				this.mowaSyncState = null;
			}
			if (this.owaSyncStateStorage != null)
			{
				this.owaSyncStateStorage.Dispose();
				this.owaSyncStateStorage = null;
			}
		}

		// Token: 0x060036DF RID: 14047 RVA: 0x000C38BC File Offset: 0x000C1ABC
		private GlobalInfo GetMowaSyncState()
		{
			if (this.mowaSyncState == null)
			{
				this.EnsureSyncStateStorageIsLoaded();
				try
				{
					this.mowaSyncState = GlobalInfo.LoadFromMailbox(this.SessionCache.GetMailboxIdentityMailboxSession(), this.owaSyncStateStorage, null);
				}
				catch (AirSyncPermanentException ex)
				{
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeLogRequestException(this.ProtocolLog, ex, "GetMowaSyncState");
				}
			}
			return this.mowaSyncState;
		}

		// Token: 0x060036E0 RID: 14048 RVA: 0x000C3920 File Offset: 0x000C1B20
		private void SaveMowaSyncState()
		{
			this.mowaSyncState.SaveToMailbox();
			this.mowaSyncState.Dispose();
			this.mowaSyncState = null;
		}

		// Token: 0x060036E1 RID: 14049 RVA: 0x000C3940 File Offset: 0x000C1B40
		private CallContext.OwaActionQueueState<T> GetOwaActionQueueState<T>()
		{
			if (this.owaActionQueueSyncState == null)
			{
				this.EnsureSyncStateStorageIsLoaded();
				CallContext.OwaActionQueueStateInfo syncStateInfo = new CallContext.OwaActionQueueStateInfo();
				try
				{
					this.owaActionQueueSyncState = this.owaSyncStateStorage.GetCustomSyncState(syncStateInfo, new PropertyDefinition[0]);
				}
				catch (CorruptSyncStateException exception)
				{
					RequestDetailsLogger.LogException(exception, "DuplicatedActionDetection is ignoring CorruptSyncStateException.", "GetOwaActionQueueState");
				}
				catch (CustomSerializationException exception2)
				{
					RequestDetailsLogger.LogException(exception2, "DuplicatedActionDetection is ignoring CustomSerializationException.", "GetOwaActionQueueState");
				}
				if (this.owaActionQueueSyncState == null)
				{
					this.owaActionQueueSyncState = this.owaSyncStateStorage.CreateCustomSyncState(syncStateInfo);
				}
			}
			return new CallContext.OwaActionQueueState<T>(this.owaActionQueueSyncState);
		}

		// Token: 0x060036E2 RID: 14050 RVA: 0x000C39E4 File Offset: 0x000C1BE4
		private void EnsureSyncStateStorageIsLoaded()
		{
			if (!this.TryLoadOwaSyncStateStorage())
			{
				if (!this.IsOutlookService)
				{
					this.httpContext.Response.Headers["X-OWA-InvalidDeviceId"] = this.owaDeviceId;
				}
				NonProvisionedException ex = new NonProvisionedException(this.IsMowa);
				throw new FaultException<JsonFaultDetail>(CallContext.CreateJsonFaultDetail(ex), new FaultReason(ex.Message));
			}
		}

		// Token: 0x060036E3 RID: 14051 RVA: 0x000C3A44 File Offset: 0x000C1C44
		private bool TryLoadOwaSyncStateStorage()
		{
			if (this.owaSyncStateStorage == null)
			{
				this.owaSyncStateStorage = SyncStateStorage.Bind(this.SessionCache.GetMailboxIdentityMailboxSession(), new DeviceIdentity(this.owaDeviceId, this.owaDeviceType, this.owaProtocol), null);
			}
			return this.owaSyncStateStorage != null;
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x060036E4 RID: 14052 RVA: 0x000C3A93 File Offset: 0x000C1C93
		// (set) Token: 0x060036E5 RID: 14053 RVA: 0x000C3A9B File Offset: 0x000C1C9B
		internal ManagementRoleType ManagementRole { get; set; }

		// Token: 0x060036E6 RID: 14054 RVA: 0x000C3AA4 File Offset: 0x000C1CA4
		internal static CallContext CreateFromRequest(MessageHeaderProcessor headerProcessor, Message request)
		{
			return CallContext.CreateFromRequest(headerProcessor, request, false);
		}

		// Token: 0x060036E7 RID: 14055 RVA: 0x000C3AB0 File Offset: 0x000C1CB0
		internal static CallContext CreateFromRequest(MessageHeaderProcessor headerProcessor, Message request, bool duplicatedActionDetectionEnabled)
		{
			CallContext callContext = null;
			ExternalIdentity externalIdentity = HttpContext.Current.User.Identity as ExternalIdentity;
			if (externalIdentity != null)
			{
				UserWorkloadManager userWorkloadManager = CallContext.GetWorkloadManager(HttpContext.Current.ApplicationInstance);
				callContext = new ExternalCallContext(headerProcessor, request, externalIdentity, userWorkloadManager);
			}
			else
			{
				callContext = CallContext.CreateCallContext(headerProcessor, request);
			}
			if (callContext != null && callContext.AccessingPrincipal != null)
			{
				if (callContext.AccessingPrincipal.MailboxInfo != null && callContext.AccessingPrincipal.MailboxInfo.OrganizationId != null && callContext.AccessingPrincipal.MailboxInfo.OrganizationId.OrganizationalUnit != null)
				{
					CallContext.PushOrgInfoToHttpContext(callContext.AccessingPrincipal.MailboxInfo.OrganizationId.OrganizationalUnit.Name);
				}
				else if (callContext.AccessingPrincipal.ObjectId != null && callContext.AccessingPrincipal.ObjectId.DomainId != null)
				{
					CallContext.PushOrgInfoToHttpContext(callContext.AccessingPrincipal.ObjectId.DomainId.ToCanonicalName());
				}
			}
			if (callContext != null)
			{
				callContext.DuplicatedActionDetectionEnabled = duplicatedActionDetectionEnabled;
			}
			CallContext.UpdateActivity(callContext);
			try
			{
				callContext.soapAction = request.Headers.Action;
				callContext.callerBudget = EwsBudget.Acquire(callContext);
				EwsBudgetWrapper ewsBudgetWrapper = callContext.callerBudget as EwsBudgetWrapper;
				if (ewsBudgetWrapper != null)
				{
					ewsBudgetWrapper.StartConnection(CallContext.GetBudgetDescription(callContext));
				}
				ExTraceGlobals.UtilAlgorithmTracer.TraceDebug((long)callContext.GetHashCode(), "[CallContext::CreateFromRequest] CallContext budget acquired.");
				if (request.Headers.MessageId != null)
				{
					callContext.messageId = request.Headers.MessageId.ToString();
				}
			}
			catch
			{
				callContext.Dispose();
				throw;
			}
			return callContext;
		}

		// Token: 0x060036E8 RID: 14056 RVA: 0x000C3C48 File Offset: 0x000C1E48
		internal static CallContext CreateForOData(HttpContext httpContext, string impersonatedUser)
		{
			HttpContext.Current = httpContext;
			HttpApplication applicationInstance = httpContext.ApplicationInstance;
			AuthZClientInfo authZClientInfo = null;
			CallContext callContext = null;
			CallContext result;
			try
			{
				AuthZClientInfo authZClientInfo2;
				authZClientInfo = (authZClientInfo2 = CallContext.GetCallerClientInfo());
				MailboxAccessType arg = MailboxAccessType.Normal;
				string callerApplicationId = null;
				if (authZClientInfo is AuthZClientInfo.ApplicationAttachedAuthZClientInfo)
				{
					arg = MailboxAccessType.ApplicationAction;
					callerApplicationId = (authZClientInfo as AuthZClientInfo.ApplicationAttachedAuthZClientInfo).OAuthIdentity.OAuthApplication.Id;
				}
				if (!string.IsNullOrEmpty(impersonatedUser))
				{
					SmtpAddressImpersonationProcessor smtpAddressImpersonationProcessor = new SmtpAddressImpersonationProcessor(impersonatedUser, false, authZClientInfo, HttpContext.Current.User.Identity);
					authZClientInfo2 = smtpAddressImpersonationProcessor.CreateAuthZClientInfo();
				}
				ADRecipientSessionContext adrecipientSessionContext = authZClientInfo2.GetADRecipientSessionContext();
				ExchangePrincipal exchangePrincipal = null;
				if (!ExchangePrincipalCache.TryGetFromCache(authZClientInfo2.ClientSecurityContext.UserSid, adrecipientSessionContext, out exchangePrincipal))
				{
					ExTraceGlobals.ServerToServerAuthZTracer.TraceDebug<SecurityIdentifier, MailboxAccessType>(0L, "Exchange principal cache gave back a null principal. AuthenticatedUser: {0}, MailboxAccessType: {1}", authZClientInfo2.ClientSecurityContext.UserSid, arg);
				}
				AppWideStoreSessionCache mailboxSessionBackingCache = (AppWideStoreSessionCache)applicationInstance.Application["WS_APPWideMailboxCacheKey"];
				UserWorkloadManager userWorkloadManager = CallContext.GetWorkloadManager(applicationInstance);
				AcceptedDomainCache acceptedDomainCache = (AcceptedDomainCache)applicationInstance.Application["WS_AcceptedDomainCacheKey"];
				callContext = new CallContext(mailboxSessionBackingCache, acceptedDomainCache, userWorkloadManager, ProxyCASStatus.InitialCASOrNoProxy, authZClientInfo2, exchangePrincipal, adrecipientSessionContext, arg, null, MessageHeaderProcessor.GetExchangeServerCulture(), MessageHeaderProcessor.GetExchangeServerCulture(), httpContext.Request.UserAgent, false, CallContext.UserKind.Uncategorized, OriginalCallerContext.FromAuthZClientInfo(authZClientInfo), RequestedLogonType.Default, WebMethodEntry.JsonWebMethodEntry, AuthZBehavior.DefaultBehavior);
				callContext.CallerApplicationId = callerApplicationId;
				callContext.callerBudget = EwsBudget.Acquire(callContext);
				ExTraceGlobals.UtilAlgorithmTracer.TraceDebug((long)callContext.GetHashCode(), "[CallContext::CreateForOData] CallContext budget acquired.");
				CallContext.UpdateActivity(callContext);
				httpContext.Items["CallContext"] = callContext;
				if (authZClientInfo != authZClientInfo2)
				{
					CallContext.DisposeObject(authZClientInfo);
				}
				result = callContext;
			}
			catch (Exception ex)
			{
				CallContext.DisposeObject(authZClientInfo);
				CallContext.DisposeObject(callContext);
				LocalizedException ex2 = ex as LocalizedException;
				if (ex2 != null)
				{
					throw FaultExceptionUtilities.CreateFault(ex2, FaultParty.Sender);
				}
				throw;
			}
			return result;
		}

		// Token: 0x060036E9 RID: 14057 RVA: 0x000C3E14 File Offset: 0x000C2014
		internal static CallContext CreateForExchangeService(HttpContext httpContext, AppWideStoreSessionCache mailboxSessionCache, AcceptedDomainCache acceptedDomainCache, UserWorkloadManager workloadManager, IEwsBudget budget, CultureInfo clientCulture)
		{
			HttpContext.Current = httpContext;
			MSAIdentity msaIdentity;
			CallContext callContext;
			if (CompositeIdentityBuilder.TryGetMsaNoAdUserIdentity(HttpContext.Current, out msaIdentity))
			{
				callContext = new MSACallContext(httpContext, mailboxSessionCache, acceptedDomainCache, msaIdentity, workloadManager, clientCulture);
			}
			else
			{
				AuthZClientInfo callerClientInfo = CallContext.GetCallerClientInfo();
				ADRecipientSessionContext adrecipientSessionContext = callerClientInfo.GetADRecipientSessionContext();
				ExchangePrincipal exchangePrincipal = null;
				if (!ExchangePrincipalCache.TryGetFromCache(callerClientInfo.ClientSecurityContext.UserSid, adrecipientSessionContext, out exchangePrincipal))
				{
					ExTraceGlobals.ServerToServerAuthZTracer.TraceDebug<string>(0L, "Exchange principal cache gave back a null principal for user {0}:", httpContext.User.Identity.GetSafeName(true));
				}
				callContext = new CallContext(mailboxSessionCache, acceptedDomainCache, workloadManager, ProxyCASStatus.InitialCASOrNoProxy, callerClientInfo, exchangePrincipal, adrecipientSessionContext, MailboxAccessType.Normal, null, MessageHeaderProcessor.GetExchangeServerCulture(), clientCulture, httpContext.Request.UserAgent, false, CallContext.UserKind.Uncategorized, OriginalCallerContext.Empty, RequestedLogonType.Default, WebMethodEntry.JsonWebMethodEntry, AuthZBehavior.DefaultBehavior);
			}
			callContext.callerBudget = budget;
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug((long)callContext.GetHashCode(), "CallContext budget assigned.");
			httpContext.Items["CallContext"] = callContext;
			ExchangeVersion.Current = ExchangeVersion.Latest;
			return callContext;
		}

		// Token: 0x060036EA RID: 14058 RVA: 0x000C3F0C File Offset: 0x000C210C
		internal static CallContext CreateFromInternalRequestContext(MessageHeaderProcessor headerProcessor, Message request, bool duplicatedActionDetectionEnabled, IEWSPartnerRequestContext partnerRequestContext)
		{
			CallContext result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				using (AuthZClientInfo authZClientInfo = CallContext.CreateTemporaryAuthZForInternalRequest(partnerRequestContext))
				{
					HttpContext httpContext = HttpContext.Current;
					HttpApplication applicationInstance = httpContext.ApplicationInstance;
					AppWideStoreSessionCache mailboxSessionBackingCache = (AppWideStoreSessionCache)applicationInstance.Application["WS_APPWideMailboxCacheKey"];
					AcceptedDomainCache acceptedDomainCache = (AcceptedDomainCache)applicationInstance.Application["WS_AcceptedDomainCacheKey"];
					CallContext callContext = null;
					if (!CallContext.IsSameADUser(authZClientInfo, partnerRequestContext.ExchangePrincipal))
					{
						ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "The partner's effectiveCaller is not the created principal.  Falling back to CreateFromRequest.");
						callContext = CallContext.CreateFromRequest(headerProcessor, request, duplicatedActionDetectionEnabled);
						disposeGuard.Add<CallContext>(callContext);
					}
					else if (!CallContext.IsSidBasedAuthZClient(authZClientInfo))
					{
						ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "The partner's authz is not SID based. Falling back to CreateFromRequest.");
						callContext = CallContext.CreateFromRequest(headerProcessor, request, duplicatedActionDetectionEnabled);
						disposeGuard.Add<CallContext>(callContext);
					}
					else
					{
						ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "Pre-requisites for fast-path internal request context creation have been met.");
						ADRecipientSessionContext adrecipientSessionContext = authZClientInfo.GetADRecipientSessionContext();
						UserWorkloadManager userWorkloadManager = CallContext.GetWorkloadManager(applicationInstance);
						headerProcessor.ProcessMailboxCultureHeader(request);
						headerProcessor.ProcessTimeZoneContextHeader(request);
						headerProcessor.ProcessDateTimePrecisionHeader(request);
						bool isServiceUnavailableOnTransientError = headerProcessor.ProcessServiceUnavailableOnTransientErrorHeader(request);
						ManagementRoleType managementRoleType = headerProcessor.ProcessManagementRoleHeader(request);
						WebMethodEntry webMethodEntry = (WebMethodEntry)request.Properties["WebMethodEntry"];
						AuthZBehavior authZBehavior = null;
						CallContext.ValidateRBACPermissions(webMethodEntry, authZClientInfo, managementRoleType, ref authZBehavior);
						using (DisposeGuard disposeGuard2 = default(DisposeGuard))
						{
							AuthZClientInfo authZClientInfo2 = authZClientInfo;
							authZClientInfo2.AddRef();
							disposeGuard2.Add<AuthZClientInfo>(authZClientInfo2);
							RequestedLogonType mailboxLogonType = authZBehavior.GetMailboxLogonType();
							ExTraceGlobals.AuthenticationTracer.TraceDebug<RequestedLogonType>(0L, "[CallContext.CreateFromInternalRequestContext] MailboxLogonType {0}", mailboxLogonType);
							callContext = new CallContext(mailboxSessionBackingCache, acceptedDomainCache, userWorkloadManager, ProxyCASStatus.InitialCASOrNoProxy, authZClientInfo, partnerRequestContext.ExchangePrincipal, adrecipientSessionContext, MailboxAccessType.Normal, null, EWSSettings.ServerCulture, EWSSettings.ClientCulture, partnerRequestContext.UserAgent, false, CallContext.UserKind.Uncategorized, OriginalCallerContext.Empty, mailboxLogonType, webMethodEntry, authZBehavior);
							disposeGuard.Add<CallContext>(callContext);
							disposeGuard2.Success();
						}
						callContext.MethodName = CallContext.GetMethodName(request);
						callContext.IsRequestTracingEnabled = CallContext.GetServerTracingEnabledFlag(httpContext.Request);
						callContext.DuplicatedActionDetectionEnabled = duplicatedActionDetectionEnabled;
						callContext.IsServiceUnavailableOnTransientError = isServiceUnavailableOnTransientError;
						CallContext.PerformPostCallContextCreationSteps(httpContext, request, callContext);
					}
					disposeGuard.Success();
					result = callContext;
				}
			}
			return result;
		}

		// Token: 0x060036EB RID: 14059 RVA: 0x000C4184 File Offset: 0x000C2384
		private static AuthZClientInfo CreateTemporaryAuthZForInternalRequest(IEWSPartnerRequestContext partnerRequestContext)
		{
			AuthZClientInfo callerClientInfo;
			if (partnerRequestContext.CallerClientInfo != null)
			{
				callerClientInfo = partnerRequestContext.CallerClientInfo;
				callerClientInfo.AddRef();
			}
			else
			{
				callerClientInfo = CallContext.GetCallerClientInfo();
			}
			return callerClientInfo;
		}

		// Token: 0x060036EC RID: 14060 RVA: 0x000C41B0 File Offset: 0x000C23B0
		private static void UpdateActivity(CallContext callContext)
		{
			if (callContext != null && (callContext.ProxyCASStatus == ProxyCASStatus.DestinationCAS || callContext.proxyCASStatus == ProxyCASStatus.DestinationCASFromCAFE))
			{
				IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
				if (currentActivityScope != null)
				{
					object obj = null;
					if (EwsOperationContextBase.Current != null && EwsOperationContextBase.Current.IncomingMessageProperties.TryGetValue(HttpRequestMessageProperty.Name, out obj))
					{
						HttpRequestMessageProperty wcfMessage = (HttpRequestMessageProperty)obj;
						currentActivityScope.UpdateFromMessage(wcfMessage);
					}
				}
			}
		}

		// Token: 0x060036ED RID: 14061 RVA: 0x000C420C File Offset: 0x000C240C
		protected static string GetMethodName(Message request)
		{
			string result = string.Empty;
			object obj;
			if (!string.IsNullOrEmpty(request.Headers.Action))
			{
				int num = request.Headers.Action.LastIndexOf('/');
				if (num >= 0)
				{
					result = request.Headers.Action.Substring(num + 1);
				}
				else
				{
					result = request.Headers.Action;
				}
			}
			else if (request.Properties.TryGetValue("HttpOperationName", out obj) && obj is string)
			{
				result = (string)obj;
			}
			return result;
		}

		// Token: 0x060036EE RID: 14062 RVA: 0x000C4290 File Offset: 0x000C2490
		private static bool GetServerTracingEnabledFlag(HttpRequest request)
		{
			return request != null && request.QueryString != null && request.QueryString.AllKeys.Contains("trace", StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x060036EF RID: 14063 RVA: 0x000C42BC File Offset: 0x000C24BC
		private static CallContext CreateCallContext(MessageHeaderProcessor headerProcessor, Message request)
		{
			HttpContext httpContext = HttpContext.Current;
			HttpApplication applicationInstance = httpContext.ApplicationInstance;
			AuthZClientInfo authZClientInfo = null;
			AuthZClientInfo authZClientInfo2 = null;
			AuthZClientInfo authZClientInfo3 = null;
			AuthZClientInfo authZClientInfo4 = null;
			AuthZClientInfo authZClientInfo5 = null;
			AuthZBehavior authZBehavior = null;
			ProxyRequestType? proxy = null;
			CallContext callContext = null;
			bool flag = false;
			CallContext.UserKind userKind = CallContext.UserKind.Uncategorized;
			string callerApplicationId = null;
			CallContext result;
			try
			{
				List<ClientStatistics> clientRequestStatistics = HttpHeaderProcessor.ProcessClientStatisticsHttpHeader(httpContext);
				headerProcessor.ProcessMailboxCultureHeader(request);
				proxy = headerProcessor.ProcessRequestTypeHeader(request);
				headerProcessor.ProcessTimeZoneContextHeader(request);
				headerProcessor.ProcessDateTimePrecisionHeader(request);
				bool flag2 = headerProcessor.ProcessBackgroundLoadHeader(request);
				bool isServiceUnavailableOnTransientError = headerProcessor.ProcessServiceUnavailableOnTransientErrorHeader(request);
				ManagementRoleType managementRoleType = headerProcessor.ProcessManagementRoleHeader(request);
				bool flag3 = EWSSettings.UpnFromClaimSets != null;
				authZClientInfo = CallContext.GetCallerClientInfo();
				bool flag4 = false;
				if (authZClientInfo != null && authZClientInfo.UserIdentity != null && authZClientInfo.UserIdentity.ADUser != null && authZClientInfo.UserIdentity.ADUser.Database != null && !string.Equals(TopologyProvider.LocalForestFqdn, authZClientInfo.UserIdentity.ADUser.Database.PartitionFQDN, StringComparison.OrdinalIgnoreCase))
				{
					flag4 = true;
				}
				bool flag5 = httpContext.Request.Headers[WellKnownHeader.XIsFromBackend] == Global.BooleanTrue;
				if (!flag4 && !flag5)
				{
					authZClientInfo2 = headerProcessor.ProcessProxyHeaders(request, authZClientInfo);
				}
				bool flag6 = flag5 && flag4;
				ProxyCASStatus proxyCASStatus;
				AuthZClientInfo authZClientInfo6;
				if (authZClientInfo2 != null)
				{
					proxyCASStatus = (flag5 ? ProxyCASStatus.DestinationCASFromCAFE : ProxyCASStatus.DestinationCAS);
					authZClientInfo6 = authZClientInfo2;
				}
				else
				{
					proxyCASStatus = ((!string.IsNullOrEmpty(httpContext.Request.UserAgent) && httpContext.Request.UserAgent.StartsWith("ExchangeWebServicesProxy/CrossSite/EXCH/")) ? (flag5 ? ProxyCASStatus.DestinationCASFromCAFE : ProxyCASStatus.DestinationCAS) : ProxyCASStatus.InitialCASOrNoProxy);
					authZClientInfo6 = authZClientInfo;
				}
				authZClientInfo3 = headerProcessor.ProcessSerializedSecurityContextHeaders(request);
				AuthZClientInfo authZClientInfo7;
				if (authZClientInfo3 != null)
				{
					authZClientInfo7 = authZClientInfo3;
				}
				else
				{
					LogonType logonType;
					OpenAsAdminOrSystemServiceBudgetTypeType openAsAdminOrSystemServiceBudgetTypeType;
					authZClientInfo5 = CallContext.GetPrivilegedUserClientInfo(request, headerProcessor, authZClientInfo6, out logonType, out openAsAdminOrSystemServiceBudgetTypeType);
					if (authZClientInfo5 != null)
					{
						RequestedLogonType logonType2 = (logonType == LogonType.Admin) ? RequestedLogonType.AdminFromOpenAsAdminOrSystemServiceHeader : RequestedLogonType.SystemServiceFromOpenAsAdminOrSystemServiceHeader;
						authZClientInfo7 = authZClientInfo5;
						flag2 = (openAsAdminOrSystemServiceBudgetTypeType == OpenAsAdminOrSystemServiceBudgetTypeType.RunAsBackgroundLoad);
						flag = (openAsAdminOrSystemServiceBudgetTypeType == OpenAsAdminOrSystemServiceBudgetTypeType.Unthrottled);
						authZBehavior = new AuthZBehavior.PrivilegedSessionAuthZBehavior(logonType2);
					}
					else
					{
						authZClientInfo4 = CallContext.GetImpersonatedClientInfo(request, headerProcessor, authZClientInfo2, authZClientInfo6);
						if (authZClientInfo4 != null)
						{
							RequestDetailsLogger.Current.ActivityScope.UserEmail = authZClientInfo4.PrimarySmtpAddress;
							authZClientInfo7 = authZClientInfo4;
						}
						else
						{
							authZClientInfo7 = authZClientInfo6;
						}
					}
				}
				MailboxAccessType arg = MailboxAccessType.Normal;
				if (authZClientInfo3 != null || authZClientInfo5 != null)
				{
					arg = MailboxAccessType.ServerToServer;
				}
				else if (authZClientInfo4 != null)
				{
					arg = MailboxAccessType.ExchangeImpersonation;
				}
				if (authZClientInfo6 is PartnerAuthZClientInfo)
				{
					userKind = CallContext.UserKind.Partner;
				}
				else if (authZClientInfo6 is AuthZClientInfo.ApplicationAttachedAuthZClientInfo)
				{
					userKind = CallContext.UserKind.OAuth;
					arg = MailboxAccessType.ApplicationAction;
					callerApplicationId = (authZClientInfo6 as AuthZClientInfo.ApplicationAttachedAuthZClientInfo).OAuthIdentity.OAuthApplication.Id;
				}
				WebMethodEntry method = (WebMethodEntry)request.Properties["WebMethodEntry"];
				CallContext.ValidateRBACPermissions(method, authZClientInfo7, managementRoleType, ref authZBehavior);
				RequestedLogonType mailboxLogonType = authZBehavior.GetMailboxLogonType();
				ADRecipientSessionContext adrecipientSessionContext = authZClientInfo7.GetADRecipientSessionContext();
				ExchangePrincipal exchangePrincipal = null;
				bool flag7 = false;
				if (authZClientInfo7.ClientSecurityContext == null)
				{
					ExTraceGlobals.AuthorizationTracer.TraceDebug(0L, "The effective caller is null (app only OAuth token)");
				}
				else if (!CallContext.SkipInitializationOfEffectiveCallerPrincipal(method, proxy) && !flag6)
				{
					if (CallContext.HasPublicFolderMailboxHeader())
					{
						flag7 = ExchangePrincipalCache.TryGetExchangePrincipalForHybridPublicFolderAccess(authZClientInfo7.ClientSecurityContext.UserSid, adrecipientSessionContext, out exchangePrincipal, false);
					}
					if (!flag7 && !ExchangePrincipalCache.TryGetFromCache(authZClientInfo7.ClientSecurityContext.UserSid, adrecipientSessionContext, out exchangePrincipal))
					{
						ExTraceGlobals.ServerToServerAuthZTracer.TraceDebug<SecurityIdentifier, MailboxAccessType>(0L, "Exchange principal cache gave back a null principal.  AuthenticatedUser: {0}, MailboxAccessType: {1}", authZClientInfo7.ClientSecurityContext.UserSid, arg);
					}
				}
				ExTraceGlobals.FaultInjectionTracer.TraceTest<ExchangePrincipal>(3789958461U, ref exchangePrincipal);
				AppWideStoreSessionCache mailboxSessionBackingCache;
				if (!headerProcessor.IsAvailabilityServiceS2S(request) && authZClientInfo7.ObjectGuid != Guid.Empty && CallContext.CanUserAgentUseBackingCache(httpContext.Request.UserAgent))
				{
					mailboxSessionBackingCache = (AppWideStoreSessionCache)applicationInstance.Application["WS_APPWideMailboxCacheKey"];
				}
				else
				{
					ExTraceGlobals.ServerToServerAuthZTracer.TraceDebug(0L, "CallContext.CreateContext, Bypassing the backing cache.");
					mailboxSessionBackingCache = null;
				}
				AcceptedDomainCache acceptedDomainCache = (AcceptedDomainCache)applicationInstance.Application["WS_AcceptedDomainCacheKey"];
				UserWorkloadManager userWorkloadManager = CallContext.GetWorkloadManager(applicationInstance);
				CultureInfo cultureInfo = EWSSettings.ClientCulture;
				CultureInfo cultureInfo2 = EWSSettings.ServerCulture;
				callContext = new CallContext(mailboxSessionBackingCache, acceptedDomainCache, userWorkloadManager, proxyCASStatus, authZClientInfo7, exchangePrincipal, adrecipientSessionContext, arg, proxy, cultureInfo2, cultureInfo, httpContext.Request.UserAgent, flag3, userKind, OriginalCallerContext.FromAuthZClientInfo(authZClientInfo6), mailboxLogonType, method, authZBehavior);
				callContext.MethodName = CallContext.GetMethodName(request);
				callContext.IsRequestTracingEnabled = CallContext.GetServerTracingEnabledFlag(httpContext.Request);
				callContext.ClientRequestStatistics = clientRequestStatistics;
				callContext.EncodeStringProperties = Global.EncodeStringProperties;
				callContext.BackgroundLoad = (flag2 || (Global.BackgroundLoadedTasksEnabled && Global.BackgroundLoadedTasks.Contains(callContext.MethodName)));
				callContext.IsServiceUnavailableOnTransientError = isServiceUnavailableOnTransientError;
				callContext.AllowUnthrottledBudget = flag;
				callContext.ManagementRole = managementRoleType;
				callContext.CallerApplicationId = callerApplicationId;
				callContext.IsRequestProxiedFromDifferentResourceForest = flag6;
				callContext.IsHybridPublicFolderAccessRequest = flag7;
				if (authZClientInfo6 != null && authZClientInfo6.PrimarySmtpAddress != null)
				{
					CallContext.PushUserInfoToHttpContext(callContext.HttpContext, authZClientInfo6.PrimarySmtpAddress);
				}
				HttpContext.Current.Items["CallContext"] = callContext;
				headerProcessor.ValidateRights(callContext, authZClientInfo6, request);
				if (authZClientInfo4 != authZClientInfo7)
				{
					CallContext.DisposeObject(authZClientInfo4);
				}
				if (authZClientInfo3 != authZClientInfo7)
				{
					CallContext.DisposeObject(authZClientInfo3);
				}
				if (authZClientInfo2 != authZClientInfo7)
				{
					CallContext.DisposeObject(authZClientInfo2);
				}
				if (authZClientInfo != authZClientInfo7)
				{
					CallContext.DisposeObject(authZClientInfo);
				}
				if (authZClientInfo5 != authZClientInfo7)
				{
					CallContext.DisposeObject(authZClientInfo5);
				}
				result = callContext;
			}
			catch (Exception ex)
			{
				CallContext.DisposeObject(authZClientInfo);
				CallContext.DisposeObject(authZClientInfo2);
				CallContext.DisposeObject(authZClientInfo3);
				CallContext.DisposeObject(authZClientInfo4);
				CallContext.DisposeObject(authZClientInfo5);
				CallContext.DisposeObject(callContext);
				LocalizedException ex2 = ex as LocalizedException;
				if (ex2 != null)
				{
					throw FaultExceptionUtilities.CreateFault(ex2, FaultParty.Sender);
				}
				throw;
			}
			return result;
		}

		// Token: 0x060036F0 RID: 14064 RVA: 0x000C4814 File Offset: 0x000C2A14
		internal static AuthZClientInfo GetCallerClientInfo()
		{
			IIdentity userIdentity = CompositeIdentityBuilder.GetUserIdentity(HttpContext.Current);
			ExTraceGlobals.AuthenticationTracer.TraceDebug<IIdentity>(0L, "[AuthZClientInfo.GetCallerClientInfo] Original calling identity is {0}", userIdentity);
			return AuthZClientInfo.ResolveIdentity(userIdentity);
		}

		// Token: 0x060036F1 RID: 14065 RVA: 0x000C4844 File Offset: 0x000C2A44
		internal static void PushOrgInfoToHttpContext(string org)
		{
			if (HttpContext.Current != null && !string.IsNullOrEmpty(org))
			{
				HttpContext.Current.Items["AuthenticatedUserOrganization"] = org;
			}
		}

		// Token: 0x060036F2 RID: 14066 RVA: 0x000C486A File Offset: 0x000C2A6A
		internal static void PushUserInfoToHttpContext(HttpContext httpContext, string userName)
		{
			if (httpContext != null && !string.IsNullOrEmpty(userName) && httpContext.Items["AuthenticatedUser"] == null)
			{
				httpContext.Items["AuthenticatedUser"] = userName;
			}
		}

		// Token: 0x060036F3 RID: 14067 RVA: 0x000C489C File Offset: 0x000C2A9C
		internal static void PushUserPuidToHttpContext()
		{
			if (HttpContext.Current != null)
			{
				using (AuthZClientInfo callerClientInfo = CallContext.GetCallerClientInfo())
				{
					if (callerClientInfo != null && callerClientInfo.UserIdentity != null && callerClientInfo.UserIdentity.ADUser != null && callerClientInfo.UserIdentity.ADUser.NetID != null)
					{
						HttpContext.Current.Items["PassportUniqueId"] = callerClientInfo.UserIdentity.ADUser.NetID.ToString();
					}
				}
			}
		}

		// Token: 0x060036F4 RID: 14068 RVA: 0x000C492C File Offset: 0x000C2B2C
		internal static UserWorkloadManager GetWorkloadManager(HttpApplication httpApplication)
		{
			return (UserWorkloadManager)httpApplication.Application["WS_WorkloadManagerKey"];
		}

		// Token: 0x060036F5 RID: 14069 RVA: 0x000C4943 File Offset: 0x000C2B43
		internal static AuthZClientInfo GetImpersonatedClientInfo(Message request, MessageHeaderProcessor headerProcessor, AuthZClientInfo proxyClientInfo, AuthZClientInfo originalCallerClientInfo)
		{
			return headerProcessor.ProcessImpersonationHeaders(request, proxyClientInfo, originalCallerClientInfo);
		}

		// Token: 0x060036F6 RID: 14070 RVA: 0x000C4950 File Offset: 0x000C2B50
		internal static AuthZClientInfo GetPrivilegedUserClientInfo(Message request, MessageHeaderProcessor headerProcessor, AuthZClientInfo originalCallerClientInfo, out LogonType logonType, out OpenAsAdminOrSystemServiceBudgetTypeType budgetType)
		{
			logonType = LogonType.BestAccess;
			budgetType = OpenAsAdminOrSystemServiceBudgetTypeType.Default;
			SpecialLogonType? specialLogonType;
			int? num;
			AuthZClientInfo authZClientInfo = headerProcessor.ProcessOpenAsAdminOrSystemServiceHeader(request, originalCallerClientInfo, out specialLogonType, out num);
			if (authZClientInfo != null)
			{
				logonType = ((specialLogonType == SpecialLogonType.Admin) ? LogonType.Admin : LogonType.SystemService);
				budgetType = (OpenAsAdminOrSystemServiceBudgetTypeType)num.Value;
			}
			return authZClientInfo;
		}

		// Token: 0x060036F7 RID: 14071 RVA: 0x000C499C File Offset: 0x000C2B9C
		internal static string GetBudgetDescription(CallContext callContext)
		{
			string text = "EwsBudgetWrapper.EwsBudgetWrapper";
			if (callContext.HttpContext != null && callContext.HttpContext.Request != null)
			{
				HttpRequest request = callContext.HttpContext.Request;
				NameValueCollection queryString = request.QueryString;
				if (queryString != null)
				{
					text = text + "." + queryString.ToString();
				}
				if (request.Headers != null && !string.IsNullOrEmpty(request.Headers["X-OWA-CorrelationId"]))
				{
					text = text + "." + request.Headers["X-OWA-CorrelationId"];
				}
			}
			return text;
		}

		// Token: 0x060036F8 RID: 14072 RVA: 0x000C4A28 File Offset: 0x000C2C28
		private static void PerformPostCallContextCreationSteps(HttpContext httpContext, Message request, CallContext callContext)
		{
			CallContext.UpdateActivity(callContext);
			httpContext.Items["CallContext"] = callContext;
			callContext.soapAction = request.Headers.Action;
			callContext.callerBudget = EwsBudget.Acquire(callContext);
			ExTraceGlobals.UtilAlgorithmTracer.TraceDebug((long)callContext.GetHashCode(), "[CallContext::PerformPostCallContextCreationSteps] CallContext budget acquired.");
			if (request.Headers.MessageId != null)
			{
				callContext.messageId = request.Headers.MessageId.ToString();
			}
		}

		// Token: 0x060036F9 RID: 14073 RVA: 0x000C4AA8 File Offset: 0x000C2CA8
		private static bool SkipInitializationOfEffectiveCallerPrincipal(WebMethodEntry method, ProxyRequestType? proxy)
		{
			string name;
			return method != null && proxy != null && ((name = method.Name) != null && (name == "GetUserPhoto" || name == "GetUserPhoto:GET"));
		}

		// Token: 0x060036FA RID: 14074 RVA: 0x000C4AEC File Offset: 0x000C2CEC
		internal static bool HasPublicFolderMailboxHeader()
		{
			HttpContext httpContext = HttpContext.Current;
			return httpContext != null && httpContext.Request != null && httpContext.Request.Headers != null && RemotePublicFolderOperations.CheckPublicFolderMailboxHeader(httpContext.Request.Headers);
		}

		// Token: 0x060036FB RID: 14075 RVA: 0x000C4B29 File Offset: 0x000C2D29
		private bool CallerAccessAllowed(EwsApplicationAccessPolicy ewsApplicationAccessPolicy, MultiValuedProperty<string> ewsExceptions)
		{
			if (ewsApplicationAccessPolicy == EwsApplicationAccessPolicy.EnforceAllowList)
			{
				return ewsExceptions == null || ewsExceptions.Find(new Predicate<string>(this.MatchesUserAgent)) != null;
			}
			return ewsExceptions == null || ewsExceptions.Find(new Predicate<string>(this.MatchesUserAgent)) == null;
		}

		// Token: 0x060036FC RID: 14076 RVA: 0x000C4B67 File Offset: 0x000C2D67
		private static void ValidateRBACPermissions(WebMethodEntry webMethodEntry, AuthZClientInfo effectiveCallerClientInfo, ManagementRoleType managementRoleType, ref AuthZBehavior authZBehavior)
		{
			effectiveCallerClientInfo.ApplyManagementRole(managementRoleType, webMethodEntry);
			if (authZBehavior == null)
			{
				authZBehavior = effectiveCallerClientInfo.GetAuthZBehavior();
			}
			if (!authZBehavior.IsAllowedToCallWebMethod(webMethodEntry))
			{
				throw new ServiceAccessDeniedException((CoreResources.IDs)2554577046U);
			}
		}

		// Token: 0x060036FD RID: 14077 RVA: 0x000C4B98 File Offset: 0x000C2D98
		private static bool IsSameADUser(AuthZClientInfo clientInfo, ExchangePrincipal exchangePrincipal)
		{
			ADObjectId adobjectId = null;
			if (clientInfo != null && clientInfo.UserIdentity != null && clientInfo.UserIdentity.ADUser != null)
			{
				adobjectId = clientInfo.UserIdentity.ADUser.ObjectId;
			}
			ADObjectId adobjectId2 = null;
			if (exchangePrincipal != null)
			{
				adobjectId2 = exchangePrincipal.ObjectId;
			}
			return adobjectId != null && adobjectId2 != null && adobjectId.Equals(adobjectId2);
		}

		// Token: 0x060036FE RID: 14078 RVA: 0x000C4BF0 File Offset: 0x000C2DF0
		private static bool IsSidBasedAuthZClient(AuthZClientInfo clientInfo)
		{
			return clientInfo != null && clientInfo.ClientSecurityContext != null && null != clientInfo.ClientSecurityContext.UserSid;
		}

		// Token: 0x060036FF RID: 14079 RVA: 0x000C4C20 File Offset: 0x000C2E20
		private bool ShouldBlockLyncBadLiveIdTokenAccess()
		{
			if (string.IsNullOrEmpty(this.UserAgent) || !this.UserAgent.StartsWith(CallContext.BlockLyncBadLiveIdTokenUserAgentPrefix.Value) || !this.UserAgent.EndsWith(CallContext.BlockLyncBadLiveIdTokenUserAgentSuffix.Value))
			{
				return false;
			}
			if (!object.Equals(this.HttpContext.Items["AuthType"], "LiveIdToken"))
			{
				return false;
			}
			string text = HttpContext.Current.Items["LiveIdTokenSmtpClaim"] as string;
			if (!string.IsNullOrEmpty(text))
			{
				ProxyAddress proxyAddress = ProxyAddress.Parse(text);
				if (proxyAddress is InvalidProxyAddress)
				{
					this.ProtocolLog.AppendGenericInfo("BlockLyncBadLiveIdTokenAccess", "BadSMTPClaim");
					if (CallContext.BlockLyncBadLiveIdTokenEnabled.Value)
					{
						return true;
					}
				}
				else if (!this.AccessingADUser.EmailAddresses.Contains(proxyAddress))
				{
					this.ProtocolLog.AppendGenericInfo("BlockLyncBadLiveIdTokenAccess_SmtpClaimMismatch", this.AccessingADUser.PrimarySmtpAddress + "-" + text);
					if (CallContext.BlockLyncBadLiveIdTokenEnabled.Value)
					{
						return true;
					}
				}
			}
			string text2 = this.HttpContext.Request.Headers[WellKnownHeader.AnchorMailbox];
			if (string.IsNullOrEmpty(text2))
			{
				return false;
			}
			ProxyAddress proxyAddress2 = ProxyAddress.Parse(text2);
			if (proxyAddress2 is InvalidProxyAddress)
			{
				return false;
			}
			if (!this.AccessingADUser.EmailAddresses.Contains(proxyAddress2))
			{
				this.ProtocolLog.AppendGenericInfo("BlockLyncBadLiveIdTokenAccess_AnchorMailboxMismatch", this.AccessingADUser.PrimarySmtpAddress + "-" + text2);
				if (CallContext.BlockLyncBadLiveIdTokenEnabled.Value)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003700 RID: 14080 RVA: 0x000C4DF0 File Offset: 0x000C2FF0
		private bool CallerAccessAllowed()
		{
			if (this.OriginalCallerContext == null || this.originalCallerContext.Sid == null || this.AccessingADUser == null)
			{
				return true;
			}
			try
			{
				if (this.ShouldBlockLyncBadLiveIdTokenAccess())
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(RequestDetailsLogger.Current, "LyncBadLiveIdCheckException", ex.Message + ex.StackTrace);
			}
			ADUser accessingADUser = this.AccessingADUser;
			Organization organization = OrganizationCache.Singleton.Get(accessingADUser.OrganizationId);
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Ews.EwsClientAccessRulesEnabled.Enabled)
			{
				bool flag = ClientAccessRulesUtils.ShouldBlockConnection(accessingADUser.OrganizationId, ClientAccessRulesUtils.GetUsernameFromIdInformation(accessingADUser.WindowsLiveID, accessingADUser.MasterAccountSid, accessingADUser.Sid, accessingADUser.Id), ClientAccessProtocol.ExchangeWebServices, ClientAccessRulesUtils.GetRemoteEndPointFromContext(this.HttpContext), ClientAccessAuthenticationMethod.BasicAuthentication, accessingADUser, delegate(ClientAccessRulesEvaluationContext context)
				{
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(RequestDetailsLogger.Current, ClientAccessRulesConstants.ClientAccessRuleName, context.CurrentRule.Name);
				}, delegate(double latency)
				{
					if (latency > 50.0)
					{
						RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(RequestDetailsLogger.Current, ClientAccessRulesConstants.ClientAccessRulesLatency, latency);
					}
				});
				if (flag)
				{
					return false;
				}
			}
			bool? ewsEnabled = accessingADUser.EwsEnabled;
			if (ewsEnabled == null)
			{
				ewsEnabled = organization.EwsEnabled;
			}
			if (ewsEnabled == null)
			{
				return true;
			}
			if (ewsEnabled == false)
			{
				return false;
			}
			if (this.UserAgent != null && (this.UserAgent.Contains("Microsoft Office Outlook") || this.UserAgent.Contains("Microsoft Outlook")))
			{
				bool? ewsAllowOutlook = accessingADUser.EwsAllowOutlook;
				if (ewsAllowOutlook == null)
				{
					ewsAllowOutlook = organization.EwsAllowOutlook;
				}
				if (ewsAllowOutlook != null)
				{
					return ewsAllowOutlook.Value;
				}
			}
			else if (this.UserAgent != null && this.UserAgent.Contains("MacOutlook"))
			{
				bool? ewsAllowMacOutlook = accessingADUser.EwsAllowMacOutlook;
				if (ewsAllowMacOutlook == null)
				{
					ewsAllowMacOutlook = organization.EwsAllowMacOutlook;
				}
				if (ewsAllowMacOutlook != null)
				{
					return ewsAllowMacOutlook.Value;
				}
			}
			else if (this.UserAgent != null && this.UserAgent.Contains("Microsoft-Entourage"))
			{
				bool? ewsAllowEntourage = accessingADUser.EwsAllowEntourage;
				if (ewsAllowEntourage == null)
				{
					ewsAllowEntourage = organization.EwsAllowEntourage;
				}
				if (ewsAllowEntourage != null)
				{
					return ewsAllowEntourage.Value;
				}
			}
			EwsApplicationAccessPolicy? ewsApplicationAccessPolicy = accessingADUser.EwsApplicationAccessPolicy;
			if (ewsApplicationAccessPolicy != null)
			{
				MultiValuedProperty<string> ewsExceptions = accessingADUser.EwsExceptions;
				return this.CallerAccessAllowed(ewsApplicationAccessPolicy.Value, ewsExceptions);
			}
			ewsApplicationAccessPolicy = organization.EwsApplicationAccessPolicy;
			if (ewsApplicationAccessPolicy != null)
			{
				MultiValuedProperty<string> ewsExceptions2 = organization.EwsExceptions;
				return this.CallerAccessAllowed(ewsApplicationAccessPolicy.Value, ewsExceptions2);
			}
			return true;
		}

		// Token: 0x06003701 RID: 14081 RVA: 0x000C508C File Offset: 0x000C328C
		internal bool CallerHasAccess()
		{
			bool result;
			try
			{
				DateTime utcNow = DateTime.UtcNow;
				bool flag = this.mailboxAccessType == MailboxAccessType.ServerToServer || this.MailboxAccessType == MailboxAccessType.ExchangeImpersonation || this.CallerAccessAllowed();
				double totalMilliseconds = (DateTime.UtcNow - utcNow).TotalMilliseconds;
				if (totalMilliseconds > 50.0)
				{
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(RequestDetailsLogger.Current, "CallerHasAccessLatency", totalMilliseconds);
				}
				result = flag;
			}
			catch (TenantOrgContainerNotFoundException arg)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<TenantOrgContainerNotFoundException>(0L, "[CallContext::CallerHasAccess] encounters TenantOrgContainerNotFoundException {0}.", arg);
				result = false;
			}
			return result;
		}

		// Token: 0x06003702 RID: 14082 RVA: 0x000C5120 File Offset: 0x000C3320
		internal IOutgoingWebResponseContext CreateWebResponseContext()
		{
			if (this.UsingWcfDispatcher)
			{
				return new OutgoingWebResponseContextWrapper(WebOperationContext.Current.OutgoingResponse);
			}
			return new OutgoingHttpResponseContextWrapper(this.HttpContext.Response);
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06003703 RID: 14083 RVA: 0x000C514A File Offset: 0x000C334A
		public string MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06003704 RID: 14084 RVA: 0x000C5152 File Offset: 0x000C3352
		// (set) Token: 0x06003705 RID: 14085 RVA: 0x000C515A File Offset: 0x000C335A
		public string MethodName
		{
			get
			{
				return this.methodName;
			}
			protected set
			{
				this.methodName = value;
			}
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06003706 RID: 14086 RVA: 0x000C5164 File Offset: 0x000C3364
		internal virtual string EffectiveCallerNetId
		{
			get
			{
				if (this.EffectiveCaller == null || this.EffectiveCaller.UserIdentity == null || this.EffectiveCaller.UserIdentity.ADUser == null)
				{
					return null;
				}
				return Convert.ToString(this.EffectiveCaller.UserIdentity.ADUser.NetID);
			}
		}

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06003707 RID: 14087 RVA: 0x000C51B4 File Offset: 0x000C33B4
		internal bool IsClientConnected
		{
			get
			{
				return this.IsWebSocketRequest || this.HttpContext.Response.IsClientConnected;
			}
		}

		// Token: 0x04001E4C RID: 7756
		public const string CallContextKey = "CallContext";

		// Token: 0x04001E4D RID: 7757
		public const string ProxiedRequestUserAgentPrefix = "ExchangeWebServicesProxy/CrossSite/EXCH/";

		// Token: 0x04001E4E RID: 7758
		private const string MonitoringClientUserAgentTag = "MSEXCHMON";

		// Token: 0x04001E4F RID: 7759
		private const string AccessingSmtpAddressKey = "AccessingSmtpAddress";

		// Token: 0x04001E50 RID: 7760
		private const string AccessingMailboxGuidKey = "AccessingMailboxGuid";

		// Token: 0x04001E51 RID: 7761
		private const string AccessingNetIdKey = "AccessingNetId";

		// Token: 0x04001E52 RID: 7762
		public const string ActionIdHttpHeaderKey = "X-OWA-ActionId";

		// Token: 0x04001E53 RID: 7763
		private const string TraceEnabledQueryStringParameter = "trace";

		// Token: 0x04001E54 RID: 7764
		[ThreadStatic]
		private static CallContext current;

		// Token: 0x04001E55 RID: 7765
		protected ProxyRequestType? availabilityProxyRequestType;

		// Token: 0x04001E56 RID: 7766
		protected MailboxAccessType mailboxAccessType;

		// Token: 0x04001E57 RID: 7767
		protected ExchangePrincipal effectiveCallerExchangePrincipal;

		// Token: 0x04001E58 RID: 7768
		protected AuthZBehavior authZBehavior;

		// Token: 0x04001E59 RID: 7769
		protected AuthZClientInfo effectiveCallerAuthZClientInfo;

		// Token: 0x04001E5A RID: 7770
		protected ADRecipientSessionContext adRecipientSessionContext;

		// Token: 0x04001E5B RID: 7771
		protected SessionCache sessionCache;

		// Token: 0x04001E5C RID: 7772
		protected UserWorkloadManager workloadManager;

		// Token: 0x04001E5D RID: 7773
		protected AcceptedDomainCache acceptedDomainCache;

		// Token: 0x04001E5E RID: 7774
		protected string userAgent;

		// Token: 0x04001E5F RID: 7775
		protected bool isDisposed;

		// Token: 0x04001E60 RID: 7776
		protected int disposerThreadId;

		// Token: 0x04001E61 RID: 7777
		protected IEwsBudget callerBudget;

		// Token: 0x04001E62 RID: 7778
		protected CallContext.UserKind userKind;

		// Token: 0x04001E63 RID: 7779
		protected RequestedLogonType requestedLogonType;

		// Token: 0x04001E64 RID: 7780
		private ProxyCASStatus proxyCASStatus;

		// Token: 0x04001E65 RID: 7781
		private bool isWSSecurityUser;

		// Token: 0x04001E66 RID: 7782
		private string soapAction;

		// Token: 0x04001E67 RID: 7783
		private object instanceLock = new object();

		// Token: 0x04001E68 RID: 7784
		private HttpContext httpContext;

		// Token: 0x04001E69 RID: 7785
		private EwsOperationContextBase operationContext;

		// Token: 0x04001E6A RID: 7786
		private RequestDetailsLogger protocolLog;

		// Token: 0x04001E6B RID: 7787
		private string description;

		// Token: 0x04001E6C RID: 7788
		private WebMethodEntry webMethodEntry;

		// Token: 0x04001E6D RID: 7789
		private IOwaCallback owaCallback;

		// Token: 0x04001E6E RID: 7790
		private OriginalCallerContext originalCallerContext = OriginalCallerContext.Empty;

		// Token: 0x04001E6F RID: 7791
		private bool backgroundLoad;

		// Token: 0x04001E70 RID: 7792
		private bool allowUnthrottledBudget;

		// Token: 0x04001E71 RID: 7793
		private WorkloadType workloadType = WorkloadType.Ews;

		// Token: 0x04001E72 RID: 7794
		private CultureInfo owaCulture;

		// Token: 0x04001E73 RID: 7795
		private CultureInfo previousThreadCulture;

		// Token: 0x04001E74 RID: 7796
		private CultureInfo previousThreadUICulture;

		// Token: 0x04001E75 RID: 7797
		private bool isRequestTracingEnabled;

		// Token: 0x04001E76 RID: 7798
		private string owaUserContextKey;

		// Token: 0x04001E77 RID: 7799
		private bool isWebSocketRequest;

		// Token: 0x04001E78 RID: 7800
		private MailboxLoggerHandler mailboxLogger;

		// Token: 0x04001E79 RID: 7801
		private bool usingWcfDispatcher = true;

		// Token: 0x04001E7A RID: 7802
		private DisposeTracker disposeTracker;

		// Token: 0x04001E7B RID: 7803
		private List<IDisposable> objectToDisposeList;

		// Token: 0x04001E7C RID: 7804
		protected CultureInfo serverCulture;

		// Token: 0x04001E7D RID: 7805
		protected CultureInfo clientCulture;

		// Token: 0x04001E7F RID: 7807
		private string mobileDevicePolicyId;

		// Token: 0x04001E80 RID: 7808
		private string owaProtocol;

		// Token: 0x04001E81 RID: 7809
		private string owaDeviceId;

		// Token: 0x04001E82 RID: 7810
		private string owaDeviceType;

		// Token: 0x04001E83 RID: 7811
		private string owaActionId;

		// Token: 0x04001E84 RID: 7812
		private CustomSyncState owaActionQueueSyncState;

		// Token: 0x04001E85 RID: 7813
		private SyncStateStorage owaSyncStateStorage;

		// Token: 0x04001E86 RID: 7814
		private GlobalInfo mowaSyncState;

		// Token: 0x04001E87 RID: 7815
		private bool isExplicitLogon;

		// Token: 0x04001E88 RID: 7816
		private string methodName;

		// Token: 0x04001E89 RID: 7817
		private string messageId;

		// Token: 0x04001E8A RID: 7818
		public static readonly BoolAppSettingsEntry BlockLyncBadLiveIdTokenEnabled = new BoolAppSettingsEntry("BlockLyncBadLiveIdTokenEnabled", false, null);

		// Token: 0x04001E8B RID: 7819
		public static readonly StringAppSettingsEntry BlockLyncBadLiveIdTokenUserAgentPrefix = new StringAppSettingsEntry("BlockLyncBadLiveIdTokenUserAgentPrefix", "OC", null);

		// Token: 0x04001E8C RID: 7820
		public static readonly StringAppSettingsEntry BlockLyncBadLiveIdTokenUserAgentSuffix = new StringAppSettingsEntry("BlockLyncBadLiveIdTokenUserAgentSuffix", "Lync)", null);

		// Token: 0x02000708 RID: 1800
		internal enum UserKind
		{
			// Token: 0x04001EA3 RID: 7843
			Uncategorized,
			// Token: 0x04001EA4 RID: 7844
			External,
			// Token: 0x04001EA5 RID: 7845
			Partner,
			// Token: 0x04001EA6 RID: 7846
			OAuth,
			// Token: 0x04001EA7 RID: 7847
			MSA
		}

		// Token: 0x02000709 RID: 1801
		private struct OwaActionQueueState<T>
		{
			// Token: 0x0600370B RID: 14091 RVA: 0x000C520D File Offset: 0x000C340D
			public OwaActionQueueState(CustomSyncState state)
			{
				this.state = state;
			}

			// Token: 0x17000CE1 RID: 3297
			// (get) Token: 0x0600370C RID: 14092 RVA: 0x000C5218 File Offset: 0x000C3418
			// (set) Token: 0x0600370D RID: 14093 RVA: 0x000C5246 File Offset: 0x000C3446
			public string LastActionId
			{
				get
				{
					StringData stringData = (StringData)this.state["LastActionId"];
					if (stringData == null)
					{
						return null;
					}
					return stringData.Data;
				}
				set
				{
					this.state["LastActionId"] = new StringData(value);
				}
			}

			// Token: 0x17000CE2 RID: 3298
			// (get) Token: 0x0600370E RID: 14094 RVA: 0x000C525E File Offset: 0x000C345E
			// (set) Token: 0x0600370F RID: 14095 RVA: 0x000C526B File Offset: 0x000C346B
			public T LastActionResults
			{
				get
				{
					return this.GetObject<T>("LastActionResults");
				}
				set
				{
					this.SetObject<T>("LastActionResults", value);
					if (value != null)
					{
						this.EnsureLastActionResultsCanBeDeserialized();
					}
				}
			}

			// Token: 0x17000CE3 RID: 3299
			// (get) Token: 0x06003710 RID: 14096 RVA: 0x000C5287 File Offset: 0x000C3487
			// (set) Token: 0x06003711 RID: 14097 RVA: 0x000C5294 File Offset: 0x000C3494
			public JsonFaultDetail LastActionError
			{
				get
				{
					return this.GetObject<JsonFaultDetail>("LastActionException");
				}
				set
				{
					this.SetObject<JsonFaultDetail>("LastActionException", value);
				}
			}

			// Token: 0x06003712 RID: 14098 RVA: 0x000C52A4 File Offset: 0x000C34A4
			private static string Serialize<D>(D data)
			{
				DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(D));
				string result;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					dataContractJsonSerializer.WriteObject(memoryStream, data);
					memoryStream.Seek(0L, SeekOrigin.Begin);
					using (StreamReader streamReader = new StreamReader(memoryStream))
					{
						result = streamReader.ReadToEnd();
					}
				}
				return result;
			}

			// Token: 0x06003713 RID: 14099 RVA: 0x000C5320 File Offset: 0x000C3520
			private static D Deserialize<D>(string data)
			{
				DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(D));
				D result;
				using (MemoryStream memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(data)))
				{
					object obj = dataContractJsonSerializer.ReadObject(memoryStream);
					result = (D)((object)obj);
				}
				return result;
			}

			// Token: 0x06003714 RID: 14100 RVA: 0x000C537C File Offset: 0x000C357C
			private void SetObject<D>(string key, D data)
			{
				string data2 = CallContext.OwaActionQueueState<T>.Serialize<D>(data);
				this.state[key] = new StringData(data2);
			}

			// Token: 0x06003715 RID: 14101 RVA: 0x000C53A4 File Offset: 0x000C35A4
			private D GetObject<D>(string key)
			{
				StringData stringData = (StringData)this.state[key];
				if (stringData == null)
				{
					return default(D);
				}
				D result;
				try
				{
					result = CallContext.OwaActionQueueState<T>.Deserialize<D>(stringData.Data);
				}
				catch (SerializationException ex)
				{
					throw new CustomSerializationException(CoreResources.ActionQueueDeserializationError(key, stringData.Data, typeof(D).FullName, ex.Message), ex);
				}
				return result;
			}

			// Token: 0x06003716 RID: 14102 RVA: 0x000C5418 File Offset: 0x000C3618
			private void EnsureLastActionResultsCanBeDeserialized()
			{
				T lastActionResults = this.LastActionResults;
			}

			// Token: 0x04001EA8 RID: 7848
			private CustomSyncState state;

			// Token: 0x0200070A RID: 1802
			private static class PropertyNames
			{
				// Token: 0x04001EA9 RID: 7849
				public const string LastActionId = "LastActionId";

				// Token: 0x04001EAA RID: 7850
				public const string LastActionResults = "LastActionResults";

				// Token: 0x04001EAB RID: 7851
				public const string LastActionError = "LastActionException";
			}
		}

		// Token: 0x0200070B RID: 1803
		private class OwaActionQueueStateInfo : SyncStateInfo
		{
			// Token: 0x17000CE4 RID: 3300
			// (get) Token: 0x06003717 RID: 14103 RVA: 0x000C5421 File Offset: 0x000C3621
			// (set) Token: 0x06003718 RID: 14104 RVA: 0x000C5428 File Offset: 0x000C3628
			public override string UniqueName
			{
				get
				{
					return "ActionQueue";
				}
				set
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x17000CE5 RID: 3301
			// (get) Token: 0x06003719 RID: 14105 RVA: 0x000C542F File Offset: 0x000C362F
			public override int Version
			{
				get
				{
					return 1;
				}
			}
		}
	}
}
