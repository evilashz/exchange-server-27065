using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.ApplicationLogic.Compliance
{
	// Token: 0x020000CB RID: 203
	internal class FolderEnumerator : QueryResultsEnumerator
	{
		// Token: 0x0600089B RID: 2203 RVA: 0x000226FC File Offset: 0x000208FC
		internal FolderEnumerator(QueryResult queryResult, Folder rootFolder, object[] rootFolderProperties) : base(queryResult)
		{
			this.rootFolder = rootFolder;
			this.rootFolderProperties = rootFolderProperties;
			this.isAtBegining = true;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0002271C File Offset: 0x0002091C
		public override bool MoveNext()
		{
			bool result = base.MoveNext();
			if (this.isAtBegining)
			{
				if (base.Current != null)
				{
					base.Current.Insert(0, this.rootFolderProperties);
				}
				this.isAtBegining = false;
			}
			return result;
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0002275A File Offset: 0x0002095A
		public override void Dispose()
		{
			base.Dispose();
			if (this.rootFolder != null)
			{
				this.rootFolder.Dispose();
			}
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00022775 File Offset: 0x00020975
		public override void Reset()
		{
			base.Reset();
			this.isAtBegining = true;
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00022784 File Offset: 0x00020984
		protected override void HandleException(Exception exception)
		{
			ExTraceGlobals.StorageTracer.TraceError<string, Exception>(0L, "{0}: Failed to get folder hierarchy because the folder was not found or was inaccessible. Exception: '{1}'", this.rootFolder.DisplayName, exception);
		}

		// Token: 0x040003CC RID: 972
		private readonly Folder rootFolder;

		// Token: 0x040003CD RID: 973
		private readonly object[] rootFolderProperties;

		// Token: 0x040003CE RID: 974
		private bool isAtBegining;
	}
}
