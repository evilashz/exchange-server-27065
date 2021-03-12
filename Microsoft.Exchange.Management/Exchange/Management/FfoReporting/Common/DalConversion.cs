using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.FfoReporting.Common
{
	// Token: 0x020003CC RID: 972
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	internal sealed class DalConversion : Attribute
	{
		// Token: 0x060022DE RID: 8926 RVA: 0x0008DC80 File Offset: 0x0008BE80
		static DalConversion()
		{
			DalConversion.conversionFunctions.Add("DefaultSerializer", delegate(DalConversion instance, object reportObject, PropertyInfo property, object dalObject, object task)
			{
				instance.DefaultSerializer(reportObject, property, dalObject, task);
			});
			DalConversion.conversionFunctions.Add("DateFromStringSerializer", delegate(DalConversion instance, object reportObject, PropertyInfo property, object dalObject, object task)
			{
				instance.DateFromStringSerializer(reportObject, property, dalObject, task);
			});
			DalConversion.conversionFunctions.Add("DateFromIntSerializer", delegate(DalConversion instance, object reportObject, PropertyInfo property, object dalObject, object task)
			{
				instance.DateFromIntSerializer(reportObject, property, dalObject, task);
			});
			DalConversion.conversionFunctions.Add("OrganizationFromTask", delegate(DalConversion instance, object reportObject, PropertyInfo property, object dalObject, object task)
			{
				instance.OrganizationFromTask(reportObject, property, dalObject, task);
			});
			DalConversion.conversionFunctions.Add("ValueFromTask", delegate(DalConversion instance, object reportObject, PropertyInfo property, object dalObject, object task)
			{
				instance.ValueFromTask(reportObject, property, dalObject, task);
			});
		}

		// Token: 0x060022DF RID: 8927 RVA: 0x0008DD73 File Offset: 0x0008BF73
		internal DalConversion(string method, string dalPropertyName, params string[] optionalDalPropertyNames)
		{
			this.methodName = method;
			this.dalPropertyName = dalPropertyName;
			this.optionalDalPropertyNames = optionalDalPropertyNames;
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x060022E0 RID: 8928 RVA: 0x0008DD90 File Offset: 0x0008BF90
		internal string DalPropertyName
		{
			get
			{
				return this.dalPropertyName;
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x060022E1 RID: 8929 RVA: 0x0008DD98 File Offset: 0x0008BF98
		internal string MethodName
		{
			get
			{
				return this.methodName;
			}
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x0008DDA0 File Offset: 0x0008BFA0
		internal void SetOutput(object reportObject, PropertyInfo property, object dalObject, object task)
		{
			DalConversion.ConversionDelegate conversionDelegate = DalConversion.conversionFunctions[this.methodName];
			conversionDelegate(this, reportObject, property, dalObject, task);
		}

		// Token: 0x060022E3 RID: 8931 RVA: 0x0008DDCC File Offset: 0x0008BFCC
		private void DefaultSerializer(object reportingObject, PropertyInfo property, object dalObject, object task)
		{
			Type type = dalObject.GetType();
			object value = type.GetProperty(this.dalPropertyName).GetValue(dalObject, null);
			if (value != null)
			{
				property.SetValue(reportingObject, value, null);
			}
		}

		// Token: 0x060022E4 RID: 8932 RVA: 0x0008DE00 File Offset: 0x0008C000
		private void DateFromStringSerializer(object reportingObject, PropertyInfo property, object dalObject, object task)
		{
			Type type = dalObject.GetType();
			object value = type.GetProperty(this.dalPropertyName).GetValue(dalObject, null);
			if (value != null)
			{
				property.SetValue(reportingObject, DateTime.Parse((string)value), null);
			}
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x0008DE44 File Offset: 0x0008C044
		private void DateFromIntSerializer(object reportingObject, PropertyInfo property, object dalObject, object task)
		{
			Type type = dalObject.GetType();
			object value = type.GetProperty(this.dalPropertyName).GetValue(dalObject, null);
			if (value != null)
			{
				int date = (int)value;
				int hour = 0;
				if (this.optionalDalPropertyNames.Length > 0)
				{
					value = type.GetProperty(this.optionalDalPropertyNames[0]).GetValue(dalObject, null);
					if (value != null)
					{
						hour = (int)value;
					}
				}
				DateTime dateTime = Schema.Utilities.FromQueryDate(date, hour);
				property.SetValue(reportingObject, dateTime, null);
			}
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x0008DEBC File Offset: 0x0008C0BC
		private void OrganizationFromTask(object reportingObject, PropertyInfo property, object dalObject, object task)
		{
			Type type = task.GetType();
			OrganizationIdParameter organizationIdParameter = type.GetProperty(this.dalPropertyName).GetValue(task, null) as OrganizationIdParameter;
			if (organizationIdParameter != null)
			{
				string value = (organizationIdParameter.InternalADObjectId != null) ? organizationIdParameter.InternalADObjectId.Name : organizationIdParameter.RawIdentity;
				property.SetValue(reportingObject, value, null);
			}
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x0008DF14 File Offset: 0x0008C114
		private void ValueFromTask(object reportingObject, PropertyInfo property, object dalObject, object task)
		{
			Type type = task.GetType();
			object value = type.GetProperty(this.dalPropertyName).GetValue(task, null);
			object value2 = ValueConvertor.ConvertValue(value, property.PropertyType, null);
			property.SetValue(reportingObject, value2, null);
		}

		// Token: 0x04001B76 RID: 7030
		private const string RegexSplitPattern = ",";

		// Token: 0x04001B77 RID: 7031
		private static readonly Dictionary<string, DalConversion.ConversionDelegate> conversionFunctions = new Dictionary<string, DalConversion.ConversionDelegate>();

		// Token: 0x04001B78 RID: 7032
		private readonly string methodName;

		// Token: 0x04001B79 RID: 7033
		private readonly string dalPropertyName;

		// Token: 0x04001B7A RID: 7034
		private string[] optionalDalPropertyNames;

		// Token: 0x020003CD RID: 973
		internal static class Methods
		{
			// Token: 0x04001B80 RID: 7040
			internal const string Default = "DefaultSerializer";

			// Token: 0x04001B81 RID: 7041
			internal const string DateFromString = "DateFromStringSerializer";

			// Token: 0x04001B82 RID: 7042
			internal const string DateFromInt = "DateFromIntSerializer";

			// Token: 0x04001B83 RID: 7043
			internal const string Organization = "OrganizationFromTask";

			// Token: 0x04001B84 RID: 7044
			internal const string FromTask = "ValueFromTask";
		}

		// Token: 0x020003CE RID: 974
		// (Invoke) Token: 0x060022EE RID: 8942
		private delegate void ConversionDelegate(DalConversion instance, object reportObject, PropertyInfo property, object dalObject, object task);
	}
}
