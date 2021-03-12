using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000374 RID: 884
	public class EmptyRecipientWell : RecipientWell
	{
		// Token: 0x06002112 RID: 8466 RVA: 0x000BE458 File Offset: 0x000BC658
		public override bool HasRecipients(RecipientWellType type)
		{
			return false;
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x000BE45B File Offset: 0x000BC65B
		internal override void RenderContents(TextWriter writer, UserContext userContext, RecipientWellType type, RecipientWellNode.RenderFlags flags, RenderRecipientWellNode wellNode)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			this.HasRecipients(type);
		}
	}
}
