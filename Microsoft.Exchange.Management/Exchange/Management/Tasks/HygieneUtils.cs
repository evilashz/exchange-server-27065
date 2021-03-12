using System;
using System.Reflection;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000EC RID: 236
	internal static class HygieneUtils
	{
		// Token: 0x060006FB RID: 1787 RVA: 0x0001CDB8 File Offset: 0x0001AFB8
		public static T InstantiateType<T>(string typeName)
		{
			if (string.IsNullOrWhiteSpace(typeName))
			{
				throw new ArgumentNullException("typeName");
			}
			Type type = HygieneUtils.hygieneDataAssembly.GetType(typeName, false);
			if (type == null)
			{
				throw new ArgumentException(string.Format("Could not load type {0}", typeName));
			}
			object obj = Activator.CreateInstance(type, false);
			if (obj == null)
			{
				throw new ArgumentException(string.Format("Could not create instance of type {0}", type.Name));
			}
			if (!(obj is T))
			{
				throw new ArgumentException(string.Format("Cannot cast instance of type {0} to {1}", type.Name, typeof(T).Name));
			}
			return (T)((object)obj);
		}

		// Token: 0x0400034E RID: 846
		private static readonly Assembly hygieneDataAssembly = Assembly.Load("Microsoft.Exchange.Hygiene.Data");
	}
}
