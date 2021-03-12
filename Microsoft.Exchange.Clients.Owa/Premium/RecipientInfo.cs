using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004C2 RID: 1218
	[OwaEventStruct("Rcp")]
	internal sealed class RecipientInfo
	{
		// Token: 0x06002E73 RID: 11891 RVA: 0x00109541 File Offset: 0x00107741
		public bool ToParticipant(out Participant participant)
		{
			return Utilities.CreateExchangeParticipant(out participant, this.DisplayName, this.RoutingAddress, this.RoutingType, this.AddressOrigin, this.StoreObjectId, this.EmailAddressIndex);
		}

		// Token: 0x06002E74 RID: 11892 RVA: 0x00109570 File Offset: 0x00107770
		public ProxyAddress ToProxyAddress()
		{
			string addressString = this.RoutingAddress ?? string.Empty;
			string prefixString = this.RoutingType ?? string.Empty;
			return ProxyAddress.Parse(prefixString, addressString);
		}

		// Token: 0x0400202F RID: 8239
		public const string StructNamespace = "Rcp";

		// Token: 0x04002030 RID: 8240
		public const string RoutingAddressName = "EM";

		// Token: 0x04002031 RID: 8241
		public const string DisplayNameName = "DN";

		// Token: 0x04002032 RID: 8242
		public const string RoutingTypeName = "RT";

		// Token: 0x04002033 RID: 8243
		public const string AddressOriginName = "AO";

		// Token: 0x04002034 RID: 8244
		public const string PendingChunkName = "PND";

		// Token: 0x04002035 RID: 8245
		public const string StoreObjectIdName = "ID";

		// Token: 0x04002036 RID: 8246
		public const string RecipientFlagsName = "RF";

		// Token: 0x04002037 RID: 8247
		public const string EmailAddressIndexName = "EI";

		// Token: 0x04002038 RID: 8248
		[OwaEventField("EM", true, "")]
		public string RoutingAddress;

		// Token: 0x04002039 RID: 8249
		[OwaEventField("DN", true, null)]
		public string DisplayName;

		// Token: 0x0400203A RID: 8250
		[OwaEventField("RT", true, null)]
		public string RoutingType;

		// Token: 0x0400203B RID: 8251
		[OwaEventField("AO", true, null)]
		public AddressOrigin AddressOrigin;

		// Token: 0x0400203C RID: 8252
		[OwaEventField("PND", true, null)]
		public string PendingChunk;

		// Token: 0x0400203D RID: 8253
		[OwaEventField("ID", true, null)]
		public StoreObjectId StoreObjectId;

		// Token: 0x0400203E RID: 8254
		[OwaEventField("RF", true, null)]
		public int RecipientFlags;

		// Token: 0x0400203F RID: 8255
		[OwaEventField("EI", true, null)]
		public EmailAddressIndex EmailAddressIndex;
	}
}
