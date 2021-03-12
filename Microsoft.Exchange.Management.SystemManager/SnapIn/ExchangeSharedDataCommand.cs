using System;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x02000281 RID: 641
	[Serializable]
	public abstract class ExchangeSharedDataCommand
	{
		// Token: 0x06001B36 RID: 6966
		public abstract void PrimaryExecute(string key, CommunicationChannelCollection channels, NodeStructureSettingsStore store, SyncQueue syncQueue);

		// Token: 0x06001B37 RID: 6967
		public abstract void ExtensionExecute(IExchangeExtensionSnapIn snapIn);

		// Token: 0x06001B38 RID: 6968 RVA: 0x00078108 File Offset: 0x00076308
		public static ExchangeSharedDataCommand Parse(byte[] bytes)
		{
			return (ExchangeSharedDataCommand)WinformsHelper.DeSerialize(bytes);
		}
	}
}
