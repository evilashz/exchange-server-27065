using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001061 RID: 4193
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidParamSpecifyIdentityOrDagException : LocalizedException
	{
		// Token: 0x0600B0BA RID: 45242 RVA: 0x00296B56 File Offset: 0x00294D56
		public InvalidParamSpecifyIdentityOrDagException() : base(Strings.InvalidParamSpecifyIdentityOrDagException)
		{
		}

		// Token: 0x0600B0BB RID: 45243 RVA: 0x00296B63 File Offset: 0x00294D63
		public InvalidParamSpecifyIdentityOrDagException(Exception innerException) : base(Strings.InvalidParamSpecifyIdentityOrDagException, innerException)
		{
		}

		// Token: 0x0600B0BC RID: 45244 RVA: 0x00296B71 File Offset: 0x00294D71
		protected InvalidParamSpecifyIdentityOrDagException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B0BD RID: 45245 RVA: 0x00296B7B File Offset: 0x00294D7B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
