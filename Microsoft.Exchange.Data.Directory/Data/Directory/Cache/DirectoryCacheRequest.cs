using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.DirectoryCache;

namespace Microsoft.Exchange.Data.Directory.Cache
{
	// Token: 0x020000A4 RID: 164
	[KnownType(typeof(BaseDirectoryCacheRequest))]
	[DebuggerDisplay("{RequestId}-{ObjectType}")]
	[DataContract]
	internal class DirectoryCacheRequest : BaseDirectoryCacheRequest, IExtensibleDataObject
	{
		// Token: 0x0600090E RID: 2318 RVA: 0x00027F10 File Offset: 0x00026110
		public DirectoryCacheRequest(string forestOrPartitionFqdn, List<Tuple<string, KeyType>> keys, ObjectType objectType, IEnumerable<PropertyDefinition> properties = null) : base(forestOrPartitionFqdn)
		{
			ArgumentValidator.ThrowIfNull("keys", keys);
			if (keys.Count == 0)
			{
				throw new InvalidOperationException("Keys should not be empty");
			}
			this.Keys = keys;
			this.ObjectType = objectType;
			if (this.ObjectType == ObjectType.ADRawEntry && properties == null)
			{
				ArgumentValidator.ThrowIfNull("properties", properties);
			}
			if (properties != null)
			{
				this.Properties = this.Convert(properties).ToArray<string>();
				this.ADPropertiesRequested = properties;
			}
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00027F8C File Offset: 0x0002618C
		public DirectoryCacheRequest(string forestOrPartitionFqdn, Tuple<string, KeyType> key, ObjectType objectType, IEnumerable<PropertyDefinition> properties = null) : this(forestOrPartitionFqdn, new List<Tuple<string, KeyType>>(1)
		{
			key
		}, objectType, properties)
		{
			ArgumentValidator.ThrowIfNull("key", key);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00027FBD File Offset: 0x000261BD
		public DirectoryCacheRequest(string forestOrPartitionFqdn, string key, KeyType keyType, ObjectType objectType, IEnumerable<PropertyDefinition> properties = null) : this(forestOrPartitionFqdn, new Tuple<string, KeyType>(key, keyType), objectType, properties)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("key", key);
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x00027FDC File Offset: 0x000261DC
		// (set) Token: 0x06000912 RID: 2322 RVA: 0x00027FE4 File Offset: 0x000261E4
		[DataMember(IsRequired = true)]
		public ObjectType ObjectType { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x00027FED File Offset: 0x000261ED
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x00027FF5 File Offset: 0x000261F5
		[DataMember(IsRequired = true)]
		public List<Tuple<string, KeyType>> Keys { get; set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x00027FFE File Offset: 0x000261FE
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x00028006 File Offset: 0x00026206
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string[] Properties { get; private set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0002800F File Offset: 0x0002620F
		// (set) Token: 0x06000918 RID: 2328 RVA: 0x00028017 File Offset: 0x00026217
		public IEnumerable<PropertyDefinition> ADPropertiesRequested { get; private set; }

		// Token: 0x06000919 RID: 2329 RVA: 0x00028020 File Offset: 0x00026220
		[Conditional("DEBUG")]
		internal void Dbg_Validate()
		{
			ObjectType objectType = this.ObjectType;
			if (objectType <= ObjectType.OWAMiniRecipient)
			{
				if (objectType <= ObjectType.FederatedOrganizationId)
				{
					switch (objectType)
					{
					case ObjectType.ExchangeConfigurationUnit:
					case ObjectType.Recipient:
					case ObjectType.AcceptedDomain:
						goto IL_7C;
					case ObjectType.ExchangeConfigurationUnit | ObjectType.Recipient:
						break;
					default:
						if (objectType == ObjectType.FederatedOrganizationId)
						{
							goto IL_7C;
						}
						break;
					}
				}
				else if (objectType == ObjectType.MiniRecipient || objectType == ObjectType.TransportMiniRecipient || objectType == ObjectType.OWAMiniRecipient)
				{
					goto IL_7C;
				}
			}
			else if (objectType <= ObjectType.ADRawEntry)
			{
				if (objectType == ObjectType.ActiveSyncMiniRecipient || objectType == ObjectType.ADRawEntry)
				{
					goto IL_7C;
				}
			}
			else if (objectType == ObjectType.StorageMiniRecipient || objectType == ObjectType.MiniRecipientWithTokenGroups || objectType == ObjectType.FrontEndMiniRecipient)
			{
				goto IL_7C;
			}
			throw new NotSupportedException("ObjectType should be single value");
			IL_7C:
			if (string.IsNullOrEmpty(base.ForestOrPartitionFqdn))
			{
				throw new InvalidOperationException("ForestOrPartitionFqdn should not be null");
			}
			if (this.Keys == null || this.Keys.Count == 0)
			{
				throw new InvalidOperationException("Keys should not be null or empty");
			}
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x000280E1 File Offset: 0x000262E1
		internal void SetOrganizationId(OrganizationId organizationId)
		{
			base.InternalSetOrganizationId(organizationId);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x000280EC File Offset: 0x000262EC
		private string[] Convert(IEnumerable<PropertyDefinition> properties)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			foreach (PropertyDefinition propertyDefinition in properties)
			{
				ADPropertyDefinition adpropertyDefinition = (ADPropertyDefinition)propertyDefinition;
				if (adpropertyDefinition.LdapDisplayName != null && !adpropertyDefinition.IsCalculated && !hashSet.Contains(adpropertyDefinition.LdapDisplayName))
				{
					hashSet.Add(adpropertyDefinition.LdapDisplayName);
				}
			}
			return hashSet.ToArray<string>();
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00028170 File Offset: 0x00026370
		public override string ToString()
		{
			if (ExTraceGlobals.CacheSessionTracer.IsTraceEnabled(TraceType.DebugTrace) || ExTraceGlobals.WCFServiceEndpointTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				return string.Format("{0}-{1}-{2}-[{3}]", new object[]
				{
					base.RequestId,
					base.ForestOrPartitionFqdn,
					this.ObjectType,
					string.Join<Tuple<string, KeyType>>("|", this.Keys)
				});
			}
			return base.ForestOrPartitionFqdn + base.RequestId + this.ObjectType.ToString();
		}
	}
}
