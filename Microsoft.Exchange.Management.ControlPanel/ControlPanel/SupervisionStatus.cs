using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Supervision;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200048C RID: 1164
	[DataContract]
	public class SupervisionStatus : BaseRow
	{
		// Token: 0x06003A23 RID: 14883 RVA: 0x000B03AE File Offset: 0x000AE5AE
		public SupervisionStatus(SupervisionPolicy supervisionStatus) : base(null, supervisionStatus)
		{
			this.MySupervisionStatus = supervisionStatus;
		}

		// Token: 0x170022F8 RID: 8952
		// (get) Token: 0x06003A24 RID: 14884 RVA: 0x000B03BF File Offset: 0x000AE5BF
		// (set) Token: 0x06003A25 RID: 14885 RVA: 0x000B03DB File Offset: 0x000AE5DB
		[DataMember]
		public bool ClosedCampusPolicyEnabled
		{
			get
			{
				return this.MySupervisionStatus.ClosedCampusInboundPolicyEnabled && this.MySupervisionStatus.ClosedCampusOutboundPolicyEnabled;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170022F9 RID: 8953
		// (get) Token: 0x06003A26 RID: 14886 RVA: 0x000B03E2 File Offset: 0x000AE5E2
		// (set) Token: 0x06003A27 RID: 14887 RVA: 0x000B040D File Offset: 0x000AE60D
		[DataMember]
		public IEnumerable<RecipientObjectResolverRow> ClosedCampusInboundGroupExceptions
		{
			get
			{
				if (this.MySupervisionStatus.ClosedCampusInboundPolicyGroupExceptions == null)
				{
					return null;
				}
				return RecipientObjectResolver.Instance.ResolveSmtpAddress(this.MySupervisionStatus.ClosedCampusInboundPolicyGroupExceptions.ToArray());
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170022FA RID: 8954
		// (get) Token: 0x06003A28 RID: 14888 RVA: 0x000B0414 File Offset: 0x000AE614
		// (set) Token: 0x06003A29 RID: 14889 RVA: 0x000B0435 File Offset: 0x000AE635
		[DataMember]
		public string[] ClosedCampusInboundDomainExceptions
		{
			get
			{
				if (this.MySupervisionStatus.ClosedCampusInboundPolicyDomainExceptions == null)
				{
					return null;
				}
				return this.MySupervisionStatus.ClosedCampusInboundPolicyDomainExceptions.ToStringArray<SmtpDomain>();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170022FB RID: 8955
		// (get) Token: 0x06003A2A RID: 14890 RVA: 0x000B043C File Offset: 0x000AE63C
		// (set) Token: 0x06003A2B RID: 14891 RVA: 0x000B0449 File Offset: 0x000AE649
		[DataMember]
		public bool BadWordsPolicyEnabled
		{
			get
			{
				return this.MySupervisionStatus.BadWordsPolicyEnabled;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170022FC RID: 8956
		// (get) Token: 0x06003A2C RID: 14892 RVA: 0x000B0450 File Offset: 0x000AE650
		// (set) Token: 0x06003A2D RID: 14893 RVA: 0x000B0470 File Offset: 0x000AE670
		[DataMember]
		public string BadWordsList
		{
			get
			{
				if (this.MySupervisionStatus.BadWordsList != null)
				{
					return this.MySupervisionStatus.BadWordsList;
				}
				return string.Empty;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170022FD RID: 8957
		// (get) Token: 0x06003A2E RID: 14894 RVA: 0x000B0477 File Offset: 0x000AE677
		// (set) Token: 0x06003A2F RID: 14895 RVA: 0x000B0484 File Offset: 0x000AE684
		[DataMember]
		public bool AntiBullyingPolicyEnabled
		{
			get
			{
				return this.MySupervisionStatus.AntiBullyingPolicyEnabled;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x040026E6 RID: 9958
		private readonly SupervisionPolicy MySupervisionStatus;
	}
}
