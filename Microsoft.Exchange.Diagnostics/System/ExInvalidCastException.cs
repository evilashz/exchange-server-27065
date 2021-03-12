using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000122 RID: 290
	public class ExInvalidCastException : InvalidCastException
	{
		// Token: 0x0600086B RID: 2155 RVA: 0x000218EA File Offset: 0x0001FAEA
		public ExInvalidCastException()
		{
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x000218F2 File Offset: 0x0001FAF2
		public ExInvalidCastException(string message) : base(message)
		{
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x000218FB File Offset: 0x0001FAFB
		public ExInvalidCastException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00021905 File Offset: 0x0001FB05
		public ExInvalidCastException(string message, int errorCode) : base(message, errorCode)
		{
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0002190F File Offset: 0x0001FB0F
		protected ExInvalidCastException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
