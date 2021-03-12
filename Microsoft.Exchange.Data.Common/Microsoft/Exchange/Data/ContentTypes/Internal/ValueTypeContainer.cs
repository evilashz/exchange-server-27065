using System;

namespace Microsoft.Exchange.Data.ContentTypes.Internal
{
	// Token: 0x020000A6 RID: 166
	internal abstract class ValueTypeContainer
	{
		// Token: 0x060006C5 RID: 1733 RVA: 0x000261B8 File Offset: 0x000243B8
		protected ValueTypeContainer()
		{
			this.Reset();
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x000261C6 File Offset: 0x000243C6
		public void SetValueTypeParameter(string value)
		{
			this.valueTypeParameter = value;
			this.isValueTypeInitialized = false;
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x000261D6 File Offset: 0x000243D6
		public void SetPropertyName(string value)
		{
			this.propertyName = value;
			this.isValueTypeInitialized = false;
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060006C8 RID: 1736
		public abstract bool IsTextType { get; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060006C9 RID: 1737
		public abstract bool CanBeMultivalued { get; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060006CA RID: 1738
		public abstract bool CanBeCompound { get; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x000261E6 File Offset: 0x000243E6
		public bool IsInitialized
		{
			get
			{
				return this.propertyName != null;
			}
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x000261F4 File Offset: 0x000243F4
		public virtual void Reset()
		{
			this.valueTypeParameter = null;
			this.propertyName = null;
			this.isValueTypeInitialized = false;
		}

		// Token: 0x040005A1 RID: 1441
		protected string valueTypeParameter;

		// Token: 0x040005A2 RID: 1442
		protected string propertyName;

		// Token: 0x040005A3 RID: 1443
		protected bool isValueTypeInitialized;
	}
}
