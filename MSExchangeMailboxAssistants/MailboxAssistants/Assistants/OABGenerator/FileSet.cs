using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.OAB;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.OABGenerator
{
	// Token: 0x020001EC RID: 492
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class FileSet : DisposeTrackableBase
	{
		// Token: 0x0600133D RID: 4925 RVA: 0x000705B0 File Offset: 0x0006E7B0
		public FileSet()
		{
			this.files = new List<FileSet.FileItem>(110);
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x000705C8 File Offset: 0x0006E7C8
		public FileStream Create(string id)
		{
			string text = Path.Combine(Globals.AlternateTempFilePath, id + Path.GetRandomFileName());
			FileStream fileStream = File.Open(text, FileMode.Create, FileAccess.ReadWrite);
			FileSet.Tracer.TraceDebug<string>((long)this.GetHashCode(), "FileSet: Created temporary file {0}", text);
			FileSet.FileItem item = new FileSet.FileItem
			{
				Name = text,
				File = fileStream,
				Delete = true
			};
			this.files.Add(item);
			return fileStream;
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x00070650 File Offset: 0x0006E850
		public void Detach(FileStream file)
		{
			FileSet.FileItem fileItem = this.files.Find((FileSet.FileItem otherItem) => otherItem.File == file);
			if (fileItem != null)
			{
				fileItem.Delete = false;
				FileSet.Tracer.TraceDebug<string>((long)this.GetHashCode(), "FileSet: Detached file {0}", fileItem.Name);
			}
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x000706A8 File Offset: 0x0006E8A8
		public void Attach(string filePath)
		{
			FileSet.Tracer.TraceDebug<string>((long)this.GetHashCode(), "FileSet: Attaching file {0}", filePath);
			FileSet.FileItem item = new FileSet.FileItem
			{
				Name = filePath,
				File = null,
				Delete = true
			};
			this.files.Add(item);
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x000706F8 File Offset: 0x0006E8F8
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing)
			{
				foreach (FileSet.FileItem fileItem in this.files)
				{
					if (fileItem.File != null)
					{
						fileItem.File.Dispose();
					}
					if (fileItem.Delete)
					{
						Exception ex = null;
						try
						{
							File.Delete(fileItem.Name);
							FileSet.Tracer.TraceDebug<string>((long)this.GetHashCode(), "FileSet.InternalDispose: deleted file {0}", fileItem.Name);
						}
						catch (IOException ex2)
						{
							ex = ex2;
						}
						catch (UnauthorizedAccessException ex3)
						{
							ex = ex3;
						}
						if (ex != null)
						{
							FileSet.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "FileSet.InternalDispose: Unable to delete temporary file {0} due to exception: {1}", fileItem.Name, ex);
						}
					}
				}
				this.files.Clear();
			}
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x000707E4 File Offset: 0x0006E9E4
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<FileSet>(this);
		}

		// Token: 0x04000BB1 RID: 2993
		private static readonly Trace Tracer = ExTraceGlobals.AssistantTracer;

		// Token: 0x04000BB2 RID: 2994
		private List<FileSet.FileItem> files;

		// Token: 0x020001ED RID: 493
		private sealed class FileItem
		{
			// Token: 0x170004FB RID: 1275
			// (get) Token: 0x06001344 RID: 4932 RVA: 0x000707F8 File Offset: 0x0006E9F8
			// (set) Token: 0x06001345 RID: 4933 RVA: 0x00070800 File Offset: 0x0006EA00
			public string Name { get; set; }

			// Token: 0x170004FC RID: 1276
			// (get) Token: 0x06001346 RID: 4934 RVA: 0x00070809 File Offset: 0x0006EA09
			// (set) Token: 0x06001347 RID: 4935 RVA: 0x00070811 File Offset: 0x0006EA11
			public FileStream File { get; set; }

			// Token: 0x170004FD RID: 1277
			// (get) Token: 0x06001348 RID: 4936 RVA: 0x0007081A File Offset: 0x0006EA1A
			// (set) Token: 0x06001349 RID: 4937 RVA: 0x00070822 File Offset: 0x0006EA22
			public bool Delete { get; set; }
		}
	}
}
