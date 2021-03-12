using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Forefront.Reporting.Common
{
	// Token: 0x02000016 RID: 22
	internal class QueryGroupField : QueryField
	{
		// Token: 0x06000050 RID: 80 RVA: 0x000027D4 File Offset: 0x000009D4
		internal QueryGroupField(QueryGroupField parent, int startPos, int endPos) : base(parent, startPos, endPos)
		{
			this.FromDate = parent.FromDate;
			this.ToDate = parent.ToDate;
			this.MsgStatus = parent.MsgStatus;
			string grouperName = this.GetGrouperName(startPos, endPos);
			try
			{
				this.GrouperName = (QueryGroupName)Enum.Parse(typeof(QueryGroupName), grouperName);
			}
			catch (FormatException innerException)
			{
				throw new InvalidQueryException(InvalidQueryException.InvalidQueryErrorCode.InvalidGrouper, grouperName, startPos, innerException);
			}
			if (this.GrouperName == QueryGroupName.AND && parent.IsFirstAndGroup == null)
			{
				this.IsFirstAndGroup = new bool?(true);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002878 File Offset: 0x00000A78
		internal QueryGroupField(string queryString, QueryCompiler compiler) : base(null, 0, queryString.Length)
		{
			base.QueryString = queryString;
			string grouperName = this.GetGrouperName(0, queryString.Length);
			try
			{
				this.GrouperName = (QueryGroupName)Enum.Parse(typeof(QueryGroupName), grouperName);
			}
			catch (FormatException innerException)
			{
				throw new InvalidQueryException(InvalidQueryException.InvalidQueryErrorCode.InvalidGrouper, grouperName, 0, innerException);
			}
			base.Compiler = compiler;
			if (this.GrouperName == QueryGroupName.AND)
			{
				this.IsFirstAndGroup = new bool?(true);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002900 File Offset: 0x00000B00
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002908 File Offset: 0x00000B08
		internal QueryGroupName GrouperName { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002911 File Offset: 0x00000B11
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002919 File Offset: 0x00000B19
		internal DateTime? FromDate { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002922 File Offset: 0x00000B22
		// (set) Token: 0x06000057 RID: 87 RVA: 0x0000292A File Offset: 0x00000B2A
		internal DateTime? ToDate { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002933 File Offset: 0x00000B33
		// (set) Token: 0x06000059 RID: 89 RVA: 0x0000293B File Offset: 0x00000B3B
		internal StatusFlags? MsgStatus { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002944 File Offset: 0x00000B44
		// (set) Token: 0x0600005B RID: 91 RVA: 0x0000294C File Offset: 0x00000B4C
		internal bool NeedToFillInMsgStatus { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002955 File Offset: 0x00000B55
		// (set) Token: 0x0600005D RID: 93 RVA: 0x0000295D File Offset: 0x00000B5D
		internal bool? IsFirstAndGroup { get; set; }

		// Token: 0x0600005E RID: 94 RVA: 0x000029B0 File Offset: 0x00000BB0
		internal override string Compile()
		{
			List<QueryPropertyField> list = new List<QueryPropertyField>();
			List<Tuple<int, int>> list2 = new List<Tuple<int, int>>();
			foreach (Tuple<int, int> tuple2 in this.GetInnerFields(base.StartPosition, base.EndPosition))
			{
				if (this.GetGrouperName(tuple2.Item1, tuple2.Item2) == null)
				{
					list.Add(new QueryPropertyField(this, tuple2.Item1, tuple2.Item2));
				}
				else
				{
					list2.Add(tuple2);
				}
			}
			if (list.Count > 100)
			{
				throw new InvalidQueryException(InvalidQueryException.InvalidQueryErrorCode.ToManyPropertiesInGroup, this.GrouperName.ToString(), base.StartPosition);
			}
			List<string> first = (from field in list
			select field.Compile() into str
			where !string.IsNullOrWhiteSpace(str)
			select str).ToList<string>();
			List<QueryGroupField> list3 = (from tuple in list2
			select new QueryGroupField(this, tuple.Item1, tuple.Item2)).ToList<QueryGroupField>();
			if (list3.Count > 100)
			{
				throw new InvalidQueryException(InvalidQueryException.InvalidQueryErrorCode.ToManyGroups, this.GrouperName.ToString(), base.StartPosition);
			}
			List<string> second = (from field in list3
			select field.Compile() into str
			where !string.IsNullOrWhiteSpace(str)
			select str).ToList<string>();
			if (this.GrouperName == QueryGroupName.AND)
			{
				base.HasOptionalCriterion = list3.Concat(list).Any((QueryField field) => field.HasOptionalCriterion);
			}
			else
			{
				base.HasOptionalCriterion = list3.Concat(list).All((QueryField field) => field.HasOptionalCriterion);
			}
			if (this.IsFirstAndGroup != null && this.IsFirstAndGroup.Value)
			{
				if (this.FromDate == null)
				{
					throw new InvalidQueryException(InvalidQueryException.InvalidQueryErrorCode.MissingRequiredProperty, QueryProperty.FromDate.ToString(), base.StartPosition);
				}
				if (this.ToDate == null)
				{
					throw new InvalidQueryException(InvalidQueryException.InvalidQueryErrorCode.MissingRequiredProperty, QueryProperty.ToDate.ToString(), base.StartPosition);
				}
			}
			string text = string.Join(QueryGroupField.supportedGroupers[this.GrouperName], first.Concat(second));
			if (this.NeedToFillInMsgStatus)
			{
				if (this.MsgStatus != null)
				{
					if (text.Contains("%MsgStatus%"))
					{
						text = text.Replace("%MsgStatus%", this.GenMsgStatusCode(this.MsgStatus.Value));
					}
					else
					{
						text = text + QueryGroupField.supportedGroupers[this.GrouperName] + string.Format("OnDemandQueryUtil.MsgStatusMatch(msgStatus {0})", this.GenMsgStatusCode(this.MsgStatus.Value));
					}
				}
				else
				{
					text = text.Replace("%MsgStatus%", string.Empty);
				}
			}
			if (string.IsNullOrWhiteSpace(text))
			{
				return string.Empty;
			}
			return string.Format("({0})", text);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002D04 File Offset: 0x00000F04
		private string GenMsgStatusCode(StatusFlags msgStatus)
		{
			if (msgStatus <= StatusFlags.Send)
			{
				if (msgStatus == StatusFlags.Expand)
				{
					return ", StatusFlags.Expand";
				}
				if (msgStatus == StatusFlags.Send)
				{
					return ", StatusFlags.Send";
				}
			}
			else
			{
				if (msgStatus == StatusFlags.Deliver)
				{
					return ", StatusFlags.Send | StatusFlags.Deliver";
				}
				if (msgStatus == StatusFlags.Fail)
				{
					return ", StatusFlags.Fail";
				}
			}
			throw new InvalidQueryException(InvalidQueryException.InvalidQueryErrorCode.InvalidValue, "MsgStatus", base.StartPosition);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002D58 File Offset: 0x00000F58
		private string GetGrouperName(int fieldStart, int fieldEnd)
		{
			int num = base.QueryString.IndexOf('(', fieldStart, fieldEnd - fieldStart);
			if (num > fieldStart)
			{
				return base.QueryString.Substring(fieldStart, num - fieldStart).Trim();
			}
			return null;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003044 File Offset: 0x00001244
		private IEnumerable<Tuple<int, int>> GetInnerFields(int fieldStart, int fieldEnd)
		{
			int openParentheses = 0;
			bool inQuote = false;
			int fromPos = -1;
			for (int pos = fieldStart; pos < fieldEnd; pos++)
			{
				char c = base.QueryString[pos];
				if (c != '"')
				{
					switch (c)
					{
					case '(':
						if (!inQuote)
						{
							openParentheses++;
							if (openParentheses == 1)
							{
								fromPos = pos + 1;
							}
						}
						break;
					case ')':
						if (!inQuote)
						{
							openParentheses--;
							if (openParentheses < 0)
							{
								throw new InvalidQueryException(InvalidQueryException.InvalidQueryErrorCode.UnpairedParenthese, string.Empty, pos);
							}
							if (openParentheses == 0)
							{
								if (pos != fieldEnd - 1)
								{
									throw new InvalidQueryException(InvalidQueryException.InvalidQueryErrorCode.UnpairedParenthese, string.Empty, pos);
								}
								if (fromPos < pos)
								{
									yield return Tuple.Create<int, int>(fromPos, pos);
									goto IL_20B;
								}
								throw new InvalidQueryException(InvalidQueryException.InvalidQueryErrorCode.InvalidProperty, string.Empty, pos);
							}
						}
						break;
					case ',':
						if (!inQuote && openParentheses == 1)
						{
							if (fromPos >= pos)
							{
								throw new InvalidQueryException(InvalidQueryException.InvalidQueryErrorCode.InvalidProperty, string.Empty, pos);
							}
							yield return Tuple.Create<int, int>(fromPos, pos);
							fromPos = pos + 1;
						}
						break;
					}
				}
				else
				{
					inQuote = !inQuote;
				}
			}
			IL_20B:
			yield break;
		}

		// Token: 0x0400006C RID: 108
		private const int NumOfPropertiesInGroupLimit = 100;

		// Token: 0x0400006D RID: 109
		private static Dictionary<QueryGroupName, string> supportedGroupers = new Dictionary<QueryGroupName, string>
		{
			{
				QueryGroupName.AND,
				" && "
			},
			{
				QueryGroupName.OR,
				" || "
			}
		};
	}
}
