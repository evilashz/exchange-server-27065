using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.PushNotifications.Server.LocStrings;

namespace Microsoft.Exchange.PushNotifications.Server
{
	// Token: 0x02000033 RID: 51
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServiceBusyException : PushNotificationTransientException
	{
		// Token: 0x06000128 RID: 296 RVA: 0x00004ABF File Offset: 0x00002CBF
		public ServiceBusyException(string command) : base(Strings.ServiceBusy(command))
		{
			this.command = command;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00004AD4 File Offset: 0x00002CD4
		public ServiceBusyException(string command, Exception innerException) : base(Strings.ServiceBusy(command), innerException)
		{
			this.command = command;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00004AEA File Offset: 0x00002CEA
		protected ServiceBusyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.command = (string)info.GetValue("command", typeof(string));
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004B14 File Offset: 0x00002D14
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("command", this.command);
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00004B2F File Offset: 0x00002D2F
		public string Command
		{
			get
			{
				return this.command;
			}
		}

		// Token: 0x0400006B RID: 107
		private readonly string command;
	}
}
