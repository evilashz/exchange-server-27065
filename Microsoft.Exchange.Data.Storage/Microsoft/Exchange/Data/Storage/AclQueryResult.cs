using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200079C RID: 1948
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AclQueryResult : DisposableObject, IQueryResult, IDisposable
	{
		// Token: 0x06004964 RID: 18788 RVA: 0x00133AA0 File Offset: 0x00131CA0
		internal AclQueryResult(StoreSession session, IList<AclTableEntry> tableEntries, bool allowExtendedPermissionInformationQuery, bool removeFreeBusyRights, ICollection<PropertyDefinition> columns)
		{
			this.session = session;
			this.tableEntries = tableEntries;
			this.allowExtendedPermissionInformationQuery = allowExtendedPermissionInformationQuery;
			this.removeFreeBusyRights = removeFreeBusyRights;
			this.columns = new List<PropertyDefinition>(columns);
		}

		// Token: 0x06004965 RID: 18789 RVA: 0x00133ADD File Offset: 0x00131CDD
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<AclQueryResult>(this);
		}

		// Token: 0x06004966 RID: 18790 RVA: 0x00133AE5 File Offset: 0x00131CE5
		protected override void InternalDispose(bool disposing)
		{
			base.InternalDispose(disposing);
		}

		// Token: 0x06004967 RID: 18791 RVA: 0x00133AEE File Offset: 0x00131CEE
		public object[][] GetRows(int rowCount, out bool mightBeMoreRows)
		{
			this.CheckDisposed(null);
			return this.GetRows(rowCount, QueryRowsFlags.None, out mightBeMoreRows);
		}

		// Token: 0x06004968 RID: 18792 RVA: 0x00133B00 File Offset: 0x00131D00
		public object[][] GetRows(int rowCount, QueryRowsFlags flags, out bool mightBeMoreRows)
		{
			this.CheckDisposed(null);
			EnumValidator.ThrowIfInvalid<QueryRowsFlags>(flags, "flags");
			List<object[]> list = new List<object[]>();
			int num = 0;
			int num2 = 0;
			while (num2 < rowCount && this.currentRowIndex + num < this.tableEntries.Count)
			{
				object[] array = new object[this.columns.Count];
				for (int i = 0; i < this.columns.Count; i++)
				{
					if (this.columns[i] == InternalSchema.InstanceKey)
					{
						array[i] = BitConverter.GetBytes(this.tableEntries[this.currentRowIndex + num].MemberId);
					}
					else if (this.columns[i] == PermissionSchema.MemberId)
					{
						array[i] = this.tableEntries[this.currentRowIndex + num].MemberId;
					}
					else if (this.columns[i] == PermissionSchema.MemberEntryId)
					{
						array[i] = this.tableEntries[this.currentRowIndex + num].MemberEntryId;
					}
					else if (this.columns[i] == PermissionSchema.MemberRights)
					{
						MemberRights memberRights = this.tableEntries[this.currentRowIndex + num].MemberRights;
						if (this.removeFreeBusyRights)
						{
							array[i] = (int)(memberRights & ~(MemberRights.FreeBusySimple | MemberRights.FreeBusyDetailed));
						}
						else
						{
							array[i] = (int)memberRights;
						}
					}
					else if (this.columns[i] == PermissionSchema.MemberName)
					{
						array[i] = this.tableEntries[this.currentRowIndex + num].MemberName;
					}
					else if (this.columns[i] == PermissionSchema.MemberSecurityIdentifier)
					{
						if (!this.allowExtendedPermissionInformationQuery)
						{
							throw new InvalidOperationException("QueryResult doesn't support MemberSecurityIdentifier property");
						}
						SecurityIdentifier securityIdentifier = this.tableEntries[this.currentRowIndex + num].SecurityIdentifier;
						byte[] array2 = new byte[securityIdentifier.BinaryLength];
						securityIdentifier.GetBinaryForm(array2, 0);
						array[i] = array2;
					}
					else if (this.columns[i] == PermissionSchema.MemberIsGroup)
					{
						if (!this.allowExtendedPermissionInformationQuery)
						{
							throw new InvalidOperationException("QueryResult doesn't support MemberIsGroup property");
						}
						array[i] = this.tableEntries[this.currentRowIndex + num].IsGroup;
					}
				}
				list.Add(array);
				num++;
				num2++;
			}
			if ((flags & QueryRowsFlags.NoAdvance) != QueryRowsFlags.NoAdvance)
			{
				this.currentRowIndex += num;
			}
			mightBeMoreRows = (list.Count > 0);
			return list.ToArray();
		}

		// Token: 0x06004969 RID: 18793 RVA: 0x00133D95 File Offset: 0x00131F95
		public void SetTableColumns(ICollection<PropertyDefinition> propertyDefinitions)
		{
			this.CheckDisposed(null);
			Util.ThrowOnNullArgument(propertyDefinitions, "propertyDefinitions");
			this.columns = new List<PropertyDefinition>(propertyDefinitions);
		}

		// Token: 0x0600496A RID: 18794 RVA: 0x00133DB8 File Offset: 0x00131FB8
		public int SeekToOffset(SeekReference reference, int offset)
		{
			this.CheckDisposed(null);
			EnumValidator.ThrowIfInvalid<SeekReference>(reference, "reference");
			reference &= ~SeekReference.SeekBackward;
			int num;
			if (reference == SeekReference.OriginBeginning)
			{
				num = 0;
			}
			else if (reference == SeekReference.OriginEnd)
			{
				num = ((this.tableEntries.Count > 0) ? this.tableEntries.Count : 0);
			}
			else
			{
				num = this.currentRowIndex;
			}
			int num2 = num + offset;
			if (offset > 0 && num2 >= this.tableEntries.Count)
			{
				num2 = this.tableEntries.Count;
			}
			else if (offset < 0 && num2 < 0)
			{
				num2 = 0;
			}
			int result = Math.Abs(num2 - this.currentRowIndex);
			this.currentRowIndex = num2;
			return result;
		}

		// Token: 0x0600496B RID: 18795 RVA: 0x00133E52 File Offset: 0x00132052
		public bool SeekToCondition(SeekReference reference, QueryFilter seekFilter, SeekToConditionFlags flags)
		{
			this.CheckDisposed(null);
			EnumValidator.ThrowIfInvalid<SeekReference>(reference, "reference");
			throw new NotSupportedException();
		}

		// Token: 0x0600496C RID: 18796 RVA: 0x00133E6B File Offset: 0x0013206B
		public bool SeekToCondition(SeekReference reference, QueryFilter seekFilter)
		{
			this.CheckDisposed(null);
			EnumValidator.ThrowIfInvalid<SeekReference>(reference, "reference");
			throw new NotSupportedException();
		}

		// Token: 0x0600496D RID: 18797 RVA: 0x00133E84 File Offset: 0x00132084
		public bool SeekToCondition(uint bookMark, bool useForwardDirection, QueryFilter seekFilter, SeekToConditionFlags flags)
		{
			this.CheckDisposed(null);
			Util.ThrowOnNullArgument(seekFilter, "seekFilter");
			EnumValidator.ThrowIfInvalid<SeekToConditionFlags>(flags, "flags");
			throw new NotSupportedException();
		}

		// Token: 0x0600496E RID: 18798 RVA: 0x00133EA9 File Offset: 0x001320A9
		public object[][] ExpandRow(int rowCount, long categoryId, out int rowsInExpandedCategory)
		{
			this.CheckDisposed(null);
			throw new NotSupportedException();
		}

		// Token: 0x0600496F RID: 18799 RVA: 0x00133EB7 File Offset: 0x001320B7
		public int CollapseRow(long categoryId)
		{
			this.CheckDisposed(null);
			throw new NotSupportedException();
		}

		// Token: 0x06004970 RID: 18800 RVA: 0x00133EC5 File Offset: 0x001320C5
		public uint CreateBookmark()
		{
			this.CheckDisposed(null);
			throw new NotSupportedException();
		}

		// Token: 0x06004971 RID: 18801 RVA: 0x00133ED3 File Offset: 0x001320D3
		public void FreeBookmark(uint bookmarkPosition)
		{
			this.CheckDisposed(null);
			throw new NotSupportedException();
		}

		// Token: 0x06004972 RID: 18802 RVA: 0x00133EE1 File Offset: 0x001320E1
		public int SeekRowBookmark(uint bookmarkPosition, int rowCount, bool wantRowsSought, out bool soughtLess, out bool positionChanged)
		{
			this.CheckDisposed(null);
			throw new NotSupportedException();
		}

		// Token: 0x06004973 RID: 18803 RVA: 0x00133EF0 File Offset: 0x001320F0
		public NativeStorePropertyDefinition[] GetAllPropertyDefinitions(params PropertyTagPropertyDefinition[] excludeProperties)
		{
			this.CheckDisposed(null);
			NativeStorePropertyDefinition[] array = this.allowExtendedPermissionInformationQuery ? AclQueryResult.availableExtendedPermissionQueryColumns : AclQueryResult.availableQueryColumns;
			if (excludeProperties != null && excludeProperties.Length != 0)
			{
				array = array.Except(excludeProperties).ToArray<NativeStorePropertyDefinition>();
			}
			return array;
		}

		// Token: 0x06004974 RID: 18804 RVA: 0x00133F2F File Offset: 0x0013212F
		public byte[] GetCollapseState(byte[] instanceKey)
		{
			this.CheckDisposed(null);
			Util.ThrowOnNullArgument(instanceKey, "instanceKey");
			throw new NotSupportedException();
		}

		// Token: 0x06004975 RID: 18805 RVA: 0x00133F48 File Offset: 0x00132148
		public uint SetCollapseState(byte[] collapseState)
		{
			this.CheckDisposed(null);
			Util.ThrowOnNullArgument(collapseState, "collapseState");
			throw new NotSupportedException();
		}

		// Token: 0x06004976 RID: 18806 RVA: 0x00133F61 File Offset: 0x00132161
		public object Advise(SubscriptionSink subscriptionSink, bool asyncMode)
		{
			this.CheckDisposed(null);
			Util.ThrowOnNullArgument(subscriptionSink, "subscriptionSink");
			throw new NotSupportedException();
		}

		// Token: 0x06004977 RID: 18807 RVA: 0x00133F7A File Offset: 0x0013217A
		public void Unadvise(object notificationHandle)
		{
			this.CheckDisposed(null);
			Util.ThrowOnNullArgument(notificationHandle, "notificationHandle");
			throw new NotSupportedException();
		}

		// Token: 0x06004978 RID: 18808 RVA: 0x00133F93 File Offset: 0x00132193
		public IStorePropertyBag[] GetPropertyBags(int rowCount)
		{
			this.CheckDisposed(null);
			throw new NotSupportedException();
		}

		// Token: 0x17001504 RID: 5380
		// (get) Token: 0x06004979 RID: 18809 RVA: 0x00133FA1 File Offset: 0x001321A1
		public StoreSession StoreSession
		{
			get
			{
				this.CheckDisposed(null);
				return this.session;
			}
		}

		// Token: 0x17001505 RID: 5381
		// (get) Token: 0x0600497A RID: 18810 RVA: 0x00133FB0 File Offset: 0x001321B0
		public ColumnPropertyDefinitions Columns
		{
			get
			{
				this.CheckDisposed(null);
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001506 RID: 5382
		// (get) Token: 0x0600497B RID: 18811 RVA: 0x00133FBE File Offset: 0x001321BE
		public int CurrentRow
		{
			get
			{
				this.CheckDisposed(null);
				return this.currentRowIndex;
			}
		}

		// Token: 0x17001507 RID: 5383
		// (get) Token: 0x0600497C RID: 18812 RVA: 0x00133FCD File Offset: 0x001321CD
		public int EstimatedRowCount
		{
			get
			{
				this.CheckDisposed(null);
				return this.tableEntries.Count;
			}
		}

		// Token: 0x17001508 RID: 5384
		// (get) Token: 0x0600497D RID: 18813 RVA: 0x00133FE1 File Offset: 0x001321E1
		public new bool IsDisposed
		{
			get
			{
				return base.IsDisposed;
			}
		}

		// Token: 0x04002790 RID: 10128
		private static readonly NativeStorePropertyDefinition[] availableQueryColumns = new NativeStorePropertyDefinition[]
		{
			InternalSchema.InstanceKey,
			InternalSchema.MemberId,
			InternalSchema.MemberName,
			InternalSchema.MemberEntryId,
			InternalSchema.MemberRights
		};

		// Token: 0x04002791 RID: 10129
		private static readonly NativeStorePropertyDefinition[] availableExtendedPermissionQueryColumns = new NativeStorePropertyDefinition[]
		{
			InternalSchema.InstanceKey,
			InternalSchema.MemberId,
			InternalSchema.MemberName,
			InternalSchema.MemberEntryId,
			InternalSchema.MemberRights,
			InternalSchema.MemberSecurityIdentifier,
			InternalSchema.MemberIsGroup
		};

		// Token: 0x04002792 RID: 10130
		private readonly StoreSession session;

		// Token: 0x04002793 RID: 10131
		private readonly IList<AclTableEntry> tableEntries;

		// Token: 0x04002794 RID: 10132
		private readonly bool allowExtendedPermissionInformationQuery;

		// Token: 0x04002795 RID: 10133
		private readonly bool removeFreeBusyRights;

		// Token: 0x04002796 RID: 10134
		private IList<PropertyDefinition> columns = Array<PropertyDefinition>.Empty;

		// Token: 0x04002797 RID: 10135
		private int currentRowIndex;
	}
}
