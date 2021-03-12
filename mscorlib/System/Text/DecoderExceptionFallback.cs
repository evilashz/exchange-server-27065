using System;

namespace System.Text
{
	// Token: 0x02000A34 RID: 2612
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class DecoderExceptionFallback : DecoderFallback
	{
		// Token: 0x06006672 RID: 26226 RVA: 0x00159467 File Offset: 0x00157667
		[__DynamicallyInvokable]
		public DecoderExceptionFallback()
		{
		}

		// Token: 0x06006673 RID: 26227 RVA: 0x0015946F File Offset: 0x0015766F
		[__DynamicallyInvokable]
		public override DecoderFallbackBuffer CreateFallbackBuffer()
		{
			return new DecoderExceptionFallbackBuffer();
		}

		// Token: 0x1700118A RID: 4490
		// (get) Token: 0x06006674 RID: 26228 RVA: 0x00159476 File Offset: 0x00157676
		[__DynamicallyInvokable]
		public override int MaxCharCount
		{
			[__DynamicallyInvokable]
			get
			{
				return 0;
			}
		}

		// Token: 0x06006675 RID: 26229 RVA: 0x0015947C File Offset: 0x0015767C
		[__DynamicallyInvokable]
		public override bool Equals(object value)
		{
			return value is DecoderExceptionFallback;
		}

		// Token: 0x06006676 RID: 26230 RVA: 0x00159496 File Offset: 0x00157696
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return 879;
		}
	}
}
