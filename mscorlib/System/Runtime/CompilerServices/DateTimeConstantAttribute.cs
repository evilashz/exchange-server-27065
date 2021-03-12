using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000882 RID: 2178
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class DateTimeConstantAttribute : CustomConstantAttribute
	{
		// Token: 0x06005C7F RID: 23679 RVA: 0x00144988 File Offset: 0x00142B88
		[__DynamicallyInvokable]
		public DateTimeConstantAttribute(long ticks)
		{
			this.date = new DateTime(ticks);
		}

		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x06005C80 RID: 23680 RVA: 0x0014499C File Offset: 0x00142B9C
		[__DynamicallyInvokable]
		public override object Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this.date;
			}
		}

		// Token: 0x06005C81 RID: 23681 RVA: 0x001449AC File Offset: 0x00142BAC
		internal static DateTime GetRawDateTimeConstant(CustomAttributeData attr)
		{
			foreach (CustomAttributeNamedArgument customAttributeNamedArgument in attr.NamedArguments)
			{
				if (customAttributeNamedArgument.MemberInfo.Name.Equals("Value"))
				{
					return new DateTime((long)customAttributeNamedArgument.TypedValue.Value);
				}
			}
			return new DateTime((long)attr.ConstructorArguments[0].Value);
		}

		// Token: 0x04002956 RID: 10582
		private DateTime date;
	}
}
