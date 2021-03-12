using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Security.Dkm;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.SendAsVerification;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription
{
	// Token: 0x020000BB RID: 187
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SendAsManager
	{
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x0001A0DC File Offset: 0x000182DC
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.SendAsTracer;
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0001A0E4 File Offset: 0x000182E4
		public void ResetVerificationEmailData(PimAggregationSubscription subscription)
		{
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			subscription.VerificationEmailState = VerificationEmailState.EmailNotSent;
			subscription.VerificationEmailMessageId = string.Empty;
			subscription.VerificationEmailTimeStamp = null;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0001A120 File Offset: 0x00018320
		public bool ValidateSharedSecret(PimAggregationSubscription subscription, string sharedSecret)
		{
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("sharedSecret", sharedSecret);
			if (!subscription.SendAsNeedsVerification)
			{
				throw new ArgumentException("The subscription type does not support SendAs validation: " + subscription.SubscriptionType.ToString(), "subscription");
			}
			Guid a;
			if (!this.TryParseGuid(sharedSecret, out a))
			{
				this.currentSyncLogSession.LogVerbose((TSLID)43UL, SendAsManager.Tracer, (long)this.GetHashCode(), "Invalid format for shared secret.  Actual data: {0}", new object[]
				{
					sharedSecret
				});
				return false;
			}
			SendAsState sendAsState;
			Guid guid;
			Guid b;
			SmtpAddress smtpAddress;
			string text;
			string text2;
			if (!this.TryParseSignedHash(subscription.SendAsValidatedEmail, subscription.SendAsNeedsVerification, out sendAsState, out guid, out b, out smtpAddress, out text, out text2))
			{
				this.currentSyncLogSession.LogVerbose((TSLID)44UL, SendAsManager.Tracer, (long)this.GetHashCode(), "Failed to parse SendAsValidatedEmail", new object[0]);
				return false;
			}
			string text3;
			string text4;
			this.GetVerifiedSubscriptionData(subscription, out text3, out text4);
			if (sendAsState != subscription.SendAsState || guid != subscription.SubscriptionGuid || smtpAddress != subscription.UserEmailAddress || text != text3 || text2 != text4)
			{
				string format = "The parsed values in SendAsValidatedEmail do not match the current subscription values. Parsed: SendAsState:'{0}' SubscriptionGuid:'{1}' EmailAddress:'{2}' UserName:'{3}' Server:'{4}' Actual: SendAsState:'{5}' SubscriptionGuid:'{6}' EmailAddress:'{7}' UserName:'{8}' Server:'{9}'";
				this.currentSyncLogSession.LogVerbose((TSLID)45UL, SendAsManager.Tracer, (long)this.GetHashCode(), format, new object[]
				{
					sendAsState,
					guid,
					smtpAddress,
					text,
					text2,
					subscription.SendAsState,
					subscription.SubscriptionGuid,
					subscription.UserEmailAddress,
					text3,
					text4
				});
				return false;
			}
			if (subscription.SendAsState == SendAsState.Enabled)
			{
				return true;
			}
			if (a != b)
			{
				this.currentSyncLogSession.LogVerbose((TSLID)46UL, SendAsManager.Tracer, (long)this.GetHashCode(), "Shared secret mismatch.  Expected: {0} Actual: {1}", new object[]
				{
					b.ToString("N"),
					a.ToString("N")
				});
				return false;
			}
			subscription.SendAsState = SendAsState.Enabled;
			Guid guid2;
			subscription.SendAsValidatedEmail = this.CreateSignedHash(subscription, out guid2);
			return true;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0001A35C File Offset: 0x0001855C
		public void ResendVerificationEmail(PimAggregationSubscription subscription, IEmailSender emailSender)
		{
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			SyncUtilities.ThrowIfArgumentNull("emailSender", emailSender);
			if (!subscription.SendAsNeedsVerification)
			{
				throw new ArgumentException("subscription is not SendAs verified.  Type: " + subscription.SubscriptionType.ToString(), "subscription");
			}
			Guid sharedSecret = Guid.Empty;
			bool flag = false;
			SendAsState sendAsState;
			Guid guid;
			Guid guid2;
			SmtpAddress smtpAddress;
			string text;
			string text2;
			if (this.TryParseSignedHash(subscription.SendAsValidatedEmail, subscription.SendAsNeedsVerification, out sendAsState, out guid, out guid2, out smtpAddress, out text, out text2))
			{
				string text3;
				string text4;
				this.GetVerifiedSubscriptionData(subscription, out text3, out text4);
				if (sendAsState == subscription.SendAsState && guid == subscription.SubscriptionGuid && smtpAddress == subscription.UserEmailAddress && text == text3 && text2 == text4)
				{
					if (subscription.SendAsState == SendAsState.Enabled)
					{
						this.currentSyncLogSession.LogVerbose((TSLID)47UL, SendAsManager.Tracer, (long)this.GetHashCode(), "Subscription already enabled.  Not sending email.", new object[0]);
						return;
					}
					sharedSecret = guid2;
				}
				else
				{
					string format = "The parsed values in SendAsValidatedEmail do not match the current subscription values. Creating a new signed hash. Parsed: SendAsState:'{0}' SubscriptionGuid:'{1}' EmailAddress:'{2}' UserName:'{3}' Server:'{4}' Actual: SendAsState:'{5}' SubscriptionGuid:'{6}' EmailAddress:'{7}' UserName:'{8}' Server:'{9}'";
					this.currentSyncLogSession.LogVerbose((TSLID)48UL, SendAsManager.Tracer, (long)this.GetHashCode(), format, new object[]
					{
						sendAsState,
						guid,
						smtpAddress,
						text,
						text2,
						subscription.SendAsState,
						subscription.SubscriptionGuid,
						subscription.UserEmailAddress,
						text3,
						text4
					});
					flag = true;
				}
			}
			else
			{
				this.currentSyncLogSession.LogVerbose((TSLID)49UL, SendAsManager.Tracer, (long)this.GetHashCode(), "Failed to parse SendAsValidatedEmail.  Creating a new signed hash.", new object[0]);
				flag = true;
			}
			if (flag)
			{
				subscription.SendAsState = SendAsState.Disabled;
				subscription.SendAsValidatedEmail = this.CreateSignedHash(subscription, out sharedSecret);
			}
			this.SendVerificationEmail(subscription, sharedSecret, emailSender);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0001A548 File Offset: 0x00018748
		public void EnableSendAs(PimAggregationSubscription subscription, bool meetsEnableCriteria, IEmailSender emailSender)
		{
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			SyncUtilities.ThrowIfArgumentNull("emailSender", emailSender);
			if (meetsEnableCriteria)
			{
				this.currentSyncLogSession.LogVerbose((TSLID)50UL, SendAsManager.Tracer, (long)this.GetHashCode(), "Send as has been enabled on this subscription.  Subscription id: {0}, type: {1}, meetsEnableCriteria: {2}", new object[]
				{
					subscription.SubscriptionGuid,
					subscription.SubscriptionType,
					meetsEnableCriteria
				});
				subscription.SendAsState = SendAsState.Enabled;
				Guid guid;
				subscription.SendAsValidatedEmail = this.CreateSignedHash(subscription, out guid);
				return;
			}
			this.DisableSendAs(subscription, emailSender);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001A5E0 File Offset: 0x000187E0
		public void DisableSendAs(PimAggregationSubscription subscription, IEmailSender emailSender)
		{
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			SyncUtilities.ThrowIfArgumentNull("emailSender", emailSender);
			this.currentSyncLogSession.LogVerbose((TSLID)51UL, SendAsManager.Tracer, (long)this.GetHashCode(), "Send as has been disabled on this subscription.  Subscription id: {0}, type: {1}", new object[]
			{
				subscription.SubscriptionGuid,
				subscription.SubscriptionType
			});
			subscription.SendAsState = SendAsState.Disabled;
			Guid sharedSecret;
			subscription.SendAsValidatedEmail = this.CreateSignedHash(subscription, out sharedSecret);
			this.SendVerificationEmail(subscription, sharedSecret, emailSender);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001A66A File Offset: 0x0001886A
		public bool IsSubscriptionEnabled(ISendAsSource subscription)
		{
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			return subscription.IsEnabled;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0001A680 File Offset: 0x00018880
		public bool IsSendAsEnabled(ISendAsSource source)
		{
			if (source is TransactionalRequestJob)
			{
				return true;
			}
			PimAggregationSubscription pimAggregationSubscription = source as PimAggregationSubscription;
			SyncUtilities.ThrowIfArgumentNull("subscription", pimAggregationSubscription);
			string verifiedSubscriptionUserName;
			string verifiedSubscriptionIncomingServer;
			this.GetVerifiedSubscriptionData(pimAggregationSubscription, out verifiedSubscriptionUserName, out verifiedSubscriptionIncomingServer);
			return this.IsSendAsEnabled(pimAggregationSubscription.SubscriptionGuid, pimAggregationSubscription.UserEmailAddress, pimAggregationSubscription.SendAsState, pimAggregationSubscription.SendAsNeedsVerification, verifiedSubscriptionUserName, verifiedSubscriptionIncomingServer, pimAggregationSubscription.SendAsValidatedEmail);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001A6DC File Offset: 0x000188DC
		public bool TryGetSendAsSubscription(MessageItem messageItem, MailboxSession mailboxSession, out ISendAsSource source)
		{
			source = null;
			SyncUtilities.ThrowIfArgumentNull("messageItem", messageItem);
			PimAggregationSubscription pimAggregationSubscription = null;
			TransactionalRequestJob transactionalRequestJob = null;
			object property = SendAsManager.GetProperty(messageItem, MessageItemSchema.SharingInstanceGuid);
			if (property != null && !PropertyError.IsPropertyNotFound(property) && (Guid)property != Guid.Empty)
			{
				this.TryLoadSubscription(mailboxSession, (Guid)property, out pimAggregationSubscription);
				if (pimAggregationSubscription != null)
				{
					source = pimAggregationSubscription;
					return true;
				}
				this.TryLoadSyncJob(mailboxSession, (Guid)property, out transactionalRequestJob);
				source = transactionalRequestJob;
			}
			return transactionalRequestJob != null;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0001A758 File Offset: 0x00018958
		public bool TryGetSendAsSubscription(MailboxSession mailboxSession, out ISendAsSource subscription, out bool foundMultipleSubscriptions)
		{
			subscription = null;
			foundMultipleSubscriptions = false;
			List<AggregationSubscription> allSubscriptions = this.GetAllSubscriptions(mailboxSession, AggregationSubscriptionType.All);
			foreach (AggregationSubscription aggregationSubscription in allSubscriptions)
			{
				PimAggregationSubscription pimAggregationSubscription = aggregationSubscription as PimAggregationSubscription;
				if (pimAggregationSubscription != null && this.IsSubscriptionEnabled(pimAggregationSubscription) && this.IsSendAsEnabled(pimAggregationSubscription))
				{
					if (subscription != null)
					{
						this.currentSyncLogSession.LogError((TSLID)52UL, SendAsManager.Tracer, "Found more than one enabled subscription", new object[0]);
						foundMultipleSubscriptions = true;
						subscription = null;
						break;
					}
					subscription = pimAggregationSubscription;
				}
			}
			if (subscription == null)
			{
				this.currentSyncLogSession.LogError((TSLID)53UL, SendAsManager.Tracer, "Did not find a unique send as subscription", new object[0]);
				return false;
			}
			this.currentSyncLogSession.LogVerbose((TSLID)54UL, SendAsManager.Tracer, "Found unique send as subscription: {0}", new object[]
			{
				subscription.SourceGuid
			});
			return true;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001A864 File Offset: 0x00018A64
		public bool IsValidSendAsMessage(ISendAsSource source, MessageItem messageItem)
		{
			if (source is TransactionalRequestJob)
			{
				return true;
			}
			PimAggregationSubscription pimAggregationSubscription = source as PimAggregationSubscription;
			SyncUtilities.ThrowIfArgumentNull("subscription", pimAggregationSubscription);
			SyncUtilities.ThrowIfArgumentNull("messageItem", messageItem);
			bool result = false;
			string text = SendAsManager.GetProperty(messageItem, ItemSchema.SentRepresentingType) as string;
			string text2 = SendAsManager.GetProperty(messageItem, ItemSchema.SentRepresentingEmailAddress) as string;
			string text3 = SendAsManager.GetProperty(messageItem, ItemSchema.SentRepresentingDisplayName) as string;
			if (string.Equals(text, "SMTP", StringComparison.OrdinalIgnoreCase) && string.Equals(text2, pimAggregationSubscription.UserEmailAddress.ToString(), StringComparison.OrdinalIgnoreCase) && string.Equals(text3, pimAggregationSubscription.UserDisplayName, StringComparison.OrdinalIgnoreCase))
			{
				result = true;
			}
			else
			{
				this.currentSyncLogSession.LogVerbose((TSLID)55UL, SendAsManager.Tracer, (long)this.GetHashCode(), "The sent representing properties on the message don't match the subscription.  Subscription id: {0}, type: {1}, email: {2}, display name: {3}", new object[]
				{
					pimAggregationSubscription.SubscriptionGuid,
					text,
					text2,
					text3
				});
			}
			return result;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001A994 File Offset: 0x00018B94
		public SendAsError MarkMessageForSendAs(MessageItem messageItem, Guid subscriptionGuid, MailboxSession mailboxSession)
		{
			SyncUtilities.ThrowIfGuidEmpty("subscriptionGuid", subscriptionGuid);
			SyncUtilities.ThrowIfArgumentNull("messageItem", messageItem);
			List<AggregationSubscription> allSubscriptions = this.GetAllSubscriptions(mailboxSession, AggregationSubscriptionType.AllEMail);
			PimAggregationSubscription subscription = null;
			int num = allSubscriptions.FindIndex(delegate(AggregationSubscription foundSubscription)
			{
				subscription = (foundSubscription as PimAggregationSubscription);
				return subscription != null && subscription.SubscriptionGuid == subscriptionGuid;
			});
			if (num == -1)
			{
				messageItem[MessageItemSchema.SharingInstanceGuid] = subscriptionGuid;
				return SendAsError.InvalidSubscriptionGuid;
			}
			bool flag = subscription.SendAsCapable && SubscriptionManager.IsValidForSendAs(subscription.SendAsState, subscription.Status);
			messageItem[MessageItemSchema.SharingInstanceGuid] = subscription.SubscriptionGuid;
			messageItem[ItemSchema.SentRepresentingType] = "smtp";
			messageItem[ItemSchema.SentRepresentingEmailAddress] = subscription.UserEmailAddress.ToString();
			messageItem[ItemSchema.SentRepresentingDisplayName] = subscription.UserDisplayName;
			if (!flag)
			{
				return SendAsError.SubscriptionDisabledForSendAs;
			}
			return SendAsError.Success;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0001AAA4 File Offset: 0x00018CA4
		public void SaveSubscriptionProperties(ISendAsSource source, IExtendedPropertyCollection properties)
		{
			SyncUtilities.ThrowIfArgumentNull("source", source);
			SyncUtilities.ThrowIfArgumentNull("properties", properties);
			PimAggregationSubscription pimAggregationSubscription = source as PimAggregationSubscription;
			TransactionalRequestJob transactionalRequestJob = source as TransactionalRequestJob;
			properties.SetValue<Guid>("Microsoft.Exchange.Transport.Sync.SendAs.SubscriptionGuid", source.SourceGuid);
			properties.SetValue<int>("Microsoft.Exchange.Transport.Sync.SendAs.SubscriptionType", (int)((pimAggregationSubscription != null) ? pimAggregationSubscription.SubscriptionType : ((AggregationSubscriptionType)1)));
			properties.SetValue<int>("Microsoft.Exchange.Transport.Sync.SendAs.SendAsState", (int)((pimAggregationSubscription != null) ? pimAggregationSubscription.SendAsState : SendAsState.Enabled));
			properties.SetValue<string>("Microsoft.Exchange.Transport.Sync.SendAs.SendAsValidatedEmail", (pimAggregationSubscription != null) ? pimAggregationSubscription.SendAsValidatedEmail : null);
			properties.SetValue<string>("Microsoft.Exchange.Transport.Sync.SendAs.UserDisplayName", (pimAggregationSubscription != null) ? pimAggregationSubscription.UserDisplayName : transactionalRequestJob.RemoteCredentialUsername);
			properties.SetValue<string>("Microsoft.Exchange.Transport.Sync.SendAs.UserEmailAddress", (pimAggregationSubscription != null) ? ((string)pimAggregationSubscription.UserEmailAddress) : ((string)transactionalRequestJob.EmailAddress));
			string value;
			string value2;
			this.GetVerifiedSubscriptionData(source, out value, out value2);
			properties.SetValue<bool>("Microsoft.Exchange.Transport.Sync.SendAs.Verified.IsVerifiedSubscription", pimAggregationSubscription != null && pimAggregationSubscription.SendAsNeedsVerification);
			properties.SetValue<string>("Microsoft.Exchange.Transport.Sync.SendAs.Verified.UserName", value);
			properties.SetValue<string>("Microsoft.Exchange.Transport.Sync.SendAs.Verified.IncomingServer", value2);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001ABA8 File Offset: 0x00018DA8
		public bool TryLoadSubscriptionProperties(IDictionary<string, object> properties, out SendAsManager.SendAsProperties sendAsProperties)
		{
			SyncUtilities.ThrowIfArgumentNull("properties", properties);
			sendAsProperties = SendAsManager.SendAsProperties.Invalid;
			object obj;
			object obj2;
			object obj3;
			object obj4;
			object obj5;
			object obj6;
			object obj7;
			object obj8;
			object obj9;
			if (properties.TryGetValue("Microsoft.Exchange.Transport.Sync.SendAs.SubscriptionGuid", out obj) && obj is Guid && properties.TryGetValue("Microsoft.Exchange.Transport.Sync.SendAs.SubscriptionType", out obj2) && obj2 is int && properties.TryGetValue("Microsoft.Exchange.Transport.Sync.SendAs.SendAsState", out obj3) && obj3 is int && properties.TryGetValue("Microsoft.Exchange.Transport.Sync.SendAs.SendAsValidatedEmail", out obj4) && obj4 is string && properties.TryGetValue("Microsoft.Exchange.Transport.Sync.SendAs.UserDisplayName", out obj5) && obj5 is string && properties.TryGetValue("Microsoft.Exchange.Transport.Sync.SendAs.UserEmailAddress", out obj6) && obj6 is string && properties.TryGetValue("Microsoft.Exchange.Transport.Sync.SendAs.Verified.UserName", out obj7) && obj7 is string && properties.TryGetValue("Microsoft.Exchange.Transport.Sync.SendAs.Verified.IncomingServer", out obj8) && obj8 is string && properties.TryGetValue("Microsoft.Exchange.Transport.Sync.SendAs.Verified.IsVerifiedSubscription", out obj9) && obj9 is bool)
			{
				this.SetUpSyncLogSessionWithContext((Guid)obj);
				this.TryLoadSubscriptionProperties((Guid)obj, (int)obj2, (int)obj3, (bool)obj9, (string)obj4, (string)obj5, (string)obj6, (string)obj7, (string)obj8, out sendAsProperties);
				return true;
			}
			return false;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0001AD1C File Offset: 0x00018F1C
		public void SetSendAsHeaders(HeaderList headers, string originalFromEmailAddress, string originalFromDisplayName, string subscriptionEmailAddress, string subscriptionDisplayName)
		{
			SyncUtilities.ThrowIfArgumentNull("headers", headers);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("originalFromEmailAddress", originalFromEmailAddress);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("subscriptionEmailAddress", subscriptionEmailAddress);
			MimeRecipient recipient = new MimeRecipient(originalFromDisplayName, originalFromEmailAddress);
			MimeRecipient recipient2 = new MimeRecipient(subscriptionDisplayName, subscriptionEmailAddress);
			this.SetAddressHeader(headers, HeaderId.From, recipient2);
			this.SetAddressHeader(headers, HeaderId.Sender, recipient);
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001AD70 File Offset: 0x00018F70
		public void UpdateSubscriptionWithDiagnostics(PimAggregationSubscription subscription, IEmailSender emailSender)
		{
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			SyncUtilities.ThrowIfArgumentNull("emailSender", emailSender);
			subscription.VerificationEmailState = this.GetVerificationEmailState(emailSender);
			if (subscription.VerificationEmailState == VerificationEmailState.EmailSent)
			{
				subscription.VerificationEmailMessageId = emailSender.MessageId;
			}
			if (subscription.VerificationEmailState != VerificationEmailState.EmailNotSent)
			{
				subscription.VerificationEmailTimeStamp = new DateTime?(DateTime.UtcNow);
			}
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001ADD0 File Offset: 0x00018FD0
		internal bool TryParseSignedHash(string hash, bool doesSubscriptionNeedVerification, out SendAsState sendAsState, out Guid subscriptionId, out Guid sharedSecret, out SmtpAddress email, out string verifiedSubscriptionUserName, out string verifiedSubscriptionIncomingServer)
		{
			sendAsState = SendAsState.None;
			subscriptionId = Guid.Empty;
			sharedSecret = Guid.Empty;
			email = SmtpAddress.Empty;
			verifiedSubscriptionUserName = string.Empty;
			verifiedSubscriptionIncomingServer = string.Empty;
			string decryptedHash;
			if (!this.TryDecryptSignedHash(hash, out decryptedHash))
			{
				return false;
			}
			if (doesSubscriptionNeedVerification)
			{
				string text;
				string text2;
				string text3;
				string text4;
				string text5;
				string text6;
				if (!SendAsManager.VerifiedSubscriptionHashUtility.TryParseHashContents(decryptedHash, out text, out text2, out text3, out text4, out text5, out text6, this.currentSyncLogSession))
				{
					return false;
				}
				if (this.TryParseSendAsState(text, out sendAsState) && this.TryParseGuid(text2, out subscriptionId) && this.TryParseGuid(text3, out sharedSecret) && this.TryParseSmtpAddress(text5, out email))
				{
					verifiedSubscriptionUserName = text4;
					verifiedSubscriptionIncomingServer = text6;
					return true;
				}
				this.currentSyncLogSession.LogVerbose((TSLID)56UL, SendAsManager.Tracer, (long)this.GetHashCode(), "Failed to parse individual components. RawSendAsState: {0} RawSubscriptionId: {1} RawSharedSecret: {2} RawEmailAddress: {3} RawUserName: {4} RawIncomingServer: {5}", new object[]
				{
					text,
					text2,
					text3,
					text5,
					text4,
					text6
				});
			}
			else
			{
				string text7;
				string text8;
				string text9;
				if (!SendAsManager.SubscriptionHashUtility.TryParseHashComponents(decryptedHash, out text7, out text8, out text9, this.currentSyncLogSession))
				{
					return false;
				}
				if (this.TryParseSendAsState(text7, out sendAsState) && this.TryParseGuid(text8, out subscriptionId) && this.TryParseSmtpAddress(text9, out email))
				{
					return true;
				}
				this.currentSyncLogSession.LogVerbose((TSLID)57UL, SendAsManager.Tracer, (long)this.GetHashCode(), "Failed to parse individual components. RawSendAsState: {0} RawSubscriptionId: {1} RawEmailAddress: {2}", new object[]
				{
					text7,
					text8,
					text9
				});
			}
			return false;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0001AF48 File Offset: 0x00019148
		protected virtual string CreateSignedHashFrom(string toEncode)
		{
			return this.exchangeGroupKeyObject.ClearStringToEncryptedString(toEncode);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0001AF56 File Offset: 0x00019156
		protected virtual Guid CreateSharedSecret()
		{
			return Guid.NewGuid();
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001AF60 File Offset: 0x00019160
		protected virtual bool TryDecryptSignedHash(string signedHash, out string decryptedHash)
		{
			decryptedHash = string.Empty;
			Exception ex = null;
			SecureString secureString = null;
			if (!this.exchangeGroupKeyObject.TryEncryptedStringToSecureString(signedHash, out secureString, out ex))
			{
				this.currentSyncLogSession.LogError((TSLID)58UL, SendAsManager.Tracer, (long)this.GetHashCode(), "Failed to Decrypt hash:{0}, error:{1}", new object[]
				{
					signedHash,
					ex
				});
				return false;
			}
			decryptedHash = SyncUtilities.SecureStringToString(secureString);
			return decryptedHash != null;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001AFD0 File Offset: 0x000191D0
		protected virtual bool TryLoadSubscription(MailboxSession session, Guid subscriptionId, out PimAggregationSubscription subscription)
		{
			try
			{
				subscription = (SubscriptionManager.GetSubscription(session, subscriptionId) as PimAggregationSubscription);
				if (subscription != null)
				{
					this.SetUpSyncLogSessionWithContext(session.MailboxGuid, subscription);
				}
			}
			catch (ObjectNotFoundException)
			{
				this.currentSyncLogSession.LogVerbose((TSLID)59UL, SendAsManager.Tracer, (long)this.GetHashCode(), "The subscription was not found.  Subscription id: {0}", new object[]
				{
					subscriptionId
				});
				subscription = null;
			}
			return subscription != null;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0001B054 File Offset: 0x00019254
		protected virtual bool TryLoadSyncJob(MailboxSession session, Guid subscriptionId, out TransactionalRequestJob requestJob)
		{
			Guid databaseGuid = session.MailboxOwner.MailboxInfo.GetDatabaseGuid();
			try
			{
				using (RequestJobProvider requestJobProvider = new RequestJobProvider(databaseGuid))
				{
					RequestJobObjectId identity = new RequestJobObjectId(subscriptionId, databaseGuid, null);
					requestJobProvider.AttachToMDB(databaseGuid);
					requestJob = (requestJobProvider.Read<TransactionalRequestJob>(identity) as TransactionalRequestJob);
				}
			}
			catch (NotEnoughInformationToFindMoveRequestPermanentException)
			{
				requestJob = null;
			}
			return requestJob != null;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0001B0D0 File Offset: 0x000192D0
		protected virtual List<AggregationSubscription> GetAllSubscriptions(MailboxSession session, AggregationSubscriptionType aggregationSubscriptionType)
		{
			SyncUtilities.ThrowIfArgumentNull("mailboxSession", session);
			return SubscriptionManager.GetAllSubscriptions(session, aggregationSubscriptionType);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001B0E4 File Offset: 0x000192E4
		private static object GetProperty(MessageItem messageItem, PropertyDefinition propertyDefinition)
		{
			try
			{
				return messageItem.TryGetProperty(propertyDefinition);
			}
			catch (StoragePermanentException)
			{
			}
			catch (StorageTransientException)
			{
			}
			return null;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001B120 File Offset: 0x00019320
		private string CreateSignedHash(PimAggregationSubscription subscription, out Guid generatedSharedSecret)
		{
			generatedSharedSecret = Guid.Empty;
			string toEncode;
			if (subscription.SendAsNeedsVerification)
			{
				string rawUserName;
				string rawIncomingServer;
				this.GetVerifiedSubscriptionData(subscription, out rawUserName, out rawIncomingServer);
				generatedSharedSecret = this.CreateSharedSecret();
				toEncode = SendAsManager.VerifiedSubscriptionHashUtility.MakeHashContents((int)subscription.SendAsState, subscription.SubscriptionGuid.ToString("N"), generatedSharedSecret.ToString("N"), rawUserName, subscription.UserEmailAddress.ToString(), rawIncomingServer, this.currentSyncLogSession);
			}
			else
			{
				toEncode = SendAsManager.SubscriptionHashUtility.MakeHashContents((int)subscription.SendAsState, subscription.SubscriptionGuid.ToString("N"), subscription.UserEmailAddress.ToString());
			}
			return this.CreateSignedHashFrom(toEncode);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001B1DC File Offset: 0x000193DC
		private VerificationEmailState GetVerificationEmailState(IEmailSender emailSender)
		{
			if (!emailSender.SendAttempted)
			{
				return VerificationEmailState.EmailNotSent;
			}
			if (emailSender.SendSuccessful)
			{
				return VerificationEmailState.EmailSent;
			}
			return VerificationEmailState.EmailFailedToSend;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0001B1F4 File Offset: 0x000193F4
		private bool TryLoadSubscriptionProperties(Guid subscriptionGuid, int subscriptionType, int sendAsState, bool isVerifiedSubscription, string sendAsValidatedEmail, string userDisplayName, string userEmailAddress, string verifiedSubscriptionUserName, string verifiedSubscriptionIncomingServer, out SendAsManager.SendAsProperties sendAsProperties)
		{
			SmtpAddress userEmailAddress2 = new SmtpAddress(userEmailAddress);
			if (!userEmailAddress2.IsValidAddress)
			{
				sendAsProperties = SendAsManager.SendAsProperties.Invalid;
				this.currentSyncLogSession.LogError((TSLID)60UL, SendAsManager.Tracer, (long)this.GetHashCode(), "This message has send as properties, but the email address is invalid.  Subscription guid: {0}", new object[]
				{
					subscriptionGuid
				});
				return false;
			}
			if (!this.IsSendAsEnabled(subscriptionGuid, userEmailAddress2, (SendAsState)sendAsState, isVerifiedSubscription, verifiedSubscriptionUserName, verifiedSubscriptionIncomingServer, sendAsValidatedEmail))
			{
				sendAsProperties = SendAsManager.SendAsProperties.Invalid;
				this.currentSyncLogSession.LogError((TSLID)61UL, SendAsManager.Tracer, (long)this.GetHashCode(), "This message has send as properties, but they are invalid. Subscription guid: {0}", new object[]
				{
					subscriptionGuid
				});
				return false;
			}
			sendAsProperties = new SendAsManager.SendAsProperties(subscriptionGuid, (AggregationSubscriptionType)subscriptionType, userDisplayName, userEmailAddress2);
			return true;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001B2C0 File Offset: 0x000194C0
		private bool TryParseGuid(string value, out Guid guid)
		{
			try
			{
				guid = new Guid(value);
				return true;
			}
			catch (FormatException)
			{
			}
			catch (OverflowException)
			{
			}
			guid = Guid.Empty;
			return false;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001B310 File Offset: 0x00019510
		private bool TryParseSmtpAddress(string value, out SmtpAddress smtpAddress)
		{
			smtpAddress = new SmtpAddress(value);
			return smtpAddress.IsValidAddress;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0001B324 File Offset: 0x00019524
		private bool TryParseSendAsState(string value, out SendAsState sendAsState)
		{
			sendAsState = SendAsState.None;
			int num;
			if (!int.TryParse(value, out num))
			{
				return false;
			}
			sendAsState = (SendAsState)num;
			return EnumValidator.IsValidValue<SendAsState>(sendAsState);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0001B34C File Offset: 0x0001954C
		private bool IsSendAsEnabled(Guid subscriptionGuid, SmtpAddress userEmailAddress, SendAsState sendAsState, bool isVerifiedSubscription, string verifiedSubscriptionUserName, string verifiedSubscriptionIncomingServer, string sendAsValidatedEmail)
		{
			if (sendAsState != SendAsState.Enabled)
			{
				this.currentSyncLogSession.LogVerbose((TSLID)62UL, SendAsManager.Tracer, (long)this.GetHashCode(), "Send as is not enabled on the subscription.  Subscription id: {0}", new object[]
				{
					subscriptionGuid
				});
				return false;
			}
			if (!this.IsSendAsValidatedEmailValid(sendAsValidatedEmail, sendAsState, isVerifiedSubscription, subscriptionGuid, userEmailAddress, verifiedSubscriptionUserName, verifiedSubscriptionIncomingServer))
			{
				this.currentSyncLogSession.LogVerbose((TSLID)63UL, SendAsManager.Tracer, (long)this.GetHashCode(), "Send as is marked as enabled, but the SendAsValidatedEmail property is invalid.  Subscription id: {0}, email address: {1}, actual SendAsValidatedEmail value: {2}", new object[]
				{
					subscriptionGuid,
					userEmailAddress,
					sendAsValidatedEmail
				});
				return false;
			}
			return true;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0001B3F0 File Offset: 0x000195F0
		private bool IsSendAsValidatedEmailValid(string sendAsValidatedEmail, SendAsState sendAsState, bool isVerifiedSubscription, Guid subscriptionGuid, SmtpAddress userEmailAddress, string verifiedSubscriptionUserName, string verifiedSubscriptionIncomingServer)
		{
			SendAsState sendAsState2;
			Guid a;
			Guid guid;
			SmtpAddress value;
			string a2;
			string a3;
			return this.TryParseSignedHash(sendAsValidatedEmail, isVerifiedSubscription, out sendAsState2, out a, out guid, out value, out a2, out a3) && (sendAsState2 == sendAsState && a == subscriptionGuid && value == userEmailAddress && a2 == verifiedSubscriptionUserName) && a3 == verifiedSubscriptionIncomingServer;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001B444 File Offset: 0x00019644
		private void GetVerifiedSubscriptionData(ISendAsSource source, out string verifiedSubscriptionUserName, out string verifiedSubscriptionIncomingServer)
		{
			PimAggregationSubscription pimAggregationSubscription = source as PimAggregationSubscription;
			TransactionalRequestJob transactionalRequestJob = source as TransactionalRequestJob;
			verifiedSubscriptionUserName = string.Empty;
			verifiedSubscriptionIncomingServer = string.Empty;
			if (pimAggregationSubscription != null && !pimAggregationSubscription.SendAsNeedsVerification)
			{
				this.currentSyncLogSession.LogVerbose((TSLID)64UL, SendAsManager.Tracer, (long)this.GetHashCode(), "Subscription does not need SendAs Verification.  No user name or password to retrieve.", new object[0]);
				return;
			}
			verifiedSubscriptionUserName = ((pimAggregationSubscription != null) ? pimAggregationSubscription.VerifiedUserName : transactionalRequestJob.RemoteCredentialUsername);
			verifiedSubscriptionIncomingServer = ((pimAggregationSubscription != null) ? pimAggregationSubscription.VerifiedIncomingServer : transactionalRequestJob.RemoteHostName);
			this.currentSyncLogSession.LogVerbose((TSLID)65UL, SendAsManager.Tracer, (long)this.GetHashCode(), "Retrieved verified data.  User name: {0} Incoming server: {1}", new object[]
			{
				verifiedSubscriptionUserName,
				verifiedSubscriptionIncomingServer
			});
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0001B500 File Offset: 0x00019700
		private void SetAddressHeader(HeaderList headerList, HeaderId headerId, MimeRecipient recipient)
		{
			AddressHeader addressHeader = headerList.FindFirst(headerId) as AddressHeader;
			if (addressHeader == null)
			{
				addressHeader = (AddressHeader)Header.Create(headerId);
				headerList.AppendChild(addressHeader);
			}
			addressHeader.RemoveAll();
			addressHeader.AppendChild(recipient);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001B53F File Offset: 0x0001973F
		private void SendVerificationEmail(PimAggregationSubscription subscription, Guid sharedSecret, IEmailSender emailSender)
		{
			emailSender.SendWith(sharedSecret);
			this.UpdateSubscriptionWithDiagnostics(subscription, emailSender);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0001B550 File Offset: 0x00019750
		private void SetUpSyncLogSessionWithContext(Guid mailboxGuid, PimAggregationSubscription subscription)
		{
			this.currentSyncLogSession = this.currentSyncLogSession.OpenWithContext(mailboxGuid, subscription);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0001B565 File Offset: 0x00019765
		private void SetUpSyncLogSessionWithContext(Guid subscriptionId)
		{
			this.currentSyncLogSession = this.currentSyncLogSession.OpenWithContext(subscriptionId);
		}

		// Token: 0x040002FC RID: 764
		private ExchangeGroupKey exchangeGroupKeyObject = new ExchangeGroupKey(null, "Microsoft Exchange DKM");

		// Token: 0x040002FD RID: 765
		private SyncLogSession currentSyncLogSession = CommonLoggingHelper.SyncLogSession;

		// Token: 0x020000BC RID: 188
		public struct SendAsProperties
		{
			// Token: 0x0600054E RID: 1358 RVA: 0x0001B59D File Offset: 0x0001979D
			public SendAsProperties(Guid subscriptionGuid, AggregationSubscriptionType subscriptionType, string userDisplayName, SmtpAddress userEmailAddress)
			{
				this.SubscriptionGuid = subscriptionGuid;
				this.SubscriptionType = subscriptionType;
				this.UserDisplayName = userDisplayName;
				this.UserEmailAddress = userEmailAddress;
			}

			// Token: 0x1700017E RID: 382
			// (get) Token: 0x0600054F RID: 1359 RVA: 0x0001B5BC File Offset: 0x000197BC
			public bool IsValid
			{
				get
				{
					return this.SubscriptionGuid != Guid.Empty;
				}
			}

			// Token: 0x040002FE RID: 766
			public static readonly SendAsManager.SendAsProperties Invalid = default(SendAsManager.SendAsProperties);

			// Token: 0x040002FF RID: 767
			public readonly Guid SubscriptionGuid;

			// Token: 0x04000300 RID: 768
			public readonly AggregationSubscriptionType SubscriptionType;

			// Token: 0x04000301 RID: 769
			public readonly string UserDisplayName;

			// Token: 0x04000302 RID: 770
			public readonly SmtpAddress UserEmailAddress;
		}

		// Token: 0x020000BD RID: 189
		private static class VerifiedSubscriptionHashUtility
		{
			// Token: 0x06000551 RID: 1361 RVA: 0x0001B5DC File Offset: 0x000197DC
			public static string MakeHashContents(int rawSendAsState, string rawSubscriptionId, string rawSharedSecret, string rawUserName, string rawEmailAddress, string rawIncomingServer, SyncLogSession syncLogSession)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0} {1} {2} {3} {4} {5}", new object[]
				{
					rawSendAsState,
					rawSubscriptionId,
					rawSharedSecret,
					HttpUtility.UrlEncode(rawUserName),
					HttpUtility.UrlEncode(rawEmailAddress),
					HttpUtility.UrlEncode(rawIncomingServer)
				});
			}

			// Token: 0x06000552 RID: 1362 RVA: 0x0001B630 File Offset: 0x00019830
			public static bool TryParseHashContents(string decryptedHash, out string rawSendAsState, out string rawSubscriptionId, out string rawSharedSecret, out string rawUserName, out string rawEmailAddress, out string rawIncomingServer, SyncLogSession syncLogSession)
			{
				string[] array = decryptedHash.Split(new char[]
				{
					' '
				});
				if (array.Length != 6)
				{
					syncLogSession.LogVerbose((TSLID)66UL, SendAsManager.Tracer, "Incorrect number of values in signed hash.  Expected: {0} Actual: {1}", new object[]
					{
						6,
						array.Length
					});
					rawSendAsState = string.Empty;
					rawSubscriptionId = string.Empty;
					rawSharedSecret = string.Empty;
					rawUserName = string.Empty;
					rawEmailAddress = string.Empty;
					rawIncomingServer = string.Empty;
					return false;
				}
				rawSendAsState = array[0];
				rawSubscriptionId = array[1];
				rawSharedSecret = array[2];
				rawUserName = HttpUtility.UrlDecode(array[3]);
				rawEmailAddress = HttpUtility.UrlDecode(array[4]);
				rawIncomingServer = HttpUtility.UrlDecode(array[5]);
				return true;
			}

			// Token: 0x04000303 RID: 771
			private const int VerifiedSubscriptionHashComponentCount = 6;
		}

		// Token: 0x020000BE RID: 190
		private static class SubscriptionHashUtility
		{
			// Token: 0x06000553 RID: 1363 RVA: 0x0001B6EC File Offset: 0x000198EC
			public static string MakeHashContents(int rawSendAsState, string rawSubscriptionId, string rawEmailAddress)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0} {1} {2}", new object[]
				{
					rawSendAsState,
					rawSubscriptionId,
					HttpUtility.UrlEncode(rawEmailAddress)
				});
			}

			// Token: 0x06000554 RID: 1364 RVA: 0x0001B728 File Offset: 0x00019928
			public static bool TryParseHashComponents(string decryptedHash, out string rawSendAsState, out string rawSubscriptionId, out string rawEmailAddress, SyncLogSession syncLogSession)
			{
				string[] array = decryptedHash.Split(new char[]
				{
					' '
				});
				if (array.Length != 3)
				{
					syncLogSession.LogVerbose((TSLID)67UL, SendAsManager.Tracer, "Incorrect number of values in signed hash.  Expected: {0} Actual: {1}", new object[]
					{
						3,
						array.Length
					});
					rawSendAsState = string.Empty;
					rawSubscriptionId = string.Empty;
					rawEmailAddress = string.Empty;
					return false;
				}
				rawSendAsState = array[0];
				rawSubscriptionId = array[1];
				rawEmailAddress = HttpUtility.UrlDecode(array[2]);
				return true;
			}

			// Token: 0x04000304 RID: 772
			private const int RegularSubscriptionHashComponentCount = 3;
		}

		// Token: 0x020000BF RID: 191
		private static class Constants
		{
			// Token: 0x04000305 RID: 773
			public const string SmtpAddressType = "SMTP";

			// Token: 0x020000C0 RID: 192
			public static class SubscriptionProperty
			{
				// Token: 0x04000306 RID: 774
				public const string Prefix = "Microsoft.Exchange.Transport.Sync.SendAs.";

				// Token: 0x04000307 RID: 775
				public const string VerifiedPrefix = "Microsoft.Exchange.Transport.Sync.SendAs.Verified.";

				// Token: 0x04000308 RID: 776
				public const string SubscriptionGuid = "Microsoft.Exchange.Transport.Sync.SendAs.SubscriptionGuid";

				// Token: 0x04000309 RID: 777
				public const string SubscriptionType = "Microsoft.Exchange.Transport.Sync.SendAs.SubscriptionType";

				// Token: 0x0400030A RID: 778
				public const string SendAsState = "Microsoft.Exchange.Transport.Sync.SendAs.SendAsState";

				// Token: 0x0400030B RID: 779
				public const string SendAsValidatedEmail = "Microsoft.Exchange.Transport.Sync.SendAs.SendAsValidatedEmail";

				// Token: 0x0400030C RID: 780
				public const string UserDisplayName = "Microsoft.Exchange.Transport.Sync.SendAs.UserDisplayName";

				// Token: 0x0400030D RID: 781
				public const string UserEmailAddress = "Microsoft.Exchange.Transport.Sync.SendAs.UserEmailAddress";

				// Token: 0x0400030E RID: 782
				public const string IsVerifiedSubscription = "Microsoft.Exchange.Transport.Sync.SendAs.Verified.IsVerifiedSubscription";

				// Token: 0x0400030F RID: 783
				public const string VerifiedSubscriptionUserName = "Microsoft.Exchange.Transport.Sync.SendAs.Verified.UserName";

				// Token: 0x04000310 RID: 784
				public const string VerifiedSubscriptionIncomingServer = "Microsoft.Exchange.Transport.Sync.SendAs.Verified.IncomingServer";
			}
		}
	}
}
