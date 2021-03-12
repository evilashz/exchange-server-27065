using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000056 RID: 86
	public class WacDiscoveryResultFailure : WacDiscoveryResultBase
	{
		// Token: 0x06000298 RID: 664 RVA: 0x00009E64 File Offset: 0x00008064
		public WacDiscoveryResultFailure(WacDiscoveryFailureException ex)
		{
			this.wacDiscoveryFailedException = ex;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00009E73 File Offset: 0x00008073
		public override string[] WacViewableFileTypes
		{
			get
			{
				throw this.wacDiscoveryFailedException;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00009E7B File Offset: 0x0000807B
		public override string[] WacEditableFileTypes
		{
			get
			{
				throw this.wacDiscoveryFailedException;
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00009E83 File Offset: 0x00008083
		public override string GetWacViewableFileTypesDisplayText()
		{
			throw this.wacDiscoveryFailedException;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00009E8B File Offset: 0x0000808B
		public override void AddViewMapping(string fileExtension, string path)
		{
			throw this.wacDiscoveryFailedException;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00009E93 File Offset: 0x00008093
		public override void AddEditMapping(string fileExtension, string path)
		{
			throw this.wacDiscoveryFailedException;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00009E9B File Offset: 0x0000809B
		public override bool TryGetViewUrlForFileExtension(string extension, string cultureName, out string url)
		{
			throw this.wacDiscoveryFailedException;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00009EA3 File Offset: 0x000080A3
		public override bool TryGetEditUrlForFileExtension(string extension, string cultureName, out string url)
		{
			throw this.wacDiscoveryFailedException;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00009EAB File Offset: 0x000080AB
		public override void MarkInitializationComplete()
		{
			throw this.wacDiscoveryFailedException;
		}

		// Token: 0x04000135 RID: 309
		private WacDiscoveryFailureException wacDiscoveryFailedException;
	}
}
