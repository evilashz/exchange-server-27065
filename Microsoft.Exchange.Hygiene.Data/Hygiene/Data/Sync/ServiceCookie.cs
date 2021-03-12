using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Sync
{
	// Token: 0x02000220 RID: 544
	public class ServiceCookie : BaseCookie
	{
		// Token: 0x0600163F RID: 5695 RVA: 0x0004514F File Offset: 0x0004334F
		public ServiceCookie()
		{
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x00045157 File Offset: 0x00043357
		public ServiceCookie(string serviceInstance, byte[] data) : base(serviceInstance, data)
		{
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x00045161 File Offset: 0x00043361
		// (set) Token: 0x06001642 RID: 5698 RVA: 0x00045173 File Offset: 0x00043373
		public DateTime LastCompletedTime
		{
			get
			{
				return (DateTime)this[ServiceCookieSchema.LastCompletedTimeProperty];
			}
			set
			{
				this[ServiceCookieSchema.LastCompletedTimeProperty] = value;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001643 RID: 5699 RVA: 0x00045186 File Offset: 0x00043386
		internal override ADObjectSchema Schema
		{
			get
			{
				return ServiceCookie.schema;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001644 RID: 5700 RVA: 0x0004518D File Offset: 0x0004338D
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ServiceCookie.mostDerivedClass;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x00045194 File Offset: 0x00043394
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04000B44 RID: 2884
		private static readonly ServiceCookieSchema schema = ObjectSchema.GetInstance<ServiceCookieSchema>();

		// Token: 0x04000B45 RID: 2885
		private static string mostDerivedClass = "ServiceCookie";
	}
}
