using System;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Data.Transport.StoreDriver;
using Microsoft.Exchange.Data.Transport.StoreDriverDelivery;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x02000023 RID: 35
	internal sealed class InterceptorStoreDriverDeliveryAgent : StoreDriverDeliveryAgent
	{
		// Token: 0x0600014A RID: 330 RVA: 0x0000749C File Offset: 0x0000569C
		public InterceptorStoreDriverDeliveryAgent(FilteredRuleCache filteredRuleCache)
		{
			this.filteredRuleCache = filteredRuleCache;
			base.OnInitializedMessage += this.InitMsgEventHandler;
			base.OnPromotedMessage += this.PromotedMessageEventHandler;
			base.OnCreatedMessage += this.CreatedMessageEventHandler;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000074EC File Offset: 0x000056EC
		private void InitMsgEventHandler(StoreDriverEventSource source, StoreDriverDeliveryEventArgs e)
		{
			this.HandleMessage(source, e, InterceptorAgentEvent.OnInitMsg);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000074FB File Offset: 0x000056FB
		private void PromotedMessageEventHandler(StoreDriverEventSource source, StoreDriverDeliveryEventArgs e)
		{
			this.HandleMessage(source, e, InterceptorAgentEvent.OnPromotedMessage);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000750A File Offset: 0x0000570A
		private void CreatedMessageEventHandler(StoreDriverEventSource source, StoreDriverDeliveryEventArgs e)
		{
			this.HandleMessage(source, e, InterceptorAgentEvent.OnCreatedMessage);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000755C File Offset: 0x0000575C
		private void HandleMessage(StoreDriverEventSource source, StoreDriverDeliveryEventArgs e, InterceptorAgentEvent evt)
		{
			InterceptorAgentRule interceptorAgentRule = InterceptorAgentRuleEvaluator.Evaluate(this.filteredRuleCache.Rules, e, evt);
			if (interceptorAgentRule == null)
			{
				return;
			}
			interceptorAgentRule.PerformAction(new DeliverableMailItemWrapper(e.MailItem), delegate
			{
				throw new SmtpResponseException(new SmtpResponse("250", "2.7.0", new string[]
				{
					"STOREDRV.Deliver; message deleted by administrative rule"
				}), "Interceptor Store Driver Delivery Agent");
			}, delegate(SmtpResponse response)
			{
				throw new SmtpResponseException(response);
			}, null);
		}

		// Token: 0x040000C1 RID: 193
		private readonly FilteredRuleCache filteredRuleCache;
	}
}
