using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies
{
	// Token: 0x020000AE RID: 174
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidPriorityException : ExchangeConfigurationException
	{
		// Token: 0x06000500 RID: 1280 RVA: 0x000181D0 File Offset: 0x000163D0
		public InvalidPriorityException() : base(TransportRulesStrings.InvalidPriority)
		{
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x000181DD File Offset: 0x000163DD
		public InvalidPriorityException(Exception innerException) : base(TransportRulesStrings.InvalidPriority, innerException)
		{
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x000181EB File Offset: 0x000163EB
		protected InvalidPriorityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x000181F5 File Offset: 0x000163F5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
