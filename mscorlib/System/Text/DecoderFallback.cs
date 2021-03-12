using System;
using System.Threading;

namespace System.Text
{
	// Token: 0x02000A37 RID: 2615
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class DecoderFallback
	{
		// Token: 0x1700118E RID: 4494
		// (get) Token: 0x06006684 RID: 26244 RVA: 0x001595C8 File Offset: 0x001577C8
		private static object InternalSyncObject
		{
			get
			{
				if (DecoderFallback.s_InternalSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange<object>(ref DecoderFallback.s_InternalSyncObject, value, null);
				}
				return DecoderFallback.s_InternalSyncObject;
			}
		}

		// Token: 0x1700118F RID: 4495
		// (get) Token: 0x06006685 RID: 26245 RVA: 0x001595F4 File Offset: 0x001577F4
		[__DynamicallyInvokable]
		public static DecoderFallback ReplacementFallback
		{
			[__DynamicallyInvokable]
			get
			{
				if (DecoderFallback.replacementFallback == null)
				{
					object internalSyncObject = DecoderFallback.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (DecoderFallback.replacementFallback == null)
						{
							DecoderFallback.replacementFallback = new DecoderReplacementFallback();
						}
					}
				}
				return DecoderFallback.replacementFallback;
			}
		}

		// Token: 0x17001190 RID: 4496
		// (get) Token: 0x06006686 RID: 26246 RVA: 0x00159654 File Offset: 0x00157854
		[__DynamicallyInvokable]
		public static DecoderFallback ExceptionFallback
		{
			[__DynamicallyInvokable]
			get
			{
				if (DecoderFallback.exceptionFallback == null)
				{
					object internalSyncObject = DecoderFallback.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (DecoderFallback.exceptionFallback == null)
						{
							DecoderFallback.exceptionFallback = new DecoderExceptionFallback();
						}
					}
				}
				return DecoderFallback.exceptionFallback;
			}
		}

		// Token: 0x06006687 RID: 26247
		[__DynamicallyInvokable]
		public abstract DecoderFallbackBuffer CreateFallbackBuffer();

		// Token: 0x17001191 RID: 4497
		// (get) Token: 0x06006688 RID: 26248
		[__DynamicallyInvokable]
		public abstract int MaxCharCount { [__DynamicallyInvokable] get; }

		// Token: 0x17001192 RID: 4498
		// (get) Token: 0x06006689 RID: 26249 RVA: 0x001596B4 File Offset: 0x001578B4
		internal bool IsMicrosoftBestFitFallback
		{
			get
			{
				return this.bIsMicrosoftBestFitFallback;
			}
		}

		// Token: 0x0600668A RID: 26250 RVA: 0x001596BC File Offset: 0x001578BC
		[__DynamicallyInvokable]
		protected DecoderFallback()
		{
		}

		// Token: 0x04002D8F RID: 11663
		internal bool bIsMicrosoftBestFitFallback;

		// Token: 0x04002D90 RID: 11664
		private static volatile DecoderFallback replacementFallback;

		// Token: 0x04002D91 RID: 11665
		private static volatile DecoderFallback exceptionFallback;

		// Token: 0x04002D92 RID: 11666
		private static object s_InternalSyncObject;
	}
}
