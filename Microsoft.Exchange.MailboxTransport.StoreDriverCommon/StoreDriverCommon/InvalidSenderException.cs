using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverCommon
{
	// Token: 0x02000006 RID: 6
	[Serializable]
	internal class InvalidSenderException : StoragePermanentException
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000022EA File Offset: 0x000004EA
		public InvalidSenderException(Participant participant) : base(LocalizedString.Empty)
		{
			if (participant != null)
			{
				this.address = participant.EmailAddress;
				this.addressType = participant.RoutingType;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002318 File Offset: 0x00000518
		public InvalidSenderException(ProxyAddress proxyAddress) : base(LocalizedString.Empty)
		{
			if (proxyAddress != null)
			{
				this.address = proxyAddress.AddressString;
				this.addressType = proxyAddress.PrefixString;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002346 File Offset: 0x00000546
		public string Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000234E File Offset: 0x0000054E
		public string AddressType
		{
			get
			{
				return this.addressType;
			}
		}

		// Token: 0x04000005 RID: 5
		private readonly string address;

		// Token: 0x04000006 RID: 6
		private readonly string addressType;
	}
}
