using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CE2 RID: 3298
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class UnresolvedPropertyDefinition : PropertyDefinition, IComparable<UnresolvedPropertyDefinition>, IComparable
	{
		// Token: 0x06007210 RID: 29200 RVA: 0x001F90DD File Offset: 0x001F72DD
		private UnresolvedPropertyDefinition(PropTag propTag) : base(string.Empty, InternalSchema.ClrTypeFromPropTag(propTag))
		{
			this.propertyTag = (uint)propTag;
		}

		// Token: 0x06007211 RID: 29201 RVA: 0x001F90F8 File Offset: 0x001F72F8
		public static UnresolvedPropertyDefinition Create(PropTag propTag)
		{
			PropType propType = propTag.ValueType();
			if (!EnumValidator<PropType>.IsValidValue(propType))
			{
				throw new EnumArgumentException(string.Format("Invalid property type {0}", propType), "propTag");
			}
			return new UnresolvedPropertyDefinition(propTag);
		}

		// Token: 0x17001E72 RID: 7794
		// (get) Token: 0x06007212 RID: 29202 RVA: 0x001F9135 File Offset: 0x001F7335
		public uint PropertyTag
		{
			get
			{
				return this.propertyTag;
			}
		}

		// Token: 0x06007213 RID: 29203 RVA: 0x001F913D File Offset: 0x001F733D
		public override int GetHashCode()
		{
			return (int)this.propertyTag;
		}

		// Token: 0x06007214 RID: 29204 RVA: 0x001F9145 File Offset: 0x001F7345
		public override string ToString()
		{
			return string.Format("[{0:x8}] {1}", this.propertyTag, base.Name);
		}

		// Token: 0x06007215 RID: 29205 RVA: 0x001F9164 File Offset: 0x001F7364
		public int CompareTo(UnresolvedPropertyDefinition other)
		{
			if (other == null)
			{
				throw new ArgumentException(ServerStrings.ObjectMustBeOfType(base.GetType().Name));
			}
			return this.PropertyTag.CompareTo(other.PropertyTag);
		}

		// Token: 0x06007216 RID: 29206 RVA: 0x001F91A4 File Offset: 0x001F73A4
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			UnresolvedPropertyDefinition other = obj as UnresolvedPropertyDefinition;
			return this.CompareTo(other);
		}

		// Token: 0x04004F51 RID: 20305
		private readonly uint propertyTag;
	}
}
