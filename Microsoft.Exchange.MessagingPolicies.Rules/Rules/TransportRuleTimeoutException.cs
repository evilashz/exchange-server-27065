using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200008E RID: 142
	[Serializable]
	public class TransportRuleTimeoutException : TransportRuleTransientException
	{
		// Token: 0x06000425 RID: 1061 RVA: 0x00015997 File Offset: 0x00013B97
		public TransportRuleTimeoutException(string message) : this(message, null)
		{
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000159A1 File Offset: 0x00013BA1
		public TransportRuleTimeoutException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x000159AB File Offset: 0x00013BAB
		protected TransportRuleTimeoutException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
		{
		}
	}
}
