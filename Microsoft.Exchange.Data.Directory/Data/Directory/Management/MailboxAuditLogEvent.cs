using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200072F RID: 1839
	[Serializable]
	public sealed class MailboxAuditLogEvent : MailboxAuditLogRecord
	{
		// Token: 0x060057ED RID: 22509 RVA: 0x00139F77 File Offset: 0x00138177
		public MailboxAuditLogEvent()
		{
		}

		// Token: 0x060057EE RID: 22510 RVA: 0x00139F7F File Offset: 0x0013817F
		public MailboxAuditLogEvent(MailboxAuditLogRecordId identity, string mailboxResolvedName, string guid, DateTime? lastAccessed) : base(identity, mailboxResolvedName, guid, lastAccessed)
		{
		}

		// Token: 0x17001DCA RID: 7626
		// (get) Token: 0x060057EF RID: 22511 RVA: 0x00139F8C File Offset: 0x0013818C
		// (set) Token: 0x060057F0 RID: 22512 RVA: 0x00139FA3 File Offset: 0x001381A3
		public string Operation
		{
			get
			{
				return this.propertyBag[MailboxAuditLogEventSchema.Operation] as string;
			}
			private set
			{
				this.propertyBag[MailboxAuditLogEventSchema.Operation] = value;
			}
		}

		// Token: 0x17001DCB RID: 7627
		// (get) Token: 0x060057F1 RID: 22513 RVA: 0x00139FB6 File Offset: 0x001381B6
		// (set) Token: 0x060057F2 RID: 22514 RVA: 0x00139FCD File Offset: 0x001381CD
		public string OperationResult
		{
			get
			{
				return this.propertyBag[MailboxAuditLogEventSchema.OperationResult] as string;
			}
			private set
			{
				this.propertyBag[MailboxAuditLogEventSchema.OperationResult] = value;
			}
		}

		// Token: 0x17001DCC RID: 7628
		// (get) Token: 0x060057F3 RID: 22515 RVA: 0x00139FE0 File Offset: 0x001381E0
		// (set) Token: 0x060057F4 RID: 22516 RVA: 0x00139FF7 File Offset: 0x001381F7
		public string LogonType
		{
			get
			{
				return this.propertyBag[MailboxAuditLogEventSchema.LogonType] as string;
			}
			private set
			{
				this.propertyBag[MailboxAuditLogEventSchema.LogonType] = value;
			}
		}

		// Token: 0x17001DCD RID: 7629
		// (get) Token: 0x060057F5 RID: 22517 RVA: 0x0013A00A File Offset: 0x0013820A
		// (set) Token: 0x060057F6 RID: 22518 RVA: 0x0013A026 File Offset: 0x00138226
		public bool? ExternalAccess
		{
			get
			{
				return this.propertyBag[MailboxAuditLogEventSchema.ExternalAccess] as bool?;
			}
			private set
			{
				this.propertyBag[MailboxAuditLogEventSchema.ExternalAccess] = value;
			}
		}

		// Token: 0x17001DCE RID: 7630
		// (get) Token: 0x060057F7 RID: 22519 RVA: 0x0013A03E File Offset: 0x0013823E
		// (set) Token: 0x060057F8 RID: 22520 RVA: 0x0013A055 File Offset: 0x00138255
		public string DestFolderId
		{
			get
			{
				return this.propertyBag[MailboxAuditLogEventSchema.DestFolderId] as string;
			}
			private set
			{
				this.propertyBag[MailboxAuditLogEventSchema.DestFolderId] = value;
			}
		}

		// Token: 0x17001DCF RID: 7631
		// (get) Token: 0x060057F9 RID: 22521 RVA: 0x0013A068 File Offset: 0x00138268
		// (set) Token: 0x060057FA RID: 22522 RVA: 0x0013A07F File Offset: 0x0013827F
		public string DestFolderPathName
		{
			get
			{
				return this.propertyBag[MailboxAuditLogEventSchema.DestFolderPathName] as string;
			}
			private set
			{
				this.propertyBag[MailboxAuditLogEventSchema.DestFolderPathName] = value;
			}
		}

		// Token: 0x17001DD0 RID: 7632
		// (get) Token: 0x060057FB RID: 22523 RVA: 0x0013A092 File Offset: 0x00138292
		// (set) Token: 0x060057FC RID: 22524 RVA: 0x0013A0A9 File Offset: 0x001382A9
		public string FolderId
		{
			get
			{
				return this.propertyBag[MailboxAuditLogEventSchema.FolderId] as string;
			}
			private set
			{
				this.propertyBag[MailboxAuditLogEventSchema.FolderId] = value;
			}
		}

		// Token: 0x17001DD1 RID: 7633
		// (get) Token: 0x060057FD RID: 22525 RVA: 0x0013A0BC File Offset: 0x001382BC
		// (set) Token: 0x060057FE RID: 22526 RVA: 0x0013A0D3 File Offset: 0x001382D3
		public string FolderPathName
		{
			get
			{
				return this.propertyBag[MailboxAuditLogEventSchema.FolderPathName] as string;
			}
			private set
			{
				this.propertyBag[MailboxAuditLogEventSchema.FolderPathName] = value;
			}
		}

		// Token: 0x17001DD2 RID: 7634
		// (get) Token: 0x060057FF RID: 22527 RVA: 0x0013A0E6 File Offset: 0x001382E6
		// (set) Token: 0x06005800 RID: 22528 RVA: 0x0013A0FD File Offset: 0x001382FD
		public string ClientInfoString
		{
			get
			{
				return this.propertyBag[MailboxAuditLogEventSchema.ClientInfoString] as string;
			}
			private set
			{
				this.propertyBag[MailboxAuditLogEventSchema.ClientInfoString] = value;
			}
		}

		// Token: 0x17001DD3 RID: 7635
		// (get) Token: 0x06005801 RID: 22529 RVA: 0x0013A110 File Offset: 0x00138310
		// (set) Token: 0x06005802 RID: 22530 RVA: 0x0013A127 File Offset: 0x00138327
		public string ClientIPAddress
		{
			get
			{
				return this.propertyBag[MailboxAuditLogEventSchema.ClientIPAddress] as string;
			}
			private set
			{
				this.propertyBag[MailboxAuditLogEventSchema.ClientIPAddress] = value;
			}
		}

		// Token: 0x17001DD4 RID: 7636
		// (get) Token: 0x06005803 RID: 22531 RVA: 0x0013A13A File Offset: 0x0013833A
		// (set) Token: 0x06005804 RID: 22532 RVA: 0x0013A151 File Offset: 0x00138351
		public string ClientMachineName
		{
			get
			{
				return this.propertyBag[MailboxAuditLogEventSchema.ClientMachineName] as string;
			}
			private set
			{
				this.propertyBag[MailboxAuditLogEventSchema.ClientMachineName] = value;
			}
		}

		// Token: 0x17001DD5 RID: 7637
		// (get) Token: 0x06005805 RID: 22533 RVA: 0x0013A164 File Offset: 0x00138364
		// (set) Token: 0x06005806 RID: 22534 RVA: 0x0013A17B File Offset: 0x0013837B
		public string ClientProcessName
		{
			get
			{
				return this.propertyBag[MailboxAuditLogEventSchema.ClientProcessName] as string;
			}
			private set
			{
				this.propertyBag[MailboxAuditLogEventSchema.ClientProcessName] = value;
			}
		}

		// Token: 0x17001DD6 RID: 7638
		// (get) Token: 0x06005807 RID: 22535 RVA: 0x0013A18E File Offset: 0x0013838E
		// (set) Token: 0x06005808 RID: 22536 RVA: 0x0013A1A5 File Offset: 0x001383A5
		public string ClientVersion
		{
			get
			{
				return this.propertyBag[MailboxAuditLogEventSchema.ClientVersion] as string;
			}
			private set
			{
				this.propertyBag[MailboxAuditLogEventSchema.ClientVersion] = value;
			}
		}

		// Token: 0x17001DD7 RID: 7639
		// (get) Token: 0x06005809 RID: 22537 RVA: 0x0013A1B8 File Offset: 0x001383B8
		// (set) Token: 0x0600580A RID: 22538 RVA: 0x0013A1CF File Offset: 0x001383CF
		public string InternalLogonType
		{
			get
			{
				return this.propertyBag[MailboxAuditLogEventSchema.InternalLogonType] as string;
			}
			private set
			{
				this.propertyBag[MailboxAuditLogEventSchema.InternalLogonType] = value;
			}
		}

		// Token: 0x17001DD8 RID: 7640
		// (get) Token: 0x0600580B RID: 22539 RVA: 0x0013A1E2 File Offset: 0x001383E2
		// (set) Token: 0x0600580C RID: 22540 RVA: 0x0013A1F9 File Offset: 0x001383F9
		public string MailboxOwnerUPN
		{
			get
			{
				return this.propertyBag[MailboxAuditLogEventSchema.MailboxOwnerUPN] as string;
			}
			private set
			{
				this.propertyBag[MailboxAuditLogEventSchema.MailboxOwnerUPN] = value;
			}
		}

		// Token: 0x17001DD9 RID: 7641
		// (get) Token: 0x0600580D RID: 22541 RVA: 0x0013A20C File Offset: 0x0013840C
		// (set) Token: 0x0600580E RID: 22542 RVA: 0x0013A223 File Offset: 0x00138423
		public string MailboxOwnerSid
		{
			get
			{
				return this.propertyBag[MailboxAuditLogEventSchema.MailboxOwnerSid] as string;
			}
			private set
			{
				this.propertyBag[MailboxAuditLogEventSchema.MailboxOwnerSid] = value;
			}
		}

		// Token: 0x17001DDA RID: 7642
		// (get) Token: 0x0600580F RID: 22543 RVA: 0x0013A236 File Offset: 0x00138436
		// (set) Token: 0x06005810 RID: 22544 RVA: 0x0013A24D File Offset: 0x0013844D
		public string DestMailboxOwnerUPN
		{
			get
			{
				return this.propertyBag[MailboxAuditLogEventSchema.DestMailboxOwnerUPN] as string;
			}
			private set
			{
				this.propertyBag[MailboxAuditLogEventSchema.DestMailboxOwnerUPN] = value;
			}
		}

		// Token: 0x17001DDB RID: 7643
		// (get) Token: 0x06005811 RID: 22545 RVA: 0x0013A260 File Offset: 0x00138460
		// (set) Token: 0x06005812 RID: 22546 RVA: 0x0013A277 File Offset: 0x00138477
		public string DestMailboxOwnerSid
		{
			get
			{
				return this.propertyBag[MailboxAuditLogEventSchema.DestMailboxOwnerSid] as string;
			}
			private set
			{
				this.propertyBag[MailboxAuditLogEventSchema.DestMailboxOwnerSid] = value;
			}
		}

		// Token: 0x17001DDC RID: 7644
		// (get) Token: 0x06005813 RID: 22547 RVA: 0x0013A28A File Offset: 0x0013848A
		// (set) Token: 0x06005814 RID: 22548 RVA: 0x0013A2A1 File Offset: 0x001384A1
		public string DestMailboxGuid
		{
			get
			{
				return this.propertyBag[MailboxAuditLogEventSchema.DestMailboxGuid] as string;
			}
			private set
			{
				this.propertyBag[MailboxAuditLogEventSchema.DestMailboxGuid] = value;
			}
		}

		// Token: 0x17001DDD RID: 7645
		// (get) Token: 0x06005815 RID: 22549 RVA: 0x0013A2B4 File Offset: 0x001384B4
		// (set) Token: 0x06005816 RID: 22550 RVA: 0x0013A2D0 File Offset: 0x001384D0
		public bool? CrossMailboxOperation
		{
			get
			{
				return this.propertyBag[MailboxAuditLogEventSchema.CrossMailboxOperation] as bool?;
			}
			private set
			{
				this.propertyBag[MailboxAuditLogEventSchema.CrossMailboxOperation] = value;
			}
		}

		// Token: 0x17001DDE RID: 7646
		// (get) Token: 0x06005817 RID: 22551 RVA: 0x0013A2E8 File Offset: 0x001384E8
		// (set) Token: 0x06005818 RID: 22552 RVA: 0x0013A2FF File Offset: 0x001384FF
		public string LogonUserDisplayName
		{
			get
			{
				return this.propertyBag[MailboxAuditLogEventSchema.LogonUserDisplayName] as string;
			}
			set
			{
				this.propertyBag[MailboxAuditLogEventSchema.LogonUserDisplayName] = value;
			}
		}

		// Token: 0x17001DDF RID: 7647
		// (get) Token: 0x06005819 RID: 22553 RVA: 0x0013A312 File Offset: 0x00138512
		// (set) Token: 0x0600581A RID: 22554 RVA: 0x0013A329 File Offset: 0x00138529
		public string LogonUserSid
		{
			get
			{
				return this.propertyBag[MailboxAuditLogEventSchema.LogonUserSid] as string;
			}
			private set
			{
				this.propertyBag[MailboxAuditLogEventSchema.LogonUserSid] = value;
			}
		}

		// Token: 0x17001DE0 RID: 7648
		// (get) Token: 0x0600581B RID: 22555 RVA: 0x0013A33C File Offset: 0x0013853C
		// (set) Token: 0x0600581C RID: 22556 RVA: 0x0013A353 File Offset: 0x00138553
		public MultiValuedProperty<MailboxAuditLogSourceItem> SourceItems
		{
			get
			{
				return this.propertyBag[MailboxAuditLogEventSchema.SourceItems] as MultiValuedProperty<MailboxAuditLogSourceItem>;
			}
			private set
			{
				this.propertyBag[MailboxAuditLogEventSchema.SourceItems] = value;
			}
		}

		// Token: 0x17001DE1 RID: 7649
		// (get) Token: 0x0600581D RID: 22557 RVA: 0x0013A366 File Offset: 0x00138566
		// (set) Token: 0x0600581E RID: 22558 RVA: 0x0013A37D File Offset: 0x0013857D
		public MultiValuedProperty<MailboxAuditLogSourceFolder> SourceFolders
		{
			get
			{
				return this.propertyBag[MailboxAuditLogEventSchema.SourceFolders] as MultiValuedProperty<MailboxAuditLogSourceFolder>;
			}
			private set
			{
				this.propertyBag[MailboxAuditLogEventSchema.SourceFolders] = value;
			}
		}

		// Token: 0x17001DE2 RID: 7650
		// (get) Token: 0x0600581F RID: 22559 RVA: 0x0013A390 File Offset: 0x00138590
		public string SourceItemIdsList
		{
			get
			{
				List<string> list = new List<string>();
				if (this.SourceItems != null)
				{
					foreach (MailboxAuditLogSourceItem mailboxAuditLogSourceItem in this.SourceItems.Added)
					{
						list.Add(mailboxAuditLogSourceItem.SourceItemId);
					}
				}
				return string.Join(",", list.ToArray());
			}
		}

		// Token: 0x17001DE3 RID: 7651
		// (get) Token: 0x06005820 RID: 22560 RVA: 0x0013A3EC File Offset: 0x001385EC
		public string SourceItemSubjectsList
		{
			get
			{
				List<string> list = new List<string>();
				if (this.SourceItems != null)
				{
					foreach (MailboxAuditLogSourceItem mailboxAuditLogSourceItem in this.SourceItems.Added)
					{
						list.Add(mailboxAuditLogSourceItem.SourceItemSubject);
					}
				}
				return string.Join(",", list.ToArray());
			}
		}

		// Token: 0x17001DE4 RID: 7652
		// (get) Token: 0x06005821 RID: 22561 RVA: 0x0013A448 File Offset: 0x00138648
		public string SourceItemFolderPathNamesList
		{
			get
			{
				List<string> list = new List<string>();
				if (this.SourceItems != null)
				{
					foreach (MailboxAuditLogSourceItem mailboxAuditLogSourceItem in this.SourceItems.Added)
					{
						string item = this.FixFolderPathName(mailboxAuditLogSourceItem.SourceItemFolderPathName);
						if (!list.Contains(item))
						{
							list.Add(item);
						}
					}
				}
				return string.Join(",", list.ToArray());
			}
		}

		// Token: 0x17001DE5 RID: 7653
		// (get) Token: 0x06005822 RID: 22562 RVA: 0x0013A4B8 File Offset: 0x001386B8
		public string SourceFolderIdsList
		{
			get
			{
				List<string> list = new List<string>();
				if (this.SourceFolders != null)
				{
					foreach (MailboxAuditLogSourceFolder mailboxAuditLogSourceFolder in this.SourceFolders.Added)
					{
						string item = this.FixFolderPathName(mailboxAuditLogSourceFolder.SourceFolderId);
						if (!list.Contains(item))
						{
							list.Add(item);
						}
					}
				}
				return string.Join(",", list.ToArray());
			}
		}

		// Token: 0x17001DE6 RID: 7654
		// (get) Token: 0x06005823 RID: 22563 RVA: 0x0013A528 File Offset: 0x00138728
		public string SourceFolderPathNamesList
		{
			get
			{
				List<string> list = new List<string>();
				if (this.SourceFolders != null)
				{
					foreach (MailboxAuditLogSourceFolder mailboxAuditLogSourceFolder in this.SourceFolders.Added)
					{
						string item = this.FixFolderPathName(mailboxAuditLogSourceFolder.SourceFolderPathName);
						if (!list.Contains(item))
						{
							list.Add(item);
						}
					}
				}
				return string.Join(",", list.ToArray());
			}
		}

		// Token: 0x17001DE7 RID: 7655
		// (get) Token: 0x06005824 RID: 22564 RVA: 0x0013A598 File Offset: 0x00138798
		// (set) Token: 0x06005825 RID: 22565 RVA: 0x0013A5AF File Offset: 0x001387AF
		public string ItemId
		{
			get
			{
				return this.propertyBag[MailboxAuditLogEventSchema.ItemId] as string;
			}
			private set
			{
				this.propertyBag[MailboxAuditLogEventSchema.ItemId] = value;
			}
		}

		// Token: 0x17001DE8 RID: 7656
		// (get) Token: 0x06005826 RID: 22566 RVA: 0x0013A5C2 File Offset: 0x001387C2
		// (set) Token: 0x06005827 RID: 22567 RVA: 0x0013A5D9 File Offset: 0x001387D9
		public string ItemSubject
		{
			get
			{
				return this.propertyBag[MailboxAuditLogEventSchema.ItemSubject] as string;
			}
			private set
			{
				this.propertyBag[MailboxAuditLogEventSchema.ItemSubject] = value;
			}
		}

		// Token: 0x17001DE9 RID: 7657
		// (get) Token: 0x06005828 RID: 22568 RVA: 0x0013A5EC File Offset: 0x001387EC
		// (set) Token: 0x06005829 RID: 22569 RVA: 0x0013A603 File Offset: 0x00138803
		public string DirtyProperties
		{
			get
			{
				return this.propertyBag[MailboxAuditLogEventSchema.DirtyProperties] as string;
			}
			private set
			{
				this.propertyBag[MailboxAuditLogEventSchema.DirtyProperties] = value;
			}
		}

		// Token: 0x17001DEA RID: 7658
		// (get) Token: 0x0600582A RID: 22570 RVA: 0x0013A616 File Offset: 0x00138816
		// (set) Token: 0x0600582B RID: 22571 RVA: 0x0013A62D File Offset: 0x0013882D
		public string OriginatingServer
		{
			get
			{
				return this.propertyBag[MailboxAuditLogEventSchema.OriginatingServer] as string;
			}
			private set
			{
				this.propertyBag[MailboxAuditLogEventSchema.OriginatingServer] = value;
			}
		}

		// Token: 0x17001DEB RID: 7659
		// (get) Token: 0x0600582C RID: 22572 RVA: 0x0013A640 File Offset: 0x00138840
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MailboxAuditLogEvent.schema;
			}
		}

		// Token: 0x0600582D RID: 22573 RVA: 0x0013A648 File Offset: 0x00138848
		private string FixFolderPathName(string folderName)
		{
			folderName = folderName.Trim();
			folderName = folderName.TrimStart(new char[]
			{
				'/',
				'\\'
			});
			return folderName;
		}

		// Token: 0x04003B65 RID: 15205
		private const string LabelValueSeparator = ":";

		// Token: 0x04003B66 RID: 15206
		private const string PowerShellSeparator = ",";

		// Token: 0x04003B67 RID: 15207
		private static readonly ObjectSchema schema = ObjectSchema.GetInstance<MailboxAuditLogEventSchema>();

		// Token: 0x02000730 RID: 1840
		internal static class Labels
		{
			// Token: 0x04003B68 RID: 15208
			public const string ClientInfoString = "ClientInfoString";

			// Token: 0x04003B69 RID: 15209
			public const string ClientIPAddress = "ClientIPAddress";

			// Token: 0x04003B6A RID: 15210
			public const string ClientMachineName = "ClientMachineName";

			// Token: 0x04003B6B RID: 15211
			public const string ClientProcessName = "ClientProcessName";

			// Token: 0x04003B6C RID: 15212
			public const string FolderPathName = "FolderPathName";

			// Token: 0x04003B6D RID: 15213
			public const string ClientVersion = "ClientVersion";

			// Token: 0x04003B6E RID: 15214
			public const string InternalLogonType = "InternalLogonType";

			// Token: 0x04003B6F RID: 15215
			public const string ExternalAccess = "ExternalAccess";

			// Token: 0x04003B70 RID: 15216
			public const string Operation = "Operation";

			// Token: 0x04003B71 RID: 15217
			public const string OperationResult = "OperationResult";

			// Token: 0x04003B72 RID: 15218
			public const string LogonType = "LogonType";

			// Token: 0x04003B73 RID: 15219
			public const string SourceFolderPathName = ".SourceFolderPathName";

			// Token: 0x04003B74 RID: 15220
			public const string SourceFolderId = ".SourceFolderId";

			// Token: 0x04003B75 RID: 15221
			public const string SourceItemSubject = ".SourceItemSubject";

			// Token: 0x04003B76 RID: 15222
			public const string SourceItemId = ".SourceItemId";

			// Token: 0x04003B77 RID: 15223
			public const string SourceItemFolderPathName = ".SourceItemFolderPathName";

			// Token: 0x04003B78 RID: 15224
			public const string MailboxOwnerUPN = "MailboxOwnerUPN";

			// Token: 0x04003B79 RID: 15225
			public const string MailboxOwnerSid = "MailboxOwnerSid";

			// Token: 0x04003B7A RID: 15226
			public const string MailboxGuid = "MailboxGuid";

			// Token: 0x04003B7B RID: 15227
			public const string LogonUserSid = "LogonUserSid";

			// Token: 0x04003B7C RID: 15228
			public const string LogonUserDisplayName = "LogonUserDisplayName";

			// Token: 0x04003B7D RID: 15229
			public const string DestFolderId = "DestFolderId";

			// Token: 0x04003B7E RID: 15230
			public const string DestFolderPathName = "DestFolderPathName";

			// Token: 0x04003B7F RID: 15231
			public const string FolderId = "FolderId";

			// Token: 0x04003B80 RID: 15232
			public const string ItemId = "ItemId";

			// Token: 0x04003B81 RID: 15233
			public const string ItemSubject = "ItemSubject";

			// Token: 0x04003B82 RID: 15234
			public const string DestinationFolderId = "DestinationFolderId";

			// Token: 0x04003B83 RID: 15235
			public const string DestinationFolderPathName = "DestinationFolderPathName";

			// Token: 0x04003B84 RID: 15236
			public const string CrossMailboxOperation = "CrossMailboxOperation";

			// Token: 0x04003B85 RID: 15237
			public const string DestMailboxGuid = "DestMailboxGuid";

			// Token: 0x04003B86 RID: 15238
			public const string DestMailboxOwnerUPN = "DestMailboxOwnerUPN";

			// Token: 0x04003B87 RID: 15239
			public const string DestMailboxOwnerSid = "DestMailboxOwnerSid";

			// Token: 0x04003B88 RID: 15240
			public const string DirtyProperties = "DirtyProperties";

			// Token: 0x04003B89 RID: 15241
			public const string OriginatingServer = "OriginatingServer";
		}
	}
}
