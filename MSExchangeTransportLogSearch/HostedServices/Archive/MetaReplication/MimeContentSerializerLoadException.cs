using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.Archive.MetaReplication
{
	// Token: 0x0200004C RID: 76
	[Serializable]
	public class MimeContentSerializerLoadException : Exception
	{
		// Token: 0x060001AC RID: 428 RVA: 0x0000BA6C File Offset: 0x00009C6C
		public MimeContentSerializerLoadException() : base("MimeContentDescriptions failed to load one or more types")
		{
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000BA79 File Offset: 0x00009C79
		public MimeContentSerializerLoadException(Exception innerException) : base("MimeContentDescriptions failed to load one or more types", innerException)
		{
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000BA87 File Offset: 0x00009C87
		public MimeContentSerializerLoadException(string message) : base(message)
		{
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000BA90 File Offset: 0x00009C90
		public MimeContentSerializerLoadException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000BA9A File Offset: 0x00009C9A
		protected MimeContentSerializerLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
