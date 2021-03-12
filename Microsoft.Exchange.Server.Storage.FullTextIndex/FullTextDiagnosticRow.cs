using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.Ceres.InteractionEngine.Services.Exchange;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.FullTextIndex
{
	// Token: 0x0200000A RID: 10
	public class FullTextDiagnosticRow
	{
		// Token: 0x0600004C RID: 76 RVA: 0x0000470A File Offset: 0x0000290A
		internal FullTextDiagnosticRow()
		{
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00004712 File Offset: 0x00002912
		public static ICollection<string> FastColumns
		{
			get
			{
				return FullTextDiagnosticRow.ColumnSetters.Keys;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000471E File Offset: 0x0000291E
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00004726 File Offset: 0x00002926
		[Queryable(Visibility = Visibility.Public)]
		public int MailboxNumber { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000050 RID: 80 RVA: 0x0000472F File Offset: 0x0000292F
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00004737 File Offset: 0x00002937
		[Queryable(Visibility = Visibility.Public)]
		public int DocumentId { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00004740 File Offset: 0x00002940
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00004748 File Offset: 0x00002948
		[Queryable(Visibility = Visibility.Public)]
		[FullTextDiagnosticRow.FastPropertyAttribute("mailboxguid")]
		public string MailboxGuid { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00004751 File Offset: 0x00002951
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00004759 File Offset: 0x00002959
		[FullTextDiagnosticRow.FastPropertyAttribute("compositeitemid")]
		[Queryable(Visibility = Visibility.Public)]
		public string CompositItemId { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00004762 File Offset: 0x00002962
		// (set) Token: 0x06000057 RID: 87 RVA: 0x0000476A File Offset: 0x0000296A
		[Queryable(Visibility = Visibility.Public)]
		[FullTextDiagnosticRow.FastPropertyAttribute("documentid")]
		public long? FastIndexId { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00004773 File Offset: 0x00002973
		// (set) Token: 0x06000059 RID: 89 RVA: 0x0000477B File Offset: 0x0000297B
		[Queryable(Visibility = Visibility.Redacted)]
		[FullTextDiagnosticRow.FastPropertyAttribute("subject")]
		public string Subject { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00004784 File Offset: 0x00002984
		// (set) Token: 0x0600005B RID: 91 RVA: 0x0000478C File Offset: 0x0000298C
		[FullTextDiagnosticRow.FastPropertyAttribute("to")]
		[Queryable(Visibility = Visibility.Redacted)]
		public string To { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00004795 File Offset: 0x00002995
		// (set) Token: 0x0600005D RID: 93 RVA: 0x0000479D File Offset: 0x0000299D
		[FullTextDiagnosticRow.FastPropertyAttribute("recipients")]
		[Queryable(Visibility = Visibility.Redacted)]
		public string Recipients { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000047A6 File Offset: 0x000029A6
		// (set) Token: 0x0600005F RID: 95 RVA: 0x000047AE File Offset: 0x000029AE
		[Queryable(Visibility = Visibility.Public)]
		[FullTextDiagnosticRow.FastPropertyAttribute("folderid")]
		public string FolderId { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000047B7 File Offset: 0x000029B7
		// (set) Token: 0x06000061 RID: 97 RVA: 0x000047BF File Offset: 0x000029BF
		[FullTextDiagnosticRow.FastPropertyAttribute("from")]
		[Queryable(Visibility = Visibility.Redacted)]
		public string From { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000047C8 File Offset: 0x000029C8
		// (set) Token: 0x06000063 RID: 99 RVA: 0x000047D0 File Offset: 0x000029D0
		[FullTextDiagnosticRow.FastPropertyAttribute("importance")]
		[Queryable(Visibility = Visibility.Public)]
		public long? Importance { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000047D9 File Offset: 0x000029D9
		// (set) Token: 0x06000065 RID: 101 RVA: 0x000047E1 File Offset: 0x000029E1
		[Queryable(Visibility = Visibility.Public)]
		[FullTextDiagnosticRow.FastPropertyAttribute("itemclass")]
		public string ItemClass { get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000047EA File Offset: 0x000029EA
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000047F2 File Offset: 0x000029F2
		[FullTextDiagnosticRow.FastPropertyAttribute("watermark")]
		[Queryable(Visibility = Visibility.Public)]
		public long? Watermark { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000047FB File Offset: 0x000029FB
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00004803 File Offset: 0x00002A03
		[FullTextDiagnosticRow.FastPropertyAttribute("errorcode")]
		[Queryable(Visibility = Visibility.Public)]
		public long? ErrorCode { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000480C File Offset: 0x00002A0C
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00004814 File Offset: 0x00002A14
		[Queryable(Visibility = Visibility.Public)]
		[FullTextDiagnosticRow.FastPropertyAttribute("errormessage")]
		public string ErrorMessage { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006C RID: 108 RVA: 0x0000481D File Offset: 0x00002A1D
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00004825 File Offset: 0x00002A25
		[FullTextDiagnosticRow.FastPropertyAttribute("attemptcount")]
		[Queryable(Visibility = Visibility.Public)]
		public long? AttemptCount { get; private set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600006E RID: 110 RVA: 0x0000482E File Offset: 0x00002A2E
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00004836 File Offset: 0x00002A36
		[Queryable(Visibility = Visibility.Public)]
		[FullTextDiagnosticRow.FastPropertyAttribute("lastattempttime")]
		public DateTime? LastAttemptTime { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000483F File Offset: 0x00002A3F
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00004847 File Offset: 0x00002A47
		[FullTextDiagnosticRow.FastPropertyAttribute("ispartiallyprocessed")]
		[Queryable(Visibility = Visibility.Public)]
		public bool? IsPartiallyProcessed { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00004850 File Offset: 0x00002A50
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00004858 File Offset: 0x00002A58
		[FullTextDiagnosticRow.FastPropertyAttribute("conversationid")]
		[Queryable(Visibility = Visibility.Public)]
		public long? ConversationId { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00004861 File Offset: 0x00002A61
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00004869 File Offset: 0x00002A69
		[Queryable(Visibility = Visibility.Public)]
		[FullTextDiagnosticRow.FastPropertyAttribute("isread")]
		public bool? IsRead { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00004872 File Offset: 0x00002A72
		// (set) Token: 0x06000077 RID: 119 RVA: 0x0000487A File Offset: 0x00002A7A
		[Queryable(Visibility = Visibility.Public)]
		[FullTextDiagnosticRow.FastPropertyAttribute("hasirm")]
		public bool? HasIrm { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00004883 File Offset: 0x00002A83
		// (set) Token: 0x06000079 RID: 121 RVA: 0x0000488B File Offset: 0x00002A8B
		[FullTextDiagnosticRow.FastPropertyAttribute("iconindex")]
		[Queryable(Visibility = Visibility.Public)]
		public long? IconIndex { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00004894 File Offset: 0x00002A94
		// (set) Token: 0x0600007B RID: 123 RVA: 0x0000489C File Offset: 0x00002A9C
		[Queryable(Visibility = Visibility.Public)]
		[FullTextDiagnosticRow.FastPropertyAttribute("hasattachment")]
		public string HasAttachment { get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000048A5 File Offset: 0x00002AA5
		// (set) Token: 0x0600007D RID: 125 RVA: 0x000048AD File Offset: 0x00002AAD
		[Queryable(Visibility = Visibility.Public)]
		[FullTextDiagnosticRow.FastPropertyAttribute("mid")]
		public long? Mid { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000048B6 File Offset: 0x00002AB6
		// (set) Token: 0x0600007F RID: 127 RVA: 0x000048BE File Offset: 0x00002ABE
		[Queryable(Visibility = Visibility.Private)]
		[FullTextDiagnosticRow.FastPropertyAttribute("bodypreview")]
		public string BodyPreview { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000048C7 File Offset: 0x00002AC7
		// (set) Token: 0x06000081 RID: 129 RVA: 0x000048CF File Offset: 0x00002ACF
		[Queryable(Visibility = Visibility.Public)]
		[FullTextDiagnosticRow.FastPropertyAttribute("refinablereceived")]
		public DateTime? RefinableReceived { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000048D8 File Offset: 0x00002AD8
		// (set) Token: 0x06000083 RID: 131 RVA: 0x000048E0 File Offset: 0x00002AE0
		[FullTextDiagnosticRow.FastPropertyAttribute("refinablefrom")]
		[Queryable(Visibility = Visibility.Redacted)]
		public string RefinableFrom { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000048E9 File Offset: 0x00002AE9
		// (set) Token: 0x06000085 RID: 133 RVA: 0x000048F1 File Offset: 0x00002AF1
		[FullTextDiagnosticRow.FastPropertyAttribute("workingsetsource")]
		[Queryable(Visibility = Visibility.Public)]
		public long? WorkingSetSource { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000048FA File Offset: 0x00002AFA
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00004902 File Offset: 0x00002B02
		[FullTextDiagnosticRow.FastPropertyAttribute("workingsetsourcepartition")]
		[Queryable(Visibility = Visibility.Public)]
		public string WorkingSetSourcePartition { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000088 RID: 136 RVA: 0x0000490B File Offset: 0x00002B0B
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00004913 File Offset: 0x00002B13
		[Queryable(Visibility = Visibility.Public)]
		[FullTextDiagnosticRow.FastPropertyAttribute("workingsetid")]
		public string WorkingSetId { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000491C File Offset: 0x00002B1C
		internal static Dictionary<string, PropertyInfo> ColumnSetters
		{
			get
			{
				if (FullTextDiagnosticRow.columnSetters == null)
				{
					PropertyInfo[] properties = typeof(FullTextDiagnosticRow).GetProperties(BindingFlags.Instance | BindingFlags.Public);
					Dictionary<string, PropertyInfo> dictionary = new Dictionary<string, PropertyInfo>(properties.Length);
					foreach (PropertyInfo propertyInfo in properties)
					{
						foreach (Attribute attribute in propertyInfo.GetCustomAttributes(false))
						{
							FullTextDiagnosticRow.FastPropertyAttribute fastPropertyAttribute = attribute as FullTextDiagnosticRow.FastPropertyAttribute;
							if (fastPropertyAttribute != null)
							{
								dictionary.Add(fastPropertyAttribute.FastPropertyName, propertyInfo);
							}
						}
					}
					FullTextDiagnosticRow.columnSetters = dictionary;
				}
				return FullTextDiagnosticRow.columnSetters;
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004BC0 File Offset: 0x00002DC0
		public static IEnumerable<FullTextDiagnosticRow> Parse(IEnumerable<SearchResultItem[]> pagedResults)
		{
			foreach (SearchResultItem[] page in pagedResults)
			{
				foreach (SearchResultItem item in page)
				{
					yield return FullTextDiagnosticRow.Parse(item);
				}
			}
			yield break;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004BE0 File Offset: 0x00002DE0
		internal void SetRowValue(string propertyName, object propertyValue)
		{
			IEnumerable enumerable = propertyValue as IEnumerable;
			object obj = (enumerable != null && propertyValue.GetType().IsGenericType) ? FullTextDiagnosticRow.GetEnumerableValue(enumerable) : propertyValue;
			PropertyInfo propertyInfo;
			if (FullTextDiagnosticRow.ColumnSetters.TryGetValue(propertyName, out propertyInfo))
			{
				if (obj != null)
				{
					Type type = obj.GetType();
					if (type != propertyInfo.PropertyType)
					{
						if (!(Nullable.GetUnderlyingType(propertyInfo.PropertyType) == type))
						{
							throw new FullTextIndexException((LID)2524327229U, ErrorCodeValue.FullTextIndexCallFailed, string.Format("Fast property '{0}' type '{1}' is inconsistent with value type '{2}'!", propertyName, obj.GetType(), propertyInfo.PropertyType));
						}
						obj = Convert.ChangeType(obj, Nullable.GetUnderlyingType(propertyInfo.PropertyType));
					}
				}
				propertyInfo.SetValue(this, obj, null);
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004C94 File Offset: 0x00002E94
		private static FullTextDiagnosticRow Parse(SearchResultItem item)
		{
			long num = (long)item.Fields[1].Value;
			SearchResultItem searchResultItem = item.Fields[2].Value as SearchResultItem;
			if (searchResultItem == null)
			{
				throw new ArgumentNullException("otherFields");
			}
			FullTextDiagnosticRow fullTextDiagnosticRow = new FullTextDiagnosticRow();
			fullTextDiagnosticRow.MailboxNumber = IndexId.GetMailboxNumber(num);
			fullTextDiagnosticRow.DocumentId = IndexId.GetDocumentId(num);
			foreach (IFieldHolder fieldHolder in searchResultItem.Fields)
			{
				fullTextDiagnosticRow.SetRowValue(fieldHolder.Name, fieldHolder.Value);
			}
			return fullTextDiagnosticRow;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004D4C File Offset: 0x00002F4C
		private static string GetEnumerableValue(IEnumerable enumerableValue)
		{
			StringBuilder stringBuilder = new StringBuilder(20);
			foreach (object arg in enumerableValue)
			{
				stringBuilder.AppendFormat("{0};", arg);
			}
			int length = (stringBuilder.Length > 0) ? (stringBuilder.Length - 1) : 0;
			return stringBuilder.ToString(0, length);
		}

		// Token: 0x0400001D RID: 29
		private const int FastSearchResultsDocumentIdFieldIndex = 1;

		// Token: 0x0400001E RID: 30
		private const int FastSearchResultsOtherFieldIndex = 2;

		// Token: 0x0400001F RID: 31
		private static Dictionary<string, PropertyInfo> columnSetters;

		// Token: 0x0200000B RID: 11
		[AttributeUsage(AttributeTargets.Property, Inherited = false)]
		private class FastPropertyAttribute : Attribute
		{
			// Token: 0x0600008F RID: 143 RVA: 0x00004DCC File Offset: 0x00002FCC
			public FastPropertyAttribute(string fastPropertyName)
			{
				this.FastPropertyName = fastPropertyName;
			}

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x06000090 RID: 144 RVA: 0x00004DDB File Offset: 0x00002FDB
			// (set) Token: 0x06000091 RID: 145 RVA: 0x00004DE3 File Offset: 0x00002FE3
			public string FastPropertyName { get; private set; }
		}
	}
}
