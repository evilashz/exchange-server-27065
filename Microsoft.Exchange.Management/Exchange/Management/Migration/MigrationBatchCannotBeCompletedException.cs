using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x0200112A RID: 4394
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MigrationBatchCannotBeCompletedException : LocalizedException
	{
		// Token: 0x0600B4C2 RID: 46274 RVA: 0x0029D4E0 File Offset: 0x0029B6E0
		public MigrationBatchCannotBeCompletedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600B4C3 RID: 46275 RVA: 0x0029D4E9 File Offset: 0x0029B6E9
		public MigrationBatchCannotBeCompletedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600B4C4 RID: 46276 RVA: 0x0029D4F3 File Offset: 0x0029B6F3
		protected MigrationBatchCannotBeCompletedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B4C5 RID: 46277 RVA: 0x0029D4FD File Offset: 0x0029B6FD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
