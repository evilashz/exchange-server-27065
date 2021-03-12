using System;
using Microsoft.ManagementGUI.Commands;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000207 RID: 519
	public class ResultsCommandProfile : CommandProfile
	{
		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x0600178E RID: 6030 RVA: 0x00063054 File Offset: 0x00061254
		// (set) Token: 0x0600178F RID: 6031 RVA: 0x0006305C File Offset: 0x0006125C
		public ResultsCommandSetting Setting
		{
			get
			{
				return this.setting;
			}
			set
			{
				if (this.Setting != value)
				{
					base.BeginUpdate();
					if (this.Setting != null)
					{
						base.Components.Remove(this.Setting);
					}
					this.setting = value;
					if (this.Setting != null)
					{
						base.Components.Add(this.Setting);
					}
					base.EndUpdate();
				}
			}
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x000630B7 File Offset: 0x000612B7
		protected override void OnUpdated(EventArgs e)
		{
			if (this.Setting != null)
			{
				this.Setting.Profile = this;
			}
			base.OnUpdated(e);
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001791 RID: 6033 RVA: 0x000630D4 File Offset: 0x000612D4
		// (set) Token: 0x06001792 RID: 6034 RVA: 0x000630DC File Offset: 0x000612DC
		public ResultPane ResultPane
		{
			get
			{
				return this.resultPane;
			}
			set
			{
				if (this.ResultPane != value)
				{
					base.BeginUpdate();
					this.resultPane = value;
					base.EndUpdate();
				}
			}
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x000630FC File Offset: 0x000612FC
		public static ResultsCommandProfile CreateSeparator()
		{
			return new ResultsCommandProfile
			{
				Command = Command.CreateSeparator()
			};
		}

		// Token: 0x040008D5 RID: 2261
		private ResultsCommandSetting setting;

		// Token: 0x040008D6 RID: 2262
		private ResultPane resultPane;
	}
}
