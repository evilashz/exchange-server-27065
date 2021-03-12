using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B68 RID: 2920
	[Serializable]
	internal class PredicateOrActionSequenceException : InvalidOperationException
	{
		// Token: 0x06006B2C RID: 27436 RVA: 0x001B76E6 File Offset: 0x001B58E6
		internal PredicateOrActionSequenceException(string message = null, Exception innerException = null) : base(message, innerException)
		{
		}

		// Token: 0x06006B2D RID: 27437 RVA: 0x001B76F0 File Offset: 0x001B58F0
		protected PredicateOrActionSequenceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
