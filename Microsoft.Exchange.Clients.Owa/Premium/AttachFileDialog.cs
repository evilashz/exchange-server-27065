using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000439 RID: 1081
	public class AttachFileDialog : OwaPage, IRegistryOnlyForm
	{
		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06002703 RID: 9987 RVA: 0x000DEA33 File Offset: 0x000DCC33
		protected AttachmentAddResult AttachResult
		{
			get
			{
				return this.attachResult;
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06002704 RID: 9988 RVA: 0x000DEA3B File Offset: 0x000DCC3B
		public ArrayList AttachmentWellRenderObjects
		{
			get
			{
				return this.attachmentWellRenderObjects;
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06002705 RID: 9989 RVA: 0x000DEA43 File Offset: 0x000DCC43
		protected bool IsInline
		{
			get
			{
				return this.isInline;
			}
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x000DEA4C File Offset: 0x000DCC4C
		protected override void OnLoad(EventArgs e)
		{
			string action = base.OwaContext.FormsRegistryContext.Action;
			this.isInline = !string.IsNullOrEmpty(action);
			if (base.Request.HttpMethod == "POST")
			{
				this.ProcessPost();
			}
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x000DEA96 File Offset: 0x000DCC96
		protected override void OnUnload(EventArgs e)
		{
			if (this.item != null)
			{
				this.item.Dispose();
				this.item = null;
			}
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x000DEAB4 File Offset: 0x000DCCB4
		protected override void OnError(EventArgs e)
		{
			Exception lastError = base.Server.GetLastError();
			if (lastError is HttpException)
			{
				base.Server.ClearError();
				Utilities.TransferToErrorPage(base.OwaContext, LocalizedStrings.GetNonEncoded(-1440270008));
				return;
			}
			base.OnError(e);
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x000DEB00 File Offset: 0x000DCD00
		private void ProcessPost()
		{
			string text = base.Request.Form["mId"];
			string text2 = base.Request.Form["mCK"];
			string bodyMarkup = base.Request.Form["sHtmlBdy"];
			if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
			{
				this.item = Utilities.GetItem<Item>(base.UserContext, text, text2, new PropertyDefinition[0]);
				if (base.UserContext.IsIrmEnabled)
				{
					Utilities.IrmDecryptIfRestricted(this.item, base.UserContext, true);
				}
			}
			else
			{
				StoreObjectType itemType = StoreObjectType.Message;
				string text3 = base.Request.Form["iT"];
				int num;
				if (text3 != null && int.TryParse(text3, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
				{
					itemType = (StoreObjectType)num;
				}
				OwaStoreObjectId destinationFolderId = null;
				string text4 = base.Request.Form["sFldId"];
				if (!string.IsNullOrEmpty(text4))
				{
					destinationFolderId = OwaStoreObjectId.CreateFromString(text4);
				}
				this.item = Utilities.CreateImplicitDraftItem(itemType, destinationFolderId);
				this.item.Save(SaveMode.ResolveConflicts);
				this.item.Load();
				if (Globals.ArePerfCountersEnabled)
				{
					OwaSingleCounters.ItemsCreated.Increment();
				}
			}
			this.attachmentLinks = new List<SanitizedHtmlString>(base.Request.Files.Count);
			if (!this.IsInline)
			{
				this.attachResult = AttachmentUtility.AddAttachment(this.item, base.Request.Files, base.UserContext, false, bodyMarkup);
			}
			else
			{
				this.attachResult = AttachmentUtility.AddAttachment(this.item, base.Request.Files, base.UserContext, true, bodyMarkup, out this.attachmentLinks);
			}
			this.item.Load();
			if (Globals.ArePerfCountersEnabled)
			{
				OwaSingleCounters.ItemsUpdated.Increment();
			}
			if (!this.IsInline)
			{
				this.attachmentWellRenderObjects = null;
				bool isPublicLogon = base.UserContext.IsPublicLogon;
				this.attachmentWellRenderObjects = AttachmentWell.GetAttachmentInformation(this.item, null, isPublicLogon);
			}
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x000DECF6 File Offset: 0x000DCEF6
		protected void RenderFormAction()
		{
			base.SanitizingResponse.Write("?ae=Dialog&t=AttachFileDialog");
			if (this.IsInline)
			{
				base.SanitizingResponse.Write("&a=InsertImage");
			}
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x000DED20 File Offset: 0x000DCF20
		protected void RenderInlineImageLinks()
		{
			base.SanitizingResponse.Write("new Array(");
			for (int i = 0; i < this.attachmentLinks.Count; i++)
			{
				if (i != 0)
				{
					base.SanitizingResponse.Write(",");
				}
				base.SanitizingResponse.Write("'");
				base.SanitizingResponse.Write(this.attachmentLinks[i]);
				base.SanitizingResponse.Write("'");
			}
			base.SanitizingResponse.Write(");");
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x000DEDB0 File Offset: 0x000DCFB0
		protected void RenderImageAttachmentInfobar()
		{
			if (this.AttachResult.ResultCode != AttachmentAddResultCode.NoError)
			{
				new StringBuilder();
				new SanitizedHtmlString(string.Empty);
				Infobar infobar = new Infobar();
				infobar.AddMessage(this.AttachResult.Message, InfobarMessageType.Warning, AttachmentWell.AttachmentInfobarErrorHtmlTag);
				infobar.Render(base.SanitizingResponse);
			}
		}

		// Token: 0x0600270D RID: 9997 RVA: 0x000DEE08 File Offset: 0x000DD008
		protected void RenderSizeAttribute()
		{
			BrowserPlatform browserPlatform = Utilities.GetBrowserPlatform(base.Request.UserAgent);
			BrowserType browserType = Utilities.GetBrowserType(base.Request.UserAgent);
			string empty = string.Empty;
			string name = Microsoft.Exchange.Clients.Owa.Core.Culture.GetUserCulture().Name;
			bool flag = name == "ja-JP" || name == "ko-KR" || name == "zh-CN" || name == "zh-TW" || name == "zh-HK" || name == "zh-MO" || name == "zh-SG";
			if (BrowserType.Firefox == browserType)
			{
				if (BrowserPlatform.Macintosh == browserPlatform)
				{
					if (flag)
					{
						base.Response.Write(" size=\"43\"");
						return;
					}
					base.Response.Write(" size=\"50\"");
					return;
				}
				else if (browserPlatform == BrowserPlatform.Windows)
				{
					if (flag)
					{
						base.Response.Write(" size=\"56\"");
						return;
					}
					base.Response.Write(" size=\"50\"");
				}
			}
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x000DEEF9 File Offset: 0x000DD0F9
		protected void RenderJavascriptEncodedItemId()
		{
			if (this.item != null)
			{
				Utilities.JavascriptEncode(Utilities.GetIdAsString(this.item), base.Response.Output);
			}
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x000DEF1E File Offset: 0x000DD11E
		protected void RenderJavascriptEncodedItemChangeKey()
		{
			if (this.item != null)
			{
				Utilities.JavascriptEncode(this.item.Id.ChangeKeyAsBase64String(), base.Response.Output);
			}
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x000DEF48 File Offset: 0x000DD148
		protected void RenderUpgradeToSilverlight()
		{
			SanitizedHtmlString sanitizedHtmlString = SanitizedHtmlString.Format("<a href=\"http://www.microsoft.com/silverlight/get-started/install/default.aspx\" target=_blank title=\"{0}\" class=\"updSL\">{1}</a>", new object[]
			{
				LocalizedStrings.GetNonEncoded(1095922679),
				LocalizedStrings.GetNonEncoded(1095922679)
			});
			base.SanitizingResponse.Write(SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(391987695), new object[]
			{
				sanitizedHtmlString
			}));
		}

		// Token: 0x06002711 RID: 10001 RVA: 0x000DEFA8 File Offset: 0x000DD1A8
		protected void RenderFileUploadButtonText()
		{
			Strings.IDs localizedId;
			Strings.IDs localizedId2;
			if (this.IsInline)
			{
				localizedId = 1319799963;
				localizedId2 = 695427503;
			}
			else
			{
				localizedId = -1952546783;
				localizedId2 = -547159221;
			}
			Strings.IDs localizedId3;
			if (base.UserContext.BrowserType == BrowserType.IE || base.UserContext.BrowserType == BrowserType.Firefox)
			{
				localizedId3 = 1698608150;
			}
			else
			{
				localizedId3 = 1368786137;
			}
			base.SanitizingResponse.Write(SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(1264365152), new object[]
			{
				LocalizedStrings.GetNonEncoded(localizedId),
				LocalizedStrings.GetNonEncoded(localizedId3),
				LocalizedStrings.GetNonEncoded(localizedId2)
			}));
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x000DF03E File Offset: 0x000DD23E
		protected void RenderDialogTitle()
		{
			if (this.IsInline)
			{
				base.SanitizingResponse.Write(SanitizedHtmlString.FromStringId(-553630704));
				return;
			}
			base.SanitizingResponse.Write(SanitizedHtmlString.FromStringId(-1551177844));
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x000DF074 File Offset: 0x000DD274
		protected string GetButtonText()
		{
			Strings.IDs localizedID;
			if (this.IsInline)
			{
				localizedID = 695427503;
			}
			else
			{
				localizedID = -547159221;
			}
			return LocalizedStrings.GetHtmlEncoded(localizedID);
		}

		// Token: 0x04001B64 RID: 7012
		private const int CopyBufferSize = 32768;

		// Token: 0x04001B65 RID: 7013
		private ArrayList attachmentWellRenderObjects;

		// Token: 0x04001B66 RID: 7014
		private Item item;

		// Token: 0x04001B67 RID: 7015
		private AttachmentAddResult attachResult = AttachmentAddResult.NoError;

		// Token: 0x04001B68 RID: 7016
		private List<SanitizedHtmlString> attachmentLinks;

		// Token: 0x04001B69 RID: 7017
		private bool isInline;
	}
}
