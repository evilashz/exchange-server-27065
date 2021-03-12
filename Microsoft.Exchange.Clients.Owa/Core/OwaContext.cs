using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Exchange.Security;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200017D RID: 381
	public sealed class OwaContext
	{
		// Token: 0x06000D83 RID: 3459 RVA: 0x0005A178 File Offset: 0x00058378
		private OwaContext(HttpContext httpContext)
		{
			this.httpContext = httpContext;
			this.FormsRegistryContext = new FormsRegistryContext(ApplicationElement.NotSet, null, null, null);
			string pathAndQuery = this.GetModifiedUri().PathAndQuery;
			this.latencyDetectionContext = OwaContext.OwaLatencyDetectionContextFactory.CreateContext(Globals.ApplicationVersion, pathAndQuery, new IPerformanceDataProvider[]
			{
				PerformanceContext.Current,
				RpcDataProvider.Instance,
				MemoryDataProvider.Instance
			});
			if (Globals.CollectPerRequestPerformanceStats)
			{
				this.owaPerformanceData = new OwaPerformanceData(httpContext.Request);
			}
			if (!StringSanitizer<OwaHtml>.TrustedStringsInitialized)
			{
				string location = Assembly.GetExecutingAssembly().Location;
				StringSanitizer<OwaHtml>.InitializeTrustedStrings(new string[]
				{
					location
				});
			}
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0005A241 File Offset: 0x00058441
		public static OwaContext Create(HttpContext httpContext)
		{
			return new OwaContext(httpContext);
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x0005A249 File Offset: 0x00058449
		// (set) Token: 0x06000D86 RID: 3462 RVA: 0x0005A251 File Offset: 0x00058451
		internal SecureNameValueCollection FormNameValueCollection
		{
			get
			{
				return this.formNameValueCollection;
			}
			set
			{
				this.formNameValueCollection = value;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x0005A25A File Offset: 0x0005845A
		// (set) Token: 0x06000D88 RID: 3464 RVA: 0x0005A262 File Offset: 0x00058462
		public bool HandledCriticalError
		{
			get
			{
				return this.handledCriticalError;
			}
			private set
			{
				this.handledCriticalError = value;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x0005A26B File Offset: 0x0005846B
		// (set) Token: 0x06000D8A RID: 3466 RVA: 0x0005A273 File Offset: 0x00058473
		public bool IsAsyncRequest
		{
			get
			{
				return this.isAsyncRequest;
			}
			set
			{
				this.isAsyncRequest = value;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x0005A27C File Offset: 0x0005847C
		// (set) Token: 0x06000D8C RID: 3468 RVA: 0x0005A284 File Offset: 0x00058484
		public bool ShouldDeferInlineImages
		{
			get
			{
				return this.shouldDeferInlineImages;
			}
			set
			{
				this.shouldDeferInlineImages = value;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000D8D RID: 3469 RVA: 0x0005A28D File Offset: 0x0005848D
		// (set) Token: 0x06000D8E RID: 3470 RVA: 0x0005A295 File Offset: 0x00058495
		internal bool IgnoreUnlockForcefully { get; set; }

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x0005A2A0 File Offset: 0x000584A0
		public static OwaContext Current
		{
			get
			{
				HttpContext httpContext = HttpContext.Current;
				if (httpContext != null)
				{
					return (OwaContext)httpContext.Items["OwaContext"];
				}
				return null;
			}
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x0005A2CD File Offset: 0x000584CD
		internal static OwaContext Get(HttpContext httpContext)
		{
			return (OwaContext)httpContext.Items["OwaContext"];
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0005A2E4 File Offset: 0x000584E4
		internal static void Set(HttpContext httpContext, OwaContext owaContext)
		{
			httpContext.Items["OwaContext"] = owaContext;
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0005A2F8 File Offset: 0x000584F8
		internal static IBudget TryGetCurrentBudget()
		{
			OwaContext owaContext = OwaContext.Current;
			if (owaContext != null)
			{
				return owaContext.Budget;
			}
			return null;
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0005A316 File Offset: 0x00058516
		internal void DoNotTriggerLatencyDetectionReport()
		{
			this.latencyDetectionContext.TriggerOptions = TriggerOptions.DoNotTrigger;
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0005A324 File Offset: 0x00058524
		internal void ExitLatencyDetectionContext()
		{
			if (this.LogonIdentity != null)
			{
				this.latencyDetectionContext.UserIdentity = this.LogonIdentity.SafeGetRenderableName();
			}
			if (string.IsNullOrEmpty(this.latencyDetectionContext.UserIdentity) && this.mailboxIdentity != null)
			{
				this.latencyDetectionContext.UserIdentity = this.mailboxIdentity.SafeGetRenderableName();
			}
			TaskPerformanceData[] array = this.latencyDetectionContext.StopAndFinalizeCollection();
			int num = 0;
			if (array.Length > num)
			{
				this.ldapData = array[num];
			}
			int num2 = 1;
			if (array.Length > num2)
			{
				this.rpcData = array[num2];
			}
			int num3 = 2;
			if (array.Length > num3)
			{
				this.MemoryData = array[num3];
			}
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0005A3C0 File Offset: 0x000585C0
		internal void UnlockMinResourcesOnCriticalError()
		{
			try
			{
				if (this.userContext != null && this.userContext.LockedByCurrentThread())
				{
					this.userContext.DisconnectAllSessions();
					this.userContext.UnlockForcefully();
				}
			}
			catch (InvalidOperationException)
			{
			}
			this.HandledCriticalError = true;
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x0005A414 File Offset: 0x00058614
		internal void AddObjectToDisposeOnEndRequest(IDisposable objectToDispose)
		{
			if (objectToDispose == null)
			{
				throw new ArgumentNullException("objectToDispose");
			}
			if (this.objectsToDispose == null)
			{
				this.objectsToDispose = new List<IDisposable>();
			}
			this.objectsToDispose.Add(objectToDispose);
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0005A444 File Offset: 0x00058644
		internal void DisposeObjectsOnEndRequest()
		{
			if (this.formNameValueCollection != null)
			{
				this.formNameValueCollection.Dispose();
				this.formNameValueCollection = null;
			}
			if (this.objectsToDispose != null)
			{
				foreach (IDisposable disposable in this.objectsToDispose)
				{
					try
					{
						disposable.Dispose();
					}
					catch (ObjectDisposedException)
					{
					}
				}
				this.objectsToDispose.Clear();
			}
			if (this.mailboxIdentity != null)
			{
				this.mailboxIdentity.Dispose();
			}
			if (this.logonIdentity != null)
			{
				this.logonIdentity.Dispose();
			}
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0005A4FC File Offset: 0x000586FC
		internal void AcquireBudgetAndStartTiming()
		{
			string callerInfo = "OwaContext.AcquireBudgetAndStartTiming";
			this.budget = StandardBudget.Acquire(this.LogonIdentity.UserSid, BudgetType.Owa, Utilities.CreateScopedADSessionSettings(this.LogonIdentity.DomainName));
			this.budget.CheckOverBudget();
			this.budget.StartConnection(callerInfo);
			this.budget.StartLocal(callerInfo, default(TimeSpan));
			this.httpContext.Response.AppendToLog("&Initial Budget>>");
			this.httpContext.Response.AppendToLog(this.budget.ToString());
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x0005A594 File Offset: 0x00058794
		internal void TryReleaseBudgetAndStopTiming()
		{
			if (this.budget != null)
			{
				this.httpContext.Response.AppendToLog("&End Budget>>");
				this.httpContext.Response.AppendToLog(this.budget.ToString());
				this.budget.Dispose();
				this.budget = null;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x0005A5EB File Offset: 0x000587EB
		internal IBudget Budget
		{
			get
			{
				return this.budget;
			}
		}

		// Token: 0x170003A4 RID: 932
		public object this[OwaContextProperty property]
		{
			get
			{
				if (this.propertyBag.ContainsKey(property))
				{
					return this.propertyBag[property];
				}
				return null;
			}
			set
			{
				this.propertyBag[property] = value;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x0005A620 File Offset: 0x00058820
		public HttpContext HttpContext
		{
			get
			{
				return this.httpContext;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x0005A628 File Offset: 0x00058828
		// (set) Token: 0x06000D9F RID: 3487 RVA: 0x0005A644 File Offset: 0x00058844
		public UserContext UserContext
		{
			get
			{
				if (Globals.OwaVDirType == OWAVDirType.Calendar)
				{
					throw new InvalidOperationException("No User Context is available in the anonymous calendar vdir");
				}
				return this.userContext;
			}
			internal set
			{
				this.userContext = value;
				if (this.userContext != null)
				{
					if (Globals.CollectPerRequestPerformanceStats && this.userContext.PerformanceConsoleNotifier != null && this.owaPerformanceData != null)
					{
						this.userContext.PerformanceConsoleNotifier.AddPerformanceData(this.owaPerformanceData);
					}
					if (this.logonIdentity == null)
					{
						if (this.UserContext.LogonIdentity == null)
						{
							throw new ArgumentException("logon identity in the user context is null");
						}
					}
					else
					{
						this.userContext.LogonIdentity.Refresh(this.logonIdentity);
					}
				}
			}
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0005A6C8 File Offset: 0x000588C8
		public UserContext TryGetUserContext()
		{
			return this.userContext;
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x0005A6D0 File Offset: 0x000588D0
		// (set) Token: 0x06000DA2 RID: 3490 RVA: 0x0005A6EA File Offset: 0x000588EA
		internal AnonymousSessionContext AnonymousSessionContext
		{
			get
			{
				if (Globals.OwaVDirType == OWAVDirType.OWA)
				{
					throw new InvalidOperationException("No anonymous session context is available in the OWA vdir");
				}
				return this.anonymousSessionContext;
			}
			set
			{
				if (Globals.OwaVDirType == OWAVDirType.OWA)
				{
					throw new InvalidOperationException("No anonymous session context is available in the OWA vdir");
				}
				if (this.anonymousSessionContext == null)
				{
					this.anonymousSessionContext = value;
					return;
				}
				throw new InvalidOperationException("Cannot set anonymousSessionContext twice!");
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x0005A718 File Offset: 0x00058918
		internal ISessionContext SessionContext
		{
			get
			{
				if (Globals.OwaVDirType == OWAVDirType.OWA)
				{
					return this.UserContext;
				}
				return this.anonymousSessionContext;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x0005A72E File Offset: 0x0005892E
		// (set) Token: 0x06000DA5 RID: 3493 RVA: 0x0005A736 File Offset: 0x00058936
		public OwaRequestType RequestType
		{
			get
			{
				return this.requestType;
			}
			set
			{
				this.requestType = value;
				if (Globals.CollectPerRequestPerformanceStats)
				{
					this.owaPerformanceData.RequestType = value.ToString();
				}
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x0005A75C File Offset: 0x0005895C
		public bool IsMowa
		{
			get
			{
				return this.httpContext.Request.Headers["X-OWA-Protocol"] == "MOWA";
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x0005A782 File Offset: 0x00058982
		// (set) Token: 0x06000DA8 RID: 3496 RVA: 0x0005A7AB File Offset: 0x000589AB
		public OwaIdentity LogonIdentity
		{
			get
			{
				if (this.userContext != null && this.userContext.LogonIdentity != null)
				{
					return this.userContext.LogonIdentity;
				}
				return this.logonIdentity;
			}
			internal set
			{
				this.logonIdentity = value;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x0005A7B4 File Offset: 0x000589B4
		// (set) Token: 0x06000DAA RID: 3498 RVA: 0x0005A7DD File Offset: 0x000589DD
		public OwaIdentity MailboxIdentity
		{
			get
			{
				if (this.userContext != null && this.userContext.MailboxIdentity != null)
				{
					return this.userContext.MailboxIdentity;
				}
				return this.mailboxIdentity;
			}
			internal set
			{
				this.mailboxIdentity = value;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x0005A7E6 File Offset: 0x000589E6
		internal TaskPerformanceData RpcData
		{
			get
			{
				return this.rpcData;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x0005A7EE File Offset: 0x000589EE
		// (set) Token: 0x06000DAD RID: 3501 RVA: 0x0005A7F6 File Offset: 0x000589F6
		internal PerformanceData EwsRpcData
		{
			get
			{
				return this.ewsRpcData;
			}
			set
			{
				this.ewsRpcData = value;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x0005A7FF File Offset: 0x000589FF
		internal TaskPerformanceData LdapData
		{
			get
			{
				return this.ldapData;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x0005A807 File Offset: 0x00058A07
		// (set) Token: 0x06000DB0 RID: 3504 RVA: 0x0005A80F File Offset: 0x00058A0F
		internal PerformanceData EwsLdapData
		{
			get
			{
				return this.ewsLdapData;
			}
			set
			{
				this.ewsLdapData = value;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x0005A818 File Offset: 0x00058A18
		// (set) Token: 0x06000DB2 RID: 3506 RVA: 0x0005A820 File Offset: 0x00058A20
		internal string EwsPerformanceHeader
		{
			get
			{
				return this.ewsPerformanceHeader;
			}
			set
			{
				this.ewsPerformanceHeader = value;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x0005A829 File Offset: 0x00058A29
		// (set) Token: 0x06000DB4 RID: 3508 RVA: 0x0005A831 File Offset: 0x00058A31
		internal TaskPerformanceData MemoryData { get; private set; }

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x0005A83C File Offset: 0x00058A3C
		internal long RequestLatencyMilliseconds
		{
			get
			{
				return (long)this.latencyDetectionContext.Elapsed.TotalMilliseconds;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x0005A860 File Offset: 0x00058A60
		internal long RequestCpuLatencyMilliseconds
		{
			get
			{
				return (long)this.latencyDetectionContext.ElapsedCpu.TotalMilliseconds;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x0005A881 File Offset: 0x00058A81
		internal bool HasTrustworthyRequestCpuLatency
		{
			get
			{
				return this.latencyDetectionContext.HasTrustworthyCpuTime;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x0005A88E File Offset: 0x00058A8E
		// (set) Token: 0x06000DB9 RID: 3513 RVA: 0x0005A896 File Offset: 0x00058A96
		internal ExchangePrincipal LogonExchangePrincipal
		{
			get
			{
				return this.logonExchangePrincipal;
			}
			set
			{
				this.logonExchangePrincipal = value;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x0005A89F File Offset: 0x00058A9F
		// (set) Token: 0x06000DBB RID: 3515 RVA: 0x0005A8A7 File Offset: 0x00058AA7
		internal ExchangePrincipal ExchangePrincipal
		{
			get
			{
				return this.mailboxExchangePrincipal;
			}
			set
			{
				this.mailboxExchangePrincipal = value;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000DBC RID: 3516 RVA: 0x0005A8B0 File Offset: 0x00058AB0
		internal OwaPerformanceData OwaPerformanceData
		{
			get
			{
				return this.owaPerformanceData;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x0005A8B8 File Offset: 0x00058AB8
		// (set) Token: 0x06000DBE RID: 3518 RVA: 0x0005A8C0 File Offset: 0x00058AC0
		internal SearchPerformanceData SearchPerformanceData
		{
			get
			{
				return this.searchPerformanceData;
			}
			set
			{
				this.searchPerformanceData = value;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x0005A8C9 File Offset: 0x00058AC9
		// (set) Token: 0x06000DC0 RID: 3520 RVA: 0x0005A8D1 File Offset: 0x00058AD1
		internal RequestExecution RequestExecution
		{
			get
			{
				return this.requestExecution;
			}
			set
			{
				this.requestExecution = value;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x0005A8DC File Offset: 0x00058ADC
		internal string UrlToHost
		{
			get
			{
				if (this.urlToHost == null)
				{
					HttpRequest request = this.HttpContext.Request;
					this.urlToHost = (request.IsSecureConnection ? "https://" : "http://");
					this.urlToHost += request.Url.Host;
					if (request.Url.Port != 80 && request.Url.Port != 443)
					{
						this.urlToHost = this.urlToHost + ":" + request.Url.Port.ToString(CultureInfo.InvariantCulture);
					}
				}
				return this.urlToHost;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x0005A98B File Offset: 0x00058B8B
		internal string LocalHostName
		{
			get
			{
				if (this.localHostName == null)
				{
					this.localHostName = this.UrlToHost + this.HttpContext.Request.ApplicationPath;
				}
				return this.localHostName;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x0005A9BC File Offset: 0x00058BBC
		// (set) Token: 0x06000DC4 RID: 3524 RVA: 0x0005A9C4 File Offset: 0x00058BC4
		internal ProxyUri SecondCasUri
		{
			get
			{
				return this.secondCasUri;
			}
			set
			{
				this.secondCasUri = value;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x0005A9CD File Offset: 0x00058BCD
		// (set) Token: 0x06000DC6 RID: 3526 RVA: 0x0005A9D5 File Offset: 0x00058BD5
		internal bool IsTemporaryRedirection
		{
			get
			{
				return this.isTemporaryRedirection;
			}
			set
			{
				this.isTemporaryRedirection = value;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x0005A9DE File Offset: 0x00058BDE
		// (set) Token: 0x06000DC8 RID: 3528 RVA: 0x0005A9E6 File Offset: 0x00058BE6
		internal bool CanAccessUsualAddressInAnHour
		{
			get
			{
				return this.canAccessUsualAddressInAnHour;
			}
			set
			{
				this.canAccessUsualAddressInAnHour = value;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x0005A9EF File Offset: 0x00058BEF
		// (set) Token: 0x06000DCA RID: 3530 RVA: 0x0005A9F7 File Offset: 0x00058BF7
		internal FormsRegistryContext FormsRegistryContext
		{
			get
			{
				return this.formsRegistryContext;
			}
			set
			{
				this.formsRegistryContext = value;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x0005AA00 File Offset: 0x00058C00
		// (set) Token: 0x06000DCC RID: 3532 RVA: 0x0005AA08 File Offset: 0x00058C08
		internal bool IsProxyRequest
		{
			get
			{
				return this.isProxyRequest;
			}
			set
			{
				this.isProxyRequest = value;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x0005AA11 File Offset: 0x00058C11
		// (set) Token: 0x06000DCE RID: 3534 RVA: 0x0005AA19 File Offset: 0x00058C19
		internal bool IsFromCafe
		{
			get
			{
				return this.isFromCafe;
			}
			set
			{
				this.isFromCafe = value;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x0005AA22 File Offset: 0x00058C22
		// (set) Token: 0x06000DD0 RID: 3536 RVA: 0x0005AA2A File Offset: 0x00058C2A
		internal string DestinationUrl
		{
			get
			{
				return this.destinationUrl;
			}
			set
			{
				this.destinationUrl = value;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x0005AA33 File Offset: 0x00058C33
		// (set) Token: 0x06000DD2 RID: 3538 RVA: 0x0005AA3B File Offset: 0x00058C3B
		internal string DestinationUrlQueryString
		{
			get
			{
				return this.destinationUrlQueryString;
			}
			set
			{
				this.destinationUrlQueryString = value;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x0005AA44 File Offset: 0x00058C44
		// (set) Token: 0x06000DD4 RID: 3540 RVA: 0x0005AA4C File Offset: 0x00058C4C
		internal bool IsManualRedirect
		{
			get
			{
				return this.isManualRedirect;
			}
			set
			{
				this.isManualRedirect = value;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x0005AA55 File Offset: 0x00058C55
		// (set) Token: 0x06000DD6 RID: 3542 RVA: 0x0005AA5D File Offset: 0x00058C5D
		internal HttpStatusCode HttpStatusCode
		{
			get
			{
				return this.httpStatusCode;
			}
			set
			{
				this.httpStatusCode = value;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x0005AA66 File Offset: 0x00058C66
		// (set) Token: 0x06000DD8 RID: 3544 RVA: 0x0005AA6E File Offset: 0x00058C6E
		public string ErrorString
		{
			get
			{
				return this.errorString;
			}
			set
			{
				this.errorString = value;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x0005AA77 File Offset: 0x00058C77
		// (set) Token: 0x06000DDA RID: 3546 RVA: 0x0005AA7F File Offset: 0x00058C7F
		internal CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
			set
			{
				this.culture = value;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x0005AA88 File Offset: 0x00058C88
		// (set) Token: 0x06000DDC RID: 3548 RVA: 0x0005AA90 File Offset: 0x00058C90
		internal Guid TraceRequestId
		{
			get
			{
				return this.traceRequestID;
			}
			set
			{
				this.traceRequestID = value;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x0005AA9C File Offset: 0x00058C9C
		// (set) Token: 0x06000DDE RID: 3550 RVA: 0x0005AB17 File Offset: 0x00058D17
		internal string TimeZoneId
		{
			get
			{
				if (this.timeZoneId == null)
				{
					HttpCookie httpCookie = this.httpContext.Request.Cookies["tzid"];
					this.timeZoneId = ((httpCookie != null && !string.IsNullOrEmpty(httpCookie.Value)) ? httpCookie.Value : null);
					if (this.timeZoneId == null && this.UserContext != null)
					{
						this.TimeZoneId = this.UserContext.TimeZone.Id;
					}
				}
				return this.timeZoneId;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException("TimezoneId can't be set to null");
				}
				this.timeZoneId = value;
				this.httpContext.Response.Cookies.Add(new HttpCookie("tzid", value));
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x0005AB53 File Offset: 0x00058D53
		// (set) Token: 0x06000DE0 RID: 3552 RVA: 0x0005AB6C File Offset: 0x00058D6C
		public bool LoadedByFormsRegistry
		{
			get
			{
				return this[OwaContextProperty.LoadedByFormsRegistry] != null && (bool)this[OwaContextProperty.LoadedByFormsRegistry];
			}
			set
			{
				this[OwaContextProperty.LoadedByFormsRegistry] = value;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x0005AB7B File Offset: 0x00058D7B
		// (set) Token: 0x06000DE2 RID: 3554 RVA: 0x0005AB94 File Offset: 0x00058D94
		public bool ErrorSent
		{
			get
			{
				return this[OwaContextProperty.ErrorSent] != null && (bool)this[OwaContextProperty.ErrorSent];
			}
			set
			{
				this[OwaContextProperty.ErrorSent] = value;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x0005ABA3 File Offset: 0x00058DA3
		// (set) Token: 0x06000DE4 RID: 3556 RVA: 0x0005ABB2 File Offset: 0x00058DB2
		public ErrorInformation ErrorInformation
		{
			get
			{
				return (ErrorInformation)this[OwaContextProperty.ErrorInformation];
			}
			set
			{
				this[OwaContextProperty.ErrorInformation] = value;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x0005ABBD File Offset: 0x00058DBD
		// (set) Token: 0x06000DE6 RID: 3558 RVA: 0x0005ABCC File Offset: 0x00058DCC
		internal OwaStoreObjectId PreFormActionId
		{
			get
			{
				return (OwaStoreObjectId)this[OwaContextProperty.PreFormActionId];
			}
			set
			{
				this[OwaContextProperty.PreFormActionId] = value;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x0005ABD7 File Offset: 0x00058DD7
		// (set) Token: 0x06000DE8 RID: 3560 RVA: 0x0005ABE1 File Offset: 0x00058DE1
		public object PreFormActionData
		{
			get
			{
				return this[OwaContextProperty.PreFormActionData];
			}
			set
			{
				this[OwaContextProperty.PreFormActionData] = value;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x0005ABEC File Offset: 0x00058DEC
		internal Dictionary<string, object> InternalHandlerParameters
		{
			get
			{
				if (this.internalHandlerParameters == null)
				{
					this.internalHandlerParameters = new Dictionary<string, object>();
				}
				return this.internalHandlerParameters;
			}
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0005AC07 File Offset: 0x00058E07
		public void SetInternalHandlerParameter(string name, object value)
		{
			this.InternalHandlerParameters.Add(name, value);
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x0005AC16 File Offset: 0x00058E16
		// (set) Token: 0x06000DEC RID: 3564 RVA: 0x0005AC1E File Offset: 0x00058E1E
		internal ProxyUriQueue ProxyUriQueue
		{
			get
			{
				return this.proxyUriQueue;
			}
			set
			{
				this.proxyUriQueue = value;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x0005AC27 File Offset: 0x00058E27
		// (set) Token: 0x06000DEE RID: 3566 RVA: 0x0005AC2F File Offset: 0x00058E2F
		public ServerVersion ProxyCasVersion
		{
			get
			{
				return this.proxyCasVersion;
			}
			internal set
			{
				this.proxyCasVersion = value;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x0005AC38 File Offset: 0x00058E38
		// (set) Token: 0x06000DF0 RID: 3568 RVA: 0x0005AC40 File Offset: 0x00058E40
		public Uri ProxyCasUri
		{
			get
			{
				return this.proxyCasUri;
			}
			internal set
			{
				this.proxyCasUri = value;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x0005AC49 File Offset: 0x00058E49
		// (set) Token: 0x06000DF2 RID: 3570 RVA: 0x0005AC51 File Offset: 0x00058E51
		internal SecurityIdentifier ProxyUserSid
		{
			get
			{
				return this.proxyUserSid;
			}
			set
			{
				this.proxyUserSid = value;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x0005AC5A File Offset: 0x00058E5A
		// (set) Token: 0x06000DF4 RID: 3572 RVA: 0x0005AC62 File Offset: 0x00058E62
		internal bool IsExplicitLogon
		{
			get
			{
				return this.isExplicitLogon;
			}
			set
			{
				this.isExplicitLogon = value;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x0005AC6B File Offset: 0x00058E6B
		// (set) Token: 0x06000DF6 RID: 3574 RVA: 0x0005AC73 File Offset: 0x00058E73
		internal bool IsAlternateMailbox
		{
			get
			{
				return this.isAlternateMailbox;
			}
			set
			{
				this.isAlternateMailbox = value;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x0005AC7C File Offset: 0x00058E7C
		internal bool IsDifferentMailbox
		{
			get
			{
				return this.IsExplicitLogon || this.IsAlternateMailbox;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x0005AC8E File Offset: 0x00058E8E
		// (set) Token: 0x06000DF9 RID: 3577 RVA: 0x0005AC96 File Offset: 0x00058E96
		internal bool IsProxyWebPart
		{
			get
			{
				return this.isProxyWebPart;
			}
			set
			{
				this.isProxyWebPart = value;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x0005AC9F File Offset: 0x00058E9F
		// (set) Token: 0x06000DFB RID: 3579 RVA: 0x0005ACA7 File Offset: 0x00058EA7
		internal CultureInfo LanguagePostUserCulture
		{
			get
			{
				return this.languagePostUserCulture;
			}
			set
			{
				this.languagePostUserCulture = value;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x0005ACB0 File Offset: 0x00058EB0
		// (set) Token: 0x06000DFD RID: 3581 RVA: 0x0005ACB8 File Offset: 0x00058EB8
		internal bool FailedToSaveUserCulture
		{
			get
			{
				return this.failedToSaveUserCulture;
			}
			set
			{
				this.failedToSaveUserCulture = value;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x0005ACC1 File Offset: 0x00058EC1
		// (set) Token: 0x06000DFF RID: 3583 RVA: 0x0005ACC9 File Offset: 0x00058EC9
		internal SerializedClientSecurityContext ReceivedSerializedClientSecurityContext
		{
			get
			{
				return this.receivedSerializedClientSecurityContext;
			}
			set
			{
				this.receivedSerializedClientSecurityContext = value;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x0005ACD2 File Offset: 0x00058ED2
		// (set) Token: 0x06000E01 RID: 3585 RVA: 0x0005ACDA File Offset: 0x00058EDA
		internal IDictionary<string, string> CustomErrorInfo
		{
			get
			{
				return this.customErrorInfo;
			}
			set
			{
				this.customErrorInfo = value;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x0005ACE4 File Offset: 0x00058EE4
		internal SanitizingTextWriter<OwaHtml> SanitizingResponseWriter
		{
			get
			{
				if (this.cachedResponseOutput != this.httpContext.Response.Output)
				{
					this.cachedResponseOutput = this.httpContext.Response.Output;
					this.sanitizingResponseWriter = new SanitizingTextWriter<OwaHtml>(this.cachedResponseOutput);
				}
				return this.sanitizingResponseWriter;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x0005AD36 File Offset: 0x00058F36
		internal LiveAssetReader LiveAssetReader
		{
			get
			{
				if (this.liveAssetReader == null)
				{
					this.liveAssetReader = new LiveAssetReader(this.httpContext);
				}
				return this.liveAssetReader;
			}
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0005AD58 File Offset: 0x00058F58
		private Uri GetModifiedUri()
		{
			Uri uri = this.httpContext.Request.Url;
			if (!string.IsNullOrEmpty(uri.Query))
			{
				MatchCollection matchCollection = OwaContext.AllowedParametersExpression.Matches(uri.Query);
				UriBuilder uriBuilder = new UriBuilder(uri);
				StringBuilder stringBuilder = new StringBuilder(uriBuilder.Query.Length);
				if (matchCollection.Count > 0)
				{
					int num = matchCollection.Count - 1;
					for (int i = 0; i < num; i++)
					{
						stringBuilder.Append(matchCollection[i].Value).Append('&');
					}
					stringBuilder.Append(matchCollection[num].Value);
				}
				uriBuilder.Query = stringBuilder.ToString();
				uri = uriBuilder.Uri;
			}
			return uri;
		}

		// Token: 0x04000944 RID: 2372
		private const string TimeZoneIdCookieName = "tzid";

		// Token: 0x04000945 RID: 2373
		private const string OwaContextKey = "OwaContext";

		// Token: 0x04000946 RID: 2374
		private static readonly TimeSpan DefaultOwaThreshold = TimeSpan.FromSeconds(30.0);

		// Token: 0x04000947 RID: 2375
		private static readonly TimeSpan MinimumOwaThreshold = TimeSpan.FromMilliseconds(100.0);

		// Token: 0x04000948 RID: 2376
		private static readonly LatencyDetectionContextFactory OwaLatencyDetectionContextFactory = LatencyDetectionContextFactory.CreateFactory("OwaContext", OwaContext.MinimumOwaThreshold, OwaContext.DefaultOwaThreshold);

		// Token: 0x04000949 RID: 2377
		private static readonly Regex AllowedParametersExpression = new Regex("(?:a|ae|t|ns)=\\w+", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x0400094A RID: 2378
		private static readonly int PropertyBagSize = Enum.GetValues(typeof(OwaContextProperty)).Length;

		// Token: 0x0400094B RID: 2379
		private readonly Dictionary<OwaContextProperty, object> propertyBag = new Dictionary<OwaContextProperty, object>(OwaContext.PropertyBagSize);

		// Token: 0x0400094C RID: 2380
		private HttpContext httpContext;

		// Token: 0x0400094D RID: 2381
		private UserContext userContext;

		// Token: 0x0400094E RID: 2382
		private OwaRequestType requestType;

		// Token: 0x0400094F RID: 2383
		private OwaIdentity logonIdentity;

		// Token: 0x04000950 RID: 2384
		private OwaIdentity mailboxIdentity;

		// Token: 0x04000951 RID: 2385
		private ExchangePrincipal logonExchangePrincipal;

		// Token: 0x04000952 RID: 2386
		private ExchangePrincipal mailboxExchangePrincipal;

		// Token: 0x04000953 RID: 2387
		private RequestExecution requestExecution;

		// Token: 0x04000954 RID: 2388
		private string urlToHost;

		// Token: 0x04000955 RID: 2389
		private string localHostName;

		// Token: 0x04000956 RID: 2390
		private FormsRegistryContext formsRegistryContext;

		// Token: 0x04000957 RID: 2391
		private bool isExplicitLogon;

		// Token: 0x04000958 RID: 2392
		private bool isAlternateMailbox;

		// Token: 0x04000959 RID: 2393
		private CultureInfo languagePostUserCulture;

		// Token: 0x0400095A RID: 2394
		private bool failedToSaveUserCulture;

		// Token: 0x0400095B RID: 2395
		private SerializedClientSecurityContext receivedSerializedClientSecurityContext;

		// Token: 0x0400095C RID: 2396
		private ServerVersion proxyCasVersion;

		// Token: 0x0400095D RID: 2397
		private Uri proxyCasUri;

		// Token: 0x0400095E RID: 2398
		private SecurityIdentifier proxyUserSid;

		// Token: 0x0400095F RID: 2399
		private bool isProxyRequest;

		// Token: 0x04000960 RID: 2400
		private bool isFromCafe;

		// Token: 0x04000961 RID: 2401
		private ProxyUri secondCasUri;

		// Token: 0x04000962 RID: 2402
		private ProxyUriQueue proxyUriQueue;

		// Token: 0x04000963 RID: 2403
		private bool isProxyWebPart;

		// Token: 0x04000964 RID: 2404
		private bool isTemporaryRedirection;

		// Token: 0x04000965 RID: 2405
		private bool canAccessUsualAddressInAnHour;

		// Token: 0x04000966 RID: 2406
		private bool handledCriticalError;

		// Token: 0x04000967 RID: 2407
		private Dictionary<string, object> internalHandlerParameters;

		// Token: 0x04000968 RID: 2408
		private string destinationUrl;

		// Token: 0x04000969 RID: 2409
		private string destinationUrlQueryString;

		// Token: 0x0400096A RID: 2410
		private bool isManualRedirect = true;

		// Token: 0x0400096B RID: 2411
		private HttpStatusCode httpStatusCode = HttpStatusCode.OK;

		// Token: 0x0400096C RID: 2412
		private string errorString;

		// Token: 0x0400096D RID: 2413
		private CultureInfo culture;

		// Token: 0x0400096E RID: 2414
		private IDictionary<string, string> customErrorInfo;

		// Token: 0x0400096F RID: 2415
		private List<IDisposable> objectsToDispose;

		// Token: 0x04000970 RID: 2416
		internal uint AvailabilityQueryCount;

		// Token: 0x04000971 RID: 2417
		internal long AvailabilityQueryLatency;

		// Token: 0x04000972 RID: 2418
		private LatencyDetectionContext latencyDetectionContext;

		// Token: 0x04000973 RID: 2419
		private TaskPerformanceData rpcData;

		// Token: 0x04000974 RID: 2420
		private TaskPerformanceData ldapData;

		// Token: 0x04000975 RID: 2421
		private PerformanceData ewsRpcData;

		// Token: 0x04000976 RID: 2422
		private PerformanceData ewsLdapData;

		// Token: 0x04000977 RID: 2423
		private string ewsPerformanceHeader;

		// Token: 0x04000978 RID: 2424
		private Guid traceRequestID;

		// Token: 0x04000979 RID: 2425
		private OwaPerformanceData owaPerformanceData;

		// Token: 0x0400097A RID: 2426
		private SearchPerformanceData searchPerformanceData;

		// Token: 0x0400097B RID: 2427
		private IStandardBudget budget;

		// Token: 0x0400097C RID: 2428
		private bool isAsyncRequest;

		// Token: 0x0400097D RID: 2429
		private string timeZoneId;

		// Token: 0x0400097E RID: 2430
		private bool shouldDeferInlineImages;

		// Token: 0x0400097F RID: 2431
		private SanitizingTextWriter<OwaHtml> sanitizingResponseWriter;

		// Token: 0x04000980 RID: 2432
		private TextWriter cachedResponseOutput;

		// Token: 0x04000981 RID: 2433
		private SecureNameValueCollection formNameValueCollection;

		// Token: 0x04000982 RID: 2434
		private LiveAssetReader liveAssetReader;

		// Token: 0x04000983 RID: 2435
		private AnonymousSessionContext anonymousSessionContext;
	}
}
