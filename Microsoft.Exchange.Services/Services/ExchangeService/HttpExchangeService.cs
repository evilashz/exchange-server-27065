using System;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Services.ExchangeService
{
	// Token: 0x02000DE2 RID: 3554
	internal class HttpExchangeService : ExchangeServiceBase
	{
		// Token: 0x06005BF7 RID: 23543 RVA: 0x0011DB44 File Offset: 0x0011BD44
		public HttpExchangeService(HttpContext httpContext, IActivityScope activityScope, IStandardBudget budget)
		{
			ArgumentValidator.ThrowIfNull("httpContext", httpContext);
			ArgumentValidator.ThrowIfNull("activityScope", activityScope);
			ArgumentValidator.ThrowIfNull("budget", budget);
			this.HttpContext = httpContext;
			base.ActivityScope = activityScope;
			this.Budget = budget;
			base.CallWithExceptionHandling(ExecutionOption.Default, new Action(this.InitializeCallContext));
		}

		// Token: 0x170014E1 RID: 5345
		// (get) Token: 0x06005BF8 RID: 23544 RVA: 0x0011DBA4 File Offset: 0x0011BDA4
		// (set) Token: 0x06005BF9 RID: 23545 RVA: 0x0011DBAC File Offset: 0x0011BDAC
		private protected HttpContext HttpContext { protected get; private set; }

		// Token: 0x170014E2 RID: 5346
		// (get) Token: 0x06005BFA RID: 23546 RVA: 0x0011DBB5 File Offset: 0x0011BDB5
		// (set) Token: 0x06005BFB RID: 23547 RVA: 0x0011DBBD File Offset: 0x0011BDBD
		private protected IStandardBudget Budget { protected get; private set; }

		// Token: 0x06005BFC RID: 23548 RVA: 0x0011DBC8 File Offset: 0x0011BDC8
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				RequestDetailsLogger current = RequestDetailsLoggerBase<RequestDetailsLogger>.GetCurrent(base.CallContext.HttpContext);
				if (current != null)
				{
					current.Dispose();
				}
				if (base.CallContext != null)
				{
					base.CallContext.DisposeForExchangeService();
					base.CallContext = null;
				}
			}
		}

		// Token: 0x06005BFD RID: 23549 RVA: 0x0011DC0C File Offset: 0x0011BE0C
		private void InitializeCallContext()
		{
			HttpContext.Current = this.HttpContext;
			HttpExchangeService.EwsGlobals.InitIfNeeded(this.HttpContext);
			if (RequestDetailsLogger.Current != null && RequestDetailsLogger.Current.IsDisposed)
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SetCurrent(HttpContext.Current, null);
				CallContext.SetCurrent(null);
				HttpContext.Current.Items["CallContext"] = null;
			}
			RequestDetailsLogger requestDetailsLogger = RequestDetailsLoggerBase<RequestDetailsLogger>.InitializeRequestLogger(base.ActivityScope);
			requestDetailsLogger.EndActivityContext = false;
			requestDetailsLogger.SkipLogging = true;
			BudgetAdapter budget = new BudgetAdapter(this.Budget);
			base.CallContext = CallContext.CreateForExchangeService(this.HttpContext, HttpExchangeService.EwsGlobals.AppWideStoreSessionCache, HttpExchangeService.EwsGlobals.AcceptedDomainCache, HttpExchangeService.EwsGlobals.UserWorkloadManager, budget, Thread.CurrentThread.CurrentCulture);
		}

		// Token: 0x02000DE3 RID: 3555
		private static class EwsGlobals
		{
			// Token: 0x06005BFE RID: 23550 RVA: 0x0011DCBC File Offset: 0x0011BEBC
			public static void InitIfNeeded(HttpContext httpContext)
			{
				if (!HttpExchangeService.EwsGlobals.initialized)
				{
					lock (HttpExchangeService.EwsGlobals.staticLock)
					{
						if (!HttpExchangeService.EwsGlobals.initialized)
						{
							HttpApplicationState application = httpContext.Application;
							ADIdentityInformationCache.Initialize(HttpExchangeService.EwsGlobals.ADIdentityCacheSize.Value);
							application["WS_APPWideMailboxCacheKey"] = (HttpExchangeService.EwsGlobals.AppWideStoreSessionCache = new AppWideStoreSessionCache());
							application["WS_AcceptedDomainCacheKey"] = (HttpExchangeService.EwsGlobals.AcceptedDomainCache = new AcceptedDomainCache());
							application["WS_WorkloadManagerKey"] = (HttpExchangeService.EwsGlobals.UserWorkloadManager = UserWorkloadManager.Singleton);
							HttpExchangeService.EwsGlobals.ADIdentityInformationCache = ADIdentityInformationCache.Singleton;
							HttpExchangeService.EwsGlobals.initialized = true;
						}
					}
				}
			}

			// Token: 0x170014E3 RID: 5347
			// (get) Token: 0x06005BFF RID: 23551 RVA: 0x0011DD70 File Offset: 0x0011BF70
			// (set) Token: 0x06005C00 RID: 23552 RVA: 0x0011DD77 File Offset: 0x0011BF77
			public static AppWideStoreSessionCache AppWideStoreSessionCache { get; private set; }

			// Token: 0x170014E4 RID: 5348
			// (get) Token: 0x06005C01 RID: 23553 RVA: 0x0011DD7F File Offset: 0x0011BF7F
			// (set) Token: 0x06005C02 RID: 23554 RVA: 0x0011DD86 File Offset: 0x0011BF86
			public static AcceptedDomainCache AcceptedDomainCache { get; private set; }

			// Token: 0x170014E5 RID: 5349
			// (get) Token: 0x06005C03 RID: 23555 RVA: 0x0011DD8E File Offset: 0x0011BF8E
			// (set) Token: 0x06005C04 RID: 23556 RVA: 0x0011DD95 File Offset: 0x0011BF95
			public static UserWorkloadManager UserWorkloadManager { get; private set; }

			// Token: 0x170014E6 RID: 5350
			// (get) Token: 0x06005C05 RID: 23557 RVA: 0x0011DD9D File Offset: 0x0011BF9D
			// (set) Token: 0x06005C06 RID: 23558 RVA: 0x0011DDA4 File Offset: 0x0011BFA4
			public static ADIdentityInformationCache ADIdentityInformationCache { get; private set; }

			// Token: 0x0400321A RID: 12826
			private static readonly IntAppSettingsEntry ADIdentityCacheSize = new IntAppSettingsEntry("HttpExchangeService.ADIdentityCacheSize", 4000, null);

			// Token: 0x0400321B RID: 12827
			private static object staticLock = new object();

			// Token: 0x0400321C RID: 12828
			private static bool initialized = false;
		}
	}
}
