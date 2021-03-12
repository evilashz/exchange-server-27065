using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F8D RID: 3981
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SendConnectorTargetServersNotSetException : LocalizedException
	{
		// Token: 0x0600AC9B RID: 44187 RVA: 0x0029073F File Offset: 0x0028E93F
		public SendConnectorTargetServersNotSetException() : base(Strings.SendConnectorTargetServersNotSet)
		{
		}

		// Token: 0x0600AC9C RID: 44188 RVA: 0x0029074C File Offset: 0x0028E94C
		public SendConnectorTargetServersNotSetException(Exception innerException) : base(Strings.SendConnectorTargetServersNotSet, innerException)
		{
		}

		// Token: 0x0600AC9D RID: 44189 RVA: 0x0029075A File Offset: 0x0028E95A
		protected SendConnectorTargetServersNotSetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AC9E RID: 44190 RVA: 0x00290764 File Offset: 0x0028E964
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
