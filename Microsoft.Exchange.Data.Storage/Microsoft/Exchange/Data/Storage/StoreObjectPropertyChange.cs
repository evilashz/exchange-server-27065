using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000454 RID: 1108
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class StoreObjectPropertyChange
	{
		// Token: 0x0600313A RID: 12602 RVA: 0x000C99C3 File Offset: 0x000C7BC3
		public StoreObjectPropertyChange(PropertyDefinition propertyDefinition, object oldValue, object newValue) : this(propertyDefinition, oldValue, newValue, false)
		{
		}

		// Token: 0x0600313B RID: 12603 RVA: 0x000C99CF File Offset: 0x000C7BCF
		public StoreObjectPropertyChange(PropertyDefinition propertyDefinition, object oldValue, object newValue, bool isPropertyValidated)
		{
			this.propertyDefinition = propertyDefinition;
			this.oldValue = oldValue;
			this.newValue = newValue;
			this.isPropertyValidated = isPropertyValidated;
		}

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x0600313C RID: 12604 RVA: 0x000C99F4 File Offset: 0x000C7BF4
		// (set) Token: 0x0600313D RID: 12605 RVA: 0x000C99FC File Offset: 0x000C7BFC
		public PropertyDefinition PropertyDefinition
		{
			get
			{
				return this.propertyDefinition;
			}
			set
			{
				this.propertyDefinition = value;
			}
		}

		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x0600313E RID: 12606 RVA: 0x000C9A05 File Offset: 0x000C7C05
		// (set) Token: 0x0600313F RID: 12607 RVA: 0x000C9A0D File Offset: 0x000C7C0D
		public object OldValue
		{
			get
			{
				return this.oldValue;
			}
			set
			{
				this.oldValue = value;
			}
		}

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x06003140 RID: 12608 RVA: 0x000C9A16 File Offset: 0x000C7C16
		// (set) Token: 0x06003141 RID: 12609 RVA: 0x000C9A1E File Offset: 0x000C7C1E
		public object NewValue
		{
			get
			{
				return this.newValue;
			}
			set
			{
				this.newValue = value;
			}
		}

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x06003142 RID: 12610 RVA: 0x000C9A27 File Offset: 0x000C7C27
		// (set) Token: 0x06003143 RID: 12611 RVA: 0x000C9A2F File Offset: 0x000C7C2F
		public bool IsPropertyValidated
		{
			get
			{
				return this.isPropertyValidated;
			}
			set
			{
				this.isPropertyValidated = value;
			}
		}

		// Token: 0x04001AA5 RID: 6821
		private PropertyDefinition propertyDefinition;

		// Token: 0x04001AA6 RID: 6822
		private object oldValue;

		// Token: 0x04001AA7 RID: 6823
		private object newValue;

		// Token: 0x04001AA8 RID: 6824
		private bool isPropertyValidated;
	}
}
