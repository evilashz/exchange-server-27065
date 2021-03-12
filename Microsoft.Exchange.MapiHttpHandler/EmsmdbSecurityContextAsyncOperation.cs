using System;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000009 RID: 9
	internal abstract class EmsmdbSecurityContextAsyncOperation : EmsmdbAsyncOperation
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00004028 File Offset: 0x00002228
		protected EmsmdbSecurityContextAsyncOperation(HttpContextBase context, AsyncOperationCookieFlags cookieFlags) : base(context, cookieFlags)
		{
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600006E RID: 110 RVA: 0x0000403D File Offset: 0x0000223D
		protected MapiHttpClientBinding ClientBinding
		{
			get
			{
				return this.clientBinding;
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004045 File Offset: 0x00002245
		public override void Prepare()
		{
			base.Prepare();
			this.initialCachedClientSecurityContext = base.GetInitialCachedClientSecurityContext();
			this.clientBinding = base.GetMapiHttpClientBinding(new Func<ClientSecurityContext>(this.GetClientSecurityContext));
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004074 File Offset: 0x00002274
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

		// Token: 0x06000071 RID: 113 RVA: 0x000040D0 File Offset: 0x000022D0
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

		// Token: 0x06000072 RID: 114 RVA: 0x00004148 File Offset: 0x00002348
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<EmsmdbSecurityContextAsyncOperation>(this);
		}

		// Token: 0x0400005F RID: 95
		private ClientSecurityContext initialCachedClientSecurityContext;

		// Token: 0x04000060 RID: 96
		private object securityContextLock = new object();

		// Token: 0x04000061 RID: 97
		private MapiHttpClientBinding clientBinding;
	}
}
