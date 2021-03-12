using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Hybrid;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008D5 RID: 2261
	internal static class HybridConfigurationTaskUtility
	{
		// Token: 0x06005023 RID: 20515 RVA: 0x0014F7A2 File Offset: 0x0014D9A2
		public static MultiValuedProperty<ADObjectId> ValidateServers(ADPropertyDefinition propertyDefinition, IConfigDataProvider session, PropertyBag fields, HybridConfigurationTaskUtility.GetUniqueObject getServer, Task.TaskErrorLoggingDelegate writeError, params HybridConfigurationTaskUtility.ServerCriterion[] serverCriteria)
		{
			return HybridConfigurationTaskUtility.ValidateServers(propertyDefinition, session, fields[propertyDefinition.Name] as MultiValuedProperty<ServerIdParameter>, getServer, writeError, serverCriteria);
		}

		// Token: 0x06005024 RID: 20516 RVA: 0x0014F7C4 File Offset: 0x0014D9C4
		public static MultiValuedProperty<ADObjectId> ValidateServers(ADPropertyDefinition propertyDefinition, IConfigDataProvider session, MultiValuedProperty<ServerIdParameter> servers, HybridConfigurationTaskUtility.GetUniqueObject getServer, Task.TaskErrorLoggingDelegate writeError, params HybridConfigurationTaskUtility.ServerCriterion[] serverCriteria)
		{
			MultiValuedProperty<ADObjectId> multiValuedProperty = new MultiValuedProperty<ADObjectId>(false, propertyDefinition, new object[0]);
			if (servers != null)
			{
				foreach (ServerIdParameter serverIdParameter in servers)
				{
					if (serverIdParameter != null)
					{
						Server server = getServer(serverIdParameter, session, null, new LocalizedString?(Strings.ErrorServerNotFound(serverIdParameter.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(serverIdParameter.ToString()))) as Server;
						if (server != null)
						{
							if (serverCriteria != null)
							{
								foreach (HybridConfigurationTaskUtility.ServerCriterion serverCriterion in serverCriteria)
								{
									if (!serverCriterion.DoesMeet(server))
									{
										writeError(new InvalidOperationException(serverCriterion.Error(server.ToString())), ErrorCategory.InvalidOperation, server.ToString());
									}
								}
							}
							if (multiValuedProperty.Contains((ADObjectId)server.Identity))
							{
								writeError(new InvalidOperationException(HybridStrings.ErrorHybridServerAlreadyAssigned(server.Identity.ToString())), ErrorCategory.InvalidOperation, server.ToString());
							}
							else
							{
								multiValuedProperty.Add(((ADObjectId)server.Identity).DistinguishedName);
							}
						}
					}
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x06005025 RID: 20517 RVA: 0x0014F920 File Offset: 0x0014DB20
		public static MultiValuedProperty<IPRange> ValidateExternalIPAddresses(MultiValuedProperty<IPRange> externalIPAddresses, Task.TaskErrorLoggingDelegate writeError)
		{
			if (externalIPAddresses.Count > 40)
			{
				writeError(new ArgumentException(HybridStrings.ErrorHybridExternalIPAddressesExceeded40Entries), ErrorCategory.InvalidArgument, null);
				return null;
			}
			foreach (IPRange iprange in externalIPAddresses)
			{
				if (iprange.RangeFormat == IPRange.Format.LoHi)
				{
					writeError(new ArgumentException(HybridStrings.ErrorHybridExternalIPAddressesRangeAddressesNotSupported), ErrorCategory.InvalidArgument, null);
					return null;
				}
			}
			return externalIPAddresses;
		}

		// Token: 0x06005026 RID: 20518 RVA: 0x0014F9B4 File Offset: 0x0014DBB4
		public static int GetCount<T>(MultiValuedProperty<T> list)
		{
			if (list != null)
			{
				return list.Count;
			}
			return 0;
		}

		// Token: 0x020008D6 RID: 2262
		// (Invoke) Token: 0x06005028 RID: 20520
		public delegate IConfigurable GetUniqueObject(IIdentityParameter id, IConfigDataProvider session, ObjectId rootID, LocalizedString? notFoundError, LocalizedString? multipleFoundError);

		// Token: 0x020008D7 RID: 2263
		public class ServerCriterion
		{
			// Token: 0x0600502B RID: 20523 RVA: 0x0014F9C1 File Offset: 0x0014DBC1
			public ServerCriterion(Func<Server, bool> doesMeet, Func<string, LocalizedString> error)
			{
				this.DoesMeet = doesMeet;
				this.Error = error;
			}

			// Token: 0x170017EF RID: 6127
			// (get) Token: 0x0600502C RID: 20524 RVA: 0x0014F9D7 File Offset: 0x0014DBD7
			// (set) Token: 0x0600502D RID: 20525 RVA: 0x0014F9DF File Offset: 0x0014DBDF
			public Func<Server, bool> DoesMeet { get; private set; }

			// Token: 0x170017F0 RID: 6128
			// (get) Token: 0x0600502E RID: 20526 RVA: 0x0014F9E8 File Offset: 0x0014DBE8
			// (set) Token: 0x0600502F RID: 20527 RVA: 0x0014F9F0 File Offset: 0x0014DBF0
			public Func<string, LocalizedString> Error { get; private set; }
		}
	}
}
