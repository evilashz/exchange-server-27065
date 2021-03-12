using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200012C RID: 300
	public class ExArgumentOutOfRangeException : ArgumentOutOfRangeException
	{
		// Token: 0x060008B1 RID: 2225 RVA: 0x00022293 File Offset: 0x00020493
		public ExArgumentOutOfRangeException()
		{
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0002229B File Offset: 0x0002049B
		public ExArgumentOutOfRangeException(string message) : base(message)
		{
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x000222A4 File Offset: 0x000204A4
		public ExArgumentOutOfRangeException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x000222AE File Offset: 0x000204AE
		public ExArgumentOutOfRangeException(string message, string paramName) : base(message, paramName)
		{
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x000222B8 File Offset: 0x000204B8
		public ExArgumentOutOfRangeException(string paramName, object actualValue, string message) : base(paramName, actualValue, message)
		{
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x000222C3 File Offset: 0x000204C3
		protected ExArgumentOutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
