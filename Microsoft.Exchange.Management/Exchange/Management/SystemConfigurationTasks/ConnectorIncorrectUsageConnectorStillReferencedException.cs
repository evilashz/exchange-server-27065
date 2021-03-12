using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F9F RID: 3999
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConnectorIncorrectUsageConnectorStillReferencedException : LocalizedException
	{
		// Token: 0x0600ACEB RID: 44267 RVA: 0x00290CD5 File Offset: 0x0028EED5
		public ConnectorIncorrectUsageConnectorStillReferencedException() : base(Strings.ConnectorIncorrectUsageConnectorStillReferenced)
		{
		}

		// Token: 0x0600ACEC RID: 44268 RVA: 0x00290CE2 File Offset: 0x0028EEE2
		public ConnectorIncorrectUsageConnectorStillReferencedException(Exception innerException) : base(Strings.ConnectorIncorrectUsageConnectorStillReferenced, innerException)
		{
		}

		// Token: 0x0600ACED RID: 44269 RVA: 0x00290CF0 File Offset: 0x0028EEF0
		protected ConnectorIncorrectUsageConnectorStillReferencedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ACEE RID: 44270 RVA: 0x00290CFA File Offset: 0x0028EEFA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
