using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Management.ControlPanel.DataContracts;
using Microsoft.Exchange.Management.DDIService;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200042C RID: 1068
	public class RuleDetails : WebControl, INamingContainer
	{
		// Token: 0x0600356F RID: 13679 RVA: 0x000A6120 File Offset: 0x000A4320
		public RuleDetails() : base(HtmlTextWriterTag.Div)
		{
			string text = this.Context.Request.QueryString["id"];
			if (!string.IsNullOrEmpty(text))
			{
				this.ObjectIdentity = Identity.ParseIdentity(text);
			}
			this.ApplyToAllRuleDescription = Strings.RuleDescriptionApplyToAll;
		}

		// Token: 0x170020F3 RID: 8435
		// (get) Token: 0x06003570 RID: 13680 RVA: 0x000A6174 File Offset: 0x000A4374
		// (set) Token: 0x06003571 RID: 13681 RVA: 0x000A617C File Offset: 0x000A437C
		public Identity ObjectIdentity { get; private set; }

		// Token: 0x170020F4 RID: 8436
		// (get) Token: 0x06003572 RID: 13682 RVA: 0x000A6185 File Offset: 0x000A4385
		// (set) Token: 0x06003573 RID: 13683 RVA: 0x000A618D File Offset: 0x000A438D
		public WebServiceReference ServiceUrl { get; set; }

		// Token: 0x170020F5 RID: 8437
		// (get) Token: 0x06003574 RID: 13684 RVA: 0x000A6196 File Offset: 0x000A4396
		// (set) Token: 0x06003575 RID: 13685 RVA: 0x000A619E File Offset: 0x000A439E
		public string ConditionHeaderString { get; set; }

		// Token: 0x170020F6 RID: 8438
		// (get) Token: 0x06003576 RID: 13686 RVA: 0x000A61A7 File Offset: 0x000A43A7
		// (set) Token: 0x06003577 RID: 13687 RVA: 0x000A61AF File Offset: 0x000A43AF
		public string UnsupportedRuleDescription { get; set; }

		// Token: 0x170020F7 RID: 8439
		// (get) Token: 0x06003578 RID: 13688 RVA: 0x000A61B8 File Offset: 0x000A43B8
		// (set) Token: 0x06003579 RID: 13689 RVA: 0x000A61C0 File Offset: 0x000A43C0
		public string ApplyToAllRuleDescription { get; set; }

		// Token: 0x0600357A RID: 13690 RVA: 0x000A61DC File Offset: 0x000A43DC
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			RuleRow ruleObject = this.GetRuleObject();
			if (ruleObject == null)
			{
				return;
			}
			if (!ruleObject.Supported && !this.UnsupportedRuleDescription.IsNullOrBlank())
			{
				Label label = new Label();
				label.Text = this.UnsupportedRuleDescription;
				label.CssClass = "RuleDetailsPanel_SectionHeader";
				label.ID = "lblTitle";
				this.Controls.Add(label);
				return;
			}
			Label label2 = new Label();
			label2.Text = HttpUtility.HtmlEncode(ruleObject.Name);
			label2.CssClass = "RuleDetailsPanel_RuleName";
			label2.ID = "lblTitle";
			this.Controls.Add(label2);
			Panel child;
			if (ruleObject.ConditionDescriptions.Length > 0)
			{
				child = this.CreateDescriptionsPane("divCondition", this.ConditionHeaderString, ruleObject.ConditionDescriptions, Strings.RuleDescriptionAndDelimiter);
			}
			else
			{
				string[] descriptionArray = new string[]
				{
					this.ApplyToAllRuleDescription
				};
				child = this.CreateDescriptionsPane("divCondition", this.ConditionHeaderString, descriptionArray, Strings.RuleDescriptionAndDelimiter);
			}
			this.Controls.Add(child);
			if (ruleObject.ActionDescriptions.Length > 0)
			{
				Panel child2 = this.CreateDescriptionsPane("divAction", Strings.RuleActionSectionHeader, ruleObject.ActionDescriptions, Strings.RuleDescriptionAndDelimiter);
				this.Controls.Add(child2);
				if (ruleObject is TransportRule)
				{
					TransportRule transportRule = (TransportRule)ruleObject;
					if (null != transportRule.GenerateIncidentReport && RbacPrincipal.Current.IsInRole("OWA") && MailboxPermissions.CanAccessMailboxOf(transportRule.GenerateIncidentReport.RawIdentity))
					{
						this.CreateIncidentManagementLink(transportRule);
					}
				}
			}
			if (ruleObject.ExceptionDescriptions.Length > 0)
			{
				Panel child3 = this.CreateDescriptionsPane("divException", Strings.RuleExceptionSectionHeader, ruleObject.ExceptionDescriptions, Strings.RuleDescriptionOrDelimiter);
				this.Controls.Add(child3);
			}
			if (!string.IsNullOrEmpty(ruleObject.DlpPolicy))
			{
				Label label3 = new Label();
				label3.Text = HttpUtility.HtmlEncode(Strings.DLPPolicyMailFlowNameColumn);
				label3.CssClass = "RuleDetailsPanel_SectionHeader";
				label3.ID = "lblDLPPolicy";
				this.Controls.Add(label3);
				HtmlGenericControl htmlGenericControl = new HtmlGenericControl(HtmlTextWriterTag.Div.ToString());
				htmlGenericControl.Attributes.Add("class", "RuleDetailsPanel_Description");
				htmlGenericControl.ID = "dlppolicy_description";
				htmlGenericControl.InnerText = ruleObject.DlpPolicy;
				this.Controls.Add(htmlGenericControl);
			}
			List<string> list = new List<string>();
			if (ruleObject is TransportRule)
			{
				TransportRule transportRule2 = (TransportRule)ruleObject;
				if (transportRule2.Comments != null)
				{
					Label label4 = new Label();
					label4.Text = HttpUtility.HtmlEncode(Strings.RuleCommentsLabel);
					label4.CssClass = "RuleDetailsPanel_SectionHeader";
					label4.ID = "lblRuleComment";
					this.Controls.Add(label4);
					HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl(HtmlTextWriterTag.Div.ToString());
					htmlGenericControl2.Attributes.Add("class", "RuleDetailsPanel_Description");
					htmlGenericControl2.ID = "lblRuleComment_description";
					htmlGenericControl2.InnerText = transportRule2.Comments;
					this.Controls.Add(htmlGenericControl2);
				}
				if (!string.IsNullOrEmpty(transportRule2.Mode))
				{
					Label label5 = new Label();
					label5.Text = HttpUtility.HtmlEncode(Strings.RuleMode);
					label5.CssClass = "RuleDetailsPanel_SectionHeader";
					label5.ID = "lblRuleMode";
					this.Controls.Add(label5);
					HtmlGenericControl htmlGenericControl3 = new HtmlGenericControl(HtmlTextWriterTag.Div.ToString());
					htmlGenericControl3.Attributes.Add("class", "RuleDetailsPanel_Description");
					htmlGenericControl3.ID = "dlppolicy_mode";
					htmlGenericControl3.InnerText = LocalizedDescriptionAttribute.FromEnum(typeof(RuleMode), (RuleMode)Enum.Parse(typeof(RuleMode), transportRule2.Mode, true));
					this.Controls.Add(htmlGenericControl3);
				}
				string format;
				if (EacRbacPrincipal.Instance.DateFormat != null && EacRbacPrincipal.Instance.TimeFormat != null)
				{
					format = EacRbacPrincipal.Instance.DateFormat + " " + EacRbacPrincipal.Instance.TimeFormat;
				}
				else
				{
					format = "yyyy/MM/dd HH:mm:ss";
				}
				if (transportRule2.ActivationDate != null)
				{
					list.Add(ruleObject.ActivationDateDescription + " " + transportRule2.ActivationDate.Value.ToString(format));
				}
				if (transportRule2.ExpiryDate != null)
				{
					list.Add(ruleObject.ExpiryDateDescription + " " + transportRule2.ExpiryDate.Value.ToString(format));
				}
				if (transportRule2.SenderAddressLocation != null)
				{
					list.Add(string.Format(Strings.RuleSenderAddressMatches, LocalizedDescriptionAttribute.FromEnum(typeof(SenderAddressLocation), Enum.Parse(typeof(SenderAddressLocation), transportRule2.SenderAddressLocation, true))));
				}
				if (transportRule2.RuleErrorAction != null)
				{
					RuleErrorAction ruleErrorAction = (RuleErrorAction)Enum.Parse(typeof(RuleErrorAction), transportRule2.RuleErrorAction, true);
					if (ruleErrorAction == RuleErrorAction.Defer)
					{
						list.Add(string.Format(Strings.RuleBusinessContinuity, LocalizedDescriptionAttribute.FromEnum(typeof(RuleErrorAction), ruleErrorAction)));
					}
				}
			}
			if (list.Count > 0)
			{
				Label label6 = new Label();
				label6.Text = HttpUtility.HtmlEncode(Strings.RuleProperties);
				label6.CssClass = "RuleDetailsPanel_SectionHeader";
				label6.ID = "lblAdditionalProps";
				this.Controls.Add(label6);
				HtmlGenericControl htmlGenericControl4 = new HtmlGenericControl(HtmlTextWriterTag.Div.ToString());
				htmlGenericControl4.Attributes.Add("class", "RuleDetailsPanel_Description");
				htmlGenericControl4.ID = "properties_description";
				htmlGenericControl4.InnerHtml = string.Join(string.Empty, from s in list
				select HttpUtility.HtmlEncode(s) + "<br />");
				this.Controls.Add(htmlGenericControl4);
			}
			if (null != ruleObject.RuleVersion)
			{
				Panel panel = new Panel();
				panel.Attributes.Add("class", "RuleDetailsPanel_Description");
				panel.Controls.Add(new Label
				{
					Text = string.Format(Strings.RuleVersion, ruleObject.RuleVersion.ToString())
				});
				this.Controls.Add(panel);
			}
		}

		// Token: 0x0600357B RID: 13691 RVA: 0x000A687C File Offset: 0x000A4A7C
		private void CreateIncidentManagementLink(TransportRule tr)
		{
			if (null != tr.GenerateIncidentReport)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("/?cmd=contents");
				if (RbacPrincipal.Current.IsInRole("LiveID"))
				{
					stringBuilder.Append("&exsvurl=1");
				}
				stringBuilder.Append("&part=1");
				stringBuilder.Append("&folderlist=1");
				this.Controls.Add(new HyperLink
				{
					Text = Strings.IncidentMailboxLinkTitle,
					Target = "_new",
					NavigateUrl = EcpUrl.OwaVDir + HttpUtility.UrlEncode(tr.GenerateIncidentReport.RawIdentity) + stringBuilder.ToString()
				});
				this.Controls.Add(new LiteralControl("<br /><br />"));
			}
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x000A6949 File Offset: 0x000A4B49
		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(new InlineHtmlTextWriter(writer.InnerWriter));
		}

		// Token: 0x0600357D RID: 13693 RVA: 0x000A695C File Offset: 0x000A4B5C
		private RuleRow GetRuleObject()
		{
			RuleRow result = null;
			if (this.ServiceUrl != null)
			{
				if (null == this.ObjectIdentity)
				{
					throw new BadQueryParameterException("id");
				}
				IEnumerable enumerable = (IEnumerable)this.ServiceUrl.GetObject(this.ObjectIdentity);
				IEnumerator enumerator = enumerable.GetEnumerator();
				if (enumerator.MoveNext())
				{
					result = (RuleRow)enumerator.Current;
				}
			}
			return result;
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x000A69C0 File Offset: 0x000A4BC0
		private Panel CreateDescriptionsPane(string containingDivID, string sectionHeaderText, string[] descriptionArray, string delimiter)
		{
			Panel panel = new Panel();
			panel.ID = containingDivID;
			Label label = new Label();
			label.CssClass = "RuleDetailsPanel_SectionHeader";
			label.Text = sectionHeaderText;
			label.ID = containingDivID + "_header";
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl(HtmlTextWriterTag.Div.ToString());
			htmlGenericControl.Attributes.Add("class", "RuleDetailsPanel_Description");
			htmlGenericControl.ID = containingDivID + "_description";
			StringBuilder stringBuilder = new StringBuilder(descriptionArray.Length * 50);
			bool flag = true;
			foreach (string s in descriptionArray)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append("<br />");
					stringBuilder.Append(delimiter);
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(HttpUtility.HtmlEncode(s));
			}
			htmlGenericControl.InnerHtml = stringBuilder.ToString();
			panel.Controls.Add(label);
			panel.Controls.Add(htmlGenericControl);
			return panel;
		}
	}
}
