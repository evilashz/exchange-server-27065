using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Parser
{
	// Token: 0x0200000E RID: 14
	internal static class Strings
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00004778 File Offset: 0x00002978
		static Strings()
		{
			Strings.stringIDs.Add(1914581610U, "ParameterShouldStartWith");
			Strings.stringIDs.Add(1564022379U, "CurrentOperationNotSet");
			Strings.stringIDs.Add(2509045854U, "InstallRequiresRoles");
			Strings.stringIDs.Add(3247175721U, "MBXRoleIsInstalled");
			Strings.stringIDs.Add(2607811679U, "CASRoleIsInstalled");
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00004818 File Offset: 0x00002A18
		public static LocalizedString NotAValidFqdn(string fqdn)
		{
			return new LocalizedString("NotAValidFqdn", Strings.ResourceManager, new object[]
			{
				fqdn
			});
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00004840 File Offset: 0x00002A40
		public static LocalizedString UnknownParameter(string parameter)
		{
			return new LocalizedString("UnknownParameter", Strings.ResourceManager, new object[]
			{
				parameter
			});
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00004868 File Offset: 0x00002A68
		public static LocalizedString ParameterShouldStartWith
		{
			get
			{
				return new LocalizedString("ParameterShouldStartWith", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00004880 File Offset: 0x00002A80
		public static LocalizedString AnswerFileCouldNotBeOpened(string answerfile)
		{
			return new LocalizedString("AnswerFileCouldNotBeOpened", Strings.ResourceManager, new object[]
			{
				answerfile
			});
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000048A8 File Offset: 0x00002AA8
		public static LocalizedString NotInTheRange(string s)
		{
			return new LocalizedString("NotInTheRange", Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000048D0 File Offset: 0x00002AD0
		public static LocalizedString InvalidRole(string role)
		{
			return new LocalizedString("InvalidRole", Strings.ResourceManager, new object[]
			{
				role
			});
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000048F8 File Offset: 0x00002AF8
		public static LocalizedString InvalidEdbFilePath(string path)
		{
			return new LocalizedString("InvalidEdbFilePath", Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00004920 File Offset: 0x00002B20
		public static LocalizedString CurrentOperationNotSet
		{
			get
			{
				return new LocalizedString("CurrentOperationNotSet", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00004938 File Offset: 0x00002B38
		public static LocalizedString DirectoryNotExist(string dir)
		{
			return new LocalizedString("DirectoryNotExist", Strings.ResourceManager, new object[]
			{
				dir
			});
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00004960 File Offset: 0x00002B60
		public static LocalizedString AnswerFileSetupParameterEntry(string name, object value)
		{
			return new LocalizedString("AnswerFileSetupParameterEntry", Strings.ResourceManager, new object[]
			{
				name,
				value
			});
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000498C File Offset: 0x00002B8C
		public static LocalizedString NotValidIndustryType(string industry, string validValue)
		{
			return new LocalizedString("NotValidIndustryType", Strings.ResourceManager, new object[]
			{
				industry,
				validValue
			});
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000049B8 File Offset: 0x00002BB8
		public static LocalizedString ParameterNotValidForCurrentRoles(string parameter)
		{
			return new LocalizedString("ParameterNotValidForCurrentRoles", Strings.ResourceManager, new object[]
			{
				parameter
			});
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000049E0 File Offset: 0x00002BE0
		public static LocalizedString ParameterMustHaveValue(string parameter)
		{
			return new LocalizedString("ParameterMustHaveValue", Strings.ResourceManager, new object[]
			{
				parameter
			});
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00004A08 File Offset: 0x00002C08
		public static LocalizedString AnswerFileNameNotValid(string name)
		{
			return new LocalizedString("AnswerFileNameNotValid", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00004A30 File Offset: 0x00002C30
		public static LocalizedString InvalidUIMode(string mode)
		{
			return new LocalizedString("InvalidUIMode", Strings.ResourceManager, new object[]
			{
				mode
			});
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00004A58 File Offset: 0x00002C58
		public static LocalizedString ParameterNotValidForCurrentOperation(string parameter, string operation)
		{
			return new LocalizedString("ParameterNotValidForCurrentOperation", Strings.ResourceManager, new object[]
			{
				parameter,
				operation
			});
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00004A84 File Offset: 0x00002C84
		public static LocalizedString InvalidMode(string mode)
		{
			return new LocalizedString("InvalidMode", Strings.ResourceManager, new object[]
			{
				mode
			});
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00004AAC File Offset: 0x00002CAC
		public static LocalizedString NotAValidNumber(string s)
		{
			return new LocalizedString("NotAValidNumber", Strings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00004AD4 File Offset: 0x00002CD4
		public static LocalizedString InvalidIPv4Address(string ipaddress)
		{
			return new LocalizedString("InvalidIPv4Address", Strings.ResourceManager, new object[]
			{
				ipaddress
			});
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00004AFC File Offset: 0x00002CFC
		public static LocalizedString InstallRequiresRoles
		{
			get
			{
				return new LocalizedString("InstallRequiresRoles", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00004B14 File Offset: 0x00002D14
		public static LocalizedString ParameterSpecifiedMultipleTimes(string parameter)
		{
			return new LocalizedString("ParameterSpecifiedMultipleTimes", Strings.ResourceManager, new object[]
			{
				parameter
			});
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004B3C File Offset: 0x00002D3C
		public static LocalizedString InvalidServerIdParameter(string server)
		{
			return new LocalizedString("InvalidServerIdParameter", Strings.ResourceManager, new object[]
			{
				server
			});
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00004B64 File Offset: 0x00002D64
		public static LocalizedString NotAValidNonRootLocalLongFullPath(string path)
		{
			return new LocalizedString("NotAValidNonRootLocalLongFullPath", Strings.ResourceManager, new object[]
			{
				path
			});
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00004B8C File Offset: 0x00002D8C
		public static LocalizedString FileNotExist(string file)
		{
			return new LocalizedString("FileNotExist", Strings.ResourceManager, new object[]
			{
				file
			});
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004BB4 File Offset: 0x00002DB4
		public static LocalizedString NotAValidCultureString(string culture)
		{
			return new LocalizedString("NotAValidCultureString", Strings.ResourceManager, new object[]
			{
				culture
			});
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00004BDC File Offset: 0x00002DDC
		public static LocalizedString MBXRoleIsInstalled
		{
			get
			{
				return new LocalizedString("MBXRoleIsInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00004BF3 File Offset: 0x00002DF3
		public static LocalizedString CASRoleIsInstalled
		{
			get
			{
				return new LocalizedString("CASRoleIsInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00004C0C File Offset: 0x00002E0C
		public static LocalizedString RoleSpecifiedMultipleTimes(string role)
		{
			return new LocalizedString("RoleSpecifiedMultipleTimes", Strings.ResourceManager, new object[]
			{
				role
			});
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004C34 File Offset: 0x00002E34
		public static LocalizedString EmptyValueSpecified(string parameter)
		{
			return new LocalizedString("EmptyValueSpecified", Strings.ResourceManager, new object[]
			{
				parameter
			});
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004C5C File Offset: 0x00002E5C
		public static LocalizedString CommandLineSetupParameterEntry(string name, object value)
		{
			return new LocalizedString("CommandLineSetupParameterEntry", Strings.ResourceManager, new object[]
			{
				name,
				value
			});
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004C88 File Offset: 0x00002E88
		public static LocalizedString NotBooleanValue(string value)
		{
			return new LocalizedString("NotBooleanValue", Strings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004CB0 File Offset: 0x00002EB0
		public static LocalizedString ParameterCannotHaveValue(string parameter)
		{
			return new LocalizedString("ParameterCannotHaveValue", Strings.ResourceManager, new object[]
			{
				parameter
			});
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004CD8 File Offset: 0x00002ED8
		public static LocalizedString PrepareFlagConstraint(string name)
		{
			return new LocalizedString("PrepareFlagConstraint", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004D00 File Offset: 0x00002F00
		public static LocalizedString InvalidCommonName(string name)
		{
			return new LocalizedString("InvalidCommonName", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004D28 File Offset: 0x00002F28
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x0400008E RID: 142
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(5);

		// Token: 0x0400008F RID: 143
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Setup.Parser.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200000F RID: 15
		public enum IDs : uint
		{
			// Token: 0x04000091 RID: 145
			ParameterShouldStartWith = 1914581610U,
			// Token: 0x04000092 RID: 146
			CurrentOperationNotSet = 1564022379U,
			// Token: 0x04000093 RID: 147
			InstallRequiresRoles = 2509045854U,
			// Token: 0x04000094 RID: 148
			MBXRoleIsInstalled = 3247175721U,
			// Token: 0x04000095 RID: 149
			CASRoleIsInstalled = 2607811679U
		}

		// Token: 0x02000010 RID: 16
		private enum ParamIDs
		{
			// Token: 0x04000097 RID: 151
			NotAValidFqdn,
			// Token: 0x04000098 RID: 152
			UnknownParameter,
			// Token: 0x04000099 RID: 153
			AnswerFileCouldNotBeOpened,
			// Token: 0x0400009A RID: 154
			NotInTheRange,
			// Token: 0x0400009B RID: 155
			InvalidRole,
			// Token: 0x0400009C RID: 156
			InvalidEdbFilePath,
			// Token: 0x0400009D RID: 157
			DirectoryNotExist,
			// Token: 0x0400009E RID: 158
			AnswerFileSetupParameterEntry,
			// Token: 0x0400009F RID: 159
			NotValidIndustryType,
			// Token: 0x040000A0 RID: 160
			ParameterNotValidForCurrentRoles,
			// Token: 0x040000A1 RID: 161
			ParameterMustHaveValue,
			// Token: 0x040000A2 RID: 162
			AnswerFileNameNotValid,
			// Token: 0x040000A3 RID: 163
			InvalidUIMode,
			// Token: 0x040000A4 RID: 164
			ParameterNotValidForCurrentOperation,
			// Token: 0x040000A5 RID: 165
			InvalidMode,
			// Token: 0x040000A6 RID: 166
			NotAValidNumber,
			// Token: 0x040000A7 RID: 167
			InvalidIPv4Address,
			// Token: 0x040000A8 RID: 168
			ParameterSpecifiedMultipleTimes,
			// Token: 0x040000A9 RID: 169
			InvalidServerIdParameter,
			// Token: 0x040000AA RID: 170
			NotAValidNonRootLocalLongFullPath,
			// Token: 0x040000AB RID: 171
			FileNotExist,
			// Token: 0x040000AC RID: 172
			NotAValidCultureString,
			// Token: 0x040000AD RID: 173
			RoleSpecifiedMultipleTimes,
			// Token: 0x040000AE RID: 174
			EmptyValueSpecified,
			// Token: 0x040000AF RID: 175
			CommandLineSetupParameterEntry,
			// Token: 0x040000B0 RID: 176
			NotBooleanValue,
			// Token: 0x040000B1 RID: 177
			ParameterCannotHaveValue,
			// Token: 0x040000B2 RID: 178
			PrepareFlagConstraint,
			// Token: 0x040000B3 RID: 179
			InvalidCommonName
		}
	}
}
