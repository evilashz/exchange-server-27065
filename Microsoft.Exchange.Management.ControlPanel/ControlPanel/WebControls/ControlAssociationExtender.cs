using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200057A RID: 1402
	[RequiredScript(typeof(CommonToolkitScripts))]
	[TargetControlType(typeof(Control))]
	[ClientScriptResource("ControlAssociationBehavior", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	public class ControlAssociationExtender : ExtenderControlBase
	{
		// Token: 0x06004138 RID: 16696 RVA: 0x000C6DE8 File Offset: 0x000C4FE8
		public ControlAssociationExtender()
		{
			this.Trigger = "click";
		}

		// Token: 0x1700254A RID: 9546
		// (get) Token: 0x06004139 RID: 16697 RVA: 0x000C6DFB File Offset: 0x000C4FFB
		// (set) Token: 0x0600413A RID: 16698 RVA: 0x000C6E03 File Offset: 0x000C5003
		public string SectionID { get; set; }

		// Token: 0x1700254B RID: 9547
		// (get) Token: 0x0600413B RID: 16699 RVA: 0x000C6E0C File Offset: 0x000C500C
		// (set) Token: 0x0600413C RID: 16700 RVA: 0x000C6E14 File Offset: 0x000C5014
		public bool InvokeOnInit { get; set; }

		// Token: 0x1700254C RID: 9548
		// (get) Token: 0x0600413D RID: 16701 RVA: 0x000C6E1D File Offset: 0x000C501D
		// (set) Token: 0x0600413E RID: 16702 RVA: 0x000C6E25 File Offset: 0x000C5025
		public bool BindToControl { get; set; }

		// Token: 0x1700254D RID: 9549
		// (get) Token: 0x0600413F RID: 16703 RVA: 0x000C6E2E File Offset: 0x000C502E
		// (set) Token: 0x06004140 RID: 16704 RVA: 0x000C6E36 File Offset: 0x000C5036
		public string Trigger { get; set; }

		// Token: 0x1700254E RID: 9550
		// (get) Token: 0x06004141 RID: 16705 RVA: 0x000C6E3F File Offset: 0x000C503F
		// (set) Token: 0x06004142 RID: 16706 RVA: 0x000C6E47 File Offset: 0x000C5047
		public string AssociatedControlId { get; set; }

		// Token: 0x1700254F RID: 9551
		// (get) Token: 0x06004143 RID: 16707 RVA: 0x000C6E50 File Offset: 0x000C5050
		// (set) Token: 0x06004144 RID: 16708 RVA: 0x000C6E58 File Offset: 0x000C5058
		public string InvokedMethod { get; set; }

		// Token: 0x06004145 RID: 16709 RVA: 0x000C6E64 File Offset: 0x000C5064
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			if (this.InvokeOnInit)
			{
				descriptor.AddProperty("InvokeOnInit", true);
			}
			if (this.BindToControl)
			{
				descriptor.AddProperty("BindToControl", true);
			}
			if (this.Trigger != "click")
			{
				descriptor.AddProperty("Trigger", this.Trigger);
			}
			descriptor.AddElementProperty("AssociatedControl", this.AssociatedControlId, this);
		}

		// Token: 0x06004146 RID: 16710 RVA: 0x000C6EE0 File Offset: 0x000C50E0
		protected override void OnLoad(EventArgs e)
		{
			if (!this.SectionID.IsNullOrBlank())
			{
				Control control = this;
				while (control != null)
				{
					if (control is Section)
					{
						Section section = control.Parent.FindControl(this.SectionID) as Section;
						if (section == null)
						{
							break;
						}
						if (this.AssociatedControlId.IsNullOrBlank())
						{
							this.AssociatedControlId = section.ClientID;
							break;
						}
						this.AssociatedControlId = section.FindControl(this.AssociatedControlId).ClientID;
						break;
					}
					else
					{
						control = control.NamingContainer;
					}
				}
			}
			if (this.AssociatedControlId.IsNullOrBlank())
			{
				throw new NotSupportedException("Associated Control cannot be found.");
			}
		}

		// Token: 0x06004147 RID: 16711 RVA: 0x000C6F78 File Offset: 0x000C5178
		protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors(Control targetControl)
		{
			IEnumerable<ScriptDescriptor> scriptDescriptors = base.GetScriptDescriptors(targetControl);
			ScriptBehaviorDescriptor scriptBehaviorDescriptor = scriptDescriptors.First<ScriptDescriptor>() as ScriptBehaviorDescriptor;
			scriptBehaviorDescriptor.AddScriptProperty("InvokedMethod", this.InvokedMethod);
			if (base.TargetControl is RadioButtonList)
			{
				scriptBehaviorDescriptor.AddProperty("IsRadioBtnList", true);
			}
			return scriptDescriptors;
		}
	}
}
