using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A5A RID: 2650
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADTreeDeleteNotFinishedException : ADTransientException
	{
		// Token: 0x06007EC0 RID: 32448 RVA: 0x001A3917 File Offset: 0x001A1B17
		public ADTreeDeleteNotFinishedException(string server) : base(DirectoryStrings.ADTreeDeleteNotFinishedException(server))
		{
			this.server = server;
		}

		// Token: 0x06007EC1 RID: 32449 RVA: 0x001A392C File Offset: 0x001A1B2C
		public ADTreeDeleteNotFinishedException(string server, Exception innerException) : base(DirectoryStrings.ADTreeDeleteNotFinishedException(server), innerException)
		{
			this.server = server;
		}

		// Token: 0x06007EC2 RID: 32450 RVA: 0x001A3942 File Offset: 0x001A1B42
		protected ADTreeDeleteNotFinishedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.server = (string)info.GetValue("server", typeof(string));
		}

		// Token: 0x06007EC3 RID: 32451 RVA: 0x001A396C File Offset: 0x001A1B6C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("server", this.server);
		}

		// Token: 0x17002EAF RID: 11951
		// (get) Token: 0x06007EC4 RID: 32452 RVA: 0x001A3987 File Offset: 0x001A1B87
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x04005589 RID: 21897
		private readonly string server;
	}
}
