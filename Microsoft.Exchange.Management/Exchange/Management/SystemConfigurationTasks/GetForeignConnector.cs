using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B2A RID: 2858
	[Cmdlet("Get", "ForeignConnector", DefaultParameterSetName = "Identity")]
	public sealed class GetForeignConnector : GetSystemConfigurationObjectTask<ForeignConnectorIdParameter, ForeignConnector>
	{
		// Token: 0x17001F9A RID: 8090
		// (get) Token: 0x060066DC RID: 26332 RVA: 0x001A9274 File Offset: 0x001A7474
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001F9B RID: 8091
		// (get) Token: 0x060066DD RID: 26333 RVA: 0x001A9278 File Offset: 0x001A7478
		protected override ObjectId RootId
		{
			get
			{
				IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
				return configurationSession.GetOrgContainerId().GetChildId("Administrative Groups");
			}
		}

		// Token: 0x060066DE RID: 26334 RVA: 0x001A92A4 File Offset: 0x001A74A4
		protected override void WriteResult(IConfigurable dataObject)
		{
			ForeignConnector foreignConnector = dataObject as ForeignConnector;
			if (foreignConnector != null && !foreignConnector.IsReadOnly)
			{
				foreignConnector.IsScopedConnector = foreignConnector.GetScopedConnector();
				foreignConnector.ResetChangeTracking();
			}
			base.WriteResult(dataObject);
		}
	}
}
