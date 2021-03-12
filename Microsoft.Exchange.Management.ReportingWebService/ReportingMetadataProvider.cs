using System;
using System.Collections.Generic;
using System.Data.Services.Providers;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x02000030 RID: 48
	internal class ReportingMetadataProvider : IDataServiceMetadataProvider
	{
		// Token: 0x060000FE RID: 254 RVA: 0x0000578F File Offset: 0x0000398F
		public ReportingMetadataProvider(ReportingSchema schema)
		{
			this.metadata = new Metadata(RbacPrincipal.Current, schema.Entities.Values, schema.ComplexTypeResourceTypes);
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000FF RID: 255 RVA: 0x000057B8 File Offset: 0x000039B8
		string IDataServiceMetadataProvider.ContainerName
		{
			get
			{
				return "TenantReportingWebService";
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000100 RID: 256 RVA: 0x000057BF File Offset: 0x000039BF
		string IDataServiceMetadataProvider.ContainerNamespace
		{
			get
			{
				return "TenantReporting";
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000057C6 File Offset: 0x000039C6
		IEnumerable<ResourceSet> IDataServiceMetadataProvider.ResourceSets
		{
			get
			{
				return this.metadata.ResourceSets.Values;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00005878 File Offset: 0x00003A78
		IEnumerable<ServiceOperation> IDataServiceMetadataProvider.ServiceOperations
		{
			get
			{
				yield break;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00005895 File Offset: 0x00003A95
		IEnumerable<ResourceType> IDataServiceMetadataProvider.Types
		{
			get
			{
				return this.metadata.ResourceTypes.Values;
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005948 File Offset: 0x00003B48
		IEnumerable<ResourceType> IDataServiceMetadataProvider.GetDerivedTypes(ResourceType resourceType)
		{
			yield break;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005965 File Offset: 0x00003B65
		ResourceAssociationSet IDataServiceMetadataProvider.GetResourceAssociationSet(ResourceSet resourceSet, ResourceType resourceType, ResourceProperty resourceProperty)
		{
			throw new NotImplementedException("No relationships.");
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005971 File Offset: 0x00003B71
		bool IDataServiceMetadataProvider.HasDerivedTypes(ResourceType resourceType)
		{
			return false;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005974 File Offset: 0x00003B74
		bool IDataServiceMetadataProvider.TryResolveResourceSet(string name, out ResourceSet resourceSet)
		{
			return this.metadata.ResourceSets.TryGetValue(name, out resourceSet);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005988 File Offset: 0x00003B88
		bool IDataServiceMetadataProvider.TryResolveResourceType(string name, out ResourceType resourceType)
		{
			return this.metadata.ResourceTypes.TryGetValue(name, out resourceType);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000599C File Offset: 0x00003B9C
		bool IDataServiceMetadataProvider.TryResolveServiceOperation(string name, out ServiceOperation serviceOperation)
		{
			serviceOperation = null;
			return false;
		}

		// Token: 0x0400007C RID: 124
		public const string Container = "TenantReportingWebService";

		// Token: 0x0400007D RID: 125
		private readonly Metadata metadata;
	}
}
