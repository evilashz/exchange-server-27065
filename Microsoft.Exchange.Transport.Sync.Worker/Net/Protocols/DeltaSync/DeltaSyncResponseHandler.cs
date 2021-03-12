using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Net.Protocols.DeltaSync.ItemOperationsResponse;
using Microsoft.Exchange.Net.Protocols.DeltaSync.SendResponse;
using Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse;
using Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessResponse;
using Microsoft.Exchange.Net.Protocols.DeltaSync.SyncResponse;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync
{
	// Token: 0x02000073 RID: 115
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class DeltaSyncResponseHandler
	{
		// Token: 0x0600052A RID: 1322 RVA: 0x0001839A File Offset: 0x0001659A
		internal DeltaSyncResponseHandler(SyncLogSession syncLogSession)
		{
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			this.syncLogSession = syncLogSession;
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x000183B4 File Offset: 0x000165B4
		private XmlSerializer SyncDeserializer
		{
			get
			{
				if (this.syncDeserializer == null)
				{
					this.syncDeserializer = new XmlSerializer(typeof(Sync));
				}
				return this.syncDeserializer;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x000183D9 File Offset: 0x000165D9
		private XmlSerializer ItemOperationsDeserializer
		{
			get
			{
				if (this.itemOperationsDeserializer == null)
				{
					this.itemOperationsDeserializer = new XmlSerializer(typeof(ItemOperations));
				}
				return this.itemOperationsDeserializer;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x000183FE File Offset: 0x000165FE
		private XmlSerializer SettingsDeserializer
		{
			get
			{
				if (this.settingsDeserializer == null)
				{
					this.settingsDeserializer = new XmlSerializer(typeof(Settings));
				}
				return this.settingsDeserializer;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x00018423 File Offset: 0x00016623
		private XmlSerializer SendDeserializer
		{
			get
			{
				if (this.sendDeserializer == null)
				{
					this.sendDeserializer = new XmlSerializer(typeof(Send));
				}
				return this.sendDeserializer;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00018448 File Offset: 0x00016648
		private XmlSerializer StatelessDeserializer
		{
			get
			{
				if (this.statelessDeserializer == null)
				{
					this.statelessDeserializer = new XmlSerializer(typeof(Stateless));
				}
				return this.statelessDeserializer;
			}
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001850C File Offset: 0x0001670C
		internal DeltaSyncResultData ParseDeltaSyncResponse(DownloadResult deltaSyncResponse, DeltaSyncCommandType commandType)
		{
			this.syncLogSession.LogDebugging((TSLID)670UL, DeltaSyncResponseHandler.Tracer, "Try Parse DS Response for Command Type: {0}", new object[]
			{
				commandType
			});
			if (deltaSyncResponse.ResponseStream == null)
			{
				this.syncLogSession.LogError((TSLID)671UL, DeltaSyncResponseHandler.Tracer, "Http Response Stream cannot be null", new object[0]);
				throw new InvalidServerResponseException(new HttpResponseStreamNullException());
			}
			switch (commandType)
			{
			case DeltaSyncCommandType.Sync:
			{
				Sync syncResponse = (Sync)this.ParseResponse(delegate
				{
					Microsoft.Exchange.Diagnostics.Components.ContentAggregation.ExTraceGlobals.FaultInjectionTracer.TraceTest(4100336957U);
					return (Sync)this.SyncDeserializer.Deserialize(deltaSyncResponse.ResponseStream);
				});
				return new DeltaSyncResultData(syncResponse);
			}
			case DeltaSyncCommandType.Fetch:
			{
				ItemOperations itemOperationsResponse = this.ParseFetchResponse(deltaSyncResponse);
				return new DeltaSyncResultData(itemOperationsResponse);
			}
			case DeltaSyncCommandType.Settings:
			{
				Settings settingsResponse = (Settings)this.ParseResponse(() => (Settings)this.SettingsDeserializer.Deserialize(deltaSyncResponse.ResponseStream));
				return new DeltaSyncResultData(settingsResponse);
			}
			case DeltaSyncCommandType.Send:
			{
				Send sendResponse = (Send)this.ParseResponse(() => (Send)this.SendDeserializer.Deserialize(deltaSyncResponse.ResponseStream));
				return new DeltaSyncResultData(sendResponse);
			}
			case DeltaSyncCommandType.Stateless:
			{
				Stateless statelessResponse = (Stateless)this.ParseResponse(() => (Stateless)this.StatelessDeserializer.Deserialize(deltaSyncResponse.ResponseStream));
				return new DeltaSyncResultData(statelessResponse);
			}
			default:
				this.syncLogSession.LogError((TSLID)672UL, DeltaSyncResponseHandler.Tracer, "Unknown DeltaSync Command Type {0}", new object[]
				{
					commandType
				});
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unknown Command: {0}", new object[]
				{
					commandType
				}));
			}
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x000186E8 File Offset: 0x000168E8
		private static bool TryGetIncludeContentId(ItemOperations fetchResponse, out string contentId)
		{
			contentId = null;
			if (fetchResponse.Responses != null && fetchResponse.Responses.Fetch != null && fetchResponse.Responses.Fetch.Message != null && fetchResponse.Responses.Fetch.Message.Include != null && fetchResponse.Responses.Fetch.Message.Include.href != null && fetchResponse.Responses.Fetch.Message.Include.href.StartsWith("cid:", StringComparison.OrdinalIgnoreCase))
			{
				contentId = fetchResponse.Responses.Fetch.Message.Include.href.Remove(0, "cid:".Length);
				return true;
			}
			return false;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x000187B4 File Offset: 0x000169B4
		private object ParseResponse(DeltaSyncResponseHandler.ResponseParser parser)
		{
			Exception ex = null;
			try
			{
				return parser();
			}
			catch (InvalidOperationException ex2)
			{
				ex = ex2;
			}
			catch (InvalidCastException ex3)
			{
				ex = ex3;
			}
			this.syncLogSession.LogError((TSLID)673UL, DeltaSyncResponseHandler.Tracer, "Response Parsing error: {0}", new object[]
			{
				ex
			});
			throw new InvalidServerResponseException(ex);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001882C File Offset: 0x00016A2C
		private ItemOperations ParseFetchResponse(DownloadResult deltaSyncResponse)
		{
			Microsoft.Exchange.Diagnostics.Components.ContentAggregation.ExTraceGlobals.FaultInjectionTracer.TraceTest(3798347069U);
			string text = deltaSyncResponse.ResponseHeaders[HttpResponseHeader.ContentType];
			if (text != null && text.Equals(DeltaSyncCommon.TextXmlContentType, StringComparison.OrdinalIgnoreCase))
			{
				return this.ParseItemOperationsXmlResponse(deltaSyncResponse.ResponseStream);
			}
			if (text != null && text.Equals(DeltaSyncCommon.ApplicationXopXmlContentType, StringComparison.OrdinalIgnoreCase))
			{
				return this.ParseFetchMtomResponse(deltaSyncResponse);
			}
			this.syncLogSession.LogError((TSLID)674UL, DeltaSyncResponseHandler.Tracer, "Unexpected Response Content Type: {0}", new object[]
			{
				text
			});
			throw new InvalidServerResponseException(new UnexpectedContentTypeException(text));
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x000188C8 File Offset: 0x00016AC8
		private ItemOperations ParseFetchMtomResponse(DownloadResult deltaSyncResponse)
		{
			Exception ex = null;
			MimeReader mimeReader = new MimeReader(deltaSyncResponse.ResponseStream);
			if (mimeReader.ReadFirstChildPart() && mimeReader.ReadFirstChildPart())
			{
				ItemOperations itemOperations = this.ParseItemOperationsXmlResponse(mimeReader.GetContentReadStream());
				string text = null;
				if (DeltaSyncResponseHandler.TryGetIncludeContentId(itemOperations, out text))
				{
					while (mimeReader.ReadNextPart())
					{
						MimeHeaderReader headerReader = mimeReader.HeaderReader;
						while (headerReader.ReadNextHeader())
						{
							if (headerReader.HeaderId == HeaderId.ContentId)
							{
								if (headerReader.Value == null || headerReader.Value.Length < 2)
								{
									break;
								}
								string value = headerReader.Value.Substring(1, headerReader.Value.Length - 2);
								if (!text.Equals(value, StringComparison.OrdinalIgnoreCase))
								{
									break;
								}
								Stream contentReadStream = mimeReader.GetContentReadStream();
								itemOperations.Responses.Fetch.Message.EmailMessage = TemporaryStorage.Create();
								if (DeltaSyncDecompressor.TryDeCompress(contentReadStream, itemOperations.Responses.Fetch.Message.EmailMessage))
								{
									itemOperations.Responses.Fetch.Message.EmailMessage.Position = 0L;
									return itemOperations;
								}
								itemOperations.Responses.Fetch.Message.EmailMessage.Close();
								itemOperations.Responses.Fetch.Message.EmailMessage = null;
								ex = new MessageDecompressionFailedException(itemOperations.Responses.Fetch.ServerId);
								break;
							}
						}
					}
				}
			}
			if (ex == null)
			{
				ex = new MTOMParsingFailedException();
			}
			this.syncLogSession.LogError((TSLID)675UL, DeltaSyncResponseHandler.Tracer, "Fetch Response Parsing error: {0}", new object[]
			{
				ex
			});
			throw new InvalidServerResponseException(ex);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00018AA4 File Offset: 0x00016CA4
		private ItemOperations ParseItemOperationsXmlResponse(Stream responseStream)
		{
			return (ItemOperations)this.ParseResponse(() => (ItemOperations)this.ItemOperationsDeserializer.Deserialize(responseStream));
		}

		// Token: 0x040002D4 RID: 724
		private const string ContentIdPrefix = "cid:";

		// Token: 0x040002D5 RID: 725
		private static readonly Trace Tracer = Microsoft.Exchange.Diagnostics.Components.Net.ExTraceGlobals.DeltaSyncResponseHandlerTracer;

		// Token: 0x040002D6 RID: 726
		private readonly SyncLogSession syncLogSession;

		// Token: 0x040002D7 RID: 727
		private XmlSerializer syncDeserializer;

		// Token: 0x040002D8 RID: 728
		private XmlSerializer itemOperationsDeserializer;

		// Token: 0x040002D9 RID: 729
		private XmlSerializer settingsDeserializer;

		// Token: 0x040002DA RID: 730
		private XmlSerializer sendDeserializer;

		// Token: 0x040002DB RID: 731
		private XmlSerializer statelessDeserializer;

		// Token: 0x02000074 RID: 116
		// (Invoke) Token: 0x06000538 RID: 1336
		private delegate object ResponseParser();
	}
}
