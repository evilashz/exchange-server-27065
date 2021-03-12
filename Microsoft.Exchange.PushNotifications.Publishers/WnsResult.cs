using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000F4 RID: 244
	internal class WnsResult<T> where T : class
	{
		// Token: 0x060007D9 RID: 2009 RVA: 0x0001832C File Offset: 0x0001652C
		public WnsResult(T response, Exception exception = null)
		{
			ArgumentValidator.ThrowIfNull("response", response);
			this.Response = response;
			this.Exception = exception;
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00018352 File Offset: 0x00016552
		public WnsResult(Exception exception)
		{
			ArgumentValidator.ThrowIfNull("exception", exception);
			this.Exception = exception;
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x0001836C File Offset: 0x0001656C
		// (set) Token: 0x060007DC RID: 2012 RVA: 0x00018374 File Offset: 0x00016574
		public T Response { get; private set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x0001837D File Offset: 0x0001657D
		// (set) Token: 0x060007DE RID: 2014 RVA: 0x00018385 File Offset: 0x00016585
		public Exception Exception { get; private set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x0001838E File Offset: 0x0001658E
		public bool IsTimeout
		{
			get
			{
				return this.Exception is DownloadTimeoutException;
			}
		}
	}
}
