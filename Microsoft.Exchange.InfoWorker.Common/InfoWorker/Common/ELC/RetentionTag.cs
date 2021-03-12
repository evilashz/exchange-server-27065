using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.InfoWorker.Common.ELC
{
	// Token: 0x020001AE RID: 430
	internal class RetentionTag
	{
		// Token: 0x06000B59 RID: 2905 RVA: 0x00030D48 File Offset: 0x0002EF48
		internal RetentionTag()
		{
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00030D50 File Offset: 0x0002EF50
		internal RetentionTag(RetentionPolicyTag retentionPolicyTag)
		{
			this.guid = retentionPolicyTag.Guid;
			this.effectiveGuid = retentionPolicyTag.RetentionId;
			this.name = retentionPolicyTag.Name;
			this.localizedRetentionPolicyTagName = retentionPolicyTag.LocalizedRetentionPolicyTagName.ToArray();
			this.comment = retentionPolicyTag.Comment;
			this.localizedComment = retentionPolicyTag.LocalizedComment.ToArray();
			this.type = retentionPolicyTag.Type;
			this.isPrimary = retentionPolicyTag.IsPrimary;
			this.mustDisplayCommentEnabled = retentionPolicyTag.MustDisplayCommentEnabled;
			if (retentionPolicyTag.LegacyManagedFolder != null)
			{
				this.legacyManagedFolder = new Guid?(retentionPolicyTag.LegacyManagedFolder.ObjectGuid);
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x00030DF7 File Offset: 0x0002EFF7
		// (set) Token: 0x06000B5C RID: 2908 RVA: 0x00030E1D File Offset: 0x0002F01D
		internal Guid Guid
		{
			get
			{
				if (this.guid == Guid.Empty)
				{
					this.guid = this.effectiveGuid;
				}
				return this.guid;
			}
			set
			{
				this.guid = value;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x00030E26 File Offset: 0x0002F026
		// (set) Token: 0x06000B5E RID: 2910 RVA: 0x00030E2E File Offset: 0x0002F02E
		internal Guid RetentionId
		{
			get
			{
				return this.effectiveGuid;
			}
			set
			{
				this.effectiveGuid = value;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x00030E37 File Offset: 0x0002F037
		// (set) Token: 0x06000B60 RID: 2912 RVA: 0x00030E3F File Offset: 0x0002F03F
		internal string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x00030E48 File Offset: 0x0002F048
		// (set) Token: 0x06000B62 RID: 2914 RVA: 0x00030E50 File Offset: 0x0002F050
		internal string[] LocalizedRetentionPolicyTagName
		{
			get
			{
				return this.localizedRetentionPolicyTagName;
			}
			set
			{
				this.localizedRetentionPolicyTagName = value;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x00030E59 File Offset: 0x0002F059
		// (set) Token: 0x06000B64 RID: 2916 RVA: 0x00030E61 File Offset: 0x0002F061
		internal string Comment
		{
			get
			{
				return this.comment;
			}
			set
			{
				this.comment = value;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x00030E6A File Offset: 0x0002F06A
		// (set) Token: 0x06000B66 RID: 2918 RVA: 0x00030E72 File Offset: 0x0002F072
		internal string[] LocalizedComment
		{
			get
			{
				return this.localizedComment;
			}
			set
			{
				this.localizedComment = value;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x00030E7B File Offset: 0x0002F07B
		// (set) Token: 0x06000B68 RID: 2920 RVA: 0x00030E83 File Offset: 0x0002F083
		internal ElcFolderType Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x00030E8C File Offset: 0x0002F08C
		// (set) Token: 0x06000B6A RID: 2922 RVA: 0x00030E94 File Offset: 0x0002F094
		internal bool IsPrimary
		{
			get
			{
				return this.isPrimary;
			}
			set
			{
				this.isPrimary = value;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x00030E9D File Offset: 0x0002F09D
		// (set) Token: 0x06000B6C RID: 2924 RVA: 0x00030EA5 File Offset: 0x0002F0A5
		internal bool MustDisplayCommentEnabled
		{
			get
			{
				return this.mustDisplayCommentEnabled;
			}
			set
			{
				this.mustDisplayCommentEnabled = value;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x00030EAE File Offset: 0x0002F0AE
		// (set) Token: 0x06000B6E RID: 2926 RVA: 0x00030EB6 File Offset: 0x0002F0B6
		internal Guid? LegacyManagedFolder
		{
			get
			{
				return this.legacyManagedFolder;
			}
			set
			{
				this.legacyManagedFolder = value;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x00030EBF File Offset: 0x0002F0BF
		// (set) Token: 0x06000B70 RID: 2928 RVA: 0x00030EC7 File Offset: 0x0002F0C7
		internal bool IsArchiveTag
		{
			get
			{
				return this.isArchiveTag;
			}
			set
			{
				this.isArchiveTag = value;
			}
		}

		// Token: 0x04000879 RID: 2169
		private Guid guid;

		// Token: 0x0400087A RID: 2170
		private string name;

		// Token: 0x0400087B RID: 2171
		private string[] localizedRetentionPolicyTagName;

		// Token: 0x0400087C RID: 2172
		private string comment;

		// Token: 0x0400087D RID: 2173
		private string[] localizedComment;

		// Token: 0x0400087E RID: 2174
		private ElcFolderType type;

		// Token: 0x0400087F RID: 2175
		private bool isPrimary;

		// Token: 0x04000880 RID: 2176
		private bool mustDisplayCommentEnabled;

		// Token: 0x04000881 RID: 2177
		private Guid? legacyManagedFolder;

		// Token: 0x04000882 RID: 2178
		private Guid effectiveGuid;

		// Token: 0x04000883 RID: 2179
		private bool isArchiveTag;
	}
}
