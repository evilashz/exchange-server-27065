using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	internal sealed class SimplePropertyDefinition : ProviderPropertyDefinition
	{
		// Token: 0x06000088 RID: 136 RVA: 0x00003BAF File Offset: 0x00001DAF
		public SimplePropertyDefinition(string name, Type type, object defaultValue) : base(name, ExchangeObjectVersion.Exchange2010, type, defaultValue)
		{
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003BBF File Offset: 0x00001DBF
		public override bool IsMultivalued
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003BC2 File Offset: 0x00001DC2
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003BC5 File Offset: 0x00001DC5
		public override bool IsCalculated
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003BC8 File Offset: 0x00001DC8
		public override bool IsFilterOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003BCB File Offset: 0x00001DCB
		public override bool IsMandatory
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003BCE File Offset: 0x00001DCE
		public override bool PersistDefaultValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003BD1 File Offset: 0x00001DD1
		public override bool IsWriteOnce
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003BD4 File Offset: 0x00001DD4
		public override bool IsBinary
		{
			get
			{
				return false;
			}
		}
	}
}
