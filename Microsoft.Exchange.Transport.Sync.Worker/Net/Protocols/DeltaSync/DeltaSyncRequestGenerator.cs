using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.Protocols.DeltaSync.HMFolder;
using Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail;
using Microsoft.Exchange.Net.Protocols.DeltaSync.ItemOperationsRequest;
using Microsoft.Exchange.Net.Protocols.DeltaSync.SendRequest;
using Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest;
using Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest;
using Microsoft.Exchange.Net.Protocols.DeltaSync.SyncRequest;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync
{
	// Token: 0x02000072 RID: 114
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class DeltaSyncRequestGenerator
	{
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x00017638 File Offset: 0x00015838
		private XmlSerializer SyncSerializer
		{
			get
			{
				if (this.syncSerializer == null)
				{
					this.syncSerializer = new XmlSerializer(typeof(Sync));
				}
				return this.syncSerializer;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x0001765D File Offset: 0x0001585D
		private XmlSerializer ItemOperationsSerializer
		{
			get
			{
				if (this.itemOperationsSerializer == null)
				{
					this.itemOperationsSerializer = new XmlSerializer(typeof(ItemOperations));
				}
				return this.itemOperationsSerializer;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x00017682 File Offset: 0x00015882
		private XmlSerializer SettingsSerializer
		{
			get
			{
				if (this.settingsSerializer == null)
				{
					this.settingsSerializer = new XmlSerializer(typeof(Settings));
				}
				return this.settingsSerializer;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x000176A7 File Offset: 0x000158A7
		private XmlSerializer SendSerializer
		{
			get
			{
				if (this.sendSerializer == null)
				{
					this.sendSerializer = new XmlSerializer(typeof(Send));
				}
				return this.sendSerializer;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x000176CC File Offset: 0x000158CC
		private XmlSerializer StatelessSerializer
		{
			get
			{
				if (this.statelessSerializer == null)
				{
					this.statelessSerializer = new XmlSerializer(typeof(Stateless));
				}
				return this.statelessSerializer;
			}
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x000176F4 File Offset: 0x000158F4
		internal void SetupGetChangesRequest(string folderSyncKey, string emailSyncKey, int windowSize, Stream requestStream)
		{
			Sync sync = new Sync();
			Collection collection = sync.Collections.CollectionCollection.Add();
			collection.Class = DeltaSyncCommon.FolderCollectionName;
			collection.GetChanges = DeltaSyncRequestGenerator.GetChanges;
			collection.SyncKey = (folderSyncKey ?? DeltaSyncCommon.DefaultSyncKey);
			Collection collection2 = sync.Collections.CollectionCollection.Add();
			collection2.Class = DeltaSyncCommon.EmailCollectionName;
			collection2.GetChanges = DeltaSyncRequestGenerator.GetChanges;
			collection2.SyncKey = (emailSyncKey ?? DeltaSyncCommon.DefaultSyncKey);
			collection2.WindowSize = windowSize;
			this.SyncSerializer.Serialize(requestStream, sync);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0001778C File Offset: 0x0001598C
		internal void SetupSendMessageRequest(DeltaSyncMail deltaSyncEmail, bool saveInSentItems, DeltaSyncRecipients recipients, Stream requestStream)
		{
			Send o = DeltaSyncRequestGenerator.SetupXmlSendMessageRequest(deltaSyncEmail, saveInSentItems, recipients);
			using (Stream stream = TemporaryStorage.Create())
			{
				this.SendSerializer.Serialize(stream, o);
				stream.Position = 0L;
				DeltaSyncRequestGenerator.SetupMtomRequestWithXmlPartAndMessagePart(stream, deltaSyncEmail.EmailMessage, deltaSyncEmail.MessageIncludeContentId, requestStream);
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x000177F0 File Offset: 0x000159F0
		internal void SetupApplyChangesRequest(List<DeltaSyncOperation> operations, ConflictResolution conflictResolution, string folderSyncKey, string emailSyncKey, Stream requestStream)
		{
			List<DeltaSyncMail> list = new List<DeltaSyncMail>(operations.Count / 2);
			Sync sync = new Sync();
			sync.Options.Conflict = (byte)conflictResolution;
			Collection collection = sync.Collections.CollectionCollection.Add();
			collection.Class = DeltaSyncCommon.FolderCollectionName;
			collection.SyncKey = (folderSyncKey ?? DeltaSyncCommon.DefaultSyncKey);
			Collection collection2 = sync.Collections.CollectionCollection.Add();
			collection2.Class = DeltaSyncCommon.EmailCollectionName;
			collection2.SyncKey = (emailSyncKey ?? DeltaSyncCommon.DefaultSyncKey);
			foreach (DeltaSyncOperation deltaSyncOperation in operations)
			{
				if (!(deltaSyncOperation.DeltaSyncObject is DeltaSyncFolder) && !(deltaSyncOperation.DeltaSyncObject is DeltaSyncMail))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unknown Object Type : {0}", new object[]
					{
						deltaSyncOperation.DeltaSyncObject.GetType()
					}));
				}
				switch (deltaSyncOperation.OperationType)
				{
				case DeltaSyncOperation.Type.Add:
					if (deltaSyncOperation.DeltaSyncObject is DeltaSyncFolder)
					{
						DeltaSyncFolder deltaSyncFolder = deltaSyncOperation.DeltaSyncObject as DeltaSyncFolder;
						Add add = collection.Commands.AddCollection.Add();
						add.ClientId = deltaSyncFolder.ClientId;
						add.ApplicationData.DisplayName.Value = deltaSyncFolder.DisplayName;
						add.ApplicationData.DisplayName.charset = DeltaSyncCommon.DefaultStringCharset;
						add.ApplicationData.Version2 = DeltaSyncCommon.DefaultEncodingVersion;
						add.ApplicationData.ParentId.isClientId = (deltaSyncFolder.Parent.IsClientObject ? Microsoft.Exchange.Net.Protocols.DeltaSync.HMFolder.bitType.one : Microsoft.Exchange.Net.Protocols.DeltaSync.HMFolder.bitType.zero);
						add.ApplicationData.ParentId.Value = deltaSyncFolder.Parent.Id;
					}
					else
					{
						DeltaSyncMail deltaSyncMail = deltaSyncOperation.DeltaSyncObject as DeltaSyncMail;
						DeltaSyncRequestGenerator.AddApplicationDataToEmailCollection(collection2.Commands.AddCollection, deltaSyncMail);
						list.Add(deltaSyncMail);
					}
					break;
				case DeltaSyncOperation.Type.Change:
					if (deltaSyncOperation.DeltaSyncObject is DeltaSyncFolder)
					{
						DeltaSyncFolder deltaSyncFolder = deltaSyncOperation.DeltaSyncObject as DeltaSyncFolder;
						Change change = collection.Commands.ChangeCollection.Add();
						change.ServerId = deltaSyncFolder.ServerId.ToString();
						change.ApplicationData.DisplayName.Value = deltaSyncFolder.DisplayName;
						change.ApplicationData.DisplayName.charset = DeltaSyncCommon.DefaultStringCharset;
						change.ApplicationData.Version2 = DeltaSyncCommon.DefaultEncodingVersion;
					}
					else
					{
						DeltaSyncMail deltaSyncMail = deltaSyncOperation.DeltaSyncObject as DeltaSyncMail;
						Change change2 = collection2.Commands.ChangeCollection.Add();
						change2.ServerId = deltaSyncMail.ServerId.ToString();
						change2.ApplicationData.Read = (deltaSyncMail.Read ? 1 : 0);
						if (deltaSyncMail.ReplyToOrForward != null)
						{
							change2.ApplicationData.ReplyToOrForwardState = (byte)deltaSyncMail.ReplyToOrForward.Value;
						}
					}
					break;
				case DeltaSyncOperation.Type.Delete:
				{
					Delete delete;
					if (deltaSyncOperation.DeltaSyncObject is DeltaSyncFolder)
					{
						delete = collection.Commands.DeleteCollection.Add();
					}
					else
					{
						delete = collection2.Commands.DeleteCollection.Add();
					}
					delete.ServerId = deltaSyncOperation.DeltaSyncObject.ServerId.ToString();
					break;
				}
				default:
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Change Type Not supported : {0}", new object[]
					{
						deltaSyncOperation.OperationType
					}));
				}
			}
			using (Stream stream = TemporaryStorage.Create())
			{
				this.SyncSerializer.Serialize(stream, sync);
				stream.Position = 0L;
				string value = Guid.NewGuid().ToString();
				MimeWriter mimeWriter = new MimeWriter(requestStream);
				mimeWriter.StartPart();
				mimeWriter.WriteHeader(HeaderId.MimeVersion, DeltaSyncCommon.VersionOneDotZero);
				mimeWriter.StartHeader(HeaderId.ContentType);
				mimeWriter.WriteHeaderValue(DeltaSyncCommon.MultipartRelated);
				mimeWriter.WriteParameter(DeltaSyncCommon.Boundary, Guid.NewGuid().ToString());
				mimeWriter.WriteParameter(DeltaSyncCommon.Type, DeltaSyncCommon.ApplicationXopXmlContentType);
				mimeWriter.WriteParameter(DeltaSyncCommon.Start, value);
				mimeWriter.WriteParameter(DeltaSyncCommon.StartInfo, DeltaSyncCommon.ApplicationXopXmlContentType);
				mimeWriter.StartPart();
				mimeWriter.StartHeader(HeaderId.ContentType);
				mimeWriter.WriteHeaderValue(DeltaSyncCommon.ApplicationXopXmlContentType);
				mimeWriter.WriteParameter(DeltaSyncCommon.Charset, Encoding.UTF8.WebName);
				mimeWriter.WriteParameter(DeltaSyncCommon.Type, DeltaSyncCommon.ApplicationXopXmlContentType);
				mimeWriter.WriteHeader(HeaderId.ContentTransferEncoding, DeltaSyncCommon.SevenBit);
				mimeWriter.WriteHeader(HeaderId.ContentId, value);
				mimeWriter.WriteContent(stream);
				mimeWriter.EndPart();
				foreach (DeltaSyncMail deltaSyncMail2 in list)
				{
					deltaSyncMail2.EmailMessage.Position = 0L;
					mimeWriter.StartPart();
					mimeWriter.WriteHeader(HeaderId.ContentType, DeltaSyncCommon.ApplicationRFC822);
					mimeWriter.WriteHeader(HeaderId.ContentTransferEncoding, DeltaSyncCommon.Binary);
					mimeWriter.WriteHeader(HeaderId.ContentId, deltaSyncMail2.MessageIncludeContentId);
					mimeWriter.WriteContent(deltaSyncMail2.EmailMessage);
					mimeWriter.EndPart();
				}
				mimeWriter.EndPart();
				mimeWriter.Flush();
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00017DB8 File Offset: 0x00015FB8
		internal void SetupFetchMessageRequest(Guid serverId, Stream requestStream)
		{
			ItemOperations itemOperations = new ItemOperations();
			itemOperations.Fetch = new FetchType();
			itemOperations.Fetch.Class = DeltaSyncCommon.EmailCollectionName;
			itemOperations.Fetch.ServerId = serverId.ToString();
			itemOperations.Fetch.Compression = "hm-compression";
			itemOperations.Fetch.ResponseContentType = Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail.ResponseContentTypeType.mtom;
			this.ItemOperationsSerializer.Serialize(requestStream, itemOperations);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00017E28 File Offset: 0x00016028
		internal void SetupGetSettingsRequest(Stream requestStream)
		{
			Settings settings = new Settings();
			settings.ServiceSettings = new ServiceSettingsType();
			settings.ServiceSettings.Properties = new Properties();
			settings.ServiceSettings.Properties.Get = new PropertiesGet();
			settings.AccountSettings = new AccountSettingsType();
			settings.AccountSettings.Get = new AccountSettingsTypeGet();
			settings.AccountSettings.Get.Properties = new AccountSettingsTypeGetProperties();
			this.SettingsSerializer.Serialize(requestStream, settings);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00017EA8 File Offset: 0x000160A8
		internal void SetupGetStatisticsRequest(Stream requestStream)
		{
			Stateless stateless = new Stateless();
			stateless.Collections = new StatelessCollection[]
			{
				new StatelessCollection
				{
					Class = DeltaSyncCommon.FolderCollectionName,
					Get = new StatelessCollectionGet()
				}
			};
			this.StatelessSerializer.Serialize(requestStream, stateless);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00017EF8 File Offset: 0x000160F8
		private static void SetupMtomRequestWithXmlPartAndMessagePart(Stream xmlPartSourceStream, Stream messagePartSourceStream, string messagePartContentId, Stream requestStream)
		{
			string value = Guid.NewGuid().ToString();
			MimeWriter mimeWriter = new MimeWriter(requestStream);
			mimeWriter.StartPart();
			mimeWriter.WriteHeader(HeaderId.MimeVersion, DeltaSyncCommon.VersionOneDotZero);
			mimeWriter.StartHeader(HeaderId.ContentType);
			mimeWriter.WriteHeaderValue(DeltaSyncCommon.MultipartRelated);
			mimeWriter.WriteParameter(DeltaSyncCommon.Boundary, Guid.NewGuid().ToString());
			mimeWriter.WriteParameter(DeltaSyncCommon.Type, DeltaSyncCommon.ApplicationXopXmlContentType);
			mimeWriter.WriteParameter(DeltaSyncCommon.Start, value);
			mimeWriter.WriteParameter(DeltaSyncCommon.StartInfo, DeltaSyncCommon.ApplicationXopXmlContentType);
			mimeWriter.StartPart();
			mimeWriter.StartHeader(HeaderId.ContentType);
			mimeWriter.WriteHeaderValue(DeltaSyncCommon.ApplicationXopXmlContentType);
			mimeWriter.WriteParameter(DeltaSyncCommon.Charset, Encoding.UTF8.WebName);
			mimeWriter.WriteParameter(DeltaSyncCommon.Type, DeltaSyncCommon.ApplicationXopXmlContentType);
			mimeWriter.WriteHeader(HeaderId.ContentTransferEncoding, DeltaSyncCommon.SevenBit);
			mimeWriter.WriteHeader(HeaderId.ContentId, value);
			mimeWriter.WriteContent(xmlPartSourceStream);
			mimeWriter.EndPart();
			mimeWriter.StartPart();
			mimeWriter.WriteHeader(HeaderId.ContentType, DeltaSyncCommon.ApplicationRFC822);
			mimeWriter.WriteHeader(HeaderId.ContentTransferEncoding, DeltaSyncCommon.Binary);
			mimeWriter.WriteHeader(HeaderId.ContentId, messagePartContentId);
			mimeWriter.WriteContent(messagePartSourceStream);
			mimeWriter.EndPart();
			mimeWriter.EndPart();
			mimeWriter.Flush();
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00018034 File Offset: 0x00016234
		private static void AddApplicationDataToEmailCollection(AddCollection addCollection, DeltaSyncMail deltaSyncEmail)
		{
			Add add = addCollection.Add();
			add.ClientId = deltaSyncEmail.ClientId;
			add.ApplicationData.From.Value = deltaSyncEmail.From;
			add.ApplicationData.Subject.Value = deltaSyncEmail.Subject;
			add.ApplicationData.DateReceived = deltaSyncEmail.DateReceivedUniversalTimeString;
			add.ApplicationData.HasAttachments = (deltaSyncEmail.HasAttachments ? Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail.bitType.one : Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail.bitType.zero);
			add.ApplicationData.FolderId.isClientId = (deltaSyncEmail.Parent.IsClientObject ? Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail.bitType.one : Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail.bitType.zero);
			add.ApplicationData.FolderId.Value = deltaSyncEmail.Parent.Id;
			add.ApplicationData.MessageClass = deltaSyncEmail.MessageClass;
			add.ApplicationData.Importance = (byte)deltaSyncEmail.Importance;
			add.ApplicationData.ConversationTopic.Value = deltaSyncEmail.ConversationTopic;
			add.ApplicationData.ConversationIndex.Value = deltaSyncEmail.ConversationIndex;
			add.ApplicationData.Sensitivity = (byte)deltaSyncEmail.Sensitivity;
			add.ApplicationData.Read = (deltaSyncEmail.Read ? 1 : 0);
			add.ApplicationData.Size = deltaSyncEmail.Size;
			add.ApplicationData.TrustedSource = Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail.bitType.one;
			add.ApplicationData.IsFromSomeoneAddressBook = Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail.bitType.zero;
			add.ApplicationData.IsToAllowList = Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail.bitType.zero;
			add.ApplicationData.Version = DeltaSyncCommon.DefaultEncodingVersion;
			add.ApplicationData.ReplyToOrForwardState = (byte)((deltaSyncEmail.ReplyToOrForward != null) ? deltaSyncEmail.ReplyToOrForward.Value : DeltaSyncMail.ReplyToOrForwardState.None);
			add.ApplicationData.Categories = DeltaSyncRequestGenerator.Categories;
			add.ApplicationData.ConfirmedJunk = Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail.bitType.zero;
			add.ApplicationData.Flag = DeltaSyncRequestGenerator.Flag;
			add.ApplicationData.Message.Include.href = deltaSyncEmail.MessageIncludeContentId;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00018214 File Offset: 0x00016414
		private static Send SetupXmlSendMessageRequest(DeltaSyncMail deltaSyncEmail, bool saveInSentItems, DeltaSyncRecipients recipients)
		{
			Send send = new Send();
			send.SendItem.Class = DeltaSyncCommon.EmailCollectionName;
			send.SendItem.Recipients = new Recipients();
			foreach (string obj in recipients.To)
			{
				send.SendItem.Recipients.To.Add(obj);
			}
			foreach (string obj2 in recipients.Cc)
			{
				send.SendItem.Recipients.Cc.Add(obj2);
			}
			foreach (string obj3 in recipients.Bcc)
			{
				send.SendItem.Recipients.Bcc.Add(obj3);
			}
			send.SendItem.Item.Include.href = deltaSyncEmail.MessageIncludeContentId;
			if (saveInSentItems)
			{
				send.SaveItem = DeltaSyncRequestGenerator.SaveItem;
			}
			return send;
		}

		// Token: 0x040002CA RID: 714
		private const string HMCompression = "hm-compression";

		// Token: 0x040002CB RID: 715
		private static readonly SaveItem SaveItem = new SaveItem();

		// Token: 0x040002CC RID: 716
		private static readonly GetChanges GetChanges = new GetChanges();

		// Token: 0x040002CD RID: 717
		private static readonly Categories Categories = new Categories();

		// Token: 0x040002CE RID: 718
		private static readonly Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail.Flag Flag = new Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail.Flag();

		// Token: 0x040002CF RID: 719
		private XmlSerializer syncSerializer;

		// Token: 0x040002D0 RID: 720
		private XmlSerializer itemOperationsSerializer;

		// Token: 0x040002D1 RID: 721
		private XmlSerializer settingsSerializer;

		// Token: 0x040002D2 RID: 722
		private XmlSerializer sendSerializer;

		// Token: 0x040002D3 RID: 723
		private XmlSerializer statelessSerializer;
	}
}
