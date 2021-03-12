using System;
using System.Text;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000082 RID: 130
	internal class SenderNotify : TransportAction
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x00015354 File Offset: 0x00013554
		public SenderNotify(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0001535D File Offset: 0x0001355D
		public override Type[] ArgumentsType
		{
			get
			{
				return SenderNotify.ArgumentTypes;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00015364 File Offset: 0x00013564
		public override TransportActionType Type
		{
			get
			{
				return TransportActionType.RecipientRelated;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x00015367 File Offset: 0x00013567
		public override string Name
		{
			get
			{
				return "SenderNotify";
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0001536E File Offset: 0x0001356E
		public override Version MinimumVersion
		{
			get
			{
				return SenderNotify.SenderNotifyActionVersion;
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00015378 File Offset: 0x00013578
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			if (!TransportUtils.IsInternalMail(transportRulesEvaluationContext))
			{
				return ExecutionControl.Execute;
			}
			SenderNotify.SenderNotifyType senderNotifyType = SenderNotify.SenderNotifyType.NotifyOnly;
			try
			{
				senderNotifyType = (SenderNotify.SenderNotifyType)Enum.Parse(typeof(SenderNotify.SenderNotifyType), (string)base.Arguments[0].GetValue(transportRulesEvaluationContext));
			}
			catch (ArgumentOutOfRangeException)
			{
				string message = TransportRulesStrings.InvalidNotifySenderTypeArgument(base.Arguments[0].GetValue(transportRulesEvaluationContext));
				ExTraceGlobals.TransportRulesEngineTracer.TraceError(0L, message);
				throw new TransportRulePermanentException(message);
			}
			catch (ArgumentException)
			{
				string message2 = TransportRulesStrings.InvalidNotifySenderTypeArgument(base.Arguments[0].GetValue(transportRulesEvaluationContext));
				ExTraceGlobals.TransportRulesEngineTracer.TraceError(0L, message2);
				throw new TransportRulePermanentException(message2);
			}
			catch (OverflowException)
			{
				string message3 = TransportRulesStrings.InvalidNotifySenderTypeArgument(base.Arguments[0].GetValue(transportRulesEvaluationContext));
				ExTraceGlobals.TransportRulesEngineTracer.TraceError(0L, message3);
				throw new TransportRulePermanentException(message3);
			}
			string status = (string)base.Arguments[1].GetValue(transportRulesEvaluationContext);
			string text = (string)base.Arguments[2].GetValue(transportRulesEvaluationContext);
			string reason = (string)base.Arguments[3].GetValue(transportRulesEvaluationContext);
			bool flag = SenderNotify.IsFpHeaderSet(transportRulesEvaluationContext);
			string senderOverrideJustification;
			bool senderOverrideHeader = SenderNotify.GetSenderOverrideHeader(transportRulesEvaluationContext, out senderOverrideJustification);
			switch (senderNotifyType)
			{
			case SenderNotify.SenderNotifyType.NotifyOnly:
				if (flag)
				{
					transportRulesEvaluationContext.FpOverriden = true;
				}
				break;
			case SenderNotify.SenderNotifyType.RejectMessage:
				transportRulesEvaluationContext.ShouldExecuteActions = false;
				return RejectMessage.Reject(transportRulesEvaluationContext, status, flag ? string.Empty : text, reason);
			case SenderNotify.SenderNotifyType.RejectUnlessFalsePositiveOverride:
				if (!flag)
				{
					transportRulesEvaluationContext.ShouldExecuteActions = false;
					return RejectMessage.Reject(transportRulesEvaluationContext, status, text, reason);
				}
				transportRulesEvaluationContext.FpOverriden = true;
				break;
			case SenderNotify.SenderNotifyType.RejectUnlessSilentOverride:
			case SenderNotify.SenderNotifyType.RejectUnlessExplicitOverride:
				if (!flag && !senderOverrideHeader)
				{
					transportRulesEvaluationContext.ShouldExecuteActions = false;
					return RejectMessage.Reject(transportRulesEvaluationContext, status, text, reason);
				}
				if (senderOverrideHeader)
				{
					transportRulesEvaluationContext.SenderOverridden = true;
					transportRulesEvaluationContext.SenderOverrideJustification = senderOverrideJustification;
				}
				if (flag)
				{
					transportRulesEvaluationContext.FpOverriden = true;
				}
				break;
			}
			return ExecutionControl.Execute;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00015598 File Offset: 0x00013798
		internal static bool IsFpHeaderSet(TransportRulesEvaluationContext context)
		{
			return context.Message.Headers["X-Ms-Exchange-Organization-Dlp-FalsePositive"].Count > 0;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000155B8 File Offset: 0x000137B8
		internal static bool GetSenderOverrideHeader(TransportRulesEvaluationContext context, out string senderOverrideHeaderValue)
		{
			bool flag = context.Message.Headers["X-Ms-Exchange-Organization-Dlp-SenderOverrideJustification"].Count > 0;
			senderOverrideHeaderValue = string.Empty;
			if (flag && !SenderNotify.TryGetBase64EncodedValue(context.Message.Headers["X-Ms-Exchange-Organization-Dlp-SenderOverrideJustification"][0], SenderNotify.MaxJustificationLength, out senderOverrideHeaderValue))
			{
				ExTraceGlobals.TransportRulesEngineTracer.TraceError<string, string>(0L, "Header {0} is not a valid base-64 encoded text \"{1}\".", "X-Ms-Exchange-Organization-Dlp-SenderOverrideJustification", context.Message.Headers["X-Ms-Exchange-Organization-Dlp-SenderOverrideJustification"][0]);
			}
			return flag;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00015648 File Offset: 0x00013848
		internal static bool TryGetBase64EncodedValue(string base64EncodedString, int maxLength, out string value)
		{
			value = string.Empty;
			try
			{
				byte[] bytes = Convert.FromBase64String(base64EncodedString);
				value = Encoding.Unicode.GetString(bytes);
				if (value.Length > maxLength)
				{
					value = value.Substring(0, maxLength);
				}
			}
			catch (FormatException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x04000263 RID: 611
		private static readonly int MaxJustificationLength = 100;

		// Token: 0x04000264 RID: 612
		public static readonly Version SenderNotifyActionVersion = new Version("15.00.0002.000");

		// Token: 0x04000265 RID: 613
		private static readonly Type[] ArgumentTypes = new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string)
		};

		// Token: 0x02000083 RID: 131
		public enum SenderNotifyType
		{
			// Token: 0x04000267 RID: 615
			NotifyOnly = 1,
			// Token: 0x04000268 RID: 616
			RejectMessage,
			// Token: 0x04000269 RID: 617
			RejectUnlessFalsePositiveOverride,
			// Token: 0x0400026A RID: 618
			RejectUnlessSilentOverride,
			// Token: 0x0400026B RID: 619
			RejectUnlessExplicitOverride
		}
	}
}
