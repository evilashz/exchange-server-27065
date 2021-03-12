using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies
{
	// Token: 0x020000AD RID: 173
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidFilteringServiceResultException : LocalizedException
	{
		// Token: 0x060004FC RID: 1276 RVA: 0x000181A1 File Offset: 0x000163A1
		public InvalidFilteringServiceResultException() : base(TransportRulesStrings.InvalidFilteringServiceResult)
		{
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x000181AE File Offset: 0x000163AE
		public InvalidFilteringServiceResultException(Exception innerException) : base(TransportRulesStrings.InvalidFilteringServiceResult, innerException)
		{
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x000181BC File Offset: 0x000163BC
		protected InvalidFilteringServiceResultException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x000181C6 File Offset: 0x000163C6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
