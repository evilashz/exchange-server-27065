using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200008C RID: 140
	[Serializable]
	public class TransportRulePermanentException : TransportRuleException
	{
		// Token: 0x0600041F RID: 1055 RVA: 0x0001595B File Offset: 0x00013B5B
		public TransportRulePermanentException(string message) : this(message, null)
		{
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00015965 File Offset: 0x00013B65
		public TransportRulePermanentException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0001596F File Offset: 0x00013B6F
		protected TransportRulePermanentException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
		{
		}
	}
}
