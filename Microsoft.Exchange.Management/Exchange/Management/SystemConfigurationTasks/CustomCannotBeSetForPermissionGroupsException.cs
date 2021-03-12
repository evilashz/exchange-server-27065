using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F78 RID: 3960
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CustomCannotBeSetForPermissionGroupsException : LocalizedException
	{
		// Token: 0x0600AC41 RID: 44097 RVA: 0x002901AE File Offset: 0x0028E3AE
		public CustomCannotBeSetForPermissionGroupsException() : base(Strings.CustomCannotBeUsedForPermissionGroups)
		{
		}

		// Token: 0x0600AC42 RID: 44098 RVA: 0x002901BB File Offset: 0x0028E3BB
		public CustomCannotBeSetForPermissionGroupsException(Exception innerException) : base(Strings.CustomCannotBeUsedForPermissionGroups, innerException)
		{
		}

		// Token: 0x0600AC43 RID: 44099 RVA: 0x002901C9 File Offset: 0x0028E3C9
		protected CustomCannotBeSetForPermissionGroupsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AC44 RID: 44100 RVA: 0x002901D3 File Offset: 0x0028E3D3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
