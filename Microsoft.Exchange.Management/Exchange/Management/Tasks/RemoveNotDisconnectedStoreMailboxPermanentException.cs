using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EF5 RID: 3829
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RemoveNotDisconnectedStoreMailboxPermanentException : LocalizedException
	{
		// Token: 0x0600A9B1 RID: 43441 RVA: 0x0028C29E File Offset: 0x0028A49E
		public RemoveNotDisconnectedStoreMailboxPermanentException(string identity) : base(Strings.ErrorRemoveNotDisconnectedStoreMailbox(identity))
		{
			this.identity = identity;
		}

		// Token: 0x0600A9B2 RID: 43442 RVA: 0x0028C2B3 File Offset: 0x0028A4B3
		public RemoveNotDisconnectedStoreMailboxPermanentException(string identity, Exception innerException) : base(Strings.ErrorRemoveNotDisconnectedStoreMailbox(identity), innerException)
		{
			this.identity = identity;
		}

		// Token: 0x0600A9B3 RID: 43443 RVA: 0x0028C2C9 File Offset: 0x0028A4C9
		protected RemoveNotDisconnectedStoreMailboxPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
		}

		// Token: 0x0600A9B4 RID: 43444 RVA: 0x0028C2F3 File Offset: 0x0028A4F3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x170036FA RID: 14074
		// (get) Token: 0x0600A9B5 RID: 43445 RVA: 0x0028C30E File Offset: 0x0028A50E
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x04006060 RID: 24672
		private readonly string identity;
	}
}
