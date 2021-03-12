using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020002B7 RID: 695
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TaskException : LocalizedException
	{
		// Token: 0x06001907 RID: 6407 RVA: 0x0005CA99 File Offset: 0x0005AC99
		public TaskException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x0005CAA2 File Offset: 0x0005ACA2
		public TaskException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x0005CAAC File Offset: 0x0005ACAC
		protected TaskException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x0005CAB6 File Offset: 0x0005ACB6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
