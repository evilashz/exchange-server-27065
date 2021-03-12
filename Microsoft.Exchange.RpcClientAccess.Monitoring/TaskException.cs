using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000082 RID: 130
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class TaskException : LocalizedException
	{
		// Token: 0x060002BD RID: 701 RVA: 0x0000B70D File Offset: 0x0000990D
		public TaskException() : base(Strings.TaskExceptionMessage)
		{
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000B71A File Offset: 0x0000991A
		public TaskException(Exception innerException) : base(Strings.TaskExceptionMessage, innerException)
		{
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000B728 File Offset: 0x00009928
		protected TaskException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000B732 File Offset: 0x00009932
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
