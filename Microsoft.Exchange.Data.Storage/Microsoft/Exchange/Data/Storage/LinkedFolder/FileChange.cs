using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x0200096E RID: 2414
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FileChange : ChangedItem
	{
		// Token: 0x170018BB RID: 6331
		// (get) Token: 0x06005986 RID: 22918 RVA: 0x00172B84 File Offset: 0x00170D84
		// (set) Token: 0x06005987 RID: 22919 RVA: 0x00172B8C File Offset: 0x00170D8C
		public string DocIcon { get; private set; }

		// Token: 0x170018BC RID: 6332
		// (get) Token: 0x06005988 RID: 22920 RVA: 0x00172B95 File Offset: 0x00170D95
		// (set) Token: 0x06005989 RID: 22921 RVA: 0x00172B9D File Offset: 0x00170D9D
		public string Author { get; private set; }

		// Token: 0x170018BD RID: 6333
		// (get) Token: 0x0600598A RID: 22922 RVA: 0x00172BA6 File Offset: 0x00170DA6
		// (set) Token: 0x0600598B RID: 22923 RVA: 0x00172BAE File Offset: 0x00170DAE
		public string Editor { get; private set; }

		// Token: 0x170018BE RID: 6334
		// (get) Token: 0x0600598C RID: 22924 RVA: 0x00172BB7 File Offset: 0x00170DB7
		// (set) Token: 0x0600598D RID: 22925 RVA: 0x00172BBF File Offset: 0x00170DBF
		public string CheckoutUser { get; private set; }

		// Token: 0x170018BF RID: 6335
		// (get) Token: 0x0600598E RID: 22926 RVA: 0x00172BC8 File Offset: 0x00170DC8
		// (set) Token: 0x0600598F RID: 22927 RVA: 0x00172BD0 File Offset: 0x00170DD0
		public int Size { get; private set; }

		// Token: 0x06005990 RID: 22928 RVA: 0x00172BDC File Offset: 0x00170DDC
		public FileChange(Uri authority, Guid id, string version, string docIcon, string author, string editor, string checkoutUser, string relativePath, string leafNode, ExDateTime whenCreated, ExDateTime lastModified, int size) : base(authority, id, version, relativePath, leafNode, whenCreated, lastModified)
		{
			if (string.IsNullOrEmpty(leafNode))
			{
				throw new ArgumentNullException("leafNode");
			}
			this.DocIcon = docIcon;
			this.Author = author;
			this.Editor = editor;
			this.CheckoutUser = checkoutUser;
			this.Size = size;
		}
	}
}
