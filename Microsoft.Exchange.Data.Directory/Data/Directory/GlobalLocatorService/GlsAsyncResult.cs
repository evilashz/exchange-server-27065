using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Directory.GlobalLocatorService
{
	// Token: 0x02000126 RID: 294
	internal class GlsAsyncResult : AsyncResult
	{
		// Token: 0x06000C5D RID: 3165 RVA: 0x000380BC File Offset: 0x000362BC
		internal GlsAsyncResult(AsyncCallback callback, object asyncState, LocatorService serviceProxy, IAsyncResult internalAsyncResult) : base(callback, asyncState)
		{
			if (serviceProxy != null)
			{
				this.pooledProxy = new GlsAsyncResult.GlsServiceProxy(serviceProxy);
			}
			this.internalAsyncResult = internalAsyncResult;
			this.creationTime = DateTime.UtcNow;
			this.isOfflineGls = false;
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x000380F0 File Offset: 0x000362F0
		internal void CheckExceptionAndEnd()
		{
			try
			{
				if (this.asyncException != null)
				{
					throw AsyncExceptionWrapperHelper.GetAsyncWrapper(this.asyncException);
				}
			}
			finally
			{
				base.End();
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x0003812C File Offset: 0x0003632C
		internal LocatorService ServiceProxy
		{
			get
			{
				if (this.pooledProxy == null)
				{
					return null;
				}
				return this.pooledProxy.Client;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x00038143 File Offset: 0x00036343
		// (set) Token: 0x06000C61 RID: 3169 RVA: 0x0003814B File Offset: 0x0003634B
		internal IPooledServiceProxy<LocatorService> PooledProxy
		{
			get
			{
				return this.pooledProxy;
			}
			set
			{
				this.pooledProxy = value;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x00038154 File Offset: 0x00036354
		internal DateTime CreationTime
		{
			get
			{
				return this.creationTime;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x0003815C File Offset: 0x0003635C
		// (set) Token: 0x06000C64 RID: 3172 RVA: 0x00038164 File Offset: 0x00036364
		internal IAsyncResult InternalAsyncResult
		{
			get
			{
				return this.internalAsyncResult;
			}
			set
			{
				if (this.internalAsyncResult != null && this.internalAsyncResult != value)
				{
					throw new ArgumentException("InternalAsyncResult");
				}
				this.internalAsyncResult = value;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x00038189 File Offset: 0x00036389
		// (set) Token: 0x06000C66 RID: 3174 RVA: 0x00038191 File Offset: 0x00036391
		internal GlsLoggerContext LoggerContext
		{
			get
			{
				return this.loggerContext;
			}
			set
			{
				this.loggerContext = value;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x0003819A File Offset: 0x0003639A
		// (set) Token: 0x06000C68 RID: 3176 RVA: 0x000381A2 File Offset: 0x000363A2
		internal Exception AsyncException
		{
			get
			{
				return this.asyncException;
			}
			set
			{
				if (this.asyncException != null)
				{
					throw new ArgumentException("AsyncException");
				}
				this.asyncException = value;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x000381BE File Offset: 0x000363BE
		// (set) Token: 0x06000C6A RID: 3178 RVA: 0x000381C6 File Offset: 0x000363C6
		internal bool IsOfflineGls
		{
			get
			{
				return this.isOfflineGls;
			}
			set
			{
				this.isOfflineGls = value;
			}
		}

		// Token: 0x04000649 RID: 1609
		private readonly DateTime creationTime;

		// Token: 0x0400064A RID: 1610
		private IPooledServiceProxy<LocatorService> pooledProxy;

		// Token: 0x0400064B RID: 1611
		private IAsyncResult internalAsyncResult;

		// Token: 0x0400064C RID: 1612
		private GlsLoggerContext loggerContext;

		// Token: 0x0400064D RID: 1613
		private Exception asyncException;

		// Token: 0x0400064E RID: 1614
		private bool isOfflineGls;

		// Token: 0x02000127 RID: 295
		private class GlsServiceProxy : IPooledServiceProxy<LocatorService>
		{
			// Token: 0x06000C6B RID: 3179 RVA: 0x000381CF File Offset: 0x000363CF
			public GlsServiceProxy(LocatorService client)
			{
				this.client = client;
			}

			// Token: 0x1700023F RID: 575
			// (get) Token: 0x06000C6C RID: 3180 RVA: 0x000381DE File Offset: 0x000363DE
			public LocatorService Client
			{
				get
				{
					return this.client;
				}
			}

			// Token: 0x17000240 RID: 576
			// (get) Token: 0x06000C6D RID: 3181 RVA: 0x000381E6 File Offset: 0x000363E6
			// (set) Token: 0x06000C6E RID: 3182 RVA: 0x000381EE File Offset: 0x000363EE
			public string Tag { get; set; }

			// Token: 0x0400064F RID: 1615
			private LocatorService client;
		}
	}
}
