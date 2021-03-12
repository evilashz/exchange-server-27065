using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Entities.Calendaring
{
	// Token: 0x02000006 RID: 6
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FolderNotFoundException : StoragePermanentException
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002830 File Offset: 0x00000A30
		public FolderNotFoundException(string folderType) : base(CalendaringStrings.FolderNotFound(folderType))
		{
			this.folderType = folderType;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002845 File Offset: 0x00000A45
		public FolderNotFoundException(string folderType, Exception innerException) : base(CalendaringStrings.FolderNotFound(folderType), innerException)
		{
			this.folderType = folderType;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000285B File Offset: 0x00000A5B
		protected FolderNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.folderType = (string)info.GetValue("folderType", typeof(string));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002885 File Offset: 0x00000A85
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("folderType", this.folderType);
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000028A0 File Offset: 0x00000AA0
		public string FolderType
		{
			get
			{
				return this.folderType;
			}
		}

		// Token: 0x0400002D RID: 45
		private readonly string folderType;
	}
}
