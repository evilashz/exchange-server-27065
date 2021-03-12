using System;
using System.Web;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.ExchangeService
{
	// Token: 0x02000DD8 RID: 3544
	internal class ExchangeServiceFactory
	{
		// Token: 0x170014D8 RID: 5336
		// (get) Token: 0x06005AA6 RID: 23206 RVA: 0x0011A80B File Offset: 0x00118A0B
		public static ExchangeServiceFactory Default
		{
			get
			{
				return ExchangeServiceFactory.defaultInstance;
			}
		}

		// Token: 0x06005AA7 RID: 23207 RVA: 0x0011A812 File Offset: 0x00118A12
		public virtual IExchangeService CreateForStandaloneHttpService(HttpContext httpContext, IActivityScope activityScope, IStandardBudget budget)
		{
			ArgumentValidator.ThrowIfNull("httpContext", httpContext);
			ArgumentValidator.ThrowIfNull("activityScope", activityScope);
			ArgumentValidator.ThrowIfNull("budget", budget);
			return new HttpExchangeService(httpContext, activityScope, budget);
		}

		// Token: 0x06005AA8 RID: 23208 RVA: 0x0011A83D File Offset: 0x00118A3D
		public virtual IExchangeService CreateForEws(CallContext callContext)
		{
			ArgumentValidator.ThrowIfNull("callContext", callContext);
			return new EwsExchangeService(callContext);
		}

		// Token: 0x06005AA9 RID: 23209 RVA: 0x0011A850 File Offset: 0x00118A50
		public virtual IPushNotificationService CreateOutlookPushNotificationService()
		{
			return new OutlookPushNotificationService();
		}

		// Token: 0x04003208 RID: 12808
		private static readonly ExchangeServiceFactory defaultInstance = new ExchangeServiceFactory();
	}
}
