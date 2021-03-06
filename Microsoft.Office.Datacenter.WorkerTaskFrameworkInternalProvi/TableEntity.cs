using System;
using System.Data;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x02000009 RID: 9
	public abstract class TableEntity
	{
		// Token: 0x06000085 RID: 133 RVA: 0x000046A0 File Offset: 0x000028A0
		public virtual void PopulateFromRow(DataRow row)
		{
			Type type = base.GetType();
			foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				ColumnAttribute columnAttribute = (ColumnAttribute)propertyInfo.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault<object>();
				if (columnAttribute != null)
				{
					string columnName = columnAttribute.Name ?? propertyInfo.Name;
					object obj = row[columnName];
					object value = null;
					try
					{
						if (obj != DBNull.Value)
						{
							value = Convert.ChangeType(obj, propertyInfo.PropertyType);
						}
					}
					catch (Exception ex)
					{
						throw ex;
					}
					propertyInfo.SetValue(this, value, null);
				}
			}
		}
	}
}
