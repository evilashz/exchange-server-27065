using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000343 RID: 835
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MdbIsOfflineTransientException : MailboxReplicationTransientException
	{
		// Token: 0x06002607 RID: 9735 RVA: 0x000526EC File Offset: 0x000508EC
		public MdbIsOfflineTransientException(Guid mdbGuid) : base(MrsStrings.MdbIsOffline(mdbGuid))
		{
			this.mdbGuid = mdbGuid;
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x00052701 File Offset: 0x00050901
		public MdbIsOfflineTransientException(Guid mdbGuid, Exception innerException) : base(MrsStrings.MdbIsOffline(mdbGuid), innerException)
		{
			this.mdbGuid = mdbGuid;
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x00052717 File Offset: 0x00050917
		protected MdbIsOfflineTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mdbGuid = (Guid)info.GetValue("mdbGuid", typeof(Guid));
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x00052741 File Offset: 0x00050941
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mdbGuid", this.mdbGuid);
		}

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x0600260B RID: 9739 RVA: 0x00052761 File Offset: 0x00050961
		public Guid MdbGuid
		{
			get
			{
				return this.mdbGuid;
			}
		}

		// Token: 0x04001044 RID: 4164
		private readonly Guid mdbGuid;
	}
}
