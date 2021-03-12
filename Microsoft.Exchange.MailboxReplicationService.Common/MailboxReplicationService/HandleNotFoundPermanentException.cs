using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002E0 RID: 736
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class HandleNotFoundPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002420 RID: 9248 RVA: 0x0004F904 File Offset: 0x0004DB04
		public HandleNotFoundPermanentException(long handle) : base(MrsStrings.HandleNotFound(handle))
		{
			this.handle = handle;
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x0004F919 File Offset: 0x0004DB19
		public HandleNotFoundPermanentException(long handle, Exception innerException) : base(MrsStrings.HandleNotFound(handle), innerException)
		{
			this.handle = handle;
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x0004F92F File Offset: 0x0004DB2F
		protected HandleNotFoundPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.handle = (long)info.GetValue("handle", typeof(long));
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x0004F959 File Offset: 0x0004DB59
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("handle", this.handle);
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06002424 RID: 9252 RVA: 0x0004F974 File Offset: 0x0004DB74
		public long Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x04000FE9 RID: 4073
		private readonly long handle;
	}
}
