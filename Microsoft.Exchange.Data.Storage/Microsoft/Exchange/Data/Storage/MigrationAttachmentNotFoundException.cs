using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000111 RID: 273
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationAttachmentNotFoundException : MigrationPermanentException
	{
		// Token: 0x060013E4 RID: 5092 RVA: 0x00069EBE File Offset: 0x000680BE
		public MigrationAttachmentNotFoundException(string attachment) : base(ServerStrings.MigrationAttachmentNotFound(attachment))
		{
			this.attachment = attachment;
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x00069ED3 File Offset: 0x000680D3
		public MigrationAttachmentNotFoundException(string attachment, Exception innerException) : base(ServerStrings.MigrationAttachmentNotFound(attachment), innerException)
		{
			this.attachment = attachment;
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x00069EE9 File Offset: 0x000680E9
		protected MigrationAttachmentNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.attachment = (string)info.GetValue("attachment", typeof(string));
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x00069F13 File Offset: 0x00068113
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("attachment", this.attachment);
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x060013E8 RID: 5096 RVA: 0x00069F2E File Offset: 0x0006812E
		public string Attachment
		{
			get
			{
				return this.attachment;
			}
		}

		// Token: 0x0400099F RID: 2463
		private readonly string attachment;
	}
}
