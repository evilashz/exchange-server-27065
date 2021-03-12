using System;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000034 RID: 52
	internal abstract class RfriSecurityContextAsyncOperation : RfriAsyncOperation
	{
		// Token: 0x060001F2 RID: 498 RVA: 0x0000BB54 File Offset: 0x00009D54
		protected RfriSecurityContextAsyncOperation(HttpContextBase context, AsyncOperationCookieFlags cookieFlags) : base(context, cookieFlags)
		{
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000BB69 File Offset: 0x00009D69
		protected MapiHttpClientBinding ClientBinding
		{
			get
			{
				return this.clientBinding;
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000BB71 File Offset: 0x00009D71
		public override void Prepare()
		{
			base.Prepare();
			this.initialCachedClientSecurityContext = base.GetInitialCachedClientSecurityContext();
			this.clientBinding = base.GetMapiHttpClientBinding(new Func<ClientSecurityContext>(this.GetClientSecurityContext));
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000BBA0 File Offset: 0x00009DA0
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

		// Token: 0x060001F6 RID: 502 RVA: 0x0000BBFC File Offset: 0x00009DFC
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

		// Token: 0x060001F7 RID: 503 RVA: 0x0000BC74 File Offset: 0x00009E74
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<RfriSecurityContextAsyncOperation>(this);
		}

		// Token: 0x040000D4 RID: 212
		private ClientSecurityContext initialCachedClientSecurityContext;

		// Token: 0x040000D5 RID: 213
		private object securityContextLock = new object();

		// Token: 0x040000D6 RID: 214
		private MapiHttpClientBinding clientBinding;
	}
}
