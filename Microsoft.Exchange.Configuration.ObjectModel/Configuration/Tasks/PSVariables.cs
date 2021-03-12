using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000081 RID: 129
	public class PSVariables : IVariableDictionary
	{
		// Token: 0x060004FF RID: 1279 RVA: 0x00011C3F File Offset: 0x0000FE3F
		public PSVariables(PSVariableIntrinsics psVariables)
		{
			this.variables = psVariables;
		}

		// Token: 0x17000143 RID: 323
		public object this[string name]
		{
			get
			{
				return this.variables.GetValue(name);
			}
			set
			{
				this.variables.Set(name, value);
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00011C6C File Offset: 0x0000FE6C
		public void Set(string name, object value, VariableScopedOptions scope)
		{
			this.variables.Set(new PSVariable(name, value, (ScopedItemOptions)scope));
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00011C8E File Offset: 0x0000FE8E
		public bool ContainsName(string name)
		{
			return this.variables.Get(name) != null;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00011CA4 File Offset: 0x0000FEA4
		public bool TryGetValue(string name, out object value)
		{
			value = null;
			PSVariable psvariable = this.variables.Get(name);
			if (psvariable != null)
			{
				value = psvariable.Value;
			}
			return psvariable != null;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00011CD3 File Offset: 0x0000FED3
		public void Remove(string name)
		{
			this.variables.Remove(name);
		}

		// Token: 0x04000121 RID: 289
		private readonly PSVariableIntrinsics variables;
	}
}
