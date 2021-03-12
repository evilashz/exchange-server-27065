using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000BA RID: 186
	[DataContract]
	public class SetSchedulingPermissionsConfiguration : SetResourceConfigurationBase
	{
		// Token: 0x17001909 RID: 6409
		// (get) Token: 0x06001C9D RID: 7325 RVA: 0x00058D86 File Offset: 0x00056F86
		// (set) Token: 0x06001C9E RID: 7326 RVA: 0x00058DA2 File Offset: 0x00056FA2
		[DataMember]
		public bool AllBookInPolicy
		{
			get
			{
				return (bool)(base["AllBookInPolicy"] ?? false);
			}
			set
			{
				base["AllBookInPolicy"] = value;
			}
		}

		// Token: 0x1700190A RID: 6410
		// (get) Token: 0x06001C9F RID: 7327 RVA: 0x00058DB5 File Offset: 0x00056FB5
		// (set) Token: 0x06001CA0 RID: 7328 RVA: 0x00058DD1 File Offset: 0x00056FD1
		[DataMember]
		public bool AllRequestInPolicy
		{
			get
			{
				return (bool)(base["AllRequestInPolicy"] ?? false);
			}
			set
			{
				base["AllRequestInPolicy"] = value;
			}
		}

		// Token: 0x1700190B RID: 6411
		// (get) Token: 0x06001CA1 RID: 7329 RVA: 0x00058DE4 File Offset: 0x00056FE4
		// (set) Token: 0x06001CA2 RID: 7330 RVA: 0x00058E00 File Offset: 0x00057000
		[DataMember]
		public bool AllRequestOutOfPolicy
		{
			get
			{
				return (bool)(base["AllRequestOutOfPolicy"] ?? false);
			}
			set
			{
				base["AllRequestOutOfPolicy"] = value;
			}
		}

		// Token: 0x1700190C RID: 6412
		// (get) Token: 0x06001CA3 RID: 7331 RVA: 0x00058E13 File Offset: 0x00057013
		// (set) Token: 0x06001CA4 RID: 7332 RVA: 0x00058E1B File Offset: 0x0005701B
		[DataMember]
		public PeopleIdentity[] BookInPolicy
		{
			get
			{
				return this.bookInPolicy;
			}
			set
			{
				this.bookInPolicy = value;
				base["BookInPolicy"] = value.ToSMTPAddressArray();
			}
		}

		// Token: 0x1700190D RID: 6413
		// (get) Token: 0x06001CA5 RID: 7333 RVA: 0x00058E35 File Offset: 0x00057035
		// (set) Token: 0x06001CA6 RID: 7334 RVA: 0x00058E3D File Offset: 0x0005703D
		[DataMember]
		public PeopleIdentity[] RequestInPolicy
		{
			get
			{
				return this.requestInPolicy;
			}
			set
			{
				this.requestInPolicy = value;
				base["RequestInPolicy"] = value.ToSMTPAddressArray();
			}
		}

		// Token: 0x1700190E RID: 6414
		// (get) Token: 0x06001CA7 RID: 7335 RVA: 0x00058E57 File Offset: 0x00057057
		// (set) Token: 0x06001CA8 RID: 7336 RVA: 0x00058E5F File Offset: 0x0005705F
		[DataMember]
		public PeopleIdentity[] RequestOutOfPolicy
		{
			get
			{
				return this.requestOutOfPolicy;
			}
			set
			{
				this.requestOutOfPolicy = value;
				base["RequestOutOfPolicy"] = value.ToSMTPAddressArray();
			}
		}

		// Token: 0x04001BA6 RID: 7078
		private PeopleIdentity[] bookInPolicy;

		// Token: 0x04001BA7 RID: 7079
		private PeopleIdentity[] requestInPolicy;

		// Token: 0x04001BA8 RID: 7080
		private PeopleIdentity[] requestOutOfPolicy;
	}
}
