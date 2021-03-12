using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001C6 RID: 454
	[Serializable]
	internal class UnresolvedRecipientBifurcatedTransientException : Exception
	{
		// Token: 0x060014CF RID: 5327 RVA: 0x0005383F File Offset: 0x00051A3F
		public UnresolvedRecipientBifurcatedTransientException()
		{
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x00053847 File Offset: 0x00051A47
		protected UnresolvedRecipientBifurcatedTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
