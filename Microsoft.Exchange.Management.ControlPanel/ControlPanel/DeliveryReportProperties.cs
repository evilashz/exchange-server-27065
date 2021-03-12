using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200029A RID: 666
	public class DeliveryReportProperties : Properties
	{
		// Token: 0x06002B45 RID: 11077 RVA: 0x00087240 File Offset: 0x00085440
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			string text = this.Context.Request.QueryString["recip"];
			if (base.ObjectIdentity == null)
			{
				string text2 = this.Context.Request.QueryString["MsgId"];
				string text3 = this.Context.Request.QueryString["Mbx"];
				if (!string.IsNullOrEmpty(text2) && !string.IsNullOrEmpty(text3))
				{
					PowerShellResults<MessageTrackingSearchResultRow> messageTrackingReportSearchResults = this.GetMessageTrackingReportSearchResults(text2, text3);
					if (messageTrackingReportSearchResults.Succeeded && messageTrackingReportSearchResults.Output.Length > 0)
					{
						base.ObjectIdentity = new Identity(new RecipientMessageTrackingReportId(messageTrackingReportSearchResults.Output[0].Identity.RawIdentity, text).RawIdentity, text);
						return;
					}
					base.Results = messageTrackingReportSearchResults;
					base.UseSetObject = false;
				}
			}
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x0008731C File Offset: 0x0008551C
		private PowerShellResults<MessageTrackingSearchResultRow> GetMessageTrackingReportSearchResults(string msgId, string mailboxId)
		{
			MessageTrackingSearch messageTrackingSearch = new MessageTrackingSearch();
			return messageTrackingSearch.GetList(new MessageTrackingSearchFilter
			{
				MessageEntryId = msgId,
				Identity = Identity.FromIdParameter(mailboxId)
			}, null);
		}
	}
}
