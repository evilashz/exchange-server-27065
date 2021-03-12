using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020007AA RID: 1962
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
	[ComVisible(true)]
	public sealed class SoapTypeAttribute : SoapAttribute
	{
		// Token: 0x060055CB RID: 21963 RVA: 0x0012FE4A File Offset: 0x0012E04A
		internal bool IsInteropXmlElement()
		{
			return (this._explicitlySet & (SoapTypeAttribute.ExplicitlySet.XmlElementName | SoapTypeAttribute.ExplicitlySet.XmlNamespace)) > SoapTypeAttribute.ExplicitlySet.None;
		}

		// Token: 0x060055CC RID: 21964 RVA: 0x0012FE57 File Offset: 0x0012E057
		internal bool IsInteropXmlType()
		{
			return (this._explicitlySet & (SoapTypeAttribute.ExplicitlySet.XmlTypeName | SoapTypeAttribute.ExplicitlySet.XmlTypeNamespace)) > SoapTypeAttribute.ExplicitlySet.None;
		}

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x060055CD RID: 21965 RVA: 0x0012FE65 File Offset: 0x0012E065
		// (set) Token: 0x060055CE RID: 21966 RVA: 0x0012FE6D File Offset: 0x0012E06D
		public SoapOption SoapOptions
		{
			get
			{
				return this._SoapOptions;
			}
			set
			{
				this._SoapOptions = value;
			}
		}

		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x060055CF RID: 21967 RVA: 0x0012FE76 File Offset: 0x0012E076
		// (set) Token: 0x060055D0 RID: 21968 RVA: 0x0012FEA4 File Offset: 0x0012E0A4
		public string XmlElementName
		{
			get
			{
				if (this._XmlElementName == null && this.ReflectInfo != null)
				{
					this._XmlElementName = SoapTypeAttribute.GetTypeName((Type)this.ReflectInfo);
				}
				return this._XmlElementName;
			}
			set
			{
				this._XmlElementName = value;
				this._explicitlySet |= SoapTypeAttribute.ExplicitlySet.XmlElementName;
			}
		}

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x060055D1 RID: 21969 RVA: 0x0012FEBB File Offset: 0x0012E0BB
		// (set) Token: 0x060055D2 RID: 21970 RVA: 0x0012FEDF File Offset: 0x0012E0DF
		public override string XmlNamespace
		{
			get
			{
				if (this.ProtXmlNamespace == null && this.ReflectInfo != null)
				{
					this.ProtXmlNamespace = this.XmlTypeNamespace;
				}
				return this.ProtXmlNamespace;
			}
			set
			{
				this.ProtXmlNamespace = value;
				this._explicitlySet |= SoapTypeAttribute.ExplicitlySet.XmlNamespace;
			}
		}

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x060055D3 RID: 21971 RVA: 0x0012FEF6 File Offset: 0x0012E0F6
		// (set) Token: 0x060055D4 RID: 21972 RVA: 0x0012FF24 File Offset: 0x0012E124
		public string XmlTypeName
		{
			get
			{
				if (this._XmlTypeName == null && this.ReflectInfo != null)
				{
					this._XmlTypeName = SoapTypeAttribute.GetTypeName((Type)this.ReflectInfo);
				}
				return this._XmlTypeName;
			}
			set
			{
				this._XmlTypeName = value;
				this._explicitlySet |= SoapTypeAttribute.ExplicitlySet.XmlTypeName;
			}
		}

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x060055D5 RID: 21973 RVA: 0x0012FF3B File Offset: 0x0012E13B
		// (set) Token: 0x060055D6 RID: 21974 RVA: 0x0012FF6A File Offset: 0x0012E16A
		public string XmlTypeNamespace
		{
			[SecuritySafeCritical]
			get
			{
				if (this._XmlTypeNamespace == null && this.ReflectInfo != null)
				{
					this._XmlTypeNamespace = XmlNamespaceEncoder.GetXmlNamespaceForTypeNamespace((RuntimeType)this.ReflectInfo, null);
				}
				return this._XmlTypeNamespace;
			}
			set
			{
				this._XmlTypeNamespace = value;
				this._explicitlySet |= SoapTypeAttribute.ExplicitlySet.XmlTypeNamespace;
			}
		}

		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x060055D7 RID: 21975 RVA: 0x0012FF81 File Offset: 0x0012E181
		// (set) Token: 0x060055D8 RID: 21976 RVA: 0x0012FF89 File Offset: 0x0012E189
		public XmlFieldOrderOption XmlFieldOrder
		{
			get
			{
				return this._XmlFieldOrder;
			}
			set
			{
				this._XmlFieldOrder = value;
			}
		}

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x060055D9 RID: 21977 RVA: 0x0012FF92 File Offset: 0x0012E192
		// (set) Token: 0x060055DA RID: 21978 RVA: 0x0012FF95 File Offset: 0x0012E195
		public override bool UseAttribute
		{
			get
			{
				return false;
			}
			set
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Attribute_UseAttributeNotsettable"));
			}
		}

		// Token: 0x060055DB RID: 21979 RVA: 0x0012FFA8 File Offset: 0x0012E1A8
		private static string GetTypeName(Type t)
		{
			if (!t.IsNested)
			{
				return t.Name;
			}
			string fullName = t.FullName;
			string @namespace = t.Namespace;
			if (@namespace == null || @namespace.Length == 0)
			{
				return fullName;
			}
			return fullName.Substring(@namespace.Length + 1);
		}

		// Token: 0x0400271A RID: 10010
		private SoapTypeAttribute.ExplicitlySet _explicitlySet;

		// Token: 0x0400271B RID: 10011
		private SoapOption _SoapOptions;

		// Token: 0x0400271C RID: 10012
		private string _XmlElementName;

		// Token: 0x0400271D RID: 10013
		private string _XmlTypeName;

		// Token: 0x0400271E RID: 10014
		private string _XmlTypeNamespace;

		// Token: 0x0400271F RID: 10015
		private XmlFieldOrderOption _XmlFieldOrder;

		// Token: 0x02000C3B RID: 3131
		[Flags]
		[Serializable]
		private enum ExplicitlySet
		{
			// Token: 0x04003709 RID: 14089
			None = 0,
			// Token: 0x0400370A RID: 14090
			XmlElementName = 1,
			// Token: 0x0400370B RID: 14091
			XmlNamespace = 2,
			// Token: 0x0400370C RID: 14092
			XmlTypeName = 4,
			// Token: 0x0400370D RID: 14093
			XmlTypeNamespace = 8
		}
	}
}
