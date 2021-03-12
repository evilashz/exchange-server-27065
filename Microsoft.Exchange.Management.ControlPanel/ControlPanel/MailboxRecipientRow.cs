using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000DC RID: 220
	[DataContract]
	[KnownType(typeof(MailboxRecipientRow))]
	public class MailboxRecipientRow : RecipientRow
	{
		// Token: 0x06001D96 RID: 7574 RVA: 0x0005A800 File Offset: 0x00058A00
		public MailboxRecipientRow(ReducedRecipient recipient) : base(recipient)
		{
			this.Recipient = recipient;
			this.IsUserManaged = (recipient.RecipientTypeDetails == Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.UserMailbox && recipient.AuthenticationType == AuthenticationType.Managed);
			this.IsUserFederated = (recipient.RecipientTypeDetails == Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.UserMailbox && recipient.AuthenticationType == AuthenticationType.Federated);
			this.IsRoom = (recipient.RecipientTypeDetails == Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.RoomMailbox);
			this.Type = recipient.RecipientTypeDetails.ToString();
			this.IsResetPasswordAllowed = RbacPrincipal.Current.RbacConfiguration.IsCmdletAllowedInScope("Set-Mailbox", new string[]
			{
				"Password"
			}, recipient, ScopeLocation.RecipientWrite);
			this.IsKeepWindowsLiveIdAllowed = RbacPrincipal.Current.IsInRole("Remove-Mailbox?KeepWindowsLiveId@W:Organization");
			this.MailboxType = MailboxRecipientRow.GenerateMailboxTypeText(recipient.RecipientTypeDetails, recipient.ArchiveGuid, this.IsUserFederated);
			base.SpriteId = Icons.FromEnum(recipient.RecipientTypeDetails, recipient.ArchiveGuid, this.IsUserFederated);
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x0005A918 File Offset: 0x00058B18
		public MailboxRecipientRow(MailEnabledRecipient recipient) : base(recipient)
		{
			this.IsRoom = (recipient.RecipientTypeDetails == Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.RoomMailbox);
		}

		// Token: 0x1700195F RID: 6495
		// (get) Token: 0x06001D98 RID: 7576 RVA: 0x0005A932 File Offset: 0x00058B32
		// (set) Token: 0x06001D99 RID: 7577 RVA: 0x0005A93A File Offset: 0x00058B3A
		public ReducedRecipient Recipient { get; private set; }

		// Token: 0x17001960 RID: 6496
		// (get) Token: 0x06001D9A RID: 7578 RVA: 0x0005A943 File Offset: 0x00058B43
		// (set) Token: 0x06001D9B RID: 7579 RVA: 0x0005A94B File Offset: 0x00058B4B
		[DataMember]
		public string Type { get; private set; }

		// Token: 0x17001961 RID: 6497
		// (get) Token: 0x06001D9C RID: 7580 RVA: 0x0005A954 File Offset: 0x00058B54
		// (set) Token: 0x06001D9D RID: 7581 RVA: 0x0005A95C File Offset: 0x00058B5C
		[DataMember]
		public string MailboxType { get; private set; }

		// Token: 0x17001962 RID: 6498
		// (get) Token: 0x06001D9E RID: 7582 RVA: 0x0005A965 File Offset: 0x00058B65
		// (set) Token: 0x06001D9F RID: 7583 RVA: 0x0005A96D File Offset: 0x00058B6D
		[DataMember]
		public bool IsUserManaged { get; private set; }

		// Token: 0x17001963 RID: 6499
		// (get) Token: 0x06001DA0 RID: 7584 RVA: 0x0005A976 File Offset: 0x00058B76
		// (set) Token: 0x06001DA1 RID: 7585 RVA: 0x0005A97E File Offset: 0x00058B7E
		[DataMember]
		public bool IsUserFederated { get; private set; }

		// Token: 0x17001964 RID: 6500
		// (get) Token: 0x06001DA2 RID: 7586 RVA: 0x0005A987 File Offset: 0x00058B87
		// (set) Token: 0x06001DA3 RID: 7587 RVA: 0x0005A98F File Offset: 0x00058B8F
		[DataMember]
		public bool IsRoom { get; private set; }

		// Token: 0x17001965 RID: 6501
		// (get) Token: 0x06001DA4 RID: 7588 RVA: 0x0005A998 File Offset: 0x00058B98
		// (set) Token: 0x06001DA5 RID: 7589 RVA: 0x0005A9A0 File Offset: 0x00058BA0
		[DataMember]
		public bool IsResetPasswordAllowed { get; private set; }

		// Token: 0x17001966 RID: 6502
		// (get) Token: 0x06001DA6 RID: 7590 RVA: 0x0005A9A9 File Offset: 0x00058BA9
		// (set) Token: 0x06001DA7 RID: 7591 RVA: 0x0005A9B1 File Offset: 0x00058BB1
		[DataMember]
		public bool IsKeepWindowsLiveIdAllowed { get; private set; }

		// Token: 0x06001DA8 RID: 7592 RVA: 0x0005A9BC File Offset: 0x00058BBC
		public static string GenerateMailboxTypeText(RecipientTypeDetails recipientTypeDetails, Guid archiveGuid, bool isUserFederated)
		{
			string text = string.Empty;
			if (recipientTypeDetails <= Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.LegacyMailbox)
			{
				if (recipientTypeDetails <= Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.SharedMailbox)
				{
					if (recipientTypeDetails < Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.UserMailbox)
					{
						goto IL_BA;
					}
					switch ((int)(recipientTypeDetails - Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.UserMailbox))
					{
					case 0:
						text = (isUserFederated ? Strings.FederatedUserMailboxText : Strings.UserMailboxText);
						goto IL_C6;
					case 1:
						text = Strings.LinkedMailboxText;
						goto IL_C6;
					case 2:
						goto IL_BA;
					case 3:
						text = Strings.SharedMailboxText;
						goto IL_C6;
					}
				}
				if (recipientTypeDetails == Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.LegacyMailbox)
				{
					text = Strings.LegacyMailboxText;
					goto IL_C6;
				}
			}
			else
			{
				if (recipientTypeDetails == Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.RoomMailbox)
				{
					text = Strings.RoomMailboxText;
					goto IL_C6;
				}
				if (recipientTypeDetails == Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.EquipmentMailbox)
				{
					text = Strings.EquipmentMailboxText;
					goto IL_C6;
				}
				if (recipientTypeDetails == Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.TeamMailbox)
				{
					text = Strings.TeamMailboxText;
					goto IL_C6;
				}
			}
			IL_BA:
			text = recipientTypeDetails.ToString();
			IL_C6:
			return archiveGuid.Equals(Guid.Empty) ? text : string.Format(Strings.ArchiveText, text);
		}
	}
}
