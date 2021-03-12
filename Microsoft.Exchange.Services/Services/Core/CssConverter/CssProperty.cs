using System;

namespace Microsoft.Exchange.Services.Core.CssConverter
{
	// Token: 0x020000C2 RID: 194
	public class CssProperty
	{
		// Token: 0x0600054F RID: 1359 RVA: 0x0001CA3A File Offset: 0x0001AC3A
		public CssProperty()
		{
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0001CA42 File Offset: 0x0001AC42
		public CssProperty(string propertyName, string propertyValue)
		{
			this.Name = propertyName;
			this.Value = propertyValue;
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x0001CA58 File Offset: 0x0001AC58
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x0001CA60 File Offset: 0x0001AC60
		public string Name { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x0001CA69 File Offset: 0x0001AC69
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x0001CA71 File Offset: 0x0001AC71
		public string Value { get; set; }

		// Token: 0x06000555 RID: 1365 RVA: 0x0001CA7A File Offset: 0x0001AC7A
		public override string ToString()
		{
			return string.Format("{0}: {1}; ", this.Name, this.Value);
		}
	}
}
