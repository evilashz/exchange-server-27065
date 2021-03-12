using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002DD RID: 733
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WindowsLiveIDAddressIsMissingPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002411 RID: 9233 RVA: 0x0004F78E File Offset: 0x0004D98E
		public WindowsLiveIDAddressIsMissingPermanentException(string user) : base(MrsStrings.WindowsLiveIDAddressIsMissing(user))
		{
			this.user = user;
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x0004F7A3 File Offset: 0x0004D9A3
		public WindowsLiveIDAddressIsMissingPermanentException(string user, Exception innerException) : base(MrsStrings.WindowsLiveIDAddressIsMissing(user), innerException)
		{
			this.user = user;
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x0004F7B9 File Offset: 0x0004D9B9
		protected WindowsLiveIDAddressIsMissingPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.user = (string)info.GetValue("user", typeof(string));
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x0004F7E3 File Offset: 0x0004D9E3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("user", this.user);
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06002415 RID: 9237 RVA: 0x0004F7FE File Offset: 0x0004D9FE
		public string User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x04000FE6 RID: 4070
		private readonly string user;
	}
}
