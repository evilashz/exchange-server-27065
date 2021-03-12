using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000D6 RID: 214
	[DataContract]
	[KnownType(typeof(RecipientRow))]
	public class RecipientRow : BaseRow
	{
		// Token: 0x06001D73 RID: 7539 RVA: 0x0005A4B0 File Offset: 0x000586B0
		public RecipientRow(ReducedRecipient recipient) : base(recipient.ToIdentity(), recipient)
		{
			this.PrimarySmtpAddress = recipient.PrimarySmtpAddress.ToString();
			this.SpriteId = Icons.FromEnum(recipient.RecipientTypeDetails);
			this.RecipientTypeDetails = recipient.RecipientTypeDetails.ToString();
			this.LocRecipientTypeDetails = LocalizedDescriptionAttribute.FromEnum(typeof(RecipientTypeDetails), recipient.RecipientTypeDetails);
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x0005A52C File Offset: 0x0005872C
		public RecipientRow(MailEnabledRecipient recipient) : base(recipient.ToIdentity(), recipient)
		{
			this.PrimarySmtpAddress = recipient.PrimarySmtpAddress.ToString();
			this.SpriteId = Icons.FromEnum(recipient.RecipientTypeDetails);
			this.RecipientTypeDetails = recipient.RecipientTypeDetails.ToString();
			this.LocRecipientTypeDetails = LocalizedDescriptionAttribute.FromEnum(typeof(RecipientTypeDetails), recipient.RecipientTypeDetails);
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x0005A5A7 File Offset: 0x000587A7
		public RecipientRow(WindowsGroup group) : base(group.Id.ToIdentity(group.DisplayName), group)
		{
			this.PrimarySmtpAddress = group.WindowsEmailAddress.ToStringWithNull();
		}

		// Token: 0x17001953 RID: 6483
		// (get) Token: 0x06001D76 RID: 7542 RVA: 0x0005A5D7 File Offset: 0x000587D7
		// (set) Token: 0x06001D77 RID: 7543 RVA: 0x0005A5DF File Offset: 0x000587DF
		[DataMember]
		public string RecipientTypeDetails { get; protected set; }

		// Token: 0x17001954 RID: 6484
		// (get) Token: 0x06001D78 RID: 7544 RVA: 0x0005A5E8 File Offset: 0x000587E8
		// (set) Token: 0x06001D79 RID: 7545 RVA: 0x0005A5F0 File Offset: 0x000587F0
		[DataMember]
		public string LocRecipientTypeDetails { get; protected set; }

		// Token: 0x17001955 RID: 6485
		// (get) Token: 0x06001D7A RID: 7546 RVA: 0x0005A5F9 File Offset: 0x000587F9
		// (set) Token: 0x06001D7B RID: 7547 RVA: 0x0005A606 File Offset: 0x00058806
		[DataMember]
		public string DisplayName
		{
			get
			{
				return base.Identity.DisplayName;
			}
			private set
			{
			}
		}

		// Token: 0x17001956 RID: 6486
		// (get) Token: 0x06001D7C RID: 7548 RVA: 0x0005A608 File Offset: 0x00058808
		// (set) Token: 0x06001D7D RID: 7549 RVA: 0x0005A610 File Offset: 0x00058810
		[DataMember]
		public string PrimarySmtpAddress { get; protected set; }

		// Token: 0x17001957 RID: 6487
		// (get) Token: 0x06001D7E RID: 7550 RVA: 0x0005A619 File Offset: 0x00058819
		// (set) Token: 0x06001D7F RID: 7551 RVA: 0x0005A621 File Offset: 0x00058821
		[DataMember]
		public string SpriteId { get; protected set; }
	}
}
