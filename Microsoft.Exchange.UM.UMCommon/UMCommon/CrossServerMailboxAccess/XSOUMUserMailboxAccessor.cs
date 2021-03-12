using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.UM.Rpc;

namespace Microsoft.Exchange.UM.UMCommon.CrossServerMailboxAccess
{
	// Token: 0x0200008D RID: 141
	internal class XSOUMUserMailboxAccessor : DisposableBase, IUMUserMailboxStorage, IDisposeTrackable, IDisposable
	{
		// Token: 0x060004F7 RID: 1271 RVA: 0x000130CC File Offset: 0x000112CC
		public XSOUMUserMailboxAccessor(ExchangePrincipal mailboxPrincipal, ADUser user)
		{
			ValidateArgument.NotNull(user, "user");
			ValidateArgument.NotNull(mailboxPrincipal, "mailboxPrincipal");
			this.user = user;
			this.mailboxPrincipal = mailboxPrincipal;
			this.tracer = new DiagnosticHelper(this, ExTraceGlobals.XsoTracer);
			this.ExecuteXSOOperation(delegate
			{
				this.Initialize(this.CreateMailboxSession("Client=UM;Action=Manage-UMMailbox"));
			});
			this.disposeMailboxSession = true;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00013134 File Offset: 0x00011334
		public XSOUMUserMailboxAccessor(ADUser user, MailboxSession mailboxSession)
		{
			ValidateArgument.NotNull(mailboxSession, "mailboxSession");
			ValidateArgument.NotNull(user, "user");
			this.tracer = new DiagnosticHelper(this, ExTraceGlobals.XsoTracer);
			this.user = user;
			this.Initialize(mailboxSession);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x000131B8 File Offset: 0x000113B8
		public void InitUMMailbox()
		{
			PIIMessage piiUser = PIIMessage.Create(PIIType._User, this.user);
			this.tracer.Trace(piiUser, "XSOUMUserMailboxAccessor : Initialize UM mailbox for user _User", new object[0]);
			this.ExecuteXSOOperation(delegate
			{
				Utils.InitUMMailbox(this.mailboxSession, this.user);
				this.tracer.Trace(piiUser, "XSOUMUserMailboxAccessor : InitUMMailbox, Successfully initialized mailbox for user _user", new object[0]);
			});
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001326C File Offset: 0x0001146C
		public void ResetUMMailbox(bool keepProperties)
		{
			PIIMessage piiUser = PIIMessage.Create(PIIType._User, this.user);
			this.tracer.Trace(piiUser, "XSOUMUserMailboxAccessor : Reset UM mailbox for user _User", new object[0]);
			this.ExecuteXSOOperation(delegate
			{
				Utils.ResetUMMailbox(this.user, keepProperties, this.mailboxSession);
				this.tracer.Trace(piiUser, "XSOUMUserMailboxAccessor : ResetUMailbox, Successfully reset mailbox for user _User", new object[0]);
			});
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001337C File Offset: 0x0001157C
		public PINInfo ValidateUMPin(string pin, Guid userUMMailboxPolicyGuid)
		{
			PIIMessage piiUser = PIIMessage.Create(PIIType._User, this.user);
			this.tracer.Trace(piiUser, "XSOUMUserMailboxAccessor : Validate Pin for user _User", new object[0]);
			PINInfo resultPinInfo = null;
			this.ExecuteXSOOperation(delegate
			{
				if (userUMMailboxPolicyGuid != Guid.Empty)
				{
					resultPinInfo = Utils.ValidateOrGeneratePIN(this.user, pin, this.mailboxSession, this.GetUMMailboxPolicy(userUMMailboxPolicyGuid));
				}
				else
				{
					resultPinInfo = Utils.ValidateOrGeneratePIN(this.user, pin, this.mailboxSession);
				}
				this.tracer.Trace(piiUser, "XSOUMUserMailboxAccessor : Validate Pin, Successfully validated or generated Pin for user _User", new object[0]);
			});
			return resultPinInfo;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x000134C8 File Offset: 0x000116C8
		public void SaveUMPin(PINInfo pin, Guid userUMMailboxPolicyGuid)
		{
			ValidateArgument.NotNull(pin, "Pin");
			PIIMessage piiUser = PIIMessage.Create(PIIType._User, this.user);
			this.tracer.Trace(piiUser, "XSOUMUserMailboxAccessor : Save Pin for user _User", new object[0]);
			this.ExecuteXSOOperation(delegate
			{
				if (userUMMailboxPolicyGuid != Guid.Empty)
				{
					Utils.SetUserPassword(this.mailboxSession, this.GetUMMailboxPolicy(userUMMailboxPolicyGuid), this.user, pin.PIN, pin.PinExpired, pin.LockedOut);
				}
				else
				{
					Utils.SetUserPassword(this.mailboxSession, this.user, pin.PIN, pin.PinExpired, pin.LockedOut);
				}
				this.tracer.Trace(piiUser, "XSOUMUserMailboxAccessor : SaveUMPin, Successfully saved UM Pin for user _User", new object[0]);
			});
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00013598 File Offset: 0x00011798
		public PINInfo GetUMPin()
		{
			PIIMessage piiUser = PIIMessage.Create(PIIType._User, this.user);
			this.tracer.Trace(piiUser, "XSOUMUserMailboxAccessor : GetUMPin for user _User", new object[0]);
			PINInfo resultPinInfo = null;
			this.ExecuteXSOOperation(delegate
			{
				resultPinInfo = Utils.GetPINInfo(this.user, this.mailboxSession);
				this.tracer.Trace(piiUser, "XSOUMUserMailboxAccessor : GetUMPin, Successfully retrieved Pin for user _User", new object[0]);
			});
			return resultPinInfo;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00013678 File Offset: 0x00011878
		public void SendEmail(string recipientMailAddress, string messageSubject, string messageBody)
		{
			PIIMessage piiUser = PIIMessage.Create(PIIType._User, this.user);
			this.tracer.Trace(piiUser, "XSOUMUserMailboxAccessor : Send notify email for user _User", new object[0]);
			ValidateArgument.NotNullOrEmpty(recipientMailAddress, "recipientMailAddress");
			if (!SmtpAddress.IsValidSmtpAddress(recipientMailAddress))
			{
				throw new InvalidArgumentException("Recipient Email address is not a valid SMTP address.");
			}
			ValidateArgument.NotNullOrEmpty(messageSubject, "messageSubject");
			ValidateArgument.NotNullOrEmpty(messageBody, "messageBody");
			this.ExecuteXSOOperation(delegate
			{
				using (MessageItem messageItem = this.ConstructNotifyMail(recipientMailAddress, messageSubject, messageBody))
				{
					messageItem.SendWithoutSavingMessage();
					this.tracer.Trace(piiUser, "XSOUMUserMailboxAccessor : Notify email successfully sent for user _User", new object[0]);
				}
			});
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x000137E0 File Offset: 0x000119E0
		public UMSubscriberCallAnsweringData GetUMSubscriberCallAnsweringData(UMSubscriber subscriber, TimeSpan timeout)
		{
			PIIMessage pii = PIIMessage.Create(PIIType._User, this.user);
			this.tracer.Trace(pii, "XSOUMUserMailboxAccessor : GetUMSubscriberCallAnsweringData for user _User", new object[0]);
			ValidateArgument.NotNull(subscriber, "subscriber");
			ValidateArgument.NotNull(timeout, "timeout");
			Stopwatch elapsedTime = new Stopwatch();
			elapsedTime.Start();
			UMSubscriberCallAnsweringData subscriberData = new UMSubscriberCallAnsweringData();
			this.ExecuteXSOOperation(delegate
			{
				subscriberData.IsOOF = subscriber.IsOOF();
				subscriberData.IsTranscriptionEnabledInMailboxConfig = subscriber.IsTranscriptionEnabledInMailboxConfig(VoiceMailTypeEnum.ReceivedVoiceMails);
				subscriberData.Greeting = subscriber.GetGreeting();
				if (elapsedTime.Elapsed > timeout)
				{
					subscriberData.TaskTimedOut = true;
					this.tracer.Trace("XSOUMUserMailboxAccessor : UMSubscriberCallAnsweringData timed out before checking IsMailboxQuotaExceeded for user _User", new object[0]);
					return;
				}
				subscriberData.IsMailboxQuotaExceeded = subscriber.IsMailboxQuotaExceeded();
			});
			elapsedTime.Stop();
			this.tracer.Trace(pii, "XSOUMUserMailboxAccessor : UMSubscriberCallAnsweringData succeeded for user _User", new object[0]);
			return subscriberData;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x000138AC File Offset: 0x00011AAC
		public UMSubscriberCallAnsweringData GetUMSubscriberCallAnsweringData(TimeSpan timeout)
		{
			ValidateArgument.NotNull(timeout, "timeout");
			UMSubscriberCallAnsweringData umsubscriberCallAnsweringData;
			using (UMSubscriber umsubscriber = new UMSubscriber(this.user, this.mailboxSession))
			{
				umsubscriberCallAnsweringData = this.GetUMSubscriberCallAnsweringData(umsubscriber, timeout);
			}
			return umsubscriberCallAnsweringData;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00013904 File Offset: 0x00011B04
		public PersonaType GetPersonaFromEmail(string emailAddress)
		{
			PIIMessage pii = PIIMessage.Create(PIIType._EmailAddress, this.user);
			this.tracer.Trace(pii, "XSOUMUserMailboxAccessor : GetPersonaFromEmail, for user _EmailAddress", new object[0]);
			if (InterServerMailboxAccessor.TestXSOHook)
			{
				return new PersonaType
				{
					EmailAddresses = new EmailAddressType[]
					{
						new EmailAddressType
						{
							EmailAddress = emailAddress
						}
					}
				};
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0001396C File Offset: 0x00011B6C
		internal UMMailboxPolicy GetUMMailboxPolicy(Guid mbxPolicyGuid)
		{
			ADObjectId mbxPolicyId = new ADObjectId(mbxPolicyGuid);
			IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromADRecipient(this.user);
			UMMailboxPolicy ummailboxPolicyFromId = iadsystemConfigurationLookup.GetUMMailboxPolicyFromId(mbxPolicyId);
			if (ummailboxPolicyFromId == null)
			{
				throw new UMMbxPolicyNotFoundException(mbxPolicyGuid.ToString(), this.user.PrimarySmtpAddress.ToString());
			}
			return ummailboxPolicyFromId;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x000139C4 File Offset: 0x00011BC4
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.tracer.Trace("XSOUMUserMailboxAccessor : InternalDispose", new object[0]);
				if (this.mailboxSession != null && this.disposeMailboxSession)
				{
					this.mailboxSession.Dispose();
				}
			}
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x000139FA File Offset: 0x00011BFA
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<XSOUMUserMailboxAccessor>(this);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00013A04 File Offset: 0x00011C04
		private void Initialize(MailboxSession session)
		{
			ExAssert.RetailAssert(session != null, "MailboxSession cannot be null");
			this.mailboxSession = session;
			this.tracer.Trace("XSOUMCallDataRecordAccessor called from WebServices : {1}", new object[]
			{
				!this.disposeMailboxSession
			});
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00013A54 File Offset: 0x00011C54
		private void ExecuteXSOOperation(Action function)
		{
			try
			{
				function();
			}
			catch (Exception ex)
			{
				CallIdTracer.TraceError(ExTraceGlobals.UtilTracer, this, ex.ToString(), new object[0]);
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMMailboxCmdletError, null, new object[]
				{
					this.user.LegacyExchangeDN,
					CommonUtil.ToEventLogString(ex)
				});
				throw;
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00013AC4 File Offset: 0x00011CC4
		private MessageItem ConstructNotifyMail(string recipientMailAddress, string messageSubject, string messageBody)
		{
			IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(this.user.OrganizationId);
			MicrosoftExchangeRecipient microsoftExchangeRecipient = iadsystemConfigurationLookup.GetMicrosoftExchangeRecipient();
			MessageItem messageItem = MessageItem.Create(this.mailboxSession, XsoUtil.GetDraftsFolderId(this.mailboxSession));
			IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromOrganizationId(this.user.OrganizationId, null);
			ADRecipient adrecipient = iadrecipientLookup.LookupBySmtpAddress(recipientMailAddress);
			if (adrecipient != null)
			{
				messageItem.Recipients.Add(new Participant(adrecipient.DisplayName, adrecipient.LegacyExchangeDN, "EX"), RecipientItemType.To);
			}
			else
			{
				messageItem.Recipients.Add(new Participant(recipientMailAddress, recipientMailAddress, "SMTP"), RecipientItemType.To);
			}
			messageItem.From = new Participant(microsoftExchangeRecipient);
			messageItem.AutoResponseSuppress = AutoResponseSuppress.All;
			messageItem.Subject = messageSubject;
			using (TextWriter textWriter = messageItem.Body.OpenTextWriter(BodyFormat.TextHtml))
			{
				textWriter.Write(messageBody);
			}
			return messageItem;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00013BB0 File Offset: 0x00011DB0
		private MailboxSession CreateMailboxSession(string clientString)
		{
			return MailboxSessionEstablisher.OpenAsAdmin(this.mailboxPrincipal, CultureInfo.InvariantCulture, clientString);
		}

		// Token: 0x04000318 RID: 792
		private readonly bool disposeMailboxSession;

		// Token: 0x04000319 RID: 793
		private MailboxSession mailboxSession;

		// Token: 0x0400031A RID: 794
		private DiagnosticHelper tracer;

		// Token: 0x0400031B RID: 795
		private ADUser user;

		// Token: 0x0400031C RID: 796
		private ExchangePrincipal mailboxPrincipal;
	}
}
