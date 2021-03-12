using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000A3 RID: 163
	internal class GcmPayloadWriter
	{
		// Token: 0x060005B1 RID: 1457 RVA: 0x00012DE7 File Offset: 0x00010FE7
		public GcmPayloadWriter()
		{
			this.properties = new List<string>();
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00012DFA File Offset: 0x00010FFA
		public void WriteProperty(string key, string value)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("key", key);
			if (!string.IsNullOrWhiteSpace(value))
			{
				this.AddProperty(key, value);
			}
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00012E18 File Offset: 0x00011018
		public void WriteProperty<T>(string key, T? value) where T : struct
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("key", key);
			if (value != null)
			{
				T value2 = value.Value;
				this.AddProperty(key, value2.ToString());
			}
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00012E58 File Offset: 0x00011058
		public void WriteProperty<T>(string key, T value) where T : struct
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("key", key);
			if (!object.Equals(value, default(T)))
			{
				this.AddProperty(key, value.ToString());
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00012E9F File Offset: 0x0001109F
		public override string ToString()
		{
			return string.Join("&", this.properties);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00012EB1 File Offset: 0x000110B1
		private void AddProperty(string key, string value)
		{
			this.properties.Add(string.Format("{0}{1}{2}", Uri.EscapeUriString(key), "=", Uri.EscapeUriString(value)));
		}

		// Token: 0x040002C5 RID: 709
		public const string PropertySeparator = "&";

		// Token: 0x040002C6 RID: 710
		public const string ValueSeparator = "=";

		// Token: 0x040002C7 RID: 711
		private List<string> properties;
	}
}
