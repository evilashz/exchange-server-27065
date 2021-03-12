using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F82 RID: 3970
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServerNotHubOrEdgeException : LocalizedException
	{
		// Token: 0x0600AC69 RID: 44137 RVA: 0x00290384 File Offset: 0x0028E584
		public ServerNotHubOrEdgeException() : base(Strings.ServerNotHubOrEdge)
		{
		}

		// Token: 0x0600AC6A RID: 44138 RVA: 0x00290391 File Offset: 0x0028E591
		public ServerNotHubOrEdgeException(Exception innerException) : base(Strings.ServerNotHubOrEdge, innerException)
		{
		}

		// Token: 0x0600AC6B RID: 44139 RVA: 0x0029039F File Offset: 0x0028E59F
		protected ServerNotHubOrEdgeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AC6C RID: 44140 RVA: 0x002903A9 File Offset: 0x0028E5A9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
