using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F8C RID: 3980
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SendConnectorInvalidSourceIPAddressException : LocalizedException
	{
		// Token: 0x0600AC97 RID: 44183 RVA: 0x00290710 File Offset: 0x0028E910
		public SendConnectorInvalidSourceIPAddressException() : base(Strings.SendConnectorInvalidSourceIPAddress)
		{
		}

		// Token: 0x0600AC98 RID: 44184 RVA: 0x0029071D File Offset: 0x0028E91D
		public SendConnectorInvalidSourceIPAddressException(Exception innerException) : base(Strings.SendConnectorInvalidSourceIPAddress, innerException)
		{
		}

		// Token: 0x0600AC99 RID: 44185 RVA: 0x0029072B File Offset: 0x0028E92B
		protected SendConnectorInvalidSourceIPAddressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AC9A RID: 44186 RVA: 0x00290735 File Offset: 0x0028E935
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
