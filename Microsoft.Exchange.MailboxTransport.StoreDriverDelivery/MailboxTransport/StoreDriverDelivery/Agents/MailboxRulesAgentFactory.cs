using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.StoreDriverDelivery;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.MailboxRules;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x0200008D RID: 141
	internal class MailboxRulesAgentFactory : StoreDriverDeliveryAgentFactory
	{
		// Token: 0x060004E2 RID: 1250 RVA: 0x00019C70 File Offset: 0x00017E70
		public MailboxRulesAgentFactory()
		{
			try
			{
				RuleConfig.Load();
			}
			catch (TransportComponentLoadFailedException ex)
			{
				throw new ExchangeConfigurationException(ex.Message, ex.InnerException);
			}
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00019CB0 File Offset: 0x00017EB0
		public override void Close()
		{
			RuleConfig.UnLoad();
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00019CB7 File Offset: 0x00017EB7
		public override StoreDriverDeliveryAgent CreateAgent(SmtpServer server)
		{
			return new MailboxRulesAgent(server);
		}
	}
}
