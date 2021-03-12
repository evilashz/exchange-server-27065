using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004A9 RID: 1193
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class ContactInfoForLinking : IContactLinkingMatchProperties
	{
		// Token: 0x060034E0 RID: 13536 RVA: 0x000D5D4C File Offset: 0x000D3F4C
		protected ContactInfoForLinking(bool linked, PersonId personId, HashSet<PersonId> linkRejectHistory, Guid? galLinkID, byte[] addressBookEntryId, GALLinkState galLinkState, string[] smtpAddressCache, bool userApprovedLink)
		{
			Util.ThrowOnNullArgument(linkRejectHistory, "linkRejectHistory");
			Util.ThrowOnNullArgument(smtpAddressCache, "smtpAddressCache");
			if (personId == null)
			{
				this.personId = PersonId.CreateNew();
				this.isDirty = true;
			}
			else
			{
				this.personId = personId;
			}
			this.linked = linked;
			this.linkRejectHistory = linkRejectHistory;
			this.galLinkID = galLinkID;
			this.addressBookEntryId = addressBookEntryId;
			this.galLinkState = galLinkState;
			this.smtpAddressCache = smtpAddressCache;
			this.userApprovedLink = userApprovedLink;
		}

		// Token: 0x060034E1 RID: 13537 RVA: 0x000D5DCC File Offset: 0x000D3FCC
		protected ContactInfoForLinking(PropertyBagAdaptor propertyBag) : this(propertyBag.GetValueOrDefault<bool>(ContactSchema.Linked, false), propertyBag.GetValueOrDefault<PersonId>(ContactSchema.PersonId, null), ContactInfoForLinking.GetPropertyAsHashSet<PersonId>(propertyBag, ContactSchema.LinkRejectHistory, new HashSet<PersonId>()), propertyBag.GetValueOrDefault<Guid?>(ContactSchema.GALLinkID, null), propertyBag.GetValueOrDefault<byte[]>(ContactSchema.AddressBookEntryId, null), propertyBag.GetValueOrDefault<GALLinkState>(ContactSchema.GALLinkState, GALLinkState.NotLinked), propertyBag.GetValueOrDefault<string[]>(ContactSchema.SmtpAddressCache, Array<string>.Empty), propertyBag.GetValueOrDefault<bool>(ContactSchema.UserApprovedLink, false))
		{
			this.ItemId = propertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id, null);
			this.EmailAddresses = ContactInfoForLinking.GetEmailAddresses(propertyBag);
			this.GivenName = propertyBag.GetValueOrDefault<string>(ContactSchema.GivenName, string.Empty);
			this.Surname = propertyBag.GetValueOrDefault<string>(ContactSchema.Surname, string.Empty);
			this.DisplayName = propertyBag.GetValueOrDefault<string>(StoreObjectSchema.DisplayName, string.Empty);
			this.PartnerNetworkId = propertyBag.GetValueOrDefault<string>(ContactSchema.PartnerNetworkId, string.Empty);
			this.PartnerNetworkUserId = propertyBag.GetValueOrDefault<string>(ContactSchema.PartnerNetworkUserId, string.Empty);
			this.IMAddress = ContactInfoForLinking.CanonicalizeEmailAddress(propertyBag.GetValueOrDefault<string>(ContactSchema.IMAddress, string.Empty));
			this.IsDL = ObjectClass.IsOfClass(propertyBag.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, string.Empty), "IPM.DistList");
		}

		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x060034E2 RID: 13538 RVA: 0x000D5F16 File Offset: 0x000D4116
		public bool IsDirty
		{
			get
			{
				return this.isDirty;
			}
		}

		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x060034E3 RID: 13539 RVA: 0x000D5F1E File Offset: 0x000D411E
		public bool IsNew
		{
			get
			{
				return this.ItemId == null;
			}
		}

		// Token: 0x17001081 RID: 4225
		// (get) Token: 0x060034E4 RID: 13540 RVA: 0x000D5F29 File Offset: 0x000D4129
		// (set) Token: 0x060034E5 RID: 13541 RVA: 0x000D5F31 File Offset: 0x000D4131
		public HashSet<PersonId> LinkRejectHistory
		{
			get
			{
				return this.linkRejectHistory;
			}
			set
			{
				if (!this.linkRejectHistory.SetEquals(value))
				{
					this.linkRejectHistory = value;
					this.isDirty = true;
				}
			}
		}

		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x060034E6 RID: 13542 RVA: 0x000D5F4F File Offset: 0x000D414F
		// (set) Token: 0x060034E7 RID: 13543 RVA: 0x000D5F57 File Offset: 0x000D4157
		public PersonId PersonId
		{
			get
			{
				return this.personId;
			}
			set
			{
				if (!this.PersonId.Equals(value))
				{
					this.personId = value;
					this.isDirty = true;
				}
			}
		}

		// Token: 0x17001083 RID: 4227
		// (get) Token: 0x060034E8 RID: 13544 RVA: 0x000D5F75 File Offset: 0x000D4175
		// (set) Token: 0x060034E9 RID: 13545 RVA: 0x000D5F7D File Offset: 0x000D417D
		public bool Linked
		{
			get
			{
				return this.linked;
			}
			set
			{
				if (this.linked != value)
				{
					this.linked = value;
					this.isDirty = true;
				}
			}
		}

		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x060034EA RID: 13546 RVA: 0x000D5F96 File Offset: 0x000D4196
		// (set) Token: 0x060034EB RID: 13547 RVA: 0x000D5F9E File Offset: 0x000D419E
		public Guid? GALLinkID
		{
			get
			{
				return this.galLinkID;
			}
			private set
			{
				if (!this.galLinkID.Equals(value))
				{
					this.galLinkID = value;
					this.isDirty = true;
				}
			}
		}

		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x060034EC RID: 13548 RVA: 0x000D5FC7 File Offset: 0x000D41C7
		// (set) Token: 0x060034ED RID: 13549 RVA: 0x000D5FCF File Offset: 0x000D41CF
		public byte[] AddressBookEntryId
		{
			get
			{
				return this.addressBookEntryId;
			}
			private set
			{
				if (!ArrayComparer<byte>.Comparer.Equals(this.addressBookEntryId, value))
				{
					this.addressBookEntryId = value;
					this.isDirty = true;
				}
			}
		}

		// Token: 0x17001086 RID: 4230
		// (get) Token: 0x060034EE RID: 13550 RVA: 0x000D5FF2 File Offset: 0x000D41F2
		// (set) Token: 0x060034EF RID: 13551 RVA: 0x000D5FFA File Offset: 0x000D41FA
		public GALLinkState GALLinkState
		{
			get
			{
				return this.galLinkState;
			}
			private set
			{
				if (this.galLinkState != value)
				{
					this.galLinkState = value;
					this.isDirty = true;
				}
			}
		}

		// Token: 0x17001087 RID: 4231
		// (get) Token: 0x060034F0 RID: 13552 RVA: 0x000D6013 File Offset: 0x000D4213
		// (set) Token: 0x060034F1 RID: 13553 RVA: 0x000D601B File Offset: 0x000D421B
		public bool UserApprovedLink
		{
			get
			{
				return this.userApprovedLink;
			}
			set
			{
				if (this.userApprovedLink != value)
				{
					this.userApprovedLink = value;
					this.isDirty = true;
				}
			}
		}

		// Token: 0x17001088 RID: 4232
		// (get) Token: 0x060034F2 RID: 13554 RVA: 0x000D6034 File Offset: 0x000D4234
		// (set) Token: 0x060034F3 RID: 13555 RVA: 0x000D603C File Offset: 0x000D423C
		public string[] SmtpAddressCache
		{
			get
			{
				return this.smtpAddressCache;
			}
			private set
			{
				if (!ContactInfoForLinking.SmtpAddressCacheComparer.Equals(this.smtpAddressCache, value))
				{
					this.smtpAddressCache = value;
					this.isDirty = true;
				}
			}
		}

		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x060034F4 RID: 13556 RVA: 0x000D605A File Offset: 0x000D425A
		// (set) Token: 0x060034F5 RID: 13557 RVA: 0x000D6062 File Offset: 0x000D4262
		public VersionedId ItemId { get; protected set; }

		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x060034F6 RID: 13558 RVA: 0x000D606B File Offset: 0x000D426B
		// (set) Token: 0x060034F7 RID: 13559 RVA: 0x000D6073 File Offset: 0x000D4273
		public HashSet<string> EmailAddresses { get; protected set; }

		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x060034F8 RID: 13560 RVA: 0x000D607C File Offset: 0x000D427C
		// (set) Token: 0x060034F9 RID: 13561 RVA: 0x000D6084 File Offset: 0x000D4284
		public string GivenName { get; protected set; }

		// Token: 0x1700108C RID: 4236
		// (get) Token: 0x060034FA RID: 13562 RVA: 0x000D608D File Offset: 0x000D428D
		// (set) Token: 0x060034FB RID: 13563 RVA: 0x000D6095 File Offset: 0x000D4295
		public string Surname { get; protected set; }

		// Token: 0x1700108D RID: 4237
		// (get) Token: 0x060034FC RID: 13564 RVA: 0x000D609E File Offset: 0x000D429E
		// (set) Token: 0x060034FD RID: 13565 RVA: 0x000D60A6 File Offset: 0x000D42A6
		public string DisplayName { get; protected set; }

		// Token: 0x1700108E RID: 4238
		// (get) Token: 0x060034FE RID: 13566 RVA: 0x000D60AF File Offset: 0x000D42AF
		// (set) Token: 0x060034FF RID: 13567 RVA: 0x000D60B7 File Offset: 0x000D42B7
		public string PartnerNetworkId { get; protected set; }

		// Token: 0x1700108F RID: 4239
		// (get) Token: 0x06003500 RID: 13568 RVA: 0x000D60C0 File Offset: 0x000D42C0
		// (set) Token: 0x06003501 RID: 13569 RVA: 0x000D60C8 File Offset: 0x000D42C8
		public string PartnerNetworkUserId { get; protected set; }

		// Token: 0x17001090 RID: 4240
		// (get) Token: 0x06003502 RID: 13570 RVA: 0x000D60D1 File Offset: 0x000D42D1
		// (set) Token: 0x06003503 RID: 13571 RVA: 0x000D60D9 File Offset: 0x000D42D9
		public string IMAddress { get; protected set; }

		// Token: 0x17001091 RID: 4241
		// (get) Token: 0x06003504 RID: 13572 RVA: 0x000D60E2 File Offset: 0x000D42E2
		// (set) Token: 0x06003505 RID: 13573 RVA: 0x000D60EA File Offset: 0x000D42EA
		public bool IsDL { get; protected set; }

		// Token: 0x06003506 RID: 13574 RVA: 0x000D60F3 File Offset: 0x000D42F3
		public void UpdateGALLink(GALLinkState galLinkState, Guid? galLinkId, byte[] addressBookEntryId, string[] smtpAddressCache)
		{
			this.GALLinkState = galLinkState;
			this.GALLinkID = galLinkId;
			this.AddressBookEntryId = addressBookEntryId;
			this.SmtpAddressCache = smtpAddressCache;
		}

		// Token: 0x06003507 RID: 13575 RVA: 0x000D6112 File Offset: 0x000D4312
		public void UpdateGALLinkFrom(ContactInfoForLinking otherContact)
		{
			ArgumentValidator.ThrowIfNull("otherContact", otherContact);
			this.UpdateGALLink(otherContact.GALLinkState, otherContact.GALLinkID, otherContact.AddressBookEntryId, otherContact.SmtpAddressCache);
		}

		// Token: 0x06003508 RID: 13576 RVA: 0x000D613D File Offset: 0x000D433D
		public void SetGALLink(ContactInfoForLinkingFromDirectory directoryContact)
		{
			ArgumentValidator.ThrowIfNull("directoryContact", directoryContact);
			this.UpdateGALLink(GALLinkState.Linked, new Guid?(directoryContact.GALLinkID), directoryContact.AddressBookEntryId, directoryContact.SmtpAddressCache);
			this.Linked = true;
		}

		// Token: 0x06003509 RID: 13577 RVA: 0x000D6170 File Offset: 0x000D4370
		public void ClearGALLink(GALLinkState galLinkState)
		{
			this.UpdateGALLink(galLinkState, null, null, Array<string>.Empty);
		}

		// Token: 0x0600350A RID: 13578 RVA: 0x000D6194 File Offset: 0x000D4394
		public void Commit(IExtensibleLogger logger, IContactLinkingPerformanceTracker performanceTracker)
		{
			if (this.isDirty)
			{
				this.UpdateContact(logger, performanceTracker);
				this.isDirty = false;
				logger.LogEvent(new SchemaBasedLogEvent<ContactLinkingLogSchema.ContactUpdate>
				{
					{
						ContactLinkingLogSchema.ContactUpdate.ItemId,
						this.ItemId
					},
					{
						ContactLinkingLogSchema.ContactUpdate.PersonId,
						this.PersonId
					},
					{
						ContactLinkingLogSchema.ContactUpdate.Linked,
						this.linked
					},
					{
						ContactLinkingLogSchema.ContactUpdate.LinkRejectHistory,
						this.linkRejectHistory
					},
					{
						ContactLinkingLogSchema.ContactUpdate.GALLinkState,
						this.galLinkState
					},
					{
						ContactLinkingLogSchema.ContactUpdate.GALLinkID,
						this.galLinkID
					},
					{
						ContactLinkingLogSchema.ContactUpdate.AddressBookEntryId,
						this.addressBookEntryId
					},
					{
						ContactLinkingLogSchema.ContactUpdate.SmtpAddressCache,
						this.smtpAddressCache
					},
					{
						ContactLinkingLogSchema.ContactUpdate.UserApprovedLink,
						this.userApprovedLink
					}
				});
			}
		}

		// Token: 0x0600350B RID: 13579 RVA: 0x000D6254 File Offset: 0x000D4454
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(300);
			ContactInfoForLinking.AddToString(stringBuilder, "Type", base.GetType().Name);
			ContactInfoForLinking.AddToString(stringBuilder, "DisplayName", this.DisplayName);
			ContactInfoForLinking.AddToString(stringBuilder, "GivenName", this.GivenName);
			ContactInfoForLinking.AddToString(stringBuilder, "Surname", this.Surname);
			ContactInfoForLinking.AddToString(stringBuilder, "EmailAddresses", this.EmailAddresses);
			ContactInfoForLinking.AddToString(stringBuilder, "PartnerNetworkId", this.PartnerNetworkId);
			ContactInfoForLinking.AddToString(stringBuilder, "PartnerNetworkUserId", this.PartnerNetworkUserId);
			ContactInfoForLinking.AddToString(stringBuilder, "IMAddress", this.IMAddress);
			ContactInfoForLinking.AddToString(stringBuilder, "ItemId", this.ItemId);
			ContactInfoForLinking.AddToString(stringBuilder, "PersonId", this.personId);
			ContactInfoForLinking.AddToString(stringBuilder, "Linked", this.linked);
			ContactInfoForLinking.AddToString(stringBuilder, "LinkRejectHistory", this.linkRejectHistory);
			ContactInfoForLinking.AddToString(stringBuilder, "GALLinkState", this.galLinkState);
			ContactInfoForLinking.AddToString(stringBuilder, "GALLinkID", this.galLinkID);
			ContactInfoForLinking.AddToString(stringBuilder, "AddressBookEntryId", this.addressBookEntryId);
			ContactInfoForLinking.AddToString(stringBuilder, "SmtpAddressCache", this.smtpAddressCache);
			ContactInfoForLinking.AddToString(stringBuilder, "UserApprovedLink", this.userApprovedLink);
			ContactInfoForLinking.AddToString(stringBuilder, "IsDirty", this.isDirty);
			return stringBuilder.ToString();
		}

		// Token: 0x0600350C RID: 13580 RVA: 0x000D63C2 File Offset: 0x000D45C2
		internal static string CanonicalizeEmailAddress(string emailAddress)
		{
			return emailAddress.Trim().ToLowerInvariant();
		}

		// Token: 0x0600350D RID: 13581
		protected abstract void UpdateContact(IExtensibleLogger logger, IContactLinkingPerformanceTracker performanceTracker);

		// Token: 0x0600350E RID: 13582 RVA: 0x000D63D0 File Offset: 0x000D45D0
		protected void SetLinkingProperties(PropertyBagAdaptor propertyBag)
		{
			propertyBag.SetValue(ContactSchema.Linked, this.Linked);
			propertyBag.SetValue(ContactSchema.PersonId, this.PersonId);
			if (this.LinkRejectHistory.Count > 0)
			{
				propertyBag.SetValue(ContactSchema.LinkRejectHistory, ContactInfoForLinking.ToArray<PersonId>(this.LinkRejectHistory));
			}
			else
			{
				propertyBag.DeleteValue(ContactSchema.LinkRejectHistory);
			}
			if (this.GALLinkID != null)
			{
				propertyBag.SetValue(ContactSchema.GALLinkID, this.GALLinkID.Value);
			}
			else
			{
				propertyBag.DeleteValue(ContactSchema.GALLinkID);
			}
			if (this.addressBookEntryId != null)
			{
				propertyBag.SetValue(ContactSchema.AddressBookEntryId, this.addressBookEntryId);
			}
			else
			{
				propertyBag.DeleteValue(ContactSchema.AddressBookEntryId);
			}
			if (this.SmtpAddressCache != null)
			{
				propertyBag.SetValue(ContactSchema.SmtpAddressCache, this.SmtpAddressCache);
			}
			else
			{
				propertyBag.DeleteValue(ContactSchema.SmtpAddressCache);
			}
			propertyBag.SetValue(ContactSchema.GALLinkState, this.GALLinkState);
			propertyBag.SetValue(ContactSchema.UserApprovedLink, this.UserApprovedLink);
		}

		// Token: 0x0600350F RID: 13583 RVA: 0x000D64E8 File Offset: 0x000D46E8
		protected void RetryOnTransientException(IExtensibleLogger logger, string description, Action action)
		{
			int num = 2;
			try
			{
				IL_02:
				action();
			}
			catch (StorageTransientException ex)
			{
				ContactInfoForLinking.Tracer.TraceError((long)this.GetHashCode(), "ContactInfoForLinking.RetryOnTransientException: failed {0} with id = {1}; given-name: {2}; person-id: {3}.  Exception: {4}", new object[]
				{
					description,
					this.ItemId,
					this.GivenName,
					this.PersonId,
					ex
				});
				if (num > 0)
				{
					num--;
					goto IL_02;
				}
				logger.LogEvent(new SchemaBasedLogEvent<ContactLinkingLogSchema.Error>
				{
					{
						ContactLinkingLogSchema.Error.Context,
						description
					},
					{
						ContactLinkingLogSchema.Error.Exception,
						ex
					}
				});
				throw;
			}
			catch (StoragePermanentException ex2)
			{
				ContactInfoForLinking.Tracer.TraceError((long)this.GetHashCode(), "ContactInfoForLinking.RetryOnTransientException: failed {0} contact with id = {1}; given-name: {2}; person-id: {3}.  Exception: {4}", new object[]
				{
					description,
					this.ItemId,
					this.GivenName,
					this.PersonId,
					ex2
				});
				logger.LogEvent(new SchemaBasedLogEvent<ContactLinkingLogSchema.Error>
				{
					{
						ContactLinkingLogSchema.Error.Context,
						description
					},
					{
						ContactLinkingLogSchema.Error.Exception,
						ex2
					}
				});
				throw;
			}
		}

		// Token: 0x06003510 RID: 13584 RVA: 0x000D66BC File Offset: 0x000D48BC
		protected void RetryOnTransientExceptionCatchObjectNotFoundException(IExtensibleLogger logger, string description, Action action)
		{
			this.RetryOnTransientException(logger, description, delegate
			{
				try
				{
					action();
				}
				catch (ObjectNotFoundException ex)
				{
					ContactInfoForLinking.Tracer.TraceError((long)this.GetHashCode(), "Failed to perform {0}. ItemId: {1}; given-name: {2}; person-id: {3}. Exception: {4}", new object[]
					{
						description,
						this.ItemId,
						this.GivenName,
						this.PersonId,
						ex
					});
					logger.LogEvent(new SchemaBasedLogEvent<ContactLinkingLogSchema.Error>
					{
						{
							ContactLinkingLogSchema.Error.Context,
							description
						},
						{
							ContactLinkingLogSchema.Error.Exception,
							ex
						}
					});
				}
			});
		}

		// Token: 0x06003511 RID: 13585 RVA: 0x000D670C File Offset: 0x000D490C
		private static void AddToString(StringBuilder text, string description, object value)
		{
			string valueString = ContactLinkingStrings.GetValueString(value);
			if (valueString != null)
			{
				if (text.Length > 0)
				{
					text.Append(", ");
				}
				text.Append(description);
				text.Append("=");
				text.Append(valueString);
			}
		}

		// Token: 0x06003512 RID: 13586 RVA: 0x000D6754 File Offset: 0x000D4954
		private static HashSet<string> GetEmailAddresses(PropertyBagAdaptor propertyBag)
		{
			HashSet<string> hashSet = new HashSet<string>();
			foreach (StorePropertyDefinition propertyDefinition in ContactSchema.EmailAddressProperties)
			{
				string text = ContactInfoForLinking.CanonicalizeEmailAddress(propertyBag.GetValueOrDefault<string>(propertyDefinition, string.Empty));
				if (!string.IsNullOrEmpty(text))
				{
					hashSet.Add(text);
				}
			}
			return hashSet;
		}

		// Token: 0x06003513 RID: 13587 RVA: 0x000D67A8 File Offset: 0x000D49A8
		private static HashSet<T> GetPropertyAsHashSet<T>(PropertyBagAdaptor propertyBag, StorePropertyDefinition property, HashSet<T> defaultValue)
		{
			T[] valueOrDefault = propertyBag.GetValueOrDefault<T[]>(property, null);
			if (valueOrDefault == null || valueOrDefault.Length == 0)
			{
				return defaultValue;
			}
			return new HashSet<T>(valueOrDefault);
		}

		// Token: 0x06003514 RID: 13588 RVA: 0x000D67D0 File Offset: 0x000D49D0
		private static T[] ToArray<T>(HashSet<T> set)
		{
			T[] array = new T[set.Count];
			set.CopyTo(array);
			return array;
		}

		// Token: 0x06003515 RID: 13589 RVA: 0x000D67F4 File Offset: 0x000D49F4
		[Conditional("DEBUG")]
		private static void ValidateGALLinkProperties(GALLinkState galLinkState, Guid? galLinkId, byte[] addressBookEntryId, string[] smtpAddressCache)
		{
			switch (galLinkState)
			{
			default:
				return;
			}
		}

		// Token: 0x04001C28 RID: 7208
		private const int MaximumRetry = 2;

		// Token: 0x04001C29 RID: 7209
		internal static readonly StorePropertyDefinition[] Properties = new StorePropertyDefinition[]
		{
			ItemSchema.Id,
			ContactSchema.Email1EmailAddress,
			ContactSchema.Email2EmailAddress,
			ContactSchema.Email3EmailAddress,
			ContactSchema.GivenName,
			ContactSchema.Surname,
			StoreObjectSchema.DisplayName,
			ContactSchema.PartnerNetworkId,
			ContactSchema.PartnerNetworkUserId,
			ContactProtectedPropertiesSchema.ProtectedEmailAddress,
			ContactSchema.Linked,
			ContactSchema.LinkRejectHistory,
			ContactSchema.IMAddress,
			ContactSchema.GALLinkID,
			ContactSchema.AddressBookEntryId,
			ContactSchema.SmtpAddressCache,
			ContactSchema.GALLinkState,
			ContactSchema.PersonId,
			ContactSchema.UserApprovedLink,
			StoreObjectSchema.ItemClass
		};

		// Token: 0x04001C2A RID: 7210
		protected static readonly Microsoft.Exchange.Diagnostics.Trace Tracer = ExTraceGlobals.ContactLinkingTracer;

		// Token: 0x04001C2B RID: 7211
		private bool linked;

		// Token: 0x04001C2C RID: 7212
		private PersonId personId;

		// Token: 0x04001C2D RID: 7213
		private HashSet<PersonId> linkRejectHistory;

		// Token: 0x04001C2E RID: 7214
		private Guid? galLinkID;

		// Token: 0x04001C2F RID: 7215
		private byte[] addressBookEntryId;

		// Token: 0x04001C30 RID: 7216
		private GALLinkState galLinkState;

		// Token: 0x04001C31 RID: 7217
		private string[] smtpAddressCache;

		// Token: 0x04001C32 RID: 7218
		private bool isDirty;

		// Token: 0x04001C33 RID: 7219
		private bool userApprovedLink;

		// Token: 0x020004AA RID: 1194
		internal static class SmtpAddressCacheComparer
		{
			// Token: 0x06003517 RID: 13591 RVA: 0x000D68E8 File Offset: 0x000D4AE8
			public static bool Equals(string[] left, string[] right)
			{
				if (object.ReferenceEquals(left, right))
				{
					return true;
				}
				if (left == null || right == null)
				{
					return false;
				}
				if (left.Length != right.Length)
				{
					return false;
				}
				for (int i = 0; i < left.Length; i++)
				{
					if (!StringComparer.Ordinal.Equals(left[i], right[i]))
					{
						return false;
					}
				}
				return true;
			}
		}
	}
}
