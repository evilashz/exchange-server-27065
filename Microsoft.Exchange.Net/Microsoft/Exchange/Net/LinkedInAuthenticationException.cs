using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net
{
	// Token: 0x020000F8 RID: 248
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class LinkedInAuthenticationException : LocalizedException
	{
		// Token: 0x06000672 RID: 1650 RVA: 0x000168BD File Offset: 0x00014ABD
		public LinkedInAuthenticationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x000168C6 File Offset: 0x00014AC6
		public LinkedInAuthenticationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x000168D0 File Offset: 0x00014AD0
		protected LinkedInAuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x000168DA File Offset: 0x00014ADA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
