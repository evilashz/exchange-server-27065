using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200007F RID: 127
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class Strings
	{
		// Token: 0x0600026C RID: 620 RVA: 0x0000A958 File Offset: 0x00008B58
		static Strings()
		{
			Strings.stringIDs.Add(1200652681U, "NspiTaskTitle");
			Strings.stringIDs.Add(4108158018U, "OutlookTaskDescription");
			Strings.stringIDs.Add(2906070402U, "AccountDisplayName");
			Strings.stringIDs.Add(1735106406U, "NspiBindTaskTitle");
			Strings.stringIDs.Add(3751775428U, "ScomAlertLoggerTaskStartProperties");
			Strings.stringIDs.Add(519952531U, "NspiTaskDescription");
			Strings.stringIDs.Add(2627813696U, "NspiGetMatchesTaskTitle");
			Strings.stringIDs.Add(2088641341U, "EmsmdbTaskTitle");
			Strings.stringIDs.Add(4012877537U, "VerifyRpcProxyTaskDescription");
			Strings.stringIDs.Add(328709952U, "DiscoverWebProxyTaskDescription");
			Strings.stringIDs.Add(1000340169U, "NspiQueryHomeMDBTaskDescription");
			Strings.stringIDs.Add(3433159169U, "AsyncTaskDescription");
			Strings.stringIDs.Add(2307157568U, "RfriTaskTitle");
			Strings.stringIDs.Add(2471490565U, "NspiGetPropsTaskTitle");
			Strings.stringIDs.Add(3194835879U, "NspiGetPropsTaskDescription");
			Strings.stringIDs.Add(3762287994U, "NspiGetNetworkAddressesPropertyTaskDescription");
			Strings.stringIDs.Add(4086077102U, "NspiGetHierarchyInfoTaskTitle");
			Strings.stringIDs.Add(375864490U, "InputPasswordRequired");
			Strings.stringIDs.Add(3350259510U, "NspiGetNetworkAddressesPropertyTaskTitle");
			Strings.stringIDs.Add(3206764939U, "VerifyRpcProxyTaskTitle");
			Strings.stringIDs.Add(1328936299U, "WrongAuthForPersonalizedServer");
			Strings.stringIDs.Add(2224461727U, "EmsmdbTaskDescription");
			Strings.stringIDs.Add(1666890870U, "WrongDefinitionType");
			Strings.stringIDs.Add(4202007121U, "RfriGetFqdnTaskTitle");
			Strings.stringIDs.Add(972661725U, "SecondaryEndpoint");
			Strings.stringIDs.Add(3707932024U, "NspiGetMatchesTaskDescription");
			Strings.stringIDs.Add(3047902021U, "EmsmdbConnectTaskDescription");
			Strings.stringIDs.Add(3157018328U, "RfriGetNewDsaTaskDescription");
			Strings.stringIDs.Add(1880067925U, "DummyTaskTitle");
			Strings.stringIDs.Add(2936865088U, "NspiQueryRowsTaskTitle");
			Strings.stringIDs.Add(3754643782U, "ExtensionAttributes");
			Strings.stringIDs.Add(3942413427U, "DummyTaskDescription");
			Strings.stringIDs.Add(1195865221U, "InputPasswordNotRequired");
			Strings.stringIDs.Add(4271094603U, "NspiGetNetworkAddressesTaskDescription");
			Strings.stringIDs.Add(3049602642U, "NspiGetHierarchyInfoTaskDescription");
			Strings.stringIDs.Add(1196155619U, "Endpoint");
			Strings.stringIDs.Add(613263819U, "NspiQueryHomeMDBTaskTitle");
			Strings.stringIDs.Add(1958074863U, "RfriGetFqdnTaskDescription");
			Strings.stringIDs.Add(52542312U, "DiscoverWebProxyTaskTitle");
			Strings.stringIDs.Add(3709524604U, "RfriGetNewDsaTaskTitle");
			Strings.stringIDs.Add(547095242U, "OutlookTaskTitle");
			Strings.stringIDs.Add(4266394152U, "ScomAlertLoggerTaskPropertyNullValue");
			Strings.stringIDs.Add(611708359U, "RetryTaskDescription");
			Strings.stringIDs.Add(433052918U, "EmsmdbLogonTaskTitle");
			Strings.stringIDs.Add(342672400U, "RcaOutlookTaskTitle");
			Strings.stringIDs.Add(1152705677U, "MonitoringAccount");
			Strings.stringIDs.Add(1587959117U, "PFEmsmdbTaskDescription");
			Strings.stringIDs.Add(3982499041U, "ScomAlertLoggerTaskCompletedProperties");
			Strings.stringIDs.Add(3321608102U, "EmsmdbLogonTaskDescription");
			Strings.stringIDs.Add(2896583015U, "EmsmdbConnectTaskTitle");
			Strings.stringIDs.Add(3911446795U, "PFEmsmdbTaskTitle");
			Strings.stringIDs.Add(3508157170U, "MonitoringAccountPassword");
			Strings.stringIDs.Add(1342291712U, "PFEmsmdbLogonTaskDescription");
			Strings.stringIDs.Add(620115544U, "RcaOutlookTaskDescription");
			Strings.stringIDs.Add(2112045541U, "NspiGetNetworkAddressesTaskTitle");
			Strings.stringIDs.Add(46727295U, "NspiUnbindTaskTitle");
			Strings.stringIDs.Add(2723049729U, "TaskExceptionMessage");
			Strings.stringIDs.Add(1442961372U, "NspiQueryRowsTaskDescription");
			Strings.stringIDs.Add(4002896033U, "NspiUnbindTaskDescription");
			Strings.stringIDs.Add(33448767U, "NspiDNToEphTaskDescription");
			Strings.stringIDs.Add(43152595U, "PFEmsmdbConnectTaskDescription");
			Strings.stringIDs.Add(763528660U, "RfriTaskDescription");
			Strings.stringIDs.Add(1421893477U, "RetryTaskTitle");
			Strings.stringIDs.Add(1091351742U, "NspiBindTaskDescription");
			Strings.stringIDs.Add(4164811492U, "Identity");
			Strings.stringIDs.Add(1994569285U, "NspiDNToEphTaskTitle");
			Strings.stringIDs.Add(3045810739U, "AsyncTaskTitle");
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000AED0 File Offset: 0x000090D0
		public static LocalizedString NspiTaskTitle
		{
			get
			{
				return new LocalizedString("NspiTaskTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000AEE7 File Offset: 0x000090E7
		public static LocalizedString OutlookTaskDescription
		{
			get
			{
				return new LocalizedString("OutlookTaskDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000AEFE File Offset: 0x000090FE
		public static LocalizedString AccountDisplayName
		{
			get
			{
				return new LocalizedString("AccountDisplayName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000AF15 File Offset: 0x00009115
		public static LocalizedString NspiBindTaskTitle
		{
			get
			{
				return new LocalizedString("NspiBindTaskTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000AF2C File Offset: 0x0000912C
		public static LocalizedString ScomAlertLoggerTaskStartProperties
		{
			get
			{
				return new LocalizedString("ScomAlertLoggerTaskStartProperties", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000AF43 File Offset: 0x00009143
		public static LocalizedString NspiTaskDescription
		{
			get
			{
				return new LocalizedString("NspiTaskDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000AF5A File Offset: 0x0000915A
		public static LocalizedString NspiGetMatchesTaskTitle
		{
			get
			{
				return new LocalizedString("NspiGetMatchesTaskTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000AF71 File Offset: 0x00009171
		public static LocalizedString EmsmdbTaskTitle
		{
			get
			{
				return new LocalizedString("EmsmdbTaskTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000AF88 File Offset: 0x00009188
		public static LocalizedString VerifyRpcProxyTaskDescription
		{
			get
			{
				return new LocalizedString("VerifyRpcProxyTaskDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000AF9F File Offset: 0x0000919F
		public static LocalizedString DiscoverWebProxyTaskDescription
		{
			get
			{
				return new LocalizedString("DiscoverWebProxyTaskDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000AFB6 File Offset: 0x000091B6
		public static LocalizedString NspiQueryHomeMDBTaskDescription
		{
			get
			{
				return new LocalizedString("NspiQueryHomeMDBTaskDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000AFCD File Offset: 0x000091CD
		public static LocalizedString AsyncTaskDescription
		{
			get
			{
				return new LocalizedString("AsyncTaskDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000AFE4 File Offset: 0x000091E4
		public static LocalizedString RfriTaskTitle
		{
			get
			{
				return new LocalizedString("RfriTaskTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000AFFB File Offset: 0x000091FB
		public static LocalizedString NspiGetPropsTaskTitle
		{
			get
			{
				return new LocalizedString("NspiGetPropsTaskTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000B012 File Offset: 0x00009212
		public static LocalizedString NspiGetPropsTaskDescription
		{
			get
			{
				return new LocalizedString("NspiGetPropsTaskDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000B029 File Offset: 0x00009229
		public static LocalizedString NspiGetNetworkAddressesPropertyTaskDescription
		{
			get
			{
				return new LocalizedString("NspiGetNetworkAddressesPropertyTaskDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000B040 File Offset: 0x00009240
		public static LocalizedString NspiGetHierarchyInfoTaskTitle
		{
			get
			{
				return new LocalizedString("NspiGetHierarchyInfoTaskTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000B058 File Offset: 0x00009258
		public static LocalizedString ScomAlertLoggerTaskFailed(LocalizedString taskName)
		{
			return new LocalizedString("ScomAlertLoggerTaskFailed", Strings.ResourceManager, new object[]
			{
				taskName
			});
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000B085 File Offset: 0x00009285
		public static LocalizedString InputPasswordRequired
		{
			get
			{
				return new LocalizedString("InputPasswordRequired", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000B09C File Offset: 0x0000929C
		public static LocalizedString NspiGetNetworkAddressesPropertyTaskTitle
		{
			get
			{
				return new LocalizedString("NspiGetNetworkAddressesPropertyTaskTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000B0B3 File Offset: 0x000092B3
		public static LocalizedString VerifyRpcProxyTaskTitle
		{
			get
			{
				return new LocalizedString("VerifyRpcProxyTaskTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000B0CA File Offset: 0x000092CA
		public static LocalizedString WrongAuthForPersonalizedServer
		{
			get
			{
				return new LocalizedString("WrongAuthForPersonalizedServer", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000B0E1 File Offset: 0x000092E1
		public static LocalizedString EmsmdbTaskDescription
		{
			get
			{
				return new LocalizedString("EmsmdbTaskDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000B0F8 File Offset: 0x000092F8
		public static LocalizedString WrongDefinitionType
		{
			get
			{
				return new LocalizedString("WrongDefinitionType", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000B10F File Offset: 0x0000930F
		public static LocalizedString RfriGetFqdnTaskTitle
		{
			get
			{
				return new LocalizedString("RfriGetFqdnTaskTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000B126 File Offset: 0x00009326
		public static LocalizedString SecondaryEndpoint
		{
			get
			{
				return new LocalizedString("SecondaryEndpoint", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000B13D File Offset: 0x0000933D
		public static LocalizedString NspiGetMatchesTaskDescription
		{
			get
			{
				return new LocalizedString("NspiGetMatchesTaskDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000B154 File Offset: 0x00009354
		public static LocalizedString EmsmdbConnectTaskDescription
		{
			get
			{
				return new LocalizedString("EmsmdbConnectTaskDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000B16B File Offset: 0x0000936B
		public static LocalizedString RfriGetNewDsaTaskDescription
		{
			get
			{
				return new LocalizedString("RfriGetNewDsaTaskDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000B182 File Offset: 0x00009382
		public static LocalizedString DummyTaskTitle
		{
			get
			{
				return new LocalizedString("DummyTaskTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000B199 File Offset: 0x00009399
		public static LocalizedString NspiQueryRowsTaskTitle
		{
			get
			{
				return new LocalizedString("NspiQueryRowsTaskTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000B1B0 File Offset: 0x000093B0
		public static LocalizedString CompositeTaskTitle(int numberOfTasks)
		{
			return new LocalizedString("CompositeTaskTitle", Strings.ResourceManager, new object[]
			{
				numberOfTasks
			});
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000B1DD File Offset: 0x000093DD
		public static LocalizedString ExtensionAttributes
		{
			get
			{
				return new LocalizedString("ExtensionAttributes", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000B1F4 File Offset: 0x000093F4
		public static LocalizedString RpcCallResultErrorCodeDescription(string callResultType)
		{
			return new LocalizedString("RpcCallResultErrorCodeDescription", Strings.ResourceManager, new object[]
			{
				callResultType
			});
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000B21C File Offset: 0x0000941C
		public static LocalizedString DummyTaskDescription
		{
			get
			{
				return new LocalizedString("DummyTaskDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000B234 File Offset: 0x00009434
		public static LocalizedString CompositeTaskDescription(int numberOfTasks)
		{
			return new LocalizedString("CompositeTaskDescription", Strings.ResourceManager, new object[]
			{
				numberOfTasks
			});
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000B264 File Offset: 0x00009464
		public static LocalizedString NetworkCredentialString(string domain, string userName)
		{
			return new LocalizedString("NetworkCredentialString", Strings.ResourceManager, new object[]
			{
				domain,
				userName
			});
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000B290 File Offset: 0x00009490
		public static LocalizedString InputPasswordNotRequired
		{
			get
			{
				return new LocalizedString("InputPasswordNotRequired", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000B2A7 File Offset: 0x000094A7
		public static LocalizedString NspiGetNetworkAddressesTaskDescription
		{
			get
			{
				return new LocalizedString("NspiGetNetworkAddressesTaskDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000B2BE File Offset: 0x000094BE
		public static LocalizedString NspiGetHierarchyInfoTaskDescription
		{
			get
			{
				return new LocalizedString("NspiGetHierarchyInfoTaskDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000B2D5 File Offset: 0x000094D5
		public static LocalizedString Endpoint
		{
			get
			{
				return new LocalizedString("Endpoint", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000B2EC File Offset: 0x000094EC
		public static LocalizedString NspiQueryHomeMDBTaskTitle
		{
			get
			{
				return new LocalizedString("NspiQueryHomeMDBTaskTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000B303 File Offset: 0x00009503
		public static LocalizedString RfriGetFqdnTaskDescription
		{
			get
			{
				return new LocalizedString("RfriGetFqdnTaskDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000B31A File Offset: 0x0000951A
		public static LocalizedString DiscoverWebProxyTaskTitle
		{
			get
			{
				return new LocalizedString("DiscoverWebProxyTaskTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000B331 File Offset: 0x00009531
		public static LocalizedString RfriGetNewDsaTaskTitle
		{
			get
			{
				return new LocalizedString("RfriGetNewDsaTaskTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000B348 File Offset: 0x00009548
		public static LocalizedString OutlookTaskTitle
		{
			get
			{
				return new LocalizedString("OutlookTaskTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000B35F File Offset: 0x0000955F
		public static LocalizedString ScomAlertLoggerTaskPropertyNullValue
		{
			get
			{
				return new LocalizedString("ScomAlertLoggerTaskPropertyNullValue", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000B376 File Offset: 0x00009576
		public static LocalizedString RetryTaskDescription
		{
			get
			{
				return new LocalizedString("RetryTaskDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000B38D File Offset: 0x0000958D
		public static LocalizedString EmsmdbLogonTaskTitle
		{
			get
			{
				return new LocalizedString("EmsmdbLogonTaskTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000B3A4 File Offset: 0x000095A4
		public static LocalizedString RcaOutlookTaskTitle
		{
			get
			{
				return new LocalizedString("RcaOutlookTaskTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000B3BC File Offset: 0x000095BC
		public static LocalizedString ScomAlertLoggerTaskProperty(string requiredProperty, string requiredPropertyValue)
		{
			return new LocalizedString("ScomAlertLoggerTaskProperty", Strings.ResourceManager, new object[]
			{
				requiredProperty,
				requiredPropertyValue
			});
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000B3E8 File Offset: 0x000095E8
		public static LocalizedString ScomAlertLoggerIndent(LocalizedString textToIndent)
		{
			return new LocalizedString("ScomAlertLoggerIndent", Strings.ResourceManager, new object[]
			{
				textToIndent
			});
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000B418 File Offset: 0x00009618
		public static LocalizedString ScomAlertLoggerTaskSucceeded(LocalizedString taskName)
		{
			return new LocalizedString("ScomAlertLoggerTaskSucceeded", Strings.ResourceManager, new object[]
			{
				taskName
			});
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000B445 File Offset: 0x00009645
		public static LocalizedString MonitoringAccount
		{
			get
			{
				return new LocalizedString("MonitoringAccount", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000B45C File Offset: 0x0000965C
		public static LocalizedString PFEmsmdbTaskDescription
		{
			get
			{
				return new LocalizedString("PFEmsmdbTaskDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000B474 File Offset: 0x00009674
		public static LocalizedString ScomAlertLoggerTaskStarted(LocalizedString taskName)
		{
			return new LocalizedString("ScomAlertLoggerTaskStarted", Strings.ResourceManager, new object[]
			{
				taskName
			});
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000B4A1 File Offset: 0x000096A1
		public static LocalizedString ScomAlertLoggerTaskCompletedProperties
		{
			get
			{
				return new LocalizedString("ScomAlertLoggerTaskCompletedProperties", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000B4B8 File Offset: 0x000096B8
		public static LocalizedString EmsmdbLogonTaskDescription
		{
			get
			{
				return new LocalizedString("EmsmdbLogonTaskDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000B4CF File Offset: 0x000096CF
		public static LocalizedString EmsmdbConnectTaskTitle
		{
			get
			{
				return new LocalizedString("EmsmdbConnectTaskTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000B4E6 File Offset: 0x000096E6
		public static LocalizedString PFEmsmdbTaskTitle
		{
			get
			{
				return new LocalizedString("PFEmsmdbTaskTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000B4FD File Offset: 0x000096FD
		public static LocalizedString MonitoringAccountPassword
		{
			get
			{
				return new LocalizedString("MonitoringAccountPassword", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000B514 File Offset: 0x00009714
		public static LocalizedString PropertyNotFoundExceptionMessage(string propertyName)
		{
			return new LocalizedString("PropertyNotFoundExceptionMessage", Strings.ResourceManager, new object[]
			{
				propertyName
			});
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000B53C File Offset: 0x0000973C
		public static LocalizedString PFEmsmdbLogonTaskDescription
		{
			get
			{
				return new LocalizedString("PFEmsmdbLogonTaskDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000B553 File Offset: 0x00009753
		public static LocalizedString RcaOutlookTaskDescription
		{
			get
			{
				return new LocalizedString("RcaOutlookTaskDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000B56A File Offset: 0x0000976A
		public static LocalizedString NspiGetNetworkAddressesTaskTitle
		{
			get
			{
				return new LocalizedString("NspiGetNetworkAddressesTaskTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000B581 File Offset: 0x00009781
		public static LocalizedString NspiUnbindTaskTitle
		{
			get
			{
				return new LocalizedString("NspiUnbindTaskTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000B598 File Offset: 0x00009798
		public static LocalizedString TaskExceptionMessage
		{
			get
			{
				return new LocalizedString("TaskExceptionMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000B5B0 File Offset: 0x000097B0
		public static LocalizedString ScomAlertLoggerTaskDescription(LocalizedString taskDescription)
		{
			return new LocalizedString("ScomAlertLoggerTaskDescription", Strings.ResourceManager, new object[]
			{
				taskDescription
			});
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000B5DD File Offset: 0x000097DD
		public static LocalizedString NspiQueryRowsTaskDescription
		{
			get
			{
				return new LocalizedString("NspiQueryRowsTaskDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000B5F4 File Offset: 0x000097F4
		public static LocalizedString NspiUnbindTaskDescription
		{
			get
			{
				return new LocalizedString("NspiUnbindTaskDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000B60B File Offset: 0x0000980B
		public static LocalizedString NspiDNToEphTaskDescription
		{
			get
			{
				return new LocalizedString("NspiDNToEphTaskDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000B622 File Offset: 0x00009822
		public static LocalizedString PFEmsmdbConnectTaskDescription
		{
			get
			{
				return new LocalizedString("PFEmsmdbConnectTaskDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000B639 File Offset: 0x00009839
		public static LocalizedString RfriTaskDescription
		{
			get
			{
				return new LocalizedString("RfriTaskDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000B650 File Offset: 0x00009850
		public static LocalizedString RetryTaskTitle
		{
			get
			{
				return new LocalizedString("RetryTaskTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000B667 File Offset: 0x00009867
		public static LocalizedString NspiBindTaskDescription
		{
			get
			{
				return new LocalizedString("NspiBindTaskDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000B67E File Offset: 0x0000987E
		public static LocalizedString Identity
		{
			get
			{
				return new LocalizedString("Identity", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000B695 File Offset: 0x00009895
		public static LocalizedString NspiDNToEphTaskTitle
		{
			get
			{
				return new LocalizedString("NspiDNToEphTaskTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000B6AC File Offset: 0x000098AC
		public static LocalizedString AsyncTaskTitle
		{
			get
			{
				return new LocalizedString("AsyncTaskTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000B6C4 File Offset: 0x000098C4
		public static LocalizedString ListOfItems(string list, string newItem)
		{
			return new LocalizedString("ListOfItems", Strings.ResourceManager, new object[]
			{
				list,
				newItem
			});
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000B6F0 File Offset: 0x000098F0
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000161 RID: 353
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(67);

		// Token: 0x04000162 RID: 354
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.RpcClientAccess.Monitoring.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000080 RID: 128
		public enum IDs : uint
		{
			// Token: 0x04000164 RID: 356
			NspiTaskTitle = 1200652681U,
			// Token: 0x04000165 RID: 357
			OutlookTaskDescription = 4108158018U,
			// Token: 0x04000166 RID: 358
			AccountDisplayName = 2906070402U,
			// Token: 0x04000167 RID: 359
			NspiBindTaskTitle = 1735106406U,
			// Token: 0x04000168 RID: 360
			ScomAlertLoggerTaskStartProperties = 3751775428U,
			// Token: 0x04000169 RID: 361
			NspiTaskDescription = 519952531U,
			// Token: 0x0400016A RID: 362
			NspiGetMatchesTaskTitle = 2627813696U,
			// Token: 0x0400016B RID: 363
			EmsmdbTaskTitle = 2088641341U,
			// Token: 0x0400016C RID: 364
			VerifyRpcProxyTaskDescription = 4012877537U,
			// Token: 0x0400016D RID: 365
			DiscoverWebProxyTaskDescription = 328709952U,
			// Token: 0x0400016E RID: 366
			NspiQueryHomeMDBTaskDescription = 1000340169U,
			// Token: 0x0400016F RID: 367
			AsyncTaskDescription = 3433159169U,
			// Token: 0x04000170 RID: 368
			RfriTaskTitle = 2307157568U,
			// Token: 0x04000171 RID: 369
			NspiGetPropsTaskTitle = 2471490565U,
			// Token: 0x04000172 RID: 370
			NspiGetPropsTaskDescription = 3194835879U,
			// Token: 0x04000173 RID: 371
			NspiGetNetworkAddressesPropertyTaskDescription = 3762287994U,
			// Token: 0x04000174 RID: 372
			NspiGetHierarchyInfoTaskTitle = 4086077102U,
			// Token: 0x04000175 RID: 373
			InputPasswordRequired = 375864490U,
			// Token: 0x04000176 RID: 374
			NspiGetNetworkAddressesPropertyTaskTitle = 3350259510U,
			// Token: 0x04000177 RID: 375
			VerifyRpcProxyTaskTitle = 3206764939U,
			// Token: 0x04000178 RID: 376
			WrongAuthForPersonalizedServer = 1328936299U,
			// Token: 0x04000179 RID: 377
			EmsmdbTaskDescription = 2224461727U,
			// Token: 0x0400017A RID: 378
			WrongDefinitionType = 1666890870U,
			// Token: 0x0400017B RID: 379
			RfriGetFqdnTaskTitle = 4202007121U,
			// Token: 0x0400017C RID: 380
			SecondaryEndpoint = 972661725U,
			// Token: 0x0400017D RID: 381
			NspiGetMatchesTaskDescription = 3707932024U,
			// Token: 0x0400017E RID: 382
			EmsmdbConnectTaskDescription = 3047902021U,
			// Token: 0x0400017F RID: 383
			RfriGetNewDsaTaskDescription = 3157018328U,
			// Token: 0x04000180 RID: 384
			DummyTaskTitle = 1880067925U,
			// Token: 0x04000181 RID: 385
			NspiQueryRowsTaskTitle = 2936865088U,
			// Token: 0x04000182 RID: 386
			ExtensionAttributes = 3754643782U,
			// Token: 0x04000183 RID: 387
			DummyTaskDescription = 3942413427U,
			// Token: 0x04000184 RID: 388
			InputPasswordNotRequired = 1195865221U,
			// Token: 0x04000185 RID: 389
			NspiGetNetworkAddressesTaskDescription = 4271094603U,
			// Token: 0x04000186 RID: 390
			NspiGetHierarchyInfoTaskDescription = 3049602642U,
			// Token: 0x04000187 RID: 391
			Endpoint = 1196155619U,
			// Token: 0x04000188 RID: 392
			NspiQueryHomeMDBTaskTitle = 613263819U,
			// Token: 0x04000189 RID: 393
			RfriGetFqdnTaskDescription = 1958074863U,
			// Token: 0x0400018A RID: 394
			DiscoverWebProxyTaskTitle = 52542312U,
			// Token: 0x0400018B RID: 395
			RfriGetNewDsaTaskTitle = 3709524604U,
			// Token: 0x0400018C RID: 396
			OutlookTaskTitle = 547095242U,
			// Token: 0x0400018D RID: 397
			ScomAlertLoggerTaskPropertyNullValue = 4266394152U,
			// Token: 0x0400018E RID: 398
			RetryTaskDescription = 611708359U,
			// Token: 0x0400018F RID: 399
			EmsmdbLogonTaskTitle = 433052918U,
			// Token: 0x04000190 RID: 400
			RcaOutlookTaskTitle = 342672400U,
			// Token: 0x04000191 RID: 401
			MonitoringAccount = 1152705677U,
			// Token: 0x04000192 RID: 402
			PFEmsmdbTaskDescription = 1587959117U,
			// Token: 0x04000193 RID: 403
			ScomAlertLoggerTaskCompletedProperties = 3982499041U,
			// Token: 0x04000194 RID: 404
			EmsmdbLogonTaskDescription = 3321608102U,
			// Token: 0x04000195 RID: 405
			EmsmdbConnectTaskTitle = 2896583015U,
			// Token: 0x04000196 RID: 406
			PFEmsmdbTaskTitle = 3911446795U,
			// Token: 0x04000197 RID: 407
			MonitoringAccountPassword = 3508157170U,
			// Token: 0x04000198 RID: 408
			PFEmsmdbLogonTaskDescription = 1342291712U,
			// Token: 0x04000199 RID: 409
			RcaOutlookTaskDescription = 620115544U,
			// Token: 0x0400019A RID: 410
			NspiGetNetworkAddressesTaskTitle = 2112045541U,
			// Token: 0x0400019B RID: 411
			NspiUnbindTaskTitle = 46727295U,
			// Token: 0x0400019C RID: 412
			TaskExceptionMessage = 2723049729U,
			// Token: 0x0400019D RID: 413
			NspiQueryRowsTaskDescription = 1442961372U,
			// Token: 0x0400019E RID: 414
			NspiUnbindTaskDescription = 4002896033U,
			// Token: 0x0400019F RID: 415
			NspiDNToEphTaskDescription = 33448767U,
			// Token: 0x040001A0 RID: 416
			PFEmsmdbConnectTaskDescription = 43152595U,
			// Token: 0x040001A1 RID: 417
			RfriTaskDescription = 763528660U,
			// Token: 0x040001A2 RID: 418
			RetryTaskTitle = 1421893477U,
			// Token: 0x040001A3 RID: 419
			NspiBindTaskDescription = 1091351742U,
			// Token: 0x040001A4 RID: 420
			Identity = 4164811492U,
			// Token: 0x040001A5 RID: 421
			NspiDNToEphTaskTitle = 1994569285U,
			// Token: 0x040001A6 RID: 422
			AsyncTaskTitle = 3045810739U
		}

		// Token: 0x02000081 RID: 129
		private enum ParamIDs
		{
			// Token: 0x040001A8 RID: 424
			ScomAlertLoggerTaskFailed,
			// Token: 0x040001A9 RID: 425
			CompositeTaskTitle,
			// Token: 0x040001AA RID: 426
			RpcCallResultErrorCodeDescription,
			// Token: 0x040001AB RID: 427
			CompositeTaskDescription,
			// Token: 0x040001AC RID: 428
			NetworkCredentialString,
			// Token: 0x040001AD RID: 429
			ScomAlertLoggerTaskProperty,
			// Token: 0x040001AE RID: 430
			ScomAlertLoggerIndent,
			// Token: 0x040001AF RID: 431
			ScomAlertLoggerTaskSucceeded,
			// Token: 0x040001B0 RID: 432
			ScomAlertLoggerTaskStarted,
			// Token: 0x040001B1 RID: 433
			PropertyNotFoundExceptionMessage,
			// Token: 0x040001B2 RID: 434
			ScomAlertLoggerTaskDescription,
			// Token: 0x040001B3 RID: 435
			ListOfItems
		}
	}
}
