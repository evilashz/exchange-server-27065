using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200012C RID: 300
	public abstract class StrongTypeEditor<T> : BindableUserControl
	{
		// Token: 0x06000BED RID: 3053 RVA: 0x0002AE04 File Offset: 0x00029004
		public StrongTypeEditor()
		{
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x0002AE0C File Offset: 0x0002900C
		// (set) Token: 0x06000BEF RID: 3055 RVA: 0x0002AE14 File Offset: 0x00029014
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public T StrongType
		{
			get
			{
				return this.strongType;
			}
			set
			{
				if (!object.Equals(this.strongType, value))
				{
					this.strongType = value;
					((StrongTypeEditorDataHandler<T>)base.Validator).StrongType = value;
					if (this.StrongTypeChanged != null)
					{
						this.StrongTypeChanged(this, EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x06000BF0 RID: 3056 RVA: 0x0002AE6C File Offset: 0x0002906C
		// (remove) Token: 0x06000BF1 RID: 3057 RVA: 0x0002AEA4 File Offset: 0x000290A4
		public event EventHandler StrongTypeChanged;

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0002AED9 File Offset: 0x000290D9
		public void LoadValidator(string schema)
		{
			base.Validator = new StrongTypeEditorDataHandler<T>(this, schema);
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x0002AEE8 File Offset: 0x000290E8
		protected override string ExposedPropertyName
		{
			get
			{
				return "StrongType";
			}
		}

		// Token: 0x040004DD RID: 1245
		private T strongType;
	}
}
