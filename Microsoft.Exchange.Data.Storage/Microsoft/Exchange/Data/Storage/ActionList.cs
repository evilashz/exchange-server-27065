using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B6D RID: 2925
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ActionList : IList<ActionBase>, ICollection<ActionBase>, IEnumerable<ActionBase>, IEnumerable
	{
		// Token: 0x060069E9 RID: 27113 RVA: 0x001C54AC File Offset: 0x001C36AC
		public ActionList(Rule rule)
		{
			this.internalList = new List<ActionBase>();
			this.rule = rule;
		}

		// Token: 0x060069EA RID: 27114 RVA: 0x001C54C6 File Offset: 0x001C36C6
		int IList<ActionBase>.IndexOf(ActionBase action)
		{
			return this.internalList.IndexOf(action);
		}

		// Token: 0x060069EB RID: 27115 RVA: 0x001C54D4 File Offset: 0x001C36D4
		void IList<ActionBase>.Insert(int index, ActionBase action)
		{
			this.CheckForDuplicate(action);
			this.internalList.Insert(index, action);
			this.rule.SetDirty();
		}

		// Token: 0x060069EC RID: 27116 RVA: 0x001C54F5 File Offset: 0x001C36F5
		void IList<ActionBase>.RemoveAt(int index)
		{
			this.internalList.RemoveAt(index);
			this.rule.SetDirty();
		}

		// Token: 0x17001CF6 RID: 7414
		ActionBase IList<ActionBase>.this[int index]
		{
			get
			{
				return this.internalList[index];
			}
			set
			{
				if (value.ActionType != this.internalList[index].ActionType)
				{
					this.CheckForDuplicate(value);
				}
				this.internalList[index] = value;
				this.rule.SetDirty();
			}
		}

		// Token: 0x060069EF RID: 27119 RVA: 0x001C5563 File Offset: 0x001C3763
		void ICollection<ActionBase>.Add(ActionBase action)
		{
			this.CheckForDuplicate(action);
			this.internalList.Add(action);
			this.rule.SetDirty();
		}

		// Token: 0x060069F0 RID: 27120 RVA: 0x001C5583 File Offset: 0x001C3783
		void ICollection<ActionBase>.Clear()
		{
			this.internalList.Clear();
			this.rule.SetDirty();
		}

		// Token: 0x060069F1 RID: 27121 RVA: 0x001C559B File Offset: 0x001C379B
		bool ICollection<ActionBase>.Contains(ActionBase action)
		{
			return this.internalList.Contains(action);
		}

		// Token: 0x060069F2 RID: 27122 RVA: 0x001C55A9 File Offset: 0x001C37A9
		void ICollection<ActionBase>.CopyTo(ActionBase[] actions, int index)
		{
			this.internalList.CopyTo(actions, index);
		}

		// Token: 0x060069F3 RID: 27123 RVA: 0x001C55B8 File Offset: 0x001C37B8
		bool ICollection<ActionBase>.Remove(ActionBase action)
		{
			this.rule.SetDirty();
			return this.internalList.Remove(action);
		}

		// Token: 0x17001CF7 RID: 7415
		// (get) Token: 0x060069F4 RID: 27124 RVA: 0x001C55D1 File Offset: 0x001C37D1
		int ICollection<ActionBase>.Count
		{
			get
			{
				return this.internalList.Count;
			}
		}

		// Token: 0x17001CF8 RID: 7416
		// (get) Token: 0x060069F5 RID: 27125 RVA: 0x001C55DE File Offset: 0x001C37DE
		bool ICollection<ActionBase>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060069F6 RID: 27126 RVA: 0x001C55E1 File Offset: 0x001C37E1
		IEnumerator<ActionBase> IEnumerable<ActionBase>.GetEnumerator()
		{
			return this.internalList.GetEnumerator();
		}

		// Token: 0x060069F7 RID: 27127 RVA: 0x001C55F3 File Offset: 0x001C37F3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.internalList.GetEnumerator();
		}

		// Token: 0x060069F8 RID: 27128 RVA: 0x001C5605 File Offset: 0x001C3805
		public ActionBase[] ToArray()
		{
			return this.internalList.ToArray();
		}

		// Token: 0x060069F9 RID: 27129 RVA: 0x001C5638 File Offset: 0x001C3838
		private void CheckForDuplicate(ActionBase newAction)
		{
			foreach (ActionBase actionBase in ((IEnumerable<ActionBase>)this))
			{
				if (actionBase.ActionType == newAction.ActionType)
				{
					this.rule.ThrowValidateException(delegate
					{
						throw new DuplicateActionException(ServerStrings.DuplicateAction);
					}, ServerStrings.DuplicateAction);
				}
				else if (newAction.ActionType == ActionType.DeleteAction)
				{
					if (actionBase.ActionType == ActionType.MoveToFolderAction)
					{
						MoveToFolderAction moveToFolderAction = actionBase as MoveToFolderAction;
						MailboxSession mailboxSession = this.rule.Folder.Session as MailboxSession;
						if (mailboxSession != null && moveToFolderAction != null && moveToFolderAction.Id.Equals(mailboxSession.GetDefaultFolderId(DefaultFolderType.DeletedItems)))
						{
							this.rule.ThrowValidateException(delegate
							{
								throw new DuplicateActionException(ServerStrings.DuplicateAction);
							}, ServerStrings.DuplicateAction);
						}
					}
				}
				else if (newAction.ActionType == ActionType.MoveToFolderAction)
				{
					MoveToFolderAction moveToFolderAction2 = newAction as MoveToFolderAction;
					MailboxSession mailboxSession2 = this.rule.Folder.Session as MailboxSession;
					if (mailboxSession2 != null && moveToFolderAction2.Id.Equals(mailboxSession2.GetDefaultFolderId(DefaultFolderType.DeletedItems)) && actionBase.ActionType == ActionType.DeleteAction)
					{
						this.rule.ThrowValidateException(delegate
						{
							throw new DuplicateActionException(ServerStrings.DuplicateAction);
						}, ServerStrings.DuplicateAction);
					}
				}
			}
		}

		// Token: 0x04003C3B RID: 15419
		private List<ActionBase> internalList;

		// Token: 0x04003C3C RID: 15420
		private Rule rule;
	}
}
