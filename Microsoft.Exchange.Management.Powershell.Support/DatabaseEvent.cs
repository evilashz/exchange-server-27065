using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x0200002D RID: 45
	[Serializable]
	public sealed class DatabaseEvent : IConfigurable
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000A213 File Offset: 0x00008413
		ObjectId IConfigurable.Identity
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000A216 File Offset: 0x00008416
		ValidationError[] IConfigurable.Validate()
		{
			return new ValidationError[0];
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000A21E File Offset: 0x0000841E
		bool IConfigurable.IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000A221 File Offset: 0x00008421
		ObjectState IConfigurable.ObjectState
		{
			get
			{
				return ObjectState.New;
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000A224 File Offset: 0x00008424
		void IConfigurable.CopyChangesFrom(IConfigurable source)
		{
			DatabaseEvent databaseEvent = source as DatabaseEvent;
			if (databaseEvent == null)
			{
				throw new NotImplementedException(string.Format("Cannot copy changes from type {0}.", source.GetType()));
			}
			this.mapiEvent = databaseEvent.mapiEvent;
			this.databaseId = databaseEvent.databaseId;
			this.server = databaseEvent.server;
			this.isDatabaseCopyActive = databaseEvent.isDatabaseCopyActive;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000A281 File Offset: 0x00008481
		void IConfigurable.ResetChangeTracking()
		{
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000A283 File Offset: 0x00008483
		public DatabaseEvent()
		{
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000A28B File Offset: 0x0000848B
		internal DatabaseEvent(MapiEvent mapiEvent, DatabaseId databaseId, Server server, bool isDatabaseCopyActive)
		{
			this.Instantiate(mapiEvent, databaseId, server, isDatabaseCopyActive);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000A2A0 File Offset: 0x000084A0
		internal void Instantiate(MapiEvent mapiEvent, DatabaseId databaseId, Server server, bool isDatabaseCopyActive)
		{
			if (mapiEvent == null)
			{
				throw new ArgumentNullException("mapiEvent");
			}
			if (null == databaseId)
			{
				throw new ArgumentNullException("databaseId");
			}
			this.mapiEvent = mapiEvent;
			this.databaseId = databaseId;
			this.server = server;
			this.isDatabaseCopyActive = isDatabaseCopyActive;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000A2EC File Offset: 0x000084EC
		public long Counter
		{
			get
			{
				return this.mapiEvent.EventCounter;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000A2F9 File Offset: 0x000084F9
		public DateTime CreateTime
		{
			get
			{
				return this.mapiEvent.CreateTime;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000A306 File Offset: 0x00008506
		public string ItemType
		{
			get
			{
				return this.mapiEvent.ItemType.ToString();
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000A31D File Offset: 0x0000851D
		public string EventName
		{
			get
			{
				return this.mapiEvent.EventMask.ToString();
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000A334 File Offset: 0x00008534
		public string Flags
		{
			get
			{
				return this.mapiEvent.EventFlags.ToString();
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000A34C File Offset: 0x0000854C
		public Guid? MailboxGuid
		{
			get
			{
				if (Guid.Empty != this.mapiEvent.MailboxGuid)
				{
					return new Guid?(this.mapiEvent.MailboxGuid);
				}
				return null;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000A38A File Offset: 0x0000858A
		public string ObjectClass
		{
			get
			{
				return this.mapiEvent.ObjectClass;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000A398 File Offset: 0x00008598
		public MapiEntryId ItemEntryId
		{
			get
			{
				byte[] itemEntryId = this.mapiEvent.ItemEntryId;
				if (itemEntryId == null || 0 >= itemEntryId.Length)
				{
					return null;
				}
				return new MapiEntryId(itemEntryId);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000A3C4 File Offset: 0x000085C4
		public MapiEntryId ParentEntryId
		{
			get
			{
				byte[] parentEntryId = this.mapiEvent.ParentEntryId;
				if (parentEntryId == null || 0 >= parentEntryId.Length)
				{
					return null;
				}
				return new MapiEntryId(parentEntryId);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000A3F0 File Offset: 0x000085F0
		public MapiEntryId OldItemEntryId
		{
			get
			{
				byte[] oldItemEntryId = this.mapiEvent.OldItemEntryId;
				if (oldItemEntryId == null || 0 >= oldItemEntryId.Length)
				{
					return null;
				}
				return new MapiEntryId(oldItemEntryId);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000A41C File Offset: 0x0000861C
		public MapiEntryId OldParentEntryId
		{
			get
			{
				byte[] oldParentEntryId = this.mapiEvent.OldParentEntryId;
				if (oldParentEntryId == null || 0 >= oldParentEntryId.Length)
				{
					return null;
				}
				return new MapiEntryId(oldParentEntryId);
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000A446 File Offset: 0x00008646
		public long ItemCount
		{
			get
			{
				return this.mapiEvent.ItemCount;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000A453 File Offset: 0x00008653
		public long UnreadItemCount
		{
			get
			{
				return this.mapiEvent.UnreadItemCount;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000A460 File Offset: 0x00008660
		public ulong ExtendedFlags
		{
			get
			{
				return (ulong)this.mapiEvent.ExtendedEventFlags;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000A46D File Offset: 0x0000866D
		public string ClientCategory
		{
			get
			{
				return this.mapiEvent.ClientType.ToString();
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000A484 File Offset: 0x00008684
		public string PrincipalName
		{
			get
			{
				SecurityIdentifier sid = this.mapiEvent.Sid;
				if (null != sid)
				{
					if (this.principalName == null)
					{
						string arg;
						string arg2;
						if (DatabaseEvent.GetAccountNameAndType(sid, out arg, out arg2))
						{
							this.principalName = string.Format("{0}\\{1}", arg, arg2);
							if (SuppressingPiiContext.NeedPiiSuppression)
							{
								this.principalName = SuppressingPiiData.Redact(this.principalName);
							}
						}
						else
						{
							this.principalName = sid.ToString();
						}
					}
					return this.principalName;
				}
				return null;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000A4FA File Offset: 0x000086FA
		public SecurityIdentifier PrincipalSid
		{
			get
			{
				return this.mapiEvent.Sid;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000A507 File Offset: 0x00008707
		public Server Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000A50F File Offset: 0x0000870F
		public DatabaseId Database
		{
			get
			{
				return this.databaseId;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000A517 File Offset: 0x00008717
		public bool IsDatabaseCopyActive
		{
			get
			{
				return this.isDatabaseCopyActive;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000A51F File Offset: 0x0000871F
		public long DocumentId
		{
			get
			{
				return (long)this.mapiEvent.DocumentId;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000A530 File Offset: 0x00008730
		public Guid? UnifiedMailboxGuid
		{
			get
			{
				if (Guid.Empty != this.mapiEvent.UnifiedMailboxGuid)
				{
					return new Guid?(this.mapiEvent.UnifiedMailboxGuid);
				}
				return null;
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000A570 File Offset: 0x00008770
		private static bool GetAccountNameAndType(SecurityIdentifier sid, out string domainName, out string accountName)
		{
			string systemName = null;
			byte[] array = new byte[sid.BinaryLength];
			sid.GetBinaryForm(array, 0);
			uint capacity = 64U;
			uint capacity2 = 64U;
			StringBuilder stringBuilder = new StringBuilder((int)capacity);
			StringBuilder stringBuilder2 = new StringBuilder((int)capacity2);
			int num = 0;
			int num2;
			if (!DatabaseEvent.LookupAccountSid(systemName, array, stringBuilder, ref capacity, stringBuilder2, ref capacity2, out num2) && (num = Marshal.GetLastWin32Error()) == 122)
			{
				stringBuilder = new StringBuilder((int)capacity);
				stringBuilder2 = new StringBuilder((int)capacity2);
				DatabaseEvent.LookupAccountSid(systemName, array, stringBuilder, ref capacity, stringBuilder2, ref capacity2, out num2);
				num = Marshal.GetLastWin32Error();
			}
			if (num == 0)
			{
				domainName = stringBuilder2.ToString();
				accountName = stringBuilder.ToString();
				return true;
			}
			if (num == 8)
			{
				throw MapiExceptionHelper.OutOfMemoryException("LookupAccountSid failure.");
			}
			accountName = "=unknown=";
			domainName = "=unknown=";
			return false;
		}

		// Token: 0x06000244 RID: 580
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "LookupAccountSidW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool LookupAccountSid(string systemName, byte[] sid, StringBuilder accountName, ref uint accountNameLength, StringBuilder domainName, ref uint domainNameLength, out int usage);

		// Token: 0x040000C7 RID: 199
		private MapiEvent mapiEvent;

		// Token: 0x040000C8 RID: 200
		private string principalName;

		// Token: 0x040000C9 RID: 201
		private DatabaseId databaseId;

		// Token: 0x040000CA RID: 202
		private Server server;

		// Token: 0x040000CB RID: 203
		private bool isDatabaseCopyActive;
	}
}
