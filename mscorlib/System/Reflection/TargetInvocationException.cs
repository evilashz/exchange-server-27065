using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020005F5 RID: 1525
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class TargetInvocationException : ApplicationException
	{
		// Token: 0x06004797 RID: 18327 RVA: 0x00102F1D File Offset: 0x0010111D
		private TargetInvocationException() : base(Environment.GetResourceString("Arg_TargetInvocationException"))
		{
			base.SetErrorCode(-2146232828);
		}

		// Token: 0x06004798 RID: 18328 RVA: 0x00102F3A File Offset: 0x0010113A
		private TargetInvocationException(string message) : base(message)
		{
			base.SetErrorCode(-2146232828);
		}

		// Token: 0x06004799 RID: 18329 RVA: 0x00102F4E File Offset: 0x0010114E
		[__DynamicallyInvokable]
		public TargetInvocationException(Exception inner) : base(Environment.GetResourceString("Arg_TargetInvocationException"), inner)
		{
			base.SetErrorCode(-2146232828);
		}

		// Token: 0x0600479A RID: 18330 RVA: 0x00102F6C File Offset: 0x0010116C
		[__DynamicallyInvokable]
		public TargetInvocationException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146232828);
		}

		// Token: 0x0600479B RID: 18331 RVA: 0x00102F81 File Offset: 0x00101181
		internal TargetInvocationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
