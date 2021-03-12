using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200116C RID: 4460
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FolderNotExistException : LocalizedException
	{
		// Token: 0x0600B607 RID: 46599 RVA: 0x0029F2C1 File Offset: 0x0029D4C1
		public FolderNotExistException(string folder) : base(Strings.ErrorFolderNotExist(folder))
		{
			this.folder = folder;
		}

		// Token: 0x0600B608 RID: 46600 RVA: 0x0029F2D6 File Offset: 0x0029D4D6
		public FolderNotExistException(string folder, Exception innerException) : base(Strings.ErrorFolderNotExist(folder), innerException)
		{
			this.folder = folder;
		}

		// Token: 0x0600B609 RID: 46601 RVA: 0x0029F2EC File Offset: 0x0029D4EC
		protected FolderNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.folder = (string)info.GetValue("folder", typeof(string));
		}

		// Token: 0x0600B60A RID: 46602 RVA: 0x0029F316 File Offset: 0x0029D516
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("folder", this.folder);
		}

		// Token: 0x17003974 RID: 14708
		// (get) Token: 0x0600B60B RID: 46603 RVA: 0x0029F331 File Offset: 0x0029D531
		public string Folder
		{
			get
			{
				return this.folder;
			}
		}

		// Token: 0x040062DA RID: 25306
		private readonly string folder;
	}
}
