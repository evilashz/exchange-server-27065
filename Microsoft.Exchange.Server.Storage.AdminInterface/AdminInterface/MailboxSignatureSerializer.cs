using System;
using Microsoft.Exchange.Data.MailboxSignature;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.AdminInterface;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.PropertyBlob;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.AdminInterface
{
	// Token: 0x0200006B RID: 107
	internal class MailboxSignatureSerializer : IMailboxSignatureSectionCreator
	{
		// Token: 0x06000220 RID: 544 RVA: 0x00010546 File Offset: 0x0000E746
		public MailboxSignatureSerializer(Context context, Mailbox mailbox, MailboxSignatureSectionType sectionsToCreate)
		{
			this.mailbox = mailbox;
			this.context = context;
			if ((short)(sectionsToCreate & MailboxSignatureSectionType.MappingMetadata) != 0)
			{
				this.mailbox.GetReservedCounterRangesForDestinationMailbox(context, out this.nextIdCounter, out this.reservedIdCounterRange, out this.nextCnCounter, out this.reservedCnCounterRange);
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00010588 File Offset: 0x0000E788
		public bool Create(MailboxSignatureSectionType sectionType, out MailboxSignatureSectionMetadata sectionMetadata, out byte[] sectionData)
		{
			sectionMetadata = null;
			sectionData = null;
			if (sectionType <= MailboxSignatureSectionType.TenantHint)
			{
				switch (sectionType)
				{
				case MailboxSignatureSectionType.BasicInformation:
					return this.SerializeMailboxBasicInformation(out sectionMetadata, out sectionData);
				case MailboxSignatureSectionType.MappingMetadata:
					return this.SerializeMailboxMappingMetadata(out sectionMetadata, out sectionData);
				case MailboxSignatureSectionType.BasicInformation | MailboxSignatureSectionType.MappingMetadata:
					break;
				case MailboxSignatureSectionType.NamedPropertyMapping:
					return this.SerializeNamedPropertyMap(out sectionMetadata, out sectionData);
				default:
					if (sectionType == MailboxSignatureSectionType.ReplidGuidMapping)
					{
						return this.SerializeReplIdGuidMap(out sectionMetadata, out sectionData);
					}
					if (sectionType == MailboxSignatureSectionType.TenantHint)
					{
						return this.SerializeTenantHint(out sectionMetadata, out sectionData);
					}
					break;
				}
			}
			else if (sectionType <= MailboxSignatureSectionType.MailboxTypeVersion)
			{
				if (sectionType == MailboxSignatureSectionType.MailboxShape)
				{
					return this.SerializeMailboxShape(out sectionMetadata, out sectionData);
				}
				if (sectionType == MailboxSignatureSectionType.MailboxTypeVersion)
				{
					return this.SerializeMailboxTypeVersion(out sectionMetadata, out sectionData);
				}
			}
			else
			{
				if (sectionType == MailboxSignatureSectionType.PartitionInformation)
				{
					return this.SerializePartitionInformation(out sectionMetadata, out sectionData);
				}
				if (sectionType == MailboxSignatureSectionType.UserInformation)
				{
					return this.SerializeUserInformation(out sectionMetadata, out sectionData);
				}
			}
			return false;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0001063C File Offset: 0x0000E83C
		private static bool SerializeMailboxSignatureSection(MailboxSignatureSectionType mailboxSignatureSectionType, short requiredMailboxSignatureSectionMetadataVersion, MailboxSignatureSerializer.SectionSerializer sectionSerializer, out MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, out byte[] sectionData)
		{
			int elementsNumber;
			int num = sectionSerializer(null, 0, out elementsNumber);
			if (num == 0)
			{
				mailboxSignatureSectionMetadata = null;
				sectionData = null;
				return false;
			}
			mailboxSignatureSectionMetadata = new MailboxSignatureSectionMetadata(mailboxSignatureSectionType, requiredMailboxSignatureSectionMetadataVersion, elementsNumber, num);
			sectionData = new byte[num];
			sectionSerializer(sectionData, 0, out elementsNumber);
			return true;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00010684 File Offset: 0x0000E884
		internal bool SerializeMailboxBasicInformation(out MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, out byte[] sectionData)
		{
			if (ExTraceGlobals.MailboxSignatureTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.MailboxSignatureTracer.TraceDebug<string, int>(34888L, "Database {0} : Mailbox {1} : Serialize mailbox basic information.", this.mailbox.Database.MdbName, this.mailbox.MailboxNumber);
			}
			return MailboxSignatureSerializer.SerializeMailboxSignatureSection(MailboxSignatureSectionType.BasicInformation, 1, new MailboxSignatureSerializer.SectionSerializer(this.SerializeMailboxBasicInformationDelegate), out mailboxSignatureSectionMetadata, out sectionData);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000106E4 File Offset: 0x0000E8E4
		internal bool SerializeMailboxMappingMetadata(out MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, out byte[] sectionData)
		{
			if (ExTraceGlobals.MailboxSignatureTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.MailboxSignatureTracer.TraceDebug<string, int>(45128L, "Database {0} : Mailbox {1} : Serialize mailbox mapping metadata.", this.mailbox.Database.MdbName, this.mailbox.MailboxNumber);
			}
			return MailboxSignatureSerializer.SerializeMailboxSignatureSection(MailboxSignatureSectionType.MappingMetadata, 1, new MailboxSignatureSerializer.SectionSerializer(this.SerializeMailboxMappingMetadataDelegate), out mailboxSignatureSectionMetadata, out sectionData);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00010744 File Offset: 0x0000E944
		internal bool SerializeNamedPropertyMap(out MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, out byte[] sectionData)
		{
			if (ExTraceGlobals.MailboxSignatureTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.MailboxSignatureTracer.TraceDebug<string, int>(61512L, "Database {0} : Mailbox {1} : Serialize named property mapping.", this.mailbox.Database.MdbName, this.mailbox.MailboxNumber);
			}
			return MailboxSignatureSerializer.SerializeMailboxSignatureSection(MailboxSignatureSectionType.NamedPropertyMapping, 1, new MailboxSignatureSerializer.SectionSerializer(this.SerializeNamedPropertyMappingDelegate), out mailboxSignatureSectionMetadata, out sectionData);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000107A4 File Offset: 0x0000E9A4
		internal bool SerializeReplIdGuidMap(out MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, out byte[] sectionData)
		{
			if (ExTraceGlobals.MailboxSignatureTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.MailboxSignatureTracer.TraceDebug<string, int>(36936L, "Database {0} : Mailbox {1} : Serialize ReplId/GUID mapping.", this.mailbox.Database.MdbName, this.mailbox.MailboxNumber);
			}
			return MailboxSignatureSerializer.SerializeMailboxSignatureSection(MailboxSignatureSectionType.ReplidGuidMapping, 1, new MailboxSignatureSerializer.SectionSerializer(this.SerializeReplidGuidMappingDelegate), out mailboxSignatureSectionMetadata, out sectionData);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00010804 File Offset: 0x0000EA04
		internal bool SerializeTenantHint(out MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, out byte[] sectionData)
		{
			if (ExTraceGlobals.MailboxSignatureTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.MailboxSignatureTracer.TraceDebug<string, int>(52456L, "Database {0} : Mailbox {1} : Serialize tenant hint.", this.mailbox.Database.MdbName, this.mailbox.MailboxNumber);
			}
			return MailboxSignatureSerializer.SerializeMailboxSignatureSection(MailboxSignatureSectionType.TenantHint, 1, new MailboxSignatureSerializer.SectionSerializer(this.SerializeTenantHintDelegate), out mailboxSignatureSectionMetadata, out sectionData);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00010864 File Offset: 0x0000EA64
		internal bool SerializeMailboxShape(out MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, out byte[] sectionData)
		{
			if (ExTraceGlobals.MailboxSignatureTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.MailboxSignatureTracer.TraceDebug<string, int>(63068L, "Database {0} : Mailbox {1} : Serialize mailbox-shape.", this.mailbox.Database.MdbName, this.mailbox.MailboxNumber);
			}
			return MailboxSignatureSerializer.SerializeMailboxSignatureSection(MailboxSignatureSectionType.MailboxShape, 1, new MailboxSignatureSerializer.SectionSerializer(this.SerializeMailboxShapeDelegate), out mailboxSignatureSectionMetadata, out sectionData);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x000108C4 File Offset: 0x0000EAC4
		internal bool SerializeMailboxTypeVersion(out MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, out byte[] sectionData)
		{
			if (ExTraceGlobals.MailboxSignatureTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.MailboxSignatureTracer.TraceDebug<string, int>(54108L, "Database {0} : Mailbox {1} : Serialize mailbox type version.", this.mailbox.Database.MdbName, this.mailbox.MailboxNumber);
			}
			return MailboxSignatureSerializer.SerializeMailboxSignatureSection(MailboxSignatureSectionType.MailboxTypeVersion, 1, new MailboxSignatureSerializer.SectionSerializer(this.SerializeMailboxTypeVersionDelegate), out mailboxSignatureSectionMetadata, out sectionData);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00010924 File Offset: 0x0000EB24
		internal bool SerializePartitionInformation(out MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, out byte[] sectionData)
		{
			if (ExTraceGlobals.MailboxSignatureTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.MailboxSignatureTracer.TraceDebug<string, int>(0L, "Database {0} : Mailbox {1} : Serialize partition information.", this.mailbox.Database.MdbName, this.mailbox.MailboxNumber);
			}
			return MailboxSignatureSerializer.SerializeMailboxSignatureSection(MailboxSignatureSectionType.PartitionInformation, 1, new MailboxSignatureSerializer.SectionSerializer(this.SerializePartitionInformationDelegate), out mailboxSignatureSectionMetadata, out sectionData);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00010984 File Offset: 0x0000EB84
		internal bool SerializeUserInformation(out MailboxSignatureSectionMetadata mailboxSignatureSectionMetadata, out byte[] sectionData)
		{
			if (ExTraceGlobals.MailboxSignatureTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.MailboxSignatureTracer.TraceDebug<string, int>(0L, "Database {0} : Mailbox {1} : Serialize user information.", this.mailbox.Database.MdbName, this.mailbox.MailboxNumber);
			}
			return MailboxSignatureSerializer.SerializeMailboxSignatureSection(MailboxSignatureSectionType.UserInformation, 1, new MailboxSignatureSerializer.SectionSerializer(this.SerializeUserInformationDelegate), out mailboxSignatureSectionMetadata, out sectionData);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000109E3 File Offset: 0x0000EBE3
		private int SerializeMailboxBasicInformationDelegate(byte[] buffer, int offset, out int elementsCount)
		{
			elementsCount = 1;
			return MailboxBasicInformation.SerializeMailboxBasicInformation(this.mailbox, buffer, offset);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000109F5 File Offset: 0x0000EBF5
		private int SerializeMailboxMappingMetadataDelegate(byte[] buffer, int offset, out int elementsCount)
		{
			elementsCount = 1;
			return MailboxMappingMetadataSerializedValue.Serialize(this.mailbox, this.nextIdCounter, this.reservedIdCounterRange, this.nextCnCounter, this.reservedCnCounterRange, buffer, offset);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00010A1F File Offset: 0x0000EC1F
		private int SerializeNamedPropertyMappingDelegate(byte[] buffer, int offset, out int elementsCount)
		{
			elementsCount = this.mailbox.NamedPropertyMap.GetNamedPropertyCount(this.context);
			return StoreSerializedValue.SerializeNamedPropertyMap(this.context, this.mailbox, buffer, offset);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00010A4C File Offset: 0x0000EC4C
		private int SerializeReplidGuidMappingDelegate(byte[] buffer, int offset, out int elementsCount)
		{
			elementsCount = this.mailbox.ReplidGuidMap.GetReplidCount(this.context);
			return StoreSerializedValue.SerializeReplidGuidMap(this.mailbox, buffer, offset);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00010A74 File Offset: 0x0000EC74
		private int SerializeTenantHintDelegate(byte[] buffer, int offset, out int elementsCount)
		{
			elementsCount = 1;
			return TenantHintHelper.SerializeTenantHint(this.mailbox.SharedState.TenantHint.TenantHintBlob, buffer, offset);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00010AA3 File Offset: 0x0000ECA3
		private int SerializeMailboxShapeDelegate(byte[] buffer, int offset, out int elementsCount)
		{
			elementsCount = 1;
			return this.mailbox.SerializeMailboxShape(buffer, offset);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00010AB5 File Offset: 0x0000ECB5
		private int SerializeMailboxTypeVersionDelegate(byte[] buffer, int offset, out int elementsCount)
		{
			elementsCount = 1;
			return MailboxTypeVersionHelper.Serialize(this.context, this.mailbox, buffer, offset);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00010ACD File Offset: 0x0000ECCD
		private int SerializePartitionInformationDelegate(byte[] buffer, int offset, out int elementsCount)
		{
			elementsCount = 1;
			return new PartitionInformation(this.mailbox.SharedState.UnifiedState.UnifiedMailboxGuid, PartitionInformation.ControlFlags.None).Serialize(buffer, offset);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00010B08 File Offset: 0x0000ED08
		private int SerializeUserInformationDelegate(byte[] buffer, int offset, out int elementsCount)
		{
			elementsCount = 1;
			Properties allProperties = UserInformation.GetAllProperties(this.mailbox.CurrentOperationContext, this.mailbox.MailboxGuid);
			if (allProperties.Count == 0)
			{
				return 0;
			}
			byte[] array = PropertyBlob.BuildBlob((from p in allProperties
			select p.Tag).ToArray<StorePropTag>(), (from p in allProperties
			select p.Value).ToArray<object>());
			if (buffer != null)
			{
				Buffer.BlockCopy(array, 0, buffer, offset, array.Length);
			}
			return array.Length;
		}

		// Token: 0x04000227 RID: 551
		private uint reservedIdCounterRange;

		// Token: 0x04000228 RID: 552
		private uint reservedCnCounterRange;

		// Token: 0x04000229 RID: 553
		private Mailbox mailbox;

		// Token: 0x0400022A RID: 554
		private Context context;

		// Token: 0x0400022B RID: 555
		private ulong nextIdCounter;

		// Token: 0x0400022C RID: 556
		private ulong nextCnCounter;

		// Token: 0x0200006C RID: 108
		// (Invoke) Token: 0x06000238 RID: 568
		private delegate int SectionSerializer(byte[] buffer, int offset, out int elementsCount);
	}
}
