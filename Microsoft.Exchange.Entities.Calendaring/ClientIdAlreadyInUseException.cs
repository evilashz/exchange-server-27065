using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Entities.DataProviders;

namespace Microsoft.Exchange.Entities.Calendaring
{
	// Token: 0x02000008 RID: 8
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ClientIdAlreadyInUseException : InvalidRequestException
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002920 File Offset: 0x00000B20
		public ClientIdAlreadyInUseException() : base(CalendaringStrings.ClientIdAlreadyInUse)
		{
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000292D File Offset: 0x00000B2D
		public ClientIdAlreadyInUseException(Exception innerException) : base(CalendaringStrings.ClientIdAlreadyInUse, innerException)
		{
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000293B File Offset: 0x00000B3B
		protected ClientIdAlreadyInUseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002945 File Offset: 0x00000B45
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
