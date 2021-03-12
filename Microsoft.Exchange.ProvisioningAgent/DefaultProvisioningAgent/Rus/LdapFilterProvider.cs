using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.DefaultProvisioningAgent.Rus
{
	// Token: 0x02000048 RID: 72
	internal class LdapFilterProvider
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x0000C628 File Offset: 0x0000A828
		private LdapFilterProvider(string domainController, NetworkCredential credential, PartitionId PartitionId)
		{
			this.cfgSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(domainController, true, ConsistencyMode.PartiallyConsistent, credential, (PartitionId == null) ? ADSessionSettings.FromRootOrgScopeSet() : ADSessionSettings.FromAccountPartitionRootOrgScopeSet(PartitionId), 92, ".ctor", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ProvisioningAgent\\Rus\\LdapFilterProvider.cs");
			this.allFilters = new Dictionary<string, LdapFilter>();
			this.allFiltersLocker = new ReaderWriterLock();
			this.allAttributeSchemas = new Dictionary<string, SchemaAttributePresentationObject>();
			this.allAttributeSchemasLocker = new ReaderWriterLock();
			this.allAttributeSchemaDisplayNameWithType = new List<string>();
			this.objectCategoryMap = new Dictionary<string, string>();
			this.objectCategoryMapLocker = new ReaderWriterLock();
			if (string.IsNullOrEmpty(domainController))
			{
				this.schemaNamingContext = ADSession.GetSchemaNamingContext(PartitionId.ForestFQDN);
			}
			else
			{
				this.schemaNamingContext = ADSession.GetSchemaNamingContext(domainController, credential);
			}
			this.lastReloadTime = Environment.TickCount;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000C6F1 File Offset: 0x0000A8F1
		public static LdapFilterProvider GetLdapFilterProvider(string domainController, NetworkCredential credential)
		{
			if (string.IsNullOrEmpty(domainController))
			{
				throw new ArgumentNullException("domainController");
			}
			return new LdapFilterProvider(domainController, credential, null);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000C710 File Offset: 0x0000A910
		public static LdapFilterProvider GetLdapFilterProvider(PartitionId PartitionId)
		{
			LdapFilterProvider result;
			lock (LdapFilterProvider.lockRoot)
			{
				if (LdapFilterProvider.allInstance.ContainsKey(PartitionId) && Environment.TickCount - LdapFilterProvider.allInstance[PartitionId].lastReloadTime > LdapFilterProvider.reloadInterval)
				{
					LdapFilterProvider.allInstance.Remove(PartitionId);
				}
				if (!LdapFilterProvider.allInstance.ContainsKey(PartitionId))
				{
					LdapFilterProvider.allInstance.Add(PartitionId, new LdapFilterProvider(null, null, PartitionId));
				}
				result = LdapFilterProvider.allInstance[PartitionId];
			}
			return result;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000C7AC File Offset: 0x0000A9AC
		public LdapFilter PrepareLdapFilter(string stringFilter)
		{
			if (string.IsNullOrEmpty(stringFilter))
			{
				throw new ArgumentNullException("stringFilter");
			}
			this.allFiltersLocker.AcquireReaderLock(-1);
			try
			{
				if (this.allFilters.ContainsKey(stringFilter))
				{
					return this.allFilters[stringFilter];
				}
			}
			finally
			{
				this.allFiltersLocker.ReleaseReaderLock();
			}
			this.allFiltersLocker.AcquireWriterLock(-1);
			LdapFilter result;
			try
			{
				if (this.allFilters.ContainsKey(stringFilter))
				{
					result = this.allFilters[stringFilter];
				}
				else
				{
					this.allFilters[stringFilter] = LdapFilter.Parse(stringFilter, this);
					result = this.allFilters[stringFilter];
				}
			}
			finally
			{
				this.allFiltersLocker.ReleaseWriterLock();
			}
			return result;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000C878 File Offset: 0x0000AA78
		public string[] GetAllAttributes()
		{
			this.allAttributeSchemasLocker.AcquireReaderLock(-1);
			string[] result;
			try
			{
				result = this.allAttributeSchemaDisplayNameWithType.ToArray();
			}
			finally
			{
				this.allAttributeSchemasLocker.ReleaseReaderLock();
			}
			return result;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000C8BC File Offset: 0x0000AABC
		public SchemaAttributePresentationObject GetSchemaAttributeObject(string schemaName)
		{
			if (string.IsNullOrEmpty("schemaName"))
			{
				throw new ArgumentNullException("schemaName");
			}
			schemaName = schemaName.ToLower();
			this.allAttributeSchemasLocker.AcquireReaderLock(-1);
			try
			{
				if (this.allAttributeSchemas.ContainsKey(schemaName))
				{
					return this.allAttributeSchemas[schemaName];
				}
			}
			finally
			{
				this.allAttributeSchemasLocker.ReleaseReaderLock();
			}
			this.allAttributeSchemasLocker.AcquireWriterLock(-1);
			SchemaAttributePresentationObject result;
			try
			{
				if (this.allAttributeSchemas.ContainsKey(schemaName))
				{
					result = this.allAttributeSchemas[schemaName];
				}
				else
				{
					ADSchemaAttributeObject adschemaAttributeObject = new ADSchemaAttributeObject();
					ADRawEntry[] array = this.cfgSession.Find(this.schemaNamingContext, QueryScope.OneLevel, new AndFilter(new QueryFilter[]
					{
						adschemaAttributeObject.ImplicitFilter,
						new ComparisonFilter(ComparisonOperator.Equal, ADSchemaAttributeSchema.LdapDisplayName, schemaName)
					}), null, 1, new ADPropertyDefinition[]
					{
						ADSchemaAttributeSchema.DataSyntax,
						ADSchemaAttributeSchema.IsSingleValued
					});
					if (array != null && array.Length != 0)
					{
						ADRawEntry adrawEntry = array[0];
						SchemaAttributePresentationObject schemaAttributePresentationObject = new SchemaAttributePresentationObject();
						schemaAttributePresentationObject.DisplayName = schemaName;
						schemaAttributePresentationObject.DataSyntax = (DataSyntax)adrawEntry[ADSchemaAttributeSchema.DataSyntax];
						string text = schemaAttributePresentationObject.DisplayName;
						bool flag = false;
						bool flag2 = false;
						DataSyntax dataSyntax = schemaAttributePresentationObject.DataSyntax;
						if (dataSyntax == DataSyntax.Octet || dataSyntax == DataSyntax.Sid || dataSyntax == DataSyntax.NTSecDesc || dataSyntax == DataSyntax.ReplicaLink || dataSyntax == DataSyntax.AccessPoint || dataSyntax == DataSyntax.PresentationAddress)
						{
							flag = true;
						}
						if (!(bool)adrawEntry[ADSchemaAttributeSchema.IsSingleValued])
						{
							flag2 = true;
						}
						if (flag && flag2)
						{
							text += ":mv-binary";
						}
						else if (flag)
						{
							text += ":binary";
						}
						else if (flag2)
						{
							text += ":mv";
						}
						this.allAttributeSchemas[schemaName] = schemaAttributePresentationObject;
						this.allAttributeSchemaDisplayNameWithType.Add(text);
						result = schemaAttributePresentationObject;
					}
					else
					{
						result = null;
					}
				}
			}
			finally
			{
				this.allAttributeSchemasLocker.ReleaseWriterLock();
			}
			return result;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000CADC File Offset: 0x0000ACDC
		public string GetObjectCategory(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (name.EndsWith(this.schemaNamingContext.DistinguishedName, StringComparison.InvariantCultureIgnoreCase))
			{
				return name;
			}
			string text = SinglePropertyLdapFilter.Unescape(new StringBuilder(name)).ToLower();
			this.objectCategoryMapLocker.AcquireReaderLock(-1);
			try
			{
				if (this.objectCategoryMap.ContainsKey(text))
				{
					return this.objectCategoryMap[text];
				}
			}
			finally
			{
				this.objectCategoryMapLocker.ReleaseReaderLock();
			}
			this.objectCategoryMapLocker.AcquireWriterLock(-1);
			string result;
			try
			{
				if (this.objectCategoryMap.ContainsKey(text))
				{
					result = this.objectCategoryMap[text];
				}
				else
				{
					ADSchemaClassObject adschemaClassObject = new ADSchemaClassObject();
					ADRawEntry[] array = this.cfgSession.Find(this.schemaNamingContext, QueryScope.OneLevel, new AndFilter(new QueryFilter[]
					{
						adschemaClassObject.ImplicitFilter,
						new ComparisonFilter(ComparisonOperator.Equal, ADSchemaObjectSchema.DisplayName, text)
					}), null, 1, new ADPropertyDefinition[]
					{
						ADSchemaObjectSchema.DefaultObjectCategory
					});
					if (array != null && array.Length != 0)
					{
						this.objectCategoryMap[text] = ((ADObjectId)array[0][ADSchemaObjectSchema.DefaultObjectCategory]).DistinguishedName;
						result = this.objectCategoryMap[text];
					}
					else
					{
						result = null;
					}
				}
			}
			finally
			{
				this.objectCategoryMapLocker.ReleaseWriterLock();
			}
			return result;
		}

		// Token: 0x040000E6 RID: 230
		private static object lockRoot = new object();

		// Token: 0x040000E7 RID: 231
		private static int reloadInterval = 3600000;

		// Token: 0x040000E8 RID: 232
		private static Dictionary<PartitionId, LdapFilterProvider> allInstance = new Dictionary<PartitionId, LdapFilterProvider>();

		// Token: 0x040000E9 RID: 233
		private IConfigurationSession cfgSession;

		// Token: 0x040000EA RID: 234
		private Dictionary<string, LdapFilter> allFilters;

		// Token: 0x040000EB RID: 235
		private ReaderWriterLock allFiltersLocker;

		// Token: 0x040000EC RID: 236
		private Dictionary<string, SchemaAttributePresentationObject> allAttributeSchemas;

		// Token: 0x040000ED RID: 237
		private ReaderWriterLock allAttributeSchemasLocker;

		// Token: 0x040000EE RID: 238
		private List<string> allAttributeSchemaDisplayNameWithType;

		// Token: 0x040000EF RID: 239
		private Dictionary<string, string> objectCategoryMap;

		// Token: 0x040000F0 RID: 240
		private ReaderWriterLock objectCategoryMapLocker;

		// Token: 0x040000F1 RID: 241
		private ADObjectId schemaNamingContext;

		// Token: 0x040000F2 RID: 242
		private int lastReloadTime;
	}
}
