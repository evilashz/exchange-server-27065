using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020002A1 RID: 673
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TaskArgumentException : LocalizedException
	{
		// Token: 0x0600189B RID: 6299 RVA: 0x0005C0B7 File Offset: 0x0005A2B7
		public TaskArgumentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x0005C0C0 File Offset: 0x0005A2C0
		public TaskArgumentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x0005C0CA File Offset: 0x0005A2CA
		protected TaskArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x0005C0D4 File Offset: 0x0005A2D4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
