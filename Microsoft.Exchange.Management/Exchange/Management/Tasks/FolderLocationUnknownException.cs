using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200116B RID: 4459
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FolderLocationUnknownException : LocalizedException
	{
		// Token: 0x0600B602 RID: 46594 RVA: 0x0029F249 File Offset: 0x0029D449
		public FolderLocationUnknownException(string folder) : base(Strings.ErrorFolderLocationUnknown(folder))
		{
			this.folder = folder;
		}

		// Token: 0x0600B603 RID: 46595 RVA: 0x0029F25E File Offset: 0x0029D45E
		public FolderLocationUnknownException(string folder, Exception innerException) : base(Strings.ErrorFolderLocationUnknown(folder), innerException)
		{
			this.folder = folder;
		}

		// Token: 0x0600B604 RID: 46596 RVA: 0x0029F274 File Offset: 0x0029D474
		protected FolderLocationUnknownException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.folder = (string)info.GetValue("folder", typeof(string));
		}

		// Token: 0x0600B605 RID: 46597 RVA: 0x0029F29E File Offset: 0x0029D49E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("folder", this.folder);
		}

		// Token: 0x17003973 RID: 14707
		// (get) Token: 0x0600B606 RID: 46598 RVA: 0x0029F2B9 File Offset: 0x0029D4B9
		public string Folder
		{
			get
			{
				return this.folder;
			}
		}

		// Token: 0x040062D9 RID: 25305
		private readonly string folder;
	}
}
