using System;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000033 RID: 51
	[Serializable]
	internal class InChainFilter : SinglePropertyFilter
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x000074A3 File Offset: 0x000056A3
		public InChainFilter(PropertyDefinition property, object value) : base(property)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.Value = value;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000074C4 File Offset: 0x000056C4
		public override bool Equals(object obj)
		{
			InChainFilter inChainFilter = obj as InChainFilter;
			return inChainFilter != null && this.Value.Equals(inChainFilter.Value) && base.Equals(obj);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000074F7 File Offset: 0x000056F7
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.Value.GetHashCode();
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000750B File Offset: 0x0000570B
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00007513 File Offset: 0x00005713
		public object Value { get; private set; }

		// Token: 0x060001B7 RID: 439 RVA: 0x0000751C File Offset: 0x0000571C
		public override SinglePropertyFilter CloneWithAnotherProperty(PropertyDefinition property)
		{
			base.CheckClonable(property);
			return new InChainFilter(property, this.Value);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00007534 File Offset: 0x00005734
		public override void ToString(StringBuilder sb)
		{
			sb.Append("(InChain(");
			sb.Append(base.Property.Name);
			sb.Append(",");
			sb.Append(this.Value);
			sb.Append("))");
		}
	}
}
