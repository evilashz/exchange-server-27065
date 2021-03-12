using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.ManagementGUI.Resources
{
	// Token: 0x02000017 RID: 23
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RootObjectNotFoundException : LocalizedException
	{
		// Token: 0x060010BF RID: 4287 RVA: 0x00037102 File Offset: 0x00035302
		public RootObjectNotFoundException() : base(Strings.RootObjectNotFound)
		{
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x0003710F File Offset: 0x0003530F
		public RootObjectNotFoundException(Exception innerException) : base(Strings.RootObjectNotFound, innerException)
		{
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0003711D File Offset: 0x0003531D
		protected RootObjectNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x00037127 File Offset: 0x00035327
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
