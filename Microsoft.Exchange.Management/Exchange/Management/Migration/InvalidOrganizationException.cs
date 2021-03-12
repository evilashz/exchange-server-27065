using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x0200111A RID: 4378
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidOrganizationException : LocalizedException
	{
		// Token: 0x0600B46F RID: 46191 RVA: 0x0029CC17 File Offset: 0x0029AE17
		public InvalidOrganizationException() : base(Strings.ErrorInvalidOrganization)
		{
		}

		// Token: 0x0600B470 RID: 46192 RVA: 0x0029CC24 File Offset: 0x0029AE24
		public InvalidOrganizationException(Exception innerException) : base(Strings.ErrorInvalidOrganization, innerException)
		{
		}

		// Token: 0x0600B471 RID: 46193 RVA: 0x0029CC32 File Offset: 0x0029AE32
		protected InvalidOrganizationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B472 RID: 46194 RVA: 0x0029CC3C File Offset: 0x0029AE3C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
