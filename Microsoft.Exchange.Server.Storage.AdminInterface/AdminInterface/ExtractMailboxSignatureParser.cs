using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.MailboxSignature;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.AdminInterface;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PropertyBlob;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.AdminInterface
{
	// Token: 0x0200006A RID: 106
	internal class ExtractMailboxSignatureParser : MailboxSignatureParserBase
	{
		// Token: 0x06000205 RID: 517 RVA: 0x0001007B File Offset: 0x0000E27B
		private ExtractMailboxSignatureParser()
		{
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00010083 File Offset: 0x0000E283
		internal ExtractMailboxSignatureParser(Guid databaseGuid, Guid mailboxGuid)
		{
			this.databaseGuid = databaseGuid;
			this.mailboxGuid = mailboxGuid;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00010099 File Offset: 0x0000E299
		public Guid MailboxInstanceGuid
		{
			get
			{
				return this.mailboxInstanceGuid;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000208 RID: 520 RVA: 0x000100A1 File Offset: 0x0000E2A1
		public Guid FolderGuid
		{
			get
			{
				return this.folderGuid;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000209 RID: 521 RVA: 0x000100A9 File Offset: 0x0000E2A9
		public ExchangeId[] FolderIdList
		{
			get
			{
				return this.fidList;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600020A RID: 522 RVA: 0x000100B1 File Offset: 0x0000E2B1
		public Guid MappingSignatureGuid
		{
			get
			{
				return this.mappingSignatureGuid;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600020B RID: 523 RVA: 0x000100B9 File Offset: 0x0000E2B9
		public Guid LocalIdGuid
		{
			get
			{
				return this.localIdGuid;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600020C RID: 524 RVA: 0x000100C1 File Offset: 0x0000E2C1
		public ulong NextIdCounter
		{
			get
			{
				return this.nextIdCounter;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600020D RID: 525 RVA: 0x000100C9 File Offset: 0x0000E2C9
		public uint ReservedIdCounterRange
		{
			get
			{
				return this.reservedIdCounterRange;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600020E RID: 526 RVA: 0x000100D1 File Offset: 0x0000E2D1
		public ulong NextCnCounter
		{
			get
			{
				return this.nextCnCounter;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600020F RID: 527 RVA: 0x000100D9 File Offset: 0x0000E2D9
		public uint ReservedCnCounterRange
		{
			get
			{
				return this.reservedCnCounterRange;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000210 RID: 528 RVA: 0x000100E1 File Offset: 0x0000E2E1
		public TenantHint TenantHint
		{
			get
			{
				return this.tenantHint;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000211 RID: 529 RVA: 0x000100E9 File Offset: 0x0000E2E9
		public PropertyBlob.BlobReader MailboxShapePropertyBlobReader
		{
			get
			{
				return this.mailboxShapePropertyBlobReader;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000212 RID: 530 RVA: 0x000100F1 File Offset: 0x0000E2F1
		public Dictionary<ushort, StoreNamedPropInfo> NumberToNameMap
		{
			get
			{
				return this.numberToNameMap;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000213 RID: 531 RVA: 0x000100F9 File Offset: 0x0000E2F9
		public Dictionary<ushort, Guid> ReplidGuidMap
		{
			get
			{
				return this.replidGuidMap;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00010101 File Offset: 0x0000E301
		public Mailbox.MailboxTypeVersion MailboxTypeVersion
		{
			get
			{
				return this.mailboxTypeVersion;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00010109 File Offset: 0x0000E309
		public PartitionInformation PartitionInformation
		{
			get
			{
				return this.partitionInformation;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00010111 File Offset: 0x0000E311
		public PropertyBlob.BlobReader UserInformationPropertyBlobReader
		{
			get
			{
				return this.userInformationPropertyBlobReader;
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0001011C File Offset: 0x0000E31C
		public override bool ParseMailboxBasicInformation(MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, byte[] buffer, ref int offset)
		{
			if (ExTraceGlobals.MailboxSignatureTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.MailboxSignatureTracer.TraceDebug<Guid, Guid>(52808L, "Database {0} : Mailbox {1} : Parse mailbox basic information section.", this.databaseGuid, this.mailboxGuid);
			}
			bool result;
			if (mailboxSignatureSectionMetadata.Version != 1)
			{
				result = false;
				MailboxSignatureParserBase.Skip(mailboxSignatureSectionMetadata, buffer, ref offset);
			}
			else
			{
				MailboxBasicInformation.ParseMailboxBasicInformation(buffer, ref offset, this.databaseGuid, this.mailboxGuid, out this.mailboxInstanceGuid, out this.folderGuid, out this.fidList);
				result = true;
			}
			return result;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00010198 File Offset: 0x0000E398
		public override bool ParseMailboxMappingMetadata(MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, byte[] buffer, ref int offset)
		{
			if (ExTraceGlobals.MailboxSignatureTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.MailboxSignatureTracer.TraceDebug<Guid, Guid>(46664L, "Database {0} : Mailbox {1} : Parse mailbox mapping metadata section.", this.databaseGuid, this.mailboxGuid);
			}
			bool result;
			if (mailboxSignatureSectionMetadata.Version != 1)
			{
				result = false;
				MailboxSignatureParserBase.Skip(mailboxSignatureSectionMetadata, buffer, ref offset);
			}
			else
			{
				MailboxMappingMetadataSerializedValue.Parse(buffer, ref offset, this.databaseGuid, this.mailboxGuid, out this.mappingSignatureGuid, out this.localIdGuid, out this.nextIdCounter, out this.reservedIdCounterRange, out this.nextCnCounter, out this.reservedCnCounterRange);
				result = true;
			}
			return result;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00010224 File Offset: 0x0000E424
		public override bool ParseMailboxNamedPropertyMapping(MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, byte[] buffer, ref int offset)
		{
			if (ExTraceGlobals.MailboxSignatureTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.MailboxSignatureTracer.TraceDebug<Guid, Guid>(63048L, "Database {0} : Mailbox {1} : Parse named property mapping section.", this.databaseGuid, this.mailboxGuid);
			}
			bool result;
			if (mailboxSignatureSectionMetadata.Version != 1)
			{
				result = false;
				MailboxSignatureParserBase.Skip(mailboxSignatureSectionMetadata, buffer, ref offset);
			}
			else
			{
				StoreSerializedValue.ParseNamedPropertyMap(buffer, ref offset, mailboxSignatureSectionMetadata.ElementsNumber, this.databaseGuid, this.mailboxGuid, out this.numberToNameMap);
				result = true;
			}
			return result;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00010298 File Offset: 0x0000E498
		public override bool ParseMailboxReplidGuidMapping(MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, byte[] buffer, ref int offset)
		{
			if (ExTraceGlobals.MailboxSignatureTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.MailboxSignatureTracer.TraceDebug<Guid, Guid>(38472L, "Database {0} : Mailbox {1} : Parse ReplId/GUID mapping section.", this.databaseGuid, this.mailboxGuid);
			}
			bool result;
			if (mailboxSignatureSectionMetadata.Version != 1)
			{
				result = false;
				MailboxSignatureParserBase.Skip(mailboxSignatureSectionMetadata, buffer, ref offset);
			}
			else
			{
				StoreSerializedValue.ParseReplidGuidMap(buffer, ref offset, mailboxSignatureSectionMetadata.ElementsNumber, this.databaseGuid, this.mailboxGuid, out this.replidGuidMap);
				result = true;
			}
			return result;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0001030C File Offset: 0x0000E50C
		public override bool ParseMailboxTenantHint(MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, byte[] buffer, ref int offset)
		{
			if (ExTraceGlobals.MailboxSignatureTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.MailboxSignatureTracer.TraceDebug<Guid, Guid>(62696L, "Database {0} : Mailbox {1} : Parse tenant hint section.", this.databaseGuid, this.mailboxGuid);
			}
			bool result;
			if (mailboxSignatureSectionMetadata.Version != 1)
			{
				result = false;
				MailboxSignatureParserBase.Skip(mailboxSignatureSectionMetadata, buffer, ref offset);
			}
			else
			{
				byte[] tenantHintBlob = TenantHintHelper.ParseTenantHint(mailboxSignatureSectionMetadata, buffer, ref offset);
				this.tenantHint = new TenantHint(tenantHintBlob);
				result = true;
			}
			return result;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00010378 File Offset: 0x0000E578
		public override bool ParseMailboxShape(MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, byte[] buffer, ref int offset)
		{
			if (ExTraceGlobals.MailboxSignatureTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.MailboxSignatureTracer.TraceDebug<Guid, Guid>(46684L, "Database {0} : Mailbox {1} : Parse mailbox-shape section.", this.databaseGuid, this.mailboxGuid);
			}
			bool result;
			if (mailboxSignatureSectionMetadata.Version != 1)
			{
				result = false;
				MailboxSignatureParserBase.Skip(mailboxSignatureSectionMetadata, buffer, ref offset);
			}
			else
			{
				if (mailboxSignatureSectionMetadata.Length > 0)
				{
					this.mailboxShapePropertyBlobReader = new PropertyBlob.BlobReader(buffer, offset);
					offset += mailboxSignatureSectionMetadata.Length;
				}
				else
				{
					this.mailboxShapePropertyBlobReader = new PropertyBlob.BlobReader(null, 0);
				}
				result = true;
			}
			return result;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00010400 File Offset: 0x0000E600
		public override bool ParseMailboxTypeVersion(MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, byte[] buffer, ref int offset)
		{
			if (ExTraceGlobals.MailboxSignatureTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.MailboxSignatureTracer.TraceDebug<Guid, Guid>(45916L, "Database {0} : Mailbox {1} : Parse mailbox type version section.", this.databaseGuid, this.mailboxGuid);
			}
			bool result;
			if (mailboxSignatureSectionMetadata.Version != 1)
			{
				result = false;
				MailboxSignatureParserBase.Skip(mailboxSignatureSectionMetadata, buffer, ref offset);
			}
			else
			{
				this.mailboxTypeVersion = MailboxTypeVersionHelper.Parse(mailboxSignatureSectionMetadata, buffer, ref offset);
				result = true;
			}
			return result;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00010464 File Offset: 0x0000E664
		public override bool ParsePartitionInformation(MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, byte[] buffer, ref int offset)
		{
			if (ExTraceGlobals.MailboxSignatureTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.MailboxSignatureTracer.TraceDebug<Guid, Guid>(0L, "Database {0} : Mailbox {1} : Parse mailbox partition information section.", this.databaseGuid, this.mailboxGuid);
			}
			bool result;
			if (mailboxSignatureSectionMetadata.Version != 1)
			{
				result = false;
				MailboxSignatureParserBase.Skip(mailboxSignatureSectionMetadata, buffer, ref offset);
			}
			else
			{
				this.partitionInformation = PartitionInformation.Parse(mailboxSignatureSectionMetadata, buffer, ref offset);
				result = true;
			}
			return result;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000104C4 File Offset: 0x0000E6C4
		public override bool ParseUserInformation(MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, byte[] buffer, ref int offset)
		{
			if (ExTraceGlobals.MailboxSignatureTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.MailboxSignatureTracer.TraceDebug<Guid, Guid>(0L, "Database {0} : Mailbox {1} : Parse user information section.", this.databaseGuid, this.mailboxGuid);
			}
			bool result;
			if (mailboxSignatureSectionMetadata.Version != 1)
			{
				result = false;
				MailboxSignatureParserBase.Skip(mailboxSignatureSectionMetadata, buffer, ref offset);
			}
			else
			{
				if (mailboxSignatureSectionMetadata.Length > 0)
				{
					this.userInformationPropertyBlobReader = new PropertyBlob.BlobReader(buffer, offset);
					offset += mailboxSignatureSectionMetadata.Length;
				}
				else
				{
					this.userInformationPropertyBlobReader = new PropertyBlob.BlobReader(null, 0);
				}
				result = true;
			}
			return result;
		}

		// Token: 0x04000215 RID: 533
		private readonly Guid databaseGuid;

		// Token: 0x04000216 RID: 534
		private readonly Guid mailboxGuid;

		// Token: 0x04000217 RID: 535
		private Guid mailboxInstanceGuid;

		// Token: 0x04000218 RID: 536
		private Guid folderGuid;

		// Token: 0x04000219 RID: 537
		private ExchangeId[] fidList;

		// Token: 0x0400021A RID: 538
		private Guid mappingSignatureGuid;

		// Token: 0x0400021B RID: 539
		private Guid localIdGuid;

		// Token: 0x0400021C RID: 540
		private ulong nextIdCounter;

		// Token: 0x0400021D RID: 541
		private uint reservedIdCounterRange;

		// Token: 0x0400021E RID: 542
		private ulong nextCnCounter;

		// Token: 0x0400021F RID: 543
		private uint reservedCnCounterRange;

		// Token: 0x04000220 RID: 544
		private TenantHint tenantHint;

		// Token: 0x04000221 RID: 545
		private PropertyBlob.BlobReader mailboxShapePropertyBlobReader;

		// Token: 0x04000222 RID: 546
		private Dictionary<ushort, StoreNamedPropInfo> numberToNameMap;

		// Token: 0x04000223 RID: 547
		private Dictionary<ushort, Guid> replidGuidMap;

		// Token: 0x04000224 RID: 548
		private Mailbox.MailboxTypeVersion mailboxTypeVersion;

		// Token: 0x04000225 RID: 549
		private PartitionInformation partitionInformation;

		// Token: 0x04000226 RID: 550
		private PropertyBlob.BlobReader userInformationPropertyBlobReader;
	}
}
