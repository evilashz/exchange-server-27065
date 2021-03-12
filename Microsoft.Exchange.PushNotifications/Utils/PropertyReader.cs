using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Utils
{
	// Token: 0x02000043 RID: 67
	internal sealed class PropertyReader
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x00005858 File Offset: 0x00003A58
		public PropertyReader(string[] propertySeparator, string[] valueSeparator)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("propertySeparator", propertySeparator);
			ArgumentValidator.ThrowIfNullOrEmpty("valueSeparator", valueSeparator);
			this.PropertySeparator = propertySeparator;
			this.ValueSeparator = valueSeparator;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00005884 File Offset: 0x00003A84
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x0000588C File Offset: 0x00003A8C
		private string[] PropertySeparator { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00005895 File Offset: 0x00003A95
		// (set) Token: 0x060001AA RID: 426 RVA: 0x0000589D File Offset: 0x00003A9D
		private string[] ValueSeparator { get; set; }

		// Token: 0x060001AB RID: 427 RVA: 0x000058A8 File Offset: 0x00003AA8
		public Dictionary<string, string> Read(string payload)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			if (!string.IsNullOrWhiteSpace(payload))
			{
				string[] array = payload.Split(this.PropertySeparator, StringSplitOptions.RemoveEmptyEntries);
				foreach (string text in array)
				{
					string[] array3 = text.Split(this.ValueSeparator, StringSplitOptions.RemoveEmptyEntries);
					if (array3.Length == 2)
					{
						dictionary.Add(Uri.UnescapeDataString(array3[0]), Uri.UnescapeDataString(array3[1]));
					}
				}
			}
			return dictionary;
		}
	}
}
