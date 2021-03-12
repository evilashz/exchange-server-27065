using System;
using System.Collections;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000462 RID: 1122
	public class PrintMeetingPage : MeetingPage
	{
		// Token: 0x06002A0A RID: 10762 RVA: 0x000EBD58 File Offset: 0x000E9F58
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			BusyType busyType = BusyType.Tentative;
			if (base.Item != null)
			{
				object obj = base.Item.TryGetProperty(CalendarItemBaseSchema.FreeBusyStatus);
				if (obj is int)
				{
					busyType = (BusyType)obj;
				}
			}
			switch (busyType)
			{
			case BusyType.Free:
				this.busyFreeString = LocalizedStrings.GetHtmlEncoded(-971703552);
				goto IL_91;
			case BusyType.Busy:
				this.busyFreeString = LocalizedStrings.GetHtmlEncoded(2052801377);
				goto IL_91;
			case BusyType.OOF:
				this.busyFreeString = LocalizedStrings.GetHtmlEncoded(2047193656);
				goto IL_91;
			}
			this.busyFreeString = LocalizedStrings.GetHtmlEncoded(1797669216);
			IL_91:
			if (base.Item.Importance == Importance.High)
			{
				this.importanceString = LocalizedStrings.GetHtmlEncoded(-77932258);
			}
			else if (base.Item.Importance == Importance.Low)
			{
				this.importanceString = LocalizedStrings.GetHtmlEncoded(1502599728);
			}
			this.categoriesString = ItemUtility.GetCategoriesAsString(base.Item);
			this.attachmentWellRenderObjects = AttachmentWell.GetAttachmentInformation(base.Item, base.AttachmentLinks, base.UserContext.IsPublicLogon, base.IsEmbeddedItem);
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x000EBE6C File Offset: 0x000EA06C
		protected override void LoadMessageBodyIntoStream(TextWriter writer)
		{
			BodyConversionUtilities.GeneratePrintMessageBody(base.Item, writer, base.OwaContext, base.IsEmbeddedItem, base.IsEmbeddedItem ? base.RenderEmbeddedUrl() : null, base.ForceAllowWebBeacon, base.ForceEnableItemLink);
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06002A0C RID: 10764 RVA: 0x000EBEA3 File Offset: 0x000EA0A3
		protected override bool HasAttachments
		{
			get
			{
				if (this.hasAttachments == null)
				{
					this.hasAttachments = PrintAttachmentWell.ShouldRenderAttachments(this.attachmentWellRenderObjects);
				}
				return (bool)this.hasAttachments;
			}
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x000EBECE File Offset: 0x000EA0CE
		protected override void MeetingPageWriterFactory(string itemType, EventArgs e)
		{
			base.MeetingPageWriterFactory(itemType, e);
			base.MeetingPageWriter = new PrintPageWriter(base.MeetingPageWriter);
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06002A0E RID: 10766 RVA: 0x000EBEE9 File Offset: 0x000EA0E9
		public string BusyFreeString
		{
			get
			{
				return this.busyFreeString;
			}
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x06002A0F RID: 10767 RVA: 0x000EBEF1 File Offset: 0x000EA0F1
		public string ImportanceString
		{
			get
			{
				return this.importanceString;
			}
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x06002A10 RID: 10768 RVA: 0x000EBEF9 File Offset: 0x000EA0F9
		protected string CategoriesString
		{
			get
			{
				return this.categoriesString;
			}
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06002A11 RID: 10769 RVA: 0x000EBF01 File Offset: 0x000EA101
		protected ArrayList AttachmentWellRenderObjects
		{
			get
			{
				return this.attachmentWellRenderObjects;
			}
		}

		// Token: 0x04001C76 RID: 7286
		private string busyFreeString;

		// Token: 0x04001C77 RID: 7287
		private string importanceString;

		// Token: 0x04001C78 RID: 7288
		private string categoriesString;

		// Token: 0x04001C79 RID: 7289
		private object hasAttachments;

		// Token: 0x04001C7A RID: 7290
		private ArrayList attachmentWellRenderObjects;
	}
}
