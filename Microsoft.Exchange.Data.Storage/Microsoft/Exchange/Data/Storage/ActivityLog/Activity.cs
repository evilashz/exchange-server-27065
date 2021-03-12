using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;

namespace Microsoft.Exchange.Data.Storage.ActivityLog
{
	// Token: 0x0200033A RID: 826
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Activity
	{
		// Token: 0x06002499 RID: 9369 RVA: 0x00093E5A File Offset: 0x0009205A
		internal Activity(MemoryPropertyBag propertyBag)
		{
			Util.ThrowOnNullArgument(propertyBag, "propertyBag");
			this.propertyBag = propertyBag;
			this.propertyBag.SetAllPropertiesLoaded();
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x00093E80 File Offset: 0x00092080
		public Activity(ActivityId id, ClientId clientId, ExDateTime timeStamp, Guid clientSessionId, string clientVersion, long sequenceNumber, IMailboxSession mailboxSession, StoreObjectId itemId = null, StoreObjectId previousItemId = null, IDictionary<string, string> customProperties = null) : this(new MemoryPropertyBag())
		{
			ArgumentValidator.ThrowIfNull("clientId", clientId);
			ArgumentValidator.ThrowIfNullOrEmpty("clientVersion", clientVersion);
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			EnumValidator<ActivityId>.ThrowIfInvalid(id);
			if (id == ActivityId.Min)
			{
				throw new ArgumentException("The value supplied for ActivityId is invalid (ActivityId.Min)");
			}
			if (clientId == ClientId.Min)
			{
				throw new ArgumentException("The value is not a valid ClientId (ClientId.Min)");
			}
			if (!timeStamp.HasTimeZone)
			{
				throw new ArgumentException("Timestamp has unspecified timezone: " + Activity.BuildDiagnosticString(id, clientId, timeStamp, clientSessionId, clientVersion, sequenceNumber, mailboxSession, itemId, previousItemId, customProperties));
			}
			this.Id = id;
			this.ClientId = clientId;
			this.TimeStamp = timeStamp.ToUtc();
			this.ClientSessionId = clientSessionId;
			this.SequenceNumber = sequenceNumber;
			this.ClientVersion = clientVersion;
			this.MailboxGuid = mailboxSession.MailboxGuid;
			IUserPrincipal userPrincipal = mailboxSession.MailboxOwner as IUserPrincipal;
			this.NetId = ((userPrincipal != null) ? userPrincipal.NetId : null);
			if (mailboxSession.Capabilities != null && mailboxSession.Capabilities.CanHaveCulture && mailboxSession.PreferedCulture != null)
			{
				this.LocaleName = mailboxSession.PreferedCulture.Name;
				this.LocaleId = Activity.GetLcidFromMailboxSession(mailboxSession);
			}
			IExchangePrincipal mailboxOwner = mailboxSession.MailboxOwner;
			if (mailboxOwner != null)
			{
				if (mailboxOwner.MailboxInfo.OrganizationId != null && mailboxOwner.MailboxInfo.OrganizationId.OrganizationalUnit != null)
				{
					this.TenantName = mailboxOwner.MailboxInfo.OrganizationId.OrganizationalUnit.Name;
				}
				this.mailboxType = new long?((long)mailboxOwner.RecipientTypeDetails);
			}
			if (itemId != null)
			{
				this.ItemId = itemId;
			}
			if (previousItemId != null)
			{
				this.PreviousItemId = previousItemId;
			}
			this.CustomPropertiesDictionary = (customProperties ?? new Dictionary<string, string>());
			if (clientId.Equals(ClientId.Other, true))
			{
				this.customPropertiesDictionary["ClientString"] = mailboxSession.ClientInfoString;
				IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
				if (currentActivityScope != null && !string.IsNullOrEmpty(currentActivityScope.ClientInfo))
				{
					this.customPropertiesDictionary["ActivityScopeClientInfo"] = currentActivityScope.ClientInfo;
				}
			}
			this.activityCreationTime = ExDateTime.UtcNow;
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x0600249B RID: 9371 RVA: 0x00094095 File Offset: 0x00092295
		// (set) Token: 0x0600249C RID: 9372 RVA: 0x000940A8 File Offset: 0x000922A8
		public ActivityId Id
		{
			get
			{
				return this.propertyBag.GetValueOrDefault<ActivityId>(ActivitySchema.ActivityId, ActivityId.Min);
			}
			private set
			{
				this.propertyBag.SetProperty(ActivitySchema.ActivityId, value);
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x0600249D RID: 9373 RVA: 0x000940C0 File Offset: 0x000922C0
		// (set) Token: 0x0600249E RID: 9374 RVA: 0x00094108 File Offset: 0x00092308
		public ClientId ClientId
		{
			get
			{
				object obj = this.propertyBag.TryGetProperty(ActivitySchema.ClientId);
				if (PropertyError.IsPropertyError(obj))
				{
					return ClientId.Min;
				}
				ClientId clientId = ClientId.FromInt((int)obj);
				if (clientId == null)
				{
					return ClientId.Min;
				}
				return clientId;
			}
			private set
			{
				this.propertyBag.SetProperty(ActivitySchema.ClientId, value.ToInt());
			}
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x0600249F RID: 9375 RVA: 0x00094125 File Offset: 0x00092325
		// (set) Token: 0x060024A0 RID: 9376 RVA: 0x0009413C File Offset: 0x0009233C
		public ExDateTime TimeStamp
		{
			get
			{
				return this.propertyBag.GetValueOrDefault<ExDateTime>(ActivitySchema.TimeStamp, ExDateTime.MinValue);
			}
			private set
			{
				this.propertyBag.SetProperty(ActivitySchema.TimeStamp, value);
			}
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x060024A1 RID: 9377 RVA: 0x00094154 File Offset: 0x00092354
		// (set) Token: 0x060024A2 RID: 9378 RVA: 0x00094167 File Offset: 0x00092367
		public StoreObjectId ItemId
		{
			get
			{
				return this.propertyBag.GetValueOrDefault<StoreObjectId>(ActivitySchema.ItemId, null);
			}
			internal set
			{
				this.propertyBag.SetProperty(ActivitySchema.ItemId, value);
			}
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x060024A3 RID: 9379 RVA: 0x0009417C File Offset: 0x0009237C
		// (set) Token: 0x060024A4 RID: 9380 RVA: 0x000941A6 File Offset: 0x000923A6
		public StoreObjectId PreviousItemId
		{
			get
			{
				byte[] valueOrDefault = this.propertyBag.GetValueOrDefault<byte[]>(ActivitySchema.PreviousItemId, null);
				if (valueOrDefault != null)
				{
					return StoreObjectId.Deserialize(valueOrDefault);
				}
				return null;
			}
			private set
			{
				ArgumentValidator.ThrowIfNull("PreviousItemId", value);
				this.propertyBag.SetProperty(ActivitySchema.PreviousItemId, value.GetBytes());
			}
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x060024A5 RID: 9381 RVA: 0x000941C9 File Offset: 0x000923C9
		// (set) Token: 0x060024A6 RID: 9382 RVA: 0x000941E0 File Offset: 0x000923E0
		public int LocaleId
		{
			get
			{
				return this.propertyBag.GetValueOrDefault<int>(ActivitySchema.LocaleId, int.MinValue);
			}
			private set
			{
				this.propertyBag.SetProperty(ActivitySchema.LocaleId, value);
			}
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x060024A7 RID: 9383 RVA: 0x000941F8 File Offset: 0x000923F8
		// (set) Token: 0x060024A8 RID: 9384 RVA: 0x0009420F File Offset: 0x0009240F
		public Guid ClientSessionId
		{
			get
			{
				return this.propertyBag.GetValueOrDefault<Guid>(ActivitySchema.SessionId, Guid.Empty);
			}
			private set
			{
				this.propertyBag.SetProperty(ActivitySchema.SessionId, value);
			}
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x060024A9 RID: 9385 RVA: 0x00094227 File Offset: 0x00092427
		// (set) Token: 0x060024AA RID: 9386 RVA: 0x0009422F File Offset: 0x0009242F
		public string ClientVersion { get; private set; }

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x060024AB RID: 9387 RVA: 0x00094238 File Offset: 0x00092438
		// (set) Token: 0x060024AC RID: 9388 RVA: 0x00094240 File Offset: 0x00092440
		public string LocaleName { get; private set; }

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x060024AD RID: 9389 RVA: 0x00094249 File Offset: 0x00092449
		// (set) Token: 0x060024AE RID: 9390 RVA: 0x00094251 File Offset: 0x00092451
		public long SequenceNumber { get; private set; }

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x060024AF RID: 9391 RVA: 0x0009425A File Offset: 0x0009245A
		// (set) Token: 0x060024B0 RID: 9392 RVA: 0x00094262 File Offset: 0x00092462
		public Guid MailboxGuid { get; private set; }

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x060024B1 RID: 9393 RVA: 0x0009426B File Offset: 0x0009246B
		// (set) Token: 0x060024B2 RID: 9394 RVA: 0x00094273 File Offset: 0x00092473
		public string TenantName { get; private set; }

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x060024B3 RID: 9395 RVA: 0x0009427C File Offset: 0x0009247C
		// (set) Token: 0x060024B4 RID: 9396 RVA: 0x00094284 File Offset: 0x00092484
		public NetID NetId { get; private set; }

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x060024B5 RID: 9397 RVA: 0x0009428D File Offset: 0x0009248D
		public ExDateTime ActivityCreationTime
		{
			get
			{
				return this.activityCreationTime;
			}
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x060024B6 RID: 9398 RVA: 0x00094295 File Offset: 0x00092495
		public long? MailboxType
		{
			get
			{
				return this.mailboxType;
			}
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x060024B7 RID: 9399 RVA: 0x0009429D File Offset: 0x0009249D
		public string CustomPropertiesString
		{
			get
			{
				if (this.customPropertiesString == null)
				{
					this.customPropertiesString = Activity.SerializeDictionaryAsString(this.CustomPropertiesDictionary, this.isCustomPropertiesDictionaryTruncated);
				}
				return this.customPropertiesString;
			}
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x000942C4 File Offset: 0x000924C4
		public override string ToString()
		{
			return Activity.BuildDiagnosticString(this.Id, this.ClientId, this.TimeStamp, this.ClientSessionId, this.ClientVersion, this.SequenceNumber, null, this.ItemId, this.PreviousItemId, this.CustomPropertiesDictionary);
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x060024B9 RID: 9401 RVA: 0x0009430D File Offset: 0x0009250D
		// (set) Token: 0x060024BA RID: 9402 RVA: 0x0009432F File Offset: 0x0009252F
		private IDictionary<string, string> CustomPropertiesDictionary
		{
			get
			{
				if (this.customPropertiesDictionary == null)
				{
					this.customPropertiesDictionary = this.DeserializeCustomPropertiesDictionary(out this.isCustomPropertiesDictionaryTruncated);
				}
				return this.customPropertiesDictionary;
			}
			set
			{
				this.SerializeCustomPropertiesDictionary(value, out this.isCustomPropertiesDictionaryTruncated);
				this.customPropertiesDictionary = value;
			}
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x00094345 File Offset: 0x00092545
		public bool TryGetCustomProperty(string propertyName, out string value)
		{
			return this.CustomPropertiesDictionary.TryGetValue(propertyName, out value);
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x00094354 File Offset: 0x00092554
		internal bool TryGetSchemaProperty(PropertyDefinition propertyDefinition, out object value)
		{
			value = this.propertyBag.TryGetProperty(propertyDefinition);
			return !(value is PropertyError);
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x0009438C File Offset: 0x0009258C
		internal static IMessage CreateMessageAdapter(Action<Activity> saveAction)
		{
			Util.ThrowOnNullArgument(saveAction, "saveAction");
			return new ActivityMessageAdapter(delegate(MemoryPropertyBag propertyBag)
			{
				saveAction(new Activity(propertyBag));
			});
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x000943C7 File Offset: 0x000925C7
		internal IMessage CreateMessageAdapter()
		{
			return new ActivityMessageAdapter(this.propertyBag);
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x000943D4 File Offset: 0x000925D4
		internal static string SerializeDictionaryAsString(IDictionary<string, string> dictionary)
		{
			return Activity.SerializeDictionaryAsString(dictionary, false);
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x000943E0 File Offset: 0x000925E0
		internal static string SerializeDictionaryAsString(IDictionary<string, string> dictionary, bool isDictionaryTruncated)
		{
			if (dictionary == null)
			{
				return string.Empty;
			}
			bool flag = true;
			bool flag2 = false;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, string> keyValuePair in dictionary)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder2.Append("&");
				}
				stringBuilder2.Append(Activity.EscapeString(keyValuePair.Key));
				stringBuilder2.Append("=");
				stringBuilder2.Append(Activity.EscapeString(keyValuePair.Value));
				if (stringBuilder.Length + stringBuilder2.Length > 4096)
				{
					flag2 = true;
					break;
				}
				stringBuilder.Append(stringBuilder2);
			}
			if (flag2 || isDictionaryTruncated)
			{
				stringBuilder.Append("&");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x000944C8 File Offset: 0x000926C8
		internal static IDictionary<string, string> DeserializeDictionaryFromString(string serializedString)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (!string.IsNullOrEmpty(serializedString))
			{
				string[] array = serializedString.Split(new char[]
				{
					'&'
				}, StringSplitOptions.RemoveEmptyEntries);
				foreach (string text in array)
				{
					string[] array3 = text.Split(new char[]
					{
						'='
					});
					if (array3.Length == 2)
					{
						dictionary.Add(Activity.UnescapeString(array3[0]), Activity.UnescapeString(array3[1]));
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x0009454C File Offset: 0x0009274C
		internal static string EscapeString(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in str)
			{
				char c2 = c;
				switch (c2)
				{
				case '%':
					stringBuilder.Append("%25");
					break;
				case '&':
					stringBuilder.Append("%26");
					break;
				default:
					if (c2 == '=')
					{
						stringBuilder.Append("%3d");
					}
					else
					{
						stringBuilder.Append(c);
					}
					break;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x000945DB File Offset: 0x000927DB
		internal static string UnescapeString(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return string.Empty;
			}
			return str.Replace("%3d", "=").Replace("%26", "&").Replace("%25", "%");
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x0009461C File Offset: 0x0009281C
		private void SerializeCustomPropertiesDictionary(IDictionary<string, string> data, out bool isTruncatedResult)
		{
			if (data == null || data.Count == 0)
			{
				isTruncatedResult = false;
				return;
			}
			AbstractCustomPropertySerializer serializer = CustomPropertySerializerFactory.GetSerializer();
			if (serializer == null)
			{
				isTruncatedResult = false;
				return;
			}
			byte[] array = serializer.Serialize(data, out isTruncatedResult);
			if (array != null)
			{
				this.propertyBag.SetProperty(ActivitySchema.CustomProperties, array);
			}
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x00094664 File Offset: 0x00092864
		private Dictionary<string, string> DeserializeCustomPropertiesDictionary(out bool isTruncatedResult)
		{
			byte[] valueOrDefault = this.propertyBag.GetValueOrDefault<byte[]>(ActivitySchema.CustomProperties, null);
			if (valueOrDefault == null || valueOrDefault.Length == 0)
			{
				isTruncatedResult = false;
				return new Dictionary<string, string>();
			}
			AbstractCustomPropertySerializer deserializer = CustomPropertySerializerFactory.GetDeserializer(valueOrDefault);
			if (deserializer == null)
			{
				isTruncatedResult = false;
				return new Dictionary<string, string>();
			}
			return deserializer.Deserialize(valueOrDefault, out isTruncatedResult);
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x000946B0 File Offset: 0x000928B0
		private static string BuildDiagnosticString(ActivityId id, ClientId clientId, ExDateTime timeStamp, Guid clientSessionId, string clientVersion, long sequenceNumber, IMailboxSession mailboxSession, StoreObjectId itemId = null, StoreObjectId previousItemId = null, IDictionary<string, string> customProperties = null)
		{
			string text = string.Empty;
			if (mailboxSession != null && mailboxSession.MailboxOwner != null)
			{
				text = mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString();
			}
			return string.Format("id:{0}, clientId:{1}, timeStamp:{2}, clientSessionId:{3}, clientVersion:{4}, sequenceNumber:{5}, owner:{6}, itemId:{7}, previousItemId:{8}, customProperties:{9}", new object[]
			{
				id,
				(clientId == null) ? string.Empty : clientId.ToString(),
				timeStamp.ToISOString(),
				clientSessionId,
				clientVersion,
				sequenceNumber,
				text,
				(itemId == null) ? string.Empty : itemId.ToBase64ProviderLevelItemId(),
				(previousItemId == null) ? string.Empty : previousItemId.ToBase64ProviderLevelItemId(),
				(customProperties == null) ? string.Empty : Activity.SerializeDictionaryAsString(customProperties)
			});
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x0009478B File Offset: 0x0009298B
		private static int GetLcidFromMailboxSession(IMailboxSession mailboxSession)
		{
			if (mailboxSession != null && mailboxSession.PreferedCulture != null)
			{
				return mailboxSession.PreferedCulture.LCID;
			}
			return 0;
		}

		// Token: 0x040015E2 RID: 5602
		internal const int MaxCustomPropertiesCharacterCount = 4096;

		// Token: 0x040015E3 RID: 5603
		private readonly MemoryPropertyBag propertyBag;

		// Token: 0x040015E4 RID: 5604
		private readonly ExDateTime activityCreationTime;

		// Token: 0x040015E5 RID: 5605
		private readonly long? mailboxType;

		// Token: 0x040015E6 RID: 5606
		private IDictionary<string, string> customPropertiesDictionary;

		// Token: 0x040015E7 RID: 5607
		private string customPropertiesString;

		// Token: 0x040015E8 RID: 5608
		private bool isCustomPropertiesDictionaryTruncated;
	}
}
