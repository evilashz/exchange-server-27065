using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200061E RID: 1566
	[DataContract]
	public class NavigationTreeNode
	{
		// Token: 0x0600457B RID: 17787 RVA: 0x000D2189 File Offset: 0x000D0389
		public NavigationTreeNode()
		{
		}

		// Token: 0x0600457C RID: 17788 RVA: 0x000D2194 File Offset: 0x000D0394
		public NavigationTreeNode(SiteMapNode siteMapNode)
		{
			this.ID = siteMapNode["id"];
			this.Title = siteMapNode.Title;
			this.Format = Util.GetPageTitleFormat(siteMapNode);
			this.Url = EcpUrl.ProcessUrl(siteMapNode.Url);
			this.HybridRole = siteMapNode["hybridRole"];
			bool noCache;
			bool.TryParse(siteMapNode["noCache"], out noCache);
			this.NoCache = noCache;
			bool flag;
			bool.TryParse(siteMapNode["contentPage"], out flag);
			this.hasContentPage = flag;
			this.isContentPage = flag;
		}

		// Token: 0x170026C9 RID: 9929
		// (get) Token: 0x0600457D RID: 17789 RVA: 0x000D222D File Offset: 0x000D042D
		// (set) Token: 0x0600457E RID: 17790 RVA: 0x000D2235 File Offset: 0x000D0435
		[DataMember(EmitDefaultValue = false)]
		public int Selected { get; set; }

		// Token: 0x170026CA RID: 9930
		// (get) Token: 0x0600457F RID: 17791 RVA: 0x000D223E File Offset: 0x000D043E
		// (set) Token: 0x06004580 RID: 17792 RVA: 0x000D2246 File Offset: 0x000D0446
		[DataMember(EmitDefaultValue = false)]
		public string Title { get; private set; }

		// Token: 0x170026CB RID: 9931
		// (get) Token: 0x06004581 RID: 17793 RVA: 0x000D224F File Offset: 0x000D044F
		// (set) Token: 0x06004582 RID: 17794 RVA: 0x000D2257 File Offset: 0x000D0457
		[DataMember(EmitDefaultValue = false)]
		public string Format { get; private set; }

		// Token: 0x170026CC RID: 9932
		// (get) Token: 0x06004583 RID: 17795 RVA: 0x000D2260 File Offset: 0x000D0460
		// (set) Token: 0x06004584 RID: 17796 RVA: 0x000D2268 File Offset: 0x000D0468
		[DataMember(EmitDefaultValue = false)]
		public string Url { get; internal set; }

		// Token: 0x170026CD RID: 9933
		// (get) Token: 0x06004585 RID: 17797 RVA: 0x000D2271 File Offset: 0x000D0471
		// (set) Token: 0x06004586 RID: 17798 RVA: 0x000D2279 File Offset: 0x000D0479
		[DataMember(EmitDefaultValue = false)]
		public string ID { get; private set; }

		// Token: 0x170026CE RID: 9934
		// (get) Token: 0x06004587 RID: 17799 RVA: 0x000D2282 File Offset: 0x000D0482
		// (set) Token: 0x06004588 RID: 17800 RVA: 0x000D228A File Offset: 0x000D048A
		[DataMember(EmitDefaultValue = false)]
		public string Sprite { get; private set; }

		// Token: 0x170026CF RID: 9935
		// (get) Token: 0x06004589 RID: 17801 RVA: 0x000D2293 File Offset: 0x000D0493
		// (set) Token: 0x0600458A RID: 17802 RVA: 0x000D229B File Offset: 0x000D049B
		[DataMember(EmitDefaultValue = false)]
		public bool NoCache { get; private set; }

		// Token: 0x170026D0 RID: 9936
		// (get) Token: 0x0600458B RID: 17803 RVA: 0x000D22A4 File Offset: 0x000D04A4
		// (set) Token: 0x0600458C RID: 17804 RVA: 0x000D22AC File Offset: 0x000D04AC
		[DataMember(EmitDefaultValue = false)]
		public string HybridRole { get; private set; }

		// Token: 0x170026D1 RID: 9937
		// (get) Token: 0x0600458D RID: 17805 RVA: 0x000D22B5 File Offset: 0x000D04B5
		public List<NavigationTreeNode> Children
		{
			get
			{
				return this.children ?? NavigationTreeNode.emptyCollection;
			}
		}

		// Token: 0x170026D2 RID: 9938
		// (get) Token: 0x0600458E RID: 17806 RVA: 0x000D22C8 File Offset: 0x000D04C8
		internal bool HasContentPage
		{
			get
			{
				if (!this.hasContentPage)
				{
					foreach (NavigationTreeNode navigationTreeNode in this.Children)
					{
						if (navigationTreeNode.HasContentPage)
						{
							this.hasContentPage = true;
							break;
						}
					}
				}
				return this.hasContentPage;
			}
		}

		// Token: 0x0600458F RID: 17807 RVA: 0x000D2334 File Offset: 0x000D0534
		internal string AggregateHybridRole()
		{
			if (!this.hybridRoleCalculated && string.IsNullOrEmpty(this.HybridRole) && !this.isContentPage)
			{
				StringBuilder stringBuilder = null;
				int selected = 0;
				int num = 0;
				foreach (NavigationTreeNode navigationTreeNode in this.Children)
				{
					string value = navigationTreeNode.AggregateHybridRole();
					if (string.IsNullOrEmpty(value))
					{
						stringBuilder = null;
						selected = num;
						break;
					}
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
					}
					else
					{
						stringBuilder.Append(',');
					}
					stringBuilder.Append(value);
					num++;
				}
				if (stringBuilder != null)
				{
					this.HybridRole = stringBuilder.ToString();
				}
				this.hybridRoleCalculated = true;
				this.Selected = selected;
			}
			return this.HybridRole;
		}

		// Token: 0x06004590 RID: 17808 RVA: 0x000D2408 File Offset: 0x000D0608
		public void AddChild(NavigationTreeNode node)
		{
			if (this.children == null)
			{
				this.children = new List<NavigationTreeNode>();
			}
			this.children.Add(node);
		}

		// Token: 0x04002E98 RID: 11928
		private readonly bool isContentPage;

		// Token: 0x04002E99 RID: 11929
		private static List<NavigationTreeNode> emptyCollection = new List<NavigationTreeNode>();

		// Token: 0x04002E9A RID: 11930
		[DataMember(Name = "Children", EmitDefaultValue = false)]
		private List<NavigationTreeNode> children;

		// Token: 0x04002E9B RID: 11931
		private bool hasContentPage;

		// Token: 0x04002E9C RID: 11932
		private bool hybridRoleCalculated;
	}
}
