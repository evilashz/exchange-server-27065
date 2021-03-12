using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005E5 RID: 1509
	public class LogonIdentifier
	{
		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x060018F8 RID: 6392 RVA: 0x0002FFDE File Offset: 0x0002E1DE
		// (set) Token: 0x060018F9 RID: 6393 RVA: 0x0002FFE6 File Offset: 0x0002E1E6
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x060018FA RID: 6394 RVA: 0x0002FFEF File Offset: 0x0002E1EF
		// (set) Token: 0x060018FB RID: 6395 RVA: 0x0002FFF7 File Offset: 0x0002E1F7
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

		// Token: 0x04001B53 RID: 6995
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _type;

		// Token: 0x04001B54 RID: 6996
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _value;
	}
}
