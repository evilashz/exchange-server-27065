using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001ED RID: 493
	[DataContract]
	public class SharingDomain
	{
		// Token: 0x17001BC8 RID: 7112
		// (get) Token: 0x06002613 RID: 9747 RVA: 0x000755A0 File Offset: 0x000737A0
		// (set) Token: 0x06002614 RID: 9748 RVA: 0x000755A8 File Offset: 0x000737A8
		[DataMember]
		public string DomainName { get; set; }

		// Token: 0x17001BC9 RID: 7113
		// (get) Token: 0x06002615 RID: 9749 RVA: 0x000755B4 File Offset: 0x000737B4
		// (set) Token: 0x06002616 RID: 9750 RVA: 0x00075602 File Offset: 0x00073802
		[DataMember]
		public string SharingStatusString
		{
			get
			{
				switch (this.SharingStatus)
				{
				case 0:
					return Strings.Succeeded;
				case 1:
					return Strings.Pending;
				case 2:
					return Strings.Failed;
				default:
					return string.Empty;
				}
			}
			set
			{
			}
		}

		// Token: 0x17001BCA RID: 7114
		// (get) Token: 0x06002617 RID: 9751 RVA: 0x00075604 File Offset: 0x00073804
		// (set) Token: 0x06002618 RID: 9752 RVA: 0x0007560C File Offset: 0x0007380C
		[DataMember]
		public int SharingStatus { get; set; }

		// Token: 0x17001BCB RID: 7115
		// (get) Token: 0x06002619 RID: 9753 RVA: 0x00075615 File Offset: 0x00073815
		// (set) Token: 0x0600261A RID: 9754 RVA: 0x0007561D File Offset: 0x0007381D
		[DataMember]
		public string Proof { get; set; }

		// Token: 0x17001BCC RID: 7116
		// (get) Token: 0x0600261B RID: 9755 RVA: 0x00075626 File Offset: 0x00073826
		// (set) Token: 0x0600261C RID: 9756 RVA: 0x0007562E File Offset: 0x0007382E
		[DataMember]
		public string RawIdentity { get; set; }

		// Token: 0x0600261D RID: 9757 RVA: 0x00075637 File Offset: 0x00073837
		public static explicit operator SmtpDomain(SharingDomain sharingDomain)
		{
			return new SmtpDomain(sharingDomain.DomainName);
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x00075644 File Offset: 0x00073844
		public static explicit operator SharingDomain(SmtpDomain smtpDomain)
		{
			return new SharingDomain
			{
				DomainName = smtpDomain.Domain,
				SharingStatus = 2
			};
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x0007566C File Offset: 0x0007386C
		public static explicit operator SharingDomain(FederatedDomain federatedDomain)
		{
			return new SharingDomain
			{
				DomainName = federatedDomain.Domain.Domain,
				SharingStatus = 0
			};
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x00075698 File Offset: 0x00073898
		public override string ToString()
		{
			return string.Format("SharingDomain:{0}", this.DomainName);
		}
	}
}
