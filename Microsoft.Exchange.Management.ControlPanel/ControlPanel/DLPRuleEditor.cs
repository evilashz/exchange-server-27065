using System;
using System.Linq;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001C1 RID: 449
	public class DLPRuleEditor : TransportRuleEditor
	{
		// Token: 0x17001B19 RID: 6937
		// (get) Token: 0x06002452 RID: 9298 RVA: 0x0006FB5D File Offset: 0x0006DD5D
		// (set) Token: 0x06002453 RID: 9299 RVA: 0x0006FB65 File Offset: 0x0006DD65
		public string DlpPolicy
		{
			get
			{
				return this.dlpPolicy;
			}
			set
			{
				this.dlpPolicy = value;
			}
		}

		// Token: 0x17001B1A RID: 6938
		// (get) Token: 0x06002454 RID: 9300 RVA: 0x0006FB6E File Offset: 0x0006DD6E
		// (set) Token: 0x06002455 RID: 9301 RVA: 0x0006FB76 File Offset: 0x0006DD76
		public string DlpPolicyMode
		{
			get
			{
				return this.dlpPolicyMode;
			}
			set
			{
				this.dlpPolicyMode = value;
			}
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x0006FB97 File Offset: 0x0006DD97
		protected override RulePhrase[] FilterConditions(RulePhrase[] conditions)
		{
			Array.ForEach<RulePhrase>(conditions, delegate(RulePhrase r)
			{
				r.DisplayedInSimpleMode = DLPRuleEditor.simpleModeConditions.Contains(r.Name);
			});
			return conditions;
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x0006FBD5 File Offset: 0x0006DDD5
		protected override RulePhrase[] FilterActions(RulePhrase[] actions)
		{
			Array.ForEach<RulePhrase>(actions, delegate(RulePhrase r)
			{
				r.DisplayedInSimpleMode = DLPRuleEditor.simpleModeActions.Contains(r.Name);
			});
			return actions;
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x0006FBFB File Offset: 0x0006DDFB
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			descriptor.AddProperty("DLPPolicy", this.DlpPolicy, true);
			descriptor.AddProperty("DLPPolicyMode", this.DlpPolicyMode, true);
			base.BuildScriptDescriptor(descriptor);
		}

		// Token: 0x04001E6A RID: 7786
		private static readonly string[] simpleModeConditions = new string[]
		{
			"From",
			"SentTo",
			"FromScope",
			"SentToScope",
			"FromMemberOf",
			"SentToMemberOf",
			"SubjectOrBodyContains",
			"FromAddressContains",
			"RecipientAddressContains",
			"AttachmentContainsWords",
			"MessageContainsDataClassifications",
			"HasSenderOverride"
		};

		// Token: 0x04001E6B RID: 7787
		private static readonly string[] simpleModeActions = new string[]
		{
			"ModerateMessageByUser",
			"RedirectMessage",
			"RejectMessage",
			"DeleteMessage",
			"BlindCopyTo",
			"AppendHtmlDisclaimer",
			"NotifySender",
			"GenerateIncidentReport"
		};

		// Token: 0x04001E6C RID: 7788
		private string dlpPolicy = string.Empty;

		// Token: 0x04001E6D RID: 7789
		private string dlpPolicyMode = string.Empty;
	}
}
