using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x020010A4 RID: 4260
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SubscriptionPasswordEmptyException : LocalizedException
	{
		// Token: 0x0600B231 RID: 45617 RVA: 0x002997EB File Offset: 0x002979EB
		public SubscriptionPasswordEmptyException() : base(Strings.SubscriptionPasswordEmptyException)
		{
		}

		// Token: 0x0600B232 RID: 45618 RVA: 0x002997F8 File Offset: 0x002979F8
		public SubscriptionPasswordEmptyException(Exception innerException) : base(Strings.SubscriptionPasswordEmptyException, innerException)
		{
		}

		// Token: 0x0600B233 RID: 45619 RVA: 0x00299806 File Offset: 0x00297A06
		protected SubscriptionPasswordEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B234 RID: 45620 RVA: 0x00299810 File Offset: 0x00297A10
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
