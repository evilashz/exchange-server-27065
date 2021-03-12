using System;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200001A RID: 26
	public class CommonSprite : BaseSprite
	{
		// Token: 0x0600188A RID: 6282 RVA: 0x0004C2B4 File Offset: 0x0004A4B4
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.CssClass = this.CssClass + " " + this.SpriteCssClass;
			this.ImageUrl = BaseSprite.GetSpriteImageSrc(this);
			string value = (this.AlternateText == null) ? "" : this.AlternateText;
			base.Attributes.Add("title", value);
			this.GenerateEmptyAlternateText = true;
		}

		// Token: 0x170017BF RID: 6079
		// (get) Token: 0x0600188B RID: 6283 RVA: 0x0004C323 File Offset: 0x0004A523
		// (set) Token: 0x0600188C RID: 6284 RVA: 0x0004C32B File Offset: 0x0004A52B
		public CommonSprite.SpriteId ImageId { get; set; }

		// Token: 0x170017C0 RID: 6080
		// (get) Token: 0x0600188D RID: 6285 RVA: 0x0004C334 File Offset: 0x0004A534
		public string SpriteCssClass
		{
			get
			{
				return CommonSprite.GetCssClass(this.ImageId);
			}
		}

		// Token: 0x170017C1 RID: 6081
		// (get) Token: 0x0600188E RID: 6286 RVA: 0x0004C341 File Offset: 0x0004A541
		public bool IsRenderable
		{
			get
			{
				return this.ImageId != CommonSprite.SpriteId.NONE;
			}
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x0004C34F File Offset: 0x0004A54F
		public static string GetCssClass(CommonSprite.SpriteId spriteid)
		{
			if (spriteid == CommonSprite.SpriteId.NONE)
			{
				return string.Empty;
			}
			return CommonSprite.GetBaseCssClass() + " " + CommonSprite.ImageStyles[(int)spriteid];
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x0004C370 File Offset: 0x0004A570
		private static string GetBaseCssClass()
		{
			string text = CommonSprite.BaseCssClass;
			if (CommonSprite.HasDCImage && BaseSprite.IsDataCenter)
			{
				text = "DC" + text;
			}
			return text;
		}

		// Token: 0x0400193A RID: 6458
		public static readonly string BaseCssClass = "CommonSprite";

		// Token: 0x0400193B RID: 6459
		public static readonly bool HasDCImage = false;

		// Token: 0x0400193C RID: 6460
		private static readonly string[] ImageStyles = new string[]
		{
			string.Empty,
			"OutlookLogo",
			"fvaArrow",
			"Warning32",
			"A-CO",
			"OfficeLogo",
			"B-CO",
			"Expand",
			"Collapse",
			"TrendingNeutral",
			"Information",
			"Information16",
			"Warning",
			"Warning16",
			"Error",
			"Blank",
			"ArrowExpand",
			"Error16",
			"ArrowDownW",
			"TrendingDown",
			"TrendingUp",
			"ArrowDown",
			"FvaBottom",
			"Plus",
			"Minus",
			"FvaTop",
			"ItmChk",
			"cbItmDisabled > .cbChkCol > .ItmChk",
			"Previous",
			"Next",
			"formletClose",
			"C-CO"
		};

		// Token: 0x0200001B RID: 27
		public enum SpriteId
		{
			// Token: 0x0400193F RID: 6463
			NONE,
			// Token: 0x04001940 RID: 6464
			OutlookLogo,
			// Token: 0x04001941 RID: 6465
			fvaArrow,
			// Token: 0x04001942 RID: 6466
			Warning32,
			// Token: 0x04001943 RID: 6467
			Aok,
			// Token: 0x04001944 RID: 6468
			OfficeLogo,
			// Token: 0x04001945 RID: 6469
			SignInArrow,
			// Token: 0x04001946 RID: 6470
			Expand,
			// Token: 0x04001947 RID: 6471
			Collapse,
			// Token: 0x04001948 RID: 6472
			TrendingNeutral,
			// Token: 0x04001949 RID: 6473
			Information,
			// Token: 0x0400194A RID: 6474
			Information16,
			// Token: 0x0400194B RID: 6475
			Warning,
			// Token: 0x0400194C RID: 6476
			Warning16,
			// Token: 0x0400194D RID: 6477
			Error,
			// Token: 0x0400194E RID: 6478
			Blank,
			// Token: 0x0400194F RID: 6479
			ArrowExpand,
			// Token: 0x04001950 RID: 6480
			Error16,
			// Token: 0x04001951 RID: 6481
			ArrowDownW,
			// Token: 0x04001952 RID: 6482
			TrendingDown,
			// Token: 0x04001953 RID: 6483
			TrendingUp,
			// Token: 0x04001954 RID: 6484
			ArrowDown,
			// Token: 0x04001955 RID: 6485
			shadowBottom,
			// Token: 0x04001956 RID: 6486
			Plus,
			// Token: 0x04001957 RID: 6487
			Minus,
			// Token: 0x04001958 RID: 6488
			shadowTop,
			// Token: 0x04001959 RID: 6489
			ItemChecked,
			// Token: 0x0400195A RID: 6490
			ItemDisabledChecked,
			// Token: 0x0400195B RID: 6491
			Previous,
			// Token: 0x0400195C RID: 6492
			Next,
			// Token: 0x0400195D RID: 6493
			MetroDelete_small,
			// Token: 0x0400195E RID: 6494
			MoreInfo
		}
	}
}
