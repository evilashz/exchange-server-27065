using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.MailboxTransportSubmission.MapiProbe
{
	// Token: 0x02000053 RID: 83
	[Cmdlet("Send", "MapiSubmitSystemProbe", SupportsShouldProcess = true)]
	[OutputType(new Type[]
	{
		typeof(Guid)
	})]
	public sealed class MapiSubmitSystemProbe : Task
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000BDB8 File Offset: 0x00009FB8
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x0000BDC0 File Offset: 0x00009FC0
		[Parameter(Mandatory = true)]
		public string SenderEmailAddress { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000BDC9 File Offset: 0x00009FC9
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x0000BDD1 File Offset: 0x00009FD1
		[Parameter(Mandatory = false)]
		public string RecipientEmailAddress { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000BDDA File Offset: 0x00009FDA
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x0000BDE2 File Offset: 0x00009FE2
		[Parameter(Mandatory = false)]
		public string InternetMessageIdOfTheMessageToDeleteFromOutbox { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000BDEB File Offset: 0x00009FEB
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSendMapiSubmitSystemProbe;
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000BDF4 File Offset: 0x00009FF4
		protected override void InternalProcessRecord()
		{
			if (string.IsNullOrEmpty(this.InternetMessageIdOfTheMessageToDeleteFromOutbox))
			{
				string text = null;
				Guid guid = this.SendMapiSubmitSystemProbe(this.SenderEmailAddress, this.RecipientEmailAddress, out text);
				base.WriteObject(string.Format("{0}:{1}\n{2}:{3}", new object[]
				{
					Strings.MapiSubmitSystemProbeId,
					guid,
					Strings.MapiSubmitSystemProbeInternetMessageId,
					text
				}));
				return;
			}
			switch (this.DeleteMessageFromOutbox(this.SenderEmailAddress, this.InternetMessageIdOfTheMessageToDeleteFromOutbox))
			{
			case DeletionResult.Fail:
				base.WriteError(new ApplicationException(Strings.MapiSubmitSystemProbeFail.ToString()), ErrorCategory.InvalidResult, null);
				return;
			case DeletionResult.NoMatchingMessage:
				base.WriteWarning(Strings.MapiSubmitSystemProbeNoMessageFound.ToString());
				return;
			case DeletionResult.Success:
				base.WriteVerbose(Strings.MapiSubmitSystemProbeSuccesfullyDeleted);
				return;
			default:
				return;
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000BEDB File Offset: 0x0000A0DB
		private Guid SendMapiSubmitSystemProbe(string senderEmail, string recipientEmail, out string internetMessageId)
		{
			return MapiSubmitSystemProbeHandler.GetInstance().SendMapiSubmitSystemProbe(senderEmail, recipientEmail, out internetMessageId);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000BEEA File Offset: 0x0000A0EA
		private DeletionResult DeleteMessageFromOutbox(string senderEmail, string internetMessageId)
		{
			return MapiSubmitSystemProbeHandler.GetInstance().DeleteMessageFromOutbox(senderEmail, internetMessageId);
		}
	}
}
