using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000083 RID: 131
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class DataMisalignedException : SystemException
	{
		// Token: 0x060006CF RID: 1743 RVA: 0x00017932 File Offset: 0x00015B32
		[__DynamicallyInvokable]
		public DataMisalignedException() : base(Environment.GetResourceString("Arg_DataMisalignedException"))
		{
			base.SetErrorCode(-2146233023);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001794F File Offset: 0x00015B4F
		[__DynamicallyInvokable]
		public DataMisalignedException(string message) : base(message)
		{
			base.SetErrorCode(-2146233023);
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00017963 File Offset: 0x00015B63
		[__DynamicallyInvokable]
		public DataMisalignedException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2146233023);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00017978 File Offset: 0x00015B78
		internal DataMisalignedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
