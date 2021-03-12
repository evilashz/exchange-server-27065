using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000255 RID: 597
	internal class SmimeParameterParser
	{
		// Token: 0x06001414 RID: 5140 RVA: 0x0007AEE8 File Offset: 0x000790E8
		public SmimeParameterParser(string smimeParameter)
		{
			string[] array = smimeParameter.Split(new char[]
			{
				';'
			});
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (string text in array)
			{
				if (!string.IsNullOrEmpty(text))
				{
					string[] array3 = text.Split(new char[]
					{
						':'
					});
					if (array3.Length != 2)
					{
						throw new OwaInvalidRequestException("Invalid S/MIME Parameter, the format should be  [key1]:[value1];[key2]:[value2]...");
					}
					dictionary.Add(array3[0], array3[1]);
				}
			}
			if (!dictionary.TryGetValue("Ver", out this.smimeControlVersion))
			{
				this.smimeControlVersion = null;
			}
			string text2;
			if (dictionary.TryGetValue("SSL", out text2) && text2.Equals("1"))
			{
				this.connectionIsSSL = true;
				return;
			}
			this.connectionIsSSL = false;
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x0007AFB6 File Offset: 0x000791B6
		public bool ConnectionIsSSL
		{
			get
			{
				return this.connectionIsSSL;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x0007AFBE File Offset: 0x000791BE
		public string SmimeControlVersion
		{
			get
			{
				return this.smimeControlVersion;
			}
		}

		// Token: 0x04000DBF RID: 3519
		private const string StatusSSLKeyName = "SSL";

		// Token: 0x04000DC0 RID: 3520
		private const string StatusVersionKeyName = "Ver";

		// Token: 0x04000DC1 RID: 3521
		private const string BoolTrueValueName = "1";

		// Token: 0x04000DC2 RID: 3522
		private const char KeyValuePairsSeperator = ';';

		// Token: 0x04000DC3 RID: 3523
		private const char KeyValueSeperator = ':';

		// Token: 0x04000DC4 RID: 3524
		private bool connectionIsSSL;

		// Token: 0x04000DC5 RID: 3525
		private string smimeControlVersion;
	}
}
