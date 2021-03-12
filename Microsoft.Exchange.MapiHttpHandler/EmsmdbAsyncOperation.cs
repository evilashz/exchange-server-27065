using System;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.MapiHttp;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000008 RID: 8
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class EmsmdbAsyncOperation : AsyncOperation
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00003E6B File Offset: 0x0000206B
		protected EmsmdbAsyncOperation(HttpContextBase context, AsyncOperationCookieFlags cookieFlags) : base(context, EmsmdbAsyncOperation.cookieVdirPath, cookieFlags)
		{
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003E7C File Offset: 0x0000207C
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00003EAC File Offset: 0x000020AC
		protected IntPtr EmsmdbContextHandle
		{
			get
			{
				IntPtr? intPtr = (IntPtr?)base.ContextHandle;
				if (intPtr == null)
				{
					return IntPtr.Zero;
				}
				return intPtr.Value;
			}
			set
			{
				if (value == IntPtr.Zero)
				{
					base.ContextHandle = null;
					return;
				}
				if (this.EmsmdbContextHandle != IntPtr.Zero && value != this.EmsmdbContextHandle)
				{
					throw new InvalidOperationException("Context handle can only be set to zero once set.");
				}
				base.ContextHandle = new IntPtr?(value);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003F17 File Offset: 0x00002117
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00003F1F File Offset: 0x0000211F
		protected uint StatusCode { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003F28 File Offset: 0x00002128
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00003F30 File Offset: 0x00002130
		protected int ErrorCode { get; set; }

		// Token: 0x0600006A RID: 106 RVA: 0x00003F39 File Offset: 0x00002139
		public override string GetTraceBeginParameters(MapiHttpRequest mapiHttpRequest)
		{
			base.CheckDisposed();
			return AsyncOperation.CombineTraceParameters(base.GetTraceBeginParameters(mapiHttpRequest), string.Format("contextHandle={0}", this.EmsmdbContextHandle));
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003F64 File Offset: 0x00002164
		public override string GetTraceEndParameters(MapiHttpRequest mapiHttpRequest, MapiHttpResponse mapiHttpResponse)
		{
			base.CheckDisposed();
			if (mapiHttpResponse.StatusCode != 0U)
			{
				return AsyncOperation.CombineTraceParameters(base.GetTraceEndParameters(mapiHttpRequest, mapiHttpResponse), string.Format("Failed; statusCode={0}, contextHandle={1}", mapiHttpResponse.StatusCode, this.EmsmdbContextHandle));
			}
			if (mapiHttpResponse is MapiHttpOperationResponse && ((MapiHttpOperationResponse)mapiHttpResponse).ReturnCode != 0U)
			{
				return AsyncOperation.CombineTraceParameters(base.GetTraceEndParameters(mapiHttpRequest, mapiHttpResponse), string.Format("Failed; errorCode={0}, contextHandle={1}", ((MapiHttpOperationResponse)mapiHttpResponse).ReturnCode, this.EmsmdbContextHandle));
			}
			return AsyncOperation.CombineTraceParameters(base.GetTraceEndParameters(mapiHttpRequest, mapiHttpResponse), string.Format("Success; contextHandle={0}", this.EmsmdbContextHandle));
		}

		// Token: 0x0400005C RID: 92
		private static readonly string cookieVdirPath = AsyncOperation.CreateCookieVdirPath(MapiHttpEndpoints.VdirPathEmsmdb);
	}
}
