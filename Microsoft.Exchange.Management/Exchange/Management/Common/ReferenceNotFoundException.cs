using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x0200012D RID: 301
	[Serializable]
	public class ReferenceNotFoundException : ReferenceException
	{
		// Token: 0x06000ABE RID: 2750 RVA: 0x00032547 File Offset: 0x00030747
		public ReferenceNotFoundException(string referenceValue, LocalizedException innerException) : base(referenceValue, innerException)
		{
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00032551 File Offset: 0x00030751
		public ReferenceNotFoundException(string referenceValue, Exception innerException) : base(referenceValue, innerException)
		{
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0003255B File Offset: 0x0003075B
		public ReferenceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
