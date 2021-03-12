using System;
using System.Collections.Generic;
using System.Data.Services.Providers;
using System.Linq;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x02000032 RID: 50
	internal class ReportingQueryProvider : IDataServiceQueryProvider
	{
		// Token: 0x06000113 RID: 275 RVA: 0x00005E48 File Offset: 0x00004048
		public ReportingQueryProvider(IDataServiceMetadataProvider metadataProvider, ReportingSchema schema)
		{
			this.metadataProvider = metadataProvider;
			this.schema = schema;
			this.dataSource = DependencyFactory.CreateReportingDataSource(RbacPrincipal.Current);
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00005E6E File Offset: 0x0000406E
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00005E76 File Offset: 0x00004076
		object IDataServiceQueryProvider.CurrentDataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				this.dataSource = (IReportingDataSource)value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00005E84 File Offset: 0x00004084
		bool IDataServiceQueryProvider.IsNullPropagationRequired
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005E87 File Offset: 0x00004087
		object IDataServiceQueryProvider.GetOpenPropertyValue(object target, string propertyName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005E8E File Offset: 0x0000408E
		IEnumerable<KeyValuePair<string, object>> IDataServiceQueryProvider.GetOpenPropertyValues(object target)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005E95 File Offset: 0x00004095
		object IDataServiceQueryProvider.GetPropertyValue(object target, ResourceProperty resourceProperty)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005E9C File Offset: 0x0000409C
		IQueryable IDataServiceQueryProvider.GetQueryRootForResourceSet(ResourceSet resourceSet)
		{
			IEntity entity = this.schema.Entities[resourceSet.Name];
			return (IQueryable)Activator.CreateInstance(typeof(ReportingDataQuery<>).MakeGenericType(new Type[]
			{
				entity.ClrType
			}), new object[]
			{
				this.dataSource,
				entity
			});
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005F1C File Offset: 0x0000411C
		ResourceType IDataServiceQueryProvider.GetResourceType(object target)
		{
			Type type = target.GetType();
			ResourceType resourceType = ResourceType.GetPrimitiveResourceType(type);
			if (resourceType == null)
			{
				resourceType = this.metadataProvider.Types.Single((ResourceType t) => t.InstanceType == type);
			}
			return resourceType;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005F6F File Offset: 0x0000416F
		object IDataServiceQueryProvider.InvokeServiceOperation(ServiceOperation serviceOperation, object[] parameters)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000083 RID: 131
		private readonly IDataServiceMetadataProvider metadataProvider;

		// Token: 0x04000084 RID: 132
		private readonly ReportingSchema schema;

		// Token: 0x04000085 RID: 133
		private IReportingDataSource dataSource;
	}
}
