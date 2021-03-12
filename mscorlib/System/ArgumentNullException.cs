using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	// Token: 0x020000A7 RID: 167
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class ArgumentNullException : ArgumentException
	{
		// Token: 0x060009A1 RID: 2465 RVA: 0x0001F418 File Offset: 0x0001D618
		[__DynamicallyInvokable]
		public ArgumentNullException() : base(Environment.GetResourceString("ArgumentNull_Generic"))
		{
			base.SetErrorCode(-2147467261);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0001F435 File Offset: 0x0001D635
		[__DynamicallyInvokable]
		public ArgumentNullException(string paramName) : base(Environment.GetResourceString("ArgumentNull_Generic"), paramName)
		{
			base.SetErrorCode(-2147467261);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0001F453 File Offset: 0x0001D653
		[__DynamicallyInvokable]
		public ArgumentNullException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2147467261);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0001F468 File Offset: 0x0001D668
		[__DynamicallyInvokable]
		public ArgumentNullException(string paramName, string message) : base(message, paramName)
		{
			base.SetErrorCode(-2147467261);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0001F47D File Offset: 0x0001D67D
		[SecurityCritical]
		protected ArgumentNullException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
