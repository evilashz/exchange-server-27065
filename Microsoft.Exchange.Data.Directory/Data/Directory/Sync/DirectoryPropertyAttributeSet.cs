using System;
using System.Collections;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000867 RID: 2151
	internal class DirectoryPropertyAttributeSet : DirectoryProperty
	{
		// Token: 0x170026E6 RID: 9958
		// (get) Token: 0x06006CD1 RID: 27857 RVA: 0x00174D76 File Offset: 0x00172F76
		// (set) Token: 0x06006CD2 RID: 27858 RVA: 0x00174D7E File Offset: 0x00172F7E
		public AttributeSet[] Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		// Token: 0x06006CD3 RID: 27859 RVA: 0x00174D87 File Offset: 0x00172F87
		public override IList GetValues()
		{
			if (this.Value != null)
			{
				return this.Value;
			}
			return DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006CD4 RID: 27860 RVA: 0x00174D9D File Offset: 0x00172F9D
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new AttributeSet[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x040046C3 RID: 18115
		private AttributeSet[] valueField;
	}
}
