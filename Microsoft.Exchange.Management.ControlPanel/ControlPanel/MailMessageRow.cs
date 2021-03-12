using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Providers;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002C3 RID: 707
	[DataContract]
	public class MailMessageRow : BaseRow
	{
		// Token: 0x06002C23 RID: 11299 RVA: 0x00088DBD File Offset: 0x00086FBD
		public MailMessageRow(MailMessage message) : base(new Identity(message.Identity.ToString(), message.Subject), message)
		{
			this.Message = message;
		}

		// Token: 0x17001DBA RID: 7610
		// (get) Token: 0x06002C24 RID: 11300 RVA: 0x00088DE3 File Offset: 0x00086FE3
		// (set) Token: 0x06002C25 RID: 11301 RVA: 0x00088DEB File Offset: 0x00086FEB
		public MailMessage Message { get; private set; }

		// Token: 0x17001DBB RID: 7611
		// (get) Token: 0x06002C26 RID: 11302 RVA: 0x00088DF4 File Offset: 0x00086FF4
		// (set) Token: 0x06002C27 RID: 11303 RVA: 0x00088E01 File Offset: 0x00087001
		[DataMember]
		public string Subject
		{
			get
			{
				return this.Message.Subject;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001DBC RID: 7612
		// (get) Token: 0x06002C28 RID: 11304 RVA: 0x00088E08 File Offset: 0x00087008
		// (set) Token: 0x06002C29 RID: 11305 RVA: 0x00088E15 File Offset: 0x00087015
		[DataMember]
		public string Body
		{
			get
			{
				return this.Message.Body;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001DBD RID: 7613
		// (get) Token: 0x06002C2A RID: 11306 RVA: 0x00088E1C File Offset: 0x0008701C
		// (set) Token: 0x06002C2B RID: 11307 RVA: 0x00088E29 File Offset: 0x00087029
		[DataMember]
		public BodyFormat BodyFormat
		{
			get
			{
				return (BodyFormat)this.Message.BodyFormat;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}
	}
}
