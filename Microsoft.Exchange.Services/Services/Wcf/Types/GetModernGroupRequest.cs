using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009E6 RID: 2534
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetModernGroupRequest : BaseRequest
	{
		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x0600477B RID: 18299 RVA: 0x00100475 File Offset: 0x000FE675
		// (set) Token: 0x0600477C RID: 18300 RVA: 0x0010047D File Offset: 0x000FE67D
		[DataMember(Name = "SmtpAddress", IsRequired = true)]
		public string SmtpAddress { get; set; }

		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x0600477D RID: 18301 RVA: 0x00100486 File Offset: 0x000FE686
		// (set) Token: 0x0600477E RID: 18302 RVA: 0x0010048E File Offset: 0x000FE68E
		[DataMember(Name = "ResultSet", IsRequired = true)]
		public ModernGroupRequestResultSet ResultSet { get; set; }

		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x0600477F RID: 18303 RVA: 0x00100497 File Offset: 0x000FE697
		// (set) Token: 0x06004780 RID: 18304 RVA: 0x0010049F File Offset: 0x000FE69F
		[DataMember(Name = "MemberSortOrder")]
		public ModernGroupMembersSortOrder MemberSortOrder { get; set; }

		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x06004781 RID: 18305 RVA: 0x001004A8 File Offset: 0x000FE6A8
		internal ProxyAddress ProxyAddress
		{
			get
			{
				return new SmtpProxyAddress(this.SmtpAddress, true);
			}
		}

		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x06004782 RID: 18306 RVA: 0x001004B6 File Offset: 0x000FE6B6
		// (set) Token: 0x06004783 RID: 18307 RVA: 0x001004BE File Offset: 0x000FE6BE
		internal bool IsMemberRequested { get; private set; }

		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x06004784 RID: 18308 RVA: 0x001004C7 File Offset: 0x000FE6C7
		// (set) Token: 0x06004785 RID: 18309 RVA: 0x001004CF File Offset: 0x000FE6CF
		internal bool IsOwnerListRequested { get; private set; }

		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x06004786 RID: 18310 RVA: 0x001004D8 File Offset: 0x000FE6D8
		// (set) Token: 0x06004787 RID: 18311 RVA: 0x001004E0 File Offset: 0x000FE6E0
		internal bool IsGeneralInfoRequested { get; private set; }

		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x06004788 RID: 18312 RVA: 0x001004E9 File Offset: 0x000FE6E9
		// (set) Token: 0x06004789 RID: 18313 RVA: 0x001004F1 File Offset: 0x000FE6F1
		internal bool IsExternalResourcesRequested { get; private set; }

		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x0600478A RID: 18314 RVA: 0x001004FA File Offset: 0x000FE6FA
		// (set) Token: 0x0600478B RID: 18315 RVA: 0x00100502 File Offset: 0x000FE702
		internal bool IsMailboxInfoRequested { get; private set; }

		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x0600478C RID: 18316 RVA: 0x0010050B File Offset: 0x000FE70B
		// (set) Token: 0x0600478D RID: 18317 RVA: 0x00100513 File Offset: 0x000FE713
		internal bool IsForceReloadRequested { get; private set; }

		// Token: 0x0600478E RID: 18318 RVA: 0x0010051C File Offset: 0x000FE71C
		internal void ValidateRequest()
		{
			if (string.IsNullOrEmpty(this.SmtpAddress) || !Microsoft.Exchange.Data.SmtpAddress.IsValidSmtpAddress(this.SmtpAddress))
			{
				ExTraceGlobals.ModernGroupsTracer.TraceDebug<string>((long)this.GetHashCode(), "Invalid smtp address {0}", this.SmtpAddress);
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(), FaultParty.Sender);
			}
			this.IsGeneralInfoRequested = ((this.ResultSet & ModernGroupRequestResultSet.General) == ModernGroupRequestResultSet.General);
			this.IsMemberRequested = ((this.ResultSet & ModernGroupRequestResultSet.Members) == ModernGroupRequestResultSet.Members);
			this.IsOwnerListRequested = ((this.ResultSet & ModernGroupRequestResultSet.Owners) == ModernGroupRequestResultSet.Owners);
			this.IsExternalResourcesRequested = ((this.ResultSet & ModernGroupRequestResultSet.ExternalResources) == ModernGroupRequestResultSet.ExternalResources);
			this.IsMailboxInfoRequested = ((this.ResultSet & ModernGroupRequestResultSet.GroupMailboxProperties) == ModernGroupRequestResultSet.GroupMailboxProperties);
			this.IsForceReloadRequested = ((this.ResultSet & ModernGroupRequestResultSet.ForceReload) == ModernGroupRequestResultSet.ForceReload);
			if (this.IsMemberRequested && this.MembersPageRequest == null)
			{
				ExTraceGlobals.ModernGroupsTracer.TraceDebug((long)this.GetHashCode(), "Members requested but paging information was missing");
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(), FaultParty.Sender);
			}
		}

		// Token: 0x0600478F RID: 18319 RVA: 0x0010060D File Offset: 0x000FE80D
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}

		// Token: 0x06004790 RID: 18320 RVA: 0x00100617 File Offset: 0x000FE817
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x04002904 RID: 10500
		[DataMember(Name = "MembersPageRequest", IsRequired = false)]
		public IndexedPageView MembersPageRequest;

		// Token: 0x04002905 RID: 10501
		[DataMember(Name = "SerializedPeopleIKnowGraph")]
		public string SerializedPeopleIKnowGraph;
	}
}
