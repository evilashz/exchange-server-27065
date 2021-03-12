using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Reporting
{
	// Token: 0x020001C1 RID: 449
	internal class DeviceData : ConfigurablePropertyBag
	{
		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x000396DD File Offset: 0x000378DD
		// (set) Token: 0x060012D6 RID: 4822 RVA: 0x000396EF File Offset: 0x000378EF
		public string AccessSetBy
		{
			get
			{
				return (string)this[DeviceCommonSchema.AccessSetByProperty];
			}
			set
			{
				this[DeviceCommonSchema.AccessSetByProperty] = value;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x000396FD File Offset: 0x000378FD
		// (set) Token: 0x060012D8 RID: 4824 RVA: 0x0003970F File Offset: 0x0003790F
		public int? AccessState
		{
			get
			{
				return (int?)this[DeviceCommonSchema.AccessStateProperty];
			}
			set
			{
				this[DeviceCommonSchema.AccessStateProperty] = value;
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x00039722 File Offset: 0x00037922
		// (set) Token: 0x060012DA RID: 4826 RVA: 0x00039734 File Offset: 0x00037934
		public int? AccessStateReason
		{
			get
			{
				return (int?)this[DeviceCommonSchema.AccessStateReasonProperty];
			}
			set
			{
				this[DeviceCommonSchema.AccessStateReasonProperty] = value;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x00039747 File Offset: 0x00037947
		// (set) Token: 0x060012DC RID: 4828 RVA: 0x00039759 File Offset: 0x00037959
		public Guid ActivityId
		{
			get
			{
				return (Guid)this[DeviceCommonSchema.ActivityIdProperty];
			}
			set
			{
				this[DeviceCommonSchema.ActivityIdProperty] = value;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x0003976C File Offset: 0x0003796C
		// (set) Token: 0x060012DE RID: 4830 RVA: 0x0003977E File Offset: 0x0003797E
		public int DateKey
		{
			get
			{
				return (int)this[DeviceCommonSchema.DateKeyProperty];
			}
			set
			{
				this[DeviceCommonSchema.DateKeyProperty] = value;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060012DF RID: 4831 RVA: 0x00039791 File Offset: 0x00037991
		// (set) Token: 0x060012E0 RID: 4832 RVA: 0x000397A3 File Offset: 0x000379A3
		public DateTime? DeletedTime
		{
			get
			{
				return (DateTime?)this[DeviceCommonSchema.DeletedTimeProperty];
			}
			set
			{
				this[DeviceCommonSchema.DeletedTimeProperty] = value;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x000397B6 File Offset: 0x000379B6
		// (set) Token: 0x060012E2 RID: 4834 RVA: 0x000397C8 File Offset: 0x000379C8
		public Guid DeviceId
		{
			get
			{
				return (Guid)this[DeviceCommonSchema.DeviceIdProperty];
			}
			set
			{
				this[DeviceCommonSchema.DeviceIdProperty] = value;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060012E3 RID: 4835 RVA: 0x000397DB File Offset: 0x000379DB
		// (set) Token: 0x060012E4 RID: 4836 RVA: 0x000397ED File Offset: 0x000379ED
		public string DeviceLanguage
		{
			get
			{
				return (string)this[DeviceCommonSchema.DeviceLanguageProperty];
			}
			set
			{
				this[DeviceCommonSchema.DeviceLanguageProperty] = value;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060012E5 RID: 4837 RVA: 0x000397FB File Offset: 0x000379FB
		// (set) Token: 0x060012E6 RID: 4838 RVA: 0x0003980D File Offset: 0x00037A0D
		public string DeviceModel
		{
			get
			{
				return (string)this[DeviceCommonSchema.DeviceModelProperty];
			}
			set
			{
				this[DeviceCommonSchema.DeviceModelProperty] = value;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060012E7 RID: 4839 RVA: 0x0003981B File Offset: 0x00037A1B
		// (set) Token: 0x060012E8 RID: 4840 RVA: 0x0003982D File Offset: 0x00037A2D
		public string DeviceName
		{
			get
			{
				return (string)this[DeviceCommonSchema.DeviceNameProperty];
			}
			set
			{
				this[DeviceCommonSchema.DeviceNameProperty] = value;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x0003983B File Offset: 0x00037A3B
		// (set) Token: 0x060012EA RID: 4842 RVA: 0x0003984D File Offset: 0x00037A4D
		public string DeviceType
		{
			get
			{
				return (string)this[DeviceCommonSchema.DeviceTypeProperty];
			}
			set
			{
				this[DeviceCommonSchema.DeviceTypeProperty] = value;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060012EB RID: 4843 RVA: 0x0003985B File Offset: 0x00037A5B
		// (set) Token: 0x060012EC RID: 4844 RVA: 0x0003986D File Offset: 0x00037A6D
		public string EASId
		{
			get
			{
				return (string)this[DeviceCommonSchema.EASIdProperty];
			}
			set
			{
				this[DeviceCommonSchema.EASIdProperty] = value;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060012ED RID: 4845 RVA: 0x0003987B File Offset: 0x00037A7B
		// (set) Token: 0x060012EE RID: 4846 RVA: 0x0003988D File Offset: 0x00037A8D
		public string EASVersion
		{
			get
			{
				return (string)this[DeviceCommonSchema.EASVersionProperty];
			}
			set
			{
				this[DeviceCommonSchema.EASVersionProperty] = value;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060012EF RID: 4847 RVA: 0x0003989B File Offset: 0x00037A9B
		// (set) Token: 0x060012F0 RID: 4848 RVA: 0x000398AD File Offset: 0x00037AAD
		public int HashBucket
		{
			get
			{
				return (int)this[DeviceCommonSchema.HashBucketProperty];
			}
			set
			{
				this[DeviceCommonSchema.HashBucketProperty] = value;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x000398C0 File Offset: 0x00037AC0
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(Guid.NewGuid().ToString());
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x000398E5 File Offset: 0x00037AE5
		// (set) Token: 0x060012F3 RID: 4851 RVA: 0x000398F7 File Offset: 0x00037AF7
		public string IMEI
		{
			get
			{
				return (string)this[DeviceCommonSchema.IMEIProperty];
			}
			set
			{
				this[DeviceCommonSchema.IMEIProperty] = value;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060012F4 RID: 4852 RVA: 0x00039905 File Offset: 0x00037B05
		// (set) Token: 0x060012F5 RID: 4853 RVA: 0x00039917 File Offset: 0x00037B17
		public Guid? IntuneId
		{
			get
			{
				return (Guid?)this[DeviceCommonSchema.IntuneIdProperty];
			}
			set
			{
				this[DeviceCommonSchema.IntuneIdProperty] = value;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x0003992A File Offset: 0x00037B2A
		// (set) Token: 0x060012F7 RID: 4855 RVA: 0x0003993C File Offset: 0x00037B3C
		public string MobileNetwork
		{
			get
			{
				return (string)this[DeviceCommonSchema.MobileNetworkProperty];
			}
			set
			{
				this[DeviceCommonSchema.MobileNetworkProperty] = value;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x0003994A File Offset: 0x00037B4A
		// (set) Token: 0x060012F9 RID: 4857 RVA: 0x0003995C File Offset: 0x00037B5C
		public Guid OrganizationalUnitRoot
		{
			get
			{
				return (Guid)this[DeviceCommonSchema.OrganizationalUnitRootProperty];
			}
			set
			{
				this[DeviceCommonSchema.OrganizationalUnitRootProperty] = value;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x0003996F File Offset: 0x00037B6F
		// (set) Token: 0x060012FB RID: 4859 RVA: 0x00039981 File Offset: 0x00037B81
		public string PhoneNumber
		{
			get
			{
				return (string)this[DeviceCommonSchema.PhoneNumberProperty];
			}
			set
			{
				this[DeviceCommonSchema.PhoneNumberProperty] = value;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x060012FC RID: 4860 RVA: 0x0003998F File Offset: 0x00037B8F
		// (set) Token: 0x060012FD RID: 4861 RVA: 0x000399A1 File Offset: 0x00037BA1
		public string Platform
		{
			get
			{
				return (string)this[DeviceCommonSchema.PlatformProperty];
			}
			set
			{
				this[DeviceCommonSchema.PlatformProperty] = value;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x000399AF File Offset: 0x00037BAF
		// (set) Token: 0x060012FF RID: 4863 RVA: 0x000399C1 File Offset: 0x00037BC1
		public string PolicyApplied
		{
			get
			{
				return (string)this[DeviceCommonSchema.PolicyAppliedProperty];
			}
			set
			{
				this[DeviceCommonSchema.PolicyAppliedProperty] = value;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x000399CF File Offset: 0x00037BCF
		// (set) Token: 0x06001301 RID: 4865 RVA: 0x000399E1 File Offset: 0x00037BE1
		public DateTime TimeStamp
		{
			get
			{
				return (DateTime)this[DeviceCommonSchema.TimeStampProperty];
			}
			set
			{
				this[DeviceCommonSchema.TimeStampProperty] = value;
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x000399F4 File Offset: 0x00037BF4
		// (set) Token: 0x06001303 RID: 4867 RVA: 0x00039A06 File Offset: 0x00037C06
		public string User
		{
			get
			{
				return (string)this[DeviceCommonSchema.UserProperty];
			}
			set
			{
				this[DeviceCommonSchema.UserProperty] = value;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x00039A14 File Offset: 0x00037C14
		// (set) Token: 0x06001305 RID: 4869 RVA: 0x00039A26 File Offset: 0x00037C26
		public string UserAgent
		{
			get
			{
				return (string)this[DeviceCommonSchema.UserAgentProperty];
			}
			set
			{
				this[DeviceCommonSchema.UserAgentProperty] = value;
			}
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x00039A34 File Offset: 0x00037C34
		public override IEnumerable<PropertyDefinition> GetPropertyDefinitions(bool isChangedOnly)
		{
			if (isChangedOnly)
			{
				return base.GetPropertyDefinitions(isChangedOnly);
			}
			return DeviceData.propertydefinitions;
		}

		// Token: 0x04000921 RID: 2337
		internal static readonly HygienePropertyDefinition[] propertydefinitions = new HygienePropertyDefinition[]
		{
			DeviceCommonSchema.OrganizationalUnitRootProperty,
			DeviceCommonSchema.HashBucketProperty,
			DeviceCommonSchema.DeviceIdProperty,
			DeviceCommonSchema.EASIdProperty,
			DeviceCommonSchema.IntuneIdProperty,
			DeviceCommonSchema.UserProperty,
			DeviceCommonSchema.DeviceNameProperty,
			DeviceCommonSchema.DeviceModelProperty,
			DeviceCommonSchema.DeviceTypeProperty,
			DeviceCommonSchema.IMEIProperty,
			DeviceCommonSchema.PhoneNumberProperty,
			DeviceCommonSchema.MobileNetworkProperty,
			DeviceCommonSchema.EASVersionProperty,
			DeviceCommonSchema.UserAgentProperty,
			DeviceCommonSchema.DeviceLanguageProperty,
			DeviceCommonSchema.DeletedTimeProperty,
			DeviceCommonSchema.ActivityIdProperty,
			DeviceCommonSchema.TimeStampProperty,
			DeviceCommonSchema.DateKeyProperty,
			DeviceCommonSchema.PlatformProperty,
			DeviceCommonSchema.AccessStateProperty,
			DeviceCommonSchema.AccessStateReasonProperty,
			DeviceCommonSchema.AccessSetByProperty,
			DeviceCommonSchema.PolicyAppliedProperty
		};
	}
}
