using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000054 RID: 84
	public abstract class WacDiscoveryResultBase
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000283 RID: 643
		public abstract string[] WacViewableFileTypes { get; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000284 RID: 644
		public abstract string[] WacEditableFileTypes { get; }

		// Token: 0x06000285 RID: 645
		public abstract string GetWacViewableFileTypesDisplayText();

		// Token: 0x06000286 RID: 646
		public abstract void AddViewMapping(string fileExtension, string path);

		// Token: 0x06000287 RID: 647
		public abstract void AddEditMapping(string fileExtension, string path);

		// Token: 0x06000288 RID: 648
		public abstract bool TryGetViewUrlForFileExtension(string extension, string cultureName, out string url);

		// Token: 0x06000289 RID: 649
		public abstract bool TryGetEditUrlForFileExtension(string extension, string cultureName, out string url);

		// Token: 0x0600028A RID: 650
		public abstract void MarkInitializationComplete();
	}
}
