using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020002BF RID: 703
	internal class PiiMapManager
	{
		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06001943 RID: 6467 RVA: 0x0004F894 File Offset: 0x0004DA94
		public static PiiMapManager Instance
		{
			get
			{
				if (PiiMapManager.instance == null)
				{
					lock (PiiMapManager.syncObject)
					{
						if (PiiMapManager.instance == null)
						{
							PiiMapManager.instance = new PiiMapManager();
						}
					}
				}
				return PiiMapManager.instance;
			}
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x0004F8EC File Offset: 0x0004DAEC
		private PiiMapManager()
		{
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x0004F8FF File Offset: 0x0004DAFF
		public static bool ContainsRedactedPiiValue(string value)
		{
			return !string.IsNullOrEmpty(value) && PiiMapManager.detectRedactedPii.IsMatch(value);
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x0004F924 File Offset: 0x0004DB24
		public string ResolveRedactedValue(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			string text = value.Replace(SuppressingPiiConfig.RedactedDataPrefix, string.Empty);
			string text2 = PiiMapManager.detectRedactedPii.Replace(text, (Match x) => this.Lookup(x.Value));
			if (!(text2 == text))
			{
				return text2;
			}
			return value;
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x0004F977 File Offset: 0x0004DB77
		public PiiMap GetOrAdd(string session)
		{
			if (string.IsNullOrEmpty(session))
			{
				throw new ArgumentException("User name should not be null or empty");
			}
			return this.piiMaps.GetOrAdd(session, (string x) => new PiiMap());
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x0004F9B8 File Offset: 0x0004DBB8
		public void Remove(string session)
		{
			if (string.IsNullOrEmpty(session))
			{
				throw new ArgumentException("Session ID should not be null or empty");
			}
			PiiMap piiMap;
			this.piiMaps.TryRemove(session, out piiMap);
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x0004F9F0 File Offset: 0x0004DBF0
		private string Lookup(string redactedValue)
		{
			if (string.IsNullOrEmpty(redactedValue))
			{
				return redactedValue;
			}
			string text = null;
			foreach (PiiMap piiMap in from x in this.piiMaps.Values
			where x != null
			select x)
			{
				text = piiMap[redactedValue];
				if (text != null)
				{
					break;
				}
			}
			if (text == null)
			{
				return redactedValue;
			}
			return text;
		}

		// Token: 0x04000F0F RID: 3855
		private static readonly Regex detectRedactedPii = new Regex("[0-9A-Fa-f]{32}", RegexOptions.Compiled);

		// Token: 0x04000F10 RID: 3856
		private ConcurrentDictionary<string, PiiMap> piiMaps = new ConcurrentDictionary<string, PiiMap>();

		// Token: 0x04000F11 RID: 3857
		private static PiiMapManager instance;

		// Token: 0x04000F12 RID: 3858
		private static object syncObject = new object();
	}
}
