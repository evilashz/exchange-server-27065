using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000089 RID: 137
	[Serializable]
	public class TransportRuleException : Exception
	{
		// Token: 0x06000416 RID: 1046 RVA: 0x00015903 File Offset: 0x00013B03
		public TransportRuleException(string message) : this(message, null)
		{
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0001590D File Offset: 0x00013B0D
		public TransportRuleException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00015917 File Offset: 0x00013B17
		protected TransportRuleException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
		{
		}
	}
}
