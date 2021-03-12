using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000113 RID: 275
	public class BindableUserControl : BindableUserControlBase
	{
		// Token: 0x06000A04 RID: 2564 RVA: 0x00022AC0 File Offset: 0x00020CC0
		public BindableUserControl()
		{
			this.validator = new NullValidator();
			base.Name = "BindableUserControl";
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x00022ADE File Offset: 0x00020CDE
		// (set) Token: 0x06000A06 RID: 2566 RVA: 0x00022AE6 File Offset: 0x00020CE6
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public IValidator Validator
		{
			get
			{
				return this.validator;
			}
			set
			{
				this.validator = ((value == null) ? new NullValidator() : value);
			}
		}

		// Token: 0x04000454 RID: 1108
		private IValidator validator;
	}
}
