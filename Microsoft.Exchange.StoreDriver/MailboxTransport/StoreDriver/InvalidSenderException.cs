using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxTransport.StoreDriver
{
	// Token: 0x02000002 RID: 2
	[Serializable]
	internal class InvalidSenderException : StoragePermanentException
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public InvalidSenderException(Participant participant) : base(LocalizedString.Empty)
		{
			if (participant != null)
			{
				this.address = participant.EmailAddress;
				this.addressType = participant.RoutingType;
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020FE File Offset: 0x000002FE
		public InvalidSenderException(ProxyAddress proxyAddress) : base(LocalizedString.Empty)
		{
			if (proxyAddress != null)
			{
				this.address = proxyAddress.AddressString;
				this.addressType = proxyAddress.PrefixString;
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000212C File Offset: 0x0000032C
		public string Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002134 File Offset: 0x00000334
		public string AddressType
		{
			get
			{
				return this.addressType;
			}
		}

		// Token: 0x04000001 RID: 1
		private string address;

		// Token: 0x04000002 RID: 2
		private string addressType;
	}
}
