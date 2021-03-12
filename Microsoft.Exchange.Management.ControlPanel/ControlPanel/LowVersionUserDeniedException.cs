using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Authorization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000010 RID: 16
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class LowVersionUserDeniedException : AuthorizationException
	{
		// Token: 0x06001861 RID: 6241 RVA: 0x0004B702 File Offset: 0x00049902
		public LowVersionUserDeniedException() : base(Strings.LowVersionUser)
		{
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x0004B70F File Offset: 0x0004990F
		public LowVersionUserDeniedException(Exception innerException) : base(Strings.LowVersionUser, innerException)
		{
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x0004B71D File Offset: 0x0004991D
		protected LowVersionUserDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x0004B727 File Offset: 0x00049927
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
