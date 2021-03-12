using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000061 RID: 97
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FacebookContactUploadException : LocalizedException
	{
		// Token: 0x06000440 RID: 1088 RVA: 0x0001020F File Offset: 0x0000E40F
		public FacebookContactUploadException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00010218 File Offset: 0x0000E418
		public FacebookContactUploadException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00010222 File Offset: 0x0000E422
		protected FacebookContactUploadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0001022C File Offset: 0x0000E42C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
