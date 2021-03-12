using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000067 RID: 103
	internal class GenerateIncidentReport : TransportAction
	{
		// Token: 0x06000360 RID: 864 RVA: 0x0001315B File Offset: 0x0001135B
		public GenerateIncidentReport(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00013164 File Offset: 0x00011364
		public override TransportActionType Type
		{
			get
			{
				return TransportActionType.RecipientRelated;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000362 RID: 866 RVA: 0x00013167 File Offset: 0x00011367
		public override string Name
		{
			get
			{
				return "GenerateIncidentReport";
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0001316E File Offset: 0x0001136E
		public override Version MinimumVersion
		{
			get
			{
				return GenerateIncidentReport.GenerateIncidentReportActionVersion;
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0001318C File Offset: 0x0001138C
		public override void ValidateArguments(ShortList<Argument> inputArguments)
		{
			if (inputArguments.Count < 1)
			{
				throw new RulesValidationException(RulesStrings.ActionArgumentMismatch(this.Name));
			}
			if (inputArguments.Any((Argument argument) => argument.Type != typeof(string)))
			{
				throw new RulesValidationException(RulesStrings.ActionArgumentMismatch(this.Name));
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000131E9 File Offset: 0x000113E9
		public override bool ShouldExecute(RuleMode mode, RulesEvaluationContext context)
		{
			return true;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x000131EC File Offset: 0x000113EC
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			if (IncidentReport.IsIncidentReport(transportRulesEvaluationContext.MailItem.Message))
			{
				TransportRulesEvaluator.Trace(transportRulesEvaluationContext.TransportRulesTracer, transportRulesEvaluationContext.MailItem, "GenerateIncidentReport: Skipping incident report generation on an incident report message");
				return ExecutionControl.Execute;
			}
			if (TransportRulesLoopChecker.IsIncidentReportLoopCountExceeded(transportRulesEvaluationContext.MailItem))
			{
				TransportRulesEvaluator.Trace(transportRulesEvaluationContext.TransportRulesTracer, transportRulesEvaluationContext.MailItem, "GenerateIncidentReport: Message loop count limit exceeded. Skipping incident report generation");
				return ExecutionControl.Execute;
			}
			string text = (string)base.Arguments[0].GetValue(transportRulesEvaluationContext);
			if (!SmtpAddress.IsValidSmtpAddress(text))
			{
				string text2 = TransportRulesStrings.InvalidReportDestinationArgument(base.Arguments[0].GetValue(transportRulesEvaluationContext));
				TransportRulesEvaluator.Trace(transportRulesEvaluationContext.TransportRulesTracer, transportRulesEvaluationContext.MailItem, "GenerateIncidentReport error: " + text2);
				throw new TransportRulePermanentException(text2);
			}
			List<IncidentReportContent> list = new List<IncidentReportContent>();
			IncidentReportOriginalMail incidentReportOriginalMail;
			if (base.Arguments.Count == 2 && GenerateIncidentReport.TryGetIncidentReportOriginalMailParameter((string)base.Arguments[1].GetValue(transportRulesEvaluationContext), out incidentReportOriginalMail))
			{
				list = GenerateIncidentReport.GetLegacyContentItems(incidentReportOriginalMail);
				if (incidentReportOriginalMail == IncidentReportOriginalMail.DoNotIncludeOriginalMail)
				{
					list.Remove(IncidentReportContent.AttachOriginalMail);
				}
			}
			else
			{
				for (int i = 1; i < base.Arguments.Count; i++)
				{
					IncidentReportContent item;
					if (!Enum.TryParse<IncidentReportContent>((string)base.Arguments[i].GetValue(transportRulesEvaluationContext), out item))
					{
						TransportRulesEvaluator.Trace(transportRulesEvaluationContext.TransportRulesTracer, transportRulesEvaluationContext.MailItem, string.Format("GenerateIncidentReport: Unrecognized incident report content item '{0}'. Item is ignored.", (string)base.Arguments[i].GetValue(transportRulesEvaluationContext)));
					}
					else
					{
						list.Add(item);
					}
				}
			}
			ITransportMailItemFacade transportMailItemFacade = IncidentReport.CreateReport(text, list, transportRulesEvaluationContext);
			TransportRulesLoopChecker.StampLoopCountHeader(TransportRulesLoopChecker.GetMessageLoopCount(transportRulesEvaluationContext.MailItem) + 1, (TransportMailItem)transportMailItemFacade);
			transportMailItemFacade.CommitLazy();
			TransportFacades.TrackReceiveByAgent(transportMailItemFacade, "Transport Rule", null, new long?(transportMailItemFacade.RecordId));
			Components.CategorizerComponent.EnqueueSideEffectMessage(transportRulesEvaluationContext.MailItem, (TransportMailItem)transportMailItemFacade, "Transport Rule Agent");
			return ExecutionControl.Execute;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000133D8 File Offset: 0x000115D8
		internal bool IsLegacyFormat(TransportRulesEvaluationContext context)
		{
			IncidentReportOriginalMail incidentReportOriginalMail;
			return base.Arguments.Count == 2 && GenerateIncidentReport.TryGetIncidentReportOriginalMailParameter((string)base.Arguments[1].GetValue(context), out incidentReportOriginalMail);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00013413 File Offset: 0x00011613
		internal static bool TryGetIncidentReportOriginalMailParameter(string value, out IncidentReportOriginalMail result)
		{
			result = IncidentReportOriginalMail.DoNotIncludeOriginalMail;
			return !string.IsNullOrEmpty(value) && Enum.TryParse<IncidentReportOriginalMail>(value, out result);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0001342C File Offset: 0x0001162C
		internal static List<IncidentReportContent> GetLegacyContentItems(IncidentReportOriginalMail legacyReportOriginalMail)
		{
			List<IncidentReportContent> list = new List<IncidentReportContent>
			{
				IncidentReportContent.Sender,
				IncidentReportContent.Recipients,
				IncidentReportContent.Subject,
				IncidentReportContent.Cc,
				IncidentReportContent.Bcc,
				IncidentReportContent.Severity,
				IncidentReportContent.Override,
				IncidentReportContent.RuleDetections,
				IncidentReportContent.FalsePositive,
				IncidentReportContent.DataClassifications,
				IncidentReportContent.IdMatch
			};
			if (legacyReportOriginalMail == IncidentReportOriginalMail.IncludeOriginalMail)
			{
				list.Add(IncidentReportContent.AttachOriginalMail);
			}
			return list;
		}

		// Token: 0x04000227 RID: 551
		public static readonly Version GenerateIncidentReportActionVersion = new Version("15.00.0005.03");
	}
}
