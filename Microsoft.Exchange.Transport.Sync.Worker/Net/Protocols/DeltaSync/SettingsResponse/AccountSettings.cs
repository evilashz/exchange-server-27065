using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000164 RID: 356
	[XmlType(TypeName = "AccountSettings", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class AccountSettings
	{
		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x0001D4D2 File Offset: 0x0001B6D2
		// (set) Token: 0x06000A6D RID: 2669 RVA: 0x0001D4DA File Offset: 0x0001B6DA
		[XmlIgnore]
		public int Status
		{
			get
			{
				return this.internalStatus;
			}
			set
			{
				this.internalStatus = value;
				this.internalStatusSpecified = true;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x0001D4EA File Offset: 0x0001B6EA
		// (set) Token: 0x06000A6F RID: 2671 RVA: 0x0001D505 File Offset: 0x0001B705
		[XmlIgnore]
		public AccountSettingsGet Get
		{
			get
			{
				if (this.internalGet == null)
				{
					this.internalGet = new AccountSettingsGet();
				}
				return this.internalGet;
			}
			set
			{
				this.internalGet = value;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x0001D50E File Offset: 0x0001B70E
		// (set) Token: 0x06000A71 RID: 2673 RVA: 0x0001D529 File Offset: 0x0001B729
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

		// Token: 0x040005C9 RID: 1481
		[XmlElement(ElementName = "Status", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalStatus;

		// Token: 0x040005CA RID: 1482
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalStatusSpecified;

		// Token: 0x040005CB RID: 1483
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(AccountSettingsGet), ElementName = "Get", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public AccountSettingsGet internalGet;

		// Token: 0x040005CC RID: 1484
		[XmlElement(Type = typeof(Set), ElementName = "Set", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Set internalSet;
	}
}
