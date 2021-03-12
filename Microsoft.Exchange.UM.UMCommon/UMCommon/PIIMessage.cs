using System;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000129 RID: 297
	internal class PIIMessage
	{
		// Token: 0x06000993 RID: 2451 RVA: 0x0002599D File Offset: 0x00023B9D
		private PIIMessage(PIIType key, string value)
		{
			this.Key = key;
			this.Value = value;
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x000259B3 File Offset: 0x00023BB3
		// (set) Token: 0x06000995 RID: 2453 RVA: 0x000259BB File Offset: 0x00023BBB
		public PIIType Key
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x000259C4 File Offset: 0x00023BC4
		// (set) Token: 0x06000997 RID: 2455 RVA: 0x000259CC File Offset: 0x00023BCC
		public string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x000259D5 File Offset: 0x00023BD5
		public string LogSafeValue
		{
			get
			{
				if (this.Value == null)
				{
					return "<null>";
				}
				return this.Value.Replace("​", "").Replace(',', '‚');
			}
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00025A06 File Offset: 0x00023C06
		public override string ToString()
		{
			return PIIMessage.PIITypeEnumToString[(int)this.key] + "=" + this.LogSafeValue;
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00025A24 File Offset: 0x00023C24
		public static PIIMessage Create(PIIType key, string value)
		{
			return new PIIMessage(key, value);
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00025A2D File Offset: 0x00023C2D
		public static PIIMessage Create(PIIType key, object value)
		{
			if (value == null)
			{
				return new PIIMessage(key, "<null>");
			}
			return new PIIMessage(key, value.ToString());
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00025A4C File Offset: 0x00023C4C
		private static string[] InitPIITypeEnumToStringMapping()
		{
			PIIType[] array = (PIIType[])Enum.GetValues(typeof(PIIType));
			string[] array2 = new string[array.Length];
			foreach (PIIType piitype in array)
			{
				array2[(int)piitype] = piitype.ToString();
			}
			return array2;
		}

		// Token: 0x04000556 RID: 1366
		private static string[] PIITypeEnumToString = PIIMessage.InitPIITypeEnumToStringMapping();

		// Token: 0x04000557 RID: 1367
		private PIIType key;

		// Token: 0x04000558 RID: 1368
		private string value;
	}
}
