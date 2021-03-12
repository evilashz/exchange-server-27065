using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002FF RID: 767
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServerNotFoundByGuidPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060024B4 RID: 9396 RVA: 0x000505E9 File Offset: 0x0004E7E9
		public ServerNotFoundByGuidPermanentException(Guid serverGuid) : base(MrsStrings.ServerNotFoundByGuid(serverGuid))
		{
			this.serverGuid = serverGuid;
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x000505FE File Offset: 0x0004E7FE
		public ServerNotFoundByGuidPermanentException(Guid serverGuid, Exception innerException) : base(MrsStrings.ServerNotFoundByGuid(serverGuid), innerException)
		{
			this.serverGuid = serverGuid;
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x00050614 File Offset: 0x0004E814
		protected ServerNotFoundByGuidPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverGuid = (Guid)info.GetValue("serverGuid", typeof(Guid));
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x0005063E File Offset: 0x0004E83E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverGuid", this.serverGuid);
		}

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x060024B8 RID: 9400 RVA: 0x0005065E File Offset: 0x0004E85E
		public Guid ServerGuid
		{
			get
			{
				return this.serverGuid;
			}
		}

		// Token: 0x04001001 RID: 4097
		private readonly Guid serverGuid;
	}
}
