using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001D7 RID: 471
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ResourceDirectoryNotFoundException : LocalizedException
	{
		// Token: 0x06000F53 RID: 3923 RVA: 0x00036436 File Offset: 0x00034636
		public ResourceDirectoryNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0003643F File Offset: 0x0003463F
		public ResourceDirectoryNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00036449 File Offset: 0x00034649
		protected ResourceDirectoryNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x00036453 File Offset: 0x00034653
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
