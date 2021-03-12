﻿using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;
using System.Text;

namespace System.Security
{
	// Token: 0x020001BD RID: 445
	[ComVisible(true)]
	[Serializable]
	public sealed class SecurityElement : ISecurityElementFactory
	{
		// Token: 0x06001BD1 RID: 7121 RVA: 0x0005F834 File Offset: 0x0005DA34
		internal SecurityElement()
		{
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x0005F83C File Offset: 0x0005DA3C
		SecurityElement ISecurityElementFactory.CreateSecurityElement()
		{
			return this;
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x0005F83F File Offset: 0x0005DA3F
		string ISecurityElementFactory.GetTag()
		{
			return this.Tag;
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x0005F847 File Offset: 0x0005DA47
		object ISecurityElementFactory.Copy()
		{
			return this.Copy();
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x0005F84F File Offset: 0x0005DA4F
		string ISecurityElementFactory.Attribute(string attributeName)
		{
			return this.Attribute(attributeName);
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x0005F858 File Offset: 0x0005DA58
		public static SecurityElement FromString(string xml)
		{
			if (xml == null)
			{
				throw new ArgumentNullException("xml");
			}
			return new Parser(xml).GetTopElement();
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x0005F874 File Offset: 0x0005DA74
		public SecurityElement(string tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (!SecurityElement.IsValidTag(tag))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementTag"), tag));
			}
			this.m_strTag = tag;
			this.m_strText = null;
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x0005F8C8 File Offset: 0x0005DAC8
		public SecurityElement(string tag, string text)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (!SecurityElement.IsValidTag(tag))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementTag"), tag));
			}
			if (text != null && !SecurityElement.IsValidText(text))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementText"), text));
			}
			this.m_strTag = tag;
			this.m_strText = text;
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06001BD9 RID: 7129 RVA: 0x0005F940 File Offset: 0x0005DB40
		// (set) Token: 0x06001BDA RID: 7130 RVA: 0x0005F948 File Offset: 0x0005DB48
		public string Tag
		{
			get
			{
				return this.m_strTag;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Tag");
				}
				if (!SecurityElement.IsValidTag(value))
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementTag"), value));
				}
				this.m_strTag = value;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06001BDB RID: 7131 RVA: 0x0005F984 File Offset: 0x0005DB84
		// (set) Token: 0x06001BDC RID: 7132 RVA: 0x0005F9F4 File Offset: 0x0005DBF4
		public Hashtable Attributes
		{
			get
			{
				if (this.m_lAttributes == null || this.m_lAttributes.Count == 0)
				{
					return null;
				}
				Hashtable hashtable = new Hashtable(this.m_lAttributes.Count / 2);
				int count = this.m_lAttributes.Count;
				for (int i = 0; i < count; i += 2)
				{
					hashtable.Add(this.m_lAttributes[i], this.m_lAttributes[i + 1]);
				}
				return hashtable;
			}
			set
			{
				if (value == null || value.Count == 0)
				{
					this.m_lAttributes = null;
					return;
				}
				ArrayList arrayList = new ArrayList(value.Count);
				IDictionaryEnumerator enumerator = value.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Key;
					string value2 = (string)enumerator.Value;
					if (!SecurityElement.IsValidAttributeName(text))
					{
						throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementName"), (string)enumerator.Current));
					}
					if (!SecurityElement.IsValidAttributeValue(value2))
					{
						throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementValue"), (string)enumerator.Value));
					}
					arrayList.Add(text);
					arrayList.Add(value2);
				}
				this.m_lAttributes = arrayList;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x0005FAC0 File Offset: 0x0005DCC0
		// (set) Token: 0x06001BDE RID: 7134 RVA: 0x0005FACD File Offset: 0x0005DCCD
		public string Text
		{
			get
			{
				return SecurityElement.Unescape(this.m_strText);
			}
			set
			{
				if (value == null)
				{
					this.m_strText = null;
					return;
				}
				if (!SecurityElement.IsValidText(value))
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementTag"), value));
				}
				this.m_strText = value;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06001BDF RID: 7135 RVA: 0x0005FB04 File Offset: 0x0005DD04
		// (set) Token: 0x06001BE0 RID: 7136 RVA: 0x0005FB14 File Offset: 0x0005DD14
		public ArrayList Children
		{
			get
			{
				this.ConvertSecurityElementFactories();
				return this.m_lChildren;
			}
			set
			{
				if (value != null)
				{
					IEnumerator enumerator = value.GetEnumerator();
					while (enumerator.MoveNext())
					{
						if (enumerator.Current == null)
						{
							throw new ArgumentException(Environment.GetResourceString("ArgumentNull_Child"));
						}
					}
				}
				this.m_lChildren = value;
			}
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x0005FB54 File Offset: 0x0005DD54
		internal void ConvertSecurityElementFactories()
		{
			if (this.m_lChildren == null)
			{
				return;
			}
			for (int i = 0; i < this.m_lChildren.Count; i++)
			{
				ISecurityElementFactory securityElementFactory = this.m_lChildren[i] as ISecurityElementFactory;
				if (securityElementFactory != null && !(this.m_lChildren[i] is SecurityElement))
				{
					this.m_lChildren[i] = securityElementFactory.CreateSecurityElement();
				}
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x0005FBBA File Offset: 0x0005DDBA
		internal ArrayList InternalChildren
		{
			get
			{
				return this.m_lChildren;
			}
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x0005FBC4 File Offset: 0x0005DDC4
		internal void AddAttributeSafe(string name, string value)
		{
			if (this.m_lAttributes == null)
			{
				this.m_lAttributes = new ArrayList(8);
			}
			else
			{
				int count = this.m_lAttributes.Count;
				for (int i = 0; i < count; i += 2)
				{
					string a = (string)this.m_lAttributes[i];
					if (string.Equals(a, name))
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_AttributeNamesMustBeUnique"));
					}
				}
			}
			this.m_lAttributes.Add(name);
			this.m_lAttributes.Add(value);
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x0005FC44 File Offset: 0x0005DE44
		public void AddAttribute(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!SecurityElement.IsValidAttributeName(name))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementName"), name));
			}
			if (!SecurityElement.IsValidAttributeValue(value))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidElementValue"), value));
			}
			this.AddAttributeSafe(name, value);
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x0005FCBB File Offset: 0x0005DEBB
		public void AddChild(SecurityElement child)
		{
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			if (this.m_lChildren == null)
			{
				this.m_lChildren = new ArrayList(1);
			}
			this.m_lChildren.Add(child);
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x0005FCEC File Offset: 0x0005DEEC
		internal void AddChild(ISecurityElementFactory child)
		{
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			if (this.m_lChildren == null)
			{
				this.m_lChildren = new ArrayList(1);
			}
			this.m_lChildren.Add(child);
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x0005FD20 File Offset: 0x0005DF20
		internal void AddChildNoDuplicates(ISecurityElementFactory child)
		{
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			if (this.m_lChildren == null)
			{
				this.m_lChildren = new ArrayList(1);
				this.m_lChildren.Add(child);
				return;
			}
			for (int i = 0; i < this.m_lChildren.Count; i++)
			{
				if (this.m_lChildren[i] == child)
				{
					return;
				}
			}
			this.m_lChildren.Add(child);
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x0005FD90 File Offset: 0x0005DF90
		public bool Equal(SecurityElement other)
		{
			if (other == null)
			{
				return false;
			}
			if (!string.Equals(this.m_strTag, other.m_strTag))
			{
				return false;
			}
			if (!string.Equals(this.m_strText, other.m_strText))
			{
				return false;
			}
			if (this.m_lAttributes == null || other.m_lAttributes == null)
			{
				if (this.m_lAttributes != other.m_lAttributes)
				{
					return false;
				}
			}
			else
			{
				int count = this.m_lAttributes.Count;
				if (count != other.m_lAttributes.Count)
				{
					return false;
				}
				for (int i = 0; i < count; i++)
				{
					string a = (string)this.m_lAttributes[i];
					string b = (string)other.m_lAttributes[i];
					if (!string.Equals(a, b))
					{
						return false;
					}
				}
			}
			if (this.m_lChildren == null || other.m_lChildren == null)
			{
				if (this.m_lChildren != other.m_lChildren)
				{
					return false;
				}
			}
			else
			{
				if (this.m_lChildren.Count != other.m_lChildren.Count)
				{
					return false;
				}
				this.ConvertSecurityElementFactories();
				other.ConvertSecurityElementFactories();
				IEnumerator enumerator = this.m_lChildren.GetEnumerator();
				IEnumerator enumerator2 = other.m_lChildren.GetEnumerator();
				while (enumerator.MoveNext())
				{
					enumerator2.MoveNext();
					SecurityElement securityElement = (SecurityElement)enumerator.Current;
					SecurityElement other2 = (SecurityElement)enumerator2.Current;
					if (securityElement == null || !securityElement.Equal(other2))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x0005FEE8 File Offset: 0x0005E0E8
		[ComVisible(false)]
		public SecurityElement Copy()
		{
			return new SecurityElement(this.m_strTag, this.m_strText)
			{
				m_lChildren = ((this.m_lChildren == null) ? null : new ArrayList(this.m_lChildren)),
				m_lAttributes = ((this.m_lAttributes == null) ? null : new ArrayList(this.m_lAttributes))
			};
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x0005FF40 File Offset: 0x0005E140
		public static bool IsValidTag(string tag)
		{
			return tag != null && tag.IndexOfAny(SecurityElement.s_tagIllegalCharacters) == -1;
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x0005FF55 File Offset: 0x0005E155
		public static bool IsValidText(string text)
		{
			return text != null && text.IndexOfAny(SecurityElement.s_textIllegalCharacters) == -1;
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x0005FF6A File Offset: 0x0005E16A
		public static bool IsValidAttributeName(string name)
		{
			return SecurityElement.IsValidTag(name);
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x0005FF72 File Offset: 0x0005E172
		public static bool IsValidAttributeValue(string value)
		{
			return value != null && value.IndexOfAny(SecurityElement.s_valueIllegalCharacters) == -1;
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x0005FF88 File Offset: 0x0005E188
		private static string GetEscapeSequence(char c)
		{
			int num = SecurityElement.s_escapeStringPairs.Length;
			for (int i = 0; i < num; i += 2)
			{
				string text = SecurityElement.s_escapeStringPairs[i];
				string result = SecurityElement.s_escapeStringPairs[i + 1];
				if (text[0] == c)
				{
					return result;
				}
			}
			return c.ToString();
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x0005FFD0 File Offset: 0x0005E1D0
		public static string Escape(string str)
		{
			if (str == null)
			{
				return null;
			}
			StringBuilder stringBuilder = null;
			int length = str.Length;
			int num = 0;
			for (;;)
			{
				int num2 = str.IndexOfAny(SecurityElement.s_escapeChars, num);
				if (num2 == -1)
				{
					break;
				}
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder();
				}
				stringBuilder.Append(str, num, num2 - num);
				stringBuilder.Append(SecurityElement.GetEscapeSequence(str[num2]));
				num = num2 + 1;
			}
			if (stringBuilder == null)
			{
				return str;
			}
			stringBuilder.Append(str, num, length - num);
			return stringBuilder.ToString();
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x00060044 File Offset: 0x0005E244
		private static string GetUnescapeSequence(string str, int index, out int newIndex)
		{
			int num = str.Length - index;
			int num2 = SecurityElement.s_escapeStringPairs.Length;
			for (int i = 0; i < num2; i += 2)
			{
				string result = SecurityElement.s_escapeStringPairs[i];
				string text = SecurityElement.s_escapeStringPairs[i + 1];
				int length = text.Length;
				if (length <= num && string.Compare(text, 0, str, index, length, StringComparison.Ordinal) == 0)
				{
					newIndex = index + text.Length;
					return result;
				}
			}
			newIndex = index + 1;
			return str[index].ToString();
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x000600C0 File Offset: 0x0005E2C0
		private static string Unescape(string str)
		{
			if (str == null)
			{
				return null;
			}
			StringBuilder stringBuilder = null;
			int length = str.Length;
			int num = 0;
			for (;;)
			{
				int num2 = str.IndexOf('&', num);
				if (num2 == -1)
				{
					break;
				}
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder();
				}
				stringBuilder.Append(str, num, num2 - num);
				stringBuilder.Append(SecurityElement.GetUnescapeSequence(str, num2, out num));
			}
			if (stringBuilder == null)
			{
				return str;
			}
			stringBuilder.Append(str, num, length - num);
			return stringBuilder.ToString();
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x00060129 File Offset: 0x0005E329
		private static void ToStringHelperStringBuilder(object obj, string str)
		{
			((StringBuilder)obj).Append(str);
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x00060138 File Offset: 0x0005E338
		private static void ToStringHelperStreamWriter(object obj, string str)
		{
			((StreamWriter)obj).Write(str);
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x00060148 File Offset: 0x0005E348
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ToString("", stringBuilder, new SecurityElement.ToStringHelperFunc(SecurityElement.ToStringHelperStringBuilder));
			return stringBuilder.ToString();
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x00060179 File Offset: 0x0005E379
		internal void ToWriter(StreamWriter writer)
		{
			this.ToString("", writer, new SecurityElement.ToStringHelperFunc(SecurityElement.ToStringHelperStreamWriter));
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x00060194 File Offset: 0x0005E394
		private void ToString(string indent, object obj, SecurityElement.ToStringHelperFunc func)
		{
			func(obj, "<");
			SecurityElementType type = this.m_type;
			if (type != SecurityElementType.Format)
			{
				if (type == SecurityElementType.Comment)
				{
					func(obj, "!");
				}
			}
			else
			{
				func(obj, "?");
			}
			func(obj, this.m_strTag);
			if (this.m_lAttributes != null && this.m_lAttributes.Count > 0)
			{
				func(obj, " ");
				int count = this.m_lAttributes.Count;
				for (int i = 0; i < count; i += 2)
				{
					string str = (string)this.m_lAttributes[i];
					string str2 = (string)this.m_lAttributes[i + 1];
					func(obj, str);
					func(obj, "=\"");
					func(obj, str2);
					func(obj, "\"");
					if (i != this.m_lAttributes.Count - 2)
					{
						if (this.m_type == SecurityElementType.Regular)
						{
							func(obj, Environment.NewLine);
						}
						else
						{
							func(obj, " ");
						}
					}
				}
			}
			if (this.m_strText == null && (this.m_lChildren == null || this.m_lChildren.Count == 0))
			{
				type = this.m_type;
				if (type != SecurityElementType.Format)
				{
					if (type == SecurityElementType.Comment)
					{
						func(obj, ">");
					}
					else
					{
						func(obj, "/>");
					}
				}
				else
				{
					func(obj, " ?>");
				}
				func(obj, Environment.NewLine);
				return;
			}
			func(obj, ">");
			func(obj, this.m_strText);
			if (this.m_lChildren != null)
			{
				this.ConvertSecurityElementFactories();
				func(obj, Environment.NewLine);
				for (int j = 0; j < this.m_lChildren.Count; j++)
				{
					((SecurityElement)this.m_lChildren[j]).ToString("", obj, func);
				}
			}
			func(obj, "</");
			func(obj, this.m_strTag);
			func(obj, ">");
			func(obj, Environment.NewLine);
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x000603AC File Offset: 0x0005E5AC
		public string Attribute(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.m_lAttributes == null)
			{
				return null;
			}
			int count = this.m_lAttributes.Count;
			for (int i = 0; i < count; i += 2)
			{
				string a = (string)this.m_lAttributes[i];
				if (string.Equals(a, name))
				{
					string str = (string)this.m_lAttributes[i + 1];
					return SecurityElement.Unescape(str);
				}
			}
			return null;
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x00060420 File Offset: 0x0005E620
		public SecurityElement SearchForChildByTag(string tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (this.m_lChildren == null)
			{
				return null;
			}
			foreach (object obj in this.m_lChildren)
			{
				SecurityElement securityElement = (SecurityElement)obj;
				if (securityElement != null && string.Equals(securityElement.Tag, tag))
				{
					return securityElement;
				}
			}
			return null;
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x0006047C File Offset: 0x0005E67C
		internal IPermission ToPermission(bool ignoreTypeLoadFailures)
		{
			IPermission permission = XMLUtil.CreatePermission(this, PermissionState.None, ignoreTypeLoadFailures);
			if (permission == null)
			{
				return null;
			}
			permission.FromXml(this);
			PermissionToken token = PermissionToken.GetToken(permission);
			return permission;
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x000604A8 File Offset: 0x0005E6A8
		[SecurityCritical]
		internal object ToSecurityObject()
		{
			string strTag = this.m_strTag;
			if (strTag == "PermissionSet")
			{
				PermissionSet permissionSet = new PermissionSet(PermissionState.None);
				permissionSet.FromXml(this);
				return permissionSet;
			}
			return this.ToPermission(false);
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x000604E0 File Offset: 0x0005E6E0
		internal string SearchForTextOfLocalName(string strLocalName)
		{
			if (strLocalName == null)
			{
				throw new ArgumentNullException("strLocalName");
			}
			if (this.m_strTag == null)
			{
				return null;
			}
			if (this.m_strTag.Equals(strLocalName) || this.m_strTag.EndsWith(":" + strLocalName, StringComparison.Ordinal))
			{
				return SecurityElement.Unescape(this.m_strText);
			}
			if (this.m_lChildren == null)
			{
				return null;
			}
			foreach (object obj in this.m_lChildren)
			{
				string text = ((SecurityElement)obj).SearchForTextOfLocalName(strLocalName);
				if (text != null)
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x00060570 File Offset: 0x0005E770
		public string SearchForTextOfTag(string tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (string.Equals(this.m_strTag, tag))
			{
				return SecurityElement.Unescape(this.m_strText);
			}
			if (this.m_lChildren == null)
			{
				return null;
			}
			IEnumerator enumerator = this.m_lChildren.GetEnumerator();
			this.ConvertSecurityElementFactories();
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				string text = ((SecurityElement)obj).SearchForTextOfTag(tag);
				if (text != null)
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x040009A9 RID: 2473
		internal string m_strTag;

		// Token: 0x040009AA RID: 2474
		internal string m_strText;

		// Token: 0x040009AB RID: 2475
		private ArrayList m_lChildren;

		// Token: 0x040009AC RID: 2476
		internal ArrayList m_lAttributes;

		// Token: 0x040009AD RID: 2477
		internal SecurityElementType m_type;

		// Token: 0x040009AE RID: 2478
		private static readonly char[] s_tagIllegalCharacters = new char[]
		{
			' ',
			'<',
			'>'
		};

		// Token: 0x040009AF RID: 2479
		private static readonly char[] s_textIllegalCharacters = new char[]
		{
			'<',
			'>'
		};

		// Token: 0x040009B0 RID: 2480
		private static readonly char[] s_valueIllegalCharacters = new char[]
		{
			'<',
			'>',
			'"'
		};

		// Token: 0x040009B1 RID: 2481
		private const string s_strIndent = "   ";

		// Token: 0x040009B2 RID: 2482
		private const int c_AttributesTypical = 8;

		// Token: 0x040009B3 RID: 2483
		private const int c_ChildrenTypical = 1;

		// Token: 0x040009B4 RID: 2484
		private static readonly string[] s_escapeStringPairs = new string[]
		{
			"<",
			"&lt;",
			">",
			"&gt;",
			"\"",
			"&quot;",
			"'",
			"&apos;",
			"&",
			"&amp;"
		};

		// Token: 0x040009B5 RID: 2485
		private static readonly char[] s_escapeChars = new char[]
		{
			'<',
			'>',
			'"',
			'\'',
			'&'
		};

		// Token: 0x02000AFD RID: 2813
		// (Invoke) Token: 0x06006A40 RID: 27200
		private delegate void ToStringHelperFunc(object obj, string str);
	}
}
