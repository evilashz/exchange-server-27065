using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000016 RID: 22
	internal class SidAnchorMailbox : UserBasedAnchorMailbox
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x00004AD0 File Offset: 0x00002CD0
		public SidAnchorMailbox(SecurityIdentifier sid, IRequestContext requestContext) : base(AnchorSource.Sid, sid, requestContext)
		{
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004ADB File Offset: 0x00002CDB
		public SidAnchorMailbox(string sid, IRequestContext requestContext) : this(new SecurityIdentifier(sid), requestContext)
		{
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00004AEA File Offset: 0x00002CEA
		public SecurityIdentifier Sid
		{
			get
			{
				return (SecurityIdentifier)base.SourceObject;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00004AF7 File Offset: 0x00002CF7
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00004AFF File Offset: 0x00002CFF
		public OrganizationId OrganizationId { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00004B08 File Offset: 0x00002D08
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00004B10 File Offset: 0x00002D10
		public string SmtpOrLiveId { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00004B19 File Offset: 0x00002D19
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00004B21 File Offset: 0x00002D21
		public string PartitionId { get; set; }

		// Token: 0x060000AA RID: 170 RVA: 0x00004B2A File Offset: 0x00002D2A
		public override string GetOrganizationNameForLogging()
		{
			if (this.OrganizationId != null)
			{
				return this.OrganizationId.GetFriendlyName();
			}
			return base.GetOrganizationNameForLogging();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004B78 File Offset: 0x00002D78
		protected override ADRawEntry LoadADRawEntry()
		{
			IRecipientSession session = null;
			if (this.OrganizationId != null)
			{
				session = DirectoryHelper.GetRecipientSessionFromOrganizationId(base.RequestContext.LatencyTracker, this.OrganizationId);
			}
			else if (this.PartitionId != null)
			{
				session = DirectoryHelper.GetRecipientSessionFromPartition(base.RequestContext.LatencyTracker, this.PartitionId);
			}
			else if (this.SmtpOrLiveId != null)
			{
				session = DirectoryHelper.GetRecipientSessionFromSmtpOrLiveId(base.RequestContext.LatencyTracker, this.SmtpOrLiveId, false);
			}
			else
			{
				session = DirectoryHelper.GetRootOrgRecipientSession();
			}
			ADRawEntry ret = DirectoryHelper.InvokeAccountForest(base.RequestContext.LatencyTracker, () => session.FindADRawEntryBySid(this.Sid, this.PropertySet));
			return base.CheckForNullAndThrowIfApplicable<ADRawEntry>(ret);
		}
	}
}
