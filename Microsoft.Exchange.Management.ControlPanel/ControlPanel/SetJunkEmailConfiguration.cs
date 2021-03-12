using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000473 RID: 1139
	[DataContract]
	public class SetJunkEmailConfiguration : SetObjectProperties
	{
		// Token: 0x170022B3 RID: 8883
		// (get) Token: 0x06003966 RID: 14694 RVA: 0x000AE7B2 File Offset: 0x000AC9B2
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-MailboxJunkEmailConfiguration";
			}
		}

		// Token: 0x170022B4 RID: 8884
		// (get) Token: 0x06003967 RID: 14695 RVA: 0x000AE7B9 File Offset: 0x000AC9B9
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}

		// Token: 0x170022B5 RID: 8885
		// (get) Token: 0x06003968 RID: 14696 RVA: 0x000AE7C0 File Offset: 0x000AC9C0
		// (set) Token: 0x06003969 RID: 14697 RVA: 0x000AE7E8 File Offset: 0x000AC9E8
		[DataMember]
		public string Enabled
		{
			get
			{
				return ((bool)(base[MailboxJunkEmailConfigurationSchema.Enabled] ?? true)).ToJsonString(null);
			}
			set
			{
				bool flag;
				if (value != null && bool.TryParse(value, out flag))
				{
					base[MailboxJunkEmailConfigurationSchema.Enabled] = flag;
					return;
				}
				base[MailboxJunkEmailConfigurationSchema.Enabled] = false;
			}
		}

		// Token: 0x170022B6 RID: 8886
		// (get) Token: 0x0600396A RID: 14698 RVA: 0x000AE825 File Offset: 0x000ACA25
		// (set) Token: 0x0600396B RID: 14699 RVA: 0x000AE841 File Offset: 0x000ACA41
		[DataMember]
		public bool ContactsTrusted
		{
			get
			{
				return (bool)(base[MailboxJunkEmailConfigurationSchema.ContactsTrusted] ?? false);
			}
			set
			{
				base[MailboxJunkEmailConfigurationSchema.ContactsTrusted] = value;
			}
		}

		// Token: 0x170022B7 RID: 8887
		// (get) Token: 0x0600396C RID: 14700 RVA: 0x000AE854 File Offset: 0x000ACA54
		// (set) Token: 0x0600396D RID: 14701 RVA: 0x000AE870 File Offset: 0x000ACA70
		[DataMember]
		public bool TrustedListsOnly
		{
			get
			{
				return (bool)(base[MailboxJunkEmailConfigurationSchema.TrustedListsOnly] ?? false);
			}
			set
			{
				base[MailboxJunkEmailConfigurationSchema.TrustedListsOnly] = value;
			}
		}

		// Token: 0x170022B8 RID: 8888
		// (get) Token: 0x0600396E RID: 14702 RVA: 0x000AE883 File Offset: 0x000ACA83
		// (set) Token: 0x0600396F RID: 14703 RVA: 0x000AE89A File Offset: 0x000ACA9A
		[DataMember]
		public string[] TrustedSendersAndDomains
		{
			get
			{
				return (string[])(base[MailboxJunkEmailConfigurationSchema.TrustedSendersAndDomains] ?? null);
			}
			set
			{
				base[MailboxJunkEmailConfigurationSchema.TrustedSendersAndDomains] = value;
			}
		}

		// Token: 0x170022B9 RID: 8889
		// (get) Token: 0x06003970 RID: 14704 RVA: 0x000AE8A8 File Offset: 0x000ACAA8
		// (set) Token: 0x06003971 RID: 14705 RVA: 0x000AE8BF File Offset: 0x000ACABF
		[DataMember]
		public string[] BlockedSendersAndDomains
		{
			get
			{
				return (string[])(base[MailboxJunkEmailConfigurationSchema.BlockedSendersAndDomains] ?? null);
			}
			set
			{
				base[MailboxJunkEmailConfigurationSchema.BlockedSendersAndDomains] = value;
			}
		}
	}
}
