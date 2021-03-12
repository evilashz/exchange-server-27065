using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000A9F RID: 2719
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SitesContainerNotFoundException : ADOperationException
	{
		// Token: 0x06007FE9 RID: 32745 RVA: 0x001A4A81 File Offset: 0x001A2C81
		public SitesContainerNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007FEA RID: 32746 RVA: 0x001A4A8A File Offset: 0x001A2C8A
		public SitesContainerNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007FEB RID: 32747 RVA: 0x001A4A94 File Offset: 0x001A2C94
		protected SitesContainerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007FEC RID: 32748 RVA: 0x001A4A9E File Offset: 0x001A2C9E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
