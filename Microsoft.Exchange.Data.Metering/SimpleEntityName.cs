using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Metering
{
	// Token: 0x02000013 RID: 19
	internal class SimpleEntityName<TEntityType> : IEntityName<TEntityType>, IEquatable<IEntityName<TEntityType>> where TEntityType : struct, IConvertible
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00005447 File Offset: 0x00003647
		public SimpleEntityName(TEntityType type, string value)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("value", value);
			this.Value = value;
			this.Type = type;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00005468 File Offset: 0x00003668
		// (set) Token: 0x060000FB RID: 251 RVA: 0x00005470 File Offset: 0x00003670
		public TEntityType Type { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00005479 File Offset: 0x00003679
		// (set) Token: 0x060000FD RID: 253 RVA: 0x00005481 File Offset: 0x00003681
		public string Value { get; private set; }

		// Token: 0x060000FE RID: 254 RVA: 0x0000548C File Offset: 0x0000368C
		public bool Equals(IEntityName<TEntityType> other)
		{
			if (other == null)
			{
				return false;
			}
			SimpleEntityName<TEntityType> simpleEntityName = other as SimpleEntityName<TEntityType>;
			if (simpleEntityName == null)
			{
				return false;
			}
			TEntityType type = this.Type;
			return type.Equals(other.Type) && this.Value.Equals(simpleEntityName.Value);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000054DE File Offset: 0x000036DE
		public override bool Equals(object obj)
		{
			return obj is IEntityName<TEntityType> && this.Equals(obj as IEntityName<TEntityType>);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000054F6 File Offset: 0x000036F6
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005503 File Offset: 0x00003703
		public override string ToString()
		{
			return string.Format("{0}-{1}", this.Type, this.Value);
		}
	}
}
