using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200068D RID: 1677
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DelegateUser : IEquatable<DelegateUser>
	{
		// Token: 0x0600449A RID: 17562 RVA: 0x00124BF4 File Offset: 0x00122DF4
		public static DelegateUser Create(IExchangePrincipal delegatePrincipal, IDictionary<DefaultFolderType, PermissionLevel> permissions)
		{
			if (delegatePrincipal == null)
			{
				throw new ArgumentNullException("delegatePrincipal");
			}
			if (delegatePrincipal.ObjectId.IsNullOrEmpty())
			{
				throw new ArgumentException("Incomplete ExchangePrincipal not valid for delegate", "delegatePrincipal");
			}
			if (permissions == null)
			{
				throw new ArgumentNullException("permissions");
			}
			foreach (PermissionLevel permissionLevel in permissions.Values)
			{
				if (!DelegateUser.DelegatePermissionsDictionary.IsSupportedPermissionLevel(permissionLevel))
				{
					throw new ArgumentException("The PermissionLevel is not valid for a DelegateUser", permissionLevel.ToString());
				}
			}
			return DelegateUser.InternalCreate(delegatePrincipal, permissions);
		}

		// Token: 0x0600449B RID: 17563 RVA: 0x00124C98 File Offset: 0x00122E98
		internal static DelegateUser InternalCreate(IExchangePrincipal delegatePrincipal, IDictionary<DefaultFolderType, PermissionLevel> permissions)
		{
			if (permissions == null)
			{
				throw new ArgumentNullException("permissions");
			}
			DelegateUser delegateUser = new DelegateUser(delegatePrincipal, new DelegateUser.DelegatePermissionsDictionary(permissions));
			if (delegatePrincipal != null)
			{
				delegateUser.name = delegatePrincipal.MailboxInfo.DisplayName;
			}
			return delegateUser;
		}

		// Token: 0x0600449C RID: 17564 RVA: 0x00124CD8 File Offset: 0x00122ED8
		internal static DelegateUser InternalCreate(string displayName, string primarySmtpAddress, IDictionary<DefaultFolderType, PermissionLevel> permissions)
		{
			if (permissions == null)
			{
				throw new ArgumentNullException("permissions cannot be null");
			}
			if (string.IsNullOrEmpty(primarySmtpAddress))
			{
				throw new ArgumentNullException("primarySmtpAddress cannot be null or empty");
			}
			if (string.IsNullOrEmpty(displayName))
			{
				throw new ArgumentNullException("displayName cannot be null or empty");
			}
			return new DelegateUser(null, new DelegateUser.DelegatePermissionsDictionary(permissions))
			{
				name = displayName,
				primarySmtpAddress = primarySmtpAddress
			};
		}

		// Token: 0x0600449D RID: 17565 RVA: 0x00124D35 File Offset: 0x00122F35
		private DelegateUser(IExchangePrincipal delegatePrincipal, DelegateUser.DelegatePermissionsDictionary permissions)
		{
			this.delegatePrincipal = delegatePrincipal;
			this.permissions = permissions;
		}

		// Token: 0x170013EF RID: 5103
		// (get) Token: 0x0600449E RID: 17566 RVA: 0x00124D4B File Offset: 0x00122F4B
		public IExchangePrincipal Delegate
		{
			get
			{
				return this.delegatePrincipal;
			}
		}

		// Token: 0x170013F0 RID: 5104
		// (get) Token: 0x0600449F RID: 17567 RVA: 0x00124D54 File Offset: 0x00122F54
		public string PrimarySmtpAddress
		{
			get
			{
				if (this.delegatePrincipal == null)
				{
					return this.primarySmtpAddress;
				}
				return this.delegatePrincipal.MailboxInfo.PrimarySmtpAddress.ToString();
			}
		}

		// Token: 0x170013F1 RID: 5105
		// (get) Token: 0x060044A0 RID: 17568 RVA: 0x00124D8E File Offset: 0x00122F8E
		// (set) Token: 0x060044A1 RID: 17569 RVA: 0x00124D96 File Offset: 0x00122F96
		public string Name
		{
			get
			{
				return this.name;
			}
			internal set
			{
				this.name = value;
			}
		}

		// Token: 0x170013F2 RID: 5106
		// (get) Token: 0x060044A2 RID: 17570 RVA: 0x00124D9F File Offset: 0x00122F9F
		// (set) Token: 0x060044A3 RID: 17571 RVA: 0x00124DA7 File Offset: 0x00122FA7
		public bool ReceivesMeetingMessageCopies
		{
			get
			{
				return this.receivesMeetingMessageCopies;
			}
			set
			{
				this.receivesMeetingMessageCopies = value;
			}
		}

		// Token: 0x170013F3 RID: 5107
		// (get) Token: 0x060044A4 RID: 17572 RVA: 0x00124DB0 File Offset: 0x00122FB0
		// (set) Token: 0x060044A5 RID: 17573 RVA: 0x00124DB8 File Offset: 0x00122FB8
		public bool CanViewPrivateItems
		{
			get
			{
				return this.canViewPrivateItems;
			}
			set
			{
				this.canViewPrivateItems = value;
			}
		}

		// Token: 0x170013F4 RID: 5108
		// (get) Token: 0x060044A6 RID: 17574 RVA: 0x00124DC1 File Offset: 0x00122FC1
		public IDictionary<DefaultFolderType, PermissionLevel> FolderPermissions
		{
			get
			{
				return this.permissions;
			}
		}

		// Token: 0x170013F5 RID: 5109
		// (get) Token: 0x060044A7 RID: 17575 RVA: 0x00124DC9 File Offset: 0x00122FC9
		// (set) Token: 0x060044A8 RID: 17576 RVA: 0x00124DD1 File Offset: 0x00122FD1
		public DelegateProblems Problems
		{
			get
			{
				return this.problems;
			}
			internal set
			{
				this.problems = value;
			}
		}

		// Token: 0x060044A9 RID: 17577 RVA: 0x00124DDC File Offset: 0x00122FDC
		public bool Equals(DelegateUser other)
		{
			if (other == null)
			{
				return false;
			}
			if (other.Delegate == null)
			{
				return this.Delegate == null;
			}
			if (this.Delegate == null)
			{
				return false;
			}
			if (other.Problems == DelegateProblems.NoADUser || this.Problems == DelegateProblems.NoADUser)
			{
				return other.Problems == this.Problems && this.LegacyDistinguishedName != null && other.LegacyDistinguishedName != null && this.LegacyDistinguishedName.Equals(other.LegacyDistinguishedName, StringComparison.OrdinalIgnoreCase);
			}
			return this.Delegate.LegacyDn.Equals(other.Delegate.LegacyDn, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x170013F6 RID: 5110
		// (get) Token: 0x060044AA RID: 17578 RVA: 0x00124E6D File Offset: 0x0012306D
		// (set) Token: 0x060044AB RID: 17579 RVA: 0x00124E75 File Offset: 0x00123075
		public ADRecipient ADRecipient
		{
			get
			{
				return this.adRecipient;
			}
			set
			{
				this.adRecipient = value;
			}
		}

		// Token: 0x170013F7 RID: 5111
		// (get) Token: 0x060044AC RID: 17580 RVA: 0x00124E7E File Offset: 0x0012307E
		// (set) Token: 0x060044AD RID: 17581 RVA: 0x00124E86 File Offset: 0x00123086
		internal string LegacyDistinguishedName
		{
			get
			{
				return this.legacyDistinguishedName;
			}
			set
			{
				this.legacyDistinguishedName = value;
			}
		}

		// Token: 0x170013F8 RID: 5112
		// (get) Token: 0x060044AE RID: 17582 RVA: 0x00124E8F File Offset: 0x0012308F
		// (set) Token: 0x060044AF RID: 17583 RVA: 0x00124E97 File Offset: 0x00123097
		internal int Flags2
		{
			get
			{
				return this.flags2;
			}
			set
			{
				this.flags2 = value;
			}
		}

		// Token: 0x170013F9 RID: 5113
		// (get) Token: 0x060044B0 RID: 17584 RVA: 0x00124EA0 File Offset: 0x001230A0
		internal bool IsReceiveMeetingMessageCopiesValid
		{
			get
			{
				return !this.ReceivesMeetingMessageCopies || this.permissions.HasEditorCalendarRights;
			}
		}

		// Token: 0x060044B1 RID: 17585 RVA: 0x00124EB8 File Offset: 0x001230B8
		public void Validate()
		{
			if (!this.IsReceiveMeetingMessageCopiesValid)
			{
				if ((this.Problems & DelegateProblems.InvalidReceiveMeetingMessageCopies) == DelegateProblems.None)
				{
					ExTraceGlobals.DelegateTracer.TraceError<string>((long)this.GetHashCode(), "DelegateUser::Validate. An error occurred while validating. Delegate {0} isn't an editor on Calendar, so the delegate can't get meeting message copies.", this.Name);
					throw new InvalidReceiveMeetingMessageCopiesException(this.Name);
				}
				ExTraceGlobals.DelegateTracer.TraceDebug((long)this.GetHashCode(), "DelegateUser::Validate. ReceivesMeetingMessageCopies = false");
				this.ReceivesMeetingMessageCopies = false;
			}
		}

		// Token: 0x04002563 RID: 9571
		private string primarySmtpAddress;

		// Token: 0x04002564 RID: 9572
		private readonly IExchangePrincipal delegatePrincipal;

		// Token: 0x04002565 RID: 9573
		private string name;

		// Token: 0x04002566 RID: 9574
		private string legacyDistinguishedName;

		// Token: 0x04002567 RID: 9575
		private ADRecipient adRecipient;

		// Token: 0x04002568 RID: 9576
		private readonly DelegateUser.DelegatePermissionsDictionary permissions;

		// Token: 0x04002569 RID: 9577
		private bool receivesMeetingMessageCopies;

		// Token: 0x0400256A RID: 9578
		private bool canViewPrivateItems;

		// Token: 0x0400256B RID: 9579
		private int flags2;

		// Token: 0x0400256C RID: 9580
		private DelegateProblems problems;

		// Token: 0x0200068E RID: 1678
		internal class DelegatePermissionsDictionary : IDictionary<DefaultFolderType, PermissionLevel>, ICollection<KeyValuePair<DefaultFolderType, PermissionLevel>>, IEnumerable<KeyValuePair<DefaultFolderType, PermissionLevel>>, IEnumerable
		{
			// Token: 0x060044B2 RID: 17586 RVA: 0x00124F20 File Offset: 0x00123120
			internal DelegatePermissionsDictionary(IDictionary<DefaultFolderType, PermissionLevel> permissions)
			{
				foreach (KeyValuePair<DefaultFolderType, PermissionLevel> keyValuePair in permissions)
				{
					DelegateUser.DelegatePermissionsDictionary.Validate(keyValuePair.Key, keyValuePair.Value);
				}
				this.data = new Dictionary<DefaultFolderType, PermissionLevel>(permissions);
			}

			// Token: 0x170013FA RID: 5114
			// (get) Token: 0x060044B3 RID: 17587 RVA: 0x00124F88 File Offset: 0x00123188
			internal bool HasEditorCalendarRights
			{
				get
				{
					bool flag = this.ContainsKey(DefaultFolderType.Calendar) && this[DefaultFolderType.Calendar] == PermissionLevel.Editor;
					ExTraceGlobals.DelegateTracer.TraceDebug<bool>((long)this.GetHashCode(), "DelegateUser::HasEditorCalendarRights value: {0}", flag);
					return flag;
				}
			}

			// Token: 0x060044B4 RID: 17588 RVA: 0x00124FC4 File Offset: 0x001231C4
			private static void Validate(DefaultFolderType key, PermissionLevel value)
			{
				if (!DelegateUser.DelegatePermissionsDictionary.IsSupportedDefaultFolder(key))
				{
					ExTraceGlobals.DelegateTracer.TraceError<DefaultFolderType>(0L, "DelegateUser::Validate The folder is not supported for granting DelegateUser permissions. {0}", key);
					throw new ArgumentException("The folder is not supported for granting DelegateUser permissions.", key.ToString());
				}
			}

			// Token: 0x060044B5 RID: 17589 RVA: 0x00124FF8 File Offset: 0x001231F8
			private static bool IsSupportedDefaultFolder(DefaultFolderType folderType)
			{
				foreach (DefaultFolderType defaultFolderType in DelegateUserCollection.Folders)
				{
					if (folderType == defaultFolderType)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060044B6 RID: 17590 RVA: 0x00125028 File Offset: 0x00123228
			internal static bool IsSupportedPermissionLevel(PermissionLevel permission)
			{
				return permission == PermissionLevel.None || permission == PermissionLevel.Author || permission == PermissionLevel.Editor || permission == PermissionLevel.Reviewer;
			}

			// Token: 0x060044B7 RID: 17591 RVA: 0x0012503C File Offset: 0x0012323C
			public void Add(DefaultFolderType key, PermissionLevel value)
			{
				DelegateUser.DelegatePermissionsDictionary.Validate(key, value);
				this.data.Add(key, value);
			}

			// Token: 0x060044B8 RID: 17592 RVA: 0x00125052 File Offset: 0x00123252
			public bool ContainsKey(DefaultFolderType key)
			{
				return this.data.ContainsKey(key);
			}

			// Token: 0x060044B9 RID: 17593 RVA: 0x00125060 File Offset: 0x00123260
			public void Clear()
			{
				this.data.Clear();
			}

			// Token: 0x170013FB RID: 5115
			// (get) Token: 0x060044BA RID: 17594 RVA: 0x0012506D File Offset: 0x0012326D
			public int Count
			{
				get
				{
					return this.data.Count;
				}
			}

			// Token: 0x170013FC RID: 5116
			// (get) Token: 0x060044BB RID: 17595 RVA: 0x0012507A File Offset: 0x0012327A
			public ICollection<DefaultFolderType> Keys
			{
				get
				{
					return this.data.Keys;
				}
			}

			// Token: 0x060044BC RID: 17596 RVA: 0x00125087 File Offset: 0x00123287
			public bool Remove(DefaultFolderType key)
			{
				return this.data.Remove(key);
			}

			// Token: 0x060044BD RID: 17597 RVA: 0x00125095 File Offset: 0x00123295
			public bool TryGetValue(DefaultFolderType key, out PermissionLevel value)
			{
				return this.data.TryGetValue(key, out value);
			}

			// Token: 0x170013FD RID: 5117
			// (get) Token: 0x060044BE RID: 17598 RVA: 0x001250A4 File Offset: 0x001232A4
			public ICollection<PermissionLevel> Values
			{
				get
				{
					return this.data.Values;
				}
			}

			// Token: 0x170013FE RID: 5118
			public PermissionLevel this[DefaultFolderType key]
			{
				get
				{
					return this.data[key];
				}
				set
				{
					DelegateUser.DelegatePermissionsDictionary.Validate(key, value);
					this.data[key] = value;
				}
			}

			// Token: 0x060044C1 RID: 17601 RVA: 0x001250D5 File Offset: 0x001232D5
			public void Add(KeyValuePair<DefaultFolderType, PermissionLevel> item)
			{
				DelegateUser.DelegatePermissionsDictionary.Validate(item.Key, item.Value);
				((ICollection<KeyValuePair<DefaultFolderType, PermissionLevel>>)this.data).Add(item);
			}

			// Token: 0x060044C2 RID: 17602 RVA: 0x001250F6 File Offset: 0x001232F6
			public bool Contains(KeyValuePair<DefaultFolderType, PermissionLevel> item)
			{
				return ((ICollection<KeyValuePair<DefaultFolderType, PermissionLevel>>)this.data).Contains(item);
			}

			// Token: 0x060044C3 RID: 17603 RVA: 0x00125104 File Offset: 0x00123304
			public void CopyTo(KeyValuePair<DefaultFolderType, PermissionLevel>[] array, int arrayIndex)
			{
				((ICollection<KeyValuePair<DefaultFolderType, PermissionLevel>>)this.data).CopyTo(array, arrayIndex);
			}

			// Token: 0x170013FF RID: 5119
			// (get) Token: 0x060044C4 RID: 17604 RVA: 0x00125113 File Offset: 0x00123313
			public bool IsReadOnly
			{
				get
				{
					return ((ICollection<KeyValuePair<DefaultFolderType, PermissionLevel>>)this.data).IsReadOnly;
				}
			}

			// Token: 0x060044C5 RID: 17605 RVA: 0x00125120 File Offset: 0x00123320
			public bool Remove(KeyValuePair<DefaultFolderType, PermissionLevel> item)
			{
				return ((ICollection<KeyValuePair<DefaultFolderType, PermissionLevel>>)this.data).Remove(item);
			}

			// Token: 0x060044C6 RID: 17606 RVA: 0x0012512E File Offset: 0x0012332E
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.data.GetEnumerator();
			}

			// Token: 0x060044C7 RID: 17607 RVA: 0x00125140 File Offset: 0x00123340
			public IEnumerator<KeyValuePair<DefaultFolderType, PermissionLevel>> GetEnumerator()
			{
				return ((IEnumerable<KeyValuePair<DefaultFolderType, PermissionLevel>>)this.data).GetEnumerator();
			}

			// Token: 0x0400256D RID: 9581
			private Dictionary<DefaultFolderType, PermissionLevel> data;
		}
	}
}
