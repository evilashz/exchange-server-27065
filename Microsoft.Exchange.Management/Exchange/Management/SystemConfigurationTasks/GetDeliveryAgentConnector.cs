using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B28 RID: 2856
	[Cmdlet("Get", "DeliveryAgentConnector", DefaultParameterSetName = "Identity")]
	public sealed class GetDeliveryAgentConnector : GetSystemConfigurationObjectTask<DeliveryAgentConnectorIdParameter, DeliveryAgentConnector>
	{
		// Token: 0x17001F98 RID: 8088
		// (get) Token: 0x060066D6 RID: 26326 RVA: 0x001A91C3 File Offset: 0x001A73C3
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001F99 RID: 8089
		// (get) Token: 0x060066D7 RID: 26327 RVA: 0x001A91C8 File Offset: 0x001A73C8
		protected override ObjectId RootId
		{
			get
			{
				IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
				return configurationSession.GetOrgContainerId().GetChildId("Administrative Groups");
			}
		}

		// Token: 0x060066D8 RID: 26328 RVA: 0x001A91F4 File Offset: 0x001A73F4
		protected override void WriteResult(IConfigurable dataObject)
		{
			DeliveryAgentConnector deliveryAgentConnector = dataObject as DeliveryAgentConnector;
			if (deliveryAgentConnector != null && !deliveryAgentConnector.IsReadOnly)
			{
				deliveryAgentConnector.IsScopedConnector = deliveryAgentConnector.GetScopedConnector();
				deliveryAgentConnector.ResetChangeTracking();
			}
			base.WriteResult(dataObject);
		}
	}
}
