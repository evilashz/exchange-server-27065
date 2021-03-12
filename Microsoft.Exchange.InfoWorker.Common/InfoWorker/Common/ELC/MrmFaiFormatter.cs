﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.EventLog;

namespace Microsoft.Exchange.InfoWorker.Common.ELC
{
	// Token: 0x020001B1 RID: 433
	internal class MrmFaiFormatter
	{
		// Token: 0x06000B78 RID: 2936 RVA: 0x00030F70 File Offset: 0x0002F170
		internal static void Serialize(Dictionary<Guid, StoreTagData> dictionary, Dictionary<Guid, StoreTagData> defaultArchiveTagData, List<Guid> deletedTags, RetentionHoldData retentionHoldData, UserConfiguration configItem, bool fullCrawlRequired, IExchangePrincipal mailboxOwner)
		{
			using (Stream xmlStream = configItem.GetXmlStream())
			{
				MrmFaiFormatter.SerializeStoreTags(dictionary, defaultArchiveTagData, deletedTags, retentionHoldData, xmlStream, mailboxOwner, false, fullCrawlRequired);
			}
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00030FB0 File Offset: 0x0002F1B0
		internal static byte[] Serialize(Dictionary<Guid, StoreTagData> dictionary, Dictionary<Guid, StoreTagData> defaultArchiveTagData, List<Guid> deletedTags, RetentionHoldData retentionHoldData, bool fullCrawlRequired, IExchangePrincipal mailboxOwner)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				MrmFaiFormatter.SerializeStoreTags(dictionary, defaultArchiveTagData, deletedTags, retentionHoldData, memoryStream, mailboxOwner, false, fullCrawlRequired);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00030FF8 File Offset: 0x0002F1F8
		internal static byte[] SerializeDefaultPolicy(Dictionary<Guid, StoreTagData> dictionary, IExchangePrincipal mailboxOwner)
		{
			RetentionHoldData retentionHoldData = new RetentionHoldData(false, null, null);
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(2048))
			{
				MrmFaiFormatter.SerializeStoreTags(dictionary, null, null, retentionHoldData, memoryStream, mailboxOwner, true, false);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0003104C File Offset: 0x0002F24C
		public static Dictionary<Guid, StoreTagData> Deserialize(UserConfiguration configItem, IExchangePrincipal mailboxOwner, out List<Guid> deletedTags, out bool fullCrawlRequired)
		{
			RetentionHoldData retentionHoldData;
			Dictionary<Guid, StoreTagData> dictionary;
			return MrmFaiFormatter.Deserialize(configItem, mailboxOwner, out deletedTags, out retentionHoldData, false, out dictionary, out fullCrawlRequired);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x00031068 File Offset: 0x0002F268
		internal static Dictionary<Guid, StoreTagData> Deserialize(UserConfiguration configItem, IExchangePrincipal mailboxOwner)
		{
			List<Guid> list;
			RetentionHoldData retentionHoldData;
			Dictionary<Guid, StoreTagData> dictionary;
			bool flag;
			return MrmFaiFormatter.Deserialize(configItem, mailboxOwner, out list, out retentionHoldData, false, out dictionary, out flag);
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00031088 File Offset: 0x0002F288
		internal static Dictionary<Guid, StoreTagData> Deserialize(UserConfiguration configItem, IExchangePrincipal mailboxOwner, out List<Guid> deletedTags, out RetentionHoldData retentionHoldData, bool returnRetentionHoldData, out Dictionary<Guid, StoreTagData> defaultArchiveTagData)
		{
			bool flag = false;
			Dictionary<Guid, StoreTagData> result;
			using (Stream xmlStream = configItem.GetXmlStream())
			{
				result = MrmFaiFormatter.Deserialize(xmlStream, mailboxOwner, out deletedTags, out retentionHoldData, returnRetentionHoldData, out defaultArchiveTagData, out flag);
			}
			return result;
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x000310CC File Offset: 0x0002F2CC
		internal static Dictionary<Guid, StoreTagData> Deserialize(UserConfiguration configItem, IExchangePrincipal mailboxOwner, out List<Guid> deletedTags, out RetentionHoldData retentionHoldData, bool returnRetentionHoldData, out Dictionary<Guid, StoreTagData> defaultArchiveTagData, out bool fullCrawlRequired)
		{
			Dictionary<Guid, StoreTagData> result;
			using (Stream xmlStream = configItem.GetXmlStream())
			{
				result = MrmFaiFormatter.Deserialize(xmlStream, mailboxOwner, out deletedTags, out retentionHoldData, returnRetentionHoldData, out defaultArchiveTagData, out fullCrawlRequired);
			}
			return result;
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00031110 File Offset: 0x0002F310
		internal static Dictionary<Guid, StoreTagData> Deserialize(byte[] xmlData, IExchangePrincipal mailboxOwner, out List<Guid> deletedTags, out RetentionHoldData retentionHoldData, bool returnRetentionHoldData, out Dictionary<Guid, StoreTagData> defaultArchiveTagData, out bool fullCrawlRequired)
		{
			Dictionary<Guid, StoreTagData> result;
			using (Stream stream = new MemoryStream(xmlData))
			{
				result = MrmFaiFormatter.Deserialize(stream, mailboxOwner, out deletedTags, out retentionHoldData, returnRetentionHoldData, out defaultArchiveTagData, out fullCrawlRequired);
			}
			return result;
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00031154 File Offset: 0x0002F354
		internal static Dictionary<Guid, StoreTagData> Deserialize(byte[] propertyBytes, IExchangePrincipal mailboxOwner)
		{
			Dictionary<Guid, StoreTagData> result;
			using (Stream stream = new MemoryStream(propertyBytes))
			{
				List<Guid> list;
				RetentionHoldData retentionHoldData;
				Dictionary<Guid, StoreTagData> dictionary;
				bool flag;
				result = MrmFaiFormatter.Deserialize(stream, mailboxOwner, out list, out retentionHoldData, false, out dictionary, out flag);
			}
			return result;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0003119C File Offset: 0x0002F39C
		private static void SerializeStoreTags(Dictionary<Guid, StoreTagData> dictionary, Dictionary<Guid, StoreTagData> defaultArchiveTagData, List<Guid> deletedTags, RetentionHoldData retentionHoldData, Stream dataStream, IExchangePrincipal mailboxOwner, bool compactDefaultTagOnly, bool fullCrawlRequired)
		{
			MrmFaiFormatter.Tracer.TraceDebug<IExchangePrincipal, int>(0L, "Mailbox:{0}. About to serialize tags to FAI. There are {1} items in the tag dictionary.", mailboxOwner, (dictionary == null) ? -1 : dictionary.Count);
			dataStream.SetLength(0L);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(dataStream, null))
			{
				xmlTextWriter.WriteStartElement("UserConfiguration");
				xmlTextWriter.WriteStartElement("Info");
				xmlTextWriter.WriteAttributeString("version", "Exchange.14");
				xmlTextWriter.WriteStartElement("Data");
				if (!compactDefaultTagOnly)
				{
					MrmFaiFormatter.Tracer.TraceDebug(0L, "Mailbox:{0}. About to write out hold info. HoldEnabled: {1}. Comment: {2}. Url: {3}", new object[]
					{
						mailboxOwner,
						retentionHoldData.HoldEnabled,
						retentionHoldData.Comment,
						retentionHoldData.Url
					});
					xmlTextWriter.WriteStartElement("RetentionHold");
					xmlTextWriter.WriteAttributeString("Enabled", retentionHoldData.HoldEnabled.ToString());
					xmlTextWriter.WriteAttributeString("RetentionComment", retentionHoldData.Comment);
					xmlTextWriter.WriteAttributeString("RetentionUrl", retentionHoldData.Url);
					xmlTextWriter.WriteEndElement();
				}
				xmlTextWriter.WriteStartElement("ArchiveSync");
				xmlTextWriter.WriteAttributeString("FullCrawlRequired", fullCrawlRequired.ToString());
				xmlTextWriter.WriteEndElement();
				if (deletedTags != null)
				{
					foreach (Guid guid in deletedTags)
					{
						xmlTextWriter.WriteStartElement("DeletedTag");
						xmlTextWriter.WriteAttributeString("Guid", guid.ToString());
						xmlTextWriter.WriteEndElement();
					}
				}
				foreach (StoreTagData storeTagData in dictionary.Values)
				{
					if (storeTagData == null)
					{
						MrmFaiFormatter.Tracer.TraceDebug<IExchangePrincipal>(0L, "Mailbox:{0}. Store data Tag is null, so it will not be serialized.", mailboxOwner);
					}
					else
					{
						bool flag = storeTagData.Tag.IsArchiveTag || ElcMailboxHelper.IsArchiveTag(storeTagData, false);
						if (storeTagData.Tag.Type == ElcFolderType.All && flag)
						{
							MrmFaiFormatter.Tracer.TraceDebug<IExchangePrincipal, string>(0L, "Mailbox:{0}. Tag {1} is a default Move To Archive tag. We don't serialize those.", mailboxOwner, storeTagData.Tag.Name);
						}
						else
						{
							MrmFaiFormatter.Tracer.TraceDebug<IExchangePrincipal, string>(0L, "Mailbox:{0}. Starting to serialize tag {1}.", mailboxOwner, storeTagData.Tag.Name);
							if (compactDefaultTagOnly && storeTagData.Tag.Type != ElcFolderType.All)
							{
								MrmFaiFormatter.Tracer.TraceDebug<IExchangePrincipal, string>(0L, "Mailbox:{0}. compactDefaultTagOnly is true and tag {1} is not default, so skip.", mailboxOwner, storeTagData.Tag.Name);
							}
							else if (flag)
							{
								MrmFaiFormatter.SerializeStoreTagData("ArchiveTag", storeTagData, xmlTextWriter, mailboxOwner, compactDefaultTagOnly);
							}
							else
							{
								MrmFaiFormatter.SerializeStoreTagData("PolicyTag", storeTagData, xmlTextWriter, mailboxOwner, compactDefaultTagOnly);
							}
						}
					}
				}
				if (defaultArchiveTagData != null)
				{
					foreach (StoreTagData storeTagData2 in defaultArchiveTagData.Values)
					{
						MrmFaiFormatter.SerializeStoreTagData("DefaultArchiveTag", storeTagData2, xmlTextWriter, mailboxOwner, compactDefaultTagOnly);
					}
				}
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteEndElement();
			}
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x000314F4 File Offset: 0x0002F6F4
		private static void SerializeStoreTagData(string startElement, StoreTagData storeTagData, XmlTextWriter xmlWriter, IExchangePrincipal mailboxOwner, bool compactDefaultTagOnly)
		{
			xmlWriter.WriteStartElement(startElement);
			xmlWriter.WriteAttributeString("ObjectGuid", storeTagData.Tag.Guid.ToString());
			xmlWriter.WriteAttributeString("Guid", storeTagData.Tag.RetentionId.ToString());
			xmlWriter.WriteAttributeString("Name", storeTagData.Tag.Name);
			xmlWriter.WriteAttributeString("Comment", storeTagData.Tag.Comment);
			xmlWriter.WriteAttributeString("Type", storeTagData.Tag.Type.ToString());
			if (!compactDefaultTagOnly)
			{
				MrmFaiFormatter.Tracer.TraceDebug(0L, "Mailbox:{0}. Tag {1}. IsVisible: {2}. OptedInto: {3}", new object[]
				{
					mailboxOwner,
					storeTagData.Tag.Name,
					storeTagData.IsVisible,
					storeTagData.OptedInto
				});
				xmlWriter.WriteAttributeString("MustDisplayComment", storeTagData.Tag.MustDisplayCommentEnabled.ToString());
				xmlWriter.WriteAttributeString("IsVisible", storeTagData.IsVisible.ToString());
				xmlWriter.WriteAttributeString("OptedInto", storeTagData.OptedInto.ToString());
				if (storeTagData.Tag.LocalizedRetentionPolicyTagName != null && storeTagData.Tag.LocalizedRetentionPolicyTagName.Length > 0)
				{
					MrmFaiFormatter.Tracer.TraceDebug<IExchangePrincipal, string, string[]>(0L, "Mailbox:{0}. Tag {1}. LocalizedRetentionPolicyTagName: {2}", mailboxOwner, storeTagData.Tag.Name, storeTagData.Tag.LocalizedRetentionPolicyTagName);
					MrmFaiFormatter.WriteLocalizedStrings(storeTagData.Tag.LocalizedRetentionPolicyTagName, xmlWriter, "LocalizedName", "Name");
				}
				if (storeTagData.Tag.LocalizedComment != null && storeTagData.Tag.LocalizedComment.Length > 0)
				{
					MrmFaiFormatter.Tracer.TraceDebug<IExchangePrincipal, string, string[]>(0L, "Mailbox:{0}. Tag {1}. LocalizedComment: {2}", mailboxOwner, storeTagData.Tag.Name, storeTagData.Tag.LocalizedComment);
					MrmFaiFormatter.WriteLocalizedStrings(storeTagData.Tag.LocalizedComment, xmlWriter, "LocalizedComment", "Comment");
				}
			}
			foreach (KeyValuePair<Guid, ContentSetting> keyValuePair in storeTagData.ContentSettings)
			{
				if (!keyValuePair.Value.RetentionEnabled)
				{
					MrmFaiFormatter.Tracer.TraceDebug<IExchangePrincipal, string, string>(0L, "Mailbox:{0}. Tag {1}. Content Setting {2} is disabled.", mailboxOwner, storeTagData.Tag.Name, keyValuePair.Value.Name);
				}
				else
				{
					MrmFaiFormatter.Tracer.TraceDebug(0L, "Mailbox:{0}. Serializing content settings for Tag {1}. Name: {2}. ExpiryAgeLimit: {3}. MessageClass: {4}", new object[]
					{
						mailboxOwner,
						storeTagData.Tag.Name,
						keyValuePair.Value.Name,
						keyValuePair.Value.AgeLimitForRetention.Value.TotalDays,
						keyValuePair.Value.MessageClass
					});
					xmlWriter.WriteStartElement("ContentSettings");
					xmlWriter.WriteAttributeString("Guid", keyValuePair.Key.ToString());
					xmlWriter.WriteAttributeString("ExpiryAgeLimit", keyValuePair.Value.AgeLimitForRetention.Value.TotalDays.ToString());
					xmlWriter.WriteAttributeString("MessageClass", keyValuePair.Value.MessageClass);
					xmlWriter.WriteAttributeString("RetentionAction", keyValuePair.Value.RetentionAction.ToString());
					xmlWriter.WriteEndElement();
				}
			}
			xmlWriter.WriteEndElement();
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x000318B0 File Offset: 0x0002FAB0
		private static Dictionary<Guid, StoreTagData> Deserialize(Stream xmlStream, IExchangePrincipal mailboxOwner, out List<Guid> deletedTags, out RetentionHoldData retentionHoldData, bool returnRetentionHoldData, out Dictionary<Guid, StoreTagData> defaultArchiveTagData, out bool fullCrawlRequired)
		{
			fullCrawlRequired = false;
			Dictionary<Guid, StoreTagData> dictionary = new Dictionary<Guid, StoreTagData>();
			defaultArchiveTagData = new Dictionary<Guid, StoreTagData>();
			retentionHoldData = default(RetentionHoldData);
			deletedTags = new List<Guid>();
			if (xmlStream.Length == 0L)
			{
				MrmFaiFormatter.Tracer.TraceDebug<IExchangePrincipal>(0L, "Mailbox:{0} has empty config message.", mailboxOwner);
				return dictionary;
			}
			using (XmlTextReader xmlTextReader = SafeXmlFactory.CreateSafeXmlTextReader(xmlStream))
			{
				Exception ex = null;
				try
				{
					xmlTextReader.MoveToContent();
					if (returnRetentionHoldData)
					{
						if (!xmlTextReader.ReadToFollowing("RetentionHold"))
						{
							MrmFaiFormatter.Tracer.TraceDebug<IExchangePrincipal>(0L, "Mailbox:{0}. Config item exists, but there is no RetentionHold node in it.", mailboxOwner);
						}
						else
						{
							if (xmlTextReader.MoveToAttribute("Enabled"))
							{
								retentionHoldData.HoldEnabled = bool.Parse(xmlTextReader.Value);
							}
							if (xmlTextReader.MoveToAttribute("RetentionComment"))
							{
								retentionHoldData.Comment = xmlTextReader.Value;
							}
							if (xmlTextReader.MoveToAttribute("RetentionUrl"))
							{
								retentionHoldData.Url = xmlTextReader.Value;
							}
							MrmFaiFormatter.Tracer.TraceDebug(0L, "Mailbox:{0}. Config item exists, and there is a RetentionHold node in it. HoldEnabled: {1}. Comment: {2}. Url: {3}", new object[]
							{
								mailboxOwner,
								retentionHoldData.HoldEnabled,
								retentionHoldData.Comment,
								retentionHoldData.Url
							});
						}
					}
					while (!MrmFaiFormatter.IsTagNode(xmlTextReader) && !MrmFaiFormatter.IsArchiveSyncNode(xmlTextReader) && xmlTextReader.Read())
					{
					}
					if (string.CompareOrdinal(xmlTextReader.Name, "ArchiveSync") == 0)
					{
						if (xmlTextReader.MoveToAttribute("FullCrawlRequired"))
						{
							fullCrawlRequired = bool.Parse(xmlTextReader.Value);
						}
						while (!MrmFaiFormatter.IsTagNode(xmlTextReader))
						{
							if (!xmlTextReader.Read())
							{
								break;
							}
						}
					}
					else
					{
						MrmFaiFormatter.Tracer.TraceDebug<IExchangePrincipal>(0L, "Mailbox:{0}. Config item exists, but there is no ArchiveSync node in it.", mailboxOwner);
					}
					for (;;)
					{
						bool flag = string.CompareOrdinal(xmlTextReader.Name, "PolicyTag") == 0;
						bool flag2 = string.CompareOrdinal(xmlTextReader.Name, "ArchiveTag") == 0;
						bool flag3 = string.CompareOrdinal(xmlTextReader.Name, "DefaultArchiveTag") == 0;
						bool flag4 = string.CompareOrdinal(xmlTextReader.Name, "DeletedTag") == 0;
						if (!flag && !flag2 && !flag3 && !flag4)
						{
							break;
						}
						MrmFaiFormatter.Tracer.TraceDebug<IExchangePrincipal>(0L, "Mailbox:{0}. Found at least 1 tag in Config item.", mailboxOwner);
						if (flag4)
						{
							Guid empty = System.Guid.Empty;
							if (xmlTextReader.MoveToAttribute("Guid"))
							{
								empty = new Guid(xmlTextReader.Value);
							}
							deletedTags.Add(empty);
							xmlTextReader.Read();
						}
						else
						{
							StoreTagData storeTagData = new StoreTagData();
							storeTagData.Tag = new RetentionTag();
							storeTagData.Tag.IsArchiveTag = flag2;
							if (xmlTextReader.MoveToAttribute("ObjectGuid"))
							{
								storeTagData.Tag.Guid = new Guid(xmlTextReader.Value);
							}
							if (xmlTextReader.MoveToAttribute("Guid"))
							{
								storeTagData.Tag.RetentionId = new Guid(xmlTextReader.Value);
							}
							if (xmlTextReader.MoveToAttribute("Name"))
							{
								storeTagData.Tag.Name = xmlTextReader.Value;
							}
							if (xmlTextReader.MoveToAttribute("Comment"))
							{
								storeTagData.Tag.Comment = xmlTextReader.Value;
							}
							if (xmlTextReader.MoveToAttribute("Type"))
							{
								storeTagData.Tag.Type = (ElcFolderType)Enum.Parse(typeof(ElcFolderType), xmlTextReader.Value);
							}
							if (xmlTextReader.MoveToAttribute("MustDisplayComment"))
							{
								storeTagData.Tag.MustDisplayCommentEnabled = bool.Parse(xmlTextReader.Value);
							}
							if (xmlTextReader.MoveToAttribute("IsVisible"))
							{
								storeTagData.IsVisible = bool.Parse(xmlTextReader.Value);
							}
							if (xmlTextReader.MoveToAttribute("OptedInto"))
							{
								storeTagData.OptedInto = bool.Parse(xmlTextReader.Value);
							}
							xmlTextReader.Read();
							if (string.CompareOrdinal(xmlTextReader.Name, "LocalizedName") == 0)
							{
								storeTagData.Tag.LocalizedRetentionPolicyTagName = MrmFaiFormatter.ReadLocalizedStrings(xmlTextReader, "Name");
							}
							if (string.CompareOrdinal(xmlTextReader.Name, "LocalizedComment") == 0)
							{
								storeTagData.Tag.LocalizedComment = MrmFaiFormatter.ReadLocalizedStrings(xmlTextReader, "Comment");
							}
							MrmFaiFormatter.Tracer.TraceDebug(0L, "Mailbox:{0}. Done reading the tag. Name: {1}. Type: {2}. IsVisible: {3}. OptedInto: {4}", new object[]
							{
								mailboxOwner,
								storeTagData.Tag.Name,
								storeTagData.Tag.Type,
								storeTagData.IsVisible,
								storeTagData.OptedInto
							});
							while (string.CompareOrdinal(xmlTextReader.Name, "ContentSettings") == 0)
							{
								ContentSetting contentSetting = new ContentSetting();
								Guid key;
								if (xmlTextReader.MoveToAttribute("Guid"))
								{
									key = new Guid(xmlTextReader.Value);
								}
								else
								{
									key = default(Guid);
								}
								if (xmlTextReader.MoveToAttribute("ExpiryAgeLimit"))
								{
									contentSetting.AgeLimitForRetention = new EnhancedTimeSpan?(EnhancedTimeSpan.FromDays(double.Parse(xmlTextReader.Value)));
								}
								contentSetting.RetentionEnabled = true;
								if (xmlTextReader.MoveToAttribute("MessageClass"))
								{
									contentSetting.MessageClass = xmlTextReader.Value;
								}
								if (xmlTextReader.MoveToAttribute("RetentionAction"))
								{
									contentSetting.RetentionAction = (RetentionActionType)Enum.Parse(typeof(RetentionActionType), xmlTextReader.Value, true);
								}
								storeTagData.ContentSettings[key] = contentSetting;
								MrmFaiFormatter.Tracer.TraceDebug(0L, "Mailbox:{0}. Done reading the content setting. RetentionEnabled: {1}. MessageClass: {2}. RetentionAction: {3}", new object[]
								{
									mailboxOwner,
									contentSetting.RetentionEnabled,
									contentSetting.MessageClass,
									contentSetting.RetentionAction
								});
								xmlTextReader.Read();
							}
							if (!flag3)
							{
								dictionary[storeTagData.Tag.RetentionId] = storeTagData;
							}
							else
							{
								defaultArchiveTagData[storeTagData.Tag.RetentionId] = storeTagData;
							}
						}
						if ((string.CompareOrdinal(xmlTextReader.Name, "PolicyTag") == 0 || string.CompareOrdinal(xmlTextReader.Name, "ArchiveTag") == 0 || string.CompareOrdinal(xmlTextReader.Name, "DefaultArchiveTag") == 0 || string.CompareOrdinal(xmlTextReader.Name, "DeletedTag") == 0) && xmlTextReader.NodeType == XmlNodeType.EndElement)
						{
							xmlTextReader.Read();
						}
					}
					MrmFaiFormatter.Tracer.TraceDebug<IExchangePrincipal>(0L, "Mailbox:{0}. Found no MTA or retention or DefaultArchive or Deleted tag in Config item.", mailboxOwner);
				}
				catch (XmlException ex2)
				{
					ex = ex2;
				}
				catch (ArgumentException ex3)
				{
					ex = ex3;
				}
				catch (FormatException ex4)
				{
					ex = ex4;
				}
				if (ex != null)
				{
					xmlStream.Position = 0L;
					string text = new StreamReader(xmlStream).ReadToEnd();
					MrmFaiFormatter.Tracer.TraceDebug<IExchangePrincipal, string, Exception>(0L, "Mailbox:{0}. Config item is corrupt. Config item: '{1}'. Exception: '{2}'", mailboxOwner, text, ex);
					Globals.ELCLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_CorruptTagConfigItem, null, new object[]
					{
						mailboxOwner,
						text,
						ex.ToString()
					});
				}
			}
			return dictionary;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x00031F98 File Offset: 0x00030198
		private static string[] ReadLocalizedStrings(XmlTextReader xmlReader, string elementToRead)
		{
			List<string> list = new List<string>();
			xmlReader.Read();
			while (string.CompareOrdinal(xmlReader.Name, elementToRead) == 0)
			{
				list.Add(xmlReader.ReadString());
				xmlReader.Read();
			}
			xmlReader.Read();
			return list.ToArray();
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00031FE4 File Offset: 0x000301E4
		private static void WriteLocalizedStrings(MultiValuedProperty<string> valuesToWrite, XmlWriter xmlWriter, string parentElement, string childElement)
		{
			xmlWriter.WriteStartElement(parentElement);
			foreach (string value in valuesToWrite)
			{
				xmlWriter.WriteElementString(childElement, value);
			}
			xmlWriter.WriteEndElement();
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00032040 File Offset: 0x00030240
		private static bool IsTagNode(XmlTextReader xmlTextReader)
		{
			return xmlTextReader.NodeType == XmlNodeType.Element && (string.CompareOrdinal(xmlTextReader.Name, "PolicyTag") == 0 || string.CompareOrdinal(xmlTextReader.Name, "ArchiveTag") == 0 || string.CompareOrdinal(xmlTextReader.Name, "DefaultArchiveTag") == 0 || string.CompareOrdinal(xmlTextReader.Name, "DeletedTag") == 0);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x000320A3 File Offset: 0x000302A3
		private static bool IsArchiveSyncNode(XmlTextReader xmlTextReader)
		{
			return xmlTextReader.NodeType == XmlNodeType.Element && string.CompareOrdinal(xmlTextReader.Name, "ArchiveSync") == 0;
		}

		// Token: 0x04000889 RID: 2185
		private const string UserConfiguration = "UserConfiguration";

		// Token: 0x0400088A RID: 2186
		private const string Info = "Info";

		// Token: 0x0400088B RID: 2187
		private const string Version = "version";

		// Token: 0x0400088C RID: 2188
		private const string VersionValue = "Exchange.14";

		// Token: 0x0400088D RID: 2189
		private const string Data = "Data";

		// Token: 0x0400088E RID: 2190
		private const string RetentionHoldTag = "RetentionHold";

		// Token: 0x0400088F RID: 2191
		private const string FullCrawlRequired = "FullCrawlRequired";

		// Token: 0x04000890 RID: 2192
		private const string ArchiveSync = "ArchiveSync";

		// Token: 0x04000891 RID: 2193
		private const string Enabled = "Enabled";

		// Token: 0x04000892 RID: 2194
		private const string RetentionComment = "RetentionComment";

		// Token: 0x04000893 RID: 2195
		private const string RetentionURL = "RetentionUrl";

		// Token: 0x04000894 RID: 2196
		private const string DeletedTag = "DeletedTag";

		// Token: 0x04000895 RID: 2197
		private const string DeletedTagGuid = "Guid";

		// Token: 0x04000896 RID: 2198
		private const string PolicyTag = "PolicyTag";

		// Token: 0x04000897 RID: 2199
		private const string ArchiveTag = "ArchiveTag";

		// Token: 0x04000898 RID: 2200
		private const string DefaultArchiveTag = "DefaultArchiveTag";

		// Token: 0x04000899 RID: 2201
		private const string Guid = "ObjectGuid";

		// Token: 0x0400089A RID: 2202
		private const string RetentionId = "Guid";

		// Token: 0x0400089B RID: 2203
		private const string ContentSettingsGuid = "Guid";

		// Token: 0x0400089C RID: 2204
		private const string Name = "Name";

		// Token: 0x0400089D RID: 2205
		private const string LocalizedNameElement = "LocalizedName";

		// Token: 0x0400089E RID: 2206
		private const string Comment = "Comment";

		// Token: 0x0400089F RID: 2207
		private const string LocalizedCommentElement = "LocalizedComment";

		// Token: 0x040008A0 RID: 2208
		private const string Type = "Type";

		// Token: 0x040008A1 RID: 2209
		private const string MustDisplayComment = "MustDisplayComment";

		// Token: 0x040008A2 RID: 2210
		private const string IsVisibleElement = "IsVisible";

		// Token: 0x040008A3 RID: 2211
		private const string OptedIntoElement = "OptedInto";

		// Token: 0x040008A4 RID: 2212
		private const string ContentSettingsElement = "ContentSettings";

		// Token: 0x040008A5 RID: 2213
		private const string ExpiryAgeLimit = "ExpiryAgeLimit";

		// Token: 0x040008A6 RID: 2214
		private const string RetentionAction = "RetentionAction";

		// Token: 0x040008A7 RID: 2215
		private const string MessageClass = "MessageClass";

		// Token: 0x040008A8 RID: 2216
		private const char Delimiter = ',';

		// Token: 0x040008A9 RID: 2217
		private static readonly Trace Tracer = ExTraceGlobals.TagProvisionerTracer;

		// Token: 0x040008AA RID: 2218
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;
	}
}
