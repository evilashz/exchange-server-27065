using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006CC RID: 1740
	[DataContract]
	public class SortOptions
	{
		// Token: 0x1700280D RID: 10253
		// (get) Token: 0x060049DE RID: 18910 RVA: 0x000E1502 File Offset: 0x000DF702
		// (set) Token: 0x060049DF RID: 18911 RVA: 0x000E150A File Offset: 0x000DF70A
		[DataMember]
		public string PropertyName { get; set; }

		// Token: 0x1700280E RID: 10254
		// (get) Token: 0x060049E0 RID: 18912 RVA: 0x000E1513 File Offset: 0x000DF713
		// (set) Token: 0x060049E1 RID: 18913 RVA: 0x000E151B File Offset: 0x000DF71B
		[DataMember]
		public SortDirection Direction { get; set; }

		// Token: 0x060049E2 RID: 18914 RVA: 0x000E1524 File Offset: 0x000DF724
		public Func<L[], L[]> GetSortFunction<L>()
		{
			if (string.IsNullOrEmpty(this.PropertyName))
			{
				return null;
			}
			PropertyInfo property = typeof(L).GetProperty(this.PropertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
			if (property == null)
			{
				throw new FaultException(Strings.InvalidSortProperty);
			}
			MethodInfo method = typeof(SortOptions).GetMethod("GetSortFunctionByProperty", BindingFlags.Instance | BindingFlags.NonPublic);
			MethodInfo methodInfo = method.MakeGenericMethod(new Type[]
			{
				typeof(L),
				property.PropertyType
			});
			return (Func<L[], L[]>)methodInfo.Invoke(this, new object[]
			{
				property
			});
		}

		// Token: 0x060049E3 RID: 18915 RVA: 0x000E1614 File Offset: 0x000DF814
		private Func<L[], L[]> GetSortFunctionByProperty<L, P>(PropertyInfo sortProperty)
		{
			Func<L, P> keySelector = (L item) => (P)((object)sortProperty.GetValue(item, null));
			if (this.Direction == SortDirection.Ascending)
			{
				return (L[] array) => array.OrderBy(keySelector).ToArray<L>();
			}
			return (L[] array) => array.OrderByDescending(keySelector).ToArray<L>();
		}

		// Token: 0x060049E4 RID: 18916 RVA: 0x000E16B0 File Offset: 0x000DF8B0
		public Func<JsonDictionary<object>[], JsonDictionary<object>[]> GetDDISortFunction()
		{
			Func<JsonDictionary<object>, object> keySelector = (JsonDictionary<object> item) => item[this.PropertyName];
			if (this.Direction == SortDirection.Ascending)
			{
				return (JsonDictionary<object>[] array) => array.OrderBy(keySelector).ToArray<JsonDictionary<object>>();
			}
			return (JsonDictionary<object>[] array) => array.OrderByDescending(keySelector).ToArray<JsonDictionary<object>>();
		}
	}
}
