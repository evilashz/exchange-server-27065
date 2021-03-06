using System;
using System.Data;
using System.Data.Common;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.Management.ReportingTask.Common
{
	// Token: 0x020006A8 RID: 1704
	internal class ReportDataContext : DataContext
	{
		// Token: 0x06003C61 RID: 15457 RVA: 0x001011A3 File Offset: 0x000FF3A3
		public ReportDataContext(IDbConnection connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		// Token: 0x06003C62 RID: 15458 RVA: 0x001011B0 File Offset: 0x000FF3B0
		public IQueryable<TReportObject> GetScaledQuery<TReportObject>(IQueryable<TReportObject> query) where TReportObject : ReportObject
		{
			int num = this.FetchRegexValue(query.Expression.ToString(), "\\.Take\\((?<value>[0-9]+)\\)", DataMart.Instance.DefaultReportResultSize);
			int num2 = this.FetchRegexValue(query.Expression.ToString(), "\\.Skip\\((?<value>[0-9]+)\\)", 0);
			string text = string.Empty;
			string sqlCommandLine = this.GetSqlCommandLine(query);
			if (num2 == 0)
			{
				num = this.FetchRegexValue(this.GetSqlCommandLine(query), "SELECT TOP \\((?<value>[0-9]+)\\)", num);
				string[] array = sqlCommandLine.Split(new string[]
				{
					"WHERE",
					"ORDER BY"
				}, StringSplitOptions.None);
				if (array.Length > 1)
				{
					text = array[1];
				}
			}
			else
			{
				string text2 = sqlCommandLine.Substring(sqlCommandLine.IndexOf("( SELECT") + 1, sqlCommandLine.LastIndexOf(") AS") - sqlCommandLine.IndexOf("( SELECT") - 1);
				text2 = text2.Replace("ROW_NUMBER() OVER (ORDER BY [t0].[DateTime]) AS [ROW_NUMBER], ", string.Empty);
				string[] array2 = text2.Split(new string[]
				{
					"WHERE"
				}, StringSplitOptions.None);
				if (array2.Length > 1)
				{
					text = array2[1];
				}
			}
			string text3 = string.Format("Get{0}", typeof(TReportObject).Name);
			MethodInfo method = typeof(ReportDataContext).GetMethod(text3);
			IQueryable<TReportObject> result;
			try
			{
				IQueryable<TReportObject> queryable = base.CreateMethodCallQuery<TReportObject>(this, method, new object[]
				{
					num + num2,
					text
				});
				queryable = queryable.Take(num + num2);
				if (num2 > 0)
				{
					queryable = queryable.Skip(num2);
				}
				result = queryable;
			}
			catch (ArgumentException innerException)
			{
				throw new ArgumentException("Cannot find Method when calling Table Function.", text3, innerException);
			}
			return result;
		}

		// Token: 0x06003C63 RID: 15459 RVA: 0x00101350 File Offset: 0x000FF550
		private string GetSqlCommandLine(IQueryable query)
		{
			DbCommand command = base.GetCommand(query);
			string text = command.CommandText;
			new StringBuilder(command.CommandText);
			foreach (object obj in command.Parameters)
			{
				DbParameter dbParameter = (DbParameter)obj;
				text = text.Replace(dbParameter.ParameterName, string.Format("'{0}'", dbParameter.Value));
			}
			return text;
		}

		// Token: 0x06003C64 RID: 15460 RVA: 0x001013E0 File Offset: 0x000FF5E0
		private int FetchRegexValue(string source, string pattern, int defaultValue)
		{
			MatchCollection matchCollection = Regex.Matches(source.ToLowerInvariant(), pattern.ToLowerInvariant());
			int result;
			if (matchCollection.Count > 0 && int.TryParse(matchCollection[0].Groups["value"].Value, out result))
			{
				return result;
			}
			return defaultValue;
		}

		// Token: 0x06003C65 RID: 15461 RVA: 0x0010142F File Offset: 0x000FF62F
		[Function(Name = "dbo.udf_MailboxUsageDetail", IsComposable = true)]
		public IQueryable<MailboxUsageDetailReport> GetMailboxUsageDetailReport([Parameter(DbType = "Int")] int topCount, [Parameter(DbType = "NVarChar(2000)")] string whereClause)
		{
			return null;
		}

		// Token: 0x06003C66 RID: 15462 RVA: 0x00101432 File Offset: 0x000FF632
		[Function(Name = "dbo.udf_SPOODBFileActivity", IsComposable = true)]
		public IQueryable<SPOODBFileActivityReport> GetSPOODBFileActivityReport([Parameter(DbType = "Int")] int topCount, [Parameter(DbType = "NVarChar(2000)")] string whereClause)
		{
			return null;
		}

		// Token: 0x06003C67 RID: 15463 RVA: 0x00101435 File Offset: 0x000FF635
		[Function(Name = "dbo.udf_SPOODBUserStatistics", IsComposable = true)]
		public IQueryable<SPOODBUserStatisticsReport> GetSPOODBUserStatisticsReport([Parameter(DbType = "Int")] int topCount, [Parameter(DbType = "NVarChar(2000)")] string whereClause)
		{
			return null;
		}

		// Token: 0x06003C68 RID: 15464 RVA: 0x00101438 File Offset: 0x000FF638
		[Function(Name = "dbo.udf_StaleMailboxDetail", IsComposable = true)]
		public IQueryable<StaleMailboxDetailReport> GetStaleMailboxDetailReport([Parameter(DbType = "Int")] int topCount, [Parameter(DbType = "NVarChar(2000)")] string whereClause)
		{
			return null;
		}
	}
}
