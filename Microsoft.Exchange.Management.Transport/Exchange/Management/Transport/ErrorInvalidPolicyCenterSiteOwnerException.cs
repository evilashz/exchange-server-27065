using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000189 RID: 393
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ErrorInvalidPolicyCenterSiteOwnerException : LocalizedException
	{
		// Token: 0x06000F9C RID: 3996 RVA: 0x0003668E File Offset: 0x0003488E
		public ErrorInvalidPolicyCenterSiteOwnerException() : base(Strings.ErrorInvalidPolicyCenterSiteOwner)
		{
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x0003669B File Offset: 0x0003489B
		public ErrorInvalidPolicyCenterSiteOwnerException(Exception innerException) : base(Strings.ErrorInvalidPolicyCenterSiteOwner, innerException)
		{
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x000366A9 File Offset: 0x000348A9
		protected ErrorInvalidPolicyCenterSiteOwnerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x000366B3 File Offset: 0x000348B3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
