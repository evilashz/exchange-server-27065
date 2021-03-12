using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200008B RID: 139
	[Serializable]
	public class FilteringServiceTimeoutException : FilteringServiceFailureException
	{
		// Token: 0x0600041C RID: 1052 RVA: 0x0001593E File Offset: 0x00013B3E
		public FilteringServiceTimeoutException(string message) : base(message)
		{
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00015947 File Offset: 0x00013B47
		public FilteringServiceTimeoutException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00015951 File Offset: 0x00013B51
		protected FilteringServiceTimeoutException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
		{
		}
	}
}
