using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005E3 RID: 1507
	public class KeyValue
	{
		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x060018EE RID: 6382 RVA: 0x0002FF8A File Offset: 0x0002E18A
		// (set) Token: 0x060018EF RID: 6383 RVA: 0x0002FF92 File Offset: 0x0002E192
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x060018F0 RID: 6384 RVA: 0x0002FF9B File Offset: 0x0002E19B
		// (set) Token: 0x060018F1 RID: 6385 RVA: 0x0002FFA3 File Offset: 0x0002E1A3
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x04001B4F RID: 6991
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _key;

		// Token: 0x04001B50 RID: 6992
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _value;
	}
}
