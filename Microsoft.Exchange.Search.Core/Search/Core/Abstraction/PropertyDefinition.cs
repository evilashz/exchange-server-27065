using System;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x02000043 RID: 67
	internal abstract class PropertyDefinition
	{
		// Token: 0x06000159 RID: 345 RVA: 0x00002A89 File Offset: 0x00000C89
		protected PropertyDefinition(string name, Type type, PropertyFlag flags)
		{
			this.name = name;
			this.type = type;
			this.flags = flags;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00002AA6 File Offset: 0x00000CA6
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00002AAE File Offset: 0x00000CAE
		internal Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00002AB6 File Offset: 0x00000CB6
		internal PropertyFlag Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00002ABE File Offset: 0x00000CBE
		public override bool Equals(object obj)
		{
			return this.Equals(obj as PropertyDefinition);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00002ACC File Offset: 0x00000CCC
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00002ADC File Offset: 0x00000CDC
		public virtual bool Equals(PropertyDefinition other)
		{
			return other != null && (object.ReferenceEquals(other, this) || (StringComparer.OrdinalIgnoreCase.Equals(other.Name, this.Name) && !(other.Type != this.Type) && other.Flags == this.Flags && !(other.GetType() != base.GetType())));
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00002B48 File Offset: 0x00000D48
		public override string ToString()
		{
			return string.Format("{0} ({1})", this.Name, this.Type);
		}

		// Token: 0x04000073 RID: 115
		private readonly string name;

		// Token: 0x04000074 RID: 116
		private readonly Type type;

		// Token: 0x04000075 RID: 117
		private readonly PropertyFlag flags;
	}
}
