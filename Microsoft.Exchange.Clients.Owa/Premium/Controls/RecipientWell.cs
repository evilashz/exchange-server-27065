using System;
using System.Collections;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000325 RID: 805
	public abstract class RecipientWell
	{
		// Token: 0x06001E8F RID: 7823
		public abstract bool HasRecipients(RecipientWellType type);

		// Token: 0x06001E90 RID: 7824 RVA: 0x000B01A8 File Offset: 0x000AE3A8
		internal static void ResolveAndRenderChunk(TextWriter writer, string chunk, ArrayList wellNodes, RecipientCache recipientCache, UserContext userContext, bool resolveAgainstContactsFirst)
		{
			RecipientWell.ResolveAndRenderChunk(writer, chunk, wellNodes, recipientCache, userContext, new AnrManager.Options
			{
				ResolveContactsFirst = resolveAgainstContactsFirst
			});
		}

		// Token: 0x06001E91 RID: 7825 RVA: 0x000B01D0 File Offset: 0x000AE3D0
		internal static void ResolveAndRenderChunk(TextWriter writer, string chunk, ArrayList wellNodes, RecipientCache recipientCache, UserContext userContext, AnrManager.Options options)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (chunk == null)
			{
				throw new ArgumentNullException("chunk");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			writer.Write("<div id=\"chk\">");
			string[] array = Utilities.ParseRecipientChunk(chunk);
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i].Trim();
					if (text.IndexOf(Globals.HtmlDirectionCharacterString, StringComparison.Ordinal) != -1)
					{
						text = Utilities.RemoveHTMLDirectionCharacters(text);
					}
					if (text.Length > 0)
					{
						RecipientAddress recipientAddress = AnrManager.ResolveAnrString(text, options, userContext);
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
							recipientWellNode.EmailAddressIndex = recipientAddress.EmailAddressIndex;
							recipientWellNode.ADObjectId = recipientAddress.ADObjectId;
							recipientWellNode.SipUri = recipientAddress.SipUri;
							recipientWellNode.MobilePhoneNumber = recipientAddress.MobilePhoneNumber;
						}
						if ((recipientWellNode.RoutingAddress != null || Utilities.IsMapiPDL(recipientWellNode.RoutingType)) && recipientCache != null)
						{
							string itemId = string.Empty;
							if (recipientWellNode.StoreObjectId != null)
							{
								itemId = recipientWellNode.StoreObjectId.ToBase64String();
							}
							else if (recipientWellNode.ADObjectId != null)
							{
								itemId = Convert.ToBase64String(recipientWellNode.ADObjectId.ObjectGuid.ToByteArray());
							}
							recipientCache.AddEntry(recipientWellNode.DisplayName, recipientWellNode.SmtpAddress, recipientWellNode.RoutingAddress, string.Empty, recipientWellNode.RoutingType, recipientWellNode.AddressOrigin, recipientWellNode.RecipientFlags, itemId, recipientWellNode.EmailAddressIndex, recipientWellNode.SipUri, recipientWellNode.MobilePhoneNumber);
						}
						recipientWellNode.Render(writer, userContext, RecipientWellNode.RenderFlags.RenderSkinnyHtml);
						if (wellNodes != null)
						{
							wellNodes.Add(recipientWellNode);
						}
					}
				}
			}
			writer.Write("</div>");
		}

		// Token: 0x06001E92 RID: 7826
		internal abstract void RenderContents(TextWriter writer, UserContext userContext, RecipientWellType type, RecipientWellNode.RenderFlags flags, RenderRecipientWellNode wellNode);

		// Token: 0x06001E93 RID: 7827 RVA: 0x000B03DB File Offset: 0x000AE5DB
		protected virtual void RenderAdditionalExpandos(TextWriter writer)
		{
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x000B03DD File Offset: 0x000AE5DD
		public virtual void Render(TextWriter writer, UserContext userContext, RecipientWellType type, RecipientWell.RenderFlags flags)
		{
			this.Render(writer, userContext, type, flags, string.Empty, null, string.Empty);
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x000B03F5 File Offset: 0x000AE5F5
		public virtual void Render(TextWriter writer, UserContext userContext, RecipientWellType type, string extraStyle)
		{
			this.Render(writer, userContext, type, RecipientWell.RenderFlags.None, string.Empty, null, extraStyle);
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x000B0409 File Offset: 0x000AE609
		public virtual void Render(TextWriter writer, UserContext userContext, RecipientWellType type, RecipientWell.RenderFlags flags, string id)
		{
			this.Render(writer, userContext, type, flags, id, null, string.Empty);
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x000B0420 File Offset: 0x000AE620
		public virtual void Render(TextWriter writer, UserContext userContext, RecipientWellType type, RecipientWell.RenderFlags flags, string id, string content, string extraStyle)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			bool flag = (flags & RecipientWell.RenderFlags.ReadOnly) != RecipientWell.RenderFlags.None;
			if (flag && !this.HasRecipients(type) && type != RecipientWellType.From)
			{
				return;
			}
			if (flags != RecipientWell.RenderFlags.Hidden)
			{
				writer.Write("<div id=divFH>M</div>");
			}
			if (!string.IsNullOrEmpty(id))
			{
				writer.Write("<div id=\"div{0}\"", id);
			}
			else
			{
				writer.Write("<div id=\"div{0}\"", RecipientWell.GetWellName(type));
			}
			if ((flags & RecipientWell.RenderFlags.Hidden) != RecipientWell.RenderFlags.None)
			{
				writer.Write(" disabled");
			}
			this.RenderAdditionalExpandos(writer);
			if (!flag)
			{
				writer.Write(" class=\"rwW");
				writer.Write(string.IsNullOrEmpty(extraStyle) ? SanitizedHtmlString.Empty : SanitizedHtmlString.Format(" {0}", new object[]
				{
					extraStyle
				}));
				writer.Write("\" contentEditable=\"true\" ");
				writer.Write("spellcheck=\"false\" ");
				writer.Write(">");
			}
			else
			{
				writer.Write(" class=\"rwW rwWRO\" ");
				writer.Write(">");
			}
			RecipientWellNode.RenderFlags renderFlags = RecipientWellNode.RenderFlags.RenderCommas;
			if ((flags & RecipientWell.RenderFlags.ReadOnly) != RecipientWell.RenderFlags.None)
			{
				renderFlags |= RecipientWellNode.RenderFlags.ReadOnly;
			}
			if (content != null)
			{
				Utilities.SanitizeHtmlEncode(content, writer);
			}
			else
			{
				if (userContext.BrowserType != BrowserType.IE && this.HasRecipients(type))
				{
					writer.Write("&nbsp;");
				}
				this.RenderContents(writer, userContext, type, renderFlags, RecipientWell.wellNodeRenderDelegate);
				if (userContext.BrowserType != BrowserType.IE && this.HasRecipients(type))
				{
					writer.Write("&nbsp;");
				}
			}
			writer.Write("</div>");
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x000B05A8 File Offset: 0x000AE7A8
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
			case RecipientWellType.From:
				return "From";
			case RecipientWellType.AddAttendee:
				return "AddAttndRW";
			case RecipientWellType.AddRoom:
				return "AddRmRW";
			case RecipientWellType.AutoSchedUser:
				return "AutoSched";
			case RecipientWellType.ManualSchedUser:
				return "ManualSched";
			case RecipientWellType.BothSchedUser:
				return "BothSched";
			case RecipientWellType.OpenMailbox:
				return "OpnMbxRw";
			case RecipientWellType.OpenOtherMailboxFolder:
				return "OpnOthMbxRw";
			case RecipientWellType.Select:
				return "Select";
			default:
				return null;
			}
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x000B0638 File Offset: 0x000AE838
		public virtual void Render(TextWriter writer, UserContext userContext, RecipientWellType type)
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

		// Token: 0x04001683 RID: 5763
		private static readonly char[] recipientSeparator = new char[]
		{
			';'
		};

		// Token: 0x04001684 RID: 5764
		private static RenderRecipientWellNode wellNodeRenderDelegate = new RenderRecipientWellNode(RecipientWellNode.Render);

		// Token: 0x02000326 RID: 806
		[Flags]
		public enum RenderFlags
		{
			// Token: 0x04001686 RID: 5766
			None = 0,
			// Token: 0x04001687 RID: 5767
			ReadOnly = 1,
			// Token: 0x04001688 RID: 5768
			Hidden = 2
		}
	}
}
