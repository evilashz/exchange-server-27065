using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Security.OAuth.OAuthProtocols
{
	// Token: 0x020000E7 RID: 231
	internal abstract class OAuth2Message
	{
		// Token: 0x060007C4 RID: 1988 RVA: 0x00035E77 File Offset: 0x00034077
		public override string ToString()
		{
			return this.Encode();
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00035E7F File Offset: 0x0003407F
		protected bool ContainsKey(string key)
		{
			return this._message.ContainsKey(key);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00035E8D File Offset: 0x0003408D
		protected void Decode(string message)
		{
			this._message.Decode(message);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00035E9B File Offset: 0x0003409B
		protected void DecodeFromJson(string message)
		{
			this._message.DecodeFromJson(message);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00035EA9 File Offset: 0x000340A9
		protected string Encode()
		{
			return this._message.Encode();
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00035EB6 File Offset: 0x000340B6
		protected string EncodeToJson()
		{
			return this._message.EncodeToJson();
		}

		// Token: 0x170001A8 RID: 424
		protected string this[string index]
		{
			get
			{
				return this.GetValue(index);
			}
			set
			{
				this._message[index] = value;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x00035EDB File Offset: 0x000340DB
		protected IEnumerable<string> Keys
		{
			get
			{
				return this._message.Keys;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x00035EE8 File Offset: 0x000340E8
		public Dictionary<string, string> Message
		{
			get
			{
				return this._message;
			}
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00035EF0 File Offset: 0x000340F0
		protected string GetValue(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentException("The input string parameter is either null or empty.", "key");
			}
			string result = null;
			this._message.TryGetValue(key, out result);
			return result;
		}

		// Token: 0x0400074A RID: 1866
		private Dictionary<string, string> _message = new Dictionary<string, string>(StringComparer.Ordinal);
	}
}
