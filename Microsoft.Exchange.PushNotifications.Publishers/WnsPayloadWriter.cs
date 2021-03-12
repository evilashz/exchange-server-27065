using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000ED RID: 237
	internal class WnsPayloadWriter
	{
		// Token: 0x06000792 RID: 1938 RVA: 0x00017AF8 File Offset: 0x00015CF8
		public WnsPayloadWriter()
		{
			this.stringBuilder = new StringBuilder();
			this.elementNames = new Stack<string>();
			this.ValidationErrors = new List<LocalizedString>();
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x00017B21 File Offset: 0x00015D21
		public bool IsValid
		{
			get
			{
				return this.ValidationErrors.Count == 0;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x00017B31 File Offset: 0x00015D31
		// (set) Token: 0x06000795 RID: 1941 RVA: 0x00017B39 File Offset: 0x00015D39
		public List<LocalizedString> ValidationErrors { get; private set; }

		// Token: 0x06000796 RID: 1942 RVA: 0x00017B42 File Offset: 0x00015D42
		public void WriteElementStart(string name, bool hasContent = false)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("name", name);
			this.stringBuilder.Append("<").Append(name);
			this.elementNames.Push(hasContent ? name : "/>");
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00017B7C File Offset: 0x00015D7C
		public void WriteAttribute(string name, int value)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("name", name);
			this.InternalWriteAttribute(name, value.ToString());
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00017B97 File Offset: 0x00015D97
		public void WriteAttribute(string name, string value, bool isOptional = false)
		{
			if (!this.CanSkipAttributeWriting(name, string.IsNullOrWhiteSpace(value), isOptional))
			{
				this.InternalWriteAttribute(name, value);
			}
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00017BB4 File Offset: 0x00015DB4
		public void WriteAttribute<T>(string name, T? nullableValue, bool isOptional = false) where T : struct
		{
			string value;
			if (nullableValue == null)
			{
				value = null;
			}
			else
			{
				T value2 = nullableValue.Value;
				value = value2.ToString();
			}
			this.WriteAttribute(name, value, isOptional);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00017BEC File Offset: 0x00015DEC
		public void WriteUriAttribute(string name, string serializedUri, bool isOptional = false)
		{
			if (this.CanSkipAttributeWriting(name, string.IsNullOrWhiteSpace(serializedUri), isOptional))
			{
				return;
			}
			try
			{
				Uri uri = new Uri(serializedUri, UriKind.RelativeOrAbsolute);
				StringComparer ordinalIgnoreCase = StringComparer.OrdinalIgnoreCase;
				if (uri.IsAbsoluteUri && !ordinalIgnoreCase.Equals(uri.Scheme, "https") && !ordinalIgnoreCase.Equals(uri.Scheme, "http") && !ordinalIgnoreCase.Equals(uri.Scheme, "ms-appx") && !ordinalIgnoreCase.Equals(uri.Scheme, "ms-appdata"))
				{
					this.ValidationErrors.Add(Strings.InvalidWnsUriScheme(serializedUri));
				}
				else
				{
					this.InternalWriteAttribute(name, serializedUri);
				}
			}
			catch (UriFormatException ex)
			{
				this.ValidationErrors.Add(Strings.InvalidWnsUri(serializedUri, ex.Message));
			}
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00017CB4 File Offset: 0x00015EB4
		public void WriteLanguageAttribute(string name, string lang, bool isOptional = false)
		{
			if (this.CanSkipAttributeWriting(name, string.IsNullOrWhiteSpace(lang), isOptional))
			{
				return;
			}
			try
			{
				CultureInfo.GetCultureInfo(lang);
				this.InternalWriteAttribute(name, lang);
			}
			catch (ArgumentException ex)
			{
				this.ValidationErrors.Add(Strings.InvalidWnsLanguage(lang, ex.Message));
			}
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00017D10 File Offset: 0x00015F10
		public void WriteTemplateAttribute(string name, WnsTemplateDescription templateDescription, bool isOptional = false)
		{
			if (!this.CanSkipAttributeWriting(name, templateDescription == null, isOptional))
			{
				this.InternalWriteAttribute(name, templateDescription.Name);
			}
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00017D30 File Offset: 0x00015F30
		public void WriteSoundAttribute(string name, WnsSound? sound, bool isOptional = false)
		{
			if (this.CanSkipAttributeWriting(name, sound == null, isOptional))
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder("ms-winsoundevent:Notification.", 16);
			if (sound >= WnsSound.Alarm)
			{
				stringBuilder.Append("Looping.");
			}
			stringBuilder.Append(sound);
			this.InternalWriteAttribute(name, stringBuilder.ToString());
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00017D9D File Offset: 0x00015F9D
		public void WriteAttributesEnd()
		{
			this.stringBuilder.Append(">");
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00017DB0 File Offset: 0x00015FB0
		public void WriteContent(string content)
		{
			if (!string.IsNullOrWhiteSpace(content))
			{
				this.stringBuilder.Append(this.XmlEscape(content));
			}
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00017DD0 File Offset: 0x00015FD0
		public void WriteElementEnd()
		{
			if (this.elementNames.Count == 0)
			{
				return;
			}
			string text = this.elementNames.Pop();
			if (text == "/>")
			{
				this.stringBuilder.Append(" ").Append(text);
				return;
			}
			this.stringBuilder.Append("</").Append(text).Append(">");
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00017E3D File Offset: 0x0001603D
		public string Dump()
		{
			if (this.IsValid)
			{
				return this.ToString();
			}
			throw new InvalidPushNotificationException(this.ValidationErrors[0]);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00017E5F File Offset: 0x0001605F
		public override string ToString()
		{
			return this.stringBuilder.ToString();
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00017E6C File Offset: 0x0001606C
		private bool CanSkipAttributeWriting(string name, bool isEmpty, bool isOptional)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("name", name);
			if (isEmpty && !isOptional)
			{
				this.ValidationErrors.Add(Strings.InvalidWnsAttributeIsMandatory(name));
			}
			return isEmpty;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00017E91 File Offset: 0x00016091
		private void InternalWriteAttribute(string name, string value)
		{
			this.stringBuilder.Append(" ").Append(name).Append("=\"").Append(this.XmlEscape(value)).Append("\"");
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00017ECA File Offset: 0x000160CA
		private string XmlEscape(string unescapedString)
		{
			return SecurityElement.Escape(unescapedString);
		}

		// Token: 0x04000429 RID: 1065
		private const string Opener = "<";

		// Token: 0x0400042A RID: 1066
		private const string ElementCloserNoContent = "/>";

		// Token: 0x0400042B RID: 1067
		private const string ElementCloserContent = "</";

		// Token: 0x0400042C RID: 1068
		private const string Closer = ">";

		// Token: 0x0400042D RID: 1069
		private const string Space = " ";

		// Token: 0x0400042E RID: 1070
		private const string AttributeOpener = "=\"";

		// Token: 0x0400042F RID: 1071
		private const string AttributeCloser = "\"";

		// Token: 0x04000430 RID: 1072
		private StringBuilder stringBuilder;

		// Token: 0x04000431 RID: 1073
		private Stack<string> elementNames;
	}
}
