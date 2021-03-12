using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002F4 RID: 756
	[ClientScriptResource("ViewAccountProperties", "Microsoft.Exchange.Management.ControlPanel.Client.Users.js")]
	public sealed class ViewAccountProperties : Properties
	{
		// Token: 0x17001E2A RID: 7722
		// (get) Token: 0x06002D40 RID: 11584 RVA: 0x0008AC61 File Offset: 0x00088E61
		public Image Image
		{
			get
			{
				return this.image;
			}
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x0008AC6C File Offset: 0x00088E6C
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.image = this.FindImage(this, "imgUserPhoto");
			Identity identity = Identity.FromExecutingUserId();
			foreach (PopupLauncher popupLauncher in this.GetVisibleControls<PopupLauncher>(this))
			{
				popupLauncher.NavigationUrl = EcpUrl.AppendQueryParameter(popupLauncher.NavigationUrl, "id", identity.RawIdentity);
			}
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x0008ACF0 File Offset: 0x00088EF0
		protected override ScriptControlDescriptor GetScriptDescriptor()
		{
			ScriptControlDescriptor scriptDescriptor = base.GetScriptDescriptor();
			scriptDescriptor.Type = "ViewAccountProperties";
			scriptDescriptor.AddElementProperty("Image", this.image.ClientID);
			return scriptDescriptor;
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x0008AD28 File Offset: 0x00088F28
		private Image FindImage(Control parent, string id)
		{
			if (parent.Visible)
			{
				Image image = parent as Image;
				if (image != null && string.Equals(id, image.ID, StringComparison.OrdinalIgnoreCase))
				{
					return image;
				}
				foreach (object obj in parent.Controls)
				{
					Control parent2 = (Control)obj;
					image = this.FindImage(parent2, id);
					if (image != null)
					{
						return image;
					}
				}
			}
			return null;
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x0008B064 File Offset: 0x00089264
		private IEnumerable<T> GetVisibleControls<T>(Control parent) where T : class
		{
			if (parent.Visible)
			{
				T control = parent as T;
				if (control != null)
				{
					yield return control;
				}
				foreach (object obj in parent.Controls)
				{
					Control subControl = (Control)obj;
					foreach (T c in this.GetVisibleControls<T>(subControl))
					{
						yield return c;
					}
				}
			}
			yield break;
		}

		// Token: 0x0400224C RID: 8780
		private Image image;
	}
}
