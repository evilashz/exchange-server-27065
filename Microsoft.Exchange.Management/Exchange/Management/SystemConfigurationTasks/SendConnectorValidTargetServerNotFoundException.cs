using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F8F RID: 3983
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SendConnectorValidTargetServerNotFoundException : LocalizedException
	{
		// Token: 0x0600ACA3 RID: 44195 RVA: 0x0029079D File Offset: 0x0028E99D
		public SendConnectorValidTargetServerNotFoundException() : base(Strings.SendConnectorValidTargetServerNotFound)
		{
		}

		// Token: 0x0600ACA4 RID: 44196 RVA: 0x002907AA File Offset: 0x0028E9AA
		public SendConnectorValidTargetServerNotFoundException(Exception innerException) : base(Strings.SendConnectorValidTargetServerNotFound, innerException)
		{
		}

		// Token: 0x0600ACA5 RID: 44197 RVA: 0x002907B8 File Offset: 0x0028E9B8
		protected SendConnectorValidTargetServerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ACA6 RID: 44198 RVA: 0x002907C2 File Offset: 0x0028E9C2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
