using System;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.ManagementConsole.Advanced;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x02000283 RID: 643
	[Serializable]
	public abstract class NotificationSharedDataCommand : ExchangeSharedDataCommand
	{
		// Token: 0x06001B3F RID: 6975 RVA: 0x000781A0 File Offset: 0x000763A0
		public override void ExtensionExecute(IExchangeExtensionSnapIn snapIn)
		{
			try
			{
				snapIn.SharedDataItem.RequestDataUpdate(WinformsHelper.Serialize(this));
			}
			catch (PrimarySnapInDataException)
			{
			}
		}
	}
}
