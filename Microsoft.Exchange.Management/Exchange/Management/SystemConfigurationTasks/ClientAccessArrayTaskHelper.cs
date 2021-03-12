using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200099C RID: 2460
	internal sealed class ClientAccessArrayTaskHelper
	{
		// Token: 0x060057FA RID: 22522 RVA: 0x0016F70C File Offset: 0x0016D90C
		internal ClientAccessArrayTaskHelper(Task.TaskVerboseLoggingDelegate writeVerbose, Task.TaskErrorLoggingDelegate writeError)
		{
			this.writeVerbose = writeVerbose;
			this.writeError = writeError;
		}

		// Token: 0x060057FB RID: 22523 RVA: 0x0016F724 File Offset: 0x0016D924
		internal ADSite GetADSite(AdSiteIdParameter siteId, ITopologyConfigurationSession session, DataAccessHelper.GetDataObjectDelegate getDataObject)
		{
			this.writeVerbose(TaskVerboseStringHelper.GetFindByIdParameterVerboseString(siteId, session, typeof(ADSite), session.GetConfigurationNamingContext().GetChildId("Sites")));
			return (ADSite)getDataObject(siteId, session, null, null, new LocalizedString?(Strings.ErrorSiteNotFound(siteId.ToString())), new LocalizedString?(Strings.ErrorSiteNotUnique(siteId.ToString())));
		}

		// Token: 0x060057FC RID: 22524 RVA: 0x0016F78C File Offset: 0x0016D98C
		internal void VerifyArrayUniqueness(IConfigDataProvider session, ClientAccessArray array)
		{
			this.CheckUnique(session, array.Id, new Func<IConfigDataProvider, QueryFilter, bool>(this.ServerOrArrayExists), ADObjectSchema.Name, array.Name, Strings.ErrorCasArrayOrServerAlreadyExists(array.Name));
		}

		// Token: 0x060057FD RID: 22525 RVA: 0x0016F7C0 File Offset: 0x0016D9C0
		internal bool ObjectExists(IConfigDataProvider session, ADObjectId thisObjectId, Func<IConfigDataProvider, QueryFilter, bool> objectExists, PropertyDefinition keyProperty, object keyValue)
		{
			QueryFilter arg = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, keyProperty, keyValue),
				(thisObjectId != null) ? new ComparisonFilter(ComparisonOperator.NotEqual, ADObjectSchema.Id, thisObjectId) : null
			});
			return objectExists(session, arg);
		}

		// Token: 0x060057FE RID: 22526 RVA: 0x0016F805 File Offset: 0x0016DA05
		internal bool ServerOrArrayExists(IConfigDataProvider session, QueryFilter filter)
		{
			return this.ObjectExists<Server>(session, filter) || this.ObjectExists<ClientAccessArray>(session, filter);
		}

		// Token: 0x060057FF RID: 22527 RVA: 0x0016F81B File Offset: 0x0016DA1B
		internal QueryFilter GetSiteFilter(ADObjectId siteId)
		{
			if (siteId == null)
			{
				return null;
			}
			return new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.ServerSite, siteId);
		}

		// Token: 0x06005800 RID: 22528 RVA: 0x0016F82E File Offset: 0x0016DA2E
		private void CheckUnique(IConfigDataProvider session, ADObjectId thisObjectId, Func<IConfigDataProvider, QueryFilter, bool> objectExists, PropertyDefinition keyProperty, object keyValue, LocalizedString errorMessage)
		{
			if (this.ObjectExists(session, thisObjectId, objectExists, keyProperty, keyValue))
			{
				this.writeError(new InvalidOperationException(errorMessage), ErrorCategory.InvalidOperation, keyValue);
			}
		}

		// Token: 0x06005801 RID: 22529 RVA: 0x0016F859 File Offset: 0x0016DA59
		internal bool ObjectExists<T>(IConfigDataProvider session, QueryFilter filter) where T : ADConfigurationObject, new()
		{
			this.writeVerbose(TaskVerboseStringHelper.GetFindDataObjectsVerboseString(session, typeof(T), filter, null, true));
			return session.Find<T>(filter, null, true, null).Length > 0;
		}

		// Token: 0x06005802 RID: 22530 RVA: 0x0016F888 File Offset: 0x0016DA88
		internal string FindRpcClientAccessArrayOrServer(ITopologyConfigurationSession session, ADObjectId localServerId)
		{
			ADSite localSite = session.GetLocalSite();
			if (localSite == null)
			{
				return null;
			}
			ClientAccessArray[] array = session.Find<ClientAccessArray>(null, QueryScope.SubTree, QueryFilter.AndTogether(new QueryFilter[]
			{
				this.GetSiteFilter(localSite.Id),
				ClientAccessArray.PriorTo15ExchangeObjectVersionFilter
			}), null, 2);
			if (array.Length > 0)
			{
				return array[0].ExchangeLegacyDN;
			}
			List<string> list = new List<string>();
			foreach (KeyValuePair<Server, ExchangeRpcClientAccess> keyValuePair in ExchangeRpcClientAccess.GetServersWithRpcClientAccessEnabled(ExchangeRpcClientAccess.GetAllPossibleServers(session, localSite.Id), ExchangeRpcClientAccess.GetAll(session)))
			{
				if ((keyValuePair.Value.Responsibility & RpcClientAccessResponsibility.Mailboxes) != RpcClientAccessResponsibility.None)
				{
					if (localServerId.Equals(keyValuePair.Key.Id))
					{
						return keyValuePair.Key.ExchangeLegacyDN;
					}
					list.Add(keyValuePair.Key.ExchangeLegacyDN);
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			return NewRpcClientAccess.SelectServerWithEqualProbability(list);
		}

		// Token: 0x040032AE RID: 12974
		private readonly Task.TaskErrorLoggingDelegate writeError;

		// Token: 0x040032AF RID: 12975
		private readonly Task.TaskVerboseLoggingDelegate writeVerbose;
	}
}
