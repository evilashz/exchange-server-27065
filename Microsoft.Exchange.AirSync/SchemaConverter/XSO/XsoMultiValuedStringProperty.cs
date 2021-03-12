using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000209 RID: 521
	internal class XsoMultiValuedStringProperty : XsoProperty, IMultivaluedProperty<string>, IProperty, IEnumerable<string>, IEnumerable
	{
		// Token: 0x0600142C RID: 5164 RVA: 0x00074686 File Offset: 0x00072886
		public XsoMultiValuedStringProperty(StorePropertyDefinition propertyDef) : base(propertyDef)
		{
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x0007468F File Offset: 0x0007288F
		public int Count
		{
			get
			{
				return ((string[])base.XsoItem.TryGetProperty(base.PropertyDef)).Length;
			}
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x000747BC File Offset: 0x000729BC
		public IEnumerator<string> GetEnumerator()
		{
			object obj = base.XsoItem.TryGetProperty(base.PropertyDef);
			if (obj is PropertyError)
			{
				bool flag = false;
				if (flag)
				{
					yield return "Business";
				}
			}
			else
			{
				string[] strs = (string[])obj;
				for (int idx = 0; idx < strs.Length; idx++)
				{
					yield return strs[idx];
				}
			}
			yield break;
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x000747D8 File Offset: 0x000729D8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x000747E0 File Offset: 0x000729E0
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
			IMultivaluedProperty<string> multivaluedProperty = srcProperty as IMultivaluedProperty<string>;
			if (multivaluedProperty == null)
			{
				throw new UnexpectedTypeException("IMultivaluedProperty<string>", srcProperty);
			}
			string[] array = new string[multivaluedProperty.Count];
			int num = 0;
			foreach (string text in multivaluedProperty)
			{
				array[num++] = text;
			}
			base.XsoItem[base.PropertyDef] = array;
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x00074864 File Offset: 0x00072A64
		protected override void InternalSetToDefault(IProperty srcProperty)
		{
			base.XsoItem[base.PropertyDef] = new string[0];
		}
	}
}
