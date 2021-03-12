using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace Microsoft.Exchange.Management.DetailsTemplates
{
	// Token: 0x0200001D RID: 29
	internal class DetailsTemplatesNameCreationService : INameCreationService
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x000061B4 File Offset: 0x000043B4
		public string CreateName(IContainer container, Type dataType)
		{
			if (dataType == null)
			{
				throw new ArgumentNullException("dataType");
			}
			string text = dataType.Name;
			if (container != null)
			{
				int num = 0;
				string text2;
				do
				{
					num++;
					text2 = string.Format(CultureInfo.CurrentCulture, "{0}{1}", new object[]
					{
						text,
						num.ToString(CultureInfo.InvariantCulture)
					});
				}
				while (container.Components[text2] != null);
				text = text2;
			}
			return text;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00006221 File Offset: 0x00004421
		public bool IsValidName(string name)
		{
			return true;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00006224 File Offset: 0x00004424
		public void ValidateName(string name)
		{
		}
	}
}
