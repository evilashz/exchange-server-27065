using System;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000024 RID: 36
	public class NavigationSprite : BaseSprite
	{
		// Token: 0x060018A8 RID: 6312 RVA: 0x0004DD20 File Offset: 0x0004BF20
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.CssClass = this.CssClass + " " + this.SpriteCssClass;
			this.ImageUrl = BaseSprite.GetSpriteImageSrc(this);
			string value = (this.AlternateText == null) ? "" : this.AlternateText;
			base.Attributes.Add("title", value);
			this.GenerateEmptyAlternateText = true;
		}

		// Token: 0x170017C8 RID: 6088
		// (get) Token: 0x060018A9 RID: 6313 RVA: 0x0004DD8F File Offset: 0x0004BF8F
		// (set) Token: 0x060018AA RID: 6314 RVA: 0x0004DD97 File Offset: 0x0004BF97
		public NavigationSprite.SpriteId ImageId { get; set; }

		// Token: 0x170017C9 RID: 6089
		// (get) Token: 0x060018AB RID: 6315 RVA: 0x0004DDA0 File Offset: 0x0004BFA0
		public string SpriteCssClass
		{
			get
			{
				return NavigationSprite.GetCssClass(this.ImageId);
			}
		}

		// Token: 0x170017CA RID: 6090
		// (get) Token: 0x060018AC RID: 6316 RVA: 0x0004DDAD File Offset: 0x0004BFAD
		public bool IsRenderable
		{
			get
			{
				return this.ImageId != NavigationSprite.SpriteId.NONE;
			}
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x0004DDBB File Offset: 0x0004BFBB
		public static string GetCssClass(NavigationSprite.SpriteId spriteid)
		{
			if (spriteid == NavigationSprite.SpriteId.NONE)
			{
				return string.Empty;
			}
			return NavigationSprite.GetBaseCssClass() + " " + NavigationSprite.ImageStyles[(int)spriteid];
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x0004DDDC File Offset: 0x0004BFDC
		private static string GetBaseCssClass()
		{
			string text = NavigationSprite.BaseCssClass;
			if (NavigationSprite.HasDCImage && BaseSprite.IsDataCenter)
			{
				text = "DC" + text;
			}
			return text;
		}

		// Token: 0x04001A5B RID: 6747
		public static readonly string BaseCssClass = "NavigationSprite";

		// Token: 0x04001A5C RID: 6748
		public static readonly bool HasDCImage = false;

		// Token: 0x04001A5D RID: 6749
		private static readonly string[] ImageStyles = new string[]
		{
			string.Empty,
			"OwaBrand",
			"Office365Icon",
			"EsoBarEdge",
			"ReturnToOWA",
			"NtfImgB",
			"NtfImg",
			"NtfInfo",
			"NtfError",
			"NtfWarning",
			"CollapsePriNav",
			"ExpandPriNav"
		};

		// Token: 0x02000025 RID: 37
		public enum SpriteId
		{
			// Token: 0x04001A60 RID: 6752
			NONE,
			// Token: 0x04001A61 RID: 6753
			OwaBrand,
			// Token: 0x04001A62 RID: 6754
			Office365Icon,
			// Token: 0x04001A63 RID: 6755
			EsoBarEdge,
			// Token: 0x04001A64 RID: 6756
			ReturnToOWA,
			// Token: 0x04001A65 RID: 6757
			NotificationBlue,
			// Token: 0x04001A66 RID: 6758
			NotificationNormal,
			// Token: 0x04001A67 RID: 6759
			NotificationInfo,
			// Token: 0x04001A68 RID: 6760
			NotificationError,
			// Token: 0x04001A69 RID: 6761
			NotificationWarning,
			// Token: 0x04001A6A RID: 6762
			CollapsePriNav,
			// Token: 0x04001A6B RID: 6763
			ExpandPriNav
		}
	}
}
