using System;
using System.Reflection;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000169 RID: 361
	public abstract class DDIDecoratorAttribute : DDIValidateAttribute
	{
		// Token: 0x17001A8A RID: 6794
		// (get) Token: 0x06002207 RID: 8711 RVA: 0x000669D4 File Offset: 0x00064BD4
		// (set) Token: 0x06002208 RID: 8712 RVA: 0x000669DC File Offset: 0x00064BDC
		public Type AttributeType { get; set; }

		// Token: 0x06002209 RID: 8713 RVA: 0x000669E5 File Offset: 0x00064BE5
		public DDIDecoratorAttribute(string description) : base(description)
		{
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x000669F0 File Offset: 0x00064BF0
		protected DDIValidateAttribute GetDDIAttribute()
		{
			DDIValidateAttribute result = null;
			if (this.AttributeType != null)
			{
				try
				{
					result = (Activator.CreateInstance(this.AttributeType) as DDIValidateAttribute);
				}
				catch (ArgumentException)
				{
				}
				catch (NotSupportedException)
				{
				}
				catch (TargetInvocationException)
				{
				}
				catch (MethodAccessException)
				{
				}
				catch (MemberAccessException)
				{
				}
				catch (TypeLoadException)
				{
				}
			}
			return result;
		}
	}
}
