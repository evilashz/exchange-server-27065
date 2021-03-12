using System;
using System.Collections;
using System.ComponentModel;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000046 RID: 70
	public class MultiValuedPropertyListSource<T> : IListSource
	{
		// Token: 0x060002C9 RID: 713 RVA: 0x0000A811 File Offset: 0x00008A11
		public MultiValuedPropertyListSource()
		{
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000A819 File Offset: 0x00008A19
		public MultiValuedPropertyListSource(MultiValuedProperty<T> property)
		{
			this.Property = property;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000A828 File Offset: 0x00008A28
		// (set) Token: 0x060002CC RID: 716 RVA: 0x0000A830 File Offset: 0x00008A30
		[DefaultValue(null)]
		public MultiValuedProperty<T> Property
		{
			get
			{
				return this.property;
			}
			set
			{
				this.property = value;
			}
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000A839 File Offset: 0x00008A39
		public IList GetList()
		{
			return this.property;
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000A841 File Offset: 0x00008A41
		public bool ContainsListCollection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040000C2 RID: 194
		private MultiValuedProperty<T> property;
	}
}
