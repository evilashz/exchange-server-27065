using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200063F RID: 1599
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PolicyTagList : Dictionary<Guid, PolicyTag>
	{
		// Token: 0x060041E1 RID: 16865 RVA: 0x00117DA0 File Offset: 0x00115FA0
		internal PolicyTagList()
		{
		}

		// Token: 0x060041E2 RID: 16866 RVA: 0x00117DA8 File Offset: 0x00115FA8
		private static string GetStringFromLocalizedStringPair(string cultureName, string localizedStringPair)
		{
			int num = localizedStringPair.IndexOf(":");
			if (num < 0 || num == localizedStringPair.Length - 1)
			{
				return null;
			}
			if (string.Compare(cultureName, localizedStringPair.Substring(0, num), StringComparison.OrdinalIgnoreCase) == 0)
			{
				return localizedStringPair.Substring(num + 1);
			}
			return null;
		}

		// Token: 0x060041E3 RID: 16867 RVA: 0x00117DF0 File Offset: 0x00115FF0
		internal static PolicyTagList GetPolicyTagListFromMailboxSession(RetentionActionType type, MailboxSession session)
		{
			StoreId defaultFolderId = session.GetDefaultFolderId(DefaultFolderType.Inbox);
			IReadableUserConfiguration readableUserConfiguration = null;
			try
			{
				readableUserConfiguration = session.UserConfigurationManager.GetReadOnlyFolderConfiguration("MRM", UserConfigurationTypes.Stream | UserConfigurationTypes.XML | UserConfigurationTypes.Dictionary, defaultFolderId);
				if (readableUserConfiguration != null)
				{
					using (Stream xmlStream = readableUserConfiguration.GetXmlStream())
					{
						string sessionCultureName = null;
						if (session.Capabilities.CanHaveCulture)
						{
							sessionCultureName = session.PreferedCulture.Name;
						}
						return PolicyTagList.GetPolicyTakListFromXmlStream(type, xmlStream, sessionCultureName);
					}
				}
			}
			catch (ObjectNotFoundException)
			{
			}
			catch (CorruptDataException)
			{
			}
			finally
			{
				if (readableUserConfiguration != null)
				{
					readableUserConfiguration.Dispose();
				}
			}
			return null;
		}

		// Token: 0x060041E4 RID: 16868 RVA: 0x00117EA4 File Offset: 0x001160A4
		internal static PolicyTagList GetPolicyTakListFromXmlStream(RetentionActionType type, Stream xmlStream, string sessionCultureName)
		{
			PolicyTagList policyTagList = new PolicyTagList();
			if (xmlStream.Length == 0L)
			{
				return policyTagList;
			}
			using (XmlTextReader xmlTextReader = SafeXmlFactory.CreateSafeXmlTextReader(xmlStream))
			{
				try
				{
					xmlTextReader.MoveToContent();
					while ((xmlTextReader.NodeType != XmlNodeType.Element || (string.CompareOrdinal(xmlTextReader.Name, "PolicyTag") != 0 && string.CompareOrdinal(xmlTextReader.Name, "ArchiveTag") != 0 && string.CompareOrdinal(xmlTextReader.Name, "DefaultArchiveTag") != 0)) && xmlTextReader.Read())
					{
					}
					for (;;)
					{
						bool flag = string.CompareOrdinal(xmlTextReader.Name, "PolicyTag") == 0;
						bool flag2 = string.CompareOrdinal(xmlTextReader.Name, "ArchiveTag") == 0 || string.CompareOrdinal(xmlTextReader.Name, "DefaultArchiveTag") == 0;
						if (!flag && !flag2)
						{
							break;
						}
						PolicyTag policyTag = new PolicyTag();
						policyTag.IsArchive = flag2;
						if (xmlTextReader.MoveToAttribute("Guid"))
						{
							policyTag.PolicyGuid = new Guid(xmlTextReader.Value);
						}
						if (xmlTextReader.MoveToAttribute("Name"))
						{
							policyTag.Name = xmlTextReader.Value;
						}
						if (xmlTextReader.MoveToAttribute("Comment"))
						{
							policyTag.Description = xmlTextReader.Value;
						}
						if (xmlTextReader.MoveToAttribute("Type"))
						{
							string value;
							switch (value = xmlTextReader.Value)
							{
							case "Calendar":
								policyTag.Type = ElcFolderType.Calendar;
								goto IL_341;
							case "Contacts":
								policyTag.Type = ElcFolderType.Contacts;
								goto IL_341;
							case "DeletedItems":
								policyTag.Type = ElcFolderType.DeletedItems;
								goto IL_341;
							case "Drafts":
								policyTag.Type = ElcFolderType.Drafts;
								goto IL_341;
							case "Inbox":
								policyTag.Type = ElcFolderType.Inbox;
								goto IL_341;
							case "JunkEmail":
								policyTag.Type = ElcFolderType.JunkEmail;
								goto IL_341;
							case "Journal":
								policyTag.Type = ElcFolderType.Journal;
								goto IL_341;
							case "Notes":
								policyTag.Type = ElcFolderType.Notes;
								goto IL_341;
							case "Outbox":
								policyTag.Type = ElcFolderType.Outbox;
								goto IL_341;
							case "SentItems":
								policyTag.Type = ElcFolderType.SentItems;
								goto IL_341;
							case "Tasks":
								policyTag.Type = ElcFolderType.Tasks;
								goto IL_341;
							case "All":
								policyTag.Type = ElcFolderType.All;
								goto IL_341;
							case "ManagedCustomFolder":
								policyTag.Type = ElcFolderType.ManagedCustomFolder;
								goto IL_341;
							case "RssSubscriptions":
								policyTag.Type = ElcFolderType.RssSubscriptions;
								goto IL_341;
							case "SyncIssues":
								policyTag.Type = ElcFolderType.SyncIssues;
								goto IL_341;
							case "ConversationHistory":
								policyTag.Type = ElcFolderType.ConversationHistory;
								goto IL_341;
							}
							policyTag.Type = ElcFolderType.Personal;
						}
						IL_341:
						if (xmlTextReader.MoveToAttribute("IsVisible"))
						{
							policyTag.IsVisible = bool.Parse(xmlTextReader.Value);
						}
						if (xmlTextReader.MoveToAttribute("OptedInto"))
						{
							policyTag.OptedInto = bool.Parse(xmlTextReader.Value);
						}
						while (string.CompareOrdinal(xmlTextReader.Name, "ContentSettings") != 0 && string.CompareOrdinal(xmlTextReader.Name, "PolicyTag") != 0 && string.CompareOrdinal(xmlTextReader.Name, "ArchiveTag") != 0)
						{
							if (string.CompareOrdinal(xmlTextReader.Name, "DefaultArchiveTag") == 0)
							{
								break;
							}
							if (!xmlTextReader.Read())
							{
								break;
							}
							if (!string.IsNullOrEmpty(sessionCultureName))
							{
								if (string.CompareOrdinal(xmlTextReader.Name, "LocalizedName") == 0)
								{
									xmlTextReader.Read();
									bool flag3 = false;
									while (string.CompareOrdinal(xmlTextReader.Name, "LocalizedName") != 0)
									{
										if (!flag3 && !string.IsNullOrEmpty(xmlTextReader.Value))
										{
											string stringFromLocalizedStringPair = PolicyTagList.GetStringFromLocalizedStringPair(sessionCultureName, xmlTextReader.Value);
											if (stringFromLocalizedStringPair != null)
											{
												policyTag.Name = stringFromLocalizedStringPair;
												flag3 = true;
											}
										}
										if (!xmlTextReader.Read())
										{
											break;
										}
									}
								}
								if (string.CompareOrdinal(xmlTextReader.Name, "LocalizedComment") == 0)
								{
									xmlTextReader.Read();
									bool flag4 = false;
									while (string.CompareOrdinal(xmlTextReader.Name, "LocalizedComment") != 0)
									{
										if (!flag4 && !string.IsNullOrEmpty(xmlTextReader.Value))
										{
											string stringFromLocalizedStringPair2 = PolicyTagList.GetStringFromLocalizedStringPair(sessionCultureName, xmlTextReader.Value);
											if (stringFromLocalizedStringPair2 != null)
											{
												policyTag.Description = stringFromLocalizedStringPair2;
												flag4 = true;
											}
										}
										if (!xmlTextReader.Read())
										{
											break;
										}
									}
								}
							}
						}
						while (string.CompareOrdinal(xmlTextReader.Name, "ContentSettings") == 0)
						{
							if (xmlTextReader.MoveToAttribute("ExpiryAgeLimit"))
							{
								policyTag.TimeSpanForRetention = EnhancedTimeSpan.FromDays(double.Parse(xmlTextReader.Value));
							}
							if (xmlTextReader.MoveToAttribute("RetentionAction"))
							{
								policyTag.RetentionAction = (RetentionActionType)Enum.Parse(typeof(RetentionActionType), xmlTextReader.Value, true);
							}
							xmlTextReader.Read();
						}
						if (type == (RetentionActionType)0 || (type == RetentionActionType.MoveToArchive && flag2) || (type != (RetentionActionType)0 && type != RetentionActionType.MoveToArchive && flag))
						{
							policyTagList[policyTag.PolicyGuid] = policyTag;
						}
						if ((string.CompareOrdinal(xmlTextReader.Name, "PolicyTag") == 0 || string.CompareOrdinal(xmlTextReader.Name, "ArchiveTag") == 0 || string.CompareOrdinal(xmlTextReader.Name, "DefaultArchiveTag") == 0) && xmlTextReader.NodeType == XmlNodeType.EndElement)
						{
							xmlTextReader.Read();
						}
					}
				}
				catch (XmlException ex)
				{
				}
				catch (ArgumentException ex2)
				{
				}
				catch (FormatException ex3)
				{
				}
			}
			return policyTagList;
		}

		// Token: 0x04002448 RID: 9288
		internal const string ElcTagConfigurationXSOClass = "MRM";

		// Token: 0x04002449 RID: 9289
		internal const UserConfigurationTypes ElcConfigurationTypes = UserConfigurationTypes.Stream | UserConfigurationTypes.XML | UserConfigurationTypes.Dictionary;

		// Token: 0x0400244A RID: 9290
		private const string Info = "Info";

		// Token: 0x0400244B RID: 9291
		private const string Version = "version";

		// Token: 0x0400244C RID: 9292
		private const string VersionValue = "Exchange.14";

		// Token: 0x0400244D RID: 9293
		private const string Data = "Data";

		// Token: 0x0400244E RID: 9294
		private const string RetentionHoldTag = "RetentionHold";

		// Token: 0x0400244F RID: 9295
		private const string Enabled = "Enabled";

		// Token: 0x04002450 RID: 9296
		private const string RetentionComment = "RetentionComment";

		// Token: 0x04002451 RID: 9297
		private const string RetentionURL = "RetentionUrl";

		// Token: 0x04002452 RID: 9298
		private const string PolicyTag = "PolicyTag";

		// Token: 0x04002453 RID: 9299
		private const string ArchiveTag = "ArchiveTag";

		// Token: 0x04002454 RID: 9300
		private const string DefaultArchiveTag = "DefaultArchiveTag";

		// Token: 0x04002455 RID: 9301
		private const string Guid = "Guid";

		// Token: 0x04002456 RID: 9302
		private const string Name = "Name";

		// Token: 0x04002457 RID: 9303
		private const string LocalizedNameElement = "LocalizedName";

		// Token: 0x04002458 RID: 9304
		private const string Comment = "Comment";

		// Token: 0x04002459 RID: 9305
		private const string LocalizedCommentElement = "LocalizedComment";

		// Token: 0x0400245A RID: 9306
		private const string Type = "Type";

		// Token: 0x0400245B RID: 9307
		private const string MustDisplayComment = "MustDisplayComment";

		// Token: 0x0400245C RID: 9308
		private const string IsVisibleElement = "IsVisible";

		// Token: 0x0400245D RID: 9309
		private const string OptedIntoElement = "OptedInto";

		// Token: 0x0400245E RID: 9310
		private const string ContentSettingsElement = "ContentSettings";

		// Token: 0x0400245F RID: 9311
		private const string ExpiryAgeLimit = "ExpiryAgeLimit";

		// Token: 0x04002460 RID: 9312
		private const string MessageClass = "MessageClass";

		// Token: 0x04002461 RID: 9313
		private const string RetentionAction = "RetentionAction";

		// Token: 0x04002462 RID: 9314
		private const char Delimiter = ',';
	}
}
