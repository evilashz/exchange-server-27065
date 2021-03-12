using System;
using System.Collections.Generic;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000003 RID: 3
	internal class FolderACL
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public FolderACL(PropValueData[][] aclData)
		{
			this.aces = new List<FolderACL.FolderACE>();
			this.defaultACE = null;
			this.anonymousACE = null;
			this.byEntryId = new EntryIdMap<FolderACL.FolderACE>();
			if (aclData != null)
			{
				int i = 0;
				while (i < aclData.Length)
				{
					PropValueData[] aceData = aclData[i];
					FolderACL.FolderACE folderACE = new FolderACL.FolderACE(aceData);
					switch (folderACE.AceType)
					{
					case FolderACEType.Invalid:
						IL_8F:
						i++;
						continue;
					case FolderACEType.Regular:
						this.byEntryId[folderACE.MemberEntryId] = folderACE;
						break;
					case FolderACEType.Default:
						this.defaultACE = folderACE;
						break;
					case FolderACEType.Anonymous:
						this.anonymousACE = folderACE;
						break;
					}
					this.aces.Add(folderACE);
					goto IL_8F;
				}
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002176 File Offset: 0x00000376
		public List<FolderACL.FolderACE> Aces
		{
			get
			{
				return this.aces;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000217E File Offset: 0x0000037E
		public FolderACL.FolderACE DefaultACE
		{
			get
			{
				return this.defaultACE;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002186 File Offset: 0x00000386
		public FolderACL.FolderACE AnonymousACE
		{
			get
			{
				return this.anonymousACE;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000218E File Offset: 0x0000038E
		public EntryIdMap<FolderACL.FolderACE> ByEntryId
		{
			get
			{
				return this.byEntryId;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002198 File Offset: 0x00000398
		public FolderACL.FolderACE FindMatchingACE(FolderACL.FolderACE ace)
		{
			switch (ace.AceType)
			{
			case FolderACEType.Regular:
			{
				FolderACL.FolderACE result;
				if (this.ByEntryId.TryGetValue(ace.MemberEntryId, out result))
				{
					return result;
				}
				break;
			}
			case FolderACEType.Default:
				return this.DefaultACE;
			case FolderACEType.Anonymous:
				return this.AnonymousACE;
			}
			return null;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021E8 File Offset: 0x000003E8
		public RowEntry[] UpdateExisting(PropValueData[][] existingAclData)
		{
			FolderACL folderACL = new FolderACL(existingAclData);
			List<RowEntry> list = new List<RowEntry>();
			foreach (FolderACL.FolderACE folderACE in folderACL.Aces)
			{
				if (this.FindMatchingACE(folderACE) == null)
				{
					list.Add(FolderACL.FolderACE.Remove(folderACE.MemberId));
				}
			}
			foreach (FolderACL.FolderACE folderACE2 in this.Aces)
			{
				FolderACL.FolderACE folderACE3 = folderACL.FindMatchingACE(folderACE2);
				if (folderACE3 != null)
				{
					if (folderACE2.MemberRights != folderACE3.MemberRights)
					{
						list.Add(FolderACL.FolderACE.Update(folderACE3.MemberId, folderACE2.MemberRights));
					}
				}
				else
				{
					list.Add(FolderACL.FolderACE.Add(folderACE2.MemberEntryId, folderACE2.MemberRights));
				}
			}
			if (list.Count <= 0)
			{
				return null;
			}
			return list.ToArray();
		}

		// Token: 0x04000006 RID: 6
		private List<FolderACL.FolderACE> aces;

		// Token: 0x04000007 RID: 7
		private FolderACL.FolderACE defaultACE;

		// Token: 0x04000008 RID: 8
		private FolderACL.FolderACE anonymousACE;

		// Token: 0x04000009 RID: 9
		private EntryIdMap<FolderACL.FolderACE> byEntryId;

		// Token: 0x02000004 RID: 4
		public class FolderACE
		{
			// Token: 0x06000008 RID: 8 RVA: 0x00002300 File Offset: 0x00000500
			public FolderACE(PropValueData[] aceData)
			{
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				if (aceData != null)
				{
					foreach (PropValueData propValueData in aceData)
					{
						PropTag propTag = (PropTag)propValueData.PropTag;
						if (propTag != PropTag.EntryId)
						{
							if (propTag != PropTag.MemberId)
							{
								if (propTag == PropTag.MemberRights)
								{
									this.memberRights = (int)propValueData.Value;
									flag3 = true;
								}
							}
							else
							{
								this.memberId = (long)propValueData.Value;
								flag2 = true;
							}
						}
						else
						{
							this.memberEntryId = (propValueData.Value as byte[]);
							flag = true;
						}
					}
				}
				if (!flag2 || !flag || !flag3)
				{
					this.aceType = FolderACEType.Invalid;
					return;
				}
				if (this.memberId == -1L)
				{
					this.aceType = FolderACEType.Anonymous;
					return;
				}
				if (this.memberEntryId == null || this.memberEntryId.Length == 0)
				{
					this.aceType = FolderACEType.Default;
					return;
				}
				this.aceType = FolderACEType.Regular;
			}

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000009 RID: 9 RVA: 0x000023DE File Offset: 0x000005DE
			public FolderACEType AceType
			{
				get
				{
					return this.aceType;
				}
			}

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x0600000A RID: 10 RVA: 0x000023E6 File Offset: 0x000005E6
			public long MemberId
			{
				get
				{
					return this.memberId;
				}
			}

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x0600000B RID: 11 RVA: 0x000023EE File Offset: 0x000005EE
			public byte[] MemberEntryId
			{
				get
				{
					return this.memberEntryId;
				}
			}

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x0600000C RID: 12 RVA: 0x000023F6 File Offset: 0x000005F6
			public int MemberRights
			{
				get
				{
					return this.memberRights;
				}
			}

			// Token: 0x0600000D RID: 13 RVA: 0x00002400 File Offset: 0x00000600
			public static RowEntry Update(long memberId, int rights)
			{
				PropValue[] propValues = new PropValue[]
				{
					new PropValue(PropTag.MemberId, memberId),
					new PropValue(PropTag.MemberRights, rights)
				};
				return RowEntry.Modify(propValues);
			}

			// Token: 0x0600000E RID: 14 RVA: 0x00002454 File Offset: 0x00000654
			public static RowEntry Remove(long memberId)
			{
				PropValue[] propValues = new PropValue[]
				{
					new PropValue(PropTag.MemberId, memberId)
				};
				return RowEntry.Remove(propValues);
			}

			// Token: 0x0600000F RID: 15 RVA: 0x0000248C File Offset: 0x0000068C
			public static RowEntry Add(byte[] memberEntryId, int rights)
			{
				PropValue[] propValues = new PropValue[]
				{
					new PropValue(PropTag.EntryId, memberEntryId),
					new PropValue(PropTag.MemberRights, rights)
				};
				return RowEntry.Add(propValues);
			}

			// Token: 0x0400000A RID: 10
			private FolderACEType aceType;

			// Token: 0x0400000B RID: 11
			private long memberId;

			// Token: 0x0400000C RID: 12
			private byte[] memberEntryId;

			// Token: 0x0400000D RID: 13
			private int memberRights;
		}
	}
}
