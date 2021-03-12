using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x020000AB RID: 171
	[DataContract(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public sealed class OrganizationRelationshipSettings
	{
		// Token: 0x06000417 RID: 1047 RVA: 0x00017CFC File Offset: 0x00015EFC
		public OrganizationRelationshipSettings()
		{
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00017D04 File Offset: 0x00015F04
		internal OrganizationRelationshipSettings(OrganizationRelationship organizationRelationship)
		{
			if (organizationRelationship.DomainNames != null)
			{
				this.DomainNames = new DomainCollection();
				foreach (SmtpDomain smtpDomain in organizationRelationship.DomainNames)
				{
					this.DomainNames.Add(smtpDomain.Domain);
				}
			}
			this.Name = organizationRelationship.Name;
			this.TargetApplicationUri = organizationRelationship.TargetApplicationUri;
			this.FreeBusyAccessLevel = Enum.GetName(typeof(FreeBusyAccessLevel), organizationRelationship.FreeBusyAccessLevel);
			this.FreeBusyAccessEnabled = organizationRelationship.FreeBusyAccessEnabled;
			this.TargetSharingEpr = organizationRelationship.TargetSharingEpr;
			this.TargetAutodiscoverEpr = organizationRelationship.TargetAutodiscoverEpr;
			this.MailboxMoveEnabled = organizationRelationship.MailboxMoveEnabled;
			this.DeliveryReportEnabled = organizationRelationship.DeliveryReportEnabled;
			this.MailTipsAccessEnabled = organizationRelationship.MailTipsAccessEnabled;
			this.MailTipsAccessLevel = Enum.GetName(typeof(MailTipsAccessLevel), organizationRelationship.MailTipsAccessLevel);
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x00017E1C File Offset: 0x0001601C
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x00017E24 File Offset: 0x00016024
		[DataMember(IsRequired = true)]
		public string Name { get; set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x00017E2D File Offset: 0x0001602D
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x00017E35 File Offset: 0x00016035
		[DataMember(IsRequired = true)]
		public DomainCollection DomainNames { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x00017E3E File Offset: 0x0001603E
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x00017E46 File Offset: 0x00016046
		[DataMember(IsRequired = true)]
		public Uri TargetApplicationUri { get; set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x00017E4F File Offset: 0x0001604F
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x00017E57 File Offset: 0x00016057
		[DataMember(IsRequired = true)]
		public string FreeBusyAccessLevel { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00017E60 File Offset: 0x00016060
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x00017E68 File Offset: 0x00016068
		[DataMember(IsRequired = true)]
		public bool FreeBusyAccessEnabled { get; set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x00017E71 File Offset: 0x00016071
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x00017E79 File Offset: 0x00016079
		[DataMember(IsRequired = true)]
		public Uri TargetSharingEpr { get; set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x00017E82 File Offset: 0x00016082
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x00017E8A File Offset: 0x0001608A
		[DataMember(IsRequired = true)]
		public Uri TargetAutodiscoverEpr { get; set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x00017E93 File Offset: 0x00016093
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x00017E9B File Offset: 0x0001609B
		[DataMember(IsRequired = true)]
		public bool MailboxMoveEnabled { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00017EA4 File Offset: 0x000160A4
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x00017EAC File Offset: 0x000160AC
		[DataMember(IsRequired = true)]
		public bool DeliveryReportEnabled { get; set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00017EB5 File Offset: 0x000160B5
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x00017EBD File Offset: 0x000160BD
		[DataMember(IsRequired = true)]
		public bool MailTipsAccessEnabled { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00017EC6 File Offset: 0x000160C6
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x00017ECE File Offset: 0x000160CE
		[DataMember(IsRequired = true)]
		public string MailTipsAccessLevel { get; set; }
	}
}
