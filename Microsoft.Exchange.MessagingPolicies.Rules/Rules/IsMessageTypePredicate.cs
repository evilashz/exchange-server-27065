using System;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200009E RID: 158
	internal class IsMessageTypePredicate : PredicateCondition
	{
		// Token: 0x0600047C RID: 1148 RVA: 0x00016A32 File Offset: 0x00014C32
		public IsMessageTypePredicate(ShortList<string> entries, RulesCreationContext creationContext) : base(null, entries, creationContext)
		{
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x00016A3D File Offset: 0x00014C3D
		public override string Name
		{
			get
			{
				return "isMessageType";
			}
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00016A44 File Offset: 0x00014C44
		protected override Value BuildValue(ShortList<string> entries, RulesCreationContext creationContext)
		{
			return Value.CreateValue(typeof(string), entries);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00016A58 File Offset: 0x00014C58
		public override bool Evaluate(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			transportRulesEvaluationContext.PredicateName = this.Name;
			string text = (string)base.Value.GetValue(transportRulesEvaluationContext);
			string key;
			switch (key = text)
			{
			case "OOF":
				return TransportUtils.IsOof(transportRulesEvaluationContext.MailItem.Message);
			case "AutoForward":
				return TransportUtils.IsAutoForward(transportRulesEvaluationContext.MailItem.Message);
			case "Encrypted":
				return TransportUtils.IsEncrypted(transportRulesEvaluationContext.MailItem.Message);
			case "Calendaring":
				return TransportUtils.IsCalendaring(transportRulesEvaluationContext.MailItem.Message);
			case "PermissionControlled":
				return TransportUtils.IsPermissionControlled(transportRulesEvaluationContext.MailItem);
			case "Voicemail":
				return TransportFacades.IsVoicemail(transportRulesEvaluationContext.MailItem.Message);
			case "Signed":
				return TransportUtils.IsSigned(transportRulesEvaluationContext.MailItem.Message);
			case "ApprovalRequest":
				return TransportUtils.IsApprovalRequest(transportRulesEvaluationContext.MailItem.Message);
			case "ReadReceipt":
				return TransportUtils.IsReadReceipt(transportRulesEvaluationContext.MailItem.Message);
			}
			return false;
		}
	}
}
