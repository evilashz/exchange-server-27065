using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.PushNotifications.Server.LocStrings;

namespace Microsoft.Exchange.PushNotifications.Server
{
	// Token: 0x02000034 RID: 52
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OperationCancelledException : PushNotificationTransientException
	{
		// Token: 0x0600012D RID: 301 RVA: 0x00004B37 File Offset: 0x00002D37
		public OperationCancelledException(string command) : base(Strings.OperationCancelled(command))
		{
			this.command = command;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004B4C File Offset: 0x00002D4C
		public OperationCancelledException(string command, Exception innerException) : base(Strings.OperationCancelled(command), innerException)
		{
			this.command = command;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004B62 File Offset: 0x00002D62
		protected OperationCancelledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.command = (string)info.GetValue("command", typeof(string));
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004B8C File Offset: 0x00002D8C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("command", this.command);
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00004BA7 File Offset: 0x00002DA7
		public string Command
		{
			get
			{
				return this.command;
			}
		}

		// Token: 0x0400006C RID: 108
		private readonly string command;
	}
}
