using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000FF RID: 255
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class InvalidCastException : SystemException
	{
		// Token: 0x06000FAD RID: 4013 RVA: 0x00030181 File Offset: 0x0002E381
		[__DynamicallyInvokable]
		public InvalidCastException() : base(Environment.GetResourceString("Arg_InvalidCastException"))
		{
			base.SetErrorCode(-2147467262);
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0003019E File Offset: 0x0002E39E
		[__DynamicallyInvokable]
		public InvalidCastException(string message) : base(message)
		{
			base.SetErrorCode(-2147467262);
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x000301B2 File Offset: 0x0002E3B2
		[__DynamicallyInvokable]
		public InvalidCastException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2147467262);
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x000301C7 File Offset: 0x0002E3C7
		protected InvalidCastException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x000301D1 File Offset: 0x0002E3D1
		[__DynamicallyInvokable]
		public InvalidCastException(string message, int errorCode) : base(message)
		{
			base.SetErrorCode(errorCode);
		}
	}
}
