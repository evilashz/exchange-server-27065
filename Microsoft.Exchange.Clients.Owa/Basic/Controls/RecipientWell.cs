using System;
using System.Collections;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000018 RID: 24
	public abstract class RecipientWell
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x000068A0 File Offset: 0x00004AA0
		public static void ResolveRecipients(string chunk, ArrayList wellNodes, UserContext userContext, bool resolveAgainstContactsFirst)
		{
			if (chunk == null)
			{
				throw new ArgumentNullException("chunk");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			string[] array = Utilities.ParseRecipientChunk(chunk);
			if (array != null)
			{
				RecipientCache recipientCache = AutoCompleteCache.TryGetCache(OwaContext.Current.UserContext);
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i].Trim();
					if (text.Length > 0)
					{
						RecipientAddress recipientAddress = AnrManager.ResolveAnrString(text, resolveAgainstContactsFirst, userContext);
						RecipientWellNode recipientWellNode = new RecipientWellNode(text, null);
						if (recipientAddress != null)
						{
							recipientWellNode.DisplayName = recipientAddress.DisplayName;
							recipientWellNode.SmtpAddress = recipientAddress.SmtpAddress;
							recipientWellNode.AddressOrigin = recipientAddress.AddressOrigin;
							recipientWellNode.RoutingAddress = recipientAddress.RoutingAddress;
							recipientWellNode.RoutingType = recipientAddress.RoutingType;
							recipientWellNode.RecipientFlags = recipientAddress.RecipientFlags;
							recipientWellNode.StoreObjectId = recipientAddress.StoreObjectId;
							recipientWellNode.ADObjectId = recipientAddress.ADObjectId;
							recipientWellNode.EmailAddressIndex = recipientAddress.EmailAddressIndex;
						}
						if ((recipientWellNode.RoutingAddress != null || Utilities.IsMapiPDL(recipientWellNode.RoutingType)) && recipientCache != null)
						{
							string itemId = string.Empty;
							if (recipientWellNode.AddressOrigin == AddressOrigin.Store && recipientWellNode.StoreObjectId != null)
							{
								itemId = recipientWellNode.StoreObjectId.ToBase64String();
							}
							else if (recipientWellNode.AddressOrigin == AddressOrigin.Directory && recipientWellNode.ADObjectId != null)
							{
								itemId = Convert.ToBase64String(recipientWellNode.ADObjectId.ObjectGuid.ToByteArray());
							}
							if (string.IsNullOrEmpty(recipientWellNode.DisplayName))
							{
								recipientWellNode.DisplayName = recipientWellNode.SmtpAddress;
							}
							if (userContext.UserOptions.AddRecipientsToAutoCompleteCache)
							{
								recipientCache.AddEntry(recipientWellNode.DisplayName, recipientWellNode.SmtpAddress, recipientWellNode.RoutingAddress, string.Empty, recipientWellNode.RoutingType, recipientWellNode.AddressOrigin, recipientWellNode.RecipientFlags, itemId, recipientWellNode.EmailAddressIndex);
							}
						}
						if (wellNodes != null)
						{
							wellNodes.Add(recipientWellNode);
						}
					}
				}
				if (recipientCache != null && recipientCache.IsDirty)
				{
					recipientCache.Commit(true);
				}
			}
		}

		// Token: 0x060000B1 RID: 177
		public abstract bool HasRecipients(RecipientWellType type);

		// Token: 0x060000B2 RID: 178
		protected abstract void RenderContents(TextWriter writer, RecipientWellType type, RecipientWellNode.RenderFlags flags);

		// Token: 0x060000B3 RID: 179 RVA: 0x00006AA2 File Offset: 0x00004CA2
		public void Render(TextWriter writer, RecipientWellType type, RecipientWell.RenderFlags flags)
		{
			this.Render(writer, type, flags, string.Empty);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00006AB4 File Offset: 0x00004CB4
		public void Render(TextWriter writer, RecipientWellType type, RecipientWell.RenderFlags flags, string id)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (!this.HasRecipients(type))
			{
				return;
			}
			bool flag = (flags & RecipientWell.RenderFlags.ReadOnly) != RecipientWell.RenderFlags.None;
			if (!flag)
			{
				writer.Write("<table class=\"rcptWll\" cellspacing=0 cellpadding=0 border=0><tr><td>");
			}
			if (!string.IsNullOrEmpty(id))
			{
				writer.Write("<div id=\"div{0}\"", id);
			}
			else
			{
				writer.Write("<div id=\"div{0}\"", RecipientWell.GetWellName(type));
			}
			if (flag)
			{
				writer.Write(" class=\"rwWRO\"");
			}
			writer.Write(">");
			RecipientWellNode.RenderFlags renderFlags = RecipientWellNode.RenderFlags.RenderCommas;
			if (flag)
			{
				renderFlags |= RecipientWellNode.RenderFlags.ReadOnly;
			}
			this.RenderContents(writer, type, renderFlags);
			writer.Write("</div>");
			if (!flag)
			{
				writer.Write("</td></tr></table>");
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00006B6D File Offset: 0x00004D6D
		public void Render(TextWriter writer, RecipientWellType type)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.Render(writer, type, RecipientWell.RenderFlags.None);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00006B88 File Offset: 0x00004D88
		public bool RenderAnr(TextWriter writer, UserContext userContext)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			RecipientWellType type = RecipientWellType.To;
			bool flag = false;
			bool flag2 = true;
			while (flag2)
			{
				if (this.HasRecipients(type))
				{
					flag = this.RenderAnrContents(writer, userContext, type, flag);
				}
				switch (type)
				{
				case RecipientWellType.To:
					type = RecipientWellType.Cc;
					break;
				case RecipientWellType.Cc:
					type = RecipientWellType.Bcc;
					break;
				case RecipientWellType.Bcc:
					flag2 = false;
					break;
				}
			}
			if (flag)
			{
				writer.Write("<tr><td class=\"h100\"></td></tr></table></td></tr></table>");
			}
			return flag;
		}

		// Token: 0x060000B7 RID: 183
		protected abstract bool RenderAnrContents(TextWriter writer, UserContext userContext, RecipientWellType type, bool isTableStartRendered);

		// Token: 0x060000B8 RID: 184 RVA: 0x00006C00 File Offset: 0x00004E00
		private static string GetWellName(RecipientWellType type)
		{
			switch (type)
			{
			case RecipientWellType.To:
				return "To";
			case RecipientWellType.Cc:
				return "Cc";
			case RecipientWellType.Bcc:
				return "Bcc";
			default:
				return null;
			}
		}

		// Token: 0x0400007D RID: 125
		private static readonly char[] recipientSeparator = new char[]
		{
			';'
		};

		// Token: 0x02000019 RID: 25
		[Flags]
		public enum RenderFlags
		{
			// Token: 0x0400007F RID: 127
			None = 0,
			// Token: 0x04000080 RID: 128
			ReadOnly = 1
		}
	}
}
