using System;
using Microsoft.Exchange.Nspi.Rfri;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x02000048 RID: 72
	internal class RfriException : Exception
	{
		// Token: 0x06000316 RID: 790 RVA: 0x0001385C File Offset: 0x00011A5C
		internal RfriException(RfriStatus status, string message) : base(message)
		{
			base.HResult = (int)status;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0001386C File Offset: 0x00011A6C
		internal RfriException(RfriStatus status, string message, Exception inner) : base(message, inner)
		{
			base.HResult = (int)status;
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0001387D File Offset: 0x00011A7D
		internal RfriStatus Status
		{
			get
			{
				return (RfriStatus)base.HResult;
			}
		}
	}
}
