using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.TextProcessing.Boomerang;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002B2 RID: 690
	internal abstract class PipelineContext : DisposableBase, IUMCreateMessage
	{
		// Token: 0x060014D3 RID: 5331 RVA: 0x00059925 File Offset: 0x00057B25
		internal PipelineContext()
		{
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x00059930 File Offset: 0x00057B30
		internal PipelineContext(SubmissionHelper helper)
		{
			bool flag = false;
			try
			{
				this.helper = helper;
				this.cultureInfo = new CultureInfo(helper.CultureInfo);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.Dispose();
				}
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x0005997C File Offset: 0x00057B7C
		// (set) Token: 0x060014D6 RID: 5334 RVA: 0x00059984 File Offset: 0x00057B84
		public MessageItem MessageToSubmit
		{
			get
			{
				return this.messageToSubmit;
			}
			protected set
			{
				this.messageToSubmit = value;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x0005998D File Offset: 0x00057B8D
		// (set) Token: 0x060014D8 RID: 5336 RVA: 0x00059995 File Offset: 0x00057B95
		public string MessageID
		{
			get
			{
				return this.messageID;
			}
			protected set
			{
				this.messageID = value;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060014D9 RID: 5337
		internal abstract Pipeline Pipeline { get; }

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060014DA RID: 5338 RVA: 0x0005999E File Offset: 0x00057B9E
		internal PhoneNumber CallerId
		{
			get
			{
				return this.helper.CallerId;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060014DB RID: 5339 RVA: 0x000599AB File Offset: 0x00057BAB
		internal Guid TenantGuid
		{
			get
			{
				return this.helper.TenantGuid;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060014DC RID: 5340 RVA: 0x000599B8 File Offset: 0x00057BB8
		internal int ProcessedCount
		{
			get
			{
				return this.processedCount;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060014DD RID: 5341 RVA: 0x000599C0 File Offset: 0x00057BC0
		// (set) Token: 0x060014DE RID: 5342 RVA: 0x000599C8 File Offset: 0x00057BC8
		internal ExDateTime SentTime
		{
			get
			{
				return this.sentTime;
			}
			set
			{
				this.sentTime = value;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060014DF RID: 5343 RVA: 0x000599D1 File Offset: 0x00057BD1
		internal CultureInfo CultureInfo
		{
			get
			{
				return this.cultureInfo;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060014E0 RID: 5344 RVA: 0x000599DC File Offset: 0x00057BDC
		// (set) Token: 0x060014E1 RID: 5345 RVA: 0x00059A29 File Offset: 0x00057C29
		protected internal string HeaderFileName
		{
			get
			{
				if (string.IsNullOrEmpty(this.headerFileName))
				{
					Guid guid = Guid.NewGuid();
					this.headerFileName = Path.Combine(Utils.VoiceMailFilePath, guid.ToString() + ".txt");
				}
				return this.headerFileName;
			}
			protected set
			{
				this.headerFileName = value;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x00059A32 File Offset: 0x00057C32
		// (set) Token: 0x060014E3 RID: 5347 RVA: 0x00059A3F File Offset: 0x00057C3F
		protected internal string CallerAddress
		{
			get
			{
				return this.helper.CallerAddress;
			}
			protected set
			{
				this.helper.CallerAddress = value;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x00059A4D File Offset: 0x00057C4D
		// (set) Token: 0x060014E5 RID: 5349 RVA: 0x00059A5A File Offset: 0x00057C5A
		protected internal string CallerIdDisplayName
		{
			get
			{
				return this.helper.CallerIdDisplayName;
			}
			protected set
			{
				this.helper.CallerIdDisplayName = value;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x00059A68 File Offset: 0x00057C68
		// (set) Token: 0x060014E7 RID: 5351 RVA: 0x00059A70 File Offset: 0x00057C70
		protected internal string MessageType
		{
			internal get
			{
				return this.messageType;
			}
			set
			{
				this.messageType = value;
			}
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x00059A7C File Offset: 0x00057C7C
		public virtual void PrepareUnProtectedMessage()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "PipelineContext:PrepareUnProtectedMessage.", new object[0]);
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				this.messageToSubmit = MessageItem.CreateInMemory(StoreObjectSchema.ContentConversionProperties);
				disposeGuard.Add<MessageItem>(this.messageToSubmit);
				this.SetMessageProperties();
				disposeGuard.Success();
			}
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x00059AFC File Offset: 0x00057CFC
		public virtual void PrepareProtectedMessage()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x00059B03 File Offset: 0x00057D03
		public virtual void PrepareNDRForFailureToGenerateProtectedMessage()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x00059B0C File Offset: 0x00057D0C
		public virtual PipelineDispatcher.WIThrottleData GetThrottlingData()
		{
			return new PipelineDispatcher.WIThrottleData
			{
				Key = this.GetMailboxServerId(),
				RecipientId = this.GetRecipientIdForThrottling(),
				WorkItemType = PipelineDispatcher.ThrottledWorkItemType.NonCDRWorkItem
			};
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x00059B40 File Offset: 0x00057D40
		public virtual void PostCompletion()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "PipelineContext - Deleting header file '{0}'", new object[]
			{
				this.headerFileName
			});
			Util.TryDeleteFile(this.headerFileName);
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x00059B80 File Offset: 0x00057D80
		internal static PipelineContext FromHeaderFile(string headerFile)
		{
			PipelineContext pipelineContext = null;
			PipelineContext result;
			try
			{
				ContactInfo contactInfo = null;
				string text = null;
				int num = 0;
				ExDateTime exDateTime = default(ExDateTime);
				string text2 = null;
				SubmissionHelper submissionHelper = new SubmissionHelper();
				using (StreamReader streamReader = File.OpenText(headerFile))
				{
					string text3;
					while ((text3 = streamReader.ReadLine()) != null)
					{
						string[] array = text3.Split(" : ".ToCharArray(), 2, StringSplitOptions.RemoveEmptyEntries);
						if (array != null && array.Length == 2)
						{
							string key;
							switch (key = array[0])
							{
							case "CallId":
								submissionHelper.CallId = array[1];
								continue;
							case "CallerId":
								submissionHelper.CallerId = PhoneNumber.Parse(array[1]);
								continue;
							case "SenderAddress":
							{
								string text4 = array[1];
								continue;
							}
							case "RecipientName":
								submissionHelper.RecipientName = array[1];
								continue;
							case "RecipientObjectGuid":
								submissionHelper.RecipientObjectGuid = new Guid(array[1]);
								continue;
							case "CultureInfo":
								submissionHelper.CultureInfo = array[1];
								continue;
							case "CallerNAme":
								submissionHelper.CallerName = array[1];
								continue;
							case "CallerIdDisplayName":
								submissionHelper.CallerIdDisplayName = array[1];
								continue;
							case "CallerAddress":
								submissionHelper.CallerAddress = array[1];
								continue;
							case "MessageType":
								text = array[1];
								continue;
							case "ProcessedCount":
								num = Convert.ToInt32(array[1], CultureInfo.InvariantCulture) + 1;
								continue;
							case "ContactInfo":
								contactInfo = (CommonUtil.Base64Deserialize(array[1]) as ContactInfo);
								continue;
							case "MessageID":
								text2 = array[1];
								continue;
							case "SentTime":
							{
								DateTime dateTime = Convert.ToDateTime(array[1], CultureInfo.InvariantCulture);
								exDateTime = new ExDateTime(ExTimeZone.CurrentTimeZone, dateTime);
								continue;
							}
							case "TenantGuid":
								submissionHelper.TenantGuid = new Guid(array[1]);
								continue;
							}
							submissionHelper.CustomHeaders[array[0]] = array[1];
						}
					}
				}
				string key2;
				if ((key2 = text) != null)
				{
					if (<PrivateImplementationDetails>{40E2B4CE-6A40-4DA1-B0B4-CD6B52D7182A}.$$method0x600147c-2 == null)
					{
						<PrivateImplementationDetails>{40E2B4CE-6A40-4DA1-B0B4-CD6B52D7182A}.$$method0x600147c-2 = new Dictionary<string, int>(10)
						{
							{
								"SMTPVoiceMail",
								0
							},
							{
								"Fax",
								1
							},
							{
								"MissedCall",
								2
							},
							{
								"IncomingCallLog",
								3
							},
							{
								"OutgoingCallLog",
								4
							},
							{
								"OCSNotification",
								5
							},
							{
								"XSOVoiceMail",
								6
							},
							{
								"PartnerTranscriptionRequest",
								7
							},
							{
								"CDR",
								8
							},
							{
								"HealthCheck",
								9
							}
						};
					}
					int num3;
					if (<PrivateImplementationDetails>{40E2B4CE-6A40-4DA1-B0B4-CD6B52D7182A}.$$method0x600147c-2.TryGetValue(key2, out num3))
					{
						switch (num3)
						{
						case 0:
							if (num < PipelineWorkItem.ProcessedCountMax - 1)
							{
								pipelineContext = new VoiceMessagePipelineContext(submissionHelper);
							}
							else
							{
								pipelineContext = new MissedCallPipelineContext(submissionHelper);
							}
							break;
						case 1:
							pipelineContext = new FaxPipelineContext(submissionHelper);
							break;
						case 2:
							pipelineContext = new MissedCallPipelineContext(submissionHelper);
							break;
						case 3:
							pipelineContext = new IncomingCallLogPipelineContext(submissionHelper);
							break;
						case 4:
							pipelineContext = new OutgoingCallLogPipelineContext(submissionHelper);
							break;
						case 5:
							pipelineContext = OCSPipelineContext.Deserialize((string)submissionHelper.CustomHeaders["OCSNotificationData"]);
							text2 = pipelineContext.messageID;
							exDateTime = pipelineContext.sentTime;
							break;
						case 6:
							pipelineContext = new XSOVoiceMessagePipelineContext(submissionHelper);
							break;
						case 7:
							pipelineContext = new PartnerTranscriptionRequestPipelineContext(submissionHelper);
							break;
						case 8:
							pipelineContext = CDRPipelineContext.Deserialize((string)submissionHelper.CustomHeaders["CDRData"]);
							break;
						case 9:
							pipelineContext = new HealthCheckPipelineContext(Path.GetFileNameWithoutExtension(headerFile));
							break;
						default:
							goto IL_45F;
						}
						if (text2 == null)
						{
							text2 = Guid.NewGuid().ToString();
							exDateTime = ExDateTime.Now;
						}
						pipelineContext.HeaderFileName = headerFile;
						pipelineContext.processedCount = num;
						if (contactInfo != null)
						{
							IUMResolveCaller iumresolveCaller = pipelineContext as IUMResolveCaller;
							if (iumresolveCaller != null)
							{
								iumresolveCaller.ContactInfo = contactInfo;
							}
						}
						pipelineContext.sentTime = exDateTime;
						pipelineContext.messageID = text2;
						pipelineContext.WriteHeaderFile(headerFile);
						return pipelineContext;
					}
				}
				IL_45F:
				throw new HeaderFileArgumentInvalidException(string.Format(CultureInfo.InvariantCulture, "{0}: {1}", new object[]
				{
					"MessageType",
					text
				}));
			}
			catch (IOException ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "Failed to parse the header file {0} because its not closed by thread creating the file.  Error={1}", new object[]
				{
					headerFile,
					ex
				});
				if (pipelineContext != null)
				{
					pipelineContext.Dispose();
					pipelineContext = null;
				}
				result = null;
			}
			catch (InvalidObjectGuidException ex2)
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.VoiceMailTracer, 0, "Couldn't find the recipient for this message. Error={0}", new object[]
				{
					ex2
				});
				if (pipelineContext != null)
				{
					pipelineContext.Dispose();
					pipelineContext = null;
				}
				throw;
			}
			catch (InvalidTenantGuidException ex3)
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.VoiceMailTracer, 0, "Couldn't find the tenant for this message. Error={0}", new object[]
				{
					ex3
				});
				if (pipelineContext != null)
				{
					pipelineContext.Dispose();
					pipelineContext = null;
				}
				throw;
			}
			catch (NonUniqueRecipientException ex4)
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.VoiceMailTracer, 0, "Multiple objects found for the recipient. Error={0}", new object[]
				{
					ex4
				});
				if (pipelineContext != null)
				{
					pipelineContext.Dispose();
					pipelineContext = null;
				}
				throw;
			}
			return result;
		}

		// Token: 0x060014EE RID: 5358
		internal abstract void WriteCustomHeaderFields(StreamWriter headerStream);

		// Token: 0x060014EF RID: 5359
		public abstract string GetMailboxServerId();

		// Token: 0x060014F0 RID: 5360
		public abstract string GetRecipientIdForThrottling();

		// Token: 0x060014F1 RID: 5361 RVA: 0x0005A1D8 File Offset: 0x000583D8
		internal virtual void SaveMessage()
		{
			this.WriteHeaderFile(this.HeaderFileName);
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x0005A1E6 File Offset: 0x000583E6
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "PipelineContext.Dispose() called", new object[0]);
			}
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0005A20B File Offset: 0x0005840B
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PipelineContext>(this);
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x0005A214 File Offset: 0x00058414
		protected virtual void SetMessageProperties()
		{
			IUMResolveCaller iumresolveCaller = this as IUMResolveCaller;
			if (iumresolveCaller != null)
			{
				ExAssert.RetailAssert(iumresolveCaller.ContactInfo != null, "ResolveCallerStage should always set the ContactInfo.");
				IUMCAMessage iumcamessage = (IUMCAMessage)this;
				UMSubscriber umsubscriber = iumcamessage.CAMessageRecipient as UMSubscriber;
				UMDialPlan dialPlan = (umsubscriber != null) ? umsubscriber.DialPlan : null;
				PhoneNumber pstnCallbackTelephoneNumber = this.CallerId.GetPstnCallbackTelephoneNumber(iumresolveCaller.ContactInfo, dialPlan);
				this.messageToSubmit.From = iumresolveCaller.ContactInfo.CreateParticipant(pstnCallbackTelephoneNumber, this.CultureInfo);
				XsoUtil.SetVoiceMessageSenderProperties(this.messageToSubmit, iumresolveCaller.ContactInfo, dialPlan, this.CallerId);
				this.messageToSubmit.InternetMessageId = BoomerangProvider.Instance.FormatInternetMessageId(this.MessageID, Utils.GetHostFqdn());
				this.messageToSubmit[ItemSchema.SentTime] = this.SentTime;
			}
			this.messageToSubmit.AutoResponseSuppress = AutoResponseSuppress.All;
			this.messageToSubmit[MessageItemSchema.CallId] = this.helper.CallId;
			IUMCAMessage iumcamessage2 = this as IUMCAMessage;
			if (iumcamessage2 != null)
			{
				this.MessageToSubmit.Recipients.Add(new Participant(iumcamessage2.CAMessageRecipient.ADRecipient));
				IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(iumcamessage2.CAMessageRecipient.ADRecipient.OrganizationId);
				this.MessageToSubmit.Sender = new Participant(iadsystemConfigurationLookup.GetMicrosoftExchangeRecipient());
			}
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x0005A370 File Offset: 0x00058570
		protected void WriteHeaderFile(string headerFileName)
		{
			using (FileStream fileStream = File.Open(headerFileName, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				using (StreamWriter streamWriter = new StreamWriter(fileStream))
				{
					if (this.MessageType != null)
					{
						streamWriter.WriteLine("MessageType : " + this.MessageType);
					}
					streamWriter.WriteLine("ProcessedCount : " + this.processedCount.ToString(CultureInfo.InvariantCulture));
					if (this.messageID != null)
					{
						streamWriter.WriteLine("MessageID : " + this.messageID);
					}
					if (this.sentTime.Year != 1)
					{
						streamWriter.WriteLine("SentTime : " + this.sentTime.ToString(CultureInfo.InvariantCulture));
					}
					this.WriteCommonHeaderFields(streamWriter);
					this.WriteCustomHeaderFields(streamWriter);
				}
			}
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x0005A45C File Offset: 0x0005865C
		protected virtual void WriteCommonHeaderFields(StreamWriter headerStream)
		{
			if (!this.CallerId.IsEmpty)
			{
				headerStream.WriteLine("CallerId : " + this.CallerId.ToDial);
			}
			if (this.helper.RecipientName != null)
			{
				headerStream.WriteLine("RecipientName : " + this.helper.RecipientName);
			}
			if (this.helper.RecipientObjectGuid != Guid.Empty)
			{
				headerStream.WriteLine("RecipientObjectGuid : " + this.helper.RecipientObjectGuid.ToString());
			}
			if (this.helper.CallerName != null)
			{
				headerStream.WriteLine("CallerNAme : " + this.helper.CallerName);
			}
			if (!string.IsNullOrEmpty(this.helper.CallerIdDisplayName))
			{
				headerStream.WriteLine("CallerIdDisplayName : " + this.helper.CallerIdDisplayName);
			}
			if (this.CallerAddress != null)
			{
				headerStream.WriteLine("CallerAddress : " + this.CallerAddress);
			}
			if (this.helper.CultureInfo != null)
			{
				headerStream.WriteLine("CultureInfo : " + this.helper.CultureInfo);
			}
			if (this.helper.CallId != null)
			{
				headerStream.WriteLine("CallId : " + this.helper.CallId);
			}
			IUMResolveCaller iumresolveCaller = this as IUMResolveCaller;
			if (iumresolveCaller != null && iumresolveCaller.ContactInfo != null)
			{
				headerStream.WriteLine("ContactInfo : " + CommonUtil.Base64Serialize(iumresolveCaller.ContactInfo));
			}
			headerStream.WriteLine("TenantGuid : " + this.helper.TenantGuid.ToString());
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x0005A614 File Offset: 0x00058814
		protected UMRecipient CreateRecipientFromObjectGuid(Guid objectGuid, Guid tenantGuid)
		{
			ADRecipient adrecipient = this.CreateADRecipientFromObjectGuid(objectGuid, tenantGuid);
			return UMRecipient.Factory.FromADRecipient<UMRecipient>(adrecipient);
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x0005A630 File Offset: 0x00058830
		protected ADRecipient CreateADRecipientFromObjectGuid(Guid objectGuid, Guid tenantGuid)
		{
			if (objectGuid == Guid.Empty)
			{
				throw new HeaderFileArgumentInvalidException("ObjectGuid is empty");
			}
			IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromTenantGuid(tenantGuid);
			ADRecipient adrecipient = iadrecipientLookup.LookupByObjectId(new ADObjectId(objectGuid));
			if (adrecipient == null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "Could not find recipient {0}", new object[]
				{
					objectGuid.ToString()
				});
				throw new InvalidObjectGuidException(objectGuid.ToString());
			}
			return adrecipient;
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x0005A6B0 File Offset: 0x000588B0
		protected UMDialPlan InitializeCallerIdAndTryGetDialPlan(UMRecipient recipient)
		{
			UMDialPlan umdialPlan = null;
			if (this.CallerId.UriType == UMUriType.E164 && recipient.ADRecipient.UMRecipientDialPlanId != null)
			{
				IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromADRecipient(recipient.ADRecipient);
				umdialPlan = iadsystemConfigurationLookup.GetDialPlanFromId(recipient.ADRecipient.UMRecipientDialPlanId);
				if (umdialPlan != null && umdialPlan.CountryOrRegionCode != null)
				{
					this.helper.CallerId = this.helper.CallerId.Clone(umdialPlan);
				}
			}
			return umdialPlan;
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x0005A720 File Offset: 0x00058920
		protected string GetMailboxServerIdHelper()
		{
			IUMCAMessage iumcamessage = this as IUMCAMessage;
			if (iumcamessage != null)
			{
				UMMailboxRecipient ummailboxRecipient = iumcamessage.CAMessageRecipient as UMMailboxRecipient;
				if (ummailboxRecipient != null)
				{
					return ummailboxRecipient.ADUser.ServerLegacyDN;
				}
			}
			return "af360a7e-e6d4-494a-ac69-6ae14896d16b";
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x0005A758 File Offset: 0x00058958
		protected string GetRecipientIdHelper()
		{
			IUMCAMessage iumcamessage = this as IUMCAMessage;
			if (iumcamessage != null)
			{
				UMMailboxRecipient ummailboxRecipient = iumcamessage.CAMessageRecipient as UMMailboxRecipient;
				if (ummailboxRecipient != null)
				{
					return ummailboxRecipient.ADUser.DistinguishedName;
				}
			}
			return "455e5330-ce1f-48d1-b6b1-2e318d2ff2c4";
		}

		// Token: 0x04000CB7 RID: 3255
		private MessageItem messageToSubmit;

		// Token: 0x04000CB8 RID: 3256
		private SubmissionHelper helper;

		// Token: 0x04000CB9 RID: 3257
		private string messageType;

		// Token: 0x04000CBA RID: 3258
		private CultureInfo cultureInfo;

		// Token: 0x04000CBB RID: 3259
		private string headerFileName;

		// Token: 0x04000CBC RID: 3260
		private int processedCount;

		// Token: 0x04000CBD RID: 3261
		private string messageID;

		// Token: 0x04000CBE RID: 3262
		private ExDateTime sentTime;
	}
}
