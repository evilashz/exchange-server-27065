using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000353 RID: 851
	internal class SetFolderMruConfiguration : ServiceCommand<bool>
	{
		// Token: 0x06001BBA RID: 7098 RVA: 0x0006AB68 File Offset: 0x00068D68
		public SetFolderMruConfiguration(CallContext callContext, TargetFolderMruConfiguration folderMruConfiguration) : base(callContext)
		{
			this.folderMruConfiguration = folderMruConfiguration;
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x0006AB78 File Offset: 0x00068D78
		protected override bool InternalExecute()
		{
			if (this.folderMruConfiguration.FolderMruEntries == null || this.folderMruConfiguration.FolderMruEntries.Length == 0)
			{
				return false;
			}
			this.folderMruConfiguration.Save(CallContext.Current);
			return true;
		}

		// Token: 0x04000FBC RID: 4028
		private TargetFolderMruConfiguration folderMruConfiguration;
	}
}
