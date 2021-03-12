using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.Mdb;
using Microsoft.Exchange.Search.TokenOperators;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x02000006 RID: 6
	internal sealed class ContentIndexingSession : IContentIndexingSession
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002770 File Offset: 0x00000970
		private ContentIndexingSession(StoreSession session, bool invalidateAnnotations)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			ContentIndexingSession.Diagnostics.TraceDebug<string>("Creating ContentIndexingSession for mailbox: {0}", session.UserLegacyDN);
			this.session = session;
			this.invalidateAnnotations = invalidateAnnotations;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000027CA File Offset: 0x000009CA
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000027D2 File Offset: 0x000009D2
		public bool EnableWordBreak { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000027DB File Offset: 0x000009DB
		internal static IDiagnosticsSession Diagnostics
		{
			get
			{
				return ContentIndexingConnectionFactory.Diagnostics;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000027E2 File Offset: 0x000009E2
		internal int NumberOfConnectionLevelFailures
		{
			get
			{
				return this.statistics.ConnectionLevelFailures;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000027EF File Offset: 0x000009EF
		internal int NumberOfDocumentLevelFailures
		{
			get
			{
				return this.statistics.MessageLevelFailures;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000027FC File Offset: 0x000009FC
		internal int NumberOfSuccessfullyAnnotatedDocuments
		{
			get
			{
				return this.statistics.MessagesSuccessfullyAnnotated;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002809 File Offset: 0x00000A09
		internal int NumberOfAnnotationAttempts
		{
			get
			{
				return this.statistics.TotalMessagesProcessed;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002816 File Offset: 0x00000A16
		internal TransportFlowStatistics Statistics
		{
			get
			{
				return this.statistics;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002820 File Offset: 0x00000A20
		public void OnBeforeItemChange(ItemChangeOperation operation, ICoreItem coreItem)
		{
			if (!this.EnableWordBreak || operation == ItemChangeOperation.ItemBind)
			{
				return;
			}
			TimeSpan elapsed = this.stopwatch.Elapsed;
			TransportFlowStatistics.ProcessingStatus processingStatus = TransportFlowStatistics.ProcessingStatus.FailedToProcess;
			ContentIndexingSession.Diagnostics.TraceDebug<string, VersionedId, ItemLevel>("ContentIndexingSession::OnBeforeItemChange for mailbox: {0}, item: {1}, level: {2}", this.session.UserLegacyDN, coreItem.Id, coreItem.ItemLevel);
			coreItem.PropertyBag.Load(CoreObjectSchema.AllPropertiesOnStore);
			Item item = null;
			try
			{
				ClientSideTimings clientSideTimings = new ClientSideTimings();
				TimeSpan elapsed2 = this.stopwatch.Elapsed;
				ContentIndexingConnection contentIndexingConnection;
				using (ContentIndexingConnectionFactory.GetConnection(out contentIndexingConnection))
				{
					if (contentIndexingConnection == null)
					{
						ContentIndexingSession.Diagnostics.TraceDebug("No connection: skip adding AnnotationToken", new object[0]);
						TransportFlowFeeder.ReportSkippedDocument();
						return;
					}
					clientSideTimings.TimeInGetConnection = this.stopwatch.Elapsed - elapsed2;
					elapsed2 = this.stopwatch.Elapsed;
					if (!this.ShouldAnnotateMessage(coreItem, contentIndexingConnection))
					{
						this.UpdateTime(elapsed, TransportFlowStatistics.ProcessingStatus.AnnotationSkipped);
						ContentIndexingSession.Diagnostics.TraceDebug("Skip adding AnnotationToken", new object[0]);
						TransportFlowFeeder.ReportSkippedDocument();
						return;
					}
					clientSideTimings.TimeInShouldAnnotateMessage = this.stopwatch.Elapsed - elapsed2;
					elapsed2 = this.stopwatch.Elapsed;
					using (Item item2 = new Item(coreItem, false))
					{
						item = Item.ConvertFrom(item2, item2.Session);
						item2.CoreObject = null;
					}
					clientSideTimings.TimeInMessageItemConversion = this.stopwatch.Elapsed - elapsed2;
					elapsed2 = this.stopwatch.Elapsed;
					bool shouldBypassNlg = false;
					object obj = coreItem.PropertyBag.TryGetProperty(ItemSchema.ReceivedTime);
					if (!PropertyError.IsPropertyError(obj))
					{
						ExDateTime t = (ExDateTime)obj;
						if (t < ExDateTime.UtcNow - contentIndexingConnection.AgeOfItemToBypassNlg)
						{
							shouldBypassNlg = true;
						}
					}
					clientSideTimings.TimeDeterminingAgeOfItem = this.stopwatch.Elapsed - elapsed2;
					elapsed2 = this.stopwatch.Elapsed;
					try
					{
						TransportFlowOperatorTimings transportFlowOperatorTimings = null;
						bool flag = false;
						using (Stream stream = TemporaryStorage.Create())
						{
							if (this.outboundConversionOptions == null && !this.InitializeOutboundConversionOptions())
							{
								ContentIndexingSession.Diagnostics.TraceDebug("Unable to Initialize the OutboundConversionOptions: skip adding AnnotationToken", new object[0]);
								TransportFlowFeeder.ReportSkippedDocument();
								return;
							}
							ConversionResult conversionResult = ItemConversion.ConvertItemToSummaryTnef(item, stream, this.outboundConversionOptions);
							ContentIndexingSession.Diagnostics.TraceDebug("Item converted to summary TNEF. BodySize: {0}, RecipientCount: {1}, AttachmentCount: {2}, AccumulatedAttachmentSize: {3}", new object[]
							{
								conversionResult.BodySize,
								conversionResult.RecipientCount,
								conversionResult.AttachmentCount,
								conversionResult.AccumulatedAttachmentSize
							});
							clientSideTimings.TimeInMimeConversion = this.stopwatch.Elapsed - elapsed2;
							elapsed2 = this.stopwatch.Elapsed;
							stream.Position = 0L;
							using (Stream stream2 = TemporaryStorage.Create())
							{
								contentIndexingConnection.ProcessMessage(stream, stream2, shouldBypassNlg);
								stream2.Position = 0L;
								foreach (ISerializableProperty serializableProperty in SerializableProperties.DeserializeFrom(stream2))
								{
									SerializablePropertyId id = serializableProperty.Id;
									switch (id)
									{
									case SerializablePropertyId.AnnotationToken:
									{
										SerializableStreamProperty serializableStreamProperty = (SerializableStreamProperty)serializableProperty;
										using (Stream stream3 = coreItem.PropertyBag.OpenPropertyStream(ItemSchema.AnnotationToken, PropertyOpenMode.Create))
										{
											serializableStreamProperty.CopyTo(stream3);
											ContentIndexingSession.Diagnostics.TraceDebug<long>("Got annotation token, length: {0}", stream3.Length);
											continue;
										}
										break;
									}
									case SerializablePropertyId.Tasks:
										coreItem.PropertyBag.SetProperty(ItemSchema.XmlExtractedTasks, serializableProperty.Value);
										continue;
									case (SerializablePropertyId)3:
									case SerializablePropertyId.Addresses:
										goto IL_3FF;
									case SerializablePropertyId.Meetings:
										coreItem.PropertyBag.SetProperty(ItemSchema.XmlExtractedMeetings, serializableProperty.Value);
										continue;
									case SerializablePropertyId.Keywords:
										break;
									default:
										switch (id)
										{
										case SerializablePropertyId.Language:
											coreItem.PropertyBag.SetProperty(ItemSchema.DetectedLanguage, serializableProperty.Value);
											flag = true;
											continue;
										case SerializablePropertyId.OperatorTiming:
										{
											string text = (string)serializableProperty.Value;
											if (!string.IsNullOrWhiteSpace(text))
											{
												transportFlowOperatorTimings = new TransportFlowOperatorTimings(text);
												this.statistics.UpdateOperatorTimings(transportFlowOperatorTimings);
												continue;
											}
											continue;
										}
										default:
											goto IL_3FF;
										}
										break;
									}
									coreItem.PropertyBag.SetProperty(ItemSchema.XmlExtractedKeywords, serializableProperty.Value);
									continue;
									IL_3FF:
									if (serializableProperty.Type == SerializablePropertyType.Stream)
									{
										SerializableStreamProperty serializableStreamProperty = (SerializableStreamProperty)serializableProperty;
										serializableStreamProperty.CopyTo(Stream.Null);
									}
								}
							}
						}
						TransportFlowFeeder.ReportTimings(transportFlowOperatorTimings, (long)elapsed.TotalMilliseconds, !flag);
						TransportFlowFeeder.ReportClientSideTimings(clientSideTimings);
						processingStatus = TransportFlowStatistics.ProcessingStatus.Success;
					}
					catch (FastTransientDocumentException ex)
					{
						if (ex.InnerException is TimeoutException || ex.InnerException is OperationCanceledException)
						{
							processingStatus = TransportFlowStatistics.ProcessingStatus.FailedToConnect;
							contentIndexingConnection.ProcessAnnotationFailure(true, ex);
						}
						else
						{
							processingStatus = TransportFlowStatistics.ProcessingStatus.FailedToProcess;
							contentIndexingConnection.ProcessAnnotationFailure(false, ex);
						}
					}
					catch (FastPermanentDocumentException exception)
					{
						processingStatus = TransportFlowStatistics.ProcessingStatus.FailedToProcess;
						contentIndexingConnection.ProcessAnnotationFailure(false, exception);
					}
					catch (FastConnectionException exception2)
					{
						processingStatus = TransportFlowStatistics.ProcessingStatus.FailedToConnect;
						contentIndexingConnection.ProcessAnnotationFailure(true, exception2);
					}
					catch (Exception ex2)
					{
						ContentIndexingSession.Diagnostics.TraceDebug<Exception>("Unexpected exception {0}", ex2);
						processingStatus = TransportFlowStatistics.ProcessingStatus.FailedToProcess;
						contentIndexingConnection.ProcessAnnotationFailure(false, ex2);
					}
					finally
					{
						if (processingStatus == TransportFlowStatistics.ProcessingStatus.Success)
						{
							contentIndexingConnection.MarkAlive();
						}
					}
				}
			}
			finally
			{
				if (item != null)
				{
					item.CoreObject = null;
					item.Dispose();
				}
			}
			this.UpdateTime(elapsed, processingStatus);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002E78 File Offset: 0x00001078
		internal static ContentIndexingSession CreateSession(StoreSession session, bool invalidateAnnotations)
		{
			ContentIndexingConnectionFactory.InitializeIfNeeded();
			return new ContentIndexingSession(session, invalidateAnnotations);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002E88 File Offset: 0x00001088
		private bool InitializeOutboundConversionOptions()
		{
			OrganizationId organizationId;
			if (this.session is MailboxSession)
			{
				organizationId = ((MailboxSession)this.session).MailboxOwner.MailboxInfo.OrganizationId;
			}
			else
			{
				if (!(this.session is PublicFolderSession))
				{
					throw new ArgumentException("Session is expected to be mailbox session or public folder session.", "session");
				}
				organizationId = ((PublicFolderSession)this.session).OrganizationId;
			}
			ADSessionSettings sessionSettings;
			if (organizationId != null)
			{
				sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerId(null, null), organizationId, organizationId, false);
			}
			else
			{
				sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			}
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 417, "InitializeOutboundConversionOptions", "f:\\15.00.1497\\sources\\dev\\Search\\src\\Fast\\ContentIndexingSession.cs");
			if (tenantOrTopologyConfigurationSession == null)
			{
				return false;
			}
			AcceptedDomain defaultAcceptedDomain = tenantOrTopologyConfigurationSession.GetDefaultAcceptedDomain();
			this.outboundConversionOptions = new OutboundConversionOptions(defaultAcceptedDomain.DomainName.Domain);
			this.outboundConversionOptions.RecipientCache = new EmptyRecipientCache();
			return true;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002F60 File Offset: 0x00001160
		private bool IsItemEncrypted(ICoreItem coreItem)
		{
			string valueOrDefault = coreItem.PropertyBag.GetValueOrDefault<string>(StoreObjectSchema.ContentClass);
			if (ObjectClass.IsSmime(valueOrDefault) && !ObjectClass.IsSmimeClearSigned(valueOrDefault))
			{
				ContentIndexingSession.Diagnostics.TraceDebug("Skip adding AnnotationToken due to Smime Encryption.", new object[0]);
				return true;
			}
			if (ObjectClass.IsRightsManagedContentClass(valueOrDefault))
			{
				ContentIndexingSession.Diagnostics.TraceDebug("Skip adding AnnotationToken due to RMS.", new object[0]);
				return true;
			}
			return false;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002FC8 File Offset: 0x000011C8
		private bool ShouldAnnotateMessage(ICoreItem coreItem, ContentIndexingConnection contentIndexingConnection)
		{
			if (contentIndexingConnection.AlwaysInvalidateAnnotationToken || this.invalidateAnnotations)
			{
				coreItem.PropertyBag.Delete(ItemSchema.AnnotationToken);
			}
			if (contentIndexingConnection.SkipWordBreakingDuringMRS)
			{
				ContentIndexingSession.Diagnostics.TraceDebug("Skip adding AnnotationToken. SkipWordBreakingDuringMRS setting turned on", new object[0]);
				return false;
			}
			if (coreItem.ItemLevel != ItemLevel.TopLevel)
			{
				return false;
			}
			bool valueOrDefault = coreItem.PropertyBag.GetValueOrDefault<bool>(MessageItemSchema.IsAssociated);
			if (valueOrDefault)
			{
				return false;
			}
			string valueOrDefault2 = coreItem.PropertyBag.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, string.Empty);
			if (XsoUtil.ShouldSkipMessageClass(valueOrDefault2))
			{
				return false;
			}
			if (this.IsItemEncrypted(coreItem))
			{
				return false;
			}
			byte[] array = null;
			object obj = coreItem.PropertyBag.TryGetProperty(ItemSchema.AnnotationToken);
			if (PropertyError.IsPropertyNotFound(obj))
			{
				return true;
			}
			if (PropertyError.IsPropertyValueTooBig(obj))
			{
				using (Stream stream = coreItem.PropertyBag.OpenPropertyStream(ItemSchema.AnnotationToken, PropertyOpenMode.ReadOnly))
				{
					array = new byte[TokenInfo.Version.Length];
					stream.Read(array, 0, array.Length);
					goto IL_11A;
				}
			}
			if (PropertyError.IsPropertyError(obj))
			{
				ContentIndexingSession.Diagnostics.Assert(false, "What else can happen? Error {0}", new object[]
				{
					obj
				});
				return false;
			}
			array = (byte[])obj;
			IL_11A:
			return !TokenInfo.IsVersionSupported(array);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000310C File Offset: 0x0000130C
		private void UpdateTime(TimeSpan startTime, TransportFlowStatistics.ProcessingStatus status)
		{
			this.statistics.UpdateProcessingTimes(this.stopwatch.Elapsed - startTime, status);
		}

		// Token: 0x04000013 RID: 19
		private readonly StoreSession session;

		// Token: 0x04000014 RID: 20
		private readonly TransportFlowStatistics statistics = new TransportFlowStatistics();

		// Token: 0x04000015 RID: 21
		private readonly Stopwatch stopwatch = Stopwatch.StartNew();

		// Token: 0x04000016 RID: 22
		private readonly bool invalidateAnnotations;

		// Token: 0x04000017 RID: 23
		private OutboundConversionOptions outboundConversionOptions;
	}
}
