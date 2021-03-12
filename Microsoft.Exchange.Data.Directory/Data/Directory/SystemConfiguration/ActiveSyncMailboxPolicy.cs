using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200031C RID: 796
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class ActiveSyncMailboxPolicy : MobileMailboxPolicy
	{
		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x060024E6 RID: 9446 RVA: 0x0009D945 File Offset: 0x0009BB45
		// (set) Token: 0x060024E7 RID: 9447 RVA: 0x0009D957 File Offset: 0x0009BB57
		[Parameter(Mandatory = false)]
		public bool AlphanumericDevicePasswordRequired
		{
			get
			{
				return (bool)this[ActiveSyncMailboxPolicySchema.AlphanumericDevicePasswordRequired];
			}
			set
			{
				this[ActiveSyncMailboxPolicySchema.AlphanumericDevicePasswordRequired] = value;
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x060024E8 RID: 9448 RVA: 0x0009D96A File Offset: 0x0009BB6A
		// (set) Token: 0x060024E9 RID: 9449 RVA: 0x0009D97C File Offset: 0x0009BB7C
		[Parameter(Mandatory = false)]
		public bool DevicePasswordEnabled
		{
			get
			{
				return (bool)this[ActiveSyncMailboxPolicySchema.DevicePasswordEnabled];
			}
			set
			{
				this[ActiveSyncMailboxPolicySchema.DevicePasswordEnabled] = value;
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x060024EA RID: 9450 RVA: 0x0009D98F File Offset: 0x0009BB8F
		// (set) Token: 0x060024EB RID: 9451 RVA: 0x0009D9A1 File Offset: 0x0009BBA1
		[Parameter(Mandatory = false)]
		public bool AllowSimpleDevicePassword
		{
			get
			{
				return (bool)this[ActiveSyncMailboxPolicySchema.AllowSimpleDevicePassword];
			}
			set
			{
				this[ActiveSyncMailboxPolicySchema.AllowSimpleDevicePassword] = value;
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x060024EC RID: 9452 RVA: 0x0009D9B4 File Offset: 0x0009BBB4
		// (set) Token: 0x060024ED RID: 9453 RVA: 0x0009D9C6 File Offset: 0x0009BBC6
		[Parameter(Mandatory = false)]
		public int? MinDevicePasswordLength
		{
			get
			{
				return (int?)this[ActiveSyncMailboxPolicySchema.MinDevicePasswordLength];
			}
			set
			{
				this[ActiveSyncMailboxPolicySchema.MinDevicePasswordLength] = value;
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x060024EE RID: 9454 RVA: 0x0009D9D9 File Offset: 0x0009BBD9
		// (set) Token: 0x060024EF RID: 9455 RVA: 0x0009D9EB File Offset: 0x0009BBEB
		[Parameter(Mandatory = false)]
		public bool IsDefaultPolicy
		{
			get
			{
				return (bool)this[ActiveSyncMailboxPolicySchema.IsDefaultPolicy];
			}
			set
			{
				this[ActiveSyncMailboxPolicySchema.IsDefaultPolicy] = value;
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x060024F0 RID: 9456 RVA: 0x0009D9FE File Offset: 0x0009BBFE
		// (set) Token: 0x060024F1 RID: 9457 RVA: 0x0009DA10 File Offset: 0x0009BC10
		[Parameter(Mandatory = false)]
		public Unlimited<EnhancedTimeSpan> MaxInactivityTimeDeviceLock
		{
			get
			{
				return (Unlimited<EnhancedTimeSpan>)this[ActiveSyncMailboxPolicySchema.MaxInactivityTimeDeviceLock];
			}
			set
			{
				this[ActiveSyncMailboxPolicySchema.MaxInactivityTimeDeviceLock] = value;
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x060024F2 RID: 9458 RVA: 0x0009DA23 File Offset: 0x0009BC23
		// (set) Token: 0x060024F3 RID: 9459 RVA: 0x0009DA35 File Offset: 0x0009BC35
		[Parameter(Mandatory = false)]
		public Unlimited<int> MaxDevicePasswordFailedAttempts
		{
			get
			{
				return (Unlimited<int>)this[ActiveSyncMailboxPolicySchema.MaxDevicePasswordFailedAttempts];
			}
			set
			{
				this[ActiveSyncMailboxPolicySchema.MaxDevicePasswordFailedAttempts] = value;
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x060024F4 RID: 9460 RVA: 0x0009DA48 File Offset: 0x0009BC48
		// (set) Token: 0x060024F5 RID: 9461 RVA: 0x0009DA5A File Offset: 0x0009BC5A
		[Parameter(Mandatory = false)]
		public Unlimited<EnhancedTimeSpan> DevicePasswordExpiration
		{
			get
			{
				return (Unlimited<EnhancedTimeSpan>)this[ActiveSyncMailboxPolicySchema.DevicePasswordExpiration];
			}
			set
			{
				this[ActiveSyncMailboxPolicySchema.DevicePasswordExpiration] = value;
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x060024F6 RID: 9462 RVA: 0x0009DA6D File Offset: 0x0009BC6D
		// (set) Token: 0x060024F7 RID: 9463 RVA: 0x0009DA7F File Offset: 0x0009BC7F
		[Parameter(Mandatory = false)]
		public int DevicePasswordHistory
		{
			get
			{
				return (int)this[ActiveSyncMailboxPolicySchema.DevicePasswordHistory];
			}
			set
			{
				this[ActiveSyncMailboxPolicySchema.DevicePasswordHistory] = value;
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x060024F8 RID: 9464 RVA: 0x0009DA92 File Offset: 0x0009BC92
		// (set) Token: 0x060024F9 RID: 9465 RVA: 0x0009DAA4 File Offset: 0x0009BCA4
		[Parameter(Mandatory = false)]
		public int MinDevicePasswordComplexCharacters
		{
			get
			{
				return (int)this[ActiveSyncMailboxPolicySchema.MinDevicePasswordComplexCharacters];
			}
			set
			{
				this[ActiveSyncMailboxPolicySchema.MinDevicePasswordComplexCharacters] = value;
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x060024FA RID: 9466 RVA: 0x0009DAB7 File Offset: 0x0009BCB7
		// (set) Token: 0x060024FB RID: 9467 RVA: 0x0009DABE File Offset: 0x0009BCBE
		private new bool AlphanumericPasswordRequired
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

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x060024FC RID: 9468 RVA: 0x0009DAC5 File Offset: 0x0009BCC5
		// (set) Token: 0x060024FD RID: 9469 RVA: 0x0009DACC File Offset: 0x0009BCCC
		private new bool PasswordEnabled
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

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x060024FE RID: 9470 RVA: 0x0009DAD3 File Offset: 0x0009BCD3
		// (set) Token: 0x060024FF RID: 9471 RVA: 0x0009DADA File Offset: 0x0009BCDA
		private new bool AllowSimplePassword
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

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06002500 RID: 9472 RVA: 0x0009DAE1 File Offset: 0x0009BCE1
		// (set) Token: 0x06002501 RID: 9473 RVA: 0x0009DAE8 File Offset: 0x0009BCE8
		private new int? MinPasswordLength
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

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06002502 RID: 9474 RVA: 0x0009DAEF File Offset: 0x0009BCEF
		// (set) Token: 0x06002503 RID: 9475 RVA: 0x0009DAF6 File Offset: 0x0009BCF6
		private new Unlimited<EnhancedTimeSpan> MaxInactivityTimeLock
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

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06002504 RID: 9476 RVA: 0x0009DAFD File Offset: 0x0009BCFD
		// (set) Token: 0x06002505 RID: 9477 RVA: 0x0009DB04 File Offset: 0x0009BD04
		private new Unlimited<int> MaxPasswordFailedAttempts
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

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06002506 RID: 9478 RVA: 0x0009DB0B File Offset: 0x0009BD0B
		// (set) Token: 0x06002507 RID: 9479 RVA: 0x0009DB12 File Offset: 0x0009BD12
		private new Unlimited<EnhancedTimeSpan> PasswordExpiration
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

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06002508 RID: 9480 RVA: 0x0009DB19 File Offset: 0x0009BD19
		// (set) Token: 0x06002509 RID: 9481 RVA: 0x0009DB20 File Offset: 0x0009BD20
		private new int PasswordHistory
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

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x0600250A RID: 9482 RVA: 0x0009DB27 File Offset: 0x0009BD27
		// (set) Token: 0x0600250B RID: 9483 RVA: 0x0009DB2E File Offset: 0x0009BD2E
		private new int MinPasswordComplexCharacters
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

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x0600250C RID: 9484 RVA: 0x0009DB35 File Offset: 0x0009BD35
		// (set) Token: 0x0600250D RID: 9485 RVA: 0x0009DB3C File Offset: 0x0009BD3C
		private new bool AllowMicrosoftPushNotifications
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x0600250E RID: 9486 RVA: 0x0009DB43 File Offset: 0x0009BD43
		// (set) Token: 0x0600250F RID: 9487 RVA: 0x0009DB4A File Offset: 0x0009BD4A
		private new bool AllowGooglePushNotifications
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}
	}
}
