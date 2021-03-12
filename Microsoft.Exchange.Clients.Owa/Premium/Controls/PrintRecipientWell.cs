using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003F1 RID: 1009
	public class PrintRecipientWell : RecipientWell
	{
		// Token: 0x060024FD RID: 9469 RVA: 0x000D6596 File Offset: 0x000D4796
		public PrintRecipientWell(RecipientWell delegateRecipient)
		{
			this.delegateRecipient = delegateRecipient;
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x000D65A8 File Offset: 0x000D47A8
		public override void Render(TextWriter writer, UserContext userContext, RecipientWellType type, RecipientWell.RenderFlags flags, string id, string content, string extraStyle)
		{
			RecipientWellNode.RenderFlags renderFlags = RecipientWellNode.RenderFlags.RenderCommas;
			if ((flags & RecipientWell.RenderFlags.ReadOnly) != RecipientWell.RenderFlags.None)
			{
				renderFlags |= RecipientWellNode.RenderFlags.ReadOnly;
			}
			if (content != null)
			{
				Utilities.HtmlEncode(content, writer);
				return;
			}
			this.RenderContents(writer, userContext, type, renderFlags, new RenderRecipientWellNode(PrintRecipientWellNode.Render));
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x000D65E4 File Offset: 0x000D47E4
		internal override void RenderContents(TextWriter writer, UserContext userContext, RecipientWellType type, RecipientWellNode.RenderFlags flags, RenderRecipientWellNode wellNode)
		{
			this.delegateRecipient.RenderContents(writer, userContext, type, flags, wellNode);
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x000D65F8 File Offset: 0x000D47F8
		public override void Render(TextWriter writer, UserContext userContext, RecipientWellType type, RecipientWell.RenderFlags flags)
		{
			this.Render(writer, userContext, type, flags, string.Empty, null, string.Empty);
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x000D6610 File Offset: 0x000D4810
		public override void Render(TextWriter writer, UserContext userContext, RecipientWellType type, RecipientWell.RenderFlags flags, string id)
		{
			this.Render(writer, userContext, type, flags, id, null, string.Empty);
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x000D6625 File Offset: 0x000D4825
		public override void Render(TextWriter writer, UserContext userContext, RecipientWellType type)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			this.Render(writer, userContext, type, RecipientWell.RenderFlags.None);
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x000D664D File Offset: 0x000D484D
		public override bool HasRecipients(RecipientWellType type)
		{
			return this.delegateRecipient.HasRecipients(type);
		}

		// Token: 0x04001997 RID: 6551
		private RecipientWell delegateRecipient;
	}
}
