using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000221 RID: 545
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MailboxUnavailableException : TransientException
	{
		// Token: 0x0600115B RID: 4443 RVA: 0x00039F8B File Offset: 0x0003818B
		public MailboxUnavailableException(string messageType, string database, string exceptionMessage) : base(Strings.MailboxUnavailableException(messageType, database, exceptionMessage))
		{
			this.messageType = messageType;
			this.database = database;
			this.exceptionMessage = exceptionMessage;
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x00039FB0 File Offset: 0x000381B0
		public MailboxUnavailableException(string messageType, string database, string exceptionMessage, Exception innerException) : base(Strings.MailboxUnavailableException(messageType, database, exceptionMessage), innerException)
		{
			this.messageType = messageType;
			this.database = database;
			this.exceptionMessage = exceptionMessage;
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x00039FD8 File Offset: 0x000381D8
		protected MailboxUnavailableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.messageType = (string)info.GetValue("messageType", typeof(string));
			this.database = (string)info.GetValue("database", typeof(string));
			this.exceptionMessage = (string)info.GetValue("exceptionMessage", typeof(string));
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x0003A04D File Offset: 0x0003824D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("messageType", this.messageType);
			info.AddValue("database", this.database);
			info.AddValue("exceptionMessage", this.exceptionMessage);
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x0003A08A File Offset: 0x0003828A
		public string MessageType
		{
			get
			{
				return this.messageType;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x0003A092 File Offset: 0x00038292
		public string Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x0003A09A File Offset: 0x0003829A
		public string ExceptionMessage
		{
			get
			{
				return this.exceptionMessage;
			}
		}

		// Token: 0x04000895 RID: 2197
		private readonly string messageType;

		// Token: 0x04000896 RID: 2198
		private readonly string database;

		// Token: 0x04000897 RID: 2199
		private readonly string exceptionMessage;
	}
}
