using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x02000964 RID: 2404
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ChangedItem
	{
		// Token: 0x170018A6 RID: 6310
		// (get) Token: 0x06005908 RID: 22792 RVA: 0x0016DF33 File Offset: 0x0016C133
		// (set) Token: 0x06005909 RID: 22793 RVA: 0x0016DF3B File Offset: 0x0016C13B
		public Guid Id { get; private set; }

		// Token: 0x170018A7 RID: 6311
		// (get) Token: 0x0600590A RID: 22794 RVA: 0x0016DF44 File Offset: 0x0016C144
		// (set) Token: 0x0600590B RID: 22795 RVA: 0x0016DF4C File Offset: 0x0016C14C
		public string Version { get; private set; }

		// Token: 0x170018A8 RID: 6312
		// (get) Token: 0x0600590C RID: 22796 RVA: 0x0016DF55 File Offset: 0x0016C155
		// (set) Token: 0x0600590D RID: 22797 RVA: 0x0016DF5D File Offset: 0x0016C15D
		public ExDateTime LastModified { get; private set; }

		// Token: 0x170018A9 RID: 6313
		// (get) Token: 0x0600590E RID: 22798 RVA: 0x0016DF66 File Offset: 0x0016C166
		// (set) Token: 0x0600590F RID: 22799 RVA: 0x0016DF6E File Offset: 0x0016C16E
		public ExDateTime WhenCreated { get; private set; }

		// Token: 0x170018AA RID: 6314
		// (get) Token: 0x06005910 RID: 22800 RVA: 0x0016DF77 File Offset: 0x0016C177
		// (set) Token: 0x06005911 RID: 22801 RVA: 0x0016DF7F File Offset: 0x0016C17F
		public string LeafNode { get; private set; }

		// Token: 0x170018AB RID: 6315
		// (get) Token: 0x06005912 RID: 22802 RVA: 0x0016DF88 File Offset: 0x0016C188
		// (set) Token: 0x06005913 RID: 22803 RVA: 0x0016DF90 File Offset: 0x0016C190
		public string RelativePath { get; private set; }

		// Token: 0x170018AC RID: 6316
		// (get) Token: 0x06005914 RID: 22804 RVA: 0x0016DF99 File Offset: 0x0016C199
		// (set) Token: 0x06005915 RID: 22805 RVA: 0x0016DFA1 File Offset: 0x0016C1A1
		public Uri Authority { get; private set; }

		// Token: 0x170018AD RID: 6317
		// (get) Token: 0x06005916 RID: 22806 RVA: 0x0016DFAA File Offset: 0x0016C1AA
		// (set) Token: 0x06005917 RID: 22807 RVA: 0x0016DFB2 File Offset: 0x0016C1B2
		public Uri Path { get; private set; }

		// Token: 0x170018AE RID: 6318
		// (get) Token: 0x06005918 RID: 22808 RVA: 0x0016DFBB File Offset: 0x0016C1BB
		// (set) Token: 0x06005919 RID: 22809 RVA: 0x0016DFC3 File Offset: 0x0016C1C3
		public int Level { get; private set; }

		// Token: 0x170018AF RID: 6319
		// (get) Token: 0x0600591A RID: 22810 RVA: 0x0016DFCC File Offset: 0x0016C1CC
		// (set) Token: 0x0600591B RID: 22811 RVA: 0x0016DFD4 File Offset: 0x0016C1D4
		public Uri ParentPath { get; private set; }

		// Token: 0x170018B0 RID: 6320
		// (get) Token: 0x0600591C RID: 22812 RVA: 0x0016DFDD File Offset: 0x0016C1DD
		// (set) Token: 0x0600591D RID: 22813 RVA: 0x0016DFE5 File Offset: 0x0016C1E5
		public int ParentLevel { get; private set; }

		// Token: 0x170018B1 RID: 6321
		// (get) Token: 0x0600591E RID: 22814 RVA: 0x0016DFEE File Offset: 0x0016C1EE
		// (set) Token: 0x0600591F RID: 22815 RVA: 0x0016DFF6 File Offset: 0x0016C1F6
		public Exception InducedException { get; set; }

		// Token: 0x06005920 RID: 22816 RVA: 0x0016E000 File Offset: 0x0016C200
		protected ChangedItem(Uri authority, Guid id, string version, string relativePath, string leafNode, ExDateTime whenCreated, ExDateTime lastModified)
		{
			if (authority == null)
			{
				throw new ArgumentNullException("authority");
			}
			if (string.IsNullOrEmpty(relativePath))
			{
				throw new ArgumentNullException("relativePath");
			}
			int num = relativePath.LastIndexOf('/');
			if (num == -1)
			{
				throw new ArgumentException("Unexpected relativePath as " + relativePath);
			}
			if (relativePath[0] == '/')
			{
				relativePath = relativePath.Substring(1, relativePath.Length - 1);
			}
			this.Id = id;
			this.Version = version;
			this.RelativePath = relativePath;
			this.LeafNode = leafNode;
			this.WhenCreated = whenCreated;
			this.LastModified = lastModified;
			this.Path = new Uri(authority, this.RelativePath);
			this.Authority = authority;
			num = this.RelativePath.LastIndexOf('/');
			if (num != -1)
			{
				this.Level = this.RelativePath.Split(new char[]
				{
					'/'
				}).Length;
				this.ParentLevel = this.Level - 1;
				this.ParentPath = new Uri(this.Authority, this.RelativePath.Substring(0, num));
			}
		}
	}
}
