using System;
using System.Security;

namespace System.Text
{
	// Token: 0x02000A43 RID: 2627
	[__DynamicallyInvokable]
	public abstract class EncoderFallbackBuffer
	{
		// Token: 0x060066EA RID: 26346
		[__DynamicallyInvokable]
		public abstract bool Fallback(char charUnknown, int index);

		// Token: 0x060066EB RID: 26347
		[__DynamicallyInvokable]
		public abstract bool Fallback(char charUnknownHigh, char charUnknownLow, int index);

		// Token: 0x060066EC RID: 26348
		[__DynamicallyInvokable]
		public abstract char GetNextChar();

		// Token: 0x060066ED RID: 26349
		[__DynamicallyInvokable]
		public abstract bool MovePrevious();

		// Token: 0x170011AA RID: 4522
		// (get) Token: 0x060066EE RID: 26350
		[__DynamicallyInvokable]
		public abstract int Remaining { [__DynamicallyInvokable] get; }

		// Token: 0x060066EF RID: 26351 RVA: 0x0015AAAC File Offset: 0x00158CAC
		[__DynamicallyInvokable]
		public virtual void Reset()
		{
			while (this.GetNextChar() != '\0')
			{
			}
		}

		// Token: 0x060066F0 RID: 26352 RVA: 0x0015AAB6 File Offset: 0x00158CB6
		[SecurityCritical]
		internal void InternalReset()
		{
			this.charStart = null;
			this.bFallingBack = false;
			this.iRecursionCount = 0;
			this.Reset();
		}

		// Token: 0x060066F1 RID: 26353 RVA: 0x0015AAD4 File Offset: 0x00158CD4
		[SecurityCritical]
		internal unsafe void InternalInitialize(char* charStart, char* charEnd, EncoderNLS encoder, bool setEncoder)
		{
			this.charStart = charStart;
			this.charEnd = charEnd;
			this.encoder = encoder;
			this.setEncoder = setEncoder;
			this.bUsedEncoder = false;
			this.bFallingBack = false;
			this.iRecursionCount = 0;
		}

		// Token: 0x060066F2 RID: 26354 RVA: 0x0015AB08 File Offset: 0x00158D08
		internal char InternalGetNextChar()
		{
			char nextChar = this.GetNextChar();
			this.bFallingBack = (nextChar > '\0');
			if (nextChar == '\0')
			{
				this.iRecursionCount = 0;
			}
			return nextChar;
		}

		// Token: 0x060066F3 RID: 26355 RVA: 0x0015AB34 File Offset: 0x00158D34
		[SecurityCritical]
		internal unsafe virtual bool InternalFallback(char ch, ref char* chars)
		{
			int index = (chars - this.charStart) / 2 - 1;
			if (char.IsHighSurrogate(ch))
			{
				if (chars >= this.charEnd)
				{
					if (this.encoder != null && !this.encoder.MustFlush)
					{
						if (this.setEncoder)
						{
							this.bUsedEncoder = true;
							this.encoder.charLeftOver = ch;
						}
						this.bFallingBack = false;
						return false;
					}
				}
				else
				{
					char c = (char)(*chars);
					if (char.IsLowSurrogate(c))
					{
						if (this.bFallingBack)
						{
							int num = this.iRecursionCount;
							this.iRecursionCount = num + 1;
							if (num > 250)
							{
								this.ThrowLastCharRecursive(char.ConvertToUtf32(ch, c));
							}
						}
						chars += 2;
						this.bFallingBack = this.Fallback(ch, c, index);
						return this.bFallingBack;
					}
				}
			}
			if (this.bFallingBack)
			{
				int num = this.iRecursionCount;
				this.iRecursionCount = num + 1;
				if (num > 250)
				{
					this.ThrowLastCharRecursive((int)ch);
				}
			}
			this.bFallingBack = this.Fallback(ch, index);
			return this.bFallingBack;
		}

		// Token: 0x060066F4 RID: 26356 RVA: 0x0015AC32 File Offset: 0x00158E32
		internal void ThrowLastCharRecursive(int charRecursive)
		{
			throw new ArgumentException(Environment.GetResourceString("Argument_RecursiveFallback", new object[]
			{
				charRecursive
			}), "chars");
		}

		// Token: 0x060066F5 RID: 26357 RVA: 0x0015AC57 File Offset: 0x00158E57
		[__DynamicallyInvokable]
		protected EncoderFallbackBuffer()
		{
		}

		// Token: 0x04002DAF RID: 11695
		[SecurityCritical]
		internal unsafe char* charStart;

		// Token: 0x04002DB0 RID: 11696
		[SecurityCritical]
		internal unsafe char* charEnd;

		// Token: 0x04002DB1 RID: 11697
		internal EncoderNLS encoder;

		// Token: 0x04002DB2 RID: 11698
		internal bool setEncoder;

		// Token: 0x04002DB3 RID: 11699
		internal bool bUsedEncoder;

		// Token: 0x04002DB4 RID: 11700
		internal bool bFallingBack;

		// Token: 0x04002DB5 RID: 11701
		internal int iRecursionCount;

		// Token: 0x04002DB6 RID: 11702
		private const int iMaxRecursion = 250;
	}
}
