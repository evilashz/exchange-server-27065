using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Mime.Internal;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.Transport.Storage;
using Microsoft.Exchange.Transport.Storage.Messaging;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020000FD RID: 253
	internal class MimeCache : IDataWithinRowComponent, IDataObjectComponent
	{
		// Token: 0x06000A17 RID: 2583 RVA: 0x00024D44 File Offset: 0x00022F44
		public MimeCache(IMailItemStorage parent)
		{
			this.parent = parent;
			this.FallbackToRawOverride = false;
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x00024D7C File Offset: 0x00022F7C
		public MimeDocument MimeDocument
		{
			get
			{
				if (this.mimeDocument == null)
				{
					lock (this.parent)
					{
						if (this.mimeDocument == null)
						{
							if (this.deferredLoadAllowed)
							{
								this.DeferredLoad();
							}
							else
							{
								this.NewMimeDocument(MimeLimits.Unlimited);
							}
						}
					}
				}
				return this.mimeDocument;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x00024DF0 File Offset: 0x00022FF0
		public MimePart RootPart
		{
			get
			{
				return this.MimeDocument.RootPart;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x00024E00 File Offset: 0x00023000
		public EmailMessage Message
		{
			get
			{
				if (this.emailMessage == null)
				{
					lock (this.parent)
					{
						if (this.emailMessage == null)
						{
							if (this.MimeDocument.RootPart == null)
							{
								this.InitializeRootPart();
							}
							if (this.emailMessage == null)
							{
								if (this.IsReadOnly)
								{
									throw new InvalidOperationException("EmailMessage should be pre-created in the r/o mode");
								}
								this.emailMessage = EmailMessage.Create(this.MimeDocument);
							}
						}
					}
				}
				return this.emailMessage;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x00024E9C File Offset: 0x0002309C
		// (set) Token: 0x06000A1C RID: 2588 RVA: 0x00024F13 File Offset: 0x00023113
		public long MimeStreamSize
		{
			get
			{
				if (this.mimeDocument != null)
				{
					long num = this.parent.MimeSize;
					if (this.persistedMimeVersion != this.mimeDocument.Version)
					{
						num = this.mimeDocument.WriteTo(Stream.Null);
						if (!this.IsReadOnly)
						{
							this.parent.MimeSize = num;
						}
					}
					return num;
				}
				if (!this.saved)
				{
					return 0L;
				}
				return this.parent.MimeSize;
			}
			private set
			{
				this.ThrowIfReadOnly();
				this.parent.MimeSize = value;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x00024F27 File Offset: 0x00023127
		public bool MimeWriteStreamOpen
		{
			get
			{
				return this.mimeWriteStream != null && this.mimeWriteStream.CanWrite;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x00024F3E File Offset: 0x0002313E
		public bool IsReadOnly
		{
			get
			{
				return this.parent.IsReadOnly;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x00024F4B File Offset: 0x0002314B
		public bool IsDirty
		{
			get
			{
				return ((IDataObjectComponent)this).PendingDatabaseUpdates;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x00024F53 File Offset: 0x00023153
		// (set) Token: 0x06000A21 RID: 2593 RVA: 0x00024F5B File Offset: 0x0002315B
		public bool FallbackToRawOverride
		{
			get
			{
				return this.fallbackToRawOverride;
			}
			set
			{
				this.fallbackToRawOverride = value;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x00024F64 File Offset: 0x00023164
		// (set) Token: 0x06000A23 RID: 2595 RVA: 0x00024F6C File Offset: 0x0002316C
		public Encoding DefaultEncoding
		{
			get
			{
				return this.defaultEncoding;
			}
			set
			{
				this.defaultEncoding = value;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x00024F75 File Offset: 0x00023175
		bool IDataObjectComponent.PendingDatabaseUpdates
		{
			get
			{
				return this.mimeDocument != null && this.mimeDocument.RootPart != null && this.persistedMimeVersion != this.mimeDocument.Version;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x00024FAC File Offset: 0x000231AC
		int IDataObjectComponent.PendingDatabaseUpdateCount
		{
			get
			{
				if (!((IDataObjectComponent)this).PendingDatabaseUpdates)
				{
					return 0;
				}
				long num = this.parent.MimeSize / 102400L;
				if (num < 2147483647L)
				{
					return (int)num + 1;
				}
				return int.MaxValue;
			}
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00024FE9 File Offset: 0x000231E9
		public static void SetConfig(bool priorityHeaderPromotionEnabled)
		{
			MimeCache.priorityHeaderPromotionEnabled = priorityHeaderPromotionEnabled;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00024FF4 File Offset: 0x000231F4
		public Stream OpenMimeWriteStream(MimeLimits mimeLimits, bool expectBinaryContent)
		{
			this.ThrowIfReadOnly();
			this.audit.Drop(Breadcrumb.OpenMimeWriteStream);
			this.NewMimeDocument(mimeLimits);
			this.parent.MimeNotSequential = false;
			this.canRestore = this.saved;
			this.saved = false;
			this.mimeWriteStream = MimeInternalHelpers.GetLoadStream(this.mimeDocument, expectBinaryContent);
			return this.mimeWriteStream;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00025058 File Offset: 0x00023258
		public Stream OpenMimeReadStream(bool downConvert)
		{
			this.audit.Drop(Breadcrumb.OpenMimeReadStream);
			Stream stream2;
			if (downConvert)
			{
				Stream stream = Streams.CreateTemporaryStorageStream();
				using (EightToSevenBitConverter.OutputFilter outputFilter = new EightToSevenBitConverter.OutputFilter())
				{
					if (this.RootPart != null)
					{
						this.RootPart.WriteTo(stream, null, outputFilter);
					}
				}
				stream2 = new ReadOnlyStream(stream);
			}
			else
			{
				if (!this.IsReadOnly && this.mimeReadStream != null && this.mimeDocument != null && this.readStreamVersion != this.mimeDocument.Version)
				{
					this.mimeReadStream = null;
				}
				if (this.mimeReadStream == null)
				{
					lock (this.parent)
					{
						if (this.mimeReadStream == null)
						{
							if (this.persistedMimeVersion != -2147483648 && this.mimeDocument == null && !this.parent.MimeNotSequential)
							{
								this.mimeReadStream = this.parent.OpenMimeDBReader();
								this.readStreamVersion = this.persistedMimeVersion;
							}
							else
							{
								Stream stream3 = Streams.CreateTemporaryStorageStream();
								if (this.RootPart != null)
								{
									this.RootPart.WriteTo(stream3);
								}
								this.mimeReadStream = new ReadOnlyStream(stream3);
								this.readStreamVersion = this.MimeDocument.Version;
							}
						}
					}
				}
				stream2 = new SynchronizedStream(this.mimeReadStream);
			}
			stream2.Position = 0L;
			return stream2;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x000251D8 File Offset: 0x000233D8
		public void RestoreLastSavedMime()
		{
			this.ThrowIfReadOnly();
			this.audit.Drop(Breadcrumb.RestoreLastSavedMime);
			this.mimeWriteStream = null;
			this.CleanupEmailMessage();
			this.CleanupMimeDocument();
			this.deferredLoadAllowed = this.canRestore;
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0002520F File Offset: 0x0002340F
		void IDataObjectComponent.CloneFrom(IDataObjectComponent otherComponent)
		{
			this.deferredLoadAllowed = true;
			this.saved = true;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00025220 File Offset: 0x00023420
		void IDataObjectComponent.MinimizeMemory()
		{
			this.audit.Drop(Breadcrumb.MinimizeMemory);
			if (Monitor.TryEnter(this.parent))
			{
				try
				{
					if (!((IDataObjectComponent)this).PendingDatabaseUpdates)
					{
						this.deferredLoadAllowed = true;
						this.CleanupEmailMessage();
						this.CleanupMimeDocument();
					}
					this.CleanupReadStream();
				}
				finally
				{
					Monitor.Exit(this.parent);
				}
			}
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0002528C File Offset: 0x0002348C
		void IDataWithinRowComponent.LoadFromParentRow(DataTableCursor cursor)
		{
			this.ThrowIfReadOnly();
			this.audit.Drop(Breadcrumb.LoadFromParentRow);
			this.deferredLoadAllowed = true;
			this.saved = true;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x000252B4 File Offset: 0x000234B4
		void IDataWithinRowComponent.SaveToParentRow(DataTableCursor cursor, Func<bool> checkpointCallback)
		{
			this.audit.Drop(Breadcrumb.SaveToParentRow);
			if (!((IDataObjectComponent)this).PendingDatabaseUpdates)
			{
				return;
			}
			if (!this.IsReadOnly)
			{
				this.PromoteHeaders();
			}
			long mimeStreamSize = this.Save(cursor, checkpointCallback);
			if (!this.IsReadOnly)
			{
				this.MimeStreamSize = mimeStreamSize;
			}
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00025300 File Offset: 0x00023500
		void IDataWithinRowComponent.Cleanup()
		{
			this.audit.Drop(Breadcrumb.Cleanup);
			lock (this.parent)
			{
				this.CleanupEmailMessage();
				this.CleanupMimeDocument();
				this.CleanupReadStream();
			}
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0002535C File Offset: 0x0002355C
		public void SetMimeDocument(MimeDocument mimeDocument)
		{
			this.ThrowIfReadOnly();
			this.audit.Drop(Breadcrumb.SetMimeDocument);
			this.InternalSetMimeDocument(mimeDocument);
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0002537C File Offset: 0x0002357C
		public void PromoteHeaders()
		{
			this.ThrowIfReadOnly();
			this.audit.Drop(Breadcrumb.PromoteHeaders);
			string subject = string.Empty;
			string internetMessageId = string.Empty;
			Guid? guid = null;
			RoutingAddress empty = RoutingAddress.Empty;
			string mimeFrom = string.Empty;
			string probeName = string.Empty;
			bool persistProbeTrace = false;
			if (this.RootPart != null)
			{
				HeaderList headers = this.RootPart.Headers;
				Header header = headers.FindFirst("Subject");
				if (header != null)
				{
					try
					{
						subject = header.Value;
					}
					catch (ExchangeDataException)
					{
						byte[] headerRawValue = MimeInternalHelpers.GetHeaderRawValue(header);
						subject = Encoding.ASCII.GetString(headerRawValue);
					}
				}
				header = headers.FindFirst("Message-ID");
				if (header != null)
				{
					internetMessageId = header.Value;
				}
				guid = new Guid?(MimeCache.GetNetworkMessageId(headers));
				header = headers.FindFirst(HeaderId.Sender);
				if (header != null)
				{
					MimeRecipient mimeRecipient = header.FirstChild as MimeRecipient;
					if (mimeRecipient != null)
					{
						empty = new RoutingAddress(mimeRecipient.Email);
						if (!empty.IsValid)
						{
							empty = RoutingAddress.Empty;
						}
					}
				}
				header = headers.FindFirst(HeaderId.From);
				if (header != null)
				{
					mimeFrom = MimeCache.FromToString(header);
					if (!empty.IsValid)
					{
						MimeRecipient mimeRecipient2 = header.FirstChild as MimeRecipient;
						if (mimeRecipient2 != null)
						{
							empty = new RoutingAddress(mimeRecipient2.Email);
							if (!empty.IsValid)
							{
								empty = RoutingAddress.Empty;
							}
						}
						else
						{
							MimeGroup mimeGroup = header.FirstChild as MimeGroup;
							if (mimeGroup != null)
							{
								mimeRecipient2 = (mimeGroup.FirstChild as MimeRecipient);
								if (mimeRecipient2 != null)
								{
									empty = new RoutingAddress(mimeRecipient2.Email);
									if (!empty.IsValid)
									{
										empty = RoutingAddress.Empty;
									}
								}
							}
						}
					}
				}
				if (this.parent.IsJournalReport && string.Compare(this.parent.FromAddress, "<>", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.parent.RetryDeliveryIfRejected = true;
				}
				bool flag;
				if (this.parent.TransportPropertiesHeader.TryGetBoolValue("MustDeliver", out flag) && flag)
				{
					this.parent.RetryDeliveryIfRejected = true;
				}
				bool flag2 = false;
				if (MimeCache.priorityHeaderPromotionEnabled)
				{
					Header priorityHeader = headers.FindFirst("X-MS-Exchange-Organization-Prioritization");
					DeliveryPriority value;
					string prioritizationReason;
					flag2 = Util.DecodePriorityHeader(priorityHeader, out value, out prioritizationReason);
					if (flag2)
					{
						this.parent.PrioritizationReason = prioritizationReason;
						this.parent.Priority = new DeliveryPriority?(value);
					}
				}
				string value2;
				if (!flag2 && this.parent.TransportPropertiesHeader.TryGetStringValue("DeliveryPriority", out value2))
				{
					DeliveryPriority value3;
					if (!EnumValidator<DeliveryPriority>.TryParse(value2, EnumParseOptions.IgnoreCase, out value3))
					{
						value3 = DeliveryPriority.Normal;
					}
					this.parent.Priority = new DeliveryPriority?(value3);
				}
				ContentTransferEncoding contentTransferEncoding = ContentTransferEncoding.Unknown;
				header = headers.FindFirst(HeaderId.ContentTransferEncoding);
				if (header != null)
				{
					contentTransferEncoding = this.RootPart.ContentTransferEncoding;
				}
				if (contentTransferEncoding == ContentTransferEncoding.Binary)
				{
					this.parent.BodyType = BodyType.BinaryMIME;
				}
				else if (contentTransferEncoding == ContentTransferEncoding.EightBit)
				{
					this.parent.BodyType = BodyType.EightBitMIME;
				}
				else if (contentTransferEncoding == ContentTransferEncoding.Unknown)
				{
					this.parent.BodyType = BodyType.Default;
				}
				else
				{
					this.parent.BodyType = BodyType.SevenBit;
				}
				int scl;
				if (MimeCache.TryGetSCLValue(headers, out scl))
				{
					this.parent.Scl = scl;
				}
				header = headers.FindFirst("X-MS-Exchange-Organization-Spam-Filter-Enumerated-Risk");
				if (header != null && !string.IsNullOrEmpty(header.Value))
				{
					RiskLevel riskLevel;
					if (!EnumValidator<RiskLevel>.TryParse(header.Value, EnumParseOptions.IgnoreCase, out riskLevel))
					{
						riskLevel = RiskLevel.Normal;
					}
					this.parent.RiskLevel = riskLevel;
				}
				header = headers.FindFirst("X-MS-Exchange-ActiveMonitoringProbeName");
				if (header != null && !string.IsNullOrEmpty(header.Value))
				{
					probeName = header.Value;
				}
				header = headers.FindFirst("X-Exchange-Persist-Probe-Trace");
				if (header != null)
				{
					bool.TryParse(header.Value, out persistProbeTrace);
				}
			}
			this.parent.Subject = subject;
			this.parent.InternetMessageId = internetMessageId;
			this.parent.MimeFrom = mimeFrom;
			this.parent.MimeSenderAddress = empty.ToString();
			this.parent.ProbeName = probeName;
			this.parent.PersistProbeTrace = persistProbeTrace;
			if (guid != null)
			{
				this.parent.NetworkMessageId = guid.Value;
			}
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0002577C File Offset: 0x0002397C
		public void ResetMimeParserEohCallback()
		{
			MimeDocument mimeDocument = this.mimeDocument;
			if (mimeDocument != null)
			{
				mimeDocument.EndOfHeaders = null;
			}
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0002579C File Offset: 0x0002399C
		public void CleanupMimeDocument()
		{
			if (this.mimeDocument != null)
			{
				this.mimeDocument.Dispose();
				this.mimeDocument = null;
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x000257BE File Offset: 0x000239BE
		public void CleanupEmailMessage()
		{
			if (this.emailMessage != null)
			{
				this.emailMessage.Dispose();
				this.emailMessage = null;
			}
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x000257E0 File Offset: 0x000239E0
		private static Guid GetNetworkMessageId(HeaderList headers)
		{
			Guid guid = Guid.Empty;
			Header header = headers.FindFirst("X-MS-Exchange-Organization-Network-Message-Id");
			if (header != null && Guid.TryParse(header.Value, out guid) && guid != Guid.Empty)
			{
				return guid;
			}
			guid = CombGuidGenerator.NewGuid();
			if (header != null)
			{
				header.Value = guid.ToString();
			}
			else
			{
				header = Header.Create("X-MS-Exchange-Organization-Network-Message-Id");
				header.Value = guid.ToString();
				headers.AppendChild(header);
			}
			return guid;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00025868 File Offset: 0x00023A68
		public void SetReadOnly(bool readOnly)
		{
			this.audit.Drop(Breadcrumb.SetReadOnly);
			if (readOnly)
			{
				if (this.mimeWriteStream != null)
				{
					if (this.mimeWriteStream.CanWrite)
					{
						throw new InvalidOperationException(Strings.MimeWriteStreamOpen);
					}
					this.mimeWriteStream = null;
				}
				if (this.MimeDocument.RootPart == null)
				{
					this.InitializeRootPart();
				}
				if (this.mimeReadStream != null && this.readStreamVersion != this.mimeDocument.Version)
				{
					this.CleanupReadStream();
				}
				this.parent.RefreshMimeSize();
			}
			if (this.mimeDocument != null)
			{
				if (readOnly && this.emailMessage == null)
				{
					this.emailMessage = EmailMessage.Create(this.mimeDocument);
				}
				if (this.emailMessage != null)
				{
					this.emailMessage.SetReadOnly(readOnly);
					return;
				}
				EmailMessage.SetDocumentReadOnly(this.mimeDocument, readOnly);
			}
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0002594C File Offset: 0x00023B4C
		private static bool TryGetSCLValue(HeaderList headers, out int scl)
		{
			Header header = headers.FindFirst("X-MS-Exchange-Organization-SCL");
			if (header != null)
			{
				string value;
				try
				{
					value = header.Value;
				}
				catch (ExchangeDataException)
				{
					scl = 0;
					return false;
				}
				if (int.TryParse(value, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out scl) && scl <= 9 && scl >= -1)
				{
					return true;
				}
			}
			scl = 0;
			return false;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x000259AC File Offset: 0x00023BAC
		private static string FromToString(Header header)
		{
			AddressHeader addressHeader = header as AddressHeader;
			if (addressHeader == null)
			{
				return null;
			}
			string result;
			using (MemoryStream memoryStream = new MemoryStream(512))
			{
				foreach (AddressItem addressItem in addressHeader)
				{
					if (memoryStream.Position > 512L)
					{
						break;
					}
					if (memoryStream.Position != 0L)
					{
						memoryStream.WriteByte(44);
						memoryStream.WriteByte(32);
					}
					addressItem.WriteTo(memoryStream);
				}
				result = ByteString.BytesToString(memoryStream.GetBuffer(), 0, (int)memoryStream.Length, true);
			}
			return result;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00025A6C File Offset: 0x00023C6C
		private static void ParseHeader(MimeDocument dom, HeaderId headerId)
		{
			MimePart rootPart = dom.RootPart;
			if (rootPart == null)
			{
				return;
			}
			HeaderList headers = rootPart.Headers;
			if (headers == null)
			{
				return;
			}
			AddressHeader addressHeader = headers.FindFirst(headerId) as AddressHeader;
			if (addressHeader == null)
			{
				return;
			}
			for (MimeNode mimeNode = addressHeader.FirstChild; mimeNode != null; mimeNode = mimeNode.NextSibling)
			{
				MimeGroup mimeGroup = mimeNode as MimeGroup;
				if (mimeGroup != null)
				{
					for (MimeNode mimeNode2 = mimeGroup.FirstChild; mimeNode2 != null; mimeNode2 = mimeNode2.NextSibling)
					{
					}
				}
			}
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x00025AD8 File Offset: 0x00023CD8
		private void InternalSetMimeDocument(MimeDocument newDocument)
		{
			if (this.mimeDocument != null)
			{
				this.ThrowIfReadOnly();
			}
			if (newDocument == this.mimeDocument)
			{
				return;
			}
			this.CleanupEmailMessage();
			this.CleanupMimeDocument();
			this.CleanupReadStream();
			this.persistedMimeVersion = int.MinValue;
			MimeCache.ParseHeader(newDocument, HeaderId.Sender);
			MimeCache.ParseHeader(newDocument, HeaderId.From);
			EmailMessage emailMessage = null;
			if (this.IsReadOnly)
			{
				emailMessage = EmailMessage.Create(newDocument);
				emailMessage.SetReadOnly(true);
			}
			else
			{
				EmailMessage.SetDocumentReadOnly(newDocument, false);
			}
			this.mimeDocument = newDocument;
			this.emailMessage = emailMessage;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00025B60 File Offset: 0x00023D60
		private DecodingOptions GetMimeDecodingOptions()
		{
			if (this.DefaultEncoding != null)
			{
				return new DecodingOptions(DecodingFlags.AllEncodings | (this.fallbackToRawOverride ? DecodingFlags.FallbackToRaw : DecodingFlags.None), this.DefaultEncoding);
			}
			this.DefaultEncoding = DecodingOptions.Default.CharsetEncoding;
			DecodingOptions @default = DecodingOptions.Default;
			if (this.FallbackToRawOverride)
			{
				MimeInternalHelpers.SetDecodingOptionsDecodingFlags(ref @default, @default.DecodingFlags | DecodingFlags.FallbackToRaw);
			}
			return @default;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00025BD0 File Offset: 0x00023DD0
		private void NewMimeDocument(MimeLimits mimeLimits)
		{
			this.ThrowIfReadOnly();
			DecodingOptions mimeDecodingOptions = this.GetMimeDecodingOptions();
			MimeDocument mimeDocument = new MimeDocument(mimeDecodingOptions, mimeLimits);
			this.SetMimeDocument(mimeDocument);
			this.persistedMimeVersion = int.MinValue;
			this.MimeStreamSize = 0L;
			this.PromoteHeaders();
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00025C14 File Offset: 0x00023E14
		private long Save(DataTableCursor cursor, Func<bool> checkpointCallback)
		{
			if (!cursor.IsWithinTransaction)
			{
				throw new InvalidOperationException("No open Transaction");
			}
			Stream stream;
			CreateFixedStream createFixedStream;
			this.parent.OpenMimeDBWriter(cursor, this.saved, checkpointCallback, out stream, out createFixedStream);
			long result;
			if (!this.saved)
			{
				using (stream)
				{
					MimeCacheMap.Create(this.mimeDocument, stream, createFixedStream, new ReOpenFixedStream(DataColumn.ReopenAsLazyReader), MimeCacheMap.SmallMessageSizeThreshold, out result);
					goto IL_D5;
				}
			}
			Serialized serialized;
			using (stream)
			{
				serialized = MimeCacheMap.Update(this.mimeDocument, stream, createFixedStream, new ReOpenFixedStream(DataColumn.ReopenAsLazyReader), MimeCacheMap.SmallMessageSizeThreshold, out result);
			}
			if (serialized == Serialized.NonSequential && !this.parent.MimeNotSequential)
			{
				this.parent.MimeNotSequential = true;
			}
			else if (serialized == Serialized.Sequential && this.parent.MimeNotSequential)
			{
				this.parent.MimeNotSequential = false;
			}
			IL_D5:
			this.saved = true;
			this.persistedMimeVersion = this.mimeDocument.Version;
			return result;
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00025D30 File Offset: 0x00023F30
		private void DeferredLoad()
		{
			DecodingOptions mimeDecodingOptions = this.GetMimeDecodingOptions();
			this.InternalSetMimeDocument(this.parent.LoadMimeFromDB(mimeDecodingOptions));
			this.persistedMimeVersion = this.mimeDocument.Version;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00025D69 File Offset: 0x00023F69
		private void CleanupReadStream()
		{
			if (this.mimeReadStream != null)
			{
				this.mimeReadStream.Close();
				this.mimeReadStream = null;
				this.readStreamVersion = int.MinValue;
			}
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00025D98 File Offset: 0x00023F98
		private void InitializeRootPart()
		{
			this.MimeDocument.RootPart = new MimePart();
			this.MimeDocument.RootPart.Headers.AppendChild(new AsciiTextHeader("MIME-Version", "1.0"));
			this.MimeDocument.RootPart.Headers.AppendChild(new ContentTypeHeader("text/plain"));
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00025DFA File Offset: 0x00023FFA
		private void ThrowIfReadOnly()
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException("This MimeCache operation cannot be performed in read-only mode.");
			}
		}

		// Token: 0x0400046E RID: 1134
		private const int MimeNotPersisted = -2147483648;

		// Token: 0x0400046F RID: 1135
		private static bool priorityHeaderPromotionEnabled;

		// Token: 0x04000470 RID: 1136
		private IMailItemStorage parent;

		// Token: 0x04000471 RID: 1137
		private volatile MimeDocument mimeDocument;

		// Token: 0x04000472 RID: 1138
		private volatile EmailMessage emailMessage;

		// Token: 0x04000473 RID: 1139
		private volatile Stream mimeReadStream;

		// Token: 0x04000474 RID: 1140
		private Stream mimeWriteStream;

		// Token: 0x04000475 RID: 1141
		private int readStreamVersion = int.MinValue;

		// Token: 0x04000476 RID: 1142
		private bool deferredLoadAllowed;

		// Token: 0x04000477 RID: 1143
		private bool saved;

		// Token: 0x04000478 RID: 1144
		private bool canRestore;

		// Token: 0x04000479 RID: 1145
		private bool fallbackToRawOverride;

		// Token: 0x0400047A RID: 1146
		private int persistedMimeVersion = int.MinValue;

		// Token: 0x0400047B RID: 1147
		private Encoding defaultEncoding;

		// Token: 0x0400047C RID: 1148
		private Breadcrumbs audit = new Breadcrumbs(8);
	}
}
