using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000085 RID: 133
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class VotingResponse : ReplyForwardCommon
	{
		// Token: 0x06000998 RID: 2456 RVA: 0x0004557C File Offset: 0x0004377C
		internal VotingResponse(MessageItem originalItem, MessageItem newItem, ReplyForwardConfiguration configuration, string votingResponse) : base(originalItem, newItem, configuration, true)
		{
			this.votingResponse = votingResponse;
			this.newItem.SafeSetProperty(InternalSchema.IsVotingResponse, 1);
			int lastAction = originalItem.VotingInfo.GetOptionsList().IndexOf(votingResponse) + 1;
			this.newItem.SafeSetProperty(InternalSchema.ReplyForwardStatus, ReplyForwardUtils.EncodeReplyForwardStatus((LastAction)lastAction, IconIndex.MailReplied, originalItem.Id));
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x000455E8 File Offset: 0x000437E8
		protected override void BuildSubject()
		{
			this.newItem[InternalSchema.SubjectPrefix] = this.votingResponse + ": ";
			this.newItem[InternalSchema.NormalizedSubject] = this.originalItem.GetValueOrDefault<string>(InternalSchema.NormalizedSubjectInternal, string.Empty);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0004563C File Offset: 0x0004383C
		protected override void UpdateNewItemProperties()
		{
			base.UpdateNewItemProperties();
			ReplyForwardCommon.BuildReplyRecipientsFromMessage(this.newItem as MessageItem, this.originalItem as MessageItem, false, true, true);
			this.newItem.SafeSetProperty(InternalSchema.Importance, Importance.Normal);
			this.newItem.SafeSetProperty(InternalSchema.IsVotingResponse, 1);
			this.newItem.SafeSetProperty(InternalSchema.ReportTag, this.originalItem.PropertyBag.GetValueOrDefault<byte[]>(InternalSchema.ReportTag));
			this.newItem.SafeSetProperty(InternalSchema.VotingResponse, this.votingResponse);
			if (this.originalItem.ClassName.Equals("IPM.Note.Microsoft.Approval.Request", StringComparison.OrdinalIgnoreCase))
			{
				MessageItem messageItem = this.originalItem as MessageItem;
				string[] array = (string[])messageItem.VotingInfo.GetOptionsList();
				int num = Array.IndexOf<string>(array, this.votingResponse);
				if (num == 0)
				{
					this.newItem.SafeSetProperty(InternalSchema.ItemClass, "IPM.Note.Microsoft.Approval.Reply.Approve");
					return;
				}
				if (num == 1)
				{
					this.newItem.SafeSetProperty(InternalSchema.ItemClass, "IPM.Note.Microsoft.Approval.Reply.Reject");
				}
			}
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00045748 File Offset: 0x00043948
		protected override void BuildBody(BodyConversionCallbacks callbacks)
		{
			BodyWriteConfiguration bodyWriteConfiguration = new BodyWriteConfiguration(base.Format);
			bodyWriteConfiguration.SetTargetFormat(this.parameters.TargetFormat, this.newItem.Body.Charset);
			if (!string.IsNullOrEmpty(this.parameters.BodyPrefix))
			{
				bodyWriteConfiguration.AddInjectedText(this.parameters.BodyPrefix, null, this.parameters.BodyPrefixFormat);
			}
			using (this.newItem.Body.OpenTextWriter(bodyWriteConfiguration))
			{
			}
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x000457E0 File Offset: 0x000439E0
		protected override void BuildAttachments(BodyConversionCallbacks callbacks, InboundConversionOptions optionsForSmime)
		{
		}

		// Token: 0x0400027D RID: 637
		private string votingResponse;
	}
}
