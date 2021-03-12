using System;

namespace System.Text
{
	// Token: 0x02000A44 RID: 2628
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class EncoderReplacementFallback : EncoderFallback
	{
		// Token: 0x060066F6 RID: 26358 RVA: 0x0015AC5F File Offset: 0x00158E5F
		[__DynamicallyInvokable]
		public EncoderReplacementFallback() : this("?")
		{
		}

		// Token: 0x060066F7 RID: 26359 RVA: 0x0015AC6C File Offset: 0x00158E6C
		[__DynamicallyInvokable]
		public EncoderReplacementFallback(string replacement)
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

		// Token: 0x170011AB RID: 4523
		// (get) Token: 0x060066F8 RID: 26360 RVA: 0x0015ACEF File Offset: 0x00158EEF
		[__DynamicallyInvokable]
		public string DefaultString
		{
			[__DynamicallyInvokable]
			get
			{
				return this.strDefault;
			}
		}

		// Token: 0x060066F9 RID: 26361 RVA: 0x0015ACF7 File Offset: 0x00158EF7
		[__DynamicallyInvokable]
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			return new EncoderReplacementFallbackBuffer(this);
		}

		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x060066FA RID: 26362 RVA: 0x0015ACFF File Offset: 0x00158EFF
		[__DynamicallyInvokable]
		public override int MaxCharCount
		{
			[__DynamicallyInvokable]
			get
			{
				return this.strDefault.Length;
			}
		}

		// Token: 0x060066FB RID: 26363 RVA: 0x0015AD0C File Offset: 0x00158F0C
		[__DynamicallyInvokable]
		public override bool Equals(object value)
		{
			EncoderReplacementFallback encoderReplacementFallback = value as EncoderReplacementFallback;
			return encoderReplacementFallback != null && this.strDefault == encoderReplacementFallback.strDefault;
		}

		// Token: 0x060066FC RID: 26364 RVA: 0x0015AD36 File Offset: 0x00158F36
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.strDefault.GetHashCode();
		}

		// Token: 0x04002DB7 RID: 11703
		private string strDefault;
	}
}
