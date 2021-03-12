using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200007C RID: 124
	internal class RejectMessage : TransportAction
	{
		// Token: 0x060003D8 RID: 984 RVA: 0x00014B7B File Offset: 0x00012D7B
		public RejectMessage(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00014B84 File Offset: 0x00012D84
		public override Type[] ArgumentsType
		{
			get
			{
				return RejectMessage.argumentTypes;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00014B8B File Offset: 0x00012D8B
		public override TransportActionType Type
		{
			get
			{
				return TransportActionType.RecipientRelated;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003DB RID: 987 RVA: 0x00014B8E File Offset: 0x00012D8E
		public override string Name
		{
			get
			{
				return "RejectMessage";
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00014B98 File Offset: 0x00012D98
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			string status = (string)base.Arguments[0].GetValue(transportRulesEvaluationContext);
			string enhancedStatus = (string)base.Arguments[1].GetValue(transportRulesEvaluationContext);
			string reason = (string)base.Arguments[2].GetValue(transportRulesEvaluationContext);
			transportRulesEvaluationContext.ShouldExecuteActions = false;
			return RejectMessage.Reject(transportRulesEvaluationContext, status, enhancedStatus, reason);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00014C04 File Offset: 0x00012E04
		internal static ExecutionControl Reject(TransportRulesEvaluationContext context, string status, string enhancedStatus, string reason)
		{
			SmtpResponse smtpResponse = new SmtpResponse(status, enhancedStatus, reason, true, new string[]
			{
				RejectMessage.responseDebugInfo
			});
			if (context.EventType == EventType.EndOfData)
			{
				ExTraceGlobals.TransportRulesEngineTracer.TraceDebug(0L, "Message is rejected at EOD");
				context.EdgeRejectResponse = new SmtpResponse?(smtpResponse);
				return ExecutionControl.Execute;
			}
			ExTraceGlobals.TransportRulesEngineTracer.TraceDebug(0L, "Message is rejected at OnRoutedMessage");
			if (context.MatchedRecipients == null)
			{
				List<EnvelopeRecipient> list = new List<EnvelopeRecipient>(context.MailItem.Recipients.Count);
				foreach (EnvelopeRecipient item in context.MailItem.Recipients)
				{
					list.Add(item);
				}
				foreach (EnvelopeRecipient envelopeRecipient in list)
				{
					envelopeRecipient.Properties["Microsoft.Exchange.DsnGenerator.DsnSource"] = DsnSource.TransportRuleAgent;
					context.MailItem.Recipients.Remove(envelopeRecipient, DsnType.Failure, smtpResponse);
				}
				return ExecutionControl.Execute;
			}
			foreach (EnvelopeRecipient envelopeRecipient2 in context.MatchedRecipients)
			{
				envelopeRecipient2.Properties["Microsoft.Exchange.DsnGenerator.DsnSource"] = DsnSource.TransportRuleAgent;
				context.MailItem.Recipients.Remove(envelopeRecipient2, DsnType.Failure, smtpResponse);
			}
			context.MatchedRecipients.Clear();
			return ExecutionControl.Execute;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00014DAC File Offset: 0x00012FAC
		internal static void Reject(MailItem mailItem, string status, string enhancedStatus, string reason)
		{
			SmtpResponse smtpResponse = new SmtpResponse(status, enhancedStatus, reason, true, new string[]
			{
				RejectMessage.responseDebugInfo
			});
			List<EnvelopeRecipient> list = new List<EnvelopeRecipient>(mailItem.Recipients.Count);
			list.AddRange(mailItem.Recipients);
			foreach (EnvelopeRecipient recipient in list)
			{
				mailItem.Recipients.Remove(recipient, DsnType.Failure, smtpResponse);
			}
		}

		// Token: 0x0400025D RID: 605
		private static Type[] argumentTypes = new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(string)
		};

		// Token: 0x0400025E RID: 606
		private static string responseDebugInfo = "TRANSPORT.RULES.RejectMessage; the message was rejected by organization policy";
	}
}
