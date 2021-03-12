using System;
using System.Reflection;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000034 RID: 52
	[Serializable]
	internal abstract class GenericBitMaskFilter : SinglePropertyFilter
	{
		// Token: 0x060001B9 RID: 441 RVA: 0x00007584 File Offset: 0x00005784
		public GenericBitMaskFilter(PropertyDefinition property, ulong mask) : base(property)
		{
			if (property.Type != typeof(short) && property.Type != typeof(int) && property.Type != typeof(long) && property.Type != typeof(ushort) && property.Type != typeof(uint) && property.Type != typeof(ulong) && !typeof(Enum).GetTypeInfo().IsAssignableFrom(property.Type.GetTypeInfo()))
			{
				throw new ArgumentOutOfRangeException(DataStrings.ExceptionBitMaskNotSupported(property.Name, property.Type));
			}
			this.mask = mask;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000766F File Offset: 0x0000586F
		public ulong Mask
		{
			get
			{
				return this.mask;
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00007678 File Offset: 0x00005878
		public override bool Equals(object obj)
		{
			GenericBitMaskFilter genericBitMaskFilter = obj as GenericBitMaskFilter;
			return genericBitMaskFilter != null && this.mask == genericBitMaskFilter.mask && base.Equals(obj);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000076A6 File Offset: 0x000058A6
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ (int)this.mask;
		}

		// Token: 0x04000095 RID: 149
		private readonly ulong mask;
	}
}
