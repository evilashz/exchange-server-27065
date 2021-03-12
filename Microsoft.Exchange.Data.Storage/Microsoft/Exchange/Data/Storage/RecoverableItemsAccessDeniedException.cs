using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000103 RID: 259
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class RecoverableItemsAccessDeniedException : StoragePermanentException
	{
		// Token: 0x06001399 RID: 5017 RVA: 0x0006982D File Offset: 0x00067A2D
		public RecoverableItemsAccessDeniedException(string folder) : base(ServerStrings.RecoverableItemsAccessDeniedException(folder))
		{
			this.folder = folder;
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x00069842 File Offset: 0x00067A42
		public RecoverableItemsAccessDeniedException(string folder, Exception innerException) : base(ServerStrings.RecoverableItemsAccessDeniedException(folder), innerException)
		{
			this.folder = folder;
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x00069858 File Offset: 0x00067A58
		protected RecoverableItemsAccessDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.folder = (string)info.GetValue("folder", typeof(string));
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x00069882 File Offset: 0x00067A82
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("folder", this.folder);
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x0600139D RID: 5021 RVA: 0x0006989D File Offset: 0x00067A9D
		public string Folder
		{
			get
			{
				return this.folder;
			}
		}

		// Token: 0x04000994 RID: 2452
		private readonly string folder;
	}
}
