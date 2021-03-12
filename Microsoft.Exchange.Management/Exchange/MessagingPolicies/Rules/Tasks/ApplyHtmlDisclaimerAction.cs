using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B86 RID: 2950
	[Serializable]
	public class ApplyHtmlDisclaimerAction : TransportRuleAction, IEquatable<ApplyHtmlDisclaimerAction>
	{
		// Token: 0x06006F2E RID: 28462 RVA: 0x001C86F4 File Offset: 0x001C68F4
		public override int GetHashCode()
		{
			return this.Location.GetHashCode() ^ this.Text.GetHashCode() ^ this.FallbackAction.GetHashCode();
		}

		// Token: 0x06006F2F RID: 28463 RVA: 0x001C8737 File Offset: 0x001C6937
		public override bool Equals(object right)
		{
			return !object.ReferenceEquals(right, null) && (object.ReferenceEquals(this, right) || (!(base.GetType() != right.GetType()) && this.Equals(right as ApplyHtmlDisclaimerAction)));
		}

		// Token: 0x06006F30 RID: 28464 RVA: 0x001C8770 File Offset: 0x001C6970
		public bool Equals(ApplyHtmlDisclaimerAction other)
		{
			return this.Location.Equals(other.Location) && this.Text.Equals(other.Text) && this.FallbackAction.Equals(other.FallbackAction);
		}

		// Token: 0x170022BA RID: 8890
		// (get) Token: 0x06006F31 RID: 28465 RVA: 0x001C87CD File Offset: 0x001C69CD
		// (set) Token: 0x06006F32 RID: 28466 RVA: 0x001C87D5 File Offset: 0x001C69D5
		[ActionParameterName("ApplyHtmlDisclaimerLocation")]
		[LocDisplayName(RulesTasksStrings.IDs.DisclaimerLocationDisplayName)]
		[LocDescription(RulesTasksStrings.IDs.DisclaimerLocationDescription)]
		public DisclaimerLocation Location
		{
			get
			{
				return this.disclaimerLocation;
			}
			set
			{
				this.disclaimerLocation = value;
			}
		}

		// Token: 0x170022BB RID: 8891
		// (get) Token: 0x06006F33 RID: 28467 RVA: 0x001C87DE File Offset: 0x001C69DE
		// (set) Token: 0x06006F34 RID: 28468 RVA: 0x001C87E6 File Offset: 0x001C69E6
		[ActionParameterName("ApplyHtmlDisclaimerText")]
		[LocDescription(RulesTasksStrings.IDs.DisclaimerTextDescription)]
		[LocDisplayName(RulesTasksStrings.IDs.DisclaimerTextDisplayName)]
		public DisclaimerText Text
		{
			get
			{
				return this.disclaimerText;
			}
			set
			{
				this.disclaimerText = value;
			}
		}

		// Token: 0x170022BC RID: 8892
		// (get) Token: 0x06006F35 RID: 28469 RVA: 0x001C87EF File Offset: 0x001C69EF
		// (set) Token: 0x06006F36 RID: 28470 RVA: 0x001C87F7 File Offset: 0x001C69F7
		[ActionParameterName("ApplyHtmlDisclaimerFallbackAction")]
		[LocDescription(RulesTasksStrings.IDs.FallbackActionDescription)]
		[LocDisplayName(RulesTasksStrings.IDs.FallbackActionDisplayName)]
		public DisclaimerFallbackAction FallbackAction
		{
			get
			{
				return this.fallbackAction;
			}
			set
			{
				this.fallbackAction = value;
			}
		}

		// Token: 0x170022BD RID: 8893
		// (get) Token: 0x06006F37 RID: 28471 RVA: 0x001C8800 File Offset: 0x001C6A00
		internal override string Description
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(200);
				if (this.Location == DisclaimerLocation.Prepend)
				{
					stringBuilder.Append(RulesTasksStrings.RuleDescriptionPrependHtmlDisclaimer(this.Text.ToString()));
				}
				else
				{
					stringBuilder.Append(RulesTasksStrings.RuleDescriptionApplyHtmlDisclaimer(this.Text.ToString()));
				}
				if (this.FallbackAction == DisclaimerFallbackAction.Wrap)
				{
					stringBuilder.Append(" ");
					stringBuilder.Append(RulesTasksStrings.RuleDescriptionDisclaimerWrapFallback);
				}
				else if (this.FallbackAction == DisclaimerFallbackAction.Ignore)
				{
					stringBuilder.Append(" ");
					stringBuilder.Append(RulesTasksStrings.RuleDescriptionDisclaimerIgnoreFallback);
				}
				else if (this.FallbackAction == DisclaimerFallbackAction.Reject)
				{
					stringBuilder.Append(" ");
					stringBuilder.Append(RulesTasksStrings.RuleDescriptionDisclaimerRejectFallback);
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06006F38 RID: 28472 RVA: 0x001C88E8 File Offset: 0x001C6AE8
		internal static TransportRuleAction CreateFromInternalAction(Action action)
		{
			if (action.Name != "ApplyHtmlDisclaimer")
			{
				return null;
			}
			DisclaimerLocation location;
			DisclaimerText text2;
			DisclaimerFallbackAction disclaimerFallbackAction;
			try
			{
				string text = TransportRuleAction.GetStringValue(action.Arguments[0]);
				if (text.Equals("Inline", StringComparison.InvariantCultureIgnoreCase))
				{
					text = "Append";
				}
				location = (DisclaimerLocation)Enum.Parse(typeof(DisclaimerLocation), text);
				text2 = new DisclaimerText(TransportRuleAction.GetStringValue(action.Arguments[1]));
				disclaimerFallbackAction = (DisclaimerFallbackAction)Enum.Parse(typeof(DisclaimerFallbackAction), TransportRuleAction.GetStringValue(action.Arguments[2]));
			}
			catch (ArgumentException)
			{
				return null;
			}
			catch (OverflowException)
			{
				return null;
			}
			return new ApplyHtmlDisclaimerAction
			{
				Location = location,
				Text = text2,
				FallbackAction = disclaimerFallbackAction
			};
		}

		// Token: 0x06006F39 RID: 28473 RVA: 0x001C89D0 File Offset: 0x001C6BD0
		internal override void Reset()
		{
			this.disclaimerLocation = DisclaimerLocation.Append;
			this.disclaimerText = DisclaimerText.Empty;
			this.fallbackAction = DisclaimerFallbackAction.Wrap;
			base.Reset();
		}

		// Token: 0x06006F3A RID: 28474 RVA: 0x001C89F4 File Offset: 0x001C6BF4
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (this.disclaimerText == DisclaimerText.Empty)
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.ArgumentNotSet, base.Name));
				return;
			}
			int index;
			if (!Utils.CheckIsUnicodeStringWellFormed(this.disclaimerText.Value, out index))
			{
				errors.Add(new RulePhrase.RulePhraseValidationError(RulesTasksStrings.CommentsHaveInvalidChars((int)this.disclaimerText.Value[index]), base.Name));
				return;
			}
			base.ValidateRead(errors);
		}

		// Token: 0x06006F3B RID: 28475 RVA: 0x001C8A70 File Offset: 0x001C6C70
		internal override Action ToInternalAction()
		{
			ShortList<Argument> arguments = new ShortList<Argument>
			{
				new Value(this.disclaimerLocation.ToString()),
				new Value(this.disclaimerText.ToString()),
				new Value(this.fallbackAction.ToString())
			};
			return TransportRuleParser.Instance.CreateAction("ApplyHtmlDisclaimer", arguments, Utils.GetActionName(this));
		}

		// Token: 0x06006F3C RID: 28476 RVA: 0x001C8AF0 File Offset: 0x001C6CF0
		internal override string ToCmdletParameter()
		{
			return string.Format("-{0} {1} -{2} {3} -{4} {5}", new object[]
			{
				"ApplyHtmlDisclaimerLocation",
				Enum.GetName(typeof(DisclaimerLocation), this.Location),
				"ApplyHtmlDisclaimerFallbackAction",
				Enum.GetName(typeof(DisclaimerFallbackAction), this.FallbackAction),
				"ApplyHtmlDisclaimerText",
				Utils.QuoteCmdletParameter(this.Text.ToString())
			});
		}

		// Token: 0x06006F3D RID: 28477 RVA: 0x001C8B7D File Offset: 0x001C6D7D
		internal override void SuppressPiiData()
		{
			this.Text = SuppressingPiiProperty.TryRedactValue<DisclaimerText>(RuleSchema.ApplyHtmlDisclaimerText, this.Text);
		}

		// Token: 0x040039AF RID: 14767
		private DisclaimerLocation disclaimerLocation;

		// Token: 0x040039B0 RID: 14768
		private DisclaimerText disclaimerText;

		// Token: 0x040039B1 RID: 14769
		private DisclaimerFallbackAction fallbackAction;
	}
}
