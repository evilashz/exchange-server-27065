using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data.Sync
{
	// Token: 0x02000214 RID: 532
	public abstract class BaseCookie : ADObject
	{
		// Token: 0x0600160F RID: 5647 RVA: 0x00044C12 File Offset: 0x00042E12
		public BaseCookie()
		{
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x00044C1C File Offset: 0x00042E1C
		public BaseCookie(string serviceInstance, byte[] data) : this()
		{
			if (serviceInstance == null)
			{
				throw new ArgumentNullException("serviceInstance");
			}
			this.ServiceInstance = serviceInstance;
			this.Data = data;
			this.BatchId = CombGuidGenerator.NewGuid();
			this.Version = ExchangeObjectVersion.Current.ToString();
			this[BaseCookieSchema.IdentityProp] = new ADObjectId(CombGuidGenerator.NewGuid());
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001611 RID: 5649 RVA: 0x00044C7B File Offset: 0x00042E7B
		public override ObjectId Identity
		{
			get
			{
				return this[BaseCookieSchema.IdentityProp] as ObjectId;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001612 RID: 5650 RVA: 0x00044C8D File Offset: 0x00042E8D
		// (set) Token: 0x06001613 RID: 5651 RVA: 0x00044C9F File Offset: 0x00042E9F
		public string ServiceInstance
		{
			get
			{
				return this[BaseCookieSchema.ServiceInstanceProp] as string;
			}
			set
			{
				this[BaseCookieSchema.ServiceInstanceProp] = value;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001614 RID: 5652 RVA: 0x00044CAD File Offset: 0x00042EAD
		// (set) Token: 0x06001615 RID: 5653 RVA: 0x00044CBF File Offset: 0x00042EBF
		public byte[] Data
		{
			get
			{
				return this[BaseCookieSchema.DataProp] as byte[];
			}
			set
			{
				this[BaseCookieSchema.DataProp] = value;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001616 RID: 5654 RVA: 0x00044CCD File Offset: 0x00042ECD
		// (set) Token: 0x06001617 RID: 5655 RVA: 0x00044CDF File Offset: 0x00042EDF
		public string Version
		{
			get
			{
				return this[BaseCookieSchema.VersionProp] as string;
			}
			set
			{
				this[BaseCookieSchema.VersionProp] = value;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001618 RID: 5656 RVA: 0x00044CED File Offset: 0x00042EED
		// (set) Token: 0x06001619 RID: 5657 RVA: 0x00044CFF File Offset: 0x00042EFF
		public string ActiveMachine
		{
			get
			{
				return this[BaseCookieSchema.ActiveMachineProperty] as string;
			}
			set
			{
				this[BaseCookieSchema.ActiveMachineProperty] = value;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x0600161A RID: 5658 RVA: 0x00044D0D File Offset: 0x00042F0D
		// (set) Token: 0x0600161B RID: 5659 RVA: 0x00044D1F File Offset: 0x00042F1F
		public ProvisioningFlags ProvisioningFlags
		{
			get
			{
				return (ProvisioningFlags)this[BaseCookieSchema.ProvisioningFlagsProperty];
			}
			set
			{
				this[BaseCookieSchema.ProvisioningFlagsProperty] = value;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x0600161C RID: 5660 RVA: 0x00044D32 File Offset: 0x00042F32
		// (set) Token: 0x0600161D RID: 5661 RVA: 0x00044D44 File Offset: 0x00042F44
		public Guid BatchId
		{
			get
			{
				return (Guid)this[BaseCookieSchema.BatchIdProp];
			}
			set
			{
				this[BaseCookieSchema.BatchIdProp] = value;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x0600161E RID: 5662 RVA: 0x00044D57 File Offset: 0x00042F57
		// (set) Token: 0x0600161F RID: 5663 RVA: 0x00044D69 File Offset: 0x00042F69
		public DateTime LastChanged
		{
			get
			{
				return (DateTime)this[BaseCookieSchema.LastChangedProp];
			}
			set
			{
				this[BaseCookieSchema.LastChangedProp] = value;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x00044D7C File Offset: 0x00042F7C
		// (set) Token: 0x06001621 RID: 5665 RVA: 0x00044D8E File Offset: 0x00042F8E
		public bool Complete
		{
			get
			{
				return (bool)this[BaseCookieSchema.CompleteProp];
			}
			set
			{
				this[BaseCookieSchema.CompleteProp] = value;
			}
		}
	}
}
