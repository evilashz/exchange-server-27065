using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000160 RID: 352
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidRootFolderMappingInCSVException : MigrationPermanentException
	{
		// Token: 0x06001647 RID: 5703 RVA: 0x0006F276 File Offset: 0x0006D476
		public InvalidRootFolderMappingInCSVException(int rowIndex, string folderPath, string identifier, string hierarchyMailboxName) : base(Strings.InvalidRootFolderMappingInCSVError(rowIndex, folderPath, identifier, hierarchyMailboxName))
		{
			this.rowIndex = rowIndex;
			this.folderPath = folderPath;
			this.identifier = identifier;
			this.hierarchyMailboxName = hierarchyMailboxName;
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x0006F2A5 File Offset: 0x0006D4A5
		public InvalidRootFolderMappingInCSVException(int rowIndex, string folderPath, string identifier, string hierarchyMailboxName, Exception innerException) : base(Strings.InvalidRootFolderMappingInCSVError(rowIndex, folderPath, identifier, hierarchyMailboxName), innerException)
		{
			this.rowIndex = rowIndex;
			this.folderPath = folderPath;
			this.identifier = identifier;
			this.hierarchyMailboxName = hierarchyMailboxName;
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x0006F2D8 File Offset: 0x0006D4D8
		protected InvalidRootFolderMappingInCSVException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.rowIndex = (int)info.GetValue("rowIndex", typeof(int));
			this.folderPath = (string)info.GetValue("folderPath", typeof(string));
			this.identifier = (string)info.GetValue("identifier", typeof(string));
			this.hierarchyMailboxName = (string)info.GetValue("hierarchyMailboxName", typeof(string));
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x0006F370 File Offset: 0x0006D570
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("rowIndex", this.rowIndex);
			info.AddValue("folderPath", this.folderPath);
			info.AddValue("identifier", this.identifier);
			info.AddValue("hierarchyMailboxName", this.hierarchyMailboxName);
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x0006F3C9 File Offset: 0x0006D5C9
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x0006F3D1 File Offset: 0x0006D5D1
		public string FolderPath
		{
			get
			{
				return this.folderPath;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x0006F3D9 File Offset: 0x0006D5D9
		public string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x0006F3E1 File Offset: 0x0006D5E1
		public string HierarchyMailboxName
		{
			get
			{
				return this.hierarchyMailboxName;
			}
		}

		// Token: 0x04000AEF RID: 2799
		private readonly int rowIndex;

		// Token: 0x04000AF0 RID: 2800
		private readonly string folderPath;

		// Token: 0x04000AF1 RID: 2801
		private readonly string identifier;

		// Token: 0x04000AF2 RID: 2802
		private readonly string hierarchyMailboxName;
	}
}
