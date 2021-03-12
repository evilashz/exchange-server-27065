using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore.OCS
{
	// Token: 0x020001FC RID: 508
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NotificationEventSignalingException : NotificationEventException
	{
		// Token: 0x060010B1 RID: 4273 RVA: 0x0003925E File Offset: 0x0003745E
		public NotificationEventSignalingException(string msg) : base(Strings.NotificationEventSignalingException(msg))
		{
			this.msg = msg;
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00039273 File Offset: 0x00037473
		public NotificationEventSignalingException(string msg, Exception innerException) : base(Strings.NotificationEventSignalingException(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x00039289 File Offset: 0x00037489
		protected NotificationEventSignalingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x000392B3 File Offset: 0x000374B3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x060010B5 RID: 4277 RVA: 0x000392CE File Offset: 0x000374CE
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x0400087F RID: 2175
		private readonly string msg;
	}
}
