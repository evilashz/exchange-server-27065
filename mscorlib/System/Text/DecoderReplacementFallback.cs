using System;

namespace System.Text
{
	// Token: 0x02000A39 RID: 2617
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class DecoderReplacementFallback : DecoderFallback
	{
		// Token: 0x06006696 RID: 26262 RVA: 0x001598BA File Offset: 0x00157ABA
		[__DynamicallyInvokable]
		public DecoderReplacementFallback() : this("?")
		{
		}

		// Token: 0x06006697 RID: 26263 RVA: 0x001598C8 File Offset: 0x00157AC8
		[__DynamicallyInvokable]
		public DecoderReplacementFallback(string replacement)
		{
			if (replacement == null)
			{
				throw new ArgumentNullException("replacement");
			}
			bool flag = false;
			for (int i = 0; i < replacement.Length; i++)
			{
				if (char.IsSurrogate(replacement, i))
				{
					if (char.IsHighSurrogate(replacement, i))
					{
						if (flag)
						{
							break;
						}
						flag = true;
					}
					else
					{
						if (!flag)
						{
							flag = true;
							break;
						}
						flag = false;
					}
				}
				else if (flag)
				{
					break;
				}
			}
			if (flag)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex", new object[]
				{
					"replacement"
				}));
			}
			this.strDefault = replacement;
		}

		// Token: 0x17001194 RID: 4500
		// (get) Token: 0x06006698 RID: 26264 RVA: 0x0015994B File Offset: 0x00157B4B
		[__DynamicallyInvokable]
		public string DefaultString
		{
			[__DynamicallyInvokable]
			get
			{
				return this.strDefault;
			}
		}

		// Token: 0x06006699 RID: 26265 RVA: 0x00159953 File Offset: 0x00157B53
		[__DynamicallyInvokable]
		public override DecoderFallbackBuffer CreateFallbackBuffer()
		{
			return new DecoderReplacementFallbackBuffer(this);
		}

		// Token: 0x17001195 RID: 4501
		// (get) Token: 0x0600669A RID: 26266 RVA: 0x0015995B File Offset: 0x00157B5B
		[__DynamicallyInvokable]
		public override int MaxCharCount
		{
			[__DynamicallyInvokable]
			get
			{
				return this.strDefault.Length;
			}
		}

		// Token: 0x0600669B RID: 26267 RVA: 0x00159968 File Offset: 0x00157B68
		[__DynamicallyInvokable]
		public override bool Equals(object value)
		{
			DecoderReplacementFallback decoderReplacementFallback = value as DecoderReplacementFallback;
			return decoderReplacementFallback != null && this.strDefault == decoderReplacementFallback.strDefault;
		}

		// Token: 0x0600669C RID: 26268 RVA: 0x00159992 File Offset: 0x00157B92
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.strDefault.GetHashCode();
		}

		// Token: 0x04002D95 RID: 11669
		private string strDefault;
	}
}
