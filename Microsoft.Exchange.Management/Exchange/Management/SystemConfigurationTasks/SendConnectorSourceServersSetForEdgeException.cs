using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F85 RID: 3973
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SendConnectorSourceServersSetForEdgeException : LocalizedException
	{
		// Token: 0x0600AC77 RID: 44151 RVA: 0x002904A3 File Offset: 0x0028E6A3
		public SendConnectorSourceServersSetForEdgeException() : base(Strings.SendConnectorSourceServersSetForEdge)
		{
		}

		// Token: 0x0600AC78 RID: 44152 RVA: 0x002904B0 File Offset: 0x0028E6B0
		public SendConnectorSourceServersSetForEdgeException(Exception innerException) : base(Strings.SendConnectorSourceServersSetForEdge, innerException)
		{
		}

		// Token: 0x0600AC79 RID: 44153 RVA: 0x002904BE File Offset: 0x0028E6BE
		protected SendConnectorSourceServersSetForEdgeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AC7A RID: 44154 RVA: 0x002904C8 File Offset: 0x0028E6C8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
