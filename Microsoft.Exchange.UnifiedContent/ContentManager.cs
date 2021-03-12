using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Microsoft.Exchange.UnifiedContent
{
	// Token: 0x02000002 RID: 2
	public class ContentManager : IDisposable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		internal ContentManager(string path)
		{
			this.tempFilePath = this.CreateUnifiedContentTempPath(path);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020E5 File Offset: 0x000002E5
		// (set) Token: 0x06000003 RID: 3 RVA: 0x000020ED File Offset: 0x000002ED
		internal List<IExtractedContent> ContentCollection { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020F6 File Offset: 0x000002F6
		internal string ShareName
		{
			get
			{
				this.GetSharedStream();
				return this.sharedStream.SharedName;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000210A File Offset: 0x0000030A
		internal string TempFilePath
		{
			get
			{
				return this.tempFilePath;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002112 File Offset: 0x00000312
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002130 File Offset: 0x00000330
		internal SharedStream GetSharedStream()
		{
			if (this.sharedStream == null)
			{
				FileSecurity fileSecurity = new FileSecurity();
				fileSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.NetworkServiceSid, null), FileSystemRights.FullControl, AccessControlType.Allow));
				fileSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.LocalServiceSid, null), FileSystemRights.FullControl, AccessControlType.Allow));
				fileSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null), FileSystemRights.FullControl, AccessControlType.Allow));
				fileSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null), FileSystemRights.FullControl, AccessControlType.Allow));
				this.sharedStream = SharedStream.Create(this.TempFilePath, 1048576L, fileSecurity);
				byte[] array = new byte[]
				{
					0,
					1,
					2,
					3,
					4,
					5,
					6,
					7
				};
				this.sharedStream.Write(array, 0, array.Length);
			}
			return this.sharedStream;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021F2 File Offset: 0x000003F2
		internal void ClearContent()
		{
			if (this.sharedStream != null)
			{
				this.sharedStream.Dispose();
				this.sharedStream = null;
			}
			this.ContentCollection = null;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002215 File Offset: 0x00000415
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.ThrowIfDisposed();
			}
			if (!this.disposed)
			{
				this.ClearContent();
				this.disposed = true;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002238 File Offset: 0x00000438
		private string CreateUnifiedContentTempPath(string path)
		{
			string text = Path.Combine(path, "UnifiedContent");
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			return text;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002261 File Offset: 0x00000461
		private void ThrowIfDisposed()
		{
			if (this.disposed)
			{
				throw new InvalidOperationException("Object disposed.");
			}
		}

		// Token: 0x04000001 RID: 1
		private readonly string tempFilePath;

		// Token: 0x04000002 RID: 2
		private bool disposed;

		// Token: 0x04000003 RID: 3
		private SharedStream sharedStream;
	}
}
