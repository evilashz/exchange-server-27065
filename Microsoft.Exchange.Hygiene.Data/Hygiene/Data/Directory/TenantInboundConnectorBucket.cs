using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x02000101 RID: 257
	[Serializable]
	internal class TenantInboundConnectorBucket : ConfigurablePropertyBag, ISerializable
	{
		// Token: 0x06000A0D RID: 2573 RVA: 0x0001E5DB File Offset: 0x0001C7DB
		public TenantInboundConnectorBucket()
		{
			this.TenantInboundConnectors = new Dictionary<string, TenantInboundConnector>(StringComparer.InvariantCultureIgnoreCase);
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0001E5F3 File Offset: 0x0001C7F3
		public TenantInboundConnectorBucket(string key)
		{
			this.BucketKey = key;
			this.TenantInboundConnectors = new Dictionary<string, TenantInboundConnector>(StringComparer.InvariantCultureIgnoreCase);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0001E614 File Offset: 0x0001C814
		public TenantInboundConnectorBucket(SerializationInfo info, StreamingContext context)
		{
			this.BucketKey = (string)info.GetValue(TenantInboundConnectorBucket.BucketKeyProp.Name, typeof(string));
			this.TenantInboundConnectors = (Dictionary<string, TenantInboundConnector>)info.GetValue(TenantInboundConnectorBucket.TenantInboundConnectorsProp.Name, typeof(Dictionary<string, TenantInboundConnector>));
			this.WhenChangedUTC = (DateTime?)info.GetValue(TenantInboundConnectorBucket.WhenChangedProp.Name, typeof(DateTime?));
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x0001E696 File Offset: 0x0001C896
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.BucketKey);
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x0001E6A3 File Offset: 0x0001C8A3
		// (set) Token: 0x06000A12 RID: 2578 RVA: 0x0001E6B5 File Offset: 0x0001C8B5
		public string BucketKey
		{
			get
			{
				return this[TenantInboundConnectorBucket.BucketKeyProp] as string;
			}
			set
			{
				this[TenantInboundConnectorBucket.BucketKeyProp] = value;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x0001E6C3 File Offset: 0x0001C8C3
		// (set) Token: 0x06000A14 RID: 2580 RVA: 0x0001E6D5 File Offset: 0x0001C8D5
		public Dictionary<string, TenantInboundConnector> TenantInboundConnectors
		{
			get
			{
				return this[TenantInboundConnectorBucket.TenantInboundConnectorsProp] as Dictionary<string, TenantInboundConnector>;
			}
			set
			{
				this[TenantInboundConnectorBucket.TenantInboundConnectorsProp] = value;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x0001E6E3 File Offset: 0x0001C8E3
		// (set) Token: 0x06000A16 RID: 2582 RVA: 0x0001E6F5 File Offset: 0x0001C8F5
		public DateTime? WhenChangedUTC
		{
			get
			{
				return (DateTime?)this[TenantInboundConnectorBucket.WhenChangedProp];
			}
			set
			{
				this[TenantInboundConnectorBucket.WhenChangedProp] = value;
			}
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0001E708 File Offset: 0x0001C908
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(TenantInboundConnectorBucket.BucketKeyProp.Name, this.BucketKey);
			info.AddValue(TenantInboundConnectorBucket.TenantInboundConnectorsProp.Name, this.TenantInboundConnectors);
			info.AddValue(TenantInboundConnectorBucket.WhenChangedProp.Name, this.WhenChangedUTC);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0001E9A0 File Offset: 0x0001CBA0
		public static IEnumerable<string> BucketizeConnector(TenantInboundConnector connector)
		{
			if (connector == null)
			{
				throw new ArgumentNullException("connector");
			}
			if (connector.SenderIPAddresses != null)
			{
				foreach (string ipKey in TenantInboundConnectorBucket.FindAllVariationsOfKeysBasedOnSenderIPAddresses(connector.SenderIPAddresses))
				{
					yield return ipKey;
				}
			}
			if (connector.TlsSenderCertificateName != null)
			{
				SmtpDomainWithSubdomains domain = connector.TlsSenderCertificateName.TlsCertificateName as SmtpDomainWithSubdomains;
				if (domain != null)
				{
					yield return domain.Address;
				}
				else
				{
					yield return connector.TlsSenderCertificateName.ToString();
				}
			}
			yield break;
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0001E9BD File Offset: 0x0001CBBD
		internal static string GetPrimaryKeyForTenantInboundConnector(TenantInboundConnector connector)
		{
			return string.Format("{0}:{1}", connector.OrganizationalUnitRoot.ObjectGuid, connector.Name.ToLower());
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0001E9E4 File Offset: 0x0001CBE4
		internal void AddEntriesToBucket(IEnumerable<TenantInboundConnector> entriesToAdd)
		{
			foreach (TenantInboundConnector tenantInboundConnector in entriesToAdd)
			{
				string primaryKeyForTenantInboundConnector = TenantInboundConnectorBucket.GetPrimaryKeyForTenantInboundConnector(tenantInboundConnector);
				this.TenantInboundConnectors[primaryKeyForTenantInboundConnector] = tenantInboundConnector;
				this.UpdateWhenChanged(tenantInboundConnector);
			}
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0001EA40 File Offset: 0x0001CC40
		internal void RemoveEntriesFromBucket(IEnumerable<TenantInboundConnector> entriesToRemove)
		{
			foreach (TenantInboundConnector connector in entriesToRemove)
			{
				string primaryKeyForTenantInboundConnector = TenantInboundConnectorBucket.GetPrimaryKeyForTenantInboundConnector(connector);
				this.TenantInboundConnectors.Remove(primaryKeyForTenantInboundConnector);
				this.UpdateWhenChanged(connector);
			}
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0001EA9C File Offset: 0x0001CC9C
		private void UpdateWhenChanged(TenantInboundConnector connector)
		{
			if (this.WhenChangedUTC == null || (connector.WhenChangedUTC != null && this.WhenChangedUTC < connector.WhenChangedUTC))
			{
				this.WhenChangedUTC = connector.WhenChangedUTC;
			}
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0001EB14 File Offset: 0x0001CD14
		internal static IEnumerable<string> FindAllVariationsOfKeysBasedOnSenderIPAddresses(IEnumerable<IPRange> ipRanges)
		{
			return ipRanges.SelectMany((IPRange ip) => DalHelper.GetIPsFromIPRange(ip));
		}

		// Token: 0x04000539 RID: 1337
		private static readonly HygienePropertyDefinition BucketKeyProp = new HygienePropertyDefinition("BucketKey", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400053A RID: 1338
		private static readonly HygienePropertyDefinition TenantInboundConnectorsProp = new HygienePropertyDefinition("TenantInboundConnectors", typeof(IDictionary<string, TenantInboundConnector>));

		// Token: 0x0400053B RID: 1339
		private static readonly HygienePropertyDefinition WhenChangedProp = DalHelper.WhenChangedProp;
	}
}
