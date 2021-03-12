using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000713 RID: 1811
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class ACLTooBigException : StoragePermanentException
	{
		// Token: 0x06004789 RID: 18313 RVA: 0x001301B7 File Offset: 0x0012E3B7
		public ACLTooBigException() : base(ServerStrings.ACLTooBig)
		{
		}

		// Token: 0x0600478A RID: 18314 RVA: 0x001301C4 File Offset: 0x0012E3C4
		protected ACLTooBigException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
