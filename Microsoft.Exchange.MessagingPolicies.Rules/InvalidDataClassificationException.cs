using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MessagingPolicies
{
	// Token: 0x020000AC RID: 172
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidDataClassificationException : ArgumentException
	{
		// Token: 0x060004F8 RID: 1272 RVA: 0x00018168 File Offset: 0x00016368
		public InvalidDataClassificationException() : base(TransportRulesStrings.InvalidDataClassification)
		{
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001817A File Offset: 0x0001637A
		public InvalidDataClassificationException(Exception innerException) : base(TransportRulesStrings.InvalidDataClassification, innerException)
		{
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001818D File Offset: 0x0001638D
		protected InvalidDataClassificationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00018197 File Offset: 0x00016397
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
