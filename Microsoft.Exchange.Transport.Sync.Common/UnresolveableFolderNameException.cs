using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000054 RID: 84
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnresolveableFolderNameException : LocalizedException
	{
		// Token: 0x06000230 RID: 560 RVA: 0x00006557 File Offset: 0x00004757
		public UnresolveableFolderNameException(string folderName) : base(Strings.UnresolveableFolderNameException(folderName))
		{
			this.folderName = folderName;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000656C File Offset: 0x0000476C
		public UnresolveableFolderNameException(string folderName, Exception innerException) : base(Strings.UnresolveableFolderNameException(folderName), innerException)
		{
			this.folderName = folderName;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00006582 File Offset: 0x00004782
		protected UnresolveableFolderNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.folderName = (string)info.GetValue("folderName", typeof(string));
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000065AC File Offset: 0x000047AC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("folderName", this.folderName);
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000234 RID: 564 RVA: 0x000065C7 File Offset: 0x000047C7
		public string FolderName
		{
			get
			{
				return this.folderName;
			}
		}

		// Token: 0x040000FD RID: 253
		private readonly string folderName;
	}
}
