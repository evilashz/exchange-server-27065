using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200116D RID: 4461
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotRunOnNonHubTransportException : LocalizedException
	{
		// Token: 0x0600B60C RID: 46604 RVA: 0x0029F339 File Offset: 0x0029D539
		public CannotRunOnNonHubTransportException() : base(Strings.CannotRunOnNonHubTransport)
		{
		}

		// Token: 0x0600B60D RID: 46605 RVA: 0x0029F346 File Offset: 0x0029D546
		public CannotRunOnNonHubTransportException(Exception innerException) : base(Strings.CannotRunOnNonHubTransport, innerException)
		{
		}

		// Token: 0x0600B60E RID: 46606 RVA: 0x0029F354 File Offset: 0x0029D554
		protected CannotRunOnNonHubTransportException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B60F RID: 46607 RVA: 0x0029F35E File Offset: 0x0029D55E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
