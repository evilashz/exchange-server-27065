using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000077 RID: 119
	internal class ModerateMessageByUser : TransportAction
	{
		// Token: 0x060003B8 RID: 952 RVA: 0x00014613 File Offset: 0x00012813
		public ModerateMessageByUser(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0001461C File Offset: 0x0001281C
		public override Version MinimumVersion
		{
			get
			{
				return TransportRuleConstants.VersionedContainerBaseVersion;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003BA RID: 954 RVA: 0x00014623 File Offset: 0x00012823
		public override string Name
		{
			get
			{
				return "ModerateMessageByUser";
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0001462A File Offset: 0x0001282A
		public override TransportActionType Type
		{
			get
			{
				return TransportActionType.BifurcationNeeded;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0001462D File Offset: 0x0001282D
		public override Type[] ArgumentsType
		{
			get
			{
				return ModerateMessageByUser.argumentTypes;
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00014634 File Offset: 0x00012834
		protected virtual string GetModeratorList(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext context = (TransportRulesEvaluationContext)baseContext;
			return (string)base.Arguments[0].GetValue(context);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00014660 File Offset: 0x00012860
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			MailItem mailItem = transportRulesEvaluationContext.MailItem;
			if (mailItem.Message.MapiMessageClass.StartsWith("IPM.Note.Microsoft.Approval", StringComparison.OrdinalIgnoreCase))
			{
				return ExecutionControl.Execute;
			}
			string moderatorList = this.GetModeratorList(transportRulesEvaluationContext);
			if (string.IsNullOrEmpty(moderatorList))
			{
				return ExecutionControl.Execute;
			}
			TransportRulesEvaluator.AddRuleToExecutionHistory(transportRulesEvaluationContext);
			ITransportMailItemFacade transportMailItemFacade = TransportUtils.GetTransportMailItemFacade(transportRulesEvaluationContext.MailItem);
			OrganizationId organizationId = (OrganizationId)transportMailItemFacade.OrganizationIdAsObject;
			try
			{
				SmtpResponse comparand = TransportFacades.CreateAndSubmitApprovalInitiationForTransportRules(transportMailItemFacade, transportRulesEvaluationContext.MailItem.FromAddress.ToString(), moderatorList, transportRulesEvaluationContext.RuleName);
				if (!SmtpResponse.Empty.Equals(comparand))
				{
					return RejectMessage.Reject(transportRulesEvaluationContext, comparand.StatusCode, comparand.EnhancedStatusCode, comparand.StatusText[0]);
				}
			}
			catch (ExchangeDataException arg)
			{
				ExTraceGlobals.TransportRulesEngineTracer.TraceError<ExchangeDataException>(0L, "Approval initiation not created due to ExchangeDataException {0}.  NDRing the message.", arg);
				return RejectMessage.Reject(transportRulesEvaluationContext, ModerateMessageByUser.InvalidContentForModeration.StatusCode, ModerateMessageByUser.InvalidContentForModeration.EnhancedStatusCode, ModerateMessageByUser.InvalidContentForModeration.StatusText[0]);
			}
			return DeleteMessage.Delete(transportRulesEvaluationContext, ModerateMessageByUser.TrackingLogResponse);
		}

		// Token: 0x04000257 RID: 599
		private static readonly SmtpResponse TrackingLogResponse = new SmtpResponse("550", "5.2.1", new string[]
		{
			"Message sent for moderation by the transport rules agent"
		});

		// Token: 0x04000258 RID: 600
		private static readonly SmtpResponse InvalidContentForModeration = new SmtpResponse(SmtpResponse.InvalidContent.StatusCode, SmtpResponse.InvalidContent.EnhancedStatusCode, new string[]
		{
			"Rules.MT.Content; invalid message content"
		});

		// Token: 0x04000259 RID: 601
		private static Type[] argumentTypes = new Type[]
		{
			typeof(string)
		};
	}
}
