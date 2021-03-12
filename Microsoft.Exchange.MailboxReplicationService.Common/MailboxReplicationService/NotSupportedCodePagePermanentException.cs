using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002E8 RID: 744
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NotSupportedCodePagePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600244A RID: 9290 RVA: 0x0004FD7C File Offset: 0x0004DF7C
		public NotSupportedCodePagePermanentException(int codePage, string server) : base(MrsStrings.NotSupportedCodePageError(codePage, server))
		{
			this.codePage = codePage;
			this.server = server;
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x0004FD99 File Offset: 0x0004DF99
		public NotSupportedCodePagePermanentException(int codePage, string server, Exception innerException) : base(MrsStrings.NotSupportedCodePageError(codePage, server), innerException)
		{
			this.codePage = codePage;
			this.server = server;
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x0004FDB8 File Offset: 0x0004DFB8
		protected NotSupportedCodePagePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.codePage = (int)info.GetValue("codePage", typeof(int));
			this.server = (string)info.GetValue("server", typeof(string));
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x0004FE0D File Offset: 0x0004E00D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("codePage", this.codePage);
			info.AddValue("server", this.server);
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x0600244E RID: 9294 RVA: 0x0004FE39 File Offset: 0x0004E039
		public int CodePage
		{
			get
			{
				return this.codePage;
			}
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x0600244F RID: 9295 RVA: 0x0004FE41 File Offset: 0x0004E041
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x04000FF3 RID: 4083
		private readonly int codePage;

		// Token: 0x04000FF4 RID: 4084
		private readonly string server;
	}
}
