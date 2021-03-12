using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000370 RID: 880
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnsupportedImapMessageEntryIdVersionException : MailboxReplicationPermanentException
	{
		// Token: 0x060026EA RID: 9962 RVA: 0x00053D35 File Offset: 0x00051F35
		public UnsupportedImapMessageEntryIdVersionException(string version) : base(MrsStrings.UnsupportedImapMessageEntryIdVersion(version))
		{
			this.version = version;
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x00053D4A File Offset: 0x00051F4A
		public UnsupportedImapMessageEntryIdVersionException(string version, Exception innerException) : base(MrsStrings.UnsupportedImapMessageEntryIdVersion(version), innerException)
		{
			this.version = version;
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x00053D60 File Offset: 0x00051F60
		protected UnsupportedImapMessageEntryIdVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.version = (string)info.GetValue("version", typeof(string));
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x00053D8A File Offset: 0x00051F8A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("version", this.version);
		}

		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x060026EE RID: 9966 RVA: 0x00053DA5 File Offset: 0x00051FA5
		public string Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x04001073 RID: 4211
		private readonly string version;
	}
}
