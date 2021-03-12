using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.TextConverters;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001F9 RID: 505
	internal abstract class OwaSafeHtmlCallbackBase : HtmlCallbackBase
	{
		// Token: 0x06001099 RID: 4249 RVA: 0x00065B41 File Offset: 0x00063D41
		public OwaSafeHtmlCallbackBase()
		{
			this.Initialize();
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x00065B70 File Offset: 0x00063D70
		public OwaSafeHtmlCallbackBase(Item item) : base(item)
		{
			this.Initialize();
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x00065BA0 File Offset: 0x00063DA0
		public OwaSafeHtmlCallbackBase(AttachmentCollection attachmentCollection, Body body) : base(attachmentCollection, body)
		{
			this.Initialize();
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x00065BD1 File Offset: 0x00063DD1
		private void Initialize()
		{
			base.ClearInlineOnUnmarkedAttachments = true;
			base.RemoveUnlinkedAttachments = false;
			OwaSafeHtmlCallbackBase.blankImageFileName = ThemeManager.GetBaseThemeFileUrl(ThemeFileId.Clear1x1);
			base.InitializeAttachmentLinks(null);
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x00065BF5 File Offset: 0x00063DF5
		protected static bool IsUrlTag(HtmlTagId tagId, HtmlTagContextAttribute attribute)
		{
			return (tagId == HtmlTagId.A && attribute.Id == HtmlAttributeId.Href) || (tagId == HtmlTagId.Area && attribute.Id == HtmlAttributeId.Href);
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x00065C19 File Offset: 0x00063E19
		protected static bool IsImageTag(HtmlTagId tagId, HtmlTagContextAttribute attribute)
		{
			return (tagId == HtmlTagId.Img && attribute.Id == HtmlAttributeId.Src) || (tagId == HtmlTagId.Img && attribute.Id == HtmlAttributeId.DynSrc) || (tagId == HtmlTagId.Img && attribute.Id == HtmlAttributeId.LowSrc);
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00065C4F File Offset: 0x00063E4F
		protected static bool IsBackgroundAttribute(HtmlTagContextAttribute attribute)
		{
			return attribute.Id == HtmlAttributeId.Background;
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00065C5C File Offset: 0x00063E5C
		protected static bool IsSanitizingAttribute(HtmlTagContextAttribute attribute)
		{
			return attribute.Id == HtmlAttributeId.Border || attribute.Id == HtmlAttributeId.Width || attribute.Id == HtmlAttributeId.Height;
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00065C81 File Offset: 0x00063E81
		public static bool IsBaseTag(HtmlTagId tagId, HtmlTagContextAttribute attribute)
		{
			return tagId == HtmlTagId.Base && attribute.Id == HtmlAttributeId.Href;
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x060010A2 RID: 4258 RVA: 0x00065C94 File Offset: 0x00063E94
		public virtual bool HasBlockedImages
		{
			get
			{
				return this.hasBlockedImages;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x00065C9C File Offset: 0x00063E9C
		public bool HasBlockedInlineAttachments
		{
			get
			{
				return this.hasBlockedInlineAttachments;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x060010A4 RID: 4260 RVA: 0x00065CA4 File Offset: 0x00063EA4
		public virtual bool HasRtfEmbeddedImages
		{
			get
			{
				return this.hasRtfEmbeddedImages;
			}
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x00065CAC File Offset: 0x00063EAC
		public bool ApplyAttachmentsUpdates(Item item)
		{
			bool flag = false;
			if (base.NeedsSave() && !Utilities.IsClearSigned(item))
			{
				item.OpenAsReadWrite();
				CalendarItemBase calendarItemBase = item as CalendarItemBase;
				if (calendarItemBase != null)
				{
					Utilities.ValidateCalendarItemBaseStoreObject(calendarItemBase);
				}
				try
				{
					flag = this.SaveChanges();
				}
				catch (AccessDeniedException)
				{
				}
				if (flag)
				{
					try
					{
						Utilities.SaveItem(item, false);
					}
					catch (AccessDeniedException)
					{
					}
				}
			}
			return flag;
		}

		// Token: 0x04000B44 RID: 2884
		protected static readonly string LocalUrlPrefix = "#";

		// Token: 0x04000B45 RID: 2885
		protected static readonly string DoubleBlank = "  ";

		// Token: 0x04000B46 RID: 2886
		protected static readonly string AttachmentBaseUrl = "attachment.ashx?id=";

		// Token: 0x04000B47 RID: 2887
		protected static readonly string JSLocalLink = "javascript:parent.onLocalLink";

		// Token: 0x04000B48 RID: 2888
		protected static readonly string JSMethodPrefix = "('";

		// Token: 0x04000B49 RID: 2889
		protected static readonly string JSMethodSuffix = "',window.frameElement)";

		// Token: 0x04000B4A RID: 2890
		protected static string blankImageFileName;

		// Token: 0x04000B4B RID: 2891
		protected bool hasBlockedImages;

		// Token: 0x04000B4C RID: 2892
		protected bool hasBlockedInlineAttachments;

		// Token: 0x04000B4D RID: 2893
		protected bool hasRtfEmbeddedImages;

		// Token: 0x04000B4E RID: 2894
		protected string inlineRTFattachmentScheme = "objattph://";

		// Token: 0x04000B4F RID: 2895
		protected string embeddedRTFImage = "rtfimage://";

		// Token: 0x04000B50 RID: 2896
		protected string inlineHTMLAttachmentScheme = "cid:";
	}
}
