using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.ContentTypes.Tnef;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Mime;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x02000100 RID: 256
	internal class TnefPropertyBag
	{
		// Token: 0x060007BE RID: 1982 RVA: 0x0001B6C8 File Offset: 0x000198C8
		static TnefPropertyBag()
		{
			int num = 0;
			TnefPropertyBag.MessageProperties = TnefPropertyBag.GetMessageProperties(ref num);
			TnefPropertyBag.NamedMessageProperties = TnefPropertyBag.GetNamedMessageProperties(ref num);
			TnefPropertyBag.MessagePropertyCount = num;
			num = 0;
			TnefPropertyBag.AttachmentProperties = TnefPropertyBag.GetAttachmentProperties(ref num);
			TnefPropertyBag.NamedAttachmentProperties = new Dictionary<TnefNameTag, int>(0, TnefPropertyBag.NameTagComparer);
			TnefPropertyBag.AttachmentPropertyCount = num;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0001B839 File Offset: 0x00019A39
		internal TnefPropertyBag(PureTnefMessage parentMessage)
		{
			this.parentMessage = parentMessage;
			this.attachmentData = null;
			this.supportedProperties = TnefPropertyBag.MessageProperties;
			this.supportedNamedProperties = TnefPropertyBag.NamedMessageProperties;
			this.properties = new TnefPropertyBag.PropertyData[TnefPropertyBag.MessagePropertyCount];
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0001B875 File Offset: 0x00019A75
		internal TnefPropertyBag(TnefAttachmentData attachmentData)
		{
			this.attachmentData = attachmentData;
			this.parentMessage = null;
			this.supportedProperties = TnefPropertyBag.AttachmentProperties;
			this.supportedNamedProperties = TnefPropertyBag.NamedAttachmentProperties;
			this.properties = new TnefPropertyBag.PropertyData[TnefPropertyBag.AttachmentPropertyCount];
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0001B8B4 File Offset: 0x00019AB4
		private static Dictionary<TnefPropertyTag, int> GetMessageProperties(ref int index)
		{
			Dictionary<TnefPropertyTag, int> dictionary = new Dictionary<TnefPropertyTag, int>(61, TnefPropertyBag.PropertyTagComparer);
			dictionary.Add(TnefPropertyTag.TnefCorrelationKey, index++);
			dictionary.Add(TnefPropertyTag.MessageCodepage, index++);
			dictionary.Add(TnefPropertyTag.InternetCPID, index++);
			dictionary.Add(TnefPropertyTag.MessageLocaleID, index++);
			dictionary.Add(TnefPropertyTag.ContentIdentifierA, index);
			dictionary.Add(TnefPropertyTag.ContentIdentifierW, index++);
			dictionary.Add(TnefPropertyTag.ReadReceiptRequested, index++);
			dictionary.Add(TnefPropertyTag.ReadReceiptDisplayNameA, index);
			dictionary.Add(TnefPropertyTag.ReadReceiptDisplayNameW, index++);
			dictionary.Add(TnefPropertyTag.ReadReceiptEmailAddressA, index);
			dictionary.Add(TnefPropertyTag.ReadReceiptEmailAddressW, index++);
			dictionary.Add(TnefPropertyTag.ReadReceiptAddrtypeA, index);
			dictionary.Add(TnefPropertyTag.ReadReceiptAddrtypeW, index++);
			dictionary.Add(TnefPropertyTag.ReadReceiptSmtpAddressA, index);
			dictionary.Add(TnefPropertyTag.ReadReceiptSmtpAddressW, index++);
			dictionary.Add(TnefPropertyTag.ReadReceiptEntryId, index++);
			dictionary.Add(TnefPropertyTag.SenderNameA, index);
			dictionary.Add(TnefPropertyTag.SenderNameW, index++);
			dictionary.Add(TnefPropertyTag.SenderEmailAddressA, index);
			dictionary.Add(TnefPropertyTag.SenderEmailAddressW, index++);
			dictionary.Add(TnefPropertyTag.SenderAddrtypeA, index);
			dictionary.Add(TnefPropertyTag.SenderAddrtypeW, index++);
			dictionary.Add(TnefPropertyTag.SenderEntryId, index++);
			dictionary.Add(TnefPropertyTag.SentRepresentingNameA, index);
			dictionary.Add(TnefPropertyTag.SentRepresentingNameW, index++);
			dictionary.Add(TnefPropertyTag.SentRepresentingEmailAddressA, index);
			dictionary.Add(TnefPropertyTag.SentRepresentingEmailAddressW, index++);
			dictionary.Add(TnefPropertyTag.SentRepresentingAddrtypeA, index);
			dictionary.Add(TnefPropertyTag.SentRepresentingAddrtypeW, index++);
			dictionary.Add(TnefPropertyTag.SentRepresentingEntryId, index++);
			dictionary.Add(TnefPropertyTag.ClientSubmitTime, index++);
			dictionary.Add(TnefPropertyTag.LastModificationTime, index++);
			dictionary.Add(TnefPropertyTag.ExpiryTime, index++);
			dictionary.Add(TnefPropertyTag.ReplyTime, index++);
			dictionary.Add(TnefPropertyTag.SubjectA, index);
			dictionary.Add(TnefPropertyTag.SubjectW, index++);
			dictionary.Add(TnefPropertyTag.NormalizedSubjectA, index);
			dictionary.Add(TnefPropertyTag.NormalizedSubjectW, index++);
			dictionary.Add(TnefPropertyTag.SubjectPrefixA, index);
			dictionary.Add(TnefPropertyTag.SubjectPrefixW, index++);
			dictionary.Add(TnefPropertyTag.ConversationTopicA, index);
			dictionary.Add(TnefPropertyTag.ConversationTopicW, index++);
			dictionary.Add(TnefPropertyTag.InternetMessageIdA, index);
			dictionary.Add(TnefPropertyTag.InternetMessageIdW, index++);
			dictionary.Add(TnefPropertyTag.Importance, index++);
			dictionary.Add(TnefPropertyTag.Priority, index++);
			dictionary.Add(TnefPropertyTag.Sensitivity, index++);
			dictionary.Add(TnefPropertyTag.MessageClassA, index);
			dictionary.Add(TnefPropertyTag.MessageClassW, index++);
			dictionary.Add(new TnefPropertyTag(TnefPropertyId.Body, TnefPropertyType.Binary), index);
			dictionary.Add(TnefPropertyTag.BodyA, index);
			dictionary.Add(TnefPropertyTag.BodyW, index++);
			dictionary.Add(TnefPropertyTag.RtfCompressed, index++);
			dictionary.Add(TnefPropertyTag.BodyHtmlB, index);
			dictionary.Add(TnefPropertyTag.BodyHtmlA, index);
			dictionary.Add(TnefPropertyTag.BodyHtmlW, index++);
			dictionary.Add(TnefPropertyTag.SendRecallReport, index++);
			dictionary.Add(TnefPropertyTag.OofReplyType, index++);
			dictionary.Add(TnefPropertyTag.AutoForwarded, index++);
			dictionary.Add(TnefPropertyTag.INetMailOverrideFormat, index++);
			dictionary.Add(TnefPropertyTag.INetMailOverrideCharset, index++);
			TnefPropertyBag.AddLookupEntries(dictionary);
			return dictionary;
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0001BD5C File Offset: 0x00019F5C
		private static Dictionary<TnefNameTag, int> GetNamedMessageProperties(ref int index)
		{
			Dictionary<TnefNameTag, int> dictionary = new Dictionary<TnefNameTag, int>(9, TnefPropertyBag.NameTagComparer);
			dictionary.Add(TnefPropertyBag.TnefNameTagIsClassified, index++);
			dictionary.Add(TnefPropertyBag.TnefNameTagClassification, index++);
			dictionary.Add(TnefPropertyBag.TnefNameTagClassificationDescription, index++);
			dictionary.Add(TnefPropertyBag.TnefNameTagClassificationGuid, index++);
			dictionary.Add(TnefPropertyBag.TnefNameTagClassificationKeep, index++);
			dictionary.Add(TnefPropertyBag.TnefNameTagContentTypeA, index);
			dictionary.Add(TnefPropertyBag.TnefNameTagContentTypeW, index++);
			dictionary.Add(TnefPropertyBag.TnefNameTagContentClassA, index);
			dictionary.Add(TnefPropertyBag.TnefNameTagContentClassW, index++);
			TnefPropertyBag.AddNamedLookupEntries(dictionary);
			return dictionary;
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0001BE2C File Offset: 0x0001A02C
		private static Dictionary<TnefPropertyTag, int> GetAttachmentProperties(ref int index)
		{
			Dictionary<TnefPropertyTag, int> dictionary = new Dictionary<TnefPropertyTag, int>(25, TnefPropertyBag.PropertyTagComparer);
			dictionary.Add(TnefPropertyTag.AttachDataBin, index);
			dictionary.Add(TnefPropertyTag.AttachDataObj, index++);
			dictionary.Add(TnefPropertyTag.AttachMethod, index++);
			dictionary.Add(TnefPropertyTag.AttachLongFilenameA, index);
			dictionary.Add(TnefPropertyTag.AttachLongFilenameW, index++);
			dictionary.Add(TnefPropertyTag.AttachFilenameA, index);
			dictionary.Add(TnefPropertyTag.AttachFilenameW, index++);
			dictionary.Add(TnefPropertyTag.AttachExtensionA, index);
			dictionary.Add(TnefPropertyTag.AttachExtensionW, index++);
			dictionary.Add(TnefPropertyTag.DisplayNameA, index);
			dictionary.Add(TnefPropertyTag.DisplayNameW, index++);
			dictionary.Add(TnefPropertyTag.AttachTransportNameA, index);
			dictionary.Add(TnefPropertyTag.AttachTransportNameW, index++);
			dictionary.Add(TnefPropertyTag.AttachPathnameA, index);
			dictionary.Add(TnefPropertyTag.AttachPathnameW, index++);
			dictionary.Add(TnefPropertyTag.AttachMimeTagA, index);
			dictionary.Add(TnefPropertyTag.AttachMimeTagW, index++);
			dictionary.Add(TnefPropertyTag.RenderingPosition, index++);
			dictionary.Add(TnefPropertyTag.AttachRendering, index++);
			dictionary.Add(TnefPropertyTag.AttachContentIdA, index++);
			dictionary.Add(TnefPropertyTag.AttachContentIdW, index++);
			dictionary.Add(TnefPropertyTag.AttachContentLocationA, index++);
			dictionary.Add(TnefPropertyTag.AttachContentLocationW, index++);
			dictionary.Add(TnefPropertyTag.AttachmentFlags, index++);
			dictionary.Add(TnefPropertyTag.AttachHidden, index++);
			TnefPropertyBag.AddLookupEntries(dictionary);
			return dictionary;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0001C028 File Offset: 0x0001A228
		internal void Dispose()
		{
			foreach (TnefPropertyBag.PropertyData propertyData in this.properties)
			{
				StoragePropertyValue storagePropertyValue = propertyData.Value as StoragePropertyValue;
				if (storagePropertyValue != null)
				{
					storagePropertyValue.DisposeStorage();
				}
			}
			if (this.newProperties != null)
			{
				foreach (KeyValuePair<TnefPropertyTag, object> keyValuePair in this.newProperties)
				{
					StoragePropertyValue storagePropertyValue2 = keyValuePair.Value as StoragePropertyValue;
					if (storagePropertyValue2 != null)
					{
						storagePropertyValue2.DisposeStorage();
					}
				}
				this.newProperties = null;
			}
			this.newNamedProperties = null;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0001C0E0 File Offset: 0x0001A2E0
		public object GetProperty(TnefPropertyTag tag, bool toUnicode)
		{
			if (toUnicode && TnefPropertyType.String8 == tag.TnefType)
			{
				tag = tag.ToUnicode();
			}
			return this[tag];
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0001C100 File Offset: 0x0001A300
		public void SetProperty(TnefPropertyTag tag, bool toUnicode, object value)
		{
			if (toUnicode && TnefPropertyType.String8 == tag.TnefType)
			{
				tag = tag.ToUnicode();
			}
			this[tag] = value;
		}

		// Token: 0x1700020C RID: 524
		internal object this[TnefPropertyTag tag]
		{
			get
			{
				object obj = this[tag.Id];
				if (obj != null)
				{
					return obj;
				}
				if (this.newProperties == null)
				{
					return null;
				}
				if (!this.newProperties.TryGetValue(tag, out obj))
				{
					return null;
				}
				return obj;
			}
			set
			{
				if (this[tag.Id] != null)
				{
					this[tag.Id] = value;
					return;
				}
				if (value != null)
				{
					if (this.newProperties == null)
					{
						this.newProperties = new Dictionary<TnefPropertyTag, object>(TnefPropertyBag.PropertyTagComparer);
					}
					this.newProperties[tag] = value;
					return;
				}
				if (this.newProperties != null)
				{
					this.newProperties.Remove(tag);
				}
			}
		}

		// Token: 0x1700020D RID: 525
		internal object this[TnefNameTag nameTag]
		{
			get
			{
				object obj = this[nameTag.Id];
				if (obj != null)
				{
					return obj;
				}
				if (this.newNamedProperties == null)
				{
					return null;
				}
				if (!this.newNamedProperties.TryGetValue(nameTag, out obj))
				{
					return null;
				}
				return obj;
			}
			set
			{
				if (this[nameTag.Id] != null)
				{
					this[nameTag.Id] = value;
					return;
				}
				if (value != null)
				{
					if (this.newNamedProperties == null)
					{
						this.newNamedProperties = new Dictionary<TnefNameTag, object>(TnefPropertyBag.NameTagComparer);
					}
					this.newNamedProperties[nameTag] = value;
					return;
				}
				if (this.newNamedProperties != null)
				{
					this.newNamedProperties.Remove(nameTag);
				}
			}
		}

		// Token: 0x1700020E RID: 526
		internal object this[TnefPropertyId id]
		{
			get
			{
				return this[this.GetIndex(id)];
			}
			set
			{
				if (this[id] != value)
				{
					this.SetDirty();
					this.Touch(id);
				}
				this[this.GetIndex(id)] = value;
			}
		}

		// Token: 0x1700020F RID: 527
		internal object this[TnefNameId nameId]
		{
			get
			{
				return this[this.GetIndex(nameId)];
			}
			set
			{
				if (this[nameId] != value)
				{
					this.SetDirty();
					this.Touch(nameId);
				}
				this[this.GetIndex(nameId)] = value;
			}
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0001C2E5 File Offset: 0x0001A4E5
		private static void StartAttributeIfNecessary(TnefWriter writer, TnefAttributeTag attributeTag, TnefAttributeLevel attributeLevel, ref bool startAttribute)
		{
			if (startAttribute)
			{
				writer.StartAttribute(attributeTag, attributeLevel);
				startAttribute = false;
			}
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0001C2F6 File Offset: 0x0001A4F6
		private static bool IsLegacyAttribute(TnefAttributeTag tag)
		{
			return tag != TnefAttributeTag.MapiProperties && tag != TnefAttributeTag.Attachment && tag != TnefAttributeTag.RecipientTable;
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0001C318 File Offset: 0x0001A518
		private static void AddLookupEntries(Dictionary<TnefPropertyTag, int> dictionary)
		{
			Dictionary<TnefPropertyTag, int> dictionary2 = new Dictionary<TnefPropertyTag, int>(dictionary.Count);
			foreach (KeyValuePair<TnefPropertyTag, int> keyValuePair in dictionary)
			{
				TnefPropertyTag key = new TnefPropertyTag(keyValuePair.Key.Id, TnefPropertyType.Null);
				if (!dictionary2.ContainsKey(key))
				{
					dictionary2.Add(key, keyValuePair.Value);
				}
			}
			foreach (KeyValuePair<TnefPropertyTag, int> keyValuePair2 in dictionary2)
			{
				dictionary.Add(keyValuePair2.Key, keyValuePair2.Value);
			}
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0001C3E8 File Offset: 0x0001A5E8
		private static void AddNamedLookupEntries(Dictionary<TnefNameTag, int> dictionary)
		{
			Dictionary<TnefNameTag, int> dictionary2 = new Dictionary<TnefNameTag, int>(dictionary.Count);
			foreach (KeyValuePair<TnefNameTag, int> keyValuePair in dictionary)
			{
				TnefNameTag key = new TnefNameTag(keyValuePair.Key.Id, TnefPropertyType.Null);
				if (!dictionary2.ContainsKey(key))
				{
					dictionary2.Add(key, keyValuePair.Value);
				}
			}
			foreach (KeyValuePair<TnefNameTag, int> keyValuePair2 in dictionary2)
			{
				dictionary.Add(keyValuePair2.Key, keyValuePair2.Value);
			}
		}

		// Token: 0x17000210 RID: 528
		private object this[int index]
		{
			get
			{
				return this.properties[index].Value;
			}
			set
			{
				StoragePropertyValue storagePropertyValue = this.properties[index].Value as StoragePropertyValue;
				if (storagePropertyValue != null)
				{
					storagePropertyValue.DisposeStorage();
				}
				this.properties[index].Value = value;
			}
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0001C50C File Offset: 0x0001A70C
		private int GetIndex(TnefPropertyId id)
		{
			TnefPropertyTag key = new TnefPropertyTag(id, TnefPropertyType.Null);
			int result;
			this.supportedProperties.TryGetValue(key, out result);
			return result;
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0001C534 File Offset: 0x0001A734
		private int GetIndex(TnefNameId nameId)
		{
			TnefNameTag key = new TnefNameTag(nameId, TnefPropertyType.Null);
			int result;
			this.supportedNamedProperties.TryGetValue(key, out result);
			return result;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0001C55A File Offset: 0x0001A75A
		internal void Touch(TnefPropertyId id)
		{
			this.Touch(this.GetIndex(id));
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0001C569 File Offset: 0x0001A769
		private void Touch(TnefNameId nameId)
		{
			this.Touch(this.GetIndex(nameId));
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0001C578 File Offset: 0x0001A778
		private void Touch(int index)
		{
			this.properties[index].IsDirty = true;
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0001C58C File Offset: 0x0001A78C
		internal bool Load(TnefReader reader, DataStorage tnefStorage, long tnefStart, long tnefEnd, TnefAttributeLevel level, int embeddingDepth, Charset binaryCharset)
		{
			bool result;
			while ((result = reader.ReadNextAttribute()) && TnefAttributeTag.AttachRenderData != reader.AttributeTag && level == reader.AttributeLevel)
			{
				if (TnefAttributeTag.RecipientTable == reader.AttributeTag)
				{
					if (level == TnefAttributeLevel.Message && this.parentMessage != null && 0 < embeddingDepth)
					{
						this.parentMessage.LoadRecipients(reader.PropertyReader);
					}
				}
				else
				{
					TnefPropertyReader propertyReader = reader.PropertyReader;
					while (propertyReader.ReadNextProperty())
					{
						this.LoadProperty(propertyReader, tnefStorage, tnefStart, tnefEnd, level, embeddingDepth, binaryCharset);
					}
				}
			}
			return result;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0001C614 File Offset: 0x0001A814
		internal bool Write(TnefReader reader, TnefWriter writer, TnefAttributeLevel level, bool dropRecipientTable, bool forceUnicode, byte[] scratchBuffer)
		{
			IDictionary<TnefPropertyTag, object> dictionary = null;
			char[] array = null;
			bool result;
			for (;;)
			{
				TnefPropertyReader propertyReader = reader.PropertyReader;
				if (0 >= propertyReader.PropertyCount)
				{
					goto IL_37A;
				}
				TnefAttributeTag attributeTag = reader.AttributeTag;
				TnefAttributeLevel attributeLevel = reader.AttributeLevel;
				bool flag = true;
				while (propertyReader.ReadNextProperty())
				{
					TnefPropertyTag propertyTag = propertyReader.PropertyTag;
					if (TnefPropertyType.Null != propertyTag.ValueTnefType)
					{
						if (propertyReader.IsNamedProperty)
						{
							TnefNameId propertyNameId = propertyReader.PropertyNameId;
							TnefNameTag key = new TnefNameTag(propertyNameId, propertyTag.ValueTnefType);
							int num;
							if (this.supportedNamedProperties.TryGetValue(key, out num) && this.properties[num].IsDirty)
							{
								object obj = this[propertyNameId];
								if (obj != null)
								{
									TnefPropertyBag.StartAttributeIfNecessary(writer, attributeTag, attributeLevel, ref flag);
									writer.StartProperty(propertyTag, propertyNameId.PropertySetGuid, propertyNameId.Id);
									writer.WritePropertyValue(obj);
									continue;
								}
								continue;
							}
						}
						else
						{
							TnefPropertyId id = propertyTag.Id;
							int num2;
							if (this.supportedProperties.TryGetValue(propertyTag, out num2) && this.properties[num2].IsDirty && (this.attachmentData == null || this.attachmentData.EmbeddedMessage == null || TnefAttributeLevel.Attachment != level || TnefAttributeTag.AttachData != attributeTag || TnefPropertyId.AttachData != id))
							{
								object obj = this[id];
								if (obj == null)
								{
									continue;
								}
								if (!this.WriteModifiedProperty(writer, reader, propertyTag, obj, forceUnicode, ref flag, scratchBuffer))
								{
									if (dictionary == null)
									{
										dictionary = new Dictionary<TnefPropertyTag, object>(TnefPropertyBag.PropertyTagComparer);
									}
									if (!dictionary.ContainsKey(propertyTag))
									{
										dictionary.Add(propertyTag, obj);
										continue;
									}
									continue;
								}
								else
								{
									if (dictionary != null && dictionary.ContainsKey(propertyTag))
									{
										dictionary.Remove(propertyTag);
										continue;
									}
									continue;
								}
							}
						}
						if (propertyTag.ValueTnefType == TnefPropertyType.String8 && forceUnicode)
						{
							if (!TnefPropertyBag.IsLegacyAttribute(attributeTag))
							{
								TnefPropertyBag.StartAttributeIfNecessary(writer, attributeTag, attributeLevel, ref flag);
								TnefPropertyBag.WriteUnicodeProperty(writer, propertyReader, propertyTag.ToUnicode(), ref array);
							}
						}
						else if (propertyTag.IsTnefTypeValid)
						{
							TnefPropertyBag.StartAttributeIfNecessary(writer, attributeTag, attributeLevel, ref flag);
							writer.WriteProperty(propertyReader);
						}
					}
				}
				if ((TnefAttributeTag.MapiProperties == attributeTag && level == TnefAttributeLevel.Message) || (TnefAttributeTag.Attachment == attributeTag && level == TnefAttributeLevel.Attachment))
				{
					if (this.newProperties != null)
					{
						foreach (KeyValuePair<TnefPropertyTag, object> keyValuePair in this.newProperties)
						{
							object obj = keyValuePair.Value;
							if (obj != null)
							{
								this.WriteModifiedProperty(writer, reader, keyValuePair.Key, obj, forceUnicode, ref flag, scratchBuffer);
							}
						}
					}
					if (dictionary != null)
					{
						foreach (KeyValuePair<TnefPropertyTag, object> keyValuePair2 in dictionary)
						{
							this.WriteModifiedProperty(writer, reader, keyValuePair2.Key, keyValuePair2.Value, forceUnicode, ref flag, scratchBuffer);
						}
					}
					if (this.newNamedProperties != null)
					{
						using (IEnumerator<KeyValuePair<TnefNameTag, object>> enumerator3 = this.newNamedProperties.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								KeyValuePair<TnefNameTag, object> keyValuePair3 = enumerator3.Current;
								object obj = keyValuePair3.Value;
								if (obj != null)
								{
									TnefPropertyTag tag = new TnefPropertyTag((TnefPropertyId)(-32768), keyValuePair3.Key.Type);
									if (forceUnicode)
									{
										tag = tag.ToUnicode();
									}
									TnefPropertyBag.StartAttributeIfNecessary(writer, attributeTag, attributeLevel, ref flag);
									writer.StartProperty(tag, keyValuePair3.Key.Id.PropertySetGuid, keyValuePair3.Key.Id.Id);
									writer.WritePropertyValue(obj);
								}
							}
							goto IL_3AC;
						}
						goto IL_37A;
					}
				}
				IL_3AC:
				if (!(result = reader.ReadNextAttribute()) || level != reader.AttributeLevel || TnefAttributeTag.AttachRenderData == reader.AttributeTag)
				{
					break;
				}
				continue;
				IL_37A:
				if (level != TnefAttributeLevel.Message || TnefAttributeTag.RecipientTable != reader.AttributeTag)
				{
					writer.WriteAttribute(reader);
					goto IL_3AC;
				}
				if (!dropRecipientTable)
				{
					this.parentMessage.WriteRecipients(reader.PropertyReader, writer, ref array);
					goto IL_3AC;
				}
				goto IL_3AC;
			}
			return result;
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0001CA1C File Offset: 0x0001AC1C
		private void LoadProperty(TnefPropertyReader propertyReader, DataStorage tnefStorage, long tnefStart, long tnefEnd, TnefAttributeLevel level, int embeddingDepth, Charset binaryCharset)
		{
			TnefPropertyTag propertyTag = propertyReader.PropertyTag;
			if (propertyTag.IsMultiValued)
			{
				return;
			}
			if (TnefPropertyType.Null == propertyTag.ValueTnefType)
			{
				return;
			}
			if (propertyReader.IsNamedProperty)
			{
				TnefNameId propertyNameId = propertyReader.PropertyNameId;
				TnefNameTag key = new TnefNameTag(propertyNameId, propertyTag.ValueTnefType);
				if (this.supportedNamedProperties.ContainsKey(key))
				{
					if (propertyReader.IsLargeValue)
					{
						return;
					}
					this[this.GetIndex(propertyNameId)] = propertyReader.ReadValue();
				}
				return;
			}
			if (!this.supportedProperties.ContainsKey(propertyTag))
			{
				return;
			}
			TnefPropertyId id = propertyTag.Id;
			int index = this.GetIndex(id);
			if (TnefPropertyId.Body == id || TnefPropertyId.RtfCompressed == id || TnefPropertyId.BodyHtml == id)
			{
				tnefStart += (long)propertyReader.RawValueStreamOffset;
				tnefEnd = tnefStart + (long)propertyReader.RawValueLength;
				this[index] = new StoragePropertyValue(propertyTag, tnefStorage, tnefStart, tnefEnd);
				return;
			}
			if (TnefPropertyId.AttachData == id)
			{
				tnefStart += (long)propertyReader.RawValueStreamOffset;
				tnefEnd = tnefStart + (long)propertyReader.RawValueLength;
				this[index] = new StoragePropertyValue(propertyTag, tnefStorage, tnefStart, tnefEnd);
				if (!propertyReader.IsEmbeddedMessage)
				{
					return;
				}
				if (++embeddingDepth > 100)
				{
					throw new MimeException(EmailMessageStrings.NestingTooDeep(embeddingDepth, 100));
				}
				using (TnefReader embeddedMessageReader = propertyReader.GetEmbeddedMessageReader())
				{
					PureTnefMessage pureTnefMessage = new PureTnefMessage(this.attachmentData, tnefStorage, tnefStart, tnefEnd);
					pureTnefMessage.Load(embeddedMessageReader, embeddingDepth, binaryCharset);
					EmailMessage embeddedMessage = new EmailMessage(pureTnefMessage);
					this.attachmentData.EmbeddedMessage = embeddedMessage;
					return;
				}
			}
			if (propertyReader.IsLargeValue)
			{
				return;
			}
			if (TnefPropertyId.InternetCPID == id)
			{
				if (TnefPropertyType.Long == propertyTag.TnefType)
				{
					int num = propertyReader.ReadValueAsInt32();
					this[index] = num;
					return;
				}
			}
			else
			{
				this[index] = propertyReader.ReadValue();
			}
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0001CBF4 File Offset: 0x0001ADF4
		internal static void WriteUnicodeProperty(TnefWriter writer, TnefPropertyReader propertyReader, TnefPropertyTag tag, ref char[] buffer)
		{
			if (tag.IsNamed)
			{
				TnefNameId propertyNameId = propertyReader.PropertyNameId;
				writer.StartProperty(tag, propertyNameId.PropertySetGuid, propertyNameId.Id);
			}
			else
			{
				writer.StartProperty(tag);
			}
			if (tag.IsMultiValued)
			{
				while (propertyReader.ReadNextValue())
				{
					writer.StartPropertyValue();
					TnefPropertyBag.WriteUnicodePropertyValue(writer, propertyReader, ref buffer);
				}
				return;
			}
			TnefPropertyBag.WriteUnicodePropertyValue(writer, propertyReader, ref buffer);
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0001CC5C File Offset: 0x0001AE5C
		internal static void WriteUnicodePropertyValue(TnefWriter writer, TnefPropertyReader propertyReader, ref char[] buffer)
		{
			if (buffer == null)
			{
				buffer = new char[1024];
			}
			int count;
			while ((count = propertyReader.ReadTextValue(buffer, 0, buffer.Length)) != 0)
			{
				writer.WritePropertyTextValue(buffer, 0, count);
			}
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0001CC98 File Offset: 0x0001AE98
		private bool WriteModifiedProperty(TnefWriter writer, TnefReader reader, TnefPropertyTag propertyTag, object value, bool forceUnicode, ref bool startAttribute, byte[] scratchBuffer)
		{
			TnefPropertyReader propertyReader = reader.PropertyReader;
			TnefAttributeTag attributeTag = reader.AttributeTag;
			TnefAttributeLevel attributeLevel = reader.AttributeLevel;
			StoragePropertyValue storagePropertyValue = value as StoragePropertyValue;
			if (storagePropertyValue != null)
			{
				if (this.attachmentData != null && this.attachmentData.EmbeddedMessage != null && propertyReader.IsEmbeddedMessage)
				{
					using (TnefReader embeddedMessageReader = propertyReader.GetEmbeddedMessageReader())
					{
						TnefPropertyBag.StartAttributeIfNecessary(writer, attributeTag, attributeLevel, ref startAttribute);
						writer.StartProperty(propertyTag);
						EmailMessage embeddedMessage = this.attachmentData.EmbeddedMessage;
						PureTnefMessage pureTnefMessage = embeddedMessage.PureTnefMessage;
						Charset textCharset = pureTnefMessage.TextCharset;
						using (TnefWriter embeddedMessageWriter = writer.GetEmbeddedMessageWriter(textCharset.CodePage))
						{
							pureTnefMessage.WriteMessage(embeddedMessageReader, embeddedMessageWriter, scratchBuffer);
						}
						return true;
					}
				}
				using (Stream readStream = storagePropertyValue.GetReadStream())
				{
					int num = readStream.Read(scratchBuffer, 0, scratchBuffer.Length);
					if (num > 0)
					{
						propertyTag = storagePropertyValue.PropertyTag;
						if (propertyTag.ValueTnefType == TnefPropertyType.Unicode && TnefPropertyBag.IsLegacyAttribute(attributeTag))
						{
							return false;
						}
						TnefPropertyBag.StartAttributeIfNecessary(writer, attributeTag, attributeLevel, ref startAttribute);
						writer.StartProperty(propertyTag);
						do
						{
							writer.WritePropertyRawValue(scratchBuffer, 0, num);
							num = readStream.Read(scratchBuffer, 0, scratchBuffer.Length);
						}
						while (num > 0);
					}
					return true;
				}
			}
			if (propertyTag.ValueTnefType == TnefPropertyType.String8 && forceUnicode)
			{
				if (TnefPropertyBag.IsLegacyAttribute(attributeTag))
				{
					return false;
				}
				TnefPropertyBag.StartAttributeIfNecessary(writer, attributeTag, attributeLevel, ref startAttribute);
				writer.WriteProperty(propertyTag.ToUnicode(), value);
			}
			else
			{
				TnefPropertyBag.StartAttributeIfNecessary(writer, attributeTag, attributeLevel, ref startAttribute);
				writer.WriteProperty(propertyTag, value);
			}
			return true;
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0001CE50 File Offset: 0x0001B050
		private void SetDirty()
		{
			if (this.attachmentData != null)
			{
				((PureTnefMessage)this.attachmentData.MessageImplementation).SetDirty();
				return;
			}
			if (this.parentMessage != null)
			{
				this.parentMessage.SetDirty();
			}
		}

		// Token: 0x0400043C RID: 1084
		internal const int MaxTnefDepth = 100;

		// Token: 0x0400043D RID: 1085
		internal static readonly TnefPropertyBag.TnefPropertyTagComparer PropertyTagComparer = new TnefPropertyBag.TnefPropertyTagComparer();

		// Token: 0x0400043E RID: 1086
		internal static readonly TnefPropertyBag.TnefNameTagComparer NameTagComparer = new TnefPropertyBag.TnefNameTagComparer();

		// Token: 0x0400043F RID: 1087
		internal static readonly TnefNameTag TnefNameTagIsClassified = new TnefNameTag(new TnefNameId(TnefPropertyBag.PropertySetIDCommon, 34229), TnefPropertyType.Boolean);

		// Token: 0x04000440 RID: 1088
		internal static readonly TnefNameTag TnefNameTagClassification = new TnefNameTag(new TnefNameId(TnefPropertyBag.PropertySetIDCommon, 34230), TnefPropertyType.Unicode);

		// Token: 0x04000441 RID: 1089
		internal static readonly TnefNameTag TnefNameTagClassificationDescription = new TnefNameTag(new TnefNameId(TnefPropertyBag.PropertySetIDCommon, 34231), TnefPropertyType.Unicode);

		// Token: 0x04000442 RID: 1090
		internal static readonly TnefNameTag TnefNameTagClassificationGuid = new TnefNameTag(new TnefNameId(TnefPropertyBag.PropertySetIDCommon, 34232), TnefPropertyType.Unicode);

		// Token: 0x04000443 RID: 1091
		internal static readonly TnefNameTag TnefNameTagClassificationKeep = new TnefNameTag(new TnefNameId(TnefPropertyBag.PropertySetIDCommon, 34234), TnefPropertyType.Boolean);

		// Token: 0x04000444 RID: 1092
		internal static readonly TnefNameId TnefNameIdContentType = new TnefNameId(new Guid("00020386-0000-0000-c000-000000000046"), "content-type");

		// Token: 0x04000445 RID: 1093
		internal static readonly TnefNameId TnefNameIdContentClass = new TnefNameId(new Guid("00020386-0000-0000-c000-000000000046"), "content-class");

		// Token: 0x04000446 RID: 1094
		private static readonly Guid PropertySetIDCommon = new Guid("{00062008-0000-0000-C000-000000000046}");

		// Token: 0x04000447 RID: 1095
		private static readonly TnefNameTag TnefNameTagContentTypeA = new TnefNameTag(TnefPropertyBag.TnefNameIdContentType, TnefPropertyType.String8);

		// Token: 0x04000448 RID: 1096
		private static readonly TnefNameTag TnefNameTagContentTypeW = new TnefNameTag(TnefPropertyBag.TnefNameIdContentType, TnefPropertyType.Unicode);

		// Token: 0x04000449 RID: 1097
		private static readonly TnefNameTag TnefNameTagContentClassA = new TnefNameTag(TnefPropertyBag.TnefNameIdContentClass, TnefPropertyType.String8);

		// Token: 0x0400044A RID: 1098
		private static readonly TnefNameTag TnefNameTagContentClassW = new TnefNameTag(TnefPropertyBag.TnefNameIdContentClass, TnefPropertyType.Unicode);

		// Token: 0x0400044B RID: 1099
		private static readonly Dictionary<TnefPropertyTag, int> MessageProperties;

		// Token: 0x0400044C RID: 1100
		private static readonly Dictionary<TnefNameTag, int> NamedMessageProperties;

		// Token: 0x0400044D RID: 1101
		private static readonly Dictionary<TnefPropertyTag, int> AttachmentProperties;

		// Token: 0x0400044E RID: 1102
		private static readonly Dictionary<TnefNameTag, int> NamedAttachmentProperties;

		// Token: 0x0400044F RID: 1103
		private static readonly int MessagePropertyCount;

		// Token: 0x04000450 RID: 1104
		private static readonly int AttachmentPropertyCount;

		// Token: 0x04000451 RID: 1105
		private Dictionary<TnefPropertyTag, int> supportedProperties;

		// Token: 0x04000452 RID: 1106
		private Dictionary<TnefNameTag, int> supportedNamedProperties;

		// Token: 0x04000453 RID: 1107
		private TnefPropertyBag.PropertyData[] properties;

		// Token: 0x04000454 RID: 1108
		private PureTnefMessage parentMessage;

		// Token: 0x04000455 RID: 1109
		private TnefAttachmentData attachmentData;

		// Token: 0x04000456 RID: 1110
		private IDictionary<TnefPropertyTag, object> newProperties;

		// Token: 0x04000457 RID: 1111
		private IDictionary<TnefNameTag, object> newNamedProperties;

		// Token: 0x02000101 RID: 257
		internal class TnefPropertyTagComparer : IEqualityComparer<TnefPropertyTag>
		{
			// Token: 0x060007E1 RID: 2017 RVA: 0x0001CE83 File Offset: 0x0001B083
			public bool Equals(TnefPropertyTag x, TnefPropertyTag y)
			{
				return x == y;
			}

			// Token: 0x060007E2 RID: 2018 RVA: 0x0001CE93 File Offset: 0x0001B093
			public int GetHashCode(TnefPropertyTag obj)
			{
				return obj;
			}
		}

		// Token: 0x02000102 RID: 258
		internal class TnefNameTagComparer : IEqualityComparer<TnefNameTag>
		{
			// Token: 0x060007E4 RID: 2020 RVA: 0x0001CEA4 File Offset: 0x0001B0A4
			public bool Equals(TnefNameTag x, TnefNameTag y)
			{
				return x.Type == y.Type && x.Id.Kind == y.Id.Kind && x.Id.Id == y.Id.Id && x.Id.PropertySetGuid == y.Id.PropertySetGuid && string.Equals(x.Id.Name, y.Id.Name);
			}

			// Token: 0x060007E5 RID: 2021 RVA: 0x0001CF54 File Offset: 0x0001B154
			public int GetHashCode(TnefNameTag obj)
			{
				return (int)obj.Type ^ obj.Id.PropertySetGuid.GetHashCode() ^ ((obj.Id.Kind == TnefNameIdKind.Id) ? obj.Id.Id : obj.Id.Name.GetHashCode());
			}
		}

		// Token: 0x02000103 RID: 259
		internal struct PropertyData
		{
			// Token: 0x060007E7 RID: 2023 RVA: 0x0001CFC6 File Offset: 0x0001B1C6
			public PropertyData(object value, bool isDirty)
			{
				this.value = value;
				this.isDirty = isDirty;
			}

			// Token: 0x17000211 RID: 529
			// (get) Token: 0x060007E8 RID: 2024 RVA: 0x0001CFD6 File Offset: 0x0001B1D6
			// (set) Token: 0x060007E9 RID: 2025 RVA: 0x0001CFDE File Offset: 0x0001B1DE
			public object Value
			{
				get
				{
					return this.value;
				}
				set
				{
					this.value = value;
				}
			}

			// Token: 0x17000212 RID: 530
			// (get) Token: 0x060007EA RID: 2026 RVA: 0x0001CFE7 File Offset: 0x0001B1E7
			// (set) Token: 0x060007EB RID: 2027 RVA: 0x0001CFEF File Offset: 0x0001B1EF
			public bool IsDirty
			{
				get
				{
					return this.isDirty;
				}
				set
				{
					this.isDirty = value;
				}
			}

			// Token: 0x04000458 RID: 1112
			private object value;

			// Token: 0x04000459 RID: 1113
			private bool isDirty;
		}
	}
}
