using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FB7 RID: 4023
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorCannotFindTargetDeliveryDomainException : LocalizedException
	{
		// Token: 0x0600AD6C RID: 44396 RVA: 0x00291B31 File Offset: 0x0028FD31
		public ErrorCannotFindTargetDeliveryDomainException() : base(Strings.ErrorCannotFindTargetDeliveryDomain)
		{
		}

		// Token: 0x0600AD6D RID: 44397 RVA: 0x00291B3E File Offset: 0x0028FD3E
		public ErrorCannotFindTargetDeliveryDomainException(Exception innerException) : base(Strings.ErrorCannotFindTargetDeliveryDomain, innerException)
		{
		}

		// Token: 0x0600AD6E RID: 44398 RVA: 0x00291B4C File Offset: 0x0028FD4C
		protected ErrorCannotFindTargetDeliveryDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AD6F RID: 44399 RVA: 0x00291B56 File Offset: 0x0028FD56
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
