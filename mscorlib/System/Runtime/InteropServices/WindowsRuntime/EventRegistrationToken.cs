using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009B3 RID: 2483
	[__DynamicallyInvokable]
	public struct EventRegistrationToken
	{
		// Token: 0x06006354 RID: 25428 RVA: 0x00151B33 File Offset: 0x0014FD33
		internal EventRegistrationToken(ulong value)
		{
			this.m_value = value;
		}

		// Token: 0x1700113A RID: 4410
		// (get) Token: 0x06006355 RID: 25429 RVA: 0x00151B3C File Offset: 0x0014FD3C
		internal ulong Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x06006356 RID: 25430 RVA: 0x00151B44 File Offset: 0x0014FD44
		[__DynamicallyInvokable]
		public static bool operator ==(EventRegistrationToken left, EventRegistrationToken right)
		{
			return left.Equals(right);
		}

		// Token: 0x06006357 RID: 25431 RVA: 0x00151B59 File Offset: 0x0014FD59
		[__DynamicallyInvokable]
		public static bool operator !=(EventRegistrationToken left, EventRegistrationToken right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06006358 RID: 25432 RVA: 0x00151B74 File Offset: 0x0014FD74
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is EventRegistrationToken && ((EventRegistrationToken)obj).Value == this.Value;
		}

		// Token: 0x06006359 RID: 25433 RVA: 0x00151BA1 File Offset: 0x0014FDA1
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.m_value.GetHashCode();
		}

		// Token: 0x04002C34 RID: 11316
		internal ulong m_value;
	}
}
