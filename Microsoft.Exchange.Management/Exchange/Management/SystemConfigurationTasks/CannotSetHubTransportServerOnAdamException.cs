using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F6F RID: 3951
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotSetHubTransportServerOnAdamException : LocalizedException
	{
		// Token: 0x0600AC18 RID: 44056 RVA: 0x0028FE8F File Offset: 0x0028E08F
		public CannotSetHubTransportServerOnAdamException() : base(Strings.CannotSetHubTransportServerOnAdam)
		{
		}

		// Token: 0x0600AC19 RID: 44057 RVA: 0x0028FE9C File Offset: 0x0028E09C
		public CannotSetHubTransportServerOnAdamException(Exception innerException) : base(Strings.CannotSetHubTransportServerOnAdam, innerException)
		{
		}

		// Token: 0x0600AC1A RID: 44058 RVA: 0x0028FEAA File Offset: 0x0028E0AA
		protected CannotSetHubTransportServerOnAdamException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AC1B RID: 44059 RVA: 0x0028FEB4 File Offset: 0x0028E0B4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
