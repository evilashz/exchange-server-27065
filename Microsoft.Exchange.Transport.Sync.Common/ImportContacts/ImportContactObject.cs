using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.ImportContacts
{
	// Token: 0x02000072 RID: 114
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ImportContactObject
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x00008625 File Offset: 0x00006825
		internal ImportContactObject(int index)
		{
			this.index = index;
			this.propertyBag = new Dictionary<ImportContactProperties, object>(ImportContactObject.propertyBagInitialSize);
			this.appendedDataToNotes = false;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000864B File Offset: 0x0000684B
		internal int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00008653 File Offset: 0x00006853
		internal int PropertyCount
		{
			get
			{
				return this.propertyBag.Count;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00008660 File Offset: 0x00006860
		internal Dictionary<ImportContactProperties, object> PropertyBag
		{
			get
			{
				return this.propertyBag;
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00008668 File Offset: 0x00006868
		internal void AddProperty(string propertyName, string value, Dictionary<string, ImportContactProperties> mappingDictionary, CultureInfo culture)
		{
			SyncUtilities.ThrowIfArgumentNull("propertyName", propertyName);
			SyncUtilities.ThrowIfArgumentNull("value", value);
			SyncUtilities.ThrowIfArgumentNull("mappingDictionary", mappingDictionary);
			SyncUtilities.ThrowIfArgumentNull("culture", culture);
			if (value == string.Empty)
			{
				return;
			}
			ImportContactProperties importContactProperties;
			if (!mappingDictionary.TryGetValue(propertyName, out importContactProperties))
			{
				this.AppendToNotes(propertyName, value, null);
				return;
			}
			if (ImportContactObject.ShouldIgnoreProperty(importContactProperties))
			{
				return;
			}
			object value2;
			if (!this.propertyBag.ContainsKey(importContactProperties) && this.GetValueObject(importContactProperties, value, culture, out value2))
			{
				this.propertyBag[importContactProperties] = value2;
				return;
			}
			this.AppendToNotes(propertyName, value, new ImportContactProperties?(importContactProperties));
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000870E File Offset: 0x0000690E
		private static bool ShouldIgnoreProperty(ImportContactProperties property)
		{
			return property == ImportContactProperties.IgnoredProperty || property == ImportContactProperties.Gender || property == ImportContactProperties.Priority || property == ImportContactProperties.Sensitivity;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00008724 File Offset: 0x00006924
		private void AppendToNotes(string propertyName, string value, ImportContactProperties? mappedProperty)
		{
			if (mappedProperty == ImportContactProperties.Anniversary || mappedProperty == ImportContactProperties.Birthday)
			{
				return;
			}
			if (!this.propertyBag.ContainsKey(ImportContactProperties.Notes))
			{
				string value2 = this.BuildNotesString(propertyName, value);
				this.propertyBag[ImportContactProperties.Notes] = value2;
				return;
			}
			string text = (string)this.propertyBag[ImportContactProperties.Notes];
			string value3;
			if (mappedProperty == ImportContactProperties.Notes)
			{
				value3 = value + '\n' + text;
			}
			else
			{
				value3 = text + '\n' + this.BuildNotesString(propertyName, value);
			}
			this.propertyBag[ImportContactProperties.Notes] = value3;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000087F0 File Offset: 0x000069F0
		private string BuildNotesString(string propertyName, string value)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "{0} : {1}", new object[]
			{
				propertyName,
				value
			});
			if (!this.appendedDataToNotes)
			{
				this.appendedDataToNotes = true;
				return ImportContactObject.FirstAppendToNotes + text;
			}
			return text;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000883C File Offset: 0x00006A3C
		private bool GetValueObject(ImportContactProperties property, string inputValue, CultureInfo culture, out object outputValue)
		{
			outputValue = null;
			switch (property)
			{
			case ImportContactProperties.Anniversary:
			case ImportContactProperties.Birthday:
				outputValue = this.GetDateTimeValue(inputValue, culture);
				goto IL_47;
			case ImportContactProperties.Categories:
			case ImportContactProperties.Children:
				outputValue = this.GetMultiValuedStrings(inputValue);
				goto IL_47;
			}
			outputValue = inputValue;
			IL_47:
			return outputValue != null && ImportContactXsoMapper.ValidatePropertyValue(property, outputValue);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000088A0 File Offset: 0x00006AA0
		private DateTime? GetDateTimeValue(string stringValue, CultureInfo culture)
		{
			CultureInfo cultureInfo = CultureInfo.GetCultureInfo("en-us");
			DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo)culture.DateTimeFormat.Clone();
			dateTimeFormatInfo.Calendar = cultureInfo.DateTimeFormat.Calendar;
			DateTime value;
			if (DateTime.TryParse(stringValue, dateTimeFormatInfo, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out value))
			{
				return new DateTime?(value);
			}
			return null;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x000088F8 File Offset: 0x00006AF8
		private string[] GetMultiValuedStrings(string stringValue)
		{
			string[] array = stringValue.Split(new char[]
			{
				ImportContactObject.MultiValueSeparator
			});
			if (array.Length == 0)
			{
				return null;
			}
			return array;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00008924 File Offset: 0x00006B24
		private bool? GetBoolValue(string stringValue)
		{
			bool value;
			if (bool.TryParse(stringValue, out value))
			{
				return new bool?(value);
			}
			return null;
		}

		// Token: 0x0400012C RID: 300
		private const char NewLine = '\n';

		// Token: 0x0400012D RID: 301
		public static readonly string FirstAppendToNotes = string.Concat(new object[]
		{
			"-----------------------------",
			'\n',
			Strings.FirstAppendToNotes,
			":",
			'\n'
		});

		// Token: 0x0400012E RID: 302
		public static readonly char MultiValueSeparator = ';';

		// Token: 0x0400012F RID: 303
		private static readonly int propertyBagInitialSize = 10;

		// Token: 0x04000130 RID: 304
		private Dictionary<ImportContactProperties, object> propertyBag;

		// Token: 0x04000131 RID: 305
		private bool appendedDataToNotes;

		// Token: 0x04000132 RID: 306
		private int index;
	}
}
