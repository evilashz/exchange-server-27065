using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002C1 RID: 705
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FolderHierarchyContainsMultipleRootsTransientException : FolderHierarchyIsInconsistentTransientException
	{
		// Token: 0x06002387 RID: 9095 RVA: 0x0004EA8A File Offset: 0x0004CC8A
		public FolderHierarchyContainsMultipleRootsTransientException(string root1str, string root2str) : base(MrsStrings.FolderHierarchyContainsMultipleRoots(root1str, root2str))
		{
			this.root1str = root1str;
			this.root2str = root2str;
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x0004EAA7 File Offset: 0x0004CCA7
		public FolderHierarchyContainsMultipleRootsTransientException(string root1str, string root2str, Exception innerException) : base(MrsStrings.FolderHierarchyContainsMultipleRoots(root1str, root2str), innerException)
		{
			this.root1str = root1str;
			this.root2str = root2str;
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x0004EAC8 File Offset: 0x0004CCC8
		protected FolderHierarchyContainsMultipleRootsTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.root1str = (string)info.GetValue("root1str", typeof(string));
			this.root2str = (string)info.GetValue("root2str", typeof(string));
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x0004EB1D File Offset: 0x0004CD1D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("root1str", this.root1str);
			info.AddValue("root2str", this.root2str);
		}

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x0600238B RID: 9099 RVA: 0x0004EB49 File Offset: 0x0004CD49
		public string Root1str
		{
			get
			{
				return this.root1str;
			}
		}

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x0600238C RID: 9100 RVA: 0x0004EB51 File Offset: 0x0004CD51
		public string Root2str
		{
			get
			{
				return this.root2str;
			}
		}

		// Token: 0x04000FCC RID: 4044
		private readonly string root1str;

		// Token: 0x04000FCD RID: 4045
		private readonly string root2str;
	}
}
