using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data.Directory.GlobalLocatorService
{
	// Token: 0x02000128 RID: 296
	internal class GlsAsyncState
	{
		// Token: 0x06000C6F RID: 3183 RVA: 0x000381F8 File Offset: 0x000363F8
		internal GlsAsyncState(AsyncCallback clientCallback, object clientAsyncState, LocatorService serviceProxy) : this(clientCallback, clientAsyncState, serviceProxy, 0, null, null, null, null, null, true, null)
		{
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00038218 File Offset: 0x00036418
		internal GlsAsyncState(AsyncCallback clientCallback, object clientAsyncState, LocatorService serviceProxy, int retryCount, GlsLoggerContext loggerContext, IExtensibleDataObject request, string methodName, string glsApiName, object parameterValue, bool isRead, Func<LocatorService, GlsLoggerContext, IAsyncResult> methodToRetry)
		{
			this.clientCallback = clientCallback;
			this.clientAsyncState = clientAsyncState;
			this.serviceProxy = serviceProxy;
			this.retryCount = retryCount;
			this.loggerContext = loggerContext;
			this.request = request;
			this.methodName = methodName;
			this.glsApiName = glsApiName;
			this.parameterValue = parameterValue;
			this.isRead = isRead;
			this.methodToRetry = methodToRetry;
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x00038280 File Offset: 0x00036480
		internal AsyncCallback ClientCallback
		{
			get
			{
				return this.clientCallback;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x00038288 File Offset: 0x00036488
		internal object ClientAsyncState
		{
			get
			{
				return this.clientAsyncState;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x00038290 File Offset: 0x00036490
		internal LocatorService ServiceProxy
		{
			get
			{
				return this.serviceProxy;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x00038298 File Offset: 0x00036498
		internal GlsLoggerContext LoggerContext
		{
			get
			{
				return this.loggerContext;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x000382A0 File Offset: 0x000364A0
		internal int RetryCount
		{
			get
			{
				return this.retryCount;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x000382A8 File Offset: 0x000364A8
		// (set) Token: 0x06000C77 RID: 3191 RVA: 0x000382B0 File Offset: 0x000364B0
		internal Exception AsyncExeption
		{
			get
			{
				return this.asyncException;
			}
			set
			{
				this.asyncException = value;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x000382B9 File Offset: 0x000364B9
		internal IExtensibleDataObject Request
		{
			get
			{
				return this.request;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x000382C1 File Offset: 0x000364C1
		internal Func<LocatorService, GlsLoggerContext, IAsyncResult> MethodToRetry
		{
			get
			{
				return this.methodToRetry;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x000382C9 File Offset: 0x000364C9
		internal string MethodName
		{
			get
			{
				return this.methodName;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x000382D1 File Offset: 0x000364D1
		internal string GlsApiName
		{
			get
			{
				return this.glsApiName;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x000382D9 File Offset: 0x000364D9
		internal object ParameterValue
		{
			get
			{
				return this.parameterValue;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x000382E1 File Offset: 0x000364E1
		internal bool IsRead
		{
			get
			{
				return this.isRead;
			}
		}

		// Token: 0x04000651 RID: 1617
		private readonly AsyncCallback clientCallback;

		// Token: 0x04000652 RID: 1618
		private readonly object clientAsyncState;

		// Token: 0x04000653 RID: 1619
		private readonly int retryCount;

		// Token: 0x04000654 RID: 1620
		private readonly Func<LocatorService, GlsLoggerContext, IAsyncResult> methodToRetry;

		// Token: 0x04000655 RID: 1621
		private readonly IExtensibleDataObject request;

		// Token: 0x04000656 RID: 1622
		private readonly LocatorService serviceProxy;

		// Token: 0x04000657 RID: 1623
		private readonly GlsLoggerContext loggerContext;

		// Token: 0x04000658 RID: 1624
		private readonly string methodName;

		// Token: 0x04000659 RID: 1625
		private readonly string glsApiName;

		// Token: 0x0400065A RID: 1626
		private readonly object parameterValue;

		// Token: 0x0400065B RID: 1627
		private readonly bool isRead;

		// Token: 0x0400065C RID: 1628
		private Exception asyncException;
	}
}
