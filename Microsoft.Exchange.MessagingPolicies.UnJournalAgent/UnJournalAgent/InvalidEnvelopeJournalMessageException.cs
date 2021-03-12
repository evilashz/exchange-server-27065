using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.UnJournalAgent
{
	// Token: 0x02000026 RID: 38
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidEnvelopeJournalMessageException : LocalizedException
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x00007A4B File Offset: 0x00005C4B
		public InvalidEnvelopeJournalMessageException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00007A54 File Offset: 0x00005C54
		public InvalidEnvelopeJournalMessageException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00007A5E File Offset: 0x00005C5E
		protected InvalidEnvelopeJournalMessageException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00007A68 File Offset: 0x00005C68
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
