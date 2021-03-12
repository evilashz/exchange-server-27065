using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002C2 RID: 706
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FolderHierarchyContainsDuplicatesPermanentException : FolderHierarchyIsInconsistentPermanentException
	{
		// Token: 0x0600238D RID: 9101 RVA: 0x0004EB59 File Offset: 0x0004CD59
		public FolderHierarchyContainsDuplicatesPermanentException(string folder1str, string folder2str) : base(MrsStrings.FolderHierarchyContainsDuplicates(folder1str, folder2str))
		{
			this.folder1str = folder1str;
			this.folder2str = folder2str;
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x0004EB76 File Offset: 0x0004CD76
		public FolderHierarchyContainsDuplicatesPermanentException(string folder1str, string folder2str, Exception innerException) : base(MrsStrings.FolderHierarchyContainsDuplicates(folder1str, folder2str), innerException)
		{
			this.folder1str = folder1str;
			this.folder2str = folder2str;
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x0004EB94 File Offset: 0x0004CD94
		protected FolderHierarchyContainsDuplicatesPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.folder1str = (string)info.GetValue("folder1str", typeof(string));
			this.folder2str = (string)info.GetValue("folder2str", typeof(string));
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x0004EBE9 File Offset: 0x0004CDE9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("folder1str", this.folder1str);
			info.AddValue("folder2str", this.folder2str);
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06002391 RID: 9105 RVA: 0x0004EC15 File Offset: 0x0004CE15
		public string Folder1str
		{
			get
			{
				return this.folder1str;
			}
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06002392 RID: 9106 RVA: 0x0004EC1D File Offset: 0x0004CE1D
		public string Folder2str
		{
			get
			{
				return this.folder2str;
			}
		}

		// Token: 0x04000FCE RID: 4046
		private readonly string folder1str;

		// Token: 0x04000FCF RID: 4047
		private readonly string folder2str;
	}
}
