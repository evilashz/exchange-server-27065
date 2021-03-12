using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA
{
	// Token: 0x0200002A RID: 42
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class OperationAbortedException : TransientException
	{
		// Token: 0x060001CA RID: 458 RVA: 0x000050C3 File Offset: 0x000032C3
		public OperationAbortedException() : base(Strings.OperationAborted)
		{
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000050D0 File Offset: 0x000032D0
		public OperationAbortedException(Exception innerException) : base(Strings.OperationAborted, innerException)
		{
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000050DE File Offset: 0x000032DE
		protected OperationAbortedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000050E8 File Offset: 0x000032E8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
