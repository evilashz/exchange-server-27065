using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200015F RID: 351
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DuplicateFolderInCSVException : MigrationPermanentException
	{
		// Token: 0x06001640 RID: 5696 RVA: 0x0006F160 File Offset: 0x0006D360
		public DuplicateFolderInCSVException(int rowIndex, string folderPath, string identifier) : base(Strings.DuplicateFolderInCSVError(rowIndex, folderPath, identifier))
		{
			this.rowIndex = rowIndex;
			this.folderPath = folderPath;
			this.identifier = identifier;
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x0006F185 File Offset: 0x0006D385
		public DuplicateFolderInCSVException(int rowIndex, string folderPath, string identifier, Exception innerException) : base(Strings.DuplicateFolderInCSVError(rowIndex, folderPath, identifier), innerException)
		{
			this.rowIndex = rowIndex;
			this.folderPath = folderPath;
			this.identifier = identifier;
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x0006F1AC File Offset: 0x0006D3AC
		protected DuplicateFolderInCSVException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.rowIndex = (int)info.GetValue("rowIndex", typeof(int));
			this.folderPath = (string)info.GetValue("folderPath", typeof(string));
			this.identifier = (string)info.GetValue("identifier", typeof(string));
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x0006F221 File Offset: 0x0006D421
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("rowIndex", this.rowIndex);
			info.AddValue("folderPath", this.folderPath);
			info.AddValue("identifier", this.identifier);
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001644 RID: 5700 RVA: 0x0006F25E File Offset: 0x0006D45E
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x0006F266 File Offset: 0x0006D466
		public string FolderPath
		{
			get
			{
				return this.folderPath;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x0006F26E File Offset: 0x0006D46E
		public string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x04000AEC RID: 2796
		private readonly int rowIndex;

		// Token: 0x04000AED RID: 2797
		private readonly string folderPath;

		// Token: 0x04000AEE RID: 2798
		private readonly string identifier;
	}
}
