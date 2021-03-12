using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200008A RID: 138
	[Serializable]
	public class FilteringServiceFailureException : TransportRuleException
	{
		// Token: 0x06000419 RID: 1049 RVA: 0x00015921 File Offset: 0x00013B21
		public FilteringServiceFailureException(string message) : base(message)
		{
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0001592A File Offset: 0x00013B2A
		public FilteringServiceFailureException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00015934 File Offset: 0x00013B34
		protected FilteringServiceFailureException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
		{
		}
	}
}
