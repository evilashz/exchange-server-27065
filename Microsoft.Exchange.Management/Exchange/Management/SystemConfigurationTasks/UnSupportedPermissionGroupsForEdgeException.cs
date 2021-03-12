using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F75 RID: 3957
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnSupportedPermissionGroupsForEdgeException : LocalizedException
	{
		// Token: 0x0600AC35 RID: 44085 RVA: 0x00290121 File Offset: 0x0028E321
		public UnSupportedPermissionGroupsForEdgeException() : base(Strings.UnSupportedPermissionGroupsForEdge)
		{
		}

		// Token: 0x0600AC36 RID: 44086 RVA: 0x0029012E File Offset: 0x0028E32E
		public UnSupportedPermissionGroupsForEdgeException(Exception innerException) : base(Strings.UnSupportedPermissionGroupsForEdge, innerException)
		{
		}

		// Token: 0x0600AC37 RID: 44087 RVA: 0x0029013C File Offset: 0x0028E33C
		protected UnSupportedPermissionGroupsForEdgeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AC38 RID: 44088 RVA: 0x00290146 File Offset: 0x0028E346
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
