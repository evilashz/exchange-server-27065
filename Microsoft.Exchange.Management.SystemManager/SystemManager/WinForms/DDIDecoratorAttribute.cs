using System;
using System.Reflection;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000F5 RID: 245
	public abstract class DDIDecoratorAttribute : DDIValidateAttribute
	{
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x000203FC File Offset: 0x0001E5FC
		// (set) Token: 0x06000938 RID: 2360 RVA: 0x00020404 File Offset: 0x0001E604
		public Type AttributeType { get; set; }

		// Token: 0x06000939 RID: 2361 RVA: 0x0002040D File Offset: 0x0001E60D
		public DDIDecoratorAttribute(string description) : base(description)
		{
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00020418 File Offset: 0x0001E618
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
