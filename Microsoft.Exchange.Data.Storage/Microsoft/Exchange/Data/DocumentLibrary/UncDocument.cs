using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006D2 RID: 1746
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UncDocument : UncDocumentLibraryItem, IDocument, IDocumentLibraryItem, IReadOnlyPropertyBag
	{
		// Token: 0x060045AE RID: 17838 RVA: 0x00128423 File Offset: 0x00126623
		internal UncDocument(UncSession session, UncObjectId objectId) : base(session, objectId, new FileInfo(objectId.Path.LocalPath), UncDocumentSchema.Instance)
		{
			this.fileInfo = (this.fileSystemInfo as FileInfo);
		}

		// Token: 0x060045AF RID: 17839 RVA: 0x001284C4 File Offset: 0x001266C4
		public new static UncDocument Read(UncSession session, ObjectId documentId)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (documentId == null)
			{
				throw new ArgumentNullException("documentId");
			}
			UncObjectId uncObjectId = documentId as UncObjectId;
			if (uncObjectId == null)
			{
				throw new ArgumentException("documentId");
			}
			return Utils.DoUncTask<UncDocument>(session.Identity, uncObjectId, false, Utils.MethodType.Read, delegate
			{
				FileSystemInfo fileSystemInfo = new FileInfo(uncObjectId.Path.LocalPath);
				if (fileSystemInfo.Attributes != (FileAttributes)(-1) && fileSystemInfo.Exists)
				{
					return new UncDocument(session, uncObjectId);
				}
				throw new ObjectNotFoundException(uncObjectId, Strings.ExObjectNotFound(uncObjectId.Path.LocalPath));
			});
		}

		// Token: 0x17001439 RID: 5177
		// (get) Token: 0x060045B0 RID: 17840 RVA: 0x00128542 File Offset: 0x00126742
		public override string DisplayName
		{
			get
			{
				return Path.GetFileNameWithoutExtension(this.fileSystemInfo.Name);
			}
		}

		// Token: 0x1700143A RID: 5178
		// (get) Token: 0x060045B1 RID: 17841 RVA: 0x00128554 File Offset: 0x00126754
		public long Size
		{
			get
			{
				return this.fileInfo.Length;
			}
		}

		// Token: 0x060045B2 RID: 17842 RVA: 0x001285EC File Offset: 0x001267EC
		public Stream GetDocument()
		{
			return Utils.DoUncTask<Stream>(this.session.Identity, base.UncId, true, Utils.MethodType.GetStream, delegate
			{
				Stream stream = File.Open(this.fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				bool flag = false;
				try
				{
					DateTime lastWriteTimeUtc = this.fileInfo.LastWriteTimeUtc;
					this.fileInfo.Refresh();
					if (lastWriteTimeUtc < this.fileInfo.LastWriteTimeUtc)
					{
						throw new DocumentModifiedException(base.Id, this.fileInfo.FullName);
					}
					flag = true;
				}
				finally
				{
					if (!flag)
					{
						stream.Dispose();
						stream = null;
					}
				}
				return stream;
			});
		}

		// Token: 0x060045B3 RID: 17843 RVA: 0x00128612 File Offset: 0x00126812
		protected override string GetParentDirectoryNameInternal()
		{
			return this.fileInfo.DirectoryName;
		}

		// Token: 0x04002620 RID: 9760
		private readonly FileInfo fileInfo;
	}
}
