using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000027 RID: 39
	public class BaseException : Exception
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x00004275 File Offset: 0x00002475
		public BaseException()
		{
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000427D File Offset: 0x0000247D
		public BaseException(string message) : base(message, null)
		{
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004287 File Offset: 0x00002487
		public BaseException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004291 File Offset: 0x00002491
		protected BaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
