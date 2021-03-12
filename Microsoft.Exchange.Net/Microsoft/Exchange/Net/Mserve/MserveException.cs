using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.Mserve
{
	// Token: 0x02000886 RID: 2182
	[Serializable]
	internal class MserveException : Exception
	{
		// Token: 0x06002E74 RID: 11892 RVA: 0x00066717 File Offset: 0x00064917
		public MserveException()
		{
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x0006671F File Offset: 0x0006491F
		public MserveException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06002E76 RID: 11894 RVA: 0x00066729 File Offset: 0x00064929
		public MserveException(string message) : base(message)
		{
		}

		// Token: 0x06002E77 RID: 11895 RVA: 0x00066732 File Offset: 0x00064932
		protected MserveException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}
	}
}
