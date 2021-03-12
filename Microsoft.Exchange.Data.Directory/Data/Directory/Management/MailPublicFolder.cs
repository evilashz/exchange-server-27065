using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Provisioning;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000735 RID: 1845
	[ProvisioningObjectTag("MailPublicFolder")]
	[Serializable]
	public class MailPublicFolder : MailEnabledRecipient
	{
		// Token: 0x17001E25 RID: 7717
		// (get) Token: 0x0600587F RID: 22655 RVA: 0x0013AB0F File Offset: 0x00138D0F
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return MailPublicFolder.schema;
			}
		}

		// Token: 0x17001E26 RID: 7718
		// (get) Token: 0x06005880 RID: 22656 RVA: 0x0013AB16 File Offset: 0x00138D16
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17001E27 RID: 7719
		// (get) Token: 0x06005881 RID: 22657 RVA: 0x0013AB1D File Offset: 0x00138D1D
		public MultiValuedProperty<ADObjectId> Contacts
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[MailPublicFolderSchema.Contacts];
			}
		}

		// Token: 0x17001E28 RID: 7720
		// (get) Token: 0x06005882 RID: 22658 RVA: 0x0013AB2F File Offset: 0x00138D2F
		public ADObjectId ContentMailbox
		{
			get
			{
				return (ADObjectId)this[MailPublicFolderSchema.ContentMailbox];
			}
		}

		// Token: 0x17001E29 RID: 7721
		// (get) Token: 0x06005883 RID: 22659 RVA: 0x0013AB41 File Offset: 0x00138D41
		// (set) Token: 0x06005884 RID: 22660 RVA: 0x0013AB53 File Offset: 0x00138D53
		[Parameter(Mandatory = false)]
		public bool DeliverToMailboxAndForward
		{
			get
			{
				return (bool)this[MailPublicFolderSchema.DeliverToMailboxAndForward];
			}
			set
			{
				this[MailPublicFolderSchema.DeliverToMailboxAndForward] = value;
			}
		}

		// Token: 0x17001E2A RID: 7722
		// (get) Token: 0x06005885 RID: 22661 RVA: 0x0013AB66 File Offset: 0x00138D66
		// (set) Token: 0x06005886 RID: 22662 RVA: 0x0013AB78 File Offset: 0x00138D78
		public ProxyAddress ExternalEmailAddress
		{
			get
			{
				return (ProxyAddress)this[MailPublicFolderSchema.ExternalEmailAddress];
			}
			set
			{
				this[MailPublicFolderSchema.ExternalEmailAddress] = value;
			}
		}

		// Token: 0x17001E2B RID: 7723
		// (get) Token: 0x06005887 RID: 22663 RVA: 0x0013AB86 File Offset: 0x00138D86
		public string EntryId
		{
			get
			{
				return (string)this[MailPublicFolderSchema.EntryId];
			}
		}

		// Token: 0x17001E2C RID: 7724
		// (get) Token: 0x06005888 RID: 22664 RVA: 0x0013AB98 File Offset: 0x00138D98
		// (set) Token: 0x06005889 RID: 22665 RVA: 0x0013ABAA File Offset: 0x00138DAA
		public ADObjectId ForwardingAddress
		{
			get
			{
				return (ADObjectId)this[MailPublicFolderSchema.ForwardingAddress];
			}
			set
			{
				this[MailPublicFolderSchema.ForwardingAddress] = value;
			}
		}

		// Token: 0x17001E2D RID: 7725
		// (get) Token: 0x0600588A RID: 22666 RVA: 0x0013ABB8 File Offset: 0x00138DB8
		// (set) Token: 0x0600588B RID: 22667 RVA: 0x0013ABCA File Offset: 0x00138DCA
		[Parameter(Mandatory = false)]
		public string PhoneticDisplayName
		{
			get
			{
				return (string)this[MailPublicFolderSchema.PhoneticDisplayName];
			}
			set
			{
				this[MailPublicFolderSchema.PhoneticDisplayName] = value;
			}
		}

		// Token: 0x0600588C RID: 22668 RVA: 0x0013ABD8 File Offset: 0x00138DD8
		public MailPublicFolder()
		{
		}

		// Token: 0x0600588D RID: 22669 RVA: 0x0013ABE0 File Offset: 0x00138DE0
		public MailPublicFolder(ADPublicFolder dataObject) : base(dataObject)
		{
		}

		// Token: 0x0600588E RID: 22670 RVA: 0x0013ABE9 File Offset: 0x00138DE9
		internal static MailPublicFolder FromDataObject(ADPublicFolder dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new MailPublicFolder(dataObject);
		}

		// Token: 0x04003B9A RID: 15258
		private static MailPublicFolderSchema schema = ObjectSchema.GetInstance<MailPublicFolderSchema>();
	}
}
