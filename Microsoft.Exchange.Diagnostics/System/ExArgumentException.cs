using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000121 RID: 289
	public class ExArgumentException : ArgumentException
	{
		// Token: 0x06000865 RID: 2149 RVA: 0x000218B0 File Offset: 0x0001FAB0
		public ExArgumentException()
		{
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x000218B8 File Offset: 0x0001FAB8
		public ExArgumentException(string message) : base(message)
		{
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000218C1 File Offset: 0x0001FAC1
		public ExArgumentException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x000218CB File Offset: 0x0001FACB
		public ExArgumentException(string message, string paramName) : base(message, paramName)
		{
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x000218D5 File Offset: 0x0001FAD5
		public ExArgumentException(string message, string paramName, Exception innerException) : base(message, paramName, innerException)
		{
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x000218E0 File Offset: 0x0001FAE0
		protected ExArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
