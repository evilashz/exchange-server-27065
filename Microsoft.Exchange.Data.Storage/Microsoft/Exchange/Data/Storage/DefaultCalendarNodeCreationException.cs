using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000119 RID: 281
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DefaultCalendarNodeCreationException : StorageTransientException
	{
		// Token: 0x0600140D RID: 5133 RVA: 0x0006A31F File Offset: 0x0006851F
		public DefaultCalendarNodeCreationException() : base(ServerStrings.idUnableToAddDefaultCalendarToDefaultCalendarGroup)
		{
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x0006A32C File Offset: 0x0006852C
		public DefaultCalendarNodeCreationException(Exception innerException) : base(ServerStrings.idUnableToAddDefaultCalendarToDefaultCalendarGroup, innerException)
		{
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x0006A33A File Offset: 0x0006853A
		protected DefaultCalendarNodeCreationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0006A344 File Offset: 0x00068544
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
