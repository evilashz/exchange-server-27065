using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200116E RID: 4462
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SystemMailboxNotFoundException : LocalizedException
	{
		// Token: 0x0600B610 RID: 46608 RVA: 0x0029F368 File Offset: 0x0029D568
		public SystemMailboxNotFoundException(string systemMailbox, string database) : base(Strings.messageSystemMailboxNotFoundException(systemMailbox, database))
		{
			this.systemMailbox = systemMailbox;
			this.database = database;
		}

		// Token: 0x0600B611 RID: 46609 RVA: 0x0029F385 File Offset: 0x0029D585
		public SystemMailboxNotFoundException(string systemMailbox, string database, Exception innerException) : base(Strings.messageSystemMailboxNotFoundException(systemMailbox, database), innerException)
		{
			this.systemMailbox = systemMailbox;
			this.database = database;
		}

		// Token: 0x0600B612 RID: 46610 RVA: 0x0029F3A4 File Offset: 0x0029D5A4
		protected SystemMailboxNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.systemMailbox = (string)info.GetValue("systemMailbox", typeof(string));
			this.database = (string)info.GetValue("database", typeof(string));
		}

		// Token: 0x0600B613 RID: 46611 RVA: 0x0029F3F9 File Offset: 0x0029D5F9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("systemMailbox", this.systemMailbox);
			info.AddValue("database", this.database);
		}

		// Token: 0x17003975 RID: 14709
		// (get) Token: 0x0600B614 RID: 46612 RVA: 0x0029F425 File Offset: 0x0029D625
		public string SystemMailbox
		{
			get
			{
				return this.systemMailbox;
			}
		}

		// Token: 0x17003976 RID: 14710
		// (get) Token: 0x0600B615 RID: 46613 RVA: 0x0029F42D File Offset: 0x0029D62D
		public string Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x040062DB RID: 25307
		private readonly string systemMailbox;

		// Token: 0x040062DC RID: 25308
		private readonly string database;
	}
}
