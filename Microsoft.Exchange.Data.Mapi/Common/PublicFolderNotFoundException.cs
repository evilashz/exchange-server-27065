using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Mapi.Common
{
	// Token: 0x0200004D RID: 77
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PublicFolderNotFoundException : MapiObjectNotFoundException
	{
		// Token: 0x060002A8 RID: 680 RVA: 0x0000E502 File Offset: 0x0000C702
		public PublicFolderNotFoundException(string folder) : base(Strings.PublicFolderNotFoundExceptionError(folder))
		{
			this.folder = folder;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000E517 File Offset: 0x0000C717
		public PublicFolderNotFoundException(string folder, Exception innerException) : base(Strings.PublicFolderNotFoundExceptionError(folder), innerException)
		{
			this.folder = folder;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000E52D File Offset: 0x0000C72D
		protected PublicFolderNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.folder = (string)info.GetValue("folder", typeof(string));
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000E557 File Offset: 0x0000C757
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("folder", this.folder);
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000E572 File Offset: 0x0000C772
		public string Folder
		{
			get
			{
				return this.folder;
			}
		}

		// Token: 0x040001A1 RID: 417
		private readonly string folder;
	}
}
