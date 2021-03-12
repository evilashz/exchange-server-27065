using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F9E RID: 3998
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SendConnectorProxyEnabledOnEdgeException : LocalizedException
	{
		// Token: 0x0600ACE7 RID: 44263 RVA: 0x00290CA6 File Offset: 0x0028EEA6
		public SendConnectorProxyEnabledOnEdgeException() : base(Strings.SendConnectorProxyEnabledOnEdge)
		{
		}

		// Token: 0x0600ACE8 RID: 44264 RVA: 0x00290CB3 File Offset: 0x0028EEB3
		public SendConnectorProxyEnabledOnEdgeException(Exception innerException) : base(Strings.SendConnectorProxyEnabledOnEdge, innerException)
		{
		}

		// Token: 0x0600ACE9 RID: 44265 RVA: 0x00290CC1 File Offset: 0x0028EEC1
		protected SendConnectorProxyEnabledOnEdgeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ACEA RID: 44266 RVA: 0x00290CCB File Offset: 0x0028EECB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
