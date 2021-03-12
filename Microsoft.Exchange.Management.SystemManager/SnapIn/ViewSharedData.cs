using System;
using Microsoft.ManagementConsole;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x0200029F RID: 671
	public class ViewSharedData
	{
		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001C64 RID: 7268 RVA: 0x0007B017 File Offset: 0x00079217
		// (set) Token: 0x06001C65 RID: 7269 RVA: 0x0007B01F File Offset: 0x0007921F
		public object SelectionObject
		{
			get
			{
				return this.selectionObject;
			}
			set
			{
				this.selectionObject = value;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001C66 RID: 7270 RVA: 0x0007B028 File Offset: 0x00079228
		// (set) Token: 0x06001C67 RID: 7271 RVA: 0x0007B030 File Offset: 0x00079230
		public string SelectedResultPaneName
		{
			get
			{
				return this.selectedResultPaneName;
			}
			set
			{
				this.selectedResultPaneName = value;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001C68 RID: 7272 RVA: 0x0007B039 File Offset: 0x00079239
		// (set) Token: 0x06001C69 RID: 7273 RVA: 0x0007B041 File Offset: 0x00079241
		public View CurrentActiveView
		{
			get
			{
				return this.currentActiveView;
			}
			internal set
			{
				this.currentActiveView = value;
			}
		}

		// Token: 0x04000A96 RID: 2710
		private object selectionObject;

		// Token: 0x04000A97 RID: 2711
		private string selectedResultPaneName;

		// Token: 0x04000A98 RID: 2712
		private View currentActiveView;
	}
}
