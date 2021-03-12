using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000062 RID: 98
	internal class DeleteMessage : TransportAction
	{
		// Token: 0x06000351 RID: 849 RVA: 0x00012EF7 File Offset: 0x000110F7
		public DeleteMessage(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00012F00 File Offset: 0x00011100
		public override string Name
		{
			get
			{
				return "DeleteMessage";
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00012F07 File Offset: 0x00011107
		public override TransportActionType Type
		{
			get
			{
				return TransportActionType.RecipientRelated;
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00012F0C File Offset: 0x0001110C
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			transportRulesEvaluationContext.ShouldExecuteActions = false;
			return DeleteMessage.Delete(transportRulesEvaluationContext, DeleteMessage.TrackingLogResponse);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00012F34 File Offset: 0x00011134
		internal static ExecutionControl Delete(TransportRulesEvaluationContext context, SmtpResponse trackingLogResponse)
		{
			if (context.EventType == EventType.EndOfData)
			{
				ExTraceGlobals.TransportRulesEngineTracer.TraceDebug(0L, "Message is deleted at EOD");
				context.EdgeRejectResponse = new SmtpResponse?(SmtpResponse.QueuedMailForDelivery(context.MailItem.Message.MimeDocument.RootPart.Headers.FindFirst("Message-Id").Value));
				return ExecutionControl.Execute;
			}
			TransportRulesEvaluator.Trace(context.TransportRulesTracer, context.MailItem, "Message is deleted at OnRoutedMessage");
			if (context.MatchedRecipients == null)
			{
				List<EnvelopeRecipient> list = new List<EnvelopeRecipient>(context.MailItem.Recipients);
				foreach (EnvelopeRecipient envelopeRecipient in list)
				{
					envelopeRecipient.Properties["Microsoft.Exchange.DsnGenerator.DsnSource"] = DsnSource.TransportRuleAgent;
					context.MailItem.Recipients.Remove(envelopeRecipient, DsnType.Expanded, trackingLogResponse);
				}
				return ExecutionControl.Execute;
			}
			foreach (EnvelopeRecipient recipient in context.MatchedRecipients)
			{
				context.MailItem.Recipients.Remove(recipient, DsnType.Expanded, trackingLogResponse);
			}
			context.MatchedRecipients.Clear();
			return ExecutionControl.Execute;
		}

		// Token: 0x04000214 RID: 532
		private static readonly SmtpResponse TrackingLogResponse = new SmtpResponse("550", "5.2.1", new string[]
		{
			"Message deleted by the transport rules agent"
		});
	}
}
