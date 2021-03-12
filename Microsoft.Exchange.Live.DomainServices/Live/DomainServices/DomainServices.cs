using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000002 RID: 2
	[DebuggerStepThrough]
	[WebServiceBinding(Name = "DomainServicesSoap", Namespace = "http://domains.live.com/Service/DomainServices/V1.0")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	public class DomainServices : SoapHttpClientProtocol
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public DomainServices()
		{
			base.Url = "https://domains-tst.live-int.com/service/DomainServices.asmx";
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020E3 File Offset: 0x000002E3
		// (set) Token: 0x06000003 RID: 3 RVA: 0x000020EB File Offset: 0x000002EB
		public AdminPassportAuthHeader AdminPassportAuthHeaderValue
		{
			get
			{
				return this.adminPassportAuthHeaderValueField;
			}
			set
			{
				this.adminPassportAuthHeaderValueField = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020F4 File Offset: 0x000002F4
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020FC File Offset: 0x000002FC
		public ManagementCertificateAuthHeader ManagementCertificateAuthHeaderValue
		{
			get
			{
				return this.managementCertificateAuthHeaderValueField;
			}
			set
			{
				this.managementCertificateAuthHeaderValueField = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002105 File Offset: 0x00000305
		// (set) Token: 0x06000007 RID: 7 RVA: 0x0000210D File Offset: 0x0000030D
		public PartnerAuthHeader PartnerAuthHeaderValue
		{
			get
			{
				return this.partnerAuthHeaderValueField;
			}
			set
			{
				this.partnerAuthHeaderValueField = value;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000008 RID: 8 RVA: 0x00002118 File Offset: 0x00000318
		// (remove) Token: 0x06000009 RID: 9 RVA: 0x00002150 File Offset: 0x00000350
		public event TestConnectionCompletedEventHandler TestConnectionCompleted;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600000A RID: 10 RVA: 0x00002188 File Offset: 0x00000388
		// (remove) Token: 0x0600000B RID: 11 RVA: 0x000021C0 File Offset: 0x000003C0
		public event GetDomainAvailabilityCompletedEventHandler GetDomainAvailabilityCompleted;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600000C RID: 12 RVA: 0x000021F8 File Offset: 0x000003F8
		// (remove) Token: 0x0600000D RID: 13 RVA: 0x00002230 File Offset: 0x00000430
		public event GetDomainInfoCompletedEventHandler GetDomainInfoCompleted;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600000E RID: 14 RVA: 0x00002268 File Offset: 0x00000468
		// (remove) Token: 0x0600000F RID: 15 RVA: 0x000022A0 File Offset: 0x000004A0
		public event GetDomainInfoExCompletedEventHandler GetDomainInfoExCompleted;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000010 RID: 16 RVA: 0x000022D8 File Offset: 0x000004D8
		// (remove) Token: 0x06000011 RID: 17 RVA: 0x00002310 File Offset: 0x00000510
		public event ReserveDomainCompletedEventHandler ReserveDomainCompleted;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000012 RID: 18 RVA: 0x00002348 File Offset: 0x00000548
		// (remove) Token: 0x06000013 RID: 19 RVA: 0x00002380 File Offset: 0x00000580
		public event ReleaseDomainCompletedEventHandler ReleaseDomainCompleted;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000014 RID: 20 RVA: 0x000023B8 File Offset: 0x000005B8
		// (remove) Token: 0x06000015 RID: 21 RVA: 0x000023F0 File Offset: 0x000005F0
		public event ProcessDomainCompletedEventHandler ProcessDomainCompleted;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000016 RID: 22 RVA: 0x00002428 File Offset: 0x00000628
		// (remove) Token: 0x06000017 RID: 23 RVA: 0x00002460 File Offset: 0x00000660
		public event GetMemberTypeCompletedEventHandler GetMemberTypeCompleted;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000018 RID: 24 RVA: 0x00002498 File Offset: 0x00000698
		// (remove) Token: 0x06000019 RID: 25 RVA: 0x000024D0 File Offset: 0x000006D0
		public event MemberNameToNetIdCompletedEventHandler MemberNameToNetIdCompleted;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600001A RID: 26 RVA: 0x00002508 File Offset: 0x00000708
		// (remove) Token: 0x0600001B RID: 27 RVA: 0x00002540 File Offset: 0x00000740
		public event NetIdToMemberNameCompletedEventHandler NetIdToMemberNameCompleted;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600001C RID: 28 RVA: 0x00002578 File Offset: 0x00000778
		// (remove) Token: 0x0600001D RID: 29 RVA: 0x000025B0 File Offset: 0x000007B0
		public event GetCountMembersCompletedEventHandler GetCountMembersCompleted;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600001E RID: 30 RVA: 0x000025E8 File Offset: 0x000007E8
		// (remove) Token: 0x0600001F RID: 31 RVA: 0x00002620 File Offset: 0x00000820
		public event EnumMembersCompletedEventHandler EnumMembersCompleted;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000020 RID: 32 RVA: 0x00002658 File Offset: 0x00000858
		// (remove) Token: 0x06000021 RID: 33 RVA: 0x00002690 File Offset: 0x00000890
		public event CreateMemberCompletedEventHandler CreateMemberCompleted;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000022 RID: 34 RVA: 0x000026C8 File Offset: 0x000008C8
		// (remove) Token: 0x06000023 RID: 35 RVA: 0x00002700 File Offset: 0x00000900
		public event CreateMemberEncryptedCompletedEventHandler CreateMemberEncryptedCompleted;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000024 RID: 36 RVA: 0x00002738 File Offset: 0x00000938
		// (remove) Token: 0x06000025 RID: 37 RVA: 0x00002770 File Offset: 0x00000970
		public event CreateMemberExCompletedEventHandler CreateMemberExCompleted;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000026 RID: 38 RVA: 0x000027A8 File Offset: 0x000009A8
		// (remove) Token: 0x06000027 RID: 39 RVA: 0x000027E0 File Offset: 0x000009E0
		public event CreateMemberEncryptedExCompletedEventHandler CreateMemberEncryptedExCompleted;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000028 RID: 40 RVA: 0x00002818 File Offset: 0x00000A18
		// (remove) Token: 0x06000029 RID: 41 RVA: 0x00002850 File Offset: 0x00000A50
		public event AddBrandInfoCompletedEventHandler AddBrandInfoCompleted;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x0600002A RID: 42 RVA: 0x00002888 File Offset: 0x00000A88
		// (remove) Token: 0x0600002B RID: 43 RVA: 0x000028C0 File Offset: 0x00000AC0
		public event RemoveBrandInfoCompletedEventHandler RemoveBrandInfoCompleted;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x0600002C RID: 44 RVA: 0x000028F8 File Offset: 0x00000AF8
		// (remove) Token: 0x0600002D RID: 45 RVA: 0x00002930 File Offset: 0x00000B30
		public event RenameMemberCompletedEventHandler RenameMemberCompleted;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x0600002E RID: 46 RVA: 0x00002968 File Offset: 0x00000B68
		// (remove) Token: 0x0600002F RID: 47 RVA: 0x000029A0 File Offset: 0x00000BA0
		public event SetMemberPropertiesCompletedEventHandler SetMemberPropertiesCompleted;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000030 RID: 48 RVA: 0x000029D8 File Offset: 0x00000BD8
		// (remove) Token: 0x06000031 RID: 49 RVA: 0x00002A10 File Offset: 0x00000C10
		public event GetMemberPropertiesCompletedEventHandler GetMemberPropertiesCompleted;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000032 RID: 50 RVA: 0x00002A48 File Offset: 0x00000C48
		// (remove) Token: 0x06000033 RID: 51 RVA: 0x00002A80 File Offset: 0x00000C80
		public event EvictMemberCompletedEventHandler EvictMemberCompleted;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000034 RID: 52 RVA: 0x00002AB8 File Offset: 0x00000CB8
		// (remove) Token: 0x06000035 RID: 53 RVA: 0x00002AF0 File Offset: 0x00000CF0
		public event ResetMemberPasswordCompletedEventHandler ResetMemberPasswordCompleted;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000036 RID: 54 RVA: 0x00002B28 File Offset: 0x00000D28
		// (remove) Token: 0x06000037 RID: 55 RVA: 0x00002B60 File Offset: 0x00000D60
		public event ResetMemberPasswordEncryptedCompletedEventHandler ResetMemberPasswordEncryptedCompleted;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000038 RID: 56 RVA: 0x00002B98 File Offset: 0x00000D98
		// (remove) Token: 0x06000039 RID: 57 RVA: 0x00002BD0 File Offset: 0x00000DD0
		public event BlockMemberEmailCompletedEventHandler BlockMemberEmailCompleted;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x0600003A RID: 58 RVA: 0x00002C08 File Offset: 0x00000E08
		// (remove) Token: 0x0600003B RID: 59 RVA: 0x00002C40 File Offset: 0x00000E40
		public event ImportUnmanagedMemberCompletedEventHandler ImportUnmanagedMemberCompleted;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x0600003C RID: 60 RVA: 0x00002C78 File Offset: 0x00000E78
		// (remove) Token: 0x0600003D RID: 61 RVA: 0x00002CB0 File Offset: 0x00000EB0
		public event EvictUnmanagedMemberCompletedEventHandler EvictUnmanagedMemberCompleted;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x0600003E RID: 62 RVA: 0x00002CE8 File Offset: 0x00000EE8
		// (remove) Token: 0x0600003F RID: 63 RVA: 0x00002D20 File Offset: 0x00000F20
		public event EvictAllUnmanagedMembersCompletedEventHandler EvictAllUnmanagedMembersCompleted;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000040 RID: 64 RVA: 0x00002D58 File Offset: 0x00000F58
		// (remove) Token: 0x06000041 RID: 65 RVA: 0x00002D90 File Offset: 0x00000F90
		public event EnableOpenMembershipCompletedEventHandler EnableOpenMembershipCompleted;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000042 RID: 66 RVA: 0x00002DC8 File Offset: 0x00000FC8
		// (remove) Token: 0x06000043 RID: 67 RVA: 0x00002E00 File Offset: 0x00001000
		public event ProvisionMemberSubscriptionCompletedEventHandler ProvisionMemberSubscriptionCompleted;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000044 RID: 68 RVA: 0x00002E38 File Offset: 0x00001038
		// (remove) Token: 0x06000045 RID: 69 RVA: 0x00002E70 File Offset: 0x00001070
		public event DeprovisionMemberSubscriptionCompletedEventHandler DeprovisionMemberSubscriptionCompleted;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000046 RID: 70 RVA: 0x00002EA8 File Offset: 0x000010A8
		// (remove) Token: 0x06000047 RID: 71 RVA: 0x00002EE0 File Offset: 0x000010E0
		public event ConvertMemberSubscriptionCompletedEventHandler ConvertMemberSubscriptionCompleted;

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000048 RID: 72 RVA: 0x00002F18 File Offset: 0x00001118
		// (remove) Token: 0x06000049 RID: 73 RVA: 0x00002F50 File Offset: 0x00001150
		public event MemberHasSubscriptionCompletedEventHandler MemberHasSubscriptionCompleted;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x0600004A RID: 74 RVA: 0x00002F88 File Offset: 0x00001188
		// (remove) Token: 0x0600004B RID: 75 RVA: 0x00002FC0 File Offset: 0x000011C0
		public event SuspendEmailCompletedEventHandler SuspendEmailCompleted;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x0600004C RID: 76 RVA: 0x00002FF8 File Offset: 0x000011F8
		// (remove) Token: 0x0600004D RID: 77 RVA: 0x00003030 File Offset: 0x00001230
		public event GetMxRecordsCompletedEventHandler GetMxRecordsCompleted;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x0600004E RID: 78 RVA: 0x00003068 File Offset: 0x00001268
		// (remove) Token: 0x0600004F RID: 79 RVA: 0x000030A0 File Offset: 0x000012A0
		public event MemberHasMailboxCompletedEventHandler MemberHasMailboxCompleted;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000050 RID: 80 RVA: 0x000030D8 File Offset: 0x000012D8
		// (remove) Token: 0x06000051 RID: 81 RVA: 0x00003110 File Offset: 0x00001310
		public event CompleteMemberEmailMigrationCompletedEventHandler CompleteMemberEmailMigrationCompleted;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06000052 RID: 82 RVA: 0x00003148 File Offset: 0x00001348
		// (remove) Token: 0x06000053 RID: 83 RVA: 0x00003180 File Offset: 0x00001380
		public event CompleteDomainEmailMigrationCompletedEventHandler CompleteDomainEmailMigrationCompleted;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000054 RID: 84 RVA: 0x000031B8 File Offset: 0x000013B8
		// (remove) Token: 0x06000055 RID: 85 RVA: 0x000031F0 File Offset: 0x000013F0
		public event CreateByodRequestCompletedEventHandler CreateByodRequestCompleted;

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06000056 RID: 86 RVA: 0x00003228 File Offset: 0x00001428
		// (remove) Token: 0x06000057 RID: 87 RVA: 0x00003260 File Offset: 0x00001460
		public event EnumByodDomainsCompletedEventHandler EnumByodDomainsCompleted;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06000058 RID: 88 RVA: 0x00003298 File Offset: 0x00001498
		// (remove) Token: 0x06000059 RID: 89 RVA: 0x000032D0 File Offset: 0x000014D0
		public event GetAdminsCompletedEventHandler GetAdminsCompleted;

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x0600005A RID: 90 RVA: 0x00003308 File Offset: 0x00001508
		// (remove) Token: 0x0600005B RID: 91 RVA: 0x00003340 File Offset: 0x00001540
		public event GetManagementCertificateCompletedEventHandler GetManagementCertificateCompleted;

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x0600005C RID: 92 RVA: 0x00003378 File Offset: 0x00001578
		// (remove) Token: 0x0600005D RID: 93 RVA: 0x000033B0 File Offset: 0x000015B0
		public event SetManagementCertificateCompletedEventHandler SetManagementCertificateCompleted;

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x0600005E RID: 94 RVA: 0x000033E8 File Offset: 0x000015E8
		// (remove) Token: 0x0600005F RID: 95 RVA: 0x00003420 File Offset: 0x00001620
		public event SetManagementPermissionsCompletedEventHandler SetManagementPermissionsCompleted;

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06000060 RID: 96 RVA: 0x00003458 File Offset: 0x00001658
		// (remove) Token: 0x06000061 RID: 97 RVA: 0x00003490 File Offset: 0x00001690
		public event GetMaxMembersCompletedEventHandler GetMaxMembersCompleted;

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06000062 RID: 98 RVA: 0x000034C8 File Offset: 0x000016C8
		// (remove) Token: 0x06000063 RID: 99 RVA: 0x00003500 File Offset: 0x00001700
		public event SetMaxMembersCompletedEventHandler SetMaxMembersCompleted;

		// Token: 0x06000064 RID: 100 RVA: 0x00003538 File Offset: 0x00001738
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/TestConnection", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("PartnerAuthHeaderValue")]
		[SoapHeader("AdminPassportAuthHeaderValue")]
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		public string TestConnection(string input)
		{
			object[] array = base.Invoke("TestConnection", new object[]
			{
				input
			});
			return (string)array[0];
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003568 File Offset: 0x00001768
		public IAsyncResult BeginTestConnection(string input, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("TestConnection", new object[]
			{
				input
			}, callback, asyncState);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003590 File Offset: 0x00001790
		public string EndTestConnection(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (string)array[0];
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000035AD File Offset: 0x000017AD
		public void TestConnectionAsync(string input)
		{
			this.TestConnectionAsync(input, null);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000035B8 File Offset: 0x000017B8
		public void TestConnectionAsync(string input, object userState)
		{
			if (this.TestConnectionOperationCompleted == null)
			{
				this.TestConnectionOperationCompleted = new SendOrPostCallback(this.OnTestConnectionOperationCompleted);
			}
			base.InvokeAsync("TestConnection", new object[]
			{
				input
			}, this.TestConnectionOperationCompleted, userState);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003600 File Offset: 0x00001800
		private void OnTestConnectionOperationCompleted(object arg)
		{
			if (this.TestConnectionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.TestConnectionCompleted(this, new TestConnectionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003648 File Offset: 0x00001848
		[SoapHeader("PartnerAuthHeaderValue")]
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/GetDomainAvailability", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public DomainAvailability GetDomainAvailability(string domainName)
		{
			object[] array = base.Invoke("GetDomainAvailability", new object[]
			{
				domainName
			});
			return (DomainAvailability)array[0];
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003678 File Offset: 0x00001878
		public IAsyncResult BeginGetDomainAvailability(string domainName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetDomainAvailability", new object[]
			{
				domainName
			}, callback, asyncState);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000036A0 File Offset: 0x000018A0
		public DomainAvailability EndGetDomainAvailability(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (DomainAvailability)array[0];
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000036BD File Offset: 0x000018BD
		public void GetDomainAvailabilityAsync(string domainName)
		{
			this.GetDomainAvailabilityAsync(domainName, null);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000036C8 File Offset: 0x000018C8
		public void GetDomainAvailabilityAsync(string domainName, object userState)
		{
			if (this.GetDomainAvailabilityOperationCompleted == null)
			{
				this.GetDomainAvailabilityOperationCompleted = new SendOrPostCallback(this.OnGetDomainAvailabilityOperationCompleted);
			}
			base.InvokeAsync("GetDomainAvailability", new object[]
			{
				domainName
			}, this.GetDomainAvailabilityOperationCompleted, userState);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003710 File Offset: 0x00001910
		private void OnGetDomainAvailabilityOperationCompleted(object arg)
		{
			if (this.GetDomainAvailabilityCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetDomainAvailabilityCompleted(this, new GetDomainAvailabilityCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003758 File Offset: 0x00001958
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/GetDomainInfo", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		[SoapHeader("AdminPassportAuthHeaderValue")]
		[SoapHeader("PartnerAuthHeaderValue")]
		public DomainInfo GetDomainInfo(string domainName)
		{
			object[] array = base.Invoke("GetDomainInfo", new object[]
			{
				domainName
			});
			return (DomainInfo)array[0];
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003788 File Offset: 0x00001988
		public IAsyncResult BeginGetDomainInfo(string domainName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetDomainInfo", new object[]
			{
				domainName
			}, callback, asyncState);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000037B0 File Offset: 0x000019B0
		public DomainInfo EndGetDomainInfo(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (DomainInfo)array[0];
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000037CD File Offset: 0x000019CD
		public void GetDomainInfoAsync(string domainName)
		{
			this.GetDomainInfoAsync(domainName, null);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000037D8 File Offset: 0x000019D8
		public void GetDomainInfoAsync(string domainName, object userState)
		{
			if (this.GetDomainInfoOperationCompleted == null)
			{
				this.GetDomainInfoOperationCompleted = new SendOrPostCallback(this.OnGetDomainInfoOperationCompleted);
			}
			base.InvokeAsync("GetDomainInfo", new object[]
			{
				domainName
			}, this.GetDomainInfoOperationCompleted, userState);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003820 File Offset: 0x00001A20
		private void OnGetDomainInfoOperationCompleted(object arg)
		{
			if (this.GetDomainInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetDomainInfoCompleted(this, new GetDomainInfoCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003868 File Offset: 0x00001A68
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/GetDomainInfoEx", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("AdminPassportAuthHeaderValue")]
		[SoapHeader("PartnerAuthHeaderValue")]
		public DomainInfoEx GetDomainInfoEx(string domainName)
		{
			object[] array = base.Invoke("GetDomainInfoEx", new object[]
			{
				domainName
			});
			return (DomainInfoEx)array[0];
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003898 File Offset: 0x00001A98
		public IAsyncResult BeginGetDomainInfoEx(string domainName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetDomainInfoEx", new object[]
			{
				domainName
			}, callback, asyncState);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000038C0 File Offset: 0x00001AC0
		public DomainInfoEx EndGetDomainInfoEx(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (DomainInfoEx)array[0];
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000038DD File Offset: 0x00001ADD
		public void GetDomainInfoExAsync(string domainName)
		{
			this.GetDomainInfoExAsync(domainName, null);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000038E8 File Offset: 0x00001AE8
		public void GetDomainInfoExAsync(string domainName, object userState)
		{
			if (this.GetDomainInfoExOperationCompleted == null)
			{
				this.GetDomainInfoExOperationCompleted = new SendOrPostCallback(this.OnGetDomainInfoExOperationCompleted);
			}
			base.InvokeAsync("GetDomainInfoEx", new object[]
			{
				domainName
			}, this.GetDomainInfoExOperationCompleted, userState);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003930 File Offset: 0x00001B30
		private void OnGetDomainInfoExOperationCompleted(object arg)
		{
			if (this.GetDomainInfoExCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetDomainInfoExCompleted(this, new GetDomainInfoExCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003978 File Offset: 0x00001B78
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/ReserveDomain", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("PartnerAuthHeaderValue")]
		public DomainInfoEx ReserveDomain(string domainName, string domainConfigId, bool processSynch)
		{
			object[] array = base.Invoke("ReserveDomain", new object[]
			{
				domainName,
				domainConfigId,
				processSynch
			});
			return (DomainInfoEx)array[0];
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000039B4 File Offset: 0x00001BB4
		public IAsyncResult BeginReserveDomain(string domainName, string domainConfigId, bool processSynch, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ReserveDomain", new object[]
			{
				domainName,
				domainConfigId,
				processSynch
			}, callback, asyncState);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000039EC File Offset: 0x00001BEC
		public DomainInfoEx EndReserveDomain(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (DomainInfoEx)array[0];
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003A09 File Offset: 0x00001C09
		public void ReserveDomainAsync(string domainName, string domainConfigId, bool processSynch)
		{
			this.ReserveDomainAsync(domainName, domainConfigId, processSynch, null);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003A18 File Offset: 0x00001C18
		public void ReserveDomainAsync(string domainName, string domainConfigId, bool processSynch, object userState)
		{
			if (this.ReserveDomainOperationCompleted == null)
			{
				this.ReserveDomainOperationCompleted = new SendOrPostCallback(this.OnReserveDomainOperationCompleted);
			}
			base.InvokeAsync("ReserveDomain", new object[]
			{
				domainName,
				domainConfigId,
				processSynch
			}, this.ReserveDomainOperationCompleted, userState);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003A6C File Offset: 0x00001C6C
		private void OnReserveDomainOperationCompleted(object arg)
		{
			if (this.ReserveDomainCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ReserveDomainCompleted(this, new ReserveDomainCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003AB4 File Offset: 0x00001CB4
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/ReleaseDomain", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("PartnerAuthHeaderValue")]
		public void ReleaseDomain(string domainName)
		{
			base.Invoke("ReleaseDomain", new object[]
			{
				domainName
			});
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003ADC File Offset: 0x00001CDC
		public IAsyncResult BeginReleaseDomain(string domainName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ReleaseDomain", new object[]
			{
				domainName
			}, callback, asyncState);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003B02 File Offset: 0x00001D02
		public void EndReleaseDomain(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003B0C File Offset: 0x00001D0C
		public void ReleaseDomainAsync(string domainName)
		{
			this.ReleaseDomainAsync(domainName, null);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003B18 File Offset: 0x00001D18
		public void ReleaseDomainAsync(string domainName, object userState)
		{
			if (this.ReleaseDomainOperationCompleted == null)
			{
				this.ReleaseDomainOperationCompleted = new SendOrPostCallback(this.OnReleaseDomainOperationCompleted);
			}
			base.InvokeAsync("ReleaseDomain", new object[]
			{
				domainName
			}, this.ReleaseDomainOperationCompleted, userState);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003B60 File Offset: 0x00001D60
		private void OnReleaseDomainOperationCompleted(object arg)
		{
			if (this.ReleaseDomainCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ReleaseDomainCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003BA0 File Offset: 0x00001DA0
		[SoapHeader("PartnerAuthHeaderValue")]
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/ProcessDomain", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public DomainInfoEx ProcessDomain(string domainName, bool processSynch)
		{
			object[] array = base.Invoke("ProcessDomain", new object[]
			{
				domainName,
				processSynch
			});
			return (DomainInfoEx)array[0];
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003BD8 File Offset: 0x00001DD8
		public IAsyncResult BeginProcessDomain(string domainName, bool processSynch, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ProcessDomain", new object[]
			{
				domainName,
				processSynch
			}, callback, asyncState);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003C08 File Offset: 0x00001E08
		public DomainInfoEx EndProcessDomain(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (DomainInfoEx)array[0];
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003C25 File Offset: 0x00001E25
		public void ProcessDomainAsync(string domainName, bool processSynch)
		{
			this.ProcessDomainAsync(domainName, processSynch, null);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003C30 File Offset: 0x00001E30
		public void ProcessDomainAsync(string domainName, bool processSynch, object userState)
		{
			if (this.ProcessDomainOperationCompleted == null)
			{
				this.ProcessDomainOperationCompleted = new SendOrPostCallback(this.OnProcessDomainOperationCompleted);
			}
			base.InvokeAsync("ProcessDomain", new object[]
			{
				domainName,
				processSynch
			}, this.ProcessDomainOperationCompleted, userState);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003C80 File Offset: 0x00001E80
		private void OnProcessDomainOperationCompleted(object arg)
		{
			if (this.ProcessDomainCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ProcessDomainCompleted(this, new ProcessDomainCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003CC8 File Offset: 0x00001EC8
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		[SoapHeader("AdminPassportAuthHeaderValue")]
		[SoapHeader("PartnerAuthHeaderValue")]
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/GetMemberType", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public MemberType GetMemberType(string memberNameIn)
		{
			object[] array = base.Invoke("GetMemberType", new object[]
			{
				memberNameIn
			});
			return (MemberType)array[0];
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003CF8 File Offset: 0x00001EF8
		public IAsyncResult BeginGetMemberType(string memberNameIn, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetMemberType", new object[]
			{
				memberNameIn
			}, callback, asyncState);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003D20 File Offset: 0x00001F20
		public MemberType EndGetMemberType(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (MemberType)array[0];
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003D3D File Offset: 0x00001F3D
		public void GetMemberTypeAsync(string memberNameIn)
		{
			this.GetMemberTypeAsync(memberNameIn, null);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003D48 File Offset: 0x00001F48
		public void GetMemberTypeAsync(string memberNameIn, object userState)
		{
			if (this.GetMemberTypeOperationCompleted == null)
			{
				this.GetMemberTypeOperationCompleted = new SendOrPostCallback(this.OnGetMemberTypeOperationCompleted);
			}
			base.InvokeAsync("GetMemberType", new object[]
			{
				memberNameIn
			}, this.GetMemberTypeOperationCompleted, userState);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003D90 File Offset: 0x00001F90
		private void OnGetMemberTypeOperationCompleted(object arg)
		{
			if (this.GetMemberTypeCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetMemberTypeCompleted(this, new GetMemberTypeCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003DD8 File Offset: 0x00001FD8
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/MemberNameToNetId", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("PartnerAuthHeaderValue")]
		public string MemberNameToNetId(string memberNameIn)
		{
			object[] array = base.Invoke("MemberNameToNetId", new object[]
			{
				memberNameIn
			});
			return (string)array[0];
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003E08 File Offset: 0x00002008
		public IAsyncResult BeginMemberNameToNetId(string memberNameIn, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("MemberNameToNetId", new object[]
			{
				memberNameIn
			}, callback, asyncState);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003E30 File Offset: 0x00002030
		public string EndMemberNameToNetId(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (string)array[0];
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003E4D File Offset: 0x0000204D
		public void MemberNameToNetIdAsync(string memberNameIn)
		{
			this.MemberNameToNetIdAsync(memberNameIn, null);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003E58 File Offset: 0x00002058
		public void MemberNameToNetIdAsync(string memberNameIn, object userState)
		{
			if (this.MemberNameToNetIdOperationCompleted == null)
			{
				this.MemberNameToNetIdOperationCompleted = new SendOrPostCallback(this.OnMemberNameToNetIdOperationCompleted);
			}
			base.InvokeAsync("MemberNameToNetId", new object[]
			{
				memberNameIn
			}, this.MemberNameToNetIdOperationCompleted, userState);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003EA0 File Offset: 0x000020A0
		private void OnMemberNameToNetIdOperationCompleted(object arg)
		{
			if (this.MemberNameToNetIdCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.MemberNameToNetIdCompleted(this, new MemberNameToNetIdCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003EE8 File Offset: 0x000020E8
		[SoapHeader("PartnerAuthHeaderValue")]
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/NetIdToMemberName", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string NetIdToMemberName(string netIdIn)
		{
			object[] array = base.Invoke("NetIdToMemberName", new object[]
			{
				netIdIn
			});
			return (string)array[0];
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003F18 File Offset: 0x00002118
		public IAsyncResult BeginNetIdToMemberName(string netIdIn, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("NetIdToMemberName", new object[]
			{
				netIdIn
			}, callback, asyncState);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003F40 File Offset: 0x00002140
		public string EndNetIdToMemberName(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (string)array[0];
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003F5D File Offset: 0x0000215D
		public void NetIdToMemberNameAsync(string netIdIn)
		{
			this.NetIdToMemberNameAsync(netIdIn, null);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003F68 File Offset: 0x00002168
		public void NetIdToMemberNameAsync(string netIdIn, object userState)
		{
			if (this.NetIdToMemberNameOperationCompleted == null)
			{
				this.NetIdToMemberNameOperationCompleted = new SendOrPostCallback(this.OnNetIdToMemberNameOperationCompleted);
			}
			base.InvokeAsync("NetIdToMemberName", new object[]
			{
				netIdIn
			}, this.NetIdToMemberNameOperationCompleted, userState);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003FB0 File Offset: 0x000021B0
		private void OnNetIdToMemberNameOperationCompleted(object arg)
		{
			if (this.NetIdToMemberNameCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.NetIdToMemberNameCompleted(this, new NetIdToMemberNameCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003FF8 File Offset: 0x000021F8
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/GetCountMembers", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("AdminPassportAuthHeaderValue")]
		[SoapHeader("PartnerAuthHeaderValue")]
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		public int GetCountMembers(string domainName)
		{
			object[] array = base.Invoke("GetCountMembers", new object[]
			{
				domainName
			});
			return (int)array[0];
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004028 File Offset: 0x00002228
		public IAsyncResult BeginGetCountMembers(string domainName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetCountMembers", new object[]
			{
				domainName
			}, callback, asyncState);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004050 File Offset: 0x00002250
		public int EndGetCountMembers(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (int)array[0];
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000406D File Offset: 0x0000226D
		public void GetCountMembersAsync(string domainName)
		{
			this.GetCountMembersAsync(domainName, null);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004078 File Offset: 0x00002278
		public void GetCountMembersAsync(string domainName, object userState)
		{
			if (this.GetCountMembersOperationCompleted == null)
			{
				this.GetCountMembersOperationCompleted = new SendOrPostCallback(this.OnGetCountMembersOperationCompleted);
			}
			base.InvokeAsync("GetCountMembers", new object[]
			{
				domainName
			}, this.GetCountMembersOperationCompleted, userState);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000040C0 File Offset: 0x000022C0
		private void OnGetCountMembersOperationCompleted(object arg)
		{
			if (this.GetCountMembersCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetCountMembersCompleted(this, new GetCountMembersCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004108 File Offset: 0x00002308
		[SoapHeader("AdminPassportAuthHeaderValue")]
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/EnumMembers", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("PartnerAuthHeaderValue")]
		[return: XmlArrayItem(IsNullable = false)]
		public Member[] EnumMembers(string domainName, string start, int count)
		{
			object[] array = base.Invoke("EnumMembers", new object[]
			{
				domainName,
				start,
				count
			});
			return (Member[])array[0];
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004144 File Offset: 0x00002344
		public IAsyncResult BeginEnumMembers(string domainName, string start, int count, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("EnumMembers", new object[]
			{
				domainName,
				start,
				count
			}, callback, asyncState);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000417C File Offset: 0x0000237C
		public Member[] EndEnumMembers(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Member[])array[0];
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004199 File Offset: 0x00002399
		public void EnumMembersAsync(string domainName, string start, int count)
		{
			this.EnumMembersAsync(domainName, start, count, null);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000041A8 File Offset: 0x000023A8
		public void EnumMembersAsync(string domainName, string start, int count, object userState)
		{
			if (this.EnumMembersOperationCompleted == null)
			{
				this.EnumMembersOperationCompleted = new SendOrPostCallback(this.OnEnumMembersOperationCompleted);
			}
			base.InvokeAsync("EnumMembers", new object[]
			{
				domainName,
				start,
				count
			}, this.EnumMembersOperationCompleted, userState);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000041FC File Offset: 0x000023FC
		private void OnEnumMembersOperationCompleted(object arg)
		{
			if (this.EnumMembersCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.EnumMembersCompleted(this, new EnumMembersCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004244 File Offset: 0x00002444
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/CreateMember", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("AdminPassportAuthHeaderValue")]
		[SoapHeader("PartnerAuthHeaderValue")]
		public string CreateMember(string memberNameIn, string password, bool forceResetPassword, [XmlArrayItem(IsNullable = false)] Property[] properties)
		{
			object[] array = base.Invoke("CreateMember", new object[]
			{
				memberNameIn,
				password,
				forceResetPassword,
				properties
			});
			return (string)array[0];
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004284 File Offset: 0x00002484
		public IAsyncResult BeginCreateMember(string memberNameIn, string password, bool forceResetPassword, Property[] properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateMember", new object[]
			{
				memberNameIn,
				password,
				forceResetPassword,
				properties
			}, callback, asyncState);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000042C0 File Offset: 0x000024C0
		public string EndCreateMember(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (string)array[0];
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000042DD File Offset: 0x000024DD
		public void CreateMemberAsync(string memberNameIn, string password, bool forceResetPassword, Property[] properties)
		{
			this.CreateMemberAsync(memberNameIn, password, forceResetPassword, properties, null);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000042EC File Offset: 0x000024EC
		public void CreateMemberAsync(string memberNameIn, string password, bool forceResetPassword, Property[] properties, object userState)
		{
			if (this.CreateMemberOperationCompleted == null)
			{
				this.CreateMemberOperationCompleted = new SendOrPostCallback(this.OnCreateMemberOperationCompleted);
			}
			base.InvokeAsync("CreateMember", new object[]
			{
				memberNameIn,
				password,
				forceResetPassword,
				properties
			}, this.CreateMemberOperationCompleted, userState);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004344 File Offset: 0x00002544
		private void OnCreateMemberOperationCompleted(object arg)
		{
			if (this.CreateMemberCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateMemberCompleted(this, new CreateMemberCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000438C File Offset: 0x0000258C
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/CreateMemberEncrypted", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		[SoapHeader("AdminPassportAuthHeaderValue")]
		[SoapHeader("PartnerAuthHeaderValue")]
		public string CreateMemberEncrypted(string memberNameIn, bool forceResetPassword, string SKI, string encryptedProperties, string version, [XmlArrayItem(IsNullable = false)] Property[] properties)
		{
			object[] array = base.Invoke("CreateMemberEncrypted", new object[]
			{
				memberNameIn,
				forceResetPassword,
				SKI,
				encryptedProperties,
				version,
				properties
			});
			return (string)array[0];
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000043D8 File Offset: 0x000025D8
		public IAsyncResult BeginCreateMemberEncrypted(string memberNameIn, bool forceResetPassword, string SKI, string encryptedProperties, string version, Property[] properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateMemberEncrypted", new object[]
			{
				memberNameIn,
				forceResetPassword,
				SKI,
				encryptedProperties,
				version,
				properties
			}, callback, asyncState);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000441C File Offset: 0x0000261C
		public string EndCreateMemberEncrypted(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (string)array[0];
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004439 File Offset: 0x00002639
		public void CreateMemberEncryptedAsync(string memberNameIn, bool forceResetPassword, string SKI, string encryptedProperties, string version, Property[] properties)
		{
			this.CreateMemberEncryptedAsync(memberNameIn, forceResetPassword, SKI, encryptedProperties, version, properties, null);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000444C File Offset: 0x0000264C
		public void CreateMemberEncryptedAsync(string memberNameIn, bool forceResetPassword, string SKI, string encryptedProperties, string version, Property[] properties, object userState)
		{
			if (this.CreateMemberEncryptedOperationCompleted == null)
			{
				this.CreateMemberEncryptedOperationCompleted = new SendOrPostCallback(this.OnCreateMemberEncryptedOperationCompleted);
			}
			base.InvokeAsync("CreateMemberEncrypted", new object[]
			{
				memberNameIn,
				forceResetPassword,
				SKI,
				encryptedProperties,
				version,
				properties
			}, this.CreateMemberEncryptedOperationCompleted, userState);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000044B0 File Offset: 0x000026B0
		private void OnCreateMemberEncryptedOperationCompleted(object arg)
		{
			if (this.CreateMemberEncryptedCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateMemberEncryptedCompleted(this, new CreateMemberEncryptedCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000044F8 File Offset: 0x000026F8
		[SoapHeader("AdminPassportAuthHeaderValue")]
		[SoapHeader("PartnerAuthHeaderValue")]
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/CreateMemberEx", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string CreateMemberEx(string memberNameIn, string password, bool forceResetPassword, string sq, string sa, bool resetSQ, string alternateEmail, int wlTOUVersion, bool noHip, bool forceHip, string brandInfo, [XmlArrayItem(IsNullable = false)] Property[] properties, bool needSlt, out string slt)
		{
			object[] array = base.Invoke("CreateMemberEx", new object[]
			{
				memberNameIn,
				password,
				forceResetPassword,
				sq,
				sa,
				resetSQ,
				alternateEmail,
				wlTOUVersion,
				noHip,
				forceHip,
				brandInfo,
				properties,
				needSlt
			});
			slt = (string)array[1];
			return (string)array[0];
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004590 File Offset: 0x00002790
		public IAsyncResult BeginCreateMemberEx(string memberNameIn, string password, bool forceResetPassword, string sq, string sa, bool resetSQ, string alternateEmail, int wlTOUVersion, bool noHip, bool forceHip, string brandInfo, Property[] properties, bool needSlt, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateMemberEx", new object[]
			{
				memberNameIn,
				password,
				forceResetPassword,
				sq,
				sa,
				resetSQ,
				alternateEmail,
				wlTOUVersion,
				noHip,
				forceHip,
				brandInfo,
				properties,
				needSlt
			}, callback, asyncState);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004618 File Offset: 0x00002818
		public string EndCreateMemberEx(IAsyncResult asyncResult, out string slt)
		{
			object[] array = base.EndInvoke(asyncResult);
			slt = (string)array[1];
			return (string)array[0];
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004640 File Offset: 0x00002840
		public void CreateMemberExAsync(string memberNameIn, string password, bool forceResetPassword, string sq, string sa, bool resetSQ, string alternateEmail, int wlTOUVersion, bool noHip, bool forceHip, string brandInfo, Property[] properties, bool needSlt)
		{
			this.CreateMemberExAsync(memberNameIn, password, forceResetPassword, sq, sa, resetSQ, alternateEmail, wlTOUVersion, noHip, forceHip, brandInfo, properties, needSlt, null);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000466C File Offset: 0x0000286C
		public void CreateMemberExAsync(string memberNameIn, string password, bool forceResetPassword, string sq, string sa, bool resetSQ, string alternateEmail, int wlTOUVersion, bool noHip, bool forceHip, string brandInfo, Property[] properties, bool needSlt, object userState)
		{
			if (this.CreateMemberExOperationCompleted == null)
			{
				this.CreateMemberExOperationCompleted = new SendOrPostCallback(this.OnCreateMemberExOperationCompleted);
			}
			base.InvokeAsync("CreateMemberEx", new object[]
			{
				memberNameIn,
				password,
				forceResetPassword,
				sq,
				sa,
				resetSQ,
				alternateEmail,
				wlTOUVersion,
				noHip,
				forceHip,
				brandInfo,
				properties,
				needSlt
			}, this.CreateMemberExOperationCompleted, userState);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004710 File Offset: 0x00002910
		private void OnCreateMemberExOperationCompleted(object arg)
		{
			if (this.CreateMemberExCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateMemberExCompleted(this, new CreateMemberExCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004758 File Offset: 0x00002958
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		[SoapHeader("AdminPassportAuthHeaderValue")]
		[SoapHeader("PartnerAuthHeaderValue")]
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/CreateMemberEncryptedEx", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string CreateMemberEncryptedEx(string memberNameIn, bool forceResetPassword, string sq, bool resetSQ, string alternateEmail, string SKI, string encryptedProperties, string version, int wlTOUVersion, bool noHip, bool forceHip, string brandInfo, [XmlArrayItem(IsNullable = false)] Property[] properties, bool needSlt, out string slt)
		{
			object[] array = base.Invoke("CreateMemberEncryptedEx", new object[]
			{
				memberNameIn,
				forceResetPassword,
				sq,
				resetSQ,
				alternateEmail,
				SKI,
				encryptedProperties,
				version,
				wlTOUVersion,
				noHip,
				forceHip,
				brandInfo,
				properties,
				needSlt
			});
			slt = (string)array[1];
			return (string)array[0];
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000047F4 File Offset: 0x000029F4
		public IAsyncResult BeginCreateMemberEncryptedEx(string memberNameIn, bool forceResetPassword, string sq, bool resetSQ, string alternateEmail, string SKI, string encryptedProperties, string version, int wlTOUVersion, bool noHip, bool forceHip, string brandInfo, Property[] properties, bool needSlt, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateMemberEncryptedEx", new object[]
			{
				memberNameIn,
				forceResetPassword,
				sq,
				resetSQ,
				alternateEmail,
				SKI,
				encryptedProperties,
				version,
				wlTOUVersion,
				noHip,
				forceHip,
				brandInfo,
				properties,
				needSlt
			}, callback, asyncState);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004880 File Offset: 0x00002A80
		public string EndCreateMemberEncryptedEx(IAsyncResult asyncResult, out string slt)
		{
			object[] array = base.EndInvoke(asyncResult);
			slt = (string)array[1];
			return (string)array[0];
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000048A8 File Offset: 0x00002AA8
		public void CreateMemberEncryptedExAsync(string memberNameIn, bool forceResetPassword, string sq, bool resetSQ, string alternateEmail, string SKI, string encryptedProperties, string version, int wlTOUVersion, bool noHip, bool forceHip, string brandInfo, Property[] properties, bool needSlt)
		{
			this.CreateMemberEncryptedExAsync(memberNameIn, forceResetPassword, sq, resetSQ, alternateEmail, SKI, encryptedProperties, version, wlTOUVersion, noHip, forceHip, brandInfo, properties, needSlt, null);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000048D8 File Offset: 0x00002AD8
		public void CreateMemberEncryptedExAsync(string memberNameIn, bool forceResetPassword, string sq, bool resetSQ, string alternateEmail, string SKI, string encryptedProperties, string version, int wlTOUVersion, bool noHip, bool forceHip, string brandInfo, Property[] properties, bool needSlt, object userState)
		{
			if (this.CreateMemberEncryptedExOperationCompleted == null)
			{
				this.CreateMemberEncryptedExOperationCompleted = new SendOrPostCallback(this.OnCreateMemberEncryptedExOperationCompleted);
			}
			base.InvokeAsync("CreateMemberEncryptedEx", new object[]
			{
				memberNameIn,
				forceResetPassword,
				sq,
				resetSQ,
				alternateEmail,
				SKI,
				encryptedProperties,
				version,
				wlTOUVersion,
				noHip,
				forceHip,
				brandInfo,
				properties,
				needSlt
			}, this.CreateMemberEncryptedExOperationCompleted, userState);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004984 File Offset: 0x00002B84
		private void OnCreateMemberEncryptedExOperationCompleted(object arg)
		{
			if (this.CreateMemberEncryptedExCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateMemberEncryptedExCompleted(this, new CreateMemberEncryptedExCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000049CC File Offset: 0x00002BCC
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		[SoapHeader("AdminPassportAuthHeaderValue")]
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/AddBrandInfo", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("PartnerAuthHeaderValue")]
		public void AddBrandInfo(string memberNameIn, string brandInfo)
		{
			base.Invoke("AddBrandInfo", new object[]
			{
				memberNameIn,
				brandInfo
			});
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000049F8 File Offset: 0x00002BF8
		public IAsyncResult BeginAddBrandInfo(string memberNameIn, string brandInfo, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("AddBrandInfo", new object[]
			{
				memberNameIn,
				brandInfo
			}, callback, asyncState);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004A23 File Offset: 0x00002C23
		public void EndAddBrandInfo(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004A2D File Offset: 0x00002C2D
		public void AddBrandInfoAsync(string memberNameIn, string brandInfo)
		{
			this.AddBrandInfoAsync(memberNameIn, brandInfo, null);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004A38 File Offset: 0x00002C38
		public void AddBrandInfoAsync(string memberNameIn, string brandInfo, object userState)
		{
			if (this.AddBrandInfoOperationCompleted == null)
			{
				this.AddBrandInfoOperationCompleted = new SendOrPostCallback(this.OnAddBrandInfoOperationCompleted);
			}
			base.InvokeAsync("AddBrandInfo", new object[]
			{
				memberNameIn,
				brandInfo
			}, this.AddBrandInfoOperationCompleted, userState);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004A84 File Offset: 0x00002C84
		private void OnAddBrandInfoOperationCompleted(object arg)
		{
			if (this.AddBrandInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.AddBrandInfoCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004AC4 File Offset: 0x00002CC4
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/RemoveBrandInfo", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		[SoapHeader("PartnerAuthHeaderValue")]
		[SoapHeader("AdminPassportAuthHeaderValue")]
		public void RemoveBrandInfo(string memberNameIn, string brandInfo)
		{
			base.Invoke("RemoveBrandInfo", new object[]
			{
				memberNameIn,
				brandInfo
			});
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004AF0 File Offset: 0x00002CF0
		public IAsyncResult BeginRemoveBrandInfo(string memberNameIn, string brandInfo, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("RemoveBrandInfo", new object[]
			{
				memberNameIn,
				brandInfo
			}, callback, asyncState);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004B1B File Offset: 0x00002D1B
		public void EndRemoveBrandInfo(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004B25 File Offset: 0x00002D25
		public void RemoveBrandInfoAsync(string memberNameIn, string brandInfo)
		{
			this.RemoveBrandInfoAsync(memberNameIn, brandInfo, null);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004B30 File Offset: 0x00002D30
		public void RemoveBrandInfoAsync(string memberNameIn, string brandInfo, object userState)
		{
			if (this.RemoveBrandInfoOperationCompleted == null)
			{
				this.RemoveBrandInfoOperationCompleted = new SendOrPostCallback(this.OnRemoveBrandInfoOperationCompleted);
			}
			base.InvokeAsync("RemoveBrandInfo", new object[]
			{
				memberNameIn,
				brandInfo
			}, this.RemoveBrandInfoOperationCompleted, userState);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004B7C File Offset: 0x00002D7C
		private void OnRemoveBrandInfoOperationCompleted(object arg)
		{
			if (this.RemoveBrandInfoCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RemoveBrandInfoCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004BBC File Offset: 0x00002DBC
		[SoapHeader("PartnerAuthHeaderValue")]
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/RenameMember", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("AdminPassportAuthHeaderValue")]
		public void RenameMember(string memberNameIn, string memberNameNewIn)
		{
			base.Invoke("RenameMember", new object[]
			{
				memberNameIn,
				memberNameNewIn
			});
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004BE8 File Offset: 0x00002DE8
		public IAsyncResult BeginRenameMember(string memberNameIn, string memberNameNewIn, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("RenameMember", new object[]
			{
				memberNameIn,
				memberNameNewIn
			}, callback, asyncState);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004C13 File Offset: 0x00002E13
		public void EndRenameMember(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004C1D File Offset: 0x00002E1D
		public void RenameMemberAsync(string memberNameIn, string memberNameNewIn)
		{
			this.RenameMemberAsync(memberNameIn, memberNameNewIn, null);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004C28 File Offset: 0x00002E28
		public void RenameMemberAsync(string memberNameIn, string memberNameNewIn, object userState)
		{
			if (this.RenameMemberOperationCompleted == null)
			{
				this.RenameMemberOperationCompleted = new SendOrPostCallback(this.OnRenameMemberOperationCompleted);
			}
			base.InvokeAsync("RenameMember", new object[]
			{
				memberNameIn,
				memberNameNewIn
			}, this.RenameMemberOperationCompleted, userState);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004C74 File Offset: 0x00002E74
		private void OnRenameMemberOperationCompleted(object arg)
		{
			if (this.RenameMemberCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.RenameMemberCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004CB4 File Offset: 0x00002EB4
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/SetMemberProperties", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("AdminPassportAuthHeaderValue")]
		[SoapHeader("PartnerAuthHeaderValue")]
		public void SetMemberProperties(string memberNameIn, [XmlArrayItem(IsNullable = false)] Property[] properties)
		{
			base.Invoke("SetMemberProperties", new object[]
			{
				memberNameIn,
				properties
			});
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004CE0 File Offset: 0x00002EE0
		public IAsyncResult BeginSetMemberProperties(string memberNameIn, Property[] properties, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetMemberProperties", new object[]
			{
				memberNameIn,
				properties
			}, callback, asyncState);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004D0B File Offset: 0x00002F0B
		public void EndSetMemberProperties(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004D15 File Offset: 0x00002F15
		public void SetMemberPropertiesAsync(string memberNameIn, Property[] properties)
		{
			this.SetMemberPropertiesAsync(memberNameIn, properties, null);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004D20 File Offset: 0x00002F20
		public void SetMemberPropertiesAsync(string memberNameIn, Property[] properties, object userState)
		{
			if (this.SetMemberPropertiesOperationCompleted == null)
			{
				this.SetMemberPropertiesOperationCompleted = new SendOrPostCallback(this.OnSetMemberPropertiesOperationCompleted);
			}
			base.InvokeAsync("SetMemberProperties", new object[]
			{
				memberNameIn,
				properties
			}, this.SetMemberPropertiesOperationCompleted, userState);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004D6C File Offset: 0x00002F6C
		private void OnSetMemberPropertiesOperationCompleted(object arg)
		{
			if (this.SetMemberPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetMemberPropertiesCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004DAC File Offset: 0x00002FAC
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/GetMemberProperties", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("PartnerAuthHeaderValue")]
		[SoapHeader("AdminPassportAuthHeaderValue")]
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		[return: XmlArrayItem(IsNullable = false)]
		public Property[] GetMemberProperties(string memberNameIn)
		{
			object[] array = base.Invoke("GetMemberProperties", new object[]
			{
				memberNameIn
			});
			return (Property[])array[0];
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004DDC File Offset: 0x00002FDC
		public IAsyncResult BeginGetMemberProperties(string memberNameIn, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetMemberProperties", new object[]
			{
				memberNameIn
			}, callback, asyncState);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004E04 File Offset: 0x00003004
		public Property[] EndGetMemberProperties(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Property[])array[0];
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004E21 File Offset: 0x00003021
		public void GetMemberPropertiesAsync(string memberNameIn)
		{
			this.GetMemberPropertiesAsync(memberNameIn, null);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004E2C File Offset: 0x0000302C
		public void GetMemberPropertiesAsync(string memberNameIn, object userState)
		{
			if (this.GetMemberPropertiesOperationCompleted == null)
			{
				this.GetMemberPropertiesOperationCompleted = new SendOrPostCallback(this.OnGetMemberPropertiesOperationCompleted);
			}
			base.InvokeAsync("GetMemberProperties", new object[]
			{
				memberNameIn
			}, this.GetMemberPropertiesOperationCompleted, userState);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004E74 File Offset: 0x00003074
		private void OnGetMemberPropertiesOperationCompleted(object arg)
		{
			if (this.GetMemberPropertiesCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetMemberPropertiesCompleted(this, new GetMemberPropertiesCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004EBC File Offset: 0x000030BC
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		[SoapHeader("PartnerAuthHeaderValue")]
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/EvictMember", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("AdminPassportAuthHeaderValue")]
		public void EvictMember(string memberNameIn)
		{
			base.Invoke("EvictMember", new object[]
			{
				memberNameIn
			});
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004EE4 File Offset: 0x000030E4
		public IAsyncResult BeginEvictMember(string memberNameIn, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("EvictMember", new object[]
			{
				memberNameIn
			}, callback, asyncState);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004F0A File Offset: 0x0000310A
		public void EndEvictMember(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004F14 File Offset: 0x00003114
		public void EvictMemberAsync(string memberNameIn)
		{
			this.EvictMemberAsync(memberNameIn, null);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004F20 File Offset: 0x00003120
		public void EvictMemberAsync(string memberNameIn, object userState)
		{
			if (this.EvictMemberOperationCompleted == null)
			{
				this.EvictMemberOperationCompleted = new SendOrPostCallback(this.OnEvictMemberOperationCompleted);
			}
			base.InvokeAsync("EvictMember", new object[]
			{
				memberNameIn
			}, this.EvictMemberOperationCompleted, userState);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004F68 File Offset: 0x00003168
		private void OnEvictMemberOperationCompleted(object arg)
		{
			if (this.EvictMemberCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.EvictMemberCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004FA8 File Offset: 0x000031A8
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/ResetMemberPassword", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("AdminPassportAuthHeaderValue")]
		[SoapHeader("PartnerAuthHeaderValue")]
		public void ResetMemberPassword(string memberNameIn, string newPassword, bool forceResetPassword)
		{
			base.Invoke("ResetMemberPassword", new object[]
			{
				memberNameIn,
				newPassword,
				forceResetPassword
			});
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004FDC File Offset: 0x000031DC
		public IAsyncResult BeginResetMemberPassword(string memberNameIn, string newPassword, bool forceResetPassword, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ResetMemberPassword", new object[]
			{
				memberNameIn,
				newPassword,
				forceResetPassword
			}, callback, asyncState);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005011 File Offset: 0x00003211
		public void EndResetMemberPassword(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000501B File Offset: 0x0000321B
		public void ResetMemberPasswordAsync(string memberNameIn, string newPassword, bool forceResetPassword)
		{
			this.ResetMemberPasswordAsync(memberNameIn, newPassword, forceResetPassword, null);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005028 File Offset: 0x00003228
		public void ResetMemberPasswordAsync(string memberNameIn, string newPassword, bool forceResetPassword, object userState)
		{
			if (this.ResetMemberPasswordOperationCompleted == null)
			{
				this.ResetMemberPasswordOperationCompleted = new SendOrPostCallback(this.OnResetMemberPasswordOperationCompleted);
			}
			base.InvokeAsync("ResetMemberPassword", new object[]
			{
				memberNameIn,
				newPassword,
				forceResetPassword
			}, this.ResetMemberPasswordOperationCompleted, userState);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000507C File Offset: 0x0000327C
		private void OnResetMemberPasswordOperationCompleted(object arg)
		{
			if (this.ResetMemberPasswordCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ResetMemberPasswordCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000050BC File Offset: 0x000032BC
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/ResetMemberPasswordEncrypted", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("AdminPassportAuthHeaderValue")]
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		[SoapHeader("PartnerAuthHeaderValue")]
		public void ResetMemberPasswordEncrypted(string memberNameIn, bool forceResetPassword, string SKI, string encryptedProperties, string version)
		{
			base.Invoke("ResetMemberPasswordEncrypted", new object[]
			{
				memberNameIn,
				forceResetPassword,
				SKI,
				encryptedProperties,
				version
			});
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000050F8 File Offset: 0x000032F8
		public IAsyncResult BeginResetMemberPasswordEncrypted(string memberNameIn, bool forceResetPassword, string SKI, string encryptedProperties, string version, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ResetMemberPasswordEncrypted", new object[]
			{
				memberNameIn,
				forceResetPassword,
				SKI,
				encryptedProperties,
				version
			}, callback, asyncState);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005137 File Offset: 0x00003337
		public void EndResetMemberPasswordEncrypted(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005141 File Offset: 0x00003341
		public void ResetMemberPasswordEncryptedAsync(string memberNameIn, bool forceResetPassword, string SKI, string encryptedProperties, string version)
		{
			this.ResetMemberPasswordEncryptedAsync(memberNameIn, forceResetPassword, SKI, encryptedProperties, version, null);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005154 File Offset: 0x00003354
		public void ResetMemberPasswordEncryptedAsync(string memberNameIn, bool forceResetPassword, string SKI, string encryptedProperties, string version, object userState)
		{
			if (this.ResetMemberPasswordEncryptedOperationCompleted == null)
			{
				this.ResetMemberPasswordEncryptedOperationCompleted = new SendOrPostCallback(this.OnResetMemberPasswordEncryptedOperationCompleted);
			}
			base.InvokeAsync("ResetMemberPasswordEncrypted", new object[]
			{
				memberNameIn,
				forceResetPassword,
				SKI,
				encryptedProperties,
				version
			}, this.ResetMemberPasswordEncryptedOperationCompleted, userState);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000051B4 File Offset: 0x000033B4
		private void OnResetMemberPasswordEncryptedOperationCompleted(object arg)
		{
			if (this.ResetMemberPasswordEncryptedCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ResetMemberPasswordEncryptedCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000051F4 File Offset: 0x000033F4
		[SoapHeader("AdminPassportAuthHeaderValue")]
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		[SoapHeader("PartnerAuthHeaderValue")]
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/BlockMemberEmail", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void BlockMemberEmail(string memberNameIn, bool isBlocked)
		{
			base.Invoke("BlockMemberEmail", new object[]
			{
				memberNameIn,
				isBlocked
			});
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005224 File Offset: 0x00003424
		public IAsyncResult BeginBlockMemberEmail(string memberNameIn, bool isBlocked, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("BlockMemberEmail", new object[]
			{
				memberNameIn,
				isBlocked
			}, callback, asyncState);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005254 File Offset: 0x00003454
		public void EndBlockMemberEmail(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000525E File Offset: 0x0000345E
		public void BlockMemberEmailAsync(string memberNameIn, bool isBlocked)
		{
			this.BlockMemberEmailAsync(memberNameIn, isBlocked, null);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000526C File Offset: 0x0000346C
		public void BlockMemberEmailAsync(string memberNameIn, bool isBlocked, object userState)
		{
			if (this.BlockMemberEmailOperationCompleted == null)
			{
				this.BlockMemberEmailOperationCompleted = new SendOrPostCallback(this.OnBlockMemberEmailOperationCompleted);
			}
			base.InvokeAsync("BlockMemberEmail", new object[]
			{
				memberNameIn,
				isBlocked
			}, this.BlockMemberEmailOperationCompleted, userState);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000052BC File Offset: 0x000034BC
		private void OnBlockMemberEmailOperationCompleted(object arg)
		{
			if (this.BlockMemberEmailCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.BlockMemberEmailCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000052FC File Offset: 0x000034FC
		[SoapHeader("PartnerAuthHeaderValue")]
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/ImportUnmanagedMember", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("AdminPassportAuthHeaderValue")]
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		public void ImportUnmanagedMember(string memberNameIn)
		{
			base.Invoke("ImportUnmanagedMember", new object[]
			{
				memberNameIn
			});
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005324 File Offset: 0x00003524
		public IAsyncResult BeginImportUnmanagedMember(string memberNameIn, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ImportUnmanagedMember", new object[]
			{
				memberNameIn
			}, callback, asyncState);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000534A File Offset: 0x0000354A
		public void EndImportUnmanagedMember(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005354 File Offset: 0x00003554
		public void ImportUnmanagedMemberAsync(string memberNameIn)
		{
			this.ImportUnmanagedMemberAsync(memberNameIn, null);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005360 File Offset: 0x00003560
		public void ImportUnmanagedMemberAsync(string memberNameIn, object userState)
		{
			if (this.ImportUnmanagedMemberOperationCompleted == null)
			{
				this.ImportUnmanagedMemberOperationCompleted = new SendOrPostCallback(this.OnImportUnmanagedMemberOperationCompleted);
			}
			base.InvokeAsync("ImportUnmanagedMember", new object[]
			{
				memberNameIn
			}, this.ImportUnmanagedMemberOperationCompleted, userState);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000053A8 File Offset: 0x000035A8
		private void OnImportUnmanagedMemberOperationCompleted(object arg)
		{
			if (this.ImportUnmanagedMemberCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ImportUnmanagedMemberCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000053E8 File Offset: 0x000035E8
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/EvictUnmanagedMember", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		[SoapHeader("AdminPassportAuthHeaderValue")]
		[SoapHeader("PartnerAuthHeaderValue")]
		public void EvictUnmanagedMember(string memberNameIn)
		{
			base.Invoke("EvictUnmanagedMember", new object[]
			{
				memberNameIn
			});
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005410 File Offset: 0x00003610
		public IAsyncResult BeginEvictUnmanagedMember(string memberNameIn, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("EvictUnmanagedMember", new object[]
			{
				memberNameIn
			}, callback, asyncState);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005436 File Offset: 0x00003636
		public void EndEvictUnmanagedMember(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005440 File Offset: 0x00003640
		public void EvictUnmanagedMemberAsync(string memberNameIn)
		{
			this.EvictUnmanagedMemberAsync(memberNameIn, null);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000544C File Offset: 0x0000364C
		public void EvictUnmanagedMemberAsync(string memberNameIn, object userState)
		{
			if (this.EvictUnmanagedMemberOperationCompleted == null)
			{
				this.EvictUnmanagedMemberOperationCompleted = new SendOrPostCallback(this.OnEvictUnmanagedMemberOperationCompleted);
			}
			base.InvokeAsync("EvictUnmanagedMember", new object[]
			{
				memberNameIn
			}, this.EvictUnmanagedMemberOperationCompleted, userState);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005494 File Offset: 0x00003694
		private void OnEvictUnmanagedMemberOperationCompleted(object arg)
		{
			if (this.EvictUnmanagedMemberCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.EvictUnmanagedMemberCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000054D4 File Offset: 0x000036D4
		[SoapHeader("AdminPassportAuthHeaderValue")]
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/EvictAllUnmanagedMembers", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		[SoapHeader("PartnerAuthHeaderValue")]
		public void EvictAllUnmanagedMembers(string domainName)
		{
			base.Invoke("EvictAllUnmanagedMembers", new object[]
			{
				domainName
			});
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000054FC File Offset: 0x000036FC
		public IAsyncResult BeginEvictAllUnmanagedMembers(string domainName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("EvictAllUnmanagedMembers", new object[]
			{
				domainName
			}, callback, asyncState);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005522 File Offset: 0x00003722
		public void EndEvictAllUnmanagedMembers(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000552C File Offset: 0x0000372C
		public void EvictAllUnmanagedMembersAsync(string domainName)
		{
			this.EvictAllUnmanagedMembersAsync(domainName, null);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005538 File Offset: 0x00003738
		public void EvictAllUnmanagedMembersAsync(string domainName, object userState)
		{
			if (this.EvictAllUnmanagedMembersOperationCompleted == null)
			{
				this.EvictAllUnmanagedMembersOperationCompleted = new SendOrPostCallback(this.OnEvictAllUnmanagedMembersOperationCompleted);
			}
			base.InvokeAsync("EvictAllUnmanagedMembers", new object[]
			{
				domainName
			}, this.EvictAllUnmanagedMembersOperationCompleted, userState);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005580 File Offset: 0x00003780
		private void OnEvictAllUnmanagedMembersOperationCompleted(object arg)
		{
			if (this.EvictAllUnmanagedMembersCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.EvictAllUnmanagedMembersCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000055C0 File Offset: 0x000037C0
		[SoapHeader("AdminPassportAuthHeaderValue")]
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		[SoapHeader("PartnerAuthHeaderValue")]
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/EnableOpenMembership", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void EnableOpenMembership(string domainName, bool isEnabled)
		{
			base.Invoke("EnableOpenMembership", new object[]
			{
				domainName,
				isEnabled
			});
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000055F0 File Offset: 0x000037F0
		public IAsyncResult BeginEnableOpenMembership(string domainName, bool isEnabled, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("EnableOpenMembership", new object[]
			{
				domainName,
				isEnabled
			}, callback, asyncState);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005620 File Offset: 0x00003820
		public void EndEnableOpenMembership(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000562A File Offset: 0x0000382A
		public void EnableOpenMembershipAsync(string domainName, bool isEnabled)
		{
			this.EnableOpenMembershipAsync(domainName, isEnabled, null);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005638 File Offset: 0x00003838
		public void EnableOpenMembershipAsync(string domainName, bool isEnabled, object userState)
		{
			if (this.EnableOpenMembershipOperationCompleted == null)
			{
				this.EnableOpenMembershipOperationCompleted = new SendOrPostCallback(this.OnEnableOpenMembershipOperationCompleted);
			}
			base.InvokeAsync("EnableOpenMembership", new object[]
			{
				domainName,
				isEnabled
			}, this.EnableOpenMembershipOperationCompleted, userState);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005688 File Offset: 0x00003888
		private void OnEnableOpenMembershipOperationCompleted(object arg)
		{
			if (this.EnableOpenMembershipCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.EnableOpenMembershipCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000056C8 File Offset: 0x000038C8
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		[SoapHeader("AdminPassportAuthHeaderValue")]
		[SoapHeader("PartnerAuthHeaderValue")]
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/ProvisionMemberSubscription", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void ProvisionMemberSubscription(string memberNameIn, string offerName)
		{
			base.Invoke("ProvisionMemberSubscription", new object[]
			{
				memberNameIn,
				offerName
			});
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000056F4 File Offset: 0x000038F4
		public IAsyncResult BeginProvisionMemberSubscription(string memberNameIn, string offerName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ProvisionMemberSubscription", new object[]
			{
				memberNameIn,
				offerName
			}, callback, asyncState);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000571F File Offset: 0x0000391F
		public void EndProvisionMemberSubscription(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005729 File Offset: 0x00003929
		public void ProvisionMemberSubscriptionAsync(string memberNameIn, string offerName)
		{
			this.ProvisionMemberSubscriptionAsync(memberNameIn, offerName, null);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005734 File Offset: 0x00003934
		public void ProvisionMemberSubscriptionAsync(string memberNameIn, string offerName, object userState)
		{
			if (this.ProvisionMemberSubscriptionOperationCompleted == null)
			{
				this.ProvisionMemberSubscriptionOperationCompleted = new SendOrPostCallback(this.OnProvisionMemberSubscriptionOperationCompleted);
			}
			base.InvokeAsync("ProvisionMemberSubscription", new object[]
			{
				memberNameIn,
				offerName
			}, this.ProvisionMemberSubscriptionOperationCompleted, userState);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005780 File Offset: 0x00003980
		private void OnProvisionMemberSubscriptionOperationCompleted(object arg)
		{
			if (this.ProvisionMemberSubscriptionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ProvisionMemberSubscriptionCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000057C0 File Offset: 0x000039C0
		[SoapHeader("AdminPassportAuthHeaderValue")]
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/DeprovisionMemberSubscription", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("PartnerAuthHeaderValue")]
		public void DeprovisionMemberSubscription(string memberNameIn, string offerName)
		{
			base.Invoke("DeprovisionMemberSubscription", new object[]
			{
				memberNameIn,
				offerName
			});
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000057EC File Offset: 0x000039EC
		public IAsyncResult BeginDeprovisionMemberSubscription(string memberNameIn, string offerName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("DeprovisionMemberSubscription", new object[]
			{
				memberNameIn,
				offerName
			}, callback, asyncState);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005817 File Offset: 0x00003A17
		public void EndDeprovisionMemberSubscription(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005821 File Offset: 0x00003A21
		public void DeprovisionMemberSubscriptionAsync(string memberNameIn, string offerName)
		{
			this.DeprovisionMemberSubscriptionAsync(memberNameIn, offerName, null);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000582C File Offset: 0x00003A2C
		public void DeprovisionMemberSubscriptionAsync(string memberNameIn, string offerName, object userState)
		{
			if (this.DeprovisionMemberSubscriptionOperationCompleted == null)
			{
				this.DeprovisionMemberSubscriptionOperationCompleted = new SendOrPostCallback(this.OnDeprovisionMemberSubscriptionOperationCompleted);
			}
			base.InvokeAsync("DeprovisionMemberSubscription", new object[]
			{
				memberNameIn,
				offerName
			}, this.DeprovisionMemberSubscriptionOperationCompleted, userState);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005878 File Offset: 0x00003A78
		private void OnDeprovisionMemberSubscriptionOperationCompleted(object arg)
		{
			if (this.DeprovisionMemberSubscriptionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.DeprovisionMemberSubscriptionCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000058B8 File Offset: 0x00003AB8
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/ConvertMemberSubscription", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("AdminPassportAuthHeaderValue")]
		[SoapHeader("PartnerAuthHeaderValue")]
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		public void ConvertMemberSubscription(string memberNameIn, string offerNameOld, string offerNameNew)
		{
			base.Invoke("ConvertMemberSubscription", new object[]
			{
				memberNameIn,
				offerNameOld,
				offerNameNew
			});
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000058E8 File Offset: 0x00003AE8
		public IAsyncResult BeginConvertMemberSubscription(string memberNameIn, string offerNameOld, string offerNameNew, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("ConvertMemberSubscription", new object[]
			{
				memberNameIn,
				offerNameOld,
				offerNameNew
			}, callback, asyncState);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005918 File Offset: 0x00003B18
		public void EndConvertMemberSubscription(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005922 File Offset: 0x00003B22
		public void ConvertMemberSubscriptionAsync(string memberNameIn, string offerNameOld, string offerNameNew)
		{
			this.ConvertMemberSubscriptionAsync(memberNameIn, offerNameOld, offerNameNew, null);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005930 File Offset: 0x00003B30
		public void ConvertMemberSubscriptionAsync(string memberNameIn, string offerNameOld, string offerNameNew, object userState)
		{
			if (this.ConvertMemberSubscriptionOperationCompleted == null)
			{
				this.ConvertMemberSubscriptionOperationCompleted = new SendOrPostCallback(this.OnConvertMemberSubscriptionOperationCompleted);
			}
			base.InvokeAsync("ConvertMemberSubscription", new object[]
			{
				memberNameIn,
				offerNameOld,
				offerNameNew
			}, this.ConvertMemberSubscriptionOperationCompleted, userState);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005980 File Offset: 0x00003B80
		private void OnConvertMemberSubscriptionOperationCompleted(object arg)
		{
			if (this.ConvertMemberSubscriptionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.ConvertMemberSubscriptionCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000059C0 File Offset: 0x00003BC0
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/MemberHasSubscription", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("AdminPassportAuthHeaderValue")]
		[SoapHeader("PartnerAuthHeaderValue")]
		public bool MemberHasSubscription(string memberNameIn, string offerName)
		{
			object[] array = base.Invoke("MemberHasSubscription", new object[]
			{
				memberNameIn,
				offerName
			});
			return (bool)array[0];
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000059F4 File Offset: 0x00003BF4
		public IAsyncResult BeginMemberHasSubscription(string memberNameIn, string offerName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("MemberHasSubscription", new object[]
			{
				memberNameIn,
				offerName
			}, callback, asyncState);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005A20 File Offset: 0x00003C20
		public bool EndMemberHasSubscription(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (bool)array[0];
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005A3D File Offset: 0x00003C3D
		public void MemberHasSubscriptionAsync(string memberNameIn, string offerName)
		{
			this.MemberHasSubscriptionAsync(memberNameIn, offerName, null);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005A48 File Offset: 0x00003C48
		public void MemberHasSubscriptionAsync(string memberNameIn, string offerName, object userState)
		{
			if (this.MemberHasSubscriptionOperationCompleted == null)
			{
				this.MemberHasSubscriptionOperationCompleted = new SendOrPostCallback(this.OnMemberHasSubscriptionOperationCompleted);
			}
			base.InvokeAsync("MemberHasSubscription", new object[]
			{
				memberNameIn,
				offerName
			}, this.MemberHasSubscriptionOperationCompleted, userState);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005A94 File Offset: 0x00003C94
		private void OnMemberHasSubscriptionOperationCompleted(object arg)
		{
			if (this.MemberHasSubscriptionCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.MemberHasSubscriptionCompleted(this, new MemberHasSubscriptionCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005ADC File Offset: 0x00003CDC
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/SuspendEmail", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		[SoapHeader("AdminPassportAuthHeaderValue")]
		[SoapHeader("PartnerAuthHeaderValue")]
		public void SuspendEmail(string domainName, bool isSuspend)
		{
			base.Invoke("SuspendEmail", new object[]
			{
				domainName,
				isSuspend
			});
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005B0C File Offset: 0x00003D0C
		public IAsyncResult BeginSuspendEmail(string domainName, bool isSuspend, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SuspendEmail", new object[]
			{
				domainName,
				isSuspend
			}, callback, asyncState);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005B3C File Offset: 0x00003D3C
		public void EndSuspendEmail(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005B46 File Offset: 0x00003D46
		public void SuspendEmailAsync(string domainName, bool isSuspend)
		{
			this.SuspendEmailAsync(domainName, isSuspend, null);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005B54 File Offset: 0x00003D54
		public void SuspendEmailAsync(string domainName, bool isSuspend, object userState)
		{
			if (this.SuspendEmailOperationCompleted == null)
			{
				this.SuspendEmailOperationCompleted = new SendOrPostCallback(this.OnSuspendEmailOperationCompleted);
			}
			base.InvokeAsync("SuspendEmail", new object[]
			{
				domainName,
				isSuspend
			}, this.SuspendEmailOperationCompleted, userState);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005BA4 File Offset: 0x00003DA4
		private void OnSuspendEmailOperationCompleted(object arg)
		{
			if (this.SuspendEmailCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SuspendEmailCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005BE4 File Offset: 0x00003DE4
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/GetMxRecords", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("AdminPassportAuthHeaderValue")]
		[SoapHeader("PartnerAuthHeaderValue")]
		public string[] GetMxRecords(string domainName)
		{
			object[] array = base.Invoke("GetMxRecords", new object[]
			{
				domainName
			});
			return (string[])array[0];
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00005C14 File Offset: 0x00003E14
		public IAsyncResult BeginGetMxRecords(string domainName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetMxRecords", new object[]
			{
				domainName
			}, callback, asyncState);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00005C3C File Offset: 0x00003E3C
		public string[] EndGetMxRecords(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (string[])array[0];
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005C59 File Offset: 0x00003E59
		public void GetMxRecordsAsync(string domainName)
		{
			this.GetMxRecordsAsync(domainName, null);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00005C64 File Offset: 0x00003E64
		public void GetMxRecordsAsync(string domainName, object userState)
		{
			if (this.GetMxRecordsOperationCompleted == null)
			{
				this.GetMxRecordsOperationCompleted = new SendOrPostCallback(this.OnGetMxRecordsOperationCompleted);
			}
			base.InvokeAsync("GetMxRecords", new object[]
			{
				domainName
			}, this.GetMxRecordsOperationCompleted, userState);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005CAC File Offset: 0x00003EAC
		private void OnGetMxRecordsOperationCompleted(object arg)
		{
			if (this.GetMxRecordsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetMxRecordsCompleted(this, new GetMxRecordsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005CF4 File Offset: 0x00003EF4
		[SoapHeader("PartnerAuthHeaderValue")]
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/MemberHasMailbox", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public bool MemberHasMailbox(string memberNameIn, EmailType emailType)
		{
			object[] array = base.Invoke("MemberHasMailbox", new object[]
			{
				memberNameIn,
				emailType
			});
			return (bool)array[0];
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005D2C File Offset: 0x00003F2C
		public IAsyncResult BeginMemberHasMailbox(string memberNameIn, EmailType emailType, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("MemberHasMailbox", new object[]
			{
				memberNameIn,
				emailType
			}, callback, asyncState);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005D5C File Offset: 0x00003F5C
		public bool EndMemberHasMailbox(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (bool)array[0];
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005D79 File Offset: 0x00003F79
		public void MemberHasMailboxAsync(string memberNameIn, EmailType emailType)
		{
			this.MemberHasMailboxAsync(memberNameIn, emailType, null);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005D84 File Offset: 0x00003F84
		public void MemberHasMailboxAsync(string memberNameIn, EmailType emailType, object userState)
		{
			if (this.MemberHasMailboxOperationCompleted == null)
			{
				this.MemberHasMailboxOperationCompleted = new SendOrPostCallback(this.OnMemberHasMailboxOperationCompleted);
			}
			base.InvokeAsync("MemberHasMailbox", new object[]
			{
				memberNameIn,
				emailType
			}, this.MemberHasMailboxOperationCompleted, userState);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00005DD4 File Offset: 0x00003FD4
		private void OnMemberHasMailboxOperationCompleted(object arg)
		{
			if (this.MemberHasMailboxCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.MemberHasMailboxCompleted(this, new MemberHasMailboxCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005E1C File Offset: 0x0000401C
		[SoapHeader("PartnerAuthHeaderValue")]
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/CompleteMemberEmailMigration", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void CompleteMemberEmailMigration(string memberNameIn)
		{
			base.Invoke("CompleteMemberEmailMigration", new object[]
			{
				memberNameIn
			});
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005E44 File Offset: 0x00004044
		public IAsyncResult BeginCompleteMemberEmailMigration(string memberNameIn, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CompleteMemberEmailMigration", new object[]
			{
				memberNameIn
			}, callback, asyncState);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005E6A File Offset: 0x0000406A
		public void EndCompleteMemberEmailMigration(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005E74 File Offset: 0x00004074
		public void CompleteMemberEmailMigrationAsync(string memberNameIn)
		{
			this.CompleteMemberEmailMigrationAsync(memberNameIn, null);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005E80 File Offset: 0x00004080
		public void CompleteMemberEmailMigrationAsync(string memberNameIn, object userState)
		{
			if (this.CompleteMemberEmailMigrationOperationCompleted == null)
			{
				this.CompleteMemberEmailMigrationOperationCompleted = new SendOrPostCallback(this.OnCompleteMemberEmailMigrationOperationCompleted);
			}
			base.InvokeAsync("CompleteMemberEmailMigration", new object[]
			{
				memberNameIn
			}, this.CompleteMemberEmailMigrationOperationCompleted, userState);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005EC8 File Offset: 0x000040C8
		private void OnCompleteMemberEmailMigrationOperationCompleted(object arg)
		{
			if (this.CompleteMemberEmailMigrationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CompleteMemberEmailMigrationCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00005F08 File Offset: 0x00004108
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/CompleteDomainEmailMigration", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("PartnerAuthHeaderValue")]
		public void CompleteDomainEmailMigration(string domainName)
		{
			base.Invoke("CompleteDomainEmailMigration", new object[]
			{
				domainName
			});
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00005F30 File Offset: 0x00004130
		public IAsyncResult BeginCompleteDomainEmailMigration(string domainName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CompleteDomainEmailMigration", new object[]
			{
				domainName
			}, callback, asyncState);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00005F56 File Offset: 0x00004156
		public void EndCompleteDomainEmailMigration(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00005F60 File Offset: 0x00004160
		public void CompleteDomainEmailMigrationAsync(string domainName)
		{
			this.CompleteDomainEmailMigrationAsync(domainName, null);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00005F6C File Offset: 0x0000416C
		public void CompleteDomainEmailMigrationAsync(string domainName, object userState)
		{
			if (this.CompleteDomainEmailMigrationOperationCompleted == null)
			{
				this.CompleteDomainEmailMigrationOperationCompleted = new SendOrPostCallback(this.OnCompleteDomainEmailMigrationOperationCompleted);
			}
			base.InvokeAsync("CompleteDomainEmailMigration", new object[]
			{
				domainName
			}, this.CompleteDomainEmailMigrationOperationCompleted, userState);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00005FB4 File Offset: 0x000041B4
		private void OnCompleteDomainEmailMigrationOperationCompleted(object arg)
		{
			if (this.CompleteDomainEmailMigrationCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CompleteDomainEmailMigrationCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00005FF4 File Offset: 0x000041F4
		[SoapHeader("PartnerAuthHeaderValue")]
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/CreateByodRequest", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public void CreateByodRequest(string domainName, string domainConfigId, string netId)
		{
			base.Invoke("CreateByodRequest", new object[]
			{
				domainName,
				domainConfigId,
				netId
			});
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00006024 File Offset: 0x00004224
		public IAsyncResult BeginCreateByodRequest(string domainName, string domainConfigId, string netId, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("CreateByodRequest", new object[]
			{
				domainName,
				domainConfigId,
				netId
			}, callback, asyncState);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00006054 File Offset: 0x00004254
		public void EndCreateByodRequest(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000605E File Offset: 0x0000425E
		public void CreateByodRequestAsync(string domainName, string domainConfigId, string netId)
		{
			this.CreateByodRequestAsync(domainName, domainConfigId, netId, null);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000606C File Offset: 0x0000426C
		public void CreateByodRequestAsync(string domainName, string domainConfigId, string netId, object userState)
		{
			if (this.CreateByodRequestOperationCompleted == null)
			{
				this.CreateByodRequestOperationCompleted = new SendOrPostCallback(this.OnCreateByodRequestOperationCompleted);
			}
			base.InvokeAsync("CreateByodRequest", new object[]
			{
				domainName,
				domainConfigId,
				netId
			}, this.CreateByodRequestOperationCompleted, userState);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000060BC File Offset: 0x000042BC
		private void OnCreateByodRequestOperationCompleted(object arg)
		{
			if (this.CreateByodRequestCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.CreateByodRequestCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000060FC File Offset: 0x000042FC
		[SoapHeader("PartnerAuthHeaderValue")]
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/EnumByodDomains", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public DomainInfoEx[] EnumByodDomains(string netId)
		{
			object[] array = base.Invoke("EnumByodDomains", new object[]
			{
				netId
			});
			return (DomainInfoEx[])array[0];
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000612C File Offset: 0x0000432C
		public IAsyncResult BeginEnumByodDomains(string netId, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("EnumByodDomains", new object[]
			{
				netId
			}, callback, asyncState);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00006154 File Offset: 0x00004354
		public DomainInfoEx[] EndEnumByodDomains(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (DomainInfoEx[])array[0];
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00006171 File Offset: 0x00004371
		public void EnumByodDomainsAsync(string netId)
		{
			this.EnumByodDomainsAsync(netId, null);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000617C File Offset: 0x0000437C
		public void EnumByodDomainsAsync(string netId, object userState)
		{
			if (this.EnumByodDomainsOperationCompleted == null)
			{
				this.EnumByodDomainsOperationCompleted = new SendOrPostCallback(this.OnEnumByodDomainsOperationCompleted);
			}
			base.InvokeAsync("EnumByodDomains", new object[]
			{
				netId
			}, this.EnumByodDomainsOperationCompleted, userState);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000061C4 File Offset: 0x000043C4
		private void OnEnumByodDomainsOperationCompleted(object arg)
		{
			if (this.EnumByodDomainsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.EnumByodDomainsCompleted(this, new EnumByodDomainsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000620C File Offset: 0x0000440C
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/GetAdmins", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("AdminPassportAuthHeaderValue")]
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		[SoapHeader("PartnerAuthHeaderValue")]
		public Admin[] GetAdmins(string domainName)
		{
			object[] array = base.Invoke("GetAdmins", new object[]
			{
				domainName
			});
			return (Admin[])array[0];
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000623C File Offset: 0x0000443C
		public IAsyncResult BeginGetAdmins(string domainName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetAdmins", new object[]
			{
				domainName
			}, callback, asyncState);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00006264 File Offset: 0x00004464
		public Admin[] EndGetAdmins(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (Admin[])array[0];
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006281 File Offset: 0x00004481
		public void GetAdminsAsync(string domainName)
		{
			this.GetAdminsAsync(domainName, null);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000628C File Offset: 0x0000448C
		public void GetAdminsAsync(string domainName, object userState)
		{
			if (this.GetAdminsOperationCompleted == null)
			{
				this.GetAdminsOperationCompleted = new SendOrPostCallback(this.OnGetAdminsOperationCompleted);
			}
			base.InvokeAsync("GetAdmins", new object[]
			{
				domainName
			}, this.GetAdminsOperationCompleted, userState);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000062D4 File Offset: 0x000044D4
		private void OnGetAdminsOperationCompleted(object arg)
		{
			if (this.GetAdminsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetAdminsCompleted(this, new GetAdminsCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000631C File Offset: 0x0000451C
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/GetManagementCertificate", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		[SoapHeader("AdminPassportAuthHeaderValue")]
		[SoapHeader("PartnerAuthHeaderValue")]
		public CertData GetManagementCertificate(string domainName)
		{
			object[] array = base.Invoke("GetManagementCertificate", new object[]
			{
				domainName
			});
			return (CertData)array[0];
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000634C File Offset: 0x0000454C
		public IAsyncResult BeginGetManagementCertificate(string domainName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetManagementCertificate", new object[]
			{
				domainName
			}, callback, asyncState);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00006374 File Offset: 0x00004574
		public CertData EndGetManagementCertificate(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (CertData)array[0];
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00006391 File Offset: 0x00004591
		public void GetManagementCertificateAsync(string domainName)
		{
			this.GetManagementCertificateAsync(domainName, null);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000639C File Offset: 0x0000459C
		public void GetManagementCertificateAsync(string domainName, object userState)
		{
			if (this.GetManagementCertificateOperationCompleted == null)
			{
				this.GetManagementCertificateOperationCompleted = new SendOrPostCallback(this.OnGetManagementCertificateOperationCompleted);
			}
			base.InvokeAsync("GetManagementCertificate", new object[]
			{
				domainName
			}, this.GetManagementCertificateOperationCompleted, userState);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000063E4 File Offset: 0x000045E4
		private void OnGetManagementCertificateOperationCompleted(object arg)
		{
			if (this.GetManagementCertificateCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetManagementCertificateCompleted(this, new GetManagementCertificateCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000642C File Offset: 0x0000462C
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/SetManagementCertificate", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("PartnerAuthHeaderValue")]
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		[SoapHeader("AdminPassportAuthHeaderValue")]
		public void SetManagementCertificate(string domainName, CertData managementCertificate)
		{
			base.Invoke("SetManagementCertificate", new object[]
			{
				domainName,
				managementCertificate
			});
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006458 File Offset: 0x00004658
		public IAsyncResult BeginSetManagementCertificate(string domainName, CertData managementCertificate, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetManagementCertificate", new object[]
			{
				domainName,
				managementCertificate
			}, callback, asyncState);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00006483 File Offset: 0x00004683
		public void EndSetManagementCertificate(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000648D File Offset: 0x0000468D
		public void SetManagementCertificateAsync(string domainName, CertData managementCertificate)
		{
			this.SetManagementCertificateAsync(domainName, managementCertificate, null);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006498 File Offset: 0x00004698
		public void SetManagementCertificateAsync(string domainName, CertData managementCertificate, object userState)
		{
			if (this.SetManagementCertificateOperationCompleted == null)
			{
				this.SetManagementCertificateOperationCompleted = new SendOrPostCallback(this.OnSetManagementCertificateOperationCompleted);
			}
			base.InvokeAsync("SetManagementCertificate", new object[]
			{
				domainName,
				managementCertificate
			}, this.SetManagementCertificateOperationCompleted, userState);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000064E4 File Offset: 0x000046E4
		private void OnSetManagementCertificateOperationCompleted(object arg)
		{
			if (this.SetManagementCertificateCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetManagementCertificateCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00006524 File Offset: 0x00004724
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/SetManagementPermissions", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("PartnerAuthHeaderValue")]
		public void SetManagementPermissions(PermissionFlags flags)
		{
			base.Invoke("SetManagementPermissions", new object[]
			{
				flags
			});
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00006550 File Offset: 0x00004750
		public IAsyncResult BeginSetManagementPermissions(PermissionFlags flags, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetManagementPermissions", new object[]
			{
				flags
			}, callback, asyncState);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000657B File Offset: 0x0000477B
		public void EndSetManagementPermissions(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00006585 File Offset: 0x00004785
		public void SetManagementPermissionsAsync(PermissionFlags flags)
		{
			this.SetManagementPermissionsAsync(flags, null);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00006590 File Offset: 0x00004790
		public void SetManagementPermissionsAsync(PermissionFlags flags, object userState)
		{
			if (this.SetManagementPermissionsOperationCompleted == null)
			{
				this.SetManagementPermissionsOperationCompleted = new SendOrPostCallback(this.OnSetManagementPermissionsOperationCompleted);
			}
			base.InvokeAsync("SetManagementPermissions", new object[]
			{
				flags
			}, this.SetManagementPermissionsOperationCompleted, userState);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000065DC File Offset: 0x000047DC
		private void OnSetManagementPermissionsOperationCompleted(object arg)
		{
			if (this.SetManagementPermissionsCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetManagementPermissionsCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000661C File Offset: 0x0000481C
		[SoapHeader("AdminPassportAuthHeaderValue")]
		[SoapHeader("PartnerAuthHeaderValue")]
		[SoapHeader("ManagementCertificateAuthHeaderValue")]
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/GetMaxMembers", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public int GetMaxMembers(string domainName)
		{
			object[] array = base.Invoke("GetMaxMembers", new object[]
			{
				domainName
			});
			return (int)array[0];
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000664C File Offset: 0x0000484C
		public IAsyncResult BeginGetMaxMembers(string domainName, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("GetMaxMembers", new object[]
			{
				domainName
			}, callback, asyncState);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00006674 File Offset: 0x00004874
		public int EndGetMaxMembers(IAsyncResult asyncResult)
		{
			object[] array = base.EndInvoke(asyncResult);
			return (int)array[0];
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00006691 File Offset: 0x00004891
		public void GetMaxMembersAsync(string domainName)
		{
			this.GetMaxMembersAsync(domainName, null);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000669C File Offset: 0x0000489C
		public void GetMaxMembersAsync(string domainName, object userState)
		{
			if (this.GetMaxMembersOperationCompleted == null)
			{
				this.GetMaxMembersOperationCompleted = new SendOrPostCallback(this.OnGetMaxMembersOperationCompleted);
			}
			base.InvokeAsync("GetMaxMembers", new object[]
			{
				domainName
			}, this.GetMaxMembersOperationCompleted, userState);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000066E4 File Offset: 0x000048E4
		private void OnGetMaxMembersOperationCompleted(object arg)
		{
			if (this.GetMaxMembersCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.GetMaxMembersCompleted(this, new GetMaxMembersCompletedEventArgs(invokeCompletedEventArgs.Results, invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000672C File Offset: 0x0000492C
		[SoapDocumentMethod("http://domains.live.com/Service/DomainServices/V1.0/SetMaxMembers", RequestNamespace = "http://domains.live.com/Service/DomainServices/V1.0", ResponseNamespace = "http://domains.live.com/Service/DomainServices/V1.0", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[SoapHeader("PartnerAuthHeaderValue")]
		public void SetMaxMembers(string domainName, int maxUsers)
		{
			base.Invoke("SetMaxMembers", new object[]
			{
				domainName,
				maxUsers
			});
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000675C File Offset: 0x0000495C
		public IAsyncResult BeginSetMaxMembers(string domainName, int maxUsers, AsyncCallback callback, object asyncState)
		{
			return base.BeginInvoke("SetMaxMembers", new object[]
			{
				domainName,
				maxUsers
			}, callback, asyncState);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000678C File Offset: 0x0000498C
		public void EndSetMaxMembers(IAsyncResult asyncResult)
		{
			base.EndInvoke(asyncResult);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00006796 File Offset: 0x00004996
		public void SetMaxMembersAsync(string domainName, int maxUsers)
		{
			this.SetMaxMembersAsync(domainName, maxUsers, null);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000067A4 File Offset: 0x000049A4
		public void SetMaxMembersAsync(string domainName, int maxUsers, object userState)
		{
			if (this.SetMaxMembersOperationCompleted == null)
			{
				this.SetMaxMembersOperationCompleted = new SendOrPostCallback(this.OnSetMaxMembersOperationCompleted);
			}
			base.InvokeAsync("SetMaxMembers", new object[]
			{
				domainName,
				maxUsers
			}, this.SetMaxMembersOperationCompleted, userState);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000067F4 File Offset: 0x000049F4
		private void OnSetMaxMembersOperationCompleted(object arg)
		{
			if (this.SetMaxMembersCompleted != null)
			{
				InvokeCompletedEventArgs invokeCompletedEventArgs = (InvokeCompletedEventArgs)arg;
				this.SetMaxMembersCompleted(this, new AsyncCompletedEventArgs(invokeCompletedEventArgs.Error, invokeCompletedEventArgs.Cancelled, invokeCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00006833 File Offset: 0x00004A33
		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		// Token: 0x04000001 RID: 1
		private AdminPassportAuthHeader adminPassportAuthHeaderValueField;

		// Token: 0x04000002 RID: 2
		private ManagementCertificateAuthHeader managementCertificateAuthHeaderValueField;

		// Token: 0x04000003 RID: 3
		private PartnerAuthHeader partnerAuthHeaderValueField;

		// Token: 0x04000004 RID: 4
		private SendOrPostCallback TestConnectionOperationCompleted;

		// Token: 0x04000005 RID: 5
		private SendOrPostCallback GetDomainAvailabilityOperationCompleted;

		// Token: 0x04000006 RID: 6
		private SendOrPostCallback GetDomainInfoOperationCompleted;

		// Token: 0x04000007 RID: 7
		private SendOrPostCallback GetDomainInfoExOperationCompleted;

		// Token: 0x04000008 RID: 8
		private SendOrPostCallback ReserveDomainOperationCompleted;

		// Token: 0x04000009 RID: 9
		private SendOrPostCallback ReleaseDomainOperationCompleted;

		// Token: 0x0400000A RID: 10
		private SendOrPostCallback ProcessDomainOperationCompleted;

		// Token: 0x0400000B RID: 11
		private SendOrPostCallback GetMemberTypeOperationCompleted;

		// Token: 0x0400000C RID: 12
		private SendOrPostCallback MemberNameToNetIdOperationCompleted;

		// Token: 0x0400000D RID: 13
		private SendOrPostCallback NetIdToMemberNameOperationCompleted;

		// Token: 0x0400000E RID: 14
		private SendOrPostCallback GetCountMembersOperationCompleted;

		// Token: 0x0400000F RID: 15
		private SendOrPostCallback EnumMembersOperationCompleted;

		// Token: 0x04000010 RID: 16
		private SendOrPostCallback CreateMemberOperationCompleted;

		// Token: 0x04000011 RID: 17
		private SendOrPostCallback CreateMemberEncryptedOperationCompleted;

		// Token: 0x04000012 RID: 18
		private SendOrPostCallback CreateMemberExOperationCompleted;

		// Token: 0x04000013 RID: 19
		private SendOrPostCallback CreateMemberEncryptedExOperationCompleted;

		// Token: 0x04000014 RID: 20
		private SendOrPostCallback AddBrandInfoOperationCompleted;

		// Token: 0x04000015 RID: 21
		private SendOrPostCallback RemoveBrandInfoOperationCompleted;

		// Token: 0x04000016 RID: 22
		private SendOrPostCallback RenameMemberOperationCompleted;

		// Token: 0x04000017 RID: 23
		private SendOrPostCallback SetMemberPropertiesOperationCompleted;

		// Token: 0x04000018 RID: 24
		private SendOrPostCallback GetMemberPropertiesOperationCompleted;

		// Token: 0x04000019 RID: 25
		private SendOrPostCallback EvictMemberOperationCompleted;

		// Token: 0x0400001A RID: 26
		private SendOrPostCallback ResetMemberPasswordOperationCompleted;

		// Token: 0x0400001B RID: 27
		private SendOrPostCallback ResetMemberPasswordEncryptedOperationCompleted;

		// Token: 0x0400001C RID: 28
		private SendOrPostCallback BlockMemberEmailOperationCompleted;

		// Token: 0x0400001D RID: 29
		private SendOrPostCallback ImportUnmanagedMemberOperationCompleted;

		// Token: 0x0400001E RID: 30
		private SendOrPostCallback EvictUnmanagedMemberOperationCompleted;

		// Token: 0x0400001F RID: 31
		private SendOrPostCallback EvictAllUnmanagedMembersOperationCompleted;

		// Token: 0x04000020 RID: 32
		private SendOrPostCallback EnableOpenMembershipOperationCompleted;

		// Token: 0x04000021 RID: 33
		private SendOrPostCallback ProvisionMemberSubscriptionOperationCompleted;

		// Token: 0x04000022 RID: 34
		private SendOrPostCallback DeprovisionMemberSubscriptionOperationCompleted;

		// Token: 0x04000023 RID: 35
		private SendOrPostCallback ConvertMemberSubscriptionOperationCompleted;

		// Token: 0x04000024 RID: 36
		private SendOrPostCallback MemberHasSubscriptionOperationCompleted;

		// Token: 0x04000025 RID: 37
		private SendOrPostCallback SuspendEmailOperationCompleted;

		// Token: 0x04000026 RID: 38
		private SendOrPostCallback GetMxRecordsOperationCompleted;

		// Token: 0x04000027 RID: 39
		private SendOrPostCallback MemberHasMailboxOperationCompleted;

		// Token: 0x04000028 RID: 40
		private SendOrPostCallback CompleteMemberEmailMigrationOperationCompleted;

		// Token: 0x04000029 RID: 41
		private SendOrPostCallback CompleteDomainEmailMigrationOperationCompleted;

		// Token: 0x0400002A RID: 42
		private SendOrPostCallback CreateByodRequestOperationCompleted;

		// Token: 0x0400002B RID: 43
		private SendOrPostCallback EnumByodDomainsOperationCompleted;

		// Token: 0x0400002C RID: 44
		private SendOrPostCallback GetAdminsOperationCompleted;

		// Token: 0x0400002D RID: 45
		private SendOrPostCallback GetManagementCertificateOperationCompleted;

		// Token: 0x0400002E RID: 46
		private SendOrPostCallback SetManagementCertificateOperationCompleted;

		// Token: 0x0400002F RID: 47
		private SendOrPostCallback SetManagementPermissionsOperationCompleted;

		// Token: 0x04000030 RID: 48
		private SendOrPostCallback GetMaxMembersOperationCompleted;

		// Token: 0x04000031 RID: 49
		private SendOrPostCallback SetMaxMembersOperationCompleted;
	}
}
