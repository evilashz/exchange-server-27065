using System;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.Aggregation;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002F3 RID: 755
	public class VerifySendAs : EcpPage
	{
		// Token: 0x06002D3A RID: 11578 RVA: 0x0008A97C File Offset: 0x00088B7C
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.EnsureChildControls();
			IconButton iconButton = (IconButton)this.myMailLoginView.FindControl("btnMyMail");
			if (iconButton != null)
			{
				iconButton.Attributes["onclick"] = this.RedirectToOWAScript(EcpUrl.OwaVDir);
			}
		}

		// Token: 0x06002D3B RID: 11579 RVA: 0x0008A9CC File Offset: 0x00088BCC
		protected override void OnLoad(EventArgs e)
		{
			string subscriptionTypeParam = this.Context.Request.QueryString["st"];
			string subscriptionGuidParam = this.Context.Request.QueryString["su"];
			string text = this.Context.Request.QueryString["ss"];
			AggregationSubscriptionType aggregationSubscriptionType;
			Guid subscriptionId;
			string queryParam;
			if (!this.ValidateUrlParameters(subscriptionTypeParam, subscriptionGuidParam, text, out aggregationSubscriptionType, out subscriptionId, out queryParam))
			{
				throw new BadQueryParameterException(queryParam);
			}
			ADObjectId executingUserId = RbacPrincipal.Current.ExecutingUserId;
			AggregationSubscriptionIdentity subId = new AggregationSubscriptionIdentity(executingUserId, subscriptionId);
			AggregationSubscriptionIdParameter aggregationSubscriptionIdParameter = new AggregationSubscriptionIdParameter(subId);
			string text2 = aggregationSubscriptionIdParameter.ToString();
			if (text2 == null)
			{
				ErrorHandlingUtil.TransferToErrorPage("unexpected");
				return;
			}
			if (aggregationSubscriptionType == AggregationSubscriptionType.Pop)
			{
				PopSubscriptions popSubscriptions = new PopSubscriptions();
				SetPopSubscription setPopSubscription = new SetPopSubscription();
				setPopSubscription.ValidateSecret = text;
				setPopSubscription.AllowExceuteThruHttpGetRequest = true;
				Identity identity = new Identity(text2, text2);
				PowerShellResults<PopSubscription> results = popSubscriptions.SetObject(identity, setPopSubscription);
				this.DisplayResults<PopSubscription>(results);
				return;
			}
			ImapSubscriptions imapSubscriptions = new ImapSubscriptions();
			SetImapSubscription setImapSubscription = new SetImapSubscription();
			setImapSubscription.ValidateSecret = text;
			setImapSubscription.AllowExceuteThruHttpGetRequest = true;
			Identity identity2 = new Identity(text2, text2);
			PowerShellResults<ImapSubscription> results2 = imapSubscriptions.SetObject(identity2, setImapSubscription);
			this.DisplayResults<ImapSubscription>(results2);
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x0008AAFC File Offset: 0x00088CFC
		private string RedirectToOWAScript(string owaUrl)
		{
			return "window.location.href = " + owaUrl + ";";
		}

		// Token: 0x06002D3D RID: 11581 RVA: 0x0008AB10 File Offset: 0x00088D10
		private bool ValidateUrlParameters(string subscriptionTypeParam, string subscriptionGuidParam, string sharedSecretParam, out AggregationSubscriptionType subscriptionType, out Guid subscriptionGuid, out string invalidQueryParam)
		{
			subscriptionGuid = Guid.Empty;
			invalidQueryParam = string.Empty;
			if (subscriptionTypeParam == 2.ToString())
			{
				subscriptionType = AggregationSubscriptionType.Pop;
			}
			else
			{
				if (!(subscriptionTypeParam == 16.ToString()))
				{
					subscriptionType = AggregationSubscriptionType.Unknown;
					invalidQueryParam = "st";
					return false;
				}
				subscriptionType = AggregationSubscriptionType.IMAP;
			}
			if (string.IsNullOrEmpty(subscriptionGuidParam))
			{
				invalidQueryParam = "su";
				return false;
			}
			if (!GuidHelper.TryParseGuid(subscriptionGuidParam, out subscriptionGuid))
			{
				invalidQueryParam = "su";
				return false;
			}
			if (string.IsNullOrEmpty(sharedSecretParam))
			{
				invalidQueryParam = "ss";
				return false;
			}
			return true;
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x0008ABA8 File Offset: 0x00088DA8
		private void DisplayResults<T>(PowerShellResults<T> results) where T : PimSubscription
		{
			if (results.Failed)
			{
				if (results.ErrorRecords[0].Exception is ManagementObjectNotFoundException)
				{
					ErrorHandlingUtil.TransferToErrorPage("liveidmismatch");
					return;
				}
				if (results.ErrorRecords[0].Exception is ValidateSecretFailureException)
				{
					ErrorHandlingUtil.TransferToErrorPage("verificationfailed");
					return;
				}
				ErrorHandlingUtil.TransferToErrorPage("verificationprocessingerror");
				return;
			}
			else
			{
				if (results.Output.Length == 0)
				{
					ErrorHandlingUtil.TransferToErrorPage("verificationprocessingerror");
					return;
				}
				this.msgText.Text = OwaOptionStrings.VerificationSuccessText(results.Output[0].EmailAddress);
				base.Title = OwaOptionStrings.VerificationSuccessPageTitle;
				return;
			}
		}

		// Token: 0x04002248 RID: 8776
		private const string MyMailButtonId = "btnMyMail";

		// Token: 0x04002249 RID: 8777
		protected Literal msgText;

		// Token: 0x0400224A RID: 8778
		protected Label msgTitle;

		// Token: 0x0400224B RID: 8779
		protected LoginView myMailLoginView;
	}
}
