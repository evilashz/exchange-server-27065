using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x02000593 RID: 1427
	public class AppMetadataEntry
	{
		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x0600137C RID: 4988 RVA: 0x0002BAB5 File Offset: 0x00029CB5
		// (set) Token: 0x0600137D RID: 4989 RVA: 0x0002BABD File Offset: 0x00029CBD
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

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x0600137E RID: 4990 RVA: 0x0002BAC6 File Offset: 0x00029CC6
		// (set) Token: 0x0600137F RID: 4991 RVA: 0x0002BAE2 File Offset: 0x00029CE2
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

		// Token: 0x040018CE RID: 6350
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _key;

		// Token: 0x040018CF RID: 6351
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private byte[] _value;
	}
}
