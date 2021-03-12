using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;

namespace System
{
	// Token: 0x0200011D RID: 285
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class OperationCanceledException : SystemException
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x00032BC6 File Offset: 0x00030DC6
		// (set) Token: 0x060010CE RID: 4302 RVA: 0x00032BCE File Offset: 0x00030DCE
		[__DynamicallyInvokable]
		public CancellationToken CancellationToken
		{
			[__DynamicallyInvokable]
			get
			{
				return this._cancellationToken;
			}
			private set
			{
				this._cancellationToken = value;
			}
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x00032BD7 File Offset: 0x00030DD7
		[__DynamicallyInvokable]
		public OperationCanceledException() : base(Environment.GetResourceString("OperationCanceled"))
		{
			base.SetErrorCode(-2146233029);
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x00032BF4 File Offset: 0x00030DF4
		[__DynamicallyInvokable]
		public OperationCanceledException(string message) : base(message)
		{
			base.SetErrorCode(-2146233029);
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x00032C08 File Offset: 0x00030E08
		[__DynamicallyInvokable]
		public OperationCanceledException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2146233029);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x00032C1D File Offset: 0x00030E1D
		[__DynamicallyInvokable]
		public OperationCanceledException(CancellationToken token) : this()
		{
			this.CancellationToken = token;
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x00032C2C File Offset: 0x00030E2C
		[__DynamicallyInvokable]
		public OperationCanceledException(string message, CancellationToken token) : this(message)
		{
			this.CancellationToken = token;
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x00032C3C File Offset: 0x00030E3C
		[__DynamicallyInvokable]
		public OperationCanceledException(string message, Exception innerException, CancellationToken token) : this(message, innerException)
		{
			this.CancellationToken = token;
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00032C4D File Offset: 0x00030E4D
		protected OperationCanceledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x040005D3 RID: 1491
		[NonSerialized]
		private CancellationToken _cancellationToken;
	}
}
