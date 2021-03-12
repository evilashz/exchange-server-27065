using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.ActiveManager
{
	// Token: 0x02000311 RID: 785
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AmDbActionCode
	{
		// Token: 0x06002312 RID: 8978 RVA: 0x0008E160 File Offset: 0x0008C360
		public static int ToInt(AmDbActionInitiator initiator, AmDbActionReason reason, AmDbActionCategory category)
		{
			return (int)(category | (AmDbActionCategory)((int)reason << 8) | (AmDbActionCategory)((int)initiator << 18));
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x0008E17C File Offset: 0x0008C37C
		public static void ToEnumFields(int actionCode, out AmDbActionInitiator initiator, out AmDbActionReason reason, out AmDbActionCategory category)
		{
			category = (AmDbActionCategory)(actionCode & 255);
			if (category > (AmDbActionCategory)255)
			{
				throw new AmInvalidActionCodeException(actionCode, category.GetType().Name, category.ToString());
			}
			reason = (AmDbActionReason)(actionCode >> 8 & 1023);
			if (reason > (AmDbActionReason)1023)
			{
				throw new AmInvalidActionCodeException(actionCode, reason.GetType().Name, reason.ToString());
			}
			initiator = (AmDbActionInitiator)(actionCode >> 18 & 255);
			if (initiator > (AmDbActionInitiator)255)
			{
				throw new AmInvalidActionCodeException(actionCode, initiator.GetType().Name, initiator.ToString());
			}
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x0008E230 File Offset: 0x0008C430
		public AmDbActionCode(AmDbActionInitiator initiator, AmDbActionReason reason, AmDbActionCategory category)
		{
			this.m_initiator = initiator;
			this.m_reason = reason;
			this.m_category = category;
			this.m_intValue = AmDbActionCode.ToInt(initiator, reason, category);
			this.UpdateStringRepresentation();
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x0008E261 File Offset: 0x0008C461
		public AmDbActionCode(int actionCode)
		{
			AmDbActionCode.ToEnumFields(actionCode, out this.m_initiator, out this.m_reason, out this.m_category);
			this.UpdateStringRepresentation();
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x0008E287 File Offset: 0x0008C487
		public AmDbActionCode() : this(AmDbActionInitiator.None, AmDbActionReason.None, AmDbActionCategory.None)
		{
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06002317 RID: 8983 RVA: 0x0008E293 File Offset: 0x0008C493
		public AmDbActionInitiator Initiator
		{
			get
			{
				return this.m_initiator;
			}
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06002318 RID: 8984 RVA: 0x0008E29B File Offset: 0x0008C49B
		public AmDbActionReason Reason
		{
			get
			{
				return this.m_reason;
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06002319 RID: 8985 RVA: 0x0008E2A3 File Offset: 0x0008C4A3
		public AmDbActionCategory Category
		{
			get
			{
				return this.m_category;
			}
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x0600231A RID: 8986 RVA: 0x0008E2AB File Offset: 0x0008C4AB
		public int IntValue
		{
			get
			{
				return this.m_intValue;
			}
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x0600231B RID: 8987 RVA: 0x0008E2B3 File Offset: 0x0008C4B3
		public bool IsMoveOperation
		{
			get
			{
				return this.Category == AmDbActionCategory.Move;
			}
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x0600231C RID: 8988 RVA: 0x0008E2BF File Offset: 0x0008C4BF
		public bool IsMountOrRemountOperation
		{
			get
			{
				return this.Category == AmDbActionCategory.Mount || this.Category == AmDbActionCategory.Remount;
			}
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x0600231D RID: 8989 RVA: 0x0008E2D7 File Offset: 0x0008C4D7
		public bool IsDismountOperation
		{
			get
			{
				return this.Category == AmDbActionCategory.Dismount || this.Category == AmDbActionCategory.ForceDismount;
			}
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x0600231E RID: 8990 RVA: 0x0008E2EF File Offset: 0x0008C4EF
		public bool IsAdminOperation
		{
			get
			{
				return this.Initiator == AmDbActionInitiator.Admin;
			}
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x0600231F RID: 8991 RVA: 0x0008E2FA File Offset: 0x0008C4FA
		public bool IsAutomaticOperation
		{
			get
			{
				return this.Initiator == AmDbActionInitiator.Automatic;
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06002320 RID: 8992 RVA: 0x0008E305 File Offset: 0x0008C505
		public bool IsAdminMountOperation
		{
			get
			{
				return this.Initiator == AmDbActionInitiator.Admin && this.Category == AmDbActionCategory.Mount;
			}
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06002321 RID: 8993 RVA: 0x0008E31C File Offset: 0x0008C51C
		public bool IsAdminMoveOperation
		{
			get
			{
				return this.Initiator == AmDbActionInitiator.Admin && this.Category == AmDbActionCategory.Move;
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06002322 RID: 8994 RVA: 0x0008E333 File Offset: 0x0008C533
		public bool IsAdminDismountOperation
		{
			get
			{
				return this.Initiator == AmDbActionInitiator.Admin && this.Category == AmDbActionCategory.Dismount;
			}
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06002323 RID: 8995 RVA: 0x0008E34A File Offset: 0x0008C54A
		public bool IsAutomaticMove
		{
			get
			{
				return this.Initiator == AmDbActionInitiator.Automatic && this.Category == AmDbActionCategory.Move;
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06002324 RID: 8996 RVA: 0x0008E361 File Offset: 0x0008C561
		public bool IsAutomaticShutdownSwitchover
		{
			get
			{
				return this.Initiator == AmDbActionInitiator.Automatic && this.Reason == AmDbActionReason.SystemShutdown && this.Category == AmDbActionCategory.Move;
			}
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06002325 RID: 8997 RVA: 0x0008E381 File Offset: 0x0008C581
		public bool IsStartupAutoMount
		{
			get
			{
				return this.Initiator == AmDbActionInitiator.Automatic && (this.Reason == AmDbActionReason.Startup || this.Reason == AmDbActionReason.StoreStarted) && this.Category == AmDbActionCategory.Mount;
			}
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06002326 RID: 8998 RVA: 0x0008E3AA File Offset: 0x0008C5AA
		public bool IsAutomaticFailureItem
		{
			get
			{
				return this.Initiator == AmDbActionInitiator.Automatic && this.Reason == AmDbActionReason.FailureItem;
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06002327 RID: 8999 RVA: 0x0008E3C0 File Offset: 0x0008C5C0
		public bool IsAutomaticManagedAvailabilityFailover
		{
			get
			{
				return this.Initiator == AmDbActionInitiator.Automatic && this.Reason == AmDbActionReason.ManagedAvailability;
			}
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x0008E3D7 File Offset: 0x0008C5D7
		public static explicit operator int(AmDbActionCode actionCode)
		{
			return actionCode.IntValue;
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x0008E3DF File Offset: 0x0008C5DF
		public override string ToString()
		{
			return this.m_actionCodeStr;
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x0008E3E7 File Offset: 0x0008C5E7
		private void UpdateStringRepresentation()
		{
			this.m_actionCodeStr = string.Format("[Initiator:{0} Reason:{1} Category:{2}]", this.m_initiator, this.m_reason, this.m_category);
		}

		// Token: 0x040014BB RID: 5307
		private const int CategoryMaxValue = 255;

		// Token: 0x040014BC RID: 5308
		private const int ReasonMaxValue = 1023;

		// Token: 0x040014BD RID: 5309
		private const int InitiatorMaxValue = 255;

		// Token: 0x040014BE RID: 5310
		private string m_actionCodeStr;

		// Token: 0x040014BF RID: 5311
		private AmDbActionInitiator m_initiator;

		// Token: 0x040014C0 RID: 5312
		private AmDbActionReason m_reason;

		// Token: 0x040014C1 RID: 5313
		private AmDbActionCategory m_category;

		// Token: 0x040014C2 RID: 5314
		private int m_intValue;
	}
}
