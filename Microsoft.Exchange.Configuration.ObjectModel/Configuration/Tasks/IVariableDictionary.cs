using System;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200006B RID: 107
	public interface IVariableDictionary
	{
		// Token: 0x1700010B RID: 267
		object this[string name]
		{
			get;
			set;
		}

		// Token: 0x06000440 RID: 1088
		void Set(string name, object value, VariableScopedOptions scope);

		// Token: 0x06000441 RID: 1089
		bool ContainsName(string name);

		// Token: 0x06000442 RID: 1090
		bool TryGetValue(string name, out object value);

		// Token: 0x06000443 RID: 1091
		void Remove(string name);
	}
}
