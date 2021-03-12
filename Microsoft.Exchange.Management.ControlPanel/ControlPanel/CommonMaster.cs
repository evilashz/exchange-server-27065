using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200003E RID: 62
	[ClientScriptResource(null, "Microsoft.Exchange.Management.ControlPanel.Client.Common.js")]
	public class CommonMaster : MasterPage, IMasterPage, IThemable
	{
		// Token: 0x1700180B RID: 6155
		// (get) Token: 0x0600196F RID: 6511 RVA: 0x00050410 File Offset: 0x0004E610
		// (set) Token: 0x06001970 RID: 6512 RVA: 0x00050418 File Offset: 0x0004E618
		protected ContentPlaceHolder ResultPanePlaceHolder { get; set; }

		// Token: 0x1700180C RID: 6156
		// (get) Token: 0x06001971 RID: 6513 RVA: 0x00050421 File Offset: 0x0004E621
		public ContentPlaceHolder ContentPlaceHolder
		{
			get
			{
				return this.ResultPanePlaceHolder;
			}
		}

		// Token: 0x1700180D RID: 6157
		// (get) Token: 0x06001972 RID: 6514 RVA: 0x00050429 File Offset: 0x0004E629
		public string Role
		{
			get
			{
				return this.role;
			}
		}

		// Token: 0x1700180E RID: 6158
		// (get) Token: 0x06001973 RID: 6515 RVA: 0x00050431 File Offset: 0x0004E631
		// (set) Token: 0x06001974 RID: 6516 RVA: 0x00050439 File Offset: 0x0004E639
		public FeatureSet FeatureSet { get; set; }

		// Token: 0x06001975 RID: 6517 RVA: 0x00050444 File Offset: 0x0004E644
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			string title = this.Page.Title;
			this.role = "document";
			using (SiteMapDataSource siteMapDataSource = new SiteMapDataSource())
			{
				SiteMapNode siteMapNode = siteMapDataSource.Provider.CurrentNode;
				if (siteMapNode != null)
				{
					title = siteMapNode.Title;
					this.role = "application";
					SiteMapNode rootNode = siteMapDataSource.Provider.RootNode;
					while (siteMapNode.ParentNode != null && siteMapNode.ParentNode != rootNode)
					{
						siteMapNode = siteMapNode.ParentNode;
					}
					string pageTitleFormat = Util.GetPageTitleFormat(siteMapNode);
					if (pageTitleFormat != null)
					{
						this.Page.Title = string.Format(pageTitleFormat, title);
					}
				}
			}
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x000504F8 File Offset: 0x0004E6F8
		public void AddCssFiles(string cssFiles)
		{
			if (!string.IsNullOrEmpty(cssFiles))
			{
				if (this.cssFiles == null)
				{
					this.cssFiles = new List<string>();
				}
				if (cssFiles.IndexOf(',') > 0)
				{
					string[] array = cssFiles.Split(CommonMaster.CommaList);
					for (int i = 0; i < array.Length; i++)
					{
						this.AddCssFile(array[i]);
					}
					return;
				}
				this.AddCssFile(cssFiles);
			}
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x00050556 File Offset: 0x0004E756
		private void AddCssFile(string file)
		{
			if (!this.cssFiles.Contains(file))
			{
				this.cssFiles.Add(file);
			}
		}

		// Token: 0x04001ABC RID: 6844
		private static readonly char[] CommaList = new char[]
		{
			','
		};

		// Token: 0x04001ABD RID: 6845
		private string role;

		// Token: 0x04001ABE RID: 6846
		protected List<string> cssFiles;
	}
}
