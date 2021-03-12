using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020007AE RID: 1966
	[ComVisible(true)]
	public class SoapAttribute : Attribute
	{
		// Token: 0x060055F3 RID: 22003 RVA: 0x001301B7 File Offset: 0x0012E3B7
		internal void SetReflectInfo(object info)
		{
			this.ReflectInfo = info;
		}

		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x060055F4 RID: 22004 RVA: 0x001301C0 File Offset: 0x0012E3C0
		// (set) Token: 0x060055F5 RID: 22005 RVA: 0x001301C8 File Offset: 0x0012E3C8
		public virtual string XmlNamespace
		{
			get
			{
				return this.ProtXmlNamespace;
			}
			set
			{
				this.ProtXmlNamespace = value;
			}
		}

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x060055F6 RID: 22006 RVA: 0x001301D1 File Offset: 0x0012E3D1
		// (set) Token: 0x060055F7 RID: 22007 RVA: 0x001301D9 File Offset: 0x0012E3D9
		public virtual bool UseAttribute
		{
			get
			{
				return this._bUseAttribute;
			}
			set
			{
				this._bUseAttribute = value;
			}
		}

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x060055F8 RID: 22008 RVA: 0x001301E2 File Offset: 0x0012E3E2
		// (set) Token: 0x060055F9 RID: 22009 RVA: 0x001301EA File Offset: 0x0012E3EA
		public virtual bool Embedded
		{
			get
			{
				return this._bEmbedded;
			}
			set
			{
				this._bEmbedded = value;
			}
		}

		// Token: 0x04002728 RID: 10024
		protected string ProtXmlNamespace;

		// Token: 0x04002729 RID: 10025
		private bool _bUseAttribute;

		// Token: 0x0400272A RID: 10026
		private bool _bEmbedded;

		// Token: 0x0400272B RID: 10027
		protected object ReflectInfo;
	}
}
