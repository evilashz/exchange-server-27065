using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A78 RID: 2680
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADVlvSizeLimitExceededException : ADOperationException
	{
		// Token: 0x06007F3D RID: 32573 RVA: 0x001A3F3D File Offset: 0x001A213D
		public ADVlvSizeLimitExceededException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F3E RID: 32574 RVA: 0x001A3F46 File Offset: 0x001A2146
		public ADVlvSizeLimitExceededException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F3F RID: 32575 RVA: 0x001A3F50 File Offset: 0x001A2150
		protected ADVlvSizeLimitExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F40 RID: 32576 RVA: 0x001A3F5A File Offset: 0x001A215A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
