using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000080 RID: 128
	[ComVisible(true)]
	[Serializable]
	public class SystemException : Exception
	{
		// Token: 0x060006C3 RID: 1731 RVA: 0x00017846 File Offset: 0x00015A46
		public SystemException() : base(Environment.GetResourceString("Arg_SystemException"))
		{
			base.SetErrorCode(-2146233087);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00017863 File Offset: 0x00015A63
		public SystemException(string message) : base(message)
		{
			base.SetErrorCode(-2146233087);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00017877 File Offset: 0x00015A77
		public SystemException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2146233087);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001788C File Offset: 0x00015A8C
		protected SystemException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
