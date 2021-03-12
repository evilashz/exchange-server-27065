using System;
using System.Runtime.Serialization;

namespace System.Text
{
	// Token: 0x02000A36 RID: 2614
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class DecoderFallbackException : ArgumentException
	{
		// Token: 0x0600667D RID: 26237 RVA: 0x00159551 File Offset: 0x00157751
		[__DynamicallyInvokable]
		public DecoderFallbackException() : base(Environment.GetResourceString("Arg_ArgumentException"))
		{
			base.SetErrorCode(-2147024809);
		}

		// Token: 0x0600667E RID: 26238 RVA: 0x0015956E File Offset: 0x0015776E
		[__DynamicallyInvokable]
		public DecoderFallbackException(string message) : base(message)
		{
			base.SetErrorCode(-2147024809);
		}

		// Token: 0x0600667F RID: 26239 RVA: 0x00159582 File Offset: 0x00157782
		[__DynamicallyInvokable]
		public DecoderFallbackException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2147024809);
		}

		// Token: 0x06006680 RID: 26240 RVA: 0x00159597 File Offset: 0x00157797
		internal DecoderFallbackException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06006681 RID: 26241 RVA: 0x001595A1 File Offset: 0x001577A1
		[__DynamicallyInvokable]
		public DecoderFallbackException(string message, byte[] bytesUnknown, int index) : base(message)
		{
			this.bytesUnknown = bytesUnknown;
			this.index = index;
		}

		// Token: 0x1700118C RID: 4492
		// (get) Token: 0x06006682 RID: 26242 RVA: 0x001595B8 File Offset: 0x001577B8
		[__DynamicallyInvokable]
		public byte[] BytesUnknown
		{
			[__DynamicallyInvokable]
			get
			{
				return this.bytesUnknown;
			}
		}

		// Token: 0x1700118D RID: 4493
		// (get) Token: 0x06006683 RID: 26243 RVA: 0x001595C0 File Offset: 0x001577C0
		[__DynamicallyInvokable]
		public int Index
		{
			[__DynamicallyInvokable]
			get
			{
				return this.index;
			}
		}

		// Token: 0x04002D8D RID: 11661
		private byte[] bytesUnknown;

		// Token: 0x04002D8E RID: 11662
		private int index;
	}
}
