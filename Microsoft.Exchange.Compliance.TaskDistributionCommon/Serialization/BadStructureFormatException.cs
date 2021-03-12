using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization
{
	// Token: 0x0200005D RID: 93
	[Serializable]
	public class BadStructureFormatException : Exception
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x0000D209 File Offset: 0x0000B409
		public BadStructureFormatException()
		{
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000D211 File Offset: 0x0000B411
		public BadStructureFormatException(string message) : base(message)
		{
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000D21A File Offset: 0x0000B41A
		public BadStructureFormatException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000D224 File Offset: 0x0000B424
		protected BadStructureFormatException(SerializationInfo info, StreamingContext context)
		{
		}
	}
}
