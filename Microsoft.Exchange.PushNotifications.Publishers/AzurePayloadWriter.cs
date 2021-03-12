using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200004F RID: 79
	internal class AzurePayloadWriter
	{
		// Token: 0x06000308 RID: 776 RVA: 0x0000ACAC File Offset: 0x00008EAC
		public AzurePayloadWriter()
		{
			this.properties = new List<string>();
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000ACBF File Offset: 0x00008EBF
		public void WriteProperty(string key, string value)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("key", key);
			if (!string.IsNullOrWhiteSpace(value))
			{
				this.AddProperty(key, value);
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000ACDC File Offset: 0x00008EDC
		public void WriteProperty<T>(string key, T? value) where T : struct
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("key", key);
			if (value != null)
			{
				T value2 = value.Value;
				this.AddProperty(key, value2.ToString());
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000AD19 File Offset: 0x00008F19
		public void WriteProperty<T>(string key, T value) where T : struct
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("key", key);
			this.AddProperty(key, value.ToString());
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000AD3A File Offset: 0x00008F3A
		public override string ToString()
		{
			return string.Format("{{{0}}}", string.Join(", ", this.properties));
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000AD56 File Offset: 0x00008F56
		private void AddProperty(string key, string value)
		{
			this.properties.Add(string.Format("\"{0}\": \"{1}\"", key, value));
		}

		// Token: 0x04000144 RID: 324
		public const string PropertySeparator = ", ";

		// Token: 0x04000145 RID: 325
		private List<string> properties;
	}
}
