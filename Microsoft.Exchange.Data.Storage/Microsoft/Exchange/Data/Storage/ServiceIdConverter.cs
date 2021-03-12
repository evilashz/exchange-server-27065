using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D82 RID: 3458
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ServiceIdConverter
	{
		// Token: 0x06007723 RID: 30499 RVA: 0x0020DB74 File Offset: 0x0020BD74
		public static string ConvertToConcatenatedId(IdHeaderInformation idHeaderInformation, List<AttachmentId> attachmentIds, bool attemptCompression)
		{
			int num = 2;
			if (1024 < idHeaderInformation.StoreIdBytes.Length)
			{
				throw new InvalidIdStoreObjectIdTooLongException();
			}
			num += 2 + idHeaderInformation.StoreIdBytes.Length;
			int num2 = 0;
			if (idHeaderInformation.IdStorageType == IdStorageType.MailboxItemSmtpAddressBased)
			{
				num2 = MailboxIdSerializer.EmailAddressToByteCount(idHeaderInformation.MailboxId.SmtpAddress);
				if (num2 == 0 || 512 < num2)
				{
					throw new InvalidIdMonikerTooLongException();
				}
				num += 2 + num2 + 1;
			}
			else if (idHeaderInformation.IdStorageType == IdStorageType.MailboxItemMailboxGuidBased || idHeaderInformation.IdStorageType == IdStorageType.ConversationIdMailboxGuidBased)
			{
				num2 = MailboxIdSerializer.MailboxGuidToByteCount(idHeaderInformation.MailboxId.MailboxGuid);
				if (num2 == 0 || 512 < num2)
				{
					throw new InvalidIdMonikerTooLongException();
				}
				num += 2 + num2 + 1;
			}
			else if (idHeaderInformation.IdStorageType == IdStorageType.PublicFolderItem)
			{
				num += 3 + idHeaderInformation.FolderIdBytes.Length;
			}
			num += ServiceIdConverter.GetRequiredByteCountForAttachmentIds(attachmentIds);
			byte[] array = new byte[num];
			int num3 = 0;
			array[num3++] = 0;
			array[num3++] = (byte)idHeaderInformation.IdStorageType;
			switch (idHeaderInformation.IdStorageType)
			{
			case IdStorageType.MailboxItemSmtpAddressBased:
			case IdStorageType.MailboxItemMailboxGuidBased:
			case IdStorageType.ConversationIdMailboxGuidBased:
				num3 += ExBitConverter.Write((short)num2, array, num3);
				if (idHeaderInformation.IdStorageType == IdStorageType.MailboxItemSmtpAddressBased)
				{
					num3 += MailboxIdSerializer.EmailAddressToBytes(idHeaderInformation.MailboxId.SmtpAddress, array, num3);
				}
				else
				{
					num3 += MailboxIdSerializer.MailboxGuidToBytes(idHeaderInformation.MailboxId.MailboxGuid, array, num3);
				}
				array[num3++] = (byte)idHeaderInformation.IdProcessingInstruction;
				num3 += ExBitConverter.Write((short)idHeaderInformation.StoreIdBytes.Length, array, num3);
				Array.Copy(idHeaderInformation.StoreIdBytes, 0, array, num3, idHeaderInformation.StoreIdBytes.Length);
				num3 += idHeaderInformation.StoreIdBytes.Length;
				break;
			case IdStorageType.PublicFolder:
			case IdStorageType.ActiveDirectoryObject:
				num3 += ExBitConverter.Write((short)idHeaderInformation.StoreIdBytes.Length, array, num3);
				Array.Copy(idHeaderInformation.StoreIdBytes, 0, array, num3, idHeaderInformation.StoreIdBytes.Length);
				num3 += idHeaderInformation.StoreIdBytes.Length;
				break;
			case IdStorageType.PublicFolderItem:
				array[num3++] = (byte)idHeaderInformation.IdProcessingInstruction;
				num3 += ExBitConverter.Write((short)idHeaderInformation.StoreIdBytes.Length, array, num3);
				Array.Copy(idHeaderInformation.StoreIdBytes, 0, array, num3, idHeaderInformation.StoreIdBytes.Length);
				num3 += idHeaderInformation.StoreIdBytes.Length;
				num3 += ExBitConverter.Write((short)idHeaderInformation.FolderIdBytes.Length, array, num3);
				Array.Copy(idHeaderInformation.FolderIdBytes, 0, array, num3, idHeaderInformation.FolderIdBytes.Length);
				num3 += idHeaderInformation.FolderIdBytes.Length;
				break;
			}
			ServiceIdConverter.WriteAttachmentIds(attachmentIds, array, num3);
			if (attemptCompression)
			{
				array = ServiceIdConverter.AttemptCompression(array, out num);
			}
			return Convert.ToBase64String(array, 0, num);
		}

		// Token: 0x06007724 RID: 30500 RVA: 0x0020DDD0 File Offset: 0x0020BFD0
		public static void WriteAttachmentIds(IList<AttachmentId> attachmentIds, byte[] idBytes, int index)
		{
			if (attachmentIds != null)
			{
				if (attachmentIds.Count > 255)
				{
					throw new InvalidIdTooManyAttachmentLevelsException();
				}
				idBytes[index++] = (byte)attachmentIds.Count;
				for (int i = 0; i < attachmentIds.Count; i++)
				{
					byte[] array = attachmentIds[i].ToByteArray();
					index += ExBitConverter.Write((short)array.Length, idBytes, index);
					Array.Copy(array, 0, idBytes, index, array.Length);
					index += array.Length;
				}
			}
		}

		// Token: 0x06007725 RID: 30501 RVA: 0x0020DE44 File Offset: 0x0020C044
		public static int GetRequiredByteCountForAttachmentIds(IList<AttachmentId> attachmentIds)
		{
			int num = 0;
			if (attachmentIds != null)
			{
				num++;
				if (attachmentIds.Count > 255)
				{
					throw new InvalidIdTooManyAttachmentLevelsException();
				}
				for (int i = 0; i < attachmentIds.Count; i++)
				{
					num += 2 + attachmentIds[i].GetByteArrayLength();
				}
			}
			return num;
		}

		// Token: 0x06007726 RID: 30502 RVA: 0x0020DE90 File Offset: 0x0020C090
		public static void ReadAttachmentIds(BinaryReader reader, List<AttachmentId> attachmentIds)
		{
			byte b = reader.ReadByte();
			if (b == 0)
			{
				ServiceIdConverter.TraceDebug("[IdConverter::ReadAttachmentIds] Number of attachment indicator was set to 0.  Shouldn't be there at all in that case");
				throw new InvalidIdException();
			}
			if (b > 255)
			{
				ServiceIdConverter.TraceDebug("[IdConverter::ReadAttachmentIds] Number of attachments in id is greater than allowed value");
				throw new InvalidIdMalformedException();
			}
			for (int i = 0; i < (int)b; i++)
			{
				short num = reader.ReadInt16();
				if (num <= 0)
				{
					throw new InvalidIdMalformedException();
				}
				byte[] array = reader.ReadBytes((int)num);
				if (array.Length != (int)num)
				{
					ServiceIdConverter.TraceDebug("[IdConverter::ReadAttachmentIds] Attachment Id length did not match actual length");
					throw new InvalidIdMalformedException();
				}
				attachmentIds.Add(AttachmentId.Deserialize(array));
			}
		}

		// Token: 0x06007727 RID: 30503 RVA: 0x0020DF18 File Offset: 0x0020C118
		public static IdHeaderInformation ConvertFromConcatenatedId(string id, BasicTypes expectedType, List<AttachmentId> attachmentIds)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw new InvalidIdEmptyException();
			}
			EnumValidator.ThrowIfInvalid<BasicTypes>(expectedType, "expectedType");
			IdHeaderInformation result;
			try
			{
				IdHeaderInformation idHeaderInformation = new IdHeaderInformation();
				using (MemoryStream decompressedMemoryStream = ServiceIdConverter.GetDecompressedMemoryStream(id))
				{
					using (BinaryReader binaryReader = new BinaryReader(decompressedMemoryStream))
					{
						idHeaderInformation.IdStorageType = ServiceIdConverter.ReadIdStorageType(binaryReader, expectedType);
						switch (idHeaderInformation.IdStorageType)
						{
						case IdStorageType.MailboxItemSmtpAddressBased:
							idHeaderInformation.MailboxId = new MailboxId(MailboxIdSerializer.EmailAddressFromBytes(ServiceIdConverter.ReadMoniker(binaryReader, expectedType)));
							idHeaderInformation.IdProcessingInstruction = ServiceIdConverter.ReadIdProcessingInstruction(binaryReader, expectedType);
							idHeaderInformation.StoreIdBytes = ServiceIdConverter.ReadStoreId(binaryReader, expectedType);
							break;
						case IdStorageType.PublicFolder:
						case IdStorageType.ActiveDirectoryObject:
							idHeaderInformation.StoreIdBytes = ServiceIdConverter.ReadStoreId(binaryReader, expectedType);
							break;
						case IdStorageType.PublicFolderItem:
							idHeaderInformation.IdProcessingInstruction = ServiceIdConverter.ReadIdProcessingInstruction(binaryReader, expectedType);
							idHeaderInformation.StoreIdBytes = ServiceIdConverter.ReadStoreId(binaryReader, expectedType);
							idHeaderInformation.FolderIdBytes = ServiceIdConverter.ReadStoreId(binaryReader, expectedType);
							break;
						case IdStorageType.MailboxItemMailboxGuidBased:
						case IdStorageType.ConversationIdMailboxGuidBased:
							idHeaderInformation.MailboxId = new MailboxId(MailboxIdSerializer.MailboxGuidFromBytes(ServiceIdConverter.ReadMoniker(binaryReader, expectedType)));
							idHeaderInformation.IdProcessingInstruction = ServiceIdConverter.ReadIdProcessingInstruction(binaryReader, expectedType);
							idHeaderInformation.StoreIdBytes = ServiceIdConverter.ReadStoreId(binaryReader, expectedType);
							break;
						default:
							ServiceIdConverter.TraceDebug("[IdConverter::ConvertFromConcatenatedId] Invalid id storage type");
							throw new InvalidIdMalformedException();
						}
						if (attachmentIds != null)
						{
							if (decompressedMemoryStream.Position < decompressedMemoryStream.Length)
							{
								ServiceIdConverter.ReadAttachmentIds(binaryReader, expectedType, attachmentIds);
							}
							else if (expectedType == BasicTypes.Attachment)
							{
								throw new InvalidIdNotAnItemAttachmentIdException();
							}
						}
					}
				}
				result = idHeaderInformation;
			}
			catch (EndOfStreamException innerException)
			{
				throw new InvalidIdMalformedException(innerException);
			}
			catch (CorruptDataException innerException2)
			{
				throw new InvalidIdMalformedException(innerException2);
			}
			catch (FormatException innerException3)
			{
				throw new InvalidIdMalformedException(innerException3);
			}
			return result;
		}

		// Token: 0x06007728 RID: 30504 RVA: 0x0020E118 File Offset: 0x0020C318
		public static bool IsPublicFolder(string id)
		{
			bool result;
			try
			{
				using (MemoryStream decompressedMemoryStream = ServiceIdConverter.GetDecompressedMemoryStream(id))
				{
					using (BinaryReader binaryReader = new BinaryReader(decompressedMemoryStream))
					{
						result = (ServiceIdConverter.ReadIdStorageType(binaryReader, BasicTypes.Folder) == IdStorageType.PublicFolder);
					}
				}
			}
			catch (EndOfStreamException innerException)
			{
				throw new InvalidIdMalformedException(innerException);
			}
			catch (CorruptDataException innerException2)
			{
				throw new InvalidIdMalformedException(innerException2);
			}
			catch (FormatException innerException3)
			{
				throw new InvalidIdMalformedException(innerException3);
			}
			return result;
		}

		// Token: 0x06007729 RID: 30505 RVA: 0x0020E1B4 File Offset: 0x0020C3B4
		internal static byte[] AttemptCompression(byte[] streamIn, out int outBytesRequired)
		{
			return ServiceIdConverter.rleCompressor.Compress(streamIn, 1, out outBytesRequired);
		}

		// Token: 0x0600772A RID: 30506 RVA: 0x0020E1C4 File Offset: 0x0020C3C4
		private static MemoryStream GetDecompressedMemoryStream(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw new InvalidIdEmptyException();
			}
			byte[] array = Convert.FromBase64String(id);
			if (array.Length == 0)
			{
				throw new InvalidIdEmptyException();
			}
			CompressionId compressionId = (CompressionId)array[0];
			if (compressionId != CompressionId.NoCompression && compressionId != CompressionId.RleCompression)
			{
				ServiceIdConverter.TraceDebug("[IdConverter::IsPublicFolder] Invalid compression id");
				throw new InvalidIdMalformedException();
			}
			IIdCompressor idCompressor2;
			if (compressionId != CompressionId.RleCompression)
			{
				IIdCompressor idCompressor = ServiceIdConverter.passthruCompressor;
				idCompressor2 = idCompressor;
			}
			else
			{
				idCompressor2 = ServiceIdConverter.rleCompressor;
			}
			IIdCompressor idCompressor3 = idCompressor2;
			return idCompressor3.Decompress(array, 1024);
		}

		// Token: 0x0600772B RID: 30507 RVA: 0x0020E22C File Offset: 0x0020C42C
		private static IdStorageType ReadIdStorageType(BinaryReader reader, BasicTypes expectedType)
		{
			IdStorageType idStorageType = (IdStorageType)reader.ReadByte();
			if (!EnumValidator.IsValidValue<IdStorageType>(idStorageType))
			{
				ServiceIdConverter.TraceDebug("[IdConverter::ReadEnumValue] Invalid value for id storage type");
				throw new InvalidIdMalformedException();
			}
			return idStorageType;
		}

		// Token: 0x0600772C RID: 30508 RVA: 0x0020E259 File Offset: 0x0020C459
		private static void ReadAttachmentIds(BinaryReader reader, BasicTypes expectedType, List<AttachmentId> attachmentIds)
		{
			if (expectedType != BasicTypes.Attachment && expectedType != BasicTypes.ItemOrAttachment)
			{
				ServiceIdConverter.TraceDebug("[IdConverter::ReadAttachmentIds] Did not expect attachments in id, but received attachments embedded in id");
				throw new InvalidIdException();
			}
			ServiceIdConverter.ReadAttachmentIds(reader, attachmentIds);
		}

		// Token: 0x0600772D RID: 30509 RVA: 0x0020E27C File Offset: 0x0020C47C
		private static byte[] ReadMoniker(BinaryReader reader, BasicTypes expectedType)
		{
			short num = reader.ReadInt16();
			if (num <= 0)
			{
				ServiceIdConverter.TraceDebug("[IdConverter::ReadMoniker] Moniker length <= 0");
				throw new InvalidIdMalformedException();
			}
			byte[] array = reader.ReadBytes((int)num);
			if (array.Length != (int)num)
			{
				ServiceIdConverter.TraceDebug("[IdConverter::ReadMoniker] Moniker length did not match actual moniker length");
				throw new InvalidIdMalformedException();
			}
			return array;
		}

		// Token: 0x0600772E RID: 30510 RVA: 0x0020E2C4 File Offset: 0x0020C4C4
		private static IdProcessingInstruction ReadIdProcessingInstruction(BinaryReader reader, BasicTypes expectedType)
		{
			IdProcessingInstruction idProcessingInstruction = (IdProcessingInstruction)reader.ReadByte();
			if (!EnumValidator.IsValidValue<IdProcessingInstruction>(idProcessingInstruction))
			{
				ServiceIdConverter.TraceDebug("[IdConverter::ReadEnumValue] Invalid value for id processing instruction");
				throw new InvalidIdMalformedException();
			}
			return idProcessingInstruction;
		}

		// Token: 0x0600772F RID: 30511 RVA: 0x0020E2F4 File Offset: 0x0020C4F4
		private static byte[] ReadStoreId(BinaryReader reader, BasicTypes expectedType)
		{
			short num = reader.ReadInt16();
			if (num < 0)
			{
				ServiceIdConverter.TraceDebug("[IdConverter::ReadStoreId] StoreId count < 0");
				throw new InvalidIdMalformedException();
			}
			byte[] array = reader.ReadBytes((int)num);
			if (array.Length != (int)num)
			{
				ServiceIdConverter.TraceDebug("[IdConverter::ReadStoreId] StoreId bytes length did not match actual length");
				throw new InvalidIdMalformedException();
			}
			return array;
		}

		// Token: 0x06007730 RID: 30512 RVA: 0x0020E33B File Offset: 0x0020C53B
		private static void TraceDebug(string message)
		{
			ExTraceGlobals.StorageTracer.TraceDebug(0L, message);
		}

		// Token: 0x04005286 RID: 21126
		private const int MaximumIncomingIdLength = 1024;

		// Token: 0x04005287 RID: 21127
		private const int MaximumIncomingMonikerLength = 512;

		// Token: 0x04005288 RID: 21128
		private const byte MaxAttachmentLevels = 255;

		// Token: 0x04005289 RID: 21129
		private static PassthruCompressor passthruCompressor = new PassthruCompressor();

		// Token: 0x0400528A RID: 21130
		private static RleCompressor rleCompressor = new RleCompressor();
	}
}
