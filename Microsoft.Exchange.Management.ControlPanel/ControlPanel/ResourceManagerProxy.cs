using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using Microsoft.Office.CsmSdk.Controls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000647 RID: 1607
	[PersistChildren(false)]
	[ParseChildren(true)]
	[NonVisualControl]
	public class ResourceManagerProxy : Control
	{
		// Token: 0x17002711 RID: 10001
		// (get) Token: 0x0600463E RID: 17982 RVA: 0x000D45C4 File Offset: 0x000D27C4
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		public List<ResourceEntry> ResourceEntries
		{
			get
			{
				return this.resourceEntries;
			}
		}

		// Token: 0x0600463F RID: 17983 RVA: 0x000D45CC File Offset: 0x000D27CC
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			ResourceManager.GetCurrent(this.Page).ResourceEntries.AddRange(this.ResourceEntries);
		}

		// Token: 0x04002F82 RID: 12162
		private List<ResourceEntry> resourceEntries = new List<ResourceEntry>();
	}
}
