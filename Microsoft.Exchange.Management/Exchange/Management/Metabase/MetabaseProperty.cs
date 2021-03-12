using System;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x020004BA RID: 1210
	internal class MetabaseProperty
	{
		// Token: 0x06002A25 RID: 10789 RVA: 0x000A8484 File Offset: 0x000A6684
		public MetabaseProperty()
		{
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x000A848C File Offset: 0x000A668C
		public MetabaseProperty(string name, object value) : this(name, value, true)
		{
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x000A8497 File Offset: 0x000A6697
		public MetabaseProperty(string name, object value, bool eraseOldValue)
		{
			this.propertyName = name;
			this.propertyValue = value;
			this.eraseOldValue = eraseOldValue;
		}

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06002A28 RID: 10792 RVA: 0x000A84B4 File Offset: 0x000A66B4
		// (set) Token: 0x06002A29 RID: 10793 RVA: 0x000A84BC File Offset: 0x000A66BC
		public string Name
		{
			get
			{
				return this.propertyName;
			}
			set
			{
				this.propertyName = value;
			}
		}

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x06002A2A RID: 10794 RVA: 0x000A84C5 File Offset: 0x000A66C5
		// (set) Token: 0x06002A2B RID: 10795 RVA: 0x000A84CD File Offset: 0x000A66CD
		public object Value
		{
			get
			{
				return this.propertyValue;
			}
			set
			{
				this.propertyValue = value;
			}
		}

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x06002A2C RID: 10796 RVA: 0x000A84D6 File Offset: 0x000A66D6
		// (set) Token: 0x06002A2D RID: 10797 RVA: 0x000A84DE File Offset: 0x000A66DE
		public bool EraseOldValue
		{
			get
			{
				return this.eraseOldValue;
			}
			set
			{
				this.eraseOldValue = value;
			}
		}

		// Token: 0x04001F82 RID: 8066
		private string propertyName;

		// Token: 0x04001F83 RID: 8067
		private object propertyValue;

		// Token: 0x04001F84 RID: 8068
		private bool eraseOldValue;
	}
}
