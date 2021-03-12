using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Data.Storage.RightsManagement
{
	// Token: 0x02000B63 RID: 2915
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RightsManagementAsyncResult : LazyAsyncResult
	{
		// Token: 0x17001CCE RID: 7374
		// (get) Token: 0x06006991 RID: 27025 RVA: 0x001C4E73 File Offset: 0x001C3073
		internal RmsClientManagerContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17001CCF RID: 7375
		// (get) Token: 0x06006992 RID: 27026 RVA: 0x001C4E7B File Offset: 0x001C307B
		internal Breadcrumbs<Constants.State> BreadCrumbs
		{
			get
			{
				return this.breadcrumbs;
			}
		}

		// Token: 0x17001CD0 RID: 7376
		// (get) Token: 0x06006993 RID: 27027 RVA: 0x001C4E83 File Offset: 0x001C3083
		internal RmsClientManager.SaveContextOnAsyncQueryCallback SaveContextCallback
		{
			get
			{
				return this.saveContextCallback;
			}
		}

		// Token: 0x17001CD1 RID: 7377
		// (get) Token: 0x06006994 RID: 27028 RVA: 0x001C4E8B File Offset: 0x001C308B
		internal IRmsLatencyTracker LatencyTracker
		{
			get
			{
				return this.context.LatencyTracker;
			}
		}

		// Token: 0x06006995 RID: 27029 RVA: 0x001C4E98 File Offset: 0x001C3098
		internal RightsManagementAsyncResult(RmsClientManagerContext context, object callerState, AsyncCallback callerCallback) : base(null, callerState, RightsManagementAsyncResult.WrapCallbackWithUnhandledExceptionAndCrash(callerCallback))
		{
			ArgumentValidator.ThrowIfNull("context", context);
			this.context = context;
			this.adSession = RmsClientManager.ADSession;
			this.saveContextCallback = RmsClientManager.SaveContextCallback;
			RmsClientManager.SaveContextCallback = null;
		}

		// Token: 0x06006996 RID: 27030 RVA: 0x001C4ED8 File Offset: 0x001C30D8
		internal void AddBreadCrumb(Constants.State value)
		{
			RightsManagementAsyncResult rightsManagementAsyncResult = base.AsyncState as RightsManagementAsyncResult;
			if (rightsManagementAsyncResult == null || rightsManagementAsyncResult == this)
			{
				if (this.breadcrumbs == null)
				{
					this.breadcrumbs = new Breadcrumbs<Constants.State>(32);
				}
				this.breadcrumbs.Drop(value);
				return;
			}
			rightsManagementAsyncResult.AddBreadCrumb(value);
		}

		// Token: 0x06006997 RID: 27031 RVA: 0x001C4F24 File Offset: 0x001C3124
		internal void InvokeSaveContextCallback()
		{
			RightsManagementAsyncResult rightsManagementAsyncResult = base.AsyncState as RightsManagementAsyncResult;
			if (rightsManagementAsyncResult == null || rightsManagementAsyncResult == this)
			{
				if (this.saveContextCallback != null)
				{
					this.saveContextCallback(base.AsyncState);
				}
				RmsClientManager.ADSession = this.adSession;
				return;
			}
			rightsManagementAsyncResult.InvokeSaveContextCallback();
		}

		// Token: 0x06006998 RID: 27032 RVA: 0x001C500C File Offset: 0x001C320C
		private static AsyncCallback WrapCallbackWithUnhandledExceptionAndCrash(AsyncCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return delegate(IAsyncResult asyncResult)
			{
				RightsManagementAsyncResult asyncResultRM = asyncResult as RightsManagementAsyncResult;
				try
				{
					if (asyncResultRM != null)
					{
						asyncResultRM.InvokeSaveContextCallback();
					}
					callback(asyncResult);
				}
				catch (Exception exception)
				{
					Exception exception2;
					ExWatson.SendReportAndCrashOnAnotherThread(exception2, ReportOptions.None, delegate(Exception exception, int threadId)
					{
						if (asyncResultRM != null)
						{
							asyncResultRM.InvokeSaveContextCallback();
						}
					}, null);
					throw;
				}
			};
		}

		// Token: 0x04003C13 RID: 15379
		private readonly IConfigurationSession adSession;

		// Token: 0x04003C14 RID: 15380
		private readonly RmsClientManager.SaveContextOnAsyncQueryCallback saveContextCallback;

		// Token: 0x04003C15 RID: 15381
		private readonly RmsClientManagerContext context;

		// Token: 0x04003C16 RID: 15382
		private Breadcrumbs<Constants.State> breadcrumbs;
	}
}
