using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200058D RID: 1421
	[ObjectScope(ConfigScopes.Server)]
	[Serializable]
	public class Server : ADLegacyVersionableObject
	{
		// Token: 0x1700143F RID: 5183
		// (get) Token: 0x06003F47 RID: 16199 RVA: 0x000F5B29 File Offset: 0x000F3D29
		internal override ADObjectSchema Schema
		{
			get
			{
				if (this.schema == null)
				{
					if (TopologyProvider.IsAdamTopology())
					{
						this.schema = ObjectSchema.GetInstance<ServerSchema>();
					}
					else
					{
						this.schema = ObjectSchema.GetInstance<ActiveDirectoryServerSchema>();
					}
				}
				return this.schema;
			}
		}

		// Token: 0x17001440 RID: 5184
		// (get) Token: 0x06003F48 RID: 16200 RVA: 0x000F5B58 File Offset: 0x000F3D58
		internal override string MostDerivedObjectClass
		{
			get
			{
				return Server.MostDerivedClass;
			}
		}

		// Token: 0x06003F49 RID: 16201 RVA: 0x000F5BA4 File Offset: 0x000F3DA4
		internal static GetterDelegate AssistantConfigurationGetterDelegate(TimeBasedAssistantIndex assistantIndex, Server.GetConfigurationDelegate getConfiguration)
		{
			return delegate(IPropertyBag bag)
			{
				MultiValuedProperty<string> allConfigurations = (MultiValuedProperty<string>)bag[ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle];
				Server.AssistantConfigurationEntry configurationForAssistant = Server.AssistantConfigurationEntry.GetConfigurationForAssistant(allConfigurations, assistantIndex);
				return getConfiguration(configurationForAssistant);
			};
		}

		// Token: 0x06003F4A RID: 16202 RVA: 0x000F5C0B File Offset: 0x000F3E0B
		internal static GetterDelegate AssistantWorkCycleGetterDelegate(TimeBasedAssistantIndex assistantIndex)
		{
			return Server.AssistantConfigurationGetterDelegate(assistantIndex, delegate(Server.AssistantConfigurationEntry entry)
			{
				if (entry != null && entry.WorkCycle != EnhancedTimeSpan.Zero)
				{
					return new EnhancedTimeSpan?(entry.WorkCycle);
				}
				return null;
			});
		}

		// Token: 0x06003F4B RID: 16203 RVA: 0x000F5C67 File Offset: 0x000F3E67
		internal static GetterDelegate AssistantCheckpointGetterDelegate(TimeBasedAssistantIndex assistantIndex)
		{
			return Server.AssistantConfigurationGetterDelegate(assistantIndex, delegate(Server.AssistantConfigurationEntry entry)
			{
				if (entry != null && entry.WorkCycleCheckpoint != EnhancedTimeSpan.Zero)
				{
					return new EnhancedTimeSpan?(entry.WorkCycleCheckpoint);
				}
				return null;
			});
		}

		// Token: 0x06003F4C RID: 16204 RVA: 0x000F5D30 File Offset: 0x000F3F30
		internal static SetterDelegate AssistantConfigurationSetterDelegate(TimeBasedAssistantIndex assistantIndex, Server.UpdateEntryDelegate updateEntry)
		{
			return delegate(object value, IPropertyBag bag)
			{
				MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)bag[ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle];
				Server.AssistantConfigurationEntry assistantConfigurationEntry = Server.AssistantConfigurationEntry.GetConfigurationForAssistant(multiValuedProperty, assistantIndex);
				for (int i = 0; i < multiValuedProperty.Count; i++)
				{
					if (Server.AssistantConfigurationEntry.IsAssistantConfiguration(multiValuedProperty[i], assistantIndex))
					{
						multiValuedProperty.RemoveAt(i);
						break;
					}
				}
				EnhancedTimeSpan? enhancedTimeSpan = (EnhancedTimeSpan?)value;
				assistantConfigurationEntry = updateEntry(assistantConfigurationEntry, (enhancedTimeSpan != null) ? enhancedTimeSpan.Value : EnhancedTimeSpan.Zero);
				multiValuedProperty.Add(assistantConfigurationEntry.ToString());
				bag[ActiveDirectoryServerSchema.AssistantsThrottleWorkcycle] = multiValuedProperty;
			};
		}

		// Token: 0x06003F4D RID: 16205 RVA: 0x000F5D88 File Offset: 0x000F3F88
		internal static SetterDelegate AssistantWorkCycleSetterDelegate(TimeBasedAssistantIndex assistantIndex)
		{
			return Server.AssistantConfigurationSetterDelegate(assistantIndex, delegate(Server.AssistantConfigurationEntry entry, EnhancedTimeSpan workCycle)
			{
				if (entry != null)
				{
					entry.WorkCycle = workCycle;
				}
				else
				{
					entry = new Server.AssistantConfigurationEntry(assistantIndex, workCycle, EnhancedTimeSpan.Zero);
				}
				return entry;
			});
		}

		// Token: 0x06003F4E RID: 16206 RVA: 0x000F5DE4 File Offset: 0x000F3FE4
		internal static SetterDelegate AssistantCheckpointSetterDelegate(TimeBasedAssistantIndex assistantIndex)
		{
			return Server.AssistantConfigurationSetterDelegate(assistantIndex, delegate(Server.AssistantConfigurationEntry entry, EnhancedTimeSpan checkpoint)
			{
				if (entry != null)
				{
					entry.WorkCycleCheckpoint = checkpoint;
				}
				else
				{
					entry = new Server.AssistantConfigurationEntry(assistantIndex, EnhancedTimeSpan.Zero, checkpoint);
				}
				return entry;
			});
		}

		// Token: 0x06003F4F RID: 16207 RVA: 0x000F5E9C File Offset: 0x000F409C
		internal static GetterDelegate AssistantMaintenanceScheduleGetterDelegate(ProviderPropertyDefinition propertyDefinition, ScheduledAssistant assistant)
		{
			return delegate(IPropertyBag bag)
			{
				MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)bag[propertyDefinition];
				foreach (string adString in multiValuedProperty)
				{
					Server.MaintenanceScheduleEntry fromADString = Server.MaintenanceScheduleEntry.GetFromADString(adString, assistant);
					if (fromADString != null)
					{
						return fromADString.MaintenanceSchedule;
					}
				}
				return new ScheduleInterval[0];
			};
		}

		// Token: 0x06003F50 RID: 16208 RVA: 0x000F5F5C File Offset: 0x000F415C
		internal static SetterDelegate AssistantMaintenanceScheduleSetterDelegate(ProviderPropertyDefinition propertyDefinition, ScheduledAssistant assistant)
		{
			return delegate(object value, IPropertyBag bag)
			{
				ScheduleInterval[] array = (ScheduleInterval[])value;
				string text = (array == null) ? null : new Server.MaintenanceScheduleEntry(assistant, array).ToADString();
				MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)bag[propertyDefinition];
				for (int i = multiValuedProperty.Count - 1; i >= 0; i--)
				{
					Server.MaintenanceScheduleEntry fromADString = Server.MaintenanceScheduleEntry.GetFromADString(multiValuedProperty[i], assistant);
					if (fromADString != null)
					{
						multiValuedProperty.RemoveAt(i);
					}
				}
				if (text != null)
				{
					multiValuedProperty.Add(text);
				}
				bag[propertyDefinition] = multiValuedProperty;
			};
		}

		// Token: 0x06003F51 RID: 16209 RVA: 0x000F5F8C File Offset: 0x000F418C
		internal static object InternalTransportCertificateThumbprintGetter(IPropertyBag propertyBag)
		{
			object result = null;
			try
			{
				byte[] array = (byte[])propertyBag[ServerSchema.InternalTransportCertificate];
				if (array != null)
				{
					X509Certificate2 x509Certificate = new X509Certificate2(array);
					result = x509Certificate.Thumbprint;
				}
			}
			catch (CryptographicException)
			{
			}
			return result;
		}

		// Token: 0x06003F52 RID: 16210 RVA: 0x000F5FD4 File Offset: 0x000F41D4
		internal static object IsPreE12FrontEndGetter(IPropertyBag propertyBag)
		{
			return (bool)Server.IsExchange2003Sp1OrLaterGetter(propertyBag) && !(bool)Server.IsE12OrLaterGetter(propertyBag) && 1 == (int)propertyBag[ServerSchema.ServerRole];
		}

		// Token: 0x06003F53 RID: 16211 RVA: 0x000F600B File Offset: 0x000F420B
		internal static object IsPreE12RPCHTTPEnabledGetter(IPropertyBag propertyBag)
		{
			return (bool)Server.IsExchange2003Sp1OrLaterGetter(propertyBag) && !(bool)Server.IsE12OrLaterGetter(propertyBag) && 0 != (536870912 & (int)propertyBag[ServerSchema.Heuristics]);
		}

		// Token: 0x06003F54 RID: 16212 RVA: 0x000F604C File Offset: 0x000F424C
		internal static object IsExchange2003OrLaterGetter(IPropertyBag propertyBag)
		{
			int num = (int)propertyBag[ServerSchema.VersionNumber];
			return num >= Server.E2k3MinVersion;
		}

		// Token: 0x06003F55 RID: 16213 RVA: 0x000F607C File Offset: 0x000F427C
		internal static object IsExchange2003Sp1OrLaterGetter(IPropertyBag propertyBag)
		{
			int num = (int)propertyBag[ServerSchema.VersionNumber];
			return num >= Server.E2k3SP1MinVersion;
		}

		// Token: 0x06003F56 RID: 16214 RVA: 0x000F60AC File Offset: 0x000F42AC
		internal static object IsExchange2003Sp2OrLaterGetter(IPropertyBag propertyBag)
		{
			int num = (int)propertyBag[ServerSchema.VersionNumber];
			return num >= Server.E2k3SP2MinVersion;
		}

		// Token: 0x06003F57 RID: 16215 RVA: 0x000F60DC File Offset: 0x000F42DC
		internal static object IsExchange2003Sp3OrLaterGetter(IPropertyBag propertyBag)
		{
			int num = (int)propertyBag[ServerSchema.VersionNumber];
			return num >= Server.E2k3SP3MinVersion;
		}

		// Token: 0x06003F58 RID: 16216 RVA: 0x000F610C File Offset: 0x000F430C
		internal static object IsE12OrLaterGetter(IPropertyBag propertyBag)
		{
			int num = (int)propertyBag[ServerSchema.VersionNumber];
			return num >= Server.E2007MinVersion;
		}

		// Token: 0x06003F59 RID: 16217 RVA: 0x000F613C File Offset: 0x000F433C
		internal static object IsE14OrLaterGetter(IPropertyBag propertyBag)
		{
			int num = (int)propertyBag[ServerSchema.VersionNumber];
			return num >= Server.E14MinVersion;
		}

		// Token: 0x06003F5A RID: 16218 RVA: 0x000F616C File Offset: 0x000F436C
		internal static object IsE14Sp1OrLaterGetter(IPropertyBag propertyBag)
		{
			int num = (int)propertyBag[ServerSchema.VersionNumber];
			return num >= Server.E14SP1MinVersion;
		}

		// Token: 0x06003F5B RID: 16219 RVA: 0x000F619C File Offset: 0x000F439C
		internal static object IsE15OrLaterGetter(IPropertyBag propertyBag)
		{
			int num = (int)propertyBag[ServerSchema.VersionNumber];
			return num >= Server.E15MinVersion;
		}

		// Token: 0x06003F5C RID: 16220 RVA: 0x000F61CC File Offset: 0x000F43CC
		internal static object MajorVersionGetter(IPropertyBag propertyBag)
		{
			int num = (int)propertyBag[ServerSchema.VersionNumber];
			return num >> 22 & 63;
		}

		// Token: 0x06003F5D RID: 16221 RVA: 0x000F61F8 File Offset: 0x000F43F8
		internal static object FqdnGetter(IPropertyBag propertyBag)
		{
			NetworkAddressCollection networkAddressCollection = (NetworkAddressCollection)propertyBag[ServerSchema.NetworkAddress];
			NetworkAddress networkAddress = networkAddressCollection[NetworkProtocol.TcpIP];
			if (!(networkAddress != null))
			{
				return string.Empty;
			}
			return networkAddress.AddressString;
		}

		// Token: 0x06003F5E RID: 16222 RVA: 0x000F6238 File Offset: 0x000F4438
		internal static QueryFilter FqdnFilterBuilder(SinglePropertyFilter filter)
		{
			string name = ServerSchema.Fqdn.Name;
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			if (comparisonFilter == null)
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(name, filter.GetType(), typeof(ComparisonFilter)));
			}
			return new ComparisonFilter(comparisonFilter.ComparisonOperator, ServerSchema.NetworkAddress, NetworkProtocol.TcpIP.ProtocolName + ':' + (comparisonFilter.PropertyValue ?? string.Empty));
		}

		// Token: 0x06003F5F RID: 16223 RVA: 0x000F62AC File Offset: 0x000F44AC
		internal static object DomainGetter(IPropertyBag propertyBag)
		{
			string result = string.Empty;
			string text = (string)Server.FqdnGetter(propertyBag);
			if (!string.IsNullOrEmpty(text))
			{
				int num = text.IndexOf('.') + 1;
				if (num > 0 && num < text.Length)
				{
					result = text.Substring(num);
				}
			}
			return result;
		}

		// Token: 0x06003F60 RID: 16224 RVA: 0x000F62F4 File Offset: 0x000F44F4
		internal static object OuGetter(IPropertyBag propertyBag)
		{
			string arg = (string)Server.DomainGetter(propertyBag);
			return string.Format("{0}/{1}", arg, (string)propertyBag[ADObjectSchema.RawName]);
		}

		// Token: 0x06003F61 RID: 16225 RVA: 0x000F6328 File Offset: 0x000F4528
		internal static object EditionGetter(IPropertyBag propertyBag)
		{
			string serverTypeInAD = (string)propertyBag[ServerSchema.ServerType];
			return ServerEdition.DecryptServerEdition(serverTypeInAD);
		}

		// Token: 0x06003F62 RID: 16226 RVA: 0x000F6354 File Offset: 0x000F4554
		internal static void EditionSetter(object value, IPropertyBag propertyBag)
		{
			ServerEditionType edition = (ServerEditionType)value;
			propertyBag[ServerSchema.ServerType] = ServerEdition.EncryptServerEdition(edition);
		}

		// Token: 0x06003F63 RID: 16227 RVA: 0x000F637C File Offset: 0x000F457C
		internal static object AdminDisplayVersionGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[ServerSchema.SerialNumber];
			if (string.IsNullOrEmpty(text))
			{
				InvalidOperationException ex = new InvalidOperationException(DirectoryStrings.SerialNumberMissing);
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("AdminDisplayVersion", ex.Message), ServerSchema.AdminDisplayVersion, string.Empty), ex);
			}
			object result;
			try
			{
				result = ServerVersion.ParseFromSerialNumber(text);
			}
			catch (FormatException ex2)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("AdminDisplayVersion", ex2.Message), ServerSchema.AdminDisplayVersion, propertyBag[ServerSchema.SerialNumber]), ex2);
			}
			return result;
		}

		// Token: 0x06003F64 RID: 16228 RVA: 0x000F6420 File Offset: 0x000F4620
		internal static void AdminDisplayVersionSetter(object value, IPropertyBag propertyBag)
		{
			ServerVersion serverVersion = (ServerVersion)value;
			propertyBag[ServerSchema.SerialNumber] = serverVersion.ToString(true);
		}

		// Token: 0x06003F65 RID: 16229 RVA: 0x000F6448 File Offset: 0x000F4648
		internal static object IsMailboxServerGetter(IPropertyBag propertyBag)
		{
			ServerRole serverRole = (ServerRole)propertyBag[ServerSchema.CurrentServerRole];
			return (serverRole & ServerRole.Mailbox) == ServerRole.Mailbox;
		}

		// Token: 0x06003F66 RID: 16230 RVA: 0x000F6474 File Offset: 0x000F4674
		internal static void IsMailboxServerSetter(object value, IPropertyBag propertyBag)
		{
			if ((bool)value)
			{
				propertyBag[ServerSchema.CurrentServerRole] = ((ServerRole)propertyBag[ServerSchema.CurrentServerRole] & ~ServerRole.ProvisionedServer);
				propertyBag[ServerSchema.CurrentServerRole] = ((ServerRole)propertyBag[ServerSchema.CurrentServerRole] | ServerRole.Mailbox);
				return;
			}
			propertyBag[ServerSchema.CurrentServerRole] = ((ServerRole)propertyBag[ServerSchema.CurrentServerRole] & ~ServerRole.Mailbox);
		}

		// Token: 0x06003F67 RID: 16231 RVA: 0x000F64F8 File Offset: 0x000F46F8
		internal static object IsClientAccessServerGetter(IPropertyBag propertyBag)
		{
			ServerRole serverRole = (ServerRole)propertyBag[ServerSchema.CurrentServerRole];
			return (serverRole & ServerRole.ClientAccess) == ServerRole.ClientAccess;
		}

		// Token: 0x06003F68 RID: 16232 RVA: 0x000F6524 File Offset: 0x000F4724
		internal static void IsClientAccessServerSetter(object value, IPropertyBag propertyBag)
		{
			if ((bool)value)
			{
				propertyBag[ServerSchema.CurrentServerRole] = ((ServerRole)propertyBag[ServerSchema.CurrentServerRole] & ~ServerRole.ProvisionedServer);
				propertyBag[ServerSchema.CurrentServerRole] = ((ServerRole)propertyBag[ServerSchema.CurrentServerRole] | ServerRole.ClientAccess);
				return;
			}
			propertyBag[ServerSchema.CurrentServerRole] = ((ServerRole)propertyBag[ServerSchema.CurrentServerRole] & ~ServerRole.ClientAccess);
		}

		// Token: 0x06003F69 RID: 16233 RVA: 0x000F65A8 File Offset: 0x000F47A8
		internal static object IsHubTransportServerGetter(IPropertyBag propertyBag)
		{
			ServerRole serverRole = (ServerRole)propertyBag[ServerSchema.CurrentServerRole];
			return (serverRole & ServerRole.HubTransport) == ServerRole.HubTransport;
		}

		// Token: 0x06003F6A RID: 16234 RVA: 0x000F65D4 File Offset: 0x000F47D4
		internal static void IsHubTransportServerSetter(object value, IPropertyBag propertyBag)
		{
			if ((bool)value)
			{
				propertyBag[ServerSchema.CurrentServerRole] = ((ServerRole)propertyBag[ServerSchema.CurrentServerRole] & ~ServerRole.ProvisionedServer);
				propertyBag[ServerSchema.CurrentServerRole] = ((ServerRole)propertyBag[ServerSchema.CurrentServerRole] | ServerRole.HubTransport);
				return;
			}
			propertyBag[ServerSchema.CurrentServerRole] = ((ServerRole)propertyBag[ServerSchema.CurrentServerRole] & ~ServerRole.HubTransport);
		}

		// Token: 0x06003F6B RID: 16235 RVA: 0x000F6658 File Offset: 0x000F4858
		internal static object IsUnifiedMessagingServerGetter(IPropertyBag propertyBag)
		{
			ServerRole serverRole = (ServerRole)propertyBag[ServerSchema.CurrentServerRole];
			return (serverRole & ServerRole.UnifiedMessaging) == ServerRole.UnifiedMessaging;
		}

		// Token: 0x06003F6C RID: 16236 RVA: 0x000F6684 File Offset: 0x000F4884
		internal static void IsUnifiedMessagingServerSetter(object value, IPropertyBag propertyBag)
		{
			if ((bool)value)
			{
				propertyBag[ServerSchema.CurrentServerRole] = ((ServerRole)propertyBag[ServerSchema.CurrentServerRole] & ~ServerRole.ProvisionedServer);
				propertyBag[ServerSchema.CurrentServerRole] = ((ServerRole)propertyBag[ServerSchema.CurrentServerRole] | ServerRole.UnifiedMessaging);
				return;
			}
			propertyBag[ServerSchema.CurrentServerRole] = ((ServerRole)propertyBag[ServerSchema.CurrentServerRole] & ~ServerRole.UnifiedMessaging);
		}

		// Token: 0x06003F6D RID: 16237 RVA: 0x000F6708 File Offset: 0x000F4908
		internal static object IsEdgeServerGetter(IPropertyBag propertyBag)
		{
			ServerRole serverRole = (ServerRole)propertyBag[ServerSchema.CurrentServerRole];
			return (serverRole & ServerRole.Edge) == ServerRole.Edge;
		}

		// Token: 0x06003F6E RID: 16238 RVA: 0x000F6734 File Offset: 0x000F4934
		internal static void IsEdgeServerSetter(object value, IPropertyBag propertyBag)
		{
			if ((bool)value)
			{
				propertyBag[ServerSchema.CurrentServerRole] = ((ServerRole)propertyBag[ServerSchema.CurrentServerRole] & ~ServerRole.ProvisionedServer);
				propertyBag[ServerSchema.CurrentServerRole] = ((ServerRole)propertyBag[ServerSchema.CurrentServerRole] | ServerRole.Edge);
				return;
			}
			propertyBag[ServerSchema.CurrentServerRole] = ((ServerRole)propertyBag[ServerSchema.CurrentServerRole] & ~ServerRole.Edge);
		}

		// Token: 0x06003F6F RID: 16239 RVA: 0x000F67B8 File Offset: 0x000F49B8
		internal static object IsProvisionedServerGetter(IPropertyBag propertyBag)
		{
			ServerRole serverRole = (ServerRole)propertyBag[ServerSchema.CurrentServerRole];
			return (serverRole & ServerRole.ProvisionedServer) == ServerRole.ProvisionedServer;
		}

		// Token: 0x06003F70 RID: 16240 RVA: 0x000F67EC File Offset: 0x000F49EC
		internal static void IsProvisionedServerSetter(object value, IPropertyBag propertyBag)
		{
			if ((bool)value)
			{
				propertyBag[ServerSchema.CurrentServerRole] = ((ServerRole)propertyBag[ServerSchema.CurrentServerRole] | ServerRole.ProvisionedServer);
				return;
			}
			propertyBag[ServerSchema.CurrentServerRole] = ((ServerRole)propertyBag[ServerSchema.CurrentServerRole] & ~ServerRole.ProvisionedServer);
		}

		// Token: 0x06003F71 RID: 16241 RVA: 0x000F684E File Offset: 0x000F4A4E
		internal static QueryFilter CafeServerRoleFlagFilterBuilder(SinglePropertyFilter filter)
		{
			return ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(ServerSchema.CurrentServerRole, 1UL));
		}

		// Token: 0x06003F72 RID: 16242 RVA: 0x000F6862 File Offset: 0x000F4A62
		internal static QueryFilter MailboxServerRoleFlagFilterBuilder(SinglePropertyFilter filter)
		{
			return ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(ServerSchema.CurrentServerRole, 2UL));
		}

		// Token: 0x06003F73 RID: 16243 RVA: 0x000F6878 File Offset: 0x000F4A78
		internal static object IsCafeServerGetter(IPropertyBag propertyBag)
		{
			ServerRole serverRole = (ServerRole)propertyBag[ServerSchema.CurrentServerRole];
			return (serverRole & ServerRole.Cafe) == ServerRole.Cafe;
		}

		// Token: 0x06003F74 RID: 16244 RVA: 0x000F68A4 File Offset: 0x000F4AA4
		internal static void IsCafeServerSetter(object value, IPropertyBag propertyBag)
		{
			if ((bool)value)
			{
				propertyBag[ServerSchema.CurrentServerRole] = ((ServerRole)propertyBag[ServerSchema.CurrentServerRole] & ~ServerRole.ProvisionedServer);
				propertyBag[ServerSchema.CurrentServerRole] = ((ServerRole)propertyBag[ServerSchema.CurrentServerRole] | ServerRole.Cafe);
				return;
			}
			propertyBag[ServerSchema.CurrentServerRole] = ((ServerRole)propertyBag[ServerSchema.CurrentServerRole] & ~ServerRole.Cafe);
		}

		// Token: 0x06003F75 RID: 16245 RVA: 0x000F6928 File Offset: 0x000F4B28
		internal static object IsFrontendTransportServerGetter(IPropertyBag propertyBag)
		{
			ServerRole serverRole = (ServerRole)propertyBag[ServerSchema.CurrentServerRole];
			return (serverRole & ServerRole.FrontendTransport) == ServerRole.FrontendTransport;
		}

		// Token: 0x06003F76 RID: 16246 RVA: 0x000F695C File Offset: 0x000F4B5C
		internal static void IsFrontendTransportServerSetter(object value, IPropertyBag propertyBag)
		{
			if ((bool)value)
			{
				propertyBag[ServerSchema.CurrentServerRole] = ((ServerRole)propertyBag[ServerSchema.CurrentServerRole] & ~ServerRole.ProvisionedServer);
				propertyBag[ServerSchema.CurrentServerRole] = ((ServerRole)propertyBag[ServerSchema.CurrentServerRole] | ServerRole.FrontendTransport);
				return;
			}
			propertyBag[ServerSchema.CurrentServerRole] = ((ServerRole)propertyBag[ServerSchema.CurrentServerRole] & ~ServerRole.FrontendTransport);
		}

		// Token: 0x06003F77 RID: 16247 RVA: 0x000F69E4 File Offset: 0x000F4BE4
		internal static object EmptyDomainAllowedGetter(IPropertyBag propertyBag)
		{
			return !(bool)Server.IsMailboxServerGetter(propertyBag) && !(bool)Server.IsClientAccessServerGetter(propertyBag) && !(bool)Server.IsHubTransportServerGetter(propertyBag) && !(bool)Server.IsUnifiedMessagingServerGetter(propertyBag) && !(bool)Server.IsCafeServerGetter(propertyBag) && !(bool)Server.IsFrontendTransportServerGetter(propertyBag);
		}

		// Token: 0x06003F78 RID: 16248 RVA: 0x000F6A48 File Offset: 0x000F4C48
		internal static object IsExchangeTrialEditionGetter(IPropertyBag propertyBag)
		{
			if (!(bool)propertyBag[ServerSchema.IsExchange2007OrLater])
			{
				return false;
			}
			string value = (string)propertyBag[ServerSchema.ProductID];
			if (string.IsNullOrEmpty(value))
			{
				return true;
			}
			switch ((ServerEditionType)propertyBag[ServerSchema.Edition])
			{
			case ServerEditionType.Standard:
			case ServerEditionType.Enterprise:
			case ServerEditionType.Coexistence:
				return false;
			}
			return true;
		}

		// Token: 0x06003F79 RID: 16249 RVA: 0x000F6AD4 File Offset: 0x000F4CD4
		internal static object IsExpiredExchangeTrialEditionGetter(IPropertyBag propertyBag)
		{
			if (!(bool)propertyBag[ServerSchema.IsExchangeTrialEdition])
			{
				return false;
			}
			EnhancedTimeSpan t = (EnhancedTimeSpan)propertyBag[ServerSchema.RemainingTrialPeriod];
			return t <= EnhancedTimeSpan.Zero;
		}

		// Token: 0x06003F7A RID: 16250 RVA: 0x000F6B20 File Offset: 0x000F4D20
		internal static object RemainingTrialPeriodGetter(IPropertyBag propertyBag)
		{
			EnhancedTimeSpan enhancedTimeSpan = EnhancedTimeSpan.Zero;
			bool flag = (bool)propertyBag[ServerSchema.IsExchangeTrialEdition];
			if (flag)
			{
				DateTime? dateTime = (DateTime?)ADObject.WhenCreatedUTCGetter(propertyBag);
				if (dateTime != null)
				{
					bool flag2 = (bool)propertyBag[ServerSchema.IsE15OrLater];
					if (flag2)
					{
						enhancedTimeSpan = dateTime.Value.Add(Server.E15TrialEditionExpirationPeriod) - DateTime.UtcNow;
					}
					else
					{
						enhancedTimeSpan = dateTime.Value.Add(Server.Exchange2007TrialEditionExpirationPeriod) - DateTime.UtcNow;
					}
					if (enhancedTimeSpan < EnhancedTimeSpan.Zero)
					{
						enhancedTimeSpan = EnhancedTimeSpan.Zero;
					}
				}
			}
			return enhancedTimeSpan;
		}

		// Token: 0x06003F7B RID: 16251 RVA: 0x000F6BE4 File Offset: 0x000F4DE4
		internal static object ExternalDNSServersGetter(IPropertyBag propertyBag)
		{
			List<IPAddress> list = Server.ParseStringForAddresses((string)propertyBag[ServerSchema.ExternalDNSServersStr]);
			if (list.Count > 0)
			{
				return new MultiValuedProperty<IPAddress>(false, null, list);
			}
			return new MultiValuedProperty<IPAddress>();
		}

		// Token: 0x06003F7C RID: 16252 RVA: 0x000F6C20 File Offset: 0x000F4E20
		internal static List<IPAddress> ParseStringForAddresses(string addressString)
		{
			List<IPAddress> list = new List<IPAddress>();
			if (!string.IsNullOrEmpty(addressString))
			{
				char[] separator = new char[]
				{
					',',
					';'
				};
				string[] array = addressString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
				if (array != null && array.Length > 0)
				{
					foreach (string ipString in array)
					{
						IPAddress item;
						if (IPAddress.TryParse(ipString, out item))
						{
							list.Add(item);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06003F7D RID: 16253 RVA: 0x000F6C93 File Offset: 0x000F4E93
		internal static void ExternalDNSServersSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ServerSchema.ExternalDNSServersStr] = Server.FormatAddressesToString((MultiValuedProperty<IPAddress>)value);
		}

		// Token: 0x06003F7E RID: 16254 RVA: 0x000F6CAC File Offset: 0x000F4EAC
		internal static string FormatAddressesToString(MultiValuedProperty<IPAddress> addresses)
		{
			if (addresses == null || addresses.Count == 0)
			{
				return null;
			}
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (IPAddress ipaddress in addresses)
			{
				num++;
				stringBuilder.Append(ipaddress.ToString());
				if (num < addresses.Count)
				{
					stringBuilder.Append(',');
				}
				else
				{
					stringBuilder.Append(';');
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003F7F RID: 16255 RVA: 0x000F6D3C File Offset: 0x000F4F3C
		private static string GetDomainOrComputerName(IPropertyBag propertyBag)
		{
			string text = (string)Server.DomainGetter(propertyBag);
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			text = (string)Server.FqdnGetter(propertyBag);
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			return "unknowndomain";
		}

		// Token: 0x06003F80 RID: 16256 RVA: 0x000F6DB8 File Offset: 0x000F4FB8
		internal static GetterDelegate MailboxRoleFlagsGetter(ProviderPropertyDefinition propertyDefinition, MailboxServerRoleFlags mask)
		{
			return delegate(IPropertyBag bag)
			{
				MailboxServerRoleFlags mailboxServerRoleFlags = (MailboxServerRoleFlags)bag[propertyDefinition];
				return (mailboxServerRoleFlags & mask) == mask;
			};
		}

		// Token: 0x06003F81 RID: 16257 RVA: 0x000F6E3C File Offset: 0x000F503C
		internal static SetterDelegate MailboxRoleFlagsSetter(ProviderPropertyDefinition propertyDefinition, MailboxServerRoleFlags mask)
		{
			return delegate(object value, IPropertyBag bag)
			{
				MailboxServerRoleFlags mailboxServerRoleFlags = (MailboxServerRoleFlags)bag[propertyDefinition];
				bag[propertyDefinition] = (((bool)value) ? (mailboxServerRoleFlags | mask) : (mailboxServerRoleFlags & ~mask));
			};
		}

		// Token: 0x06003F82 RID: 16258 RVA: 0x000F6E6C File Offset: 0x000F506C
		internal static object IPAddressFamilyGetter(IPropertyBag propertyBag)
		{
			UMServerSetFlags umserverSetFlags = (UMServerSetFlags)propertyBag[ActiveDirectoryServerSchema.UMServerSet];
			bool flag = (umserverSetFlags & UMServerSetFlags.IPv4Enabled) == UMServerSetFlags.IPv4Enabled;
			bool flag2 = (umserverSetFlags & UMServerSetFlags.IPv6Enabled) == UMServerSetFlags.IPv6Enabled;
			if (flag && flag2)
			{
				return IPAddressFamily.Any;
			}
			if (flag2)
			{
				return IPAddressFamily.IPv6Only;
			}
			if (flag)
			{
				return IPAddressFamily.IPv4Only;
			}
			ExAssert.RetailAssert(false, "At least one of UMServerSChema IPv4Enabled and IPv6Enabled must be set");
			return (IPAddressFamily)(-1);
		}

		// Token: 0x06003F83 RID: 16259 RVA: 0x000F6ECC File Offset: 0x000F50CC
		internal static void IPAddressFamilySetter(object value, IPropertyBag propertyBag)
		{
			UMServerSetFlags umserverSetFlags = (UMServerSetFlags)propertyBag[ActiveDirectoryServerSchema.UMServerSet];
			IPAddressFamily ipaddressFamily = (IPAddressFamily)value;
			if (ipaddressFamily == IPAddressFamily.Any)
			{
				umserverSetFlags |= UMServerSetFlags.IPv4Enabled;
				umserverSetFlags |= UMServerSetFlags.IPv6Enabled;
			}
			else if (ipaddressFamily == IPAddressFamily.IPv6Only)
			{
				umserverSetFlags &= ~UMServerSetFlags.IPv4Enabled;
				umserverSetFlags |= UMServerSetFlags.IPv6Enabled;
			}
			else if (ipaddressFamily == IPAddressFamily.IPv4Only)
			{
				umserverSetFlags |= UMServerSetFlags.IPv4Enabled;
				umserverSetFlags &= ~UMServerSetFlags.IPv6Enabled;
			}
			else
			{
				ExAssert.RetailAssert(false, "IPAddressFamily set value must be Any, IPv6Only, or IPv4Only");
			}
			propertyBag[ActiveDirectoryServerSchema.UMServerSet] = (int)umserverSetFlags;
		}

		// Token: 0x17001441 RID: 5185
		// (get) Token: 0x06003F84 RID: 16260 RVA: 0x000F6F38 File Offset: 0x000F5138
		// (set) Token: 0x06003F85 RID: 16261 RVA: 0x000F6F4A File Offset: 0x000F514A
		public string ExchangeLegacyDN
		{
			get
			{
				return (string)this[ServerSchema.ExchangeLegacyDN];
			}
			internal set
			{
				this[ServerSchema.ExchangeLegacyDN] = value;
			}
		}

		// Token: 0x17001442 RID: 5186
		// (get) Token: 0x06003F86 RID: 16262 RVA: 0x000F6F58 File Offset: 0x000F5158
		public ADObjectId ResponsibleMTA
		{
			get
			{
				return (ADObjectId)this[ServerSchema.ResponsibleMTA];
			}
		}

		// Token: 0x17001443 RID: 5187
		// (get) Token: 0x06003F87 RID: 16263 RVA: 0x000F6F6A File Offset: 0x000F516A
		// (set) Token: 0x06003F88 RID: 16264 RVA: 0x000F6F7C File Offset: 0x000F517C
		public int Heuristics
		{
			get
			{
				return (int)this[ServerSchema.Heuristics];
			}
			internal set
			{
				this[ServerSchema.Heuristics] = value;
			}
		}

		// Token: 0x17001444 RID: 5188
		// (get) Token: 0x06003F89 RID: 16265 RVA: 0x000F6F8F File Offset: 0x000F518F
		// (set) Token: 0x06003F8A RID: 16266 RVA: 0x000F6FA1 File Offset: 0x000F51A1
		public ADObjectId HomeRoutingGroup
		{
			get
			{
				return (ADObjectId)this[ServerSchema.HomeRoutingGroup];
			}
			internal set
			{
				this[ServerSchema.HomeRoutingGroup] = value;
			}
		}

		// Token: 0x17001445 RID: 5189
		// (get) Token: 0x06003F8B RID: 16267 RVA: 0x000F6FAF File Offset: 0x000F51AF
		// (set) Token: 0x06003F8C RID: 16268 RVA: 0x000F6FC1 File Offset: 0x000F51C1
		public NetworkAddressCollection NetworkAddress
		{
			get
			{
				return (NetworkAddressCollection)this[ServerSchema.NetworkAddress];
			}
			internal set
			{
				this[ServerSchema.NetworkAddress] = value;
			}
		}

		// Token: 0x17001446 RID: 5190
		// (get) Token: 0x06003F8D RID: 16269 RVA: 0x000F6FCF File Offset: 0x000F51CF
		// (set) Token: 0x06003F8E RID: 16270 RVA: 0x000F6FE1 File Offset: 0x000F51E1
		public MultiValuedProperty<byte[]> EdgeSyncCredentials
		{
			get
			{
				return (MultiValuedProperty<byte[]>)this[ServerSchema.EdgeSyncCredentials];
			}
			internal set
			{
				this[ServerSchema.EdgeSyncCredentials] = value;
			}
		}

		// Token: 0x17001447 RID: 5191
		// (get) Token: 0x06003F8F RID: 16271 RVA: 0x000F6FEF File Offset: 0x000F51EF
		// (set) Token: 0x06003F90 RID: 16272 RVA: 0x000F7001 File Offset: 0x000F5201
		public MultiValuedProperty<string> EdgeSyncStatus
		{
			get
			{
				return (MultiValuedProperty<string>)this[ServerSchema.EdgeSyncStatus];
			}
			internal set
			{
				this[ServerSchema.EdgeSyncStatus] = value;
			}
		}

		// Token: 0x17001448 RID: 5192
		// (get) Token: 0x06003F91 RID: 16273 RVA: 0x000F700F File Offset: 0x000F520F
		// (set) Token: 0x06003F92 RID: 16274 RVA: 0x000F7021 File Offset: 0x000F5221
		public MultiValuedProperty<byte[]> EdgeSyncCookies
		{
			get
			{
				return (MultiValuedProperty<byte[]>)this[ServerSchema.EdgeSyncCookies];
			}
			internal set
			{
				this[ServerSchema.EdgeSyncCookies] = value;
			}
		}

		// Token: 0x17001449 RID: 5193
		// (get) Token: 0x06003F93 RID: 16275 RVA: 0x000F702F File Offset: 0x000F522F
		// (set) Token: 0x06003F94 RID: 16276 RVA: 0x000F7041 File Offset: 0x000F5241
		public int EdgeSyncAdamSslPort
		{
			get
			{
				return (int)this[ActiveDirectoryServerSchema.EdgeSyncAdamSslPort];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.EdgeSyncAdamSslPort] = value;
			}
		}

		// Token: 0x1700144A RID: 5194
		// (get) Token: 0x06003F95 RID: 16277 RVA: 0x000F7054 File Offset: 0x000F5254
		// (set) Token: 0x06003F96 RID: 16278 RVA: 0x000F7066 File Offset: 0x000F5266
		public byte[] InternalTransportCertificate
		{
			get
			{
				return (byte[])this[ServerSchema.InternalTransportCertificate];
			}
			internal set
			{
				this[ServerSchema.InternalTransportCertificate] = value;
			}
		}

		// Token: 0x1700144B RID: 5195
		// (get) Token: 0x06003F97 RID: 16279 RVA: 0x000F7074 File Offset: 0x000F5274
		// (set) Token: 0x06003F98 RID: 16280 RVA: 0x000F7086 File Offset: 0x000F5286
		public byte[] EdgeSyncSourceGuid
		{
			get
			{
				return (byte[])this[ServerSchema.EdgeSyncSourceGuid];
			}
			internal set
			{
				this[ServerSchema.EdgeSyncSourceGuid] = value;
			}
		}

		// Token: 0x1700144C RID: 5196
		// (get) Token: 0x06003F99 RID: 16281 RVA: 0x000F7094 File Offset: 0x000F5294
		public string InternalTransportCertificateThumbprint
		{
			get
			{
				return (string)this[ServerSchema.InternalTransportCertificateThumbprint];
			}
		}

		// Token: 0x1700144D RID: 5197
		// (get) Token: 0x06003F9A RID: 16282 RVA: 0x000F70A6 File Offset: 0x000F52A6
		// (set) Token: 0x06003F9B RID: 16283 RVA: 0x000F70B8 File Offset: 0x000F52B8
		public MultiValuedProperty<string> ComponentStates
		{
			get
			{
				return (MultiValuedProperty<string>)this[ServerSchema.ComponentStates];
			}
			internal set
			{
				this[ServerSchema.ComponentStates] = value;
			}
		}

		// Token: 0x1700144E RID: 5198
		// (get) Token: 0x06003F9C RID: 16284 RVA: 0x000F70C6 File Offset: 0x000F52C6
		// (set) Token: 0x06003F9D RID: 16285 RVA: 0x000F70D8 File Offset: 0x000F52D8
		public string MonitoringGroup
		{
			get
			{
				return (string)this[ServerSchema.MonitoringGroup];
			}
			set
			{
				this[ServerSchema.MonitoringGroup] = value;
			}
		}

		// Token: 0x1700144F RID: 5199
		// (get) Token: 0x06003F9E RID: 16286 RVA: 0x000F70E6 File Offset: 0x000F52E6
		// (set) Token: 0x06003F9F RID: 16287 RVA: 0x000F70F8 File Offset: 0x000F52F8
		public byte[] EdgeSyncLease
		{
			get
			{
				return (byte[])this[ServerSchema.EdgeSyncLease];
			}
			internal set
			{
				this[ServerSchema.EdgeSyncLease] = value;
			}
		}

		// Token: 0x17001450 RID: 5200
		// (get) Token: 0x06003FA0 RID: 16288 RVA: 0x000F7106 File Offset: 0x000F5306
		// (set) Token: 0x06003FA1 RID: 16289 RVA: 0x000F7118 File Offset: 0x000F5318
		public string ServerType
		{
			get
			{
				return (string)this[ServerSchema.ServerType];
			}
			internal set
			{
				this[ServerSchema.ServerType] = value;
			}
		}

		// Token: 0x17001451 RID: 5201
		// (get) Token: 0x06003FA2 RID: 16290 RVA: 0x000F7126 File Offset: 0x000F5326
		// (set) Token: 0x06003FA3 RID: 16291 RVA: 0x000F7138 File Offset: 0x000F5338
		public bool IsMailboxServer
		{
			get
			{
				return (bool)this[ServerSchema.IsMailboxServer];
			}
			internal set
			{
				this[ServerSchema.IsMailboxServer] = value;
			}
		}

		// Token: 0x17001452 RID: 5202
		// (get) Token: 0x06003FA4 RID: 16292 RVA: 0x000F714B File Offset: 0x000F534B
		// (set) Token: 0x06003FA5 RID: 16293 RVA: 0x000F715D File Offset: 0x000F535D
		public bool? CustomerFeedbackEnabled
		{
			get
			{
				return (bool?)this[ServerSchema.CustomerFeedbackEnabled];
			}
			set
			{
				this[ServerSchema.CustomerFeedbackEnabled] = value;
			}
		}

		// Token: 0x17001453 RID: 5203
		// (get) Token: 0x06003FA6 RID: 16294 RVA: 0x000F7170 File Offset: 0x000F5370
		// (set) Token: 0x06003FA7 RID: 16295 RVA: 0x000F7182 File Offset: 0x000F5382
		public Uri InternetWebProxy
		{
			get
			{
				return (Uri)this[ServerSchema.InternetWebProxy];
			}
			set
			{
				this[ServerSchema.InternetWebProxy] = value;
			}
		}

		// Token: 0x17001454 RID: 5204
		// (get) Token: 0x06003FA8 RID: 16296 RVA: 0x000F7190 File Offset: 0x000F5390
		// (set) Token: 0x06003FA9 RID: 16297 RVA: 0x000F71A2 File Offset: 0x000F53A2
		public bool IsClientAccessServer
		{
			get
			{
				return (bool)this[ServerSchema.IsClientAccessServer];
			}
			internal set
			{
				this[ServerSchema.IsClientAccessServer] = value;
			}
		}

		// Token: 0x17001455 RID: 5205
		// (get) Token: 0x06003FAA RID: 16298 RVA: 0x000F71B5 File Offset: 0x000F53B5
		// (set) Token: 0x06003FAB RID: 16299 RVA: 0x000F71C7 File Offset: 0x000F53C7
		public bool IsUnifiedMessagingServer
		{
			get
			{
				return (bool)this[ServerSchema.IsUnifiedMessagingServer];
			}
			internal set
			{
				this[ServerSchema.IsUnifiedMessagingServer] = value;
			}
		}

		// Token: 0x17001456 RID: 5206
		// (get) Token: 0x06003FAC RID: 16300 RVA: 0x000F71DA File Offset: 0x000F53DA
		// (set) Token: 0x06003FAD RID: 16301 RVA: 0x000F71EC File Offset: 0x000F53EC
		public bool IsHubTransportServer
		{
			get
			{
				return (bool)this[ServerSchema.IsHubTransportServer];
			}
			internal set
			{
				this[ServerSchema.IsHubTransportServer] = value;
			}
		}

		// Token: 0x17001457 RID: 5207
		// (get) Token: 0x06003FAE RID: 16302 RVA: 0x000F71FF File Offset: 0x000F53FF
		// (set) Token: 0x06003FAF RID: 16303 RVA: 0x000F7211 File Offset: 0x000F5411
		public bool IsEdgeServer
		{
			get
			{
				return (bool)this[ServerSchema.IsEdgeServer];
			}
			internal set
			{
				this[ServerSchema.IsEdgeServer] = value;
			}
		}

		// Token: 0x17001458 RID: 5208
		// (get) Token: 0x06003FB0 RID: 16304 RVA: 0x000F7224 File Offset: 0x000F5424
		// (set) Token: 0x06003FB1 RID: 16305 RVA: 0x000F7236 File Offset: 0x000F5436
		public bool IsCafeServer
		{
			get
			{
				return (bool)this[ServerSchema.IsCafeServer];
			}
			internal set
			{
				this[ServerSchema.IsCafeServer] = value;
			}
		}

		// Token: 0x17001459 RID: 5209
		// (get) Token: 0x06003FB2 RID: 16306 RVA: 0x000F7249 File Offset: 0x000F5449
		// (set) Token: 0x06003FB3 RID: 16307 RVA: 0x000F725B File Offset: 0x000F545B
		public bool IsFrontendTransportServer
		{
			get
			{
				return (bool)this[ServerSchema.IsFrontendTransportServer];
			}
			internal set
			{
				this[ServerSchema.IsFrontendTransportServer] = value;
			}
		}

		// Token: 0x1700145A RID: 5210
		// (get) Token: 0x06003FB4 RID: 16308 RVA: 0x000F726E File Offset: 0x000F546E
		// (set) Token: 0x06003FB5 RID: 16309 RVA: 0x000F7280 File Offset: 0x000F5480
		public bool IsPhoneticSupportEnabled
		{
			get
			{
				return (bool)this[ServerSchema.IsPhoneticSupportEnabled];
			}
			internal set
			{
				this[ServerSchema.IsPhoneticSupportEnabled] = value;
			}
		}

		// Token: 0x1700145B RID: 5211
		// (get) Token: 0x06003FB6 RID: 16310 RVA: 0x000F7293 File Offset: 0x000F5493
		// (set) Token: 0x06003FB7 RID: 16311 RVA: 0x000F72A5 File Offset: 0x000F54A5
		public bool EmptyDomainAllowed
		{
			get
			{
				return (bool)this[ServerSchema.EmptyDomainAllowed];
			}
			internal set
			{
				this[ServerSchema.EmptyDomainAllowed] = value;
			}
		}

		// Token: 0x1700145C RID: 5212
		// (get) Token: 0x06003FB8 RID: 16312 RVA: 0x000F72B8 File Offset: 0x000F54B8
		// (set) Token: 0x06003FB9 RID: 16313 RVA: 0x000F72CA File Offset: 0x000F54CA
		public ServerVersion AdminDisplayVersion
		{
			get
			{
				return (ServerVersion)this[ServerSchema.AdminDisplayVersion];
			}
			internal set
			{
				this[ServerSchema.AdminDisplayVersion] = value;
			}
		}

		// Token: 0x1700145D RID: 5213
		// (get) Token: 0x06003FBA RID: 16314 RVA: 0x000F72D8 File Offset: 0x000F54D8
		public string Domain
		{
			get
			{
				return (string)this[ServerSchema.Domain];
			}
		}

		// Token: 0x1700145E RID: 5214
		// (get) Token: 0x06003FBB RID: 16315 RVA: 0x000F72EA File Offset: 0x000F54EA
		public string OrganizationalUnit
		{
			get
			{
				return (string)this[ServerSchema.OrganizationalUnit];
			}
		}

		// Token: 0x1700145F RID: 5215
		// (get) Token: 0x06003FBC RID: 16316 RVA: 0x000F72FC File Offset: 0x000F54FC
		// (set) Token: 0x06003FBD RID: 16317 RVA: 0x000F730E File Offset: 0x000F550E
		public ServerRole CurrentServerRole
		{
			get
			{
				return (ServerRole)this[ServerSchema.CurrentServerRole];
			}
			internal set
			{
				this[ServerSchema.CurrentServerRole] = value;
			}
		}

		// Token: 0x17001460 RID: 5216
		// (get) Token: 0x06003FBE RID: 16318 RVA: 0x000F7321 File Offset: 0x000F5521
		public string SerialNumber
		{
			get
			{
				return (string)this.propertyBag[ServerSchema.SerialNumber];
			}
		}

		// Token: 0x17001461 RID: 5217
		// (get) Token: 0x06003FBF RID: 16319 RVA: 0x000F7338 File Offset: 0x000F5538
		// (set) Token: 0x06003FC0 RID: 16320 RVA: 0x000F734A File Offset: 0x000F554A
		public int VersionNumber
		{
			get
			{
				return (int)this[ServerSchema.VersionNumber];
			}
			internal set
			{
				this[ServerSchema.VersionNumber] = value;
			}
		}

		// Token: 0x17001462 RID: 5218
		// (get) Token: 0x06003FC1 RID: 16321 RVA: 0x000F735D File Offset: 0x000F555D
		public int MajorVersion
		{
			get
			{
				return (int)this[ServerSchema.MajorVersion];
			}
		}

		// Token: 0x17001463 RID: 5219
		// (get) Token: 0x06003FC2 RID: 16322 RVA: 0x000F736F File Offset: 0x000F556F
		// (set) Token: 0x06003FC3 RID: 16323 RVA: 0x000F7381 File Offset: 0x000F5581
		public LocalLongFullPath DataPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.DataPath];
			}
			internal set
			{
				this[ServerSchema.DataPath] = value;
			}
		}

		// Token: 0x17001464 RID: 5220
		// (get) Token: 0x06003FC4 RID: 16324 RVA: 0x000F738F File Offset: 0x000F558F
		public LocalLongFullPath InstallPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.InstallPath];
			}
		}

		// Token: 0x17001465 RID: 5221
		// (get) Token: 0x06003FC5 RID: 16325 RVA: 0x000F73A1 File Offset: 0x000F55A1
		// (set) Token: 0x06003FC6 RID: 16326 RVA: 0x000F73B3 File Offset: 0x000F55B3
		public ServerEditionType Edition
		{
			get
			{
				return (ServerEditionType)this[ServerSchema.Edition];
			}
			internal set
			{
				this[ServerSchema.Edition] = value;
			}
		}

		// Token: 0x17001466 RID: 5222
		// (get) Token: 0x06003FC7 RID: 16327 RVA: 0x000F73C6 File Offset: 0x000F55C6
		public bool IsPreE12FrontEnd
		{
			get
			{
				return (bool)this[ServerSchema.IsPreE12FrontEnd];
			}
		}

		// Token: 0x17001467 RID: 5223
		// (get) Token: 0x06003FC8 RID: 16328 RVA: 0x000F73D8 File Offset: 0x000F55D8
		public bool IsPreE12RPCHTTPEnabled
		{
			get
			{
				return (bool)this[ServerSchema.IsPreE12RPCHTTPEnabled];
			}
		}

		// Token: 0x17001468 RID: 5224
		// (get) Token: 0x06003FC9 RID: 16329 RVA: 0x000F73EA File Offset: 0x000F55EA
		// (set) Token: 0x06003FCA RID: 16330 RVA: 0x000F73FC File Offset: 0x000F55FC
		public bool IsProvisionedServer
		{
			get
			{
				return (bool)this[ServerSchema.IsProvisionedServer];
			}
			internal set
			{
				this[ServerSchema.IsProvisionedServer] = value;
			}
		}

		// Token: 0x17001469 RID: 5225
		// (get) Token: 0x06003FCB RID: 16331 RVA: 0x000F740F File Offset: 0x000F560F
		public bool IsExchange2003OrLater
		{
			get
			{
				return (bool)this[ServerSchema.IsExchange2003OrLater];
			}
		}

		// Token: 0x1700146A RID: 5226
		// (get) Token: 0x06003FCC RID: 16332 RVA: 0x000F7421 File Offset: 0x000F5621
		public bool IsExchange2003Sp1OrLater
		{
			get
			{
				return (bool)this[ServerSchema.IsExchange2003Sp1OrLater];
			}
		}

		// Token: 0x1700146B RID: 5227
		// (get) Token: 0x06003FCD RID: 16333 RVA: 0x000F7433 File Offset: 0x000F5633
		public bool IsExchange2003Sp2OrLater
		{
			get
			{
				return (bool)this[ServerSchema.IsExchange2003Sp2OrLater];
			}
		}

		// Token: 0x1700146C RID: 5228
		// (get) Token: 0x06003FCE RID: 16334 RVA: 0x000F7445 File Offset: 0x000F5645
		public bool IsExchange2003Sp3OrLater
		{
			get
			{
				return (bool)this[ServerSchema.IsExchange2003Sp3OrLater];
			}
		}

		// Token: 0x1700146D RID: 5229
		// (get) Token: 0x06003FCF RID: 16335 RVA: 0x000F7457 File Offset: 0x000F5657
		public bool IsExchange2007OrLater
		{
			get
			{
				return (bool)this[ServerSchema.IsExchange2007OrLater];
			}
		}

		// Token: 0x1700146E RID: 5230
		// (get) Token: 0x06003FD0 RID: 16336 RVA: 0x000F7469 File Offset: 0x000F5669
		public bool IsE14OrLater
		{
			get
			{
				return (bool)this[ServerSchema.IsE14OrLater];
			}
		}

		// Token: 0x1700146F RID: 5231
		// (get) Token: 0x06003FD1 RID: 16337 RVA: 0x000F747B File Offset: 0x000F567B
		public bool IsE14Sp1OrLater
		{
			get
			{
				return (bool)this[ServerSchema.IsE14Sp1OrLater];
			}
		}

		// Token: 0x17001470 RID: 5232
		// (get) Token: 0x06003FD2 RID: 16338 RVA: 0x000F748D File Offset: 0x000F568D
		public bool IsE15OrLater
		{
			get
			{
				return (bool)this[ServerSchema.IsE15OrLater];
			}
		}

		// Token: 0x17001471 RID: 5233
		// (get) Token: 0x06003FD3 RID: 16339 RVA: 0x000F749F File Offset: 0x000F569F
		public string Fqdn
		{
			get
			{
				return (string)this[ServerSchema.Fqdn];
			}
		}

		// Token: 0x17001472 RID: 5234
		// (get) Token: 0x06003FD4 RID: 16340 RVA: 0x000F74B1 File Offset: 0x000F56B1
		// (set) Token: 0x06003FD5 RID: 16341 RVA: 0x000F74C3 File Offset: 0x000F56C3
		public int IntraOrgConnectorSmtpMaxMessagesPerConnection
		{
			get
			{
				return (int)this[ServerSchema.IntraOrgConnectorSmtpMaxMessagesPerConnection];
			}
			internal set
			{
				this[ServerSchema.IntraOrgConnectorSmtpMaxMessagesPerConnection] = value;
			}
		}

		// Token: 0x17001473 RID: 5235
		// (get) Token: 0x06003FD6 RID: 16342 RVA: 0x000F74D6 File Offset: 0x000F56D6
		// (set) Token: 0x06003FD7 RID: 16343 RVA: 0x000F74E8 File Offset: 0x000F56E8
		public ScheduleInterval[] ManagedFolderAssistantSchedule
		{
			get
			{
				return (ScheduleInterval[])this[ServerSchema.ElcSchedule];
			}
			internal set
			{
				this[ServerSchema.ElcSchedule] = value;
			}
		}

		// Token: 0x17001474 RID: 5236
		// (get) Token: 0x06003FD8 RID: 16344 RVA: 0x000F74F6 File Offset: 0x000F56F6
		// (set) Token: 0x06003FD9 RID: 16345 RVA: 0x000F7508 File Offset: 0x000F5708
		public LocalLongFullPath LogPathForManagedFolders
		{
			get
			{
				return (LocalLongFullPath)this[ActiveDirectoryServerSchema.ElcAuditLogPath];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.ElcAuditLogPath] = value;
			}
		}

		// Token: 0x17001475 RID: 5237
		// (get) Token: 0x06003FDA RID: 16346 RVA: 0x000F7516 File Offset: 0x000F5716
		// (set) Token: 0x06003FDB RID: 16347 RVA: 0x000F7528 File Offset: 0x000F5728
		public EnhancedTimeSpan LogFileAgeLimitForManagedFolders
		{
			get
			{
				return (EnhancedTimeSpan)this[ActiveDirectoryServerSchema.ElcAuditLogFileAgeLimit];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.ElcAuditLogFileAgeLimit] = value;
			}
		}

		// Token: 0x17001476 RID: 5238
		// (get) Token: 0x06003FDC RID: 16348 RVA: 0x000F753B File Offset: 0x000F573B
		// (set) Token: 0x06003FDD RID: 16349 RVA: 0x000F754D File Offset: 0x000F574D
		public Unlimited<ByteQuantifiedSize> LogDirectorySizeLimitForManagedFolders
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ActiveDirectoryServerSchema.ElcAuditLogDirectorySizeLimit];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.ElcAuditLogDirectorySizeLimit] = value;
			}
		}

		// Token: 0x17001477 RID: 5239
		// (get) Token: 0x06003FDE RID: 16350 RVA: 0x000F7560 File Offset: 0x000F5760
		// (set) Token: 0x06003FDF RID: 16351 RVA: 0x000F7572 File Offset: 0x000F5772
		public Unlimited<ByteQuantifiedSize> LogFileSizeLimitForManagedFolders
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ActiveDirectoryServerSchema.ElcAuditLogFileSizeLimit];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.ElcAuditLogFileSizeLimit] = value;
			}
		}

		// Token: 0x17001478 RID: 5240
		// (get) Token: 0x06003FE0 RID: 16352 RVA: 0x000F7585 File Offset: 0x000F5785
		// (set) Token: 0x06003FE1 RID: 16353 RVA: 0x000F7597 File Offset: 0x000F5797
		public bool MAPIEncryptionRequired
		{
			get
			{
				return (bool)this[ActiveDirectoryServerSchema.MAPIEncryptionRequired];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.MAPIEncryptionRequired] = value;
			}
		}

		// Token: 0x17001479 RID: 5241
		// (get) Token: 0x06003FE2 RID: 16354 RVA: 0x000F75AA File Offset: 0x000F57AA
		// (set) Token: 0x06003FE3 RID: 16355 RVA: 0x000F75BC File Offset: 0x000F57BC
		public bool RetentionLogForManagedFoldersEnabled
		{
			get
			{
				return (bool)this[ActiveDirectoryServerSchema.ExpirationAuditLogEnabled];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.ExpirationAuditLogEnabled] = value;
			}
		}

		// Token: 0x1700147A RID: 5242
		// (get) Token: 0x06003FE4 RID: 16356 RVA: 0x000F75CF File Offset: 0x000F57CF
		// (set) Token: 0x06003FE5 RID: 16357 RVA: 0x000F75E1 File Offset: 0x000F57E1
		public bool JournalingLogForManagedFoldersEnabled
		{
			get
			{
				return (bool)this[ActiveDirectoryServerSchema.AutocopyAuditLogEnabled];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.AutocopyAuditLogEnabled] = value;
			}
		}

		// Token: 0x1700147B RID: 5243
		// (get) Token: 0x06003FE6 RID: 16358 RVA: 0x000F75F4 File Offset: 0x000F57F4
		// (set) Token: 0x06003FE7 RID: 16359 RVA: 0x000F7606 File Offset: 0x000F5806
		public bool FolderLogForManagedFoldersEnabled
		{
			get
			{
				return (bool)this[ActiveDirectoryServerSchema.FolderAuditLogEnabled];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.FolderAuditLogEnabled] = value;
			}
		}

		// Token: 0x1700147C RID: 5244
		// (get) Token: 0x06003FE8 RID: 16360 RVA: 0x000F7619 File Offset: 0x000F5819
		// (set) Token: 0x06003FE9 RID: 16361 RVA: 0x000F762B File Offset: 0x000F582B
		public bool SubjectLogForManagedFoldersEnabled
		{
			get
			{
				return (bool)this[ActiveDirectoryServerSchema.ElcSubjectLoggingEnabled];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.ElcSubjectLoggingEnabled] = value;
			}
		}

		// Token: 0x1700147D RID: 5245
		// (get) Token: 0x06003FEA RID: 16362 RVA: 0x000F763E File Offset: 0x000F583E
		// (set) Token: 0x06003FEB RID: 16363 RVA: 0x000F7650 File Offset: 0x000F5850
		public ScheduleInterval[] SharingPolicySchedule
		{
			get
			{
				return (ScheduleInterval[])this[ActiveDirectoryServerSchema.SharingPolicySchedule];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.SharingPolicySchedule] = value;
			}
		}

		// Token: 0x1700147E RID: 5246
		// (get) Token: 0x06003FEC RID: 16364 RVA: 0x000F765E File Offset: 0x000F585E
		// (set) Token: 0x06003FED RID: 16365 RVA: 0x000F7670 File Offset: 0x000F5870
		public bool CalendarRepairMissingItemFixDisabled
		{
			get
			{
				return (bool)this[ActiveDirectoryServerSchema.CalendarRepairMissingItemFixDisabled];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.CalendarRepairMissingItemFixDisabled] = value;
			}
		}

		// Token: 0x1700147F RID: 5247
		// (get) Token: 0x06003FEE RID: 16366 RVA: 0x000F7683 File Offset: 0x000F5883
		// (set) Token: 0x06003FEF RID: 16367 RVA: 0x000F7695 File Offset: 0x000F5895
		public bool CalendarRepairLogEnabled
		{
			get
			{
				return (bool)this[ActiveDirectoryServerSchema.CalendarRepairLogEnabled];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.CalendarRepairLogEnabled] = value;
			}
		}

		// Token: 0x17001480 RID: 5248
		// (get) Token: 0x06003FF0 RID: 16368 RVA: 0x000F76A8 File Offset: 0x000F58A8
		// (set) Token: 0x06003FF1 RID: 16369 RVA: 0x000F76BA File Offset: 0x000F58BA
		public bool CalendarRepairLogSubjectLoggingEnabled
		{
			get
			{
				return (bool)this[ActiveDirectoryServerSchema.CalendarRepairLogSubjectLoggingEnabled];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.CalendarRepairLogSubjectLoggingEnabled] = value;
			}
		}

		// Token: 0x17001481 RID: 5249
		// (get) Token: 0x06003FF2 RID: 16370 RVA: 0x000F76CD File Offset: 0x000F58CD
		// (set) Token: 0x06003FF3 RID: 16371 RVA: 0x000F76DF File Offset: 0x000F58DF
		public LocalLongFullPath CalendarRepairLogPath
		{
			get
			{
				return (LocalLongFullPath)this[ActiveDirectoryServerSchema.CalendarRepairLogPath];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.CalendarRepairLogPath] = value;
			}
		}

		// Token: 0x17001482 RID: 5250
		// (get) Token: 0x06003FF4 RID: 16372 RVA: 0x000F76ED File Offset: 0x000F58ED
		// (set) Token: 0x06003FF5 RID: 16373 RVA: 0x000F76FF File Offset: 0x000F58FF
		public int CalendarRepairIntervalEndWindow
		{
			get
			{
				return (int)this[ActiveDirectoryServerSchema.CalendarRepairIntervalEndWindow];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.CalendarRepairIntervalEndWindow] = value;
			}
		}

		// Token: 0x17001483 RID: 5251
		// (get) Token: 0x06003FF6 RID: 16374 RVA: 0x000F7712 File Offset: 0x000F5912
		// (set) Token: 0x06003FF7 RID: 16375 RVA: 0x000F7724 File Offset: 0x000F5924
		public EnhancedTimeSpan CalendarRepairLogFileAgeLimit
		{
			get
			{
				return (EnhancedTimeSpan)this[ActiveDirectoryServerSchema.CalendarRepairLogFileAgeLimit];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.CalendarRepairLogFileAgeLimit] = value;
			}
		}

		// Token: 0x17001484 RID: 5252
		// (get) Token: 0x06003FF8 RID: 16376 RVA: 0x000F7737 File Offset: 0x000F5937
		// (set) Token: 0x06003FF9 RID: 16377 RVA: 0x000F7749 File Offset: 0x000F5949
		public Unlimited<ByteQuantifiedSize> CalendarRepairLogDirectorySizeLimit
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ActiveDirectoryServerSchema.CalendarRepairLogDirectorySizeLimit];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.CalendarRepairLogDirectorySizeLimit] = value;
			}
		}

		// Token: 0x17001485 RID: 5253
		// (get) Token: 0x06003FFA RID: 16378 RVA: 0x000F775C File Offset: 0x000F595C
		// (set) Token: 0x06003FFB RID: 16379 RVA: 0x000F776E File Offset: 0x000F596E
		public CalendarRepairType CalendarRepairMode
		{
			get
			{
				return (CalendarRepairType)this[ActiveDirectoryServerSchema.CalendarRepairMode];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.CalendarRepairMode] = value;
			}
		}

		// Token: 0x17001486 RID: 5254
		// (get) Token: 0x06003FFC RID: 16380 RVA: 0x000F7781 File Offset: 0x000F5981
		// (set) Token: 0x06003FFD RID: 16381 RVA: 0x000F7793 File Offset: 0x000F5993
		public EnhancedTimeSpan? CalendarRepairWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.CalendarRepairWorkCycle];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.CalendarRepairWorkCycle] = value;
			}
		}

		// Token: 0x17001487 RID: 5255
		// (get) Token: 0x06003FFE RID: 16382 RVA: 0x000F77A6 File Offset: 0x000F59A6
		// (set) Token: 0x06003FFF RID: 16383 RVA: 0x000F77B8 File Offset: 0x000F59B8
		public EnhancedTimeSpan? CalendarRepairWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.CalendarRepairWorkCycleCheckpoint];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.CalendarRepairWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001488 RID: 5256
		// (get) Token: 0x06004000 RID: 16384 RVA: 0x000F77CB File Offset: 0x000F59CB
		// (set) Token: 0x06004001 RID: 16385 RVA: 0x000F77DD File Offset: 0x000F59DD
		public EnhancedTimeSpan? SharingPolicyWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.SharingPolicyWorkCycle];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.SharingPolicyWorkCycle] = value;
			}
		}

		// Token: 0x17001489 RID: 5257
		// (get) Token: 0x06004002 RID: 16386 RVA: 0x000F77F0 File Offset: 0x000F59F0
		// (set) Token: 0x06004003 RID: 16387 RVA: 0x000F7802 File Offset: 0x000F5A02
		public EnhancedTimeSpan? SharingPolicyWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.SharingPolicyWorkCycleCheckpoint];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.SharingPolicyWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x1700148A RID: 5258
		// (get) Token: 0x06004004 RID: 16388 RVA: 0x000F7815 File Offset: 0x000F5A15
		// (set) Token: 0x06004005 RID: 16389 RVA: 0x000F7827 File Offset: 0x000F5A27
		public EnhancedTimeSpan? PublicFolderWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.PublicFolderWorkCycle];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.PublicFolderWorkCycle] = value;
			}
		}

		// Token: 0x1700148B RID: 5259
		// (get) Token: 0x06004006 RID: 16390 RVA: 0x000F783A File Offset: 0x000F5A3A
		// (set) Token: 0x06004007 RID: 16391 RVA: 0x000F784C File Offset: 0x000F5A4C
		public EnhancedTimeSpan? PublicFolderWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.PublicFolderWorkCycleCheckpoint];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.PublicFolderWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x1700148C RID: 5260
		// (get) Token: 0x06004008 RID: 16392 RVA: 0x000F785F File Offset: 0x000F5A5F
		// (set) Token: 0x06004009 RID: 16393 RVA: 0x000F7871 File Offset: 0x000F5A71
		public EnhancedTimeSpan? SiteMailboxWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.SiteMailboxWorkCycle];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.SiteMailboxWorkCycle] = value;
			}
		}

		// Token: 0x1700148D RID: 5261
		// (get) Token: 0x0600400A RID: 16394 RVA: 0x000F7884 File Offset: 0x000F5A84
		// (set) Token: 0x0600400B RID: 16395 RVA: 0x000F7896 File Offset: 0x000F5A96
		public EnhancedTimeSpan? SiteMailboxWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.SiteMailboxWorkCycleCheckpoint];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.SiteMailboxWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x1700148E RID: 5262
		// (get) Token: 0x0600400C RID: 16396 RVA: 0x000F78A9 File Offset: 0x000F5AA9
		// (set) Token: 0x0600400D RID: 16397 RVA: 0x000F78BB File Offset: 0x000F5ABB
		public EnhancedTimeSpan? SharingSyncWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.SharingSyncWorkCycle];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.SharingSyncWorkCycle] = value;
			}
		}

		// Token: 0x1700148F RID: 5263
		// (get) Token: 0x0600400E RID: 16398 RVA: 0x000F78CE File Offset: 0x000F5ACE
		// (set) Token: 0x0600400F RID: 16399 RVA: 0x000F78E0 File Offset: 0x000F5AE0
		public EnhancedTimeSpan? SharingSyncWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.SharingSyncWorkCycleCheckpoint];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.SharingSyncWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001490 RID: 5264
		// (get) Token: 0x06004010 RID: 16400 RVA: 0x000F78F3 File Offset: 0x000F5AF3
		// (set) Token: 0x06004011 RID: 16401 RVA: 0x000F7905 File Offset: 0x000F5B05
		public EnhancedTimeSpan? ManagedFolderWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.ManagedFolderWorkCycle];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.ManagedFolderWorkCycle] = value;
			}
		}

		// Token: 0x17001491 RID: 5265
		// (get) Token: 0x06004012 RID: 16402 RVA: 0x000F7918 File Offset: 0x000F5B18
		// (set) Token: 0x06004013 RID: 16403 RVA: 0x000F792A File Offset: 0x000F5B2A
		public EnhancedTimeSpan? ManagedFolderWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.ManagedFolderWorkCycleCheckpoint];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.ManagedFolderWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001492 RID: 5266
		// (get) Token: 0x06004014 RID: 16404 RVA: 0x000F793D File Offset: 0x000F5B3D
		// (set) Token: 0x06004015 RID: 16405 RVA: 0x000F794F File Offset: 0x000F5B4F
		public EnhancedTimeSpan? MailboxAssociationReplicationWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.MailboxAssociationReplicationWorkCycle];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.MailboxAssociationReplicationWorkCycle] = value;
			}
		}

		// Token: 0x17001493 RID: 5267
		// (get) Token: 0x06004016 RID: 16406 RVA: 0x000F7962 File Offset: 0x000F5B62
		// (set) Token: 0x06004017 RID: 16407 RVA: 0x000F7974 File Offset: 0x000F5B74
		public EnhancedTimeSpan? MailboxAssociationReplicationWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.MailboxAssociationReplicationWorkCycleCheckpoint];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.MailboxAssociationReplicationWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001494 RID: 5268
		// (get) Token: 0x06004018 RID: 16408 RVA: 0x000F7987 File Offset: 0x000F5B87
		// (set) Token: 0x06004019 RID: 16409 RVA: 0x000F7999 File Offset: 0x000F5B99
		public EnhancedTimeSpan? GroupMailboxWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.GroupMailboxWorkCycle];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.GroupMailboxWorkCycle] = value;
			}
		}

		// Token: 0x17001495 RID: 5269
		// (get) Token: 0x0600401A RID: 16410 RVA: 0x000F79AC File Offset: 0x000F5BAC
		// (set) Token: 0x0600401B RID: 16411 RVA: 0x000F79BE File Offset: 0x000F5BBE
		public EnhancedTimeSpan? GroupMailboxWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.GroupMailboxWorkCycleCheckpoint];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.GroupMailboxWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001496 RID: 5270
		// (get) Token: 0x0600401C RID: 16412 RVA: 0x000F79D1 File Offset: 0x000F5BD1
		// (set) Token: 0x0600401D RID: 16413 RVA: 0x000F79E3 File Offset: 0x000F5BE3
		public EnhancedTimeSpan? TopNWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.TopNWorkCycle];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.TopNWorkCycle] = value;
			}
		}

		// Token: 0x17001497 RID: 5271
		// (get) Token: 0x0600401E RID: 16414 RVA: 0x000F79F6 File Offset: 0x000F5BF6
		// (set) Token: 0x0600401F RID: 16415 RVA: 0x000F7A08 File Offset: 0x000F5C08
		public EnhancedTimeSpan? TopNWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.TopNWorkCycleCheckpoint];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.TopNWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x17001498 RID: 5272
		// (get) Token: 0x06004020 RID: 16416 RVA: 0x000F7A1B File Offset: 0x000F5C1B
		// (set) Token: 0x06004021 RID: 16417 RVA: 0x000F7A2D File Offset: 0x000F5C2D
		public EnhancedTimeSpan? UMReportingWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.UMReportingWorkCycle];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.UMReportingWorkCycle] = value;
			}
		}

		// Token: 0x17001499 RID: 5273
		// (get) Token: 0x06004022 RID: 16418 RVA: 0x000F7A40 File Offset: 0x000F5C40
		// (set) Token: 0x06004023 RID: 16419 RVA: 0x000F7A52 File Offset: 0x000F5C52
		public EnhancedTimeSpan? UMReportingWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.UMReportingWorkCycleCheckpoint];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.UMReportingWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x1700149A RID: 5274
		// (get) Token: 0x06004024 RID: 16420 RVA: 0x000F7A65 File Offset: 0x000F5C65
		// (set) Token: 0x06004025 RID: 16421 RVA: 0x000F7A77 File Offset: 0x000F5C77
		public EnhancedTimeSpan? InferenceTrainingWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.InferenceTrainingWorkCycle];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.InferenceTrainingWorkCycle] = value;
			}
		}

		// Token: 0x1700149B RID: 5275
		// (get) Token: 0x06004026 RID: 16422 RVA: 0x000F7A8A File Offset: 0x000F5C8A
		// (set) Token: 0x06004027 RID: 16423 RVA: 0x000F7A9C File Offset: 0x000F5C9C
		public EnhancedTimeSpan? InferenceTrainingWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.InferenceTrainingWorkCycleCheckpoint];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.InferenceTrainingWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x1700149C RID: 5276
		// (get) Token: 0x06004028 RID: 16424 RVA: 0x000F7AAF File Offset: 0x000F5CAF
		// (set) Token: 0x06004029 RID: 16425 RVA: 0x000F7AC1 File Offset: 0x000F5CC1
		public EnhancedTimeSpan? DirectoryProcessorWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.DirectoryProcessorWorkCycle];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.DirectoryProcessorWorkCycle] = value;
			}
		}

		// Token: 0x1700149D RID: 5277
		// (get) Token: 0x0600402A RID: 16426 RVA: 0x000F7AD4 File Offset: 0x000F5CD4
		// (set) Token: 0x0600402B RID: 16427 RVA: 0x000F7AE6 File Offset: 0x000F5CE6
		public EnhancedTimeSpan? DirectoryProcessorWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.DirectoryProcessorWorkCycleCheckpoint];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.DirectoryProcessorWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x1700149E RID: 5278
		// (get) Token: 0x0600402C RID: 16428 RVA: 0x000F7AF9 File Offset: 0x000F5CF9
		// (set) Token: 0x0600402D RID: 16429 RVA: 0x000F7B0B File Offset: 0x000F5D0B
		public EnhancedTimeSpan? OABGeneratorWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.OABGeneratorWorkCycle];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.OABGeneratorWorkCycle] = value;
			}
		}

		// Token: 0x1700149F RID: 5279
		// (get) Token: 0x0600402E RID: 16430 RVA: 0x000F7B1E File Offset: 0x000F5D1E
		// (set) Token: 0x0600402F RID: 16431 RVA: 0x000F7B30 File Offset: 0x000F5D30
		public EnhancedTimeSpan? OABGeneratorWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.OABGeneratorWorkCycleCheckpoint];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.OABGeneratorWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x170014A0 RID: 5280
		// (get) Token: 0x06004030 RID: 16432 RVA: 0x000F7B43 File Offset: 0x000F5D43
		// (set) Token: 0x06004031 RID: 16433 RVA: 0x000F7B55 File Offset: 0x000F5D55
		public EnhancedTimeSpan? InferenceDataCollectionWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.InferenceDataCollectionWorkCycle];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.InferenceDataCollectionWorkCycle] = value;
			}
		}

		// Token: 0x170014A1 RID: 5281
		// (get) Token: 0x06004032 RID: 16434 RVA: 0x000F7B68 File Offset: 0x000F5D68
		// (set) Token: 0x06004033 RID: 16435 RVA: 0x000F7B7A File Offset: 0x000F5D7A
		public EnhancedTimeSpan? InferenceDataCollectionWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.InferenceDataCollectionWorkCycleCheckpoint];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.InferenceDataCollectionWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x170014A2 RID: 5282
		// (get) Token: 0x06004034 RID: 16436 RVA: 0x000F7B8D File Offset: 0x000F5D8D
		// (set) Token: 0x06004035 RID: 16437 RVA: 0x000F7B9F File Offset: 0x000F5D9F
		public EnhancedTimeSpan? PeopleRelevanceWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.PeopleRelevanceWorkCycle];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.PeopleRelevanceWorkCycle] = value;
			}
		}

		// Token: 0x170014A3 RID: 5283
		// (get) Token: 0x06004036 RID: 16438 RVA: 0x000F7BB2 File Offset: 0x000F5DB2
		// (set) Token: 0x06004037 RID: 16439 RVA: 0x000F7BC4 File Offset: 0x000F5DC4
		public EnhancedTimeSpan? PeopleRelevanceWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.PeopleRelevanceWorkCycleCheckpoint];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.PeopleRelevanceWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x170014A4 RID: 5284
		// (get) Token: 0x06004038 RID: 16440 RVA: 0x000F7BD7 File Offset: 0x000F5DD7
		// (set) Token: 0x06004039 RID: 16441 RVA: 0x000F7BE9 File Offset: 0x000F5DE9
		public EnhancedTimeSpan? SharePointSignalStoreWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.SharePointSignalStoreWorkCycle];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.SharePointSignalStoreWorkCycle] = value;
			}
		}

		// Token: 0x170014A5 RID: 5285
		// (get) Token: 0x0600403A RID: 16442 RVA: 0x000F7BFC File Offset: 0x000F5DFC
		// (set) Token: 0x0600403B RID: 16443 RVA: 0x000F7C0E File Offset: 0x000F5E0E
		public EnhancedTimeSpan? SharePointSignalStoreWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.SharePointSignalStoreWorkCycleCheckpoint];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.SharePointSignalStoreWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x170014A6 RID: 5286
		// (get) Token: 0x0600403C RID: 16444 RVA: 0x000F7C21 File Offset: 0x000F5E21
		// (set) Token: 0x0600403D RID: 16445 RVA: 0x000F7C33 File Offset: 0x000F5E33
		public EnhancedTimeSpan? PeopleCentricTriageWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.PeopleCentricTriageWorkCycle];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.PeopleCentricTriageWorkCycle] = value;
			}
		}

		// Token: 0x170014A7 RID: 5287
		// (get) Token: 0x0600403E RID: 16446 RVA: 0x000F7C46 File Offset: 0x000F5E46
		// (set) Token: 0x0600403F RID: 16447 RVA: 0x000F7C58 File Offset: 0x000F5E58
		public EnhancedTimeSpan? PeopleCentricTriageWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.PeopleCentricTriageWorkCycleCheckpoint];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.PeopleCentricTriageWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x170014A8 RID: 5288
		// (get) Token: 0x06004040 RID: 16448 RVA: 0x000F7C6B File Offset: 0x000F5E6B
		// (set) Token: 0x06004041 RID: 16449 RVA: 0x000F7C7D File Offset: 0x000F5E7D
		public EnhancedTimeSpan? MailboxProcessorWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.MailboxProcessorWorkCycle];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.MailboxProcessorWorkCycle] = value;
			}
		}

		// Token: 0x170014A9 RID: 5289
		// (get) Token: 0x06004042 RID: 16450 RVA: 0x000F7C90 File Offset: 0x000F5E90
		// (set) Token: 0x06004043 RID: 16451 RVA: 0x000F7CA2 File Offset: 0x000F5EA2
		public EnhancedTimeSpan? StoreDsMaintenanceWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.StoreDsMaintenanceWorkCycle];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.StoreDsMaintenanceWorkCycle] = value;
			}
		}

		// Token: 0x170014AA RID: 5290
		// (get) Token: 0x06004044 RID: 16452 RVA: 0x000F7CB5 File Offset: 0x000F5EB5
		// (set) Token: 0x06004045 RID: 16453 RVA: 0x000F7CC7 File Offset: 0x000F5EC7
		public EnhancedTimeSpan? StoreDsMaintenanceWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.StoreDsMaintenanceWorkCycleCheckpoint];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.StoreDsMaintenanceWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x170014AB RID: 5291
		// (get) Token: 0x06004046 RID: 16454 RVA: 0x000F7CDA File Offset: 0x000F5EDA
		// (set) Token: 0x06004047 RID: 16455 RVA: 0x000F7CEC File Offset: 0x000F5EEC
		public EnhancedTimeSpan? StoreIntegrityCheckWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.StoreIntegrityCheckWorkCycle];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.StoreIntegrityCheckWorkCycle] = value;
			}
		}

		// Token: 0x170014AC RID: 5292
		// (get) Token: 0x06004048 RID: 16456 RVA: 0x000F7CFF File Offset: 0x000F5EFF
		// (set) Token: 0x06004049 RID: 16457 RVA: 0x000F7D11 File Offset: 0x000F5F11
		public EnhancedTimeSpan? StoreIntegrityCheckWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.StoreIntegrityCheckWorkCycleCheckpoint];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.StoreIntegrityCheckWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x170014AD RID: 5293
		// (get) Token: 0x0600404A RID: 16458 RVA: 0x000F7D24 File Offset: 0x000F5F24
		// (set) Token: 0x0600404B RID: 16459 RVA: 0x000F7D36 File Offset: 0x000F5F36
		public EnhancedTimeSpan? StoreMaintenanceWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.StoreMaintenanceWorkCycle];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.StoreMaintenanceWorkCycle] = value;
			}
		}

		// Token: 0x170014AE RID: 5294
		// (get) Token: 0x0600404C RID: 16460 RVA: 0x000F7D49 File Offset: 0x000F5F49
		// (set) Token: 0x0600404D RID: 16461 RVA: 0x000F7D5B File Offset: 0x000F5F5B
		public EnhancedTimeSpan? StoreMaintenanceWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.StoreMaintenanceWorkCycleCheckpoint];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.StoreMaintenanceWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x170014AF RID: 5295
		// (get) Token: 0x0600404E RID: 16462 RVA: 0x000F7D6E File Offset: 0x000F5F6E
		// (set) Token: 0x0600404F RID: 16463 RVA: 0x000F7D80 File Offset: 0x000F5F80
		public EnhancedTimeSpan? StoreScheduledIntegrityCheckWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.StoreScheduledIntegrityCheckWorkCycle];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.StoreScheduledIntegrityCheckWorkCycle] = value;
			}
		}

		// Token: 0x170014B0 RID: 5296
		// (get) Token: 0x06004050 RID: 16464 RVA: 0x000F7D93 File Offset: 0x000F5F93
		// (set) Token: 0x06004051 RID: 16465 RVA: 0x000F7DA5 File Offset: 0x000F5FA5
		public EnhancedTimeSpan? StoreScheduledIntegrityCheckWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.StoreScheduledIntegrityCheckWorkCycleCheckpoint];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.StoreScheduledIntegrityCheckWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x170014B1 RID: 5297
		// (get) Token: 0x06004052 RID: 16466 RVA: 0x000F7DB8 File Offset: 0x000F5FB8
		// (set) Token: 0x06004053 RID: 16467 RVA: 0x000F7DCA File Offset: 0x000F5FCA
		public EnhancedTimeSpan? StoreUrgentMaintenanceWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.StoreUrgentMaintenanceWorkCycle];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.StoreUrgentMaintenanceWorkCycle] = value;
			}
		}

		// Token: 0x170014B2 RID: 5298
		// (get) Token: 0x06004054 RID: 16468 RVA: 0x000F7DDD File Offset: 0x000F5FDD
		// (set) Token: 0x06004055 RID: 16469 RVA: 0x000F7DEF File Offset: 0x000F5FEF
		public EnhancedTimeSpan? StoreUrgentMaintenanceWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.StoreUrgentMaintenanceWorkCycleCheckpoint];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.StoreUrgentMaintenanceWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x170014B3 RID: 5299
		// (get) Token: 0x06004056 RID: 16470 RVA: 0x000F7E02 File Offset: 0x000F6002
		// (set) Token: 0x06004057 RID: 16471 RVA: 0x000F7E14 File Offset: 0x000F6014
		public EnhancedTimeSpan? JunkEmailOptionsCommitterWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.JunkEmailOptionsCommitterWorkCycle];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.JunkEmailOptionsCommitterWorkCycle] = value;
			}
		}

		// Token: 0x170014B4 RID: 5300
		// (get) Token: 0x06004058 RID: 16472 RVA: 0x000F7E27 File Offset: 0x000F6027
		// (set) Token: 0x06004059 RID: 16473 RVA: 0x000F7E39 File Offset: 0x000F6039
		public EnhancedTimeSpan? ProbeTimeBasedAssistantWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.ProbeTimeBasedAssistantWorkCycle];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.ProbeTimeBasedAssistantWorkCycle] = value;
			}
		}

		// Token: 0x170014B5 RID: 5301
		// (get) Token: 0x0600405A RID: 16474 RVA: 0x000F7E4C File Offset: 0x000F604C
		// (set) Token: 0x0600405B RID: 16475 RVA: 0x000F7E5E File Offset: 0x000F605E
		public EnhancedTimeSpan? ProbeTimeBasedAssistantWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.ProbeTimeBasedAssistantWorkCycleCheckpoint];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.ProbeTimeBasedAssistantWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x170014B6 RID: 5302
		// (get) Token: 0x0600405C RID: 16476 RVA: 0x000F7E71 File Offset: 0x000F6071
		// (set) Token: 0x0600405D RID: 16477 RVA: 0x000F7E83 File Offset: 0x000F6083
		public EnhancedTimeSpan? SearchIndexRepairTimeBasedAssistantWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.SearchIndexRepairTimeBasedAssistantWorkCycle];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.SearchIndexRepairTimeBasedAssistantWorkCycle] = value;
			}
		}

		// Token: 0x170014B7 RID: 5303
		// (get) Token: 0x0600405E RID: 16478 RVA: 0x000F7E96 File Offset: 0x000F6096
		// (set) Token: 0x0600405F RID: 16479 RVA: 0x000F7EA8 File Offset: 0x000F60A8
		public EnhancedTimeSpan? SearchIndexRepairTimeBasedAssistantWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.SearchIndexRepairTimeBasedAssistantWorkCycleCheckpoint];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.SearchIndexRepairTimeBasedAssistantWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x170014B8 RID: 5304
		// (get) Token: 0x06004060 RID: 16480 RVA: 0x000F7EBB File Offset: 0x000F60BB
		// (set) Token: 0x06004061 RID: 16481 RVA: 0x000F7ECD File Offset: 0x000F60CD
		public EnhancedTimeSpan? DarTaskStoreTimeBasedAssistantWorkCycle
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.DarTaskStoreTimeBasedAssistantWorkCycle];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.DarTaskStoreTimeBasedAssistantWorkCycle] = value;
			}
		}

		// Token: 0x170014B9 RID: 5305
		// (get) Token: 0x06004062 RID: 16482 RVA: 0x000F7EE0 File Offset: 0x000F60E0
		// (set) Token: 0x06004063 RID: 16483 RVA: 0x000F7EF2 File Offset: 0x000F60F2
		public EnhancedTimeSpan? DarTaskStoreTimeBasedAssistantWorkCycleCheckpoint
		{
			get
			{
				return (EnhancedTimeSpan?)this[ActiveDirectoryServerSchema.DarTaskStoreTimeBasedAssistantWorkCycleCheckpoint];
			}
			internal set
			{
				this[ActiveDirectoryServerSchema.DarTaskStoreTimeBasedAssistantWorkCycleCheckpoint] = value;
			}
		}

		// Token: 0x170014BA RID: 5306
		// (get) Token: 0x06004064 RID: 16484 RVA: 0x000F7F05 File Offset: 0x000F6105
		// (set) Token: 0x06004065 RID: 16485 RVA: 0x000F7F17 File Offset: 0x000F6117
		internal MailboxServerRoleFlags MailboxRoleFlags
		{
			get
			{
				return (MailboxServerRoleFlags)this[ActiveDirectoryServerSchema.MailboxRoleFlags];
			}
			set
			{
				this[ActiveDirectoryServerSchema.MailboxRoleFlags] = value;
			}
		}

		// Token: 0x170014BB RID: 5307
		// (get) Token: 0x06004066 RID: 16486 RVA: 0x000F7F2A File Offset: 0x000F612A
		// (set) Token: 0x06004067 RID: 16487 RVA: 0x000F7F3C File Offset: 0x000F613C
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan DelayNotificationTimeout
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.DelayNotificationTimeout];
			}
			set
			{
				this[ServerSchema.DelayNotificationTimeout] = value;
			}
		}

		// Token: 0x170014BC RID: 5308
		// (get) Token: 0x06004068 RID: 16488 RVA: 0x000F7F4F File Offset: 0x000F614F
		// (set) Token: 0x06004069 RID: 16489 RVA: 0x000F7F61 File Offset: 0x000F6161
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan MessageExpirationTimeout
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.MessageExpirationTimeout];
			}
			set
			{
				this[ServerSchema.MessageExpirationTimeout] = value;
			}
		}

		// Token: 0x170014BD RID: 5309
		// (get) Token: 0x0600406A RID: 16490 RVA: 0x000F7F74 File Offset: 0x000F6174
		// (set) Token: 0x0600406B RID: 16491 RVA: 0x000F7F86 File Offset: 0x000F6186
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan QueueMaxIdleTime
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.QueueMaxIdleTime];
			}
			set
			{
				this[ServerSchema.QueueMaxIdleTime] = value;
			}
		}

		// Token: 0x170014BE RID: 5310
		// (get) Token: 0x0600406C RID: 16492 RVA: 0x000F7F99 File Offset: 0x000F6199
		// (set) Token: 0x0600406D RID: 16493 RVA: 0x000F7FAB File Offset: 0x000F61AB
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan MessageRetryInterval
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.MessageRetryInterval];
			}
			set
			{
				this[ServerSchema.MessageRetryInterval] = value;
			}
		}

		// Token: 0x170014BF RID: 5311
		// (get) Token: 0x0600406E RID: 16494 RVA: 0x000F7FBE File Offset: 0x000F61BE
		// (set) Token: 0x0600406F RID: 16495 RVA: 0x000F7FD0 File Offset: 0x000F61D0
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan TransientFailureRetryInterval
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.TransientFailureRetryInterval];
			}
			set
			{
				this[ServerSchema.TransientFailureRetryInterval] = value;
			}
		}

		// Token: 0x170014C0 RID: 5312
		// (get) Token: 0x06004070 RID: 16496 RVA: 0x000F7FE3 File Offset: 0x000F61E3
		// (set) Token: 0x06004071 RID: 16497 RVA: 0x000F7FF5 File Offset: 0x000F61F5
		[Parameter(Mandatory = false)]
		public int TransientFailureRetryCount
		{
			get
			{
				return (int)this[ServerSchema.TransientFailureRetryCount];
			}
			set
			{
				this[ServerSchema.TransientFailureRetryCount] = value;
			}
		}

		// Token: 0x170014C1 RID: 5313
		// (get) Token: 0x06004072 RID: 16498 RVA: 0x000F8008 File Offset: 0x000F6208
		// (set) Token: 0x06004073 RID: 16499 RVA: 0x000F801A File Offset: 0x000F621A
		[Parameter(Mandatory = false)]
		public Unlimited<int> MaxOutboundConnections
		{
			get
			{
				return (Unlimited<int>)this[ServerSchema.MaxOutboundConnections];
			}
			set
			{
				this[ServerSchema.MaxOutboundConnections] = value;
			}
		}

		// Token: 0x170014C2 RID: 5314
		// (get) Token: 0x06004074 RID: 16500 RVA: 0x000F802D File Offset: 0x000F622D
		// (set) Token: 0x06004075 RID: 16501 RVA: 0x000F803F File Offset: 0x000F623F
		[Parameter(Mandatory = false)]
		public Unlimited<int> MaxPerDomainOutboundConnections
		{
			get
			{
				return (Unlimited<int>)this[ServerSchema.MaxPerDomainOutboundConnections];
			}
			set
			{
				this[ServerSchema.MaxPerDomainOutboundConnections] = value;
			}
		}

		// Token: 0x170014C3 RID: 5315
		// (get) Token: 0x06004076 RID: 16502 RVA: 0x000F8052 File Offset: 0x000F6252
		// (set) Token: 0x06004077 RID: 16503 RVA: 0x000F8064 File Offset: 0x000F6264
		[Parameter(Mandatory = false)]
		public int MaxConnectionRatePerMinute
		{
			get
			{
				return (int)this[ServerSchema.MaxConnectionRatePerMinute];
			}
			set
			{
				this[ServerSchema.MaxConnectionRatePerMinute] = value;
			}
		}

		// Token: 0x170014C4 RID: 5316
		// (get) Token: 0x06004078 RID: 16504 RVA: 0x000F8077 File Offset: 0x000F6277
		// (set) Token: 0x06004079 RID: 16505 RVA: 0x000F8089 File Offset: 0x000F6289
		[Parameter(Mandatory = false)]
		public LocalLongFullPath ReceiveProtocolLogPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.ReceiveProtocolLogPath];
			}
			set
			{
				this[ServerSchema.ReceiveProtocolLogPath] = value;
			}
		}

		// Token: 0x170014C5 RID: 5317
		// (get) Token: 0x0600407A RID: 16506 RVA: 0x000F8097 File Offset: 0x000F6297
		// (set) Token: 0x0600407B RID: 16507 RVA: 0x000F80A9 File Offset: 0x000F62A9
		[Parameter(Mandatory = false)]
		public LocalLongFullPath SendProtocolLogPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.SendProtocolLogPath];
			}
			set
			{
				this[ServerSchema.SendProtocolLogPath] = value;
			}
		}

		// Token: 0x170014C6 RID: 5318
		// (get) Token: 0x0600407C RID: 16508 RVA: 0x000F80B7 File Offset: 0x000F62B7
		// (set) Token: 0x0600407D RID: 16509 RVA: 0x000F80C9 File Offset: 0x000F62C9
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan OutboundConnectionFailureRetryInterval
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.OutboundConnectionFailureRetryInterval];
			}
			set
			{
				this[ServerSchema.OutboundConnectionFailureRetryInterval] = value;
			}
		}

		// Token: 0x170014C7 RID: 5319
		// (get) Token: 0x0600407E RID: 16510 RVA: 0x000F80DC File Offset: 0x000F62DC
		// (set) Token: 0x0600407F RID: 16511 RVA: 0x000F80EE File Offset: 0x000F62EE
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ReceiveProtocolLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.ReceiveProtocolLogMaxAge];
			}
			set
			{
				this[ServerSchema.ReceiveProtocolLogMaxAge] = value;
			}
		}

		// Token: 0x170014C8 RID: 5320
		// (get) Token: 0x06004080 RID: 16512 RVA: 0x000F8101 File Offset: 0x000F6301
		// (set) Token: 0x06004081 RID: 16513 RVA: 0x000F8113 File Offset: 0x000F6313
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.ReceiveProtocolLogMaxDirectorySize];
			}
			set
			{
				this[ServerSchema.ReceiveProtocolLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x170014C9 RID: 5321
		// (get) Token: 0x06004082 RID: 16514 RVA: 0x000F8126 File Offset: 0x000F6326
		// (set) Token: 0x06004083 RID: 16515 RVA: 0x000F8138 File Offset: 0x000F6338
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.ReceiveProtocolLogMaxFileSize];
			}
			set
			{
				this[ServerSchema.ReceiveProtocolLogMaxFileSize] = value;
			}
		}

		// Token: 0x170014CA RID: 5322
		// (get) Token: 0x06004084 RID: 16516 RVA: 0x000F814B File Offset: 0x000F634B
		// (set) Token: 0x06004085 RID: 16517 RVA: 0x000F815D File Offset: 0x000F635D
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan SendProtocolLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.SendProtocolLogMaxAge];
			}
			set
			{
				this[ServerSchema.SendProtocolLogMaxAge] = value;
			}
		}

		// Token: 0x170014CB RID: 5323
		// (get) Token: 0x06004086 RID: 16518 RVA: 0x000F8170 File Offset: 0x000F6370
		// (set) Token: 0x06004087 RID: 16519 RVA: 0x000F8182 File Offset: 0x000F6382
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> SendProtocolLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.SendProtocolLogMaxDirectorySize];
			}
			set
			{
				this[ServerSchema.SendProtocolLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x170014CC RID: 5324
		// (get) Token: 0x06004088 RID: 16520 RVA: 0x000F8195 File Offset: 0x000F6395
		// (set) Token: 0x06004089 RID: 16521 RVA: 0x000F81A7 File Offset: 0x000F63A7
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> SendProtocolLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.SendProtocolLogMaxFileSize];
			}
			set
			{
				this[ServerSchema.SendProtocolLogMaxFileSize] = value;
			}
		}

		// Token: 0x170014CD RID: 5325
		// (get) Token: 0x0600408A RID: 16522 RVA: 0x000F81BA File Offset: 0x000F63BA
		// (set) Token: 0x0600408B RID: 16523 RVA: 0x000F81CF File Offset: 0x000F63CF
		[Parameter(Mandatory = false)]
		public bool InternalDNSAdapterEnabled
		{
			get
			{
				return !(bool)this[ServerSchema.InternalDNSAdapterDisabled];
			}
			set
			{
				this[ServerSchema.InternalDNSAdapterDisabled] = !value;
			}
		}

		// Token: 0x170014CE RID: 5326
		// (get) Token: 0x0600408C RID: 16524 RVA: 0x000F81E5 File Offset: 0x000F63E5
		// (set) Token: 0x0600408D RID: 16525 RVA: 0x000F81F7 File Offset: 0x000F63F7
		[Parameter(Mandatory = false)]
		public Guid InternalDNSAdapterGuid
		{
			get
			{
				return (Guid)this[ServerSchema.InternalDNSAdapterGuid];
			}
			set
			{
				this[ServerSchema.InternalDNSAdapterGuid] = value;
			}
		}

		// Token: 0x170014CF RID: 5327
		// (get) Token: 0x0600408E RID: 16526 RVA: 0x000F820A File Offset: 0x000F640A
		// (set) Token: 0x0600408F RID: 16527 RVA: 0x000F821C File Offset: 0x000F641C
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IPAddress> InternalDNSServers
		{
			get
			{
				return (MultiValuedProperty<IPAddress>)this[ServerSchema.InternalDNSServers];
			}
			set
			{
				this[ServerSchema.InternalDNSServers] = value;
			}
		}

		// Token: 0x170014D0 RID: 5328
		// (get) Token: 0x06004090 RID: 16528 RVA: 0x000F822A File Offset: 0x000F642A
		// (set) Token: 0x06004091 RID: 16529 RVA: 0x000F823C File Offset: 0x000F643C
		[Parameter(Mandatory = false)]
		public ProtocolOption InternalDNSProtocolOption
		{
			get
			{
				return (ProtocolOption)this[ServerSchema.InternalDNSProtocolOption];
			}
			set
			{
				this[ServerSchema.InternalDNSProtocolOption] = value;
			}
		}

		// Token: 0x170014D1 RID: 5329
		// (get) Token: 0x06004092 RID: 16530 RVA: 0x000F824F File Offset: 0x000F644F
		// (set) Token: 0x06004093 RID: 16531 RVA: 0x000F8264 File Offset: 0x000F6464
		[Parameter(Mandatory = false)]
		public bool ExternalDNSAdapterEnabled
		{
			get
			{
				return !(bool)this[ServerSchema.ExternalDNSAdapterDisabled];
			}
			set
			{
				this[ServerSchema.ExternalDNSAdapterDisabled] = !value;
			}
		}

		// Token: 0x170014D2 RID: 5330
		// (get) Token: 0x06004094 RID: 16532 RVA: 0x000F827A File Offset: 0x000F647A
		// (set) Token: 0x06004095 RID: 16533 RVA: 0x000F828C File Offset: 0x000F648C
		[Parameter(Mandatory = false)]
		public Guid ExternalDNSAdapterGuid
		{
			get
			{
				return (Guid)this[ServerSchema.ExternalDNSAdapterGuid];
			}
			set
			{
				this[ServerSchema.ExternalDNSAdapterGuid] = value;
			}
		}

		// Token: 0x170014D3 RID: 5331
		// (get) Token: 0x06004096 RID: 16534 RVA: 0x000F829F File Offset: 0x000F649F
		// (set) Token: 0x06004097 RID: 16535 RVA: 0x000F82B1 File Offset: 0x000F64B1
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IPAddress> ExternalDNSServers
		{
			get
			{
				return (MultiValuedProperty<IPAddress>)this[ServerSchema.ExternalDNSServers];
			}
			set
			{
				this[ServerSchema.ExternalDNSServers] = value;
			}
		}

		// Token: 0x170014D4 RID: 5332
		// (get) Token: 0x06004098 RID: 16536 RVA: 0x000F82BF File Offset: 0x000F64BF
		// (set) Token: 0x06004099 RID: 16537 RVA: 0x000F82D1 File Offset: 0x000F64D1
		[Parameter(Mandatory = false)]
		public IPAddress ExternalIPAddress
		{
			get
			{
				return (IPAddress)this[ServerSchema.ExternalIPAddress];
			}
			set
			{
				this[ServerSchema.ExternalIPAddress] = value;
			}
		}

		// Token: 0x170014D5 RID: 5333
		// (get) Token: 0x0600409A RID: 16538 RVA: 0x000F82DF File Offset: 0x000F64DF
		// (set) Token: 0x0600409B RID: 16539 RVA: 0x000F82F1 File Offset: 0x000F64F1
		[Parameter(Mandatory = false)]
		public ProtocolOption ExternalDNSProtocolOption
		{
			get
			{
				return (ProtocolOption)this[ServerSchema.ExternalDNSProtocolOption];
			}
			set
			{
				this[ServerSchema.ExternalDNSProtocolOption] = value;
			}
		}

		// Token: 0x170014D6 RID: 5334
		// (get) Token: 0x0600409C RID: 16540 RVA: 0x000F8304 File Offset: 0x000F6504
		// (set) Token: 0x0600409D RID: 16541 RVA: 0x000F8316 File Offset: 0x000F6516
		[Parameter(Mandatory = false)]
		public int MaxConcurrentMailboxDeliveries
		{
			get
			{
				return (int)this[ServerSchema.MaxConcurrentMailboxDeliveries];
			}
			set
			{
				this[ServerSchema.MaxConcurrentMailboxDeliveries] = value;
			}
		}

		// Token: 0x170014D7 RID: 5335
		// (get) Token: 0x0600409E RID: 16542 RVA: 0x000F8329 File Offset: 0x000F6529
		// (set) Token: 0x0600409F RID: 16543 RVA: 0x000F833B File Offset: 0x000F653B
		[Parameter(Mandatory = false)]
		public int MaxConcurrentMailboxSubmissions
		{
			get
			{
				return (int)this[ActiveDirectoryServerSchema.MaxConcurrentMailboxSubmissions];
			}
			set
			{
				this[ActiveDirectoryServerSchema.MaxConcurrentMailboxSubmissions] = value;
			}
		}

		// Token: 0x170014D8 RID: 5336
		// (get) Token: 0x060040A0 RID: 16544 RVA: 0x000F834E File Offset: 0x000F654E
		// (set) Token: 0x060040A1 RID: 16545 RVA: 0x000F8360 File Offset: 0x000F6560
		[Parameter(Mandatory = false)]
		public int PoisonThreshold
		{
			get
			{
				return (int)this[ServerSchema.PoisonThreshold];
			}
			set
			{
				this[ServerSchema.PoisonThreshold] = value;
			}
		}

		// Token: 0x170014D9 RID: 5337
		// (get) Token: 0x060040A2 RID: 16546 RVA: 0x000F8373 File Offset: 0x000F6573
		// (set) Token: 0x060040A3 RID: 16547 RVA: 0x000F8385 File Offset: 0x000F6585
		[Parameter(Mandatory = false)]
		public LocalLongFullPath MessageTrackingLogPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.MessageTrackingLogPath];
			}
			set
			{
				this[ServerSchema.MessageTrackingLogPath] = value;
			}
		}

		// Token: 0x170014DA RID: 5338
		// (get) Token: 0x060040A4 RID: 16548 RVA: 0x000F8393 File Offset: 0x000F6593
		// (set) Token: 0x060040A5 RID: 16549 RVA: 0x000F83A5 File Offset: 0x000F65A5
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan MessageTrackingLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.MessageTrackingLogMaxAge];
			}
			set
			{
				this[ServerSchema.MessageTrackingLogMaxAge] = value;
			}
		}

		// Token: 0x170014DB RID: 5339
		// (get) Token: 0x060040A6 RID: 16550 RVA: 0x000F83B8 File Offset: 0x000F65B8
		// (set) Token: 0x060040A7 RID: 16551 RVA: 0x000F83CA File Offset: 0x000F65CA
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> MessageTrackingLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.MessageTrackingLogMaxDirectorySize];
			}
			set
			{
				this[ServerSchema.MessageTrackingLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x170014DC RID: 5340
		// (get) Token: 0x060040A8 RID: 16552 RVA: 0x000F83DD File Offset: 0x000F65DD
		// (set) Token: 0x060040A9 RID: 16553 RVA: 0x000F83EF File Offset: 0x000F65EF
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize MessageTrackingLogMaxFileSize
		{
			get
			{
				return (ByteQuantifiedSize)this[ServerSchema.MessageTrackingLogMaxFileSize];
			}
			set
			{
				this[ServerSchema.MessageTrackingLogMaxFileSize] = value;
			}
		}

		// Token: 0x170014DD RID: 5341
		// (get) Token: 0x060040AA RID: 16554 RVA: 0x000F8402 File Offset: 0x000F6602
		// (set) Token: 0x060040AB RID: 16555 RVA: 0x000F8414 File Offset: 0x000F6614
		[Parameter(Mandatory = false)]
		public string MigrationLogExtensionData
		{
			get
			{
				return (string)this[ActiveDirectoryServerSchema.MigrationLogExtensionData];
			}
			set
			{
				this[ActiveDirectoryServerSchema.MigrationLogExtensionData] = value;
			}
		}

		// Token: 0x170014DE RID: 5342
		// (get) Token: 0x060040AC RID: 16556 RVA: 0x000F8422 File Offset: 0x000F6622
		// (set) Token: 0x060040AD RID: 16557 RVA: 0x000F8434 File Offset: 0x000F6634
		[Parameter(Mandatory = false)]
		public MigrationEventType MigrationLogLoggingLevel
		{
			get
			{
				return (MigrationEventType)this[ActiveDirectoryServerSchema.MigrationLogLoggingLevel];
			}
			set
			{
				this[ActiveDirectoryServerSchema.MigrationLogLoggingLevel] = value;
			}
		}

		// Token: 0x170014DF RID: 5343
		// (get) Token: 0x060040AE RID: 16558 RVA: 0x000F8447 File Offset: 0x000F6647
		// (set) Token: 0x060040AF RID: 16559 RVA: 0x000F8459 File Offset: 0x000F6659
		[Parameter(Mandatory = false)]
		public LocalLongFullPath MigrationLogFilePath
		{
			get
			{
				return (LocalLongFullPath)this[ActiveDirectoryServerSchema.MigrationLogFilePath];
			}
			set
			{
				this[ActiveDirectoryServerSchema.MigrationLogFilePath] = value;
			}
		}

		// Token: 0x170014E0 RID: 5344
		// (get) Token: 0x060040B0 RID: 16560 RVA: 0x000F8467 File Offset: 0x000F6667
		// (set) Token: 0x060040B1 RID: 16561 RVA: 0x000F8479 File Offset: 0x000F6679
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan MigrationLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ActiveDirectoryServerSchema.MigrationLogMaxAge];
			}
			set
			{
				this[ActiveDirectoryServerSchema.MigrationLogMaxAge] = value;
			}
		}

		// Token: 0x170014E1 RID: 5345
		// (get) Token: 0x060040B2 RID: 16562 RVA: 0x000F848C File Offset: 0x000F668C
		// (set) Token: 0x060040B3 RID: 16563 RVA: 0x000F849E File Offset: 0x000F669E
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize MigrationLogMaxDirectorySize
		{
			get
			{
				return (ByteQuantifiedSize)this[ActiveDirectoryServerSchema.MigrationLogMaxDirectorySize];
			}
			set
			{
				this[ActiveDirectoryServerSchema.MigrationLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x170014E2 RID: 5346
		// (get) Token: 0x060040B4 RID: 16564 RVA: 0x000F84B1 File Offset: 0x000F66B1
		// (set) Token: 0x060040B5 RID: 16565 RVA: 0x000F84C3 File Offset: 0x000F66C3
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize MigrationLogMaxFileSize
		{
			get
			{
				return (ByteQuantifiedSize)this[ActiveDirectoryServerSchema.MigrationLogMaxFileSize];
			}
			set
			{
				this[ActiveDirectoryServerSchema.MigrationLogMaxFileSize] = value;
			}
		}

		// Token: 0x170014E3 RID: 5347
		// (get) Token: 0x060040B6 RID: 16566 RVA: 0x000F84D6 File Offset: 0x000F66D6
		// (set) Token: 0x060040B7 RID: 16567 RVA: 0x000F84E8 File Offset: 0x000F66E8
		[Parameter(Mandatory = false)]
		public LocalLongFullPath IrmLogPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.IrmLogPath];
			}
			set
			{
				this[ServerSchema.IrmLogPath] = value;
			}
		}

		// Token: 0x170014E4 RID: 5348
		// (get) Token: 0x060040B8 RID: 16568 RVA: 0x000F84F6 File Offset: 0x000F66F6
		// (set) Token: 0x060040B9 RID: 16569 RVA: 0x000F8508 File Offset: 0x000F6708
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan IrmLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.IrmLogMaxAge];
			}
			set
			{
				this[ServerSchema.IrmLogMaxAge] = value;
			}
		}

		// Token: 0x170014E5 RID: 5349
		// (get) Token: 0x060040BA RID: 16570 RVA: 0x000F851B File Offset: 0x000F671B
		// (set) Token: 0x060040BB RID: 16571 RVA: 0x000F852D File Offset: 0x000F672D
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> IrmLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.IrmLogMaxDirectorySize];
			}
			set
			{
				this[ServerSchema.IrmLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x170014E6 RID: 5350
		// (get) Token: 0x060040BC RID: 16572 RVA: 0x000F8540 File Offset: 0x000F6740
		// (set) Token: 0x060040BD RID: 16573 RVA: 0x000F8552 File Offset: 0x000F6752
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize IrmLogMaxFileSize
		{
			get
			{
				return (ByteQuantifiedSize)this[ServerSchema.IrmLogMaxFileSize];
			}
			set
			{
				this[ServerSchema.IrmLogMaxFileSize] = value;
			}
		}

		// Token: 0x170014E7 RID: 5351
		// (get) Token: 0x060040BE RID: 16574 RVA: 0x000F8565 File Offset: 0x000F6765
		// (set) Token: 0x060040BF RID: 16575 RVA: 0x000F8577 File Offset: 0x000F6777
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ActiveUserStatisticsLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.ActiveUserStatisticsLogMaxAge];
			}
			set
			{
				this[ServerSchema.ActiveUserStatisticsLogMaxAge] = value;
			}
		}

		// Token: 0x170014E8 RID: 5352
		// (get) Token: 0x060040C0 RID: 16576 RVA: 0x000F858A File Offset: 0x000F678A
		// (set) Token: 0x060040C1 RID: 16577 RVA: 0x000F859C File Offset: 0x000F679C
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize ActiveUserStatisticsLogMaxDirectorySize
		{
			get
			{
				return (ByteQuantifiedSize)this[ServerSchema.ActiveUserStatisticsLogMaxDirectorySize];
			}
			set
			{
				this[ServerSchema.ActiveUserStatisticsLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x170014E9 RID: 5353
		// (get) Token: 0x060040C2 RID: 16578 RVA: 0x000F85AF File Offset: 0x000F67AF
		// (set) Token: 0x060040C3 RID: 16579 RVA: 0x000F85C1 File Offset: 0x000F67C1
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize ActiveUserStatisticsLogMaxFileSize
		{
			get
			{
				return (ByteQuantifiedSize)this[ServerSchema.ActiveUserStatisticsLogMaxFileSize];
			}
			set
			{
				this[ServerSchema.ActiveUserStatisticsLogMaxFileSize] = value;
			}
		}

		// Token: 0x170014EA RID: 5354
		// (get) Token: 0x060040C4 RID: 16580 RVA: 0x000F85D4 File Offset: 0x000F67D4
		// (set) Token: 0x060040C5 RID: 16581 RVA: 0x000F85E6 File Offset: 0x000F67E6
		[Parameter(Mandatory = false)]
		public LocalLongFullPath ActiveUserStatisticsLogPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.ActiveUserStatisticsLogPath];
			}
			set
			{
				this[ServerSchema.ActiveUserStatisticsLogPath] = value;
			}
		}

		// Token: 0x170014EB RID: 5355
		// (get) Token: 0x060040C6 RID: 16582 RVA: 0x000F85F4 File Offset: 0x000F67F4
		// (set) Token: 0x060040C7 RID: 16583 RVA: 0x000F8606 File Offset: 0x000F6806
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ServerStatisticsLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.ServerStatisticsLogMaxAge];
			}
			set
			{
				this[ServerSchema.ServerStatisticsLogMaxAge] = value;
			}
		}

		// Token: 0x170014EC RID: 5356
		// (get) Token: 0x060040C8 RID: 16584 RVA: 0x000F8619 File Offset: 0x000F6819
		// (set) Token: 0x060040C9 RID: 16585 RVA: 0x000F862B File Offset: 0x000F682B
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize ServerStatisticsLogMaxDirectorySize
		{
			get
			{
				return (ByteQuantifiedSize)this[ServerSchema.ServerStatisticsLogMaxDirectorySize];
			}
			set
			{
				this[ServerSchema.ServerStatisticsLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x170014ED RID: 5357
		// (get) Token: 0x060040CA RID: 16586 RVA: 0x000F863E File Offset: 0x000F683E
		// (set) Token: 0x060040CB RID: 16587 RVA: 0x000F8650 File Offset: 0x000F6850
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize ServerStatisticsLogMaxFileSize
		{
			get
			{
				return (ByteQuantifiedSize)this[ServerSchema.ServerStatisticsLogMaxFileSize];
			}
			set
			{
				this[ServerSchema.ServerStatisticsLogMaxFileSize] = value;
			}
		}

		// Token: 0x170014EE RID: 5358
		// (get) Token: 0x060040CC RID: 16588 RVA: 0x000F8663 File Offset: 0x000F6863
		// (set) Token: 0x060040CD RID: 16589 RVA: 0x000F8675 File Offset: 0x000F6875
		[Parameter(Mandatory = false)]
		public LocalLongFullPath ServerStatisticsLogPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.ServerStatisticsLogPath];
			}
			set
			{
				this[ServerSchema.ServerStatisticsLogPath] = value;
			}
		}

		// Token: 0x170014EF RID: 5359
		// (get) Token: 0x060040CE RID: 16590 RVA: 0x000F8683 File Offset: 0x000F6883
		// (set) Token: 0x060040CF RID: 16591 RVA: 0x000F8695 File Offset: 0x000F6895
		[Parameter(Mandatory = false)]
		public bool ConnectivityLogEnabled
		{
			get
			{
				return (bool)this[ServerSchema.ConnectivityLogEnabled];
			}
			set
			{
				this[ServerSchema.ConnectivityLogEnabled] = value;
			}
		}

		// Token: 0x170014F0 RID: 5360
		// (get) Token: 0x060040D0 RID: 16592 RVA: 0x000F86A8 File Offset: 0x000F68A8
		// (set) Token: 0x060040D1 RID: 16593 RVA: 0x000F86BA File Offset: 0x000F68BA
		[Parameter(Mandatory = false)]
		public LocalLongFullPath ConnectivityLogPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.ConnectivityLogPath];
			}
			set
			{
				this[ServerSchema.ConnectivityLogPath] = value;
			}
		}

		// Token: 0x170014F1 RID: 5361
		// (get) Token: 0x060040D2 RID: 16594 RVA: 0x000F86C8 File Offset: 0x000F68C8
		// (set) Token: 0x060040D3 RID: 16595 RVA: 0x000F86DA File Offset: 0x000F68DA
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ConnectivityLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.ConnectivityLogMaxAge];
			}
			set
			{
				this[ServerSchema.ConnectivityLogMaxAge] = value;
			}
		}

		// Token: 0x170014F2 RID: 5362
		// (get) Token: 0x060040D4 RID: 16596 RVA: 0x000F86ED File Offset: 0x000F68ED
		// (set) Token: 0x060040D5 RID: 16597 RVA: 0x000F86FF File Offset: 0x000F68FF
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ConnectivityLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.ConnectivityLogMaxDirectorySize];
			}
			set
			{
				this[ServerSchema.ConnectivityLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x170014F3 RID: 5363
		// (get) Token: 0x060040D6 RID: 16598 RVA: 0x000F8712 File Offset: 0x000F6912
		// (set) Token: 0x060040D7 RID: 16599 RVA: 0x000F8724 File Offset: 0x000F6924
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ConnectivityLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.ConnectivityLogMaxFileSize];
			}
			set
			{
				this[ServerSchema.ConnectivityLogMaxFileSize] = value;
			}
		}

		// Token: 0x170014F4 RID: 5364
		// (get) Token: 0x060040D8 RID: 16600 RVA: 0x000F8737 File Offset: 0x000F6937
		// (set) Token: 0x060040D9 RID: 16601 RVA: 0x000F8749 File Offset: 0x000F6949
		[Parameter(Mandatory = false)]
		public LocalLongFullPath PickupDirectoryPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.PickupDirectoryPath];
			}
			set
			{
				this[ServerSchema.PickupDirectoryPath] = value;
			}
		}

		// Token: 0x170014F5 RID: 5365
		// (get) Token: 0x060040DA RID: 16602 RVA: 0x000F8757 File Offset: 0x000F6957
		// (set) Token: 0x060040DB RID: 16603 RVA: 0x000F8769 File Offset: 0x000F6969
		[Parameter(Mandatory = false)]
		public LocalLongFullPath ReplayDirectoryPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.ReplayDirectoryPath];
			}
			set
			{
				this[ServerSchema.ReplayDirectoryPath] = value;
			}
		}

		// Token: 0x170014F6 RID: 5366
		// (get) Token: 0x060040DC RID: 16604 RVA: 0x000F8777 File Offset: 0x000F6977
		// (set) Token: 0x060040DD RID: 16605 RVA: 0x000F8789 File Offset: 0x000F6989
		[Parameter(Mandatory = false)]
		public int PickupDirectoryMaxMessagesPerMinute
		{
			get
			{
				return (int)this[ServerSchema.PickupDirectoryMaxMessagesPerMinute];
			}
			set
			{
				this[ServerSchema.PickupDirectoryMaxMessagesPerMinute] = value;
			}
		}

		// Token: 0x170014F7 RID: 5367
		// (get) Token: 0x060040DE RID: 16606 RVA: 0x000F879C File Offset: 0x000F699C
		// (set) Token: 0x060040DF RID: 16607 RVA: 0x000F87AE File Offset: 0x000F69AE
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize PickupDirectoryMaxHeaderSize
		{
			get
			{
				return (ByteQuantifiedSize)this[ServerSchema.PickupDirectoryMaxHeaderSize];
			}
			set
			{
				this[ServerSchema.PickupDirectoryMaxHeaderSize] = value;
			}
		}

		// Token: 0x170014F8 RID: 5368
		// (get) Token: 0x060040E0 RID: 16608 RVA: 0x000F87C1 File Offset: 0x000F69C1
		// (set) Token: 0x060040E1 RID: 16609 RVA: 0x000F87D3 File Offset: 0x000F69D3
		[Parameter(Mandatory = false)]
		public int PickupDirectoryMaxRecipientsPerMessage
		{
			get
			{
				return (int)this[ServerSchema.PickupDirectoryMaxRecipientsPerMessage];
			}
			set
			{
				this[ServerSchema.PickupDirectoryMaxRecipientsPerMessage] = value;
			}
		}

		// Token: 0x170014F9 RID: 5369
		// (get) Token: 0x060040E2 RID: 16610 RVA: 0x000F87E6 File Offset: 0x000F69E6
		// (set) Token: 0x060040E3 RID: 16611 RVA: 0x000F87F8 File Offset: 0x000F69F8
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan RoutingTableLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.RoutingTableLogMaxAge];
			}
			set
			{
				this[ServerSchema.RoutingTableLogMaxAge] = value;
			}
		}

		// Token: 0x170014FA RID: 5370
		// (get) Token: 0x060040E4 RID: 16612 RVA: 0x000F880B File Offset: 0x000F6A0B
		// (set) Token: 0x060040E5 RID: 16613 RVA: 0x000F881D File Offset: 0x000F6A1D
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> RoutingTableLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.RoutingTableLogMaxDirectorySize];
			}
			set
			{
				this[ServerSchema.RoutingTableLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x170014FB RID: 5371
		// (get) Token: 0x060040E6 RID: 16614 RVA: 0x000F8830 File Offset: 0x000F6A30
		// (set) Token: 0x060040E7 RID: 16615 RVA: 0x000F8842 File Offset: 0x000F6A42
		[Parameter(Mandatory = false)]
		public LocalLongFullPath RoutingTableLogPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.RoutingTableLogPath];
			}
			set
			{
				this[ServerSchema.RoutingTableLogPath] = value;
			}
		}

		// Token: 0x170014FC RID: 5372
		// (get) Token: 0x060040E8 RID: 16616 RVA: 0x000F8850 File Offset: 0x000F6A50
		// (set) Token: 0x060040E9 RID: 16617 RVA: 0x000F8862 File Offset: 0x000F6A62
		[Parameter(Mandatory = false)]
		public ProtocolLoggingLevel IntraOrgConnectorProtocolLoggingLevel
		{
			get
			{
				return (ProtocolLoggingLevel)this[ServerSchema.IntraOrgConnectorProtocolLoggingLevel];
			}
			set
			{
				this[ServerSchema.IntraOrgConnectorProtocolLoggingLevel] = value;
			}
		}

		// Token: 0x170014FD RID: 5373
		// (get) Token: 0x060040EA RID: 16618 RVA: 0x000F8875 File Offset: 0x000F6A75
		// (set) Token: 0x060040EB RID: 16619 RVA: 0x000F8887 File Offset: 0x000F6A87
		[Parameter(Mandatory = false)]
		public ProtocolLoggingLevel InMemoryReceiveConnectorProtocolLoggingLevel
		{
			get
			{
				return (ProtocolLoggingLevel)this[ServerSchema.InMemoryReceiveConnectorProtocolLoggingLevel];
			}
			set
			{
				this[ServerSchema.InMemoryReceiveConnectorProtocolLoggingLevel] = value;
			}
		}

		// Token: 0x170014FE RID: 5374
		// (get) Token: 0x060040EC RID: 16620 RVA: 0x000F889A File Offset: 0x000F6A9A
		// (set) Token: 0x060040ED RID: 16621 RVA: 0x000F88AC File Offset: 0x000F6AAC
		[Parameter(Mandatory = false)]
		public bool InMemoryReceiveConnectorSmtpUtf8Enabled
		{
			get
			{
				return (bool)this[ServerSchema.InMemoryReceiveConnectorSmtpUtf8Enabled];
			}
			set
			{
				this[ServerSchema.InMemoryReceiveConnectorSmtpUtf8Enabled] = value;
			}
		}

		// Token: 0x170014FF RID: 5375
		// (get) Token: 0x060040EE RID: 16622 RVA: 0x000F88BF File Offset: 0x000F6ABF
		// (set) Token: 0x060040EF RID: 16623 RVA: 0x000F88D1 File Offset: 0x000F6AD1
		[Parameter(Mandatory = false)]
		public bool MessageTrackingLogEnabled
		{
			get
			{
				return (bool)this[ServerSchema.MessageTrackingLogEnabled];
			}
			set
			{
				this[ServerSchema.MessageTrackingLogEnabled] = value;
			}
		}

		// Token: 0x17001500 RID: 5376
		// (get) Token: 0x060040F0 RID: 16624 RVA: 0x000F88E4 File Offset: 0x000F6AE4
		// (set) Token: 0x060040F1 RID: 16625 RVA: 0x000F88F6 File Offset: 0x000F6AF6
		[Parameter(Mandatory = false)]
		public bool MessageTrackingLogSubjectLoggingEnabled
		{
			get
			{
				return (bool)this[ServerSchema.MessageTrackingLogSubjectLoggingEnabled];
			}
			set
			{
				this[ServerSchema.MessageTrackingLogSubjectLoggingEnabled] = value;
			}
		}

		// Token: 0x17001501 RID: 5377
		// (get) Token: 0x060040F2 RID: 16626 RVA: 0x000F8909 File Offset: 0x000F6B09
		// (set) Token: 0x060040F3 RID: 16627 RVA: 0x000F891B File Offset: 0x000F6B1B
		[Parameter(Mandatory = false)]
		public bool IrmLogEnabled
		{
			get
			{
				return (bool)this[ServerSchema.IrmLogEnabled];
			}
			set
			{
				this[ServerSchema.IrmLogEnabled] = value;
			}
		}

		// Token: 0x17001502 RID: 5378
		// (get) Token: 0x060040F4 RID: 16628 RVA: 0x000F892E File Offset: 0x000F6B2E
		// (set) Token: 0x060040F5 RID: 16629 RVA: 0x000F8940 File Offset: 0x000F6B40
		public bool PipelineTracingEnabled
		{
			get
			{
				return (bool)this[ServerSchema.PipelineTracingEnabled];
			}
			internal set
			{
				this[ServerSchema.PipelineTracingEnabled] = value;
			}
		}

		// Token: 0x17001503 RID: 5379
		// (get) Token: 0x060040F6 RID: 16630 RVA: 0x000F8953 File Offset: 0x000F6B53
		// (set) Token: 0x060040F7 RID: 16631 RVA: 0x000F8965 File Offset: 0x000F6B65
		public bool ContentConversionTracingEnabled
		{
			get
			{
				return (bool)this[ServerSchema.ContentConversionTracingEnabled];
			}
			internal set
			{
				this[ServerSchema.ContentConversionTracingEnabled] = value;
			}
		}

		// Token: 0x17001504 RID: 5380
		// (get) Token: 0x060040F8 RID: 16632 RVA: 0x000F8978 File Offset: 0x000F6B78
		// (set) Token: 0x060040F9 RID: 16633 RVA: 0x000F898A File Offset: 0x000F6B8A
		[Parameter(Mandatory = false)]
		public bool GatewayEdgeSyncSubscribed
		{
			get
			{
				return (bool)this[ServerSchema.GatewayEdgeSyncSubscribed];
			}
			set
			{
				this[ServerSchema.GatewayEdgeSyncSubscribed] = value;
			}
		}

		// Token: 0x17001505 RID: 5381
		// (get) Token: 0x060040FA RID: 16634 RVA: 0x000F899D File Offset: 0x000F6B9D
		// (set) Token: 0x060040FB RID: 16635 RVA: 0x000F89AF File Offset: 0x000F6BAF
		public bool AntispamUpdatesEnabled
		{
			get
			{
				return (bool)this[ServerSchema.AntispamUpdatesEnabled];
			}
			internal set
			{
				this[ServerSchema.AntispamUpdatesEnabled] = value;
			}
		}

		// Token: 0x17001506 RID: 5382
		// (get) Token: 0x060040FC RID: 16636 RVA: 0x000F89C2 File Offset: 0x000F6BC2
		// (set) Token: 0x060040FD RID: 16637 RVA: 0x000F89D4 File Offset: 0x000F6BD4
		public LocalLongFullPath PipelineTracingPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.PipelineTracingPath];
			}
			internal set
			{
				this[ServerSchema.PipelineTracingPath] = value;
			}
		}

		// Token: 0x17001507 RID: 5383
		// (get) Token: 0x060040FE RID: 16638 RVA: 0x000F89E2 File Offset: 0x000F6BE2
		// (set) Token: 0x060040FF RID: 16639 RVA: 0x000F89F4 File Offset: 0x000F6BF4
		public SmtpAddress? PipelineTracingSenderAddress
		{
			get
			{
				return (SmtpAddress?)this[ServerSchema.PipelineTracingSenderAddress];
			}
			internal set
			{
				this[ServerSchema.PipelineTracingSenderAddress] = value;
			}
		}

		// Token: 0x17001508 RID: 5384
		// (get) Token: 0x06004100 RID: 16640 RVA: 0x000F8A07 File Offset: 0x000F6C07
		// (set) Token: 0x06004101 RID: 16641 RVA: 0x000F8A19 File Offset: 0x000F6C19
		[Parameter(Mandatory = false)]
		public bool PoisonMessageDetectionEnabled
		{
			get
			{
				return (bool)this[ServerSchema.PoisonMessageDetectionEnabled];
			}
			set
			{
				this[ServerSchema.PoisonMessageDetectionEnabled] = value;
			}
		}

		// Token: 0x17001509 RID: 5385
		// (get) Token: 0x06004102 RID: 16642 RVA: 0x000F8A2C File Offset: 0x000F6C2C
		// (set) Token: 0x06004103 RID: 16643 RVA: 0x000F8A3E File Offset: 0x000F6C3E
		[Parameter(Mandatory = false)]
		public bool AntispamAgentsEnabled
		{
			get
			{
				return (bool)this[ServerSchema.AntispamAgentsEnabled];
			}
			set
			{
				this[ServerSchema.AntispamAgentsEnabled] = value;
			}
		}

		// Token: 0x1700150A RID: 5386
		// (get) Token: 0x06004104 RID: 16644 RVA: 0x000F8A51 File Offset: 0x000F6C51
		// (set) Token: 0x06004105 RID: 16645 RVA: 0x000F8A63 File Offset: 0x000F6C63
		[Parameter(Mandatory = false)]
		public bool RecipientValidationCacheEnabled
		{
			get
			{
				return (bool)this[ServerSchema.RecipientValidationCacheEnabled];
			}
			set
			{
				this[ServerSchema.RecipientValidationCacheEnabled] = value;
			}
		}

		// Token: 0x1700150B RID: 5387
		// (get) Token: 0x06004106 RID: 16646 RVA: 0x000F8A76 File Offset: 0x000F6C76
		internal override SystemFlagsEnum SystemFlags
		{
			get
			{
				return (SystemFlagsEnum)this[ServerSchema.SystemFlags];
			}
		}

		// Token: 0x1700150C RID: 5388
		// (get) Token: 0x06004107 RID: 16647 RVA: 0x000F8A88 File Offset: 0x000F6C88
		// (set) Token: 0x06004108 RID: 16648 RVA: 0x000F8A9A File Offset: 0x000F6C9A
		[Parameter(Mandatory = false)]
		public string RootDropDirectoryPath
		{
			get
			{
				return (string)this[ServerSchema.RootDropDirectoryPath];
			}
			set
			{
				this[ServerSchema.RootDropDirectoryPath] = value;
			}
		}

		// Token: 0x1700150D RID: 5389
		// (get) Token: 0x06004109 RID: 16649 RVA: 0x000F8AA8 File Offset: 0x000F6CA8
		// (set) Token: 0x0600410A RID: 16650 RVA: 0x000F8ABA File Offset: 0x000F6CBA
		[Parameter(Mandatory = false)]
		public int? MaxCallsAllowed
		{
			get
			{
				return (int?)this[ServerSchema.MaxCallsAllowed];
			}
			set
			{
				this[ServerSchema.MaxCallsAllowed] = value;
			}
		}

		// Token: 0x1700150E RID: 5390
		// (get) Token: 0x0600410B RID: 16651 RVA: 0x000F8ACD File Offset: 0x000F6CCD
		// (set) Token: 0x0600410C RID: 16652 RVA: 0x000F8ADF File Offset: 0x000F6CDF
		[Parameter(Mandatory = false)]
		public ServerStatus Status
		{
			get
			{
				return (ServerStatus)this[ServerSchema.Status];
			}
			set
			{
				this[ServerSchema.Status] = value;
			}
		}

		// Token: 0x1700150F RID: 5391
		// (get) Token: 0x0600410D RID: 16653 RVA: 0x000F8AF2 File Offset: 0x000F6CF2
		// (set) Token: 0x0600410E RID: 16654 RVA: 0x000F8B04 File Offset: 0x000F6D04
		public MultiValuedProperty<UMLanguage> Languages
		{
			get
			{
				return (MultiValuedProperty<UMLanguage>)this[ServerSchema.Languages];
			}
			internal set
			{
				this[ServerSchema.Languages] = value;
			}
		}

		// Token: 0x17001510 RID: 5392
		// (get) Token: 0x0600410F RID: 16655 RVA: 0x000F8B12 File Offset: 0x000F6D12
		// (set) Token: 0x06004110 RID: 16656 RVA: 0x000F8B24 File Offset: 0x000F6D24
		public MultiValuedProperty<ADObjectId> DialPlans
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ServerSchema.DialPlans];
			}
			set
			{
				this[ServerSchema.DialPlans] = value;
			}
		}

		// Token: 0x17001511 RID: 5393
		// (get) Token: 0x06004111 RID: 16657 RVA: 0x000F8B32 File Offset: 0x000F6D32
		// (set) Token: 0x06004112 RID: 16658 RVA: 0x000F8B44 File Offset: 0x000F6D44
		[Parameter(Mandatory = false)]
		public ScheduleInterval[] GrammarGenerationSchedule
		{
			get
			{
				return (ScheduleInterval[])this[ServerSchema.GrammarGenerationSchedule];
			}
			set
			{
				this[ServerSchema.GrammarGenerationSchedule] = value;
			}
		}

		// Token: 0x17001512 RID: 5394
		// (get) Token: 0x06004113 RID: 16659 RVA: 0x000F8B52 File Offset: 0x000F6D52
		// (set) Token: 0x06004114 RID: 16660 RVA: 0x000F8B64 File Offset: 0x000F6D64
		public UMSmartHost ExternalHostFqdn
		{
			get
			{
				return (UMSmartHost)this[ServerSchema.ExternalHostFqdn];
			}
			internal set
			{
				this[ServerSchema.ExternalHostFqdn] = value;
			}
		}

		// Token: 0x17001513 RID: 5395
		// (get) Token: 0x06004115 RID: 16661 RVA: 0x000F8B72 File Offset: 0x000F6D72
		// (set) Token: 0x06004116 RID: 16662 RVA: 0x000F8B84 File Offset: 0x000F6D84
		public MultiValuedProperty<ADObjectId> SubmissionServerOverrideList
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ServerSchema.SubmissionServerOverrideList];
			}
			set
			{
				this[ServerSchema.SubmissionServerOverrideList] = value;
			}
		}

		// Token: 0x17001514 RID: 5396
		// (get) Token: 0x06004117 RID: 16663 RVA: 0x000F8B92 File Offset: 0x000F6D92
		// (set) Token: 0x06004118 RID: 16664 RVA: 0x000F8BA4 File Offset: 0x000F6DA4
		public bool UseCustomReferralServerList
		{
			get
			{
				return (bool)this[ServerSchema.FolderAffinityCustom];
			}
			internal set
			{
				this[ServerSchema.FolderAffinityCustom] = value;
			}
		}

		// Token: 0x17001515 RID: 5397
		// (get) Token: 0x06004119 RID: 16665 RVA: 0x000F8BB7 File Offset: 0x000F6DB7
		// (set) Token: 0x0600411A RID: 16666 RVA: 0x000F8BC9 File Offset: 0x000F6DC9
		public MultiValuedProperty<ServerCostPair> CustomReferralServerList
		{
			get
			{
				return (MultiValuedProperty<ServerCostPair>)this[ServerSchema.FolderAffinityList];
			}
			internal set
			{
				this[ServerSchema.FolderAffinityList] = value;
			}
		}

		// Token: 0x17001516 RID: 5398
		// (get) Token: 0x0600411B RID: 16667 RVA: 0x000F8BD7 File Offset: 0x000F6DD7
		// (set) Token: 0x0600411C RID: 16668 RVA: 0x000F8BE9 File Offset: 0x000F6DE9
		public MultiValuedProperty<CultureInfo> Locale
		{
			get
			{
				return (MultiValuedProperty<CultureInfo>)this[ServerSchema.Locale];
			}
			internal set
			{
				this[ServerSchema.Locale] = value;
			}
		}

		// Token: 0x17001517 RID: 5399
		// (get) Token: 0x0600411D RID: 16669 RVA: 0x000F8BF7 File Offset: 0x000F6DF7
		// (set) Token: 0x0600411E RID: 16670 RVA: 0x000F8C09 File Offset: 0x000F6E09
		public bool? ErrorReportingEnabled
		{
			get
			{
				return (bool?)this[ServerSchema.ErrorReportingEnabled];
			}
			internal set
			{
				this[ServerSchema.ErrorReportingEnabled] = value;
			}
		}

		// Token: 0x17001518 RID: 5400
		// (get) Token: 0x0600411F RID: 16671 RVA: 0x000F8C1C File Offset: 0x000F6E1C
		// (set) Token: 0x06004120 RID: 16672 RVA: 0x000F8C2E File Offset: 0x000F6E2E
		public MultiValuedProperty<string> StaticDomainControllers
		{
			get
			{
				return (MultiValuedProperty<string>)this[ServerSchema.StaticDomainControllers];
			}
			internal set
			{
				this[ServerSchema.StaticDomainControllers] = value;
			}
		}

		// Token: 0x17001519 RID: 5401
		// (get) Token: 0x06004121 RID: 16673 RVA: 0x000F8C3C File Offset: 0x000F6E3C
		// (set) Token: 0x06004122 RID: 16674 RVA: 0x000F8C4E File Offset: 0x000F6E4E
		public MultiValuedProperty<string> StaticGlobalCatalogs
		{
			get
			{
				return (MultiValuedProperty<string>)this[ServerSchema.StaticGlobalCatalogs];
			}
			internal set
			{
				this[ServerSchema.StaticGlobalCatalogs] = value;
			}
		}

		// Token: 0x1700151A RID: 5402
		// (get) Token: 0x06004123 RID: 16675 RVA: 0x000F8C5C File Offset: 0x000F6E5C
		// (set) Token: 0x06004124 RID: 16676 RVA: 0x000F8C6E File Offset: 0x000F6E6E
		public string StaticConfigDomainController
		{
			get
			{
				return (string)this[ServerSchema.StaticConfigDomainController];
			}
			internal set
			{
				this[ServerSchema.StaticConfigDomainController] = value;
			}
		}

		// Token: 0x1700151B RID: 5403
		// (get) Token: 0x06004125 RID: 16677 RVA: 0x000F8C7C File Offset: 0x000F6E7C
		// (set) Token: 0x06004126 RID: 16678 RVA: 0x000F8C8E File Offset: 0x000F6E8E
		public MultiValuedProperty<string> StaticExcludedDomainControllers
		{
			get
			{
				return (MultiValuedProperty<string>)this[ServerSchema.StaticExcludedDomainControllers];
			}
			internal set
			{
				this[ServerSchema.StaticExcludedDomainControllers] = value;
			}
		}

		// Token: 0x1700151C RID: 5404
		// (get) Token: 0x06004127 RID: 16679 RVA: 0x000F8C9C File Offset: 0x000F6E9C
		// (set) Token: 0x06004128 RID: 16680 RVA: 0x000F8CAE File Offset: 0x000F6EAE
		public MultiValuedProperty<string> CurrentDomainControllers
		{
			get
			{
				return (MultiValuedProperty<string>)this[ServerSchema.CurrentDomainControllers];
			}
			internal set
			{
				this[ServerSchema.CurrentDomainControllers] = value;
			}
		}

		// Token: 0x1700151D RID: 5405
		// (get) Token: 0x06004129 RID: 16681 RVA: 0x000F8CBC File Offset: 0x000F6EBC
		// (set) Token: 0x0600412A RID: 16682 RVA: 0x000F8CCE File Offset: 0x000F6ECE
		public MultiValuedProperty<string> CurrentGlobalCatalogs
		{
			get
			{
				return (MultiValuedProperty<string>)this[ServerSchema.CurrentGlobalCatalogs];
			}
			internal set
			{
				this[ServerSchema.CurrentGlobalCatalogs] = value;
			}
		}

		// Token: 0x1700151E RID: 5406
		// (get) Token: 0x0600412B RID: 16683 RVA: 0x000F8CDC File Offset: 0x000F6EDC
		// (set) Token: 0x0600412C RID: 16684 RVA: 0x000F8CEE File Offset: 0x000F6EEE
		public string CurrentConfigDomainController
		{
			get
			{
				return (string)this[ServerSchema.CurrentConfigDomainController];
			}
			internal set
			{
				this[ServerSchema.CurrentConfigDomainController] = value;
			}
		}

		// Token: 0x1700151F RID: 5407
		// (get) Token: 0x0600412D RID: 16685 RVA: 0x000F8CFC File Offset: 0x000F6EFC
		// (set) Token: 0x0600412E RID: 16686 RVA: 0x000F8D0E File Offset: 0x000F6F0E
		public ADObjectId ServerSite
		{
			get
			{
				return (ADObjectId)this[ServerSchema.ServerSite];
			}
			internal set
			{
				this[ServerSchema.ServerSite] = value;
			}
		}

		// Token: 0x17001520 RID: 5408
		// (get) Token: 0x0600412F RID: 16687 RVA: 0x000F8D1C File Offset: 0x000F6F1C
		public ADObjectId[] Databases
		{
			get
			{
				lock (this)
				{
					if (this.databases == null)
					{
						Database[] array = this.GetDatabases();
						if (array != null)
						{
							List<ADObjectId> list = new List<ADObjectId>(array.Length);
							foreach (Database database in array)
							{
								if (database != null)
								{
									list.Add(database.Id);
								}
							}
							this.databases = list.ToArray();
						}
					}
				}
				return this.databases;
			}
		}

		// Token: 0x17001521 RID: 5409
		// (get) Token: 0x06004130 RID: 16688 RVA: 0x000F8DB0 File Offset: 0x000F6FB0
		// (set) Token: 0x06004131 RID: 16689 RVA: 0x000F8DC2 File Offset: 0x000F6FC2
		public int? MaximumActiveDatabases
		{
			get
			{
				return (int?)this[ServerSchema.MaxActiveMailboxDatabases];
			}
			internal set
			{
				this[ServerSchema.MaxActiveMailboxDatabases] = value;
			}
		}

		// Token: 0x17001522 RID: 5410
		// (get) Token: 0x06004132 RID: 16690 RVA: 0x000F8DD5 File Offset: 0x000F6FD5
		// (set) Token: 0x06004133 RID: 16691 RVA: 0x000F8DE7 File Offset: 0x000F6FE7
		public int? MaximumPreferredActiveDatabases
		{
			get
			{
				return (int?)this[ServerSchema.MaxPreferredActiveDatabases];
			}
			internal set
			{
				this[ServerSchema.MaxPreferredActiveDatabases] = value;
			}
		}

		// Token: 0x17001523 RID: 5411
		// (get) Token: 0x06004134 RID: 16692 RVA: 0x000F8DFA File Offset: 0x000F6FFA
		// (set) Token: 0x06004135 RID: 16693 RVA: 0x000F8E0C File Offset: 0x000F700C
		[Parameter(Mandatory = false)]
		public AutoDatabaseMountDial AutoDatabaseMountDial
		{
			get
			{
				return (AutoDatabaseMountDial)this[ActiveDirectoryServerSchema.AutoDatabaseMountDialType];
			}
			set
			{
				this[ActiveDirectoryServerSchema.AutoDatabaseMountDialType] = value;
			}
		}

		// Token: 0x17001524 RID: 5412
		// (get) Token: 0x06004136 RID: 16694 RVA: 0x000F8E1F File Offset: 0x000F701F
		// (set) Token: 0x06004137 RID: 16695 RVA: 0x000F8E31 File Offset: 0x000F7031
		public ADObjectId DatabaseAvailabilityGroup
		{
			get
			{
				return (ADObjectId)this[ServerSchema.DatabaseAvailabilityGroup];
			}
			internal set
			{
				this[ServerSchema.DatabaseAvailabilityGroup] = value;
			}
		}

		// Token: 0x17001525 RID: 5413
		// (get) Token: 0x06004138 RID: 16696 RVA: 0x000F8E3F File Offset: 0x000F703F
		// (set) Token: 0x06004139 RID: 16697 RVA: 0x000F8E51 File Offset: 0x000F7051
		[Parameter(Mandatory = false)]
		public DatabaseCopyAutoActivationPolicyType DatabaseCopyAutoActivationPolicy
		{
			get
			{
				return (DatabaseCopyAutoActivationPolicyType)this[ActiveDirectoryServerSchema.DatabaseCopyAutoActivationPolicy];
			}
			set
			{
				this[ActiveDirectoryServerSchema.DatabaseCopyAutoActivationPolicy] = value;
			}
		}

		// Token: 0x17001526 RID: 5414
		// (get) Token: 0x0600413A RID: 16698 RVA: 0x000F8E64 File Offset: 0x000F7064
		// (set) Token: 0x0600413B RID: 16699 RVA: 0x000F8E76 File Offset: 0x000F7076
		[Parameter(Mandatory = false)]
		public bool DatabaseCopyActivationDisabledAndMoveNow
		{
			get
			{
				return (bool)this[ActiveDirectoryServerSchema.DatabaseCopyActivationDisabledAndMoveNow];
			}
			set
			{
				this[ActiveDirectoryServerSchema.DatabaseCopyActivationDisabledAndMoveNow] = value;
			}
		}

		// Token: 0x17001527 RID: 5415
		// (get) Token: 0x0600413C RID: 16700 RVA: 0x000F8E89 File Offset: 0x000F7089
		// (set) Token: 0x0600413D RID: 16701 RVA: 0x000F8E9B File Offset: 0x000F709B
		internal ServerAutoDagFlags AutoDagFlags
		{
			get
			{
				return (ServerAutoDagFlags)this[ActiveDirectoryServerSchema.AutoDagFlags];
			}
			set
			{
				this[ActiveDirectoryServerSchema.AutoDagFlags] = value;
			}
		}

		// Token: 0x17001528 RID: 5416
		// (get) Token: 0x0600413E RID: 16702 RVA: 0x000F8EAE File Offset: 0x000F70AE
		// (set) Token: 0x0600413F RID: 16703 RVA: 0x000F8EC0 File Offset: 0x000F70C0
		[Parameter(Mandatory = false)]
		public string FaultZone
		{
			get
			{
				return (string)this[ActiveDirectoryServerSchema.FaultZone];
			}
			set
			{
				this[ActiveDirectoryServerSchema.FaultZone] = value;
			}
		}

		// Token: 0x17001529 RID: 5417
		// (get) Token: 0x06004140 RID: 16704 RVA: 0x000F8ECE File Offset: 0x000F70CE
		// (set) Token: 0x06004141 RID: 16705 RVA: 0x000F8EE0 File Offset: 0x000F70E0
		[Parameter(Mandatory = false)]
		public bool AutoDagServerConfigured
		{
			get
			{
				return (bool)this[ActiveDirectoryServerSchema.AutoDagServerConfigured];
			}
			set
			{
				this[ActiveDirectoryServerSchema.AutoDagServerConfigured] = value;
			}
		}

		// Token: 0x1700152A RID: 5418
		// (get) Token: 0x06004142 RID: 16706 RVA: 0x000F8EF3 File Offset: 0x000F70F3
		// (set) Token: 0x06004143 RID: 16707 RVA: 0x000F8F05 File Offset: 0x000F7105
		public string ProductID
		{
			get
			{
				return (string)this[ServerSchema.ProductID];
			}
			internal set
			{
				this[ServerSchema.ProductID] = value;
			}
		}

		// Token: 0x1700152B RID: 5419
		// (get) Token: 0x06004144 RID: 16708 RVA: 0x000F8F13 File Offset: 0x000F7113
		public bool IsExchangeTrialEdition
		{
			get
			{
				return (bool)this[ServerSchema.IsExchangeTrialEdition];
			}
		}

		// Token: 0x1700152C RID: 5420
		// (get) Token: 0x06004145 RID: 16709 RVA: 0x000F8F25 File Offset: 0x000F7125
		public bool IsExpiredExchangeTrialEdition
		{
			get
			{
				return (bool)this[ServerSchema.IsExpiredExchangeTrialEdition];
			}
		}

		// Token: 0x1700152D RID: 5421
		// (get) Token: 0x06004146 RID: 16710 RVA: 0x000F8F37 File Offset: 0x000F7137
		public EnhancedTimeSpan RemainingTrialPeriod
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.RemainingTrialPeriod];
			}
		}

		// Token: 0x1700152E RID: 5422
		// (get) Token: 0x06004147 RID: 16711 RVA: 0x000F8F49 File Offset: 0x000F7149
		// (set) Token: 0x06004148 RID: 16712 RVA: 0x000F8F5B File Offset: 0x000F715B
		[Parameter(Mandatory = false)]
		public bool TransportSyncEnabled
		{
			get
			{
				return (bool)this[ServerSchema.TransportSyncEnabled];
			}
			set
			{
				this[ServerSchema.TransportSyncEnabled] = value;
			}
		}

		// Token: 0x1700152F RID: 5423
		// (get) Token: 0x06004149 RID: 16713 RVA: 0x000F8F6E File Offset: 0x000F716E
		// (set) Token: 0x0600414A RID: 16714 RVA: 0x000F8F80 File Offset: 0x000F7180
		[Parameter(Mandatory = false)]
		public bool TransportSyncPopEnabled
		{
			get
			{
				return (bool)this[ServerSchema.TransportSyncPopEnabled];
			}
			set
			{
				this[ServerSchema.TransportSyncPopEnabled] = value;
			}
		}

		// Token: 0x17001530 RID: 5424
		// (get) Token: 0x0600414B RID: 16715 RVA: 0x000F8F93 File Offset: 0x000F7193
		// (set) Token: 0x0600414C RID: 16716 RVA: 0x000F8FA5 File Offset: 0x000F71A5
		[Parameter(Mandatory = false)]
		public bool WindowsLiveHotmailTransportSyncEnabled
		{
			get
			{
				return (bool)this[ServerSchema.WindowsLiveHotmailTransportSyncEnabled];
			}
			set
			{
				this[ServerSchema.WindowsLiveHotmailTransportSyncEnabled] = value;
			}
		}

		// Token: 0x17001531 RID: 5425
		// (get) Token: 0x0600414D RID: 16717 RVA: 0x000F8FB8 File Offset: 0x000F71B8
		// (set) Token: 0x0600414E RID: 16718 RVA: 0x000F8FCA File Offset: 0x000F71CA
		[Parameter(Mandatory = false)]
		public bool TransportSyncExchangeEnabled
		{
			get
			{
				return (bool)this[ServerSchema.TransportSyncExchangeEnabled];
			}
			set
			{
				this[ServerSchema.TransportSyncExchangeEnabled] = value;
			}
		}

		// Token: 0x17001532 RID: 5426
		// (get) Token: 0x0600414F RID: 16719 RVA: 0x000F8FDD File Offset: 0x000F71DD
		// (set) Token: 0x06004150 RID: 16720 RVA: 0x000F8FEF File Offset: 0x000F71EF
		[Parameter(Mandatory = false)]
		public bool TransportSyncImapEnabled
		{
			get
			{
				return (bool)this[ServerSchema.TransportSyncImapEnabled];
			}
			set
			{
				this[ServerSchema.TransportSyncImapEnabled] = value;
			}
		}

		// Token: 0x17001533 RID: 5427
		// (get) Token: 0x06004151 RID: 16721 RVA: 0x000F9002 File Offset: 0x000F7202
		// (set) Token: 0x06004152 RID: 16722 RVA: 0x000F9014 File Offset: 0x000F7214
		[Parameter(Mandatory = false)]
		public bool TransportSyncFacebookEnabled
		{
			get
			{
				return (bool)this[ServerSchema.TransportSyncFacebookEnabled];
			}
			set
			{
				this[ServerSchema.TransportSyncFacebookEnabled] = value;
			}
		}

		// Token: 0x17001534 RID: 5428
		// (get) Token: 0x06004153 RID: 16723 RVA: 0x000F9027 File Offset: 0x000F7227
		// (set) Token: 0x06004154 RID: 16724 RVA: 0x000F9039 File Offset: 0x000F7239
		[Parameter(Mandatory = false)]
		public bool TransportSyncDispatchEnabled
		{
			get
			{
				return (bool)this[ServerSchema.TransportSyncDispatchEnabled];
			}
			set
			{
				this[ServerSchema.TransportSyncDispatchEnabled] = value;
			}
		}

		// Token: 0x17001535 RID: 5429
		// (get) Token: 0x06004155 RID: 16725 RVA: 0x000F904C File Offset: 0x000F724C
		// (set) Token: 0x06004156 RID: 16726 RVA: 0x000F905E File Offset: 0x000F725E
		[Parameter(Mandatory = false)]
		public bool TransportSyncLinkedInEnabled
		{
			get
			{
				return (bool)this[ServerSchema.TransportSyncLinkedInEnabled];
			}
			set
			{
				this[ServerSchema.TransportSyncLinkedInEnabled] = value;
			}
		}

		// Token: 0x17001536 RID: 5430
		// (get) Token: 0x06004157 RID: 16727 RVA: 0x000F9071 File Offset: 0x000F7271
		// (set) Token: 0x06004158 RID: 16728 RVA: 0x000F9083 File Offset: 0x000F7283
		[Parameter(Mandatory = false)]
		public int MaxNumberOfTransportSyncAttempts
		{
			get
			{
				return (int)this[ServerSchema.MaxNumberOfTransportSyncAttempts];
			}
			set
			{
				this[ServerSchema.MaxNumberOfTransportSyncAttempts] = value;
			}
		}

		// Token: 0x17001537 RID: 5431
		// (get) Token: 0x06004159 RID: 16729 RVA: 0x000F9096 File Offset: 0x000F7296
		// (set) Token: 0x0600415A RID: 16730 RVA: 0x000F90A8 File Offset: 0x000F72A8
		[Parameter(Mandatory = false)]
		public int MaxAcceptedTransportSyncJobsPerProcessor
		{
			get
			{
				return (int)this[ServerSchema.MaxAcceptedTransportSyncJobsPerProcessor];
			}
			set
			{
				this[ServerSchema.MaxAcceptedTransportSyncJobsPerProcessor] = value;
			}
		}

		// Token: 0x17001538 RID: 5432
		// (get) Token: 0x0600415B RID: 16731 RVA: 0x000F90BB File Offset: 0x000F72BB
		// (set) Token: 0x0600415C RID: 16732 RVA: 0x000F90CD File Offset: 0x000F72CD
		[Parameter(Mandatory = false)]
		public int MaxActiveTransportSyncJobsPerProcessor
		{
			get
			{
				return (int)this[ServerSchema.MaxActiveTransportSyncJobsPerProcessor];
			}
			set
			{
				this[ServerSchema.MaxActiveTransportSyncJobsPerProcessor] = value;
			}
		}

		// Token: 0x17001539 RID: 5433
		// (get) Token: 0x0600415D RID: 16733 RVA: 0x000F90E0 File Offset: 0x000F72E0
		// (set) Token: 0x0600415E RID: 16734 RVA: 0x000F90F2 File Offset: 0x000F72F2
		[Parameter(Mandatory = false)]
		public string HttpTransportSyncProxyServer
		{
			get
			{
				return (string)this[ServerSchema.HttpTransportSyncProxyServer];
			}
			set
			{
				this[ServerSchema.HttpTransportSyncProxyServer] = value;
			}
		}

		// Token: 0x1700153A RID: 5434
		// (get) Token: 0x0600415F RID: 16735 RVA: 0x000F9100 File Offset: 0x000F7300
		// (set) Token: 0x06004160 RID: 16736 RVA: 0x000F9112 File Offset: 0x000F7312
		[Parameter(Mandatory = false)]
		public bool HttpProtocolLogEnabled
		{
			get
			{
				return (bool)this[ServerSchema.HttpProtocolLogEnabled];
			}
			set
			{
				this[ServerSchema.HttpProtocolLogEnabled] = value;
			}
		}

		// Token: 0x1700153B RID: 5435
		// (get) Token: 0x06004161 RID: 16737 RVA: 0x000F9125 File Offset: 0x000F7325
		// (set) Token: 0x06004162 RID: 16738 RVA: 0x000F9137 File Offset: 0x000F7337
		[Parameter(Mandatory = false)]
		public LocalLongFullPath HttpProtocolLogFilePath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.HttpProtocolLogFilePath];
			}
			set
			{
				this[ServerSchema.HttpProtocolLogFilePath] = value;
			}
		}

		// Token: 0x1700153C RID: 5436
		// (get) Token: 0x06004163 RID: 16739 RVA: 0x000F9145 File Offset: 0x000F7345
		// (set) Token: 0x06004164 RID: 16740 RVA: 0x000F9157 File Offset: 0x000F7357
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan HttpProtocolLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.HttpProtocolLogMaxAge];
			}
			set
			{
				this[ServerSchema.HttpProtocolLogMaxAge] = value;
			}
		}

		// Token: 0x1700153D RID: 5437
		// (get) Token: 0x06004165 RID: 16741 RVA: 0x000F916A File Offset: 0x000F736A
		// (set) Token: 0x06004166 RID: 16742 RVA: 0x000F917C File Offset: 0x000F737C
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize HttpProtocolLogMaxDirectorySize
		{
			get
			{
				return (ByteQuantifiedSize)this[ServerSchema.HttpProtocolLogMaxDirectorySize];
			}
			set
			{
				this[ServerSchema.HttpProtocolLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x1700153E RID: 5438
		// (get) Token: 0x06004167 RID: 16743 RVA: 0x000F918F File Offset: 0x000F738F
		// (set) Token: 0x06004168 RID: 16744 RVA: 0x000F91A1 File Offset: 0x000F73A1
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize HttpProtocolLogMaxFileSize
		{
			get
			{
				return (ByteQuantifiedSize)this[ServerSchema.HttpProtocolLogMaxFileSize];
			}
			set
			{
				this[ServerSchema.HttpProtocolLogMaxFileSize] = value;
			}
		}

		// Token: 0x1700153F RID: 5439
		// (get) Token: 0x06004169 RID: 16745 RVA: 0x000F91B4 File Offset: 0x000F73B4
		// (set) Token: 0x0600416A RID: 16746 RVA: 0x000F91C6 File Offset: 0x000F73C6
		[Parameter(Mandatory = false)]
		public ProtocolLoggingLevel HttpProtocolLogLoggingLevel
		{
			get
			{
				return (ProtocolLoggingLevel)this[ServerSchema.HttpProtocolLogLoggingLevel];
			}
			set
			{
				this[ServerSchema.HttpProtocolLogLoggingLevel] = value;
			}
		}

		// Token: 0x17001540 RID: 5440
		// (get) Token: 0x0600416B RID: 16747 RVA: 0x000F91D9 File Offset: 0x000F73D9
		// (set) Token: 0x0600416C RID: 16748 RVA: 0x000F91EB File Offset: 0x000F73EB
		[Parameter(Mandatory = false)]
		public bool TransportSyncLogEnabled
		{
			get
			{
				return (bool)this[ServerSchema.TransportSyncLogEnabled];
			}
			set
			{
				this[ServerSchema.TransportSyncLogEnabled] = value;
			}
		}

		// Token: 0x17001541 RID: 5441
		// (get) Token: 0x0600416D RID: 16749 RVA: 0x000F91FE File Offset: 0x000F73FE
		// (set) Token: 0x0600416E RID: 16750 RVA: 0x000F9210 File Offset: 0x000F7410
		[Parameter(Mandatory = false)]
		public SyncLoggingLevel TransportSyncLogLoggingLevel
		{
			get
			{
				return (SyncLoggingLevel)this[ServerSchema.TransportSyncLogLoggingLevel];
			}
			set
			{
				this[ServerSchema.TransportSyncLogLoggingLevel] = value;
			}
		}

		// Token: 0x17001542 RID: 5442
		// (get) Token: 0x0600416F RID: 16751 RVA: 0x000F9223 File Offset: 0x000F7423
		// (set) Token: 0x06004170 RID: 16752 RVA: 0x000F9235 File Offset: 0x000F7435
		[Parameter(Mandatory = false)]
		public LocalLongFullPath TransportSyncLogFilePath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.TransportSyncLogFilePath];
			}
			set
			{
				this[ServerSchema.TransportSyncLogFilePath] = value;
			}
		}

		// Token: 0x17001543 RID: 5443
		// (get) Token: 0x06004171 RID: 16753 RVA: 0x000F9243 File Offset: 0x000F7443
		// (set) Token: 0x06004172 RID: 16754 RVA: 0x000F9255 File Offset: 0x000F7455
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan TransportSyncLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.TransportSyncLogMaxAge];
			}
			set
			{
				this[ServerSchema.TransportSyncLogMaxAge] = value;
			}
		}

		// Token: 0x17001544 RID: 5444
		// (get) Token: 0x06004173 RID: 16755 RVA: 0x000F9268 File Offset: 0x000F7468
		// (set) Token: 0x06004174 RID: 16756 RVA: 0x000F927A File Offset: 0x000F747A
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize TransportSyncLogMaxDirectorySize
		{
			get
			{
				return (ByteQuantifiedSize)this[ServerSchema.TransportSyncLogMaxDirectorySize];
			}
			set
			{
				this[ServerSchema.TransportSyncLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17001545 RID: 5445
		// (get) Token: 0x06004175 RID: 16757 RVA: 0x000F928D File Offset: 0x000F748D
		// (set) Token: 0x06004176 RID: 16758 RVA: 0x000F929F File Offset: 0x000F749F
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize TransportSyncLogMaxFileSize
		{
			get
			{
				return (ByteQuantifiedSize)this[ServerSchema.TransportSyncLogMaxFileSize];
			}
			set
			{
				this[ServerSchema.TransportSyncLogMaxFileSize] = value;
			}
		}

		// Token: 0x17001546 RID: 5446
		// (get) Token: 0x06004177 RID: 16759 RVA: 0x000F92B2 File Offset: 0x000F74B2
		// (set) Token: 0x06004178 RID: 16760 RVA: 0x000F92C4 File Offset: 0x000F74C4
		[Parameter(Mandatory = false)]
		public bool TransportSyncHubHealthLogEnabled
		{
			get
			{
				return (bool)this[ActiveDirectoryServerSchema.TransportSyncHubHealthLogEnabled];
			}
			set
			{
				this[ActiveDirectoryServerSchema.TransportSyncHubHealthLogEnabled] = value;
			}
		}

		// Token: 0x17001547 RID: 5447
		// (get) Token: 0x06004179 RID: 16761 RVA: 0x000F92D7 File Offset: 0x000F74D7
		// (set) Token: 0x0600417A RID: 16762 RVA: 0x000F92E9 File Offset: 0x000F74E9
		[Parameter(Mandatory = false)]
		public LocalLongFullPath TransportSyncHubHealthLogFilePath
		{
			get
			{
				return (LocalLongFullPath)this[ActiveDirectoryServerSchema.TransportSyncHubHealthLogFilePath];
			}
			set
			{
				this[ActiveDirectoryServerSchema.TransportSyncHubHealthLogFilePath] = value;
			}
		}

		// Token: 0x17001548 RID: 5448
		// (get) Token: 0x0600417B RID: 16763 RVA: 0x000F92F7 File Offset: 0x000F74F7
		// (set) Token: 0x0600417C RID: 16764 RVA: 0x000F9309 File Offset: 0x000F7509
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan TransportSyncHubHealthLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ActiveDirectoryServerSchema.TransportSyncHubHealthLogMaxAge];
			}
			set
			{
				this[ActiveDirectoryServerSchema.TransportSyncHubHealthLogMaxAge] = value;
			}
		}

		// Token: 0x17001549 RID: 5449
		// (get) Token: 0x0600417D RID: 16765 RVA: 0x000F931C File Offset: 0x000F751C
		// (set) Token: 0x0600417E RID: 16766 RVA: 0x000F932E File Offset: 0x000F752E
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize TransportSyncHubHealthLogMaxDirectorySize
		{
			get
			{
				return (ByteQuantifiedSize)this[ActiveDirectoryServerSchema.TransportSyncHubHealthLogMaxDirectorySize];
			}
			set
			{
				this[ActiveDirectoryServerSchema.TransportSyncHubHealthLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x1700154A RID: 5450
		// (get) Token: 0x0600417F RID: 16767 RVA: 0x000F9341 File Offset: 0x000F7541
		// (set) Token: 0x06004180 RID: 16768 RVA: 0x000F9353 File Offset: 0x000F7553
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize TransportSyncHubHealthLogMaxFileSize
		{
			get
			{
				return (ByteQuantifiedSize)this[ActiveDirectoryServerSchema.TransportSyncHubHealthLogMaxFileSize];
			}
			set
			{
				this[ActiveDirectoryServerSchema.TransportSyncHubHealthLogMaxFileSize] = value;
			}
		}

		// Token: 0x1700154B RID: 5451
		// (get) Token: 0x06004181 RID: 16769 RVA: 0x000F9366 File Offset: 0x000F7566
		// (set) Token: 0x06004182 RID: 16770 RVA: 0x000F9378 File Offset: 0x000F7578
		[Parameter(Mandatory = false)]
		public bool TransportSyncAccountsPoisonDetectionEnabled
		{
			get
			{
				return (bool)this[ServerSchema.TransportSyncAccountsPoisonDetectionEnabled];
			}
			set
			{
				this[ServerSchema.TransportSyncAccountsPoisonDetectionEnabled] = value;
			}
		}

		// Token: 0x1700154C RID: 5452
		// (get) Token: 0x06004183 RID: 16771 RVA: 0x000F938B File Offset: 0x000F758B
		// (set) Token: 0x06004184 RID: 16772 RVA: 0x000F939D File Offset: 0x000F759D
		[Parameter(Mandatory = false)]
		public int TransportSyncAccountsPoisonAccountThreshold
		{
			get
			{
				return (int)this[ServerSchema.TransportSyncAccountsPoisonAccountThreshold];
			}
			set
			{
				this[ServerSchema.TransportSyncAccountsPoisonAccountThreshold] = value;
			}
		}

		// Token: 0x1700154D RID: 5453
		// (get) Token: 0x06004185 RID: 16773 RVA: 0x000F93B0 File Offset: 0x000F75B0
		// (set) Token: 0x06004186 RID: 16774 RVA: 0x000F93C2 File Offset: 0x000F75C2
		[Parameter(Mandatory = false)]
		public int TransportSyncAccountsPoisonItemThreshold
		{
			get
			{
				return (int)this[ServerSchema.TransportSyncAccountsPoisonItemThreshold];
			}
			set
			{
				this[ServerSchema.TransportSyncAccountsPoisonItemThreshold] = value;
			}
		}

		// Token: 0x1700154E RID: 5454
		// (get) Token: 0x06004187 RID: 16775 RVA: 0x000F93D5 File Offset: 0x000F75D5
		// (set) Token: 0x06004188 RID: 16776 RVA: 0x000F93E7 File Offset: 0x000F75E7
		[Parameter(Mandatory = false)]
		public int TransportSyncAccountsSuccessivePoisonItemThreshold
		{
			get
			{
				return (int)this[ActiveDirectoryServerSchema.TransportSyncAccountsSuccessivePoisonItemThreshold];
			}
			set
			{
				this[ActiveDirectoryServerSchema.TransportSyncAccountsSuccessivePoisonItemThreshold] = value;
			}
		}

		// Token: 0x1700154F RID: 5455
		// (get) Token: 0x06004189 RID: 16777 RVA: 0x000F93FA File Offset: 0x000F75FA
		// (set) Token: 0x0600418A RID: 16778 RVA: 0x000F940C File Offset: 0x000F760C
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan TransportSyncRemoteConnectionTimeout
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.TransportSyncRemoteConnectionTimeout];
			}
			set
			{
				this[ServerSchema.TransportSyncRemoteConnectionTimeout] = value;
			}
		}

		// Token: 0x17001550 RID: 5456
		// (get) Token: 0x0600418B RID: 16779 RVA: 0x000F941F File Offset: 0x000F761F
		// (set) Token: 0x0600418C RID: 16780 RVA: 0x000F9431 File Offset: 0x000F7631
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize TransportSyncMaxDownloadSizePerItem
		{
			get
			{
				return (ByteQuantifiedSize)this[ServerSchema.TransportSyncMaxDownloadSizePerItem];
			}
			set
			{
				this[ServerSchema.TransportSyncMaxDownloadSizePerItem] = value;
			}
		}

		// Token: 0x17001551 RID: 5457
		// (get) Token: 0x0600418D RID: 16781 RVA: 0x000F9444 File Offset: 0x000F7644
		// (set) Token: 0x0600418E RID: 16782 RVA: 0x000F9456 File Offset: 0x000F7656
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize TransportSyncMaxDownloadSizePerConnection
		{
			get
			{
				return (ByteQuantifiedSize)this[ServerSchema.TransportSyncMaxDownloadSizePerConnection];
			}
			set
			{
				this[ServerSchema.TransportSyncMaxDownloadSizePerConnection] = value;
			}
		}

		// Token: 0x17001552 RID: 5458
		// (get) Token: 0x0600418F RID: 16783 RVA: 0x000F9469 File Offset: 0x000F7669
		// (set) Token: 0x06004190 RID: 16784 RVA: 0x000F947B File Offset: 0x000F767B
		[Parameter(Mandatory = false)]
		public int TransportSyncMaxDownloadItemsPerConnection
		{
			get
			{
				return (int)this[ServerSchema.TransportSyncMaxDownloadItemsPerConnection];
			}
			set
			{
				this[ServerSchema.TransportSyncMaxDownloadItemsPerConnection] = value;
			}
		}

		// Token: 0x17001553 RID: 5459
		// (get) Token: 0x06004191 RID: 16785 RVA: 0x000F948E File Offset: 0x000F768E
		// (set) Token: 0x06004192 RID: 16786 RVA: 0x000F94A0 File Offset: 0x000F76A0
		[Parameter(Mandatory = false)]
		public string DeltaSyncClientCertificateThumbprint
		{
			get
			{
				return (string)this[ServerSchema.DeltaSyncClientCertificateThumbprint];
			}
			set
			{
				this[ServerSchema.DeltaSyncClientCertificateThumbprint] = value;
			}
		}

		// Token: 0x17001554 RID: 5460
		// (get) Token: 0x06004193 RID: 16787 RVA: 0x000F94AE File Offset: 0x000F76AE
		// (set) Token: 0x06004194 RID: 16788 RVA: 0x000F94C0 File Offset: 0x000F76C0
		[Parameter(Mandatory = false)]
		public int MaxTransportSyncDispatchers
		{
			get
			{
				return (int)this[ServerSchema.MaxTransportSyncDispatchers];
			}
			set
			{
				this[ServerSchema.MaxTransportSyncDispatchers] = value;
			}
		}

		// Token: 0x17001555 RID: 5461
		// (get) Token: 0x06004195 RID: 16789 RVA: 0x000F94D3 File Offset: 0x000F76D3
		// (set) Token: 0x06004196 RID: 16790 RVA: 0x000F94E5 File Offset: 0x000F76E5
		[Parameter(Mandatory = false)]
		public bool TransportSyncMailboxLogEnabled
		{
			get
			{
				return (bool)this[ServerSchema.TransportSyncMailboxLogEnabled];
			}
			set
			{
				this[ServerSchema.TransportSyncMailboxLogEnabled] = value;
			}
		}

		// Token: 0x17001556 RID: 5462
		// (get) Token: 0x06004197 RID: 16791 RVA: 0x000F94F8 File Offset: 0x000F76F8
		// (set) Token: 0x06004198 RID: 16792 RVA: 0x000F950A File Offset: 0x000F770A
		[Parameter(Mandatory = false)]
		public SyncLoggingLevel TransportSyncMailboxLogLoggingLevel
		{
			get
			{
				return (SyncLoggingLevel)this[ServerSchema.TransportSyncMailboxLogLoggingLevel];
			}
			set
			{
				this[ServerSchema.TransportSyncMailboxLogLoggingLevel] = value;
			}
		}

		// Token: 0x17001557 RID: 5463
		// (get) Token: 0x06004199 RID: 16793 RVA: 0x000F951D File Offset: 0x000F771D
		// (set) Token: 0x0600419A RID: 16794 RVA: 0x000F952F File Offset: 0x000F772F
		[Parameter(Mandatory = false)]
		public LocalLongFullPath TransportSyncMailboxLogFilePath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.TransportSyncMailboxLogFilePath];
			}
			set
			{
				this[ServerSchema.TransportSyncMailboxLogFilePath] = value;
			}
		}

		// Token: 0x17001558 RID: 5464
		// (get) Token: 0x0600419B RID: 16795 RVA: 0x000F953D File Offset: 0x000F773D
		// (set) Token: 0x0600419C RID: 16796 RVA: 0x000F954F File Offset: 0x000F774F
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan TransportSyncMailboxLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.TransportSyncMailboxLogMaxAge];
			}
			set
			{
				this[ServerSchema.TransportSyncMailboxLogMaxAge] = value;
			}
		}

		// Token: 0x17001559 RID: 5465
		// (get) Token: 0x0600419D RID: 16797 RVA: 0x000F9562 File Offset: 0x000F7762
		// (set) Token: 0x0600419E RID: 16798 RVA: 0x000F9574 File Offset: 0x000F7774
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize TransportSyncMailboxLogMaxDirectorySize
		{
			get
			{
				return (ByteQuantifiedSize)this[ServerSchema.TransportSyncMailboxLogMaxDirectorySize];
			}
			set
			{
				this[ServerSchema.TransportSyncMailboxLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x1700155A RID: 5466
		// (get) Token: 0x0600419F RID: 16799 RVA: 0x000F9587 File Offset: 0x000F7787
		// (set) Token: 0x060041A0 RID: 16800 RVA: 0x000F9599 File Offset: 0x000F7799
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize TransportSyncMailboxLogMaxFileSize
		{
			get
			{
				return (ByteQuantifiedSize)this[ServerSchema.TransportSyncMailboxLogMaxFileSize];
			}
			set
			{
				this[ServerSchema.TransportSyncMailboxLogMaxFileSize] = value;
			}
		}

		// Token: 0x1700155B RID: 5467
		// (get) Token: 0x060041A1 RID: 16801 RVA: 0x000F95AC File Offset: 0x000F77AC
		// (set) Token: 0x060041A2 RID: 16802 RVA: 0x000F95BE File Offset: 0x000F77BE
		[Parameter(Mandatory = false)]
		public bool TransportSyncMailboxHealthLogEnabled
		{
			get
			{
				return (bool)this[ActiveDirectoryServerSchema.TransportSyncMailboxHealthLogEnabled];
			}
			set
			{
				this[ActiveDirectoryServerSchema.TransportSyncMailboxHealthLogEnabled] = value;
			}
		}

		// Token: 0x1700155C RID: 5468
		// (get) Token: 0x060041A3 RID: 16803 RVA: 0x000F95D1 File Offset: 0x000F77D1
		// (set) Token: 0x060041A4 RID: 16804 RVA: 0x000F95E3 File Offset: 0x000F77E3
		[Parameter(Mandatory = false)]
		public LocalLongFullPath TransportSyncMailboxHealthLogFilePath
		{
			get
			{
				return (LocalLongFullPath)this[ActiveDirectoryServerSchema.TransportSyncMailboxHealthLogFilePath];
			}
			set
			{
				this[ActiveDirectoryServerSchema.TransportSyncMailboxHealthLogFilePath] = value;
			}
		}

		// Token: 0x1700155D RID: 5469
		// (get) Token: 0x060041A5 RID: 16805 RVA: 0x000F95F1 File Offset: 0x000F77F1
		// (set) Token: 0x060041A6 RID: 16806 RVA: 0x000F9603 File Offset: 0x000F7803
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan TransportSyncMailboxHealthLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ActiveDirectoryServerSchema.TransportSyncMailboxHealthLogMaxAge];
			}
			set
			{
				this[ActiveDirectoryServerSchema.TransportSyncMailboxHealthLogMaxAge] = value;
			}
		}

		// Token: 0x1700155E RID: 5470
		// (get) Token: 0x060041A7 RID: 16807 RVA: 0x000F9616 File Offset: 0x000F7816
		// (set) Token: 0x060041A8 RID: 16808 RVA: 0x000F9628 File Offset: 0x000F7828
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize TransportSyncMailboxHealthLogMaxDirectorySize
		{
			get
			{
				return (ByteQuantifiedSize)this[ActiveDirectoryServerSchema.TransportSyncMailboxHealthLogMaxDirectorySize];
			}
			set
			{
				this[ActiveDirectoryServerSchema.TransportSyncMailboxHealthLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x1700155F RID: 5471
		// (get) Token: 0x060041A9 RID: 16809 RVA: 0x000F963B File Offset: 0x000F783B
		// (set) Token: 0x060041AA RID: 16810 RVA: 0x000F964D File Offset: 0x000F784D
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize TransportSyncMailboxHealthLogMaxFileSize
		{
			get
			{
				return (ByteQuantifiedSize)this[ActiveDirectoryServerSchema.TransportSyncMailboxHealthLogMaxFileSize];
			}
			set
			{
				this[ActiveDirectoryServerSchema.TransportSyncMailboxHealthLogMaxFileSize] = value;
			}
		}

		// Token: 0x17001560 RID: 5472
		// (get) Token: 0x060041AB RID: 16811 RVA: 0x000F9660 File Offset: 0x000F7860
		// (set) Token: 0x060041AC RID: 16812 RVA: 0x000F968A File Offset: 0x000F788A
		public MailboxRelease MailboxRelease
		{
			get
			{
				MailboxRelease result;
				if (!Enum.TryParse<MailboxRelease>((string)this[ActiveDirectoryServerSchema.MailboxRelease], true, out result))
				{
					return MailboxRelease.None;
				}
				return result;
			}
			set
			{
				this[ActiveDirectoryServerSchema.MailboxRelease] = value.ToString();
			}
		}

		// Token: 0x17001561 RID: 5473
		// (get) Token: 0x060041AD RID: 16813 RVA: 0x000F96A2 File Offset: 0x000F78A2
		// (set) Token: 0x060041AE RID: 16814 RVA: 0x000F96B4 File Offset: 0x000F78B4
		public MailboxProvisioningAttributes MailboxProvisioningAttributes
		{
			get
			{
				return this[ServerSchema.MailboxProvisioningAttributes] as MailboxProvisioningAttributes;
			}
			set
			{
				this[ServerSchema.MailboxProvisioningAttributes] = value;
			}
		}

		// Token: 0x17001562 RID: 5474
		// (get) Token: 0x060041AF RID: 16815 RVA: 0x000F96C2 File Offset: 0x000F78C2
		internal long? ContinuousReplicationMaxMemoryPerDatabase
		{
			get
			{
				return (long?)this[ActiveDirectoryServerSchema.ContinuousReplicationMaxMemoryPerDatabase];
			}
		}

		// Token: 0x060041B0 RID: 16816 RVA: 0x000F96D4 File Offset: 0x000F78D4
		internal MailboxDatabase[] GetMailboxDatabases()
		{
			return this.GetDatabases<MailboxDatabase>();
		}

		// Token: 0x17001563 RID: 5475
		// (get) Token: 0x060041B1 RID: 16817 RVA: 0x000F96DC File Offset: 0x000F78DC
		public bool UseDowngradedExchangeServerAuth
		{
			get
			{
				return (bool)this[ActiveDirectoryServerSchema.UseDowngradedExchangeServerAuth];
			}
		}

		// Token: 0x17001564 RID: 5476
		// (get) Token: 0x060041B2 RID: 16818 RVA: 0x000F96EE File Offset: 0x000F78EE
		internal bool IsFfoWebServiceRole
		{
			get
			{
				return (this.CurrentServerRole & ServerRole.FfoWebService) == ServerRole.FfoWebService;
			}
		}

		// Token: 0x17001565 RID: 5477
		// (get) Token: 0x060041B3 RID: 16819 RVA: 0x000F9704 File Offset: 0x000F7904
		internal bool IsOSPRole
		{
			get
			{
				bool result;
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\ExchangeServer\\v15\\OspServerRole"))
				{
					result = (registryKey != null);
				}
				return result;
			}
		}

		// Token: 0x17001566 RID: 5478
		// (get) Token: 0x060041B4 RID: 16820 RVA: 0x000F9748 File Offset: 0x000F7948
		// (set) Token: 0x060041B5 RID: 16821 RVA: 0x000F975A File Offset: 0x000F795A
		public EnhancedTimeSpan QueueLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.QueueLogMaxAge];
			}
			set
			{
				this[ServerSchema.QueueLogMaxAge] = value;
			}
		}

		// Token: 0x17001567 RID: 5479
		// (get) Token: 0x060041B6 RID: 16822 RVA: 0x000F976D File Offset: 0x000F796D
		// (set) Token: 0x060041B7 RID: 16823 RVA: 0x000F977F File Offset: 0x000F797F
		public Unlimited<ByteQuantifiedSize> QueueLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.QueueLogMaxDirectorySize];
			}
			set
			{
				this[ServerSchema.QueueLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17001568 RID: 5480
		// (get) Token: 0x060041B8 RID: 16824 RVA: 0x000F9792 File Offset: 0x000F7992
		// (set) Token: 0x060041B9 RID: 16825 RVA: 0x000F97A4 File Offset: 0x000F79A4
		public Unlimited<ByteQuantifiedSize> QueueLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.QueueLogMaxFileSize];
			}
			set
			{
				this[ServerSchema.QueueLogMaxFileSize] = value;
			}
		}

		// Token: 0x17001569 RID: 5481
		// (get) Token: 0x060041BA RID: 16826 RVA: 0x000F97B7 File Offset: 0x000F79B7
		// (set) Token: 0x060041BB RID: 16827 RVA: 0x000F97C9 File Offset: 0x000F79C9
		public LocalLongFullPath QueueLogPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.QueueLogPath];
			}
			set
			{
				this[ServerSchema.QueueLogPath] = value;
			}
		}

		// Token: 0x1700156A RID: 5482
		// (get) Token: 0x060041BC RID: 16828 RVA: 0x000F97D7 File Offset: 0x000F79D7
		// (set) Token: 0x060041BD RID: 16829 RVA: 0x000F97E9 File Offset: 0x000F79E9
		public EnhancedTimeSpan WlmLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.WlmLogMaxAge];
			}
			set
			{
				this[ServerSchema.WlmLogMaxAge] = value;
			}
		}

		// Token: 0x1700156B RID: 5483
		// (get) Token: 0x060041BE RID: 16830 RVA: 0x000F97FC File Offset: 0x000F79FC
		// (set) Token: 0x060041BF RID: 16831 RVA: 0x000F980E File Offset: 0x000F7A0E
		public Unlimited<ByteQuantifiedSize> WlmLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.WlmLogMaxDirectorySize];
			}
			set
			{
				this[ServerSchema.WlmLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x1700156C RID: 5484
		// (get) Token: 0x060041C0 RID: 16832 RVA: 0x000F9821 File Offset: 0x000F7A21
		// (set) Token: 0x060041C1 RID: 16833 RVA: 0x000F9833 File Offset: 0x000F7A33
		public Unlimited<ByteQuantifiedSize> WlmLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.WlmLogMaxFileSize];
			}
			set
			{
				this[ServerSchema.WlmLogMaxFileSize] = value;
			}
		}

		// Token: 0x1700156D RID: 5485
		// (get) Token: 0x060041C2 RID: 16834 RVA: 0x000F9846 File Offset: 0x000F7A46
		// (set) Token: 0x060041C3 RID: 16835 RVA: 0x000F9858 File Offset: 0x000F7A58
		public LocalLongFullPath WlmLogPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.WlmLogPath];
			}
			set
			{
				this[ServerSchema.WlmLogPath] = value;
			}
		}

		// Token: 0x1700156E RID: 5486
		// (get) Token: 0x060041C4 RID: 16836 RVA: 0x000F9866 File Offset: 0x000F7A66
		// (set) Token: 0x060041C5 RID: 16837 RVA: 0x000F9878 File Offset: 0x000F7A78
		public EnhancedTimeSpan AgentLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.AgentLogMaxAge];
			}
			set
			{
				this[ServerSchema.AgentLogMaxAge] = value;
			}
		}

		// Token: 0x1700156F RID: 5487
		// (get) Token: 0x060041C6 RID: 16838 RVA: 0x000F988B File Offset: 0x000F7A8B
		// (set) Token: 0x060041C7 RID: 16839 RVA: 0x000F989D File Offset: 0x000F7A9D
		public Unlimited<ByteQuantifiedSize> AgentLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.AgentLogMaxDirectorySize];
			}
			set
			{
				this[ServerSchema.AgentLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17001570 RID: 5488
		// (get) Token: 0x060041C8 RID: 16840 RVA: 0x000F98B0 File Offset: 0x000F7AB0
		// (set) Token: 0x060041C9 RID: 16841 RVA: 0x000F98C2 File Offset: 0x000F7AC2
		public Unlimited<ByteQuantifiedSize> AgentLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.AgentLogMaxFileSize];
			}
			set
			{
				this[ServerSchema.AgentLogMaxFileSize] = value;
			}
		}

		// Token: 0x17001571 RID: 5489
		// (get) Token: 0x060041CA RID: 16842 RVA: 0x000F98D5 File Offset: 0x000F7AD5
		// (set) Token: 0x060041CB RID: 16843 RVA: 0x000F98E7 File Offset: 0x000F7AE7
		public LocalLongFullPath AgentLogPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.AgentLogPath];
			}
			set
			{
				this[ServerSchema.AgentLogPath] = value;
			}
		}

		// Token: 0x17001572 RID: 5490
		// (get) Token: 0x060041CC RID: 16844 RVA: 0x000F98F5 File Offset: 0x000F7AF5
		// (set) Token: 0x060041CD RID: 16845 RVA: 0x000F9907 File Offset: 0x000F7B07
		public bool AgentLogEnabled
		{
			get
			{
				return (bool)this[ServerSchema.AgentLogEnabled];
			}
			set
			{
				this[ServerSchema.AgentLogEnabled] = value;
			}
		}

		// Token: 0x17001573 RID: 5491
		// (get) Token: 0x060041CE RID: 16846 RVA: 0x000F991A File Offset: 0x000F7B1A
		// (set) Token: 0x060041CF RID: 16847 RVA: 0x000F9922 File Offset: 0x000F7B22
		public EnhancedTimeSpan AttributionLogMaxAge { get; set; }

		// Token: 0x17001574 RID: 5492
		// (get) Token: 0x060041D0 RID: 16848 RVA: 0x000F992B File Offset: 0x000F7B2B
		// (set) Token: 0x060041D1 RID: 16849 RVA: 0x000F9933 File Offset: 0x000F7B33
		public Unlimited<ByteQuantifiedSize> AttributionLogMaxDirectorySize { get; set; }

		// Token: 0x17001575 RID: 5493
		// (get) Token: 0x060041D2 RID: 16850 RVA: 0x000F993C File Offset: 0x000F7B3C
		// (set) Token: 0x060041D3 RID: 16851 RVA: 0x000F9944 File Offset: 0x000F7B44
		public Unlimited<ByteQuantifiedSize> AttributionLogMaxFileSize { get; set; }

		// Token: 0x17001576 RID: 5494
		// (get) Token: 0x060041D4 RID: 16852 RVA: 0x000F994D File Offset: 0x000F7B4D
		// (set) Token: 0x060041D5 RID: 16853 RVA: 0x000F9955 File Offset: 0x000F7B55
		public LocalLongFullPath AttributionLogPath { get; set; }

		// Token: 0x17001577 RID: 5495
		// (get) Token: 0x060041D6 RID: 16854 RVA: 0x000F995E File Offset: 0x000F7B5E
		// (set) Token: 0x060041D7 RID: 16855 RVA: 0x000F9966 File Offset: 0x000F7B66
		public bool AttributionLogEnabled { get; set; }

		// Token: 0x17001578 RID: 5496
		// (get) Token: 0x060041D8 RID: 16856 RVA: 0x000F996F File Offset: 0x000F7B6F
		// (set) Token: 0x060041D9 RID: 16857 RVA: 0x000F9981 File Offset: 0x000F7B81
		public EnhancedTimeSpan FlowControlLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.FlowControlLogMaxAge];
			}
			set
			{
				this[ServerSchema.FlowControlLogMaxAge] = value;
			}
		}

		// Token: 0x17001579 RID: 5497
		// (get) Token: 0x060041DA RID: 16858 RVA: 0x000F9994 File Offset: 0x000F7B94
		// (set) Token: 0x060041DB RID: 16859 RVA: 0x000F99A6 File Offset: 0x000F7BA6
		public Unlimited<ByteQuantifiedSize> FlowControlLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.FlowControlLogMaxDirectorySize];
			}
			set
			{
				this[ServerSchema.FlowControlLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x1700157A RID: 5498
		// (get) Token: 0x060041DC RID: 16860 RVA: 0x000F99B9 File Offset: 0x000F7BB9
		// (set) Token: 0x060041DD RID: 16861 RVA: 0x000F99CB File Offset: 0x000F7BCB
		public Unlimited<ByteQuantifiedSize> FlowControlLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.FlowControlLogMaxFileSize];
			}
			set
			{
				this[ServerSchema.FlowControlLogMaxFileSize] = value;
			}
		}

		// Token: 0x1700157B RID: 5499
		// (get) Token: 0x060041DE RID: 16862 RVA: 0x000F99DE File Offset: 0x000F7BDE
		// (set) Token: 0x060041DF RID: 16863 RVA: 0x000F99F0 File Offset: 0x000F7BF0
		public LocalLongFullPath FlowControlLogPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.FlowControlLogPath];
			}
			set
			{
				this[ServerSchema.FlowControlLogPath] = value;
			}
		}

		// Token: 0x1700157C RID: 5500
		// (get) Token: 0x060041E0 RID: 16864 RVA: 0x000F99FE File Offset: 0x000F7BFE
		// (set) Token: 0x060041E1 RID: 16865 RVA: 0x000F9A10 File Offset: 0x000F7C10
		public bool FlowControlLogEnabled
		{
			get
			{
				return (bool)this[ServerSchema.FlowControlLogEnabled];
			}
			set
			{
				this[ServerSchema.FlowControlLogEnabled] = value;
			}
		}

		// Token: 0x1700157D RID: 5501
		// (get) Token: 0x060041E2 RID: 16866 RVA: 0x000F9A23 File Offset: 0x000F7C23
		// (set) Token: 0x060041E3 RID: 16867 RVA: 0x000F9A35 File Offset: 0x000F7C35
		public EnhancedTimeSpan ProcessingSchedulerLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.ProcessingSchedulerLogMaxAge];
			}
			set
			{
				this[ServerSchema.ProcessingSchedulerLogMaxAge] = value;
			}
		}

		// Token: 0x1700157E RID: 5502
		// (get) Token: 0x060041E4 RID: 16868 RVA: 0x000F9A48 File Offset: 0x000F7C48
		// (set) Token: 0x060041E5 RID: 16869 RVA: 0x000F9A5A File Offset: 0x000F7C5A
		public Unlimited<ByteQuantifiedSize> ProcessingSchedulerLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.ProcessingSchedulerLogMaxDirectorySize];
			}
			set
			{
				this[ServerSchema.ProcessingSchedulerLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x1700157F RID: 5503
		// (get) Token: 0x060041E6 RID: 16870 RVA: 0x000F9A6D File Offset: 0x000F7C6D
		// (set) Token: 0x060041E7 RID: 16871 RVA: 0x000F9A7F File Offset: 0x000F7C7F
		public Unlimited<ByteQuantifiedSize> ProcessingSchedulerLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.ProcessingSchedulerLogMaxFileSize];
			}
			set
			{
				this[ServerSchema.ProcessingSchedulerLogMaxFileSize] = value;
			}
		}

		// Token: 0x17001580 RID: 5504
		// (get) Token: 0x060041E8 RID: 16872 RVA: 0x000F9A92 File Offset: 0x000F7C92
		// (set) Token: 0x060041E9 RID: 16873 RVA: 0x000F9AA4 File Offset: 0x000F7CA4
		public LocalLongFullPath ProcessingSchedulerLogPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.ProcessingSchedulerLogPath];
			}
			set
			{
				this[ServerSchema.ProcessingSchedulerLogPath] = value;
			}
		}

		// Token: 0x17001581 RID: 5505
		// (get) Token: 0x060041EA RID: 16874 RVA: 0x000F9AB2 File Offset: 0x000F7CB2
		// (set) Token: 0x060041EB RID: 16875 RVA: 0x000F9AC4 File Offset: 0x000F7CC4
		public bool ProcessingSchedulerLogEnabled
		{
			get
			{
				return (bool)this[ServerSchema.ProcessingSchedulerLogEnabled];
			}
			set
			{
				this[ServerSchema.ProcessingSchedulerLogEnabled] = value;
			}
		}

		// Token: 0x17001582 RID: 5506
		// (get) Token: 0x060041EC RID: 16876 RVA: 0x000F9AD7 File Offset: 0x000F7CD7
		// (set) Token: 0x060041ED RID: 16877 RVA: 0x000F9AE9 File Offset: 0x000F7CE9
		public EnhancedTimeSpan ResourceLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.ResourceLogMaxAge];
			}
			set
			{
				this[ServerSchema.ResourceLogMaxAge] = value;
			}
		}

		// Token: 0x17001583 RID: 5507
		// (get) Token: 0x060041EE RID: 16878 RVA: 0x000F9AFC File Offset: 0x000F7CFC
		// (set) Token: 0x060041EF RID: 16879 RVA: 0x000F9B0E File Offset: 0x000F7D0E
		public Unlimited<ByteQuantifiedSize> ResourceLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.ResourceLogMaxDirectorySize];
			}
			set
			{
				this[ServerSchema.ResourceLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17001584 RID: 5508
		// (get) Token: 0x060041F0 RID: 16880 RVA: 0x000F9B21 File Offset: 0x000F7D21
		// (set) Token: 0x060041F1 RID: 16881 RVA: 0x000F9B33 File Offset: 0x000F7D33
		public Unlimited<ByteQuantifiedSize> ResourceLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.ResourceLogMaxFileSize];
			}
			set
			{
				this[ServerSchema.ResourceLogMaxFileSize] = value;
			}
		}

		// Token: 0x17001585 RID: 5509
		// (get) Token: 0x060041F2 RID: 16882 RVA: 0x000F9B46 File Offset: 0x000F7D46
		// (set) Token: 0x060041F3 RID: 16883 RVA: 0x000F9B58 File Offset: 0x000F7D58
		public LocalLongFullPath ResourceLogPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.ResourceLogPath];
			}
			set
			{
				this[ServerSchema.ResourceLogPath] = value;
			}
		}

		// Token: 0x17001586 RID: 5510
		// (get) Token: 0x060041F4 RID: 16884 RVA: 0x000F9B66 File Offset: 0x000F7D66
		// (set) Token: 0x060041F5 RID: 16885 RVA: 0x000F9B78 File Offset: 0x000F7D78
		public bool ResourceLogEnabled
		{
			get
			{
				return (bool)this[ServerSchema.ResourceLogEnabled];
			}
			set
			{
				this[ServerSchema.ResourceLogEnabled] = value;
			}
		}

		// Token: 0x17001587 RID: 5511
		// (get) Token: 0x060041F6 RID: 16886 RVA: 0x000F9B8B File Offset: 0x000F7D8B
		// (set) Token: 0x060041F7 RID: 16887 RVA: 0x000F9B9D File Offset: 0x000F7D9D
		public EnhancedTimeSpan DnsLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.DnsLogMaxAge];
			}
			set
			{
				this[ServerSchema.DnsLogMaxAge] = value;
			}
		}

		// Token: 0x17001588 RID: 5512
		// (get) Token: 0x060041F8 RID: 16888 RVA: 0x000F9BB0 File Offset: 0x000F7DB0
		// (set) Token: 0x060041F9 RID: 16889 RVA: 0x000F9BC2 File Offset: 0x000F7DC2
		public Unlimited<ByteQuantifiedSize> DnsLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.DnsLogMaxDirectorySize];
			}
			set
			{
				this[ServerSchema.DnsLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17001589 RID: 5513
		// (get) Token: 0x060041FA RID: 16890 RVA: 0x000F9BD5 File Offset: 0x000F7DD5
		// (set) Token: 0x060041FB RID: 16891 RVA: 0x000F9BE7 File Offset: 0x000F7DE7
		public Unlimited<ByteQuantifiedSize> DnsLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.DnsLogMaxFileSize];
			}
			set
			{
				this[ServerSchema.DnsLogMaxFileSize] = value;
			}
		}

		// Token: 0x1700158A RID: 5514
		// (get) Token: 0x060041FC RID: 16892 RVA: 0x000F9BFA File Offset: 0x000F7DFA
		// (set) Token: 0x060041FD RID: 16893 RVA: 0x000F9C0C File Offset: 0x000F7E0C
		public LocalLongFullPath DnsLogPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.DnsLogPath];
			}
			set
			{
				this[ServerSchema.DnsLogPath] = value;
			}
		}

		// Token: 0x1700158B RID: 5515
		// (get) Token: 0x060041FE RID: 16894 RVA: 0x000F9C1A File Offset: 0x000F7E1A
		// (set) Token: 0x060041FF RID: 16895 RVA: 0x000F9C2C File Offset: 0x000F7E2C
		public bool DnsLogEnabled
		{
			get
			{
				return (bool)this[ServerSchema.DnsLogEnabled];
			}
			set
			{
				this[ServerSchema.DnsLogEnabled] = value;
			}
		}

		// Token: 0x1700158C RID: 5516
		// (get) Token: 0x06004200 RID: 16896 RVA: 0x000F9C3F File Offset: 0x000F7E3F
		// (set) Token: 0x06004201 RID: 16897 RVA: 0x000F9C51 File Offset: 0x000F7E51
		public EnhancedTimeSpan JournalLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.JournalLogMaxAge];
			}
			set
			{
				this[ServerSchema.JournalLogMaxAge] = value;
			}
		}

		// Token: 0x1700158D RID: 5517
		// (get) Token: 0x06004202 RID: 16898 RVA: 0x000F9C64 File Offset: 0x000F7E64
		// (set) Token: 0x06004203 RID: 16899 RVA: 0x000F9C76 File Offset: 0x000F7E76
		public Unlimited<ByteQuantifiedSize> JournalLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.JournalLogMaxDirectorySize];
			}
			set
			{
				this[ServerSchema.JournalLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x1700158E RID: 5518
		// (get) Token: 0x06004204 RID: 16900 RVA: 0x000F9C89 File Offset: 0x000F7E89
		// (set) Token: 0x06004205 RID: 16901 RVA: 0x000F9C9B File Offset: 0x000F7E9B
		public Unlimited<ByteQuantifiedSize> JournalLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.JournalLogMaxFileSize];
			}
			set
			{
				this[ServerSchema.JournalLogMaxFileSize] = value;
			}
		}

		// Token: 0x1700158F RID: 5519
		// (get) Token: 0x06004206 RID: 16902 RVA: 0x000F9CAE File Offset: 0x000F7EAE
		// (set) Token: 0x06004207 RID: 16903 RVA: 0x000F9CC0 File Offset: 0x000F7EC0
		public LocalLongFullPath JournalLogPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.JournalLogPath];
			}
			set
			{
				this[ServerSchema.JournalLogPath] = value;
			}
		}

		// Token: 0x17001590 RID: 5520
		// (get) Token: 0x06004208 RID: 16904 RVA: 0x000F9CCE File Offset: 0x000F7ECE
		// (set) Token: 0x06004209 RID: 16905 RVA: 0x000F9CE0 File Offset: 0x000F7EE0
		public bool JournalLogEnabled
		{
			get
			{
				return (bool)this[ServerSchema.JournalLogEnabled];
			}
			set
			{
				this[ServerSchema.JournalLogEnabled] = value;
			}
		}

		// Token: 0x17001591 RID: 5521
		// (get) Token: 0x0600420A RID: 16906 RVA: 0x000F9CF3 File Offset: 0x000F7EF3
		// (set) Token: 0x0600420B RID: 16907 RVA: 0x000F9D05 File Offset: 0x000F7F05
		public EnhancedTimeSpan TransportMaintenanceLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[ServerSchema.TransportMaintenanceLogMaxAge];
			}
			set
			{
				this[ServerSchema.TransportMaintenanceLogMaxAge] = value;
			}
		}

		// Token: 0x17001592 RID: 5522
		// (get) Token: 0x0600420C RID: 16908 RVA: 0x000F9D18 File Offset: 0x000F7F18
		// (set) Token: 0x0600420D RID: 16909 RVA: 0x000F9D2A File Offset: 0x000F7F2A
		public Unlimited<ByteQuantifiedSize> TransportMaintenanceLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.TransportMaintenanceLogMaxDirectorySize];
			}
			set
			{
				this[ServerSchema.TransportMaintenanceLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17001593 RID: 5523
		// (get) Token: 0x0600420E RID: 16910 RVA: 0x000F9D3D File Offset: 0x000F7F3D
		// (set) Token: 0x0600420F RID: 16911 RVA: 0x000F9D4F File Offset: 0x000F7F4F
		public Unlimited<ByteQuantifiedSize> TransportMaintenanceLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ServerSchema.TransportMaintenanceLogMaxFileSize];
			}
			set
			{
				this[ServerSchema.TransportMaintenanceLogMaxFileSize] = value;
			}
		}

		// Token: 0x17001594 RID: 5524
		// (get) Token: 0x06004210 RID: 16912 RVA: 0x000F9D62 File Offset: 0x000F7F62
		// (set) Token: 0x06004211 RID: 16913 RVA: 0x000F9D74 File Offset: 0x000F7F74
		public LocalLongFullPath TransportMaintenanceLogPath
		{
			get
			{
				return (LocalLongFullPath)this[ServerSchema.TransportMaintenanceLogPath];
			}
			set
			{
				this[ServerSchema.TransportMaintenanceLogPath] = value;
			}
		}

		// Token: 0x17001595 RID: 5525
		// (get) Token: 0x06004212 RID: 16914 RVA: 0x000F9D82 File Offset: 0x000F7F82
		// (set) Token: 0x06004213 RID: 16915 RVA: 0x000F9D94 File Offset: 0x000F7F94
		public bool TransportMaintenanceLogEnabled
		{
			get
			{
				return (bool)this[ServerSchema.TransportMaintenanceLogEnabled];
			}
			set
			{
				this[ServerSchema.TransportMaintenanceLogEnabled] = value;
			}
		}

		// Token: 0x17001596 RID: 5526
		// (get) Token: 0x06004214 RID: 16916 RVA: 0x000F9DA7 File Offset: 0x000F7FA7
		// (set) Token: 0x06004215 RID: 16917 RVA: 0x000F9DAF File Offset: 0x000F7FAF
		public int MaxReceiveTlsRatePerMinute { get; set; }

		// Token: 0x17001597 RID: 5527
		// (get) Token: 0x06004216 RID: 16918 RVA: 0x000F9DB8 File Offset: 0x000F7FB8
		// (set) Token: 0x06004217 RID: 16919 RVA: 0x000F9DC0 File Offset: 0x000F7FC0
		public EnhancedTimeSpan MailboxDeliveryAgentLogMaxAge { get; set; }

		// Token: 0x17001598 RID: 5528
		// (get) Token: 0x06004218 RID: 16920 RVA: 0x000F9DC9 File Offset: 0x000F7FC9
		// (set) Token: 0x06004219 RID: 16921 RVA: 0x000F9DD1 File Offset: 0x000F7FD1
		public Unlimited<ByteQuantifiedSize> MailboxDeliveryAgentLogMaxDirectorySize { get; set; }

		// Token: 0x17001599 RID: 5529
		// (get) Token: 0x0600421A RID: 16922 RVA: 0x000F9DDA File Offset: 0x000F7FDA
		// (set) Token: 0x0600421B RID: 16923 RVA: 0x000F9DE2 File Offset: 0x000F7FE2
		public Unlimited<ByteQuantifiedSize> MailboxDeliveryAgentLogMaxFileSize { get; set; }

		// Token: 0x1700159A RID: 5530
		// (get) Token: 0x0600421C RID: 16924 RVA: 0x000F9DEB File Offset: 0x000F7FEB
		// (set) Token: 0x0600421D RID: 16925 RVA: 0x000F9DF3 File Offset: 0x000F7FF3
		public LocalLongFullPath MailboxDeliveryAgentLogPath { get; set; }

		// Token: 0x1700159B RID: 5531
		// (get) Token: 0x0600421E RID: 16926 RVA: 0x000F9DFC File Offset: 0x000F7FFC
		// (set) Token: 0x0600421F RID: 16927 RVA: 0x000F9E04 File Offset: 0x000F8004
		public bool MailboxDeliveryAgentLogEnabled { get; set; }

		// Token: 0x1700159C RID: 5532
		// (get) Token: 0x06004220 RID: 16928 RVA: 0x000F9E0D File Offset: 0x000F800D
		// (set) Token: 0x06004221 RID: 16929 RVA: 0x000F9E15 File Offset: 0x000F8015
		public EnhancedTimeSpan MailboxSubmissionAgentLogMaxAge { get; set; }

		// Token: 0x1700159D RID: 5533
		// (get) Token: 0x06004222 RID: 16930 RVA: 0x000F9E1E File Offset: 0x000F801E
		// (set) Token: 0x06004223 RID: 16931 RVA: 0x000F9E26 File Offset: 0x000F8026
		public Unlimited<ByteQuantifiedSize> MailboxSubmissionAgentLogMaxDirectorySize { get; set; }

		// Token: 0x1700159E RID: 5534
		// (get) Token: 0x06004224 RID: 16932 RVA: 0x000F9E2F File Offset: 0x000F802F
		// (set) Token: 0x06004225 RID: 16933 RVA: 0x000F9E37 File Offset: 0x000F8037
		public Unlimited<ByteQuantifiedSize> MailboxSubmissionAgentLogMaxFileSize { get; set; }

		// Token: 0x1700159F RID: 5535
		// (get) Token: 0x06004226 RID: 16934 RVA: 0x000F9E40 File Offset: 0x000F8040
		// (set) Token: 0x06004227 RID: 16935 RVA: 0x000F9E48 File Offset: 0x000F8048
		public LocalLongFullPath MailboxSubmissionAgentLogPath { get; set; }

		// Token: 0x170015A0 RID: 5536
		// (get) Token: 0x06004228 RID: 16936 RVA: 0x000F9E51 File Offset: 0x000F8051
		// (set) Token: 0x06004229 RID: 16937 RVA: 0x000F9E59 File Offset: 0x000F8059
		public bool MailboxSubmissionAgentLogEnabled { get; set; }

		// Token: 0x170015A1 RID: 5537
		// (get) Token: 0x0600422A RID: 16938 RVA: 0x000F9E62 File Offset: 0x000F8062
		// (set) Token: 0x0600422B RID: 16939 RVA: 0x000F9E6A File Offset: 0x000F806A
		public bool MailboxDeliveryThrottlingLogEnabled { get; set; }

		// Token: 0x170015A2 RID: 5538
		// (get) Token: 0x0600422C RID: 16940 RVA: 0x000F9E73 File Offset: 0x000F8073
		// (set) Token: 0x0600422D RID: 16941 RVA: 0x000F9E7B File Offset: 0x000F807B
		public EnhancedTimeSpan MailboxDeliveryThrottlingLogMaxAge { get; set; }

		// Token: 0x170015A3 RID: 5539
		// (get) Token: 0x0600422E RID: 16942 RVA: 0x000F9E84 File Offset: 0x000F8084
		// (set) Token: 0x0600422F RID: 16943 RVA: 0x000F9E8C File Offset: 0x000F808C
		public Unlimited<ByteQuantifiedSize> MailboxDeliveryThrottlingLogMaxDirectorySize { get; set; }

		// Token: 0x170015A4 RID: 5540
		// (get) Token: 0x06004230 RID: 16944 RVA: 0x000F9E95 File Offset: 0x000F8095
		// (set) Token: 0x06004231 RID: 16945 RVA: 0x000F9E9D File Offset: 0x000F809D
		public Unlimited<ByteQuantifiedSize> MailboxDeliveryThrottlingLogMaxFileSize { get; set; }

		// Token: 0x170015A5 RID: 5541
		// (get) Token: 0x06004232 RID: 16946 RVA: 0x000F9EA6 File Offset: 0x000F80A6
		// (set) Token: 0x06004233 RID: 16947 RVA: 0x000F9EAE File Offset: 0x000F80AE
		public LocalLongFullPath MailboxDeliveryThrottlingLogPath { get; set; }

		// Token: 0x06004234 RID: 16948 RVA: 0x000F9EB7 File Offset: 0x000F80B7
		internal PublicFolderDatabase[] GetPublicFolderDatabases()
		{
			return this.GetPublicFolderDatabases(null);
		}

		// Token: 0x06004235 RID: 16949 RVA: 0x000F9EC0 File Offset: 0x000F80C0
		internal PublicFolderDatabase[] GetMapiPublicFolderDatabases()
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, PublicFolderTreeSchema.PublicFolderTreeType, PublicFolderTreeType.Mapi);
			return this.GetPublicFolderDatabases(filter);
		}

		// Token: 0x06004236 RID: 16950 RVA: 0x000F9EE8 File Offset: 0x000F80E8
		private PublicFolderDatabase[] GetPublicFolderDatabases(QueryFilter filter)
		{
			List<PublicFolderDatabase> list = new List<PublicFolderDatabase>();
			PublicFolderDatabase[] array = this.GetDatabases<PublicFolderDatabase>();
			foreach (PublicFolderDatabase publicFolderDatabase in array)
			{
				PublicFolderTree[] array3 = base.Session.Find<PublicFolderTree>(publicFolderDatabase.PublicFolderHierarchy, QueryScope.SubTree, filter, null, 0);
				if (array3.Length > 0)
				{
					list.Add(publicFolderDatabase);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06004237 RID: 16951 RVA: 0x000F9F47 File Offset: 0x000F8147
		internal TDatabase[] GetDatabases<TDatabase>() where TDatabase : IConfigurable, new()
		{
			return this.GetDatabases<TDatabase>(false);
		}

		// Token: 0x06004238 RID: 16952 RVA: 0x000F9F50 File Offset: 0x000F8150
		internal TDatabase[] GetDatabases<TDatabase>(bool allowInvalidCopies) where TDatabase : IConfigurable, new()
		{
			if (base.Session == null)
			{
				throw new InvalidOperationException("Server object does not have a session reference, so cannot get databases.");
			}
			List<TDatabase> list = new List<TDatabase>();
			if (this.IsE14OrLater)
			{
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, base.Name);
				DatabaseCopy[] array = base.Session.Find<DatabaseCopy>(null, QueryScope.SubTree, filter, null, 0);
				foreach (DatabaseCopy databaseCopy in array)
				{
					if (databaseCopy.IsValidDatabaseCopy(allowInvalidCopies))
					{
						TDatabase database = databaseCopy.GetDatabase<TDatabase>();
						if (database != null)
						{
							list.Add(database);
						}
					}
				}
			}
			else
			{
				list.AddRange(base.Session.FindPaged<TDatabase>(null, base.Id, true, null, 0));
			}
			return list.ToArray();
		}

		// Token: 0x06004239 RID: 16953 RVA: 0x000FA002 File Offset: 0x000F8202
		internal Database[] GetDatabases()
		{
			return this.GetDatabases(false);
		}

		// Token: 0x0600423A RID: 16954 RVA: 0x000FA00B File Offset: 0x000F820B
		internal Database[] GetDatabases(bool allowInvalidCopies)
		{
			return this.GetDatabases<Database>(allowInvalidCopies);
		}

		// Token: 0x0600423B RID: 16955 RVA: 0x000FA014 File Offset: 0x000F8214
		internal string GetDomainOrComputerName()
		{
			return Server.GetDomainOrComputerName(this.propertyBag);
		}

		// Token: 0x0600423C RID: 16956 RVA: 0x000FA024 File Offset: 0x000F8224
		internal string GetAcceptedDomainOrDomainOrComputerName()
		{
			AcceptedDomain defaultAcceptedDomain = base.Session.GetDefaultAcceptedDomain();
			if (defaultAcceptedDomain != null && !string.IsNullOrEmpty(defaultAcceptedDomain.DomainName.Domain))
			{
				return defaultAcceptedDomain.DomainName.Domain;
			}
			return this.GetDomainOrComputerName();
		}

		// Token: 0x0600423D RID: 16957 RVA: 0x000FA064 File Offset: 0x000F8264
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if ((this.RetentionLogForManagedFoldersEnabled || this.JournalingLogForManagedFoldersEnabled || this.FolderLogForManagedFoldersEnabled) && this.LogPathForManagedFolders == null)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ElcAuditLogPathMissing, ActiveDirectoryServerSchema.ElcAuditLogPath, this));
			}
			string domain = this.Domain;
			if (this.EmptyDomainAllowed)
			{
				if (!string.IsNullOrEmpty(domain) && !SmtpAddress.IsValidDomain(domain))
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.InvalidDomain, ServerSchema.Domain, this));
				}
			}
			else if (!SmtpAddress.IsValidDomain(domain))
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.InvalidDomain, ServerSchema.Domain, this));
			}
			if (this.IsEdgeServer || this.IsHubTransportServer)
			{
				if (this.ReceiveProtocolLogMaxFileSize.CompareTo(this.ReceiveProtocolLogMaxDirectorySize) > 0)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.InvalidRcvProtocolLogSizeConfiguration, ServerSchema.ReceiveProtocolLogMaxFileSize, this));
				}
				if (this.SendProtocolLogMaxFileSize.CompareTo(this.SendProtocolLogMaxDirectorySize) > 0)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.InvalidSndProtocolLogSizeConfiguration, ServerSchema.SendProtocolLogMaxFileSize, this));
				}
				if (!this.MessageTrackingLogMaxDirectorySize.IsUnlimited && this.MessageTrackingLogMaxFileSize.CompareTo(this.MessageTrackingLogMaxDirectorySize.Value) > 0)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.InvalidMsgTrackingLogSizeConfiguration, ServerSchema.MessageTrackingLogMaxFileSize, this));
				}
				if (this.ActiveUserStatisticsLogMaxFileSize.CompareTo(this.ActiveUserStatisticsLogMaxDirectorySize) > 0)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.InvalidActiveUserStatisticsLogSizeConfiguration, ServerSchema.ActiveUserStatisticsLogMaxFileSize, this));
				}
				if (this.ServerStatisticsLogMaxFileSize.CompareTo(this.ServerStatisticsLogMaxDirectorySize) > 0)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.InvalidServerStatisticsLogSizeConfiguration, ServerSchema.ServerStatisticsLogMaxFileSize, this));
				}
				if (this.PickupDirectoryPath != null && this.ReplayDirectoryPath != null && this.PickupDirectoryPath.Equals(this.ReplayDirectoryPath))
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.InvalidDirectoryConfiguration, ServerSchema.PickupDirectoryPath, this));
				}
				if (!this.MaxOutboundConnections.IsUnlimited && (this.MaxPerDomainOutboundConnections.IsUnlimited || this.MaxPerDomainOutboundConnections.Value > this.MaxOutboundConnections.Value))
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.InvalidMaxOutboundConnectionConfiguration(this.MaxPerDomainOutboundConnections.ToString(), this.MaxOutboundConnections.ToString()), ServerSchema.MaxPerDomainOutboundConnections, this));
				}
				if (base.IsModified(ServerSchema.PipelineTracingEnabled) && this.PipelineTracingEnabled && (this.PipelineTracingSenderAddress == null || this.PipelineTracingSenderAddress.Equals(SmtpAddress.Empty) || null == this.PipelineTracingPath || string.IsNullOrEmpty(this.PipelineTracingPath.ToString())))
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.ErrorPipelineTracingRequirementsMissing, ServerSchema.PipelineTracingEnabled, this));
				}
				if (base.IsModified(ServerSchema.ContentConversionTracingEnabled) && this.ContentConversionTracingEnabled && (null == this.PipelineTracingPath || string.IsNullOrEmpty(this.PipelineTracingPath.ToString())))
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.ErrorPipelineTracingRequirementsMissing, ServerSchema.ContentConversionTracingEnabled, this));
				}
				if (!this.ExternalDNSAdapterEnabled && MultiValuedPropertyBase.IsNullOrEmpty(this.ExternalDNSServers))
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.ExternalDNSServersNotSet, ServerSchema.ExternalDNSServers, this));
				}
				if (!this.InternalDNSAdapterEnabled && MultiValuedPropertyBase.IsNullOrEmpty(this.InternalDNSServers))
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.InternalDNSServersNotSet, ServerSchema.InternalDNSServers, this));
				}
				if (this.HttpProtocolLogMaxFileSize.CompareTo(this.HttpProtocolLogMaxDirectorySize) > 0)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.InvalidHttpProtocolLogSizeConfiguration, ServerSchema.HttpProtocolLogMaxFileSize, this));
				}
				if (this.TransportSyncLogMaxFileSize.CompareTo(this.TransportSyncLogMaxDirectorySize) > 0)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.InvalidTransportSyncLogSizeConfiguration, ServerSchema.TransportSyncLogMaxFileSize, this));
				}
				if (this.TransportSyncMailboxLogMaxFileSize.CompareTo(this.TransportSyncMailboxLogMaxDirectorySize) > 0)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.InvalidTransportSyncLogSizeConfiguration, ServerSchema.TransportSyncMailboxLogMaxFileSize, this));
				}
				if (this.TransportSyncHubHealthLogMaxFileSize.CompareTo(this.TransportSyncHubHealthLogMaxDirectorySize) > 0)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.InvalidTransportSyncHealthLogSizeConfiguration, ActiveDirectoryServerSchema.TransportSyncHubHealthLogMaxFileSize, this));
				}
				if (this.TransportSyncMailboxHealthLogMaxFileSize.CompareTo(this.TransportSyncMailboxHealthLogMaxDirectorySize) > 0)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.InvalidTransportSyncHealthLogSizeConfiguration, ActiveDirectoryServerSchema.TransportSyncMailboxHealthLogMaxFileSize, this));
				}
				if (this.TransportSyncMaxDownloadSizePerItem.CompareTo(this.TransportSyncMaxDownloadSizePerConnection) > 0)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.InvalidTransportSyncDownloadSizeConfiguration, ServerSchema.TransportSyncMaxDownloadSizePerItem, this));
				}
			}
			if (this.SubmissionServerOverrideList != null && this.SubmissionServerOverrideList != MultiValuedProperty<ADObjectId>.Empty && this.SubmissionServerOverrideList.Added != null && this.SubmissionServerOverrideList.Added.Length != 0 && (this.CurrentServerRole & ServerRole.Mailbox) == ServerRole.None)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.SubmissionOverrideListOnWrongServer, ServerSchema.SubmissionServerOverrideList, this));
			}
		}

		// Token: 0x0600423E RID: 16958 RVA: 0x000FA564 File Offset: 0x000F8764
		internal static string GetParentLegacyDN(ITopologyConfigurationSession session)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}/cn=Configuration/cn=Servers", new object[]
			{
				session.GetAdministrativeGroup().LegacyExchangeDN
			});
		}

		// Token: 0x0600423F RID: 16959 RVA: 0x000FA596 File Offset: 0x000F8796
		internal static LegacyDN GetSystemAttendantLegacyDN(LegacyDN mailboxServerLegacyDN)
		{
			return mailboxServerLegacyDN.GetChildLegacyDN("cn", "Microsoft System Attendant");
		}

		// Token: 0x06004240 RID: 16960 RVA: 0x000FA5A8 File Offset: 0x000F87A8
		internal static string GetSystemAttendantLegacyDN(string mailboxServerLegacyDN)
		{
			return Server.GetSystemAttendantLegacyDN(LegacyDN.Parse(mailboxServerLegacyDN)).ToString();
		}

		// Token: 0x06004241 RID: 16961 RVA: 0x000FA5BA File Offset: 0x000F87BA
		internal ServerRoleOperationException GetServerRoleError(ServerRole role)
		{
			return new ServerRoleOperationException(DirectoryStrings.ErrorServerRoleNotSupported(base.Name));
		}

		// Token: 0x06004242 RID: 16962 RVA: 0x000FA5CC File Offset: 0x000F87CC
		internal static bool IsSubscribedGateway(ITopologyConfigurationSession session)
		{
			if (TopologyProvider.IsAdamTopology())
			{
				Server server = session.ReadLocalServer();
				return server != null && server.GatewayEdgeSyncSubscribed;
			}
			return false;
		}

		// Token: 0x06004243 RID: 16963 RVA: 0x000FA644 File Offset: 0x000F8844
		internal static ServerVersion GetServerVersion(string serverName)
		{
			if (!string.IsNullOrEmpty(serverName))
			{
				return ProvisioningCache.Instance.TryAddAndGetGlobalDictionaryValue<ServerVersion, string>(CannedProvisioningCacheKeys.ServerAdminDisplayVersionCacheKey, serverName, delegate()
				{
					ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 11218, "GetServerVersion", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\server.cs");
					MiniServer miniServer = topologyConfigurationSession.FindMiniServerByName(serverName, null);
					if (miniServer != null)
					{
						return miniServer.AdminDisplayVersion;
					}
					return null;
				});
			}
			return null;
		}

		// Token: 0x06004244 RID: 16964 RVA: 0x000FA690 File Offset: 0x000F8890
		internal static ClientAccessArray GetLocalServerClientAccessArray()
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 11233, "GetLocalServerClientAccessArray", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\server.cs");
			Server server = topologyConfigurationSession.ReadLocalServer();
			if (server != null && server.IsCafeServer && server.IsE15OrLater)
			{
				ADObjectId adobjectId = (ADObjectId)server[ServerSchema.ClientAccessArray];
				if (adobjectId != null)
				{
					QueryFilter filter = QueryFilter.AndTogether(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Id, adobjectId),
						QueryFilter.NotFilter(ClientAccessArray.PriorTo15ExchangeObjectVersionFilter)
					});
					return topologyConfigurationSession.FindUnique<ClientAccessArray>(null, QueryScope.SubTree, filter);
				}
			}
			return null;
		}

		// Token: 0x06004245 RID: 16965 RVA: 0x000FA72C File Offset: 0x000F892C
		internal bool HasExtendedRight(ClientSecurityContext clientSecurityContext, Guid extendedRightGuid)
		{
			SecurityDescriptor securityDescriptor = base.ReadSecurityDescriptorBlob();
			return securityDescriptor != null && clientSecurityContext.HasExtendedRightOnObject(securityDescriptor, extendedRightGuid);
		}

		// Token: 0x170015A6 RID: 5542
		// (get) Token: 0x06004246 RID: 16966 RVA: 0x000FA74D File Offset: 0x000F894D
		internal SmtpAddress? ExternalPostmasterAddress
		{
			get
			{
				return (SmtpAddress?)this[ServerSchema.ExternalPostmasterAddress];
			}
		}

		// Token: 0x04002CEF RID: 11503
		internal const string ExchangeTransportConfigContainerADObjectName = "Transport Configuration";

		// Token: 0x04002CF0 RID: 11504
		internal const string DefaultPostmasterAlias = "postmaster";

		// Token: 0x04002CF1 RID: 11505
		private const string UnknownDomain = "unknowndomain";

		// Token: 0x04002CF2 RID: 11506
		public const string DefaultFaultZone = "FaultZone1";

		// Token: 0x04002CF3 RID: 11507
		public const int DefaultCalendarRepairLogMaxAge = 10;

		// Token: 0x04002CF4 RID: 11508
		public const int DefaultMaxTransportSyncDispatchers = 5;

		// Token: 0x04002CF5 RID: 11509
		public const bool DefaultTransportSyncDispatchEnabled = true;

		// Token: 0x04002CF6 RID: 11510
		public const int DefaultTransportSyncLogMaxAge = 720;

		// Token: 0x04002CF7 RID: 11511
		public const int DefaultTransportSyncLogMaxDirectorySizeInGB = 10;

		// Token: 0x04002CF8 RID: 11512
		public const int DefaultTransportSyncMailboxLogMaxDirectorySizeInGB = 2;

		// Token: 0x04002CF9 RID: 11513
		public const int DefaultTransportSyncLogMaxFileSize = 10240;

		// Token: 0x04002CFA RID: 11514
		public const int DefaultTransportSyncAccountsPoisonAccountThreshold = 2;

		// Token: 0x04002CFB RID: 11515
		public const int DefaultTransportSyncAccountsPoisonItemThreshold = 2;

		// Token: 0x04002CFC RID: 11516
		public const int DefaultTransportSyncAccountsSuccessivePoisonItemThreshold = 3;

		// Token: 0x04002CFD RID: 11517
		public const int DefaultIrmLogMaxFileSizeInMB = 10;

		// Token: 0x04002CFE RID: 11518
		public const string NameValidationRegexPattern = "^[^`~!@#&\\^\\(\\)\\+\\[\\]\\{\\}\\<\\>\\?=,:|./\\\\; ]+$";

		// Token: 0x04002CFF RID: 11519
		public const string NameValidationSpaceAllowedRegexPattern = "^[^`~!@#&\\^\\(\\)\\+\\[\\]\\{\\}\\<\\>\\?=,:|./\\\\;]*$";

		// Token: 0x04002D00 RID: 11520
		internal static string MostDerivedClass = "msExchExchangeServer";

		// Token: 0x04002D01 RID: 11521
		public static readonly int Exchange2011MajorVersion = 15;

		// Token: 0x04002D02 RID: 11522
		public static readonly int Exchange2009MajorVersion = 14;

		// Token: 0x04002D03 RID: 11523
		public static readonly int CurrentExchangeMajorVersion = Server.Exchange2011MajorVersion;

		// Token: 0x04002D04 RID: 11524
		public static readonly int Exchange2007MajorVersion = 8;

		// Token: 0x04002D05 RID: 11525
		public static readonly int Exchange2000MajorVersion = 6;

		// Token: 0x04002D06 RID: 11526
		public static readonly int E2007MinVersion = 1912602624;

		// Token: 0x04002D07 RID: 11527
		public static readonly int E2007SP2MinVersion = 1912733696;

		// Token: 0x04002D08 RID: 11528
		public static readonly int E14MinVersion = 1937768448;

		// Token: 0x04002D09 RID: 11529
		public static readonly int E14SP1MinVersion = 1937833984;

		// Token: 0x04002D0A RID: 11530
		public static readonly int E15MinVersion = 1941962752;

		// Token: 0x04002D0B RID: 11531
		public static readonly int E16MinVersion = 1946157056;

		// Token: 0x04002D0C RID: 11532
		public static readonly int CurrentProductMinimumVersion = new ServerVersion(15, 0, 0, 0).ToInt();

		// Token: 0x04002D0D RID: 11533
		public static readonly int NextProductMinimumVersion = new ServerVersion(16, 0, 0, 0).ToInt();

		// Token: 0x04002D0E RID: 11534
		public static readonly int E2k3MinVersion = 6500;

		// Token: 0x04002D0F RID: 11535
		public static readonly int E2k3SP1MinVersion = 7226;

		// Token: 0x04002D10 RID: 11536
		public static readonly int E2k3SP2MinVersion = 7638;

		// Token: 0x04002D11 RID: 11537
		public static readonly int E2k3SP3MinVersion = 7720;

		// Token: 0x04002D12 RID: 11538
		public static readonly EnhancedTimeSpan Exchange2007TrialEditionExpirationPeriod = EnhancedTimeSpan.FromDays(120.0);

		// Token: 0x04002D13 RID: 11539
		public static readonly EnhancedTimeSpan E15TrialEditionExpirationPeriod = EnhancedTimeSpan.FromDays(180.0);

		// Token: 0x04002D14 RID: 11540
		private ADObjectId[] databases;

		// Token: 0x04002D15 RID: 11541
		[NonSerialized]
		private ADObjectSchema schema;

		// Token: 0x0200058E RID: 1422
		// (Invoke) Token: 0x0600424C RID: 16972
		internal delegate EnhancedTimeSpan? GetConfigurationDelegate(Server.AssistantConfigurationEntry entry);

		// Token: 0x0200058F RID: 1423
		// (Invoke) Token: 0x06004250 RID: 16976
		internal delegate Server.AssistantConfigurationEntry UpdateEntryDelegate(Server.AssistantConfigurationEntry entry, EnhancedTimeSpan value);

		// Token: 0x02000590 RID: 1424
		internal class AssistantConfigurationEntry
		{
			// Token: 0x170015A7 RID: 5543
			// (get) Token: 0x06004253 RID: 16979 RVA: 0x000FA855 File Offset: 0x000F8A55
			// (set) Token: 0x06004254 RID: 16980 RVA: 0x000FA85D File Offset: 0x000F8A5D
			public EnhancedTimeSpan WorkCycle { get; set; }

			// Token: 0x170015A8 RID: 5544
			// (get) Token: 0x06004255 RID: 16981 RVA: 0x000FA866 File Offset: 0x000F8A66
			// (set) Token: 0x06004256 RID: 16982 RVA: 0x000FA86E File Offset: 0x000F8A6E
			public EnhancedTimeSpan WorkCycleCheckpoint { get; set; }

			// Token: 0x06004257 RID: 16983 RVA: 0x000FA877 File Offset: 0x000F8A77
			public AssistantConfigurationEntry(TimeBasedAssistantIndex index, EnhancedTimeSpan workCycle, EnhancedTimeSpan workCycleCheckpoint)
			{
				this.index = index;
				this.WorkCycle = workCycle;
				this.WorkCycleCheckpoint = workCycleCheckpoint;
			}

			// Token: 0x06004258 RID: 16984 RVA: 0x000FA894 File Offset: 0x000F8A94
			public static Server.AssistantConfigurationEntry GetConfigurationForAssistant(MultiValuedProperty<string> allConfigurations, TimeBasedAssistantIndex assistantIndex)
			{
				string value = string.Format("{0},", (int)assistantIndex);
				foreach (string text in allConfigurations)
				{
					if (text.StartsWith(value))
					{
						string[] array = text.Split(new char[]
						{
							','
						});
						if (array != null && array.Length >= 3)
						{
							EnhancedTimeSpan zero;
							if (!EnhancedTimeSpan.TryParse(array[1], out zero))
							{
								zero = EnhancedTimeSpan.Zero;
							}
							EnhancedTimeSpan zero2;
							if (!EnhancedTimeSpan.TryParse(array[2], out zero2))
							{
								zero2 = EnhancedTimeSpan.Zero;
							}
							return new Server.AssistantConfigurationEntry(assistantIndex, zero, zero2);
						}
					}
				}
				return null;
			}

			// Token: 0x06004259 RID: 16985 RVA: 0x000FA94C File Offset: 0x000F8B4C
			public static bool IsAssistantConfiguration(string configuration, TimeBasedAssistantIndex assistantIndex)
			{
				return configuration.StartsWith(string.Format("{0},", (int)assistantIndex));
			}

			// Token: 0x0600425A RID: 16986 RVA: 0x000FA964 File Offset: 0x000F8B64
			public override string ToString()
			{
				return string.Format("{0},{1},{2}", (int)this.index, this.WorkCycle, this.WorkCycleCheckpoint);
			}

			// Token: 0x04002D2D RID: 11565
			private const string Format = "{0},{1},{2}";

			// Token: 0x04002D2E RID: 11566
			private const int AssistantIndex = 0;

			// Token: 0x04002D2F RID: 11567
			private const int WorkCycleIndex = 1;

			// Token: 0x04002D30 RID: 11568
			private const int CheckpointIndex = 2;

			// Token: 0x04002D31 RID: 11569
			private TimeBasedAssistantIndex index;
		}

		// Token: 0x02000591 RID: 1425
		private class MaintenanceScheduleEntry
		{
			// Token: 0x0600425B RID: 16987 RVA: 0x000FA991 File Offset: 0x000F8B91
			public MaintenanceScheduleEntry(ScheduledAssistant assistant, ScheduleInterval[] schedule)
			{
				this.Assistant = assistant;
				this.MaintenanceSchedule = schedule;
			}

			// Token: 0x0600425C RID: 16988 RVA: 0x000FA9A8 File Offset: 0x000F8BA8
			public static Server.MaintenanceScheduleEntry GetFromADString(string adString, ScheduledAssistant assistant)
			{
				if (string.IsNullOrEmpty(adString))
				{
					return null;
				}
				if (adString.Length != 84)
				{
					return null;
				}
				ScheduledAssistant scheduledAssistant = (ScheduledAssistant)adString[42];
				if (scheduledAssistant != assistant)
				{
					return null;
				}
				byte[] array = new byte[84];
				for (int i = 0; i < 42; i++)
				{
					array[i * 2] = (byte)(adString[i] >> 8);
					array[i * 2 + 1] = (byte)(adString[i] & 'ÿ');
				}
				return new Server.MaintenanceScheduleEntry(assistant, ScheduleInterval.GetIntervalsFromWeekBitmap(array));
			}

			// Token: 0x0600425D RID: 16989 RVA: 0x000FAA20 File Offset: 0x000F8C20
			public string ToADString()
			{
				byte[] weekBitmapFromIntervals = ScheduleInterval.GetWeekBitmapFromIntervals(this.MaintenanceSchedule);
				if (weekBitmapFromIntervals.Length != 84)
				{
					return null;
				}
				char[] array = new char[84];
				for (int i = 0; i < 42; i++)
				{
					int num = ((int)weekBitmapFromIntervals[i * 2] << 8) + (int)weekBitmapFromIntervals[i * 2 + 1];
					array[i] = (char)num;
				}
				array[42] = (char)this.Assistant;
				return new string(array);
			}

			// Token: 0x04002D34 RID: 11572
			private const int StringLengthLimit = 84;

			// Token: 0x04002D35 RID: 11573
			public readonly ScheduleInterval[] MaintenanceSchedule;

			// Token: 0x04002D36 RID: 11574
			public readonly ScheduledAssistant Assistant;
		}
	}
}
