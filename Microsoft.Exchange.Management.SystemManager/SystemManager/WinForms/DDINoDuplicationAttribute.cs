using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000FB RID: 251
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public class DDINoDuplicationAttribute : DDIValidateAttribute
	{
		// Token: 0x06000949 RID: 2377 RVA: 0x000207A7 File Offset: 0x0001E9A7
		public DDINoDuplicationAttribute() : base("DDINoDuplicationAttribute")
		{
			this.PropertyName = "Name";
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x000207BF File Offset: 0x0001E9BF
		// (set) Token: 0x0600094B RID: 2379 RVA: 0x000207C7 File Offset: 0x0001E9C7
		public string PropertyName { get; set; }

		// Token: 0x0600094C RID: 2380 RVA: 0x000207D0 File Offset: 0x0001E9D0
		public override List<string> Validate(object target, PageConfigurableProfile profile)
		{
			List<string> list = new List<string>();
			if (target != null)
			{
				IEnumerable enumerable = target as IEnumerable;
				if (enumerable == null)
				{
					throw new ArgumentException("DDINoDuplicationAttribute can only be applied to Collection object");
				}
				if (enumerable.OfType<object>().Count<object>() > 0)
				{
					PropertyInfo propertyInfo = null;
					if (!string.IsNullOrEmpty(this.PropertyName))
					{
						object obj = enumerable.OfType<object>().First<object>();
						propertyInfo = obj.GetType().GetProperty(this.PropertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
						if (propertyInfo == null)
						{
							throw new ArgumentException(string.Format("{0} is not a valid property in class {1}", this.PropertyName, obj.GetType().FullName));
						}
						enumerable.GetEnumerator().Reset();
					}
					Dictionary<object, int> dictionary = new Dictionary<object, int>();
					foreach (object obj2 in enumerable)
					{
						object obj3 = (propertyInfo != null) ? propertyInfo.GetValue(obj2, null) : obj2;
						if (obj3 != null)
						{
							dictionary[obj3] = (dictionary.ContainsKey(obj3) ? (dictionary[obj3] + 1) : 1);
						}
					}
					foreach (object obj4 in dictionary.Keys)
					{
						if (dictionary[obj4] > 1)
						{
							list.Add(string.Format("Duplicated element {0} found in collection", obj4));
						}
					}
				}
			}
			return list;
		}
	}
}
