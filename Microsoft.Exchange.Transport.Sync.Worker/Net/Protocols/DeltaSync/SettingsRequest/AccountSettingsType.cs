using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x0200010C RID: 268
	[XmlType(TypeName = "AccountSettingsType", Namespace = "HMSETTINGS:")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class AccountSettingsType
	{
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x0001B322 File Offset: 0x00019522
		// (set) Token: 0x060007EE RID: 2030 RVA: 0x0001B33D File Offset: 0x0001953D
		[XmlIgnore]
		public AccountSettingsTypeGet Get
		{
			get
			{
				if (this.internalGet == null)
				{
					this.internalGet = new AccountSettingsTypeGet();
				}
				return this.internalGet;
			}
			set
			{
				this.internalGet = value;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x0001B346 File Offset: 0x00019546
		// (set) Token: 0x060007F0 RID: 2032 RVA: 0x0001B361 File Offset: 0x00019561
		[XmlIgnore]
		public Set Set
		{
			get
			{
				if (this.internalSet == null)
				{
					this.internalSet = new Set();
				}
				return this.internalSet;
			}
			set
			{
				this.internalSet = value;
			}
		}

		// Token: 0x04000451 RID: 1105
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(AccountSettingsTypeGet), ElementName = "Get", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public AccountSettingsTypeGet internalGet;

		// Token: 0x04000452 RID: 1106
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(Set), ElementName = "Set", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public Set internalSet;
	}
}
