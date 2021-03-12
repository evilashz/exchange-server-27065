using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x0200009D RID: 157
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlInclude(typeof(DirectorySearch))]
	[WebServiceBinding(Name = "CompanyManagerProviderSoap", Namespace = "http://www.ccs.com/TestServices/")]
	[XmlInclude(typeof(DirectoryReference))]
	[XmlInclude(typeof(DirectoryProperty))]
	[DesignerCategory("code")]
	[XmlInclude(typeof(CompanyDomainValue))]
	[XmlInclude(typeof(DirectoryObject))]
	[DebuggerStepThrough]
	public class CompanyManagerProvider : SoapHttpClientProtocol
	{
		// Token: 0x06000437 RID: 1079 RVA: 0x0001A9DD File Offset: 0x00018BDD
		internal CompanyManagerProvider()
		{
			base.Url = "https://remotetestservice.msol-test.com/CompanyManagerProvider.asmx";
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000438 RID: 1080 RVA: 0x0001A9F0 File Offset: 0x00018BF0
		// (remove) Token: 0x06000439 RID: 1081 RVA: 0x0001AA28 File Offset: 0x00018C28
		public event IsDomainAvailableCompletedEventHandler IsDomainAvailableCompleted;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600043A RID: 1082 RVA: 0x0001AA60 File Offset: 0x00018C60
		// (remove) Token: 0x0600043B RID: 1083 RVA: 0x0001AA98 File Offset: 0x00018C98
		public event CreateCompanyCompletedEventHandler CreateCompanyCompleted;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600043C RID: 1084 RVA: 0x0001AAD0 File Offset: 0x00018CD0
		// (remove) Token: 0x0600043D RID: 1085 RVA: 0x0001AB08 File Offset: 0x00018D08
		public event CreateSyndicatedCompanyCompletedEventHandler CreateSyndicatedCompanyCompleted;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600043E RID: 1086 RVA: 0x0001AB40 File Offset: 0x00018D40
		// (remove) Token: 0x0600043F RID: 1087 RVA: 0x0001AB78 File Offset: 0x00018D78
		public event SetCompanyPartnershipCompletedEventHandler SetCompanyPartnershipCompleted;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000440 RID: 1088 RVA: 0x0001ABB0 File Offset: 0x00018DB0
		// (remove) Token: 0x06000441 RID: 1089 RVA: 0x0001ABE8 File Offset: 0x00018DE8
		public event UpdateCompanyProfileCompletedEventHandler UpdateCompanyProfileCompleted;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000442 RID: 1090 RVA: 0x0001AC20 File Offset: 0x00018E20
		// (remove) Token: 0x06000443 RID: 1091 RVA: 0x0001AC58 File Offset: 0x00018E58
		public event UpdateCompanyTagsCompletedEventHandler UpdateCompanyTagsCompleted;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000444 RID: 1092 RVA: 0x0001AC90 File Offset: 0x00018E90
		// (remove) Token: 0x06000445 RID: 1093 RVA: 0x0001ACC8 File Offset: 0x00018EC8
		public event DeleteCompanyCompletedEventHandler DeleteCompanyCompleted;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000446 RID: 1094 RVA: 0x0001AD00 File Offset: 0x00018F00
		// (remove) Token: 0x06000447 RID: 1095 RVA: 0x0001AD38 File Offset: 0x00018F38
		public event ForceDeleteCompanyCompletedEventHandler ForceDeleteCompanyCompleted;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000448 RID: 1096 RVA: 0x0001AD70 File Offset: 0x00018F70
		// (remove) Token: 0x06000449 RID: 1097 RVA: 0x0001ADA8 File Offset: 0x00018FA8
		public event CreateAccountCompletedEventHandler CreateAccountCompleted;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600044A RID: 1098 RVA: 0x0001ADE0 File Offset: 0x00018FE0
		// (remove) Token: 0x0600044B RID: 1099 RVA: 0x0001AE18 File Offset: 0x00019018
		public event RenameAccountCompletedEventHandler RenameAccountCompleted;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600044C RID: 1100 RVA: 0x0001AE50 File Offset: 0x00019050
		// (remove) Token: 0x0600044D RID: 1101 RVA: 0x0001AE88 File Offset: 0x00019088
		public event DeleteAccountCompletedEventHandler DeleteAccountCompleted;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600044E RID: 1102 RVA: 0x0001AEC0 File Offset: 0x000190C0
		// (remove) Token: 0x0600044F RID: 1103 RVA: 0x0001AEF8 File Offset: 0x000190F8
		public event CreateUpdateDeleteSubscriptionCompletedEventHandler CreateUpdateDeleteSubscriptionCompleted;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000450 RID: 1104 RVA: 0x0001AF30 File Offset: 0x00019130
		// (remove) Token: 0x06000451 RID: 1105 RVA: 0x0001AF68 File Offset: 0x00019168
		public event CreateCompanyWithSubscriptionsCompletedEventHandler CreateCompanyWithSubscriptionsCompleted;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000452 RID: 1106 RVA: 0x0001AFA0 File Offset: 0x000191A0
		// (remove) Token: 0x06000453 RID: 1107 RVA: 0x0001AFD8 File Offset: 0x000191D8
		public event SignupCompletedEventHandler SignupCompleted;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000454 RID: 1108 RVA: 0x0001B010 File Offset: 0x00019210
		// (remove) Token: 0x06000455 RID: 1109 RVA: 0x0001B048 File Offset: 0x00019248
		public event SignupWithCompanyTagsCompletedEventHandler SignupWithCompanyTagsCompleted;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000456 RID: 1110 RVA: 0x0001B080 File Offset: 0x00019280
		// (remove) Token: 0x06000457 RID: 1111 RVA: 0x0001B0B8 File Offset: 0x000192B8
		public event PromoteToPartnerCompletedEventHandler PromoteToPartnerCompleted;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000458 RID: 1112 RVA: 0x0001B0F0 File Offset: 0x000192F0
		// (remove) Token: 0x06000459 RID: 1113 RVA: 0x0001B128 File Offset: 0x00019328
		public event DemoteToCompanyCompletedEventHandler DemoteToCompanyCompleted;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x0600045A RID: 1114 RVA: 0x0001B160 File Offset: 0x00019360
		// (remove) Token: 0x0600045B RID: 1115 RVA: 0x0001B198 File Offset: 0x00019398
		public event ForceDemoteToCompanyCompletedEventHandler ForceDemoteToCompanyCompleted;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x0600045C RID: 1116 RVA: 0x0001B1D0 File Offset: 0x000193D0
		// (remove) Token: 0x0600045D RID: 1117 RVA: 0x0001B208 File Offset: 0x00019408
		public event AddServiceTypeCompletedEventHandler AddServiceTypeCompleted;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x0600045E RID: 1118 RVA: 0x0001B240 File Offset: 0x00019440
		// (remove) Token: 0x0600045F RID: 1119 RVA: 0x0001B278 File Offset: 0x00019478
		public event RemoveServiceTypeCompletedEventHandler RemoveServiceTypeCompleted;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000460 RID: 1120 RVA: 0x0001B2B0 File Offset: 0x000194B0
		// (remove) Token: 0x06000461 RID: 1121 RVA: 0x0001B2E8 File Offset: 0x000194E8
		public event ListServicesForPartnershipCompletedEventHandler ListServicesForPartnershipCompleted;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000462 RID: 1122 RVA: 0x0001B320 File Offset: 0x00019520
		// (remove) Token: 0x06000463 RID: 1123 RVA: 0x0001B358 File Offset: 0x00019558
		public event AssociateToPartnerCompletedEventHandler AssociateToPartnerCompleted;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000464 RID: 1124 RVA: 0x0001B390 File Offset: 0x00019590
		// (remove) Token: 0x06000465 RID: 1125 RVA: 0x0001B3C8 File Offset: 0x000195C8
		public event DeletePartnerContractCompletedEventHandler DeletePartnerContractCompleted;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000466 RID: 1126 RVA: 0x0001B400 File Offset: 0x00019600
		// (remove) Token: 0x06000467 RID: 1127 RVA: 0x0001B438 File Offset: 0x00019638
		public event CreatePartnerCompletedEventHandler CreatePartnerCompleted;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000468 RID: 1128 RVA: 0x0001B470 File Offset: 0x00019670
		// (remove) Token: 0x06000469 RID: 1129 RVA: 0x0001B4A8 File Offset: 0x000196A8
		public event CreateMailboxAgentsGroupCompletedEventHandler CreateMailboxAgentsGroupCompleted;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x0600046A RID: 1130 RVA: 0x0001B4E0 File Offset: 0x000196E0
		// (remove) Token: 0x0600046B RID: 1131 RVA: 0x0001B518 File Offset: 0x00019718
		public event GetCompanyContextIdCompletedEventHandler GetCompanyContextIdCompleted;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x0600046C RID: 1132 RVA: 0x0001B550 File Offset: 0x00019750
		// (remove) Token: 0x0600046D RID: 1133 RVA: 0x0001B588 File Offset: 0x00019788
		public event GetPartitionIdCompletedEventHandler GetPartitionIdCompleted;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x0600046E RID: 1134 RVA: 0x0001B5C0 File Offset: 0x000197C0
		// (remove) Token: 0x0600046F RID: 1135 RVA: 0x0001B5F8 File Offset: 0x000197F8
		public event GetPartNumberFromSkuIdCompletedEventHandler GetPartNumberFromSkuIdCompleted;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000470 RID: 1136 RVA: 0x0001B630 File Offset: 0x00019830
		// (remove) Token: 0x06000471 RID: 1137 RVA: 0x0001B668 File Offset: 0x00019868
		public event GetSkuIdFromPartNumberCompletedEventHandler GetSkuIdFromPartNumberCompleted;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000472 RID: 1138 RVA: 0x0001B6A0 File Offset: 0x000198A0
		// (remove) Token: 0x06000473 RID: 1139 RVA: 0x0001B6D8 File Offset: 0x000198D8
		public event GetAccountSubscriptionsCompletedEventHandler GetAccountSubscriptionsCompleted;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000474 RID: 1140 RVA: 0x0001B710 File Offset: 0x00019910
		// (remove) Token: 0x06000475 RID: 1141 RVA: 0x0001B748 File Offset: 0x00019948
		public event GetCompanyAccountsCompletedEventHandler GetCompanyAccountsCompleted;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000476 RID: 1142 RVA: 0x0001B780 File Offset: 0x00019980
		// (remove) Token: 0x06000477 RID: 1143 RVA: 0x0001B7B8 File Offset: 0x000199B8
		public event GetCompanyAssignedPlansCompletedEventHandler GetCompanyAssignedPlansCompleted;

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000478 RID: 1144 RVA: 0x0001B7F0 File Offset: 0x000199F0
		// (remove) Token: 0x06000479 RID: 1145 RVA: 0x0001B828 File Offset: 0x00019A28
		public event GetCompanyProvisionedPlansCompletedEventHandler GetCompanyProvisionedPlansCompleted;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x0600047A RID: 1146 RVA: 0x0001B860 File Offset: 0x00019A60
		// (remove) Token: 0x0600047B RID: 1147 RVA: 0x0001B898 File Offset: 0x00019A98
		public event GetCompanySubscriptionsCompletedEventHandler GetCompanySubscriptionsCompleted;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x0600047C RID: 1148 RVA: 0x0001B8D0 File Offset: 0x00019AD0
		// (remove) Token: 0x0600047D RID: 1149 RVA: 0x0001B908 File Offset: 0x00019B08
		public event GetCompanyForeignPrincipalObjectsCompletedEventHandler GetCompanyForeignPrincipalObjectsCompleted;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x0600047E RID: 1150 RVA: 0x0001B940 File Offset: 0x00019B40
		// (remove) Token: 0x0600047F RID: 1151 RVA: 0x0001B978 File Offset: 0x00019B78
		public event SetCompanyProvisioningStatusCompletedEventHandler SetCompanyProvisioningStatusCompleted;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000480 RID: 1152 RVA: 0x0001B9B0 File Offset: 0x00019BB0
		// (remove) Token: 0x06000481 RID: 1153 RVA: 0x0001B9E8 File Offset: 0x00019BE8
		public event ForceRefreshSystemDataCompletedEventHandler ForceRefreshSystemDataCompleted;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06000482 RID: 1154 RVA: 0x0001BA20 File Offset: 0x00019C20
		// (remove) Token: 0x06000483 RID: 1155 RVA: 0x0001BA58 File Offset: 0x00019C58
		public event BuildRandomAccountCompletedEventHandler BuildRandomAccountCompleted;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000484 RID: 1156 RVA: 0x0001BA90 File Offset: 0x00019C90
		// (remove) Token: 0x06000485 RID: 1157 RVA: 0x0001BAC8 File Offset: 0x00019CC8
		public event BuildRandomCompanyCompletedEventHandler BuildRandomCompanyCompleted;

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06000486 RID: 1158 RVA: 0x0001BB00 File Offset: 0x00019D00
		// (remove) Token: 0x06000487 RID: 1159 RVA: 0x0001BB38 File Offset: 0x00019D38
		public event BuildRandomCompanyProfileCompletedEventHandler BuildRandomCompanyProfileCompleted;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06000488 RID: 1160 RVA: 0x0001BB70 File Offset: 0x00019D70
		// (remove) Token: 0x06000489 RID: 1161 RVA: 0x0001BBA8 File Offset: 0x00019DA8
		public event BuildRandomUserCompletedEventHandler BuildRandomUserCompleted;

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x0600048A RID: 1162 RVA: 0x0001BBE0 File Offset: 0x00019DE0
		// (remove) Token: 0x0600048B RID: 1163 RVA: 0x0001BC18 File Offset: 0x00019E18
		public event BuildRandomSubscriptionCompletedEventHandler BuildRandomSubscriptionCompleted;

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x0600048C RID: 1164 RVA: 0x0001BC50 File Offset: 0x00019E50
		// (remove) Token: 0x0600048D RID: 1165 RVA: 0x0001BC88 File Offset: 0x00019E88
		public event GetDefaultContractRoleMapCompletedEventHandler GetDefaultContractRoleMapCompleted;

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x0600048E RID: 1166 RVA: 0x0001BCC0 File Offset: 0x00019EC0
		// (remove) Token: 0x0600048F RID: 1167 RVA: 0x0001BCF8 File Offset: 0x00019EF8
		public event ForceTransitiveReplicationCompletedEventHandler ForceTransitiveReplicationCompleted;

		// Token: 0x06000490 RID: 1168 RVA: 0x0001BD30 File Offset: 0x00019F30
		[SoapDocumentMethod("http://www.ccs.com/TestServices/IsDomainAvailable", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public bool IsDomainAvailable(string domainPrefix, string domainSuffix, Guid trackingId)
		{
			object[] array = base.Invoke("IsDomainAvailable", new object[]
			{
				domainPrefix,
				domainSuffix,
				trackingId
			});
			return (bool)array[0];
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0001BD6C File Offset: 0x00019F6C
		public IAsyncResult BeginIsDomainAvailable(string domainPrefix, string domainSuffix, Guid trackingId, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("IsDomainAvailable", new object[]
			{
				domainPrefix,
				domainSuffix,
				trackingId
			}, callback, asyncState);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0001BDA4 File Offset: 0x00019FA4
		public bool EndIsDomainAvailable(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (bool)array[0];
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0001BDC1 File Offset: 0x00019FC1
		public void IsDomainAvailableAsync(string domainPrefix, string domainSuffix, Guid trackingId)
		{
			this.IsDomainAvailableAsync(domainPrefix, domainSuffix, trackingId, null);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0001BDD0 File Offset: 0x00019FD0
		public void IsDomainAvailableAsync(string domainPrefix, string domainSuffix, Guid trackingId, object userState)
		{
			if (this.IsDomainAvailableOperationCompleted == null)
			{
				this.IsDomainAvailableOperationCompleted = new SendOrPostCallback(this.OnIsDomainAvailableOperationCompleted);
			}
			base.InvokeAsync("IsDomainAvailable", new object[]
			{
				domainPrefix,
				domainSuffix,
				trackingId
			}, this.IsDomainAvailableOperationCompleted, userState);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0001BE24 File Offset: 0x0001A024
		private void OnIsDomainAvailableOperationCompleted(object arg)
		{
			if (this.IsDomainAvailableCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.IsDomainAvailableCompleted(this, new IsDomainAvailableCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0001BE6C File Offset: 0x0001A06C
		[SoapDocumentMethod("http://www.ccs.com/TestServices/CreateCompany", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public ProvisionInfo CreateCompany(Company company, User user, Account account, Guid trackingId)
		{
			object[] array = base.Invoke("CreateCompany", new object[]
			{
				company,
				user,
				account,
				trackingId
			});
			return (ProvisionInfo)array[0];
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0001BEAC File Offset: 0x0001A0AC
		public IAsyncResult BeginCreateCompany(Company company, User user, Account account, Guid trackingId, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateCompany", new object[]
			{
				company,
				user,
				account,
				trackingId
			}, callback, asyncState);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0001BEE8 File Offset: 0x0001A0E8
		public ProvisionInfo EndCreateCompany(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ProvisionInfo)array[0];
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0001BF05 File Offset: 0x0001A105
		public void CreateCompanyAsync(Company company, User user, Account account, Guid trackingId)
		{
			this.CreateCompanyAsync(company, user, account, trackingId, null);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0001BF14 File Offset: 0x0001A114
		public void CreateCompanyAsync(Company company, User user, Account account, Guid trackingId, object userState)
		{
			if (this.CreateCompanyOperationCompleted == null)
			{
				this.CreateCompanyOperationCompleted = new SendOrPostCallback(this.OnCreateCompanyOperationCompleted);
			}
			base.InvokeAsync("CreateCompany", new object[]
			{
				company,
				user,
				account,
				trackingId
			}, this.CreateCompanyOperationCompleted, userState);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0001BF6C File Offset: 0x0001A16C
		private void OnCreateCompanyOperationCompleted(object arg)
		{
			if (this.CreateCompanyCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateCompanyCompleted(this, new CreateCompanyCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0001BFB4 File Offset: 0x0001A1B4
		[SoapDocumentMethod("http://www.ccs.com/TestServices/CreateSyndicatedCompany", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public ProvisionInfo CreateSyndicatedCompany(Company company, User user, Account account, Guid trackingId, PartnershipValue[] partnerships)
		{
			object[] array = base.Invoke("CreateSyndicatedCompany", new object[]
			{
				company,
				user,
				account,
				trackingId,
				partnerships
			});
			return (ProvisionInfo)array[0];
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0001BFF8 File Offset: 0x0001A1F8
		public IAsyncResult BeginCreateSyndicatedCompany(Company company, User user, Account account, Guid trackingId, PartnershipValue[] partnerships, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateSyndicatedCompany", new object[]
			{
				company,
				user,
				account,
				trackingId,
				partnerships
			}, callback, asyncState);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001C038 File Offset: 0x0001A238
		public ProvisionInfo EndCreateSyndicatedCompany(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ProvisionInfo)array[0];
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001C055 File Offset: 0x0001A255
		public void CreateSyndicatedCompanyAsync(Company company, User user, Account account, Guid trackingId, PartnershipValue[] partnerships)
		{
			this.CreateSyndicatedCompanyAsync(company, user, account, trackingId, partnerships, null);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0001C068 File Offset: 0x0001A268
		public void CreateSyndicatedCompanyAsync(Company company, User user, Account account, Guid trackingId, PartnershipValue[] partnerships, object userState)
		{
			if (this.CreateSyndicatedCompanyOperationCompleted == null)
			{
				this.CreateSyndicatedCompanyOperationCompleted = new SendOrPostCallback(this.OnCreateSyndicatedCompanyOperationCompleted);
			}
			base.InvokeAsync("CreateSyndicatedCompany", new object[]
			{
				company,
				user,
				account,
				trackingId,
				partnerships
			}, this.CreateSyndicatedCompanyOperationCompleted, userState);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0001C0C8 File Offset: 0x0001A2C8
		private void OnCreateSyndicatedCompanyOperationCompleted(object arg)
		{
			if (this.CreateSyndicatedCompanyCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateSyndicatedCompanyCompleted(this, new CreateSyndicatedCompanyCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0001C110 File Offset: 0x0001A310
		[SoapDocumentMethod("http://www.ccs.com/TestServices/SetCompanyPartnership", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetCompanyPartnership(Guid contextId, PartnershipValue[] partnerships)
		{
			base.Invoke("SetCompanyPartnership", new object[]
			{
				contextId,
				partnerships
			});
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0001C140 File Offset: 0x0001A340
		public IAsyncResult BeginSetCompanyPartnership(Guid contextId, PartnershipValue[] partnerships, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetCompanyPartnership", new object[]
			{
				contextId,
				partnerships
			}, callback, asyncState);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001C170 File Offset: 0x0001A370
		public void EndSetCompanyPartnership(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001C17A File Offset: 0x0001A37A
		public void SetCompanyPartnershipAsync(Guid contextId, PartnershipValue[] partnerships)
		{
			this.SetCompanyPartnershipAsync(contextId, partnerships, null);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0001C188 File Offset: 0x0001A388
		public void SetCompanyPartnershipAsync(Guid contextId, PartnershipValue[] partnerships, object userState)
		{
			if (this.SetCompanyPartnershipOperationCompleted == null)
			{
				this.SetCompanyPartnershipOperationCompleted = new SendOrPostCallback(this.OnSetCompanyPartnershipOperationCompleted);
			}
			base.InvokeAsync("SetCompanyPartnership", new object[]
			{
				contextId,
				partnerships
			}, this.SetCompanyPartnershipOperationCompleted, userState);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0001C1D8 File Offset: 0x0001A3D8
		private void OnSetCompanyPartnershipOperationCompleted(object arg)
		{
			if (this.SetCompanyPartnershipCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetCompanyPartnershipCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0001C218 File Offset: 0x0001A418
		[SoapDocumentMethod("http://www.ccs.com/TestServices/UpdateCompanyProfile", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void UpdateCompanyProfile(Guid contextId, CompanyProfile companyProfile)
		{
			base.Invoke("UpdateCompanyProfile", new object[]
			{
				contextId,
				companyProfile
			});
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001C248 File Offset: 0x0001A448
		public IAsyncResult BeginUpdateCompanyProfile(Guid contextId, CompanyProfile companyProfile, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateCompanyProfile", new object[]
			{
				contextId,
				companyProfile
			}, callback, asyncState);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0001C278 File Offset: 0x0001A478
		public void EndUpdateCompanyProfile(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0001C282 File Offset: 0x0001A482
		public void UpdateCompanyProfileAsync(Guid contextId, CompanyProfile companyProfile)
		{
			this.UpdateCompanyProfileAsync(contextId, companyProfile, null);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0001C290 File Offset: 0x0001A490
		public void UpdateCompanyProfileAsync(Guid contextId, CompanyProfile companyProfile, object userState)
		{
			if (this.UpdateCompanyProfileOperationCompleted == null)
			{
				this.UpdateCompanyProfileOperationCompleted = new SendOrPostCallback(this.OnUpdateCompanyProfileOperationCompleted);
			}
			base.InvokeAsync("UpdateCompanyProfile", new object[]
			{
				contextId,
				companyProfile
			}, this.UpdateCompanyProfileOperationCompleted, userState);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0001C2E0 File Offset: 0x0001A4E0
		private void OnUpdateCompanyProfileOperationCompleted(object arg)
		{
			if (this.UpdateCompanyProfileCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateCompanyProfileCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0001C320 File Offset: 0x0001A520
		[SoapDocumentMethod("http://www.ccs.com/TestServices/UpdateCompanyTags", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void UpdateCompanyTags(Guid contextId, string[] companyTags)
		{
			base.Invoke("UpdateCompanyTags", new object[]
			{
				contextId,
				companyTags
			});
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0001C350 File Offset: 0x0001A550
		public IAsyncResult BeginUpdateCompanyTags(Guid contextId, string[] companyTags, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("UpdateCompanyTags", new object[]
			{
				contextId,
				companyTags
			}, callback, asyncState);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0001C380 File Offset: 0x0001A580
		public void EndUpdateCompanyTags(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0001C38A File Offset: 0x0001A58A
		public void UpdateCompanyTagsAsync(Guid contextId, string[] companyTags)
		{
			this.UpdateCompanyTagsAsync(contextId, companyTags, null);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0001C398 File Offset: 0x0001A598
		public void UpdateCompanyTagsAsync(Guid contextId, string[] companyTags, object userState)
		{
			if (this.UpdateCompanyTagsOperationCompleted == null)
			{
				this.UpdateCompanyTagsOperationCompleted = new SendOrPostCallback(this.OnUpdateCompanyTagsOperationCompleted);
			}
			base.InvokeAsync("UpdateCompanyTags", new object[]
			{
				contextId,
				companyTags
			}, this.UpdateCompanyTagsOperationCompleted, userState);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001C3E8 File Offset: 0x0001A5E8
		private void OnUpdateCompanyTagsOperationCompleted(object arg)
		{
			if (this.UpdateCompanyTagsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.UpdateCompanyTagsCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001C428 File Offset: 0x0001A628
		[SoapDocumentMethod("http://www.ccs.com/TestServices/DeleteCompany", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void DeleteCompany(Guid contextId)
		{
			base.Invoke("DeleteCompany", new object[]
			{
				contextId
			});
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0001C454 File Offset: 0x0001A654
		public IAsyncResult BeginDeleteCompany(Guid contextId, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteCompany", new object[]
			{
				contextId
			}, callback, asyncState);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001C47F File Offset: 0x0001A67F
		public void EndDeleteCompany(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0001C489 File Offset: 0x0001A689
		public void DeleteCompanyAsync(Guid contextId)
		{
			this.DeleteCompanyAsync(contextId, null);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001C494 File Offset: 0x0001A694
		public void DeleteCompanyAsync(Guid contextId, object userState)
		{
			if (this.DeleteCompanyOperationCompleted == null)
			{
				this.DeleteCompanyOperationCompleted = new SendOrPostCallback(this.OnDeleteCompanyOperationCompleted);
			}
			base.InvokeAsync("DeleteCompany", new object[]
			{
				contextId
			}, this.DeleteCompanyOperationCompleted, userState);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001C4E0 File Offset: 0x0001A6E0
		private void OnDeleteCompanyOperationCompleted(object arg)
		{
			if (this.DeleteCompanyCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteCompanyCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001C520 File Offset: 0x0001A720
		[SoapDocumentMethod("http://www.ccs.com/TestServices/ForceDeleteCompany", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void ForceDeleteCompany(Guid contextId)
		{
			base.Invoke("ForceDeleteCompany", new object[]
			{
				contextId
			});
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0001C54C File Offset: 0x0001A74C
		public IAsyncResult BeginForceDeleteCompany(Guid contextId, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ForceDeleteCompany", new object[]
			{
				contextId
			}, callback, asyncState);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001C577 File Offset: 0x0001A777
		public void EndForceDeleteCompany(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001C581 File Offset: 0x0001A781
		public void ForceDeleteCompanyAsync(Guid contextId)
		{
			this.ForceDeleteCompanyAsync(contextId, null);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0001C58C File Offset: 0x0001A78C
		public void ForceDeleteCompanyAsync(Guid contextId, object userState)
		{
			if (this.ForceDeleteCompanyOperationCompleted == null)
			{
				this.ForceDeleteCompanyOperationCompleted = new SendOrPostCallback(this.OnForceDeleteCompanyOperationCompleted);
			}
			base.InvokeAsync("ForceDeleteCompany", new object[]
			{
				contextId
			}, this.ForceDeleteCompanyOperationCompleted, userState);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0001C5D8 File Offset: 0x0001A7D8
		private void OnForceDeleteCompanyOperationCompleted(object arg)
		{
			if (this.ForceDeleteCompanyCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ForceDeleteCompanyCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001C618 File Offset: 0x0001A818
		[SoapDocumentMethod("http://www.ccs.com/TestServices/CreateAccount", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void CreateAccount(Guid contextId, Account account)
		{
			base.Invoke("CreateAccount", new object[]
			{
				contextId,
				account
			});
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001C648 File Offset: 0x0001A848
		public IAsyncResult BeginCreateAccount(Guid contextId, Account account, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateAccount", new object[]
			{
				contextId,
				account
			}, callback, asyncState);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001C678 File Offset: 0x0001A878
		public void EndCreateAccount(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0001C682 File Offset: 0x0001A882
		public void CreateAccountAsync(Guid contextId, Account account)
		{
			this.CreateAccountAsync(contextId, account, null);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001C690 File Offset: 0x0001A890
		public void CreateAccountAsync(Guid contextId, Account account, object userState)
		{
			if (this.CreateAccountOperationCompleted == null)
			{
				this.CreateAccountOperationCompleted = new SendOrPostCallback(this.OnCreateAccountOperationCompleted);
			}
			base.InvokeAsync("CreateAccount", new object[]
			{
				contextId,
				account
			}, this.CreateAccountOperationCompleted, userState);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001C6E0 File Offset: 0x0001A8E0
		private void OnCreateAccountOperationCompleted(object arg)
		{
			if (this.CreateAccountCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateAccountCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0001C720 File Offset: 0x0001A920
		[SoapDocumentMethod("http://www.ccs.com/TestServices/RenameAccount", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void RenameAccount(Guid contextId, Account account)
		{
			base.Invoke("RenameAccount", new object[]
			{
				contextId,
				account
			});
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0001C750 File Offset: 0x0001A950
		public IAsyncResult BeginRenameAccount(Guid contextId, Account account, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("RenameAccount", new object[]
			{
				contextId,
				account
			}, callback, asyncState);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001C780 File Offset: 0x0001A980
		public void EndRenameAccount(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0001C78A File Offset: 0x0001A98A
		public void RenameAccountAsync(Guid contextId, Account account)
		{
			this.RenameAccountAsync(contextId, account, null);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001C798 File Offset: 0x0001A998
		public void RenameAccountAsync(Guid contextId, Account account, object userState)
		{
			if (this.RenameAccountOperationCompleted == null)
			{
				this.RenameAccountOperationCompleted = new SendOrPostCallback(this.OnRenameAccountOperationCompleted);
			}
			base.InvokeAsync("RenameAccount", new object[]
			{
				contextId,
				account
			}, this.RenameAccountOperationCompleted, userState);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001C7E8 File Offset: 0x0001A9E8
		private void OnRenameAccountOperationCompleted(object arg)
		{
			if (this.RenameAccountCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RenameAccountCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001C828 File Offset: 0x0001AA28
		[SoapDocumentMethod("http://www.ccs.com/TestServices/DeleteAccount", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void DeleteAccount(Guid contextId, Guid accountId)
		{
			base.Invoke("DeleteAccount", new object[]
			{
				contextId,
				accountId
			});
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0001C85C File Offset: 0x0001AA5C
		public IAsyncResult BeginDeleteAccount(Guid contextId, Guid accountId, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeleteAccount", new object[]
			{
				contextId,
				accountId
			}, callback, asyncState);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001C891 File Offset: 0x0001AA91
		public void EndDeleteAccount(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001C89B File Offset: 0x0001AA9B
		public void DeleteAccountAsync(Guid contextId, Guid accountId)
		{
			this.DeleteAccountAsync(contextId, accountId, null);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0001C8A8 File Offset: 0x0001AAA8
		public void DeleteAccountAsync(Guid contextId, Guid accountId, object userState)
		{
			if (this.DeleteAccountOperationCompleted == null)
			{
				this.DeleteAccountOperationCompleted = new SendOrPostCallback(this.OnDeleteAccountOperationCompleted);
			}
			base.InvokeAsync("DeleteAccount", new object[]
			{
				contextId,
				accountId
			}, this.DeleteAccountOperationCompleted, userState);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0001C8FC File Offset: 0x0001AAFC
		private void OnDeleteAccountOperationCompleted(object arg)
		{
			if (this.DeleteAccountCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeleteAccountCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0001C93C File Offset: 0x0001AB3C
		[SoapDocumentMethod("http://www.ccs.com/TestServices/CreateUpdateDeleteSubscription", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void CreateUpdateDeleteSubscription(Subscription subscription)
		{
			base.Invoke("CreateUpdateDeleteSubscription", new object[]
			{
				subscription
			});
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0001C964 File Offset: 0x0001AB64
		public IAsyncResult BeginCreateUpdateDeleteSubscription(Subscription subscription, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateUpdateDeleteSubscription", new object[]
			{
				subscription
			}, callback, asyncState);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0001C98A File Offset: 0x0001AB8A
		public void EndCreateUpdateDeleteSubscription(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0001C994 File Offset: 0x0001AB94
		public void CreateUpdateDeleteSubscriptionAsync(Subscription subscription)
		{
			this.CreateUpdateDeleteSubscriptionAsync(subscription, null);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001C9A0 File Offset: 0x0001ABA0
		public void CreateUpdateDeleteSubscriptionAsync(Subscription subscription, object userState)
		{
			if (this.CreateUpdateDeleteSubscriptionOperationCompleted == null)
			{
				this.CreateUpdateDeleteSubscriptionOperationCompleted = new SendOrPostCallback(this.OnCreateUpdateDeleteSubscriptionOperationCompleted);
			}
			base.InvokeAsync("CreateUpdateDeleteSubscription", new object[]
			{
				subscription
			}, this.CreateUpdateDeleteSubscriptionOperationCompleted, userState);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001C9E8 File Offset: 0x0001ABE8
		private void OnCreateUpdateDeleteSubscriptionOperationCompleted(object arg)
		{
			if (this.CreateUpdateDeleteSubscriptionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateUpdateDeleteSubscriptionCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001CA28 File Offset: 0x0001AC28
		[SoapDocumentMethod("http://www.ccs.com/TestServices/CreateCompanyWithSubscriptions", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public ProvisionInfo CreateCompanyWithSubscriptions(Company company, User user, Account account, Subscription[] subscriptions)
		{
			object[] array = base.Invoke("CreateCompanyWithSubscriptions", new object[]
			{
				company,
				user,
				account,
				subscriptions
			});
			return (ProvisionInfo)array[0];
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0001CA64 File Offset: 0x0001AC64
		public IAsyncResult BeginCreateCompanyWithSubscriptions(Company company, User user, Account account, Subscription[] subscriptions, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateCompanyWithSubscriptions", new object[]
			{
				company,
				user,
				account,
				subscriptions
			}, callback, asyncState);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0001CA9C File Offset: 0x0001AC9C
		public ProvisionInfo EndCreateCompanyWithSubscriptions(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ProvisionInfo)array[0];
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0001CAB9 File Offset: 0x0001ACB9
		public void CreateCompanyWithSubscriptionsAsync(Company company, User user, Account account, Subscription[] subscriptions)
		{
			this.CreateCompanyWithSubscriptionsAsync(company, user, account, subscriptions, null);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001CAC8 File Offset: 0x0001ACC8
		public void CreateCompanyWithSubscriptionsAsync(Company company, User user, Account account, Subscription[] subscriptions, object userState)
		{
			if (this.CreateCompanyWithSubscriptionsOperationCompleted == null)
			{
				this.CreateCompanyWithSubscriptionsOperationCompleted = new SendOrPostCallback(this.OnCreateCompanyWithSubscriptionsOperationCompleted);
			}
			base.InvokeAsync("CreateCompanyWithSubscriptions", new object[]
			{
				company,
				user,
				account,
				subscriptions
			}, this.CreateCompanyWithSubscriptionsOperationCompleted, userState);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0001CB1C File Offset: 0x0001AD1C
		private void OnCreateCompanyWithSubscriptionsOperationCompleted(object arg)
		{
			if (this.CreateCompanyWithSubscriptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateCompanyWithSubscriptionsCompleted(this, new CreateCompanyWithSubscriptionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001CB64 File Offset: 0x0001AD64
		[SoapDocumentMethod("http://www.ccs.com/TestServices/Signup", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public ProvisionInfo Signup(Company company, User user, Account account, Subscription subscription, Guid trackingId)
		{
			object[] array = base.Invoke("Signup", new object[]
			{
				company,
				user,
				account,
				subscription,
				trackingId
			});
			return (ProvisionInfo)array[0];
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0001CBA8 File Offset: 0x0001ADA8
		public IAsyncResult BeginSignup(Company company, User user, Account account, Subscription subscription, Guid trackingId, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("Signup", new object[]
			{
				company,
				user,
				account,
				subscription,
				trackingId
			}, callback, asyncState);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001CBE8 File Offset: 0x0001ADE8
		public ProvisionInfo EndSignup(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ProvisionInfo)array[0];
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001CC05 File Offset: 0x0001AE05
		public void SignupAsync(Company company, User user, Account account, Subscription subscription, Guid trackingId)
		{
			this.SignupAsync(company, user, account, subscription, trackingId, null);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001CC18 File Offset: 0x0001AE18
		public void SignupAsync(Company company, User user, Account account, Subscription subscription, Guid trackingId, object userState)
		{
			if (this.SignupOperationCompleted == null)
			{
				this.SignupOperationCompleted = new SendOrPostCallback(this.OnSignupOperationCompleted);
			}
			base.InvokeAsync("Signup", new object[]
			{
				company,
				user,
				account,
				subscription,
				trackingId
			}, this.SignupOperationCompleted, userState);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0001CC78 File Offset: 0x0001AE78
		private void OnSignupOperationCompleted(object arg)
		{
			if (this.SignupCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SignupCompleted(this, new SignupCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0001CCC0 File Offset: 0x0001AEC0
		[SoapDocumentMethod("http://www.ccs.com/TestServices/SignupWithCompanyTags", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public ProvisionInfo SignupWithCompanyTags(Company company, string[] companyTags, User user, Account account, Subscription subscription, Guid trackingId)
		{
			object[] array = base.Invoke("SignupWithCompanyTags", new object[]
			{
				company,
				companyTags,
				user,
				account,
				subscription,
				trackingId
			});
			return (ProvisionInfo)array[0];
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0001CD0C File Offset: 0x0001AF0C
		public IAsyncResult BeginSignupWithCompanyTags(Company company, string[] companyTags, User user, Account account, Subscription subscription, Guid trackingId, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SignupWithCompanyTags", new object[]
			{
				company,
				companyTags,
				user,
				account,
				subscription,
				trackingId
			}, callback, asyncState);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0001CD50 File Offset: 0x0001AF50
		public ProvisionInfo EndSignupWithCompanyTags(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ProvisionInfo)array[0];
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0001CD6D File Offset: 0x0001AF6D
		public void SignupWithCompanyTagsAsync(Company company, string[] companyTags, User user, Account account, Subscription subscription, Guid trackingId)
		{
			this.SignupWithCompanyTagsAsync(company, companyTags, user, account, subscription, trackingId, null);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0001CD80 File Offset: 0x0001AF80
		public void SignupWithCompanyTagsAsync(Company company, string[] companyTags, User user, Account account, Subscription subscription, Guid trackingId, object userState)
		{
			if (this.SignupWithCompanyTagsOperationCompleted == null)
			{
				this.SignupWithCompanyTagsOperationCompleted = new SendOrPostCallback(this.OnSignupWithCompanyTagsOperationCompleted);
			}
			base.InvokeAsync("SignupWithCompanyTags", new object[]
			{
				company,
				companyTags,
				user,
				account,
				subscription,
				trackingId
			}, this.SignupWithCompanyTagsOperationCompleted, userState);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0001CDE4 File Offset: 0x0001AFE4
		private void OnSignupWithCompanyTagsOperationCompleted(object arg)
		{
			if (this.SignupWithCompanyTagsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SignupWithCompanyTagsCompleted(this, new SignupWithCompanyTagsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0001CE2C File Offset: 0x0001B02C
		[SoapDocumentMethod("http://www.ccs.com/TestServices/PromoteToPartner", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void PromoteToPartner(Guid partnerContextId, string[] serviceTypes, PartnerType partnerType)
		{
			base.Invoke("PromoteToPartner", new object[]
			{
				partnerContextId,
				serviceTypes,
				partnerType
			});
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0001CE64 File Offset: 0x0001B064
		public IAsyncResult BeginPromoteToPartner(Guid partnerContextId, string[] serviceTypes, PartnerType partnerType, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("PromoteToPartner", new object[]
			{
				partnerContextId,
				serviceTypes,
				partnerType
			}, callback, asyncState);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001CE9E File Offset: 0x0001B09E
		public void EndPromoteToPartner(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001CEA8 File Offset: 0x0001B0A8
		public void PromoteToPartnerAsync(Guid partnerContextId, string[] serviceTypes, PartnerType partnerType)
		{
			this.PromoteToPartnerAsync(partnerContextId, serviceTypes, partnerType, null);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0001CEB4 File Offset: 0x0001B0B4
		public void PromoteToPartnerAsync(Guid partnerContextId, string[] serviceTypes, PartnerType partnerType, object userState)
		{
			if (this.PromoteToPartnerOperationCompleted == null)
			{
				this.PromoteToPartnerOperationCompleted = new SendOrPostCallback(this.OnPromoteToPartnerOperationCompleted);
			}
			base.InvokeAsync("PromoteToPartner", new object[]
			{
				partnerContextId,
				serviceTypes,
				partnerType
			}, this.PromoteToPartnerOperationCompleted, userState);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001CF0C File Offset: 0x0001B10C
		private void OnPromoteToPartnerOperationCompleted(object arg)
		{
			if (this.PromoteToPartnerCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.PromoteToPartnerCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0001CF4C File Offset: 0x0001B14C
		[SoapDocumentMethod("http://www.ccs.com/TestServices/DemoteToCompany", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void DemoteToCompany(Guid partnerContextId)
		{
			base.Invoke("DemoteToCompany", new object[]
			{
				partnerContextId
			});
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001CF78 File Offset: 0x0001B178
		public IAsyncResult BeginDemoteToCompany(Guid partnerContextId, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DemoteToCompany", new object[]
			{
				partnerContextId
			}, callback, asyncState);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0001CFA3 File Offset: 0x0001B1A3
		public void EndDemoteToCompany(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001CFAD File Offset: 0x0001B1AD
		public void DemoteToCompanyAsync(Guid partnerContextId)
		{
			this.DemoteToCompanyAsync(partnerContextId, null);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001CFB8 File Offset: 0x0001B1B8
		public void DemoteToCompanyAsync(Guid partnerContextId, object userState)
		{
			if (this.DemoteToCompanyOperationCompleted == null)
			{
				this.DemoteToCompanyOperationCompleted = new SendOrPostCallback(this.OnDemoteToCompanyOperationCompleted);
			}
			base.InvokeAsync("DemoteToCompany", new object[]
			{
				partnerContextId
			}, this.DemoteToCompanyOperationCompleted, userState);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0001D004 File Offset: 0x0001B204
		private void OnDemoteToCompanyOperationCompleted(object arg)
		{
			if (this.DemoteToCompanyCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DemoteToCompanyCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001D044 File Offset: 0x0001B244
		[SoapDocumentMethod("http://www.ccs.com/TestServices/ForceDemoteToCompany", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void ForceDemoteToCompany(Guid partnerContextId)
		{
			base.Invoke("ForceDemoteToCompany", new object[]
			{
				partnerContextId
			});
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0001D070 File Offset: 0x0001B270
		public IAsyncResult BeginForceDemoteToCompany(Guid partnerContextId, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ForceDemoteToCompany", new object[]
			{
				partnerContextId
			}, callback, asyncState);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0001D09B File Offset: 0x0001B29B
		public void EndForceDemoteToCompany(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001D0A5 File Offset: 0x0001B2A5
		public void ForceDemoteToCompanyAsync(Guid partnerContextId)
		{
			this.ForceDemoteToCompanyAsync(partnerContextId, null);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001D0B0 File Offset: 0x0001B2B0
		public void ForceDemoteToCompanyAsync(Guid partnerContextId, object userState)
		{
			if (this.ForceDemoteToCompanyOperationCompleted == null)
			{
				this.ForceDemoteToCompanyOperationCompleted = new SendOrPostCallback(this.OnForceDemoteToCompanyOperationCompleted);
			}
			base.InvokeAsync("ForceDemoteToCompany", new object[]
			{
				partnerContextId
			}, this.ForceDemoteToCompanyOperationCompleted, userState);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001D0FC File Offset: 0x0001B2FC
		private void OnForceDemoteToCompanyOperationCompleted(object arg)
		{
			if (this.ForceDemoteToCompanyCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ForceDemoteToCompanyCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0001D13C File Offset: 0x0001B33C
		[SoapDocumentMethod("http://www.ccs.com/TestServices/AddServiceType", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void AddServiceType(Guid partnerContextId, string serviceType)
		{
			base.Invoke("AddServiceType", new object[]
			{
				partnerContextId,
				serviceType
			});
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0001D16C File Offset: 0x0001B36C
		public IAsyncResult BeginAddServiceType(Guid partnerContextId, string serviceType, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("AddServiceType", new object[]
			{
				partnerContextId,
				serviceType
			}, callback, asyncState);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001D19C File Offset: 0x0001B39C
		public void EndAddServiceType(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0001D1A6 File Offset: 0x0001B3A6
		public void AddServiceTypeAsync(Guid partnerContextId, string serviceType)
		{
			this.AddServiceTypeAsync(partnerContextId, serviceType, null);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001D1B4 File Offset: 0x0001B3B4
		public void AddServiceTypeAsync(Guid partnerContextId, string serviceType, object userState)
		{
			if (this.AddServiceTypeOperationCompleted == null)
			{
				this.AddServiceTypeOperationCompleted = new SendOrPostCallback(this.OnAddServiceTypeOperationCompleted);
			}
			base.InvokeAsync("AddServiceType", new object[]
			{
				partnerContextId,
				serviceType
			}, this.AddServiceTypeOperationCompleted, userState);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0001D204 File Offset: 0x0001B404
		private void OnAddServiceTypeOperationCompleted(object arg)
		{
			if (this.AddServiceTypeCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AddServiceTypeCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0001D244 File Offset: 0x0001B444
		[SoapDocumentMethod("http://www.ccs.com/TestServices/RemoveServiceType", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void RemoveServiceType(Guid partnerContextId, string serviceType)
		{
			base.Invoke("RemoveServiceType", new object[]
			{
				partnerContextId,
				serviceType
			});
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001D274 File Offset: 0x0001B474
		public IAsyncResult BeginRemoveServiceType(Guid partnerContextId, string serviceType, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("RemoveServiceType", new object[]
			{
				partnerContextId,
				serviceType
			}, callback, asyncState);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001D2A4 File Offset: 0x0001B4A4
		public void EndRemoveServiceType(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0001D2AE File Offset: 0x0001B4AE
		public void RemoveServiceTypeAsync(Guid partnerContextId, string serviceType)
		{
			this.RemoveServiceTypeAsync(partnerContextId, serviceType, null);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001D2BC File Offset: 0x0001B4BC
		public void RemoveServiceTypeAsync(Guid partnerContextId, string serviceType, object userState)
		{
			if (this.RemoveServiceTypeOperationCompleted == null)
			{
				this.RemoveServiceTypeOperationCompleted = new SendOrPostCallback(this.OnRemoveServiceTypeOperationCompleted);
			}
			base.InvokeAsync("RemoveServiceType", new object[]
			{
				partnerContextId,
				serviceType
			}, this.RemoveServiceTypeOperationCompleted, userState);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0001D30C File Offset: 0x0001B50C
		private void OnRemoveServiceTypeOperationCompleted(object arg)
		{
			if (this.RemoveServiceTypeCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RemoveServiceTypeCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001D34C File Offset: 0x0001B54C
		[SoapDocumentMethod("http://www.ccs.com/TestServices/ListServicesForPartnership", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string[] ListServicesForPartnership()
		{
			object[] array = base.Invoke("ListServicesForPartnership", new object[0]);
			return (string[])array[0];
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001D373 File Offset: 0x0001B573
		public IAsyncResult BeginListServicesForPartnership(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ListServicesForPartnership", new object[0], callback, asyncState);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001D388 File Offset: 0x0001B588
		public string[] EndListServicesForPartnership(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (string[])array[0];
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001D3A5 File Offset: 0x0001B5A5
		public void ListServicesForPartnershipAsync()
		{
			this.ListServicesForPartnershipAsync(null);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0001D3AE File Offset: 0x0001B5AE
		public void ListServicesForPartnershipAsync(object userState)
		{
			if (this.ListServicesForPartnershipOperationCompleted == null)
			{
				this.ListServicesForPartnershipOperationCompleted = new SendOrPostCallback(this.OnListServicesForPartnershipOperationCompleted);
			}
			base.InvokeAsync("ListServicesForPartnership", new object[0], this.ListServicesForPartnershipOperationCompleted, userState);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001D3E4 File Offset: 0x0001B5E4
		private void OnListServicesForPartnershipOperationCompleted(object arg)
		{
			if (this.ListServicesForPartnershipCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ListServicesForPartnershipCompleted(this, new ListServicesForPartnershipCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0001D42C File Offset: 0x0001B62C
		[SoapDocumentMethod("http://www.ccs.com/TestServices/AssociateToPartner", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public Contract AssociateToPartner(Guid companyContextId, Guid partnerContextId, PartnerRoleMap[] roleMaps)
		{
			object[] array = base.Invoke("AssociateToPartner", new object[]
			{
				companyContextId,
				partnerContextId,
				roleMaps
			});
			return (Contract)array[0];
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0001D46C File Offset: 0x0001B66C
		public IAsyncResult BeginAssociateToPartner(Guid companyContextId, Guid partnerContextId, PartnerRoleMap[] roleMaps, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("AssociateToPartner", new object[]
			{
				companyContextId,
				partnerContextId,
				roleMaps
			}, callback, asyncState);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0001D4A8 File Offset: 0x0001B6A8
		public Contract EndAssociateToPartner(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Contract)array[0];
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0001D4C5 File Offset: 0x0001B6C5
		public void AssociateToPartnerAsync(Guid companyContextId, Guid partnerContextId, PartnerRoleMap[] roleMaps)
		{
			this.AssociateToPartnerAsync(companyContextId, partnerContextId, roleMaps, null);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0001D4D4 File Offset: 0x0001B6D4
		public void AssociateToPartnerAsync(Guid companyContextId, Guid partnerContextId, PartnerRoleMap[] roleMaps, object userState)
		{
			if (this.AssociateToPartnerOperationCompleted == null)
			{
				this.AssociateToPartnerOperationCompleted = new SendOrPostCallback(this.OnAssociateToPartnerOperationCompleted);
			}
			base.InvokeAsync("AssociateToPartner", new object[]
			{
				companyContextId,
				partnerContextId,
				roleMaps
			}, this.AssociateToPartnerOperationCompleted, userState);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0001D52C File Offset: 0x0001B72C
		private void OnAssociateToPartnerOperationCompleted(object arg)
		{
			if (this.AssociateToPartnerCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AssociateToPartnerCompleted(this, new AssociateToPartnerCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0001D574 File Offset: 0x0001B774
		[SoapDocumentMethod("http://www.ccs.com/TestServices/DeletePartnerContract", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void DeletePartnerContract(Guid partnerContextId, Guid contractId)
		{
			base.Invoke("DeletePartnerContract", new object[]
			{
				partnerContextId,
				contractId
			});
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001D5A8 File Offset: 0x0001B7A8
		public IAsyncResult BeginDeletePartnerContract(Guid partnerContextId, Guid contractId, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeletePartnerContract", new object[]
			{
				partnerContextId,
				contractId
			}, callback, asyncState);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001D5DD File Offset: 0x0001B7DD
		public void EndDeletePartnerContract(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0001D5E7 File Offset: 0x0001B7E7
		public void DeletePartnerContractAsync(Guid partnerContextId, Guid contractId)
		{
			this.DeletePartnerContractAsync(partnerContextId, contractId, null);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0001D5F4 File Offset: 0x0001B7F4
		public void DeletePartnerContractAsync(Guid partnerContextId, Guid contractId, object userState)
		{
			if (this.DeletePartnerContractOperationCompleted == null)
			{
				this.DeletePartnerContractOperationCompleted = new SendOrPostCallback(this.OnDeletePartnerContractOperationCompleted);
			}
			base.InvokeAsync("DeletePartnerContract", new object[]
			{
				partnerContextId,
				contractId
			}, this.DeletePartnerContractOperationCompleted, userState);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0001D648 File Offset: 0x0001B848
		private void OnDeletePartnerContractOperationCompleted(object arg)
		{
			if (this.DeletePartnerContractCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeletePartnerContractCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001D688 File Offset: 0x0001B888
		[SoapDocumentMethod("http://www.ccs.com/TestServices/CreatePartner", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public ProvisionInfo CreatePartner(Company company, User user, Account account, string[] serviceTypes, PartnerType partnerType, Subscription[] subscriptions)
		{
			object[] array = base.Invoke("CreatePartner", new object[]
			{
				company,
				user,
				account,
				serviceTypes,
				partnerType,
				subscriptions
			});
			return (ProvisionInfo)array[0];
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001D6D4 File Offset: 0x0001B8D4
		public IAsyncResult BeginCreatePartner(Company company, User user, Account account, string[] serviceTypes, PartnerType partnerType, Subscription[] subscriptions, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreatePartner", new object[]
			{
				company,
				user,
				account,
				serviceTypes,
				partnerType,
				subscriptions
			}, callback, asyncState);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001D718 File Offset: 0x0001B918
		public ProvisionInfo EndCreatePartner(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ProvisionInfo)array[0];
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001D735 File Offset: 0x0001B935
		public void CreatePartnerAsync(Company company, User user, Account account, string[] serviceTypes, PartnerType partnerType, Subscription[] subscriptions)
		{
			this.CreatePartnerAsync(company, user, account, serviceTypes, partnerType, subscriptions, null);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0001D748 File Offset: 0x0001B948
		public void CreatePartnerAsync(Company company, User user, Account account, string[] serviceTypes, PartnerType partnerType, Subscription[] subscriptions, object userState)
		{
			if (this.CreatePartnerOperationCompleted == null)
			{
				this.CreatePartnerOperationCompleted = new SendOrPostCallback(this.OnCreatePartnerOperationCompleted);
			}
			base.InvokeAsync("CreatePartner", new object[]
			{
				company,
				user,
				account,
				serviceTypes,
				partnerType,
				subscriptions
			}, this.CreatePartnerOperationCompleted, userState);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0001D7AC File Offset: 0x0001B9AC
		private void OnCreatePartnerOperationCompleted(object arg)
		{
			if (this.CreatePartnerCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreatePartnerCompleted(this, new CreatePartnerCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0001D7F4 File Offset: 0x0001B9F4
		[SoapDocumentMethod("http://www.ccs.com/TestServices/CreateMailboxAgentsGroup", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void CreateMailboxAgentsGroup(Guid partnerContextId)
		{
			base.Invoke("CreateMailboxAgentsGroup", new object[]
			{
				partnerContextId
			});
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0001D820 File Offset: 0x0001BA20
		public IAsyncResult BeginCreateMailboxAgentsGroup(Guid partnerContextId, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateMailboxAgentsGroup", new object[]
			{
				partnerContextId
			}, callback, asyncState);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0001D84B File Offset: 0x0001BA4B
		public void EndCreateMailboxAgentsGroup(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0001D855 File Offset: 0x0001BA55
		public void CreateMailboxAgentsGroupAsync(Guid partnerContextId)
		{
			this.CreateMailboxAgentsGroupAsync(partnerContextId, null);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0001D860 File Offset: 0x0001BA60
		public void CreateMailboxAgentsGroupAsync(Guid partnerContextId, object userState)
		{
			if (this.CreateMailboxAgentsGroupOperationCompleted == null)
			{
				this.CreateMailboxAgentsGroupOperationCompleted = new SendOrPostCallback(this.OnCreateMailboxAgentsGroupOperationCompleted);
			}
			base.InvokeAsync("CreateMailboxAgentsGroup", new object[]
			{
				partnerContextId
			}, this.CreateMailboxAgentsGroupOperationCompleted, userState);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0001D8AC File Offset: 0x0001BAAC
		private void OnCreateMailboxAgentsGroupOperationCompleted(object arg)
		{
			if (this.CreateMailboxAgentsGroupCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateMailboxAgentsGroupCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001D8EC File Offset: 0x0001BAEC
		[SoapDocumentMethod("http://www.ccs.com/TestServices/GetCompanyContextId", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement(IsNullable = true)]
		public Guid? GetCompanyContextId(string domainOrPrincipalName)
		{
			object[] array = base.Invoke("GetCompanyContextId", new object[]
			{
				domainOrPrincipalName
			});
			return (Guid?)array[0];
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0001D91C File Offset: 0x0001BB1C
		public IAsyncResult BeginGetCompanyContextId(string domainOrPrincipalName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetCompanyContextId", new object[]
			{
				domainOrPrincipalName
			}, callback, asyncState);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0001D944 File Offset: 0x0001BB44
		public Guid? EndGetCompanyContextId(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Guid?)array[0];
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0001D961 File Offset: 0x0001BB61
		public void GetCompanyContextIdAsync(string domainOrPrincipalName)
		{
			this.GetCompanyContextIdAsync(domainOrPrincipalName, null);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0001D96C File Offset: 0x0001BB6C
		public void GetCompanyContextIdAsync(string domainOrPrincipalName, object userState)
		{
			if (this.GetCompanyContextIdOperationCompleted == null)
			{
				this.GetCompanyContextIdOperationCompleted = new SendOrPostCallback(this.OnGetCompanyContextIdOperationCompleted);
			}
			base.InvokeAsync("GetCompanyContextId", new object[]
			{
				domainOrPrincipalName
			}, this.GetCompanyContextIdOperationCompleted, userState);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0001D9B4 File Offset: 0x0001BBB4
		private void OnGetCompanyContextIdOperationCompleted(object arg)
		{
			if (this.GetCompanyContextIdCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetCompanyContextIdCompleted(this, new GetCompanyContextIdCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0001D9FC File Offset: 0x0001BBFC
		[SoapDocumentMethod("http://www.ccs.com/TestServices/GetPartitionId", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GetPartitionId(Guid contextId)
		{
			object[] array = base.Invoke("GetPartitionId", new object[]
			{
				contextId
			});
			return (int)array[0];
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001DA30 File Offset: 0x0001BC30
		public IAsyncResult BeginGetPartitionId(Guid contextId, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetPartitionId", new object[]
			{
				contextId
			}, callback, asyncState);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001DA5C File Offset: 0x0001BC5C
		public int EndGetPartitionId(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (int)array[0];
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0001DA79 File Offset: 0x0001BC79
		public void GetPartitionIdAsync(Guid contextId)
		{
			this.GetPartitionIdAsync(contextId, null);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001DA84 File Offset: 0x0001BC84
		public void GetPartitionIdAsync(Guid contextId, object userState)
		{
			if (this.GetPartitionIdOperationCompleted == null)
			{
				this.GetPartitionIdOperationCompleted = new SendOrPostCallback(this.OnGetPartitionIdOperationCompleted);
			}
			base.InvokeAsync("GetPartitionId", new object[]
			{
				contextId
			}, this.GetPartitionIdOperationCompleted, userState);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0001DAD0 File Offset: 0x0001BCD0
		private void OnGetPartitionIdOperationCompleted(object arg)
		{
			if (this.GetPartitionIdCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetPartitionIdCompleted(this, new GetPartitionIdCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001DB18 File Offset: 0x0001BD18
		[SoapDocumentMethod("http://www.ccs.com/TestServices/GetPartNumberFromSkuId", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string GetPartNumberFromSkuId(Guid skuId)
		{
			object[] array = base.Invoke("GetPartNumberFromSkuId", new object[]
			{
				skuId
			});
			return (string)array[0];
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001DB4C File Offset: 0x0001BD4C
		public IAsyncResult BeginGetPartNumberFromSkuId(Guid skuId, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetPartNumberFromSkuId", new object[]
			{
				skuId
			}, callback, asyncState);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0001DB78 File Offset: 0x0001BD78
		public string EndGetPartNumberFromSkuId(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (string)array[0];
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001DB95 File Offset: 0x0001BD95
		public void GetPartNumberFromSkuIdAsync(Guid skuId)
		{
			this.GetPartNumberFromSkuIdAsync(skuId, null);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0001DBA0 File Offset: 0x0001BDA0
		public void GetPartNumberFromSkuIdAsync(Guid skuId, object userState)
		{
			if (this.GetPartNumberFromSkuIdOperationCompleted == null)
			{
				this.GetPartNumberFromSkuIdOperationCompleted = new SendOrPostCallback(this.OnGetPartNumberFromSkuIdOperationCompleted);
			}
			base.InvokeAsync("GetPartNumberFromSkuId", new object[]
			{
				skuId
			}, this.GetPartNumberFromSkuIdOperationCompleted, userState);
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001DBEC File Offset: 0x0001BDEC
		private void OnGetPartNumberFromSkuIdOperationCompleted(object arg)
		{
			if (this.GetPartNumberFromSkuIdCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetPartNumberFromSkuIdCompleted(this, new GetPartNumberFromSkuIdCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001DC34 File Offset: 0x0001BE34
		[SoapDocumentMethod("http://www.ccs.com/TestServices/GetSkuIdFromPartNumber", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public Guid GetSkuIdFromPartNumber(string partNumber)
		{
			object[] array = base.Invoke("GetSkuIdFromPartNumber", new object[]
			{
				partNumber
			});
			return (Guid)array[0];
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0001DC64 File Offset: 0x0001BE64
		public IAsyncResult BeginGetSkuIdFromPartNumber(string partNumber, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetSkuIdFromPartNumber", new object[]
			{
				partNumber
			}, callback, asyncState);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0001DC8C File Offset: 0x0001BE8C
		public Guid EndGetSkuIdFromPartNumber(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Guid)array[0];
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001DCA9 File Offset: 0x0001BEA9
		public void GetSkuIdFromPartNumberAsync(string partNumber)
		{
			this.GetSkuIdFromPartNumberAsync(partNumber, null);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001DCB4 File Offset: 0x0001BEB4
		public void GetSkuIdFromPartNumberAsync(string partNumber, object userState)
		{
			if (this.GetSkuIdFromPartNumberOperationCompleted == null)
			{
				this.GetSkuIdFromPartNumberOperationCompleted = new SendOrPostCallback(this.OnGetSkuIdFromPartNumberOperationCompleted);
			}
			base.InvokeAsync("GetSkuIdFromPartNumber", new object[]
			{
				partNumber
			}, this.GetSkuIdFromPartNumberOperationCompleted, userState);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0001DCFC File Offset: 0x0001BEFC
		private void OnGetSkuIdFromPartNumberOperationCompleted(object arg)
		{
			if (this.GetSkuIdFromPartNumberCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetSkuIdFromPartNumberCompleted(this, new GetSkuIdFromPartNumberCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0001DD44 File Offset: 0x0001BF44
		[SoapDocumentMethod("http://www.ccs.com/TestServices/GetAccountSubscriptions", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public Subscription[] GetAccountSubscriptions(Guid contextId, Guid accountId)
		{
			object[] array = base.Invoke("GetAccountSubscriptions", new object[]
			{
				contextId,
				accountId
			});
			return (Subscription[])array[0];
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001DD80 File Offset: 0x0001BF80
		public IAsyncResult BeginGetAccountSubscriptions(Guid contextId, Guid accountId, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetAccountSubscriptions", new object[]
			{
				contextId,
				accountId
			}, callback, asyncState);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001DDB8 File Offset: 0x0001BFB8
		public Subscription[] EndGetAccountSubscriptions(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Subscription[])array[0];
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001DDD5 File Offset: 0x0001BFD5
		public void GetAccountSubscriptionsAsync(Guid contextId, Guid accountId)
		{
			this.GetAccountSubscriptionsAsync(contextId, accountId, null);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0001DDE0 File Offset: 0x0001BFE0
		public void GetAccountSubscriptionsAsync(Guid contextId, Guid accountId, object userState)
		{
			if (this.GetAccountSubscriptionsOperationCompleted == null)
			{
				this.GetAccountSubscriptionsOperationCompleted = new SendOrPostCallback(this.OnGetAccountSubscriptionsOperationCompleted);
			}
			base.InvokeAsync("GetAccountSubscriptions", new object[]
			{
				contextId,
				accountId
			}, this.GetAccountSubscriptionsOperationCompleted, userState);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001DE34 File Offset: 0x0001C034
		private void OnGetAccountSubscriptionsOperationCompleted(object arg)
		{
			if (this.GetAccountSubscriptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetAccountSubscriptionsCompleted(this, new GetAccountSubscriptionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001DE7C File Offset: 0x0001C07C
		[SoapDocumentMethod("http://www.ccs.com/TestServices/GetCompanyAccounts", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public Account[] GetCompanyAccounts(Guid contextId)
		{
			object[] array = base.Invoke("GetCompanyAccounts", new object[]
			{
				contextId
			});
			return (Account[])array[0];
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0001DEB0 File Offset: 0x0001C0B0
		public IAsyncResult BeginGetCompanyAccounts(Guid contextId, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetCompanyAccounts", new object[]
			{
				contextId
			}, callback, asyncState);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0001DEDC File Offset: 0x0001C0DC
		public Account[] EndGetCompanyAccounts(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Account[])array[0];
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0001DEF9 File Offset: 0x0001C0F9
		public void GetCompanyAccountsAsync(Guid contextId)
		{
			this.GetCompanyAccountsAsync(contextId, null);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001DF04 File Offset: 0x0001C104
		public void GetCompanyAccountsAsync(Guid contextId, object userState)
		{
			if (this.GetCompanyAccountsOperationCompleted == null)
			{
				this.GetCompanyAccountsOperationCompleted = new SendOrPostCallback(this.OnGetCompanyAccountsOperationCompleted);
			}
			base.InvokeAsync("GetCompanyAccounts", new object[]
			{
				contextId
			}, this.GetCompanyAccountsOperationCompleted, userState);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0001DF50 File Offset: 0x0001C150
		private void OnGetCompanyAccountsOperationCompleted(object arg)
		{
			if (this.GetCompanyAccountsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetCompanyAccountsCompleted(this, new GetCompanyAccountsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001DF98 File Offset: 0x0001C198
		[SoapDocumentMethod("http://www.ccs.com/TestServices/GetCompanyAssignedPlans", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public AssignedPlanValue[] GetCompanyAssignedPlans(Guid contextId)
		{
			object[] array = base.Invoke("GetCompanyAssignedPlans", new object[]
			{
				contextId
			});
			return (AssignedPlanValue[])array[0];
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0001DFCC File Offset: 0x0001C1CC
		public IAsyncResult BeginGetCompanyAssignedPlans(Guid contextId, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetCompanyAssignedPlans", new object[]
			{
				contextId
			}, callback, asyncState);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0001DFF8 File Offset: 0x0001C1F8
		public AssignedPlanValue[] EndGetCompanyAssignedPlans(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (AssignedPlanValue[])array[0];
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001E015 File Offset: 0x0001C215
		public void GetCompanyAssignedPlansAsync(Guid contextId)
		{
			this.GetCompanyAssignedPlansAsync(contextId, null);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0001E020 File Offset: 0x0001C220
		public void GetCompanyAssignedPlansAsync(Guid contextId, object userState)
		{
			if (this.GetCompanyAssignedPlansOperationCompleted == null)
			{
				this.GetCompanyAssignedPlansOperationCompleted = new SendOrPostCallback(this.OnGetCompanyAssignedPlansOperationCompleted);
			}
			base.InvokeAsync("GetCompanyAssignedPlans", new object[]
			{
				contextId
			}, this.GetCompanyAssignedPlansOperationCompleted, userState);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0001E06C File Offset: 0x0001C26C
		private void OnGetCompanyAssignedPlansOperationCompleted(object arg)
		{
			if (this.GetCompanyAssignedPlansCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetCompanyAssignedPlansCompleted(this, new GetCompanyAssignedPlansCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0001E0B4 File Offset: 0x0001C2B4
		[SoapDocumentMethod("http://www.ccs.com/TestServices/GetCompanyProvisionedPlans", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public ProvisionedPlanValue[] GetCompanyProvisionedPlans(Guid contextId)
		{
			object[] array = base.Invoke("GetCompanyProvisionedPlans", new object[]
			{
				contextId
			});
			return (ProvisionedPlanValue[])array[0];
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0001E0E8 File Offset: 0x0001C2E8
		public IAsyncResult BeginGetCompanyProvisionedPlans(Guid contextId, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetCompanyProvisionedPlans", new object[]
			{
				contextId
			}, callback, asyncState);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0001E114 File Offset: 0x0001C314
		public ProvisionedPlanValue[] EndGetCompanyProvisionedPlans(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ProvisionedPlanValue[])array[0];
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0001E131 File Offset: 0x0001C331
		public void GetCompanyProvisionedPlansAsync(Guid contextId)
		{
			this.GetCompanyProvisionedPlansAsync(contextId, null);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0001E13C File Offset: 0x0001C33C
		public void GetCompanyProvisionedPlansAsync(Guid contextId, object userState)
		{
			if (this.GetCompanyProvisionedPlansOperationCompleted == null)
			{
				this.GetCompanyProvisionedPlansOperationCompleted = new SendOrPostCallback(this.OnGetCompanyProvisionedPlansOperationCompleted);
			}
			base.InvokeAsync("GetCompanyProvisionedPlans", new object[]
			{
				contextId
			}, this.GetCompanyProvisionedPlansOperationCompleted, userState);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0001E188 File Offset: 0x0001C388
		private void OnGetCompanyProvisionedPlansOperationCompleted(object arg)
		{
			if (this.GetCompanyProvisionedPlansCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetCompanyProvisionedPlansCompleted(this, new GetCompanyProvisionedPlansCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0001E1D0 File Offset: 0x0001C3D0
		[SoapDocumentMethod("http://www.ccs.com/TestServices/GetCompanySubscriptions", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public Subscription[] GetCompanySubscriptions(Guid contextId)
		{
			object[] array = base.Invoke("GetCompanySubscriptions", new object[]
			{
				contextId
			});
			return (Subscription[])array[0];
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0001E204 File Offset: 0x0001C404
		public IAsyncResult BeginGetCompanySubscriptions(Guid contextId, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetCompanySubscriptions", new object[]
			{
				contextId
			}, callback, asyncState);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0001E230 File Offset: 0x0001C430
		public Subscription[] EndGetCompanySubscriptions(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Subscription[])array[0];
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0001E24D File Offset: 0x0001C44D
		public void GetCompanySubscriptionsAsync(Guid contextId)
		{
			this.GetCompanySubscriptionsAsync(contextId, null);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0001E258 File Offset: 0x0001C458
		public void GetCompanySubscriptionsAsync(Guid contextId, object userState)
		{
			if (this.GetCompanySubscriptionsOperationCompleted == null)
			{
				this.GetCompanySubscriptionsOperationCompleted = new SendOrPostCallback(this.OnGetCompanySubscriptionsOperationCompleted);
			}
			base.InvokeAsync("GetCompanySubscriptions", new object[]
			{
				contextId
			}, this.GetCompanySubscriptionsOperationCompleted, userState);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0001E2A4 File Offset: 0x0001C4A4
		private void OnGetCompanySubscriptionsOperationCompleted(object arg)
		{
			if (this.GetCompanySubscriptionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetCompanySubscriptionsCompleted(this, new GetCompanySubscriptionsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0001E2EC File Offset: 0x0001C4EC
		[SoapDocumentMethod("http://www.ccs.com/TestServices/GetCompanyForeignPrincipalObjects", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public ForeignPrincipal[] GetCompanyForeignPrincipalObjects(Guid contextId)
		{
			object[] array = base.Invoke("GetCompanyForeignPrincipalObjects", new object[]
			{
				contextId
			});
			return (ForeignPrincipal[])array[0];
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0001E320 File Offset: 0x0001C520
		public IAsyncResult BeginGetCompanyForeignPrincipalObjects(Guid contextId, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetCompanyForeignPrincipalObjects", new object[]
			{
				contextId
			}, callback, asyncState);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0001E34C File Offset: 0x0001C54C
		public ForeignPrincipal[] EndGetCompanyForeignPrincipalObjects(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (ForeignPrincipal[])array[0];
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0001E369 File Offset: 0x0001C569
		public void GetCompanyForeignPrincipalObjectsAsync(Guid contextId)
		{
			this.GetCompanyForeignPrincipalObjectsAsync(contextId, null);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0001E374 File Offset: 0x0001C574
		public void GetCompanyForeignPrincipalObjectsAsync(Guid contextId, object userState)
		{
			if (this.GetCompanyForeignPrincipalObjectsOperationCompleted == null)
			{
				this.GetCompanyForeignPrincipalObjectsOperationCompleted = new SendOrPostCallback(this.OnGetCompanyForeignPrincipalObjectsOperationCompleted);
			}
			base.InvokeAsync("GetCompanyForeignPrincipalObjects", new object[]
			{
				contextId
			}, this.GetCompanyForeignPrincipalObjectsOperationCompleted, userState);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001E3C0 File Offset: 0x0001C5C0
		private void OnGetCompanyForeignPrincipalObjectsOperationCompleted(object arg)
		{
			if (this.GetCompanyForeignPrincipalObjectsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetCompanyForeignPrincipalObjectsCompleted(this, new GetCompanyForeignPrincipalObjectsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0001E408 File Offset: 0x0001C608
		[SoapDocumentMethod("http://www.ccs.com/TestServices/SetCompanyProvisioningStatus", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void SetCompanyProvisioningStatus(Guid contextId, ServicePlanProvisioningStatus[] servicePlanProvisioningStatus)
		{
			base.Invoke("SetCompanyProvisioningStatus", new object[]
			{
				contextId,
				servicePlanProvisioningStatus
			});
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0001E438 File Offset: 0x0001C638
		public IAsyncResult BeginSetCompanyProvisioningStatus(Guid contextId, ServicePlanProvisioningStatus[] servicePlanProvisioningStatus, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetCompanyProvisioningStatus", new object[]
			{
				contextId,
				servicePlanProvisioningStatus
			}, callback, asyncState);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0001E468 File Offset: 0x0001C668
		public void EndSetCompanyProvisioningStatus(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0001E472 File Offset: 0x0001C672
		public void SetCompanyProvisioningStatusAsync(Guid contextId, ServicePlanProvisioningStatus[] servicePlanProvisioningStatus)
		{
			this.SetCompanyProvisioningStatusAsync(contextId, servicePlanProvisioningStatus, null);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0001E480 File Offset: 0x0001C680
		public void SetCompanyProvisioningStatusAsync(Guid contextId, ServicePlanProvisioningStatus[] servicePlanProvisioningStatus, object userState)
		{
			if (this.SetCompanyProvisioningStatusOperationCompleted == null)
			{
				this.SetCompanyProvisioningStatusOperationCompleted = new SendOrPostCallback(this.OnSetCompanyProvisioningStatusOperationCompleted);
			}
			base.InvokeAsync("SetCompanyProvisioningStatus", new object[]
			{
				contextId,
				servicePlanProvisioningStatus
			}, this.SetCompanyProvisioningStatusOperationCompleted, userState);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0001E4D0 File Offset: 0x0001C6D0
		private void OnSetCompanyProvisioningStatusOperationCompleted(object arg)
		{
			if (this.SetCompanyProvisioningStatusCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetCompanyProvisioningStatusCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0001E50F File Offset: 0x0001C70F
		[SoapDocumentMethod("http://www.ccs.com/TestServices/ForceRefreshSystemData", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void ForceRefreshSystemData()
		{
			base.Invoke("ForceRefreshSystemData", new object[0]);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001E523 File Offset: 0x0001C723
		public IAsyncResult BeginForceRefreshSystemData(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ForceRefreshSystemData", new object[0], callback, asyncState);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0001E538 File Offset: 0x0001C738
		public void EndForceRefreshSystemData(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001E542 File Offset: 0x0001C742
		public void ForceRefreshSystemDataAsync()
		{
			this.ForceRefreshSystemDataAsync(null);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0001E54B File Offset: 0x0001C74B
		public void ForceRefreshSystemDataAsync(object userState)
		{
			if (this.ForceRefreshSystemDataOperationCompleted == null)
			{
				this.ForceRefreshSystemDataOperationCompleted = new SendOrPostCallback(this.OnForceRefreshSystemDataOperationCompleted);
			}
			base.InvokeAsync("ForceRefreshSystemData", new object[0], this.ForceRefreshSystemDataOperationCompleted, userState);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0001E580 File Offset: 0x0001C780
		private void OnForceRefreshSystemDataOperationCompleted(object arg)
		{
			if (this.ForceRefreshSystemDataCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ForceRefreshSystemDataCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0001E5C0 File Offset: 0x0001C7C0
		[SoapDocumentMethod("http://www.ccs.com/TestServices/BuildRandomAccount", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public Account BuildRandomAccount()
		{
			object[] array = base.Invoke("BuildRandomAccount", new object[0]);
			return (Account)array[0];
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0001E5E7 File Offset: 0x0001C7E7
		public IAsyncResult BeginBuildRandomAccount(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("BuildRandomAccount", new object[0], callback, asyncState);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0001E5FC File Offset: 0x0001C7FC
		public Account EndBuildRandomAccount(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Account)array[0];
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0001E619 File Offset: 0x0001C819
		public void BuildRandomAccountAsync()
		{
			this.BuildRandomAccountAsync(null);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0001E622 File Offset: 0x0001C822
		public void BuildRandomAccountAsync(object userState)
		{
			if (this.BuildRandomAccountOperationCompleted == null)
			{
				this.BuildRandomAccountOperationCompleted = new SendOrPostCallback(this.OnBuildRandomAccountOperationCompleted);
			}
			base.InvokeAsync("BuildRandomAccount", new object[0], this.BuildRandomAccountOperationCompleted, userState);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0001E658 File Offset: 0x0001C858
		private void OnBuildRandomAccountOperationCompleted(object arg)
		{
			if (this.BuildRandomAccountCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.BuildRandomAccountCompleted(this, new BuildRandomAccountCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0001E6A0 File Offset: 0x0001C8A0
		[SoapDocumentMethod("http://www.ccs.com/TestServices/BuildRandomCompany", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public Company BuildRandomCompany(string domainSuffix)
		{
			object[] array = base.Invoke("BuildRandomCompany", new object[]
			{
				domainSuffix
			});
			return (Company)array[0];
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0001E6D0 File Offset: 0x0001C8D0
		public IAsyncResult BeginBuildRandomCompany(string domainSuffix, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("BuildRandomCompany", new object[]
			{
				domainSuffix
			}, callback, asyncState);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0001E6F8 File Offset: 0x0001C8F8
		public Company EndBuildRandomCompany(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Company)array[0];
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0001E715 File Offset: 0x0001C915
		public void BuildRandomCompanyAsync(string domainSuffix)
		{
			this.BuildRandomCompanyAsync(domainSuffix, null);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0001E720 File Offset: 0x0001C920
		public void BuildRandomCompanyAsync(string domainSuffix, object userState)
		{
			if (this.BuildRandomCompanyOperationCompleted == null)
			{
				this.BuildRandomCompanyOperationCompleted = new SendOrPostCallback(this.OnBuildRandomCompanyOperationCompleted);
			}
			base.InvokeAsync("BuildRandomCompany", new object[]
			{
				domainSuffix
			}, this.BuildRandomCompanyOperationCompleted, userState);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0001E768 File Offset: 0x0001C968
		private void OnBuildRandomCompanyOperationCompleted(object arg)
		{
			if (this.BuildRandomCompanyCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.BuildRandomCompanyCompleted(this, new BuildRandomCompanyCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0001E7B0 File Offset: 0x0001C9B0
		[SoapDocumentMethod("http://www.ccs.com/TestServices/BuildRandomCompanyProfile", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public CompanyProfile BuildRandomCompanyProfile()
		{
			object[] array = base.Invoke("BuildRandomCompanyProfile", new object[0]);
			return (CompanyProfile)array[0];
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0001E7D7 File Offset: 0x0001C9D7
		public IAsyncResult BeginBuildRandomCompanyProfile(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("BuildRandomCompanyProfile", new object[0], callback, asyncState);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0001E7EC File Offset: 0x0001C9EC
		public CompanyProfile EndBuildRandomCompanyProfile(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (CompanyProfile)array[0];
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0001E809 File Offset: 0x0001CA09
		public void BuildRandomCompanyProfileAsync()
		{
			this.BuildRandomCompanyProfileAsync(null);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0001E812 File Offset: 0x0001CA12
		public void BuildRandomCompanyProfileAsync(object userState)
		{
			if (this.BuildRandomCompanyProfileOperationCompleted == null)
			{
				this.BuildRandomCompanyProfileOperationCompleted = new SendOrPostCallback(this.OnBuildRandomCompanyProfileOperationCompleted);
			}
			base.InvokeAsync("BuildRandomCompanyProfile", new object[0], this.BuildRandomCompanyProfileOperationCompleted, userState);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0001E848 File Offset: 0x0001CA48
		private void OnBuildRandomCompanyProfileOperationCompleted(object arg)
		{
			if (this.BuildRandomCompanyProfileCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.BuildRandomCompanyProfileCompleted(this, new BuildRandomCompanyProfileCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0001E890 File Offset: 0x0001CA90
		[SoapDocumentMethod("http://www.ccs.com/TestServices/BuildRandomUser", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public User BuildRandomUser()
		{
			object[] array = base.Invoke("BuildRandomUser", new object[0]);
			return (User)array[0];
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0001E8B7 File Offset: 0x0001CAB7
		public IAsyncResult BeginBuildRandomUser(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("BuildRandomUser", new object[0], callback, asyncState);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0001E8CC File Offset: 0x0001CACC
		public User EndBuildRandomUser(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (User)array[0];
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0001E8E9 File Offset: 0x0001CAE9
		public void BuildRandomUserAsync()
		{
			this.BuildRandomUserAsync(null);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0001E8F2 File Offset: 0x0001CAF2
		public void BuildRandomUserAsync(object userState)
		{
			if (this.BuildRandomUserOperationCompleted == null)
			{
				this.BuildRandomUserOperationCompleted = new SendOrPostCallback(this.OnBuildRandomUserOperationCompleted);
			}
			base.InvokeAsync("BuildRandomUser", new object[0], this.BuildRandomUserOperationCompleted, userState);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001E928 File Offset: 0x0001CB28
		private void OnBuildRandomUserOperationCompleted(object arg)
		{
			if (this.BuildRandomUserCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.BuildRandomUserCompleted(this, new BuildRandomUserCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0001E970 File Offset: 0x0001CB70
		[SoapDocumentMethod("http://www.ccs.com/TestServices/BuildRandomSubscription", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public Subscription BuildRandomSubscription(Guid contextId, Guid accountId, Guid skuId)
		{
			object[] array = base.Invoke("BuildRandomSubscription", new object[]
			{
				contextId,
				accountId,
				skuId
			});
			return (Subscription)array[0];
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0001E9B4 File Offset: 0x0001CBB4
		public IAsyncResult BeginBuildRandomSubscription(Guid contextId, Guid accountId, Guid skuId, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("BuildRandomSubscription", new object[]
			{
				contextId,
				accountId,
				skuId
			}, callback, asyncState);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0001E9F4 File Offset: 0x0001CBF4
		public Subscription EndBuildRandomSubscription(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Subscription)array[0];
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0001EA11 File Offset: 0x0001CC11
		public void BuildRandomSubscriptionAsync(Guid contextId, Guid accountId, Guid skuId)
		{
			this.BuildRandomSubscriptionAsync(contextId, accountId, skuId, null);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0001EA20 File Offset: 0x0001CC20
		public void BuildRandomSubscriptionAsync(Guid contextId, Guid accountId, Guid skuId, object userState)
		{
			if (this.BuildRandomSubscriptionOperationCompleted == null)
			{
				this.BuildRandomSubscriptionOperationCompleted = new SendOrPostCallback(this.OnBuildRandomSubscriptionOperationCompleted);
			}
			base.InvokeAsync("BuildRandomSubscription", new object[]
			{
				contextId,
				accountId,
				skuId
			}, this.BuildRandomSubscriptionOperationCompleted, userState);
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0001EA80 File Offset: 0x0001CC80
		private void OnBuildRandomSubscriptionOperationCompleted(object arg)
		{
			if (this.BuildRandomSubscriptionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.BuildRandomSubscriptionCompleted(this, new BuildRandomSubscriptionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0001EAC8 File Offset: 0x0001CCC8
		[SoapDocumentMethod("http://www.ccs.com/TestServices/GetDefaultContractRoleMap", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public RoleMap[] GetDefaultContractRoleMap()
		{
			object[] array = base.Invoke("GetDefaultContractRoleMap", new object[0]);
			return (RoleMap[])array[0];
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0001EAEF File Offset: 0x0001CCEF
		public IAsyncResult BeginGetDefaultContractRoleMap(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetDefaultContractRoleMap", new object[0], callback, asyncState);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0001EB04 File Offset: 0x0001CD04
		public RoleMap[] EndGetDefaultContractRoleMap(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (RoleMap[])array[0];
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0001EB21 File Offset: 0x0001CD21
		public void GetDefaultContractRoleMapAsync()
		{
			this.GetDefaultContractRoleMapAsync(null);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0001EB2A File Offset: 0x0001CD2A
		public void GetDefaultContractRoleMapAsync(object userState)
		{
			if (this.GetDefaultContractRoleMapOperationCompleted == null)
			{
				this.GetDefaultContractRoleMapOperationCompleted = new SendOrPostCallback(this.OnGetDefaultContractRoleMapOperationCompleted);
			}
			base.InvokeAsync("GetDefaultContractRoleMap", new object[0], this.GetDefaultContractRoleMapOperationCompleted, userState);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0001EB60 File Offset: 0x0001CD60
		private void OnGetDefaultContractRoleMapOperationCompleted(object arg)
		{
			if (this.GetDefaultContractRoleMapCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetDefaultContractRoleMapCompleted(this, new GetDefaultContractRoleMapCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001EBA8 File Offset: 0x0001CDA8
		[SoapDocumentMethod("http://www.ccs.com/TestServices/ForceTransitiveReplication", RequestNamespace = "http://www.ccs.com/TestServices/", ResponseNamespace = "http://www.ccs.com/TestServices/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public bool ForceTransitiveReplication()
		{
			object[] array = base.Invoke("ForceTransitiveReplication", new object[0]);
			return (bool)array[0];
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0001EBCF File Offset: 0x0001CDCF
		public IAsyncResult BeginForceTransitiveReplication(AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ForceTransitiveReplication", new object[0], callback, asyncState);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001EBE4 File Offset: 0x0001CDE4
		public bool EndForceTransitiveReplication(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (bool)array[0];
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001EC01 File Offset: 0x0001CE01
		public void ForceTransitiveReplicationAsync()
		{
			this.ForceTransitiveReplicationAsync(null);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001EC0A File Offset: 0x0001CE0A
		public void ForceTransitiveReplicationAsync(object userState)
		{
			if (this.ForceTransitiveReplicationOperationCompleted == null)
			{
				this.ForceTransitiveReplicationOperationCompleted = new SendOrPostCallback(this.OnForceTransitiveReplicationOperationCompleted);
			}
			base.InvokeAsync("ForceTransitiveReplication", new object[0], this.ForceTransitiveReplicationOperationCompleted, userState);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0001EC40 File Offset: 0x0001CE40
		private void OnForceTransitiveReplicationOperationCompleted(object arg)
		{
			if (this.ForceTransitiveReplicationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ForceTransitiveReplicationCompleted(this, new ForceTransitiveReplicationCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0001EC85 File Offset: 0x0001CE85
		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		// Token: 0x0400027B RID: 635
		private SendOrPostCallback IsDomainAvailableOperationCompleted;

		// Token: 0x0400027C RID: 636
		private SendOrPostCallback CreateCompanyOperationCompleted;

		// Token: 0x0400027D RID: 637
		private SendOrPostCallback CreateSyndicatedCompanyOperationCompleted;

		// Token: 0x0400027E RID: 638
		private SendOrPostCallback SetCompanyPartnershipOperationCompleted;

		// Token: 0x0400027F RID: 639
		private SendOrPostCallback UpdateCompanyProfileOperationCompleted;

		// Token: 0x04000280 RID: 640
		private SendOrPostCallback UpdateCompanyTagsOperationCompleted;

		// Token: 0x04000281 RID: 641
		private SendOrPostCallback DeleteCompanyOperationCompleted;

		// Token: 0x04000282 RID: 642
		private SendOrPostCallback ForceDeleteCompanyOperationCompleted;

		// Token: 0x04000283 RID: 643
		private SendOrPostCallback CreateAccountOperationCompleted;

		// Token: 0x04000284 RID: 644
		private SendOrPostCallback RenameAccountOperationCompleted;

		// Token: 0x04000285 RID: 645
		private SendOrPostCallback DeleteAccountOperationCompleted;

		// Token: 0x04000286 RID: 646
		private SendOrPostCallback CreateUpdateDeleteSubscriptionOperationCompleted;

		// Token: 0x04000287 RID: 647
		private SendOrPostCallback CreateCompanyWithSubscriptionsOperationCompleted;

		// Token: 0x04000288 RID: 648
		private SendOrPostCallback SignupOperationCompleted;

		// Token: 0x04000289 RID: 649
		private SendOrPostCallback SignupWithCompanyTagsOperationCompleted;

		// Token: 0x0400028A RID: 650
		private SendOrPostCallback PromoteToPartnerOperationCompleted;

		// Token: 0x0400028B RID: 651
		private SendOrPostCallback DemoteToCompanyOperationCompleted;

		// Token: 0x0400028C RID: 652
		private SendOrPostCallback ForceDemoteToCompanyOperationCompleted;

		// Token: 0x0400028D RID: 653
		private SendOrPostCallback AddServiceTypeOperationCompleted;

		// Token: 0x0400028E RID: 654
		private SendOrPostCallback RemoveServiceTypeOperationCompleted;

		// Token: 0x0400028F RID: 655
		private SendOrPostCallback ListServicesForPartnershipOperationCompleted;

		// Token: 0x04000290 RID: 656
		private SendOrPostCallback AssociateToPartnerOperationCompleted;

		// Token: 0x04000291 RID: 657
		private SendOrPostCallback DeletePartnerContractOperationCompleted;

		// Token: 0x04000292 RID: 658
		private SendOrPostCallback CreatePartnerOperationCompleted;

		// Token: 0x04000293 RID: 659
		private SendOrPostCallback CreateMailboxAgentsGroupOperationCompleted;

		// Token: 0x04000294 RID: 660
		private SendOrPostCallback GetCompanyContextIdOperationCompleted;

		// Token: 0x04000295 RID: 661
		private SendOrPostCallback GetPartitionIdOperationCompleted;

		// Token: 0x04000296 RID: 662
		private SendOrPostCallback GetPartNumberFromSkuIdOperationCompleted;

		// Token: 0x04000297 RID: 663
		private SendOrPostCallback GetSkuIdFromPartNumberOperationCompleted;

		// Token: 0x04000298 RID: 664
		private SendOrPostCallback GetAccountSubscriptionsOperationCompleted;

		// Token: 0x04000299 RID: 665
		private SendOrPostCallback GetCompanyAccountsOperationCompleted;

		// Token: 0x0400029A RID: 666
		private SendOrPostCallback GetCompanyAssignedPlansOperationCompleted;

		// Token: 0x0400029B RID: 667
		private SendOrPostCallback GetCompanyProvisionedPlansOperationCompleted;

		// Token: 0x0400029C RID: 668
		private SendOrPostCallback GetCompanySubscriptionsOperationCompleted;

		// Token: 0x0400029D RID: 669
		private SendOrPostCallback GetCompanyForeignPrincipalObjectsOperationCompleted;

		// Token: 0x0400029E RID: 670
		private SendOrPostCallback SetCompanyProvisioningStatusOperationCompleted;

		// Token: 0x0400029F RID: 671
		private SendOrPostCallback ForceRefreshSystemDataOperationCompleted;

		// Token: 0x040002A0 RID: 672
		private SendOrPostCallback BuildRandomAccountOperationCompleted;

		// Token: 0x040002A1 RID: 673
		private SendOrPostCallback BuildRandomCompanyOperationCompleted;

		// Token: 0x040002A2 RID: 674
		private SendOrPostCallback BuildRandomCompanyProfileOperationCompleted;

		// Token: 0x040002A3 RID: 675
		private SendOrPostCallback BuildRandomUserOperationCompleted;

		// Token: 0x040002A4 RID: 676
		private SendOrPostCallback BuildRandomSubscriptionOperationCompleted;

		// Token: 0x040002A5 RID: 677
		private SendOrPostCallback GetDefaultContractRoleMapOperationCompleted;

		// Token: 0x040002A6 RID: 678
		private SendOrPostCallback ForceTransitiveReplicationOperationCompleted;
	}
}
