using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003A8 RID: 936
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TPRNotEnabledException : TransientException
	{
		// Token: 0x0600279A RID: 10138 RVA: 0x000B6449 File Offset: 0x000B4649
		public TPRNotEnabledException() : base(ReplayStrings.TPRNotEnabled)
		{
		}

		// Token: 0x0600279B RID: 10139 RVA: 0x000B6456 File Offset: 0x000B4656
		public TPRNotEnabledException(Exception innerException) : base(ReplayStrings.TPRNotEnabled, innerException)
		{
		}

		// Token: 0x0600279C RID: 10140 RVA: 0x000B6464 File Offset: 0x000B4664
		protected TPRNotEnabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600279D RID: 10141 RVA: 0x000B646E File Offset: 0x000B466E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
