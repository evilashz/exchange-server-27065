using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxTransport.Shared.Providers;
using Microsoft.Exchange.MailboxTransport.StoreDriver;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Common;
using Microsoft.Exchange.Transport.Internal;

namespace Microsoft.Exchange.MailboxTransport.Shared.SubmissionItem
{
	// Token: 0x0200002F RID: 47
	internal abstract class SubmissionItemBase : IDisposable
	{
		// Token: 0x0600014F RID: 335 RVA: 0x00008128 File Offset: 0x00006328
		static SubmissionItemBase()
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
			SubmissionItemBase.serverVersion = versionInfo.FileVersion;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00008154 File Offset: 0x00006354
		public SubmissionItemBase(string mailProtocol, IStoreDriverTracer storeDriverTracer)
		{
			this.conversionOptions = ConfigurationProvider.GetGlobalConversionOptions();
			this.mailProtocol = mailProtocol;
			this.storeDriverTracer = storeDriverTracer;
			try
			{
				this.localIp = Dns.GetHostEntry(Dns.GetHostName());
			}
			catch (SocketException ex)
			{
				this.storeDriverTracer.StoreDriverCommonTracer.TraceFail<string>(this.storeDriverTracer.MessageProbeActivityId, 0L, "Unable to determine local ip {0}", ex.ToString());
				throw;
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000081D0 File Offset: 0x000063D0
		public SubmissionItemBase(string mailProtocol) : this(mailProtocol, new StoreDriverTracer())
		{
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000152 RID: 338 RVA: 0x000081DE File Offset: 0x000063DE
		public OutboundConversionOptions ConversionOptions
		{
			get
			{
				return this.conversionOptions;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000153 RID: 339 RVA: 0x000081E6 File Offset: 0x000063E6
		public ConversionResult ConversionResult
		{
			get
			{
				return this.conversionResult;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000154 RID: 340 RVA: 0x000081EE File Offset: 0x000063EE
		public IStoreDriverTracer StoreDriverTracer
		{
			get
			{
				return this.storeDriverTracer;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000081F6 File Offset: 0x000063F6
		// (set) Token: 0x06000156 RID: 342 RVA: 0x000081FE File Offset: 0x000063FE
		public StoreSession Session
		{
			get
			{
				return this.storeSession;
			}
			protected set
			{
				this.storeSession = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00008208 File Offset: 0x00006408
		public bool ResubmittedMessage
		{
			get
			{
				int valueOrDefault = this.GetValueTypePropValue<int>(MessageItemSchema.Flags).GetValueOrDefault();
				return (valueOrDefault & 128) != 0;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00008236 File Offset: 0x00006436
		// (set) Token: 0x06000159 RID: 345 RVA: 0x0000823E File Offset: 0x0000643E
		public MessageItem Item
		{
			get
			{
				return this.messageItem;
			}
			protected set
			{
				this.messageItem = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00008247 File Offset: 0x00006447
		public string MessageClass
		{
			get
			{
				return this.messageItem.ClassName;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600015B RID: 347
		public abstract string SourceServerFqdn { get; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600015C RID: 348
		public abstract IPAddress SourceServerNetworkAddress { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600015D RID: 349
		public abstract DateTime OriginalCreateTime { get; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00008254 File Offset: 0x00006454
		public Participant Sender
		{
			get
			{
				return this.messageItem.Sender;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00008261 File Offset: 0x00006461
		public string QuarantineOriginalSender
		{
			get
			{
				return this.GetRefTypePropValue<string>(ItemSchema.QuarantineOriginalSender);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000160 RID: 352 RVA: 0x0000826E File Offset: 0x0000646E
		public RecipientCollection Recipients
		{
			get
			{
				return this.messageItem.Recipients;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000161 RID: 353 RVA: 0x0000827B File Offset: 0x0000647B
		public virtual bool HasMessageItem
		{
			get
			{
				return this.storeSession != null && this.messageItem != null;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00008294 File Offset: 0x00006494
		public bool IsSubmitMessage
		{
			get
			{
				int? valueTypePropValue = this.GetValueTypePropValue<int>(MessageItemSchema.Flags);
				return valueTypePropValue != null && (valueTypePropValue.Value & 4) == 4;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000163 RID: 355 RVA: 0x000082C4 File Offset: 0x000064C4
		public bool IsElcJournalReport
		{
			get
			{
				return this.GetRefTypePropValue<string>(MessageItemSchema.ElcAutoCopyLabel) != null;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000082D8 File Offset: 0x000064D8
		public bool IsMapiAdminSubmission
		{
			get
			{
				bool? valueTypePropValue = this.GetValueTypePropValue<bool>(ItemSchema.SubmittedByAdmin);
				return valueTypePropValue != null && valueTypePropValue.Value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00008304 File Offset: 0x00006504
		public bool IsDlExpansionProhibited
		{
			get
			{
				bool? valueTypePropValue = this.GetValueTypePropValue<bool>(MessageItemSchema.DlExpansionProhibited);
				return valueTypePropValue != null && valueTypePropValue.Value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00008330 File Offset: 0x00006530
		public bool IsAltRecipientProhibited
		{
			get
			{
				bool? valueTypePropValue = this.GetValueTypePropValue<bool>(MessageItemSchema.RecipientReassignmentProhibited);
				return valueTypePropValue != null && valueTypePropValue.Value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000835B File Offset: 0x0000655B
		public virtual bool Done
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000835E File Offset: 0x0000655E
		public IPHostEntry LocalIP
		{
			get
			{
				return this.localIp;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00008366 File Offset: 0x00006566
		public string ReceivedHeaderTcpInfo
		{
			get
			{
				return SubmissionItemBase.FormatIPAddress(this.localIp.AddressList[0]);
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000837C File Offset: 0x0000657C
		public static T? GetValueTypePropValue<T>(Recipient recipient, PropertyDefinition propDefinition) where T : struct
		{
			object obj = recipient.TryGetProperty(propDefinition);
			SubmissionItemBase.LogPropError(obj);
			if (obj == null || !(obj is T))
			{
				return null;
			}
			return new T?((T)((object)obj));
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000083B8 File Offset: 0x000065B8
		public static T GetRefTypePropValue<T>(Recipient recipient, PropertyDefinition propDefinition) where T : class
		{
			object obj = recipient.TryGetProperty(propDefinition);
			SubmissionItemBase.LogPropError(obj);
			return obj as T;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000083DE File Offset: 0x000065DE
		public static string FormatIPAddress(IPAddress address)
		{
			return "[" + address.ToString() + "]";
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000083F5 File Offset: 0x000065F5
		public virtual uint LoadFromStore()
		{
			return 0U;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000083F8 File Offset: 0x000065F8
		public virtual Exception DoneWithMessage()
		{
			return null;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000083FC File Offset: 0x000065FC
		public TimeSpan CopyContentTo(TransportMailItem mailItem)
		{
			this.ConversionOptions.RecipientCache = mailItem.ADRecipientCache;
			this.ConversionOptions.UserADSession = mailItem.ADRecipientCache.ADSession;
			mailItem.CacheTransportSettings();
			this.ConversionOptions.ClearCategories = mailItem.TransportSettings.ClearCategories;
			this.ConversionOptions.UseRFC2231Encoding = mailItem.TransportSettings.Rfc2231EncodingEnabled;
			this.ConversionOptions.AllowDlpHeadersToPenetrateFirewall = true;
			this.storeDriverTracer.StoreDriverCommonTracer.TracePass<long>(this.storeDriverTracer.MessageProbeActivityId, 0L, "Generate content for mailitem {0}", mailItem.RecordId);
			ExDateTime utcNow = ExDateTime.UtcNow;
			using (Stream stream = mailItem.OpenMimeWriteStream(MimeLimits.Default))
			{
				this.conversionResult = ItemConversion.ConvertItemToSummaryTnef(this.messageItem, stream, this.ConversionOptions);
				stream.Flush();
			}
			return ExDateTime.UtcNow - utcNow;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000084F0 File Offset: 0x000066F0
		public void DecorateMessage(TransportMailItem message)
		{
			message.HeloDomain = ConfigurationProvider.GetDefaultDomainName();
			message.ReceiveConnectorName = "FromLocal";
			message.RefreshMimeSize();
			long mimeSize = message.MimeSize;
			HeaderList headers = message.RootPart.Headers;
			if (!(headers.FindFirst(HeaderId.Date) is DateHeader))
			{
				DateHeader newChild = new DateHeader("Date", DateTime.UtcNow.ToLocalTime());
				headers.AppendChild(newChild);
			}
			headers.RemoveAll(HeaderId.Received);
			DateHeader dateHeader = new DateHeader("Date", DateTime.UtcNow.ToLocalTime());
			string value = dateHeader.Value;
			ReceivedHeader newChild2 = new ReceivedHeader(this.SourceServerFqdn, SubmissionItemBase.FormatIPAddress(this.SourceServerNetworkAddress), this.LocalIP.HostName, this.ReceivedHeaderTcpInfo, null, this.mailProtocol, SubmissionItemBase.serverVersion, null, value);
			headers.PrependChild(newChild2);
			message.ExtendedProperties.SetValue<bool>("Microsoft.Exchange.Transport.ElcJournalReport", this.IsElcJournalReport);
			if (this.IsMapiAdminSubmission)
			{
				headers.AppendChild(new AsciiTextHeader("X-MS-Exchange-Organization-Mapi-Admin-Submission", string.Empty));
			}
			if (this.IsDlExpansionProhibited)
			{
				headers.AppendChild(new AsciiTextHeader("X-MS-Exchange-Organization-DL-Expansion-Prohibited", string.Empty));
			}
			headers.AppendChild(new AsciiTextHeader("X-MS-Exchange-Organization-Processed-By-MBTSubmission", string.Empty));
			if (ConfigurationProvider.GetForwardingProhibitedFeatureStatus() && this.IsAltRecipientProhibited)
			{
				headers.AppendChild(new AsciiTextHeader("X-MS-Exchange-Organization-Alt-Recipient-Prohibited", string.Empty));
			}
			headers.RemoveAll("X-MS-Exchange-Organization-OriginalSize");
			headers.AppendChild(new AsciiTextHeader("X-MS-Exchange-Organization-OriginalSize", mimeSize.ToString(NumberFormatInfo.InvariantInfo)));
			headers.RemoveAll("X-MS-Exchange-Organization-OriginalArrivalTime");
			Header newChild3 = new AsciiTextHeader("X-MS-Exchange-Organization-OriginalArrivalTime", Util.FormatOrganizationalMessageArrivalTime(this.OriginalCreateTime));
			headers.AppendChild(newChild3);
			headers.RemoveAll("X-MS-Exchange-Organization-MessageSource");
			Header newChild4 = new AsciiTextHeader("X-MS-Exchange-Organization-MessageSource", "StoreDriver");
			headers.AppendChild(newChild4);
			headers.AppendChild(new AsciiTextHeader("X-MS-Exchange-Transport-FromEntityHeader", RoutingEndpoint.Hosted.ToString()));
			headers.AppendChild(new AsciiTextHeader("X-MS-Exchange-Organization-FromEntityHeader", RoutingEndpoint.Hosted.ToString()));
			message.Directionality = MailDirectionality.Originating;
			message.UpdateDirectionalityAndScopeHeaders();
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00008711 File Offset: 0x00006911
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00008720 File Offset: 0x00006920
		public T? GetValueTypePropValue<T>(PropertyDefinition propDefinition) where T : struct
		{
			object obj = this.messageItem.TryGetProperty(propDefinition);
			SubmissionItemBase.LogPropError(obj);
			if (obj == null || !(obj is T))
			{
				return null;
			}
			return new T?((T)((object)obj));
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00008760 File Offset: 0x00006960
		public T GetRefTypePropValue<T>(PropertyDefinition propDefinition) where T : class
		{
			object obj = this.messageItem.TryGetProperty(propDefinition);
			SubmissionItemBase.LogPropError(obj);
			return obj as T;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000878C File Offset: 0x0000698C
		public T GetPropValue<T>(PropertyDefinition propDefinition, T defaultValue) where T : struct
		{
			object obj = this.messageItem.TryGetProperty(propDefinition);
			SubmissionItemBase.LogPropError(obj);
			if (obj != null && obj is T)
			{
				return (T)((object)obj);
			}
			return defaultValue;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000087C0 File Offset: 0x000069C0
		public void ApplySecurityAttributesTo(TransportMailItem mailitem)
		{
			TransportConfigContainer transportConfigObject = Configuration.TransportConfigObject;
			if (this.GetPropValue<bool>(MessageItemSchema.ClientSubmittedSecurely, false))
			{
				MultilevelAuth.EnsureSecurityAttributes(mailitem, SubmitAuthCategory.Internal, MultilevelAuthMechanism.SecureMapiSubmit, null);
				return;
			}
			if (transportConfigObject.VerifySecureSubmitEnabled)
			{
				MultilevelAuth.EnsureSecurityAttributes(mailitem, SubmitAuthCategory.Anonymous, MultilevelAuthMechanism.MapiSubmit, null);
				return;
			}
			MultilevelAuth.EnsureSecurityAttributes(mailitem, SubmitAuthCategory.Internal, MultilevelAuthMechanism.MapiSubmit, null);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00008812 File Offset: 0x00006A12
		protected void SetSessionTimeZone()
		{
			if (this.Session != null)
			{
				this.Session.ExTimeZone = ExTimeZone.CurrentTimeZone;
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000882C File Offset: 0x00006A2C
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000882E File Offset: 0x00006A2E
		protected void DisposeMessageItem()
		{
			if (this.Item != null)
			{
				this.Item.Dispose();
				this.Item = null;
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000884A File Offset: 0x00006A4A
		protected void DisposeStoreSession()
		{
			if (this.Session != null)
			{
				this.Session.Dispose();
				this.Session = null;
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00008868 File Offset: 0x00006A68
		private static void LogPropError(object value)
		{
			PropertyError propertyError = value as PropertyError;
			if (propertyError != null && propertyError.PropertyErrorCode != PropertyErrorCode.NotFound)
			{
				TraceHelper.StoreDriverTracer.TracePass<PropertyError>(TraceHelper.MessageProbeActivityId, 0L, "Error when trying to access prop : {0}", propertyError);
			}
		}

		// Token: 0x040000A6 RID: 166
		private const int ResendMessageFlag = 128;

		// Token: 0x040000A7 RID: 167
		private static string serverVersion;

		// Token: 0x040000A8 RID: 168
		private readonly IPHostEntry localIp;

		// Token: 0x040000A9 RID: 169
		private readonly string mailProtocol;

		// Token: 0x040000AA RID: 170
		private StoreSession storeSession;

		// Token: 0x040000AB RID: 171
		private MessageItem messageItem;

		// Token: 0x040000AC RID: 172
		private OutboundConversionOptions conversionOptions;

		// Token: 0x040000AD RID: 173
		private ConversionResult conversionResult;

		// Token: 0x040000AE RID: 174
		private IStoreDriverTracer storeDriverTracer;
	}
}
