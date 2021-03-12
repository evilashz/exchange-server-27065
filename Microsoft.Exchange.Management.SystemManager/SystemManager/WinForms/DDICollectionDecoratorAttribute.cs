using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000F6 RID: 246
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
	public class DDICollectionDecoratorAttribute : DDIDecoratorAttribute
	{
		// Token: 0x0600093B RID: 2363 RVA: 0x000204A8 File Offset: 0x0001E6A8
		public DDICollectionDecoratorAttribute() : base("DDICollectionDecoratorAttribute")
		{
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x000204B5 File Offset: 0x0001E6B5
		// (set) Token: 0x0600093D RID: 2365 RVA: 0x000204BD File Offset: 0x0001E6BD
		[DefaultValue(false)]
		public bool ExpandInnerCollection { get; set; }

		// Token: 0x0600093E RID: 2366 RVA: 0x000204C8 File Offset: 0x0001E6C8
		public override List<string> Validate(object target, PageConfigurableProfile profile)
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
				foreach (object obj in enumerable)
				{
					if (this.ExpandInnerCollection && obj is IEnumerable)
					{
						using (IEnumerator enumerator2 = (obj as IEnumerable).GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								object target2 = enumerator2.Current;
								list.AddRange(ddiattribute.Validate(target2, profile));
							}
							continue;
						}
					}
					list.AddRange(ddiattribute.Validate(obj, profile));
				}
			}
			return list;
		}
	}
}
