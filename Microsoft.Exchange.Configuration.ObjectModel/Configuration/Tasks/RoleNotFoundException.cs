using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020002C3 RID: 707
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RoleNotFoundException : LocalizedException
	{
		// Token: 0x0600193C RID: 6460 RVA: 0x0005CE22 File Offset: 0x0005B022
		public RoleNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x0005CE2B File Offset: 0x0005B02B
		public RoleNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x0005CE35 File Offset: 0x0005B035
		protected RoleNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x0005CE3F File Offset: 0x0005B03F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
