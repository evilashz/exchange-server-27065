using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.LoggingCommon;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A57 RID: 2647
	internal class FfoDualWriter
	{
		// Token: 0x17001C97 RID: 7319
		// (get) Token: 0x06005EDB RID: 24283 RVA: 0x0018CF80 File Offset: 0x0018B180
		private static IConfigDataProvider FfoDataProvider
		{
			get
			{
				if (FfoDualWriter.configDataProvider == null)
				{
					Assembly assembly = Assembly.Load("Microsoft.Exchange.Hygiene.WebserviceDataProvider");
					Type type = assembly.GetType("Microsoft.Exchange.Hygiene.WebserviceDataProvider.WebserviceDataProviderFactory");
					MethodInfo method = type.GetMethod("CreateDataProvider", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
					FfoDualWriter.configDataProvider = (IConfigDataProvider)method.Invoke(null, new object[]
					{
						"Directory"
					});
				}
				return FfoDualWriter.configDataProvider;
			}
		}

		// Token: 0x06005EDC RID: 24284 RVA: 0x0018CFDF File Offset: 0x0018B1DF
		public FfoDualWriter(string oldName = null)
		{
			this.oldName = oldName;
		}

		// Token: 0x06005EDD RID: 24285 RVA: 0x0018CFEE File Offset: 0x0018B1EE
		public void Save<T>(Task task, T adObject) where T : ADObject, new()
		{
			FfoDualWriter.SaveToFfo<T>(task, adObject, this.oldName);
		}

		// Token: 0x06005EDE RID: 24286 RVA: 0x0018CFFD File Offset: 0x0018B1FD
		public static void SaveToFfo<T>(Task task, T adObject, string oldName = null) where T : ADObject, new()
		{
			FfoDualWriter.SaveToFfo<T>(task, adObject, TenantSettingSyncLogGenerator.Instance.GetLogType(adObject), oldName);
		}

		// Token: 0x06005EDF RID: 24287 RVA: 0x0018D018 File Offset: 0x0018B218
		public static void SaveToFfo<T>(Task task, T adObject, TenantSettingSyncLogType logType, string oldName = null) where T : ADObject, new()
		{
			bool flag = false;
			try
			{
				if (!DatacenterRegistry.IsForefrontForOffice() && !task.CurrentOrganizationId.Equals(OrganizationId.ForestWideOrgId) && DatacenterRegistry.IsDualWriteAllowed() && adObject != null)
				{
					flag = true;
					if (adObject.m_Session != null)
					{
						ComparisonFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, adObject.Name);
						T t = adObject.m_Session.Find<T>(null, QueryScope.SubTree, filter, null, 0).FirstOrDefault<T>();
						if (t != null)
						{
							adObject = t;
						}
					}
					FfoDualWriter.FixTenantId(adObject);
					FfoDualWriter.HandleRenaming(adObject, oldName);
					FfoDualWriter.FfoDataProvider.Save(adObject);
				}
			}
			catch (Exception ex)
			{
				if (flag)
				{
					FfoDualWriter.LogToFile<T>(adObject, logType, oldName);
				}
				FfoDualWriter.LogException(ex);
			}
		}

		// Token: 0x06005EE0 RID: 24288 RVA: 0x0018D0F4 File Offset: 0x0018B2F4
		public static void DeleteFromFfo<T>(Task task, T adObject) where T : ADObject, new()
		{
			FfoDualWriter.DeleteFromFfo<T>(task, adObject, TenantSettingSyncLogGenerator.Instance.GetLogType(adObject));
		}

		// Token: 0x06005EE1 RID: 24289 RVA: 0x0018D110 File Offset: 0x0018B310
		public static void DeleteFromFfo<T>(Task task, T adObject, TenantSettingSyncLogType logType) where T : ADObject, new()
		{
			bool flag = false;
			try
			{
				if (!DatacenterRegistry.IsForefrontForOffice() && !task.CurrentOrganizationId.Equals(OrganizationId.ForestWideOrgId) && DatacenterRegistry.IsDualWriteAllowed())
				{
					flag = true;
					FfoDualWriter.FixTenantId(adObject);
					TenantSettingSyncLogGenerator.Instance.LogChangesForDelete(adObject, logType, new Guid?(adObject.OrganizationalUnitRoot.ObjectGuid));
					FfoDualWriter.FfoDataProvider.Delete(adObject);
				}
			}
			catch (Exception ex)
			{
				if (flag)
				{
					FfoDualWriter.LogToFile<T>(adObject, logType, null);
				}
				FfoDualWriter.LogException(ex);
			}
		}

		// Token: 0x06005EE2 RID: 24290 RVA: 0x0018D1AC File Offset: 0x0018B3AC
		private static void FixTenantId(IConfigurable instance)
		{
			IPropertyBag propertyBag = instance as IPropertyBag;
			OrganizationId organizationId = (OrganizationId)propertyBag[ADObjectSchema.OrganizationId];
			if (organizationId == null)
			{
				return;
			}
			IConfigDataProvider tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(null, true, ConsistencyMode.IgnoreInvalid, null, ADSessionSettings.FromAllTenantsOrRootOrgAutoDetect(organizationId), 192, "FixTenantId", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\MessageHygiene\\HygieneConfiguration\\FfoDualWriter.cs");
			Guid objectGuid;
			ADOperationResult externalDirectoryOrganizationId = SystemConfigurationTasksHelper.GetExternalDirectoryOrganizationId(tenantOrTopologyConfigurationSession, organizationId, out objectGuid);
			if (!externalDirectoryOrganizationId.Succeeded)
			{
				throw new InvalidOperationException("Error resolving orgId to external org id", externalDirectoryOrganizationId.Exception);
			}
			string distinguishedName = FfoDualWriter.FfoRootDN.GetChildId(organizationId.OrganizationalUnit.Name).GetChildId(objectGuid.ToString()).DistinguishedName;
			propertyBag[ADObjectSchema.OrganizationalUnitRoot] = new ADObjectId(distinguishedName, objectGuid);
		}

		// Token: 0x06005EE3 RID: 24291 RVA: 0x0018D266 File Offset: 0x0018B466
		private static bool HandleRenaming(ADObject adObject, string oldName)
		{
			if (!string.IsNullOrWhiteSpace(oldName) && oldName != adObject.Name)
			{
				adObject[FfoDualWriter.oldNameProp] = oldName;
				return true;
			}
			return false;
		}

		// Token: 0x06005EE4 RID: 24292 RVA: 0x0018D28D File Offset: 0x0018B48D
		private static void LogException(Exception ex)
		{
			TaskLogger.LogError(ex);
		}

		// Token: 0x06005EE5 RID: 24293 RVA: 0x0018D298 File Offset: 0x0018B498
		private static void LogToFile<T>(T adObject, TenantSettingSyncLogType logType, string oldName) where T : ADObject, new()
		{
			try
			{
				if (FfoDualWriter.HandleRenaming(adObject, oldName))
				{
					TenantSettingSyncLogGenerator.Instance.LogChangesForSave(adObject, logType, new Guid?(adObject.OrganizationalUnitRoot.ObjectGuid), null, new List<KeyValuePair<string, object>>
					{
						new KeyValuePair<string, object>(FfoDualWriter.oldNameProp.Name, adObject[FfoDualWriter.oldNameProp])
					});
				}
				else
				{
					TenantSettingSyncLogGenerator.Instance.LogChangesForSave(adObject, logType, new Guid?(adObject.OrganizationalUnitRoot.ObjectGuid), null, null);
				}
			}
			catch (Exception ex)
			{
				FfoDualWriter.LogException(ex);
			}
		}

		// Token: 0x040034F6 RID: 13558
		private static ADObjectId FfoRootDN = new ADObjectId("OU=Microsoft Exchange Hosted Organizations,DC=FFO,DC=extest,DC=microsoft,DC=com", Guid.Empty);

		// Token: 0x040034F7 RID: 13559
		private static IConfigDataProvider configDataProvider;

		// Token: 0x040034F8 RID: 13560
		private static readonly ADPropertyDefinition oldNameProp = new ADPropertyDefinition("OldName", ExchangeObjectVersion.Exchange2003, typeof(string), "oldName", ADPropertyDefinitionFlags.DoNotProvisionalClone, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040034F9 RID: 13561
		private readonly string oldName;
	}
}
