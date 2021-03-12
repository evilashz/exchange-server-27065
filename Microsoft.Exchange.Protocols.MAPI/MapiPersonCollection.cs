using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.Mapi;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000078 RID: 120
	public sealed class MapiPersonCollection : DisposableBase, IList<MapiPerson>, ICollection<MapiPerson>, IEnumerable<MapiPerson>, IEnumerable
	{
		// Token: 0x060003A5 RID: 933 RVA: 0x0001C4C4 File Offset: 0x0001A6C4
		internal MapiPersonCollection(MapiMessage message)
		{
			this.parentObject = message;
			this.people = new List<MapiPerson>();
			this.initialized = false;
			this.extraPropList = new List<StorePropTag>();
		}

		// Token: 0x17000089 RID: 137
		internal MapiPerson this[int index]
		{
			get
			{
				this.ThrowIfNotValid();
				this.Load();
				return this.people[index];
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001C50A File Offset: 0x0001A70A
		[Conditional("DEBUG")]
		private void AssertConsistency()
		{
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0001C50C File Offset: 0x0001A70C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiPersonCollection>(this);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0001C514 File Offset: 0x0001A714
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.people != null)
			{
				foreach (MapiPerson mapiPerson in this.people)
				{
					mapiPerson.Dispose();
				}
				this.people = null;
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0001C578 File Offset: 0x0001A778
		IEnumerator IEnumerable.GetEnumerator()
		{
			this.ThrowIfNotValid();
			this.Load();
			return this.people.GetEnumerator();
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0001C596 File Offset: 0x0001A796
		public int GetCount()
		{
			this.ThrowIfNotValid();
			this.Load();
			return this.people.Count;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0001C5B0 File Offset: 0x0001A7B0
		public int GetAliveCount()
		{
			this.ThrowIfNotValid();
			this.Load();
			int num = 0;
			foreach (MapiPerson mapiPerson in this.people)
			{
				if (!mapiPerson.IsDeleted)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0001C618 File Offset: 0x0001A818
		public void DeleteAll()
		{
			this.ThrowIfNotValid();
			this.Load();
			foreach (MapiPerson mapiPerson in this.people)
			{
				if (!mapiPerson.IsDeleted)
				{
					mapiPerson.Delete();
				}
			}
			this.extraPropList.Clear();
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0001C68C File Offset: 0x0001A88C
		public void SaveChanges(MapiContext context)
		{
			bool flag = true;
			this.ThrowIfNotValid();
			for (int i = 0; i < this.people.Count; i++)
			{
				MapiPerson mapiPerson = this.people[i];
				mapiPerson.SaveChanges(context);
				if (!mapiPerson.IsDeleted)
				{
					flag = false;
				}
			}
			if (flag)
			{
				this.extraPropList.Clear();
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0001C6E4 File Offset: 0x0001A8E4
		public MapiPerson GetItem(int rowId, bool createIfNotFound)
		{
			this.ThrowIfNotValid();
			this.Load();
			int num = this.IndexFromRowId(rowId);
			if (num >= 0 && (!this.people[num].IsDeleted || !createIfNotFound))
			{
				return this.people[num];
			}
			if (!createIfNotFound)
			{
				return null;
			}
			MapiPerson mapiPerson = new MapiPerson();
			MapiPerson result;
			try
			{
				Recipient storePerson = this.parentObject.CreateStorePerson(this.parentObject.CurrentOperationContext, rowId);
				mapiPerson.Configure(this.parentObject, rowId, storePerson);
				if (num < 0)
				{
					num = ~num;
					this.people.Insert(num, mapiPerson);
				}
				else
				{
					this.people[num].Dispose();
					this.people[num] = mapiPerson;
				}
				MapiPerson mapiPerson2 = mapiPerson;
				mapiPerson = null;
				result = mapiPerson2;
			}
			finally
			{
				if (mapiPerson != null)
				{
					mapiPerson.Dispose();
				}
			}
			return result;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0001C7B8 File Offset: 0x0001A9B8
		public void RemoveItem(int rowId)
		{
			this.ThrowIfNotValid();
			this.Load();
			MapiPerson item = this.GetItem(rowId, true);
			item.Delete();
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0001C7E0 File Offset: 0x0001A9E0
		public List<StorePropTag> GetRecipientPropListExtra()
		{
			this.ThrowIfNotValid();
			this.Load();
			return this.extraPropList;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0001C7F4 File Offset: 0x0001A9F4
		public void SetRecipientPropListExtra(List<StorePropTag> newList)
		{
			this.ThrowIfNotValid();
			this.Load();
			if (!this.VerifyExtraPropTags(newList))
			{
				throw new ExExceptionMapiRpcFormat((LID)33592U, "Extra recipient properties are invalid.");
			}
			if (newList.Count > this.extraPropList.Count)
			{
				this.extraPropList = newList;
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0001C848 File Offset: 0x0001AA48
		private bool VerifyExtraPropTags(IList<StorePropTag> newList)
		{
			bool result = true;
			int num = 0;
			while (num < this.extraPropList.Count && num < newList.Count)
			{
				if (this.extraPropList[num] != newList[num])
				{
					result = false;
					break;
				}
				num++;
			}
			return result;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0001C898 File Offset: 0x0001AA98
		private int IndexFromRowId(int rowId)
		{
			int i = 0;
			int num = this.people.Count - 1;
			while (i <= num)
			{
				int num2 = i + (num - i) / 2;
				int rowId2 = this.people[num2].GetRowId();
				if (rowId2 == rowId)
				{
					return num2;
				}
				if (rowId2 < rowId)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2 - 1;
				}
			}
			return ~i;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0001C8EB File Offset: 0x0001AAEB
		private int IndexOf(MapiPerson person)
		{
			return this.people.IndexOf(person);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0001C8F9 File Offset: 0x0001AAF9
		internal RecipientCollection GetRecipientCollection()
		{
			return this.parentObject.StoreRecipientCollection(this.parentObject.CurrentOperationContext);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0001C914 File Offset: 0x0001AB14
		private void Load()
		{
			if (this.initialized)
			{
				return;
			}
			if (this.parentObject.Mid.Counter == 0UL && this.parentObject.Mid.Guid == Guid.Empty && !this.parentObject.StoreMessage.IsEmbedded)
			{
				return;
			}
			if (this.people.Count == 0)
			{
				RecipientCollection recipientCollection = this.GetRecipientCollection();
				if (recipientCollection != null)
				{
					HashSet<StorePropTag> hashSet = new HashSet<StorePropTag>();
					for (int i = 0; i < recipientCollection.Count; i++)
					{
						MapiPerson mapiPerson = new MapiPerson();
						mapiPerson.Configure(this.parentObject, i, recipientCollection[i]);
						this.people.Add(mapiPerson);
						mapiPerson.CollectExtraProperties(this.parentObject.CurrentOperationContext, hashSet);
					}
					hashSet.Remove(PropTag.Recipient.EntryIdSvrEid);
					hashSet.ExceptWith(MapiPerson.GetRecipientPropListStandard());
					this.extraPropList = hashSet.ToList<StorePropTag>();
				}
			}
			this.initialized = true;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0001CA05 File Offset: 0x0001AC05
		private void ThrowIfNotValid()
		{
			if (this.parentObject == null || !this.parentObject.IsValid)
			{
				ExTraceGlobals.GeneralTracer.TraceError(0L, "Invalid MapiMessageBase for MapiPersonCollection.  Throwing ExExceptionInvalidParameter!");
				throw new ExExceptionInvalidParameter((LID)58168U, "The MapiMessageBase within the MapiRecipientCollection is null or invalid");
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0001CA42 File Offset: 0x0001AC42
		IEnumerator<MapiPerson> IEnumerable<MapiPerson>.GetEnumerator()
		{
			this.Load();
			return this.people.GetEnumerator();
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0001CA5A File Offset: 0x0001AC5A
		void ICollection<MapiPerson>.Add(MapiPerson item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0001CA61 File Offset: 0x0001AC61
		void ICollection<MapiPerson>.Clear()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0001CA68 File Offset: 0x0001AC68
		bool ICollection<MapiPerson>.Contains(MapiPerson item)
		{
			this.ThrowIfNotValid();
			this.Load();
			return this.people.Contains(item);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0001CA82 File Offset: 0x0001AC82
		void ICollection<MapiPerson>.CopyTo(MapiPerson[] array, int arrayIndex)
		{
			this.ThrowIfNotValid();
			this.Load();
			this.people.CopyTo(array, arrayIndex);
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0001CA9D File Offset: 0x0001AC9D
		int ICollection<MapiPerson>.Count
		{
			get
			{
				return this.GetCount();
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0001CAA5 File Offset: 0x0001ACA5
		bool ICollection<MapiPerson>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0001CAA8 File Offset: 0x0001ACA8
		bool ICollection<MapiPerson>.Remove(MapiPerson item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700008C RID: 140
		MapiPerson IList<MapiPerson>.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new System.NotSupportedException();
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0001CABF File Offset: 0x0001ACBF
		int IList<MapiPerson>.IndexOf(MapiPerson item)
		{
			return this.IndexOf(item);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0001CAC8 File Offset: 0x0001ACC8
		void IList<MapiPerson>.Insert(int index, MapiPerson item)
		{
			throw new NotImplementedException("read only for now");
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0001CAD4 File Offset: 0x0001ACD4
		void IList<MapiPerson>.RemoveAt(int index)
		{
			throw new NotImplementedException("read only for now");
		}

		// Token: 0x0400025D RID: 605
		private MapiMessage parentObject;

		// Token: 0x0400025E RID: 606
		private List<MapiPerson> people;

		// Token: 0x0400025F RID: 607
		private bool initialized;

		// Token: 0x04000260 RID: 608
		private List<StorePropTag> extraPropList;
	}
}
