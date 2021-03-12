using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001070 RID: 4208
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagNetworkInconsistentRoleException : LocalizedException
	{
		// Token: 0x0600B112 RID: 45330 RVA: 0x0029768E File Offset: 0x0029588E
		public DagNetworkInconsistentRoleException() : base(Strings.DagNetworkInconsistentRoleException)
		{
		}

		// Token: 0x0600B113 RID: 45331 RVA: 0x0029769B File Offset: 0x0029589B
		public DagNetworkInconsistentRoleException(Exception innerException) : base(Strings.DagNetworkInconsistentRoleException, innerException)
		{
		}

		// Token: 0x0600B114 RID: 45332 RVA: 0x002976A9 File Offset: 0x002958A9
		protected DagNetworkInconsistentRoleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B115 RID: 45333 RVA: 0x002976B3 File Offset: 0x002958B3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
