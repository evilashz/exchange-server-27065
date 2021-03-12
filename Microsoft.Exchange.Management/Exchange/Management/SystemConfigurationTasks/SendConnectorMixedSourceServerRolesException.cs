using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F89 RID: 3977
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SendConnectorMixedSourceServerRolesException : LocalizedException
	{
		// Token: 0x0600AC89 RID: 44169 RVA: 0x002905F1 File Offset: 0x0028E7F1
		public SendConnectorMixedSourceServerRolesException() : base(Strings.SendConnectorMixedSourceServerRoles)
		{
		}

		// Token: 0x0600AC8A RID: 44170 RVA: 0x002905FE File Offset: 0x0028E7FE
		public SendConnectorMixedSourceServerRolesException(Exception innerException) : base(Strings.SendConnectorMixedSourceServerRoles, innerException)
		{
		}

		// Token: 0x0600AC8B RID: 44171 RVA: 0x0029060C File Offset: 0x0028E80C
		protected SendConnectorMixedSourceServerRolesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AC8C RID: 44172 RVA: 0x00290616 File Offset: 0x0028E816
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
