using System;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000020 RID: 32
	public class HomePageSprite : BaseSprite
	{
		// Token: 0x06001896 RID: 6294 RVA: 0x0004DA38 File Offset: 0x0004BC38
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.CssClass = this.CssClass + " " + this.SpriteCssClass;
			this.ImageUrl = BaseSprite.GetSpriteImageSrc(this);
			string value = (this.AlternateText == null) ? "" : this.AlternateText;
			base.Attributes.Add("title", value);
			this.GenerateEmptyAlternateText = true;
		}

		// Token: 0x170017C2 RID: 6082
		// (get) Token: 0x06001897 RID: 6295 RVA: 0x0004DAA7 File Offset: 0x0004BCA7
		// (set) Token: 0x06001898 RID: 6296 RVA: 0x0004DAAF File Offset: 0x0004BCAF
		public HomePageSprite.SpriteId ImageId { get; set; }

		// Token: 0x170017C3 RID: 6083
		// (get) Token: 0x06001899 RID: 6297 RVA: 0x0004DAB8 File Offset: 0x0004BCB8
		public string SpriteCssClass
		{
			get
			{
				return HomePageSprite.GetCssClass(this.ImageId);
			}
		}

		// Token: 0x170017C4 RID: 6084
		// (get) Token: 0x0600189A RID: 6298 RVA: 0x0004DAC5 File Offset: 0x0004BCC5
		public bool IsRenderable
		{
			get
			{
				return this.ImageId != HomePageSprite.SpriteId.NONE;
			}
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x0004DAD3 File Offset: 0x0004BCD3
		public static string GetCssClass(HomePageSprite.SpriteId spriteid)
		{
			if (spriteid == HomePageSprite.SpriteId.NONE)
			{
				return string.Empty;
			}
			return HomePageSprite.GetBaseCssClass() + " " + HomePageSprite.ImageStyles[(int)spriteid];
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x0004DAF4 File Offset: 0x0004BCF4
		private static string GetBaseCssClass()
		{
			string text = HomePageSprite.BaseCssClass;
			if (HomePageSprite.HasDCImage && BaseSprite.IsDataCenter)
			{
				text = "DC" + text;
			}
			return text;
		}

		// Token: 0x04001A3D RID: 6717
		public static readonly string BaseCssClass = "HomePageSprite";

		// Token: 0x04001A3E RID: 6718
		public static readonly bool HasDCImage = false;

		// Token: 0x04001A3F RID: 6719
		private static readonly string[] ImageStyles = new string[]
		{
			string.Empty,
			"J-HP",
			"K-HP",
			"H-HP",
			"I-HP",
			"N-HP",
			"O-HP",
			"L-HP",
			"M-HP",
			"B-HP",
			"C-HP",
			"CalendarLog",
			"A-HP",
			"F-HP",
			"G-HP",
			"D-HP",
			"E-HP"
		};

		// Token: 0x02000021 RID: 33
		public enum SpriteId
		{
			// Token: 0x04001A42 RID: 6722
			NONE,
			// Token: 0x04001A43 RID: 6723
			RemotePowerShell,
			// Token: 0x04001A44 RID: 6724
			Rules,
			// Token: 0x04001A45 RID: 6725
			Password,
			// Token: 0x04001A46 RID: 6726
			Passwordliveid,
			// Token: 0x04001A47 RID: 6727
			Voicemail,
			// Token: 0x04001A48 RID: 6728
			WhatsNewForOrganizations,
			// Token: 0x04001A49 RID: 6729
			Themes,
			// Token: 0x04001A4A RID: 6730
			Users32,
			// Token: 0x04001A4B RID: 6731
			Forward,
			// Token: 0x04001A4C RID: 6732
			ImportContacts,
			// Token: 0x04001A4D RID: 6733
			CalendarLog,
			// Token: 0x04001A4E RID: 6734
			Forefront,
			// Token: 0x04001A4F RID: 6735
			Oof,
			// Token: 0x04001A50 RID: 6736
			Outlook,
			// Token: 0x04001A51 RID: 6737
			ManageOrganization,
			// Token: 0x04001A52 RID: 6738
			Mobiledevices
		}
	}
}
