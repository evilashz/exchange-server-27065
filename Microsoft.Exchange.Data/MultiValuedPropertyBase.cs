using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000015 RID: 21
	[TypeConverter(typeof(SimpleGenericsTypeConverter))]
	[Serializable]
	public abstract class MultiValuedPropertyBase : ICloneable, ICollection, IEnumerable
	{
		// Token: 0x06000069 RID: 105 RVA: 0x0000348C File Offset: 0x0000168C
		internal static string FormatMultiValuedProperty(IList mvp)
		{
			if (mvp == null)
			{
				throw new ArgumentNullException("mvp");
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < mvp.Count - 1; i++)
			{
				stringBuilder.Append("'");
				stringBuilder.Append(mvp[i].ToString());
				stringBuilder.Append("', ");
			}
			if (mvp.Count != 0)
			{
				stringBuilder.Append("'" + mvp[mvp.Count - 1].ToString() + "'");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00003522 File Offset: 0x00001722
		// (set) Token: 0x0600006B RID: 107 RVA: 0x0000352A File Offset: 0x0000172A
		internal bool IsCompletelyRead
		{
			get
			{
				return this.isCompletelyRead;
			}
			set
			{
				this.isCompletelyRead = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003533 File Offset: 0x00001733
		// (set) Token: 0x0600006D RID: 109 RVA: 0x0000353B File Offset: 0x0000173B
		internal IntRange ValueRange { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006E RID: 110
		internal abstract bool WasCleared { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006F RID: 111
		internal abstract object[] Added { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000070 RID: 112
		internal abstract object[] Removed { get; }

		// Token: 0x06000071 RID: 113
		internal abstract void Add(object item);

		// Token: 0x06000072 RID: 114
		internal abstract bool Remove(object item);

		// Token: 0x06000073 RID: 115
		internal abstract void MarkAsChanged();

		// Token: 0x06000074 RID: 116
		internal abstract void ResetChangeTracking();

		// Token: 0x06000075 RID: 117
		internal abstract void UpdateValues(MultiValuedPropertyBase newMvp);

		// Token: 0x06000076 RID: 118
		internal abstract void UpdatePropertyDefinition(ProviderPropertyDefinition newPropertyDefinition);

		// Token: 0x06000077 RID: 119
		internal abstract void SetIsReadOnly(bool isReadOnly, LocalizedString? readOnlyErrorMessage);

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000078 RID: 120
		internal abstract ProviderPropertyDefinition PropertyDefinition { get; }

		// Token: 0x06000079 RID: 121 RVA: 0x00003544 File Offset: 0x00001744
		internal virtual void FinalizeDeserialization()
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003546 File Offset: 0x00001746
		private void AddHandler(ref Delegate mainDelegate, Delegate value)
		{
			mainDelegate = Delegate.Combine(mainDelegate, value);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003552 File Offset: 0x00001752
		private void RemoveHandler(ref Delegate mainDelegate, Delegate value)
		{
			mainDelegate = Delegate.Remove(mainDelegate, value);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600007C RID: 124 RVA: 0x0000355E File Offset: 0x0000175E
		// (remove) Token: 0x0600007D RID: 125 RVA: 0x0000356D File Offset: 0x0000176D
		internal event EventHandler CollectionChanging
		{
			add
			{
				this.AddHandler(ref this.eventCollectionChangingDelegate, value);
			}
			remove
			{
				this.RemoveHandler(ref this.eventCollectionChangingDelegate, value);
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000357C File Offset: 0x0000177C
		protected virtual void OnCollectionChanging(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)this.eventCollectionChangingDelegate;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600007F RID: 127 RVA: 0x000035A0 File Offset: 0x000017A0
		// (remove) Token: 0x06000080 RID: 128 RVA: 0x000035AF File Offset: 0x000017AF
		internal event EventHandler CollectionChanged
		{
			add
			{
				this.AddHandler(ref this.eventCollectionChangedDelegate, value);
			}
			remove
			{
				this.RemoveHandler(ref this.eventCollectionChangedDelegate, value);
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000035C0 File Offset: 0x000017C0
		protected virtual void OnCollectionChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)this.eventCollectionChangedDelegate;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000035E4 File Offset: 0x000017E4
		protected void BeginUpdate()
		{
			if (this.updateCount == 0)
			{
				this.OnCollectionChanging(EventArgs.Empty);
			}
			this.updateCount++;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003607 File Offset: 0x00001807
		protected void EndUpdate()
		{
			this.updateCount--;
			if (this.updateCount == 0)
			{
				this.OnCollectionChanged(EventArgs.Empty);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000362A File Offset: 0x0000182A
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00003632 File Offset: 0x00001832
		internal bool CopyChangesOnly
		{
			get
			{
				return this.copyChangesOnly;
			}
			set
			{
				this.copyChangesOnly = value;
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000363B File Offset: 0x0000183B
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003642 File Offset: 0x00001842
		public static bool IsNullOrEmpty(MultiValuedPropertyBase property)
		{
			return property == null || property.Count == 0;
		}

		// Token: 0x06000088 RID: 136
		public abstract void Clear();

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000089 RID: 137
		public abstract bool Changed { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600008A RID: 138
		public abstract bool IsChangesOnlyCopy { get; }

		// Token: 0x0600008B RID: 139
		public abstract object Clone();

		// Token: 0x0600008C RID: 140
		public abstract void CopyChangesFrom(MultiValuedPropertyBase changedMvp);

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008D RID: 141
		public abstract int Count { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600008E RID: 142
		public abstract bool IsReadOnly { get; }

		// Token: 0x0600008F RID: 143
		public abstract void CopyTo(Array array, int index);

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000090 RID: 144
		public abstract bool IsSynchronized { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000091 RID: 145
		public abstract object SyncRoot { get; }

		// Token: 0x06000092 RID: 146 RVA: 0x00003652 File Offset: 0x00001852
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.copyChangesOnly = false;
			this.updateCount = 0;
		}

		// Token: 0x0400002E RID: 46
		[NonSerialized]
		private bool copyChangesOnly;

		// Token: 0x0400002F RID: 47
		[NonSerialized]
		private int updateCount;

		// Token: 0x04000030 RID: 48
		private bool isCompletelyRead = true;

		// Token: 0x04000031 RID: 49
		[NonSerialized]
		private Delegate eventCollectionChangingDelegate;

		// Token: 0x04000032 RID: 50
		[NonSerialized]
		private Delegate eventCollectionChangedDelegate;
	}
}
