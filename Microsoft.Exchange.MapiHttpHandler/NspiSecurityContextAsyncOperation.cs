using System;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200001F RID: 31
	internal abstract class NspiSecurityContextAsyncOperation : NspiAsyncOperation
	{
		// Token: 0x0600015B RID: 347 RVA: 0x00007E2F File Offset: 0x0000602F
		protected NspiSecurityContextAsyncOperation(HttpContextBase context, AsyncOperationCookieFlags cookieFlags) : base(context, cookieFlags)
		{
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00007E44 File Offset: 0x00006044
		protected MapiHttpClientBinding ClientBinding
		{
			get
			{
				return this.clientBinding;
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00007E4C File Offset: 0x0000604C
		public override void Prepare()
		{
			base.Prepare();
			this.initialCachedClientSecurityContext = base.GetInitialCachedClientSecurityContext();
			this.clientBinding = base.GetMapiHttpClientBinding(new Func<ClientSecurityContext>(this.GetClientSecurityContext));
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00007E78 File Offset: 0x00006078
		protected ClientSecurityContext GetClientSecurityContext()
		{
			ClientSecurityContext result = null;
			if (this.initialCachedClientSecurityContext != null)
			{
				lock (this.securityContextLock)
				{
					if (this.initialCachedClientSecurityContext != null)
					{
						result = this.initialCachedClientSecurityContext;
						this.initialCachedClientSecurityContext = null;
					}
				}
			}
			return result;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00007ED4 File Offset: 0x000060D4
		protected override void InternalDispose()
		{
			if (this.clientBinding != null)
			{
				this.clientBinding.ClearClientSecurityContextGetter();
			}
			if (this.initialCachedClientSecurityContext != null)
			{
				lock (this.securityContextLock)
				{
					if (this.initialCachedClientSecurityContext != null)
					{
						this.initialCachedClientSecurityContext.Dispose();
						this.initialCachedClientSecurityContext = null;
					}
				}
			}
			base.InternalDispose();
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00007F4C File Offset: 0x0000614C
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiSecurityContextAsyncOperation>(this);
		}

		// Token: 0x040000AC RID: 172
		private ClientSecurityContext initialCachedClientSecurityContext;

		// Token: 0x040000AD RID: 173
		private object securityContextLock = new object();

		// Token: 0x040000AE RID: 174
		private MapiHttpClientBinding clientBinding;
	}
}
