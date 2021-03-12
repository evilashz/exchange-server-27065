using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Mapi.Common
{
	// Token: 0x0200004B RID: 75
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FolderAlreadyExistsException : MapiObjectAlreadyExistsException
	{
		// Token: 0x0600029F RID: 671 RVA: 0x0000E463 File Offset: 0x0000C663
		public FolderAlreadyExistsException(string folder) : base(Strings.FolderAlreadyExistsExceptionError(folder))
		{
			this.folder = folder;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000E478 File Offset: 0x0000C678
		public FolderAlreadyExistsException(string folder, Exception innerException) : base(Strings.FolderAlreadyExistsExceptionError(folder), innerException)
		{
			this.folder = folder;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000E48E File Offset: 0x0000C68E
		protected FolderAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.folder = (string)info.GetValue("folder", typeof(string));
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000E4B8 File Offset: 0x0000C6B8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("folder", this.folder);
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000E4D3 File Offset: 0x0000C6D3
		public string Folder
		{
			get
			{
				return this.folder;
			}
		}

		// Token: 0x040001A0 RID: 416
		private readonly string folder;
	}
}
