using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000040 RID: 64
	public class FilterValuePropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x0600028C RID: 652 RVA: 0x00009E76 File Offset: 0x00008076
		internal FilterValuePropertyDescriptor(FilterNode owner, PropertyDescriptor pd) : base(pd)
		{
			this.owner = owner;
			this.original = pd;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00009E8D File Offset: 0x0000808D
		public override Type ComponentType
		{
			get
			{
				return this.original.ComponentType;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00009E9A File Offset: 0x0000809A
		public override bool IsReadOnly
		{
			get
			{
				return this.original.IsReadOnly;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00009EA7 File Offset: 0x000080A7
		public override Type PropertyType
		{
			get
			{
				return this.original.PropertyType;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00009EB4 File Offset: 0x000080B4
		public Type ValuePropertyType
		{
			get
			{
				if (this.owner.PropertyDefinition != null)
				{
					return this.owner.PropertyDefinition.Type;
				}
				return this.PropertyType;
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00009EDA File Offset: 0x000080DA
		public override bool CanResetValue(object component)
		{
			return this.original.CanResetValue(component);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00009EE8 File Offset: 0x000080E8
		public override object GetValue(object component)
		{
			return this.original.GetValue(component);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00009EF6 File Offset: 0x000080F6
		public override void ResetValue(object component)
		{
			this.original.ResetValue(component);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00009F04 File Offset: 0x00008104
		public override void SetValue(object component, object value)
		{
			this.original.SetValue(component, value);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00009F13 File Offset: 0x00008113
		public override bool ShouldSerializeValue(object component)
		{
			return this.original.ShouldSerializeValue(component);
		}

		// Token: 0x040000B0 RID: 176
		private FilterNode owner;

		// Token: 0x040000B1 RID: 177
		private PropertyDescriptor original;
	}
}
