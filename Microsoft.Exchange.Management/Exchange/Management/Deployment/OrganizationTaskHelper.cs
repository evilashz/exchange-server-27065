using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management.Automation;
using System.Reflection;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000216 RID: 534
	internal static class OrganizationTaskHelper
	{
		// Token: 0x0600124C RID: 4684 RVA: 0x00050979 File Offset: 0x0004EB79
		internal static void SetOrganizationStatusTimeout(int seconds)
		{
			OrganizationTaskHelper.organizationStatusTimeout = new TimeSpan(0, 0, seconds);
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x00050988 File Offset: 0x0004EB88
		internal static ADOrganizationalUnit GetOUFromOrganizationId(OrganizationIdParameter organization, IConfigurationSession session, Task.TaskErrorLoggingDelegate errorLogger, bool reportError)
		{
			bool useConfigNC = session.UseConfigNC;
			ADOrganizationalUnit result = null;
			try
			{
				session.UseConfigNC = false;
				IEnumerable<ADOrganizationalUnit> objects = organization.GetObjects<ADOrganizationalUnit>(null, session);
				using (IEnumerator<ADOrganizationalUnit> enumerator = objects.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						result = enumerator.Current;
						if (reportError && enumerator.MoveNext())
						{
							errorLogger(new ManagementObjectAmbiguousException(Strings.ErrorOrganizationNotUnique(organization.ToString())), ErrorCategory.InvalidArgument, null);
						}
					}
					else if (reportError)
					{
						errorLogger(new ManagementObjectNotFoundException(Strings.ErrorOrganizationNotFound(organization.ToString())), ErrorCategory.InvalidArgument, null);
					}
				}
			}
			finally
			{
				session.UseConfigNC = useConfigNC;
			}
			return result;
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x00050A38 File Offset: 0x0004EC38
		internal static ExchangeConfigurationUnit GetExchangeConfigUnitFromOrganizationId(OrganizationIdParameter organization, IConfigurationSession session, Task.TaskErrorLoggingDelegate errorLogger, bool reportError)
		{
			ExchangeConfigurationUnit result = null;
			IEnumerable<ExchangeConfigurationUnit> objects = organization.GetObjects<ExchangeConfigurationUnit>(null, session);
			using (IEnumerator<ExchangeConfigurationUnit> enumerator = objects.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					result = enumerator.Current;
					if (reportError && enumerator.MoveNext())
					{
						errorLogger(new ManagementObjectAmbiguousException(Strings.ErrorOrganizationNotUnique(organization.ToString())), ErrorCategory.InvalidArgument, null);
					}
				}
				else if (reportError)
				{
					errorLogger(new ManagementObjectNotFoundException(Strings.ErrorOrganizationNotFound(organization.ToString())), ErrorCategory.InvalidArgument, null);
				}
			}
			return result;
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x00050AC4 File Offset: 0x0004ECC4
		internal static void SetOrganizationStatus(IConfigurationSession session, ExchangeConfigurationUnit tenantCU, OrganizationStatus newStatus)
		{
			tenantCU.OrganizationStatus = newStatus;
			if (!tenantCU.IsTenantAccessBlocked && ExchangeConfigurationUnit.IsBeingDeleted(newStatus))
			{
				tenantCU.IsTenantAccessBlocked = true;
			}
			session.Save(tenantCU);
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x00050AEC File Offset: 0x0004ECEC
		internal static OrganizationStatus SetOrganizationStatus(OrganizationIdParameter orgIdParam, IConfigurationSession session, OrganizationStatus statusToSet, Task.TaskErrorLoggingDelegate errorLogger)
		{
			ExchangeConfigurationUnit exchangeConfigUnitFromOrganizationId = OrganizationTaskHelper.GetExchangeConfigUnitFromOrganizationId(orgIdParam, session, errorLogger, true);
			OrganizationStatus organizationStatus = exchangeConfigUnitFromOrganizationId.OrganizationStatus;
			if (statusToSet != organizationStatus)
			{
				bool useConfigNC = session.UseConfigNC;
				try
				{
					session.UseConfigNC = true;
					exchangeConfigUnitFromOrganizationId.OrganizationStatus = statusToSet;
					session.Save(exchangeConfigUnitFromOrganizationId);
				}
				finally
				{
					session.UseConfigNC = useConfigNC;
				}
			}
			return organizationStatus;
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x00050B48 File Offset: 0x0004ED48
		internal static OrganizationStatus GetOrganizationStatus(OrganizationIdParameter orgIdParam, IConfigurationSession session, Task.TaskErrorLoggingDelegate errorLogger)
		{
			ExchangeConfigurationUnit exchangeConfigUnitFromOrganizationId = OrganizationTaskHelper.GetExchangeConfigUnitFromOrganizationId(orgIdParam, session, errorLogger, true);
			return exchangeConfigUnitFromOrganizationId.OrganizationStatus;
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x00050B68 File Offset: 0x0004ED68
		internal static bool CanProceedWithOrganizationTask(OrganizationIdParameter orgIdParam, IConfigurationSession session, OrganizationStatus[] ignorableFlagsOnStatusTimeout, Task.TaskErrorLoggingDelegate errorLogger)
		{
			bool result = false;
			ExchangeConfigurationUnit exchangeConfigUnitFromOrganizationId = OrganizationTaskHelper.GetExchangeConfigUnitFromOrganizationId(orgIdParam, session, errorLogger, true);
			if (ExchangeConfigurationUnit.IsOrganizationActive(exchangeConfigUnitFromOrganizationId.OrganizationStatus))
			{
				result = true;
			}
			else
			{
				DateTime? whenOrganizationStatusSet = exchangeConfigUnitFromOrganizationId.WhenOrganizationStatusSet;
				if (whenOrganizationStatusSet != null)
				{
					DateTime value = whenOrganizationStatusSet.Value.ToUniversalTime();
					if (DateTime.UtcNow.Subtract(value) > OrganizationTaskHelper.organizationStatusTimeout && ignorableFlagsOnStatusTimeout != null)
					{
						foreach (OrganizationStatus organizationStatus in ignorableFlagsOnStatusTimeout)
						{
							if (organizationStatus == exchangeConfigUnitFromOrganizationId.OrganizationStatus)
							{
								result = true;
								break;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00050C00 File Offset: 0x0004EE00
		internal static AcceptedDomain GetAcceptedDomain(AcceptedDomainIdParameter acceptedDomainId, IConfigurationSession adSession, Task.TaskErrorLoggingDelegate errorLogger, bool reportError)
		{
			AcceptedDomain result = null;
			IEnumerable<AcceptedDomain> objects = acceptedDomainId.GetObjects<AcceptedDomain>(null, adSession);
			using (IEnumerator<AcceptedDomain> enumerator = objects.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					result = enumerator.Current;
					if (reportError && enumerator.MoveNext())
					{
						errorLogger(new ManagementObjectAmbiguousException(Strings.ErrorSecondaryDomainNotUnique(acceptedDomainId.ToString())), ErrorCategory.InvalidArgument, null);
					}
				}
				else if (reportError)
				{
					errorLogger(new ManagementObjectNotFoundException(Strings.ErrorSecondaryDomainNotFound(acceptedDomainId.ToString())), ErrorCategory.InvalidArgument, null);
				}
			}
			return result;
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x00050C8C File Offset: 0x0004EE8C
		internal static void ValidateParamString(string paramName, string value, Task.TaskErrorLoggingDelegate errorLogger)
		{
			OrganizationTaskHelper.ValidateParamString(paramName, value, errorLogger, false);
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x00050C98 File Offset: 0x0004EE98
		internal static void ValidateParamString(string paramName, string value, Task.TaskErrorLoggingDelegate errorLogger, bool blockWildcards)
		{
			if (string.IsNullOrEmpty(paramName))
			{
				throw new ArgumentNullException("paramName");
			}
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentNullException("value");
			}
			if (value.Contains("\"") || value.Contains("$") || (blockWildcards && value.Contains("*")))
			{
				errorLogger(new ArgumentException(Strings.ErrorInvalidCharactersInParameterValue(paramName, value, blockWildcards ? "{'\"', '$', '*'}" : "{'\"', '$'}")), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x00050D20 File Offset: 0x0004EF20
		internal static OrganizationId ResolveOrganization(Task task, OrganizationIdParameter organization, ADObjectId rootOrgContainerId, LocalizedString cannotResolveOrganizationMessage)
		{
			if (organization != null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(rootOrgContainerId, task.CurrentOrganizationId, task.ExecutingUserOrganizationId, true);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.RescopeToSubtree(sessionSettings), 371, "ResolveOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\OrganizationTaskHelper.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = false;
				ADOrganizationalUnit oufromOrganizationId = OrganizationTaskHelper.GetOUFromOrganizationId(organization, tenantOrTopologyConfigurationSession, new Task.TaskErrorLoggingDelegate(task.WriteError), true);
				if (oufromOrganizationId == null)
				{
					task.WriteError(new ArgumentException(cannotResolveOrganizationMessage), ErrorCategory.InvalidOperation, null);
					return null;
				}
				return oufromOrganizationId.OrganizationId;
			}
			else
			{
				if (task.CurrentOrganizationId == OrganizationId.ForestWideOrgId)
				{
					task.WriteError(new ArgumentException(cannotResolveOrganizationMessage), ErrorCategory.InvalidOperation, null);
					return null;
				}
				return task.CurrentOrganizationId;
			}
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x00050DD0 File Offset: 0x0004EFD0
		internal static ExchangeConfigurationUnit[] FindSharedConfigurations(SharedConfigurationInfo sci, PartitionId partitionId)
		{
			ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromAllTenantsPartitionId(partitionId), 406, "FindSharedConfigurations", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Deployment\\OrganizationTaskHelper.cs");
			return tenantConfigurationSession.FindSharedConfiguration(sci, true);
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x00050E08 File Offset: 0x0004F008
		internal static bool IsSharedConfigLinkedToOtherTenants(OrganizationId organizationId, IConfigurationSession session)
		{
			ExchangeConfigurationUnit[] array = session.Find<ExchangeConfigurationUnit>(organizationId.ConfigurationUnit, QueryScope.Base, new ExistsFilter(OrganizationSchema.SupportedSharedConfigurationsBL), null, 1);
			return array != null && array.Length > 0;
		}

		// Token: 0x040007D4 RID: 2004
		internal static TimeSpan organizationStatusTimeout = new TimeSpan(0, 10, 0);

		// Token: 0x040007D5 RID: 2005
		internal static FileVersionInfo ManagementDllVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);

		// Token: 0x040007D6 RID: 2006
		internal static ServerVersion CurrentBuildVersion = new ServerVersion(OrganizationTaskHelper.ManagementDllVersion.FileMajorPart, OrganizationTaskHelper.ManagementDllVersion.FileMinorPart, OrganizationTaskHelper.ManagementDllVersion.FileBuildPart, OrganizationTaskHelper.ManagementDllVersion.FilePrivatePart);
	}
}
