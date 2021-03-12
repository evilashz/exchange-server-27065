using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000173 RID: 371
	public class MRSSettingsLogCollection
	{
		// Token: 0x06000E3A RID: 3642 RVA: 0x000206EC File Offset: 0x0001E8EC
		public MRSSettingsLogCollection(string settingsString)
		{
			if (string.IsNullOrWhiteSpace(settingsString))
			{
				return;
			}
			string[] array = settingsString.Split(new char[]
			{
				';'
			});
			foreach (string text in array)
			{
				string text2 = text.Trim();
				if (!string.IsNullOrWhiteSpace(text2))
				{
					this.SettingsLogCollection.Add(new MRSSettingsLogCollection.MRSSettingsLogElement(text2));
				}
			}
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x00020764 File Offset: 0x0001E964
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (MRSSettingsLogCollection.MRSSettingsLogElement mrssettingsLogElement in this.SettingsLogCollection)
			{
				if (mrssettingsLogElement != null && !string.IsNullOrWhiteSpace(mrssettingsLogElement.SettingName))
				{
					if (!flag)
					{
						stringBuilder.Append(';');
						flag = false;
					}
					stringBuilder.AppendFormat("{0}", mrssettingsLogElement.SettingName);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400080A RID: 2058
		internal readonly List<MRSSettingsLogCollection.MRSSettingsLogElement> SettingsLogCollection = new List<MRSSettingsLogCollection.MRSSettingsLogElement>();

		// Token: 0x02000174 RID: 372
		public class MRSSettingsLogElement
		{
			// Token: 0x17000470 RID: 1136
			// (get) Token: 0x06000E3C RID: 3644 RVA: 0x000207F0 File Offset: 0x0001E9F0
			// (set) Token: 0x06000E3D RID: 3645 RVA: 0x000207F8 File Offset: 0x0001E9F8
			public string SettingName { get; set; }

			// Token: 0x06000E3E RID: 3646 RVA: 0x00020801 File Offset: 0x0001EA01
			public MRSSettingsLogElement(string inputSettingName)
			{
				this.SettingName = inputSettingName;
			}
		}

		// Token: 0x02000175 RID: 373
		public class MRSSettingsLogCollectionConverter : TypeConverter
		{
			// Token: 0x06000E3F RID: 3647 RVA: 0x00020810 File Offset: 0x0001EA10
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x06000E40 RID: 3648 RVA: 0x0002082E File Offset: 0x0001EA2E
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x06000E41 RID: 3649 RVA: 0x0002084C File Offset: 0x0001EA4C
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				if (value == null || value is string)
				{
					return new MRSSettingsLogCollection((string)value);
				}
				return base.ConvertFrom(context, culture, value);
			}

			// Token: 0x06000E42 RID: 3650 RVA: 0x00020870 File Offset: 0x0001EA70
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(MRSSettingsLogCollection))
				{
					return this.ConvertFrom(context, culture, value);
				}
				MRSSettingsLogCollection mrssettingsLogCollection = value as MRSSettingsLogCollection;
				if (mrssettingsLogCollection == null)
				{
					throw new ArgumentException("Converted value is not of MRSSettingsLogCollection type");
				}
				if (destinationType == typeof(string))
				{
					return mrssettingsLogCollection.ToString();
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
	}
}
