using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.TransportConfig
{
	// Token: 0x020000B5 RID: 181
	[Cmdlet("Get", "TransportConfig")]
	public sealed class GetTransportConfig : GetMultitenancySingletonSystemConfigurationObjectTask<TransportConfigContainer>
	{
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x0001AA1A File Offset: 0x00018C1A
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001AA20 File Offset: 0x00018C20
		protected override void WriteResult(IConfigurable dataObject)
		{
			TransportConfigContainer transportConfigContainer = (TransportConfigContainer)dataObject;
			base.WriteResult(transportConfigContainer);
			IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
			MessageDeliveryGlobalSettings[] array = configurationSession.Find<MessageDeliveryGlobalSettings>(configurationSession.GetOrgContainerId(), QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, MessageDeliveryGlobalSettings.DefaultName), null, 1);
			if (array.Length > 0 && (array[0].MaxReceiveSize != transportConfigContainer.MaxReceiveSize || array[0].MaxSendSize != transportConfigContainer.MaxSendSize || array[0].MaxRecipientEnvelopeLimit != transportConfigContainer.MaxRecipientEnvelopeLimit) && !this.IsPureE12Environment())
			{
				this.WriteWarning(Strings.WarningMessageSizeRestrictionOutOfSync);
			}
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001AAC0 File Offset: 0x00018CC0
		private bool IsPureE12Environment()
		{
			IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
			ADPagedReader<Server> adpagedReader = configurationSession.FindAllPaged<Server>();
			foreach (Server server in adpagedReader)
			{
				if (!server.IsExchange2007OrLater)
				{
					return false;
				}
			}
			return true;
		}
	}
}
