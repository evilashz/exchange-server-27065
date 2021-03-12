using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Exceptions
{
	// Token: 0x0200001F RID: 31
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class NonPromotableTransientException : TransientException
	{
		// Token: 0x06000147 RID: 327 RVA: 0x000053A9 File Offset: 0x000035A9
		public NonPromotableTransientException(LocalizedString localizedString) : base(localizedString)
		{
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000053B2 File Offset: 0x000035B2
		public NonPromotableTransientException(LocalizedString localizedString, Exception innerException) : base(localizedString, innerException)
		{
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000053BC File Offset: 0x000035BC
		protected NonPromotableTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
