using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200054B RID: 1355
	[Serializable]
	public class ReplicationCheckSkippedException : ReplicationCheckException
	{
		// Token: 0x0600304D RID: 12365 RVA: 0x000C40A1 File Offset: 0x000C22A1
		public ReplicationCheckSkippedException() : this(LocalizedString.Empty)
		{
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x000C40AE File Offset: 0x000C22AE
		public ReplicationCheckSkippedException(LocalizedString localizedString) : base(localizedString)
		{
		}

		// Token: 0x0600304F RID: 12367 RVA: 0x000C40B7 File Offset: 0x000C22B7
		public ReplicationCheckSkippedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
