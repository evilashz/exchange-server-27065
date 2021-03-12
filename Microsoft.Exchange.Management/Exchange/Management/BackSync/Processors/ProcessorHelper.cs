using System;
using System.Collections;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.DirSync;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Diagnostics.Components.BackSync;

namespace Microsoft.Exchange.Management.BackSync.Processors
{
	// Token: 0x020000BA RID: 186
	internal static class ProcessorHelper
	{
		// Token: 0x060005EF RID: 1519 RVA: 0x00019B8C File Offset: 0x00017D8C
		internal static ADObjectId GetTenantOU(PropertyBag propertyBag)
		{
			bool flag = ProcessorHelper.IsDeletedObject(propertyBag);
			bool flag2 = ProcessorHelper.IsObjectOrganizationUnit(propertyBag);
			ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.Id];
			ADObjectId adobjectId2 = flag2 ? adobjectId : adobjectId.Parent;
			if (flag)
			{
				ADObjectId adobjectId3 = (ADObjectId)propertyBag[SyncObjectSchema.LastKnownParent];
				if (adobjectId3 == null)
				{
					adobjectId3 = adobjectId.DomainId.GetChildId("CN", "LostAndFound");
					ExTraceGlobals.BackSyncTracer.TraceWarning<string>((long)SyncConfiguration.TraceId, "ProcessorHelper::GetTenantOU for id {0}. Change for deleted object does not include lastKnowParent. This are not properly auth restored objects in the past prior to MSO tenants. Ignoring.", adobjectId.ToString());
				}
				adobjectId2 = (flag2 ? adobjectId : adobjectId3);
			}
			if (adobjectId2.Rdn.Equals(ProcessorHelper.SoftDeletedContainerName))
			{
				adobjectId2 = adobjectId2.Parent;
			}
			return adobjectId2;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00019C33 File Offset: 0x00017E33
		internal static bool IsDeletedObject(PropertyBag propertyBag)
		{
			return (bool)propertyBag[ADDirSyncResultSchema.IsDeleted] || (bool)propertyBag[SyncObjectSchema.Deleted];
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00019C5C File Offset: 0x00017E5C
		internal static bool IsUserObject(PropertyBag propertyBag)
		{
			DirectoryObjectClass directoryObjectClass = DirectoryObjectClass.Account;
			if (propertyBag.Contains(ADObjectSchema.ObjectClass))
			{
				directoryObjectClass = SyncRecipient.GetRecipientType(propertyBag);
			}
			return directoryObjectClass == DirectoryObjectClass.User;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00019C84 File Offset: 0x00017E84
		internal static bool IsObjectOrganizationUnit(PropertyBag propertyBag)
		{
			ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.Id];
			bool flag = !adobjectId.Rdn.Equals(ProcessorHelper.SoftDeletedContainerName) && adobjectId.Rdn.Prefix.Equals("OU", StringComparison.InvariantCultureIgnoreCase);
			ExTraceGlobals.BackSyncTracer.TraceDebug<bool, string>((long)SyncConfiguration.TraceId, "ProcessorHelper::IsObjectOrganizationUnit return {0} ({1})", flag, adobjectId.ToString());
			return flag;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00019CF4 File Offset: 0x00017EF4
		internal static void TracePropertBag(string hint, PropertyBag bag)
		{
			IDictionaryEnumerator dictionaryEnumerator = bag.GetEnumerator();
			while (dictionaryEnumerator.MoveNext())
			{
				if (dictionaryEnumerator.Value != null)
				{
					ADPropertyDefinition property = (ADPropertyDefinition)dictionaryEnumerator.Key;
					ProcessorHelper.TracePropertyValue(hint, property, dictionaryEnumerator.Value);
				}
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00019D38 File Offset: 0x00017F38
		private static void TracePropertyValue(string hint, ADPropertyDefinition property, object value)
		{
			string arg = string.Empty;
			if (property.IsMultivalued)
			{
				if (value != null)
				{
					IList list = (IList)value;
					StringBuilder stringBuilder = new StringBuilder();
					foreach (object obj in list)
					{
						if (stringBuilder.Length > 0)
						{
							stringBuilder.Append(";");
						}
						stringBuilder.AppendFormat("{0}", obj.ToString());
					}
					arg = stringBuilder.ToString();
				}
				else
				{
					arg = "NULL";
				}
			}
			else
			{
				arg = ((value != null) ? value.ToString() : "NULL");
			}
			ExTraceGlobals.BackSyncTracer.TraceDebug<string, string, string>((long)SyncConfiguration.TraceId, "<{0}> PropertyValue - {1}: {2}", hint, property.ToString(), arg);
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00019E0C File Offset: 0x0001800C
		internal static bool IsSoftDeletedObject(PropertyBag propertyBag)
		{
			ADObjectId id = (ADObjectId)propertyBag[ADObjectSchema.Id];
			return !ProcessorHelper.IsObjectOrganizationUnit(propertyBag) && ProcessorHelper.IsSoftDeletedObject(id);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00019E3C File Offset: 0x0001803C
		internal static bool IsSoftDeletedObject(ADObjectId id)
		{
			if (id != null)
			{
				ADObjectId parent = id.Parent;
				if (parent.Rdn.Equals(ProcessorHelper.SoftDeletedContainerName))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040002E5 RID: 741
		private const string OuRdnPrefix = "OU";

		// Token: 0x040002E6 RID: 742
		private static AdName SoftDeletedContainerName = new AdName("OU", "Soft Deleted Objects");
	}
}
