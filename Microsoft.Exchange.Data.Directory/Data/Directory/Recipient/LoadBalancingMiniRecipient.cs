using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000251 RID: 593
	[Serializable]
	public class LoadBalancingMiniRecipient : MiniRecipient
	{
		// Token: 0x06001D14 RID: 7444 RVA: 0x00078D76 File Offset: 0x00076F76
		internal LoadBalancingMiniRecipient(IRecipientSession session, PropertyBag propertyBag) : base(session, propertyBag)
		{
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x00078D80 File Offset: 0x00076F80
		public LoadBalancingMiniRecipient()
		{
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001D16 RID: 7446 RVA: 0x00078D88 File Offset: 0x00076F88
		public UserConfigXML ConfigXML
		{
			get
			{
				return (UserConfigXML)this[MiniRecipientSchema.ConfigurationXML];
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001D17 RID: 7447 RVA: 0x00078D9A File Offset: 0x00076F9A
		public string MailboxMoveBatchName
		{
			get
			{
				return (string)this[LoadBalancingMiniRecipientSchema.MailboxMoveBatchName];
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001D18 RID: 7448 RVA: 0x00078DAC File Offset: 0x00076FAC
		public RequestStatus MailboxMoveStatus
		{
			get
			{
				return (RequestStatus)this[LoadBalancingMiniRecipientSchema.MailboxMoveStatus];
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001D19 RID: 7449 RVA: 0x00078DBE File Offset: 0x00076FBE
		public RequestFlags MailboxMoveFlags
		{
			get
			{
				return (RequestFlags)this[LoadBalancingMiniRecipientSchema.MailboxMoveFlags];
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001D1A RID: 7450 RVA: 0x00078DD0 File Offset: 0x00076FD0
		internal override ADObjectSchema Schema
		{
			get
			{
				return LoadBalancingMiniRecipient.schema;
			}
		}

		// Token: 0x04000DCE RID: 3534
		private static readonly LoadBalancingMiniRecipientSchema schema = ObjectSchema.GetInstance<LoadBalancingMiniRecipientSchema>();
	}
}
