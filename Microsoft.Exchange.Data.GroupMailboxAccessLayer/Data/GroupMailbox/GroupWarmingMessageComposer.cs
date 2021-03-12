using System;
using System.Globalization;
using System.IO;
using System.Web.Security.AntiXss;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x0200003D RID: 61
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GroupWarmingMessageComposer : BaseGroupMessageComposer
	{
		// Token: 0x060001BC RID: 444 RVA: 0x0000CBB0 File Offset: 0x0000ADB0
		public GroupWarmingMessageComposer(ADUser groupMailbox, ADUser executingUser)
		{
			ArgumentValidator.ThrowIfNull("groupMailbox", groupMailbox);
			this.encodedGroupDisplayName = AntiXssEncoder.HtmlEncode(groupMailbox.DisplayName, false);
			this.plainGroupDisplayName = groupMailbox.DisplayName;
			this.groupMailbox = groupMailbox;
			this.participant = new Participant(groupMailbox);
			this.preferredCulture = BaseGroupMessageComposer.GetPreferredCulture(new ADUser[]
			{
				groupMailbox
			});
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000CC18 File Offset: 0x0000AE18
		protected override ADUser[] Recipients
		{
			get
			{
				return new ADUser[]
				{
					this.groupMailbox
				};
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001BE RID: 446 RVA: 0x0000CC36 File Offset: 0x0000AE36
		protected override Participant FromParticipant
		{
			get
			{
				return this.participant;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000CC40 File Offset: 0x0000AE40
		protected override string Subject
		{
			get
			{
				return ClientStrings.GroupMailboxWelcomeMessageSubject(this.plainGroupDisplayName).ToString(this.preferredCulture);
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000CC66 File Offset: 0x0000AE66
		protected override void SetAdditionalMessageProperties(MessageItem message)
		{
			message.IsDraft = false;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000CC6F File Offset: 0x0000AE6F
		protected override void WriteMessageBody(StreamWriter streamWriter)
		{
			ArgumentValidator.ThrowIfNull("streamWriter", streamWriter);
			WelcomeMessageBodyBuilder.WriteWarmingMessageBody(streamWriter, this.encodedGroupDisplayName, this.preferredCulture);
		}

		// Token: 0x040000EF RID: 239
		private readonly string encodedGroupDisplayName;

		// Token: 0x040000F0 RID: 240
		private readonly string plainGroupDisplayName;

		// Token: 0x040000F1 RID: 241
		private readonly ADUser groupMailbox;

		// Token: 0x040000F2 RID: 242
		private readonly Participant participant;

		// Token: 0x040000F3 RID: 243
		private readonly CultureInfo preferredCulture;
	}
}
