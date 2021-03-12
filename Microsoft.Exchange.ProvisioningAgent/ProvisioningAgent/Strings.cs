using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000061 RID: 97
	internal static class Strings
	{
		// Token: 0x0600027C RID: 636 RVA: 0x0000FEAC File Offset: 0x0000E0AC
		static Strings()
		{
			Strings.stringIDs.Add(3378717027U, "LdapFilterErrorInvalidBooleanValue");
			Strings.stringIDs.Add(1860494422U, "LdapFilterErrorNotSupportSingleComp");
			Strings.stringIDs.Add(1386447651U, "InvokedProvisionDefaultProperties");
			Strings.stringIDs.Add(4236118860U, "ProxyDLLError");
			Strings.stringIDs.Add(1091171211U, "ExitedValidate");
			Strings.stringIDs.Add(4230107429U, "LdapFilterErrorQueryTooLong");
			Strings.stringIDs.Add(1688256845U, "LdapFilterErrorBracketMismatch");
			Strings.stringIDs.Add(3060439136U, "ErrorUpdateAffectedIConfigurableBadRetObject");
			Strings.stringIDs.Add(1755960558U, "ErrorNoMailboxPlan");
			Strings.stringIDs.Add(449876541U, "ProxyDLLDiskSpace");
			Strings.stringIDs.Add(56024811U, "LdapFilterErrorInvalidToken");
			Strings.stringIDs.Add(1880382981U, "ProxyDLLContention");
			Strings.stringIDs.Add(2164854509U, "ProxyDLLSyntax");
			Strings.stringIDs.Add(2800661133U, "EnteredOnComplete");
			Strings.stringIDs.Add(4152138657U, "InvokedUpdateAffectedIConfigurable");
			Strings.stringIDs.Add(1989031583U, "ProxyNotImplemented");
			Strings.stringIDs.Add(3580054472U, "RunningAsSystem");
			Strings.stringIDs.Add(3269299477U, "ProxyDLLOOM");
			Strings.stringIDs.Add(3410851087U, "ProxyDLLNotFound");
			Strings.stringIDs.Add(2781326009U, "ErrorNoDefaultMailboxPlan");
			Strings.stringIDs.Add(3226694139U, "ExitedInitializeConfig");
			Strings.stringIDs.Add(1998083787U, "EnteredInitializeConfig");
			Strings.stringIDs.Add(1775082576U, "InvokedValidate");
			Strings.stringIDs.Add(1859635364U, "ErrorUnexpectedReturnTypeInValidate");
			Strings.stringIDs.Add(2735291928U, "ProxyDLLConfig");
			Strings.stringIDs.Add(3936814429U, "LdapFilterErrorAnrIsNotSupported");
			Strings.stringIDs.Add(3897335713U, "ExitedOnComplete");
			Strings.stringIDs.Add(3269299668U, "ProxyDLLEOF");
			Strings.stringIDs.Add(1703828310U, "ProxyDLLProtocol");
			Strings.stringIDs.Add(4045631128U, "LdapFilterErrorInvalidGtLtOperand");
			Strings.stringIDs.Add(2520898135U, "WarningNoDefaultMailboxPlan");
			Strings.stringIDs.Add(1236062515U, "EnteredValidate");
			Strings.stringIDs.Add(1767506191U, "VerboseZeroProvisioningPolicyFound");
			Strings.stringIDs.Add(1005127777U, "NoError");
			Strings.stringIDs.Add(3641189505U, "WarningNoDefaultMailboxPlanUsingNonDefault");
			Strings.stringIDs.Add(367606263U, "ProxyDLLDefault");
			Strings.stringIDs.Add(156106839U, "ProxyDLLSoftware");
			Strings.stringIDs.Add(135248896U, "LdapFilterErrorInvalidBitwiseOperand");
			Strings.stringIDs.Add(531667494U, "FailedToEnableEwsMailboxLogger");
			Strings.stringIDs.Add(2753705114U, "ErrorTooManyDefaultMailboxPlans");
			Strings.stringIDs.Add(3355805493U, "ErrorLoadBalancingFailedToFindDatabase");
			Strings.stringIDs.Add(2370329957U, "NotRunningAsSystem");
			Strings.stringIDs.Add(1324265457U, "LdapFilterErrorInvalidWildCardGtLt");
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600027D RID: 637 RVA: 0x00010244 File Offset: 0x0000E444
		public static LocalizedString LdapFilterErrorInvalidBooleanValue
		{
			get
			{
				return new LocalizedString("LdapFilterErrorInvalidBooleanValue", "ExF79F20", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00010262 File Offset: 0x0000E462
		public static LocalizedString LdapFilterErrorNotSupportSingleComp
		{
			get
			{
				return new LocalizedString("LdapFilterErrorNotSupportSingleComp", "ExEDC679", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00010280 File Offset: 0x0000E480
		public static LocalizedString ErrorProxyDllNotFound(string dll)
		{
			return new LocalizedString("ErrorProxyDllNotFound", "ExE0C9D5", false, true, Strings.ResourceManager, new object[]
			{
				dll
			});
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000280 RID: 640 RVA: 0x000102AF File Offset: 0x0000E4AF
		public static LocalizedString InvokedProvisionDefaultProperties
		{
			get
			{
				return new LocalizedString("InvokedProvisionDefaultProperties", "Ex025540", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000281 RID: 641 RVA: 0x000102CD File Offset: 0x0000E4CD
		public static LocalizedString ProxyDLLError
		{
			get
			{
				return new LocalizedString("ProxyDLLError", "ExCE9170", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x000102EC File Offset: 0x0000E4EC
		public static LocalizedString VerboseAddSecondaryEmailAddress(string address)
		{
			return new LocalizedString("VerboseAddSecondaryEmailAddress", "Ex8CF8B9", false, true, Strings.ResourceManager, new object[]
			{
				address
			});
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0001031C File Offset: 0x0000E51C
		public static LocalizedString LdapFilterErrorUnsupportedAttrbuteType(string type)
		{
			return new LocalizedString("LdapFilterErrorUnsupportedAttrbuteType", "Ex375F26", false, true, Strings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0001034C File Offset: 0x0000E54C
		public static LocalizedString VerboseSetWindowsEmailAddress(string addres)
		{
			return new LocalizedString("VerboseSetWindowsEmailAddress", "ExA71AE7", false, true, Strings.ResourceManager, new object[]
			{
				addres
			});
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0001037C File Offset: 0x0000E57C
		public static LocalizedString VerboseAddAddressListMemberShip(string id)
		{
			return new LocalizedString("VerboseAddAddressListMemberShip", "Ex86C423", false, true, Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000286 RID: 646 RVA: 0x000103AB File Offset: 0x0000E5AB
		public static LocalizedString ExitedValidate
		{
			get
			{
				return new LocalizedString("ExitedValidate", "Ex7C45B4", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x000103CC File Offset: 0x0000E5CC
		public static LocalizedString VerboseNoDefaultForTask(string task)
		{
			return new LocalizedString("VerboseNoDefaultForTask", "Ex16E409", false, true, Strings.ResourceManager, new object[]
			{
				task
			});
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000103FC File Offset: 0x0000E5FC
		public static LocalizedString ErrorInvalidBaseAddress(string baseAddress)
		{
			return new LocalizedString("ErrorInvalidBaseAddress", "Ex7D174C", false, true, Strings.ResourceManager, new object[]
			{
				baseAddress
			});
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0001042C File Offset: 0x0000E62C
		public static LocalizedString ScriptReturned(string output)
		{
			return new LocalizedString("ScriptReturned", "Ex44DA9E", false, true, Strings.ResourceManager, new object[]
			{
				output
			});
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0001045C File Offset: 0x0000E65C
		public static LocalizedString XmlErrorMissingInnerText(string file)
		{
			return new LocalizedString("XmlErrorMissingInnerText", "Ex1C0E71", false, true, Strings.ResourceManager, new object[]
			{
				file
			});
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0001048C File Offset: 0x0000E68C
		public static LocalizedString ErrorProxyGenErrorEntryPoint(string module, string addressType, string methodName)
		{
			return new LocalizedString("ErrorProxyGenErrorEntryPoint", "Ex45A6FB", false, true, Strings.ResourceManager, new object[]
			{
				module,
				addressType,
				methodName
			});
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000104C4 File Offset: 0x0000E6C4
		public static LocalizedString ErrorInProvisionDefaultProperties(string inner)
		{
			return new LocalizedString("ErrorInProvisionDefaultProperties", "ExDF2BDD", false, true, Strings.ResourceManager, new object[]
			{
				inner
			});
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600028D RID: 653 RVA: 0x000104F3 File Offset: 0x0000E6F3
		public static LocalizedString LdapFilterErrorQueryTooLong
		{
			get
			{
				return new LocalizedString("LdapFilterErrorQueryTooLong", "Ex7DB526", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00010514 File Offset: 0x0000E714
		public static LocalizedString VerboseEmailAddressPoliciesForOganizationFromDC(string org, string dc)
		{
			return new LocalizedString("VerboseEmailAddressPoliciesForOganizationFromDC", "Ex2E6460", false, true, Strings.ResourceManager, new object[]
			{
				org,
				dc
			});
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00010548 File Offset: 0x0000E748
		public static LocalizedString VerboseProvisionDefaultProperties(string org)
		{
			return new LocalizedString("VerboseProvisionDefaultProperties", "ExBB2146", false, true, Strings.ResourceManager, new object[]
			{
				org
			});
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00010577 File Offset: 0x0000E777
		public static LocalizedString LdapFilterErrorBracketMismatch
		{
			get
			{
				return new LocalizedString("LdapFilterErrorBracketMismatch", "Ex78886A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00010598 File Offset: 0x0000E798
		public static LocalizedString VerboseSettingDisplayName(string name)
		{
			return new LocalizedString("VerboseSettingDisplayName", "Ex216486", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000292 RID: 658 RVA: 0x000105C7 File Offset: 0x0000E7C7
		public static LocalizedString ErrorUpdateAffectedIConfigurableBadRetObject
		{
			get
			{
				return new LocalizedString("ErrorUpdateAffectedIConfigurableBadRetObject", "Ex666611", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000293 RID: 659 RVA: 0x000105E5 File Offset: 0x0000E7E5
		public static LocalizedString ErrorNoMailboxPlan
		{
			get
			{
				return new LocalizedString("ErrorNoMailboxPlan", "ExFAD707", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00010603 File Offset: 0x0000E803
		public static LocalizedString ProxyDLLDiskSpace
		{
			get
			{
				return new LocalizedString("ProxyDLLDiskSpace", "ExB64B84", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00010621 File Offset: 0x0000E821
		public static LocalizedString LdapFilterErrorInvalidToken
		{
			get
			{
				return new LocalizedString("LdapFilterErrorInvalidToken", "Ex9FB888", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0001063F File Offset: 0x0000E83F
		public static LocalizedString ProxyDLLContention
		{
			get
			{
				return new LocalizedString("ProxyDLLContention", "ExEABD38", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00010660 File Offset: 0x0000E860
		public static LocalizedString LdapFilterErrorNoAttributeType(string filter)
		{
			return new LocalizedString("LdapFilterErrorNoAttributeType", "Ex4E7238", false, true, Strings.ResourceManager, new object[]
			{
				filter
			});
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0001068F File Offset: 0x0000E88F
		public static LocalizedString ProxyDLLSyntax
		{
			get
			{
				return new LocalizedString("ProxyDLLSyntax", "ExD0AF2A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000299 RID: 665 RVA: 0x000106AD File Offset: 0x0000E8AD
		public static LocalizedString EnteredOnComplete
		{
			get
			{
				return new LocalizedString("EnteredOnComplete", "ExC8CC66", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600029A RID: 666 RVA: 0x000106CB File Offset: 0x0000E8CB
		public static LocalizedString InvokedUpdateAffectedIConfigurable
		{
			get
			{
				return new LocalizedString("InvokedUpdateAffectedIConfigurable", "ExCC1F76", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600029B RID: 667 RVA: 0x000106E9 File Offset: 0x0000E8E9
		public static LocalizedString ProxyNotImplemented
		{
			get
			{
				return new LocalizedString("ProxyNotImplemented", "ExF3E01F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00010708 File Offset: 0x0000E908
		public static LocalizedString ErrorInvalidLdapFilter(string reason)
		{
			return new LocalizedString("ErrorInvalidLdapFilter", "Ex84358D", false, true, Strings.ResourceManager, new object[]
			{
				reason
			});
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00010737 File Offset: 0x0000E937
		public static LocalizedString RunningAsSystem
		{
			get
			{
				return new LocalizedString("RunningAsSystem", "ExC0B449", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00010758 File Offset: 0x0000E958
		public static LocalizedString InvokedOnComplete(string first, string second)
		{
			return new LocalizedString("InvokedOnComplete", "Ex817DB0", false, true, Strings.ResourceManager, new object[]
			{
				first,
				second
			});
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0001078C File Offset: 0x0000E98C
		public static LocalizedString MultiTemplatePolicyFound(string tag)
		{
			return new LocalizedString("MultiTemplatePolicyFound", "ExEFE4F4", false, true, Strings.ResourceManager, new object[]
			{
				tag
			});
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x000107BC File Offset: 0x0000E9BC
		public static LocalizedString LdapFilterErrorNoAttributeValue(string filter)
		{
			return new LocalizedString("LdapFilterErrorNoAttributeValue", "ExFDFEA3", false, true, Strings.ResourceManager, new object[]
			{
				filter
			});
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x000107EB File Offset: 0x0000E9EB
		public static LocalizedString ProxyDLLOOM
		{
			get
			{
				return new LocalizedString("ProxyDLLOOM", "Ex024420", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00010809 File Offset: 0x0000EA09
		public static LocalizedString ProxyDLLNotFound
		{
			get
			{
				return new LocalizedString("ProxyDLLNotFound", "Ex3E8843", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00010828 File Offset: 0x0000EA28
		public static LocalizedString ErrorFailedToGenerateUniqueProxy(string baseAddress, string recipient)
		{
			return new LocalizedString("ErrorFailedToGenerateUniqueProxy", "Ex1D6500", false, true, Strings.ResourceManager, new object[]
			{
				baseAddress,
				recipient
			});
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0001085C File Offset: 0x0000EA5C
		public static LocalizedString ErrorInValidate(string inner)
		{
			return new LocalizedString("ErrorInValidate", "ExA96987", false, true, Strings.ResourceManager, new object[]
			{
				inner
			});
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0001088B File Offset: 0x0000EA8B
		public static LocalizedString ErrorNoDefaultMailboxPlan
		{
			get
			{
				return new LocalizedString("ErrorNoDefaultMailboxPlan", "Ex76EB50", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x000108A9 File Offset: 0x0000EAA9
		public static LocalizedString ExitedInitializeConfig
		{
			get
			{
				return new LocalizedString("ExitedInitializeConfig", "Ex46F1E9", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000108C8 File Offset: 0x0000EAC8
		public static LocalizedString ErrorFailedToFindAddressTypeObject(string type)
		{
			return new LocalizedString("ErrorFailedToFindAddressTypeObject", "Ex0553B4", false, true, Strings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x000108F8 File Offset: 0x0000EAF8
		public static LocalizedString VerboseTaskUseTypeAsDefault(string task, string type)
		{
			return new LocalizedString("VerboseTaskUseTypeAsDefault", "Ex637177", false, true, Strings.ResourceManager, new object[]
			{
				task,
				type
			});
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0001092C File Offset: 0x0000EB2C
		public static LocalizedString VerboseSettingLegacyExchangeDN(string legacyDN)
		{
			return new LocalizedString("VerboseSettingLegacyExchangeDN", "Ex81C24E", false, true, Strings.ResourceManager, new object[]
			{
				legacyDN
			});
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0001095C File Offset: 0x0000EB5C
		public static LocalizedString ErrorProvisioningPolicyCorrupt(string identity)
		{
			return new LocalizedString("ErrorProvisioningPolicyCorrupt", "Ex4B23DC", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0001098C File Offset: 0x0000EB8C
		public static LocalizedString VerbosePolicyProviderUseDomainController(string dc)
		{
			return new LocalizedString("VerbosePolicyProviderUseDomainController", "ExC1187D", false, true, Strings.ResourceManager, new object[]
			{
				dc
			});
		}

		// Token: 0x060002AC RID: 684 RVA: 0x000109BC File Offset: 0x0000EBBC
		public static LocalizedString ErrorFailedToReadRegistryKey(string key, string msg)
		{
			return new LocalizedString("ErrorFailedToReadRegistryKey", "ExA70D00", false, true, Strings.ResourceManager, new object[]
			{
				key,
				msg
			});
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002AD RID: 685 RVA: 0x000109EF File Offset: 0x0000EBEF
		public static LocalizedString EnteredInitializeConfig
		{
			get
			{
				return new LocalizedString("EnteredInitializeConfig", "ExC318A7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00010A10 File Offset: 0x0000EC10
		public static LocalizedString LdapFilterErrorInvalidDecimal(string value)
		{
			return new LocalizedString("LdapFilterErrorInvalidDecimal", "Ex16EF43", false, true, Strings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00010A3F File Offset: 0x0000EC3F
		public static LocalizedString InvokedValidate
		{
			get
			{
				return new LocalizedString("InvokedValidate", "ExA995B0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00010A60 File Offset: 0x0000EC60
		public static LocalizedString LdapFilterErrorObjectCategoryNotFound(string name)
		{
			return new LocalizedString("LdapFilterErrorObjectCategoryNotFound", "Ex4B0417", false, true, Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00010A8F File Offset: 0x0000EC8F
		public static LocalizedString ErrorUnexpectedReturnTypeInValidate
		{
			get
			{
				return new LocalizedString("ErrorUnexpectedReturnTypeInValidate", "ExB328F1", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x00010AAD File Offset: 0x0000ECAD
		public static LocalizedString ProxyDLLConfig
		{
			get
			{
				return new LocalizedString("ProxyDLLConfig", "Ex381B58", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00010ACB File Offset: 0x0000ECCB
		public static LocalizedString LdapFilterErrorAnrIsNotSupported
		{
			get
			{
				return new LocalizedString("LdapFilterErrorAnrIsNotSupported", "Ex476BF1", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00010AEC File Offset: 0x0000ECEC
		public static LocalizedString ErrorInUpdateAffectedIConfigurable(string inner)
		{
			return new LocalizedString("ErrorInUpdateAffectedIConfigurable", "Ex052563", false, true, Strings.ResourceManager, new object[]
			{
				inner
			});
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00010B1B File Offset: 0x0000ED1B
		public static LocalizedString ExitedOnComplete
		{
			get
			{
				return new LocalizedString("ExitedOnComplete", "ExCB425E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00010B3C File Offset: 0x0000ED3C
		public static LocalizedString VerboseNoNeedToValidate(string type)
		{
			return new LocalizedString("VerboseNoNeedToValidate", "ExA207E6", false, true, Strings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00010B6B File Offset: 0x0000ED6B
		public static LocalizedString ProxyDLLEOF
		{
			get
			{
				return new LocalizedString("ProxyDLLEOF", "Ex7CEC81", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00010B8C File Offset: 0x0000ED8C
		public static LocalizedString LdapFilterErrorSpaceMiddleType(string attributeType)
		{
			return new LocalizedString("LdapFilterErrorSpaceMiddleType", "Ex978866", false, true, Strings.ResourceManager, new object[]
			{
				attributeType
			});
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00010BBC File Offset: 0x0000EDBC
		public static LocalizedString FailedToFindEwsEndpoint(string mailbox)
		{
			return new LocalizedString("FailedToFindEwsEndpoint", "ExE1F5ED", false, true, Strings.ResourceManager, new object[]
			{
				mailbox
			});
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002BA RID: 698 RVA: 0x00010BEB File Offset: 0x0000EDEB
		public static LocalizedString ProxyDLLProtocol
		{
			get
			{
				return new LocalizedString("ProxyDLLProtocol", "Ex7D557D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00010C0C File Offset: 0x0000EE0C
		public static LocalizedString LdapFilterErrorInvalidWildCard(string type)
		{
			return new LocalizedString("LdapFilterErrorInvalidWildCard", "Ex6F59E7", false, true, Strings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00010C3C File Offset: 0x0000EE3C
		public static LocalizedString VerboseFoundAddressList(string id)
		{
			return new LocalizedString("VerboseFoundAddressList", "Ex8817D1", false, true, Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00010C6C File Offset: 0x0000EE6C
		public static LocalizedString LdapFilterErrorUnsupportedOperand(string op)
		{
			return new LocalizedString("LdapFilterErrorUnsupportedOperand", "Ex883BC4", false, true, Strings.ResourceManager, new object[]
			{
				op
			});
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00010C9C File Offset: 0x0000EE9C
		public static LocalizedString ErrorProxyGeneration(string message)
		{
			return new LocalizedString("ErrorProxyGeneration", "ExCE3FA1", false, true, Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00010CCC File Offset: 0x0000EECC
		public static LocalizedString XmlErrorWrongAPI(string file, string api)
		{
			return new LocalizedString("XmlErrorWrongAPI", "Ex76549C", false, true, Strings.ResourceManager, new object[]
			{
				file,
				api
			});
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00010D00 File Offset: 0x0000EF00
		public static LocalizedString VerboseUpdateRecipientObject(string id, string cdc, string dc, string gc)
		{
			return new LocalizedString("VerboseUpdateRecipientObject", "Ex32E49A", false, true, Strings.ResourceManager, new object[]
			{
				id,
				cdc,
				dc,
				gc
			});
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00010D3B File Offset: 0x0000EF3B
		public static LocalizedString LdapFilterErrorInvalidGtLtOperand
		{
			get
			{
				return new LocalizedString("LdapFilterErrorInvalidGtLtOperand", "Ex9A634A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00010D59 File Offset: 0x0000EF59
		public static LocalizedString WarningNoDefaultMailboxPlan
		{
			get
			{
				return new LocalizedString("WarningNoDefaultMailboxPlan", "ExCD6408", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00010D78 File Offset: 0x0000EF78
		public static LocalizedString FailedToCreateAdminAuditLogFolder(string responseclass, string code, string error)
		{
			return new LocalizedString("FailedToCreateAdminAuditLogFolder", "", false, false, Strings.ResourceManager, new object[]
			{
				responseclass,
				code,
				error
			});
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00010DB0 File Offset: 0x0000EFB0
		public static LocalizedString AdminAuditLogFolderNotFound(string reason)
		{
			return new LocalizedString("AdminAuditLogFolderNotFound", "Ex913D13", false, true, Strings.ResourceManager, new object[]
			{
				reason
			});
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00010DE0 File Offset: 0x0000EFE0
		public static LocalizedString XmlErrorMissingNode(string node, string file)
		{
			return new LocalizedString("XmlErrorMissingNode", "ExE024CE", false, true, Strings.ResourceManager, new object[]
			{
				node,
				file
			});
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00010E13 File Offset: 0x0000F013
		public static LocalizedString EnteredValidate
		{
			get
			{
				return new LocalizedString("EnteredValidate", "ExC593C0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00010E31 File Offset: 0x0000F031
		public static LocalizedString VerboseZeroProvisioningPolicyFound
		{
			get
			{
				return new LocalizedString("VerboseZeroProvisioningPolicyFound", "ExE1585A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00010E4F File Offset: 0x0000F04F
		public static LocalizedString NoError
		{
			get
			{
				return new LocalizedString("NoError", "Ex2025DD", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00010E70 File Offset: 0x0000F070
		public static LocalizedString ExecutingScript(string script)
		{
			return new LocalizedString("ExecutingScript", "ExAAA631", false, true, Strings.ResourceManager, new object[]
			{
				script
			});
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00010EA0 File Offset: 0x0000F0A0
		public static LocalizedString LdapFilterErrorTypeOnlySpaces(string filter)
		{
			return new LocalizedString("LdapFilterErrorTypeOnlySpaces", "Ex2B210D", false, true, Strings.ResourceManager, new object[]
			{
				filter
			});
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00010ED0 File Offset: 0x0000F0D0
		public static LocalizedString VerboseToBeValidateObject(string id, string type)
		{
			return new LocalizedString("VerboseToBeValidateObject", "Ex2A1953", false, true, Strings.ResourceManager, new object[]
			{
				id,
				type
			});
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00010F03 File Offset: 0x0000F103
		public static LocalizedString WarningNoDefaultMailboxPlanUsingNonDefault
		{
			get
			{
				return new LocalizedString("WarningNoDefaultMailboxPlanUsingNonDefault", "Ex98C64F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00010F21 File Offset: 0x0000F121
		public static LocalizedString ProxyDLLDefault
		{
			get
			{
				return new LocalizedString("ProxyDLLDefault", "Ex572958", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00010F40 File Offset: 0x0000F140
		public static LocalizedString ErrorAddressListInvalidLdapFilter(string filter, string id, string message)
		{
			return new LocalizedString("ErrorAddressListInvalidLdapFilter", "Ex87D3D4", false, true, Strings.ResourceManager, new object[]
			{
				filter,
				id,
				message
			});
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00010F78 File Offset: 0x0000F178
		public static LocalizedString VerboseSettingHomeMTA(string id)
		{
			return new LocalizedString("VerboseSettingHomeMTA", "Ex4AC437", false, true, Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00010FA7 File Offset: 0x0000F1A7
		public static LocalizedString ProxyDLLSoftware
		{
			get
			{
				return new LocalizedString("ProxyDLLSoftware", "ExAA9657", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00010FC5 File Offset: 0x0000F1C5
		public static LocalizedString LdapFilterErrorInvalidBitwiseOperand
		{
			get
			{
				return new LocalizedString("LdapFilterErrorInvalidBitwiseOperand", "Ex3C76E0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00010FE4 File Offset: 0x0000F1E4
		public static LocalizedString VerboseRemoveAddressListMemberShip(string id)
		{
			return new LocalizedString("VerboseRemoveAddressListMemberShip", "Ex10A84F", false, true, Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00011014 File Offset: 0x0000F214
		public static LocalizedString VerboseMakeEmailAddressToPrimary(string address)
		{
			return new LocalizedString("VerboseMakeEmailAddressToPrimary", "ExC1589E", false, true, Strings.ResourceManager, new object[]
			{
				address
			});
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00011044 File Offset: 0x0000F244
		public static LocalizedString LdapFilterErrorUndefinedAttributeType(string type)
		{
			return new LocalizedString("LdapFilterErrorUndefinedAttributeType", "ExB8987F", false, true, Strings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00011074 File Offset: 0x0000F274
		public static LocalizedString VerboseSkipRemoteForestEmailAddressUpdate(string id, string forestName)
		{
			return new LocalizedString("VerboseSkipRemoteForestEmailAddressUpdate", "ExD038DE", false, true, Strings.ResourceManager, new object[]
			{
				id,
				forestName
			});
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x000110A8 File Offset: 0x0000F2A8
		public static LocalizedString ErrorFailedToValidBaseAddress(string baseAddress, string msg)
		{
			return new LocalizedString("ErrorFailedToValidBaseAddress", "ExD050D0", false, true, Strings.ResourceManager, new object[]
			{
				baseAddress,
				msg
			});
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x000110DB File Offset: 0x0000F2DB
		public static LocalizedString FailedToEnableEwsMailboxLogger
		{
			get
			{
				return new LocalizedString("FailedToEnableEwsMailboxLogger", "ExC7850C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x000110FC File Offset: 0x0000F2FC
		public static LocalizedString VerboseAddPrimaryEmailAddress(string address)
		{
			return new LocalizedString("VerboseAddPrimaryEmailAddress", "Ex8C9BE8", false, true, Strings.ResourceManager, new object[]
			{
				address
			});
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0001112C File Offset: 0x0000F32C
		public static LocalizedString VerboseHandlingProvisioningPolicyFound(string id)
		{
			return new LocalizedString("VerboseHandlingProvisioningPolicyFound", "ExFBB9B9", false, true, Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0001115C File Offset: 0x0000F35C
		public static LocalizedString ErrorFileIsNotFound(string file)
		{
			return new LocalizedString("ErrorFileIsNotFound", "ExAFB7E9", false, true, Strings.ResourceManager, new object[]
			{
				file
			});
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0001118C File Offset: 0x0000F38C
		public static LocalizedString VerboseSettingExchangeGuid(string id)
		{
			return new LocalizedString("VerboseSettingExchangeGuid", "ExA89C29", false, true, Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x060002DC RID: 732 RVA: 0x000111BC File Offset: 0x0000F3BC
		public static LocalizedString VerboseEmailAddressPolicy(string id)
		{
			return new LocalizedString("VerboseEmailAddressPolicy", "Ex907BFF", false, true, Strings.ResourceManager, new object[]
			{
				id
			});
		}

		// Token: 0x060002DD RID: 733 RVA: 0x000111EC File Offset: 0x0000F3EC
		public static LocalizedString VerboseAddressListsForOganizationFromDC(string org, string dc)
		{
			return new LocalizedString("VerboseAddressListsForOganizationFromDC", "ExF0FA60", false, true, Strings.ResourceManager, new object[]
			{
				org,
				dc
			});
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00011220 File Offset: 0x0000F420
		public static LocalizedString LdapFilterErrorEscCharWithoutEscapable(string value)
		{
			return new LocalizedString("LdapFilterErrorEscCharWithoutEscapable", "ExE67B50", false, true, Strings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00011250 File Offset: 0x0000F450
		public static LocalizedString ScriptingAgentInitializationFailed(string msg)
		{
			return new LocalizedString("ScriptingAgentInitializationFailed", "Ex19AD41", false, true, Strings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00011280 File Offset: 0x0000F480
		public static LocalizedString VerboseWorkingOrganizationForPolicy(string org)
		{
			return new LocalizedString("VerboseWorkingOrganizationForPolicy", "ExE5D77C", false, true, Strings.ResourceManager, new object[]
			{
				org
			});
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000112B0 File Offset: 0x0000F4B0
		public static LocalizedString LdapFilterErrorInvalidEscaping(string value)
		{
			return new LocalizedString("LdapFilterErrorInvalidEscaping", "Ex4F5C08", false, true, Strings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x000112E0 File Offset: 0x0000F4E0
		public static LocalizedString FailedToCreateAdminAuditLogItem(string responseclass, string code, string error)
		{
			return new LocalizedString("FailedToCreateAdminAuditLogItem", "", false, false, Strings.ResourceManager, new object[]
			{
				responseclass,
				code,
				error
			});
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00011318 File Offset: 0x0000F518
		public static LocalizedString ErrorProxyLoadDll(string module, string addressType, string message)
		{
			return new LocalizedString("ErrorProxyLoadDll", "Ex8BD879", false, true, Strings.ResourceManager, new object[]
			{
				module,
				addressType,
				message
			});
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0001134F File Offset: 0x0000F54F
		public static LocalizedString ErrorTooManyDefaultMailboxPlans
		{
			get
			{
				return new LocalizedString("ErrorTooManyDefaultMailboxPlans", "Ex0E03C9", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00011370 File Offset: 0x0000F570
		public static LocalizedString MultipleAdminAuditLogConfig(string organization)
		{
			return new LocalizedString("MultipleAdminAuditLogConfig", "Ex7B3DFB", false, true, Strings.ResourceManager, new object[]
			{
				organization
			});
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000113A0 File Offset: 0x0000F5A0
		public static LocalizedString XmlErrorMissingAttribute(string attrib, string file)
		{
			return new LocalizedString("XmlErrorMissingAttribute", "ExE31801", false, true, Strings.ResourceManager, new object[]
			{
				attrib,
				file
			});
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x000113D4 File Offset: 0x0000F5D4
		public static LocalizedString ErrorsDuringAdminLogProvisioningHandlerValidate(string error)
		{
			return new LocalizedString("ErrorsDuringAdminLogProvisioningHandlerValidate", "Ex2796D3", false, true, Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00011404 File Offset: 0x0000F604
		public static LocalizedString LdapFilterErrorValueOnlySpaces(string filter)
		{
			return new LocalizedString("LdapFilterErrorValueOnlySpaces", "Ex0E8168", false, true, Strings.ResourceManager, new object[]
			{
				filter
			});
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00011434 File Offset: 0x0000F634
		public static LocalizedString ErrorInOnComplete(string inner)
		{
			return new LocalizedString("ErrorInOnComplete", "Ex0398C5", false, true, Strings.ResourceManager, new object[]
			{
				inner
			});
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00011463 File Offset: 0x0000F663
		public static LocalizedString ErrorLoadBalancingFailedToFindDatabase
		{
			get
			{
				return new LocalizedString("ErrorLoadBalancingFailedToFindDatabase", "Ex02FD47", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00011484 File Offset: 0x0000F684
		public static LocalizedString LdapFilterErrorNoValidComparison(string filter)
		{
			return new LocalizedString("LdapFilterErrorNoValidComparison", "Ex056AEF", false, true, Strings.ResourceManager, new object[]
			{
				filter
			});
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002EC RID: 748 RVA: 0x000114B3 File Offset: 0x0000F6B3
		public static LocalizedString NotRunningAsSystem
		{
			get
			{
				return new LocalizedString("NotRunningAsSystem", "Ex350B38", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002ED RID: 749 RVA: 0x000114D1 File Offset: 0x0000F6D1
		public static LocalizedString LdapFilterErrorInvalidWildCardGtLt
		{
			get
			{
				return new LocalizedString("LdapFilterErrorInvalidWildCardGtLt", "ExCDC904", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x000114EF File Offset: 0x0000F6EF
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000146 RID: 326
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(43);

		// Token: 0x04000147 RID: 327
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.ProvisioningAgent.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000062 RID: 98
		public enum IDs : uint
		{
			// Token: 0x04000149 RID: 329
			LdapFilterErrorInvalidBooleanValue = 3378717027U,
			// Token: 0x0400014A RID: 330
			LdapFilterErrorNotSupportSingleComp = 1860494422U,
			// Token: 0x0400014B RID: 331
			InvokedProvisionDefaultProperties = 1386447651U,
			// Token: 0x0400014C RID: 332
			ProxyDLLError = 4236118860U,
			// Token: 0x0400014D RID: 333
			ExitedValidate = 1091171211U,
			// Token: 0x0400014E RID: 334
			LdapFilterErrorQueryTooLong = 4230107429U,
			// Token: 0x0400014F RID: 335
			LdapFilterErrorBracketMismatch = 1688256845U,
			// Token: 0x04000150 RID: 336
			ErrorUpdateAffectedIConfigurableBadRetObject = 3060439136U,
			// Token: 0x04000151 RID: 337
			ErrorNoMailboxPlan = 1755960558U,
			// Token: 0x04000152 RID: 338
			ProxyDLLDiskSpace = 449876541U,
			// Token: 0x04000153 RID: 339
			LdapFilterErrorInvalidToken = 56024811U,
			// Token: 0x04000154 RID: 340
			ProxyDLLContention = 1880382981U,
			// Token: 0x04000155 RID: 341
			ProxyDLLSyntax = 2164854509U,
			// Token: 0x04000156 RID: 342
			EnteredOnComplete = 2800661133U,
			// Token: 0x04000157 RID: 343
			InvokedUpdateAffectedIConfigurable = 4152138657U,
			// Token: 0x04000158 RID: 344
			ProxyNotImplemented = 1989031583U,
			// Token: 0x04000159 RID: 345
			RunningAsSystem = 3580054472U,
			// Token: 0x0400015A RID: 346
			ProxyDLLOOM = 3269299477U,
			// Token: 0x0400015B RID: 347
			ProxyDLLNotFound = 3410851087U,
			// Token: 0x0400015C RID: 348
			ErrorNoDefaultMailboxPlan = 2781326009U,
			// Token: 0x0400015D RID: 349
			ExitedInitializeConfig = 3226694139U,
			// Token: 0x0400015E RID: 350
			EnteredInitializeConfig = 1998083787U,
			// Token: 0x0400015F RID: 351
			InvokedValidate = 1775082576U,
			// Token: 0x04000160 RID: 352
			ErrorUnexpectedReturnTypeInValidate = 1859635364U,
			// Token: 0x04000161 RID: 353
			ProxyDLLConfig = 2735291928U,
			// Token: 0x04000162 RID: 354
			LdapFilterErrorAnrIsNotSupported = 3936814429U,
			// Token: 0x04000163 RID: 355
			ExitedOnComplete = 3897335713U,
			// Token: 0x04000164 RID: 356
			ProxyDLLEOF = 3269299668U,
			// Token: 0x04000165 RID: 357
			ProxyDLLProtocol = 1703828310U,
			// Token: 0x04000166 RID: 358
			LdapFilterErrorInvalidGtLtOperand = 4045631128U,
			// Token: 0x04000167 RID: 359
			WarningNoDefaultMailboxPlan = 2520898135U,
			// Token: 0x04000168 RID: 360
			EnteredValidate = 1236062515U,
			// Token: 0x04000169 RID: 361
			VerboseZeroProvisioningPolicyFound = 1767506191U,
			// Token: 0x0400016A RID: 362
			NoError = 1005127777U,
			// Token: 0x0400016B RID: 363
			WarningNoDefaultMailboxPlanUsingNonDefault = 3641189505U,
			// Token: 0x0400016C RID: 364
			ProxyDLLDefault = 367606263U,
			// Token: 0x0400016D RID: 365
			ProxyDLLSoftware = 156106839U,
			// Token: 0x0400016E RID: 366
			LdapFilterErrorInvalidBitwiseOperand = 135248896U,
			// Token: 0x0400016F RID: 367
			FailedToEnableEwsMailboxLogger = 531667494U,
			// Token: 0x04000170 RID: 368
			ErrorTooManyDefaultMailboxPlans = 2753705114U,
			// Token: 0x04000171 RID: 369
			ErrorLoadBalancingFailedToFindDatabase = 3355805493U,
			// Token: 0x04000172 RID: 370
			NotRunningAsSystem = 2370329957U,
			// Token: 0x04000173 RID: 371
			LdapFilterErrorInvalidWildCardGtLt = 1324265457U
		}

		// Token: 0x02000063 RID: 99
		private enum ParamIDs
		{
			// Token: 0x04000175 RID: 373
			ErrorProxyDllNotFound,
			// Token: 0x04000176 RID: 374
			VerboseAddSecondaryEmailAddress,
			// Token: 0x04000177 RID: 375
			LdapFilterErrorUnsupportedAttrbuteType,
			// Token: 0x04000178 RID: 376
			VerboseSetWindowsEmailAddress,
			// Token: 0x04000179 RID: 377
			VerboseAddAddressListMemberShip,
			// Token: 0x0400017A RID: 378
			VerboseNoDefaultForTask,
			// Token: 0x0400017B RID: 379
			ErrorInvalidBaseAddress,
			// Token: 0x0400017C RID: 380
			ScriptReturned,
			// Token: 0x0400017D RID: 381
			XmlErrorMissingInnerText,
			// Token: 0x0400017E RID: 382
			ErrorProxyGenErrorEntryPoint,
			// Token: 0x0400017F RID: 383
			ErrorInProvisionDefaultProperties,
			// Token: 0x04000180 RID: 384
			VerboseEmailAddressPoliciesForOganizationFromDC,
			// Token: 0x04000181 RID: 385
			VerboseProvisionDefaultProperties,
			// Token: 0x04000182 RID: 386
			VerboseSettingDisplayName,
			// Token: 0x04000183 RID: 387
			LdapFilterErrorNoAttributeType,
			// Token: 0x04000184 RID: 388
			ErrorInvalidLdapFilter,
			// Token: 0x04000185 RID: 389
			InvokedOnComplete,
			// Token: 0x04000186 RID: 390
			MultiTemplatePolicyFound,
			// Token: 0x04000187 RID: 391
			LdapFilterErrorNoAttributeValue,
			// Token: 0x04000188 RID: 392
			ErrorFailedToGenerateUniqueProxy,
			// Token: 0x04000189 RID: 393
			ErrorInValidate,
			// Token: 0x0400018A RID: 394
			ErrorFailedToFindAddressTypeObject,
			// Token: 0x0400018B RID: 395
			VerboseTaskUseTypeAsDefault,
			// Token: 0x0400018C RID: 396
			VerboseSettingLegacyExchangeDN,
			// Token: 0x0400018D RID: 397
			ErrorProvisioningPolicyCorrupt,
			// Token: 0x0400018E RID: 398
			VerbosePolicyProviderUseDomainController,
			// Token: 0x0400018F RID: 399
			ErrorFailedToReadRegistryKey,
			// Token: 0x04000190 RID: 400
			LdapFilterErrorInvalidDecimal,
			// Token: 0x04000191 RID: 401
			LdapFilterErrorObjectCategoryNotFound,
			// Token: 0x04000192 RID: 402
			ErrorInUpdateAffectedIConfigurable,
			// Token: 0x04000193 RID: 403
			VerboseNoNeedToValidate,
			// Token: 0x04000194 RID: 404
			LdapFilterErrorSpaceMiddleType,
			// Token: 0x04000195 RID: 405
			FailedToFindEwsEndpoint,
			// Token: 0x04000196 RID: 406
			LdapFilterErrorInvalidWildCard,
			// Token: 0x04000197 RID: 407
			VerboseFoundAddressList,
			// Token: 0x04000198 RID: 408
			LdapFilterErrorUnsupportedOperand,
			// Token: 0x04000199 RID: 409
			ErrorProxyGeneration,
			// Token: 0x0400019A RID: 410
			XmlErrorWrongAPI,
			// Token: 0x0400019B RID: 411
			VerboseUpdateRecipientObject,
			// Token: 0x0400019C RID: 412
			FailedToCreateAdminAuditLogFolder,
			// Token: 0x0400019D RID: 413
			AdminAuditLogFolderNotFound,
			// Token: 0x0400019E RID: 414
			XmlErrorMissingNode,
			// Token: 0x0400019F RID: 415
			ExecutingScript,
			// Token: 0x040001A0 RID: 416
			LdapFilterErrorTypeOnlySpaces,
			// Token: 0x040001A1 RID: 417
			VerboseToBeValidateObject,
			// Token: 0x040001A2 RID: 418
			ErrorAddressListInvalidLdapFilter,
			// Token: 0x040001A3 RID: 419
			VerboseSettingHomeMTA,
			// Token: 0x040001A4 RID: 420
			VerboseRemoveAddressListMemberShip,
			// Token: 0x040001A5 RID: 421
			VerboseMakeEmailAddressToPrimary,
			// Token: 0x040001A6 RID: 422
			LdapFilterErrorUndefinedAttributeType,
			// Token: 0x040001A7 RID: 423
			VerboseSkipRemoteForestEmailAddressUpdate,
			// Token: 0x040001A8 RID: 424
			ErrorFailedToValidBaseAddress,
			// Token: 0x040001A9 RID: 425
			VerboseAddPrimaryEmailAddress,
			// Token: 0x040001AA RID: 426
			VerboseHandlingProvisioningPolicyFound,
			// Token: 0x040001AB RID: 427
			ErrorFileIsNotFound,
			// Token: 0x040001AC RID: 428
			VerboseSettingExchangeGuid,
			// Token: 0x040001AD RID: 429
			VerboseEmailAddressPolicy,
			// Token: 0x040001AE RID: 430
			VerboseAddressListsForOganizationFromDC,
			// Token: 0x040001AF RID: 431
			LdapFilterErrorEscCharWithoutEscapable,
			// Token: 0x040001B0 RID: 432
			ScriptingAgentInitializationFailed,
			// Token: 0x040001B1 RID: 433
			VerboseWorkingOrganizationForPolicy,
			// Token: 0x040001B2 RID: 434
			LdapFilterErrorInvalidEscaping,
			// Token: 0x040001B3 RID: 435
			FailedToCreateAdminAuditLogItem,
			// Token: 0x040001B4 RID: 436
			ErrorProxyLoadDll,
			// Token: 0x040001B5 RID: 437
			MultipleAdminAuditLogConfig,
			// Token: 0x040001B6 RID: 438
			XmlErrorMissingAttribute,
			// Token: 0x040001B7 RID: 439
			ErrorsDuringAdminLogProvisioningHandlerValidate,
			// Token: 0x040001B8 RID: 440
			LdapFilterErrorValueOnlySpaces,
			// Token: 0x040001B9 RID: 441
			ErrorInOnComplete,
			// Token: 0x040001BA RID: 442
			LdapFilterErrorNoValidComparison
		}
	}
}
