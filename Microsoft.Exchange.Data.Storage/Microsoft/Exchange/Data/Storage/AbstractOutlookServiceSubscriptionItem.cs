using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002DB RID: 731
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AbstractOutlookServiceSubscriptionItem : AbstractItem, IOutlookServiceSubscriptionItem, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06001F46 RID: 8006 RVA: 0x000864BD File Offset: 0x000846BD
		// (set) Token: 0x06001F47 RID: 8007 RVA: 0x000864C4 File Offset: 0x000846C4
		public virtual string SubscriptionId
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06001F48 RID: 8008 RVA: 0x000864CB File Offset: 0x000846CB
		// (set) Token: 0x06001F49 RID: 8009 RVA: 0x000864D2 File Offset: 0x000846D2
		public virtual ExDateTime LastUpdateTimeUTC
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06001F4A RID: 8010 RVA: 0x000864D9 File Offset: 0x000846D9
		// (set) Token: 0x06001F4B RID: 8011 RVA: 0x000864E0 File Offset: 0x000846E0
		public virtual string PackageId
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06001F4C RID: 8012 RVA: 0x000864E7 File Offset: 0x000846E7
		// (set) Token: 0x06001F4D RID: 8013 RVA: 0x000864EE File Offset: 0x000846EE
		public virtual string AppId
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06001F4E RID: 8014 RVA: 0x000864F5 File Offset: 0x000846F5
		// (set) Token: 0x06001F4F RID: 8015 RVA: 0x000864FC File Offset: 0x000846FC
		public virtual string DeviceNotificationId
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06001F50 RID: 8016 RVA: 0x00086503 File Offset: 0x00084703
		// (set) Token: 0x06001F51 RID: 8017 RVA: 0x0008650A File Offset: 0x0008470A
		public virtual ExDateTime ExpirationTime
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06001F52 RID: 8018 RVA: 0x00086511 File Offset: 0x00084711
		// (set) Token: 0x06001F53 RID: 8019 RVA: 0x00086518 File Offset: 0x00084718
		public virtual bool LockScreen
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}
	}
}
