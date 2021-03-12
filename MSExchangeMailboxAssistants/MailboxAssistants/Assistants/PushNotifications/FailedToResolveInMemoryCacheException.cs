using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PushNotifications
{
	// Token: 0x0200014A RID: 330
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FailedToResolveInMemoryCacheException : PushNotificationBaseException
	{
		// Token: 0x06000D43 RID: 3395 RVA: 0x00052166 File Offset: 0x00050366
		public FailedToResolveInMemoryCacheException(Guid mailboxInstance) : base(Strings.descFailedToResolveInMemoryCache(mailboxInstance))
		{
			this.mailboxInstance = mailboxInstance;
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x0005217B File Offset: 0x0005037B
		public FailedToResolveInMemoryCacheException(Guid mailboxInstance, Exception innerException) : base(Strings.descFailedToResolveInMemoryCache(mailboxInstance), innerException)
		{
			this.mailboxInstance = mailboxInstance;
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x00052191 File Offset: 0x00050391
		protected FailedToResolveInMemoryCacheException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailboxInstance = (Guid)info.GetValue("mailboxInstance", typeof(Guid));
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x000521BB File Offset: 0x000503BB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailboxInstance", this.mailboxInstance);
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x000521DB File Offset: 0x000503DB
		public Guid MailboxInstance
		{
			get
			{
				return this.mailboxInstance;
			}
		}

		// Token: 0x04000841 RID: 2113
		private readonly Guid mailboxInstance;
	}
}
