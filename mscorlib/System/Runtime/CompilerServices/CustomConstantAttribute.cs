using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000881 RID: 2177
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class CustomConstantAttribute : Attribute
	{
		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x06005C7C RID: 23676
		[__DynamicallyInvokable]
		public abstract object Value { [__DynamicallyInvokable] get; }

		// Token: 0x06005C7D RID: 23677 RVA: 0x00144908 File Offset: 0x00142B08
		internal static object GetRawConstant(CustomAttributeData attr)
		{
			foreach (CustomAttributeNamedArgument customAttributeNamedArgument in attr.NamedArguments)
			{
				if (customAttributeNamedArgument.MemberInfo.Name.Equals("Value"))
				{
					return customAttributeNamedArgument.TypedValue.Value;
				}
			}
			return DBNull.Value;
		}

		// Token: 0x06005C7E RID: 23678 RVA: 0x00144980 File Offset: 0x00142B80
		[__DynamicallyInvokable]
		protected CustomConstantAttribute()
		{
		}
	}
}
