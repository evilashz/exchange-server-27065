using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F94 RID: 3988
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SendConnectorNonE12SourceAndTargetServersException : LocalizedException
	{
		// Token: 0x0600ACBB RID: 44219 RVA: 0x002909AC File Offset: 0x0028EBAC
		public SendConnectorNonE12SourceAndTargetServersException() : base(Strings.SendConnectorNonE12SourceAndTargetServers)
		{
		}

		// Token: 0x0600ACBC RID: 44220 RVA: 0x002909B9 File Offset: 0x0028EBB9
		public SendConnectorNonE12SourceAndTargetServersException(Exception innerException) : base(Strings.SendConnectorNonE12SourceAndTargetServers, innerException)
		{
		}

		// Token: 0x0600ACBD RID: 44221 RVA: 0x002909C7 File Offset: 0x0028EBC7
		protected SendConnectorNonE12SourceAndTargetServersException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ACBE RID: 44222 RVA: 0x002909D1 File Offset: 0x0028EBD1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
