using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x0200012F RID: 303
	[Serializable]
	public class ReferenceAmbiguousException : ReferenceException
	{
		// Token: 0x06000AC4 RID: 2756 RVA: 0x00032583 File Offset: 0x00030783
		public ReferenceAmbiguousException(string referenceValue, LocalizedException innerException) : base(referenceValue, innerException)
		{
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0003258D File Offset: 0x0003078D
		public ReferenceAmbiguousException(string referenceValue, Exception innerException) : base(referenceValue, innerException)
		{
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00032597 File Offset: 0x00030797
		public ReferenceAmbiguousException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
