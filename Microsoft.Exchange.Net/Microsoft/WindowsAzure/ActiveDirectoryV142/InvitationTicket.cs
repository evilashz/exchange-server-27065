using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005E4 RID: 1508
	public class InvitationTicket
	{
		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x060018F3 RID: 6387 RVA: 0x0002FFB4 File Offset: 0x0002E1B4
		// (set) Token: 0x060018F4 RID: 6388 RVA: 0x0002FFBC File Offset: 0x0002E1BC
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x060018F5 RID: 6389 RVA: 0x0002FFC5 File Offset: 0x0002E1C5
		// (set) Token: 0x060018F6 RID: 6390 RVA: 0x0002FFCD File Offset: 0x0002E1CD
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string Ticket
		{
			get
			{
				return this._Ticket;
			}
			set
			{
				this._Ticket = value;
			}
		}

		// Token: 0x04001B51 RID: 6993
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _Type;

		// Token: 0x04001B52 RID: 6994
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _Ticket;
	}
}
