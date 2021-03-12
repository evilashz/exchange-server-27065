using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001D6 RID: 470
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MwiDeliveryException : LocalizedException
	{
		// Token: 0x06000F4F RID: 3919 RVA: 0x0003640F File Offset: 0x0003460F
		public MwiDeliveryException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x00036418 File Offset: 0x00034618
		public MwiDeliveryException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x00036422 File Offset: 0x00034622
		protected MwiDeliveryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0003642C File Offset: 0x0003462C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
