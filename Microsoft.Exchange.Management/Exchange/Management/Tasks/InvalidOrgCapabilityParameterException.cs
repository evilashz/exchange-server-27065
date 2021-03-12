using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001170 RID: 4464
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidOrgCapabilityParameterException : LocalizedException
	{
		// Token: 0x0600B61B RID: 46619 RVA: 0x0029F4AD File Offset: 0x0029D6AD
		public InvalidOrgCapabilityParameterException() : base(Strings.InvalidOrgCapabilityParameter)
		{
		}

		// Token: 0x0600B61C RID: 46620 RVA: 0x0029F4BA File Offset: 0x0029D6BA
		public InvalidOrgCapabilityParameterException(Exception innerException) : base(Strings.InvalidOrgCapabilityParameter, innerException)
		{
		}

		// Token: 0x0600B61D RID: 46621 RVA: 0x0029F4C8 File Offset: 0x0029D6C8
		protected InvalidOrgCapabilityParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B61E RID: 46622 RVA: 0x0029F4D2 File Offset: 0x0029D6D2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
