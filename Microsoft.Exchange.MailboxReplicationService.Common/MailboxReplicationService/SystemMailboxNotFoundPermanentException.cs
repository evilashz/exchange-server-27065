using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002AB RID: 683
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SystemMailboxNotFoundPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002314 RID: 8980 RVA: 0x0004DE39 File Offset: 0x0004C039
		public SystemMailboxNotFoundPermanentException(string systemMailboxName) : base(MrsStrings.SystemMailboxNotFound(systemMailboxName))
		{
			this.systemMailboxName = systemMailboxName;
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x0004DE4E File Offset: 0x0004C04E
		public SystemMailboxNotFoundPermanentException(string systemMailboxName, Exception innerException) : base(MrsStrings.SystemMailboxNotFound(systemMailboxName), innerException)
		{
			this.systemMailboxName = systemMailboxName;
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x0004DE64 File Offset: 0x0004C064
		protected SystemMailboxNotFoundPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.systemMailboxName = (string)info.GetValue("systemMailboxName", typeof(string));
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x0004DE8E File Offset: 0x0004C08E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("systemMailboxName", this.systemMailboxName);
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06002318 RID: 8984 RVA: 0x0004DEA9 File Offset: 0x0004C0A9
		public string SystemMailboxName
		{
			get
			{
				return this.systemMailboxName;
			}
		}

		// Token: 0x04000FB1 RID: 4017
		private readonly string systemMailboxName;
	}
}
