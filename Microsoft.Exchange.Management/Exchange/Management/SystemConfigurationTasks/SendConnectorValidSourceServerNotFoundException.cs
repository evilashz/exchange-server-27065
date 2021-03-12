using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F8E RID: 3982
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SendConnectorValidSourceServerNotFoundException : LocalizedException
	{
		// Token: 0x0600AC9F RID: 44191 RVA: 0x0029076E File Offset: 0x0028E96E
		public SendConnectorValidSourceServerNotFoundException() : base(Strings.SendConnectorValidSourceServerNotFound)
		{
		}

		// Token: 0x0600ACA0 RID: 44192 RVA: 0x0029077B File Offset: 0x0028E97B
		public SendConnectorValidSourceServerNotFoundException(Exception innerException) : base(Strings.SendConnectorValidSourceServerNotFound, innerException)
		{
		}

		// Token: 0x0600ACA1 RID: 44193 RVA: 0x00290789 File Offset: 0x0028E989
		protected SendConnectorValidSourceServerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ACA2 RID: 44194 RVA: 0x00290793 File Offset: 0x0028E993
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
