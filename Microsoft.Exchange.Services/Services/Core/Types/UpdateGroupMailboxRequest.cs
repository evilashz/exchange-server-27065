using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000496 RID: 1174
	[XmlType(TypeName = "UpdateGroupMailboxType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class UpdateGroupMailboxRequest : BaseRequest
	{
		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060022FA RID: 8954 RVA: 0x000A3754 File Offset: 0x000A1954
		// (set) Token: 0x060022FB RID: 8955 RVA: 0x000A375C File Offset: 0x000A195C
		[XmlElement("GroupSmtpAddress")]
		[DataMember(Name = "GroupSmtpAddress", IsRequired = true)]
		public string GroupSmtpAddress { get; set; }

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060022FC RID: 8956 RVA: 0x000A3765 File Offset: 0x000A1965
		// (set) Token: 0x060022FD RID: 8957 RVA: 0x000A376D File Offset: 0x000A196D
		[XmlElement("ExecutingUserSmtpAddress")]
		[DataMember(Name = "ExecutingUserSmtpAddress", IsRequired = false)]
		public string ExecutingUserSmtpAddress { get; set; }

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060022FE RID: 8958 RVA: 0x000A3776 File Offset: 0x000A1976
		// (set) Token: 0x060022FF RID: 8959 RVA: 0x000A377E File Offset: 0x000A197E
		[DataMember(Name = "DomainController", IsRequired = false)]
		[XmlElement("DomainController")]
		public string DomainController { get; set; }

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06002300 RID: 8960 RVA: 0x000A3787 File Offset: 0x000A1987
		// (set) Token: 0x06002301 RID: 8961 RVA: 0x000A378F File Offset: 0x000A198F
		[DataMember(Name = "ForceConfigurationAction", IsRequired = false)]
		[XmlElement("ForceConfigurationAction")]
		public GroupMailboxConfigurationAction ForceConfigurationAction { get; set; }

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06002302 RID: 8962 RVA: 0x000A3798 File Offset: 0x000A1998
		// (set) Token: 0x06002303 RID: 8963 RVA: 0x000A37A0 File Offset: 0x000A19A0
		[DataMember(Name = "MemberIdentifierType", IsRequired = false)]
		[XmlElement("MemberIdentifierType")]
		public GroupMemberIdentifierType? MemberIdentifierType { get; set; }

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06002304 RID: 8964 RVA: 0x000A37A9 File Offset: 0x000A19A9
		// (set) Token: 0x06002305 RID: 8965 RVA: 0x000A37B1 File Offset: 0x000A19B1
		[XmlArrayItem("String", typeof(string), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = true)]
		[DataMember(Name = "AddedMembers", IsRequired = false)]
		[XmlArray("AddedMembers")]
		public string[] AddedMembers { get; set; }

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06002306 RID: 8966 RVA: 0x000A37BA File Offset: 0x000A19BA
		// (set) Token: 0x06002307 RID: 8967 RVA: 0x000A37C2 File Offset: 0x000A19C2
		[DataMember(Name = "RemovedMembers", IsRequired = false)]
		[XmlArray("RemovedMembers")]
		[XmlArrayItem("String", typeof(string), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = true)]
		public string[] RemovedMembers { get; set; }

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06002308 RID: 8968 RVA: 0x000A37CB File Offset: 0x000A19CB
		// (set) Token: 0x06002309 RID: 8969 RVA: 0x000A37D3 File Offset: 0x000A19D3
		[DataMember(Name = "AddedPendingMembers", IsRequired = false)]
		[XmlArrayItem("String", typeof(string), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = true)]
		[XmlArray("AddedPendingMembers")]
		public string[] AddedPendingMembers { get; set; }

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x0600230A RID: 8970 RVA: 0x000A37DC File Offset: 0x000A19DC
		// (set) Token: 0x0600230B RID: 8971 RVA: 0x000A37E4 File Offset: 0x000A19E4
		[DataMember(Name = "RemovedPendingMembers", IsRequired = false)]
		[XmlArray("RemovedPendingMembers")]
		[XmlArrayItem("String", typeof(string), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = true)]
		public string[] RemovedPendingMembers { get; set; }

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x0600230C RID: 8972 RVA: 0x000A37ED File Offset: 0x000A19ED
		// (set) Token: 0x0600230D RID: 8973 RVA: 0x000A37F5 File Offset: 0x000A19F5
		[XmlElement("PermissionsVersion")]
		[DataMember(Name = "PermissionsVersion", IsRequired = false)]
		public int? PermissionsVersion { get; set; }

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x0600230E RID: 8974 RVA: 0x000A37FE File Offset: 0x000A19FE
		// (set) Token: 0x0600230F RID: 8975 RVA: 0x000A3806 File Offset: 0x000A1A06
		internal SmtpAddress GroupPrimarySmtpAddress { get; set; }

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06002310 RID: 8976 RVA: 0x000A380F File Offset: 0x000A1A0F
		internal string DomainControllerOrNull
		{
			get
			{
				if (string.IsNullOrWhiteSpace(this.DomainController))
				{
					return null;
				}
				return this.DomainController;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06002311 RID: 8977 RVA: 0x000A3826 File Offset: 0x000A1A26
		internal bool IsAddedMembersSpecified
		{
			get
			{
				return this.AddedMembers != null && this.AddedMembers.Length > 0;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06002312 RID: 8978 RVA: 0x000A383D File Offset: 0x000A1A3D
		internal bool IsRemovedMembersSpecified
		{
			get
			{
				return this.RemovedMembers != null && this.RemovedMembers.Length > 0;
			}
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x000A3854 File Offset: 0x000A1A54
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new UpdateGroupMailbox(callContext, this);
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x000A385D File Offset: 0x000A1A5D
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return IdConverter.GetServerInfoForCallContext(callContext);
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x000A3865 File Offset: 0x000A1A65
		internal override ResourceKey[] GetResources(CallContext callContext, int currentStep)
		{
			return base.GetResourceKeysFromProxyInfo(true, callContext);
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x000A3870 File Offset: 0x000A1A70
		internal override void Validate()
		{
			base.Validate();
			SmtpAddress groupPrimarySmtpAddress = new SmtpAddress(this.GroupSmtpAddress);
			if (!groupPrimarySmtpAddress.IsValidAddress)
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidSmtpAddressException(new LocalizedException(ServerStrings.InvalidSmtpAddress(this.GroupSmtpAddress))), FaultParty.Sender);
			}
			this.GroupPrimarySmtpAddress = groupPrimarySmtpAddress;
		}
	}
}
