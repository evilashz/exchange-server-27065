using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000327 RID: 807
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnsupportedRehomeTargetVersionPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002576 RID: 9590 RVA: 0x0005178C File Offset: 0x0004F98C
		public UnsupportedRehomeTargetVersionPermanentException(string mdbID, string serverVersion) : base(MrsStrings.MustRehomeRequestToSupportedVersion(mdbID, serverVersion))
		{
			this.mdbID = mdbID;
			this.serverVersion = serverVersion;
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x000517A9 File Offset: 0x0004F9A9
		public UnsupportedRehomeTargetVersionPermanentException(string mdbID, string serverVersion, Exception innerException) : base(MrsStrings.MustRehomeRequestToSupportedVersion(mdbID, serverVersion), innerException)
		{
			this.mdbID = mdbID;
			this.serverVersion = serverVersion;
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x000517C8 File Offset: 0x0004F9C8
		protected UnsupportedRehomeTargetVersionPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mdbID = (string)info.GetValue("mdbID", typeof(string));
			this.serverVersion = (string)info.GetValue("serverVersion", typeof(string));
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x0005181D File Offset: 0x0004FA1D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mdbID", this.mdbID);
			info.AddValue("serverVersion", this.serverVersion);
		}

		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x0600257A RID: 9594 RVA: 0x00051849 File Offset: 0x0004FA49
		public string MdbID
		{
			get
			{
				return this.mdbID;
			}
		}

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x0600257B RID: 9595 RVA: 0x00051851 File Offset: 0x0004FA51
		public string ServerVersion
		{
			get
			{
				return this.serverVersion;
			}
		}

		// Token: 0x04001023 RID: 4131
		private readonly string mdbID;

		// Token: 0x04001024 RID: 4132
		private readonly string serverVersion;
	}
}
