﻿using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000084 RID: 132
	[Obsolete("This type previously indicated an unspecified fatal error in the runtime. The runtime no longer raises this exception so this type is obsolete.")]
	[ComVisible(true)]
	[Serializable]
	public sealed class ExecutionEngineException : SystemException
	{
		// Token: 0x060006D3 RID: 1747 RVA: 0x00017982 File Offset: 0x00015B82
		public ExecutionEngineException() : base(Environment.GetResourceString("Arg_ExecutionEngineException"))
		{
			base.SetErrorCode(-2146233082);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001799F File Offset: 0x00015B9F
		public ExecutionEngineException(string message) : base(message)
		{
			base.SetErrorCode(-2146233082);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x000179B3 File Offset: 0x00015BB3
		public ExecutionEngineException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2146233082);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x000179C8 File Offset: 0x00015BC8
		internal ExecutionEngineException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
