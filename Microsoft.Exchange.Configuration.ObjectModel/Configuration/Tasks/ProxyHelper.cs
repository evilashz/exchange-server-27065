using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security;
using System.Text;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Core;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.Tasks;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000285 RID: 645
	internal class ProxyHelper
	{
		// Token: 0x06001625 RID: 5669 RVA: 0x00052C34 File Offset: 0x00050E34
		internal static IEnumerable<PSObject> RPSProxyExecution(Guid cmdletUniqueId, PSCommand command, string serverFqn, ExchangeRunspaceConfiguration runspaceConfig, int serverVersion, bool asyncProxying, Task.TaskWarningLoggingDelegate writeWarning)
		{
			Uri uri = ProxyHelper.BuildCmdletProxyUri(serverFqn, runspaceConfig, serverVersion);
			IEnumerable<PSObject> result;
			try
			{
				RemoteConnectionInfo connectionInfo = ProxyHelper.BuildProxyWSManConnectionInfo(uri);
				ProxyPSCommand proxyPSCommand = new ProxyPSCommand(connectionInfo, command, asyncProxying, writeWarning);
				result = proxyPSCommand.Invoke();
			}
			catch (Exception)
			{
				CmdletLogger.SafeAppendGenericInfo(cmdletUniqueId, "TargetUri", uri.ToString());
				throw;
			}
			return result;
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x00052C8C File Offset: 0x00050E8C
		internal static string GetCommandString(PSCommand command)
		{
			if (command.Commands.Count <= 0)
			{
				throw new ArgumentOutOfRangeException("command", "[ProxyHelper.GetCommandString] PSCommand should only have one command.");
			}
			StringBuilder stringBuilder = new StringBuilder(command.Commands[0].CommandText);
			foreach (CommandParameter commandParameter in command.Commands[0].Parameters)
			{
				ProxyHelper.BuildCommandFromParameter(commandParameter.Name, commandParameter.Value, stringBuilder);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x00052D2C File Offset: 0x00050F2C
		private static bool IsSingleQuote(char c)
		{
			return c == '\'' || c == '‘' || c == '’' || c == '‚' || c == '‛';
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x00052D58 File Offset: 0x00050F58
		private static string EscapeSingleQuotedString(string stringContent)
		{
			if (string.IsNullOrEmpty(stringContent))
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(stringContent.Length);
			foreach (char c in stringContent)
			{
				stringBuilder.Append(c);
				if (ProxyHelper.IsSingleQuote(c))
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x00052DB8 File Offset: 0x00050FB8
		internal static void BuildCommandFromParameter(string parameterName, object parameterValue, StringBuilder commandBuilder)
		{
			if (parameterValue == null)
			{
				commandBuilder.AppendFormat(" -{0}:{1}", parameterName, ProxyHelper.nullString);
				return;
			}
			Type type = parameterValue.GetType();
			if (type == typeof(bool))
			{
				commandBuilder.AppendFormat(" -{0}:${1}", parameterName, parameterValue);
				return;
			}
			if (type == typeof(SwitchParameter))
			{
				commandBuilder.AppendFormat(" -{0}:${1}", parameterName, ((SwitchParameter)parameterValue).IsPresent);
				return;
			}
			string stringContent;
			if (parameterValue is IDictionary)
			{
				stringContent = ProxyHelper.TranslateCmdletProxyDictionaryParameter(parameterValue, ExchangeRunspaceConfigurationSettings.ProxyMethod.PSWS).ToString();
			}
			else
			{
				if (parameterValue is ICollection)
				{
					string arg = ProxyHelper.TranslateCmdletProxyCollectionParameter(parameterValue, ExchangeRunspaceConfigurationSettings.ProxyMethod.PSWS).ToString();
					commandBuilder.AppendFormat(" -{0}:{1}", parameterName, arg);
					return;
				}
				stringContent = parameterValue.ToString();
			}
			commandBuilder.AppendFormat(" -{0}:'{1}'", parameterName, ProxyHelper.EscapeSingleQuotedString(stringContent));
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x00052E8C File Offset: 0x0005108C
		internal static Uri BuildCmdletProxyUri(string targetFqdn, ExchangeRunspaceConfiguration runspaceConfig, int targetVersion)
		{
			if (string.IsNullOrWhiteSpace(targetFqdn))
			{
				throw new ArgumentNullException("targetFqdn");
			}
			if (runspaceConfig == null)
			{
				throw new ArgumentNullException("runspaceConfig");
			}
			ExchangeRunspaceConfigurationSettings configurationSettings = runspaceConfig.ConfigurationSettings;
			ExAssert.RetailAssert(configurationSettings != null, "runspaceConfig.ConfigurationSettings should not be null.");
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("https://");
			stringBuilder.Append(targetFqdn);
			if (targetVersion >= Server.E15MinVersion)
			{
				stringBuilder.Append(":444/powershell/Powershell-proxy?");
			}
			else
			{
				stringBuilder.Append("/Powershell-proxy?");
			}
			stringBuilder.AppendFormat("{0}={1}", "X-Rps-CAT", Uri.EscapeDataString(configurationSettings.UserToken.CommonAccessTokenForCmdletProxy().Serialize()));
			stringBuilder.AppendFormat(";{0}={1}", "serializationLevel", configurationSettings.CurrentSerializationLevel.ToString());
			stringBuilder.AppendFormat(";{0}={1}", "clientApplication", configurationSettings.ClientApplication.ToString());
			if (configurationSettings.ClientApplication == ExchangeRunspaceConfigurationSettings.ExchangeApplication.ECP || configurationSettings.ClientApplication == ExchangeRunspaceConfigurationSettings.ExchangeApplication.EMC || configurationSettings.ClientApplication == ExchangeRunspaceConfigurationSettings.ExchangeApplication.OSP)
			{
				stringBuilder.AppendFormat(";{0}={1}", "proxyFullSerialization", "true");
			}
			string managedOrganization = ProxyHelper.GetManagedOrganization(runspaceConfig);
			if (!string.IsNullOrEmpty(managedOrganization))
			{
				stringBuilder.AppendFormat(";{0}", ProxyHelper.GetOrganizationAppendQueryIfNeeded(managedOrganization));
			}
			stringBuilder.AppendFormat(";{0}={1}", WellKnownHeader.CmdletProxyIsOn, "99C6ECEE-5A4F-47B9-AE69-49EAFB58F368");
			IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
			if (currentActivityScope != null && currentActivityScope.ActivityId != Guid.Empty)
			{
				stringBuilder.AppendFormat(";{0}={1}", "RequestId48CD6591-0506-4D6E-9131-797489A3260F", currentActivityScope.ActivityId);
			}
			return new Uri(stringBuilder.ToString());
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x00053021 File Offset: 0x00051221
		internal static string GetPSWSProxySiteUri(string remoteServerFqdn)
		{
			return string.Format("https://{0}:444/psws", remoteServerFqdn);
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x00053030 File Offset: 0x00051230
		internal static NameValueCollection GetPSWSProxyRequestHeaders(ExchangeRunspaceConfiguration runspaceConfig)
		{
			ExchangeRunspaceConfigurationSettings configurationSettings = runspaceConfig.ConfigurationSettings;
			ExAssert.RetailAssert(configurationSettings != null, "runspaceConfig.ConfigurationSettings should not be null.");
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["X-CommonAccessToken"] = configurationSettings.UserToken.CommonAccessTokenForCmdletProxy().Serialize();
			nameValueCollection["serializationLevel"] = configurationSettings.CurrentSerializationLevel.ToString();
			nameValueCollection["clientApplication"] = configurationSettings.ClientApplication.ToString();
			nameValueCollection["X-EncodeDecode-Key"] = "false";
			nameValueCollection[WellKnownHeader.CmdletProxyIsOn] = "99C6ECEE-5A4F-47B9-AE69-49EAFB58F368";
			if (configurationSettings.ClientApplication == ExchangeRunspaceConfigurationSettings.ExchangeApplication.ECP || configurationSettings.ClientApplication == ExchangeRunspaceConfigurationSettings.ExchangeApplication.EMC || configurationSettings.ClientApplication == ExchangeRunspaceConfigurationSettings.ExchangeApplication.OSP)
			{
				nameValueCollection["proxyFullSerialization"] = "true";
			}
			string managedOrganization = ProxyHelper.GetManagedOrganization(runspaceConfig);
			if (!string.IsNullOrEmpty(managedOrganization))
			{
				nameValueCollection["organization"] = managedOrganization;
			}
			return nameValueCollection;
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x00053114 File Offset: 0x00051314
		internal static string GetOrganizationAppendQueryIfNeeded(string tenantOrganization)
		{
			if (string.IsNullOrEmpty(tenantOrganization))
			{
				return string.Empty;
			}
			return string.Format("{0}={1}", "organization", tenantOrganization);
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x00053134 File Offset: 0x00051334
		internal static object ConvertPSObjectToOriginalType(PSObject psObject, int remoteServerVersion, Task.TaskVerboseLoggingDelegate writeVerbose)
		{
			if (psObject == null)
			{
				throw new ArgumentNullException("psObject");
			}
			Type type = MonadCommand.ResolveType(psObject);
			if (remoteServerVersion >= Server.E15MinVersion)
			{
				if (psObject.Members["SerializationData"] == null || psObject.Members["SerializationData"].Value == null)
				{
					if (writeVerbose != null)
					{
						writeVerbose(Strings.VerboseSerializationDataNotExist);
					}
				}
				else
				{
					try
					{
						return ProxyHelper.TypeConvertor.ConvertFrom(psObject, type, null, true);
					}
					catch (Exception ex)
					{
						if (writeVerbose != null)
						{
							writeVerbose(Strings.VerboseFailedToDeserializePSObject(ex.Message));
						}
					}
				}
			}
			return MockObjectInformation.TranslateToMockObject(type, psObject);
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x000531D8 File Offset: 0x000513D8
		internal static RemoteConnectionInfo BuildProxyWSManConnectionInfo(Uri connectionUri)
		{
			if (connectionUri == null)
			{
				throw new ArgumentNullException("connectionUri");
			}
			PSCredential pscredential = null;
			AuthenticationMechanism authenticationMechanism = AuthenticationMechanism.NegotiateWithImplicitCredential;
			if (ExTraceGlobals.FaultInjectionTracer.IsTraceEnabled(TraceType.FaultInjection))
			{
				ProxyHelper.FaultInjection_ProxySessionCredentialAndType(ref pscredential, ref authenticationMechanism);
			}
			return new RemoteConnectionInfo(connectionUri, pscredential, ProxyHelper.ExchangeShellSchema, null, authenticationMechanism, true, 0);
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x00053228 File Offset: 0x00051428
		public static object TranslateCmdletProxyParameter(object paramValue, ExchangeRunspaceConfigurationSettings.ProxyMethod proxyMethod)
		{
			if (paramValue == null)
			{
				return null;
			}
			Type type = paramValue.GetType();
			if (type.IsPrimitive)
			{
				return paramValue;
			}
			if (paramValue is IDictionary)
			{
				return ProxyHelper.TranslateCmdletProxyDictionaryParameter(paramValue, proxyMethod);
			}
			if (paramValue is ICollection)
			{
				return ProxyHelper.TranslateCmdletProxyCollectionParameter(paramValue, proxyMethod);
			}
			if (type == typeof(IIdentityParameter))
			{
				return paramValue.ToString();
			}
			if (type == typeof(SwitchParameter))
			{
				return ((SwitchParameter)paramValue).IsPresent;
			}
			return paramValue.ToString();
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x000532B0 File Offset: 0x000514B0
		public static string GetFriendlyVersionInformation(int version)
		{
			ServerVersion serverVersion = new ServerVersion(version);
			return string.Format("{0}.{1:D2}.{2:D4}.{3:D4}", new object[]
			{
				serverVersion.Major,
				serverVersion.Minor,
				serverVersion.Build,
				serverVersion.Revision
			});
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x00053310 File Offset: 0x00051510
		private static object TranslateCmdletProxyCollectionParameter(object paramValue, ExchangeRunspaceConfigurationSettings.ProxyMethod proxyMethod)
		{
			ICollection collection = (ICollection)paramValue;
			if (proxyMethod == ExchangeRunspaceConfigurationSettings.ProxyMethod.RPS)
			{
				Collection<object> collection2 = new Collection<object>();
				foreach (object paramValue2 in collection)
				{
					collection2.Add(ProxyHelper.TranslateCmdletProxyParameter(paramValue2, proxyMethod));
				}
				return collection2;
			}
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object paramValue3 in collection)
			{
				num++;
				object obj = ProxyHelper.TranslateCmdletProxyParameter(paramValue3, proxyMethod);
				string arg;
				if (obj == null)
				{
					arg = ProxyHelper.nullString;
				}
				else if (obj.GetType() == typeof(bool))
				{
					arg = (((bool)obj) ? ProxyHelper.trueString : ProxyHelper.falseString);
				}
				else
				{
					arg = obj.ToString().Replace("'", "''");
				}
				stringBuilder.AppendFormat("'{0}',", arg);
			}
			if (num == 0)
			{
				return null;
			}
			return stringBuilder.ToString().Trim().TrimEnd(new char[]
			{
				','
			});
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x00053464 File Offset: 0x00051664
		private static object TranslateCmdletProxyDictionaryParameter(object paramValue, ExchangeRunspaceConfigurationSettings.ProxyMethod proxyMethod)
		{
			IDictionary dictionary = (IDictionary)paramValue;
			if (proxyMethod == ExchangeRunspaceConfigurationSettings.ProxyMethod.RPS)
			{
				Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
				foreach (object obj in dictionary.Keys)
				{
					dictionary2.Add(obj.ToString(), ProxyHelper.TranslateCmdletProxyParameter(dictionary[obj], proxyMethod));
				}
				return dictionary2;
			}
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			foreach (object obj2 in dictionary.Keys)
			{
				num++;
				stringBuilder.Append(obj2.ToString()).Append(" = ");
				object obj3 = dictionary[obj2];
				if (obj3 == null)
				{
					stringBuilder.Append(ProxyHelper.nullString);
				}
				else
				{
					object obj4 = ProxyHelper.TranslateCmdletProxyParameter(obj3, proxyMethod);
					if (obj4.GetType() == typeof(bool))
					{
						stringBuilder.Append(((bool)obj4) ? ProxyHelper.trueString : ProxyHelper.falseString);
					}
					else
					{
						stringBuilder.Append(obj4.ToString());
					}
				}
				stringBuilder.Append("; ");
			}
			if (num == 0)
			{
				return null;
			}
			string str = stringBuilder.ToString().Trim().TrimEnd(new char[]
			{
				';'
			});
			return str + "}";
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x00053610 File Offset: 0x00051810
		private static string GetManagedOrganization(ExchangeRunspaceConfiguration runspaceConfiguration)
		{
			if (runspaceConfiguration.PartnerMode)
			{
				if (runspaceConfiguration.OrganizationId != null && runspaceConfiguration.OrganizationId != OrganizationId.ForestWideOrgId)
				{
					return runspaceConfiguration.OrganizationId.GetFriendlyName();
				}
				if (!string.IsNullOrEmpty(runspaceConfiguration.ConfigurationSettings.TenantOrganization))
				{
					return runspaceConfiguration.ConfigurationSettings.TenantOrganization;
				}
			}
			return null;
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x00053670 File Offset: 0x00051870
		internal static void FaultInjection_UserSid(ref UserToken userToken)
		{
			string empty = string.Empty;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(2982554941U, ref empty);
			if (!string.IsNullOrWhiteSpace(empty))
			{
				userToken.UpdateUserSidForTest(empty);
			}
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x000536A4 File Offset: 0x000518A4
		internal static void FaultInjection_ProxySessionCredentialAndType(ref PSCredential proxySessionCredential, ref AuthenticationMechanism authType)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			string empty3 = string.Empty;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(2915446077U, ref empty);
			ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(3989187901U, ref empty2);
			ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(2378575165U, ref empty3);
			if (!string.IsNullOrEmpty(empty) && !string.IsNullOrEmpty(empty2))
			{
				SecureString secureString = new SecureString();
				char[] array = empty2.ToCharArray();
				foreach (char c in array)
				{
					secureString.AppendChar(c);
				}
				proxySessionCredential = new PSCredential(empty, secureString);
			}
			if (!string.IsNullOrEmpty(empty3))
			{
				authType = (AuthenticationMechanism)Enum.Parse(typeof(AuthenticationMechanism), empty3, true);
			}
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x00053764 File Offset: 0x00051964
		internal static void FaultInjection_Identity(PropertyBag parameters)
		{
			if (parameters.Contains("Identity"))
			{
				string value = parameters["Identity"].ToString();
				ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(3519425853U, ref value);
				parameters.Remove("Identity");
				parameters.Add("Identity", value);
			}
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x000537B8 File Offset: 0x000519B8
		internal static void FaultInjection_ShouldForcedlyProxyCmdlet(Uri originalUrl, string remoteServerFQDN, ref bool shouldForcedlyProxyCmdlet)
		{
			if (originalUrl == null || (originalUrl.ToString().IndexOf("CommandInvocations", StringComparison.OrdinalIgnoreCase) < 0 && originalUrl.ToString().IndexOf("X-Rps-CAT", StringComparison.OrdinalIgnoreCase) < 0))
			{
				ExTraceGlobals.FaultInjectionTracer.TraceTest<bool>(2714119485U, ref shouldForcedlyProxyCmdlet);
			}
		}

		// Token: 0x040006C0 RID: 1728
		private const char quoteSingleLeft = '‘';

		// Token: 0x040006C1 RID: 1729
		private const char quoteSingleRight = '’';

		// Token: 0x040006C2 RID: 1730
		private const char quoteSingleBase = '‚';

		// Token: 0x040006C3 RID: 1731
		private const char quoteReversed = '‛';

		// Token: 0x040006C4 RID: 1732
		private static readonly string nullString = "$null";

		// Token: 0x040006C5 RID: 1733
		private static readonly string trueString = "$true";

		// Token: 0x040006C6 RID: 1734
		private static readonly string falseString = "$false";

		// Token: 0x040006C7 RID: 1735
		private static readonly string ExchangeShellSchema = "http://schemas.microsoft.com/powershell/Microsoft.Exchange";

		// Token: 0x040006C8 RID: 1736
		private static readonly SerializationTypeConverter TypeConvertor = new SerializationTypeConverter();

		// Token: 0x040006C9 RID: 1737
		public static readonly int PswsSupportProxyMinimumVersion = new ServerVersion(15, 0, 496, 0).ToInt();
	}
}
