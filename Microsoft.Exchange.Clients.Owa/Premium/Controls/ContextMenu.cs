using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020002FF RID: 767
	public abstract class ContextMenu
	{
		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06001CFC RID: 7420 RVA: 0x000A717E File Offset: 0x000A537E
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x000A7186 File Offset: 0x000A5386
		protected static void RenderMenuDivider(TextWriter output, string id)
		{
			ContextMenu.RenderMenuDivider(output, id, true);
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x000A7190 File Offset: 0x000A5390
		protected static void RenderMenuDivider(TextWriter output, string id, bool setIsMenuDivider)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (id != null && id.Length == 0)
			{
				throw new InvalidOperationException("Argument id cannot be string empty");
			}
			output.Write("<div");
			if (id != null)
			{
				output.Write(" id=");
				output.Write(id);
			}
			output.Write(" class=\"cmDvdr\"");
			if (setIsMenuDivider)
			{
				output.Write(" _isMnuDvdr=1");
			}
			output.Write("><span>&nbsp;</span></div>");
		}

		// Token: 0x06001CFF RID: 7423
		protected abstract void RenderMenuItems(TextWriter output);

		// Token: 0x06001D00 RID: 7424 RVA: 0x000A7205 File Offset: 0x000A5405
		public ContextMenu(string id, UserContext userContext) : this(id, userContext, true, false, null)
		{
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x000A7212 File Offset: 0x000A5412
		protected ContextMenu(string id, UserContext userContext, bool hasImages) : this(id, userContext, hasImages, false, null)
		{
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x000A721F File Offset: 0x000A541F
		protected ContextMenu(string id, UserContext userContext, bool hasImages, string className) : this(id, userContext, hasImages, false, className)
		{
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x000A722D File Offset: 0x000A542D
		protected ContextMenu(string id, UserContext userContext, bool hasImages, bool isAnr) : this(id, userContext, hasImages, isAnr, null)
		{
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x000A723C File Offset: 0x000A543C
		protected ContextMenu(string id, UserContext userContext, bool hasImages, bool isAnr, string className)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentException("id may not be null or empty string");
			}
			this.id = id;
			this.userContext = userContext;
			this.hasImages = hasImages;
			this.isAnr = isAnr;
			this.className = className;
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06001D05 RID: 7429 RVA: 0x000A72B2 File Offset: 0x000A54B2
		protected virtual bool HasShadedColumn
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06001D06 RID: 7430 RVA: 0x000A72B5 File Offset: 0x000A54B5
		protected virtual ushort ImagePadding
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001D07 RID: 7431 RVA: 0x000A72B9 File Offset: 0x000A54B9
		private bool IsLazyLoadAsSubmenu
		{
			get
			{
				return !string.IsNullOrEmpty(this.clientScript);
			}
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x000A72CC File Offset: 0x000A54CC
		public void Render(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			output.Write("<div id=");
			output.Write(this.id);
			output.Write(" class=\"ctxMnu");
			if (this.hasImages)
			{
				output.Write(" ctxMnuIco");
			}
			if (this.shouldScroll)
			{
				output.Write(" ctxMnuScrl");
			}
			if (!string.IsNullOrEmpty(this.className))
			{
				output.Write(" ");
				output.Write(this.className);
			}
			output.Write("\"");
			if (this.shouldScroll)
			{
				output.Write(" fScrl=1");
			}
			this.RenderExpandoData(output);
			output.Write(" style=display:none>");
			if (this.HasShadedColumn)
			{
				output.Write("<div id=\"cmpane\"></div>");
			}
			this.RenderMenuItems(output);
			if (!this.shouldScroll)
			{
				RenderingUtilities.RenderDropShadows(output, this.userContext);
			}
			output.Write("</div>");
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x000A73BC File Offset: 0x000A55BC
		protected virtual void RenderExpandoData(TextWriter output)
		{
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x000A73BE File Offset: 0x000A55BE
		protected virtual void RenderMenuItemExpandoData(TextWriter output)
		{
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x000A73C0 File Offset: 0x000A55C0
		protected void RenderHeader(TextWriter output)
		{
			output.Write("<div class=\"sttc\" nowrap><span id=\"spnImg\" class=\"cmIco\">");
			RenderingUtilities.RenderInlineSpacer(output, this.userContext, 16, "imgI");
			output.Write("</span>");
			RenderingUtilities.RenderInlineSpacer(output, this.userContext, 12);
			output.Write("<span id=\"spnHdr\"></span></div>");
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x000A740F File Offset: 0x000A560F
		protected void RenderMenuItem(TextWriter output, Strings.IDs displayString, string command)
		{
			this.RenderMenuItem(output, displayString, ThemeFileId.None, null, command, false);
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x000A741D File Offset: 0x000A561D
		protected void RenderMenuItem(TextWriter output, Strings.IDs displayString, string command, string additionalStyles)
		{
			this.RenderMenuItem(output, displayString, null, command, additionalStyles);
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x000A742C File Offset: 0x000A562C
		protected void RenderMenuItem(TextWriter output, Strings.IDs displayString, string id, string command, string additionalStyles)
		{
			this.RenderMenuItem(output, LocalizedStrings.GetNonEncoded(displayString), null, id, command, false, null, additionalStyles, null, null, null, null, false);
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x000A7453 File Offset: 0x000A5653
		protected void RenderMenuItem(TextWriter output, Strings.IDs displayString, string command, bool disabled)
		{
			this.RenderMenuItem(output, displayString, ThemeFileId.None, null, command, disabled);
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x000A7462 File Offset: 0x000A5662
		protected void RenderMenuItem(TextWriter output, Strings.IDs displayString, ThemeFileId imageFileId, string id, string command)
		{
			this.RenderMenuItem(output, displayString, imageFileId, id, command, false);
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x000A7474 File Offset: 0x000A5674
		protected void RenderMenuItem(TextWriter output, Strings.IDs displayString, ThemeFileId imageFileId, string id, string command, bool disabled)
		{
			this.RenderMenuItem(output, displayString, imageFileId, id, command, disabled, null, null, null);
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x000A7494 File Offset: 0x000A5694
		protected void RenderMenuItem(TextWriter output, Strings.IDs displayString, ThemeFileId imageFileId, string id, string command, bool disabled, string onMouseOverScript, string onMouseOutScript)
		{
			this.RenderMenuItem(output, displayString, imageFileId, id, command, disabled, onMouseOverScript, onMouseOutScript, null);
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x000A74B8 File Offset: 0x000A56B8
		protected void RenderMenuItem(TextWriter output, Strings.IDs displayString, ThemeFileId imageFileId, string id, string command, bool disabled, string onMouseOverScript, string onMouseOutScript, ContextMenu subContextMenu)
		{
			this.RenderMenuItem(output, LocalizedStrings.GetNonEncoded(displayString), imageFileId, id, command, disabled, onMouseOverScript, onMouseOutScript, subContextMenu, null);
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x000A74E0 File Offset: 0x000A56E0
		protected void RenderMenuItem(TextWriter output, string text, ThemeFileId imageFileId, string id, string command, bool disabled, string onMouseOverScript, string onMouseOutScript)
		{
			this.RenderMenuItem(output, text, imageFileId, id, command, disabled, onMouseOverScript, onMouseOutScript, null, null);
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x000A7504 File Offset: 0x000A5704
		protected void RenderMenuItem(TextWriter output, string text, ThemeFileId imageFileId, string id, string command, bool disabled, string onMouseOverScript, string onMouseOutScript, ContextMenu subContextMenu, ContextMenu.RenderMenuItemHtml renderMenuItemHtml)
		{
			this.RenderMenuItem(output, text, imageFileId, id, command, disabled, onMouseOverScript, onMouseOutScript, subContextMenu, renderMenuItemHtml, false);
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x000A756C File Offset: 0x000A576C
		protected void RenderMenuItem(TextWriter output, string text, ThemeFileId imageFileId, string id, string command, bool disabled, string onMouseOverScript, string onMouseOutScript, ContextMenu subContextMenu, ContextMenu.RenderMenuItemHtml renderMenuItemHtml, bool blockContent)
		{
			ContextMenu.RenderMenuItemIcon renderMenuItemIcon = null;
			if (this.hasImages && imageFileId != ThemeFileId.None)
			{
				renderMenuItemIcon = delegate(TextWriter writer)
				{
					this.userContext.RenderThemeImage(writer, imageFileId, null, new object[]
					{
						"id=imgI"
					});
				};
			}
			this.RenderMenuItem(output, text, renderMenuItemIcon, id, command, disabled, null, onMouseOverScript, onMouseOutScript, subContextMenu, renderMenuItemHtml, blockContent);
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x000A75FC File Offset: 0x000A57FC
		protected void RenderMenuItem(TextWriter output, string text, string imageUrl, string id, string command, bool disabled, string additionalAttributes, string onMouseOverScript, string onMouseOutScript, ContextMenu subContextMenu, ContextMenu.RenderMenuItemHtml renderMenuItemHtml)
		{
			ContextMenu.RenderMenuItemIcon renderMenuItemIcon = null;
			if (this.hasImages && imageUrl != null)
			{
				renderMenuItemIcon = delegate(TextWriter writer)
				{
					writer.Write("<img id=\"imgI\" class=\"cstmMnuImg\" src=\"");
					Utilities.SanitizeHtmlEncode(imageUrl, writer);
					writer.Write("\">");
				};
			}
			this.RenderMenuItem(output, text, renderMenuItemIcon, id, command, disabled, additionalAttributes, onMouseOverScript, onMouseOutScript, subContextMenu, renderMenuItemHtml, false);
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x000A7698 File Offset: 0x000A5898
		protected void RenderMenuItem(TextWriter output, string text, ThemeFileId imageFileId, string id, string command, bool disabled, string additionalAttributes, string onMouseOverScript, string onMouseOutScript, ContextMenu subContextMenu, ContextMenu.RenderMenuItemHtml renderMenuItemHtml, bool blockContent)
		{
			ContextMenu.RenderMenuItemIcon renderMenuItemIcon = null;
			if (this.hasImages && imageFileId != ThemeFileId.None)
			{
				renderMenuItemIcon = delegate(TextWriter writer)
				{
					this.userContext.RenderThemeImage(writer, imageFileId, null, new object[]
					{
						"id=imgI"
					});
				};
			}
			this.RenderMenuItem(output, text, renderMenuItemIcon, id, command, disabled, additionalAttributes, onMouseOverScript, onMouseOutScript, subContextMenu, renderMenuItemHtml, blockContent);
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x000A76FC File Offset: 0x000A58FC
		protected void RenderMenuItem(TextWriter output, string text, ContextMenu.RenderMenuItemIcon renderMenuItemIcon, string id, string command, bool disabled, string additionalAttributes, string onMouseOverScript, string onMouseOutScript, ContextMenu subContextMenu, ContextMenu.RenderMenuItemHtml renderMenuItemHtml, bool blockContent)
		{
			this.RenderMenuItem(output, text, renderMenuItemIcon, id, command, disabled, additionalAttributes, null, onMouseOverScript, onMouseOutScript, subContextMenu, renderMenuItemHtml, blockContent);
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x000A7728 File Offset: 0x000A5928
		protected void RenderMenuItem(TextWriter output, string text, ContextMenu.RenderMenuItemIcon renderMenuItemIcon, string id, string command, bool disabled, string additionalAttributes, string additionalStyles, string onMouseOverScript, string onMouseOutScript, ContextMenu subContextMenu, ContextMenu.RenderMenuItemHtml renderMenuItemHtml, bool blockContent)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (renderMenuItemHtml == null && text == null)
			{
				throw new InvalidOperationException("Must have either a non null string text or a non null renderMenuItemHtml");
			}
			if (!this.hasImages && renderMenuItemIcon != null)
			{
				throw new InvalidOperationException("This menu doesn't have images");
			}
			if (id != null && id.Length == 0)
			{
				throw new InvalidOperationException("Argument id cannot be string empty");
			}
			output.Write("<div _lnk=");
			output.Write(disabled ? "0" : "1");
			output.Write(" class=\"cmLnk");
			if (disabled)
			{
				output.Write(" cmDsbld");
			}
			if (subContextMenu != null)
			{
				output.Write(" subMnu");
			}
			if (!string.IsNullOrEmpty(additionalStyles))
			{
				output.Write(" ");
				output.Write(additionalStyles);
			}
			if (blockContent)
			{
				output.Write(" blockContent");
			}
			output.Write("\"");
			if (!string.IsNullOrEmpty(additionalAttributes))
			{
				output.Write(" ");
				output.Write(additionalAttributes);
			}
			if (id != null)
			{
				output.Write(" id=");
				output.Write(id);
			}
			if (command != null)
			{
				output.Write(" cmd=\"");
				output.Write(command);
				output.Write("\"");
			}
			if (!string.IsNullOrEmpty(onMouseOverScript))
			{
				output.Write(" onmouseover=\"");
				output.Write(onMouseOverScript);
				output.Write("\"");
			}
			if (!string.IsNullOrEmpty(onMouseOutScript))
			{
				output.Write(" onmouseout=\"");
				output.Write(onMouseOutScript);
				output.Write("\"");
			}
			if (subContextMenu != null)
			{
				if (subContextMenu.IsLazyLoadAsSubmenu)
				{
					output.Write(" sSmScript=\"");
					output.Write(subContextMenu.clientScript);
					output.Write("\"");
				}
				else
				{
					output.Write(" sSmId=");
					output.Write(subContextMenu.Id);
				}
			}
			this.RenderMenuItemExpandoData(output);
			output.Write(">");
			this.RenderMenuItemInnerSpan(output, text, renderMenuItemIcon, renderMenuItemHtml, blockContent);
			if (subContextMenu != null)
			{
				RenderingUtilities.RenderInlineSpacer(output, this.userContext, 1);
				ThemeFileId themeFileId = this.userContext.IsRtl ? ThemeFileId.LeftArrow : ThemeFileId.RightArrow;
				this.userContext.RenderThemeImage(output, themeFileId, null, new object[]
				{
					"id=imgRA"
				});
			}
			output.Write("</div>");
			if (subContextMenu != null && !subContextMenu.IsLazyLoadAsSubmenu)
			{
				subContextMenu.Render(output);
			}
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x000A7978 File Offset: 0x000A5B78
		protected void RenderMenuItemInnerSpan(TextWriter output, string text, ContextMenu.RenderMenuItemIcon renderMenuItemIcon, ContextMenu.RenderMenuItemHtml renderMenuItemHtml, bool blockContent)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (renderMenuItemHtml == null && text == null)
			{
				throw new InvalidOperationException("Must have either a non null string text or a non null renderMenuItemHtml");
			}
			if (!this.hasImages && renderMenuItemIcon != null)
			{
				throw new InvalidOperationException("This menu doesn't have images");
			}
			if (this.hasImages || this.isAnr)
			{
				output.Write("<span class=\"cmIco\"");
				if (this.id != null)
				{
					output.Write(" id=spnImg");
				}
				output.Write(">");
				if (!this.isAnr)
				{
					if (renderMenuItemIcon != null)
					{
						renderMenuItemIcon(output);
					}
					else
					{
						RenderingUtilities.RenderInlineSpacer(output, this.userContext, 16, "imgI");
					}
				}
				output.Write("</span>");
				if (!blockContent)
				{
					RenderingUtilities.RenderInlineSpacer(output, this.userContext, this.ImagePadding);
				}
			}
			if (!blockContent)
			{
				output.Write("<span id=spnT>");
			}
			if (text != null)
			{
				string text2 = Utilities.HtmlEncode(text);
				text2 = text2.Replace("\\n", "<br>");
				output.Write(SanitizedHtmlString.GetSanitizedStringWithoutEncoding(text2));
			}
			else
			{
				renderMenuItemHtml(output);
			}
			if (!blockContent)
			{
				output.Write("</span>");
			}
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x000A7AC4 File Offset: 0x000A5CC4
		protected void RenderNoOpMenuItem(TextWriter output, string id, Strings.IDs labelResourceId, string spanId)
		{
			output.Write("<div");
			output.Write(" id=");
			output.Write(id);
			output.Write(">");
			this.RenderMenuItemInnerSpan(output, null, null, delegate(TextWriter writer)
			{
				writer.Write(LocalizedStrings.GetHtmlEncoded(labelResourceId));
				writer.Write("&nbsp;<span id=\"" + spanId + "\">...</span>");
			}, false);
			output.Write("</div>");
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x000A7B30 File Offset: 0x000A5D30
		protected void RenderMenuHeader(TextWriter output, string id, Strings.IDs displayString, string additionalStyles)
		{
			output.Write("<div");
			if (!string.IsNullOrEmpty(id))
			{
				output.Write(" id=\"");
				output.Write(id);
				output.Write("\"");
			}
			output.Write(" class=\"mnuItmTxtHdr ");
			if (!string.IsNullOrEmpty(id))
			{
				output.Write(additionalStyles);
			}
			output.Write("\">");
			output.Write(LocalizedStrings.GetHtmlEncoded(displayString));
			output.Write("</div>");
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001D1E RID: 7454 RVA: 0x000A7BAA File Offset: 0x000A5DAA
		protected UserContext UserContext
		{
			get
			{
				return this.userContext;
			}
		}

		// Token: 0x0400155E RID: 5470
		protected UserContext userContext;

		// Token: 0x0400155F RID: 5471
		protected bool hasImages = true;

		// Token: 0x04001560 RID: 5472
		protected bool isAnr;

		// Token: 0x04001561 RID: 5473
		protected bool shouldScroll;

		// Token: 0x04001562 RID: 5474
		private string id = string.Empty;

		// Token: 0x04001563 RID: 5475
		private string className = string.Empty;

		// Token: 0x04001564 RID: 5476
		protected string clientScript;

		// Token: 0x02000300 RID: 768
		// (Invoke) Token: 0x06001D20 RID: 7456
		protected delegate void RenderMenuItemHtml(TextWriter output);

		// Token: 0x02000301 RID: 769
		// (Invoke) Token: 0x06001D24 RID: 7460
		protected delegate void RenderMenuItemIcon(TextWriter output);
	}
}
