using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriver;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Internal;

namespace Microsoft.Exchange.MailboxTransport.StoreDriver
{
	// Token: 0x02000006 RID: 6
	internal abstract class SubmissionItem : IDisposable
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002FF8 File Offset: 0x000011F8
		static SubmissionItem()
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
			SubmissionItem.serverVersion = versionInfo.FileVersion;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000302C File Offset: 0x0000122C
		public SubmissionItem(string mailProtocol) : this(mailProtocol, null, null)
		{
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003037 File Offset: 0x00001237
		public SubmissionItem(string mailProtocol, MailItemSubmitter context, SubmissionInfo submissionInfo)
		{
			this.conversionOptions = SubmissionItem.GetGlobalConversionOptions();
			this.mailProtocol = mailProtocol;
			this.Context = context;
			this.submissionInfo = submissionInfo;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000305F File Offset: 0x0000125F
		public OutboundConversionOptions ConversionOptions
		{
			get
			{
				return this.conversionOptions;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00003067 File Offset: 0x00001267
		public ConversionResult ConversionResult
		{
			get
			{
				return this.conversionResult;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000306F File Offset: 0x0000126F
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00003077 File Offset: 0x00001277
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

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00003080 File Offset: 0x00001280
		public bool ResubmittedMessage
		{
			get
			{
				int valueOrDefault = this.GetValueTypePropValue<int>(MessageItemSchema.Flags).GetValueOrDefault();
				return (valueOrDefault & 128) != 0;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000030AE File Offset: 0x000012AE
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000030B6 File Offset: 0x000012B6
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

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000030BF File Offset: 0x000012BF
		public string MessageClass
		{
			get
			{
				return this.messageItem.ClassName;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002B RID: 43
		public abstract string SourceServerFqdn { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002C RID: 44
		public abstract IPAddress SourceServerNetworkAddress { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002D RID: 45
		public abstract DateTime OriginalCreateTime { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000030CC File Offset: 0x000012CC
		public Participant Sender
		{
			get
			{
				return this.messageItem.Sender;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000030D9 File Offset: 0x000012D9
		public string QuarantineOriginalSender
		{
			get
			{
				return this.GetRefTypePropValue<string>(ItemSchema.QuarantineOriginalSender);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000030E6 File Offset: 0x000012E6
		public RecipientCollection Recipients
		{
			get
			{
				return this.messageItem.Recipients;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000030F3 File Offset: 0x000012F3
		public virtual bool HasMessageItem
		{
			get
			{
				return this.storeSession != null && this.messageItem != null;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000310C File Offset: 0x0000130C
		public bool IsSubmitMessage
		{
			get
			{
				int? valueTypePropValue = this.GetValueTypePropValue<int>(MessageItemSchema.Flags);
				return valueTypePropValue != null && (valueTypePropValue.Value & 4) == 4;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000313C File Offset: 0x0000133C
		public bool IsElcJournalReport
		{
			get
			{
				return this.GetRefTypePropValue<string>(MessageItemSchema.ElcAutoCopyLabel) != null;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00003150 File Offset: 0x00001350
		public bool IsMapiAdminSubmission
		{
			get
			{
				bool? valueTypePropValue = this.GetValueTypePropValue<bool>(ItemSchema.SubmittedByAdmin);
				return valueTypePropValue != null && valueTypePropValue.Value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000317C File Offset: 0x0000137C
		public bool IsDlExpansionProhibited
		{
			get
			{
				bool? valueTypePropValue = this.GetValueTypePropValue<bool>(MessageItemSchema.DlExpansionProhibited);
				return valueTypePropValue != null && valueTypePropValue.Value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000031A8 File Offset: 0x000013A8
		public bool IsAltRecipientProhibited
		{
			get
			{
				bool? valueTypePropValue = this.GetValueTypePropValue<bool>(MessageItemSchema.RecipientReassignmentProhibited);
				return valueTypePropValue != null && valueTypePropValue.Value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000031D3 File Offset: 0x000013D3
		public virtual bool Done
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000031D6 File Offset: 0x000013D6
		// (set) Token: 0x06000039 RID: 57 RVA: 0x000031DE File Offset: 0x000013DE
		public virtual TenantPartitionHint TenantPartitionHint
		{
			get
			{
				return this.tenantPartitionHint;
			}
			set
			{
				this.tenantPartitionHint = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000031E7 File Offset: 0x000013E7
		// (set) Token: 0x0600003B RID: 59 RVA: 0x000031EF File Offset: 0x000013EF
		private protected MailItemSubmitter Context { protected get; private set; }

		// Token: 0x0600003C RID: 60 RVA: 0x000031F8 File Offset: 0x000013F8
		public static T? GetValueTypePropValue<T>(Recipient recipient, PropertyDefinition propDefinition) where T : struct
		{
			object obj = recipient.TryGetProperty(propDefinition);
			SubmissionItem.LogPropError(obj);
			if (obj == null || !(obj is T))
			{
				return null;
			}
			return new T?((T)((object)obj));
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003234 File Offset: 0x00001434
		public static T GetRefTypePropValue<T>(Recipient recipient, PropertyDefinition propDefinition) where T : class
		{
			object obj = recipient.TryGetProperty(propDefinition);
			SubmissionItem.LogPropError(obj);
			return obj as T;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000325A File Offset: 0x0000145A
		public virtual uint OpenStore()
		{
			return 0U;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000325D File Offset: 0x0000145D
		public virtual uint LoadFromStore()
		{
			return 0U;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003260 File Offset: 0x00001460
		public virtual Exception DoneWithMessage()
		{
			return null;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003264 File Offset: 0x00001464
		public void CopyContentTo(TransportMailItem mailItem)
		{
			this.conversionOptions.RecipientCache = mailItem.ADRecipientCache;
			this.conversionOptions.UserADSession = mailItem.ADRecipientCache.ADSession;
			mailItem.CacheTransportSettings();
			this.conversionOptions.ClearCategories = mailItem.TransportSettings.ClearCategories;
			this.conversionOptions.UseRFC2231Encoding = mailItem.TransportSettings.Rfc2231EncodingEnabled;
			this.conversionOptions.AllowDlpHeadersToPenetrateFirewall = true;
			SubmissionItem.diag.TraceDebug<long>(0L, "Generate content for mailitem {0}", mailItem.RecordId);
			using (Stream stream = mailItem.OpenMimeWriteStream(MimeLimits.Default))
			{
				this.conversionResult = ItemConversion.ConvertItemToSummaryTnef(this.messageItem, stream, this.conversionOptions);
				stream.Flush();
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003334 File Offset: 0x00001534
		public void DecorateMessage(TransportMailItem message)
		{
			message.HeloDomain = Components.Configuration.FirstOrgAcceptedDomainTable.DefaultDomainName;
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
			ReceivedHeader newChild2 = new ReceivedHeader(this.SourceServerFqdn, StoreDriver.FormatIPAddress(this.SourceServerNetworkAddress), StoreDriver.LocalIP.HostName, StoreDriver.ReceivedHeaderTcpInfo, null, this.mailProtocol, SubmissionItem.serverVersion, null, value);
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
			if (Components.TransportAppConfig.Resolver.EnableForwardingProhibitedFeature && this.IsAltRecipientProhibited)
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
			message.Directionality = MailDirectionality.Originating;
			message.UpdateDirectionalityAndScopeHeaders();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003519 File Offset: 0x00001719
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003528 File Offset: 0x00001728
		public T? GetValueTypePropValue<T>(PropertyDefinition propDefinition) where T : struct
		{
			object obj = this.messageItem.TryGetProperty(propDefinition);
			SubmissionItem.LogPropError(obj);
			if (obj == null || !(obj is T))
			{
				return null;
			}
			return new T?((T)((object)obj));
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003568 File Offset: 0x00001768
		public T GetRefTypePropValue<T>(PropertyDefinition propDefinition) where T : class
		{
			object obj = this.messageItem.TryGetProperty(propDefinition);
			SubmissionItem.LogPropError(obj);
			return obj as T;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003594 File Offset: 0x00001794
		public T GetPropValue<T>(PropertyDefinition propDefinition, T defaultValue) where T : struct
		{
			object obj = this.messageItem.TryGetProperty(propDefinition);
			SubmissionItem.LogPropError(obj);
			if (obj != null && obj is T)
			{
				return (T)((object)obj);
			}
			return defaultValue;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000035C8 File Offset: 0x000017C8
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

		// Token: 0x06000048 RID: 72 RVA: 0x0000361A File Offset: 0x0000181A
		protected void SetSessionTimeZone()
		{
			this.Session.ExTimeZone = ExTimeZone.CurrentTimeZone;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000362C File Offset: 0x0000182C
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003630 File Offset: 0x00001830
		protected void DisposeMapiObjects()
		{
			if (this.Item != null)
			{
				this.Item.Dispose();
				this.Item = null;
			}
			if (this.Session != null)
			{
				if (this.Context != null)
				{
					LatencyTracker.BeginTrackLatency(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionStoreDisposeSession, this.Context.LatencyTracker);
				}
				try
				{
					this.Session.Dispose();
					this.Session = null;
				}
				finally
				{
					if (this.Context != null)
					{
						TimeSpan additionalLatency = LatencyTracker.EndTrackLatency(LatencyComponent.StoreDriverSubmissionRpc, this.Context.LatencyTracker);
						this.Context.AddRpcLatency(additionalLatency, "Session dispose");
					}
				}
			}
			if (this.Context != null)
			{
				MailItemSubmitter.TraceLatency(this.submissionInfo, "RPC", this.Context.RpcLatency);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000036F0 File Offset: 0x000018F0
		private static void LogPropError(object value)
		{
			PropertyError propertyError = value as PropertyError;
			if (propertyError != null && propertyError.PropertyErrorCode != PropertyErrorCode.NotFound)
			{
				SubmissionItem.diag.TraceDebug<PropertyError>(0L, "Error when trying to access prop : {0}", propertyError);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003724 File Offset: 0x00001924
		private static OutboundConversionOptions GetGlobalConversionOptions()
		{
			return new OutboundConversionOptions(new EmptyRecipientCache(), Components.Configuration.FirstOrgAcceptedDomainTable.DefaultDomainName)
			{
				DsnMdnOptions = DsnMdnOptions.PropagateUserSettings,
				DsnHumanReadableWriter = Components.DsnGenerator.DsnHumanReadableWriter,
				Limits = 
				{
					MimeLimits = MimeLimits.Unlimited
				},
				QuoteDisplayNameBeforeRfc2047Encoding = Components.TransportAppConfig.ContentConversion.QuoteDisplayNameBeforeRfc2047Encoding,
				LogDirectoryPath = Components.Configuration.LocalServer.ContentConversionTracingPath
			};
		}

		// Token: 0x0400001B RID: 27
		private const int ResendMessageFlag = 128;

		// Token: 0x0400001C RID: 28
		private static readonly Microsoft.Exchange.Diagnostics.Trace diag = ExTraceGlobals.MapiSubmitTracer;

		// Token: 0x0400001D RID: 29
		private static string serverVersion;

		// Token: 0x0400001E RID: 30
		private TenantPartitionHint tenantPartitionHint;

		// Token: 0x0400001F RID: 31
		private StoreSession storeSession;

		// Token: 0x04000020 RID: 32
		private MessageItem messageItem;

		// Token: 0x04000021 RID: 33
		private OutboundConversionOptions conversionOptions;

		// Token: 0x04000022 RID: 34
		private ConversionResult conversionResult;

		// Token: 0x04000023 RID: 35
		private string mailProtocol;

		// Token: 0x04000024 RID: 36
		private SubmissionInfo submissionInfo;
	}
}
