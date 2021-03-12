using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect
{
	// Token: 0x020000D4 RID: 212
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class ConnectSubscription : PimAggregationSubscription, IConnectSubscription, ISyncWorkerData
	{
		// Token: 0x060005FD RID: 1533 RVA: 0x0001ED12 File Offset: 0x0001CF12
		public ConnectSubscription()
		{
			base.UserDisplayName = string.Empty;
			base.UserEmailAddress = SmtpAddress.Empty;
			base.AggregationType = AggregationType.PeopleConnection;
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x0001ED38 File Offset: 0x0001CF38
		public override bool SendAsNeedsVerification
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x0001ED3B File Offset: 0x0001CF3B
		public override bool SendAsCapable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x0001ED3E File Offset: 0x0001CF3E
		public override FolderSupport FolderSupport
		{
			get
			{
				return FolderSupport.ContactsOnly;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x0001ED41 File Offset: 0x0001CF41
		public override ItemSupport ItemSupport
		{
			get
			{
				return ItemSupport.Contacts;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x0001ED44 File Offset: 0x0001CF44
		public override SyncQuirks SyncQuirks
		{
			get
			{
				return SyncQuirks.None;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x0001ED47 File Offset: 0x0001CF47
		public override bool PasswordRequired
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x0001ED4A File Offset: 0x0001CF4A
		// (set) Token: 0x06000605 RID: 1541 RVA: 0x0001ED52 File Offset: 0x0001CF52
		public string MessageClass { get; internal set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x0001ED5B File Offset: 0x0001CF5B
		// (set) Token: 0x06000607 RID: 1543 RVA: 0x0001ED63 File Offset: 0x0001CF63
		public Guid ProviderGuid { get; internal set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x0001ED6C File Offset: 0x0001CF6C
		public ConnectState ConnectState
		{
			get
			{
				if (base.Status == AggregationStatus.Disabled)
				{
					return ConnectState.Disabled;
				}
				if (base.Status == AggregationStatus.Delayed)
				{
					return ConnectState.Delayed;
				}
				if (base.DetailedAggregationStatus == DetailedAggregationStatus.None)
				{
					return ConnectState.Connected;
				}
				return ConnectState.ConnectedNeedsToken;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x0001ED8F File Offset: 0x0001CF8F
		// (set) Token: 0x0600060A RID: 1546 RVA: 0x0001ED97 File Offset: 0x0001CF97
		public string AccessTokenInClearText
		{
			get
			{
				return this.accessTokenClearText;
			}
			internal set
			{
				this.accessTokenClearText = value;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x0001EDA0 File Offset: 0x0001CFA0
		// (set) Token: 0x0600060C RID: 1548 RVA: 0x0001EDA8 File Offset: 0x0001CFA8
		public bool HasAccessToken { get; private set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x0001EDB1 File Offset: 0x0001CFB1
		// (set) Token: 0x0600060E RID: 1550 RVA: 0x0001EDB9 File Offset: 0x0001CFB9
		public int EncryptedAccessTokenHash { get; private set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x0001EDC2 File Offset: 0x0001CFC2
		// (set) Token: 0x06000610 RID: 1552 RVA: 0x0001EDCA File Offset: 0x0001CFCA
		public string AppId { get; internal set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0001EDD3 File Offset: 0x0001CFD3
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x0001EDDB File Offset: 0x0001CFDB
		public string UserId { get; internal set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0001EDE4 File Offset: 0x0001CFE4
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x0001EDEC File Offset: 0x0001CFEC
		public string AccessTokenSecretInClearText
		{
			get
			{
				return this.accessTokenSecretClearText;
			}
			internal set
			{
				this.accessTokenSecretClearText = value;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x0001EDF5 File Offset: 0x0001CFF5
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x0001EE03 File Offset: 0x0001D003
		internal string EncryptedAccessToken
		{
			get
			{
				return this.EncryptString(this.accessTokenClearText);
			}
			set
			{
				this.accessTokenClearText = this.DecryptToUnsecureString(value);
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0001EE12 File Offset: 0x0001D012
		// (set) Token: 0x06000618 RID: 1560 RVA: 0x0001EE20 File Offset: 0x0001D020
		internal string EncryptedAccessTokenSecret
		{
			get
			{
				return this.EncryptString(this.accessTokenSecretClearText);
			}
			set
			{
				this.accessTokenSecretClearText = this.DecryptToUnsecureString(value);
			}
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0001EE2F File Offset: 0x0001D02F
		public static bool IsDkmException(Exception e)
		{
			return AggregationSubscription.ExchangeGroupKeyObject.IsDkmException(e);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0001EE3C File Offset: 0x0001D03C
		public override PimSubscriptionProxy CreateSubscriptionProxy()
		{
			return new ConnectSubscriptionProxy(this);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001EE44 File Offset: 0x0001D044
		protected override void SetPropertiesToMessageObject(MessageItem message)
		{
			base.AggregationType = AggregationType.PeopleConnection;
			base.SubscriptionEvents = SubscriptionEvents.WorkItemCompleted;
			message[StoreObjectSchema.ItemClass] = this.MessageClass;
			message[MessageItemSchema.SharingProviderGuid] = this.ProviderGuid;
			message[AggregationSubscriptionMessageSchema.SharingEncryptedAccessToken] = (this.EncryptedAccessToken ?? string.Empty);
			message[AggregationSubscriptionMessageSchema.SharingEncryptedAccessTokenSecret] = (this.EncryptedAccessTokenSecret ?? string.Empty);
			message[AggregationSubscriptionMessageSchema.SharingAppId] = this.AppId;
			message[AggregationSubscriptionMessageSchema.SharingUserId] = this.UserId;
			base.SetPropertiesToMessageObject(message);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001EEE4 File Offset: 0x0001D0E4
		protected override void LoadProperties(MessageItem message)
		{
			base.LoadProperties(message);
			if (base.AggregationType != AggregationType.PeopleConnection)
			{
				throw new SyncPropertyValidationException("AggregationType", base.AggregationType.ToString());
			}
			string messageClass;
			base.GetStringProperty(message, StoreObjectSchema.ItemClass, false, false, null, null, out messageClass);
			this.MessageClass = messageClass;
			this.ProviderGuid = SyncUtilities.SafeGetProperty<Guid>(message, MessageItemSchema.SharingProviderGuid);
			string appId;
			base.GetStringProperty(message, AggregationSubscriptionMessageSchema.SharingAppId, false, false, null, null, out appId);
			this.AppId = appId;
			string userId;
			base.GetStringProperty(message, AggregationSubscriptionMessageSchema.SharingUserId, false, false, null, null, out userId);
			this.UserId = userId;
			string text;
			base.GetStringProperty(message, AggregationSubscriptionMessageSchema.SharingEncryptedAccessToken, true, true, null, null, out text);
			if (!string.IsNullOrEmpty(text))
			{
				this.accessTokenClearText = this.DecryptToUnsecureStringAndThrowPropertyValidationExceptionIfDecryptionFails(text, AggregationSubscriptionMessageSchema.SharingEncryptedAccessToken.Name);
				this.HasAccessToken = true;
				this.EncryptedAccessTokenHash = text.GetHashCode();
			}
			else
			{
				this.HasAccessToken = false;
			}
			string encrypted;
			base.GetStringProperty(message, AggregationSubscriptionMessageSchema.SharingEncryptedAccessTokenSecret, true, true, null, null, out encrypted);
			this.accessTokenSecretClearText = this.DecryptToUnsecureStringAndThrowPropertyValidationExceptionIfDecryptionFails(encrypted, AggregationSubscriptionMessageSchema.SharingEncryptedAccessTokenSecret.Name);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0001F04A File Offset: 0x0001D24A
		protected override void Serialize(AggregationSubscription.SubscriptionSerializer subscriptionSerializer)
		{
			subscriptionSerializer.SerializeConnectSubscription(this);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001F053 File Offset: 0x0001D253
		protected override void Deserialize(AggregationSubscription.SubscriptionDeserializer deserializer)
		{
			deserializer.DeserializeConnectSubscription(this);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001F05C File Offset: 0x0001D25C
		private string EncryptString(string s)
		{
			return AggregationSubscription.ExchangeGroupKeyObject.ClearStringToEncryptedString(s);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0001F06C File Offset: 0x0001D26C
		private string DecryptToUnsecureString(string s)
		{
			string result;
			using (SecureString secureString = AggregationSubscription.ExchangeGroupKeyObject.EncryptedStringToSecureString(s))
			{
				result = secureString.AsUnsecureString();
			}
			return result;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0001F0AC File Offset: 0x0001D2AC
		private string DecryptToUnsecureStringAndThrowPropertyValidationExceptionIfDecryptionFails(string encrypted, string property)
		{
			string result;
			try
			{
				result = this.DecryptToUnsecureString(encrypted);
			}
			catch (FormatException innerException)
			{
				throw new SyncPropertyValidationException(property, "<undisclosed>", innerException);
			}
			catch (CryptographicException innerException2)
			{
				throw new SyncPropertyValidationException(property, "<undisclosed>", innerException2);
			}
			catch (InvalidDataException innerException3)
			{
				throw new SyncPropertyValidationException(property, "<undisclosed>", innerException3);
			}
			catch (Exception ex)
			{
				if (ConnectSubscription.IsDkmException(ex))
				{
					throw new SyncPropertyValidationException(property, "<undisclosed>", ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x04000367 RID: 871
		private const string UndisclosedSentitiveData = "<undisclosed>";

		// Token: 0x04000368 RID: 872
		public static readonly string FacebookProtocolName = "Graph";

		// Token: 0x04000369 RID: 873
		public static readonly int FacebookProtocolVersion = 1;

		// Token: 0x0400036A RID: 874
		public static readonly Guid FacebookProviderGuid = new Guid("b7cfcba5-ec45-4712-bd37-dc0c26eb03c2");

		// Token: 0x0400036B RID: 875
		public static readonly string LinkedInProtocolName = "LinkedIn";

		// Token: 0x0400036C RID: 876
		public static readonly int LinkedInProtocolVersion = 1;

		// Token: 0x0400036D RID: 877
		public static readonly Guid LinkedInProviderGuid = new Guid("1c006afd-7c4e-4ce5-bbd3-b67352fdc685");

		// Token: 0x0400036E RID: 878
		[NonSerialized]
		private string accessTokenClearText;

		// Token: 0x0400036F RID: 879
		[NonSerialized]
		private string accessTokenSecretClearText;
	}
}
