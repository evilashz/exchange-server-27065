using System;

namespace Microsoft.Exchange.Clients.Common
{
	// Token: 0x02000025 RID: 37
	public class LiveHeaderLink : ILiveHeaderElement
	{
		// Token: 0x06000103 RID: 259 RVA: 0x00008160 File Offset: 0x00006360
		public LiveHeaderLink()
		{
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00008168 File Offset: 0x00006368
		public LiveHeaderLink(string linkText)
		{
			this.Text = linkText;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00008177 File Offset: 0x00006377
		// (set) Token: 0x06000106 RID: 262 RVA: 0x0000817F File Offset: 0x0000637F
		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00008188 File Offset: 0x00006388
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00008190 File Offset: 0x00006390
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00008199 File Offset: 0x00006399
		// (set) Token: 0x0600010A RID: 266 RVA: 0x000081A1 File Offset: 0x000063A1
		public string Href
		{
			get
			{
				return this.href;
			}
			set
			{
				this.href = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000081AA File Offset: 0x000063AA
		// (set) Token: 0x0600010C RID: 268 RVA: 0x000081B2 File Offset: 0x000063B2
		public bool OpenInNewWindow
		{
			get
			{
				return this.openInNewWindow;
			}
			set
			{
				this.openInNewWindow = value;
			}
		}

		// Token: 0x04000269 RID: 617
		private string text;

		// Token: 0x0400026A RID: 618
		private string title;

		// Token: 0x0400026B RID: 619
		private string href;

		// Token: 0x0400026C RID: 620
		private bool openInNewWindow;
	}
}
