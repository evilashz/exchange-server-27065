using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020002A7 RID: 679
	internal struct TypedValue
	{
		// Token: 0x06001884 RID: 6276 RVA: 0x0004DA9D File Offset: 0x0004BC9D
		public TypedValue(StreamPropertyType type, object value)
		{
			this.Type = type;
			this.Value = value;
		}

		// Token: 0x04000E67 RID: 3687
		public StreamPropertyType Type;

		// Token: 0x04000E68 RID: 3688
		public object Value;
	}
}
