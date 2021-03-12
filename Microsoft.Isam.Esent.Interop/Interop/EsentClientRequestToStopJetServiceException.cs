using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001BD RID: 445
	[Serializable]
	public sealed class EsentClientRequestToStopJetServiceException : EsentOperationException
	{
		// Token: 0x0600098D RID: 2445 RVA: 0x0001313E File Offset: 0x0001133E
		public EsentClientRequestToStopJetServiceException() : base("Client has requested stop service", JET_err.ClientRequestToStopJetService)
		{
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00013150 File Offset: 0x00011350
		private EsentClientRequestToStopJetServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
