using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000425 RID: 1061
	public class NewTransportRulePage : BaseForm
	{
		// Token: 0x0600355B RID: 13659 RVA: 0x000A5A94 File Offset: 0x000A3C94
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (!base.IsPostBack)
			{
				this.CheckIfCopyMode();
				int num = 0;
				if (int.TryParse(base.Request.QueryString["configId"], out num))
				{
					switch (num)
					{
					case 1:
						this.transportRuleEditor.DefaultActions = new string[]
						{
							"ApplyClassification"
						};
						return;
					case 2:
						this.transportRuleEditor.DefaultActions = new string[]
						{
							"ApplyRightsProtectionTemplate"
						};
						return;
					case 3:
						this.transportRuleEditor.DefaultActions = new string[]
						{
							"AppendHtmlDisclaimer"
						};
						return;
					case 4:
						this.transportRuleEditor.DefaultConditions = new string[]
						{
							"MessageSizeOver"
						};
						return;
					case 5:
						this.transportRuleEditor.DefaultConditions = new string[]
						{
							"MessageContainsDataClassifications"
						};
						this.transportRuleEditor.DefaultActions = new string[]
						{
							"GenerateIncidentReport"
						};
						return;
					case 6:
						this.transportRuleEditor.SimpleModeActions = new string[]
						{
							"PrependSubject",
							"RedirectMessage",
							"RemoveHeader",
							"ApplyRightsProtectionTemplate",
							"SetHeaderName",
							"SetSCL",
							"RejectMessage"
						};
						return;
					case 7:
						this.transportRuleEditor.SimpleModeConditions = new string[]
						{
							"SenderManagementRelationship",
							"ManagerForEvaluatedUser"
						};
						return;
					case 8:
						this.transportRuleEditor.SimpleModeConditions = new string[]
						{
							"AnyOfCcHeader",
							"AnyOfCcHeaderMemberOf",
							"RecipientAddressContains",
							"RecipientAddressMatchesPatterns",
							"AnyOfToCcHeader",
							"AnyOfToCcHeaderMemberOf",
							"AnyOfToHeader",
							"AnyOfToCcHeaderMemberOf",
							"BetweenMemberOf",
							"From",
							"FromAddressContains",
							"FromAddressMatchesPatterns",
							"FromMemberOf",
							"FromScope",
							"HeaderMatches",
							"SenderManagementRelationship",
							"MessageTypeMatches",
							"RecipientAddressContains",
							"RecipientAddressMatchesPatterns",
							"RecipientADAttributeContainsWords",
							"RecipientADAttributeMatchesPatterns",
							"SenderADAttributeContainsWords",
							"SenderADAttributeMatchesPatterns",
							"SentTo",
							"SentToMemberOf",
							"SentToScope"
						};
						this.transportRuleEditor.SimpleModeActions = new string[]
						{
							"DeleteMessage",
							"ModerateMessageByManager",
							"ModerateMessageByUser",
							"Quarantine",
							"RedirectMessage",
							"RejectMessage",
							"RejectMessageEnhancedStatusCode"
						};
						return;
					case 9:
						this.transportRuleEditor.SimpleModeActions = new string[]
						{
							"ModerateMessageByManager",
							"ModerateMessageByUser"
						};
						return;
					case 10:
						this.transportRuleEditor.SimpleModeActions = new string[]
						{
							"AddManagerAsRecipientType",
							"AddToRecipient",
							"BlindCopyTo",
							"CopyTo",
							"ModerateMessageByManager",
							"ModerateMessageByUser",
							"PrependSubject"
						};
						return;
					case 11:
						this.transportRuleEditor.DefaultActions = new string[]
						{
							"SetSCL"
						};
						this.transportRuleEditor.DefaultValues.Add("SetSCL", -1.ToString());
						break;
					default:
						return;
					}
				}
			}
		}

		// Token: 0x0600355C RID: 13660 RVA: 0x000A5E63 File Offset: 0x000A4063
		private Properties FindPropertiesControl()
		{
			return (Properties)base.ContentControl;
		}

		// Token: 0x0600355D RID: 13661 RVA: 0x000A5E70 File Offset: 0x000A4070
		private void CheckIfCopyMode()
		{
			if (base.Request.QueryString["mode"] == "copy")
			{
				Properties properties = this.FindPropertiesControl();
				if (properties != null)
				{
					base.Title = Strings.CopyTransportRule;
					properties.UseSetObject = false;
					properties.GetObjectForNew = true;
					properties.CaptionTextField = null;
					this.transportRuleEditor.IsCopyMode = true;
				}
			}
		}

		// Token: 0x0400258B RID: 9611
		protected TransportRuleEditor transportRuleEditor;
	}
}
