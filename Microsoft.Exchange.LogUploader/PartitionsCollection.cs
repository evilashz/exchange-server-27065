using System;
using System.Collections.Generic;
using System.Configuration;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000037 RID: 55
	[ConfigurationCollection(typeof(PartitionElement), AddItemName = "Partition")]
	public class PartitionsCollection : ConfigurationElementCollection
	{
		// Token: 0x06000294 RID: 660 RVA: 0x0000B78E File Offset: 0x0000998E
		public PartitionsCollection() : base(StringComparer.InvariantCultureIgnoreCase)
		{
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000B7A6 File Offset: 0x000099A6
		public void Add(PartitionElement element)
		{
			this.InsertElementList(element);
			this.BaseAdd(element);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000B7B6 File Offset: 0x000099B6
		public PartitionElement Get(object key)
		{
			return (PartitionElement)base.BaseGet(key);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000B7C4 File Offset: 0x000099C4
		public PartitionElement Get(int copyId, int partitionId)
		{
			if (this.elements.ContainsKey(copyId))
			{
				Dictionary<int, PartitionElement> dictionary = this.elements[copyId];
				if (dictionary.ContainsKey(partitionId))
				{
					return dictionary[partitionId];
				}
			}
			return (PartitionElement)base.BaseGet("Default");
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000B810 File Offset: 0x00009A10
		public void BuildSearchList()
		{
			foreach (object obj in this)
			{
				PartitionElement e = (PartitionElement)obj;
				this.InsertElementList(e);
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000B864 File Offset: 0x00009A64
		protected override ConfigurationElement CreateNewElement()
		{
			return new PartitionElement();
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000B86B File Offset: 0x00009A6B
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((PartitionElement)element).Name;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000B878 File Offset: 0x00009A78
		private void InsertElementList(PartitionElement e)
		{
			if (string.Compare(e.Name, "Default", true) != 0)
			{
				Dictionary<int, PartitionElement> dictionary;
				if (this.elements.ContainsKey(e.CopyId))
				{
					dictionary = this.elements[e.CopyId];
				}
				else
				{
					dictionary = new Dictionary<int, PartitionElement>();
					this.elements.Add(e.CopyId, dictionary);
				}
				dictionary.Add(e.PartitionId, e);
			}
		}

		// Token: 0x0400016A RID: 362
		private Dictionary<int, Dictionary<int, PartitionElement>> elements = new Dictionary<int, Dictionary<int, PartitionElement>>();
	}
}
