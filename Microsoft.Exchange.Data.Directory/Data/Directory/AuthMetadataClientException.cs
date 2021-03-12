using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AED RID: 2797
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AuthMetadataClientException : LocalizedException
	{
		// Token: 0x0600814D RID: 33101 RVA: 0x001A6537 File Offset: 0x001A4737
		public AuthMetadataClientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600814E RID: 33102 RVA: 0x001A6540 File Offset: 0x001A4740
		public AuthMetadataClientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600814F RID: 33103 RVA: 0x001A654A File Offset: 0x001A474A
		protected AuthMetadataClientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008150 RID: 33104 RVA: 0x001A6554 File Offset: 0x001A4754
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
