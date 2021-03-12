using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000149 RID: 329
	[__DynamicallyInvokable]
	[Serializable]
	public class TypeAccessException : TypeLoadException
	{
		// Token: 0x060014AE RID: 5294 RVA: 0x0003D456 File Offset: 0x0003B656
		[__DynamicallyInvokable]
		public TypeAccessException() : base(Environment.GetResourceString("Arg_TypeAccessException"))
		{
			base.SetErrorCode(-2146233021);
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x0003D473 File Offset: 0x0003B673
		[__DynamicallyInvokable]
		public TypeAccessException(string message) : base(message)
		{
			base.SetErrorCode(-2146233021);
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x0003D487 File Offset: 0x0003B687
		[__DynamicallyInvokable]
		public TypeAccessException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233021);
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x0003D49C File Offset: 0x0003B69C
		protected TypeAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			base.SetErrorCode(-2146233021);
		}
	}
}
