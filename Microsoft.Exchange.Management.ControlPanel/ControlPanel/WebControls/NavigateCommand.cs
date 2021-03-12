using System;
using System.ComponentModel;
using System.Security.Principal;
using System.Web.UI;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000601 RID: 1537
	public class NavigateCommand : Command
	{
		// Token: 0x060044D0 RID: 17616 RVA: 0x000CFD43 File Offset: 0x000CDF43
		public NavigateCommand()
		{
		}

		// Token: 0x060044D1 RID: 17617 RVA: 0x000CFD4B File Offset: 0x000CDF4B
		public NavigateCommand(string text, CommandSprite.SpriteId imageID) : base(text, imageID)
		{
			this.SelectionParameterName = "id";
		}

		// Token: 0x17002694 RID: 9876
		// (get) Token: 0x060044D2 RID: 17618 RVA: 0x000CFD60 File Offset: 0x000CDF60
		// (set) Token: 0x060044D3 RID: 17619 RVA: 0x000CFD68 File Offset: 0x000CDF68
		[DefaultValue(null)]
		[UrlProperty]
		public virtual string NavigateUrl { get; set; }

		// Token: 0x17002695 RID: 9877
		// (get) Token: 0x060044D4 RID: 17620 RVA: 0x000CFD71 File Offset: 0x000CDF71
		// (set) Token: 0x060044D5 RID: 17621 RVA: 0x000CFD79 File Offset: 0x000CDF79
		[DefaultValue(false)]
		public virtual bool BypassUrlCheck { get; set; }

		// Token: 0x17002696 RID: 9878
		// (get) Token: 0x060044D6 RID: 17622 RVA: 0x000CFD82 File Offset: 0x000CDF82
		// (set) Token: 0x060044D7 RID: 17623 RVA: 0x000CFD8A File Offset: 0x000CDF8A
		[DefaultValue("_self")]
		public virtual string TargetFrame { get; set; }

		// Token: 0x17002697 RID: 9879
		// (get) Token: 0x060044D8 RID: 17624 RVA: 0x000CFD93 File Offset: 0x000CDF93
		// (set) Token: 0x060044D9 RID: 17625 RVA: 0x000CFD9B File Offset: 0x000CDF9B
		[DefaultValue("id")]
		public virtual string SelectionParameterName { get; set; }

		// Token: 0x060044DA RID: 17626 RVA: 0x000CFDA4 File Offset: 0x000CDFA4
		public override bool IsAccessibleToUser(IPrincipal user)
		{
			return (this.BypassUrlCheck || LoginUtil.CheckUrlAccess(this.NavigateUrl)) && base.IsAccessibleToUser(user);
		}

		// Token: 0x060044DB RID: 17627 RVA: 0x000CFDC4 File Offset: 0x000CDFC4
		protected internal override void PreRender(Control c)
		{
			base.PreRender(c);
			if (!string.IsNullOrEmpty(this.NavigateUrl))
			{
				this.NavigateUrl = c.ResolveClientUrl(this.NavigateUrl);
			}
		}
	}
}
