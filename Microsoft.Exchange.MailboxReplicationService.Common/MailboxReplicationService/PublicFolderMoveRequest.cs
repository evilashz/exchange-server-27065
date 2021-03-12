using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001EB RID: 491
	[Serializable]
	public sealed class PublicFolderMoveRequest : RequestBase
	{
		// Token: 0x060014C5 RID: 5317 RVA: 0x0002EB77 File Offset: 0x0002CD77
		public PublicFolderMoveRequest()
		{
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x0002EB7F File Offset: 0x0002CD7F
		internal PublicFolderMoveRequest(IRequestIndexEntry index) : base(index)
		{
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x060014C7 RID: 5319 RVA: 0x0002EB88 File Offset: 0x0002CD88
		public new ADObjectId SourceMailbox
		{
			get
			{
				return base.SourceMailbox;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x0002EB90 File Offset: 0x0002CD90
		public new ADObjectId TargetMailbox
		{
			get
			{
				return base.TargetMailbox;
			}
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x0002EB98 File Offset: 0x0002CD98
		public override string ToString()
		{
			if (base.Name != null && base.OrganizationId != null)
			{
				return string.Format("{0}\\{1}", base.OrganizationId.ToString(), base.Name);
			}
			return base.ToString();
		}
	}
}
