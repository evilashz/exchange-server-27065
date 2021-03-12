using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000472 RID: 1138
	[DataContract]
	public class JunkEmailConfiguration : BaseRow
	{
		// Token: 0x06003959 RID: 14681 RVA: 0x000AE722 File Offset: 0x000AC922
		public JunkEmailConfiguration(MailboxJunkEmailConfiguration mailboxJunkEmailConfiguration) : base(mailboxJunkEmailConfiguration)
		{
			this.MailboxJunkEmailConfiguration = mailboxJunkEmailConfiguration;
		}

		// Token: 0x170022AD RID: 8877
		// (get) Token: 0x0600395A RID: 14682 RVA: 0x000AE732 File Offset: 0x000AC932
		// (set) Token: 0x0600395B RID: 14683 RVA: 0x000AE73A File Offset: 0x000AC93A
		public MailboxJunkEmailConfiguration MailboxJunkEmailConfiguration { get; private set; }

		// Token: 0x170022AE RID: 8878
		// (get) Token: 0x0600395C RID: 14684 RVA: 0x000AE743 File Offset: 0x000AC943
		// (set) Token: 0x0600395D RID: 14685 RVA: 0x000AE75B File Offset: 0x000AC95B
		[DataMember]
		public string Enabled
		{
			get
			{
				return this.MailboxJunkEmailConfiguration.Enabled.ToJsonString(null);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170022AF RID: 8879
		// (get) Token: 0x0600395E RID: 14686 RVA: 0x000AE762 File Offset: 0x000AC962
		// (set) Token: 0x0600395F RID: 14687 RVA: 0x000AE76F File Offset: 0x000AC96F
		[DataMember]
		public bool ContactsTrusted
		{
			get
			{
				return this.MailboxJunkEmailConfiguration.ContactsTrusted;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170022B0 RID: 8880
		// (get) Token: 0x06003960 RID: 14688 RVA: 0x000AE776 File Offset: 0x000AC976
		// (set) Token: 0x06003961 RID: 14689 RVA: 0x000AE783 File Offset: 0x000AC983
		[DataMember]
		public bool TrustedListsOnly
		{
			get
			{
				return this.MailboxJunkEmailConfiguration.TrustedListsOnly;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170022B1 RID: 8881
		// (get) Token: 0x06003962 RID: 14690 RVA: 0x000AE78A File Offset: 0x000AC98A
		// (set) Token: 0x06003963 RID: 14691 RVA: 0x000AE797 File Offset: 0x000AC997
		[DataMember]
		public IEnumerable<string> TrustedSendersAndDomains
		{
			get
			{
				return this.MailboxJunkEmailConfiguration.TrustedSendersAndDomains;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170022B2 RID: 8882
		// (get) Token: 0x06003964 RID: 14692 RVA: 0x000AE79E File Offset: 0x000AC99E
		// (set) Token: 0x06003965 RID: 14693 RVA: 0x000AE7AB File Offset: 0x000AC9AB
		[DataMember]
		public IEnumerable<string> BlockedSendersAndDomains
		{
			get
			{
				return this.MailboxJunkEmailConfiguration.BlockedSendersAndDomains;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}
	}
}
