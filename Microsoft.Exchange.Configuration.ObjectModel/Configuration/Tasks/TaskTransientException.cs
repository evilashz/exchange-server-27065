using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020002B8 RID: 696
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TaskTransientException : TaskException
	{
		// Token: 0x0600190B RID: 6411 RVA: 0x0005CAC0 File Offset: 0x0005ACC0
		public TaskTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x0005CAC9 File Offset: 0x0005ACC9
		public TaskTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x0005CAD3 File Offset: 0x0005ACD3
		protected TaskTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x0005CADD File Offset: 0x0005ACDD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
