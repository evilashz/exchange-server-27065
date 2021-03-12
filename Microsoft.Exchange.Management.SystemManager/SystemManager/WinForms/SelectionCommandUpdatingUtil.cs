using System;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000204 RID: 516
	public class SelectionCommandUpdatingUtil : ResultsCommandUpdatingUtil
	{
		// Token: 0x0600177E RID: 6014 RVA: 0x00062D79 File Offset: 0x00060F79
		protected override void OnProfileUpdating()
		{
			if (base.ResultPane != null && base.Command != null)
			{
				base.ResultPane.SelectionChanged -= this.ResultPane_SelectionChanged;
			}
			base.OnProfileUpdating();
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x00062DA8 File Offset: 0x00060FA8
		protected override void OnProfileUpdated()
		{
			base.OnProfileUpdated();
			if (base.ResultPane != null && base.Command != null && WinformsHelper.IsCurrentOrganizationAllowed(base.OrganizationTypes))
			{
				base.ResultPane.SelectionChanged += this.ResultPane_SelectionChanged;
				this.ResultPane_SelectionChanged(base.ResultPane, EventArgs.Empty);
			}
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x00062E00 File Offset: 0x00061000
		private void ResultPane_SelectionChanged(object sender, EventArgs e)
		{
			if (base.ResultPane.HasSelection)
			{
				base.Command.Visible = true;
				this.UpdateCommand();
				if (base.Setting.RequiresSingleSelection)
				{
					base.Command.Visible = (base.Command.Visible && base.ResultPane.HasSingleSelection);
					return;
				}
				if (base.Setting.RequiresMultiSelection)
				{
					base.Command.Visible = (base.Command.Visible && base.ResultPane.HasMultiSelection);
					return;
				}
			}
			else
			{
				base.Command.Visible = false;
			}
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x00062EA0 File Offset: 0x000610A0
		protected virtual void UpdateCommand()
		{
		}
	}
}
