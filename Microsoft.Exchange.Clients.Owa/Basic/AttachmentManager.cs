using System;
using System.Collections;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa.Basic.Controls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x02000090 RID: 144
	public class AttachmentManager : OwaForm
	{
		// Token: 0x0600042F RID: 1071 RVA: 0x00023760 File Offset: 0x00021960
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (base.OwaContext.FormsRegistryContext.Action != null)
			{
				this.action = base.OwaContext.FormsRegistryContext.Action;
			}
			if (string.IsNullOrEmpty(this.action))
			{
				this.messageIdString = Utilities.GetQueryStringParameter(base.Request, "id", true);
				this.changeKeyString = null;
			}
			else
			{
				this.messageIdString = Utilities.GetFormParameter(base.Request, "hidid", true);
				this.changeKeyString = Utilities.GetFormParameter(base.Request, "hidchk", true);
			}
			this.GetItem();
			string a;
			if ((a = this.action) != null)
			{
				if (!(a == "Add"))
				{
					if (a == "Del")
					{
						if (Utilities.GetFormParameter(base.Request, "dLst", false) != null)
						{
							this.RemoveAttachments();
						}
					}
				}
				else
				{
					this.AddAttachments();
				}
			}
			this.attachmentList = AttachmentWell.GetAttachmentInformation(base.Item, base.AttachmentLinks, base.UserContext.IsPublicLogon);
			if (base.Item is Contact)
			{
				OwaForm.RemoveContactPhoto(this.attachmentList);
			}
			CalendarItemBaseData userContextData = EditCalendarItemHelper.GetUserContextData(base.UserContext);
			if (userContextData != null && userContextData.Id != null && !string.IsNullOrEmpty("hidchk") && userContextData.Id.Equals(base.Item.Id.ObjectId) && userContextData.ChangeKey != this.changeKeyString)
			{
				userContextData.ChangeKey = this.changeKeyString;
			}
			this.levelOneFound = AttachmentUtility.IsLevelOneAndBlock(this.attachmentList);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x000238F0 File Offset: 0x00021AF0
		private void GetItem()
		{
			if (this.changeKeyString != null && this.changeKeyString.Length > 0)
			{
				base.Item = Utilities.GetItem<Item>(base.UserContext, this.messageIdString, this.changeKeyString, new PropertyDefinition[0]);
				return;
			}
			base.Item = Utilities.GetItem<Item>(base.UserContext, this.messageIdString, new PropertyDefinition[0]);
			this.changeKeyString = base.Item.Id.ChangeKeyAsBase64String();
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0002396C File Offset: 0x00021B6C
		public void RenderAttachmentList()
		{
			AttachmentWellInfo attachmentWellInfo = null;
			ArrayList previousAttachmentDisplayNames = new ArrayList();
			base.SanitizingResponse.Write("<form name=\"delFrm\" action=\"?ae=Dialog&t=Attach&a=Del\" method=\"POST\">");
			base.SanitizingResponse.Write("<input type=\"hidden\" name=\"{0}\" value=\"{1}\">", "hidid", this.messageIdString);
			base.SanitizingResponse.Write("<input type=\"hidden\" name=\"{0}\" value=\"{1}\">", "hid64bitmsgid", Utilities.UrlEncode(this.messageIdString));
			base.SanitizingResponse.Write("<input type=\"hidden\" name=\"{0}\" value=\"{1}\">", "hidchk", this.changeKeyString);
			base.SanitizingResponse.Write("<div id=\"{0}\">", "lstArId");
			base.SanitizingResponse.Write("<table cellpadding=1 cellspacing=0 border=\"0\" class=\"attchList\"><caption>");
			base.SanitizingResponse.Write(LocalizedStrings.GetNonEncoded(573876176));
			base.SanitizingResponse.Write("</caption><tr>");
			base.SanitizingResponse.Write("<th class=\"lftcrnr\"><img src=\"");
			base.SanitizingResponse.Write(base.UserContext.GetThemeFileUrl(ThemeFileId.Clear));
			base.SanitizingResponse.Write("\" alt=\"\" class=\"invimg\"></th>");
			base.SanitizingResponse.Write("<th class=\"chkbx\">");
			base.SanitizingResponse.Write("<input type=\"checkbox\" name=\"chkAll\" id=\"chkAll\" onclick=\"chkAttchAll();\" alt=\"");
			base.SanitizingResponse.Write(LocalizedStrings.GetNonEncoded(-288583276));
			base.SanitizingResponse.Write("\"></th>");
			base.SanitizingResponse.Write("<th>");
			base.SanitizingResponse.Write(LocalizedStrings.GetNonEncoded(796893232));
			base.SanitizingResponse.Write("</th>");
			base.SanitizingResponse.Write("<th align=\"right\" class=\"sze\">");
			base.SanitizingResponse.Write(LocalizedStrings.GetNonEncoded(-837446919));
			base.SanitizingResponse.Write("</th>");
			base.SanitizingResponse.Write("<th class=\"rtcrnr\"><img src=\"");
			base.SanitizingResponse.Write(base.UserContext.GetThemeFileUrl(ThemeFileId.Clear));
			base.SanitizingResponse.Write("\" alt=\"\" class=\"invimg\"></th>");
			base.SanitizingResponse.Write("</tr>");
			if (this.attachmentList.Count > 0)
			{
				string value = null;
				for (int i = 0; i < this.attachmentList.Count; i++)
				{
					string arg;
					if (i == 0)
					{
						arg = "class=\"frst\"";
						value = "frst ";
					}
					else
					{
						arg = null;
						value = null;
					}
					attachmentWellInfo = (this.attachmentList[i] as AttachmentWellInfo);
					base.SanitizingResponse.Write("<tr>");
					base.SanitizingResponse.Write("<td {0} style=\"padding:0 0 0 0;\"><img src=\"{1}\" alt=\"\" class=\"invimg\"></td>", arg, base.UserContext.GetThemeFileUrl(ThemeFileId.Clear));
					base.SanitizingResponse.Write("<td {0}><input type=checkbox name=\"{1}\" id=\"{1}\" value=\"{2}\" onclick=\"onClkChkBx(this);\"></td>", arg, "dLst", attachmentWellInfo.AttachmentId.ToBase64String());
					base.SanitizingResponse.Write("<td {0}>", arg);
					bool flag = false;
					if (attachmentWellInfo.AttachmentType == AttachmentType.EmbeddedMessage)
					{
						using (Attachment attachment = attachmentWellInfo.OpenAttachment())
						{
							ItemAttachment itemAttachment = attachment as ItemAttachment;
							if (itemAttachment != null)
							{
								using (Item item = itemAttachment.GetItem())
								{
									flag = true;
									AttachmentWell.RenderAttachmentLinkForItem(base.SanitizingResponse, attachmentWellInfo, item, this.messageIdString, base.UserContext, previousAttachmentDisplayNames, AttachmentWell.AttachmentWellFlags.None, false);
								}
							}
						}
					}
					if (!flag)
					{
						AttachmentWell.RenderAttachmentLink(base.SanitizingResponse, AttachmentWellType.ReadWrite, attachmentWellInfo, this.messageIdString, base.UserContext, previousAttachmentDisplayNames, AttachmentWell.AttachmentWellFlags.None, false);
					}
					base.SanitizingResponse.Write("</td>");
					base.SanitizingResponse.Write("<td colspan=2 class=\"");
					base.SanitizingResponse.Write(value);
					base.SanitizingResponse.Write("sze\">");
					double num = 0.0;
					if (attachmentWellInfo.Size > 0L)
					{
						num = (double)attachmentWellInfo.Size / 1024.0 / 1024.0;
						num = Math.Round(num, 2);
						this.attachUsage += num;
					}
					if (num == 0.0)
					{
						base.SanitizingResponse.Write(" < 0.01");
					}
					else
					{
						base.SanitizingResponse.Write(num);
					}
					base.SanitizingResponse.Write(" MB</td>");
					base.SanitizingResponse.Write("</tr>");
				}
			}
			else
			{
				base.SanitizingResponse.Write("<tr>");
				base.SanitizingResponse.Write("<td colspan=5 class=\"noattach\" nowrap>");
				base.SanitizingResponse.Write(LocalizedStrings.GetNonEncoded(-1907299050));
				base.SanitizingResponse.Write("</td>");
				base.SanitizingResponse.Write("</tr>");
			}
			base.SanitizingResponse.Write("</table>");
			base.SanitizingResponse.Write("</div>");
			Utilities.RenderCanaryHidden(base.SanitizingResponse, base.UserContext);
			base.SanitizingResponse.Write("</form>");
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00023E34 File Offset: 0x00022034
		private void AddAttachments()
		{
			InfobarMessageType type = InfobarMessageType.Error;
			AttachmentAddResult attachmentAddResult = AttachmentUtility.AddAttachment(base.Item, base.Request.Files, base.UserContext);
			if (attachmentAddResult.ResultCode != AttachmentAddResultCode.NoError)
			{
				base.Infobar.AddMessageHtml(attachmentAddResult.Message, type);
			}
			base.Item.Load();
			if (attachmentAddResult.ResultCode != AttachmentAddResultCode.IrresolvableConflictWhenSaving)
			{
				this.changeKeyString = base.Item.Id.ChangeKeyAsBase64String();
			}
			if (Globals.ArePerfCountersEnabled)
			{
				OwaSingleCounters.ItemsUpdated.Increment();
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00023EB8 File Offset: 0x000220B8
		private void RemoveAttachments()
		{
			char[] separator = ",".ToCharArray();
			ArrayList arrayList = new ArrayList();
			string[] array = base.Request.Form["dLst"].Split(separator);
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				AttachmentId attachmentId = AttachmentId.Deserialize(array[i]);
				if (base.Item.AttachmentCollection.Contains(attachmentId))
				{
					arrayList.Add(attachmentId);
				}
			}
			if (arrayList.Count > 0)
			{
				AttachmentUtility.RemoveAttachment(base.Item, arrayList);
				base.Item.Save(SaveMode.ResolveConflicts);
				if (Globals.ArePerfCountersEnabled)
				{
					OwaSingleCounters.ItemsUpdated.Increment();
				}
			}
			base.Item.Load();
			this.changeKeyString = base.Item.Id.ChangeKeyAsBase64String();
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00023F8C File Offset: 0x0002218C
		protected void RenderNavigation()
		{
			NavigationModule navigationModule = (base.Item.ClassName == "IPM.Contact") ? NavigationModule.Contacts : NavigationModule.Mail;
			Navigation navigation = new Navigation(navigationModule, base.OwaContext, base.Response.Output);
			navigation.Render();
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00023FD4 File Offset: 0x000221D4
		protected void RenderAttachmentForm()
		{
			base.SanitizingResponse.Write("<div><form name=\"addFrm\" action=\"?ae=Dialog&t=Attach&a=Add\" enctype=\"multipart/form-data\" method=\"POST\">");
			base.SanitizingResponse.Write("<table cellpadding=4 class=\"attchfrm\"><caption>");
			base.SanitizingResponse.Write(LocalizedStrings.GetNonEncoded(-1286941817));
			base.SanitizingResponse.Write("</caption><tr><td class=\"txt\">");
			base.SanitizingResponse.Write("<input type=\"hidden\" name=\"{0}\" value=\"{1}\">", "hidid", this.messageIdString);
			base.SanitizingResponse.Write("<input type=\"hidden\" name=\"{0}\" value=\"{1}\">", "hidchk", this.changeKeyString);
			base.SanitizingResponse.Write(LocalizedStrings.GetNonEncoded(935002604), "<b>", "</b>");
			base.SanitizingResponse.Write("</td></tr>");
			base.SanitizingResponse.Write("<tr><td align=\"right\">");
			base.SanitizingResponse.Write("<input type=\"file\" size=\"35\" name=\"attach\" id=\"attach\" alt=\"Attachment\">");
			base.SanitizingResponse.Write("</td></tr>");
			base.SanitizingResponse.Write("<tr><td align=\"right\">");
			base.SanitizingResponse.Write("<input class=\"btn\" type=\"button\" name=\"attachbtn\" id=\"attachbtn\" alt=\"");
			base.SanitizingResponse.Write(LocalizedStrings.GetNonEncoded(-60366385));
			base.SanitizingResponse.Write("\" value=\"");
			base.SanitizingResponse.Write(LocalizedStrings.GetNonEncoded(-547159221));
			base.SanitizingResponse.Write("\" onclick=\"durUp('");
			base.SanitizingResponse.Write("lstArId");
			base.SanitizingResponse.Write("','remove');\">");
			base.SanitizingResponse.Write("</td></tr>");
			base.SanitizingResponse.Write("</table>");
			Utilities.RenderCanaryHidden(base.SanitizingResponse, base.UserContext);
			base.SanitizingResponse.Write("</form></div>");
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00024188 File Offset: 0x00022388
		protected void RenderAttachmentToolbar()
		{
			Toolbar toolbar = new Toolbar(base.Response.Output, true);
			toolbar.RenderStart();
			toolbar.RenderButton(ToolbarButtons.Done);
			toolbar.RenderDivider();
			toolbar.RenderButton(ToolbarButtons.Remove, ToolbarButtonFlags.Sticky);
			toolbar.RenderFill();
			toolbar.RenderButton(ToolbarButtons.CloseImage);
			toolbar.RenderEnd();
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x000241E2 File Offset: 0x000223E2
		protected void RenderInfoBar()
		{
			if (this.levelOneFound)
			{
				base.Infobar.AddMessageLocalized(-2118248931, InfobarMessageType.Informational);
			}
			base.Infobar.Render(base.SanitizingResponse);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00024210 File Offset: 0x00022410
		protected void RenderAttachmentListFooter()
		{
			Toolbar toolbar = new Toolbar(base.SanitizingResponse, false);
			toolbar.RenderStart();
			toolbar.RenderFill();
			toolbar.RenderEnd();
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0002423C File Offset: 0x0002243C
		protected override bool ShowInfobar
		{
			get
			{
				return base.ShowInfobar || this.levelOneFound;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0002424E File Offset: 0x0002244E
		protected string UrlEncodedMessageId
		{
			get
			{
				return HttpUtility.UrlEncode(this.messageIdString);
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x0002425B File Offset: 0x0002245B
		protected string MessageIdString
		{
			get
			{
				return this.messageIdString;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x00024263 File Offset: 0x00022463
		protected string MessageIdString64bitEconded
		{
			get
			{
				return Utilities.UrlEncode(this.messageIdString);
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x00024270 File Offset: 0x00022470
		protected static string PostMessageId
		{
			get
			{
				return "hidid";
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x00024277 File Offset: 0x00022477
		protected override string ItemType
		{
			get
			{
				return base.Item.ClassName;
			}
		}

		// Token: 0x0400037A RID: 890
		private const string DeleteList = "dLst";

		// Token: 0x0400037B RID: 891
		private const string Add = "Add";

		// Token: 0x0400037C RID: 892
		private const string Del = "Del";

		// Token: 0x0400037D RID: 893
		private const string ChangeKey = "hidchk";

		// Token: 0x0400037E RID: 894
		private const string ListAreaId = "lstArId";

		// Token: 0x0400037F RID: 895
		private const string PostMessageIdString = "hidid";

		// Token: 0x04000380 RID: 896
		private const string Hid64bitMessageId = "hid64bitmsgid";

		// Token: 0x04000381 RID: 897
		private const int Limit = 20;

		// Token: 0x04000382 RID: 898
		private string messageIdString;

		// Token: 0x04000383 RID: 899
		private string changeKeyString;

		// Token: 0x04000384 RID: 900
		private double attachUsage;

		// Token: 0x04000385 RID: 901
		private ArrayList attachmentList;

		// Token: 0x04000386 RID: 902
		private bool levelOneFound;

		// Token: 0x04000387 RID: 903
		private string action;
	}
}
