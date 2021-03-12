using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x02000983 RID: 2435
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AttachmentPropertyRestriction : PropertyRestriction
	{
		// Token: 0x060059F5 RID: 23029 RVA: 0x00174F2C File Offset: 0x0017312C
		public AttachmentPropertyRestriction()
		{
			this.BlockAfterLink.Add(AttachmentSchema.DisplayName);
			this.BlockAfterLink.Add(AttachmentSchema.AttachLongPathName);
			this.BlockAfterLink.Add(AttachmentSchema.AttachMethod);
			this.BlockAfterLink.Add(AttachmentSchema.AttachFileName);
		}

		// Token: 0x04003176 RID: 12662
		public static AttachmentPropertyRestriction Instance = new AttachmentPropertyRestriction();
	}
}
