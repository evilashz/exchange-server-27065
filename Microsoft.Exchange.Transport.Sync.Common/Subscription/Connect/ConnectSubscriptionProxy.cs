using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect
{
	// Token: 0x020000D6 RID: 214
	[Serializable]
	public sealed class ConnectSubscriptionProxy : PimSubscriptionProxy
	{
		// Token: 0x06000661 RID: 1633 RVA: 0x0001FAE2 File Offset: 0x0001DCE2
		public ConnectSubscriptionProxy() : this(new ConnectSubscription())
		{
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0001FAEF File Offset: 0x0001DCEF
		internal ConnectSubscriptionProxy(ConnectSubscription subscription) : base(subscription)
		{
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x0001FAF8 File Offset: 0x0001DCF8
		public bool HasAccessToken
		{
			get
			{
				return ((ConnectSubscription)base.Subscription).HasAccessToken;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x0001FB0A File Offset: 0x0001DD0A
		public int EncryptedAccessTokenHash
		{
			get
			{
				return ((ConnectSubscription)base.Subscription).EncryptedAccessTokenHash;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x0001FB1C File Offset: 0x0001DD1C
		public ConnectState ConnectState
		{
			get
			{
				return ((ConnectSubscription)base.Subscription).ConnectState;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x0001FB2E File Offset: 0x0001DD2E
		// (set) Token: 0x06000667 RID: 1639 RVA: 0x0001FB40 File Offset: 0x0001DD40
		public string AppId
		{
			get
			{
				return ((ConnectSubscription)base.Subscription).AppId;
			}
			set
			{
				((ConnectSubscription)base.Subscription).AppId = value;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x0001FB53 File Offset: 0x0001DD53
		// (set) Token: 0x06000669 RID: 1641 RVA: 0x0001FB65 File Offset: 0x0001DD65
		public string UserId
		{
			get
			{
				return ((ConnectSubscription)base.Subscription).UserId;
			}
			set
			{
				((ConnectSubscription)base.Subscription).UserId = value;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x0001FB78 File Offset: 0x0001DD78
		// (set) Token: 0x0600066B RID: 1643 RVA: 0x0001FB8A File Offset: 0x0001DD8A
		public bool InitialSyncInRecoveryMode
		{
			get
			{
				return ((ConnectSubscription)base.Subscription).InitialSyncInRecoveryMode;
			}
			internal set
			{
				((ConnectSubscription)base.Subscription).InitialSyncInRecoveryMode = value;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x0001FB9D File Offset: 0x0001DD9D
		// (set) Token: 0x0600066D RID: 1645 RVA: 0x0001FBA5 File Offset: 0x0001DDA5
		internal string AppAuthorizationCode
		{
			get
			{
				return this.appAuthorizationCode;
			}
			set
			{
				this.appAuthorizationCode = value;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x0001FBAE File Offset: 0x0001DDAE
		// (set) Token: 0x0600066F RID: 1647 RVA: 0x0001FBC0 File Offset: 0x0001DDC0
		internal string MessageClass
		{
			get
			{
				return ((ConnectSubscription)base.Subscription).MessageClass;
			}
			set
			{
				((ConnectSubscription)base.Subscription).MessageClass = value;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x0001FBD3 File Offset: 0x0001DDD3
		// (set) Token: 0x06000671 RID: 1649 RVA: 0x0001FBE5 File Offset: 0x0001DDE5
		internal Guid ProviderGuid
		{
			get
			{
				return ((ConnectSubscription)base.Subscription).ProviderGuid;
			}
			set
			{
				((ConnectSubscription)base.Subscription).ProviderGuid = value;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x0001FBF8 File Offset: 0x0001DDF8
		// (set) Token: 0x06000673 RID: 1651 RVA: 0x0001FC00 File Offset: 0x0001DE00
		internal string RedirectUri { get; set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x0001FC09 File Offset: 0x0001DE09
		// (set) Token: 0x06000675 RID: 1653 RVA: 0x0001FC11 File Offset: 0x0001DE11
		internal string RequestToken { get; set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x0001FC1A File Offset: 0x0001DE1A
		// (set) Token: 0x06000677 RID: 1655 RVA: 0x0001FC22 File Offset: 0x0001DE22
		internal string RequestSecret
		{
			get
			{
				return this.requestSecret;
			}
			set
			{
				this.requestSecret = value;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x0001FC2B File Offset: 0x0001DE2B
		// (set) Token: 0x06000679 RID: 1657 RVA: 0x0001FC33 File Offset: 0x0001DE33
		internal string OAuthVerifier { get; set; }

		// Token: 0x0600067A RID: 1658 RVA: 0x0001FC3C File Offset: 0x0001DE3C
		public override ValidationError[] Validate()
		{
			List<ValidationError> list = new List<ValidationError>();
			if (string.IsNullOrEmpty(this.MessageClass))
			{
				list.Add(new PropertyValidationError(DataStrings.PropertyNotEmptyOrNull, ConnectSubscriptionProxy.ConnectSubscriptionSchema.MessageClass, this.MessageClass));
			}
			if (Guid.Empty.Equals(this.ProviderGuid))
			{
				list.Add(new PropertyValidationError(DataStrings.PropertyNotEmptyOrNull, ConnectSubscriptionProxy.ConnectSubscriptionSchema.ProviderGuid, this.ProviderGuid));
			}
			if (string.IsNullOrEmpty(this.AppId))
			{
				list.Add(new PropertyValidationError(DataStrings.PropertyNotEmptyOrNull, ConnectSubscriptionProxy.ConnectSubscriptionSchema.AppId, this.AppId));
			}
			if (string.IsNullOrEmpty(this.UserId))
			{
				list.Add(new PropertyValidationError(DataStrings.PropertyNotEmptyOrNull, ConnectSubscriptionProxy.ConnectSubscriptionSchema.UserId, this.UserId));
			}
			return base.Validate().Concat(list).ToArray<ValidationError>();
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001FD0D File Offset: 0x0001DF0D
		internal void AssignAccessToken(string accessTokenInClearText)
		{
			((ConnectSubscription)base.Subscription).AccessTokenInClearText = accessTokenInClearText;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0001FD20 File Offset: 0x0001DF20
		internal void AssignAccessTokenSecret(string accessTokenSecretInClearText)
		{
			((ConnectSubscription)base.Subscription).AccessTokenSecretInClearText = accessTokenSecretInClearText;
		}

		// Token: 0x0400037C RID: 892
		[NonSerialized]
		private string appAuthorizationCode;

		// Token: 0x0400037D RID: 893
		[NonSerialized]
		private string requestSecret;

		// Token: 0x020000D7 RID: 215
		private sealed class ConnectSubscriptionSchema : SimpleProviderObjectSchema
		{
			// Token: 0x04000381 RID: 897
			internal static readonly SimpleProviderPropertyDefinition MessageClass = new SimpleProviderPropertyDefinition("MessageClass", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.Mandatory, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04000382 RID: 898
			internal static readonly SimpleProviderPropertyDefinition ProviderGuid = new SimpleProviderPropertyDefinition("ProviderGuid", ExchangeObjectVersion.Exchange2007, typeof(Guid), PropertyDefinitionFlags.Mandatory, Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04000383 RID: 899
			internal static readonly SimpleProviderPropertyDefinition AppId = new SimpleProviderPropertyDefinition("AppId", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.Mandatory, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04000384 RID: 900
			internal static readonly SimpleProviderPropertyDefinition UserId = new SimpleProviderPropertyDefinition("UserId", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.Mandatory, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}
	}
}
