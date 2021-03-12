using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.Data
{
	// Token: 0x02000041 RID: 65
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class BandRandomEntitySelector : EntitySelector
	{
		// Token: 0x06000260 RID: 608 RVA: 0x00007F8C File Offset: 0x0000618C
		protected BandRandomEntitySelector(Band band, LoadContainer sourceContainer, string constraintSetIdentity)
		{
			AnchorUtil.ThrowOnNullArgument(band, "band");
			AnchorUtil.ThrowOnNullArgument(sourceContainer, "sourceContainer");
			this.band = band;
			this.SourceEntities = (from x in sourceContainer.Children
			where x.DirectoryObjectIdentity.Name == constraintSetIdentity
			select x).Cast<LoadContainer>().SelectMany((LoadContainer x) => x.Children).Where(new Func<LoadEntity, bool>(this.IsAcceptedEntity));
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00008026 File Offset: 0x00006226
		public override bool IsEmpty
		{
			get
			{
				return !this.SourceEntities.Any<LoadEntity>();
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00008036 File Offset: 0x00006236
		// (set) Token: 0x06000263 RID: 611 RVA: 0x0000803E File Offset: 0x0000623E
		private protected IEnumerable<LoadEntity> SourceEntities { protected get; private set; }

		// Token: 0x06000264 RID: 612 RVA: 0x00008048 File Offset: 0x00006248
		protected virtual bool IsAcceptedEntity(LoadEntity entity)
		{
			DirectoryMailbox directoryMailbox = entity.DirectoryObject as DirectoryMailbox;
			return directoryMailbox != null && this.band.ContainsMailbox(directoryMailbox);
		}

		// Token: 0x040000A8 RID: 168
		private readonly Band band;
	}
}
