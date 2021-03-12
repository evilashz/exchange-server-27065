using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.TenantMonitoring
{
	// Token: 0x02000CEF RID: 3311
	[Serializable]
	public sealed class Notification : ConfigurableObject
	{
		// Token: 0x06007F55 RID: 32597 RVA: 0x00208488 File Offset: 0x00206688
		public Notification() : this(new NotificationIdentity(), string.Empty, 0, 0, ExDateTime.UtcNow, EventLogEntryType.Information, Notification.EmptyStringArray, false, ExDateTime.UtcNow, Notification.EmptyStringArray, Notification.EmptyStringArray, string.Empty, ObjectState.New)
		{
		}

		// Token: 0x06007F56 RID: 32598 RVA: 0x002084C8 File Offset: 0x002066C8
		internal Notification(NotificationIdentity identity, string eventSource, int eventId, int eventCategoryId, ExDateTime eventTime, EventLogEntryType entryType, IEnumerable<string> insertionStrings, bool emailSent, ExDateTime creationTime, ICollection notificationRecipients, ICollection notificationMessageIds, string periodicKey, ObjectState objectState) : base(new SimpleProviderPropertyBag())
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this[this.propertyBag.ObjectIdentityPropertyDefinition] = identity;
			if (!string.IsNullOrEmpty(eventSource))
			{
				this.EventSource = eventSource;
			}
			this.EventInstanceId = eventId;
			this.EventCategoryId = eventCategoryId;
			this.EventTimeUtc = eventTime;
			this.EntryType = entryType;
			if (insertionStrings != null)
			{
				this.InsertionStrings = new List<string>(insertionStrings);
			}
			if (notificationRecipients != null)
			{
				this.NotificationRecipients = new MultiValuedProperty<string>(notificationRecipients);
			}
			if (notificationMessageIds != null)
			{
				this.NotificationMessageIds = new MultiValuedProperty<string>(notificationMessageIds);
			}
			this.EmailSent = emailSent;
			this.CreationTimeUtc = creationTime;
			this.PeriodicKey = periodicKey;
			this.propertyBag.SetField(SimpleProviderObjectSchema.ObjectState, objectState);
			this.propertyBag.ResetChangeTracking(SimpleProviderObjectSchema.ObjectState);
		}

		// Token: 0x1700278B RID: 10123
		// (get) Token: 0x06007F57 RID: 32599 RVA: 0x002085A1 File Offset: 0x002067A1
		// (set) Token: 0x06007F58 RID: 32600 RVA: 0x002085B3 File Offset: 0x002067B3
		public int EventInstanceId
		{
			get
			{
				return (int)this[Notification.NotificationSchema.EventInstanceId];
			}
			internal set
			{
				this[Notification.NotificationSchema.EventInstanceId] = value;
			}
		}

		// Token: 0x1700278C RID: 10124
		// (get) Token: 0x06007F59 RID: 32601 RVA: 0x002085C6 File Offset: 0x002067C6
		public int EventDisplayId
		{
			get
			{
				return Utils.ExtractCodeFromEventIdentifier(this.EventInstanceId);
			}
		}

		// Token: 0x1700278D RID: 10125
		// (get) Token: 0x06007F5A RID: 32602 RVA: 0x002085D3 File Offset: 0x002067D3
		// (set) Token: 0x06007F5B RID: 32603 RVA: 0x002085E5 File Offset: 0x002067E5
		public string EventSource
		{
			get
			{
				return (string)this[Notification.NotificationSchema.EventSource];
			}
			internal set
			{
				this[Notification.NotificationSchema.EventSource] = value;
			}
		}

		// Token: 0x1700278E RID: 10126
		// (get) Token: 0x06007F5C RID: 32604 RVA: 0x002085F3 File Offset: 0x002067F3
		// (set) Token: 0x06007F5D RID: 32605 RVA: 0x00208605 File Offset: 0x00206805
		public string EventMessage
		{
			get
			{
				return (string)this[Notification.NotificationSchema.EventMessage];
			}
			internal set
			{
				this[Notification.NotificationSchema.EventMessage] = value;
			}
		}

		// Token: 0x1700278F RID: 10127
		// (get) Token: 0x06007F5E RID: 32606 RVA: 0x00208613 File Offset: 0x00206813
		// (set) Token: 0x06007F5F RID: 32607 RVA: 0x00208625 File Offset: 0x00206825
		public int EventCategoryId
		{
			get
			{
				return (int)this[Notification.NotificationSchema.EventCategoryId];
			}
			internal set
			{
				this[Notification.NotificationSchema.EventCategoryId] = value;
			}
		}

		// Token: 0x17002790 RID: 10128
		// (get) Token: 0x06007F60 RID: 32608 RVA: 0x00208638 File Offset: 0x00206838
		// (set) Token: 0x06007F61 RID: 32609 RVA: 0x0020864A File Offset: 0x0020684A
		public string EventCategory
		{
			get
			{
				return (string)this[Notification.NotificationSchema.EventCategory];
			}
			internal set
			{
				this[Notification.NotificationSchema.EventCategory] = value;
			}
		}

		// Token: 0x17002791 RID: 10129
		// (get) Token: 0x06007F62 RID: 32610 RVA: 0x00208658 File Offset: 0x00206858
		// (set) Token: 0x06007F63 RID: 32611 RVA: 0x0020867D File Offset: 0x0020687D
		public ExDateTime EventTimeUtc
		{
			get
			{
				return ((ExDateTime)this[Notification.NotificationSchema.EventTimeUtc]).ToUtc();
			}
			internal set
			{
				this[Notification.NotificationSchema.EventTimeUtc] = value;
			}
		}

		// Token: 0x17002792 RID: 10130
		// (get) Token: 0x06007F64 RID: 32612 RVA: 0x00208690 File Offset: 0x00206890
		public DateTime EventTimeLocal
		{
			get
			{
				return this.EventTimeUtc.UniversalTime.ToLocalTime();
			}
		}

		// Token: 0x17002793 RID: 10131
		// (get) Token: 0x06007F65 RID: 32613 RVA: 0x002086B3 File Offset: 0x002068B3
		// (set) Token: 0x06007F66 RID: 32614 RVA: 0x002086C5 File Offset: 0x002068C5
		public bool EmailSent
		{
			get
			{
				return (bool)this[Notification.NotificationSchema.EmailSent];
			}
			internal set
			{
				this[Notification.NotificationSchema.EmailSent] = value;
			}
		}

		// Token: 0x17002794 RID: 10132
		// (get) Token: 0x06007F67 RID: 32615 RVA: 0x002086D8 File Offset: 0x002068D8
		// (set) Token: 0x06007F68 RID: 32616 RVA: 0x002086EA File Offset: 0x002068EA
		public MultiValuedProperty<string> NotificationRecipients
		{
			get
			{
				return (MultiValuedProperty<string>)this[Notification.NotificationSchema.NotificationRecipients];
			}
			internal set
			{
				this[Notification.NotificationSchema.NotificationRecipients] = value;
			}
		}

		// Token: 0x17002795 RID: 10133
		// (get) Token: 0x06007F69 RID: 32617 RVA: 0x002086F8 File Offset: 0x002068F8
		// (set) Token: 0x06007F6A RID: 32618 RVA: 0x0020870A File Offset: 0x0020690A
		public MultiValuedProperty<string> NotificationMessageIds
		{
			get
			{
				return (MultiValuedProperty<string>)this[Notification.NotificationSchema.NotificationMessageIds];
			}
			internal set
			{
				this[Notification.NotificationSchema.NotificationMessageIds] = value;
			}
		}

		// Token: 0x17002796 RID: 10134
		// (get) Token: 0x06007F6B RID: 32619 RVA: 0x00208718 File Offset: 0x00206918
		// (set) Token: 0x06007F6C RID: 32620 RVA: 0x0020873D File Offset: 0x0020693D
		public ExDateTime CreationTimeUtc
		{
			get
			{
				return ((ExDateTime)this[Notification.NotificationSchema.CreationTimeUtc]).ToUtc();
			}
			internal set
			{
				this[Notification.NotificationSchema.CreationTimeUtc] = value;
			}
		}

		// Token: 0x17002797 RID: 10135
		// (get) Token: 0x06007F6D RID: 32621 RVA: 0x00208750 File Offset: 0x00206950
		public DateTime CreationTimeLocal
		{
			get
			{
				return this.CreationTimeUtc.UniversalTime.ToLocalTime();
			}
		}

		// Token: 0x17002798 RID: 10136
		// (get) Token: 0x06007F6E RID: 32622 RVA: 0x00208773 File Offset: 0x00206973
		// (set) Token: 0x06007F6F RID: 32623 RVA: 0x00208785 File Offset: 0x00206985
		public EventLogEntryType EntryType
		{
			get
			{
				return (EventLogEntryType)this[Notification.NotificationSchema.EntryType];
			}
			set
			{
				this[Notification.NotificationSchema.EntryType] = value;
			}
		}

		// Token: 0x17002799 RID: 10137
		// (get) Token: 0x06007F70 RID: 32624 RVA: 0x00208798 File Offset: 0x00206998
		// (set) Token: 0x06007F71 RID: 32625 RVA: 0x002087AA File Offset: 0x002069AA
		public string EventHelpUrl
		{
			get
			{
				return (string)this[Notification.NotificationSchema.EventHelpUrl];
			}
			internal set
			{
				this[Notification.NotificationSchema.EventHelpUrl] = value;
			}
		}

		// Token: 0x06007F72 RID: 32626 RVA: 0x002087B8 File Offset: 0x002069B8
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			foreach (string value in this.InsertionStrings)
			{
				PropertyConstraintViolationError propertyConstraintViolationError = Notification.InsertionStringLengthConstraint.Validate(value, Notification.NotificationSchema.InsertionStrings, this);
				if (propertyConstraintViolationError != null)
				{
					errors.Add(propertyConstraintViolationError);
				}
			}
		}

		// Token: 0x1700279A RID: 10138
		// (get) Token: 0x06007F73 RID: 32627 RVA: 0x00208824 File Offset: 0x00206A24
		// (set) Token: 0x06007F74 RID: 32628 RVA: 0x0020883A File Offset: 0x00206A3A
		internal IList<string> InsertionStrings
		{
			get
			{
				return this.insertionStrings ?? ((IList<string>)Notification.EmptyStringArray);
			}
			set
			{
				this.insertionStrings = value;
			}
		}

		// Token: 0x1700279B RID: 10139
		// (get) Token: 0x06007F75 RID: 32629 RVA: 0x00208843 File Offset: 0x00206A43
		// (set) Token: 0x06007F76 RID: 32630 RVA: 0x00208855 File Offset: 0x00206A55
		internal string PeriodicKey
		{
			get
			{
				return (string)this[Notification.NotificationSchema.PeriodicKey];
			}
			set
			{
				this[Notification.NotificationSchema.PeriodicKey] = value;
			}
		}

		// Token: 0x06007F77 RID: 32631 RVA: 0x00208863 File Offset: 0x00206A63
		internal void CopyChangesFrom(IConfigurable source)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700279C RID: 10140
		// (get) Token: 0x06007F78 RID: 32632 RVA: 0x0020886A File Offset: 0x00206A6A
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return Notification.Schema;
			}
		}

		// Token: 0x06007F79 RID: 32633 RVA: 0x00208874 File Offset: 0x00206A74
		internal long ComputeHashCodeForDuplicateDetection()
		{
			int num = this.EventSource.GetHashCodeCaseInsensitive() ^ this.PeriodicKey.GetHashCode();
			return (long)this.EventInstanceId << 32 | (long)((ulong)num);
		}

		// Token: 0x04003E66 RID: 15974
		internal const int MaxEventSourceLength = 256;

		// Token: 0x04003E67 RID: 15975
		internal const int MaxInsertionStringsCount = 100;

		// Token: 0x04003E68 RID: 15976
		internal const int MaxInsertionStringLength = 4096;

		// Token: 0x04003E69 RID: 15977
		internal const int MaxNotificationRecipientsCount = 64;

		// Token: 0x04003E6A RID: 15978
		private static readonly ObjectSchema Schema = ObjectSchema.GetInstance<Notification.NotificationSchema>();

		// Token: 0x04003E6B RID: 15979
		private static readonly string[] EmptyStringArray = new string[0];

		// Token: 0x04003E6C RID: 15980
		private static readonly StringLengthConstraint InsertionStringLengthConstraint = new StringLengthConstraint(0, 4096);

		// Token: 0x04003E6D RID: 15981
		[NonSerialized]
		private IList<string> insertionStrings;

		// Token: 0x02000CF0 RID: 3312
		private sealed class NotificationSchema : SimpleProviderObjectSchema
		{
			// Token: 0x04003E6E RID: 15982
			internal static readonly SimpleProviderPropertyDefinition EventInstanceId = new SimpleProviderPropertyDefinition("EventInstanceId", ExchangeObjectVersion.Exchange2007, typeof(int), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04003E6F RID: 15983
			internal static readonly SimpleProviderPropertyDefinition EventSource = new SimpleProviderPropertyDefinition("EventSource", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.Mandatory, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
			{
				new StringLengthConstraint(1, 256)
			});

			// Token: 0x04003E70 RID: 15984
			internal static readonly SimpleProviderPropertyDefinition EventMessage = new SimpleProviderPropertyDefinition("EventMessage", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04003E71 RID: 15985
			internal static readonly SimpleProviderPropertyDefinition EventCategoryId = new SimpleProviderPropertyDefinition("EventCategoryId", ExchangeObjectVersion.Exchange2007, typeof(int), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04003E72 RID: 15986
			internal static readonly SimpleProviderPropertyDefinition EventCategory = new SimpleProviderPropertyDefinition("EventCategory", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04003E73 RID: 15987
			internal static readonly SimpleProviderPropertyDefinition EventTimeUtc = new SimpleProviderPropertyDefinition("EventTimeUtc", ExchangeObjectVersion.Exchange2007, typeof(ExDateTime), PropertyDefinitionFlags.None, ExDateTime.UtcNow, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04003E74 RID: 15988
			internal static readonly SimpleProviderPropertyDefinition InsertionStrings = new SimpleProviderPropertyDefinition("InsertionStrings", ExchangeObjectVersion.Exchange2007, typeof(IList<string>), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04003E75 RID: 15989
			internal static readonly SimpleProviderPropertyDefinition EntryType = new SimpleProviderPropertyDefinition("EntryType", ExchangeObjectVersion.Exchange2007, typeof(EventLogEntryType), PropertyDefinitionFlags.None, EventLogEntryType.Information, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04003E76 RID: 15990
			internal static readonly SimpleProviderPropertyDefinition EmailSent = new SimpleProviderPropertyDefinition("EmailSent", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04003E77 RID: 15991
			internal static readonly SimpleProviderPropertyDefinition CreationTimeUtc = new SimpleProviderPropertyDefinition("CreationTimeUtc", ExchangeObjectVersion.Exchange2007, typeof(ExDateTime), PropertyDefinitionFlags.Mandatory, ExDateTime.UtcNow, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04003E78 RID: 15992
			internal static readonly SimpleProviderPropertyDefinition NotificationRecipients = new SimpleProviderPropertyDefinition("NotificationRecipients", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04003E79 RID: 15993
			internal static readonly SimpleProviderPropertyDefinition EventHelpUrl = new SimpleProviderPropertyDefinition("EventHelpUrl", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04003E7A RID: 15994
			internal static readonly SimpleProviderPropertyDefinition NotificationMessageIds = new SimpleProviderPropertyDefinition("NotificationMessageIds", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04003E7B RID: 15995
			internal static readonly SimpleProviderPropertyDefinition PeriodicKey = new SimpleProviderPropertyDefinition("PeriodicKey", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}
	}
}
