using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.Mapi;
using Microsoft.Exchange.Protocols.MAPI.ExtensionMethods;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.LazyIndexing;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000051 RID: 81
	public sealed class MapiLogon : MapiPropBagBase
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000B073 File Offset: 0x00009273
		public static MapiLogon.GenerateQuotaReportDelegate GenerateQuotaReport
		{
			get
			{
				return MapiLogon.hookableGenerateQuotaReport.Value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000B07F File Offset: 0x0000927F
		public bool IsOwner
		{
			get
			{
				base.ThrowIfNotValid(null);
				return this.isOwner;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000B08E File Offset: 0x0000928E
		public bool IsPrimaryOwner
		{
			get
			{
				base.ThrowIfNotValid(null);
				return this.isPrimaryOwner;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x0000B09D File Offset: 0x0000929D
		public CrucialFolderId FidC
		{
			get
			{
				base.ThrowIfNotValid(null);
				return this.fidc;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x0000B0AC File Offset: 0x000092AC
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000B0B4 File Offset: 0x000092B4
		public Mailbox StoreMailbox
		{
			get
			{
				return this.mapiMailbox.StoreMailbox;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000B0C1 File Offset: 0x000092C1
		public MapiMailbox MapiMailbox
		{
			get
			{
				if (base.IsDisposed)
				{
					throw new ObjectDisposedException("mapilogon");
				}
				return this.mapiMailbox;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000B0DC File Offset: 0x000092DC
		public Guid MailboxGuid
		{
			get
			{
				base.ThrowIfNotValid(null);
				return this.mapiMailbox.MailboxGuid;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000B0F0 File Offset: 0x000092F0
		public bool IsSystemServiceLogon
		{
			get
			{
				base.ThrowIfNotValid(null);
				return this.session.UsingAdminPrivilege;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000B104 File Offset: 0x00009304
		public AddressInfo EffectiveOwnerAddressInfo
		{
			get
			{
				base.ThrowIfNotValid(null);
				if (this.IsOwner || (this.hasAdminRights && this.logonUser.UserSid == null))
				{
					return this.mailboxOwner;
				}
				return this.logonUser;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000B13D File Offset: 0x0000933D
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x0000B14C File Offset: 0x0000934C
		public AddressInfo LoggedOnUserAddressInfo
		{
			get
			{
				base.ThrowIfNotValid(null);
				return this.logonUser;
			}
			internal set
			{
				base.ThrowIfNotValid(null);
				this.logonUser = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000B15C File Offset: 0x0000935C
		// (set) Token: 0x060001AA RID: 426 RVA: 0x0000B16B File Offset: 0x0000936B
		public AddressInfo MailboxOwnerAddressInfo
		{
			get
			{
				base.ThrowIfNotValid(null);
				return this.mailboxOwner;
			}
			internal set
			{
				base.ThrowIfNotValid(null);
				this.mailboxOwner = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000B17B File Offset: 0x0000937B
		// (set) Token: 0x060001AC RID: 428 RVA: 0x0000B194 File Offset: 0x00009394
		public MailboxInfo MailboxInfo
		{
			get
			{
				base.ThrowIfNotValid(null);
				return this.mapiMailbox.StoreMailbox.MailboxInfo;
			}
			internal set
			{
				base.ThrowIfNotValid(null);
				this.mapiMailbox.StoreMailbox.MailboxInfo = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000B1AE File Offset: 0x000093AE
		// (set) Token: 0x060001AE RID: 430 RVA: 0x0000B1BD File Offset: 0x000093BD
		public DatabaseInfo DatabaseInfo
		{
			get
			{
				base.ThrowIfNotValid(null);
				return this.databaseInfo;
			}
			internal set
			{
				base.ThrowIfNotValid(null);
				this.databaseInfo = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000B1CD File Offset: 0x000093CD
		public bool MapiTransportProvider
		{
			get
			{
				return this.mapiTransportProvider;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000B1D5 File Offset: 0x000093D5
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x0000B1DD File Offset: 0x000093DD
		public bool IsNormalMessageDelivery { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000B1E6 File Offset: 0x000093E6
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x0000B1EE File Offset: 0x000093EE
		public bool IsQuotaMessageDelivery { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000B1F7 File Offset: 0x000093F7
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x0000B1FF File Offset: 0x000093FF
		public bool IsReportMessageDelivery { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000B208 File Offset: 0x00009408
		public OpenStoreFlags OpenStoreFlags
		{
			get
			{
				return this.openStoreFlags;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000B210 File Offset: 0x00009410
		public LinkedListNode<MapiLogon> NodeOfMailboxStateLogonList
		{
			get
			{
				return this.nodeOfMailboxStateLogonList;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000B218 File Offset: 0x00009418
		public MapiLogon.ClientActivityTracker ClientActivity
		{
			get
			{
				return this.clientActivityTracker;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000B220 File Offset: 0x00009420
		public bool UnifiedLogon
		{
			get
			{
				return this.unifiedLogon;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000B228 File Offset: 0x00009428
		public Queue<MapiBase> DeferedReleaseROPsForTest
		{
			get
			{
				return this.deferedReleaseROPs;
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000B230 File Offset: 0x00009430
		internal static IDisposable SetUpdateLastLogonLogoffTimeTestHook(Func<bool> action)
		{
			return MapiLogon.updateLastLogonLogoffTimeTestHook.SetTestHook(action);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000B23D File Offset: 0x0000943D
		internal static IDisposable SetGenerateQuotaReportHook(MapiLogon.GenerateQuotaReportDelegate hook)
		{
			return MapiLogon.hookableGenerateQuotaReport.SetTestHook(hook);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000B24C File Offset: 0x0000944C
		internal static ErrorCode InternalGenerateQuotaReport(MapiContext context, MapiLogon mapiLogon, Folder folder, QuotaType quotaType, QuotaInfo quotaInfo, IList<byte[]> recipients, long containerSize)
		{
			ErrorCode result;
			using (context.GrantMailboxFullRights())
			{
				if (quotaType >= QuotaType.MaxQuotaType)
				{
					result = ErrorCode.NoError;
				}
				else
				{
					bool flag = mapiLogon.MailboxInfo.IsArchiveMailbox && quotaType == QuotaType.StorageOverQuotaLimit;
					if (flag)
					{
						result = ErrorCode.NoError;
					}
					else
					{
						if (mapiLogon.MapiMailbox.IsPublicFolderMailbox && (quotaType == QuotaType.StorageWarningLimit || quotaType == QuotaType.StorageOverQuotaLimit || quotaType == QuotaType.StorageShutoff))
						{
							MapiLogon.PublishEventNotificationQuotaReport(context, mapiLogon);
						}
						string text = null;
						if (folder != null)
						{
							text = Folder.GetFolderPathName(context, folder.Mailbox, folder.GetId(context), '\\');
							if (text.StartsWith("\\IPM_SUBTREE", StringComparison.OrdinalIgnoreCase))
							{
								text = text.Substring("\\IPM_SUBTREE".Length);
							}
						}
						Properties[] array = new Properties[recipients.Count];
						string[] array2 = new string[recipients.Count];
						for (int i = 0; i < recipients.Count; i++)
						{
							Properties properties = new Properties(3);
							string text2 = null;
							string value = null;
							string text3 = null;
							properties.Add(PropTag.Recipient.RecipientType, 1);
							properties.Add(PropTag.Recipient.EntryId, recipients[i]);
							Eidt eidt;
							MapiAPIFlags mapiAPIFlags;
							if (AddressBookEID.IsAddressBookEntryId(context, recipients[i], out eidt, out text2))
							{
								array2[i] = text2;
								properties.Add(PropTag.Recipient.AddressType, "EX");
							}
							else if (AddressBookEID.IsOneOffEntryId(context, recipients[i], out mapiAPIFlags, ref value, ref text2, ref text3))
							{
								array2[i] = text2;
								properties.Add(PropTag.Recipient.AddressType, value);
							}
							else
							{
								array2[i] = Convert.ToBase64String(recipients[i]);
							}
							array[i] = properties;
						}
						StringBuilder stringBuilder = new StringBuilder(array2.Length * 100);
						stringBuilder.AppendLine();
						for (int j = 0; j < array2.Length; j++)
						{
							stringBuilder.AppendLine(array2[j]);
						}
						stringBuilder.AppendLine();
						switch (quotaType)
						{
						case QuotaType.StorageWarningLimit:
						case QuotaType.StorageShutoff:
						case QuotaType.DumpsterWarningLimit:
						case QuotaType.DumpsterShutoff:
						{
							ExEventLog.EventTuple tuple;
							if (mapiLogon.MailboxInfo.IsArchiveMailbox)
							{
								if (MapiLogon.ArchiveQuotaEvents.TryGetValue(quotaType, out tuple))
								{
									Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(tuple, new object[]
									{
										mapiLogon.MailboxGuid,
										mapiLogon.MapiMailbox.Database.MdbGuid
									});
								}
							}
							else if (mapiLogon.MapiMailbox.IsPublicFolderMailbox)
							{
								if (text != null)
								{
									if (MapiLogon.PublicFolderQuotaEvents.TryGetValue(quotaType, out tuple))
									{
										Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(tuple, new object[]
										{
											stringBuilder.ToString(),
											text
										});
									}
								}
								else if (MapiLogon.MailboxQuotaEvents.TryGetValue(quotaType, out tuple))
								{
									Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(tuple, new object[]
									{
										mapiLogon.MailboxGuid,
										mapiLogon.MapiMailbox.Database.MdbGuid
									});
								}
							}
							else if (MapiLogon.MailboxQuotaEvents.TryGetValue(quotaType, out tuple))
							{
								Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(tuple, new object[]
								{
									mapiLogon.MailboxGuid,
									mapiLogon.MapiMailbox.Database.MdbGuid
								});
							}
							break;
						}
						case QuotaType.StorageOverQuotaLimit:
						{
							ExEventLog.EventTuple tuple;
							if (MapiLogon.MailboxQuotaEvents.TryGetValue(quotaType, out tuple))
							{
								Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(tuple, new object[]
								{
									mapiLogon.MailboxGuid,
									mapiLogon.MapiMailbox.Database.MdbGuid
								});
							}
							break;
						}
						case QuotaType.MailboxMessagesPerFolderCountWarningQuota:
						case QuotaType.MailboxMessagesPerFolderCountReceiveQuota:
						case QuotaType.DumpsterMessagesPerFolderCountWarningQuota:
						case QuotaType.DumpsterMessagesPerFolderCountReceiveQuota:
						case QuotaType.FolderHierarchyChildrenCountWarningQuota:
						case QuotaType.FolderHierarchyChildrenCountReceiveQuota:
						case QuotaType.FolderHierarchyDepthWarningQuota:
						case QuotaType.FolderHierarchyDepthReceiveQuota:
						{
							ExEventLog.EventTuple tuple;
							if (MapiLogon.MailboxShapeQuotaEvents.TryGetValue(quotaType, out tuple))
							{
								Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(tuple, new object[]
								{
									mapiLogon.MailboxGuid,
									mapiLogon.MapiMailbox.Database.MdbGuid,
									text
								});
							}
							break;
						}
						case QuotaType.FoldersCountWarningQuota:
						case QuotaType.FoldersCountReceiveQuota:
						{
							ExEventLog.EventTuple tuple;
							if (MapiLogon.MailboxShapeQuotaEvents.TryGetValue(quotaType, out tuple))
							{
								Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(tuple, new object[]
								{
									mapiLogon.MailboxGuid,
									mapiLogon.MapiMailbox.Database.MdbGuid
								});
							}
							break;
						}
						default:
							return ErrorCode.CreateInvalidParameter((LID)34364U);
						}
						if (MapiMailboxShape.IsDumpster(quotaType) || (folder != null && !folder.IsIpmFolder(context)))
						{
							result = ErrorCode.NoError;
						}
						else
						{
							MapiMessage mapiMessage = null;
							ErrorCode first = MapiMailbox.PrepareReportMessage(context, mapiLogon, array, out mapiMessage);
							using (mapiMessage)
							{
								if (first != ErrorCode.NoError)
								{
									result = first.Propagate((LID)29784U);
								}
								else
								{
									mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.QuotaType, (int)quotaType);
									switch (quotaType)
									{
									case QuotaType.StorageWarningLimit:
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.MessageClass, "IPM.Note.StorageQuotaWarning.Warning");
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.StorageQuotaLimit, (int)quotaInfo.MailboxWarningQuota.KB);
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.ExcessStorageUsed, (int)(containerSize / 1024L - quotaInfo.MailboxWarningQuota.KB));
										break;
									case QuotaType.StorageOverQuotaLimit:
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.MessageClass, "IPM.Note.StorageQuotaWarning.Send");
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.StorageQuotaLimit, (int)quotaInfo.MailboxSendQuota.KB);
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.ExcessStorageUsed, (int)(containerSize / 1024L - quotaInfo.MailboxSendQuota.KB));
										break;
									case QuotaType.StorageShutoff:
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.MessageClass, "IPM.Note.StorageQuotaWarning.SendReceive");
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.StorageQuotaLimit, (int)quotaInfo.MailboxShutoffQuota.KB);
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.ExcessStorageUsed, (int)(containerSize / 1024L - quotaInfo.MailboxShutoffQuota.KB));
										break;
									case QuotaType.MailboxMessagesPerFolderCountWarningQuota:
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.MessageClass, "IPM.Note.StorageQuotaWarning.Warning");
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.StorageQuotaLimit, (int)quotaInfo.MailboxMessagesPerFolderCountWarningQuota.Value);
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.ExcessStorageUsed, (int)(containerSize - quotaInfo.MailboxMessagesPerFolderCountWarningQuota.Value));
										break;
									case QuotaType.MailboxMessagesPerFolderCountReceiveQuota:
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.MessageClass, "IPM.Note.StorageQuotaWarning.SendReceive");
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.StorageQuotaLimit, (int)quotaInfo.MailboxMessagesPerFolderCountReceiveQuota.Value);
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.ExcessStorageUsed, (int)(containerSize - quotaInfo.MailboxMessagesPerFolderCountReceiveQuota.Value));
										break;
									case QuotaType.FolderHierarchyChildrenCountWarningQuota:
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.MessageClass, "IPM.Note.StorageQuotaWarning.Warning");
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.StorageQuotaLimit, (int)quotaInfo.FolderHierarchyChildrenCountWarningQuota.Value);
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.ExcessStorageUsed, (int)(containerSize - quotaInfo.FolderHierarchyChildrenCountWarningQuota.Value));
										break;
									case QuotaType.FolderHierarchyChildrenCountReceiveQuota:
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.MessageClass, "IPM.Note.StorageQuotaWarning.SendReceive");
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.StorageQuotaLimit, (int)quotaInfo.FolderHierarchyChildrenCountReceiveQuota.Value);
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.ExcessStorageUsed, (int)(containerSize - quotaInfo.FolderHierarchyChildrenCountReceiveQuota.Value));
										break;
									case QuotaType.FolderHierarchyDepthWarningQuota:
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.MessageClass, "IPM.Note.StorageQuotaWarning.Warning");
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.StorageQuotaLimit, (int)quotaInfo.FolderHierarchyDepthWarningQuota.Value);
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.ExcessStorageUsed, (int)(containerSize - quotaInfo.FolderHierarchyDepthWarningQuota.Value));
										break;
									case QuotaType.FolderHierarchyDepthReceiveQuota:
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.MessageClass, "IPM.Note.StorageQuotaWarning.SendReceive");
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.StorageQuotaLimit, (int)quotaInfo.FolderHierarchyDepthReceiveQuota.Value);
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.ExcessStorageUsed, (int)(containerSize - quotaInfo.FolderHierarchyDepthReceiveQuota.Value));
										break;
									case QuotaType.FoldersCountWarningQuota:
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.MessageClass, "IPM.Note.StorageQuotaWarning.Warning");
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.StorageQuotaLimit, (int)quotaInfo.FoldersCountWarningQuota.Value);
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.ExcessStorageUsed, (int)(containerSize - quotaInfo.FoldersCountWarningQuota.Value));
										break;
									case QuotaType.FoldersCountReceiveQuota:
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.MessageClass, "IPM.Note.StorageQuotaWarning.SendReceive");
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.StorageQuotaLimit, (int)quotaInfo.FoldersCountReceiveQuota.Value);
										mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.ExcessStorageUsed, (int)(containerSize - quotaInfo.FoldersCountReceiveQuota.Value));
										break;
									}
									switch (quotaType)
									{
									case QuotaType.StorageWarningLimit:
									case QuotaType.StorageOverQuotaLimit:
									case QuotaType.StorageShutoff:
										if (!quotaInfo.MailboxSendQuota.IsUnlimited)
										{
											mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.ProhibitSendQuota, (int)quotaInfo.MailboxSendQuota.KB);
										}
										if (!quotaInfo.MailboxShutoffQuota.IsUnlimited)
										{
											mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.ProhibitReceiveQuota, (int)quotaInfo.MailboxShutoffQuota.KB);
										}
										break;
									case QuotaType.MailboxMessagesPerFolderCountWarningQuota:
									case QuotaType.MailboxMessagesPerFolderCountReceiveQuota:
										if (!quotaInfo.MailboxMessagesPerFolderCountReceiveQuota.IsUnlimited)
										{
											mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.ProhibitReceiveQuota, (int)quotaInfo.MailboxMessagesPerFolderCountReceiveQuota.Value);
										}
										break;
									case QuotaType.FolderHierarchyChildrenCountWarningQuota:
									case QuotaType.FolderHierarchyChildrenCountReceiveQuota:
										if (!quotaInfo.FolderHierarchyChildrenCountReceiveQuota.IsUnlimited)
										{
											mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.ProhibitReceiveQuota, (int)quotaInfo.FolderHierarchyChildrenCountReceiveQuota.Value);
										}
										break;
									case QuotaType.FolderHierarchyDepthWarningQuota:
									case QuotaType.FolderHierarchyDepthReceiveQuota:
										if (!quotaInfo.FolderHierarchyDepthReceiveQuota.IsUnlimited)
										{
											mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.ProhibitReceiveQuota, (int)quotaInfo.FolderHierarchyDepthReceiveQuota.Value);
										}
										break;
									case QuotaType.FoldersCountWarningQuota:
									case QuotaType.FoldersCountReceiveQuota:
										if (!quotaInfo.FoldersCountReceiveQuota.IsUnlimited)
										{
											mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.ProhibitReceiveQuota, (int)quotaInfo.FoldersCountReceiveQuota.Value);
										}
										break;
									}
									mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.IsPublicFolderQuotaMessage, mapiLogon.MapiMailbox.IsPublicFolderMailbox);
									mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.PrimaryMailboxOverQuota, !mapiLogon.MailboxInfo.IsArchiveMailbox);
									string value2 = string.Empty;
									if (folder != null)
									{
										value2 = text + " .";
									}
									mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.ServerGeneratingQuotaMsg, value2);
									mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.MessageLocaleId, mapiLogon.MapiMailbox.Lcid);
									mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.Priority, 1);
									mapiMessage.StoreMessage.SetProperty(context, PropTag.Message.Importance, 2);
									result = mapiMessage.SubmitMessage(context, RopFlags.Mapi0, null, null, SubmitMessageRightsCheckFlags.SendAsRights).Propagate((LID)29792U);
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000BEDC File Offset: 0x0000A0DC
		private static byte[] GetMailboxGlobalCounters(Context context, Mailbox mailbox)
		{
			byte[] array = null;
			int num = 0;
			ulong nextIdCounterAndReserveRange = mailbox.GetNextIdCounterAndReserveRange(context, 1U);
			ulong nextCnCounterAndReserveRange = mailbox.GetNextCnCounterAndReserveRange(context, 1U);
			Guid localIdGuid = mailbox.GetLocalIdGuid(context);
			num += SerializedValue.SerializeInt64((long)nextIdCounterAndReserveRange, array, num);
			num += SerializedValue.SerializeInt64((long)nextCnCounterAndReserveRange, array, num);
			num += SerializedValue.SerializeGuid(localIdGuid, array, num);
			array = new byte[num];
			num = 0;
			num += SerializedValue.SerializeInt64((long)nextIdCounterAndReserveRange, array, num);
			num += SerializedValue.SerializeInt64((long)nextCnCounterAndReserveRange, array, num);
			num += SerializedValue.SerializeGuid(localIdGuid, array, num);
			return array;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000BF58 File Offset: 0x0000A158
		private static void FinalizeMailboxSignaturePreservingMailboxMove(Context context, Mailbox mailbox, object objValue)
		{
			if (ExTraceGlobals.MailboxSignatureTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.MailboxSignatureTracer.TraceDebug<string, int>(36552L, "Database {0} : Mailbox {1} : Finalize mailbox signature preserving mailbox move.", mailbox.Database.MdbName, mailbox.MailboxNumber);
			}
			if (objValue == null)
			{
				throw new InvalidParameterException((LID)54344U, "No mailbox global counters specified.");
			}
			if (!mailbox.GetMRSPreservingMailboxSignature(context))
			{
				throw new CorruptDataException((LID)47008U, "The mailbox is not in preserving mailbox signature state as far as MRS is concerned.");
			}
			if (!mailbox.GetPreservingMailboxSignature(context))
			{
				throw new CorruptDataException((LID)42056U, "The mailbox is not in preserving mailbox signature state.");
			}
			int num = 0;
			byte[] buffer = objValue as byte[];
			ulong num2 = (ulong)SerializedValue.ParseInt64(buffer, ref num);
			ulong num3 = (ulong)SerializedValue.ParseInt64(buffer, ref num);
			Guid localIdGuidSource = SerializedValue.ParseGuid(buffer, ref num);
			if (ExTraceGlobals.MailboxSignatureTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ulong num4;
				ulong num5;
				mailbox.GetGlobalCounters(context, out num4, out num5);
				ExTraceGlobals.MailboxSignatureTracer.TraceDebug(1010L, "Database {0} : Mailbox {1} : Has current Id: {2}, Cn: {3}, next Id: {4}, Cn: {5}.", new object[]
				{
					mailbox.Database.MdbName,
					mailbox.MailboxNumber,
					num4,
					num5,
					num2,
					num3
				});
			}
			mailbox.VerifyAndUpdateGlobalCounters(context, localIdGuidSource, num2, num3);
			mailbox.SetPreservingMailboxSignature(context, false);
			mailbox.SetProperty(context, PropTag.Mailbox.ReservedIdCounterRangeUpperLimit, null);
			mailbox.SetProperty(context, PropTag.Mailbox.ReservedCnCounterRangeUpperLimit, null);
			if (ExTraceGlobals.MailboxSignatureTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.MailboxSignatureTracer.TraceDebug<string, int>(63176L, "Database {0} : Mailbox {1} : Finalize mailbox signature preserving mailbox move done.", mailbox.Database.MdbName, mailbox.MailboxNumber);
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000C0F4 File Offset: 0x0000A2F4
		private static void PublishEventNotificationQuotaReport(MapiContext context, MapiLogon mapiLogon)
		{
			EventNotificationItem eventNotificationItem = new EventNotificationItem(ExchangeComponent.PublicFolders.Name, "PublicFolderMailboxQuota", string.Empty, ResultSeverityLevel.Error);
			eventNotificationItem.CustomProperties["TenantHint"] = mapiLogon.StoreMailbox.SharedState.TenantHint.ToString();
			eventNotificationItem.CustomProperties["MailboxDisplayName"] = mapiLogon.MailboxInfo.OwnerDisplayName;
			eventNotificationItem.CustomProperties["MailboxGuid"] = mapiLogon.MailboxGuid.ToString();
			eventNotificationItem.StateAttribute1 = mapiLogon.MailboxInfo.OwnerDisplayName;
			string stateAttribute = string.Empty;
			TenantHint tenantHint = mapiLogon.StoreMailbox.SharedState.TenantHint;
			if (!tenantHint.IsEmpty)
			{
				stateAttribute = HexConverter.ByteArrayToHexString(tenantHint.TenantHintBlob);
			}
			eventNotificationItem.StateAttribute2 = stateAttribute;
			try
			{
				eventNotificationItem.Publish(false);
			}
			catch (UnauthorizedAccessException ex)
			{
				context.OnExceptionCatch(ex);
				if (ExTraceGlobals.QuotaTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					ExTraceGlobals.QuotaTracer.TraceError<UnauthorizedAccessException>(52320L, "MapiLogon::InternalGenerateQuotaReport: UnauthorizedAccessException {0}", ex);
				}
			}
			catch (EventLogNotFoundException ex2)
			{
				context.OnExceptionCatch(ex2);
				if (ExTraceGlobals.QuotaTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					ExTraceGlobals.QuotaTracer.TraceError<EventLogNotFoundException>(46176L, "MapiLogon::InternalGenerateQuotaReport: EventLogNotFoundException {0}", ex2);
				}
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000C254 File Offset: 0x0000A454
		internal static IDisposable SetControlAdminRightsTestHook(Func<bool> action)
		{
			return MapiLogon.controlAdminRights.SetTestHook(action);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000C261 File Offset: 0x0000A461
		internal static IDisposable SetControlTransportRightsTestHook(Func<bool> action)
		{
			return MapiLogon.controlTransportRights.SetTestHook(action);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000C270 File Offset: 0x0000A470
		internal static bool CheckOwnerRights(ClientSecurityContext securityContext, MailboxInfo mailboxInfo, DatabaseInfo databaseInfo, OpenStoreFlags openStoreFlags, out bool isPrimaryOwnerLogon)
		{
			bool flag = false;
			Trace createLogonTracer = ExTraceGlobals.CreateLogonTracer;
			isPrimaryOwnerLogon = false;
			SecurityIdentifier securityIdentifier = (mailboxInfo.MasterAccountSid != null) ? mailboxInfo.MasterAccountSid : mailboxInfo.UserSid;
			if (securityIdentifier == null)
			{
				if (createLogonTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					createLogonTracer.TraceDebug(0L, "Invalid mailbox owner SID - NULL; not granting owner rights for this logon");
				}
				DiagnosticContext.TraceLocation((LID)58615U);
			}
			else if (securityContext.UserSid == securityIdentifier)
			{
				isPrimaryOwnerLogon = true;
				flag = true;
				if (createLogonTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					createLogonTracer.TraceDebug(0L, "owner rights and primary logon granted based on SID equality");
				}
			}
			else if ((openStoreFlags & OpenStoreFlags.TakeOwnership) == OpenStoreFlags.None)
			{
				if (createLogonTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					createLogonTracer.TraceDebug(0L, "openStoreFlags.TakeOwnership was not passed in so we can't do an ACL based check for owner rights");
				}
				DiagnosticContext.TraceLocation((LID)54831U);
			}
			else
			{
				if (createLogonTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					createLogonTracer.TraceDebug(0L, "Checking Mailbox Owner rights on the mailbox; enable SecurityMailboxOwnerAccess for info on this access check");
				}
				flag = SecurityHelper.CheckMailboxOwnerRights(securityContext, mailboxInfo, databaseInfo);
			}
			if (createLogonTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(256);
				stringBuilder.Append("ACCESS ");
				stringBuilder.Append(flag ? "ALLOWED" : "DENIED");
				stringBuilder.Append(". securityContext.UserSid:[");
				SecurityHelper.AppendToString(stringBuilder, securityContext.UserSid);
				stringBuilder.Append("]. mailbox guid:[");
				stringBuilder.Append(mailboxInfo.MailboxGuid);
				stringBuilder.Append("].");
				createLogonTracer.TraceDebug(0L, stringBuilder.ToString());
			}
			if (!flag)
			{
				DiagnosticContext.TraceLocation((LID)47631U);
			}
			return flag;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000C3FA File Offset: 0x0000A5FA
		internal bool IsPublicFolderSystem
		{
			get
			{
				return this.mapiMailbox.StoreMailbox.CurrentOperationContext.ClientType == ClientType.PublicFolderSystem;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000C415 File Offset: 0x0000A615
		internal bool AllowsDuplicateFolderNames
		{
			get
			{
				return this.IsPublicFolderSystem || this.IsMoveDestination;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000C427 File Offset: 0x0000A627
		internal bool IsMoveUser
		{
			get
			{
				base.ThrowIfNotValid(null);
				return InTransitInfo.IsMoveUser(this.GetInTransitStatus());
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000C43B File Offset: 0x0000A63B
		internal bool IsMoveDestination
		{
			get
			{
				base.ThrowIfNotValid(null);
				return InTransitInfo.IsMoveDestination(this.GetInTransitStatus());
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000C44F File Offset: 0x0000A64F
		internal bool IsMoveSource
		{
			get
			{
				base.ThrowIfNotValid(null);
				return InTransitInfo.IsMoveSource(this.GetInTransitStatus());
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000C463 File Offset: 0x0000A663
		internal bool IsForPublicFolderMove
		{
			get
			{
				base.ThrowIfNotValid(null);
				return InTransitInfo.IsForPublicFolderMove(this.GetInTransitStatus());
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000C477 File Offset: 0x0000A677
		internal MapiLogon.PendingNotificationQueue PendingNotifications
		{
			get
			{
				base.ThrowIfNotValid(null);
				return this.pendingNotifications;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000C486 File Offset: 0x0000A686
		public override MapiSession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000C48E File Offset: 0x0000A68E
		public Guid MailboxInstanceGuid
		{
			get
			{
				return this.mapiMailbox.MailboxInstanceGuid;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000C49B File Offset: 0x0000A69B
		public int PendingNotificationsCount
		{
			get
			{
				base.ThrowIfNotValid(null);
				if (this.pendingNotifications != null)
				{
					return this.pendingNotifications.Count;
				}
				return 0;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000C4B9 File Offset: 0x0000A6B9
		public CodePage CodePage
		{
			get
			{
				return this.Session.CodePage;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000C4C6 File Offset: 0x0000A6C6
		public Encoding Encoding
		{
			get
			{
				return this.Session.Encoding;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000C4D3 File Offset: 0x0000A6D3
		public bool NoSpooler
		{
			get
			{
				return (this.OpenStoreFlags & OpenStoreFlags.NoMail) != OpenStoreFlags.None;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000C4E7 File Offset: 0x0000A6E7
		public bool ExchangeTransportServiceRights
		{
			get
			{
				return FaultInjection.Replace<bool>(MapiLogon.controlTransportRights, this.Session != null && this.Session.UsingTransportPrivilege);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000C509 File Offset: 0x0000A709
		public bool SystemRights
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000C50C File Offset: 0x0000A70C
		public bool SpoolerRights
		{
			get
			{
				return this.isSpooler;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000C514 File Offset: 0x0000A714
		public bool SendAsRights
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000C517 File Offset: 0x0000A717
		public object ClientIdentity
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000C51A File Offset: 0x0000A71A
		public bool AdminRights
		{
			get
			{
				return FaultInjection.Replace<bool>(MapiLogon.controlAdminRights, this.hasAdminRights);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000C52C File Offset: 0x0000A72C
		public InTransitStatus InTransitStatus
		{
			get
			{
				return this.GetInTransitStatus();
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000C534 File Offset: 0x0000A734
		public MapiLogon() : base(MapiObjectType.Logon)
		{
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000C54F File Offset: 0x0000A74F
		internal static void Initialize()
		{
			if (MapiLogon.updateLastLogonTimeActionSlot == -1)
			{
				MapiLogon.updateLastLogonTimeActionSlot = LazyMailboxActionList.AllocateSlot();
			}
			if (MapiLogon.updateLastLogoffTimeActionSlot == -1)
			{
				MapiLogon.updateLastLogoffTimeActionSlot = LazyMailboxActionList.AllocateSlot();
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000C578 File Offset: 0x0000A778
		public void EstablishQuotaInfo()
		{
			switch (this.MailboxInfo.QuotaStyle)
			{
			case QuotaStyle.UseDatabaseDefault:
				this.StoreMailbox.QuotaInfo.MergeQuotaFromAD(this.DatabaseInfo.QuotaInfo);
				break;
			case QuotaStyle.UseSpecificValues:
				this.StoreMailbox.QuotaInfo.MergeQuotaFromAD(this.MailboxInfo.QuotaInfo);
				break;
			default:
				this.StoreMailbox.QuotaInfo.MergeQuotaFromAD(QuotaInfo.Unlimited);
				break;
			}
			if (this.MailboxInfo.Type == MailboxInfo.MailboxType.PublicFolderSecondary)
			{
				this.StoreMailbox.QuotaInfo.ResetFolderRelatedQuotaToUnlimited();
			}
			this.StoreMailbox.QuotaStyle = this.MailboxInfo.QuotaStyle;
			this.StoreMailbox.MaxItemSize = this.MailboxInfo.MaxMessageSize;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000C63C File Offset: 0x0000A83C
		public void PerformLogonQuotaCheck(MapiContext context)
		{
			bool flag = ExTraceGlobals.QuotaTracer.IsTraceEnabled(TraceType.DebugTrace);
			if (!this.ExchangeTransportServiceRights && ((!this.SystemRights && !this.AdminRights) || this.MailboxInfo.IsArchiveMailbox))
			{
				DateTime utcNow = this.MapiMailbox.StoreMailbox.UtcNow;
				DateTime lastQuotaCheckTime = this.MapiMailbox.StoreMailbox.GetLastQuotaCheckTime(context);
				if (utcNow - lastQuotaCheckTime > MapiMailboxShape.QuotaWarningCheckInterval)
				{
					if (flag)
					{
						ExTraceGlobals.QuotaTracer.TraceDebug<string>(29924L, "Logon requires a quota check. Last check done on {0}", lastQuotaCheckTime.ToString());
					}
					MapiMailboxShape.PerformLogonQuotaCheck(context, this);
					this.MapiMailbox.StoreMailbox.SetLastQuotaCheckTime(context, utcNow);
					return;
				}
				if (flag)
				{
					ExTraceGlobals.QuotaTracer.TraceDebug<string>(29940L, "Logon does not require a quota check. Last check done on {0}", lastQuotaCheckTime.ToString());
				}
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000C71C File Offset: 0x0000A91C
		public void PerformTransportLogonQuotaCheck(MapiContext context)
		{
			bool flag = ExTraceGlobals.QuotaTracer.IsTraceEnabled(TraceType.DebugTrace);
			if (this.ExchangeTransportServiceRights)
			{
				if (this.IsReportMessageDelivery || this.IsNormalMessageDelivery)
				{
					if (flag)
					{
						ExTraceGlobals.QuotaTracer.TraceDebug<string>(29936L, "Transport logon qualifies for a quota check. IsReportMessageDelivery = {0}", this.IsReportMessageDelivery.ToString());
					}
					Quota.Enforce((LID)36108U, context, this.StoreMailbox, QuotaType.StorageShutoff, this.IsReportMessageDelivery);
					return;
				}
				if (this.IsQuotaMessageDelivery && flag)
				{
					ExTraceGlobals.QuotaTracer.TraceDebug(29928L, "Transport logon skipping quota check for quota delivery.");
				}
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000C7B4 File Offset: 0x0000A9B4
		internal bool GetAllPerUserLongTermIds(MapiContext context, StoreLongTermId startId, Func<PerUserData, bool> tryCollectData)
		{
			byte[] array = startId.ToBytes(false);
			foreach (PerUser perUser in PerUser.ForeignEntries(context, this.StoreMailbox, array))
			{
				if (!ValueHelper.ArraysEqual<byte>(array, perUser.FolderIdBytes))
				{
					PerUserData arg = new PerUserData(StoreLongTermId.Parse(perUser.FolderIdBytes, false), perUser.Guid);
					if (!tryCollectData(arg))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000C848 File Offset: 0x0000AA48
		internal List<StoreLongTermId> GetPerUserLongTermIds(MapiContext context, Guid databaseGuid)
		{
			IEnumerable<PerUser> enumerable = PerUser.ForeignEntries(context, this.StoreMailbox);
			if (enumerable == null)
			{
				return new List<StoreLongTermId>();
			}
			List<StoreLongTermId> list = new List<StoreLongTermId>(100);
			foreach (PerUser perUser in enumerable)
			{
				if (perUser.Guid == databaseGuid)
				{
					StoreLongTermId item = StoreLongTermId.Parse(perUser.FolderIdBytes, false);
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000C8D0 File Offset: 0x0000AAD0
		internal ErrorCode ReadPerUserInformation(MapiContext context, StoreLongTermId folderId, uint dataOffset, ushort maxDataSize, out bool hasFinished, out byte[] buffer)
		{
			if (maxDataSize == 0 || maxDataSize > 4096)
			{
				maxDataSize = 4096;
			}
			if (!this.perUserTransferFolderId.Equals(folderId) || this.perUserTransferData == null || this.perUserTransferData.Position != (long)((ulong)dataOffset))
			{
				if (this.perUserTransferData != null)
				{
					this.perUserTransferData.Dispose();
					this.perUserTransferData = null;
				}
				this.perUserTransferFolderId = folderId;
				PerUser perUser;
				if (this.MailboxInfo.Type == MailboxInfo.MailboxType.Private)
				{
					perUser = PerUser.LoadForeign(context, this.StoreMailbox, folderId.ToBytes(false));
				}
				else
				{
					ExchangeId folderId2 = ExchangeId.Create(context, this.MapiMailbox.StoreMailbox.ReplidGuidMap, folderId.Guid, ExchangeIdHelpers.GlobcntFromByteArray(folderId.GlobCount, 0U));
					MapiFolder.GhostedFolderCheck(context, this, folderId2, (LID)54361U);
					perUser = PerUser.LoadResident(context, this.StoreMailbox, this.LoggedOnUserAddressInfo.ObjectId, folderId2);
				}
				if (perUser == null)
				{
					this.perUserTransferData = new MemoryStream(0);
				}
				else
				{
					using (LockManager.Lock(perUser, LockManager.LockType.PerUserShared, context.Diagnostics))
					{
						this.perUserTransferData = new MemoryStream(perUser.CNSetBytes);
					}
				}
			}
			if ((ulong)dataOffset != (ulong)this.perUserTransferData.Position)
			{
				hasFinished = false;
				buffer = null;
				return ErrorCode.CreateCallFailed((LID)30360U);
			}
			maxDataSize = (ushort)Math.Min((long)((ulong)maxDataSize), this.perUserTransferData.Length - this.perUserTransferData.Position);
			buffer = new byte[(int)maxDataSize];
			this.perUserTransferData.Read(buffer, 0, (int)maxDataSize);
			hasFinished = (this.perUserTransferData.Length == this.perUserTransferData.Position);
			if (hasFinished)
			{
				this.perUserTransferData.Dispose();
				this.perUserTransferData = null;
				this.perUserTransferFolderId = StoreLongTermId.Null;
			}
			return ErrorCode.NoError;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000CAAC File Offset: 0x0000ACAC
		internal ErrorCode WritePerUserInformation(MapiContext context, StoreLongTermId folderId, uint dataOffset, bool hasFinished, byte[] buffer, Guid? replicaGuid)
		{
			if (dataOffset == 0U && this.MailboxInfo.Type == MailboxInfo.MailboxType.Private)
			{
				this.perUserTransferReplicaGuid = replicaGuid.Value;
			}
			if (!this.perUserTransferFolderId.Equals(folderId) || this.perUserTransferData == null || this.perUserTransferData.Position != (long)((ulong)dataOffset))
			{
				if (this.perUserTransferData != null)
				{
					this.perUserTransferData.Dispose();
				}
				this.perUserTransferFolderId = folderId;
				this.perUserTransferData = new MemoryStream(buffer.Length);
			}
			if ((ulong)dataOffset != (ulong)this.perUserTransferData.Position)
			{
				return ErrorCode.CreateCallFailed((LID)30080U);
			}
			if ((ulong)dataOffset + (ulong)((long)buffer.Length) > 52428800UL)
			{
				return ErrorCode.CreateNotEnoughMemory((LID)51612U);
			}
			try
			{
				this.perUserTransferData.Write(buffer, 0, buffer.Length);
			}
			catch (IOException exception)
			{
				context.OnExceptionCatch(exception);
				return ErrorCode.CreateNotEnoughMemory((LID)52007U);
			}
			catch (System.NotSupportedException exception2)
			{
				context.OnExceptionCatch(exception2);
				return ErrorCode.CreateNotEnoughMemory((LID)61852U);
			}
			if (hasFinished)
			{
				bool flag = false;
				byte[] array = this.perUserTransferFolderId.ToBytes(false);
				PerUser perUser;
				if (this.MailboxInfo.Type == MailboxInfo.MailboxType.Private)
				{
					byte[] array2 = this.perUserTransferData.ToArray();
					perUser = PerUser.LoadForeign(context, this.StoreMailbox, array);
					if (perUser != null && perUser.Guid == this.perUserTransferReplicaGuid && ValueHelper.ArraysEqual<byte>(array2, perUser.CNSetBytes))
					{
						flag = true;
					}
					else
					{
						perUser = PerUser.CreateForeign(this.perUserTransferReplicaGuid, array, array2);
					}
				}
				else
				{
					ExchangeId exchangeId = ExchangeId.CreateFrom22ByteArray(context, this.StoreMailbox.ReplidGuidMap, array);
					using (MapiFolder mapiFolder = MapiFolder.OpenFolder(context, this, exchangeId))
					{
						if (mapiFolder == null)
						{
							return ErrorCode.CreateObjectDeleted((LID)30132U);
						}
						mapiFolder.GhostedFolderCheck(context, (LID)64889U);
					}
					IdSet idSet;
					try
					{
						idSet = IdSet.ThrowableParse(context, this.perUserTransferData);
					}
					finally
					{
						this.perUserTransferData = null;
					}
					perUser = PerUser.LoadResident(context, this.StoreMailbox, this.LoggedOnUserAddressInfo.ObjectId, exchangeId);
					if (perUser != null)
					{
						using (LockManager.Lock(perUser, LockManager.LockType.PerUserShared))
						{
							flag = idSet.Equals(perUser.CNSet);
						}
					}
					if (!flag)
					{
						perUser = PerUser.CreateResident(context, this.StoreMailbox, this.LoggedOnUserAddressInfo.ObjectId, exchangeId, idSet);
						if (!this.IsMoveDestination)
						{
							Folder folder = Folder.OpenFolder(context, this.StoreMailbox, exchangeId);
							folder.RepopulateSearchFoldersForPerUserUpload(context);
						}
					}
				}
				if (!flag)
				{
					perUser.SaveWithCacheLock(context, this.StoreMailbox);
				}
				if (this.perUserTransferData != null)
				{
					this.perUserTransferData.Dispose();
					this.perUserTransferData = null;
				}
				this.perUserTransferFolderId = StoreLongTermId.Null;
				this.perUserTransferReplicaGuid = Guid.Empty;
			}
			return ErrorCode.NoError;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000CDB4 File Offset: 0x0000AFB4
		internal bool IsMailboxHardDeleted(MapiContext context)
		{
			return this.MapiMailbox.StoreMailbox.SharedState.IsHardDeleted;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000CDD8 File Offset: 0x0000AFD8
		protected override bool TryGetPropertyImp(MapiContext context, ushort propId, out StorePropTag actualPropTag, out object propValue)
		{
			if (propId <= 26155)
			{
				if (propId <= 15804)
				{
					if (propId == 13320)
					{
						actualPropTag = PropTag.Mailbox.MailboxPartitionMailboxGuids;
						if (this.StoreMailbox.SharedState.UnifiedState != null)
						{
							propValue = MapiMailbox.GetPartitionMailboxGuids(context, this.StoreMailbox.SharedState.UnifiedState.UnifiedMailboxGuid);
						}
						else
						{
							propValue = null;
						}
						return propValue != null;
					}
					if (propId == 13334)
					{
						propValue = this.GetLocalDirectoryEntryId(context);
						actualPropTag = PropTag.Mailbox.LocalDirectoryEntryID;
						return true;
					}
					if (propId != 15804)
					{
						goto IL_374;
					}
					propValue = (int)this.MailboxInfo.Type;
					actualPropTag = PropTag.Mailbox.MailboxType;
					return true;
				}
				else if (propId <= 16383)
				{
					if (propId != 16373)
					{
						if (propId != 16383)
						{
							goto IL_374;
						}
						propValue = this.MailboxInfo.RulesQuota;
						actualPropTag = PropTag.Mailbox.RulesSize;
						return true;
					}
				}
				else
				{
					switch (propId)
					{
					case 26137:
						if (this.StoreMailbox.SharedState.MailboxTypeDetail == MailboxInfo.MailboxTypeDetail.TeamMailbox)
						{
							propValue = this.EffectiveOwnerAddressInfo.UserEntryId();
							actualPropTag = PropTag.Mailbox.UserEntryId;
						}
						else
						{
							propValue = (this.IsPrimaryOwner ? this.mailboxOwner.UserEntryId() : this.LoggedOnUserAddressInfo.UserEntryId());
							actualPropTag = PropTag.Mailbox.UserEntryId;
						}
						return true;
					case 26138:
						if (this.StoreMailbox.SharedState.MailboxTypeDetail == MailboxInfo.MailboxTypeDetail.TeamMailbox)
						{
							propValue = this.EffectiveOwnerAddressInfo.DisplayName;
							actualPropTag = PropTag.Mailbox.UserName;
						}
						else
						{
							propValue = (this.IsOwner ? this.mailboxOwner.DisplayName : this.LoggedOnUserAddressInfo.DisplayName);
							actualPropTag = PropTag.Mailbox.UserName;
						}
						return true;
					case 26139:
						propValue = this.mailboxOwner.UserEntryId();
						actualPropTag = PropTag.Mailbox.MailboxOwnerEntryId;
						return true;
					case 26140:
						propValue = this.mailboxOwner.DisplayName;
						actualPropTag = PropTag.Mailbox.MailboxOwnerName;
						return true;
					default:
						if (propId != 26155)
						{
							goto IL_374;
						}
						propValue = new byte[256];
						actualPropTag = PropTag.Mailbox.TestLineSpeed;
						return true;
					}
				}
			}
			else if (propId <= 26266)
			{
				if (propId == 26168)
				{
					propValue = this.StoreMailbox.ReplidGuidMap.GetSerializedMap(context);
					actualPropTag = PropTag.Mailbox.SerializedReplidGuidMap;
					return propValue != null;
				}
				switch (propId)
				{
				case 26218:
				case 26221:
				case 26222:
					break;
				case 26219:
				case 26220:
					goto IL_374;
				default:
					if (propId != 26266)
					{
						goto IL_374;
					}
					propValue = this.StoreMailbox.GetCurrentSchemaVersion(context).Value;
					actualPropTag = PropTag.Mailbox.MailboxDatabaseVersion;
					goto IL_374;
				}
			}
			else if (propId <= 26474)
			{
				if (propId == 26468)
				{
					propValue = this.FidC.FidRoot.ToLong();
					actualPropTag = PropTag.Mailbox.RootFid;
					return true;
				}
				if (propId != 26474)
				{
					goto IL_374;
				}
				propValue = this.MapiMailbox.MdbGuid.ToByteArray();
				actualPropTag = PropTag.Mailbox.MdbDSGuid;
				return true;
			}
			else
			{
				if (propId == 26483)
				{
					propValue = this.mailboxOwner.DisplayName;
					actualPropTag = PropTag.Mailbox.MailboxOwnerDisplayName;
					return true;
				}
				if (propId != 26669)
				{
					goto IL_374;
				}
			}
			return this.TryGetQuotaProperty(context, propId, out actualPropTag, out propValue);
			IL_374:
			return base.TryGetPropertyImp(context, propId, out actualPropTag, out propValue);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000D164 File Offset: 0x0000B364
		protected override object GetPropertyValueImp(MapiContext context, StorePropTag propTag)
		{
			ushort propId = propTag.PropId;
			if (propId <= 26168)
			{
				if (propId <= 16373)
				{
					if (propId <= 13334)
					{
						if (propId != 13320)
						{
							if (propId != 13334)
							{
								goto IL_48A;
							}
							return (propTag != PropTag.Mailbox.LocalDirectoryEntryID) ? null : this.GetLocalDirectoryEntryId(context);
						}
						else
						{
							if (propTag == PropTag.Mailbox.MailboxPartitionMailboxGuids && this.StoreMailbox.SharedState.UnifiedState != null)
							{
								return MapiMailbox.GetPartitionMailboxGuids(context, this.StoreMailbox.SharedState.UnifiedState.UnifiedMailboxGuid);
							}
							return null;
						}
					}
					else
					{
						if (propId == 15804)
						{
							return (propTag != PropTag.Mailbox.MailboxType) ? null : ((int)this.MailboxInfo.Type);
						}
						if (propId != 16373)
						{
							goto IL_48A;
						}
					}
				}
				else if (propId <= 26140)
				{
					if (propId == 16383)
					{
						return (propTag != PropTag.Mailbox.RulesSize) ? null : this.MailboxInfo.RulesQuota;
					}
					switch (propId)
					{
					case 26137:
						if (this.StoreMailbox.SharedState.MailboxTypeDetail == MailboxInfo.MailboxTypeDetail.TeamMailbox)
						{
							return (propTag != PropTag.Mailbox.UserEntryId) ? null : this.EffectiveOwnerAddressInfo.UserEntryId();
						}
						return (propTag != PropTag.Mailbox.UserEntryId) ? null : (this.IsPrimaryOwner ? this.mailboxOwner.UserEntryId() : this.LoggedOnUserAddressInfo.UserEntryId());
					case 26138:
						if (this.StoreMailbox.SharedState.MailboxTypeDetail == MailboxInfo.MailboxTypeDetail.TeamMailbox)
						{
							return (propTag != PropTag.Mailbox.UserName) ? null : this.EffectiveOwnerAddressInfo.DisplayName;
						}
						return (propTag != PropTag.Mailbox.UserName) ? null : (this.IsOwner ? this.mailboxOwner.DisplayName : this.LoggedOnUserAddressInfo.DisplayName);
					case 26139:
						return (propTag != PropTag.Mailbox.MailboxOwnerEntryId) ? null : this.mailboxOwner.UserEntryId();
					case 26140:
						return (propTag != PropTag.Mailbox.MailboxOwnerName) ? null : this.mailboxOwner.DisplayName;
					default:
						goto IL_48A;
					}
				}
				else
				{
					if (propId == 26155)
					{
						return (propTag != PropTag.Mailbox.TestLineSpeed) ? null : new byte[256];
					}
					if (propId != 26168)
					{
						goto IL_48A;
					}
					return (propTag != PropTag.Mailbox.SerializedReplidGuidMap) ? null : this.StoreMailbox.ReplidGuidMap.GetSerializedMap(context);
				}
			}
			else if (propId <= 26465)
			{
				if (propId <= 26266)
				{
					switch (propId)
					{
					case 26218:
					case 26221:
					case 26222:
						break;
					case 26219:
					case 26220:
						goto IL_48A;
					default:
						if (propId != 26266)
						{
							goto IL_48A;
						}
						return (propTag != PropTag.Mailbox.MailboxDatabaseVersion) ? null : this.StoreMailbox.GetCurrentSchemaVersion(context).Value;
					}
				}
				else
				{
					switch (propId)
					{
					case 26280:
						return (propTag != PropTag.Mailbox.PreservingMailboxSignature) ? null : this.StoreMailbox.GetPreservingMailboxSignature(context);
					case 26281:
						return (propTag != PropTag.Mailbox.MRSPreservingMailboxSignature) ? null : this.StoreMailbox.GetMRSPreservingMailboxSignature(context);
					default:
						if (propId != 26465)
						{
							goto IL_48A;
						}
						return (propTag != PropTag.Mailbox.LocalIdNext) ? null : MapiLogon.GetMailboxGlobalCounters(context, this.StoreMailbox);
					}
				}
			}
			else if (propId <= 26474)
			{
				if (propId == 26468)
				{
					return (propTag != PropTag.Mailbox.RootFid) ? null : this.FidC.FidRoot.ToLong();
				}
				if (propId != 26474)
				{
					goto IL_48A;
				}
				return (propTag != PropTag.Mailbox.MdbDSGuid) ? null : this.MapiMailbox.MdbGuid.ToByteArray();
			}
			else
			{
				if (propId == 26483)
				{
					return (propTag != PropTag.Mailbox.MailboxOwnerDisplayName) ? null : this.mailboxOwner.DisplayName;
				}
				if (propId != 26669)
				{
					goto IL_48A;
				}
			}
			StorePropTag storePropTag;
			object propertyValueImp;
			this.TryGetQuotaProperty(context, propTag.PropId, out storePropTag, out propertyValueImp);
			if (propTag.PropTag != storePropTag.PropTag)
			{
				return null;
			}
			return propertyValueImp;
			IL_48A:
			propertyValueImp = base.GetPropertyValueImp(context, propTag);
			return propertyValueImp;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000D605 File Offset: 0x0000B805
		public override void OnRelease(MapiContext context)
		{
			this.UpdateLastLogoffTime(context);
			this.StoreMailbox.Save(context);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000D61A File Offset: 0x0000B81A
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiLogon>(this);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000D624 File Offset: 0x0000B824
		protected override void InternalDispose(bool isCalledFromDispose)
		{
			base.InternalDispose(isCalledFromDispose);
			if (isCalledFromDispose)
			{
				if (this.perUserTransferData != null)
				{
					this.perUserTransferData.Dispose();
					this.perUserTransferData = null;
				}
				if (this.index != -1)
				{
					this.session.RemoveLogon(this.index);
				}
				if (this.nodeOfMailboxStateLogonList != null)
				{
					MailboxLogonList.RemoveLogon(this);
					this.nodeOfMailboxStateLogonList = null;
				}
				if (this.mapiMailbox != null)
				{
					if (this.InTransitStatus != InTransitStatus.NotInTransit)
					{
						Trace inTransitTransitionsTracer = ExTraceGlobals.InTransitTransitionsTracer;
						bool flag = inTransitTransitionsTracer.IsTraceEnabled(TraceType.DebugTrace);
						if (flag)
						{
							inTransitTransitionsTracer.TraceDebug<Guid, InTransitStatus>(0L, "Mailbox {0}, abandoned logon is leaving an in-transit state {1}", this.mapiMailbox.MailboxGuid, this.InTransitStatus);
						}
						this.RemoveInTransitState();
					}
					if (this.spoolerLockList != null && this.spoolerLockList.Count > 0)
					{
						HashSet<ExchangeId> hashSet = Microsoft.Exchange.Protocols.MAPI.Globals.GetSpoolerLockList(this.StoreMailbox);
						if (hashSet != null)
						{
							foreach (ExchangeId item in this.spoolerLockList)
							{
								hashSet.Remove(item);
							}
							if (hashSet.Count == 0)
							{
								hashSet = null;
							}
							Microsoft.Exchange.Protocols.MAPI.Globals.SetSpoolerLockList(this.StoreMailbox, hashSet);
						}
					}
					if (this.session != null)
					{
						this.session.ReleaseMapiMailbox(this.mapiMailbox);
					}
					else
					{
						this.mapiMailbox.Dispose();
					}
				}
			}
			this.index = -1;
			this.mapiMailbox = null;
			this.fidc = null;
			this.pendingNotifications = null;
			this.session = null;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000D79C File Offset: 0x0000B99C
		internal void AddPendingNotification(NotificationEvent nev, MapiBase mapiBase, uint notifyObjectHsot)
		{
			if (this.pendingNotifications == null)
			{
				this.pendingNotifications = new MapiLogon.PendingNotificationQueue();
			}
			this.pendingNotifications.Enqueue(nev, notifyObjectHsot);
			if (ExTraceGlobals.NotificationTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				string value;
				string value2;
				string value3;
				string value4;
				this.session.GetCurrentActivityDetails(out value, out value2, out value3, out value4);
				stringBuilder.Append(mapiBase.GetType().Name);
				stringBuilder.Append(" has queued a notification: hsot:[");
				stringBuilder.Append(notifyObjectHsot);
				stringBuilder.Append("] session:[");
				stringBuilder.Append(this.session.SessionId);
				stringBuilder.Append("] logon:[");
				stringBuilder.Append(this.Index);
				stringBuilder.Append("] data:[");
				nev.AppendToString(stringBuilder);
				stringBuilder.Append("] activityId:[");
				stringBuilder.Append(value);
				stringBuilder.Append("] protocol:[");
				stringBuilder.Append(value2);
				stringBuilder.Append("] component:[");
				stringBuilder.Append(value3);
				stringBuilder.Append("] action:[");
				stringBuilder.Append(value4);
				stringBuilder.Append("]");
				ExTraceGlobals.NotificationTracer.TraceDebug(0L, stringBuilder.ToString());
			}
			this.session.NotificationPending();
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000D8E3 File Offset: 0x0000BAE3
		internal void FlushStaleTableModifiedNotifications(uint viewObjectHsot)
		{
			if (this.pendingNotifications != null)
			{
				this.pendingNotifications.FlushStaleTableModifiedNotifications(viewObjectHsot);
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000D8F9 File Offset: 0x0000BAF9
		internal bool IsThereTableChangedNotification(uint viewObjectHsot)
		{
			return this.pendingNotifications != null && this.pendingNotifications.IsThereTableChangedNotification(viewObjectHsot);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000D914 File Offset: 0x0000BB14
		private bool TryGetQuotaProperty(MapiContext context, ushort propId, out StorePropTag actualPropTag, out object propValue)
		{
			UnlimitedBytes unlimitedBytes;
			if (propId != 26221)
			{
				if (propId != 26669)
				{
					QuotaInfo quotaInfo;
					switch (this.MailboxInfo.QuotaStyle)
					{
					case QuotaStyle.UseDatabaseDefault:
						quotaInfo = this.DatabaseInfo.QuotaInfo;
						break;
					case QuotaStyle.UseSpecificValues:
						quotaInfo = this.MailboxInfo.QuotaInfo;
						break;
					case QuotaStyle.NoQuota:
						goto IL_10B;
					default:
						goto IL_10B;
					}
					if (propId != 16373)
					{
						if (propId == 26218)
						{
							unlimitedBytes = quotaInfo.MailboxShutoffQuota;
							goto IL_DD;
						}
						if (propId == 26222)
						{
							unlimitedBytes = quotaInfo.MailboxSendQuota;
							if (unlimitedBytes.IsUnlimited)
							{
								unlimitedBytes = quotaInfo.MailboxShutoffQuota;
								goto IL_DD;
							}
							if (!quotaInfo.MailboxShutoffQuota.IsUnlimited && unlimitedBytes > quotaInfo.MailboxShutoffQuota)
							{
								unlimitedBytes = quotaInfo.MailboxShutoffQuota;
								goto IL_DD;
							}
							goto IL_DD;
						}
					}
					unlimitedBytes = quotaInfo.MailboxWarningQuota;
				}
				else
				{
					unlimitedBytes = this.MailboxInfo.MaxMessageSize;
				}
			}
			else
			{
				unlimitedBytes = this.MailboxInfo.MaxSendSize;
			}
			IL_DD:
			if (!unlimitedBytes.IsUnlimited)
			{
				actualPropTag = this.StoreMailbox.GetStorePropTag(context, propId, PropertyType.Int32, Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Mailbox);
				propValue = (int)unlimitedBytes.KB;
				return true;
			}
			IL_10B:
			actualPropTag = this.StoreMailbox.GetStorePropTag(context, propId, PropertyType.Error, Microsoft.Exchange.Server.Storage.PropTags.ObjectType.Mailbox);
			propValue = null;
			return false;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000DAD0 File Offset: 0x0000BCD0
		public void ConfigureLogon(MapiContext context, MapiSession session, AddressInfo addressInfoMailbox, MailboxInfo mailboxInfo, DatabaseInfo databaseInfo, ref MapiMailbox mapiMailbox, OpenStoreFlags openStoreFlags, bool unifiedLogon, int index)
		{
			MapiLogon.<>c__DisplayClass1 CS$<>8__locals1 = new MapiLogon.<>c__DisplayClass1();
			CS$<>8__locals1.session = session;
			CS$<>8__locals1.index = index;
			CS$<>8__locals1.<>4__this = this;
			Trace createLogonTracer = ExTraceGlobals.CreateLogonTracer;
			if (createLogonTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				createLogonTracer.TraceDebug(0L, "LOGON START");
			}
			this.IsNormalMessageDelivery = ((openStoreFlags & OpenStoreFlags.DeliverNormalMessage) == OpenStoreFlags.DeliverNormalMessage);
			this.IsQuotaMessageDelivery = ((openStoreFlags & OpenStoreFlags.DeliverQuotaMessage) == OpenStoreFlags.DeliverQuotaMessage);
			this.IsReportMessageDelivery = ((openStoreFlags & OpenStoreFlags.DeliverSpecialMessage) == OpenStoreFlags.DeliverSpecialMessage);
			mapiMailbox.StoreMailbox.MailboxInfo = mailboxInfo;
			this.databaseInfo = databaseInfo;
			this.openStoreFlags = openStoreFlags;
			this.unifiedLogon = unifiedLogon;
			this.constructionTime = mapiMailbox.StoreMailbox.UtcNow;
			base.Logon = this;
			if (CS$<>8__locals1.session != null)
			{
				if (CS$<>8__locals1.index < 0 || CS$<>8__locals1.session.GetLogon(CS$<>8__locals1.index) != null)
				{
					if (createLogonTracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						createLogonTracer.TraceError<int>(0L, "Invalid index passed in for the logon: {0}", CS$<>8__locals1.index);
					}
					using (context.CriticalBlock((LID)52044U, CriticalBlockScope.MailboxSession))
					{
						throw new ExExceptionInvalidParameter((LID)37959U, "Invalid index passed in for the logon");
					}
				}
				bool flag = false;
				bool flag2 = false;
				bool flag3 = CS$<>8__locals1.session.UsingTransportPrivilege;
				if (flag3)
				{
					if (createLogonTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						createLogonTracer.TraceDebug(0L, "Owner rights granted to service/system logon.");
					}
				}
				else if (OpenStoreFlags.UseAdminPrivilege == (openStoreFlags & OpenStoreFlags.UseAdminPrivilege))
				{
					if (createLogonTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						createLogonTracer.TraceDebug(0L, "Admin rights requested; running access check for admin rights.");
					}
					flag = SecurityHelper.CheckAdministrativeRights(CS$<>8__locals1.session.CurrentSecurityContext, databaseInfo.NTSecurityDescriptor);
				}
				else
				{
					if (createLogonTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						createLogonTracer.TraceDebug(0L, "running access check for owner rights.");
					}
					flag3 = MapiLogon.CheckOwnerRights(CS$<>8__locals1.session.CurrentSecurityContext, mailboxInfo, databaseInfo, openStoreFlags, out flag2);
				}
				if (createLogonTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					StringBuilder stringBuilder = new StringBuilder(100);
					stringBuilder.Append("session owned by userDn:[");
					stringBuilder.Append(CS$<>8__locals1.session.UserDN);
					stringBuilder.Append("] is logging on to mailbox :[");
					stringBuilder.Append(mapiMailbox.MailboxGuid);
					stringBuilder.Append("] With Admin Rights?[");
					stringBuilder.Append(CS$<>8__locals1.session.UsingAdminPrivilege);
					stringBuilder.Append("] With Transport Rights?[");
					stringBuilder.Append(CS$<>8__locals1.session.UsingTransportPrivilege);
					stringBuilder.Append("] With DelegatedAuth Rights?[");
					stringBuilder.Append(CS$<>8__locals1.session.UsingDelegatedAuth);
					stringBuilder.Append("] As(admin/owner/delegate)?[");
					stringBuilder.Append(flag ? "ADMIN]" : (flag3 ? "OWNER]" : "DELEGATE]"));
					createLogonTracer.TraceDebug(0L, stringBuilder.ToString());
				}
				this.session = CS$<>8__locals1.session;
				this.isOwner = flag3;
				this.isPrimaryOwner = flag2;
				this.hasAdminRights = flag;
				this.logonUser = CS$<>8__locals1.session.AddressInfoUser;
			}
			this.mailboxOwner = addressInfoMailbox;
			this.fidc = mapiMailbox.GetFIDC(context);
			CS$<>8__locals1.localMapiMailbox = mapiMailbox;
			context.SystemCriticalOperation(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<ConfigureLogon>b__0)));
			mapiMailbox = null;
			if (CS$<>8__locals1.session != null)
			{
				if (this.CannotLogonToInTransitMailbox(context))
				{
					if (createLogonTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						createLogonTracer.TraceDebug(0L, "Mailbox is in transit. Failing Logon!");
					}
					throw new ExExceptionMailboxInTransit((LID)42055U, "Mailbox is in transit.");
				}
				if (this.IsMailboxHardDeleted(context))
				{
					if (createLogonTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						createLogonTracer.TraceDebug(0L, "Mailbox is hard deleted. Failing Logon!");
					}
					throw new ExExceptionObjectDeleted((LID)58439U, "Mailbox is hard deleted");
				}
				if (this.session.LogonCount == 1)
				{
					this.session.IncrementSessionCount(MapiObjectTrackingScope.Service | MapiObjectTrackingScope.User);
				}
			}
			this.accessCheckState = new AccessCheckState(context, null);
			base.IsValid = true;
			this.EstablishQuotaInfo();
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000DEC8 File Offset: 0x0000C0C8
		public NotificationEvent GetNotificationEvent(out uint hsot)
		{
			base.ThrowIfNotValid(null);
			if (this.pendingNotifications == null)
			{
				hsot = 0U;
				return null;
			}
			return this.pendingNotifications.Dequeue(out hsot);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000DEEA File Offset: 0x0000C0EA
		public Guid GetGuidFromReplid(MapiContext context, ushort replid)
		{
			base.ThrowIfNotValid(null);
			return this.mapiMailbox.GetGuidFromReplid(context, replid);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000DF00 File Offset: 0x0000C100
		public ushort GetReplidFromGuid(MapiContext context, Guid guid)
		{
			base.ThrowIfNotValid(null);
			return this.mapiMailbox.GetReplidFromGuid(context, guid);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000DF16 File Offset: 0x0000C116
		public void SetReceiveFolder(MapiContext context, string messageClass, ExchangeId folderId)
		{
			base.ThrowIfNotValid(null);
			this.mapiMailbox.SetReceiveFolder(context, messageClass, folderId);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000DF2D File Offset: 0x0000C12D
		public ReceiveFolder GetReceiveFolder(MapiContext context, string messageClass)
		{
			base.ThrowIfNotValid(null);
			return this.mapiMailbox.GetReceiveFolder(context, messageClass);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000DF43 File Offset: 0x0000C143
		public IList<ReceiveFolder> GetReceiveFolders(MapiContext context)
		{
			base.ThrowIfNotValid(null);
			return this.mapiMailbox.GetReceiveFolders(context);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000DFF4 File Offset: 0x0000C1F4
		public void UpdateLastLogonTime(MapiContext context)
		{
			if (!this.IsUserLogon(context) || context.Database.IsReadOnly)
			{
				return;
			}
			DateTime now = this.StoreMailbox.UtcNow;
			LazyMailboxActionList.SetMailboxAction(MapiLogon.updateLastLogonTimeActionSlot, this.StoreMailbox.SharedState, delegate(Context operationContext, Mailbox storeMailbox)
			{
				object propertyValue = storeMailbox.GetPropertyValue(operationContext, PropTag.Mailbox.LastLogoffTime);
				if (propertyValue != null)
				{
					storeMailbox.SetProperty(operationContext, PropTag.Mailbox.LastLogoffTime, null);
				}
				object propertyValue2 = storeMailbox.GetPropertyValue(operationContext, PropTag.Mailbox.LastLogonTime);
				if (propertyValue2 == null || ((DateTime)propertyValue2).AddMinutes(1.0) < now || (MapiLogon.updateLastLogonLogoffTimeTestHook.Value != null && MapiLogon.updateLastLogonLogoffTimeTestHook.Value()))
				{
					storeMailbox.SetProperty(operationContext, PropTag.Mailbox.LastLogonTime, now);
				}
			});
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000E0D0 File Offset: 0x0000C2D0
		public void UpdateLastLogoffTime(MapiContext context)
		{
			if (!this.IsUserLogon(context) || context.Database.IsReadOnly)
			{
				return;
			}
			DateTime now = this.StoreMailbox.UtcNow;
			LazyMailboxActionList.SetMailboxAction(MapiLogon.updateLastLogoffTimeActionSlot, this.StoreMailbox.SharedState, delegate(Context operationContext, Mailbox storeMailbox)
			{
				object propertyValue = storeMailbox.GetPropertyValue(operationContext, PropTag.Mailbox.LastLogoffTime);
				if (propertyValue == null || ((DateTime)propertyValue).AddMinutes(1.0) < now || (MapiLogon.updateLastLogonLogoffTimeTestHook.Value != null && MapiLogon.updateLastLogonLogoffTimeTestHook.Value()))
				{
					storeMailbox.SetProperty(operationContext, PropTag.Mailbox.LastLogoffTime, now);
				}
			});
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000E12C File Offset: 0x0000C32C
		public string[] GetSupportedAddressTypes()
		{
			return MapiLogon.supportedAddressTypes;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000E134 File Offset: 0x0000C334
		public bool CanHandleAddressType(string addrType)
		{
			if (!string.IsNullOrEmpty(addrType))
			{
				foreach (string strB in this.GetSupportedAddressTypes())
				{
					if (string.Compare(addrType, strB, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000E173 File Offset: 0x0000C373
		public void Connect(MapiContext context)
		{
			base.ThrowIfNotValid(null);
			this.mapiMailbox.Connect(context);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000E188 File Offset: 0x0000C388
		public void SetTransportProvider(out ExchangeId transportQueue)
		{
			base.ThrowIfNotValid(null);
			this.mapiTransportProvider = true;
			transportQueue = this.fidc.FidOutbox;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000E1A9 File Offset: 0x0000C3A9
		public ErrorCode SetSpooler()
		{
			base.ThrowIfNotValid(null);
			if (!this.IsOwner)
			{
				return ErrorCode.CreateLogonFailed((LID)55832U);
			}
			this.isSpooler = true;
			return ErrorCode.NoError;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000E1D6 File Offset: 0x0000C3D6
		internal void SetSpoolerForTest(bool spooler)
		{
			this.isSpooler = spooler;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000E1E0 File Offset: 0x0000C3E0
		internal ErrorCode SpoolerLockMessage(MapiContext context, ExchangeId messageId, LockState lockState)
		{
			base.ThrowIfNotValid(null);
			ErrorCode result = ErrorCode.NoError;
			HashSet<ExchangeId> hashSet = Microsoft.Exchange.Protocols.MAPI.Globals.GetSpoolerLockList(this.StoreMailbox);
			if (lockState != LockState.Locked)
			{
				if (this.spoolerLockList != null && this.spoolerLockList.Contains(messageId))
				{
					try
					{
						if (lockState == LockState.Finished)
						{
							SearchFolder searchFolder = Folder.OpenFolder(context, this.StoreMailbox, this.FidC.FidSpoolerQ) as SearchFolder;
							if (searchFolder == null)
							{
								return ErrorCode.CreateNotInQueue((LID)52088U);
							}
							int? num = searchFolder.LookupMessageByMid(context, messageId, new bool?(false));
							if (num == null)
							{
								return ErrorCode.CreateNotInQueue((LID)35352U);
							}
							using (TopMessage topMessage = TopMessage.OpenMessage(context, this.StoreMailbox, num.Value))
							{
								if (topMessage == null)
								{
									return ErrorCode.CreateNotInQueue((LID)51736U);
								}
								topMessage.AdjustUncomputedMessageFlags(context, MessageFlags.None, MessageFlags.Submit | MessageFlags.Unsent);
								topMessage.SaveChanges(context, SaveMessageChangesFlags.SkipQuotaCheck);
								return ErrorCode.NoError;
							}
						}
						return result;
					}
					finally
					{
						if (hashSet == null)
						{
							result = ErrorCode.CreateNotInQueue((LID)62328U);
						}
						else
						{
							if (!hashSet.Remove(messageId))
							{
								result = ErrorCode.CreateNotInQueue((LID)45944U);
							}
							if (hashSet.Count == 0)
							{
								hashSet = null;
								Microsoft.Exchange.Protocols.MAPI.Globals.SetSpoolerLockList(this.StoreMailbox, hashSet);
							}
						}
						this.spoolerLockList.Remove(messageId);
						if (this.spoolerLockList.Count == 0)
						{
							this.spoolerLockList = null;
						}
					}
				}
				return ErrorCode.CreateNotInQueue((LID)43544U);
			}
			if (this.spoolerLockList == null)
			{
				this.spoolerLockList = new HashSet<ExchangeId>();
			}
			if (hashSet == null)
			{
				hashSet = new HashSet<ExchangeId>();
			}
			if (!hashSet.Add(messageId))
			{
				return ErrorCode.CreateNotInQueue((LID)59928U);
			}
			this.spoolerLockList.Add(messageId);
			Microsoft.Exchange.Protocols.MAPI.Globals.SetSpoolerLockList(this.StoreMailbox, hashSet);
			return result;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000E3DC File Offset: 0x0000C5DC
		protected override PropertyBag StorePropertyBag
		{
			get
			{
				return this.StoreMailbox;
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000E3E4 File Offset: 0x0000C5E4
		public override void SetProps(MapiContext context, Properties mapiProperties, ref List<MapiPropertyProblem> allProblems)
		{
			base.ThrowIfNotValid(null);
			base.SetProps(context, mapiProperties, ref allProblems);
			if (mapiProperties.Contains(PropTag.Mailbox.OofState))
			{
				if (!mapiProperties.Contains(PropTag.Mailbox.OofStateUserChangeTime) && !this.IsMoveUser)
				{
					base.InternalSetOnePropShouldNotFail(context, PropTag.Mailbox.OofStateUserChangeTime, this.StoreMailbox.UtcNow);
				}
				if (this.StoreMailbox.IsDirty)
				{
					context.RiseNotificationEvent(new MailboxModifiedNotificationEvent(this.StoreMailbox.Database, this.StoreMailbox.MailboxNumber, null, context.ClientType, EventFlags.None));
				}
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000E478 File Offset: 0x0000C678
		protected override ErrorCode InternalSetOneProp(MapiContext context, StorePropTag propTag, object objValue)
		{
			ErrorCode errorCode = ErrorCode.NoError;
			if (propTag == PropTag.Mailbox.LocaleId)
			{
				int num = (int)objValue;
				if (!CultureHelper.IsValidLcid(context, num))
				{
					return ErrorCode.CreateBadValue((LID)65064U);
				}
				this.StoreMailbox.SetLCID(context, num);
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_MailboxLocaleIdChanged, new object[]
				{
					this.MailboxGuid,
					num
				});
			}
			if (propTag == PropTag.Mailbox.LocalDirectoryEntryID)
			{
				byte[] array = objValue as byte[];
				if (array == null || array.Length != 16)
				{
					throw new ExExceptionCorruptData((LID)33080U, "Invalid data for SetProps(LocalDirectoryEntryID)");
				}
				ExchangeId exchangeId = ExchangeId.CreateFrom8ByteArray(context, this.StoreMailbox.ReplidGuidMap, array, 0);
				ExchangeId mid = ExchangeId.CreateFrom8ByteArray(context, this.StoreMailbox.ReplidGuidMap, array, 8);
				if (exchangeId != this.FidC.FidRoot)
				{
					return ErrorCode.CreateInvalidEntryId((LID)64208U);
				}
				if (exchangeId.IsValid && mid.IsValid)
				{
					if (!this.IsMoveUser)
					{
						using (MapiMessage mapiMessage = new MapiMessage())
						{
							errorCode = mapiMessage.ConfigureMessage(context, this, exchangeId, mid, MessageConfigureFlags.None, CodePage.None);
							if (errorCode != ErrorCode.NoError)
							{
								throw new StoreException((LID)48952U, errorCode);
							}
						}
					}
					this.StoreMailbox.SetProperty(context, PropTag.Mailbox.LocalDirectoryEntryID, array);
				}
				return ErrorCode.NoError;
			}
			else
			{
				if (propTag == PropTag.Mailbox.InTransitStatus)
				{
					InTransitStatus inTransitStatus = (InTransitStatus)((int)objValue);
					Trace inTransitTransitionsTracer = ExTraceGlobals.InTransitTransitionsTracer;
					bool flag = inTransitTransitionsTracer.IsTraceEnabled(TraceType.DebugTrace);
					if (flag)
					{
						inTransitTransitionsTracer.TraceDebug(0L, "Mailbox {0}, Session {4}, Logon {5}, current inTransit mode {1}, new inTransit mode {2}, publicFolder mode {3}", new object[]
						{
							this.MailboxGuid,
							this.InTransitStatus,
							inTransitStatus,
							this.IsForPublicFolderMove,
							this.session.SessionId,
							this.index
						});
					}
					switch (inTransitStatus & InTransitStatus.DirectionMask)
					{
					case InTransitStatus.NotInTransit:
						if ((inTransitStatus & InTransitStatus.ControlMask) != InTransitStatus.NotInTransit)
						{
							return ErrorCode.CreateCorruptData((LID)39976U);
						}
						if (!this.IsMoveUser)
						{
							return ErrorCode.CreateCorruptData((LID)42556U);
						}
						if (this.StoreMailbox.GetPreservingMailboxSignature(context))
						{
							return ErrorCode.CreateCorruptData((LID)46152U);
						}
						if (this.IsMoveDestination && !this.IsForPublicFolderMove)
						{
							this.StoreMailbox.SetMRSPreservingMailboxSignature(context, false);
							if (!PropertyBagHelpers.TestPropertyFlags(context, this.StoreMailbox, PropTag.Mailbox.MailboxFlags, 16, 16))
							{
								return ErrorCode.CreateCorruptData((LID)50888U);
							}
							PropertyBagHelpers.AdjustPropertyFlags(context, this.StoreMailbox, PropTag.Mailbox.MailboxFlags, 0, 16);
							this.StoreMailbox.MakeUserAccessible(context);
							this.StoreMailbox.UpdateTableSizeStatistics(context);
							LogicalIndexCache.DiscardCacheForMailbox(context, this.StoreMailbox.SharedState);
							MailboxMoveSucceededNotificationEvent nev = NotificationEvents.CreateMailboxMoveSucceededNotificationEvent(context, this.mapiMailbox.StoreMailbox, false);
							context.RiseNotificationEvent(nev);
						}
						if (this.IsMoveSource && !this.IsForPublicFolderMove)
						{
							MailboxMoveFailedNotificationEvent nev2 = NotificationEvents.CreateMailboxMoveFailedNotificationEvent(context, this.mapiMailbox.StoreMailbox, true);
							context.RiseNotificationEvent(nev2);
						}
						errorCode = this.RemoveInTransitState();
						if (errorCode != ErrorCode.NoError)
						{
							return errorCode.Propagate((LID)57144U);
						}
						break;
					case InTransitStatus.SourceOfMove:
					case InTransitStatus.DestinationOfMove:
						if ((inTransitStatus & InTransitStatus.ControlMask & ~(InTransitStatus.OnlineMove | InTransitStatus.AllowLargeItem | InTransitStatus.ForPublicFolderMove)) != InTransitStatus.NotInTransit)
						{
							return ErrorCode.CreateCorruptData((LID)51000U);
						}
						if (InTransitInfo.IsMoveDestination(inTransitStatus) && !InTransitInfo.IsForPublicFolderMove(inTransitStatus) && !PropertyBagHelpers.TestPropertyFlags(context, this.StoreMailbox, PropTag.Mailbox.MailboxFlags, 16, 16))
						{
							return ErrorCode.CreateCorruptData((LID)34504U);
						}
						if (InTransitInfo.IsMoveDestination(inTransitStatus) && !InTransitInfo.IsForPublicFolderMove(inTransitStatus))
						{
							this.StoreMailbox.MakeUserAccessible(context);
						}
						if (!this.IsMoveUser && InTransitInfo.IsMoveSource(inTransitStatus) && !InTransitInfo.IsForPublicFolderMove(inTransitStatus))
						{
							MailboxMoveStartedNotificationEvent nev3 = NotificationEvents.CreateMailboxMoveStartedNotificationEvent(context, this.mapiMailbox.StoreMailbox, true);
							context.RiseNotificationEvent(nev3);
						}
						errorCode = this.SetInTransitState(inTransitStatus);
						if (errorCode != ErrorCode.NoError)
						{
							return errorCode.Propagate((LID)61976U);
						}
						break;
					}
					if (flag)
					{
						inTransitTransitionsTracer.TraceDebug(0L, "Mailbox {0}, Session {2}, Logon {3}, current inTransit mode updated to {1}", new object[]
						{
							this.MailboxGuid,
							this.InTransitStatus,
							this.session.SessionId,
							this.index
						});
					}
					return ErrorCode.NoError;
				}
				if (propTag == PropTag.Mailbox.LocalIdNext)
				{
					MapiLogon.FinalizeMailboxSignaturePreservingMailboxMove(context, this.StoreMailbox, objValue);
					return ErrorCode.NoError;
				}
				return base.InternalSetOneProp(context, propTag, objValue).Propagate((LID)30056U);
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000E978 File Offset: 0x0000CB78
		protected override ErrorCode CheckPropertyOperationAllowed(MapiContext context, MapiPropBagBase.PropOperation operation, StorePropTag propTag, object value)
		{
			PropertyType propertyType = propTag.PropType & (PropertyType)61439;
			if (propertyType <= PropertyType.Unicode)
			{
				switch (propertyType)
				{
				case PropertyType.Unspecified:
				case PropertyType.Null:
				case (PropertyType)8:
				case (PropertyType)9:
				case PropertyType.Error:
					break;
				case PropertyType.Int16:
				case PropertyType.Int32:
				case PropertyType.Real32:
				case PropertyType.Real64:
				case PropertyType.Currency:
				case PropertyType.AppTime:
				case PropertyType.Boolean:
					goto IL_82;
				default:
					if (propertyType == PropertyType.Int64 || propertyType == PropertyType.Unicode)
					{
						goto IL_82;
					}
					break;
				}
			}
			else if (propertyType == PropertyType.SysTime || propertyType == PropertyType.Guid || propertyType == PropertyType.Binary)
			{
				goto IL_82;
			}
			if (operation != MapiPropBagBase.PropOperation.GetProps)
			{
				return ErrorCode.CreateUnexpectedType((LID)48744U, propTag.PropTag);
			}
			IL_82:
			switch (operation)
			{
			case MapiPropBagBase.PropOperation.SetProps:
			case MapiPropBagBase.PropOperation.DeleteProps:
			{
				if (propTag.IsNamedProperty)
				{
					throw new InvalidParameterException((LID)52936U, "Invalid logon property.");
				}
				uint propTag2 = propTag.PropTag;
				if (propTag2 <= 1721827331U)
				{
					if (propTag2 == 1712848907U)
					{
						throw new ExExceptionVersionMismatch((LID)38712U, "Unsupported property");
					}
					if (propTag2 != 1721827331U)
					{
						goto IL_10A;
					}
				}
				else if (propTag2 != 1728380931U && propTag2 != 1735196683U)
				{
					goto IL_10A;
				}
				if (operation == MapiPropBagBase.PropOperation.SetProps)
				{
					return ErrorCode.NoError;
				}
				IL_10A:
				ErrorCode first = base.CheckPropertyOperationAllowed(context, operation, propTag, value);
				if (first == ErrorCode.NoError)
				{
					first = this.ValidateExtendedPropertySetOrDeleteOperation(context, operation, propTag);
				}
				return first.Propagate((LID)54844U);
			}
			default:
				return base.CheckPropertyOperationAllowed(context, operation, propTag, value);
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000EAD0 File Offset: 0x0000CCD0
		public void WriteMailboxInfoTrace(MapiContext context)
		{
			IBinaryLogger logger = LoggerManager.GetLogger(LoggerType.ReferenceData);
			if (logger != null && logger.IsLoggingEnabled)
			{
				Mailbox storeMailbox = this.MapiMailbox.StoreMailbox;
				FolderHierarchy folderHierarchy = FolderHierarchy.GetFolderHierarchy(context, storeMailbox, ExchangeShortId.Zero, FolderInformationType.Basic);
				using (TraceBuffer traceBuffer = TraceRecord.Create(LoggerManager.TraceGuids.MailboxInfo, true, false, storeMailbox.Database.MdbGuid.ToString(), storeMailbox.MailboxGuid.ToString(), storeMailbox.Database.MdbGuid.GetHashCode(), storeMailbox.MailboxNumber, (byte)storeMailbox.GetStatus(context), (byte)storeMailbox.MailboxInfo.Type, (byte)storeMailbox.MailboxInfo.TypeDetail, this.MailboxInfo.IsArchiveMailbox, this.MailboxInfo.IsSystemMailbox, this.MailboxInfo.IsHealthMailbox, storeMailbox.GetMessageSize(context), storeMailbox.GetHiddenMessageSize(context), storeMailbox.GetDeletedMessageSize(context), storeMailbox.GetHiddenDeletedMessageSize(context), storeMailbox.GetMessageCount(context), storeMailbox.GetHiddenMessageCount(context), storeMailbox.GetDeletedMessageCount(context), storeMailbox.GetHiddenDeletedMessageCount(context), (long)folderHierarchy.TotalFolderCount, storeMailbox.GetCurrentSchemaVersion(context).Value, (int)storeMailbox.GetPropertyValue(context, PropTag.Mailbox.MessageTableTotalPages), (int)storeMailbox.GetPropertyValue(context, PropTag.Mailbox.MessageTableAvailablePages), (int)storeMailbox.GetPropertyValue(context, PropTag.Mailbox.AttachmentTableTotalPages), (int)storeMailbox.GetPropertyValue(context, PropTag.Mailbox.AttachmentTableAvailablePages), (int)storeMailbox.GetPropertyValue(context, PropTag.Mailbox.OtherTablesTotalPages), (int)storeMailbox.GetPropertyValue(context, PropTag.Mailbox.OtherTablesAvailablePages), storeMailbox.MailboxInfo.OwnerGuid.ToString(), (int)(storeMailbox.GetPropertyValue(context, PropTag.Mailbox.ScheduledISIntegCorruptionCount) ?? -1), (int)(storeMailbox.GetPropertyValue(context, PropTag.Mailbox.ScheduledISIntegExecutionTime) ?? -1)))
				{
					logger.TryWrite(traceBuffer);
				}
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000ECD8 File Offset: 0x0000CED8
		public byte[] GetLocalDirectoryEntryId(MapiContext context)
		{
			byte[] array = null;
			StorePropTag storePropTag;
			object obj;
			if (this.StoreMailbox.TryGetProperty(context, 13334, out storePropTag, out obj))
			{
				array = (byte[])obj;
				if (array == null || array.Length != 16)
				{
					if (ExTraceGlobals.GetPropsPropertiesTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.GetPropsPropertiesTracer.TraceDebug(0L, "LocalDirectoryEntryId is malformed");
					}
					array = null;
				}
				else
				{
					ExchangeId fid = ExchangeId.CreateFrom8ByteArray(context, this.MapiMailbox.StoreMailbox.ReplidGuidMap, array, 0);
					ExchangeId mid = ExchangeId.CreateFrom8ByteArray(context, this.MapiMailbox.StoreMailbox.ReplidGuidMap, array, 8);
					using (MapiMessage mapiMessage = new MapiMessage())
					{
						ErrorCode errorCode = mapiMessage.ConfigureMessage(context, this, fid, mid, MessageConfigureFlags.None, this.CodePage);
						if (errorCode != ErrorCode.NoError)
						{
							if (errorCode != ErrorCodeValue.NotFound)
							{
								throw new StoreException((LID)43216U, errorCode);
							}
							if (ExTraceGlobals.GetPropsPropertiesTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								ExTraceGlobals.GetPropsPropertiesTracer.TraceDebug(0L, "Local directory message is not found");
							}
							array = null;
						}
					}
				}
			}
			if (array != null)
			{
				return array;
			}
			if (this.IsMoveUser)
			{
				return new byte[16];
			}
			byte[] result;
			using (MapiMessage mapiMessage2 = new MapiMessage())
			{
				ErrorCode errorCode2 = mapiMessage2.ConfigureMessage(context, this, this.FidC.FidRoot, ExchangeId.Null, MessageConfigureFlags.CreateNewMessage | MessageConfigureFlags.SkipQuotaCheck, CodePage.None);
				if (errorCode2 != ErrorCode.NoError)
				{
					throw new StoreException((LID)59600U, errorCode2);
				}
				ExchangeId exchangeId;
				errorCode2 = mapiMessage2.SaveChanges(context, MapiSaveMessageChangesFlags.SkipMailboxQuotaCheck | MapiSaveMessageChangesFlags.SkipFolderQuotaCheck | MapiSaveMessageChangesFlags.SkipSizeCheck, out exchangeId);
				if (errorCode2 != ErrorCode.NoError)
				{
					throw new StoreException((LID)35024U, errorCode2);
				}
				int num = 0;
				array = new byte[16];
				num += ExchangeIdHelpers.To8ByteArray(this.FidC.FidRoot.Replid, this.FidC.FidRoot.Counter, array, num);
				ExchangeIdHelpers.To8ByteArray(exchangeId.Replid, exchangeId.Counter, array, num);
				this.StoreMailbox.SetProperty(context, PropTag.Mailbox.LocalDirectoryEntryID, array);
				result = array;
			}
			return result;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000EF14 File Offset: 0x0000D114
		internal ErrorCode SetInTransitState(InTransitStatus status)
		{
			ErrorCode errorCode = InTransitInfo.SetInTransitState(this.MapiMailbox.SharedState, status, this);
			Trace inTransitTransitionsTracer = ExTraceGlobals.InTransitTransitionsTracer;
			bool flag = inTransitTransitionsTracer.IsTraceEnabled(TraceType.DebugTrace);
			if (flag)
			{
				if (errorCode == ErrorCode.NoError)
				{
					inTransitTransitionsTracer.TraceDebug<Guid, InTransitStatus>(0L, "Mailbox {0} was added to the in-transit list with status {1}", this.MailboxGuid, status);
				}
				else
				{
					inTransitTransitionsTracer.TraceDebug<Guid, ErrorCode>(0L, "Mailbox {0} was NOT added to the in-transit list due to error {1}", this.MailboxGuid, errorCode);
				}
			}
			return errorCode.Propagate((LID)30464U);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000EF90 File Offset: 0x0000D190
		internal ErrorCode RemoveInTransitState()
		{
			ErrorCode errorCode = InTransitInfo.RemoveInTransitState(this.MapiMailbox.SharedState, this);
			Trace inTransitTransitionsTracer = ExTraceGlobals.InTransitTransitionsTracer;
			bool flag = inTransitTransitionsTracer.IsTraceEnabled(TraceType.DebugTrace);
			if (flag)
			{
				if (errorCode == ErrorCode.NoError)
				{
					inTransitTransitionsTracer.TraceDebug<Guid>(0L, "Mailbox {0} was removed from the in-transit list", this.mapiMailbox.MailboxGuid);
				}
				else
				{
					inTransitTransitionsTracer.TraceDebug<Guid, ErrorCode>(0L, "Mailbox {0} was NOT removed from the in-transit list due to error {1}", this.mapiMailbox.MailboxGuid, errorCode);
				}
			}
			return errorCode.Propagate((LID)59993U);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000F011 File Offset: 0x0000D211
		internal InTransitStatus GetInTransitStatus()
		{
			return InTransitInfo.GetInTransitStatusForClient(this.MapiMailbox.SharedState, this);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000F024 File Offset: 0x0000D224
		internal bool AllowLargeItem()
		{
			InTransitStatus inTransitStatus = this.GetInTransitStatus();
			return (inTransitStatus & InTransitStatus.DirectionMask) == InTransitStatus.DestinationOfMove && (inTransitStatus & InTransitStatus.AllowLargeItem) == InTransitStatus.AllowLargeItem;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000F04C File Offset: 0x0000D24C
		internal bool CannotLogonToInTransitMailbox(MapiContext context)
		{
			if (context.ClientType == ClientType.Migration || ClientTypeHelper.IsContentIndexing(context.ClientType))
			{
				return InTransitInfo.IsClientNotAllowedToLogIn(this.MapiMailbox.SharedState, false, this);
			}
			return PropertyBagHelpers.TestPropertyFlags(context, this.StoreMailbox, PropTag.Mailbox.MailboxFlags, 16, 16) || this.constructionTime < this.MapiMailbox.SharedState.InvalidatedTime || InTransitInfo.IsClientNotAllowedToLogIn(this.MapiMailbox.SharedState, true, this);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000F0D0 File Offset: 0x0000D2D0
		internal override void CheckRights(MapiContext context, FolderSecurity.ExchangeSecurityDescriptorFolderRights requestedRights, bool allRights, AccessCheckOperation operation, LID lid)
		{
			if (operation == AccessCheckOperation.PropertyGet || operation == AccessCheckOperation.PropertyGetList)
			{
				return;
			}
			bool flag = this.accessCheckState.CheckContextRights(context, requestedRights);
			if (ExTraceGlobals.AccessCheckTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.AccessCheckTracer.TraceDebug(0L, "MapiLogon({0}) logon access check: Operation={1}, Requested rights={2}, Allowed={3}", new object[]
				{
					this.logonUser.UserSid,
					operation,
					requestedRights,
					flag
				});
			}
			if (!flag)
			{
				DiagnosticContext.TraceDword(lid, (uint)operation);
				DiagnosticContext.TraceDword((LID)50152U, (uint)this.accessCheckState.GetContextRights(context, false));
				throw new ExExceptionAccessDenied((LID)36664U, "Insufficient rights.");
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000F184 File Offset: 0x0000D384
		public bool IsUserLogon(MapiContext context)
		{
			if (this.SystemRights || this.AdminRights || this.Session == null)
			{
				return false;
			}
			ClientType clientType = context.ClientType;
			if (clientType <= ClientType.WebServices)
			{
				switch (clientType)
				{
				case ClientType.User:
				case ClientType.AirSync:
				case ClientType.OWA:
				case ClientType.Imap:
					break;
				case ClientType.Transport:
				case ClientType.EventBasedAssistants:
					return false;
				default:
					if (clientType != ClientType.WebServices)
					{
						return false;
					}
					break;
				}
			}
			else if (clientType != ClientType.MoMT && clientType != ClientType.Pop)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000F1EB File Offset: 0x0000D3EB
		public void DeferReleaseROP(MapiBase serverObject)
		{
			if (this.deferedReleaseROPs == null)
			{
				this.deferedReleaseROPs = new Queue<MapiBase>();
			}
			this.deferedReleaseROPs.Enqueue(serverObject);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000F20C File Offset: 0x0000D40C
		public void ProcessDeferedReleaseROPs(MapiContext context)
		{
			if (this.deferedReleaseROPs == null)
			{
				return;
			}
			while (this.deferedReleaseROPs.Count != 0)
			{
				MapiBase mapiBase = this.deferedReleaseROPs.Dequeue();
				try
				{
					try
					{
						context.Diagnostics.ClearExceptionHistory();
						mapiBase.OnRelease(context);
					}
					finally
					{
						mapiBase.Dispose();
					}
					this.StoreMailbox.Save(context);
					context.Commit();
				}
				catch (StoreException exception)
				{
					context.OnExceptionCatch(exception);
				}
				finally
				{
					context.Abort();
				}
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000F2A8 File Offset: 0x0000D4A8
		public bool IsDeferedReleaseSharedOperation()
		{
			if (this.deferedReleaseROPs == null)
			{
				return true;
			}
			foreach (MapiBase mapiBase in this.deferedReleaseROPs)
			{
				MapiStream mapiStream = mapiBase as MapiStream;
				if (mapiStream != null && mapiStream.ReleaseMayNeedExclusiveLock)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000F318 File Offset: 0x0000D518
		private ErrorCode ValidateExtendedPropertySetOrDeleteOperation(MapiContext context, MapiPropBagBase.PropOperation operation, StorePropTag propTag)
		{
			if (MapiLogon.wellKnownMailboxPropsAllowedToSetByAnyClient.Contains(propTag.PropTag))
			{
				return ErrorCode.NoError;
			}
			object obj = null;
			StorePropTag storePropTag = propTag;
			bool flag = this.TryGetPropertyImp(context, propTag.PropId, out storePropTag, out obj);
			if (operation == MapiPropBagBase.PropOperation.DeleteProps && !flag)
			{
				return ErrorCode.NoError;
			}
			if (!this.CanModifyExtendedProperty(context))
			{
				return ErrorCode.CreateNoAccess((LID)59496U, propTag.PropTag);
			}
			if (flag && storePropTag.PropType != propTag.PropType)
			{
				return ErrorCode.CreateUnexpectedType((LID)34920U, propTag.PropTag);
			}
			return ErrorCode.NoError;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000F3B4 File Offset: 0x0000D5B4
		private bool CanModifyExtendedProperty(MapiContext context)
		{
			ClientType clientType = context.ClientType;
			if (clientType <= ClientType.TransportSync)
			{
				if (clientType != ClientType.EventBasedAssistants)
				{
					switch (clientType)
					{
					case ClientType.Management:
					case ClientType.UnifiedMessaging:
					case ClientType.WebServices:
					case ClientType.TimeBasedAssistants:
					case ClientType.Migration:
					case ClientType.TransportSync:
						break;
					case ClientType.Monitoring:
					case ClientType.ApprovalAPI:
					case ClientType.MoMT:
						return false;
					default:
						return false;
					}
				}
			}
			else if (clientType != ClientType.TeamMailbox)
			{
				switch (clientType)
				{
				case ClientType.UnifiedPolicy:
				case ClientType.NotificationBroker:
					break;
				default:
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400014D RID: 333
		private static Hookable<Func<bool>> controlAdminRights = Hookable<Func<bool>>.Create(true, null);

		// Token: 0x0400014E RID: 334
		private static Hookable<Func<bool>> controlTransportRights = Hookable<Func<bool>>.Create(true, null);

		// Token: 0x0400014F RID: 335
		private static Hookable<Func<bool>> updateLastLogonLogoffTimeTestHook = Hookable<Func<bool>>.Create(true, null);

		// Token: 0x04000150 RID: 336
		internal static Dictionary<QuotaType, ExEventLog.EventTuple> PublicFolderQuotaEvents = new Dictionary<QuotaType, ExEventLog.EventTuple>(2)
		{
			{
				QuotaType.StorageWarningLimit,
				MSExchangeISEventLogConstants.Tuple_FolderStorageOverWarningLimit
			},
			{
				QuotaType.StorageShutoff,
				MSExchangeISEventLogConstants.Tuple_FolderStorageShutoff
			}
		};

		// Token: 0x04000151 RID: 337
		internal static Dictionary<QuotaType, ExEventLog.EventTuple> MailboxQuotaEvents = new Dictionary<QuotaType, ExEventLog.EventTuple>(5)
		{
			{
				QuotaType.StorageWarningLimit,
				MSExchangeISEventLogConstants.Tuple_MailboxStorageOverWarningLimit
			},
			{
				QuotaType.StorageOverQuotaLimit,
				MSExchangeISEventLogConstants.Tuple_MailboxStorageOverQuotaLimit
			},
			{
				QuotaType.StorageShutoff,
				MSExchangeISEventLogConstants.Tuple_MailboxStorageShutoff
			},
			{
				QuotaType.DumpsterWarningLimit,
				MSExchangeISEventLogConstants.Tuple_MailboxOverDumpsterWarningQuota
			},
			{
				QuotaType.DumpsterShutoff,
				MSExchangeISEventLogConstants.Tuple_MailboxOverDumpsterQuota
			}
		};

		// Token: 0x04000152 RID: 338
		internal static Dictionary<QuotaType, ExEventLog.EventTuple> MailboxShapeQuotaEvents = new Dictionary<QuotaType, ExEventLog.EventTuple>(8)
		{
			{
				QuotaType.MailboxMessagesPerFolderCountWarningQuota,
				MSExchangeISEventLogConstants.Tuple_MailboxMessagesPerFolderCountWarningQuota
			},
			{
				QuotaType.MailboxMessagesPerFolderCountReceiveQuota,
				MSExchangeISEventLogConstants.Tuple_MailboxMessagesPerFolderCountReceiveQuota
			},
			{
				QuotaType.DumpsterMessagesPerFolderCountWarningQuota,
				MSExchangeISEventLogConstants.Tuple_DumpsterMessagesPerFolderCountWarningQuota
			},
			{
				QuotaType.DumpsterMessagesPerFolderCountReceiveQuota,
				MSExchangeISEventLogConstants.Tuple_DumpsterMessagesPerFolderCountReceiveQuota
			},
			{
				QuotaType.FolderHierarchyChildrenCountWarningQuota,
				MSExchangeISEventLogConstants.Tuple_FolderHierarchyChildrenCountWarningQuota
			},
			{
				QuotaType.FolderHierarchyChildrenCountReceiveQuota,
				MSExchangeISEventLogConstants.Tuple_FolderHierarchyChildrenCountReceiveQuota
			},
			{
				QuotaType.FolderHierarchyDepthWarningQuota,
				MSExchangeISEventLogConstants.Tuple_FolderHierarchyDepthWarningQuota
			},
			{
				QuotaType.FolderHierarchyDepthReceiveQuota,
				MSExchangeISEventLogConstants.Tuple_FolderHierarchyDepthReceiveQuota
			},
			{
				QuotaType.FoldersCountWarningQuota,
				MSExchangeISEventLogConstants.Tuple_FoldersCountWarningQuota
			},
			{
				QuotaType.FoldersCountReceiveQuota,
				MSExchangeISEventLogConstants.Tuple_FoldersCountReceiveQuota
			}
		};

		// Token: 0x04000153 RID: 339
		internal static Dictionary<QuotaType, ExEventLog.EventTuple> ArchiveQuotaEvents = new Dictionary<QuotaType, ExEventLog.EventTuple>(4)
		{
			{
				QuotaType.StorageWarningLimit,
				MSExchangeISEventLogConstants.Tuple_ArchiveStorageOverWarningLimit
			},
			{
				QuotaType.StorageShutoff,
				MSExchangeISEventLogConstants.Tuple_ArchiveStorageShutoff
			},
			{
				QuotaType.DumpsterWarningLimit,
				MSExchangeISEventLogConstants.Tuple_ArchiveOverDumpsterWarningQuota
			},
			{
				QuotaType.DumpsterShutoff,
				MSExchangeISEventLogConstants.Tuple_ArchiveOverDumpsterQuota
			}
		};

		// Token: 0x04000154 RID: 340
		private static int updateLastLogonTimeActionSlot = -1;

		// Token: 0x04000155 RID: 341
		private static int updateLastLogoffTimeActionSlot = -1;

		// Token: 0x04000156 RID: 342
		private static Hookable<MapiLogon.GenerateQuotaReportDelegate> hookableGenerateQuotaReport = Hookable<MapiLogon.GenerateQuotaReportDelegate>.Create(true, new MapiLogon.GenerateQuotaReportDelegate(MapiLogon.InternalGenerateQuotaReport));

		// Token: 0x04000157 RID: 343
		private static HashSet<uint> wellKnownMailboxPropsAllowedToSetByAnyClient = new HashSet<uint>
		{
			1712848899U,
			1721958464U,
			1721892928U,
			873857282U,
			1734410498U,
			1071644930U,
			920129794U,
			1071906827U,
			1710624799U,
			805830720U,
			1746534411U,
			267911426U,
			1735393291U,
			237437186U,
			1721761823U,
			1721762050U,
			1713176587U,
			1745879043U,
			1746075712U,
			1736114179U,
			1724579871U,
			1746141442U,
			875561024U,
			875626560U,
			1071841538U,
			2082078723U,
			2082144259U,
			877396032U
		};

		// Token: 0x04000158 RID: 344
		private static string[] supportedAddressTypes = new string[]
		{
			"X400",
			"EX",
			"XENIX",
			"SMTP",
			"MS",
			"MSXCX500",
			"MOBILE"
		};

		// Token: 0x04000159 RID: 345
		private DateTime constructionTime;

		// Token: 0x0400015A RID: 346
		private int index = -1;

		// Token: 0x0400015B RID: 347
		private bool isOwner;

		// Token: 0x0400015C RID: 348
		private bool isPrimaryOwner;

		// Token: 0x0400015D RID: 349
		private bool hasAdminRights;

		// Token: 0x0400015E RID: 350
		private bool unifiedLogon;

		// Token: 0x0400015F RID: 351
		private MapiMailbox mapiMailbox;

		// Token: 0x04000160 RID: 352
		private CrucialFolderId fidc;

		// Token: 0x04000161 RID: 353
		private MapiSession session;

		// Token: 0x04000162 RID: 354
		private MapiLogon.PendingNotificationQueue pendingNotifications;

		// Token: 0x04000163 RID: 355
		private AddressInfo logonUser;

		// Token: 0x04000164 RID: 356
		private AddressInfo mailboxOwner;

		// Token: 0x04000165 RID: 357
		private DatabaseInfo databaseInfo;

		// Token: 0x04000166 RID: 358
		private bool mapiTransportProvider;

		// Token: 0x04000167 RID: 359
		private bool isSpooler;

		// Token: 0x04000168 RID: 360
		private HashSet<ExchangeId> spoolerLockList;

		// Token: 0x04000169 RID: 361
		private AccessCheckState accessCheckState;

		// Token: 0x0400016A RID: 362
		private Guid perUserTransferReplicaGuid;

		// Token: 0x0400016B RID: 363
		private StoreLongTermId perUserTransferFolderId;

		// Token: 0x0400016C RID: 364
		private MemoryStream perUserTransferData;

		// Token: 0x0400016D RID: 365
		private OpenStoreFlags openStoreFlags;

		// Token: 0x0400016E RID: 366
		private LinkedListNode<MapiLogon> nodeOfMailboxStateLogonList;

		// Token: 0x0400016F RID: 367
		private MapiLogon.ClientActivityTracker clientActivityTracker = new MapiLogon.ClientActivityTracker();

		// Token: 0x04000170 RID: 368
		private Queue<MapiBase> deferedReleaseROPs;

		// Token: 0x02000052 RID: 82
		// (Invoke) Token: 0x0600020F RID: 527
		public delegate ErrorCode GenerateQuotaReportDelegate(MapiContext context, MapiLogon mapiLogon, Folder folder, QuotaType quotaType, QuotaInfo quotaInfo, IList<byte[]> recipients, long containerSize);

		// Token: 0x02000053 RID: 83
		internal class PendingNotificationQueue
		{
			// Token: 0x06000212 RID: 530 RVA: 0x0000F76F File Offset: 0x0000D96F
			public PendingNotificationQueue()
			{
				this.array = new MapiLogon.PendingNotificationQueue.PendingNotificationEntry[17];
				this.head = 0;
				this.tail = 0;
			}

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x06000213 RID: 531 RVA: 0x0000F792 File Offset: 0x0000D992
			public int Count
			{
				get
				{
					if (this.head > this.tail)
					{
						return this.tail + this.array.Length - this.head;
					}
					return this.tail - this.head;
				}
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x06000214 RID: 532 RVA: 0x0000F7C6 File Offset: 0x0000D9C6
			public bool IsEmpty
			{
				get
				{
					return this.head == this.tail;
				}
			}

			// Token: 0x06000215 RID: 533 RVA: 0x0000F7D8 File Offset: 0x0000D9D8
			public void Enqueue(NotificationEvent nev, uint hsot)
			{
				Statistics.LogonNotifications.Total.Bump();
				if (!this.IsEmpty)
				{
					NotificationEvent.RedundancyStatus redundancyStatus = NotificationEvent.RedundancyStatus.Continue;
					int num = this.Count - 1;
					while (num >= 0 && (redundancyStatus & NotificationEvent.RedundancyStatus.FlagStopSearch) == NotificationEvent.RedundancyStatus.Continue)
					{
						if (this.array[this.PositionFromIndex(num)].Hsot == hsot)
						{
							redundancyStatus = nev.GetRedundancyStatus(this.array[this.PositionFromIndex(num)].Nev);
							if ((redundancyStatus & (NotificationEvent.RedundancyStatus.FlagDropNew | NotificationEvent.RedundancyStatus.FlagDropOld | NotificationEvent.RedundancyStatus.FlagReplaceOld | NotificationEvent.RedundancyStatus.FlagMerge)) != NotificationEvent.RedundancyStatus.Continue)
							{
								NotificationEvent nev2 = this.array[this.PositionFromIndex(num)].Nev;
								if ((redundancyStatus & NotificationEvent.RedundancyStatus.FlagDropOld) != NotificationEvent.RedundancyStatus.Continue)
								{
									Statistics.LogonNotifications.Redundant.Bump();
									Statistics.LogonNotifications.DropOld.Bump();
									this.RemoveAt(num);
								}
								if ((redundancyStatus & NotificationEvent.RedundancyStatus.FlagDropNew) != NotificationEvent.RedundancyStatus.Continue)
								{
									Statistics.LogonNotifications.Redundant.Bump();
									Statistics.LogonNotifications.DropNew.Bump();
									nev = null;
									break;
								}
								if ((redundancyStatus & NotificationEvent.RedundancyStatus.FlagMerge) != NotificationEvent.RedundancyStatus.Continue)
								{
									Statistics.LogonNotifications.Merge.Bump();
									nev = nev.MergeWithOldEvent(nev2);
								}
								if ((redundancyStatus & NotificationEvent.RedundancyStatus.FlagReplaceOld) != NotificationEvent.RedundancyStatus.Continue)
								{
									this.array[this.PositionFromIndex(num)].Nev = nev;
									this.array[this.PositionFromIndex(num)].Hsot = hsot;
									Statistics.LogonNotifications.Redundant.Bump();
									Statistics.LogonNotifications.ReplaceOld.Bump();
									nev = null;
									break;
								}
							}
						}
						num--;
					}
				}
				if (nev != null)
				{
					bool flag = false;
					if ((this.tail + 1) % this.array.Length == this.head && !this.Expand())
					{
						flag = true;
					}
					if (flag)
					{
						int num2 = 0;
						while (num2 < this.Count && flag)
						{
							if ((long)this.array[this.PositionFromIndex(num2)].Nev.EventTypeValue == 256L)
							{
								TableModifiedNotificationEvent tableModifiedNotificationEvent = this.array[this.PositionFromIndex(num2)].Nev as TableModifiedNotificationEvent;
								if (tableModifiedNotificationEvent.TableEventType != TableEventType.Changed)
								{
									for (int i = num2 + 1; i < this.Count; i++)
									{
										if (this.array[this.PositionFromIndex(num2)].Hsot == this.array[this.PositionFromIndex(i)].Hsot)
										{
											if (hsot == this.array[this.PositionFromIndex(num2)].Hsot)
											{
												nev = null;
											}
											Statistics.LogonNotifications.OverflowFlushWithTableChanged.Bump();
											this.Enqueue(new TableModifiedNotificationEvent(tableModifiedNotificationEvent.Database, tableModifiedNotificationEvent.MailboxNumber, tableModifiedNotificationEvent.UserIdentity, tableModifiedNotificationEvent.ClientType, EventFlags.None, TableEventType.Changed, tableModifiedNotificationEvent.Fid, ExchangeId.Null, 0, ExchangeId.Null, ExchangeId.Null, 0, Properties.Empty), this.array[this.PositionFromIndex(num2)].Hsot);
											flag = false;
											break;
										}
									}
								}
							}
							num2++;
						}
					}
					if (flag)
					{
						Statistics.LogonNotifications.OverflowDrop.Bump();
						uint num3;
						this.Dequeue(out num3);
					}
					if (nev != null)
					{
						this.array[this.tail].Hsot = hsot;
						this.array[this.tail].Nev = nev;
						this.tail = (this.tail + 1) % this.array.Length;
					}
				}
			}

			// Token: 0x06000216 RID: 534 RVA: 0x0000FB00 File Offset: 0x0000DD00
			public NotificationEvent Dequeue(out uint hsot)
			{
				if (this.IsEmpty)
				{
					hsot = 0U;
					return null;
				}
				hsot = this.array[this.head].Hsot;
				NotificationEvent nev = this.array[this.head].Nev;
				this.array[this.head].Nev = null;
				this.head = (this.head + 1) % this.array.Length;
				return nev;
			}

			// Token: 0x06000217 RID: 535 RVA: 0x0000FB78 File Offset: 0x0000DD78
			public NotificationEvent Peek(out uint hsot)
			{
				if (this.IsEmpty)
				{
					hsot = 0U;
					return null;
				}
				hsot = this.array[this.head].Hsot;
				return this.array[this.head].Nev;
			}

			// Token: 0x06000218 RID: 536 RVA: 0x0000FBB5 File Offset: 0x0000DDB5
			internal int PositionFromIndex(int index)
			{
				return (this.head + index) % this.array.Length;
			}

			// Token: 0x06000219 RID: 537 RVA: 0x0000FBC8 File Offset: 0x0000DDC8
			internal void RemoveAt(int index)
			{
				int num = this.PositionFromIndex(index);
				int num2 = this.PositionFromIndex(index + 1);
				if (num == this.head)
				{
					this.array[this.head].Nev = null;
					this.head = (this.head + 1) % this.array.Length;
					return;
				}
				if (num2 == this.tail)
				{
					this.tail = (this.array.Length + this.tail - 1) % this.array.Length;
					this.array[this.tail].Nev = null;
					return;
				}
				if (this.tail > num)
				{
					this.tail--;
					Array.Copy(this.array, num2, this.array, num, this.tail - num);
					this.array[this.tail].Nev = null;
					return;
				}
				Array.Copy(this.array, this.head, this.array, this.head + 1, num - this.head);
				this.array[this.head].Nev = null;
				this.head = (this.head + 1) % this.array.Length;
			}

			// Token: 0x0600021A RID: 538 RVA: 0x0000FD00 File Offset: 0x0000DF00
			internal NotificationEvent GetAt(int index, out uint hsot)
			{
				int num = this.PositionFromIndex(index);
				hsot = this.array[num].Hsot;
				return this.array[num].Nev;
			}

			// Token: 0x0600021B RID: 539 RVA: 0x0000FD3C File Offset: 0x0000DF3C
			internal void FlushStaleTableModifiedNotifications(uint hsot)
			{
				if (!this.IsEmpty)
				{
					TableModifiedNotificationEvent tableModifiedNotificationEvent = null;
					int i = this.Count - 1;
					while (i >= 0)
					{
						if (this.array[this.PositionFromIndex(i)].Hsot == hsot)
						{
							tableModifiedNotificationEvent = (this.array[this.PositionFromIndex(i)].Nev as TableModifiedNotificationEvent);
							if (tableModifiedNotificationEvent.TableEventType == TableEventType.Changed)
							{
								tableModifiedNotificationEvent = null;
								break;
							}
							break;
						}
						else
						{
							i--;
						}
					}
					if (tableModifiedNotificationEvent != null)
					{
						this.Enqueue(new TableModifiedNotificationEvent(tableModifiedNotificationEvent.Database, tableModifiedNotificationEvent.MailboxNumber, tableModifiedNotificationEvent.UserIdentity, tableModifiedNotificationEvent.ClientType, EventFlags.None, TableEventType.Changed, tableModifiedNotificationEvent.Fid, ExchangeId.Null, 0, ExchangeId.Null, ExchangeId.Null, 0, Properties.Empty), hsot);
					}
				}
			}

			// Token: 0x0600021C RID: 540 RVA: 0x0000FDF4 File Offset: 0x0000DFF4
			internal bool IsThereTableChangedNotification(uint hsot)
			{
				if (!this.IsEmpty)
				{
					for (int i = this.Count - 1; i >= 0; i--)
					{
						if (this.array[this.PositionFromIndex(i)].Hsot == hsot)
						{
							TableModifiedNotificationEvent tableModifiedNotificationEvent = this.array[this.PositionFromIndex(i)].Nev as TableModifiedNotificationEvent;
							return tableModifiedNotificationEvent.TableEventType == TableEventType.Changed;
						}
					}
				}
				return false;
			}

			// Token: 0x0600021D RID: 541 RVA: 0x0000FE64 File Offset: 0x0000E064
			private bool Expand()
			{
				if (this.array.Length > 64)
				{
					return false;
				}
				int num = (this.array.Length - 1) * 2;
				MapiLogon.PendingNotificationQueue.PendingNotificationEntry[] destinationArray = new MapiLogon.PendingNotificationQueue.PendingNotificationEntry[num + 1];
				if (this.head < this.tail)
				{
					Array.Copy(this.array, this.head, destinationArray, 0, this.tail - this.head);
				}
				else if (this.head > this.tail)
				{
					Array.Copy(this.array, this.head, destinationArray, 0, this.array.Length - this.head);
					Array.Copy(this.array, 0, destinationArray, this.array.Length - this.head, this.tail);
				}
				this.tail = this.Count;
				this.head = 0;
				this.array = destinationArray;
				return true;
			}

			// Token: 0x04000174 RID: 372
			internal const int MaximumCapacity = 64;

			// Token: 0x04000175 RID: 373
			private const int InitialCapacity = 16;

			// Token: 0x04000176 RID: 374
			private MapiLogon.PendingNotificationQueue.PendingNotificationEntry[] array;

			// Token: 0x04000177 RID: 375
			private int head;

			// Token: 0x04000178 RID: 376
			private int tail;

			// Token: 0x02000054 RID: 84
			private struct PendingNotificationEntry
			{
				// Token: 0x04000179 RID: 377
				public NotificationEvent Nev;

				// Token: 0x0400017A RID: 378
				public uint Hsot;
			}
		}

		// Token: 0x02000055 RID: 85
		public class ClientActivityTracker
		{
			// Token: 0x17000057 RID: 87
			// (get) Token: 0x0600021E RID: 542 RVA: 0x0000FF32 File Offset: 0x0000E132
			internal ushort[] RopsCount
			{
				get
				{
					return this.ropsCountInCurrentActivity;
				}
			}

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x0600021F RID: 543 RVA: 0x0000FF3A File Offset: 0x0000E13A
			internal int NumberOfRpcCalls
			{
				get
				{
					return this.numberOfRpcCallsForCurrentActivityId;
				}
			}

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x06000220 RID: 544 RVA: 0x0000FF42 File Offset: 0x0000E142
			// (set) Token: 0x06000221 RID: 545 RVA: 0x0000FF4A File Offset: 0x0000E14A
			internal bool ActivityReported
			{
				get
				{
					return this.activityReported;
				}
				set
				{
					this.activityReported = value;
				}
			}

			// Token: 0x06000222 RID: 546 RVA: 0x0000FF54 File Offset: 0x0000E154
			internal bool Update(Guid activityId, int rpcCallCookie, RopId ropId)
			{
				bool result = false;
				if (this.currentActivityId == activityId)
				{
					if (this.ropsCountInCurrentActivity[(int)ropId] == 0)
					{
						result = true;
					}
				}
				else
				{
					result = true;
					this.currentActivityId = activityId;
					Array.Clear(this.ropsCountInCurrentActivity, 0, this.ropsCountInCurrentActivity.Length);
					this.numberOfRpcCallsForCurrentActivityId = 0;
					this.currentRpcCallCookie = 0;
				}
				if (this.currentRpcCallCookie != rpcCallCookie)
				{
					this.currentRpcCallCookie = rpcCallCookie;
					this.numberOfRpcCallsForCurrentActivityId++;
				}
				if (this.ropsCountInCurrentActivity[(int)ropId] != 65535)
				{
					ushort[] array = this.ropsCountInCurrentActivity;
					array[(int)ropId] = array[(int)ropId] + 1;
				}
				return result;
			}

			// Token: 0x0400017B RID: 379
			private Guid currentActivityId;

			// Token: 0x0400017C RID: 380
			private ushort[] ropsCountInCurrentActivity = new ushort[256];

			// Token: 0x0400017D RID: 381
			private int currentRpcCallCookie;

			// Token: 0x0400017E RID: 382
			private int numberOfRpcCallsForCurrentActivityId;

			// Token: 0x0400017F RID: 383
			private bool activityReported;
		}
	}
}
