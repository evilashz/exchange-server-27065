using System;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.ManagementConsole.Advanced;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x02000282 RID: 642
	[Serializable]
	public abstract class SyncQueueSharedDataCommand : ExchangeSharedDataCommand
	{
		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001B3A RID: 6970 RVA: 0x0007811D File Offset: 0x0007631D
		public Guid Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x00078125 File Offset: 0x00076325
		public sealed override void PrimaryExecute(string key, CommunicationChannelCollection channels, NodeStructureSettingsStore store, SyncQueue syncQueue)
		{
			channels[key].Channel.SetData(WinformsHelper.Serialize(this));
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x00078140 File Offset: 0x00076340
		public sealed override void ExtensionExecute(IExchangeExtensionSnapIn snapIn)
		{
			this.Execute(snapIn);
			try
			{
				snapIn.SharedDataItem.RequestDataUpdate(WinformsHelper.Serialize(new AcKnowledgeCommand
				{
					AcknowledgedId = this.Id
				}));
			}
			catch (PrimarySnapInDataException)
			{
			}
		}

		// Token: 0x06001B3D RID: 6973
		public abstract void Execute(IExchangeExtensionSnapIn snapIn);

		// Token: 0x04000A1E RID: 2590
		private Guid id = default(Guid);
	}
}
