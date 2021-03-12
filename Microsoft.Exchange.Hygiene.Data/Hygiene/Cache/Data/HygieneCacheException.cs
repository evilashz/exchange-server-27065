using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Hygiene.Cache.Data
{
	// Token: 0x0200004A RID: 74
	[Serializable]
	internal class HygieneCacheException : Exception
	{
		// Token: 0x06000300 RID: 768 RVA: 0x000096BB File Offset: 0x000078BB
		public HygieneCacheException()
		{
		}

		// Token: 0x06000301 RID: 769 RVA: 0x000096C3 File Offset: 0x000078C3
		public HygieneCacheException(string message) : base(message)
		{
		}

		// Token: 0x06000302 RID: 770 RVA: 0x000096CC File Offset: 0x000078CC
		public HygieneCacheException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000303 RID: 771 RVA: 0x000096D6 File Offset: 0x000078D6
		protected HygieneCacheException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
