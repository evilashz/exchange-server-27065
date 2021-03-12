using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Entities.Calendaring
{
	// Token: 0x0200000C RID: 12
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CalendarFolderUpdateFailedException : CalendarUpdateFailedException
	{
		// Token: 0x06000049 RID: 73 RVA: 0x000029D4 File Offset: 0x00000BD4
		public CalendarFolderUpdateFailedException() : base(CalendaringStrings.CalendarFolderUpdateFailed)
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000029E1 File Offset: 0x00000BE1
		public CalendarFolderUpdateFailedException(Exception innerException) : base(CalendaringStrings.CalendarFolderUpdateFailed, innerException)
		{
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000029EF File Offset: 0x00000BEF
		protected CalendarFolderUpdateFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000029F9 File Offset: 0x00000BF9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
