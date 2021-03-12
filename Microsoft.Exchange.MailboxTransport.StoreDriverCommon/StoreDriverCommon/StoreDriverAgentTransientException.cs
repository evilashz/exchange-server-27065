using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverCommon
{
	// Token: 0x02000012 RID: 18
	[Serializable]
	internal class StoreDriverAgentTransientException : TransientException
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00003CB2 File Offset: 0x00001EB2
		public StoreDriverAgentTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003CBB File Offset: 0x00001EBB
		public StoreDriverAgentTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
