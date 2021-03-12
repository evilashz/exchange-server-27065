using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020002A0 RID: 672
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TaskInvalidOperationException : LocalizedException
	{
		// Token: 0x06001897 RID: 6295 RVA: 0x0005C090 File Offset: 0x0005A290
		public TaskInvalidOperationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x0005C099 File Offset: 0x0005A299
		public TaskInvalidOperationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x0005C0A3 File Offset: 0x0005A2A3
		protected TaskInvalidOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x0005C0AD File Offset: 0x0005A2AD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
