using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000114 RID: 276
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidReceiveMeetingMessageCopiesException : StoragePermanentException
	{
		// Token: 0x060013F4 RID: 5108 RVA: 0x0006A07D File Offset: 0x0006827D
		public InvalidReceiveMeetingMessageCopiesException(string delegateUser) : base(ServerStrings.InvalidReceiveMeetingMessageCopiesException(delegateUser))
		{
			this.delegateUser = delegateUser;
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x0006A092 File Offset: 0x00068292
		public InvalidReceiveMeetingMessageCopiesException(string delegateUser, Exception innerException) : base(ServerStrings.InvalidReceiveMeetingMessageCopiesException(delegateUser), innerException)
		{
			this.delegateUser = delegateUser;
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x0006A0A8 File Offset: 0x000682A8
		protected InvalidReceiveMeetingMessageCopiesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.delegateUser = (string)info.GetValue("delegateUser", typeof(string));
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x0006A0D2 File Offset: 0x000682D2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("delegateUser", this.delegateUser);
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x060013F8 RID: 5112 RVA: 0x0006A0ED File Offset: 0x000682ED
		public string DelegateUser
		{
			get
			{
				return this.delegateUser;
			}
		}

		// Token: 0x040009A3 RID: 2467
		private readonly string delegateUser;
	}
}
