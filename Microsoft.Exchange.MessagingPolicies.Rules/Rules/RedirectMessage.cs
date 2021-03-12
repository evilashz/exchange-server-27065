using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Logging.MessageTracking;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200007B RID: 123
	internal class RedirectMessage : TransportAction
	{
		// Token: 0x060003D0 RID: 976 RVA: 0x0001498A File Offset: 0x00012B8A
		public RedirectMessage(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00014993 File Offset: 0x00012B93
		public override string Name
		{
			get
			{
				return "RedirectMessage";
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0001499A File Offset: 0x00012B9A
		public override Type[] ArgumentsType
		{
			get
			{
				return RedirectMessage.argumentTypes;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x000149A1 File Offset: 0x00012BA1
		public override TransportActionType Type
		{
			get
			{
				return TransportActionType.RecipientRelated;
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x000149A4 File Offset: 0x00012BA4
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			string text = (string)base.Arguments[0].GetValue(transportRulesEvaluationContext);
			RedirectMessage.AddTrackingInfo(transportRulesEvaluationContext, text);
			DeleteMessage.Delete(transportRulesEvaluationContext, new SmtpResponse("550", "5.2.1", new string[]
			{
				string.Format("Message redirected to '{0}' by the transport rules agent", text)
			}));
			ExTraceGlobals.TransportRulesEngineTracer.TraceDebug<string>(0L, "Redirect To: {0}", text);
			transportRulesEvaluationContext.RecipientsToAdd.Add(new TransportRulesEvaluationContext.AddedRecipient(text, text, RecipientP2Type.Redirect));
			return ExecutionControl.Execute;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00014A2C File Offset: 0x00012C2C
		private static void AddTrackingInfo(TransportRulesEvaluationContext context, string redirectAddress)
		{
			ITransportMailItemWrapperFacade transportMailItemWrapperFacade = context.MailItem as ITransportMailItemWrapperFacade;
			if (transportMailItemWrapperFacade != null)
			{
				RoutingAddress redirectDestination = new RoutingAddress(redirectAddress);
				string sourceContext = string.Format("Redirected by transport rule '{0}'", context.CurrentRule.ImmutableId);
				if (context.MatchedRecipients == null)
				{
					List<EnvelopeRecipient> list = new List<EnvelopeRecipient>(context.MailItem.Recipients);
					using (List<EnvelopeRecipient>.Enumerator enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							EnvelopeRecipient envelopeRecipient = enumerator.Current;
							RedirectMessage.TrackRedirect(sourceContext, transportMailItemWrapperFacade, envelopeRecipient.Address, redirectDestination);
						}
						return;
					}
				}
				foreach (EnvelopeRecipient envelopeRecipient2 in context.MatchedRecipients)
				{
					RedirectMessage.TrackRedirect(sourceContext, transportMailItemWrapperFacade, envelopeRecipient2.Address, redirectDestination);
				}
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00014B20 File Offset: 0x00012D20
		private static void TrackRedirect(string sourceContext, ITransportMailItemWrapperFacade tmiFacade, RoutingAddress redirectSource, RoutingAddress redirectDestination)
		{
			MsgTrackRedirectInfo msgTrackInfo = new MsgTrackRedirectInfo(sourceContext, redirectSource, redirectDestination, null);
			MessageTrackingLog.TrackRedirect(MessageTrackingSource.AGENT, (IReadOnlyMailItem)tmiFacade.TransportMailItem, msgTrackInfo);
		}

		// Token: 0x0400025C RID: 604
		private static Type[] argumentTypes = new Type[]
		{
			typeof(string)
		};
	}
}
