using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200012D RID: 301
	public class ExInvalidOperationException : InvalidOperationException
	{
		// Token: 0x060008B7 RID: 2231 RVA: 0x000222CD File Offset: 0x000204CD
		public ExInvalidOperationException()
		{
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x000222D5 File Offset: 0x000204D5
		public ExInvalidOperationException(string message) : base(message)
		{
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x000222DE File Offset: 0x000204DE
		public ExInvalidOperationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x000222E8 File Offset: 0x000204E8
		protected ExInvalidOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
