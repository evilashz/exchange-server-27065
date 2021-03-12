using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000289 RID: 649
	internal abstract class RopCopyMoveFolderBase : DualInputRop
	{
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x0002A592 File Offset: 0x00028792
		protected bool ReportProgress
		{
			get
			{
				return this.reportProgress;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x0002A59A File Offset: 0x0002879A
		protected bool Recurse
		{
			get
			{
				return this.recurse;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x0002A5A2 File Offset: 0x000287A2
		protected StoreId FolderId
		{
			get
			{
				return this.folderId;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000E2E RID: 3630 RVA: 0x0002A5AA File Offset: 0x000287AA
		protected String8 FolderName
		{
			get
			{
				return this.folderName;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x0002A5B2 File Offset: 0x000287B2
		protected virtual bool IsCopyFolder
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0002A5B5 File Offset: 0x000287B5
		internal void SetInput(byte logonIndex, byte sourceHandleTableIndex, byte destinationHandleTableIndex, bool reportProgress, bool recurse, bool useUnicode, StoreId folderId, string folderName)
		{
			base.SetCommonInput(logonIndex, sourceHandleTableIndex, destinationHandleTableIndex);
			this.reportProgress = reportProgress;
			this.recurse = recurse;
			this.useUnicode = useUnicode;
			this.folderId = folderId;
			this.folderName = String8.Create(folderName);
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0002A5F0 File Offset: 0x000287F0
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteBool(this.reportProgress);
			if (this.IsCopyFolder)
			{
				writer.WriteBool(this.recurse);
			}
			writer.WriteBool(this.useUnicode);
			this.folderId.Serialize(writer);
			if (this.useUnicode)
			{
				writer.WriteUnicodeString(this.folderName.StringValue, StringFlags.IncludeNull);
				return;
			}
			writer.WriteString8(this.folderName.StringValue, string8Encoding, StringFlags.IncludeNull);
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0002A66C File Offset: 0x0002886C
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.reportProgress = reader.ReadBool();
			if (this.IsCopyFolder)
			{
				this.recurse = reader.ReadBool();
			}
			this.useUnicode = reader.ReadBool();
			this.folderId = StoreId.Parse(reader);
			this.folderName = String8.Parse(reader, this.useUnicode, StringFlags.IncludeNull);
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0002A6CC File Offset: 0x000288CC
		internal override void ResolveString8Values(Encoding string8Encoding)
		{
			base.ResolveString8Values(string8Encoding);
			this.folderName.ResolveString8Values(string8Encoding);
		}

		// Token: 0x04000742 RID: 1858
		private bool reportProgress;

		// Token: 0x04000743 RID: 1859
		private bool recurse;

		// Token: 0x04000744 RID: 1860
		private bool useUnicode;

		// Token: 0x04000745 RID: 1861
		private StoreId folderId;

		// Token: 0x04000746 RID: 1862
		private String8 folderName;
	}
}
