using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x02000033 RID: 51
	[Table]
	public class StatusEntry
	{
		// Token: 0x06000326 RID: 806 RVA: 0x0000B73A File Offset: 0x0000993A
		public StatusEntry()
		{
			this.MarkEntryAsPersisted();
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000B764 File Offset: 0x00009964
		protected internal StatusEntry(string key)
		{
			this.Key = key;
			this.state = StatusEntry.EntryState.ToBeWritten;
			this.CreatedTime = DateTime.UtcNow;
			this.UpdatedTime = this.CreatedTime;
			this.TableName = Settings.GetTableName(typeof(StatusEntry));
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000B7CA File Offset: 0x000099CA
		// (set) Token: 0x06000329 RID: 809 RVA: 0x0000B7D2 File Offset: 0x000099D2
		[Column]
		public DateTime CreatedTime { get; internal set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000B7DB File Offset: 0x000099DB
		// (set) Token: 0x0600032B RID: 811 RVA: 0x0000B7E3 File Offset: 0x000099E3
		[Column]
		public DateTime UpdatedTime { get; internal set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000B7EC File Offset: 0x000099EC
		// (set) Token: 0x0600032D RID: 813 RVA: 0x0000B7F4 File Offset: 0x000099F4
		[Column]
		public string XmlColumn1 { get; internal set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000B7FD File Offset: 0x000099FD
		// (set) Token: 0x0600032F RID: 815 RVA: 0x0000B805 File Offset: 0x00009A05
		[Column]
		public bool Remove { get; internal set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000B80E File Offset: 0x00009A0E
		// (set) Token: 0x06000331 RID: 817 RVA: 0x0000B816 File Offset: 0x00009A16
		[Column(IsPrimaryKey = true, IsDbGenerated = true)]
		public int Id
		{
			get
			{
				return this.id;
			}
			internal set
			{
				if (this.id == -1)
				{
					this.id = value;
				}
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000B828 File Offset: 0x00009A28
		// (set) Token: 0x06000333 RID: 819 RVA: 0x0000B830 File Offset: 0x00009A30
		[Column]
		public string Key { get; internal set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000B839 File Offset: 0x00009A39
		// (set) Token: 0x06000335 RID: 821 RVA: 0x0000B841 File Offset: 0x00009A41
		[Column]
		public string TableName { get; internal set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000B84A File Offset: 0x00009A4A
		internal StatusEntry.EntryState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x1700011D RID: 285
		public string this[string propertyName]
		{
			get
			{
				return this.GetPropertyValue(propertyName);
			}
			set
			{
				this.SetPropertyValue(propertyName, value);
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000B865 File Offset: 0x00009A65
		internal bool EntryExistsInDatabase()
		{
			return this.state != StatusEntry.EntryState.ToBeWritten && this.state != StatusEntry.EntryState.Removed;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000B8BC File Offset: 0x00009ABC
		internal void PrepareForPersistance()
		{
			this.LoadPropertiesFromXml();
			XElement xelement = new XElement("Status", this.properties.Select(delegate(KeyValuePair<string, string> kv)
			{
				XElement xelement2 = new XElement("Prop", kv.Value);
				xelement2.SetAttributeValue("Name", kv.Key);
				return xelement2;
			}));
			this.XmlColumn1 = xelement.ToString();
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000B913 File Offset: 0x00009B13
		internal void MarkEntryAsPersisted()
		{
			if (this.state != StatusEntry.EntryState.Removed)
			{
				this.state = StatusEntry.EntryState.Unchanged;
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000B925 File Offset: 0x00009B25
		internal void MarkEntryAsRemoved()
		{
			this.state = StatusEntry.EntryState.Removed;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000B930 File Offset: 0x00009B30
		private string GetPropertyValue(string propertyName)
		{
			string propertyValueFromXml;
			if (!this.properties.TryGetValue(propertyName, out propertyValueFromXml))
			{
				propertyValueFromXml = this.GetPropertyValueFromXml(propertyName);
			}
			return propertyValueFromXml;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000B956 File Offset: 0x00009B56
		private void SetPropertyValue(string propertyName, string value)
		{
			if (!this.properties.ContainsKey(propertyName))
			{
				this.properties.Add(propertyName, value);
				return;
			}
			throw new Exception(string.Format("Unable to set key {0} as it alreayd has a value.", propertyName));
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000B984 File Offset: 0x00009B84
		private void LoadPropertiesFromXml()
		{
			if (!string.IsNullOrEmpty(this.XmlColumn1))
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(this.XmlColumn1);
				XmlNodeList xmlNodeList = xmlDocument.SelectNodes(StatusEntry.XpathSearchExpression);
				foreach (object obj in xmlNodeList)
				{
					XmlNode xmlNode = (XmlNode)obj;
					string value = xmlNode.Attributes["Name"].Value;
					if (!this.properties.ContainsKey(value))
					{
						string innerText = xmlNode.InnerText;
						this.properties.Add(value, innerText);
					}
				}
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000BA40 File Offset: 0x00009C40
		private string GetPropertyValueFromXml(string propertyName)
		{
			string empty = string.Empty;
			this.LoadPropertiesFromXml();
			this.properties.TryGetValue(propertyName, out empty);
			return empty;
		}

		// Token: 0x0400013F RID: 319
		private const int NonPersistedItemId = -1;

		// Token: 0x04000140 RID: 320
		private const string XmlRootNodeName = "Status";

		// Token: 0x04000141 RID: 321
		private const string PropertyXmlNodeName = "Prop";

		// Token: 0x04000142 RID: 322
		private static readonly string XpathSearchExpression = string.Format("{0}/{1}", "Status", "Prop");

		// Token: 0x04000143 RID: 323
		private StatusEntry.EntryState state = StatusEntry.EntryState.Unchanged;

		// Token: 0x04000144 RID: 324
		private int id = -1;

		// Token: 0x04000145 RID: 325
		private Dictionary<string, string> properties = new Dictionary<string, string>();

		// Token: 0x02000034 RID: 52
		internal enum EntryState
		{
			// Token: 0x0400014E RID: 334
			ToBeWritten,
			// Token: 0x0400014F RID: 335
			Removed,
			// Token: 0x04000150 RID: 336
			Unchanged
		}
	}
}
