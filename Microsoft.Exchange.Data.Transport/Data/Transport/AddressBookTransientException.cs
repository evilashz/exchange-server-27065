using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x0200004B RID: 75
	[Serializable]
	public class AddressBookTransientException : TransientException
	{
		// Token: 0x060001B9 RID: 441 RVA: 0x000063D0 File Offset: 0x000045D0
		public AddressBookTransientException(string message) : base(new LocalizedString(message))
		{
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000063DE File Offset: 0x000045DE
		public AddressBookTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000063E7 File Offset: 0x000045E7
		public AddressBookTransientException(string message, Exception innerException) : base(new LocalizedString(message), innerException)
		{
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000063F6 File Offset: 0x000045F6
		public AddressBookTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00006400 File Offset: 0x00004600
		protected AddressBookTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
