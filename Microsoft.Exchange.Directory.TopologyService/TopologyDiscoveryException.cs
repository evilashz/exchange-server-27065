using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x0200003D RID: 61
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TopologyDiscoveryException : TopologyServiceTransientException
	{
		// Token: 0x06000241 RID: 577 RVA: 0x0000F0C9 File Offset: 0x0000D2C9
		public TopologyDiscoveryException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000F0D2 File Offset: 0x0000D2D2
		public TopologyDiscoveryException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000F0DC File Offset: 0x0000D2DC
		protected TopologyDiscoveryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000F0E6 File Offset: 0x0000D2E6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
