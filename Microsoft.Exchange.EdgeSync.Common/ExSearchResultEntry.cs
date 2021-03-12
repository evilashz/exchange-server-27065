using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Text;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x02000005 RID: 5
	internal class ExSearchResultEntry
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000023B4 File Offset: 0x000005B4
		public ExSearchResultEntry(string distinguishedName, DirectoryAttributeCollection attributes)
		{
			if (string.IsNullOrEmpty(distinguishedName))
			{
				throw new ArgumentNullException("distinguishedName");
			}
			this.attributes = new Dictionary<string, DirectoryAttribute>(attributes.Count, StringComparer.OrdinalIgnoreCase);
			foreach (object obj in attributes)
			{
				DirectoryAttribute directoryAttribute = (DirectoryAttribute)obj;
				this.attributes.Add(directoryAttribute.Name, directoryAttribute);
			}
			this.distinguishedName = distinguishedName;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000244C File Offset: 0x0000064C
		public ExSearchResultEntry(SearchResultEntry baseEntry)
		{
			if (baseEntry == null)
			{
				throw new ArgumentNullException("baseEntry");
			}
			if (string.IsNullOrEmpty(baseEntry.DistinguishedName))
			{
				throw new InvalidOperationException("baseEntry DistinguishedName can't be null or empty");
			}
			this.attributes = new Dictionary<string, DirectoryAttribute>(baseEntry.Attributes.Count);
			foreach (object obj in baseEntry.Attributes)
			{
				DirectoryAttribute directoryAttribute = (DirectoryAttribute)((DictionaryEntry)obj).Value;
				if (!directoryAttribute.Name.Equals("instanceType", StringComparison.OrdinalIgnoreCase))
				{
					this.attributes.Add(directoryAttribute.Name, directoryAttribute);
				}
			}
			this.distinguishedName = baseEntry.DistinguishedName;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002520 File Offset: 0x00000720
		public Dictionary<string, DirectoryAttribute> Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002528 File Offset: 0x00000728
		public string DistinguishedName
		{
			get
			{
				return this.distinguishedName;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002530 File Offset: 0x00000730
		public string ObjectClass
		{
			get
			{
				DirectoryAttribute attribute = this.GetAttribute("objectClass");
				if (attribute != null)
				{
					return (string)attribute[attribute.Count - 1];
				}
				return null;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002561 File Offset: 0x00000761
		public bool IsDeleted
		{
			get
			{
				return ExSearchResultEntry.IsDeletedDN(this.distinguishedName);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000256E File Offset: 0x0000076E
		public bool IsNew
		{
			get
			{
				return this.attributes.ContainsKey("whenCreated");
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002580 File Offset: 0x00000780
		public bool IsRenamed
		{
			get
			{
				return this.attributes.ContainsKey("name");
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002594 File Offset: 0x00000794
		public static bool IsDeletedDN(string distinguishedName)
		{
			int num = distinguishedName.IndexOf("\\0ADEL", StringComparison.OrdinalIgnoreCase);
			return num != -1;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000025B8 File Offset: 0x000007B8
		public static string GetAsciiStringValueOfAttribute(object attrValue, string attrName)
		{
			string text = attrValue as string;
			if (text == null)
			{
				byte[] array = attrValue as byte[];
				if (array == null)
				{
					throw new ArgumentException("The value of attribute " + attrName + " is neither string nor byte[]", "attrValue");
				}
				text = Encoding.ASCII.GetString(array);
			}
			return text;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002604 File Offset: 0x00000804
		public bool MultiValuedAttributeContain(string attributeName, string valueToFind)
		{
			DirectoryAttribute directoryAttribute;
			if (!this.Attributes.TryGetValue(attributeName, out directoryAttribute))
			{
				throw new ArgumentException("The entry should contain the attribute " + attributeName);
			}
			foreach (object obj in directoryAttribute)
			{
				if (obj != null)
				{
					string asciiStringValueOfAttribute = ExSearchResultEntry.GetAsciiStringValueOfAttribute(obj, attributeName);
					if (asciiStringValueOfAttribute != null && asciiStringValueOfAttribute.Equals(valueToFind, StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002694 File Offset: 0x00000894
		public bool IsCollisionObject(out int index, out int length)
		{
			index = -1;
			length = 0;
			index = this.distinguishedName.IndexOf("\\0ACNF", StringComparison.OrdinalIgnoreCase);
			if (index != -1)
			{
				length = "\\0ACNF".Length;
				return true;
			}
			return false;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000026C3 File Offset: 0x000008C3
		public ExSearchResultEntry Clone()
		{
			return this.Clone(this.distinguishedName);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000026D4 File Offset: 0x000008D4
		public ExSearchResultEntry Clone(string distinguishedName)
		{
			DirectoryAttributeCollection directoryAttributeCollection = new DirectoryAttributeCollection();
			foreach (KeyValuePair<string, DirectoryAttribute> keyValuePair in this.attributes)
			{
				DirectoryAttribute value = keyValuePair.Value;
				DirectoryAttribute directoryAttribute = new DirectoryAttribute();
				directoryAttribute.Name = value.Name;
				foreach (object obj in value)
				{
					if (obj is byte[])
					{
						byte[] array = new byte[((byte[])obj).Length];
						Buffer.BlockCopy((byte[])obj, 0, array, 0, array.Length);
						directoryAttribute.Add(array);
					}
					else if (obj is string)
					{
						string value2 = string.Copy((string)obj);
						directoryAttribute.Add(value2);
					}
					else
					{
						if (!(obj is Uri))
						{
							throw new NotSupportedException("Type " + obj.GetType() + " is not supported");
						}
						Uri value3 = new Uri(((Uri)obj).OriginalString);
						directoryAttribute.Add(value3);
					}
				}
				directoryAttributeCollection.Add(directoryAttribute);
			}
			return new ExSearchResultEntry(distinguishedName, directoryAttributeCollection);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002850 File Offset: 0x00000A50
		public DirectoryAttribute GetAttribute(string name)
		{
			DirectoryAttribute directoryAttribute;
			if (!this.attributes.TryGetValue(name, out directoryAttribute) || (directoryAttribute != null && directoryAttribute.Count == 0))
			{
				directoryAttribute = null;
			}
			return directoryAttribute;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000287C File Offset: 0x00000A7C
		public Guid GetObjectGuid()
		{
			DirectoryAttribute attribute = this.GetAttribute("objectGUID");
			if (attribute == null)
			{
				throw new InvalidOperationException("AD entry does not contain objectGUID");
			}
			return new Guid((byte[])attribute[0]);
		}

		// Token: 0x04000007 RID: 7
		private const string DeletedObjectSig = "\\0ADEL";

		// Token: 0x04000008 RID: 8
		private const string CollisionObjectSig = "\\0ACNF";

		// Token: 0x04000009 RID: 9
		private readonly Dictionary<string, DirectoryAttribute> attributes;

		// Token: 0x0400000A RID: 10
		private readonly string distinguishedName;
	}
}
