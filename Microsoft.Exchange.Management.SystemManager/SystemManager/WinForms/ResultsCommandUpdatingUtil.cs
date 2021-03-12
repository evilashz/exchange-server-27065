using System;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.ManagementGUI.Commands;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000203 RID: 515
	public class ResultsCommandUpdatingUtil : CommandUpdatingUtil
	{
		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001777 RID: 6007 RVA: 0x00062D07 File Offset: 0x00060F07
		public ResultsCommandProfile Profile
		{
			get
			{
				return base.Profile as ResultsCommandProfile;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x00062D14 File Offset: 0x00060F14
		public ResultPane ResultPane
		{
			get
			{
				if (this.Profile != null)
				{
					return this.Profile.ResultPane;
				}
				return null;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001779 RID: 6009 RVA: 0x00062D2B File Offset: 0x00060F2B
		public ResultsCommandSetting Setting
		{
			get
			{
				if (this.Profile != null)
				{
					return this.Profile.Setting;
				}
				return null;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x0600177A RID: 6010 RVA: 0x00062D42 File Offset: 0x00060F42
		// (set) Token: 0x0600177B RID: 6011 RVA: 0x00062D4A File Offset: 0x00060F4A
		public OrganizationType[] OrganizationTypes { get; set; }

		// Token: 0x0600177C RID: 6012 RVA: 0x00062D53 File Offset: 0x00060F53
		protected override void OnProfileUpdated()
		{
			base.OnProfileUpdated();
			base.Command.Visible = WinformsHelper.IsCurrentOrganizationAllowed(this.OrganizationTypes);
		}
	}
}
