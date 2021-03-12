﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Forefront.Reporting.Common;

namespace Microsoft.Forefront.Reporting.OnDemandQuery
{
	// Token: 0x0200000A RID: 10
	public class ReportRowFormatter
	{
		// Token: 0x06000043 RID: 67 RVA: 0x0000340C File Offset: 0x0000160C
		internal ReportRowFormatter(string schemaDefinition, PIIDecryptionDelegate decrypterIn, Guid tenantIdIn) : this(schemaDefinition, decrypterIn, tenantIdIn, null)
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003420 File Offset: 0x00001620
		internal ReportRowFormatter(string schemaDefinition, PIIDecryptionDelegate decrypterIn, Guid tenantIdIn, CultureInfo resultLocalIn)
		{
			this.decrypter = decrypterIn;
			this.resultLocale = resultLocalIn;
			List<Tuple<string, ResultColumnDataType, int, int>> list = new List<Tuple<string, ResultColumnDataType, int, int>>();
			int num = 0;
			foreach (string text in schemaDefinition.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries))
			{
				string[] array2 = text.Split(new char[]
				{
					':'
				}, StringSplitOptions.RemoveEmptyEntries);
				string item = array2[0];
				ResultColumnDataType item2;
				if (!Enum.TryParse<ResultColumnDataType>(array2[1], true, out item2))
				{
					item2 = ResultColumnDataType.Text;
				}
				int item3 = int.Parse(array2[2]);
				list.Add(Tuple.Create<string, ResultColumnDataType, int, int>(item, item2, num, item3));
				num++;
			}
			this.resultColumnCount = 0;
			this.inputSchema = new Tuple<string, ResultColumnDataType, int>[list.Count];
			this.outputHeader = new List<string>();
			foreach (Tuple<string, ResultColumnDataType, int, int> tuple in from t in list
			orderby t.Item4
			select t)
			{
				string item4 = tuple.Item1;
				ResultColumnDataType item5 = tuple.Item2;
				int item6 = tuple.Item3;
				if (tuple.Item2 == ResultColumnDataType.Hide)
				{
					this.inputSchema[item6] = Tuple.Create<string, ResultColumnDataType, int>(item4, item5, -1);
				}
				else
				{
					this.inputSchema[item6] = Tuple.Create<string, ResultColumnDataType, int>(item4, item5, this.resultColumnCount);
					this.resultColumnCount++;
					this.outputHeader.Add(item4);
				}
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000035DC File Offset: 0x000017DC
		public string GetHeader()
		{
			if (this.resultLocale != null)
			{
				return string.Join(",", from value in this.outputHeader
				select string.Format("\"{0}\"", value));
			}
			return string.Join(",", from value in this.outputHeader
			select string.Format("\"{0}\"", value));
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003674 File Offset: 0x00001874
		public string Format(string rawLine)
		{
			if (string.IsNullOrWhiteSpace(rawLine))
			{
				return string.Empty;
			}
			string[] array = rawLine.Split(new char[]
			{
				'\t'
			});
			if (array.Length != this.inputSchema.Length)
			{
				return string.Empty;
			}
			string[] array2 = new string[this.resultColumnCount];
			int i = 0;
			while (i < this.inputSchema.Length)
			{
				int item = this.inputSchema[i].Item3;
				switch (this.inputSchema[i].Item2)
				{
				case ResultColumnDataType.Hide:
					break;
				case ResultColumnDataType.Text:
					goto IL_B1;
				case ResultColumnDataType.PIIEncrypted:
					array2[item] = this.decrypter(array[i]);
					break;
				case ResultColumnDataType.TranportRule:
					array2[item] = array[i];
					break;
				case ResultColumnDataType.DlpId:
					array2[item] = array[i];
					break;
				case ResultColumnDataType.DcId:
					array2[item] = array[i];
					break;
				default:
					goto IL_B1;
				}
				IL_B7:
				i++;
				continue;
				IL_B1:
				array2[item] = array[i];
				goto IL_B7;
			}
			return string.Join(",", from value in array2
			select string.Format("\"{0}\"", value.Replace("\"", "\"\"")));
		}

		// Token: 0x0400004A RID: 74
		private readonly int resultColumnCount;

		// Token: 0x0400004B RID: 75
		private PIIDecryptionDelegate decrypter;

		// Token: 0x0400004C RID: 76
		private CultureInfo resultLocale;

		// Token: 0x0400004D RID: 77
		private Tuple<string, ResultColumnDataType, int>[] inputSchema;

		// Token: 0x0400004E RID: 78
		private List<string> outputHeader = new List<string>();
	}
}
