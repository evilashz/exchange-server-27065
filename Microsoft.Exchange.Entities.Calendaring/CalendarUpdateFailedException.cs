using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Entities.Calendaring
{
	// Token: 0x0200000B RID: 11
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CalendarUpdateFailedException : StoragePermanentException
	{
		// Token: 0x06000045 RID: 69 RVA: 0x000029AD File Offset: 0x00000BAD
		public CalendarUpdateFailedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000029B6 File Offset: 0x00000BB6
		public CalendarUpdateFailedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000029C0 File Offset: 0x00000BC0
		protected CalendarUpdateFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000029CA File Offset: 0x00000BCA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
