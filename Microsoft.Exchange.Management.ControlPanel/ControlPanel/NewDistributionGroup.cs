using System;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000242 RID: 578
	public class NewDistributionGroup : OrgSettingsPage
	{
		// Token: 0x0600283B RID: 10299 RVA: 0x0007DA34 File Offset: 0x0007BC34
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			EcpCollectionEditor ecpCollectionEditor = (EcpCollectionEditor)(base.Master as IMasterPage).ContentPlaceHolder.FindControl("Wizard1").FindControl("generalInfoSection").FindControl("ceManagedBy");
			ecpCollectionEditor.IsUsingOwaPeoplePicker = true;
			ecpCollectionEditor.PickerCallerType = PickerCallerType.GroupsOwners;
			EcpCollectionEditor ecpCollectionEditor2 = (EcpCollectionEditor)(base.Master as IMasterPage).ContentPlaceHolder.FindControl("Wizard1").FindControl("generalInfoSection").FindControl("ceMembers");
			ecpCollectionEditor2.IsUsingOwaPeoplePicker = true;
			ecpCollectionEditor2.PickerCallerType = PickerCallerType.GroupsMembers;
			PropertyPageSheet propertyPageSheet = (PropertyPageSheet)(base.Master as IMasterPage).ContentPlaceHolder.FindControl("Wizard1");
			propertyPageSheet.Attributes["vm-IsInOwaOption"] = "true";
		}
	}
}
