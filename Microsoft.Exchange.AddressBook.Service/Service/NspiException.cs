using System;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x02000033 RID: 51
	internal class NspiException : Exception
	{
		// Token: 0x06000205 RID: 517 RVA: 0x0000E31B File Offset: 0x0000C51B
		internal NspiException(NspiStatus status, string message) : base(message)
		{
			base.HResult = (int)status;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000E32B File Offset: 0x0000C52B
		internal NspiException(NspiStatus status, string message, Exception inner) : base(message, inner)
		{
			base.HResult = (int)status;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000E33C File Offset: 0x0000C53C
		internal NspiStatus Status
		{
			get
			{
				return (NspiStatus)base.HResult;
			}
		}
	}
}
