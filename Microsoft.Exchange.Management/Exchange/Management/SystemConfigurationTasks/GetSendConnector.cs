using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B30 RID: 2864
	[Cmdlet("Get", "SendConnector", DefaultParameterSetName = "Identity")]
	public sealed class GetSendConnector : GetSystemConfigurationObjectTask<SendConnectorIdParameter, SmtpSendConnectorConfig>
	{
		// Token: 0x17001FA4 RID: 8100
		// (get) Token: 0x060066F4 RID: 26356 RVA: 0x001A9698 File Offset: 0x001A7898
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001FA5 RID: 8101
		// (get) Token: 0x060066F5 RID: 26357 RVA: 0x001A969C File Offset: 0x001A789C
		protected override ObjectId RootId
		{
			get
			{
				IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
				return configurationSession.GetOrgContainerId().GetChildId("Administrative Groups");
			}
		}

		// Token: 0x060066F6 RID: 26358 RVA: 0x001A96C5 File Offset: 0x001A78C5
		protected override void InternalValidate()
		{
			base.InternalValidate();
		}

		// Token: 0x060066F7 RID: 26359 RVA: 0x001A96D0 File Offset: 0x001A78D0
		protected override void WriteResult(IConfigurable dataObject)
		{
			SmtpSendConnectorConfig smtpSendConnectorConfig = dataObject as SmtpSendConnectorConfig;
			if (smtpSendConnectorConfig != null && !smtpSendConnectorConfig.IsReadOnly)
			{
				smtpSendConnectorConfig.DNSRoutingEnabled = string.IsNullOrEmpty(smtpSendConnectorConfig.SmartHostsString);
				smtpSendConnectorConfig.IsScopedConnector = smtpSendConnectorConfig.GetScopedConnector();
				smtpSendConnectorConfig.ResetChangeTracking();
			}
			base.WriteResult(dataObject);
		}

		// Token: 0x0400364A RID: 13898
		public const string rootLocation = "Administrative Groups";
	}
}
