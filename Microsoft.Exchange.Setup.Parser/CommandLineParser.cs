using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Setup.Parser
{
	// Token: 0x02000005 RID: 5
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class CommandLineParser : SetupParser
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002720 File Offset: 0x00000920
		public CommandLineParser(ISetupLogger logger)
		{
			base.SeparatorCharacters = ":";
			this.setupLogger = logger;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000273A File Offset: 0x0000093A
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002741 File Offset: 0x00000941
		public static bool IsClientAccessRole { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002749 File Offset: 0x00000949
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002750 File Offset: 0x00000950
		public static int TotalClientAccessRoles { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002758 File Offset: 0x00000958
		// (set) Token: 0x06000011 RID: 17 RVA: 0x0000275F File Offset: 0x0000095F
		public static bool IsMailboxRole { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002767 File Offset: 0x00000967
		// (set) Token: 0x06000013 RID: 19 RVA: 0x0000276E File Offset: 0x0000096E
		public static int TotalMailboxRoles { get; private set; }

		// Token: 0x06000014 RID: 20 RVA: 0x00002778 File Offset: 0x00000978
		public static object ParseMode(string givenMode)
		{
			string a;
			if ((a = givenMode.ToLowerInvariant()) != null)
			{
				SetupOperations setupOperations;
				if (!(a == "install"))
				{
					if (!(a == "uninstall"))
					{
						if (!(a == "recoverserver"))
						{
							if (!(a == "upgrade"))
							{
								goto IL_50;
							}
							setupOperations = SetupOperations.Upgrade;
						}
						else
						{
							setupOperations = SetupOperations.RecoverServer;
						}
					}
					else
					{
						setupOperations = SetupOperations.Uninstall;
					}
				}
				else
				{
					setupOperations = SetupOperations.Install;
				}
				return setupOperations;
			}
			IL_50:
			throw new ParseException(Strings.InvalidMode(givenMode));
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000027E8 File Offset: 0x000009E8
		public static object ParseRoles(string givenRoles)
		{
			CommandLineParser.IsClientAccessRole = false;
			CommandLineParser.IsMailboxRole = false;
			CommandLineParser.TotalClientAccessRoles = 0;
			CommandLineParser.TotalMailboxRoles = 0;
			string text = givenRoles.ToLowerInvariant();
			RoleCollection roleCollection = new RoleCollection();
			SetupRoles setupRoles = SetupRoles.None;
			string[] array = text.Split(",".ToCharArray());
			foreach (string text2 in array)
			{
				string text3 = text2.Trim();
				if (!string.IsNullOrEmpty(text3))
				{
					string key;
					switch (key = text3)
					{
					case "h":
					case "ht":
					case "hubtransport":
						if ((setupRoles & SetupRoles.Bridgehead) != SetupRoles.None)
						{
							throw new ParseException(Strings.RoleSpecifiedMultipleTimes("HubTransport"));
						}
						setupRoles |= SetupRoles.Bridgehead;
						roleCollection.Add(new BridgeheadRole());
						goto IL_490;
					case "e":
					case "et":
					case "edgetransport":
						if ((setupRoles & SetupRoles.Gateway) != SetupRoles.None)
						{
							throw new ParseException(Strings.RoleSpecifiedMultipleTimes("EdgeTransport"));
						}
						setupRoles |= SetupRoles.Gateway;
						roleCollection.Add(new GatewayRole());
						goto IL_490;
					case "t":
					case "mt":
					case "managementtools":
						if ((setupRoles & SetupRoles.AdminTools) != SetupRoles.None)
						{
							throw new ParseException(Strings.RoleSpecifiedMultipleTimes("ManagementTools"));
						}
						setupRoles |= SetupRoles.AdminTools;
						roleCollection.Add(new AdminToolsRole());
						goto IL_490;
					case "monitoring":
					case "mn":
					case "n":
						if ((setupRoles & SetupRoles.Monitoring) != SetupRoles.None)
						{
							throw new ParseException(Strings.RoleSpecifiedMultipleTimes("Monitoring"));
						}
						setupRoles |= SetupRoles.Monitoring;
						roleCollection.Add(new MonitoringRole());
						goto IL_490;
					case "centraladmin":
					case "eca":
					case "a":
						if ((setupRoles & SetupRoles.CentralAdmin) != SetupRoles.None)
						{
							throw new ParseException(Strings.RoleSpecifiedMultipleTimes("CentralAdmin"));
						}
						setupRoles |= SetupRoles.CentralAdmin;
						roleCollection.Add(new CentralAdminRole());
						goto IL_490;
					case "centraladmindatabase":
					case "cadb":
					case "d":
						if ((setupRoles & SetupRoles.CentralAdminDatabase) != SetupRoles.None)
						{
							throw new ParseException(Strings.RoleSpecifiedMultipleTimes("CentralAdminDatabase"));
						}
						setupRoles |= SetupRoles.CentralAdminDatabase;
						roleCollection.Add(new CentralAdminDatabaseRole());
						goto IL_490;
					case "centraladminfrontend":
					case "mafe":
					case "ma":
						if ((setupRoles & SetupRoles.CentralAdminFrontEnd) != SetupRoles.None)
						{
							throw new ParseException(Strings.RoleSpecifiedMultipleTimes("CentralAdminFrontEnd"));
						}
						setupRoles |= SetupRoles.CentralAdminFrontEnd;
						roleCollection.Add(new CentralAdminFrontEndRole());
						goto IL_490;
					case "frontendtransport":
					case "fet":
					case "ft":
						if ((setupRoles & SetupRoles.FrontendTransport) != SetupRoles.None)
						{
							throw new ParseException(Strings.RoleSpecifiedMultipleTimes("FrontendTransport"));
						}
						setupRoles |= SetupRoles.FrontendTransport;
						roleCollection.Add(new FrontendTransportRole());
						goto IL_490;
					case "clientaccess":
					case "ca":
					case "c":
						CommandLineParser.IsClientAccessRole = true;
						goto IL_490;
					case "m":
					case "mb":
					case "mailbox":
						CommandLineParser.IsMailboxRole = true;
						goto IL_490;
					case "o":
					case "os":
					case "osp":
						if ((setupRoles & SetupRoles.OSP) != SetupRoles.None)
						{
							throw new ParseException(Strings.RoleSpecifiedMultipleTimes("OfficeServicePulse"));
						}
						setupRoles |= SetupRoles.OSP;
						roleCollection.Add(new OSPRole());
						goto IL_490;
					case "":
						goto IL_490;
					}
					throw new ParseException(Strings.InvalidRole(text3));
				}
				IL_490:;
			}
			if (CommandLineParser.IsClientAccessRole)
			{
				if ((setupRoles & SetupRoles.Cafe) == SetupRoles.None)
				{
					roleCollection.Add(new CafeRole());
					CommandLineParser.TotalClientAccessRoles++;
				}
				if ((setupRoles & SetupRoles.FrontendTransport) == SetupRoles.None)
				{
					roleCollection.Add(new FrontendTransportRole());
					CommandLineParser.TotalClientAccessRoles++;
				}
			}
			if (CommandLineParser.IsMailboxRole)
			{
				CommandLineParser.TotalMailboxRoles = 0;
				if ((setupRoles & SetupRoles.Mailbox) == SetupRoles.None)
				{
					roleCollection.Add(new MailboxRole());
					CommandLineParser.TotalMailboxRoles++;
				}
				if ((setupRoles & SetupRoles.ClientAccess) == SetupRoles.None)
				{
					roleCollection.Add(new ClientAccessRole());
					CommandLineParser.TotalMailboxRoles++;
				}
				if ((setupRoles & SetupRoles.Bridgehead) == SetupRoles.None)
				{
					roleCollection.Add(new BridgeheadRole());
					CommandLineParser.TotalMailboxRoles++;
				}
				if ((setupRoles & SetupRoles.UnifiedMessaging) == SetupRoles.None)
				{
					roleCollection.Add(new UnifiedMessagingRole());
					CommandLineParser.TotalMailboxRoles++;
				}
			}
			return roleCollection;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002D5C File Offset: 0x00000F5C
		public static object ParseUInt16(string s)
		{
			ushort num;
			try
			{
				num = ushort.Parse(s);
			}
			catch (FormatException innerException)
			{
				throw new ParseException(Strings.NotAValidNumber(s), innerException);
			}
			catch (OverflowException innerException2)
			{
				throw new ParseException(Strings.NotInTheRange(s), innerException2);
			}
			return num;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002DB0 File Offset: 0x00000FB0
		public static object ParseCultureList(string cultures)
		{
			string text = cultures.ToLowerInvariant();
			List<CultureInfo> list = new List<CultureInfo>();
			string[] array = text.Split(",".ToCharArray());
			foreach (string text2 in array)
			{
				string text3 = text2.Trim();
				if (!string.IsNullOrEmpty(text3))
				{
					try
					{
						CultureInfo item = CultureInfo.CreateSpecificCulture(text3);
						if (!list.Contains(item))
						{
							list.Add(item);
						}
					}
					catch (ArgumentException innerException)
					{
						throw new ParseException(Strings.NotAValidCultureString(text3), innerException);
					}
				}
			}
			return list;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002E48 File Offset: 0x00001048
		public static object ParseSourceDir(string path)
		{
			LongPath result = CommandLineParser.ParseToLongPath(path);
			if (!Directory.Exists(path))
			{
				throw new ParseException(Strings.DirectoryNotExist(path));
			}
			return result;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002E74 File Offset: 0x00001074
		public static object ParseSourceFile(string file)
		{
			LongPath result = CommandLineParser.ParseToLongPath(file);
			if (!File.Exists(file))
			{
				throw new ParseException(Strings.FileNotExist(file));
			}
			return result;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002EA0 File Offset: 0x000010A0
		public static object ParseNonRootLocalLongFullPath(string path)
		{
			NonRootLocalLongFullPath nonRootLocalLongFullPath;
			try
			{
				nonRootLocalLongFullPath = NonRootLocalLongFullPath.Parse(path);
			}
			catch (ArgumentException innerException)
			{
				throw new ParseException(Strings.NotAValidNonRootLocalLongFullPath(path), innerException);
			}
			if (!Directory.Exists(nonRootLocalLongFullPath.DriveName))
			{
				throw new ParseException(Strings.NotAValidNonRootLocalLongFullPath(path));
			}
			return nonRootLocalLongFullPath;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002EF0 File Offset: 0x000010F0
		public static object ParseOrganizationName(string name)
		{
			return name;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002EF4 File Offset: 0x000010F4
		public static object ParseBool(string s)
		{
			bool flag;
			try
			{
				flag = bool.Parse(s);
			}
			catch (FormatException innerException)
			{
				throw new ParseException(Strings.NotBooleanValue(s), innerException);
			}
			return flag;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002F30 File Offset: 0x00001130
		public static object ParseDbFilePath(string path)
		{
			EdbFilePath edbFilePath;
			try
			{
				edbFilePath = EdbFilePath.Parse(path);
			}
			catch (ArgumentException innerException)
			{
				throw new ParseException(Strings.InvalidEdbFilePath(path), innerException);
			}
			catch (FormatException innerException2)
			{
				throw new ParseException(Strings.InvalidEdbFilePath(path), innerException2);
			}
			if (!Directory.Exists(edbFilePath.DriveName))
			{
				throw new ParseException(Strings.InvalidEdbFilePath(path));
			}
			return edbFilePath;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002F98 File Offset: 0x00001198
		public Dictionary<string, object> ParseCommandLine(string[] args)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			List<string> args2 = this.ProcessCommandLine(args);
			Dictionary<string, SetupParameter> dictionary2 = base.ParseAll(args2);
			Dictionary<string, SetupParameter> dictionary3 = null;
			if (dictionary2.ContainsKey("roles") && !dictionary2.ContainsKey("mode"))
			{
				dictionary2.Add("mode", base.Parse("mode:install"));
			}
			foreach (SetupParameter setupParameter in dictionary2.Values)
			{
				this.setupLogger.Log(Strings.CommandLineSetupParameterEntry(setupParameter.Name, setupParameter.Value));
			}
			SetupOperations currentOperation = this.CalculateOperationType(dictionary2);
			if (dictionary2.ContainsKey("answerfile"))
			{
				AnswerFileParser answerFileParser = new AnswerFileParser();
				string text = dictionary2["answerfile"].Value as string;
				if (string.IsNullOrEmpty(text))
				{
					throw new ParseException(Strings.AnswerFileNameNotValid(text));
				}
				dictionary3 = answerFileParser.ParseFile(text);
				foreach (SetupParameter setupParameter2 in dictionary3.Values)
				{
					this.setupLogger.Log(Strings.AnswerFileSetupParameterEntry(setupParameter2.Name, setupParameter2.Value));
				}
				answerFileParser.ValidateParameters(dictionary3, currentOperation);
			}
			this.ValidateParameters(dictionary2, currentOperation);
			foreach (SetupParameter setupParameter3 in dictionary2.Values)
			{
				dictionary.Add(setupParameter3.Name, setupParameter3.Value);
			}
			if (dictionary3 != null)
			{
				foreach (SetupParameter setupParameter4 in dictionary3.Values)
				{
					if (dictionary.ContainsKey(setupParameter4.Name))
					{
						throw new ParseException(Strings.ParameterSpecifiedMultipleTimes(setupParameter4.Name));
					}
					dictionary.Add(setupParameter4.Name, setupParameter4.Value);
				}
			}
			return dictionary;
		}

		// Token: 0x0600001F RID: 31
		public abstract SetupOperations CalculateOperationType(Dictionary<string, SetupParameter> parameters);

		// Token: 0x06000020 RID: 32 RVA: 0x000031D8 File Offset: 0x000013D8
		private static LongPath ParseToLongPath(string path)
		{
			LongPath result;
			try
			{
				result = LongPath.Parse(path);
			}
			catch (ArgumentException ex)
			{
				throw new ParseException(new LocalizedString(ex.Message), ex);
			}
			return result;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003214 File Offset: 0x00001414
		private List<string> ProcessCommandLine(string[] args)
		{
			List<string> list = new List<string>();
			if (args == null || args.Length == 0)
			{
				return list;
			}
			int i = 0;
			if (!args[i].StartsWith("/") && !args[i].StartsWith("-"))
			{
				throw new ParseException(Strings.ParameterShouldStartWith);
			}
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder(128);
			List<string> list2 = new List<string>(args);
			while (i < list2.Count)
			{
				if (list2[i].Contains(":\""))
				{
					list2[i] = list2[i].Replace(":\"", ":\\");
					string[] array = list2[i].Split(new char[]
					{
						' '
					});
					if (array.Length > 1)
					{
						list2.RemoveAt(i);
						list2.InsertRange(i, array);
					}
				}
				if (list2[i].StartsWith("/") || list2[i].StartsWith("-"))
				{
					flag = (list2[i].IndexOfAny(base.SeparatorCharacters.ToCharArray()) == -1 || list2[i].IndexOfAny(base.SeparatorCharacters.ToCharArray()) == list2[i].Length - 1);
					if (stringBuilder.Length != 0)
					{
						list.Add(stringBuilder.ToString());
					}
					stringBuilder.Clear();
					stringBuilder.Append(list2[i].Substring(1));
				}
				else if (flag)
				{
					if (stringBuilder.Length == 0 || base.SeparatorCharacters.IndexOf(stringBuilder[stringBuilder.Length - 1]) == -1)
					{
						stringBuilder.Append(base.SeparatorCharacters[0]);
						stringBuilder.Append(list2[i]);
					}
					else
					{
						stringBuilder.Append(list2[i]);
					}
					flag = false;
				}
				else
				{
					stringBuilder.Append(",");
					stringBuilder.Append(list2[i]);
				}
				i++;
			}
			if (stringBuilder.Length != 0)
			{
				list.Add(stringBuilder.ToString());
			}
			return list;
		}

		// Token: 0x04000027 RID: 39
		private const string BridgeheadShort = "h";

		// Token: 0x04000028 RID: 40
		private const string BridgeheadMedium = "ht";

		// Token: 0x04000029 RID: 41
		private const string BridgeheadLong = "hubtransport";

		// Token: 0x0400002A RID: 42
		private const string Bridgehead = "HubTransport";

		// Token: 0x0400002B RID: 43
		private const string GatewayShort = "e";

		// Token: 0x0400002C RID: 44
		private const string GatewayMedium = "et";

		// Token: 0x0400002D RID: 45
		private const string GatewayLong = "edgetransport";

		// Token: 0x0400002E RID: 46
		private const string Gateway = "EdgeTransport";

		// Token: 0x0400002F RID: 47
		private const string AdminToolsShort = "t";

		// Token: 0x04000030 RID: 48
		private const string AdminToolsMedium = "mt";

		// Token: 0x04000031 RID: 49
		private const string AdminToolsLong = "managementtools";

		// Token: 0x04000032 RID: 50
		private const string AdminTools = "ManagementTools";

		// Token: 0x04000033 RID: 51
		private const string MonitoringShort = "n";

		// Token: 0x04000034 RID: 52
		private const string MonitoringMedium = "mn";

		// Token: 0x04000035 RID: 53
		private const string MonitoringLong = "monitoring";

		// Token: 0x04000036 RID: 54
		private const string Monitoring = "Monitoring";

		// Token: 0x04000037 RID: 55
		private const string CentralAdminShort = "a";

		// Token: 0x04000038 RID: 56
		private const string CentralAdminMedium = "eca";

		// Token: 0x04000039 RID: 57
		private const string CentralAdminLong = "centraladmin";

		// Token: 0x0400003A RID: 58
		private const string CentralAdmin = "CentralAdmin";

		// Token: 0x0400003B RID: 59
		private const string CentralAdminDatabaseShort = "d";

		// Token: 0x0400003C RID: 60
		private const string CentralAdminDatabaseMedium = "cadb";

		// Token: 0x0400003D RID: 61
		private const string CentralAdminDatabaseLong = "centraladmindatabase";

		// Token: 0x0400003E RID: 62
		private const string CentralAdminDatabase = "CentralAdminDatabase";

		// Token: 0x0400003F RID: 63
		private const string CentralAdminFrontEndShort = "ma";

		// Token: 0x04000040 RID: 64
		private const string CentralAdminFrontEndMedium = "mafe";

		// Token: 0x04000041 RID: 65
		private const string CentralAdminFrontEndLong = "centraladminfrontend";

		// Token: 0x04000042 RID: 66
		private const string CentralAdminFrontEnd = "CentralAdminFrontEnd";

		// Token: 0x04000043 RID: 67
		private const string FrontendTransportShort = "ft";

		// Token: 0x04000044 RID: 68
		private const string FrontendTransportMedium = "fet";

		// Token: 0x04000045 RID: 69
		private const string FrontendTransportLong = "frontendtransport";

		// Token: 0x04000046 RID: 70
		private const string FrontendTransport = "FrontendTransport";

		// Token: 0x04000047 RID: 71
		private const string MailboxShort = "m";

		// Token: 0x04000048 RID: 72
		private const string MailboxMedium = "mb";

		// Token: 0x04000049 RID: 73
		private const string MailboxLong = "mailbox";

		// Token: 0x0400004A RID: 74
		private const string Mailbox = "Mailbox";

		// Token: 0x0400004B RID: 75
		private const string ClientAccessShort = "c";

		// Token: 0x0400004C RID: 76
		private const string ClientAccessMedium = "ca";

		// Token: 0x0400004D RID: 77
		private const string ClientAccessLong = "clientaccess";

		// Token: 0x0400004E RID: 78
		private const string ClientAccess = "ClientAccess";

		// Token: 0x0400004F RID: 79
		private const string OSPShort = "o";

		// Token: 0x04000050 RID: 80
		private const string OSPMedium = "os";

		// Token: 0x04000051 RID: 81
		private const string OSPLong = "osp";

		// Token: 0x04000052 RID: 82
		private const string OSP = "OfficeServicePulse";

		// Token: 0x04000053 RID: 83
		private const string RoleSeparator = ",";

		// Token: 0x04000054 RID: 84
		private const string NetworkSeparator = ",";

		// Token: 0x04000055 RID: 85
		private const string CultureSeparator = ",";

		// Token: 0x04000056 RID: 86
		private readonly ISetupLogger setupLogger;
	}
}
