using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000102 RID: 258
	internal class DependentColumnExistRule : LambdaExpressionRule
	{
		// Token: 0x06000960 RID: 2400 RVA: 0x00020E40 File Offset: 0x0001F040
		protected override List<string> OnValidate(string lambdaExpression, PageConfigurableProfile profile)
		{
			List<string> list = new List<string>();
			string[] array = lambdaExpression.Split(new string[]
			{
				"=>"
			}, StringSplitOptions.None);
			string[] array2 = array[0].Split(new char[]
			{
				',',
				' '
			}, StringSplitOptions.RemoveEmptyEntries);
			string[] array3 = array2;
			for (int i = 0; i < array3.Length; i++)
			{
				string columnName = array3[i];
				if (profile.ColumnProfiles.All((ColumnProfile columnProfile) => !columnName.Equals(columnProfile.Name)))
				{
					list.Add(string.Format("{0} in input columns is not a valid column name in lambda expression", columnName));
				}
			}
			string text = array[1];
			foreach (string arg in array2)
			{
				string value = string.Format("@0[{0}]", arg);
				if (!text.Contains(value))
				{
					list.Add(string.Format("Column {0} is not used in lambda expression {1}", arg, lambdaExpression));
				}
			}
			return list;
		}
	}
}
