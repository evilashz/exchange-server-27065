using System;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.MapiHttp;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200001E RID: 30
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class NspiAsyncOperation : AsyncOperation
	{
		// Token: 0x06000155 RID: 341 RVA: 0x00007C8A File Offset: 0x00005E8A
		protected NspiAsyncOperation(HttpContextBase context, AsyncOperationCookieFlags cookieFlags) : base(context, NspiAsyncOperation.cookieVdirPath, cookieFlags)
		{
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00007C9C File Offset: 0x00005E9C
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00007CCC File Offset: 0x00005ECC
		protected IntPtr NspiContextHandle
		{
			get
			{
				IntPtr? intPtr = (IntPtr?)base.ContextHandle;
				if (intPtr == null)
				{
					return IntPtr.Zero;
				}
				return intPtr.GetValueOrDefault();
			}
			set
			{
				if (value == IntPtr.Zero)
				{
					base.ContextHandle = null;
					return;
				}
				if (this.NspiContextHandle != IntPtr.Zero && value != this.NspiContextHandle)
				{
					throw new InvalidOperationException("Context handle can only be set to zero once set.");
				}
				base.ContextHandle = new IntPtr?(value);
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00007D37 File Offset: 0x00005F37
		public override string GetTraceBeginParameters(MapiHttpRequest mapiHttpRequest)
		{
			base.CheckDisposed();
			return AsyncOperation.CombineTraceParameters(base.GetTraceBeginParameters(mapiHttpRequest), string.Format("contextHandle={0}", this.NspiContextHandle));
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00007D60 File Offset: 0x00005F60
		public override string GetTraceEndParameters(MapiHttpRequest mapiHttpRequest, MapiHttpResponse mapiHttpResponse)
		{
			base.CheckDisposed();
			if (mapiHttpResponse.StatusCode != 0U)
			{
				return AsyncOperation.CombineTraceParameters(base.GetTraceEndParameters(mapiHttpRequest, mapiHttpResponse), string.Format("Failed; statusCode={0}, contextHandle={1}", mapiHttpResponse.StatusCode, this.NspiContextHandle));
			}
			if (mapiHttpResponse is MapiHttpOperationResponse && ((MapiHttpOperationResponse)mapiHttpResponse).ReturnCode != 0U)
			{
				return AsyncOperation.CombineTraceParameters(base.GetTraceEndParameters(mapiHttpRequest, mapiHttpResponse), string.Format("Failed; statusCode={0}. errorCode={1}, contextHandle={2}", mapiHttpResponse.StatusCode, ((MapiHttpOperationResponse)mapiHttpResponse).ReturnCode, this.NspiContextHandle));
			}
			return AsyncOperation.CombineTraceParameters(base.GetTraceEndParameters(mapiHttpRequest, mapiHttpResponse), string.Format("Success; contextHandle={0}", this.NspiContextHandle));
		}

		// Token: 0x040000AB RID: 171
		private static readonly string cookieVdirPath = AsyncOperation.CreateCookieVdirPath(MapiHttpEndpoints.VdirPathNspi);
	}
}
