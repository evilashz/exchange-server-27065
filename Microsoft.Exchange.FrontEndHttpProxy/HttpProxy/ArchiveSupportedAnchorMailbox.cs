using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000009 RID: 9
	internal abstract class ArchiveSupportedAnchorMailbox : UserBasedAnchorMailbox
	{
		// Token: 0x0600004F RID: 79 RVA: 0x000036EA File Offset: 0x000018EA
		protected ArchiveSupportedAnchorMailbox(AnchorSource anchorSource, object sourceObject, IRequestContext requestContext) : base(anchorSource, sourceObject, requestContext)
		{
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000036F5 File Offset: 0x000018F5
		// (set) Token: 0x06000051 RID: 81 RVA: 0x000036FD File Offset: 0x000018FD
		public bool? IsArchive { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00003708 File Offset: 0x00001908
		protected override ADPropertyDefinition[] PropertySet
		{
			get
			{
				if (this.IsArchive != null && this.IsArchive.Value)
				{
					return ArchiveSupportedAnchorMailbox.ArchiveMailboxADRawEntryPropertySet;
				}
				return base.PropertySet;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00003744 File Offset: 0x00001944
		protected override ADPropertyDefinition DatabaseProperty
		{
			get
			{
				if (this.IsArchive != null && this.IsArchive.Value)
				{
					return ADUserSchema.ArchiveDatabase;
				}
				return base.DatabaseProperty;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003780 File Offset: 0x00001980
		protected override string ToCacheKey()
		{
			string text = base.ToCacheKey();
			if (this.IsArchive != null && this.IsArchive.Value)
			{
				text += "_Archive";
			}
			return text;
		}

		// Token: 0x04000026 RID: 38
		protected static readonly ADPropertyDefinition[] ArchiveMailboxADRawEntryPropertySet = new ADPropertyDefinition[]
		{
			ADObjectSchema.OrganizationId,
			ADUserSchema.ArchiveDatabase,
			ADRecipientSchema.PrimarySmtpAddress
		};
	}
}
