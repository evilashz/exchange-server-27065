using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Format
{
	// Token: 0x020002A6 RID: 678
	internal struct NodePropertiesEnumerator : IEnumerable<Property>, IEnumerable, IEnumerator<Property>, IDisposable, IEnumerator
	{
		// Token: 0x06001B27 RID: 6951 RVA: 0x000D21C1 File Offset: 0x000D03C1
		public NodePropertiesEnumerator(FormatNode node)
		{
			this.FlagProperties = node.FlagProperties;
			this.Properties = node.Properties;
			this.CurrentPropertyIndex = 0;
			this.CurrentProperty = Property.Null;
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001B28 RID: 6952 RVA: 0x000D21EF File Offset: 0x000D03EF
		public Property Current
		{
			get
			{
				return this.CurrentProperty;
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001B29 RID: 6953 RVA: 0x000D21F7 File Offset: 0x000D03F7
		object IEnumerator.Current
		{
			get
			{
				return this.CurrentProperty;
			}
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x000D2204 File Offset: 0x000D0404
		public IEnumerator<Property> GetEnumerator()
		{
			return this;
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x000D2211 File Offset: 0x000D0411
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this;
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x000D2220 File Offset: 0x000D0420
		public bool MoveNext()
		{
			while (this.CurrentPropertyIndex < 16)
			{
				this.CurrentPropertyIndex++;
				if (this.FlagProperties.IsDefined((PropertyId)this.CurrentPropertyIndex))
				{
					this.CurrentProperty.Set((PropertyId)this.CurrentPropertyIndex, new PropertyValue(this.FlagProperties.IsOn((PropertyId)this.CurrentPropertyIndex)));
					return true;
				}
			}
			if (this.Properties != null && this.CurrentPropertyIndex < this.Properties.Length + 17)
			{
				this.CurrentPropertyIndex++;
				if (this.CurrentPropertyIndex < this.Properties.Length + 17)
				{
					this.CurrentProperty = this.Properties[this.CurrentPropertyIndex - 17];
					return true;
				}
			}
			this.CurrentProperty = Property.Null;
			return false;
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x000D22EF File Offset: 0x000D04EF
		public void Reset()
		{
			this.CurrentPropertyIndex = 0;
			this.CurrentProperty = Property.Null;
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x000D2303 File Offset: 0x000D0503
		public void Dispose()
		{
		}

		// Token: 0x040020AD RID: 8365
		internal FlagProperties FlagProperties;

		// Token: 0x040020AE RID: 8366
		internal Property[] Properties;

		// Token: 0x040020AF RID: 8367
		internal int CurrentPropertyIndex;

		// Token: 0x040020B0 RID: 8368
		internal Property CurrentProperty;
	}
}
