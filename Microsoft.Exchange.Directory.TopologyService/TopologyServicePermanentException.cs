using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x0200003C RID: 60
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TopologyServicePermanentException : LocalizedException
	{
		// Token: 0x0600023D RID: 573 RVA: 0x0000F0A2 File Offset: 0x0000D2A2
		public TopologyServicePermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000F0AB File Offset: 0x0000D2AB
		public TopologyServicePermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000F0B5 File Offset: 0x0000D2B5
		protected TopologyServicePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000F0BF File Offset: 0x0000D2BF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
