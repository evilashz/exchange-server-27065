using System;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000026 RID: 38
	public class VoicemailSprite : BaseSprite
	{
		// Token: 0x060018B1 RID: 6321 RVA: 0x0004DEA4 File Offset: 0x0004C0A4
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.CssClass = this.CssClass + " " + this.SpriteCssClass;
			this.ImageUrl = BaseSprite.GetSpriteImageSrc(this);
			string value = (this.AlternateText == null) ? "" : this.AlternateText;
			base.Attributes.Add("title", value);
			this.GenerateEmptyAlternateText = true;
		}

		// Token: 0x170017CB RID: 6091
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x0004DF13 File Offset: 0x0004C113
		// (set) Token: 0x060018B3 RID: 6323 RVA: 0x0004DF1B File Offset: 0x0004C11B
		public VoicemailSprite.SpriteId ImageId { get; set; }

		// Token: 0x170017CC RID: 6092
		// (get) Token: 0x060018B4 RID: 6324 RVA: 0x0004DF24 File Offset: 0x0004C124
		public string SpriteCssClass
		{
			get
			{
				return VoicemailSprite.GetCssClass(this.ImageId);
			}
		}

		// Token: 0x170017CD RID: 6093
		// (get) Token: 0x060018B5 RID: 6325 RVA: 0x0004DF31 File Offset: 0x0004C131
		public bool IsRenderable
		{
			get
			{
				return this.ImageId != VoicemailSprite.SpriteId.NONE;
			}
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x0004DF3F File Offset: 0x0004C13F
		public static string GetCssClass(VoicemailSprite.SpriteId spriteid)
		{
			if (spriteid == VoicemailSprite.SpriteId.NONE)
			{
				return string.Empty;
			}
			return VoicemailSprite.GetBaseCssClass() + " " + VoicemailSprite.ImageStyles[(int)spriteid];
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x0004DF60 File Offset: 0x0004C160
		private static string GetBaseCssClass()
		{
			string text = VoicemailSprite.BaseCssClass;
			if (VoicemailSprite.HasDCImage && BaseSprite.IsDataCenter)
			{
				text = "DC" + text;
			}
			return text;
		}

		// Token: 0x04001A6C RID: 6764
		public static readonly string BaseCssClass = "VoicemailSprite";

		// Token: 0x04001A6D RID: 6765
		public static readonly bool HasDCImage = false;

		// Token: 0x04001A6E RID: 6766
		private static readonly string[] ImageStyles = new string[]
		{
			string.Empty,
			"G-VM",
			"D-VM",
			"C-VM",
			"F-VM",
			"A-VM",
			"B-VM",
			"E-VM"
		};

		// Token: 0x02000027 RID: 39
		public enum SpriteId
		{
			// Token: 0x04001A71 RID: 6769
			NONE,
			// Token: 0x04001A72 RID: 6770
			VoicemailPlayerPlaceHolder,
			// Token: 0x04001A73 RID: 6771
			VoiceMailSms32,
			// Token: 0x04001A74 RID: 6772
			VoiceMailPin32,
			// Token: 0x04001A75 RID: 6773
			VoiceMailGreeting32,
			// Token: 0x04001A76 RID: 6774
			TextMessaging32,
			// Token: 0x04001A77 RID: 6775
			Voicemail32,
			// Token: 0x04001A78 RID: 6776
			VoiceMailConfirm32
		}
	}
}
