using System;
using System.Collections;
using System.Text;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000162 RID: 354
	public class ToStringConverter : IOutputConverter, IDDIConverter
	{
		// Token: 0x060021EA RID: 8682 RVA: 0x000662AC File Offset: 0x000644AC
		public ToStringConverter(ConvertMode convertMode)
		{
			this.ConvertMode = convertMode;
		}

		// Token: 0x17001A87 RID: 6791
		// (get) Token: 0x060021EB RID: 8683 RVA: 0x000662BB File Offset: 0x000644BB
		// (set) Token: 0x060021EC RID: 8684 RVA: 0x000662C3 File Offset: 0x000644C3
		public ConvertMode ConvertMode { get; private set; }

		// Token: 0x060021ED RID: 8685 RVA: 0x000662CC File Offset: 0x000644CC
		public bool CanConvert(object sourceValue)
		{
			return true;
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x000662D0 File Offset: 0x000644D0
		public object Convert(object sourceValue)
		{
			string result = string.Empty;
			if (sourceValue != null)
			{
				if (this.ConvertMode == ConvertMode.PerItemInEnumerable)
				{
					IEnumerable enumerable = sourceValue as IEnumerable;
					if (enumerable != null)
					{
						StringBuilder stringBuilder = new StringBuilder();
						bool flag = true;
						foreach (object obj in enumerable)
						{
							if (flag)
							{
								flag = false;
							}
							else
							{
								stringBuilder.Append(',');
							}
							stringBuilder.Append(obj.ToString());
						}
						result = stringBuilder.ToString();
					}
				}
				else
				{
					result = sourceValue.ToString();
				}
			}
			return result;
		}
	}
}
