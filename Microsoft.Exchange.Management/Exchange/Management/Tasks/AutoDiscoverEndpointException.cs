using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FD1 RID: 4049
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AutoDiscoverEndpointException : TaskException
	{
		// Token: 0x0600ADE9 RID: 44521 RVA: 0x0029261E File Offset: 0x0029081E
		public AutoDiscoverEndpointException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600ADEA RID: 44522 RVA: 0x00292627 File Offset: 0x00290827
		public AutoDiscoverEndpointException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600ADEB RID: 44523 RVA: 0x00292631 File Offset: 0x00290831
		protected AutoDiscoverEndpointException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ADEC RID: 44524 RVA: 0x0029263B File Offset: 0x0029083B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
