using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000118 RID: 280
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DefaultCalendarGroupCreationException : StorageTransientException
	{
		// Token: 0x06001408 RID: 5128 RVA: 0x0006A2A7 File Offset: 0x000684A7
		public DefaultCalendarGroupCreationException(string calendarType) : base(ServerStrings.idUnableToCreateDefaultCalendarGroupException(calendarType))
		{
			this.calendarType = calendarType;
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x0006A2BC File Offset: 0x000684BC
		public DefaultCalendarGroupCreationException(string calendarType, Exception innerException) : base(ServerStrings.idUnableToCreateDefaultCalendarGroupException(calendarType), innerException)
		{
			this.calendarType = calendarType;
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x0006A2D2 File Offset: 0x000684D2
		protected DefaultCalendarGroupCreationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.calendarType = (string)info.GetValue("calendarType", typeof(string));
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0006A2FC File Offset: 0x000684FC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("calendarType", this.calendarType);
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x0006A317 File Offset: 0x00068517
		public string CalendarType
		{
			get
			{
				return this.calendarType;
			}
		}

		// Token: 0x040009A8 RID: 2472
		private readonly string calendarType;
	}
}
