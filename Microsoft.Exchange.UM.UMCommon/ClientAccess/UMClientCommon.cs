using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Configuration.SQM;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.ClientAccess
{
	// Token: 0x02000027 RID: 39
	internal sealed class UMClientCommon : UMClientCommonBase
	{
		// Token: 0x0600023A RID: 570 RVA: 0x00008F1F File Offset: 0x0000711F
		public UMClientCommon()
		{
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00008F28 File Offset: 0x00007128
		public UMClientCommon(IExchangePrincipal principal)
		{
			try
			{
				this.mailboxRecipient = UMRecipient.Factory.FromPrincipal<UMMailboxRecipient>(principal);
				if (this.mailboxRecipient == null)
				{
					throw new InvalidPrincipalException();
				}
				base.TracePrefix = string.Format(CultureInfo.InvariantCulture, "{0}({1}): ", new object[]
				{
					base.GetType().Name,
					this.mailboxRecipient.DisplayName
				});
			}
			catch (LocalizedException ex)
			{
				base.DebugTrace("{0}", new object[]
				{
					ex
				});
				throw;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00008FBC File Offset: 0x000071BC
		internal UMMailboxPolicy UMPolicy
		{
			get
			{
				this.ValidateUser();
				return this.Subscriber.UMMailboxPolicy;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00008FCF File Offset: 0x000071CF
		internal UMDialPlan DialPlan
		{
			get
			{
				this.ValidateUser();
				return this.Subscriber.DialPlan;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00008FE2 File Offset: 0x000071E2
		private UMSubscriber Subscriber
		{
			get
			{
				return this.mailboxRecipient as UMSubscriber;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00008FF0 File Offset: 0x000071F0
		private ADUser UserInstance
		{
			get
			{
				if (this.lazyUserInstance == null)
				{
					this.ValidateUser();
					IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromADRecipient(this.mailboxRecipient.ADRecipient, false);
					ADRecipient adrecipient = iadrecipientLookup.LookupByObjectId(this.mailboxRecipient.ADRecipient.Id);
					this.lazyUserInstance = (adrecipient as ADUser);
					if (this.lazyUserInstance == null)
					{
						throw new InvalidADRecipientException();
					}
				}
				return this.lazyUserInstance;
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00009054 File Offset: 0x00007254
		public UMPropertiesEx GetUMProperties()
		{
			UMPropertiesEx result;
			try
			{
				base.DebugTrace("GetUMProperties()", new object[0]);
				this.ValidateUser();
				ADUser aduser = this.mailboxRecipient.ADRecipient as ADUser;
				if (aduser == null)
				{
					throw new InvalidADRecipientException();
				}
				UMPropertiesEx umpropertiesEx = new UMPropertiesEx();
				umpropertiesEx.OofStatus = this.Subscriber.ConfigFolder.IsOof;
				umpropertiesEx.TelephoneAccessNumbers = this.GetTelephoneAccessNumbers();
				umpropertiesEx.PlayOnPhoneDialString = this.Subscriber.ConfigFolder.PlayOnPhoneDialString;
				umpropertiesEx.TelephoneAccessFolderEmail = this.Subscriber.ConfigFolder.TelephoneAccessFolderEmail;
				umpropertiesEx.ReceivedVoiceMailPreviewEnabled = this.Subscriber.ConfigFolder.ReceivedVoiceMailPreviewEnabled;
				umpropertiesEx.SentVoiceMailPreviewEnabled = this.Subscriber.ConfigFolder.SentVoiceMailPreviewEnabled;
				umpropertiesEx.ReadUnreadVoicemailInFIFOOrder = this.Subscriber.ConfigFolder.ReadUnreadVoicemailInFIFOOrder;
				umpropertiesEx.PlayOnPhoneEnabled = this.IsPlayOnPhoneEnabled();
				UMMailbox ummailbox = new UMMailbox(aduser);
				umpropertiesEx.MissedCallNotificationEnabled = ummailbox.MissedCallNotificationEnabled;
				umpropertiesEx.PinlessAccessToVoicemail = ummailbox.PinlessAccessToVoiceMailEnabled;
				umpropertiesEx.SMSNotificationOption = ummailbox.UMSMSNotificationOption;
				base.DebugTrace("\tOOf={0}, TelNumbers={1}, PopDialStr={2}, EmailFolder={3}, ReceivedVoiceMailPreviewEnabled={4}, SentVoiceMailPreviewEnabled {5}, PlayOnPhone={6}, MissedCall={7}, PinlessAccesstoVoicemail={8}, SMSNotificationOption={9}, ReadUnreadVoicemailInFIFOOrder={10}", new object[]
				{
					umpropertiesEx.OofStatus,
					umpropertiesEx.TelephoneAccessNumbers,
					umpropertiesEx.PlayOnPhoneDialString,
					umpropertiesEx.TelephoneAccessFolderEmail,
					umpropertiesEx.ReceivedVoiceMailPreviewEnabled,
					umpropertiesEx.SentVoiceMailPreviewEnabled,
					umpropertiesEx.PlayOnPhoneEnabled,
					umpropertiesEx.MissedCallNotificationEnabled,
					umpropertiesEx.PinlessAccessToVoicemail,
					umpropertiesEx.SMSNotificationOption,
					umpropertiesEx.ReadUnreadVoicemailInFIFOOrder
				});
				result = umpropertiesEx;
			}
			catch (LocalizedException ex)
			{
				base.DebugTrace("{0}", new object[]
				{
					ex
				});
				throw;
			}
			return result;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00009250 File Offset: 0x00007450
		public bool IsUMEnabled()
		{
			return null != this.Subscriber;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000925E File Offset: 0x0000745E
		public bool IsPlayOnPhoneEnabled()
		{
			return this.IsUMEnabled() && this.Subscriber.IsPlayOnPhoneEnabled;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00009275 File Offset: 0x00007475
		public bool IsSmsNotificationsEnabled()
		{
			return this.IsUMEnabled() && this.Subscriber.IsSmsNotificationsEnabled;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000928C File Offset: 0x0000748C
		public bool IsRequireProtectedPlayOnPhone()
		{
			return this.IsUMEnabled() && this.Subscriber.IsRequireProtectedPlayOnPhone;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000092A3 File Offset: 0x000074A3
		public string GetPlayOnPhoneDialString()
		{
			if (!this.IsUMEnabled())
			{
				return string.Empty;
			}
			return this.GetUMProperties().PlayOnPhoneDialString;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000092E4 File Offset: 0x000074E4
		public void SetOofStatus(bool status)
		{
			base.DebugTrace("SetOofStatus({0})", new object[]
			{
				status
			});
			this.UpdateSubscriberConfig(delegate
			{
				this.Subscriber.ConfigFolder.IsOof = status;
			});
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00009360 File Offset: 0x00007560
		public void SetPlayOnPhoneDialString(string dialString)
		{
			if (dialString != null)
			{
				dialString = dialString.Trim();
			}
			base.DebugTrace("SetPlayOnPhoneDialString({0})", new object[]
			{
				dialString
			});
			this.UpdateSubscriberConfig(delegate
			{
				this.Subscriber.ConfigFolder.PlayOnPhoneDialString = dialString;
			});
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000093F0 File Offset: 0x000075F0
		public void SetTelephoneAccessFolderEmail(string base64FolderId)
		{
			base.DebugTrace("SetTelephoneAccessFolderEmail({0})", new object[]
			{
				base64FolderId
			});
			this.UpdateSubscriberConfig(delegate
			{
				this.Subscriber.ConfigFolder.TelephoneAccessFolderEmail = base64FolderId;
			});
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00009464 File Offset: 0x00007664
		public void SetReceivedVoiceMailPreview(bool receivedVoiceMailPreview)
		{
			base.DebugTrace("ReceivedVoiceMailPreviewEnabled({0})", new object[]
			{
				receivedVoiceMailPreview
			});
			this.UpdateSubscriberConfig(delegate
			{
				this.Subscriber.ConfigFolder.ReceivedVoiceMailPreviewEnabled = receivedVoiceMailPreview;
			});
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000094E0 File Offset: 0x000076E0
		public void SetSentVoiceMailPreview(bool sentVoiceMailPreview)
		{
			base.DebugTrace("SentVoiceMailPreviewEnabled({0})", new object[]
			{
				sentVoiceMailPreview
			});
			this.UpdateSubscriberConfig(delegate
			{
				this.Subscriber.ConfigFolder.SentVoiceMailPreviewEnabled = sentVoiceMailPreview;
			});
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000955C File Offset: 0x0000775C
		public void SetUnReadVoiceMailReadingOrder(bool fifoOrder)
		{
			base.DebugTrace("SetUnReadVoiceMailReadingOrder({0})", new object[]
			{
				fifoOrder
			});
			this.UpdateSubscriberConfig(delegate
			{
				this.Subscriber.ConfigFolder.ReadUnreadVoicemailInFIFOOrder = fifoOrder;
			});
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000095D8 File Offset: 0x000077D8
		public void SetVoiceNotificationStatus(VoiceNotificationStatus voiceNotificationStatus)
		{
			base.DebugTrace("SetVoiceNotification({0})", new object[]
			{
				voiceNotificationStatus
			});
			if (voiceNotificationStatus == VoiceNotificationStatus.Enabled || voiceNotificationStatus == VoiceNotificationStatus.Disabled)
			{
				this.UpdateSubscriberConfig(delegate
				{
					this.Subscriber.ConfigFolder.VoiceNotificationStatus = voiceNotificationStatus;
				});
				return;
			}
			throw new InvalidArgumentException("voiceNotificationStatus");
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00009668 File Offset: 0x00007868
		public void SetMissedCallNotificationEnabled(bool status)
		{
			base.DebugTrace("SetMissedCallNotificationEnabled({0})", new object[]
			{
				status
			});
			this.UpdateUMMailbox(delegate(UMMailbox x)
			{
				x.MissedCallNotificationEnabled = status;
			});
		}

		// Token: 0x0600024E RID: 590 RVA: 0x000096CC File Offset: 0x000078CC
		public void SetPinlessAccessToVoicemail(bool pinlessAccess)
		{
			base.DebugTrace("PinlessAccessToVoicemail({0})", new object[]
			{
				pinlessAccess
			});
			this.UpdateUMMailbox(delegate(UMMailbox x)
			{
				x.PinlessAccessToVoiceMailEnabled = pinlessAccess;
			});
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00009730 File Offset: 0x00007930
		public void SetSMSNotificationOption(UMSMSNotificationOptions option)
		{
			base.DebugTrace("SetSMSNotificationOption({0})", new object[]
			{
				option
			});
			this.UpdateUMMailbox(delegate(UMMailbox x)
			{
				x.UMSMSNotificationOption = option;
			});
			SmsSqmDataPointHelper.AddNotificationConfigDataPoint(SmsSqmSession.Instance, this.mailboxRecipient.ADRecipient.Id, this.mailboxRecipient.ADRecipient.LegacyExchangeDN, SmsSqmDataPointHelper.TranslateEnumForSqm<UMSMSNotificationOptions>(option));
		}

		// Token: 0x06000250 RID: 592 RVA: 0x000097B4 File Offset: 0x000079B4
		public void ResetPIN()
		{
			try
			{
				base.DebugTrace("ResetPIN()", new object[0]);
				if (UMClientCommonBase.Counters != null)
				{
					UMClientCommonBase.Counters.TotalPINResetRequests.Increment();
				}
				this.ValidateUser();
				Utils.ResetPassword(this.Subscriber, true, LockOutResetMode.Reset);
			}
			catch (LocalizedException exception)
			{
				base.LogException(exception);
				if (UMClientCommonBase.Counters != null)
				{
					UMClientCommonBase.Counters.TotalPINResetErrors.Increment();
				}
				throw;
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00009830 File Offset: 0x00007A30
		public string PlayOnPhone(string base64ObjectId, string dialString)
		{
			string result;
			try
			{
				base.DebugTrace("PlayOnPhone({0}, {1})", new object[]
				{
					base64ObjectId,
					dialString
				});
				if (UMClientCommonBase.Counters != null)
				{
					UMClientCommonBase.Counters.TotalPlayOnPhoneRequests.Increment();
				}
				this.ValidateUser();
				this.ValidateObjectId(base64ObjectId);
				UMServerProxy serverByDialplan = UMServerManager.GetServerByDialplan((ADObjectId)this.Subscriber.DialPlan.Identity);
				string sessionId = serverByDialplan.PlayOnPhoneMessage("SMTP:" + this.mailboxRecipient.MailAddress, this.mailboxRecipient.ADRecipient.Guid, this.mailboxRecipient.TenantGuid, base64ObjectId, dialString);
				result = base.EncodeCallId(serverByDialplan.Fqdn, sessionId);
			}
			catch (LocalizedException exception)
			{
				base.LogException(exception);
				if (UMClientCommonBase.Counters != null)
				{
					UMClientCommonBase.Counters.TotalPlayOnPhoneErrors.Increment();
				}
				throw;
			}
			return result;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00009918 File Offset: 0x00007B18
		public string PlayOnPhoneGreeting(UMGreetingType greetingType, string dialString)
		{
			string result;
			try
			{
				base.DebugTrace("PlayOnPhoneGreeting({0}, {1})", new object[]
				{
					greetingType,
					dialString
				});
				if (UMClientCommonBase.Counters != null)
				{
					UMClientCommonBase.Counters.TotalPlayOnPhoneRequests.Increment();
				}
				this.ValidateUser();
				UMServerProxy serverByDialplan = UMServerManager.GetServerByDialplan((ADObjectId)this.Subscriber.DialPlan.Identity);
				string sessionId = serverByDialplan.PlayOnPhoneGreeting("SMTP:" + this.mailboxRecipient.MailAddress, this.mailboxRecipient.ADRecipient.Guid, this.mailboxRecipient.TenantGuid, greetingType, dialString);
				result = base.EncodeCallId(serverByDialplan.Fqdn, sessionId);
			}
			catch (LocalizedException exception)
			{
				base.LogException(exception);
				if (UMClientCommonBase.Counters != null)
				{
					UMClientCommonBase.Counters.TotalPlayOnPhoneErrors.Increment();
				}
				throw;
			}
			return result;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x000099FC File Offset: 0x00007BFC
		public string PlayOnPhonePAAGreeting(Guid paaIdentity, string dialString)
		{
			string result;
			try
			{
				base.DebugTrace("PlayOnPhoneGreeting( PAA = {0},DialString = {1})", new object[]
				{
					paaIdentity.ToString(),
					dialString
				});
				this.ValidateUser();
				UMServerProxy serverByDialplan = UMServerManager.GetServerByDialplan((ADObjectId)this.Subscriber.DialPlan.Identity);
				string sessionId = serverByDialplan.PlayOnPhonePAAGreeting("SMTP:" + this.mailboxRecipient.MailAddress, this.mailboxRecipient.ADRecipient.Guid, this.mailboxRecipient.TenantGuid, paaIdentity, dialString);
				result = base.EncodeCallId(serverByDialplan.Fqdn, sessionId);
			}
			catch (LocalizedException exception)
			{
				base.LogException(exception);
				throw;
			}
			return result;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00009ABC File Offset: 0x00007CBC
		protected override void DisposeMe()
		{
			if (this.mailboxRecipient != null)
			{
				this.mailboxRecipient.Dispose();
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00009AD1 File Offset: 0x00007CD1
		protected override string GetUserContext()
		{
			if (this.mailboxRecipient != null)
			{
				return this.mailboxRecipient.ExchangePrincipal.MailboxInfo.DisplayName;
			}
			return string.Empty;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00009AF8 File Offset: 0x00007CF8
		private string GetTelephoneAccessNumbers()
		{
			StringBuilder stringBuilder = new StringBuilder(string.Empty);
			if (this.Subscriber.DialPlan.AccessTelephoneNumbers != null && this.Subscriber.DialPlan.AccessTelephoneNumbers.Count > 0)
			{
				stringBuilder.Append(this.Subscriber.DialPlan.AccessTelephoneNumbers[0]);
				for (int i = 1; i < this.Subscriber.DialPlan.AccessTelephoneNumbers.Count; i++)
				{
					stringBuilder.Append(", ");
					stringBuilder.Append(this.Subscriber.DialPlan.AccessTelephoneNumbers[i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00009BA8 File Offset: 0x00007DA8
		private void ValidateObjectId(string base64ObjectId)
		{
			Exception ex = null;
			try
			{
				byte[] entryId = Convert.FromBase64String(base64ObjectId);
				StoreObjectId storeId = StoreObjectId.FromProviderSpecificId(entryId);
				PropertyDefinition[] propsToReturn = new PropertyDefinition[]
				{
					ItemSchema.Id
				};
				using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.mailboxRecipient.CreateSessionLock())
				{
					Item item = Item.Bind(mailboxSessionLock.Session, storeId, propsToReturn);
					item.Dispose();
				}
			}
			catch (ArgumentException ex2)
			{
				ex = ex2;
			}
			catch (FormatException ex3)
			{
				ex = ex3;
			}
			catch (LocalizedException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				throw new InvalidObjectIdException(ex);
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00009C60 File Offset: 0x00007E60
		private void ValidateUser()
		{
			if (!this.IsUMEnabled())
			{
				throw new UserNotUmEnabledException(this.mailboxRecipient.ExchangePrincipal.MailboxInfo.DisplayName);
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00009C88 File Offset: 0x00007E88
		private void UpdateUMMailbox(Action<UMMailbox> updater)
		{
			try
			{
				UMMailbox obj = new UMMailbox(this.UserInstance);
				updater(obj);
				this.UserInstance.Session.Save(this.UserInstance);
			}
			catch (LocalizedException ex)
			{
				base.DebugTrace("{0}", new object[]
				{
					ex
				});
				throw;
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00009CEC File Offset: 0x00007EEC
		private void UpdateSubscriberConfig(Action updater)
		{
			try
			{
				this.ValidateUser();
				updater();
				this.Subscriber.ConfigFolder.Save();
			}
			catch (LocalizedException ex)
			{
				base.DebugTrace("{0}", new object[]
				{
					ex
				});
				throw;
			}
		}

		// Token: 0x040000B9 RID: 185
		private UMMailboxRecipient mailboxRecipient;

		// Token: 0x040000BA RID: 186
		private ADUser lazyUserInstance;
	}
}
