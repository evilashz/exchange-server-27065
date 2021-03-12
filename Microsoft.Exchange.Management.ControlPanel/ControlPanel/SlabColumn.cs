using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Principal;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200065D RID: 1629
	[ParseChildren(true, "Slabs")]
	[DefaultProperty("Slabs")]
	public class SlabColumn : SlabComponent
	{
		// Token: 0x17002743 RID: 10051
		// (get) Token: 0x060046D1 RID: 18129 RVA: 0x000D62D5 File Offset: 0x000D44D5
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public List<SlabControl> Slabs
		{
			get
			{
				return this.slabs;
			}
		}

		// Token: 0x17002744 RID: 10052
		// (get) Token: 0x060046D2 RID: 18130 RVA: 0x000D62DD File Offset: 0x000D44DD
		// (set) Token: 0x060046D3 RID: 18131 RVA: 0x000D62E5 File Offset: 0x000D44E5
		public Unit Width { get; set; }

		// Token: 0x060046D4 RID: 18132 RVA: 0x000D62F0 File Offset: 0x000D44F0
		internal void Refactor()
		{
			IPrincipal user = this.Context.User;
			double num = 0.0;
			double num2 = 0.0;
			bool flag = false;
			for (int i = this.Slabs.Count - 1; i >= 0; i--)
			{
				SlabControl slabControl = this.Slabs[i];
				bool flag2 = false;
				slabControl.InitializeAsUserControl(this.Page);
				if (!slabControl.AccessibleToUser(user))
				{
					this.Slabs.RemoveAt(i);
					flag2 = true;
					slabControl.Visible = false;
				}
				if (slabControl.Height.Type == UnitType.Percentage)
				{
					num += slabControl.Height.Value;
					if (flag2)
					{
						flag = true;
					}
					else
					{
						num2 += slabControl.Height.Value;
					}
				}
			}
			if (flag && num2 != 0.0)
			{
				double num3 = num / num2;
				foreach (SlabControl slabControl2 in this.Slabs)
				{
					if (slabControl2.Height.Type == UnitType.Percentage)
					{
						slabControl2.Height = new Unit(slabControl2.Height.Value * num3, UnitType.Percentage);
					}
				}
			}
		}

		// Token: 0x04002FCF RID: 12239
		private List<SlabControl> slabs = new List<SlabControl>();
	}
}
