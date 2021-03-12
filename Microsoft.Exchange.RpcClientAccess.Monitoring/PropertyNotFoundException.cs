using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000083 RID: 131
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class PropertyNotFoundException : LocalizedException
	{
		// Token: 0x060002C1 RID: 705 RVA: 0x0000B73C File Offset: 0x0000993C
		public PropertyNotFoundException(string propertyName) : base(Strings.PropertyNotFoundExceptionMessage(propertyName))
		{
			this.propertyName = propertyName;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000B751 File Offset: 0x00009951
		public PropertyNotFoundException(string propertyName, Exception innerException) : base(Strings.PropertyNotFoundExceptionMessage(propertyName), innerException)
		{
			this.propertyName = propertyName;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000B767 File Offset: 0x00009967
		protected PropertyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.propertyName = (string)info.GetValue("propertyName", typeof(string));
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000B791 File Offset: 0x00009991
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("propertyName", this.propertyName);
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000B7AC File Offset: 0x000099AC
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x040001B4 RID: 436
		private readonly string propertyName;
	}
}
