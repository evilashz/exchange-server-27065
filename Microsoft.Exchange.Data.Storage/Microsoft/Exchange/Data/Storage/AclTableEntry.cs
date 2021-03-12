using System;
using System.IO;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200079A RID: 1946
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AclTableEntry : FolderSecurity.AclTableEntry
	{
		// Token: 0x06004953 RID: 18771 RVA: 0x001337C9 File Offset: 0x001319C9
		internal AclTableEntry(long id, byte[] entryId, string name, MemberRights rights) : base(id, entryId, name, (FolderSecurity.ExchangeFolderRights)rights)
		{
		}

		// Token: 0x06004954 RID: 18772 RVA: 0x001337D6 File Offset: 0x001319D6
		private AclTableEntry(BinaryDeserializer deserializer) : base(deserializer.Reader)
		{
		}

		// Token: 0x06004955 RID: 18773 RVA: 0x001337E4 File Offset: 0x001319E4
		private AclTableEntry(BinaryReader deserializer) : base(deserializer)
		{
		}

		// Token: 0x170014FE RID: 5374
		// (get) Token: 0x06004956 RID: 18774 RVA: 0x001337ED File Offset: 0x001319ED
		public string MemberName
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x170014FF RID: 5375
		// (get) Token: 0x06004957 RID: 18775 RVA: 0x001337F5 File Offset: 0x001319F5
		public long MemberId
		{
			get
			{
				return base.RowId;
			}
		}

		// Token: 0x17001500 RID: 5376
		// (get) Token: 0x06004958 RID: 18776 RVA: 0x001337FD File Offset: 0x001319FD
		public byte[] MemberEntryId
		{
			get
			{
				return base.EntryId;
			}
		}

		// Token: 0x17001501 RID: 5377
		// (get) Token: 0x06004959 RID: 18777 RVA: 0x00133805 File Offset: 0x00131A05
		// (set) Token: 0x0600495A RID: 18778 RVA: 0x0013380D File Offset: 0x00131A0D
		public MemberRights MemberRights
		{
			get
			{
				return (MemberRights)base.Rights;
			}
			set
			{
				base.Rights = (FolderSecurity.ExchangeFolderRights)value;
			}
		}

		// Token: 0x0600495B RID: 18779 RVA: 0x00133816 File Offset: 0x00131A16
		internal static AclTableEntry Parse(BinaryDeserializer deserializer)
		{
			return new AclTableEntry(deserializer);
		}

		// Token: 0x0600495C RID: 18780 RVA: 0x0013381E File Offset: 0x00131A1E
		internal new static AclTableEntry Parse(BinaryReader deserializer)
		{
			return new AclTableEntry(deserializer);
		}

		// Token: 0x0600495D RID: 18781 RVA: 0x00133826 File Offset: 0x00131A26
		internal void Serialize(BinarySerializer serializer)
		{
			base.Serialize(serializer.Writer);
		}

		// Token: 0x0600495E RID: 18782 RVA: 0x00133834 File Offset: 0x00131A34
		internal void SetMemberId(long id)
		{
			base.RowId = id;
		}

		// Token: 0x0600495F RID: 18783 RVA: 0x0013383D File Offset: 0x00131A3D
		internal void SetMemberName(string name)
		{
			base.Name = name;
		}

		// Token: 0x0200079B RID: 1947
		internal class ModifyOperation
		{
			// Token: 0x06004960 RID: 18784 RVA: 0x00133846 File Offset: 0x00131A46
			internal ModifyOperation(ModifyTableOperationType operation, AclTableEntry tableEntry)
			{
				this.operation = operation;
				this.tableEntry = tableEntry;
			}

			// Token: 0x06004961 RID: 18785 RVA: 0x0013385C File Offset: 0x00131A5C
			public static AclTableEntry.ModifyOperation FromModifyTableOperation(ModifyTableOperation modifyTableOperation)
			{
				long? num = null;
				byte[] array = null;
				MemberRights? memberRights = null;
				string text = null;
				byte[] array2 = null;
				bool? flag = null;
				foreach (PropValue propValue in modifyTableOperation.Properties)
				{
					if (propValue.Property == PermissionSchema.MemberId)
					{
						num = new long?((long)propValue.Value);
					}
					else if (propValue.Property == PermissionSchema.MemberEntryId)
					{
						array = (byte[])propValue.Value;
					}
					else if (propValue.Property == PermissionSchema.MemberRights)
					{
						memberRights = new MemberRights?((MemberRights)((int)propValue.Value));
					}
					else if (propValue.Property == PermissionSchema.MemberName)
					{
						text = (string)propValue.Value;
					}
					else if (propValue.Property == PermissionSchema.MemberSecurityIdentifier)
					{
						array2 = (byte[])propValue.Value;
					}
					else
					{
						if (propValue.Property != PermissionSchema.MemberIsGroup)
						{
							throw new InvalidParamException(new LocalizedString("Unexpected property in modification entry"));
						}
						flag = new bool?((bool)propValue.Value);
					}
				}
				AclTableEntry aclTableEntry = null;
				switch (modifyTableOperation.Operation)
				{
				case ModifyTableOperationType.Add:
					if (array == null || memberRights == null || num != null || (text != null && (array2 == null || flag == null)))
					{
						throw new InvalidParamException(new LocalizedString("Invalid modification(add) entry"));
					}
					aclTableEntry = new AclTableEntry(0L, array, string.Empty, memberRights.Value);
					break;
				case ModifyTableOperationType.Modify:
					if (num == null || memberRights == null || array != null || text != null)
					{
						throw new InvalidParamException(new LocalizedString("Invalid modification(modify) entry"));
					}
					aclTableEntry = new AclTableEntry(num.Value, null, string.Empty, memberRights.Value);
					break;
				case ModifyTableOperationType.Remove:
					if (num == null || memberRights != null || array != null || text != null)
					{
						throw new InvalidParamException(new LocalizedString("Invalid modification(remove) entry"));
					}
					aclTableEntry = new AclTableEntry(num.Value, null, string.Empty, MemberRights.None);
					break;
				}
				return new AclTableEntry.ModifyOperation(modifyTableOperation.Operation, aclTableEntry);
			}

			// Token: 0x17001502 RID: 5378
			// (get) Token: 0x06004962 RID: 18786 RVA: 0x00133A90 File Offset: 0x00131C90
			public ModifyTableOperationType Operation
			{
				get
				{
					return this.operation;
				}
			}

			// Token: 0x17001503 RID: 5379
			// (get) Token: 0x06004963 RID: 18787 RVA: 0x00133A98 File Offset: 0x00131C98
			public AclTableEntry Entry
			{
				get
				{
					return this.tableEntry;
				}
			}

			// Token: 0x0400278E RID: 10126
			private readonly ModifyTableOperationType operation;

			// Token: 0x0400278F RID: 10127
			private readonly AclTableEntry tableEntry;
		}
	}
}
