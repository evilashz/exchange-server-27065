using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200008D RID: 141
	[Serializable]
	public class TransportRuleTransientException : TransportRuleException
	{
		// Token: 0x06000422 RID: 1058 RVA: 0x00015979 File Offset: 0x00013B79
		public TransportRuleTransientException(string message) : this(message, null)
		{
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00015983 File Offset: 0x00013B83
		public TransportRuleTransientException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0001598D File Offset: 0x00013B8D
		protected TransportRuleTransientException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
		{
		}
	}
}
