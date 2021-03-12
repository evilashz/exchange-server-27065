using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000120 RID: 288
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DefaultTaskNodeCreationException : StorageTransientException
	{
		// Token: 0x0600142C RID: 5164 RVA: 0x0006A543 File Offset: 0x00068743
		public DefaultTaskNodeCreationException(string folderType) : base(ServerStrings.idUnableToAddDefaultTaskFolderToDefaultTaskGroup(folderType))
		{
			this.folderType = folderType;
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x0006A558 File Offset: 0x00068758
		public DefaultTaskNodeCreationException(string folderType, Exception innerException) : base(ServerStrings.idUnableToAddDefaultTaskFolderToDefaultTaskGroup(folderType), innerException)
		{
			this.folderType = folderType;
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x0006A56E File Offset: 0x0006876E
		protected DefaultTaskNodeCreationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.folderType = (string)info.GetValue("folderType", typeof(string));
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x0006A598 File Offset: 0x00068798
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("folderType", this.folderType);
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x0006A5B3 File Offset: 0x000687B3
		public string FolderType
		{
			get
			{
				return this.folderType;
			}
		}

		// Token: 0x040009AC RID: 2476
		private readonly string folderType;
	}
}
