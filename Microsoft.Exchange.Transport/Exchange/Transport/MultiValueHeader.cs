using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Transport.Storage.Messaging;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020000FE RID: 254
	internal class MultiValueHeader
	{
		// Token: 0x06000A41 RID: 2625 RVA: 0x00025E0F File Offset: 0x0002400F
		internal MultiValueHeader(IMailItemStorage mailItem, string headerName)
		{
			if (mailItem == null)
			{
				throw new ArgumentNullException("mailItem");
			}
			if (string.IsNullOrEmpty(headerName))
			{
				throw new ArgumentException("headerName cannot be null or empty", "headerName");
			}
			this.mailItem = mailItem;
			this.headerName = headerName;
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x00025E4B File Offset: 0x0002404B
		public string HeaderName
		{
			get
			{
				return this.headerName;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x00025E53 File Offset: 0x00024053
		private Dictionary<string, string> PropertyBag
		{
			get
			{
				if (this.propertyBag == null)
				{
					this.propertyBag = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
					this.Deserialize();
				}
				return this.propertyBag;
			}
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00025E7C File Offset: 0x0002407C
		public void SetStringValue(string property, string value)
		{
			if (string.IsNullOrEmpty(property))
			{
				throw new ArgumentException("Cannot set property to empty or null string", "property");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!MultiValueHeader.IsValidToken(property) || !MultiValueHeader.IsValidToken(value))
			{
				throw new FormatException("Invalid property format");
			}
			string a;
			if (this.PropertyBag.TryGetValue(property, out a) && string.Equals(a, value, StringComparison.OrdinalIgnoreCase))
			{
				return;
			}
			this.PropertyBag[property] = value;
			string headerValue = this.Serialize();
			this.UpdateMimeDoc(headerValue);
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00025F00 File Offset: 0x00024100
		public void SetBoolValue(string property, bool value)
		{
			this.SetStringValue(property, value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00025F15 File Offset: 0x00024115
		public void SetInt32Value(string property, int value)
		{
			this.SetStringValue(property, value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00025F2A File Offset: 0x0002412A
		public bool TryGetStringValue(string property, out string value)
		{
			return this.PropertyBag.TryGetValue(property, out value);
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00025F3C File Offset: 0x0002413C
		public bool TryGetBoolValue(string property, out bool value)
		{
			value = false;
			string value2;
			return this.TryGetStringValue(property, out value2) && bool.TryParse(value2, out value);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00025F64 File Offset: 0x00024164
		public bool TryGetInt32Value(string property, out int value)
		{
			value = 0;
			string s;
			return this.TryGetStringValue(property, out s) && int.TryParse(s, out value);
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00025F98 File Offset: 0x00024198
		private static bool IsValidToken(string token)
		{
			char[] anyOf = new char[]
			{
				'\r',
				'\n',
				';',
				'='
			};
			return token.IndexOfAny(anyOf) == -1;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00025FC4 File Offset: 0x000241C4
		private string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, string> keyValuePair in this.PropertyBag)
			{
				stringBuilder.AppendFormat("{0}={1}; ", keyValuePair.Key, keyValuePair.Value);
			}
			if (stringBuilder.Length > 2)
			{
				stringBuilder.Length -= 2;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00026050 File Offset: 0x00024250
		private void Deserialize()
		{
			if (this.mailItem.MimeDocument.RootPart == null)
			{
				throw new InvalidOperationException("MimeDocument.RootPart is not accessible");
			}
			char[] separator = new char[]
			{
				';'
			};
			char[] separator2 = new char[]
			{
				'='
			};
			HeaderList headers = this.mailItem.MimeDocument.RootPart.Headers;
			Header header = headers.FindFirst(this.headerName);
			if (header != null)
			{
				string value = header.Value;
				string[] array = value.Split(separator, StringSplitOptions.RemoveEmptyEntries);
				foreach (string text in array)
				{
					string[] array3 = text.Split(separator2);
					if (array3.Length == 2)
					{
						this.PropertyBag[array3[0].Trim()] = array3[1].Trim();
					}
				}
			}
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00026124 File Offset: 0x00024324
		private void UpdateMimeDoc(string headerValue)
		{
			if (this.mailItem.MimeDocument.RootPart == null)
			{
				throw new InvalidOperationException("MimeDocument.RootPart is not accessible");
			}
			if (!string.IsNullOrEmpty(headerValue))
			{
				HeaderList headers = this.mailItem.MimeDocument.RootPart.Headers;
				Header header = headers.FindFirst(this.headerName);
				if (header == null)
				{
					headers.AppendChild(new TextHeader(this.headerName, headerValue));
					return;
				}
				header.Value = headerValue;
			}
		}

		// Token: 0x0400047D RID: 1149
		private readonly IMailItemStorage mailItem;

		// Token: 0x0400047E RID: 1150
		private readonly string headerName;

		// Token: 0x0400047F RID: 1151
		private Dictionary<string, string> propertyBag;
	}
}
