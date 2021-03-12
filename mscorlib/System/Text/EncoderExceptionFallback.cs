using System;

namespace System.Text
{
	// Token: 0x02000A3F RID: 2623
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class EncoderExceptionFallback : EncoderFallback
	{
		// Token: 0x060066CE RID: 26318 RVA: 0x0015A753 File Offset: 0x00158953
		[__DynamicallyInvokable]
		public EncoderExceptionFallback()
		{
		}

		// Token: 0x060066CF RID: 26319 RVA: 0x0015A75B File Offset: 0x0015895B
		[__DynamicallyInvokable]
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			return new EncoderExceptionFallbackBuffer();
		}

		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x060066D0 RID: 26320 RVA: 0x0015A762 File Offset: 0x00158962
		[__DynamicallyInvokable]
		public override int MaxCharCount
		{
			[__DynamicallyInvokable]
			get
			{
				return 0;
			}
		}

		// Token: 0x060066D1 RID: 26321 RVA: 0x0015A768 File Offset: 0x00158968
		[__DynamicallyInvokable]
		public override bool Equals(object value)
		{
			return value is EncoderExceptionFallback;
		}

		// Token: 0x060066D2 RID: 26322 RVA: 0x0015A782 File Offset: 0x00158982
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return 654;
		}
	}
}
