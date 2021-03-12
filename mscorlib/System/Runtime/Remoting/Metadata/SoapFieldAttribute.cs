using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020007AC RID: 1964
	[AttributeUsage(AttributeTargets.Field)]
	[ComVisible(true)]
	public sealed class SoapFieldAttribute : SoapAttribute
	{
		// Token: 0x060055EC RID: 21996 RVA: 0x00130144 File Offset: 0x0012E344
		public bool IsInteropXmlElement()
		{
			return (this._explicitlySet & SoapFieldAttribute.ExplicitlySet.XmlElementName) > SoapFieldAttribute.ExplicitlySet.None;
		}

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x060055ED RID: 21997 RVA: 0x00130151 File Offset: 0x0012E351
		// (set) Token: 0x060055EE RID: 21998 RVA: 0x0013017F File Offset: 0x0012E37F
		public string XmlElementName
		{
			get
			{
				if (this._xmlElementName == null && this.ReflectInfo != null)
				{
					this._xmlElementName = ((FieldInfo)this.ReflectInfo).Name;
				}
				return this._xmlElementName;
			}
			set
			{
				this._xmlElementName = value;
				this._explicitlySet |= SoapFieldAttribute.ExplicitlySet.XmlElementName;
			}
		}

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x060055EF RID: 21999 RVA: 0x00130196 File Offset: 0x0012E396
		// (set) Token: 0x060055F0 RID: 22000 RVA: 0x0013019E File Offset: 0x0012E39E
		public int Order
		{
			get
			{
				return this._order;
			}
			set
			{
				this._order = value;
			}
		}

		// Token: 0x04002725 RID: 10021
		private SoapFieldAttribute.ExplicitlySet _explicitlySet;

		// Token: 0x04002726 RID: 10022
		private string _xmlElementName;

		// Token: 0x04002727 RID: 10023
		private int _order;

		// Token: 0x02000C3C RID: 3132
		[Flags]
		[Serializable]
		private enum ExplicitlySet
		{
			// Token: 0x0400370F RID: 14095
			None = 0,
			// Token: 0x04003710 RID: 14096
			XmlElementName = 1
		}
	}
}
