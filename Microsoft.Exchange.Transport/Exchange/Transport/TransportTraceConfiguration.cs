using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000078 RID: 120
	internal class TransportTraceConfiguration : TraceConfigurationBase
	{
		// Token: 0x06000377 RID: 887 RVA: 0x0000F920 File Offset: 0x0000DB20
		public override void OnLoad()
		{
			this.filteredSubjects = TransportTraceConfiguration.GetFilterList(this.exTraceConfiguration, "Subject");
			this.filteredSenders = TransportTraceConfiguration.GetFilterList(this.exTraceConfiguration, "SenderSmtp");
			this.filteredRecipients = TransportTraceConfiguration.GetFilterList(this.exTraceConfiguration, "RecipientSmtp");
			this.filteredUsers = TransportTraceConfiguration.EncapsulateAddressList(TransportTraceConfiguration.GetFilterList(this.exTraceConfiguration, "UserDN"));
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000F98A File Offset: 0x0000DB8A
		public List<string> FilteredSubjects
		{
			get
			{
				return this.filteredSubjects;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000F992 File Offset: 0x0000DB92
		public List<string> FilteredSenders
		{
			get
			{
				return this.filteredSenders;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000F99A File Offset: 0x0000DB9A
		public List<string> FilteredRecipients
		{
			get
			{
				return this.filteredRecipients;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000F9A2 File Offset: 0x0000DBA2
		public List<string> FilteredUsers
		{
			get
			{
				return this.filteredUsers;
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000F9AC File Offset: 0x0000DBAC
		private static List<string> EncapsulateAddressList(List<string> legacyDNList)
		{
			List<string> list = new List<string>(legacyDNList.Count);
			foreach (string address in legacyDNList)
			{
				SmtpProxyAddress smtpProxyAddress;
				if (SmtpProxyAddress.TryEncapsulate("EX", address, Components.Configuration.FirstOrgAcceptedDomainTable.DefaultDomain.DomainName.Domain, out smtpProxyAddress))
				{
					list.Add(smtpProxyAddress.SmtpAddress);
				}
			}
			return list;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000FA34 File Offset: 0x0000DC34
		private static List<string> GetFilterList(ExTraceConfiguration configuration, string filterKey)
		{
			List<string> result;
			if (configuration.CustomParameters.TryGetValue(filterKey, out result))
			{
				return result;
			}
			return TransportTraceConfiguration.emptyList;
		}

		// Token: 0x040001EE RID: 494
		public const string SmtpSenderFilterKey = "SenderSmtp";

		// Token: 0x040001EF RID: 495
		public const string SmtpRecipientFilterKey = "RecipientSmtp";

		// Token: 0x040001F0 RID: 496
		public const string UserDNFilterKey = "UserDN";

		// Token: 0x040001F1 RID: 497
		public const string SubjectFilterKey = "Subject";

		// Token: 0x040001F2 RID: 498
		private static List<string> emptyList = new List<string>();

		// Token: 0x040001F3 RID: 499
		private List<string> filteredSubjects;

		// Token: 0x040001F4 RID: 500
		private List<string> filteredSenders;

		// Token: 0x040001F5 RID: 501
		private List<string> filteredRecipients;

		// Token: 0x040001F6 RID: 502
		private List<string> filteredUsers;
	}
}
