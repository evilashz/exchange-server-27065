using System;
using System.Collections;
using System.Collections.Generic;

namespace System
{
	// Token: 0x020000BD RID: 189
	internal class ConfigNode
	{
		// Token: 0x06000AF1 RID: 2801 RVA: 0x00022AA9 File Offset: 0x00020CA9
		internal ConfigNode(string name, ConfigNode parent)
		{
			this.m_name = name;
			this.m_parent = parent;
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x00022AD7 File Offset: 0x00020CD7
		internal string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x00022ADF File Offset: 0x00020CDF
		// (set) Token: 0x06000AF4 RID: 2804 RVA: 0x00022AE7 File Offset: 0x00020CE7
		internal string Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x00022AF0 File Offset: 0x00020CF0
		internal ConfigNode Parent
		{
			get
			{
				return this.m_parent;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x00022AF8 File Offset: 0x00020CF8
		internal List<ConfigNode> Children
		{
			get
			{
				return this.m_children;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x00022B00 File Offset: 0x00020D00
		internal List<DictionaryEntry> Attributes
		{
			get
			{
				return this.m_attributes;
			}
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00022B08 File Offset: 0x00020D08
		internal void AddChild(ConfigNode child)
		{
			child.m_parent = this;
			this.m_children.Add(child);
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00022B1D File Offset: 0x00020D1D
		internal int AddAttribute(string key, string value)
		{
			this.m_attributes.Add(new DictionaryEntry(key, value));
			return this.m_attributes.Count - 1;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00022B3E File Offset: 0x00020D3E
		internal void ReplaceAttribute(int index, string key, string value)
		{
			this.m_attributes[index] = new DictionaryEntry(key, value);
		}

		// Token: 0x0400045A RID: 1114
		private string m_name;

		// Token: 0x0400045B RID: 1115
		private string m_value;

		// Token: 0x0400045C RID: 1116
		private ConfigNode m_parent;

		// Token: 0x0400045D RID: 1117
		private List<ConfigNode> m_children = new List<ConfigNode>(5);

		// Token: 0x0400045E RID: 1118
		private List<DictionaryEntry> m_attributes = new List<DictionaryEntry>(5);
	}
}
