using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.Cache
{
	// Token: 0x020000AE RID: 174
	[KnownType(typeof(BaseDirectoryCacheRequest))]
	[DataContract]
	internal class RemoveDirectoryCacheRequest : BaseDirectoryCacheRequest, IExtensibleDataObject
	{
		// Token: 0x06000961 RID: 2401 RVA: 0x00029F58 File Offset: 0x00028158
		public RemoveDirectoryCacheRequest(string forestFqdn, OrganizationId organizationId, Tuple<string, KeyType> key, ObjectType objectType) : base((organizationId != null) ? organizationId.PartitionId.ForestFQDN : null)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("forestFqdn", forestFqdn);
			ArgumentValidator.ThrowIfNull("organizationId", organizationId);
			ArgumentValidator.ThrowIfNull("key", key);
			if (objectType == ObjectType.Unknown)
			{
				throw new InvalidOperationException("Invalid object type");
			}
			this.Key = key;
			this.ObjectType = objectType;
			base.ForestOrPartitionFqdn = forestFqdn;
			base.InternalSetOrganizationId(organizationId);
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x00029FCF File Offset: 0x000281CF
		// (set) Token: 0x06000963 RID: 2403 RVA: 0x00029FD7 File Offset: 0x000281D7
		[DataMember(IsRequired = true)]
		public ObjectType ObjectType { get; private set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x00029FE0 File Offset: 0x000281E0
		// (set) Token: 0x06000965 RID: 2405 RVA: 0x00029FE8 File Offset: 0x000281E8
		[DataMember(IsRequired = true)]
		public Tuple<string, KeyType> Key { get; private set; }
	}
}
