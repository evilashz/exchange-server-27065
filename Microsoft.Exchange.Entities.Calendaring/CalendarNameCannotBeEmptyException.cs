using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Entities.DataProviders;

namespace Microsoft.Exchange.Entities.Calendaring
{
	// Token: 0x02000016 RID: 22
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CalendarNameCannotBeEmptyException : InvalidRequestException
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00002ED0 File Offset: 0x000010D0
		public CalendarNameCannotBeEmptyException() : base(CalendaringStrings.CalendarNameCannotBeEmpty)
		{
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002EDD File Offset: 0x000010DD
		public CalendarNameCannotBeEmptyException(Exception innerException) : base(CalendaringStrings.CalendarNameCannotBeEmpty, innerException)
		{
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002EEB File Offset: 0x000010EB
		protected CalendarNameCannotBeEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002EF5 File Offset: 0x000010F5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
