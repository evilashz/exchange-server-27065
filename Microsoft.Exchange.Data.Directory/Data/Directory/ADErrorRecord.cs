using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000028 RID: 40
	[Serializable]
	internal class ADErrorRecord
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000EC1D File Offset: 0x0000CE1D
		// (set) Token: 0x0600028A RID: 650 RVA: 0x0000EC25 File Offset: 0x0000CE25
		internal HandlingType HandlingType
		{
			get
			{
				return this.handlingType;
			}
			set
			{
				this.handlingType = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000EC2E File Offset: 0x0000CE2E
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0000EC36 File Offset: 0x0000CE36
		internal LdapError LdapError
		{
			get
			{
				return this.ldapError;
			}
			set
			{
				this.ldapError = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000EC3F File Offset: 0x0000CE3F
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000EC47 File Offset: 0x0000CE47
		internal string Message
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000EC50 File Offset: 0x0000CE50
		// (set) Token: 0x06000290 RID: 656 RVA: 0x0000EC58 File Offset: 0x0000CE58
		internal int NativeError
		{
			get
			{
				return this.nativeError;
			}
			set
			{
				this.nativeError = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000EC61 File Offset: 0x0000CE61
		// (set) Token: 0x06000292 RID: 658 RVA: 0x0000EC69 File Offset: 0x0000CE69
		internal int JetError
		{
			get
			{
				return this.jetError;
			}
			set
			{
				this.jetError = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000EC72 File Offset: 0x0000CE72
		// (set) Token: 0x06000294 RID: 660 RVA: 0x0000EC7A File Offset: 0x0000CE7A
		internal bool IsDownError
		{
			get
			{
				return this.isDownError;
			}
			set
			{
				this.isDownError = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000EC83 File Offset: 0x0000CE83
		// (set) Token: 0x06000296 RID: 662 RVA: 0x0000EC8B File Offset: 0x0000CE8B
		internal bool IsFatalError
		{
			get
			{
				return this.isFatalError;
			}
			set
			{
				this.isFatalError = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000EC94 File Offset: 0x0000CE94
		// (set) Token: 0x06000298 RID: 664 RVA: 0x0000EC9C File Offset: 0x0000CE9C
		internal bool IsSearchError
		{
			get
			{
				return this.isSearchError;
			}
			set
			{
				this.isSearchError = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000ECA5 File Offset: 0x0000CEA5
		// (set) Token: 0x0600029A RID: 666 RVA: 0x0000ECAD File Offset: 0x0000CEAD
		internal bool IsTimeoutError
		{
			get
			{
				return this.isTimeoutError;
			}
			set
			{
				this.isTimeoutError = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000ECB6 File Offset: 0x0000CEB6
		// (set) Token: 0x0600029C RID: 668 RVA: 0x0000ECBE File Offset: 0x0000CEBE
		internal bool IsServerSideTimeoutError
		{
			get
			{
				return this.isServerSideTimeoutError;
			}
			set
			{
				this.isServerSideTimeoutError = value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000ECC7 File Offset: 0x0000CEC7
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000ECCF File Offset: 0x0000CECF
		internal bool IsModificationError
		{
			get
			{
				return this.isModificationError;
			}
			set
			{
				this.isModificationError = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000ECD8 File Offset: 0x0000CED8
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000ECE0 File Offset: 0x0000CEE0
		internal Exception InnerException
		{
			get
			{
				return this.innerException;
			}
			set
			{
				this.innerException = value;
			}
		}

		// Token: 0x0400009B RID: 155
		private HandlingType handlingType;

		// Token: 0x0400009C RID: 156
		private LdapError ldapError;

		// Token: 0x0400009D RID: 157
		private string message = string.Empty;

		// Token: 0x0400009E RID: 158
		private int nativeError;

		// Token: 0x0400009F RID: 159
		private int jetError;

		// Token: 0x040000A0 RID: 160
		private bool isDownError;

		// Token: 0x040000A1 RID: 161
		private bool isFatalError;

		// Token: 0x040000A2 RID: 162
		private bool isSearchError;

		// Token: 0x040000A3 RID: 163
		private bool isTimeoutError;

		// Token: 0x040000A4 RID: 164
		private bool isServerSideTimeoutError;

		// Token: 0x040000A5 RID: 165
		private bool isModificationError;

		// Token: 0x040000A6 RID: 166
		private Exception innerException;
	}
}
