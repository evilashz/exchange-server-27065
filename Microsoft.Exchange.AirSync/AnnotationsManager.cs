using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000030 RID: 48
	internal class AnnotationsManager
	{
		// Token: 0x06000359 RID: 857 RVA: 0x00013750 File Offset: 0x00011950
		public AnnotationsManager()
		{
			this.annotationsStore = new Dictionary<string, Dictionary<string, string>>();
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00013764 File Offset: 0x00011964
		public void ParseWLAnnotations(XmlNode annotationsNode, string annotationGroup = null)
		{
			if (annotationsNode == null)
			{
				return;
			}
			using (XmlNodeList childNodes = annotationsNode.ChildNodes)
			{
				foreach (object obj in childNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					string innerText = xmlNode["Name", "WindowsLive:"].InnerText;
					string value = null;
					if (xmlNode["Value", "WindowsLive:"] != null)
					{
						value = xmlNode["Value", "WindowsLive:"].InnerText;
					}
					this.AddAnnotationToStore(innerText, value, annotationGroup);
				}
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00013824 File Offset: 0x00011A24
		public void ParseWLAnnotations(XmlNode annotationsNode, string collectionId, string optionsClass)
		{
			this.ParseWLAnnotations(annotationsNode, AnnotationsManager.GetOptionsAnnotationsGroup(collectionId, optionsClass));
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00013834 File Offset: 0x00011A34
		public bool ContainsAnnotation(string name, string group = null)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			group = (group ?? string.Empty);
			Dictionary<string, string> dictionary;
			return this.annotationsStore.ContainsKey(group) && this.annotationsStore.TryGetValue(group, out dictionary) && dictionary.ContainsKey(name);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00013882 File Offset: 0x00011A82
		public bool ContainsAnnotation(string name, string collectionId, string optionsClass)
		{
			return this.ContainsAnnotation(name, AnnotationsManager.GetOptionsAnnotationsGroup(collectionId, optionsClass));
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00013894 File Offset: 0x00011A94
		public string FetchAnnotation(string name, string group = null)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			group = (group ?? string.Empty);
			Dictionary<string, string> dictionary = this.annotationsStore[group];
			return dictionary[name];
		}

		// Token: 0x0600035F RID: 863 RVA: 0x000138CF File Offset: 0x00011ACF
		public string FetchAnnotation(string name, string collectionId, string optionsClass)
		{
			return this.FetchAnnotation(name, AnnotationsManager.GetOptionsAnnotationsGroup(collectionId, optionsClass));
		}

		// Token: 0x06000360 RID: 864 RVA: 0x000138DF File Offset: 0x00011ADF
		public Dictionary<string, string> GetAnnotationGroup(string group)
		{
			group = (group ?? string.Empty);
			if (!this.annotationsStore.ContainsKey(group))
			{
				return null;
			}
			return this.annotationsStore[group];
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00013909 File Offset: 0x00011B09
		public Dictionary<string, string> GetAnnotationGroup(string collectionId, string optionsClass)
		{
			return this.GetAnnotationGroup(AnnotationsManager.GetOptionsAnnotationsGroup(collectionId, optionsClass));
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00013918 File Offset: 0x00011B18
		public static string GetCommandAnnotationGroup(string collectionId, string itemId)
		{
			if (collectionId == null)
			{
				throw new ArgumentNullException("collectionId");
			}
			if (itemId == null)
			{
				throw new ArgumentNullException("itemId");
			}
			return string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
			{
				collectionId,
				itemId
			});
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00013960 File Offset: 0x00011B60
		public void RemoveAnnotationGroup(string group)
		{
			if (group != null)
			{
				this.annotationsStore.Remove(group);
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00013972 File Offset: 0x00011B72
		public void RemoveAnnotationGroup(string collectionId, string optionsClass)
		{
			this.RemoveAnnotationGroup(AnnotationsManager.GetOptionsAnnotationsGroup(collectionId, optionsClass));
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00013984 File Offset: 0x00011B84
		public void AddAnnotationToStore(string name, string value, string group)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			group = (group ?? string.Empty);
			Dictionary<string, string> dictionary = null;
			if (!this.annotationsStore.TryGetValue(group, out dictionary))
			{
				dictionary = new Dictionary<string, string>();
				this.annotationsStore.Add(group, dictionary);
			}
			if (!dictionary.ContainsKey(name))
			{
				dictionary.Add(name, value);
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x000139E1 File Offset: 0x00011BE1
		public void AddAnnotationToStore(string name, string value, string collectionId, string optionsClass)
		{
			this.AddAnnotationToStore(name, value, AnnotationsManager.GetOptionsAnnotationsGroup(collectionId, optionsClass));
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000139F4 File Offset: 0x00011BF4
		private static string GetOptionsAnnotationsGroup(string collectionId, string optionsClass)
		{
			if (collectionId == null)
			{
				throw new ArgumentNullException("collectionId");
			}
			if (optionsClass == null)
			{
				throw new ArgumentNullException("optionsClass");
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
			{
				collectionId,
				optionsClass
			});
		}

		// Token: 0x04000298 RID: 664
		private Dictionary<string, Dictionary<string, string>> annotationsStore;
	}
}
