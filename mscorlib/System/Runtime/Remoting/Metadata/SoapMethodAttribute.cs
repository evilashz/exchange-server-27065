using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020007AB RID: 1963
	[AttributeUsage(AttributeTargets.Method)]
	[ComVisible(true)]
	public sealed class SoapMethodAttribute : SoapAttribute
	{
		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x060055DD RID: 21981 RVA: 0x0012FFF7 File Offset: 0x0012E1F7
		internal bool SoapActionExplicitySet
		{
			get
			{
				return this._bSoapActionExplicitySet;
			}
		}

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x060055DE RID: 21982 RVA: 0x0012FFFF File Offset: 0x0012E1FF
		// (set) Token: 0x060055DF RID: 21983 RVA: 0x00130035 File Offset: 0x0012E235
		public string SoapAction
		{
			[SecuritySafeCritical]
			get
			{
				if (this._SoapAction == null)
				{
					this._SoapAction = this.XmlTypeNamespaceOfDeclaringType + "#" + ((MemberInfo)this.ReflectInfo).Name;
				}
				return this._SoapAction;
			}
			set
			{
				this._SoapAction = value;
				this._bSoapActionExplicitySet = true;
			}
		}

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x060055E0 RID: 21984 RVA: 0x00130045 File Offset: 0x0012E245
		// (set) Token: 0x060055E1 RID: 21985 RVA: 0x00130048 File Offset: 0x0012E248
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

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x060055E2 RID: 21986 RVA: 0x00130059 File Offset: 0x0012E259
		// (set) Token: 0x060055E3 RID: 21987 RVA: 0x00130075 File Offset: 0x0012E275
		public override string XmlNamespace
		{
			[SecuritySafeCritical]
			get
			{
				if (this.ProtXmlNamespace == null)
				{
					this.ProtXmlNamespace = this.XmlTypeNamespaceOfDeclaringType;
				}
				return this.ProtXmlNamespace;
			}
			set
			{
				this.ProtXmlNamespace = value;
			}
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x060055E4 RID: 21988 RVA: 0x0013007E File Offset: 0x0012E27E
		// (set) Token: 0x060055E5 RID: 21989 RVA: 0x001300B6 File Offset: 0x0012E2B6
		public string ResponseXmlElementName
		{
			get
			{
				if (this._responseXmlElementName == null && this.ReflectInfo != null)
				{
					this._responseXmlElementName = ((MemberInfo)this.ReflectInfo).Name + "Response";
				}
				return this._responseXmlElementName;
			}
			set
			{
				this._responseXmlElementName = value;
			}
		}

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x060055E6 RID: 21990 RVA: 0x001300BF File Offset: 0x0012E2BF
		// (set) Token: 0x060055E7 RID: 21991 RVA: 0x001300DB File Offset: 0x0012E2DB
		public string ResponseXmlNamespace
		{
			get
			{
				if (this._responseXmlNamespace == null)
				{
					this._responseXmlNamespace = this.XmlNamespace;
				}
				return this._responseXmlNamespace;
			}
			set
			{
				this._responseXmlNamespace = value;
			}
		}

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x060055E8 RID: 21992 RVA: 0x001300E4 File Offset: 0x0012E2E4
		// (set) Token: 0x060055E9 RID: 21993 RVA: 0x001300FF File Offset: 0x0012E2FF
		public string ReturnXmlElementName
		{
			get
			{
				if (this._returnXmlElementName == null)
				{
					this._returnXmlElementName = "return";
				}
				return this._returnXmlElementName;
			}
			set
			{
				this._returnXmlElementName = value;
			}
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x060055EA RID: 21994 RVA: 0x00130108 File Offset: 0x0012E308
		private string XmlTypeNamespaceOfDeclaringType
		{
			[SecurityCritical]
			get
			{
				if (this.ReflectInfo != null)
				{
					Type declaringType = ((MemberInfo)this.ReflectInfo).DeclaringType;
					return XmlNamespaceEncoder.GetXmlNamespaceForType((RuntimeType)declaringType, null);
				}
				return null;
			}
		}

		// Token: 0x04002720 RID: 10016
		private string _SoapAction;

		// Token: 0x04002721 RID: 10017
		private string _responseXmlElementName;

		// Token: 0x04002722 RID: 10018
		private string _responseXmlNamespace;

		// Token: 0x04002723 RID: 10019
		private string _returnXmlElementName;

		// Token: 0x04002724 RID: 10020
		private bool _bSoapActionExplicitySet;
	}
}
