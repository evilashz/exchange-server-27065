using System;
using System.Collections.Generic;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.ManagementGUI.Commands;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x020000D0 RID: 208
	public class ResultCommandsProfile
	{
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x00018C28 File Offset: 0x00016E28
		public List<ResultsCommandProfile> ResultPaneCommands
		{
			get
			{
				return this.resultPaneCommands;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x00018C30 File Offset: 0x00016E30
		public List<ResultsCommandProfile> CustomSelectionCommands
		{
			get
			{
				return this.customSelectionCommands;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x00018C38 File Offset: 0x00016E38
		public List<ResultsCommandProfile> DeleteSelectionCommands
		{
			get
			{
				return this.deleteSelectionCommands;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x00018C40 File Offset: 0x00016E40
		public List<ResultsCommandProfile> ShowSelectionPropertiesCommands
		{
			get
			{
				return this.showSelectionPropertiesCommands;
			}
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00018C48 File Offset: 0x00016E48
		public ResultsCommandProfile GetProfile(Command command)
		{
			ResultsCommandProfile profile = this.GetProfile(this.ResultPaneCommands, command);
			if (profile == null)
			{
				profile = this.GetProfile(this.ResultPaneCommands, command);
			}
			if (profile == null)
			{
				profile = this.GetProfile(this.CustomSelectionCommands, command);
			}
			if (profile == null)
			{
				profile = this.GetProfile(this.DeleteSelectionCommands, command);
			}
			if (profile == null)
			{
				profile = this.GetProfile(this.ShowSelectionPropertiesCommands, command);
			}
			return profile;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00018CA8 File Offset: 0x00016EA8
		private ResultsCommandProfile GetProfile(List<ResultsCommandProfile> profiles, Command command)
		{
			foreach (ResultsCommandProfile resultsCommandProfile in profiles)
			{
				if (resultsCommandProfile.Command == command)
				{
					return resultsCommandProfile;
				}
			}
			return null;
		}

		// Token: 0x0400037E RID: 894
		private List<ResultsCommandProfile> resultPaneCommands = new List<ResultsCommandProfile>();

		// Token: 0x0400037F RID: 895
		private List<ResultsCommandProfile> customSelectionCommands = new List<ResultsCommandProfile>();

		// Token: 0x04000380 RID: 896
		private List<ResultsCommandProfile> deleteSelectionCommands = new List<ResultsCommandProfile>();

		// Token: 0x04000381 RID: 897
		private List<ResultsCommandProfile> showSelectionPropertiesCommands = new List<ResultsCommandProfile>();
	}
}
