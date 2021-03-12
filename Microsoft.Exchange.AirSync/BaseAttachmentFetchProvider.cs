using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Xml;
using Microsoft.Exchange.AirSync.Wbxml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200003F RID: 63
	internal abstract class BaseAttachmentFetchProvider : DisposeTrackableBase, IItemOperationsProvider, IReusable, IMultipartResponse
	{
		// Token: 0x060003AF RID: 943 RVA: 0x00015883 File Offset: 0x00013A83
		internal BaseAttachmentFetchProvider(MailboxSession session, SyncStateStorage syncStateStorage, ProtocolLogger protocolLogger, Unlimited<ByteQuantifiedSize> maxAttachmentSize, bool attachmentsEnabled)
		{
			this.Session = session;
			this.SyncStateStorage = syncStateStorage;
			this.ProtocolLogger = protocolLogger;
			this.MaxAttachmentSize = maxAttachmentSize;
			this.AttachmentsEnabled = attachmentsEnabled;
			AirSyncCounters.NumberOfMailboxAttachmentFetches.Increment();
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x000158BB File Offset: 0x00013ABB
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x000158C3 File Offset: 0x00013AC3
		public bool RightsManagementSupport { get; private set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x000158CC File Offset: 0x00013ACC
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x000158D4 File Offset: 0x00013AD4
		protected int MinRange { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x000158DD File Offset: 0x00013ADD
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x000158E5 File Offset: 0x00013AE5
		protected int MaxRange { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x000158EE File Offset: 0x00013AEE
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x000158F6 File Offset: 0x00013AF6
		protected bool MultiPartResponse { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x000158FF File Offset: 0x00013AFF
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x00015907 File Offset: 0x00013B07
		protected int PartNumber { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060003BA RID: 954 RVA: 0x00015910 File Offset: 0x00013B10
		// (set) Token: 0x060003BB RID: 955 RVA: 0x00015918 File Offset: 0x00013B18
		protected int Total { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00015921 File Offset: 0x00013B21
		// (set) Token: 0x060003BD RID: 957 RVA: 0x00015929 File Offset: 0x00013B29
		protected string FileReference { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00015932 File Offset: 0x00013B32
		// (set) Token: 0x060003BF RID: 959 RVA: 0x0001593A File Offset: 0x00013B3A
		protected string ContentType { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00015943 File Offset: 0x00013B43
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x0001594B File Offset: 0x00013B4B
		protected AirSyncStream OutStream { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00015954 File Offset: 0x00013B54
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x0001595C File Offset: 0x00013B5C
		protected bool RangeSpecified { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x00015965 File Offset: 0x00013B65
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x0001596D File Offset: 0x00013B6D
		protected MailboxSession Session { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00015976 File Offset: 0x00013B76
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x0001597E File Offset: 0x00013B7E
		protected SyncStateStorage SyncStateStorage { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00015987 File Offset: 0x00013B87
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x0001598F File Offset: 0x00013B8F
		protected ProtocolLogger ProtocolLogger { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00015998 File Offset: 0x00013B98
		// (set) Token: 0x060003CB RID: 971 RVA: 0x000159A0 File Offset: 0x00013BA0
		protected Unlimited<ByteQuantifiedSize> MaxAttachmentSize { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060003CC RID: 972 RVA: 0x000159A9 File Offset: 0x00013BA9
		// (set) Token: 0x060003CD RID: 973 RVA: 0x000159B1 File Offset: 0x00013BB1
		protected bool AttachmentsEnabled { get; set; }

		// Token: 0x060003CE RID: 974 RVA: 0x000159BA File Offset: 0x00013BBA
		public void Reset()
		{
			this.MinRange = 0;
			this.MaxRange = 0;
			this.Total = 0;
			this.FileReference = null;
			this.ContentType = null;
			this.RangeSpecified = false;
			this.RightsManagementSupport = false;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x000159ED File Offset: 0x00013BED
		public void BuildResponse(XmlNode responseNode, int partNumber)
		{
			this.PartNumber = partNumber;
			this.MultiPartResponse = true;
			this.BuildResponse(responseNode);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00015A04 File Offset: 0x00013C04
		public Stream GetResponseStream()
		{
			Stream outStream = this.OutStream;
			this.OutStream = null;
			return outStream;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00015A20 File Offset: 0x00013C20
		public void ParseRequest(XmlNode fetchNode)
		{
			foreach (object obj in fetchNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string name;
				if ((name = xmlNode.Name) != null)
				{
					if (name == "FileReference")
					{
						this.FileReference = xmlNode.InnerText;
						continue;
					}
					if (name == "Options")
					{
						this.ParseOptions(xmlNode);
						continue;
					}
					if (name == "Store")
					{
						continue;
					}
				}
				this.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "InvalidNodeInAttachmentFetch");
				throw new AirSyncPermanentException(StatusCode.Sync_ProtocolVersionMismatch, false);
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00015AD8 File Offset: 0x00013CD8
		public void Execute()
		{
			AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "Attachment Fetch command received. Processing request...");
			int count = -1;
			if (this.RangeSpecified)
			{
				count = this.MaxRange - this.MinRange + 1;
			}
			if (!this.AttachmentsEnabled)
			{
				this.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "AttachmentsNotEnabledMbxAttProvider");
				throw new AirSyncPermanentException(HttpStatusCode.Forbidden, StatusCode.AccessDenied, null, false);
			}
			this.OutStream = new AirSyncStream();
			try
			{
				this.Total = this.InternalExecute(count);
				if (this.Total == 0)
				{
					this.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "ZeroAttachmentSizeForFetch");
					throw new AirSyncPermanentException(HttpStatusCode.OK, StatusCode.Sync_NotificationGUID, null, false);
				}
			}
			catch (FormatException innerException)
			{
				this.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "BadAttachmentIdInFetch");
				throw new AirSyncPermanentException(StatusCode.Sync_TooManyFolders, innerException, false);
			}
			catch (ObjectNotFoundException innerException2)
			{
				this.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "AttachmentNotFoundInFetch");
				throw new AirSyncPermanentException(StatusCode.Sync_ClientServerConversion, innerException2, false);
			}
			catch (ArgumentOutOfRangeException innerException3)
			{
				this.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "AttachmentRangeErrorInFetch");
				throw new AirSyncPermanentException(StatusCode.Sync_ObjectNotFound, innerException3, false);
			}
			catch (IOException innerException4)
			{
				this.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "IOErrorInAttachmentFetch");
				throw new AirSyncPermanentException(StatusCode.Sync_FolderHierarchyRequired, innerException4, false);
			}
			catch (DataTooLargeException innerException5)
			{
				this.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "AttachmentTooBigInFetch");
				throw new AirSyncPermanentException(StatusCode.Sync_NotificationsNotProvisioned, innerException5, false);
			}
			this.ProtocolLogger.IncrementValue(ProtocolLoggerData.Attachments);
			this.ProtocolLogger.IncrementValueBy(ProtocolLoggerData.AttachmentBytes, this.Total);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00015C74 File Offset: 0x00013E74
		public void BuildResponse(XmlNode responseNode)
		{
			XmlNode xmlNode = responseNode.OwnerDocument.CreateElement("Fetch", "ItemOperations:");
			XmlNode xmlNode2 = responseNode.OwnerDocument.CreateElement("Status", "ItemOperations:");
			XmlNode xmlNode3 = responseNode.OwnerDocument.CreateElement("FileReference", "AirSyncBase:");
			XmlNode xmlNode4 = responseNode.OwnerDocument.CreateElement("Properties", "ItemOperations:");
			XmlNode xmlNode5 = responseNode.OwnerDocument.CreateElement("ContentType", "AirSyncBase:");
			if (this.RangeSpecified)
			{
				XmlNode xmlNode6 = responseNode.OwnerDocument.CreateElement("Range", "ItemOperations:");
				xmlNode6.InnerText = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", new object[]
				{
					this.MinRange,
					(long)this.MinRange + this.OutStream.Length - 1L
				});
				xmlNode4.AppendChild(xmlNode6);
				XmlNode xmlNode7 = responseNode.OwnerDocument.CreateElement("Total", "ItemOperations:");
				xmlNode7.InnerText = this.Total.ToString(CultureInfo.InvariantCulture);
				xmlNode4.AppendChild(xmlNode7);
			}
			xmlNode5.InnerText = this.ContentType;
			xmlNode4.AppendChild(xmlNode5);
			if (this.MultiPartResponse)
			{
				XmlNode xmlNode8 = responseNode.OwnerDocument.CreateElement("Part", "ItemOperations:");
				xmlNode8.InnerText = this.PartNumber.ToString(CultureInfo.InvariantCulture);
				xmlNode4.AppendChild(xmlNode8);
			}
			else
			{
				this.OutStream.DoBase64Conversion = true;
				AirSyncBlobXmlNode airSyncBlobXmlNode = new AirSyncBlobXmlNode(null, "Data", "ItemOperations:", responseNode.OwnerDocument);
				airSyncBlobXmlNode.Stream = this.GetResponseStream();
				airSyncBlobXmlNode.StreamDataSize = airSyncBlobXmlNode.Stream.Length;
				airSyncBlobXmlNode.OriginalNodeType = XmlNodeType.Text;
				xmlNode4.AppendChild(airSyncBlobXmlNode);
			}
			xmlNode2.InnerText = 1.ToString(CultureInfo.InvariantCulture);
			xmlNode.AppendChild(xmlNode2);
			xmlNode3.InnerText = this.FileReference;
			xmlNode.AppendChild(xmlNode3);
			xmlNode.AppendChild(xmlNode4);
			responseNode.AppendChild(xmlNode);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00015E98 File Offset: 0x00014098
		public void BuildErrorResponse(string statusCode, XmlNode responseNode, ProtocolLogger protocolLogger)
		{
			if (protocolLogger != null)
			{
				protocolLogger.IncrementValue(ProtocolLoggerData.IOFetchAttErrors);
			}
			XmlNode xmlNode = responseNode.OwnerDocument.CreateElement("Fetch", "ItemOperations:");
			XmlNode xmlNode2 = responseNode.OwnerDocument.CreateElement("Status", "ItemOperations:");
			xmlNode2.InnerText = statusCode;
			xmlNode.AppendChild(xmlNode2);
			if (!string.IsNullOrEmpty(this.FileReference))
			{
				XmlNode xmlNode3 = responseNode.OwnerDocument.CreateElement("FileReference", "AirSyncBase:");
				xmlNode3.InnerText = this.FileReference;
				xmlNode.AppendChild(xmlNode3);
			}
			responseNode.AppendChild(xmlNode);
		}

		// Token: 0x060003D5 RID: 981
		protected abstract int InternalExecute(int count);

		// Token: 0x060003D6 RID: 982 RVA: 0x00015F2A File Offset: 0x0001412A
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing && this.OutStream != null)
			{
				this.OutStream.Dispose();
				this.OutStream = null;
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00015F49 File Offset: 0x00014149
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<BaseAttachmentFetchProvider>(this);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00015F54 File Offset: 0x00014154
		private void ParseOptions(XmlNode optionsNode)
		{
			foreach (object obj in optionsNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string localName;
				if ((localName = xmlNode.LocalName) != null)
				{
					if (!(localName == "Range"))
					{
						if (localName == "RightsManagementSupport")
						{
							string innerText;
							if ((innerText = xmlNode.InnerText) != null)
							{
								if (innerText == "0")
								{
									this.RightsManagementSupport = false;
									continue;
								}
								if (innerText == "1")
								{
									this.RightsManagementSupport = true;
									continue;
								}
							}
							this.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "InvalidRightsManagementSupportInAttachmentFetch");
							throw new AirSyncPermanentException(StatusCode.Sync_ProtocolVersionMismatch, false);
						}
					}
					else
					{
						string[] array = xmlNode.InnerText.Split(new char[]
						{
							'-'
						});
						this.MinRange = int.Parse(array[0], CultureInfo.InvariantCulture);
						this.MaxRange = int.Parse(array[1], CultureInfo.InvariantCulture);
						if (this.MinRange > this.MaxRange)
						{
							this.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "MinMoreThanMaxInAttachmentFetch");
							throw new AirSyncPermanentException(StatusCode.Sync_ObjectNotFound, false);
						}
						this.RangeSpecified = true;
						continue;
					}
				}
				this.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "UnknownOptionInAttachmentFetch");
				throw new AirSyncPermanentException(StatusCode.Sync_ProtocolVersionMismatch, false);
			}
		}
	}
}
