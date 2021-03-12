using System;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.UM.Rpc;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon.CrossServerMailboxAccess
{
	// Token: 0x02000087 RID: 135
	internal class EWSUMUserMailboxAccessor : DisposableBase, IUMUserMailboxStorage, IDisposeTrackable, IDisposable
	{
		// Token: 0x0600049B RID: 1179 RVA: 0x0001042C File Offset: 0x0000E62C
		public EWSUMUserMailboxAccessor(ExchangePrincipal userPrincipal, ADUser user)
		{
			EWSUMUserMailboxAccessor <>4__this = this;
			ValidateArgument.NotNull(user, "user");
			ValidateArgument.NotNull(userPrincipal, "userPrincipal");
			this.user = user;
			this.tracer = new DiagnosticHelper(this, ExTraceGlobals.XsoTracer);
			PIIMessage pii = PIIMessage.Create(PIIType._SmtpAddress, userPrincipal.MailboxInfo.PrimarySmtpAddress.ToString());
			this.tracer.Trace(pii, "EWSUMUserMailboxAccessor for user: _SmtpAddress", new object[0]);
			Exception e = UMMailboxAccessorEwsBinding.ExecuteEWSOperation(delegate
			{
				<>4__this.ewsBinding = new UMMailboxAccessorEwsBinding(userPrincipal, <>4__this.tracer);
				<>4__this.tracer.Trace("EWSUMUserMailboxAccessor, EWS Url = {0}", new object[]
				{
					<>4__this.ewsBinding.Url
				});
			}, this.tracer);
			this.CheckForErrors(e);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00010510 File Offset: 0x0000E710
		public void InitUMMailbox()
		{
			PIIMessage pii = PIIMessage.Create(PIIType._User, this.user);
			this.tracer.Trace(pii, "EWSUMUserMailboxAccessor : InitUMMailbox, for user _User", new object[0]);
			InitUMMailboxType request = new InitUMMailboxType();
			InitUMMailboxResponseMessageType response = null;
			Exception e = UMMailboxAccessorEwsBinding.ExecuteEWSOperation(delegate
			{
				response = this.ewsBinding.InitUMMailbox(request);
			}, this.tracer);
			this.CheckResponse(e, response);
			this.tracer.Trace(pii, "EWSUMUserMailboxAccessor :Sucessfully enabled UM mailbox for user _User ", new object[0]);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x000105C4 File Offset: 0x0000E7C4
		public void ResetUMMailbox(bool keepProperties)
		{
			this.tracer.Trace("EWSUMUserMailboxAccessor : ResetUMMailbox, for user {0} with Keepproperties = {1}", new object[]
			{
				this.user,
				keepProperties
			});
			ResetUMMailboxType request = new ResetUMMailboxType();
			request.KeepProperties = keepProperties;
			ResetUMMailboxResponseMessageType response = null;
			Exception e = UMMailboxAccessorEwsBinding.ExecuteEWSOperation(delegate
			{
				response = this.ewsBinding.ResetUMMailbox(request);
			}, this.tracer);
			this.CheckResponse(e, response);
			PIIMessage pii = PIIMessage.Create(PIIType._User, this.user);
			this.tracer.Trace(pii, "EWSUMUserMailboxAccessor :Sucessfully Reset UM mailbox for user _User ", new object[0]);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00010698 File Offset: 0x0000E898
		public PINInfo ValidateUMPin(string pin, Guid userUMMailboxPolicyGuid)
		{
			PIIMessage pii = PIIMessage.Create(PIIType._User, this.user);
			this.tracer.Trace(pii, "EWSUMUserMailboxAccessor : ValidateUMPin, for user _User", new object[0]);
			ValidateUMPinType request = new ValidateUMPinType();
			request.PinInfo = new PinInfoType();
			request.PinInfo.PIN = pin;
			request.UserUMMailboxPolicyGuid = userUMMailboxPolicyGuid.ToString();
			ValidateUMPinResponseMessageType response = null;
			Exception e = UMMailboxAccessorEwsBinding.ExecuteEWSOperation(delegate
			{
				response = this.ewsBinding.ValidateUMPin(request);
			}, this.tracer);
			this.CheckResponse(e, response);
			this.tracer.Trace(pii, "EWSUMUserMailboxAccessor :Sucessfully Validated or generated the pin for user _User ", new object[0]);
			PinInfoType pinInfo = response.PinInfo;
			return new PINInfo
			{
				PIN = pinInfo.PIN,
				IsValid = pinInfo.IsValid,
				PinExpired = pinInfo.PinExpired,
				LockedOut = pinInfo.LockedOut,
				FirstTimeUser = pinInfo.FirstTimeUser
			};
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x000107DC File Offset: 0x0000E9DC
		public void SaveUMPin(PINInfo pin, Guid userUMMailboxPolicyGuid)
		{
			PIIMessage pii = PIIMessage.Create(PIIType._User, this.user);
			this.tracer.Trace(pii, "EWSUMUserMailboxAccessor : SaveUMPin, for user _User", new object[0]);
			SaveUMPinType request = new SaveUMPinType();
			request.PinInfo = new PinInfoType
			{
				PIN = pin.PIN,
				IsValid = pin.IsValid,
				PinExpired = pin.PinExpired,
				LockedOut = pin.LockedOut,
				FirstTimeUser = pin.FirstTimeUser
			};
			request.UserUMMailboxPolicyGuid = userUMMailboxPolicyGuid.ToString();
			SaveUMPinResponseMessageType response = null;
			Exception e = UMMailboxAccessorEwsBinding.ExecuteEWSOperation(delegate
			{
				response = this.ewsBinding.SaveUMPin(request);
			}, this.tracer);
			this.CheckResponse(e, response);
			this.tracer.Trace(pii, "EWSUMUserMailboxAccessor :Sucessfully Save the UM mailbox Pin for user _User ", new object[0]);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x000108F4 File Offset: 0x0000EAF4
		public PINInfo GetUMPin()
		{
			PIIMessage pii = PIIMessage.Create(PIIType._User, this.user);
			this.tracer.Trace(pii, "EWSUMUserMailboxAccessor : GetUMPin, for user _User", new object[0]);
			GetUMPinType request = new GetUMPinType();
			GetUMPinResponseMessageType response = null;
			Exception e = UMMailboxAccessorEwsBinding.ExecuteEWSOperation(delegate
			{
				response = this.ewsBinding.GetUMPin(request);
			}, this.tracer);
			this.CheckResponse(e, response);
			this.tracer.Trace(pii, "EWSUMUserMailboxAccessor :Sucessfully retrieved the pin for user _User", new object[0]);
			PinInfoType pinInfo = response.PinInfo;
			return new PINInfo
			{
				PIN = pinInfo.PIN,
				IsValid = pinInfo.IsValid,
				PinExpired = pinInfo.PinExpired,
				LockedOut = pinInfo.LockedOut,
				FirstTimeUser = pinInfo.FirstTimeUser
			};
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x000109FC File Offset: 0x0000EBFC
		public void SendEmail(string recipientMailAddress, string messageSubject, string messageBody)
		{
			ValidateArgument.NotNullOrEmpty(recipientMailAddress, "recipientMailAddress");
			if (!SmtpAddress.IsValidSmtpAddress(recipientMailAddress))
			{
				throw new InvalidArgumentException("Recipient Email address is not a valid SMTP address.");
			}
			ValidateArgument.NotNullOrEmpty(messageSubject, "messageSubject");
			ValidateArgument.NotNullOrEmpty(messageBody, "messageBody");
			CreateItemResponseType response = null;
			CreateItemType request = this.GetCreateItemRequestType(recipientMailAddress, messageSubject, messageBody);
			Exception e = UMMailboxAccessorEwsBinding.ExecuteEWSOperation(delegate
			{
				response = this.ewsBinding.CreateItem(request);
			}, this.tracer);
			if (response != null && response.ResponseMessages != null && response.ResponseMessages.Items != null && response.ResponseMessages.Items.Length > 0)
			{
				this.CheckResponse(e, response.ResponseMessages.Items[0]);
			}
			else
			{
				this.CheckResponse(e, null);
			}
			PIIMessage pii = PIIMessage.Create(PIIType._User, this.user);
			this.tracer.Trace(pii, "EWSUMUserMailboxAccessor :Sucessfully sent mail for the user _User", new object[0]);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00010B24 File Offset: 0x0000ED24
		public UMSubscriberCallAnsweringData GetUMSubscriberCallAnsweringData(UMSubscriber subscriber, TimeSpan timeout)
		{
			PIIMessage pii = PIIMessage.Create(PIIType._User, this.user);
			this.tracer.Trace(pii, "EWSUMUserMailboxAccessor : GetUMSubscriberCallAnsweringData, for user _User", new object[0]);
			ValidateArgument.NotNull(subscriber, "subscriber");
			ValidateArgument.NotNull(timeout, "timeout");
			GetUMSubscriberCallAnsweringDataType request = new GetUMSubscriberCallAnsweringDataType();
			request.Timeout = XmlConvert.ToString(timeout);
			GetUMSubscriberCallAnsweringDataResponseMessageType response = null;
			Exception e = UMMailboxAccessorEwsBinding.ExecuteEWSOperation(delegate
			{
				response = this.ewsBinding.GetUMSubscriberCallAnsweringData(request);
			}, this.tracer);
			this.CheckResponse(e, response);
			this.tracer.Trace(pii, "EWSUMUserMailboxAccessor :Sucessfully retrieved the Call Answering Data for user _User", new object[0]);
			TranscriptionEnabledSetting isTranscriptionEnabledInMailboxConfig;
			Enum.TryParse<TranscriptionEnabledSetting>(response.IsTranscriptionEnabledInMailboxConfig.ToString(), true, out isTranscriptionEnabledInMailboxConfig);
			return new UMSubscriberCallAnsweringData(response.Greeting, response.GreetingName, response.IsOOF, isTranscriptionEnabledInMailboxConfig, response.IsMailboxQuotaExceeded, response.TaskTimedOut);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00010C6C File Offset: 0x0000EE6C
		public PersonaType GetPersonaFromEmail(string emailAddress)
		{
			PIIMessage pii = PIIMessage.Create(PIIType._EmailAddress, emailAddress);
			this.tracer.Trace(pii, "EWSUMUserMailboxAccessor : GetPersonaFromEmail, for user _EmailAddress", new object[]
			{
				emailAddress
			});
			ValidateArgument.NotNullOrEmpty(emailAddress, "emailAddress");
			if (!SmtpAddress.IsValidSmtpAddress(emailAddress))
			{
				throw new InvalidSmtpAddressException(emailAddress);
			}
			GetPersonaType request = new GetPersonaType();
			request.EmailAddress = new EmailAddressType();
			request.EmailAddress.EmailAddress = emailAddress;
			request.EmailAddress.MailboxType = MailboxTypeType.Mailbox;
			request.EmailAddress.MailboxTypeSpecified = true;
			GetPersonaResponseMessageType response = null;
			Exception e = UMMailboxAccessorEwsBinding.ExecuteEWSOperation(delegate
			{
				response = this.ewsBinding.GetPersona(request);
			}, this.tracer);
			if (response == null)
			{
				return null;
			}
			this.CheckResponse(e, response);
			this.tracer.Trace(pii, "EWSUMUserMailboxAccessor :Sucessfully retrieved the persona for email address _EmailAddress", new object[0]);
			return response.Persona;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00010D6C File Offset: 0x0000EF6C
		internal void CheckResponse(Exception e, ResponseMessageType response)
		{
			this.CheckForErrors(e);
			if (response == null)
			{
				this.tracer.Trace("EWSUMUserMailboxAccessor : CheckResponse, response == null", new object[0]);
				throw new EWSUMMailboxAccessException(Strings.EWSNoResponseReceived);
			}
			this.tracer.Trace("EWSUMUserMailboxAccessor : CheckResponse, ResponseCode = {0}, ResponseClass = {1}, MessageText = {2}", new object[]
			{
				response.ResponseCode,
				response.ResponseClass,
				response.MessageText
			});
			if (response.ResponseClass != ResponseClassType.Success || response.ResponseCode != ResponseCodeType.NoError)
			{
				ResponseCodeType responseCode = response.ResponseCode;
				if (responseCode <= ResponseCodeType.ErrorMailboxConfiguration)
				{
					if (responseCode <= ResponseCodeType.ErrorExceededConnectionCount)
					{
						if (responseCode != ResponseCodeType.ErrorConnectionFailed && responseCode != ResponseCodeType.ErrorExceededConnectionCount)
						{
							goto IL_171;
						}
					}
					else
					{
						switch (responseCode)
						{
						case ResponseCodeType.ErrorInsufficientResources:
						case ResponseCodeType.ErrorInternalServerTransientError:
							break;
						case ResponseCodeType.ErrorInternalServerError:
							goto IL_171;
						default:
							if (responseCode != ResponseCodeType.ErrorMailboxConfiguration)
							{
								goto IL_171;
							}
							goto IL_14B;
						}
					}
				}
				else if (responseCode <= ResponseCodeType.ErrorQuotaExceeded)
				{
					switch (responseCode)
					{
					case ResponseCodeType.ErrorMailboxMoveInProgress:
					case ResponseCodeType.ErrorMailboxStoreUnavailable:
						break;
					default:
						if (responseCode != ResponseCodeType.ErrorQuotaExceeded)
						{
							goto IL_171;
						}
						throw new QuotaExceededException(Strings.UMMailboxOperationQuotaExceededError(response.MessageText));
					}
				}
				else
				{
					if (responseCode == ResponseCodeType.ErrorSendAsDenied)
					{
						throw new UMMailboxOperationException(Strings.UMMailboxOperationSendEmailError(response.MessageText));
					}
					if (responseCode != ResponseCodeType.ErrorServerBusy)
					{
						if (responseCode != ResponseCodeType.ErrorRecipientNotFound)
						{
							goto IL_171;
						}
						goto IL_14B;
					}
				}
				throw new UMMailboxOperationException(Strings.UMMailboxOperationTransientError(response.MessageText));
				IL_14B:
				throw new EWSUMMailboxAccessException(Strings.EWSOperationFailed(response.ResponseCode.ToString(), response.MessageText));
				IL_171:
				throw new EWSUMMailboxAccessException(Strings.EWSOperationFailed(response.ResponseCode.ToString(), response.MessageText));
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00010F10 File Offset: 0x0000F110
		private CreateItemType GetCreateItemRequestType(string recipientMailAddress, string messageSubject, string messageBody)
		{
			IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(this.user.OrganizationId);
			MicrosoftExchangeRecipient microsoftExchangeRecipient = iadsystemConfigurationLookup.GetMicrosoftExchangeRecipient();
			SingleRecipientType singleRecipientType = new SingleRecipientType();
			singleRecipientType.Item = new EmailAddressType();
			singleRecipientType.Item.EmailAddress = microsoftExchangeRecipient.LegacyExchangeDN;
			EmailAddressType emailAddressType = new EmailAddressType();
			emailAddressType.EmailAddress = recipientMailAddress;
			return new CreateItemType
			{
				MessageDisposition = MessageDispositionType.SendOnly,
				MessageDispositionSpecified = true,
				Items = new NonEmptyArrayOfAllItemsType
				{
					Items = new ItemType[]
					{
						new MessageType
						{
							ToRecipients = new EmailAddressType[]
							{
								emailAddressType
							},
							From = singleRecipientType,
							ItemClass = "IPM.Note",
							Subject = messageSubject,
							Body = new BodyType
							{
								Value = messageBody,
								BodyType1 = BodyTypeType.HTML
							}
						}
					}
				}
			};
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00011001 File Offset: 0x0000F201
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.tracer.Trace("EWSUMUserMailboxAccessor : InternalDispose", new object[0]);
				if (this.ewsBinding != null)
				{
					this.ewsBinding.Dispose();
					this.ewsBinding = null;
				}
			}
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00011036 File Offset: 0x0000F236
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<EWSUMUserMailboxAccessor>(this);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00011040 File Offset: 0x0000F240
		private void CheckForErrors(Exception e)
		{
			if (e != null)
			{
				this.tracer.Trace("EWSUMUserMailboxAccessor : CheckForErrors, Exception: {0}", new object[]
				{
					e
				});
				throw new UMMailboxOperationException(e.Message, e);
			}
		}

		// Token: 0x040002FD RID: 765
		private UMMailboxAccessorEwsBinding ewsBinding;

		// Token: 0x040002FE RID: 766
		private DiagnosticHelper tracer;

		// Token: 0x040002FF RID: 767
		private ADUser user;
	}
}
