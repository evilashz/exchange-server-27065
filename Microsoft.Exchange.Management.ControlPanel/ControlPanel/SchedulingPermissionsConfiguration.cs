using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000B9 RID: 185
	[DataContract]
	public class SchedulingPermissionsConfiguration : ResourceConfigurationBase
	{
		// Token: 0x06001C90 RID: 7312 RVA: 0x00058C7C File Offset: 0x00056E7C
		public SchedulingPermissionsConfiguration(CalendarConfiguration calendarConfiguration) : base(calendarConfiguration)
		{
		}

		// Token: 0x17001903 RID: 6403
		// (get) Token: 0x06001C91 RID: 7313 RVA: 0x00058C85 File Offset: 0x00056E85
		// (set) Token: 0x06001C92 RID: 7314 RVA: 0x00058C9D File Offset: 0x00056E9D
		[DataMember]
		public string AllBookInPolicy
		{
			get
			{
				return base.CalendarConfiguration.AllBookInPolicy.ToJsonString(null);
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001904 RID: 6404
		// (get) Token: 0x06001C93 RID: 7315 RVA: 0x00058CA4 File Offset: 0x00056EA4
		// (set) Token: 0x06001C94 RID: 7316 RVA: 0x00058CBC File Offset: 0x00056EBC
		[DataMember]
		public string AllRequestInPolicy
		{
			get
			{
				return base.CalendarConfiguration.AllRequestInPolicy.ToJsonString(null);
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001905 RID: 6405
		// (get) Token: 0x06001C95 RID: 7317 RVA: 0x00058CC4 File Offset: 0x00056EC4
		// (set) Token: 0x06001C96 RID: 7318 RVA: 0x00058CE9 File Offset: 0x00056EE9
		[DataMember]
		public string AllRequestOutOfPolicy
		{
			get
			{
				return base.CalendarConfiguration.AllRequestOutOfPolicy.ToString().ToLowerInvariant();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001906 RID: 6406
		// (get) Token: 0x06001C97 RID: 7319 RVA: 0x00058CF0 File Offset: 0x00056EF0
		// (set) Token: 0x06001C98 RID: 7320 RVA: 0x00058D1B File Offset: 0x00056F1B
		[DataMember]
		public PeopleIdentity[] BookInPolicy
		{
			get
			{
				if (base.CalendarConfiguration.BookInPolicy == null)
				{
					return null;
				}
				return RecipientObjectResolver.Instance.ResolveLegacyDNs(base.CalendarConfiguration.BookInPolicy).ToPeopleIdentityArray();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001907 RID: 6407
		// (get) Token: 0x06001C99 RID: 7321 RVA: 0x00058D22 File Offset: 0x00056F22
		// (set) Token: 0x06001C9A RID: 7322 RVA: 0x00058D4D File Offset: 0x00056F4D
		[DataMember]
		public PeopleIdentity[] RequestInPolicy
		{
			get
			{
				if (base.CalendarConfiguration.RequestInPolicy == null)
				{
					return null;
				}
				return RecipientObjectResolver.Instance.ResolveLegacyDNs(base.CalendarConfiguration.RequestInPolicy).ToPeopleIdentityArray();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001908 RID: 6408
		// (get) Token: 0x06001C9B RID: 7323 RVA: 0x00058D54 File Offset: 0x00056F54
		// (set) Token: 0x06001C9C RID: 7324 RVA: 0x00058D7F File Offset: 0x00056F7F
		[DataMember]
		public PeopleIdentity[] RequestOutOfPolicy
		{
			get
			{
				if (base.CalendarConfiguration.RequestOutOfPolicy == null)
				{
					return null;
				}
				return RecipientObjectResolver.Instance.ResolveLegacyDNs(base.CalendarConfiguration.RequestOutOfPolicy).ToPeopleIdentityArray();
			}
			set
			{
				throw new NotSupportedException();
			}
		}
	}
}
