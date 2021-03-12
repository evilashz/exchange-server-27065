using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Categorizer;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x0200000E RID: 14
	internal interface IDeliveryItem
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600013F RID: 319
		bool HasMessage { get; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000140 RID: 320
		MessageItem Message { get; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000141 RID: 321
		StoreSession Session { get; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000142 RID: 322
		// (set) Token: 0x06000143 RID: 323
		StoreId DeliverToFolder { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000144 RID: 324
		MailboxSession MailboxSession { get; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000145 RID: 325
		PublicFolderSession PublicFolderSession { get; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000146 RID: 326
		DisposeTracker DisposeTracker { get; }

		// Token: 0x06000147 RID: 327
		void Deliver(ProxyAddress recipientProxyAddress);

		// Token: 0x06000148 RID: 328
		void SetProperty(PropertyDefinition property, object value);

		// Token: 0x06000149 RID: 329
		void DeleteProperty(PropertyDefinition property);

		// Token: 0x0600014A RID: 330
		Stream OpenPropertyStream(PropertyDefinition propertyDefinition, PropertyOpenMode openMode);

		// Token: 0x0600014B RID: 331
		void CreatePublicFolderMessage(MailRecipient recipient, DeliverableItem item);

		// Token: 0x0600014C RID: 332
		void CreateSession(MailRecipient recipient, OpenTransportSessionFlags deliveryFlags, DeliverableItem item, ICollection<CultureInfo> recipientLanguages);

		// Token: 0x0600014D RID: 333
		void CreateMailboxMessage(bool leaveReceivedTime);

		// Token: 0x0600014E RID: 334
		void LoadMailboxMessage(string internetMessageId);

		// Token: 0x0600014F RID: 335
		void DisposeMessageAndSession();

		// Token: 0x06000150 RID: 336
		void Dispose();
	}
}
