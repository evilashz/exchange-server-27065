using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Entities.Calendaring
{
	// Token: 0x0200000E RID: 14
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CalendarNameAlreadyInUseException : ObjectExistedException
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00002A32 File Offset: 0x00000C32
		public CalendarNameAlreadyInUseException(string name) : base(CalendaringStrings.CalendarNameAlreadyInUse(name))
		{
			this.name = name;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002A47 File Offset: 0x00000C47
		public CalendarNameAlreadyInUseException(string name, Exception innerException) : base(CalendaringStrings.CalendarNameAlreadyInUse(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002A5D File Offset: 0x00000C5D
		protected CalendarNameAlreadyInUseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002A87 File Offset: 0x00000C87
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002AA2 File Offset: 0x00000CA2
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0400002F RID: 47
		private readonly string name;
	}
}
