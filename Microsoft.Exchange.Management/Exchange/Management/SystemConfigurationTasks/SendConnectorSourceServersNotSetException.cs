using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F86 RID: 3974
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SendConnectorSourceServersNotSetException : LocalizedException
	{
		// Token: 0x0600AC7B RID: 44155 RVA: 0x002904D2 File Offset: 0x0028E6D2
		public SendConnectorSourceServersNotSetException() : base(Strings.SendConnectorSourceServersNotSet)
		{
		}

		// Token: 0x0600AC7C RID: 44156 RVA: 0x002904DF File Offset: 0x0028E6DF
		public SendConnectorSourceServersNotSetException(Exception innerException) : base(Strings.SendConnectorSourceServersNotSet, innerException)
		{
		}

		// Token: 0x0600AC7D RID: 44157 RVA: 0x002904ED File Offset: 0x0028E6ED
		protected SendConnectorSourceServersNotSetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AC7E RID: 44158 RVA: 0x002904F7 File Offset: 0x0028E6F7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
