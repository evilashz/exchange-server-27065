using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005D8 RID: 1496
	public class AppMetadataEntry
	{
		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x0600185E RID: 6238 RVA: 0x0002F809 File Offset: 0x0002DA09
		// (set) Token: 0x0600185F RID: 6239 RVA: 0x0002F811 File Offset: 0x0002DA11
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

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x0002F81A File Offset: 0x0002DA1A
		// (set) Token: 0x06001861 RID: 6241 RVA: 0x0002F836 File Offset: 0x0002DA36
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public byte[] value
		{
			get
			{
				if (this._value != null)
				{
					return (byte[])this._value.Clone();
				}
				return null;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x04001B0E RID: 6926
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _key;

		// Token: 0x04001B0F RID: 6927
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private byte[] _value;
	}
}
