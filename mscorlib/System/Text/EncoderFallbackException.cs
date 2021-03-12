using System;
using System.Runtime.Serialization;

namespace System.Text
{
	// Token: 0x02000A41 RID: 2625
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class EncoderFallbackException : ArgumentException
	{
		// Token: 0x060066D9 RID: 26329 RVA: 0x0015A87E File Offset: 0x00158A7E
		[__DynamicallyInvokable]
		public EncoderFallbackException() : base(Environment.GetResourceString("Arg_ArgumentException"))
		{
			base.SetErrorCode(-2147024809);
		}

		// Token: 0x060066DA RID: 26330 RVA: 0x0015A89B File Offset: 0x00158A9B
		[__DynamicallyInvokable]
		public EncoderFallbackException(string message) : base(message)
		{
			base.SetErrorCode(-2147024809);
		}

		// Token: 0x060066DB RID: 26331 RVA: 0x0015A8AF File Offset: 0x00158AAF
		[__DynamicallyInvokable]
		public EncoderFallbackException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2147024809);
		}

		// Token: 0x060066DC RID: 26332 RVA: 0x0015A8C4 File Offset: 0x00158AC4
		internal EncoderFallbackException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060066DD RID: 26333 RVA: 0x0015A8CE File Offset: 0x00158ACE
		internal EncoderFallbackException(string message, char charUnknown, int index) : base(message)
		{
			this.charUnknown = charUnknown;
			this.index = index;
		}

		// Token: 0x060066DE RID: 26334 RVA: 0x0015A8E8 File Offset: 0x00158AE8
		internal EncoderFallbackException(string message, char charUnknownHigh, char charUnknownLow, int index) : base(message)
		{
			if (!char.IsHighSurrogate(charUnknownHigh))
			{
				throw new ArgumentOutOfRangeException("charUnknownHigh", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					55296,
					56319
				}));
			}
			if (!char.IsLowSurrogate(charUnknownLow))
			{
				throw new ArgumentOutOfRangeException("CharUnknownLow", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					56320,
					57343
				}));
			}
			this.charUnknownHigh = charUnknownHigh;
			this.charUnknownLow = charUnknownLow;
			this.index = index;
		}

		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x060066DF RID: 26335 RVA: 0x0015A98C File Offset: 0x00158B8C
		[__DynamicallyInvokable]
		public char CharUnknown
		{
			[__DynamicallyInvokable]
			get
			{
				return this.charUnknown;
			}
		}

		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x060066E0 RID: 26336 RVA: 0x0015A994 File Offset: 0x00158B94
		[__DynamicallyInvokable]
		public char CharUnknownHigh
		{
			[__DynamicallyInvokable]
			get
			{
				return this.charUnknownHigh;
			}
		}

		// Token: 0x170011A4 RID: 4516
		// (get) Token: 0x060066E1 RID: 26337 RVA: 0x0015A99C File Offset: 0x00158B9C
		[__DynamicallyInvokable]
		public char CharUnknownLow
		{
			[__DynamicallyInvokable]
			get
			{
				return this.charUnknownLow;
			}
		}

		// Token: 0x170011A5 RID: 4517
		// (get) Token: 0x060066E2 RID: 26338 RVA: 0x0015A9A4 File Offset: 0x00158BA4
		[__DynamicallyInvokable]
		public int Index
		{
			[__DynamicallyInvokable]
			get
			{
				return this.index;
			}
		}

		// Token: 0x060066E3 RID: 26339 RVA: 0x0015A9AC File Offset: 0x00158BAC
		[__DynamicallyInvokable]
		public bool IsUnknownSurrogate()
		{
			return this.charUnknownHigh > '\0';
		}

		// Token: 0x04002DA7 RID: 11687
		private char charUnknown;

		// Token: 0x04002DA8 RID: 11688
		private char charUnknownHigh;

		// Token: 0x04002DA9 RID: 11689
		private char charUnknownLow;

		// Token: 0x04002DAA RID: 11690
		private int index;
	}
}
