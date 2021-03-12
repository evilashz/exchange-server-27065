using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200075D RID: 1885
	[Serializable]
	public class OccurrenceNotFoundException : ObjectNotFoundException
	{
		// Token: 0x06004865 RID: 18533 RVA: 0x00130F1D File Offset: 0x0012F11D
		public OccurrenceNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06004866 RID: 18534 RVA: 0x00130F26 File Offset: 0x0012F126
		public OccurrenceNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004867 RID: 18535 RVA: 0x00130F30 File Offset: 0x0012F130
		protected OccurrenceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
