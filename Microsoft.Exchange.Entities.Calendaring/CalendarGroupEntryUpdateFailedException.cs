using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Entities.Calendaring
{
	// Token: 0x0200000D RID: 13
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CalendarGroupEntryUpdateFailedException : CalendarUpdateFailedException
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002A03 File Offset: 0x00000C03
		public CalendarGroupEntryUpdateFailedException() : base(CalendaringStrings.CalendarGroupEntryUpdateFailed)
		{
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002A10 File Offset: 0x00000C10
		public CalendarGroupEntryUpdateFailedException(Exception innerException) : base(CalendaringStrings.CalendarGroupEntryUpdateFailed, innerException)
		{
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002A1E File Offset: 0x00000C1E
		protected CalendarGroupEntryUpdateFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002A28 File Offset: 0x00000C28
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
