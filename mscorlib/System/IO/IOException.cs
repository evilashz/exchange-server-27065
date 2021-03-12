using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x02000197 RID: 407
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class IOException : SystemException
	{
		// Token: 0x060018D1 RID: 6353 RVA: 0x000510AE File Offset: 0x0004F2AE
		[__DynamicallyInvokable]
		public IOException() : base(Environment.GetResourceString("Arg_IOException"))
		{
			base.SetErrorCode(-2146232800);
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x000510CB File Offset: 0x0004F2CB
		[__DynamicallyInvokable]
		public IOException(string message) : base(message)
		{
			base.SetErrorCode(-2146232800);
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x000510DF File Offset: 0x0004F2DF
		[__DynamicallyInvokable]
		public IOException(string message, int hresult) : base(message)
		{
			base.SetErrorCode(hresult);
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x000510EF File Offset: 0x0004F2EF
		internal IOException(string message, int hresult, string maybeFullPath) : base(message)
		{
			base.SetErrorCode(hresult);
			this._maybeFullPath = maybeFullPath;
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x00051106 File Offset: 0x0004F306
		[__DynamicallyInvokable]
		public IOException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2146232800);
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x0005111B File Offset: 0x0004F31B
		protected IOException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x040008B3 RID: 2227
		[NonSerialized]
		private string _maybeFullPath;
	}
}
