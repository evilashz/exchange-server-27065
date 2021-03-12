using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Data.Transport.Partner;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Categorizer;

namespace Microsoft.Exchange.Transport.Delivery
{
	// Token: 0x020003C5 RID: 965
	internal class DeliveryAgentServer : ExtendedDeliveryAgentSmtpServer
	{
		// Token: 0x06002C3B RID: 11323 RVA: 0x000B0B4F File Offset: 0x000AED4F
		private DeliveryAgentServer(IReadOnlyMailItem currentMailItem, AcceptedDomainCollection acceptedDomains)
		{
			ArgumentValidator.ThrowIfNull("acceptedDomains", acceptedDomains);
			this.catServer = CatServer.GetInstance(currentMailItem, acceptedDomains);
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x000B0B6F File Offset: 0x000AED6F
		public static DeliveryAgentServer GetInstance(IReadOnlyMailItem currentMailItem, AcceptedDomainCollection acceptedDomains)
		{
			return new DeliveryAgentServer(currentMailItem, acceptedDomains);
		}

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x06002C3D RID: 11325 RVA: 0x000B0B78 File Offset: 0x000AED78
		public override string Name
		{
			get
			{
				return this.catServer.Name;
			}
		}

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x06002C3E RID: 11326 RVA: 0x000B0B85 File Offset: 0x000AED85
		public override Version Version
		{
			get
			{
				return this.catServer.Version;
			}
		}

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x06002C3F RID: 11327 RVA: 0x000B0B92 File Offset: 0x000AED92
		public override IPPermission IPPermission
		{
			get
			{
				return this.catServer.IPPermission;
			}
		}

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x06002C40 RID: 11328 RVA: 0x000B0B9F File Offset: 0x000AED9F
		public override AddressBook AddressBook
		{
			get
			{
				return this.catServer.AddressBook;
			}
		}

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x06002C41 RID: 11329 RVA: 0x000B0BAC File Offset: 0x000AEDAC
		public override AcceptedDomainCollection AcceptedDomains
		{
			get
			{
				return this.catServer.AcceptedDomains;
			}
		}

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x06002C42 RID: 11330 RVA: 0x000B0BB9 File Offset: 0x000AEDB9
		public override RemoteDomainCollection RemoteDomains
		{
			get
			{
				return this.catServer.RemoteDomains;
			}
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x000B0BC6 File Offset: 0x000AEDC6
		public override void SubmitMessage(EmailMessage message)
		{
			this.catServer.SubmitMessage(message);
		}

		// Token: 0x04001632 RID: 5682
		private readonly CatServer catServer;
	}
}
