using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.FfoReporting.Common;
using Microsoft.Exchange.Management.FfoReporting.Data;
using Microsoft.Exchange.Management.FfoReporting.Providers;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003A7 RID: 935
	[Cmdlet("Get", "MessageTraceDetail")]
	[OutputType(new Type[]
	{
		typeof(MessageTraceDetail)
	})]
	public sealed class GetMessageTraceDetail : MtrtTask<MessageTraceDetail>
	{
		// Token: 0x060020DF RID: 8415 RVA: 0x0008B2C8 File Offset: 0x000894C8
		public GetMessageTraceDetail() : base("Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.MessageTraceDetail, Microsoft.Exchange.Hygiene.Data")
		{
			this.MessageTraceId = Guid.Empty;
			this.MessageId = null;
			this.RecipientAddress = null;
			this.SenderAddress = null;
			this.Action = new MultiValuedProperty<string>();
			this.Event = new MultiValuedProperty<string>();
			this.functionMap.Add("receive", (GetMessageTraceDetail.DalPlaceholder dalObject) => this.GetMessageTraceMail(dalObject, "MtrtMessageRecieve", Strings.MtrtNoDetailInformation, new string[]
			{
				"ServerHostName"
			}));
			this.functionMap.Add("send", (GetMessageTraceDetail.DalPlaceholder dalObject) => this.GetMessageTraceMail(dalObject, "MtrtMessageSend", Strings.MtrtNoDetailInformation, new string[]
			{
				"ConnectorId"
			}));
			this.functionMap.Add("deliver", (GetMessageTraceDetail.DalPlaceholder dalObject) => this.GetMessageTraceMail(dalObject, "MtrtMessageDeliverDetailMessage", Strings.MtrtNoDetailInformation, null));
			this.functionMap.Add("badmail", (GetMessageTraceDetail.DalPlaceholder dalObject) => this.GetMessageTraceMail(dalObject, "MtrtMessageBadmail", Strings.MtrtNoDetailInformation, new string[0]));
			this.functionMap.Add("expand", (GetMessageTraceDetail.DalPlaceholder dalObject) => this.GetMessageTraceMail(dalObject, "MtrtMessageExpandDetailMessage", Strings.MtrtNoDetailInformation, null));
			this.functionMap.Add("submit", (GetMessageTraceDetail.DalPlaceholder dalObject) => this.GetMessageTraceMail(dalObject, "MtrtMessageSubmitDetailMessage", Strings.MtrtNoDetailInformation, null));
			this.functionMap.Add("defer", (GetMessageTraceDetail.DalPlaceholder dalObject) => this.GetMessageTraceMail(dalObject, "MtrtMessageFailureReason", Strings.MtrtMessageDefer, new string[]
			{
				"RecipientStatus"
			}));
			this.functionMap.Add("fail", (GetMessageTraceDetail.DalPlaceholder dalObject) => this.GetMessageTraceMail(dalObject, "MtrtMessageFailureReason", Strings.MtrtMessageFail, new string[]
			{
				"RecipientStatus"
			}));
			this.functionMap.Add("agentinfo", new GetMessageTraceDetail.ParseDataDelegate(this.GetAgentInfoMessageTrace));
			this.functionMap.Add("ama", new GetMessageTraceDetail.ParseDataDelegate(this.GetMalwareMessageTrace));
			this.functionMap.Add("sfa", new GetMessageTraceDetail.ParseDataDelegate(this.GetSpamMessageTrace));
			this.functionMap.Add("tra", new GetMessageTraceDetail.ParseDataDelegate(this.GetTransportRuleTrace));
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x060020E0 RID: 8416 RVA: 0x0008B8E0 File Offset: 0x00089AE0
		// (set) Token: 0x060020E1 RID: 8417 RVA: 0x0008B8E8 File Offset: 0x00089AE8
		[CmdletValidator("ValidateRequiredField", new object[]
		{

		})]
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		[QueryParameter("InternalMessageIdQueryDefinition", new string[]
		{

		})]
		public Guid MessageTraceId { get; set; }

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x060020E2 RID: 8418 RVA: 0x0008B8F1 File Offset: 0x00089AF1
		// (set) Token: 0x060020E3 RID: 8419 RVA: 0x0008B8F9 File Offset: 0x00089AF9
		[QueryParameter("ClientMessageIdQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public string MessageId { get; set; }

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x060020E4 RID: 8420 RVA: 0x0008B902 File Offset: 0x00089B02
		// (set) Token: 0x060020E5 RID: 8421 RVA: 0x0008B90A File Offset: 0x00089B0A
		[CmdletValidator("ValidateEmailAddress", new object[]
		{
			CmdletValidator.EmailAddress.Sender,
			CmdletValidator.WildcardValidationOptions.Disallow
		})]
		[QueryParameter("SenderAddressQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public string SenderAddress { get; set; }

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x060020E6 RID: 8422 RVA: 0x0008B913 File Offset: 0x00089B13
		// (set) Token: 0x060020E7 RID: 8423 RVA: 0x0008B91B File Offset: 0x00089B1B
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		[QueryParameter("RecipientAddressQueryDefinition", new string[]
		{

		})]
		[CmdletValidator("ValidateRequiredField", new object[]
		{

		})]
		[CmdletValidator("ValidateEmailAddress", new object[]
		{
			CmdletValidator.EmailAddress.Recipient,
			CmdletValidator.WildcardValidationOptions.Disallow
		})]
		public string RecipientAddress { get; set; }

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x060020E8 RID: 8424 RVA: 0x0008B924 File Offset: 0x00089B24
		// (set) Token: 0x060020E9 RID: 8425 RVA: 0x0008B92C File Offset: 0x00089B2C
		[CmdletValidator("ValidateEnum", new object[]
		{
			typeof(Schema.Actions)
		}, ErrorMessage = Strings.IDs.InvalidActionParameter)]
		[Parameter(Mandatory = false)]
		[QueryParameter("ActionListQueryDefinition", new string[]
		{

		})]
		public MultiValuedProperty<string> Action { get; set; }

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x060020EA RID: 8426 RVA: 0x0008B935 File Offset: 0x00089B35
		// (set) Token: 0x060020EB RID: 8427 RVA: 0x0008B93D File Offset: 0x00089B3D
		[QueryParameter("EventListQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> Event { get; set; }

		// Token: 0x060020EC RID: 8428 RVA: 0x0008B948 File Offset: 0x00089B48
		protected override void CustomInternalValidate()
		{
			base.CustomInternalValidate();
			if (!string.IsNullOrEmpty(this.MessageId))
			{
				bool flag = this.MessageId[0] != '<' && this.MessageId[this.MessageId.Length - 1] != '>';
				if (flag)
				{
					this.MessageId = '<' + this.MessageId + '>';
				}
			}
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x0008B9C0 File Offset: 0x00089BC0
		protected override IReadOnlyList<MessageTraceDetail> AggregateOutput()
		{
			IEnumerable dalRecords = base.GetDalRecords(new FfoReportingDalTask<MessageTraceDetail>.DalRetrievalDelegate(ServiceLocator.Current.GetService<IDalProvider>().GetSingleDataPage), null);
			IReadOnlyList<GetMessageTraceDetail.DalPlaceholder> readOnlyList = DataProcessorDriver.Process<GetMessageTraceDetail.DalPlaceholder>(dalRecords, ConversionProcessor.Create<GetMessageTraceDetail.DalPlaceholder>(this));
			List<MessageTraceDetail> list = new List<MessageTraceDetail>();
			foreach (GetMessageTraceDetail.DalPlaceholder dalPlaceholder in readOnlyList)
			{
				GetMessageTraceDetail.ParseDataDelegate parseDataDelegate;
				if (string.IsNullOrEmpty(dalPlaceholder.EventDescription))
				{
					base.Diagnostics.TraceWarning("Unknown EventDescription");
				}
				else if (this.functionMap.TryGetValue(dalPlaceholder.EventDescription.ToLower(), out parseDataDelegate))
				{
					MessageTraceDetail messageTraceDetail = parseDataDelegate(dalPlaceholder);
					if (messageTraceDetail != null)
					{
						list.Add(messageTraceDetail);
						base.Diagnostics.Checkpoint(dalPlaceholder.EventDescription);
					}
					else
					{
						base.Diagnostics.TraceWarning(string.Format("EventDescription not defined[{0}]", dalPlaceholder.EventDescription));
					}
				}
			}
			return list;
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x0008BABC File Offset: 0x00089CBC
		internal MessageTraceDetail GetMessageTraceMail(GetMessageTraceDetail.DalPlaceholder dalObject, string detailMessage, string defaultDetailMessage, params string[] propertyNames)
		{
			MessageTraceDetail messageTraceDetail = this.CreateMessageTraceDetail(dalObject);
			if (string.IsNullOrEmpty(detailMessage))
			{
				base.Diagnostics.TraceWarning("Unknown detail message");
				messageTraceDetail.Detail = Strings.MtrtNoDetailInformation;
			}
			else if (propertyNames == null || propertyNames.Length == 0)
			{
				PropertyInfo property = typeof(Strings).GetProperty(detailMessage, BindingFlags.Static | BindingFlags.Public);
				messageTraceDetail.Detail = (LocalizedString)property.GetValue(null, null);
			}
			else
			{
				List<string> list = this.ParseXml(dalObject.Data, propertyNames);
				if (list.Count > 0)
				{
					MethodInfo method = typeof(Strings).GetMethod(detailMessage, BindingFlags.Static | BindingFlags.Public);
					messageTraceDetail.Detail = (LocalizedString)Schema.Utilities.Invoke(method, null, list.ToArray());
				}
				else
				{
					base.Diagnostics.TraceWarning("No detail xml");
					if (string.IsNullOrWhiteSpace(defaultDetailMessage))
					{
						base.Diagnostics.TraceWarning("Unknown default detail message");
						messageTraceDetail.Detail = Strings.MtrtNoDetailInformation;
					}
					else
					{
						messageTraceDetail.Detail = defaultDetailMessage;
					}
				}
			}
			return messageTraceDetail;
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x0008BBC4 File Offset: 0x00089DC4
		internal MessageTraceDetail GetAgentInfoMessageTrace(GetMessageTraceDetail.DalPlaceholder dalObject)
		{
			GetMessageTraceDetail.ParseDataDelegate parseDataDelegate;
			if (dalObject.AgentName != null && this.functionMap.TryGetValue(dalObject.AgentName.ToLower(), out parseDataDelegate))
			{
				return parseDataDelegate(dalObject);
			}
			string arg = (dalObject.AgentName == null) ? "Null" : dalObject.AgentName;
			base.Diagnostics.TraceWarning(string.Format("Unknown AgentName[{0}]", arg));
			return null;
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x0008BC28 File Offset: 0x00089E28
		internal MessageTraceDetail GetSpamMessageTrace(GetMessageTraceDetail.DalPlaceholder dalObject)
		{
			string detailMessage = string.Empty;
			string text = null;
			string action;
			switch (action = dalObject.Action)
			{
			case "sn":
				detailMessage = "MtrtMessageSpamNonProvisionedDomain";
				text = "SCL";
				break;
			case "st":
				detailMessage = "MtrtMessageSpamAdditional";
				break;
			case "sd":
			case "so":
			case "sq":
			case "sr":
			case "ss":
			case "sx":
				detailMessage = "MtrtMessageSpam";
				text = "SCL";
				break;
			}
			return this.GetMessageTraceMail(dalObject, detailMessage, Strings.MtrtNoDetailInformation, new string[]
			{
				text
			});
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x0008BD3C File Offset: 0x00089F3C
		internal MessageTraceDetail GetMalwareMessageTrace(GetMessageTraceDetail.DalPlaceholder dalObject)
		{
			string action;
			if ((action = dalObject.Action) != null && (action == "b" || action == "r"))
			{
				return this.GetMessageTraceMail(dalObject, "MtrtMessageMalware", Strings.MtrtNoDetailInformation, new string[]
				{
					"name",
					"file"
				});
			}
			string arg = (dalObject.Action == null) ? "Null" : dalObject.Action;
			base.Diagnostics.TraceWarning(string.Format("Unknown Malware Action[{0}]", arg));
			return null;
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x0008BDCC File Offset: 0x00089FCC
		internal MessageTraceDetail GetTransportRuleTrace(GetMessageTraceDetail.DalPlaceholder dalObject)
		{
			MessageTraceDetail messageTraceDetail = this.CreateMessageTraceDetail(dalObject);
			string ruleName = dalObject.RuleName ?? string.Empty;
			string id = dalObject.RuleId ?? string.Empty;
			if (string.IsNullOrWhiteSpace(dalObject.PolicyId))
			{
				messageTraceDetail.Detail = Strings.MtrtMessageTransportRule(ruleName, id);
			}
			else
			{
				string policyName = dalObject.PolicyName ?? string.Empty;
				string dlpid = dalObject.PolicyId ?? string.Empty;
				messageTraceDetail.Detail = Strings.MtrtMessageDLPRule(ruleName, id, policyName, dlpid);
			}
			return messageTraceDetail;
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x0008BE5C File Offset: 0x0008A05C
		private MessageTraceDetail CreateMessageTraceDetail(GetMessageTraceDetail.DalPlaceholder dalObject)
		{
			MessageTraceDetail messageTraceDetail = new MessageTraceDetail();
			messageTraceDetail.Organization = dalObject.Organization;
			messageTraceDetail.MessageTraceId = dalObject.InternalMessageId;
			messageTraceDetail.MessageId = dalObject.MessageId;
			messageTraceDetail.Date = dalObject.EventDate;
			messageTraceDetail.Data = dalObject.Data;
			messageTraceDetail.Index = dalObject.Index;
			if (string.Equals(dalObject.EventDescription, "agentinfo", StringComparison.InvariantCultureIgnoreCase))
			{
				string @event;
				if (!string.IsNullOrEmpty(dalObject.AgentName) && this.eventNameMap.TryGetValue(dalObject.AgentName.ToLower(), out @event))
				{
					messageTraceDetail.Event = @event;
				}
				string action;
				if (!string.IsNullOrEmpty(dalObject.Action) && this.actionNameMap.TryGetValue(dalObject.Action.ToLower(), out action))
				{
					messageTraceDetail.Action = action;
				}
			}
			else
			{
				messageTraceDetail.Event = dalObject.EventDescription;
			}
			return messageTraceDetail;
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x0008BF7C File Offset: 0x0008A17C
		private List<string> ParseXml(string xml, params string[] propertyNames)
		{
			List<string> list = new List<string>();
			bool flag = false;
			if (propertyNames != null && !string.IsNullOrWhiteSpace(xml))
			{
				XDocument xdocument = XDocument.Parse(xml);
				IEnumerable<XElement> source = xdocument.Descendants("MEP");
				int i = 0;
				while (i < propertyNames.Length)
				{
					string propertyName = propertyNames[i];
					XElement xelement = (from elem in source
					where string.Equals(elem.Attribute("Name").Value, propertyName, StringComparison.InvariantCultureIgnoreCase)
					select elem).FirstOrDefault<XElement>();
					if (xelement == null)
					{
						goto IL_BD;
					}
					XAttribute xattribute = (from att in xelement.Attributes()
					where !string.Equals(att.Name.LocalName, "Name", StringComparison.InvariantCultureIgnoreCase)
					select att).FirstOrDefault<XAttribute>();
					if (xattribute == null)
					{
						goto IL_BD;
					}
					list.Add(xattribute.Value);
					flag = true;
					IL_C8:
					i++;
					continue;
					IL_BD:
					list.Add(string.Empty);
					goto IL_C8;
				}
			}
			if (!flag)
			{
				list.Clear();
			}
			return list;
		}

		// Token: 0x04001A32 RID: 6706
		private Dictionary<string, string> eventNameMap = new Dictionary<string, string>
		{
			{
				"receive",
				Strings.MtrtEventReceive
			},
			{
				"send",
				Strings.MtrtEventSend
			},
			{
				"fail",
				Strings.MtrtEventFail
			},
			{
				"deliver",
				Strings.MtrtEventDeliver
			},
			{
				"badmail",
				Strings.MtrtEventBadmail
			},
			{
				"expand",
				Strings.MtrtEventExpand
			},
			{
				"submit",
				Strings.MtrtEventSubmit
			},
			{
				"defer",
				Strings.MtrtEventDefer
			},
			{
				"ama",
				Strings.MtrtEventMalware
			},
			{
				"sfa",
				Strings.MtrtEventSpam
			},
			{
				"tra",
				Strings.MtrtEventTransportRule
			}
		};

		// Token: 0x04001A33 RID: 6707
		private Dictionary<string, string> actionNameMap = new Dictionary<string, string>
		{
			{
				"blindcopyto",
				Strings.MtrtAddBlindCopyToRecipient
			},
			{
				"copyto",
				Strings.MtrtAddCopyToRecipient
			},
			{
				"sx",
				Strings.MtrtAddHeader
			},
			{
				"addmanagerasrecipienttype",
				Strings.MtrtAddManagerAsRecipient
			},
			{
				"addtorecipient",
				Strings.MtrtAddToRecipient
			},
			{
				"applyclassification ",
				Strings.MtrtApplyClassification
			},
			{
				"applyhtmldisclaimer",
				Strings.MtrtApplyHtmlDisclaimer
			},
			{
				"decrypt",
				Strings.MtrtDecrypt
			},
			{
				"r",
				Strings.MtrtDeleteAttachment
			},
			{
				"b",
				Strings.MtrtDeleteMessage
			},
			{
				"deletemessage",
				Strings.MtrtDeleteMessage
			},
			{
				"sd",
				Strings.MtrtDeleteMessage
			},
			{
				"generateincidentreport",
				Strings.MtrtGenerateIncidentReport
			},
			{
				"moderatemessagebymanager",
				Strings.MtrtModerateMessageByManager
			},
			{
				"moderatemessagebyuser",
				Strings.MtrtModerateMessageByUser
			},
			{
				"sendernotify",
				Strings.MtrtNotifySender
			},
			{
				"prependsubject",
				Strings.MtrtPrependSubject
			},
			{
				"ss",
				Strings.MtrtPrependSubject
			},
			{
				"quarantine",
				Strings.MtrtQuarantine
			},
			{
				"sq",
				Strings.MtrtQuarantine
			},
			{
				"redirectmessage",
				Strings.MtrtRedirectMessage
			},
			{
				"sr",
				Strings.MtrtRedirectMessage
			},
			{
				"rejectmessage",
				Strings.MtrtRejectMessage
			},
			{
				"removeheader",
				Strings.MtrtRemoveHeader
			},
			{
				"reportseveritylevelhigh",
				Strings.MtrtReportSeverityLevelHigh
			},
			{
				"reportseveritylevellow",
				Strings.MtrtReportSeverityLevelLow
			},
			{
				"reportseveritylevelmed",
				Strings.MtrtReportSeverityLevelMed
			},
			{
				"routemessageoutboundrequiretls",
				Strings.MtrtRequireTLS
			},
			{
				"encryptmessage",
				Strings.MtrtRequireEncryption
			},
			{
				"decryptmessage",
				Strings.MtrtRequireDecryption
			},
			{
				"rightsprotectmessage",
				Strings.MtrtRightsProtectMessage
			},
			{
				"sn",
				Strings.MtrtRouteMessageHighRisk
			},
			{
				"so",
				Strings.MtrtRouteMessageHighRisk
			},
			{
				"routemessageoutboundconnector",
				Strings.MtrtRouteMessageUsingConnector
			},
			{
				"setauditseverity",
				Strings.MtrtSetAuditSeverity
			},
			{
				"setheader",
				Strings.MtrtSetHeader
			},
			{
				"st",
				Strings.MtrtSetHeader
			},
			{
				"setscl",
				Strings.MtrtSetSpamConfidenceLevel
			}
		};

		// Token: 0x04001A34 RID: 6708
		private Dictionary<string, GetMessageTraceDetail.ParseDataDelegate> functionMap = new Dictionary<string, GetMessageTraceDetail.ParseDataDelegate>();

		// Token: 0x020003A8 RID: 936
		internal static class LocalizedStringMethods
		{
			// Token: 0x04001A3C RID: 6716
			internal const string Malware = "MtrtMessageMalware";

			// Token: 0x04001A3D RID: 6717
			internal const string Spam = "MtrtMessageSpam";

			// Token: 0x04001A3E RID: 6718
			internal const string SpamDomain = "MtrtMessageSpamNonProvisionedDomain";

			// Token: 0x04001A3F RID: 6719
			internal const string SpamAdditional = "MtrtMessageSpamAdditional";

			// Token: 0x04001A40 RID: 6720
			internal const string TransportRule = "MtrtMessageTransportRule";

			// Token: 0x04001A41 RID: 6721
			internal const string DLPRule = "MtrtMessageDLPRule";

			// Token: 0x04001A42 RID: 6722
			internal const string Receive = "MtrtMessageRecieve";

			// Token: 0x04001A43 RID: 6723
			internal const string Send = "MtrtMessageSend";

			// Token: 0x04001A44 RID: 6724
			internal const string Fail = "MtrtMessageFail";

			// Token: 0x04001A45 RID: 6725
			internal const string FailureDetails = "MtrtMessageFailureReason";

			// Token: 0x04001A46 RID: 6726
			internal const string Deliver = "MtrtMessageDeliverDetailMessage";

			// Token: 0x04001A47 RID: 6727
			internal const string Badmail = "MtrtMessageBadmail";

			// Token: 0x04001A48 RID: 6728
			internal const string Expand = "MtrtMessageExpandDetailMessage";

			// Token: 0x04001A49 RID: 6729
			internal const string Submit = "MtrtMessageSubmitDetailMessage";

			// Token: 0x04001A4A RID: 6730
			internal const string Defer = "MtrtMessageDefer";
		}

		// Token: 0x020003A9 RID: 937
		internal static class PropertyNames
		{
			// Token: 0x04001A4B RID: 6731
			internal const string ServerHostName = "ServerHostName";

			// Token: 0x04001A4C RID: 6732
			internal const string ConnectorId = "ConnectorId";

			// Token: 0x04001A4D RID: 6733
			internal const string Name = "name";

			// Token: 0x04001A4E RID: 6734
			internal const string File = "file";

			// Token: 0x04001A4F RID: 6735
			internal const string SCL = "SCL";

			// Token: 0x04001A50 RID: 6736
			internal const string RecipientStatus = "RecipientStatus";
		}

		// Token: 0x020003AA RID: 938
		internal static class Actions
		{
			// Token: 0x04001A51 RID: 6737
			internal const string AddManagerAsRecipientType = "addmanagerasrecipienttype";

			// Token: 0x04001A52 RID: 6738
			internal const string AddToRecipient = "addtorecipient";

			// Token: 0x04001A53 RID: 6739
			internal const string ApplyClassification = "applyclassification ";

			// Token: 0x04001A54 RID: 6740
			internal const string ApplyHtmlDisclaimer = "applyhtmldisclaimer";

			// Token: 0x04001A55 RID: 6741
			internal const string b = "b";

			// Token: 0x04001A56 RID: 6742
			internal const string BlindCopyTo = "blindcopyto";

			// Token: 0x04001A57 RID: 6743
			internal const string CopyTo = "copyto";

			// Token: 0x04001A58 RID: 6744
			internal const string Decrypt = "decrypt";

			// Token: 0x04001A59 RID: 6745
			internal const string DeleteMessage = "deletemessage";

			// Token: 0x04001A5A RID: 6746
			internal const string GenerateIncidentReport = "generateincidentreport";

			// Token: 0x04001A5B RID: 6747
			internal const string ModerateMessageByManager = "moderatemessagebymanager";

			// Token: 0x04001A5C RID: 6748
			internal const string ModerateMessageByUser = "moderatemessagebyuser";

			// Token: 0x04001A5D RID: 6749
			internal const string PrependSubject = "prependsubject";

			// Token: 0x04001A5E RID: 6750
			internal const string Quarantine = "quarantine";

			// Token: 0x04001A5F RID: 6751
			internal const string r = "r";

			// Token: 0x04001A60 RID: 6752
			internal const string RedirectMessage = "redirectmessage";

			// Token: 0x04001A61 RID: 6753
			internal const string RejectMessage = "rejectmessage";

			// Token: 0x04001A62 RID: 6754
			internal const string RemoveHeader = "removeheader";

			// Token: 0x04001A63 RID: 6755
			internal const string ReportSeverityLevelHigh = "reportseveritylevelhigh";

			// Token: 0x04001A64 RID: 6756
			internal const string ReportSeverityLevelLow = "reportseveritylevellow";

			// Token: 0x04001A65 RID: 6757
			internal const string ReportSeverityLevelMed = "reportseveritylevelmed";

			// Token: 0x04001A66 RID: 6758
			internal const string RightsProtectMessage = "rightsprotectmessage";

			// Token: 0x04001A67 RID: 6759
			internal const string RouteMessageOutboundConnector = "routemessageoutboundconnector";

			// Token: 0x04001A68 RID: 6760
			internal const string RouteMessageOutboundRequireTls = "routemessageoutboundrequiretls";

			// Token: 0x04001A69 RID: 6761
			internal const string EncryptMessage = "encryptmessage";

			// Token: 0x04001A6A RID: 6762
			internal const string DecryptMessage = "decryptmessage";

			// Token: 0x04001A6B RID: 6763
			internal const string SD = "sd";

			// Token: 0x04001A6C RID: 6764
			internal const string SetAuditSeverity = "setauditseverity";

			// Token: 0x04001A6D RID: 6765
			internal const string SenderNotify = "sendernotify";

			// Token: 0x04001A6E RID: 6766
			internal const string SetHeader = "setheader";

			// Token: 0x04001A6F RID: 6767
			internal const string SetSCL = "setscl";

			// Token: 0x04001A70 RID: 6768
			internal const string SN = "sn";

			// Token: 0x04001A71 RID: 6769
			internal const string SO = "so";

			// Token: 0x04001A72 RID: 6770
			internal const string SQ = "sq";

			// Token: 0x04001A73 RID: 6771
			internal const string SR = "sr";

			// Token: 0x04001A74 RID: 6772
			internal const string SS = "ss";

			// Token: 0x04001A75 RID: 6773
			internal const string ST = "st";

			// Token: 0x04001A76 RID: 6774
			internal const string SX = "sx";
		}

		// Token: 0x020003AB RID: 939
		internal static class EventNames
		{
			// Token: 0x04001A77 RID: 6775
			internal const string Receive = "receive";

			// Token: 0x04001A78 RID: 6776
			internal const string Send = "send";

			// Token: 0x04001A79 RID: 6777
			internal const string Fail = "fail";

			// Token: 0x04001A7A RID: 6778
			internal const string Deliver = "deliver";

			// Token: 0x04001A7B RID: 6779
			internal const string Badmail = "badmail";

			// Token: 0x04001A7C RID: 6780
			internal const string Expand = "expand";

			// Token: 0x04001A7D RID: 6781
			internal const string Submit = "submit";

			// Token: 0x04001A7E RID: 6782
			internal const string Defer = "defer";

			// Token: 0x04001A7F RID: 6783
			internal const string Malware = "ama";

			// Token: 0x04001A80 RID: 6784
			internal const string Spam = "sfa";

			// Token: 0x04001A81 RID: 6785
			internal const string TransportRule = "tra";

			// Token: 0x04001A82 RID: 6786
			internal const string AgentInfo = "agentinfo";
		}

		// Token: 0x020003AC RID: 940
		// (Invoke) Token: 0x060020FF RID: 8447
		private delegate MessageTraceDetail ParseDataDelegate(GetMessageTraceDetail.DalPlaceholder dalObject);

		// Token: 0x020003AD RID: 941
		internal sealed class DalPlaceholder : IPageableObject
		{
			// Token: 0x17000981 RID: 2433
			// (get) Token: 0x06002102 RID: 8450 RVA: 0x0008C06C File Offset: 0x0008A26C
			// (set) Token: 0x06002103 RID: 8451 RVA: 0x0008C074 File Offset: 0x0008A274
			[DalConversion("OrganizationFromTask", "Organization", new string[]
			{

			})]
			public string Organization { get; set; }

			// Token: 0x17000982 RID: 2434
			// (get) Token: 0x06002104 RID: 8452 RVA: 0x0008C07D File Offset: 0x0008A27D
			// (set) Token: 0x06002105 RID: 8453 RVA: 0x0008C085 File Offset: 0x0008A285
			[DalConversion("DefaultSerializer", "InternalMessageId", new string[]
			{

			})]
			public Guid InternalMessageId { get; set; }

			// Token: 0x17000983 RID: 2435
			// (get) Token: 0x06002106 RID: 8454 RVA: 0x0008C08E File Offset: 0x0008A28E
			// (set) Token: 0x06002107 RID: 8455 RVA: 0x0008C096 File Offset: 0x0008A296
			[DalConversion("DefaultSerializer", "ClientMessageId", new string[]
			{

			})]
			public string MessageId { get; set; }

			// Token: 0x17000984 RID: 2436
			// (get) Token: 0x06002108 RID: 8456 RVA: 0x0008C09F File Offset: 0x0008A29F
			// (set) Token: 0x06002109 RID: 8457 RVA: 0x0008C0A7 File Offset: 0x0008A2A7
			[DalConversion("DefaultSerializer", "EventDate", new string[]
			{

			})]
			public DateTime EventDate { get; set; }

			// Token: 0x17000985 RID: 2437
			// (get) Token: 0x0600210A RID: 8458 RVA: 0x0008C0B0 File Offset: 0x0008A2B0
			// (set) Token: 0x0600210B RID: 8459 RVA: 0x0008C0B8 File Offset: 0x0008A2B8
			[DalConversion("DefaultSerializer", "EventDescription", new string[]
			{

			})]
			public string EventDescription { get; set; }

			// Token: 0x17000986 RID: 2438
			// (get) Token: 0x0600210C RID: 8460 RVA: 0x0008C0C1 File Offset: 0x0008A2C1
			// (set) Token: 0x0600210D RID: 8461 RVA: 0x0008C0C9 File Offset: 0x0008A2C9
			[DalConversion("DefaultSerializer", "AgentName", new string[]
			{

			})]
			public string AgentName { get; set; }

			// Token: 0x17000987 RID: 2439
			// (get) Token: 0x0600210E RID: 8462 RVA: 0x0008C0D2 File Offset: 0x0008A2D2
			// (set) Token: 0x0600210F RID: 8463 RVA: 0x0008C0DA File Offset: 0x0008A2DA
			[DalConversion("DefaultSerializer", "Action", new string[]
			{

			})]
			public string Action { get; set; }

			// Token: 0x17000988 RID: 2440
			// (get) Token: 0x06002110 RID: 8464 RVA: 0x0008C0E3 File Offset: 0x0008A2E3
			// (set) Token: 0x06002111 RID: 8465 RVA: 0x0008C0EB File Offset: 0x0008A2EB
			[DalConversion("DefaultSerializer", "RuleId", new string[]
			{

			})]
			public string RuleId { get; set; }

			// Token: 0x17000989 RID: 2441
			// (get) Token: 0x06002112 RID: 8466 RVA: 0x0008C0F4 File Offset: 0x0008A2F4
			// (set) Token: 0x06002113 RID: 8467 RVA: 0x0008C0FC File Offset: 0x0008A2FC
			[DalConversion("DefaultSerializer", "RuleName", new string[]
			{

			})]
			public string RuleName { get; set; }

			// Token: 0x1700098A RID: 2442
			// (get) Token: 0x06002114 RID: 8468 RVA: 0x0008C105 File Offset: 0x0008A305
			// (set) Token: 0x06002115 RID: 8469 RVA: 0x0008C10D File Offset: 0x0008A30D
			[DalConversion("DefaultSerializer", "PolicyId", new string[]
			{

			})]
			public string PolicyId { get; set; }

			// Token: 0x1700098B RID: 2443
			// (get) Token: 0x06002116 RID: 8470 RVA: 0x0008C116 File Offset: 0x0008A316
			// (set) Token: 0x06002117 RID: 8471 RVA: 0x0008C11E File Offset: 0x0008A31E
			[DalConversion("DefaultSerializer", "PolicyName", new string[]
			{

			})]
			public string PolicyName { get; set; }

			// Token: 0x1700098C RID: 2444
			// (get) Token: 0x06002118 RID: 8472 RVA: 0x0008C127 File Offset: 0x0008A327
			// (set) Token: 0x06002119 RID: 8473 RVA: 0x0008C12F File Offset: 0x0008A32F
			[DalConversion("DefaultSerializer", "Data", new string[]
			{

			})]
			public string Data { get; set; }

			// Token: 0x1700098D RID: 2445
			// (get) Token: 0x0600211A RID: 8474 RVA: 0x0008C138 File Offset: 0x0008A338
			// (set) Token: 0x0600211B RID: 8475 RVA: 0x0008C140 File Offset: 0x0008A340
			public int Index { get; set; }
		}
	}
}
