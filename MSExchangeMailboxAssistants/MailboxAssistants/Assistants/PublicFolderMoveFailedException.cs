using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x0200013F RID: 319
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class PublicFolderMoveFailedException : Exception
	{
		// Token: 0x06000D13 RID: 3347 RVA: 0x00051E25 File Offset: 0x00050025
		public PublicFolderMoveFailedException(string error) : base(Strings.PublicFolderMoveFailedError(error))
		{
			this.error = error;
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00051E3F File Offset: 0x0005003F
		public PublicFolderMoveFailedException(string error, Exception innerException) : base(Strings.PublicFolderMoveFailedError(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00051E5A File Offset: 0x0005005A
		protected PublicFolderMoveFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00051E84 File Offset: 0x00050084
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x00051E9F File Offset: 0x0005009F
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x0400083D RID: 2109
		private readonly string error;
	}
}
