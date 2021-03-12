using System;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000022 RID: 34
	public class HorizontalSprite : BaseSprite
	{
		// Token: 0x0600189F RID: 6303 RVA: 0x0004DBE8 File Offset: 0x0004BDE8
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.CssClass = this.CssClass + " " + this.SpriteCssClass;
			this.ImageUrl = BaseSprite.GetSpriteImageSrc(this);
			string value = (this.AlternateText == null) ? "" : this.AlternateText;
			base.Attributes.Add("title", value);
			this.GenerateEmptyAlternateText = true;
		}

		// Token: 0x170017C5 RID: 6085
		// (get) Token: 0x060018A0 RID: 6304 RVA: 0x0004DC57 File Offset: 0x0004BE57
		// (set) Token: 0x060018A1 RID: 6305 RVA: 0x0004DC5F File Offset: 0x0004BE5F
		public HorizontalSprite.SpriteId ImageId { get; set; }

		// Token: 0x170017C6 RID: 6086
		// (get) Token: 0x060018A2 RID: 6306 RVA: 0x0004DC68 File Offset: 0x0004BE68
		public string SpriteCssClass
		{
			get
			{
				return HorizontalSprite.GetCssClass(this.ImageId);
			}
		}

		// Token: 0x170017C7 RID: 6087
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x0004DC75 File Offset: 0x0004BE75
		public bool IsRenderable
		{
			get
			{
				return this.ImageId != HorizontalSprite.SpriteId.NONE;
			}
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x0004DC83 File Offset: 0x0004BE83
		public static string GetCssClass(HorizontalSprite.SpriteId spriteid)
		{
			if (spriteid == HorizontalSprite.SpriteId.NONE)
			{
				return string.Empty;
			}
			return HorizontalSprite.GetBaseCssClass() + " " + HorizontalSprite.ImageStyles[(int)spriteid];
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x0004DCA4 File Offset: 0x0004BEA4
		private static string GetBaseCssClass()
		{
			string text = HorizontalSprite.BaseCssClass;
			if (HorizontalSprite.HasDCImage && BaseSprite.IsDataCenter)
			{
				text = "DC" + text;
			}
			return text;
		}

		// Token: 0x04001A53 RID: 6739
		public static readonly string BaseCssClass = "HorizontalSprite";

		// Token: 0x04001A54 RID: 6740
		public static readonly bool HasDCImage = false;

		// Token: 0x04001A55 RID: 6741
		private static readonly string[] ImageStyles = new string[]
		{
			string.Empty,
			"HBGR",
			"A-HS"
		};

		// Token: 0x02000023 RID: 35
		public enum SpriteId
		{
			// Token: 0x04001A58 RID: 6744
			NONE,
			// Token: 0x04001A59 RID: 6745
			HBGR,
			// Token: 0x04001A5A RID: 6746
			EsoBar
		}
	}
}
