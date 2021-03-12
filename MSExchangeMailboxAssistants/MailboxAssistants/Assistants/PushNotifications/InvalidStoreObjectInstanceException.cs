using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PushNotifications
{
	// Token: 0x02000149 RID: 329
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidStoreObjectInstanceException : PushNotificationBaseException
	{
		// Token: 0x06000D3E RID: 3390 RVA: 0x000520EE File Offset: 0x000502EE
		public InvalidStoreObjectInstanceException(Type actualInstance) : base(Strings.descInvalidStoreObjectInstance(actualInstance))
		{
			this.actualInstance = actualInstance;
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00052103 File Offset: 0x00050303
		public InvalidStoreObjectInstanceException(Type actualInstance, Exception innerException) : base(Strings.descInvalidStoreObjectInstance(actualInstance), innerException)
		{
			this.actualInstance = actualInstance;
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x00052119 File Offset: 0x00050319
		protected InvalidStoreObjectInstanceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.actualInstance = (Type)info.GetValue("actualInstance", typeof(Type));
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00052143 File Offset: 0x00050343
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("actualInstance", this.actualInstance);
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x0005215E File Offset: 0x0005035E
		public Type ActualInstance
		{
			get
			{
				return this.actualInstance;
			}
		}

		// Token: 0x04000840 RID: 2112
		private readonly Type actualInstance;
	}
}
