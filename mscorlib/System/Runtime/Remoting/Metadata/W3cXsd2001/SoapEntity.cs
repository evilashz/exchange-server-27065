using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020007CF RID: 1999
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapEntity : ISoapXsd
	{
		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06005704 RID: 22276 RVA: 0x00131F69 File Offset: 0x00130169
		public static string XsdType
		{
			get
			{
				return "ENTITY";
			}
		}

		// Token: 0x06005705 RID: 22277 RVA: 0x00131F70 File Offset: 0x00130170
		public string GetXsdType()
		{
			return SoapEntity.XsdType;
		}

		// Token: 0x06005706 RID: 22278 RVA: 0x00131F77 File Offset: 0x00130177
		public SoapEntity()
		{
		}

		// Token: 0x06005707 RID: 22279 RVA: 0x00131F7F File Offset: 0x0013017F
		public SoapEntity(string value)
		{
			this._value = value;
		}

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06005708 RID: 22280 RVA: 0x00131F8E File Offset: 0x0013018E
		// (set) Token: 0x06005709 RID: 22281 RVA: 0x00131F96 File Offset: 0x00130196
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x0600570A RID: 22282 RVA: 0x00131F9F File Offset: 0x0013019F
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x0600570B RID: 22283 RVA: 0x00131FAC File Offset: 0x001301AC
		public static SoapEntity Parse(string value)
		{
			return new SoapEntity(value);
		}

		// Token: 0x04002774 RID: 10100
		private string _value;
	}
}
