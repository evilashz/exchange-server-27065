using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200031D RID: 797
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RpcClientAccessServerNotConfiguredForMdbPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002548 RID: 9544 RVA: 0x000513F2 File Offset: 0x0004F5F2
		public RpcClientAccessServerNotConfiguredForMdbPermanentException(string mdbID) : base(MrsStrings.RpcClientAccessServerNotConfiguredForMdb(mdbID))
		{
			this.mdbID = mdbID;
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x00051407 File Offset: 0x0004F607
		public RpcClientAccessServerNotConfiguredForMdbPermanentException(string mdbID, Exception innerException) : base(MrsStrings.RpcClientAccessServerNotConfiguredForMdb(mdbID), innerException)
		{
			this.mdbID = mdbID;
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x0005141D File Offset: 0x0004F61D
		protected RpcClientAccessServerNotConfiguredForMdbPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mdbID = (string)info.GetValue("mdbID", typeof(string));
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x00051447 File Offset: 0x0004F647
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mdbID", this.mdbID);
		}

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x0600254C RID: 9548 RVA: 0x00051462 File Offset: 0x0004F662
		public string MdbID
		{
			get
			{
				return this.mdbID;
			}
		}

		// Token: 0x0400101D RID: 4125
		private readonly string mdbID;
	}
}
