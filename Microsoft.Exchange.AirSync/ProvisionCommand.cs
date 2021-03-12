using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000106 RID: 262
	internal sealed class ProvisionCommand : Command, IProvisionCommandHost
	{
		// Token: 0x06000E52 RID: 3666 RVA: 0x0004FCAC File Offset: 0x0004DEAC
		public ProvisionCommand()
		{
			base.PerfCounter = AirSyncCounters.NumberOfProvisionRequests;
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x0004FCBF File Offset: 0x0004DEBF
		internal override bool RequiresPolicyCheck
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x0004FCC2 File Offset: 0x0004DEC2
		internal override bool ShouldOpenGlobalSyncState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x0004FCC5 File Offset: 0x0004DEC5
		protected override string RootNodeName
		{
			get
			{
				return "Provision";
			}
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x0004FCCC File Offset: 0x0004DECC
		internal override XmlDocument GetValidationErrorXml()
		{
			if (ProvisionCommand.validationErrorXml == null)
			{
				XmlDocument commandXmlStub = base.GetCommandXmlStub();
				XmlElement xmlElement = commandXmlStub.CreateElement("Status", this.RootNodeNamespace);
				xmlElement.InnerText = XmlConvert.ToString(2);
				commandXmlStub[this.RootNodeName].AppendChild(xmlElement);
				ProvisionCommand.validationErrorXml = commandXmlStub;
			}
			return ProvisionCommand.validationErrorXml;
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x0004FD24 File Offset: 0x0004DF24
		internal override Command.ExecutionState ExecuteCommand()
		{
			base.XmlResponse = base.GetCommandXmlStub();
			XmlNode provisionResponseNode = base.XmlResponse[this.RootNodeName];
			switch (ProvisionCommandPhaseBase.DetermineCallPhase(base.XmlRequest))
			{
			case ProvisionCommandPhaseBase.ProvisionPhase.PhaseOne:
			{
				ProvisionCommandPhaseOne provisionCommandPhaseOne = new ProvisionCommandPhaseOne(this);
				provisionCommandPhaseOne.Process(provisionResponseNode);
				break;
			}
			case ProvisionCommandPhaseBase.ProvisionPhase.PhaseTwo:
			{
				ProvisionCommandPhaseTwo provisionCommandPhaseTwo = new ProvisionCommandPhaseTwo(this);
				provisionCommandPhaseTwo.Process(provisionResponseNode);
				break;
			}
			default:
				throw new AirSyncPermanentException(StatusCode.Sync_ProtocolVersionMismatch, false)
				{
					ErrorStringForProtocolLogger = "Provision_InvalidCallType"
				};
			}
			return Command.ExecutionState.Complete;
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x0004FDA5 File Offset: 0x0004DFA5
		protected override bool HandleQuarantinedState()
		{
			return true;
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x0004FDA8 File Offset: 0x0004DFA8
		XmlNode IProvisionCommandHost.XmlRequest
		{
			get
			{
				return base.XmlRequest;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x0004FDB0 File Offset: 0x0004DFB0
		XmlDocument IProvisionCommandHost.XmlResponse
		{
			get
			{
				return base.XmlResponse;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x0004FDB8 File Offset: 0x0004DFB8
		ProtocolLogger IProvisionCommandHost.ProtocolLogger
		{
			get
			{
				return base.ProtocolLogger;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06000E5C RID: 3676 RVA: 0x0004FDC0 File Offset: 0x0004DFC0
		uint? IProvisionCommandHost.HeaderPolicyKey
		{
			get
			{
				return base.Context.Request.PolicyKey;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x0004FDD2 File Offset: 0x0004DFD2
		int IProvisionCommandHost.ProtocolVersion
		{
			get
			{
				return base.Version;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x0004FDDA File Offset: 0x0004DFDA
		IPolicyData IProvisionCommandHost.PolicyData
		{
			get
			{
				return ADNotificationManager.GetPolicyData(base.User);
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x0004FDE7 File Offset: 0x0004DFE7
		IGlobalInfo IProvisionCommandHost.GlobalInfo
		{
			get
			{
				return base.GlobalInfo;
			}
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x0004FDEF File Offset: 0x0004DFEF
		void IProvisionCommandHost.SetErrorResponse(HttpStatusCode httpStatusCode, StatusCode easStatusCode)
		{
			base.Context.Response.SetErrorResponse(httpStatusCode, easStatusCode);
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x0004FE03 File Offset: 0x0004E003
		void IProvisionCommandHost.SendRemoteWipeConfirmationMessage(ExDateTime wipeAckTime)
		{
			ProvisionCommand.SendRemoteWipeConfirmationMessage(base.GlobalInfo.RemoteWipeConfirmationAddresses, wipeAckTime, base.MailboxSession, base.Context.Request.DeviceIdentity, this);
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x0004FE2D File Offset: 0x0004E02D
		void IProvisionCommandHost.ResetMobileServiceSelector()
		{
			DeviceInfo.ResetMobileServiceSelector(base.MailboxSession, base.SyncStateStorage);
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0004FE40 File Offset: 0x0004E040
		void IProvisionCommandHost.ProcessDeviceInformationSettings(XmlNode inboundDeviceInformationNode, XmlNode provisionResponseNode)
		{
			if (inboundDeviceInformationNode != null)
			{
				XmlNode xmlNode = base.XmlResponse.CreateElement(inboundDeviceInformationNode.LocalName, "Settings:");
				DeviceInformationSetting deviceInformationSetting = new DeviceInformationSetting(inboundDeviceInformationNode, xmlNode, this, base.ProtocolLogger);
				deviceInformationSetting.Execute();
				if (string.IsNullOrEmpty(base.GlobalInfo.DeviceModel))
				{
					throw new AirSyncPermanentException(StatusCode.DeviceInformationRequired, false)
					{
						ErrorStringForProtocolLogger = "DeviceModelMissingInDeviceInformation"
					};
				}
				provisionResponseNode.AppendChild(xmlNode);
			}
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0004FEB0 File Offset: 0x0004E0B0
		public static void SendRemoteWipeConfirmationMessage(string[] addresses, ExDateTime wipeAckTime, MailboxSession mailboxSession, DeviceIdentity deviceIdentity, object traceObject)
		{
			bool flag = false;
			MessageItem messageItem = null;
			CultureInfo preferedCulture = mailboxSession.PreferedCulture;
			try
			{
				StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Drafts);
				messageItem = MessageItem.Create(mailboxSession, defaultFolderId);
				messageItem.ClassName = "IPM.Note.Exchange.ActiveSync.RemoteWipeConfirmation";
				messageItem.Subject = Strings.RemoteWipeConfirmationMessageSubject.ToString(preferedCulture);
				string format = (addresses == null) ? Strings.RemoteWipeConfirmationMessageBody1Owa.ToString(preferedCulture) : Strings.RemoteWipeConfirmationMessageBody1Task.ToString(preferedCulture);
				string text = AirSyncUtility.HtmlEncode(string.Format(CultureInfo.InvariantCulture, format, new object[]
				{
					wipeAckTime
				}), false);
				string text2 = AirSyncUtility.HtmlEncode(string.Format(CultureInfo.InvariantCulture, Strings.DeviceType.ToString(preferedCulture), new object[]
				{
					deviceIdentity.DeviceType
				}), false);
				string text3 = AirSyncUtility.HtmlEncode(string.Format(CultureInfo.InvariantCulture, Strings.DeviceId.ToString(preferedCulture), new object[]
				{
					deviceIdentity.DeviceId
				}), false);
				using (TextWriter textWriter = messageItem.Body.OpenTextWriter(BodyFormat.TextHtml))
				{
					textWriter.Write("\r\n            <html>\r\n                <style>\r\n                    {0}\r\n                </style>\r\n                <body>\r\n                    <h1>{1}</h1>\r\n                    <br><br>\r\n                    <p>\r\n                        {2}\r\n                        <br><br>\r\n                        {3}\r\n                        <br>\r\n                        {4}\r\n                        <br><br>\r\n                        <font color=\"red\">\r\n                        {5}\r\n                        </font>\r\n                        <br><br>\r\n                        {6}\r\n                        <br><br>\r\n                    </p>\r\n                </body>\r\n            </html>\r\n            ", new object[]
					{
						"\r\n            body\r\n            {\r\n                font-family: Tahoma;\r\n                background-color: rgb(255,255,255);\r\n                color: #000000;\r\n                font-size:x-small;\r\n                width: 600px\r\n            }\r\n            p\r\n            {\r\n                margin:0in;\r\n            }\r\n            h1\r\n            {\r\n                font-family: Arial;\r\n                color: #000066;\r\n                margin: 0in;\r\n                font-size: medium; font-weight:bold\r\n            }\r\n            ",
						AirSyncUtility.HtmlEncode(Strings.RemoteWipeConfirmationMessageHeader.ToString(preferedCulture), false),
						text,
						text2,
						text3,
						AirSyncUtility.HtmlEncode(Strings.RemoteWipeConfirmationMessageBody2.ToString(preferedCulture), false),
						AirSyncUtility.HtmlEncode(Strings.RemoteWipeConfirmationMessageBody3.ToString(preferedCulture), false)
					});
				}
				messageItem.From = null;
				Participant participant = new Participant(mailboxSession.MailboxOwner.MailboxInfo.DisplayName, mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(), "SMTP");
				messageItem.Recipients.Add(participant, RecipientItemType.To);
				if (addresses != null)
				{
					foreach (string emailAddress in addresses)
					{
						Participant participant2 = new Participant(null, emailAddress, "SMTP");
						messageItem.Recipients.Add(participant2, RecipientItemType.Bcc);
					}
				}
				messageItem.Send();
				flag = true;
			}
			finally
			{
				if (messageItem != null)
				{
					if (!flag)
					{
						ProvisionCommand.DeleteMessage(messageItem, mailboxSession, traceObject);
					}
					messageItem.Dispose();
				}
			}
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x0005012C File Offset: 0x0004E32C
		private static void DeleteMessage(MessageItem message, MailboxSession mailboxSession, object traceObject)
		{
			message.Load();
			if (message.Id != null)
			{
				AggregateOperationResult aggregateOperationResult = mailboxSession.Delete(DeleteItemFlags.HardDelete, new StoreId[]
				{
					message.Id.ObjectId
				});
				if (OperationResult.Succeeded != aggregateOperationResult.OperationResult)
				{
					AirSyncDiagnostics.TraceDebug<MessageItem>(ExTraceGlobals.RequestsTracer, traceObject, "Failed to delete {0}", message);
				}
			}
		}

		// Token: 0x040009AF RID: 2479
		private const string RemoteWipeMessageBody = "\r\n            <html>\r\n                <style>\r\n                    {0}\r\n                </style>\r\n                <body>\r\n                    <h1>{1}</h1>\r\n                    <br><br>\r\n                    <p>\r\n                        {2}\r\n                        <br><br>\r\n                        {3}\r\n                        <br>\r\n                        {4}\r\n                        <br><br>\r\n                        <font color=\"red\">\r\n                        {5}\r\n                        </font>\r\n                        <br><br>\r\n                        {6}\r\n                        <br><br>\r\n                    </p>\r\n                </body>\r\n            </html>\r\n            ";

		// Token: 0x040009B0 RID: 2480
		private const string RemoteWipeMessageStyle = "\r\n            body\r\n            {\r\n                font-family: Tahoma;\r\n                background-color: rgb(255,255,255);\r\n                color: #000000;\r\n                font-size:x-small;\r\n                width: 600px\r\n            }\r\n            p\r\n            {\r\n                margin:0in;\r\n            }\r\n            h1\r\n            {\r\n                font-family: Arial;\r\n                color: #000066;\r\n                margin: 0in;\r\n                font-size: medium; font-weight:bold\r\n            }\r\n            ";

		// Token: 0x040009B1 RID: 2481
		private static XmlDocument validationErrorXml;

		// Token: 0x02000107 RID: 263
		internal enum PolicyStatusCode
		{
			// Token: 0x040009B3 RID: 2483
			NotPresent,
			// Token: 0x040009B4 RID: 2484
			Success,
			// Token: 0x040009B5 RID: 2485
			NoPolicy,
			// Token: 0x040009B6 RID: 2486
			UnknownPolicyType,
			// Token: 0x040009B7 RID: 2487
			PolicyDataIsCorrupt,
			// Token: 0x040009B8 RID: 2488
			PolicyKeyMismatch
		}

		// Token: 0x02000108 RID: 264
		internal enum ProvisionStatusCode
		{
			// Token: 0x040009BA RID: 2490
			NotPresent,
			// Token: 0x040009BB RID: 2491
			Success,
			// Token: 0x040009BC RID: 2492
			ProtocolError,
			// Token: 0x040009BD RID: 2493
			GeneralServerError
		}
	}
}
