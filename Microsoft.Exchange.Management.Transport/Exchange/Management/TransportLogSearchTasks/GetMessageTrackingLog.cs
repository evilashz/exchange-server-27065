using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Tracking;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Management.Tracking;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Logging.Search;
using Microsoft.Exchange.Transport.LoggingCommon;

namespace Microsoft.Exchange.Management.TransportLogSearchTasks
{
	// Token: 0x0200004B RID: 75
	[OutputType(new Type[]
	{
		typeof(MessageTrackingEvent)
	})]
	[Cmdlet("Get", "MessageTrackingLog")]
	public sealed class GetMessageTrackingLog : LogSearchTask
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000AC3C File Offset: 0x00008E3C
		private ADObjectId RootOrgContainerId
		{
			get
			{
				if (this.rootOrgContainerId == null)
				{
					this.rootOrgContainerId = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
				}
				return this.rootOrgContainerId;
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000AC57 File Offset: 0x00008E57
		public GetMessageTrackingLog() : base(Strings.MessageTrackingActivityName, MessageTrackingSchema.MessageTrackingEvent, "MSGTRK")
		{
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000AC6E File Offset: 0x00008E6E
		// (set) Token: 0x060002AF RID: 687 RVA: 0x0000AC76 File Offset: 0x00008E76
		[Parameter(Mandatory = false, ParameterSetName = "Default")]
		public string EventId
		{
			get
			{
				return this.eventId;
			}
			set
			{
				this.eventId = value;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000AC7F File Offset: 0x00008E7F
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x0000AC87 File Offset: 0x00008E87
		[Parameter(Mandatory = false, ParameterSetName = "Default")]
		public string Sender
		{
			get
			{
				return this.sender;
			}
			set
			{
				this.sender = value;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000AC90 File Offset: 0x00008E90
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x0000AC98 File Offset: 0x00008E98
		[Parameter(Mandatory = false, ParameterSetName = "Default")]
		public string[] Recipients
		{
			get
			{
				return this.recipients;
			}
			set
			{
				this.recipients = value;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000ACA1 File Offset: 0x00008EA1
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000ACA9 File Offset: 0x00008EA9
		[Parameter(Mandatory = false, ParameterSetName = "Default")]
		public string MessageId
		{
			get
			{
				return this.messageId;
			}
			set
			{
				this.messageId = value;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000ACB2 File Offset: 0x00008EB2
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x0000ACBA File Offset: 0x00008EBA
		[Parameter(Mandatory = false, ParameterSetName = "Default")]
		public string InternalMessageId
		{
			get
			{
				return this.internalMessageId;
			}
			set
			{
				this.internalMessageId = value;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000ACC3 File Offset: 0x00008EC3
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x0000ACCB File Offset: 0x00008ECB
		[Parameter(Mandatory = false, ParameterSetName = "Default")]
		public string MessageSubject
		{
			get
			{
				return this.subject;
			}
			set
			{
				this.subject = value;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000ACD4 File Offset: 0x00008ED4
		// (set) Token: 0x060002BB RID: 699 RVA: 0x0000ACDC File Offset: 0x00008EDC
		[Parameter(Mandatory = false, ParameterSetName = "Default")]
		public string Reference
		{
			get
			{
				return this.reference;
			}
			set
			{
				this.reference = value;
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000ACE5 File Offset: 0x00008EE5
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (base.NeedSuppressingPiiData && base.ExchangeRunspaceConfig != null)
			{
				base.ExchangeRunspaceConfig.EnablePiiMap = true;
				this.ResolvePiiParameters();
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000AD10 File Offset: 0x00008F10
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (this.eventId != null)
			{
				bool flag = false;
				foreach (string a in GetMessageTrackingLog.TrackingEventTypes)
				{
					if (string.Equals(a, this.eventId, StringComparison.OrdinalIgnoreCase))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					base.ThrowTerminatingError(new LocalizedException(Strings.EventIdNotFound(this.eventId)), ErrorCategory.InvalidArgument, this.eventId);
				}
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000AD84 File Offset: 0x00008F84
		protected override LogCondition GetCondition()
		{
			LogAndCondition logAndCondition = new LogAndCondition();
			if (this.eventId != null)
			{
				logAndCondition.Conditions.Add(this.GetFieldStringComparison(MessageTrackingField.EventId, this.eventId));
			}
			if (this.sender != null)
			{
				logAndCondition.Conditions.Add(this.GetSenderCondition());
			}
			if (this.recipients != null)
			{
				logAndCondition.Conditions.Add(this.GetRecipientsCondition());
			}
			if (this.messageId != null)
			{
				logAndCondition.Conditions.Add(this.GetFieldStringComparison(MessageTrackingField.MessageId, CsvFieldCache.NormalizeMessageID(this.messageId)));
			}
			if (this.internalMessageId != null)
			{
				logAndCondition.Conditions.Add(this.GetFieldStringComparison(MessageTrackingField.InternalMessageId, this.internalMessageId));
			}
			if (this.subject != null)
			{
				LogConditionField logConditionField = new LogConditionField();
				logConditionField.Name = base.Table.Fields[18].Name;
				LogConditionConstant logConditionConstant = new LogConditionConstant();
				logConditionConstant.Value = this.subject;
				LogStringContainsCondition logStringContainsCondition = new LogStringContainsCondition();
				logStringContainsCondition.Left = logConditionField;
				logStringContainsCondition.Right = logConditionConstant;
				logStringContainsCondition.IgnoreCase = true;
				logAndCondition.Conditions.Add(logStringContainsCondition);
			}
			if (this.reference != null)
			{
				logAndCondition.Conditions.Add(this.GetFieldForAnyCondition(MessageTrackingField.Reference, this.reference, "e"));
			}
			return logAndCondition;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000AEB8 File Offset: 0x000090B8
		protected override void WriteResult(LogSearchCursor cursor)
		{
			object field = cursor.GetField(0);
			if (!(field is DateTime))
			{
				return;
			}
			MessageTrackingEvent messageTrackingEvent = new MessageTrackingEvent();
			messageTrackingEvent.Timestamp = ((DateTime)field).ToLocalTime();
			messageTrackingEvent.ClientIp = (cursor.GetField(1) as string);
			messageTrackingEvent.ClientHostname = (string)cursor.GetField(2);
			messageTrackingEvent.ServerIp = (cursor.GetField(3) as string);
			messageTrackingEvent.ServerHostname = (cursor.GetField(4) as string);
			messageTrackingEvent.SourceContext = (cursor.GetField(5) as string);
			messageTrackingEvent.ConnectorId = (cursor.GetField(6) as string);
			messageTrackingEvent.Source = (cursor.GetField(7) as string);
			messageTrackingEvent.EventId = (cursor.GetField(8) as string);
			messageTrackingEvent.InternalMessageId = (cursor.GetField(9) as string);
			messageTrackingEvent.MessageId = (cursor.GetField(10) as string);
			messageTrackingEvent.Recipients = (cursor.GetField(12) as string[]);
			this.RedactPiiStringArrayIfNeeded(messageTrackingEvent.Recipients);
			messageTrackingEvent.RecipientStatus = (cursor.GetField(13) as string[]);
			if (messageTrackingEvent.EventId == "DELIVER" || messageTrackingEvent.EventId == "DUPLICATEDELIVER")
			{
				this.RedactPiiStringArrayIfNeeded(messageTrackingEvent.RecipientStatus);
			}
			messageTrackingEvent.TotalBytes = GetMessageTrackingLog.Unbox<int>(cursor.GetField(14));
			messageTrackingEvent.RecipientCount = GetMessageTrackingLog.Unbox<int>(cursor.GetField(15));
			messageTrackingEvent.RelatedRecipientAddress = this.RedactPiiStringIfNeeded(cursor.GetField(16) as string, false);
			messageTrackingEvent.Reference = (cursor.GetField(17) as string[]);
			if (messageTrackingEvent.Reference != null && messageTrackingEvent.Reference.Length == 1 && string.IsNullOrEmpty(messageTrackingEvent.Reference[0]))
			{
				messageTrackingEvent.Reference = null;
			}
			messageTrackingEvent.MessageSubject = this.RedactPiiStringIfNeeded(cursor.GetField(18) as string, true);
			messageTrackingEvent.Sender = this.RedactPiiStringIfNeeded(cursor.GetField(19) as string, false);
			messageTrackingEvent.ReturnPath = this.RedactPiiStringIfNeeded(cursor.GetField(20) as string, false);
			messageTrackingEvent.Directionality = (cursor.GetField(22) as string);
			messageTrackingEvent.TenantId = (cursor.GetField(23) as string);
			messageTrackingEvent.OriginalClientIp = (cursor.GetField(24) as string);
			messageTrackingEvent.MessageInfo = (cursor.GetField(21) as string);
			messageTrackingEvent.EventData = (cursor.GetField(26) as KeyValuePair<string, object>[]);
			if (base.NeedSuppressingPiiData)
			{
				Utils.RedactEventData(messageTrackingEvent.EventData, GetMessageTrackingLog.EventDataKeyToRedact, this);
			}
			GetMessageTrackingLog.CalculateLatencyProperties(messageTrackingEvent);
			base.WriteObject(messageTrackingEvent);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000B157 File Offset: 0x00009357
		private string RedactPiiStringIfNeeded(string original, bool caseInvariant)
		{
			if (!base.NeedSuppressingPiiData)
			{
				return original;
			}
			if (caseInvariant)
			{
				original = original.ToUpperInvariant();
			}
			return Utils.RedactPiiString(original, this);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000B175 File Offset: 0x00009375
		private void RedactPiiStringArrayIfNeeded(string[] original)
		{
			if (!base.NeedSuppressingPiiData)
			{
				return;
			}
			Utils.RedactPiiStringArray(original, this);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000B188 File Offset: 0x00009388
		private void ResolvePiiParameters()
		{
			string text;
			if (Utils.TryResolveRedactedString(this.sender, this, out text))
			{
				this.sender = text;
			}
			if (Utils.TryResolveRedactedString(this.subject, this, out text))
			{
				this.subject = text;
			}
			Utils.TryResolveRedactedStringArray(this.recipients, this);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000B1D0 File Offset: 0x000093D0
		private string[] GetProxyAddresses(string address)
		{
			if (string.IsNullOrEmpty(address))
			{
				return null;
			}
			int num = 0;
			string[] array = null;
			Exception ex = null;
			ADRawEntry adrawEntry = null;
			ProxyAddress proxyAddress = ProxyAddress.Parse(address);
			if (proxyAddress is InvalidProxyAddress)
			{
				ex = ((InvalidProxyAddress)proxyAddress).ParseException;
				LocalizedException exception = new LocalizedException(Strings.WarningProxyAddressIsInvalid(address, ex.Message));
				base.WriteError(exception, ErrorCategory.InvalidArgument, null);
				return null;
			}
			if (this.recipSession == null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.RootOrgOrSingleTenantFromAcceptedDomainAutoDetect(SmtpAddress.Parse(address).Domain);
				this.recipSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, true, ConsistencyMode.FullyConsistent, sessionSettings, 977, "GetProxyAddresses", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\LogSearch\\GetMessageTrackingLog.cs");
				if (!this.recipSession.IsReadConnectionAvailable())
				{
					this.recipSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.FullyConsistent, sessionSettings, 986, "GetProxyAddresses", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\LogSearch\\GetMessageTrackingLog.cs");
				}
			}
			AcceptedDomain acceptedDomain = null;
			bool flag;
			do
			{
				flag = false;
				try
				{
					this.GetRecipientInformation(proxyAddress.ToString(), out adrawEntry, out acceptedDomain);
				}
				catch (DataValidationException ex2)
				{
					ex = ex2;
				}
				catch (TransientException ex3)
				{
					if (num < 3)
					{
						flag = true;
						num++;
						Thread.Sleep(1000);
					}
					else
					{
						ex = ex3;
					}
				}
			}
			while (flag);
			if (ex != null)
			{
				this.WriteWarning(Strings.WarningProxyListUnavailable(address, ex.GetType().Name + ": " + ex.Message));
				return null;
			}
			if (adrawEntry == null)
			{
				return null;
			}
			ProxyAddressCollection proxyAddressCollection = (ProxyAddressCollection)adrawEntry[ADRecipientSchema.EmailAddresses];
			string address2 = (string)adrawEntry[ADRecipientSchema.LegacyExchangeDN];
			array = new string[proxyAddressCollection.Count + 1];
			int num2 = 0;
			SmtpProxyAddress smtpProxyAddress;
			if (acceptedDomain != null && SmtpProxyAddress.TryEncapsulate(ProxyAddressPrefix.LegacyDN.PrimaryPrefix, address2, acceptedDomain.DomainName.Domain, out smtpProxyAddress))
			{
				array[0] = smtpProxyAddress.AddressString;
			}
			foreach (ProxyAddress proxyAddress2 in proxyAddressCollection)
			{
				num2++;
				SmtpProxyAddress smtpProxyAddress2 = null;
				if (proxyAddress2.Prefix == ProxyAddressPrefix.Smtp)
				{
					array[num2] = proxyAddress2.AddressString;
				}
				else if (acceptedDomain != null && SmtpProxyAddress.TryEncapsulate(proxyAddress2, acceptedDomain.DomainName.Domain, out smtpProxyAddress2))
				{
					array[num2] = smtpProxyAddress2.AddressString;
				}
			}
			return array;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000B428 File Offset: 0x00009628
		private LogCondition GetSenderCondition()
		{
			string[] proxyAddresses = this.GetProxyAddresses(this.sender);
			if (proxyAddresses == null || proxyAddresses.Length == 1)
			{
				return this.GetFieldStringComparison(MessageTrackingField.SenderAddress, this.sender);
			}
			LogOrCondition logOrCondition = new LogOrCondition();
			foreach (string text in proxyAddresses)
			{
				if (text != null)
				{
					logOrCondition.Conditions.Add(this.GetFieldStringComparison(MessageTrackingField.SenderAddress, text));
				}
			}
			return logOrCondition;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000B494 File Offset: 0x00009694
		private LogCondition GetRecipientsCondition()
		{
			LogOrCondition logOrCondition = new LogOrCondition();
			for (int i = 0; i < this.recipients.Length; i++)
			{
				if (this.recipients[i] != null)
				{
					string[] proxyAddresses = this.GetProxyAddresses(this.recipients[i]);
					if (proxyAddresses == null)
					{
						logOrCondition.Conditions.Add(this.GetFieldForAnyCondition(MessageTrackingField.RecipientAddress, this.recipients[i], "r" + i));
					}
					else
					{
						for (int j = 0; j < proxyAddresses.Length; j++)
						{
							if (proxyAddresses[j] != null)
							{
								logOrCondition.Conditions.Add(this.GetFieldForAnyCondition(MessageTrackingField.RecipientAddress, proxyAddresses[j], string.Concat(new object[]
								{
									"r",
									i,
									"p",
									j
								})));
							}
						}
					}
				}
			}
			return logOrCondition;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000B56C File Offset: 0x0000976C
		private LogStringComparisonCondition GetFieldStringComparison(MessageTrackingField field, string value)
		{
			LogConditionField logConditionField = new LogConditionField();
			logConditionField.Name = base.Table.Fields[(int)field].Name;
			LogConditionConstant logConditionConstant = new LogConditionConstant();
			logConditionConstant.Value = value;
			return new LogStringComparisonCondition
			{
				Left = logConditionField,
				Right = logConditionConstant,
				IgnoreCase = true,
				Operator = LogComparisonOperator.Equals
			};
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000B5C8 File Offset: 0x000097C8
		private LogForAnyCondition GetFieldForAnyCondition(MessageTrackingField field, string value, string variableName)
		{
			LogConditionField logConditionField = new LogConditionField();
			logConditionField.Name = base.Table.Fields[(int)field].Name;
			LogConditionConstant logConditionConstant = new LogConditionConstant();
			logConditionConstant.Value = value;
			LogConditionVariable logConditionVariable = new LogConditionVariable();
			logConditionVariable.Name = variableName;
			LogStringComparisonCondition logStringComparisonCondition = new LogStringComparisonCondition();
			logStringComparisonCondition.Left = logConditionVariable;
			logStringComparisonCondition.Right = logConditionConstant;
			logStringComparisonCondition.IgnoreCase = true;
			logStringComparisonCondition.Operator = LogComparisonOperator.Equals;
			return new LogForAnyCondition
			{
				Field = logConditionField,
				Variable = logConditionVariable,
				Condition = logStringComparisonCondition
			};
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000B650 File Offset: 0x00009850
		private void GetRecipientInformation(string proxyAddress, out ADRawEntry adRecipientEntry, out AcceptedDomain defaultDomain)
		{
			adRecipientEntry = null;
			defaultDomain = null;
			ComparisonFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.EmailAddresses, proxyAddress.ToString());
			ADPagedReader<ADRecipient> adpagedReader = this.recipSession.FindPaged(null, QueryScope.SubTree, filter, null, 0);
			using (IEnumerator<ADRecipient> enumerator = adpagedReader.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					adRecipientEntry = enumerator.Current;
					OrganizationId organizationId = ((ADRecipient)adRecipientEntry).OrganizationId;
					IConfigurationSession configurationSession = this.CreateConfigurationSession(organizationId);
					defaultDomain = configurationSession.GetDefaultAcceptedDomain();
					if (defaultDomain == null)
					{
						throw new DataValidationException(new ObjectValidationError(Strings.ErrorNoDefaultAcceptedDomainFound(organizationId.ToString()), null, null));
					}
				}
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000B6F8 File Offset: 0x000098F8
		private IConfigurationSession CreateConfigurationSession(OrganizationId orgId)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(this.RootOrgContainerId, orgId, base.ExecutingUserOrganizationId, false);
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.FullyConsistent, sessionSettings, 1280, "CreateConfigurationSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\LogSearch\\GetMessageTrackingLog.cs");
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000B740 File Offset: 0x00009940
		private static T? Unbox<T>(object obj) where T : struct
		{
			if (obj == null || !(obj is T))
			{
				return null;
			}
			return new T?((T)((object)obj));
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000B770 File Offset: 0x00009970
		private static void CalculateLatencyProperties(MessageTrackingEvent mte)
		{
			string messageInfo = mte.MessageInfo;
			if (string.IsNullOrEmpty(messageInfo) || (mte.EventId != "DELIVER" && mte.EventId != "SEND" && mte.EventId != "SUBMIT" && (mte.EventId != "RESUBMIT" || mte.Source != "STOREDRIVER")))
			{
				mte.MessageLatency = null;
				mte.MessageLatencyType = MessageLatencyType.None;
				return;
			}
			GetMessageTrackingLog.MessageLatencyParser messageLatencyParser = new GetMessageTrackingLog.MessageLatencyParser();
			if (messageLatencyParser.TryParse(messageInfo))
			{
				if (messageLatencyParser.OriginalArrivalTime != DateTime.MinValue)
				{
					mte.MessageLatency = new EnhancedTimeSpan?((mte.Timestamp.ToUniversalTime() > messageLatencyParser.OriginalArrivalTime.ToUniversalTime()) ? (mte.Timestamp.ToUniversalTime() - messageLatencyParser.OriginalArrivalTime.ToUniversalTime()) : TimeSpan.Zero);
				}
				else
				{
					mte.MessageLatency = null;
				}
				mte.MessageLatencyType = messageLatencyParser.MessageLatencyType;
				return;
			}
			mte.MessageLatency = null;
			mte.MessageLatencyType = MessageLatencyType.None;
		}

		// Token: 0x040000F8 RID: 248
		private const int AdRetryCount = 3;

		// Token: 0x040000F9 RID: 249
		private const int AdRetryIntervalMsec = 1000;

		// Token: 0x040000FA RID: 250
		private static readonly PropertyDefinition[] PropertiesToGet = new PropertyDefinition[]
		{
			ADRecipientSchema.EmailAddresses,
			ADRecipientSchema.LegacyExchangeDN
		};

		// Token: 0x040000FB RID: 251
		private static readonly string[] TrackingEventTypes = Enum.GetNames(typeof(MessageTrackingEvent));

		// Token: 0x040000FC RID: 252
		private static readonly HashSet<string> EventDataKeyToRedact = new HashSet<string>
		{
			"PurportedSender"
		};

		// Token: 0x040000FD RID: 253
		private IRecipientSession recipSession;

		// Token: 0x040000FE RID: 254
		private string eventId;

		// Token: 0x040000FF RID: 255
		private string sender;

		// Token: 0x04000100 RID: 256
		private string[] recipients;

		// Token: 0x04000101 RID: 257
		private string messageId;

		// Token: 0x04000102 RID: 258
		private string internalMessageId;

		// Token: 0x04000103 RID: 259
		private string subject;

		// Token: 0x04000104 RID: 260
		private string reference;

		// Token: 0x04000105 RID: 261
		private ADObjectId rootOrgContainerId;

		// Token: 0x0200004C RID: 76
		private sealed class MessageLatencyParser : LatencyParser
		{
			// Token: 0x060002CD RID: 717 RVA: 0x0000B90E File Offset: 0x00009B0E
			public MessageLatencyParser() : base(ExTraceGlobals.TaskTracer)
			{
				this.originalArrivalTime = DateTime.MinValue;
				this.messageLatencyType = MessageLatencyType.None;
			}

			// Token: 0x060002CE RID: 718 RVA: 0x0000B930 File Offset: 0x00009B30
			public bool TryParse(string s)
			{
				if (string.IsNullOrEmpty(s))
				{
					return false;
				}
				bool flag = false;
				bool flag2 = false;
				DateTime dateTime;
				int num;
				if (ComponentLatencyParser.TryParseOriginalArrivalTime(s, out dateTime, out num))
				{
					this.originalArrivalTime = dateTime;
					flag = true;
				}
				if (num < s.Length)
				{
					flag2 = base.TryParse(s, num, s.Length - num);
				}
				return flag || flag2;
			}

			// Token: 0x1700011F RID: 287
			// (get) Token: 0x060002CF RID: 719 RVA: 0x0000B980 File Offset: 0x00009B80
			public DateTime OriginalArrivalTime
			{
				get
				{
					return this.originalArrivalTime;
				}
			}

			// Token: 0x17000120 RID: 288
			// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000B988 File Offset: 0x00009B88
			public MessageLatencyType MessageLatencyType
			{
				get
				{
					return this.messageLatencyType;
				}
			}

			// Token: 0x060002D1 RID: 721 RVA: 0x0000B990 File Offset: 0x00009B90
			protected override bool HandleLocalServerFqdn(string s, int startIndex, int count)
			{
				this.messageLatencyType = MessageLatencyType.LocalServer;
				return true;
			}

			// Token: 0x060002D2 RID: 722 RVA: 0x0000B99A File Offset: 0x00009B9A
			protected override bool HandleServerFqdn(string s, int startIndex, int count)
			{
				this.messageLatencyType = MessageLatencyType.EndToEnd;
				return true;
			}

			// Token: 0x04000106 RID: 262
			private DateTime originalArrivalTime;

			// Token: 0x04000107 RID: 263
			private MessageLatencyType messageLatencyType;
		}
	}
}
