using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200016A RID: 362
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
	public class DDICollectionDecoratorAttribute : DDIDecoratorAttribute
	{
		// Token: 0x0600220B RID: 8715 RVA: 0x00066A80 File Offset: 0x00064C80
		public DDICollectionDecoratorAttribute() : base("DDICollectionDecoratorAttribute")
		{
		}

		// Token: 0x17001A8B RID: 6795
		// (get) Token: 0x0600220C RID: 8716 RVA: 0x00066A8D File Offset: 0x00064C8D
		// (set) Token: 0x0600220D RID: 8717 RVA: 0x00066A95 File Offset: 0x00064C95
		[DefaultValue(false)]
		public bool ExpandInnerCollection { get; set; }

		// Token: 0x17001A8C RID: 6796
		// (get) Token: 0x0600220E RID: 8718 RVA: 0x00066A9E File Offset: 0x00064C9E
		// (set) Token: 0x0600220F RID: 8719 RVA: 0x00066AA6 File Offset: 0x00064CA6
		[DefaultValue(null)]
		public Type ObjectConverter { get; set; }

		// Token: 0x06002210 RID: 8720 RVA: 0x00066AB0 File Offset: 0x00064CB0
		public override List<string> Validate(object target, Service profile)
		{
			DDIValidateAttribute ddiattribute = base.GetDDIAttribute();
			if (ddiattribute == null)
			{
				throw new ArgumentException(string.Format("{0} is not a valid DDIAttribute", base.AttributeType));
			}
			List<string> list = new List<string>();
			if (target != null)
			{
				IEnumerable enumerable = target as IEnumerable;
				if (enumerable == null)
				{
					throw new ArgumentException("DDICollectionDecoratorAttribute can only be applied to collection target");
				}
				IDDIValidationObjectConverter iddivalidationObjectConverter = null;
				if (this.ObjectConverter != null)
				{
					iddivalidationObjectConverter = (Activator.CreateInstance(this.ObjectConverter) as IDDIValidationObjectConverter);
				}
				foreach (object obj in enumerable)
				{
					if (this.ExpandInnerCollection && obj is IEnumerable)
					{
						using (IEnumerator enumerator2 = (obj as IEnumerable).GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								object obj2 = enumerator2.Current;
								object target2 = (iddivalidationObjectConverter == null) ? obj2 : iddivalidationObjectConverter.ConvertTo(obj2);
								list.AddRange(ddiattribute.Validate(target2, profile));
							}
							continue;
						}
					}
					object target3 = (iddivalidationObjectConverter == null) ? obj : iddivalidationObjectConverter.ConvertTo(obj);
					list.AddRange(ddiattribute.Validate(target3, profile));
				}
			}
			return list;
		}
	}
}
