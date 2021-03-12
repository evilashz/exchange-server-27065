using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Format
{
	// Token: 0x02000299 RID: 665
	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	internal struct Property
	{
		// Token: 0x06001A8D RID: 6797 RVA: 0x000CFE9A File Offset: 0x000CE09A
		public Property(PropertyId id, PropertyValue value)
		{
			this.id = id;
			this.value = value;
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001A8E RID: 6798 RVA: 0x000CFEAA File Offset: 0x000CE0AA
		public bool IsNull
		{
			get
			{
				return this.id == PropertyId.Null;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001A8F RID: 6799 RVA: 0x000CFEB5 File Offset: 0x000CE0B5
		// (set) Token: 0x06001A90 RID: 6800 RVA: 0x000CFEBD File Offset: 0x000CE0BD
		public PropertyId Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001A91 RID: 6801 RVA: 0x000CFEC6 File Offset: 0x000CE0C6
		// (set) Token: 0x06001A92 RID: 6802 RVA: 0x000CFECE File Offset: 0x000CE0CE
		public PropertyValue Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x000CFED7 File Offset: 0x000CE0D7
		public void Set(PropertyId id, PropertyValue value)
		{
			this.id = id;
			this.value = value;
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x000CFEE7 File Offset: 0x000CE0E7
		public override string ToString()
		{
			return this.id.ToString() + " = " + this.value.ToString();
		}

		// Token: 0x04002059 RID: 8281
		private PropertyId id;

		// Token: 0x0400205A RID: 8282
		private PropertyValue value;

		// Token: 0x0400205B RID: 8283
		public static readonly Property Null = default(Property);
	}
}
