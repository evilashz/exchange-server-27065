using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020002C2 RID: 706
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServerNotAvailableException : LocalizedException
	{
		// Token: 0x06001938 RID: 6456 RVA: 0x0005CDF3 File Offset: 0x0005AFF3
		public ServerNotAvailableException() : base(Strings.ServerNotAvailable)
		{
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x0005CE00 File Offset: 0x0005B000
		public ServerNotAvailableException(Exception innerException) : base(Strings.ServerNotAvailable, innerException)
		{
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x0005CE0E File Offset: 0x0005B00E
		protected ServerNotAvailableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x0005CE18 File Offset: 0x0005B018
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
