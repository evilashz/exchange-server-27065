using System;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200007C RID: 124
	public class DynamicProperty
	{
		// Token: 0x06000443 RID: 1091 RVA: 0x0000FB35 File Offset: 0x0000DD35
		public DynamicProperty(string name, Type type)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.name = name;
			this.type = type;
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0000FB6D File Offset: 0x0000DD6D
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x0000FB75 File Offset: 0x0000DD75
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04000115 RID: 277
		private string name;

		// Token: 0x04000116 RID: 278
		private Type type;
	}
}
