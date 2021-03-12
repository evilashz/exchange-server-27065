﻿using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.VersionedXml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.TextMessaging.MobileDriver;
using Microsoft.Exchange.TextMessaging.MobileDriver.Resources;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004B0 RID: 1200
	[OwaEventNamespace("EditSms")]
	internal sealed class EditSmsEventHandler : MessageEventHandler
	{
		// Token: 0x06002DFF RID: 11775 RVA: 0x001056E4 File Offset: 0x001038E4
		public EditSmsEventHandler()
		{
			base.AnrOptions.DefaultRoutingType = "MOBILE";
			base.AnrOptions.OnlyAllowDefaultRoutingType = true;
			base.AnrOptions.RecipientBlockType = RecipientBlockType.DL;
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x00105714 File Offset: 0x00103914
		[OwaEventParameter("Body", typeof(string), false, true)]
		[OwaEventParameter("Recips", typeof(RecipientInfoAC), true, true)]
		[OwaEventParameter("Text", typeof(bool), false, true)]
		[OwaEvent("Save")]
		[OwaEventParameter("CK", typeof(string), false, true)]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("To", typeof(RecipientInfo), true, true)]
		public void Save()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "EditSmsEventHandler.Save");
			this.ProcessMessageRequest(MessageAction.Save);
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x00105733 File Offset: 0x00103933
		[OwaEventParameter("Id", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("Body", typeof(string), false, true)]
		[OwaEventParameter("Text", typeof(bool), false, true)]
		[OwaEvent("Send")]
		[OwaEventParameter("To", typeof(RecipientInfo), true, true)]
		[OwaEventParameter("Recips", typeof(RecipientInfoAC), true, true)]
		[OwaEventParameter("CK", typeof(string), false, true)]
		public void Send()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "EditSmsEventHandler.Send");
			this.ProcessMessageRequest(MessageAction.Send);
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x00105754 File Offset: 0x00103954
		private void ProcessMessageRequest(MessageAction action)
		{
			MessageItem messageItem = null;
			bool flag = base.IsParameterSet("Id");
			RecipientInfoAC[] array = (RecipientInfoAC[])base.GetParameter("Recips");
			if (array != null && array.Length != 0)
			{
				AutoCompleteCache.UpdateAutoCompleteCacheFromRecipientInfoList(array, base.UserContext);
			}
			try
			{
				if (flag)
				{
					messageItem = base.GetRequestItem<MessageItem>(new PropertyDefinition[0]);
				}
				else
				{
					ExTraceGlobals.MailTracer.TraceDebug((long)this.GetHashCode(), "ItemId wasn't set in the request. Creating new message in the drafts folder.");
					messageItem = MessageItem.Create(base.UserContext.MailboxSession, base.UserContext.DraftsFolderId);
					messageItem[ItemSchema.ConversationIndexTracking] = true;
					messageItem.ClassName = "IPM.Note.Mobile.SMS";
					if (Globals.ArePerfCountersEnabled)
					{
						OwaSingleCounters.ItemsCreated.Increment();
					}
				}
				bool flag2 = base.UpdateMessage(messageItem, StoreObjectType.Message);
				if (base.IsParameterSet("Body"))
				{
					string text = (string)base.GetParameter("Body");
					messageItem.Subject = ((text.Length > 160) ? text.Substring(0, 160) : text).Replace("\r\n", " ");
				}
				if (action == MessageAction.Save)
				{
					ExTraceGlobals.MailTracer.TraceDebug((long)this.GetHashCode(), "Saving message");
					Utilities.SaveItem(messageItem, flag);
					messageItem.Load();
					if (!flag)
					{
						if (ExTraceGlobals.MailDataTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.MailDataTracer.TraceDebug<string>((long)this.GetHashCode(), "New message ID is '{0}'", messageItem.Id.ObjectId.ToBase64String());
						}
						base.WriteNewItemId(messageItem);
					}
					base.WriteChangeKey(messageItem);
				}
				else if (action == MessageAction.Send)
				{
					if (flag2)
					{
						throw new OwaEventHandlerException("Unresolved recipients", LocalizedStrings.GetNonEncoded(2063734279));
					}
					if (messageItem.Recipients.Count == 0)
					{
						throw new OwaEventHandlerException("No recipients", LocalizedStrings.GetNonEncoded(1878192149));
					}
					if (Utilities.RecipientsOnlyHaveEmptyPDL<Recipient>(base.UserContext, messageItem.Recipients))
					{
						base.RenderPartialFailure(1389137820);
					}
					else
					{
						ExTraceGlobals.MailTracer.TraceDebug((long)this.GetHashCode(), "Sending message");
						messageItem.SetProperties(EditSmsEventHandler.propertiesDeliveryStatusForNewMessage, EditSmsEventHandler.propertyValuesDeliveryStatusForNewMessage);
						try
						{
							messageItem.Send();
						}
						catch (Exception exception)
						{
							if (Utilities.ShouldSendChangeKeyForException(exception))
							{
								messageItem.Load();
								if (!flag && ExTraceGlobals.MailDataTracer.IsTraceEnabled(TraceType.DebugTrace))
								{
									ExTraceGlobals.MailDataTracer.TraceDebug<string>((long)this.GetHashCode(), "New message ID is '{0}'", messageItem.Id.ObjectId.ToBase64String());
								}
								base.SaveIdAndChangeKeyInCustomErrorInfo(messageItem);
							}
							throw;
						}
						if (Globals.ArePerfCountersEnabled)
						{
							OwaSingleCounters.MessagesSent.Increment();
						}
					}
				}
			}
			finally
			{
				if (messageItem != null)
				{
					messageItem.Dispose();
					messageItem = null;
				}
			}
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x00105A0C File Offset: 0x00103C0C
		[OwaEvent("GetSPC")]
		public void GetServiceProviderCapability()
		{
			IList<DeliveryPoint> list = null;
			using (VersionedXmlDataProvider versionedXmlDataProvider = new VersionedXmlDataProvider(base.UserContext.MailboxSession))
			{
				list = ((TextMessagingAccount)versionedXmlDataProvider.Read<TextMessagingAccount>(base.UserContext.ExchangePrincipal.ObjectId)).TextMessagingSettings.PersonToPersonPreferences;
			}
			if (list.Count == 0)
			{
				return;
			}
			MobileServiceCapability mobileServiceCapability = null;
			try
			{
				MobileSession mobileSession = new MobileSession(base.UserContext.ExchangePrincipal, list);
				if (mobileSession.ServiceManagers != null && mobileSession.ServiceManagers.Count > 0)
				{
					mobileServiceCapability = mobileSession.ServiceManagers[0].GetCapabilityForRecipient(null);
				}
			}
			catch (MobileTransientException)
			{
				return;
			}
			catch (MobilePermanentException)
			{
				return;
			}
			if (mobileServiceCapability != null)
			{
				foreach (CodingSupportability codingSupportability in mobileServiceCapability.CodingSupportabilities)
				{
					if (codingSupportability.CodingScheme == CodingScheme.GsmDefault)
					{
						this.RenderScriptIntAssignment("a_iMax7Bit", codingSupportability.RadixPerPart);
					}
					else if (codingSupportability.CodingScheme == CodingScheme.Unicode)
					{
						this.RenderScriptIntAssignment("a_iMaxUncd", codingSupportability.RadixPerPart);
					}
				}
				int value = ((mobileServiceCapability.SupportedPartType & PartType.Concatenated) == PartType.Concatenated) ? 1 : 0;
				this.RenderScriptIntAssignment("a_fLngSms", value);
				return;
			}
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x00105B6C File Offset: 0x00103D6C
		private void RenderScriptIntAssignment(string name, int value)
		{
			this.Writer.Write(name);
			this.Writer.Write('=');
			this.Writer.Write(value);
			this.Writer.Write(';');
		}

		// Token: 0x04001F40 RID: 8000
		public const string EventNamespace = "EditSms";

		// Token: 0x04001F41 RID: 8001
		public const string MethodSend = "Send";

		// Token: 0x04001F42 RID: 8002
		public const string MethodGetServiceProviderCapability = "GetSPC";

		// Token: 0x04001F43 RID: 8003
		private const int MaxSubjectLength = 160;

		// Token: 0x04001F44 RID: 8004
		private static readonly PropertyDefinition[] propertiesDeliveryStatusForNewMessage = new PropertyDefinition[]
		{
			MessageItemSchema.TextMessageDeliveryStatus
		};

		// Token: 0x04001F45 RID: 8005
		private static readonly object[] propertyValuesDeliveryStatusForNewMessage = new object[]
		{
			1
		};
	}
}
