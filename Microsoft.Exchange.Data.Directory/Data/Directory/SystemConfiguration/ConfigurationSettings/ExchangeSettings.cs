using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x02000660 RID: 1632
	[Serializable]
	public class ExchangeSettings : InternalExchangeSettings
	{
		// Token: 0x17001923 RID: 6435
		// (get) Token: 0x06004C67 RID: 19559 RVA: 0x0011A803 File Offset: 0x00118A03
		public MultiValuedProperty<SettingsGroup> Groups
		{
			get
			{
				if (base.Settings == null)
				{
					return null;
				}
				return new MultiValuedProperty<SettingsGroup>(true, null, base.Settings.Values.ToArray<SettingsGroup>());
			}
		}

		// Token: 0x17001924 RID: 6436
		// (get) Token: 0x06004C68 RID: 19560 RVA: 0x0011A826 File Offset: 0x00118A26
		public MultiValuedProperty<string> GroupNames
		{
			get
			{
				if (base.Settings == null)
				{
					return null;
				}
				return new MultiValuedProperty<string>(true, null, base.Settings.Keys.ToArray<string>());
			}
		}

		// Token: 0x17001925 RID: 6437
		// (get) Token: 0x06004C69 RID: 19561 RVA: 0x0011A849 File Offset: 0x00118A49
		public override XMLSerializableDictionary<string, SettingsHistory> History
		{
			get
			{
				return (XMLSerializableDictionary<string, SettingsHistory>)this[ExchangeSettingsSchema.History];
			}
		}

		// Token: 0x17001926 RID: 6438
		// (get) Token: 0x06004C6A RID: 19562 RVA: 0x0011A85B File Offset: 0x00118A5B
		// (set) Token: 0x06004C6B RID: 19563 RVA: 0x0011A863 File Offset: 0x00118A63
		public string DiagnosticInfo { get; set; }

		// Token: 0x17001927 RID: 6439
		// (get) Token: 0x06004C6C RID: 19564 RVA: 0x0011A86C File Offset: 0x00118A6C
		// (set) Token: 0x06004C6D RID: 19565 RVA: 0x0011A874 File Offset: 0x00118A74
		public KeyValuePair<string, object> EffectiveSetting { get; set; }

		// Token: 0x06004C6E RID: 19566 RVA: 0x0011A8A4 File Offset: 0x00118AA4
		public override string ToString()
		{
			List<SettingsGroup> list = new List<SettingsGroup>(base.Settings.Values);
			list.Sort((SettingsGroup x, SettingsGroup y) => y.Priority.CompareTo(x.Priority));
			StringBuilder stringBuilder = new StringBuilder();
			foreach (SettingsGroup settingsGroup in list)
			{
				stringBuilder.AppendLine(settingsGroup.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04003452 RID: 13394
		private static ExchangeSettingsSchema schema = ObjectSchema.GetInstance<ExchangeSettingsSchema>();
	}
}
