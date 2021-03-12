using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.CrossServerMailboxAccess;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x020000ED RID: 237
	internal class UserDataValidator : DisposableBase, IDataValidator, IDisposeTrackable, IDisposable
	{
		// Token: 0x060007C6 RID: 1990 RVA: 0x0001E3A6 File Offset: 0x0001C5A6
		public UserDataValidator(UMSubscriber user)
		{
			this.subscriber = user;
			this.permChecker = new DialingPermissionsCheck(this.subscriber.ADRecipient as ADUser, this.subscriber.DialPlan);
			this.cache = new Dictionary<string, UserDataValidator.CacheItem>();
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0001E3E8 File Offset: 0x0001C5E8
		public bool ValidateADContactForOutdialing(string legacyExchangeDN, out IDataValidationResult result)
		{
			DataValidationResult dataValidationResult = new DataValidationResult();
			result = dataValidationResult;
			PIIMessage piimessage = PIIMessage.Create(PIIType._PII, legacyExchangeDN);
			base.CheckDisposed();
			UserDataValidator.CacheItem cacheItem = null;
			ADRecipient adrecipient;
			if (this.cache.TryGetValue(legacyExchangeDN, out cacheItem))
			{
				adrecipient = cacheItem.Recipient;
				if (cacheItem.TransferToPhoneValidationResult != null)
				{
					dataValidationResult.PAAValidationResult = cacheItem.TransferToPhoneValidationResult.Value;
					dataValidationResult.PhoneNumber = cacheItem.NumberToDial;
					dataValidationResult.ADRecipient = adrecipient;
					PIIMessage piimessage2 = PIIMessage.Create(PIIType._PhoneNumber, (dataValidationResult.PhoneNumber != null) ? dataValidationResult.PhoneNumber.ToString() : "<null>");
					PIIMessage[] data = new PIIMessage[]
					{
						piimessage,
						piimessage2
					};
					CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, data, "UserDataValidator::ValidateADContactForOutdialing(_PII) [FROM CACHE] returns status {0} NumberToDial [_PhoneNumber]", new object[]
					{
						result
					});
					return dataValidationResult.PAAValidationResult == PAAValidationResult.Valid;
				}
			}
			else
			{
				adrecipient = this.Resolve(legacyExchangeDN);
				cacheItem = new UserDataValidator.CacheItem(adrecipient);
				this.cache[legacyExchangeDN] = cacheItem;
			}
			if (adrecipient == null)
			{
				dataValidationResult.PAAValidationResult = PAAValidationResult.NonExistentDirectoryUser;
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, piimessage, "UserDataValidator::ValidateADContactForOutdialing(_PII) returns status {0}", new object[]
				{
					result
				});
				cacheItem.TransferToPhoneValidationResult = new PAAValidationResult?(dataValidationResult.PAAValidationResult);
				cacheItem.TransferToMailboxValidationResult = new PAAValidationResult?(dataValidationResult.PAAValidationResult);
				return false;
			}
			DialingPermissionsCheck.DialingPermissionsCheckResult dialingPermissionsCheckResult = this.permChecker.CheckDirectoryUser(adrecipient, null);
			if (!dialingPermissionsCheckResult.HaveValidPhone)
			{
				dataValidationResult.PAAValidationResult = PAAValidationResult.NoValidPhones;
				cacheItem.TransferToPhoneValidationResult = new PAAValidationResult?(dataValidationResult.PAAValidationResult);
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, piimessage, "UserDataValidator::ValidateADContactForOutdialing(_PII) returns status {0}", new object[]
				{
					result
				});
				return false;
			}
			if (!dialingPermissionsCheckResult.AllowCall)
			{
				dataValidationResult.PAAValidationResult = PAAValidationResult.PermissionCheckFailure;
				cacheItem.TransferToPhoneValidationResult = new PAAValidationResult?(dataValidationResult.PAAValidationResult);
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, piimessage, "UserDataValidator::ValidateADContactForOutdialing(_PII) returns status {0}", new object[]
				{
					result
				});
				return false;
			}
			dataValidationResult.PhoneNumber = dialingPermissionsCheckResult.NumberToDial;
			dataValidationResult.ADRecipient = adrecipient;
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, piimessage, "UserDataValidator::ValidateADContactForOutdialing(_PII) returns status {0} NumberToDial {1}[{2}]", new object[]
			{
				result,
				dataValidationResult.PhoneNumber.Number,
				dataValidationResult.PhoneNumber.ToDial
			});
			cacheItem.TransferToPhoneValidationResult = new PAAValidationResult?(dataValidationResult.PAAValidationResult);
			cacheItem.NumberToDial = dataValidationResult.PhoneNumber;
			return true;
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0001E640 File Offset: 0x0001C840
		public bool ValidateADContactForTransferToMailbox(string legacyExchangeDN, out IDataValidationResult result)
		{
			DataValidationResult dataValidationResult = new DataValidationResult();
			result = dataValidationResult;
			bool result2 = true;
			PIIMessage data = PIIMessage.Create(PIIType._PII, legacyExchangeDN);
			base.CheckDisposed();
			UserDataValidator.CacheItem cacheItem = null;
			ADRecipient adrecipient;
			if (this.cache.TryGetValue(legacyExchangeDN, out cacheItem))
			{
				adrecipient = cacheItem.Recipient;
				if (cacheItem.TransferToMailboxValidationResult != null)
				{
					dataValidationResult.PAAValidationResult = cacheItem.TransferToMailboxValidationResult.Value;
					dataValidationResult.ADRecipient = adrecipient;
					CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, data, "UserDataValidator::ValidateADContactForOutdialing(_PII) [FROM CACHE] returns status {0}", new object[]
					{
						result
					});
					return dataValidationResult.PAAValidationResult == PAAValidationResult.Valid;
				}
			}
			else
			{
				adrecipient = this.Resolve(legacyExchangeDN);
				cacheItem = new UserDataValidator.CacheItem(adrecipient);
				this.cache[legacyExchangeDN] = cacheItem;
			}
			if (adrecipient == null)
			{
				dataValidationResult.PAAValidationResult = PAAValidationResult.NonExistentDirectoryUser;
				cacheItem.TransferToMailboxValidationResult = new PAAValidationResult?(dataValidationResult.PAAValidationResult);
				cacheItem.TransferToPhoneValidationResult = new PAAValidationResult?(dataValidationResult.PAAValidationResult);
				result2 = false;
			}
			else if (adrecipient.RecipientType != RecipientType.UserMailbox && adrecipient.RecipientType != RecipientType.MailContact && adrecipient.RecipientType != RecipientType.MailUser)
			{
				dataValidationResult.PAAValidationResult = PAAValidationResult.NonMailboxDirectoryUser;
				cacheItem.TransferToMailboxValidationResult = new PAAValidationResult?(dataValidationResult.PAAValidationResult);
				dataValidationResult.ADRecipient = adrecipient;
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, data, "UserDataValidator::ValidateADContactForTransferToMailbox(_PII) Recipient {0} is of invalid t ype {1}", new object[]
				{
					adrecipient.DisplayName,
					adrecipient.RecipientType.ToString()
				});
				result2 = false;
			}
			dataValidationResult.ADRecipient = adrecipient;
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, data, "UserDataValidator::ValidateADContactForTransferToMailbox(_PII) returns status {0}", new object[]
			{
				result
			});
			return result2;
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0001E7CC File Offset: 0x0001C9CC
		public bool ValidatePhoneNumberForOutdialing(string phoneNumber, out IDataValidationResult result)
		{
			DataValidationResult dataValidationResult = new DataValidationResult();
			result = dataValidationResult;
			base.CheckDisposed();
			PIIMessage data = PIIMessage.Create(PIIType._PhoneNumber, phoneNumber);
			PhoneNumber phoneNumber2 = null;
			if (!PhoneNumber.TryParse(this.subscriber.DialPlan, phoneNumber, out phoneNumber2))
			{
				dataValidationResult.PAAValidationResult = PAAValidationResult.ParseError;
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, data, "UserDataValidator::ValidatePhoneNumberForOutdialing(_PhoneNumber) returns status {0}", new object[]
				{
					result
				});
				return false;
			}
			if (phoneNumber2.UriType == UMUriType.SipName && this.subscriber.DialPlan.URIType != UMUriType.SipName)
			{
				dataValidationResult.PAAValidationResult = PAAValidationResult.SipUriInNonSipDialPlan;
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, data, "UserDataValidator::ValidatePhoneNumberForOutdialing(_PhoneNumber) returns status {0}", new object[]
				{
					result
				});
				return false;
			}
			DialingPermissionsCheck.DialingPermissionsCheckResult dialingPermissionsCheckResult = this.permChecker.CheckPhoneNumber(phoneNumber2);
			if (!dialingPermissionsCheckResult.AllowCall)
			{
				dataValidationResult.PAAValidationResult = PAAValidationResult.PermissionCheckFailure;
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, data, "UserDataValidator::ValidatePhoneNumberForOutdialing(_PhoneNumber) returns status {0}", new object[]
				{
					result
				});
				return false;
			}
			dataValidationResult.PhoneNumber = dialingPermissionsCheckResult.NumberToDial;
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, data, "UserDataValidator::ValidatePhoneNumberForOutdialing(_PhoneNumber) returns status {0} NumberToDial {1}[{2}]", new object[]
			{
				result,
				dataValidationResult.PhoneNumber,
				dataValidationResult.PhoneNumber.ToDial
			});
			return true;
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0001E900 File Offset: 0x0001CB00
		public bool ValidateContactItemCallerId(StoreObjectId storeId, out IDataValidationResult result)
		{
			DataValidationResult dataValidationResult = new DataValidationResult();
			result = dataValidationResult;
			bool result2 = true;
			base.CheckDisposed();
			if (storeId.ObjectType != StoreObjectType.Contact)
			{
				result2 = false;
				dataValidationResult.PAAValidationResult = PAAValidationResult.NonExistentContact;
				return result2;
			}
			try
			{
				this.BuildContactCache();
				if (!this.contactItemCache.IsContactValid(storeId))
				{
					result2 = false;
					dataValidationResult.PAAValidationResult = PAAValidationResult.NonExistentContact;
				}
				else
				{
					dataValidationResult.PersonalContactInfo = this.contactItemCache.GetContact(storeId);
				}
			}
			catch (ObjectNotFoundException)
			{
				result2 = false;
				dataValidationResult.PAAValidationResult = PAAValidationResult.NonExistentContact;
			}
			catch (ArgumentException)
			{
				result2 = false;
				dataValidationResult.PAAValidationResult = PAAValidationResult.NonExistentContact;
			}
			catch (CorruptDataException)
			{
				result2 = false;
				dataValidationResult.PAAValidationResult = PAAValidationResult.NonExistentContact;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "UserDataValidator::ValidateContactItemCallerId({0}) returns status {1}", new object[]
			{
				storeId.ToString(),
				dataValidationResult.PAAValidationResult
			});
			return result2;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0001E9E4 File Offset: 0x0001CBE4
		public bool ValidateADContactCallerId(string legacyExchangeDN, out IDataValidationResult result)
		{
			bool result2 = true;
			DataValidationResult dataValidationResult = new DataValidationResult();
			result = dataValidationResult;
			IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromUmUser(this.subscriber);
			ADRecipient adrecipient = iadrecipientLookup.LookupByLegacyExchangeDN(legacyExchangeDN);
			if (adrecipient == null)
			{
				result2 = false;
				dataValidationResult.PAAValidationResult = PAAValidationResult.NonExistentDirectoryUser;
			}
			dataValidationResult.ADRecipient = adrecipient;
			PIIMessage data = PIIMessage.Create(PIIType._PII, legacyExchangeDN);
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, data, "UserDataValidator::ValidateADContactCallerId(_PII) returns status {0}", new object[]
			{
				result
			});
			return result2;
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0001EA50 File Offset: 0x0001CC50
		public bool ValidatePersonaContactCallerId(string emailAddress, out IDataValidationResult result)
		{
			bool result2 = true;
			DataValidationResult dataValidationResult = new DataValidationResult();
			result = dataValidationResult;
			PersonaType personaType = null;
			using (IUMUserMailboxStorage umuserMailboxAccessor = InterServerMailboxAccessor.GetUMUserMailboxAccessor(this.subscriber.ADUser, false))
			{
				personaType = umuserMailboxAccessor.GetPersonaFromEmail(emailAddress);
			}
			if (personaType == null)
			{
				result2 = false;
				dataValidationResult.PAAValidationResult = PAAValidationResult.NonExistentPersona;
			}
			dataValidationResult.PersonaContactInfo = personaType;
			PIIMessage data = PIIMessage.Create(PIIType._EmailAddress, emailAddress);
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, data, "UserDataValidator::ValidatePersonaContactCallerId(_EmailAddress) returns status {0}", new object[]
			{
				result
			});
			return result2;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0001EAE4 File Offset: 0x0001CCE4
		public bool ValidatePhoneNumberCallerId(string number, out IDataValidationResult result)
		{
			DataValidationResult dataValidationResult = new DataValidationResult();
			result = dataValidationResult;
			bool result2 = true;
			base.CheckDisposed();
			PhoneNumber phoneNumber = null;
			if (!PhoneNumber.TryParse(this.subscriber.DialPlan, number, out phoneNumber))
			{
				dataValidationResult.PAAValidationResult = PAAValidationResult.ParseError;
				result2 = false;
			}
			UMUriType umuriType = Utils.DetermineNumberType(number);
			if (umuriType == UMUriType.SipName && this.subscriber.DialPlan.URIType != UMUriType.SipName)
			{
				dataValidationResult.PAAValidationResult = PAAValidationResult.SipUriInNonSipDialPlan;
				result2 = false;
			}
			dataValidationResult.PhoneNumber = phoneNumber;
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "UserDataValidator::ValidatePhoneNumberCallerId({0}) returns status {1}", new object[]
			{
				number,
				result
			});
			return result2;
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001EB78 File Offset: 0x0001CD78
		public bool ValidateContactFolderCallerId(out IDataValidationResult result)
		{
			DataValidationResult dataValidationResult = new DataValidationResult();
			result = dataValidationResult;
			bool result2 = true;
			if (!this.subscriber.HasContactsFolder)
			{
				dataValidationResult.PAAValidationResult = PAAValidationResult.NonExistentDefaultContactsFolder;
				result2 = false;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "UserDataValidator::ValidateContactFolderCallerId() returns status {0}", new object[]
			{
				result
			});
			return result2;
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0001EBC4 File Offset: 0x0001CDC4
		public bool ValidateExtensions(IList<string> extensions, out PAAValidationResult result, out string extensionInError)
		{
			IList<string> extensionsInPrimaryDialPlan = this.subscriber.GetExtensionsInPrimaryDialPlan();
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
			extensionInError = null;
			result = PAAValidationResult.Valid;
			if (extensions.Count == 0)
			{
				return true;
			}
			bool flag = false;
			foreach (string text in extensionsInPrimaryDialPlan)
			{
				if (dictionary.TryGetValue(text, out flag))
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "UserDataValidator::ValidateExtensions() Extension '{0}' has been specified twice for the user", new object[]
					{
						text
					});
					result = PAAValidationResult.InvalidExtension;
					extensionInError = text;
					return false;
				}
				dictionary.Add(text, true);
			}
			flag = false;
			foreach (string text2 in extensions)
			{
				if (!dictionary.TryGetValue(text2, out flag))
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "UserDataValidator::ValidateExtensions() Extension '{0}' is not on the list of extensions for user", new object[]
					{
						text2
					});
					extensionInError = text2;
					result = PAAValidationResult.InvalidExtension;
					return false;
				}
			}
			result = PAAValidationResult.Valid;
			return true;
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0001ECEC File Offset: 0x0001CEEC
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0001ECEE File Offset: 0x0001CEEE
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<UserDataValidator>(this);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0001ECF8 File Offset: 0x0001CEF8
		private ADRecipient Resolve(string legacyExchangeDN)
		{
			IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromUmUser(this.subscriber);
			return iadrecipientLookup.LookupByLegacyExchangeDN(legacyExchangeDN);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0001ED18 File Offset: 0x0001CF18
		private void BuildContactCache()
		{
			if (this.contactItemCache != null)
			{
				return;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "UserDataValidator::BuildContactCache()", new object[0]);
			this.contactItemCache = PersonalContactCache.Create(this.subscriber);
			this.contactItemCache.BuildCache();
		}

		// Token: 0x04000463 RID: 1123
		private UMSubscriber subscriber;

		// Token: 0x04000464 RID: 1124
		private DialingPermissionsCheck permChecker;

		// Token: 0x04000465 RID: 1125
		private Dictionary<string, UserDataValidator.CacheItem> cache;

		// Token: 0x04000466 RID: 1126
		private IPersonalContactCache contactItemCache;

		// Token: 0x020000EE RID: 238
		private class CacheItem
		{
			// Token: 0x060007D4 RID: 2004 RVA: 0x0001ED55 File Offset: 0x0001CF55
			internal CacheItem(ADRecipient recipient)
			{
				this.recipient = recipient;
			}

			// Token: 0x170001D8 RID: 472
			// (get) Token: 0x060007D5 RID: 2005 RVA: 0x0001ED64 File Offset: 0x0001CF64
			internal ADRecipient Recipient
			{
				get
				{
					return this.recipient;
				}
			}

			// Token: 0x170001D9 RID: 473
			// (get) Token: 0x060007D6 RID: 2006 RVA: 0x0001ED6C File Offset: 0x0001CF6C
			// (set) Token: 0x060007D7 RID: 2007 RVA: 0x0001ED74 File Offset: 0x0001CF74
			internal PAAValidationResult? TransferToMailboxValidationResult
			{
				get
				{
					return this.txfrMailboxValidationResult;
				}
				set
				{
					this.txfrMailboxValidationResult = value;
				}
			}

			// Token: 0x170001DA RID: 474
			// (get) Token: 0x060007D8 RID: 2008 RVA: 0x0001ED7D File Offset: 0x0001CF7D
			// (set) Token: 0x060007D9 RID: 2009 RVA: 0x0001ED85 File Offset: 0x0001CF85
			internal PAAValidationResult? TransferToPhoneValidationResult
			{
				get
				{
					return this.txfrPhoneValidationResult;
				}
				set
				{
					this.txfrPhoneValidationResult = value;
				}
			}

			// Token: 0x170001DB RID: 475
			// (get) Token: 0x060007DA RID: 2010 RVA: 0x0001ED8E File Offset: 0x0001CF8E
			// (set) Token: 0x060007DB RID: 2011 RVA: 0x0001ED96 File Offset: 0x0001CF96
			internal PhoneNumber NumberToDial
			{
				get
				{
					return this.numberToDial;
				}
				set
				{
					this.numberToDial = value;
				}
			}

			// Token: 0x04000467 RID: 1127
			private ADRecipient recipient;

			// Token: 0x04000468 RID: 1128
			private PAAValidationResult? txfrPhoneValidationResult;

			// Token: 0x04000469 RID: 1129
			private PAAValidationResult? txfrMailboxValidationResult;

			// Token: 0x0400046A RID: 1130
			private PhoneNumber numberToDial;
		}
	}
}
