using System;
using System.Collections;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000004 RID: 4
	internal abstract class ListViewDataSource
	{
		// Token: 0x06000013 RID: 19 RVA: 0x000024F0 File Offset: 0x000006F0
		public ListViewDataSource(Hashtable properties, Folder folder)
		{
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			if (folder == null)
			{
				throw new ArgumentNullException("folder");
			}
			this.properties = properties;
			this.folder = folder;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002543 File Offset: 0x00000743
		public ListViewDataSource(Hashtable properties)
		{
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			this.properties = properties;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002576 File Offset: 0x00000776
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000257E File Offset: 0x0000077E
		public Folder Folder
		{
			get
			{
				return this.folder;
			}
			set
			{
				this.folder = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002587 File Offset: 0x00000787
		// (set) Token: 0x06000018 RID: 24 RVA: 0x0000258F File Offset: 0x0000078F
		public object[][] Items
		{
			get
			{
				return this.items;
			}
			set
			{
				this.items = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002598 File Offset: 0x00000798
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000025A0 File Offset: 0x000007A0
		public string Cookie
		{
			get
			{
				return this.cookie;
			}
			set
			{
				this.cookie = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000025A9 File Offset: 0x000007A9
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000025B1 File Offset: 0x000007B1
		public int StartRange
		{
			get
			{
				return this.startRange;
			}
			set
			{
				this.startRange = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000025BA File Offset: 0x000007BA
		// (set) Token: 0x0600001E RID: 30 RVA: 0x000025C2 File Offset: 0x000007C2
		public int EndRange
		{
			get
			{
				return this.endRange;
			}
			set
			{
				this.endRange = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000025CB File Offset: 0x000007CB
		public int RangeCount
		{
			get
			{
				if (this.endRange < this.startRange || this.startRange == -2147483648 || this.endRange == -2147483648)
				{
					return 0;
				}
				return this.endRange - this.startRange + 1;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002606 File Offset: 0x00000806
		public virtual int TotalCount
		{
			get
			{
				return this.folder.ItemCount;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002613 File Offset: 0x00000813
		public int UnreadCount
		{
			get
			{
				if (this.folder.TryGetProperty(FolderSchema.UnreadCount) is int)
				{
					return (int)this.folder[FolderSchema.UnreadCount];
				}
				return 0;
			}
		}

		// Token: 0x06000022 RID: 34
		public abstract void LoadData(int startRange, int endRange);

		// Token: 0x06000023 RID: 35 RVA: 0x00002643 File Offset: 0x00000843
		public virtual int LoadData(StoreObjectId storeObjectId, int itemsPerPage)
		{
			return 0;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002648 File Offset: 0x00000848
		public object GetItemProperty(int item, PropertyDefinition propertyDefinition)
		{
			int num = (int)this.propertyIndices[propertyDefinition];
			return this.items[item][num];
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002671 File Offset: 0x00000871
		public VersionedId GetItemPropertyVersionedId(int item, PropertyDefinition propertyDefinition)
		{
			return this.GetItemProperty(item, propertyDefinition) as VersionedId;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002680 File Offset: 0x00000880
		public string GetItemPropertyString(int item, PropertyDefinition propertyDefinition)
		{
			string text = this.GetItemProperty(item, propertyDefinition) as string;
			if (text == null)
			{
				return string.Empty;
			}
			return text;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000026A8 File Offset: 0x000008A8
		public ExDateTime GetItemPropertyExDateTime(int item, PropertyDefinition propertyDefinition)
		{
			object itemProperty = this.GetItemProperty(item, propertyDefinition);
			if (itemProperty is DateTime)
			{
				throw new OwaInvalidInputException("List view item property must be ExDateTime not DateTime");
			}
			if (itemProperty is ExDateTime)
			{
				return (ExDateTime)itemProperty;
			}
			return ExDateTime.MinValue;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000026E8 File Offset: 0x000008E8
		public int GetItemPropertyInt(int item, PropertyDefinition propertyDefinition, int defaultValue)
		{
			object itemProperty = this.GetItemProperty(item, propertyDefinition);
			if (!(itemProperty is int))
			{
				return defaultValue;
			}
			return (int)itemProperty;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002710 File Offset: 0x00000910
		public bool GetItemPropertyBool(int item, PropertyDefinition propertyDefinition, bool defaultValue)
		{
			object itemProperty = this.GetItemProperty(item, propertyDefinition);
			if (!(itemProperty is bool))
			{
				return defaultValue;
			}
			return (bool)itemProperty;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002738 File Offset: 0x00000938
		protected PropertyDefinition[] CreateProperyTable()
		{
			this.propertyIndices = (Hashtable)this.properties.Clone();
			PropertyDefinition[] array = new PropertyDefinition[this.propertyIndices.Count];
			int num = 0;
			IDictionaryEnumerator enumerator = this.properties.GetEnumerator();
			while (enumerator.MoveNext())
			{
				PropertyDefinition propertyDefinition = (PropertyDefinition)enumerator.Key;
				array[num] = propertyDefinition;
				this.propertyIndices[propertyDefinition] = num++;
			}
			return array;
		}

		// Token: 0x0400000A RID: 10
		private Hashtable properties;

		// Token: 0x0400000B RID: 11
		private Hashtable propertyIndices;

		// Token: 0x0400000C RID: 12
		private Folder folder;

		// Token: 0x0400000D RID: 13
		private int startRange = int.MinValue;

		// Token: 0x0400000E RID: 14
		private int endRange = int.MinValue;

		// Token: 0x0400000F RID: 15
		private object[][] items;

		// Token: 0x04000010 RID: 16
		private string cookie;
	}
}
