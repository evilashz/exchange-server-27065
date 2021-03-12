using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Caching;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.DirectoryCache;

namespace Microsoft.Exchange.Data.Directory.Cache
{
	// Token: 0x0200008B RID: 139
	[KnownType(typeof(BaseDirectoryCacheRequest))]
	[KnownType(typeof(List<Tuple<string, KeyType>>))]
	[DataContract]
	[DebuggerDisplay("{RequestId}-{ObjectType}")]
	internal class AddDirectoryCacheRequest : BaseDirectoryCacheRequest, IExtensibleDataObject
	{
		// Token: 0x06000700 RID: 1792 RVA: 0x00025A38 File Offset: 0x00023C38
		public AddDirectoryCacheRequest(List<Tuple<string, KeyType>> keys, ADRawEntry objectToCache, string forestFqdn, OrganizationId organizationId, IEnumerable<PropertyDefinition> properties, int secondsTimeout = 2147483646, CacheItemPriority priority = CacheItemPriority.Default)
		{
			ArgumentValidator.ThrowIfNull("keys", keys);
			ArgumentValidator.ThrowIfNull("objectToCache", objectToCache);
			ArgumentValidator.ThrowIfOutOfRange<int>("secondsTimeout", secondsTimeout, 1, 2147483646);
			ArgumentValidator.ThrowIfNull("organizationId", organizationId);
			if (keys.Count == 0)
			{
				throw new InvalidOperationException("Keys should not be empty");
			}
			this.Keys = keys;
			ADObject adobject = objectToCache as ADObject;
			if (adobject != null)
			{
				this.Object = SimpleADObject.CreateFrom(adobject, properties);
			}
			else
			{
				this.Object = SimpleADObject.CreateFromRawEntry(objectToCache, properties, true);
			}
			this.ObjectType = CacheUtils.GetObjectTypeFor(objectToCache.GetType(), true);
			this.SecondsTimeout = secondsTimeout;
			this.Priority = priority;
			base.ForestOrPartitionFqdn = forestFqdn;
			base.InternalSetOrganizationId(organizationId);
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x00025AF3 File Offset: 0x00023CF3
		// (set) Token: 0x06000702 RID: 1794 RVA: 0x00025AFB File Offset: 0x00023CFB
		[DataMember(IsRequired = true, EmitDefaultValue = false)]
		public List<Tuple<string, KeyType>> Keys { get; private set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x00025B04 File Offset: 0x00023D04
		// (set) Token: 0x06000704 RID: 1796 RVA: 0x00025B0C File Offset: 0x00023D0C
		[DataMember(IsRequired = true, EmitDefaultValue = false)]
		public int SecondsTimeout { get; private set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x00025B15 File Offset: 0x00023D15
		// (set) Token: 0x06000706 RID: 1798 RVA: 0x00025B1D File Offset: 0x00023D1D
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public CacheItemPriority Priority { get; private set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x00025B26 File Offset: 0x00023D26
		// (set) Token: 0x06000708 RID: 1800 RVA: 0x00025B2E File Offset: 0x00023D2E
		[DataMember(IsRequired = true, EmitDefaultValue = false)]
		public SimpleADObject Object { get; private set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x00025B37 File Offset: 0x00023D37
		// (set) Token: 0x0600070A RID: 1802 RVA: 0x00025B3F File Offset: 0x00023D3F
		[DataMember(IsRequired = true, EmitDefaultValue = false)]
		public ObjectType ObjectType { get; private set; }

		// Token: 0x0600070B RID: 1803 RVA: 0x00025B48 File Offset: 0x00023D48
		public override string ToString()
		{
			if (ExTraceGlobals.WCFServiceEndpointTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				return string.Format("{0}-{1}-{2}-{3}-[{4}]", new object[]
				{
					base.RequestId,
					this.ObjectType,
					this.SecondsTimeout,
					this.Priority,
					string.Join<Tuple<string, KeyType>>("|", this.Keys)
				});
			}
			return base.RequestId + this.ObjectType.ToString();
		}
	}
}
