using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020002FB RID: 763
	public abstract class Toolbar
	{
		// Token: 0x06001CC3 RID: 7363 RVA: 0x000A5C7B File Offset: 0x000A3E7B
		public Toolbar()
		{
			this.sessionContext = OwaContext.Current.SessionContext;
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x000A5CA5 File Offset: 0x000A3EA5
		public Toolbar(string id) : this()
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			this.id = id;
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x000A5CC2 File Offset: 0x000A3EC2
		public Toolbar(string id, ToolbarType toolbarType) : this(id)
		{
			this.toolbarType = toolbarType;
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x000A5CD2 File Offset: 0x000A3ED2
		public Toolbar(ToolbarType toolbarType) : this()
		{
			this.toolbarType = toolbarType;
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001CC7 RID: 7367 RVA: 0x000A5CE1 File Offset: 0x000A3EE1
		protected ISessionContext SessionContext
		{
			get
			{
				return this.sessionContext;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x000A5CE9 File Offset: 0x000A3EE9
		protected UserContext UserContext
		{
			get
			{
				return (UserContext)this.sessionContext;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001CC9 RID: 7369 RVA: 0x000A5CF6 File Offset: 0x000A3EF6
		protected TextWriter Writer
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001CCA RID: 7370 RVA: 0x000A5CFE File Offset: 0x000A3EFE
		// (set) Token: 0x06001CCB RID: 7371 RVA: 0x000A5D06 File Offset: 0x000A3F06
		public ToolbarType ToolbarType
		{
			get
			{
				return this.toolbarType;
			}
			set
			{
				this.toolbarType = value;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001CCC RID: 7372 RVA: 0x000A5D0F File Offset: 0x000A3F0F
		protected virtual bool IsNarrow
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x000A5D14 File Offset: 0x000A3F14
		public virtual void Render(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.writer = writer;
			writer.Write("<div id=\"");
			writer.Write(this.id);
			writer.Write("\" class=\"");
			if (this.toolbarType == ToolbarType.Form)
			{
				writer.Write("tfFrm");
			}
			else if (this.toolbarType == ToolbarType.View)
			{
				writer.Write("tfVw");
			}
			else if (this.toolbarType == ToolbarType.Preview)
			{
				writer.Write("tfPvw");
			}
			else if (this.toolbarType == ToolbarType.ChatWindow)
			{
				writer.Write("tfCht");
			}
			else if (this.toolbarType == ToolbarType.AddContact)
			{
				writer.Write("tfCon");
			}
			if (this.HasBigButton)
			{
				writer.Write(" tfBigHeight");
			}
			else
			{
				writer.Write(" tfHeight");
			}
			if (this.IsRightAligned)
			{
				writer.Write(" tfPosAfter");
			}
			else
			{
				writer.Write(" tfPosBefore");
			}
			writer.Write("\">");
			this.RenderButtons();
			writer.Write("</div>");
		}

		// Token: 0x06001CCE RID: 7374
		protected abstract void RenderButtons();

		// Token: 0x06001CCF RID: 7375 RVA: 0x000A5E1F File Offset: 0x000A401F
		protected void RenderButton(ToolbarButton button)
		{
			this.RenderButton(button, ToolbarButtonFlags.None, null, null);
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x000A5E2B File Offset: 0x000A402B
		protected void RenderButton(ToolbarButton button, SanitizedHtmlString addtionalMarkup)
		{
			this.RenderButton(button, ToolbarButtonFlags.None, null, addtionalMarkup);
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x000A5E37 File Offset: 0x000A4037
		protected void RenderButton(ToolbarButton button, ToolbarButtonFlags flags)
		{
			this.RenderButton(button, flags, null, null);
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x000A5E43 File Offset: 0x000A4043
		protected void RenderButton(ToolbarButton button, Toolbar.RenderMenuItems renderMenuItems)
		{
			this.RenderButton(button, ToolbarButtonFlags.None, renderMenuItems, null);
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x000A5E4F File Offset: 0x000A404F
		protected void RenderButton(ToolbarButton button, ToolbarButtonFlags flags, Toolbar.RenderMenuItems renderMenuItems)
		{
			this.RenderButton(button, flags, renderMenuItems, null);
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x000A5E5C File Offset: 0x000A405C
		protected void RenderButton(ToolbarButton button, ToolbarButtonFlags flags, Toolbar.RenderMenuItems renderMenuItems, SanitizedHtmlString addtionalMarkup)
		{
			if (button == null)
			{
				throw new ArgumentNullException("button");
			}
			flags |= button.Flags;
			bool flag = ToolbarButtonFlags.None != (flags & ToolbarButtonFlags.BigSize);
			bool isDisabled = ToolbarButtonFlags.None != (flags & ToolbarButtonFlags.Disabled);
			bool flag2 = ToolbarButtonFlags.None != (flags & ToolbarButtonFlags.AlwaysPressed);
			bool isPressed = flag2 || ToolbarButtonFlags.None != (flags & ToolbarButtonFlags.Pressed);
			bool isComboMenu = ToolbarButtonFlags.None != (flags & ToolbarButtonFlags.ComboMenu);
			bool flag3 = ToolbarButtonFlags.None != (flags & ToolbarButtonFlags.ComboDropDown);
			bool flag4 = ToolbarButtonFlags.None != (flags & ToolbarButtonFlags.NoAction);
			bool renderBigButtonWrapper = this.HasBigButton && (!flag3 || !flag);
			bool hasDropdown = (flags & ToolbarButtonFlags.Menu) != ToolbarButtonFlags.None || ((flags & ToolbarButtonFlags.CustomMenu) != ToolbarButtonFlags.None && ToolbarButtonFlags.None == (flags & ToolbarButtonFlags.ComboMenu));
			bool isShareButton = false;
			if (button.TextId == 2009299861 || button.TextId == -1011044205 || button.TextId == -1230529569 || button.TextId == -142048603)
			{
				isShareButton = true;
			}
			this.InternalRenderButtonStart(button, flags, renderMenuItems, flag, isDisabled, isPressed, flag3, flag4, renderBigButtonWrapper, isComboMenu, hasDropdown, isShareButton);
			if (!flag4)
			{
				this.InternalRenderLinkStart(button, flags, flag2, isPressed, flag3, renderBigButtonWrapper);
			}
			this.InternalRenderButtonBody(button, flags, addtionalMarkup, flag, flag3, hasDropdown);
			if (!flag4)
			{
				this.InternalRenderLinkEnd();
			}
			if ((flags & ToolbarButtonFlags.Radio) != ToolbarButtonFlags.None)
			{
				RenderingUtilities.RenderDropShadows(this.writer, this.sessionContext);
			}
			this.InternalRenderMenu(button, flags, renderMenuItems);
			this.InternalRenderButtonEnd(button, flags, renderMenuItems, flag, renderBigButtonWrapper, isComboMenu);
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x000A5FC8 File Offset: 0x000A41C8
		private void InternalRenderButtonStart(ToolbarButton button, ToolbarButtonFlags flags, Toolbar.RenderMenuItems renderMenuItems, bool isBig, bool isDisabled, bool isPressed, bool isComboDropDown, bool isNoAction, bool renderBigButtonWrapper, bool isComboMenu, bool hasDropdown, bool isShareButton)
		{
			if (renderBigButtonWrapper)
			{
				this.writer.Write("<div class=\"tbBigWrap ");
				this.RenderFloatCssClass();
				this.writer.Write("\"");
				if ((flags & ToolbarButtonFlags.Hidden) != ToolbarButtonFlags.None)
				{
					this.writer.Write(" style=\"display:none;\"");
				}
				this.writer.Write(">");
			}
			if (this.IsRightAligned && !isBig && isComboMenu)
			{
				this.RenderComboDropDown(button, flags, renderMenuItems);
			}
			this.writer.Write("<div id=\"divToolbarButton");
			this.writer.Write(button.Command);
			this.writer.Write("\" class=\"");
			if (isBig)
			{
				if (isComboDropDown)
				{
					this.writer.Write("tbfBigDD");
				}
				else if ((flags & ToolbarButtonFlags.ComboMenu) != ToolbarButtonFlags.None)
				{
					this.writer.Write("tbfBigCMB");
				}
				else
				{
					this.writer.Write("tbfBig");
				}
			}
			else
			{
				this.writer.Write("tbf");
				if (!this.HasBigButton)
				{
					this.writer.Write(" ");
					this.RenderFloatCssClass();
				}
			}
			if (isNoAction)
			{
				this.writer.Write(" tbfNA");
			}
			else
			{
				if (isDisabled && isPressed)
				{
					this.writer.Write(" tbfD tbfP");
				}
				else if (isDisabled)
				{
					this.writer.Write(" tbfD");
				}
				else if (isPressed)
				{
					this.writer.Write(" tbfP");
				}
				else if (isShareButton)
				{
					this.writer.Write(" tbfShareHvr");
				}
				else
				{
					this.writer.Write(" tbfHvr");
				}
				if (isComboDropDown && !isBig)
				{
					this.writer.Write(" tbfDd");
				}
			}
			if (hasDropdown && !isComboDropDown)
			{
				this.writer.Write(" tbfMnuDd");
			}
			if (isComboMenu)
			{
				this.writer.Write(" tbfCmb");
			}
			if (!this.HasBigButton && (flags & ToolbarButtonFlags.Hidden) != ToolbarButtonFlags.None)
			{
				this.writer.Write("\" style=\"display:none;");
			}
			if (((flags & ToolbarButtonFlags.Tooltip) != ToolbarButtonFlags.None || (flags & ToolbarButtonFlags.Text) == ToolbarButtonFlags.None) && button.TooltipId != -1018465893)
			{
				this.writer.Write("\" title=\"");
				this.writer.Write(SanitizedHtmlString.FromStringId(button.TooltipId));
			}
			this.writer.Write("\">");
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x000A6220 File Offset: 0x000A4420
		private void InternalRenderLinkStart(ToolbarButton button, ToolbarButtonFlags flags, bool isAlwaysPressed, bool isPressed, bool isComboDropDown, bool renderBigButtonWrapper)
		{
			this.writer.Write("<a _tbb=1 class=\"tba\" id=\"");
			this.writer.Write(button.Command);
			if (isComboDropDown)
			{
				this.writer.Write("d");
			}
			this.writer.Write("\" name=\"lnkB\"");
			if (renderBigButtonWrapper)
			{
				this.writer.Write(" _fBig=\"1\"");
			}
			if (isPressed)
			{
				this.writer.Write(" _pushed=\"1\"");
			}
			if (isAlwaysPressed)
			{
				this.writer.Write(" _alwaysPushed=\"1\"");
			}
			if ((flags & ToolbarButtonFlags.Sticky) != ToolbarButtonFlags.None || (flags & ToolbarButtonFlags.Menu) != ToolbarButtonFlags.None)
			{
				this.writer.Write(" _stky=\"1\"");
			}
			if ((flags & ToolbarButtonFlags.Radio) != ToolbarButtonFlags.None)
			{
				this.writer.Write(" _radio=\"1\"");
			}
			if (button.SwapWithButtons != null)
			{
				this.writer.Write(" _swp=\"");
				for (int i = 0; i < button.SwapWithButtons.Length; i++)
				{
					if (0 < i)
					{
						this.writer.Write(";");
					}
					this.writer.Write(button.SwapWithButtons[i].Command);
				}
				this.writer.Write("\"");
			}
			if (button.ToggleWithButtons != null)
			{
				this.writer.Write(" _tgl=\"");
				for (int j = 0; j < button.ToggleWithButtons.Length; j++)
				{
					if (0 < j)
					{
						this.writer.Write(";");
					}
					this.writer.Write(button.ToggleWithButtons[j].Command);
				}
				this.writer.Write("\"");
			}
			if ((flags & ToolbarButtonFlags.Menu) != ToolbarButtonFlags.None)
			{
				this.writer.Write(" _sbMnu=\"tbl");
				this.writer.Write(button.Command);
				this.writer.Write("Mnu\"");
			}
			if (isComboDropDown)
			{
				this.writer.Write(" _ovr=\"");
				this.writer.Write(button.Command);
				this.writer.Write("\"");
			}
			if ((flags & ToolbarButtonFlags.ComboMenu) != ToolbarButtonFlags.None)
			{
				this.writer.Write(" _twsy=\"");
				this.writer.Write(button.Command);
				this.writer.Write("d\"");
			}
			this.writer.Write(" href=\"#\">");
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x000A6460 File Offset: 0x000A4660
		private void InternalRenderButtonBody(ToolbarButton button, ToolbarButtonFlags flags, SanitizedHtmlString addtionalMarkup, bool isBig, bool isComboDropDown, bool hasDropdown)
		{
			if ((flags & ToolbarButtonFlags.HasInnerControl) != ToolbarButtonFlags.None)
			{
				button.RenderControl(this.writer);
			}
			if (!isBig)
			{
				bool flag = ToolbarButtonFlags.None != (flags & ToolbarButtonFlags.Text);
				bool flag2 = (flags & ToolbarButtonFlags.Image) != ToolbarButtonFlags.None && button.Image != ThemeFileId.None;
				bool flag3 = ToolbarButtonFlags.None != (flags & ToolbarButtonFlags.ImageAfterText);
				List<Toolbar.ButtonComponents> list = new List<Toolbar.ButtonComponents>(4);
				if (flag3)
				{
					list.Add(Toolbar.ButtonComponents.Text);
					list.Add(Toolbar.ButtonComponents.Image);
				}
				else
				{
					if (flag2)
					{
						list.Add(Toolbar.ButtonComponents.Image);
					}
					if (flag)
					{
						list.Add(Toolbar.ButtonComponents.Text);
					}
				}
				if (addtionalMarkup != null)
				{
					list.Add(Toolbar.ButtonComponents.AdditionalMarkup);
				}
				if (hasDropdown)
				{
					list.Add(Toolbar.ButtonComponents.DropDown);
				}
				for (int i = 0; i < list.Count; i++)
				{
					Toolbar.ButtonComponents buttonComponents = list[i];
					bool flag4 = i > 0;
					bool flag5 = i < list.Count - 1;
					string text = string.Empty;
					if (buttonComponents == Toolbar.ButtonComponents.Image || buttonComponents == Toolbar.ButtonComponents.Text)
					{
						if (flag4 && flag5)
						{
							text = "tbLh tbBtwn";
						}
						else if (!flag4 && flag5)
						{
							text = "tbLh tbBefore";
						}
						else if (flag4 && !flag5)
						{
							text = "tbLh tbBtwn tbAfter";
						}
						else if ((flags & ToolbarButtonFlags.Radio) != ToolbarButtonFlags.None)
						{
							text = "tbLh tbRadioBefore tbRadioAfter";
						}
						else
						{
							text = "tbLh tbBefore tbAfter";
						}
					}
					switch (buttonComponents)
					{
					case Toolbar.ButtonComponents.Image:
						this.sessionContext.RenderThemeImage(this.writer, button.Image, text, new object[]
						{
							"id=\"imgToolbarButtonIcon\""
						});
						break;
					case Toolbar.ButtonComponents.Text:
						this.writer.Write("<span class=\"");
						this.writer.Write(text);
						this.writer.Write("\">");
						this.RenderButtonText(this.writer, button.TextId);
						this.writer.Write("</span>");
						break;
					case Toolbar.ButtonComponents.AdditionalMarkup:
						this.writer.Write(addtionalMarkup);
						break;
					case Toolbar.ButtonComponents.DropDown:
						if (flag4)
						{
							this.sessionContext.RenderThemeImage(this.writer, ThemeFileId.AlertBarDropDownArrow, "tbBtwnDD", new object[]
							{
								"id=\"imgToolbarButtonDropdownIcon\""
							});
						}
						else
						{
							this.sessionContext.RenderThemeImage(this.writer, ThemeFileId.AlertBarDropDownArrow, "ddSp", new object[]
							{
								"id=\"imgToolbarButtonDropdownIcon\""
							});
						}
						break;
					}
				}
				return;
			}
			if (isComboDropDown)
			{
				this.sessionContext.RenderThemeImage(this.writer, ThemeFileId.AlertBarDropDownArrow, "tbImgBigDD", new object[]
				{
					"id=\"imgToolbarButtonDropdownIcon\""
				});
				return;
			}
			this.sessionContext.RenderThemeImage(this.writer, button.Image, "tbImgBig", new object[0]);
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x000A66F7 File Offset: 0x000A48F7
		private void InternalRenderLinkEnd()
		{
			this.writer.Write("</a>");
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x000A670C File Offset: 0x000A490C
		private void InternalRenderMenu(ToolbarButton button, ToolbarButtonFlags flags, Toolbar.RenderMenuItems renderMenuItems)
		{
			if ((flags & ToolbarButtonFlags.Menu) != ToolbarButtonFlags.None)
			{
				this.writer.Write("<div id=\"tbl");
				this.writer.Write(button.Command);
				this.writer.Write("Mnu\" class=\"subMenu\" style=\"display:none;\">");
				renderMenuItems();
				RenderingUtilities.RenderDropShadows(this.writer, this.sessionContext);
				this.writer.Write("</div>");
			}
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x000A6776 File Offset: 0x000A4976
		private void InternalRenderButtonEnd(ToolbarButton button, ToolbarButtonFlags flags, Toolbar.RenderMenuItems renderMenuItems, bool isBig, bool renderBigButtonWrapper, bool isComboMenu)
		{
			this.writer.Write("</div>");
			if ((!this.IsRightAligned || isBig) && isComboMenu)
			{
				this.RenderComboDropDown(button, flags, renderMenuItems);
			}
			if (renderBigButtonWrapper)
			{
				this.writer.Write("</div>");
			}
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x000A67B8 File Offset: 0x000A49B8
		private void RenderComboDropDown(ToolbarButton button, ToolbarButtonFlags flags, Toolbar.RenderMenuItems renderMenuItems)
		{
			ToolbarButtonFlags toolbarButtonFlags = ToolbarButtonFlags.ComboDropDown | (flags & ToolbarButtonFlags.BigSize) | (flags & ToolbarButtonFlags.CustomMenu) | (flags & ToolbarButtonFlags.Disabled) | (flags & ToolbarButtonFlags.Hidden) | (flags & ToolbarButtonFlags.AlwaysPressed);
			if ((flags & ToolbarButtonFlags.CustomMenu) == ToolbarButtonFlags.None)
			{
				toolbarButtonFlags |= ToolbarButtonFlags.Menu;
			}
			this.RenderButton(new ToolbarButton(button.Command, toolbarButtonFlags), renderMenuItems);
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001CDC RID: 7388 RVA: 0x000A6810 File Offset: 0x000A4A10
		public virtual bool HasBigButton
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x000A6813 File Offset: 0x000A4A13
		protected void RenderMenuItem(ToolbarButton button)
		{
			this.RenderMenuItem(button, false);
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x000A6820 File Offset: 0x000A4A20
		protected void RenderMenuItem(ToolbarButton button, bool isDisabled)
		{
			if (button == null)
			{
				throw new ArgumentNullException("button");
			}
			this.writer.Write("<div class=\"menuBtn ");
			this.writer.Write(isDisabled ? "miDa" : "miDf");
			this.writer.Write("\"><a class=\"menuA\" id=\"");
			this.writer.Write(button.Command);
			this.writer.Write("\"");
			if ((button.Flags & ToolbarButtonFlags.Image) != ToolbarButtonFlags.None && (button.Flags & ToolbarButtonFlags.Text) == ToolbarButtonFlags.None)
			{
				this.writer.Write(" title=\"");
				this.writer.Write(SanitizedHtmlString.FromStringId(button.TooltipId));
				this.writer.Write("\"");
			}
			this.writer.Write(" name=\"lnkB\" _mnuItm=\"1\" href=\"#\">");
			if ((button.Flags & ToolbarButtonFlags.Image) != ToolbarButtonFlags.None)
			{
				this.sessionContext.RenderThemeImage(this.writer, button.Image, "menuImgLh tbAfter", new object[0]);
			}
			if ((button.Flags & ToolbarButtonFlags.Text) != ToolbarButtonFlags.None)
			{
				this.writer.Write("<span class=\"menuTxtLh\">");
				this.RenderButtonText(this.writer, button.TextId);
				this.writer.Write("</span>");
			}
			this.writer.Write("</a>");
			this.writer.Write("</div>");
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x000A6978 File Offset: 0x000A4B78
		private void RenderFloatCssClass()
		{
			if (this.IsRightAligned)
			{
				this.writer.Write("fltAfter");
				return;
			}
			this.writer.Write("fltBefore");
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x000A69A3 File Offset: 0x000A4BA3
		private void RenderButtonText(TextWriter writer, Strings.IDs id)
		{
			this.RenderButtonText(writer, LocalizedStrings.GetNonEncoded(id));
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x000A69B2 File Offset: 0x000A4BB2
		private void RenderButtonText(TextWriter writer, string text)
		{
			Utilities.SanitizeHtmlEncode(text, writer);
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x000A69BC File Offset: 0x000A4BBC
		protected void RenderCustomMenuItem(string command, string text, string type, string iconUrl)
		{
			this.writer.Write("<div class=\"menuBtn miDf\"><a class=\"menuA\" id=\"");
			this.writer.Write(command);
			this.writer.Write("\" name=\"lnkB\" _mnuItm=\"1\" _t=\"");
			this.writer.Write(type);
			this.writer.Write("\" href=\"#\"><img class=\"menuImgLh tbAfter\" src=\"");
			this.writer.Write(iconUrl);
			this.writer.Write("\">");
			this.writer.Write("<span class=\"menuTxtLh\">");
			this.RenderButtonText(this.writer, text);
			this.writer.Write("</span></a></div>");
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x000A6A5B File Offset: 0x000A4C5B
		protected void RenderMenuItemDivider()
		{
			this.writer.Write("<div class=\"menuBtnHl\">&nbsp;</div>");
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x000A6A70 File Offset: 0x000A4C70
		protected void RenderCustomNewMenuItems()
		{
			bool flag = true;
			using (List<UIExtensionManager.NewMenuExtensionItem>.Enumerator menuItemEnumerator = UIExtensionManager.GetMenuItemEnumerator())
			{
				while (menuItemEnumerator.MoveNext())
				{
					UIExtensionManager.NewMenuExtensionItem newMenuExtensionItem = menuItemEnumerator.Current;
					if (flag)
					{
						this.RenderMenuItemDivider();
						flag = false;
					}
					this.RenderCustomMenuItem("nwCstm", newMenuExtensionItem.GetTextByLanguage(this.sessionContext.UserCulture.Name), newMenuExtensionItem.CustomType, newMenuExtensionItem.Icon);
				}
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001CE5 RID: 7397 RVA: 0x000A6AF0 File Offset: 0x000A4CF0
		protected bool ShouldUseTwistyForReplyButton
		{
			get
			{
				return this.sessionContext.IsSmsEnabled || this.sessionContext.IsInstantMessageEnabled() || this.sessionContext.IsReplyByPhoneEnabled;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06001CE6 RID: 7398 RVA: 0x000A6B19 File Offset: 0x000A4D19
		public virtual bool IsRightAligned
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x000A6B1C File Offset: 0x000A4D1C
		protected void RenderReplyMenuItems()
		{
			this.RenderMenuItem(ToolbarButtons.ReplyInDropDown);
			if (this.sessionContext.IsInstantMessageEnabled())
			{
				this.RenderMenuItem(ToolbarButtons.ReplyByChat);
			}
			if (this.sessionContext.IsReplyByPhoneEnabled)
			{
				this.RenderMenuItem(ToolbarButtons.ReplyByPhone);
			}
			if (this.sessionContext.IsSmsEnabled)
			{
				this.RenderMenuItem(ToolbarButtons.ReplyBySms);
			}
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x000A6B7C File Offset: 0x000A4D7C
		protected void RenderForwardMenuItems()
		{
			this.RenderMenuItem(ToolbarButtons.ForwardInDropDown);
			this.RenderMenuItem(ToolbarButtons.ForwardAsAttachmentInDropDown);
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x000A6B94 File Offset: 0x000A4D94
		protected void RenderSharingMenuItems()
		{
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x000A6B98 File Offset: 0x000A4D98
		protected void RenderDivider(string id, bool displayed)
		{
			this.Writer.Write("<div class=\"");
			if (this.IsNarrow)
			{
				this.Writer.Write("tbSpN ");
			}
			else
			{
				this.Writer.Write("tbSp ");
			}
			this.RenderFloatCssClass();
			this.Writer.Write("\"");
			if (id != null)
			{
				this.Writer.Write(" id=\"");
				this.Writer.Write(id);
				this.Writer.Write("\"");
			}
			if (!displayed)
			{
				this.Writer.Write(" style=\"display:none\"");
			}
			this.writer.Write("></div>");
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x000A6C47 File Offset: 0x000A4E47
		protected void RenderFloatedSpacer(ushort width)
		{
			this.RenderFloatedSpacer(width, null);
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x000A6C54 File Offset: 0x000A4E54
		protected void RenderFloatedSpacer(ushort width, string id)
		{
			this.writer.Write("<div");
			if (!string.IsNullOrEmpty(id))
			{
				this.writer.Write(" id=\"");
				this.writer.Write(id);
				this.writer.Write("\"");
			}
			this.writer.Write(" class=\"");
			this.RenderFloatCssClass();
			this.writer.Write("\" style=\"width:");
			this.writer.Write((int)width);
			this.writer.Write("px\">&nbsp;</div>");
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x000A6CE7 File Offset: 0x000A4EE7
		protected void RenderDivider()
		{
			this.RenderDivider(null, true);
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x000A6CF4 File Offset: 0x000A4EF4
		protected void RenderHelpButton(string helpFile, string helpAnchor, bool shouldRenderHelpText)
		{
			if (helpFile == null)
			{
				throw new ArgumentNullException("helpFile");
			}
			if (helpAnchor == null)
			{
				throw new ArgumentNullException("helpAnchor");
			}
			this.writer.Write("<div class=\"tbf ");
			if (this.IsRightAligned)
			{
				this.writer.Write("fltBefore");
			}
			else
			{
				this.writer.Write("fltAfter");
			}
			this.writer.Write(" tbfHvr\"><a _tbb=1 id=\"help\" class=\"tba\" name=\"lnkB\" href=\"");
			this.writer.Write(Utilities.SanitizeHtmlEncode(Utilities.BuildEhcHref(helpFile)));
			this.writer.Write("\">");
			this.sessionContext.RenderThemeImage(this.writer, ThemeFileId.Help, "tbLh tbBefore tbAfter", new object[]
			{
				SanitizedHtmlString.Format("{0}{1}{2}", new object[]
				{
					"title=\"",
					LocalizedStrings.GetNonEncoded(1454393937),
					"\""
				})
			});
			this.writer.Write("<span id=\"spnhlpLnk\" style=\"display:none\">./help/default.htm?");
			this.writer.Write(helpFile);
			if (helpAnchor.Length != 0)
			{
				this.writer.Write("#");
				this.writer.Write(helpAnchor);
			}
			else
			{
				this.writer.Write(string.Empty);
			}
			this.writer.Write("</span></a>");
			this.writer.Write("</div>");
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x000A6E51 File Offset: 0x000A5051
		protected void RenderHelpButton(string helpFile, string helpAnchor)
		{
			this.RenderHelpButton(helpFile, helpAnchor, false);
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x000A6E5C File Offset: 0x000A505C
		protected void RenderHtmlTextToggle()
		{
			this.RenderHtmlTextToggle("0");
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x000A6E69 File Offset: 0x000A5069
		protected void RenderHtmlTextToggle(string currentSelection)
		{
			this.RenderHtmlTextToggle(currentSelection, false);
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x000A6E74 File Offset: 0x000A5074
		protected void RenderHtmlTextToggle(string currentSelection, bool disabled)
		{
			this.writer.Write("<div class=\"");
			this.RenderFloatCssClass();
			this.writer.Write(" tbfNoH\">");
			new DropDownList("divCmbFrmt", currentSelection, Toolbar.bodyFormatList)
			{
				AdditionalListStyles = "frmtList"
			}.Render(this.writer, disabled);
			this.writer.Write("</div>");
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x000A6EE0 File Offset: 0x000A50E0
		protected void RenderInstantMessageButtons()
		{
			this.RenderButton(ToolbarButtons.Chat, ToolbarButtonFlags.Disabled);
			this.RenderButton(ToolbarButtons.AddToBuddyListWithText, ToolbarButtonFlags.Hidden);
			this.RenderButton(ToolbarButtons.RemoveFromBuddyListWithText, ToolbarButtonFlags.Hidden);
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x000A6F14 File Offset: 0x000A5114
		protected void RenderSpellCheckLanguageDialog()
		{
			UserContext userContext = this.sessionContext as UserContext;
			if (userContext == null)
			{
				return;
			}
			if (!userContext.IsFeatureEnabled(Feature.SpellChecker))
			{
				return;
			}
			string value = userContext.IsRtl ? "left" : "right";
			this.Writer.Write("<div id=\"divOpnSplDlg\" onmouseup=\"onMUSpl()\"><div class=\"titlebox\">");
			this.Writer.Write(SanitizedHtmlString.FromStringId(-1676730620));
			this.Writer.Write("</div><div class=\"mainbox\"><div class=\"dlgBkgrd\"><div>");
			this.Writer.Write(SanitizedHtmlString.FromStringId(-1116120248));
			this.Writer.Write("</div><div class=\"listbox\">");
			this.Writer.Write("</div></div><div id=\"divSplBtn\" style=\"text-align:");
			this.Writer.Write(value);
			this.Writer.Write("\"><button id=\"btnSpOk\"");
			Utilities.RenderScriptHandler(this.Writer, "onclick", "clkSpOK();");
			this.Writer.Write(" ");
			Utilities.RenderScriptHandler(this.Writer, "onmouseover", "btnOnMsOvrBtn(_this);");
			this.Writer.Write(" ");
			Utilities.RenderScriptHandler(this.Writer, "onmouseout", "btnOnMsOutBtn(_this);");
			this.Writer.Write(">");
			this.Writer.Write(SanitizedHtmlString.FromStringId(2041362128));
			this.Writer.Write("</button>&nbsp;");
			this.Writer.Write("<button id=\"btnSpCn\" ");
			Utilities.RenderScriptHandler(this.Writer, "onclick", "clkSpCn();");
			this.Writer.Write(" ");
			Utilities.RenderScriptHandler(this.Writer, "onmouseover", "btnOnMsOvrBtn(_this);");
			this.Writer.Write(" ");
			Utilities.RenderScriptHandler(this.Writer, "onmouseout", "btnOnMsOutBtn(_this);");
			this.Writer.Write(">");
			this.Writer.Write(SanitizedHtmlString.FromStringId(-1936577052));
			this.Writer.Write("</button></div></div></div>");
		}

		// Token: 0x0400154E RID: 5454
		protected const ushort SpaceWidthBetweenButtons = 3;

		// Token: 0x0400154F RID: 5455
		protected const string MeasureElementId = "divMeasure";

		// Token: 0x04001550 RID: 5456
		private const string NarrowButtonClass = "nrw";

		// Token: 0x04001551 RID: 5457
		private const string LeftNarrowButtonClass = "nrwl";

		// Token: 0x04001552 RID: 5458
		private const string RightNarrowButtonClass = "nrwr";

		// Token: 0x04001553 RID: 5459
		private TextWriter writer;

		// Token: 0x04001554 RID: 5460
		private string id = "divTB";

		// Token: 0x04001555 RID: 5461
		private ToolbarType toolbarType = ToolbarType.View;

		// Token: 0x04001556 RID: 5462
		protected ISessionContext sessionContext;

		// Token: 0x04001557 RID: 5463
		private static readonly DropDownListItem[] bodyFormatList = new DropDownListItem[]
		{
			new DropDownListItem("0", 439663569),
			new DropDownListItem("1", 460918983)
		};

		// Token: 0x020002FC RID: 764
		private enum ButtonComponents : uint
		{
			// Token: 0x04001559 RID: 5465
			None,
			// Token: 0x0400155A RID: 5466
			Image,
			// Token: 0x0400155B RID: 5467
			Text,
			// Token: 0x0400155C RID: 5468
			AdditionalMarkup,
			// Token: 0x0400155D RID: 5469
			DropDown
		}

		// Token: 0x020002FD RID: 765
		// (Invoke) Token: 0x06001CF7 RID: 7415
		protected delegate void RenderMenuItems();
	}
}
