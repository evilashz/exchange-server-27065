using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ClassificationDefinitions;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x020001B3 RID: 435
	public static class DataClassificationService
	{
		// Token: 0x060023C2 RID: 9154 RVA: 0x0006D7C0 File Offset: 0x0006B9C0
		public static void PostGetListAction(DataRow inputrow, DataTable dataTable, DataObjectStore store)
		{
			List<DataRow> list = new List<DataRow>();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (dataRow["ClassificationType"].ToString() == "Entity")
				{
					list.Add(dataRow);
				}
				DataClassificationObjectId dataClassificationObjectId = (DataClassificationObjectId)dataRow["Identity"];
				dataRow["Identity"] = new Identity(dataClassificationObjectId);
			}
			Array.ForEach<DataRow>(list.ToArray(), delegate(DataRow r)
			{
				dataTable.Rows.Remove(r);
			});
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x0006D894 File Offset: 0x0006BA94
		public static void NewObjectPreAction(DataRow inputrow, DataTable dataTable, DataObjectStore store)
		{
			Microsoft.Exchange.Management.ClassificationDefinitions.Fingerprint[] fingerprints = DataClassificationService.GetFingerprints((object[])inputrow["FingerprintNames"]);
			inputrow["Fingerprints"] = fingerprints;
			store.ModifiedColumns.Add("Fingerprints");
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x0006D8D4 File Offset: 0x0006BAD4
		public static void LanguagesAddedPreAction(DataRow inputrow, DataTable dataTable, DataObjectStore store)
		{
			DataClassificationService.LanguageSetting languageSetting = (DataClassificationService.LanguageSetting)inputrow["CurrentLanguage"];
			inputrow["Locale"] = languageSetting.Locale;
			inputrow["Name"] = languageSetting.Name;
			inputrow["Description"] = languageSetting.Description;
			store.ModifiedColumns.Add("Locale");
			store.ModifiedColumns.Add("Name");
			store.ModifiedColumns.Add("Description");
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x0006D958 File Offset: 0x0006BB58
		public static void LanguagesRemovedPreAction(DataRow inputrow, DataTable dataTable, DataObjectStore store)
		{
			DataClassificationService.LanguageSetting languageSetting = (DataClassificationService.LanguageSetting)inputrow["CurrentLanguage"];
			inputrow["Locale"] = languageSetting.Locale;
			inputrow["Name"] = null;
			inputrow["Description"] = null;
			store.ModifiedColumns.Add("Locale");
			store.ModifiedColumns.Add("Name");
			store.ModifiedColumns.Add("Description");
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x0006D9D0 File Offset: 0x0006BBD0
		private static Microsoft.Exchange.Management.ClassificationDefinitions.Fingerprint[] GetFingerprints(object[] fingerprints)
		{
			List<Microsoft.Exchange.Management.ClassificationDefinitions.Fingerprint> list = new List<Microsoft.Exchange.Management.ClassificationDefinitions.Fingerprint>();
			foreach (object obj in fingerprints)
			{
				list.Add(Microsoft.Exchange.Management.ClassificationDefinitions.Fingerprint.Parse(obj.ToString()));
			}
			return list.ToArray();
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x0006DA10 File Offset: 0x0006BC10
		public static void GetForSDOPostAction(DataRow inputrow, DataTable dataTable, DataObjectStore store)
		{
			if (dataTable.Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = dataTable.Rows[0];
			if (!DBNull.Value.Equals(dataRow["Fingerprints"]))
			{
				List<string> list = new List<string>();
				foreach (Microsoft.Exchange.Management.ClassificationDefinitions.Fingerprint fingerprint in ((MultiValuedProperty<Microsoft.Exchange.Management.ClassificationDefinitions.Fingerprint>)dataRow["Fingerprints"]))
				{
					string item = fingerprint.Description.Substring(fingerprint.Description.LastIndexOf("\\") + 1);
					list.Add(item);
				}
				dataRow["Fingerprints"] = list;
				dataTable.Rows[0]["FingerprintFileList"] = list.ToArray();
			}
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x0006DAF0 File Offset: 0x0006BCF0
		public static void GetObjectPostAction(DataRow inputrow, DataTable dataTable, DataObjectStore store)
		{
			if (dataTable.Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = dataTable.Rows[0];
			if (!DBNull.Value.Equals(dataRow["AllLocalizedNames"]))
			{
				List<DataClassificationService.LanguageSetting> list = new List<DataClassificationService.LanguageSetting>();
				Dictionary<CultureInfo, string> dictionary = (Dictionary<CultureInfo, string>)dataRow["AllLocalizedDescriptions"];
				foreach (KeyValuePair<CultureInfo, string> keyValuePair in ((Dictionary<CultureInfo, string>)dataRow["AllLocalizedNames"]))
				{
					DataClassificationService.LanguageSetting item = new DataClassificationService.LanguageSetting
					{
						Locale = keyValuePair.Key.ToString(),
						Language = keyValuePair.Key.DisplayName,
						Name = keyValuePair.Value,
						Description = dictionary[keyValuePair.Key],
						IsDefault = (keyValuePair.Key.ToString() == dataRow["DefaultCulture"].ToString())
					};
					list.Add(item);
				}
				if (list.Count > 0)
				{
					dataRow["AllLocalizedNamesList"] = list.ToArray();
				}
			}
			List<Microsoft.Exchange.Management.ControlPanel.Fingerprint> list2 = new List<Microsoft.Exchange.Management.ControlPanel.Fingerprint>();
			MultiValuedProperty<Microsoft.Exchange.Management.ClassificationDefinitions.Fingerprint> multiValuedProperty = (MultiValuedProperty<Microsoft.Exchange.Management.ClassificationDefinitions.Fingerprint>)dataRow["Fingerprints"];
			foreach (Microsoft.Exchange.Management.ClassificationDefinitions.Fingerprint print in multiValuedProperty)
			{
				list2.Add(new Microsoft.Exchange.Management.ControlPanel.Fingerprint(print));
			}
			dataRow["Fingerprints"] = list2.ToArray();
			store.ModifiedColumns.Add("Fingerprints");
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x0006DCB8 File Offset: 0x0006BEB8
		public static DataClassificationService.LanguageSetting ConvertToLanguageSetting(object language)
		{
			return (DataClassificationService.LanguageSetting)language;
		}

		// Token: 0x020001B4 RID: 436
		[DataContract]
		public class LanguageSetting
		{
			// Token: 0x17001AE9 RID: 6889
			// (get) Token: 0x060023CA RID: 9162 RVA: 0x0006DCC0 File Offset: 0x0006BEC0
			// (set) Token: 0x060023CB RID: 9163 RVA: 0x0006DCC8 File Offset: 0x0006BEC8
			[DataMember]
			public string Locale { get; set; }

			// Token: 0x17001AEA RID: 6890
			// (get) Token: 0x060023CC RID: 9164 RVA: 0x0006DCD1 File Offset: 0x0006BED1
			// (set) Token: 0x060023CD RID: 9165 RVA: 0x0006DCD9 File Offset: 0x0006BED9
			[DataMember]
			public string Language { get; set; }

			// Token: 0x17001AEB RID: 6891
			// (get) Token: 0x060023CE RID: 9166 RVA: 0x0006DCE2 File Offset: 0x0006BEE2
			// (set) Token: 0x060023CF RID: 9167 RVA: 0x0006DCEA File Offset: 0x0006BEEA
			[DataMember]
			public string Name { get; set; }

			// Token: 0x17001AEC RID: 6892
			// (get) Token: 0x060023D0 RID: 9168 RVA: 0x0006DCF3 File Offset: 0x0006BEF3
			// (set) Token: 0x060023D1 RID: 9169 RVA: 0x0006DCFB File Offset: 0x0006BEFB
			[DataMember]
			public string Description { get; set; }

			// Token: 0x17001AED RID: 6893
			// (get) Token: 0x060023D2 RID: 9170 RVA: 0x0006DD04 File Offset: 0x0006BF04
			// (set) Token: 0x060023D3 RID: 9171 RVA: 0x0006DD0C File Offset: 0x0006BF0C
			[DataMember]
			public bool IsDefault { get; set; }
		}
	}
}
